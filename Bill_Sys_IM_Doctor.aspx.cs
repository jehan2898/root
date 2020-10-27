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
using SIGPLUSLib;
using System.IO;
using PDFValueReplacement;
using Vintasoft.Barcode;
using Vintasoft.Pdf;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class Bill_Sys_Image_Save : PageBase
{
    System.Drawing.Image img;
    string DoctorImagePath = "";
    string PatientPath = "";
    string CaseBarcodePath = "";
    string sz_CompanyID = "";
    string sz_CompanyName = "";
    string sz_EventID = "";
    Bill_Sys_NF3_Template objNF3Template;
    Bill_Sys_CheckoutBO _objcheckOut;
    
    protected void Page_Load(object sender, EventArgs e)   
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _objcheckOut = new Bill_Sys_CheckoutBO();
        DigitalSign signobj = new DigitalSign();        
        objNF3Template = new Bill_Sys_NF3_Template();
        DataSet dsObj = _objcheckOut.PatientName(Session["IMEventID"].ToString());
        Session["IM_InicialCaseID"] = dsObj.Tables[0].Rows[0][1].ToString();
        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        sz_EventID = Session["IMEventID"].ToString();       
        int flag = 0;
        try
        {
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["IM_InicialCaseID"].ToString() + "/Signs/";
            if (!Directory.Exists(szDefaultPath))
            {
                Directory.CreateDirectory(szDefaultPath);
            }
            szDefaultPath = szDefaultPath + sz_EventID;
            DoctorImagePath = szDefaultPath + "_DoctorSign.jpg";
            signobj.SignSave(Request.Form["hidden"], DoctorImagePath);

            #region" "Barcode functionality
                DataSet dset = new DataSet();
                dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);
                string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
                string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
                string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
                String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["IM_InicialCaseID"].ToString() + "/Signs/";    
                SpecialityPDFBO pdfbo = new SpecialityPDFBO();
                CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);
            #endregion

                _objcheckOut.UpdateIMSignPath(sz_EventID, DoctorImagePath, CaseBarcodePath);
                SpecialityPDFFill spf = new SpecialityPDFFill();
                ArrayList obj = new ArrayList();
                spf.sz_EventID = Session["IMEventID"].ToString();
                spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                spf.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;

                spf.sz_XMLPath = "IM_XML_Path";
                spf.sz_PDFPath = "IM_PDF_Path";

                spf.sz_Session_Id = Session["IM_InicialCaseID"].ToString();
                spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                spf.SZ_SPECIALITY_NAME = "IM";
                obj = spf.FillPDFValue(spf);                
                Response.Redirect(obj[1].ToString(), false);
                spf.sz_Session_Id = Session["IM_InicialCaseID"].ToString(); ;
                spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                spf.sz_EventID = Session["IMEventID"].ToString();
                spf.SZ_PT_FILE_NAME = obj[0].ToString();
                spf.SZ_PT_FILE_PATH = obj[2].ToString();
                spf.SZ_SPECIALITY_CODE = "NFMIM";
                spf.SZ_SPECIALITY_NAME = "IM";
                spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                spf.savePDFForminDocMang(spf);
                
            //Code To generate pdf according to check box value and Visit Status 
                Bill_Sys_CheckoutBO ChkBo = new Bill_Sys_CheckoutBO();
                if (ChkBo.ChekVisitStaus(Convert.ToInt32(Session["IMEventID"].ToString()), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == "2")
                {
                    ArrayList ObjalGetVal = new ArrayList();
                    ArrayList objal1 = new ArrayList();
                    SpecialityPDFFill SPDFF = new SpecialityPDFFill();
                    ObjalGetVal = ChkBo.ChekIMCheckBoxStaus(Convert.ToInt32(Session["IMEventID"].ToString()));
                    //CHK_REFERRALS_CHIROPRACTOR
                    if (ObjalGetVal[0].ToString().Equals("True"))
                    {
                        SPDFF.sz_EventID = Session["IMEventID"].ToString();
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
                        SPDFF.sz_EventID = Session["IMEventID"].ToString();
                        SPDFF.SZ_PT_FILE_NAME = objal1[0].ToString();
                        SPDFF.SZ_PT_FILE_PATH = objal1[2].ToString();
                        SPDFF.SZ_SPECIALITY_CODE = "NFMCH";
                        SPDFF.SZ_SPECIALITY_NAME = "CH";
                        SPDFF.IMG_ID_COLUMN_CODE = "CH_IE_REFERRAL";
                        SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                        SPDFF.savePDFForminDocMang(SPDFF);
                    }
                    //CHK_REFERRALS_PHYSICAL_THERAPIST
                    if (ObjalGetVal[1].ToString().Equals("True"))
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

                    //CHK_REFERRALS_OCCUPATIONAL_THERAPIST
                    if (ObjalGetVal[2].ToString().Equals("True"))
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

                    //CHK_TEST_EMG_NCV
                    if (ObjalGetVal[13].ToString().Equals("True"))
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
                    if (ObjalGetVal[3].ToString().Equals("True") || ObjalGetVal[4].ToString().Equals("True") || ObjalGetVal[5].ToString().Equals("True") || ObjalGetVal[6].ToString().Equals("True") || ObjalGetVal[7].ToString().Equals("True") || ObjalGetVal[8].ToString().Equals("True") || ObjalGetVal[9].ToString().Equals("True") || ObjalGetVal[10].ToString().Equals("True") || ObjalGetVal[11].ToString().Equals("True") || ObjalGetVal[12].ToString().Equals("True"))
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
            else
            {
                //signature has not been returned successfully!
            }
        }
        catch (Exception ex)
        {
           Label1.Text =  "Page Laod :" + ex.ToString();
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
            cv.MakeReadOnlyPage("Bill_Sys_Image_Save.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
