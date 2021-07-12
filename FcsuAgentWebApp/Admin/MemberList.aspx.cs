using FcsuAgentWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp.Admin
{
    public partial class MemberList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (HttpContext.Current.Request["myGVPageId"] != null)
                {
                    GridViewMem.PageIndex = Convert.ToInt32(HttpContext.Current.Request["myGVPageId"]);
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
           
            var viewmem = new UserListViewModel();
            GridViewMem.DataSource = viewmem.Members;
            GridViewMem.DataBind();
        }

        protected void GridViewMem_RowSelected(object sender, EventArgs e)
        {
            var row = GridViewMem.SelectedRow;
            var userPk = GridViewMem.DataKeys[row.RowIndex].Value;
            Session["gridIndex"] = GridViewMem.PageIndex;
            Session["prevurl"] = "../admin/MemberList.aspx";
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + userPk.ToString());
        }

        protected void AddUser(object sender, CommandEventArgs e)
        {
            Session["prevurl"] = "../admin/MemberList.aspx";
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + Guid.Empty.ToString());
        }

        protected void GridViewMem_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewMem.PageIndex = e.NewPageIndex;
            var view = new UserListViewModel();
            GridViewMem.DataSource = view.Members;

            this.GridViewMem.DataBind();
        }
    }
}