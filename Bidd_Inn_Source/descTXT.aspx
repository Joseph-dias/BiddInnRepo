<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="descTXT.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:TextBox ID="descTXT" runat="server" Height="270px" TextMode="MultiLine" Width="717px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="saveBTN" runat="server" Font-Bold="True" Font-Names="Bernard MT Condensed" Font-Size="Large" Height="34px" OnClick="saveBTN_Click" Text="Save" Width="79px" />
</asp:Content>

