<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ViewUnBilledReason.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_ViewUnBilledReason" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReason" Text="Reason" runat="server"></asp:Label>
                </td>
                <td width="80%" height="50%">
                    <asp:TextBox ID="txtAddReason" runat="server" TextMode="MultiLine" Width="100%" ReadOnly="true"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:CheckBox ID="chkReason" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
