using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.Models.DataModels
{
    public class AgentPolicyModel
    {
 
        public string Policy { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PlanType { get; set; }
        public decimal Value { get; set; }
        public decimal BegBal { get; set; }
        public decimal Ytd_int  { get; set; }
        public decimal AnnRate { get; set; }
        public decimal Curbal { get; set; }

        public DateTime? PolDate { get; set; }

        public string Status { get; set; }
        public string Phone  { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }

        public DateTime? MemberDt { get; set; }
        public string LastName { get; set; }
        public string Mode { get; set; }
        public decimal baseprem { get; set; }

        public DateTime? Updatedt { get; set; }

        public DateTime? MatDate { get; set; }

        public string Pl_spia { get; set; }

        //membermain
        public decimal? lien { get; set; }
        public decimal loan { get; set; }
        public decimal cashval { get; set; }
        public decimal puadiv { get; set; }
        public decimal accumdiv { get; set; }
        public string oname { get; set; }
        public DateTime? paidto { get; set; }
        public decimal rmdcurr { get; set; }
        public decimal rmdprev { get; set; }
        public decimal totdeath { get; set; }
        public string ira { get; set; }
        public decimal dueamt { get; set; }

    }
}