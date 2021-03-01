<%@Page Title= "" Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" MasterPageFile="~/Site.master" Inherits="FcsuAgentWebApp.ForgotPassword" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
    <asp:Label  runat="server" ID="heading">Verify following details to change password</asp:Label> 
     
    <div class="forgot">
        <fieldset class="register" style="margin-top:10px">
     <legend id ="legheading" runat="server">Forgot Password</legend>
            <asp:Label runat="server" ID="lblmsg" style="color:red"></asp:Label><br />

            <asp:ValidationSummary ID="PswdValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="PswdValidationGroup"/>
         <p>
            <asp:Label ID="lblUName" runat="server" AssociatedControlID="txtUName">User Name:</asp:Label>
            <asp:TextBox ID="txtUName" runat="server" CssClass="textEntry"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUName" ValidationGroup="PswdValidationGroup"
             CssClass="failureNotification" ErrorMessage="First name is required." ToolTip="User Name is required."
             >*</asp:RequiredFieldValidator>
             </p> 
        <p>
            <asp:Label ID="lblFname" runat="server" AssociatedControlID="txtFname">First Name:</asp:Label>
            <asp:TextBox ID="txtFname" runat="server" CssClass="textEntry"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFname" ValidationGroup="PswdValidationGroup"
             CssClass="failureNotification" ErrorMessage="First name is required." ToolTip="First name is required."
             >*</asp:RequiredFieldValidator>
             </p>
         <p>
               <asp:Label ID="lbllname" runat="server" AssociatedControlID="txtLname">Last Name:</asp:Label>
                <asp:TextBox ID="txtLname" runat="server" CssClass="textEntry"></asp:TextBox>

                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLname" ValidationGroup="PswdValidationGroup"
                                    CssClass="failureNotification" ErrorMessage="Last name is required." ToolTip="Last name is required."
                                   >*</asp:RequiredFieldValidator>
                            </p>
      
         <asp:Label ID="DOBLabel" runat="server" Style="padding: 11px">DOB:</asp:Label>
                            <div style="padding-left: 11px">
                                <asp:Label ID="lblMon" Width="83px" runat="server" Text="Month"></asp:Label>
                                <asp:Label ID="lblDay" Width="85px" runat="server" Text="Day"></asp:Label>
                                <asp:Label ID="lblYr" Width="90px" runat="server" Text="Year"></asp:Label>
                            </div>
                            <div style="padding-left: 11px">
                                
                                <asp:TextBox ID="txtMon" runat="server" Width="75px" CssClass="textEntry" placeholder="mm" MaxLength="2"></asp:TextBox>
                               
                                &nbsp;<asp:TextBox ID="txtDay" runat="server" Width="75px" CssClass="textEntry" placeholder="dd" MaxLength="2"></asp:TextBox>
                                
                                &nbsp;<asp:TextBox ID="txtYear" runat="server" Width="145px" CssClass="textEntry" placeholder="yyyy" MaxLength="4"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMon" ValidationGroup="PswdValidationGroup"
                                    CssClass="failureNotification" ErrorMessage="Month is required." ToolTip="Month is required."
                                  >*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDay" ValidationGroup="PswdValidationGroup"
                                    CssClass="failureNotification" ErrorMessage="Day is required." ToolTip="Day is required."
                                   >*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtYear" ValidationGroup="PswdValidationGroup"
                                    CssClass="failureNotification" ErrorMessage="Year is required." ToolTip="Year is required."
                                   >*</asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" Type="Integer" MinimumValue="1" MaximumValue="12" ControlToValidate="txtMon" ValidationGroup="PswdValidationGroup"
                                    CssClass="failureNotification" ToolTip="Month should between 1 and 12." ErrorMessage="Month should between 1 and 12." >*</asp:RangeValidator>
                                <asp:RangeValidator runat="server" Type="Integer" MinimumValue="1900" MaximumValue="2100" ControlToValidate="txtYear" ValidationGroup="PswdValidationGroup"
                                    CssClass="failureNotification" ToolTip="Year should between 1900 and 1200." ErrorMessage="Year should between 1900 and 1200." >*</asp:RangeValidator>
                                 <asp:RangeValidator runat="server" Type="Integer" MinimumValue="1" MaximumValue="31" ControlToValidate="txtDay" ValidationGroup="PswdValidationGroup"
                                    CssClass="failureNotification" ToolTip="Day should between 1 and 31" ErrorMessage="Day should between 1 and 31." >*</asp:RangeValidator>
                                
                            </div>
            <p style="margin-top: 10px">
                                <asp:Label ID="SSNLabel" Width="400px" runat="server" AssociatedControlID="txtSSN">SSN: (last 4 digits)</asp:Label>
                                <asp:TextBox ID="txtSSN" runat="server" CssClass="textEntry"></asp:TextBox>

                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtSSN" ValidationExpression="^\d{4}$"
                                    ErrorMessage="only 4 digits." CssClass="failureNotification" ToolTip="only 4 digits." ValidationGroup="PswdValidationGroup"
                                   >*</asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSSN"
                                    CssClass="failureNotification" ErrorMessage="Last 4-digits of SSN is required." ToolTip="Last 4-digits of SSN is required."
                                   ValidationGroup="PswdValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
              <p>
               <asp:Button ID="btnVerify" runat="server"  Text="Verify" OnClick="btnVerify_Click" ValidationGroup="PswdValidationGroup"/>&nbsp;&nbsp; &nbsp;
                <asp:Button ID="btncancel" runat="server"  Text="Cancel" OnClick="btncancel_Click" />
           </p>
             </fieldset>
          
    </div>
  </asp:Content>



