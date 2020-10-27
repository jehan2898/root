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
public partial class Bill_Sys_CM_PlanOfCare : PageBase
{
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        Bill_Sys_CheckoutBO _obj = new Bill_Sys_CheckoutBO();
        DataSet dsObj = _obj.PatientName(Session["CM_HI_EventID"].ToString());
        Session["CO_CHIRO_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString(); 

        txtEventID.Text = Session["CM_HI_EventID"].ToString();
        if (!IsPostBack)
        {
           LoadData();
           LoadPatientData();

        }
        TXT_DOE.Text = DateTime.Now.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CM_PlanOfCare.aspx");
        }
        #endregion
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_CM_DiagnosticImpresstion.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
          if(Page.IsValid)
           {
            if (RDO_MEDICATION_RESTRICTION.SelectedValue.Equals(""))
            {
                txtRDO_MEDICATION_RESTRICTION.Text = "-1";
            }
            else
            {
                txtRDO_MEDICATION_RESTRICTION.Text = RDO_MEDICATION_RESTRICTION.SelectedValue;
            }

            if (RDO_PATIENT_NEED_DIAGNOSTIC.SelectedValue.Equals(""))
            {
                txtRDO_PATIENT_NEED_DIAGNOSTIC.Text = "-1";
            }
            else
            {
                txtRDO_PATIENT_NEED_DIAGNOSTIC.Text = RDO_PATIENT_NEED_DIAGNOSTIC.SelectedValue;
            }

            _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "CM_INITIAL_EVALPlanOfCare.xml";
            _saveOperation.SaveMethod();
          }



             //Code To generate pdf according to check box value and Visit Status 
            Bill_Sys_CheckoutBO ChkBo = new Bill_Sys_CheckoutBO();
            if (ChkBo.ChekVisitStaus(Convert.ToInt32(txtEventID.Text), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == "2")
            {
                
                ArrayList objal1 = new ArrayList();
                SpecialityPDFFill SPDFF = new SpecialityPDFFill();
                

                //CHK_REFERRALS_CHIROPRACTOR
                if (CHK_REFERRALS_CHIROPRACTOR.Checked == true)
                {

                    SPDFF.sz_EventID = Session["CM_HI_EventID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "CH";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMCH";
                    SPDFF.sz_XMLPath = "CO_Refferal_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["CO_CHIRO_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["CM_HI_EventID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMCH";
                    SPDFF.SZ_SPECIALITY_NAME = "CH";
                    SPDFF.IMG_ID_COLUMN_CODE = "CH_IE_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_REFERRALS_PHYSICAL_THERAPIST
                if (CHK_REFERRALS_PHYSICAL_THERAPIST.Checked == true)
                {

                    SPDFF.sz_EventID = Session["CM_HI_EventID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "PT";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMPT";
                    SPDFF.sz_XMLPath = "CO_Refferal_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["CO_CHIRO_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["CM_HI_EventID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMPT";
                    SPDFF.SZ_SPECIALITY_NAME ="PT";
                    SPDFF.IMG_ID_COLUMN_CODE = "PT_IE_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_REFERRALS_OCCUPATIONAL_THERAPIST
                if (CHK_REFERRALS_OCCUPATIONAL_THERAPIST.Checked == true)
                {

                    SPDFF.sz_EventID = Session["CM_HI_EventID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "OT";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMOT";
                    SPDFF.sz_XMLPath = "CO_Refferal_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["CO_CHIRO_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["CM_HI_EventID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMOT";
                    SPDFF.SZ_SPECIALITY_NAME = "OT";
                    SPDFF.IMG_ID_COLUMN_CODE = "OT_IE_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_TEST_EMG_NCV.Checked
                if (CHK_TEST_EMG_NCV.Checked == true)
                {

                    SPDFF.sz_EventID = Session["CM_HI_EventID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "EMG";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMEMG";
                    SPDFF.sz_XMLPath = "CO_Refferal_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["CO_CHIRO_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["CM_HI_EventID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMEMG";
                    SPDFF.SZ_SPECIALITY_NAME = "EMG";
                    SPDFF.IMG_ID_COLUMN_CODE = "VSNCT_IE_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_SUPPLIES_EMS_UNIT TO CHK_SUPPLIES_BIOFEEDBACK_TRAINING_SESSIONS
                if ((CHK_ASSISTIVE_DEVICE_CANE.Checked == true) || (CHK_CRUTCHES.Checked == true) || (CHK_ORTHOTICS.Checked == true) || (CHK_WALKER.Checked == true) || (CHK_WHEEL_CHAIR.Checked == true) || (CHK_ASSISTIVE_DEVICE_OTHER.Checked == true))
                {

                    SPDFF.sz_EventID = Session["CM_HI_EventID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "SP";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMSP";
                    SPDFF.sz_XMLPath = "Supplies_CM_INICIAL_XML_Path";
                    SPDFF.sz_PDFPath = "Supplies_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["CO_CHIRO_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["CM_HI_EventID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMSP";
                    SPDFF.SZ_SPECIALITY_NAME ="SP";
                    SPDFF.IMG_ID_COLUMN_CODE = "SUPPLIES_IE_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }
            }
            //End Code                   
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
       
        Response.Redirect("Bill_Sys_CM_workStatus.aspx", false);
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public void LoadPatientData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CM_INITIAL_EVAL_PatientInformetion.xml";
            _editOperation.LoadData();

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

    public void LoadData()
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CM_INITIAL_EVALPlanOfCare.xml";
            _editOperation.LoadData();


            //RDO_MEDICATION_RESTRICTION
            if (txtRDO_MEDICATION_RESTRICTION.Text .Equals("-1"))
            {
               
            }
            else
            {
                RDO_MEDICATION_RESTRICTION.SelectedValue = txtRDO_MEDICATION_RESTRICTION.Text;
            }
            //RDO_PATIENT_NEED_DIAGNOSTIC
            if (txtRDO_PATIENT_NEED_DIAGNOSTIC.Text.Equals("-1"))
            {
                
            }
            else
            {
                RDO_PATIENT_NEED_DIAGNOSTIC.SelectedValue = txtRDO_PATIENT_NEED_DIAGNOSTIC.Text;
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
