using FcsuAgentWebApp.DAL;
using FcsuAgentWebApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.BAL
{
    public class AgentMainBAL
    {

        public List<AgentPolicyModel> getPolicyDetails( string searchText, string number)
        {
            AgentMainDAL objAgent = new AgentMainDAL();
            return objAgent.getPolicyDetails(searchText, number);
        }

        public List<PolicyBeneficiaryModel> GetPolicyBeneficiaries(string policyNumber)
        {
            AgentMainDAL objAgent = new AgentMainDAL();
            return objAgent.GetPolicyBeneficiaries(policyNumber);
        }
        public List<PolicyRiderModel> GetPolicyRiders(string policyNumber)
        {
            AgentMainDAL objAgent = new AgentMainDAL();
            return objAgent.GetPolicyRiders(policyNumber);
        }
    }
}