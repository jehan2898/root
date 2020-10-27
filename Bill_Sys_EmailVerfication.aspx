<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_EmailVerfication.aspx.cs" Inherits="Bill_Sys_EmailVerfication" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Go Gereen Bills</title>
     <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
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
</head>
<body>
    <form id="frmContact" runat="server">
  

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
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;" >
                                            <tr>
                                                <td style="width: 100%; height: 600px; text-align: center;" class="TDPart">
                                                    <table border="0"  cellpadding="3" cellspacing="0" style="width: 100%;" >
                                                        <tr>
                                                            <td style="width: 35%">
                                                                <asp:Label runat="server" ID="lblMessage"></asp:Label>
                                                                <asp:HiddenField ID="hdUrlIntegration" runat="server" />
                                                            </td>
                                                            <td style="width: 50%; text-align: left;" colspan="2" >
                                                               
                                                                    
                                                                  
                                                               
                                                                <table width="32%" style="border-collapse: collapse; border: 2px solid white;"  >
                                                                <tr>
                                                                <td colspan="2">
                                                                 <img src="Registration/GREENLOGO2.JPG" style="height: 60px;width:100%" />
                                                                 <div style="color:White;background:Black;"> <h4>
                                                                        Email Verification</h4></div>
                                                                </td>
                                                                </tr>
                                                                <tr class="userRow">
                                                                <td style="padding:3px" >   <asp:Label  CssClass="LabelCSS" runat="server" ID="Label1">User Name</asp:Label></td>
                                                                  <td>    <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter UserName"></asp:TextBox></td>
                                                                </tr>

                                                                 <tr  class="userRow">
                                                                <td style="padding:3px" >  <asp:Label runat="server" CssClass="LabelCSS" ID="Label2">Password</asp:Label></td>
                                                                <td style="padding:3px" ><asp:TextBox  ID="txtPassword" runat="server" TextMode="Password"  placeholder="Enter Email"></asp:TextBox></td>
                                                                </tr>

                                                           
                                                               
                                                                <tr> <td  style="padding:3px"></td> 
                                                                <td colspan="1" align="left"  style="padding:3px">
                                                                
                                                                <asp:Button ID="btnActivate" runat="server" CssClass="loginButtonCSS" Text="Activate" onclick="btnActivate_Click"/>
                                                                </td>
                                                               
                                                                </tr>
                                                                </table>
                                                                 <table width="32%"  >
                                                                <tr>
                                                                <td colspan="2"  style=" text-align: center;">
                                                                  <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                </tr>
                                                                <tr>
                                                                <td colspan="2"  style=" text-align: center;">

                                                                 <asp:Label ID="lblDomainError" CssClass="blink" runat="server" Text=""></asp:Label>
                                                                 <asp:TextBox ID="txtToken" Visible="false" runat="server"></asp:TextBox>
                                                                </td>
                                                                </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 15%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 35%">
                                                            </td>
                                                            <td colspan="2" style="width: 50%; text-align:center">
                                                              
                                                                
                                                            </td>
                                                            <td style="width: 15%">
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 35%">
                                                            </td>
                                                            <td colspan="2" style="width: 50%; text-align: center">
                                                             
                                                            </td>
                                                            <td style="width: 15%">
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 30%">
                                                            </td>
                                                            <td align="left" style="width: 50%">
                                                               
                                                                
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
                                    </td>
                                    <td class="RightCenter" style="width: 10px; height: 100%;">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftBottom" style="height: 10px">
                                    </td>
                                    <td class="CenterBottom" style="height: 10px">
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
    
   
    </form>
</body>
</html>
