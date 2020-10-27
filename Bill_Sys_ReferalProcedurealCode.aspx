<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_ReferalProcedurealCode.aspx.cs"
    Inherits="Bill_Sys_ReferalProcedurealCode" %>


<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

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
                                                        Referal Office
                                                   
                                                </td>
                                               <td style="width: 35%">
                                                <cc1:ExtendedDropDownList ID="extddlReferalList" Width="255px" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_REFERAL_OFFICE" Flag_Key_Value="REFERAL_OFFICE_LIST" Selected_Text="--- Select ---" />
                                                    
                                                </td>
                                                 <td class="ContentLabel" style="width: 15%">
                                                        Procedural Code
                                                  
                                                </td>
                                                <td style="width: 35%">
                                                  <cc1:ExtendedDropDownList ID="extddlProceduralCode" Width="255px" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURAL_CODE" Flag_Key_Value="PROCEDURAL_CODE_LIST" Selected_Text="--- Select ---" />  
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
                                                   <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                        Width="80px" CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                                        CssClass="Buttons" />
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
                                    
                                     <asp:DataGrid ID="grdReferalProcedural" runat="server" OnDeleteCommand="grdReferalProcedural_DeleteCommand"
                                        OnPageIndexChanged="grdReferalProcedural_PageIndexChanged" OnSelectedIndexChanged="grdReferalProcedural_SelectedIndexChanged" Width="100%" CssClass="GridTable" AutoGenerateColumns="false"  AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                                       
                                        <ItemStyle CssClass="GridRow"/>
                                        <Columns>
                                            <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="SZ_REF_CODE_ID" HeaderText="Referal Code ID" Visible="false">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_REFERAL_OFFICE_ID" HeaderText="Referal Office ID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_PROCEDURAL_CODE_ID" HeaderText="Procedural Code ID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="Referal Office" Visible="True"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_PROCEDURAL_CODE" HeaderText="Procedural Code" Visible="True"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY ID" Visible="false"></asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
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


