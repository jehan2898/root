<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VisitReports.aspx.cs" Inherits="AJAX_Pages_Visit_Reports" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../Css/UI.css" rel="stylesheet" />
    <link href="../Css/main.css" type="text/css" rel="Stylesheet" />
    <style type="text/css" rel="stylesheet">
.ajax__calendar {
 position : absolute;
 }
</style>
    <script type="text/javascript">


        function ValidateSearch() {
            debugger;

            if (document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlShowPatientVisit').value == 0) {
                alert('Please select value to proceed.');
                return false;
            }
            else {
                if (document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlShowPatientVisit').value == 2) {

                    if (document.getElementById("ctl00_ContentPlaceHolder1_ASPxPageControl1_extddlMissingVisitsSpeciality").value == 'NA') {
                        alert('Please select Speciality to proceed.');
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else {
                    if (document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlShowPatientVisit').value == 3)
                    {
                        if (document.getElementById("ctl00_ContentPlaceHolder1_ASPxPageControl1_extddlMissingVisitsSpeciality").value == 'NA') {
                            alert('Please select Speciality to proceed.');
                            return false;
                        } else {
                            if (document.getElementById("<%=txtdays.ClientID%>").value == '') {
                                alert('Enter days to proceed.');
                                return false;
                            }
                            else {
                                return true;
                            }

                        }
                    }
                }
            }
        }

           
            


        

        function selectValue() {


            var lbl = document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_lblDate');
            if (document.getElementById("ctl00_ContentPlaceHolder1_ASPxPageControl1_rblInitialReeval_0").checked == true) {


                lbl.innerText = "Accident Date";
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_ASPxPageControl1_rblInitialReeval_1").checked == true) {



                lbl.innerText = "Re-eval Date";
            }
        }
    </script>
    <script type="text/javascript">

        function SelectAllSpeciality(ival) {
            //alert(ival);
            var f = document.getElementById('ctl00_ContentPlaceHolder1_grdSpeciality');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }


        function showPateintFrame(caseid) {
            debugger;
        
            var value = "";

            var r1 = document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_rblNoshow_0');
            var r2 = document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_rblNoshow_1');
            var r3 = document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_rblNoshow_2');
            var r4 = document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_rblNoshow_3');
            if (r1.checked) {
                value = "0";
            }
            if (r2.checked) {
                value = "1";
            }
            if (r3.checked) {
                value = "2";
            }
            if (r4.checked) {
                value = "3";
            }

            var url = "PatientUnseenFrame.aspx?CaseID=" + caseid + "&Status=" + value;
            //alert(url);
            PatientInfoPop.SetContentUrl(url);
            PatientInfoPop.Show();
            return false;
        }
        function Export() {
            expLoadingPanel.Show();
            Callback1.PerformCallback();
        }
        function OnCallbackComplete(s, e) {

            expLoadingPanel.Hide();
            var url = "../RP/DownloadFiles.aspx";
            IFrame_DownloadFiles.SetContentUrl(url);
            IFrame_DownloadFiles.Show();
            return false;
        }
        function SetDate() {
            debugger;
            getWeek();
            var selValue = document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlDateValues').value;
            if (selValue == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDate').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDate').value = "";

            }
            else if (selValue == 1) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDate').value = getDate('today');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDate').value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDate').value = getWeek('endweek');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDate').value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDate').value = getDate('monthend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDate').value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDate').value = getDate('quarterend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDate').value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDate').value = getDate('yearend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDate').value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDate').value = getLastWeek('endweek');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDate').value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDate').value = lastmonth('endmonth');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDate').value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDate').value = lastyear('endyear');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDate').value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDate').value = quarteryear('endquaeter');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDate').value = quarteryear('startquaeter');
            }
        }
        function SetNoShowDate() {
            debugger;

            getWeek();
            var selValue = document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlNoShowDate').value;
            if (selValue == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDateRange').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDateRange').value = "";

            }
            else if (selValue == 1) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDateRange').value = getDate('today');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDateRange').value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDateRange').value = getWeek('endweek');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDateRange').value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDateRange').value = getDate('monthend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDateRange').value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDateRange').value = getDate('quarterend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDateRange').value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDateRange').value = getDate('yearend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDateRange').value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDateRange').value = getLastWeek('endweek');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDateRange').value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDateRange').value = lastmonth('endmonth');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDateRange').value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDateRange').value = lastyear('endyear');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDateRange').value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtToDateRange').value = quarteryear('endquaeter');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtFromDateRange').value = quarteryear('startquaeter');
            }
        }

        function SetMissingVisitDate() {
            debugger;

            getWeek();
            var selValue = document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlMissingVisit').value;
            if (selValue == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = "";

            }
            else if (selValue == 1) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = getDate('today');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = getWeek('endweek');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = getDate('monthend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = getDate('quarterend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = getDate('yearend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = getLastWeek('endweek');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = lastmonth('endmonth');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = lastyear('endyear');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = quarteryear('endquaeter');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = quarteryear('startquaeter');
            }
        }
        function SetInitialRevalDate() {
            getWeek();
            var selValue = document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlInitialRevalDate').value;
            if (selValue == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateToDate').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateFromDate').value = "";

            }
            else if (selValue == 1) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateToDate').value = getDate('today');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateFromDate').value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateToDate').value = getWeek('endweek');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateFromDate').value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateToDate').value = getDate('monthend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateFromDate').value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateToDate').value = getDate('quarterend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateFromDate').value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateToDate').value = getDate('yearend');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateFromDate').value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateToDate').value = getLastWeek('endweek');
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateFromDate').value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateToDate').value = lastmonth('endmonth');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateFromDate').value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateToDate').value = lastyear('endyear');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateFromDate').value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateToDate').value = quarteryear('endquaeter');

                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtupdateFromDate').value = quarteryear('startquaeter');
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

            document.getElementById('ASPxPageControl1_txtToDateRange').value = '';
            document.getElementById('ASPxPageControl1_txtFromDateRange').value = '';
            document.getElementById('ASPxPageControl1_ddlNoShowDate').value = 0;
            document.getElementById('ASPxPageControl1_extddlNoShowspeciality').value = "NA";
            document.getElementById('ASPxPageControl1_extddlNoShowProvider').value = "NA";
            document.getElementById('ASPxPageControl1_extddlNoShowDoctor').value = "NA";


        }


        //for Missing Visits
        function MissingVisitClear() {

            document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitToDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_txtMissingVisitFromDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlMissingVisit').value = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlShowPatientVisit').value = 0;
            
        }

        function InitialReevalClear() {
            document.getElementById('ASPxPageControl1_txtupdateToDate').value = '';
            document.getElementById('ASPxPageControl1_txtupdateFromDate').value = '';
            document.getElementById('ASPxPageControl1_ddlInitialRevalDate').value = 0;
            document.getElementById('ASPxPageControl1_txtINS1').value = '';
            document.getElementById('ASPxPageControl1_txtOffice1').value = '';
            document.getElementById('ASPxPageControl1_extddlCaseType').value = "NA";
            document.getElementById('ASPxPageControl1_extddlCaseStatus').value = "NA";
            var f = document.getElementById('ASPxPageControl1_grdInitialRevalSpeciality');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = false;
                    }
                }
            }
        }

       
        function Vlidate() {

            var d = new Date();
            var n = d.getDate();
            var y = d.getFullYear();
            var m = d.getMonth() + 1;
            var fdate = document.getElementById('ASPxPageControl1_txtupdateFromDate').value;
            var ldate = document.getElementById('ASPxPageControl1_txtupdateToDate').value;

            if (document.getElementById("ctl00_ContentPlaceHolder1_rblInitialReeval_1").checked == true) {
                if (ldate != "") {
                    if (!getDateDiff(ldate, m + '/' + n + '/' + y)) {
                        return false;
                    }
                }
            }
            var f = document.getElementById('ASPxPageControl1_grdInitialRevalSpeciality');
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkall1') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;


                        }

                    }
                }
            }

            if (bfFlag == false) {
                alert('Please select record.');
                return false;
            }
            else {
                return true;
            }
        }

        
       

    </script>

    <script type="text/javascript" src="http://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script language="javascript">
        $(document).ready(function () {
            debugger;
            
            /*Code to copy the gridview header with style*/
            var gridHeader = $('ASPxPageControl1_grdNoShow').clone(true);
            /*Code to remove first ror which is header row*/
            $(gridHeader).find("tr:gt(0)").remove();
            $('ASPxPageControl1_grdNoShow tr th').each(function (i) {
                /* Here Set Width of each th from gridview to new table th */
                $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
            });
            $("#controlHead").append(gridHeader);
            $('#controlHead').css('position', 'absolute');
            $('#controlHead').css('top', $('ASPxPageControl1_grdNoShow').offset().top);

        });
    </script>

    <script language="javascript" type="text/javascript">

        function ShowVisits() {
            debugger;
            
            if (document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlShowPatientVisit').value == 1 || document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlShowPatientVisit').value == 0) {
                 document.getElementById("<%=divlblSpeciality.ClientID%>").style.visibility='hidden';
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_divShowLastDayVisit').style.visibility = 'hidden';
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_divgrd').style.visibility = 'hidden';
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlShowPatientVisit').value == 2 ) {
                 document.getElementById("<%=divlblSpeciality.ClientID%>").style.visibility='visible';
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_divgrd').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_divShowLastDayVisit').style.visibility = 'hidden';
                document.getElementById("ctl00_ContentPlaceHolder1_ASPxPageControl1_extddlMissingVisitsSpeciality").value = 'NA';
               
            }
           
            if (document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_ddlShowPatientVisit').value == 3) {
                document.getElementById("<%=divlblSpeciality.ClientID%>").style.visibility='visible';
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_divShowLastDayVisit').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_ASPxPageControl1_divgrd').style.visibility = 'visible';
                document.getElementById("ctl00_ContentPlaceHolder1_ASPxPageControl1_extddlMissingVisitsSpeciality").value = 'NA';
                document.getElementById("<%=txtdays.ClientID%>").value = '';
                
            }
            
            return false;
        }

     
       

    </script>


    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td>
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="1" EnableHierarchyRecreation="True"
                    Width="100%" Height="240">
                    <TabPages>
                        <dx:TabPage Text="Visit Summary" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                            Name="case" TabStyle-BackColor="#B1BEE0">
                            <ActiveTabStyle BackColor="White" Font-Bold="True"></ActiveTabStyle>

                            <TabStyle Width="100%" BackColor="#B1BEE0"></TabStyle>
                            <ContentCollection>
                                <dx:ContentControl>
                                    <table border="0" style="margin-left: 10px; margin-bottom: 5px; margin-top: 5px;">
                                        <tr>
                                            <td>
                                     <table style="border: 1px solid #d3d3d3;" width="650px" border="0">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#d3d3d3" colspan="5">

                                                            <b>Search Parameters</b>
                                                        </td>
                                                    </tr>
                                        <tr>
                                            <td class="td-widget-bc-search-desc-ch1" style="width: 20%;" align="center">Visit Date
                                            </td>
                                            <td class="td-widget-bc-search-desc-ch1" style="width: 20%;" align="center">From Date
                                            </td>
                                            <td class="td-widget-bc-search-desc-ch1" style="width: 20%;" align="center">To Date
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                <asp:DropDownList ID="ddlDateValues" runat="Server" Width="90%">
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
                                                            <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                                            <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="imgbtnDateofAccident"
                                                                TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
                                                            <asp:Label ID="Label3" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>

                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch3">
                                                            <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                                            <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" PopupButtonID="imgbtnDateofBirth"
                                                                TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender>
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>

                                                        </td>
                                           
                                        </tr>
                                        
                                        <tr>
                                             <td class="td-widget-bc-search-desc-ch" style="width: 20%;" align="center">Doctor
                                            </td>
                                            <td class="td-widget-bc-search-desc-ch1" style="width: 20%;" align="center">Specialty
                                            </td>
                                        </tr>
                                        <tr>
                                             <td class="td-widget-bc-search-desc-ch3" align="left" valign="top">
                                                <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="200px" Connection_Key="Connection_String"
                                                    Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                            </td>
                                            <td class="td-widget-bc-search-desc-ch3" valign="top" align="left">
                                                <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                    Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                    Selected_Text="---Select---" Width="200px"></cc1:ExtendedDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center">
                                                <dx:ASPxButton ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_OnClick">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    <table width="100%" runat="server">
                                        <tr>
                                            <td style="vertical-align: top">
                                                <dx:ASPxPageControl ID="carTabPage" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
                                                    Width="100%" Height="250" OnActiveTabChanged="carTabPage_ActiveTabChanged" AutoPostBack="true">
                                                    <TabPages>
                                                        <dx:TabPage Text="By Doctor" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%"
                                                            ActiveTabStyle-BackColor="White" Name="case" TabStyle-BackColor="#B1BEE0">
                                                            <ActiveTabStyle BackColor="White" Font-Bold="True"></ActiveTabStyle>

                                                            <TabStyle Width="100%" BackColor="#B1BEE0"></TabStyle>
                                                            <ContentCollection>
                                                                <dx:ContentControl>
                                                                    <asp:Panel ID="pnl_CaseDetails1" runat="server" Width="100%">
                                                                        <table style="width: 100%" class="ContentTable" border="0">
                                                                            <tr>
                                                                                <td align="right" colspan="2">
                                                                                    <asp:LinkButton ID="btnXlsExport1" OnClick="btnXlsExport1_Click" runat="server" Text="Export TO Excel">
                                                                                <img 
                                                                                src="../Images/Excel.jpg"
                                                                                alt="" 
                                                                                style="border:none;" 
                                                                                height="15px" 
                                                                                width ="15px" 
                                                                                title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="width: 100%">
                                                                                    <dx:ASPxGridView ID="grdDoctorVisits" runat="server" KeyFieldName="VISIT_COUNT" AutoGenerateColumns="false"
                                                                                        Width="100%" SettingsPager-PageSize="50" SettingsCustomizationWindow-Height="200"
                                                                                        Settings-VerticalScrollableHeight="360">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataColumn FieldName="SZ_DOCTOR_NAME" Caption="Doctor" HeaderStyle-HorizontalAlign="Center"
                                                                                                Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="123px">
                                                                                                <Settings AllowAutoFilter="False" AllowSort="False"></Settings>

                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            </dx:GridViewDataColumn>
                                                                                            <dx:GridViewDataColumn FieldName="VISIT_COUNT" Caption="Visit Count" HeaderStyle-HorizontalAlign="Center"
                                                                                                Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="123px">
                                                                                                <Settings AllowAutoFilter="False" AllowSort="False"></Settings>

                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            </dx:GridViewDataColumn>
                                                                                        </Columns>
                                                                                        <Settings ShowVerticalScrollBar="true" ShowFilterRow="false" ShowGroupPanel="false" />
                                                                                        <SettingsBehavior AllowFocusedRow="false" />
                                                                                        <SettingsBehavior AllowSelectByRowClick="true" />
                                                                                        <SettingsPager Position="Bottom" />

                                                                                        <SettingsCustomizationWindow Height="200px"></SettingsCustomizationWindow>
                                                                                    </dx:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Text="By Speciality" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%"
                                                            ActiveTabStyle-BackColor="White" Name="case" TabStyle-BackColor="#B1BEE0">
                                                            <ActiveTabStyle BackColor="White" Font-Bold="True"></ActiveTabStyle>

                                                            <TabStyle Width="100%" BackColor="#B1BEE0"></TabStyle>
                                                            <ContentCollection>
                                                                <dx:ContentControl>
                                                                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                                                                        <table style="width: 100%" class="ContentTable" border="0">
                                                                            <tr>
                                                                                <td align="right" colspan="2">
                                                                                    <asp:LinkButton ID="LinkButton1" OnClick="btnXlsExportSpec_Click" runat="server" Text="Export TO Excel">
                                                                                <img 
                                                                                src="../Images/Excel.jpg"
                                                                                alt="" 
                                                                                style="border:none;" 
                                                                                height="15px" 
                                                                                width ="15px" 
                                                                                title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="width: 100%">
                                                                                    <dx:ASPxGridView ID="grdSpecVisits" runat="server" KeyFieldName="VISIT_COUNT" AutoGenerateColumns="false"
                                                                                        Width="100%" SettingsPager-PageSize="50" SettingsCustomizationWindow-Height="200"
                                                                                        Settings-VerticalScrollableHeight="360">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP" Caption="Specialty" HeaderStyle-HorizontalAlign="Center"
                                                                                                Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="123px">
                                                                                                <Settings AllowAutoFilter="False" AllowSort="False"></Settings>

                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            </dx:GridViewDataColumn>
                                                                                            <dx:GridViewDataColumn FieldName="VISIT_COUNT" Caption="Visit Count" HeaderStyle-HorizontalAlign="Center"
                                                                                                Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="123px">
                                                                                                <Settings AllowAutoFilter="False" AllowSort="False"></Settings>

                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            </dx:GridViewDataColumn>
                                                                                        </Columns>
                                                                                        <Settings ShowVerticalScrollBar="true" ShowFilterRow="false" ShowGroupPanel="false" />
                                                                                        <SettingsBehavior AllowFocusedRow="false" />
                                                                                        <SettingsBehavior AllowSelectByRowClick="true" />
                                                                                        <SettingsPager Position="Bottom" />

                                                                                        <SettingsCustomizationWindow Height="200px"></SettingsCustomizationWindow>
                                                                                    </dx:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                    </TabPages>
                                                </dx:ASPxPageControl>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                    </TabPages>

                    <TabPages>
                        <dx:TabPage Text="Visit Details" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                            Name="case" TabStyle-BackColor="#B1BEE0">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                        <table border="0" style="margin-left: 10px; margin-bottom: 5px; margin-top: 5px;">
                                            <tr>
                                                <td>
                                                    <table style="border: 1px solid #d3d3d3;" width="650px" border="0">
                                                        <tr>
                                                            <td height="28" align="left" valign="middle" bgcolor="#d3d3d3" colspan="3">

                                                                <b>Search Parameters</b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-bc-search-desc-ch1" align="center" style="font: bold">Visit Date
                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch1" align="center" style="font: bold">From Date
                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch1" align="center" style="font: bold">To Date
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-bc-search-desc-ch3">
                                                                <asp:DropDownList ID="ddlNoShowDate" runat="Server" Width="90%">
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
                                                                <asp:TextBox ID="txtFromDateRange" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnFromVisitDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgbtnFromVisitDate" PopupPosition="Right"
                                                                    TargetControlID="txtFromDateRange"></ajaxToolkit:CalendarExtender>
                                                                <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>

                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch3">
                                                                <asp:TextBox ID="txtToDateRange" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnToVisitDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="imgbtnToVisitDate"
                                                                    TargetControlID="txtToDateRange"></ajaxToolkit:CalendarExtender>
                                                                <asp:Label ID="lblValid1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>

                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td class="td-widget-bc-search-desc-ch" align="center" style="font: bold">Provider
                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch" align="center" style="font: bold">Doctor
                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch" align="center" style="font: bold">Carrier
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td align="left">
                                                                <extddl:ExtendedDropDownList ID="extddlNoShowProvider" runat="server" Width="95%" Connection_Key="Connection_String"
                                                                    Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_MST_OFFICE"></extddl:ExtendedDropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <extddl:ExtendedDropDownList ID="extddlNoShowDoctor" runat="server" Width="92%" Connection_Key="Connection_String"
                                                                    Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR"></extddl:ExtendedDropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="92%" Selected_Text="---Select---"
                                                                    Connection_Key="Connection_String" Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY"></extddl:ExtendedDropDownList>

                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td class="td-widget-bc-search-desc-ch" align="center" style="font: bold">Case Type
                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch" align="center" style="font: bold">Case Status
                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch" align="center" style="font: bold"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <extddl:ExtendedDropDownList ID="extVisitDetailCaseType" runat="server" Width="95%" Selected_Text="---Select---"
                                                                    Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <extddl:ExtendedDropDownList ID="extVistDetailCaseStatus" runat="server" Width="92%" Selected_Text="OPEN"
                                                                    Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Connection_Key="Connection_String"
                                                                    ></extddl:ExtendedDropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-bc-search-desc-ch" width="33%" colspan="3" style="padding-top: 10px">
                                                                <asp:RadioButtonList ID="rblNoshow" runat="server" RepeatDirection="Horizontal"
                                                                    Width="90%">
                                                                    <asp:ListItem Text="Scheduled" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Re-Scheduled" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="No-show" Value="3"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td align="center" height="40" colspan="3">


                                                                <asp:Button ID="BtnSearchVisit" runat="server" Text="Search" Width="80px" OnClick="BtnSearchVisit_Click" />
                                                                <input style="width: 80px" id="btnClear1" onclick="Clear();" type="button" value="Clear"
                                                                    runat="server" />

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td>
                                               
                                                <td valign="bottom" style="width:650px" align="right">
                                                    
                                                <fieldset style="width: 400px; text-align: left; background-color: #D8D8D8">
                                                    <legend style="color:red"><b>Legend :</b></legend>
                                                    <b>NA indicates that the specialty is not applicable for the patient.</b>

                                                </fieldset>
                                                       
                                            </td>
                                            </tr>

                                        </table>




                                    </div>
                                   <%-- <table style="width: 100%">
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td style="width: 300px; text-align: left; background-color: #D8D8D8">
                                                <fieldset>
                                                    <legend style="color:red"><b>Legend :</b></legend>
                                                    <b>NA indicates that the specialty is not applicable for the patient.</b>

                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>--%>
                                    <table style="width: 100%">

                                        <tr>
                                            <td style="width: 100%; text-align: right;">
                                                <dx:ASPxHyperLink Text="[Excel]" runat="server" ID="xExcel">
                                                    <ClientSideEvents Click="Export" />
                                                </dx:ASPxHyperLink>
                                                <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="Callback1"
                                                    OnCallback="ASPxCallback1_Callback">
                                                    <ClientSideEvents CallbackComplete="OnCallbackComplete" />
                                                </dx:ASPxCallback>
                                                <dx:ASPxLoadingPanel Text="Exporting..." runat="server" ID="expLoadingPanel" ClientInstanceName="expLoadingPanel">
                                                </dx:ASPxLoadingPanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">


                                                <div style="height: 400px; overflow-y: scroll; overflow-y: scroll">
                                                    <asp:DataGrid ID="grdNoShow" runat="server" Width="100%" hight="200px" CellPadding="4" BorderColor="#A0A0A0" BackColor="White" BorderStyle="None" BorderWidth="1px">

                                                        <FooterStyle BackColor="#8D8D8D" ForeColor="White"></FooterStyle>

                                                        <HeaderStyle BackColor="#d3d3d3" Font-Bold="True" ForeColor="Black"></HeaderStyle>

                                                        <ItemStyle BackColor="White" ForeColor="Black"></ItemStyle>

                                                        <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="Black"></PagerStyle>

                                                        <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedItemStyle>
                                                        <FooterStyle />
                                                        <SelectedItemStyle />
                                                        <PagerStyle />
                                                        <AlternatingItemStyle />
                                                        <ItemStyle />
                                                        <Columns>
                                                        </Columns>
                                                        <HeaderStyle />
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>

                    <TabPages>
                        <dx:TabPage Text="Missing Visits" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                            Name="case" TabStyle-BackColor="#B1BEE0">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <table border="0" style="margin-left: 10px; margin-bottom: 5px; margin-top: 5px;">
                                        <tr>
                                            <td>

                                                <table style="border: 1px solid #d3d3d3;" width="650px" border="0">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#d3d3d3" colspan="3">

                                                            <b>Search Parameters</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch1" align="center" style="font: bold">Visit Date
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch1" align="center" style="font: bold">From Date
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch1" align="center" style="font: bold">To Date
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch3">
                                                            <asp:DropDownList ID="ddlMissingVisit" runat="Server" Width="90%">
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
                                                            <asp:TextBox ID="txtMissingVisitFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                                            <asp:ImageButton ID="imgMissingVisitFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="imgMissingVisitFromDate"
                                                                TargetControlID="txtMissingVisitFromDate"></ajaxToolkit:CalendarExtender>
                                                            <asp:Label ID="lblValidator3" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>

                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch3">
                                                            <asp:TextBox ID="txtMissingVisitToDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                                            <asp:ImageButton ID="imgMissingVisitToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="imgMissingVisitToDate"
                                                                TargetControlID="txtMissingVisitToDate"></ajaxToolkit:CalendarExtender>
                                                            <asp:Label ID="lblValid4" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td class="td-widget-bc-search-desc-ch" align="center" style="font: bold">Show
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch" align="center" style="font: bold">
                                                            <div id="divlblSpeciality" runat="server" style="visibility:hidden">
                                                            <asp:Label ID="lblMissingSpeciality" runat="server" Text="Specialty" Font-Bold="true" Font-Size="13px" ></asp:Label>
                                                        </div></td>
                                                        <td class="td-widget-bc-search-desc-ch" align="center" style="font: bold"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlShowPatientVisit" runat="Server" Width="90%" onchange="return ShowVisits();">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Patient with no visits</asp:ListItem>
                                                                <asp:ListItem Value="2">Patient with no visits for</asp:ListItem>
                                                                <asp:ListItem Value="3">Days since last Visit</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                       <td align="left">
                                                            <div id="divgrd" runat="server" style="visibility: hidden" >
                                                                <cc1:ExtendedDropDownList ID="extddlMissingVisitsSpeciality" runat="server" Connection_Key="Connection_String" 
                                                                    Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                    Selected_Text="---Select---" Width="95%" StausText="False" ></cc1:ExtendedDropDownList>
                                                            </div>
                                                            </td>
                                                        <td class="td-widget-bc-search-desc-ch1" align="left" style="font: bold">
                                                            <div id="divShowLastDayVisit" runat="server" style="visibility:hidden" enableviewstate="true">
                                                                &nbsp&nbsp<b>For</b>
                                                            <asp:TextBox ID="txtdays" runat="server" TextMode="Number" Width="60px">
                                                                      
                                                            </asp:TextBox>
                                                            &nbsp&nbsp<b>Days</b>
                                                                </div>
                                                        </td>
                                                    </tr>
                                                   <%-- <tr>

                                                        <td align="left">
                                                            <div id="divgrd" runat="server" style="visibility: hidden">
                                                                <cc1:ExtendedDropDownList ID="extddlMissingVisitsSpeciality" runat="server" Connection_Key="Connection_String" 
                                                                    Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                    Selected_Text="---Select---" Width="95%"></cc1:ExtendedDropDownList>
                                                            </div>
                                                            </td>

                                                   
                                                             <dx:ASPxGridView ID="grdSpeciality" runat="server" Width="100%" SettingsBehavior-AllowSort="false" 
                                                        SettingsPager-PageSize="20" ClientInstanceName="grdSpeciality" KeyFieldName="CODE">
                                                        <Columns>
                                                            <dx:GridViewDataColumn Caption="chk1" VisibleIndex="0" Width="30px">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAllSpeciality" runat="server" onclick="javascript:SelectAllSpeciality(this.checked);"
                                                                        ToolTip="Select All" />
                                                                </HeaderTemplate>
                                                                <DataItemTemplate>
                                                                    <asp:CheckBox ID="chkall1" Visible="true" runat="server" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="description" Caption="Specialty Name" VisibleIndex="1">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="code" Caption="Speciality Id" VisibleIndex="2" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                        <SettingsPager PageSize="1000">
                                                        </SettingsPager>
                                                    </dx:ASPxGridView>
                                                
                                                        

                                                   
                                                        <td class="td-widget-bc-search-desc-ch1" align="left" style="font: bold">
                                                            <div id="divShowLastDayVisit" runat="server" style="visibility:hidden">
                                                            <asp:TextBox ID="txtdays" runat="server" TextMode="Number" Width="60px">
                                                                      
                                                            </asp:TextBox>
                                                            &nbsp&nbsp<b>Days</b>
                                                                </div>
                                                        </td>
                                                    </tr>--%>

                                                    <tr>
                                                        <td align="center" height="40" colspan="3">

                                                            <asp:Button ID="btn_Unseensearch" runat="server" Text="Search" Width="80px" OnClick="btn_search_Click" OnClientClick="return ValidateSearch();" />
                                                            &nbsp;
                                                    <input style="width: 80px" id="btnUnseenClear" onclick="MissingVisitClear();" type="button" value="Clear" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">

                                        <tr>
                                            <td style="width: 100%" colspan="5">

                                                <table style="vertical-align: middle; width: 100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" Text="Search:"></asp:Label>
                                                                <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                    CssClass="search-input">
                                                                </gridsearch:XGridSearchTextBox>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 30%" align="left"></td>
                                                            <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">Record Count:<%= this.grdUnseenReport.RecordCount %>| Page Count:
                                                                       
                                     <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                     </gridpagination:XGridPaginationDropDown>

                                                                <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                    Text="Export TO Excel">
                         <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                            </td>

                                                        </tr>
                                                    </tbody>
                                                </table>

                                                <xgrid:XGridViewControl ID="grdUnseenReport" runat="server" Height="148px" Width="100%"
                                                    CssClass="mGrid" AllowSorting="true" BorderColor="black"
                                                    PagerStyle-CssClass="pgr" PageRowCount="50" DataKeyNames=""
                                                    XGridKey="UnseenReport" GridLines="None" AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                                    ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                    ShowExcelTableBorder="true" ExcelFileNamePrefix="UnseenReport" MouseOverColor="0, 153, 153"
                                                    AutoGenerateColumns="false" ExportToExcelColumnNames="Case #,Patient Name,Date Of Accident,Date of First Treatment,Phone Number,Specialty,Case Type,Carrier,Aging,Insurance"
                                                    ExportToExcelFields="SZ_CASE_NO,PatientName,DT_DATE_OF_ACCIDENT,DT_EVENT_DATE,SZ_PATIENT_PHONE,sz_procedure_group,SZ_CASE_TYPE_NAME,SZ_INSURANCE_NAME,Count,SZ_INSURANCE_NAME">
                                                    <FooterStyle BackColor="#8D8D8D" ForeColor="White"></FooterStyle>

                                                    <HeaderStyle BackColor="#d3d3d3" Font-Bold="True" ForeColor="Black"></HeaderStyle>




                                                    <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="Black"></PagerStyle>


                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>




                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Case #" DataField="SZ_CASE_NO" SortExpression=" convert(int,SZ_CASE_NO)" />

                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Patient Name" DataField="PatientName" SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME" />

                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Date Of Accident" DataField="DT_DATE_OF_ACCIDENT" />

                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Phone Number" DataField="SZ_PATIENT_PHONE" />

                                                         <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Cell Number" DataField="SZ_PATIENT_CELLNO" />

                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Specialty" DataField="sz_procedure_group" SortExpression="sz_procedure_group" />

                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Case Type" DataField="SZ_CASE_TYPE_NAME" SortExpression="SZ_CASE_TYPE_NAME" />

                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Carrier" DataField="SZ_INSURANCE_NAME" SortExpression="SZ_INSURANCE_NAME" />

                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Doctor" DataField="Doctor" />

                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Provider" DataField="provider" />

                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Last Visit Date" DataField="DT_EVENT_DATE" />

                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </td>
                                        </tr>



                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>



                    <TabPages>
                        <dx:TabPage Text="Initial Re-eval" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                            Name="case" TabStyle-BackColor="#B1BEE0">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <table id="First" border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: White;">
                                        <tr>
                                            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; padding-top: 3px; vertical-align: top; width: 1500px;">
                                                <table cellpadding="0" cellspacing="0" border="0" style="background-color: White; width: 800px;">
                                                    <tr>
                                                        <td></td>
                                                        <td valign="top">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="background-color: White; width: 100%;">
                                                                <tr>
                                                                    <td>

                                                                        <UserMessage:MessageControl runat="server" ID="usrMessage2"></UserMessage:MessageControl>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100%" valign="top">
                                                                        <table width="100%" border="0">
                                                                            <tr>
                                                                                <td style="text-align: left; width: 100%;">
                                                                                    <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 550px; height: 50%; border: 1px solid #d3d3d3;">
                                                                                        <tr>
                                                                                            <td height="28" align="left" valign="middle" bgcolor="#d3d3d3" style="width: 200px">
                                                                                                <b class="txt3">Search Parameters</b>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 20%; height: 0px;" valign="top">
                                                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 550px;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                                    <tr>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center">
                                                                                                            <asp:Label ID="lblDate" runat="server" Text="Re-eval" Font-Bold="true" Font-Size="12px"> </asp:Label>
                                                                                                        </td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center"><b>From Date</b>
                                                                                                        </td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center"><b>To Date</b>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                                            <asp:DropDownList ID="ddlInitialRevalDate" runat="Server" Height="18px" Width="100%">
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
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                                            <asp:TextBox ID="txtupdateFromDate" runat="server" Width="80%" onkeypress="return CheckForInteger(event,'/')"
                                                                                                                Height="15px"></asp:TextBox>
                                                                                                            &nbsp;<asp:ImageButton ID="imgbtnFromDate" Width="10%" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtnFromDate"
                                                                                                                TargetControlID="txtupdatefromdate"></ajaxToolkit:CalendarExtender>
                                                                                                        </td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                                            <asp:TextBox ID="txtupdateToDate" runat="server" Width="80%" onkeypress="return CheckForInteger(event,'/')"
                                                                                                                Height="15px"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgbtnToDate" Width="10%" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="imgbtnToDate"
                                                                                                                TargetControlID="txtupdateToDate"></ajaxToolkit:CalendarExtender>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center"><b>Insurance Company</b>
                                                                                                        </td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center"><b>Provider</b>
                                                                                                        </td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center"><b>Case Type</b>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                                            <%--  <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="15%" Selected_Text="---Select---"
                                                                                        Connection_Key="Connection_String" Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY">
                                                                                    </extddl:ExtendedDropDownList>--%>
                                                                                                            <asp:TextBox ID="txtINS1" runat="server" Width="98%" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                                                                            <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                                                                                                                DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtINS1"
                                                                                                                ServiceMethod="GetInsurance" OnClientItemSelected="GetInsuranceValue" ServicePath="PatientService.asmx"
                                                                                                                UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                                                                                                            </ajaxToolkit:AutoCompleteExtender>
                                                                                                        </td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                                            <%-- <cc1:ExtendedDropDownList ID="extddlOffice" runat="server" Connection_Key="Connection_String"
                                                                                        Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_GET_OFFICE_LIST_FOR_SHCEDULE_REPORT"
                                                                                        Selected_Text="--- Select ---" Width="15%"></cc1:ExtendedDropDownList>--%>
                                                                                                            <asp:TextBox ID="txtOffice1" runat="server" Text="" Width="98%" autocomplete="off"
                                                                                                                CssClass="search-input"></asp:TextBox>
                                                                                                            <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoOffice" EnableCaching="true"
                                                                                                                OnClientItemSelected="GetOfficeValue" DelimiterCharacters="" MinimumPrefixLength="1"
                                                                                                                CompletionInterval="500" TargetControlID="txtOffice1" ServiceMethod="GetProvider"
                                                                                                                ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                                                                                                            </ajaxToolkit:AutoCompleteExtender>
                                                                                                        </td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                                            <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="98%" Selected_Text="---Select---"
                                                                                                                Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center">
                                                                                                            <b>Specialty</b>
                                                                                                        </td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center"><b>Case Status</b></td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center"></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                                            <%--  <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                                        Selected_Text="---Select---" Width="100%"></cc1:ExtendedDropDownList>--%>
                                                                                                            <div style="height: 160px; background-color: Gray; overflow: scroll;">
                                                                                                                <dx:ASPxGridView ID="grdInitialRevalSpeciality" runat="server" Width="100%" SettingsBehavior-AllowSort="false"
                                                                                                                    SettingsPager-PageSize="400" ClientInstanceName="grdInitialRevalSpeciality" KeyFieldName="CODE">
                                                                                                                    <Columns>
                                                                                                                        <dx:GridViewDataColumn Caption="chk1" VisibleIndex="0" Width="30px">
                                                                                                                            <HeaderTemplate>
                                                                                                                                <asp:CheckBox ID="chkSelectAllSpeciality" runat="server" onclick="javascript:SelectAllSpeciality(this.checked);"
                                                                                                                                    ToolTip="Select All" />
                                                                                                                            </HeaderTemplate>
                                                                                                                            <DataItemTemplate>
                                                                                                                                <asp:CheckBox ID="chkall1" Visible="true" runat="server" />
                                                                                                                            </DataItemTemplate>
                                                                                                                        </dx:GridViewDataColumn>
                                                                                                                        <dx:GridViewDataColumn FieldName="description" Caption="Speciality Name" VisibleIndex="1">
                                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                                        </dx:GridViewDataColumn>
                                                                                                                        <dx:GridViewDataColumn FieldName="code" Caption="Speciality Id" VisibleIndex="2" Visible="false">
                                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                                        </dx:GridViewDataColumn>
                                                                                                                    </Columns>
                                                                                                                    <SettingsPager PageSize="1000">
                                                                                                                    </SettingsPager>
                                                                                                                </dx:ASPxGridView>
                                                                                                            </div>

                                                                                                        </td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" valign="top">
                                                                                                            <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="100%" Selected_Text="OPEN"
                                                                                                                Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Connection_Key="Connection_String"
                                                                                                                CssClass="search-input"></extddl:ExtendedDropDownList>
                                                                                                        </td>
                                                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" valign="top">
                                                                                                            <asp:RadioButtonList ID="rblInitialReeval" runat="server" RepeatDirection="Horizontal" onchange="return selectValue();"
                                                                                                                Width="90%">
                                                                                                                <asp:ListItem Text="Initial" Value="1"></asp:ListItem>
                                                                                                                <asp:ListItem Text="Re-Eval" Value="2" Selected="True"></asp:ListItem>
                                                                                                            </asp:RadioButtonList>
                                                                                                        </td>
                                                                                                        <%-- <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                     <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="90%" Selected_Text="---Select---"
                                                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String">
                                                                                        </extddl:ExtendedDropDownList>
                                                                                  </td>--%>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3">&nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" align="center">

                                                                                                            <%--<asp:UpdateProgress ID="UpdateProgress123" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                                                                                            DisplayAfter="10">
                                                                                                                            <ProgressTemplate>
                                                                                                                                <div id="DivStatus12" style="vertical-align: bottom; position: absolute; top: 350px; left: 600px"
                                                                                                                                    runat="Server">
                                                                                                                                    <asp:Image ID="img50" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                                                        Height="25px" Width="24px"></asp:Image>
                                                                                                                                    Loading...
                                                                                                                                </div>
                                                                                                                            </ProgressTemplate>
                                                                                                                        </asp:UpdateProgress>--%>
                                                                                                            <asp:Button Style="width: 80px" ID="btnInitialRevalSearch" OnClick="btnInitialReevalSearch_Click" runat="server"
                                                                                                                Text="Search"></asp:Button>
                                                                                                            &nbsp;
                                                                                                                  <input style="width: 80px" id="btn_Clear" onclick="InitialReevalClear();" type="button" value="Clear" />

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
                                                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtSort" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                <asp:HiddenField ID="hdnInsurace" runat="server" />
                                                <asp:HiddenField ID="hdnOffice" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="First1" border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: White;">
                                        <tr>
                                            <td align="right">
                                                <asp:Label runat="server" ID="lblCount" Text="Count:" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                <asp:Label runat="server" ID="lblCnt" Text="" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                <asp:ImageButton ID="btnInitialReevalexport" runat="server" Height="15px" Width="15px" OnClick="btnInitialReevalexport_Click"
                                                    ImageUrl="~/Images/Excel.jpg" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" bgcolor="#d3d3d3" height="28" style="width: 100%" valign="middle">
                                                <b class="txt3"></b>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Text="Visits"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">&nbsp;
                                                   <%-- <div style="width: 100%">
                                                        <table style="height: 9px; background-color: #d3d3d3" width="99%">
                                                            <tr>
                                                                <td style="width: 9px; font-size: 13px" align="left"><b>Case#</b></td>
                                                                <td style="width: 40px; font-size: 13px" align="center"><b>Name</b></td>
                                                                <td style="width: 47px; font-size: 13px" align="center"><b>Phone</b></td>
                                                                <td style="width: 40px; font-size: 13px" align="center"><b>Work Phone</b></td>
                                                                <td style="width: 40px; font-size: 13px" align="center"><b>Cell No</b></td>
                                                                <td style="width: 10px; font-size: 13px" align="center"><b>DOA</b></td>
                                                                <td style="width: 93px; font-size: 13px" align="center"><b>Insurance</b></td>
                                                                <td style="width: 12px; font-size: 13px" align="center"><b>Case Type</b></td>
                                                                <td style="width: 6px; font-size: 13px" align="center"><b>Specialty</b></td>
                                                                <td style="width: 5px; font-size: 13px" align="center"><b>Doctor</b></td>
                                                                <td style="width: 83px; font-size: 13px" align="center"><b>Provider</b></td>
                                                                <td style="width: 10px; font-size: 13px" align="center"><b>Last Initial/Re-Eval Date</b></td>
                                                            </tr>
                                                        </table>
                                                    </div>--%>
                                                
                                                    <asp:DataGrid ID="grdVisits" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnItemCommand="grdVisits_ItemCommand" Width="100%">

                                                        <AlternatingItemStyle BackColor="#EEEEEE"></AlternatingItemStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="CASE#" Visible="true" ItemStyle-Width="50px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnlCasetNo" runat="server" CommandName="case" CommandArgument="SZ_CASE_NO"
                                                                        Font-Bold="true" Font-Size="12px">CASE#</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.CASE#")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Name" Visible="true" ItemStyle-Width="140px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnlName" runat="server" CommandName="Name" CommandArgument="SZ_PATIENT_FIRST_NAME"
                                                                        Font-Bold="true" Font-Size="12px">Name</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Phone" Visible="true" ItemStyle-Width="90px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnPhone" runat="server" CommandName="Phone" CommandArgument="SZ_PATIENT_PHONE"
                                                                        Font-Bold="true" Font-Size="12px">Phone</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_PHONE")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Work Phone" Visible="true" ItemStyle-Width="90px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnPhone" runat="server" CommandName="WPhone" CommandArgument="SZ_WORK_PHONE"
                                                                        Font-Bold="true" Font-Size="12px" Width="100px">Work Phone</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_WORK_PHONE")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Cell No" Visible="true" ItemStyle-Width="90px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnPhone" runat="server" CommandName="CPhone" CommandArgument="SZ_PATIENT_CELLNO"
                                                                        Font-Bold="true" Font-Size="12px">Cell No</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_CELLNO")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="DOA" Visible="true" ItemStyle-Width="70px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnDOA" runat="server" CommandName="DOA" CommandArgument="DT_DATE_OF_ACCIDENT"
                                                                        Font-Bold="true" Font-Size="12px">DOA</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.DT_DATE_OF_ACCIDENT")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Insurance" Visible="true" ItemStyle-Width="200px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnInsurance" runat="server" CommandName="Insurance" CommandArgument="SZ_INSURANCE_NAME"
                                                                        Font-Bold="true" Font-Size="12px">Insurance</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_INSURANCE_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Case Type" Visible="true" ItemStyle-Width="80px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnCaseType" runat="server" CommandName="CaseType" CommandArgument="SZ_CASE_TYPE_NAME"
                                                                        Font-Bold="true" Font-Size="12px">Case Type</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_CASE_TYPE_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Specialty" Visible="true" ItemStyle-Width="80px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnSpeciality" runat="server" CommandName="Speciality" CommandArgument="SPECIALTY"
                                                                        Font-Bold="true" Font-Size="12px">Specialty</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SPECIALTY")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Doctor" Visible="true" ItemStyle-Width="150px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnDoctor" runat="server" CommandName="Doctor" CommandArgument="SZ_DOCTOR_NAME"
                                                                        Font-Bold="true" Font-Size="12px">Doctor</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_DOCTOR_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Provider" Visible="true" ItemStyle-Width="170px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnProvider" runat="server" CommandName="Provider" CommandArgument="SZ_OFFICE"
                                                                        Font-Bold="true" Font-Size="12px">Provider</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.OFFICE")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Last Initial/Re-Eval Date" Visible="true" ItemStyle-Width="120px">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnlstdate" runat="server" CommandName="LastDate" CommandArgument="DT_EVENT_DATE"
                                                                        Font-Bold="true" Font-Size="12px">Last Initial/Re-Eval Date</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.LAST VISIT DATE")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#d3d3d3" Font-Bold="true" />
                                                    </asp:DataGrid>




                                          
                                                <div style="height: 450px; overflow: scroll; visibility: hidden">
                                                    <asp:DataGrid Width="100%" ID="grdExPort" CssClass="mGrid" runat="server" AutoGenerateColumns="False">
                                                        <AlternatingItemStyle BackColor="#EEEEEE"></AlternatingItemStyle>
                                                        <Columns>
                                                            <asp:BoundColumn DataField="CASE#" HeaderText="Case no" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Phone" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_WORK_PHONE" HeaderText="Work Phone" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_PATIENT_CELLNO" HeaderText="Cell No." ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="DOA" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left" Visible="true">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SPECIALTY" HeaderText="Specialty" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="OFFICE" HeaderText="Provider" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="LAST VISIT DATE" HeaderText="Last Initial/Re-Eval Date"
                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#b5df82" Font-Bold="true" />
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>

                </dx:ASPxPageControl>


                <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtUnseenDate" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtCount" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtShowFlag" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtMissingSpeciality" runat="server" Visible="false"></asp:TextBox>
                <dx:ASPxGridViewExporter ID="grdExportDoctorallvisits" runat="server" GridViewID="grdDoctorVisits">
                </dx:ASPxGridViewExporter>
                <dx:ASPxGridViewExporter ID="grdExportSpecialtylvisits" runat="server" GridViewID="grdSpecVisits">
                </dx:ASPxGridViewExporter>
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                    Modal="True">
                </dx:ASPxLoadingPanel>
                <dx:ASPxPopupControl ID="PatientInfoPop" runat="server" CloseAction="CloseButton"
                    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ClientInstanceName="PatientInfoPop" HeaderText="Patient Information" HeaderStyle-HorizontalAlign="Left"
                    HeaderStyle-BackColor="#B1BEE0" AllowDragging="True" EnableAnimation="False"
                    EnableViewState="True" Width="800px" ToolTip="Patient Information" PopupHorizontalOffset="0"
                    PopupVerticalOffset="0" AutoUpdatePosition="true" ScrollBars="Auto"
                    RenderIFrameForPopupElements="Default" Height="500px">
                    <ContentStyle>
                        <Paddings PaddingBottom="5px" />
                    </ContentStyle>
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl
                    ID="IFrame_DownloadFiles"
                    runat="server" CloseAction="CloseButton"
                    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ClientInstanceName="IFrame_DownloadFiles"
                    HeaderText="Data Export"
                    HeaderStyle-HorizontalAlign="Left"
                    HeaderStyle-ForeColor="White"
                    HeaderStyle-BackColor="#000000"
                    AllowDragging="True"
                    EnableAnimation="False"
                    EnableViewState="True" Width="600px" ToolTip="Download File(s)" PopupHorizontalOffset="0"
                    PopupVerticalOffset="0" AutoUpdatePosition="true" ScrollBars="Auto"
                    RenderIFrameForPopupElements="Default" Height="200px">
                    <ContentStyle>
                        <Paddings PaddingBottom="5px" />
                    </ContentStyle>
                </dx:ASPxPopupControl>
            </td>



        </tr>
    </table>




</asp:Content>

