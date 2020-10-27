<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewScheduler.aspx.cs" Inherits="AJAX_Pages_NewScheduler" %>

<%--<%@ Register assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>--%>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v16.2.Core, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(EndRequest);



            function EndRequest(sender, args) {

                NewScheduler.GetMainElement().ondblclick = function () {
                    //if (!NewScheduler.cpAppointmentFormOpened) {
                    NewScheduler.RaiseCallback('MNUVIEW|NewAppointment');
                    //}
                }
            }
        });

    </script>
    <style type="text/css">
body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}
</style>
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="updPnlSchedular">
        <ProgressTemplate>

            <div id="Div1" style="text-align: center; vertical-align: bottom;" class="modal"
                >
                <div class="center">
                <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                    Height="25px" Width="24px"></asp:Image>
                Loading...
                    </div>
            </div>


        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updPnlSchedular" runat="server">

        <ContentTemplate>
            <table>
                <tr>
                    <td>Specialty :</td>
                    <td>
                        <dx:ASPxComboBox runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSpeciality_SelectedIndexChanged" TextField="description" ValueField="code" ID="ddlSpeciality"></dx:ASPxComboBox>
                    </td>
                    <td style="width: 20px;"></td>
                    <td>Provider :</td>
                    <td>
                        <dx:ASPxComboBox runat="server" TextField="SZ_DOCTOR_NAME" ValueField="SZ_DOCTOR_ID" OnSelectedIndexChanged="ddlProvider_SelectedIndexChanged" ID="ddlProvider"></dx:ASPxComboBox>
                    </td>
                    <td style="width: 20px;"></td>
                    <td>

                        <dx:ASPxButton Text="Search" runat="server"></dx:ASPxButton>
                    </td>
                </tr>
            </table>


            <dx:ASPxScheduler ID="ASPxScheduler1" OnAppointmentViewInfoCustomizing="ASPxScheduler_AppointmentViewInfoCustomizing1" ClientInstanceName="NewScheduler" runat="server" Width="100%" GroupType="Resource"
                ClientIDMode="AutoID" DataMember="" DataSourceID="" AppointmentDataSourceID="AppointmentDataSource" OnAppointmentChanging="ASPxScheduler1_AppointmentChanging" OnAppointmentInserting="ASPxScheduler1_AppointmentInserting" OnAppointmentRowDeleted="ASPxScheduler1_AppointmentRowDeleted" OnAppointmentsDeleted="ASPxScheduler1_AppointmentsDeleted" OnAppointmentsInserted="ASPxScheduler1_AppointmentsInserted" ResourceDataSourceID="ResData" Theme="Default" OnAppointmentFormShowing="ASPxScheduler1_AppointmentFormShowing" OnBeforeExecuteCallbackCommand="NewScheduler1_BeforeExecuteCallbackCommand" OnPopupMenuShowing="ASPxScheduler1_PopupMenuShowing" OnInitAppointmentDisplayText="ASPxScheduler1_InitAppointmentDisplayText" OnAppointmentsChanged="ASPxScheduler1_AppointmentsChanged" OnCustomJSProperties="ASPxScheduler1_CustomJSProperties" OnInitNewAppointment="ASPxScheduler1_InitNewAppointment">
                <ResourceNavigator EnableIncreaseDecrease="true" />
                <Storage EnableReminders="false" EnableSmartFetch="true">
                    <Appointments CommitIdToDataSource="false" AutoRetrieveId="True" ResourceSharing="false">
                        <Mappings AppointmentId="ID" End="EndTime" Start="StartTime" Subject="Subject" Description="Description"
                            Location="Location" AllDay="AllDay" Type="EventType" RecurrenceInfo="RecurrenceInfo"
                            ReminderInfo="ReminderInfo" Label="Label" Status="Status" ResourceId="MedicId" />
                        <statuses>
                    <dx:AppointmentStatus Color="White" DisplayName="IE" MenuCaption="&amp;IE" Type="Free" />
                    <dx:AppointmentStatus Color="0, 171, 71" DisplayName="FU" MenuCaption="&amp;FU" Type="Tentative" />
                    <dx:AppointmentStatus Color="41, 121, 255" DisplayName="C" MenuCaption="&amp;C" Type="Busy" />
                </statuses>
                        <CustomFieldMappings>
                            <dx:ASPxAppointmentCustomFieldMapping Name="SZ_PATIENT_ID" Member="SZ_PATIENT_ID" ValueType="string" />
                            <dx:ASPxAppointmentCustomFieldMapping Name="SZ_COMPANY_ID" Member="SZ_COMPANY_ID" ValueType="string" />
                            <dx:ASPxAppointmentCustomFieldMapping Name="SZ_DOCTOR_ID" Member="SZ_DOCTOR_ID" ValueType="string" />
                            <dx:ASPxAppointmentCustomFieldMapping Name="VISIT_TYPE_ID" Member="VISIT_TYPE_ID" ValueType="string" />

                        </CustomFieldMappings>

                    </Appointments>
                    <Resources>
                        <Mappings ResourceId="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_NAME" />
                    </Resources>

                </Storage>
                <OptionsForms AppointmentFormTemplateUrl="~/UserForms/ScriptAppointmentForm.ascx" />

                <Views>
                    <DayView ResourcesPerPage="1" WorkTime-End="22:00" ShowWorkTimeOnly="true" WorkTime-Start="7:00">
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>

                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>

                        <DayViewStyles ScrollAreaHeight="400px" />
                    </DayView>
                    <WorkWeekView WorkTime-End="22:00" ShowWorkTimeOnly="true" WorkTime-Start="7:00" ResourcesPerPage="1">
                        <WorkWeekViewStyles ScrollAreaHeight="400px" />
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>

                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                    </WorkWeekView>
                    <WeekView ResourcesPerPage="1" Enabled="false">
                        <WeekViewStyles>
                            <DateCellBody Height="150px" />
                        </WeekViewStyles>
                    </WeekView>
                    <FullWeekView Enabled="true" WorkTime-End="22:00" ShowWorkTimeOnly="true" WorkTime-Start="7:00" ResourcesPerPage="2">
                        <FullWeekViewStyles ScrollAreaHeight="400px" />
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>

                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                    </FullWeekView>
                    <MonthView ResourcesPerPage="1">
                        <MonthViewStyles>
                            <DateCellBody Height="100px" />
                        </MonthViewStyles>
                    </MonthView>
                    <TimelineView ResourcesPerPage="1">
                        <TimelineViewStyles>
                            <TimelineCellBody Height="250px" />
                        </TimelineViewStyles>
                    </TimelineView>
                </Views>
            </dx:ASPxScheduler>



            <asp:SqlDataSource ID="AppointmentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SchedularConnection %>" DeleteCommand="DELETE FROM [MedicalAppointments] WHERE [ID] = @ID" InsertCommand="INSERT INTO [MedicalAppointments] ([MedicId], [MedicIds], [Status], [Subject], [Description], [Label], [StartTime], [EndTime], [Location], [AllDay], [EventType], [RecurrenceInfo], [ReminderInfo], [ContactInfo], [SZ_PATIENT_ID], [SZ_COMPANY_ID], [SZ_DOCTOR_ID], [VISIT_TYPE_ID]) VALUES (@SZ_DOCTOR_ID, @MedicIds, @Status, @Subject, @Description, @Label, @StartTime, @EndTime, @Location, @AllDay, @EventType, @RecurrenceInfo, @ReminderInfo, @ContactInfo, @SZ_PATIENT_ID, @SZ_COMPANY_ID, @SZ_DOCTOR_ID, @VISIT_TYPE_ID)" SelectCommand="SELECT * FROM [MedicalAppointments]" UpdateCommand="UPDATE [MedicalAppointments] SET [MedicId] = @MedicId, [MedicIds] = @MedicIds, [Status] = @Status, [Subject] = @Subject, [Description] = @Description, [Label] = @Label, [StartTime] = @StartTime, [EndTime] = @EndTime, [Location] = @Location, [AllDay] = @AllDay, [EventType] = @EventType, [RecurrenceInfo] = @RecurrenceInfo, [ReminderInfo] = @ReminderInfo, [ContactInfo] = @ContactInfo, [SZ_PATIENT_ID] = @SZ_PATIENT_ID, [SZ_COMPANY_ID] = @SZ_COMPANY_ID, [SZ_DOCTOR_ID] = @SZ_DOCTOR_ID, [VISIT_TYPE_ID] = @VISIT_TYPE_ID WHERE [ID] = @ID">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="MedicId" Type="String" />
                    <asp:Parameter Name="MedicIds" Type="String" />
                    <asp:Parameter Name="Status" Type="Int32" />
                    <asp:Parameter Name="Subject" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Label" Type="Int32" />
                    <asp:Parameter Name="StartTime" Type="DateTime" />
                    <asp:Parameter Name="EndTime" Type="DateTime" />
                    <asp:Parameter Name="Location" Type="String" />
                    <asp:Parameter Name="AllDay" Type="Boolean" />
                    <asp:Parameter Name="EventType" Type="Int32" />
                    <asp:Parameter Name="RecurrenceInfo" Type="String" />
                    <asp:Parameter Name="ReminderInfo" Type="String" />
                    <asp:Parameter Name="ContactInfo" Type="String" />
                    <asp:Parameter Name="SZ_PATIENT_ID" Type="String" />
                    <asp:Parameter Name="SZ_COMPANY_ID" Type="String" />
                    <asp:Parameter Name="SZ_DOCTOR_ID" Type="String" />
                    <asp:Parameter Name="VISIT_TYPE_ID" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="MedicId" Type="String" />
                    <asp:Parameter Name="MedicIds" Type="String" />
                    <asp:Parameter Name="Status" Type="Int32" />
                    <asp:Parameter Name="Subject" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Label" Type="Int32" />
                    <asp:Parameter Name="StartTime" Type="DateTime" />
                    <asp:Parameter Name="EndTime" Type="DateTime" />
                    <asp:Parameter Name="Location" Type="String" />
                    <asp:Parameter Name="AllDay" Type="Boolean" />
                    <asp:Parameter Name="EventType" Type="Int32" />
                    <asp:Parameter Name="RecurrenceInfo" Type="String" />
                    <asp:Parameter Name="ReminderInfo" Type="String" />
                    <asp:Parameter Name="ContactInfo" Type="String" />
                    <asp:Parameter Name="SZ_PATIENT_ID" Type="String" />
                    <asp:Parameter Name="SZ_COMPANY_ID" Type="String" />
                    <asp:Parameter Name="SZ_DOCTOR_ID" Type="String" />
                    <asp:Parameter Name="VISIT_TYPE_ID" Type="String" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>



            <asp:SqlDataSource ID="ResData" OnInit="Sche_Pre_Init" runat="server" ConnectionString="<%$ ConnectionStrings:SchedularConnection %>"></asp:SqlDataSource>




            <script type="text/javascript">
        

            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        NewScheduler.GetMainElement().ondblclick = function () {
            //if (!NewScheduler.cpAppointmentFormOpened) {
            NewScheduler.RaiseCallback('MNUVIEW|NewAppointment');
            //}
        }
    </script>


</asp:Content>

