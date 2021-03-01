<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminMain0.aspx.cs" Inherits="FcsuAgentWebApp.Admin.adminMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" 
                    SortExpression="CreateDate" />
                <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" 
                    SortExpression="LastLoginDate" />
                <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" 
                    SortExpression="IsApproved" />
                <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" 
                    SortExpression="IsLockedOut" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" 
                    SortExpression="UserName" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:db_28354_ppcsConnectionString %>" 
            SelectCommand="SELECT [Email], [CreateDate], [LastLoginDate], [IsApproved], [IsLockedOut], [UserName] FROM [vw_aspnet_MembershipUsers]">
        </asp:SqlDataSource>
    
    </div>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
    </form>
</body>
</html>
