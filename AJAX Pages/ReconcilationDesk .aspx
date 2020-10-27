<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReconcilationDesk .aspx.cs" Inherits="AJAX_Pages_ReconcilationDesk_" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function Clear(s, e) {
            cddlReconciliationdate.SetValue('0');
            cddlReconciliationdate.SetText('All');
            cntdtfromdate.SetDate(null);
            cntdttodate.SetDate(null);
            clstLawfirm.UnselectAll();
            clstCarrier.UnselectAll();
            clstSpeciality.UnselectAll();
            clstProviderName.UnselectAll();
            clstCaseType.UnselectAll();
            e.processOnServer = false;
        }
        //Case Type
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
            for (var i = 1; i < clstCaseType.GetItemCount(); i++)
                if (!clstCaseType.GetItem(i).selected)
                    return false;
            return true;
        }
        function UpdateTextCaseType() {
            var selectedItems = clstCaseType.GetSelectedItems();
            clstCaseType.SetText(GetSelectedItemsText(selectedItems));
        }


        //Lawfirm
        function OnLawfirmSelectionChanged(listBoxl, argsl) {
            if (argsl.index == 0)
                argsl.isSelected ? listBoxl.SelectAll() : listBoxl.UnselectAll();

            UpdateSelectAllItemLawfirm();
            UpdateTextLawfirm();
        }

        function UpdateSelectAllItemLawfirm(listBox) {
            IsAllSelectedLawfirm() ? clstLawfirm.SelectIndices([0]) : clstLawfirm.UnselectIndices([0]);
        }
        function IsAllSelectedLawfirm() {
            for (var i = 1; i < clstLawfirm.GetItemCount(); i++)
                if (!clstLawfirm.GetItem(i).selected)
                    return false;
            return true;
        }
        function UpdateTextLawfirm() {
            var selectedItems = clstLawfirm.GetSelectedItems();
            clstLawfirm.SetText(GetSelectedItemsText(selectedItems));
        }

        //Carrier
        function OnCarrierSelectionChanged(listBox1, args1) {
            if (args1.index == 0)
                args1.isSelected ? listBox1.SelectAll() : listBox1.UnselectAll();

            UpdateSelectAllItemCarrier();
            UpdateCarrier();
        }

        function UpdateSelectAllItemCarrier(listBox1) {
            IsAllSelectedCarrier() ? clstCarrier.SelectIndices([0]) : clstCarrier.UnselectIndices([0]);
        }
        function IsAllSelectedCarrier() {
            for (var i = 1; i < clstCarrier.GetItemCount(); i++)
                if (!clstCarrier.GetItem(i).selected)
                    return false;
            return true;
        }
        function UpdateCarrier() {
            var selectedItems = clstCarrier.GetSelectedItems();
            clstCarrier.SetText(GetSelectedItemsText(selectedItems));
        }
        //Speciality
        function OnSpecialitySelectionChanged(listBox2, args2) {
            if (args2.index == 0)
                args2.isSelected ? listBox2.SelectAll() : listBox2.UnselectAll();

            UpdateSelectAllItemSpeciality();
            UpdateTextSpeciality();
        }

        function UpdateSelectAllItemSpeciality(listBox2) {
            IsAllSelectedSpeciality() ? clstSpeciality.SelectIndices([0]) : clstSpeciality.UnselectIndices([0]);
        }
        function IsAllSelectedSpeciality() {
            for (var i = 1; i < clstSpeciality.GetItemCount(); i++)
                if (!clstSpeciality.GetItem(i).selected)
                    return false;
            return true;
        }
        function UpdateTextSpeciality() {
            var selectedItems = clstSpeciality.GetSelectedItems();
            clstSpeciality.SetText(GetSelectedItemsText(selectedItems));
        }
        //Provide Name-lstProviderName
        function OnProviderNameSelectionChanged(listBox3, args3) {
            if (args3.index == 0)
                args3.isSelected ? listBox3.SelectAll() : listBox3.UnselectAll();

            UpdateSelectAllItemProviderName();
            UpdateTextProviderName();
        }

        function UpdateSelectAllItemProviderName(listBox2) {
            IsAllSelected() ? clstProviderName.SelectIndices([0]) : clstProviderName.UnselectIndices([0]);
        }
        function IsAllSelected() {
            for (var i = 1; i < clstProviderName.GetItemCount(); i++)
                if (!clstProviderName.GetItem(i).selected)
                    return false;
            return true;
        }
        function UpdateTextProviderName() {
            var selectedItems = clstProviderName.GetSelectedItems();
            clstProviderName.SetText(GetSelectedItemsText(selectedItems));
        }
        //Date Change    
        function OnIndexChange(s, e) {

            // var dropDown = window[listBox.cplsb_datevalues];
            var lastType = null;
            lastType = cddlReconciliationdate.GetValue().toString();
            getWeek();
            if (lastType != "NA") {

                if (lastType == "1") {
                    var tDate = getDate('today');

                    cntdtfromdate.SetText(tDate);
                    cntdttodate.SetText(tDate);

                } else if (lastType == "0") {
                    cntdtfromdate.SetDate(null);
                    cntdttodate.SetDate(null);
                } else if (lastType == "2") {

                    cntdtfromdate.SetText(getWeek('startweek'));
                    cntdttodate.SetText(getWeek('endweek'));

                } else if (lastType == "3") {

                    cntdtfromdate.SetText(getDate('monthstart'));
                    cntdttodate.SetText(getDate('monthend'));

                } else if (lastType == "4") {

                    cntdtfromdate.SetText(getDate('quarterstart'));
                    cntdttodate.SetText(getDate('quarterend'));

                } else if (lastType == "5") {

                    cntdtfromdate.SetText(getDate('yearstart'));
                    cntdttodate.SetText(getDate('yearend'));

                } else if (lastType == "6") {

                    cntdtfromdate.SetText(getLastWeek('startweek'));
                    cntdttodate.SetText(getLastWeek('endweek'));

                } else if (lastType == "7") {

                    cntdtfromdate.SetText(lastmonth('startmonth'));
                    cntdttodate.SetText(lastmonth('endmonth'));

                } else if (lastType == "8") {

                    cntdtfromdate.SetText(lastyear('startyear'));
                    cntdttodate.SetText(lastyear('endyear'));

                } else if (lastType == "9") {

                    cntdtfromdate.SetText(quarteryear('startquaeter'));
                    cntdttodate.SetText(quarteryear('endquaeter'));

                }
            } else {
                cntdtfromdate.SetDate(null);
                cntdttodate.SetDate(null);
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
    <asp:ScriptManager ID="ScriptManagers" runat="server">
    </asp:ScriptManager>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
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
                                                                    height: 50%;">
                                                                    <tr>
                                                                        <td style="width: 40%; height: 0px;" align="center">
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                                <tr>
                                                                                    <td height="28" align="left" valign="middle" bgcolor="#aaaaaa" class="txt2" colspan="3">
                                                                                        <b class="txt3">Search Parameters</b>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Carrier
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Provider Name
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Law Firm
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Specialty
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <dx:ASPxDropDownEdit ID="ddleCarrier" runat="server" ClientInstanceName="cddleCarrier">
                                                                                            <DropDownWindowStyle>
                                                                                            </DropDownWindowStyle>
                                                                                            <DropDownWindowTemplate>
                                                                                                <dx:ASPxListBox ID="lstCarrier" runat="server" ClientInstanceName="clstCarrier" SelectionMode="CheckColumn">
                                                                                                    <Border BorderStyle="None" />
                                                                                                    <Border BorderStyle="Solid" BorderWidth="1px" />
                                                                                                    <Items>
                                                                                                    </Items>
                                                                                                    <ClientSideEvents SelectedIndexChanged="OnCarrierSelectionChanged" />
                                                                                                </dx:ASPxListBox>
                                                                                            </DropDownWindowTemplate>
                                                                                        </dx:ASPxDropDownEdit>
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        <%-- <dxe:ASPxComboBox ID="ddlProviderName" ClientInstanceName="cddlProviderName" runat="server">
                                                                                                <Items>
                                                                                                
                                                                                                </Items>
                                                                                            </dxe:ASPxComboBox>--%>
                                                                                        <dx:ASPxDropDownEdit ID="ddleProviderName" runat="server" ClientInstanceName="cddleProviderName">
                                                                                            <DropDownWindowStyle>
                                                                                            </DropDownWindowStyle>
                                                                                            <DropDownWindowTemplate>
                                                                                                <dx:ASPxListBox ID="lstProviderName" runat="server" ClientInstanceName="clstProviderName"
                                                                                                    SelectionMode="CheckColumn">
                                                                                                    <Border BorderStyle="None" />
                                                                                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" />
                                                                                                    <Items>
                                                                                                    </Items>
                                                                                                    <ClientSideEvents SelectedIndexChanged="OnProviderNameSelectionChanged" />
                                                                                                </dx:ASPxListBox>
                                                                                            </DropDownWindowTemplate>
                                                                                        </dx:ASPxDropDownEdit>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dx:ASPxDropDownEdit ID="ddleLawfirm" runat="server" ClientInstanceName="cddleLawfirm">
                                                                                            <DropDownWindowStyle>
                                                                                            </DropDownWindowStyle>
                                                                                            <DropDownWindowTemplate>
                                                                                                <dx:ASPxListBox ID="lstLawfirm" runat="server" ClientInstanceName="clstLawfirm" SelectionMode="CheckColumn">
                                                                                                    <Border BorderStyle="None" />
                                                                                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" />
                                                                                                    <Items>
                                                                                                    </Items>
                                                                                                    <ClientSideEvents SelectedIndexChanged="OnLawfirmSelectionChanged" />
                                                                                                </dx:ASPxListBox>
                                                                                            </DropDownWindowTemplate>
                                                                                        </dx:ASPxDropDownEdit>
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        <dx:ASPxDropDownEdit ID="ddleSpeciality" runat="server" ClientInstanceName="cddleSpeciality">
                                                                                            <DropDownWindowStyle>
                                                                                            </DropDownWindowStyle>
                                                                                            <DropDownWindowTemplate>
                                                                                                <dx:ASPxListBox ID="lstSpeciality" runat="server" Width="100%" ClientInstanceName="clstSpeciality"
                                                                                                    SelectionMode="CheckColumn">
                                                                                                    <Border BorderStyle="None" />
                                                                                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" />
                                                                                                    <Items>
                                                                                                    </Items>
                                                                                                    <ClientSideEvents SelectedIndexChanged="OnSpecialitySelectionChanged" />
                                                                                                </dx:ASPxListBox>
                                                                                            </DropDownWindowTemplate>
                                                                                        </dx:ASPxDropDownEdit>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Reconciliation Date
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        From
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        To
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Case Type
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxComboBox runat="server" ID="ddlReconciliationdate" EnableSynchronization="False"
                                                                                            SelectedIndex="0" ValueType="System.String" ClientInstanceName="cddlReconciliationdate">
                                                                                            <Items>
                                                                                                <dxe:ListEditItem Text="All" Value="0" Selected="true" />
                                                                                                <dxe:ListEditItem Text="Today" Value="1" />
                                                                                                <dxe:ListEditItem Text="This Week" Value="2" />
                                                                                                <dxe:ListEditItem Text="This Month" Value="3" />
                                                                                                <dxe:ListEditItem Text="This Quarter" Value="4" />
                                                                                                <dxe:ListEditItem Text="This Year" Value="5" />
                                                                                                <dxe:ListEditItem Text="Last Week" Value="6" />
                                                                                                <dxe:ListEditItem Text="Last Month" Value="7" />
                                                                                                <dxe:ListEditItem Text="Last Quarter" Value="9" />
                                                                                                <dxe:ListEditItem Text="Last Year" Value="8" />
                                                                                            </Items>
                                                                                            <ItemStyle>
                                                                                                <HoverStyle BackColor="#F6F6F6">
                                                                                                </HoverStyle>
                                                                                            </ItemStyle>
                                                                                            <ClientSideEvents SelectedIndexChanged="OnIndexChange" />
                                                                                        </dxe:ASPxComboBox>
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdtfromdate" ID="dtfromdate"
                                                                                            EditFormat="Custom" EditFormatString="MM/dd/yyyy" EnableTheming="False">
                                                                                        </dxe:ASPxDateEdit>
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdttodate" ID="dttodate" EditFormat="Custom"
                                                                                            EditFormatString="MM/dd/yyyy" EnableTheming="False">
                                                                                        </dxe:ASPxDateEdit>
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        <dx:ASPxDropDownEdit ID="ddleCaseType" runat="server" ClientInstanceName="cddleCaseType">
                                                                                            <DropDownWindowStyle>
                                                                                            </DropDownWindowStyle>
                                                                                            <DropDownWindowTemplate>
                                                                                                <dx:ASPxListBox ID="lstCaseType" runat="server" Width="100%" ClientInstanceName="clstCaseType"
                                                                                                    SelectionMode="CheckColumn">
                                                                                                    <Border BorderStyle="None" />
                                                                                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" />
                                                                                                    <Items>
                                                                                                    </Items>
                                                                                                    <ClientSideEvents SelectedIndexChanged="OnCaseTypeSelectionChanged" />
                                                                                                </dx:ASPxListBox>
                                                                                            </DropDownWindowTemplate>
                                                                                        </dx:ASPxDropDownEdit>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" colspan="4px">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td align="center" colspan="1.5px">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <dxe:ASPxButton ID="btnSearch" runat="server" Text="Search" Width="80px" Font-Size="Medium"
                                                                                                                OnClick="btnSearch_Click">
                                                                                                            </dxe:ASPxButton>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <dxe:ASPxButton ID="btnclear" runat="server" Width="80px" Text="Clear" Font-Size="Medium"
                                                                                                                ClientInstanceName="btnclear">
                                                                                                                <ClientSideEvents Click="Clear" />
                                                                                                            </dxe:ASPxButton>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
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
                                                            <%-- <td>
                                                                    <table style="width: 80%; border: 1px solid #B5DF82;" class="txt2" align="right"
                                                                           cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 50%">
                                                                                <b class="txt3">Account Summary</b>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 45%; text-align: center; vertical-align: top; padding: 2px;">
                                                                                <asp:UpdatePanel ID="UpdatePanelAcc" runat="server">
                                                                                    <contenttemplate>
                                                                                        <dx:ASPxGridView ID="grdAccsummary" runat="server" AutoGenerateColumns="false" SettingsPager-PageSize="20">
                                                                                            <Columns>
                                                                                                <dx:GridViewDataColumn Caption="Title" FieldName="" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle BackColor="#B5DF82" />
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="Amount($)" FieldName="" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                                    <HeaderStyle BackColor="#B5DF82" />
                                                                                                    <DataItemTemplate>
                                                                                                    </DataItemTemplate>
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="" FieldName="">
                                                                                                </dx:GridViewDataColumn>
                                                                                            </Columns>
                                                                                        </dx:ASPxGridView>
                                                                                    </contenttemplate>
                                                                                </asp:UpdatePanel>
                                                                            </td> 
                                                                        </tr>
                                                                    </table>
                                                                </td>--%>
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
                                            <table style="height: auto; width: 100%; border: 1px solid #aaaaaa;" class="txt2"
                                                align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td height="28" align="left" valign="middle" bgcolor="#aaaaaa" class="txt2" style="width: 413px">
                                                        <b class="txt3">List</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 1017px;">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <table style="vertical-align: middle; width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                                                                <ProgressTemplate>
                                                                                    <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                        runat="Server">
                                                                                        <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                            Height="25px" Width="24px"></asp:Image>
                                                                                        Loading...</div>
                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <%--<table class="OptionsTable BottomMargin">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Group footer mode:" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <dx:ASPxComboBox runat="server" ID="ddlGroupFooter" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupFooter_SelectedIndexChanged" />
                                                                                    </td>
                                                                                </tr>
                                                                        </table>--%>
                                                                <div style="height: 400px; width: 100%; overflow: scroll;">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <%-- <td align="right">
                                                                            <asp:LinkButton Text="Export To Excel" ID="btnETE" runat="server" OnClick="btnETE_Click"></asp:LinkButton>
                                                                            </td>--%>
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
                                                                            <td style="width: 100%">
                                                                                <dx:ASPxGridView ID="grdReconcilationdesk" runat="server" ClientInstanceName="grdReconcilationdesk"
                                                                                    Border-BorderColor="#aaaaaa" SettingsPager-PageSize="20" Width="100%" AutoGenerateColumns="False"
                                                                                    Settings-ShowHorizontalScrollBar="true" Settings-ShowGroupedColumns="true" Settings-VerticalScrollableHeight="330"
                                                                                    KeyFieldName="SZ_ASSIGNED_LAWFIRM_ID" Settings-ShowGroupPanel="true" OnCustomUnboundColumnData="grid_CustomUnboundColumnData"
                                                                                    SettingsBehavior-AllowGroup="true" Settings-ShowFooter="true" SettingsBehavior-AutoExpandAllGroups="true">
                                                                                    <Columns>
                                                                                        <dx:GridViewDataColumn Caption="Law Firm Name" GroupIndex="0" VisibleIndex="1" FieldName="Lawfirm Name"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="Case#" VisibleIndex="2" FieldName="CASE_NO" Settings-AllowSort="True"
                                                                                            HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="Patient Name" VisibleIndex="3" FieldName="SZ_PATIENT_NAME"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="Carrier" VisibleIndex="4" FieldName="SZ_INSURANCE_NAME"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="Bill Number" VisibleIndex="5" FieldName="SZ_BILL_NUMBER"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="Bill Date" VisibleIndex="6" FieldName="BILL_DATE"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="" Width="150px" FieldName="SZ_INSURANCE_ID" Visible="false">
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="" Width="25px" FieldName="SZ_INSURANCE_NAME_ID" Visible="false">
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="" FieldName="SZ_SPECIALITY_ID" Visible="false">
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="Specialty" VisibleIndex="7" FieldName="Speciality"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Doctor" VisibleIndex="8" FieldName="SZ_DOCTOR_NAME"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <PropertiesTextEdit DisplayFormatString="c" />
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Provider" VisibleIndex="9" FieldName="SZ_OFFICE"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <PropertiesTextEdit DisplayFormatString="c" />
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataColumn Caption="Bill Submitted" VisibleIndex="10" FieldName="DT_DATE_BILL_SUBMITTED"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Settled Principal" VisibleIndex="11" FieldName="SZ_SETTLED_PRINCIPLE"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <PropertiesTextEdit DisplayFormatString="c" />
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Settled Interest" VisibleIndex="12" FieldName="SZ_SETTLED_INT"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <PropertiesTextEdit DisplayFormatString="c">
                                                                                            </PropertiesTextEdit>
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    
                                                                                        <dx:GridViewDataColumn Caption=" Date Settled" VisibleIndex="13" FieldName="DT_DATE_SETTLED"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Collected Principal" VisibleIndex="14" Width="150px"
                                                                                            FieldName="SZ_COLLECTED_PR" Settings-AllowSort="True" HeaderStyle-Font-Bold="true"
                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                            <PropertiesTextEdit DisplayFormatString="c">
                                                                                            </PropertiesTextEdit>
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                      
                                                                                        <dx:GridViewDataTextColumn Caption="Collected Interest" VisibleIndex="15" Width="150px"
                                                                                            FieldName="SZ_COLLECTED_INT" Settings-AllowSort="True" HeaderStyle-Font-Bold="true"
                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                            <PropertiesTextEdit DisplayFormatString="c">
                                                                                            </PropertiesTextEdit>
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                       
                                                                                        <dx:GridViewDataColumn Caption="Index #" VisibleIndex="16" FieldName="SZ_INDEX_NO"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="Index Purchased" VisibleIndex="17" FieldName="DT_PURCHASED_DATE"
                                                                                            Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                                            <Settings AllowSort="True" />
                                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="" FieldName="MST_DOCTOR.SZ_OFFICE_ID" Visible="false">
                                                                                        </dx:GridViewDataColumn>
                                                                                        <dx:GridViewDataColumn Caption="" FieldName="SZ_OFFICE" Visible="false">
                                                                                        </dx:GridViewDataColumn>
                                                                                    </Columns>
                                                                                    <SettingsBehavior AutoExpandAllGroups="True" />
                                                                                    <SettingsPager PageSize="20">
                                                                                    </SettingsPager>
                                                                                    <Settings ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded"
                                                                                        ShowGroupButtons="false" />
                                                                                    <TotalSummary>
                                                                                        <dx:ASPxSummaryItem FieldName="Insurance Company" SummaryType="Count" />
                                                                                        <dx:ASPxSummaryItem FieldName="FLT_BILL_AMOUNT" SummaryType="Sum" />
                                                                                        <dx:ASPxSummaryItem FieldName="SZ_SETTLED_PRINCIPLE" SummaryType="Sum" />
                                                                                        <dx:ASPxSummaryItem FieldName="SZ_SETTLED_INT" SummaryType="Sum" />
                                                                                        <dx:ASPxSummaryItem FieldName="SZ_COLLECTED_PR" SummaryType="Sum" />
                                                                                        <dx:ASPxSummaryItem FieldName="SZ_COLLECTED_INT" SummaryType="Sum" />
                                                                                        <%--<dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" />--%>
                                                                                    </TotalSummary>
                                                                                    <GroupSummary>
                                                                                        <dx:ASPxSummaryItem FieldName="Insurance Company" ShowInGroupFooterColumn="Insurance Company"
                                                                                            SummaryType="Count" />
                                                                                        <dx:ASPxSummaryItem FieldName="FLT_BILL_AMOUNT" ShowInGroupFooterColumn="FLT_BILL_AMOUNT"
                                                                                            SummaryType="Sum" />
                                                                                        <dx:ASPxSummaryItem FieldName="SZ_SETTLED_PRINCIPLE" ShowInGroupFooterColumn="SZ_SETTLED_PRINCIPLE"
                                                                                            SummaryType="Sum" />
                                                                                        <dx:ASPxSummaryItem FieldName="SZ_SETTLED_INT" ShowInGroupFooterColumn="SZ_SETTLED_INT"
                                                                                            SummaryType="Sum" />
                                                                                        <dx:ASPxSummaryItem FieldName="SZ_COLLECTED_PR" ShowInGroupFooterColumn="SZ_COLLECTED_PR"
                                                                                            SummaryType="Sum" />
                                                                                        <dx:ASPxSummaryItem FieldName="SZ_COLLECTED_INT" ShowInGroupFooterColumn="SZ_COLLECTED_INT"
                                                                                            SummaryType="Sum" />
                                                                                        <%--<dx:ASPxSummaryItem FieldName="Total" ShowInGroupFooterColumn="Total" SummaryType="Sum" />--%>
                                                                                    </GroupSummary>
                                                                                    <Border BorderColor="#AAAAAA"></Border>
                                                                                </dx:ASPxGridView>
                                                                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdReconcilationdesk">
                                                                                </dx:ASPxGridViewExporter>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
