<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="FcsuAgentWebApp.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br /><asp:Label ID="MsgLbl" runat="server" style ="color:red" MultiLine="true"></asp:Label>
    <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" OnCreatedUser="RegisterUser_CreatedUser">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
                <ContentTemplate>
                    <h2>
                        Create a New Account
                    </h2>
                    <p>
                        Use the form below to create a new account.
                    </p>
                    
                  <%--  <p>
                        Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
                    </p>--%>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register" style="margin-top:50px;width:550px" >
                            <legend>Account Information</legend>
                              <p>
                                <asp:Label ID="lblFname" runat="server" AssociatedControlID="Fname">First Name:</asp:Label>
                                <asp:TextBox ID="Fname" runat="server" CssClass="textEntry"></asp:TextBox>

                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Fname"
                                    CssClass="failureNotification" ErrorMessage="First name is required." ToolTip="First name is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                             <p>
                                <asp:Label ID="lnameLabel" runat="server" AssociatedControlID="Lname">Last Name:</asp:Label>
                                <asp:TextBox ID="Lname" runat="server" CssClass="textEntry"></asp:TextBox>

                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Lname"
                                    CssClass="failureNotification" ErrorMessage="Last name is required." ToolTip="Last name is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                              <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification" ToolTip="Invalid Email Format."
                                      ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Email" ErrorMessage="Invalid Email Format">*</asp:RegularExpressionValidator>
                            </p>
                          <%--    <p>
                                <asp:Label ID="lblphone" Width="400px" runat="server" AssociatedControlID="Phone">Cell Phone: (for verification at time of login)</asp:Label>
                                <asp:TextBox ID="Phone" runat="server" CssClass="passwordEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="Phone" CssClass="failureNotification" Display="Dynamic"
                                    ErrorMessage="Phone Number is required." ID="PhoneValidator" runat="server"
                                    ToolTip="Phone Number is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="PhoneExpressionValidator" runat="server"
                                    CssClass="failureNotification" ControlToValidate="Phone" Display="Dynamic" ErrorMessage="Phone Number should contain numbers and only 10 digits"
                                    ValidationExpression="[0-9]{10}" ValidationGroup="RegisterUserValidationGroup">*</asp:RegularExpressionValidator>
                            </p>--%>
                            <p>
                                 <asp:Label ID="lblphone" Width="400px" runat="server" AssociatedControlID="txtph1">Cell Phone: (for verification at time of login)</asp:Label>
                                 <asp:TextBox ID="txtph1" runat="server" Width="75px" CssClass="textEntry"  MinLength="3" MaxLength="3"></asp:TextBox>
                               
                                &nbsp;<asp:TextBox ID="txtph2" runat="server" Width="75px" CssClass="textEntry"  MinLength="3" MaxLength="3"></asp:TextBox>
                                
                                &nbsp;<asp:TextBox ID="txtph3" runat="server" type="text" Width="146px" CssClass="textEntry"  MinLength="4" MaxLength="4"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtph1"
                                    CssClass="failureNotification" ErrorMessage="first 3 digits of phone number is required." ToolTip="first 3 digits of phone number is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtph2"
                                    CssClass="failureNotification" ErrorMessage="second 3 digits of phone number is required." ToolTip="second 3 digits of phone number is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtph3" 
                                    CssClass="failureNotification" ErrorMessage="last 4 digits of phone number is required." ToolTip="last 4 digits of phone number is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <%-- <asp:RangeValidator runat="server" Type="Integer" MinimumValue="100" MaximumValue="999" ControlToValidate="txtph1"
                                    CssClass="failureNotification" ToolTip="first 3 digits of phone number should between 100 and 999." ErrorMessage="first 3 digits of phone number should between 100 and 999." ValidationGroup="RegisterUserValidationGroup">*</asp:RangeValidator>
                                <asp:RangeValidator runat="server" Type="Integer" MinimumValue="100" MaximumValue="999" ControlToValidate="txtph2"
                                    CssClass="failureNotification" ToolTip="second 3 digits of phone number should be between 100 and 999." ErrorMessage="second 3 digits of phone number should be between 100 and 999." ValidationGroup="RegisterUserValidationGroup">*</asp:RangeValidator>
                                 <asp:RangeValidator runat="server" Type="Integer" MinimumValue="1000" MaximumValue="9999" ControlToValidate="txtph3"
                                    CssClass="failureNotification" ToolTip="last 4 digits of phone number should be between 1000 and 9999" ErrorMessage="last 4 digits of phone number should be between 1000 and 9999." ValidationGroup="RegisterUserValidationGroup">*</asp:RangeValidator>
                          --%>
                                <asp:RegularExpressionValidator  runat="server" ControlToValidate="txtph2" ValidationGroup="RegisterUserValidationGroup" 
                                    CssClass="failureNotification" ToolTip= "Minimum length of second 3 digits of phone number is 3" ErrorMessage="Minimum length of second 3 digits of phone number is 3" ValidationExpression=".{3}.*" >*</asp:RegularExpressionValidator>
                           <asp:RegularExpressionValidator  runat="server" ControlToValidate="txtph1" ValidationGroup="RegisterUserValidationGroup" 
                                    CssClass="failureNotification" ToolTip= "Minimum length of first 3 digits of phone number is 3" ErrorMessage="Minimum length of first 3 digits of phone number is 3" ValidationExpression=".{3}.*" >*</asp:RegularExpressionValidator>
                         <asp:RegularExpressionValidator  runat="server" ControlToValidate="txtph3" ValidationGroup="RegisterUserValidationGroup" 
                                    CssClass="failureNotification" ToolTip= "Minimum length of last 4 digits of phone number is 4" ErrorMessage="Minimum length of last 4 digits of phone number is 4" ValidationExpression=".{4}.*" >*</asp:RegularExpressionValidator>
                         </p>
                             <asp:Label ID="DOBLabel" runat="server" Style="padding: 11px">DOB:</asp:Label>
                            <div style="padding-left: 11px">
                                <asp:Label ID="lblMon" Width="83px" runat="server" Text="Month"></asp:Label>
                                <asp:Label ID="lblDay" Width="85px" runat="server" Text="Day"></asp:Label>
                                <asp:Label ID="lblYr" Width="90px" runat="server" Text="Year"></asp:Label>

                            </div>
                            <div style="padding-left: 11px">
                                
                                <asp:TextBox ID="Mon" runat="server" Width="75px" CssClass="textEntry" placeholder="mm" MaxLength="2"></asp:TextBox>
                               
                                &nbsp;<asp:TextBox ID="Day" runat="server" Width="75px" CssClass="textEntry" placeholder="dd" MaxLength="2"></asp:TextBox>
                                
                                &nbsp;<asp:TextBox ID="Year" runat="server" Width="145px" CssClass="textEntry" placeholder="yyyy" MaxLength="4"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Mon"
                                    CssClass="failureNotification" ErrorMessage="Month is required." ToolTip="Month is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Day"
                                    CssClass="failureNotification" ErrorMessage="Day is required." ToolTip="Day is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Year"
                                    CssClass="failureNotification" ErrorMessage="Year is required." ToolTip="Year is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" Type="Integer" MinimumValue="1" MaximumValue="12" ControlToValidate="Mon"
                                    CssClass="failureNotification" ToolTip="Month should between 1 and 12." ErrorMessage="Month should between 1 and 12." ValidationGroup="RegisterUserValidationGroup">*</asp:RangeValidator>
                                <asp:RangeValidator runat="server" Type="Integer" MinimumValue="1900" MaximumValue="2100" ControlToValidate="Year"
                                    CssClass="failureNotification" ToolTip="Year should between 1900 and 1200." ErrorMessage="Year should between 1900 and 1200." ValidationGroup="RegisterUserValidationGroup">*</asp:RangeValidator>
                                 <asp:RangeValidator runat="server" Type="Integer" MinimumValue="1" MaximumValue="31" ControlToValidate="Day"
                                    CssClass="failureNotification" ToolTip="Day should between 1 and 31" ErrorMessage="Day should between 1 and 31." ValidationGroup="RegisterUserValidationGroup">*</asp:RangeValidator>
                                
                            </div>

                              <p style="margin-top: 7px">
                                <asp:Label ID="SSNLabel" Width="400px" runat="server" AssociatedControlID="SSN">SSN: (last 4 digits)</asp:Label>
                                <asp:TextBox ID="SSN" runat="server" CssClass="textEntry"></asp:TextBox>

                                <asp:RegularExpressionValidator runat="server" ControlToValidate="SSN" ValidationExpression="^\d{4}$"
                                    ErrorMessage="only 4 digits." CssClass="failureNotification" ToolTip="only 4 digits."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="SSN"
                                    CssClass="failureNotification" ErrorMessage="Last 4-digits of SSN is required." ToolTip="Last 4-digits of SSN is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                           
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" ErrorMessage="User RoleName is required." ToolTip="User RoleName is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                          
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" Width="400px" AssociatedControlID="Password">Password:</asp:Label>
                               
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    CssClass="failureNotification" ControlToValidate="Password" Display="Dynamic"
                                      ToolTip="Passwords are required to be a minimum of 8 characters in length and should contain at least one uppercase, one lowercase and one special character."
                                      ErrorMessage="Passwords are required to be a minimum of 8 characters in length and should contain at least one uppercase, one lowercase and one special character."
                                    ValidationExpression="(?=^.{8,}$)(?=.*\d)(?=.*\W+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" ValidationGroup="RegisterUserValidationGroup">*</asp:RegularExpressionValidator>
                         
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" Width="400px" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                                     ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                            </p>

                            </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Create User" 
                                 ValidationGroup="RegisterUserValidationGroup"/>
                            <asp:Button ID="cancel" runat="server"  Text="Cancel" onclick="cancel_Click"/>
                        </p>
                       


                    </div>
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>



