using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp.Classes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using FcsuAgentWebApp.Models.DataModels;
using FcsuAgentWebApp.BAL;
using System.Text.RegularExpressions;
using FcsuAgentWebApp.Models.Checkout;
using FcsuAgentWebApp.Services.Business;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;


using System.Drawing;

namespace FcsuAgentWebApp.Member
{

    public partial class memberMain : System.Web.UI.Page
    {
       
        DataTable gridPolicy = new DataTable();
   
        string number;
        GridViewRow selectedRow;
       
        int index;
        protected void Page_Load(object sender, EventArgs e)

        {
            if (User.IsInRole("member"))
            {
                this.Master.addMemberMenu();
            }
            if (User.IsInRole("admin"))
            {
                this.Master.addAdminMenu();
            }
            var userName = User.Identity.Name;
            var currentUser = (FcsuMembershipUser)new FcsuWebMembershipProvider().GetUser(userName, true);

            number = currentUser.MemberNumber;
            LabelNumber.Text = number;

            if (Page.IsPostBack)
            {
                HideAll();
            }
            else
            {
                load_memberDropdown();
            }
            // Hide payment information
            hidePayment();

            LabelNumber.Text = memberDropdown.Text;
            Session["member"] = LabelNumber.Text;
            number= memberDropdown.Text;
            List<AgentPolicyModel> policyList = new List<AgentPolicyModel>();
            MemberMainBAL objAgent = new MemberMainBAL();
            policyList = objAgent.getInsPolicyDetails(number);
            GridView1.DataSource = policyList;
            GridView1.DataBind();
            policyList = objAgent.getAnnPolicyDetails(number);
            GridAnn.DataSource = policyList;
            GridAnn.DataBind();
            policyList = objAgent.getSetPolicyDetails(number);
            GridSetlmt.DataSource = policyList;
            GridSetlmt.DataBind();

            GridViewRow r;
            if (GridView1.Rows.Count > 0)
            {
               r=GridView1.Rows[0];
            }
            else if(GridAnn.Rows.Count > 0)
            {
                r = GridAnn.Rows[0];
            }
            else if(GridSetlmt.Rows.Count > 0)
            {
                r = GridSetlmt.Rows[0];
            }
           
            foreach (GridViewRow x in GridView1.Rows)
            {
                x.Cells[4].Text = x.Cells[4].Text.Replace(";", "<br />");
            }
            for (int i = 0; i < 2; i++)
            {
                gridPolicy.Columns.Add(GridView1.Columns[i].ToString());
            }
            
            foreach (GridViewRow n in GridAnn.Rows)
            {
                n.Cells[4].Text = n.Cells[4].Text.Replace(";", "<br />");
            }

            foreach (GridViewRow g in GridSetlmt.Rows)
            {
                g.Cells[4].Text = g.Cells[4].Text.Replace(";", "<br />");
            }
        }






        protected void HideAll()
        {
           
            BenfLabel.Visible = false;

        }

        /// <summary>
        /// Hide everythihg relating to payment info in presentation layer
        /// </summary>
        protected void hidePayment()
        {
            Payment_Div.Visible = false;
        }


        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            //            var foo = "Yes";
        }

       

      

        protected void load_memberDropdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            string updateSqlStatement;

            updateSqlStatement = "SELECT member.cst_num, member.LASTNAME FROM member WHERE member.cst_num = '" + number + "' order by cst_num";

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(updateSqlStatement, myConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                myConnection.Close();

                memberDropdown.DataSource = ds.Tables[0].DefaultView;
                memberDropdown.DataTextField = "LASTNAME";
                memberDropdown.DataValueField = "cst_num";
                memberDropdown.DataBind();
            }

