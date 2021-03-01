<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePasswordOut.aspx.cs" MasterPageFile="~/Site.master"
    Inherits="FcsuAgentWebApp.ChangePasswordOut" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <p>
                       Verify following details to change password
                    </p>
    <div class="changePsd">
        <fieldset class="register" style="margin-top:10px">
     <legend>Forgot Password</legend>
          
            <asp:ValidationSummary ID="PswdValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="PswdValidationGroup"/>
                    <p>
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword" Width="331px">New Password:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                             CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required." 
                             ValidationGroup="PswdValidationGroup">*</asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    CssClass="failureNotification" ControlToValidate="NewPassword" Display="Dynamic"
                                      ToolTip="Passwords are required to be a minimum of 8 characters in length and should contain at least one uppercase, one lowercase, one number and one special character."
                                      ErrorMessage="Passwords are required to be a minimum of 8 characters in length and should contain at least one uppercase, one lowercase, one number and one special character."
                                    ValidationExpression="(?=^.{8,}$)(?=.*\d)(?=.*\W+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" ValidationGroup="PswdValidationGroup">*</asp:RegularExpressionValidator>
                         
                        </p>
                    <p>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword" Width="197px">Confirm New Password:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                             ToolTip="Confirm New Password is required." ValidationGroup="PswdValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                             ValidationGroup="PswdValidationGroup">*</asp:CompareValidator>
                    </p>
             <p>
               <asp:Button ID="btnChange" runat="server"  Text="Change Password" OnClick="btnChange_Click" ValidationGroup="PswdValidationGroup"/>&nbsp;&nbsp; &nbsp;
                <asp:Button ID="btncancel" runat="server"  Text="Cancel" OnClick="btncancel_Click"  />
           </p>
            </fieldset>
        </div>
</asp:Content>


