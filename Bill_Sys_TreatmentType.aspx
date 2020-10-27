<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_TreatmentType.aspx.cs" Inherits="Bill_Sys_TreatmentType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Treatment types</title>
	<style>
		.lcss_maintable{
			width:100%;
			height:2%;
			cellpadding:0; 
			cellspacing:0;
		}

		.blocktitle{
			width:98%; 
			height:auto;
			text-align:left;
			background-color:#d8dfea;
			border-color:#8ba1ca;
			border-style:solid;
			border-width:1px;
			line-height:2em;		
			margin-left:10px;
		}
		
		.div_blockcontent{
			width:100%; 
			vertical-align:middle; 
			background-color:#ffffff;

			/*padding-right:10%;*/
		}
	</style>
	
	<link href="Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
		<div align="center" style="border:none; ">
			<table class="lcss_maintable" cellpadding="0px" cellspacing="0px" border="0">
			<tr>
				<td>&nbsp;
					
				</td>
			</tr>
			<tr>
			  <td>
					<table width="100%" id="content_table"  border="0" cellspacing="0" cellpadding="0">
						<tr>
						  <td scope="col" width="98%">
							<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
						  		<div class="blocktitle">Treatment types
                            		<div class="div_blockcontent">
										<table width="100%">
											<tr>
												<td>
												<ajaxToolkit:TabContainer ID="tabcontainerPatientTreatment" runat="Server" ActiveTabIndex="0">
                                            		<ajaxToolkit:TabPanel runat="server" ID="tabpnlViewTreatment" TabIndex="0">
													<HeaderTemplate>
														<div style="width: 80px;height:200px;" class="lbl">Treatments</div>
													</HeaderTemplate>
													<ContentTemplate>
														<div>
														<table width="100%" border="0" cellspacing="0" cellpadding="0">
															<tr>
																<td width="5%" class="tablecellLabel">
																	<div class="lbl">Doctor</div>
																</td>
																<td width="95%">
																	<span class="tablecellControl" style="padding:1px;">
																	<extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="98%" Connection_Key="Connection_String"
																			Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"
																			AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlDoctor_extendDropDown_SelectedIndexChanged" />
																	</span>
																</td>
															</tr>
	
															<tr>
																<td width="5%" class="tablecellLabel">
																	<div class="lbl">Treatment</div>
																</td>
																<td width="95%">
																	<span class="tablecellControl" style="padding:1px;">
																		<asp:TextBox ID="txtTreatmentName" runat="server" CssClass="text-box" Width="98%"></asp:TextBox>
																	</span>
																</td>
															</tr>
	
															<tr>
																<td width="5%" class="tablecellLabel">
																	<div class="lbl">Description</div>
																</td>
																<td width="95%">
																	<span class="tablecellControl" style="padding:1px;">
																		<asp:TextBox TextMode="MultiLine" ID="txtTreatmentDescription" runat="server" CssClass="text-box" Width="98%" Height="80px"></asp:TextBox>
																	</span>
																</td>
															</tr>
															<tr>
															  <td colspan="2" align="center">
																<span class="tablecellControl" style="width:100%">
																	<asp:Button ID="btnSaveTreatment" runat="server" Text="Add" Width="80px" cssclass="btn-gray"/>
																	<asp:Button ID="btnClose" runat="server" Text="Close" Width="80px" cssclass="btn-gray"/>
																</span>											  
															  </td>
														  </tr>
															<tr>
															  <td colspan="2" align="left">
																<asp:DataGrid ID="grdTreatments" runat="Server" AutoGenerateColumns="False">
																	<Columns>
																		<asp:BoundColumn DataField="SZ_TREATMENT_NAME" HeaderText="Treatment" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
																		<asp:BoundColumn DataField="SZ_TREATMENT_DESCRIPTION" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
																		<asp:BoundColumn DataField="SZ_PROCEDURE" HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
																		<asp:BoundColumn DataField="SZ_PROCEDURE_TEXT" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
																		<asp:BoundColumn DataField="SZ_PROCEDURE_AMOUNT" HeaderText="Procedure charge" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
																		<asp:BoundColumn DataField="SZ_DOCTOR_AMOUNT" HeaderText="Doctor's charge" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
																	</Columns>
																</asp:DataGrid>														  
															  </td>
														  </tr>
														</table>
														</div>
													</ContentTemplate>
												</ajaxToolkit:TabPanel>
												
												<ajaxToolkit:TabPanel runat="server" ID="tabpnlAddTreatmentPrice" TabIndex="1" HeaderText="Anywya">
													<HeaderTemplate>
														<div style="width: 80px;" class="lbl">Treatment prices</div>
													</HeaderTemplate>
													<ContentTemplate>
														<div>
														<table width="100%" border="0" cellspacing="0" cellpadding="0">
															<tr>
																<td width="5%" class="tablecellLabel">
																	<div class="lbl">Treatment</div>
																</td>
																<td width="95%">
																	<span class="tablecellControl" style="padding:1px;">
																	<extddl:ExtendedDropDownList ID="extddlDoctorTreatments" runat="server" Width="98%" Connection_Key="Connection_String"
																			Flag_Key_Value="GETTDOCTORTREATMENT" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"
																			AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlDoctor_Treatment_extendDropDown_SelectedIndexChanged" />
																	</span>
																</td>
															</tr>
	
															<tr>
																<td width="5%" class="tablecellLabel">
																	<div class="lbl">Procedures</div>
																</td>
																<td width="95%">
																	<span class="tablecellControl" style="padding:1px;">
																		<asp:TextBox TextMode="MultiLine" ID="txtProcedures" runat="server" CssClass="text-box" Width="98%" Height="120px"></asp:TextBox>
																	</span>
																</td>
															</tr>
	
															<tr>
																<td width="5%" class="tablecellLabel">
																	<div class="lbl">KOEL</div>
																</td>
																<td width="95%">
																	<span class="tablecellControl" style="padding:1px;">
																		<asp:TextBox ReadOnly="true" ID="txtProviderKOEL" runat="server" CssClass="text-box" Width="10%"></asp:TextBox>
																	</span>
																	<span class="lbl" style="width:30%;text-align:right;">
																		Doctor's price
																	</span>
																	<span class="tablecellControl" style="padding:1px;">
																		<asp:TextBox ID="txtDoctorPrice" runat="server" CssClass="text-box" Width="10%"></asp:TextBox>
																	</span>																	
																</td>
															</tr>
															<tr>
															  <td colspan="2" align="center">
																<span class="tablecellControl" style="width:100%">
																	<asp:Button ID="btnSaveTreatmentPrices" runat="server" Text="Add" Width="80px" cssclass="btn-gray"/>
																	<asp:Button ID="btnCloseFromTPrices" runat="server" Text="Close" Width="80px" cssclass="btn-gray"/>
																</span>											  
															  </td>
														  </tr>
															<tr>
															  <td colspan="2" align="left">
																<asp:DataGrid ID="grdTreatmentPrices" runat="Server" AutoGenerateColumns="False" visible="false">
																	<Columns>
																		<asp:BoundColumn DataField="SZ_TREATMENT_NAME" HeaderText="Treatment" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
																		<asp:BoundColumn DataField="SZ_TREATMENT_DESCRIPTION" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
																		<asp:BoundColumn DataField="SZ_PROCEDURE" HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
																		<asp:BoundColumn DataField="SZ_PROCEDURE_TEXT" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
																		<asp:BoundColumn DataField="SZ_PROCEDURE_AMOUNT" HeaderText="Procedure charge" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
																		<asp:BoundColumn DataField="SZ_DOCTOR_AMOUNT" HeaderText="Doctor's charge" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
																	</Columns>
																</asp:DataGrid>
															  </td>
														  </tr>
														</table>
														</div>
													</ContentTemplate>
												</ajaxToolkit:TabPanel>
												
												</ajaxToolkit:TabContainer>
												</td>
											</tr>
										</table>
									</div>
								</div>
						  </td>
						</tr>
					</table>
			  </td>
			  </tr>
			</table>
		</div>
    </form>
</body>
</html>
