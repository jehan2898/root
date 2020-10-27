<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Confirmation.aspx.cs" Inherits="Bill_Sys_Confirmation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Billing System Registration Confirmation</title>
</head>
<body>
    <form id="form1" runat="server" style="text-align:center;vertical-align:middle;">
    
        Thank you registering. An email has been sent to the specified address with login details to use the website.<br />
        Check your mail for further process.<br />
        
        
Click <a href="../Bill_Sys_Login.aspx"> here </a> to go back to the login page to start using the website.
        
        <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOk_Click" Width="75px" Visible="false"/>
   
    </form>
</body>
</html>
