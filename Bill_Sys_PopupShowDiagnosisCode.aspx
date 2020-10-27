<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_PopupShowDiagnosisCode.aspx.cs"
    Inherits="Bill_Sys_PopupShowDiagnosisCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css"rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                     <td class="ContentLabel" colspan="2" style="text-align:left;">
                        List Of Diagnosis Code</td>
                </tr>
                <tr>
                    <td width="100%">
                         <asp:DataGrid ID="grdDisplayDiagonosisCode" runat="server" Width="100%" CssClass="GridTable"
                            AutoGenerateColumns="false" AllowPaging="false" PageSize="10" PagerStyle-Mode="NumericPages">
                            <ItemStyle CssClass="GridRow" />
                            <Columns>
                                <asp:BoundColumn DataField="CODE" HeaderText="DIAGNOSIS CODE"  Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Diagnosis Codes"> </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="GridHeader" />
                        </asp:DataGrid>
                    </td>
                </tr>                
            </table>
        </div>
    </form>
</body>
</html>
