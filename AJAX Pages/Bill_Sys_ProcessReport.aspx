<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_ProcessReport.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_ProcessReport"
    Title="Green Your Bills - Import Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization" TagPrefix="CPA" %>
<%@ Register Src="~/UserControl/WUC_QuickLinks.ascx" TagName="WUC_QuickLinks" TagPrefix="QuickLinksBox" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
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
        function SetImportDate() {
            getWeek();
            var selValue = document.getElementById("<%=ddlImportDateValue.ClientID %>").value;
            if (selValue == 0) {
                document.getElementById("<%=txtImportToDate.ClientID %>").value = "";
                document.getElementById("<%=txtImportFromDate.ClientID %>").value = "";
            }
            else if (selValue == 1) {
                document.getElementById("<%=txtImportToDate.ClientID %>").value = getDate('today');
                document.getElementById("<%=txtImportFromDate.ClientID %>").value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById("<%=txtImportToDate.ClientID %>").value = getWeek('endweek');
                document.getElementById("<%=txtImportFromDate.ClientID %>").value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById("<%=txtImportToDate.ClientID %>").value = getDate('monthend');
                document.getElementById("<%=txtImportFromDate.ClientID %>").value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById("<%=txtImportToDate.ClientID %>").value = getDate('quarterend');
                document.getElementById("<%=txtImportFromDate.ClientID %>").value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById("<%=txtImportToDate.ClientID %>").value = getDate('yearend');
                document.getElementById("<%=txtImportFromDate.ClientID %>").value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById("<%=txtImportToDate.ClientID %>").value = getLastWeek('endweek');
                document.getElementById("<%=txtImportFromDate.ClientID %>").value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById("<%=txtImportToDate.ClientID %>").value = lastmonth('endmonth');
                document.getElementById("<%=txtImportFromDate.ClientID %>").value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById("<%=txtImportToDate.ClientID %>").value = lastyear('endyear');
                document.getElementById("<%=txtImportFromDate.ClientID %>").value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById("<%=txtImportToDate.ClientID %>").value = quarteryear('endquaeter');
                document.getElementById("<%=txtImportFromDate.ClientID %>").value = quarteryear('startquaeter');
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


function Clear()
{
    document.getElementById("<%=txtToDate.ClientID %>").value = '';
    document.getElementById("<%=txtFromDate.ClientID %>").value = '';
    document.getElementById("<%=ddlDateValues.ClientID %>").value = 0;
    document.getElementById("<%=txtImportToDate.ClientID %>").value = '';
    document.getElementById("<%=txtImportFromDate.ClientID %>").value = '';
    document.getElementById("<%=ddlImportDateValue.ClientID %>").value = 0;
    document.getElementById("ctl00_ContentPlaceHolder1_extddlLocation").value = 'NA';      
}
    </script>
    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%;
        border: 1px solid #B5DF82;">
        <tr>
            <td align="left">
                <table width="42%" style="border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82;
                    border-bottom: 1px solid #B5DF82">
                    <tr>
                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                            <b class="txt3">Search Parameters</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        Case NO
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        Number Of Days >=
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        Case Type
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:TextBox ID="txtCaseNo" runat="server" ></asp:TextBox>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:TextBox ID="txtNoOfDays" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="100%" Selected_Text="---Select---"
                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"
                                            CssClass="search-input"></extddl:ExtendedDropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        Appointment Date
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        From
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        To
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:DropDownList ID="ddlDateValues" runat="Server" Width="100%">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="1">Today</asp:ListItem>
                                            <asp:ListItem Value="2">This Week</asp:ListItem>
                                            <asp:ListItem Value="3">This Month</asp:ListItem>
                                            <asp:ListItem Value="4">This Quarter</asp:ListItem>
                                            <asp:ListItem Value="5">This Year</asp:ListItem>
                                            <asp:ListItem Value="6">Last Week</asp:ListItem>
                                            <asp:ListItem Value="7">Last Month</asp:ListItem>
                                            <asp:ListItem Value="9">Last Quarter</asp:ListItem>
                                            <asp:ListItem Value="8">Last Year</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="80%"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                            PopupButtonID="imgbtnFromDate" />
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="80%"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                            PopupButtonID="imgbtnToDate" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        Import Date
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        From
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        To
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:DropDownList ID="ddlImportDateValue" runat="Server" Width="100%">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="1">Today</asp:ListItem>
                                            <asp:ListItem Value="2">This Week</asp:ListItem>
                                            <asp:ListItem Value="3">This Month</asp:ListItem>
                                            <asp:ListItem Value="4">This Quarter</asp:ListItem>
                                            <asp:ListItem Value="5">This Year</asp:ListItem>
                                            <asp:ListItem Value="6">Last Week</asp:ListItem>
                                            <asp:ListItem Value="7">Last Month</asp:ListItem>
                                            <asp:ListItem Value="9">Last Quarter</asp:ListItem>
                                            <asp:ListItem Value="8">Last Year</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:TextBox ID="txtImportFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="80%"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnImportFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="calExtImportFromDate" runat="server" TargetControlID="txtImportFromDate"
                                            PopupButtonID="imgbtnImportFromDate" />
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:TextBox ID="txtImportToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="80%"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnImportToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="calExtImportToDate" runat="server" TargetControlID="txtImportToDate"
                                            PopupButtonID="imgbtnImportToDate" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                                        <asp:Label ID="lblLocation" runat="server" Text="Location" class="td-widget-bc-search-desc-ch1"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch3" valign="top">
                                        <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="100%" Selected_Text="---Select---"
                                            Procedure_Name="SP_MST_LOCATION" Flag_Key_Value="LOCATION_LIST" Connection_Key="Connection_String">
                                        </extddl:ExtendedDropDownList>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch3" valign="top">
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Width="80px"
                                        Text="Search"></asp:Button>
                                    <input style="width: 80px" id="btnClear1" onclick="Clear();" type="button" value="Clear"
                                        runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UP_grdBillSearch" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                    <b class="txt3">Import Visit</b>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdBillSearch"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <progresstemplate>
                                 <div id="Div1" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                     runat="Server">
                                     <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                         Height="25px" Width="24px"></asp:Image>
                                     Loading...</div>
                             </progresstemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <progresstemplate>
                                 <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                     runat="Server">
                                     <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                         Height="25px" Width="24px"></asp:Image>
                                     Loading...</div>
                             </progresstemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                        <table style="vertical-align: middle; width: 100%;">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: middle; width: 30%" align="left">
                                        Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                            CssClass="search-input">
                                        </gridsearch:XGridSearchTextBox>
                                        <%--<XCon:XControl ID="xcon" runat="server" Visible ="false"></XCon:XControl>--%>
                                    </td>
                                    <td style="width: 60%" align="right">
                                        Record Count:
                                        <%= this.grdImportVisit.RecordCount%>
                                        | Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                        <asp:LinkButton ID="btnExportToExcel" runat="server" Text="Export TO Excel" OnClick="btnExportToExcel_onclick">
                                 <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <xgrid:XGridViewControl ID="grdImportVisit" runat="server" Width="100%" CssClass="mGrid"
                                        DataKeyNames="" MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                        HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                        ExportToExcelFields="SZ_CASE_NO,PATIENT_NAME,DT_DATE_OF_SERVICE,SZ_PROCEDURE_GROUP,I_NO_OF_DAYS,SZ_LHR_CODE,SZ_CASE_TYPE_NAME,sz_remote_case_id,sz_remote_appointment_id,DT_IMPORT_DATE"
                                        ShowExcelTableBorder="true" ExportToExcelColumnNames="Case #,Patient Name,Date Of Service,Procedure Group,No Of Days,LHR Code,Case Type,Patient Id,Appointment Id,Date Of Import"
                                        AllowPaging="true" XGridKey="GET_IMPORT_VISIT_LHR" PageRowCount="50" PagerStyle-CssClass="pgr"
                                        AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                        <Columns>
                                            <%--0--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="convert(int,MST_CASE_MASTER.SZ_CASE_NO)" headertext="Case #"
                                                DataField="CASE_NO" />
                                            <%--1--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME" headertext="Patient Name"
                                                DataField="PATIENT_NAME" />
                                            <%--2--%>
                                            <%--1--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="convert(nvarchar,TXN_CALENDAR_EVENT.DT_EVENT_DATE,106)" DataFormatString="{0:MM/dd/yyyy}"
                                                headertext="Date Of Service" DataField="DT_DATE_OF_SERVICE" />
                                            <%--4--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="Patient ID" DataField="SZ_PATIENT_ID" visible="false" />
                                            <%--5--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="Procedure Group ID" DataField="SZ_PROCEDURE_GROUP_ID" visible="false" />
                                            <%--6--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression=" (select ISNULL(SZ_PROCEDURE_GROUP,'''')  from MST_PROCEDURE_GROUP where SZ_PROCEDURE_GROUP_ID in((select ISNULL(SZ_PROCEDURE_GROUP_ID,'''') from MST_DOCTOR where SZ_DOCTOR_ID= TXN_CALENDAR_EVENT.SZ_DOCTOR_ID)))"
                                                headertext="Procedure Group" DataField="SZ_PROCEDURE_GROUP" />
                                            <%--9--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="txn_calendar_event.dt_event_date" headertext="No Of Days" DataField="I_NO_OF_DAYS" />
                                            <%--10--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="(select top 1 isnull(sz_remote_procedure_desc,'''') from txn_visit_document where i_Event_id=txn_calendar_event.i_event_id and      i_event_id= TXN_CALENDAR_EVENT.I_EVENT_ID)"
                                                headertext="LHR Code" DataField="SZ_LHR_CODE" />
                                            <%--11--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="(select isnull( SZ_CASE_TYPE_NAME,'''') from MST_CASE_TYPE where SZ_CASE_TYPE_ID in(select SZ_CASE_TYPE_ID from  MST_CASE_MASTER where    SZ_CASE_ID= TXN_CALENDAR_EVENT.SZ_CASE_ID))"
                                                headertext="Case Type Name" DataField="SZ_CASE_TYPE_NAME" />
                                            <%--12--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="Remote Appointment Id" DataField="sz_remote_appointment_id" visible="false" />
                                            <%--13--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="remote Case ID" DataField="sz_remote_case_id" Visible="false" />
                                            <%--14--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                 SortExpression="convert(nvarchar, TXN_CALENDER_EVENT_PRPCEDURE.DT_IMPORT_DATE,101)" DataFormatString="{0:MM/dd/yyyy}"
                                                headertext="Date Of Import" DataField="DT_IMPORT_DATE" />
                                        </Columns>
                                    </xgrid:XGridViewControl>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="con" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
