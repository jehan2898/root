<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownloadFiles.aspx.cs" Inherits="DownloadFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="CSS/rp.css" />
</head>
<body>
    <form id="form1" runat="server">
        <table id="page-title">
            <tr>
                <td>
                    Download File(s)
                </td>
            </tr>
        </table>

        <table>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>

        <div>
            <asp:Panel runat="server" ID="pnlDownloads">
            </asp:Panel>
        </div>
    </form>
</body>
</html>
