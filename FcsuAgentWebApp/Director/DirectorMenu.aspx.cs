using FcsuAgentWebApp.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp.Director
{
    //HyperLink1.NavigateUrl = "~/Uploads/"+"2019 Comparative Report.pdf";
    public partial class DirectorMenu : System.Web.UI.Page
    {
        string constr = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        bool showAll_ = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //string url = "https://www.youtube.com/embed/u1qMIqJbE5w?rel=0 ";
            //iframeDiv.Controls.Add(new LiteralControl("<iframe src=\""+ url + "\" ></iframe><br />"));

           
            if (User.IsInRole("director"))
            {
                this.Master.addDirectorMenu();
            }
            if (User.IsInRole("admin"))
            {
                this.Master.addAdminMenu();
            }
            if (!Page.IsPostBack)
            {
             
                setLists(showAll_, bListDirector, "Board Of Directors Meeting", btnmoreDirector);
                setLists(showAll_, bListExecutive, "Executive Committee Meeting",btnmoreExecutive);
                setLists(showAll_, bListAnnouncements, "Announcements",btnmoreAnnouncement);
                setLists(showAll_, bListMiscellaneous, "Miscellaneous", btnmoreMiscellaneous);
            }
        }

        //Buttons
        //--------------
        protected void btnmoreDirector_Click(object sender, EventArgs e)
        {
            showMoreorLess(btnmoreDirector, bListDirector, "Board Of Directors Meeting");
        }
        protected void btnmoreExecutive_Click(object sender, EventArgs e)
        {
             showMoreorLess(btnmoreExecutive, bListExecutive, "Executive Committee Meeting");
        }
        protected void btnmoreAnnouncement_Click(object sender, EventArgs e)
        {
            showMoreorLess(btnmoreAnnouncement, bListAnnouncements, "Announcements");
        }
        protected void btnmoreMiscellaneous_Click(object sender, EventArgs e)
        {
            showMoreorLess(btnmoreMiscellaneous, bListMiscellaneous, "Miscellaneous");
        }
        //Methods
        //-------

        private void showMoreorLess(Button btnMore, BulletedList list,string category)
        {
            bool showall = false;
            if (btnMore.Text.Equals("ShowMore"))
            {
                btnMore.Text = "ShowLess";
                showall = true;
                setLists(showall, list, category, btnMore);
            }
            else//ShowLess clicked
            {
              
                btnMore.Text = "ShowMore";
                setLists(showall,list, category, btnMore);
            }
        }

        private void setLists(bool showall, BulletedList list, string category, Button btnMore)
        {/*(Year*100+ Month)*/
            string query = $"Select * From filesdir where category = '{category}' order by year desc, month desc";
            DataTable dtCategory = new DataTable();
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dtCategory);
            list.Items.Clear();
            btnMore.Visible = dtCategory.Rows.Count > 5 ? true : false;
            if (category.Equals("Board Of Directors"))
            {
                directorheader.Visible = dtCategory.Rows.Count > 0 ? true : false;
             }
            else if (category.Equals("Executive Committe"))
            {
                executiveheader.Visible = dtCategory.Rows.Count > 0 ? true : false;
            }
            else if(category.Equals("Announcements"))//Announcements
            {
                announceheader.Visible = dtCategory.Rows.Count > 0 ? true : false;
            }
           else //Miscellaneous
            {
                miscellaneousheader.Visible = dtCategory.Rows.Count > 0 ? true : false;
            }
            
            if (!showall)
            {
                int i = 1;
                foreach (DataRow dr in dtCategory.Rows)
                {
                    int month = Convert.ToInt16(dr["month"]);
                    string monthDescr = getMonthsDescr(month);
                    if (i < 6)
                    {
                        ListItem _item;
                        if ((bool)dr["link"] == true)
                        {
                            _item = new ListItem(string.Concat(dr["description"].ToString(), " - ", monthDescr, " " ,dr["year"].ToString()), dr["filename"].ToString());
                        }
                        else
                        {
                            _item = new ListItem(string.Concat(dr["description"].ToString(), " - ", monthDescr, " ", dr["year"].ToString()), string.Concat("~/Uploads/", dr["filename"]));
                        }

                       list.Items.Add(_item);
                    }
                    i++;
                }
               
            }
            else
            {
                foreach (DataRow dr in dtCategory.Rows)
                {
                    int month = Convert.ToInt16(dr["month"]);
                    string monthDescr = getMonthsDescr(month);


                    ListItem _item;
                        if ((bool)dr["link"] == true)
                        {
                            _item = new ListItem(string.Concat(dr["description"].ToString(), " - ", monthDescr, " ", dr["year"].ToString()), dr["filename"].ToString());
                        }
                        else
                        {
                            _item = new ListItem(string.Concat(dr["description"].ToString(), " - ", monthDescr, " ", dr["year"].ToString()), string.Concat("~/Uploads/", dr["filename"]));
                        }

                        list.Items.Add(_item);
                   
                }
                
            }
            list.Height = list.Items.Count * 20;
        }
           
          
        
       private string getMonthsDescr(int monthNum)
        {
            System.Globalization.DateTimeFormatInfo mfi = new
            System.Globalization.DateTimeFormatInfo();
            return  mfi.GetMonthName(monthNum).ToString();
            
        }
            
    }
}