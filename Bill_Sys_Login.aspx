<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Login.aspx.cs" Inherits="Bill_Sys_Login"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedLoginControl" Namespace="ExtendedLoginControl" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Green Your Bills - Login</title>
    <link rel="shortcut icon" href="Registration/icon.ico" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="validation.js"></script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <style>
        .maintblCSS
        {
            width:300px;
        }
        .hdRow
        {
            background-color:#254117;
            color:#FFFFFF;
            height:28px;
            font-size: 14pt;
           font-weight: bold;
            border-left: thin solid white;
            border-right: thin solid white;
        }
        .userRow
        {
         border-left: thin solid white;
            border-right: thin solid white;
            padding-left:10px;
           padding-top:10px;
        }
        .passwordRow
        {
         border-left: thin solid white;
            border-right: thin solid white;
            padding-left:10px;
             padding-top:10px;
        }
        .rememberRow
        {
            padding-left:10px;
            padding-top:10px;
             border-left: thin solid white;
            border-right: thin solid white;
        }
        .buttonRow
        {
           text-align:right;
           padding-top:10px;
            border-left: thin solid white;
            border-right: thin solid white;
             border-left: thin solid white;
            border-bottom: thin solid white;
        }
        .errorRow
        {
            color:Red;
        }
        .textBoxPasswordCSS
        {
           
            border-top: solid green;
              border-right: solid green;
              border-bottom: solid green;
              border-left: solid green;
              width:180px;
        }
         .textBoxUserNameCSS
        {
           
            border-top: solid green;
              border-right: solid green;
              border-bottom: solid green;
              border-left: solid green;
              width:120px;
        }
        .cancelButtonCSS
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
         .loginButtonCSS
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
    <style type="text/css">
    <!--
    * {margin: 0; padding: 0;}
    BODY
    {
	    padding-right: 0px;
	    padding-left: 0px;
	    padding-bottom: 0px;
	    margin: 0px;
	    padding-top: 0px;
	    background-color: #F7F7F7;
	    width: 100%; 
	    height: 100%;
    }
     -->
    </style>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="1">
    <form id="form1" runat="server" style="text-align: center; vertical-align: middle;">
        <div style="margin-top: 1px; width: 99%; height: 96%; border: 1px solid #D9D9D7">
            <table>
                <tr>
                    <td>
                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; height: 655px;">
                <tr valign="top">
                    <td style="width: 28%;">
                        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                            height: 100%">
                            <tr>
                                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                                    padding-top: 3px; height: 100%; vertical-align: top;">
                                    <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
                                        <tr>
                                            <td class="LeftTop">
                                            </td>
                                            <td class="CenterTop">
                                            </td>
                                            <td class="RightTop" style="width: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LeftCenter" style="height: 100%">
                                            </td>
                                            <td class="Center" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td style="width: 100%; height: 600px; text-align: center;" class="TDPart">
                                                            <table border="0" cellpadding="3" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 35%">
                                                                        <asp:Label runat="server" ID="lblMessage" ></asp:Label>
                                                                        <asp:HiddenField ID="hdUrlIntegration" runat="server" />
                                                                    </td>
                                                                    <td style="width: 50%; text-align: left;" colspan="2">
                                                                        <img src="Registration/GREENLOGO2.JPG" style="width: 300px; height: 60px;" />
                                                                        <ext:ExtendedLoginControl ID="ExtendedLoginControl1" runat="server" CssClass="span"
                                                                            XMLFILE="LoginXML.xml" CONNECTIONSTRING="Connection_String" LoginButtonText="Login"
                                                                            CancelButtonText="Clear" ControlHeading="Login" UserNameLabel="User Name &nbsp;&nbsp;&nbsp;: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                                                            UserLabelCSS="LabelCSS" PasswordLabel="Password &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                                                            PasswordLabelCSS="LabelCSS" UserRowCSS="userRow" PasswordRowCSS="passwordRow"
                                                                            ReminderRowCSS="rememberRow" LoginButtonRowCSS="buttonRow" ErrorRowCSS="errorRow"
                                                                            LoginTableCSS="maintblCSS" HeaderRowCSS="hdRow" TextBoxPasswordCSS="textBoxPasswordCSS"
                                                                            TextBoxUserNameCSS="textBoxUserNameCSS" LoginButtonCSS="loginButtonCSS" CancelButtonCSS="cancelButtonCSS"
                                                                            RememberMeText=" - Remember me on this computer" meta:resourcekey="ExtendedLoginControl1Resource1"
                                                                            BackColor="Gray" />
                                                                    </td>
                                                                    <td style="width: 15%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35%">
                                                                    </td>
                                                                    <td colspan="2" style="width: 50%; text-align: left">
                                                                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="lblDomainError" runat="server" Visible="false" Text=""></asp:Label>
                                                                        </td>
                                                                    <td style="width: 15%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 30%">
                                                                    </td>
                                                                    <td align="left" style="width: 50%">
                                                                        For doctor login please click here
                                                                        <asp:LinkButton ID="linkurl" runat="server" OnClick="linkurl_Click">IM Doctor</asp:LinkButton>
                                                                    </td>
                                                                    <td style="width: 15%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35%; height: 18px;">
                                                                    </td>
                                                                    <td style="width: 50%; text-align: left; height: 18px;" colspan="2">
                                                                    </td>
                                                                    <td style="width: 15%; height: 18px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35%; height: 18px">
                                                                    </td>
                                                                    <td style="width: 30%; height: 35px; text-align: center">
                                                                        <%--<a href="Registration/Bill_Sys_Registration.aspx" style="text-decoration: none; color: Blue;
                                                                            font-weight: bold; font-size: 13px;" visible="false">Create an Account</a>--%>
                                                                    </td>
                                                                    <td style="width: 20%; height: 18px">
                                                                    </td>
                                                                    <td style="width: 15%; height: 18px">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            <td class="RightCenter" style="width: 10px; height: 100%;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LeftBottom" style="height: 10px" style="text-align:left;">
                                                  <asp:Label runat="server" ID="lblDatabaseServer"  style="font-size:8pt;" />
                                            </td>
                                            <td class="CenterBottom" style="height: 10px" align="right">
                                                 <asp:Label runat="server" ID="lblCompanyName" style="font-size:8pt;"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label runat="server" ID="lblVersion"  style="font-size:8pt;" />
                                            </td>
                                            <td class="RightBottom" style="width: 10px; height: 10px;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            &nbsp;</div>
    </form>
</body>
</html>
