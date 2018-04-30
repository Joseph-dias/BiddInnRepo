<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="rmPGE.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="roomNMELBL" runat="server" Font-Names="Algerian" Font-Size="XX-Large" Text="[ROOM NAME]"></asp:Label>
&nbsp;&nbsp;
    <asp:Label ID="hotelLBL" runat="server" Font-Italic="True" Font-Names="Britannic Bold" Font-Size="Medium" Text="(HOTEL)"></asp:Label>
    <br />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="descLBL" runat="server" Text="Description:"></asp:Label>
&nbsp;&nbsp;&nbsp;
    <asp:ImageButton ID="editBTN" runat="server" Height="26px" ImageUrl="~/SiteImages/pencil.png" Visible="False" Width="24px" OnClick="editBTN_Click" />
    <br />
    <br />
    <asp:Image ID="rmPC" runat="server" Height="300px" Width="300px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="descTXT" runat="server" Height="289px" ReadOnly="True" TextMode="MultiLine" Width="366px" style="overflow:hidden;resize:none" BackColor="White" BorderStyle="None" Enabled="False"></asp:TextBox>
    <br />
    <br />
    <br />
    <br />
    <asp:Label ID="askedPRCLBL" runat="server" Text="Wanted Price/day: "></asp:Label>
&nbsp;<asp:Label ID="priceLBL" runat="server" Text="$$"></asp:Label>
    <br />
    <br />
    <br />
    <asp:Label ID="offerLBL" runat="server" Font-Names="Bodoni MT Black" Font-Size="Large" Text="Submit an offer:"></asp:Label>
    <br />
    <br />
    <br />
    <asp:Label ID="fnLBL" runat="server" Text="First Night:"></asp:Label>
    <asp:Calendar ID="fCAL" runat="server" OnSelectionChanged="fCAL_SelectionChanged"></asp:Calendar>
    <br />
    <br />
    <br />
    <asp:Label ID="lnLBL" runat="server" Text="Checkout:"></asp:Label>
    <asp:Calendar ID="lCAL" runat="server" OnSelectionChanged="lCAL_SelectionChanged"></asp:Calendar>
    <br />
    <br />
    <asp:Label ID="offerPRCLBL" runat="server" Text="Offer a price per day (Per Room):"></asp:Label>
    &nbsp;
    <asp:Label ID="dollarLBL" runat="server" Text="$"></asp:Label>
    <asp:TextBox ID="offerPRC" runat="server" Width="96px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="numRMLBL" runat="server" Text="How many rooms?:"></asp:Label>
&nbsp;<asp:DropDownList ID="numDRP" runat="server" Height="50px" Width="70px">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="errorTXT" runat="server" ForeColor="Red" Text="Oops.  Something went wrong." Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Button ID="submitBTN" runat="server" BackColor="#FF6600" Font-Bold="True" Font-Names="AR DELANEY" Height="40px" Text="SUBMIT OFFER" Width="137px" OnClick="submitBTN_Click" />
</asp:Content>

