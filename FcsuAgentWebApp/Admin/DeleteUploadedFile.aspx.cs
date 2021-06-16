using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp.BAL;
using FcsuAgentWebApp.Models.DataModels;

namespace FcsuAgentWebApp.Admin
{
    public partial class DeleteUploadedFile : System.Web.UI.Page
    {
        GridViewRow row;
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
            DeleteUploadedFileBAL deleteUploadedFileBal = new DeleteUploadedFileBAL();
            List<FileDirModel> filesList = deleteUploadedFileBal.getFilesDirectory();
            GridFilesDir.DataSource = filesList;
            GridFilesDir.DataBind();
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

       protected void GridFilesDir_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int Row=e.RowIndex+1;
            int filedir_i = Convert.ToInt32(row.Cells[2].Text);
            //int month = Convert.ToInt32(row.Cells[6].Text);
            //int year = Convert.ToInt32(row.Cells[7].Text);
            //DeleteUploadedFileBAL deleteUploadedFileBal = new DeleteUploadedFileBAL();
            //int countfilesList = deleteUploadedFileBal.editFilesDirectory(year, month, filedir_i);
            //GridFilesDir.EditIndex = -1;
            this.BindGrid();



        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridFilesDir.EditIndex = e.NewEditIndex;
            
            this.BindGrid();

        }
      
        protected void GridFilesDir_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //switch back to default mode
            GridFilesDir.EditIndex = -1;
            //Bind the grid
            BindGrid();
        }
        private void BindGrid()
        {
            DeleteUploadedFileBAL deleteUploadedFileBal = new DeleteUploadedFileBAL();
            List<FileDirModel> filesList = deleteUploadedFileBal.getFilesDirectory();
            GridFilesDir.DataSource = filesList;
            GridFilesDir.DataBind();
        }



    }
}