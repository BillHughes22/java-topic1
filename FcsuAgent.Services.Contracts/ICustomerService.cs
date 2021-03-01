using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace FcsuAgent.Services.Contracts
{
    // TODO: This is a simple example service contract for customers. 

    /// <summary>Example customer service</summary>
    /// <remarks>Contracts should always be well documented as their documentation also bubbles through for service implementations</remarks>
    [ServiceContract]
    public interface ICustomerService
    {
        /// <summary>Retrieves a list of customers</summary>
        /// <param name="request">Request</param>
        /// <returns>Response</returns>
        [OperationContract]
        GetCustomerListResponse GetCustomerList(GetCustomerListRequest request);

        /// <summary>Retrieves a specific customer</summary>
        /// <param name="request">Request</param>
        /// <returns>Response</returns>        
        [OperationContract]
        GetCustomerResponse GetCustomer(GetCustomerRequest request);
    }


    // Note: The data contracts below are shown in this file for simplicity. Feel free to break them our into separate files.

    /// <summary>Request contract for GetCustomerList() operation</summary>
    /// <remarks>Always create a request object, even if this object has no parameters. This allows for future changes in a structured fashion.</remarks>
    [DataContract]
    public class GetCustomerListRequest
    {
        /// <summary>Constructor</summary>
        /// <remarks>Explicitly set default values for all properties in the constructor</remarks>
        public GetCustomerListRequest()
        {
        }
    }

    ///<summary>Response contract for GetCustomerList() operation</summary>
    [DataContract]
    public class GetCustomerListResponse
    {
        ///<summary>Constructor</summary>
        /// <remarks>Explicitly set default values for all properties in the constructor</remarks>
        public GetCustomerListResponse()
        {
            Success = false;
            FailureInformation = string.Empty;

            Customers = new List<CustomerQuickInformation>();
        }

        /// <summary>Indicates whether the call succeeded without errors or problems.</summary>
        /// <remarks>'Success' is a standard member for CODE Framework contracts. It is not technically required but we recommend supporting it.</remarks>
        [DataMember(IsRequired = true)]
        public bool Success { get; set; }

        /// <summary>If Success is false, FailureInformation contains a brief indicator of what went wrong.</summary>
        /// <remarks>This should NOT contain an exception message for security reasons. 'FailureInformation' is a standard member for CODE Framework contracts. It is not technically required but we recommend supporting it.</remarks>
        [DataMember(IsRequired = true)]
        public string FailureInformation { get; set; }

        ///<summary>Actual list of customers returned by GetCustomerList()</summary>
        [DataMember(IsRequired = true)]
        public List<CustomerQuickInformation> Customers { get; set; }
    }

    /// <summary>Request contract for GetCustomer() operation</summary>    
    [DataContract]
    public class GetCustomerRequest
    {
        /// <summary>Constructor</summary>
        /// <remarks>Explicitly set default values for all properties in the constructor</remarks>
        public GetCustomerRequest()
        {
            CustomerId = Guid.Empty;
        }

        ///<summary>ID of the customer that is to be returned</summary>
        [DataMember(IsRequired = true)]
        public Guid CustomerId { get; set; }
    }

    /// <summary>Response contract for GetCustomer() operation</summary>
    [DataContract]
    public class GetCustomerResponse
    {
        /// <summary>Constructor</summary>
        /// <remarks>Explicitly set default values for all properties in the constructor</remarks>
        public GetCustomerResponse()
        {
            Success = false;
            FailureInformation = string.Empty;

            Customer = new CustomerInformation();
        }

        /// <summary>Indicates whether the call succeeded without errors or problems.</summary>
        /// <remarks>'Success' is a standard member for CODE Framework contracts. It is not technically required but we recommend supporting it.</remarks>
        [DataMember(IsRequired = true)]
        public bool Success { get; set; }

        /// <summary>If Success is false, FailureInformation contains a brief indicator of what went wrong.</summary>
        /// <remarks>This should NOT contain an exception message for security reasons. 'FailureInformation' is a standard member for CODE Framework contracts. It is not technically required but we recommend supporting it.</remarks>
        [DataMember(IsRequired = true)]
        public string FailureInformation { get; set; }

        ///<summary>This is the payload this sample call will return.</summary>
        [DataMember(IsRequired = true)]
        public CustomerInformation Customer { get; set; }
    }

    /// <summary>Detailed information for a single customer.</summary>
    [DataContract]
    public class CustomerInformation
    {
        /// <summary>Constructor</summary>
        /// <remarks>Explicitly set default values for all properties in the constructor</remarks>
        public CustomerInformation()
        {
            Id = Guid.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            Phone = string.Empty;
            CreditLimit = 1000m;
            CustomerSince = DateTime.Today;
        }

        ///<summary>Id/Primary key that uniquely identifies the customer</summary>
        [DataMember(IsRequired = true)]
        public Guid Id { get; set; }

        ///<summary>The customer's first name.</summary>
        [DataMember(IsRequired = true)]
        public string FirstName { get; set; }

        ///<summary>The customer's last name.</summary>
        [DataMember(IsRequired = true)]
        public string LastName { get; set; }

        ///<summary>City, State for the customer's main offices.</summary>
        [DataMember(IsRequired = true)]
        public string Address { get; set; }

        ///<summary>Main phone line (switchboard) for the customer.</summary>
        [DataMember(IsRequired = true)]
        public string Phone { get; set; }

        ///<summary>Customer's total credit limit for all departments.</summary>
        [DataMember(IsRequired = true)]
        public decimal CreditLimit { get; set; }

        ///<summary>Date of customer's first signed contract with us.</summary>
        [DataMember(IsRequired = true)]
        public DateTime CustomerSince { get; set; }
    }

    /// <summary>Shortened customer information suitable for use in lists</summary>    
    /// <remarks>For detailed customer information, use the CustomerInformation contract. However, for large lists (with potentially large amounts of data), use this contract instead.</remarks>
    [DataContract]
    public class CustomerQuickInformation
    {
        /// <summary>Constructor</summary>
        /// <remarks>Explicitly set default values for all properties in the constructor</remarks>
        public CustomerQuickInformation()
        {
            Id = Guid.Empty;
            FullName = string.Empty;
        }

        /// <summary>Unique customer ID</summary>
        [DataMember(IsRequired = true)]
        public Guid Id { get; set; }

        ///<summary>The customer's full name.</summary>
        [DataMember(IsRequired = true)]
        public string FullName { get; set; }
    }
}
