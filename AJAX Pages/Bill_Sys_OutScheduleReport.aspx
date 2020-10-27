<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_OutScheduleReport.aspx.cs" Inherits="Bill_Sys_OutScheduleReport" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../validation.js"></script>


    <script language="javascript" type="text/javascript">

        function openTypePage(obj) {

            document.getElementById('divid').style.position = 'absolute';
            document.getElementById('divid').style.left = '450px';
            document.getElementById('divid').style.top = '250px';
            document.getElementById('divid').style.visibility = 'visible';
            document.getElementById('frameeditexpanse').src = "ViwScheduled.aspx?id=" + obj;

        }

        function ShowDiv() {
            document.getElementById('divDashBoard').style.position = 'absolute';
            document.getElementById('divDashBoard').style.left = '150px';
            document.getElementById('divDashBoard').style.top = '100px';
            document.getElementById('divDashBoard').style.visibility = 'visible';
            return false;
        }

        function ascii_value(c) {
            c = c.charAt(0);
            var i;
            for (i = 0; i < 256; ++i) {
                var h = i.toString(16);
                if (h.length == 1)
                    h = "0" + h;
                h = "%" + h;
                h = unescape(h);
                if (h == c)
                    break;
            }
            return i;
        }

        function clickButton1(e, charis) {
            var keychar;
            if (navigator.appName.indexOf("Netscape") > (-1)) {
                var key = ascii_value(charis);
                if (e.charCode == key || e.charCode == 0) {
                    return true;
                } else {
                    if (e.charCode < 48 || e.charCode > 57) {
                        return false;
                    }
                }
            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                var key = ""
                if (charis != "") {
                    key = ascii_value(charis);
                }
                if (event.keyCode == key) {
                    return true;
                }
                else {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        return false;
                    }
                }
            }
        }

        function CheckForInteger(e, charis) {
            var keychar;
            if (navigator.appName.indexOf("Netscape") > (-1)) {
                var key = ascii_value(charis);
                if (e.charCode == key || e.charCode == 0) {
                    return true;
                } else {
                    if (e.charCode < 48 || e.charCode > 57) {
                        return false;
                    }
                }
            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                var key = ""
                if (charis != "") {
                    key = ascii_value(charis);
                }
                if (event.keyCode == key) {
                    return true;
                }
                else {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        return false;
                    }
                }
            }


        }
        function SetDate() {
            getWeek();
            // var selValue = document.getElementById('ctl00_ContentPlaceHolder1_ddlDateValues').value;
            var selValue = document.getElementById("<% =ddlDateValues.ClientID%>").value;
            if (selValue == 0) {
                // document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate').value = "";
                document.getElementById("<% =txtEndDate.ClientID%>").value = "";
                //document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate').value = "";
                document.getElementById("<%=txtStartDate.ClientID %>").value = "";

            }
            else if (selValue == 1) {
                // document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate').value = getDate('today');
                document.getElementById("<% =txtEndDate.ClientID%>").value = getDate('today');
                //  document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate').value = getDate('today');
                document.getElementById("<%=txtStartDate.ClientID %>").value = getDate('today');
            }
            else if (selValue == 2) {
                // document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate').value = getWeek('endweek');
                document.getElementById("<%=txtEndDate.ClientID%>").value = getWeek('endweek');
                //document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate').value = getWeek('startweek');
                document.getElementById("<%=txtStartDate.ClientID %>").value = getWeek('startweek');
            }
            else if (selValue == 3) {
                // document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate').value = getDate('monthend');
                document.getElementById("<%=txtEndDate.ClientID %>").value = getDate('monthend');
                //  document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate').value = getDate('monthstart');
                document.getElementById("<%=txtStartDate.ClientID%>").value = getDate('monthstart');
            }
            else if (selValue == 4) {
                // document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate').value = getDate('quarterend');
                document.getElementById("<%=txtEndDate.ClientID %>").value = getDate('quarterend');
                //document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate').value = getDate('quarterstart');
                document.getElementById("<%=txtStartDate.ClientID %>").value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                // document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate').value = getDate('yearend');
                document.getElementById("<%=txtEndDate.ClientID %>").value = getDate('yearend');
                //document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate').value = getDate('yearstart');
                document.getElementById("<%=txtStartDate.ClientID %>").value = getDate('yearstart');
            }
            else if (selValue == 6) {
                //document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate').value = getLastWeek('endweek');
                document.getElementById("<%=txtEndDate.ClientID %>").value = getLastWeek('endweek');
                  //document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate').value = getLastWeek('startweek');
                  document.getElementById("<%=txtStartDate.ClientID%>").value = getLastWeek('startweek');
              } else if (selValue == 7) {
                  //  document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate').value = lastmonth('endmonth');
                  document.getElementById("<%=txtEndDate.ClientID %>").value = lastmonth('endmonth');

                // document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate').value = lastmonth('startmonth');
                document.getElementById("<%=txtStartDate.ClientID%>").value = lastmonth('startmonth');
            } else if (selValue == 8) {
                //  document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate').value =lastyear('endyear');
                document.getElementById("<%=txtEndDate.ClientID %>").value = lastyear('endyear');

                //  document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate').value = lastyear('startyear');
                document.getElementById("<%=txtStartDate.ClientID %>").value = lastyear('startyear');
            } else if (selValue == 9) {
                //   document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate').value = quarteryear('endquaeter');
                document.getElementById("<%=txtEndDate.ClientID %>").value = quarteryear('endquaeter');

                //   document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate').value =  quarteryear('startquaeter');
                document.getElementById("<%=txtStartDate.ClientID%>").value = quarteryear('startquaeter');
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

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnGenerateReport')">--%>
    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, <%=btnGenerateReport.ClientID %>)">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                    <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="LeftTop"></td>
                            <td class="CenterTop"></td>
                            <td class="RightTop"></td>
                        </tr>
                        <tr>
                            <td class="LeftCenter" style="height: 100%"></td>
                            <td class="Center" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <table width="100%" border="0" align="center" class="ContentTable" cellpadding="0"
                                                cellspacing="3">
                                                <tr>
                                                    <td class="tablecellLabel">
                                                        <div class="lbl">
                                                            Visit Date
                                                        </div>
                                                    </td>
                                                    <td></td>
                                                    <td style="width: 35%" colspan="1" onchange=" return javascript:SetDate();">
                                                        <asp:DropDownList ID="ddlDateValues" runat="Server">
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
                                                </tr>
                                                <tr>
                                                    <td class="tablecellLabel">
                                                        <div class="lbl">
                                                            Start Date
                                                        </div>
                                                    </td>
                                                    <td class="tablecellSpace"></td>
                                                    <td class="tablecellControl">
                                                        <asp:TextBox ID="txtStartDate" runat="server" Width="120px" MaxLength="50"></asp:TextBox>
                                                        <asp:ImageButton ID="imgStartDate" runat="server" ImageUrl="~/Images/cal.gif" />&nbsp;
                                                    </td>
                                                    <td class="tablecellLabel">
                                                        <div class="lbl">
                                                            End Date
                                                        </div>
                                                    </td>
                                                    <td class="tablecellSpace"></td>
                                                    <td class="tablecellControl">
                                                        <asp:TextBox ID="txtEndDate" runat="server" Width="120px" MaxLength="50"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/Images/cal.gif" />&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tablecellLabel">
                                                        <div class="lbl">
                                                            Status
                                                        </div>
                                                    </td>
                                                    <td class="tablecellSpace"></td>
                                                    <td class="tablecellControl">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="180px">
                                                            <asp:ListItem Value="NA">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">Visit Not Completed</asp:ListItem>
                                                            <asp:ListItem Value="1">Re-Schedule</asp:ListItem>
                                                            <asp:ListItem Value="2">Visit Completed</asp:ListItem>
                                                            <asp:ListItem Value="3">No Show</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td class="tablecellLabel" style="text-align: right">
                                                        <div class="lbl">
                                                            Doctor
                                                        </div>
                                                    </td>
                                                    <td class="tablecellSpace"></td>
                                                    <td class="tablecellControl">
                                                        <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="180px" Connection_Key="Connection_String"
                                                            Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"
                                                            AutoPost_back="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tablecellLabel" style="text-align: left">
                                                        <asp:Label ID="lblOffice" runat="server" Text="Office"></asp:Label>
                                                        <asp:Label ID="lblTestFacility" runat="server" Text="Test Facility" Font-Size="12px"></asp:Label></td>
                                                    <td class="tablecellSpace"></td>
                                                    <td class="tablecellControl">
                                                        <extddl:ExtendedDropDownList ID="extddlOffice" runat="server" Connection_Key="Connection_String"
                                                            Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_MST_OFFICE" Selected_Text="--- Select ---"
                                                            Width="180px"></extddl:ExtendedDropDownList>
                                                        <extddl:ExtendedDropDownList ID="extddlReferringFacility" runat="server" Connection_Key="Connection_String"
                                                            Flag_Key_Value="REFERRING_FACILITY_LIST" Procedure_Name="SP_TXN_REFERRING_FACILITY"
                                                            Selected_Text="--- Select ---" Width="180px"></extddl:ExtendedDropDownList>
                                                    </td>
                                                    <td class="tablecellLabel" style="text-align: left">

                                                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name" Font-Size="12px"> </asp:Label>
                                                    </td>
                                                    <td class="tablecellSpace"></td>
                                                    <td class="tablecellControl">
                                                        <asp:TextBox ID="txtPatientName" runat="server"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tablecellControl">
                                                        <asp:Label ID="lblCaseNO" runat="server" Text="Case No" Font-Size="12px"> </asp:Label>
                                                    </td>
                                                    <td class="tablecellControl"></td>
                                                    <td class="tablecellControl">
                                                        <asp:TextBox ID="txtCaseNo" runat="server"></asp:TextBox>
                                                        <%-- --%>
                                                    </td>
                                                    <td class="tablecellControl">

                                                        <asp:Label ID="lblChartNO" runat="server" Text="ChartNO"> </asp:Label>
                                                    </td>
                                                    <td class="tablecellControl">
                                                        <%-- --%>
                                                   
                                                    </td>
                                                    <td>
                                                        <%--  --%>

                                                        <asp:TextBox ID="txtChartNO" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" align="right">
                                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtSort" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:Button ID="btnGenerateReport" runat="server" Text="Show" Width="150px" OnClick="btnGenerateReport_Click"
                                                            CssClass="Buttons" />&nbsp;
                                                        <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                            OnClick="btnExportToExcel_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <asp:DataGrid ID="grdScheduleReport" runat="server" Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages"
                                                OnPageIndexChanged="grdScheduleReport_PageIndexChanged" OnSelectedIndexChanged="grdScheduleReport_SelectedIndexChanged"
                                                OnItemCommand="grdScheduleReport_ItemCommand">
                                                <FooterStyle />
                                                <SelectedItemStyle />
                                                <PagerStyle />
                                                <AlternatingItemStyle />
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <%--  <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="Office Name"></asp:BoundColumn> --%>
                                                    <asp:TemplateColumn HeaderText="Office Name" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlOffice" runat="server" CommandName="OfficeSearch" CommandArgument="SZ_OFFICE_NAME"
                                                                Font-Bold="true" Font-Size="12px">Office Name</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.SZ_OFFICE_NAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Chart No" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="ChartNoSearch" CommandArgument="CHART_NO"
                                                                Font-Bold="true" Font-Size="12px">Chart No</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.CHART_NO")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Case No" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlCaseNo" runat="server" CommandName="CaseNoSearch" CommandArgument="CASE_NO"
                                                                Font-Bold="true" Font-Size="12px">Case No</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.CASE_NO")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Patient Name" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="PatientNameSearch"
                                                                CommandArgument="PATIENT_NAME" Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a href='<%# DataBinder.Eval(Container, "DataItem.PATIENT_NAME")%>' target="_blank">POM</a>
                                                            <%# DataBinder.Eval(Container, "DataItem.PATIENT_NAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--
                                                <asp:BoundColumn DataField="CHART_NO" HeaderText="Chart No"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="CASE_NO" HeaderText="Case No"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name" ></asp:BoundColumn>--%>
                                                    <asp:BoundColumn DataField="PATIENT PHONE" HeaderText="Patient Phone"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PATIENT ACCIDENT DATE" HeaderText="Accident Date"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DOCTOR NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="INSURANCE NAME" HeaderText="Insurance Name"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Visit Date" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlVisteDate" runat="server" CommandName="VisteDateSearch" CommandArgument="VISIT"
                                                                Font-Bold="true" Font-Size="12px">Visit Date</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.VISIT_DATE")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--  <asp:BoundColumn DataField="VISIT_ORDER_DATE" HeaderText="Viste Date"></asp:BoundColumn>--%>
                                                    <asp:BoundColumn DataField="END TIME" HeaderText="End Time" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="STATUS" HeaderText="Status" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PROCEDURE CODE" HeaderText="Treated Codes"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_STUDY_NUMBER" HeaderText="Study No"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NOTES" HeaderText="Notes"></asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:DataGrid>
                                            <asp:DataGrid ID="grdForReport" runat="server" Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" Visible="false">
                                                <FooterStyle />
                                                <SelectedItemStyle />
                                                <PagerStyle />
                                                <AlternatingItemStyle />
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <%--             <asp:TemplateColumn HeaderText="Office Name" >
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlOffice" runat="server" CommandName="OfficeSearch" CommandArgument="SZ_OFFICE_NAME"
                                                            Font-Bold="true" Font-Size="12px">Office Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectOffice" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_OFFICE_NAME")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_OFFICE_NAME")%>' CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                 <asp:TemplateColumn HeaderText="Chart No">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="ChartNoSearch" CommandArgument="CHART_NO"
                                                            Font-Bold="true" Font-Size="12px">Chart No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectChartNo" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CHART_NO")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.CHART_NO")%>' CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                
                                                  <asp:TemplateColumn HeaderText="Case No" >
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlCaseNo" runat="server" CommandName="CaseNoSearch" CommandArgument="CASE_NO"
                                                            Font-Bold="true" Font-Size="12px">Case No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectCaseNo" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CASE_NO")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.CASE_NO")%>' CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                             
                                                
                                                  <asp:TemplateColumn HeaderText="Patient Name">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="PatientNameSearch" CommandArgument="PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectPatientName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>'
                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>' CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="Office Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="CHART_NO" HeaderText="Chart No"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="CASE_NO" HeaderText="Case No"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PATIENT PHONE" HeaderText="Patient Phone"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PATIENT ACCIDENT DATE" HeaderText="Accident Date"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DOCTOR NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="INSURANCE NAME" HeaderText="Insurance Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="VISIT_DATE" HeaderText="Visite"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="END TIME" HeaderText="End Time" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="STATUS" HeaderText="Status" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PROCEDURE CODE" HeaderText="Treated Codes"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_STUDY_NUMBER" HeaderText="Study No"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NOTES" HeaderText="Notes"></asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:DataGrid>
                                            <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate"
                                                PopupButtonID="imgStartDate" />
                                            <ajaxToolkit:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate"
                                                PopupButtonID="imgbtnEndDate" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="RightCenter" style="width: 10px; height: 100%;"></td>
                        </tr>
                        <tr>
                            <td class="LeftBottom"></td>
                            <td class="CenterBottom"></td>
                            <td class="RightBottom" style="width: 10px"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="divid" style="position: absolute; width: 350px; height: 200px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
            <div style="position: relative; text-align: right; background-color: #8babe4;">
                <a onclick="document.getElementById('divid').style.visibility='hidden';" style="cursor: pointer;"
                    title="Close">X</a>
            </div>
            <iframe id="frameeditexpanse" src="" frameborder="0" height="200px" width="350px"></iframe>
        </div>
        <div id="divDashBoard" visible="false" style="position: absolute; width: 600px; height: 480px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
            <div style="position: relative; text-align: right; background-color: #8babe4;">
                <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;"
                    title="Close">X</a>
            </div>
            <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%"
                        valign="top">
                        <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="LeftTop"></td>
                                <td class="CenterTop"></td>
                                <td class="RightTop"></td>
                            </tr>
                            <tr>
                                <td class="LeftCenter" style="height: 100%"></td>
                                <td class="Center" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                        <tr>
                                            <td class="Center" valign="top" width="45%">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="TDHeading" style="width: 100%">Today's Appointment</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="TDPart">
                                                            <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="SectionDevider"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="Center" width="45%" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="TDHeading" style="width: 100%">Weekly &nbsp;Appointment</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="TDPart">
                                                            <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="SectionDevider"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Center" valign="top" width="45%">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="TDHeading" style="width: 100%">Bill Status</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="TDPart">You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="SectionDevider"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="Center" width="45%" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="TDHeading" style="width: 100%">Desk</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="TDPart">You have &nbsp;
                                                        <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Center" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="TDHeading" style="width: 100%">Missing Information</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" class="TDPart">You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="RightCenter" style="width: 10px; height: 100%;"></td>
                            </tr>
                            <tr>
                                <td class="LeftBottom"></td>
                                <td class="CenterBottom"></td>
                                <td class="RightBottom" style="width: 10px"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
