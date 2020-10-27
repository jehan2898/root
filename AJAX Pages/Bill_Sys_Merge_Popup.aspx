<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Merge_Popup.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Merge_Popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript" src="../Document Manager/Case/EasyMenu/Styles/ListBoxEvents.js"></script>

    <script type="text/javascript" src="../Document Manager/Document Manager/Case/EasyMenu/Styles/js_TreeViewFunctions.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="divListPDF">
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
                        <center>
                            <input name="btnSelectOrder" type="button" class="box" id="btnSelectOrder" onclick="setorder()"
                                value="Start Merging" runat="server" />
                            <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="box" OnClick="btnCancel_Click" />--%>
                        </center>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div2" style="visibility: hidden;">
            <asp:Button ID="btnDone" runat="server" CssClass="box" Text="Button" Visible="true" />
            <asp:Button ID="btnCheck" runat="server" Text="Check" Visible="true" />
            <input type="hidden" id="hidnFile" name="hidnFile" runat="server" class="box" value="" />
            <input type="hidden" id="hidnOrderFiles" name="hidnOrderFiles" runat="server" class="box"
                value="" />
        </div>
    </form>
</body>
</html>
