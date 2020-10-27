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

using PDFValueReplacement;
using Vintasoft.Barcode;
using Vintasoft.Pdf;
using System.Drawing;
using System.Drawing.Drawing2D;
using SIGPLUSLib;
using System.IO;


public partial class Bill_Sys_MRI_Patient_Signature : PageBase
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
        SIGPLUSLib.SigPlus sigObj_Patient1 = new SIGPLUSLib.SigPlus();
        objNF3Template = new Bill_Sys_NF3_Template();
        // DataSet dsObj = _objcheckOut.PatientName(Session["IMEventID"].ToString());
        //Session["ChkCaseID"] = ((SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"]).CaseID;

        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
        {
            sz_CompanyID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            Bill_Sys_NF3_Template obj1 = new Bill_Sys_NF3_Template();
            sz_CompanyName = obj1.GetCompanyName(sz_CompanyID);
        }
        else
        {
            sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        }
      
        sz_EventID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
        try
        {
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + sz_CompanyName + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Signs/";
            //Added By-Sunil
            string SaveSignPath = sz_CompanyName + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Signs/";
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
            string sz_Patient_path = szDefaultPath + "_Patient.jpg";
            string sz_Patient_path_logical = SaveSignPath + "_Patient.jpg";
            signobj.SignSave(Request.Form["hiddenPatient"], sz_Patient_path);
            
            #region" "Barcode functionality
            DataSet dset = new DataSet();
            dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);
            string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
            string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
            string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
            String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + sz_CompanyName + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Signs/";
            //Added By-Sunil
            string sz_BarcodeImagePathlogical = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["PATIENT_INTEK_CASE_ID"].ToString() + "/Signs/";
            //end
            //Added By-Sunil
            string CaseBarcodePathlogical = sz_BarcodeImagePathlogical + "BarcodeImg.jpg";
            //end
            SpecialityPDFBO pdfbo = new SpecialityPDFBO();
            CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);
            #endregion
           // _objcheckOut.MRI_Questionary_SaveSignPath(sz_EventID, CaseBarcodePath, sz_Patient_path);
            //Added By--Sunil
            _objcheckOut.MRI_Questionary_SaveSignPath(sz_EventID, CaseBarcodePathlogical, sz_Patient_path_logical);
            //end
            SpecialityPDFFill spf = new SpecialityPDFFill();
            ArrayList obj = new ArrayList();
            spf.sz_EventID = "";
            spf.sz_CompanyID = sz_CompanyID;
            spf.sz_CompanyName = sz_CompanyName;

            spf.sz_XMLPath = "MRI_QUESTIONAIRE_XML_Path";
            spf.sz_PDFPath = "MRI_QUESTIONAIRE_PDF_Path";

            spf.sz_Session_Id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            spf.SZ_SPECIALITY_NAME = "MRI";
            obj = FillPDFValue(spf);
            Response.Redirect(obj[1].ToString(), false);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + obj[1] + "'); ", true);
            Response.Write("<script>window.open('" + obj[1] + "')</script>");

            spf.sz_Session_Id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID; 
            spf.sz_CompanyID = sz_CompanyID;
            spf.sz_EventID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            spf.SZ_PT_FILE_NAME = obj[0].ToString();
            spf.SZ_PT_FILE_PATH = obj[2].ToString();
            spf.SZ_SPECIALITY_CODE = "ININT";
            spf.SZ_SPECIALITY_NAME = "Intake Sheet";
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            spf.savePDFForminDocMang(spf);

            
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

    public ArrayList FillPDFValue(SpecialityPDFFill p_objSepcialityPDF)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string sz_CaseID = p_objSepcialityPDF.sz_Session_Id;
        PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
        string pdffilepath;
        string strGenFileName = "";
        String szOpenFilePath = "";
        String szDestinationDir = "";
        try
        {
            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            string xmlpath = ConfigurationManager.AppSettings[p_objSepcialityPDF.sz_XMLPath].ToString();
            string pdfpath = ConfigurationManager.AppSettings[p_objSepcialityPDF.sz_PDFPath].ToString();

            String szDefaultPath = p_objSepcialityPDF.sz_CompanyName + "/" + sz_CaseID + "/Packet Document/";
            szDestinationDir = p_objSepcialityPDF.sz_CompanyName + "/" + sz_CaseID + "/MRI/";

            strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, sz_EventID, p_objSepcialityPDF.sz_CompanyName, sz_CaseID);

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
            objAL.Add(p_objSepcialityPDF.SZ_USER_NAME);
            objAL.Add(sz_CompanyID);
            objAL.Add(sz_EventID);


            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
            ArrayList al = new ArrayList();
            al.Add(strGenFileName);
            al.Add(szOpenFilePath);
            al.Add(szDestinationDir);
            return al;

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
        return null;

    }
}
