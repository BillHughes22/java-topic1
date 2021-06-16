using FcsuAgentWebApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.DAL
{
    public class DeleteUploadedFileDAL
    {
        public List<FileDirModel> getFilesDirectory()
        {
            List<FileDirModel> filesList = new List<FileDirModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;


            string usersSelectCommand = string.Format(@"SELECT [filesdir_i], [Filename], [Category], [Description], [Month], [Year] FROM [filesdir] order by (Year*100+ Month) desc");


            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                FileDirModel objFile;
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(usersSelectCommand, myConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objFile = new FileDirModel();
                    objFile.FilesDir_i = Convert.ToInt32(dr["filesdir_i"]);

                    objFile.FileName = Convert.ToString(dr["Filename"]);
                    objFile.Category = Convert.ToString(dr["Category"]);
                    objFile.Description = Convert.ToString(dr["Description"]);
                    objFile.Year = Convert.ToInt32(dr["Year"]);
                    objFile.Month = Convert.ToInt32(dr["Month"]);
                    
                    filesList.Add(objFile);
                }

                myConnection.Close();
            }
            return filesList;
        }

        public int editFilesDirectory(int year, int month, int filesdir_i)
        {
            List<FileDirModel> filesList = new List<FileDirModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;


            string usersSelectCommand = string.Format($"Update filesdir set Year={year}, Month= {month} WHERE filesdir_i = {filesdir_i}");
            int rowsUpdated;

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(usersSelectCommand, myConnection);
                rowsUpdated = myCommand.ExecuteNonQuery();

                myConnection.Close();
            }
            return rowsUpdated;
        }
    }
}