<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ebiz_customer_profile.aspx.vb"
    Inherits="ebiz_customer_profile" %>

<!DOCTYPE html >
<html>
<head runat="server">
    <title>Customer Profiles</title>
    <link href="App_Themes/default/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="Panel5NOTES" runat="server" ScrollBars="Auto" CssClass="t_RegionHeader"   >
        <asp:Panel ID="PanelEXP5NOTES" runat="server">
            <asp:Label ID="Label1" runat="server" Text="" Font-Bold="True"></asp:Label>
            </asp:Panel>
        <asp:Panel ID="Panel57NOTES" runat="server" ScrollBars="Auto">
            <asp:DataGrid ID="dbg" runat="Server" AutoGenerateColumns="True" CellPadding="4"
                CellSpacing="0" Visible="true" CssClass="gridRowClsRep1" HeaderStyle-CssClass="gridhDRClsRep1"
                GridLines="None" AlternatingItemStyle-CssClass="gridAltRowClsRep1">
            </asp:DataGrid>
        </asp:Panel>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
