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
    string DoctorImagePathlogical = "";
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
        DataSet dsObj = _objcheckOut.PatientName(Session["IM_FW_EVENT_ID"].ToString());
        Session["IM_FOLLOWUP_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString();
        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        sz_EventID = Session["IM_FW_EVENT_ID"].ToString();       
        int flag = 0;
        try
        {
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["IM_FOLLOWUP_CaseID"].ToString() + "/Signs/";
            //Added By-Sunil
            string SaveSignPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["IM_FOLLOWUP_CaseID"].ToString() + "/Signs/";
            //end
            if (!Directory.Exists(szDefaultPath))
            {
                Directory.CreateDirectory(szDefaultPath);
            }
            szDefaultPath = szDefaultPath + sz_EventID;
            //Added By-Sunil
            SaveSignPath = SaveSignPath + sz_EventID;
            //end
            DoctorImagePath = szDefaultPath + "_DoctorSign.jpg";
            DoctorImagePathlogical = SaveSignPath + "_DoctorSign.jpg";
            signobj.SignSave(Request.Form["hidden"], DoctorImagePath);

            #region" "Barcode functionality
                DataSet dset = new DataSet();
                dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);
                string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
                string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
                string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
                String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["IM_FOLLOWUP_CaseID"].ToString() + "/Signs/";    
                SpecialityPDFBO pdfbo = new SpecialityPDFBO();
                //Added By-Sunil
                string sz_BarcodeImagePathlogical = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["AC_Followup_CaseID"].ToString() + "/Signs/";
                //end
                //Added By-Sunil
                string CaseBarcodePathlogical = sz_BarcodeImagePathlogical + "BarcodeImg.jpg";
                //end
                CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);
            #endregion
        
                //_objcheckOut.SaveIMFollowupSignPath(sz_EventID, DoctorImagePath, CaseBarcodePath);
                _objcheckOut.SaveIMFollowupSignPath(sz_EventID, DoctorImagePathlogical, CaseBarcodePathlogical);
                SpecialityPDFFill spf = new SpecialityPDFFill();
                ArrayList obj = new ArrayList();
                spf.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                spf.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;

                spf.sz_XMLPath = "IM_FOLLOWUP_XML_Path";
                spf.sz_PDFPath = "IM_FOLLOWUP_PDF_Path";

                spf.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                spf.SZ_SPECIALITY_NAME = "IM";
                obj = spf.FillPDFValue(spf);                
                Response.Redirect(obj[1].ToString(), false);
                spf.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString(); ;
                spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                spf.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                spf.SZ_PT_FILE_NAME = obj[0].ToString();
                spf.SZ_PT_FILE_PATH = obj[2].ToString();
                spf.SZ_SPECIALITY_CODE = "NFMIM";
                spf.SZ_SPECIALITY_NAME = "IM";
                spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                spf.savePDFForminDocMang(spf);
                
            //Code To generate pdf according to check box value and Visit Status 
                Bill_Sys_CheckoutBO ChkBo = new Bill_Sys_CheckoutBO();
                if (ChkBo.ChekVisitStaus(Convert.ToInt32(Session["IM_FW_EVENT_ID"].ToString()), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == "2")
                {
                    ArrayList ObjalGetVal = new ArrayList();
                    ArrayList objal1 = new ArrayList();
                    SpecialityPDFFill SPDFF = new SpecialityPDFFill();
                    ObjalGetVal = ChkBo.ChekCheckBoxStaus(Convert.ToInt32(Session["IM_FW_EVENT_ID"].ToString()));
                    //CHK_REFERRALS_CHIROPRACTOR
                    if (ObjalGetVal[0].ToString().Equals("True"))
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
                    if (ObjalGetVal[1].ToString().Equals("True"))
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
                        SPDFF.SZ_SPECIALITY_NAME = "PT";
                        SPDFF.IMG_ID_COLUMN_CODE = "PT_FU_REFERRAL";
                        SPDFF.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                        SPDFF.savePDFForminDocMang(SPDFF);
                    }

                    //CHK_REFERRALS_OCCUPATIONAL_THERAPIST
                    if (ObjalGetVal[14].ToString().Equals("True"))
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

                    //CHK_TEST_EMG_NCV
                    if (ObjalGetVal[15].ToString().Equals("True"))
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
                    if (ObjalGetVal[2].ToString().Equals("True") || ObjalGetVal[3].ToString().Equals("True") || ObjalGetVal[4].ToString().Equals("True") || ObjalGetVal[5].ToString().Equals("True") || ObjalGetVal[6].ToString().Equals("True") || ObjalGetVal[7].ToString().Equals("True") || ObjalGetVal[8].ToString().Equals("True") || ObjalGetVal[9].ToString().Equals("True") || ObjalGetVal[10].ToString().Equals("True") || ObjalGetVal[11].ToString().Equals("True") || ObjalGetVal[12].ToString().Equals("True") || ObjalGetVal[13].ToString().Equals("True"))
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
                        SPDFF.SZ_SPECIALITY_NAME = "SP";
                        SPDFF.IMG_ID_COLUMN_CODE = "SUPPLIES_FU_REFERRAL";
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
