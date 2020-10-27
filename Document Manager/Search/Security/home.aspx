<%@ Page Language="c#" SmartNavigation="true" Inherits="Security.Home" CodeFile="Home.aspx.cs" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>LAW ALLIES</title>
	<link href="../CssAndJs/DocMgrCss.css" rel="stylesheet" type="text/css" />
	<link href="../CssAndJs/css.css" rel="stylesheet" type="text/css" />
	<script language="JavaScript" src="../CssAndJs/menu.js"></script>
	<script language="JavaScript" src="../CssAndJs/JavaScriptClass.js"></script>
	<script >
	   
		function readCook(name) 
        {
            var nameEQ = name + '=';
            //alert('Name : ' + nameEQ);
            //alert(document.cookie);
            
            var ca = new Array(); 
            ca = document.cookie.split("&");
            //alert(ca.length);
            for(var i=0;i<ca.length;i++) 
            {
	            var c = ca[i];
	            while (c.charAt(0)==' ') 
	            c = c.substring(1,c.length);
	            if (c.indexOf(nameEQ) == 0) 
	                return c.substring(nameEQ.length,c.length);
            }
            
            return null;
        }
        
		function getCookie() 
		{
		
	        var EmailID=document.getElementById('txtUserID').value;
	        var x=readCook(EmailID+"IP");
	        if (x==null || x=='')
	        {
	            document.getElementById('hflIsCookie').value='N';
	            document.getElementById('txtPassword').value="";
	            document.getElementById('chkRememberUser').checked=false;
	        }
	        else
	        {
	            document.getElementById('txtPassword').value=x;
	            document.getElementById('hflIsCookie').style.visibility='visible';
	            document.getElementById('hflIsCookie').value='Y';
	            document.getElementById('chkRememberUser').checked=true;
	            document.getElementById('hflIsCookie').style.visibility='Hidden';
	        }
            return false;
        }
        
        function changeStatus()
        {
            document.getElementById('hflIsCookie').style.visibility='visible';
            document.getElementById('hflIsCookie').value='N';
            document.getElementById('hflIsCookie').style.visibility='Hidden';
            return false;
        }
        
        function MyHelp()
        {
            //window.open("../Help/Help.aspx");
            window.open('../Help/Help.aspx','Help','location=no,status=yes,toolbar=no,resizable=yes,scrollbars=yes,menubar=no,width=800,height=600,left=50,top=25')
            return false;
        }

		//-->
    </script>
	</head>
