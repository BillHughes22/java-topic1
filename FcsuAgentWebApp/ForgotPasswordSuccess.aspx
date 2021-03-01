<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master"
    CodeBehind="ForgotPasswordSuccess.aspx.cs" Inherits="FcsuAgentWebApp.ForgotPasswordSuccess" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  
    <%--<asp:Label id="heading" runat="server">Change Password</asp:Label> <br />--%>
    <asp:Label id="msg" runat="server" Text="Your password has been changed successfully."></asp:Label>
    <br />
   <br />
     <asp:Button id="login" runat="server" Text="Go Back To Login" OnClick="login_Click"/>
</asp:Content>