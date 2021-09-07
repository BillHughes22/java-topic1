<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="agentMain.aspx.cs" Inherits="FcsuAgentWebApp.Agent.policyView" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register namespace="AjaxControlToolkit" assembly="AjaxControlToolkit"  tagprefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" overflow="auto">

 

<input type="hidden" name="keep" id="keep" />

    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
   <%-- <div id="divTest" style="max-height: 173px; border-bottom-style: none; border-bottom-color: inherit; border-bottom-width: 1px;" class="auto-style1" >--%>
        <asp:Label ID="Label1" runat="server" Text="Policies Sold by Agent:" class="noprint"></asp:Label>
        <asp:Label ID="LabelNumber" runat="server"></asp:Label>&nbsp;<asp:Label ID="Label4" runat="server" Text="SearchByNames:"></asp:Label><asp:TextBox ID="TextBox2" runat="server" Height="16px" onkeyup="SetContextKey()" Width="115px"></asp:TextBox> 
    <script type = "text/javascript">
       

   </script> 
    <ajaxToolkit:AutoCompleteExtender ID="TextBox2_AutoCompleteExtender"
        runat="server" BehaviorID="TextBox2_AutoCompleteExtender" 
        CompletionInterval="100" CompletionSetCount="10" DelimiterCharacters="" 
        EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" 
        ServiceMethod="searchNames" ServicePath="" TargetControlID="TextBox2" 
        UseContextKey="true">
    </ajaxToolkit:AutoCompleteExtender>
   
   &nbsp; Rows to show on Grid: <asp:TextBox ID="txtSize" runat="server" CssClass="TextBox" Width="30px" 
          AutoPostBack="true" OnTextChanged="txtSize_TextChanged1"></asp:TextBox><button type="submit" value="Submit">Submit
                                                                                 </button> &nbsp;
    <input id="Button1" type="button" value="Print Screen" class="auto-style2" onclick="window.print();" />
  

     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true">
    </asp:ScriptManager>
   
    <%--   Policies Gridview--%>
    <%--AutoGenerateSelectButton="True"--%>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True"  
                SelectedRowStyle-BackColor="#a46cc0" SelectedRowStyle-ForeColor="Gold"
            AutoGenerateColumns="False" 
            BorderStyle="Inset" BorderWidth="5px" BorderColor="#4B6C9E" BackColor="White"
            HeaderStyle-BackColor="BlanchedAlmond" RowStyle-BorderColor="Brown" 
                HorizontalAlign="Center" Width="983px" style="margin-top: 0px" 
              onselectedindexchanged="GridView1_SelectedIndexChanged"  onpageindexchanging="GridView1_PageIndexChanging"
              onSorting="GridView1_Sorting" CurrentSortField="Policy" CurrentSortDirection="ASC"
                DataKeyNames="POLICY" 
                ondatabound="GridView1_DataBound" AllowPaging="True"  PageSize="5" CssClass="noprint"  Height="186px" >
            <Columns >
                 <asp:buttonfield buttontype="Button" commandname="Select" headertext="Select" text="Select"/>
                <asp:BoundField DataField="POLICY" HeaderText="POLICY" SortExpression="POLICY" ItemStyle-Width="65px" >
                    <ItemStyle Width="65px"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="LASTNAME" ItemStyle-Width="200px">
                    <ItemStyle Width="200px"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" SortExpression="ADDRESS" ItemStyle-Width="400px" ItemStyle-Font-Size = "X-Small">
                    <ItemStyle Font-Size="X-Small" Width="400px"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="PLANTYPE" HeaderText="PLAN" SortExpression="PLANTYPE" ItemStyle-Width="150px" ItemStyle-Font-Size ="X-Small">
                    <ItemStyle Font-Size="X-Small" Width="150px"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="VALUE" HeaderText="FACE AMOUNT" SortExpression="VALUE" ItemStyle-Width="60px" ItemStyle-Font-Size ="Small">
                    <ItemStyle Font-Size="Small" Width="60px"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="BEGBAL" HeaderText="BEGBAL" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                    SortExpression="BEGBAL" Visible="True" ></asp:BoundField>
                <asp:BoundField DataField="YTD_INT" HeaderText="YTD_INT" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" 
                    SortExpression="YTD_INT" Visible="True" ></asp:BoundField>
                <asp:BoundField DataField="ANNRATE" HeaderText="ANNRATE" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                    SortExpression="ANNRATE" Visible="True" ></asp:BoundField>
                <asp:BoundField DataField="CURBAL" HeaderText="BALANCE" SortExpression="CURBAL" ItemStyle-Width="110px" DataFormatString ="{0:C}" ItemStyle-Font-Size ="Small">
                    <ItemStyle Font-Size="Small" Width="110px"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="POLDATE" HeaderText="ISSUE DATE" SortExpression="POLDATE" ItemStyle-Width="110px" DataFormatString ="{0:d}" ItemStyle-Font-Size ="Small">
                    <ItemStyle Font-Size="Small" Width="110px"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" ItemStyle-Width="70px" ItemStyle-Font-Size ="Small">
                    <ItemStyle Font-Size="Small" Width="70px"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="PHONE" HeaderText="PHONE" SortExpression="PHONE" 
                    Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" SortExpression="EMAIL" 
                    Visible="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                <asp:BoundField DataField="DOB" HeaderText="DOB" SortExpression="DOB" 
                    Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                <asp:BoundField DataField="MEMBERDT" HeaderText="MEMBERDT" 
                    SortExpression="MEMBERDT" Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                <asp:BoundField DataField="LASTNAME" HeaderText="LASTNAME"
                    SortExpression="LASTNAME" Visible="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                <asp:BoundField DataField="MATDATE" HeaderText="matdate" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" 
                    SortExpression="matdate" Visible="True" ></asp:BoundField>
                <asp:BoundField DataField="MODE" HeaderText="mode" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" 
                    SortExpression="mode" Visible="True" ></asp:BoundField>
                <asp:BoundField DataField="BASEPREM" HeaderText="baseprem" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" 
                    SortExpression="baseprem" Visible="True" ></asp:BoundField>
                <asp:BoundField DataField="UPDATEDT" HeaderText="updatedt" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" 
                    SortExpression="updatedt" Visible="True" >
                </asp:BoundField>
                 <asp:BoundField DataField="pl_spia" HeaderText="pl_spia" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="pl_spia" Visible="True">
                </asp:BoundField>
              
                 <asp:BoundField DataField="oname" HeaderText="ownername" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="oname" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            </Columns>

            <FooterStyle Height="0px" />
            <HeaderStyle BackColor="#FFFFCC"></HeaderStyle>
            <RowStyle BorderColor="Brown"></RowStyle>
            <SelectedRowStyle BackColor="#CCFFFF" ForeColor="#660033"></SelectedRowStyle>
        </asp:GridView>
   &nbsp;

