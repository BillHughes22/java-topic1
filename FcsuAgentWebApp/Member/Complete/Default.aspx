<%@ Page Language="C#" MasterPageFile="~/Cart.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FcsuAgentWebApp.Member.Complete.Default" %>


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
        <img src='../images/PP-summary.jpg' width='403px' height='31px' alt='Shopping Cart' /><br />
        <asp:Label ID="Label4" runat="server" Text="Payment Completed Successfully" ></asp:Label>
        <br />

    </div>


    <div class="printing">
        <asp:Label ID="Label3" runat="server" Text="Print This Page For Your Records" CssClass="printlbl"></asp:Label><br />
        <br />

    </div>




    <!-- --------------------------------------------------------------------------- -->
    <!-- Summary Information -->
    <!-- --------------------------------------------------------------------------- -->
    <div class="bttn_area">
        <div id="left_column">
            <div class="buyer_column">

                <br />
                <asp:Label ID="Label2" runat="server" Text="Buyer Information" CssClass="buyerHeader"></asp:Label>
                <br>
                <asp:Label ID="lbluserName" runat="server" Text="" CssClass="userName"></asp:Label>
                <asp:Label ID="lbluserAddress" runat="server" Text="" CssClass="userName2"></asp:Label>
            </div>
        </div>

        

        <div id="payment_right_column">
            <div class="transaction_column">

                <br />
                <asp:Label ID="CurrentDate" runat="server" Text="" CssClass="datelbl"></asp:Label>
                <br>
                <asp:Label ID="Label1" runat="server" Text="Transaction ID:" CssClass="transactionIDlbl"></asp:Label><asp:Label ID="Transaction_ID_lbl" runat="server" Text="" CssClass="transactionID"></asp:Label>
            </div>
        </div>

    </div>
    <!-- --------------------------------------------------------------------------- -->
    <!-- End Summary Information -->
    <!-- --------------------------------------------------------------------------- -->



    


    <!-- --------------------------------------------------------------------------- -->
    <!-- Grid that will show the shopping cart -->
    <!-- --------------------------------------------------------------------------- -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="rounded-corners" style="width: 80%">


                <asp:Label ID="Label3a" runat="server" Text="Label" Visible="False"></asp:Label>
                <asp:Label ID="Error_Message" runat="server" Text="Label" Visible="False"></asp:Label>


                <asp:GridView ID="gv_cart" CssClass="Grid" runat="server" Width="100%"
                    HeaderStyle-BackColor="#E4E4E4" 
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

                        <asp:TemplateField HeaderText="Policy Desc">
                            <ItemTemplate>
                                <div class="grid_view_main_bold"><%#Eval("polDescr")%></div>
                            </ItemTemplate>
                        </asp:TemplateField>

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


    <asp:Label ID="Label5" runat="server" style="color:red" Text="Allow 48 hours for payment to post to your account"></asp:Label>
    <br />


    <br />
    <asp:Button CssClass="PaymentBtn" ID="bttnReturn" runat="server" Width="30%" CausesValidation="false" OnClick="bttnReturn_Click" Text="Return to Account" />
    <br /><br />

</asp:Content>

