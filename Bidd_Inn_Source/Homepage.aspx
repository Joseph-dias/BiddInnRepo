<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Panel ID="Panel1" runat="server" BackImageUrl="~/SiteImages/hotel pool.jpg" Height="100%" Width="100%" Font-Bold="True">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="custBTN" runat="server" BackColor="#FFCC00" BorderStyle="Groove" Font-Bold="True" Font-Size="Large" Height="57px" PostBackUrl="~/LoginFormCust.aspx" Text="Sign in as a Customer" Width="267px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="hotBTN" runat="server" BackColor="Lime" BorderStyle="Groove" Font-Bold="True" Font-Size="Large" Height="57px" PostBackUrl="~/LoginFormHot.aspx" Text="Sign in as a Hotel" Width="242px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="custBTN0" runat="server" BackColor="#FFCC00" BorderStyle="Groove" Font-Bold="True" Font-Size="Large" Height="36px" PostBackUrl="~/addCust.aspx" Text="Sign Up" Width="116px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="custBTN2" runat="server" BackColor="Lime" BorderStyle="Groove" Font-Bold="True" Font-Size="Large" Height="36px" PostBackUrl="~/addHotel.aspx" Text="Sign Up" Width="116px" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="ttleLBL" runat="server" Font-Bold="True" Font-Names="Impact" Font-Size="XX-Large" ForeColor="#FFFF66" Text="Welcome to Bidd Inn"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <asp:TextBox ID="descTXT" runat="server" BackColor="#3399FF" style="overflow:hidden;resize:none" Enabled="False" Height="191px" TextMode="MultiLine" Width="1415px" Font-Size="Medium" ForeColor="#FFFF66">This is your one-stop for all of your hotel needs.

For Customers:
Simply sign up, go to rooms, click on your desired room&#39;s name, scroll down, and make an offer.

For Hotels:
Simply sign up and start adding your rooms to the list.</asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Label ID="custNumber" runat="server" BackColor="#3399FF" Font-Size="Medium" ForeColor="#FFFF66" Text="Number of Customers: "></asp:Label>
        <asp:Label ID="cNum" runat="server" ClientIDMode="Static" BackColor="#3399FF" Font-Size="Medium" ForeColor="#FFFF66" Text="####"></asp:Label>
        &nbsp;
        <asp:Label ID="hotNumber" runat="server" BackColor="#3399FF" Font-Size="Medium" ForeColor="#FFFF66" Text="Number of Hotels: "></asp:Label>
        <asp:Label ID="hNum" runat="server" ClientIDMode="Static" BackColor="#3399FF" Font-Size="Medium" ForeColor="#FFFF66" Text="####"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
</asp:Panel>
    <br />
    <br />
&nbsp;
    <script src="Scripts/myScript.js"></script>
</asp:Content>
