<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_PaperAuthorizationDesk.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_PaperAuthorizationDesk" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

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




        function SetVisitDate() {
            getWeek();
            var selValue = document.getElementById("<%=ddlDateValues.ClientID %>").value;
            if (selValue == 0) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = "";
                document.getElementById("<%=txtVisitDate.ClientID %>").value = "";
            }
            else if (selValue == 1) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('today');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getWeek('endweek');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('monthend');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('quarterend');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('yearend');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getLastWeek('endweek');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = lastmonth('endmonth');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = lastyear('endyear');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = quarteryear('endquaeter');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = quarteryear('startquaeter');
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






        function CloseDocPopup() {
            document.getElementById('divid').style.visibility = 'hidden';
            document.getElementById('divid').style.zIndex = -1;
        }
        function OpenReport(obj) {
            document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value = obj;
            //alert(document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value);
            document.getElementById('_ctl0_ContentPlaceHolder1_btnSpeciality').click();


        }
        function CloseEditProcPopup() {

            document.getElementById('divid1').style.visibility = 'hidden';
            document.getElementById('divid1').style.zIndex = -1;
        }
        function CloseDocumentPopup() {
            document.getElementById('divid2').style.visibility = 'hidden';
            document.getElementById('divid2').style.zIndex = -1;
        }
        function showUploadFilePopup() {

            var flag = false;
            var grdProc = document.getElementById('_ctl0_ContentPlaceHolder1_grdAllReports');
            if (grdProc.rows.length > 0) {
                for (var i = 1; i < grdProc.rows.length; i++) {
                    var cell = grdProc.rows[i].cells[0];
                    for (j = 0; j < cell.childNodes.length; j++) {
                        if (cell.childNodes[j].type == "checkbox" && grdProc.rows[i].cells[4].innerHTML != "Received Report") {
                            if (cell.childNodes[j].checked) {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
                if (flag == true) {
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height = '100px';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'visible';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.position = "absolute";
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.top = '10px';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.left = '200px';
                    //    document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value=''; 
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.height = '0px';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.visibility = 'hidden';
                    //    document.getElementById('_ctl0_ContentPlaceHolder1_txtDateofService').value='';   
                    MA.length = 0;
                }
                else {
                    alert("Select procedure code ..");
                }
            }
        }

        function CloseUploadFilePopup() {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height = '0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'hidden';
            //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
        }
        // 21 April 2010 show ReceiveReport popup -- sachin
        function showReceiveDocumentPopup() {

            document.getElementById('divid').style.zIndex = 1;
            document.getElementById('divid').style.position = 'absolute';
            document.getElementById('divid').style.left = '300px';
            document.getElementById('divid').style.top = '100px';
            document.getElementById('divid').style.visibility = 'visible';
            document.getElementById('frameeditexpanse').src = '../Bill_Sys_ReceivedDocumentPopupPage.aspx';
            return false;

        }

        function TestPopup() {
            alert("Hi");
        }

        //        function showPateintFrame(objCaseID, objCompanyID) {

        //            var url = "PatientViewFrame.aspx?CaseID=" + objCaseID + "&cmpId=" + objCompanyID + "";
        //            //alert(url);

        //            TestDataPopup.SetContentUrl(url);
        //            TestDataPopup.Show();
        //            return false;

        //        }

        function showEditPopup(caseid, Eventprocid, progroupid, patientid, eventid, speciality, descrption, code, CaseType, patientname, dateofservice, lhrcode, caseno, drid, patientid, ReadingDoctorId) {
            var url = 'Bill_Sys_EditAll.aspx?CaseID=' + caseid + '&Type=YES&EProcid=' + Eventprocid + '&pdid=' + progroupid + '&eventid=' + eventid + '&spc=' + speciality + '&desc=' + descrption + '&code=' + code + '&patientname=' + patientname + '&dateofservice=' + dateofservice + '&lhrcode=' + lhrcode + '&caseno=' + caseno + '&doctorid=' + drid + '&patientid=' + patientid + '&ReadingDoctorId=' + ReadingDoctorId;
            TestDataPopup.SetContentUrl(url);
            TestDataPopup.SetHeaderText("Patient Name - " + patientname + " | Date - " + dateofservice + " | LHR Code - " + lhrcode + " | Case Type - " + CaseType + " | Case# - " + caseno + " ");
            TestDataPopup.Show();

            return false;
        }

        function ChangeHeaderText(patientname, dateofservice, lhrcode, CaseType, caseno) {
            TestDataPopup.SetHeaderText("Patient Name - " + patientname + " | Date - " + dateofservice + " | LHR Code - " + lhrcode + " | Case Type - " + CaseType + " | Case# - " + caseno + " ");
        }

        function ClosePopup() {
            TestDataPopup.Hide();
            var sURL = unescape(window.location.pathname);
            sURL = sURL + '?Flag=report&Type=P';
            window.location.href = sURL;
            var button = document.getElementById('<%=btnCls.ClientID%>');
            button.click();
        }

        function SelectAndClosePopup() {
            TestDataPopup.Hide();
            window.location.href = 'Bill_Sys_paid_bills.aspx?Flag=report&Type=p&Save=done';
        }

        function ShowDiv() {
            document.getElementById('divDashBoard').style.position = 'absolute';
            document.getElementById('divDashBoard').style.left = '200px';
            document.getElementById('divDashBoard').style.top = '120px';
            document.getElementById('divDashBoard').style.visibility = 'visible';
            return false;
        }


        function showProcPopup() {
            // alert('123');
            document.getElementById('divid1').style.zIndex = 1;
            document.getElementById('divid1').style.position = 'absolute';
            document.getElementById('divid1').style.left = '300px';
            document.getElementById('divid1').style.top = '100px';
            document.getElementById('divid1').style.visibility = 'visible';
            document.getElementById('frameeditProc').src = '../Bill_Sys_EditRProcPopupPage.aspx';
            return false;

        }

        function ViewDocumentPopup(caseid, Eventprocid, speciality) {

            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute';
            document.getElementById('divid2').style.left = '300px';
            document.getElementById('divid2').style.top = '100px';
            document.getElementById('divid2').style.visibility = 'visible';
            document.getElementById('frm').src = '../Bill_Sys_ViewDocuments.aspx?CaseID=' + caseid + '&Type=YES&EProcid=' + Eventprocid + '&spc=' + speciality;
        }

        function SelectCaseAll(ival) {

            var f = document.getElementById('ctl00_ContentPlaceHolder1_grdViewDocuments');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {



                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }



                }


            }

        }

        function confirm_update_bill_status() {

            var f = document.getElementById('ctl00_ContentPlaceHolder1_grdViewDocuments');
            var bfFlag = false;
            var cnt = 0;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkView') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;
                            cnt = cnt + 1;

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

        function Clear() {
            //alert("call");

            document.getElementById("<%=txtVisitDate.ClientID %>").value = '';
            document.getElementById("<%=txtToVisitDate.ClientID %>").value = '';
            document.getElementById("<%=txtNumberOfDays.ClientID %>").value = '';
            document.getElementById("<%=txtpatientid.ClientID %>").value = '';
            document.getElementById("<%=txtappointmentid.ClientID %>").value = '';

            document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value = 'NA';


            document.getElementById("<%=ddlDateValues.ClientID %>").value = 0;
        }
       
    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td height="28" align="left" bgcolor="#B5DF82" class="txt2" colspan="3" valign="middle">
                            <table width="40%" style="border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82;
                                border-bottom: 1px solid #B5DF82">
                                <tr>
                                    <td height="28" style="background-color: #B5DF82;" class="txt2" colspan="3">
                                        <asp:Label Font-Bold="true" Font-Size="Small" ID="txtHeading" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99%" colspan="3">
                            <table width="50%">
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblvisitdate" runat="server" Text=" Visit Date" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblfrom" runat="server" Text=" From" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblto" runat="server" Text=" To" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
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
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:TextBox ID="txtVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="69%"></asp:TextBox>
                                        <asp:ImageButton ID="imgVisit" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtVisitDate"
                                            PopupButtonID="imgVisit" />
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:TextBox ID="txtToVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="70%"></asp:TextBox>
                                        <asp:ImageButton ID="imgVisite1" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToVisitDate"
                                            PopupButtonID="imgVisite1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblCaseType" runat="server" Text="Case Type" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblNoOfDays" runat="server" Text="Number Of Days(>=)" CssClass="td-widget-bc-search-desc-ch"> </asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblNoofDaysLess" runat="server" Text="Number Of Days(<=)" CssClass="td-widget-bc-search-desc-ch"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="100%" Selected_Text="---Select---"
                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"
                                            CssClass="search-input"></extddl:ExtendedDropDownList>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:TextBox ID="txtNumberOfDays" runat="server" onkeypress="return CheckForInteger(event,'')"
                                            MaxLength="4"></asp:TextBox>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:TextBox ID="txtNumberOfDaysLess" runat="server" onkeypress="return CheckForInteger(event,'')"
                                            MaxLength="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblAppointmentid" runat="server" Text="Appointment Id" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblpatientid" runat="server" Text="Patient Id" CssClass="td-widget-bc-search-desc-ch"> </asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblInsurance" runat="server" Text="Insurance" CssClass="td-widget-bc-search-desc-ch"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:TextBox ID="txtappointmentid" runat="server" Width="98%"></asp:TextBox>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:TextBox ID="txtpatientid" runat="server" Width="80%"></asp:TextBox>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="100%" Selected_Text="---Select---"
                                            Procedure_Name="SP_MST_INSURANCE_COMPANY" Flag_Key_Value="INSURANCE_LIST_PAPER_AUTHORIZATION"
                                            Connection_Key="Connection_String" AutoPost_back="false"></extddl:ExtendedDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:UpdatePanel ID="pnlprogress" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearchvisit_Click" />
                                                <input style="width: 80px" id="btnClear" onclick="Clear();" type="button" value="Clear"
                                                    runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="3" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="text-align: left; height: 25px;" colspan="4">
                                        <a id="hlnkShowDiv" href="#" onclick="ShowDiv()" runat="server" visible="false">Dash
                                            board</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; height: 25px;" colspan="4">
                                        <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; height: 25px;" colspan="4">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <contenttemplate>
                                                <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                            </contenttemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="ReceivedReport" runat="server" visible="false">
                        <td style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <asp:UpdatePanel ID="ReportUpdate" runat="server">
                                <contenttemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnnext" runat="server" Style="float: left;" Text="Received Document"
                                                    Width="150px" Visible="true" OnClick="btnnext_Click" />
                                                <asp:DropDownList ID="drpdown_Documents" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpdown_Documents_SelectionChanged">
                                                    <asp:ListItem Text="Show All Visits" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Show Visits with Documents" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Show Visits without Documents" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CheckBox ID="chkAOb" runat="server" Text="AOB" Font-Bold="true" Font-Size="12px">
                                                </asp:CheckBox>
                                                &nbsp;
                                                <asp:CheckBox ID="chkReport" runat="server" Text="Report" Font-Bold="true" Font-Size="12px">
                                                </asp:CheckBox>&nbsp;
                                                <asp:CheckBox ID="chkReferral" runat="server" Text="Referral" Font-Bold="true" Font-Size="12px">
                                                </asp:CheckBox>
                                                <asp:CheckBox ID="chkLien" runat="server" Text="Lien form" Font-Bold="true" Font-Size="12px">
                                                </asp:CheckBox>
                                                <asp:CheckBox ID="chkComp" runat="server" Text="Comp Autho" Font-Bold="true" Font-Size="12px">
                                                </asp:CheckBox>
                                                <asp:CheckBox ID="chkAdditionalReports" runat="server" Text="Additional Reports"
                                                    Font-Bold="true" Font-Size="12px"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="SectionDevider" colspan="2">
                                                <asp:TextBox ID="txtSort" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                                                <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="False" Font-Bold="True"
                                                    ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table style="vertical-align: middle; width: 100%">
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="4" align="center">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: middle; width: 30%" id="Td1" runat="server" align="left">
                                                                Search:&nbsp;
                                                                <gridsearch:XGridSearchTextBox ID="txtSearchBox1" runat="server" AutoPostBack="true"
                                                                    CssClass="search-input">
                                                                </gridsearch:XGridSearchTextBox>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                            </td>
                                                            <td style="vertical-align: middle; width: 40%; text-align: right" id="Exceltd" runat="server"
                                                                align="right" colspan="2">
                                                                Record Count:<%=grdpaidbills.RecordCount%>| Page Count:
                                                                <gridpagination:XGridPaginationDropDown ID="con1" runat="server">
                                                                </gridpagination:XGridPaginationDropDown>
                                                                <asp:LinkButton ID="lnkExportToExcel1" runat="server" OnClick="lnkExportTOExcel_onclick"
                                                                    Text="Export TO Excel">
                                            <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <xgrid:XGridViewControl ID="grdpaidbills" runat="server" Height="148px" Width="100%"
                                                    CssClass="mGrid" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="100"
                                                    DataKeyNames=" SZ_PROC_CODE,SZ_CASE_ID,SZ_PATIENT_ID,PATIENT_NAME,DT_DATE_OF_SERVICE,I_EVENT_ID,I_EVENT_PROC_ID,SZ_DOCTOR_ID,CASE_NO,DT_EVENT_DATE,SZ_PROCEDURE_GROUP_ID,SZ_CODE_DESCRIPTION,sz_procedure_group"
                                                    XGridKey="Paper_Authorization_Desk" GridLines="None" AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                                    ExportToExcelFields="CASE_NO,PATIENT_NAME,DT_DATE_OF_SERVICE,SZ_CODE,SZ_CODE_DESCRIPTION"
                                                    ExportToExcelColumnNames="Case #,Patient Name,Date of Service,Procedure Code,Description"
                                                    ShowExcelTableBorder="true" ExcelFileNamePrefix="DoctorChange" HeaderStyle-CssClass="GridViewHeader"
                                                    ContextMenuID="ContextMenu1" EnableRowClick="false" MouseOverColor="0, 153, 153"
                                                    AutoGenerateColumns="false" OnRowCommand="grdpaidbills_RowCommand" OnRowEditing="grdpaidbills_RowEditing">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <%--0--%>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" Text=" " />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--1--%>
                                                        <asp:BoundField DataField="CHART_NO" HeaderText="Chart#" SortExpression="MST_PATIENT.I_RFO_CHART_NO">
                                                        </asp:BoundField>
                                                        <%--2--%>
                                                        <%-- <asp:BoundField DataField="CASE_NO" HeaderText="Case#" SortExpression="convert(int,MST_CASE_MASTER.SZ_CASE_NO)" ></asp:BoundField>--%>
                                                        <asp:TemplateField HeaderText="Case#" Visible="true" SortExpression="convert(int,MST_CASE_MASTER.SZ_CASE_NO)">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkCaseno" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CASE_NO")%>'
                                                                    Visible="true" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="CaseNo"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--3--%>
                                                        <asp:BoundField DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" Visible="false"></asp:BoundField>
                                                        <%--4--%>
                                                        <asp:BoundField DataField="SZ_PATIENT_ID" HeaderText="SZ_PATIENT_ID" Visible="false"
                                                            SortExpression=""></asp:BoundField>
                                                        <%--5--%>
                                                        <asp:BoundField DataField="PATIENT_NAME" HeaderText="PATIENT_NAME" Visible="false">
                                                        </asp:BoundField>
                                                        <%--6--%>
                                                        <asp:BoundField DataField="DT_DATE_OF_SERVICE" HeaderText="DT_DATE_OF_SERVICE" Visible="false"
                                                            DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                                                        <%--7--%>
                                                        <%--      <asp:TemplateField HeaderText="" SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME"  >
                                                    <headertemplate  >
                                                           <asp:LinkButton ID="lnkPatientNameSearch" runat="server" CommandName="PATIENT_NAME" CommandArgument="PATIENT_NAME" Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </headertemplate>
                                                    <itemtemplate>
                                                         <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>' Visible="true" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  CommandName="workarea"></asp:LinkButton>   
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                        --%>
                                                        <asp:TemplateField HeaderText="Patient Name" Visible="true" SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>'
                                                                    Visible="true" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="workarea"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--8--%>
                                                        <asp:TemplateField HeaderText="Date Of Service" Visible="true" SortExpression="TXN_CALENDAR_EVENT.DT_EVENT_DATE">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSelectdate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DT_DATE_OF_SERVICE")%>'
                                                                    Visible="true" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="appointment"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText=""  SortExpression="convert(nvarchar,TXN_CALENDAR_EVENT.DT_EVENT_DATE,106)">
                                                    <headertemplate>
                                                           <asp:LinkButton ID="lnkdateservice" runat="server" CommandName="DT_DATE_OF_SERVICE" CommandArgument="DT_DATE_OF_SERVICE" Font-Bold="true" Font-Size="12px">Date Of Service</asp:LinkButton>
                                                    </headertemplate>
                                                    <itemtemplate>
                                                         <asp:LinkButton ID="lnkSelectdate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DT_DATE_OF_SERVICE")%>' Visible="true" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  CommandName="appointment" ></asp:LinkButton>   
                                                    </itemtemplate>
                                                </asp:TemplateField>--%>
                                                        <%--9--%>
                                                        <asp:BoundField DataField="SZ_CODE" HeaderText="Procedure code" SortExpression=" MST_BILL_PROC_TYPE.SZ_TYPE_CODE">
                                                        </asp:BoundField>
                                                        <%--10--%>
                                                        <asp:BoundField DataField="SZ_CODE_DESCRIPTION" HeaderText="Description" SortExpression="mst_bill_proc_type.SZ_TYPE_DESCRIPTION">
                                                        </asp:BoundField>
                                                        <%--11--%>
                                                        <asp:BoundField DataField="SZ_STATUS" HeaderText="Status"></asp:BoundField>
                                                        <%--12--%>
                                                        <asp:BoundField DataField="SZ_COMPANY_ID" HeaderText="CompanyID" Visible="false">
                                                        </asp:BoundField>
                                                        <%--13--%>
                                                        <asp:BoundField DataField="I_EVENT_PROC_ID" HeaderText="EventProcID" Visible="false">
                                                        </asp:BoundField>
                                                        <%--14--%>
                                                        <asp:BoundField DataField="SZ_PROC_CODE" HeaderText="Pro Code" Visible="false"></asp:BoundField>
                                                        <%--15--%>
                                                        <asp:BoundField DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                        </asp:BoundField>
                                                        <%--16--%>
                                                        <asp:BoundField DataField="CASE_NO" HeaderText="Case No." Visible="false"></asp:BoundField>
                                                        <%--17--%>
                                                        <asp:BoundField DataField="DT_EVENT_DATE" HeaderText="Case No." Visible="false">
                                                        </asp:BoundField>
                                                        <%--18--%>
                                                        <asp:BoundField DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID"
                                                            Visible="false"></asp:BoundField>
                                                        <%--19--%>
                                                        <asp:BoundField DataField="sz_procedure_group" HeaderText="sz_procedure_group" Visible="false">
                                                        </asp:BoundField>
                                                        <%--20--%>
                                                        <asp:BoundField DataField="I_EVENT_ID" HeaderText="sz_procedure_group" Visible="false">
                                                        </asp:BoundField>
                                                        <%--21--%>
                                                        <asp:BoundField DataField="SZ_LHR_CODE" HeaderText="LHR Code" Visible="true" SortExpression="(select isnull(sz_remote_procedure_desc,'''') from txn_visit_document where i_Event_id=txn_calendar_event.i_event_id and i_event_proc_id=txn_calender_event_prpcedure.i_event_proc_id)">
                                                        </asp:BoundField>
                                                        <%--22--%>
                                                        <%--<asp:TemplateField HeaderText="">
                                                    <itemtemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text='Test Data' CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Edit"></asp:LinkButton>
                                                        </itemtemplate>
                                                </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <a target="_self" style="text-decoration: underline; cursor: hand;" onclick="showEditPopup('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.I_EVENT_PROC_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_PROCEDURE_GROUP_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>','<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID")%>','<%# DataBinder.Eval(Container,"DataItem.sz_procedure_group")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_CODE_DESCRIPTION")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_CODE")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_TYPE_NAME")%>', '<%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>', '<%# DataBinder.Eval(Container,"DataItem.DT_DATE_OF_SERVICE")%>', '<%# DataBinder.Eval(Container,"DataItem.SZ_LHR_CODE")%>', '<%# DataBinder.Eval(Container,"DataItem.CASE_NO")%>', '<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID")%>', '<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>', '<%# DataBinder.Eval(Container,"DataItem.SZ_READING_DOCTOR_ID")%>')">
                                                                    Test Data</a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--23--%>
                                                        <asp:BoundField DataField="I_NO_OF_DAYS" HeaderText="No. of Days" Visible="true"
                                                            SortExpression="(DATEDIFF(dd,txn_calendar_event.dt_event_date,getdate())+1)">
                                                        </asp:BoundField>
                                                        <%--24--%>
                                                        <asp:BoundField DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" Visible="true"
                                                            SortExpression="(select isnull( SZ_CASE_TYPE_NAME,'''') from MST_CASE_TYPE where SZ_CASE_TYPE_ID in(select SZ_CASE_TYPE_ID from  MST_CASE_MASTER where SZ_CASE_ID= TXN_CALENDAR_EVENT.SZ_CASE_ID))">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DT_IMPORT_DATE" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Import Date"
                                                            Visible="true" SortExpression="TXN_CALENDER_EVENT_PRPCEDURE.DT_IMPORT_DATE" HeaderStyle-Width="8%">
                                                        </asp:BoundField>
                                                         <%--26--%>
                                                        <asp:BoundField DataField="SZ_READING_DOCTOR_ID" HeaderText="SZ_READING_DOCTOR_ID" Visible="false">
                                                        </asp:BoundField>
                                                          <%--27--%>
                                                          <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                           SortExpression="(SELECT CASE WHEN @SZ_COMPANY_ID='CO000000000000000081' THEN (SELECT ISNULL(SZ_INSURANCE_NAME,'') FROM MST_INSURANCE_COMPANY MIC WHERE MIC.SZ_INSURANCE_ID=TXN_CALENDaR_EVENT.SZ_INSURANCE_NAME_ID)
							                                 ELSE	(SELECT ISNULL(SZ_INSURANCE_NAME,'') FROM MST_INSURANCE_COMPANY MIC WHERE MIC.SZ_INSURANCE_ID=MST_CASE_MASTER.SZ_INSURANCE_ID)
							                                  END  )" headertext="Insurance" DataField="SZ_INSURANCE_NAME">
                                                          </asp:BoundField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divid1" style="position: absolute; width: 600px; height: 400px; background-color: #DBE6FA;
                                        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                                        border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                        <div style="position: relative; text-align: right; background-color: #8babe4; width: 600px;">
                                            <asp:Button ID="txtUpdate1" Text="X" BackColor="#8babe4" BorderStyle="None" runat="server"
                                                OnClick="txtUpdate_Click" />
                                        </div>
                                        <iframe id="frameeditProc" src="" frameborder="0" height="400px" width="600px"></iframe>
                                    </div>
                                    <div id="divid2" style="position: absolute; width: 600px; height: 350px; background-color: #DBE6FA;
                                        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                                        border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                        <div style="position: relative; text-align: right; background-color: #8babe4; width: 600px;">
                                            <asp:Button ID="txtUpdate2" Text="X" BackColor="#8babe4" BorderStyle="None" runat="server"
                                                OnClick="txtUpdate_Click" />
                                        </div>
                                        <iframe id="frm" src="" frameborder="0" height="350px" width="600px"></iframe>
                                    </div>
                                    <div id="divid" style="position: absolute; width: 600px; height: 600px; background-color: #DBE6FA;
                                        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                                        border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                        <div style="position: relative; text-align: right; background-color: #8babe4; width: 600px;">
                                            <asp:Button ID="txtUpdate3" Text="X" BackColor="#8babe4" BorderStyle="None" runat="server"
                                                OnClick="txtUpdate_Click" />
                                        </div>
                                        <iframe id="frameeditexpanse" src="" frameborder="0" height="600px" width="600px">
                                        </iframe>
                                    </div>
                                    <div id="divid4" style="position: absolute; width: 900px; height: 530px; background-color: #DBE6FA;
                                        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                                        border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                        <div style="position: relative; background-color: #B5DF82; width: 900px; text-align: center">
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label2" runat="server" Text="Patient Name -" ForeColor="red" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                        <asp:Label ID="lblPatientNameEditALL" runat="server" ForeColor="red" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>&nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;
                                                        <asp:Label ID="Label3" runat="server" Text="Date -" ForeColor="red" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                        <asp:Label ID="lblDateofServiceEditALL" runat="server" ForeColor="red" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>&nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;
                                                        <asp:Label ID="Label4" runat="server" Text="LHR Code -" ForeColor="red" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                        <asp:Label ID="lblLHRCodeEditALL" runat="server" ForeColor="red" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>&nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;
                                                        <asp:Label ID="Label1" runat="server" Text="Case Type -" ForeColor="red" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                        <asp:Label ID="lblCaseTypeEditALL" runat="server" ForeColor="red" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>&nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;
                                                        <asp:Label ID="Label5" runat="server" Text="Case # -" ForeColor="red" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                        <asp:Label ID="lblCasenoEditALL" runat="server" ForeColor="red" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Button ID="txtUpdate4" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
                                                            OnClick="txtUpdate_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <iframe id="frameeditexpanse1" src="" frameborder="0" height="530px" width="900px">
                                        </iframe>
                                    </div>
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr id="PaidBills" runat="server" visible="false">
                        <td style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <asp:UpdatePanel ID="BillsUpdatePanel" runat="server">
                                <contenttemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                        <tr>
                                            <td>
                                                <table style="vertical-align: middle; width: 100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 50%">
                                                                <asp:GridView ID="BillsSum" CssClass="mGrid" AutoGenerateColumns="false" runat="server">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="bill_amount" DataFormatString="{0:c}" HeaderText="Bill Amount">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="paid_amount" DataFormatString="{0:c}" HeaderText="Paid Amount">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="balance" DataFormatString="{0:c}" HeaderText="Balance">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                            <td style="width: 50%" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: middle; width: 30%" id="Td2" runat="server" align="left">
                                                                Search:&nbsp;
                                                                <gridsearch:XGridSearchTextBox ID="XGridSearchTextBoxUnPaid" runat="server" AutoPostBack="true"
                                                                    CssClass="search-input">
                                                                </gridsearch:XGridSearchTextBox>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 70%; text-align: right" id="Td3" runat="server"
                                                                align="right" colspan="2">
                                                                Record Count:<%=grdBills.RecordCount%>| Page Count:
                                                                <gridpagination:XGridPaginationDropDown ID="XGridPaginationDropDownUnPaid" runat="server">
                                                                </gridpagination:XGridPaginationDropDown>
                                                                <asp:LinkButton ID="lnkExcelUnPaid" runat="server" OnClick="lnkExcelUnPaid_onclick"
                                                                    Text="Export TO Excel">
                                            <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <xgrid:XGridViewControl ID="grdBills" runat="server" Height="148px" Width="100%"
                                                    CssClass="mGrid" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="50"
                                                    XGridKey="Bill_Sys_Paid_Bills_PaidUnPaid" GridLines="None" AllowPaging="true"
                                                    AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="sz_bill_number,SZ_CASE_NO"
                                                    ExportToExcelColumnNames="Case No" ShowExcelTableBorder="true" ExcelFileNamePrefix="DoctorChange"
                                                    HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                    MouseOverColor="0, 153, 153" AutoGenerateColumns="false" DataKeyNames="sz_case_id,sz_case_no,sz_bill_number"
                                                    OnRowCommand="grdBills_RowCommand" OnRowEditing="grdBills_RowEditing" OnRowDataBound="grdBills_RowBound">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <%--0--%>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkBulkPayment" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--1--%>
                                                        <asp:TemplateField HeaderText="Bill Number" SortExpression="SZ_BILL_NUMBER">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSelectBill" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--2--%>
                                                        <asp:TemplateField HeaderText="Case #" SortExpression="Convert(int,(SELECT SZ_CASE_NO FROM MST_CASE_MASTER WHERE SZ_CASE_ID = TXN_BILL_TRANSACTIONS.SZ_CASE_ID))">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--3--%>
                                                        <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundField>
                                                        <%--4--%>
                                                        <asp:TemplateField HeaderText="Chart No" Visible="true">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--5--%>
                                                        <asp:TemplateField HeaderText="Patient Name" SortExpression="(MST_PATIENT.SZ_PATIENT_FIRST_NAME)"
                                                            Visible="true">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--6--%>
                                                        <asp:TemplateField HeaderText="Insurance Name" SortExpression="MST_INSURANCE_COMPANY.SZ_INSURANCE_NAME"
                                                            Visible="true">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval( Container, "DataItem.SZ_INSURANCE_NAME") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--7--%>
                                                        <asp:BoundField DataField="SZ_CASE_ID" HeaderText="CaseID" Visible="False"></asp:BoundField>
                                                        <%--8--%>
                                                        <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill Id" Visible="False">
                                                        </asp:BoundField>
                                                        <%--9--%>
                                                        <asp:TemplateField HeaderText="Bill Date" SortExpression="DT_BILL_DATE">
                                                            <ItemTemplate>
                                                                <%#DataBinder.Eval(Container, "DataItem.DT_BILL_DATE","{0:dd MMM yyyy}")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--10--%>
                                                        <asp:BoundField DataField="FLT_WRITE_OFF" HeaderText="Write Off" DataFormatString="{0:0.00}"
                                                            Visible="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <%--11--%>
                                                        <asp:BoundField DataField="BIT_PAID" HeaderText="Paid" Visible="False"></asp:BoundField>
                                                        <%--12--%>
                                                        <asp:TemplateField HeaderText="Bill Amount" SortExpression="ISNULL(FLT_BILL_AMOUNT,0)">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT","{0:c}")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--13--%>
                                                        <asp:TemplateField HeaderText="Paid Amount" SortExpression="ISNULL(mn_paid,0)">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.PAID_AMOUNT", "{0:c}")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--14--%>
                                                        <asp:TemplateField HeaderText="Balance" SortExpression="ISNULL(TXN_BILL_TRANSACTIONS.FLT_BALANCE,0)">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.FLT_BALANCE", "{0:c}")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--15--%>
                                                        <asp:TemplateField HeaderText="Make Payment" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                    CommandName="Make Payment"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <%--16--%>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemTemplate>
                                                                <a href="Bill_Sys_PatientInformation.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                                    target="_blank">Edit W.C. 4.0</a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <%--17--%>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemTemplate>
                                                                <a href="Bill_Sys_PatientInformationC4_2.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                                    target="_blank">Edit W.C. 4.2</a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <%--18--%>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemTemplate>
                                                                <a href="Bill_Sys_PatientInformationC4_3.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                                    target="_blank">Edit W.C. 4.3</a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <%--19--%>
                                                        <asp:TemplateField HeaderText="Generate bill" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                    CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <%--20--%>
                                                        <asp:TemplateField HeaderText="Delete" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--21--%>
                                                        <asp:BoundField DataField="I_PAYMENT_STATE" HeaderText="COMMENT" Visible="False">
                                                        </asp:BoundField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </td>
                                        </tr>
                                    </table>
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<div id="divid1" style="position: absolute; width: 400px; height: 600px; background-color: #DBE6FA;
                                visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                                border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                <div style="position: relative; text-align: right; background-color: #8babe4; width: 600px;">
                                    <a onclick="CloseEditProcPopup()"
                                        style="cursor: pointer;" title="Close">X</a>
                                </div>
                                <iframe id="frameeditProc" src="" frameborder="0" height="400px" width="600px"></iframe>
                            </div>--%>
                            <%--<div id="divid2"  style="position: absolute; width: 600px; height: 350px; background-color: #DBE6FA;
                                visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                                border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                <div style="position: relative; text-align: right; background-color: #8babe4; width: 600px;">
                                    <a onclick="CloseDocumentPopup()"
                                        style="cursor: pointer;" title="Close">X</a>
                                </div>
                                <iframe id="frm" src="" frameborder="0" height="350px" width="600px"></iframe>
                            </div>--%>
                            <%--  <div id="divid" style="position: absolute; width: 600px; height: 600px; background-color: #DBE6FA;
                                visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;	
                                border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                <div style="position: relative; text-align: right; background-color: #8babe4; width: 600px;">
                                    <a onclick="document.getElementById('divid').style.visibility='hidden';document.getElementById('divid').style.zIndex = -1;  window.parent.document.location.href='Bill_Sys_paid_bills.aspx?Flag=report&Type=p&popup=done1'; "
                                        style="cursor: pointer;" title="Close">X</a>
                                </div>
                                <iframe id="frameeditexpanse" src="" frameborder="0" height="600px" width="600px"></iframe>
                            </div>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divDashBoard" visible="true" style="position: absolute; width: 800px; height: 475px;
        background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 430; float: left; position: relative;">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 100%" valign="top">
                    <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
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
                            <td style="width: 97%" class="TDPart">
                                <table id="tblMissingSpeciality" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 99%; height: 130px; float: left; position: relative; left: 0px;
                                    top: 0px; vertical-align: top" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Missing Speciality
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <table>
                                                <tr>
                                                    <td>
                                                        You have
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMissingSpecialityText" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%; height: 10px;" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" id="tblDailyAppointment" runat="server" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Today's Appointment
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblWeeklyAppointment" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%">
                                            Weekly &nbsp;Appointment
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart">
                                            <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblBillStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; vertical-align: top; float: left; position: relative;"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Bill Status
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblDesk" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 32%;
                                    height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%;" valign="top">
                                            Desk
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have&nbsp;
                                            <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblMissingInfo" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Missing Information
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%;" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblReportSection" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Report Section
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblReport" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblBilledUnbilledProcCode" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative; left: 0px;
                                    top: 0px;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Procedure Status
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblProcedureStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblVisits" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 32%;
                                    height: 195px; float: left; position: relative; left: 0px; top: 0px;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Visits
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <asp:Label ID="lblVisits" runat="server" Visible="true"></asp:Label>
                                            <table>
                                                <tr>
                                                    <td>
                                                        You have
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ul style="list-style-type: disc; padding-left: 60px;">
                                                            <li><a id="hlnkTotalVisit" href="#" runat="server">
                                                                <asp:Label ID="lblTotalVisit" runat="server"></asp:Label></a>&nbsp;Total Visit</li><li>
                                                                    <a id="hlnkBilledVisit" href="#" runat="server">
                                                                        <asp:Label ID="lblBilledVisit" runat="server"></asp:Label></a>&nbsp;Billed Visit
                                                                </li>
                                                            <li><a id="hlnkUnBilledVisit" href="#" runat="server">
                                                                <asp:Label ID="lblUnBilledVisit" runat="server"></asp:Label></a>&nbsp;UnBilled Visit
                                                            </li>
                                                        </ul>
                                                        <ajaxToolkit:PopupControlExtender ID="PopExTotalVisit" runat="server" TargetControlID="hlnkTotalVisit"
                                                            PopupControlID="pnlTotalVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                        <ajaxToolkit:PopupControlExtender ID="PopExBilledVisit" runat="server" TargetControlID="hlnkBilledVisit"
                                                            PopupControlID="pnlBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                        <ajaxToolkit:PopupControlExtender ID="PopExUnBilledVisit" runat="server" TargetControlID="hlnkUnBilledVisit"
                                                            PopupControlID="pnlUnBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblPatientVisitStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 33%; height: 195px; float: left; position: relative; left: 0px;
                                    top: 0px; vertical-align: top" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Patient Visit Status
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblPatientVisitStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
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
    </div>
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
            visible="false"></iframe>
    </div>
    <asp:Panel ID="pnlTotalVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdTotalVisit" runat="server" Width="25px" CssClass="GridTable"
                        AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlUnBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdUnVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="upCaseNO" runat="server">
        <contenttemplate>
            <ajaxToolkit:ModalPopupExtender ID="mpCaseNo" runat="server" TargetControlID="lbn_job_det"
                DropShadow="false" PopupControlID="pnlCaseNo" BehaviorID="modal1" PopupDragHandleControlID="Div2">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel Style="display: none; width: 450px; height: 300px; background-color: white;
                border: 1px solid #B5DF82;" ID="pnlCaseNo" runat="server">
                <table width="100%">
                    <tr>
                        <td>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div style="left: 0px; width: 450px; position: absolute; top: 0px; height: 18px;
                                                background-color: #B5DF82; text-align: left" id="Div2">
                                                <b>Documents</b>
                                                <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #B5DF82;
                                                    border: 1px solid #B5DF82;">
                                                    <asp:Button ID="btnbillnotesclose" runat="server" Height="19px" Width="30px" Text="X"
                                                        OnClientClick="$find('modal1').hide(); return false;"></asp:Button>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <UserMessage:MessageControl runat="server" ID="MessageControl1"></UserMessage:MessageControl>
                                                    <asp:UpdateProgress ID="UpdatePanel19" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="upCaseNO">
                                                        <ProgressTemplate>
                                                            <div id="Div3" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                runat="Server">
                                                                <asp:Image ID="img46" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                    Height="25px" Width="24px"></asp:Image>
                                                                Loading...</div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; height: 25px;" colspan="2">
                                            <asp:UpdatePanel ID="UpdatePanel101" runat="server">
                                                <ContentTemplate>
                                                    <UserMessage:MessageControl runat="server" ID="msg1" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <extddl:ExtendedDropDownList ID="extddlSpecialty" runat="server" Connection_Key="Connection_String"
                                                Flag_Key_Value="GET_SPECIALTY" AutoPost_back="true" OnextendDropDown_SelectedIndexChanged="extddlSpecialty_SelectedIndexChanged"
                                                Procedure_Name="SP_MST_SPECIALTY_LHR" Selected_Text="---Select---" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="width: 80%">
                                            <div style="overflow: scroll; height: 155px;">
                                                <asp:DataGrid ID="grdViewDocuments" Width="100%" runat="Server" OnItemCommand="grdViewDocuments_ItemCommand"
                                                    AutoGenerateColumns="False" CssClass="GridTable">
                                                    <HeaderStyle CssClass="GridHeader1" />
                                                    <ItemStyle CssClass="GridViewHeader" />
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectCaseAll" runat="server" onclick="javascript:SelectCaseAll(this.checked);"
                                                                    ToolTip="Select All" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkView" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="File Name" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LKBView" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Filename")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="FilePath" HeaderText="Filepath" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Filename" HeaderText="Filename" Visible="false"></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlreport" runat="server" Width="100px">
                                                                    <asp:ListItem Value="8" Selected="true">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="0">Report</asp:ListItem>
                                                                    <asp:ListItem Value="1">Referral</asp:ListItem>
                                                                    <asp:ListItem Value="2">AOB</asp:ListItem>
                                                                    <asp:ListItem Value="3">Comp Authorization</asp:ListItem>
                                                                    <asp:ListItem Value="4">HIPPA Consent</asp:ListItem>
                                                                    <asp:ListItem Value="5">Lien Form</asp:ListItem>
                                                                    <asp:ListItem Value="6">Misc</asp:ListItem>
                                                                    <asp:ListItem Value="7">Additional Reports</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnUPdate" runat="server" Text="Save" Width="104px" OnClick="btnUPdate_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="display: none">
                <asp:LinkButton ID="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
        </contenttemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdnSpeciality" runat="server" />
    <asp:TextBox ID="txtCompanyid" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="hdnmdltxtCaseID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="hdnmdltxtProcID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="hdnmdltxtSpeciality" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtireortreceive" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtVisit" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtDiagnosisSetID" runat="server" Visible="false" Width="10px"></asp:TextBox>
    <asp:TextBox ID="txtAOb" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtReport" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtReferral" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtLien" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtComp" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtAdditionalReports" runat="server" Visible="false"></asp:TextBox>
    <div style="visibility: hidden;">
        <asp:Button ID="btnCls" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
            OnClick="txtUpdate_Click" /></div>
    <asp:Image ID="img2" Visible="false" Style="position: absolute; left: 50%; top: 86%"
        runat="server" ImageUrl="~/AJAX Pages/Images/loading_transp.gif" AlternateText="Loading....."
        Height="60px" Width="60px"></asp:Image>
    <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0">
        <progresstemplate>
            <asp:Image ID="img1" runat="server" Style="position: fixed; z-index: 1; left: 50%;
                top: 50%" ImageUrl="~/Ajax Pages/Images/loading_transp.gif" AlternateText="Loading....."
                Height="60px" Width="60px"></asp:Image>
        </progresstemplate>
    </asp:UpdateProgress>
    <dx:ASPxPopupControl ID="TestDataPopup" runat="server" CloseAction="CloseButton"
        CloseButtonImage-ToolTip="Close" Modal="true" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ClientInstanceName="TestDataPopup" HeaderStyle-HorizontalAlign="center"
        HeaderStyle-Font-Bold="true" HeaderStyle-BorderTop-BorderColor="DarkGreen" HeaderStyle-ForeColor="Red"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="900px"
        ToolTip="Test Data Popup" PopupHorizontalOffset="0" PopupVerticalOffset="0"  
        AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="600px">
        <ClientSideEvents CloseButtonClick="ClosePopup" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
        <HeaderStyle BackColor="#B5DF82"></HeaderStyle>
    </dx:ASPxPopupControl>
</asp:Content>
