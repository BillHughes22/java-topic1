using FcsuAgentWebApp.DAL;
using FcsuAgentWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.BAL
{
    public class AdminMainBAL
    {

        public List<UserListDataModel> getUsersDetails()
        {
          AdminMainDAL admindal = new AdminMainDAL();
          return admindal.getUsersDetails();
        }
    }
}