<%@ Page Language="c#" SmartNavigation ="true" Inherits="Security.frmUserRoleMapping" CodeFile="frmUserRoleMapping.aspx.cs" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title id="lnkRoleMaster">Role Master</title>
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
	<body leftMargin="0" topMargin="0" bgColor="#dbe2e8">
		<form id="Form2" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<!-- -->
				<tr>
                    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td width="64%" background="../images/title_bg_01.JPG" class="txtheader">&nbsp;Welcome 
						&gt; User Role Mapping</td>
					<td width="4%" background="../images/title_bg_01.JPG"><div align="right"><img src="../images/title_div.JPG" width="40" height="39"></div>
					</td>
					<td width="100%" background="../images/title_bg_02.JPG" class="txtheader"></td>
				</tr>
			</table>
			
					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
						<tr>
							<td vAlign="top" align="left">
								<table cellSpacing="1" cellPadding="2" width="100%" border="0">
									<tr bgColor="#e5e5e5">
										<TD class="txtheader" style="WIDTH: 214px; HEIGHT: 13px" bgColor="#dbe2e8" colSpan="7"
											rowSpan="1">&nbsp;</TD>
									</tr>
									<tr bgColor="#e5e5e5">
										<TD class="regtxt" style="WIDTH: 76px; HEIGHT: 17px" bgColor="#dbe2e8"></TD>
										<td class="regtxt" style="WIDTH: 125px; HEIGHT: 17px" bgColor="#dbe2e8">User Name</td>
										<td style="WIDTH: 280px; HEIGHT: 17px" colSpan="4" bgColor="#dbe2e8">
											<P><asp:dropdownlist id="ddlUserName" runat="server" Height="24px" AutoPostBack="True" Width="370px"
													CssClass="combox" tabIndex="2" onselectedindexchanged="ddlUserName_SelectedIndexChanged">
													<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
												</asp:dropdownlist></P>
										</td>
										<TD style="WIDTH: 320px; HEIGHT: 17px" bgColor="#dbe2e8">
											<asp:RequiredFieldValidator id="rfvUName" runat="server" CssClass="Validatecontroltxt" ErrorMessage="Please Select User Name"
												ControlToValidate="ddlUserName" InitialValue="--Select--"></asp:RequiredFieldValidator></TD>
									</tr>
									<TR>
										<TD class="regtxt" style="WIDTH: 76px; HEIGHT: 53px" bgColor="#dbe2e8"></TD>
										<TD class="regtxt" style="WIDTH: 125px; HEIGHT: 53px" bgColor="#dbe2e8" valign="top">User Roles</TD>
										<TD style="WIDTH: 280px; HEIGHT: 53px" bgColor="#dbe2e8" colSpan="4" rowSpan="1">
											<P>
												<asp:listbox id="lbxRoles" tabIndex="3" runat="server" Width="250px" Height="165px" Rows="1" SelectionMode="Multiple"></asp:listbox></P>
										</TD>
										<TD style="WIDTH: 320px; HEIGHT: 53px" bgColor="#dbe2e8"></TD>
									</TR>
                                    <tr>
                                        <td bgcolor="#dbe2e8" class="regtxt" style="width: 76px; height: 9px">
                                        </td>
                                        <td bgcolor="#dbe2e8" class="regtxt" style="width: 125px; height: 9px">
                                        </td>
                                        <td bgcolor="#dbe2e8" colspan="4" rowspan="1" style="width: 280px; height: 9px">
								<asp:button id="btnSave" accessKey="s" runat="server" CssClass="button_text" CausesValidation="False"
									Text="Save" ToolTip="Alt + s" Width="60px" tabIndex="4" onclick="btnSave_Click"></asp:button>&nbsp;
                                            <asp:button id="btnClear" accessKey="l" runat="server" CssClass="button_text" CausesValidation="False"
									Text="Clear" ToolTip="Alt + l" Width="60px" tabIndex="5" onclick="btnClear_Click"></asp:button>
								</td>
                                        <td bgcolor="#dbe2e8" style="width: 320px; height: 9px">
                                        </td>
                                    </tr>
									<tr bgColor="#e5e5e5">
										<TD class="regtxt" style="WIDTH: 214px" bgColor="#dbe2e8" colSpan="7"></TD>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
						</tr>
						<TR>
							<TD vAlign="bottom" align="center" height="35">
                                &nbsp;
									
								</TD>
						</TR>
					</table>
				
		</form>
	</body>
</HTML>
