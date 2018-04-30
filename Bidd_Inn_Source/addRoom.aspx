<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="addRoom.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="titleLBL" runat="server" Font-Bold="True" Font-Names="Castellar" Font-Size="XX-Large" ForeColor="#FF9966" Text="ADD A ROOM"></asp:Label>
    <br />
    <br />
    <asp:Label ID="picLBL" runat="server" Font-Bold="True" Font-Names="Bodoni MT Black" Font-Size="Large" Text="Picture (JPG only): "></asp:Label>
    <br />
    <asp:FileUpload ID="pcUpload" runat="server" Width="293px" />
    <br />
    <asp:Label ID="priceLBL" runat="server" Font-Bold="True" Font-Names="Bodoni MT Black" Font-Size="Large" Text="What Price Would You Like? (Per Day): "></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="$"></asp:Label>
&nbsp;
    <asp:TextBox ID="priceTXT" runat="server" Width="60px">000.00</asp:TextBox>
    <br />
    <asp:Label ID="nameLBL" runat="server" Font-Bold="True" Font-Names="Bodoni MT Black" Font-Size="Large" Text="Name the Room: "></asp:Label>
    <asp:TextBox ID="nameTXT" runat="server" MaxLength="40" Width="347px"></asp:TextBox>
    <br />
    <asp:Label ID="nameLBL0" runat="server" Font-Bold="True" Font-Names="Bodoni MT Black" Font-Size="Large" Text="How many of these rooms do you want to list?:  "></asp:Label>
    <asp:TextBox ID="numTXT" runat="server" TextMode="Number" Width="39px">1</asp:TextBox>
    <br />
    <asp:Label ID="descLBL" runat="server" Font-Bold="True" Font-Names="Bodoni MT Black" Font-Size="Large" Text="Room Description:"></asp:Label>
    &nbsp;<asp:TextBox ID="descTXT" runat="server" MaxLength="300" Width="347px" Height="166px" TextMode="MultiLine"></asp:TextBox>
    <br />
    <asp:Label ID="errorTXT" runat="server" Font-Size="Large" ForeColor="Red" Text="Oops.  Invalid Input" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Button ID="submitBTN" runat="server" Font-Names="AR DESTINE" Font-Size="X-Large" OnClick="submitBTN_Click" Text="SUBMIT" />
</asp:Content>

