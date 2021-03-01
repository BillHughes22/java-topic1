<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="adminMain.aspx.cs" Inherits="FcsuAgentWebApp.Admin.adminMain1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
        Font-Underline="True" Text="Admin Screen"></asp:Label>
    <br/><br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSource2">
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="UserName" 
                SortExpression="UserName" />
            <asp:BoundField DataField="AgentNumber" HeaderText="AgentNumber" 
                SortExpression="AgentNumber" />
            <asp:BoundField DataField="MemberNumber" HeaderText="MemberNumber" 
                SortExpression="MemberNumber" />
            <asp:BoundField DataField="Email" HeaderText="Email" 
                SortExpression="Email" />
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" 
                SortExpression="LastName" />
            <asp:CheckBoxField DataField="IsDisabled" HeaderText="IsDisabled" 
                SortExpression="IsDisabled" />
            <asp:BoundField DataField="Comments" HeaderText="Comments" 
                SortExpression="Comments" />
              <asp:BoundField DataField="UpdatedDate" HeaderText="UpdatedDate" 
                SortExpression="UpdatedDate" />
        </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
    
        SelectCommand="SELECT a.[UserName], a.[AgentNumber], a.[Email], [FirstName], [LastName], [IsDisabled], [Comments], [MemberNumber], c.[UpdatedDate]  FROM [User]a Left Join ( (select distinct (username),updatedDate from [UpdatedDateInfo] a where updateddate in (select Top (1) updateddate from [UpdatedDateInfo] b where b.userName = a.username order by updateddate desc))) c on a.[UserName] = c.[UserName] ORDER BY a.[IsDisabled], a.[LastName], a.[FirstName]">
 
</asp:SqlDataSource>
</asp:Content>
