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
        Session["AOB_CaseID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID; 
        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        sz_EventID = Session["AOB_CaseID"].ToString();
        int flag = 0;
        try
        {
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["AOB_CaseID"].ToString() + "/Signs/";
            //Added By-Sunil
            string SaveSignPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["AOB_CaseID"].ToString() + "/Signs/";
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
            string DoctorImagePathlogical = SaveSignPath + "_DoctorSign.jpg";
            signobj.SignSave(Request.Form["hiddenPatient"], szDefaultPath + "_Patient.jpg");
            //_objcheckOut.UpdatePatientAOBSign(sz_EventID, szDefaultPath + "_Patient.jpg");
            _objcheckOut.UpdatePatientAOBSign(sz_EventID, SaveSignPath + "_Patient.jpg");
            signobj.SignSave(Request.Form["hidden"], DoctorImagePath);

            #region" "Barcode functionality
            DataSet dset = new DataSet();
            dset = _objcheckOut.GetIntakeSheetNodeID(sz_CompanyID, sz_EventID, "NFAOB");
            string sz_NodeId = dset.Tables[0].Rows[0][0].ToString();
            string sz_CaseId = sz_EventID.ToString();
            string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
            String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["AOB_CaseID"].ToString() + "/Signs/";
            string sz_BarcodeImagePathlogical = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["AOB_CaseID"].ToString() + "/Signs/";
            SpecialityPDFBO pdfbo = new SpecialityPDFBO();
            CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);
            #endregion
            //Added By-Sunil
            string CaseBarcodePathlogical = sz_BarcodeImagePathlogical + "BarcodeImg.jpg";
            //end
            //_objcheckOut.UpdateAOBSign(sz_EventID, DoctorImagePath, CaseBarcodePath);
            _objcheckOut.UpdateAOBSign(sz_EventID, DoctorImagePathlogical, CaseBarcodePathlogical);
          

            ArrayList objal = new ArrayList();
            SpecialityPDFFill spf = new SpecialityPDFFill();
            spf.sz_EventID = Session["AOB_CaseID"].ToString();
            spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            spf.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            spf.SZ_SPECIALITY_NAME = "AOB";
            spf.SZ_SPECIALITY_CODE = "NFAOB";
            spf.sz_XMLPath = "AOB_XML_Path";
            spf.sz_PDFPath = "AOB_PDF_Path";
            spf.sz_Session_Id = Session["AOB_CaseID"].ToString();
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            objal = spf.FillPDFValue(spf);
            Response.Redirect(objal[1].ToString(),false);

            spf.sz_Session_Id = Session["AOB_CaseID"].ToString();
            spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            spf.sz_EventID = "";
            spf.SZ_PT_FILE_NAME = objal[0].ToString();
            spf.SZ_PT_FILE_PATH = objal[2].ToString();
            spf.SZ_SPECIALITY_CODE = "NFAOB";
            spf.SZ_SPECIALITY_NAME = "AOB";
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            spf.savePDFForminDocMang(spf);
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
            Response.Write("Exp Msg= " + ex);
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
  //public string FillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
  //  {
  //      string sz_CaseID = ((SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"]).CaseID;
  //      PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
  //      string pdffilepath;
  //      string strGenFileName = "";
  //      try
  //      {
  //          Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
  //          string xmlpath = ConfigurationManager.AppSettings["AOB_XML_Path"].ToString();
  //          string pdfpath = ConfigurationManager.AppSettings["AOB_PDF_Path"].ToString();

  //          String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
  //          String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/No Fault File/AOB/";

  //          strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, EventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, sz_CaseID);

  //          if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName))
  //          {
  //              if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
  //              {
  //                  Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
  //              }
  //              File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName, objNF3Template.getPhysicalPath() + szDestinationDir + strGenFileName);
  //          }

  //          Bill_Sys_CheckoutBO objCheckoutBO = new Bill_Sys_CheckoutBO();
  //          ArrayList objAL = new ArrayList();
  //          objAL.Add(sz_CaseID);
  //          objAL.Add(strGenFileName);
  //          objAL.Add(szDestinationDir);
  //          objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]) + sz_CaseID);
  //          objAL.Add(sz_CompanyID);
  //          objAL.Add(EventID);
  //          objCheckoutBO.save_CHIRO_DocMang(objAL);
  //          // Open file
  //          String szOpenFilePath = "";
  //          szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
  //          Response.Redirect(szOpenFilePath, false);
  //          //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
  //      }
  //     catch (Exception ex)
  //      {
  //          Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
  //      }
  //      return strGenFileName;
  //  }
}
