<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" enableEventValidation ="false"
    CodeBehind="agentCommissions.aspx.cs" Inherits="FcsuAgentWebApp.Agent.agentCommissions" %>

<%@ MasterType VirtualPath="~/Site.Master" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>




<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server" overflow="scroll">
     <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    
     <%--   Show the Page Header  --%>
    <asp:Label ID="head_title" CssClass="head_title" runat="server" Text="Agent Commissions Report"></asp:Label>


    <div id="div_spacing" class="div_spacing">
    <%--   Show the Agent name and number  --%>
    <asp:Label ID="head_number" CssClass="head_number" runat="server"></asp:Label>
    <asp:Label ID="head_name" CssClass="head_name" runat="server"></asp:Label>
    </div>
    <div id="div_spacing2" class="div_spacing">
    <%--   Show the Agent Address Line1  --%>
    <asp:Label ID="Spacer1" CssClass="head_number" runat="server"></asp:Label>
    <asp:Label ID="head_Add1" CssClass="head_name" runat="server"></asp:Label>
    </div>
    <div id="div_spacing3" class="div_spacing">
    <%--   Show the Agent Address Line1  --%>
    <asp:Label ID="Spacer2" CssClass="head_number" runat="server"></asp:Label>
    <asp:Label ID="head_Add2" CssClass="head_name" runat="server"></asp:Label>
    </div>

    
                          

    <%--   Policies Gridview--%>
   <input type="hidden" name="keep" id="keep" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
    
  

     <asp:Label ID="Lbl_update" runat="server" Text=""></asp:Label>
    
    <%--   Pull Down to show all the report dates that are available for the agent that is currently logged in   --%>
    &nbsp;<asp:Label ID="Label2" runat="server" Text="Agent Number:" class="noprint"></asp:Label>
    &nbsp;<asp:Label ID="LabelNumber" runat="server"></asp:Label>
    &nbsp;<asp:DropDownList ID="agentDropdown" runat="server" DataTextField="agtname" DataValueField="agent" Width="16%" AutoPostBack="True" Height="28px" OnSelectedIndexChanged="agentDropdown_SelectedIndexChanged"></asp:DropDownList>
    

    <%--   Pull Down to show all the report dates that are available for the agent that is currently logged in   --%>
    <asp:Label ID="Label3" runat="server" Text="Search by Report Date:" class="noprint"></asp:Label>
    &nbsp;<asp:DropDownList ID="reportDropdown" runat="server" DataTextField="REPORT_NAME" DataValueField="REPORT_NAME" AutoPostBack="True" Height="28px" OnSelectedIndexChanged="reportDropdown_SelectedIndexChanged"></asp:DropDownList>
    <asp:Button ID="Export_PDF" runat="server" Text="Export to PDF" OnClick="Export_PDF_Click" Visible="false" />
    <br /><br />
    <%--   Gridview for Commissions   --%>

    
    <asp:GridView ID="gv_commissions" runat="server" AllowSorting="False" AutoGenerateColumns="False"
        SelectedRowStyle-BackColor="#a46cc0" SelectedRowStyle-ForeColor="Gold"
        BorderStyle="Inset" BorderWidth="1px" BorderColor="#4B6C9E" BackColor="White"
        RowStyle-BorderColor="Brown" ShowFooter="true"
        HorizontalAlign="Center" Width="100%" Style="margin-left: 0px;" 
        AllowPaging="False" Height="71px" >
        <Columns>
            
            
            <asp:BoundField DataField="POLICY" HeaderText="Policy" SortExpression="POLICY">
                <ItemStyle Width="100px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="FULLNM" HeaderText="Name" SortExpression="FULLNM">
                <ItemStyle Width="435px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="POLDATE" HeaderText="Policy Date" DataFormatString="{0:d}">
                <ItemStyle Width="105px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="TYPE" HeaderText="Type" SortExpression="TYPE">
                <ItemStyle Width="125px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="DATEREC" HeaderText="Payment Date" SortExpression="DATEREC" DataFormatString="{0:d}">
                <ItemStyle Width="165px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AMT_PD" HeaderText="Premium Received" SortExpression="AMT_PD" DataFormatString="{0:c}">
                <ItemStyle Width="165px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="YEAR" HeaderText="Year" SortExpression="YEAR" DataFormatString="{0:F0}">
                <ItemStyle Width="135px" HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="COM_PCT" HeaderText="Commission Percent" DataFormatString="{0:0.00%}" >
                <ItemStyle Width="135px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AGENTPCT" HeaderText="Agent Share*" SortExpression="AGENTPCT" DataFormatString="{0:p}">
                <ItemStyle Width="165px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="COM1ST" HeaderText="1st Year Life" SortExpression="COM1ST" DataFormatString="{0:c}">
                <ItemStyle Width="165px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="COMREN" HeaderText="Renewal Life" SortExpression="COMREN" DataFormatString="{0:c}">
                <ItemStyle Width="165px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="COMSP" HeaderText="Single Premium" SortExpression="COMSP" DataFormatString="{0:c}">
                <ItemStyle Width="165px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="COMANN" HeaderText="Annuity" SortExpression="COMANN" DataFormatString="{0:c}">
                <ItemStyle Width="165px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="LODGE" HeaderText="Lodge" SortExpression="LODGE">
                <ItemStyle Width="115px" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>

        </Columns>
        <FooterStyle Height="30px" BackColor="#FFFFFF" ForeColor="Black" Font-Bold="true" HorizontalAlign="Right"/>
        <HeaderStyle BackColor="#CCFFCC"></HeaderStyle>
        <PagerStyle BorderWidth="1px" />
        <RowStyle BorderColor="Brown"></RowStyle>
        <AlternatingRowStyle BackColor="#EEEEEE" />
    </asp:GridView>





    <%--   End Gridview for Commissions   --%>




    

</asp:Content>


