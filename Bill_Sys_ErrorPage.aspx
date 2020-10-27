<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ErrorPage.aspx.cs" Inherits="Bill_Sys_ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Billing System Error Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table id="tblErrorTable" border="0" width="100%" cellpadding="2" cellspacing="2">
            <tr>
                <td>
                    
                </td>
            </tr>
            <tr class="Container" align="center">
                <td ><strong>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <span style="font-size: 11pt">
                    Can't Continue further process.</span></strong><span
                        style="font-size: 11pt">
                    <br />
                    <br />
                   <strong>Contact Your System Administrator</strong><br />
                    </span>
                    <br />
                    Click here to go <a href="Bill_Sys_Login.aspx" >Home</a><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr  align="center" class="Container">
                <td >
                    <br />
                    <br />
                    <table border="0" width="50%" align="center" cellpadding="2" cellspacing="2" class="TABLE_OUT">
                        <tr>
                            <td class="Container"><strong>Technical Information</strong></td>
                        </tr>
                        <tr>
                            <td class="Container"><div align="justify" style="width:100%;font-family:Verdana;color:ThreeDDarkShadow;"><%string strErrmsg = Request.QueryString["ErrMsg"].ToString(); Response.Write(strErrmsg); %></div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
