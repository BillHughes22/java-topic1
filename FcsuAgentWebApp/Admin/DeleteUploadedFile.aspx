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
         AutoGenerateColumns="False" DataKeyNames="filesdir_i" Width="559px"  ShowGridLines="False" AllowPaging="True" PageSize="25" 
         CssClass="auto-style1" OnRowDeleting="GridFilesDir_RowDeleting"  >  
                    <Columns >  
                         <asp:CommandField ShowEditButton="True" />
                         <asp:TemplateField>
                    <ItemTemplate >
                        <%--ADD THE DELETE LINK BUTTON--%>
                        <asp:LinkButton Runat="server" 
                            OnClientClick ="return confirm('Are you sure you want to delete ?');"
                            CommandName="Delete" >Delete</asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:BoundField DataField="filesdir_i" HeaderText="S.No." Visible="False" />  
                        <asp:BoundField DataField="Filename" HeaderText="Filename" ItemStyle-Width="50%" ItemStyle-Wrap="false" ReadOnly="true">  
<ItemStyle Wrap="False" Width="50%"></ItemStyle>
                         </asp:BoundField>
                        <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Width="50%" ItemStyle-Wrap="false" ReadOnly="true">  
<ItemStyle Wrap="False" Width="50%"></ItemStyle>
                         </asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="50%" ItemStyle-Wrap="false" ReadOnly="true">  
<ItemStyle Wrap="False" Width="50%"></ItemStyle>
                         </asp:BoundField>

                    <asp:TemplateField HeaderText="Month">
            <ItemTemplate>
                <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Month")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlMonth" runat="server" 
                 DataTextField="month"
                  DataValueField="month"
                  SelectedValue='<%# Bind("month") %>'
                  AppendDataBoundItems="True" >
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                  <asp:ListItem Text="2" Value="2"></asp:ListItem>
                  <asp:ListItem Text="3" Value="3"></asp:ListItem>
                  <asp:ListItem Text="4" Value="4"></asp:ListItem>
                  <asp:ListItem Text="5" Value="5"></asp:ListItem>
                  <asp:ListItem Text="6" Value="6"></asp:ListItem>
                     <asp:ListItem Text="7" Value="7"></asp:ListItem>
                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                     <asp:ListItem Text="9" Value="9"></asp:ListItem>
                     <asp:ListItem Text="10" Value="10"></asp:ListItem>
                     <asp:ListItem Text="11" Value="11"></asp:ListItem>
                     <asp:ListItem Text="12" Value="12"></asp:ListItem>
                     
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
                        
                   

                         <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Uploaded Year">
                             <EditItemTemplate>
                                 <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("Year") %>'>
                                                                           <asp:ListItem value="2022" Text="2022"></asp:ListItem>

                                      <asp:ListItem value="2021" Text="2021"></asp:ListItem>
                                      <asp:ListItem value="2020" Text="2020"></asp:ListItem>
                                     <asp:ListItem value="2019" Text="2019"></asp:ListItem>
                                     <asp:ListItem value="2018" Text="2018"></asp:ListItem>
                                     <asp:ListItem value="2017" Text="2017"></asp:ListItem>
                                     <asp:ListItem value="2016" Text="2016"></asp:ListItem>
                                      <asp:ListItem value="2015" Text="2015"></asp:ListItem>
                                      <asp:ListItem value="2014" Text="2014"></asp:ListItem>
                                      <asp:ListItem value="2013" Text="2013"></asp:ListItem>
                                     <asp:ListItem value="2012" Text="2012"></asp:ListItem>
                                      <asp:ListItem value="2011" Text="2011"></asp:ListItem>

                                 </asp:DropDownList>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# Bind("Year") %>'></asp:Label>
                             </ItemTemplate>
                             <ItemStyle Width="50%" Wrap="False" />
                         </asp:TemplateField>
                        
                    </Columns>  

<HeaderStyle BackColor="BlanchedAlmond"></HeaderStyle>

<RowStyle BorderColor="Brown"></RowStyle>

<SelectedRowStyle BackColor="#A46CC0" ForeColor="Gold"></SelectedRowStyle>
                </asp:GridView>  

     <asp:Label ID="ErrMsg" runat="server" Text=""></asp:Label>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT [filesdir_i],[Filename], [Category], [Description], [Month], [Year] FROM [filesdir] order by (Year*100+ Month) desc "
        DeleteCommand="DELETE FROM filesdir WHERE filesdir_i = @filesdir_i" 
        UpdateCommand="Update filesdir set Year=@Year, Month= @Month WHERE filesdir_i = @filesdir_i" >


<DeleteParameters>
    <asp:Parameter Name="filesdir_i"/>
</DeleteParameters> 

<UpdateParameters>
    
    <asp:Parameter Name="filesdir_i"/>   
    <asp:Parameter Name="Year" Type="Int32"/>
    <asp:Parameter Name="Month" Type="Int32"/>

</UpdateParameters> 

    </asp:SqlDataSource>
   
    </asp:Content>


