<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="getAgentProfile.aspx.cs" Inherits="FcsuAgentWebApp.Agent.getAgentProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
    Please enter requested information.</p>
<p>
    <asp:TextBox ID="nameTextBox" runat="server" Width="236px" 
        ontextchanged="nameTextBox_TextChanged"></asp:TextBox>
</p>
<p>
    Name</p>
<p>
    &nbsp;</p>
<p>
    <asp:TextBox ID="agentTextBox" runat="server" Width="85px"></asp:TextBox>
</p>
<p>
    Agent Number</p>
<p>
    &nbsp;</p>
<p>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Save" />
</p>
</asp:Content>
