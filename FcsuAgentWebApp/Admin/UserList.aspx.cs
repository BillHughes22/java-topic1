using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp.Models;

namespace FcsuAgentWebApp.Admin
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var view = new UserListViewModel();
            //GridViewUsers.DataSource = view.Users;
            //GridViewUsers.DataBind();
        }


        protected void GridViewUsers_RowSelected(object sender, EventArgs e)
        {
            //var row = GridViewUsers.SelectedRow;
            //var userPk = GridViewUsers.DataKeys[row.RowIndex].Value;
            //Response.Redirect("../admin/UserEdit.aspx?userPk=" + userPk.ToString());
        }

        protected void AddUser(object sender, CommandEventArgs e)
        {
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + Guid.Empty.ToString());
        }
    }
}