<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DeleteUploadedFile.aspx.cs" 
    Inherits="FcsuAgentWebApp.Admin.DeleteUploadedFile" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>
    </asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  





     <asp:GridView ID="GridFilesDir" runat="server"  DataSourceID="SqlDataSource2" SelectedRowStyle-BackColor="#a46cc0" SelectedRowStyle-ForeColor="Gold"
         BorderStyle="Inset" BorderWidth="5px" BorderColor="#4B6C9E" BackColor="White"
          HeaderStyle-BackColor="BlanchedAlmond" RowStyle-BorderColor="Brown"  HorizontalAlign="Center" style="margin-top: 0px" 
         AutoGenerateColumns="False" DataKeyNames="filesdir_i" Width="559px"  ShowGridLines="False" AllowPaging="True" PageSize="25" CssClass="auto-style1" OnRowDeleting="GridFilesDir_RowDeleting" >  
                    <Columns>  
                         <asp:TemplateField>
                    <ItemTemplate>
                        <%--ADD THE DELETE LINK BUTTON--%>
                        <asp:LinkButton Runat="server" 
                            OnClientClick ="return confirm('Are you sure you want to delete ?');"
                            CommandName="Delete" >Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:BoundField DataField="filesdir_i" HeaderText="S.No." Visible="False" />  
                        <asp:BoundField DataField="Filename" HeaderText="Filename" ItemStyle-Width="50%" ItemStyle-Wrap="false"/>  
                        <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Width="50%" ItemStyle-Wrap="false"/>  
                        <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="50%" ItemStyle-Wrap="false"/>  

                    </Columns>  
                </asp:GridView>  

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
         ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT [filesdir_i],[Filename], [Category], [Description] FROM [filesdir] order by (Year*100+ Month) desc "
       DeleteCommand="DELETE FROM filesdir WHERE filesdir_i = @filesdir_i"  >


<DeleteParameters>
    <asp:Parameter Name="filesdir_i"/>
</DeleteParameters> 

    </asp:SqlDataSource>
   
    </asp:Content>