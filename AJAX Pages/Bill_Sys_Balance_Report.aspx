<%@ Page Title="Green Your Bills-Balance Report" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="Bill_Sys_Balance_Report.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Balance_Report" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Data" Assembly="DevExpress.Data.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<%@ Register Assembly="DevExpress.XtraCharts.v16.2.Web, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="dxcharts" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.PivotGrid.v16.2.Core, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraPivotGrid" TagPrefix="temp" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="../validation.js"></script>
    <script type="text/javascript">

        function confirm_update_bill_status() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_drdUpdateStatus").value == 'NA') {
                alert('Select Bill Status');
                return false;
            }


            var f = document.getElementById("<%=grdBalanceBill.ClientID%>");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkSelect') != -1) {
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

function Clear() {
    //alert("call");

    document.getElementById("<%=txtFromDate.ClientID %>").value = '';
    document.getElementById("<%=txtToDate.ClientID %>").value = '';
    document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value = 0;
    document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value = "NA";
    document.getElementById("ctl00_ContentPlaceHolder1_extddlBillStatus").value = "NA";
    document.getElementById("ctl00_ContentPlaceHolder1_ddlAmount").value = "NA";
    document.getElementById("<%=txtAmount.ClientID %>").value = '';
    document.getElementById("<%=txtToAmt.ClientID %>").value = '';
}

function ClickOnButton() {
    document.getElementById("<%=hdnClick.ClientID %>").value = '1';
    return true;
}

