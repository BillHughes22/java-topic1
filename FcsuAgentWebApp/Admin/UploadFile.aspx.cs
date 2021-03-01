using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp.Models;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;


namespace FcsuAgentWebApp.Admin
{
    public partial class UploadFile : System.Web.UI.Page
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
            //Fill Combo boxes
            FillMonths();
            FillYrs();

        }

        //Methods
        //-------
        protected void Upload(object sender, EventArgs e)
        {
            bool okToContinue = false;
            bool uploaded = false;
            bool fileExists = false;
            bool link = false;
            var userName = User.Identity.Name;
            if(String.IsNullOrWhiteSpace(FileUpload.FileName) &&
                string.IsNullOrWhiteSpace(txtLinkUpload.Text))
            {
                
                MessageBox.Show("Upload file or link");
            }
            else if(!String.IsNullOrWhiteSpace(FileUpload.PostedFile.FileName) &&
                !string.IsNullOrWhiteSpace(txtLinkUpload.Text))
            {
               
                MessageBox.Show("Upload either file or link");
            }
            else  
            {
                okToContinue = true;

            }
            if(okToContinue && !string.IsNullOrWhiteSpace(txtDescr.Text))
            {
              
                okToContinue = true;
            }
            else
            {
                MessageBox.Show("Fill Description");
                okToContinue = false;
            }
            if(okToContinue)
            {
               // if (Page.IsValid)
                {
                    string query = string.Empty;
                    if (!string.IsNullOrWhiteSpace(txtLinkUpload.Text))
                    {
                        link = true;
                    }
                    string filename = Path.GetFileName(FileUpload.PostedFile.FileName);
                    if(!link)
                    {
                        if (System.IO.File.Exists(Request.PhysicalApplicationPath + "/Uploads/" + filename))
                        {
                            fileExists = true;
                            lblMsg.Text = "A file with the same name already exists.";
                        }
                        if (!fileExists)
                        {
                            FileUpload.SaveAs(Request.PhysicalApplicationPath + "/Uploads/" + filename);
                        }
                    }

                    if (!fileExists || link)
                    {
                       
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                        if(link)
                        {
                             query = "insert into [filesdir] (Filename,Description, Datetime, UserId, Year, Month, Category, link) " +
                                                                               $"values ('{txtLinkUpload.Text}','{txtDescr.Text}', '{DateTime.Now.Date}','{userName}' , {cbBxYears.Text}, {cbBxMonths.Text},'{cbBxCategory.SelectedItem.Text}',1 )";
                        }
                        else
                        {
                             query = "insert into [filesdir] (Filename,Description, Datetime, UserId, Year, Month, Category, link) " +
                                                                               $"values ('{filename}','{txtDescr.Text}', '{DateTime.Now.Date}','{userName}' , {cbBxYears.Text}, {cbBxMonths.Text},'{cbBxCategory.SelectedItem.Text}',0 )";
                        }
                       
                        using (SqlConnection myCon = new SqlConnection(connectionString))
                        {
                            myCon.Open();
                            SqlCommand myCommand = new SqlCommand(query, myCon);
                            //myCommand.Parameters.AddWithValue("deppol", TextBoxPolicy.Text);
                            myCommand.ExecuteNonQuery();
                            myCon.Close();
                            uploaded = true;
                        }
                    }

                    if (!fileExists && uploaded)
                    {
                        lblMsg.Text = "File uploaded successfully";
                        txtDescr.Text = String.Empty;
                        txtLinkUpload.Text = string.Empty;
                    }


                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            
            
            
        }

        
        private void FillMonths()
        {
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < months.Length; i++)
            {
                if (!String.IsNullOrWhiteSpace(months[i]))
                {
                    cbBxMonths.Items.Add(new ListItem(months[i], (i+1).ToString()));
                }
            }
        }
        private void FillYrs()
        {
            for (int i = DateTime.Now.Year+1; i >= DateTime.Now.Year - 10; i--)
            {
                cbBxYears.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
}