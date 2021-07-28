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
    public class AgentMainDAL
    {
        internal List<AgentPolicyModel> getPolicyDetails(string searchText, string number, string sortColumn=null)
        {
            List<AgentPolicyModel> policyList = new List<AgentPolicyModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            string usersSelectCommand = string.Empty;
            string sort = sortColumn != null ? " order by "+sortColumn : " order by lastname";
            if (string.IsNullOrEmpty(searchText))
            {

                usersSelectCommand = string.Format(@"SELECT policy.POLICY, member.NAME, member.ADDRESS, policy.PLANTYPE, policy.VALUE, policy.BEGBAL, 
            policy.YTD_INT, policy.ANNRATE, policy.CURBAL, policy.POLDATE, policy.STATUS, member.PHONE, member.EMAIL, member.DOB, 
            member.MEMBERDT, member.LASTNAME, policy.MATDATE, policy.mode, policy.baseprem, policy.updatedt, policy.pl_spia 
            FROM policy INNER JOIN member ON 
            policy.CST_NUM = member.CST_NUM 
            WHERE policy.AGENT = {0} and ( member.LastName like  member.LASTNAME ) {1}" , number, sort );
            }//
            else {
                usersSelectCommand = string.Format(@"SELECT policy.POLICY, member.NAME, member.ADDRESS, policy.PLANTYPE, policy.VALUE, policy.BEGBAL, 
            policy.YTD_INT, policy.ANNRATE, policy.CURBAL, policy.POLDATE, policy.STATUS, member.PHONE, member.EMAIL, member.DOB, 
            member.MEMBERDT, member.LASTNAME, policy.MATDATE, policy.mode, policy.baseprem, policy.updatedt, policy.pl_spia 
            FROM policy INNER JOIN member ON 
            policy.CST_NUM = member.CST_NUM 
            WHERE policy.AGENT = {0} and ( member.LastName like CASE WHEN LEN('{1}') >0 THEN '{2}%' ELSE  member.LASTNAME END) {3}", number, searchText, searchText , sort);

            }
           
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                AgentPolicyModel objPolicy;
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(usersSelectCommand, myConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objPolicy = new AgentPolicyModel();
                    objPolicy.Policy = Convert.ToString(dr["POLICY"]);
                    objPolicy.Name = Convert.ToString(dr["NAME"]);
                    objPolicy.Address = dr["ADDRESS"].ToString();
                    objPolicy.PlanType = dr["PLANTYPE"].ToString();
                    objPolicy.Value = (decimal)dr["VALUE"];
                    objPolicy.BegBal =(decimal)dr["BEGBAL"];
                    objPolicy.Ytd_int = (decimal)dr["YTD_INT"];
                    objPolicy.AnnRate = (decimal)dr["ANNRATE"];
                    objPolicy.Curbal = (decimal)dr["CURBAL"];
                    objPolicy.PolDate = dr["POLDATE"] != DBNull.Value ? (DateTime?)(dr["POLDATE"]) : null;
                    objPolicy.Status =dr["STATUS"].ToString();
                    objPolicy.Phone = dr["PHONE"].ToString();
                    objPolicy.Email = dr["EMAIL"].ToString();
                    objPolicy.Dob = dr["DOB"] != DBNull.Value ? (DateTime?)(dr["DOB"]) : null;
                    objPolicy.MemberDt = dr["MEMBERDT"] != DBNull.Value ? (DateTime?)(dr["MEMBERDT"]) : null;
                    objPolicy.LastName = Convert.ToString(dr["LASTNAME"]);
                    objPolicy.MatDate = dr["MATDATE"] != DBNull.Value ? (DateTime?)(dr["MATDATE"]) : null; 
                    objPolicy.baseprem = (decimal)dr["baseprem"];
                    objPolicy.Updatedt = dr["updatedt"] != DBNull.Value ? (DateTime?)(dr["updatedt"]) : null;
                    objPolicy.Mode = dr["mode"].ToString();
                    objPolicy.Pl_spia = dr["pl_spia"].ToString();
                   
                    policyList.Add(objPolicy);
                }

                myConnection.Close();
            }
            return policyList;
        }
        internal List<PolicyBeneficiaryModel> GetPolicyBeneficiaries(string policyNumber)
        {
            List<PolicyBeneficiaryModel> beneficiaryList = new List<PolicyBeneficiaryModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;


            string usersSelectCommand = string.Format($"SELECT policy, bname, relate, btype FROM bnefcary where policy = '{policyNumber}' order by btype");


            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                PolicyBeneficiaryModel objUser;
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(usersSelectCommand, myConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objUser = new PolicyBeneficiaryModel();
                    objUser.Policy = Convert.ToString(dr["policy"]);
                    objUser.BName = Convert.ToString(dr["bname"]);
                    objUser.Relate = Convert.ToString(dr["relate"]);
                    objUser.BType = Convert.ToString(dr["btype"]);

                    beneficiaryList.Add(objUser);
                }

                myConnection.Close();
            }
            return beneficiaryList;
        }
        internal List<PolicyRiderModel> GetPolicyRiders(string policyNumber)
        {
            List<PolicyRiderModel> riderList = new List<PolicyRiderModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;


            string usersSelectCommand = $"SELECT policy, riderprem1, riderprem2, riderprem3, ridertype1, ridertype2, ridertype3 FROM policy where policy = '{policyNumber}' and (riderprem1 > 00.00  or riderprem2 > 00.00 or  riderprem3 > 00.00)";


            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                PolicyRiderModel objUser;
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(usersSelectCommand, myConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    objUser = new PolicyRiderModel();
                    objUser.Policy = Convert.ToString(dr["policy"]);
                    objUser.RiderPrem1 = Convert.ToDecimal(dr["riderprem1"]);
                    objUser.RiderPrem2 = Convert.ToDecimal(dr["riderprem2"]);
                    objUser.RiderPrem3 = Convert.ToDecimal(dr["riderprem3"]);
                    objUser.RiderType1 = Convert.ToString(dr["ridertype1"]);
                    objUser.RiderType2 = Convert.ToString(dr["ridertype2"]);
                    objUser.RiderType3 = Convert.ToString(dr["ridertype3"]);
                     riderList.Add(objUser);
                }

                myConnection.Close();
            }
            return riderList;
        }
    }
    
}