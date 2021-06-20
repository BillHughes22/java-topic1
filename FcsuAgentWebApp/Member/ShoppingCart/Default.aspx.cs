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

namespace FcsuAgentWebApp.Member.ShoppingCart
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Make sure a member is logged in before allowing access to this page
            if (!User.IsInRole("member"))
            {
                Response.Redirect("/Member/ShoppingCart/Default.aspx");
            }

            // Configure the next button
            btnCart.Attributes.Add("onmouseover", "src='../images/next_over.jpg'");
            btnCart.Attributes.Add("onmouseout", "src='../images/next.jpg'");

            Label3a.Visible = false;
            if (!IsPostBack)
            {
                // Make sure there is an id value
                if (Session["orderID"] != null)
                {
                    // Populate the grid with all the cart rows
                    gv_cart_DataBind();
                    
                }
                else
                {
                    // Show message if there is no order id which means there are no entries for payment
                    Response.Write("<script>alert('Currently there are no payments in the shopping cart.  Please goto the previous page and enter a payment.');</script>");
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------
        // Delete a cart item
        //-------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Manage the removal of a cart item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_cart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // *** Retreive the DataGridRow
            int row = -1, id = 0;
            string policyColumn = "";
            int.TryParse(e.CommandArgument as string, out row);
            GridViewRow gdrow = gv_cart.Rows[row];
            // Lets get the id of the row that was selected
            try
            {
                id = Convert.ToInt32(gdrow.Cells[6].Text);
                policyColumn = gdrow.Cells[1].Text;
            }
            catch (Exception)
            {
                // Show message if there is no order id which means there are no entries for payment
                Response.Write("<script>alert('The system can not process the delete request.  Please try again.');</script>");
            }
            // Can not allow the user to delete a surcharge row
            if (policyColumn == "Surcharge")
            {
                // Show message if there is no order id which means there are no entries for payment
                Response.Write("<script>alert('Surcharge column can not be removed.');</script>");
            }
            else
            {
                switch (e.CommandName)
                {
                    case "cmdRemove":

                        // Delete the one item from the shopping cart
                        // First we instantiate the business layer
                        BusinessLayer DeleteItem = new BusinessLayer();
                        // Make sure the process was successful
                        if (!DeleteItem.DeleteCheckoutItem(id))
                        {
                            // Show message if there is no order id which means there are no entries for payment
                            Response.Write("<script>alert('The system is having a problem deleting this payment.  Please try again.');</script>");
                        }

                        // Now lets refresh the grid with the updated data.
                        gv_cart_DataBind();
                        break;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        // End Delete a cart item
        //-------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------
        // Bind the data to the grid
        //-------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Bind the data to the gv_cart grid
        /// </summary>
        protected void gv_cart_DataBind()
        {
            // Populate the grid with all the cart rows
            BusinessLayer GetCartContents = new BusinessLayer();
            var cartContents = GetCartContents.GetAllCheckoutItems(Convert.ToInt32(Session["orderID"]));
            gv_cart.DataSource = cartContents.Item1.ToList();
            gv_cart.DataBind();
            
            // Ok now lets update the total
            lbl_total.Text = string.Format("{0:C}", Convert.ToDecimal(cartContents.Item2.ToString()));
        }
        //-------------------------------------------------------------------------------------------------------------------------
        // End Bind the data to the grid
        //-------------------------------------------------------------------------------------------------------------------------
                
        //-------------------------------------------------------------------------------------------------------------------------
        //
        // Continue Checkout button was pressed
        //
        //-------------------------------------------------------------------------------------------------------------------------
        protected void btnCart_Click(object sender, ImageClickEventArgs e)
        {
            // Now lets redirect to the address page
            //Response.Redirect("~/address");
            Response.Redirect("/Member/Payments/Default.aspx");

        }
        //-------------------------------------------------------------------------------------------------------------------------
        //
        // End Continue Checkout button was pressed
        //
        //-------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------
        //
        // Previous button was pressed
        //
        //-------------------------------------------------------------------------------------------------------------------------
        protected void btnPrevious_Click(object sender, ImageClickEventArgs e)
        {
            // Now lets redirect to the address page
            //Response.Redirect("~/address");
            Response.Redirect("/Member/memberMain.aspx");

        }
        //-------------------------------------------------------------------------------------------------------------------------
        //
        // End Previous button was pressed
        //
        //-------------------------------------------------------------------------------------------------------------------------

    }
}