<%--   Data Source for Selecting Annuity Summary Info.  --%>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
        SelectCommand="SELECT * FROM [annsum] WHERE ([policy] = @policy)">
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="policy" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

<%--   Labels for the bottom of the Grid--%>
    
<%--   Policy Information--%>
    <asp:Table ID="Table1" runat="server" BorderWidth="0" GridLines="None" Visible="False" Width="1000px" EnableViewState ="false">
        <asp:TableRow Width="100%" EnableViewState ="false">
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelName" runat="server" Text="Name:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="30%" ColumnSpan="6" EnableViewState ="false"><asp:TextBox ID="TextBoxName" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelAddress" runat="server" Text="Address:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="50%" ColumnSpan="10" EnableViewState ="false"><asp:TextBox ID="TextBoxAddress" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
 
        <asp:TableRow Width="100%" EnableViewState ="false">
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelPhone" runat="server" Text="Phone:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="30%" ColumnSpan="6" EnableViewState ="false"><asp:TextBox ID="TextBoxPhone" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelEmail" runat="server" Text="Email:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="50%" ColumnSpan="10" EnableViewState ="false"><asp:TextBox ID="TextBoxEmail" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Width="100%" EnableViewState="false">
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="LabelOwner" runat="server" Text="Owner:" Style="color: navy; text-align: right" Width="100%" eColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="90%" ColumnSpan="18" EnableViewState="false">
                <asp:TextBox ID="TextBoxOwner" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Width="100%" EnableViewState ="false">
            <asp:TableCell Width="100%" ColumnSpan="20" EnableViewState ="false"><hr width="95%" align="center"  /></asp:TableCell>
        </asp:TableRow>
 
        <asp:TableRow Width="100%" EnableViewState ="false">
            <asp:TableCell Width="20%" ColumnSpan="4" EnableViewState ="false"> <asp:Label ID="Label2" runat="server" Width="20%" align="center"> </asp:Label></asp:TableCell>
            <asp:TableCell Width="60%" ColumnSpan="12" EnableViewState ="false"> <center><asp:Label ID="polinfo" runat="server" Width="100%" align="center" Font-Size="18" Font-Underline="true">Information</asp:Label></center></asp:TableCell>
            <asp:TableCell Width="20%" ColumnSpan="4" EnableViewState ="false"> <asp:Label ID="Label3" runat="server" Width="20%" align="center"> </asp:Label></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow Width ="100%" EnableViewState ="false">
            <asp:TableCell Width="10%"  ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelPolicy" runat="server"  Text="Certificate#:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="10%"  ColumnSpan="2" EnableViewState ="false"><asp:TextBox ID="TextBoxPolicy" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
            <asp:TableCell Width="10%"  ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelPlan" runat="server"  Text="Plan:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="30%" ColumnSpan="6" EnableViewState ="false"><asp:TextBox ID="TextBoxPlan" runat="server" Width="100%" align="right" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelBirthDate" runat="server" Text="Birth Date:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:TextBox ID="TextBoxBirthDate" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelMatDate" runat="server" Text="Maturity Date:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:TextBox ID="TextBoxMatDate" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow Width="100%" EnableViewState ="false">
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelValue" runat="server" Text="Face Amount:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:TextBox ID="TextBoxValue" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelPremium" runat="server" Text="Premium:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:TextBox ID="TextBoxPremium" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelIssueDate" runat="server" Text="Issue Date:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:TextBox ID="TextBoxIssueDate" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState ="false"><asp:Label ID="LabelCurrentRate" runat="server" Text="Current Rate:" style="text-align:right" Width="100%" ForeColor="Navy" EnableViewState ="false"></asp:Label></asp:TableCell>
            <asp:TableCell Width="30%" ColumnSpan="6" EnableViewState ="false"><asp:TextBox ID="TextBoxCurrentRate" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>

    </asp:Table><br />
     <asp:Label ID="RiderLabel" runat="server" Text="Rider Information:" 
        Visible="false" Font-Size="Medium"></asp:Label>

    <%--   Labels for the bottom of the Grid--%>

 <asp:GridView ID="GridView4" runat="server"  AutoGenerateColumns="False" Height="26px" Width="16px">
        <Columns>
            <asp:BoundField DataField="policy" HeaderText="Policy" ReadOnly="true" SortExpression="policy" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="riderprem1" HeaderText="Premium1" ReadOnly="true" SortExpression="riderprem1" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="ridertype1" HeaderText="Type1" ReadOnly="true" SortExpression="ridertype1" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="riderprem2" HeaderText="Premium2" ReadOnly="true" SortExpression="riderprem2" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="ridertype2" HeaderText="Type2" ReadOnly="true" SortExpression="ridertype2" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="riderprem3" HeaderText="Premium3" ReadOnly="true" SortExpression="riderprem3" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="ridertype3" HeaderText="Type3" ReadOnly="true" SortExpression="ridertype3" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center"/>
            
        </Columns>
   </asp:GridView>  

    <br />
