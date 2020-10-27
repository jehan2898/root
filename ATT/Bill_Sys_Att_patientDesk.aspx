<%@ Page Language="C#" MasterPageFile="~/ATT/AttMaster.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Att_patientDesk.aspx.cs" Inherits="ATT_Bill_Sys_Att_patientDesk"
    Title="Untitled Page" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="oem" Namespace="OboutInc.EasyMenu_Pro" Assembly="obout_EasyMenu_Pro" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr> 
                        <td align="right">
                            <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataList ID="DtlPatientDesk" runat="server" BorderWidth="0px" BorderStyle="None"
                                CssClass="TDPart" BorderColor="#DEBA84" Width="100%">
                                <ItemTemplate>
                                    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; border: #8babe4 1px solid #B5DF82;">
                                        <tr>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Case#</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="tblheader" runat="server">
                                                <b>Name</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Insurance Company</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Attorney Company</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Accident Date</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_ID")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" id="tblvalue" runat="server" style="border: 1px solid #B5DF82">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_NAME")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ATTORNEY_NAME")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT", "{0:MM/dd/yyyy}")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divtab" style="width: 1250px; overflow: scroll;">
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxPageControl ID="tabVistInformation" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
                                    Height="250" Width="100%">
                                    <TabPages>
                                        <dx:TabPage Name="tabpnlOne" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl1" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportOne" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportOne_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdOne" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdOne" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportOne" runat="server" GridViewID="grdOne">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwo" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl2" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwo" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwo_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwo" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdTwo" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwo" runat="server" GridViewID="grdTwo">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThree" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl3" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThree" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThree_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThree" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdThree" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThree" runat="server" GridViewID="grdThree">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlFour" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl4" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportFour" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportFour_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdFour" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportFour" runat="server" GridViewID="grdFour">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlFive" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl5" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportFive" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportFive_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdFive" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportFive" runat="server" GridViewID="grdFive">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlSix" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl6" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportSix" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportSix_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdSix" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportSix" runat="server" GridViewID="grdSix">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlSeven" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl7" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportSeven" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportSeven_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdSeven" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportSeven" runat="server" GridViewID="grdSeven">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlEight" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl8" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportEight" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportEight_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdEight" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportEight" runat="server" GridViewID="grdEight">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlNine" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl9" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportNine" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportNine_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdNine" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportNine" runat="server" GridViewID="grdNine">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl10" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTen" runat="server" GridViewID="grdTen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlEleven" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl11" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportEleven" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportEleven_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdEleven" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportEleven" runat="server" GridViewID="grdEleven">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwelve" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl12" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwelve" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwelve_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwelve" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwelve" runat="server" GridViewID="grdTwelve">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl13" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirteen" runat="server" GridViewID="grdThirteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlFourteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl14" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportFourteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportFourteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdFourteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportFourteen" runat="server" GridViewID="grdFourteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlFifteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl15" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportFifteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportFifteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdFifteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportFifteen" runat="server" GridViewID="grdFifteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlSixteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl16" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportSixteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportSixteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdSixteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportSixteen" runat="server" GridViewID="grdSixteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlSeventeen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl17" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportSeventeen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportSeventeen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdSeventeen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportSeventeen" runat="server" GridViewID="grdSeventeen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlEighteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl18" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportEighteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportEighteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdEighteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportEighteen" runat="server" GridViewID="grdEighteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlNineteen" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl19" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportNineteen" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportNineteen_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdNineteen" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportNineteen" runat="server" GridViewID="grdNineteen">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwenty" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl20" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwenty" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwenty_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwenty" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwenty" runat="server" GridViewID="grdTwenty">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyOne" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl21" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyOne" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyOne_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyOne" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyOne" runat="server" GridViewID="grdTwentyOne">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyTwo" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl22" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyTwo" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyTwo_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyTwo" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyTwo" runat="server" GridViewID="grdTwentyTwo">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyThree" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl23" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyThree" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyThree_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyThree" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyThree" runat="server" GridViewID="grdTwentyThree">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyFour" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl24" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyFour" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyFour_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyFour" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyFour" runat="server" GridViewID="grdTwentyFour">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyFive" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl25" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyFive" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyFive_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyFive" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyFive" runat="server" GridViewID="grdTwentyFive">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentySix" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl26" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentySix" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentySix_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentySix" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentySix" runat="server" GridViewID="grdTwentySix">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentySeven" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl27" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentySeven" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentySeven_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentySeven" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdFour" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentySeven" runat="server" GridViewID="grdTwentySeven">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyEight" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl28" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyEight" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyEight_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyEight" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdTwentyEight" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyEight" runat="server" GridViewID="grdTwentyEight">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlTwentyNine" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl29" runat="server">
                                                    <table border="0px" width="10%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportTwentyNine" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportTwentyNine_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdTwentyNine" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdTwentyEight" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportTwentyNine" runat="server" GridViewID="grdTwentyNine">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirty" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl30" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirty" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirty_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirty" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdTwentyEight" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="true">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirty" runat="server" GridViewID="grdThirty">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirtyOne" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl31" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirtyOne" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirtyOne_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirtyOne" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdTwentyEight" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirtyOne" runat="server" GridViewID="grdThirtyOne">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirtyTwo" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl32" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirtyTwo" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirtyTwo_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirtyTwo" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdThirtyTwo" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirtyTwo" runat="server" GridViewID="grdThirtyTwo">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirtyThree" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl33" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirtyThree" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirtyThree_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirtyThree" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdThirtyTwo" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirtyThree" runat="server" GridViewID="grdThirtyThree">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirtyFour" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl34" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirtyFour" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirtyFour_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirtyFour" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdThirtyTwo" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirtyFour" runat="server" GridViewID="grdThirtyFour">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabpnlThirtyFive" Visible="False">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl35" runat="server">
                                                    <table border="0px" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0px" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table width="70%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExportThirtyFive" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExportThirtyFive_Click" HorizontalAlign="Right" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="dxscControlCell" style="width: 400px; border-bottom: 0px solid #CBDAE6;"
                                                                            valign="top">
                                                                            <dx:ASPxGridView ID="grdThirtyFive" runat="server" Width="70%" SettingsBehavior-AllowSort="false"
                                                                                SettingsPager-PageSize="20" ClientInstanceName="grdThirtyTwo" KeyFieldName="I_EVENT_ID">
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="VISTID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_NAME" Caption="OFFICE NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DOCTOR_NAME" Caption="DOCTOR NAME" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_DATE_SHOW" Caption="TREATMENT DATE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxComboBox runat="server" ID="ddlStatus" Enabled="false" Width="100px">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="Scheduled" Value="0" />
                                                                                                    <dxe:ListEditItem Text="Re-Scheduled" Value="1" />
                                                                                                    <dxe:ListEditItem Text="Completed" Value="2" />
                                                                                                    <dxe:ListEditItem Text="No-show" Value="3" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="STATUS" Caption="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "VISIT_TYPE")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ATT" Caption="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="TEST" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="EVENT_ID" Caption="Eventid" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="TYPE_CODE_ID" Caption="TYPE CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COLOR_CODE" Caption="COLOR_CODE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Caption="Treatments" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PROC_CODES")%>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROC_CODES" Caption="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE_ID" Caption="VISIT_TYPE_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="I_STATUS" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "I_EVENT_ID")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="IS_REFERRAL" Caption="Referral Doctor" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SCHEDULE_TYPE")%>'>
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SCHEDULE_TYPE" Caption="SCHEDULE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Event Date" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME" Caption="Event Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_TIME_TYPE" Caption="Event Time Type" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME" Caption="Event End Time" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_END_TIME_TYPE" Caption="Event End Time Type"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID"
                                                                                        HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="BT_DOC" Caption="BT_DOC" HeaderStyle-HorizontalAlign="Left"
                                                                                        Visible="false">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                                <Styles>
                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                                                    </FocusedRow>
                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                    </AlternatingRow>
                                                                                </Styles>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="grdExportThirtyFive" runat="server" GridViewID="grdThirtyFive">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                </dx:ASPxPageControl>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtCompanyId" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtPatientId" runat="server" Width="10px" Visible="false"></asp:TextBox>
</asp:Content>
