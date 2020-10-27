<%@ Page Title="Green Your Bills - Unprocess Bill" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewPOM.aspx.cs" Inherits="NewPOM"  %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript" src="../js/jquery.maskedinput.js"></script>
    <script type="text/javascript" src="../js/jquery.maskedinput.min.js"></script>
    <link rel="Stylesheet" href="../Css/admin.css" type="text/css" />
    <script type="text/javascript">
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
            var selValue = document.getElementById("<%=ddlDateValues.ClientID%>").value;
            if (selValue == 0) {
                document.getElementById("<%=txtToBillDate.ClientID%>").value = "";
                document.getElementById("<%=txtFromBillDate.ClientID%>").value = "";
            }
            else if (selValue == 1) {
                document.getElementById("<%=txtToBillDate.ClientID%>").value = getDate('today');
            document.getElementById("<%=txtFromBillDate.ClientID%>").value = getDate('today');
        }
        else if (selValue == 2) {
            document.getElementById("<%=txtToBillDate.ClientID%>").value = getWeek('endweek');
            document.getElementById("<%=txtFromBillDate.ClientID%>").value = getWeek('startweek');
        }
        else if (selValue == 3) {
            document.getElementById("<%=txtToBillDate.ClientID%>").value = getDate('monthend');
            document.getElementById("<%=txtFromBillDate.ClientID%>").value = getDate('monthstart');
        }
        else if (selValue == 4) {
            document.getElementById("<%=txtToBillDate.ClientID%>").value = getDate('quarterend');
            document.getElementById("<%=txtFromBillDate.ClientID%>").value = getDate('quarterstart');
        }
        else if (selValue == 5) {
            document.getElementById("<%=txtToBillDate.ClientID%>").value = getDate('yearend');
            document.getElementById("<%=txtFromBillDate.ClientID%>").value = getDate('yearstart');
        }
        else if (selValue == 6) {
            document.getElementById("<%=txtToBillDate.ClientID%>").value = getLastWeek('endweek');
            document.getElementById("<%=txtFromBillDate.ClientID%>").value = getLastWeek('startweek');
        } else if (selValue == 7) {
            document.getElementById("<%=txtToBillDate.ClientID%>").value = lastmonth('endmonth');

            document.getElementById("<%=txtFromBillDate.ClientID%>").value = lastmonth('startmonth');
        } else if (selValue == 8) {
            document.getElementById("<%=txtToBillDate.ClientID%>").value = lastyear('endyear');

            document.getElementById("<%=txtFromBillDate.ClientID%>").value = lastyear('startyear');
        } else if (selValue == 9) {
            document.getElementById("<%=txtToBillDate.ClientID%>").value = quarteryear('endquaeter');

            document.getElementById("<%=txtFromBillDate.ClientID%>").value = quarteryear('startquaeter');
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
            return ('12/01/' + y);

        }
        else {
            var m = t_mon - 1;
            if (m < 10) {
                return ('0' + m + '/01/' + t_year);
            }
            else {
                return (m + '/01/' + t_year);
            }

        }
    }
    else if (type == 'endmonth') {
        if (t_mon == 1) {
            var y = t_year - 1;
            return ('12/31/' + y);
        } else {
            var m = t_mon - 1;
            var d = daysInMonth(t_mon - 1, t_year);
            if (m < 10) {
                return ('0' + m + '/' + d + '/' + t_year);
            }
            else {
                return (m + '/' + d + '/' + t_year);
            }

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
            return ('10/01/' + y);
        }
        else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
            return ('01/01/' + t_year);

        } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
            return ('04/01/' + t_year);


        } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
            return ('07/01/' + t_year);

        }

    } else if (type == 'endquaeter') {
        if (t_mon == 1 || t_mon == 2 || t_mon == 3) {
            //
            var y = t_year - 1;
            return ('12/31/' + y);
        }
        else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
            return ('03/31/' + t_year);

        } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
            return ('06/30/' + t_year);


        } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
            return ('09/30/' + t_year);
        }

    }

}

