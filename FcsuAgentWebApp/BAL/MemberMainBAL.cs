using FcsuAgentWebApp.DAL;
using FcsuAgentWebApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.BAL
{
    public class MemberMainBAL
    {
        public List<AgentPolicyModel> getInsPolicyDetails(string memnumber)
        {
            MemberMainDAL objAgent = new MemberMainDAL();
            return objAgent.getPolicyDetails(memnumber,string.Empty);
        }
        public List<AgentPolicyModel> getAnnPolicyDetails(string memnumber)
        {
            MemberMainDAL objAgent = new MemberMainDAL();
            return objAgent.getPolicyDetails(memnumber,"ANN");
        }
        public List<AgentPolicyModel> getSetPolicyDetails(string memnumber)
        {
            MemberMainDAL objAgent = new MemberMainDAL();
            return objAgent.getPolicyDetails(memnumber,"SETL");
        }
    }
}