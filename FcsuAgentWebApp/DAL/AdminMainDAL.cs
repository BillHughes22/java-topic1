using FcsuAgentWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.DAL
{
    public class AdminMainDAL
    {

        public List<UserListDataModel> getUsersDetails()
        {
            List<UserListDataModel> usersList = new List<UserListDataModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
           

            string usersSelectCommand = string.Format(@"SELECT a.[UserName], a.[AgentNumber], a.[Email], [FirstName], [LastName], [IsDisabled], [Comments], [MemberNumber], c.[UpdatedDate]  FROM [User]a Left Join ( (select distinct (username),updatedDate from [UpdatedDateInfo] a where updateddate in (select Top (1) updateddate from [UpdatedDateInfo] b where b.userName = a.username order by updateddate desc))) c on a.[UserName] = c.[UserName] ORDER BY a.[IsDisabled], a.[LastName], a.[FirstName]");


            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                UserListDataModel objUser;
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(usersSelectCommand, myConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                foreach(DataRow dr in  ds.Tables[0].Rows)
                {

                    objUser = new UserListDataModel();
                    objUser.LastName = Convert.ToString( dr["LastName"]);
                    objUser.FirstName = Convert.ToString(dr["FirstName"]);
                    objUser.Email = Convert.ToString(dr["Email"]);
                    objUser.AgentNumber = Convert.ToString(dr["AgentNumber"]);
                    objUser.MemberNumber = Convert.ToString(dr["MemberNumber"]);
                    objUser.IsDisabled = Convert.ToBoolean(dr["IsDisabled"]);
                    objUser.UserName = Convert.ToString(dr["UserName"]);
                    objUser.UpdatedDate = dr["UpdatedDate"] != DBNull.Value ?(DateTime?)(dr["UpdatedDate"]) : null;
                     objUser.Comments = Convert.ToString(dr["Comments"]);
                    usersList.Add(objUser);
                }

                myConnection.Close();
            }
            return usersList;
        }

    }
}