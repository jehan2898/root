<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_pom_popup.aspx.cs"
    Inherits="AJAX_Pages_Report_pom_popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="framehead">
    <title>POM POPUP</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
        </asp:ScriptManager>
        <asp:DataGrid ID="grdPaymentTransaction" runat="server" Width="100%" AutoGenerateColumns="false"
            CssClass="mGrid" GridLines="None">
            <FooterStyle />
            <SelectedItemStyle />
            <PagerStyle CssClass="pgr" />
            <AlternatingItemStyle BackColor="#EEEEEE" />
            <ItemStyle CssClass="GridRow" />
            <Columns>
                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No" Visible="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:MM/dd/yyyy}">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No"></asp:BoundColumn>
                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
            </Columns>
            <HeaderStyle CssClass="GridViewHeader" BackColor="#B5DF82" Font-Bold="true" />
        </asp:DataGrid>
    </form>
</body>
</html>
