using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using FcsuAgentWebApp.Classes;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
namespace FcsuAgentWebApp.Agent
{
    public partial class agentCommissions : System.Web.UI.Page
    {
        string number, agent_name, transfer_text, loggedInNumber;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.IsInRole("member"))
            {
                this.Master.addMemberMenu();
            }
            if (User.IsInRole("agent"))
            {
                this.Master.addAgentMenu();
            }
            if (User.IsInRole("admin"))
            {
                this.Master.addAdminMenu();
            }

            var userName = User.Identity.Name;
            var currentUser = (FcsuMembershipUser)new FcsuWebMembershipProvider().GetUser(userName, true);

            //number = currentUser.AgentNumber;
            loggedInNumber = currentUser.AgentNumber;
            // Testing Purpose Only - Remove for production
            //loggedInNumber = "1150";
            // Define the session
            Session["loggedInNumber"] = loggedInNumber;
            loggedInNumber = Session["loggedInNumber"].ToString();


            // Testing Purpose Only - Remove for production
            //Session["Agent"] = "1150";
            //number = "1150";



            if (Page.IsPostBack)
            {
                HideAll();
            }
            else
            {
                load_agentReports(loggedInNumber);

                // Display the Agent Number
                head_number.Text = number + "&nbsp;&nbsp;&nbsp;";
                // Get the Agent Name
                head_name.Text = getAgentName();
                // Populate the Agent header information
                getAgentHeader(loggedInNumber);

                // Populate the agent drop down menu
                load_agentDropDown();

                // Initially the number must be the agent that is logged in until the pull down agent selection is changed.
                number = loggedInNumber;
                Session["Agent"] = number;
            }



