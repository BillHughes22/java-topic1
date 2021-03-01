<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NoRoleTest.aspx.cs" Inherits="FcsuAgentWebApp.Admin.NoRoleTest" %>


<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <span class="auto-style1"><strong>No Role Assigned:</strong></span><asp:GridView ID="GridViewNoRole" runat="server"
        DataSourceID="SqlDataSource4" SelectedRowStyle-BackColor="#a46cc0" 
         HeaderStyle-BackColor="BlanchedAlmond" CellPadding="4" ForeColor="#333333" GridLines="None"
        RowStyle-BorderColor="Brown" OnSelectedIndexChanged="GridViewNoRole_RowSelected" DataKeyNames = "UserPk"
        SelectedRowStyle-ForeColor="Gold" AutoGenerateColumns="False" AllowPaging="True" PageSize="25">
         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="true"  ItemStyle-Width="100"  >
<ItemStyle  Width="100px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="true"  ItemStyle-Width="200"  >
<ItemStyle  Width="200px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="IsDisabled" HeaderText="Disabled">
            </asp:BoundField>
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" ReadOnly="true"  ItemStyle-Width="100"  >
<ItemStyle  Width="100px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="LastName" HeaderText="LastName" ReadOnly="true" ItemStyle-Width="100"  >
<ItemStyle  Width="100px"></ItemStyle>
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
    <asp:SqlDataSource ID="SqlDataSource4" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand=" SELECT [user].[id] as UserPk , [UserName], [Email], [isDisabled],  [FirstName], [LastName] FROM [User] left  outer join  [UserAndRoleXLink] on [User].id = [UserAndRoleXLink].UserId where RoleId is null order by lastname">

        <SelectParameters>
            <%--<asp:ControlParameter ControlID="TextBoxPolicy" Name="policyNum" DefaultValue="" />--%>
        </SelectParameters>

    </asp:SqlDataSource>
    </asp:Content>