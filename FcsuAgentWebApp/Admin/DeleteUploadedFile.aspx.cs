using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp.Admin
{
    public partial class DeleteUploadedFile : System.Web.UI.Page
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
        }
                      
        //Deletes file from folder
        protected void GridFilesDir_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int a= e.RowIndex;
            GridViewRow row = GridFilesDir.Rows[a];
            String filename = row.Cells[2].Text;
            string filePath = string.Concat(Request.PhysicalApplicationPath , "/Uploads/" , filename);
           if ( System.IO.File.Exists(filePath))
            {
                File.Delete(filePath);
            }
           
        }

        



     
    }
}