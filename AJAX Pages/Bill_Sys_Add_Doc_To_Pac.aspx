<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Add_Doc_To_Pac.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Add_Doc_To_Pac" %>

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
    <asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
    </asp:scriptmanager>
    <link rel="Stylesheet" href="css/ticketing.css" type="text/css" />
    <script language="javascript" type="text/javascript">
        function showPopup2(CaseId, PatientName, EventId, PgId, VisitId, visitType, CaseNo, DoctorId) {
            var url = "Bill_Sys_Popup_For_Visit.aspx?CaseId=" + CaseId + "&PatientName=" + PatientName + "&EventId=" + EventId + "&PgId=" + PgId + "&VisitId=" + VisitId + "&visitType=" + visitType + "&DoctorId=" + DoctorId + "&CaseNo=" + CaseNo;
            IFrame_NewTicket.SetContentUrl(url);
            IFrame_NewTicket.Show();
            return false;


        }

        function OnIndexChnage(s, e) {


            var lastType = null;
            lastType = cIssueDate.GetValue().toString();
            alert(lastType);
            getWeek();
            if (lastType != "NA") {

                if (lastType == "1") {
                    var tDate = getDate('today');

                    cntdtfromdate.SetText(tDate);
                    cntdttodate.SetText(tDate);

                } else if (lastType == "0") {
                    cntdtfromdate.SetDate(null);
                    cntdttodate.SetDate(null);
                } else if (lastType == "2") {

                    cntdtfromdate.SetText(getWeek('startweek'));
                    cntdttodate.SetText(getWeek('endweek'));

                } else if (lastType == "3") {

                    cntdtfromdate.SetText(getDate('monthstart'));
                    cntdttodate.SetText(getDate('monthend'));

                } else if (lastType == "4") {

                    cntdtfromdate.SetText(getDate('quarterstart'));
                    cntdttodate.SetText(getDate('quarterend'));

                } else if (lastType == "5") {

                    cntdtfromdate.SetText(getDate('yearstart'));
                    cntdttodate.SetText(getDate('yearend'));

                } else if (lastType == "6") {

                    cntdtfromdate.SetText(getLastWeek('startweek'));
                    cntdttodate.SetText(getLastWeek('endweek'));

                } else if (lastType == "7") {

                    cntdtfromdate.SetText(lastmonth('startmonth'));
                    cntdttodate.SetText(lastmonth('endmonth'));

                } else if (lastType == "8") {

                    cntdtfromdate.SetText(lastyear('startyear'));
                    cntdttodate.SetText(lastyear('endyear'));

                } else if (lastType == "9") {

                    cntdtfromdate.SetText(quarteryear('startquaeter'));
                    cntdttodate.SetText(quarteryear('endquaeter'));

                }
            } else {
                cntdtfromdate.SetDate(null);
                cntdttodate.SetDate(null);
            }
        }

        function getLastWeek(type) {
            var d = new Date();
            d.setDate(d.getDate() - 7);
            var day = d.getDay();
            d.setDate(d.getDate() - day);
            if (type == 'startweek')
                return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
            if (type == 'endweek') {
                d.setDate(d.getDate() + 6);
                return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
            }
        }
        function lastmonth(type) {

            var d = new Date();
            var t_date = d.getDate();      // Returns the day of the month
            var t_mon = d.getMonth() + 1;      // Returns the month as a digit
            var t_year = d.getFullYear();

            if (type == 'startmonth') {
                if (t_mon == 1) {
                    var y = t_year - 1;
                    return ('12/1/' + y);

                }
                else {
                    var m = t_mon - 1;
                    return (m + '/1/' + t_year);
                }
            }
            else if (type == 'endmonth') {
                if (t_mon == 1) {
                    var y = t_year - 1;
                    return ('12/31/' + y);
                } else {
                    var m = t_mon - 1;
                    var d = daysInMonth(t_mon - 1, t_year);
                    return (m + '/' + d + '/' + t_year);
                }
            }

        }

        function quarteryear(type) {
            var d = new Date();
            var t_date = d.getDate();      // Returns the day of the month
            var t_mon = d.getMonth() + 1;      // Returns the month as a digit
            var t_year = d.getFullYear();

            if (type == 'startquaeter') {
                if (t_mon == 1 || t_mon == 2 || t_mon == 3) {
                    var y = t_year - 1;
                    return ('10/1/' + y);
                }
                else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
                    return ('1/1/' + t_year);

                } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
                    return ('4/1/' + t_year);


                } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
                    return ('7/1/' + t_year);

                }

            } else if (type == 'endquaeter') {
                if (t_mon == 1 || t_mon == 2 || t_mon == 3) {
                    //
                    var y = t_year - 1;
                    return ('12/31/' + y);
                }
                else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
                    return ('3/31/' + t_year);

                } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
                    return ('6/30/' + t_year);


                } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
                    return ('9/30/' + t_year);
                }

            }

        }

        function lastyear(type) {
            var d = new Date();

            var t_year = d.getFullYear();
            if (type == 'startyear') {
                y = t_year - 1;
                return ('1/1/' + y);
            }
            else if (type == 'endyear') {
                y = t_year - 1;
                return ('12/31/' + y);
            }
        }



        function getDate(type) {
            var d = new Date();
            var t_date = d.getDate();      // Returns the day of the month
            var t_mon = d.getMonth();      // Returns the month as a digit
            var t_year = d.getFullYear();  // Returns 4 digit year

            var q_start = 0;
            var q_end = 0;
            if ((t_mon + 1) >= 1 && (t_mon + 1) <= 3) {
                q_start = 1;
                q_end = 3;
            }
            else if ((t_mon + 1) >= 4 && (t_mon + 1) <= 6) {
                q_start = 4;
                q_end = 6;
            }
            else if ((t_mon + 1) >= 7 && (t_mon + 1) <= 9) {
                q_start = 7;
                q_end = 9;
            }
            else if ((t_mon + 1) >= 10 && (t_mon + 1) <= 12) {
                q_start = 10;
                q_end = 12;
            }

            if (type == 'today')
                return (getFormattedDate(t_date, t_mon, t_year));
            if (type == 'monthstart')
                return (getFormattedDate(1, t_mon, t_year));
            if (type == 'monthend')
                return (getFormattedDate(daysInMonth(t_mon + 1, t_year), t_mon, t_year));
            if (type == 'quarterstart') {
                return (getFormattedDateForMonth(1, q_start, t_year));
            }
            if (type == 'quarterend') {
                return (getFormattedDateForMonth(daysInMonth(q_end), q_end, t_year));
            }
            if (type == 'yearstart')
                return (getFormattedDate(1, 0, t_year));
            if (type == 'yearend')
                return (getFormattedDate(31, 11, t_year));
        }

        function daysInMonth(month, year) {
            var m = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
            if (month != 2) return m[month - 1];
            if (year % 4 != 0) return m[1];
            if (year % 100 == 0 && year % 400 != 0) return m[1];
            return m[1] + 1;
        }

        function getFormattedDate(day, month, year) {
            return '' + (month + 1) + '/' + day + '/' + year;
        }

        function getFormattedDateForMonth(day, month, year) {
            return '' + (month) + '/' + day + '/' + year;
        }

        function getWeek(type) {
            var d = new Date();
            var day = d.getDay();
            d.setDate(d.getDate() - day);
            if (type == 'startweek')
                return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
            if (type == 'endweek') {
                d.setDate(d.getDate() + 6);
                return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
            }
        }


    </script>
    <asp:updatepanel id="UpdatePanel2" runat="server">
        <contenttemplate>
    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server">
        <PanelCollection>
            <dx:PanelContent>
                <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                    background-color: White;">
                    <tr>
                        <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 3px; width: 100%;
                            padding-top: 3px; height: 100%; vertical-align: top;">
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
                                    <td colspan="3">
                                        <asp:updateprogress id="UpdateProgress123" runat="server" associatedupdatepanelid="UpdatePanel2"
                                            displayafter="10">
                                            <progresstemplate>
                                                                                                        <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 350px;
                                                                                                            left: 600px" runat="Server">
                                                                                                            <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                                                            Loading...</div>
                                                                                                    </progresstemplate>
                                        </asp:updateprogress>
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
                                                                                                <td align="left" valign="middle" colspan="3" style="background-color: #CDCAB9; font-family: Calibri;
                                                                                                    font-size: 20px; font-weight: normal; font-style: italic;">
                                                                                                    Search Parameters
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Visit Date</label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        From
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        To
                                                                                                    </label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                                                                                        ValueType="System.String" ClientInstanceName="cIssueDate" CssClass="inputBox"
                                                                                                        ID="ddlDateValues">
                                                                                                        <Items>
                                                                                                            <dxe:ListEditItem Text="--Select--" Value="NA" Selected="True"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="All" Value="0"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Today" Value="1"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Week" Value="2"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Month" Value="3"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Quarter" Value="4"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Year" Value="5"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Week" Value="6"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Month" Value="7"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Quarter" Value="9"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Year" Value="8"></dxe:ListEditItem>
                                                                                                        </Items>
                                                                                                        <ItemStyle>
                                                                                                            <HoverStyle BackColor="#F6F6F6">
                                                                                                            </HoverStyle>
                                                                                                        </ItemStyle>
                                                                                                        <ClientSideEvents SelectedIndexChanged="OnIndexChnage" />
                                                                                                    </dxe:ASPxComboBox>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdtfromdate" CssClass="inputBox"
                                                                                                        ID="dtfromdate">
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdttodate" CssClass="inputBox"
                                                                                                        ID="dttodate">
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Doctor Name
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Case#
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Billed/Un-Billed
                                                                                                    </label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxComboBox runat="server" EnableSynchronization="False" ValueType="System.String"
                                                                                                        ClientInstanceName="cIssueDoc" CssClass="inputBox" ID="ddlDoctor">
                                                                                                        <ItemStyle>
                                                                                                            <HoverStyle BackColor="#F6F6F6">
                                                                                                            </HoverStyle>
                                                                                                        </ItemStyle>
                                                                                                    </dxe:ASPxComboBox>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                                                                                        ID="txtCaseNo">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                                                                                        ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                                                                                        ID="ddlBiled">
                                                                                                        <Items>
                                                                                                            <dxe:ListEditItem Text="--Select--" Value="2" Selected="True"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Bill" Value="1"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Unbill" Value="0"></dxe:ListEditItem>
                                                                                                        </Items>
                                                                                                        <ItemStyle>
                                                                                                            <HoverStyle BackColor="#F6F6F6">
                                                                                                            </HoverStyle>
                                                                                                        </ItemStyle>
                                                                                                    </dxe:ASPxComboBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3" align="center">
                                                                                                    <dx:ASPxButton runat="server" Text="Search" ID="BtnSearch">
                                                                                                    </dx:ASPxButton>
                                                                                                </td>
                                                                                                <tr>
                                                                                                    <td colspan="3">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
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
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <div style="height: 400px; width: 100%; background-color: gray; overflow: scroll;">
                                                <dx:ASPxGridView ID="grdVisits" runat="server" KeyFieldName="SZ_CASE_ID" AutoGenerateColumns="false"
                                                    Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                                    Settings-VerticalScrollableHeight="330">
                                                    <columns>
                                                    <%--0--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="Case ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
                                                    </dx:GridViewDataColumn>
                                                    <%--1--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_PATIENT_ID" Caption="Patient ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
                                                    </dx:GridViewDataColumn>
                                                    <%--2--%>
                                                      <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="Event ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
                                                    
                                                    </dx:GridViewDataColumn>
                                                    <%--3--%>
                                                     <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE_ID" Caption="Visit Type ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
                                                    </dx:GridViewDataColumn>
                                                    <%--4--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="Doctor ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
                                                    </dx:GridViewDataColumn>

                                                      <%--5--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
                                                    </dx:GridViewDataColumn>
                                                    <%--6--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case#" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                       <%--7--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                       <%--8--%>
                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Visit Date" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                       <%--9--%>
                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="Visit Type" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                     <%--10--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_NAME" Caption="Doctor Name" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                     <%--11--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP" Caption="Specialty" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>


                                                    <%--12--%>
                                                    <dx:GridViewDataColumn Caption="Open File" Settings-AllowSort="False" Width="25px">
                                                    
                                                        <HeaderTemplate >
                                                         Add Doc.
                                                        </HeaderTemplate>
                                                        <DataItemTemplate>
                                                            <asp:linkbutton id="lnkAddVisit" runat="server" text='Add Document' commandname="" onclick='<%# "showPopup2(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ", " + "\""+ Eval("SZ_PATIENT_NAME") +"\""+ ", " + "\""+ Eval("I_EVENT_ID") +"\""+ ", " + "\""+ Eval("SZ_PROCEDURE_GROUP_ID") +"\""+ ", "  + "\""+ Eval("SZ_VISIT_TYPE_ID") +"\""+ ", "  + "\""+ Eval("VISIT_TYPE") +"\""+ "," + "\""+ Eval("SZ_CASE_NO") +"\""+ "," + "\""+ Eval("SZ_DOCTOR_ID") +"\"); return false;" %> '>
                                                            </asp:linkbutton>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                    </dx:GridViewDataColumn>
                                                </columns>
                                                    <settingsbehavior allowfocusedrow="True" allowsort="False" />
                                                    <settingspager pagesize="20"></settingspager>
                                                    <settings verticalscrollableheight="330"></settings>
                                                    <settingscustomizationwindow height="330px"></settingscustomizationwindow>
                                                    <styles>
                                                    <FocusedRow CssClass="dxgvFocusedGroupRow">
                                                    </FocusedRow>
                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                    </AlternatingRow>
                                                    <SelectedRow CssClass="dxgvFocusedGroupRow">
                                                    </SelectedRow>
                                                </styles>
                                                </dx:ASPxGridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:textbox id="txtCompanyID" runat="server" visible="false">
                </asp:textbox>
                <dx:ASPxPopupControl ID="IFrame_NewTicket" runat="server" CloseAction="CloseButton"
                    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ClientInstanceName="IFrame_NewTicket" HeaderText="Upload Document" HeaderStyle-HorizontalAlign="Left"
                    HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
                    EnableAnimation="False" EnableViewState="True" Width="800px" ToolTip="Open New Ticket"
                    PopupHorizontalOffset="0" PopupVerticalOffset="0"   AutoUpdatePosition="true"
                    ScrollBars="Auto" RenderIFrameForPopupElements="Default" Height="500px">
                    <ContentStyle>
                        <Paddings PaddingBottom="5px" />
                    </ContentStyle>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    </contenttemplate>
    </asp:updatepanel>
</asp:content>
