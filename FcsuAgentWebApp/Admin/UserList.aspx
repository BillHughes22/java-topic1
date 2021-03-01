<%@ Page Title="User List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="UserList.aspx.cs" Inherits="FcsuAgentWebApp.Admin.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="AddUser">Add User</asp:LinkButton>
    <asp:GridView ID="GridViewUsers" 
        runat="server" 
        AllowSorting="True"
        SelectedRowStyle-BackColor="#a46cc0" 
        SelectedRowStyle-ForeColor="Gold"
        AutoGenerateColumns="False"
        HeaderStyle-BackColor="BlanchedAlmond" 
        RowStyle-BorderColor="Brown"
        CellPadding="4" ForeColor="#333333" GridLines="None" 
        OnSelectedIndexChanged="GridViewUsers_RowSelected"
        DataKeyNames = "UserPk">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns >
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="UserName"  HeaderText="User Name">
            </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email">
            </asp:BoundField>
            <asp:BoundField DataField="IsDisabled" HeaderText="Disabled">
            </asp:BoundField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <EmptyDataTemplate>
            No Records found
        </EmptyDataTemplate>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BorderColor="Brown" BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
        <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

</asp:Content>