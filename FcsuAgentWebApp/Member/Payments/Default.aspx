﻿<%@ Page Language="C#" MasterPageFile="~/Payment.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FcsuAgentWebApp.Member.Payments.Default" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style type="text/css">
        .Hide {
            display: none;
        }

        .column1 {
            float: left;
            width: 50%;
        }

        .column2 {
            float: right;
            width: 50%;
        }

        
    </style>

</asp:Content>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="checkout-status">
        <img src='../images/PP-payment.jpg' width='403px' height='31px' alt='Shopping Cart' /><br />
        <br />
        <br />

    </div>




    <div class="row">
        <!-- column1 is for PayPal -->
        <div class="column1">
            <div style="width: 80%; float: left; margin-left: 20px">
                <asp:Label ID="Label1" runat="server" Text="Select Payment Option" Style="text-align: center; margin-bottom: 15px;" Font-Size="16" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
                

                <script src="https://www.paypal.com/sdk/js?client-id=AWxMwR_BHnLSoq4F6inV027CkYVA66hXMFwzMupxl67oi7b6Idvyh-cBTwv9K-zGkr6zRz9VzaFp9mVb" data-page-type="checkout"> // Use this for the sandbox testing </script>
                <%--<script src="https://www.paypal.com/sdk/js?client-id=AS926yHAwMmIHpHJVgTwEKGqNuVGWsBw3ODVywK6OY2SG-S7u4M7aUSC4oIiIox34JgVkQqbLizsAKck" data-page-type="checkout"> // Use this for the production mode</script>--%>
                


                <div id="paypal-button-container"></div>


                <!-- Add the checkout buttons, set up the order and approve the order -->

                <script>

                    paypal.Buttons({

                        createOrder: function (data, actions) {

                            return actions.order.create({

                                purchase_units: [{
                                    
                                    description: "<%=Session["orderID"]%>",

                                    
                                    amount: {
                                        currency_code: "USD",
                                        value: "<%=Session["cartTotal"]%>",
                                        breakdown: {
                                            item_total: {
                                                currency_code: "USD",
                                                value: "<%=Session["cartTotal"]%>"
                                            }


                                        }
                                    },
                                    items: [<%=Session["cartItems"]%>],

                                }]
                            })

                        },

                        onApprove: function (data, actions) {

                            return actions.order.capture().then(function (details) {

                                // Send the PayPal Transaction ID to the completion screen
                                document.getElementById('JSON').value = details.purchase_units[0].payments.captures[0].id;
                                var myform = document.getElementById('form_id');
                                myform.submit();

                            });

                        }

                    }).render('#paypal-button-container'); // Display payment options on your web page

                </script>

            </div>

        </div>


        <!-- column2 is for the Order Summary -->
        <div class="column2">
            <asp:Label ID="Label2" runat="server" Text="Selected Payment Summary" Style="text-align: center; margin-bottom: 15px;" Font-Size="16" Width="100%" ForeColor="Navy" EnableViewState="false"></asp:Label>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="rounded-corners" style="width: 80%; float: right; margin-right: 40px">


                        <asp:Label ID="Label3a" runat="server" Text="Label" Visible="False"></asp:Label>
                        <asp:Label ID="Error_Message" runat="server" Text="Label" Visible="False"></asp:Label>


                        <asp:GridView ID="gv_summary" CssClass="Grid" runat="server" Width="100%"
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
                                        <div class="grid_view_main_bold"><%#Eval("policy")%></div>
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
                            <asp:Label ID="Label12" runat="server" Text="Total:" CssClass="total_lbl_payment"></asp:Label><asp:Label ID="lbl_total" runat="server" Text="" CssClass="total_lbl_money_bold"></asp:Label>
                        </div>


                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <%-- Form to transport the data to the completion page --%>

    <div visible="false">
        <%--<form id="form-id2" action="../Complete/Default.aspx" method="post" enctype="multipart/form-data">--%>


        <input type="hidden" id="JSON" name="JSON" />

        <%--<input type="submit" value="Submit" />--%>
        <%--</form>--%>
    </div>



</asp:Content>

