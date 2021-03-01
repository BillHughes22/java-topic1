using System;
using System.Linq;
using System.Security.Cryptography;
using CODE.Framework.Core.Utilities;
using FcsuAgent.Services.Contracts;
using FcsuAgent.BuisnessObjects;

namespace FcsuAgent.Services.Implementation
{
    public class FcsuServices : IFcsuServices
    {
        public static byte[] PasswordKey = new byte[] { 48, 110, 143, 130, 105, 128, 147, 129, 163, 130, 122, 11, 43, 18, 181, 22, 72, 42, 124, 164, 243, 152, 19, 200 };

        public GetRolesResponse GetRoles(GetRolesRequest request)
        {
            try
            {
                var response = new GetRolesResponse();

                using (var context = new FcsuAgentEntities())
                {
                    var query = context.Roles.Select
                        (x => new RoleInformation
                        {
                            RolePk = x.Id,
                            RoleName = x.Name,
                            Description = x.Description,
                            IsDisabled = x.IsDisabled
                        });
                    if (query != null)
                    {
                        var listOfRoles = query.OrderBy(r => r.RoleName).ToList<RoleInformation>();
                        response.Roles = listOfRoles;
                    }
                    else
                    { //todo:come back and handle else case
                    }
                }

                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                LoggingMediator.Log(ex.Message);
                return new GetRolesResponse { Success = false, FailureMessage = "Unable to retrieve roles." };
            }
        }

