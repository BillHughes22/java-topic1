<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="adminMain.aspx.cs" Inherits="FcsuAgentWebApp.Admin.adminMain1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
        Font-Underline="True" Text="Admin Screen"></asp:Label>
    <br/><br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" style ="width: 1028px;" 
     
    >
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="UserName" 
                SortExpression="UserName" ItemStyle-Width="65px">  
             <ItemStyle Width="100px" ></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AgentNumber" HeaderText="AgentNumber" 
                SortExpression="AgentNumber" >
             <ItemStyle Width="100px" ></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MemberNumber" HeaderText="MemberNumber" 
                SortExpression="MemberNumber" >
             <ItemStyle Width="100px" ></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email" 
                SortExpression="Email" ItemStyle-Wrap="true" >
            <ItemStyle Width="150px"   ></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                SortExpression="FirstName" >
             <ItemStyle Width="100px" ></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="LastName" HeaderText="LastName" 
                SortExpression="LastName" >
             <ItemStyle Width="100px" ></ItemStyle>
            </asp:BoundField>
            <asp:CheckBoxField DataField="IsDisabled" HeaderText="IsDisabled" 
                SortExpression="IsDisabled" >
             <ItemStyle Width="50px" ></ItemStyle>
            </asp:CheckBoxField>
            <asp:BoundField DataField="Comments" HeaderText="Comments" 
                SortExpression="Comments" >
             <ItemStyle Width="50px" ></ItemStyle>
                </asp:BoundField>
              <asp:BoundField DataField="UpdatedDate" HeaderText="UpdatedDate" 
                SortExpression="UpdatedDate" >
                   <ItemStyle Width="50px" ></ItemStyle>
            </asp:BoundField>
        </Columns>
</asp:GridView>
</asp:Content>
