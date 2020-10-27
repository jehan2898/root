<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyAppointmentForm.ascx.cs"
    Inherits="Templates_MyAppointmentForm" %>

<table style="width: 100%; border-collapse: separate; border-spacing: 4px;">
    <tr>
        <td style="width: 57px; padding-right: 5px;">
            <dx:aspxlabel ID="lblSubject" runat="server" Text="Subject:" AssociatedControlID="tbSubject">
            </dx:aspxlabel>
        </td>
        <td>
            <dx:aspxtextbox ID="tbSubject" runat="server" Width="100%" Text='<%# ((MyAppointmentFormTemplateContainer)Container).Subject %>'>
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField ErrorText="The Subject must contain at least one character." IsRequired="True" />
                </ValidationSettings>
                <ClientSideEvents  ValueChanged="function(s, e) { OnUpdateControlValue(s, e); }" KeyUp="function(s,e) { OnUpdateControlValue(s,e); }" />
            </dx:aspxtextbox>
            <asp:SqlDataSource ID="PatientDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SchedularConnection %>" SelectCommand="Select  SZ_PATIENT_ID,Name FROM PatientList" ProviderName="System.Data.SqlClient">
            </asp:SqlDataSource>
        </td>
        <td style="padding-right: 5px;">
            <dx:aspxlabel ID="lblLocation" runat="server" Text="Location:" AssociatedControlID="tbLocation">
            </dx:aspxlabel>
        </td>
        <td>
            <dx:aspxtextbox ID="tbLocation" Width="100%" runat="server" Text='<%#((MyAppointmentFormTemplateContainer)Container).Appointment.Location %>'>
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField IsRequired="true" />
                </ValidationSettings>
                <ClientSideEvents  ValueChanged="function(s, e) { OnUpdateControlValue(s, e); }" KeyUp="function(s,e) { OnUpdateControlValue(s,e); }" />
            </dx:aspxtextbox>
        </td>
    </tr>
    <tr>
        <td style="width: 57px; padding-right: 5px;">
            <dx:aspxlabel ID="lblVisitType" runat="server" Text="Visit Type:" AssociatedControlID="tbSubject">
            </dx:aspxlabel></td>
        <td>
                         <dx:aspxcombobox  ID="ddlVisit" runat="server" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith"
                Width="100%" DataSourceID="VisitTypeDataSource" TextField="VISIT_TYPE" ValueField="VISIT_TYPE_ID" ValueType="System.String" />
             <asp:SqlDataSource ID="VisitTypeDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SchedularConnection %>" SelectCommand="SELECT [VISIT_TYPE_ID], [VISIT_TYPE] FROM [MST_VISIT_TYPE] Where SZ_Company_ID='CO000000000000000135'">

                 
             </asp:SqlDataSource>
        </td>
        <td style="padding-right: 5px;">
                         &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 57px; padding-right: 5px;">
            <dx:aspxlabel ID="ASPxLabel1" runat="server" Text="Patient:" AssociatedControlID="tbSubject">
            </dx:aspxlabel></td>
        <td>
             <dx:aspxcombobox ID="ddlPatient" runat="server" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith"
                DataSourceID="PatientDataSource" TextField="Name" ValueField="SZ_PATIENT_ID" ValueType="System.String"
                Width="100%" />
            </td>
        <td style="padding-right: 5px;">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
        <tr>
        <td style="width: 57px; padding-right: 5px;">
            <dx:aspxlabel ID="lblDoctor" runat="server" Text="Doctor:" AssociatedControlID="ddlDoctor">
            </dx:aspxlabel></td>
        <td>
             <dx:aspxcombobox ID="ddlDoctor" runat="server" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith"
                DataSourceID="DoctorDatasource" TextField="SZ_DOCTOR_NAME" ValueField="SZ_DOCTOR_ID"
                Width="100%" ValueType="System.String"/>
             <asp:SqlDataSource ID="DoctorDatasource" runat="server" ConnectionString="<%$ ConnectionStrings:SchedularConnection %>" SelectCommand="Select MST_DOCTOR.SZ_DOCTOR_NAME+' ('+MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP+')' as SZ_DOCTOR_NAME,MST_DOCTOR.SZ_DOCTOR_ID from MST_DOCTOR
