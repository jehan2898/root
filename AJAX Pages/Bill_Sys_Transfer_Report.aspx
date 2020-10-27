<%@ Page Title="Green Your Bills- Transfer Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
CodeFile="Bill_Sys_Transfer_Report.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Transfer_Report" %>



<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Data" Assembly="DevExpress.Data.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<%@ Register Assembly="DevExpress.XtraCharts.v16.2.Web, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="dxcharts" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.PivotGrid.v16.2.Core, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraPivotGrid" TagPrefix="temp" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
<script type="text/javascript" src="../validation.js"></script>
    <script type="text/javascript">

        function SetDate() {
            getWeek();
            var selValue = document.getElementById("<%=ddlDateValues.ClientID %>").value;
            if (selValue == 0) {
                document.getElementById("<%=txtToDate.ClientID %>").value = "";
                document.getElementById("<%=txtFromDate.ClientID %>").value = "";
            }
            else if (selValue == 1) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('today');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getWeek('endweek');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('monthend');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('quarterend');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('yearend');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getLastWeek('endweek');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById("<%=txtToDate.ClientID %>").value = lastmonth('endmonth');
                document.getElementById("<%=txtFromDate.ClientID %>").value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById("<%=txtToDate.ClientID %>").value = lastyear('endyear');
                document.getElementById("<%=txtFromDate.ClientID %>").value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById("<%=txtToDate.ClientID %>").value = quarteryear('endquaeter');
                document.getElementById("<%=txtFromDate.ClientID %>").value = quarteryear('startquaeter');
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

        function Clear() {
            //alert("call");

            document.getElementById("<%=txtFromDate.ClientID %>").value = '';
            document.getElementById("<%=txtToDate.ClientID %>").value = '';
            document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value = 0;
            SelectAllLawFirm(false);
            
        }

        function ClickOnButton() {
            document.getElementById("<%=hdnClick.ClientID %>").value = '1';
            return true;
        }

        function SelectAllLawFirm(ival) {
            //alert(ival);
            var f = document.getElementById('<%=grdLawFirm.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }

                }
            }
        }
        function mapSelectedClick() {


            var flag = false;

            var f = document.getElementById('<%=grdLawFirm.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {

                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked == true) {
                        flag = true;
                    }
                }
            }

            if (flag == false) {
                alert('Please select atleast 1 Law firm company');
                return false;
            }
        }

    </script>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        background-color: White;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
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
                        <td style="height: 100%">
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%;
                                background-color: White;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="text-align: left; width: 100%; vertical-align: top;">
                                                    <table style="text-align: left; width: 50%;">
                                                        <tr>
                                                            <td style="text-align: left; width: 100%; vertical-align: top;">
                                                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                                    height: 50%; border: 0px solid #B5DF82;">
                                                                    <tr>
                                                                        <td style="width: 100%; height: 0px;" align="center">
                                                                            <table border="0" cellpadding="10" cellspacing="0" style="width: 100%; border-right: 1px solid #d3d3d3;
                                                                                border-left: 1px solid #d3d3d3; border-bottom: 1px solid #d3d3d3" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                <tr>
                                                                                    <td height="28" align="left" valign="middle" bgcolor="#d3d3d3" class="txt2" colspan="3">
                                                                                        <b class="txt3">Search Parameters</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Transferred Date
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        From
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        To
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:dropdownlist id="ddlDateValues" runat="Server" width="90%">
                                                                                            <asp:listitem value="0">All</asp:listitem>
                                                                                            <asp:listitem value="1">Today</asp:listitem>
                                                                                            <asp:listitem value="2">This Week</asp:listitem>
                                                                                            <asp:listitem value="3">This Month</asp:listitem>
                                                                                            <asp:listitem value="4">This Quarter</asp:listitem>
                                                                                            <asp:listitem value="5">This Year</asp:listitem>
                                                                                            <asp:listitem value="6">Last Week</asp:listitem>
                                                                                            <asp:listitem value="7">Last Month</asp:listitem>
                                                                                            <asp:listitem value="9">Last Quarter</asp:listitem>
                                                                                            <asp:listitem value="8">Last Year</asp:listitem>
                                                                                        </asp:dropdownlist>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:textbox id="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            maxlength="10" width="76%">
                                                                                        </asp:textbox>
                                                                                        <asp:imagebutton id="imgbtnFromDate" runat="server" imageurl="~/Images/cal.gif" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:textbox id="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            maxlength="10" width="70%">
                                                                                        </asp:textbox>
                                                                                        <asp:imagebutton id="imgbtnToDate" runat="server" imageurl="~/Images/cal.gif" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td  class="td-widget-bc-search-desc-ch" align="left" >
                                                                                        Law Firm 
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 33%">
                                                                                        <div style="height: 200px; background-color: Gray; overflow: scroll;">
                                                                                            <dx:ASPxGridView ID="grdLawFirm" runat="server" Width="100%" SettingsBehavior-AllowSort="false"
                                                                                                            SettingsPager-PageSize="20" ClientInstanceName="grdLawFirm" KeyFieldName="CODE">
                                                                                                <Columns>
                                                                                                    <dx:GridViewDataColumn Caption="chk1" VisibleIndex="0" Width="30px">
                                                                                                        <HeaderTemplate>
                                                                                                            <asp:CheckBox ID="chkSelectAllLawFirm" runat="server" onclick="javascript:SelectAllLawFirm(this.checked);"
                                                                                                            ToolTip="Select All" />
                                                                                                        </HeaderTemplate>
                                                                                                        <DataItemTemplate>
                                                                                                        <asp:CheckBox ID="chkall1" Visible="true" runat="server" />
                                                                                                        </DataItemTemplate>
                                                                                                    </dx:GridViewDataColumn>
                                                                                                    <dx:GridViewDataColumn FieldName="DESCRIPTION" Caption="Law Firm" VisibleIndex="1">
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                    </dx:GridViewDataColumn>
                                                                                                    <dx:GridViewDataColumn FieldName="CODE" Caption="Law Firm Id" VisibleIndex="2" Visible="false">
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                    </dx:GridViewDataColumn>
                                                                                                </Columns>
                                                                                                <%--<SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                                                                                <Styles>
                                                                                                    <FocusedRow BackColor="#8C001A" ForeColor="White">
                                                                                                    </FocusedRow>
                                                                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                                                                    </AlternatingRow>
                                                                                                </Styles>--%>
                                                                                                <SettingsPager PageSize="1000">
                                                                                                </SettingsPager>
                                                                                            </dx:ASPxGridView>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                
                                                                                <tr>
                                                                                    <td colspan="3" style="width: 100%" align="center">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:updateprogress id="UpdateProgress123" runat="server" associatedupdatepanelid="UpdatePanel2"
                                                                                                    displayafter="10">
                                                                                                    <progresstemplate>
                                                                                                        <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 200px;
                                                                                                            left: 600px" runat="Server">
                                                                                                            <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Searching....."
                                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                                                            Searching...
                                                                                                        </div>
                                                                                                    </progresstemplate>
                                                                                                </asp:updateprogress>
                                                                                                &nbsp;
                                                                                                <asp:button id="btnSearch" runat="server" text="Search" onclick="btnSearch_Click" />
                                                                                                <input style="width: 60px" id="btnClear1" onclick="Clear();" type="button" value="Clear"
                                                                                                    runat="server" />
                                                                                            </ContentTemplate>
                                                                                            
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
                        <td style="padding-right: 4px" align="right" colspan="3">
                            <asp:linkbutton id="btnXlsExport" onclick="btnXlsExport_Click" runat="server" text="Export TO Excel">
                                <img src="Images/Excel.jpg" alt="" style="border: none;" height="15px" width="15px"
                                    title="Export TO Excel" />
                            </asp:linkbutton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div style="height: 600px; width: 1135px; background-color: gray; overflow: scroll;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grdTransfer" runat="server" KeyFieldName="SZ_CASE_ID" AutoGenerateColumns="false"
                                            Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                            Settings-VerticalScrollableHeight="330">
                                            <Columns>
                                                <%--0--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="CASE ID" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                </dx:GridViewDataColumn>
                                                <%--1--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case#" HeaderStyle-HorizontalAlign="Center"
                                                    Width="25px" Settings-AllowSort="true">
                                                </dx:GridViewDataColumn>
                                                <%--2--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_FIRST_NAME" Caption="Patient First Name" HeaderStyle-HorizontalAlign="Center"
                                                    Width="25px" Settings-AllowSort="true">
                                                </dx:GridViewDataColumn>
                                                <%--3--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_LAST_NAME" Caption="Patient Last Name" HeaderStyle-HorizontalAlign="Center"
                                                    Width="25px" Settings-AllowSort="true">
                                                </dx:GridViewDataColumn>
                                                <%--4--%>
                                                <dx:GridViewDataColumn FieldName="SZ_INSURANCE_NAME" Caption="INSURANCE NAME" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                </dx:GridViewDataColumn>
                                                <%--5--%>
                                                <dx:GridViewDataColumn FieldName="SZ_INSURANCE_ADDRESS" Caption="INSURANCE ADDRESS" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                </dx:GridViewDataColumn>
                                                <%--6--%>
                                                <dx:GridViewDataColumn FieldName="SZ_INSURANCE_CITY" Caption="INSURANCE CITY" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                </dx:GridViewDataColumn>
                                                <%--7--%>
                                                <dx:GridViewDataColumn FieldName="SZ_STATE" Caption="INSURANCE STATE" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                </dx:GridViewDataColumn>
                                                <%--8--%>
                                                <dx:GridViewDataColumn FieldName="SZ_INSURANCE_ZIP" Caption="INSURANCE ZIP" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                </dx:GridViewDataColumn>
                                                <%--9--%>
                                                <dx:GridViewDataColumn FieldName="SZ_INSURANCE_PHONE" Caption="INSURANCE PHONE" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                </dx:GridViewDataColumn>
                                                <%--10--%>
                                                <dx:GridViewDataColumn FieldName="SZ_INSURANCE_EMAIL" Caption="INSURANCE MAIL" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                </dx:GridViewDataColumn>
                                                <%--11--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_ADDRESS" Caption="PATIENT ADDRESS" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <%--12--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_STREET" Caption="PATIENT STREET" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--13--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_CITY" Caption="PATIENT CITY" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--14--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_STATE" Caption="PATIENT STATE" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--15--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_ZIP" Caption="PATIENT ZIP" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--16--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_PHONE" Caption="PATIENT PHONE" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--17--%>
                                                <dx:GridViewDataColumn FieldName="SZ_POLICY_NUMBER" Caption="POLICY NUMBER" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--18--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CLAIM_NUMBER" Caption="CLAIM NUMBER" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--19--%>
                                                <dx:GridViewDataColumn FieldName="SZ_STATUS_NAME" Caption="STATUS NAME" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--20--%>
                                                <dx:GridViewDataColumn FieldName="SZ_ATTORNEY_NAME" Caption="ATTORNEY NAME" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--21--%>
                                                <dx:GridViewDataColumn FieldName="SZ_ATTORNEY_LAST_NAME" Caption="ATTORNEY LAST NAME" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--22--%>
                                                <dx:GridViewDataColumn FieldName="SZ_ATTORNEY_ADDRESS" Caption="ATTORNEY ADDRESS" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--23--%>
                                                <dx:GridViewDataColumn FieldName="SZ_ATTORNEY_CITY" Caption="ATTORNEY CITY" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--24--%>
                                                <dx:GridViewDataColumn FieldName="SZ_ATTORNEY_STATE" Caption="ATTORNEY STATE" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--25--%>
                                                <dx:GridViewDataColumn FieldName="SZ_ATTORNEY_ZIP" Caption="ATTORNEY ZIP" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--26--%>
                                                <dx:GridViewDataColumn FieldName="SZ_ATTORNEY_FAX" Caption="ATTORNEY FAX" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--27--%>
                                                <dx:GridViewDataColumn FieldName="SZ_SOCIAL_SECURITY_NO" Caption="SSN" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--28--%>
                                                <dx:GridViewDataColumn FieldName="DT_DOB" Caption="DOB" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--29--%>
                                                <dx:GridViewDataColumn FieldName="SZ_POLICY_HOLDER" Caption="POLICY HOLDER" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--30--%>
                                                <dx:GridViewDataColumn FieldName="SZ_BILL_NUMBER" Caption="BILL NUMBER" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--31--%>
                                                <dx:GridViewDataColumn FieldName="DATE OF SERVICE" Caption="DOS" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--32--%>
                                                <dx:GridViewDataColumn FieldName="FLT_BILL_AMOUNT" Caption="BILL AMOUNT" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--33--%>
                                                <dx:GridViewDataColumn FieldName="FLT_PAID" Caption="PAID" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--34--%>
                                                <dx:GridViewDataColumn FieldName="FLT_BALANCE" Caption="BALANCE" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--35--%>
                                                <dx:GridViewDataColumn FieldName="DT_START_VISIT_DATE" Caption="VISTI FROM" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--36--%>
                                                <dx:GridViewDataColumn FieldName="DT_END_VISIT_DATE" Caption="VISIT TO" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--37--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_NAME" Caption="CASE TYPE" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--38--%>
                                                <dx:GridViewDataColumn FieldName="Location" Caption="LOCATION" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--39--%>
                                                <dx:GridViewDataColumn FieldName="Copmayid" Caption="Copmayid" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--40--%>
                                                <dx:GridViewDataColumn FieldName="Provider" Caption="Provider" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                                <%--41--%>
                                                <dx:GridViewDataColumn FieldName="DT_DATE_OF_ACCIDENT" Caption="DOA" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true" Width="73px">
                                                </dx:GridViewDataColumn>
                                                <%--42--%>
                                                <dx:GridViewDataColumn FieldName="Denial Reasons" Caption="DENIAL REASON" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true" Width="73px">
                                                </dx:GridViewDataColumn>
                                                <%--43--%>
                                                <dx:GridViewDataColumn FieldName="Verification" Caption="VERIFICATION" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true" Width="73px">
                                                </dx:GridViewDataColumn>
                                                <%--44--%>
                                                <dx:GridViewDataColumn FieldName="AssignLaw firm" Caption="LAW FIRM" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="true" Width="73px">
                                                </dx:GridViewDataColumn>
                                                <%--45--%>
                                                <dx:GridViewDataColumn FieldName="Transfer Amount" Caption="TRANSFER AMOUNT" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px">
                                                </dx:GridViewDataColumn>
                                                <%--46--%>
                                                <dx:GridViewDataColumn FieldName="DT_DATE_OF_TRANSFERRED" Caption="TRANSFERED DATE" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px" >
                                                </dx:GridViewDataColumn>
                                                
                                                  <%--47--%>
                                                <dx:GridViewDataColumn FieldName="DT_BILL_DATE" Caption="BILL DATE" HeaderStyle-HorizontalAlign="Center"
                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                                </dx:GridViewDataColumn>
                                            
                                            </Columns>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grdTransfer">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:textbox id="txtCompanyID" runat="server" visible="false">
                            </asp:textbox>
                            <asp:hiddenfield id="hdnClick" runat="server">
                            </asp:hiddenfield>
                            <asp:textbox id="txtNotesType" runat="server" visible="false">
                            </asp:textbox>
                            <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true">
                            </ajaxcontrol:MaskedEditExtender>
                            <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true">
                            </ajaxcontrol:MaskedEditExtender>
                            <ajaxcontrol:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                PopupButtonID="imgbtnFromDate" />
                            <ajaxcontrol:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                PopupButtonID="imgbtnToDate" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

