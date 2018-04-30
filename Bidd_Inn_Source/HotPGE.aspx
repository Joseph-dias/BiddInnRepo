<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="HotPGE.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="logoutBTN" runat="server" Font-Size="Medium" OnClick="logoutBTN_Click">Logout</asp:LinkButton>
    <br />
</p>
<p>
    <asp:ImageButton ID="addBTN" runat="server" BorderStyle="Double" BorderWidth="2px" Height="50px" ImageUrl="~/SiteImages/Plus hotel.jpg" PostBackUrl="~/addRoom.aspx" Width="50px" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <p>
    <asp:Button ID="offerBTN" runat="server" Height="50px" Text="0" Width="50px" OnClick="offerBTN_Click" BackColor="#CC0000" BorderStyle="Double" Font-Bold="True" Font-Size="Large" ForeColor="#FFCC00" />
    </p>
    <p>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="custNME" runat="server" Font-Bold="True" Font-Names="Bernard MT Condensed" Font-Size="XX-Large" Text="NAME"></asp:Label>
    </p>
    <asp:UpdatePanel ID="UpdatePicPNL" runat="server" OnUnload="UpdatePicPNL_Unload" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Image ID="mainIMG" runat="server" Height="210px" ImageUrl="~/SiteImages/man silhouette.png" Width="213px" BorderStyle="Solid" BorderWidth="5px" ImageAlign="Middle" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="infoTXT" runat="server" BorderStyle="None" Columns="2" Height="209px" ReadOnly="True" Rows="3" TextMode="MultiLine" Width="281px" style="overflow:hidden;resize:none" BackColor="White" Enabled="False" Font-Size="Medium" Font-Bold="True" Font-Names="Algerian">


ADDRESS</asp:TextBox>
            &nbsp;&nbsp;&nbsp;
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <br />
    <asp:Button ID="uploadBTN" runat="server" OnClick="uploadBTN_Click" Text="UPLOAD JPG" />
    <br />
    <br />
    <asp:Label ID="avgLBL" runat="server" Font-Bold="True" Font-Names="Algerian" Font-Size="Medium" Text="AVERAGE RATING:" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Label ID="avgTXT" runat="server" Font-Bold="True" Font-Names="Arial Black" Font-Size="XX-Large" Text="##" Visible="False"></asp:Label>
    <br />
    <br />
    <br />
    <asp:Button ID="rBTN" runat="server" OnClick="rBTN_Click" Text="Submit Review" Width="108px" />
    <br />
    <br />
    <asp:Table ID="reviewTBL" runat="server" Height="99px" Width="1307px">
    </asp:Table>
</asp:Content>

