<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_Patient_Desk.aspx.cs"
    MasterPageFile="~/Provider/MasterPage.master" Inherits="Provider_Test_Patient_Desk" %>

<%@ Register Src="~/UserControl/PatientInformation.ascx" TagName="UserPatientInfoControl"
    TagPrefix="UserPatientInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="oem" Namespace="OboutInc.EasyMenu_Pro" Assembly="obout_EasyMenu_Pro" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table width="100%" border="0">
            <tr>
                <td style="width: 90%;" valign="top">
                    <table id="First" style="width: 100%; height: 100%" bgcolor="white">
                        <tr>
                            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                                padding-top: 3px; height: 100%">
                                <table id="MainBodyTable" style="width: 100%; border: 0;">
                                    <tr>
                                        <td class="LeftTop">
                                        </td>
                                        <td class="CenterTop">
                                        </td>
                                        <td class="RightTop">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LeftCenter" style="height: 100%">
                                        </td>
                                        <td style="width: 100%">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <UserPatientInfo:UserPatientInfoControl runat="server" ID="UserPatientInfoControl" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table id="tblTabTreatment" runat="server" style="width: 100%; vertical-align: top;"
                                    height="60">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <ajaxToolkit:TabContainer ID="tabVistInformation" runat="Server" ActiveTabIndex="1">
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlOne" Visible="true" TabIndex="1">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadOne" runat="server" Text="" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdOne" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwo" TabIndex="1" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwo" runat="server" Text="" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwo" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--<asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlThree" TabIndex="2" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadThree" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdThree" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--<asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />
--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlFour" TabIndex="3" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadFour" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdFour" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlFive" TabIndex="4" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadFive" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdFive" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlSix" TabIndex="5" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadSix" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdSix" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--   <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlSeven" TabIndex="6" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadSeven" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdSeven" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--<asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlEight" TabIndex="7" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadEight" runat="server" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdEight" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlNine" TabIndex="8" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadNine" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdNine" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--   <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTen" TabIndex="9" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTen" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <div style="border: 3px solid #c0cff0; width: auto; height: auto; text-align: right;">
                                                                        <asp:DataGrid ID="grdTen" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                            <HeaderStyle CssClass="GridHeader1" />
                                                                            <ItemStyle CssClass="GridRow" />
                                                                            <Columns>
                                                                                <%--0--%>
                                                                                <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                    Visible="false"></asp:BoundColumn>
                                                                                <%--1--%>
                                                                                <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                <%--2--%>
                                                                                <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                <%--3--%>
                                                                                <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                    Visible="false"></asp:BoundColumn>
                                                                                <%--4--%>
                                                                                <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                <%--5--%>
                                                                                <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                            <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                            <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                            <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                            <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--6--%>
                                                                                <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                    Visible="false"></asp:BoundColumn>
                                                                                <%--7--%>
                                                                                <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--8--%>
                                                                                <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                    Visible="false"></asp:BoundColumn>
                                                                                <%--9--%>
                                                                                <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                    Visible="false"></asp:BoundColumn>
                                                                                <%--10--%>
                                                                                <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                    Visible="false"></asp:BoundColumn>
                                                                                <%--11--%>
                                                                                <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                    Visible="false"></asp:BoundColumn>
                                                                                <%--12--%>
                                                                                <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                    Visible="false"></asp:BoundColumn>
                                                                                <%--13--%>
                                                                                <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--14--%>
                                                                                <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                </asp:BoundColumn>
                                                                                <%--15--%>
                                                                                <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                <%--16--%>
                                                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                </asp:BoundColumn>
                                                                                <%--17--%>
                                                                                <asp:TemplateColumn Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                        </asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--18--%>
                                                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                </asp:BoundColumn>
                                                                                <%--19--%>
                                                                                <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                </asp:BoundColumn>
                                                                                <%--20--%>
                                                                                <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                <%--21--%>
                                                                                <asp:TemplateColumn Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                        </asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--22--%>
                                                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                <%--23--%>
                                                                                <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                <%--24--%>
                                                                                <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                                </asp:BoundColumn>
                                                                                <%--25--%>
                                                                                <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                                </asp:BoundColumn>
                                                                                <%--26--%>
                                                                                <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                    Visible="false"></asp:BoundColumn>
                                                                                <%--27--%>
                                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                    Visible="false"></asp:BoundColumn>
                                                                                <%--28--%>
                                                                                <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                            </Columns>
                                                                        </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlEleven" TabIndex="10" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadEleven" runat="server" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdEleven" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwelve" TabIndex="11" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwelve" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwelve" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--   <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlThirteen" TabIndex="12" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadThirteen" runat="server" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdThirteen" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlFourteen" TabIndex="13" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadFourteen" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdFourteen" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--<asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlFifteen" TabIndex="14" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadFifteen" runat="server" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdFifteen" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--<asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlSixteen" TabIndex="15" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadSixteen" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdSixteen" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--  <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlSeventeen" TabIndex="16" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadSeventeen" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdSeventeen" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlEighteen" TabIndex="17" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadEighteen" runat="server" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdEighteen" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlNineteen" TabIndex="18" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadNineteen" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdNineteen" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--  <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwenty" TabIndex="19" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwenty" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwenty" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwentyOne" TabIndex="20" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwentyOne" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwentyOne" Width="100%" CssClass="GridTable" runat="Server">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwentyTwo" TabIndex="21" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwentyTwo" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwentyTwo" Width="100%" CssClass="GridTable" runat="Server">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--  <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwentyThree" TabIndex="22" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwentyThree" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwentyThree" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwentyFour" TabIndex="23" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwentyFour" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwentyFour" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--<asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwentyFive" TabIndex="24" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwentyFive" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwentyFive" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--<asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwentySix" TabIndex="25" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwentySix" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwentySix" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--  <%--15--%>
                                                                            <%--                                                            <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwentySeven" TabIndex="26" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwentySeven" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwentySeven" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--  <%--15--%>
                                                                            <%--  <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <%--New Tab Added - SY - 14 Apr - 2010--%>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwentyEight" TabIndex="26" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwentyEight" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwentyEight" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--   <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwentyNine" TabIndex="26" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadTwentyNine" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdTwentyNine" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlThirty" TabIndex="26" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadThirty" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdThirty" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlThirtyOne" TabIndex="26" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadThirtyOne" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdThirtyOne" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%-- <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlThirtyTwo" TabIndex="26" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadThirtyTwo" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdThirtyTwo" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--<asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlThirtyThree" TabIndex="26" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadThirtyThree" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdThirtyThree" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <%--15--%>
                                                                            <%--  <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                UpdateText="Update" />--%>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlThirtyFour" TabIndex="26" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadThirtyFour" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdThirtyFour" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlThirtyFive" TabIndex="26" Visible="False">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblHeadThirtyFive" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:DataGrid ID="grdThirtyFive" Width="100%" CssClass="GridTable" runat="Server"
                                                                        AutoGenerateColumns="False">
                                                                        <HeaderStyle CssClass="GridHeader1" />
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--1--%>
                                                                            <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--2--%>
                                                                            <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--3--%>
                                                                            <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--4--%>
                                                                            <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                            <%--5--%>
                                                                            <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                        <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--6--%>
                                                                            <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--7--%>
                                                                            <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--8--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--9--%>
                                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--10--%>
                                                                            <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--11--%>
                                                                            <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--12--%>
                                                                            <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--13--%>
                                                                            <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--14--%>
                                                                            <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--15--%>
                                                                            <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                            <%--16--%>
                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--17--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--18--%>
                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--19--%>
                                                                            <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--20--%>
                                                                            <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                            <%--21--%>
                                                                            <asp:TemplateColumn Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <%--22--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                            <%--23--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                            <%--24--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--25--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false">
                                                                            </asp:BoundColumn>
                                                                            <%--26--%>
                                                                            <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--27--%>
                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                                                Visible="false"></asp:BoundColumn>
                                                                            <%--28--%>
                                                                            <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtcaseno" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtOffice_ID" runat="server" Visible="false"></asp:TextBox>
        <asp:HiddenField ID="hdnCaseId" runat="server" />
        <asp:HiddenField ID="hdnCaseNo" runat="server" />
        <asp:HiddenField ID="hdnCompanyId" runat="server" />
        <asp:HiddenField ID="tabid" runat="server" />
    </div>
</asp:Content>
