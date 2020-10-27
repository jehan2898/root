using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Componend;

public partial class Bill_Sys_Treatment_OT_PT : PageBase
{

    private OTPT obj_OTPT;
    private OTPT_TREATMENT_DAO obj_OTPT_Treatment;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;
    private workers_templateC4_2 _workerstemplate = new workers_templateC4_2();
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            rdbreferral.Attributes.Add("onclick", "GetReferral();");
            rdlstDiagnosisTestandReferral.Attributes.Add("onclick", "GetrdlstDiagnosisTestandReferral();");
            rbPatientWorking.Attributes.Add("onclick", "GetrbPatientWorking();");
            rdnpatientseen.Attributes.Add("onclick", "Getrdnpatientseen();");
            chkTreatment.Attributes.Add("onclick", "chktreatmentevent();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString();// "sas0000001";
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
            
            if (Request.QueryString["billnumber"] != null)
            {
                Session["BILL_NO"] = Request.QueryString["billnumber"];
            }
            if (Request.QueryString["caseid"] != null)
            {
                Session["CASE_ID"] = Request.QueryString["caseid"];
            }
            txtBillNumber.Text = Session["BILL_NO"].ToString();// "sas0000001";           
            txtCaseID.Text = Session["CASE_ID"].ToString();
            if (!IsPostBack)
            {
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                txtPatientID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNumber.Text, "SP_MST_PATIENT_INFORMATION", "GETPATIENTID");
                LoadData();
                passInformationToLoadData();
               
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ExaminationandTreatmentC4_2.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        obj_OTPT = new OTPT();
        DataSet dsevlutreatment = new DataSet();
        try
        {
            
            dsevlutreatment = obj_OTPT.GET_Evalution_and_Treatment_Information_OTPT(txtCaseID.Text, txtCompanyID.Text);
            if (dsevlutreatment.Tables.Count > 0)
            {
                if (dsevlutreatment.Tables[0].Rows.Count > 0)
                {
                    if (dsevlutreatment.Tables[0].Rows[0].ToString() != "" && dsevlutreatment.Tables[0].Rows[0].ToString() != null)
                    {
                        string referall = dsevlutreatment.Tables[0].Rows[0]["BT_REFRERAL_FOR"].ToString();
                        if (referall == "0")
                        {
                            rdbreferral.SelectedIndex = 0;
                            txtcondition.Enabled = false;
                            txtTreatment.Enabled = false;
                            txtfrequency.Enabled = false;
                            txtPeriod.Enabled = false;
                            rdlstDiagnosisTestandReferral.Enabled = false;
                            chkTreatment.Enabled =false;
                           
                           
                        }
                        else if (referall == "1")
                        {
                            rdbreferral.SelectedIndex = 1;
                            txtEvaluation.Enabled = false;


                        }
                        else if (referall == "2")
                        {
                            rdbreferral.SelectedIndex = 2;

                        }
                        txtEvaluation.Text = dsevlutreatment.Tables[0].Rows[0]["SZ_EVALUTION_DESC"].ToString();
                        txtcondition.Text = dsevlutreatment.Tables[0].Rows[0]["SZ_PATIENT_CONDITION"].ToString();
                        txtTreatment.Text = dsevlutreatment.Tables[0].Rows[0]["SZ_TREATMENT"].ToString();
                        string treatmentplan = dsevlutreatment.Tables[0].Rows[0]["BT_TREATMENT_PLAN"].ToString();
                        if (treatmentplan == "1")
                        {
                            rdlstDiagnosisTestandReferral.SelectedIndex = 0;
                        }
                        else if (treatmentplan == "0")
                        {
                            rdlstDiagnosisTestandReferral.SelectedIndex = 1;
                            txtfrequency.Enabled = false;
                            txtPeriod.Enabled = false;

                        }
                        else if (treatmentplan == "2")
                        {
                            rdlstDiagnosisTestandReferral.SelectedIndex = 2;

                        }
                        txtfrequency.Text = dsevlutreatment.Tables[0].Rows[0]["SZ_FREQUENCY"].ToString();
                        txtPeriod.Text = dsevlutreatment.Tables[0].Rows[0]["SZ_PERIOD"].ToString();
                        txtVisitDate.Text = dsevlutreatment.Tables[0].Rows[0]["DT_DATE_OF_VISIT_REPORT"].ToString();
                        txtFirstVisitDate.Text = dsevlutreatment.Tables[0].Rows[0]["DT_DATE_OF_FIRST_VISIT"].ToString();
                        string patientseen = dsevlutreatment.Tables[0].Rows[0]["BT_PATIENT_SEEN_AGAIN"].ToString();
                        if (patientseen == "1")
                        {
                            rdnpatientseen.SelectedIndex = 0;
                        }
                        else if (patientseen == "0")
                        {
                            rdnpatientseen.SelectedIndex = 1;

                        }
                        else if (patientseen == "2")
                        {
                            rdnpatientseen.SelectedIndex = 2;

                        }
                        txtPatientSeen.Text = dsevlutreatment.Tables[0].Rows[0]["SZ_PATIENT_SEEN_YES"].ToString();
                        string attendoctor = dsevlutreatment.Tables[0].Rows[0]["BT_PATIENT_ATTENDING_DOCTOR"].ToString();
                        if (attendoctor == "1")
                        {
                            rbpatientAttendingDoctor.SelectedIndex = 0;
                        }
                        else if (attendoctor == "0")
                        {
                            rbpatientAttendingDoctor.SelectedIndex = 1;

                        }
                        else if (attendoctor == "2")
                        {
                            rbpatientAttendingDoctor.SelectedIndex = 2;

                        }
                        string patientworking = dsevlutreatment.Tables[0].Rows[0]["BT_PATIENT_WORKING"].ToString();
                        if (patientworking == "1")
                        {
                            rbPatientWorking.SelectedIndex = 0;
                        }
                        else if (patientworking == "0")
                        {
                            rbPatientWorking.SelectedIndex = 1;

                        }
                        else if (patientworking == "2")
                        {
                            rbPatientWorking.SelectedIndex = 2;

                        }
                        txtlimitedwork.Text = dsevlutreatment.Tables[0].Rows[0]["SZ_LIMITED_WORK"].ToString();
                        txtregularwork.Text = dsevlutreatment.Tables[0].Rows[0]["SZ_REGULAR_WORK"].ToString();
                        if (dsevlutreatment.Tables[0].Rows[0]["BT_ADDITIONAL_SPACE"].ToString() == "1")
                        {
                            chkTreatment.Checked = true;

                        }
                        else if (dsevlutreatment.Tables[0].Rows[0]["BT_ADDITIONAL_SPACE"].ToString() == "0")
                        {

                            chkTreatment.Checked = false;
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
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SaveData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //rdbreferral
        try
        {
            obj_OTPT_Treatment = new OTPT_TREATMENT_DAO();
            obj_OTPT = new OTPT();
            obj_OTPT_Treatment.SZ_PATIENT_ID = txtPatientID.Text;
            obj_OTPT_Treatment.SZ_CASE_ID = txtCaseID.Text;
            obj_OTPT_Treatment.SZ_BILL_NO = txtBillNumber.Text;
            obj_OTPT_Treatment.BT_REFRERAL_FOR = rdbreferral.SelectedValue;
            
             obj_OTPT_Treatment.SZ_EVALUTION_DESC = txtEvaluation.Text;
             obj_OTPT_Treatment.SZ_PATIENT_CONDITION = txtcondition.Text;
             obj_OTPT_Treatment.SZ_TREATMENT = txtTreatment.Text;
             obj_OTPT_Treatment.BT_TREATMENT_PLAN = rdlstDiagnosisTestandReferral.SelectedValue;

             obj_OTPT_Treatment.SZ_FREQUENCY = txtfrequency.Text;
             obj_OTPT_Treatment.SZ_PERIOD = txtPeriod.Text;

            
            obj_OTPT_Treatment.DT_DATE_OF_VISIT_REPORT = txtVisitDate.Text;
            obj_OTPT_Treatment.DT_DATE_OF_FIRST_VISIT = txtFirstVisitDate.Text;

            obj_OTPT_Treatment.BT_PATIENT_SEEN_AGAIN = rdnpatientseen.SelectedValue;
            obj_OTPT_Treatment.SZ_PATIENT_SEEN_YES = txtPatientSeen.Text;


           

            obj_OTPT_Treatment.BT_PATIENT_ATTENDING_DOCTOR = rbpatientAttendingDoctor.SelectedValue;
            obj_OTPT_Treatment.BT_PATIENT_WORKING = rbPatientWorking.SelectedValue;

            obj_OTPT_Treatment.SZ_LIMITED_WORK = txtlimitedwork.Text;
            obj_OTPT_Treatment.SZ_REGULAR_WORK = txtregularwork.Text;
            obj_OTPT_Treatment.SZ_COMPANY_ID = txtCompanyID.Text;
            if (chkTreatment.Checked)
            {

                obj_OTPT_Treatment.BT_ADDITIONAL_SPACE = "1";
               
            }
            else
            {
                obj_OTPT_Treatment.BT_ADDITIONAL_SPACE = "0";
               
            }
            obj_OTPT.Patient_Treatment_Info(obj_OTPT_Treatment);
                
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSaveAndGoToNext_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveData();
            //SaveExaminationDetails();
            Response.Redirect("Bill_Sys_BillingInformationOT-PT.aspx", false);     
           
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SaveExaminationDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            DeleteExaminationDetails();
            if (rdlstDiagnosisTestandReferral.SelectedValue == "0")
            {
                
                //if (chkReferralsOthers.Checked)
                //{
                //    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                //    txtTransactionName.Text = chkReferralsOthers.Text;
                //    txtDescription.Text = txtReferralsOthers.Text;
                //    _saveOperation.SaveMethod();
                //}
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void DeleteExaminationDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _workerstemplate = new workers_templateC4_2();
        try
        {
            if (txtTreatmentID.Text != "")
            {
                _workerstemplate.DeleteExaminationDetails(txtTreatmentID.Text);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void LoadCheckBoxAndTextBox(DataSet p_objDataSet)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            foreach (DataRow obj1 in p_objDataSet.Tables[0].Rows)
            {
                for (int j = 0; j <= pnlTestReferrals.Controls.Count - 1; j++)
                {
                    if (pnlTestReferrals.Controls[j].ID != null)
                    {
                        if (pnlTestReferrals.Controls[j].GetType() == typeof(CheckBox))
                        {
                            CheckBox objCheckBox;
                            objCheckBox = (CheckBox)pnlTestReferrals.Controls[j];
                            if (objCheckBox.Text == obj1[0].ToString())
                            {
                                objCheckBox.Checked = true;
                                String szID = objCheckBox.ID;
                                String szTxtID = szID.Replace("chk", "txt");

                                TextBox objTextBox;
                                objTextBox = (TextBox)pnlTestReferrals.FindControl(szTxtID);
                                if (objTextBox != null)
                                    objTextBox.Text = obj1[1].ToString();

                            }
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
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void passInformationToLoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet _objDataSet;
        try
        {
            if (rdlstDiagnosisTestandReferral.SelectedValue == "0")
            {
                _workerstemplate = new workers_templateC4_2();
                _objDataSet = new DataSet();
                _objDataSet = _workerstemplate.GetExaminationDetailList(txtTreatmentID.Text);
                LoadCheckBoxAndTextBox(_objDataSet);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


    }
}
