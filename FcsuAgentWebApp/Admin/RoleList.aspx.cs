using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp.Models.Admin;

namespace FcsuAgentWebApp.Admin
{
    public partial class RoleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var view = new RoleListViewModel();
            GridViewRoles.DataSource = view.Roles;
            GridViewRoles.DataBind();
        }


        protected void GridViewRoles_RowSelected(object sender, EventArgs e)
        {
            var row = GridViewRoles.SelectedRow;
            var userPk = GridViewRoles.DataKeys[row.RowIndex].Value;
            Response.Redirect("../admin/RoleEdit.aspx?rolePk=" + userPk.ToString());
        }

        protected void AddRole(object sender, CommandEventArgs e)
        {
            Response.Redirect("../admin/RoleEdit.aspx?rolePk=" + Guid.Empty.ToString());
        }
    }
}