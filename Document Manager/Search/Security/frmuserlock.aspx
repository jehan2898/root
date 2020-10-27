<%@ Page language="c#" Inherits="Security.frmUserLock" CodeFile="frmUserLock.aspx.cs" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>frmUserLock</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body scroll="no" bgColor="#dbe2e8">
		<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" background="Image/WATERMRK.GIF"
			border="0" bgColor="#dbe2e8">
			
			<TR>
                <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
				<TD colSpan="2" height="10"></TD>
			</TR>
			<TR>
				<TD style="HEIGHT: 258px" vAlign="top" noWrap align="left" colSpan="2" height="258">
					<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR>
							<TD width="240"></TD>
							<TD align="center"></TD>
							<TD vAlign="middle" align="left" width="190"></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 187px" width="240" height="187">
								<P align="right">&nbsp;</P>
                                <p>
                                    &nbsp;</p>
							</TD>
							<TD style="HEIGHT: 187px" align="center" height="187">&nbsp;
							</TD>
							<TD style="HEIGHT: 187px" vAlign="middle" align="left" width="190" height="187"></TD>
						</TR>
						<TR>
							<TD noWrap align="center" colSpan="3">&nbsp;
								<asp:Image id="Image1" runat="server" Width="59px" Height="55px" ImageUrl="../images/upgrade_asst.jpg"></asp:Image></TD>
						</TR>
						<TR>
							<TD width="240"></TD>
							<TD align="center"></TD>
							<TD vAlign="middle" align="left" width="190"></TD>
						</TR>
						<TR>
							<TD width="240"></TD>
							<TD align="center"><asp:label id="lblMsgLater" Font-Bold="True" ForeColor="DarkBlue" Font-Names="Verdana" Font-Size="X-Small"
									runat="server">Your Account has been Activated.</asp:label>
								<!--<asp:hyperlink id="hlkReLogin" Font-Names="Verdana" Font-Size="8pt" runat="server" NavigateUrl="ICICI/db_logon.aspx">click here to login again.</asp:hyperlink>--></TD>
							<TD vAlign="middle" align="left" width="190"></TD>
						</TR>
						<TR>
							<TD width="240"></TD>
							<TD align="center"></TD>
							<TD vAlign="middle" align="left" width="190"></TD>
						</TR>
						<TR>
							<TD width="180"></TD>
							<TD id="l" align="center"></TD>
							<TD width="230"></TD>
						</TR>
						<TR>
							<TD width="180"></TD>
							<TD align=center>
								<asp:HyperLink id="HyperLink1" runat="server" onclick="history.back();" Font-Size="X-Small" Font-Names="Verdana" ForeColor="Red" Font-Bold="True" Visible="False">Close</asp:HyperLink></TD>
							<TD width="230"></TD>
						</TR>
						<TR>
							<TD width="180"></TD>
							<TD align="center">&nbsp;
							</TD>
							<TD width="230"></TD>
						</TR>
						<TR>
							<TD width="180"></TD>
							<TD align="center"></TD>
							<TD width="230"></TD>
						</TR>
					</TABLE>
					<FORM id="Form1" runat="server">
					</FORM>
				</TD>
			</TR>
			<TR>
				<TD noWrap align="center" colSpan="2">&nbsp;</TD>
			</TR>
			<TR>
				<TD noWrap align="center" colSpan="2"></TD>
			</TR>
		</TABLE>
		<P></P>
	</body>
</HTML>
