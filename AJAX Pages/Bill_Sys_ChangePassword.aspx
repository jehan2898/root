<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ChangePassword.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Green Bills - Change Password</title>
    <link rel="shortcut icon" href="../Registration/icon.ico" />
    <script type="text/javascript">
    function keyup(txtDays)
    {
    if(isNaN(txtDays.value))
    { txtDays.value="";
    }
    }
    </script>
    <style>
    .Buttons
        {
           font-size: 12px;
    color: #000099;
    font-family: arial;
    background-color: #BDB76B;
    border: 1px solid #254117;	
    padding-top:1px;
    padding-bottom:1px;
    padding-left:10px;
    padding-right:10px;
        }
    </style>
</head>

<body style="background-color:#DBE6FA ">
    <form id="form1" runat="server" style="background-color:#DBE6FA;">
    <div align="center">
    <table width="40%" border="1" frame="box" rules="none" style="position:absolute; top:0;right:0;bottom:0; left:0; width:40%; margin:auto; background-color:white" cellpadding="2">
        <%--<tr>
            <td colspan="2" align="center">
            <td>
        </tr>--%>
        <tr>
            <td colspan="2" style="font-size:medium; font-family:Verdana; font-weight:600; background-color:#003300; color:White">
                Change Password
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="New Password and Confirm Password must be same." ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword" Operator="Equal" ></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblCriteria" runat="server" ForeColor="black" Text="Password must contain minimum 8 characters including atleast one uppercase letter, one lowercase letter and one number" Font-Italic="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" style="width:30%">
                New Password:
            </td>
            <td align="left" style="width:70%">
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
       <tr>
	    <td colspan="2">
		<asp:RegularExpressionValidator id="RegularExpressionValidator1" 
		     ControlToValidate="txtNewPassword"
		     ValidationExpression="^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{8,24}$"
		     ErrorMessage="Password must match the above criteria!"
		     EnableClientScript="true" SetFocusOnError="true"
                     runat="server"/>
	     </td>
        </tr>
        <tr>
            <td align="right">
                Confirm Password:
            </td>
            <td align="left">
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <%--<tr>
            <td align="right">
                <asp:RadioButton ID="rdbPrompt" runat="server" GroupName="rdb" />
            </td>
            <td align="left">
                Prompt me to reset my password every <input type="text" onkeyup="keyup(this)" runat="server" id="txtdays" maxlength="2" style="width:5%; height:90%" /> days.
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:RadioButton ID="rdbNeverExp" runat="server" GroupName="rdb" />
            </td>
            <td align="left">
                My password never expires, I will change it when I want.
            </td>
        </tr>--%>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="Buttons" CausesValidation="true"/> &nbsp;
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="Buttons"/>
            </td>
        </tr>        
    </table>
    </div>
    </form>
</body>
</html>
