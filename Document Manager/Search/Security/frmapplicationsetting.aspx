<%@ Page language="c#" SmartNavigation ="true" Inherits="Security.frmApplicationSetting" CodeFile="frmApplicationSetting.aspx.cs" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>frmApplicationSetting</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../CssAndJs/css.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body leftMargin="0" topMargin="0" bgColor="#dbe2e8">
		<FORM id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
                    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
					<td align="left" valign="top" bgColor="#dbe2e8"><table width="100%" border="0" cellspacing="0" cellpadding="0">
							<!--<tr>
								<td height="24" align="left" valign="middle" background="../images/links_bg.jpg" class="txtheader"><FONT face="Arial" size="2"><STRONG>
											&nbsp;Welcome &gt; Content Management &gt; Preferences</STRONG></FONT></td>
							</tr>-->
							<!-- -->
							<tr>
							</tr>
						</table>
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="64%" background="../images/title_bg_01.JPG">&nbsp;<FONT face="Arial" size="2"><STRONG class=txtheader>&nbsp;Welcome 
											&gt; Content Management &gt; Preferences</STRONG></FONT></td>
								<td width="4%" background="../images/title_bg_01.JPG"><div align="right"><img src="../images/title_div.JPG" width="40" height="39"></div>
								</td>
								<td width="32%" background="../images/title_bg_02.JPG">&nbsp;</td>
							</tr>
						</table>
					</td>
				<!-- -->
				<tr>
					<td align="center" valign="middle" bgColor="#dbe2e8"><table width="100%" border="0" cellspacing="0" cellpadding="0">
							<TR>
								<TD vAlign="top" align="center" bgColor="#dbe2e8"></TD>
							</TR>
							<TR>
								<TD vAlign="top" align="center" bgColor="#dbe2e8"></TD>
							</TR>
							<tr>
								<td align="center" valign="top" bgColor="#dbe2e8"><table border="0" cellspacing="1" cellpadding="2">
										<TR id="trGridPaging" runat = "server">
											<TD class="regtxt" bgColor="#dbe2e8" colSpan="6" height="7" style="width: 144px">
                                                No.of record per Page&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="ddlGridPaging" runat="server" AutoPostBack="True" CssClass="combox"
                                                    OnSelectedIndexChanged="ddlGridPaging_SelectedIndexChanged" Width="42px">
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                </asp:DropDownList></TD>
										</TR>
										<TR>
											<TD class="regtxt" bgColor="#dbe2e8" colSpan="6" height="7" style="width: 144px"></TD>
										</TR>
										<TR>
											<TD class="regtxt" bgColor="#dbe2e8" colSpan="6" height="7" style="width: 144px">
												
												<div id="divDataGrid" runat="server" style="vertical-align: top; height:395px; width:759px; overflow:auto;">

													<asp:datagrid id="dgApplicationSetting" runat="server" Width="95%" CellPadding="3" BackColor="White"
														BorderStyle="Solid" AutoGenerateColumns="False" BorderWidth="1px" AllowCustomPaging="True" PageSize="5" AllowSorting="True" OnSortCommand="dgApplicationSetting_SortCommand">
														<SelectedItemStyle Font-Bold="True"></SelectedItemStyle>
														<ItemStyle ForeColor="#000066"></ItemStyle>
														<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="table_header" BackColor="#006699"></HeaderStyle>
														<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
														<Columns>
															<asp:BoundColumn Visible="False" DataField="ParameterID" HeaderText="ParameterID">
																<HeaderStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="table_text" VerticalAlign="Middle"></ItemStyle>
																<FooterStyle CssClass="table_header"></FooterStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="ParameterName" HeaderText="ParameterName">
																<HeaderStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="table_text" VerticalAlign="Middle"></ItemStyle>
																<FooterStyle CssClass="table_header"></FooterStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="DisplayName" HeaderText="DisplayName" SortExpression="DisplayName">
																<HeaderStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="table_text" VerticalAlign="Middle"></ItemStyle>
																<FooterStyle CssClass="table_header"></FooterStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="ParameterValue" HeaderText="ParameterValue" SortExpression="ParameterValue">
																<HeaderStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="table_text" VerticalAlign="Middle"></ItemStyle>
																<FooterStyle CssClass="table_header"></FooterStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn HeaderText="ParameterValue" SortExpression="ParameterValue">
																<HeaderStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="table_text" VerticalAlign="Middle"></ItemStyle>
																<ItemTemplate>
																	<asp:TextBox id="txtParameterValue" runat="server" CssClass="box"></asp:TextBox>
																</ItemTemplate>
																<FooterStyle CssClass="table_header"></FooterStyle>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"
															CssClass="table_header" Mode="NumericPages"></PagerStyle>
													</asp:datagrid>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												
												</div>
												
											</TD>
										</TR>
									</table>
									<asp:Button id="btnAppSubmit" runat="server" Text="Submit" BorderStyle="None" BackColor="#336699"
										CssClass="button_text" onclick="btnAppSubmit_Click"></asp:Button>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</HTML>
