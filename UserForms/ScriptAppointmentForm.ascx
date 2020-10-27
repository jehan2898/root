<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ScriptAppointmentForm.ascx.cs"
    Inherits="UserForms_ScriptAppointmentForm" %>
<%@ Register Src="~/UserForms/ScriptRecurrenceForm.ascx" TagName="RecurrenceControl" TagPrefix="demo" %>

<table class="dxscAppointmentForm" id="ClientAppointmentForm1" style="width: 800px; height: 100px;">
    <tr>
        <td colspan="7" style="width: 100%; padding-right: 5px;">
            <dx:ASPxCheckBox ID="chkNewPatient" ClientInstanceName="chkNewPatient" EnableClientSideAPI="True" Text="New Patient" runat="server" Checked="true"></dx:ASPxCheckBox>
        </td>

    </tr>
    <tr>
        <td style="width: 12%; padding-right: 5px;">
            <input runat="server" type="hidden" id="HdnAppId" clientidmode="Static" />
            <input runat="server" type="hidden" id="HdnType" clientidmode="Static" />
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Patient:">
            </dx:ASPxLabel>
        </td>
        <td style="width: 37%;">
            <dx:ASPxComboBox ID="ddlPatient" EnableIncrementalFiltering="true" IncrementalFilteringMode="Contains" EnableClientSideAPI="true" ClientInstanceName="cddlPatient" runat="server" DropDownStyle="DropDownList"
                DataSourceID="PatientDataSource" TextField="Name" ValueField="SZ_PATIENT_ID" ValueType="System.String"
                Width="100%" OnCallback="ddlPatient_Callback" />
            <asp:SqlDataSource ID="PatientDataSource" OnInit="PatientDataSource_Init" runat="server" ConnectionString="<%$ ConnectionStrings:SchedularConnection %>" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
        </td>
        <td style="width: 2%; padding-right: 5px;">&nbsp;</td>

        <td style="width: 10%; padding-right: 5px;">

            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Case No:">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxLabel ID="lblCaseNo" ClientInstanceName="lblCaseNo" runat="server">
            </dx:ASPxLabel>
        </td>
        <td style="width: 10%; padding-right: 5px;">

            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="DOB:">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxLabel ID="lblDOB" ClientInstanceName="lblDOB" runat="server">
            </dx:ASPxLabel>
        </td>

    </tr>
    <tr>
        <td style="padding-right: 5px;">
            <dx:ASPxLabel ID="lblDoctor" runat="server" Text="Doctor:" AssociatedControlID="ddlDoctor">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxComboBox ID="ddlDoctor" ClientInstanceName="cddlDoctor" IncrementalFilteringMode="Contains" EnableIncrementalFiltering="true" runat="server" DropDownStyle="DropDownList"
                DataSourceID="DoctorDatasource" TextField="SZ_DOCTOR_NAME" ValueField="SZ_DOCTOR_ID"
                Width="100%" ValueType="System.String" EnableClientSideAPI="True" OnCallback="ddlDoctor_Callback" />
            <asp:SqlDataSource ID="DoctorDatasource" OnInit="DoctorDatasource_Init" runat="server" ConnectionString="<%$ ConnectionStrings:SchedularConnection %>"></asp:SqlDataSource>
        </td>
        <td style="padding-right: 5px;">&nbsp;</td>

        <td style="width: 10%; padding-right: 5px;">

            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="DOA:" AssociatedControlID="tbSubject">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxLabel ID="lblDOA" ClientInstanceName="lblDOA" runat="server">
            </dx:ASPxLabel>
        </td>
        <td style="width: 10%; padding-right: 5px;">

            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Phone No:">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxLabel ID="lblPhone" ClientInstanceName="lblPhone" runat="server">
            </dx:ASPxLabel>
        </td>
    </tr>


    <tr>
        <td style="padding-right: 5px;">
            <dx:ASPxLabel ID="lblVisitType" runat="server" Text="Visit Type:">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxComboBox ID="ddlVisit" EnableClientSideAPI="true" IncrementalFilteringMode="Contains" EnableIncrementalFiltering="true" runat="server" DropDownStyle="DropDownList"
                Width="100%" DataSourceID="VisitTypeDataSource" ClientInstanceName="cddlVisit" TextField="VISIT_TYPE" ValueField="VISIT_TYPE_ID" ValueType="System.String" OnCallback="ddlVisit_Callback" />
            <asp:SqlDataSource ID="VisitTypeDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SchedularConnection %>" SelectCommand="SELECT [VISIT_TYPE_ID], [VISIT_TYPE] FROM [MST_VISIT_TYPE] Where SZ_Company_ID='CO000000000000000135'"></asp:SqlDataSource>
        </td>
        <td style="padding-right: 5px;">&nbsp;</td>

        <td style="width: 10%; padding-right: 5px;">

            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Attorny Name:">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxLabel ID="lblAttorny" ClientInstanceName="lblAttorny" runat="server">
            </dx:ASPxLabel>
        </td>
        <td style="width: 10%; padding-right: 5px;">

            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Attorny No:">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxLabel ID="lblAttornyNo" ClientInstanceName="lblAttornyNo" runat="server">
            </dx:ASPxLabel>
        </td>
    </tr>
    <tr runat="server" id="ddlStatustr">
        <td style="width: 57px; padding-right: 5px;">
            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Status:" AssociatedControlID="ddlDoctor">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxComboBox ID="ddlStatus" ClientEnabled="true" ClientInstanceName="cddlStatus" runat="server" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
                Width="100%" ValueType="System.String" EnableClientSideAPI="True" OnCallback="ddlStatus_Callback">
                <Items>
                    <dx:ListEditItem Text="Scheduled" Selected="true" Value="0" />
                    <dx:ListEditItem Text="Re-Scheduled" Value="1" />
                    <dx:ListEditItem Text="Completed" Value="2" />
                    <dx:ListEditItem Text="No-show" Value="3" />

                </Items>
            </dx:ASPxComboBox>

        </td>
        <td style="padding-right: 5px;">&nbsp;</td>

        <td style="width: 10%; padding-right: 5px;"></td>
        <td></td>
        <td style="width: 10%; padding-right: 5px;"></td>
        <td></td>
    </tr>