            LabelNumber.Text = agentDropdown.Text;
            // Testing purpose only - Remove for production
            //Session["Agent"] = LabelNumber.Text;
            //Session["Agent"] = "9999";
            //number = "1079";


        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]

        protected void HideAll()
        {
            gv_commissions.Visible = false;
            Export_PDF.Visible = false;
        }


        //-----------------------------------------------------------------------------------
        //
        // Load the reports for this agent that are available
        //
        //-----------------------------------------------------------------------------------
        protected void load_agentReports(string whichAgent)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            string updateSqlStatement;

            updateSqlStatement = string.Format(@"SELECT DISTINCT REPORT_NAME FROM [tbl_agent_commissions] WHERE ([AGENT] = '{0}') ORDER BY [REPORT_NAME]", whichAgent);


            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(updateSqlStatement, myConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                myConnection.Close();


                reportDropdown.DataSource = ds.Tables[0].DefaultView;
                reportDropdown.DataTextField = "REPORT_NAME";
                reportDropdown.DataValueField = "REPORT_NAME";
                reportDropdown.DataBind();
            }

            // Create the initial selection for the drop down
            reportDropdown.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Report", String.Empty));
            reportDropdown.SelectedIndex = 0;
        }
        //-----------------------------------------------------------------------------------
        //
        // End - Load the reports for this agent that are available
        //
        //-----------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------
        //
        // Routine that will populate the datatable from the pull down selection
        //
        //-------------------------------------------------------------------------------------------
        protected void reportDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Define the variables
            bool hasSellingAgent = false;
            bool isCTCT = false;
            string sellingAgentNum, sellingAgentName;
            number = Session["Agent"].ToString();

            // Calculate the commissions
            decimal COM1ST_Total = 0, COMREN_Total = 0, COMSP_Total = 0, COMANN_Total = 0;
            gv_commissions.Visible = true;
            // Get the selected item in the pull down menu
            string report_name = reportDropdown.SelectedItem.ToString();
            DataRow dtNewRow;

            //-----------------------------------------------------------------------------------------------------------
            // Start DataTable
            //-----------------------------------------------------------------------------------------------------------
            DataTable dt = new DataTable();

            // Create the GridVeiw columns and define datatype so we can set the DataFormatString in the GridView
            dt.Clear();

            dt.Columns.Add("POLICY").DataType = System.Type.GetType("System.String");
            dt.Columns.Add("FULLNM").DataType = System.Type.GetType("System.String");
            dt.Columns.Add("POLDATE").DataType = System.Type.GetType("System.DateTime");
            dt.Columns.Add("TYPE").DataType = System.Type.GetType("System.String");
            dt.Columns.Add("DATEREC").DataType = System.Type.GetType("System.DateTime");
            dt.Columns.Add("AMT_PD").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("YEAR").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("COM_PCT").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("AGENTPCT").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("COM1ST").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("COMREN").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("COMSP").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("COMANN").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("LODGE").DataType = System.Type.GetType("System.String");
            dt.Columns.Add("AGE").DataType = System.Type.GetType("System.Decimal");
            //number = 1043.ToString(); // used for testing only - remove for production

            //--------------------------------------------------------------
            // Determine if there are selling agents
            //--------------------------------------------------------------
            //string query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([PAY_AGT] = '{0}' AND REPORT_NAME = '{1}' AND TYPE = 'CTCT') ORDER BY [TYPE], [POLDATE]", number, report_name);
            // Update 08/20/2020 By Bill Hughes to include all sales agents instead of only including sales agents with commission transfers
            //string query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([PAY_AGT] = '{0}' AND REPORT_NAME = '{1}') ORDER BY [TYPE], [POLDATE]", number, report_name);
            // Update on 11/07/2020 to test for Agent with no Saleagt and Agent with Saleage and type is either CTCT or not
            string query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE (AGENT = '{0}' AND REPORT_NAME = '{1}') ORDER BY [TYPE], [POLDATE]", number, report_name);

            transfer_text = ""; //clear out the string
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                // Initialize the SqlCommand with the new SQL string and the connection information.
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Connection.Open();
                    using (SqlDataReader reader_admin = cmd.ExecuteReader())
                    {
                        if (reader_admin.HasRows)
                        {
                            // Lets now read in the data
                            while (reader_admin.Read())
                            {
                                // If there are rows then we know this one has at least one selling agent.
                                sellingAgentNum = reader_admin["SALEAGT"].ToString();
                                // Make sure there is a sellingAgentNum
                                if (sellingAgentNum != "")
                                {
                                    // If there are rows and the column is not blank then we know this one has at least one selling agent.
                                    hasSellingAgent = true;
                                }
                                if (reader_admin["TYPE"].ToString() == "CTCT")
                                    isCTCT = true;
                            }
                        }
                    }
                }
            }
            //--------------------------------------------------------------
            // End - Determine if there is sales agent present for the transfer
            //--------------------------------------------------------------

            // Here is where need to split the reports into two different reports
            // One if there are selling agents and one if there are not selling agents.

            if (hasSellingAgent && isCTCT)
            {
                //--------------------------------------------------------------
                // Loop through the selling agent(s)
                //--------------------------------------------------------------
                //query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([PAY_AGT] = '{0}' AND REPORT_NAME = '{1}' AND TYPE = 'CTCT') ORDER BY [TYPE], [POLDATE]", number, report_name);
                // Update 08/20/2020 By Bill Hughes to include all sales agents instead of only including sales agents with commission transfers
                query = string.Format(@"SELECT DISTINCT SALEAGT, SELLNAME FROM [tbl_agent_commissions] WHERE ([PAY_AGT] = '{0}' AND REPORT_NAME = '{1}') ORDER BY SALEAGT", number, report_name);
                transfer_text = ""; //clear out the string
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                    // Initialize the SqlCommand with the new SQL string and the connection information.
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader reader_admin = cmd.ExecuteReader())
                        {
                            if (reader_admin.HasRows)
                            {
                                // Lets now read in the data
                                while (reader_admin.Read())
                                {
                                    // If there are rows then we know this one has at least one selling agent.
                                    // Configure the first row in the grid


                                    sellingAgentNum = reader_admin["SALEAGT"].ToString();
                                    // Make sure there is a sellingAgentNum
                                    if (sellingAgentNum != "")
                                    {
                                        // Add a new row to the datatable
                                        dtNewRow = dt.NewRow();
                                        // Get the Selling Agent Number
                                        dtNewRow["POLICY"] = "Selling Agent: " + reader_admin["SALEAGT"].ToString() + " " + reader_admin["SELLNAME"].ToString(); ;
                                        dt.Rows.Add(dtNewRow);
                                        //----------------------------------------------------------------------------------------
                                        // Fill out the datatable for this agent
                                        //----------------------------------------------------------------------------------------
                                        query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([SALEAGT] = '{0}' AND REPORT_NAME = '{1}' AND [AGENT] = '{2}') ORDER BY [TYPE], [POLDATE]", sellingAgentNum, report_name, number);
                                        //query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([SALEAGT] = '{0}' AND REPORT_NAME = '{1}' AND (TYPE = 'CTCT' OR TYPE = '234N')) ORDER BY [TYPE], [POLDATE]", sellingAgentNum, report_name);
                                        //query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([AGENT] = '{0}' AND REPORT_NAME = '{1}') ORDER BY [TYPE], [POLDATE]", sellingAgentNum, report_name);

                                        using (SqlConnection conn2 = new SqlConnection())
                                        {
                                            conn2.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                                            // Initialize the SqlCommand with the new SQL string and the connection information.
                                            using (SqlCommand cmd2 = new SqlCommand(query, conn2))
                                            {
                                                cmd2.Connection.Open();
                                                using (SqlDataReader reader_admin2 = cmd2.ExecuteReader())
                                                {
                                                    if (reader_admin2.HasRows)
                                                    {
                                                        try
                                                        {
                                                            // Lets now read in the data
                                                            while (reader_admin2.Read())
                                                            {
                                                                // Add a new row to the datatable
                                                                dtNewRow = dt.NewRow();


                                                                dtNewRow["POLICY"] = reader_admin2["POLICY"].ToString();
                                                                dtNewRow["FULLNM"] = reader_admin2["FULLNM"].ToString().ToUpper();

                                                                try
                                                                {
                                                                    dtNewRow["POLDATE"] = reader_admin2["POLDATE"].ToString();
                                                                }
                                                                catch
                                                                {
                                                                    dtNewRow["POLDATE"] = DBNull.Value;
                                                                }

                                                                // If the type is CTCT do not show
                                                                if (reader_admin2["TYPE"].ToString() != "CTCT")
                                                                {
                                                                    dtNewRow["TYPE"] = reader_admin2["TYPE"].ToString();
                                                                    var bill = reader_admin2["TYPE"].ToString();
                                                                }


                                                                try
                                                                {
                                                                    dtNewRow["DATEREC"] = reader_admin2["DATEREC"].ToString();
                                                                }
                                                                catch
                                                                {
                                                                    dtNewRow["DATEREC"] = DBNull.Value;
                                                                }

                                                                // If the type is CTCT do not show
                                                                if (reader_admin2["TYPE"].ToString() != "CTCT")
                                                                {
                                                                    dtNewRow["AMT_PD"] = reader_admin2["AMT_PD"];
                                                                    dtNewRow["YEAR"] = reader_admin2["YEAR"];
                                                                    try
                                                                    {
                                                                        dtNewRow["COM_PCT"] = reader_admin2["COM_PCT"];
                                                                        var billy = reader_admin2["COM_PCT"];
                                                                    }
                                                                    catch
                                                                    {
                                                                        dtNewRow["COM_PCT"] = 0;
                                                                    }

                                                                }



                                                                // Make sure Agent Share is not null from the db table
                                                                //if (reader_admin["AGENTPCT"] == DBNull.Value)
                                                                //{
                                                                //    dtNewRow["AGENTPCT"] = 0;
                                                                //}
                                                                //else
                                                                //{
                                                                dtNewRow["AGENTPCT"] = reader_admin2["AGENTPCT"];
                                                                //}

                                                                dtNewRow["COM1ST"] = reader_admin2["COM1ST"];
                                                                dtNewRow["COMREN"] = reader_admin2["COMREN"];
                                                                dtNewRow["COMSP"] = reader_admin2["COMSP"];
                                                                dtNewRow["COMANN"] = reader_admin2["COMANN"];
                                                                dtNewRow["LODGE"] = reader_admin2["LODGE"].ToString();
                                                                dt.Rows.Add(dtNewRow);

                                                                // Add up 1st Year Life
                                                                try
                                                                {
                                                                    COM1ST_Total = COM1ST_Total + Convert.ToDecimal(reader_admin2["COM1ST"]);
                                                                }
                                                                catch
                                                                {
                                                                    COM1ST_Total = 0;
                                                                }
                                                                // Add up Renewal Life
                                                                try
                                                                {
                                                                    COMREN_Total = COMREN_Total + Convert.ToDecimal(reader_admin2["COMREN"]);
                                                                }
                                                                catch
                                                                {
                                                                    COMREN_Total = 0;
                                                                }
                                                                // Add up Single Premium
                                                                try
                                                                {
                                                                    COMSP_Total = COMSP_Total + Convert.ToDecimal(reader_admin2["COMSP"]);
                                                                }
                                                                catch
                                                                {
                                                                    COMSP_Total = 0;
                                                                }
                                                                // Add up Annuity
                                                                try
                                                                {
                                                                    COMANN_Total = COMANN_Total + Convert.ToDecimal(reader_admin2["COMANN"]);
                                                                }
                                                                catch
                                                                {
                                                                    COMANN_Total = 0;
                                                                }



                                                                // Add to datatable dt
                                                                ////dt.Rows.Add(reader_admin["AGENT"].ToString(), reader_admin["POLICY"].ToString(), reader_admin["FULLNM"].ToString().ToUpper(), reader_admin["POLDATE"], reader_admin["TYPE"], reader_admin["DATEREC"], reader_admin["AMT_PD"], reader_admin["YEAR"], reader_admin["COM_PCT"], reader_admin["AGENTPCT"], reader_admin["COM1ST"], reader_admin["COMREN"], reader_admin["COMSP"], reader_admin["COMANN"], reader_admin["LODGE"]);
                                                            } // End of While routine
                                                        }
                                                        catch (Exception err)
                                                        {
                                                            // This will need to be put in the error log
                                                            var errormessage = err;
                                                            //Response.Write("This is the error message for State selection: " + err);
                                                            // Set the Pass or Fail varibl
                                                            //pass = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        // Insert a blank line
                                        dtNewRow = dt.NewRow();
                                        // Get the Selling Agent Number
                                        dtNewRow["POLICY"] = "";

                                        dt.Rows.Add(dtNewRow);
                                    }
                                    //----------------------------------------------------------------------------------------
                                    // End - filling out the datatable for this agent
                                    //----------------------------------------------------------------------------------------






                                }
                            }
                        }
                    }
                }
                //--------------------------------------------------------------
                // End - Determine if there is sales agent and CTCT present for the transfer
                //--------------------------------------------------------------

            }
            else if (hasSellingAgent && !isCTCT)
            {
                //--------------------------------------------------------------
                // Start - Determine all the rows without the SaleAgt
                //--------------------------------------------------------------
                query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([AGENT] = '{0}' AND REPORT_NAME = '{1}' AND SALEAGT = '') ORDER BY [TYPE], [POLDATE]", number, report_name);

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                    // Initialize the SqlCommand with the new SQL string and the connection information.
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader reader_admin = cmd.ExecuteReader())
                        {
                            if (reader_admin.HasRows)
                            {
                                try
                                {
                                    // Lets now read in the data
                                    while (reader_admin.Read())
                                    {
                                        // Add a new row to the datatable
                                        dtNewRow = dt.NewRow();


                                        dtNewRow["POLICY"] = reader_admin["POLICY"].ToString();
                                        dtNewRow["FULLNM"] = reader_admin["FULLNM"].ToString().ToUpper();

                                        try
                                        {
                                            dtNewRow["POLDATE"] = reader_admin["POLDATE"].ToString();
                                        }
                                        catch
                                        {
                                            dtNewRow["POLDATE"] = DBNull.Value;
                                        }

                                        // If the type is CTCT do not show
                                        if (reader_admin["TYPE"].ToString() != "CTCT")
                                        {
                                            dtNewRow["TYPE"] = reader_admin["TYPE"].ToString();
                                        }


                                        try
                                        {
                                            dtNewRow["DATEREC"] = reader_admin["DATEREC"].ToString();
                                        }
                                        catch
                                        {
                                            dtNewRow["DATEREC"] = DBNull.Value;
                                        }

                                        // If the type is CTCT do not show
                                        if (reader_admin["TYPE"].ToString() != "CTCT")
                                        {
                                            dtNewRow["AMT_PD"] = reader_admin["AMT_PD"];
                                            dtNewRow["YEAR"] = reader_admin["YEAR"];
                                            try
                                            {
                                                dtNewRow["COM_PCT"] = reader_admin["COM_PCT"];
                                                var billy = reader_admin["COM_PCT"];
                                            }
                                            catch
                                            {
                                                dtNewRow["COM_PCT"] = 0;
                                            }

                                        }



                                        // Make sure Agent Share is not null from the db table
                                        //if (reader_admin["AGENTPCT"] == DBNull.Value)
                                        //{
                                        //    dtNewRow["AGENTPCT"] = 0;
                                        //}
                                        //else
                                        //{
                                        dtNewRow["AGENTPCT"] = reader_admin["AGENTPCT"];
                                        //}

                                        dtNewRow["COM1ST"] = reader_admin["COM1ST"];
                                        dtNewRow["COMREN"] = reader_admin["COMREN"];
                                        dtNewRow["COMSP"] = reader_admin["COMSP"];
                                        dtNewRow["COMANN"] = reader_admin["COMANN"];
                                        dtNewRow["LODGE"] = reader_admin["LODGE"].ToString();
                                        dt.Rows.Add(dtNewRow);

                                        // Add up 1st Year Life
                                        try
                                        {
                                            COM1ST_Total = COM1ST_Total + Convert.ToDecimal(reader_admin["COM1ST"]);
                                        }
                                        catch
                                        {
                                            COM1ST_Total = 0;
                                        }
                                        // Add up Renewal Life
                                        try
                                        {
                                            COMREN_Total = COMREN_Total + Convert.ToDecimal(reader_admin["COMREN"]);
                                        }
                                        catch
                                        {
                                            COMREN_Total = 0;
                                        }
                                        // Add up Single Premium
                                        try
                                        {
                                            COMSP_Total = COMSP_Total + Convert.ToDecimal(reader_admin["COMSP"]);
                                        }
                                        catch
                                        {
                                            COMSP_Total = 0;
                                        }
                                        // Add up Annuity
                                        try
                                        {
                                            COMANN_Total = COMANN_Total + Convert.ToDecimal(reader_admin["COMANN"]);
                                        }
                                        catch
                                        {
                                            COMANN_Total = 0;
                                        }



                                        // Add to datatable dt
                                        ////dt.Rows.Add(reader_admin["AGENT"].ToString(), reader_admin["POLICY"].ToString(), reader_admin["FULLNM"].ToString().ToUpper(), reader_admin["POLDATE"], reader_admin["TYPE"], reader_admin["DATEREC"], reader_admin["AMT_PD"], reader_admin["YEAR"], reader_admin["COM_PCT"], reader_admin["AGENTPCT"], reader_admin["COM1ST"], reader_admin["COMREN"], reader_admin["COMSP"], reader_admin["COMANN"], reader_admin["LODGE"]);
                                    } // End of While routine
                                }
                                catch (Exception err)
                                {
                                    // This will need to be put in the error log
                                    var errormessage = err;
                                    //Response.Write("This is the error message for State selection: " + err);
                                    // Set the Pass or Fail varibl
                                    //pass = false;
                                }
                            }
                        }
                    }
                }
                //--------------------------------------------------------------
                // End - Determine all the rows without the SaleAgt
                //--------------------------------------------------------------

                // Put a blank row in the dt
                // Insert a blank line
                dtNewRow = dt.NewRow();
                dt.Rows.Add(dtNewRow);


                //--------------------------------------------------------------
                // Loop through the selling agent(s) where there is NOT CTCT
                //--------------------------------------------------------------
                //query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([PAY_AGT] = '{0}' AND REPORT_NAME = '{1}' AND TYPE = 'CTCT') ORDER BY [TYPE], [POLDATE]", number, report_name);
                // Update 08/20/2020 By Bill Hughes to include all sales agents instead of only including sales agents with commission transfers
                query = string.Format(@"SELECT DISTINCT SALEAGT, SELLNAME FROM [tbl_agent_commissions] WHERE (AGENT = '{0}' AND REPORT_NAME = '{1}') ORDER BY SALEAGT", number, report_name);
                transfer_text = ""; //clear out the string
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                    // Initialize the SqlCommand with the new SQL string and the connection information.
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader reader_admin = cmd.ExecuteReader())
                        {
                            if (reader_admin.HasRows)
                            {
                                // Lets now read in the data
                                while (reader_admin.Read())
                                {
                                    // If there are rows then we know this one has at least one selling agent.
                                    // Configure the first row in the grid


                                    sellingAgentNum = reader_admin["SALEAGT"].ToString();
                                    // Make sure there is a sellingAgentNum
                                    if (sellingAgentNum != "")
                                    {
                                        // Add a new row to the datatable
                                        dtNewRow = dt.NewRow();
                                        // Get the Selling Agent Number
                                        dtNewRow["POLICY"] = "Selling Agent: " + reader_admin["SALEAGT"].ToString() + " " + reader_admin["SELLNAME"].ToString(); ;
                                        dt.Rows.Add(dtNewRow);
                                        //----------------------------------------------------------------------------------------
                                        // Fill out the datatable for this agent
                                        //----------------------------------------------------------------------------------------
                                        query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([SALEAGT] = '{0}' AND REPORT_NAME = '{1}' AND TYPE <> 'CTCT') ORDER BY [TYPE], [POLDATE]", sellingAgentNum, report_name);
                                        //query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([AGENT] = '{0}' AND REPORT_NAME = '{1}') ORDER BY [TYPE], [POLDATE]", sellingAgentNum, report_name);

                                        using (SqlConnection conn2 = new SqlConnection())
                                        {
                                            conn2.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                                            // Initialize the SqlCommand with the new SQL string and the connection information.
                                            using (SqlCommand cmd2 = new SqlCommand(query, conn2))
                                            {
                                                cmd2.Connection.Open();
                                                using (SqlDataReader reader_admin2 = cmd2.ExecuteReader())
                                                {
                                                    if (reader_admin2.HasRows)
                                                    {
                                                        try
                                                        {
                                                            // Lets now read in the data
                                                            while (reader_admin2.Read())
                                                            {
                                                                // Add a new row to the datatable
                                                                dtNewRow = dt.NewRow();


                                                                dtNewRow["POLICY"] = reader_admin2["POLICY"].ToString();
                                                                dtNewRow["FULLNM"] = reader_admin2["FULLNM"].ToString().ToUpper();

                                                                try
                                                                {
                                                                    dtNewRow["POLDATE"] = reader_admin2["POLDATE"].ToString();
                                                                }
                                                                catch
                                                                {
                                                                    dtNewRow["POLDATE"] = DBNull.Value;
                                                                }

                                                                // If the type is CTCT do not show
                                                                if (reader_admin2["TYPE"].ToString() != "CTCT")
                                                                {
                                                                    dtNewRow["TYPE"] = reader_admin2["TYPE"].ToString();
                                                                    var bill = reader_admin2["TYPE"].ToString();
                                                                }


                                                                try
                                                                {
                                                                    dtNewRow["DATEREC"] = reader_admin2["DATEREC"].ToString();
                                                                }
                                                                catch
                                                                {
                                                                    dtNewRow["DATEREC"] = DBNull.Value;
                                                                }

                                                                // If the type is CTCT do not show
                                                                if (reader_admin2["TYPE"].ToString() != "CTCT")
                                                                {
                                                                    dtNewRow["AMT_PD"] = reader_admin2["AMT_PD"];
                                                                    dtNewRow["YEAR"] = reader_admin2["YEAR"];
                                                                    try
                                                                    {
                                                                        dtNewRow["COM_PCT"] = reader_admin2["COM_PCT"];
                                                                        var billy = reader_admin2["COM_PCT"];
                                                                    }
                                                                    catch
                                                                    {
                                                                        dtNewRow["COM_PCT"] = 0;
                                                                    }

                                                                }



                                                                // Make sure Agent Share is not null from the db table
                                                                //if (reader_admin["AGENTPCT"] == DBNull.Value)
                                                                //{
                                                                //    dtNewRow["AGENTPCT"] = 0;
                                                                //}
                                                                //else
                                                                //{
                                                                dtNewRow["AGENTPCT"] = reader_admin2["AGENTPCT"];
                                                                //}

                                                                dtNewRow["COM1ST"] = reader_admin2["COM1ST"];
                                                                dtNewRow["COMREN"] = reader_admin2["COMREN"];
                                                                dtNewRow["COMSP"] = reader_admin2["COMSP"];
                                                                dtNewRow["COMANN"] = reader_admin2["COMANN"];
                                                                dtNewRow["LODGE"] = reader_admin2["LODGE"].ToString();
                                                                dt.Rows.Add(dtNewRow);

                                                                // Add up 1st Year Life
                                                                try
                                                                {
                                                                    COM1ST_Total = COM1ST_Total + Convert.ToDecimal(reader_admin2["COM1ST"]);
                                                                }
                                                                catch
                                                                {
                                                                    COM1ST_Total = 0;
                                                                }
                                                                // Add up Renewal Life
                                                                try
                                                                {
                                                                    COMREN_Total = COMREN_Total + Convert.ToDecimal(reader_admin2["COMREN"]);
                                                                }
                                                                catch
                                                                {
                                                                    COMREN_Total = 0;
                                                                }
                                                                // Add up Single Premium
                                                                try
                                                                {
                                                                    COMSP_Total = COMSP_Total + Convert.ToDecimal(reader_admin2["COMSP"]);
                                                                }
                                                                catch
                                                                {
                                                                    COMSP_Total = 0;
                                                                }
                                                                // Add up Annuity
                                                                try
                                                                {
                                                                    COMANN_Total = COMANN_Total + Convert.ToDecimal(reader_admin2["COMANN"]);
                                                                }
                                                                catch
                                                                {
                                                                    COMANN_Total = 0;
                                                                }



                                                                // Add to datatable dt
                                                                ////dt.Rows.Add(reader_admin["AGENT"].ToString(), reader_admin["POLICY"].ToString(), reader_admin["FULLNM"].ToString().ToUpper(), reader_admin["POLDATE"], reader_admin["TYPE"], reader_admin["DATEREC"], reader_admin["AMT_PD"], reader_admin["YEAR"], reader_admin["COM_PCT"], reader_admin["AGENTPCT"], reader_admin["COM1ST"], reader_admin["COMREN"], reader_admin["COMSP"], reader_admin["COMANN"], reader_admin["LODGE"]);
                                                            } // End of While routine
                                                        }
                                                        catch (Exception err)
                                                        {
                                                            // This will need to be put in the error log
                                                            var errormessage = err;
                                                            //Response.Write("This is the error message for State selection: " + err);
                                                            // Set the Pass or Fail varibl
                                                            //pass = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        // Insert a blank line
                                        dtNewRow = dt.NewRow();
                                        // Get the Selling Agent Number
                                        dtNewRow["POLICY"] = "";

                                        dt.Rows.Add(dtNewRow);
                                    }
                                    //----------------------------------------------------------------------------------------
                                    // End - filling out the datatable for this agent
                                    //----------------------------------------------------------------------------------------






                                }
                            }
                        }
                    }
                }
                //--------------------------------------------------------------
                // End - Determine if there is sales agent and NOT CTCT
                //--------------------------------------------------------------

                //--------------------------------------------------------------
                // Determine if there is sales agent present for the transfer
                //--------------------------------------------------------------
                query = string.Format(@"SELECT PAY_AGT, AGENTNM FROM [tbl_agent_commissions] WHERE ([SALEAGT] = '{0}' AND REPORT_NAME = '{1}' AND TYPE = 'CTCT') ORDER BY [TYPE], [POLDATE]", number, report_name);
                transfer_text = ""; //clear out the string
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                    // Initialize the SqlCommand with the new SQL string and the connection information.
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader reader_admin = cmd.ExecuteReader())
                        {
                            if (reader_admin.HasRows)
                            {
                                try
                                {
                                    // Lets now read in the data
                                    while (reader_admin.Read())
                                    {
                                        // Now we can get the text
                                        transfer_text = " to Agent ";
                                        transfer_text = transfer_text + reader_admin["PAY_AGT"].ToString();
                                        transfer_text = transfer_text + " " + reader_admin["AGENTNM"].ToString().ToUpper();
                                    }

                                }
                                catch
                                {
                                    transfer_text = "";
                                }
                            }
                        }
                    }
                }
                //--------------------------------------------------------------
                // End - Determine if there is sales agent present for the transfer
                //--------------------------------------------------------------










            }

            else
            {

                query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([AGENT] = '{0}' AND REPORT_NAME = '{1}') ORDER BY [TYPE], [POLDATE]", number, report_name);

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                    // Initialize the SqlCommand with the new SQL string and the connection information.
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader reader_admin = cmd.ExecuteReader())
                        {
                            if (reader_admin.HasRows)
                            {
                                try
                                {
                                    // Lets now read in the data
                                    while (reader_admin.Read())
                                    {
                                        // Add a new row to the datatable
                                        dtNewRow = dt.NewRow();


                                        dtNewRow["POLICY"] = reader_admin["POLICY"].ToString();
                                        dtNewRow["FULLNM"] = reader_admin["FULLNM"].ToString().ToUpper();

                                        try
                                        {
                                            dtNewRow["POLDATE"] = reader_admin["POLDATE"].ToString();
                                        }
                                        catch
                                        {
                                            dtNewRow["POLDATE"] = DBNull.Value;
                                        }

                                        // If the type is CTCT do not show
                                        if (reader_admin["TYPE"].ToString() != "CTCT")
                                        {
                                            dtNewRow["TYPE"] = reader_admin["TYPE"].ToString();
                                        }


                                        try
                                        {
                                            dtNewRow["DATEREC"] = reader_admin["DATEREC"].ToString();
                                        }
                                        catch
                                        {
                                            dtNewRow["DATEREC"] = DBNull.Value;
                                        }

                                        // If the type is CTCT do not show
                                        if (reader_admin["TYPE"].ToString() != "CTCT")
                                        {
                                            dtNewRow["AMT_PD"] = reader_admin["AMT_PD"];
                                            dtNewRow["YEAR"] = reader_admin["YEAR"];
                                            try
                                            {
                                                dtNewRow["COM_PCT"] = reader_admin["COM_PCT"];
                                                var billy = reader_admin["COM_PCT"];
                                            }
                                            catch
                                            {
                                                dtNewRow["COM_PCT"] = 0;
                                            }

                                        }



                                        // Make sure Agent Share is not null from the db table
                                        //if (reader_admin["AGENTPCT"] == DBNull.Value)
                                        //{
                                        //    dtNewRow["AGENTPCT"] = 0;
                                        //}
                                        //else
                                        //{
                                        dtNewRow["AGENTPCT"] = reader_admin["AGENTPCT"];
                                        //}

                                        dtNewRow["COM1ST"] = reader_admin["COM1ST"];
                                        dtNewRow["COMREN"] = reader_admin["COMREN"];
                                        dtNewRow["COMSP"] = reader_admin["COMSP"];
                                        dtNewRow["COMANN"] = reader_admin["COMANN"];
                                        dtNewRow["LODGE"] = reader_admin["LODGE"].ToString();
                                        dt.Rows.Add(dtNewRow);

                                        // Add up 1st Year Life
                                        try
                                        {
                                            COM1ST_Total = COM1ST_Total + Convert.ToDecimal(reader_admin["COM1ST"]);
                                        }
                                        catch
                                        {
                                            COM1ST_Total = 0;
                                        }
                                        // Add up Renewal Life
                                        try
                                        {
                                            COMREN_Total = COMREN_Total + Convert.ToDecimal(reader_admin["COMREN"]);
                                        }
                                        catch
                                        {
                                            COMREN_Total = 0;
                                        }
                                        // Add up Single Premium
                                        try
                                        {
                                            COMSP_Total = COMSP_Total + Convert.ToDecimal(reader_admin["COMSP"]);
                                        }
                                        catch
                                        {
                                            COMSP_Total = 0;
                                        }
                                        // Add up Annuity
                                        try
                                        {
                                            COMANN_Total = COMANN_Total + Convert.ToDecimal(reader_admin["COMANN"]);
                                        }
                                        catch
                                        {
                                            COMANN_Total = 0;
                                        }



                                        // Add to datatable dt
                                        ////dt.Rows.Add(reader_admin["AGENT"].ToString(), reader_admin["POLICY"].ToString(), reader_admin["FULLNM"].ToString().ToUpper(), reader_admin["POLDATE"], reader_admin["TYPE"], reader_admin["DATEREC"], reader_admin["AMT_PD"], reader_admin["YEAR"], reader_admin["COM_PCT"], reader_admin["AGENTPCT"], reader_admin["COM1ST"], reader_admin["COMREN"], reader_admin["COMSP"], reader_admin["COMANN"], reader_admin["LODGE"]);
                                    } // End of While routine
                                }
                                catch (Exception err)
                                {
                                    // This will need to be put in the error log
                                    var errormessage = err;
                                    //Response.Write("This is the error message for State selection: " + err);
                                    // Set the Pass or Fail varibl
                                    //pass = false;
                                }
                            }
                        }
                    }
                }

                //--------------------------------------------------------------
                // Determine if there is sales agent present for the transfer
                //--------------------------------------------------------------
                query = string.Format(@"SELECT PAY_AGT, AGENTNM FROM [tbl_agent_commissions] WHERE ([SALEAGT] = '{0}' AND REPORT_NAME = '{1}' AND TYPE = 'CTCT') ORDER BY [TYPE], [POLDATE]", number, report_name);
                transfer_text = ""; //clear out the string
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                    // Initialize the SqlCommand with the new SQL string and the connection information.
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader reader_admin = cmd.ExecuteReader())
                        {
                            if (reader_admin.HasRows)
                            {
                                try
                                {
                                    // Lets now read in the data
                                    while (reader_admin.Read())
                                    {
                                        // Now we can get the text
                                        transfer_text = " to Agent ";
                                        transfer_text = transfer_text + reader_admin["PAY_AGT"].ToString();
                                        transfer_text = transfer_text + " " + reader_admin["AGENTNM"].ToString().ToUpper();
                                    }

                                }
                                catch
                                {
                                    transfer_text = "";
                                }
                            }
                        }
                    }
                }
                //--------------------------------------------------------------
                // End - Determine if there is sales agent present for the transfer
                //--------------------------------------------------------------


            }

            ViewState["dt"] = dt;
            //dt.DefaultView.Sort = "lname";

            gv_commissions.DataSource = ViewState["dt"] as DataTable;
            //grid.DataSource = dt;
            gv_commissions.DataBind();
            gv_commissions.Visible = true;

            // Define the footer
            // Have to define a space in fields with no value so they will print out as pdf ok
            // Merge the first 7 columns

            try
            {
                gv_commissions.FooterRow.Cells[0].ColumnSpan = 8;
                gv_commissions.FooterRow.Cells.RemoveAt(1);
                gv_commissions.FooterRow.Cells.RemoveAt(2);
                gv_commissions.FooterRow.Cells.RemoveAt(3);
                gv_commissions.FooterRow.Cells.RemoveAt(4);
                gv_commissions.FooterRow.Cells.RemoveAt(5);
                gv_commissions.FooterRow.Cells.RemoveAt(6);
                gv_commissions.FooterRow.Cells.RemoveAt(7);


                // Calculate the total for COM1ST
                gv_commissions.FooterRow.Cells[2].Text = COM1ST_Total.ToString("c");
                // Calculate the total for Renewal Life
                gv_commissions.FooterRow.Cells[3].Text = COMREN_Total.ToString("c");
                // Calculate the total for Single Premium
                gv_commissions.FooterRow.Cells[4].Text = COMSP_Total.ToString("c");
                // Calculate the total for Annuity
                gv_commissions.FooterRow.Cells[5].Text = COMANN_Total.ToString("c");
                // Calculate the total Commissions
                gv_commissions.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                gv_commissions.FooterRow.Cells[0].Text = loggedInNumber + "   Total Commission: " + (COM1ST_Total + COMREN_Total + COMSP_Total + COMANN_Total).ToString("c") + transfer_text;
                //gv_commissions.FooterRow.Cells[3].Text = " ";
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "s", "window.alert('The proper data was not available to show this commission structure...');", true);
            }


            // Show the Export button
            Export_PDF.Visible = true;

            // Format the cells for the selling agent.
            // Get the grid row count
            int row_count = gv_commissions.Rows.Count;
            int x;
            for (x = 0; x < row_count; x++)
            {
                // Get the value of the cells
                GridViewRow row = gv_commissions.Rows[x];
                string cell_value = row.Cells[0].Text.ToUpper();
                // Determine if cellvalue contains "SELLING"
                if (cell_value.Contains("SELLING"))
                {
                    // Now format the gridview
                    row.Cells[0].ColumnSpan = 14;
                    // Now remove all that we don't want.
                    row.Cells.RemoveAt(13);
                    row.Cells.RemoveAt(12);
                    row.Cells.RemoveAt(11);
                    row.Cells.RemoveAt(10);
                    row.Cells.RemoveAt(9);
                    row.Cells.RemoveAt(8);
                    row.Cells.RemoveAt(7);
                    row.Cells.RemoveAt(6);
                    row.Cells.RemoveAt(5);
                    row.Cells.RemoveAt(4);
                    row.Cells.RemoveAt(3);
                    row.Cells.RemoveAt(2);
                    row.Cells.RemoveAt(1);
                    // Merge all 14 columns
                    row.Cells[0].ColumnSpan = 14;

                    row.Cells[0].Font.Bold = true;
                    row.HorizontalAlign = HorizontalAlign.Left;
                }
            }

            // Format the cells for the commission transfer text.
            // Get the grid row count
            for (x = 0; x < row_count; x++)
            {
                // Get the value of the cells
                GridViewRow row = gv_commissions.Rows[x];
                // Only process rows that have more than one column
                int column_count = row.Cells.Count;
                if (column_count > 10)
                {

                    string cell_value = row.Cells[1].Text.ToUpper();

                    // Determine if cellvalue contains "SELLING"
                    if (cell_value.Contains("COMMISSION"))
                    {
                        // Move the cell contents to column 0
                        row.Cells[0].Text = cell_value;
                        // Now format the gridview
                        row.Cells[0].ColumnSpan = 14;
                        // Now remove all that we don't want.
                        // Merge first 4 columns
                        row.Cells[0].ColumnSpan = 4;


                        row.Cells.RemoveAt(3);
                        row.Cells.RemoveAt(2);
                        row.Cells.RemoveAt(1);


                        row.HorizontalAlign = HorizontalAlign.Left;
                    }
                }
            }

        }
        //-------------------------------------------------------------------------------------------
        //
        // End - Routine that will populate the datatable from the pull down selection
        //
        //-------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------
        //
        // Populate and Export PDF
        //
        //-------------------------------------------------------------------------------------------
        protected void Export_PDF_Click(object sender, EventArgs e)
        {
            // Define the Variables
            number = Session["Agent"].ToString();

            // Get the agent information
            string query = string.Format(@"SELECT agtparent.agent, agtparent.agtname FROM agtparent WHERE agtparent.agent = '" + number + "' order by agtname");

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                // Initialize the SqlCommand with the new SQL string and the connection information.
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Connection.Open();
                    using (SqlDataReader reader_admin = cmd.ExecuteReader())
                    {
                        if (reader_admin.HasRows)
                        {
                            try
                            {
                                // Lets now read in the data
                                while (reader_admin.Read())
                                {

                                    agent_name = reader_admin["AGTNAME"].ToString();
                                } // End of While routine
                            }
                            catch (Exception err)
                            {
                                // This will need to be put in the error log
                                var errormessage = err;
                                //Response.Write("This is the error message for State selection: " + err);
                                // Set the Pass or Fail varibl
                                //pass = false;
                            }
                        }
                    }
                }
            }




            // Adding the top for the header text
            String para = "FCSU Financial Life\nCommissions Paid List\n" + reportDropdown.SelectedItem.ToString() + "\n\n";
            // Creating an Area Break    
            Paragraph para1 = new Paragraph(para);
            para1.Alignment = Element.ALIGN_CENTER;

            // Add the agent name
            // Adding the top for the header text
            String para_name = number + "  " + agent_name + "\n\n";
            // Creating an Area Break    
            Paragraph para2 = new Paragraph(para_name);
            para2.Alignment = Element.ALIGN_LEFT;



            //BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            // Create the font structures for the PDF
            BaseFont bf = BaseFont.CreateFont(
                BaseFont.HELVETICA_BOLD,
                BaseFont.CP1252,
                BaseFont.EMBEDDED);
            Font font = new Font(bf, 10);

            BaseFont bf2 = BaseFont.CreateFont(
                BaseFont.HELVETICA,
                BaseFont.CP1252,
                BaseFont.EMBEDDED);
            Font font2 = new Font(bf2, 8);

            BaseFont bf3 = BaseFont.CreateFont(
                BaseFont.HELVETICA_BOLD,
                BaseFont.CP1252,
                BaseFont.EMBEDDED);
            Font font3 = new Font(bf3, 10);

            //gv_commissions.ShowHeader = true;
            PdfPTable pdfTable = new PdfPTable(gv_commissions.HeaderRow.Cells.Count);
            pdfTable.WidthPercentage = 100;
            // Define the individual cell widths
            float[] widths = new float[] { 40f, 110f, 45f, 30f, 50f, 50f, 25f, 75f, 50f, 50f, 50f, 50f, 50f, 40f };
            pdfTable.SetWidths(widths);

            // Add the headers to the table
            for (int j = 0; j < gv_commissions.Columns.Count; j++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(gv_commissions.Columns[j].HeaderText, font));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.MinimumHeight = 35;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfTable.AddCell(cell);
            }

            // Add individual rows
            int col_counter = 1;
            int counter = 1;
            bool commit_cell = false;

            // Loop through the rows in the gridview table
            foreach (GridViewRow gridViewRow in gv_commissions.Rows)
            {
                // Modify this section so it detectes the correct number of columns for each row.

                PdfPCell pdfCell = new PdfPCell();
                // reset the counter
                counter = 1;
                col_counter = 0;
                commit_cell = false;
                foreach (TableCell tableCell in gridViewRow.Cells)
                {
                    string bill = tableCell.Text;
                    // Prepare the cell to be written into
                    // If a cell in the table is null make sure to not print the &nbsp; in the PDF cell.
                    if (col_counter == 0)
                    {
                        if (tableCell.Text == "&nbsp;")
                        {
                            pdfCell = new PdfPCell(new Phrase("", font2));
                        }
                        else
                        {
                            pdfCell = new PdfPCell(new Phrase(tableCell.Text, font2));
                        }

                        pdfCell.BackgroundColor = new BaseColor(gv_commissions.RowStyle.BackColor);
                        // Right justify all but the name
                        if (counter == 2)
                        {
                            pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        }
                        else
                        {
                            pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }
                        pdfCell.Colspan = 1;
                        counter++;
                        pdfCell.MinimumHeight = 25;
                        commit_cell = true;
                    }

                    // Detect if the columns are merged?
                    if (tableCell.Text.ToUpper().Contains("SELLING AGENT"))
                    {
                        // Loop through 14 times and only fill in first column

                        col_counter = 14;
                        pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        pdfCell.Colspan = 14;

                        pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        // Commit the cell to the pdf
                        pdfTable.AddCell(pdfCell);
                        commit_cell = false;
                    }
                    else if (tableCell.Text.ToUpper().Contains("COMMISSION"))
                    {
                        col_counter = 3;
                        counter = counter + 5;
                        pdfCell.Colspan = 6;

                        pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        // Commit the cell to the pdf
                        pdfTable.AddCell(pdfCell);
                        commit_cell = false;

                    }
                    // Should the cell be committed
                    if (commit_cell && counter <= 15)
                    {
                        pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        pdfTable.AddCell(pdfCell);
                    }
                    // Make sure to continue decrementing the counter in the loop
                    if (col_counter > 0)
                    {
                        col_counter--;
                    }


                }
            }

            // Add Footer
            // Add individual rows
            counter = 1;
            col_counter = 0;
            commit_cell = false;
            foreach (TableCell footerCell in gv_commissions.FooterRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell();
                // Prepare the cell to be written into
                // If a cell in the table is null make sure to not print the &nbsp; in the PDF cell.
                //if (col_counter == 0)
                //{
                if (footerCell.Text == "&nbsp;")
                {
                    pdfCell = new PdfPCell(new Phrase("", font2));
                }
                else
                {
                    pdfCell = new PdfPCell(new Phrase(footerCell.Text, font2));
                }

                pdfCell.BackgroundColor = new BaseColor(gv_commissions.RowStyle.BackColor);
                // Right justify all but the name
                if (counter == 2)
                {
                    pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                }
                else
                {
                    pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                }
                pdfCell.Colspan = 1;
                counter++;
                pdfCell.MinimumHeight = 25;
                commit_cell = true;
                //}

                // Detect if the columns are merged?
                if (footerCell.Text.ToUpper().Contains("COMMISSION"))
                {
                    col_counter = 4;
                    counter = counter + 4;
                    pdfCell.Colspan = 8;

                    pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    // Commit the cell to the pdf
                    pdfTable.AddCell(pdfCell);
                    commit_cell = false;

                }
                // Should the cell be committed
                if (commit_cell && counter <= 15)
                {
                    pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    pdfTable.AddCell(pdfCell);
                }
                // Make sure to continue decrementing the counter in the loop
                if (col_counter > 0)
                {
                    col_counter--;
                }

                //PdfPCell pdfCell = new PdfPCell();
                //// If a cell in the table is null make sure to not print the &nbsp; in the PDF cell.
                //if (footerCell.Text == "&nbsp;")
                //{
                //    pdfCell = new PdfPCell(new Phrase("", font3));
                //}
                //else
                //{
                //    pdfCell = new PdfPCell(new Phrase(footerCell.Text, font3));
                //}
                //if (footerCell.Text.ToUpper().Contains("COMMISSION"))
                //{
                //    col_counter = 3;
                //    counter = counter + 5;
                //    pdfCell.Colspan = 6;

                //    pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //    // Commit the cell to the pdf
                //    pdfTable.AddCell(pdfCell);
                //    commit_cell = false;

                //}
                //pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //pdfCell.MinimumHeight = 35;
                //pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //pdfCell.BorderWidthTop = 3f;
                //pdfTable.AddCell(pdfCell);
            }

            Document pdfDocument = new Document(PageSize.A4.Rotate());
            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            pdfDocument.Open();
            pdfDocument.Add(para1);
            pdfDocument.Add(para2);
            pdfDocument.Add(pdfTable);
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition", "attachment;filename=Commission.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();



        }

        //-------------------------------------------------------------------------------------------
        //
        // End - Populate and Export PDF
        //
        //-------------------------------------------------------------------------------------------

        public override void VerifyRenderingInServerForm(Control control)

        {

        }

        protected void gv_commissions_DataBinding(object sender, EventArgs e)
        {
            // Define the vars
            number = Session["Agent"].ToString();
            // Calculate the commissions
            decimal COM1ST_Total = 0, COMREN_Total = 0, COMSP_Total = 0, COMANN_Total = 0;
            gv_commissions.Visible = true;
            // Get the selected item in the pull down menu
            string report_name = reportDropdown.SelectedItem.ToString();
            DataRow dtNewRow;

            //-----------------------------------------------------------------------------------------------------------
            // Start DataTable
            //-----------------------------------------------------------------------------------------------------------
            DataTable dt = new DataTable();

            // Create the GridVeiw columns and define datatype so we can set the DataFormatString in the GridView
            dt.Clear();

            dt.Columns.Add("POLICY").DataType = System.Type.GetType("System.String");
            dt.Columns.Add("FULLNM").DataType = System.Type.GetType("System.String");
            dt.Columns.Add("POLDATE").DataType = System.Type.GetType("System.DateTime");
            dt.Columns.Add("TYPE").DataType = System.Type.GetType("System.String");
            dt.Columns.Add("DATEREC").DataType = System.Type.GetType("System.DateTime");
            dt.Columns.Add("AMT_PD").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("YEAR").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("COM_PCT").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("AGENTPCT").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("COM1ST").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("COMREN").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("COMSP").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("COMANN").DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add("LODGE").DataType = System.Type.GetType("System.String");

            string query = string.Format(@"SELECT * FROM [tbl_agent_commissions] WHERE ([AGENT] = '{0}' AND REPORT_NAME = '{1}') ORDER BY [POLDATE]", number, report_name);

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                // Initialize the SqlCommand with the new SQL string and the connection information.
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Connection.Open();
                    using (SqlDataReader reader_admin = cmd.ExecuteReader())
                    {
                        if (reader_admin.HasRows)
                        {
                            try
                            {
                                // Lets now read in the data
                                while (reader_admin.Read())
                                {
                                    // Add a new row to the datatable
                                    dtNewRow = dt.NewRow();


                                    dtNewRow["POLICY"] = reader_admin["POLICY"].ToString();
                                    dtNewRow["FULLNM"] = reader_admin["FULLNM"].ToString().ToUpper();
                                    dtNewRow["POLDATE"] = System.DateTime.Today;
                                    dtNewRow["TYPE"] = reader_admin["TYPE"].ToString();
                                    dtNewRow["DATEREC"] = System.DateTime.Today;
                                    dtNewRow["AMT_PD"] = reader_admin["AMT_PD"];
                                    dtNewRow["YEAR"] = reader_admin["YEAR"];
                                    dtNewRow["COM_PCT"] = reader_admin["COM_PCT"];
                                    dtNewRow["AGENTPCT"] = reader_admin["AGENTPCT"];
                                    dtNewRow["COM1ST"] = reader_admin["COM1ST"];
                                    dtNewRow["COMREN"] = reader_admin["COMREN"];
                                    dtNewRow["COMSP"] = reader_admin["COMSP"];
                                    dtNewRow["COMANN"] = reader_admin["COMANN"];
                                    dtNewRow["LODGE"] = reader_admin["LODGE"].ToString();
                                    dt.Rows.Add(dtNewRow);

                                    // Add up 1st Year Life
                                    try
                                    {
                                        COM1ST_Total = COM1ST_Total + Convert.ToDecimal(reader_admin["COM1ST"]);
                                    }
                                    catch
                                    {
                                        COM1ST_Total = 0;
                                    }
                                    // Add up Renewal Life
                                    try
                                    {
                                        COMREN_Total = COMREN_Total + Convert.ToDecimal(reader_admin["COMREN"]);
                                    }
                                    catch
                                    {
                                        COMREN_Total = 0;
                                    }
                                    // Add up Single Premium
                                    try
                                    {
                                        COMSP_Total = COMSP_Total + Convert.ToDecimal(reader_admin["COMSP"]);
                                    }
                                    catch
                                    {
                                        COMSP_Total = 0;
                                    }
                                    // Add up Annuity
                                    try
                                    {
                                        COMANN_Total = COMANN_Total + Convert.ToDecimal(reader_admin["COMANN"]);
                                    }
                                    catch
                                    {
                                        COMANN_Total = 0;
                                    }



                                    // Add to datatable dt
                                    ////dt.Rows.Add(reader_admin["AGENT"].ToString(), reader_admin["POLICY"].ToString(), reader_admin["FULLNM"].ToString().ToUpper(), reader_admin["POLDATE"], reader_admin["TYPE"], reader_admin["DATEREC"], reader_admin["AMT_PD"], reader_admin["YEAR"], reader_admin["COM_PCT"], reader_admin["AGENTPCT"], reader_admin["COM1ST"], reader_admin["COMREN"], reader_admin["COMSP"], reader_admin["COMANN"], reader_admin["LODGE"]);
                                } // End of While routine
                            }
                            catch (Exception err)
                            {
                                // This will need to be put in the error log
                                var errormessage = err;
                                //Response.Write("This is the error message for State selection: " + err);
                                // Set the Pass or Fail varibl
                                //pass = false;
                            }
                        }
                    }
                }
            }
            ViewState["dt"] = dt;
            //dt.DefaultView.Sort = "lname";

            gv_commissions.DataSource = ViewState["dt"] as DataTable;
            //grid.DataSource = dt;
            //gv_commissions.DataBind();
            gv_commissions.Visible = true;

            // Define the footer
            gv_commissions.FooterRow.Cells[1].Text = "Total Commission: ";

            // Calculate the total for COM1ST
            gv_commissions.FooterRow.Cells[9].Text = COM1ST_Total.ToString("c");
            // Calculate the total for Renewal Life
            gv_commissions.FooterRow.Cells[10].Text = COMREN_Total.ToString("c");
            // Calculate the total for Single Premium
            gv_commissions.FooterRow.Cells[11].Text = COMSP_Total.ToString("c");
            // Calculate the total for Annuity
            gv_commissions.FooterRow.Cells[12].Text = COMANN_Total.ToString("c");
            // Calculate the total Commissions
            gv_commissions.FooterRow.Cells[2].Text = (COM1ST_Total + COMREN_Total + COMSP_Total + COMANN_Total).ToString("c");

        }

        protected void Gridpaging(object sender, GridViewPageEventArgs e)
        {
            gv_commissions.PageIndex = e.NewPageIndex;
            gv_commissions.DataSource = ViewState["Paging"];
            gv_commissions.DataBind();


        }

        //-------------------------------------------------------------------------------------------------------------------
        // Get the Agent Name and return it
        //-------------------------------------------------------------------------------------------------------------------
        protected string getAgentName()
        {
            // Define the vars
            number = Session["Agent"].ToString();
            // Get the agent information
            string query = string.Format(@"SELECT agtparent.agent, agtparent.agtname FROM agtparent WHERE agtparent.agent = '" + number + "' order by agtname");

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                // Initialize the SqlCommand with the new SQL string and the connection information.
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Connection.Open();
                    using (SqlDataReader reader_admin = cmd.ExecuteReader())
                    {
                        if (reader_admin.HasRows)
                        {
                            try
                            {
                                // Lets now read in the data
                                while (reader_admin.Read())
                                {

                                    agent_name = reader_admin["AGTNAME"].ToString();
                                } // End of While routine
                            }
                            catch (Exception err)
                            {
                                // This will need to be put in the error log
                                var errormessage = err;
                                //Response.Write("This is the error message for State selection: " + err);
                                // Set the Pass or Fail varibl
                                //pass = false;
                            }
                        }
                    }
                }
            }
            return agent_name;

        }
        //-------------------------------------------------------------------------------------------------------------------
        // End - Get the Agent Name and return it
        //-------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------
        // Populate the Agent Header Information
        //-------------------------------------------------------------------------------------------------------------------
        protected void getAgentHeader(string agentNumber)
        {
            // Get the agent information
            //string query = string.Format(@"SELECT agtparent.agent, agtparent.agtname FROM agtparent WHERE agtparent.agent = '" + number + "' order by agtname");
            string query = string.Format(@"SELECT * FROM [dbo].[tbl_agent_commissions] WHERE AGENT = '" + agentNumber + "' order by daterec desc");

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                // Initialize the SqlCommand with the new SQL string and the connection information.
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Connection.Open();
                    using (SqlDataReader reader_admin = cmd.ExecuteReader())
                    {
                        if (reader_admin.HasRows)
                        {
                            try
                            {
                                // Lets now read in the data
                                while (reader_admin.Read())
                                {
                                    // Create the space before the address
                                    Spacer1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    Spacer2.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                                    head_number.Text = reader_admin["AGENT"].ToString();
                                    head_name.Text = reader_admin["AGENTNM"].ToString();
                                    head_Add1.Text = reader_admin["AGTADD1"].ToString();
                                    head_Add2.Text = reader_admin["AGTADD2"].ToString();
                                } // End of While routine
                            }
                            catch (Exception err)
                            {
                                // This will need to be put in the error log
                                var errormessage = err;
                                //Response.Write("This is the error message for State selection: " + err);
                                // Set the Pass or Fail varibl
                                //pass = false;
                            }
                        }
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        // End - Populate the Agent Header Information
        //-------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------
        //
        // Populate the agent drop down box
        //
        //-------------------------------------------------------------------------------------------

        protected void load_agentDropDown()
        {
            // Configure the variables
            loggedInNumber = Session["loggedInNumber"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            string updateSqlStatement;

            updateSqlStatement = "SELECT agtparent.agent, agtparent.agtname FROM agtparent WHERE agtparent.agtparent = '" + loggedInNumber + "' order by agtname";

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(updateSqlStatement, myConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                myConnection.Close();

                agentDropdown.DataSource = ds.Tables[0].DefaultView;
                agentDropdown.DataTextField = "agtname";
                agentDropdown.DataValueField = "agent";
                agentDropdown.DataBind();
            }

            agentDropdown.SelectedValue = loggedInNumber;
        }
        //-------------------------------------------------------------------------------------------
        //
        // End - Populate the agent drop down box
        //
        //-------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------
        //
        // Routine that will get the new agent from the pull down
        //
        //-------------------------------------------------------------------------------------------
        protected void agentDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected item in the pull down menu
            string agent_name = agentDropdown.SelectedItem.ToString();
            string agent_value = agentDropdown.SelectedValue.ToString();
            Session["Agent"] = agent_value;
            number = agent_value;

            // Update the agent report pull down
            load_agentReports(number);

            // Display the Agent Number
            //head_number.Text = number + "&nbsp;&nbsp;";
            // Get the Agent Name
            //head_name.Text = getAgentName();
            getAgentHeader(number);
        }
        //-------------------------------------------------------------------------------------------
        //
        // Routine that will get the new agent from the pull down
        //
        //-------------------------------------------------------------------------------------------
    
    }
}