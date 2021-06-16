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
                x.Cells[3].Text = x.Cells[3].Text.Replace(";", "<br />");
            }
            for (int i = 0; i < 2; i++)
            {
                gridPolicy.Columns.Add(GridView1.Columns[i].ToString());
            }
            
            foreach (GridViewRow n in GridAnn.Rows)
            {
                n.Cells[3].Text = n.Cells[3].Text.Replace(";", "<br />");
            }

            foreach (GridViewRow g in GridSetlmt.Rows)
            {
                g.Cells[3].Text = g.Cells[3].Text.Replace(";", "<br />");
            }
        }






        protected void HideAll()
        {
           
            BenfLabel.Visible = false;

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
           
           string address= row.Cells[3].Text.Replace("<br />", " ");
           
            Table1.Visible = true;

            BenfLabel.Visible = true;

            // Policy Info
            TextBoxPolicy.Text = row.Cells[1].Text;
            TextBoxName.Text = row.Cells[2].Text;
            TextBoxAddress.Text = /*row.Cells[3].Text*/address;
            TextBoxPlan.Text = row.Cells[4].Text;
            TextBoxOwner.Text = row.Cells[27].Text;

            bool result;

            DateTime tmpUpDate;
            result = DateTime.TryParse(row.Cells[20].Text, out tmpUpDate);
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

            TextBoxIssueDate.Text = row.Cells[10].Text;

            DateTime tmpDate;
            result = DateTime.TryParse(row.Cells[14].Text, out tmpDate);
            //  if (result) { TextBoxBirthDate.Text = String.Format("{0:d}", tmpDate); } else { TextBoxBirthDate.Text = ""; }

            Int64 tmpPhone;
            result = Int64.TryParse(row.Cells[12].Text, out tmpPhone);
            if (result) { TextBoxPhone.Text = String.Format("{0:(###) ###-####}", tmpPhone); } else { TextBoxPhone.Text = ""; }

            if (row.Cells[13].Text.Equals("&nbsp;")) { TextBoxEmail.Text = ""; } else { TextBoxEmail.Text = row.Cells[13].Text; }

            DateTime tmpMatDate;
            result = DateTime.TryParse(row.Cells[17].Text, out tmpMatDate);
            if (result) { TextBoxMatDate.Text = String.Format("{0:d}", tmpMatDate); } else { TextBoxMatDate.Text = ""; }

            decimal annuityRate;
            result = Decimal.TryParse(row.Cells[8].Text, out annuityRate);


            decimal tempLien;
            result = Decimal.TryParse(row.Cells[21].Text, out tempLien);
            if (result) { TextBox_lien.Text = String.Format("{0:C}", tempLien); } else { TextBox_lien.Text = ""; }

            decimal tempLoan;
            result = Decimal.TryParse(row.Cells[22].Text, out tempLoan);
            if (result) { TextBox_loan.Text = String.Format("{0:C}", tempLoan); } else { TextBox_loan.Text = ""; }

            //decimal tempCashval;
            //result = Decimal.TryParse(row.Cells[23].Text, out tempCashval);
            //if (result) { TextBox_cv.Text = String.Format("{0:C}", tempCashval); } else { TextBox_cv.Text = ""; }

            decimal tempAccumdiv;
            result = Decimal.TryParse(row.Cells[25].Text, out tempAccumdiv);
            if (result) { TextBox_div.Text = String.Format("{0:C}", tempAccumdiv); } else { TextBox_div.Text = ""; }

            decimal tempPuadiv;
            result = Decimal.TryParse(row.Cells[24].Text, out tempPuadiv);
            if (result) { TextBox_div.Text = String.Format("{0:C}", tempPuadiv); } else { TextBox_div.Text = ""; }

            decimal tempTotdeath;
            result = Decimal.TryParse(row.Cells[26].Text, out tempTotdeath);
            if (result) { TextBox_db.Text = String.Format("{0:C}", tempTotdeath); } else { TextBox_db.Text = ""; }


            DateTime tmpLastUpDate;
            result = DateTime.TryParse(row.Cells[20].Text, out tmpLastUpDate);
            if (result) { Lbl_update.Text = "Information as of " + String.Format("{0:d}", tmpLastUpDate); } else { Lbl_update.Text = "Not Sure When Updated."; }



            if (result && annuityRate > 0)
            {
                // Policy is Annuity
                Int32 tmpValue;
                result = Int32.TryParse(row.Cells[5].Text, out tmpValue);
                if (result) { TextBoxValue.Text = String.Format("{0:C}", tmpValue); } else { TextBoxValue.Text = ""; }
                decimal tmpCurrRmd;
                Decimal.TryParse(row.Cells[29].Text, out tmpCurrRmd);
                //txtrmdreq.Text = tmpCurrRmd > 0m ? "Yes" : "No";
                //lblrmdreq.Text = string.Concat(DateTime.Now.Year, " RMD Required");
                decimal tmpDecimal;
                result = Decimal.TryParse(row.Cells[29].Text, out tmpDecimal);
                
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

                result = Decimal.TryParse(row.Cells[30].Text, out tmpDecimal);
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

                if (row.Cells[31].Text.Equals("Y"))
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
                TextBoxPremium.Text = row.Cells[9].Text;
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
                TextBoxValue.Text = row.Cells[5].Text;

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
                result = DateTime.TryParse(row.Cells[28].Text, out tmpPaidTo);
               // if (result) { TextBoxCurrentRate.Text = String.Format("{0:d}", tmpUpDate); } else { TextBoxCurrentRate.Text = ""; }
                  if (result) { TextBoxCurrentRate.Text = String.Format("{0:d}", tmpPaidTo); } else { TextBoxCurrentRate.Text = ""; }

                decimal tempDecimal;
                result = Decimal.TryParse(row.Cells[19].Text, out tempDecimal);
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            index = Convert.ToInt32(e.CommandArgument);

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

            if (e.CommandName == "Sort")
            {

                selectedRow = GridAnn.Rows[0];
                this.load_Information(selectedRow);

            }
            else if(e.CommandName == "Page")
            {
                GridView2.Visible = false;
               
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
            if (e.CommandName == "Sort")
            {

                selectedRow = GridSetlmt.Rows[0];
                this.load_Information(selectedRow);

            }
            else if (e.CommandName == "Page")
            {
                GridView2.Visible = false;

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

    }
}
