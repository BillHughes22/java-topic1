﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AgentList.aspx.cs" Inherits="FcsuAgentWebApp.Admin.AgentList" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="AddUser">Add User</asp:LinkButton>
    <br />
    <span class="auto-style1"><strong>Agents:</strong></span><asp:GridView ID="GridViewUsers" 
        runat="server" 
        AllowSorting="True"
        SelectedRowStyle-BackColor="#a46cc0" 
        SelectedRowStyle-ForeColor="Gold"
        AutoGenerateColumns="False"
        HeaderStyle-BackColor="BlanchedAlmond" 
        RowStyle-BorderColor="Brown" 
        CellPadding="4" ForeColor="#333333" GridLines="None"  AllowPaging="True" PageSize="25"
        OnSelectedIndexChanged="GridViewUsers_RowSelected" OnPageIndexChanging="OnPageIndexChanging" 
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
            <asp:BoundField DataField="FirstName" HeaderText="First Name">
            </asp:BoundField>
            <asp:BoundField DataField="LastName" HeaderText="Last Name">
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

