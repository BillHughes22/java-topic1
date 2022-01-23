using FcsuAgentWebApp.Models.CheckoutPayPal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.Services.DataAccess
{
    public class DataLayer
    {
        public string connectionString { get; set; }

        /// <summary>
        /// Constructor that will manage all code when this class is instantiated
        /// </summary>
        public DataLayer()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        }

        /// <summary>
        /// Save the payment items to the database and return the last inserted id and if successful
        /// </summary>
        /// <param name="checkoutItems"></param>
        public ReturnData SaveCheckoutItem(CheckoutPayPal checkoutItems)
        {
            ReturnData insertResults = new ReturnData();
            insertResults.isSuccessful = true;
            insertResults.newId = 0;

            string usersSelectCommand = string.Format(@"INSERT INTO payhist (policy, polDescr, datetimePaid, amountPaid, userName, memberNumber, order_id, payyear, isAnnuity)
                                                        OUTPUT INSERTED.payhist_i
                                                        VALUES (@policy, @polDescr, @datetimePaid, @amountPaid, @userName, @memberNumber, @order_id, @payyear, @isAnnuity)");
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(usersSelectCommand, conn))
                    {
                        cmd.Parameters.AddWithValue("@policy", checkoutItems.policyNumber);
                        cmd.Parameters.AddWithValue("@polDescr", checkoutItems.policyDesc);
                        cmd.Parameters.AddWithValue("@datetimePaid", DateTime.Now);
                        cmd.Parameters.AddWithValue("@amountPaid", checkoutItems.payment);
                        cmd.Parameters.AddWithValue("@userName", checkoutItems.userName);
                        cmd.Parameters.AddWithValue("@memberNumber", checkoutItems.memberNumber);
                        cmd.Parameters.AddWithValue("@payyear", checkoutItems.payYear);
                        cmd.Parameters.AddWithValue("@isAnnuity", checkoutItems.isAnnuity);
                        // Test to see if this is the first order or if we already have an order id
                        if (System.Web.HttpContext.Current.Session["orderID"] != null)
                            cmd.Parameters.AddWithValue("@order_id", Convert.ToInt32(System.Web.HttpContext.Current.Session["orderID"]));
                        else
                            cmd.Parameters.AddWithValue("@order_id", 0);

                        conn.Open();
                        // ExecuteNonQuery used for executing queries that do not return any data. 
                        //cmd.ExecuteNonQuery();
                        //insertResults.newId = (Int32)cmd.ExecuteScalar();

                        // Get the last inserted ID so we can pass it back up.
                        // We always want to return the orderID if this is the second order
                        if (System.Web.HttpContext.Current.Session["orderID"] != null)
                        {
                            insertResults.newId = (Int32)System.Web.HttpContext.Current.Session["orderID"];
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            insertResults.newId = (Int32)cmd.ExecuteScalar();
                        }

                    }
                    conn.Close();
                    // We need to insert the new id into the order_id field so we can track all orders if there are more than one per order.
                    string updateCommand = string.Format(@"UPDATE payhist SET order_id = @order_id WHERE payhist_i = '" + insertResults.newId + "'");
                    using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                    {
                        cmd.Parameters.AddWithValue("@order_id", insertResults.newId);

                        conn.Open();
                        // ExecuteNonQuery used for executing queries that do not return any data. 
                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();

                }

            }
            catch (Exception)
            {
                // If there is an error set the flag to false
                insertResults.isSuccessful = false;
            }

            return insertResults;
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        // End Save payment
        //-------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get all the items in the cart and return them
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public IEnumerable<CheckoutGrid> getCheckoutItems(int orderID)
        {
            // Create a new transport list
            List<CheckoutGrid> cartResults = new List<CheckoutGrid>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Define the query
                string query = string.Format(@"SELECT payhist_i, order_id, policy, polDescr, amountPaid, isAnnuity FROM [payhist] WHERE order_id = '{0}' ORDER BY payhist_i", orderID);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Instantiate our Checkout Grid Class creating an object
                            // called checkout
                            CheckoutGrid checkout = new CheckoutGrid();
                            // Populate this object with a verse that was found
                            checkout.payhist_i = Convert.ToInt32(reader["payhist_i"]);
                            checkout.order_id = Convert.ToInt32(reader["order_id"]);
                            checkout.policy = reader["policy"].ToString();
                            checkout.polDescr = reader["polDescr"].ToString();
                            checkout.amountPaid = Convert.ToDecimal(reader["amountPaid"]);
                            checkout.isAnnuity = Convert.ToBoolean(reader["isAnnuity"]);

                            // Add each payment so we can pass back all the rows
                            cartResults.Add(checkout);
                        }
                    }
                }
            }
            return cartResults;
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        // End Getting Cart Contents
        //-------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete an item in the cart
        /// </summary>
        /// <param name="payhist_i"></param>
        /// <returns></returns>
        public bool DeleteCheckoutItem(int payhist_i)
        {
            bool isSuccessful = true;
            
            string usersDeleteCommand = string.Format(@"DELETE FROM [payhist] WHERE payhist_i = '{0}'", payhist_i);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(usersDeleteCommand, conn))
                    {
                        conn.Open();
                        // ExecuteNonQuery used for executing queries that do not return any data. 
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }

            }
            catch (Exception)
            {
                // If there is an error set the flag to false
                isSuccessful = false;
            }

            return isSuccessful;
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        // End Delete payment
        //-------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update the sales cart items to include the PayPal Transaction ID and return if successful
        /// </summary>
        /// <param name="checkoutItems"></param>
        public bool UpdateTransID(int order_id, string paypalTransID)
        {
            bool isSuccessful = true;
            
           
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // We need to update the paypal transaction id
                    string updateCommand = string.Format(@"UPDATE payhist SET trans_id = @paypal_id WHERE order_id = '" + order_id + "'");
                    using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                    {
                        cmd.Parameters.AddWithValue("@paypal_id", paypalTransID);

                        conn.Open();
                        // ExecuteNonQuery used for executing queries that do not return any data. 
                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();

                }

            }
            catch (Exception)
            {
                // If there is an error set the flag to false
                isSuccessful = false;
            }

            return isSuccessful;
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        // End Update PayPal Transaction ID
        //-------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update the sales cart items to include the KeyBank Transaction ID, paymentType and return if successful
        /// </summary>
        /// <param name="checkoutItems"></param>
        public bool UpdateKeyBankTransID(int order_id, string keybankTransID)
        {
            bool isSuccessful = true;


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // We need to update the KeyBank transaction id and paymentType
                    string updateCommand = string.Format(@"UPDATE payhist SET trans_id = @keybank_id, paymentType = 'KeyBank' WHERE order_id = '" + order_id + "'");
                    using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                    {
                        cmd.Parameters.AddWithValue("@keybank_id", keybankTransID);

                        conn.Open();
                        // ExecuteNonQuery used for executing queries that do not return any data. 
                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();

                }

            }
            catch (Exception)
            {
                // If there is an error set the flag to false
                isSuccessful = false;
            }

            return isSuccessful;
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        // End Update PayPal Transaction ID
        //-------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get userName and memberNumber
        /// </summary>
        /// <returns></returns>
        public Tuple<string, string> getUserNameMemberNumber(int orderID)
        {
            string userName = "";
            string memberNumber = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Define the query
                string query = string.Format(@"SELECT userName, memberNumber FROM [payhist] WHERE order_id = '{0}'", orderID);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userName = reader["userName"].ToString();
                            memberNumber = reader["memberNumber"].ToString();
                        }
                    }
                }
            }
            return Tuple.Create(userName, memberNumber);
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        // End Getting Cart Contents
        //-------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update the surcharge
        /// </summary>
        public bool UpdateSurcharge(int payhist_i, decimal surchageUpdate)
        {
            bool isSuccessful = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // We need to update the paypal transaction id
                    string updateCommand = string.Format(@"UPDATE payhist SET amountPaid = @surchgeUpdate WHERE payhist_i = '" + payhist_i + "'");
                    using (SqlCommand cmd = new SqlCommand(updateCommand, conn))
                    {
                        cmd.Parameters.AddWithValue("@surchgeUpdate", surchageUpdate);

                        conn.Open();
                        // ExecuteNonQuery used for executing queries that do not return any data. 
                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();

                }

            }
            catch (Exception)
            {
                // If there is an error set the flag to false
                isSuccessful = false;
            }

            return isSuccessful;
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        // End Update PayPal Transaction ID
        //-------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get User Information
        /// </summary>
        /// <returns></returns>
        public UserInformation GetUserInformation(decimal custNum)
        {
            

            // Create a new transport list
            UserInformation userResults = new UserInformation();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Define the query
                string query = string.Format(@"SELECT NAME, ADDRESS FROM [member] WHERE CST_NUM = '{0}'", custNum);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userResults.userName = reader["NAME"].ToString();
                            userResults.userAddress = reader["ADDRESS"].ToString();
                        }
                    }
                }
            }
            return userResults;
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        // End Getting User Information
        //-------------------------------------------------------------------------------------------------------------------------------


    }
}