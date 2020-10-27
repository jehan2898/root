<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="InitialReeval.aspx.cs" Inherits="AJAX_Pages_InitialReeval" %>

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

        function selectValue() {

            var lbl = document.getElementById('ctl00_ContentPlaceHolder1_lblDate');
            if (document.getElementById("ctl00_ContentPlaceHolder1_rblInitialReeval_0").checked == true) {


                lbl.innerText = "Accident Date";
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_rblInitialReeval_1").checked == true) {



                lbl.innerText = "Visit Date";
            }
        }
    </script>
    <script type="text/javascript">
          function SelectAllSpeciality(ival) {
            //alert(ival);
            var f = document.getElementById('<%=grdSpeciality.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

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
            var fdate = document.getElementById("<%=txtupdateFromDate.ClientID%>").value;
            var ldate = document.getElementById("<%=txtupdateToDate.ClientID%>").value;


            if (ldate != "") {
                if (!getDateDiff(ldate, m + '/' + n + '/' + y)) {
                    return false;
                }
            }
           var f = document.getElementById("<%=grdSpeciality.ClientID%>");
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
                alert('Please select specialty.');
                return false;
            }
            else {
                return true;
            }
        }
        function Clear() {
            //alert("call");
            document.getElementById("<%=txtupdateFromDate.ClientID%>").value = '';

            document.getElementById("<%=txtupdateToDate.ClientID %>").value = '';
            document.getElementById("<%=txtINS1.ClientID %>").value = '';
            document.getElementById("<%=txtOffice1.ClientID %>").value = '';

            document.getElementById("<%=hdnInsurace.ClientID %>").value = '';
            document.getElementById("<%=hdnOffice.ClientID %>").value = '';

            document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value = 0;
            document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value = 'NA';
            document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value = 'NA';
            var RB1 = document.getElementById("<%=rblInitialReeval.ClientID%>");
            var radio = RB1.getElementsByTagName("input");
            radio[1].checked = true;

        }
        function GetInsuranceValue(source, eventArgs) {
            //alert(eventArgs.get_value());
            document.getElementById("<%=hdnInsurace.ClientID %>").value = eventArgs.get_value();
        }

        function GetOfficeValue(source, eventArgs) {
            //alert(eventArgs.get_value());
            document.getElementById("<%=hdnOffice.ClientID %>").value = eventArgs.get_value();
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
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = lastmonth('endmonth'); i

                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = lastyear('endyear');

                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById('<%=txtupdateToDate.ClientID %>').value = quarteryear('endquaeter');

                document.getElementById('<%=txtupdateFromDate.ClientID %>').value = quarteryear('startquaeter');
            }
}




    </script>
    <asp:UpdatePanel ID="UpdatePanel112" runat="server" Visible="true">
        <ContentTemplate>
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
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <UserMessage:MessageControl runat="server" ID="usrMessage2"></UserMessage:MessageControl>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px" valign="top">
                                                <table width="550px" border="0">
                                                    <tr>
                                                        <td style="text-align: left; width: 550px;">
                                                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 550px; height: 50%; border: 1px solid #B5DF82;">
                                                                <tr>
                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" style="width: 200px">
                                                                        <b class="txt3">Search Parameters
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20%; height: 0px;" valign="top">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 550px;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                            <tr>
                                                                                 <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                    <asp:Label ID="lblDate" runat="server" Text="Visit Date" Font-Bold="true" Font-Size="12px"> </asp:Label>
                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">From Date
                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">To Date
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
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                    <asp:TextBox ID="txtupdateFromDate" runat="server" Width="80%" onkeypress="return CheckForInteger(event,'/')"
                                                                                        Height="15px"></asp:TextBox>
                                                                                    &nbsp;<asp:ImageButton ID="imgbtnFromDate" Width="10%" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="imgbtnFromDate"
                                                                                        TargetControlID="txtupdatefromdate"></ajaxcontrol:CalendarExtender>
                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                    <asp:TextBox ID="txtupdateToDate" runat="server" Width="80%" onkeypress="return CheckForInteger(event,'/')"
                                                                                        Height="15px"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgbtnToDate" Width="10%" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="imgbtnToDate"
                                                                                        TargetControlID="txtupdateToDate"></ajaxcontrol:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">Insurance Company
                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">Provider
                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">Case Type
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                    <%--  <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="15%" Selected_Text="---Select---"
                                                                                        Connection_Key="Connection_String" Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY">
                                                                                    </extddl:ExtendedDropDownList>--%>
                                                                                    <asp:TextBox ID="txtINS1" runat="server" Width="98%" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                                                    <ajaxcontrol:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                                                                                        DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtINS1"
                                                                                        ServiceMethod="GetInsurance" OnClientItemSelected="GetInsuranceValue" ServicePath="PatientService.asmx"
                                                                                        UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                                                                                    </ajaxcontrol:AutoCompleteExtender>
                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                    <%-- <cc1:ExtendedDropDownList ID="extddlOffice" runat="server" Connection_Key="Connection_String"
                                                                                        Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_GET_OFFICE_LIST_FOR_SHCEDULE_REPORT"
                                                                                        Selected_Text="--- Select ---" Width="15%"></cc1:ExtendedDropDownList>--%>
                                                                                    <asp:TextBox ID="txtOffice1" runat="server" Text="" Width="98%" autocomplete="off"
                                                                                        CssClass="search-input"></asp:TextBox>
                                                                                    <ajaxcontrol:AutoCompleteExtender runat="server" ID="ajAutoOffice" EnableCaching="true"
                                                                                        OnClientItemSelected="GetOfficeValue" DelimiterCharacters="" MinimumPrefixLength="1"
                                                                                        CompletionInterval="500" TargetControlID="txtOffice1" ServiceMethod="GetProvider"
                                                                                        ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                                                                                    </ajaxcontrol:AutoCompleteExtender>
                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="98%" Selected_Text="---Select---"
                                                                                        Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                </td>
                                                                            </tr>
                                                                           <tr>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">Specialty
                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">Case Status</td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                  <%--  <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                                        Selected_Text="---Select---" Width="100%"></cc1:ExtendedDropDownList>--%>
                                                                                    <div style="height: 160px; background-color: Gray; overflow: scroll;">
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
                                                                                                <dx:GridViewDataColumn FieldName="code" Caption="Specialty Id" VisibleIndex="2" Visible="false">
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                </dx:GridViewDataColumn>
                                                                                            </Columns>
                                                                                            <SettingsPager PageSize="1000">
                                                                                            </SettingsPager>
                                                                                        </dx:ASPxGridView>
                                                                                    </div>

                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                    <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="100%" Selected_Text="OPEN"
                                                                                        Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Connection_Key="Connection_String"
                                                                                        CssClass="search-input"></extddl:ExtendedDropDownList>
                                                                                </td>
                                                                                <td class="td-widget-bc-search-desc-ch" width="33%">
                                                                                    <asp:RadioButtonList ID="rblInitialReeval" runat="server" RepeatDirection="Horizontal"
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
                                                                                    <asp:UpdateProgress ID="UpdateProgress123" runat="server" AssociatedUpdatePanelID="UpdatePanel112"
                                                                                        DisplayAfter="10">
                                                                                        <ProgressTemplate>
                                                                                            <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 350px; left: 600px"
                                                                                                runat="Server">
                                                                                                <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                                Loading...
                                                                                            </div>
                                                                                        </ProgressTemplate>
                                                                                    </asp:UpdateProgress>
                                                                                    <asp:Button Style="width: 80px" ID="btnSearch" OnClick="btnSearch_Click" runat="server"
                                                                                        Text="Search"></asp:Button>
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
                        <asp:ImageButton ID="btnexport" runat="server" Height="15px" Width="15px" OnClick="btnexport_Click"
                            ImageUrl="~/Images/Excel.jpg" />
                    </td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#B5DF82" height="28" style="width: 100%" valign="middle">
                        <b class="txt3"></b>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Text="Visits"></asp:Label>
                    </td>
                </tr>
               <caption>
                    
                    <tr>
                        <td style="width: 100%">
                            &nbsp;
                            <div style="width:100%">
                                <table style="height:9px; background-color:#d3d3d3" width="99%">
                                    <tr>
                                        <td style="width:9px; font-size:13px" align="left"><b>Case#</b></td>
                                        <td style="width:40px; font-size:13px" align="center"><b>Name</b></td>
                                        <td style="width:30px; font-size:13px" align="center"><b>Phone</b></td>
                                         <td style="width:40px; font-size:13px" align="center"><b>Work Phone</b></td>
                                        <td style="width:40px; font-size:13px" align="center"><b>Cell No</b></td>
                                        <td style="width:10px; font-size:13px" align="center"><b>DOA</b></td>
                                        <td style="width:85px; font-size:13px" align="center"><b>Insurance</b></td>
                                        <td style="width:15px; font-size:13px" align="center"><b>Case Type</b></td>
                                        <td style="width:18px; font-size:13px" align="center"><b>Specialty</b></td>
                                        <td style="width:20px; font-size:13px" align="center"><b>Doctor</b></td>
                                        <td style="width:70px; font-size:13px" align="center"><b>Provider</b></td>
                                        <td style="width:10px; font-size:13px" align="center"><b>Last Initial/Re-Eval Date</b></td>
                                    </tr>
                                </table>
                            </div>
                            <div style="height: 400px; overflow-y: scroll; overflow-x:scroll">
                                <asp:DataGrid ID="grdVisits" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnItemCommand="grdVisits_ItemCommand" ShowHeader="false" Width="100%">
                                    <AlternatingItemStyle BackColor="#EEEEEE" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="CASE#" Visible="true" ItemStyle-Width="50px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnlCasetNo" runat="server" CommandArgument="SZ_CASE_NO" CommandName="case" Font-Bold="true" Font-Size="12px">CASE#</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.CASE#")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Name" Visible="true" ItemStyle-Width="140px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnlName" runat="server" CommandArgument="SZ_PATIENT_FIRST_NAME" CommandName="Name" Font-Bold="true" Font-Size="12px">Name</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Phone" Visible="true" ItemStyle-Width="90px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnPhone" runat="server" CommandArgument="SZ_PATIENT_PHONE" CommandName="Phone" Font-Bold="true" Font-Size="12px">Phone</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_PHONE")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Work Phone" Visible="true" ItemStyle-Width="90px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnPhone" runat="server" CommandArgument="SZ_WORK_PHONE" CommandName="WPhone" Font-Bold="true" Font-Size="12px">Work Phone</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.SZ_WORK_PHONE")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cell No." Visible="true" ItemStyle-Width="90px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnPhone" runat="server" CommandArgument="SZ_PATIENT_CELLNO" CommandName="CPhone" Font-Bold="true" Font-Size="12px">Cell No.</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_CELLNO")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="DOA" Visible="true" ItemStyle-Width="70px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnDOA" runat="server" CommandArgument="DT_DATE_OF_ACCIDENT" CommandName="DOA" Font-Bold="true" Font-Size="12px">DOA</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.DT_DATE_OF_ACCIDENT")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Insurance" Visible="true" ItemStyle-Width="200px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnInsurance" runat="server" CommandArgument="SZ_INSURANCE_NAME" CommandName="Insurance" Font-Bold="true" Font-Size="12px">Insurance</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.SZ_INSURANCE_NAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Case Type" Visible="true" ItemStyle-Width="70px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnCaseType" runat="server" CommandArgument="SZ_CASE_TYPE_NAME" CommandName="CaseType" Font-Bold="true" Font-Size="12px">Case Type</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.SZ_CASE_TYPE_NAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Speciality" Visible="true" ItemStyle-Width="80px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnSpeciality" runat="server" CommandArgument="SPECIALTY" CommandName="Speciality" Font-Bold="true" Font-Size="12px">Specialty</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.SPECIALTY")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Doctor" Visible="true" ItemStyle-Width="150px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnDoctor" runat="server" CommandArgument="SZ_DOCTOR_NAME" CommandName="Doctor" Font-Bold="true" Font-Size="12px">Doctor</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.SZ_DOCTOR_NAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Provider" Visible="true" ItemStyle-Width="170px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnProvider" runat="server" CommandArgument="SZ_OFFICE" CommandName="Provider" Font-Bold="true" Font-Size="12px">Provider</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.OFFICE")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Last Initial/Re-Eval Date" Visible="true" ItemStyle-Width="120px">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnlstdate" runat="server" CommandArgument="DT_EVENT_DATE" CommandName="LastDate" Font-Bold="true" Font-Size="12px">Last Initial/Re-Eval Date</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.LAST VISIT DATE")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#d3d3d3" Font-Bold="true" />
                                </asp:DataGrid>
                            </div>
                            <div style="height: 450px; overflow: scroll; visibility: hidden">
                                <asp:DataGrid ID="grdExPort" runat="server" AutoGenerateColumns="False" CssClass="mGrid" Width="100%">
                                    <AlternatingItemStyle BackColor="#EEEEEE" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CASE#" HeaderStyle-HorizontalAlign="left" HeaderText="Case no" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderStyle-HorizontalAlign="left" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderStyle-HorizontalAlign="left" HeaderText="Phone" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_WORK_PHONE" HeaderStyle-HorizontalAlign="left" HeaderText="Work Phone" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_PATIENT_CELLNO" HeaderStyle-HorizontalAlign="left" HeaderText="Cell No." ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderStyle-HorizontalAlign="left" HeaderText="DOA" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderStyle-HorizontalAlign="left" HeaderText="Insurance" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderStyle-HorizontalAlign="left" HeaderText="Case Type" ItemStyle-HorizontalAlign="Left" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderStyle-HorizontalAlign="Center" HeaderText="Doctor" ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="OFFICE" HeaderStyle-HorizontalAlign="Center" HeaderText="Provider" ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="LAST VISIT DATE" HeaderStyle-HorizontalAlign="left" HeaderText="Last Initial/Re-Eval Date" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#b5df82" Font-Bold="true" />
                                </asp:DataGrid>
                            </div>
                        </td>
                    </tr>
                </caption>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