JOIN TXN_DOCTOR_SPECIALITY ON MST_DOCTOR.SZ_DOCTOR_ID=TXN_DOCTOR_SPECIALITY.SZ_DOCTOR_ID
JOIN MST_PROCEDURE_GROUP ON MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP_ID=TXN_DOCTOR_SPECIALITY.SZ_PROCEDURE_GROUP_ID
 Where MST_DOCTOR.SZ_COMPANY_ID='CO000000000000000135'"></asp:SqlDataSource>
            </td>
        <td style="padding-right: 5px;">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="white-space: nowrap; padding-right: 5px;">
            <dx:aspxlabel ID="lblStartTime" runat="server" Text="Start time:" AssociatedControlID="edtStartDate">
            </dx:aspxlabel>
        </td>
        <td>
            <dx:aspxdateedit ID="edtStartDate" runat="server" Date='<%# ((MyAppointmentFormTemplateContainer)Container).Start %>'
                Width="100%" EditFormat="DateTime" ClientInstanceName="edtStartTimeAppointmentForm">
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" EnableCustomValidation="True"
                    ValidationGroup="DateValidatoinGroup">
                    <RequiredField ErrorText="The field cannot be blank." IsRequired="True" />
                </ValidationSettings>
                <ClientSideEvents Validation="function(s, e) { OnStartTimeValidate(s, e); }" ValueChanged="function(s, e) {	OnUpdateControlValue(s, e); }" />
            </dx:aspxdateedit>
        </td>
        <td style="white-space: nowrap; padding-right: 5px;">
            <dx:aspxlabel ID="lblEndTime" runat="server" Text="End time:" AssociatedControlID="edtEndDate">
            </dx:aspxlabel>
        </td>
        <td>
            <dx:aspxdateedit ID="edtEndDate" runat="server" Date='<%# ((MyAppointmentFormTemplateContainer)Container).End %>'
                Width="100%" EditFormat="DateTime" ClientInstanceName="edtEndTimeAppointmentForm">
                <ClientSideEvents Validation="function(s, e) {	OnEndTimeValidate(s, e); }" ValueChanged="function(s, e) { OnUpdateControlValue(s, e); }" />
                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip"
                    ValidationGroup="DateValidatoinGroup">
                    <RequiredField ErrorText="Cannot be empty" IsRequired="True" />
                </ValidationSettings>
            </dx:aspxdateedit>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <dx:aspxlabel ID="lblDescription" runat="server" Text="Notes:" AssociatedControlID="memDescription">
            </dx:aspxlabel>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <dx:aspxmemo ID="memDescription" runat="server" Rows="2" Text='<%#((MyAppointmentFormTemplateContainer)Container).Appointment.Description %>'
                Width="100%">
            </dx:aspxmemo>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <dx:aspxlabel ID="lblResource" runat="server" Text="Resource:" AssociatedControlID="memContacts">
            </dx:aspxlabel>
        </td>
    </tr>
    <tr>
        <td colspan="4">

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SchedularConnection %>" SelectCommand="SELECT * FROM [Medics]"></asp:SqlDataSource></td>
    </tr>
