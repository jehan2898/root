<%@ Page Title="" Language="C#" MasterPageFile="TreatingDoctorMasterPage.master" AutoEventWireup="true"
    CodeFile="BillDesk.aspx.cs" Inherits="TreatingDoctor_Bill" %>

<%@ Register Src="UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function Export() {
            expLoadingPanel.Show();
            Callback1.PerformCallback();
        }
        function OnCallbackComplete(s, e) {
            expLoadingPanel.Hide();
            var url = "DownloadFiles.aspx";
            IFrame_DownloadFiles.SetContentUrl(url);
            IFrame_DownloadFiles.Show();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
</asp:scriptmanager>
<script language="javascript" type="text/javascript">
    function OnIndexChnage(s, e) {
        var lastType = null;
        lastType = cIssueDate.GetValue().toString();
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
    <div style="padding-top:10px;">
    <table width="100%;padding-left:0px;height:30px;" border="0">
        <tr>
            <td align="left" valign="middle" colspan="3" style="background-color: #CDCAB9; font-family: Calibri;
                font-size: 20px; font-weight: normal; font-style: italic;">
                Bill(s) List
            </td>
        </tr>
    </table>
    <table id="manage-reg-filters" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 100%; vertical-align: top;">
                <table style="width: 100%;">
                    <tr>
                        <td class="manage-member-lable-td">
                            <label>
                                Bill Number
                            </label>
                        </td>
                        <td class="manage-member-lable-td">
                            <label>
                                Case Number</label>
                        </td>
                        <td class="manage-member-lable-td">
                            <label>
                                Bill Status</label>
                        </td>
                        <td class="manage-member-lable-td">
                            <label>
                                Specialty</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="registration-form-ibox">
                            <dxe:ASPxTextBox CssClass="inputBox" runat="server" ID="Billno" Width="195px" Height="30px">
                            </dxe:ASPxTextBox>
                        </td>
                        <td class="registration-form-ibox">
                            <dxe:ASPxTextBox CssClass="inputBox" runat="server" ID="txtCaseno" Width="195px" Height="30px">
                            </dxe:ASPxTextBox>
                        </td>
                        <td class="form-ibox">
                            <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                ID="ddlBillstatus">
                                <Items>
                                    <dxe:ListEditItem Text="--Select--" Value="2" Selected="True"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Bill" Value="1"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Unbill" Value="0"></dxe:ListEditItem>
                                </Items>
                                <ItemStyle>
                                    <HoverStyle BackColor="#F6F6F6">
                                    </HoverStyle>
                                </ItemStyle>
                            </dxe:ASPxComboBox>
                        </td>
                        <td class="form-ibox">
                            <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                ID="ddlspeciality">
                                <Items>
                                    <dxe:ListEditItem Text="--Select--" Value="2" Selected="True"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Bill" Value="1"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Unbill" Value="0"></dxe:ListEditItem>
                                </Items>
                                <ItemStyle>
                                    <HoverStyle BackColor="#F6F6F6">
                                    </HoverStyle>
                                </ItemStyle>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="manage-member-lable-td">
                            <label>
                                Billing Date</label>
                        </td>
                        <td class="manage-member-lable-td">
                            <label>
                                From
                            </label>
                        </td>
                        <td class="manage-member-lable-td">
                            <label>
                                To</label>
                        </td>
                        <td class="manage-member-lable-td">
                            <label>
                                Case Type</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="registration-form-ibox">
                            <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                ValueType="System.String" ClientInstanceName="cIssueDate" CssClass="inputBox"
                                ID="ddlDateValues">
                                <Items>
                                    <dxe:ListEditItem Text="--Select--" Value="NA" Selected="True"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="All" Value="0"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Today" Value="1"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="This Week" Value="2"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="This Month" Value="3"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="This Quarter" Value="4"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="This Year" Value="5"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Last Week" Value="6"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Last Month" Value="7"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Last Quarter" Value="9"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Last Year" Value="8"></dxe:ListEditItem>
                                </Items>
                                <ItemStyle>
                                    <HoverStyle BackColor="#F6F6F6">
                                    </HoverStyle>
                                </ItemStyle>
                                <ClientSideEvents SelectedIndexChanged="OnIndexChnage" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td class="registration-form-ibox">
                            <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdtfromdate" CssClass="inputBox"
                                ID="dtfromdate">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td class="registration-form-ibox">
                            <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdttodate" CssClass="inputBox"
                                ID="dttodate">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td class="form-ibox">
                            <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                ID="ddlcasetype">
                                <%--<Items>
                                    <dxe:ListEditItem Text="--Select--" Value="2" Selected="True"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Bill" Value="1"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Unbill" Value="0"></dxe:ListEditItem>
                                </Items>
                                <ItemStyle>
                                    <HoverStyle BackColor="#F6F6F6">
                                    </HoverStyle>
                                </ItemStyle>--%>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="manage-member-lable-td">
                            <label>
                                Patient Name</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="registration-form-ibox">
                            <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                ID="txtchartno">
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>--%>
                </table>
                <table id="Table1" style="width: 100%;">
                     <tr>
                        <td align="right" >
                            <dx:ASPxButton runat="server" Text="Search" ID="btnSearch" 
                                onclick="btnSearch_Click">
                            </dx:ASPxButton>
                        </td>
                        <td align="left">
                            <dx:ASPxButton runat="server" Text="Reset" ID="btnReset" 
                                onclick="btnReset_Click">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%;text-align:right;">
                            <dx:ASPxHyperLink text = "[Excel]" runat="server" ID="xExcel">
                                <ClientSideEvents Click="Export" />
                            </dx:ASPxHyperLink>
                            <dx:ASPxCallback ID="ASPxCallback1" 
                                runat="server" ClientInstanceName="Callback1" OnCallback="ASPxCallback1_Callback">
                                <ClientSideEvents CallbackComplete="OnCallbackComplete" />
                            </dx:ASPxCallback>
                            <dx:ASPxLoadingPanel 
                                Text="Exporting..."
                                runat="server" ID="expLoadingPanel" ClientInstanceName="expLoadingPanel">
                            </dx:ASPxLoadingPanel>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%">
                            <div style="height: 400px; width: 100%; overflow: scroll;">
                                <dx:ASPxGridView ID="grdBills" runat="server" KeyFieldName="SZ_CASE_ID" AutoGenerateColumns="false"
                                    Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                    Settings-VerticalScrollableHeight="330">
                                    <Columns>
                                        <%--0--%>
                                        <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case #" HeaderStyle-HorizontalAlign="Center"
                                            Visible="true" HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--1--%>
                                        <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                            Visible="true" HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--2--%>
                                        <dx:GridViewDataColumn FieldName="SZ_BILL_NUMBER" Caption="Bill #" HeaderStyle-HorizontalAlign="Center"
                                            Visible="true" HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--3--%>
                                        <dx:GridViewDataColumn 
                                            FieldName="SZ_CASE_ID" 
                                            Caption="Case ID" HeaderStyle-HorizontalAlign="Center"
                                            Visible = "false"
                                            Settings-AllowSort="False" 
                                            Width="28px" HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--4--%>
                                        <dx:GridViewDataColumn HeaderStyle-HorizontalAlign="Center" FieldName="DT_BILL_DATE" Caption="Bill Date" 
                                        Visible="true" HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--5--%>
                                        <dx:GridViewDataTextColumn
                                            FieldName="FLT_BILL_AMOUNT" 
                                            Caption="Bill Amount" 
                                            HeaderStyle-HorizontalAlign="Center"
                                            Settings-AllowSort="False" 
                                            HeaderStyle-Font-Bold="true">
                                            <PropertiesTextEdit DisplayFormatString="c"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <%--9--%>
                                        <dx:GridViewDataColumn 
                                            FieldName="SZ_BILL_STATUS_NAME" 
                                            Caption="Bill Status" 
                                            HeaderStyle-HorizontalAlign="Center"
                                            Visible="true" 
                                            HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--10--%>
                                        <dx:GridViewDataColumn FieldName="SPECIALITY_ID" 
                                            Caption="Specialty ID" HeaderStyle-HorizontalAlign="Center"
                                            visible="false"
                                            Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--11--%>
                                        <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="Specialty" HeaderStyle-HorizontalAlign="Center"
                                            Visible="true" HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--12--%>
                                        <dx:GridViewDataColumn 
                                            FieldName="MST_CASE_TYPE" 
                                            Caption="Case Type" 
                                            HeaderStyle-HorizontalAlign="Center"
                                            Visible="true" HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--13--%>
                                        <dx:GridViewDataColumn 
                                            FieldName="SZ_INSURANCE_NAME" 
                                            Caption="Insurance" 
                                            Width="15%" 
                                            HeaderStyle-HorizontalAlign="Center"
                                            Settings-AllowSort="False" HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--14--%>
                                        <dx:GridViewDataColumn 
                                            FieldName="ProviderName" 
                                            Caption="Provider" 
                                            HeaderStyle-HorizontalAlign="Center"
                                            Settings-AllowSort="False" 
                                            Width="15%" 
                                            HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                        <%--14--%>
                                        <dx:GridViewDataColumn FieldName="ReferringDoctor" 
                                            Caption=" Treating Doctor" 
                                            HeaderStyle-HorizontalAlign="Center"
                                            Settings-AllowSort="False" 
                                            Width="15%" 
                                            HeaderStyle-Font-Bold="true">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <SettingsPager PageSize="50">
                                    </SettingsPager>
                                    <Settings VerticalScrollableHeight="330"></Settings>
                                    <SettingsCustomizationWindow Height="330px"></SettingsCustomizationWindow>
                                    <Styles>
                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                        <Footer BackColor="#F0F0F0" Font-Bold="True"></Footer>
                                    </Styles>
                                    <Settings ShowFooter="True"/>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="FLT_BILL_AMOUNT" SummaryType="Sum" DisplayFormat="Total Billed: {0:c2}" />
                                        <dx:ASPxSummaryItem FieldName="SZ_BILL_NUMBER" SummaryType="Count" DisplayFormat="Total Bills: {0}" />
                                    </TotalSummary>
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdBillsExport" runat="server" GridViewID="grdBills"></dx:ASPxGridViewExporter>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <dx:ASPxPopupControl 
        ID="IFrame_DownloadFiles" 
        runat="server" CloseAction="CloseButton"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="IFrame_DownloadFiles"
        HeaderText="Data Export"
        HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-ForeColor="White"
        HeaderStyle-BackColor="#000000" 
        AllowDragging="True" 
        EnableAnimation="False"
        EnableViewState="True" Width="600px" ToolTip="Download File(s)" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
        RenderIFrameForPopupElements="Default" Height="200px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>

    </div>
</asp:Content>