<body>
<form id="Form2" method="post" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr >
    <td  height="27" background="../images/topstripbg.jpg"> &nbsp;
    <a href="inner.html" class="red11arial" onclick="return loadIframe('MyFrame', this.href)" >Home</a> | <asp:LinkButton CssClass="red11arial" ID="lnkHelp" runat="server"  Width="35px">Help</asp:LinkButton>
    </td>
  </tr>
  <tr>
    <td>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="264" valign="top" background="../images/leftstripbg.jpg">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr >
                            <td><img src="../images/logo.JPG" width="264" height="125" /></td>
                        </tr>
                        <tr>
                            <td>
                                <table  width="240" border="0" align="right" cellpadding="0" cellspacing="0">
                                    <tr >
                                        <td bgcolor="#003263"><img src="../images/login.JPG" height="27" /></td>
                                    </tr>
                                    <tr >
                                        <td height="27" >
                                            <table  width="100%" border="0" cellspacing="0" cellpadding="2">
                                                <tr>
                                                  <td  class="white11arial">Username<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserId"
                                                          CssClass="error" Display="Dynamic" ErrorMessage="Please enter ID." ForeColor=" "
                                                          Width="10px">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                  <td><label>
                                                    &nbsp;
                                                      <asp:TextBox ID="txtUserID" runat="server" AutoCompleteType="Email" CssClass="textfield"
                                                          onblur="getCookie()" Width="203px"></asp:TextBox>
                                                    
                                                  </label></td>
                                                </tr>
                                                <tr>
                                                  <td class="white11arial">Password<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                                          CssClass="error" Display="Dynamic" ErrorMessage="Please enter password." ForeColor=" ">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                  <td>&nbsp;
                                                      <asp:TextBox ID="txtPassword" runat="server" CssClass="textfield" onchange="changeStatus()"
                                                          OnPreRender="txtPassword_PreRender" TextMode="Password" Width="203px"></asp:TextBox>
                                                      <asp:HiddenField ID="hflIsCookie" runat="server" Value="N" />
                                                      <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
                                                      
                                                      &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                  <td><img src="../images/spacer.gif" width="10" height="10" /></td>
                                                </tr>
                                                <tr >
                                                  <td>
                                                    <table   width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr >
                                                          <td width="63%">&nbsp;
                                                              <asp:CheckBox ID="chkRememberUser" CssClass="white12arial" runat="server" OnCheckedChanged="chkRememberUser_CheckedChanged"
                                                                  Text="Remember me" /></td>
                                                          <td width="37%"><div align="center">
                                                              <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="../images/enter.JPG" OnClick="btnLogin_Click" /></div ></td>
                                                        </tr>
                                                    </table>
                                                  </td >
                                                </tr>
                                            </table >
                                        </td>
                                    </tr>
                                  <tr >
                                    <td><img src="../images/left_button_div.JPG" width="240" height="2" /></td>
                                  </tr>
                                  <tr>
                                    <td height="27"><a href="frmForgotPassword.aspx" class="white11arial" onclick="return loadIframe('MyFrame', this.href)" >Forgot Password?</a></td>
                                  </tr>
                                  <tr>
                                    <td><img src="../images/left_button_div.JPG" width="240" height="2" /></td>
                                  </tr>
                                  <tr>
                                    <td height="27"><a href="../UserRegistration/frmCreateUsers.aspx?OPR=I" class="white11arial" onclick="return loadIframe('MyFrame', this.href)">Register</a></td>
                                  </tr>
                                  <tr>
                                    <td><img src="../images/left_button_div.JPG" width="240" height="2" /></td>
                                  </tr>
                                  
                                 <tr>
                                    <td  >
                                        <table  width="240" border="0" align="right" cellpadding="0" cellspacing="0">
                                          <tr>
                                            <td bgcolor="#003263"><img src="../images/cus_support.JPG" width="130" height="27" /></td>
                                          </tr>
                                          <tr>
                                            <td background="images/tel_bg.JPG" ><table width="100%" border="0" cellspacing="0" cellpadding="6">
                                              <tr>
                                                <td class="white11arial">If you have any problem or questions to be answered to your satisfaction, you can write to us or </td>
                                              </tr>
                                              <tr>
                                                <td class="white11arial">CALL US AT </td>
                                              </tr>
                                              <tr>
                                                <td class="telnumber">
                                                    718-301-9109</td>
                                              </tr>
                                            </table></td>
                                          </tr >
                                          <tr >
                                            <td><img src="images/left_button_div.JPG" width="240" height="2" /></td>
                                          </tr>
                                        </table>
                                   </td>
                              </tr>
          
                              <tr >
                                <td >
                                    <table  width="240" border="0" align="right" cellpadding="0" cellspacing="0">
                                        <tr >
                                            <td bgcolor="#003263"><img src="../images/contact_us.JPG" width="83" height="27" /></td>
                                        </tr>
                                          <!--
                                          <tr >
                                            <td class="addbg"><table width="100%" border="0" cellspacing="0" cellpadding="6">
                                              <tr>
                                                <td class="white11arial">Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam tristique   metus ultrices nisi. In hac habitasse platea dictumst. </td>
                                              </tr>
                                              <tr>
                                                <td class="white11arial">Mailing address...Mailing address...Mailing address...Mailing address...</td>
                                              </tr>

                                            </table></td>
                                          </tr>-->
                                        <tr>
                                            <td><img src="images/left_button_div.JPG" width="240" height="2" /></td>
                                        </tr>
                                    </table>
                               </td>
                             </tr>
                        </table>
                    </td >
                </tr>
            </table >
         </td >
         <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <!--
          <tr>
            <td background="../images/bannerbg.jpg"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td><div style="float:left"><img src="../images/bannerleft.jpg" width="392" height="125" /></div>
				<div style="float:right"><img src="../images/bannerright.jpg" /></div></td>
                </tr>
            </table></td>
          </tr>-->
          <tr>
            <td><iframe id="MyFrame" runat="server" src="inner.html" width="100%" height="730px" scrolling="auto" frameborder="0"></iframe></td>
          </tr>
            </table></td>
      </tr >
    </table>
   </td>
  </tr>
  <tr>
    <td height="30" background="../images/footerbg.jpg">
        <table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="264" background="../images/leftstripbg.jpg">&nbsp;</td>
            <td bgcolor="#F0F0F0">&nbsp;<a href="inner.html" class="red11arial" onclick="return loadIframe('MyFrame', this.href)">Privacy Policy</a> | <a href="inner.html" class="red11arial" onclick="return loadIframe('MyFrame', this.href)"  >Contact Us</a></td>
            <td align="right" bgcolor="#F0F0F0" class="copyrights"><div align="right">Copyright &copy; 2007 Law Allies-Online Document Managment. All right reserved</div></td>
        </tr>
    </table>
    </td>
  </tr>
</table>
</form>
</body>
</html>
