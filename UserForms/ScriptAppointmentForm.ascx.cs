using System.Collections;
using System.Web.UI;
using DevExpress.Web.ASPxScheduler;
using System;
using System.Web;
using System.Data;

public partial class UserForms_ScriptAppointmentForm : ASPxSchedulerClientFormBase
{
    #region Fields
    IEnumerable labelDataSource;
    IEnumerable statusDataSource;
    IEnumerable resourceDataSource;
    public ASPxScheduler scheduler;
    #endregion

    #region Properties
    public override string ClassName { get { return "ASPxClientAppointmentForm"; } }
    ASPxScheduler Scheduler
    {
        get
        {

            if (scheduler == null)
                this.scheduler = ((AppointmentFormTemplateContainer)Parent).Control;
            return scheduler;
        }
    }
    protected IEnumerable LabelDataSource
    {
        get
        {
            if (labelDataSource == null)
            {
                this.labelDataSource = ASPxSchedulerFormDataHelper.CreateLabelDataSource(Scheduler);
            }
            return labelDataSource;
        }
    }
    protected IEnumerable StatusDataSource
    {
        get
        {
            if (statusDataSource == null)
                this.statusDataSource = ASPxSchedulerFormDataHelper.CreateStatusesDataSource(Scheduler);
            return statusDataSource;
        }
    }
    protected IEnumerable ResourceDataSource
    {
        get
        {
            if (resourceDataSource == null)
                this.resourceDataSource = ASPxSchedulerFormDataHelper.CreateResourceDataSource(Scheduler);
            return resourceDataSource;
        }
    }
    #endregion
    protected void DoctorDatasource_Init(object sender, EventArgs e)
    {
        DoctorDatasource.SelectCommand = @"Select MST_DOCTOR.SZ_DOCTOR_NAME+'('+MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP+')' as SZ_DOCTOR_NAME,MST_DOCTOR.SZ_DOCTOR_ID from MST_DOCTOR
JOIN TXN_DOCTOR_SPECIALITY ON MST_DOCTOR.SZ_DOCTOR_ID = TXN_DOCTOR_SPECIALITY.SZ_DOCTOR_ID
JOIN MST_PROCEDURE_GROUP ON MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP_ID = TXN_DOCTOR_SPECIALITY.SZ_PROCEDURE_GROUP_ID
 Where MST_DOCTOR.SZ_COMPANY_ID = '" + ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'";
    }
    protected void PatientDataSource_Init(object sender, EventArgs e)
    {
        PatientDataSource.SelectCommand = "Select      SZ_PATIENT_ID,Name FROM PatientList where SZ_COMPANY_ID='" + ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PrepareChildControls();
        //tbSubject.Focus();

        //lblSubject.Visible = lblLocation.Visible = tbSubject.Visible = tbLocation.Visible = false;

        // RegisterClientScript();

    }
    protected override Control[] GetChildControls()
    {
        Control[] controls = new Control[] { edtStartDate, edtEndDate,
                ddlVisit, ddlPatient, ddlDoctor, memDescription,
                chkRecurrence, recurrenceControl, btnOk, btnCancel, btnDelete};
        return controls;
    }
    protected void PrepareChildControls()
    {
        AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
        scheduler = container.Control;
        if (container.Appointment.Id != null)
        {
            HdnAppId.Value = container.Appointment.Id.ToString();
            HdnType.Value = container.Appointment.Type.ToString();
        }
        ddlVisit.Value = container.CustomFields[CustomFieldNames.VISIT_TYPE_ID];
        ddlDoctor.Value = container.CustomFields[CustomFieldNames.SZ_DOCTOR_ID];
        ddlPatient.Value = container.CustomFields[CustomFieldNames.SZ_PATIENT_ID];
        //AppointmentRecurrenceForm1.EditorsInfo = new EditorsInfo(control, control.Styles.FormEditors, control.Images.FormEditors, control.Styles.Buttons);
        // base.PrepareChildControls();
    }


    protected void ddlPatient_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
      
        DataSet ds = new SchedularDAO().GetPatientDetail(e.Parameter, ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlPatient.JSProperties.Add("cpSZ_CASE_NO", ds.Tables[0].Rows[0]["sz_case_no"]);
            ddlPatient.JSProperties.Add("cpPATIENT_NAME", ds.Tables[0].Rows[0]["PATIENT_NAME"]);
            ddlPatient.JSProperties.Add("cpDT_DOB", ds.Tables[0].Rows[0]["DT_DOB"]);
            ddlPatient.JSProperties.Add("cpDT_DATE_OF_ACCIDENT", ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"]);
            ddlPatient.JSProperties.Add("cpSZ_PATIENT_PHONE", ds.Tables[0].Rows[0]["SZ_PATIENT_PHONE"]);
            ddlPatient.JSProperties.Add("cpATTORNEY_NAME", ds.Tables[0].Rows[0]["ATTORNEY_NAME"]);
            ddlPatient.JSProperties.Add("cpSZ_ATTORNEY_PHONE", ds.Tables[0].Rows[0]["sz_attorney_phone"]);
           
        }
    }

    protected void ddlVisit_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)

    {
        string[] clientData = e.Parameter.Split(new char[] { '-' });
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();

        ddlVisit.JSProperties.Add("cpIsValid", !_bill_Sys_Calender.CheckVisitForVisitType(clientData[2], clientData[1], clientData[0], ((Bill_Sys_BillingCompanyObject)System.Web.HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));

    }

    protected void ddlDoctor_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ddlDoctor.Value = e.Parameter;
    }

    protected void ddlStatus_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ddlStatus.Value = e.Parameter;
    }
}