            memberDropdown.SelectedValue = number;
        }

        public void agtDropDownChg()
        {

        }

        protected void load_Information(GridViewRow row)
        {
           
           string address= row.Cells[4].Text.Replace("<br />", " ");
           
            Table1.Visible = true;

            BenfLabel.Visible = true;

            // UPDATE 06/17/2021
            // By adding in the Payment column - all cell numbers were required to be shifted by +1
            // Policy Info
            TextBoxPolicy.Text = row.Cells[2].Text;
            TextBoxName.Text = row.Cells[3].Text;
            TextBoxAddress.Text = /*row.Cells[4].Text*/address;
            TextBoxPlan.Text = row.Cells[5].Text;
            TextBoxOwner.Text = row.Cells[28].Text;

            bool result;

            DateTime tmpUpDate;
            result = DateTime.TryParse(row.Cells[21].Text, out tmpUpDate);
            if (result)
            {
                LabelCurBal.Text = String.Format("{0:d}", tmpUpDate) + " Balance:";
                LabelPrevBal.Text = String.Format("{0:d}", tmpUpDate.AddYears(-1)) + " Balance:";
                ////currRmdLbl.Text = tmpUpDate.ToString("yyyy") + " RMD";
                ////prevRmdLbl.Text = tmpUpDate.AddYears(-1).ToString("yyyy") + " RMD";
            }
            else
            {
                LabelCurBal.Text = "";
                LabelPrevBal.Text = "";
            }


            //TextBoxValue.Text = String.Format("{0:G}", Int32.TryParse(GridView1.SelectedRow.Cells[5].Text));

            TextBoxIssueDate.Text = row.Cells[11].Text;

            DateTime tmpDate;
            result = DateTime.TryParse(row.Cells[15].Text, out tmpDate);
            //  if (result) { TextBoxBirthDate.Text = String.Format("{0:d}", tmpDate); } else { TextBoxBirthDate.Text = ""; }

            Int64 tmpPhone;
            result = Int64.TryParse(row.Cells[13].Text, out tmpPhone);
            if (result) { TextBoxPhone.Text = String.Format("{0:(###) ###-####}", tmpPhone); } else { TextBoxPhone.Text = ""; }

            if (row.Cells[14].Text.Equals("&nbsp;")) { TextBoxEmail.Text = ""; } else { TextBoxEmail.Text = row.Cells[14].Text; }

            DateTime tmpMatDate;
            result = DateTime.TryParse(row.Cells[18].Text, out tmpMatDate);
            if (result) { TextBoxMatDate.Text = String.Format("{0:d}", tmpMatDate); } else { TextBoxMatDate.Text = ""; }

            decimal annuityRate;
            result = Decimal.TryParse(row.Cells[9].Text, out annuityRate);


            decimal tempLien;
            result = Decimal.TryParse(row.Cells[22].Text, out tempLien);
            if (result) { TextBox_lien.Text = String.Format("{0:C}", tempLien); } else { TextBox_lien.Text = ""; }

            decimal tempLoan;
            result = Decimal.TryParse(row.Cells[23].Text, out tempLoan);
            if (result) { TextBox_loan.Text = String.Format("{0:C}", tempLoan); } else { TextBox_loan.Text = ""; }

            //decimal tempCashval;
            //result = Decimal.TryParse(row.Cells[24].Text, out tempCashval);
            //if (result) { TextBox_cv.Text = String.Format("{0:C}", tempCashval); } else { TextBox_cv.Text = ""; }

            decimal tempAccumdiv;
            result = Decimal.TryParse(row.Cells[26].Text, out tempAccumdiv);
            if (result) { TextBox_div.Text = String.Format("{0:C}", tempAccumdiv); } else { TextBox_div.Text = ""; }

            decimal tempPuadiv;
            result = Decimal.TryParse(row.Cells[25].Text, out tempPuadiv);
            if (result) { TextBox_div.Text = String.Format("{0:C}", tempPuadiv); } else { TextBox_div.Text = ""; }

            decimal tempTotdeath;
            result = Decimal.TryParse(row.Cells[27].Text, out tempTotdeath);
            if (result) { TextBox_db.Text = String.Format("{0:C}", tempTotdeath); } else { TextBox_db.Text = ""; }


            DateTime tmpLastUpDate;
            result = DateTime.TryParse(row.Cells[21].Text, out tmpLastUpDate);
            if (result) { Lbl_update.Text = "Information as of " + String.Format("{0:d}", tmpLastUpDate); } else { Lbl_update.Text = "Not Sure When Updated."; }



            if (result && annuityRate > 0)
            {
                // Policy is Annuity
                Int32 tmpValue;
                result = Int32.TryParse(row.Cells[6].Text, out tmpValue);
                if (result) { TextBoxValue.Text = String.Format("{0:C}", tmpValue); } else { TextBoxValue.Text = ""; }
                decimal tmpCurrRmd;
                Decimal.TryParse(row.Cells[30].Text, out tmpCurrRmd);
                //txtrmdreq.Text = tmpCurrRmd > 0m ? "Yes" : "No";
                //lblrmdreq.Text = string.Concat(DateTime.Now.Year, " RMD Required");
                decimal tmpDecimal;
                result = Decimal.TryParse(row.Cells[30].Text, out tmpDecimal);
                
                if (result)
                {
                    ////currRmdBox.Text = String.Format("{0:C}", tmpDecimal);
                    ////currRmdBox.Visible = true;
                    ////currRmdLbl.Visible = true;
                    ////RMDtitle.Visible = true;
                }
                else
                {
                    ////currRmdBox.Text = "";
                    ////currRmdBox.Visible = false;
                    ////currRmdLbl.Visible = false;
                    ////RMDtitle.Visible = false;
                }

                result = Decimal.TryParse(row.Cells[31].Text, out tmpDecimal);
                if (result)
                {
                    ////prevRmdBox.Text = String.Format("{0:C}", tmpDecimal);
                    ////prevRmdBox.Visible = true;
                    ////prevRmdLbl.Visible = true;
                }
                else
                {
                    ////prevRmdBox.Text = "";
                    ////prevRmdBox.Visible = false;
                    ////prevRmdLbl.Visible = false;
                }

                // insurance fields made invisible
                Lbl_cv.Visible = false;
                TextBox_cv.Visible = false;
                Lbl_loan.Visible = false;
                TextBox_loan.Visible = false;
                Lbl_div.Visible = false;
                TextBox_div.Visible = false;
               
                // Annuity Grid and TextBoxes
                polinfo.Text = "Annuity Information";
                LabelValue.Text = "Initial Deposit:";


                //old way                //System.Data.DataView dv7 = (System.Data.DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

                string sqlString = "SELECT * from annsum where policy='" + TextBoxPolicy.Text + "'";
                SqlDataSource dsTemp = new SqlDataSource(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString, sqlString);
                System.Data.DataView dv = (System.Data.DataView)dsTemp.Select(DataSourceSelectArguments.Empty);

                if (dv.Count>0)
                {
                    TextBoxPrevBal.Text = ((decimal)dv[0][2]).ToString("C2");
                    TextBoxDeposits.Text = ((decimal)dv[0][4]).ToString("N2");
                    TextBoxWithdrawals.Text = ((decimal)dv[0][5]).ToString("N2");
                    TextBoxInterest.Text = ((decimal)dv[0][6]).ToString("N2");
                    TextBoxCurBal.Text = ((decimal)dv[0][3]).ToString("C2");
                    TextBoxCurrentRate.Text = annuityRate.ToString("N3") + "%";
                    TextBoxGurRate.Text = ((decimal)dv[0][8]).ToString("N3") + "%";
                }
                GridView2.Visible = true;
                Table2.Visible = true;
                Summary.Visible = true;
                Transactions.Visible = true;
                TextBoxCurrentRate.Visible = true;
                LabelCurrentRate.Text = "Current Rate:";
                LabelCurrentRate.Visible = true;
                LabelGurRate.Visible = true;
                TextBoxGurRate.Visible = true;
                TextBox_db.Visible = false;
                Lbl_db.Visible = false;
                //                getAnnBalTable.Visible = true;

                if (row.Cells[32].Text.Equals("Y"))
                {
                    LabelCurrentRate.Visible = false;
                    TextBoxCurrentRate.Visible = false;
                    LabelGurRate.Visible = false;
                    TextBoxGurRate.Visible = false;
                    LabelValue.Visible = false;
                    TextBoxValue.Visible = false;
                    TextBoxPremium.Visible = false;
                    LabelPremium.Visible = false;
                    LabelMatDate.Text = LabelIssueDate.Text;
                    TextBoxMatDate.Text = TextBoxIssueDate.Text;
                    LabelIssueDate.Visible = false;
                    TextBoxIssueDate.Visible = false;
                    Table2.Visible = false;
                    Summary.Visible = false;
                }

                decimal tempDecimal;
                //result = Decimal.TryParse(GridView1.SelectedRow.Cells[9].Text,out tempDecimal);
                //if (result) { TextBoxPremium.Text = String.Format("{0:C}", tempDecimal); } else { TextBoxPremium.Text = "";}
                TextBoxPremium.Text = row.Cells[10].Text;
                LabelPremium.Text = "Balance:";

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                string updateSqlStatement;
                updateSqlStatement = "SELECT policy, sum(amount) as totdep, trandate FROM deposit WHERE policy = @deppol group by policy, trandate";
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                {
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand(updateSqlStatement, myConnection);
                    myCommand.Parameters.AddWithValue("deppol", TextBoxPolicy.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "initdep");
                    myConnection.Close();
                    if (ds.Tables["initdep"].Rows.Count > 0)
                    {
                        TextBoxValue.Text = String.Format("{0:C}", (decimal)ds.Tables["initdep"].Rows[0]["totdep"]);
                    }

                }


                DateTime getBalanceDate;
                DateTime startBalanceDate = DateTime.Now;
                Boolean resultBalanceDate = false;
                //                resultBalanceDate = DateTime.TryParse(calcDateMonth.Text + "/" + calcDateDay.Text + "/" + calcDateYear.Text, out getBalanceDate);

                if (resultBalanceDate)
                {
                    //                    startBalanceDate = getBalanceDate.AddYears(-1).AddDays(1);
                    //                    SqlDataSource3.SelectCommand = "Select policy, trandate, amount, descr from deposit where trandate between '" + startBalanceDate.ToShortDateString() + "' and '" + getBalanceDate.ToShortDateString() + "' union Select policy, trandate, amount, descr from withdrl where trandate between '" + startBalanceDate.ToShortDateString() + "' and '" + getBalanceDate.ToShortDateString() + "'";
                }
                else
                {
                    //                    SqlDataSource3.SelectCommand = "SELECT policy, trandate, amount, descr FROM deposit WHERE (year = YEAR({ fn NOW() })) union SELECT policy, trandate, amount, descr FROM withdrl WHERE (year = YEAR({ fn NOW() })) ORDER BY trandate";
                    startBalanceDate = tmpUpDate.AddYears(-1).AddDays(1);
                    SqlDataSource3.SelectCommand = "Select policy, trandate, amount, descr from deposit where trandate between '" + startBalanceDate.ToShortDateString() + "' and '" + tmpUpDate.ToShortDateString() + "' union Select policy, trandate, amount, descr from withdrl where trandate between '" + startBalanceDate.ToShortDateString() + "' and '" + tmpUpDate.ToShortDateString() + "'";
                }


                DateTime what2Use = DateTime.Now;

                System.Data.DataView dv3 = (System.Data.DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);
                dv3.RowFilter = "policy = '" + TextBoxPolicy.Text + "'";
                if (dv3.Count > 0)
                {
                    GridView2.DataSource = dv3;
                    if (resultBalanceDate)
                    {
                        //                        Transactions.Text = "Transactions from " + startBalanceDate.ToShortDateString() + " to " + getBalanceDate.ToShortDateString();
                    }
                    else
                    {
                        Transactions.Text = "Transactions for last 12 months";
                    }
                }
                else
                {
                    GridView2.DataSource = null;
                    if (resultBalanceDate)
                    {
                        //                        Transactions.Text = "No Transactions from " + startBalanceDate.ToShortDateString() + " to " + getBalanceDate.ToShortDateString();
                    }
                    else
                    {
                        Transactions.Text = "No Transactions for last 12 months";
                    }
                }
                GridView2.DataBind();

            }
            else
            {
                // Policy is Life Insurance
                //Int32 tmpValue;
                //result = Int32.TryParse(GridView1.SelectedRow.Cells[5].Text, out tmpValue);
                //if (result) { TextBoxValue.Text = String.Format("{0:C0}", tmpValue); } else { TextBoxValue.Text = ""; }
                //changed because we added $ for int changed to string-Naga
                TextBoxValue.Text = row.Cells[6].Text;

                // insurance fields made visible (invisible if annuity)
                Lbl_cv.Visible = false;
                TextBox_cv.Visible = false;
                Lbl_loan.Visible = false;
                TextBox_loan.Visible = false;
                Lbl_div.Visible = false;
                TextBox_div.Visible = false;


                polinfo.Text = "Insurance Information";
                LabelValue.Text = "Face Amount:";
                GridView2.Visible = false;
                Table2.Visible = false;
                Summary.Visible = false;
                Transactions.Visible = false;
                TextBoxCurrentRate.Visible = false;
                LabelCurrentRate.Visible = false;
                LabelGurRate.Visible = false;
                TextBoxGurRate.Visible = false;
                TextBox_db.Visible = true;
                Lbl_db.Visible = true;
                //                getAnnBalTable.Visible = false;
                // rmd info not shown
                ////currRmdBox.Visible = false;
                ////currRmdLbl.Visible = false;
                ////RMDtitle.Visible = false;
                ////prevRmdBox.Visible = false;
                ////prevRmdLbl.Visible = false;


                TextBoxCurrentRate.Visible = true;
                LabelCurrentRate.Visible = true;
                LabelCurrentRate.Text = "Premium Due:"; //Paid To changed

                DateTime tmpPaidTo;
                result = DateTime.TryParse(row.Cells[29].Text, out tmpPaidTo);
               // if (result) { TextBoxCurrentRate.Text = String.Format("{0:d}", tmpUpDate); } else { TextBoxCurrentRate.Text = ""; }
                  if (result) { TextBoxCurrentRate.Text = String.Format("{0:d}", tmpPaidTo); } else { TextBoxCurrentRate.Text = ""; }

                decimal tempDecimal;
                result = Decimal.TryParse(row.Cells[20].Text, out tempDecimal);
                if (result) { TextBoxPremium.Text = String.Format("{0:C}", tempDecimal) + " " + row.Cells[18].Text; } else { TextBoxPremium.Text = ""; }
                LabelPremium.Text = "Premium:";

            }

        }




        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //            gottenAnnBalance.Text = "";
            //selectedRow=GridView1.Rows[GridView1.SelectedIndex];
            //oldindex = GridView1.SelectedIndex;
            //this.load_Information(selectedRow);
            
            //if (getAnnBalTable.Visible)
            //{

            //    DateTime getBalanceDate2;
            //    Boolean result2;
            //    result2 = DateTime.TryParse(calcDateMonth.Text + "/" + calcDateDay.Text + "/" + calcDateYear.Text, out getBalanceDate2);
            //    if (result2)
            //    {
            //        this.calcBalance();
            //    }
            //    else
            //    {
            //        calcDateMonth.Text = "";
            //        calcDateDay.Text = "";
            //        calcDateYear.Text = "";
            //    }

            //}
        }

        protected void GridAnn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////selectedRow = GridAnn.Rows[GridAnn.SelectedIndex];

            
            ////    oldindex = GridAnn.SelectedIndex;
            
            //////            gottenAnnBalance.Text = "";
            ////this.load_Information(selectedRow);
            //if (getAnnBalTable.Visible)
            //{

            //    DateTime getBalanceDate2;
            //    Boolean result2;
            //    result2 = DateTime.TryParse(calcDateMonth.Text + "/" + calcDateDay.Text + "/" + calcDateYear.Text, out getBalanceDate2);
            //    if (result2)
            //    {
            //        this.calcBalance();
            //    }
            //    else
            //    {
            //        calcDateMonth.Text = "";
            //        calcDateDay.Text = "";
            //        calcDateYear.Text = "";
            //    }

            //}
        }



        protected void GridView1_Sorted(object sender, EventArgs e)
        {

            //if (GridView1.SelectedIndex > -1)
            //{
            //    this.load_Information(GridView1.Rows[index]);
            //}
        }
        protected void GridAnn_Sorted(object sender, EventArgs e)
        {

            //    if (GridAnn.SelectedIndex > -1)
            //    {
            //        this.load_Information(GridAnn.Rows[index]);
            //    }
        }

        //protected void agentDropdown_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}
        protected void txtSize_TextChanged1(object sender, EventArgs e)
        {
            // GridView1.PageSize = Convert.ToInt32(txtSize.Text);
        }

        /// <summary>
        /// On Row Command for Payment Button - Added 06/17/2021
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            index = Convert.ToInt32(e.CommandArgument);
            selectedRow = GridView1.Rows[index];

            if (e.CommandName == "Sort")
            {
               
                    selectedRow = GridView1.Rows[0];
                    this.load_Information(selectedRow);
               
            }
            else if (e.CommandName == "Page")
            {
                GridView2.Visible = false;
                //selectedRow = GridView1.Rows[0];
                //this.load_Information(selectedRow);
            }
            // CommandName property to determine which button was clicked.
            else if (e.CommandName == "GetPayment")
            {
                // First get the row that was selected.
                GridViewRow gRow = selectedRow;
                // index containst the row that was selected.
                int index = gRow.RowIndex;
                // We need to make sure the Balance is not 0 so the person can make a payment on the policy
                string balance = gRow.Cells[20].Text;
                decimal balValue = 0;
                try
                {
                    balance = Regex.Replace(balance, "[^0-9.]", "");
                    balValue = Convert.ToDecimal(balance);
                }
                catch (Exception exp)
                {
                    var seeError = exp;
                    balValue = 0;
                }
                if (balValue == 0)
                {
                    // Show message if the balance is $0
                    Response.Write("<script>alert('The balance is $0 so no payment can be made at this time.  If there are any questions please contact your agent.');</script>");
                }
                else
                {
                    // Goto the methold to get the payment information
                    this.LoadPaymentInfo(gRow);
                }


            }
            else
            {
               // if (GridView1.SelectedIndex > -1)
                {
                   
                    //if (e.CommandName == "Select")
                    //{

                    //    // Convert the row index stored in the CommandArgument
                    //    // property to an Integer.
                   
                    // Get the last name of the selected author from the appropriate
                    // cell in the GridView control.
                    selectedRow = GridView1.Rows[index];


                    // }
                    this.load_Information(selectedRow);
                }
            }
           
                
        }

      
        protected void GridAnn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            index = Convert.ToInt32(e.CommandArgument);
            selectedRow = GridAnn.Rows[index];

            if (e.CommandName == "Sort")
            {

                selectedRow = GridAnn.Rows[0];
                this.load_Information(selectedRow);

            }
            else if(e.CommandName == "Page")
            {
                GridView2.Visible = false;
               
            }
            // CommandName property to determine which button was clicked.
            else if (e.CommandName == "GetPayment")
            {
                // First get the row that was selected.
                int index2 = Convert.ToInt32(e.CommandArgument);
                GridViewRow gRow = GridAnn.Rows[index2];
                // index containst the row that was selected.
                int index = gRow.RowIndex;
                // We need to make sure the Balance is not 0 so the person can make a payment on the policy
                string balance = gRow.Cells[10].Text;
                decimal balValue = 0;
                try
                {
                    balance = Regex.Replace(balance, "[^0-9.]", "");
                    balValue = Convert.ToDecimal(balance);
                }
                catch (Exception exp)
                {
                    var seeError = exp;
                    balValue = 0;
                }
                if (balValue == 0)
                {
                    // Show message if the balance is $0
                    Response.Write("<script>alert('The balance is $0 so no payment can be made at this time.  If there are any questions please contact your agent.');</script>");
                }
                else
                {
                    // Goto the methold to get the payment information
                    this.LoadPaymentInfo(gRow);
                }


            }
            else
            {
                //if (GridAnn.SelectedIndex > -1)
                {

                    //if (e.CommandName == "Select")
                    //{

                    //    // Convert the row index stored in the CommandArgument
                    //    // property to an Integer.

                    // Get the last name of the selected author from the appropriate
                    // cell in the GridView control.
                     selectedRow = GridAnn.Rows[index];



                    this.load_Information(selectedRow);
                }
            }
        }


        protected void GridSetlmt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            index = Convert.ToInt32(e.CommandArgument);
            selectedRow = GridSetlmt.Rows[index];
            if (e.CommandName == "Sort")
            {

                selectedRow = GridSetlmt.Rows[0];
                this.load_Information(selectedRow);

            }
            else if (e.CommandName == "Page")
            {
                GridView2.Visible = false;

            }
            // CommandName property to determine which button was clicked.
            else if (e.CommandName == "GetPayment")
            {
                // First get the row that was selected.
                GridViewRow gRow = selectedRow;
                // index containst the row that was selected.
                int index = gRow.RowIndex;
                // We need to make sure the Balance is not 0 so the person can make a payment on the policy
                string balance = gRow.Cells[10].Text;
                decimal balValue = 0;
                try
                {
                    balance = Regex.Replace(balance, "[^0-9.]", "");
                    balValue = Convert.ToDecimal(balance);
                }
                catch (Exception exp)
                {
                    var seeError = exp;
                    balValue = 0;
                }
                if (balValue == 0)
                {
                    // Show message if the balance is $0
                    Response.Write("<script>alert('The balance is $0 so no payment can be made at this time.  If there are any questions please contact your agent.');</script>");
                }
                else
                {
                    // Goto the methold to get the payment information
                    this.LoadPaymentInfo(gRow);
                }


            }
            else
            {
                //if (GridAnn.SelectedIndex > -1)
                {

                    //if (e.CommandName == "Select")
                    //{

                    //    // Convert the row index stored in the CommandArgument
                    //    // property to an Integer.
                   

                    // Get the last name of the selected author from the appropriate
                    // cell in the GridView control.
                    selectedRow = GridSetlmt.Rows[index];


                    // }

                    this.load_Information(selectedRow);
                }
            }
            
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            List<AgentPolicyModel> policyList = new List<AgentPolicyModel>();
            MemberMainBAL objAgent = new MemberMainBAL();
            policyList = objAgent.getInsPolicyDetails(number);
            GridView1.DataSource = policyList;
            GridView1.DataBind();
            policyList = objAgent.getAnnPolicyDetails(number);
            GridAnn.DataSource = policyList;
            GridAnn.DataBind();
            policyList = objAgent.getSetPolicyDetails(number);
            GridSetlmt.DataSource = policyList;
            GridSetlmt.DataBind();

        }

        /// <summary>
        /// Add to Cart Button Selectd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bttnPayment_Click(object sender, EventArgs e)
        {
            // First step is to validate the number is a valid dollar figure.
            var currencyString = tbPayment.Text;
            var regex = @"^((([1-9]\d{0,2},(\d{3},)*\d{3}|[1-9]\d*)(.\d{1,4})?)|(0\.\d{1,4}))$";
            var isValidCurency = Regex.IsMatch(currencyString, regex);

            // Only continue if the dollar value is valid
            if (isValidCurency)
            {
                Checkout checkoutItems = new Checkout();
                // Get the value to add
                try
                {
                    checkoutItems.payment = Convert.ToDecimal((tbPayment.Text).ToString());
                }
                catch (Exception)
                {
                    // Show message if it can not be converted to decimal
                    Response.Write("<script>alert('The dollar amount is not valid.  Please make sure the format is 0.00 and try again.');</script>");
                }

                // Get the userName
                var userName = User.Identity.Name;
                checkoutItems.userName = userName;
                // Get the currentUser
                var currentUser = (FcsuMembershipUser)new FcsuWebMembershipProvider().GetUser(userName, true);
                // Get the number of the member
                try
                {
                    checkoutItems.memberNumber = Convert.ToInt32(currentUser.MemberNumber);
                }
                catch (Exception)
                {
                    // Show message if member number can not be converted to int
                    Response.Write("<script>alert('The member number does not seem to be accurate.  Please try again...');</script>");
                }
                // Get the policy number
                checkoutItems.policyNumber = Session["policyNumber"].ToString();
                // Get the policy description
                checkoutItems.policyDesc = Session["policyDesc"].ToString();

                // Save this info to the temp shopping cart
                // and return if it is successful and last inserted id
                BusinessLayer SaveItems = new BusinessLayer();
                ReturnData dbInsertResults = new ReturnData();
                dbInsertResults = SaveItems.SaveCheckoutItems(checkoutItems);
                if (!dbInsertResults.isSuccessful)
                {
                    // Show message if member number can not be converted to int
                    Response.Write("<script>alert('The information did not get saved correctly.  Please try again...');</script>");
                }
                else
                {
                    // If the data was saved correctly, set this flag so we can now display the checkout now button.
                    Session["cartData"] = true;
                    Session["orderID"] = dbInsertResults.newId;

                    Response.Redirect("/Member/ShoppingCart/Default.aspx");
                }
            }







        }

        /// <summary>
        /// Method to get the payment information and put it in the shopping cart.
        /// </summary>
        protected void LoadPaymentInfo(GridViewRow gvRow)
        {
            // Set the focus on the payment textbox
            tbPayment.Focus();
            Payment_Div.Focus();

            int index = gvRow.RowIndex;
            // Get all the data for the selected row.
            //GridViewRow gvRow = GridView1.Rows[index];

            // Show the payment policy number
            lbl_Payment.Text = "Make a Payment for Policy #: " + gvRow.Cells[2].Text;
            lbl_Desc.Text = "Plan: " + gvRow.Cells[5].Text;
            // Define the session variables
            Session["policyNumber"] = gvRow.Cells[2].Text;
            Session["policyDesc"] = gvRow.Cells[5].Text;
            Payment_Div.Visible = true;
            HideAll();
            transactions2.Visible = false;



            //Table1.Visible = true;

            //BenfLabel.Visible = true;

            //// UPDATE 05/13/2021
            //// By adding in the Payment column - all cell numbers were required to be shifted by +1
            //// Policy Info
            //TextBoxPolicy.Text = gvRow.Cells[2].Text;

            //TextBoxName.Text = gvRow.Cells[3].Text;
            //TextBoxAddress.Text = gvRow.Cells[4].Text;
            //TextBoxPlan.Text = gvRow.Cells[5].Text;
            //TextBoxOwner.Text = gvRow.Cells[28].Text;

            //bool result;

            //DateTime tmpUpDate;
            //result = DateTime.TryParse(gvRow.Cells[21].Text, out tmpUpDate);
            //if (result)
            //{
            //    LabelCurBal.Text = String.Format("{0:d}", tmpUpDate) + " Balance:";
            //    LabelPrevBal.Text = String.Format("{0:d}", tmpUpDate.AddYears(-1)) + " Balance:";
            //    currRmdLbl.Text = tmpUpDate.ToString("yyyy") + " RMD";
            //    prevRmdLbl.Text = tmpUpDate.AddYears(-1).ToString("yyyy") + " RMD";
            //}
            //else
            //{
            //    LabelCurBal.Text = "";
            //    LabelPrevBal.Text = "";
            //}


            ////TextBoxValue.Text = String.Format("{0:G}", Int32.TryParse(gvRow.Cells[5].Text));

            //TextBoxIssueDate.Text = gvRow.Cells[11].Text;

            //DateTime tmpDate;
            //result = DateTime.TryParse(gvRow.Cells[15].Text, out tmpDate);
            ////  if (result) { TextBoxBirthDate.Text = String.Format("{0:d}", tmpDate); } else { TextBoxBirthDate.Text = ""; }

            //Int64 tmpPhone;
            //result = Int64.TryParse(gvRow.Cells[13].Text, out tmpPhone);
            //if (result) { TextBoxPhone.Text = String.Format("{0:(###) ###-####}", tmpPhone); } else { TextBoxPhone.Text = ""; }

            //if (gvRow.Cells[14].Text.Equals("&nbsp;")) { TextBoxEmail.Text = ""; } else { TextBoxEmail.Text = gvRow.Cells[14].Text; }

            //DateTime tmpMatDate;
            //result = DateTime.TryParse(gvRow.Cells[18].Text, out tmpMatDate);
            //if (result) { TextBoxMatDate.Text = String.Format("{0:d}", tmpMatDate); } else { TextBoxMatDate.Text = ""; }

            //decimal annuityRate;
            //result = Decimal.TryParse(gvRow.Cells[9].Text, out annuityRate);


            //decimal tempLien;
            //result = Decimal.TryParse(gvRow.Cells[22].Text, out tempLien);
            //if (result) { TextBox_lien.Text = String.Format("{0:C}", tempLien); } else { TextBox_lien.Text = ""; }

            //decimal tempLoan;
            //result = Decimal.TryParse(gvRow.Cells[23].Text, out tempLoan);
            //if (result) { TextBox_loan.Text = String.Format("{0:C}", tempLoan); } else { TextBox_loan.Text = ""; }

            //decimal tempCashval;
            //result = Decimal.TryParse(gvRow.Cells[24].Text, out tempCashval);
            //if (result) { TextBox_cv.Text = String.Format("{0:C}", tempCashval); } else { TextBox_cv.Text = ""; }

            //decimal tempAccumdiv;
            //result = Decimal.TryParse(gvRow.Cells[26].Text, out tempAccumdiv);
            //if (result) { TextBox_div.Text = String.Format("{0:C}", tempAccumdiv); } else { TextBox_div.Text = ""; }

            //decimal tempPuadiv;
            //result = Decimal.TryParse(gvRow.Cells[25].Text, out tempPuadiv);
            //if (result) { TextBox_div.Text = String.Format("{0:C}", tempPuadiv); } else { TextBox_div.Text = ""; }

            //decimal tempTotdeath;
            //result = Decimal.TryParse(gvRow.Cells[27].Text, out tempTotdeath);
            //if (result) { TextBox_db.Text = String.Format("{0:C}", tempTotdeath); } else { TextBox_db.Text = ""; }


            //DateTime tmpLastUpDate;
            //result = DateTime.TryParse(gvRow.Cells[21].Text, out tmpLastUpDate);
            //if (result) { Lbl_update.Text = "Information as of " + String.Format("{0:d}", tmpLastUpDate); } else { Lbl_update.Text = "Not Sure When Updated."; }



            //if (result && annuityRate > 0)
            //{
            //    // Policy is Annuity
            //    Int32 tmpValue;
            //    result = Int32.TryParse(gvRow.Cells[6].Text, out tmpValue);
            //    if (result) { TextBoxValue.Text = String.Format("{0:C}", tmpValue); } else { TextBoxValue.Text = ""; }

            //    decimal tmpDecimal;
            //    result = Decimal.TryParse(gvRow.Cells[30].Text, out tmpDecimal);
            //    if (result)
            //    {
            //        currRmdBox.Text = String.Format("{0:C}", tmpDecimal);
            //        currRmdBox.Visible = true;
            //        currRmdLbl.Visible = true;
            //        RMDtitle.Visible = true;
            //    }
            //    else
            //    {
            //        currRmdBox.Text = "";
            //        currRmdBox.Visible = false;
            //        currRmdLbl.Visible = false;
            //        RMDtitle.Visible = false;
            //    }

            //    result = Decimal.TryParse(gvRow.Cells[31].Text, out tmpDecimal);
            //    if (result)
            //    {
            //        prevRmdBox.Text = String.Format("{0:C}", tmpDecimal);
            //        prevRmdBox.Visible = true;
            //        prevRmdLbl.Visible = true;
            //    }
            //    else
            //    {
            //        prevRmdBox.Text = "";
            //        prevRmdBox.Visible = false;
            //        prevRmdLbl.Visible = false;
            //    }

            //    // insurance fields made invisible
            //    Lbl_cv.Visible = false;
            //    TextBox_cv.Visible = false;
            //    Lbl_loan.Visible = false;
            //    TextBox_loan.Visible = false;
            //    Lbl_div.Visible = false;
            //    TextBox_div.Visible = false;


            //    // Annuity Grid and TextBoxes
            //    polinfo.Text = "Annuity Information";
            //    LabelValue.Text = "Initial Deposit:";


            //    //old way                //System.Data.DataView dv7 = (System.Data.DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

            //    string sqlString = "SELECT * from annsum where policy='" + TextBoxPolicy.Text + "'";
            //    SqlDataSource dsTemp = new SqlDataSource(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString, sqlString);
            //    System.Data.DataView dv = (System.Data.DataView)dsTemp.Select(DataSourceSelectArguments.Empty);


            //    TextBoxPrevBal.Text = ((decimal)dv[0][2]).ToString("C2");
            //    TextBoxDeposits.Text = ((decimal)dv[0][4]).ToString("N2");
            //    TextBoxWithdrawals.Text = ((decimal)dv[0][5]).ToString("N2");
            //    TextBoxInterest.Text = ((decimal)dv[0][6]).ToString("N2");
            //    TextBoxCurBal.Text = ((decimal)dv[0][3]).ToString("C2");
            //    TextBoxCurrentRate.Text = annuityRate.ToString("N3") + "%";
            //    TextBoxGurRate.Text = ((decimal)dv[0][8]).ToString("N3") + "%";

            //    GridView2.Visible = true;
            //    Table2.Visible = true;
            //    Summary.Visible = true;
            //    Transactions.Visible = true;
            //    TextBoxCurrentRate.Visible = true;
            //    LabelCurrentRate.Text = "Current Rate:";
            //    LabelCurrentRate.Visible = true;
            //    LabelGurRate.Visible = true;
            //    TextBoxGurRate.Visible = true;
            //    TextBox_db.Visible = false;
            //    Lbl_db.Visible = false;
            //    //                getAnnBalTable.Visible = true;


            //    decimal tempDecimal;
            //    //result = Decimal.TryParse(gvRow.Cells[9].Text,out tempDecimal);
            //    //if (result) { TextBoxPremium.Text = String.Format("{0:C}", tempDecimal); } else { TextBoxPremium.Text = "";}
            //    TextBoxPremium.Text = gvRow.Cells[10].Text;
            //    LabelPremium.Text = "Balance:";

            //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            //    string updateSqlStatement;
            //    updateSqlStatement = "SELECT policy, sum(amount) as totdep, trandate FROM deposit WHERE policy = @deppol group by policy, trandate";
            //    using (SqlConnection myConnection = new SqlConnection(connectionString))
            //    {
            //        myConnection.Open();
            //        SqlCommand myCommand = new SqlCommand(updateSqlStatement, myConnection);
            //        myCommand.Parameters.AddWithValue("deppol", TextBoxPolicy.Text);
            //        SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            //        DataSet ds = new DataSet();
            //        adapter.Fill(ds, "initdep");
            //        myConnection.Close();
            //        if (ds.Tables["initdep"].Rows.Count > 0)
            //        {
            //            TextBoxValue.Text = String.Format("{0:C}", (decimal)ds.Tables["initdep"].Rows[0]["totdep"]);
            //        }

            //    }


            //    DateTime getBalanceDate;
            //    DateTime startBalanceDate = DateTime.Now;
            //    Boolean resultBalanceDate = false;
            //    //                resultBalanceDate = DateTime.TryParse(calcDateMonth.Text + "/" + calcDateDay.Text + "/" + calcDateYear.Text, out getBalanceDate);

            //    if (resultBalanceDate)
            //    {
            //        //                    startBalanceDate = getBalanceDate.AddYears(-1).AddDays(1);
            //        //                    SqlDataSource3.SelectCommand = "Select policy, trandate, amount, descr from deposit where trandate between '" + startBalanceDate.ToShortDateString() + "' and '" + getBalanceDate.ToShortDateString() + "' union Select policy, trandate, amount, descr from withdrl where trandate between '" + startBalanceDate.ToShortDateString() + "' and '" + getBalanceDate.ToShortDateString() + "'";
            //    }
            //    else
            //    {
            //        //                    SqlDataSource3.SelectCommand = "SELECT policy, trandate, amount, descr FROM deposit WHERE (year = YEAR({ fn NOW() })) union SELECT policy, trandate, amount, descr FROM withdrl WHERE (year = YEAR({ fn NOW() })) ORDER BY trandate";
            //        startBalanceDate = tmpUpDate.AddYears(-1).AddDays(1);
            //        SqlDataSource3.SelectCommand = "Select policy, trandate, amount, descr from deposit where trandate between '" + startBalanceDate.ToShortDateString() + "' and '" + tmpUpDate.ToShortDateString() + "' union Select policy, trandate, amount, descr from withdrl where trandate between '" + startBalanceDate.ToShortDateString() + "' and '" + tmpUpDate.ToShortDateString() + "'";
            //    }


            //    DateTime what2Use = DateTime.Now;

            //    System.Data.DataView dv3 = (System.Data.DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);
            //    dv3.RowFilter = "policy = '" + TextBoxPolicy.Text + "'";
            //    if (dv3.Count > 0)
            //    {
            //        GridView2.DataSource = dv3;
            //        if (resultBalanceDate)
            //        {
            //            //                        Transactions.Text = "Transactions from " + startBalanceDate.ToShortDateString() + " to " + getBalanceDate.ToShortDateString();
            //        }
            //        else
            //        {
            //            Transactions.Text = "Transactions for last 12 months";
            //        }
            //    }
            //    else
            //    {
            //        GridView2.DataSource = null;
            //        if (resultBalanceDate)
            //        {
            //            //                        Transactions.Text = "No Transactions from " + startBalanceDate.ToShortDateString() + " to " + getBalanceDate.ToShortDateString();
            //        }
            //        else
            //        {
            //            Transactions.Text = "No Transactions for last 12 months";
            //        }
            //    }
            //    GridView2.DataBind();

            //}
            //else
            //{
            //    // Policy is Life Insurance
            //    //Int32 tmpValue;
            //    //result = Int32.TryParse(gvRow.Cells[5].Text, out tmpValue);
            //    //if (result) { TextBoxValue.Text = String.Format("{0:C0}", tmpValue); } else { TextBoxValue.Text = ""; }
            //    //changed because we added $ for int changed to string-Naga
            //    TextBoxValue.Text = gvRow.Cells[6].Text;

            //    // insurance fields made visible (invisible if annuity)
            //    Lbl_cv.Visible = true;
            //    TextBox_cv.Visible = true;
            //    Lbl_loan.Visible = true;
            //    TextBox_loan.Visible = true;
            //    Lbl_div.Visible = true;
            //    TextBox_div.Visible = true;


            //    polinfo.Text = "Insurance Information";
            //    LabelValue.Text = "Face Amount:";
            //    GridView2.Visible = false;
            //    Table2.Visible = false;
            //    Summary.Visible = false;
            //    Transactions.Visible = false;
            //    TextBoxCurrentRate.Visible = false;
            //    LabelCurrentRate.Visible = false;
            //    LabelGurRate.Visible = false;
            //    TextBoxGurRate.Visible = false;
            //    TextBox_db.Visible = true;
            //    Lbl_db.Visible = true;
            //    //                getAnnBalTable.Visible = false;
            //    // rmd info not shown
            //    currRmdBox.Visible = false;
            //    currRmdLbl.Visible = false;
            //    RMDtitle.Visible = false;
            //    prevRmdBox.Visible = false;
            //    prevRmdLbl.Visible = false;


            //    TextBoxCurrentRate.Visible = true;
            //    LabelCurrentRate.Visible = true;
            //    LabelCurrentRate.Text = "Paid To:";

            //    DateTime tmpPaidTo;
            //    result = DateTime.TryParse(gvRow.Cells[29].Text, out tmpPaidTo);
            //    if (result) { TextBoxCurrentRate.Text = String.Format("{0:d}", tmpUpDate); } else { TextBoxCurrentRate.Text = ""; }


            //    decimal tempDecimal;
            //    result = Decimal.TryParse(gvRow.Cells[20].Text, out tempDecimal);
            //    if (result) { TextBoxPremium.Text = String.Format("{0:C}", tempDecimal) + " " + gvRow.Cells[18].Text; } else { TextBoxPremium.Text = ""; }
            //    LabelPremium.Text = "Premium:";

            //}

            //// For the payment section we want to hide most of the data
            //BenfLabel.Visible = false; // Beneficary Label
            //GridView3.Visible = false;  // Beneficary Information
            //divAnnuity.Visible = false; // Summary Information
            //transactions2.Visible = false; // Transactions section
            //getAnnBalTable.Visible = false; //

        }


    }
}