</table>
<table class="dxscAppointmentForm" id="ClientAppointmentForm" style="width: 500px; height: 100px;">
    <%-- <tr>
        <td style="width: 57px; padding-right: 5px;">
            
            <dx:ASPxLabel ID="lblSubject" runat="server" Text="Subject:" >
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxTextBox ID="tbSubject" runat="server" Width="100%" Text='<%# ((MyAppointmentFormTemplateContainer)Container).Subject %>'>
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField ErrorText="The Subject must contain at least one character." IsRequired="True" />
                </ValidationSettings>
                <ClientSideEvents ValueChanged="function(s, e) { OnUpdateControlValue(s, e); }" KeyUp="function(s,e) { OnUpdateControlValue(s,e); }" />
            </dx:ASPxTextBox>
            
        </td>
        <td style="padding-right: 5px;">
            <dx:ASPxLabel ID="lblLocation" runat="server" Text="Location:" AssociatedControlID="tbLocation">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxTextBox ID="tbLocation" Width="100%" runat="server" Text='<%#((MyAppointmentFormTemplateContainer)Container).Appointment.Location %>'>
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField IsRequired="true" />
                </ValidationSettings>
                <ClientSideEvents ValueChanged="function(s, e) { OnUpdateControlValue(s, e); }" KeyUp="function(s,e) { OnUpdateControlValue(s,e); }" />
            </dx:ASPxTextBox>
        </td>
    </tr>--%>


    <tr>
        <td style="white-space: nowrap; padding-right: 5px;">
            <dx:ASPxLabel ID="lblStartTime" runat="server" Text="Start time:" AssociatedControlID="edtStartDate">
            </dx:ASPxLabel>
        </td>
        <td>

            <dx:ASPxDateEdit ID="edtStartDate" runat="server" Date='<%# ((MyAppointmentFormTemplateContainer)Container).Start %>'
                Width="100%" EditFormat="DateTime" TimeSectionProperties-Visible="true" ClientInstanceName="edtStartTimeAppointmentForm">
                <TimeSectionProperties>
                    <TimeEditProperties EditFormatString="hh:mm tt" />
                </TimeSectionProperties>
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" EnableCustomValidation="True"
                    ValidationGroup="DateValidatoinGroup">
                    <RequiredField ErrorText="The field cannot be blank." IsRequired="True" />
                </ValidationSettings>
                <ClientSideEvents Validation="function(s, e) { OnStartTimeValidate(s, e); }" ValueChanged="function(s, e) {	OnUpdateControlValue(s, e); }" />
            </dx:ASPxDateEdit>
        </td>
        <td style="white-space: nowrap; padding-right: 5px;">
            <dx:ASPxLabel ID="lblEndTime" runat="server" Text="End time:" AssociatedControlID="edtEndDate">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxDateEdit ID="edtEndDate" runat="server" Date='<%# ((MyAppointmentFormTemplateContainer)Container).End %>'
                Width="100%" EditFormat="DateTime" TimeSectionProperties-Visible="true" ClientInstanceName="edtEndTimeAppointmentForm">
                <ClientSideEvents Validation="function(s, e) {	OnEndTimeValidate(s, e); }" ValueChanged="function(s, e) { OnUpdateControlValue(s, e); }" />
                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip"
                    ValidationGroup="DateValidatoinGroup">
                    <RequiredField ErrorText="Cannot be empty" IsRequired="True" />
                </ValidationSettings>
            </dx:ASPxDateEdit>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <dx:ASPxLabel ID="lblDescription" runat="server" Text="Notes:" AssociatedControlID="memDescription">
            </dx:ASPxLabel>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <dx:ASPxMemo ID="memDescription" runat="server" Rows="2" Text='<%#((MyAppointmentFormTemplateContainer)Container).Appointment.Description %>'
                Width="100%">
            </dx:ASPxMemo>
        </td>
    </tr>
    <%--<tr>
        <td colspan="4">
            <dx:ASPxLabel ID="lblResource" runat="server" Text="Resource:" AssociatedControlID="memContacts">
            </dx:ASPxLabel>
        </td>
    </tr>--%>
    <tr>
        <td colspan="4">

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SchedularConnection %>" SelectCommand="SELECT * FROM [Medics]"></asp:SqlDataSource>
        </td>
    </tr>
