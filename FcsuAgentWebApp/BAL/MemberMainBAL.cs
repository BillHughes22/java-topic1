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
        public List<AgentPolicyModel> getInsPolicyDetails(string memnumber, string sortfield=null)
        {
            MemberMainDAL objAgent = new MemberMainDAL();
            return objAgent.getPolicyDetails(memnumber,string.Empty, sortfield);
        }
        public List<AgentPolicyModel> getAnnPolicyDetails(string memnumber, string sortfield = null)
        {
            MemberMainDAL objAgent = new MemberMainDAL();
            return objAgent.getPolicyDetails(memnumber,"ANN", sortfield);
        }
        public List<AgentPolicyModel> getSetPolicyDetails(string memnumber, string sortfield = null)
        {
            MemberMainDAL objAgent = new MemberMainDAL();
            return objAgent.getPolicyDetails(memnumber,"SETL", sortfield);
        }
    }
}