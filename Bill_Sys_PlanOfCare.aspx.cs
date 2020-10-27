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
public partial class Bill_Sys_PlanOfCare : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private workers_templateC4_2 _workerstemplate;
    protected void Page_Load(object sender, EventArgs e)
    {
        //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtPatientID.Text = Session["PatientID"].ToString();
        txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString();
        txtProposedWorkTreatment.Attributes.Add("Onkeyup", "CheckLengthProposedWorkTreatment(this,320)");
        txtMedicationRestrictions.Attributes.Add("Onkeyup", "CheckLengthMedicationRestrictions(this,232)");
        
        //txtPatientID.Text = "PI00003";
        if (!IsPostBack)
        {
            rdlstMedicationRestrictions.Attributes.Add("onclick", "checkvalidate_medicationRestriction_none();");
            _workerstemplate = new workers_templateC4_2();
            txtPlanofCareID.Text = _workerstemplate.GetPlanOfCareLatestID_New(txtBillNumber.Text);
            LoadData();
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_PlanOfCare.aspx");
        }
        #endregion
        
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
            if (rdlstTreatmentGuidelines.SelectedValue.ToString() == "0")
            {
                txtTreatmentGuidelines.Text = txtTreatmentGuidelines.Text;
            }
            if (rdlstTreatmentGuidelines.SelectedValue.ToString() == "1")
            {
                txtTreatmentGuidelines.Text = txtVarianceGuideline.Text;
            }
            SavePlanOfCare();
            SavePlanOfCareDetails();
            SaveAssistiveDevice();
            Response.Redirect("Bill_Sys_WorkStatus.aspx", false);
            if (rdlstTreatmentGuidelines.SelectedValue.ToString() == "1")
            {
                txtTreatmentGuidelines.Text = "";
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }


    private void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "PlanOfCare_New.xml";
            _editOperation.Primary_Value = txtBillNumber.Text; 
            _editOperation.LoadData();

            if (txtDateOfInjury.Text != "")
            {
                txtDateOfInjury.Text = Convert.ToDateTime(txtDateOfInjury.Text).ToShortDateString();
            }
            passInformationToLoadData();

            if (rdlstTreatmentGuidelines.SelectedValue.ToString() == "1")
            {
                txtVarianceGuideline.Text = txtTreatmentGuidelines.Text;
                txtTreatmentGuidelines.Text = "";
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
    private  void SavePlanOfCare()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        _editOperation = new EditOperation();
        try
        {
            if (txtPlanofCareID.Text != "")
            {
                _editOperation.Primary_Value = txtPlanofCareID.Text;
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "PlanOfCareNew.xml";
                _editOperation.UpdateMethod();
            }
            else
            {
                if (rdlstMedicationRestrictions.SelectedValue == "0")
                {
                    txtMedicationRestrictions.Text = "";
                }
                
                if (rdlstTreatmentGuidelines.SelectedValue == "1")
                {
                    txtTreatmentGuidelines.Text = txtVarianceGuideline.Text;
                }
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "PlanOfCare_New.xml";
                _saveOperation.SaveMethod();
                _workerstemplate = new workers_templateC4_2();
                txtPlanofCareID.Text = _workerstemplate.GetPlanOfCareLatestID_New(txtBillNumber.Text);
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

    private void SavePlanOfCareDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        {
            DeletePlanOfCareDetails();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "PlanOfCareTransaction_New.xml";
            if (rdlstPatientNeedDiagnosisTest.SelectedValue == "0")
            {
                if (chkCTScans.Checked)
                {
                  
                    txtTransactionName.Text = chkCTScans.Text;
                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkEMGNCS.Checked)
                {
                    txtTransactionName.Text = chkEMGNCS.Text;
                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkMRI.Checked)
                {
                    txtTransactionName.Text = chkMRI.Text;

                    txtDescription.Text = txtMri.Text;
                    _saveOperation.SaveMethod();
                }
                if (chkLabs.Checked)
                {
                    txtTransactionName.Text = chkLabs.Text;
                    txtDescription.Text = txtLabs.Text;
                    _saveOperation.SaveMethod();
                }
                if (chkXRay.Checked)
                {
                    txtTransactionName.Text = chkXRay.Text;

                    txtDescription.Text = txtXRay.Text;
                    _saveOperation.SaveMethod();
                }
                if (chkTestOthers.Checked)
                {
                    txtTransactionName.Text = chkTestOthers.Text;

                    txtDescription.Text = txtTestOthers.Text;
                    _saveOperation.SaveMethod();
                }



                if (chkReferralsChiropractor.Checked)
                {
                    txtTransactionName.Text = chkReferralsChiropractor.Text;

                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkReferralsInternist.Checked)
                {
                    txtTransactionName.Text = chkReferralsInternist.Text;

                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkReferralsOccupationalTherapist.Checked)
                {
                    txtTransactionName.Text = chkReferralsOccupationalTherapist.Text;

                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkReferralsPhysicalTherapist.Checked)
                {
                    txtTransactionName.Text = chkReferralsPhysicalTherapist.Text;

                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkReferralsSpecialistIn.Checked)
                {
                    txtTransactionName.Text = chkReferralsSpecialistIn.Text;

                    txtDescription.Text = txtReferralsSpecialistIn.Text;
                    _saveOperation.SaveMethod();
                }
                if (chkReferralsOthers.Checked)
                {
                    txtTransactionName.Text = chkReferralsOthers.Text;
                    txtDescription.Text = txtReferralsOthers.Text;
                    _saveOperation.SaveMethod();
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

    private void SaveAssistiveDevice()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        {
            DeleteAssistiveDevice();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "AssistiveDeviceDetails_New.xml";

            if (chkCane.Checked)
            {
                txtAssistiveDesc.Text = "";
                txtAssistiveDeviceName.Text = chkCane.Text;
                _saveOperation.SaveMethod();
            }
            if (chkCrutches.Checked)
            {
                txtAssistiveDesc.Text = "";
                txtAssistiveDeviceName.Text = chkCrutches.Text;
                _saveOperation.SaveMethod();
            }
            if (chkOrthotics.Checked)
            {
                txtAssistiveDesc.Text = "";
                txtAssistiveDeviceName.Text = chkOrthotics.Text;
                _saveOperation.SaveMethod();
            }
            if (chkWalker.Checked)
            {
                txtAssistiveDesc.Text = "";
                txtAssistiveDeviceName.Text = chkWalker.Text;
                _saveOperation.SaveMethod();
            }
            if (chkWheelchair.Checked)
            {
                txtAssistiveDesc.Text = "";
                txtAssistiveDeviceName.Text = chkWheelchair.Text;
                _saveOperation.SaveMethod();
            }
            if (chkAssistiveOthers.Checked)
            {
                txtAssistiveDesc.Text = txtAssistiveOthers.Text;
                txtAssistiveDeviceName.Text = chkAssistiveOthers.Text;
                _saveOperation.SaveMethod();
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

    private void DeletePlanOfCareDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _workerstemplate = new workers_templateC4_2();
        try
        {
            _workerstemplate.DeletePlanOfCareDetailsNewWC4("SP_TXN_TEST_REFERRAL_TRANSACTION_NEW", txtBillNumber.Text);
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
    private void DeleteAssistiveDevice()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _workerstemplate = new workers_templateC4_2();
        try
        {
            _workerstemplate.DeletePlanOfCareDetailsNewWC4("SP_TXN_PATIENT_ASSISTIVE_DEVICE_NEW", txtBillNumber.Text);
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
                for (int j = 0; j <= pnlDevicePrescribe.Controls.Count - 1; j++)
                {
                    if (pnlDevicePrescribe.Controls[j].ID != null)
                    {
                        if (pnlDevicePrescribe.Controls[j].GetType() == typeof(CheckBox))
                        {
                            CheckBox objCheckBox;
                            objCheckBox = (CheckBox)pnlDevicePrescribe.Controls[j];
                            if (objCheckBox.Text == obj1[0].ToString())
                            {
                                objCheckBox.Checked = true;
                                String szID = objCheckBox.ID;
                                String szTxtID = szID.Replace("chk", "txt");

                                TextBox objTextBox;
                                objTextBox = (TextBox)pnlDevicePrescribe.FindControl(szTxtID);
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
            if (rdlstPatientNeedDiagnosisTest.SelectedValue == "0")
            {
                _workerstemplate = new workers_templateC4_2();
                _objDataSet = new DataSet();
                _objDataSet = _workerstemplate.GetTestTransactionDetailList_New(txtPlanofCareID.Text,txtBillNumber.Text);
                LoadCheckBoxAndTextBox(_objDataSet);
            }
            _workerstemplate = new workers_templateC4_2();
            _objDataSet = new DataSet();
            _objDataSet = _workerstemplate.GetAssistiveDeviceDetailListNewWC4(txtPlanofCareID.Text,txtBillNumber.Text);
            LoadCheckBoxAndTextBox(_objDataSet);
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