</table>
<dx:ASPxCheckBox ID="chkRecurrence" runat="server" Text="Recurrence">
</dx:ASPxCheckBox>
<demo:RecurrenceControl runat="server" ID="recurrenceControl"></demo:RecurrenceControl>
<table style="width: 500px; height: 35px;">
    <tr>
        <td style="width: 100%; height: 100%;">
            <table style="height: 100%; margin: 0 auto;">
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
                            AutoPostBack="false" EnableViewState="false" Width="91px" CausesValidation="False" />
                    </td>
                    <td>
                        <dx:ASPxButton runat="server" ID="btnDelete" Text="Delete" EnableClientSideAPI="true"
                            UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false" Width="91px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<script id="dxss_myAppoinmentForm007" type="text/javascript">
    function OnStartTimeValidate(s, e) {
        if (!e.isValid)
            return;
        var startDate = edtStartTimeAppointmentForm.GetDate();
        var endDate = edtEndTimeAppointmentForm.GetDate();
        e.isValid = startDate == null || endDate == null || startDate < endDate;
        e.errorText = "The Start Date must precede the End Date.";
    }
    function OnEndTimeValidate(s, e) {
        if (!e.isValid)
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
<script id="dxss_ASPxSchedulerClientAppoinmentForm" type="text/javascript">
    // <![CDATA[
    function OnMenuItemClick(s, e) {
        e.handled = true;
        switch (e.itemName) {
            case ASPx.SchedulerMenuItemId.NewAppointment:
                NewAppointment(scheduler);
                break;
            case ASPx.SchedulerMenuItemId.NewRecurringAppointment:
                NewRecurringAppointment(scheduler);
                break;
            case ASPx.SchedulerMenuItemId.NewAllDayEvent:
                NewAllDayEvent(scheduler);
                break;
            case ASPx.SchedulerMenuItemId.NewRecurringEvent:
                NewRecurringEvent(scheduler);
                break;
            case ASPx.SchedulerMenuItemId.OpenAppointment:
                OpenAppointment(scheduler);
                break;
            case ASPx.SchedulerMenuItemId.EditSeries:
                EditSeries(scheduler);
                break;
            default:
                e.handled = false;
        }
    }
    ASPxClientAppointmentForm = ASPx.CreateClass(ASPxClientFormBase, {
        lastValue: "",
        Initialize: function () {
            this.NullPatient = false;
            this.controls.recurrenceControl.SetVisible(false);
            this.scheduler = ASPxClientScheduler.Cast("NewScheduler");
            this.controls.chkRecurrence.SetVisible(false);

            this.controls.btnDelete.SetVisible(false);
            this.controls.btnOk.Click.AddHandler(ASPx.CreateDelegate(this.OnBtnOkClick, this));
            this.controls.btnCancel.Click.AddHandler(ASPx.CreateDelegate(this.OnBtnCancelClick, this));
            this.controls.btnDelete.Click.AddHandler(ASPx.CreateDelegate(this.OnBtnDeleteClick, this));
            this.controls.chkRecurrence.CheckedChanged.AddHandler(ASPx.CreateDelegate(this.OnChkRecurrenceChanged, this));
            this.controls.edtStartDate.Validation.AddHandler(ASPx.CreateDelegate(this.OnEdtStartDateValidate, this));
            this.controls.edtStartDate.ValueChanged.AddHandler(ASPx.CreateDelegate(this.OnUpdateStartEndValue, this));
            this.controls.edtEndDate.Validation.AddHandler(ASPx.CreateDelegate(this.OnEdtEndDateValidate, this));
            this.controls.edtEndDate.ValueChanged.AddHandler(ASPx.CreateDelegate(this.OnUpdateStartEndValue, this));
            this.controls.ddlVisit.SelectedIndexChanged.AddHandler(ASPx.CreateDelegate(this.OnVisitSelected, this))
            this.controls.ddlVisit.EndCallback.AddHandler(ASPx.CreateDelegate(this.OnVisitSelectedCallBackEnd, this))
            this.controls.ddlPatient.SelectedIndexChanged.AddHandler(ASPx.CreateDelegate(this.OnPatientSelected, this))
            this.controls.ddlPatient.EndCallback.AddHandler(ASPx.CreateDelegate(this.OnPatientSelectedCallBackEnd, this))
            debugger;
            chkNewPatient.CheckedChanged.AddHandler(ASPx.CreateDelegate(this.OnNewPatientCheckedChanged, this))

            if (this.scheduler.GetSelectedAppointmentIds()[0]) {
                var appData = this.scheduler.GetSelectedAppointmentIds()[0].split('_');
                if (appData.length == 1) {
                    this.appIndex = 0
                } else if (appData.length == 2) {

                    this.appIndex = appData[1]
                }
                this.appointmentCopy = this.scheduler.GetAppointment(this.scheduler.GetSelectedAppointmentIds()[0]);
                if (this.appointmentCopy) {

                    this.Update(this.appointmentCopy);
                    //cddlPatient.SetEnabled(false);
                } else {



                }
            } else {
                cddlPatient.SetEnabled(false);
                this.SetDoctor();
                var item = cddlVisit.FindItemByText("IE");
                //var item = lb.GetSelectedItem();
                var text = item ? item.text : '';
                cddlVisit.SetText(text);
                cddlVisit.SetLastSuccessText((item != null) ? text : "");
                cddlVisit.SetLastSuccessValue((item != null) ? item.value : null);
                cddlVisit.SetEnabled(false);

            }
        },
        SetDoctor: function () {
            cddlDoctor.SetValue(this.scheduler.GetSelectedResource());
            var item = cddlDoctor.FindItemByValue(this.scheduler.GetSelectedResource());
            //var item = lb.GetSelectedItem();
            var text = item ? item.text : value;
            cddlDoctor.SetText(text);
            cddlDoctor.SetLastSuccessText((item != null) ? text : "");
            cddlDoctor.SetLastSuccessValue((item != null) ? item.value : null);
            document.getElementById("<%=ddlStatustr.ClientID%>").setAttribute("style", "display:none");
        },
        OnAppintmentSave: function (s, e) {

            s.AppointmentFormCancel()
            s.Refresh()
        },
        OnAppintmentDelete: function (s, e) {
            s.AppointmentFormCancel()
            // s.Refresh()
        },
        OnVisitSelected: function (s, e) {

            this.controls.chkRecurrence.SetVisible(false);
            if (cddlPatient.GetValue() != null && cddlVisit.GetValue() != null) {
                this.lastValue = cddlVisit.GetValue()
                if (cddlVisit.GetText() != "IE" && !this.appointmentCopy) {
                    this.controls.chkRecurrence.SetVisible(true);

                }
                if (cddlVisit.GetText() == "IE")
                    PageMethods.CheckVisitForVisitType(cddlVisit.GetValue() + "-" + cddlDoctor.GetValue() + "-" + cddlPatient.GetValue(), function (results) {

                        if (results == false) {
                            alert("IE Visit Already Exist For Same Speciality");
                            cddlVisit.SetValue(null);
                        } else {

                        }
                    }, function (error) {
                        if (error) {
                            alert(error.get_message());
                        }
                        else
                            alert("An unexpeceted error occurred"); // Shouldn't occur if things are coded properly
                    });




                //cddlVisit.PerformCallback(cddlVisit.GetValue() + "-" + cddlDoctor.GetValue() + "-" + cddlPatient.GetValue());
            }
        },
        OnVisitSelectedCallBackEnd: function (s, e) {

            if (s.cpIsValid == false) {
                alert("IE Visit Already Exist For Same Speciality");
            } else {
                var item = cddlVisit.FindItemByValue(this.lastValue);
                //var item = lb.GetSelectedItem();
                var text = item ? item.text : this.lastValue;
                cddlVisit.SetText(text);
                cddlVisit.SetLastSuccessText((item != null) ? text : "");
                cddlVisit.SetLastSuccessValue((item != null) ? item.value : null);
            }
        },
        OnPatientSelected: function (s, e) {

            if (cddlPatient.GetValue() != null) {
                this.lastValue = cddlPatient.GetValue();
                //cddlPatient.PerformCallback(cddlPatient.GetValue());
                //
                PageMethods.GetPatientDetail(cddlPatient.GetValue(), function (s) {

                    s = JSON.parse(s);
                    lblCaseNo.SetText(s.SZ_CASE_NO);
                    lblDOB.SetText(s.DT_DOB);
                    lblDOA.SetText(s.DT_DATE_OF_ACCIDENT);
                    lblPhone.SetText(s.SZ_PATIENT_PHONE);
                    lblAttorny.SetText(s.ATTORNEY_NAME);
                    lblAttornyNo.SetText(s.SZ_ATTORNEY_PHONE);
                }, function (error) {
                    if (error) {
                        alert(error.get_message());
                    }
                    else
                        alert("An unexpeceted error occurred"); // Shouldn't occur if things are coded properly
                });
                if (this.NullPatient != undefined && this.NullPatient == false) {
                    cddlVisit.SetEnabled(true);
                    cddlVisit.SetValue(null);
                }
                else {

                }
            }
            else {
                cddlVisit.SetEnabled(false);
            }
        },
        OnPatientSelectedCallBackEnd: function (s, e) {

            setTimeout(function () {
                lblCaseNo.SetText(s.cpSZ_CASE_NO);
                lblDOB.SetText(s.cpDT_DOB);
                lblDOA.SetText(s.cpDT_DATE_OF_ACCIDENT);
                lblPhone.SetText(s.cpSZ_PATIENT_PHONE);
                lblAttorny.SetText(s.cpATTORNEY_NAME);
                lblAttornyNo.SetText(s.cpSZ_ATTORNEY_PHONE);
            }, 100)

            var item = cddlPatient.FindItemByValue(this.lastValue);
            //var item = lb.GetSelectedItem();
            var text = item ? item.text : this.lastValue;
            cddlPatient.SetText(text);
            cddlPatient.SetLastSuccessText((item != null) ? text : "");
            cddlPatient.SetLastSuccessValue((item != null) ? item.value : null);
        },
        OnNewPatientCheckedChanged: function (s, e) {
            if (s.GetValue() == false)
                cddlPatient.SetEnabled(true);
            else
                cddlPatient.SetEnabled(false);
        },
        OnUpdateStartEndValue: function (s, e) {
            var isValid = ASPxClientEdit.ValidateEditorsInContainer(null);
            this.controls.btnOk.SetEnabled(isValid)
        },
        OnEdtStartDateValidate: function (s, e) {
            if (!e.isValid)
                return;
            var startDate = this.controls.edtStartDate.GetDate();
            var endDate = this.controls.edtEndDate.GetDate();
            e.isValid = startDate == null || endDate == null || startDate < endDate;
            e.errorText = "The Start Date must precede the End Date.";
        },
        OnEdtEndDateValidate: function (s, e) {
            if (!e.isValid)
                return;
            var startDate = this.controls.edtStartDate.GetDate();
            var endDate = this.controls.edtEndDate.GetDate();
            e.isValid = startDate == null || endDate == null || startDate < endDate;
            e.errorText = "The Start Date must precede the End Date.";
        },
        OnBtnOkClick: function (s, e) {

            var apt = this.Parse();
            var recurrenceInfo = apt.GetRecurrenceInfo();
            if (recurrenceInfo && recurrenceInfo.GetRange() == "EndByDate" && apt.GetStart() > recurrenceInfo.GetEnd())
                apt.GetRecurrenceInfo().SetEnd(apt.GetStart());

            if (apt.appointmentId)
                this.scheduler.UpdateAppointment(apt);
            else

                this.scheduler.InsertAppointment(apt);


            this.scheduler.EndCallback.AddHandler(ASPx.CreateDelegate(this.RefreshHandler, null));

        },
        RefreshHandler: function (s, e) {


            s.ClosePopupForm();
            s.Refresh();
            ASPx.Data.ArrayRemoveAt(s.EndCallback.handlerInfoList, 1);

        },
        OnBtnCancelClick: function (s, e) {

            this.scheduler.AppointmentFormCancel()
        },
        OnBtnDeleteClick: function (s, e) {

            var apt = this.Parse();


            this.scheduler.DeleteAppointment(apt);
            this.scheduler.ClosePopupForm();

        },
        OnChkRecurrenceChanged: function (s, e) {

            var isChecked = s.GetChecked();
            this.controls.recurrenceControl.SetVisible(isChecked);
        },
        Parse: function () {
            if (cddlVisit.GetValue() == null) {

                alert("Please Select Visit Type !")
                return;
            }
            var start = this.controls.edtStartDate.GetDate();
            var end = this.controls.edtEndDate.GetDate();
            //var subject = this.controls.tbSubject.GetText();
            var subject = cddlVisit.GetText() + " - " + cddlPatient.GetText();
            var description = this.controls.memDescription.GetText();
            // var location = this.controls.tbLocation.GetText();
            var location = cddlVisit.GetValue() + "-" + cddlDoctor.GetValue() + "-" + cddlPatient.GetValue();
            //var labelId = this.controls.edtLabel.GetValue();
            var statusId = cddlStatus.GetValue();
            // var allDay = this.controls.chkAllDay.GetChecked();
            var resourceId = cddlDoctor.GetValue();
            var apt = new ASPxClientAppointment();
            if (!this.controls.chkRecurrence.GetChecked()) {
                apt.SetAppointmentType(ASPxAppointmentType.Normal);
            } else {

                apt.SetAppointmentType(ASPxAppointmentType.Pattern);
            }
            apt.SetStart(start);
            apt.SetEnd(end);
            // this.scheduler.

            apt.SetSubject(subject);
            apt.SetDescription(description);
            apt.SetLocation(location);
            //apt.SetLabelId(labelId);
            apt.SetStatusId(statusId);
            //apt.SetAllDay(allDay);
            //apt.SZ_PATIENT_ID = ddlPatient.GetValue();
            // apt.SZ_DOCTOR_ID = ddlDoctor.GetValue();
            // apt.VISIT_TYPE_ID = ddlVisit.GetValue();

            apt.AddResource(resourceId);
            if (this.appointmentCopy && this.appointmentCopy.GetId())
                apt.SetId(this.appointmentCopy.GetId());

            if (this.controls.chkRecurrence.GetChecked()) {
                apt.SetAppointmentType(ASPxAppointmentType.Pattern);
                var recurrenceInfo = this.controls.recurrenceControl.Parse();
                apt.SetRecurrenceInfo(recurrenceInfo);
            }
            return apt;
        },
        Update: function (apt) {

            this.appointmentCopy = apt;
            ASPxClientEdit.ClearEditorsInContainerById("ClientAppointmentForm", null, true);
            this.controls.btnOk.SetEnabled(true);
            this.controls.edtStartDate.SetDate(apt.GetStart());
            this.controls.edtEndDate.SetDate(apt.GetEnd());
            this.controls.memDescription.SetText(apt.GetDescription());
            var now = new Date();
            if (new Date(apt.GetStart().getTime() + (apt.GetStart().getTimezoneOffset() * 60000)).getTime() > (new Date(now.getTime() + (now.getTimezoneOffset() * 60000)).getTime()) && !(apt.statusIndex == 2 || apt.statusIndex == 3)) {

                cddlStatus.listBox.RemoveItem(2);
                cddlStatus.listBox.RemoveItem(2);
            }
            //this.controls.tbSubject.SetText(apt.GetSubject());
            // this.controls.chkAllDay.SetChecked(apt.GetAllDay());
            // this.controls.edtLabel.SetValue(apt.GetLabelId());
            // this.controls.edtStatus.SetValue(apt.GetStatusId());
            //this.controls.tbLocation.SetText(apt.GetLocation());
            var a = apt.GetLocation().split("-");
            if (a[2] != 'null') {
                chkNewPatient.SetVisible(false);
                chkNewPatient.SetValue(false);
                this.NullPatient = false;
                cddlPatient.SetValue(a[2]);
                var item = cddlPatient.FindItemByValue(a[2]);
                //var item = lb.GetSelectedItem();
                var text = item ? item.text : a[2];

                cddlPatient.SetText(text);
                cddlPatient.SetLastSuccessText((item != null) ? text : "");
                cddlPatient.SetLastSuccessValue((item != null) ? item.value : null);
                this.lastValue = cddlPatient.GetValue();
                PageMethods.GetPatientDetail(cddlPatient.GetValue(), function (s) {

                    s = JSON.parse(s);
                    lblCaseNo.SetText(s.SZ_CASE_NO);
                    lblDOB.SetText(s.DT_DOB);
                    lblDOA.SetText(s.DT_DATE_OF_ACCIDENT);
                    lblPhone.SetText(s.SZ_PATIENT_PHONE);
                    lblAttorny.SetText(s.ATTORNEY_NAME);
                    lblAttornyNo.SetText(s.SZ_ATTORNEY_PHONE);
                }, function (error) {
                    if (error) {
                        alert(error.get_message());
                    }
                    else
                        alert("An unexpeceted error occurred"); // Shouldn't occur if things are coded properly
                });

                cddlPatient.SetEnabled(false);
            } else {
                this.NullPatient = true;
                cddlPatient.SetEnabled(true);
                cddlVisit.SetEnabled(false);
            }
            cddlVisit.SetValue(a[0]);
            var item = cddlVisit.FindItemByValue(a[0]);
            //var item = lb.GetSelectedItem();
            var text = item ? item.text : a[0];
            cddlVisit.SetText(text);
            cddlVisit.SetLastSuccessText((item != null) ? text : "");
            cddlVisit.SetLastSuccessValue((item != null) ? item.value : null);
            cddlDoctor.SetValue(a[1]);
            var item = cddlDoctor.FindItemByValue(a[1]);
            //var item = lb.GetSelectedItem();
            var text = item ? item.text : a[1];
            cddlDoctor.SetText(text);
            cddlDoctor.SetLastSuccessText((item != null) ? text : "");
            cddlDoctor.SetLastSuccessValue((item != null) ? item.value : null);
            // cddlPatient.PerformCallback(a[2]);
            // cddlDoctor.PerformCallback(a[1]);
            // cddlVisit.PerformCallback(a[0]);
            if (apt.statusIndex == 2 || apt.statusIndex == 3) {
                this.controls.btnDelete.SetVisible(false);
            } else {
                this.controls.btnDelete.SetVisible(true);
            }
            cddlStatus.SetValue(apt.statusIndex);
            var item = cddlStatus.FindItemByValue(apt.statusIndex);
            //var item = lb.GetSelectedItem();
            var text = item ? item.text : value;
            cddlStatus.SetText(text);
            cddlStatus.SetLastSuccessText((item != null) ? text : "");
            cddlStatus.SetLastSuccessValue((item != null) ? item.value : null);
            //cddlStatus.PerformCallback(apt.statusIndex);
            // cddlStatus.SetSelectedItem(cddlStatus.FindItemByValue(apt.statusIndex));
            //cddlStatus.SelectedIndex=cddlStatus.FindItemByValue(apt.statusIndex).index;
            // cddlStatus.FindItemByValue(apt.statusIndex).selected=true;
            // cddlPatient.SetSelectedIndex(cddlPatient.FindItemByValue(a[2]).index);
            //cddlPatient.SetText(cddlPatient.FindItemByValue(a[2]).text);
            //cddlDoctor.SetSelectedItem(cddlDoctor.FindItemByValue(a[1]));
            // cddlDoctor.SetValue(a[1]);
            //cddlDoctor.SetText(cddlDoctor.FindItemByValue(a[1]).text);
            //cddlDoctor.SetSelectedItem(cddlVisit.FindItemByValue(a[1]));


            //cddlVisit.SetText(cddlVisit.FindItemByValue(a[0]).text);

            //cddlPatient.SetValue(a[2]);

            // cddlVisit.SetValue(a[0]);
            var resourceIdValue = apt.GetResource(0);
            resourceIdValue = (resourceIdValue == null) ? "null" : resourceIdValue;
            //this.controls.edtResource.SetValue(resourceIdValue);
            this.controls.chkRecurrence.SetVisible(false);
            var appointmentType = apt.GetAppointmentType();
            //if(appointmentType) {
            //    if(appointmentType == ASPxAppointmentType.Normal || appointmentType == ASPxAppointmentType.Pattern) {
            //        this.controls.chkRecurrence.SetVisible(true);
            //        this.controls.chkRecurrence.SetChecked(true);
            //        this.controls.chkRecurrence.SetChecked(false);
            //        if(appointmentType == ASPxAppointmentType.Pattern) {
            //            this.controls.recurrenceControl.SetVisible(true);
            //            this.controls.chkRecurrence.SetVisible(true);
            //            this.controls.chkRecurrence.SetChecked(true);
            //            this.controls.recurrenceControl.Update(apt.GetStart(), apt.GetRecurrenceInfo());
            //        }
            //    }
            //    else {
            //        this.controls.chkRecurrence.SetVisible(false);
            //    }
            //}
            this.controls.btnDelete.SetEnabled((apt.GetId()) ? true : false);
        },
        Clear: function () {
            this.controls.edtStartDate.SetDate();
            this.controls.edtEndDate.SetDate();
            this.controls.tbDescription.SetText('');
            this.controls.tbSubject.SetText('');
            this.controls.chkAllDay.SetChecked(false);
            this.controls.edtLabel.SetValue()
            this.controls.edtStatus.SetValue()
            this.controls.tbLocation.SetText('');
            this.controls.chkRecurrence.SetChecked(false);
            this.controls.edtResource.SetValue();
            this.controls.recurrenceControl.Clear();
        }
    });
    // ]]> 
</script>
