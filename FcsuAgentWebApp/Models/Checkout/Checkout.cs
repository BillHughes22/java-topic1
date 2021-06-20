using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.Models.Checkout
{
    /// <summary>
    /// Model for checkout info to be stored in db
    /// </summary>
    public class Checkout
    {
        // Define the items that are captured for the cart
        public decimal payment { get; set; }
        public string userName { get; set; }
        public int memberNumber { get; set; }
        public string policyNumber { get; set; }
        public string policyDesc { get; set; }
    }

    /// <summary>
    /// Result of db insert
    /// </summary>
    public class ReturnData
    {
        public bool isSuccessful { get; set; }
        public int newId { get; set; }
    }

    /// <summary>
    /// Define the items that are shown in the grid
    /// </summary>
    public class CheckoutGrid
    {
        public decimal amountPaid { get; set; }
        public string policy { get; set; }
        public string polDescr { get; set; }
        public int order_id { get; set; }
        public int payhist_i { get; set; }
    }

    /// <summary>
    /// Get the User Information
    /// </summary>
    public class UserInformation
    {
        public string userName { get; set; }
        public string userAddress { get; set; }
    }
}