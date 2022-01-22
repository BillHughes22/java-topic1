//------------------------------------------------------------------------------------------
// Updated 12/29/2021
// By: Bill Hughes
// Purpose: Integrate KeyBank payment process
//-----------------------------------------------------------------------------------------
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
using FcsuAgentWebApp.Models.CheckoutPayPal;
using Com.Alacriti.Checkout;
using System.Net;
using Com.Alacriti.Checkout.Api;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace FcsuAgentWebApp.Member.Payments
{
    public partial class Default : System.Web.UI.Page
    {
        //Define Variables

        public object live_mode = false;

        // Variables for KeyBank
        string keyBankToken, keyBankDigiSign, keyBankCustRef;


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
                Response.Redirect("~/Member/ShoppingCart/Default.aspx");
            }

            Label3a.Visible = false;
            // This next line was used for testing to keep the order_id to 7
            //Session["order_id"] = "7";
            // Lets only do this once
            // Create a connection to the SQL database located on the server.
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "First Message");

            // Only perform this upon page load and not post backs
            if (!IsPostBack)
            {
                //-------------------------------------------------------------------------------------------------------------
                // New section for reading POST response for KeyBank
                //-------------------------------------------------------------------------------------------------------------

                // Define the array that will contain the json contents.
                // Request.Form.AllKeys is used to return the collection of form elements that posted to HTTP request body.
                string[] keys = Request.Form.AllKeys;
                // We know if there is a data when the collection is not empty
                if (keys.Length > 1)
                {
                    

                    // Iterate through the array and pull out the required data.
                    for (int i = 0; i < keys.Length; i++)
                    {
                        // Retrieve the token
                        if (keys[i] == "token")
                        {
                            keyBankToken = Request[keys[i]];
                        }
                        // Retrieve the digiSign
                        if (keys[i] == "digiSign")
                        {
                            keyBankDigiSign = Request[keys[i]];
                        }
                        // Retrieve the customer_account_reference
                        if (keys[i] == "customer_account_reference")
                        {
                            keyBankCustRef = Request[keys[i]];
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", keyBankCustRef); // used for testing
                        }
                    }


                    try
                    {
                        //Checkout.initProperties("E:\\web\\fraterna\\portfcs\\Files\\orbipay_checkout_config.json");
                        //Checkout.initProperties("~\\Files\\orbipay_checkout_config.json");
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        Checkout.initProperties("C:\\Users\\bill\\source\\repos\\FcsAgentWebApp\\FcsuAgentWebApp\\Files\\orbipay_checkout_config.json");

                        // Customer Identifiers
                        string feeAmount = "0.00";
                        string liveMode = "false";
                        //String customFields = "";
                        Dictionary<string, string> customFields = new Dictionary<string, string>();

                        string client_key = "2421355621";
                        string signatureKey = "4K79PXBSPP20SMB4";
                        string clientApiKey = "capik_c62af8fe-5aa0-4aa1-96d6-c78aa06639f7";

                        //Com.Alacriti.Checkout.Model.Payment payment = new Com.Alacriti.Checkout.Api.Payment(keyBankCustRef, Session["cartTotal"].ToString())
                        //    .withToken(keyBankToken, keyBankDigiSign)
                        //    //.withFee(feeAmount)
                        //    .forClient(client_key, signatureKey, clientApiKey)
                        //    //.forClient(client_key, signatureKey)
                        //    //.withCustomFields(customFields)
                        //    .confirm();

                        Com.Alacriti.Checkout.Model.Payment payment = new Com.Alacriti.Checkout.Api.Payment(keyBankCustRef, Session["cartTotal"].ToString())
                                 .withToken(keyBankToken, keyBankDigiSign)
                                 .forClient(client_key, signatureKey, clientApiKey)
                                 .confirm("false");

                        //var obj = new paymentStructure
                        //{

                        //}

                        // Convert the object to a json string
                        var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(payment);
                        var details = JObject.Parse(jsonString);
                        // Get the Confirmation Number so we can store it to the db
                        string TransactionID = details["confirmation_number"].ToString();
                        
                        //string bill26 = (details["funding_account"]["account_number"]).ToString();

                        // Update the KeyBank Transaction ID
                        BusinessLayer UpdateKeyBankTransID = new BusinessLayer();
                        bool isSuccessful = UpdateKeyBankTransID.UpdateKeyBankTransID(Convert.ToInt32(Session["orderID"]), TransactionID);

                    }
                    catch (Exception exp)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "There was an error!");
                        //Label4.Text = exp.Message;
                    }

                    string bill = "test";

                    //Payment payment = new Com.Alacriti.Checkout.Api.Payment(customer_account_reference, amount)
                    //    .withToken(token, digiSign)
                    //    .withFee(feeAmount)
                    //    .forClient(client_key, signatureKey, clientApiKey)
                    //    .withCustomFields(customFields)
                    //    .confirm("false");

                    //string bill = payment;

                }
                //-------------------------------------------------------------------------------------------------------------
                // End -- New section for reading POST response for KeyBank
                //-------------------------------------------------------------------------------------------------------------


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