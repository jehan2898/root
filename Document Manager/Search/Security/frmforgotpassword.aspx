<%@ Page Language="c#" SmartNavigation ="true" Inherits="Security.frmForgotPassword" CodeFile="frmForgotPassword.aspx.cs" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>frmForgotPassword</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../CssAndJs/css.css" rel="stylesheet" type="text/css"/>
		
		<script language="JavaScript" type="text/JavaScript" >
        
		</script>
		
</head>

	<body  style="background-color:#ffffff" leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
		
			<!--<table width="100%" border="0" cellspacing="0" cellpadding="0" style="WIDTH: 100%; HEIGHT: 39px">
				<tr>
					<td  style="background-image:url(../images/title_bg_big.png)" class="txtheader">&nbsp;Welcome 
						&gt; Forgot Password</td>
					<td  style="background-image:url(../images/title_bg_big.png)">&nbsp;</td>
				</tr>
			</table>-->
			<div class="regtxt">
                <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
            </div> 
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td align="left" valign="top" style="background-color:#eee8db" colspan="1" rowspan="1" class="black11arial">
							&nbsp;Welcome - Forgot Password</td>
						</tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" style="background-color: #ffffff" valign="top">
                    </td>
                </tr>
					
						<tr>
							<td style="HEIGHT: 21px" align="center">
								<TABLE id="Table2" style="WIDTH: 350px; HEIGHT: 80px" cellspacing="1" cellpadding="1" width="100%"
									border="0">
									<tr>
										<td class="regtxt" style="WIDTH: 72px; HEIGHT: 7px" valign="middle" >EmailID:-</td>
										<td style="HEIGHT: 7px" colspan="2">
											<asp:TextBox id="txtLoginID" runat="server" Width="250px" CssClass="box" CausesValidation="True" ></asp:TextBox>&nbsp;
											<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="txtLoginID" Display="Dynamic"></asp:RequiredFieldValidator></td>
											</tr>
											<tr><td valign="middle" style="WIDTH: 72px; HEIGHT: 7px" ></td>
											<td>
											<asp:regularexpressionvalidator id="revLoginID" runat="server" CssClass="Validatecontroltxt" ErrorMessage="E-mail address is not in correct format"
												ControlToValidate="txtLoginID" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:regularexpressionvalidator></td>
											
									</tr>
											
									<tr>
										<td style="WIDTH: 72px; HEIGHT: 7px" valign="middle"></td>
										<td style="WIDTH: 72px; HEIGHT: 7px" valign="middle">
											<table width="110" border="0" cellspacing="0" cellpadding="0">
												
												<tr>
													<td width="110" align="left" style="height: 19px">
                                                        <input id="btnSubmit" runat="server" class="button_text" name="submit" onserverclick="btnSubmit_ServerClick"
                                                            style="width: 138px" tabindex="0" type="submit" value="Retrieve Password" /><a href="#"></a></td>
												</tr>
											</table>
                                            &nbsp;&nbsp;
										</td>
									</tr>
								</TABLE>
							</td>
						</tr>
						
					</table>
				
				
		</form>
	</body>
</html>
