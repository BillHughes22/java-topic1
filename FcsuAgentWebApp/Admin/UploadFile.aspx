<%@ Page Title="Upolad File" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UploadFile.aspx.cs" Inherits="FcsuAgentWebApp.Admin.UploadFile" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style2 {
            margin-left: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <strong>File&nbsp;Upload&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Category&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Time Period</strong><br />
<asp:FileUpload ID="FileUpload" Width="400px" runat="server" Font-Size="Medium" Height="31px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:DropDownList ID="cbBxCategory" AppendDataBoundItems="true" runat="server" Font-Size="Medium" Height="31px">
     <asp:ListItem Text="Board Of Directors" Value="1" />
     <asp:ListItem Text="Executive Committe" Value="2" />
     <asp:ListItem Text="Announcements" Value="3" />
     <asp:ListItem Text="Miscellaneous" Value="4" />
    </asp:DropDownList>&nbsp;&nbsp;&nbsp; 
    <asp:DropDownList ID="cbBxMonths"  runat="server" Height="31px" Width="117px" Font-Size="Medium"></asp:DropDownList>&nbsp;
    <asp:DropDownList ID="cbBxYears"  runat="server" Height="31px" Width="108px" Font-Size="Medium"></asp:DropDownList> &nbsp;&nbsp;
    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" Height="26px" Width="92px" CssClass="auto-style2" Font-Size="Medium" />
    <br />
     <br />
    <strong>Link Upload</strong> <br />
    <asp:TextBox ID="txtLinkUpload" runat="server" Height="23px" Width="289px"></asp:TextBox>
    <br />
    <br />
    <%-- <asp:RequiredFieldValidator runat="server" style="color:red;" id="reqFileUpload" controltovalidate="FileUpload" errormessage="Please upload File!" Font-Size="Medium" />--%>
    <asp:Label ID="lblDescr" runat="server" style="font-weight: bold;" Text="Description"></asp:Label>
    
    <br />
    <asp:TextBox ID="txtDescr" runat="server" Height="28px" Width="288px" Font-Size="Medium"></asp:TextBox><br />
    <%--<asp:RequiredFieldValidator runat="server" style="color:red;" id="reqDescr" controltovalidate="txtDescr" errormessage="Please enter Description!" Font-Size="Medium" />--%>
      <asp:Label ID="lblMsg" runat="server" style="font-weight: bold;" Font-Size="Medium"></asp:Label>   
      <br />
      <br />

     </asp:Content>