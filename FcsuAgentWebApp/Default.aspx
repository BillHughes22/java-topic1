<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="FcsuAgentWebApp._Default" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to Agent Portal.
    </h2>
    <p>
        Please 
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="~/Account/Login.aspx">login</asp:HyperLink>
        . If this is your first time to our Agent Portal Please
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/Account/Register.aspx">register</asp:HyperLink>
        .
    </p>
<%--<iframe id="iframeFcsu" height="400px" width="1000px" src="http://www.fraternalsoftware.com" ></iframe>--%>

</asp:Content>
