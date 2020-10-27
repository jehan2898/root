using System;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
using System.Web;

public class CustomFieldNames {
    public const string ContactInfo = "ContactInfo";
    public const string SZ_PATIENT_ID = "SZ_PATIENT_ID";
    public const string SZ_COMPANY_ID = "SZ_COMPANY_ID";
    public const string SZ_DOCTOR_ID = "SZ_DOCTOR_ID";
    public const string VISIT_TYPE_ID = "VISIT_TYPE_ID";
}


public class MyAppointmentFormTemplateContainer : AppointmentFormTemplateContainer {
    public MyAppointmentFormTemplateContainer(ASPxScheduler control)
        : base(control) {
    }

    public string ContactInfo { 
        get {
            return Convert.ToString(Appointment.CustomFields[CustomFieldNames.ContactInfo]); 
        } 
    }
    public string SZ_PATIENT_ID
    {
        get
        {
            return Convert.ToString(Appointment.CustomFields[CustomFieldNames.SZ_PATIENT_ID]);
        }
    }
    public string SZ_COMPANY_ID
    {
        get
        {
            return Convert.ToString(Appointment.CustomFields[CustomFieldNames.SZ_COMPANY_ID]);
        }
    }
    public string SZ_DOCTOR_ID
    {
        get
        {
            return Convert.ToString(Appointment.CustomFields[CustomFieldNames.SZ_DOCTOR_ID]);
        }
    }
    public string VISIT_TYPE_ID
    {
        get
        {
            return Convert.ToString(Appointment.CustomFields[CustomFieldNames.VISIT_TYPE_ID]);
        }
    }
}
public class MyAppointmentSaveCallbackCommand : AppointmentFormSaveCallbackCommand {
    public MyAppointmentSaveCallbackCommand(ASPxScheduler control)
        : base(control) {
    }
    protected internal new MyAppointmentFormController Controller { get { return (MyAppointmentFormController)base.Controller; } }

    protected override void AssignControllerValues() {
        ASPxTextBox tbSubject = (ASPxTextBox)FindControlByID("tbSubject");
        ASPxTextBox tbLocation = (ASPxTextBox)FindControlByID("tbLocation");
        ASPxDateEdit edtStartDate = (ASPxDateEdit)FindControlByID("edtStartDate");
        ASPxDateEdit edtEndDate = (ASPxDateEdit)FindControlByID("edtEndDate");
        ASPxMemo memDescription = (ASPxMemo)FindControlByID("memDescription");
        ASPxComboBox ddlPatient = (ASPxComboBox)FindControlByID("ddlPatient");
        ASPxComboBox ddlDoctor = (ASPxComboBox)FindControlByID("ddlDoctor");
        ASPxComboBox ddlVisit = (ASPxComboBox)FindControlByID("ddlVisit");
        //ASPxDropDownEdit ddlResource = (ASPxDropDownEdit)FindControlByID("ddlResource");

        Controller.Subject = "";
        Controller.Location = "";
        Controller.Description = memDescription.Text;

        Controller.Start = edtStartDate.Date;
        Controller.End = edtEndDate.Date;
        
        #region Custom Fields Mapping
        Controller.SZ_PATIENT_ID = ddlPatient.Value.ToString();
        Controller.SZ_COMPANY_ID = HttpContext.Current.Session["Billing_Company_ID"].ToString();
        Controller.SZ_DOCTOR_ID = ddlDoctor.Value.ToString();
        Controller.VISIT_TYPE_ID = ddlVisit.Value.ToString();
        #endregion
        //Controller.ResourceId = ddlResource;


        DateTime clientStart = FromClientTime(Controller.Start);
        AssignControllerRecurrenceValues(clientStart);
    }
    protected override AppointmentFormController CreateAppointmentFormController(Appointment apt) {
        return new MyAppointmentFormController(Control, apt);
    }
}

public class MyAppointmentFormController : AppointmentFormController {
    private const string PatientIDFieldName = "SZ_PATIENT_ID";
    private const string CompanyIDFieldName = "SZ_COMPANY_ID";
    private const string CaseIDFieldName = "SZ_DOCTOR_ID";
    private const string VisitTypeFieldName = "VISIT_TYPE_ID";


    public MyAppointmentFormController(ASPxScheduler control, Appointment apt)
        : base(control, apt) {
    }
    public string SZ_PATIENT_ID { get { return (string)EditedAppointmentCopy.CustomFields[PatientIDFieldName]; } set { EditedAppointmentCopy.CustomFields[PatientIDFieldName] = value; } }
    string Source_SZ_PATIENT_ID { get { return (string)SourceAppointment.CustomFields[PatientIDFieldName]; } set { SourceAppointment.CustomFields[PatientIDFieldName] = value; } }


    #region CompanyID
    public string SZ_COMPANY_ID { get { return (string)EditedAppointmentCopy.CustomFields[CompanyIDFieldName]; } set { EditedAppointmentCopy.CustomFields[CompanyIDFieldName] = value; } }
    string Source_SZ_COMPANY_ID { get { return (string)SourceAppointment.CustomFields[CompanyIDFieldName]; } set { SourceAppointment.CustomFields[CompanyIDFieldName] = value; } }
    #endregion

    #region CaseID
    public string SZ_DOCTOR_ID { get { return (string)EditedAppointmentCopy.CustomFields[CaseIDFieldName]; } set { EditedAppointmentCopy.CustomFields[CaseIDFieldName] = value; } }
    string Source_SZ_DOCTOR_ID { get { return (string)SourceAppointment.CustomFields[CaseIDFieldName]; } set { SourceAppointment.CustomFields[CaseIDFieldName] = value; } }
    #endregion

    #region VisitTypeID
    public string VISIT_TYPE_ID { get { return (string)EditedAppointmentCopy.CustomFields[VisitTypeFieldName]; } set { EditedAppointmentCopy.CustomFields[VisitTypeFieldName] = value; } }
    string Source_SZ_VISIT_TYPE_ID { get { return (string)SourceAppointment.CustomFields[VisitTypeFieldName]; } set { SourceAppointment.CustomFields[VisitTypeFieldName] = value; } }
    #endregion

    public override bool IsAppointmentChanged() {
        if(base.IsAppointmentChanged())
            return true;
        return false;
    }
    protected override void ApplyCustomFieldsValues() {
        Source_SZ_PATIENT_ID = SZ_PATIENT_ID;
        Source_SZ_COMPANY_ID = SZ_COMPANY_ID;
        Source_SZ_DOCTOR_ID = SZ_DOCTOR_ID;
        Source_SZ_VISIT_TYPE_ID = VISIT_TYPE_ID;
    }
}
