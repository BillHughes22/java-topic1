<%@ Page Title="Role List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="RoleList.aspx.cs" Inherits="FcsuAgentWebApp.Admin.RoleList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LinkButton runat="server" OnCommand="AddRole">Add Role</asp:LinkButton>
    <asp:GridView ID="GridViewRoles" 
        runat="server" 
        AllowSorting="True"
        SelectedRowStyle-BackColor="#a46cc0" 
        SelectedRowStyle-ForeColor="Gold"
        AutoGenerateColumns="False"
        HeaderStyle-BackColor="BlanchedAlmond" 
        RowStyle-BorderColor="Brown"
        CellPadding="4" ForeColor="#333333" GridLines="None" 
        OnSelectedIndexChanged="GridViewRoles_RowSelected"
        DataKeyNames = "RolePk">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns >
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="RoleName"  HeaderText="Role">
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