        public GetRoleResponse GetRole(GetRoleRequest request)
        {
            try
            {
                var response = new GetRoleResponse();

                using (var context = new FcsuAgentEntities())
                {
                    var role = (from r in context.Roles
                                where r.Id == request.RolePk
                                select new RoleInformation
                                    {
                                        RolePk = r.Id,
                                        RoleName = r.Name,
                                        Description = r.Description,
                                        IsDisabled = r.IsDisabled
                                    }).FirstOrDefault();
                    if (role != null)
                    {
                        response.RoleInfo = role;
                        response.Success = true;
                    }
                    else
                    {
                        response.Success = false;
                        response.FailureInformation = "Unable to locate role";
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                LoggingMediator.Log(ex.Message);
                return new GetRoleResponse { Success = false, FailureInformation = "Unable to retrieve role." };
            }
        }

        public GetRoleAndUsersResponse GetRoleAndUsers(GetRoleRequest request)
        {
            try
            {
                var response = new GetRoleAndUsersResponse();
                using (var context = new FcsuAgentEntities())
                {
                    var roleResponse = GetRole(request);
                    if (roleResponse.Success)
                    {
                        response.RoleInfo = roleResponse.RoleInfo;
                        //get a list of all the users
                        response.UsersRoleList = context.Users.OrderBy(u => u.UserName)
                                                        .Select(u => new UserRoleListInformation
                                                            {
                                                                UserPk = u.Id,
                                                                UserName = u.UserName,
                                                                UserIsInRole=false
                                                            }).ToList();
                        //see if they are in the current role
                        var usersInRoleList = context.UserAndRoleXLinks.Where(x => x.RoleId == request.RolePk);
                        foreach (var userAndRoleXLink in usersInRoleList)
                        {
                            var user = response.UsersRoleList.FirstOrDefault(u => u.UserPk == userAndRoleXLink.UserId);
                            if (user != null) user.UserIsInRole = true;
                        }
                        response.Success = true;
                    }
                    else
                    {
                        response.Success = false;
                        response.FailureInformation = roleResponse.FailureInformation;
                    }
                }
                
                return response;
            }
            catch (Exception ex)
            {
                LoggingMediator.Log(ex.Message);
                return new GetRoleAndUsersResponse { Success = false, FailureInformation = "Unable to retrieve role/users." };
            }

        }

        public GetRolesForUserResponse GetRolesForUser(GetRolesForUserRequest request)
        {
            try
            {
                var response = new GetRolesForUserResponse();

                using (var context = new FcsuAgentEntities())
                {
                    var user = context.Users.FirstOrDefault(x => x.UserName == request.UserName);
                    if (user != null)
                    {
                        var foo = (from xrole in context.UserAndRoleXLinks
                                  join role in context.Roles on xrole.RoleId equals role.Id
                                  where xrole.UserId == user.Id
                                  select role).Distinct().ToList();

                        
                        foreach (var x in foo)
                        {
                            var role = new RoleInformation
                                {
                                    RolePk = x.Id,
                                    RoleName = x.Name,
                                    Description = x.Description,
                                    IsDisabled = x.IsDisabled

                                };
                            response.Roles.Add(role);
                        }

                    }


                }

                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                LoggingMediator.Log(ex.Message);
                return new GetRolesForUserResponse { Success = false, FailureMessage = "Unable to retrieve roles." };
            }


        }

        public GetUserInformationResponse GetUserInformation(GetUserInformationRequest request)
        {
            var response = new GetUserInformationResponse();
            try
            {
                using (var context = new FcsuAgentEntities())
                {
                    UserInformation lookupUser;
                    if (!string.IsNullOrEmpty(request.UserName))
                    {
                        lookupUser = (from user in context.Users
                                    where user.UserName == request.UserName
                                    select new UserInformation
                                        {
                                            UserPk = user.Id,
                                            UserName = user.UserName,
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,
                                            Email = user.Email,
                                            Comments = user.Comments,
                                            IsDisabled = user.IsDisabled,
                                            AgentNumber = user.AgentNumber,
                                            MemberNumber = user.MemberNumber,
                                            AgentMemberDescr=user.AgentMemberDescr,
                                            Phone=user.Phone
                                        }).FirstOrDefault();
                    }
                    else
                    {
                        lookupUser = (from user in context.Users
                                    where user.Id == request.UserPk
                                    select new UserInformation
                                    {
                                        UserPk = user.Id,
                                        UserName = user.UserName,
                                        FirstName = user.FirstName,
                                        LastName = user.LastName,
                                        Email = user.Email,
                                        Comments = user.Comments,
                                        IsDisabled = user.IsDisabled,
                                        AgentNumber = user.AgentNumber,
                                        MemberNumber = user.MemberNumber,
                                        AgentMemberDescr = user.AgentMemberDescr,
                                        Phone = user.Phone
                                    }).FirstOrDefault();
                    }

                    if (lookupUser != null)
                    {
                        response.User = lookupUser;
                     
                        //get all active roles available
                        var allRoles = context.Roles.Where(r=>r.IsDisabled == false).OrderBy(r => r.Description);
                        foreach (var role in allRoles)
                        {
                            response.Roles.Add(new RoleInformation
                                {
                                    Description= role.Description,
                                    IsDisabled = role.IsDisabled,
                                    RoleName=role.Name,
                                    UserIsInRole=false,
                                    RolePk=role.Id
                                });
                        }
                        //determine if the user is a member
                        var userRoles = context.UserAndRoleXLinks.Where(u => u.UserId == lookupUser.UserPk);
                        foreach (var item in userRoles)
                        {
                            var role = response.Roles.FirstOrDefault(x => x.RolePk == item.RoleId);
                            if (role != null) role.UserIsInRole = true;
                        }
                        
                        response.Success = true;
                    }
                    else
                    {
                        response.FailureMessage = "Unable to find the requested user";
                        response.Success = false;
                    }
                }
                return response;

            }
            catch (Exception eX)
            {
                LoggingMediator.Log(eX, LogEventType.Exception);
                return new GetUserInformationResponse { Success = false, FailureMessage = "Error from GetUserInformation:: " };
            }

        }
        
        public IsUserValidResponse IsUserValid(IsUserValidRequest request)
        {
            try
            {
                string encryptedPassword = SecurityHelper.EncryptString(request.Password, PasswordKey);

                var response = new IsUserValidResponse();
                using (var context = new FcsuAgentEntities())
                {
                    var query = from user in context.Users where user.UserName == request.UserName && user.Password == encryptedPassword && user.IsDisabled == false select user;
                    var f = query.FirstOrDefault();
                    if (f == null) response.FailureMessage = "The user name or password does not match and/or this account is disabled.\nPlease contact your administrator.";
                    else
                    {
                        response.UserId = f.Id;
                        response.AgentNumber = f.AgentNumber;
//                       response.MemberNumber = f.MemberNumber;
                        response.FirstName = f.FirstName;
                        response.LastName = f.LastName;
                        response.Success = true;
                    }
                }
                return response;
            }
            catch (Exception eX)
            {
                LoggingMediator.Log(eX, LogEventType.Exception);
                return new IsUserValidResponse { Success = false, FailureMessage = "Could not call Is User Valid" };
            }
        }

        public GetUserListResponse GetUserList(GetUserListRequest request)
        {
            try
            {
                var response = new GetUserListResponse();

                using (var context = new FcsuAgentEntities())
                {
                    var query = context.Users.
                        Select(user => new UserListInformation
                        {
                            UserPk = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            IsDisabled = user.IsDisabled,
                        });

                    response.UserList = query.OrderBy(x => x.UserName).ToList();
                }

                response.Success = true;
                return response;
            }
            catch (Exception eX)
            {
                LoggingMediator.Log(eX, LogEventType.Exception);
                return new GetUserListResponse() { Success = false, FailureMessage = "Could not Get User List" };
            }
        }

        public SaveUserResponse SaveUser(SaveUserRequest request)
        {
            try
            {
                var response = new SaveUserResponse();

                using (var context = new FcsuAgentEntities())
                {
                    var thisUser = context.Users.FirstOrDefault(u => u.Id == request.UserInfo.UserPk);
                    if (thisUser == null)
                    {
                        thisUser = new User();
                        Mapper.Map(request.UserInfo, thisUser);
                        thisUser.Id = request.UserInfo.UserPk == Guid.Empty ? Guid.NewGuid() : request.UserInfo.UserPk;
                        if (request.UserInfo.SetPassword == null) request.UserInfo.SetPassword = string.Empty;
                        thisUser.Password = SecurityHelper.EncryptString(request.UserInfo.SetPassword, PasswordKey);
                        context.Users.AddObject(thisUser);
                    }
                    else
                    {
                        Mapper.Map(request.UserInfo, thisUser);
                        if (request.UserInfo.SetPassword != null)
                            thisUser.Password = SecurityHelper.EncryptString(request.UserInfo.SetPassword,
                                                                             PasswordKey);
                    }
                    //map results back to response object
                    var destination = new UserInformation();
                    Mapper.Map(thisUser, destination);
                    destination.UserPk = thisUser.Id;
                    response.User = destination;
                    context.SaveChanges();

                    //set the roles 
                    //first wipe out all existing roles
                    foreach (var userInRole in context.UserAndRoleXLinks.Where(u => u.UserId == request.UserInfo.UserPk))
                    {
                        context.UserAndRoleXLinks.DeleteObject(userInRole);
                    }
                    //add back only roles selected
                    foreach (var role in request.Roles.Where(r => r.UserIsInRole))
                    {
                        var newRole = new UserAndRoleXLink
                        {
                            Id = Guid.NewGuid(),
                            UserId = request.UserInfo.UserPk,
                            RoleId = role.RolePk
                        };
                        context.UserAndRoleXLinks.AddObject(newRole);
                    }

                    context.SaveChanges();
                }
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                LoggingMediator.Log(ex);
                return new SaveUserResponse { Success = false, FailureMessage = "Generic fault in FcsuServices::SaveUser()" };
            }
        }

        public SaveRoleAndUsersResponse SaveRoleAndUsers(SaveRoleAndUsersRequest request)
        {
            try
            {
                var response = new SaveRoleAndUsersResponse();

                using (var context = new FcsuAgentEntities())
                {
                    var role = (from r in context.Roles
                                where r.Id == request.RoleInfo.RolePk
                                select r).FirstOrDefault();
                    if (role == null)
                    {
                        role = new Role();
                        context.Roles.AddObject(role);
                    }
                    role.Name = request.RoleInfo.RoleName;
                    role.Description = request.RoleInfo.Description;
                    role.IsDisabled = request.RoleInfo.IsDisabled;
                    role.Id = request.RoleInfo.RolePk == Guid.Empty ? Guid.NewGuid() : request.RoleInfo.RolePk;

                    //process people...
                    //wipe out all existing x-links for role
                    foreach (var userInRole in context.UserAndRoleXLinks.Where(u => u.RoleId == request.RoleInfo.RolePk))
                    {
                        context.UserAndRoleXLinks.DeleteObject(userInRole);
                    }
                    //add back only users selected
                    foreach (var user in request.UsersRoleList.Where(r => r.UserIsInRole))
                    {
                        var newRole = new UserAndRoleXLink
                        {
                            Id = Guid.NewGuid(),
                            UserId = user.UserPk,
                            RoleId = request.RoleInfo.RolePk
                        };
                        context.UserAndRoleXLinks.AddObject(newRole);
                    }


                    context.SaveChanges();
                }

                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                LoggingMediator.Log(ex.Message);
                return new SaveRoleAndUsersResponse { Success = false, FailureInformation = "Unable to save role." };
            }            
        }

        #region Purnima
        //Purnima
        public DeleteUserResponse DeleteUserResponse(DeleteUserRequest request)
        {
            try
            {
                var response = new DeleteUserResponse();

                using (var context = new FcsuAgentEntities())
                {
                    var thisUser = context.Users.FirstOrDefault(u => u.Id == request.UserPk);
                    if (thisUser == null)
                    {
                    }
                    else
                    {
                        context.Users.DeleteObject(thisUser);
                    }
                    //    context.SaveChanges();                    
                    response.Success = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LoggingMediator.Log(ex);
                return new DeleteUserResponse { Success = false, FailureMessage = "Unable to delete user" };
            }
        }
        //Purnima
        #endregion Purnima
    }
}
