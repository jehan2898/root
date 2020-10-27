<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ConnectionProcedures.aspx.cs" 
    Inherits="AJAX_Pages_Bill_Sys_ConnectionProcedures" %>
    
<%@ Register 
        Src="~/UserControl/ErrorMessageControl.ascx" 
        TagName="MessageControl"
        TagPrefix="UserMessage" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table style="border:1px solid;width:100%;vertical-align:top;">
        <tr>
                            <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                           
                            <asp:TextBox runat="server" ID="txtcaseId" Visible="false"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtLoginCompanyId" Visible="false"></asp:TextBox>
                           
                            
            <td style="border:1px solid;vertical-align:top;">
               <table style="width:450px;vertical-align:top;">
                    <tr>
                         <asp:TextBox runat="server" ID="txtCompanyId" Visible="false"></asp:TextBox>
                         <asp:TextBox runat="server" ID="txtProcCodeId" Visible="false"></asp:TextBox>
                         <asp:TextBox runat="server" ID="txtProcGroupId" Visible="false"></asp:TextBox>
                         <asp:TextBox runat="server" ID="txtLocationId" Visible="false"></asp:TextBox>
                        <td style="width:100px">
                          <asp:UpdatePanel ID="UpdatePanelFacility" runat="server">
                            <ContentTemplate>
                             <asp:Panel runat="server" ID="pnlFacility">
                              Facility
                              <extddl:ExtendedDropDownList 
                                ID="extddlBillCompany" 
                                runat="server" 
                                Width="125px"
                                Connection_Key="Connection_String" Flag_Key_Value="flag" AutoPost_back="true"
                                Procedure_Name="SP_GET_COMPANY_CONNECTIONS" Selected_Text="---Select---" 
                                OnextendDropDown_SelectedIndexChanged="extddlBillCompany_extendDropDown_SelectedIndexChanged" />
                             </asp:Panel>
                            </ContentTemplate>
                          </asp:UpdatePanel>
                        </td>
                        
                        <td style="width:100px">
                            <asp:UpdatePanel ID="UpdatePanelSpecialty" runat="server">
                                  <ContentTemplate>
                                     <asp:Panel runat="server" ID="pnlSpecialty" Visible="true">
                                        Specialty
                                      <extddl:ExtendedDropDownList 
                                        ID="extddlSpeciality" 
                                        runat="server" 
                                        Width="125px" 
                                        Connection_Key="Connection_String"
                                        Flag_Key_Value="GET_REFERRAL_PROC_GROUP" 
                                        Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                        Selected_Text="---Select---" 
                                        AutoPost_back="true"
                                        OnextendDropDown_SelectedIndexChanged="extddlSpeciality_extendDropDown_SelectedIndexChanged"
                                         />
                                     </asp:Panel>
                                  </ContentTemplate>
                             </asp:UpdatePanel> 
                        </td>
                        <td style="width:100px">
                            <asp:UpdatePanel ID="UpdatePanelProcode" runat="server">
                                  <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnlProcedure" Visible="true">
                                        Procedure Code
                                        <asp:DropDownList ID="ddlProcedureCode" runat="server" Width="125px">
                                        </asp:DropDownList>
                                     </asp:Panel>
                                  </ContentTemplate>
                            </asp:UpdatePanel>
                       </td>
                       <td style="width:100px" valign="bottom">
                             <asp:UpdatePanel ID="UpdatePanelShow" runat="server">
                                  <ContentTemplate>
                                     <asp:Panel runat="server" ID="PanelShow" Visible="true">
                                       <asp:Button ID="btnShow" runat="server" Text="SHOW" OnClick="btnShow_Click" />
                                     </asp:Panel>
                                 </ContentTemplate>
                            </asp:UpdatePanel>
                       </td>
           
                    </tr>
     
                </table>
            </td>
          </tr>    
        <tr>
            <td>
                <table style="vertical-align:top;">
                    <tr>
                         <td runat="server" id="tdsrch">
                        
                             <asp:UpdatePanel ID="UpdatePanel33" runat="server" >
                                  <ContentTemplate>
                                     <asp:Panel ID="pnlGrdSrch" runat="server" >
                                      Search:
                                     <gridsearch:XGridSearchTextBox 
                                        ID="XGridSearchTextBox1" 
                                        runat="server" CssClass="search-input"
                                        AutoPostBack="true">
                                     </gridsearch:XGridSearchTextBox>
                                     </asp:Panel>
                                  </ContentTemplate>
                             </asp:UpdatePanel>
                            
                          </td>
                                            
                          <td align="right" runat="server" id="tdrecnt">
                            
                             <asp:UpdatePanel ID="UpdatePanel222" runat="server" >
                                  <ContentTemplate>
                                  <asp:Panel ID="PnlRecnt" runat="server" >
                                      Record Count:
                                      <%= this.MissingProcGrid.RecordCount%>
                                      |
                                   </asp:Panel>     
                                  </ContentTemplate>
                             </asp:UpdatePanel>
                           
                          </td>
                                            
                          <td runat="server" id="tdpgcnt">
                         
                             <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                 <ContentTemplate>
                                  <asp:Panel ID="PnlPgCnt" runat="server">
                                      Page Count:
                                       <gridpagination:XGridPaginationDropDown ID="XGridPaginationDropDown1" runat="server">
                                       </gridpagination:XGridPaginationDropDown>
                                       <asp:LinkButton ID="LinkButton1" runat="server" Text="Export TO Excel" OnClick="lnkExportMissingGrd_onclick"> <%--OnClick="LinkButton1_onclick">--%>
                                       <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                      </asp:Panel>  
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                         
                           </td>
                    </tr>
               </table>        
             </td>                                      
        </tr>
        
        <tr>
              <td>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>
                 <xgrid:XGridViewControl 
                    ID="MissingProcGrid" 
                    runat="server" 
                    CssClass="mGrid" 
                    AutoGenerateColumns="false"
                    AllowSorting="true" 
                    PagerStyle-CssClass="pgr" 
                    PageRowCount="10" 
                    XGridKey="MissingProc"
                    AllowPaging="true" 
                    ExportToExcelColumnNames=
                    "Case No.,
                    Patient Name,
                    Insurance Name,
                    Patient Phone,
                    Patient Address,
                    First Treatment Date,
                    Accident Date,
                    Specialty,
                    Procedure Code,
                    Office Name,
                    Visit Date"
                                                    
                    ShowExcelTableBorder="true" 
                    ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_INSURANCE_NAME,SZ_PATIENT_PHONE,SZ_PATIENT_ADDRESS,DT_FIRST_TREATMENT,DT_ACCIDENT_DATE,SZ_SPECIALITY,SZ_PROCEDURE_CODE,SZ_OFFICE_NAME,DT_VISIT_DATE"
                    AlternatingRowStyle-BackColor="#EEEEEE" 
                    HeaderStyle-CssClass="GridViewHeader"
                    ContextMenuID="ContextMenu1" 
                    EnableRowClick="false" 
                    MouseOverColor="0, 153, 153">
                    <Columns>
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="false" 
                            ItemStyle-Width="100%" 
                            SortExpression="I_ASSOCIATE_ID" 
                            headertext="Associate ID"
                            DataField="I_ASSOCIATE_ID" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="true" 
                            ItemStyle-Width="40px" 
                            SortExpression="SZ_CASE_NO" 
                            headertext="Case #"
                            DataField="SZ_CASE_NO" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="true" 
                            ItemStyle-Width="80px" 
                            headertext="Patient Name" 
                            SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME  + ' '  + MST_PATIENT.SZ_PATIENT_LAST_NAME"
                            DataField="SZ_PATIENT_NAME" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="true" 
                            ItemStyle-Width="160px" 
                            SortExpression="SZ_INSURANCE_NAME" 
                            headertext="Insurance Name"
                            DataField="SZ_INSURANCE_NAME" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="true" 
                            ItemStyle-Width="40px" 
                            headertext=" Patient Phone" 
                            DataField="SZ_PATIENT_PHONE" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="true" 
                            ItemStyle-Width="140px" 
                            headertext="Patient Address" 
                            DataField="SZ_PATIENT_ADDRESS" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            ItemStyle-Width="90px" 
                            SortExpression="DT_FIRST_TREATMENT" 
                            headertext="First Treatment Date"
                            DataField="DT_FIRST_TREATMENT" 
                            DataFormatString="{0:MM/dd/yyyy}" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="true" 
                            ItemStyle-Width="80px" 
                            SortExpression="CONVERT(NVARCHAR(20),mst_case_master.DT_DATE_OF_ACCIDENT,106)"
                            headertext="Accident Date" 
                            DataField="DT_ACCIDENT_DATE" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="true" 
                            ItemStyle-Width="40px" 
                            SortExpression="(SELECT SZ_PROCEDURE_GROUP FROM MST_PROCEDURE_GROUP WHERE SZ_PROCEDURE_GROUP_ID = (SELECT SZ_PROCEDURE_GROUP_ID FROM MST_PROCEDURE_CODES WHERE SZ_PROCEDURE_ID = TXN_ASSOCIATE_PROCEDURE_CODE.SZ_PROCEDURE_CODE_ID ))"
                            headertext="Specialty" 
                            DataField="SZ_SPECIALITY" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="true" 
                            ItemStyle-Width="140px" 
                            SortExpression="(SELECT SZ_PROCEDURE_CODE + '-' + SZ_CODE_DESCRIPTION FROM MST_PROCEDURE_CODES WHERE SZ_PROCEDURE_ID=TXN_ASSOCIATE_PROCEDURE_CODE.SZ_PROCEDURE_CODE_ID)"
                            headertext="Procedure Code" 
                            DataField="SZ_PROCEDURE_CODE" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="false" 
                            headertext="Company ID" 
                            DataField="SZ_COMPANY_ID" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="true" 
                            ItemStyle-Width="80px" 
                            headertext="Office Name" 
                            DataField="SZ_OFFICE_NAME" />
                         
                         <asp:BoundField 
                            HeaderStyle-HorizontalAlign="left" 
                            ItemStyle-HorizontalAlign="left"
                            visible="true" 
                            ItemStyle-Width="40px" 
                            headertext="Visit Date" 
                            DataField="DT_VISIT_DATE" />
                     
                     </Columns>
                 </xgrid:XGridViewControl>
                 </ContentTemplate>
               </asp:UpdatePanel>
            </td>
        </tr>
        
    </table>   
</asp:Content>