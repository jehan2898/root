<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadJFKVisitFiles.aspx.cs" Inherits="AJAX_Pages_UploadJFKVisitFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/jscript" >
        function Validate() {
        
        var upload = document.getElementById('ReportUpload');
        if (upload.value == "") {
            alert('Please Select File to Upload');
            return false;
        } else {
            return true;
        }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table cellpadding="1" cellspacing="0" align="center" style="width:100%; vertical-align:middle; font-size:small"  border="0">
                <tr> 
                    <td colspan="2" center" style="width:70%; color:Red">
                       <asp:Label ID="lblmsg" runat="server"  Font-Size="Small" Text="" Visible="false"></asp:Label>                   
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
               <tr>
               
               <td>
               <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"> </asp:TextBox>
               <asp:TextBox ID="txtCompanyName" runat="server" Visible="false"> </asp:TextBox>
               <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
               <asp:TextBox ID="txtVisitId" runat="server" Visible="false"></asp:TextBox>
               <asp:TextBox ID="txtUserName" runat="server" Visible="false"></asp:TextBox>
               <asp:TextBox ID="txtUserID" runat="server" Visible="false"></asp:TextBox>
               </td></tr>
        </table>
    </div>
    </form>
</body>
</html>
