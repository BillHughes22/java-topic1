using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;



namespace FcsuAgentWebApp
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var heading = Request.QueryString["heading"];

           if(Request.FilePath == "/Account/Login.aspx")
            {
                chgHeading.InnerHtml = "FCSU Agent Portal";
            }
            if (heading != null)
            {
                if (heading == "director")
                {

                    chgHeading.InnerHtml = "FCSU Director portal";
                }
                else if(heading == "member")
                {
                    chgHeading.InnerHtml = "FCSU Member Portal";
                }
                else
                {
                    chgHeading.InnerHtml = "FCSU Agent Portal";
                }

            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
           
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
          //  MessageBox.Show("Don");
        }
       
        public void addNavMenu(string caption, string navUrl)
        {
            this.NavigationMenu.Items.Add(new MenuItem(caption,caption,"",navUrl));
        }

        public void addAgentMenu()
        {
            chgHeading.InnerHtml = "FCSU Agent Portal";
           this.NavigationMenu.Items.Add(new MenuItem("Clients", "Clients", "", "~/Agent/agentMain.aspx"));
            this.NavigationMenu.Items.Add(new MenuItem("Products", "Products", "", "http://fcsu.com/products","blank"));
            this.NavigationMenu.Items.Add(new MenuItem("Forms", "Forms", "", "http://fcsu.com/forms","blank"));
            this.NavigationMenu.Items.Add(new MenuItem("FCSU Illustration", "FCSU Illustration", "", "http://www.viscalc.com/fcsu/download.php?software=45e35357-b254-11e8-9fb7-00163ea2ab4c", "blank"));
            this.NavigationMenu.Items.Add(new MenuItem("Quotes", "Quotes", "", "http://fcsu.com/request-a-quote","blank"));
            this.NavigationMenu.Items.Add(new MenuItem("Marketing Materials", "Marketing Materials", "", "http://fcsu.com/agent-center/marketing-material","blank"));
            this.NavigationMenu.Items.Add(new MenuItem("Contact", "Contact", "", "http://fcsu.com/contact","blank"));
            this.NavigationMenu.Items.Add(new MenuItem("Change Password", "Change Password", "", "~/Account/ChangePassword.aspx"));
        }

        public void addAdminMenu()
        {
            chgHeading.InnerHtml = "FCSU Agent Portal";
           // this.NavigationMenu.Items.Add(new MenuItem("Admin", "Admin", "", "~/Admin/NewUserList.aspx"));
            this.NavigationMenu.Items.Add(new MenuItem("Agents List", "Agents List", "", "~/Admin/AgentList.aspx"));
            this.NavigationMenu.Items.Add(new MenuItem("Members List", "Members List", "", "~/Admin/MemberList.aspx"));
            this.NavigationMenu.Items.Add(new MenuItem("Directors List", "Directors List", "", "~/Admin/DirectorList.aspx"));
            this.NavigationMenu.Items.Add(new MenuItem("No Role Assigned List", "No Role Assigned List", "", "~/Admin/NoRoleTest.aspx"));
           
            //naga updateddate
            this.NavigationMenu.Items.Add(new MenuItem("User Info", "UserInfo", "", "~/Admin/AdminMain.aspx"));
            this.NavigationMenu.Items.Add(new MenuItem("Upload Files", "Upload", "", "~/Admin/UploadFile.aspx"));
            this.NavigationMenu.Items.Add(new MenuItem("Update or Delete Uploaded Files", "Delete", "", "~/Admin/DeleteUploadedFile.aspx")); 
        }

        public void addMemberMenu()
        {
            
               // removeHomeInNavMenu();
                this.NavigationMenu.Items.Add(new MenuItem("Member", "Member", "", "~/Member/memberMain.aspx"));
              
            chgHeading.InnerHtml = "FCSU Member Portal";
            MenuItem chgPswd = NavigationMenu.FindItem(@"Change Password");
           if( this.NavigationMenu.Items.Contains(chgPswd)) this.NavigationMenu.Items.Remove(chgPswd);
            this.NavigationMenu.Items.Add(new MenuItem("Change Password", "Change Password", "", "~/Account/ChangePassword.aspx"));
           
        }
        public void  addHeading(string heading)
        {

            chgHeading.InnerHtml = heading;
        }

        public void removeHomeInNavMenu()
        {
          //  this.NavigationMenu.Items.RemoveAt(0);
            chgHeading.InnerHtml = "FCSU Member Portal";
        }
        public void addDirectorMenu()
        {
            chgHeading.InnerHtml = "FCSU Director Portal";
            this.NavigationMenu.Items.Add(new MenuItem("Contact", "Contact", "", "http://fcsu.com/contact", "blank"));
            this.NavigationMenu.Items.Add(new MenuItem("Director", "Director", "", "~/Director/DirectorMenu.aspx"));
            MenuItem chgPswd = NavigationMenu.FindItem(@"Change Password");
            if (this.NavigationMenu.Items.Contains(chgPswd)) this.NavigationMenu.Items.Remove(chgPswd);
            this.NavigationMenu.Items.Add(new MenuItem("Change Password", "Change Password", "", "~/Account/ChangePassword.aspx"));
            this.NavigationMenu.Items.RemoveAt(0);
            
        }

        public void disableLogin()
        {
            HeadLoginView.Visible = false;
        }
       

        public void AddHomeForMember()
        {
           // this.NavigationMenu.Items.Add(new MenuItem("Home", "Home", "", "~/Account/Login.aspx?heading=member"));
          
        }
        
    }
}
