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
public partial class Bill_Sys_ExaminationandTreatmentC4_2 : PageBase
{
    private EditOperation _editOperation;
    private SaveOperation _saveOperation;
    private workers_templateC4_2 _workerstemplate;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;
    protected void Page_Load(object sender, EventArgs e)
     {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString();// "sas0000001";
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
            if (!IsPostBack)
            {
                _workerstemplate = new workers_templateC4_2();
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                txtPatientID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNumber.Text, "SP_MST_PATIENT_INFORMATION", "GETPATIENTID");
                txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                if (txtTreatmentID.Text != "")
                {
                    LoadData();
                    passInformationToLoadData();
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
        _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtTreatmentID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "ExaminationTreatment.xml";            
            _editOperation.LoadData();

            if (txtDateOfInjury.Text != "")
            {
                txtDateOfInjury.Text = Convert.ToDateTime(txtDateOfInjury.Text).ToShortDateString();                
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
        _saveOperation = new SaveOperation();
        _editOperation = new EditOperation();
        _workerstemplate = new workers_templateC4_2();
        try
        {
            if (txtTreatmentID.Text != "")
            {
                _editOperation.Primary_Value = txtTreatmentID.Text;
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "ExaminationTreatment.xml";
                _editOperation.UpdateMethod();
            }
            else
            {
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "ExaminationTreatment.xml";
                _saveOperation.SaveMethod();
                txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtPatientID.Text);
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
            SaveExaminationDetails();
            Response.Redirect("Bill_Sys_DoctorsOpinionC4_2.aspx", false);
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
        _saveOperation = new SaveOperation();
        try
        {
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "ExaminationTreatmentDetails.xml";
            DeleteExaminationDetails();
            if (rdlstDiagnosisTestandReferral.SelectedValue == "0")
            {
                if (chkCTScans.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkCTScans.Text;
                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkEMGNCS.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkEMGNCS.Text;
                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkMRI.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkMRI.Text;

                    txtDescription.Text = txtMRI.Text;
                    _saveOperation.SaveMethod();
                }
                if (chkLabs.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkLabs.Text;
                    txtDescription.Text = txtLabs.Text;
                    _saveOperation.SaveMethod();
                }
                if (chkXRay.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkXRay.Text;

                    txtDescription.Text = txtXRay.Text;
                    _saveOperation.SaveMethod();
                }
                if (chkTestOthers.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkTestOthers.Text;

                    txtDescription.Text = txtTestOthers.Text;
                    _saveOperation.SaveMethod();
                }



                if (chkReferralsChiropractor.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkReferralsChiropractor.Text;

                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkReferralsInternist.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkReferralsInternist.Text;

                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkReferralsOccupationalTherapist.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkReferralsOccupationalTherapist.Text;

                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkReferralsPhysicalTherapist.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkReferralsPhysicalTherapist.Text;

                    txtDescription.Text = "";
                    _saveOperation.SaveMethod();
                }
                if (chkReferralsSpecialistIn.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
                    txtTransactionName.Text = chkReferralsSpecialistIn.Text;

                    txtDescription.Text = txtReferralsSpecialistIn.Text;
                    _saveOperation.SaveMethod();
                }
                if (chkReferralsOthers.Checked)
                {
                    txtTreatmentID.Text = _workerstemplate.GetExaminationLatestID(txtBillNumber.Text);
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
