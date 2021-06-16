﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp.Account
{
    public partial class ChangePasswordSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.IsInRole("director"))
            {
                  this.Master.addDirectorMenu();
               
            }
            if (User.IsInRole("member"))
            {
                this.Master.addMemberMenu();

            }
            if (User.IsInRole("agent"))
            {
                this.Master.addAgentMenu();

            }

        }

       
    }
}
