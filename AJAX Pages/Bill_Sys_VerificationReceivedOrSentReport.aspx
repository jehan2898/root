<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_VerificationReceivedOrSentReport.aspx.cs" Inherits="Bill_Sys_VerificationReceivedOrSentReport"
    Title="Green Your Bills - Verification Report" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    <script type="text/javascript">

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

        function ShowChildGrid(obj) {
            var div = document.getElementById(obj);
            div.style.display = 'block';
        }

        function ShowDenialChildGrid(obj) {
            var div1 = document.getElementById(obj);
            div1.style.display = 'block';
        }

        function HideChildGrid(obj) {
            var div = document.getElementById(obj);
            div.style.display = 'none';
        }

        function HideDenialChildGrid(obj) {
            var div1 = document.getElementById(obj);
            div1.style.display = 'none';
        }

        function CheckGrid() {
            var f = document.getElementById('<%=grdvDenial.ClientID %>');
            if (f == null) {
                bfFlag = true;
            }
            if (bfFlag == true) {
                alert('Data not available!');
                return false;
            }
            else {
                return true;
            }
        }
        function openURL(url) {
            if (url == "") {
                alert("There is no bill created for the selected record!");
            }
            else {
                var url1 = url;
                window.open(url1, "", "top=10,left=100,menubar=no,toolbar=no,location=no,width=750,height=600,status=no,scrollbars=no,maximize=null,resizable=1,titlebar=no;");
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
        function SetDate() {
            getWeek();
            var selValue = document.getElementById('<%=ddlDateValues.ClientID %>').value;
            if (selValue == 0) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = "";
                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = "";

            }
            else if (selValue == 1) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('today');
                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getWeek('endweek');
                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('monthend');
                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('quarterend');
                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('yearend');
                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = getLastWeek('endweek');
                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = lastmonth('endmonth');

                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = lastyear('endyear');

                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = quarteryear('endquaeter');

                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = quarteryear('startquaeter');
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

        function Validate() {
            var f = document.getElementById("<%= grdvDenial.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        document.getElementById("<%= grdvDenial.ClientID %>").value = 'true';
                        return true;
                    }
                }
            }
            document.getElementById("<%= hdlverisent.ClientID %>").value = 'false';
            alert('Please select bill no.');
            return false;
        }

        function SelectAll(ival) {
            var f = document.getElementById("<%= grdvDenial.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    f.getElementsByTagName("input").item(i).checked = ival;
                }
            }
        }
        //check verification recived date  greter than 30 or not
        function ChecklitgationDate() {
            var todaydate = getDate('today');
            var verisentflag = false;
            var verirec = false;
            //alert("today-"+todaydate);
            var f = document.getElementById("<%= grdvDenial.ClientID %>");
            var flag = "";
            var griddate = "";
            var billnumber = "";
            var statusname = "";
            document.getElementById("<%=hdlbillnumber.ClientID %>").value = "";
            // alert(f);
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {

                        document.getElementById("<%= grdvDenial.ClientID %>").value = 'true';
                        var str = i + 2;
                        // alert(str);
                        if (str < 10) {
                            //alert("delete");
                            var statusnameid1 = document.getElementById("ctl00_ContentPlaceHolder1_grdvDenial_ctl0" + str + "_lblStatus");
                            statusname = statusnameid1.innerHTML;
                            if (statusname.toLowerCase() != "verification sent") {
                                var spanbillno = document.getElementById("ctl00_ContentPlaceHolder1_grdvDenial_ctl0" + str + "_lblBill");
                                billnumber = spanbillno.innerHTML;
                                var spantext = document.getElementById("ctl00_ContentPlaceHolder1_grdvDenial_ctl0" + str + "_lbltest");
                                griddate = spantext.innerHTML;
                            } else {
                                verisentflag = true;

                            }


                        }
                        else {
                            //alert("lest 100");
                            var statusnameid2 = document.getElementById("ctl00_ContentPlaceHolder1_grdvDenial_ctl0" + str + "_lblStatus");
                            statusname = statusnameid2.innerHTML;
                            if (statusname.toLowerCase() != "verification sent") {
                                var spanbillno = document.getElementById("ctl00_ContentPlaceHolder1_grdvDenial_ctl0" + str + "_lblBill");
                                billnumber = spanbillno.innerHTML;
                                var spantext = document.getElementById("ctl00_ContentPlaceHolder1_grdvDenial_ctl0" + str + "_lbltest");
                                griddate = spantext.innerHTML;
                            } else {
                                verisentflag = true;
                            }
                            var spantext = document.getElementById("ctl00_ContentPlaceHolder1_grdvDenial_ctl" + str + "_lbltest");
                            var spanbillno = document.getElementById("ctl00_ContentPlaceHolder1_grdvDenial_ctl" + str + "_lblBill");
                            griddate = spantext.innerHTML;
                            billnumber = spanbillno.innerHTML;
                        }


                        var calculateday = DifferenceDate(griddate, todaydate);
                        if (calculateday < 31) {
                            var statusnameid = document.getElementById("ctl00_ContentPlaceHolder1_grdvDenial_ctl0" + str + "_lblStatus");
                            statusname = statusnameid.innerHTML;
                            //alert(statusname.toLowerCase());
                            if (statusname.toLowerCase() == "verification received") {
                                //alert("find");
                                document.getElementById("<%=hdlbillnumber.ClientID %>").value = document.getElementById("<%=hdlbillnumber.ClientID %>").value + billnumber + ",";
                                verirec = true;

                            }
                        }
                        flag = "get";
                    }

                }
            }
            if (flag != "") {
                //alert("enter");
                var bill = document.getElementById("<%=hdlbillnumber.ClientID %>").value;
                var alertflag = "";
                if (bill != "") {
                    alertflag = "test";
                    alert(bill + "You can not litigate bill with in 30 days of verification received");
                    document.getElementById("<%= hdlverisent.ClientID %>").value = 'false';
                    return false;

                }
                if (verisentflag && !verirec) {
                    if (confirm("Do you want to sent bill to litigation?")) {
                        document.getElementById("<%= hdlverisent.ClientID %>").value = 'true';
                        return true;
                    }
                    else {
                        document.getElementById("<%= hdlverisent.ClientID %>").value = 'false';
                        return false;
                    }
                }
                if (alertflag != "test") {
                    if (confirm("You have verification on this bill, Do you still want to sent bill to litigation?")) {
                        document.getElementById("<%= hdlverisent.ClientID %>").value = 'true';
                        return true;
                    }
                    else {
                        document.getElementById("<%= hdlverisent.ClientID %>").value = 'false';
                        return false;
                    }
                }

            }

            else {
                alert('Please select bill no.');
                document.getElementById("<%= hdlverisent.ClientID %>").value = 'false';
                return false;
            }
        }

        function DifferenceDate(firstdate, seconddate) {
            var err;
            var era1 = "A.D.";
            var era2 = "A.D.";
            var date1 = firstdate;
            var date2 = seconddate;
            err = checkdate(date1, era1);
            if (err == 1) return;
            err = checkdate(date2, era2);
            if (err == 1) return;
            firstdate = parseDate(date1, era1);
            seconddate = parseDate(date2, era2);
            dbd = seconddate.getJulianDay - firstdate.getJulianDay;
            return dbd;
            //alert(dbd.toString() + " days");
            //document.datainput.result.value=dbd.toString() + " days";
        }



        function checkdate(date, era) {

            var err = 0;
            var valid = "0123456789/";
            //var ok = "yes"
            var temp;
            if (date == null || date.length < 1) err = 1; //is there a date?
            //check for invalid characters
            for (var i = 0; i < date.length; i++) {
                temp = "" + date.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") err = 1;
            }

            //split is Javascript 1.2
            dary = date.split("/");
            b = dary[0]; //month
            d = dary[1]; //day
            f = dary[2]; //year

            if (err != 1 && f.length < 3) { //one or two digit date
                f = fix2DigitDate(f); //20th century
                if (era != null && era == 1) {
                    err = 1;  //BC years must be 4 digits
                    date = date + " B.C.";
                }
            }
            if (b < 1 || b > 12) err = 1;
            if (d < 1 || d > 31) err = 1;
            if (f < 0 || f > 9999) err = 1;
            if (b == 4 || b == 6 || b == 9 || b == 11) {
                if (d == 31) err = 1;
            }
            if (b == 2) {  //leap year checking
                var g = parseInt(f / 4);
                if (isNaN(g)) {
                    err = 1;
                }
                if (d > 29) {
                    err = 1;
                }
                if (d == 29) {
                    //leap years are always divisible by 4
                    if ((f / 4) != parseInt(f / 4)) { err = 1; }
                    //in the Gregorian calendar century years are not leap years unless divisible by 400
                    if (f > 1582) {
                        if (((f / 100) == parseInt(f / 100)) && (f / 400 != parseInt(f / 400))) err = 1;
                    }
                }
            }
            if (err == 1) {
                // alert('Is this date ('+date+') correct?');
                // document.datainput.dateerr.value="??"
            }
            else {
                //alert('Valid date!');
                //document.datainput.dateerr.value=""
            }

            return err;
        } //checkdate

        function parseDate(dateval, eraval) {
            //split is a Javascript 1.2 function

            var dary = dateval.split("/")
            var era;
            eraval > 0 ? era = -1 : era = 1;
            var y = fix2DigitDate(dary[2]) * era;
            //alert(y);
            switch (1) {
                case 1: { m = dary[0]; d = dary[1]; } // mm/dd/yyyy
                    //alert("call");
                    break;
                case 2: { m = dary[1]; d = dary[0]; } // dd/mm/yyyy
                    break;
                default: { m = dary[0]; d = dary[1]; }
            }

            var calendar;
            //alert(y);
            if (y > 1582) {
                calendar = "GREGORIAN";
                // alert(calendar);
            }
            else if (y < 1582) {
                calendar = "JULIAN";
            }
            else if (m < 10 | (m == 10 && d < 15)) {
                calendar = "JULIAN";
            }
            else {
                calendar = "GREGORIAN";
            }

            i = new CustomDate(y, m, d, calendar);
            //alert(i);
            return i
        } //dateval

        function fix2DigitDate(dateval) {

            var date = dateval + ""; //dateval must be a string
            if (date.length < 3) {
                date = 1900 + date * 1.0;
                date = date + ""; //to string
            }
            //alert(date);
            return date;
        }

        function CustomDate(yr, mo, da, type) {

            year = yr * 1.0;  //convert string to float

            if (year < -4713 || year > 3268) {
                alert("Year out of range");
                //document.datainput.dateerr.value="??"
                return;
            }
            month = mo * 1.0;
            day = da * 1.0;
            if (year == 1582 && month == 10 && day > 4 && day < 15) {
                alert("Invalid date: 15 Oct immediately followed 4 Oct in the year 1582")
                //	document.datainput.dateerr.value="??"
                return;
            }
            if (year < 0) year = year + 1; //B.C. date correction
            var a = ipart((14 - month) / 12);
            var y = year + 4800 - a;
            var m = month + 12 * a - 3;

            if (type == "GREGORIAN") {

                julianday = day + ipart((153 * m + 2) / 5) + y * 365 + ipart(y / 4) - ipart(y / 100) + ipart(y / 400) - 32045;

            }
            if (type == "JULIAN") {
                julianday = day + ipart((153 * m + 2) / 5) + y * 365 + ipart(y / 4) - 32083;
            }
            modifiedjulianday = julianday - 2400000.5; //Zero at 17 Nov 1858 00:00:00 UTC
            this.getJulianDay = getJulianDay();
            this.getModifiedJulianDay = getModifiedJulianDay();
        }

        function ipart(r) { return Math.round(r - 0.5); }
        function getJulianDay() { return this.julianday; }
        function getModifiedJulianDay() { return this.modifiedjulianday; }
        function chkrdo() {

            if (document.getElementById('ctl00_ContentPlaceHolder1_rblView_0').checked == false && document.getElementById('ctl00_ContentPlaceHolder1_rblView_1').checked == false && document.getElementById('ctl00_ContentPlaceHolder1_rblView_2').checked == false) {
                alert('Select atleast one verification type ');
                return false;

            }

        }
        function Validation() {

            if (document.getElementById('ctl00_ContentPlaceHolder1_rblView_0').checked || document.getElementById('ctl00_ContentPlaceHolder1_rblView_1').checked || document.getElementById('ctl00_ContentPlaceHolder1_rblView_2').checked) {

                return true;
            }

            alert('Select atleast one verification type ');
            return false;
        }
        function closeTypePage1() {
            $find('modal').hide();
            return false;
        }

        //Nirmalkumar
        function confirm_check() {
            var f = document.getElementById("<%= grdvDenial.ClientID %>");

            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('ChkLitigantion') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            return true;
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
        function YesMassage() {
            document.getElementById('div1').style.visibility = 'hidden';
        }
        function NoMassage() {
            document.getElementById('div1').style.visibility = 'hidden';
        }

        function POMConformation() {

            if (confirm_check()) {
                document.getElementById('div1').style.zIndex = 1;
                document.getElementById('div1').style.position = 'absolute';
                document.getElementById('div1').style.left = '360px';
                document.getElementById('div1').style.top = '250px';
                document.getElementById('div1').style.visibility = 'visible';
                return false;
            }
            else {
                return false;
            }



        }

        function showPopup2(billnumber, caseid, caseno) {
            var url = 'Bill_Sys_PayMent_Popup.aspx?billnumber=' + billnumber + '&caseid=' + caseid + '&caseno=' + caseno;
            ShowPopup2.SetContentUrl(url);
            ShowPopup2.Show();

            return false;
        } 
    </script>
    <asp:updatepanel id="UpdatePanel112" runat="server" visible="true">
        <contenttemplate>
            <table id="First" border="0" cellpadding="0" cellspacing="0" width="1200px" style="background-color:White;">
                <tr>
                    <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; ;
                        padding-top: 3px;  vertical-align: top; width:1000px; ">
                        <table cellpadding="0" cellspacing="0"  border="0" style="background-color:White; width:800px;"> 
                            <tr>
                                <td >
                                </td>
                                <td valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" 
                                        style="background-color:White;width:1000px; ">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <UserMessage:MessageControl runat="server" ID="usrMessage2" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:550px" valign="top">
                                                <table width="550px" border="0">
                                                    <tr>
                                                        <td style="text-align: left; width: 550px;" >
                                                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 550px;
                                                                height: 50%; border: 1px solid #B5DF82;">
                                                                <tr>
                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82"  style="width: 200px">
                                                                        <b class="txt3">Search Parameters</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20%; height: 0px;" valign="top">
                                                                        <table border="0" cellpadding="0"  cellspacing="0" style="width: 550px;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                            <tr>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                    Verification</td>
                                                                                     <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    From Date</td>
                                                                                     <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    To Date
                                                                                </td>
                                                                           
                                                                              
                                                                                </tr>
                                                                              
                                                                            <tr>
                                                                             
                                                                                <td class="td-widget-bc-search-desc-ch"  width="33%">
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
                                                                           
                                                                               <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    <asp:TextBox ID="txtupdateFromDate" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox><asp:ImageButton
                                                                                        ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="imgbtnFromDate"
                                                                                        TargetControlID="txtupdatefromdate">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                </td>
                                                                                   <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    <asp:TextBox ID="txtupdateToDate" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox><asp:ImageButton
                                                                                        ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="imgbtnToDate"
                                                                                        TargetControlID="txtupdateToDate">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                               
                                                                              <tr>
                                                                               <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    Case No</td>
                                                                                    
                                                                                     <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                
                                                                                    Bill No</td>
                                                                                    
                                                                                    <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    Patient Name</td>
                                                                              </tr>
                                                                      <tr>
                                                                     <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    <asp:TextBox ID="txtupdateCaseNo" runat="server" Width="90px"></asp:TextBox>
                                                                                    </td>
                                                                                 
                                                                               <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    <asp:TextBox ID="txtupdateBillNo" runat="server" Width="89px"></asp:TextBox>
                                                                                </td>
                                                                                
                                                                               <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    <asp:TextBox ID="txtupdatePatientName" runat="server" Width="90px"></asp:TextBox>
                                                                                </td>
                                                                      </tr>
                                                                            
                                                                          
                                                                                
                                                                                   
                                                                                
                                                                            
                                                                           
                                                                            <tr>
                                                                               <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    Insurance Company
                                                                                </td>
                                                                           <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                    Provider
                                                                                </td>
                                                                                            <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                        Case Type
                                                                                    </td>
                                                                              
                                                                            </tr>
                                                                             
                                                                            <tr>
                                                                                 <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                  <%--  <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="15%" Selected_Text="---Select---"
                                                                                        Connection_Key="Connection_String" Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY">
                                                                                    </extddl:ExtendedDropDownList>--%>
                                                                                    <asp:TextBox ID="txtINS1" runat="server"  Width="98%"  autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                                                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                                                                                                                DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtINS1"
                                                                                                                ServiceMethod="GetInsurance" ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                                                                                                            </ajaxToolkit:AutoCompleteExtender>
                                                                                                
                                                                                    
                                                                                </td>
                                                                           
                                                                                 <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                   <%-- <cc1:ExtendedDropDownList ID="extddlOffice" runat="server" Connection_Key="Connection_String"
                                                                                        Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_GET_OFFICE_LIST_FOR_SHCEDULE_REPORT"
                                                                                        Selected_Text="--- Select ---" Width="15%"></cc1:ExtendedDropDownList>--%>
                                                                                        <asp:TextBox ID="txtOffice1" runat="server" Text=""  Width="98%"  autocomplete="off" CssClass="search-input" ></asp:TextBox> 
                                                                                         <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoOffice" EnableCaching="true" DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtOffice1" ServiceMethod="GetProvider" ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                                                                                                            </ajaxToolkit:AutoCompleteExtender>
                                                                                                            
                                                                                        
                                                                                                
                                                                                </td>
                                                                                  <td class="td-widget-bc-search-desc-ch"  width="33%">
                                                                                     <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="90%" Selected_Text="---Select---"
                                                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String">
                                                                                        </extddl:ExtendedDropDownList>
                                                                                  </td>
                                                                            </tr>
                                                                            
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <table border="0" style="width: 550px">
                                                                                        <tr>
                                                                                            <td style="width: 550px">
                                                                                                <asp:RadioButtonList ID="rblView" runat="server" RepeatDirection="Horizontal">
                                                                                                    <asp:ListItem Value="0"  Selected="True" >Outstanding Verifications &nbsp;</asp:ListItem>
                                                                                                    <asp:ListItem Value="1">Answered Verifications&nbsp;</asp:ListItem>
                                                                                                      <asp:ListItem Value="2">paid/denied&nbsp;</asp:ListItem>
                                                                                                    <%-- <asp:ListItem Value="2">Verification sent&nbsp;&nbsp;</asp:ListItem>--%>
                                                                                                </asp:RadioButtonList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td  style="vertical-align: middle; text-align: center" colspan="3">
                                                                                    &nbsp;
                                                                                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click1" runat="server" Width="80px"
                                                                                        Text="Search" OnClientClick="return Validation()"></asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                    
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:850px" valign="top">
                                                 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table>
                                                </table>
                                            </td>
                                            <tr>
                                                <td>
                                                    <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                                </td>
                                            </tr>
                                        </tr>
                                        <tr>
                                            <td style="width: 1027px; height: auto;">
                                               <div style="width: 1035px; ">
                                                    <table style="height: auto; width: 1035px; border: 1px solid #B5DF82;" 
                                                        align="left" cellpadding="0" cellspacing="0" class="border">
                                                        <tr>
                                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px">
                                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel112"
                                                                    DisplayAfter="10">
                                                                    <ProgressTemplate>
                                                                        <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                            runat="Server">
                                                                            <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                Height="25px" Width="24px" align="center"></asp:Image>
                                                                            Loading...</div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 800px;">
                                                                <table style="vertical-align: middle; width: 1110px;" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="vertical-align: middle; width: 500px" align="left">
                                                                                Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                                    CssClass="search-input">
                                                                                </gridsearch:XGridSearchTextBox>
                                                                            </td>
                                                                            <td style="vertical-align: middle; width: 1110px; text-align: right" align="right" colspan="3">
                                                                                Record Count:<%= this.grdvDenial.RecordCount %>| Page Count:
                                                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                </gridpagination:XGridPaginationDropDown>
                                                                                <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                    Text="Export TO Excel">
                                                                                     <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                                <asp:Button ID="btnLitigantion" runat="server" Text="Litigate" OnClick="btnLitigantion_Click" />
                                                                                <asp:Button ID="btnVeriSent" runat="server" Text="Save Answer" Visible="true" OnClick="btnVeriSent_Click" />
                                                                                <asp:Button ID="btnSendMail" runat="server" Text="Print Pom"/> 
                                                                                <asp:Button ID="btnPrintEnv" runat="server" Text="Print Envelope"  OnClick="btnPrintEnv_Click" /> 
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                
                                                                    <xgrid:XGridViewControl ID="grdvDenial" OnRowCommand="grdvDenial_RowCommand" runat="server"
                                                                        Height="148px" Width="1110px" CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153"
                                                                        ExcelFileNamePrefix="ExcelLitigation" ShowExcelTableBorder="true" EnableRowClick="false"
                                                                        ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader" ExportToExcelColumnNames="Bill No.,Case #,Patient Name,Doctor Name,Provider Name,Insurance Company,Verification Date,Visit Date,Bill Amount,Paid Amount,Balance,Verification Request,Requested User,Date,Answer,Answered User,date"
                                                                        ExportToExcelFields="SZ_BILL_NUMBER,SZ_CASE_NO,SZ_PATIENT_NAME,SZ_DOCTOR_NAME,SZ_OFFICE,INS_NAME,DT_VERIFICATION_DATE,DT_VISIT_DATE,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE,SZ_VERIFICATION,SZ_VERIFICATION_USER,SZ_VERIFICATION_DATE,SZ_ANS,SZ_ANS_USER,SZ_ANS_DATE"
                                                                        AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" GridLines="None" XGridKey="VerificationRecivedSentReport"
                                                                        PageRowCount="10" PagerStyle-CssClass="pgr" 
                                                                        DataKeyNames="SZ_CASE_ID,SZ_CASE_NO,SZ_BILL_NUMBER,SZ_BILL_CODE,status_code,SZ_ANS_ID,I_VERIFICATION_ID, SZ_DOCTOR_NAME,DT_VISIT_DATE,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE,SZ_VERIFICATION_USER,SZ_VERIFICATION_DATE,SZ_ANS_USER,SZ_SPECIALITY,SZ_COMPANY_NAME,SZ_CLAIM_NUMBER,SZ_INSURANCE_ADDRESS,SZ_MIN_SERVICE_DATE,SZ_MAX_SERVICE_DATE,SZ_CASE_TYPE_ID,WC_ADDRESS,DT_BILL_DATE,SZ_COMPANY_ID,SZ_OFFICE_ID,SZ_PATIENT_NAME,BT_PAYMENT"
                                                                        AllowSorting="true">
                                                                        <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                        <Columns>
                                                                                  <%--0--%>
                                                                            <asp:TemplateField HeaderText="Bill No" SortExpression="cast(substring(TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER,3,(Len(TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)-2)) as numeric(13))">
                                                                                <itemtemplate>
                                                                                        <%--<asp:LinkButton ID="lnkP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PLS"  font-size="15px" 
                                                                                            Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                                                        <asp:LinkButton ID="lnkM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="15px"  Visible="false"></asp:LinkButton>--%>                                                                                  
                                                                                        <asp:Label id="lblBill" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'></asp:Label>
                                                                                   </itemtemplate>
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left" width="50px"></itemstyle>
                                                                            </asp:TemplateField>
                                                                              <%--1--%>
                                                                            <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #" SortExpression="convert(int,SZ_CASE_NO)">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--2--%>
                                                                            <%--<asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" SortExpression="(ISNULL(SZ_PATIENT_FIRST_NAME,'')  +' '+ ISNULL(SZ_PATIENT_LAST_NAME ,''))">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>--%>
                                                                            <%--Nirmalkumar--%>
                                                                            <asp:TemplateField HeaderText="Patient Name" Visible="false" SortExpression="(ISNULL(SZ_PATIENT_FIRST_NAME,'')  +' '+ ISNULL(SZ_PATIENT_LAST_NAME ,''))">
                                                                                <itemtemplate>
                                                                                    <a target="_self" href='../AJAX Pages/Bill_Sys_CaseDetails.aspx?CaseID=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>&cmp=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>'><%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%></a>
                                                                                    <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>' Visible="false" ></asp:LinkButton>
                                                                                </itemtemplate>
                                                                            </asp:TemplateField>
                                                                            <%--3--%>
                                                                            <asp:TemplateField HeaderText="Patient Name" Visible="false" SortExpression="(ISNULL(SZ_PATIENT_FIRST_NAME,'')  +' '+ ISNULL(SZ_PATIENT_LAST_NAME ,''))">
                                                                                <itemtemplate>
                                                                                    <a target="_self" href='../AJAX Pages/Bill_Sys_ReCaseDetails.aspx?CaseID=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>&cmp=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>'><%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%></a>
                                                                                    <asp:LinkButton ID="lnkSelectRCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>' Visible="false" ></asp:LinkButton>
                                                                                </itemtemplate>
                                                                            </asp:TemplateField>
                                                                             <%--4--%>
                                                                            <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name" SortExpression="SZ_DOCTOR_NAME" visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--5--%>
                                                                            <asp:BoundField DataField="SZ_OFFICE" HeaderText="Provider Name" SortExpression="MST_OFFICE.SZ_OFFICE">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="left"></itemstyle>
                                                                            </asp:BoundField>
                                                                            <%--      <asp:TemplateField HeaderText="Bill Status">
                                                                            <itemtemplate>
                                                        <asp:Label id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_NAME")%>' Visible="false" ></asp:Label>
                                                
                                            </itemtemplate>
                                                                        </asp:TemplateField>--%>
                                                                         <%--6--%>
                                                                            <asp:BoundField DataField="INS_NAME" HeaderText="Insurance Company" SortExpression="MST_INSURANCE_COMPANY.SZ_INSURANCE_NAME">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="left"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--7--%>
                                                                            <asp:TemplateField HeaderText="Verification Date" 
                                                                            SortExpression="TXN_BILL_VERIFICATION.DT_VERIFICATION_DATE">
                                                                                <itemtemplate>
                                                                           <asp:Label id="lbltest" runat="server" Text='<%# DataBinder.Eval (Container,"DataItem.DT_VERIFICATION_DATE")%>'></asp:Label>
                                                                         </itemtemplate>
                                                                            </asp:TemplateField>
                                                                             <%--8--%>
                                                                            <asp:BoundField DataField="DT_VISIT_DATE" HeaderText="Visit Date" SortExpression="(SELECT MAX(DT_DATE_OF_SERVICE) FROM TXN_BILL_TRANSACTIONS_DETAIL WHERE SZ_BILL_NUMBER = TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)" visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="left"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--9--%>
                                                                            <asp:BoundField DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" SortExpression="convert(int,FLT_BILL_AMOUNT)" visible="false"
                                                                                DataFormatString="{0:C}" >
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--10--%>
                                                                            <asp:BoundField DataField="PAID_AMOUNT" HeaderText="Paid Amount" SortExpression="(SELECT SUM(FLT_CHECK_AMOUNT) FROM TXN_PAYMENT_TRANSACTIONS WHERE SZ_BILL_ID=TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)" visible="false" DataFormatString="{0:C}">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--11--%>
                                                                            <asp:BoundField DataField="FLT_BALANCE" HeaderText="Balance" SortExpression="convert(int,FLT_BALANCE)" visible="false"
                                                                                DataFormatString="{0:c}">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--12--%>
                                                                            <asp:BoundField DataField="SZ_VERIFICATION" HeaderText="Verification Request" SortExpression="TXN_BILL_VERIFICATION.SZ_DESCRIPTION" >
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--13--%>
                                                                            <asp:BoundField DataField="SZ_VERIFICATION_USER" HeaderText="Requested User" SortExpression="TXN_BILL_VERIFICATION.SZ_USER_NAME" visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                              <%--14--%>
                                                                            <asp:BoundField DataField="SZ_VERIFICATION_DATE" HeaderText="Date" SortExpression="TXN_BILL_VERIFICATION.DT_CREATED_DATE"
                                                                                DataFormatString="{0:MM/dd/yyyy}" visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                              <%--15--%>
                                                                            <asp:TemplateField HeaderText="Answer">
                                                                                <itemtemplate>
                                                                        <asp:TextBox id="txtAnswer" TextMode="MultiLine" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_ANS")%>' ></asp:TextBox>
                                                
                                                                            </itemtemplate>
                                                                            </asp:TemplateField>
                                                                                <%--16--%>
                                                                            <asp:BoundField DataField="SZ_ANS_USER" HeaderText="Answered User" SortExpression="TXN_BILL_VERIFICATION_ANSWER.sz_user_name" visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                                <%--17--%>
                                                                            <asp:BoundField DataField="SZ_ANS_DATE" HeaderText="Ans. Date" SortExpression="TXN_BILL_VERIFICATION_ANSWER.dt_created"
                                                                                DataFormatString="{0:MM/dd/yyyy}">
                                                                                <headerstyle horizontalalign="Left"> </headerstyle>
                                                                                <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--18--%>
                                                                            <asp:BoundField DataField="DT_VERIFICATION_ANSWER" HeaderText="Answer Date" SortExpression=""
                                                                                visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--19--%>
                                                                            <asp:BoundField DataField="SZ_ANS_ID" HeaderText="Answer id" SortExpression="" visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--20--%>
                                                                            <asp:BoundField DataField="I_VERIFICATION_ID" HeaderText="I_VERIFICATION_ID" SortExpression=""
                                                                                visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--21--%>
                                                                            <asp:BoundField DataField="SZ_VERIFICATION_ANSWER" HeaderText="Answer" SortExpression=""
                                                                                visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--22--%>
                                                                            <asp:TemplateField HeaderText="View">
                                                                                <itemtemplate>
                                                <%# DataBinder.Eval(Container,"DataItem.SZ_BILL_PATH")%>
                                           </itemtemplate>
                                                                            </asp:TemplateField>
                                                                             <%--23--%>
                                                                            <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill #" visible="false"></asp:BoundField>
                                                                             <%--24--%>
                                                                            <asp:BoundField DataField="SZ_BILL_CODE" HeaderText="Answer Date" SortExpression=""
                                                                                visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>
                                                                             <%--25--%>
                                                                            <asp:TemplateField HeaderText="" >
                                                                                <itemtemplate>
                                                                        <asp:LinkButton ID="lnkP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PLS"  font-size="12px" 
                                                                            Text="more" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                                        <asp:LinkButton ID="lnkM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="MNS" Text="hide" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="12px"  Visible="false"></asp:LinkButton>                                                                                  
                                                                    </itemtemplate>
                                                                                <itemstyle horizontalalign="Center" />
                                                                            </asp:TemplateField>
                                                                             <%--26--%>
                                                                            <asp:TemplateField HeaderText="Denial" visible="false">
                                                                                <itemtemplate>
                                                    <asp:LinkButton ID="lnkDP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="DenialPLS"  font-size="15px" 
                                                        Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                    <asp:LinkButton ID="lnkDM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="DenialMNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="15px"  Visible="false"></asp:LinkButton>                                                                                  
                                                </itemtemplate>
                                                                                <itemstyle horizontalalign="Center" />
                                                                            </asp:TemplateField>
                                                                           
                                                                            <%--27--%>
                                                                            <asp:TemplateField HeaderText="Insurance Details">
                                                                            <itemtemplate>
                                                                                <asp:TextBox ID="txtInsDetails" runat="server" Width="100%" ToolTip="Enter insurance company name"></asp:TextBox>
                                                                                <asp:TextBox ID="txtInsAddress" runat="server" Width="100%" ToolTip="Enter insurance company address/street"></asp:TextBox>
                                                                                <asp:TextBox ID="txtInsState" runat="server" Width="100%" ToolTip="Enter insurance company city, state zip"></asp:TextBox> 
                                                                             </itemtemplate>
                                                                             <headerstyle horizontalalign="Left"></headerstyle>
                                                                             <itemstyle horizontalalign="left" width="20%"></itemstyle>
                                                                            </asp:TemplateField>
                                                                            <%--28--%>
                                                                            <asp:TemplateField HeaderText="Insurance Description">
                                                                                <itemtemplate>
                                                                                      <asp:TextBox ID="txtInsDesc" runat="server" Width="100%" ToolTip="Enter insurance company name" TextMode="MultiLine"></asp:TextBox>
                                                                                </itemtemplate>
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="left" width="20%" ></itemstyle>
                                                                            </asp:TemplateField>

                                                                             <%--29--%>
                                                                      
                                                                     <asp:TemplateField HeaderText="Status" Visible="true" SortExpression="bt_verification">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkPayment" Font-Underline="false" runat="server" CausesValidation="false"
                                                                                        CommandName="pay" Font-Size="12px" Text="Payment" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                        OnClientClick='<%# "showPopup2(" + "\""+ Eval("SZ_BILL_NUMBER") + "\""+ ", " + "\""+ Eval("SZ_CASE_ID") +"\""+ ", " + "\""+ Eval("SZ_CASE_NO") +"\");" %>'></asp:LinkButton>
                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--30--%>
                                                                         <asp:BoundField DataField="days" HeaderText="Verification Days" >
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>

                                                                     <%--31--%>
                                                                      <asp:TemplateField>
                                                                                 <HeaderTemplate>
                                               <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                           </HeaderTemplate>
                                                                                <itemtemplate>
                                                 <asp:CheckBox ID="ChkLitigantion" runat="server" ></asp:CheckBox>
                                               </itemtemplate>
                                                                            </asp:TemplateField>
                                                                             <%--32--%>
                                                                            <asp:TemplateField visible="false">
                                                                                <itemtemplate>                                            
                                                <tr>
                                                    <td colspan="100%">
                                                        <div id="div<%# Eval("SZ_BILL_NUMBER") %><%# ((GridViewRow) Container).RowIndex %>" style="display: none; position: relative; left:25px;">
                                                        <asp:GridView ID="grdVerification" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SZ_DOCTOR_NAME" ItemStyle-Width="550px"  HeaderText="Doctor Name">
                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DT_VISIT_DATE" ItemStyle-Width="85px"  HeaderText="Verification Date">
                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FLT_BILL_AMOUNT" ItemStyle-Width="400px" DataFormatString="{0:C}" HeaderText="Bill Amount"  >	
                                                                <itemstyle horizontalalign="right"></itemstyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PAID_AMOUNT" ItemStyle-Width="400px" HeaderText="Paid Amount" DataFormatString="{0:C}" >	
                                                                <itemstyle horizontalalign="right"></itemstyle>
                                                            </asp:BoundField>
                                                               <asp:BoundField DataField="FLT_BALANCE" ItemStyle-Width="400px" HeaderText="Balance" DataFormatString="{0:C}" >	
                                                                <itemstyle horizontalalign="right"></itemstyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SZ_VERIFICATION_USER" ItemStyle-Width="500px" HeaderText="Request User"  >	
                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                            </asp:BoundField>     
                                                            <asp:BoundField DataField="SZ_VERIFICATION_DATE" ItemStyle-Width="85px"  DataFormatString="{0:ddd MM, yyyy}"     HtmlEncode="false"  HeaderText="Request Date">
                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                            </asp:BoundField>
                                                             <asp:BoundField DataField="SZ_ANS_USER" ItemStyle-Width="500px"  HeaderText="Answer User">
                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                        </asp:GridView>
                                                   
                                                    </div>
                                                    </td>
                                                </tr>
                                                </itemtemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField visible="false">
                                                                                <itemtemplate>                                            
                                            <tr>
                                                <td colspan="50%">
                                                    <div id="div1<%# Eval("SZ_BILL_NUMBER") %><%# ((GridViewRow) Container).RowIndex %>" style="display: none; position: relative; left:25px;">
                                                    <asp:GridView ID="grdDenial" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="500px" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SZ_DENIAL_REASONS" ItemStyle-Width="105px"  HeaderText="Denial Reason">
                                                            <itemstyle horizontalalign="center"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DT_DENIAL_DATE" ItemStyle-Width="50px" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Denial Date">
                                                            <itemstyle horizontalalign="Center"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SZ_DESCRIPTION" ItemStyle-Width="85px" HeaderText="Description">	
                                                            <itemstyle horizontalalign="center"></itemstyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                    </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr> 
                                            </itemtemplate>
                                                                            </asp:TemplateField>
                                                                             <%--33--%>
                                                                            <asp:BoundField DataField="status_code" HeaderText="status code" SortExpression=""
                                                                                visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>

                                                                                <%--34--%>
                                                                            <asp:BoundField DataField="BT_PAYMENT" HeaderText="BT_PAYMENT" SortExpression="" visible="false">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>

                                                                        </Columns>
                                                                    </xgrid:XGridViewControl>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 10px; height: 100%;">
                                    &nbsp;
                                </td>
                            </tr>
                            
                </table> </td> </tr>
            </table>
            <div style="display: none">
                <asp:LinkButton ID="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="modal" runat="server"
                TargetControlID="lbn_job_det" PopupDragHandleControlID="pnlSaveDescriptionHeader"
                PopupControlID="pnlSaveDescription" DropShadow="true">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel Style="display: none; background-color: #DBE6FA; width: 800px; height=0px;"
                ID="pnlSaveDescription" runat="server">
                <div align="left" style="vertical-align: top;">
                    <div style="position: relative; text-align: left; background-color: #8babe4;" id="pnlSaveDescriptionHeader">
                        <asp:Label ID="Label2" runat="server" Text="Verification Sent" Font-Bold="true" CssClass="lbl"></asp:Label>
                    </div>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                        class="TDPart">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <UserMessage:MessageControl runat="server" ID="usrMessage1" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <div style="overflow: scroll; height: 400px;">
                                    <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #8babe4;">
                                        <asp:Button ID="btnClose1" runat="server" Height="19px" Width="50px" CssClass="Buttons"
                                            Text="X" OnClientClick="closeTypePage1()"></asp:Button>
                                    </div>
                                    <asp:DataGrid ID="grdVerificationSend" Width="100%" CssClass="GridTable" runat="Server"
                                        AutoGenerateColumns="False">
                                        <HeaderStyle CssClass="GridHeader" />
                                        <ItemStyle CssClass="GridRow" />
                                        <Columns>
                                            <asp:BoundColumn DataField="sz_bill_number" HeaderText="Bill#" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="verification_request" HeaderText="Verification Request"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="verification_date" HeaderText="Request Date" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="request_user" HeaderText="Username" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="left"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Answer">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="taxAns" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container,"DataItem.answer")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="answer_date" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="answer_user" HeaderText="Answer User" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="answer_id" HeaderText="Answer User" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="I_VERIFICATION_ID" HeaderText="id" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="sz_case_id" HeaderText="case_id" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSaveSendRequest" runat="server" Text="Save" OnClick="btnSaveSendRequest_Click" />&nbsp<asp:Button
                                    ID="btnCancelSendRequest" Text="Cancel" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </contenttemplate>
    </asp:updatepanel>
    <asp:textbox id="txtCaseNo" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtBillNo" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtCompanyID" runat="server" visible="False" width="10px">
    </asp:textbox>
    <asp:textbox id="txtBillStatus" runat="server" text="VR" visible="false" width="10px" />
    <asp:textbox id="txtVerRec" runat="server" visible="false" />
    <asp:textbox id="txtVarAns" runat="server" visible="false" />
    <asp:textbox id="txtDenial" runat="server" visible="false" />
    <asp:textbox id="txtDay" runat="server" text="" visible="false" width="10px" />
    <asp:textbox id="txtFlag" runat="server" text="REF" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtFromDate" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtToDate" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtPatientName" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <input type="hidden" value="" runat="server" id="hdlverisent" />
    <input type="hidden" value="" runat="server" id="hdlbillnumber" />
    <asp:textbox id="txtOffice" runat="server" text="" visible="false">
    </asp:textbox>
    <asp:textbox id="txtINS" runat="server" visible="false">
    </asp:textbox>
    <asp:hiddenfield id="hdnPOMValue" runat="server" />
    <div id="div1" style="position: absolute; left: 50%; top: 920px; width: 30%; height: 150px;
        background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4; left: 0px;
            top: 0px;">
            MSG
        </div>
        <br />
        <br />
        <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
            <span id="Span2" runat="server"></span>
        </div>
        <br />
        <br />
        <div style="text-align: center;">
            <asp:button id="btnYes" runat="server" cssclass="Buttons" onclick="btnYes_Click"
                text="Yes" width="80px" />
            <asp:button id="btnNo" runat="server" cssclass="Buttons" onclick="btnNo_Click" text="No"
                width="80px" />
        </div>
    </div>
    

     <dx:ASPxPopupControl ID="ShowPopup2" runat="server" CloseAction="CloseButton" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ShowPopup2"
        HeaderText="Bill Payment" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#D3D3D3"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="800px"
        ToolTip="Select Cost Center" PopupHorizontalOffset="0" PopupVerticalOffset="0"
          AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="450px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <asp:TextBox ID="utxtCaseType" runat="server" Width="10px" Visible="False"></asp:TextBox>
</asp:content>