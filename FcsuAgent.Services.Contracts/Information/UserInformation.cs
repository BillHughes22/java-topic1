using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    public class UserInformation
    {
        /// <summary>
        /// Gets or sets the User primary key.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Guid UserPk { get; set; }

        /// <summary>
        /// Gets or sets the logon user name.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string UserName { get; set; }


        /// <summary>
        /// Gets or sets the logon users agent number.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string AgentNumber { get; set; }

        /// <summary>
        /// Gets or sets the logon users member number.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string MemberNumber { get; set; }

        /// <summary>
        /// Gets or sets the user email address.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's middle initial.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string MiddleInitial { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's title.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the user's first address line.
        /// </summary>
        //[DataMember( IsRequired = true )]
        //public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the user's second address line.
        /// </summary>
        //[DataMember( IsRequired = true )]
        //public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the user's city.
        /// </summary>
        //[DataMember( IsRequired = true )]
        //public string City { get; set; }

        /// <summary>
        /// Gets or sets the user's country.
        /// </summary>
        //[DataMember( IsRequired = true )]
        //public string Country { get; set; }

        /// <summary>
        /// Gets or sets the user's zip code.
        /// </summary>
        //[DataMember( IsRequired = true )]
        //public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the user's telephone number.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Telephone { get; set; }

        /// <summary>
        /// Gets or sets the user's fax number.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the user's mobile number.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string CellPhone { get; set; }

        /// <summary>
        /// Gets or sets the user's pager or other phone number.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OtherPhone { get; set; }

        /// <summary>
        /// Gets or sets the user's contact address.
        /// </summary>
        [DataMember(IsRequired = true)]
        public AddressInformation ContactAddress { get; set; }

        /// <summary>
        /// Gets or sets the user's ship to address.
        /// </summary>
        [DataMember(IsRequired = true)]
        public AddressInformation ShipToAddress { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not this user has been disabled.
        /// </summary>
        [DataMember(IsRequired = true)]
        public bool IsDisabled { get; set; }

        ///<summary>Leave null unless setting a new password. The user's password will be set to this.</summary>
        [DataMember(IsRequired = true)]
        public string SetPassword { get; set; }

        /// <summary>
        /// Gets or sets the creation date for this user's record.
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not this user has been approved.
        /// </summary>
        [DataMember(IsRequired = true)]
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the date on which the user last logged in.
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the date on which the user last accessed the application.
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime LastAccessDate { get; set; }

        /// <summary>
        /// Gets or sets the date on which the user last changed his password.
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime LastPasswordChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the roles assigned to this user.
        /// </summary>
        //[DataMember(IsRequired = false)]
        //public List<RoleInformation> Roles { get; set; }

        /// <summary>
        /// Gets or sets the user's equipment type certifications.
        /// </summary>
        /// <value>
        /// The equipment type certifications.
        /// </value>
        //[DataMember(IsRequired = true)]
        //public List<CertificationInformation> Certifications { get; set; }

        /// <summary>
        /// Gets or sets the language in which this user operates the application.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Language { get; set; }
        /// <summary>
        /// Gets or sets the logon users agent number.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string AgentMemberDescr { get; set; } 

        /// Gets or sets the logon users phone number.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Phone { get; set; }

        

        /// <summary>
        /// Gets or sets the user's culture.
        /// </summary>
        //[DataMember( IsRequired = true )]
        //public string Culture { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInformation"/> class.
        /// </summary>
        public UserInformation()
        {
            UserPk = Guid.NewGuid();
            UserName = string.Empty;
            IsDisabled = false;
            Email = string.Empty;
            FirstName = string.Empty;
            MiddleInitial = string.Empty;
            LastName = string.Empty;
            Title = string.Empty;
            //Address1 = string.Empty;
            //Address2 = string.Empty;
            //City = string.Empty;
            //Country = string.Empty;
            //ZipCode = string.Empty;
            Telephone = string.Empty;
            Fax = string.Empty;
            CellPhone = string.Empty;
            OtherPhone = string.Empty;
            ContactAddress = null;
            ShipToAddress = null;
            Comments = string.Empty;
            SetPassword = null;
            Language = string.Empty;
            //Culture = string.Empty;
            IsApproved = false;
           // Certifications = new List<CertificationInformation>();
            AgentNumber = string.Empty;
            MemberNumber = string.Empty;
            AgentMemberDescr = string.Empty; 
             Phone = string.Empty;
            
        }

    }
}
