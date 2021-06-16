using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.Models.DataModels
{
    public class PolicyRiderModel
    {
       
        public string Policy { get; set; }
        public decimal RiderPrem1 { get; set; }
        public decimal RiderPrem2 { get; set; }
        public decimal RiderPrem3 { get; set; }
        public string RiderType1 { get; set; }
        public string RiderType2 { get; set; }
        public string RiderType3 { get; set; }
    }
}