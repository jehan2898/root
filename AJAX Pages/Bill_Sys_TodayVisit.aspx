<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_TodayVisit.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_TodayVisit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <script type="text/javascript">


        function getDateDiff(time1, time2) {

            var str1 = time1.split('/');
            var str2 = time2.split('/');

            //                yyyy   , mm       , dd
            var t1 = new Date(str1[2], str1[0] - 1, str1[1]);
            var t2 = new Date(str2[2], str2[0] - 1, str2[1]);

            var diffMS = t1 - t2;


            var diffS = diffMS / 1000;


            var diffM = diffS / 60;


            var diffH = diffM / 60;


            var diffD = diffH / 24;


            if (diffD < 0) {

                alert('Please select current date or future date..');
                return false;
            } else {
                return true;
            }
        }

        function Vlidate() {

            var d = new Date();
            var n = d.getDate();
            var y = d.getFullYear();
            var m = d.getMonth() + 1;
            var fdate = document.getElementById("<%=txtFromDate.ClientID%>").value;
            var ldate = document.getElementById("<%=txtToDate.ClientID%>").value;


            if (ldate != "") {
                if (!getDateDiff(ldate, m + '/' + n + '/' + y)) {
                    return false;
                }
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value == 'NA') {



                alert('Please Select the specialty');
                return false;
            }
            else {
                return true;
            }
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
                document.getElementById('<%=txtToDate.ClientID %>').value = "";
                document.getElementById('<%=txtFromDate.ClientID %>').value = "";

            }
            else if (selValue == 1) {
                document.getElementById('<%=txtToDate.ClientID %>').value = getDate('today');
                document.getElementById('<%=txtFromDate.ClientID %>').value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById('<%=txtToDate.ClientID %>').value = getWeek('endweek');
                document.getElementById('<%=txtFromDate.ClientID %>').value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById('<%=txtToDate.ClientID %>').value = getDate('monthend');
                document.getElementById('<%=txtFromDate.ClientID %>').value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById('<%=txtToDate.ClientID %>').value = getDate('quarterend');
                document.getElementById('<%=txtFromDate.ClientID %>').value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById('<%=txtToDate.ClientID %>').value = getDate('yearend');
                document.getElementById('<%=txtFromDate.ClientID %>').value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById('<%=txtToDate.ClientID %>').value = getLastWeek('endweek');
                document.getElementById('<%=txtFromDate.ClientID %>').value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById('<%=txtToDate.ClientID %>').value = lastmonth('endmonth'); i

                document.getElementById('<%=txtFromDate.ClientID %>').value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById('<%=txtToDate.ClientID %>').value = lastyear('endyear');

                document.getElementById('<%=txtFromDate.ClientID %>').value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById('<%=txtToDate.ClientID %>').value = quarteryear('endquaeter');

                document.getElementById('<%=txtFromDate.ClientID %>').value = quarteryear('startquaeter');
            }
}

function SelectAll(ival) {
    var f = document.getElementById("<%= grdTodayVisit.ClientID %>");
    var str = 1;
    for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
        if (f.getElementsByTagName("input").item(i).type == "checkbox") {



            if (f.getElementsByTagName("input").item(i).disabled == false) {
                f.getElementsByTagName("input").item(i).checked = ival;
            }

            //			                    str=str+1;	
            //			        
            //			                     if (str < 10)
            //		                        {
            //		                            var statusnameid1 = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch_ctl0"+str+"_lblStatus");
            //		                           
            //		                           alert(statusnameid1.innerHTML);
            //		                              statusname  = statusnameid1.innerHTML;
            //		                            
            //		                              
            //		                                    if(statusname.toLowerCase() != "transferred")
            //		                                    {  alert(str); 
            //		                                         f.getElementsByTagName("input").item(i).checked=ival; 
            //        		                                
            //		                                    }
            //		                           }else
            //		                            {
            //		                                var statusnameid2 = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch_ctl"+str+"_lblStatus");
            //		                                    statusname  = statusnameid2.innerHTML;
            //		                                      alert(statusname);
            //		                                    if (statusname.toLowerCase() != "transferred")
            //		                                    {  
            //		                                         f.getElementsByTagName("input").item(i).checked=ival;
            //		                                    }
            //			                        }        
            //			                 				

        }


    }
}

function SetReValue() {
    debugger;

    if (document.getElementById('ctl00_ContentPlaceHolder1_ddlStatus').selectedIndex == 1) {
        document.getElementById('tdReDate').style.visibility = 'visible';
        document.getElementById('tdReDateValue').style.visibility = 'visible';
        document.getElementById('tdReTime').style.visibility = 'visible';
        document.getElementById('tdReTimeValue').style.visibility = 'visible';
    }
    else {
        document.getElementById('tdReDate').style.visibility = 'hidden';
        document.getElementById('tdReDateValue').style.visibility = 'hidden';
        document.getElementById('tdReTime').style.visibility = 'hidden';
        document.getElementById('tdReTimeValue').style.visibility = 'hidden';
    }
}

