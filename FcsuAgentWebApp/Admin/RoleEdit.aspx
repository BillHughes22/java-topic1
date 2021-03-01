<%@ Page Title="Role Edit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="RoleEdit.aspx.cs" Inherits="FcsuAgentWebApp.Admin.RoleEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div  class="accountInfo" style="float:right;">
        <fieldset class="changePassword">
            <legend>User In Role</legend>
            <asp:CheckBoxList runat="server" ID="chkListUsers" 
                DataTextField="UserName" DataValueField="UserPk"
                CssClass="chkListStyle" >
                
            </asp:CheckBoxList>
        </fieldset>
        
    </div>

    <div class="accountInfo">
        <fieldset  class="changePassword">
            <legend>Role Information</legend>
            <p>
                <asp:Label runat="server" ID="txtRolePk" Visible="False"></asp:Label>
                <label >Name:</label>
                <asp:TextBox CssClass="textEntry" ID="txtRoleName" runat="server"></asp:TextBox>
            </p>
            <p>
                <label >Description:</label>
                <asp:TextBox CssClass="textEntry" ID="txtDescription" runat="server"></asp:TextBox>
            </p>
            <p>
                <label >Disabled:</label>
                <asp:CheckBox ID="chkIsDisabled" runat="server" />
            </p>
        </fieldset>   
    </div>
    

    <p >
        <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" Text="Cancel" OnCommand="RoleEdit_Cancel"/>
        <asp:Button ID="SavePushButton" runat="server" Text="Save" OnCommand="RoleEdit_Save"/>
    </p>    

</asp:Content>