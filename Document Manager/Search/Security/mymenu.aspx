<%@ Page Language="c#" SmartNavigation="true" Inherits="Security.MyMenu" CodeFile="MyMenu.aspx.cs" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>

<%@ Register TagPrefix="cc1" Namespace="SuperControls" Assembly="SuperControls.WebMenu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>Law Allies - Oneline Document Manager</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<meta http-equiv="imagetoolbar" content="no" />
		<link href="../CssAndJs/DocMgrCss.css" rel="stylesheet" type="text/css" />	
		<link href="../CssAndJs/css.css" type="text/css" rel="stylesheet" />
		<link href="../CssAndJs/menu.css" type="text/css" rel="stylesheet" />
		<script language="JavaScript" src="../CssAndJs/menu.js"></script>
		<script language="JavaScript" src="../CssAndJs/menu_tpl.js"></script>
		<script language="JavaScript" src="../CssAndJs/JavaScriptClass.js"></script>
		
		<script language="JavaScript" type="text/JavaScript">
		
		    var qs = new Querystring();
		    var QSTR = qs.get("CheckLogin");
		    if(QSTR=="False")
		    {
		        loadIframe('MyFrame', 'frmChangePassword.aspx?CheckLogin=False');
		    }
		    
			PreventF11KeyPress();	
	     	PreventRightClick();
		<!--	
		function MM_preloadImages() { //v3.0
		var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
			var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
			if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
		}

		function MM_swapImgRestore() { //v3.0
		var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
		}

		function MM_findObj(n, d) { //v4.01
		var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
			d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
		if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
		for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
		if(!x && d.getElementById) x=d.getElementById(n); return x;
		}

		function MM_swapImage() { //v3.0
		var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
		if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
		}
		//-->

			window.history.forward(1);
			
	        function MyHelp()
	        {
	            //window.open("../Help/Help.aspx");
	            window.open('../Help/Help.aspx','Help','location=no,status=yes,toolbar=no,resizable=yes,scrollbars=yes,menubar=no,width=800,height=600,left=50,top=25')
	            return false;
	        }
	       
		</script>
	</head>
	<body>
	<form id="Form1" method="post" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td  width="100%" height="27" background="../images/topstripbg.jpg"> 
    <table width="100%">
    <tr>
    <td width="50%">
    &nbsp;<asp:LinkButton class="white11arial" ID="lnkHome" runat="server"  Width="35px">Home</asp:LinkButton> | <a onclick="return loadIframe('MyFrame', this.href)" href="frmchangepassword.aspx" class="white11arial">Change Password</a> | <asp:LinkButton class="white11arial" ID="lnkHelp" runat="server"  Width="35px">Help</asp:LinkButton> | <a href="Home.aspx" class="white11arial">Logout</a></td>
    <td width="50%" align="right"> 
        <asp:Label ID="lbl_wusr" runat="server" Width="100%" class="white11arial" >Label</asp:Label></td>
    </tr>
    </table>
    </td>
  </tr>
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="264" valign="top" background="../images/leftstripbg.jpg"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td><img src="../images/logo.JPG" width="264" height="125" /></td>
          </tr>
          <tr>
            <td><table width="240" border="0" align="right" cellpadding="0" cellspacing="0">
              <tr>
                <td bgcolor="#003263"><img src="../images/my_menus.JPG" width="81" height="27" /></td>
              </tr>
              <tr>
                <td id="MenuCell" runat="server" height="27" >
                    <cc1:WebMenu ID="WebMenu1" runat="server" ImageArrow="<img src=../images/arrow.gif border=0>"
                        Width="100%" />
                    &nbsp;<busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
                    &nbsp; &nbsp;&nbsp;
                </td>
              </tr>
              
              
              <tr>
                <td><img src="../images/left_button_div.JPG" width="240" height="2" /></td>
              </tr>
            </table></td>
          </tr>
          
          
          <tr>
            <td><table width="240" border="0" align="right" cellpadding="0" cellspacing="0">
              <tr>
                <td bgcolor="#003263"><img src="../images/cus_support.JPG" width="130" height="27" /></td>
              </tr>
              <tr>
                <td background="../images/tel_bg.JPG" ><table width="100%" border="0" cellspacing="0" cellpadding="6">
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
              </tr>


              <tr>
                <td><img src="../images/left_button_div.JPG" width="240" height="2" /></td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td><table width="240" border="0" align="right" cellpadding="0" cellspacing="0">
              <tr>
                <td bgcolor="#003263"><img src="../images/contact_us.JPG" width="83" height="27" /></td>
              </tr>
          <tr>
                <td><img src="../images/left_button_div.JPG" width="240" height="2" /></td>
              </tr>
            </table></td>
          </tr>
          
          
          
          <tr>
            <td>&nbsp;</td>
          </tr>
        </table></td>
        <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
                
          <td><iframe id="MyFrame" onload="HideDisplayedBusyBox();" src="inner.html" width="100%" height="730px" scrolling="auto" frameborder="0"></iframe></td>
          </tr>
            </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="30" background="../images/footerbg.jpg"><table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="264" background="../images/leftstripbg.jpg">&nbsp;</td>
        <td bgcolor="#F0F0F0">&nbsp;<a href="#" class="red11arial">Privacy Policy</a> | <a href="#" class="red11arial">Contact Us</a></td>
        <td align="right" bgcolor="#F0F0F0" class="copyrights"><div align="right">Copyright &copy; 2007 Law Alies-Online Document Managment. All right reserved</div></td>
      </tr>
    </table></td>
  </tr>
</table>
</form>
</body>
</html>
