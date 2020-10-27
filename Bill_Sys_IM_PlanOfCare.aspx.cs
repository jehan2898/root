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

public partial class Bill_Sys_IM_PlanOfCare : PageBase
{
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        Bill_Sys_CheckoutBO _obj = new Bill_Sys_CheckoutBO();
        Session["IMEventID"] = "76";

        DataSet dsObj = _obj.PatientName(Session["IMEventID"].ToString());
        Session["IM_InicialCaseID"] = dsObj.Tables[0].Rows[0][1].ToString();
        if (!IsPostBack)
        {
            if (Request.QueryString["EID"] != null)
            {
                Session["IMEventID"] = Request.QueryString["EID"].ToString();

                txtEventID.Text = (string)Session["IMEventID"].ToString();

                LoadData();
                LoadPatientData();
            }
            else
            {
                txtEventID.Text = "344184";
                LoadData();
                LoadPatientData();

            }
        }
        Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
        txtDOE.Text = DateTime.Now.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_IM_PlanOfCare.aspx");
        }
        #endregion
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        {

            if (rdlMedication_Restriction.SelectedValue.ToString().Equals(""))
            {
                txtrdlMedication_Restriction.Text = "-1";
            }
            else
            {
                txtrdlMedication_Restriction.Text = rdlMedication_Restriction.SelectedValue;

            }


            if (rdlPatient_Need_Diagnostic.SelectedValue.ToString().Equals(""))
            {
                txtrdlPatient_Need_Diagnostic.Text = "-1";
            }
            else
            {
                txtrdlPatient_Need_Diagnostic.Text = rdlPatient_Need_Diagnostic.SelectedValue;

            }


            if (Page.IsValid)
            {
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "IMPlanOfCare.xml";
                _saveOperation.SaveMethod();
            }
            //Code To generate pdf according to check box value and Visit Status 
            Bill_Sys_CheckoutBO ChkBo = new Bill_Sys_CheckoutBO();
            if (ChkBo.ChekVisitStaus(Convert.ToInt32(txtEventID.Text), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == "2")
            {

                ArrayList objal1 = new ArrayList();
                SpecialityPDFFill SPDFF = new SpecialityPDFFill();


                //chkReferrals_Chiropractor
                if (chkReferrals_Chiropractor.Checked == true)
                {

                    SPDFF.sz_EventID = "344184";//Session["IMEventID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "CH";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMCH";
                    SPDFF.sz_XMLPath = "CO_Refferal_IM_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);

                    SPDFF.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = "344184"; //Session["IMEventID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMCH";
                    SPDFF.SZ_SPECIALITY_NAME = "CH";
                    SPDFF.IMG_ID_COLUMN_CODE = "CH_IE_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //chkReferrals_Physical_Therapist
                if (chkReferrals_Physical_Therapist.Checked == true)
                {

                    SPDFF.sz_EventID = Session["IMEventID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "PT";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMPT";
                    SPDFF.sz_XMLPath = "CO_Refferal_IM_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);

                    SPDFF.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IMEventID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMPT";
                    SPDFF.SZ_SPECIALITY_NAME = "PT";
                    SPDFF.IMG_ID_COLUMN_CODE = "PT_IE_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //chkReferrals_Occupational_Therapist
                if (chkReferrals_Occupational_Therapist.Checked == true)
                {

                    SPDFF.sz_EventID = Session["IMEventID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "OT";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMOT";
                    SPDFF.sz_XMLPath = "CO_Refferal_IM_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);

                    SPDFF.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IMEventID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMOT";
                    SPDFF.SZ_SPECIALITY_NAME = "OT";
                    SPDFF.IMG_ID_COLUMN_CODE = "OT_IE_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //chkTest_Emg_Ncv 
                if (CHK_VS_NCT.Checked == true)
                {

                    SPDFF.sz_EventID = Session["IMEventID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "EMG";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMEMG";
                    SPDFF.sz_XMLPath = "MST_ECG_IM_XML_Path";
                    SPDFF.sz_PDFPath = "MST_ECG_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);

                    SPDFF.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IMEventID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMEMG";
                    SPDFF.SZ_SPECIALITY_NAME = "EMG";
                    SPDFF.IMG_ID_COLUMN_CODE = "VSNCT_IE_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_SUPPLIES_EMS_UNIT TO CHK_SUPPLIES_BIOFEEDBACK_TRAINING_SESSIONS
                if ((chk_Assistive_Device_EMS_Unit.Checked == true) || (chk_Assistive_Device_Wrist_Support.Checked == true) || (chkAssistive_Device_Cervical_Pillow.Checked == true) || (chk_Assistive_Device_Elbow_Support.Checked == true) || (chk_Assistive_Device_Lumbosacral_Back_Support.Checked == true) || (chk_Assistive_Device_Ankle_Support.Checked == true) || (chk_Assistive_Device_Lumbar_Cushion.Checked == true) || (chk_Assistive_Device_Knee_Support.Checked == true) || (chkAssistive_Device_Massager.Checked == true) || (chk_Assistive_Device_Cane.Checked == true) || (chkAssistive_Device_Other.Checked == true))
                {

                    SPDFF.sz_EventID = Session["IMEventID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "SP";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMSP";
                    SPDFF.sz_XMLPath = "Supplies_IM_INICIAL_XML_Path";
                    SPDFF.sz_PDFPath = "Supplies_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);

                    SPDFF.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IMEventID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMSP";
                    SPDFF.SZ_SPECIALITY_NAME = "SP";
                    SPDFF.IMG_ID_COLUMN_CODE = "SUPPLIES_IE_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }
            }
            //End Code           

            Response.Redirect("Bill_Sys_IM_workStatus.aspx", false);
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

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Response.Redirect("Bill_Sys_IM_DiagnosticImpresstion.aspx", false);
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
            _editOperation.Xml_File = "IMPlanOfCare.xml";
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
            _editOperation.Xml_File = "IM_PatientInformetion.xml";
            _editOperation.LoadData();


            if (txtrdlMedication_Restriction.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlMedication_Restriction.SelectedValue = txtrdlMedication_Restriction.Text;

            }


            if (txtrdlPatient_Need_Diagnostic.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlPatient_Need_Diagnostic.SelectedValue = txtrdlPatient_Need_Diagnostic.Text;

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
