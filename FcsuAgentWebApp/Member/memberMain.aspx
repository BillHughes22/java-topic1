<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="memberMain.aspx.cs" 
    Inherits="FcsuAgentWebApp.Member.memberMain" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
 <%--   Policies Gridview--%>
   

    <%--   Policies Gridview--%>
   
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" overflow="scroll">
     <script src="../Scripts/jquery-1.4.1.js" type="text/javascript">
        
     </script>

    <%--   Policies Gridview--%>
   <input type="hidden" name="keep" id="keep" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   

   

     <asp:Label ID="Lbl_update" runat="server" Text=""></asp:Label>
    &nbsp;<asp:Label ID="Label1" runat="server" Text="Member Number:" class="noprint"></asp:Label>
    &nbsp;<asp:Label ID="LabelNumber" runat="server"></asp:Label>
    &nbsp;<asp:DropDownList ID="memberDropdown" runat="server" DataTextField="membername" DataValueField="cst_num" Width="14%" AutoPostBack="True" Height="22px"></asp:DropDownList>
    &nbsp;<%--<asp:Label ID="Label4" runat="server" Text="Search By Member Name:"></asp:Label>--%>
  
 <%--<asp:TextBox ID="TextBox1" runat="server" Height="17px" Width="132px"></asp:TextBox>--%>
    <%--<ajaxtoolkit:AutoCompleteExtender
        ID="TextBox1_AutoCompleteExtender" 
        ServiceMethod="searchNames" 
        FirstRowSelected = "false" 
        UseContextKey = "true" 
        runat="server"  
        CompletionInterval="100" 
        EnableCaching="false" 
        CompletionSetCount="10" 
        TargetControlID="TextBox1" 
        BehaviorID="TextBox1_AutoCompleteExtender" 
        DelimiterCharacters="" ServicePath="" MinimumPrefixLength="1">
    </ajaxtoolkit:AutoCompleteExtender>
   --%>

    &nbsp;
   

   <%-- <button type="submit" value="Submit" style="height: 25px; width: 55px">Submit</button>
    &nbsp;Rows to show on Grid: <asp:TextBox ID="txtSize" runat="server" CssClass="TextBox" Width="30px" 

          AutoPostBack="true" OnTextChanged="txtSize_TextChanged1"></asp:TextBox>--%> &nbsp;&nbsp;
    <button onclick="window.print()">Print this page</button>

    <%--class="noprint" removed--%>
     <%--AutoGenerateSelectButton="True"--%>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" DataSourceID="SqlDataSource1"
        SelectedRowStyle-BackColor="#a46cc0" SelectedRowStyle-ForeColor="Gold"
        AutoGenerateColumns="False"
        BorderStyle="Inset" BorderWidth="1px" BorderColor="#4B6C9E" BackColor="White" 
        RowStyle-BorderColor="brown" 
        HorizontalAlign="Center" Width="100%" Style="margin-left: 0px;"  
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="3"
        DataKeyNames="POLICY" onrowcommand="GridView1_RowCommand" 
         AllowPaging="True" Height="171px" PageSize="3" Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#ddb547"><tr height="30px"><td style="font-weight: bold;">LIFE INSURANCE</td></tr></table>'>
        <Columns>
              <asp:buttonfield ControlStyle-CssClass="button" buttontype="Button" commandname="Select" headertext="Select" text="Select" />
               <asp:BoundField  DataField="POLICY" HeaderText="POLICY" SortExpression="POLICY" ItemStyle-Width="140px">
                <ItemStyle Width="166px" ></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="LASTNAME" ItemStyle-Width="200px">
                <ItemStyle Width="183px"></ItemStyle>
            </asp:BoundField>
          <%-- ItemStyle-Font-Size="X-Small"--%>
            <%--Font-Size="X-Small" Width="500px"--%>
            <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" SortExpression="ADDRESS" ItemStyle-Width="490px" >
                <ItemStyle Width="704px" ></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PLANTYPE" HeaderText="PLAN"
                SortExpression="PLANTYPE" ItemStyle-Width="150px" >
                <ItemStyle  Width="148px" ></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="VALUE" HeaderText="FACE AMOUNT"
                SortExpression="VALUE" ItemStyle-Width="60px"  DataFormatString="{0:C}" >
                <ItemStyle  Width="190px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="BEGBAL" HeaderText="BEGBAL" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="BEGBAL" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="YTD_INT" HeaderText="YTD_INT" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="YTD_INT" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ANNRATE" HeaderText="ANNRATE" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="ANNRATE" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="CURBAL" HeaderText="BALANCE" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="CURBAL" ItemStyle-Width="110px" DataFormatString="{0:C}"  Visible="True"
                ItemStyle-Font-Size="Small">
                <ItemStyle Font-Size="Small" Width="110px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="POLDATE" HeaderText="ISSUE DATE"
                SortExpression="POLDATE" ItemStyle-Width="110px" DataFormatString="{0:d}"
                >
                <ItemStyle Width="77px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="STATUS" HeaderText="STATUS"
                SortExpression="STATUS" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" ItemStyle-Width="70px">
                <ItemStyle  Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PHONE" HeaderText="PHONE" SortExpression="PHONE"
                Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" SortExpression="EMAIL"
                Visible="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="DOB" HeaderText="DOB" SortExpression="DOB"
                Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MEMBERDT" HeaderText="MEMBERDT"
                SortExpression="MEMBERDT" Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="LASTNAME" HeaderText="LASTNAME"
                SortExpression="LASTNAME" Visible="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MATDATE" HeaderText="matdate" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="matdate" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MODE" HeaderText="mode" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="mode" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="BASEPREM" HeaderText="baseprem" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="baseprem" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="UPDATEDT" HeaderText="updatedt" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="updatedt" Visible="True" DataFormatString="{0:d}">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

            <asp:BoundField DataField="LIEN" HeaderText="lien" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="lien" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

            <asp:BoundField DataField="LOAN" HeaderText="loan" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="loan" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="CASHVAL" HeaderText="cashval" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="cashval" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PUADIV" HeaderText="puadiv" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="puadiv" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ACCUMDIV" HeaderText="accumdiv" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="accumdiv" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="TOTDEATH" HeaderText="totdeath" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="totdeath" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="oname" HeaderText="ownername" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="oname" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="paidto" HeaderText="paidto" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="paidto" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="rmdcurr" HeaderText="rmdcurr" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="rmdcurr" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="rmdprev" HeaderText="rmdprev" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="rmdprev" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

        </Columns>

        <FooterStyle Height="0px" />
        <HeaderStyle BackColor="#ddb547"></HeaderStyle>
        <PagerStyle BorderWidth="1px" />
        <RowStyle BorderColor="Brown"></RowStyle>
        <SelectedRowStyle BackColor="white" ForeColor="#660033"></SelectedRowStyle>
    </asp:GridView>
    <br />
    <asp:GridView ID="GridAnn" runat="server" AllowSorting="True" DataSourceID="SqlDataSourceAnn"
        SelectedRowStyle-BackColor="#a46cc0" SelectedRowStyle-ForeColor="Gold"
        AutoGenerateColumns="False"  CellPadding="3"
        BorderStyle="Inset" BorderWidth="1px" BorderColor="#4B6C9E" BackColor="White"
        RowStyle-BorderColor="Brown"
        HorizontalAlign="Center"  Style="margin-left: 0px;"
        OnSelectedIndexChanged="GridAnn_SelectedIndexChanged"
        DataKeyNames="POLICY" onrowcommand="GridAnn_RowCommand" 
         OnSorted="GridAnn_Sorted"  AllowPaging="True" Height="171px" PageSize="3" Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#ddb547"><tr height="30px"><td style="font-weight: bold;">ANNUITY</td></tr></table>'>
        <Columns>
              <asp:buttonfield buttontype="Button" commandname="Select" headertext="Select" text="Select" ControlStyle-CssClass="button"/>
            <asp:BoundField  DataField="POLICY" HeaderText="POLICY" SortExpression="POLICY" ItemStyle-Width="164px">
                <ItemStyle Width="214px" ></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="LASTNAME" ItemStyle-Width="250px">
                <ItemStyle Width="239px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" SortExpression="ADDRESS" ItemStyle-Width="450px">
                <ItemStyle Width="681px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PLANTYPE" HeaderText="PLAN" SortExpression="PLANTYPE" ItemStyle-Width="150px" >
                <ItemStyle  Width="354px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="VALUE" HeaderText="INITIAL DEPOSIT"
                SortExpression="VALUE" ItemStyle-Width="60px"  DataFormatString="{0:C}" >
                <ItemStyle  Width="160px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="BEGBAL" HeaderText="BEGBAL" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="BEGBAL" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="YTD_INT" HeaderText="YTD_INT" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="YTD_INT" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ANNRATE" HeaderText="ANNRATE" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="ANNRATE" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="CURBAL" HeaderText="CURRENT BALANCE"
                SortExpression="CURBAL" ItemStyle-Width="110px" DataFormatString="{0:C}"
               >
                <ItemStyle Width="110px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="POLDATE" HeaderText="ISSUE DATE"
                SortExpression="POLDATE" ItemStyle-Width="110px" DataFormatString="{0:d}"
                >
                <ItemStyle Width="110px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="STATUS" HeaderText="STATUS"
                SortExpression="STATUS" ItemStyle-Width="70px" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <ItemStyle  Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PHONE" HeaderText="PHONE" SortExpression="PHONE"
                Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" SortExpression="EMAIL"
                Visible="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="DOB" HeaderText="DOB" SortExpression="DOB"
                Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MEMBERDT" HeaderText="MEMBERDT"
                SortExpression="MEMBERDT" Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="LASTNAME" HeaderText="LASTNAME"
                SortExpression="LASTNAME" Visible="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MATDATE" HeaderText="matdate" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="matdate" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MODE" HeaderText="mode" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="mode" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="BASEPREM" HeaderText="baseprem" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="baseprem" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="UPDATEDT" HeaderText="updatedt" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="updatedt" Visible="True" DataFormatString="{0:d}">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

            <asp:BoundField DataField="LIEN" HeaderText="lien" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="lien" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

            <asp:BoundField DataField="LOAN" HeaderText="loan" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="loan" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="CASHVAL" HeaderText="cashval" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="cashval" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PUADIV" HeaderText="puadiv" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="puadiv" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ACCUMDIV" HeaderText="accumdiv" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="accumdiv" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="TOTDEATH" HeaderText="totdeath" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="totdeath" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="oname" HeaderText="ownername" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="oname" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="paidto" HeaderText="paidto" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="paidto" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

            <asp:BoundField DataField="rmdcurr" HeaderText="rmdcurr" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="rmdcurr" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="rmdprev" HeaderText="rmdprev" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="rmdprev" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

        </Columns>

        <FooterStyle Height="0px" />
        <HeaderStyle BackColor="#ddb547"></HeaderStyle>
        <PagerStyle BorderWidth="1px" />
        <RowStyle BorderColor="Brown"></RowStyle>
        <SelectedRowStyle BackColor="white" ForeColor="#660033"></SelectedRowStyle>
    </asp:GridView>

    <asp:GridView ID="GridSetlmt" runat="server" AllowSorting="True" DataSourceID="SqlDataSourceSetlmt"
        SelectedRowStyle-BackColor="#a46cc0" SelectedRowStyle-ForeColor="Gold"
        AutoGenerateColumns="False"  CellPadding="3"
        BorderStyle="Inset" BorderWidth="1px" BorderColor="#4B6C9E" BackColor="White"
        RowStyle-BorderColor="Brown"
        HorizontalAlign="Center"  Style="margin-left: 0px;"
        
        DataKeyNames="POLICY" onrowcommand="GridSetlmt_RowCommand" 
          AllowPaging="True" Height="171px" PageSize="3" Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#ddb547"><tr height="30px"><td style="font-weight: bold;">SETTLEMENT OPTION</td></tr></table>'>
        <Columns>
              <asp:buttonfield buttontype="Button" commandname="Select" headertext="Select" text="Select" ControlStyle-CssClass="button"/>
            <asp:BoundField  DataField="POLICY" HeaderText="POLICY" SortExpression="POLICY" ItemStyle-Width="164px">
                <ItemStyle Width="214px" ></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="LASTNAME" ItemStyle-Width="250px">
                <ItemStyle Width="239px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" SortExpression="ADDRESS" ItemStyle-Width="450px">
                <ItemStyle Width="681px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PLANTYPE" HeaderText="PLAN" SortExpression="PLANTYPE" ItemStyle-Width="150px" >
                <ItemStyle  Width="354px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="VALUE" HeaderText="INITIAL DEPOSIT" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" Visible="true"
                SortExpression="VALUE" ItemStyle-Width="60px"  DataFormatString="{0:C}" >
                <ItemStyle  Width="160px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="BEGBAL" HeaderText="BEGBAL" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="BEGBAL" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="YTD_INT" HeaderText="YTD_INT" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="YTD_INT" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ANNRATE" HeaderText="ANNRATE" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="ANNRATE" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="CURBAL" HeaderText="CURRENT BALANCE" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" Visible="true" 
                SortExpression="CURBAL" ItemStyle-Width="110px" DataFormatString="{0:C}"
               >
                <ItemStyle Width="110px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="POLDATE" HeaderText="ISSUE DATE"
                SortExpression="POLDATE" ItemStyle-Width="110px" DataFormatString="{0:d}"
                >
                <ItemStyle Width="110px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="STATUS" HeaderText="STATUS"
                SortExpression="STATUS" ItemStyle-Width="70px" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <ItemStyle  Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PHONE" HeaderText="PHONE" SortExpression="PHONE"
                Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" SortExpression="EMAIL"
                Visible="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="DOB" HeaderText="DOB" SortExpression="DOB"
                Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MEMBERDT" HeaderText="MEMBERDT"
                SortExpression="MEMBERDT" Visible="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="LASTNAME" HeaderText="LASTNAME"
                SortExpression="LASTNAME" Visible="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MATDATE" HeaderText="matdate" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="matdate" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MODE" HeaderText="mode" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="mode" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="BASEPREM" HeaderText="baseprem" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="baseprem" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="UPDATEDT" HeaderText="updatedt" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="updatedt" Visible="True" DataFormatString="{0:d}">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

            <asp:BoundField DataField="LIEN" HeaderText="lien" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="lien" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

            <asp:BoundField DataField="LOAN" HeaderText="loan" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="loan" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="CASHVAL" HeaderText="cashval" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="cashval" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PUADIV" HeaderText="puadiv" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="puadiv" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ACCUMDIV" HeaderText="accumdiv" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="accumdiv" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="TOTDEATH" HeaderText="totdeath" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="totdeath" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="oname" HeaderText="ownername" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="oname" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="paidto" HeaderText="paidto" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="paidto" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

            <asp:BoundField DataField="rmdcurr" HeaderText="rmdcurr" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="rmdcurr" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="rmdprev" HeaderText="rmdprev" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="rmdprev" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

            <asp:BoundField DataField="pl_spia" HeaderText="pl_spia" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"
                SortExpression="pl_spia" Visible="True">
                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>

                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
            </asp:BoundField>

        </Columns>

        <FooterStyle Height="0px" />
        <HeaderStyle BackColor="#ddb547"></HeaderStyle>
        <PagerStyle BorderWidth="1px" />
        <RowStyle BorderColor="Brown"></RowStyle>
        <SelectedRowStyle BackColor="white" ForeColor="#660033"></SelectedRowStyle>
    </asp:GridView>
    <asp:Table ID="Table1" runat="server" BorderWidth="0" GridLines="None" Visible="False" Width="1000px" EnableViewState="false">

        <asp:TableRow Width="100%" EnableViewState="false">
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="LabelName" runat="server" Text="Name:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="30%" ColumnSpan="6" EnableViewState="false">
                <asp:TextBox ID="TextBoxName" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="LabelAddress" runat="server" Text="Address:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="50%" ColumnSpan="10" EnableViewState="false">
                <asp:TextBox ID="TextBoxAddress" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Width="100%" EnableViewState="false">
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="LabelPhone" runat="server" Text="Phone:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="30%" ColumnSpan="6" EnableViewState="false">
                <asp:TextBox ID="TextBoxPhone" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="LabelEmail" runat="server" Text="Email:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="50%" ColumnSpan="10" EnableViewState="false">
                <asp:TextBox ID="TextBoxEmail" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow Width="100%" EnableViewState="false">
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="LabelOwner" runat="server"  Text="Owner:" Style="color:navy;text-align: right" Width="100%"  eColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="90%" ColumnSpan="18" EnableViewState="false">
                <asp:TextBox ID="TextBoxOwner" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
