<%@ Page  Title="AutOut" Language="C#" AutoEventWireup="true" CodeBehind="AuthenticationOut.aspx.cs" MasterPageFile="~/Site.Master" 
    Inherits="FcsuAgentWebApp.AuthenticationOut" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
 
    <script type='text/javascript' src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type='text/javascript' charset='UTF-8' src="//cdn.ringcaptcha.com/widget/v2/bundle.min.js"></script>
  
    <style type="text/css">
        div.ringcaptcha.widget.no-brand {
  border: 0px; !important 
}

button.btn.btn-submit.btn-block.btn-verify.js-send-code {
  color: white; !important;
  background-color: green; !important
}
        .auto-style1 {
            height: 185px;
        }
        .auto-style2 {
            height: 185px;
            min-height: 420px;
            width: 1017px;
            margin-left: 8px;
            margin-right: 8px;
            margin-top: 0px;
            margin-bottom: 8px;
            padding: 0px 12px;
        }
        </style>
 </asp:Content>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label style="color:white;" runat="server" ID="lbl" Text="" ></asp:Label>

   <asp:Label style="color:white;" runat="server" ID="Label1" Text="" ></asp:Label>
     <asp:Label style="color:white;" runat="server" ID="nav" Text="" ></asp:Label>
    <div id="xyz" data-widget data-app="a4ekacaty4i4esijerib"
data-locale="en" data-mode="verification" data-type="sms" class="auto-style2">

    </div>
  

    <asp:ScriptManager ID="smMain" runat="server" EnablePageMethods="true" />
   <script type="text/javascript" >
       $(document).ready(function () {
           var appKey = "o4o7i3i9i4ajace4apu1";
           var USER_PHONE_VARAIBLE = $('#<%=lbl.ClientID%>').html();
           var widget = new RingCaptcha.Widget('#xyz', {
               app: appKey,
               events: {
                   // When widget is read
                   ready: function (event) {
                       $('input.form-control.phone-input.phone-input-verify').val(USER_PHONE_VARAIBLE);
                       $('input.form-control.phone-input.phone-input-verify').prop('disabled', true);
                   },
                   // Add JavaScript Callbacks
                   verified: function (event) {
                     
                       //PageMethods.CheckMethod();
                      // window.location.href = "ChangePasswordOut.aspx?name=" + $('#<%=Label1.ClientID%>').html();;
                       window.location.href = $('#<%=nav.ClientID%>').html();
                       const dataString = localStorage.getItem('ringcaptcha.widget.' + appKey);
                       const data = dataString ? JSON.parse(dataString) : null;
                       const phone = data.phoneNumber;
                       alert("Phone number verified:" + phone);
                       // Make the widget disappear
                       $('div.ringcaptcha.widget.no-brand').hide();
                       
                   }
               }
           }).setup();
       })

       
   </script>


   

     
    </asp:Content>
     
