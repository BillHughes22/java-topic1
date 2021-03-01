<%@ Page Title="User Edit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="UserEdit.aspx.cs" Inherits="FcsuAgentWebApp.Admin.UserEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div  class="accountInfo" style="float:right;">
        <fieldset class="changePassword">
            <legend>User In Roles</legend>
            <asp:CheckBoxList runat="server" ID="chkListRoles" 
                DataTextField="RoleName" DataValueField="RolePk"
                CssClass="chkListStyle">
                
            </asp:CheckBoxList>
        </fieldset>
        
        <fieldset class="changePassword">
            <legend><asp:Label runat="server" ID="lblPassword">Reset Password</asp:Label></legend>
            <label >New Password:</label>
                <asp:TextBox CssClass="textEntry" ID="txtSetPassword" runat="server"></asp:TextBox>
        </fieldset>
    </div>

    <div class="accountInfo">
        <fieldset  class="changePassword">
            <legend>User Information</legend>
            <p>
                <asp:Label runat="server" ID="txtUserPk" Visible="False"></asp:Label>
                <label >User Name:</label>
                <asp:TextBox CssClass="textEntry" ID="txtUserName" runat="server"></asp:TextBox>
            </p>
            <p>
                <label >First Name:</label>
                <asp:TextBox CssClass="textEntry" ID="txtFirstName" runat="server"></asp:TextBox>
            </p>
            <p>
                <label >Last Name:</label>
                <asp:TextBox CssClass="textEntry" ID="txtLastName" runat="server"></asp:TextBox>
            </p>
            <p>
                <label >Email:</label>
                <asp:TextBox CssClass="textEntry" ID="txtEmail" runat="server"></asp:TextBox>
            </p>
             <%--<p>
                <label>Agent or Member:</label>
                <asp:DropDownList CssClass="textEntry" ID="cboAgentMemberDescr" runat="server">
                     <asp:ListItem  Value="Agent">Agent</asp:ListItem>
                 
                  <asp:ListItem Value="Member">Member</asp:ListItem>
                </asp:DropDownList>
            </p>--%>
            <p>
                <label>Agent Number:</label>
                <asp:TextBox CssClass="textEntry" ID="txtAgentNumber" runat="server"></asp:TextBox>
            </p>
            <p>
                <label>Member Number:</label>
                <asp:TextBox CssClass="textEntry" ID="txtMemberNumber" runat="server"></asp:TextBox>
            </p>
           <p>
                <label>Phone:</label>
                <asp:TextBox CssClass="textEntry" ID="txtPhone" runat="server"></asp:TextBox>
            </p>
            <p>
                <label >Disabled:</label>
                <asp:CheckBox ID="chkIsDisabled" runat="server" />
            </p>
            <p>
                <label >Comments:</label>
                <asp:TextBox CssClass="textEntry" ID="txtComments" runat="server" Rows="3" TextMode="MultiLine" Width="320px"></asp:TextBox>
            </p>
        </fieldset>   
    </div>
    

    <p>
        <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" Text="Cancel" OnCommand="UserEdit_Cancel"/>
        <asp:Button ID="SavePushButton" runat="server" Text="Save" OnCommand="UserEdit_Save"/>
        <asp:Button ID="DeleteButton" runat="server" Text="Delete" OnCommand="UserEdit_Delete"/>
    </p>

</asp:Content>   
