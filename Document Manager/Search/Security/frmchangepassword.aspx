<%@ Page Language="c#" SmartNavigation ="true" Inherits="Security.frmChangePassword" CodeFile="frmChangePassword.aspx.cs" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmChangePassword</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../CssAndJs/css.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" type="text/JavaScript">
<!--



function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}
//-->
		</script>
	</HEAD>
	<body bgColor="#dbe2e8" leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td width="64%" background="../images/title_bg_01.JPG" class="txtheader">&nbsp;Home &gt; Change 
						Password</td>
					<td width="4%" background="../images/title_bg_01.JPG"><div align="right"><img src="../images/title_div.JPG" width="40" height="39"></div>
					</td>
					<td width="32%" background="../images/title_bg_02.JPG">&nbsp;</td>
                    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
				</tr>
			</table>
			
					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR>
							<TD style="HEIGHT: 21px" align="center">&nbsp;&nbsp;</TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 21px" align="center">&nbsp;
							</TD>
						</TR>
                        <tr id="rowMessage" runat="server">
                            <td align="center" class="headertext" style="font-weight: bold; font-size: 12pt;
                                color: blue; font-family: Verdana; height: 20px; font-variant: small-caps">
                                We request you to please change your password before proceeding</td>
                        </tr>
						<TR>
							<TD style="HEIGHT: 21px" align="center">&nbsp;&nbsp;</TD>
						</TR>
						<TR>
							<TD vAlign="bottom" align="center" height="35">
								<TABLE id="Table2" style="WIDTH: 627px; HEIGHT: 80px" cellSpacing="1" cellPadding="1" width="627"
									border="0" runat="server">
									<TR>
										<TD class="regtxt" style="WIDTH: 124px; HEIGHT: 24px" vAlign="top" align="left">Old 
											Password</TD>
										<TD style="HEIGHT: 24px" colSpan="2"><asp:textbox id="txtOldPassword" tabIndex="1" runat="server" CssClass="box" Width="160px" TextMode="Password"></asp:textbox><asp:requiredfieldvalidator id="rfvOldPass" runat="server" CssClass="Validatecontroltxt" Width="200px" ErrorMessage="Please Enter Old Password"
												ControlToValidate="txtOldPassword"></asp:requiredfieldvalidator></TD>
									</TR>
									<TR>
										<TD class="regtxt" style="WIDTH: 124px; HEIGHT: 7px" vAlign="top" align="left">New 
											Password</TD>
										<TD style="HEIGHT: 7px" colSpan="2"><asp:textbox id="txtNewPassword" tabIndex="2" runat="server" CssClass="box" Width="160px" TextMode="Password"></asp:textbox><asp:requiredfieldvalidator id="rfvNewPass" runat="server" CssClass="Validatecontroltxt" Width="200px" ErrorMessage="Please Enter New Password"
												ControlToValidate="txtNewPassword"></asp:requiredfieldvalidator></TD>
									</TR>
									<TR>
										<TD class="regtxt" style="WIDTH: 124px; HEIGHT: 26px" vAlign="top" align="left">Confirm 
											Password</TD>
										<TD style="HEIGHT: 26px" colSpan="2"><asp:textbox id="txtCfmPassword" tabIndex="3" runat="server" CssClass="box" Width="160px" TextMode="Password"></asp:textbox><asp:comparevalidator id="rfvConfPass" runat="server" CssClass="Validatecontroltxt" Width="320px" ErrorMessage="New Password and Confirm Password must be same"
												ControlToValidate="txtCfmPassword" ControlToCompare="txtNewPassword"></asp:comparevalidator></TD>
									</TR>
									<TR>
										<TD class="regtxt" style="WIDTH: 124px; HEIGHT: 26px" colSpan="3">&nbsp;&nbsp;
										</TD>
									</TR>
									<TR>
										<TD class="regtxt" style="WIDTH: 124px; HEIGHT: 26px"></TD>
										<TD style="HEIGHT: 26px" colSpan="2"><asp:button id="btnSumbit" tabIndex="4" runat="server" CssClass="button_text" Text="Submit" onclick="btnSumbit_Click"></asp:button></TD>
									</TR>
								</TABLE>
								<BR>
							</TD>
						</TR>
					</table>
				
		</form>
	</body>
</HTML>
