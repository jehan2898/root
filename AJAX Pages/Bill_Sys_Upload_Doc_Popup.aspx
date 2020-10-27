<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Upload_Doc_Popup.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Upload_Doc_Popup" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <script type="text/javascript">
   function Close_UploadPanel()
    {   
        document.getElementById("Uploaddiv").style.visibility="hidden";
    }
        </script>
    
</head>
<body>
 
    <form id="form1" runat="server">

    
       <table cellpadding="1" cellspacing="0" align="center" style="width:100%; vertical-align:middle; font-size:small"  border="0">
                <tr>
                    <td colspan="2">
                        &nbsp;                    
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="width:70%; color:Blue">
                        <asp:Label ID="Msglbl" runat="server" Font-Size="Small" Text="Select a File to Upload"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;                    
                    </td>
                </tr>
                <tr>
                    <td style="width:30%;" align="center">
                        Upload File:
                    </td>
                    <td style="width:70%" >
                        <asp:FileUpload  ID="ReportUpload" runat="server" />
                    </td>
                </tr>
                <tr>
                <td colspan="1" >
                &nbsp;
                </td>
                   <td align="left" >
                    <asp:Button ID="UploadButton" runat="server" Text="Upload"  OnClick="UploadButton_Click" />         
                   </td>
                   
                </tr>
               
        </table>
    
    </form>
</body>
</html>
