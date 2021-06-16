using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp.Classes;
using System.Data.SqlClient;
using FcsuAgentWebApp.Models;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace FcsuAgentWebApp.Account
{
    public partial class Register : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        string agentormember = string.Empty;
        string MsgLblData = string.Empty;
        string UserName = string.Empty;
        string Email = string.Empty;
        //string Phone = string.Empty;
        string phone1 = string.Empty;
        string phone2 = string.Empty;
        string phone3 = string.Empty;
        string FirstName = string.Empty;
        string Mon = string.Empty;
        string Day = string.Empty;
        string Yr = string.Empty;
        string LastName = string.Empty;
        string ss = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Members
            //-------
            agentormember = Session["user"].ToString();
            MsgLblData = Session["msg"] != null ?
                Session["msg"].ToString() : string.Empty;
            MsgLbl.Text = MsgLblData;
            bool memcreated = false;
            Label PswdLbl = (Label)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("PasswordLabel");
            Label lblMon = (Label)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("lblMon");
            TextBox Mon = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Mon");
            Label lblDay = (Label)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("lblDay");
            TextBox Day = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Day");
            Label lblYear = (Label)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("lblYr");
            TextBox Year = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Year");
            Label SSNLabel = (Label)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("SSNLabel");
            TextBox SSN = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("SSN");
            TextBox Lname = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Lname");
            TextBox Fname = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Fname");
            Label lnameLabel = (Label)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("lnameLabel");
            Label FnameLabel = (Label)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("lblFname");
            Label DOBLabel = (Label)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("DOBLabel");
            TextBox Email = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Email");
            TextBox Uname = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Username");
            // TextBox Ph = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Phone");
            TextBox Phone1 = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("txtph1");
            TextBox Phone2 = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("txtph2");
            TextBox Phone3 = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("txtph3");


            if (!memcreated)
            {
                string ssn = SSN.Text;
                string lname = Lname.Text;
                string fname = Fname.Text;
                string email = Email.Text;
                string ph1 = Phone1.Text;
                string ph2 = Phone2.Text;
                string ph3 = Phone3.Text;
                string mon = Mon.Text;
                string yr = Year.Text;
                string day = Day.Text;
                string uname = Uname.Text;

                SSN.Text = Session["ssn"] != null ? Session["ssn"].ToString() : ssn;
                Lname.Text= Session["lname"] != null ? Session["lname"].ToString() : lname;
                Fname.Text = Session["fname"] != null ? Session["fname"].ToString() : fname;
                Year.Text = Session["yr"] != null ? Session["yr"].ToString() : yr;
                Day.Text = Session["day"] != null ? Session["day"].ToString() : day;
                Mon.Text = Session["month"] != null ? Session["month"].ToString() : mon;
                Email.Text = Session["email"] != null ? Session["email"].ToString() : email;
               // Ph.Text = Session["ph"] != null ? Session["ph"].ToString() : ph;
               Phone1.Text= Session["ph1"] != null ? Session["ph1"].ToString() : ph1;
                Phone2.Text = Session["ph2"] != null ? Session["ph2"].ToString() : ph2;
                Phone3.Text = Session["ph3"] != null ? Session["ph3"].ToString() : ph3;
                Uname.Text = Session["uname"] != null ? Session["uname"].ToString() : uname;

                Session["lname"] = null;
                Session["fname"] = null;
                Session["month"] = null;
                Session["yr"] = null;
                Session["day"] = null;
                Session["ssn"] = null;
               // Session["ph"] = null;
                Session["uname"] = null;
                Session["email"] = null;
                Session["ph1"] = null;
                Session["ph2"] = null;
                Session["ph3"] = null;
               
            }
                
                
            
                if (agentormember.Equals("member"))
                {

                    this.Master.disableLogin();
                    this.Master.removeHomeInNavMenu();
                    this.Master.AddHomeForMember();

            
                     //PswdLbl.Font.Italic = true;
                PswdLbl.Text = "Password:"+ "<i> (minimum 8 characters - 1 uppercase, 1 lowercase, 1 special character and 1 number) </i> ";
              
               
            }
                else
                {
                    lblMon.Visible = false;
                    lblDay.Visible = false;
                    lblYear.Visible = false;
                    Mon.Visible = false;
                    Day.Visible = false;
                    Year.Visible = false;
                    Mon.Text = "01";
                    Day.Text = "01";
                    Year.Text = DateTime.Now.Year.ToString();

                    SSN.Visible = false;
                    SSN.Text = "9999";
                    Lname.Visible = false;
                    Lname.Text = "test";
                    Fname.Visible = false;
                    Fname.Text = "test";
                    SSNLabel.Visible = false;
                    lnameLabel.Visible = false;
                    FnameLabel.Visible = false;
                    DOBLabel.Visible = false;

                    Label lblemail = (Label)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("EmailLabel");
                    Label lblUserName = (Label)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("UserNameLabel");
                    lblemail.Style.Add("margin-top", "-70px");
                    lblUserName.Style.Add("margin-top", "-70px");
                    RegularExpressionValidator RemoveRegExpPswd = (RegularExpressionValidator)((CreateUserWizardStep)RegisterUserWizardStep.FindControl("RegisterUserWizardStep")).ContentTemplateContainer.FindControl("RegularExpressionValidator1");
                    RemoveRegExpPswd.Enabled = false;
                    //RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];


                }
            
           
            //moved to else
            ////RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
            RegisterUser.ContinueDestinationPageUrl = "success.aspx";
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);
            // TextBox phoneNum = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Phone");
            TextBox Phn1 = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("txtph1");
            TextBox Phn2 = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("txtph2");
            TextBox Phn3 = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("txtph3");
            TextBox lname = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Lname");
            TextBox fname = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Fname");
            //insert phonenum-added on 07/20 both mmem
            
            string insertSqlStatement = $"update [User] set phone='{string.Concat(Phn1.Text, Phn2.Text, Phn3.Text)}', FirstName= '{fname.Text}', lastname= '{lname.Text}' " +
                $"where UserName = '{RegisterUser.UserName}'";
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(insertSqlStatement, myConnection);
                //myCommand.Parameters.AddWithValue("deppol", TextBoxPolicy.Text);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }

           
            if (agentormember.Equals("member"))
            {
                TextBox UserName = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("UserName");
                TextBox Email = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Email");
                TextBox Ph1 = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("txtph1");
                TextBox Ph2 = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("txtph2");
                TextBox Ph3 = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("txtph3");
                TextBox Mon = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Mon");
                TextBox Day = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Day");
                TextBox Year = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Year");
                string DOBdata = string.Concat(Mon.Text, '/', Day.Text, '/', Year.Text);//DOB.Text;
                TextBox SSN = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("SSN");
                TextBox Lname = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Lname");
                TextBox Fname = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Fname");
                decimal ssnData = Convert.ToDecimal(SSN.Text);
                String fnameData = Fname.Text;
                fnameData = fnameData.Substring(0, 1);
                String lnameData = Lname.Text;
                lnameData = lnameData.Replace(" ", "");
                lnameData = lnameData.Replace("'", "");
                var cstNum = string.Empty;
                var ownNum = string.Empty;
                var altzip = string.Empty;
                bool cstNumFoundinMemberTbl = false;
                bool cstNumFoundinUserTbl = false;
               
                int countpoliciesCstNum = 0;



                string getcst_numfrmMember = $"select cst_num from [member] " +
                    $"where DOB = '{DOBdata}' and lname = '{lnameData}' and fname like  '{fnameData}%' and altzip = {ssnData * 7} and altzip <> 0";
                string checkcstnumalreadyexists = string.Empty;
                using (SqlConnection myConnection = new SqlConnection(connectionString))

                {
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand(getcst_numfrmMember, myConnection);
                    SqlDataReader reader = myCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cstNumFoundinMemberTbl = true;
                            cstNum = reader[0].ToString();

                        }
                    }
                    myConnection.Close();
                    // reader.Close();
                    // in case a member num registered and trying to register again will not register


                    // and ownnum = cst_num
                    using (SqlConnection myConnPolicies = new SqlConnection(connectionString))
                        {
                            string getcountPolicies = $"select count(policy)  from [policy] where cst_num = '{cstNum}'   ";

                            myConnPolicies.Open();
                            SqlCommand myCommand1 = new SqlCommand(getcountPolicies, myConnPolicies);
                            SqlDataReader readercount = myCommand1.ExecuteReader();
                            if (readercount.HasRows)
                            {
                                while (readercount.Read())
                                {
                                   
                                        countpoliciesCstNum = Convert.ToInt32(readercount[0]);
                                  

                                }
                            }
                            myConnPolicies.Close();
                        }
                    
                }
                //if(cstNumFoundinMemberTbl)
                //{
                //    string getaltzipfrmMember = $"select altzip from [member] " +
                //                          $"where DOB = '{DOBdata}' and lname = '{lnameData}' and fname like  '{fnameData}%' and altzip = {ssnData * 7} and altzip <> 0";

                //    using (SqlConnection myConnection = new SqlConnection(connectionString))

                //    {
                //        myConnection.Open();
                //        SqlCommand myCommand = new SqlCommand(getaltzipfrmMember, myConnection);
                //        SqlDataReader reader = myCommand.ExecuteReader();
                //        if (reader.HasRows)
                //        {
                //            while (reader.Read())
                //            {
                //                altzip = reader[0].ToString();

                //            }
                //        }
                //        myConnection.Close();

                //    }
                //    if (altzip == "0")
                //    {
                //        string insertSqlStatement1 = $"update [User] set memaltzip = {ssnData} " +
                //         $"where UserName = '{RegisterUser.UserName}'";
                //        using (SqlConnection myConnection = new SqlConnection(connectionString))
                //        {
                //            myConnection.Open();
                //            SqlCommand myCommand = new SqlCommand(insertSqlStatement1, myConnection);
                //            //myCommand.Parameters.AddWithValue("deppol", TextBoxPolicy.Text);
                //            myCommand.ExecuteNonQuery();
                //            myConnection.Close();
                //        }

                //    }
                //}
               

                if (cstNum != string.Empty)
                {
                    using (SqlConnection myConnection1 = new SqlConnection(connectionString))
                    {
                        checkcstnumalreadyexists = $"select membernumber  from [user] where membernumber = '{cstNum}' ";

                        myConnection1.Open();
                        SqlCommand myCommand1 = new SqlCommand(checkcstnumalreadyexists, myConnection1);
                        SqlDataReader reader1 = myCommand1.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                cstNumFoundinUserTbl = true;
                                // cstNum = reader[0].ToString();
                            }
                        }
                        myConnection1.Close();
                    }
                }



                var currentUser = (FcsuMembershipUser)new FcsuWebMembershipProvider().GetUser(RegisterUser.UserName, true);
                var view = new UserEditViewModel
                {
                    UserPk = currentUser.UserPk,
                    UserName = currentUser.UserName,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Email = currentUser.Email,
                    AgentNumber = 0.ToString(),
                    MemberNumber = cstNumFoundinMemberTbl && !cstNumFoundinUserTbl ? cstNum : 0.ToString(),
                    Phone = currentUser.Phone,
                    AgentMemberDescr = "Member",
                    Comments = String.Empty,
                    IsDisabled = false
                };

                List<string> chkListRoles = new List<string>();

                // Add items using Add method   
                chkListRoles.Add("admin");
                chkListRoles.Add("agent");
                chkListRoles.Add("member");
                chkListRoles.Add("staff");
                chkListRoles.Add("test");

                for (var i = 0; i < chkListRoles.Count; i++)
                {
                    view.UserRoles.Add(new RoleDataModel
                    {
                        RolePk = new Guid("7CD369F9-397B-4E96-A47A-1F3C4D9F0327"),
                        UserIsInRole = i == 2 ? true : false


                    });
                }



                var success = String.IsNullOrWhiteSpace(cstNum) || cstNumFoundinUserTbl ?
                    false : view.SaveUser(view);

                if ((!success && String.IsNullOrWhiteSpace(cstNum)) || cstNumFoundinUserTbl|| countpoliciesCstNum==0)
                {
                  //  string connectionStringdel = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                    string delRoleInfo = $"delete from [UserAndRoleXLink] where UserId = '{currentUser.UserPk}'";
                    string delSqlStatement = $"delete from [user] where Id ='{currentUser.UserPk}' and UserName='{currentUser.UserName}'";
                    using (SqlConnection myConnection = new SqlConnection(connectionString))
                    {
                        myConnection.Open();
                        SqlCommand myCommand = new SqlCommand(delRoleInfo, myConnection);
                        SqlCommand myCommand1 = new SqlCommand(delSqlStatement, myConnection);
                        myCommand.ExecuteNonQuery();
                        myCommand1.ExecuteNonQuery();
                        myConnection.Close();
                    }

                    if (Session["Login"] == null)
                    {
                        TextBox lname1 = (TextBox)this.RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Lname");
                        string dataLname = lname.Text;
                        FormsAuthentication.SignOut();
                                
                        Session["msg"] =  cstNumFoundinUserTbl ?"Member already registered try with different member details":
                          !cstNumFoundinUserTbl && cstNum.Equals(string.Empty)?  "Your entry does not match our records. Please try again. " +
                            " \nPlease contact the Home Office at 800-533-6682 for more details.":string.Empty;
                        if(Session["msg"].ToString()==string.Empty)
                        {
                            Session["msg"]=  countpoliciesCstNum == 0 ? "No Policies Found.  \nPlease contact the Home Office at 800-533-6682 for more details." : string.Empty;
                        }
                       
                            Session["user"] = "member";
                        Session["lname"] = Lname.Text;
                        Session["fname"] = Fname.Text;
                        Session["month"] = Mon.Text;
                        Session["yr"] = Year.Text;
                        Session["day"] =Day.Text;
                        Session["ssn"] = SSN.Text;
                        //  Session["ph"] = phoneNum.Text;
                        Session["ph1"] = Ph1.Text;
                        Session["ph2"] = Ph2.Text;
                        Session["ph3"] = Ph3.Text;
                        Session["uname"] =UserName.Text ;
                        Session["email"] = Email.Text;
                        
                        Response.Redirect("~/Account/Register.aspx", true);


                    }
                }
                else
                {
                    Session["agent"] = "member";
                    Response.Redirect("../account/Authentication.aspx");
                  
                }
            }
            else
            {
                string continueUrl = RegisterUser.ContinueDestinationPageUrl;
                if (String.IsNullOrEmpty(continueUrl))
                {
                    continueUrl = "~/";
                }
                Response.Redirect(continueUrl);
            }

           
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            if(agentormember=="member")
            {
                Response.Redirect("../account/Login.aspx?heading=member");
            }
            else
            {
                Response.Redirect("../account/Login.aspx");
            }
        }
    }
}
