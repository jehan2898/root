<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="PatientHistory.aspx.cs" Inherits="PatientHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="validation.js"></script>
<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align:top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                              <tr>
                              <td style="width: 100%" class="TDPart">
                               <asp:LinkButton ID="hlnkAssociate" runat="server" OnClick="hlnkAssociate_Click">Associate Diagnosis Codes</asp:LinkButton>
                              </td>
                              </tr>
                              <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                               <tr>
                                    <td style="width: 100%" class="TDPart">
                               Case Information <a href="#anch_top">(Top)</a>
                                </td> 
                                </tr> 
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                    <asp:DataGrid ID="grdCase" runat="Server" Width="100%" CssClass="GridTable"  AutoGenerateColumns="False">
                                    <HeaderStyle CssClass="GridHeader"/>
                            <ItemStyle CssClass="GridRow"/>
                                                            <Columns>
                                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_INSURANCE" HeaderText="Insurance Carrier" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_CLAIM" HeaderText="Claim #" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_WCB_NO" HeaderText="WCB Number" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left"></asp:BoundColumn>
                                                            </Columns>
                                                        </asp:DataGrid>	                    
                                        
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                
                                  <tr>
                                    <td style="width: 100%" class="TDPart">
                                    Treatment Information <a href="#anch_top">(Top)</a>
                                    </td> 
                                    </tr> 
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                    
                                     <asp:DataGrid ID="grdTreatment" runat="Server" Width="100%" CssClass="GridTable" AutoGenerateColumns="False">
                                     
                                     <HeaderStyle CssClass="GridHeader"/>
                            <ItemStyle CssClass="GridRow"/>
                                                             <Columns>
                                                                <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="Treatment date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="center"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Treating Doctor" ItemStyle-HorizontalAlign="left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure" ItemStyle-HorizontalAlign="left" ></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Remarks" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" ></asp:BoundColumn>
                                                            </Columns>
                                                        </asp:DataGrid>		
                                    </td>
                                    </tr>
                                     <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    
                                    </td>
                                </tr>
                                
                                    <tr>
                                    <td style="width: 100%" class="TDPart">
                                   Billing (all figures in USD) <a href="#anch_top">(Top)</a>
                                    </td> 
                                    </tr> 
                                    
                                     <tr>
                                    <td style="width: 100%" class="TDPart">
                                    <asp:DataGrid ID="grdBilling" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                    <HeaderStyle CssClass="GridHeader"/>
                            <ItemStyle CssClass="GridRow"/>
                                                             <Columns>
                                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill #" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" DataFormatString="{0:0.00}"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="Paid Amount" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" DataFormatString="{0:0.00}"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write off" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" DataFormatString="{0:0.00}"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" DataFormatString="{0:0.00}"></asp:BoundColumn>
                                                            </Columns>
                                                        </asp:DataGrid>		
                                    </td> 
                                    </tr> 
                                     <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    
                                    </td>
                                </tr>
                                
                                    <tr>
                                    <td style="width: 100%" class="TDPart">
                                  Denials <a href="#anch_top">(Top)</a>
                                    </td> 
                                    </tr> 
                                    
                                     <tr>
                                    <td style="width: 100%" class="TDPart">
                                     <asp:DataGrid ID="grdDenials" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                     <HeaderStyle CssClass="GridHeader"/>
                            <ItemStyle CssClass="GridRow"/>
                                                             <Columns>
                                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill #" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" ItemStyle-HorizontalAlign="center" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" ItemStyle-HorizontalAlign="right" ></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DT_FIRST_DENIEL_DATE" HeaderText="First Denial" ItemStyle-HorizontalAlign="center" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DT_SECOND_DENIEL_DATE" HeaderText="Second Denial" ItemStyle-HorizontalAlign="center" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_REMARK" HeaderText="Remarks" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true"></asp:BoundColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                    </td> 
                                    </tr> 
                            </table>
    
</td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

<%--<html xmlns="http://www.w3.org/1999/xhtml" >


<body>
    <form id="form1" runat="server">
        <div align="center">
    		<table cellpadding="0" cellspacing="0" class="lcss_maintable">
				<tr valign="top" id="anch_top">
					
				    <td width="85%" valign="top">
						<table width="100%"  border="0" cellspacing="0" cellpadding="0" id="content_table">
						    <tr>
						        <td>&nbsp;
						             
						        </td>
						    </tr>                     		

							<tr>
								<td>
									&nbsp;
								</td>
							</tr>

                      		<tr id="anch_denials">
                        		<td scope="col" valign="top">
									<div class="blocktitle">
										
										<div class="div_blockcontent">
											<table width="100%" border="0" align="center">
											    <tr>
											        <td>
                                                        <asp:DataGrid ID="" runat="Server" AutoGenerateColumns="False">
                                                           
                                                        </asp:DataGrid>
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
</html>--%>