function confirm_packet_bill_status() {

    var f = document.getElementById("<%=grdBalanceBill.ClientID%>");
    var bfFlag = false;
    for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
        if (f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') != -1) {
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


function SelectAll(ival) {
    var f = document.getElementById("<%= grdBalanceBill.ClientID %>");
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


    </script>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color: White;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
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
                                                <td style="text-align: left; width: 100%; vertical-align: top;">
                                                    <table style="text-align: left; width: 50%;">
                                                        <tr>
                                                            <td style="text-align: left; width: 100%; vertical-align: top;">
                                                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%; border: 0px solid #B5DF82;">
                                                                    <tr>
                                                                        <td style="width: 100%; height: 0px;" align="center">
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82"
                                                                                onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                <tr>
                                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                                        <b class="txt3">Search Parameters</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">Bill Date
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">From
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">To
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
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="76%">
                                                                                        </asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="70%">
                                                                                        </asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">Case Type
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">Bill Status

                                                                                    </td>
                                                                                    <td></td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="94%" Selected_Text="---Select---"
                                                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <extddl:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="94%" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="GET_SELECTED_STATUS_LIST_WITHOUT_VR_VS_DEN_FBP" Procedure_Name="SP_MST_BILL_STATUS_BILL_SEARCH"
                                                                                            Selected_Text="---Select---" />

                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">Bill Amount

                                                                                    </td>
                                                                                    <td class="td-widget-bc-serach-desc-ch" colspan="2" align="center">
                                                                                        <asp:Label ID="lblfrom" runat="server" Text="Between" Font-Size="12px" Font-Bold="true" Visible="false" Width="250px"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <asp:DropDownList ID="ddlAmount" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="ddlAmount_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="NA">--Select--</asp:ListItem>
                                                                                            <asp:ListItem Value="0">Range</asp:ListItem>
                                                                                            <asp:ListItem Value="1">Greater Than</asp:ListItem>
                                                                                            <asp:ListItem Value="2">Less Than</asp:ListItem>
                                                                                            <asp:ListItem Value="3">Equal To</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblFamt" runat="server" Width="3%" Visible="false" Font-Bold="true" CssClass="lbl" Text="$"></asp:Label>
                                                                                        <asp:TextBox ID="txtAmount" runat="server" Width="75%" Visible="false"></asp:TextBox>
                                                                                    </td>
                                                                                    <td valign="top">
                                                                                        <asp:Label ID="lblTamt" runat="server" Width="3%" Visible="false" Font-Bold="true" CssClass="lbl" Text="$"></asp:Label>
                                                                                        <asp:TextBox ID="txtToAmt" runat="server" Width="70%" Visible="false"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td colspan="3" style="width: 100%" align="center">
                                                                                        <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:UpdateProgress ID="UpdateProgress123" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                                                                    DisplayAfter="10">
                                                                                                    <ProgressTemplate>
                                                                                                        <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 200px; left: 600px"
                                                                                                            runat="Server">
                                                                                                            <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Searching....."
                                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                                                            Searching...
                                                                                                        </div>
                                                                                                    </ProgressTemplate>
                                                                                                </asp:UpdateProgress>--%>
                                                                                                &nbsp;
                                                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                                                        <input style="width: 60px" id="btnClear1" onclick="Clear();" type="button" value="Clear"
                                                                                            runat="server" />
                                                                                        <%--</ContentTemplate>

                                                                                        </asp:UpdatePanel>--%>
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
                            <br />

                            <table cellspacing="10px" width="100%">



                                <tr>
                                    <td style="width: 100%" class="txt2" valign="middle" align="left" bgcolor="#b5df82"
                                        colspan="6" height="28">
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
                                        <%--<asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                                        DisplayAfter="10">
                                                                        <ProgressTemplate>
                                                                            <div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                runat="Server">
                                                                                <asp:Image ID="img4" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                Loading...</div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>--%>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="lbl" style="width: 40%">
                                        <%-- <asp:UpdatePanel ID="UpdatePaneamount" runat="server">
                                                            <ContentTemplate>--%>
                                        <%--  </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                    </td>
                                    <td class="lbl" style="width: 40%">
                                        <%--    <asp:UpdatePanel ID="UpdatePaneamount1" runat="server">
                                                            <ContentTemplate>--%>
                                        <%-- </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                    </td>
                                    <td class="lbl" align="left">
                                        <asp:Label ID="lblbillstatus" runat="server" Font-Bold="True" Font-Size="Small" Text="Bill"
                                            Status=" "></asp:Label>
                                        <extddl:ExtendedDropDownList ID="drdUpdateStatus" runat="server" Width="125px" Selected_Text="---Select---"
                                            Procedure_Name="SP_MST_BILL_STATUS" Flag_Key_Value="GET_STATUS_LIST_NOT_TRF_DNL"
                                            Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                    </td>
                                    <td align="left">
                                        <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>--%>
                                        <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" OnClick="btnUpdateStatus_Click"></asp:Button>
                                        &nbsp;
                                                                    
                                                                    <%-- </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                    </td>
                                    <td style="padding-right: 4px" align="right" colspan="3">
                                       

                                    </td>

                                    <%--    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>--%>
                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; width: 30%" align="left">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" Text="Search:"></asp:Label>
                                        <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                            CssClass="search-input">
                                        </gridsearch:XGridSearchTextBox>
                                    </td>
                                    <td style="vertical-align: middle; width: 30%" align="left"></td>
                                    <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">Record Count:<%= this.grdBalanceBill.RecordCount %>| Page Count:
                                                                       
                                     <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                     </gridpagination:XGridPaginationDropDown>

                                      <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                        Text="Export TO Excel">
                                           <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="height: 10px">
                            </table>
                        </td>
                    </tr>
                    <tr>


                        <td colspan="3">


                            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                            <div style="height: 500px; overflow-y: scroll; overflow-x: scroll">
                                <%-- <dx:ASPxGridView ID="" runat="server" KeyFieldName="SZ_BILL_NUMBER"  AutoGenerateColumns="false" 
                                        Width="100%"  >  
                                       
                                        <Columns>
                                            
                                            <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="CASE ID" HeaderStyle-HorizontalAlign="Center"
                                                Visible="false">
                                            </dx:GridViewDataColumn>
                                           
                                            <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case#" HeaderStyle-HorizontalAlign="Center"
                                                Width="25px" Settings-AllowSort="true">
                                            </dx:GridViewDataColumn>
                                          
                                            <dx:GridViewDataColumn FieldName="SZ_INSURANCE_ID" Caption="INSURANCE ID" HeaderStyle-HorizontalAlign="Center"
                                                Visible="false">
                                            </dx:GridViewDataColumn>
                                            
                                            <dx:GridViewDataColumn FieldName="SZ_PATIENT_ID" Caption="Patient ID" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px" Visible="false">
                                            </dx:GridViewDataColumn>
                                           
                                            <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="180px">
                                            </dx:GridViewDataColumn>
                                            
                                            <dx:GridViewDataColumn FieldName="SZ_BILL_NUMBER"  Caption="BILL NUMBER" HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" Width="73px">
                                            </dx:GridViewDataColumn>
                                          
                                            <dx:GridViewDataColumn FieldName="DT_VISIT_DATE" Caption="VISIT DATE" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px">
                                            </dx:GridViewDataColumn>
                                            
                                            <dx:GridViewDataColumn FieldName="DT_BILL_DATE" Caption="BILL DATE" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px">
                                            </dx:GridViewDataColumn>
                                           
                                            <dx:GridViewDataColumn FieldName="INSURANCE_NAME" Caption="INSURANCE NAME" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px">
                                            </dx:GridViewDataColumn>
                                           
                                            <dx:GridViewDataColumn FieldName="ACCIDENT_DATE" Caption="ACCIDENT DATE" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px">
                                            </dx:GridViewDataColumn>
                                           
                                            <dx:GridViewDataTextColumn FieldName="BILL_AMOUNT" Caption="BILL AMOUNT" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px">
                                                <PropertiesTextEdit DisplayFormatString="{0:c2}" />
                                            </dx:GridViewDataTextColumn>
                                           
                                            <dx:GridViewDataTextColumn FieldName="PAID_AMOUNT" Caption="PAID AMOUNT" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px">
                                                <PropertiesTextEdit DisplayFormatString="{0:c2}" />
                                            </dx:GridViewDataTextColumn>
                                          
                                            <dx:GridViewDataTextColumn FieldName="FLT_WRITE_OFF" Caption="WRITE OFF AMOUNT" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px">
                                                <PropertiesTextEdit DisplayFormatString="{0:c2}" />
                                            </dx:GridViewDataTextColumn>
                                            
                                            <dx:GridViewDataTextColumn FieldName="OUTSTANDING" Caption="OUTSTANDING" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Settings-AllowAutoFilter="False" Width="73px">
                                                <PropertiesTextEdit DisplayFormatString="{0:c2}" />
                                            </dx:GridViewDataTextColumn>
                                            
                                            <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_NAME" Caption="Case Type" HeaderStyle-HorizontalAlign="Center"
                                                Width="30px" Settings-AllowSort="False">
                                            </dx:GridViewDataColumn>
                                           
                                            <dx:GridViewDataColumn FieldName="SZ_BILL_STATUS_NAME" Caption="Bill Status" HeaderStyle-HorizontalAlign="Center" Width="30px">
                                            </dx:GridViewDataColumn>
                                     
                                           <dx:GridViewDataColumn Caption="">
                                               <HeaderTemplate>
                                                     <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                                                        ToolTip="Select All" />
                                               </HeaderTemplate>
                                               
                                               <DataItemTemplate>
                                              <asp:CheckBox ID="chkSelect" runat="server" />
                                               </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>--%>

                                <xgrid:XGridViewControl ID="grdBalanceBill" runat="server" Height="148px" Width="100%"
                                    CssClass="mGrid" AllowSorting="true"
                                    PagerStyle-CssClass="pgr" PageRowCount="50" DataKeyNames="SZ_BILL_NUMBER"
                                    XGridKey="Bill_Sys_Balance_Report" GridLines="None" AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                    HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                    ShowExcelTableBorder="true" ExcelFileNamePrefix="BalanceReport" MouseOverColor="0, 153, 153"
                                    AutoGenerateColumns="false" ExportToExcelColumnNames="Case#,Patient Name,BILL NUMBER,VISIT DATE,BILL DATE,INSURANCE NAME,ACCIDENT DATE,BILL AMOUNT,PAID AMOUNT,WRITE OFF,OUTSTANDING,Case Type,Bill Status"
                                    ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_BILL_NUMBER,DT_VISIT_DATE,DT_BILL_DATE,INSURANCE_NAME,ACCIDENT_DATE,BILL_AMOUNT,PAID_AMOUNT,FLT_WRITE_OFF,OUTSTANDING,SZ_CASE_TYPE_NAME,SZ_BILL_STATUS_NAME">
                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                    <Columns>
                                        <%--  0--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            Visible="false" HeaderText="Case ID" DataField="SZ_CASE_ID" />
                                        <%-- 1--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            Visible="true" HeaderText="Case#" DataField="SZ_CASE_NO" SortExpression="SZ_CASE_NO" />
                                        <%--  2--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                            HeaderText="INSURANCE ID" DataField="SZ_INSURANCE_ID" Visible="false"  />
                                        <%-- 3--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            SortExpression="SZ_PATIENT_ID" HeaderText="Patient Id" DataField="SZ_PATIENT_ID" Visible="false" />
                                        <%--  4--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            HeaderText="Patient Name" DataField="SZ_PATIENT_NAME" SortExpression="SZ_PATIENT_NAME" />
                                        <%--  5--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                            HeaderText="BILL NUMBER" DataField="SZ_BILL_NUMBER" SortExpression="SZ_BILL_NUMBER" />
                                        <%--  6--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            DataFormatString="{0:MM/dd/yyyy}" HeaderText="VISIT DATE"
                                            DataField="DT_VISIT_DATE" />
                                        <%--  7 --%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            HeaderText="BILL DATE"
                                            DataField="DT_BILL_DATE" Visible="true" />
                                        <%--  8 --%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            HeaderText="INSURANCE NAME" SortExpression="INSURANCE_NAME"
                                            DataField="INSURANCE_NAME" />
                                        <%--  9--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            HeaderText="ACCIDENT DATE" DataField="ACCIDENT_DATE" SortExpression="ACCIDENT_DATE" />
                                        <%--  10--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            HeaderText="BILL AMOUNT" DataFormatString="{0:c2}" SortExpression="BILL_AMOUNT"
                                            DataField="BILL_AMOUNT" />
                                        <%--  11--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            HeaderText="PAID AMOUNT" DataFormatString="{0:c2}" SortExpression="PAID_AMOUNT"
                                            DataField="PAID_AMOUNT" />
                                        <%--  12--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            HeaderText="WRITE OFF" DataFormatString="{0:c2}" SortExpression="FLT_WRITE_OFF"
                                            DataField="FLT_WRITE_OFF" />
                                        <%--  13--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            HeaderText="OUTSTANDING" DataFormatString="{0:c2}" SortExpression="OUTSTANDING"
                                            DataField="OUTSTANDING" />
                                        <%--  14--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            HeaderText="Case Type" DataFormatString="{0:c2}" SortExpression="SZ_CASE_TYPE_NAME"
                                            DataField="SZ_CASE_TYPE_NAME" />
                                        <%--  15--%>
                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                            HeaderText="Bill Status" DataField="SZ_BILL_STATUS_NAME" SortExpression="SZ_BILL_STATUS_NAME" />
                                        <%--  16--%>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                    ToolTip="Select All" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </xgrid:XGridViewControl>
                                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grdBalanceBill">
                                </dx:ASPxGridViewExporter>
                                <%--</ContentTemplate>--%>
                                <%-- <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                </Triggers>--%>
                                <%--</asp:UpdatePanel>--%>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false">
                            </asp:TextBox>
                            <asp:HiddenField ID="hdnClick" runat="server"></asp:HiddenField>
                            <asp:TextBox ID="txtBillStatusID" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtNotesType" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtBillAmount" runat="server" Visible="false"></asp:TextBox>
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
</asp:Content>

