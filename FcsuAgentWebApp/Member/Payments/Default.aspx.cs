using System.Data;
using System.Data.SqlClient;
using System.Text;
using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Mail;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Web.Services;
using System.Text.RegularExpressions;
using FcsuAgentWebApp.Services.Business;
using FcsuAgentWebApp.Models.Checkout;

namespace FcsuAgentWebApp.Member.Payments
{
    public partial class Default : System.Web.UI.Page
    {
        //Define Variables

        SqlConnection myConnection;

        DateTime start_date, end_date;

        int LastInserted_id;

        string id, shipping, pickup, coupon_used, entire_order_coupon2;
        string coupon_name, discount_type, discount_applies, products, date_range_active, active, categories, categories_coupon, coupon_continue, entire_order_coupon, greater_than_coupon, qb_sku, category, category_coupon;



        Decimal discount_price, total_product_price, discount_total, discount_percentage_coupon, greater_price, coupon_amount, discount_price_coupon;
        Int32 discount_percentage, times_allowed, used, coupon_id, cart_id, coupon_qty;

        int quantity_value;
        Decimal total_price, shipping_price, shipping_cost_master, total_shipping;
        //-----------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------
        //
        // Define the Array that will be used in the coupon section
        //
        //-------------------------------------------------------------------------------------------------------------------------
        public class Cart_Contents
        {
            public int cart_id { get; set; }
            public string qb_sku { get; set; }
            public string category_each { get; set; }
            public string coupon_used { get; set; }
            public Decimal total_product_price { get; set; }
            public int coupon_qty { get; set; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //
        // End Array
        //
        //-------------------------------------------------------------------------------------------------------------------------

        protected void Page_Load(object sender, EventArgs e)
        {
            // Make sure a member is logged in before allowing access to this page
            if (!User.IsInRole("member"))
            {
                Response.Redirect("/Member/ShoppingCart/Default.aspx");
            }

            Label3a.Visible = false;
            // This next line was used for testing to keep the order_id to 7
            //Session["order_id"] = "7";
            // Lets only do this once
            // Create a connection to the SQL database located on the server.
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            // Only perform this upon page load and not post backs
            if (!IsPostBack)
            {
                // Lets first determine if the session variable is set.  If not then lets start a new order
                if (Session["orderID"] == null)
                {

                }

                //-------------------------------------------------------------------------------------------------------------------------
                //
                // Add selected products to the persons order
                //
                //-------------------------------------------------------------------------------------------------------------------------
                // We can only add products to an order if the session has started


                if (!IsPostBack)
                {
                    // Make sure there is an id value
                    if (Session["orderID"] != null)
                    {
                        // Populate the grid with all the cart rows
                        gv_summary_DataBind();
                    }
                    else
                    {
                        // Show message if there is no order id which means there are no entries for payment
                        Response.Write("<script>alert('Currently there are no payments in the shopping cart.  Please goto the previous page and enter a payment.');</script>");
                    }
                }



                //-------------------------------------------------------------------------------------------------------------------------
                //  End Adding products to the persons order
                //-------------------------------------------------------------------------------------------------------------------------

            }
        }

        /// <summary>
        /// Bind the data to the gv_cart grid
        /// </summary>
        protected void gv_summary_DataBind()
        {
            // Populate the grid with all the cart rows
            BusinessLayer GetCartContents = new BusinessLayer();
            var cartContents = GetCartContents.GetItemsJsonTotal(Convert.ToInt32(Session["orderID"]));
            
            gv_summary.DataSource = cartContents.Item1.ToList();
            gv_summary.DataBind();
            calculate_totals(Convert.ToInt32(Session["orderID"]));
            // Get the JSON items
            Session["cartItems"] = cartContents.Item2.ToString();
            // Set the Total Cost
            Session["cartTotal"] = cartContents.Item3.ToString();
            
            //Session["cartItems"] = bill;
            //string testString = "{name: 'T -Shirt', description: 'Green XL', sku: 'sku01', unit_amount: { currency_code: 'USD', value: '90.00'}, tax: { currency_code: 'USD', value: '0.00' }, quantity: '1'}";
            //Session["testing"] = testString;
        }

        //-------------------------------------------------------------------------------------------------------------------------
        // Pop Up Message Routine
        //-------------------------------------------------------------------------------------------------------------------------

        protected void popupmessage(String message)
        {
            //WilsToolbox.MessageBox.Show("This is my message");
            string script = "<script>alert('" + message + "');</script>";
            Page.RegisterStartupScript("sample pop up alert", script);
        }

        //-------------------------------------------------------------------------------------------------------------------------
        // End of Pop Up Message
        //-------------------------------------------------------------------------------------------------------------------------


        //-------------------------------------------------------------------------------------------------------------------------
        //
        // Calculate the totals and store them in the db
        //
        //-------------------------------------------------------------------------------------------------------------------------

        protected void calculate_totals(int total_id)
        {
            //Label3a.Text = "here................: " + total_id;
            // Lets see if we have any products to search through
            // Create a connection to the SQL database located on the server.
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            // Configure the SQL Statement
            String selectCmd = "SELECT amountPaid FROM [payhist] WHERE order_id = '" + total_id + "'";
            // Initialize the SqlCommand with the new SQL string and the connection information.
            SqlCommand myCommand2 = new SqlCommand(selectCmd, conn);
            //SqlDataReader reader;
            myCommand2.Connection.Open();
            SqlDataReader reader = myCommand2.ExecuteReader();

            // Set all the variables


            Decimal order_total = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Ok if rows were found we need to get the values

                    //Now lets take care of the total price
                    Decimal total_price = Convert.ToDecimal(reader["amountPaid"].ToString());

                    order_total = order_total + Convert.ToDecimal(reader["amountPaid"].ToString());



                }
                reader.Close();
                myCommand2.Connection.Close();

            }
            // Ok now lets update all the labels to show the correct data
            lbl_total.Text = string.Format("{0:C}", order_total);
            Session["order_total"] = order_total.ToString();
            string testString = "{name: 'T -Shirt', description: 'Green XL', sku: 'sku01', unit_amount: { currency_code: 'USD', value: '90.00'}, tax: { currency_code: 'USD', value: '10.00' }, quantity: '1', category: 'PHYSICAL_GOODS' }, {name: 'Shoes', description: 'Running, Size 10.5', sku: 'sku02', unit_amount: { currency_code: 'USD', value: '45.00'}, tax: { currency_code: 'USD', value: '5.00' }, quantity: '2', category: 'PHYSICAL_GOODS' }";

            //Session["testing"] = testString;

            // Lets setup the variables for PayPal Express
            Session["thetime"] = DateTime.Now;
            Session["thedate"] = DateTime.Now;
            Session["subweight"] = "0";
            Session["subtotal"] = order_total;
            Session["subsize"] = "0";
            Session["subquantity"] = "1";
            //Session["subship"] = order_shipping;
            Session["subhand"] = "";
            Session["orderid"] = Session["orderID"];
            // End PayPal Express variables


            gv_summary.DataBind();
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //
        // End Calculating Totals
        //
        //-------------------------------------------------------------------------------------------------------------------------



    }
}