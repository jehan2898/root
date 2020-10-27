<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_SelectDiagnosis.aspx.cs" Inherits="Bill_Sys_AssociateProcedureCode" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl"  %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
 <title>Billing System</title>
    <script type="text/javascript" src="validation.js" ></script>
     <link href="Css/main.css" type="text/css" rel="Stylesheet" />
 <link href="Css/UI.css" rel="stylesheet" type="text/css" />
 <script language="javascript">
    function postbackFunc()
    {
     if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
       // theForm.__EVENTTARGET.value = eventTarget;
        //theForm.__EVENTARGUMENT.value = eventArgument;
        theForm.submit();
    }
  
    }
 </script>
	 <style>
 		.lcss_maintable{
			width:100%;
			height:2%;
			cellpadding:0; 
			cellspacing:0; 
			border: 1px solid #D8D8D8;
		}
		
	 	.blocktitle_ql{
			width:98%; 
			height:auto;
			text-align:left;
			background-color:#d8dfea;
			border-color:#8ba1ca;
			border-style:solid;
			border-width:1px;
			line-height:2em;		
			margin-left:0px;
		}
	
		TABLE#content_table{
			border-color:#8ba1ca;
			text-align:left;
			font:Arial, Helvetica, sans-serif;
			font-family:Arial, Helvetica, sans-serif;
			font-size:13px;
			font-weight:normal;
			height: 100%; 
			width: 100%;
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
			font-size:12px;
			font-family:Arial, Helvetica, sans-serif;
		}
		.div_blockcontent{
			width:100%; 
			vertical-align:middle; 
			background-color:#ffffff;
		}
		
		.blocktitle_adv{
			width:100%;
			height: 20px;
			text-align:left;
			background-color:#CCCCCC;
			border-color:#8ba1ca;
			border-style:solid;
			border-width:1px;
			font-family:Arial, Helvetica, sans-serif;
			font-size:12px;
			font-weight:bold;
			padding-top:2px;
		}		
	 </style>