function UpdateVisit() {

    var f = document.getElementById('<%=grdTodayVisit.ClientID%>');
    for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
        if (f.getElementsByTagName("input").item(i).type == "checkbox") {
            if (f.getElementsByTagName("input").item(i).checked != false) {
                if (document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ddlStatus').selectedIndex == 0) {
                    alert('Please select Status.');
                    return false;
                }
                if (document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ddlReSchHours').selectedIndex == 0) {
                    alert('Please Select Re-Schedule Date and Time');
                    return false;
                }
                if (confirm("Are you sure to continue")) {
                    return true;
                }

                else {
                    return false;
                }

            }
        }
    }

    alert('Please select Record.');
    return false;



}


function Clear() {
    //alert("call");
    document.getElementById("<%=txtFromDate.ClientID%>").value = '';
             document.getElementById("<%=txtToDate.ClientID %>").value = '';
             document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value = 0;
             document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value = 'NA';
             document.getElementById("ctl00_ContentPlaceHolder1_extddlProvider").value = 'NA';
             document.getElementById("ctl00_ContentPlaceHolder1_extddlDoctor").value = 'NA';


         }

    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: White;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; padding-top: 3px; vertical-align: top; width: 1500px;">
                <table cellpadding="0" cellspacing="0" border="0" style="background-color: White; width: 800px;">
                    <tr>
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                    <td align="center">
                                        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="background-color: White; width: 100%;">
                                <tr>
                                    <td style="width: 500px" valign="top">
                                        <table width="500px" border="0" style="height: 200px">
                                            <tr>
                                                <td style="text-align: left; width: 550px;">
                                                    <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 550px; height: 50%; border: 1px solid #B5DF82;">
                                                        <tr>
                                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" style="width: 200px">
                                                                <b class="txt3">Search Parameters</b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%; height: 0px; height: 50%" valign="top">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 550px;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center">Visit Date
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center">From Date
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center">To Date
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                            <asp:DropDownList ID="ddlDateValues" runat="Server" Height="18px" Width="100%">
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
                                                                         <td class="td-widget-bc-search-desc-ch3"  width="33%">
                                                                <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Width="84%"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtnFromDate"
                                                                    TargetControlID="txtFromDate"></ajaxcontrol:CalendarExtender>
                                                                <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>

                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch3"  width="33%">
                                                                <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Width="84%"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="imgbtnToDate"
                                                                    TargetControlID="txtToDate"></ajaxcontrol:CalendarExtender>
                                                                <asp:Label ID="lblValid1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>

                                                            </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center">Specialty
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center">Provider
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="center">Doctor
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" valign="top">

                                                                            <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Width="100%" Selected_Text="--- Select ---"
                                                                                Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Connection_Key="Connection_String"
                                                                                AutoPost_back="True"></extddl:ExtendedDropDownList>
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="left">
                                                                            <extddl:ExtendedDropDownList ID="extddlProvider" runat="server" Width="100%" Selected_Text="---Select---"
                                                                                Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_MST_OFFICE"
                                                                                Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" align="left">
                                                                            <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="100%" Selected_Text="---Select---"
                                                                                Procedure_Name="SP_MST_DOCTOR" Flag_Key_Value="GETDOCTORLIST" Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch" width="33%" colspan="4" style="padding-top: 10px">
                                                                            <asp:RadioButtonList ID="rblTodaysVisit" runat="server" RepeatDirection="Horizontal"
                                                                                Width="90%">
                                                                                <asp:ListItem Text="Scheduled" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="Re-Scheduled" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="No-show" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="All" Value="5" Selected="True"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" align="center">
                                                                           
                                                                                    <asp:Button Style="width: 80px" ID="btnSearch" runat="server"
                                                                                        Text="Search" OnClick="btnSearch_Click"></asp:Button>
                                                                                    &nbsp;
                                                                                    <input style="width: 80px" id="btnClear1" onclick="Clear();" type="button" value="Clear" />
                                                                                
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
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtStatus" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtSpeciality" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtProvider" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtDoctor" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtFDate" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtTDate" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tbody>
            <tr>
                <td colspan="3" align="right">
                    <table width="100%">
                        <tr>
                            <td style="vertical-align: middle; width: 40%; text-align: right" align="right">
                                <b>Status</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="114px" onchange="javascript:SetReValue();">
                                    <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                   <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                    <asp:ListItem Value="2">Visit Completed</asp:ListItem>
                                    <asp:ListItem Value="3">No Show</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td id="tdReDate" style="visibility: hidden;">
                                <asp:Label ID="lblReScheduleDAte" runat="server" Text="Reschedule Date" Font-Bold="true"></asp:Label>
                            </td>
                            <td id="tdReDateValue" style="visibility: hidden;">
                                <asp:TextBox ID="txtReScheduleDate" runat="server" MaxLength="10" Width="106px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                <ajaxcontrol:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReScheduleDate"
                                    PopupButtonID="imgbtnDateofBirth" Enabled="True" />
                            </td>
                            <td id="tdReTime" style="visibility: hidden;">
                                <asp:Label ID="lblReScheduleTime" runat="server" Text="Reschedule Time" Font-Bold="true"></asp:Label>
                            </td>
                            <td id="tdReTimeValue" style="visibility: hidden;">
                                <asp:DropDownList ID="ddlReSchHours" runat="server" Width="45px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlReSchMinutes" runat="server" Width="45px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlReSchTime" runat="server" Width="45px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
                                                                                        DisplayAfter="10">
                                                                                        <ProgressTemplate>
                                                                                            <div id="DivStatus20" style="vertical-align: bottom; position: absolute; top: 350px; left: 600px"
                                                                                                runat="Server">
                                                                                                <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                                Loading...
                                                                                            </div>
                                                                                        </ProgressTemplate>
                                                                                    </asp:UpdateProgress>
                                <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" OnClick="btnUpdateStatus_Click"></asp:Button>
                                                                                   
                                                                                        <asp:Button ID="btnDeletVisit" Text="Delete" runat="server" OnClick="btnDeletVisit_Click" />
                                                                                    
                                                                                    </ContentTemplate>
                                     </asp:UpdatePanel>
                            </td>

                        </tr>
                    </table>

                </td>

            </tr>
            <tr>
                <td style="width: 413px" class="txt2" valign="middle" align="left" bgcolor="#b5df82"
                    colspan="6" height="28">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Text="Visits"></asp:Label>
                    <%-- <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                            DisplayAfter="10">
                                                            <ProgressTemplate>
                                                                <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                    runat="Server">
                                                                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                        Height="25px" Width="24px"></asp:Image>
                                                                    Loading...</div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>--%>
                    <%--<asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                                        DisplayAfter="10">
                                                                        <ProgressTemplate>--%>
                    <%--<div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                runat="Server">
                                                                                <asp:Image ID="img4" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                Loading...</div>--%>
                    <%--</ProgressTemplate>
                                                                    </asp:UpdateProgress>--%>
                </td>
            </tr>

            <tr>


                <td class="lbl" style="width: 20px"></td>
                <td colspan="2" width="100%">
                    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>--%>

                    <%-- </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                </td>
                <%--    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>--%>
            </tr>
            <tr>
                <td style="width: 100%" colspan="5">
                    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>--%>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
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
                                        <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">Record Count:<%= this.grdTodayVisit.RecordCount %>| Page Count:
                                                                       
                                     <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                     </gridpagination:XGridPaginationDropDown>

                                            <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                Text="Export TO Excel">
                         <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                            <xgrid:XGridViewControl ID="grdTodayVisit" runat="server" Height="148px" Width="100%"
                                CssClass="mGrid" AllowSorting="true"
                                PagerStyle-CssClass="pgr" PageRowCount="50" DataKeyNames="SZ_CASE_ID,I_EVENT_ID,SZ_DOCTOR_ID,IS_HAVE_LOGIN,GROUP_CODE"
                                XGridKey="TodaysVisit" GridLines="None" AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                ShowExcelTableBorder="true" ExcelFileNamePrefix="TodaysVisit" MouseOverColor="0, 153, 153"
                                AutoGenerateColumns="false" ExportToExcelColumnNames="Case#,Patient Name,Phone No,Cell No,Visit Date,Specialty,Status,Insurance,Provider,Doctor"
                                ExportToExcelFields="SZ_CASE_NO,SZ_PATINET_NAME,SZ_PATIENT_PHONE,SZ_PATIENT_CELLNO,DT_EVENT_DATE,SZ_PROCEDURE_GROUP,STATUS,SZ_INSURANCE_NAME,SZ_OFFICE,SZ_DOCTOR_NAME">
                                <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                <Columns>
                                    <%--  0--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        Visible="false" HeaderText="CaseId" DataField="SZ_CASE_ID" />
                                    <%--  0--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderText="Case #" DataField="SZ_CASE_NO" SortExpression="convert(int,SZ_CASE_NO)" />
                                    <%-- 1--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderText="Patient Name" DataField="SZ_PATINET_NAME" SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME" />
                                    <%-- 2--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderText="Phone No" DataField="SZ_PATIENT_PHONE" />
                                    <%-- 3--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderText="Cell No" DataField="SZ_PATIENT_CELLNO" />
                                    <%-- 4--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderText="Visit Date" DataField="DT_EVENT_DATE" />
                                    <%-- 5--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderText="Specialty" DataField="SZ_PROCEDURE_GROUP" SortExpression="SZ_PROCEDURE_GROUP" />
                                    <%-- 6--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderText="Provider" DataField="SZ_OFFICE" SortExpression="SZ_OFFICE" />
                                    <%-- 7--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderText="Doctor" DataField="SZ_DOCTOR_NAME" SortExpression="SZ_DOCTOR_NAME" />
                                    <%-- 8--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderText="Status" DataField="STATUS" />
                                    <%-- 9--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                        HeaderText="Insurance" DataField="SZ_INSURANCE_NAME" SortExpression="SZ_INSURANCE_NAME" />
                                    <%-- 10--%>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                ToolTip="Select All" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </xgrid:XGridViewControl>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </tbody>
    </table>
            






</asp:Content>

