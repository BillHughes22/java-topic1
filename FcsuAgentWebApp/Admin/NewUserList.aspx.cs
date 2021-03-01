using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp.Models;

namespace FcsuAgentWebApp.Admin
{
    public partial class NewUserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.IsInRole("member"))
            {
                this.Master.addMemberMenu();
            }
            if (User.IsInRole("agent"))
            {
                this.Master.addAgentMenu();
            }
            if (User.IsInRole("admin"))
            {
                this.Master.addAdminMenu();
            }
            if (User.IsInRole("director"))
            {
                this.Master.addDirectorMenu();
            }
            var view = new UserListViewModel();
            GridViewUsers.DataSource = view.AllUsers;
            GridViewUsers.DataBind();
            

        }
      

        protected void GridViewUsers_RowSelected(object sender, EventArgs e)
        {
            var row = GridViewUsers.SelectedRow;
            var userPk = GridViewUsers.DataKeys[row.RowIndex].Value;
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + userPk.ToString());
        }
       


        protected void AddUser(object sender, CommandEventArgs e)
        {
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + Guid.Empty.ToString());
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUsers.PageIndex = e.NewPageIndex;
            var view = new UserListViewModel();
            GridViewUsers.DataSource = view.AllUsers;
            this.GridViewUsers.DataBind();
        }
       


    }
}