</table>
<dx:appointmentrecurrenceform ID="AppointmentRecurrenceForm1" runat="server" IsRecurring='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).Appointment.IsRecurring %>'
    DayNumber='<%# ((MyAppointmentFormTemplateContainer)Container).RecurrenceDayNumber %>'
    End='<%# ((MyAppointmentFormTemplateContainer)Container).RecurrenceEnd %>' Month='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).RecurrenceMonth %>'
    OccurrenceCount='<%# ((MyAppointmentFormTemplateContainer)Container).RecurrenceOccurrenceCount %>'
    Periodicity='<%# ((MyAppointmentFormTemplateContainer)Container).RecurrencePeriodicity %>'
    RecurrenceRange='EndByDate'
    Start='<%# ((MyAppointmentFormTemplateContainer)Container).RecurrenceStart %>'
    WeekDays='<%# ((MyAppointmentFormTemplateContainer)Container).RecurrenceWeekDays %>'
    WeekOfMonth='<%# ((MyAppointmentFormTemplateContainer)Container).RecurrenceWeekOfMonth %>'
    RecurrenceType='<%# ((MyAppointmentFormTemplateContainer)Container).RecurrenceType %>'
    IsFormRecreated='<%# ((MyAppointmentFormTemplateContainer)Container).IsFormRecreated %>' OnPreRender="AppointmentRecurrenceForm1_PreRender"
    
    >

</dx:appointmentrecurrenceform>
<script type="text/javascript">

    function hide() {

        document.getElementById("ctl00_ContentPlaceHolder1_ASPxScheduler1_formBlock_AptFrmContainer_AptFrmTemplateContainer_AppointmentForm_AppointmentRecurrenceForm1_AptRecCtl_RangeCtl_DeNoEnd").style.display = none;
    }
</script>
<table onload="hide()" style="width: 100%; height: 35px;">
    <tr>
        <td style="width: 100%; height: 100%;" align="center">
            <table style="height: 100%;">
                <tr>
                    <td>
                        <dx:ASPxButton runat="server" ID="btnOk" Text="OK" UseSubmitBehavior="False" AutoPostBack="False"
                            ClientInstanceName="btnMyAppointmentFormOk" EnableViewState="False" Width="91px"
                            ValidationGroup="MyValidationGroup">
                            <ClientSideEvents Init="function(s, e) { OnUpdateControlValue(s, e); }" />
                        </dx:ASPxButton>
                    </td>
                    <td style="padding: 0 4px;">
                        <dx:ASPxButton runat="server" ID="btnCancel" Text="Cancel" UseSubmitBehavior="false"
                            AutoPostBack="false" EnableViewState="false" Width="91px" />
                    </td>
                    <td>
                        <dx:aspxbutton runat="server" ID="btnDelete" Text="Delete" UseSubmitBehavior="false"
                            AutoPostBack="false" EnableViewState="false" Width="91px" Enabled='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).CanDeleteAppointment %>' />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table style="width: 100%;">
    <tr>
        <td style="width: 100%;" align="left">
            <dx:ASPxSchedulerStatusInfo runat="server" ID="schedulerStatusInfo" Priority="1"
                MasterControlID='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).ControlId %>' />
        </td>
    </tr>
</table>
<script id="dxss_myAppoinmentForm007" type="text/javascript">
    function OnStartTimeValidate(s, e) {
        if(!e.isValid)
            return;
        var startDate = edtStartTimeAppointmentForm.GetDate();
        var endDate = edtEndTimeAppointmentForm.GetDate();
        e.isValid = startDate == null || endDate == null || startDate < endDate;
        e.errorText = "The Start Date must precede the End Date.";
    }
    function OnEndTimeValidate(s, e) {
        if(!e.isValid)
            return;
        var startDate = edtStartTimeAppointmentForm.GetDate();
        var endDate = edtEndTimeAppointmentForm.GetDate();
        e.isValid = startDate == null || endDate == null || startDate < endDate;
        e.errorText = "The Start Date must precede the End Date.";
    }
    function OnUpdateControlValue(s, e) {
        var isValid = ASPxClientEdit.ValidateEditorsInContainer(null);
        btnMyAppointmentFormOk.SetEnabled(isValid)
    }
    window.setTimeout("ASPxClientEdit.ValidateEditorsInContainer(null)", 0);

    function OnSelectedIndexChanged(s, e) {
        var items = listbox.GetSelectedItems();
        var text = "";
        var values = "";
        for (var i = 0; i < items.length; i++) {
            text += items[i].text + ";";
            values += items[i].value + ";";
        }
        dd.SetText(text)
        dd.SetKeyValue(values);
    }
</script>
