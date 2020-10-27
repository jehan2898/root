<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Calendar.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Calendar" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="../validation.js"></script>
    <script type="text/javascript" src="../Registration/validation.js"></script>
    <script type="text/javascript">

        // Function used to close AJAX modal popup using javascript.

        function CloseModalPopup(a) {

            var button = document.getElementById('<%=btnCls.ClientID%>');

            document.getElementById("<%=txtGetDay.ClientID %>").value = a;


            button.click();



        }


        function ShowPreviewMailPopup(szcaseid, eventid, docname, Name, statusId, stime, etime, szDoctorid, szhavelogin, szgroupcode) {
            var url = "Bill_Sys_ViewVisit.aspx?CaseID=" + szcaseid + "&eventid=" + eventid + "&docname=" + docname + "&Name=" + Name + "&statusId=" + statusId + "&stime=" + stime + "&etime=" + etime + "&szdoctorid=" + szDoctorid + "&szhavelogin=" + szhavelogin + "&szgroupcode=" + szgroupcode;
            ShowVisit.SetContentUrl(url);
            ShowVisit.Show();
        }
        function ShowAddVisitPopup() {
            var url = "Bill_Sys_Add_Visit.aspx";
            AddVisit.SetContentUrl(url);
            AddVisit.Show();
        }
        function CloseShowVisit() {
            ShowVisit.Hide();
            var button = document.getElementById('<%=btnCls.ClientID%>');
            button.click();
        }
        function CloseAddVisit() {
            AddVisit.Hide();
            var button = document.getElementById('<%=btnadd.ClientID%>');
            button.click();
        }



        function SelectAllTrans(ival) {
            var f = document.getElementById('<%=grdTransportVisits.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }

                }
            }
        }


        function SelectAll(ival) {
            var f = document.getElementById('<%=grdVisits.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }

                }
            }
        }


        function DeleteVisitTrans() {

            var f = document.getElementById('<%=grdTransportVisits.ClientID%>');
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {

                        if (confirm("Are you sure to continue for  delete?")) {
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



        function DeleteVisit() {

            var f = document.getElementById('<%=grdVisits.ClientID%>');
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {

                        if (confirm("Are you sure to continue for  delete?")) {
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

        function SetReValue() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ddlStatus').selectedIndex == 1) {
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


        function SaveVisit() {

            var f = document.getElementById('<%=grdVisits.ClientID%>');
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


        function ChangeTime() {

            var f = document.getElementById('<%=grdVisits.ClientID%>');
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        if (document.getElementById('ctl00_ContentPlaceHolder1_carTabPage_ddlchangeReSchHours').selectedIndex == 0) {
                            alert('Please Select Change Time...!!');
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

        function ShowReportPopup() {
            var visitdate = document.getElementById("<%=txtGetDay.ClientID %>").value;
            var url = "Bill_Sys_ScheduleReportPopup.aspx?visitdate=" + visitdate;
            ReportPopUp.SetContentUrl(url);
            ReportPopUp.Show();
        }
    </script>
    <script language="javascript" type="text/jscript">
        // Call the page method and run the success function  
        function GetTime(Time) {
            var button = document.getElementById('<%=btnTime.ClientID%>');
            document.getElementById("<%=txtGetTime.ClientID %>").value = Time;
            button.click();
        }

        function GetDayVisits() {
            var button = document.getElementById('<%=btnShowAllVisits.ClientID%>');
            button.click();
        }

    </script>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td colspan="2">
                                    <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top" width="10%">
                                    <table border="0" class="ContentTable" style="width: 100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div style="text-align: left">
                                                        <table cellspacing="2px">
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch1">Select Date</td>
                                                                <td>
                                                                    <dx:ASPxDateEdit ID="dtEdit" AutoPostBack="true" runat="server" OnDateChanged="dtEdit_DateChanged">
                                                                    </dx:ASPxDateEdit>
                                                                </td>
                                                            </tr>

                                                        </table>

                                                        <%--<asp:DropDownList ID="ddlMonthList" runat="server" Width="45px">
                                                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="Sept"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlYearList" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:Button ID="btnLoadCalendar" OnClick="btnLoadCalendar_Click" runat="server" Width="23px"
                                                            CssClass="Buttons" Text="Go"></asp:Button>--%>
                                                    </div>
                                                    <asp:Panel ID="Panel1" runat="server">
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td align="center"><a href="javascript:void(0);" onclick="ShowReportPopup()">Reports</a> </td>
                                            </tr>--%>
                                        </tbody>
                                    </table>
                                </td>
                                <tr>

                                    <td style="vertical-align: top" width="90%">
                                        <table id="Table1" width="100%" runat="server">
                                            <tr>
                                                <td style="vertical-align: top;">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <table border="0" width="100%">
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch1" style="width: 25%;">Patient
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch" style="width: 25%;">Doctor
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch1" style="width: 25%;">Specialty
                                                                        </td>
                                                                        <td style="width: 25%;"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                            <asp:TextBox ID="txtPatientName" runat="server" Width="96%" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                                            <extddl:ExtendedDropDownList ID="extddlPatient" runat="server" Width="0%" Selected_Text="--- Select ---"
                                                                                Procedure_Name="SP_MST_PATIENT" Flag_Key_Value="REF_PATIENT_LIST" Connection_Key="Connection_String"
                                                                                AutoPost_back="True" Visible="false"></extddl:ExtendedDropDownList>
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch3" align="center" valign="top">
                                                                            <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="240px" Connection_Key="Connection_String"
                                                                                Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                                        </td>
                                                                        <td class="td-widget-bc-search-desc-ch3" valign="top" align="center">
                                                                            <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                                                Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                                Selected_Text="---Select---" Width="140px"></cc1:ExtendedDropDownList>
                                                                        </td>
                                                                        <td valign="top">
                                                                            <dx:ASPxButton ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Onclick">
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                                                DisplayAfter="10" DynamicLayout="true">
                                                                                <ProgressTemplate>
                                                                                    <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                        runat="Server">
                                                                                        <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                            Height="25px" Width="24px"></asp:Image>
                                                                                        Loading...
                                                                                    </div>
                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top">
                                                    <dx:ASPxPageControl ID="carTabPage" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
                                                        Width="100%" Height="250" OnActiveTabChanged="carTabPage_ActiveTabChanged" AutoPostBack="true">
                                                        <TabPages>
                                                            <dx:TabPage Text="View" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                                                                Name="case" TabStyle-BackColor="#B1BEE0">
                                                                <ContentCollection>
                                                                    <dx:ContentControl>
                                                                        <asp:Panel ID="pnl_CaseDetails1" runat="server" Width="100%">
                                                                            <table style="width: 100%" class="ContentTable" border="0">
                                                                                <tr>
                                                                                    <td align="center" colspan="2">
                                                                                        <asp:Label ID="lblMessage" Style="color: red;" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblMed" runat="server" Width="100%" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                                                    </td>
                                                                                    <td align="right">
                                                                                        <a href="javascript:void(0);" onclick="GetDayVisits()">Show All</a> <a href="javascript:void(0);"
                                                                                            onclick="ShowAddVisitPopup()">Add Visit</a>
                                                                                        <asp:Button ID="btnDeletVisit" Text="Delete" runat="server" OnClick="btnDeletVisit_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" style="width: 100%" align="right">
                                                                                        <dx:ASPxButton ID="btnXlsExport2" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                                                            OnClick="btnXlsExport2_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" style="width: 100%">
                                                                                        <dx:ASPxGridView ID="grdVisits" runat="server" KeyFieldName="I_EVENT_ID" AutoGenerateColumns="false"
                                                                                            Width="100%" SettingsPager-PageSize="50" SettingsCustomizationWindow-Height="200"
                                                                                            Settings-VerticalScrollableHeight="360">
                                                                                            <Columns>
                                                                                                <%--0--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="CASE ID" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Visible="false" Settings-AllowSort="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--1--%>
                                                                                                <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="EVENT ID" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Visible="false" Settings-AllowSort="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--2--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case#" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowSort="False" Width="60px">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--3--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Name (Transport)" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="123px">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--4--%>
                                                                                                <dx:GridViewDataColumn FieldName="PatientPhone" Caption="Phone" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="100px">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--5--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_INSURANCE_NAME" Caption="Insurance" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="100px">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--6--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="DOCTOR ID" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Visible="false" Settings-AllowSort="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--7--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_DOCTOR_NAME" Caption="Doctor Name" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="100px">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--8--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="SZ_PROCEDURE_GROUP_ID"
                                                                                                    HeaderStyle-HorizontalAlign="Center" Settings-AllowAutoFilter="False" Settings-AllowSort="False"
                                                                                                    Visible="false">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--9--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP" Caption="Specialty" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Width="7%" Settings-AllowAutoFilter="False" Visible="true" Settings-AllowSort="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--10--%>
                                                                                                <dx:GridViewDataColumn FieldName="STATUS" Caption="Status" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Width="9%" Settings-AllowAutoFilter="False" Settings-AllowSort="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--11--%>
                                                                                                <dx:GridViewDataHyperLinkColumn Caption="Edit Visit" Visible="true" Settings-AllowSort="False"
                                                                                                    Width="60px">
                                                                                                    <DataItemTemplate>
                                                                                                        <a href="javascript:void(0);" onclick="ShowPreviewMailPopup('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME")%>','<%# DataBinder.Eval(Container,"DataItem.Name")%>','<%# DataBinder.Eval(Container,"DataItem.STATUS_ID")%>','<%# DataBinder.Eval(Container,"DataItem.START_TIME")%>','<%# DataBinder.Eval(Container,"DataItem.END_TIME")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID")%>','<%# DataBinder.Eval(Container,"DataItem.IS_HAVE_LOGIN")%>','<%# DataBinder.Eval(Container,"DataItem.GROUP_CODE")%>')">Edit Visit</a>
                                                                                                    </DataItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                </dx:GridViewDataHyperLinkColumn>
                                                                                                <%--12--%>
                                                                                                <dx:GridViewDataColumn FieldName="Name" Caption="Name" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Visible="false" Settings-AllowSort="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--13--%>
                                                                                                <dx:GridViewDataColumn FieldName="STATUS_ID" Caption="STATUS_ID" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Visible="false" Settings-AllowSort="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--14--%>
                                                                                                <dx:GridViewDataColumn FieldName="END_TIME" Caption="END_TIME" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Visible="false" Settings-AllowSort="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--15--%>
                                                                                                <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Visit Date" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="73px">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--16--%>
                                                                                                <dx:GridViewDataColumn FieldName="START_TIME" Caption="Time" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="46px">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--17--%>
                                                                                                <dx:GridViewDataColumn FieldName="CLAIM_NUMBER" Caption="Claim No" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Settings-AllowSort="False" Width="82px">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--18--%>
                                                                                                <dx:GridViewDataColumn Caption="Select" Settings-AllowSort="False">
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                                                                            ToolTip="Select All" />
                                                                                                    </HeaderTemplate>
                                                                                                    <DataItemTemplate>
                                                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                    </DataItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--19--%>
                                                                                                <dx:GridViewDataColumn FieldName="IS_HAVE_LOGIN" Caption="IS_HAVE_LOGIN" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Settings-AllowSort="False" Visible="false">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--20--%>
                                                                                                <dx:GridViewDataColumn FieldName="GROUP_CODE" Caption="GROUP_CODE" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowAutoFilter="False" Settings-AllowSort="False" Visible="false">
                                                                                                </dx:GridViewDataColumn>
                                                                                            </Columns>
                                                                                            <Settings ShowVerticalScrollBar="true" ShowFilterRow="false" ShowGroupPanel="false" />
                                                                                            <SettingsBehavior AllowFocusedRow="false" />
                                                                                            <SettingsBehavior AllowSelectByRowClick="true" />
                                                                                            <SettingsPager Position="Bottom" />
                                                                                        </dx:ASPxGridView>
                                                                                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdVisits">
                                                                                        </dx:ASPxGridViewExporter>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table style="width: 100%" class="ContentTable" border="0">
                                                                                <tr>
                                                                                    <td colspan="8">
                                                                                        <asp:Label ID="lblMess" Visible="false" runat="server" Text="Note: You cannot mark a visit as Completed if that patient visit is to be finalized by the doctor.)"
                                                                                            Font-Italic="true" Font-Size="Small"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 15%">Status
                                                                                    </td>
                                                                                    <td style="width: 15%">
                                                                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="114px" onchange="javascript:SetReValue();">
                                                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                            <asp:ListItem Value="1">Re-Schedule</asp:ListItem>
                                                                                            <asp:ListItem Value="2">Visit Completed</asp:ListItem>
                                                                                            <asp:ListItem Value="3">No Show</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td id="tdReDate" style="width: 13%; visibility: hidden;">
                                                                                        <asp:Label ID="lblReScheduleDAte" runat="server" Text="Reschedule Date"></asp:Label>
                                                                                    </td>
                                                                                    <td id="tdReDateValue" style="width: 15%; visibility: hidden;">
                                                                                        <asp:TextBox ID="txtReScheduleDate" runat="server" MaxLength="10" Width="106px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReScheduleDate"
                                                                                            PopupButtonID="imgbtnDateofBirth" Enabled="True" />
                                                                                    </td>
                                                                                    <td id="tdReTime" style="width: 11%; visibility: hidden;">
                                                                                        <asp:Label ID="lblReScheduleTime" runat="server" Text="Reschedule Time"></asp:Label>
                                                                                    </td>
                                                                                    <td id="tdReTimeValue" style="width: 21%; visibility: hidden;">
                                                                                        <asp:DropDownList ID="ddlReSchHours" runat="server" Width="45px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:DropDownList ID="ddlReSchMinutes" runat="server" Width="45px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:DropDownList ID="ddlReSchTime" runat="server" Width="45px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 5%; visibility: hidden;"></td>
                                                                                    <td style="width: 5%; visibility: hidden;"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 15%">Notes
                                                                                    </td>
                                                                                    <td colspan="3">
                                                                                        <asp:TextBox ID="txtNotes" runat="server" Width="96%" Height="100%" TextMode="MultiLine"></asp:TextBox>
                                                                                    </td>
                                                                                    <td id="td1" runat="server">Change Time
                                                                                    </td>
                                                                                    <td id="Td2" style="width: 21%;" runat="server">
                                                                                        <asp:DropDownList ID="ddlchangeReSchHours" runat="server" Width="45px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:DropDownList ID="ddlchangeReSchMinutes" runat="server" Width="45px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:DropDownList ID="ddlchangeReSchTime" runat="server" Width="45px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <%--<td>
                                                </td>--%>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                            </table>
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td style="width: 50%;" align="center">
                                                                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                                                                    </td>
                                                                                    <td style="width: 50%;" align="center">
                                                                                        <asp:Button ID="btnchnagetime" runat="server" Text="Change Time" OnClick="btnchnagetime_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                            <dx:TabPage Text="Transport" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%"
                                                                ActiveTabStyle-BackColor="White" Name="case" TabStyle-BackColor="#B1BEE0">
                                                                <ContentCollection>
                                                                    <dx:ContentControl>
                                                                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                                            <table style="width: 100%" class="ContentTable" border="0">
                                                                                <tr>
                                                                                    <td colspan="2" style="width: 100%">
                                                                                        <dx:ASPxGridView ID="grdTransportVisits" runat="server" KeyFieldName="SZ_CASE_ID"
                                                                                            AutoGenerateColumns="false" Width="100%" SettingsPager-PageSize="50" SettingsCustomizationWindow-Height="330"
                                                                                            Settings-VerticalScrollableHeight="330">
                                                                                            <Columns>
                                                                                                <%--0--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="CASE ID" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Visible="false">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--1--%>
                                                                                                <dx:GridViewDataColumn FieldName="TIME" Caption="Time" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowSort="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--2--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case#" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Width="8%" Settings-AllowSort="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--3--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--4--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_ADDRESS" Caption="Patient Address" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--5--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_PHONE" Caption="Patient Phone" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--6--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_TARNSPOTATION_COMPANY_NAME" Caption="Transport Name"
                                                                                                    Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" Settings-AllowAutoFilter="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--7--%>
                                                                                                <dx:GridViewDataColumn FieldName="DT_TRANS_DATE" Caption="Transport Date" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowSort="False" Settings-AllowAutoFilter="False">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--8--%>
                                                                                                <dx:GridViewDataColumn FieldName="I_TRANS_ID" Caption="I_TRANS_ID" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowSort="False" Width="7%" Settings-AllowAutoFilter="False" Visible="false">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--9--%>
                                                                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_ID" Caption="SZ_PATIENT_ID" HeaderStyle-HorizontalAlign="Center"
                                                                                                    Settings-AllowSort="False" Width="7%" Settings-AllowAutoFilter="False" Visible="false">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <%--10--%>
                                                                                                <dx:GridViewDataColumn Caption="Select" Settings-AllowSort="False">
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllTrans(this.checked);"
                                                                                                            Text="Select" ToolTip="Delete" />
                                                                                                    </HeaderTemplate>
                                                                                                    <DataItemTemplate>
                                                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                    </DataItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                                                                </dx:GridViewDataColumn>
                                                                                            </Columns>
                                                                                            <Settings ShowVerticalScrollBar="true" ShowFilterRow="false" ShowGroupPanel="false" />
                                                                                            <SettingsBehavior AllowFocusedRow="false" />
                                                                                            <SettingsPager Position="Bottom" />
                                                                                        </dx:ASPxGridView>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" style="width: 100%" align="right">
                                                                                        <asp:Button ID="btnTransportdelete" runat="server" Text="Delete" OnClick="btnTransportdelete_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                            <dx:TabPage Text="Day View" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%"
                                                                ActiveTabStyle-BackColor="White" Name="case" TabStyle-BackColor="#B1BEE0">
                                                                <ActiveTabStyle BackColor="White" Font-Bold="True">
                                                                </ActiveTabStyle>
                                                                <TabStyle BackColor="#B1BEE0" Width="100%">
                                                                </TabStyle>
                                                                <ContentCollection>
                                                                    <dx:ContentControl>
                                                                        <asp:Panel ID="Panel3" runat="server" Width="100%">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch3" valign="top" align="center">
                                                                                        <asp:TextBox ID="txtDate" runat="server" MaxLength="10" Width="30%"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                        <ajaxcontrol:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtDate"
                                                                                            PopupButtonID="imgbtnFromDate" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:Button ID="btnshowall" runat="server" Text="Search" OnClick="btnshowall_Click"
                                                                                            Class="Buttons" />
                                                                                        <%--<asp:LinkButton ID="lnkForVistsbyTime" OnClick="lnkForVistsbyTime_OnClick" runat="server" CommandArgument=""></asp:LinkButton>    --%>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td scope="col" style="text-align: center;">
                                                                                        <input id="btnAddScedule" type="button" value="Add Schedule" style="visibility: hidden;"
                                                                                            onclick="return btnAddScedule_onclick()" class="Buttons" />
                                                                                        <asp:DropDownList ID="ddlInterval" runat="server" Width="60px" Visible="False">
                                                                                            <asp:ListItem Text="0.15" Value="0.15"></asp:ListItem>
                                                                                            <asp:ListItem Text="0.30" Selected="True" Value="0.30"></asp:ListItem>
                                                                                            <asp:ListItem Text="0.45" Value="0.45"></asp:ListItem>
                                                                                            <asp:ListItem Text="0.60" Value="0.60"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <div align="right">
                                                                                            &nbsp;
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 75%">
                                                                                        <div id="DivDayView" runat="server">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th scope="col" colspan="3">
                                                                                        <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnSessionValue" runat="server" Value=""></asp:HiddenField>
                                                                                    </th>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                        </TabPages>
                                                    </dx:ASPxPageControl>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                        </table>
                        <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoName" EnableCaching="true"
                            DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtPatientName"
                            ServiceMethod="GetPatient" ServicePath="PatientService.asmx" UseContextKey="true"
                            ContextKey="SZ_COMPANY_ID">
                        </ajaxToolkit:AutoCompleteExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <input type="hidden" id="txtGetDay" runat="server" />
    <input type="hidden" id="txtGetTime" runat="server" />
    <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
    <div style="visibility: hidden;">
        <asp:Button ID="btnCls" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
            OnClick="Link1_Click" />
    </div>
    <div style="visibility: hidden;">
        <asp:Button ID="btnTime" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
            OnClick="lnkTime_OnClick" />
    </div>
    <div style="visibility: hidden;">
        <asp:Button ID="btnShowAllVisits" Text="X" BackColor="#B5DF82" BorderStyle="None"
            runat="server" OnClick="ShowAllVisits_Click" />
    </div>
    <div style="visibility: hidden;">
        <asp:Button ID="btnadd" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
            OnClick="btnadd_Click" />
    </div>
    <dx:ASPxPopupControl ID="ShowVisit" runat="server" CloseAction="CloseButton" HeaderStyle-BackColor="#C1DCFF"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ShowVisit"
        HeaderText="Update Visit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="1000px"
        ToolTip="Update Visit" PopupHorizontalOffset="0" PopupVerticalOffset="0"
        AutoUpdatePosition="true" RenderIFrameForPopupElements="Default" ScrollBars="Vertical"
        Height="370px" EnableHierarchyRecreation="True">
        <ClientSideEvents CloseButtonClick="CloseShowVisit" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="AddVisit" runat="server" CloseAction="CloseButton" HeaderStyle-BackColor="#C1DCFF"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="AddVisit"
        HeaderText="Add Visit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="1100px"
        ToolTip="Add Visit" PopupHorizontalOffset="0" PopupVerticalOffset="0"
        AutoUpdatePosition="true" RenderIFrameForPopupElements="Default" ScrollBars="Vertical"
        Height="510px" EnableHierarchyRecreation="True">
        <ClientSideEvents CloseButtonClick="CloseAddVisit" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="ReportPopUp" runat="server" CloseAction="CloseButton" HeaderStyle-BackColor="#C1DCFF"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ReportPopUp"
        HeaderText="Visit Report" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="1500px"
        PopupHorizontalOffset="0" PopupVerticalOffset="0" AutoUpdatePosition="true"
        RenderIFrameForPopupElements="Default" ScrollBars="Vertical" Height="700px" EnableHierarchyRecreation="True">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>
