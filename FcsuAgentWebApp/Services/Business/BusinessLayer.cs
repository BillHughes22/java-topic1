using FcsuAgentWebApp.Models.CheckoutPayPal;
using FcsuAgentWebApp.Services.DataAccess;
using FcsuAgentWebApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.Services.Business
{
    public class BusinessLayer
    {
        bool surchargeChanged = false;
        bool isSurcharge = false;
        /// <summary>
        /// Pass through the checkout items to save
        /// </summary>
        /// <param name="checkOutItems"></param>
        /// <returns></returns>
        public ReturnData SaveCheckoutItems(CheckoutPayPal checkOutItems)
        {
            DataLayer SaveItems = new DataLayer();
            return SaveItems.SaveCheckoutItem(checkOutItems);
        }

        /// <summary>
        /// Get User Information
        /// </summary>
        /// <param name="custNum"></param>
        /// <returns></returns>
        public UserInformation GetUserInfo(decimal custNum)
        {
            DataLayer GetUserData = new DataLayer();
            return GetUserData.GetUserInformation(custNum);
        }

        /// <summary>
        /// Get all the cart items from data layer
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Tuple<IEnumerable<CheckoutGrid>, decimal> GetAllCheckoutItems(int orderID)
        {
            // First we need to instantiate the data access layer
            DataLayer GetCartItems = new DataLayer();
            IEnumerable<CheckoutGrid> allItems = GetCartItems.getCheckoutItems(orderID);
            // Get the order total
            decimal orderTotal = CalculateTotal(allItems);
            // Make sure a surcharge was not added or removed
            if (surchargeChanged)
            {
                // Get all the items again
                allItems = GetCartItems.getCheckoutItems(orderID);
                // Get the order total again
                orderTotal = CalculateTotal(allItems);
                // Reset the flag
                surchargeChanged = false;
            }
            return Tuple.Create(allItems, orderTotal);
        }

        /// <summary>
        /// Returns everying needed for PayPal payment page.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Tuple<IEnumerable<CheckoutGrid>, string, decimal> GetItemsJsonTotal(int orderID)
        {
            // First we need to instantiate the data access layer
            DataLayer GetCartItems = new DataLayer();
            IEnumerable<CheckoutGrid> allItems = GetCartItems.getCheckoutItems(orderID);
            // Create the JSON format for the products that are being paid for
            string cartItems = PrepareJson(allItems);
            decimal orderTotal = CalculateTotal(allItems);
            // Make sure a surcharge was not added or removed
            if (surchargeChanged)
            {
                // Get all the items again
                allItems = GetCartItems.getCheckoutItems(orderID);
                // Create the JSON format for the products that are being paid for
                cartItems = PrepareJson(allItems);
                // Reset the flag
                surchargeChanged = false;
            }
            return Tuple.Create(allItems, cartItems, orderTotal);
        }

        /// <summary>
        /// Pass through the item to delete
        /// </summary>
        /// <param name="payhist_i"></param>
        /// <returns></returns>
        public bool DeleteCheckoutItem(int payhist_i)
        {
            DataLayer DeleteItem = new DataLayer();
            return DeleteItem.DeleteCheckoutItem(payhist_i);
        }

        /// <summary>
        /// Update PayPal Transaction ID in the db
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="paypalTransID"></param>
        /// <returns></returns>
        public bool UpdatePayPalTransID(int order_id, string paypalTransID)
        {
            DataLayer UpdatePayPal = new DataLayer();
            return UpdatePayPal.UpdateTransID(order_id, paypalTransID);
        }

        /// <summary>
        /// Prepare JSON structure for products being purchased
        /// </summary>
        /// <param name="allItems"></param>
        public string PrepareJson(IEnumerable<CheckoutGrid> allItems)
        {
            // Initialize the string for the JSON structure
            string cartContents = "";
            // Iterate throught the collection
            // There must be a min of one item to iterate through
            if (allItems.Count() > 0)
            {
                // The purpose is to build the JSON format that needs to be sent to PayPal
                int itemCounter = 0;

                foreach (var item in allItems)
                {
                    itemCounter++;
                    // Start this one item
                    cartContents = cartContents + "{";
                    // Policy Description
                    cartContents = cartContents + "name: '" + item.polDescr + "', description: '" + item.polDescr + "', ";
                    // Policy Number
                    cartContents = cartContents + "sku: '" + item.policy + "', ";
                    // Amount Paid on this policy
                    cartContents = cartContents + "unit_amount: { currency_code: 'USD', value: '" + item.amountPaid + "'}, tax: { currency_code: 'USD', value: '0.00' }, ";
                    // Qty needs to be set to one
                    cartContents = cartContents + "quantity: '1' ";
                    // Close out this one item
                    cartContents = cartContents + "}";
                    // We need to determine if there are more items to add.
                    if (allItems.Count() > itemCounter)
                        cartContents = cartContents + ",";

                }
            }
            return cartContents;
        }
        
        /// <summary>
        /// Returns the total amount
        /// </summary>
        /// <param name="allItems"></param>
        /// <returns></returns>
        public decimal CalculateTotal(IEnumerable<CheckoutGrid> allItems)
        {
            int idItem = 0;
            // Initialize the variables
            decimal order_total = 0, order_totalAnnIns = 0, total_price = 0;
            decimal surchageAmount = 0;
            int order_id = 0;
            // Iterate throught the collection
            // There must be a min of one item to iterate through
            if (allItems.Count() > 0)
            {
                foreach (var item in allItems)
                {
                    // See if a surcharge has already been applied
                    if (item.polDescr.ToString().Equals("Surcharge"))
                    {
                        isSurcharge = true;
                        surchargeChanged = false;
                        idItem = Convert.ToInt32(item.payhist_i);
                        surchageAmount = Convert.ToDecimal(item.amountPaid.ToString());
                    }


                    // Ok if rows were found we need to get the values

                    //Now lets take care of the total price
                    order_total = item.isAnnuity ? order_total + Convert.ToDecimal(item.amountPaid.ToString()) : order_total;
                    order_totalAnnIns = order_totalAnnIns + Convert.ToDecimal(item.amountPaid.ToString());
                    // Store the order_id incase we need it
                    order_id = item.order_id;
                }
                // Now let's see if we need to add a surcharge
                if (!isSurcharge && order_total > 500)
                {
                    surchargeChanged = true;
                    // Remove Surcharge row to order.
                    DataLayer SaveItems = new DataLayer();
                    CheckoutPayPal addCheckoutItem = new CheckoutPayPal();
                    addCheckoutItem.payment = decimal.Multiply(order_total, (decimal).02);
                    addCheckoutItem.policyDesc = "Surcharge";
                    addCheckoutItem.policyNumber = "";
                    // Get the userName
                    var cartOwnerInfo = SaveItems.getUserNameMemberNumber(order_id);

                    addCheckoutItem.userName = cartOwnerInfo.Item1.ToString();
                    // Get the currentUser
                    addCheckoutItem.memberNumber = Convert.ToInt32(cartOwnerInfo.Item2.ToString());
                    // Add the payyear
                    addCheckoutItem.payYear = 0;
                    // Add the isannuity
                    addCheckoutItem.isAnnuity = true;
                    // Add the Surcharge to the database.
                    SaveItems.SaveCheckoutItem(addCheckoutItem);

                    // Make sure to update the total amount
                    order_total = decimal.Multiply(order_total, (decimal)1.02);
                    order_totalAnnIns = decimal.Multiply(order_totalAnnIns, (decimal)1.02);
                    isSurcharge = true;
                }



                // If the Surchage is already present but the user adds more to their shopping cart
                // we need to modify the surcharge amount.
                if (isSurcharge && order_total != (surchageAmount / (decimal).02 + surchageAmount))
                {
                    // If we fall into here we need to update the surchage amount
                    DataLayer UpdateSurch = new DataLayer();
                    UpdateSurch.UpdateSurcharge(idItem, ((order_total - surchageAmount) * (decimal).02));
                    surchargeChanged = true;
                }

                // Now test to see if there is a surcharge but dollar amount has fallen below $501
                if (isSurcharge && ((order_total - surchageAmount) < 501))
                {
                    surchargeChanged = true;
                    // Add Surcharge row to order.
                    DataLayer DeleteItem = new DataLayer();
                    DeleteItem.DeleteCheckoutItem(idItem);

                    // Update the flag
                    isSurcharge = false;
                }
            }

                return order_totalAnnIns;
        }
    }
}