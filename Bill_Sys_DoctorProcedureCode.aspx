<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_DoctorProcedureCode.aspx.cs" Inherits="Bill_Sys_DoctorProcedureCode" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl"  %>

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
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        
                                        <tr>
                                                <td class="ContentLabel" style="text-align:center; height:25px;" colspan="4" >
                                                <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                </td> 
                                                </tr> 
                                                
                                               
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Doctor:</td>
                                                <td style="width: 35%">
                                            <extddl:ExtendedDropDownList  AutoPost_back="true" ID="extddlDoctor" runat="server" Width="200px" Connection_Key="Connection_String" Flag_Key_Value="GETDOCTORLIST" 
														Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" OnextendDropDown_SelectedIndexChanged="extddlDoctor_extendDropDown_SelectedIndexChanged"   /></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                         
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                Search
                                                </td>
                                                <td style="width: 35%">
                                                <asp:TextBox Width="100%" ID="txtSearchText" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                   <asp:CheckBox ID="chkProcedureCode" runat="server" Text="Procedure Code" />
														<asp:CheckBox ID="chkProcedureCodeDescription" runat="server" Text="Procedure Code Description" /> 
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                <asp:Button id="btnSearch"  runat="server" Width="80px" cssclass="Buttons" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                                            </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                  <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px"  CssClass="Buttons" OnClick="btnUpdate_Click" />
														
														<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px"  CssClass="Buttons"   />
														
														<asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                   </td>
                                            </tr>
                                        </table>
                                    
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                
                                  <tr>
                                    <td style="width: 100%" class="TDPart">
                                    <asp:DataGrid ID="grdProcedureCode" runat="server" AllowPaging="false" Width="100%" CssClass="GridTable" AutoGenerateColumns="false" >
                           <ItemStyle CssClass="GridRow"/>
                             <Columns>
                                 
                                 <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="Diagnosis CodeID" Visible="False"></asp:BoundColumn>
                                 <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code" ></asp:BoundColumn>
                                 <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Procedure Code Dscription" Visible="true"></asp:BoundColumn>
                                  <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:0.00}"></asp:BoundColumn>

                                 <asp:TemplateColumn HeaderText="Doctors Amount">
                                 <ItemTemplate>
                                 <asp:TextBox ID="txtDoctorsAmount" runat="server"  Text='<%# DataBinder.Eval(Container,"DataItem.DOCTOR_AMOUNT")%>'></asp:TextBox>
                                 </ItemTemplate>
                                 </asp:TemplateColumn>

                             </Columns>
                           <HeaderStyle CssClass="GridHeader"/>
                         </asp:DataGrid>
                                    </td>
                                    </tr> 
                                
                                 <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                     <asp:DataGrid ID="grdDoctorsProcedureCode" runat="server" Width="100%" CssClass="GridTable" AutoGenerateColumns="false"  AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                      
                            <ItemStyle CssClass="GridRow"/>
                           <Columns>
							
							<asp:BoundColumn DataField="SZ_DOCT_PROC_AMOUNT_ID" HeaderText="Doctor Proc ID" Visible="False"></asp:BoundColumn>
							<asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="false" ></asp:BoundColumn>
							<asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name" Visible="false"></asp:BoundColumn>
							
							<asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="Procedure ID" Visible="false" ></asp:BoundColumn>
							<asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code" ></asp:BoundColumn>
							<asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Procedure Description" ></asp:BoundColumn>
							
							<asp:BoundColumn DataField="P_FLT_AMOUNT" HeaderText="Procedure Amount" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:0.00}"></asp:BoundColumn>
							
							<asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:0.00}"></asp:BoundColumn>																		
							<asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="false"></asp:BoundColumn>
							
						</Columns>
                            <HeaderStyle CssClass="GridHeader"/>
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


