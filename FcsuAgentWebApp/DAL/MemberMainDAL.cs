using FcsuAgentWebApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace FcsuAgentWebApp.DAL
{
    public class MemberMainDAL
    {
        internal List<AgentPolicyModel> getPolicyDetails(string memnumber, string gridType)
        {
            List<AgentPolicyModel> policyList = new List<AgentPolicyModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            StringBuilder query = new StringBuilder();

            query.Append("SELECT policy.POLICY, member.NAME, member.ADDRESS, policy.PLANTYPE, policy.VALUE,");
            query.Append("policy.BEGBAL, policy.YTD_INT, policy.ANNRATE, policy.CURBAL, policy.POLDATE, policy.STATUS, member.PHONE, member.EMAIL,");
            query.Append("member.DOB, member.MEMBERDT, member.LASTNAME, policy.MATDATE, policy.MODE, policy.BASEPREM, policy.UPDATEDT, policy.LIEN,");
            query.Append("policy.LOAN, policy.CASHVAL, policy.PUADIV, policy.ACCUMDIV, policy.TOTDEATH, member_1.NAME AS oname, policy.paidto, " );
        query.Append("policy.rmdcurr, policy.rmdprev ");
        if(gridType.Equals("SETL")|| gridType.Equals("ANN"))
            {
                query.Append(",policy.pl_spia ");
            }
        query.Append("FROM policy ");
        query.Append("INNER JOIN member ON policy.CST_NUM = member.CST_NUM ");
        query.Append("INNER JOIN member AS member_1 ON policy.OWNNUM = member_1.CST_NUM ");
         
        if(gridType.Equals("SETL"))
                {
                query.Append($"WHERE(policy.OWNNUM = {memnumber} or policy.cst_num = {memnumber}) and policy.ANNRATE > 0 and policy.pl_spia = 'Y' ORDER BY member.LASTNAME ");
         }
        else if(gridType.Equals("ANN"))
         {
                query.Append($"WHERE(policy.OWNNUM = {memnumber} or policy.cst_num = {memnumber}) and policy.ANNRATE > 0 and pl_spia = 'N' ORDER BY member.LASTNAME ");

         }
            else
            {
                query.Append($"WHERE policy.OWNNUM = {memnumber} and policy.ANNRATE = 0  ORDER BY member.LASTNAME ");
            }


           string usersSelectCommand = query.ToString();
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
                    objPolicy.BegBal = (decimal)dr["BEGBAL"];
                    objPolicy.Ytd_int = (decimal)dr["YTD_INT"];
                    objPolicy.AnnRate = (decimal)dr["ANNRATE"];
                    objPolicy.Curbal = (decimal)dr["CURBAL"];
                    objPolicy.PolDate = dr["POLDATE"] != DBNull.Value ? (DateTime?)(dr["POLDATE"]) : null;
                    objPolicy.Status = dr["STATUS"].ToString();
                    objPolicy.Phone = dr["PHONE"].ToString();
                    objPolicy.Email = dr["EMAIL"].ToString();
                    objPolicy.Dob = dr["DOB"] != DBNull.Value ? (DateTime?)(dr["DOB"]) : null;
                    objPolicy.MemberDt = dr["MEMBERDT"] != DBNull.Value ? (DateTime?)(dr["MEMBERDT"]) : null;
                    objPolicy.LastName = Convert.ToString(dr["LASTNAME"]);
                    objPolicy.MatDate = dr["MATDATE"] != DBNull.Value ? (DateTime?)(dr["MATDATE"]) : null;
                    objPolicy.baseprem = (decimal)dr["baseprem"];
                    objPolicy.Updatedt = dr["updatedt"] != DBNull.Value ? (DateTime?)(dr["updatedt"]) : null;
                    objPolicy.Mode = dr["mode"].ToString();
                    objPolicy.loan= (decimal)dr["loan"];
                    objPolicy.rmdcurr = (decimal)dr["rmdcurr"];
                    objPolicy.rmdprev= (decimal)dr["rmdprev"];
                    objPolicy.lien = dr["lien"] != DBNull.Value ? (decimal?)dr["lien"]:null;
                    objPolicy.cashval = (decimal)dr["cashval"];
                    objPolicy.puadiv = (decimal)dr["puadiv"];
                    objPolicy.accumdiv = (decimal)dr["accumdiv"];
                    objPolicy.paidto = dr["paidto"] != DBNull.Value ? (DateTime?)(dr["paidto"]) : null;
                    objPolicy.oname = dr["oname"].ToString();
                    objPolicy.totdeath = (decimal)dr["totdeath"];

                    if(gridType.Equals("SETL")|| gridType.Equals("ANN"))
                    {
                        objPolicy.Pl_spia = dr["pl_spia"].ToString();

                    }


                    policyList.Add(objPolicy);
                }

                myConnection.Close();
            }
            return policyList;
        }

    }
}