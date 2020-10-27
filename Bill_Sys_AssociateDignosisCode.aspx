<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AssociateDignosisCode.aspx.cs" Inherits="Bill_Sys_AssociateDignosisCode" MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="validation.js"></script>
   <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 80%;
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
                                         <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                    <HeaderStyle CssClass="GridHeader"/>
                            <ItemStyle CssClass="GridRow"/>
                                                            <Columns>
                                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                
                                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                                <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                <a href="#" onclick="return openTypePage('a')">
                                                                <img src="Images/actionEdit.gif" style="border:none;"/>
                                                                </a> 
                                                                </ItemTemplate>
                                                                </asp:TemplateColumn>
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
                                    <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                    </td>
                                </tr>  
                                 <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>                             
                                <tr>
                                    <td style="width: 100%">
                                    <ajaxToolkit:TabContainer ID="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="0" CssClass="ajax__tab_theme">
                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlAssociate" TabIndex="0">
                                <HeaderTemplate>
                                    <div style="width: 200px; height: 200px;" class="lbl">
                                        Associate Diagnosis Code</div>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="100%" scope="col">
                                                <div class="blocktitle">
                                                    Patient diagnostic codes <em>(Select the doctor)</em>
                                                    <div class="div_blockcontent">
                                                        <table width="100%" border="0">
                                                            <tr>
                                                                <td class="tablecellLabel" scope="col" style="height: 18px">
                                                                    <asp:Label ID="lblDoctor" runat="server" Text="Doctor"></asp:Label> 
                                                                </td>
                                                                <td style="height: 18px">
                                                                    <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"
                                                                        AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlDoctor_extendDropDown_SelectedIndexChanged" OldText="" StausText="False" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tablecellLabel">
                                                                    Search
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox Width="100%" ID="txtSearchText" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkDiagnosisCode" runat="server" Text="Diagnosis Code" />
                                                                    <asp:CheckBox ID="chkDiagnosisCodeDescription" runat="server" Text="Diagnosis Code Description" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btnSearch" runat="server" Width="80px" CssClass="Buttons" Text="Search"
                                                                        OnClick="btnSearch_Click"></asp:Button></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:DataGrid Width="100%" ID="grdSelectedDiagnosisCode" CssClass="GridTable" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="grdNormalDgCode_PageIndexChanged">
                                                                      
                                                                         <ItemStyle CssClass="GridRow"/>
                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="GridHeader"/>
                                                                    </asp:DataGrid></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                    <asp:Button ID="btnAssign" runat="server" Text="Assign" Width="80px" CssClass="Buttons"
                                                                        OnClick="btnAssign_Click" />
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="Buttons"
                                                                        OnClick="btnCancel_Click" />
                                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                    <asp:TextBox ID="txtDiagnosisSetID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Search result</td>
                                                                <td>
                                                                    <table width="100%" id="tblDiagnosisCodeFirst" runat="server">
                                                                        <tr runat="server">
                                                                            <td width="100%" runat="server">
                                                                                <div style="overflow: scroll; height: 300px; width: 95%;">
                                                                                    <asp:DataGrid Width="100%" ID="grdNormalDgCode" CssClass="GridTable" runat="server" AutoGenerateColumns="False"  OnPageIndexChanged="grdNormalDgCode_PageIndexChanged">
                                                                                      
                                                                                       <ItemStyle CssClass="GridRow"/>
                                                                                        <Columns>
                                                                                            <asp:TemplateColumn>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description">
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                                        </Columns>
                                                                                      <HeaderStyle CssClass="GridHeader"/>
                                                                                    </asp:DataGrid>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table width="100%" id="tblDiagnosisCode" runat="server">
                                                                        <tr runat="server">
                                                                            <td width="100%" runat="server">
                                                                                <asp:DataGrid ID="grdPTDgCode" runat="server" Width="100%" CssClass="GridTable" AutoGenerateColumns="False"  AllowPaging="True">
                                                                                   <ItemStyle CssClass="GridRow"/>
                                                                                    <Columns>
                                                                                        <asp:TemplateColumn>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                                    </Columns>
                                                                                   <HeaderStyle CssClass="GridHeader"/>
                                                                                    <PagerStyle Mode="NumericPages" />
                                                                                </asp:DataGrid>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table width="100%" id="sue" runat="server">
                                                                        <tr runat="server">
                                                                            <td width="100%" runat="server">
                                                                                <asp:DataGrid Width="100%" ID="grdDoctorEvaluationDgCode" runat="server" CssClass="GridTable" AutoGenerateColumns="False"  AllowPaging="True">
                                                                                 
                                                                                     <ItemStyle CssClass="GridRow"/>
                                                                                    <Columns>
                                                                                        <asp:TemplateColumn>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                                    </Columns>
                                                                                 <HeaderStyle CssClass="GridHeader"/>
                                                                                    <PagerStyle Mode="NumericPages" />
                                                                                </asp:DataGrid>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlDeassociate" TabIndex="1">
                                <HeaderTemplate>
                                    <div style="width: 200px; height: 200px;" class="lbl">
                                        De-associate Diagnosis Code</div>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="100%" scope="col">
                                                <div class="blocktitle">
                                                    Patient diagnostic codes <em>(Select the doctor and the diagnostic codes)</em>
                                                    <div class="div_blockcontent">
                                                        <table width="100%" border="0">
                                                            <tr>
                                                                <td class="tablecellLabel" scope="col">
                                                                    <asp:Label ID="lblDeDoctor" runat="server" Text="Doctor"></asp:Label> 
                                                                </td>
                                                                <td>
                                                                    <extddl:ExtendedDropDownList ID="extddlAPDoctor" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"
                                                                        AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlAPDoctor_extendDropDown_SelectedIndexChanged" OldText="" StausText="False" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td align="center">
                                                                    <asp:DataGrid Width="100%" ID="grdAssignedDiagnosisCode" runat="server" OnPageIndexChanged="grdNormalDgCode_PageIndexChanged" CssClass="GridTable" AutoGenerateColumns="False"  AllowPaging="True">
                                                                      
                                                                       <ItemStyle CssClass="GridRow"/>
                                                                        <Columns>
                                                                            <asp:TemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="GridHeader"/>
                                                                        <PagerStyle Mode="NumericPages" />
                                                                    </asp:DataGrid></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnDeassociateDiagCode" runat="server" Text="De-Associate" Width="80px"
                                                                        CssClass="Buttons" OnClick="btnDeassociateDiagCode_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                   
                                        
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
    
   <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px" visible="false" ></iframe>
    </div>
</asp:Content>