function lastyear(type) {
    var d = new Date();

    var t_year = d.getFullYear();
    if (type == 'startyear') {
        y = t_year - 1;
        return ('01/01/' + y);
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
    // alert("Hi");

    if (day < 10 && (month + 1) < 10) {
        return '0' + (month + 1) + '/0' + day + '/' + year;
    }
    if (day < 10 && (month + 1) >= 10) {
        return '' + (month + 1) + '/0' + day + '/' + year;
    }
    if (day >= 10 && (month + 1) < 10) {
        return '0' + (month + 1) + '/' + day + '/' + year;
    }
    if (day >= 10 && (month + 1) >= 10) {
        return '' + (month + 1) + '/' + day + '/' + year;
    }



}

function getFormattedDateForMonth(day, month, year) {

    if (day < 10 && month < 10) {
        return '0' + (month) + '/0' + day + '/' + year;
    }
    if (day < 10 && month > 10) {
        return '' + (month) + '/0' + day + '/' + year;
    }
    if (day > 10 && month < 10) {
        return '0' + (month) + '/' + day + '/' + year;
    }
    if (day > 10 && month > 10) {
        return '' + (month) + '/' + day + '/' + year;
    }

    if (day < 10 && month == 10) {
        return '' + (month) + '/0' + day + '/' + year;
    }
    if (day > 10 && month == 10) {
        return '' + (month) + '/' + day + '/' + year;
    }
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
function SetBillDate() {
    getWeek();
    var selValue = document.getElementById("<%=ddlBillDate.ClientID%>").value;
    if (selValue == 0) {
        document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value = "";
        document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value = "";
    }
    else if (selValue == 1) {
        document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value = getDate('today');
            document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value = getDate('today');
        }
        else if (selValue == 2) {
            document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value = getWeek('endweek');
            document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value = getWeek('startweek');
        }
        else if (selValue == 3) {
            document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value = getDate('monthend');
            document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value = getDate('monthstart');
        }
        else if (selValue == 4) {
            document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value = getDate('quarterend');
            document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value = getDate('quarterstart');
        }
        else if (selValue == 5) {
            document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value = getDate('yearend');
            document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value = getDate('yearstart');
        }
        else if (selValue == 6) {
            document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value = getLastWeek('endweek');
            document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value = getLastWeek('startweek');
        } else if (selValue == 7) {
            document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value = lastmonth('endmonth');

            document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value = lastmonth('startmonth');
        } else if (selValue == 8) {
            document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value = lastyear('endyear');

            document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value = lastyear('startyear');
        } else if (selValue == 9) {
            document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value = quarteryear('endquaeter');

            document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value = quarteryear('startquaeter');
        }
}
    </script>
    <script language="javascript" type="text/javascript">
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
    </script>
    <script type="text/javascript" language="javascript">
        function FromDateValidation() {
            var year1 = "";
            year1 = document.getElementById("<%=txtFromBillDate.ClientID%>").value;
            if (year1.charAt(0) == '_' && year1.charAt(1) == '_' && year1.charAt(2) == '/' && year1.charAt(3) == '_' && year1.charAt(4) == '_' && year1.charAt(5) == '/' && year1.charAt(6) == '_' && year1.charAt(7) == '_' && year1.charAt(8) == '_' && year1.charAt(9) == '_') {
                return true;
            }
            if ((year1.charAt(6) == '_' && year1.charAt(7) == '_') || (year1.charAt(8) == '_' && year1.charAt(9) == '_') || (year1.charAt(6) == '0' && year1.charAt(7) == '0') || (year1.charAt(6) == '0') || (year1.charAt(9) == '_')) {
                document.getElementById("<%=lblValidator1.ClientID%>").innerText = 'Date is Invalid';
            document.getElementById("<%=txtFromBillDate.ClientID%>").focus();
            return false;
        }
        if ((year1.charAt(6) != '_' && year1.charAt(7) != '_') || (year1.charAt(8) != '_' && year1.charAt(9) != '_') || (year1.charAt(6) != '0' && year1.charAt(7) != '0')) {
            document.getElementById("<%=lblValidator1.ClientID%>").innerText = '';
            return true;
        }

    }

    function ToDateValidation() {
        var year2 = "";
        year2 = document.getElementById("<%=txtToBillDate.ClientID%>").value;

        if (year2.charAt(0) == '_' && year2.charAt(1) == '_' && year2.charAt(2) == '/' && year2.charAt(3) == '_' && year2.charAt(4) == '_' && year2.charAt(5) == '/' && year2.charAt(6) == '_' && year2.charAt(7) == '_' && year2.charAt(8) == '_' && year2.charAt(9) == '_') {
            return true;
        }
        if ((year2.charAt(6) == '_' && year2.charAt(7) == '_') || (year2.charAt(8) == '_' && year2.charAt(9) == '_') || (year2.charAt(6) == '0' && year2.charAt(7) == '0') || (year2.charAt(6) == '0') || (year2.charAt(9) == '_')) {
            document.getElementById("<%=lblValid1.ClientID%>").innerText = 'Date is Invalid';
            document.getElementById("<%=txtToBillDate.ClientID%>").focus();
            return false;
        }
        if ((year2.charAt(6) != '_' && year2.charAt(7) != '_') || (year2.charAt(8) != '_' && year2.charAt(9) != '_') || (year2.charAt(6) != '0' && year2.charAt(7) != '0')) {
            document.getElementById("<%=lblValid1.ClientID%>").innerText = '';
            return true;
        }

    }
    function Clear() {
        document.getElementById("<%=txtToBillDate.ClientID %>").value = '';
        document.getElementById("<%=txtFromBillDate.ClientID %>").value = '';
        document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value = 'NA';
        document.getElementById("ctl00_ContentPlaceHolder1_extddlOffice").value = 'NA';
        document.getElementById("<%=ddlDateValues.ClientID %>").value = 0;
        document.getElementById("<%=txt_Bill_from_Date.ClientID %>").value = '';
        document.getElementById("<%=txt_Bill_to_Date.ClientID %>").value = '';
        document.getElementById("<%=ddlBillDate.ClientID %>").value = 0;
        document.getElementById("<%=txt_search_POM.ClientID%>").value = '';
        document.getElementById("<%=txt_search_Bill.ClientID%>").value = '';
        
    }
</script>
    <script type="text/javascript" language="javascript">
        function FromDateValidation_Bill() {
            var year1 = "";
            year1 = document.getElementById("<%=txt_Bill_from_Date.ClientID%>").value;
            if (year1.charAt(0) == '_' && year1.charAt(1) == '_' && year1.charAt(2) == '/' && year1.charAt(3) == '_' && year1.charAt(4) == '_' && year1.charAt(5) == '/' && year1.charAt(6) == '_' && year1.charAt(7) == '_' && year1.charAt(8) == '_' && year1.charAt(9) == '_') {
                return true;
            }
            if ((year1.charAt(6) == '_' && year1.charAt(7) == '_') || (year1.charAt(8) == '_' && year1.charAt(9) == '_') || (year1.charAt(6) == '0' && year1.charAt(7) == '0') || (year1.charAt(6) == '0') || (year1.charAt(9) == '_')) {
                document.getElementById("<%=lblValidator1.ClientID%>").innerText = 'Date is Invalid';
                document.getElementById("<%=txt_Bill_from_Date.ClientID%>").focus();
                return false;
            }
            if ((year1.charAt(6) != '_' && year1.charAt(7) != '_') || (year1.charAt(8) != '_' && year1.charAt(9) != '_') || (year1.charAt(6) != '0' && year1.charAt(7) != '0')) {
                document.getElementById("<%=lblValidator1.ClientID%>").innerText = '';
            return true;
        }

    }

    function ToDateValidation_Bill() {
        var year2 = "";
        year2 = document.getElementById("<%=txt_Bill_to_Date.ClientID%>").value;

        if (year2.charAt(0) == '_' && year2.charAt(1) == '_' && year2.charAt(2) == '/' && year2.charAt(3) == '_' && year2.charAt(4) == '_' && year2.charAt(5) == '/' && year2.charAt(6) == '_' && year2.charAt(7) == '_' && year2.charAt(8) == '_' && year2.charAt(9) == '_') {
            return true;
        }
        if ((year2.charAt(6) == '_' && year2.charAt(7) == '_') || (year2.charAt(8) == '_' && year2.charAt(9) == '_') || (year2.charAt(6) == '0' && year2.charAt(7) == '0') || (year2.charAt(6) == '0') || (year2.charAt(9) == '_')) {
            document.getElementById("<%=lblValid1.ClientID%>").innerText = 'Date is Invalid';
            document.getElementById("<%=txt_Bill_to_Date.ClientID%>").focus();
            return false;
        }
        if ((year2.charAt(6) != '_' && year2.charAt(7) != '_') || (year2.charAt(8) != '_' && year2.charAt(9) != '_') || (year2.charAt(6) != '0' && year2.charAt(7) != '0')) {
            document.getElementById("<%=lblValid1.ClientID%>").innerText = '';
            return true;
        }

    }
    function txt_Validate() {
            var txt_POM = document.getElementById("<%=txt_search_POM.ClientID%>").value.trim();
            var txt_Bill = document.getElementById("<%=txt_search_Bill.ClientID%>").value.trim();
            if (txt_POM == "" && txt_Bill == "") {
                alert('POM days or Bill days can not be blank');
                document.getElementById("<%=txt_search_POM.ClientID%>").focus();
                return false;
            }
    }
</script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="width: 100%;">
                <table width="100%" style="background-color: white">
                    <tr>
                        <td valign="top" style="width: 100%;">

                            <div style="width: 100%; vertical-align: top;">
                                <table id="page-title" style="width: 100%;">
                                    <tr style="width: 100%;">
                                        <td>Search Parameters
                                        </td>
                                    </tr>
                                </table>
                                <table id="manage-reg-filters" width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100%; vertical-align: top;">
                                            <table style="width: 100%;">
                                                <tr style="width: 100%;">
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            POM
                                                        </label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            From Date</label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            To Date</label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%;">
                                                    <td class="registration-form-ibox">
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
                                                    <td class="registration-form-ibox">
                                                        <asp:TextBox ID="txtFromBillDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="inputBox" MaxLength="10" Width="87%"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnFromBillDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender4" ControlToValidate="txtFromBillDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>

                                                    </td>
                                                    <td class="registration-form-ibox">
                                                        <asp:TextBox ID="txtToBillDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="inputBox" MaxLength="10" Width="87%"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnToBillDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <asp:Label ID="lblValid1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender5" ControlToValidate="txtToBillDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>

                                                    </td>
                                                    <td class="registration-form-ibox"></td>
                                                </tr>
                                                <tr style="width: 100%;">
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            Bill</label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            From Date</label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            To Date</label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%;">
                                                    <td class="registration-form-ibox">
                                                        <asp:DropDownList ID="ddlBillDate" runat="Server" Width="100%">
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
                                                    <td class="registration-form-ibox">
                                                        <asp:TextBox ID="txt_Bill_from_Date" runat="server" MaxLength="10" Width="271px" CssClass="inputBox" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                        <asp:ImageButton ID="img_Btn_Bill_from_date" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txt_Bill_from_Date" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                    </td>
                                                    <td class="registration-form-ibox">
                                                        <asp:TextBox ID="txt_Bill_to_Date" runat="server" MaxLength="10" Width="271px" CssClass="inputBox" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                        <asp:ImageButton ID="img_Btn_Bill_to_date" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <asp:Label ID="Label3" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txt_Bill_to_Date" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                    </td>
                                                    <td class="registration-form-ibox"></td>
                                                </tr>
                                                <tr style="width: 100%;">
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            Specialty</label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            Provider</label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                        </label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%;">
                                                    <td class="registration-form-ibox">
                                                        <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                            Procedure_Name="SP_MST_PROCEDURE_GROUP" Selected_Text="--- Select ---" Width="295px" />
                                                    </td>
                                                    <td class="registration-form-ibox">
                                                        <cc1:ExtendedDropDownList ID="extddlOffice" Width="295px" runat="server" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_OFFICE" Flag_Key_Value="OFFICE_LIST" Selected_Text="--- Select ---" />
                                                    </td>
                                                    <td class="registration-form-ibox"></td>

                                                    <td class="registration-form-ibox"></td>
                                                </tr>
                                                <tr style="width: 100%;">
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            Days from POM</label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            Days from Bill</label>
                                                    </td>
                                                    <td>
                                                        <label>
                                                        </label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%;">
                                                    <td class="registration-form-ibox">
                                                        <asp:TextBox ID="txt_search_POM" runat="server" CssClass="inputBox" MaxLength="10" Width="100%" Text="30"></asp:TextBox>
                                                    </td>
                                                    <td class="registration-form-ibox">
                                                        <asp:TextBox ID="txt_search_Bill" runat="server" CssClass="inputBox" MaxLength="10" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td class="registration-form-ibox"></td>
                                                    <td class="registration-form-ibox"></td>
                                                </tr>
                                                <tr style="width: 100%;">
                                                    <td style="height: 19px" align="center"></td>
                                                    <td style="height: 19px" align="center"></td>
                                                    <td style="height: 19px" align="center"></td>
                                                </tr>
                                                <tr style="width: 100%;">
                                                    <td align="center" colspan="3">
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" Height="30px" BorderStyle="None" BackColor="#555555"
                                                            onClick="btnSearch_Click" ForeColor="White" OnClientClick="return txt_Validate();" />
                                                        <input style="width: 100px; height: 30px; background-color: #555555; color: white; border-style: none;" id="btnClear1" onclick="Clear();" type="button" value="Clear"
                                                            runat="server" />

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 130%">
                                                <table style="vertical-align: middle; width: 100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 413px" class="txt2" valign="middle" align="left" bgcolor="#336699"
                                                                colspan="6" height="13">
                                                                
                                                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                                    DisplayAfter="10">
                                                                    <ProgressTemplate>
                                                                        <div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                            runat="Server">
                                                                            <asp:Image ID="img4" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                Height="25px" Width="24px"></asp:Image>
                                                                            Loading...
                                                                        </div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtBillingOfficeFlag" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
                <table style="width:100%" >
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
        <td style="width: 90%">
                <dx:ASPxGridView ID="grdPOMReport" runat="server" 
                    Border-BorderColor="#aaaaaa" SettingsPager-PageSize="20" Width="100%" AutoGenerateColumns="False"
                    Settings-ShowHorizontalScrollBar="true" Settings-ShowGroupedColumns="true" Settings-VerticalScrollableHeight="330"
                    Settings-ShowGroupPanel="true" 
                    SettingsBehavior-AllowGroup="true" Settings-ShowFooter="true" SettingsBehavior-AutoExpandAllGroups="true">
                    <Columns>
                        <dx:GridViewDataColumn Caption="Case#"  VisibleIndex="1"  FieldName="CASE #" 
                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <Settings AllowSort="True" />
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Bill Number" VisibleIndex="2" FieldName="BillNo" Settings-AllowSort="True"
                            HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <Settings AllowSort="True" />
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Patient Name" VisibleIndex="3" width="180px" FieldName="PATIENT_NAME"
                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <Settings AllowSort="True" />
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Visit Date" VisibleIndex="4" width="150px" FieldName="VISIT_DATE"
                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <Settings AllowSort="True" />
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Specialty" VisibleIndex="5" FieldName="Specialty"
                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <Settings AllowSort="True" />
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Doctor" VisibleIndex="6" width="200px" FieldName="DOCTOR NAME"
                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <Settings AllowSort="True" />
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn Caption="Provider" VisibleIndex="7" width="200px" FieldName="PROVIDER_NAME"
                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <PropertiesTextEdit DisplayFormatString="c" />
                            <Settings AllowSort="True" />
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Date Of POM" VisibleIndex="8" FieldName="DT_POM_DATE"
                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                            <PropertiesTextEdit DisplayFormatString="c" />
                            <Settings AllowSort="True" />
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AutoExpandAllGroups="True" />
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                    <Settings ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded"
                        ShowGroupButtons="false" />
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdPOMReport">
                </dx:ASPxGridViewExporter>
            </td>
    </tr>
</table>
            </div>
            
            <ajaxToolkit:CalendarExtender ID="calExtFromBillDate" runat="server" TargetControlID="txtFromBillDate"
                PopupButtonID="imgbtnFromBillDate" PopupPosition="BottomRight"/>
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                MaskType="Date" TargetControlID="txtFromBillDate" PromptCharacter="_" AutoComplete="true">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:CalendarExtender ID="calExtToBillDate" runat="server" TargetControlID="txtToBillDate"
                PopupButtonID="imgbtnToBillDate" PopupPosition="BottomRight"/>
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                MaskType="Date" TargetControlID="txtToBillDate" PromptCharacter="_" AutoComplete="true">
            </ajaxToolkit:MaskedEditExtender>

            <ajaxToolkit:CalendarExtender ID="cal_ext_Bill_from_bill_date" runat="server" TargetControlID="txt_Bill_from_Date"
                PopupButtonID="img_Btn_Bill_from_date" PopupPosition="BottomRight" />
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                MaskType="Date" TargetControlID="txt_Bill_from_Date" PromptCharacter="_" AutoComplete="true">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:CalendarExtender ID="cal_ext_Bill_to_date" runat="server" TargetControlID="txt_Bill_to_Date" 
                PopupButtonID="img_Btn_Bill_to_date" PopupPosition="BottomRight"/>
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                MaskType="Date" TargetControlID="txt_Bill_to_Date" PromptCharacter="_" AutoComplete="true">
            </ajaxToolkit:MaskedEditExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <dx:ASPxPopupControl ID="IFrame_DownloadFiles" runat="server" CloseAction="CloseButton"
    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ClientInstanceName="IFrame_DownloadFiles" HeaderText="Data Export" HeaderStyle-HorizontalAlign="Left"
    HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
    EnableAnimation="False" EnableViewState="True" Width="600px" ToolTip="Download File(s)"
    PopupHorizontalOffset="0" PopupVerticalOffset="0"   AutoUpdatePosition="true"
    ScrollBars="Auto" RenderIFrameForPopupElements="Default" Height="200px">
    <ContentStyle>
        <Paddings PaddingBottom="5px" />
    </ContentStyle>
</dx:ASPxPopupControl>

</asp:Content>