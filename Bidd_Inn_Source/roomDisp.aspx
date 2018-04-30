<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="roomDisp.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="fLBL" runat="server" Font-Bold="True" Text="First Night:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="fLBL0" runat="server" Font-Bold="True" Text="Checkout:"></asp:Label>
    <br />
    <asp:Label ID="filterLBL" runat="server" Text="Filter By Date: "></asp:Label>
&nbsp;<asp:TextBox ID="fMTXT" runat="server" Height="16px" TextMode="Number" Width="40px"></asp:TextBox>
&nbsp;/
    <asp:TextBox ID="fDTXT" runat="server" Height="16px" TextMode="Number" Width="40px"></asp:TextBox>
&nbsp;/
    <asp:TextBox ID="fYTXT" runat="server" Height="16px" TextMode="Number" Width="52px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="lMTXT" runat="server" Height="16px" TextMode="Number" Width="40px"></asp:TextBox>
&nbsp;/
    <asp:TextBox ID="lDTXT1" runat="server" Height="16px" TextMode="Number" Width="40px"></asp:TextBox>
&nbsp;/
    <asp:TextBox ID="lYTXT" runat="server" Height="16px" TextMode="Number" Width="52px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="locationDRP" runat="server" Height="21px" OnSelectedIndexChanged="locationDRP_SelectedIndexChanged" Width="184px">
        <asp:ListItem Selected="True">Location</asp:ListItem>
    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="filterBTN" runat="server" OnClick="filterBTN_Click" Text="Filter" />
&nbsp;
    <asp:Button ID="resetBTN" runat="server" OnClick="resetBTN_Click" Text="Reset" Width="51px" />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="errorTXT" runat="server" ForeColor="Red" Text="Invalid Input" Visible="False"></asp:Label>
    <br />
    <asp:Table ID="roomTBL" runat="server" Height="78px" Width="1307px">
    </asp:Table>
</asp:Content>

