using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp.Account
{
    public partial class Login : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
           
            var heading = Request.QueryString["heading"];
            memberFooter.Visible = false;
            if (heading != null)
            {
                
                    memberFooter.Visible = heading == "member"?true:false;
               
                if (heading == "director")
                {
                    Button ForgotPswd = (Button)this.LoginUser.FindControl("btnForgotPswd");
                    ForgotPswd.Visible = false;
                    Button ForgotUName = (Button)this.LoginUser.FindControl("btnForgotUserName");
                    ForgotUName.Visible = false;
                    Button registerMem = (Button)this.LoginUser.FindControl("RegisterMem");
                    registerMem.Visible = false;
                }
                if (heading == "member" || heading == "director")
                {

                    Button RegisterAgent = (Button)this.LoginUser.FindControl("RegisterAgent");
                    RegisterAgent.Visible = false;
                    this.Master.disableLogin();

                }
               
                
            }
            else //agent
            {
                Button registerMem = (Button)this.LoginUser.FindControl("RegisterMem");
                registerMem.Visible = false;
                Button ForgotPswd = (Button)this.LoginUser.FindControl("btnForgotPswd");
                ForgotPswd.Visible = false;
                Button ForgotUName = (Button)this.LoginUser.FindControl("btnForgotUserName");
                ForgotUName.Visible = false;
            }
            if (Session["Login"] != null)
            {
                //if (User.IsInRole("admin"))
                //{
                //    Response.Redirect("../admin/AgentList.aspx");
                //}
                //else if (User.IsInRole("member"))
                //{
                //    Response.Redirect("../member/memberMain.aspx");
                //}
                //else
                //    Response.Redirect("../agent/agentMain.aspx");

                //Response.Redirect("agentMain.aspx");
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);


            SetFocus(LoginUser.FindControl("UserName"));
        }

        protected void LoginUser_LoggedIn(object sender, EventArgs e)
        {
            Session["Login"] = "success"; 

            System.Web.UI.WebControls.Login login = sender as System.Web.UI.WebControls.Login;
            if (login != null)
            {
                var username = login.UserName;
                var roles = Roles.GetRolesForUser(username).ToList();

            
                if (roles.Any(x => x == "admin"))
                {
                    Session["Agent"] = "admin";
                    LoginUser.DestinationPageUrl = "../Account/Authentication.aspx";
                  //  LoginUser.DestinationPageUrl = "../admin/AgentList.aspx";
                }
                else if (roles.Any(x => x == "member"))
                {
                    Session["Agent"] = "member";
                    // LoginUser.DestinationPageUrl = "../member/memberMain.aspx";
                    LoginUser.DestinationPageUrl = "../Account/Authentication.aspx";
                }
                else if (roles.Any(x => x == "agent"))
                {
                    Session["Agent"] = "agent";
                    LoginUser.DestinationPageUrl = "../Account/Authentication.aspx";
                    // LoginUser.DestinationPageUrl = "../agent/agentMain.aspx";
                }
                else if (roles.Any(x => x == "director"))
                {
                    Session["Agent"] = "director";
                    LoginUser.DestinationPageUrl = "../Account/Authentication.aspx";
                    //Session["AgentorDirector"] = "director";
                    //int firstTymLoginInDirector=0;
                    //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                    //using (SqlConnection myConnection1 = new SqlConnection(connectionString))
                    //{
                    //    string checkFirstDirectorLogin = $"select count(*) from [UpdatedDateInfo] where UserName = '{username}' ";
                    //    myConnection1.Open();
                    //    SqlCommand myCommand1 = new SqlCommand(checkFirstDirectorLogin, myConnection1);
                    //    SqlDataReader reader1 = myCommand1.ExecuteReader();
                    //    if (reader1.HasRows)
                    //    {
                    //        while (reader1.Read())
                    //        {

                    //            firstTymLoginInDirector = (Int32)reader1[0];

                    //        }

                    //    }

                    //    myConnection1.Close();

                    //}
                    //if (firstTymLoginInDirector == 1)
                    //{
                    //    Session["directororAgent"] = "director";
                    //    LoginUser.DestinationPageUrl = "../Account/ChangePassword.aspx";
                    //}
                    //else
                    //{
                        
                    //    LoginUser.DestinationPageUrl = "../Director/DirectorMenu.aspx";
                    //}





                        
                }
                else
                    LoginUser.DestinationPageUrl = "../Account/Register.aspx";
            }

            //if (Roles.IsUserInRole(username,"admin"))
            //{
            //    LoginUser.DestinationPageUrl = "../admin/adminMain.aspx";
            //}
            //else if (Roles.IsUserInRole(username,"member"))
            //{
            //    LoginUser.DestinationPageUrl = "../member/memberMain.aspx";
            //}
            //else if (Roles.IsUserInRole(username,"agent"))
            //{
            //    LoginUser.DestinationPageUrl = "../default.aspx";
            //    //LoginUser.DestinationPageUrl = "../agent/agentMain.aspx";
            //}
            //else
            //    LoginUser.DestinationPageUrl = "../Account/Register.aspx";
            //    //LoginUser.DestinationPageUrl ="../agent/agentMain.aspx";
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
           
        }
   

        protected void AuthenticateUser(object sender, AuthenticateEventArgs e)
        {
            var login = sender as System.Web.UI.WebControls.Login;
           // if (login != null)
            {

               
                int noOfTimesLoginFailed = 0;
                var userName = login.UserName;
                var password = login.Password;
                e.Authenticated = Membership.ValidateUser(userName, password);
                bool isLoginFailed = e.Authenticated ? false : true;
                //get no of loginfailed
                using (SqlConnection myConnection1 = new SqlConnection(connectionString))
                {
                    string checkisLoginFailed = $"select count(*) from [UpdatedDateInfo] where UserName = '{userName}' and (DATEDIFF(d, UpdatedDate, GETDATE()) = 0) and isLoginFailed=1";
                    myConnection1.Open();
                    SqlCommand myCommand1 = new SqlCommand(checkisLoginFailed, myConnection1);
                    SqlDataReader reader1 = myCommand1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {

                            noOfTimesLoginFailed = (Int32)reader1[0];

                        }

                    }

                    myConnection1.Close();

                }
               
                if(noOfTimesLoginFailed>=5)
                {
                    isLoginFailed = true;
                   this.LoginUser.FailureText= "Access to my site is temporarily disabled...Contact the Home office at 800.533.6682 to reset your account.";
                    string insertSqlStatement = $"update [User] set [IsDisabled]=1 where UserName = '{userName}'";
                    using (SqlConnection myConnection = new SqlConnection(connectionString))
                    {
                        myConnection.Open();
                        SqlCommand myCommand = new SqlCommand(insertSqlStatement, myConnection);
                        //myCommand.Parameters.AddWithValue("deppol", TextBoxPolicy.Text);
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                    }

                }

                //Purnima insert 
                if(noOfTimesLoginFailed < 5)
                {
                    int loginFailed = isLoginFailed ? 1 : 0;
                    string insertSqlStatement = "INSERT into [UpdatedDateInfo] values ('" + login.UserName + "','" + DateTime.Now + "',"+ loginFailed + " )";
                    using (SqlConnection myConnection = new SqlConnection(connectionString))
                    {
                        myConnection.Open();
                        SqlCommand myCommand = new SqlCommand(insertSqlStatement, myConnection);
                        //myCommand.Parameters.AddWithValue("deppol", TextBoxPolicy.Text);
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                    }

                }

                //Purnima



            }
          //  else
            {

            }
        }
        protected void RegisterAgent_Click(object sender, EventArgs e)
        {
            Session["user"] = "agent";

            Response.Redirect("~/Account/Register.aspx", true);
        }

        protected void RegisterMemButton_Click(object sender, EventArgs e)
        {
            Session["user"] = "member";
            Response.Redirect("~/Account/Register.aspx", true);
        }

        protected void btnForgotPswd_Click(object sender, EventArgs e)
        {
            Session["unameorpswd"] = "pswd";
            Response.Redirect("~/ForgotPassword.aspx");
        }

        protected void btnForgotUserName_Click(object sender, EventArgs e)
        {
            Session["unameorpswd"] = "uname";
            Response.Redirect("~/ForgotPassword.aspx");
        }
    }
}
