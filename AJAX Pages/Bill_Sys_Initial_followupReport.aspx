<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Initial_followupReport.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Initial_followupReport" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
    </asp:scriptmanager>
    <script type="text/javascript" src="../validation.js"></script>
    <script type="text/javascript">
        function Clear() {
        }
    </script>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        background-color: White;">
        <tr>
            <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
                            <tr>
                                <td colspan="3">
                                    <asp:updatepanel id="UpdatePanel10" runat="server">
                                        <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                                    </asp:updatepanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 100%">
                                </td>
                                <td valign="top">
                                    <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%;
                                        background-color: White;">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0">
                                                    <tr>
                                                        <td style="text-align: center; width: 100%; vertical-align: top;">
                                                            <table style="text-align: center; width: 50%;">
                                                                <tr>
                                                                    <td style="text-align: center; width: 100%; vertical-align: top;">
                                                                        <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;
                                                                            height: 50%; border: 0px solid #B5DF82;">
                                                                            <tr>
                                                                                <td style="width: 100%; height: 0px;" align="center">
                                                                                    <table border="0" cellpadding="10" cellspacing="0" style="width: 100%; border-right: 1px solid #d3d3d3;
                                                                                        border-left: 1px solid #d3d3d3; border-bottom: 1px solid #d3d3d3" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                        <tr>
                                                                                            <td height="28" align="left" valign="middle" bgcolor="#d3d3d3" class="txt2" colspan="2">
                                                                                                <b class="txt3">Search Parameters</b>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-bc-search-desc-ch">
                                                                                                IE Visit From Date
                                                                                            </td>
                                                                                            <td class="td-widget-bc-search-desc-ch">
                                                                                                IE Visit To Date
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <dx:ASPxDateEdit ID="dtfromdate" runat="server">
                                                                                                </dx:ASPxDateEdit>
                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <dx:ASPxDateEdit ID="dttodate" runat="server">
                                                                                                </dx:ASPxDateEdit>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-bc-search-desc-ch">
                                                                                                Doctor Name
                                                                                            </td>
                                                                                            <td class="td-widget-bc-search-desc-ch">
                                                                                                Specialty
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <cc1:ExtendedDropDownList ID="extddlDoctor" Width="69%" runat="server" Connection_Key="Connection_String"
                                                                                                    Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <cc1:ExtendedDropDownList ID="extddlSpeciality" Width="93%" runat="server" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                                                    Selected_Text="---Select---"></cc1:ExtendedDropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr runat="server" id="trLocation">
                                                                                            <td class="td-widget-bc-search-desc-ch">
                                                                                                Location
                                                                                            </td>
                                                                                            <td class="td-widget-bc-search-desc-ch">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <cc1:ExtendedDropDownList ID="extddlLocation" runat="server" Width="69%" Connection_Key="Connection_String"
                                                                                                    Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---"
                                                                                                    Visible="false" />
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="20" colspan="2">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2" style="width: 100%" align="center">
                                                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                    <contenttemplate>
                                                                                                <asp:updateprogress id="UpdateProgress123" runat="server" associatedupdatepanelid="UpdatePanel2"
                                                                                                    displayafter="10">
                                                                                                    <progresstemplate>
                                                                                                        <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 200px;
                                                                                                            left: 600px" runat="Server">
                                                                                                            <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Searching....."
                                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                                                            Processing...
                                                                                                        </div>
                                                                                                    </progresstemplate>
                                                                                                </asp:updateprogress>
                                                                                                &nbsp;
                                                                                          
                                                                                                <asp:button id="btnSearch" style="width: 10%"  runat="server" text="Search" onclick="btnSearch_Click" />
                                                                                               
                                                                                                <input style="width: 60px" id="btnClear" onclick="Clear();" type="button" value="Clear"
                                                                                                    runat="server" />
                                                                                            </contenttemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div style="height: 400px; width: 100%; background-color: gray; overflow: scroll;">
                                        <dx:ASPxGridView ID="grdVisits" runat="server" KeyFieldName="SZ_CASE_ID" AutoGenerateColumns="false"
                                            Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                            Settings-VerticalScrollableHeight="330" onhtmldatacellprepared="grdVisits_HtmlDataCellPrepared" 
                                            >
                                            <Columns>
                                                <%--0--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="Case ID" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                </dx:GridViewDataColumn>
                                                <%--1--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_ID" Caption="Patient ID" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                </dx:GridViewDataColumn>
                                                <%--2--%>
                                                <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="Doctor ID" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                </dx:GridViewDataColumn>
                                                <%--3--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Specialty ID" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                </dx:GridViewDataColumn>
                                                <%--4--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case#" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="true" Width="28px">
                                                </dx:GridViewDataColumn>
                                                <%--5--%>
                                                <dx:GridViewDataColumn FieldName="PATIENT_NAME" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="true"     Width="40px">
                                                    
                                                </dx:GridViewDataColumn>
                                                <%--6--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_PHONE" Caption="Patient Phone" HeaderStyle-HorizontalAlign="Center"
                                                  Width="15px"  Settings-AllowSort="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--7--%>
                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Visit Date" HeaderStyle-HorizontalAlign="Center"
                                                        Width="10px" Settings-AllowSort="true">
                                                        <cellstyle backgroundimage-repeat="NoRepeat"  HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <%--8--%>
                                                    <dx:GridViewDataColumn FieldName="DT_NEXT_VISIT_DATE" Caption="Next Visit Date" HeaderStyle-HorizontalAlign="Center"
                                                        Width="10px" Settings-AllowSort="true">
                                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <%--9--%>
                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="Type" HeaderStyle-HorizontalAlign="Center" 
                                                        Width="6px" Settings-AllowSort="true">
                                                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <%--10--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP" Caption="Specialty" HeaderStyle-HorizontalAlign="Center"
                                                        Width="25px" Settings-AllowSort="true">
                                                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <%--11--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_NAME" Caption="Doctor Name" HeaderStyle-HorizontalAlign="Center"
                                                        Width="40px" Settings-AllowSort="true">
                                                    </dx:GridViewDataColumn>

                                                     <%--12--%>
                                                <dx:GridViewDataColumn FieldName="SZ_VISIT" Caption="SZ_VISIT" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                    </dx:GridViewDataColumn>
                                                  
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                            <Styles>
                                                <FocusedRow CssClass="dxgvFocusedGroupRow">
                                                </FocusedRow>
                                                <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                </AlternatingRow>
                                                <SelectedRow CssClass="dxgvFocusedGroupRow">
                                                </SelectedRow>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:textbox id="txtCompanyID" runat="server" visible="false">
                                    </asp:textbox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:asyncpostbacktrigger controlid="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:content>
