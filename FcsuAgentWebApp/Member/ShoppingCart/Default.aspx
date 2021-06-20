<%@ Page Language="C#" MasterPageFile="~/Cart.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FcsuAgentWebApp.Member.ShoppingCart.Default" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style type="text/css">
        .Hide {
            display: none;
        }
    </style>

</asp:Content>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="checkout-status">
        <img src='../images/PP-shopping-cart.jpg' width='403px' height='31px' alt='Shopping Cart' /><br />
        <br />

    </div>

    <!-- --------------------------------------------------------------------------- -->
    <!-- Grid that will show the shopping cart -->
    <!-- --------------------------------------------------------------------------- -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="rounded-corners" style="width: 80%">


                <asp:Label ID="Label3a" runat="server" Text="Label" Visible="False"></asp:Label>
                <asp:Label ID="Error_Message" runat="server" Text="Label" Visible="False"></asp:Label>


                <asp:GridView ID="gv_cart" CssClass="Grid" runat="server" Width="100%"
                    HeaderStyle-BackColor="#E4E4E4" OnRowCommand="gv_cart_RowCommand"
                    RowStyle-BackColor="White" AlternatingRowStyle-BackColor="White" ShowFooter="False"
                    AutoGenerateColumns="False" HorizontalAlign="Right"
                    HeaderStyle-BorderWidth="0px" HeaderStyle-BorderStyle="None"
                    RowStyle-CssClass="articleborder" AlternatingRowStyle-CssClass="articleborder"
                    GridLines="None">
                    <HeaderStyle CssClass="grid_view_header" />

                    <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

                    <Columns>

                        <asp:TemplateField HeaderText="Policy">
                            <ItemTemplate>
                                <div class="grid_view_main_bold_center"><%#Eval("policy")%></div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="polDescr" HeaderText="Policy Desc" InsertVisible="false"
                            Visible="true" >
                            
                            <ItemStyle CssClass="grid_view_main_bold" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amountPaid" HeaderText="Payment" InsertVisible="false"
                            SortExpression="amountPaid" Visible="true" DataFormatString="{0:c}"
                            HeaderStyle-CssClass="Hide">
                            <HeaderStyle CssClass="Hide" />
                            <ItemStyle CssClass="Hide" />
                        </asp:BoundField>


                        <asp:TemplateField HeaderText="Qty" Visible="false">
                            <ItemTemplate>
                               
                            </ItemTemplate>
                            <ItemStyle CssClass="Qty_ItemStyle" />

                            <FooterTemplate>
                            </FooterTemplate>
                            <FooterStyle CssClass="Qty_FooterStyle" />

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Shipping" Visible="false">
                            <ItemTemplate>


                            </ItemTemplate>
                            <HeaderStyle CssClass="header_shipping" />
                            <ItemStyle CssClass="Shipping_ItemStyle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Payment"
                            ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("amountPaid", "{0:c}") %>
                            </ItemTemplate>
                            <ItemStyle CssClass="Total_ItemStyle" />
                        </asp:TemplateField>


                        <asp:BoundField DataField="payhist_i" HeaderText="id" InsertVisible="False"
                            ReadOnly="True" SortExpression="payhist_i" ItemStyle-CssClass="Hide"
                            HeaderStyle-CssClass="Hide">
                            <HeaderStyle CssClass="Hide" />
                            <ItemStyle CssClass="Hide" />
                        </asp:BoundField>

                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>


                                <asp:ImageButton ID="btnRemove" runat="server"
                                    OnClientClick="return confirm('Are you certain you want to remove this payment from your shopping cart?  If this is a Surcharge it will not be removed.');"
                                    ImageUrl="../images/delete.jpg" Height="32px" Width="30px"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    CommandName="cmdRemove" CssClass="bttn_qty" />

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle CssClass="Ck_ViewCart_FooterStyle" />
                    <RowStyle BackColor="White"></RowStyle>
                </asp:GridView>

                <div class="total_value">

                    <br />
                    <br>
                    <asp:Label ID="Label12" runat="server" Text="Total:" CssClass="total_lbl"></asp:Label><asp:Label ID="lbl_total" runat="server" Text="" CssClass="total_lbl_money_bold"></asp:Label>
                </div>


            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- --------------------------------------------------------------------------- -->
    <!-- End Grid that will show the shopping cart -->
    <!-- --------------------------------------------------------------------------- -->



    <!-- --------------------------------------------------------------------------- -->
    <!-- Next and Previous Buttons just below the grid -->
    <!-- --------------------------------------------------------------------------- -->
    <div class="bttn_area">
        <div id="left_column">
            <asp:ImageButton ID="ImageButton1" runat="server"
                ImageUrl="../images/previous.jpg" CssClass="bttn_previous"
                Height="48px" Width="128px" CommandName="cmdPrevious" 
                OnClick="btnPrevious_Click" />
        </div>

        <div id="center_column">
        </div>

        <div id="right_column">
            <asp:ImageButton ID="btnCart" runat="server"
                ImageUrl="../images/next.jpg"
                Height="48px" Width="128px" CommandName="cmdUpdate" CssClass="bttn_checkout"
                OnClick="btnCart_Click" />
        </div>

    </div>
    <!-- --------------------------------------------------------------------------- -->
    <!-- End Next and Previous Buttons just below the grid -->
    <!-- --------------------------------------------------------------------------- -->

</asp:Content>

