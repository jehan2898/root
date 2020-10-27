<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhysicianSign.aspx.cs" Inherits="AJAX_Pages_PhysicianSign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Save Signature</title>
</head>
<script language="javascript" type="text/javascript">
    function ClosePopup() {
        parent.document.getElementById('divPatientSignature').style.visibility = 'hidden';
        parent.document.getElementById('divPatientSignature').style.zIndex = -1;
        parent.document.getElementById('divDocSignature').style.visibility = 'hidden';
        parent.document.getElementById('divDocSignature').style.zIndex = -1;
    }
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                &nbsp;<asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td>
                <asp:Button ID="btnBack" runat="server" Text="Back" 
                            CssClass="Buttons" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
