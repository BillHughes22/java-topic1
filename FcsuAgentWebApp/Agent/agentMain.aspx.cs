using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp.Classes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace FcsuAgentWebApp.Agent
{
    public partial class policyView : System.Web.UI.Page
    {
        string number;
        GridViewRow selectedRow;
        int index;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView2.Visible = false;
            getAnnBalTable.Visible = false;

            if (User.IsInRole("member"))
            {
                this.Master.addMemberMenu();
            }
            if (User.IsInRole("agent"))
            {
                this.Master.addAgentMenu();
            }  
            if(User.IsInRole("admin"))
            {
//                this.Master.addNavMenu("Admin", "~/admin/userlist.aspx");
                this.Master.addAdminMenu();
            }
            if (User.IsInRole("director"))
            {
                this.Master.addDirectorMenu();
            }
            var userName = User.Identity.Name;
            var currentUser = (FcsuMembershipUser)new FcsuWebMembershipProvider().GetUser(userName, true);
            number = currentUser.AgentNumber;
            LabelNumber.Text = number;
            if (Page.IsPostBack)
            {
                HideAll();
            }
            Session["Agent"] = LabelNumber.Text;
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
//            var foo = "Yes";
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> searchNames(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            string number = HttpContext.Current.Session["Agent"] == null ? "" : HttpContext.Current.Session["Agent"].ToString();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "select m.LASTNAME from member m " +
                           "inner join policy p on p.CST_NUM = m.CST_NUM where " +
                      "m.LASTNAME like '" + prefixText + "%' and p.Agent =" + number + "";
                    cmd.CommandText = query;
                    cmd.Connection = myConnection;
                    myConnection.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["LASTNAME"].ToString());
                        }
                    }
                    myConnection.Close();
                    return names;
                }
            }
        }

        protected void calcBalance()
        {
            DateTime getBalanceDate;
            Boolean result;
            result = DateTime.TryParse(calcDateMonth.Text + "/" + calcDateDay.Text + "/" + calcDateYear.Text, out getBalanceDate);

            if (result)
            {
                string sqlString = "SELECT annbal.balance FROM annbal WHERE (annbal.policy = '" + TextBoxPolicy.Text + "') and (annbal.trandate = '" + getBalanceDate.ToShortDateString() + "')";


                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sqlString, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    // record found, do update
                    reader.Read();
                    decimal balanceThen = (decimal)reader.GetValue(0);
                    con.Close();

                    gottenAnnBalance.Text = balanceThen.ToString("$0,0.00");
                }
                else
                {
                    gottenAnnBalance.Text = "Balance not found for given date.";
                }
            }
            else
            {
                gottenAnnBalance.Text = "Invalid date.";
            }
        }
        protected void GridView1_RowCommand(object sender, EventArgs e)
        {
            calcDateMonth.Text = "";
            calcDateDay.Text = "";
            calcDateYear.Text = "";
            gottenAnnBalance.Text = "";
            this.load_Information(selectedRow);
            if (getAnnBalTable.Visible)
            {

                DateTime getBalanceDate2;
                Boolean result2;
                result2 = DateTime.TryParse(calcDateMonth.Text + "/" + calcDateDay.Text + "/" + calcDateYear.Text, out getBalanceDate2);
                if (result2)
                {
                    this.calcBalance();
                }
                else
                {
                    calcDateMonth.Text = "";
                    calcDateDay.Text = "";
                    calcDateYear.Text = "";
                }

            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcDateMonth.Text = "";
            calcDateDay.Text = "";
            calcDateYear.Text = "";
            gottenAnnBalance.Text = "";
            this.load_Information(selectedRow);
            if (getAnnBalTable.Visible)
            {

                DateTime getBalanceDate2;
                Boolean result2;
                result2 = DateTime.TryParse(calcDateMonth.Text + "/" + calcDateDay.Text + "/" + calcDateYear.Text, out getBalanceDate2);
                if (result2)
                {
                    this.calcBalance();
                }
                else
                {
                    calcDateMonth.Text = "";
                    calcDateDay.Text = "";
                    calcDateYear.Text = "";
                }

            }
        }
        /// <summary>
        /// changed GridView1.SelectedRow to row
        /// </summary>
        /// <param name="row"></param>
        protected void load_Information(GridViewRow row)
        {
            Table1.Visible = true;
            BenefLabel.Visible = true;
            RiderLabel.Visible = true;
            // Policy Info
            TextBoxPolicy.Text = row.Cells[1].Text;
            TextBoxName.Text = row.Cells[2].Text;
            TextBoxAddress.Text = row.Cells[3].Text;
            TextBoxPlan.Text = row.Cells[4].Text;


            bool result;

            DateTime tmpUpDate;
            result = DateTime.TryParse(row.Cells[20].Text, out tmpUpDate);
            if (result) { LabelCurBal.Text = String.Format("{0:d}", tmpUpDate) + " Balance:"; }
            if (result) { LabelPrevBal.Text = String.Format("{0:d}", tmpUpDate.AddYears(-1)) + " Balance:"; }


            //TextBoxValue.Text = String.Format("{0:G}", Int32.TryParse(GridView1.SelectedRow.Cells[5].Text));

            TextBoxIssueDate.Text = row.Cells[10].Text;

            DateTime tmpDate;
           
            if (row.Cells[21].Text.Equals("Y"))
            {
                TextBoxBirthDate.Visible = false;
                LabelBirthDate.Visible = false;

            }
            else
            {
                result = DateTime.TryParse(row.Cells[14].Text, out tmpDate);
                if (result) { TextBoxBirthDate.Text = String.Format("{0:d}", tmpDate); } else { TextBoxBirthDate.Text = ""; }

            }

            Int64 tmpPhone;
            result = Int64.TryParse(row.Cells[12].Text, out tmpPhone);
            if (result) { TextBoxPhone.Text = String.Format("{0:(###) ###-####}", tmpPhone); } else { TextBoxPhone.Text = ""; }

            if (row.Cells[13].Text.Equals("&nbsp;")) { TextBoxEmail.Text = ""; } else { TextBoxEmail.Text = row.Cells[13].Text; }

            DateTime tmpMatDate;
            result = DateTime.TryParse(row.Cells[17].Text, out tmpMatDate);
            if (result) { TextBoxMatDate.Text = String.Format("{0:d}", tmpMatDate); } else { TextBoxMatDate.Text = ""; }

            decimal annuityRate;
            result = Decimal.TryParse(row.Cells[8].Text, out annuityRate);

            if (result && annuityRate > 0)
            {
                RiderLabel.Visible = false;
                GridView4.Visible = false;
                Int32 tmpValue;
                result = Int32.TryParse(row.Cells[5].Text, out tmpValue);
                if (result) { TextBoxValue.Text = String.Format("{0:C}", tmpValue); } else { TextBoxValue.Text = ""; }



                // Annuity Grid and TextBoxes
                polinfo.Text = "Annuity Information";
                getAnnBalTable.Visible = true;
                LabelValue.Text = "Initial Deposit:";
                //removed 08/04/2020
                //System.Data.DataView dv = (System.Data.DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
                string sqlString = "SELECT * from annsum where policy='" + TextBoxPolicy.Text + "'";
                SqlDataSource dsTemp = new SqlDataSource(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString, sqlString);
                System.Data.DataView dv = (System.Data.DataView)dsTemp.Select(DataSourceSelectArguments.Empty);

                Table2.Visible = row.Cells[21].Text.Equals("N");
                Summary.Visible = row.Cells[21].Text.Equals("N");
                if(row.Cells[21].Text.Equals("N"))
                {
                    TextBoxPrevBal.Text = ((decimal)dv[0][2]).ToString("C2");
                    TextBoxDeposits.Text = ((decimal)dv[0][4]).ToString("N2");
                    TextBoxWithdrawals.Text = ((decimal)dv[0][5]).ToString("N2");
                    TextBoxInterest.Text = ((decimal)dv[0][6]).ToString("N2");
                    TextBoxCurBal.Text = ((decimal)dv[0][3]).ToString("C2");
                    TextBoxCurrentRate.Text = annuityRate.ToString("N3") + "%";
                }
               

                GridView2.Visible = true;
                //Table2.Visible = true;
                //Summary.Visible = true;
                Transactions.Visible = true;
                TextBoxCurrentRate.Visible = true;
                LabelCurrentRate.Visible = true;

                //decimal tempDecimal;
                //result = Decimal.TryParse(GridView1.SelectedRow.Cells[9].Text,out tempDecimal);
                //if (result) { TextBoxPremium.Text = String.Format("{0:C}", tempDecimal); } else { TextBoxPremium.Text = "";}
                TextBoxPremium.Text = row.Cells[9].Text;
                LabelPremium.Text = "Balance:";


                //result = Decimal.TryParse(GridView1.SelectedRow.Cells[6].Text, out tempDecimal);
                //if (result) { TextBoxValue.Text = String.Format("{0:C}", tempDecimal); } else { TextBoxValue.Text = ""; }


                System.Data.DataView dv3 = (System.Data.DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);
                dv3.RowFilter = "policy = '" + TextBoxPolicy.Text + "'";
                if (dv3.Count > 0)
                {
                    GridView2.DataSource = dv3;
                    Transactions.Text = "Transactions for last 12 months";
                }
                else
                {
                    GridView2.DataSource = null;
                    Transactions.Text = "No Transactions for last 12 months";
                }
                GridView2.DataBind();

            }
            else
            {
                getAnnBalTable.Visible = false;
                GridView4.Visible = true;
                Int32 tmpValue;
                result = Int32.TryParse(row.Cells[5].Text, out tmpValue);
                if (result) { TextBoxValue.Text = String.Format("{0:C0}", tmpValue); } else { TextBoxValue.Text = ""; }


                polinfo.Text = "Insurance Information";
                LabelValue.Text = "Face Amount:";
                GridView2.Visible = false;
                Table2.Visible = false;
                Summary.Visible = false;
                Transactions.Visible = false;
                TextBoxCurrentRate.Visible = false;
                LabelCurrentRate.Visible = false;

                decimal tempDecimal;
                result = Decimal.TryParse(row.Cells[19].Text, out tempDecimal);
                if (result) { TextBoxPremium.Text = String.Format("{0:C}", tempDecimal) + " " + row.Cells[18].Text; } else { TextBoxPremium.Text = ""; }
                LabelPremium.Text = "Premium:";
            }

            //naga
            if (GridView4.Rows.Count == 0)
            {
                RiderLabel.Visible = false;
            }

            
        }
            protected void GridView1_DataBound(object sender, EventArgs e)
        {
//            GridView1.Columns[5].Visible = false;
  //          GridView1.Columns[6].Visible = false;
    //        GridView1.Columns[7].Visible = false;
      //      GridView1.Columns[11].Visible = false;
        //    GridView1.Columns[12].Visible = false;
          //  GridView1.Columns[13].Visible = false;
            //GridView1.Columns[14].Visible = false;

        }
        
        protected void HideAll()
        {
            getAnnBalTable.Visible = false;
            BenefLabel.Visible = false;
            RiderLabel.Visible = false;

        }
        protected void txtSize_TextChanged1(object sender, EventArgs e)
        {
            GridView2.Visible = false;
            GridView1.PageSize = Convert.ToInt32(txtSize.Text);
        }
        protected void getAnnBalance_CLick(object sender, EventArgs e)
        {
            this.load_Information(GridView1.Rows[index]);
            this.calcBalance();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Select")
            //{

            //    // Convert the row index stored in the CommandArgument
            //    // property to an Integer.
             index = Convert.ToInt32(e.CommandArgument);

            // Get the last name of the selected author from the appropriate
            // cell in the GridView control.
             selectedRow = GridView1.Rows[index];


            // }

            this.load_Information(selectedRow);
        }

    }
}