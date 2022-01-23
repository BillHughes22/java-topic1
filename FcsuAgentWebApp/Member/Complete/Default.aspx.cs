using Com.Alacriti.Checkout;
using FcsuAgentWebApp.Models.CheckoutPayPal;
using FcsuAgentWebApp.Services.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp.Member.Complete
{
    public partial class Default : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            // Make sure a member is logged in before allowing access to this page
            if (!User.IsInRole("member"))
            {
                Response.Redirect("~/Member/ShoppingCart/Default.aspx");
            }
            //string fullname1 = Request.QueryString["details"];

            string TransactionID;
            // Check if the transaction is KeyBank or PayPal
            if (Session["Transaction"].ToString() == "KeyBank")
            {
                // We are in the KeyBank code block
                TransactionID = Session["Transaction_ID"].ToString();
            }
            else
            {
                // We are in the PayPal code block
                NameValueCollection nvc = Request.Form;
                TransactionID = nvc["JSON"];
            }
            // Apply the headers to the summary
            Transaction_ID_lbl.Text = TransactionID;
            CurrentDate.Text = DateTime.Now.ToString("F");

            //
            if (!IsPostBack)
            {
                // Make sure there is an id value
                if (Session["orderID"] != null)
                {
                    // Update the PayPal Transaction ID
                    BusinessLayer UpdatePayPalTransID = new BusinessLayer();
                    bool isSuccessful = UpdatePayPalTransID.UpdatePayPalTransID(Convert.ToInt32(Session["orderID"]), TransactionID);

                    // Populate the grid with all the cart rows
                    gv_cart_DataBind();
                    GetUserInfo();
                }
                else
                {
                    // Show message if there is no order id which means there are no entries for payment
                    Response.Write("<script>alert('Currently there is not a valid transaction ID.');</script>");
                }
            }

            
        }

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
            lbl_total.Text = string.Format("{0:C}", cartContents.Item2.ToString());
        }
        //-------------------------------------------------------------------------------------------------------------------------
        // End Bind the data to the grid
        //-------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------
        // Get the user information
        //-------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Get User Data
        /// </summary>
        protected void GetUserInfo()
        {
            // Populate the grid with all the cart rows
            BusinessLayer GetUserInfo = new BusinessLayer();
            UserInformation userData = new UserInformation();
            userData = GetUserInfo.GetUserInfo(Convert.ToDecimal(Session["member"]));
            lbluserName.Text = userData.userName;
            lbluserAddress.Text = userData.userAddress;
        }

        /// <summary>
        /// Return the user to their account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bttnReturn_Click(object sender, EventArgs e)
        {
            // Clear out the order information
            Session["orderID"] = null;
            Response.Redirect("~/Member/memberMain.aspx");
        }
        //-------------------------------------------------------------------------------------------------------------------------
        // End Bind the data to the grid
        //-------------------------------------------------------------------------------------------------------------------------


    }
}