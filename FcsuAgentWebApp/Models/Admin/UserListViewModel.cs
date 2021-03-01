using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CODE.Framework.Core.Utilities;
using CODE.Framework.Services.Client;
using FcsuAgent.Services.Contracts;

namespace FcsuAgentWebApp.Models
{
    public class UserListViewModel
    {
        
            public UserListViewModel()
            {
               // LoadAllUsers();
                LoadData();//Agents
                LoadMembers();
                LoadRoleNotAssigned();
                RolePk = Guid.Parse("8C390F2C-D861-46D6-B672-E27062B34668");
                LoadDirector();
            }
            private List<UserListDataModel> _users = new List<UserListDataModel>();
            public List<UserListDataModel> Users
            {
                get { return _users; }
                set { _users = value; }
            }
            private List<UserListDataModel> _members = new List<UserListDataModel>();
            public List<UserListDataModel> Members
            {
                get { return _members; }
                set { _members = value; }
            }
            private List<UserListDataModel> _director = new List<UserListDataModel>();
            public List<UserListDataModel> Director
            {
                get { return _director; }
                set { _director = value; }
            }
            private List<UserListDataModel> _norole = new List<UserListDataModel>();
            public List<UserListDataModel> NoRole
            {
                get { return _norole; }
                set { _norole = value; }
            }

            private List<UserListDataModel> _allusers = new List<UserListDataModel>();
            public List<UserListDataModel> AllUsers
            {
                get { return _allusers; }
                set { _allusers = value; }
            }
            public Guid RolePk { get; set; }
            public void LoadData()
            {
                try
                {
                    var response = new GetUserListResponse();
                    ServiceClient.Call<IFcsuServices>(x => response = x.GetUserList(new GetUserListRequest()));

                    if (response.Success)
                    {
                        foreach (var item in response.UserList)
                        {
                            int agentnum = (item.AgentNumber == string.Empty || item.AgentNumber == null) ? 0 : Convert.ToInt32(item.AgentNumber);
                            if (agentnum > 0)
                            {
                                var newUser = new UserListDataModel();
                                Mapper.Map(item, newUser);
                                Users.Add(newUser);
                            }

                        }
                    }
                    else
                    {
                        Users.Add(new UserListDataModel { UserName = response.FailureMessage });
                    }
                }
                catch (Exception exception)
                {
                    LoggingMediator.Log(exception, LogEventType.Exception);
                    Users.Add(new UserListDataModel { UserName = "Error getting users" });
                }
            }

            public void LoadMembers()
            {
                try
                {
                    var response = new GetUserListResponse();
                    ServiceClient.Call<IFcsuServices>(x => response = x.GetUserList(new GetUserListRequest()));

                    if (response.Success)
                    {
                        foreach (var item in response.UserList)
                        {
                            int memnum = (item.MemberNumber == string.Empty || item.MemberNumber == null) ? 0 : Convert.ToInt32(item.MemberNumber);
                            if (memnum > 0)
                            {
                                var newUser = new UserListDataModel();
                                Mapper.Map(item, newUser);
                                Members.Add(newUser);
                            }

                        }
                    }
                    else
                    {
                        Members.Add(new UserListDataModel { UserName = response.FailureMessage });
                    }
                }
                catch (Exception exception)
                {
                    LoggingMediator.Log(exception, LogEventType.Exception);
                    Members.Add(new UserListDataModel { UserName = "Error getting members" });
                }
            }


            public void LoadRoleNotAssigned()
            {
                try
                {
                    
                var response = new GetUserListResponse();
                    ServiceClient.Call<IFcsuServices>(x => response = x.GetUserList(new GetUserListRequest()));

                    if (response.Success)
                    {
                        foreach (var item in response.UserList)
                        {
                       
                       
                        
                        int agent = (item.AgentNumber == string.Empty || item.AgentNumber == null) ? 0 : Convert.ToInt32(item.AgentNumber);
                            int memnum = (item.MemberNumber == null || item.MemberNumber == string.Empty) ? 0 : Convert.ToInt32(item.MemberNumber);
                            
                            if (memnum == 0 && agent == 0)
                            {
                                var noRole = new UserListDataModel();
                                Mapper.Map(item, noRole);
                                NoRole.Add(noRole);
                            }

                        }
                    }
                    else
                    {
                        NoRole.Add(new UserListDataModel { UserName = response.FailureMessage });
                    }
                }
                catch (Exception exception)
                {
                    LoggingMediator.Log(exception, LogEventType.Exception);
                    NoRole.Add(new UserListDataModel { UserName = "Error getting no role assinged users" });
                }
            }


            public void LoadDirector()
            {
                try
                {
                    var Userresponse = new GetUserListResponse();
                    ServiceClient.Call<IFcsuServices>(x => Userresponse = x.GetUserList(new GetUserListRequest()));

                    if (!Userresponse.Success)
                    {
                        Members.Add(new UserListDataModel { UserName = Userresponse.FailureMessage });
                    }
                    var Roleresponse = new GetRoleAndUsersResponse();
                    ServiceClient.Call<IFcsuServices>(x => Roleresponse = x.GetRoleAndUsers(new GetRoleRequest
                    {
                        RolePk = RolePk
                    }));

                    if (Roleresponse.Success)
                    {
                        Mapper.Map(Roleresponse.RoleInfo, this);
                        foreach (var usr in Roleresponse.UsersRoleList)
                        {
                            if (usr.UserIsInRole == true)
                            {
                                foreach (var item in Userresponse.UserList)
                                {
                                    if (usr.UserName == item.UserName)
                                    {
                                        var newdirector = new UserListDataModel();
                                        Mapper.Map(item, newdirector);
                                        Director.Add(newdirector);
                                    }
                                }

                            }

                        }
                    }

                }
                catch (Exception exception)
                {
                    LoggingMediator.Log(exception, LogEventType.Exception);
                    Director.Add(new UserListDataModel { UserName = "Error getting directors" });
                }
            }
        //#endregi


    //    public void LoadAllUsers()
    //    {
    //        try
    //        {
    //            var response = new GetUserListResponse();
    //            ServiceClient.Call<IFcsuServices>(x => response = x.GetUserList(new GetUserListRequest()));

    //            if (response.Success)
    //            {
    //                foreach (var item in response.UserList)
    //                {
                        
    //                        var newUser = new UserListDataModel();
    //                        Mapper.Map(item, newUser);
    //                        AllUsers.Add(newUser);


    //                }
    //            }
    //            else
    //            {
    //                AllUsers.Add(new UserListDataModel { UserName = response.FailureMessage });
    //            }
    //        }
    //        catch (Exception exception)
    //        {
    //            LoggingMediator.Log(exception, LogEventType.Exception);
    //            AllUsers.Add(new UserListDataModel { UserName = "Error getting all users" });
    //        }
    //    }
    }






    }