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

public partial class Bill_Sys_FUIM_Plan : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Request.QueryString["EID"] != null)
        {
            Session["IM_FW_EVENT_ID"] = Request.QueryString["EID"].ToString();
        }
        Bill_Sys_CheckoutBO _obj = new Bill_Sys_CheckoutBO();
        DataSet dsObj = _obj.PatientName(Session["IM_FW_EVENT_ID"].ToString());
        Session["IM_FOLLOWUP_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString(); 
        //TXT_EVENT_ID.Text = Session["IM_FW_EVENT_ID"].ToString();
        if (!IsPostBack)
        {
            LoadPatientData();
            LoadData();
        }
        TXT_DOS.Text = DateTime.Today.Date.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_FUIM_Plan.aspx");
        }
        #endregion
    }

    protected void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {

            _editOperation.Primary_Value = Session["IM_FW_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Bill_Sys_IM_Plan.xml";
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
            _editOperation.Primary_Value = Session["IM_FW_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_FalloUP_PatientInformetion.xml";
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
    protected void BTN_PREVIOUS_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Bill_Sys_FUIM_Test_Results.aspx");
    }
    protected void BTN_SAVE_NEXT_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        // Create object of SaveOperation. With the help of this object you save information into table.
        try
        {
            if (Page.IsValid)  // Check for Validation.
            {
                _saveOperation.WebPage = this.Page;  // Pass Current web form to SaveOperation.
                _saveOperation.Xml_File = "Bill_Sys_IM_Plan.xml";  // Pass xml file to SaveOperation
                _saveOperation.SaveMethod(); // Call  save method of SaveOperation. Will save all information from web form.
            }

            //Code To generate pdf according to check box value and Visit Status 
            Bill_Sys_CheckoutBO ChkBo = new Bill_Sys_CheckoutBO();
            if (ChkBo.ChekVisitStaus(Convert.ToInt32(TXT_EVENT_ID.Text), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == "2")
            {
                ArrayList ObjalGetVal = new ArrayList();
                ArrayList objal1 = new ArrayList();
                SpecialityPDFFill SPDFF = new SpecialityPDFFill();
                ObjalGetVal = ChkBo.ChekCheckBoxStaus(Convert.ToInt32(TXT_EVENT_ID.Text));

                //CHK_REFERRALS_CHIROPRACTOR
                if (CHK_REFERRALS_CHIROPRACTOR.Checked==true)
                {
                    
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "CH";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMCH";
                    SPDFF.sz_XMLPath = "Refferal_IM_FOLLOWUP_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);
                    


                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMCH";
                    SPDFF.SZ_SPECIALITY_NAME = "CH";
                    SPDFF.IMG_ID_COLUMN_CODE = "CH_FU_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_REFERRALS_PHYSICAL_THERAPIST
                if (CHK_REFERRALS_PHYSICAL_THERAPIST.Checked == true)
                {
                    
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "PT";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMPT";
                    SPDFF.sz_XMLPath = "Refferal_IM_FOLLOWUP_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);
                    


                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMPT";
                    SPDFF.SZ_SPECIALITY_NAME ="PT";
                    SPDFF.IMG_ID_COLUMN_CODE = "PT_FU_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_REFERRALS_OCCUPATIONAL_THERAPIST
                if (CHK_REFERRALS_OCCUPATIONAL_THERAPIST.Checked == true)
                {

                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "OT";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMOT";
                    SPDFF.sz_XMLPath = "Refferal_IM_FOLLOWUP_XML_Path";
                    SPDFF.sz_PDFPath = "CO_Refferal_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMOT";
                    SPDFF.SZ_SPECIALITY_NAME = "OT";
                    SPDFF.IMG_ID_COLUMN_CODE = "OT_FU_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_TEST_EMG_NCV.Checked
                if (CHK_TEST_EMG_NCV.Checked == true)
                {

                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "EMG";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMEMG";
                    SPDFF.sz_XMLPath = "MST_ECG_IM_FOLLOWUP_XML_Path";
                    SPDFF.sz_PDFPath = "MST_ECG_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMEMG";
                    SPDFF.SZ_SPECIALITY_NAME = "EMG";
                    SPDFF.IMG_ID_COLUMN_CODE = "VSNCT_FU_REFERRAL";
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    SPDFF.savePDFForminDocMang(SPDFF);
                }

                //CHK_SUPPLIES_EMS_UNIT TO CHK_SUPPLIES_BIOFEEDBACK_TRAINING_SESSIONS
                if ((CHK_SUPPLIES_EMS_UNIT.Checked==true)  || (CHK_SUPPLIES_RT_LT_WRIST_SUPPORT.Checked==true)  || (CHK_SUPPLIES_CERVICAL_PILLOW.Checked==true)  || (CHK_SUPPLIES_RT_LT_ELBOW_SUPPORT.Checked==true)  || (CHK_SUPPLIES_LUMBOSACRAL_BACK_SUPPORT.Checked==true)  || (CHK_SUPPLIES_RT_LT_ANKLE_SUPPORT.Checked==true)  || (CHK_SUPPLIES_LUMBAR_CUSHION.Checked==true)  || (CHK_SUPPLIES_RT_LT_KNEE_SUPPORT.Checked==true)  || (CHK_SUPPLIES_MASSAGER.Checked==true)  || (CHK_SUPPLIES_CANE.Checked==true)  || (CHK_SUPPLIES_ROM_TESTING.Checked==true)  || (CHK_SUPPLIES_BIOFEEDBACK_TRAINING_SESSIONS.Checked==true))
                {

                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    SPDFF.SZ_SPECIALITY_NAME = "SP";
                    SPDFF.SZ_SPECIALITY_CODE = "NFMSP";
                    SPDFF.sz_XMLPath = "Supplies_XML_Path";
                    SPDFF.sz_PDFPath = "Supplies_PDF_Path";
                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    objal1 = SPDFF.FillPDFValue(SPDFF);



                    SPDFF.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                    SPDFF.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    SPDFF.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                    SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                    SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                    SPDFF.SZ_SPECIALITY_CODE = "NFMSP";
                    SPDFF.SZ_SPECIALITY_NAME ="SP";
                    SPDFF.IMG_ID_COLUMN_CODE = "SUPPLIES_FU_REFERRAL";
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
        Response.Redirect("~/Bill_Sys_FUIM_Doctors_opinion.aspx");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
         
}
