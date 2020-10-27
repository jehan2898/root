<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewContract.aspx.cs" Inherits="AJAX_Pages_ViewContract" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Agent/Css/main-ch.css" rel="stylesheet" />
    <link href="../Agent/Css/main-ff.css" rel="stylesheet" />
    <link href="../Agent/Css/main-ie.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div  style="height:410px; overflow:scroll;">
    <asp:GridView ID="grdContract" runat="server" CssClass="mGrid" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Code" HeaderStyle-HorizontalAlign="center" HeaderText="Code" Visible="true"></asp:BoundField>
            <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="center" HeaderText="Description" Visible="true"></asp:BoundField>
            <asp:BoundField DataField="InsuranceName" HeaderStyle-HorizontalAlign="center" HeaderText="Insurance Name" Visible="true"></asp:BoundField>
            <asp:BoundField DataField="ContractAmount" DataFormatString="{0:C}" HeaderStyle-HorizontalAlign="center" HeaderText="Contract Amount" Visible="true"></asp:BoundField>
            <asp:BoundField DataField="ContractDate" HeaderStyle-HorizontalAlign="center" HeaderText="Contract Date" Visible="true"></asp:BoundField>
        </Columns>

    </asp:GridView>
    </div>
    </form>
</body>
</html>
