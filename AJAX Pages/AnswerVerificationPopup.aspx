<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnswerVerificationPopup.aspx.cs" Inherits="AJAX_Pages_AnswerVerificationPopup" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
    <div>
    <UserMessage:MessageControl runat="server" ID="usrMessage1"></UserMessage:MessageControl>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                    <tr>
                        <td style="width: 98%;" valign="top">
                            <table border="0" class="ContentTable" style="width: 100%">
                                <tr>
                                    <td>
                                        Upload Report :
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fuUploadReport" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" CssClass="Buttons"
                                            OnClick="btnUploadFile_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
            
    </div>
    
    </form>
</body>
</html>
