<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SetOrder.aspx.vb" Inherits="Case_SetOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PDF Merge</title>

    <script type="text/javascript" src="EasyMenu/Styles/ListBoxEvents.js"></script>

    <script type="text/javascript" src="EasyMenu/Styles/js_TreeViewFunctions.js"></script>

    <link href="../CssAndJs/css.css" type="text/css" rel="stylesheet" />
    <link href="css/forms.css" type="text/css" rel="stylesheet" />
    <link href="../CssAndJs/DocMgrCss.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="EasyMenu/Styles/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!--TO LIST SELECTED PDF FILES -->
        <div id="divListPDF" style="display: block">
            <table id="tblListPDF" border="0" style="width: 324px; height: auto" runat="server">
                <tr>
                    <td>
                        <input type="button" name="btnmoveup" id="btnmoveup" onclick="listbox_move('lstPDF', 'up');"
                            runat="server" value="Up" style="width: 50px" class="box" />
                        &nbsp;
                        <input type="button" name="btnmovedown" id="btnmovedown" onclick="listbox_move('lstPDF', 'down');"
                            runat="server" value="Down" style="width: 50px" class="box" />
                        &nbsp; &nbsp;
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="ver11blue" style="width: 395px; height: 119px;" valign="top">
                        Select the order in which you want to merge the pdf: &nbsp;
                        <asp:ListBox ID="lstPDF" runat="server" Width="500px" Height="200px"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <center><input name="btnSelectOrder" type="button" class="box" id="btnSelectOrder" onclick="setorder()"
                            value="Start Merging" runat="server" />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="box" OnClick="btnCancel_Click" />
                        </center></td>
                </tr>
            </table>
        </div>
        <div id="div2" style="visibility: hidden">
            <asp:Button ID="btnDone" runat="server" CssClass="box" Text="Button" Visible="true" />
            <asp:Button ID="btnCheck" runat="server" Text="Check" Visible="true" />
            <input type="hidden" id="hidnFile" name="hidnFile" runat="server" class="box" value="" />
            <input type="hidden" id="hidnOrderFiles" name="hidnOrderFiles" runat="server" class="box"
                value="" />
        </div>
        <!-- -------------------- -->
        <div ID="pnlProgress" class="modalPopup" style="visibility: hidden;">
        <div id="content" class="modalDialogClear" style="text-align: center; " > 
        <br />
        <br />
    Validating PDFs, please wait...<br />
    <img src="../../Images/waiting.gif" />
    </div>
            <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
	<link href="../../dist/styles/metro/notify-metro.css" rel="stylesheet" />
	<script src="../../dist/notify.js"></script>
	<script src="../../dist/styles/metro/notify-metro.js"></script>
        <script src="../../Scripts/jquery.msgBox.js" type="text/javascript"></script>
            <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

<link href="../../Styles/msgBoxLight.css" rel="stylesheet" type="text/css">
    <script>
        function ShowMessage(message,title,style) {
            $.notify({
                title: title,
                text: message,

                image: "<img src='../../images/Mail.png'/>"
            }, {
                style: 'metro',
                className: style,
                autoHide: false,
                clickToHide: true
            });
        }

            var flag;
    function ConfirmSave(sender, _title, _message, method) {
        if (flag)
            return true;
        flag = false;

        debugger;
        $.msgBox({
            title: _title,
            content: _message,
            type: "info",
            buttons: [{ value: "OK" }],
            success: function (result) {
                debugger;
                if (result == "OK") {
                    flag = true;
                    $("#btnCancel").click();
                }
                else {
                    return false;
                }
            }
        })
        return false;
    }
    </script>
            <style type="text/css">
.modalPopup
{
background-color: lightgrey;
filter: alpha(opacity=10);
opacity: 0.7;
zindex:-1;
width:100%;
}
</style>
    </form>
</body>
</html>
