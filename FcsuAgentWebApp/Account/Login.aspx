<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="FcsuAgentWebApp.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            min-height: auto !important;
        }

        .auto-style2 {
            width: 322px;
        }

        .auto-style3 {
            margin-left: 0px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Log In
    </h2>
    <p>
        Please enter your username and password.
        <%--<asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink> if you don't have an account.--%>
    </p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false"
        RenderOuterTable="false" OnLoggedIn="LoginUser_LoggedIn" OnAuthenticate="AuthenticateUser"
        DestinationPageUrl="~/Agent/agentMain.aspx" TabIndex="1">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>



            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="LoginUserValidationGroup" />
            <div class="auto-style1">
                <fieldset class="login">
                    <legend>Account Information</legend>

                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                            CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry"
                            TextMode="Password" TabIndex="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p class="auto-style2">
                        <asp:CheckBox ID="RememberMe" runat="server" Visible="False" />
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe"
                            CssClass="inline" Visible="False" Width="125px">Keep me logged in</asp:Label>
                    </p>
                </fieldset>
                <%-- <p class="submitButton">
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                        ValidationGroup="LoginUserValidationGroup" BackColor="#ddb547" 
                        TabIndex="3" OnClick="LoginButton_Click"/>
             
                    </p>--%>
            <p>
                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Height="40" TabIndex="10" Text="Login" ValidationGroup="LoginUserValidationGroup" Width="80" />
                &nbsp;&nbsp;
                    <asp:Button ID="RegisterAgent" runat="server" Height="40px" OnClick="RegisterAgent_Click" TabIndex="11" Text="Register" Width="70px" />&nbsp;&nbsp;
                    <asp:Button ID="RegisterMem" runat="server" Height="40px" OnClick="RegisterMemButton_Click" TabIndex="12" Text="Create Account" Width="120px" Font-Bold="True" ForeColor="Red" />
            </p>

            <p>
                <asp:Button ID="btnForgotPswd" runat="server" Height="40px" TabIndex="13" Text="Forgot Password"
                    Width="121px" Style="text-decoration: underline; cursor: pointer; background: none!important; border: none; padding: 0!important;" OnClick="btnForgotPswd_Click" />

                <asp:Button ID="btnForgotUserName" runat="server" Height="40px" TabIndex="14" Text="Forgot Username" Width="123px"
                    Style="text-decoration: underline; cursor: pointer; background: none!important; border: none; padding: 0!important;" OnClick="btnForgotUserName_Click" CssClass="auto-style3" />
            </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
    <div runat="server" id="memberFooter">
        Having trouble with the Member LogIn?  Please <a href="https://www.fcsu.com/faq" target="_blank">visit our FAQ</a> for a possible solution.
        <br />
        If you still need help, <a href="https://www.fcsu.com/contact/" target="_blank">email</a> or call us at 800-533-6682 ext.129.

    </div>
</asp:Content>

