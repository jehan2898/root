<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="LitigationDesk" CodeFile="Bill_Sys_LitigationDesk.aspx.cs" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script src="../js/jquery-1.8.2.js" type="text/javascript"></script>
      <script type="text/javascript">
        $(document).ready(function () {
            $("input:checkbox").click(function () {
                
                GetCheckedRows();
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();

            parameter.add_endRequest(function () {
                $("input:checkbox").click(function () {
                    
                    GetCheckedRows();
                });
            });
            function GetCheckedRows() {
                debugger;
                var sum = 0;
                var paid = 0;
                var liti = 0;
                $("#<%=grdLitigationDesk.ClientID%> tr").each(function () {

                    var checkBox = $(this).find("input[type='checkbox']");
                    if ($(checkBox).is(':checked')) {
                        var value = $(this).closest('tr').find('td:eq(7)').text();
                        var valuepaid = $(this).closest('tr').find('td:eq(8)').text();
                        var valueLiti = $(this).closest('tr').find('td:eq(9)').text();
                        //alert(value);
                        var intRegex = /^-?\d+$/;
                        var floatRegex = /^-?((\d+(\.\d *)?)|((\d*\.)?\d+))$/;
                        if (intRegex.test(value.replace("$", "").replace("(", "-").replace(")", "")) || floatRegex.test(value.replace("$", "").replace("(", "-").replace(")", ""))) {
                            var iNum = parseFloat(value.replace("$", "").replace("(", "-").replace(")", ""));
                            
                            sum = sum + iNum;
                        }
                        if (intRegex.test(valuepaid.replace("$", "").replace("(", "-").replace(")", "")) || floatRegex.test(valuepaid.replace("$", "").replace("(", "-").replace(")", ""))) {
                            var iNum = parseFloat(valuepaid.replace("$", "").replace("(", "-").replace(")", ""));
                            paid = paid + iNum;
                        }
                        
                        if (intRegex.test(valueLiti.replace("$", "").replace("(", "-").replace(")", "")) || floatRegex.test(valueLiti.replace("$", "").replace("(", "-").replace(")", ""))) {
                            
                            var iNum = parseFloat(valueLiti.replace("$", "").replace("(", "-").replace(")", ""));
                            liti = liti + iNum;
                        }

                    }

                });
                sum = sum.toFixed(2);
                paid = paid.toFixed(2);
                liti = liti.toFixed(2);

                sum = 'Bill Amout: $' + sum;
                paid = 'Paid Amount: $' + paid;
                liti = 'Litigated Amount: $' + liti;

                $("#divSum").html(sum);
                $("#divpaid").html(paid);
                $("#divLiti").html(liti);
                ////$("#grdLitigationDesk tr").each(function () {
                //    var checkBox = $(this).find("input[type='checkbox']");

                //    if ($(checkBox).is(':checked')) {
                //        alert('click');
                //    }
                //});
            }

        });
    </script>
    <script type="text/javascript">


        function OnCaseTypeSelectionChanged(listBoxcase, argscase) {
            if (argscase.index == 0)
                argscase.isSelected ? listBoxcase.SelectAll() : listBoxcase.UnselectAll();

            UpdateSelectAllItemCaseType();
            UpdateTextCaseType();
        }

        function UpdateSelectAllItemCaseType(listBoxcase) {
            IsAllSelectedCaseType() ? clstCaseType.SelectIndices([0]) : clstCaseType.UnselectIndices([0]);
        }
        function IsAllSelectedCaseType() {
            for (var i = 1; i < clstCaseType.GetItemCount() ; i++)
                if (!clstCaseType.GetItem(i).selected)
                    return false;
            return true;
        }
        function UpdateTextCaseType() {
            var selectedItems = clstCaseType.GetSelectedItems();
            clstCaseType.SetText(GetSelectedItemsText(selectedItems));
        }

        function ShowDenialChildGrid(obj) {
            var div1 = document.getElementById(obj);
            div1.style.display = 'block';
        }


        function SelectAll(ival) {
            var f = document.getElementById("<%= grdLitigationDesk.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {



                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }


                    //			                 				

                }


            }
        }



        function ClearFields() {
            document.getElementById("<%=txtCaseNumber.ClientID%>").value = "";
            document.getElementById("<%=txtPatientName.ClientID%>").value = "";
            document.getElementById("<%=txtFromDate.ClientID%>").value = "";
            document.getElementById("<%=txtToDate.ClientID%>").value = "";
            document.getElementById("<%=ddlDateValues.ClientID%>").value = "0";
            // document.getElementById("ctl00_ContentPlaceHolder1_extddlInsurance").value="NA";
            document.getElementById("<%=extddlInsurance.ClientID %>").value = "NA";
            //document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseStatus").value="NA";
            document.getElementById("<%=extddlCaseStatus.ClientID %>").value = "NA";
            //document.getElementById("ctl00_ContentPlaceHolder1_extdlitigate").value="NA";
            document.getElementById("<%=extdlitigate.ClientID %>").value = "NA";
        }


        function OpenDocManager(CaseNo, CaseId, cmpid) {
            window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid=' + CaseId + '&caseno=' + CaseNo + '&cmpid=' + cmpid, 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
        }



        function LitigationConformation() {
            var flag = 0;
            var f = document.getElementById("<%= grdLitigationDesk.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        flag = 1;
                    }
                }
            }

            if (flag == 0) {
                alert('Select atleast 1 case to litigate');
                return false;
            }
            else {
                if (document.getElementById("<%=ddlTransferStatus.ClientID %>").value != '1') {
                    //if(document.getElementById('ctl00_ContentPlaceHolder1_extddlUserLawFirm').value != 'NA')
                    if (document.getElementById("<%=extddlUserLawFirm.ClientID %>").value != 'NA') {
                        if (confirm('LawFirm Will Get Assign And Will Not RollBack. Do You Want To Continue?')) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        alert('Select a Law-Firm before you send cases for litigation');
                        return false;
                    }
                }
                else {
                    if (confirm('LawFirm Will Get Assign And Will Not RollBack. Do You Want To Continue?')) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }

        <%--function LitigationConfirmationforFullyTransferd()
        {
            debugger;
            alert('hi');
            var flag = 0;
            var f = document.getElementById("<%= grdLitigationDesk.ClientID %>");
           
               if (document.getElementById("<%=extddlUserLawFirm.ClientID %>").value != 'NA') {
                 
                for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            alert('somthing');
                        }
                    }
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
function ShowChildGrid(obj) {
    var div = document.getElementById(obj);
    div.style.display = 'block';
}

function HideChildGrid(obj) {
    var div = document.getElementById(obj);
    div.style.display = 'none';
}
function LawFirmValidation() {
    alert('Please Select LawFirm...');
}

function CheckBoxValidation() {
    alert('Please Select Case...');
}

    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
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
                        <td style="height: 100%"></td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%; background-color: White;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="text-align: left; width: 50%; vertical-align: top;">
                                                    <table style="text-align: left; width: 100%;">
                                                        <tr>
                                                            <td style="text-align: left; width: 50%; vertical-align: top;">
                                                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%; border: 1px solid #B5DF82;">
                                                                    <tr>
                                                                        <td style="width: 40%; height: 0px;" align="center">
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" onkeypress="javascript:return WebForm_FireDefaultButton(event, <%=btnSearch.ClientID %>)">
                                                                                <tr>
                                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                                        <b class="txt3">Search Parameters</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">Case No
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">Patient Name
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">Transfer Status
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" style="height: 24px">
                                                                                        <asp:TextBox ID="txtCaseNumber" runat="server" Width="78%"></asp:TextBox>&nbsp;
                                                                                    </td>
                                                                                    <td style="width: 33%; height: 24px;" align="center">
                                                                                        <asp:TextBox ID="txtPatientName" runat="server" Width="80%"></asp:TextBox>
                                                                                    </td>
                                                                                    <%--<td style="width: 148px; height: 24px;" align="center">--%>
                                                                                    <td align="center">
                                                                                        <asp:DropDownList ID="ddlTransferStatus" runat="server" Width="80%">
                                                                                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="Not Transfered" Value="3" Selected="True"></asp:ListItem>
                                                                                            <%--<asp:ListItem Text="Partially Transfered" Value="1"></asp:ListItem>--%>
                                                                                            <asp:ListItem Text="Fully Transfered" Value="2"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">Case Status
                                                                                    </td>

                                                                                    <td class="td-widget-bc-search-desc-ch">Case Type

                                                                                    </td>
                                                                                    


                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Specialty


                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="90%" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="CASESTATUS_LIST" Procedure_Name="SP_MST_CASE_STATUS"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch" align="center">
                                                                                        <dx:ASPxDropDownEdit ID="ddleCaseType" runat="server" Width="85%"
                                                                                            ClientInstanceName="cddleCaseType">
                                                                                            <DropDownWindowStyle>
                                                                                            </DropDownWindowStyle>
                                                                                            <DropDownWindowTemplate>
                                                                                                <dx:ASPxListBox ID="lstCaseType" runat="server" Width="100%"
                                                                                                    ClientInstanceName="clstCaseType" SelectionMode="CheckColumn">
                                                                                                    <Border BorderStyle="None" />
                                                                                                    <BorderBottom BorderStyle="Solid"
                                                                                                        BorderWidth="1px" />
                                                                                                    <Items>
                                                                                                    </Items>
                                                                                                    <ClientSideEvents SelectedIndexChanged="OnCaseTypeSelectionChanged" />
                                                                                                </dx:ASPxListBox>
                                                                                            </DropDownWindowTemplate>
                                                                                        </dx:ASPxDropDownEdit>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Width="90%" Selected_Text="---Select---"
                                                                                            Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                                                            Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                    <%--<td align="center" colspan="2">
                                                                                        <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="99%" Selected_Text="---Select---"
                                                                                            Style="float: left;" Connection_Key="Connection_String" Flag_Key_Value="INSURANCE_LIST"
                                                                                            Procedure_Name="SP_MST_INSURANCE_COMPANY"></extddl:ExtendedDropDownList>
                                                                                    </td>--%>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">Litigation Date
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">From
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">To
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch3" align="center">
                                                                                        <asp:DropDownList ID="ddlDateValues" runat="Server" Width="82%">
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
                                                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            CssClass="text-box" MaxLength="10" Width="70%"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" Style="float: left;" />
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator></td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            CssClass="text-box" MaxLength="10" Width="70%"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" Style="float: left;" />
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" colspan="3" style="width: 500px">Denial Reason
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" colspan="3" valign="top">
                                                                                        <extddl:ExtendedDropDownList ID="extdlitigate" Width="99%" runat="server" Connection_Key="Connection_String"
                                                                                            Procedure_Name="SP_MST_DENIAL" Flag_Key_Value="DENIAL_LIST" Selected_Text="--- Select ---"
                                                                                            CssClass="cinput" Visible="true" />
                                                                                    </td>
                                                                                    <%--    <td align="center">
                                                                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                        <contenttemplate>
                                                                                            &nbsp;<asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server"
                                                                                                Text="Search"></asp:Button>
                                                                                        </contenttemplate>
                                                                                    </asp:UpdatePanel>
                                                                            </td>--%>
                                                                                    <%-- <td colspan="3" style="vertical-align: middle; text-align: center">
                                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                        <contenttemplate>
                                                                                            &nbsp;<asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server"
                                                                                                Text="Search"></asp:Button>
                                                                                        </contenttemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>--%>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" colspan="3" style="width: 500px">Provider Name
                                                                                    </td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" colspan="3" valign="top">
                                                                                        <extddl:ExtendedDropDownList ID="extddlProvider" runat="server" Width="99%" Selected_Text="---Select---"
                                                                                            Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_MST_OFFICE" Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                    </td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" colspan="3" style="width: 500px">Carrier
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" colspan="3" valign="top">
                                                                                        <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="99%" Selected_Text="---Select---"
                                                                                            Style="float: left;" Connection_Key="Connection_String" Flag_Key_Value="INSURANCE_LIST"
                                                                                            Procedure_Name="SP_MST_INSURANCE_COMPANY"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="3" align="center">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
                                                                                                &nbsp;<asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="20%"
                                                                                                    Text="Search" />
                                                                                                <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" Width="20%" />
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="text-align: right; width: 50%; vertical-align: text-top;">
                                                                <table style="width: 80%; border: 1px solid #B5DF82;" class="txt2" align="right"
                                                                    cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 50%">
                                                                            <b class="txt3">Account Summary</b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 45%; text-align: center; vertical-align: top; padding: 2px;">
                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                <ContentTemplate>
                                                                                    <xgrid:XGridViewControl ID="grdLitigationCompanyWise" runat="server" Height="148px"
                                                                                        Width="385px" CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153"
                                                                                        ExcelFileNamePrefix="ExcelLitigation" ShowExcelTableBorder="true" EnableRowClick="false"
                                                                                        ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                                        AllowPaging="true" GridLines="None" XGridKey="LitigationCompanyWise" PageRowCount="10" PagerStyle-CssClass="pgr"
                                                                                        AllowSorting="true" OnRowCommand="grdLitigationCompanyWise_RowCommand" DataKeyNames="lawfirm_id">
                                                                                        <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Text" HeaderText="Title">
                                                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--     <asp:BoundField DataField="amount" DataFormatString="{0:C}" HeaderText="Amount($)">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>   --%>

                                                                                            <%-- Bill_Sys_BillTransaction.aspx?Type=Search&CaseID={0}&bno={1}--%>
                                                                                            <asp:TemplateField HeaderText="Amount($)" SortExpression="">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.amount")%>'></asp:Label>

                                                                                                    <asp:HyperLink ID="hamount" Font-Underline="false" runat="server" NavigateUrl='<%# "Bill_Sys_DeskInFo.aspx?LfirmId="+ DataBinder.Eval(Container, "DataItem.lawfirm_id") %>'>
                                                                             <%# DataBinder.Eval(Container,"DataItem.amount")%></asp:HyperLink>



                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkPlus" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PLS" Font-Size="15px"
                                                                                                        Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkMinus" Font-Underline="false" runat="server" CausesValidation="false" CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Font-Size="15px" Visible="false"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <tr>
                                                                                                        <td colspan="100%" align="left">
                                                                                                            <div id="div<%# Eval("lawfirm_id") %>" style="display: none; position: relative;">
                                                                                                                <asp:GridView ID="grdVerification" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                                                    <Columns>
                                                                                                                        <asp:TemplateField HeaderText="" SortExpression="">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblToralAmount" runat="server" Text="Amount($)" Font-Bold="true" Font-Size="x-Small"></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="In litigation" SortExpression="">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:HyperLink ID="hINLITIGATION" Font-Underline="false" runat="server" NavigateUrl='<%# "Bill_Sys_DeskInFo.aspx?LfirmId="+DataBinder.Eval(Container, "DataItem.LfirmId")+"&id=1" %>'>
                                                                                                 <%# DataBinder.Eval(Container,"DataItem.INLITIGATION")%> </asp:HyperLink>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Sold" SortExpression="">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:HyperLink ID="hISOLD" Font-Underline="false" runat="server" NavigateUrl='<%# "Bill_Sys_DeskInFo.aspx?LfirmId="+DataBinder.Eval(Container, "DataItem.LfirmId")+"&id=2" %>'>
                                                                                                 <%# DataBinder.Eval(Container,"DataItem.SOLD")%></asp:HyperLink>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Loaned" SortExpression="">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:HyperLink ID="hLOAN" Font-Underline="false" runat="server" NavigateUrl='<%# "Bill_Sys_DeskInFo.aspx?LfirmId="+DataBinder.Eval(Container, "DataItem.LfirmId")    +"&id=3" %>'>
                                                                                                 <%# DataBinder.Eval(Container,"DataItem.LOAN")%></asp:HyperLink>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>

                                                                                                                        <%--<asp:BoundField DataField="Header" ItemStyle-Width="85px"  HeaderText="">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="INLITIGATION" ItemStyle-Width="85px"  HeaderText="In litigation">
                                                                                        
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="SOLD" ItemStyle-Width="85px" HeaderText="Sold">
                                                                                        
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="LOAN" ItemStyle-Width="85px" HeaderText="Loaned">	
                                                                                        
                                                                                        </asp:BoundField>--%>
                                                                                                                    </Columns>
                                                                                                                </asp:GridView>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>

                                                                                    </xgrid:XGridViewControl>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                    <%-- <asp:Label    id="Label1" runat="server" Text="Amount($)" Font-Bold="true" Font-Size="Smaller"  ></asp:Labe>--%>
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
                                        <div style="width: 100%;">
                                            <table style="height: auto; width: 100%; border: 1px solid #B5DF82;" class="txt2"
                                                align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px">
                                                        <b class="txt3">Case list</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 1017px;">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <table style="vertical-align: middle; width: 100%;">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="3"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="vertical-align: middle; width: 15%" align="left">Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                                CssClass="search-input">
                                                                            </gridsearch:XGridSearchTextBox>
                                                                            </td>
                                                                            <td style="width: 30%">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <div id="divSum" style="font-size: 130%;font-weight: bold"></div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div id="divpaid" style="font-size: 130%;font-weight: bold"></div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div id="divLiti" style="font-size: 130%;font-weight: bold"></div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                                                                    <ProgressTemplate>
                                                                                        <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                            runat="Server">
                                                                                            <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                                            Loading...
                                                                                        </div>
                                                                                    </ProgressTemplate>
                                                                                </asp:UpdateProgress>




                                                                            </td>

                                                                             <td class="lbl">
                                                                                <asp:Label ID="lblbillstatus" runat="server" Font-Bold="True" Font-Size="Small" Text="Bill"
                                                                                    Status=" "></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                <extddl:ExtendedDropDownList ID="drdRevertStatus" runat="server" Width="125px" Selected_Text="---Select---"
                                                                                    Procedure_Name="SP_MST_BILL_STATUS" Flag_Key_Value="GET_LIST_FOR_REVERT_BILL_STATUS"
                                                                                    Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                            </td>
                                                                             

                                                                            <td>
                                                                                <asp:Button ID="Button1" OnClick="btnRevert_Click" runat="server" Text="Revert" style="height: 26px" />
                                                                            </td>

                                                                            <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">Record Count:<%= this.grdLitigationDesk.RecordCount %>| Page Count:
                                                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server" __designer:wfdid="w1">
                                                                                </gridpagination:XGridPaginationDropDown>
                                                                                <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                    Text="Export TO Excel" __designer:wfdid="w2">
                                    <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <xgrid:XGridViewControl ID="grdLitigationDesk" runat="server" Height="148px" Width="100%"
                                                                    CssClass="mGrid" OnRowCommand="grdLitigationDesk_RowCommand" AutoGenerateColumns="false"
                                                                    MouseOverColor="0, 153, 153" ExcelFileNamePrefix="ExcelLitigation" ShowExcelTableBorder="true"
                                                                    EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                    ExportToExcelColumnNames="Case #,Patient Name,Insurance Company,Total Bill Amount,Total Paid Amount,Total Litigation Amount,Assigned Firm,Transfer Status,Office Name,Bill #,Transfer Date"
                                                                    ExportToExcelFields="SZ_CASE_NO,PATIENT NAME,INSURANCE_COMPANY,BILL_AMOUNT,PAID_AMOUNT,LITIGATION_AMOUNT,SZ_LEGAL_FIRM,TRANSFER_STATUS,SZ_OFFICE,SZ_BILL_NUMBER,TRANSFER_DATE"
                                                                    AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" GridLines="None" XGridKey="LitigationDesk"
                                                                    PageRowCount="50" PagerStyle-CssClass="pgr" DataKeyNames="SZ_CASE_ID,SZ_CASE_NO,SZ_BILL_NUMBER"
                                                                    AllowSorting="true">
                                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                    <Columns>
                                                                        <%--  0--%>
                                                                        <asp:BoundField DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="False">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <%--  1--%>
                                                                        <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case#" SortExpression="convert(Integer,SZ_CASE_NO)">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>

                                                                        <%--  2--%>
                                                                        <asp:TemplateField HeaderText="Bill No" SortExpression="">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PLS" Font-Size="15px"
                                                                                    Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Font-Size="15px" Visible="false"></asp:LinkButton>
                                                                                <%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:BoundField DataField="DOS" HeaderText="DOS" >
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <%--  3--%>
                                                                        <asp:BoundField DataField="SZ_PROCEDURE_GROUP" HeaderText="Specialty" SortExpression="MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>

                                                                        <%--  4--%>
                                                                        <asp:BoundField DataField="PATIENT_NAME" HeaderText="Patient Name" SortExpression="SZ_PATIENT_FIRST_NAME">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <%--  5--%>
                                                                        <asp:BoundField DataField="DOA" HeaderText="DAO" SortExpression="MST_CASE_MASTER.DT_DATE_OF_ACCIDENT">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <%--  6--%>
                                                                        <asp:BoundField DataField="INSURANCE_COMPANY" HeaderText="Insurance Company" SortExpression="MST_INSURANCE_COMPANY.SZ_INSURANCE_NAME">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <%--  7--%>
                                                                        <asp:BoundField DataField="BILL_AMOUNT" DataFormatString="{0:C}" HeaderText="Billed ($)">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <%--  8--%>
                                                                        <asp:BoundField DataField="PAID_AMOUNT" DataFormatString="{0:C}" HeaderText="Paid ($)">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <%--  9--%>
                                                                        <asp:BoundField DataField="LITIGATION_AMOUNT" DataFormatString="{0:C}" HeaderText="Litigated ($)">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <%--  10--%>
                                                                        <asp:BoundField DataField="SZ_LEGAL_FIRM" HeaderText="Attorney">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <%--  11--%>
                                                                        <asp:BoundField DataField="TRANSFER_STATUS" HeaderText="Transferred">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <%--  12--%>
                                                                        <asp:TemplateField HeaderText="Docs">
                                                                            <ItemTemplate>
                                                                                <img alt="" onclick="javascript:OpenDocManager('<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_ID")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_COMPANY_ID")%> ');" src="Images/grid-doc-mng.gif" style="border: none; cursor: pointer;" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <%--  13--%>  <%--Add new column Verification By Kapil 03 April 2012--%>
                                                                        <asp:TemplateField HeaderText="Verification" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkVP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="VerifPLS" Font-Size="15px"
                                                                                    Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkVM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="VerifMNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Font-Size="15px" Visible="false"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--  14--%>  <%--Add new column Denial By Kapil 26 March 2012--%>
                                                                        <asp:TemplateField HeaderText="Denials" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="DenialPLS" Font-Size="15px"
                                                                                    Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkDM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="DenialMNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Font-Size="15px" Visible="false"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--  15--%>
                                                                        <asp:BoundField DataField="SZ_OFFICE" HeaderText="Provider Name" SortExpression="SZ_OFFICE">
                                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:BoundField>

                                                                          <%--  16--%>
                                                                        <asp:BoundField DataField="TRANSFER_DATE" HeaderText="Transfer Date" SortExpression="TRANSFER_DATE">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>

                                                                        <%--  17--%>  <%--change column 15 to 16 By Kapil--%>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);" ToolTip="Select All" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="10px" HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <%--  18--%>   <%--change column 16 to 17 By Kapil--%>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td colspan="100%">
                                                                                        <div id="div<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative; left: 25px;">
                                                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false"
                                                                                                EmptyDataText="No Record Found" Width="80%" CellPadding="4" ForeColor="#333333"
                                                                                                GridLines="None">
                                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Bill No" HeaderText="Bill No." ItemStyle-Width="25px"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="Total Amount" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Billed ($)">
                                                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="Total Paid Amount" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Paid ($)">
                                                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="Total Litigation Amount" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Litigated ($)">
                                                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="Lawfirm Claim" ItemStyle-Width="105px" HeaderText="Lawfirm Claim">
                                                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="SZ_LAW_FIRM_CASE_ID" ItemStyle-Width="105px" HeaderText="Lawfirm Case #">
                                                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="DT_PURCHASED_DATE" ItemStyle-Width="105px" HeaderText="Purchase Date">
                                                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--19--%>     <%--change column 17 to 18 By Kapil--%>  <%--Add new column Verification grid By Kapil 03 April 2012--%>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td colspan="100%" align="left">
                                                                                        <div id="div2<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative;">
                                                                                            <asp:GridView ID="grdVerification_Gird" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="verification_request" ItemStyle-Width="85px" HeaderText="Verification Request">
                                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="verification_date" ItemStyle-Width="85px" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Verification Date">
                                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="answer" ItemStyle-Width="85px" HeaderText="Answer">
                                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="answer_date" ItemStyle-Width="105px" HeaderText="Answer Date" DataFormatString="{0:MM/dd/yyyy}">
                                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                </Columns>
                                                                                            </asp:GridView>

                                                                                        </div>
                                                                                    </td>
                                                                                </tr>


                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--  20--%> <%--change column 18 to 19 By Kapil--%>  <%--Add new column 18 By Kapil 26 March 2012--%>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td colspan="100%" align="right">
                                                                                        <div id="div1<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative;">
                                                                                            <asp:GridView ID="grdDenial" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="500px" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="SZ_DENIAL_REASONS" ItemStyle-Width="105px" HeaderText="Denial Reason">
                                                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="DT_DENIAL_DATE" ItemStyle-Width="50px" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Denial Date">
                                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="SZ_DESCRIPTION" ItemStyle-Width="85px" HeaderText="Description">
                                                                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                                                    </asp:BoundField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


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
                                <tr>
                                    <td>
                                        <table style="height: auto; width: 100%; border: 0px solid #B5DF82; background-color: White;">
                                            <tr>
                                                <td align="right" style="width: 50%; height: 20px">
                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <ContentTemplate>
                                                            Law Firm :
                                                    <extddl:ExtendedDropDownList ID="extddlUserLawFirm" runat="server" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_USER_LIST" Procedure_Name="SP_MST_LEGAL_LOGIN" Width="250px" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td align="left" style="width: 50%; height: 20px">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnAssign" OnClick="btnAssign_Click" runat="server" Text="Assign"></asp:Button>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:TextBox ID="txtCompanyId" runat="server" Visible="false" Width="15px"></asp:TextBox>
                                                    <asp:TextBox ID="Flag" runat="server" Visible="false" Text="1" Width="18px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCase_Status" runat="server" Visible="False" Width="21px"></asp:TextBox>
                                                    <asp:TextBox ID="txtInsuranceCompany" runat="server" Width="31px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtDenialReason" runat="server" Width="31px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtCasetype" runat="server" Visible="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 10px; height: 100%;">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td style="width: 10px"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
