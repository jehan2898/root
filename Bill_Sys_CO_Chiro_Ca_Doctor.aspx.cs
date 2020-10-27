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
    string DoctorImagePathlogical = "";
    string PatientPath = "";
    string CaseBarcodePath = "";
    string sz_CompanyID = "";
    string sz_CompanyName = "";
    string sz_EventID = "";
    Bill_Sys_NF3_Template objNF3Template;
    Bill_Sys_CheckoutBO _objcheckOut;
    string DoctorImagePath = "";

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
       // DataSet dsObj = _objcheckOut.PatientName(Session["IMEventID"].ToString());
        Session["ChkCaseID"] = ((SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"]).CaseID;
        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        sz_EventID = ((SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"]).EventID;
        int flag = 0;
        try
        {
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkCaseID"].ToString() + "/Signs/";
            //Added By-Sunil
            string SaveSignPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkCaseID"].ToString() + "/Signs/";
            //end
            if (!Directory.Exists(szDefaultPath))
            {
                Directory.CreateDirectory(szDefaultPath);
            }
            szDefaultPath = szDefaultPath + sz_EventID;
            //Added By-Sunil
            SaveSignPath = SaveSignPath + sz_EventID;
            DoctorImagePathlogical = SaveSignPath + "_DoctorSign.jpg";
            //end

            DoctorImagePath = szDefaultPath + "_DoctorSign.jpg";
            signobj.SignSave(Request.Form["hiddenPatient"], szDefaultPath + "_Patient.jpg");
            //_objcheckOut.UpdatePatientChairoSign(sz_EventID, szDefaultPath + "_Patient.jpg");
            _objcheckOut.UpdatePatientChairoSign(sz_EventID, SaveSignPath + "_Patient.jpg");
            signobj.SignSave(Request.Form["hidden"], DoctorImagePath);

            #region" "Barcode functionality
            DataSet dset = new DataSet();
            dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);
            string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
            string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
            string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
            String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkCaseID"].ToString() + "/Signs/";
            //Added By-Sunil
            string sz_BarcodeImagePathlogical = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkCaseID"].ToString() + "/Signs/";
            //end
            SpecialityPDFBO pdfbo = new SpecialityPDFBO();
            CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);
            //Added By-Sunil
            string CaseBarcodePathlogical = sz_BarcodeImagePathlogical + "BarcodeImg.jpg";
            //end
            #endregion

           // _objcheckOut.UpdateChairoSign(sz_EventID, DoctorImagePath, CaseBarcodePath);
            _objcheckOut.UpdateChairoSign(sz_EventID, DoctorImagePathlogical, CaseBarcodePathlogical);
            FillPDFValue(sz_EventID, sz_CompanyID, sz_CompanyName);
          #region
            //string sz_CaseID = ((SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"]).CaseID;
            //PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
            //string pdffilepath;
            //string strGenFileName = "";
            //try
            //{
            //   // Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            //    string xmlpath = ConfigurationManager.AppSettings["MST_CHIRO_CA_XML_Path"].ToString();
            //    string pdfpath = ConfigurationManager.AppSettings["MST_CHIRO_CA_PDF_Path"].ToString();

            //     szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
            //    String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/No Fault File/Medicals/CH/";

            //    strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, sz_EventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, sz_CaseID);

            //    if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName))
            //    {
            //        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
            //        {
            //            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
            //        }
            //        File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName, objNF3Template.getPhysicalPath() + szDestinationDir + strGenFileName);
            //    }

            //    Bill_Sys_CheckoutBO objCheckoutBO = new Bill_Sys_CheckoutBO();
            //    ArrayList objAL = new ArrayList();
            //    objAL.Add(sz_CaseID);
            //    objAL.Add(strGenFileName);
            //    objAL.Add(szDestinationDir);
            //    objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]) + sz_CaseID);
            //    objAL.Add(sz_CompanyID);
            //    objAL.Add(sz_EventID);
            //    objCheckoutBO.save_CHIRO_DocMang(objAL);
            //    // Open file
            //    String szOpenFilePath = "";
            //    szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
            //    Response.Redirect(szOpenFilePath, false);
            //    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
          
            //}
            //catch (Exception _ex)
            //{
            //    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //}
           #endregion

        }
        catch (Exception ex)
        {
            Label1.Text = "Page Laod :" + ex.ToString();

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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        #endregion
    }
    public string FillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string sz_CaseID = ((SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"]).CaseID;
        PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
        string pdffilepath;
        string strGenFileName = "";
        try
        {
            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            string xmlpath = ConfigurationManager.AppSettings["MST_CHIRO_CA_XML_Path"].ToString();
            string pdfpath = ConfigurationManager.AppSettings["MST_CHIRO_CA_PDF_Path"].ToString();

            String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
            String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/No Fault File/Medicals/CH/";

            strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, EventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, sz_CaseID);

            if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName))
            {
                if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                {
                    Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                }
                File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName, objNF3Template.getPhysicalPath() + szDestinationDir + strGenFileName);
            }

            Bill_Sys_CheckoutBO objCheckoutBO = new Bill_Sys_CheckoutBO();
            ArrayList objAL = new ArrayList();
            objAL.Add(sz_CaseID);
            objAL.Add(strGenFileName);
            objAL.Add(szDestinationDir);
            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]) + sz_CaseID);
            objAL.Add(sz_CompanyID);
            objAL.Add(EventID);
            objCheckoutBO.save_CHIRO_DocMang(objAL);
            // Open file
            String szOpenFilePath = "";
            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
            Response.Redirect(szOpenFilePath, false);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
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
       
        return strGenFileName;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
