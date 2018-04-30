<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="authenticateEmail.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="infoLBL" runat="server" Font-Bold="True" Font-Names="Algerian" Font-Size="XX-Large" ForeColor="Red" Text="Email sent!!  Please input the special code below"></asp:Label>
    <br />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Britannic Bold" Font-Size="Large" Text="Code:"></asp:Label>
&nbsp;
    <asp:TextBox ID="inputTXT" runat="server" Width="190px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="authenticateBTN" runat="server" BackColor="Blue" Font-Bold="True" ForeColor="Yellow" Height="31px" OnClick="authenticateBTN_Click" Text="Authenticate" Width="116px" />
    <br />
    <br />
</asp:Content>

