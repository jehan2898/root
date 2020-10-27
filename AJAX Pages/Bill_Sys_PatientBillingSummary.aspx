<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_PatientBillingSummary.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_PatientBillingSummary"
    Title="Untitled Page" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="javascript" type="text/javascript">
    // <!CDATA[

    function TABLE1_onclick() {

    }

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


    function SetVisitDate() {
        getWeek();
        var selValue = document.getElementById("<%=ddlVisit.ClientID %>").value;
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


    // ]]>
</script>

       <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
      <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table <%--id="mainbodytable"--%> cellpadding="0" cellspacing="0" style="width: 100%"
                    border="0">
                    <tr>
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%">
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%;
                                background-color: White;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="text-align: left; width: 50%; vertical-align: top;">
                                                    <table style="text-align: left; width: 100%;">
                                                        <tr>
                                                            <td style="text-align: left; width: 50%; vertical-align: top;">
                                                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                                    height: 50%; border: 0px solid #B5DF82;">
                                                                    <tr>
                                                                        <td style="width:20%; height: 0px;" align="center">
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: 1px solid #B5DF82;
                                                                                border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')" id="TABLE1" onclick="return TABLE1_onclick()">
                                                                                <tr>
                                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                                        <b class="txt3">Search Parameters</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" width="100%">
                                                                                        Bill No
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Specialty
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Bill Status
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch3" style="text-align: center" >
                                                                                        <asp:TextBox ID="txtBillNo" runat="server" CssClass="search-input"></asp:TextBox>
                                                                                    </td>
                                                                                   
                                                                                    <td class="td-widget-bc-search-desc-ch3">
                                                                                        <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Width="100%" Selected_Text="---Select---"
                                                                                            Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                                                            Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                        </td>
                                                                                    <td class="td-widget-bc-search-desc-ch3">
                                                                                        <extddl:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="100%" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="GET_SELECTED_STATUS_LIST_WITHOUT_VR_VS_DEN_FBP" Procedure_Name="SP_MST_BILL_STATUS_BILL_SEARCH"
                                                                                            Selected_Text="---Select---" />
                                                                                    </td>
                                                                                </tr>
                                                                                 <tr>
                                                                                    <td align="left" valign="top" class="td-widget-bc-search-desc-ch" style="text-align: center; height: 19px;">
                                                                                       </td>
                                                                                       <td colspan="2" style="text-align:left; height: 19px;">
                                                                                           </td> 
                                                                                  
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                       Billing Date</td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        From
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        To
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="text-align: center" class="td-widget-bc-search-desc-ch3">
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
                                                                                            MaxLength="10" Width="76%"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                        <td class="td-widget-bc-search-desc-ch3">
                                                                                            <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                                MaxLength="10" Width="70%"></asp:TextBox>
                                                                                            <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                        </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="60%">
                                                                                        </ajaxToolkit:MaskedEditValidator></td>
                                                                                    <td>
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtToDate" CssClass="search-input" EmptyValueMessage="Date is required"
                                                                                            InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date"
                                                                                            Visible="true" Width="60%">
                                                                                        </ajaxToolkit:MaskedEditValidator>
                                                                                    </td>
                                                                                </tr>
                          
                                                                                <tr visible="false">
                                                                                    <td visible="false">
                                                                                        <asp:DropDownList ID="ddlVisit" runat="Server" Width="100%" Visible="false">
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
                                                                                    <td visible="false">
                                                                                        <asp:TextBox ID="txtVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="76%" Visible="false"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgVisit" runat="server" ImageUrl="~/Images/cal.gif" Visible="false" />
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtVisitDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="false">
                                                                                        </ajaxToolkit:MaskedEditValidator>
                                                                                   </td>
                                                                                    <td visible="false">
                                                                                        <asp:TextBox ID="txtToVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="70%" Visible="false"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgVisite1" runat="server" ImageUrl="~/Images/cal.gif" Visible="false" />
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtToVisitDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="false">
                                                                                        </ajaxToolkit:MaskedEditValidator>
                                                                                      </td>
                                                                                </tr>
                                                                                <tr>
                                                                                  
                                                                                    <td class="td-widget-bc-search-desc-ch" >
                                                                                    Provider Name</td>
                                                                                    <td class="td-widget-bc-search-desc-ch" >
                                                                                        &nbsp;</td>
                                                                                 
                                                                                </tr>
                                                                                <tr>
                                                                                    
                                                                                    <td>
                                                                                     <cc1:ExtendedDropDownList ID="extddlOffice" Width="100%" runat="server" Connection_Key="Connection_String"
                                                                                         Procedure_Name="SP_MST_OFFICE" Flag_Key_Value="OFFICE_LIST" Selected_Text="--- Select ---"/>
                                                                                        </td>
                                                                                    <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                                        &nbsp;&nbsp;</td>
                                                                                </tr>
                                                                            
                                                                               
                                                                             
                                                                                <tr>
                                                                                    <td colspan="4" style="vertical-align: middle; text-align: center">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
<asp:UpdateProgress id="UpdateProgress123" runat="server" AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="10">
                                                                                                <ProgressTemplate>
                                                                                                    <div id="DivStatus123" style="vertical-align: bottom; position:absolute; top:350px; left:600px" 
                                                                                                        runat="Server">
                                                                                                        <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                            Height="25px" Width="24px"></asp:Image>
                                                                                                        Loading...</div>
                                                                                                </ProgressTemplate>
                                                                                            </asp:UpdateProgress> &nbsp;<asp:Button style="WIDTH: 80px" id="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"></asp:Button> &nbsp;<INPUT style="WIDTH: 80px" id="btnClear1" onclick="Clear();" type=button value="Clear" runat="server" visible="false" /> <INPUT style="WIDTH: 80px" id="btnClear2" onclick="Clear1();" type=button value="Clear" runat="server" visible="false" /> 
</ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                  
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width: 40%; vertical-align: top" class="td-widget-lf-holder-ch" id="td2"
                                                                            runat="server">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td style="width: 100%;">
                                                                                        <asp:DataList ID="DtlView" runat="server" BorderWidth="0px" BorderStyle="None" BorderColor="#DEBA84"
                                                                                            RepeatColumns="1" Width="100%" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <table style="width: 100%; vertical-align: top;" border="0" cellpadding="0" cellspacing="0">
                                                                                                    <tr style="background-color: #B5DF82">
                                                                                                        <td class="td-widget-lf-desc-ch">
                                                                                                            Name
                                                                                                        </td>
                                                                                                        <td class="td-widget-lf-desc-ch">
                                                                                                            Insurance Company
                                                                                                        </td>
                                                                                                        <td class="td-widget-lf-desc-ch">
                                                                                                            Accident Date
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="center" class="lbl">
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_NAME")%>
                                                                                                        </td>
                                                                                                        <td align="center">
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME")%>
                                                                                                        </td>
                                                                                                        <td align="center">
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT", "{0:dd MMM yyyy}")%>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </ItemTemplate>
                                                                                        </asp:DataList>
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
                                <tr>
                                    <td style="width: 100%; height: auto;">
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                         <ContentTemplate>
                                        <div style="width: 100%;" id="dvSrch">
                                            <table style="height: auto; width: 100%; border: 1px solid #B5DF82;" align="left"
                                                cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px"
                                                        colspan="6">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Text="Bills"></asp:Label>
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
                                                      
                                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                            DisplayAfter="10">
                                                            <ProgressTemplate>
                                                                <div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                    runat="Server">
                                                                    <asp:Image ID="img4" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                        Height="25px" Width="24px"></asp:Image>
                                                                    Loading...</div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="lbl">
                                                       <%-- <asp:UpdatePanel ID="UpdatePaneamount" runat="server">
                                                            <ContentTemplate>--%>
                                                                <asp:Label ID="lblTotalBillAmount" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                          <%--  </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                                    </td>
                                                           <td class="lbl">
                                                           <asp:Label ID="lblPaid" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                     
                                                    </td>
                                                    <td class="lbl">
                                                    <%--    <asp:UpdatePanel ID="UpdatePaneamount1" runat="server">
                                                            <ContentTemplate>--%>
                                                                <asp:Label ID="lblOutSratingAmount" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                           <%-- </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                                    </td>
                                                   <td class="lbl">
                                                           <asp:Label ID="lblWrite" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                     
                                                    </td>
                                                              
                                                              
                                                        
                                                    <%--    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td style="width: 1017px;" colspan="5">
                                                      <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>--%>
                                                            
                                                                <table style="vertical-align: middle; width: 100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Search:" Font-Size="Small"></asp:Label><gridsearch:XGridSearchTextBox
                                                                                    ID="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input">
                                                                                </gridsearch:XGridSearchTextBox>
                                                                            </td>
                                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                                            </td>
                                                                            <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">
                                                                                Record Count:<%= this.grdBillSearch.RecordCount %>
                                                                                | Page Count:
                                                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                </gridpagination:XGridPaginationDropDown>
                                                                                <asp:LinkButton ID="lnkExportToExcel"  runat="server" OnClick="lnkExportTOExcel_onclick"
                                                                                    Text="Export TO Excel">
                                                                                <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <xgrid:XGridViewControl ID="grdBillSearch" runat="server" Height="148px" Width="100%"
                                                                    CssClass="mGrid" 
                                                                  
                                                                    AutoGenerateColumns="false" MouseOverColor="0, 153, 153" ExcelFileNamePrefix="ExcelSummary"
                                                                    ExportToExcelColumnNames="Bill No,Case #,Speciality,Visit Date,Bill Date,Bill Status,CPT,Write off,Bill Amount,Paid,Outstanding,Provider Name" 
                                                                     ExportToExcelFields="SZ_BILL_NUMBER,SZ_CASE_NO,SPECIALITY,PROC_DATE,DT_BILL_DATE,SZ_BILL_STATUS_NAME,Procedure_Codes,FLT_WRITE_OFF,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE,sz_provider_name"
                                                                    ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                                    HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                    AllowPaging="true" GridLines="None" XGridKey="Patient_Bill_Summary" 
                                                                    PageRowCount="50" PagerStyle-CssClass="pgr" AllowSorting="true" >
                                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                    <Columns>
                                                                        <%--  0--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                            visible="false" headertext="Case ID" DataField="SZ_CASE_ID" />
                                                                        <%-- 1--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                            visible="false" headertext="BillNo" DataField="SZ_BILL_NUMBER" />
                                                                        <%--  2--%>
                                                                        <xlink:XGridHyperlinkField SortExpression="convert(int,substring(TBT.SZ_BILL_NUMBER,3,len(TBT.SZ_BILL_NUMBER)))"
                                                                            headertext="Bill#" DataNavigateUrlFields="SZ_CASE_ID,SZ_BILL_NUMBER" DataNavigateUrlFormatString="Bill_Sys_BillTransaction.aspx?Type=Search&CaseID={0}&bno={1}"
                                                                            DataTextField="SZ_BILL_NUMBER">
                                                                        </xlink:XGridHyperlinkField>
                                                                        <%--CAST(MCM.SZ_CASE_NO as int)"--%>
                                                                        <%--  3--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                            SortExpression="MCM.SZ_CASE_NO" headertext="Case#" DataField="SZ_CASE_NO" />
                                                                        <%--  4--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                            SortExpression="(select SZ_PROCEDURE_GROUP from MST_PROCEDURE_GROUP where SZ_PROCEDURE_GROUP_ID =(select SZ_PROCEDURE_GROUP_ID  from MST_PROCEDURE_CODES where SZ_PROCEDURE_ID=(select top 1 SZ_PROCEDURE_ID from TXN_BILL_TRANSACTIONS_DETAIL  where SZ_BILL_NUMBER=TBT.SZ_BILL_NUMBER )))" headertext="Specialty" DataField="SPECIALITY" />
                                                                        <%--  5--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                            SortExpression="convert(nvarchar(10),TBT.DT_FIRST_VISIT_DATE,101)	+ '-'+ CONVERT(nvarchar(10), TBT.DT_LAST_VISIT_DATE,101)"
                                                                            headertext="Visit Date" DataField="PROC_DATE" />
                                                                        <%--  6--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" DataFormatString="{0:MM/dd/yyyy}"
                                                                            SortExpression="TBT.DT_BILL_DATE" headertext="Bill Date" DataField="DT_BILL_DATE" />
                                                                        <%--  7--%>
                                                                       
                                                                        <asp:TemplateField HeaderText="Bill Status" SortExpression="MBS.SZ_BILL_STATUS_NAME">
                                                                            <itemtemplate>
                                                                                        <asp:Label    id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_NAME")%>'></asp:Label>
                                                                                    </itemtemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                         <%--  8--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                             headertext="CPT" DataField="Procedure_Codes" />
                                                                            
                                                                        <%--  9 --%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                            SortExpression="TBT.FLT_WRITE_OFF" headertext="Write Off" DataFormatString="{0:C}"
                                                                            DataField="FLT_WRITE_OFF" />
                                                                        <%--  10 --%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                            SortExpression="CAST(TBT.FLT_BILL_AMOUNT  as float)" headertext="Bill Amount"
                                                                            DataFormatString="{0:C}" DataField="FLT_BILL_AMOUNT" />
                                                                        <%--  11--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                            SortExpression="CAST(ISNULL((SELECT SUM(FLT_CHECK_AMOUNT) FROM TXN_PAYMENT_TRANSACTIONS WHERE SZ_BILL_ID=TBT.SZ_BILL_NUMBER),0)as float)"
                                                                            headertext="Paid" DataFormatString="{0:C}" DataField="PAID_AMOUNT" />
                                                                        <%--  12--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                            SortExpression="CAST(TBT.FLT_BALANCE as float)" headertext="Outstanding" DataFormatString="{0:C}"
                                                                            DataField="FLT_BALANCE" />
                                                                            
                                                                            <%--  13--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                            SortExpression="MO.sz_office" headertext="Provider Name" 
                                                                            DataField="sz_provider_name" />
                                                              
                                                                       
                                                                    
                                                                        
                                                              
                                                                    
                                                                   
                                                                  
                                                                         
                                                                       
                                                                          
                                                                    </Columns>
                                                                </xgrid:XGridViewControl>
                                                           <%-- </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        </ContentTemplate>
                                       
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="height: auto; width: 100%; border: 0px solid #B5DF82; background-color: White;">
                                            <tr>
                                                <td align="right" style="width: 50%; height: 20px">
                                                </td>
                                                <td align="left" style="width: 50%; height: 20px">
                                                    <asp:TextBox ID="txtRange" runat="server" Visible="false" Width="15px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtVisitDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtToVisitDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtVisitDate"
                                                        PopupButtonID="imgVisit" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtToVisitDate"
                                                        PopupButtonID="imgVisit1" />
                                                    <asp:TextBox ID="txtGroupId" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtBillStatusID" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                                                   <asp:TextBox ID="txtOfficeId" runat="server" Visible="false"></asp:TextBox>
                                                   <asp:TextBox ID="txtCaseId" runat="server" Visible="false" ></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 10px; height: 100%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <%--<td style="width: 10px">
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    

    
</asp:Content>
