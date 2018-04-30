<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="offerInfo.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:LinkButton ID="nmeBTN" runat="server" Font-Names="Algerian" Font-Size="XX-Large" OnClick="nmeBTN_Click">NAME</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="roomTXT" runat="server" OnClick="roomTXT_Click">Room</asp:LinkButton>
    <br />
    <br />
    <asp:TextBox ID="infoTXT" runat="server" Font-Size="Medium" Height="328px" ReadOnly="True" TextMode="MultiLine" style="overflow:hidden;resize:none" Width="1103px" BorderStyle="None" Wrap="False" CssClass="txtSize" Enabled="False" Font-Names="Bauhaus 93">Offer Info</asp:TextBox>
    <br />
    <br />
    <br />
</asp:Content>

