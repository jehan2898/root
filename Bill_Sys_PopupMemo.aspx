<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_PopupMemo.aspx.cs" Inherits="Bill_Sys_PopupMemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="Registration/validation.js"></script>
     <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table align="center" style="width:400px;height:200;">
         <tr>
             <td class="ContentLabel" colspan="3" style="height:25px;text-align:left;" >
                 <div id="ErrorDiv" style="color: red" visible="true">
                 </div>
                 <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false" style="color:Blue;"></asp:Label>
             </td>
         </tr>
            <tr>
               
                <td class="tablecellControl" colspan="3" >
                    <asp:TextBox ID="txtNoteDesc" runat="server" Height="120px" Width="387px" TextMode="MultiLine"
                        ></asp:TextBox></td>
            </tr>
        
            
            <tr>
                <td colspan="3" align="right" width="100%">
                    <asp:Button id="btnNoteSave" onclick="btnNoteSave_Click" runat="server" Width="80px" CssClass="Buttons" Text="Add">
                                       
                    </asp:Button>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
