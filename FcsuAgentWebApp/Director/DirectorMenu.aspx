 <%@ Page Title="Director Page" Language="C#" AutoEventWireup="true" 
    MasterPageFile="~/Site.Master" CodeBehind="DirectorMenu.aspx.cs" Inherits="FcsuAgentWebApp.Director.DirectorMenu" %>

<%@ MasterType VirtualPath="~/Site.Master" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   
     <style type="text/css">
       .row {
  display: block;
 background: #1a3187;

        }
       h3 {
      
    display: inline-block;
    font-size: 1.175rem;
    font-weight: 700;
    text-transform: uppercase;
}
       h1, h2, h3, h4, h5, h6 {
     color: white;
    font-family: Lato,"Helvetica Neue",Helvetica,Roboto,Arial,sans-serif;
    font-style: normal;
    font-weight: 400;
    line-height: 1.4;
    margin-bottom: .5rem;
    margin-top: .2rem;
    text-rendering: optimizeLegibility;
}
       
  .btn
  {

    color: #ddb547;
  }

a:link{
    text-decoration: none;
      color: #585858;
      font-family: inherit;
     font-family: inherit;
}
a:visited{
    text-decoration: none;
     color: #585858;
font-family: inherit;
} 

a:hover{
    text-decoration: none;
    font-family: inherit;
      color: #585858;
} 
a:active{
    text-decoration: none;
      color: #585858;
      font-family: inherit;
} 


</style>

    </asp:Content>
      
         
           
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 

    

         <div class="row">
<div class="small-12 medium-11 medium-centered columns">
<h3 id="directorheader" runat="server" title="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BOARD OF DIRECTORS MEETING</h3>
</div>
</div>
    <div >
        
   <asp:BulletedList  ID="bListDirector" runat="server" DisplayMode="HyperLink" BulletStyle="Disc" Height="16px" Target="_blank" Font-Size="Large" >
    </asp:BulletedList>      
        <asp:Button class="btn" ID="btnmoreDirector" runat="server" Text="ShowMore" BorderColor="white"  OnClick="btnmoreDirector_Click" Font-Bold="True" BorderStyle="None" />
       
    </div>
 <br />
<div class="row">
<div class="small-12 medium-11 medium-centered columns">
<h3 id="executiveheader" runat="server" title="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EXECUTIVE COMMITTEE MEETING</h3>
</div>
</div>
<div >

    <asp:BulletedList ID="bListExecutive" runat="server" DisplayMode="HyperLink" BulletStyle="Disc" Height="16px" Target="_blank" Font-Size="Large" TabIndex="2" >
    </asp:BulletedList>   
  <asp:Button class="btn" ID="btnmoreExecutive" runat="server" Text="ShowMore" BorderColor="white" OnClick="btnmoreExecutive_Click" Font-Bold="True" BorderStyle="None" />
    
</div>
    <br />
<div class="row">
<div class="small-12 medium-11 medium-centered columns">
<h3 id="announceheader" runat="server" title="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Announcements</h3>
</div>
</div> 
    <div >     
     <asp:BulletedList ID="bListAnnouncements" runat="server"  DisplayMode="HyperLink" BulletStyle="Disc" Height="16px" Target="_blank" Font-Size="Large" >
    </asp:BulletedList>   
   <asp:Button class="btn" ID="btnmoreAnnouncement" runat="server" Text="ShowMore" BorderColor="white" OnClick="btnmoreAnnouncement_Click" Font-Bold="True" BorderStyle="None" />
  </div>
    <br />
    <div class="row">
<div class="small-12 medium-11 medium-centered columns">
<h3 id="miscellaneousheader" runat="server" title="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Miscellaneous</h3>
</div>
</div> 
    <div >     
     <asp:BulletedList ID="bListMiscellaneous" runat="server"  DisplayMode="HyperLink" BulletStyle="Disc" Height="16px" Target="_blank" Font-Size="Large" >
    </asp:BulletedList>   
   <asp:Button class="btn" ID="btnmoreMiscellaneous" runat="server" Text="ShowMore" BorderColor="white" OnClick="btnmoreMiscellaneous_Click" Font-Bold="True" BorderStyle="None" />
  </div>
      <br />
   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
   
   <%--<iframe width="400" height="280" src="https://www.youtube.com/embed/u1qMIqJbE5w?rel=0" frameborder="0" allowfullscreen=""></iframe>--%>
         
                         <%--  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

   
<asp:PlaceHolder ID="iframeDiv" runat="server"  />--%>

 
   

 

  
    

</asp:Content>
  