using FcsuAgentWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp.Admin
{
    public partial class NoRoleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                if (HttpContext.Current.Request["myGVPageId"] != null)
                {
                    GridViewNoRole.PageIndex = Convert.ToInt32(HttpContext.Current.Request["myGVPageId"]);
                }
            }
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
           
            var viewnorole = new Models.UserListViewModel();
            GridViewNoRole.DataSource = viewnorole.NoRole;
            GridViewNoRole.DataBind();
           
        }

        protected void GridViewNoRole_RowSelected(object sender, EventArgs e)
        {
            var row = GridViewNoRole.SelectedRow;
            var userPk = GridViewNoRole.DataKeys[row.RowIndex].Value;
            Session["gridIndex"] = GridViewNoRole.PageIndex;
            Session["prevurl"] = "../admin/NoRoleList.aspx";
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + userPk.ToString());
        }

        protected void AddUser(object sender, CommandEventArgs e)
        {
            Session["prevurl"] = "../admin/NoRoleList.aspx";
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + Guid.Empty.ToString());
        }

        protected void GridViewNoRole_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewNoRole.PageIndex = e.NewPageIndex;
            var view = new UserListViewModel();
            GridViewNoRole.DataSource = view.NoRole;

            this.GridViewNoRole.DataBind();
        }
    }
}