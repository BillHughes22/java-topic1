<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agentMain_old.aspx.cs" Inherits="FcsuAgentWebApp.Agent.agentMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Agent Main 
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/Agent/getAgentProfile.aspx">add Info</asp:HyperLink>
        <br />

        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    
    </div>
    <p>
        <asp:HyperLink ID="HyperLink2" runat="server">History</asp:HyperLink>
        <asp:HyperLink ID="HyperLink3" runat="server" 
            NavigateUrl="~/Agent/policyView.aspx">Policies</asp:HyperLink>
    </p>
    </form>
</body>
</html>
