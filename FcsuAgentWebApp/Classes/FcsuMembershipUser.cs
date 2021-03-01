using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FcsuAgent.Services.Contracts;
using CODE.Framework.Services.Client;
using CODE.Framework.Core.Utilities;

namespace FcsuAgentWebApp.Classes
{
    public class FcsuMembershipUser : MembershipUser
    {
       public FcsuMembershipUser()
        {
            _user = new UserInformation();
        }

        public FcsuMembershipUser(UserInformation user)
        {
            _user = user;
        }

        public FcsuMembershipUser(string username)
        {
            try
            {
                var response = new GetUserInformationResponse();
                var request = new GetUserInformationRequest {UserName = username};
                ServiceClient.Call<IFcsuServices>(x => response = x.GetUserInformation(request));
                if (response.Success) _user = response.User;
                if (User == null) _user = new UserInformation { UserPk = Guid.Empty };
            }
            catch (Exception ex)
            {
                LoggingMediator.Log(ex, LogEventType.Exception);
            }
        }

        public FcsuMembershipUser(Guid userPk)
        {
            //try
            //{
            //    var response = new GetUserInformationResponse();
            //    ServiceClient.Call<ISecurityService>(x => response = x.GetUserInformation(new GetUserInformationRequest {UserPk = userPk}));
            //    if (response.Success) _user = response.User;
            //}
            //catch (Exception ex)
            //{
            //    LoggingMediator.Log(ex, LogEventType.Exception);
            //}

            throw new NotImplementedException();
        }

        private UserInformation _user;

        /// <summary>
        ///   Gets the date and time when the user was added to the membership data store.
        /// </summary>
        /// <value> </value>
        /// <returns> The date and time when the user was added to the membership data store. </returns>
        public override DateTime CreationDate { get { return User.CreationDate; } }

        /// <summary>
        ///   Gets or sets the e-mail address for the membership user.
        /// </summary>
        /// <value> </value>
        /// <returns> The e-mail address for the membership user. </returns>
        public override string Email { get { return User.Email; } set { User.Email = value; } }

        /// <summary>
        ///   Gets or sets whether the membership user can be authenticated.
        /// </summary>
        /// <value> </value>
        /// <returns> true if the user can be authenticated; otherwise, false. </returns>
        public override bool IsApproved { get { return User.IsApproved; } set { User.IsApproved = value; } }

        /// <summary>
        ///   Gets or sets the date and time when the membership user was last authenticated or accessed the application.
        /// </summary>
        /// <value> </value>
        /// <returns> The date and time when the membership user was last authenticated or accessed the application. </returns>
        public override DateTime LastActivityDate { get { return User.LastAccessDate; } set { User.LastAccessDate = value; } }

        /// <summary>
        ///   Gets the most recent date and time that the membership user was locked out.
        /// </summary>
        /// <value> </value>
        /// <returns> A <see cref="T:System.DateTime"></see> object that represents the most recent date and time that the membership user was locked out. </returns>
        /// <remarks>
        ///   Not supported. This is always DateTime.MinValue
        /// </remarks>
        public override DateTime LastLockoutDate { get { return DateTime.MinValue; } }

        /// <summary>
        ///   Gets or sets the date and time when the user was last authenticated.
        /// </summary>
        /// <value> </value>
        /// <returns> The date and time when the user was last authenticated. </returns>
        public override DateTime LastLoginDate { get { return User.LastAccessDate; } set { User.LastAccessDate = value; } }

        /// <summary>
        ///   Gets the date and time when the membership user's password was last updated.
        /// </summary>
        /// <value> </value>
        /// <returns> The date and time when the membership user's password was last updated. </returns>
        /// <remarks>
        ///   Not supported. This is always DateTime.MinValue
        /// </remarks>
        public override DateTime LastPasswordChangedDate { get { return User.LastPasswordChangeDate; } }

        /// <summary>
        ///   Gets the logon name of the membership user.
        /// </summary>
        /// <value> </value>
        /// <returns> The logon name of the membership user. </returns>
        public override string UserName { get { return User.UserName; } }

        public Guid UserPk { get { return User.UserPk; } }

        public UserInformation User { get { return _user; } }

        public string FullName { get { return _user.FirstName + " " + _user.LastName; } }
        public string Language { get { return _user.Language; } }

        public string LastName { get { return _user.LastName; } }
        public string FirstName { get { return _user.FirstName; } }
        public string AgentNumber { get { return _user.AgentNumber; } }
        public string MemberNumber { get { return _user.MemberNumber; } }
        public string AgentMemberDescr { get { return _user.AgentMemberDescr; } } 
        public string Phone { get { return _user.Phone; } } 

        public void GetUserByEmail()
        {
            //try
            //{
            //    var response = new GetUserInformationResponse();
            //    ServiceClient.Call<ISecurityService>(x => response = x.GetUserInformation(new GetUserInformationRequest {Email = User.Email}));
            //    if (response.Success) _user = response.User;
            //}
            //catch (Exception ex)
            //{
            //    LoggingMediator.Log(ex, LogEventType.Exception);
            //}
        }


        public bool Save()
        {
            try
            {
                var response = new SaveUserResponse();
                ServiceClient.Call<IFcsuServices>(x => response = x.SaveUser(new SaveUserRequest {UserInfo = User}));
                if (response.Success) User.UserPk = response.User.UserPk;
                return response.Success;
            }
            catch (Exception ex)
            {
                LoggingMediator.Log(ex, LogEventType.Exception);
                return false;
            }
        }
//
    }
}