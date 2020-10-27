<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CasesList.aspx.vb" Inherits="Search_CasesList" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<%@ Register TagPrefix="axp" Namespace="Axezz.WebControls" Assembly="AxpDBNet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<HEAD>
		<title>Cases List</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href='<%=Page.ResolveUrl("~/CssAndJs/DemoStyles.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/Main.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/AxpStyleXPGrid3.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/LinkSelector.css")%>' type="text/css" rel="stylesheet">
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/CssAndJs/milonic_src.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/CssAndJs/mmenudom.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/CssAndJs/menu_data.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/CssAndJs/script.js")%>'></script>
	</HEAD>
<body MS_POSITIONING="GridLayout">
    <form id="form1" runat="server">
    <div>
    <table class="contentTable" id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="left"
				border="0">
				<tr vAlign="top">
                    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
                    <busyboxdotnet:BusyBox ID="BusyBox3" runat="server" ShowBusyBox="onLeavingPage" />
					<td width="10"></td>
					<td align="left" width="99%">
						<table id="Table3" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
							<tr>
								<td class="defaultText" align="left" width="100%">
									<!--HTML Data Goes Here - Chetan A. Sharma -->
									<table id="Table77" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td vAlign="top" width="120"></td>
											<td class="regText" vAlign="top" align="left" width="100%">&nbsp;
												<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<TD class="blueBackBold" noWrap align="left" width="100%"><asp:label id="Label5" runat="server">Search Criteria</asp:label></TD>
														
													</TR>
													<TR>
														<TD bgColor="#eee8db" colSpan="2">
														</TD>
													</TR>
												</TABLE>
												<TABLE id="Table12" cellSpacing="0" cellPadding="0" width="100%" bgColor="#eeeeee"
													border="0">
													<TR>
														<TD width="1" bgColor="#eee8db"></TD>
														<TD class="defaultText" vAlign="top">
															<table id="Table14" cellSpacing="0" cellPadding="0" width="100%" border="0">
																<tr>
																	<td class="defaultText" height="49">
																		<TABLE id="Table2" height="100" cellSpacing="1" cellPadding="1" width="100%" border="0"
																			class="grid">
																			<TR>
																				<TD width="10" height="13"></TD>
																				<TD height="13" width="138">
																					<asp:label id="Label100" runat="server" Width="120px" CssClass="esnav">Case ID</asp:label></TD>
																				<TD width="162" height="13" style="WIDTH: 162px">
																					<asp:textbox id="Case_Id" runat="server" CssClass="defaultText"></asp:textbox></TD>
																				<TD height="13" width="132">
																					</TD>
																				<TD height="13">
																					</TD>
																			</TR>
																			<TR>
																				<TD></TD>
																				<TD width="138">
																					<asp:label id="Label4" runat="server" Width="120px" CssClass="esnav" DESIGNTIMEDRAGDROP="1829">Injured Last Name</asp:label></TD>
																				<TD width="162" style="WIDTH: 162px">
																					<asp:textbox id="InjuredParty_LastName" runat="server" CssClass="defaultText" DESIGNTIMEDRAGDROP="569"></asp:textbox></TD>
																				<TD width="132">
																					<asp:label id="Label6" runat="server" Width="120px" CssClass="esnav" DESIGNTIMEDRAGDROP="1102">Injured First Name</asp:label></TD>
																				<TD>
																					<asp:textbox id="InjuredParty_FirstName" runat="server" CssClass="defaultText" DESIGNTIMEDRAGDROP="570"></asp:textbox></TD>
																			</TR>
																			<TR>
																				<TD></TD>
																				<TD width="138">
																					<asp:label id="Label13" runat="server" Width="128px" CssClass="esnav" DESIGNTIMEDRAGDROP="1363">Policy Number</asp:label></TD>
																				<TD width="162" style="WIDTH: 162px">
																					<asp:textbox id="Policy_Number" runat="server" CssClass="defaultText" DESIGNTIMEDRAGDROP="1771"></asp:textbox></TD>
																				<TD width="132">
																					<asp:label id="Label14" runat="server" Width="151px" CssClass="esnav" DESIGNTIMEDRAGDROP="1364">Ins. Claim Number</asp:label></TD>
																				<TD>
																					<asp:textbox id="Ins_Claim_Number" runat="server" CssClass="defaultText"></asp:textbox></TD>
																			</TR>
																			<TR>
																				<TD height="10"></TD>
																				<TD height="10" width="138">
																					<asp:label id="Label15" runat="server" Width="128px" CssClass="esnav">Index/AAA Number</asp:label></TD>
																				<TD width="162" height="10" style="WIDTH: 162px">
																					<asp:textbox id="IndexOrAAA_Number" runat="server" CssClass="defaultText"></asp:textbox></TD>
																				<TD height="10" width="132">
																					<asp:label id="Label12" runat="server" Width="80px" CssClass="esnav">Status</asp:label></TD>
																				<TD height="10">
																					<asp:dropdownlist id="ddlStatus" runat="server" CssClass="defaultText" Width="146px">
																						<asp:ListItem Value="0" Selected="True">ALL</asp:ListItem>
                                                                                        <asp:ListItem>Open</asp:ListItem>
                                                                                        <asp:ListItem Value="Close">Close</asp:ListItem>
																					</asp:dropdownlist></TD>
																			</TR>
																			</TABLE>
																	</td>
																</tr>
															</table>
															<asp:button id="btnSearchCase" runat="server" CssClass="esnav" DESIGNTIMEDRAGDROP="335" Text="Search"></asp:button>&nbsp;<asp:button id="btnReset" runat="server" CssClass="esnav" Text="Reset"></asp:button></TD>
														<TD width="1" bgColor="#eee8db"></TD>
													</TR>
												</TABLE>
												<TABLE id="Table5" cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD height="2"></TD>
													</TR>
												</TABLE>
												<asp:panel id="Panel1" runat="server" Visible="False" Width="100%">
													<TABLE id="Table9" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD  class="blueBackBold" noWrap align="left" width="100%">
																<asp:label id="Label3" runat="server" Width="100%">Search Results</asp:label></TD>
															
														</TR>
														
													</TABLE>
													<TABLE id="Table4" height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#eeeeee"
														border="0">
														<TR>
															<TD width="1" bgColor="#eee8db" height="10"></TD>
															<TD class="defaultText" vAlign="top" height="10">
																<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	
																	<TR>
																		<TD class="defaultText">
																		
																			<axp:AxpDataGrid id="AxpDataGrid1" runat="server" Width="100%" Visible="True" GridColsNoSort="1, 12"
																				UseDataBinding="False" DisableNavBarLastPageBtn="False" GridDisplayFieldNames="Edit, Delete,Details,Documents, Doctor Exam,Client Name,Provider, Insurance Company, Accident Date, D.O.S.Start, D.O.S.End, Status, Claim #, Claim Amt., ID"
																				RecordCount="-1" EditPerformUpdates="False" RowRollOverEffect="True" ImageDir="Images/" DisplayNavBar="both"
																				GridFreeFormatCellDataFieldDelimiter="#" DisplayDebug="False" DisplayType="grid" RecordsPerPage="13"></axp:AxpDataGrid>
																				
																				
																				</TD>
																	</TR>
																	<TR>
																		<TD class="defaultText"></TD>
																	</TR>
																</TABLE></TD>
															<TD width="1" bgColor="#eee8db" ></TD>
														</TR>
														<TR height="1">
															<TD width="1" bgColor="#eee8db"></TD>
															<TD bgColor="#eee8db" height="1"></TD>
															<TD width="1" bgColor="#eee8db"></TD>
														</TR>
													</TABLE>
												</asp:panel></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
					<TD width="10"></TD>
				</tr>
			</table>
    </div>
    </form>
</body>
</html>
