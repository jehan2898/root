using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using Scheduling;


public partial class AJAX_Pages_ScheduleAppointment : PageBase
{
    private Patient_TVBO _patient_TVBO;
    DataSet ds = null;

    private void BindTimeControl()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            for (int i = 0; i <= 12; i++)
            {
                if (i > 9)
                {
                    this.ddlHours.Items.Add(i.ToString());
                    this.ddlEndHours.Items.Add(i.ToString());
                }
                else
                {
                    this.ddlHours.Items.Add("0" + i.ToString());
                    this.ddlEndHours.Items.Add("0" + i.ToString());
                }
            }
            for (int j = 0; j <= 60; j++)
            {
                if (j > 9)
                {
                    this.ddlMinutes.Items.Add(j.ToString());
                    this.ddlEndMinutes.Items.Add(j.ToString());
                }
                else
                {
                    this.ddlMinutes.Items.Add("0" + j.ToString());
                    this.ddlEndMinutes.Items.Add("0" + j.ToString());
                }
            }
            this.ddlTime.Items.Add("AM");
            this.ddlTime.Items.Add("PM");
            this.ddlEndTime.Items.Add("AM");
            this.ddlEndTime.Items.Add("PM");
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAddPatient_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        Bill_Sys_PatientBO tbo = new Bill_Sys_PatientBO();
        try
        {
            this.txtPatientID.Text = "";
            this.txtPatientFName.Text = "";
            this.txtMI.Text = "";
            this.txtPatientLName.Text = "";
            this.txtPatientPhone.Text = "";
            this.txtPatientAddress.Text = "";
            this.txtBirthdate.Text = "";
            this.txtPatientAge.Text = "";
            this.txtSocialSecurityNumber.Text = "";
            this.txtCaseID.Text = "";
            this.cmbState.Text = "NA";
            this.cmbCaseType.Text = "NA";
            this.cmbInsurance.Text = "NA";
            this.TextBox3.Text = "";
            this.txtPatientFName.ReadOnly = false;
            this.txtMI.ReadOnly = false;
            this.txtPatientLName.ReadOnly = false;
            this.txtPatientPhone.ReadOnly = false;
            this.txtPatientAddress.ReadOnly = false;
            this.cmbState.Enabled = true;
            this.cmbInsurance.Enabled = true;
            this.cmbCaseType.Enabled = true;
            this.cmbMedicalOffice.Enabled = true;
            this.txtCaseID.Text = "";
            this.TextBox3.ReadOnly = false;
            this.lblSSN.Visible = false;
            this.txtSocialSecurityNumber.Visible = false;
            this.lblBirthdate.Visible = false;
            this.txtBirthdate.Visible = false;
            this.lblAge.Visible = false;
            this.txtPatientAge.Visible = false;
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
            {
                this.lblChartNumber.Visible = true;
                this.txtRefChartNumber.Visible = true;
                string maxChartNumber = "";
                maxChartNumber = tbo.GetMaxChartNumber(this.txtCompanyID.Text);
                if (maxChartNumber != "")
                {
                    this.txtRefChartNumber.Text = maxChartNumber;
                }
                else
                {
                    this.txtRefChartNumber.Text = "1";
                }
            }
            else
            {
                this.lblChartNumber.Visible = false;
                this.txtRefChartNumber.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearValues();
    }

    protected void btnLoadPageData_Click(object sender, EventArgs e)
    {
    }

    protected void btnLoadPatient_Click(object sender, EventArgs e)
    {
        this.LoadPatientDetails();
    }

    protected void btnPECancel_Click(object sender, EventArgs e)
    {
    }

    protected void btnPEOk_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        calOperation operation = new calOperation();
        calPatientEO teo = new calPatientEO();
        calEvent event2 = new calEvent();
        CalendarTransaction transaction = new CalendarTransaction();
        calResult result = new calResult();
        ArrayList list = new ArrayList();
        ArrayList list2 = new ArrayList();
        string text = "";
        Billing_Sys_ManageNotesBO sbo = new Billing_Sys_ManageNotesBO();
        try
        {
            this.txtCaseID.Text = this.hdnCaseID.Value.ToString();
            if (!this.txtPatientFName.ReadOnly)
            {
                teo = this.create_calPatientEO();
                this.txtPatientID.Text = sbo.GetPatientLatestID();
                operation.add_patient = true;
            }
            if (this.hdnAppDate.Value.ToString() != "")
            {
                if (this.check_appointment() != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('The patient is already scheduled for this date.')", true);
                    return;
                }
                Convert.ToDateTime(this.hdnAppDate.Value.ToString());
                if (this.hdnAppDate.Value.ToString() != "")
                {
                    string str3 = "";
                    this.hdnAppDate.Value.ToString();
                    if (this.hdnEventID.Value.ToString() != "")
                    {
                        this.hdnEventID.Value.ToString();
                        Bill_Sys_ReferalEvent event3 = new Bill_Sys_ReferalEvent();
                        ArrayList list3 = new ArrayList();
                        list3.Add(this.cmbReferringDoctor.SelectedItem.Value.ToString());
                        list3.Add(this.txtPatientID.Text);
                        list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        str3 = event3.AddDoctor(list3);
                        for (int j = 0; j < this.grdProcedureCode.Items.Count; j++)
                        {
                            CheckBox box = (CheckBox)this.grdProcedureCode.Items[j].FindControl("chkSelect");
                            if (box.Checked)
                            {
                                text = this.grdProcedureCode.Items[j].Cells[1].Text;
                                list3 = new ArrayList();
                                list3.Add(str3);
                                list3.Add(text);
                                list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list3.Add(text);
                                event3.AddDoctorAmount(list3);
                                list3 = new ArrayList();
                                list3.Add(this.txtPatientID.Text);
                                list3.Add(str3);
                                list3.Add(this.hdnAppDate.Value.ToString());
                                list3.Add(text);
                                list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list3.Add(this.ddlType.SelectedValue);
                                event3.AddPatientProc(list3);
                            }
                        }
                    }
                    else if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        new Bill_Sys_ReferalEvent();
                        new ArrayList();
                        str3 = this.cmbReferringDoctor.SelectedItem.Value.ToString();
                        for (int k = 0; k < this.grdProcedureCode.Items.Count; k++)
                        {
                            CheckBox box2 = (CheckBox)this.grdProcedureCode.Items[k].FindControl("chkSelect");
                            if (box2.Checked)
                            {
                                text = this.grdProcedureCode.Items[k].Cells[1].Text;
                                calDoctorAmount amount = new calDoctorAmount();
                                amount.SZ_DOCTOR_ID = str3;
                                amount.SZ_PROCEDURE_ID = text;
                                amount.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                amount.SZ_TYPE_CODE_ID = text;
                                list.Add(amount);
                                operation.bt_add_doctor_amount = true;
                            }
                        }
                    }
                    else
                    {
                        str3 = this.cmbReferringDoctor.SelectedItem.Value.ToString();
                    }
                    string str4 = this.hdnAppDate.Value.ToString();
                    new Bill_Sys_Calender();
                    new ArrayList();
                    event2.SZ_PATIENT_ID = this.txtPatientID.Text;
                    event2.DT_EVENT_DATE = str4;
                    event2.DT_EVENT_TIME = this.ddlHours.SelectedValue.ToString() + "." + this.ddlMinutes.SelectedValue.ToString();
                    event2.SZ_EVENT_NOTES = this.txtNotes.Text;
                    event2.SZ_DOCTOR_ID = str3;
                    text = "";
                    int num3 = 0;
                    while (num3 < this.grdProcedureCode.Items.Count)
                    {
                        text = this.grdProcedureCode.Items[num3].Cells[1].Text;
                        break;
                    }
                    if (text != null)
                    {
                        event2.SZ_TYPE_CODE_ID = text;
                    }
                    else
                    {
                        event2.DT_EVENT_DATE = str4;
                    }
                    event2.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    event2.DT_EVENT_TIME_TYPE = this.ddlTime.SelectedValue;
                    event2.DT_EVENT_END_TIME = this.ddlEndHours.SelectedValue.ToString() + "." + this.ddlEndMinutes.SelectedValue.ToString();
                    event2.DT_EVENT_END_TIME_TYPE = this.ddlEndTime.SelectedValue;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        event2.SZ_REFERENCE_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                        event2.SZ_REFERENCE_ID = this.reffCompanyID.Value.ToString();
                    }
                    event2.BT_STATUS = "0";
                    if (this.chkTransportation.Checked)
                    {
                        event2.BT_TRANSPORTATION = "1";
                    }
                    else
                    {
                        event2.BT_TRANSPORTATION = "0";
                    }
                    event2.DT_EVENT_DATE = str4;
                    if (this.chkTransportation.Checked)
                    {
                        event2.I_TRANSPORTATION_COMPANY = this.cmbTransport.Value.ToString();
                    }
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        event2.SZ_OFFICE_ID = this.cmbMedicalOffice.SelectedItem.Value.ToString();
                    }
                    for (int i = 0; i < this.grdProcedureCode.Items.Count; i++)
                    {
                        CheckBox box3 = (CheckBox)this.grdProcedureCode.Items[i].FindControl("chkSelect");
                        if (box3.Checked)
                        {
                            text = this.grdProcedureCode.Items[i].Cells[1].Text;
                            calProcedureCodeEO eeo = new calProcedureCodeEO();
                            eeo.SZ_PROC_CODE = text;
                            eeo.I_EVENT_ID = "";
                            eeo.I_STATUS = "0";
                            list2.Add(eeo);
                        }
                    }
                    result = transaction.fnc_SaveAppointment(operation, teo, list, event2, list2, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    if (result.msg_code == "SUCCESS")
                    {
                        DAO_NOTES_EO dao_notes_eo = new DAO_NOTES_EO();
                        dao_notes_eo.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                        dao_notes_eo.SZ_ACTIVITY_DESC = "Date : " + str4;
                        DAO_NOTES_BO dao_notes_bo = new DAO_NOTES_BO();
                        dao_notes_eo.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        dao_notes_eo.SZ_CASE_ID = this.txtCaseID.Text;
                        dao_notes_eo.SZ_COMPANY_ID = this.txtCompanyID.Text;
                        dao_notes_bo.SaveActivityNotes(dao_notes_eo);
                    }
                }
                if (this.hdnEventID.Value.ToString() != "")
                {
                    this.Session["PopUp"] = null;
                }
                else
                {
                    this.Session["PopUp"] = "True";
                }
            }
            if (result.msg_code == "SUCCESS")
            {
                string str5 = "Appointment.aspx?appointmentDate=" + this.hdnAppDate.Value.ToString() + "&interval=" + this.Session["INTERVAL"].ToString() + "&reffFacility=" + this.reffCompanyID.Value.ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "alert('Appointment saved successfully.');window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='" + str5 + "';", true);
                this.hdnReturnOpration.Value = "refresh";
                this.hdnReturnPath.Value = str5;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('" + result.msg + "')", true);
            }
            this.ClearValues();
            this.txtPatientExistMsg.Value = "";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        OutSchedulePatientDAO tdao = new OutSchedulePatientDAO();
        string str = "";
        string caseIdForDocumentPath = "";
        calOperation operation = new calOperation();
        calPatientEO teo = new calPatientEO();
        calEvent event2 = new calEvent();
        CalendarTransaction transaction = new CalendarTransaction();
        calResult result = new calResult();
        ArrayList list = new ArrayList();
        ArrayList list2 = new ArrayList();
        string text = "";
        Billing_Sys_ManageNotesBO sbo = new Billing_Sys_ManageNotesBO();
        try
        {
            if (this.txtPatientFName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Patient First Name should not be Empty.')", true);
            }
            else if (this.txtPatientLName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Patient Last name should not be Empty.')", true);
            }
            else if (this.cmbReferringFacility.Visible && (Convert.ToString(this.cmbReferringFacility.SelectedItem.Value) == "NA"))
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Please Select any one Reference Facility.')", true);
            }
            else if (Convert.ToString(this.cmbReferringDoctor.SelectedItem.Value) == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Please Select any one Referring Doctor.')", true);
            }
            else if (this.chkTransportation.Checked && (Convert.ToString(this.cmbTransport.SelectedItem.Value) == "NA"))
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('please select transport company.')", true);
            }
            else
            {
                if (!this.txtPatientFName.ReadOnly)
                {
                    ArrayList list3 = new ArrayList();
                    list3.Add(this.txtPatientFName.Text);
                    list3.Add(this.txtPatientLName.Text);
                    list3.Add(null);
                    list3.Add(this.cmbCaseType.SelectedItem.Value.ToString());
                    if (this.txtBirthdate.Text != "")
                    {
                        list3.Add(this.txtBirthdate.Text);
                    }
                    else
                    {
                        list3.Add(null);
                    }
                    list3.Add(this.txtCompanyID.Text);
                    list3.Add("existpatient");
                    string str4 = new Bill_Sys_PatientBO().CheckPatientExists(list3);
                    if ((str4 != "") && (this.txtPatientExistMsg.Value == ""))
                    {
                        this.msgPatientExists.InnerHtml = str4;
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "javascript:openExistsPage();", true);
                        return;
                    }
                    teo = this.create_calPatientEO();
                    this.txtPatientID.Text = sbo.GetPatientLatestID();
                    operation.add_patient = true;
                }
                if (this.hdnAppDate.Value.ToString() != "")
                {
                    string str5 = this.check_appointment();
                    if (str5 != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('" + str5 + "')", true);
                        return;
                    }
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (this.check_appointment_for_period() != ""))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('The patient is already scheduled for this date and time period.')", true);
                        return;
                    }
                    Convert.ToDateTime(this.hdnAppDate.Value.ToString());
                    if (this.hdnAppDate.Value.ToString() != "")
                    {
                        string str7 = "";
                        string str8 = this.hdnAppDate.Value.ToString();
                        if (this.hdnEventID.Value.ToString() != "")
                        {
                            this.hdnEventID.Value.ToString();
                            Bill_Sys_ReferalEvent event3 = new Bill_Sys_ReferalEvent();
                            ArrayList list4 = new ArrayList();
                            list4.Add(this.cmbReferringDoctor.SelectedItem.Value.ToString());
                            list4.Add(this.txtPatientID.Text);
                            list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            str7 = event3.AddDoctor(list4);
                            for (int j = 0; j < this.grdProcedureCode.Items.Count; j++)
                            {
                                CheckBox box = (CheckBox)this.grdProcedureCode.Items[j].FindControl("chkSelect");
                                if (box.Checked)
                                {
                                    text = this.grdProcedureCode.Items[j].Cells[1].Text;
                                    list4 = new ArrayList();
                                    list4.Add(str7);
                                    list4.Add(text);
                                    list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    list4.Add(text);
                                    event3.AddDoctorAmount(list4);
                                    list4 = new ArrayList();
                                    list4.Add(this.txtPatientID.Text);
                                    list4.Add(str7);
                                    list4.Add(str8);
                                    list4.Add(text);
                                    list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    list4.Add(this.ddlType.SelectedValue);
                                    event3.AddPatientProc(list4);
                                }
                            }
                        }
                        else if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            new Bill_Sys_ReferalEvent();
                            new ArrayList();
                            str7 = this.cmbReferringDoctor.SelectedItem.Value.ToString();
                            for (int k = 0; k < this.grdProcedureCode.Items.Count; k++)
                            {
                                CheckBox box2 = (CheckBox)this.grdProcedureCode.Items[k].FindControl("chkSelect");
                                if (box2.Checked)
                                {
                                    text = this.grdProcedureCode.Items[k].Cells[1].Text;
                                    calDoctorAmount amount = new calDoctorAmount();
                                    amount.SZ_DOCTOR_ID = str7;
                                    amount.SZ_PROCEDURE_ID = text;
                                    amount.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                    amount.SZ_TYPE_CODE_ID = text;
                                    list.Add(amount);
                                    operation.bt_add_doctor_amount = true;
                                }
                            }
                        }
                        else
                        {
                            str7 = this.cmbReferringDoctor.SelectedItem.Value.ToString();
                        }
                        string str9 = this.hdnAppDate.Value.ToString();
                        new Bill_Sys_Calender();
                        new ArrayList();
                        event2.SZ_PATIENT_ID = this.txtPatientID.Text;
                        event2.DT_EVENT_DATE = str9;
                        event2.DT_EVENT_TIME = this.ddlHours.SelectedValue.ToString() + "." + this.ddlMinutes.SelectedValue.ToString();
                        event2.SZ_EVENT_NOTES = this.txtNotes.Text;
                        event2.SZ_DOCTOR_ID = str7;
                        text = "";
                        int num3 = 0;
                        while (num3 < this.grdProcedureCode.Items.Count)
                        {
                            text = this.grdProcedureCode.Items[num3].Cells[1].Text;
                            break;
                        }
                        if (text != null)
                        {
                            event2.SZ_TYPE_CODE_ID = text;
                        }
                        else
                        {
                            event2.DT_EVENT_DATE = str9;
                        }
                        event2.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        event2.DT_EVENT_TIME_TYPE = this.ddlTime.SelectedValue;
                        event2.DT_EVENT_END_TIME = this.ddlEndHours.SelectedValue.ToString() + "." + this.ddlEndMinutes.SelectedValue.ToString();
                        event2.DT_EVENT_END_TIME_TYPE = this.ddlEndTime.SelectedValue;
                        event2.SZ_REFERENCE_ID = this.cmbReferringFacility.SelectedItem.Value.ToString();
                        event2.BT_STATUS = "0";
                        if (this.chkTransportation.Checked)
                        {
                            event2.BT_TRANSPORTATION = "1";
                        }
                        else
                        {
                            event2.BT_TRANSPORTATION = "0";
                        }
                        event2.DT_EVENT_DATE = str9;
                        if (this.chkTransportation.Checked)
                        {
                            event2.I_TRANSPORTATION_COMPANY = this.cmbTransport.SelectedItem.Value.ToString();
                        }
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            event2.SZ_OFFICE_ID = this.cmbMedicalOffice.SelectedItem.Value.ToString();
                        }
                        for (int i = 0; i < this.grdProcedureCode.Items.Count; i++)
                        {
                            CheckBox box3 = (CheckBox)this.grdProcedureCode.Items[i].FindControl("chkSelect");
                            if (box3.Checked)
                            {
                                text = this.grdProcedureCode.Items[i].Cells[1].Text;
                                calProcedureCodeEO eeo = new calProcedureCodeEO();
                                eeo.SZ_PROC_CODE = text;
                                eeo.I_EVENT_ID = "";
                                eeo.I_STATUS = "0";
                                list2.Add(eeo);
                            }
                        }
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_COPY_PATIENT_TO_TEST_FACILITY.ToString() == "1")
                        {
                            if (!this.txtPatientFName.ReadOnly)
                            {
                                ArrayList list5 = new ArrayList();
                                list5.Add(this.txtPatientFName.Text);
                                list5.Add(this.txtPatientLName.Text);
                                list5.Add(null);
                                list5.Add(this.cmbCaseType.SelectedItem.Value.ToString());
                                if (this.txtBirthdate.Text != "")
                                {
                                    list5.Add(this.txtBirthdate.Text);
                                }
                                else
                                {
                                    list5.Add(null);
                                }
                                list5.Add(this.reffCompanyID.Value.ToString());
                                list5.Add("existpatient");
                                string str11 = new Bill_Sys_PatientBO().CheckPatientExists(list5);
                                if ((str11 != "") && (this.txtPatientExistMsg.Value == ""))
                                {
                                    this.msgPatientExists.InnerHtml = str11;
                                    ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "javascript:openExistsPage();", true);
                                    return;
                                }
                                tdao = this.create_outPatientDAO();
                                this.txtPatientID.Text = sbo.GetPatientLatestID();
                                tdao.addPatient = true;
                            }
                            string str12 = this.roomID.Value.ToString();
                            OutSchedulePatient patient = new OutSchedulePatient();
                            if (this.txtPatientFName.ReadOnly && (event2.SZ_PATIENT_ID != null))
                            {
                                caseIdForDocumentPath = patient.GetCaseIdForDocumentPath(event2.SZ_PATIENT_ID);
                            }
                            result = patient.AddVisit(operation, tdao, list, event2, list2, this.txtUserId.Text, str12, this.reffCompanyID.Value.ToString(), this.txtCompanyID.Text, this.cmbReferringDoctor.SelectedItem.Value.ToString());
                            if (result.msg_code == "SUCCESS")
                            {
                                str = patient.GetCaseIdForDocumentPath(result.sz_patient_id);
                            }
                            if (caseIdForDocumentPath != "")
                            {
                                string str13 = new Bill_Sys_NF3_Template().getPhysicalPath();
                                DataSet nodeIdForCopyDocument = patient.GetNodeIdForCopyDocument(this.reffCompanyID.Value.ToString());
                                if (((nodeIdForCopyDocument != null) && (nodeIdForCopyDocument.Tables.Count > 0)) && (nodeIdForCopyDocument.Tables[0].Rows.Count > 0))
                                {
                                    for (int m = 0; m < nodeIdForCopyDocument.Tables[0].Rows.Count; m++)
                                    {
                                        string str14 = nodeIdForCopyDocument.Tables[0].Rows[m]["SZ_NODE_TYPE"].ToString();
                                        string str15 = patient.GetSourcePath(this.txtCompanyID.Text, str14, caseIdForDocumentPath);
                                        string destPath = patient.GetDestPath(this.reffCompanyID.Value.ToString(), str);
                                        if (str15 != "")
                                        {
                                            string sourceDirName = str13 + str15;
                                            string destDirName = str13 + destPath + "/" + str15;
                                            DirectoryCopy(sourceDirName, destDirName);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            result = transaction.fnc_SaveAppointment(operation, teo, list, event2, list2, this.txtUserId.Text);
                        }
                        if (result.msg_code == "SUCCESS")
                        {
                            DAO_NOTES_EO dao_notes_eo = new DAO_NOTES_EO();
                            dao_notes_eo.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                            dao_notes_eo.SZ_ACTIVITY_DESC = "Date : " + str9;
                            dao_notes_eo.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            dao_notes_eo.SZ_CASE_ID = this.txtCaseID.Text;
                            dao_notes_eo.SZ_COMPANY_ID = this.txtCompanyID.Text;
                            new DAO_NOTES_BO().SaveActivityNotes(dao_notes_eo);
                        }
                    }
                    if (this.hdnEventID.Value.ToString() != "")
                    {
                        this.Session["PopUp"] = null;
                    }
                    else
                    {
                        this.Session["PopUp"] = "True";
                    }
                }
                if (result.msg_code == "SUCCESS")
                {
                    string str19 = "Appointment.aspx?appointmentDate=" + this.hdnAppDate.Value.ToString() + "&interval=" + this.Session["INTERVAL"].ToString() + "&reffFacility=" + this.reffCompanyID.Value.ToString();
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "alert('Appointment saved successfully.');window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='" + str19 + "';", true);
                    this.hdnReturnOpration.Value = "refresh";
                    this.hdnReturnPath.Value = str19;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('" + result.msg + "')", true);
                }
                this.ClearValues();
                this.txtPatientExistMsg.Value = "";
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string check_appointment()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        Bill_Sys_PatientBO tbo = new Bill_Sys_PatientBO();
        string str = "";
        try
        {
            if (!(this.roomID.Value.ToString() != ""))
            {
                return str;
            }
            string text = "";
            for (int i = 0; i < this.grdProcedureCode.Items.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdProcedureCode.Items[i].FindControl("chkSelect");
                if (box.Checked)
                {
                    if (text == "")
                    {
                        text = this.grdProcedureCode.Items[i].Cells[1].Text;
                    }
                    else
                    {
                        text = text + "," + this.grdProcedureCode.Items[i].Cells[1].Text;
                    }
                }
            }
            string str3 = this.hdnAppDate.Value.ToString();
            ArrayList list = new ArrayList();
            list.Add(this.txtCaseID.Text);
            list.Add(this.txtCompanyID.Text);
            list.Add(this.roomID.Value.ToString());
            list.Add(str3);
            list.Add(text);
            list.Add(this.txtPatientID.Text);
            str = tbo.Check_Appointment(list);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        return str;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string check_appointment_for_period()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        Bill_Sys_PatientBO tbo = new Bill_Sys_PatientBO();
        string str = "";
        try
        {
            if (this.roomID.Value.ToString() != "")
            {
                string str2 = this.hdnAppDate.Value.ToString();
                ArrayList list = new ArrayList();
                list.Add(this.txtCaseID.Text);
                list.Add(this.txtCompanyID.Text);
                list.Add(this.roomID.Value.ToString());
                list.Add(str2);
                list.Add(this.ddlHours.SelectedValue + "." + this.ddlMinutes.SelectedValue);
                list.Add(this.ddlEndHours.SelectedValue + "." + this.ddlEndMinutes.SelectedValue);
                list.Add(this.ddlTime.SelectedValue);
                str = tbo.Check_Appointment_For_Period(list);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        return str;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void chkTransportation_CheckedChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            if (this.chkTransportation.Checked)
            {
                OutSchedule schedule = new OutSchedule();
                this.cmbTransport.TextField = "DESCRIPTION";
                this.cmbTransport.ValueField = "CODE";
                if (Convert.ToString(this.cmbReferringFacility.SelectedItem.Value) != "NA")
                {
                    this.cmbTransport.DataSource = schedule.GetTransport(this.cmbReferringFacility.SelectedItem.Value.ToString());
                }
                else
                {
                    this.cmbTransport.DataSource = schedule.GetTransport(this.txtCompanyID.Text);
                }
                this.cmbTransport.DataBind();
                this.cmbTransport.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                this.cmbTransport.Visible = true;
            }
            else
            {
                bool flag1 = this.chkTransportation.Checked;
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void ClearValues()
    {
        this.txtPatientID.Text = "";
        this.txtPatientFName.Text = "";
        this.txtMI.Text = "";
        this.txtPatientLName.Text = "";
        this.txtPatientPhone.Text = "";
        this.txtPatientAddress.Text = "";
        this.txtState.Text = "";
        this.txtBirthdate.Text = "";
        this.txtPatientAge.Text = "";
        this.txtSocialSecurityNumber.Text = "";
        this.txtCaseID.Text = "";
        this.Session["SZ_CASE_ID"] = "";
        this.cmbCaseType.Text = "NA";
        this.cmbInsurance.Text = "NA";
        this.txtNotes.Text = "";
        this.ddlTestNames.Items.Clear();
        this.ddlEndHours.Items.Clear();
        this.ddlEndMinutes.Items.Clear();
        this.ddlEndTime.Items.Clear();
        this.ddlHours.Items.Clear();
        this.ddlMinutes.Items.Clear();
        this.ddlTime.Items.Clear();
        this.grdPatientList_.DataSource = null;
        this.grdPatientList_.DataBind();
        this.lblMsg.Text = "";
        this.txtPatientFirstName.Text = "";
        this.txtPatientLastName.Text = "";
        this.txtRefChartNumber.Text = "";
    }

    protected void cmbMedicalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            OutSchedule schedule = new OutSchedule();
            this.cmbReferringDoctor.Items.Clear();
            this.cmbReferringDoctor.DataSource = schedule.GetReferringDoctorByOffice(this.cmbMedicalOffice.SelectedItem.Value.ToString());
            this.cmbReferringDoctor.TextField = "DESCRIPTION";
            this.cmbReferringDoctor.ValueField = "CODE";
            this.cmbReferringDoctor.DataBind();
            this.cmbReferringDoctor.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
            this.cmbReferringDoctor.Value = "NA";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void cmbReferringDoctor_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        this.cmbReferringDoctor.TextField = "DESCRIPTION";
        this.cmbReferringDoctor.ValueField = "CODE";
        this.cmbReferringDoctor.DataSource = new OutSchedule().GetReferringDoctorByOffice(e.Parameter);
        this.cmbReferringDoctor.DataBind();
        this.cmbReferringDoctor.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
        this.cmbReferringDoctor.Value = "NA";
    }

    protected void cmbTransport_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            if (this.chkTransportation.Checked)
            {
                OutSchedule schedule = new OutSchedule();
                this.cmbTransport.TextField = "DESCRIPTION";
                this.cmbTransport.ValueField = "CODE";
                if (Convert.ToString(this.cmbReferringFacility.SelectedItem.Value) != "NA")
                {
                    this.cmbTransport.DataSource = schedule.GetTransport(this.cmbReferringFacility.SelectedItem.Value.ToString());
                }
                else
                {
                    this.cmbTransport.DataSource = schedule.GetTransport(this.txtCompanyID.Text);
                }
                this.cmbTransport.DataBind();
                this.cmbTransport.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                this.cmbTransport.Visible = true;
            }
            else
            {
                bool flag1 = this.chkTransportation.Checked;
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private calPatientEO create_calPatientEO()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        new Bill_Sys_PatientBO();
        calPatientEO teo = new calPatientEO();
        Bill_Sys_Calender calender = new Bill_Sys_Calender();
        try
        {
            this.cmbCaseStatus.Value = calender.GetOpenCaseStatus(this.txtCompanyID.Text);
            new ArrayList();
            teo.SZ_PATIENT_FIRST_NAME = this.txtPatientFName.Text;
            teo.SZ_PATIENT_LAST_NAME = this.txtPatientLName.Text;
            teo.SZ_CASE_TYPE_ID = this.cmbCaseType.SelectedItem.Value.ToString();
            teo.SZ_PATIENT_ADDRESS = this.txtPatientAddress.Text;
            teo.SZ_PATIENT_CITY = this.TextBox3.Text;
            teo.SZ_PATIENT_PHONE = this.txtPatientPhone.Text;
            teo.SZ_PATIENT_STATE_ID = this.cmbState.SelectedItem.Value.ToString();
            teo.SZ_COMPANY_ID = this.txtCompanyID.Text;
            teo.MI = this.txtMI.Text;
            teo.SZ_CASE_STATUS_ID = this.cmbCaseStatus.SelectedItem.Value.ToString();
            teo.SZ_INSURANCE_ID = this.cmbInsurance.SelectedItem.Value.ToString();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        return teo;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private OutSchedulePatientDAO create_outPatientDAO()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        new Bill_Sys_PatientBO();
        OutSchedulePatientDAO tdao = new OutSchedulePatientDAO();
        Bill_Sys_Calender calender = new Bill_Sys_Calender();
        try
        {
            this.cmbCaseStatus.Value = calender.GetOpenCaseStatus(this.reffCompanyID.Value.ToString());
            new ArrayList();
            tdao.sPatientFirstName = this.txtPatientFName.Text;
            tdao.sPatientLastName = this.txtPatientLName.Text;
            tdao.sCaseTypeID = this.cmbCaseType.SelectedItem.Value.ToString();
            tdao.sPatientAddress = this.txtPatientAddress.Text;
            tdao.sPatientCity = this.TextBox3.Text;
            tdao.sPatientPhone = this.txtPatientPhone.Text;
            tdao.sPatientState = this.cmbState.SelectedItem.Value.ToString();
            tdao.sSourceCompanyID = this.txtCompanyID.Text;
            tdao.sPatientMI = this.txtMI.Text;
            tdao.sCaseStatusID = this.cmbCaseStatus.SelectedItem.Value.ToString();
            tdao.sInsuranceID = this.cmbInsurance.SelectedItem.Value.ToString();
            tdao.sDestinationCompanyID = this.reffCompanyID.Value.ToString();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        return tdao;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void DeleteEvent()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        this.lblMsg.Text = "";
        this.lblMsg.Visible = false;
        try
        {
            string text1 = this.hdnEventID.Value;
            if ((this.hdnEventID.Value != "") && (this.hdnAppDate.Value != ""))
            {
                int num = new Bill_Sys_Calender().Delete_Event(Convert.ToInt32(this.hdnEventID.Value.ToString()), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                string str = this.hdnAppDate.Value;
                if (num > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.btnDeleteEvent, typeof(Button), "Msg", "alert('Appointment Deleted Successfully.')", true);
                    this.lblMsg.Text = Convert.ToString("schedule.appointment.delete.success");
                    this.lblMsg.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.btnDeleteEvent, typeof(Button), "Msg", "alert('Can not delete appointment.')", true);
                    this.lblMsg.Text = Convert.ToString("schedule.appointment.delete.failed");
                    this.lblMsg.Visible = true;
                }
                bool flag1 = str != "";
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            this.hdnEventID.Value = "";
            this.hdnAppDate.Value = "";
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private static void DirectoryCopy(string sourceDirName, string destDirName)
    {
        DirectoryInfo info = new DirectoryInfo(sourceDirName);
        if (info.Exists)
        {
            DirectoryInfo[] directories = info.GetDirectories();
            if (!info.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
            foreach (FileInfo info2 in info.GetFiles())
            {
                string path = Path.Combine(destDirName, info2.Name);
                if (!File.Exists(path))
                {
                    info2.CopyTo(path, false);
                }
            }
            foreach (DirectoryInfo info3 in directories)
            {
                string str2 = Path.Combine(destDirName, info3.Name);
                DirectoryCopy(info3.FullName, str2);
            }
        }
    }

    private void DisplayControlForAddVisit()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            this.txtRefChartNumber.ReadOnly = true;
            this.txtPatientFName.ReadOnly = true;
            this.txtMI.ReadOnly = true;
            this.txtPatientLName.ReadOnly = true;
            this.txtPatientPhone.ReadOnly = true;
            this.txtPatientAddress.ReadOnly = true;
            this.TextBox3.ReadOnly = true;
            this.txtState.ReadOnly = true;
            this.txtBirthdate.ReadOnly = true;
            this.txtPatientAge.ReadOnly = true;
            this.txtSocialSecurityNumber.ReadOnly = true;
            this.cmbInsurance.Enabled = false;
            this.cmbCaseType.Enabled = false;
            this.cmbMedicalOffice.Enabled = true;
            this.cmbState.Enabled = false;
            this.lblSSN.Visible = false;
            this.txtSocialSecurityNumber.Visible = false;
            this.lblBirthdate.Visible = false;
            this.txtBirthdate.Visible = false;
            this.lblAge.Visible = false;
            this.txtPatientAge.Visible = false;
            this.lblChartNumber.Visible = false;
            this.txtRefChartNumber.Visible = false;
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.Trim().ToLower().ToString() == "referring office"))
            {
                new Bill_Sys_BillingCompanyDetails_BO();
                string str = this.reffOfficeID.Value.ToString();
                if (str != "")
                {
                    this.cmbMedicalOffice.Value = str;
                    this.cmbMedicalOffice.Enabled = false;
                    OutSchedule schedule = new OutSchedule();
                    this.cmbReferringDoctor.TextField = "DESCRIPTION";
                    this.cmbReferringDoctor.ValueField = "CODE";
                    this.cmbReferringDoctor.DataSource = schedule.GetReferringDoctorForOffice(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.cmbReferringDoctor.DataBind();
                    this.cmbReferringDoctor.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                    this.cmbReferringDoctor.Value = "NA";
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void DisplayProcedureGridColumns(bool value)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            this.grdProcedureCode.Columns[3].Visible = value;
            this.grdProcedureCode.Columns[4].Visible = value;
            this.grdProcedureCode.Columns[5].Visible = value;
            this.grdProcedureCode.Columns[6].Visible = value;
            this.grdProcedureCode.Columns[9].Visible = value;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string GETAppointPatientDetail(int i_schedule_id)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            new DataTable();
            this.ds = new DataSet();
            this._patient_TVBO = new Patient_TVBO();
            this.ds = this._patient_TVBO.GetAppointPatientDetails(i_schedule_id);
            if (this.ds != null)
            {
                if (this.ds.Tables[0] != null)
                {
                    if (this.ds.Tables[0].Rows.Count > 0)
                    {
                        this.ds.Tables[0].Clone();
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;")
                        {
                            this.txtPatientID.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;")
                        {
                            this.txtPatientFName.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;")
                        {
                            this.txtMI.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;")
                        {
                            this.txtPatientLName.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;")
                        {
                            this.txtPatientPhone.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;")
                        {
                            this.txtPatientAddress.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString() != "&nbsp;")
                        {
                            this.cmbState.Value = this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();
                        }
                        else
                        {
                            this.cmbState.Text = "NA";
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x11).ToString() != "&nbsp;")
                        {
                            this.txtBirthdate.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x11).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;")
                        {
                            this.txtPatientAge.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x10).ToString() != "&nbsp;")
                        {
                            this.txtSocialSecurityNumber.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x10).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "&nbsp;")
                        {
                            if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() == "True")
                            {
                                this.chkTransportation.Checked = true;
                            }
                            else
                            {
                                this.chkTransportation.Checked = false;
                            }
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x22).ToString() != "&nbsp;")
                        {
                            this.txtCaseID.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x22).ToString();
                            this.hdnCaseID.Value = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x22).ToString();
                        }
                        OutSchedule schedule = new OutSchedule();
                        this.cmbInsurance.TextField = "DESCRIPTION";
                        this.cmbInsurance.ValueField = "CODE";
                        this.cmbInsurance.DataSource = schedule.GetInsuranceCompany(this.ds.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());
                        this.cmbInsurance.DataBind();
                        this.cmbInsurance.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                        this.cmbInsurance.Value = "NA";
                        this.cmbCaseType.TextField = "DESCRIPTION";
                        this.cmbCaseType.ValueField = "CODE";
                        this.cmbCaseType.DataSource = schedule.GetCaseType(this.ds.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());
                        this.cmbCaseType.DataBind();
                        this.cmbCaseType.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                        this.cmbCaseType.Value = "NA";
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x21).ToString() != "&nbsp;")
                        {
                            this.cmbCaseType.Value = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x21).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x20).ToString() != "&nbsp;")
                        {
                            this.cmbInsurance.Value = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x20).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x23).ToString() != "&nbsp;")
                        {
                            this.cmbReferringDoctor.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x23).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x24).ToString() != "&nbsp;")
                        {
                            this.ddlType.SelectedValue = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x24).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x25).ToString() != "&nbsp;")
                        {
                            string str = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x25).ToString();
                            this.ddlHours.SelectedValue = str.Substring(0, str.IndexOf(".")).PadLeft(2, '0');
                            this.ddlMinutes.SelectedValue = str.Substring(str.IndexOf(".") + 1, str.Length - (str.IndexOf(".") + 1)).PadLeft(2, '0');
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x26).ToString() != "&nbsp;")
                        {
                            string str2 = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x26).ToString();
                            this.ddlEndHours.SelectedValue = str2.Substring(0, str2.IndexOf(".")).PadLeft(2, '0');
                            this.ddlEndMinutes.SelectedValue = str2.Substring(str2.IndexOf(".") + 1, str2.Length - (str2.IndexOf(".") + 1)).PadLeft(2, '0');
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x27).ToString() != "&nbsp;")
                        {
                            this.ddlTime.SelectedValue = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x27).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(40).ToString() != "&nbsp;")
                        {
                            this.ddlEndTime.SelectedValue = this.ds.Tables[0].Rows[0].ItemArray.GetValue(40).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x2a).ToString() != "&nbsp;")
                        {
                            this.txtNotes.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x2a).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x2c).ToString() != "&nbsp;")
                        {
                            this.txtPatientCompany.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x2c).ToString();
                        }
                        if ((this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x2b).ToString() != "&nbsp;") && (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x2b).ToString() == "True"))
                        {
                            this.cmbReferringDoctor.Enabled = false;
                            this.ddlType.Enabled = false;
                            this.txtNotes.ReadOnly = true;
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x2d).ToString() != "")
                        {
                            this.cmbMedicalOffice.Value = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x2d).ToString();
                        }
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            this.cmbReferringDoctor.TextField = "DESCRIPTION";
                            this.cmbReferringDoctor.ValueField = "CODE";
                            this.cmbReferringDoctor.DataSource = schedule.GetReferringDoctorByOffice(this.cmbMedicalOffice.SelectedItem.Value.ToString());
                            this.cmbReferringDoctor.DataBind();
                            this.cmbReferringDoctor.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                            this.cmbReferringDoctor.Value = "NA";
                            if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x29).ToString() != "&nbsp;")
                            {
                                this.cmbReferringDoctor.Value = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x29).ToString();
                            }
                            if (this.ds.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString() != "&nbsp;")
                            {
                                this.txtRefChartNumber.Text = this.ds.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString();
                            }
                        }
                        else
                        {
                            if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x33).ToString() != "&nbsp;")
                            {
                                this.cmbReferringDoctor.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x33).ToString();
                            }
                            this.txtRefChartNumber.Text = "";
                        }
                        if (this.ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString() != "&nbsp;")
                        {
                            if ((Convert.ToInt32(this.ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString()) > 0) && this.chkTransportation.Checked)
                            {
                                this.cmbTransport.TextField = "DESCRIPTION";
                                this.cmbTransport.ValueField = "CODE";
                                if (Convert.ToString(this.cmbReferringFacility.SelectedItem.Value) != "NA")
                                {
                                    this.cmbTransport.DataSource = schedule.GetTransport(this.cmbReferringFacility.SelectedItem.Value.ToString());
                                }
                                else
                                {
                                    this.cmbTransport.DataSource = schedule.GetTransport(this.txtCompanyID.Text);
                                }
                                this.cmbTransport.DataBind();
                                this.cmbTransport.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                                this.cmbTransport.Value = "NA";
                                this.cmbTransport.Value = this.ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString();
                                this.cmbTransport.Visible = true;
                            }
                            else if ((Convert.ToInt32(this.ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString()) == 0) && this.chkTransportation.Checked)
                            {
                                this.cmbTransport.Text = "NA";
                                this.cmbTransport.Visible = true;
                            }
                        }
                        else
                        {
                            this.cmbTransport.Text = "NA";
                        }
                        this.ds = new DataSet();
                        this._patient_TVBO = new Patient_TVBO();
                        this.ds = this._patient_TVBO.GetAppointProcCode(i_schedule_id);
                        foreach (DataRow row in this.ds.Tables[0].Rows)
                        {
                            foreach (DataGridItem item in this.grdProcedureCode.Items)
                            {
                                if (item.Cells[1].Text == row.ItemArray.GetValue(0).ToString())
                                {
                                    CheckBox box = (CheckBox)item.Cells[1].FindControl("chkSelect");
                                    box.Checked = true;
                                    item.BackColor = System.Drawing.Color.LightSeaGreen;
                                }
                            }
                        }
                        return "success";
                    }
                    this.lblMsg.Text = "Could not load appointment data. Possible reasons - Case type is not added to the case.";
                    this.lblMsg.Visible = true;
                    this.usrMessage.PutMessage("Could not load appointment data. Possible reasons - Case type is not added to the case.");
                    this.usrMessage.SetMessageType(0);
                    this.usrMessage.Show();
                    return "missing_case_type";
                }
                this.usrMessage.PutMessage("Could not load appointment data. Possible reasons - Case type is not added to the case.");
                this.usrMessage.SetMessageType(0);
                this.usrMessage.Show();
                return "missing_case_type";
            }
            this.usrMessage.PutMessage("Could not load appointment data. Possible reasons - Case type is not added to the case.");
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
            return "missing_case_type";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        return null;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private DataSet getLocationDataset()
    {
        DataSet set = new DataSet();
        DataTable table = new DataTable();
        table.Columns.Add("Description");
        table.Columns.Add("Code");
        DataRow row = table.NewRow();
        row["Description"] = "Maharashtra";
        row["Code"] = "MH";
        table.Rows.Add(row);
        set.Tables.Add(table);
        return set;
    }

    private string GetOpenCaseStatus()
    {
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        string str2 = "";
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();
            SqlDataReader reader = new SqlCommand("Select SZ_CASE_STATUS_ID FROM MST_CASE_STATUS WHERE SZ_STATUS_NAME='OPEN' AND SZ_COMPANY_ID='" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'", connection).ExecuteReader();
            while (reader.Read())
            {
                str2 = Convert.ToString(reader[0].ToString());
            }
        }
        catch
        {
        }
        return str2;
    }

    protected void grdProcedureCode_ItemDataBound(object sender, DataGridItemEventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            if (e.Item.Cells[5].Controls.Count > 0)
            {
                DropDownList list = (DropDownList)e.Item.Cells[5].FindControl("ddlReSchHours");
                DropDownList list2 = (DropDownList)e.Item.Cells[5].FindControl("ddlReSchMinutes");
                DropDownList list3 = (DropDownList)e.Item.Cells[5].FindControl("ddlReSchTime");
                for (int i = 0; i <= 12; i++)
                {
                    if (i > 9)
                    {
                        list.Items.Add(i.ToString());
                    }
                    else
                    {
                        list.Items.Add("0" + i.ToString());
                    }
                }
                for (int j = 0; j < 60; j++)
                {
                    if (j > 9)
                    {
                        list2.Items.Add(j.ToString());
                    }
                    else
                    {
                        list2.Items.Add("0" + j.ToString());
                    }
                }
                list3.Items.Add("AM");
                list3.Items.Add("PM");
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkPatientDesk_Click(object sender, EventArgs e)
    {
        if (this.txtCaseID.Text != "")
        {
            this.Session["SZ_CASE_ID"] = this.txtCaseID.Text;
            this.Session["PROVIDERNAME"] = this.txtCaseID.Text;
            CaseDetailsBO sbo = new CaseDetailsBO();
            Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
            obj2.SZ_PATIENT_ID = this.txtPatientID.Text;
            obj2.SZ_CASE_ID = this.txtCaseID.Text;
            obj2.SZ_PATIENT_NAME = this.txtPatientFName.Text + this.txtPatientLName.Text;
            obj2.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            obj2.SZ_CASE_NO = sbo.GetCaseNo(obj2.SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.Session["CASE_OBJECT"] = obj2;
            Bill_Sys_Case @case = new Bill_Sys_Case();
            @case.SZ_CASE_ID = this.txtCaseID.Text;
            this.Session["CASEINFO"] = @case;
            this.Session["QStrCaseID"] = this.txtCaseID.Text;
            this.Session["Case_ID"] = this.txtCaseID.Text;
            this.Session["Archived"] = "0";
            this.Session["QStrCID"] = this.txtCaseID.Text;
            this.Session["SelectedID"] = this.txtCaseID.Text;
            string str = "../Bill_SysPatientDesk.aspx?Flag=true";
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='" + str + "';", true);
            this.hdnReturnOpration.Value = "refresh";
            this.hdnReturnPath.Value = str;
        }
    }

    private void LoadPatientDetails()
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ADD_APPOINTMENT.ToString() == "1")
            {
                DataSet dsRoomDetails = GetRoomDetailsBySpeciality(this.roomID.Value, txtCompanyID.Text);
                DataSet dsAppointment = GetAppointmentInterval(txtCompanyID.Text);
                ArrayList arrList = new ArrayList();
                if (dsRoomDetails.Tables.Count > 0)
                {
                    if (dsRoomDetails.Tables[0].Rows.Count > 0)
                    {
                        Double startTime = Convert.ToDouble(dsRoomDetails.Tables[0].Rows[0]["FL_START_TIME"]);
                        Double endTime = Convert.ToDouble(dsRoomDetails.Tables[0].Rows[0]["FL_END_TIME"]);

                        for (Double i = startTime; i < endTime; i++)
                            if (startTime < endTime)
                            {
                                if (arrList.Count == 0)
                                {
                                    string t1 = String.Format("{0:F2}", startTime);
                                    arrList.Add(t1);
                                }
                                else
                                {
                                    string date = "";
                                    DateTime sTime;
                                    if (dsAppointment.Tables[0].Rows.Count > 0)
                                    {
                                        int appointmentTime = Convert.ToInt32(dsAppointment.Tables[0].Rows[0]["i_interval_minutes"]);

                                        if (Session["FinalTime"] == null)
                                        {
                                            string t1 = String.Format("{0:F2}", startTime);

                                            date = this.hdnAppDate.Value.ToString() + " " + dsRoomDetails.Tables[0].Rows[0]["FL_START_TIME"];
                                            sTime = DateTime.Parse(t1.Replace(".", ":")).AddMinutes(appointmentTime);
                                        }
                                        else
                                        {
                                            sTime = DateTime.Parse(Session["FinalTime"].ToString().Replace(".", ":")).AddMinutes(appointmentTime);
                                        }
                                        //string strRemove = GetLast(sTime.ToString(), 11);
                                        string finalTime = sTime.ToString("H:mm");
                                        finalTime= finalTime.Replace(":", ".");
                                        Session["FinalTime"] = finalTime;
                                        arrList.Add(finalTime);
                                    }
                                    else
                                    {
                                        startTime++;
                                        arrList.Add(startTime);
                                    }
                                }
                            }
                    }
                }
                string time = ConvertTimeFormat(hdnAppTime.Value);

                Decimal timeSlotOne;
                Decimal timeSlotTwo;
                string count="";
                string count1 = "";
                string count2 = "";
                time = time.Replace(':', '.');
                Decimal convertTime = Convert.ToDecimal(time);
                for (int j = 0; j < arrList.Count - 1; j++)
                {

                    if (convertTime >= Convert.ToDecimal(arrList[j]) && convertTime < Convert.ToDecimal(arrList[j + 1]))
                    {
                        timeSlotOne = Convert.ToDecimal(arrList[j]);
                        timeSlotTwo = Convert.ToDecimal(arrList[j + 1]);

                        string t1 = String.Format("{0:F2}", timeSlotOne);
                        string t2 = String.Format("{0:F2}", timeSlotTwo);


                        string convertT1 = ConvertTo12TimeFormat(t1);
                        string convertT2 = ConvertTo12TimeFormat(t2);

                        convertT1 = convertT1.Replace(':', '.');
                        convertT2 = convertT2.Replace(':', '.');
                        string timeTypeT1 = GetLast(convertT1.ToString(), 3);
                        string timeT1 = GetLast((convertT1.Replace(timeTypeT1, "")), 5).Replace(':', '.').ToString();
                        string timeTypeT2 = GetLast(convertT2.ToString(), 3);
                        string timeT2 = GetLast((convertT2.Replace(timeTypeT2, "")), 5).Replace(':', '.').ToString();

                        timeTypeT1 = timeTypeT1.Trim();
                        timeTypeT2 = timeTypeT2.Trim();

                        CaseDetailsBO caseDetailsBO = new CaseDetailsBO();

                        string caseId = caseDetailsBO.GetCaseIdByPatientID(txtCompanyID.Text, hdnPatientID.Value);
                        count = GetPatientsVisitCounts(this.roomID.Value, txtCompanyID.Text, this.hdnAppDate.Value.ToString(), timeT1, timeT2, caseId, timeTypeT1, timeTypeT2);
                        string[] tmp = count.Split(',');
                        count1 = tmp[0];
                        count2 = tmp[1];
                    }
                }
                int iPatientCount = 0;
                int iCaseCount = 0;
                iPatientCount = Convert.ToInt32(count1);
                iCaseCount = Convert.ToInt32(count2);
                if (dsAppointment.Tables[0].Rows.Count > 0)
                {
                    int appointmentTime = Convert.ToInt32(dsAppointment.Tables[0].Rows[0]["i_interval_minutes"]);
                    
                    if (iPatientCount >= Convert.ToInt32(dsAppointment.Tables[0].Rows[0]["i_patient_count"]) && iCaseCount == 0)
                    {
                        this.lblMsg.Text = "You can add only " + iPatientCount + " case(s) in " + appointmentTime + " minutes.";
                        this.lblMsg.Visible = true;
                        this.usrMessage.PutMessage(this.lblMsg.Text);
                        this.usrMessage.SetMessageType(0);
                        this.usrMessage.Show();
                        return;
                    }
                }
                else
                {
                    if (iPatientCount >= 1 && iCaseCount == 0)
                    {
                        this.lblMsg.Text = "You can add only '" + iPatientCount + "' case in an 1 hour.";
                        this.lblMsg.Visible = true;
                        this.usrMessage.PutMessage(this.lblMsg.Text);
                        this.usrMessage.SetMessageType(0);
                        this.usrMessage.Show();
                        return;
                    }
                }
            }

            new DataTable();
            this.ds = new DataSet();
            this.ds = new OutSchedule().GetPatientData(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, this.hdnPatientID.Value);
            this.ds.Tables[0].Clone();
            if ((this.ds.Tables.Count > 0) && (this.ds.Tables[0].Rows.Count > 0))
            {
                this.lnkPatientDesk.Visible = true;
                this.txtPatientID.Text = this.ds.Tables[0].Rows[0]["SZ_PATIENT_ID"].ToString();
                this.txtPatientFName.Text = this.ds.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                this.txtMI.Text = this.ds.Tables[0].Rows[0]["MI"].ToString();
                this.txtPatientLName.Text = this.ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                this.txtPatientPhone.Text = this.ds.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
                this.txtPatientAddress.Text = this.ds.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                this.cmbState.Value = this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();
                this.txtBirthdate.Text = this.ds.Tables[0].Rows[0]["DT_DOB"].ToString();
                this.txtPatientAge.Text = this.ds.Tables[0].Rows[0]["I_PATIENT_AGE"].ToString();
                this.txtSocialSecurityNumber.Text = this.ds.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                if (this.ds.Tables[0].Rows[0]["BT_TRANSPORTATION"].ToString() == "True")
                {
                    this.chkTransportation.Checked = true;
                }
                else
                {
                    this.chkTransportation.Checked = false;
                }
                DataSet caseDetails = new Billing_Sys_ManageNotesBO().GetCaseDetails(this.txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                if (caseDetails.Tables[0].Rows.Count > 0)
                {
                    OutSchedule schedule2 = new OutSchedule();
                    this.cmbInsurance.TextField = "DESCRIPTION";
                    this.cmbInsurance.ValueField = "CODE";
                    this.cmbInsurance.DataSource = schedule2.GetInsuranceCompany(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.cmbInsurance.DataBind();
                    this.cmbInsurance.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                    this.cmbInsurance.Value = "NA";
                    this.cmbCaseType.TextField = "DESCRIPTION";
                    this.cmbCaseType.ValueField = "CODE";
                    this.cmbCaseType.DataSource = schedule2.GetCaseType(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.cmbCaseType.DataBind();
                    this.cmbCaseType.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                    this.cmbCaseType.Value = "NA";
                    this.cmbReferringDoctor.TextField = "DESCRIPTION";
                    this.cmbReferringDoctor.ValueField = "CODE";
                    this.cmbReferringDoctor.DataSource = schedule2.GetReferringDoctor(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.cmbReferringDoctor.DataBind();
                    this.cmbReferringDoctor.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                    this.cmbReferringDoctor.Value = "NA";
                    this.cmbCaseStatus.TextField = "DESCRIPTION";
                    this.cmbCaseStatus.ValueField = "CODE";
                    this.cmbCaseStatus.DataSource = schedule2.GetCaseStatus(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.cmbCaseStatus.DataBind();
                    this.cmbCaseStatus.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                    this.cmbCaseStatus.Value = "NA";
                    this.cmbMedicalOffice.TextField = "DESCRIPTION";
                    this.cmbMedicalOffice.ValueField = "CODE";
                    this.cmbMedicalOffice.DataSource = schedule2.GetMedicalOffice(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.cmbMedicalOffice.DataBind();
                    this.cmbMedicalOffice.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                    this.cmbMedicalOffice.Value = "NA";
                    this.cmbReferringFacility.TextField = "DESCRIPTION";
                    this.cmbReferringFacility.ValueField = "CODE";
                    this.cmbReferringFacility.DataSource = schedule2.GetReferringFacility(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.cmbReferringFacility.DataBind();
                    this.cmbReferringFacility.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                    this.cmbReferringFacility.Value = "NA";
                    this.txtCaseID.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    this.hdnCaseID.Value = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                    {
                        this.cmbInsurance.TextField = "DESCRIPTION";
                        this.cmbInsurance.ValueField = "CODE";
                        this.cmbInsurance.DataSource = schedule2.GetInsuranceCompany(caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                        this.cmbInsurance.DataBind();
                        this.cmbInsurance.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                        this.cmbInsurance.Value = "NA";
                        this.cmbCaseType.TextField = "DESCRIPTION";
                        this.cmbCaseType.ValueField = "CODE";
                        this.cmbCaseType.DataSource = schedule2.GetCaseType(caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                        this.cmbCaseType.DataBind();
                        this.cmbCaseType.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                        this.cmbCaseType.Value = "NA";
                    }
                    else
                    {
                        this.cmbInsurance.TextField = "DESCRIPTION";
                        this.cmbInsurance.ValueField = "CODE";
                        this.cmbInsurance.DataSource = schedule2.GetInsuranceCompany(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        this.cmbInsurance.DataBind();
                        this.cmbInsurance.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                        this.cmbInsurance.Value = "NA";
                        this.cmbCaseType.TextField = "DESCRIPTION";
                        this.cmbCaseType.ValueField = "CODE";
                        this.cmbCaseType.DataSource = schedule2.GetCaseType(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        this.cmbCaseType.DataBind();
                        this.cmbCaseType.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                        this.cmbCaseType.Value = "NA";
                    }
                    if (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x15).ToString() != "")
                    {
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            this.cmbMedicalOffice.Value = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x15).ToString();
                            this.cmbMedicalOffice.Enabled = false;
                            this.cmbReferringDoctor.TextField = "DESCRIPTION";
                            this.cmbReferringDoctor.ValueField = "CODE";
                            this.cmbReferringDoctor.DataSource = schedule2.GetReferringDoctorByOffice(caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x15).ToString());
                            this.cmbReferringDoctor.DataBind();
                            this.cmbReferringDoctor.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                            this.cmbReferringDoctor.Value = "NA";
                        }
                    }
                    else
                    {
                        this.cmbMedicalOffice.Enabled = true;
                    }
                    this.cmbInsurance.Value = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    this.cmbCaseType.Value = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    if (this.txtPatientID.Text.ToString() != "")
                    {
                        CaseDetailsBO sbo2 = new CaseDetailsBO();
                        Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                        obj2.SZ_PATIENT_ID = this.txtPatientID.Text;
                        obj2.SZ_CASE_ID = this.txtCaseID.Text;
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                        {
                            obj2.SZ_CASE_NO = sbo2.GetCaseNo(obj2.SZ_CASE_ID, caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                        }
                        else
                        {
                            obj2.SZ_CASE_NO = sbo2.GetCaseNo(obj2.SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        }
                        obj2.SZ_PATIENT_NAME = sbo2.GetPatientName(obj2.SZ_PATIENT_ID);
                        this.Session["CASE_OBJECT"] = obj2;
                    }
                }
                this.DisplayControlForAddVisit();
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    this.cmbReferringFacility.Value = this.reffCompanyID.Value.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void LoadProcedureGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            ArrayList list = new ArrayList();
            if (Convert.ToString(this.cmbReferringFacility.SelectedItem.Value) != "NA")
            {
                list.Add(this.cmbReferringFacility.SelectedItem.Value.ToString());
            }
            else
            {
                list.Add(this.txtCompanyID.Text);
            }
            string str = this.roomID.Value;
            list.Add(str);
            this.grdProcedureCode.DataSource = new Bill_Sys_ManageVisitsTreatmentsTests_BO().GetReferringProcCodeList(list);
            this.grdProcedureCode.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void onLoadPatient_CallBack(object source, DevExpress.Web.CallbackEventArgsBase b)
    {
        this.lblMsg.Text = "";
        this.lblMsg.Visible = false;
        if (this.hdnOperation.Value.ToString() == "save")
        {
            this.SaveEvent();
        }
        else if (this.hdnOperation.Value.ToString() == "update")
        {
            this.UpdateEvent();
        }
        else if (this.hdnOperation.Value.ToString() == "delete")
        {
            this.DeleteEvent();
        }
        else
        {
            this.LoadPatientDetails();
        }
    }

    protected void onUpdateSearch_CallBack(object source, DevExpress.Web.CallbackEventArgsBase b)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            this.Session["Page_Index"] = "";
            this.SearchPatientCallBackList();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void onUserMessege_CallBack(object source, DevExpress.Web.CallbackEventArgsBase b)
    {
        this.usrMessage.PutMessage(this.hdnUserMessege.Value);
        this.usrMessage.SetMessageType(0);
        this.usrMessage.Show();
        this.hdnUserMessege.Value = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string text = this.txtPatientFName.Text;
        string text2 = this.hdnPatientID.Value;
        if (!base.IsPostBack && !base.IsCallback)
        {
            if (base.Request.QueryString["reffCompany"] != null)
            {
                this.reffCompanyID.Value = base.Request.QueryString["reffCompany"].ToString();
            }
            if (base.Request.QueryString["RoomID"] != null)
            {
                this.roomID.Value = base.Request.QueryString["RoomID"].ToString();
            }
            if (base.Request.QueryString["flag"] != null)
            {
                this.hdnOpration.Value = base.Request.QueryString["flag"].ToString();
            }
            if (base.Request.QueryString["RefOffID"] != null)
            {
                this.reffOfficeID.Value = base.Request.QueryString["RefOffID"].ToString();
            }
            if (base.Request.QueryString["appointmentTime"] != null)
            {
                this.hdnAppTime.Value = base.Request.QueryString["appointmentTime"].ToString();
            }
            if (base.Request.QueryString["evt"] != null)
            {
                this.hdnEventID.Value = base.Request.QueryString["evt"].ToString();
            }
            if (base.Request.QueryString["appointmentDate"] != null)
            {
                this.hdnAppDate.Value = base.Request.QueryString["appointmentDate"].ToString();
            }
            if (this.reffOfficeID.Value.ToString() != "")
            {
                this.btnAddPatient.Visible = false;
            }
            else
            {
                this.btnAddPatient.Visible = true;
            }

            Session["FinalTime"] = null;

            string str = ((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_COPY_PATIENT_TO_TEST_FACILITY.ToString();
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            



            this.txtUserId.Text = "";
            OutSchedule schedule = new OutSchedule();
            this.cmbInsurance.TextField = "DESCRIPTION";
            this.cmbInsurance.ValueField = "CODE";
            this.cmbInsurance.DataSource = schedule.GetInsuranceCompany(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.cmbInsurance.DataBind();
            this.cmbInsurance.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
            this.cmbInsurance.Value = "NA";
            this.cmbState.TextField = "DESCRIPTION";
            this.cmbState.ValueField = "CODE";
            this.cmbState.DataSource = schedule.GetState();
            this.cmbState.DataBind();
            this.cmbState.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
            this.cmbState.Value = "NA";
            this.cmbReferringDoctor.TextField = "DESCRIPTION";
            this.cmbReferringDoctor.ValueField = "CODE";
            this.cmbReferringDoctor.DataSource = schedule.GetReferringDoctor(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.cmbReferringDoctor.DataBind();
            this.cmbReferringDoctor.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
            this.cmbReferringDoctor.Value = "NA";
            this.cmbReferringFacility.TextField = "DESCRIPTION";
            this.cmbReferringFacility.ValueField = "CODE";
            this.cmbReferringFacility.DataSource = schedule.GetReferringFacility(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.cmbReferringFacility.DataBind();
            this.cmbReferringFacility.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
            this.cmbReferringFacility.Value = "NA";
            this.cmbMedicalOffice.TextField = "DESCRIPTION";
            this.cmbMedicalOffice.ValueField = "CODE";
            this.cmbMedicalOffice.DataSource = schedule.GetMedicalOffice(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.cmbMedicalOffice.DataBind();
            this.cmbMedicalOffice.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
            this.cmbMedicalOffice.Value = "NA";
            this.cmbCaseType.TextField = "DESCRIPTION";
            this.cmbCaseType.ValueField = "CODE";
            this.cmbCaseType.DataSource = schedule.GetCaseType(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.cmbCaseType.DataBind();
            this.cmbCaseType.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
            this.cmbCaseType.Value = "NA";
            this.cmbCaseStatus.TextField = "DESCRIPTION";
            this.cmbCaseStatus.ValueField = "CODE";
            this.cmbCaseStatus.DataSource = schedule.GetCaseStatus(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.cmbCaseStatus.DataBind();
            this.cmbCaseStatus.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
            this.cmbCaseStatus.Value = "NA";
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.cmbTransport.TextField = "DESCRIPTION";
                this.cmbTransport.ValueField = "CODE";
                this.cmbTransport.DataSource = schedule.GetTransport(this.txtCompanyID.Text);
                this.cmbTransport.DataBind();
                this.cmbTransport.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                this.cmbTransport.Value = "NA";
            }
            else
            {
                this.cmbTransport.TextField = "DESCRIPTION";
                this.cmbTransport.ValueField = "CODE";
                this.cmbTransport.DataSource = schedule.GetTransport(this.cmbReferringFacility.SelectedItem.Value.ToString());
                this.cmbTransport.DataBind();
                this.cmbTransport.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                this.cmbTransport.Value = "NA";
            }
            this.BindTimeControl();
            string str2 = this.hdnAppTime.Value;
            if (str2.Substring(2, 1) == ".")
            {
                this.ddlHours.SelectedValue = str2.Substring(0, 2).Replace(" ", "0");
            }
            else
            {
                this.ddlHours.SelectedValue = "0" + str2.Substring(0, 1);
            }
            this.ddlMinutes.SelectedValue = str2.Substring(str2.IndexOf(".") + 1, 2);
            this.ddlTime.SelectedValue = str2.Substring(str2.IndexOf(".") + 3, 2);
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.cmbReferringFacility.Value = this.reffCompanyID.Value.ToString();
            }
            string str3 = this.Session["INTERVAL"].ToString();
            int num = Convert.ToInt32(this.ddlMinutes.SelectedValue) + Convert.ToInt32(str3.Substring(2, 2));
            int num2 = Convert.ToInt32(this.ddlHours.SelectedValue);
            string selectedValue = this.ddlTime.SelectedValue;
            if (num >= 60)
            {
                num -= 60;
                num2++;
                if (num2 > 12)
                {
                    num2 -= 12;
                    if (this.ddlHours.SelectedValue != "12")
                    {
                        if (selectedValue == "AM")
                        {
                            selectedValue = "PM";
                        }
                        else if (selectedValue == "PM")
                        {
                            selectedValue = "AM";
                        }
                    }
                }
                else if ((num2 == 12) && (this.ddlHours.SelectedValue != "12"))
                {
                    if (selectedValue == "AM")
                    {
                        selectedValue = "PM";
                    }
                    else if (selectedValue == "PM")
                    {
                        selectedValue = "AM";
                    }
                }
            }
            this.ddlEndHours.SelectedValue = num2.ToString().PadLeft(2, '0');
            this.ddlEndMinutes.SelectedValue = num.ToString().PadLeft(2, '0');
            this.ddlEndTime.SelectedValue = selectedValue.ToString();
            PopupBO pbo = new PopupBO();
            string companyID = this.txtCompanyID.Text;
            string str6 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            if (this.Session["CASE_OBJECT"] != null)
            {
                companyID = new PopupBO().GetCompanyID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID);
            }
            if (this.Session["Flag"] != null)
            {
                this._patient_TVBO = new Patient_TVBO();
                this.Session["PatientDataList"] = this._patient_TVBO.GetSelectedPatientDataListNEW(companyID, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID, str6);
                this.SearchPatientList();
                this.btnClickSearch.Visible = false;
            }
            else if (this.Session["DataEntryFlag"] != null)
            {
                this._patient_TVBO = new Patient_TVBO();
                this.Session["PatientDataList"] = this._patient_TVBO.GetSelectedPatientDataListNEW(companyID, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID, str6);
                this.SearchPatientList();
                this.btnClickSearch.Visible = false;
                this.Session["DataEntryFlag"] = null;
            }
            else
            {
                this._patient_TVBO = new Patient_TVBO();
                this.txtPatientID.Text = "";
                this.txtPatientFName.Text = "";
                this.txtMI.Text = "";
                this.txtPatientLName.Text = "";
                this.txtPatientPhone.Text = "";
                this.txtPatientAddress.Text = "";
                this.txtState.Text = "";
                this.txtBirthdate.Text = "";
                this.txtPatientAge.Text = "";
                this.txtSocialSecurityNumber.Text = "";
                this.txtCaseID.Text = "";
                this.cmbCaseType.Text = "NA";
                this.cmbInsurance.Text = "NA";
                this.cmbState.Text = "NA";
                this.cmbMedicalOffice.Text = "NA";
                this.btnClickSearch.Visible = true;
            }
            this.LoadProcedureGrid();
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.cmbMedicalOffice.Visible = true;
                this.cmbReferringFacility.Visible = false;
                this.lblTypetext.Visible = false;
                this.ddlType.Visible = false;
                this.lblTestFacility.Text = "Office Name";
                if (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.Trim().ToLower().ToString() == "referring office")
                {
                    new Bill_Sys_BillingCompanyDetails_BO();
                    string str7 = this.reffOfficeID.Value.ToString();
                    if (str7 != "")
                    {
                        this.cmbMedicalOffice.Value = str7;
                        this.cmbMedicalOffice.Enabled = false;
                        this.cmbReferringDoctor.TextField = "DESCRIPTION";
                        this.cmbReferringDoctor.ValueField = "CODE";
                        this.cmbReferringDoctor.DataSource = schedule.GetReferringDoctorForOffice(str7, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        this.cmbReferringDoctor.DataBind();
                        this.cmbReferringDoctor.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                        this.cmbReferringDoctor.Value = "NA";
                    }
                }
            }
            this.cmbCaseStatus.Value = this.GetOpenCaseStatus();
            if (this.hdnEventID.Value.ToString() != "")
            {
                string str8 = this.hdnEventID.Value.ToString();
                this._patient_TVBO = new Patient_TVBO();
                if (this._patient_TVBO.getScheduleStatus(str8))
                {
                    this.lblMsg2.Visible = true;
                    this.lblMsg2.Text = "Visit Completed.";
                    this.lblMsg2.ForeColor = System.Drawing.Color.Red;
                    this.Session["VISIT_COMPLETED"] = "YES";
                }
                this.Session["SCHEDULEDID"] = str8;
                if (this.reffCompanyID.Value.ToString() == this.txtCompanyID.Text.ToString())
                {
                    this.ddlTestNames.Visible = false;
                    this.divProcedureCode.Visible = true;
                    this.SelectSavedProcedureCodes(str8);
                }
                else
                {
                    this.LoadProcedureGrid();
                }
                this.GETAppointPatientDetail(Convert.ToInt32(str8));
                this.btnClickSearch.Visible = false;
                this.tdSerach.Visible = false;
                this.trReminder.Visible = true;
                this.tdSerach.Height = "0px";
            }
            else if (str == "1")
            {
                string str9 = "";
                if (base.Request.QueryString["testFacility"] != null)
                {
                    str9 = "Patient will be copied to " + base.Request.QueryString["testFacility"].ToString() + " account.";
                }
                else
                {
                    str9 = "Patient will be copied to selected test facility account.";
                }
                this.lblMsg2.Visible = true;
                this.lblMsg2.Text = str9;
                this.lblMsg2.ForeColor = System.Drawing.Color.Red;
            }
            this.setControlAccordingOperation();
            if (this.txtPatientFName.Text == "")
            {
                this.lnkPatientDesk.Visible = false;
            }
            else
            {
                this.lnkPatientDesk.Visible = true;
            }
        }
    }

    private void SaveEvent()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        this.lblMsg.Visible = false;
        OutSchedulePatientDAO tdao = new OutSchedulePatientDAO();
        string str = "";
        string caseIdForDocumentPath = "";
        calOperation operation = new calOperation();
        calPatientEO teo = new calPatientEO();
        calEvent event2 = new calEvent();
        CalendarTransaction transaction = new CalendarTransaction();
        calResult result = new calResult();
        ArrayList list = new ArrayList();
        ArrayList list2 = new ArrayList();
        string text = "";
        Billing_Sys_ManageNotesBO sbo = new Billing_Sys_ManageNotesBO();
        try
        {
            this.txtPatientID.Text = this.hdnPatientID.Value.ToString();
            this.txtUserId.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this.cmbReferringFacility.Value = this.reffCompanyID.Value.ToString();
            this.txtCaseID.Text = this.hdnCaseID.Value;
            int isProcedure = 0;
            for (int j = 0; j < this.grdProcedureCode.Items.Count; j++)
            {
                CheckBox box = (CheckBox)this.grdProcedureCode.Items[j].FindControl("chkSelect");
                if (box.Checked)
                {
                    isProcedure = 1;
                    break;
                }
            }
            if (this.txtPatientFName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Patient First Name should not be Empty.')", true);
                this.lblMsg.Text = Convert.ToString("atient First Name should not be Empty.");
                this.lblMsg.Visible = true;
            }
            else if (this.txtPatientLName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Patient Last name should not be Empty.')", true);
                this.lblMsg.Text = Convert.ToString("Patient Last name should not be Empty.");
                this.lblMsg.Visible = true;
            }
            else if (this.cmbReferringFacility.Visible && (Convert.ToString(this.cmbReferringFacility.SelectedItem.Value) == "NA"))
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Please Select any one Reference Facility.')", true);
                this.lblMsg.Text = Convert.ToString("Please Select any one Reference Facility.");
                this.lblMsg.Visible = true;
            }
            else if (Convert.ToString(this.cmbReferringDoctor.Value) == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Please Select any one Referring Doctor.')", true);
                this.lblMsg.Text = Convert.ToString("Please Select any one Referring Doctor.");
                this.lblMsg.Visible = true;
            }
            else if (this.chkTransportation.Checked && (this.cmbTransport.Value == null || (Convert.ToString(this.cmbTransport.Value) == "NA")))
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Please select transport company.')", true);
                this.lblMsg.Text = Convert.ToString("Please select transport company.");
                this.lblMsg.Visible = true;
            }
            else if (isProcedure == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Please select atleast one procedure code.')", true);
                this.lblMsg.Text = Convert.ToString("Please select atleast one procedure code.");
                this.lblMsg.Visible = true;
            }
            else
            {
                if (!this.txtPatientFName.ReadOnly)
                {
                    ArrayList list3 = new ArrayList();
                    list3.Add(this.txtPatientFName.Text);
                    list3.Add(this.txtPatientLName.Text);
                    list3.Add(null);
                    list3.Add(this.cmbCaseType.SelectedItem.Value.ToString());
                    if (this.txtBirthdate.Text != "")
                    {
                        list3.Add(this.txtBirthdate.Text);
                    }
                    else
                    {
                        list3.Add(null);
                    }
                    list3.Add(this.txtCompanyID.Text);
                    list3.Add("existpatient");
                    string str4 = new Bill_Sys_PatientBO().CheckPatientExists(list3);
                    if ((str4 != "") && (this.txtPatientExistMsg.Value == ""))
                    {
                        this.msgPatientExists.InnerHtml = str4;
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "javascript:openExistsPage();", true);
                        this.hdnReturnOpration.Value = "check permission";
                        return;
                    }
                    teo = this.create_calPatientEO();
                    this.txtPatientID.Text = sbo.GetPatientLatestID();
                    operation.add_patient = true;
                }
                if (this.hdnAppDate.Value.ToString() != "")
                {
                    string str5 = this.check_appointment();
                    if (str5 != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('" + str5 + "')", true);
                        lblMsg.Visible = true;
                        lblMsg.Text = str5;
                        return;
                    }
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (this.check_appointment_for_period() != ""))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('The patient is already scheduled for this date and time period.')", true);
                        lblMsg.Visible = true;
                        lblMsg.Text = "The patient is already scheduled for this date and time period.";
                        return;
                    }
                    //Convert.ToDateTime(this.hdnAppDate.Value.ToString());
                    if (this.hdnAppDate.Value.ToString() != "")
                    {
                        string str7 = "";
                        string str8 = this.hdnAppDate.Value.ToString();
                        if (this.hdnEventID.Value.ToString() != "")
                        {
                            this.hdnEventID.Value.ToString();
                            Bill_Sys_ReferalEvent event3 = new Bill_Sys_ReferalEvent();
                            ArrayList list4 = new ArrayList();
                            list4.Add(this.cmbReferringDoctor.SelectedItem.Value.ToString());
                            list4.Add(this.txtPatientID.Text);
                            list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            str7 = event3.AddDoctor(list4);
                            for (int j = 0; j < this.grdProcedureCode.Items.Count; j++)
                            {
                                CheckBox box = (CheckBox)this.grdProcedureCode.Items[j].FindControl("chkSelect");
                                if (box.Checked)
                                {
                                    text = this.grdProcedureCode.Items[j].Cells[1].Text;
                                    list4 = new ArrayList();
                                    list4.Add(str7);
                                    list4.Add(text);
                                    list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    list4.Add(text);
                                    event3.AddDoctorAmount(list4);
                                    list4 = new ArrayList();
                                    list4.Add(this.txtPatientID.Text);
                                    list4.Add(str7);
                                    list4.Add(str8);
                                    list4.Add(text);
                                    list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    list4.Add(this.ddlType.SelectedValue);
                                    event3.AddPatientProc(list4);
                                }
                            }
                        }
                        else if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            new Bill_Sys_ReferalEvent();
                            new ArrayList();
                            str7 = this.cmbReferringDoctor.SelectedItem.Value.ToString();
                            for (int k = 0; k < this.grdProcedureCode.Items.Count; k++)
                            {
                                CheckBox box2 = (CheckBox)this.grdProcedureCode.Items[k].FindControl("chkSelect");
                                if (box2.Checked)
                                {
                                    text = this.grdProcedureCode.Items[k].Cells[1].Text;
                                    calDoctorAmount amount = new calDoctorAmount();
                                    amount.SZ_DOCTOR_ID = str7;
                                    amount.SZ_PROCEDURE_ID = text;
                                    amount.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                    amount.SZ_TYPE_CODE_ID = text;
                                    list.Add(amount);
                                    operation.bt_add_doctor_amount = true;
                                }
                            }
                        }
                        else
                        {
                            str7 = this.cmbReferringDoctor.Value.ToString();
                        }
                        if (this.cmbMedicalOffice.Visible && (this.cmbMedicalOffice.Text != ""))
                        {
                            bool flag1 = this.cmbMedicalOffice.Text != "NA";
                        }
                        string str9 = this.hdnAppDate.Value.ToString();
                        new Bill_Sys_Calender();
                        new ArrayList();
                        event2.SZ_PATIENT_ID = this.txtPatientID.Text;
                        event2.DT_EVENT_DATE = str9;
                        event2.DT_EVENT_TIME = this.ddlHours.SelectedValue.ToString() + "." + this.ddlMinutes.SelectedValue.ToString();
                        event2.SZ_EVENT_NOTES = this.txtNotes.Text;
                        event2.SZ_DOCTOR_ID = str7;
                        text = "";
                        int num3 = 0;
                        while (num3 < this.grdProcedureCode.Items.Count)
                        {
                            text = this.grdProcedureCode.Items[num3].Cells[1].Text;
                            break;
                        }
                        if (text != null)
                        {
                            event2.SZ_TYPE_CODE_ID = text;
                        }
                        else
                        {
                            event2.DT_EVENT_DATE = str9;
                        }
                        event2.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        event2.DT_EVENT_TIME_TYPE = this.ddlTime.SelectedValue;
                        event2.DT_EVENT_END_TIME = this.ddlEndHours.SelectedValue.ToString() + "." + this.ddlEndMinutes.SelectedValue.ToString();
                        event2.DT_EVENT_END_TIME_TYPE = this.ddlEndTime.SelectedValue;
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            event2.SZ_REFERENCE_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        }
                        else
                        {
                            event2.SZ_REFERENCE_ID = this.reffCompanyID.Value.ToString();
                        }
                        event2.BT_STATUS = "0";
                        if (this.chkTransportation.Checked)
                        {
                            event2.BT_TRANSPORTATION = "1";
                        }
                        else
                        {
                            event2.BT_TRANSPORTATION = "0";
                        }
                        event2.DT_EVENT_DATE = str9;
                        if (this.chkTransportation.Checked && this.cmbTransport.Value != null)
                        {
                            event2.I_TRANSPORTATION_COMPANY = this.cmbTransport.Value.ToString();
                        }
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            event2.SZ_OFFICE_ID = this.cmbMedicalOffice.SelectedItem.Value.ToString();
                        }
                        for (int i = 0; i < this.grdProcedureCode.Items.Count; i++)
                        {
                            CheckBox box3 = (CheckBox)this.grdProcedureCode.Items[i].FindControl("chkSelect");
                            if (box3.Checked)
                            {
                                text = this.grdProcedureCode.Items[i].Cells[1].Text;
                                calProcedureCodeEO eeo = new calProcedureCodeEO();
                                eeo.SZ_PROC_CODE = text;
                                eeo.I_EVENT_ID = "";
                                eeo.I_STATUS = "0";
                                list2.Add(eeo);
                            }
                        }
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_COPY_PATIENT_TO_TEST_FACILITY.ToString() == "1")
                        {
                            if (!this.txtPatientFName.ReadOnly)
                            {
                                ArrayList list5 = new ArrayList();
                                list5.Add(this.txtPatientFName.Text);
                                list5.Add(this.txtPatientLName.Text);
                                list5.Add(null);
                                list5.Add(this.cmbCaseType.SelectedItem.Value.ToString());
                                if (this.txtBirthdate.Text != "")
                                {
                                    list5.Add(this.txtBirthdate.Text);
                                }
                                else
                                {
                                    list5.Add(null);
                                }
                                list5.Add(this.reffCompanyID.Value.ToString());
                                list5.Add("existpatient");
                                string str11 = new Bill_Sys_PatientBO().CheckPatientExists(list5);
                                if ((str11 != "") && (this.txtPatientExistMsg.Value == ""))
                                {
                                    this.msgPatientExists.InnerHtml = str11;
                                    ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "javascript:openExistsPage();", true);
                                    this.hdnReturnOpration.Value = "check permission";
                                    return;
                                }
                                tdao = this.create_outPatientDAO();
                                this.txtPatientID.Text = sbo.GetPatientLatestID();
                                tdao.addPatient = true;
                            }
                            string str12 = this.roomID.Value.ToString();
                            OutSchedulePatient patient = new OutSchedulePatient();
                            if (this.txtPatientFName.ReadOnly && (event2.SZ_PATIENT_ID != null))
                            {
                                caseIdForDocumentPath = patient.GetCaseIdForDocumentPath(event2.SZ_PATIENT_ID);
                            }
                            result = patient.AddVisit(operation, tdao, list, event2, list2, this.txtUserId.Text, str12, this.reffCompanyID.Value.ToString(), this.txtCompanyID.Text, this.cmbReferringDoctor.SelectedItem.Value.ToString());
                            if (result.msg_code == "SUCCESS")
                            {
                                str = patient.GetCaseIdForDocumentPath(result.sz_patient_id);
                            }
                            if (caseIdForDocumentPath != "")
                            {
                                string str13 = new Bill_Sys_NF3_Template().getPhysicalPath();
                                DataSet nodeIdForCopyDocument = patient.GetNodeIdForCopyDocument(this.reffCompanyID.Value.ToString());
                                if (((nodeIdForCopyDocument != null) && (nodeIdForCopyDocument.Tables.Count > 0)) && (nodeIdForCopyDocument.Tables[0].Rows.Count > 0))
                                {
                                    for (int m = 0; m < nodeIdForCopyDocument.Tables[0].Rows.Count; m++)
                                    {
                                        string str14 = nodeIdForCopyDocument.Tables[0].Rows[m]["SZ_NODE_TYPE"].ToString();
                                        string str15 = patient.GetSourcePath(this.txtCompanyID.Text, str14, caseIdForDocumentPath);
                                        string destPath = patient.GetDestPath(this.reffCompanyID.Value.ToString(), str);
                                        if (str15 != "")
                                        {
                                            string sourceDirName = str13 + str15;
                                            string destDirName = str13 + destPath + "/" + str15;
                                            DirectoryCopy(sourceDirName, destDirName);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            result = transaction.fnc_SaveAppointment(operation, teo, list, event2, list2, this.txtUserId.Text);
                        }
                        if (result.msg_code == "SUCCESS")
                        {
                            DAO_NOTES_EO dao_notes_eo = new DAO_NOTES_EO();
                            dao_notes_eo.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                            dao_notes_eo.SZ_ACTIVITY_DESC = "Date : " + str9;
                            dao_notes_eo.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            dao_notes_eo.SZ_CASE_ID = this.txtCaseID.Text;
                            dao_notes_eo.SZ_COMPANY_ID = this.txtCompanyID.Text;
                            new DAO_NOTES_BO().SaveActivityNotes(dao_notes_eo);
                        }
                    }
                    if (this.hdnEventID.Value.ToString() != "")
                    {
                        this.Session["PopUp"] = null;
                    }
                    else
                    {
                        this.Session["PopUp"] = "True";
                    }
                }
                if (result.msg_code == "SUCCESS")
                {
                    string str19 = "Appointment.aspx?appointmentDate=" + this.hdnAppDate.Value.ToString() + "&interval=" + this.Session["INTERVAL"].ToString() + "&reffFacility=" + this.reffCompanyID.Value.ToString();
                    this.lblMsg.Text = Convert.ToString("Appointemnt is saved successfully");
                    this.lblMsg.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "alert('" + Convert.ToString("Appointemnt is saved successfully") + "');window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='" + str19 + "';", true);
                    this.hdnReturnOpration.Value = "refresh";
                    this.hdnReturnPath.Value = str19;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('" + result.msg + "')", true);
                    this.lblMsg.Text = Convert.ToString("Error occurred while saving appointment. Please contact admin.");
                    this.lblMsg.Visible = true;
                }
                this.ClearValues();
                this.txtPatientExistMsg.Value = "";
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SearchPatientCallBackList()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            DataTable table = new DataTable();
            this.ds = new DataSet();
            this.hdnPatientFirstName.Value.ToString();
            this.hdnPatientLastName.Value.ToString();
            this.ds = new OutSchedule().GetPatientDataListNEW(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            table = this.ds.Tables[0].Clone();
            if (this.ds.Tables[0].Rows.Count > 0)
            {
                if ((this.Session["Flag"] != null) || (this.Session["DataEntryFlag"] != null))
                {
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;")
                    {
                        this.txtPatientID.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        this.txtPatientID.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;")
                    {
                        this.txtPatientFName.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    }
                    else
                    {
                        this.txtPatientFName.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;")
                    {
                        this.txtMI.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                    }
                    else
                    {
                        this.txtMI.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;")
                    {
                        this.txtPatientLName.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    }
                    else
                    {
                        this.txtPatientLName.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;")
                    {
                        this.txtPatientPhone.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    }
                    else
                    {
                        this.txtPatientPhone.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;")
                    {
                        this.txtPatientAddress.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    }
                    else
                    {
                        this.txtPatientAddress.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x11).ToString() != "&nbsp;")
                    {
                        this.txtBirthdate.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x11).ToString();
                    }
                    else
                    {
                        this.txtBirthdate.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;")
                    {
                        this.txtPatientAge.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    }
                    else
                    {
                        this.txtPatientAge.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x10).ToString() != "&nbsp;")
                    {
                        this.txtSocialSecurityNumber.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x10).ToString();
                    }
                    else
                    {
                        this.txtSocialSecurityNumber.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "&nbsp;")
                    {
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() == "True")
                        {
                            this.chkTransportation.Checked = true;
                        }
                        else
                        {
                            this.chkTransportation.Checked = false;
                        }
                    }
                    if (this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString() != "&nbsp;")
                    {
                        this.cmbState.Value = this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();
                    }
                    else
                    {
                        this.cmbState.Text = "NA";
                    }
                    if (this.ds.Tables[0].Rows[0]["CHART NUMBER"].ToString() != "&nbsp;")
                    {
                        this.txtRefChartNumber.Text = this.ds.Tables[0].Rows[0]["CHART NUMBER"].ToString();
                    }
                    else
                    {
                        this.txtRefChartNumber.Text = "";
                    }
                    DataSet caseDetails = new Billing_Sys_ManageNotesBO().GetCaseDetails(this.txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    if (caseDetails.Tables[0].Rows.Count > 0)
                    {
                        this.txtCaseID.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        this.hdnCaseID.Value = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                        {
                            OutSchedule schedule2 = new OutSchedule();
                            this.cmbInsurance.TextField = "DESCRIPTION";
                            this.cmbInsurance.ValueField = "CODE";
                            this.cmbInsurance.DataSource = schedule2.GetInsuranceCompany(caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                            this.cmbInsurance.DataBind();
                            this.cmbInsurance.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                            this.cmbInsurance.Value = "NA";
                            this.cmbCaseType.TextField = "DESCRIPTION";
                            this.cmbCaseType.ValueField = "CODE";
                            this.cmbCaseType.DataSource = schedule2.GetCaseType(caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                            this.cmbCaseType.DataBind();
                            this.cmbCaseType.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                            this.cmbCaseType.Value = "NA";
                        }
                        if (caseDetails.Tables[0].Rows[0]["SZ_OFFICE_ID"].ToString() != "")
                        {
                            this.cmbMedicalOffice.Value = caseDetails.Tables[0].Rows[0]["SZ_OFFICE_ID"].ToString();
                            this.cmbMedicalOffice.Enabled = false;
                            OutSchedule schedule3 = new OutSchedule();
                            this.cmbReferringDoctor.TextField = "DESCRIPTION";
                            this.cmbReferringDoctor.ValueField = "CODE";
                            this.cmbReferringDoctor.DataSource = schedule3.GetReferringDoctorByOffice(caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x15).ToString());
                            this.cmbReferringDoctor.DataBind();
                            this.cmbReferringDoctor.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                            this.cmbReferringDoctor.Value = "NA";
                        }
                        else
                        {
                            this.cmbMedicalOffice.Enabled = true;
                        }
                        this.cmbCaseType.Value = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        this.cmbInsurance.Value = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    }
                }
                else
                {
                    if ((this.txtPatientFirstName.Text != "") && (this.txtPatientLastName.Text == ""))
                    {
                        DataRow[] rowArray = this.ds.Tables[0].Select("SZ_PATIENT_FIRST_NAME LIKE '" + this.txtPatientFirstName.Text + "%'");
                        for (int i = 0; i < rowArray.Length; i++)
                        {
                            table.ImportRow(rowArray[i]);
                        }
                    }
                    else if ((this.txtPatientLastName.Text != "") && (this.txtPatientFirstName.Text == ""))
                    {
                        DataRow[] rowArray2 = this.ds.Tables[0].Select("SZ_PATIENT_LAST_NAME LIKE '" + this.txtPatientLastName.Text + "%'");
                        for (int j = 0; j < rowArray2.Length; j++)
                        {
                            table.ImportRow(rowArray2[j]);
                        }
                    }
                    else if ((this.txtPatientLastName.Text != "") && (this.txtPatientFirstName.Text != ""))
                    {
                        DataRow[] rowArray3 = this.ds.Tables[0].Select("SZ_PATIENT_FIRST_NAME LIKE '" + this.txtPatientFirstName.Text + "%' AND SZ_PATIENT_LAST_NAME LIKE '" + this.txtPatientLastName.Text + "%'");
                        for (int k = 0; k < rowArray3.Length; k++)
                        {
                            table.ImportRow(rowArray3[k]);
                        }
                    }
                    if (table.Rows.Count > 0)
                    {
                        if (!(this.Session["Page_Index"].ToString() != ""))
                        {
                            this.grdPatientList_.DataSource = table;
                            this.grdPatientList_.DataBind();
                        }
                    }
                    else
                    {
                        this.Session["dtView"] = "";
                    }
                }
            }
            this.grdPatientList_.Visible = true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SearchPatientList()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            DataTable table = new DataTable();
            this.ds = new DataSet();
            this.ds = (DataSet)this.Session["PatientDataList"];
            table = this.ds.Tables[0].Clone();
            if (this.ds.Tables[0].Rows.Count > 0)
            {
                if ((this.Session["Flag"] != null) || (this.Session["DataEntryFlag"] != null))
                {
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;")
                    {
                        this.txtPatientID.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        this.txtPatientID.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;")
                    {
                        this.txtPatientFName.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    }
                    else
                    {
                        this.txtPatientFName.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;")
                    {
                        this.txtMI.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                    }
                    else
                    {
                        this.txtMI.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;")
                    {
                        this.txtPatientLName.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    }
                    else
                    {
                        this.txtPatientLName.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;")
                    {
                        this.txtPatientPhone.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    }
                    else
                    {
                        this.txtPatientPhone.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;")
                    {
                        this.txtPatientAddress.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    }
                    else
                    {
                        this.txtPatientAddress.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x11).ToString() != "&nbsp;")
                    {
                        this.txtBirthdate.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x11).ToString();
                    }
                    else
                    {
                        this.txtBirthdate.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;")
                    {
                        this.txtPatientAge.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    }
                    else
                    {
                        this.txtPatientAge.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x10).ToString() != "&nbsp;")
                    {
                        this.txtSocialSecurityNumber.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0x10).ToString();
                    }
                    else
                    {
                        this.txtSocialSecurityNumber.Text = "";
                    }
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "&nbsp;")
                    {
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() == "True")
                        {
                            this.chkTransportation.Checked = true;
                        }
                        else
                        {
                            this.chkTransportation.Checked = false;
                        }
                    }
                    if (this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString() != "&nbsp;")
                    {
                        this.cmbState.Value = this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();
                    }
                    else
                    {
                        this.cmbState.Text = "NA";
                    }
                    if (this.ds.Tables[0].Rows[0]["CHART NUMBER"].ToString() != "&nbsp;")
                    {
                        this.txtRefChartNumber.Text = this.ds.Tables[0].Rows[0]["CHART NUMBER"].ToString();
                    }
                    else
                    {
                        this.txtRefChartNumber.Text = "";
                    }
                    DataSet caseDetails = new Billing_Sys_ManageNotesBO().GetCaseDetails(this.txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    if (caseDetails.Tables[0].Rows.Count > 0)
                    {
                        this.txtCaseID.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        this.hdnCaseID.Value = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                        {
                            OutSchedule schedule = new OutSchedule();
                            this.cmbInsurance.TextField = "DESCRIPTION";
                            this.cmbInsurance.ValueField = "CODE";
                            this.cmbInsurance.DataSource = schedule.GetInsuranceCompany(caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                            this.cmbInsurance.DataBind();
                            this.cmbInsurance.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                            this.cmbInsurance.Value = "NA";
                            this.cmbCaseType.TextField = "DESCRIPTION";
                            this.cmbCaseType.ValueField = "CODE";
                            this.cmbCaseType.DataSource = schedule.GetCaseType(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            this.cmbCaseType.DataBind();
                            this.cmbCaseType.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                            this.cmbCaseType.Value = "NA";
                        }
                        if (caseDetails.Tables[0].Rows[0]["SZ_OFFICE_ID"].ToString() != "")
                        {
                            this.cmbMedicalOffice.Value = caseDetails.Tables[0].Rows[0]["SZ_OFFICE_ID"].ToString();
                            this.cmbMedicalOffice.Enabled = false;
                            OutSchedule schedule2 = new OutSchedule();
                            this.cmbReferringDoctor.TextField = "DESCRIPTION";
                            this.cmbReferringDoctor.ValueField = "CODE";
                            this.cmbReferringDoctor.DataSource = schedule2.GetReferringDoctorByOffice(caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x15).ToString());
                            this.cmbReferringDoctor.DataBind();
                            this.cmbReferringDoctor.Items.Insert(0, new DevExpress.Web.ListEditItem("---Select---", "NA"));
                            this.cmbReferringDoctor.Value = "NA";
                        }
                        else
                        {
                            this.cmbMedicalOffice.Enabled = true;
                        }
                        this.cmbCaseType.Value = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        this.cmbInsurance.Value = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    }
                }
                else
                {
                    if ((this.txtPatientFirstName.Text != "") && (this.txtPatientLastName.Text == ""))
                    {
                        DataRow[] rowArray = this.ds.Tables[0].Select("SZ_PATIENT_FIRST_NAME LIKE '" + this.txtPatientFirstName.Text + "%'");
                        for (int i = 0; i < rowArray.Length; i++)
                        {
                            table.ImportRow(rowArray[i]);
                        }
                    }
                    else if ((this.txtPatientLastName.Text != "") && (this.txtPatientFirstName.Text == ""))
                    {
                        DataRow[] rowArray2 = this.ds.Tables[0].Select("SZ_PATIENT_LAST_NAME LIKE '" + this.txtPatientLastName.Text + "%'");
                        for (int j = 0; j < rowArray2.Length; j++)
                        {
                            table.ImportRow(rowArray2[j]);
                        }
                    }
                    else if ((this.txtPatientLastName.Text != "") && (this.txtPatientFirstName.Text != ""))
                    {
                        DataRow[] rowArray3 = this.ds.Tables[0].Select("SZ_PATIENT_FIRST_NAME LIKE '" + this.txtPatientFirstName.Text + "%' AND SZ_PATIENT_LAST_NAME LIKE '" + this.txtPatientLastName.Text + "%'");
                        for (int k = 0; k < rowArray3.Length; k++)
                        {
                            table.ImportRow(rowArray3[k]);
                        }
                    }
                    if (table.Rows.Count <= 0)
                    {
                        this.Session["dtView"] = "";
                    }
                }
            }
            new Bill_Sys_BillingCompanyDetails_BO();
            if (this.reffOfficeID.Value.ToString() != "")
            {
                this.btnAddPatient.Visible = false;
            }
            else
            {
                this.btnAddPatient.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void SelectSavedProcedureCodes(string i_schedule_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["BILLED_EVENT"] = "";
            this.Session["VISIT_COMPLETED"] = "";
            this.ds = new DataSet();
            this._patient_TVBO = new Patient_TVBO();
            this.ds = this._patient_TVBO.GetAppointProcCode(Convert.ToInt32(i_schedule_id));
            DataSet dataSource = new DataSet();
            dataSource = (DataSet)this.grdProcedureCode.DataSource;
            DataSet set2 = new DataSet();
            DataTable table = new DataTable();
            set2.Tables.Add(table);
            set2.Tables[0].Columns.Add("CODE");
            set2.Tables[0].Columns.Add("DESCRIPTION");
            set2.Tables[0].Columns.Add("I_RESCHEDULE_ID");
            set2.Tables[0].Columns.Add("I_EVENT_PROC_ID");
            foreach (DataRow row in this.ds.Tables[0].Rows)
            {
                for (int i = 0; i < dataSource.Tables[0].Rows.Count; i++)
                {
                    if (dataSource.Tables[0].Rows[i][0].ToString().Equals(row.ItemArray.GetValue(0).ToString()))
                    {
                        DataRow row2 = set2.Tables[0].NewRow();
                        row2["CODE"] = dataSource.Tables[0].Rows[i][0].ToString();
                        row2["DESCRIPTION"] = dataSource.Tables[0].Rows[i][1].ToString();
                        row2["I_RESCHEDULE_ID"] = dataSource.Tables[0].Rows[i][2].ToString();
                        row2["I_EVENT_PROC_ID"] = dataSource.Tables[0].Rows[i][3].ToString();
                        set2.Tables[0].Rows.Add(row2);
                        dataSource.Tables[0].Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            set2.Tables[0].Merge(dataSource.Tables[0]);
            this.grdProcedureCode.DataSource = set2.Tables[0];
            this.grdProcedureCode.DataBind();
            foreach (DataRow row3 in this.ds.Tables[0].Rows)
            {
                foreach (DataGridItem item in this.grdProcedureCode.Items)
                {
                    if (item.Cells[1].Text == row3.ItemArray.GetValue(0).ToString())
                    {
                        CheckBox box = (CheckBox)item.Cells[1].FindControl("chkSelect");
                        box.Checked = true;
                        DropDownList list = (DropDownList)item.Cells[4].FindControl("ddlStatus");
                        list.SelectedValue = row3.ItemArray.GetValue(1).ToString();
                        TextBox box2 = (TextBox)item.Cells[7].FindControl("txtStudyNo");
                        box2.Text = row3.ItemArray.GetValue(7).ToString();
                        TextBox box3 = (TextBox)item.FindControl("txtProcNotes");
                        box3.Text = row3.ItemArray.GetValue(8).ToString();
                        if (list.SelectedValue == "2")
                        {
                            this.lblMsg2.Visible = true;
                            this.lblMsg2.Text = "Visit completed.";
                            this.lblMsg2.ForeColor = System.Drawing.Color.Red;
                            this.Session["VISIT_COMPLETED"] = "YES";
                        }
                        if ((row3.ItemArray.GetValue(2) != DBNull.Value) && (Convert.ToInt32(row3.ItemArray.GetValue(1).ToString()) != 0))
                        {
                            TextBox box4 = (TextBox)item.Cells[5].FindControl("txtReScheduleDate");
                            box4.Text = row3.ItemArray.GetValue(2).ToString();
                            box4.ReadOnly = true;
                            string str = row3.ItemArray.GetValue(3).ToString();
                            DropDownList list2 = (DropDownList)item.Cells[6].FindControl("ddlReSchHours");
                            list2.SelectedValue = str.Substring(0, str.IndexOf(".")).PadLeft(2, '0');
                            list2.Enabled = false;
                            DropDownList list3 = (DropDownList)item.Cells[6].FindControl("ddlReSchMinutes");
                            list3.SelectedValue = str.Substring(str.IndexOf(".") + 1, str.Length - (str.IndexOf(".") + 1)).PadLeft(2, '0');
                            list3.Enabled = false;
                            DropDownList list4 = (DropDownList)item.Cells[6].FindControl("ddlReSchTime");
                            list4.SelectedValue = row3.ItemArray.GetValue(4).ToString();
                            list4.Enabled = false;
                            list.Enabled = false;
                            box.Enabled = false;
                            box2.ReadOnly = true;
                            box3.ReadOnly = true;
                        }
                        item.BackColor = System.Drawing.Color.LightSeaGreen;
                        if (row3.ItemArray.GetValue(9).ToString() == "True")
                        {
                            this.Session["BILLED_EVENT"] = "BILLED";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void setControlAccordingOperation()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            if (this.hdnEventID.Value.ToString() != "")
            {
                if (this.reffCompanyID.Value.ToString() == this.txtCompanyID.Text.ToString())
                {
                    this.DisplayProcedureGridColumns(true);
                    this.cmbMedicalOffice.Visible = true;
                    this.cmbReferringFacility.Visible = false;
                    this.lblTestFacility.Text = "Office Name";
                    this.btnUpdate.Visible = true;
                    if ((((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "1") || (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "True"))
                    {
                        this.btnDeleteEvent.Visible = true;
                    }
                    else
                    {
                        this.btnDeleteEvent.Visible = false;
                    }
                    this.btnSave.Visible = false;
                    this.btnDuplicateSaveClick.Visible = false;
                    this.grdProcedureCode.Visible = true;
                    this.ddlTestNames.Visible = false;
                    this.divProcedureCode.Visible = true;
                    this.divProcedureCode.Style.Add("HEIGHT", "250px");
                    this.divProcedureCode.Style.Add("OVERFLOW", "scroll");
                    this.divProcedureCode.Style.Add("WIDTH", "100%");
                    this.lblSSN.Visible = true;
                    this.txtSocialSecurityNumber.Visible = true;
                    this.lblBirthdate.Visible = true;
                    this.txtBirthdate.Visible = true;
                    this.lblAge.Visible = true;
                    this.txtPatientAge.Visible = true;
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "0")
                    {
                        this.lblChartNumber.Visible = true;
                        this.txtRefChartNumber.Visible = true;
                    }
                    this.txtRefChartNumber.ReadOnly = true;
                    this.txtPatientFName.ReadOnly = true;
                    this.txtMI.ReadOnly = true;
                    this.txtPatientLName.ReadOnly = true;
                    this.txtPatientPhone.ReadOnly = true;
                    this.txtPatientAddress.ReadOnly = true;
                    this.TextBox3.ReadOnly = true;
                    this.txtState.ReadOnly = true;
                    this.txtBirthdate.ReadOnly = true;
                    this.txtPatientAge.ReadOnly = true;
                    this.txtSocialSecurityNumber.ReadOnly = true;
                    this.cmbInsurance.Enabled = false;
                    this.cmbCaseType.Enabled = false;
                    if (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.Trim().ToLower().ToString() == "referring office")
                    {
                        new Bill_Sys_BillingCompanyDetails_BO();
                        if (this.reffOfficeID.Value.ToString() != "")
                        {
                            this.cmbMedicalOffice.Enabled = false;
                        }
                        else
                        {
                            this.cmbMedicalOffice.Enabled = true;
                        }
                    }
                    else
                    {
                        this.cmbMedicalOffice.Enabled = true;
                    }
                    this.cmbState.Enabled = false;
                    this.btnUpdate.Attributes.Add("Onclick", "return val_updateTestFacility();");
                    if (this.Session["BILLED_EVENT"].ToString() == "BILLED")
                    {
                        this.lblMsg.Text = "Bill generated for selected appointment.";
                        this.btnUpdate.Visible = false;
                        this.btnDeleteEvent.Visible = false;
                        this.Session["BILLED_EVENT"] = "";
                    }
                }
                else if (this.reffCompanyID.Value.ToString() != this.txtCompanyID.Text.ToString())
                {
                    this.DisplayProcedureGridColumns(false);
                    this.btnSave.Visible = false;
                    this.btnDuplicateSaveClick.Visible = false;
                    this.btnUpdate.Visible = true;
                    if ((((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "1") || (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "True"))
                    {
                        this.btnDeleteEvent.Visible = true;
                    }
                    else
                    {
                        this.btnDeleteEvent.Visible = false;
                    }
                    this.lblSSN.Visible = false;
                    this.txtSocialSecurityNumber.Visible = false;
                    this.lblBirthdate.Visible = false;
                    this.txtBirthdate.Visible = false;
                    this.lblAge.Visible = false;
                    this.txtPatientAge.Visible = false;
                    this.lblChartNumber.Visible = false;
                    this.txtRefChartNumber.Visible = false;
                    this.cmbState.Enabled = false;
                    this.btnUpdate.Attributes.Add("Onclick", "return Val_AddVisitForTestFacility();");
                    if ((this.Session["VISIT_COMPLETED"] != null) && (this.Session["VISIT_COMPLETED"].ToString() == "YES"))
                    {
                        this.btnUpdate.Visible = false;
                        this.btnDeleteEvent.Visible = false;
                        this.Session["VISIT_COMPLETED"] = "";
                    }
                }
            }
            else
            {
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    this.btnDuplicateSaveClick.Attributes.Add("Onclick", "return Val_AddVisitForTestFacility();");
                    this.cmbMedicalOffice.Visible = true;
                    if (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.Trim().ToLower().ToString() == "referring office")
                    {
                        new Bill_Sys_BillingCompanyDetails_BO();
                        if (this.reffOfficeID.Value.ToString() != "")
                        {
                            this.cmbMedicalOffice.Enabled = false;
                        }
                        else
                        {
                            this.cmbMedicalOffice.Enabled = true;
                        }
                    }
                    else
                    {
                        this.cmbMedicalOffice.Enabled = true;
                    }
                    this.cmbReferringFacility.Visible = false;
                    this.lblTypetext.Visible = false;
                    this.ddlType.Visible = false;
                    this.lblTestFacility.Text = "Office Name";
                    if (!(this.cmbMedicalOffice.Text != "NA") && !(this.cmbMedicalOffice.Text != ""))
                    {
                        this.cmbMedicalOffice.Text = "NA";
                    }
                    this.cmbReferringDoctor.Text = "NA";
                    this.cmbTransport.Text = "NA";
                    this.chkTransportation.Checked = false;
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "0")
                    {
                        this.lblChartNumber.Visible = true;
                        this.txtRefChartNumber.Visible = true;
                    }
                }
                else
                {
                    this.btnDuplicateSaveClick.Attributes.Add("Onclick", "return Val_Add_UpdateVisitForBillingCompany();");
                    this.cmbMedicalOffice.Visible = false;
                    this.cmbReferringFacility.Visible = true;
                    this.lblTestFacility.Text = "Test Facility";
                    this.lblChartNumber.Visible = false;
                    this.txtRefChartNumber.Visible = false;
                }
                this.DisplayProcedureGridColumns(false);
                this.lblSSN.Visible = false;
                this.txtSocialSecurityNumber.Visible = false;
                this.lblBirthdate.Visible = false;
                this.txtBirthdate.Visible = false;
                this.lblAge.Visible = false;
                this.txtPatientAge.Visible = false;
                this.txtNotes.Text = "";
                this.btnUpdate.Visible = false;
                this.btnDeleteEvent.Visible = false;
                this.btnSave.Visible = true;
                this.btnDuplicateSaveClick.Visible = true;
                this.txtRefChartNumber.ReadOnly = true;
                this.txtPatientFName.ReadOnly = true;
                this.txtMI.ReadOnly = true;
                this.txtPatientLName.ReadOnly = true;
                this.txtPatientPhone.ReadOnly = true;
                this.txtPatientAddress.ReadOnly = true;
                this.TextBox3.ReadOnly = true;
                this.txtState.ReadOnly = true;
                this.txtBirthdate.ReadOnly = true;
                this.txtPatientAge.ReadOnly = true;
                this.txtSocialSecurityNumber.ReadOnly = true;
                this.cmbInsurance.Enabled = false;
                this.cmbCaseType.Enabled = false;
                this.cmbState.Enabled = false;
                if (this.txtPatientFName.Text == "")
                {
                    this.btnClickSearch.Visible = true;
                    this.tdSerach.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void updateAppointmentFromBillingCompany()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            string text = "";
            if (this.txtPatientFName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Patient First Name should not be Empty.')", true);
                this.lblMsg.Text = Convert.ToString("Patient First Name should not be Empty");
                this.lblMsg.Visible = true;
            }
            else if (this.txtPatientLName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Patient Last name should not be Empty.')", true);
                this.lblMsg.Text = Convert.ToString("Patient Last name should not be Empty.");
                this.lblMsg.Visible = true;
            }
            else if (this.ddlType.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please Select any one Type.')", true);
                this.lblMsg.Text = Convert.ToString("Please Select any one Type.");
                this.lblMsg.Visible = true;
            }
            else if (this.cmbReferringDoctor.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please Select any one Referring Doctor.')", true);
                this.lblMsg.Text = Convert.ToString("Please Select any one Referring Doctor.");
                this.lblMsg.Visible = true;
            }
            else if (this.chkTransportation.Checked && (this.cmbTransport.Text == "NA"))
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('please select transport company.')", true);
                this.lblMsg.Text = Convert.ToString("schedule.appointment.transport.validation");
                this.lblMsg.Visible = true;
            }
            else
            {
                int num = Convert.ToInt32(this.Session["SCHEDULEDID"].ToString());
                string str2 = this.hdnAppDate.Value.ToString();
                Bill_Sys_Calender calender = new Bill_Sys_Calender();
                ArrayList list = new ArrayList();
                list.Add(this.txtPatientID.Text);
                list.Add(str2);
                list.Add(this.ddlHours.SelectedValue.ToString() + "." + this.ddlMinutes.SelectedValue.ToString());
                list.Add(this.txtNotes.Text);
                list.Add(this.cmbReferringDoctor.Value.ToString());
                text = "";
                int num2 = 0;
                while (num2 < this.grdProcedureCode.Items.Count)
                {
                    text = this.grdProcedureCode.Items[num2].Cells[1].Text;
                    break;
                }
                if (text != null)
                {
                    list.Add(text);
                }
                else
                {
                    list.Add("");
                }
                list.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                list.Add(this.ddlTime.SelectedValue);
                list.Add(this.ddlEndHours.SelectedValue.ToString() + "." + this.ddlEndMinutes.SelectedValue.ToString());
                list.Add(this.ddlEndTime.SelectedValue);
                list.Add(this.cmbReferringFacility.Value.ToString());
                list.Add(num);
                if (this.chkTransportation.Checked)
                {
                    list.Add(1);
                }
                else
                {
                    list.Add(0);
                }
                if (this.chkTransportation.Checked && (this.cmbTransport.Text != "NA"))
                {
                    list.Add(Convert.ToInt32(this.cmbTransport.Value.ToString()));
                }
                else
                {
                    list.Add(null);
                }
                DateTime time = new DateTime();
                time = Convert.ToDateTime(str2);
                if (this.ddlTime.SelectedValue == "AM")
                {
                    new DateTime(time.Year, time.Month, time.Day, Convert.ToInt32(this.ddlHours.SelectedValue), Convert.ToInt32(this.ddlEndMinutes.SelectedValue), 0);
                }
                else
                {
                    int hour = 0;
                    if (Convert.ToInt32(this.ddlHours.SelectedValue) == 12)
                    {
                        hour = Convert.ToInt32(this.ddlHours.SelectedValue);
                    }
                    else
                    {
                        hour = Convert.ToInt32(this.ddlHours.SelectedValue) + 12;
                    }
                    new DateTime(time.Year, time.Month, time.Day, hour, Convert.ToInt32(this.ddlEndMinutes.SelectedValue), 0);
                }
                num = calender.UPDATE_Event_Referral(list, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                calender.Delete_Event_RefferPrcedure(num);
                for (int i = 0; i < this.grdProcedureCode.Items.Count; i++)
                {
                    CheckBox box = (CheckBox)this.grdProcedureCode.Items[i].FindControl("chkSelect");
                    if (box.Checked)
                    {
                        text = this.grdProcedureCode.Items[i].Cells[1].Text;
                        list = new ArrayList();
                        list.Add(text);
                        list.Add(num);
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            list.Add(2);
                        }
                        else
                        {
                            list.Add(0);
                        }
                        calender.Save_Event_RefferPrcedure(list);
                    }
                }
                DAO_NOTES_EO dao_notes_eo = new DAO_NOTES_EO();
                dao_notes_eo.SZ_MESSAGE_TITLE = "APPOINTMENT_UPDATED";
                dao_notes_eo.SZ_ACTIVITY_DESC = "Date : " + str2;
                DAO_NOTES_BO dao_notes_bo = new DAO_NOTES_BO();
                dao_notes_eo.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                dao_notes_eo.SZ_CASE_ID = this.txtCaseID.Text;
                dao_notes_eo.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                dao_notes_bo.SaveActivityNotes(dao_notes_eo);
                this.Session["PopUp"] = "True";
                if (this.hdnAppDate.Value.ToString() != "")
                {
                    string str4 = "Appointment.aspx?appointmentDate=" + this.hdnAppDate.Value.ToString() + "&interval=" + this.Session["INTERVAL"].ToString() + "&reffFacility=" + this.reffCompanyID.Value.ToString();
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "alert('Appointment saved successfully.');window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='" + str4 + "';", true);
                    this.hdnReturnOpration.Value = "refresh";
                    this.hdnReturnPath.Value = str4;
                }
                this.ClearValues();
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Appointment updated successfully.')", true);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void updateAppointmentFromTestFacility()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        int num = 0;
        try
        {
            int num2;
            Bill_Sys_Calender calender;
            ArrayList list7;
            ArrayList list8;
            bool flag = false;
            if (this.chkTransportation.Checked && (this.cmbTransport.Text == "NA"))
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('please select transport company.')", true);
            }
            else
            {
                foreach (DataGridItem item in this.grdProcedureCode.Items)
                {
                    CheckBox box = (CheckBox)item.Cells[1].FindControl("chkSelect");
                    if (box.Checked)
                    {
                        DropDownList list = (DropDownList)item.Cells[4].FindControl("ddlStatus");
                        if (list.SelectedValue != "0")
                        {
                            num = 1;
                        }
                    }
                }
                if (num == 1)
                {
                    foreach (DataGridItem item2 in this.grdProcedureCode.Items)
                    {
                        CheckBox box2 = (CheckBox)item2.Cells[1].FindControl("chkSelect");
                        if (box2.Checked)
                        {
                            DropDownList list2 = (DropDownList)item2.Cells[4].FindControl("ddlStatus");
                            if (list2.SelectedValue == "0")
                            {
                                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please select procedure status.')", true);
                                return;
                            }
                            if (Convert.ToInt32(list2.SelectedValue) == 1)
                            {
                                TextBox box3 = (TextBox)item2.Cells[5].FindControl("txtReScheduleDate");
                                if (box3.Text == "")
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please enter valid rescheduled date.')", true);
                                    return;
                                }
                                try
                                {
                                    DateTime.Parse(box3.Text);
                                }
                                catch (Exception ex)
                                {
                                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                                    using (Utils utility = new Utils())
                                    {
                                        utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                                    }
                                    string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
                                    base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

                                }
                                DropDownList list3 = (DropDownList)item2.Cells[6].FindControl("ddlReSchHours");
                                DropDownList list4 = (DropDownList)item2.Cells[6].FindControl("ddlReSchMinutes");
                                DropDownList list5 = (DropDownList)item2.Cells[6].FindControl("ddlReSchTime");
                                if (list3.SelectedValue == "00")
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please enter valid rescheduled time.')", true);
                                    return;
                                }
                                DateTime time = Convert.ToDateTime(Convert.ToDateTime(box3.Text).ToString("MM/dd/yyyy") + " " + list3.SelectedValue + ":" + list4.SelectedValue + " " + list5.SelectedValue);
                                DateTime time2 = time.AddMinutes(30.0);
                                string str = this.roomID.Value.ToString();
                                Bill_Sys_RoomDays days = new Bill_Sys_RoomDays();
                                ArrayList list6 = new ArrayList();
                                list6.Add(str);
                                list6.Add(Convert.ToDateTime(box3.Text).ToString("MM/dd/yyyy"));
                                list6.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list6.Add(time.Hour.ToString() + "." + time.Minute.ToString());
                                list6.Add(time2.Hour.ToString() + "." + time2.Minute.ToString());
                                if (!days.checkRoomTiming(list6))
                                {
                                    string str2 = days.getRoomStart_EndTime(str, Convert.ToDateTime(box3.Text).ToString("MM/dd/yyyy"), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please add visit on " + Convert.ToDateTime(box3.Text).ToString("MM/dd/yyyy") + " between " + str2 + ".')", true);
                                    return;
                                }
                            }
                        }
                    }
                }
                if (!flag)
                {
                    num2 = Convert.ToInt32(this.Session["SCHEDULEDID"].ToString());
                    calender = new Bill_Sys_Calender();
                    if ((!this.cmbMedicalOffice.Visible || !(this.cmbMedicalOffice.Text != "")) || !(this.cmbMedicalOffice.Text != "NA"))
                    {
                        goto Label_061D;
                    }
                    list7 = new ArrayList();
                    list7.Add(this.txtPatientID.Text);
                    list7.Add(this.cmbMedicalOffice.SelectedItem.Value.ToString());
                    list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    if (Convert.ToString(this.cmbReferringDoctor.Value.ToString()) != "NA")
                    {
                        list7.Add(Convert.ToString(this.cmbReferringDoctor.Value));
                        goto Label_0613;
                    }
                    this.lblMsg.Text = "Please select referring doctor.";
                    this.lblMsg.Visible = true;
                }
            }
            return;
        Label_0613:
            calender.Update_Office_Id(list7);
        Label_061D:
            list8 = new ArrayList();
            calender.Delete_Event_RefferPrcedure(num2);
            ArrayList list9 = new ArrayList();
            foreach (DataGridItem item3 in this.grdProcedureCode.Items)
            {
                CheckBox box4 = (CheckBox)item3.Cells[1].FindControl("chkSelect");
                if (box4.Checked)
                {
                    TextBox box5 = (TextBox)item3.Cells[5].FindControl("txtStudyNo");
                    TextBox box6 = (TextBox)item3.Cells[8].FindControl("txtProcNotes");
                    DropDownList list10 = (DropDownList)item3.Cells[4].FindControl("ddlStatus");
                    list8 = new ArrayList();
                    list8.Add(item3.Cells[1].Text);
                    list8.Add(num2);
                    list8.Add(list10.SelectedValue);
                    int eventID = 0;
                    if (Convert.ToInt32(list10.SelectedValue) == 1)
                    {
                        TextBox box7 = (TextBox)item3.Cells[5].FindControl("txtReScheduleDate");
                        DropDownList list11 = (DropDownList)item3.Cells[6].FindControl("ddlReSchHours");
                        DropDownList list12 = (DropDownList)item3.Cells[6].FindControl("ddlReSchMinutes");
                        DropDownList list13 = (DropDownList)item3.Cells[6].FindControl("ddlReSchTime");
                        if (list9.Count > 0)
                        {
                            foreach (AddedEvetDetail detail2 in list9)
                            {
                                if ((detail2.EventDate == Convert.ToDateTime(box7.Text)) && (detail2.EventTime == Convert.ToDecimal(list11.SelectedValue.ToString() + "." + list12.SelectedValue.ToString())))
                                {
                                    eventID = detail2.EventID;
                                }
                            }
                        }
                        if ((eventID == 0) && box4.Enabled)
                        {
                            Bill_Sys_Calender calender2 = new Bill_Sys_Calender();
                            ArrayList list14 = new ArrayList();
                            list14.Add(this.txtPatientID.Text);
                            list14.Add(box7.Text);
                            list14.Add(list11.SelectedValue.ToString() + "." + list12.SelectedValue.ToString());
                            list14.Add(this.txtNotes.Text);
                            list14.Add(this.cmbReferringDoctor.SelectedItem.Value.ToString());
                            list14.Add(item3.Cells[1].Text);
                            list14.Add(this.txtPatientCompany.Text);
                            list14.Add(list13.SelectedValue);
                            decimal num4 = Convert.ToDecimal(list11.SelectedValue.ToString() + "." + list12.SelectedValue.ToString()) + Convert.ToDecimal(this.Session["INTERVAL"].ToString());
                            string str3 = list13.SelectedValue;
                            if ((Convert.ToDecimal(list11.SelectedValue.ToString() + "." + list12.SelectedValue.ToString()) < Convert.ToDecimal((double)12.0)) && (num4 >= Convert.ToDecimal((double)12.0)))
                            {
                                if (str3 == "AM")
                                {
                                    str3 = "PM";
                                }
                                else if (str3 == "PM")
                                {
                                    str3 = "AM";
                                }
                            }
                            list14.Add(num4);
                            list14.Add(str3);
                            list14.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list14.Add("False");
                            if (this.chkTransportation.Checked)
                            {
                                list14.Add(1);
                            }
                            else
                            {
                                list14.Add(0);
                            }
                            if (this.chkTransportation.Checked)
                            {
                                list14.Add(Convert.ToInt32(this.cmbTransport.SelectedItem.Value.ToString()));
                            }
                            else
                            {
                                list14.Add(0);
                            }
                            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                            {
                                list14.Add(this.cmbMedicalOffice.SelectedItem.Value.ToString());
                            }
                            eventID = calender2.Save_Event(list14, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                            AddedEvetDetail detail = new AddedEvetDetail();
                            detail.EventID = eventID;
                            detail.EventDate = Convert.ToDateTime(box7.Text);
                            detail.EventTime = Convert.ToDecimal(list11.SelectedValue.ToString() + "." + list12.SelectedValue.ToString());
                            list9.Add(detail);
                        }
                        if (eventID != 0)
                        {
                            ArrayList list15 = new ArrayList();
                            list15.Add(item3.Cells[1].Text);
                            list15.Add(eventID);
                            list15.Add(0);
                            calender.Save_Event_RefferPrcedure(list15);
                        }
                        list8.Add(box7.Text);
                        list8.Add(list11.SelectedValue.ToString() + "." + list12.SelectedValue.ToString());
                        list8.Add(list13.SelectedValue);
                        list8.Add(eventID);
                        list8.Add(box5.Text);
                        list8.Add(box6.Text);
                        calender.Save_Event_RefferPrcedure(list8);
                    }
                    else if (Convert.ToInt32(list10.SelectedValue) == 2)
                    {
                        new Bill_Sys_Calender();
                        list8.Add(box5.Text);
                        list8.Add(box6.Text);
                        calender.Save_Event_OtherVType(list8);
                        Bill_Sys_ReferalEvent event2 = new Bill_Sys_ReferalEvent();
                        ArrayList list16 = new ArrayList();
                        list16.Add(this.cmbReferringDoctor.Value.ToString());
                        list16.Add(item3.Cells[1].Text);
                        list16.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list16.Add(item3.Cells[1].Text);
                        event2.AddDoctorAmount(list16);
                    }
                    else
                    {
                        new Bill_Sys_Calender();
                        list8.Add(box5.Text);
                        list8.Add(box6.Text);
                        calender.Save_Event_OtherVType(list8);
                    }
                    item3.BackColor = System.Drawing.Color.LightSeaGreen;
                    ArrayList list17 = new ArrayList();
                    TextBox box8 = (TextBox)item3.Cells[5].FindControl("txtReScheduleDate");
                    DropDownList list18 = (DropDownList)item3.Cells[6].FindControl("ddlReSchHours");
                    DropDownList list19 = (DropDownList)item3.Cells[6].FindControl("ddlReSchMinutes");
                    DropDownList list20 = (DropDownList)item3.Cells[6].FindControl("ddlReSchTime");
                    decimal num5 = Convert.ToDecimal(list18.SelectedValue.ToString() + "." + list19.SelectedValue.ToString()) + Convert.ToDecimal(this.Session["INTERVAL"].ToString());
                    string selectedValue = list20.SelectedValue;
                    if ((Convert.ToDecimal(list18.SelectedValue.ToString() + "." + list19.SelectedValue.ToString()) < Convert.ToDecimal((double)12.0)) && (num5 >= Convert.ToDecimal((double)12.0)))
                    {
                        if (selectedValue == "AM")
                        {
                            selectedValue = "PM";
                        }
                        else if (selectedValue == "PM")
                        {
                            selectedValue = "AM";
                        }
                    }
                    list17.Add(item3.Cells[1].Text);
                    list17.Add(eventID);
                    list17.Add(list10.SelectedValue);
                    list17.Add(box5.Text);
                    list17.Add(box6.Text);
                    list17.Add(box8.Text);
                    list17.Add(num5.ToString());
                    list17.Add(selectedValue.ToString());
                    if (list10.SelectedValue != "1")
                    {
                        calender.Update_ReShedule_Info(list17);
                    }
                }
            }
            calender = new Bill_Sys_Calender();
            list8 = new ArrayList();
            list8.Add(num2);
            list8.Add(false);
            list8.Add(this.txtNotes.Text);
            calender.UPDATE_EventNotes_Status(list8, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
            ArrayList list21 = new ArrayList();
            list21.Add(num2);
            if (this.chkTransportation.Checked)
            {
                list21.Add(1);
            }
            else
            {
                list21.Add(0);
            }
            if (this.chkTransportation.Checked)
            {
                if (this.cmbTransport.Value.ToString() == "NA")
                {
                    this.lblMsg.Text = "Please select a transport";
                    this.lblMsg.Visible = true;
                    return;
                }
                list21.Add(Convert.ToInt32(this.cmbTransport.Value.ToString()));
            }
            else
            {
                list21.Add(null);
            }
            calender.UPDATE_TransportationCompany_Event(list21);
            if ((this.cmbReferringDoctor.Visible && (this.cmbReferringDoctor.Text != "")) && (this.cmbReferringDoctor.Text != "NA"))
            {
                ArrayList list22 = new ArrayList();
                list22.Add(num2);
                if (this.cmbReferringDoctor.Items.Count > 0)
                {
                    list22.Add(this.cmbReferringDoctor.Value.ToString());
                }
                else
                {
                    list22.Add(this.cmbReferringDoctor.Value.ToString());
                }
                calender.Update_Doctor_Id(list22);
            }
            if (this.hdnAppDate.Value.ToString() != "")
            {
                string str6 = "Appointment.aspx?appointmentDate=" + this.hdnAppDate.Value.ToString() + "&interval=" + this.Session["INTERVAL"].ToString() + "&reffFacility=" + this.reffCompanyID.Value.ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "alert('Appointment saved successfully.');window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='" + str6 + "';", true);
                this.hdnReturnOpration.Value = "refresh";
                this.hdnReturnPath.Value = str6;
            }
            this.ClearValues();
            this.Session["PopUp"] = "True";
            if (base.Request.QueryString["From"] == null)
            {
                string str7 = "Appointment.aspx?appointmentDate=" + this.hdnAppDate.Value.ToString() + "&interval=" + this.Session["INTERVAL"].ToString() + "&reffFacility=" + this.reffCompanyID.Value.ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "alert('Appointment saved successfully.');window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='" + str7 + "';", true);
                this.hdnReturnOpration.Value = "refresh";
                this.hdnReturnPath.Value = str7;
            }
            else
            {
                if (base.Request.QueryString["GRD_ID"] != null)
                {
                    this.Session["GRD_ID"] = base.Request.QueryString["GRD_ID"].ToString();
                }
                string str8 = "../Bill_SysPatientDesk.aspx";
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='" + str8 + "';", true);
                this.hdnReturnOpration.Value = "refresh";
                this.hdnReturnPath.Value = str8;
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void UpdateEvent()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            this.lblMsg.Visible = false;
            this.txtCaseID.Text = this.hdnCaseID.Value;
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.updateAppointmentFromBillingCompany();
            }
            else
            {
                this.updateAppointmentFromTestFacility();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private DataSet GetRoomDetailsBySpeciality(string roomId, string companyId)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        SqlCommand comm;
        SqlConnection conn = null;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();
        String strsqlCon;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = "sp_get_room_details";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_ROOM_ID", roomId);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlda = new SqlDataAdapter(comm);
            
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally { conn.Close(); }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        return ds;

    }

    private DataSet GetAppointmentInterval(string companyId)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        SqlCommand comm;
        SqlConnection conn = null;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();
        String strsqlCon;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = "sp_get_appointment_interval";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlda = new SqlDataAdapter(comm);
            
            sqlda.Fill(ds);



        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally { conn.Close(); }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return ds;
    }

    public static string ConvertTimeFormat(string strTime)
    {
        string time = "";
        if (String.IsNullOrEmpty(strTime))
        {
            return null;
        }

        DateTime dt1;
        string appTime = strTime;
        appTime = appTime.Replace('.', ':');
        if (!appTime.StartsWith("1"))
        {
            appTime = "0" + appTime;
        }
        bool res = DateTime.TryParse(appTime, out dt1);
        if (res)
        {
            time = dt1.ToString("HH:mm");
        }
        return time;
    }

    public static string ConvertTo12TimeFormat(string strTime)
    {
        string time = "";
        if (String.IsNullOrEmpty(strTime))
        {
            return null;
        }

        DateTime dt1;
        string appTime = strTime;
        appTime = appTime.Replace('.', ':');
        //if (!appTime.StartsWith("1"))
        //{
        //    appTime = "0" + appTime;
        //}
        appTime = appTime + ":00";
        bool res = DateTime.TryParse(appTime, out dt1);
        if (res)
        {
            time = dt1.ToString("h:mm tt");
        }
        return time;
    }


    private string GetPatientsVisitCounts(string roomId, string companyId, string eventDate, string eventStartTime, string eventEndTime,string caseId,string timeType, string endTimeType)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        SqlCommand comm;
        SqlConnection conn = null;
        String strsqlCon;
        string count1="", count2 = "";
        string count = "";

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = "sp_get_patient_visit_count";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_ROOM_ID", roomId);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", eventDate);
            comm.Parameters.AddWithValue("@DT_START_TIME", eventStartTime);
            comm.Parameters.AddWithValue("@DT_END_TIME", eventEndTime);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", caseId);
            comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", timeType);
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", endTimeType);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                count1 = dr.GetValue(0).ToString(); ;
            }

            dr.NextResult();

            while (dr.Read())
            {
                count2 = dr.GetValue(0).ToString(); ;
            }
            dr.Close();
            count= count1 + "," + count2;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally { conn.Close(); }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return count;
    }

    public static string GetLast(string source, int tail_length)
    {
        if (tail_length >= source.Length)
            return source;
        return source.Substring(source.Length - tail_length);
    }


    protected void btnReminder_Click(object sender, EventArgs e)
    {
        if (this.hdnEventID.Value.ToString() != "")
        {
            string str8 = this.hdnEventID.Value.ToString();
            this._patient_TVBO = new Patient_TVBO();

          string msg=  SMSMessaging.SendReminder(Convert.ToInt32(str8));
            if (msg != "Error") {
                this.usrMessage.PutMessage("Reminder Sent Successfully .");
                this.usrMessage.SetMessageType(0);
                this.usrMessage.Show();
            }
        }
    }
  
    
}

class AddedEvetDetail
{
    int eventID;
    public int EventID
    {
        get
        {
            return eventID;
        }
        set
        {
            eventID = value;
        }
    }
    DateTime eventDate;
    public DateTime EventDate
    {
        get
        {
            return eventDate;
        }
        set
        {
            eventDate = value;
        }
    }
    decimal eventTime;
    public decimal EventTime
    {
        get
        {
            return eventTime;
        }
        set
        {
            eventTime = value;
        }
    }
}