<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_NewPaymentReport.aspx.cs" Inherits="Bill_Sys_NewPaymentReport"
    Title="Green Your Bills - Payment Report" AsyncTimeout="10000" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
    </asp:ScriptManager>
    <style type="text/css">
        .modalBackground
        {
            background-color: #778899;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
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
        //visit dates
        function SetVisitDate() {
            getWeek();
            var selValue = document.getElementById("<%=ddlvisitdate.ClientID %>").value;
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

        //end
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
            var selValue = document.getElementById("<%= ddlDateValues.ClientID %>").value;
            if (selValue == 0) {
                document.getElementById("<%= txtToDate.ClientID %>").value = "";
                document.getElementById("<%= txtFromDate.ClientID %>").value = "";

            }
            else if (selValue == 1) {
                document.getElementById("<%= txtToDate.ClientID %>").value = getDate('today');
                document.getElementById("<%= txtFromDate.ClientID %>").value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById("<%= txtToDate.ClientID %>").value = getWeek('endweek');
                document.getElementById("<%= txtFromDate.ClientID %>").value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById("<%= txtToDate.ClientID %>").value = getDate('monthend');
                document.getElementById("<%= txtFromDate.ClientID %>").value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById("<%= txtToDate.ClientID %>").value = getDate('quarterend');
                document.getElementById("<%= txtFromDate.ClientID %>").value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById("<%= txtToDate.ClientID %>").value = getDate('yearend');
                document.getElementById("<%= txtFromDate.ClientID %>").value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById("<%= txtToDate.ClientID %>").value = getLastWeek('endweek');
                document.getElementById("<%= txtFromDate.ClientID %>").value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById("<%= txtToDate.ClientID %>").value = lastmonth('endmonth');

                document.getElementById("<%= txtFromDate.ClientID %>").value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById("<%= txtToDate.ClientID %>").value = lastyear('endyear');

                document.getElementById("<%= txtFromDate.ClientID %>").value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById("<%= txtToDate.ClientID %>").value = quarteryear('endquaeter');

                document.getElementById("<%= txtFromDate.ClientID %>").value = quarteryear('startquaeter');
            }
        }

        function SetCheckDate() {
            getWeek();
            var selValue = document.getElementById("<%= drpcheckdate.ClientID %>").value;
            if (selValue == 0) {
                document.getElementById("<%= txtChkFromDate.ClientID %>").value = "";
                document.getElementById("<%= txtChkToDate.ClientID %>").value = "";

            }
            else if (selValue == 1) {
                document.getElementById("<%= txtChkToDate.ClientID %>").value = getDate('today');
                document.getElementById("<%= txtChkFromDate.ClientID %>").value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById("<%= txtChkToDate.ClientID %>").value = getWeek('endweek');
                document.getElementById("<%= txtChkFromDate.ClientID %>").value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById("<%= txtChkToDate.ClientID %>").value = getDate('monthend');
                document.getElementById("<%= txtChkFromDate.ClientID %>").value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById("<%= txtChkToDate.ClientID %>").value = getDate('quarterend');
                document.getElementById("<%= txtChkFromDate.ClientID %>").value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById("<%= txtChkToDate.ClientID %>").value = getDate('yearend');
                document.getElementById("<%= txtChkFromDate.ClientID %>").value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById("<%= txtChkToDate.ClientID %>").value = getLastWeek('endweek');
                document.getElementById("<%= txtChkFromDate.ClientID %>").value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById("<%= txtChkToDate.ClientID %>").value = lastmonth('endmonth');

                document.getElementById("<%= txtChkFromDate.ClientID %>").value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById("<%= txtChkToDate.ClientID %>").value = lastyear('endyear');

                document.getElementById("<%= txtChkFromDate.ClientID %>").value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById("<%= txtChkToDate.ClientID %>").value = quarteryear('endquaeter');

                document.getElementById("<%= txtChkFromDate.ClientID %>").value = quarteryear('startquaeter');
            }
        }

        function SetPaymentDate() {
            getWeek();
            var selValue = document.getElementById("<%= ddlPaymentDateValues.ClientID %>").value;
            if (selValue == 0) {
                document.getElementById("<%= txtPayFromDate.ClientID %>").value = "";
                document.getElementById("<%= txtPayToDate.ClientID %>").value = "";

            }
            else if (selValue == 1) {
                document.getElementById("<%= txtPayToDate.ClientID %>").value = getDate('today');
                document.getElementById("<%= txtPayFromDate.ClientID %>").value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById("<%= txtPayToDate.ClientID %>").value = getWeek('endweek');
                document.getElementById("<%= txtPayFromDate.ClientID %>").value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById("<%= txtPayToDate.ClientID %>").value = getDate('monthend');
                document.getElementById("<%= txtPayFromDate.ClientID %>").value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById("<%= txtPayToDate.ClientID %>").value = getDate('quarterend');
                document.getElementById("<%= txtPayFromDate.ClientID %>").value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById("<%= txtPayToDate.ClientID %>").value = getDate('yearend');
                document.getElementById("<%= txtPayFromDate.ClientID %>").value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById("<%= txtPayToDate.ClientID %>").value = getLastWeek('endweek');
                document.getElementById("<%= txtPayFromDate.ClientID %>").value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById("<%= txtPayToDate.ClientID %>").value = lastmonth('endmonth');

                document.getElementById("<%= txtPayFromDate.ClientID %>").value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById("<%= txtPayToDate.ClientID %>").value = lastyear('endyear');

                document.getElementById("<%= txtPayFromDate.ClientID %>").value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById("<%= txtPayToDate.ClientID %>").value = quarteryear('endquaeter');

                document.getElementById("<%= txtPayFromDate.ClientID %>").value = quarteryear('startquaeter');
            }
        }
        // Start :   Download Date function 



        // End
         
    </script>
    <script type="text/javascript" language="javascript">
        function FromDateValidation() {
            var year1 = "";
            year1 = document.getElementById("<%=txtFromDate.ClientID%>").value;

            //alert(year1);
            // var aryyear = new Array();
            //alert(year1.charAt(6)+"     "+year1.charAt(6));
            //if(year.indexOf)
            //alert(year1);

            if (year1.charAt(0) == '_' && year1.charAt(1) == '_' && year1.charAt(2) == '/' && year1.charAt(3) == '_' && year1.charAt(4) == '_' && year1.charAt(5) == '/' && year1.charAt(6) == '_' && year1.charAt(7) == '_' && year1.charAt(8) == '_' && year1.charAt(9) == '_') {
                return true;
            }


            if ((year1.charAt(6) == '_' && year1.charAt(7) == '_') || (year1.charAt(8) == '_' && year1.charAt(9) == '_') || (year1.charAt(6) == '0' && year1.charAt(7) == '0') || (year1.charAt(6) == '0') || (year1.charAt(9) == '_')) {
                //alert("Invalide 'From Date'");


                //document.getElementById('_ctl0_ContentPlaceHolder1_MaskedEditValidator1').innerText = 'Invalide F Date';
                //document.getElementById('_ctl0_ContentPlaceHolder1_MaskedEditValidator1').style.visibility = 'visible';

                document.getElementById("<%= lblValidator1.ClientID %>").innerText = 'Date is Invalid';

                // document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').style.display = "block";

                document.getElementById("<%=txtFromDate.ClientID%>").focus();
                return false;
            }
            if ((year1.charAt(6) != '_' && year1.charAt(7) != '_') || (year1.charAt(8) != '_' && year1.charAt(9) != '_') || (year1.charAt(6) != '0' && year1.charAt(7) != '0')) {

                document.getElementById("<%= lblValidator1.ClientID %>").innerText = '';
                return true;
            }

        }

        function ToDateValidation() {
            var year2 = "";
            year2 = document.getElementById("<%=txtToDate.ClientID%>").value;

            if (year2.charAt(0) == '_' && year2.charAt(1) == '_' && year2.charAt(2) == '/' && year2.charAt(3) == '_' && year2.charAt(4) == '_' && year2.charAt(5) == '/' && year2.charAt(6) == '_' && year2.charAt(7) == '_' && year2.charAt(8) == '_' && year2.charAt(9) == '_') {
                return true;
            }
            if ((year2.charAt(6) == '_' && year2.charAt(7) == '_') || (year2.charAt(8) == '_' && year2.charAt(9) == '_') || (year2.charAt(6) == '0' && year2.charAt(7) == '0') || (year2.charAt(6) == '0') || (year2.charAt(9) == '_')) {
                //alert("Invalide 'To Date'");
                document.getElementById("<%=lblValidator2.ClientID%>").innerText = 'Date is Invalid';
                document.getElementById("<%=txtToDate.ClientID%>").focus();
                return false;
            }
            if ((year2.charAt(6) != '_' && year2.charAt(7) != '_') || (year2.charAt(8) != '_' && year2.charAt(9) != '_') || (year2.charAt(6) != '0' && year2.charAt(7) != '0')) {
                document.getElementById("<%=lblValidator2.ClientID%>").innerText = '';
                return true;
            }

        }


        function CheckFromDateValidation() {
            var year1 = "";
            year1 = document.getElementById("<%=txtChkFromDate.ClientID%>").value;

            //alert(year1);
            // var aryyear = new Array();
            //alert(year1.charAt(6)+"     "+year1.charAt(6));
            //if(year.indexOf)
            //alert(year1);

            if (year1.charAt(0) == '_' && year1.charAt(1) == '_' && year1.charAt(2) == '/' && year1.charAt(3) == '_' && year1.charAt(4) == '_' && year1.charAt(5) == '/' && year1.charAt(6) == '_' && year1.charAt(7) == '_' && year1.charAt(8) == '_' && year1.charAt(9) == '_') {
                return true;
            }


            if ((year1.charAt(6) == '_' && year1.charAt(7) == '_') || (year1.charAt(8) == '_' && year1.charAt(9) == '_') || (year1.charAt(6) == '0' && year1.charAt(7) == '0') || (year1.charAt(6) == '0') || (year1.charAt(9) == '_')) {
                //alert("Invalide 'From Date'");


                //document.getElementById('_ctl0_ContentPlaceHolder1_MaskedEditValidator1').innerText = 'Invalide F Date';
                //document.getElementById('_ctl0_ContentPlaceHolder1_MaskedEditValidator1').style.visibility = 'visible';

                document.getElementById("<%=lblValidator3.ClientID%>").innerText = 'Date is Invalid';

                // document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').style.display = "block";

                document.getElementById("<%=txtChkFromDate.ClientID%>").focus();
                return false;
            }
            if ((year1.charAt(6) != '_' && year1.charAt(7) != '_') || (year1.charAt(8) != '_' && year1.charAt(9) != '_') || (year1.charAt(6) != '0' && year1.charAt(7) != '0')) {
                document.getElementById("<%=lblValidator3.ClientID%>").innerText = '';
                return true;
            }

        }

        function CheckToDateValidation() {
            var year2 = "";
            year2 = document.getElementById("<%=txtChkToDate.ClientID%>").value;

            if (year2.charAt(0) == '_' && year2.charAt(1) == '_' && year2.charAt(2) == '/' && year2.charAt(3) == '_' && year2.charAt(4) == '_' && year2.charAt(5) == '/' && year2.charAt(6) == '_' && year2.charAt(7) == '_' && year2.charAt(8) == '_' && year2.charAt(9) == '_') {
                return true;
            }
            if ((year2.charAt(6) == '_' && year2.charAt(7) == '_') || (year2.charAt(8) == '_' && year2.charAt(9) == '_') || (year2.charAt(6) == '0' && year2.charAt(7) == '0') || (year2.charAt(6) == '0') || (year2.charAt(9) == '_')) {
                //alert("Invalide 'To Date'");
                document.getElementById("<%=lblValidator4.ClientID%>").innerText = 'Date is Invalid';
                document.getElementById("<%=txtChkToDate.ClientID%>").focus();
                return false;
            }
            if ((year2.charAt(6) != '_' && year2.charAt(7) != '_') || (year2.charAt(8) != '_' && year2.charAt(9) != '_') || (year2.charAt(6) != '0' && year2.charAt(7) != '0')) {
                document.getElementById("<%=lblValidator4.ClientID%>").innerText = '';
                return true;
            }

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        function InitializeRequest(sender, args) {

            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);
            postBackElement = args.get_postBackElement();
            //  if (postBackElement.id == 'ctl00_ContentPlaceHolder1_btnSearch') 
            if (postBackElement.id == "<%=btnSearch.ClientID %>")
            //$get('ctl00_ContentPlaceHolder1_UpdateProgress1').style.display = 'block'; 
                $get("<% =UpdateProgress1.ClientID%>").style.display = 'block';
        }



        function EndRequest(sender, args) {
            //if (postBackElement.id == 'ctl00_ContentPlaceHolder1_btnSearch') 
            if (postBackElement.id == "<%=btnSearch.ClientID %>")
            //$get('ctl00_ContentPlaceHolder1_UpdateProgress1').style.display = 'none'; 
                $get("<%=UpdateProgress1.ClientID%>").style.display = 'none';
        } 

      
    </script>
    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, <%=btnSearch.ClientID %>)">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%">
            <tr>
                <td colspan="3" align="center">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updtpanel"
                        DisplayAfter="10" DynamicLayout="true">
                        <ProgressTemplate>
                            <div id="DivStatus2" runat="Server">
                                <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                    Height="25px" Width="24px"></asp:Image>
                                Loading...</div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                        DisplayAfter="10" DynamicLayout="true">
                        <ProgressTemplate>
                            <div id="DivStatus112" runat="Server">
                                <asp:Image ID="img112" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                    Height="25px" Width="24px"></asp:Image>
                                Loading...</div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 100%; vertical-align: top;">
                    <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
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
                            <td class="Center" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                                <tr>
                                                    <td colspan="3">
                                                        <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 50%;
                                                            height: 50%; border: 1px solid #8babe4;" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
                                                            <tr>
                                                                <td height="28" align="left" valign="middle" bgcolor="#8babe4" class="txt2" colspan="3">
                                                                    <b class="txt3">Payment Report</b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Bill
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    From Date
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    To Date
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:DropDownList ID="ddlDateValues" runat="Server" Width="98%">
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
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                        CssClass="text-box" MaxLength="10" Width="80%">
                                                                    </asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                                        PopupButtonID="imgbtnFromDate" />
                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>--%>
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                                        MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDateofService" runat="server" TargetControlID="txtFromDate"
                                                                        PopupButtonID="imgbtnFromDate" />
                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                        ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                        CssClass="text-box" MaxLength="10" Width="80%">
                                                                    </asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <asp:Label ID="lblValidator2" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                                    <%--  <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate" PopupButtonID="imgbtnToDate" />
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                                    --%>
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                                        MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                                        PopupButtonID="imgbtnToDate" />
                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                                        ControlToValidate="txtToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="True" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Check
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Check From Date
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Check To Date
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:DropDownList ID="drpcheckdate" runat="Server" Width="98%">
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
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:TextBox ID="txtChkFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                        CssClass="text-box" MaxLength="10" Width="80%">
                                                                    </asp:TextBox>
                                                                    <asp:ImageButton ID="imgChkFromDate" runat="Server" ImageUrl="~/Images/cal.gif" />
                                                                    <asp:Label ID="lblValidator3" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                                    <ajaxToolkit:CalendarExtender ID="calExtChkFromDate" runat="server" TargetControlID="txtChkFromDate"
                                                                        PopupButtonID="imgChkFromDate">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                                        MaskType="Date" TargetControlID="txtChkFromDate" PromptCharacter="_" AutoComplete="true">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
                                                                        ControlToValidate="txtChkFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="True" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator>
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:TextBox ID="txtChkToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                        CssClass="text-box" MaxLength="10" Width="80%">
                                                                    </asp:TextBox>
                                                                    <asp:ImageButton ID="imgChkToDate" runat="Server" ImageUrl="~/Images/cal.gif" />
                                                                    <asp:Label ID="lblValidator4" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                                    <ajaxToolkit:CalendarExtender ID="calExtChkToDate" runat="server" TargetControlID="txtChkToDate"
                                                                        PopupButtonID="imgChkToDate">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                                                                        MaskType="Date" TargetControlID="txtChkToDate" PromptCharacter="_" AutoComplete="true">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender4"
                                                                        ControlToValidate="txtChkToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="True" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Patient Name
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Case No
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    User
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:TextBox ID="txtPatientName" runat="server" Width="90%">
                                                                    </asp:TextBox>
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:TextBox ID="txtCaseNo" runat="server" Width="90%">
                                                                    </asp:TextBox>
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <cc1:ExtendedDropDownList ID="extddlUser" runat="server" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_USERS" Flag_Key_Value="GETUSERLIST" Selected_Text="---Select---"
                                                                        Width="100%"></cc1:ExtendedDropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Check Number
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Check Amount
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Case Type
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:TextBox ID="txtChkNumber" runat="server" Width="90%">
                                                                    </asp:TextBox>
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:TextBox ID="txtChkAmount" runat="server" Width="90%">
                                                                    </asp:TextBox><asp:Label ID="lbldollar" runat="server" Text="$" CssClass="lbl"></asp:Label>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="validtxt" runat="server" TargetControlID="txtChkAmount"
                                                                        ValidChars="1234567890." />
                                                                </td>
                                                                <td>
                                                                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="90%" Selected_Text="---Select---"
                                                                        Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String">
                                                                    </extddl:ExtendedDropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" class="td-widget-bc-search-desc-ch1">
                                                                    Insurance
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    <asp:Label ID="lblLocationName" runat="server" CssClass="td-widget-bc-search-desc-ch1"
                                                                        Text="Location" Visible="False" Width="90%"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="99%" Selected_Text="---Select---"
                                                                        Style="float: left;" Connection_Key="Connection_String" Flag_Key_Value="INSURANCE_LIST"
                                                                        Procedure_Name="SP_MST_INSURANCE_COMPANY"></extddl:ExtendedDropDownList>
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="98%" Connection_Key="Connection_String"
                                                                        Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---"
                                                                        Visible="false" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Payment
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Payment From Date
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                    Payment To Date
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:DropDownList ID="ddlPaymentDateValues" runat="Server" Width="98%">
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
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:TextBox ID="txtPayFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                        CssClass="text-box" MaxLength="10" Width="80%">
                                                                    </asp:TextBox>
                                                                    <asp:ImageButton ID="imgPayFromDate" runat="Server" ImageUrl="~/Images/cal.gif" />
                                                                    <asp:Label ID="lblValidator5" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPayFromDate"
                                                                        PopupButtonID="imgPayFromDate">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                                                                        MaskType="Date" TargetControlID="txtPayFromDate" PromptCharacter="_" AutoComplete="true">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender4"
                                                                        ControlToValidate="txtPayFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="True" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator>
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                    <asp:TextBox ID="txtPayToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                        CssClass="text-box" MaxLength="10" Width="80%">
                                                                    </asp:TextBox>
                                                                    <asp:ImageButton ID="imgPayToDate" runat="Server" ImageUrl="~/Images/cal.gif" />
                                                                    <asp:Label ID="lblValidator6" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPayToDate"
                                                                        PopupButtonID="imgPayToDate">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
                                                                        MaskType="Date" TargetControlID="txtPayToDate" PromptCharacter="_" AutoComplete="true">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedEditExtender4"
                                                                        ControlToValidate="txtPayToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="True" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator>
                                                                </td>
                                                            </tr>
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
                                                                    <asp:DropDownList ID="ddlvisitdate" runat="Server" Width="90%">
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
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtVisitDate"
                                                                        PopupButtonID="imgVisit" />
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch">
                                                                    <asp:TextBox ID="txtToVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                        MaxLength="10" Width="70%"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgVisite1" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtToVisitDate"
                                                                        PopupButtonID="imgVisite1" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-top: 5px" colspan="3">
                                                                    <asp:CheckBox ID="chkReportOnly" Text="Report Only" runat="server" Font-Bold="True"
                                                                        Font-Size="8.5pt" />
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    <asp:CheckBox ID="chkColletion" Text="Collections" runat="server" Font-Bold="True"
                                                                        Font-Size="8.5pt" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align="center">
                                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px">
                                                                    </asp:TextBox>
                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearch_Click1"
                                                                        CssClass="Buttons" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;" class="TDPart" valign="top">
                                            <div style="text-align: right;">
                                                <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                    OnClick="btnExportToExcel_Click" />
                                            </div>
                                            <br />
                                            <asp:UpdatePanel ID="updtpanel" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="grdPayment" runat="Server" AutoGenerateColumns="False" CssClass="GridTable"
                                                        Width="100%" OnItemCommand="grdPayment_ItemCommand">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <%--0--%>
                                                            <asp:BoundColumn DataField="BILL_DATE" HeaderText="Bill Date"></asp:BoundColumn>
                                                            <%--1--%>
                                                            <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                            <%--2--%>
                                                            <asp:TemplateColumn HeaderText="Bill Number">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkBill_No" runat="server" CommandName="BillNo" CommandArgument='<%#Eval("SZ_BILL_NUMBER")+ "," +(((DataGridItem) Container).ItemIndex)%>'
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <%--3--%>
                                                            <asp:BoundColumn DataField="VISIT_DATE" HeaderText="Visit Date"></asp:BoundColumn>
                                                            <%--4--%>
                                                            <asp:BoundColumn DataField="CPT" HeaderText="CPT"></asp:BoundColumn>
                                                            <%--5--%>
                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                            <%--6--%>
                                                             <asp:BoundColumn DataField="PROVIDER_NAME" HeaderText="Provider Name" Visible="true">
                                                            </asp:BoundColumn>

                                                            <%--6=7--%>
                                                            <asp:BoundColumn DataField="CASE_TYPE" HeaderText="Case Type"></asp:BoundColumn>
                                                            <%--7=8--%>
                                                            <asp:BoundColumn DataField="BILLED" HeaderText="Total Billed" ItemStyle-HorizontalAlign="Right">
                                                            </asp:BoundColumn>

                                                              <%--9--%>
                                                               <asp:BoundColumn DataField="specialty" HeaderText="Specialty" Visible="true"></asp:BoundColumn>
                                                            <%--8=10--%>
                                                            <asp:BoundColumn DataField="RECEIVED" HeaderText="Total Received" ItemStyle-HorizontalAlign="Right">
                                                            </asp:BoundColumn>
                                                            <%--9=11--%>
                                                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="Total Outstanding" ItemStyle-HorizontalAlign="Right">
                                                            </asp:BoundColumn>


                                                            <%--10=12--%>
                                                            <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off"></asp:BoundColumn>
                                                            <%--11=13--%>
                                                            <asp:BoundColumn DataField="PaidInPeriod" HeaderText="Paid Amount (Period Range)">
                                                            </asp:BoundColumn>
                                                             <%--14--%>
                                                             <asp:BoundColumn DataField="mn_transferred_amount" HeaderText="Transferred Amount"
                                                                Visible="true"></asp:BoundColumn>
                                                                   <%--15--%>
                                                                  <asp:BoundColumn DataField="dt_transferred_on" HeaderText="Transferred Date" Visible="true">
                                                            </asp:BoundColumn>
                                                             <%--16--%>
                                                               <asp:BoundColumn DataField="SZ_COMPANY_NAME" HeaderText="Transferred Attorney" Visible="true">
                                                            </asp:BoundColumn>
                                                        

                                                            <%--12=17--%>
                                                            <asp:BoundColumn DataField="SZ_COMMENT" HeaderText="Comment"></asp:BoundColumn>
                                                            <%--13=18--%>
                                                            <asp:BoundColumn DataField="SZ_PAYMENT" HeaderText="Last Payment"></asp:BoundColumn>
                                                            <%--14=19--%>
                                                            <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case NO" Visible="false"></asp:BoundColumn>
                                                            <%--15=20--%>
                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Verification" Visible="false">
                                                            </asp:BoundColumn>
                                                            <%--16=21--%>
                                                            <asp:BoundColumn DataField="SZ_DENAIL" HeaderText="Denail" Visible="false"></asp:BoundColumn>
                                                            <%--17=22--%>
                                                            <%-- <asp:BoundColumn DataField="SZ_PATH" HeaderText="View" Visible="true"></asp:BoundColumn>--%>
                                                            <asp:BoundColumn DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance" Visible="true">
                                                            </asp:BoundColumn>
                                                            <%--18=23--%>
                                                            <asp:BoundColumn DataField="SZ_PATH" HeaderText="View" Visible="false"></asp:BoundColumn>
                                                        </Columns>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                    </asp:DataGrid>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="grdPayment2" runat="Server" AutoGenerateColumns="False" CssClass="GridTable"
                                                        Width="100%" OnItemCommand="grdPayment2_ItemCommand">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <%--0--%>
                                                            <asp:BoundColumn DataField="BILL_DATE" HeaderText="Bill Date"></asp:BoundColumn>
                                                            <%--1--%>
                                                            <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                            <%--2--%>
                                                            <asp:TemplateColumn HeaderText="Bill Number">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkBill_No" runat="server" CommandName="BillNo" CommandArgument='<%#Eval("SZ_BILL_NUMBER")+ "," +(((DataGridItem) Container).ItemIndex)%>'
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <%--3--%>
                                                            <asp:BoundColumn DataField="VISIT_DATE" HeaderText="Visit Date"></asp:BoundColumn>
                                                            <%--4--%>
                                                            <asp:BoundColumn DataField="CPT" HeaderText="CPT" Visible="false"></asp:BoundColumn>
                                                            <%--5--%>
                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                              <%--6--%>
                                                             <asp:BoundColumn DataField="PROVIDER_NAME" HeaderText="Provider Name" Visible="true">
                                                            </asp:BoundColumn>

                                                            <%--6=7--%>
                                                            <asp:BoundColumn DataField="CASE_TYPE" HeaderText="Case Type"></asp:BoundColumn>
                                                            <%--7=8--%>
                                                            <asp:BoundColumn DataField="BILLED" HeaderText="Total Billed" ItemStyle-HorizontalAlign="Right">
                                                            </asp:BoundColumn>

                                                              <%--9--%>
                                                               <asp:BoundColumn DataField="specialty" HeaderText="Specialty" Visible="true"></asp:BoundColumn>
                                                            <%--8=10--%>
                                                            <asp:BoundColumn DataField="RECEIVED" HeaderText="Total Received" ItemStyle-HorizontalAlign="Right">
                                                            </asp:BoundColumn>
                                                            <%--9=11--%>
                                                            <asp:BoundColumn DataField="OUTSTANDING" HeaderText="Total Outstanding" ItemStyle-HorizontalAlign="Right">
                                                            </asp:BoundColumn>


                                                            <%--10=12--%>
                                                            <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off"></asp:BoundColumn>
                                                            <%--11=13--%>
                                                            <asp:BoundColumn DataField="PaidInPeriod" HeaderText="Paid Amount (Period Range)">
                                                            </asp:BoundColumn>
                                                             <%--14--%>
                                                             <asp:BoundColumn DataField="mn_transferred_amount" HeaderText="Transferred Amount"
                                                                Visible="true"></asp:BoundColumn>
                                                                   <%--15--%>
                                                                  <asp:BoundColumn DataField="dt_transferred_on" HeaderText="Transferred Date" Visible="true">
                                                            </asp:BoundColumn>
                                                             <%--16--%>
                                                               <asp:BoundColumn DataField="SZ_COMPANY_NAME" HeaderText="Transferred Attorney" Visible="true">
                                                            </asp:BoundColumn>
                                                        

                                                            <%--12=17--%>
                                                            <asp:BoundColumn DataField="SZ_COMMENT" HeaderText="Comment"></asp:BoundColumn>
                                                            <%--13=18--%>
                                                            <asp:BoundColumn DataField="SZ_PAYMENT" HeaderText="Last Payment"></asp:BoundColumn>
                                                            <%--14=19--%>
                                                            <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case NO" Visible="false"></asp:BoundColumn>
                                                            <%--15=20--%>
                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Verification" Visible="false">
                                                            </asp:BoundColumn>
                                                            <%--16=21--%>
                                                            <asp:BoundColumn DataField="SZ_DENAIL" HeaderText="Denail" Visible="false"></asp:BoundColumn>
                                                            <%--17=22--%>
                                                            <%-- <asp:BoundColumn DataField="SZ_PATH" HeaderText="View" Visible="true"></asp:BoundColumn>--%>
                                                            <asp:BoundColumn DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance" Visible="true">
                                                            </asp:BoundColumn>
                                                            <%--18=23--%>
                                                            <asp:BoundColumn DataField="SZ_PATH" HeaderText="View" Visible="false"></asp:BoundColumn>
                                                            
                                                           
                                                        </Columns>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                    </asp:DataGrid>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" style="text-align: left;" class="TDPart">
                                            <table width="100%">
                                                <tr>
                                                    <td width="10%">
                                                        <asp:Label ID="lblTotalBillAmount" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10%">
                                                        <asp:Label ID="lblTotalPaidAmount" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%">
                                                        <asp:Label ID="lblTotalOutstandingAmount" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="display: none">
                <asp:LinkButton ID="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" PopupControlID="Panel3"
                TargetControlID="lbn_job_det" BackgroundCssClass="modalBackground" DropShadow="false"
                PopupDragHandleControlID="divHeader">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel Style="display: none" ID="Panel3" runat="server" Width="70%" CssClass="modalPopup">
                <div id="divHeader" style="left: 0%; vertical-align: text-bottom; width: 100%; position: absolute;
                    top: 0px; height: 15px; background-color: lightgrey; text-align: left" colspan=""
                    rowspan="">
                    <asp:Button Style="left: 95%; position: absolute; top: 5px" ID="btnClose" OnClick="btnClose_Click"
                        runat="server" CssClass="Buttons" Text="X"></asp:Button>
                </div>
                <table width="100%">
                    <tr>
                        <td class="TDPart" width="100%">
                            <table width="100%">
                                <tr>
                                    <td style="height: 4px" width="15%">
                                        <asp:Label ID="lblBillNoText" Text="Bill No:-" runat="server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblBillNo" runat="server"></asp:Label>
                                    </td>
                                    <td style="height: 4px" width="20%">
                                        <asp:Label ID="lblBillDateText" Text="Bill Date:-" runat="server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                                    </td>
                                    <td style="height: 4px" width="40%">
                                        <asp:Label ID="lblBillNameText" Text="Patient Name:-" runat="server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblPatientName" runat="server"></asp:Label>
                                    </td>
                                    <td style="height: 4px" width="23%">
                                        <asp:Label ID="lblCaseNoText" Text="Case No:-" runat="server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblCaseNo" runat="server"></asp:Label>
                                    </td>
                                    <td style="height: 4px" width="2%">
                                        <%--<asp:Button id="btnClose" onclick="btnClose_Click" runat="server" Width="100%" CssClass="Buttons" Text="X" align="RIGHT"></asp:Button>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDPart" width="100%">
                            <asp:DataGrid ID="grdListOfPayment" AutoGenerateColumns="false" runat="server" Width="100%"
                                CssClass="GridTable">
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:BoundColumn DataField="SZ_CHECK_NUMBER" HeaderText="Check Number"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DT_CHECK_DATE" HeaderText="Check Date"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="FLT_CHECK_AMOUNT" HeaderText="Check Amount"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="User Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PAYMENT_TYPE" HeaderText="Payment Type"></asp:BoundColumn>
                                </Columns>
                                <HeaderStyle CssClass="GridHeader" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