&nbsp;<%--   Policy Information--%><%--<asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT policy, riderprem1,ridertype1,riderprem2,ridertype2,riderprem3,ridertype3 FROM policy where policy=@policyNum and (riderprem1 > 00.00  or riderprem2 > 00.00 or  riderprem3 > 00.00)">
        
         <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxPolicy" Name="policyNum" DefaultValue=""/>
        </SelectParameters>
       </asp:SqlDataSource>--%><asp:Label ID="BenefLabel" runat="server" Text="Beneficiary Information:" 
        Visible="false" Font-Size="Medium"></asp:Label>

   
    <%--   Labels for the bottom of the Grid--%>
<asp:GridView ID="GridView3" runat="server"  AutoGenerateColumns="False" Height="34px" Width="491px">
        <Columns>
            <asp:BoundField DataField="policy" HeaderText="Policy" ReadOnly="true" SortExpression="policy" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="bname" HeaderText="Name" ReadOnly="true" SortExpression="bname" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="relate" HeaderText="Relation" ReadOnly="true" SortExpression="relate" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="btype" HeaderText="Type" ReadOnly="true" SortExpression="btype" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"/>
            
            
        </Columns>
   </asp:GridView>  

    <%--   Labels for the bottom of the Grid--%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <%--   Policy Information--%>
    
    <br/>
    

    <div style="width:48%; float:left; height:260px;">

        <center>
        <asp:Label ID="Summary" runat="server"  Font-Size="14" Visible="False" EnableViewState ="false">Summary</asp:Label>
        </center>

        
