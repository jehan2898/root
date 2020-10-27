<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Upadte_Ins.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Upadte_Ins" %>
<%@ Register Src="~/UserControl/Bill_Sys_InsCnt.ascx" TagName="InsuranceCompany" TagPrefix="Ins" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
      <table width="100%">
      <tr>
      <td>
         <Ins:InsuranceCompany runat="server" ID="inscnt" />
      </td>
      </tr>
      </table>
    </form>
</body>
</html>
