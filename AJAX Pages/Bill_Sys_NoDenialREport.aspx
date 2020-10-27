<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_NoDenialREport.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_NoDenialREport" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            //prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);



            function EndRequest(sender, args) {
                $('[id*=extddlInsurance]').multiselect({
                    includeSelectAllOption: false,
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    dropRight: false,
                    buttonWidth: 250,
                    nonSelectedText: "---Select---",
                });
                $("#ctl00_ContentPlaceHolder1_extddlCaseType").addClass("form-control")
            }
        });

        $(document).ready(function () {
            $('[id*=extddlInsurance]').multiselect({
                includeSelectAllOption: false,
                enableFiltering: true,
                enableCaseInsensitiveFiltering: true,
                dropRight: false,
                buttonWidth: 250,
                nonSelectedText: "---Select---",
            });
            $("#ctl00_ContentPlaceHolder1_extddlCaseType").addClass("form-control")
        });




    </script>
    <script type="text/javascript">

        function ShowDenialChildGrid(obj) {
            var div1 = document.getElementById(obj);
            div1.style.display = 'block';
        }

        function HideDenialChildGrid(obj) {
            var div1 = document.getElementById(obj);
            div1.style.display = 'none';
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


        <%--function SelectAll(ival) {
            var f = document.getElementById("<%= grdvDenial.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    f.getElementsByTagName("input").item(i).checked = ival;
                }
            }
        }--%>


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

        function openURL(url) {
            if (url == "") {
                alert("There is no bill created for the selected record!");
            }
            else {
                var url1 = url;
                window.open(url1, "", "top=10,left=100,menubar=no,toolbar=no,location=no,resizable=no,width=750,height=600,status=no,scrollbars=no,maximize=null,resizable=0,titlebar=no;");
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
       <%-- function CheckGrid() {
            var f = document.getElementById('<%=grdvDenial.ClientID %>');
            if (f == null) {
                bfFlag = true;
            }
            if (bfFlag == true) {
                alert('Data not available!');
                bfFlag = false;
                return false;
            }
            else {
                return true;
            }
        }--%>


        function SetDate() {
            getWeek();
            var selValue = document.getElementById('<%=ddlDateValues.ClientID %>').value;
            if (selValue == 0) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = "";
                document.getElementById('<%=txtupdatefromdate.ClientID %>').value = "";

            }
            else if (selValue == 1) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('today');
                document.getElementById('<%=txtupdatefromdate.ClientID %>').value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getWeek('endweek');
                document.getElementById('<%=txtupdatefromdate.ClientID %>').value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('monthend');
                document.getElementById('<%=txtupdatefromdate.ClientID %>').value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('quarterend');
                document.getElementById('<%=txtupdatefromdate.ClientID %>').value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('yearend');
                document.getElementById('<%=txtupdatefromdate.ClientID %>').value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getLastWeek('endweek');
                document.getElementById('<%=txtupdatefromdate.ClientID %>').value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = lastmonth('endmonth');

                document.getElementById('<%=txtupdatefromdate.ClientID %>').value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = lastyear('endyear');

                document.getElementById('<%=txtupdatefromdate.ClientID %>').value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = quarteryear('endquaeter');

                document.getElementById('<%=txtupdatefromdate.ClientID %>').value = quarteryear('startquaeter');
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

        <%-- function Validate() {


            var f = document.getElementById("<%= grdvDenial.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        if (confirm("Do you want to sent bill to litigation?")) {

                            return true;
                        }
                        return false;
                    }
                }
            }
            alert('Please select bill no.');
            return false;
        }--%>
        
    </script>
    <script type="text/javascript">
        function selectValue() {
            debugger;

            var lbl = document.getElementById('ctl00_ContentPlaceHolder1_lblpomdays');
            if (document.getElementById("ctl00_ContentPlaceHolder1_rblpomanswered_0").checked == true) {


                lbl.innerText = "No denial since days of POM";
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_rblpomanswered_1").checked == true) {



                lbl.innerText = "No denial since days of verification answered";
            }
        }

        function Clear() {

            document.getElementById('ctl00_ContentPlaceHolder1_txtupdateToDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtupdatefromdate').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtupdateCaseNo').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtupdateBillNo').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtupdatePatientName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_ddlDateValues').value = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_extddlInsurance').value = "NA";
            document.getElementById('ctl00_ContentPlaceHolder1_extddlCaseType').value = "NA";



        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel112" runat="server" Visible="true">
        <ContentTemplate>

            <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                        <table <%--id="MainBodyTable"--%> cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td <%--class="LeftTop"--%>></td>
                                <td <%--class="CenterTop"--%>></td>
                                <td <%--class="RightTop"--%>></td>
                            </tr>
                            <tr>
                                <td <%--class="LeftCenter"--%> style="height: 100%"></td>
                                <td valign="top">
                                    <table border="0" cellpadding="1" cellspacing="3" style="width: 100%; height: 100%; background-color: White;">
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td <%--class="ContentLabel"--%> style="text-align: left; width: 50%;">
                                                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 90%; height: 257px; border: 1px solid #B5DF82;">
                                                                <tr>
                                                                    <%--<td align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 50%;
                                                                height: 3px;">
                                                                &nbsp;&nbsp;<b class="txt3">Search Parameters</b>                                                                  
                                                            </td>--%>
                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px"><b class="txt3">Search Parameters</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 50%; height: 0px;" align="center">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                            <tr>
                                                                                <td class="td-widget-bc-search-desc-ch1">Bill Date</td>
                                                                                <td class="td-widget-bc-search-desc-ch1">From Date</td>
                                                                                <td align="left" style="font-size: 1.00em;font-family: Verdana; font-weight: bold">To Date
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <asp:DropDownList ID="ddlDateValues" runat="Server">
                                                                                        <asp:ListItem Value="0">All</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Today</asp:ListItem>
                                                                                        <asp:ListItem Value="2">This Week</asp:ListItem>
                                                                                        <asp:ListItem Value="3" Selected="True">This Month</asp:ListItem>
                                                                                        <asp:ListItem Value="4">This Quarter</asp:ListItem>
                                                                                        <asp:ListItem Value="5">This Year</asp:ListItem>
                                                                                        <asp:ListItem Value="6">Last Week</asp:ListItem>
                                                                                        <asp:ListItem Value="7">Last Month</asp:ListItem>
                                                                                        <asp:ListItem Value="9">Last Quarter</asp:ListItem>
                                                                                        <asp:ListItem Value="8">Last Year</asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:TextBox ID="txtupdatefromdate" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox><asp:ImageButton
                                                                                        ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    <ajaxcontrol:CalendarExtender
                                                                                        ID="CalendarExtender3"
                                                                                        runat="server"
                                                                                        PopupButtonID="imgbtnFromDate"
                                                                                        TargetControlID="txtupdatefromdate"></ajaxcontrol:CalendarExtender>

                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtupdateToDate" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox><asp:ImageButton
                                                                                        ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    <ajaxcontrol:CalendarExtender
                                                                                        ID="CalendarExtender4"
                                                                                        runat="server"
                                                                                        PopupButtonID="imgbtnToDate"
                                                                                        TargetControlID="txtupdateToDate"></ajaxcontrol:CalendarExtender>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-widget-bc-search-desc-ch1">Case No</td>
                                                                                <td class="td-widget-bc-search-desc-ch1">Bill No</td>
                                                                                <td align="left"  style="font-size: 1.00em;font-family: Verdana; font-weight: bold">Patient Name</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <asp:TextBox ID="txtupdateCaseNo" runat="server" Width="90px"></asp:TextBox>

                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:TextBox ID="txtupdateBillNo" runat="server" Width="89px"></asp:TextBox>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtupdatePatientName" runat="server" Width="90px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>


                                                                            <tr>

                                                                                <td class="td-widget-bc-search-desc-ch1" colspan="2">Insurance Name</td>
                                                                                <td class="td-widget-bc-search-desc-ch1">Case Type</td>

                                                                            </tr>
                                                                            <tr>

                                                                                <td class="td-widget-bc-search-desc-ch1" colspan="2">

                                                                                    <asp:ListBox runat="server" DataTextField="DESCRIPTION" Width="250px" DataValueField="CODE" ID="extddlInsurance" Visible="true" SelectionMode="Multiple"></asp:ListBox>
                                                                                    <%--<extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="250px" Connection_Key="Connection_String"
                                                                                        Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                                                        Selected_Text="---Select---" CssClass="cinput" Visible="true" />--%>
                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="125px" Selected_Text="---Select---"
                                                                                        Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"
                                                                                        Visible="true"></extddl:ExtendedDropDownList>
                                                                                </td>
                                                                            </tr>


                                                                            <tr>

                                                                                <td align="left" colspan="2" style="padding-left: 5px">
                                                                                    <asp:RadioButtonList ID="rblpomanswered" runat="server" RepeatDirection="Vertical"
                                                                                        Width="118%" Font-Size="12px">
                                                                                        <asp:ListItem Text="Since POM sent" Value="1" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Since verification answered" Value="2"></asp:ListItem>

                                                                                    </asp:RadioButtonList>
                                                                                </td>

                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 80%">
                                                                                    <table width="100%">
                                                                                        <tr>
                                                                                            <td class="td-widget-bc-search-desc-ch1">
                                                                                                <dx:ASPxLabel ID="lblpomdays" runat="server" Font-Size="12px" Text="No denial since days of POM" Font-Bold="true"></dx:ASPxLabel>
                                                                                                <br />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-bc-search-desc-ch1" style="">
                                                                                                <asp:DropDownList ID="ddldays" Style="width: 80px; margin-left: 38%" CssClass="form-control" runat="Server" Width="40%">
                                                                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                                                                    <asp:ListItem Value="30" Selected="True">30</asp:ListItem>
                                                                                                    <asp:ListItem Value="45">45</asp:ListItem>
                                                                                                    <asp:ListItem Value="60">60</asp:ListItem>
                                                                                                    <asp:ListItem Value="90">90</asp:ListItem>
                                                                                                    <asp:ListItem Value="120">120</asp:ListItem>

                                                                                                </asp:DropDownList></td>
                                                                                        </tr>
                                                                                    </table>



                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2">&nbsp &nbsp &nbsp 
                                                                            <%--<extddl:ExtendedDropDownList 
                                                                                ID="extddenial" 
                                                                                Width="250px"
                                                                                runat="server" 
                                                                                Connection_Key="Connection_String" 
                                                                                Procedure_Name="SP_MST_DENIAL"
                                                                                Flag_Key_Value="DENIAL_LIST" 
                                                                                Selected_Text="--- Select ---" 
                                                                                CssClass="cinput" Visible="true" />--%>
                                                                                </td>

                                                                                <td align="center" valign="top"></td>

                                                                            </tr>


                                                                            <tr>
                                                                                <td align="center" colspan="3">
                                                                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click1" Text="Search" Width="80px" />
                                                                                    <input style="width: 80px" id="Button1" onclick="Clear();" type="button" value="Clear"
                                                                                        runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-widget-bc-search-desc-ch1" colspan="2"></td>
                                                                                <td class="td-widget-bc-search-desc-ch1"></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>

                                                        <td valign="top" style="width: 10px">
                                                            <table id="div12" runat="server" cellpadding="0" cellspacing="0" style="width: 90%;">
                                                                <tr>
                                                                    <td>
                                                                        <%-- <div style="overflow: scroll; height: 250px">
                                                                     <xgrid:XGridViewControl id="testgrid1" runat="server" CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153" 
                                                                        EnableRowClick="false" HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="false" GridLines="None" XGridKey="DenialReasonsReport" PagerStyle-CssClass="pgr" DataKeyNames="SZ_DENIAL_REASONS" AllowSorting="true">
                                                                <Columns>
                                                                       <asp:BoundField DataField="SZ_DENIAL_REASONS" HeaderText="DENIAL REASON" SortExpression="SZ_DENIAL_REASONS" >
                                                                        <headerstyle horizontalalign="center"></headerstyle>
                                                                            <itemstyle width="35%" horizontalalign="Left"></itemstyle>
                                                                         </asp:BoundField>
                                                                       <asp:BoundField DataField="TOTAL_BILLED" DataFormatString={0:c} HeaderText="TOTAL BILLED" SortExpression="SUM(TXN_BILL_TRANSACTIONS.FLT_BILL_AMOUNT)" >
                                                                        <headerstyle horizontalalign="center"></headerstyle>
                                                                            <itemstyle width="20%" horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="TOTAL_PAID"  DataFormatString={0:c} HeaderText="TOTAL PAID" SortExpression="SUM((TXN_BILL_TRANSACTIONS.FLT_BILL_AMOUNT)-(TXN_BILL_TRANSACTIONS.FLT_BALANCE))" >
                                                                        <headerstyle horizontalalign="center"></headerstyle>
                                                                            <itemstyle width="20%" horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                       
                                                                       <asp:BoundField DataField="TOTAL_DENIAL_AMOUNT" DataFormatString={0:c} HeaderText="TOTAL BALANCE" SortExpression="SUM(TXN_BILL_TRANSACTIONS.FLT_BALANCE)">
                                                                        <headerstyle horizontalalign="center"></headerstyle>
                                                                            <itemstyle width="20%" horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="NO_OF_BILLS" HeaderText="BILL COUNT" SortExpression="count(TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)">
                                                                        <headerstyle horizontalalign="center"></headerstyle>
                                                                            <itemstyle width="5%" horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>                                                                    
                                                               </Columns> 
                                                    </xgrid:XGridViewControl>
                                                    </div>--%>
                                            
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <caption>

                                            <tr>
                                                <td>
                                                    <UserMessage:MessageControl ID="usrMessage" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 1127px; height: auto;">
                                                    <div style="width: 100%;">
                                                        <table align="left" cellpadding="0" cellspacing="0" class="txt2" class="border" style="height: auto; width: 100%; border: 0px solid #B5DF82;">
                                                            <tr>
                                                                <td align="left" bgcolor="#B5DF82" class="txt2" height="28" style="width: 413px" valign="middle">
                                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel112" DisplayAfter="10">
                                                                        <ProgressTemplate>
                                                                            <div id="DivStatus2" runat="Server" class="PageUpdateProgress" style="text-align: center; vertical-align: bottom;">
                                                                                <asp:Image ID="img2" runat="server" AlternateText="Loading....." Height="25px" ImageUrl="~/Images/rotation.gif" Width="24px" />
                                                                                Loading...
                                                                            </div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 1017px;">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>
                                                                            <table style="vertical-align: middle; width: 100%;">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td align="left" style="vertical-align: middle; width: 50%">Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input">
                                                                                        </gridsearch:XGridSearchTextBox>
                                                                                        </td>
                                                                                        <td align="right" style="vertical-align: middle; width: 50%; text-align: right">Record Count:<%= this.grdvNoDenial.RecordCount %>| Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                            <asp:LinkButton ID="lnkExportToExcel" runat="server" Text="Export TO Excel" OnClick="lnkExportTOExcel_onclick">
                                                                                    <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/>
                                                                                            </asp:LinkButton>
                                                                                            <asp:Button ID="btnLitigantion" runat="server" Text="Litigate" OnClick="btnLitigantion_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <xgrid:XGridViewControl ID="grdvNoDenial" runat="server" AllowPaging="true" AllowSorting="true" AlternatingRowStyle-BackColor="#EEEEEE" AutoGenerateColumns="false" ContextMenuID="ContextMenu1" CssClass="mGrid" EnableRowClick="false" ExcelFileNamePrefix="ExcelLitigation"
                                                                                ExportToExcelColumnNames="Bill No.,Case #,Patient Name,Provider Name,Bill Status,Visit Date"
                                                                                ExportToExcelFields="BillNo,CASE#,SZ_PATIENT_NAME,SZ_INSURANCE_NAME,VISIT_DATE,Days" GridLines="None" HeaderStyle-CssClass="GridViewHeader" MouseOverColor="0, 153, 153" PageRowCount="50"
                                                                                PagerStyle-CssClass="pgr" ShowExcelTableBorder="true" Width="100%" XGridKey="No_Denial_report">
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                <PagerStyle CssClass="pgr" />
                                                                                <AlternatingRowStyle BackColor="#EEEEEE" />
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="BillNo" HeaderText="Bill No">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="CASE#" HeaderText="Case #" SortExpression="convert(int,SZ_CASE_NO)">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="PATIENT_NAME" HeaderText="Patient Name" SortExpression="(ISNULL(SZ_PATIENT_FIRST_NAME,'')  +' '+ ISNULL(SZ_PATIENT_LAST_NAME ,''))">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Name" SortExpression="SZ_INSURANCE_NAME">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>

                                                                                    <asp:BoundField DataField="PROVIDER_NAME" HeaderText="Provider Name" SortExpression="MST_OFFICE.SZ_OFFICE">
                                                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                                    </asp:BoundField>

                                                                                    <asp:BoundField DataField="Specialty" HeaderText="Speciality" SortExpression="MST_OFFICE.SZ_OFFICE">
                                                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                                    </asp:BoundField>

                                                                                    <asp:BoundField DataField="SZ_BILL_STATUS_NAME" HeaderText="Bill Status">
                                                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                                    </asp:BoundField>

                                                                                    <asp:BoundField DataField="CASE_TYPE" HeaderText="Case Type" SortExpression="SZ_INSURANCE_NAME">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="VISIT_DATE" HeaderText="Visit Date">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>

                                                                                     <asp:BoundField DataField="DT_BILL_DATE" HeaderText="Bill Date">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>

                                                                                    <asp:BoundField DataField="Days" HeaderText="Days">
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);" ToolTip="Select All" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="ChkLitigantion" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <%--<asp:TemplateField>
                                                                         <itemstyle width="10px" horizontalalign="Left"></itemstyle>
                                                                        </asp:TemplateField>--%>
                                                                                </Columns>
                                                                            </xgrid:XGridViewControl>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </caption>
                                    </table>
                                </td>
                                <td <%--class="RightCenter"--%> style="width: 10px; height: 100%;">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td <%--class="LeftBottom"--%>></td>
                                <td <%--class="CenterBottom"--%>></td>
                                <td <%--class="RightBottom" --%>style="width: 10px"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtCaseNo" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtBillNo" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtBillStatus" runat="server" Text="VR" Visible="false" Width="10px" />
    <asp:TextBox ID="txtDay" runat="server" Text="" Visible="false" Width="10px" />
    <asp:TextBox ID="txtFlag" runat="server" Text="REF" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtFromDate" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtToDate" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtPatientName" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtDenial" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtDoctor" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtLocation" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtCaseType" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtInsurance" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtDenialFromdt" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtDenialTodt" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtDays" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <asp:TextBox ID="txtPOMVerification" runat="server" Text="" Visible="false" Width="10px">
    </asp:TextBox>
    <script type="text/javascript">

        SetDate();
    </script>
    <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
    <link href="../packages/bootstrap.3.3.7/content/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../packages/bootstrap.3.3.7/content/Scripts/bootstrap.min.js"></script>

    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>

</asp:Content>
