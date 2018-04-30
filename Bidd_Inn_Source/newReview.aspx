<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="newReview.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <asp:Label ID="starLBL" runat="server" Font-Names="Cooper Black" Font-Size="Large" Text="Star Value:"></asp:Label>
    <asp:RadioButtonList ID="starBTN" runat="server" Font-Names="Algerian" Font-Size="X-Large" ForeColor="#009900" RepeatDirection="Horizontal" Width="563px">
        <asp:ListItem>0</asp:ListItem>
        <asp:ListItem>1</asp:ListItem>
        <asp:ListItem>2</asp:ListItem>
        <asp:ListItem>3</asp:ListItem>
        <asp:ListItem>4</asp:ListItem>
        <asp:ListItem>5</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <br />
    <asp:Label ID="comLBL" runat="server" Font-Bold="True" Font-Names="Cooper Black" Font-Size="Large" Text="Comments:"></asp:Label>
    <br />
    <asp:TextBox ID="comTXT" runat="server" Height="324px" TextMode="MultiLine" Width="544px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="cBTN" runat="server" Font-Bold="True" Font-Names="Bahnschrift SemiBold" Font-Size="Large" Height="49px" OnClick="cBTN_Click" Text="Submit" Width="94px" />
</asp:Content>

