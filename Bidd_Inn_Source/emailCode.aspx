<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="emailCode.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <br />
    </p>
    <p>
        <asp:Label ID="codeLBL" runat="server" Font-Bold="True" Text="One-Time Code: "></asp:Label>
        <asp:TextBox ID="codeTXT" runat="server" Width="146px"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="newPSS" runat="server" Font-Bold="True" Text="New Password: "></asp:Label>
&nbsp;
        <asp:TextBox ID="passTXT" runat="server" Width="146px"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="smtBTN" runat="server" OnClick="smtBTN_Click" Text="Submit" />
    </p>
</asp:Content>

