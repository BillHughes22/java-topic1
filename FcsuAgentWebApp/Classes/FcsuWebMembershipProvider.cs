using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FcsuAgent.Services;
using FcsuAgent.Services.Contracts;
using CODE.Framework.Services.Client;
using FcsuAgentWebApp.Models;

namespace FcsuAgentWebApp.Classes
{
    public class FcsuWebMembershipProvider : MembershipProvider
    {
        public override string ApplicationName
        {
            get
            {
                return "FCSU Agent Portal";
               
            }
            set
            {
                /* No Action */
            }
        }

        public override bool EnablePasswordReset { get { return true; } }
        
        public override bool EnablePasswordRetrieval { get { return true; } }

        public override int MaxInvalidPasswordAttempts { get { return 10; } }

        public override int MinRequiredNonAlphanumericCharacters { get { return 0; } }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <value></value>
        /// <returns>The minimum length required for a password. </returns>
        public override int MinRequiredPasswordLength { get { return 5; } }

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are 
        /// allowed before the membership user is locked out.
        /// </summary>
        /// <value></value>
        /// <returns>The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.</returns>
        public override int PasswordAttemptWindow { get { return 10; } }

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <value></value>
        /// <returns>One of the <see cref="T:System.Web.Security.MembershipPasswordFormat"/> values indicating the format for storing passwords in the data store.</returns>
        public override MembershipPasswordFormat PasswordFormat { get { return MembershipPasswordFormat.Encrypted; } }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <value></value>
        /// <returns>A regular expression used to evaluate a password.</returns>
        public override string PasswordStrengthRegularExpression { get { return string.Empty; } }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        /// <value></value>
        /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.</returns>
        public override bool RequiresQuestionAndAnswer { get { return false; } }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <value></value>
        /// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.</returns>
        public override bool RequiresUniqueEmail { get { return false; } }


        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            
             try 
                { 
                    var info = new UserInformation() 
                        { 
                            UserName = username, 
                            SetPassword = password, 
                            Email = email, 
                            IsApproved = isApproved 
                        }; 
                

                    var saveRequest = new SaveUserRequest() {UserInfo = info}; 
                    var saveResponse = new SaveUserResponse();
                //Purnima
                var getRequest = new GetUserInformationRequest() { UserName = info.UserName };
                var getResponse = new GetUserInformationResponse();
                ServiceClient.Call<IFcsuServices>(x => getResponse = x.GetUserInformation(getRequest));
                if (getResponse.Success)
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                    return null;
                }
                //purnima
                ServiceClient.Call<IFcsuServices>(x=>saveResponse= x.SaveUser(saveRequest)); 
                    if (!saveResponse.Success) 
                    { 
                        status = MembershipCreateStatus.ProviderError; 
                        return null; 
                    } 
                    status = MembershipCreateStatus.Success; 
                    return new FcsuMembershipUser(saveResponse.User); 
                
                }
             catch (Exception)
             {

                 throw;
             }

          
            
          //  return null;
            throw new NotImplementedException();
        }

        /// <summary>
        /// User and Password exist in the data source
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>true if valid, else false.</returns>
        public override bool ValidateUser(string username, string password)
        {
            var request = new IsUserValidRequest() { UserName = username, Password = password };
            var response = new IsUserValidResponse();
            ServiceClient.Call<IFcsuServices>(x => response = x.IsUserValid(request));
            return response.Success;

        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            //var FooUser = user as FcsuMembershipUser;
            //if (FooUser != null) FooUser.Save();
            throw new NotImplementedException();
        }
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool result = false;
            var request = new IsUserValidRequest() { UserName = username, Password = oldPassword };
            var response = new IsUserValidResponse();
            ServiceClient.Call<IFcsuServices>(x => response = x.IsUserValid(request));

            if (response.Success)
            {
                var view = new UserEditViewModel(response.UserId);

                if (!string.IsNullOrEmpty(newPassword)) view.SetPassword = newPassword;
                result = view.SaveUser(view);
            }
            return result;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return new FcsuMembershipUser(username);
            //throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            //var user = new FooMembershipUser() { Email = email };
            //user.GetUserByEmail();
            //return user.UserName;
            throw new NotImplementedException();
        }

    }
}