</asp:TableRow>
       <asp:TableRow>
           <asp:TableCell Width="10%" ColumnSpan="100" EnableViewState="false"><br />
                <asp:Label runat="server"  style="color:navy" eColor="Navy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;For any address/beneficiary changes please contact the home office at fcsu@fcsu.com or call 800.533.6682</asp:Label>
        
           </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow Width="100%" EnableViewState="false">
            <asp:TableCell Width="100%" ColumnSpan="20" EnableViewState="false"><hr width="95%" align="center"  /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Width="100%" EnableViewState="false">
            <asp:TableCell Width="20%" ColumnSpan="4" EnableViewState="false">
                <asp:Label ID="Label2" runat="server" Width="20%" align="center"> </asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="60%" ColumnSpan="12" EnableViewState="false">
                <center><asp:Label ID="polinfo" runat="server" Width="100%" align="center" Font-Size="18" Font-Underline="true">Information</asp:Label></center>
            </asp:TableCell>

            <asp:TableCell Width="20%" ColumnSpan="4" EnableViewState="false">
                <asp:Label ID="Label3" runat="server" Width="20%" align="center"> </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow Width="110%" EnableViewState="false">
            <asp:TableCell Width="15%" ColumnSpan="3" EnableViewState="false">
                <asp:Label ID="LabelPolicy" runat="server" Text="Certificate#:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBoxPolicy" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Width="5%" ColumnSpan="1" EnableViewState="false">
                <asp:Label ID="LabelPlan" runat="server" Text="Plan:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="25%" ColumnSpan="5" EnableViewState="false">
                <asp:TextBox ID="TextBoxPlan" runat="server" Width="100%" align="right" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
           <%-- <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="LabelBirthDate" runat="server" Text="Birth Date:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>--%>
           <%-- <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBoxBirthDate" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>--%>
            <asp:TableCell Width="15%" ColumnSpan="3" EnableViewState="false">
                <asp:Label ID="LabelMatDate" runat="server" Text="Maturity Date:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBoxMatDate" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
             <asp:TableCell Width="15%" ColumnSpan="3" EnableViewState="false">
                <asp:Label ID="LabelGurRate" runat="server" Text="Guaranteed Rate:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBoxGurRate" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow Width="100%" EnableViewState="false">
            <asp:TableCell Width="15%" ColumnSpan="3" EnableViewState="false">
                <asp:Label ID="LabelValue" runat="server" Text="Face Amount:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBoxValue" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Width="5%" ColumnSpan="1" EnableViewState="false">
                <asp:Label ID="LabelPremium" runat="server" Text="Premium:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="30%" ColumnSpan="6" EnableViewState="false">
                <asp:TextBox ID="TextBoxPremium" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>

            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="LabelIssueDate" runat="server" Text="Issue Date:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBoxIssueDate" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>

            <asp:TableCell Width="15%" ColumnSpan="3" EnableViewState="false">
                <asp:Label ID="LabelCurrentRate" runat="server" Text="Current Rate:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="12%" ColumnSpan="3" EnableViewState="false">
                <asp:TextBox ID="TextBoxCurrentRate" runat="server" Width="143%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>

           <%-- <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="LabelGurRate" runat="server" Text="Guaranteed Rate:" Style="text-align: left" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="1" EnableViewState="false">
                <asp:TextBox ID="TextBoxGurRate" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>--%>
        </asp:TableRow>

        <asp:TableRow Width="100%" EnableViewState="false">
            <asp:TableCell Width="15%" ColumnSpan="3" EnableViewState="false">
                <asp:Label ID="Lbl_db" runat="server" Text="Death Benefit:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBox_db" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
             
            <asp:TableCell Width="5%" ColumnSpan="1" EnableViewState="false">
                <asp:Label ID="Lbl_loan" runat="server" Text="Loan:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBox_loan" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="Lbl_cv" runat="server" Text="Cash Value:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBox_cv" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>


           

            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="Lbl_div" runat="server" Text="Dividend:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBox_div" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false"></asp:TextBox>
            </asp:TableCell>

            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:Label ID="Lbl_lien" runat="server" Text="Lien:" Style="text-align: right" Width="100%" ForeColor="Navy" EnableViewState="false" Visible="False"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Width="10%" ColumnSpan="2" EnableViewState="false">
                <asp:TextBox ID="TextBox_lien" runat="server" Width="100%" BorderStyle="None" ReadOnly="true" EnableViewState="false" Visible="False"></asp:TextBox>
            </asp:TableCell>

        </asp:TableRow>
    </asp:Table>


    <br />
    <asp:Label ID="BenfLabel" runat="server" Text="Beneficiary Information:"
        Visible="False" Font-Size="Medium"></asp:Label>

    <%--   Labels for the bottom of the Grid--%>
    <asp:GridView ID="GridView3" runat="server" DataSourceID="SqlDataSource4" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="policy" HeaderText="Policy" ReadOnly="true" SortExpression="policy" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="bname" HeaderText="Name" ReadOnly="true" SortExpression="bname" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="relate" HeaderText="Relation" ReadOnly="true" SortExpression="relate" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="btype" HeaderText="Type" ReadOnly="true" SortExpression="btype" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" />
        </Columns>

    </asp:GridView>

    <%--   Policy Information--%>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT policy, bname, relate, btype FROM bnefcary where policy=@policyNum order by btype desc">

        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxPolicy" Name="policyNum" DefaultValue="" />
        </SelectParameters>

    </asp:SqlDataSource>


    <%--   Start New Grid    --%>
    <br />
    <br />


    <div id="divAnnuity" class="auto-style2">

        <center>
    <asp:Label ID="Summary" runat="server"  Font-Size="14" Visible="False" EnableViewState ="false">Summary</asp:Label>
    </center>


        <%--   Data Source for Bnefcary Gridview--%>
        <asp:Table ID="Table2" runat="server" Visible="False" HorizontalAlign="Center" EnableViewState="false" Width="340px">
            <asp:TableRow runat="server" EnableViewState="false" Width="340px">
                <asp:TableCell runat="server" EnableViewState="false" Width="130px">
                    <asp:Label ID="LabelPrevBal" runat="server" Text="Prev. Balance" EnableViewState="false" Width="130px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" EnableViewState="false" Width="100px">
                    <asp:TextBox ID="TextBoxPrevBal" runat="server" Style="text-align: right;" EnableViewState="false" readonly></asp:TextBox>
                </asp:TableCell>
              <%--  <asp:TableCell ID="TableCell1" runat="server" EnableViewState="false" Width="100px" HorizontalAlign="Center">
                    <asp:Label ID="RMDtitle" runat="server" Text="IRA RMD's" EnableViewState="false" Style="text-align: center; text-decoration: underline;">
                    </asp:Label>
                </asp:TableCell>--%>
            </asp:TableRow>

            <asp:TableRow runat="server" EnableViewState="false" Width="340px">
                <asp:TableCell runat="server" EnableViewState="false" Width="130px">
                    <asp:Label ID="LabelDeposits" runat="server" Text="Deposits" EnableViewState="false" Width="130px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" EnableViewState="false" Width="100px">
                    <asp:TextBox ID="TextBoxDeposits" runat="server" Style="text-align: right;" EnableViewState="false" readonly></asp:TextBox>
                </asp:TableCell>
               <%-- <asp:TableCell ID="TableCell2" runat="server" EnableViewState="false" Width="100px" HorizontalAlign="Center">
                    <asp:Label ID="currRmdLbl" runat="server" Text="2011 RMD" EnableViewState="false"></asp:Label>
                </asp:TableCell>--%>

            </asp:TableRow>
            <asp:TableRow runat="server" EnableViewState="false" Width="340px">
                <asp:TableCell runat="server" EnableViewState="false" Width="130px">
                    <asp:Label ID="LabelWithdrawals" runat="server" Text="Withdrawals" EnableViewState="false" Width="130px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" EnableViewState="false" Width="100px">
                    <asp:TextBox ID="TextBoxWithdrawals" runat="server" Style="text-align: right;" EnableViewState="false" readonly></asp:TextBox>
                </asp:TableCell>
                <%--<asp:TableCell ID="TableCell3" runat="server" EnableViewState="false" Width="100px" HorizontalAlign="Center">
                    <asp:TextBox ID="currRmdBox" runat="server" Style="text-align: right;" EnableViewState="false" readonly></asp:TextBox>
                </asp:TableCell>--%>

            </asp:TableRow>
            <asp:TableRow runat="server" EnableViewState="false" Width="340px">
                <asp:TableCell runat="server" EnableViewState="false" Width="130px">
                    <asp:Label ID="LabelInterest" runat="server" Text="Interest" EnableViewState="false" Width="130px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" EnableViewState="false" Width="100px">
                    <asp:TextBox ID="TextBoxInterest" runat="server" Style="text-align: right;" EnableViewState="false" readonly></asp:TextBox>
                </asp:TableCell>
               <%-- <asp:TableCell ID="TableCell4" runat="server" EnableViewState="false" Width="100px" HorizontalAlign="Center">
                    <asp:Label ID="prevRmdLbl" runat="server" Text="2010 RMD" EnableViewState="false"></asp:Label>
                </asp:TableCell>--%>

            </asp:TableRow>
            <asp:TableRow runat="server" EnableViewState="false" Width="340px">
                <asp:TableCell runat="server" EnableViewState="false" Width="130px">
                    <asp:Label ID="LabelCurBal" runat="server" Text="Cur. Balance" EnableViewState="false" Width="130px" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" EnableViewState="false" Width="100px">
                    <asp:TextBox ID="TextBoxCurBal" runat="server" Style="text-align: right;" EnableViewState="false" readonly></asp:TextBox>
                </asp:TableCell>
               <%-- <asp:TableCell ID="TableCell5" runat="server" EnableViewState="false" Width="100px" HorizontalAlign="Center">
                    <asp:TextBox ID="prevRmdBox" runat="server" Style="text-align: right;" EnableViewState="false" readonly></asp:TextBox>
                </asp:TableCell>--%>
            </asp:TableRow>
           <%-- <asp:TableRow runat="server" EnableViewState="false" Width="340px">
                <asp:TableCell runat="server" EnableViewState="false" Width="240px">
                    <asp:Label ID="lblrmdreq" runat="server" Text="Rmd Required" EnableViewState="false" Width="137px" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" EnableViewState="false" Width="100px">
                    <asp:TextBox ID="txtrmdreq" runat="server" Style="text-align: right;" EnableViewState="false" readonly></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>--%>
        </asp:Table>
    </div>


    <div style="width: 48%; margin-left: 1%; float: right; height: 160px; overflow: auto;">
        <center>
    <asp:Label ID="Transactions" runat="server"  Font-Size="14" Visible="False" EnableViewState ="false">Transactions for last 12 months</asp:Label>
    </center>


        <%--   End New Grid    --%>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
            Visible="False" HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="policy" HeaderText="policy" ReadOnly="True"
                    SortExpression="policy" Visible="False" />
                <asp:BoundField DataField="trandate" HeaderText="Date" ReadOnly="True"
                    SortExpression="trandate" DataFormatString="{0:d}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="amount" HeaderText="Amount" ReadOnly="True"
                    SortExpression="amount" DataFormatString="{0:N2}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="descr" HeaderText="Description" ReadOnly="True"
                    SortExpression="descr" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </asp:GridView>

    </div>

    <%--   Annuity Summary Information--%>

    <%--<asp:TextBox ID="keepBox" Visible="false" runat="server"></asp:TextBox>--%>


    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
    SelectCommand=" SELECT policy.POLICY, member.NAME, member.ADDRESS, policy.PLANTYPE, policy.VALUE,
        policy.BEGBAL, policy.YTD_INT, policy.ANNRATE, policy.CURBAL, policy.POLDATE, policy.STATUS, member.PHONE, member.EMAIL, 
        member.DOB, member.MEMBERDT, member.LASTNAME, policy.MATDATE, policy.MODE, policy.BASEPREM, policy.UPDATEDT, policy.LIEN, 
        policy.LOAN, policy.CASHVAL, policy.PUADIV, policy.ACCUMDIV, policy.TOTDEATH, member_1.NAME AS oname, policy.paidto, 
        policy.rmdcurr, policy.rmdprev 
        FROM policy 
        INNER JOIN member ON policy.CST_NUM = member.CST_NUM 
        INNER JOIN member AS member_1 ON policy.OWNNUM = member_1.CST_NUM 
        WHERE policy.OWNNUM = @number and policy.ANNRATE = 0  ORDER BY member.LASTNAME "    
        OnSelecting="SqlDataSource1_Selecting" ProviderName="<%$ ConnectionStrings:ApplicationServices.ProviderName %>" >
         <SelectParameters>
            <%--<asp:FormParameter  FormField="number" Name="number"/>--%>
            <asp:ControlParameter ControlID="memberDropdown" Name="number" DefaultValue="0000"/>
            <%--<asp:ControlParameter ControlID="TextBox1" />--%>
          <%--  <asp:ControlParameter ControlID="TextBox1" Name="searchText"  DbType="String" DefaultValue=" " />
          --%>
            <%--<asp:ControlParameter ControlID="LabelNumber" Name="number" DefaultValue="0000"/>--%>
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceAnn" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
    SelectCommand=" SELECT policy.POLICY, member.NAME, member.ADDRESS, policy.PLANTYPE, policy.VALUE,
        policy.BEGBAL, policy.YTD_INT, policy.ANNRATE, policy.CURBAL, policy.POLDATE, policy.STATUS, member.PHONE, member.EMAIL, 
        member.DOB, member.MEMBERDT, member.LASTNAME, policy.MATDATE, policy.MODE, policy.BASEPREM, policy.UPDATEDT, policy.LIEN, 
        policy.LOAN, policy.CASHVAL, policy.PUADIV, policy.ACCUMDIV, policy.TOTDEATH, member_1.NAME AS oname, policy.paidto, 
        policy.rmdcurr, policy.rmdprev 
        FROM policy 
        INNER JOIN member ON policy.CST_NUM = member.CST_NUM 
        INNER JOIN member AS member_1 ON policy.OWNNUM = member_1.CST_NUM 
        WHERE policy.OWNNUM = @number and policy.ANNRATE > 0 and pl_spia='N' ORDER BY member.LASTNAME "    
        OnSelecting="SqlDataSource1_Selecting" ProviderName="<%$ ConnectionStrings:ApplicationServices.ProviderName %>" >
         <SelectParameters>
            <%--<asp:FormParameter  FormField="number" Name="number"/>--%>
            <asp:ControlParameter ControlID="memberDropdown" Name="number" DefaultValue="0000"/>
            <%--<asp:ControlParameter ControlID="TextBox1" />--%>
          <%--  <asp:ControlParameter ControlID="TextBox1" Name="searchText"  DbType="String" DefaultValue=" " />
          --%>
            <%--<asp:ControlParameter ControlID="LabelNumber" Name="number" DefaultValue="0000"/>--%>
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceSetlmt" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
    SelectCommand=" SELECT policy.POLICY, member.NAME, member.ADDRESS, policy.PLANTYPE, policy.VALUE,
        policy.BEGBAL, policy.YTD_INT, policy.ANNRATE, policy.CURBAL, policy.POLDATE, policy.STATUS, member.PHONE, member.EMAIL, 
        member.DOB, member.MEMBERDT, member.LASTNAME, policy.MATDATE, policy.MODE, policy.BASEPREM, policy.UPDATEDT, policy.LIEN, 
        policy.LOAN, policy.CASHVAL, policy.PUADIV, policy.ACCUMDIV, policy.TOTDEATH, member_1.NAME AS oname, policy.paidto, 
        policy.rmdcurr, policy.rmdprev, policy.pl_spia
        FROM policy 
        INNER JOIN member ON policy.CST_NUM = member.CST_NUM 
        INNER JOIN member AS member_1 ON policy.OWNNUM = member_1.CST_NUM 
        WHERE policy.OWNNUM = @number and policy.ANNRATE > 0 and policy.pl_spia='Y' ORDER BY member.LASTNAME "    
        OnSelecting="SqlDataSource1_Selecting" ProviderName="<%$ ConnectionStrings:ApplicationServices.ProviderName %>" >
         <SelectParameters>
            <%--<asp:FormParameter  FormField="number" Name="number"/>--%>
            <asp:ControlParameter ControlID="memberDropdown" Name="number" DefaultValue="0000"/>
            <%--<asp:ControlParameter ControlID="TextBox1" />--%>
          <%--  <asp:ControlParameter ControlID="TextBox1" Name="searchText"  DbType="String" DefaultValue=" " />
          --%>
            <%--<asp:ControlParameter ControlID="LabelNumber" Name="number" DefaultValue="0000"/>--%>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT policy, trandate, amount, descr FROM deposit WHERE (year = YEAR({ fn NOW() }))
union
SELECT policy, trandate, amount, descr FROM withdrl WHERE (year = YEAR({ fn NOW() }))
ORDER BY trandate"></asp:SqlDataSource>

    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
        SelectCommand="SELECT * FROM [annsum] WHERE ([policy] = @policy)">
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="policy"
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    


  
    


    


</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
         .button

    {

       

        width:77px;

      

    }
        .auto-style2 {
            width: 48%;
            float: left;
            height: 156px;
        }
        </style>
</asp:Content>


