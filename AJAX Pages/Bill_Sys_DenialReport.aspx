<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_DenialReport.aspx.cs" Inherits="Bill_Sys_DenialReport" Title="Green Your Bills - Denial Report" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
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


        function SelectAll(ival) {
            var f = document.getElementById("<%= grdvDenial.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    f.getElementsByTagName("input").item(i).checked = ival;
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
        function CheckGrid() {
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
        }


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

        function Validate() {


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
        }
    </script>
    <asp:updatepanel id="UpdatePanel112" runat="server" visible="true">
        <contenttemplate>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table <%--id="MainBodyTable"--%> cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td <%--class="LeftTop"--%>>
                        </td>
                        <td <%--class="CenterTop"--%>>
                        </td>
                        <td <%--class="RightTop"--%>>
                        </td>
                    </tr>
                    <tr>
                        <td <%--class="LeftCenter"--%> style="height: 100%">
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="1" cellspacing="3" style="width: 100%; height: 100%;background-color: White;">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td <%--class="ContentLabel"--%> style="text-align: left; width: 40%;">
                                                    <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                        height: 50%; border: 1px solid #B5DF82;">
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
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                            Bill</td>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                           From Date</td>
                                                                           <td class="td-widget-bc-search-desc-ch1">
                                                                           To Date
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
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
                                                                        <td align="center">
                                                                            <asp:TextBox ID="txtupdatefromdate" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox><asp:ImageButton
                                                                             ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                             <ajaxcontrol:CalendarExtender 
                                                                                ID="CalendarExtender3" 
                                                                                runat="server" 
                                                                                PopupButtonID="imgbtnFromDate" 
                                                                                TargetControlID="txtupdatefromdate">
                                                                            </ajaxcontrol:CalendarExtender>

                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:TextBox ID="txtupdateToDate" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox><asp:ImageButton
                                                                                ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                <ajaxcontrol:CalendarExtender 
                                                                                        ID="CalendarExtender4" 
                                                                                        runat="server" 
                                                                                        PopupButtonID="imgbtnToDate" 
                                                                                        TargetControlID="txtupdateToDate">
                                                                                 </ajaxcontrol:CalendarExtender>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                            Case No</td>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                            Bill No</td>
                                                                          <td class="td-widget-bc-search-desc-ch1">
                                                                            Patient Name</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:TextBox ID="txtupdateCaseNo" runat="server" Width="90px"></asp:TextBox>

                                                                        </td>
                                                                        <td align="center">
                                                                             <asp:TextBox ID="txtupdateBillNo" runat="server" Width="89px"></asp:TextBox>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:TextBox ID="txtupdatePatientName" runat="server" Width="90px"></asp:TextBox>
                                                                         </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                            Denial Scan From Date
                                                                        </td>
                                                                     
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                        Denial Scan To &nbsp; &nbsp; Date
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                            Status
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                            <asp:TextBox ID="txtDenialFromDate" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                            <asp:ImageButton ID="imgbtnDenialFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                             <ajaxcontrol:CalendarExtender 
                                                                                ID="CalendarExtender1" 
                                                                                runat="server" 
                                                                                PopupButtonID="imgbtnDenialFromDate" 
                                                                                TargetControlID="txtDenialFromDate">
                                                                            </ajaxcontrol:CalendarExtender>
                                                                        </td>
                                                                     
                                                                            <td class="td-widget-bc-search-desc-ch1">
                                                                            <asp:TextBox ID="txtDenialToDate" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                            <asp:ImageButton ID="imgbtnDenialToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                             <ajaxcontrol:CalendarExtender 
                                                                                ID="CalendarExtender2" 
                                                                                runat="server" 
                                                                                PopupButtonID="imgbtnDenialToDate" 
                                                                                TargetControlID="txtDenialToDate">
                                                                            </ajaxcontrol:CalendarExtender>
                                                                
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                       <asp:RadioButtonList ID="rblView" runat="server" RepeatDirection="Horizontal">
                                                                                                    <asp:ListItem Value="0"  >Paid</asp:ListItem>
                                                                                                    <asp:ListItem Value="1">Unpaid</asp:ListItem>
                                                                                                      <asp:ListItem Value="2"  Selected="True">Both/denied&nbsp;</asp:ListItem>
                                                                                                    <%-- <asp:ListItem Value="2">Verification sent&nbsp;&nbsp;</asp:ListItem>--%>
                                                                                                </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch1">Case Type</td>
                                                                        <td class="td-widget-bc-search-desc-ch1" colspan="2">Location</td>
                                                                                                                                   
                                                                    </tr>
                                                                    <tr>
                                                                       <td class="td-widget-bc-search-desc-ch1">
                                                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="90%" Selected_Text="---Select---"
                                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"
                                                                            CssClass="search-input" Visible="true" ></extddl:ExtendedDropDownList>
                                                                        </td>
                                                                       <td class="td-widget-bc-search-desc-ch1" colspan="2">
                                                                            <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="250px" Connection_Key="Connection_String"
                                                                             Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" 
                                                                             Selected_Text="---Select---" CssClass="cinput" Visible="true"/>
                                                                         </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                            Doctor Name</td>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                            </td>
                                                                          <td class="td-widget-bc-search-desc-ch1">
                                                                            </td>                                                                    
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="3">&nbsp;&nbsp; &nbsp;&nbsp;
                                                                            <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="373px" Connection_Key="Connection_String"
                                                                             Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" 
                                                                             Selected_Text="---Select---" CssClass="cinput" Visible="true"/>
                                                                         </td>
                                                                    </tr>
                                                                    
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                           Denial Reason </td>
                                                                        <td class="td-widget-bc-search-desc-ch1">
                                                                            </td>
                                                                          <td class="td-widget-bc-search-desc-ch1">
                                                                            </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="3">&nbsp &nbsp &nbsp 
                                                                            <extddl:ExtendedDropDownList 
                                                                                ID="extddenial" 
                                                                                Width="373px"
                                                                                runat="server" 
                                                                                Connection_Key="Connection_String" 
                                                                                Procedure_Name="SP_MST_DENIAL"
                                                                                Flag_Key_Value="DENIAL_LIST" 
                                                                                Selected_Text="--- Select ---" 
                                                                                CssClass="cinput" Visible="true" />
                                                                        </td>
                                                                        <%--<td align="center">
                                                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click1" runat="server" Width="80px"
                                                                                               Text="Search"></asp:Button>
                                                                        </td>--%>
                                                                        <%--<td>
                                                                        </td>--%>
                                                                    </tr>
                                                                   <%-- <tr>
                                                                        <td colspan="4" style="vertical-align: middle; text-align: center">
                                                                            
                                                                &nbsp; <asp:Button ID="btnSearch" OnClick="btnSearch_Click1" runat="server" Width="80px"
                                                                                               Text="Search"></asp:Button>
                                                               </td>
                                                                    </tr>--%>
                                                                     <tr>
                                                                        <td align="center">
                                                                            

                                                                        </td>
                                                                        <td align="center">
                                                                             <asp:Button ID="btnSearch" OnClick="btnSearch_Click1" runat="server" Width="80px"
                                                                                               Text="Search"></asp:Button>
                                                                        </td>
                                                                        <td align="center">
                                                                           
                                                                         </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                 
                                                <td valign="top">
                                                           <table id="div12" runat="server"  cellpadding="0" cellspacing="0" style="width: 90%;">
                                                           <tr>
                                                           <td>
                                                           <div style="overflow: scroll; height: 250px">

                                                                     <xgrid:XGridViewControl id="testgrid1" runat="server" CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153" 
                                                                        EnableRowClick="false" PageRowCount="50"  HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="false" GridLines="None" XGridKey="DenialReasonsReport" PagerStyle-CssClass="pgr" DataKeyNames="SZ_DENIAL_REASONS" AllowSorting="true">
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
                                                    </div>
                                            
                                                         </td>
                                                           </tr>
                                                           </table>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                <td>
                                     <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                </td>
                                </tr>
                                <tr>
                                    <td style="width: 1127px; height: auto;">
                                        <div style="width: 100%;">
                                            <table style="height: auto; width: 100%; border: 0px solid #B5DF82;" class="txt2"   align="left"
                                                cellpadding="0" cellspacing="0" class="border">
                                                <tr>
                                                 <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px">
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel112"  DisplayAfter="10">
                                                    <ProgressTemplate>
                                                        <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                runat="Server">
                                                        <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                        Height="25px" Width="24px"></asp:Image>
                                                    Loading...</div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 1017px;">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <contenttemplate>
                        <TABLE style="VERTICAL-ALIGN: middle;width:100%;"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50%" align=left>Search:<gridsearch:XGridSearchTextBox id="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input">
                         </gridsearch:XGridSearchTextBox> </TD>
                         <TD style="VERTICAL-ALIGN: middle; WIDTH: 50%; TEXT-ALIGN: right" align="right" colSpan=2>
                            Record Count:<%= this.grdvDenial.RecordCount %>| Page Count: <gridpagination:XGridPaginationDropDown id="con" runat="server">
                                    </gridpagination:XGridPaginationDropDown> 
                                    <asp:LinkButton id="lnkExportToExcel" onclick="lnkExportTOExcel_onclick" runat="server" Text="Export TO Excel">
                                    <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton> 
                                    <asp:Button ID="btnLitigantion" runat="server" Text="Litigate"  OnClick="btnLitigantion_Click" /> 
                                    </TD></TR></TBODY></TABLE>
                                    <xgrid:XGridViewControl id="grdvDenial" runat="server" Width="100%" CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153" 
                                    ExcelFileNamePrefix="ExcelLitigation" ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader" 
                                    ExportToExcelColumnNames="Bill No.,Case #,Patient Name,Doctor Name,Provider Name,Bill Status,Visit Date,Bill Amount,Paid Amount,Balance,Denial Date,Description,Denial Reasons" 
                                    ExportToExcelFields="SZ_BILL_NUMBER,SZ_CASE_NO,SZ_PATIENT_NAME,SZ_DOCTOR_NAME,SZ_OFFICE,SZ_BILL_STATUS_NAME,DT_VISIT_DATE,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE,dt_verification_date,description,sz_denial_reasons"
                                    OnRowCommand="grdvDenial_RowCommand"
                                     AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" GridLines="None" PageRowCount="50" XGridKey="DenialReport" PagerStyle-CssClass="pgr" DataKeyNames="SZ_CASE_ID,SZ_CASE_NO,SZ_BILL_NUMBER,bt_denial" AllowSorting="true">
                                                                    <HeaderStyle CssClass="GridViewHeader" ></HeaderStyle>
                                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                    <Columns>                                                                        
                                                                        <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill No" SortExpression="convert(int,substring(TXN_BILL_TRANSACTIONS.sz_Bill_number,3,len(TXN_BILL_TRANSACTIONS.sz_bill_number)))">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                       <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #" SortExpression="convert(int,SZ_CASE_NO)" HeaderStyle-Width="40px">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>                                                                   
                                                                        <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderStyle-Width="100px" HeaderText="Patient Name" SortExpression="(ISNULL(SZ_PATIENT_FIRST_NAME,'')  +' '+ ISNULL(SZ_PATIENT_LAST_NAME ,''))">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderStyle-Width="90px" HeaderText="Doctor Name" SortExpression="SZ_DOCTOR_NAME">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SZ_OFFICE" HeaderText="Provider Name"  HeaderStyle-Width="100px" SortExpression="MST_OFFICE.SZ_OFFICE">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                            <itemstyle horizontalalign="left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SZ_BILL_STATUS_NAME" HeaderText="Bill Status">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                         <asp:BoundField DataField="DT_VISIT_DATE" HeaderText="Visit Date" SortExpression="(SELECT MAX(DT_DATE_OF_SERVICE) FROM TXN_BILL_TRANSACTIONS_DETAIL WHERE SZ_BILL_NUMBER = TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                            <itemstyle horizontalalign="left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="FLT_BILL_AMOUNT" HeaderText="Bill($)" SortExpression="convert(int,FLT_BILL_AMOUNT)"  DataFormatString={0:C}>
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="PAID_AMOUNT" HeaderText="Paid($)"  DataFormatString="{0:C}">
                                                                         <headerstyle horizontalalign="Center"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="FLT_BALANCE" HeaderText="Balance"  DataFormatString="{0:C}">
                                                                         <headerstyle horizontalalign="Center"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                       
                                                                         <asp:BoundField DataField="dt_verification_date" HeaderText="Denial Date">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                            <itemstyle horizontalalign="Center"></itemstyle>
                                                                        </asp:BoundField>
                                                                       
                                                                         <asp:BoundField DataField="description" HeaderText="Description">
                                                                        <headerstyle horizontalalign="Center" ></headerstyle>
                                                                            <itemstyle horizontalalign="left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="sz_denial_reasons" HeaderText="Denial Reasons">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                            <itemstyle horizontalalign="left"></itemstyle>
                                                                        </asp:BoundField>

                                                                         <asp:TemplateField HeaderText="View">
                                                                         <itemtemplate>
                                                                             <%# DataBinder.Eval(Container,"DataItem.SZ_BILL_PATH")%>
                                                                        </itemtemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <%--<asp:TemplateField HeaderText="Denials" visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lnkDP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="DenialPLS"  font-size="15px" 
                                                                                    Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                                                <asp:LinkButton ID="lnkDM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="DenialMNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="15px"  Visible="false"></asp:LinkButton>                                                                                  
                                                                            </itemtemplate>
                                                                        </asp:TemplateField>--%>
                                                                        
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                                                        </HeaderTemplate>
                                                                          <itemtemplate>
                                                                              <asp:CheckBox ID="ChkLitigantion" runat="server" ></asp:CheckBox>
                                                                            </itemtemplate>
                                                                         </asp:TemplateField>
                                                                       
                                                                        
                                                                        <%-- <asp:TemplateField visible="false">
                                                                            <itemtemplate>                                            
                                                                            <tr>
                                                                                <td colspan="50%" align="right">
                                                                                    <div id="div1<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position:relative; ">
                                                                                    <asp:GridView ID="grdDenial" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="500px" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="SZ_DENIAL_REASONS" ItemStyle-Width="105px"  HeaderText="Denial Reason" HeaderStyle-HorizontalAlign="Center">
                                                                                            <itemstyle horizontalalign="center"></itemstyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="DT_DENIAL_DATE" ItemStyle-Width="50px" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Denial Date" HeaderStyle-HorizontalAlign="Center">
                                                                                            <itemstyle horizontalalign="Center"></itemstyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="SZ_DESCRIPTION" ItemStyle-Width="85px" HeaderText="Description" HeaderStyle-HorizontalAlign="Center">	
                                                                                            <itemstyle horizontalalign="center"></itemstyle>
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                    </asp:GridView>
                                                                                    </div>
                                                                                </td>
                                                                            </tr> 
                                                                            </itemtemplate>
                                                                        </asp:TemplateField> --%>
                                                                        
                                                   </Columns> </xgrid:XGridViewControl> 
                                                        </contenttemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                          </table>
                        </td>
                        <td <%--class="RightCenter"--%> style="width: 10px; height: 100%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td <%--class="LeftBottom"--%>>
                        </td>
                        <td <%--class="CenterBottom"--%>>
                        </td>
                        <td <%--class="RightBottom" --%>style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</contenttemplate>
    </asp:updatepanel>
    <asp:textbox id="txtCaseNo" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtBillNo" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtCompanyID" runat="server" visible="False" width="10px">
    </asp:textbox>
    <asp:textbox id="txtBillStatus" runat="server" text="VR" visible="false" width="10px" />
    <asp:textbox id="txtDay" runat="server" text="" visible="false" width="10px" />
    <asp:textbox id="txtFlag" runat="server" text="REF" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtFromDate" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtToDate" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtPatientName" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtDenial" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtDoctor" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtLocation" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtCaseType" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtDenialFromdt" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtDenialTodt" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
    <asp:textbox id="txtPaidUnPaid" runat="server" text="" visible="false" width="10px">
    </asp:textbox>
</asp:content>
