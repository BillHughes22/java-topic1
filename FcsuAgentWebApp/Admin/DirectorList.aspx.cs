using FcsuAgentWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp.Admin
{
    public partial class DirectorList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (HttpContext.Current.Request["myGVPageId"] != null)
                {
                    GridViewDirector.PageIndex = Convert.ToInt32(HttpContext.Current.Request["myGVPageId"]);
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
           
            var viewdirector = new UserListViewModel();
            GridViewDirector.DataSource = viewdirector.Director;
            GridViewDirector.DataBind();
        }

        protected void AddUser(object sender, CommandEventArgs e)
        {

            Session["prevurl"] = "../admin/DirectorList.aspx";
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + Guid.Empty.ToString());
        }
        protected void GridViewDirector_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewDirector.PageIndex = e.NewPageIndex;
            var view = new UserListViewModel();
            GridViewDirector.DataSource = view.Director;

            this.GridViewDirector.DataBind();
        }


        protected void GridViewDirector_RowSelected(object sender, EventArgs e)
        {
            var row = GridViewDirector.SelectedRow;
            var userPk = GridViewDirector.DataKeys[row.RowIndex].Value;
            Session["gridIndex"] = GridViewDirector.PageIndex;
            Session["prevurl"] = "../admin/DirectorList.aspx";
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + userPk.ToString());
        }
    }
}