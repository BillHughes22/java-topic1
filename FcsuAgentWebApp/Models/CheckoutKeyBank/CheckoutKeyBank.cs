using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.Models.CheckoutKeyBank
{
    /// <summary>
    /// Model for checkout info to be stored in db for KeyBank
    /// </summary>
    public class CheckoutKeyBank
    {
        // Define the items that are captured for the cart
        public decimal payment { get; set; }
        public string userName { get; set; }
        public int memberNumber { get; set; }
        public string policyNumber { get; set; }
        public string policyDesc { get; set; }
        public int payYear { get; set; }
        public bool isAnnuity { get; set; }
    }
}