<%--   Annuity Summary Information--%>
        <asp:Table ID="Table2" runat="server" Visible="False" HorizontalAlign="Center" EnableViewState ="false">
            <asp:TableRow runat="server" EnableViewState ="false">
                <asp:TableCell runat="server" EnableViewState ="false">
                    <asp:Label ID="LabelPrevBal" runat="server" Text="Prev. Balance" EnableViewState ="false"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server" EnableViewState ="false">
                    <asp:TextBox ID="TextBoxPrevBal" runat="server" style="text-align:right;" EnableViewState ="false" readonly></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" EnableViewState ="false">
                <asp:TableCell runat="server" EnableViewState ="false">
                    <asp:Label ID="LabelDeposits" runat="server" Text="Deposits" EnableViewState ="false"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server" EnableViewState ="false">
                    <asp:TextBox ID="TextBoxDeposits" runat="server" style="text-align:right;" EnableViewState ="false" readonly></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" EnableViewState ="false">
                <asp:TableCell runat="server" EnableViewState ="false">
                    <asp:Label ID="LabelWithdrawals" runat="server" Text="Withdrawals" EnableViewState ="false"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server" EnableViewState ="false">
                    <asp:TextBox ID="TextBoxWithdrawals" runat="server" style="text-align:right;" EnableViewState ="false" readonly></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" EnableViewState ="false">
                <asp:TableCell runat="server" EnableViewState ="false">
                    <asp:Label ID="LabelInterest" runat="server" Text="Interest" EnableViewState ="false"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server" EnableViewState ="false">
                    <asp:TextBox ID="TextBoxInterest" runat="server" style="text-align:right;" EnableViewState ="false" readonly></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" EnableViewState ="false">
                <asp:TableCell runat="server" EnableViewState ="false">
                    <asp:Label ID="LabelCurBal" runat="server" Text="Cur. Balance" EnableViewState ="false"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server" EnableViewState ="false">
                    <asp:TextBox ID="TextBoxCurBal" runat="server" style="text-align:right;" EnableViewState ="false" readonly></asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table> 

    </div>
    

    <div style="width:48%; margin-left:1%; float:right; height:260px; overflow:auto;">
    <center>
    <asp:Label ID="Transactions" runat="server"  Font-Size="14" Visible="False" EnableViewState ="false">Transactions for last 12 months</asp:Label>
    </center>


 <%--   Annuity Transaction Gridview--%>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            Visible="False" HorizontalAlign="Center" >
        <Columns>
            <asp:BoundField DataField="policy" HeaderText="policy" ReadOnly="True" 
                SortExpression="policy" Visible="False" />
            <asp:BoundField DataField="trandate" HeaderText="Date" ReadOnly="True" 
                SortExpression="trandate" DataFormatString ="{0:d}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="amount" HeaderText="Amount" ReadOnly="True" 
                SortExpression="amount" DataFormatString ="{0:N2}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="descr" HeaderText="Description" ReadOnly="True" 
                SortExpression="descr" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>

    </div>