</head>
<body>
    <form id="frmAssociateDignosisCode" runat="server">
    <div align="center">
		<table cellpadding="0px" cellspacing="0px" class="lcss_maintable">
                <tr>
                    <td background="Images/header-bg-gray.jpg" colspan="2">
                        <div align="right">
                            <span class="top-menu" ></span></div>
                    </td>
                </tr>
                <tr>
                    <td background="Images/header-bg-gray.jpg" class="top-menu" colspan="2">&nbsp;
					</td>
                </tr>
                <tr>
                    <td background="Images/header-bg-gray.jpg" colspan="2">&nbsp;
					</td>
                </tr>
                <tr>
                    <td background="Images/header-bg-gray.jpg" colspan="2">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="100%" Xml_Transform_File="TransformXSLT.xsl"
                            LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1" DynamicMenuItemStyleCSS="sublevel1"
                            StaticMenuItemStyleCSS="parentlevel1" Height="24px"></cc2:WebCustomControl1>
                    </td>
                </tr>
                <tr>
                    <td height="35px" bgcolor="#000000">
                        <div align="left">
                        </div>
                        <div align="left">
                            <span class="pg-bl-usr">Billing company name</span></div>
                    </td>
                    <td width="12%" height="35px" bgcolor="#000000">
                        <div align="right">
                            <span class="usr">Admin</span></div>
                    </td>
                </tr>
                <tr>
                    <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center">
                        <span class="message-text">
							<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
						</span>
                    </td>
                </tr>
				<tr>
					<td height="20px" colspan="2" bgcolor="#EAEAEA" align="center">
						<div id="ErrorDiv" style="color: red" visible="true" runat="Server">
                            &nbsp;<asp:LinkButton ID="hlnkAssociate" runat="server" OnClick="hlnkAssociate_Click"
                                Visible="true">Associate Diagnosis Codes</asp:LinkButton></div>
					</td>
				</tr>
				<tr>
					<td width="100%">
						<table width="100%">
							<tr>
								<td width="100%" scope="col">
									<div class="blocktitle">
                                        Bill Procedure codes <em>(Select the Procedure codes)</em>
										<div class="div_blockcontent" >
											<table width="100%" border="0">
												<tr>
													<td class="tablecellLabel" scope="col">											&nbsp;</td>
													<td>										
                                                        <asp:DataGrid ID="grdDiagnosisCode" runat="server">
                                                            <FooterStyle />
                                                            <SelectedItemStyle />
                                                            <PagerStyle />
                                                            <AlternatingItemStyle />
                                                            <ItemStyle />
                                                            <Columns>
                                                               
                                                                <asp:BoundColumn DataField="CODE" HeaderText="Diagnosis CodeID" Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Description"></asp:BoundColumn>
                                                            </Columns>
                                                            <HeaderStyle />
                                                        </asp:DataGrid></td>
												</tr>
												<tr>
													<td class="tablecellLabel">	Search </td>
													<td><asp:TextBox Width="100%" ID="txtSearchText" runat="server"></asp:TextBox>												    </td>
												
												</tr>
												<tr>
													<td>&nbsp;
														
													</td>
													<td>
														<asp:CheckBox ID="chkDiagnosisCode" runat="server" Text="Code" />
														<asp:CheckBox ID="chkDiagnosisCodeDescription" runat="server" Text="Description" /> 
													</td>
												</tr>
												<tr>
												  <td>&nbsp;</td>
												  <td><asp:Button id="btnSearch"  runat="server" Width="80px" cssclass="btn-gray" Text="Search" OnClick="btnSearch_Click"></asp:Button></td>
											  </tr>
												<tr>
												  <td>&nbsp;</td>
												  <td>
												    <div style="overflow:scroll;height:300px; width:100%;">
                                                      <asp:DataGrid ID="grdProcedureCode" runat="server" AllowPaging="false">
                                                          <FooterStyle />
                                                          <SelectedItemStyle />
                                                          <PagerStyle />
                                                          <AlternatingItemStyle />
                                                          <ItemStyle />
                                                          <Columns>
                                                              <asp:TemplateColumn>
                                                                  <ItemTemplate>
                                                                      <asp:CheckBox ID="chkSelect" runat="server" />
                                                                  </ItemTemplate>
                                                              </asp:TemplateColumn>
                                                              <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                              </asp:BoundColumn>
                                                              <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Code"></asp:BoundColumn>
                                                              <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"
                                                                  Visible="true"></asp:BoundColumn>
                                                              <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Procedure amount" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                              <asp:BoundColumn DataField="DOCTOR_AMOUNT" HeaderText="Doctor’s amount"  ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                               <asp:BoundColumn DataField="FLT_KOEL" HeaderText="FACTOR" ItemStyle-HorizontalAlign="Right" Visible="false"></asp:BoundColumn>
                                                          </Columns>
                                                          <HeaderStyle />
                                                      </asp:DataGrid>
                                                      </div></td>
											  </tr>
											  <tr>
												  <td>&nbsp;</td>
												  <td><asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                        <asp:Button ID="btnAdd" runat="server" Text="Save" Width="80px"  CssClass="btn-gray" OnClick="btnSave_Click"  />
                        <asp:Button CssClass="btn-gray" ID="Button2" runat="server" Text="Submit" Width="80px" OnClick="Button2_Click1"  />
                        <asp:Button ID="btnCalcel" runat="server" Text="Cancel" Width="80px"  CssClass="btn-gray" OnClick="btnCancel_Click"  />
                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                        
                            <asp:TextBox ID="txtDiagnosisSetID" runat="server" Width="10px" Visible="False"></asp:TextBox>
												  </td>
											  </tr>
											  <tr>
													<td>&nbsp;
														
													</td>
													<td>
                                                           <div style="overflow:scroll;height:300px; width:100%;">
                                                      <asp:DataGrid ID="grdSelectedServices" runat="server" AllowPaging="false">
                                                          <FooterStyle />
                                                          <SelectedItemStyle />
                                                          <PagerStyle />
                                                          <AlternatingItemStyle />
                                                          <ItemStyle />
                                                          <Columns>
                                                              
                                                              <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                              </asp:BoundColumn>
                                                              <asp:BoundColumn DataField="SZ_PROCEDURAL_CODE" HeaderText="Code" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                              <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"
                                                                  Visible="true" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                              <asp:BoundColumn DataField="PROC_AMOUNT" HeaderText="Procedure amount" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                              <asp:BoundColumn DataField="DOCT_AMOUNT" HeaderText="Doctor’s amount"  ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                          </Columns>
                                                          <HeaderStyle />
                                                      </asp:DataGrid>
                                                      </div>
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