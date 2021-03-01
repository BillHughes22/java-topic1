using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        string unameorpsd = string.Empty;
        string lname = string.Empty;
        string fname = string.Empty;
        string uname = string.Empty;
        string ssn = string.Empty;
        string month = string.Empty;
        string day = string.Empty;
        string year = string.Empty;
        string dob = string.Empty;
        string msg= string.Empty;
        string getuname = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.removeHomeInNavMenu();
            this.Master.addHeading("FCSU Member Portal");
            //validations won't work
            btncancel.CausesValidation = false;
            unameorpsd = Session["unameorpswd"].ToString();
            if(unameorpsd.Equals("uname"))
            {
                heading.Text = "Verify following details to get username";
                legheading.InnerHtml = "Forgot Username";
                txtUName.Visible = false;
                lblUName.Visible = false;
            }
            else
            {
                txtUName.Visible = true;
                lblUName.Visible = true;
            }
            lname = txtLname.Text;
            fname = txtFname.Text;
            uname = txtUName.Text;
            month = txtMon.Text;
            day = txtDay.Text;
            year = txtYear.Text;
            ssn = txtSSN.Text;
            msg = lblmsg.Text;
            dob= string.Concat(month, '/', day, '/', year);
            txtLname.Text = Session["lname"] != null ? Session["lname"].ToString() : lname; 
            txtUName.Text = Session["uname"] != null ? Session["uname"].ToString() : uname;
            txtFname.Text = Session["fname"] != null ? Session["fname"].ToString() : fname; 
            txtSSN.Text = Session["ssn"] != null ? Session["ssn"].ToString() : ssn; 
            txtMon.Text = Session["month"] != null ? Session["month"].ToString() : month; 
            txtDay.Text = Session["day"] != null ? Session["day"].ToString() : day; 
            txtYear.Text = Session["yr"] != null ? Session["yr"].ToString() : year;
            lblmsg.Text = Session["msg"] != null ? Session["msg"].ToString() : msg;

            Session["lname"] = null;
            Session["fname"] = null;
            Session["month"] = null;
            Session["yr"] = null;
            Session["day"] = null;
            Session["ssn"] = null;
            Session["msg"] = null;
            Session["uname"] = null;
         
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
           
            string cstnum = string.Empty;
            string getcst_numfrmMember = 
                $"select cst_num from [member] " +
                               $"where DOB = '{dob}' and lname = '{lname}' and fname like  '{fname}%' and altzip = {Convert.ToInt32(ssn) * 7} ";
           
            using (SqlConnection myConnection = new SqlConnection(connectionString))

            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(getcst_numfrmMember, myConnection);
                SqlDataReader reader = myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        cstnum = reader[0].ToString();

                    }
                }
                myConnection.Close();
                
            }
            if(!String.IsNullOrWhiteSpace(cstnum) && unameorpsd.Equals("uname"))
            { 
            using (SqlConnection myConnection = new SqlConnection(connectionString))

            {
                    string getunamequery = $"select username from [user] where membernumber = '{cstnum}'";

                    myConnection.Open();
                SqlCommand myCommand = new SqlCommand(getunamequery, myConnection);
                SqlDataReader reader = myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                            getuname = reader[0].ToString();
                            uname = reader[0].ToString();
                        }
                }
                myConnection.Close();
                }
            }
            if (String.IsNullOrWhiteSpace(cstnum))
            {
                Session["uname"] = txtUName.Text;
                Session["lname"] = txtLname.Text;
                Session["fname"] = txtFname.Text;
                Session["month"] = txtMon.Text;
                Session["yr"] = txtYear.Text;
                Session["day"] = txtDay.Text;
                Session["ssn"] = txtSSN.Text;
                Session["msg"] = "credentials didn't match try again";

               
                Response.Redirect("~/ForgotPassword.aspx");
            }
            else
            {
                Session["unameorpswd"] = unameorpsd;
                Session["forgotpassword"] = uname;
                Response.Redirect("~/AutenticationOut.aspx");
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("~/Account/Login.aspx?heading=member", true);
        }
    }
}