<%--   Data Source for Annuity Transaction Gridview--%>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT policy, trandate, amount, descr FROM deposit WHERE (year = YEAR({ fn NOW() }))
        union SELECT policy, trandate, amount, descr FROM withdrl WHERE (year = YEAR({ fn NOW() }))
        ORDER BY trandate"></asp:SqlDataSource>
 
 <asp:TextBox ID="keepBox" visible="false" runat="server" ></asp:TextBox>
<asp:Table ID = "getAnnBalTable" runat="server" Width="100%" visible="false">
 <asp:TableRow Width="100%">
  <asp:TableCell id="cell_label1" Width="30%" BorderWidth="1px" HorizontalAlign="Center"> <asp:Label ID="annGetBalLabel1" runat="server" Text="Get past balances(Last 2 years)" Font-Size="Large"></asp:Label>&nbsp </asp:TableCell>
  <asp:TableCell id="cell_label2" Width="18%"  HorizontalAlign="Right"> <asp:Label ID="annGetBalLabel2" runat="server" Text="Enter date (mm/dd/yyyy):"></asp:Label> </asp:TableCell>
  
  <asp:TableCell id="cell_date" Width="3%"  VerticalAlign="Middle"><asp:TextBox ID="calcDateMonth" runat="server" Text="" Width="100%" MaxLength="2" on></asp:TextBox></asp:TableCell>
     <asp:TableCell ID="cell_date2" Width="2%" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="X-Large">/</asp:TableCell>
     <asp:TableCell ID="cell_date3" Width="3%" VerticalAlign="Middle"><asp:TextBox ID="calcDateDay" runat="server" Width="100%" Text="" MaxLength="2"></asp:TextBox></asp:TableCell>
     <asp:TableCell ID="cell_date4" Width="2%" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="X-Large">/</asp:TableCell>
     <asp:TableCell ID="cell_date5" Width="7%" VerticalAlign="Middle"><asp:TextBox ID="calcDateYear" runat="server" Text=""  Width="70%" MaxLength="4"></asp:TextBox></asp:TableCell>

  <asp:TableCell id="cell_getButton" Width="12%" HorizontalAlign="Center"> <asp:Button ID="getAnnBalance" runat="server" Text="Get Balance" OnClick="getAnnBalance_CLick" /> </asp:TableCell>
  <asp:TableCell id="cell_result" Width="23%" HorizontalAlign="Left">&nbsp&nbsp&nbsp <asp:TextBox ID="gottenAnnBalance" runat="server" Text="" Width="85%" style="text-align:right;"></asp:TextBox> </asp:TableCell>
 </asp:TableRow>
</asp:Table>
      <script type="text/javascript">
          function myFunction() {

              var str = document.documentElement.innerHTML;

              // get rid of grid1
              var nPos1 = str.indexOf("<div id=\"divTest");
              var str7 = str.substr(nPos1);
              var nPos2 = str7.indexOf("</div");
              var nPos3 = nPos1 + nPos2 + 32;

              var str1 = str.substr(0, nPos1 - 18);
              var str2 = str.substr(nPos3);
              // get rid of table of labels under grid1
              var nPos4 = str2.indexOf("</table");
              var str4 = str2.substr(nPos4 + 8);
              var str5 = str1.concat(str4);
              // creates new webpage
              var yourDOCTYPE = "";
              var WinPrint = window.open('', '', 'letf=0,top=0,toolbar=0,scrollbars=0,status=0,menubar=0,titlebar=0');
              WinPrint.document.write(yourDOCTYPE + "<html>" + str5 + "</html>");
              WinPrint.document.close();
              WinPrint.focus();
              WinPrint.print();
              WinPrint.close();
          }
</script>
</asp:Content>

<%--<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style2 {
            margin-bottom: 0px;
        }
    </style>

  
</asp:Content>--%>

<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style2 {
            width: 109px;
        }

 
        </style>
</asp:Content>


