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
using System.Data.Sql;
using System.Data.SqlClient;
using PDFValueReplacement;
using System.IO;
public partial class Bill_Sys_CO_Patient_Intake4 : PageBase
{
    string CaseBarcodePath = "";
    Bill_Sys_NF3_Template objNF3Template;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["PATIENT_INTEK_CASE_ID"] = "1179";
        if (Request.QueryString["EID"] != null)
        {
            Session["PATIENT_INTEK_CASE_ID"] = Request.QueryString["EID"].ToString();
        }
        Session["PATIENT_INTEK_CASE_ID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
       TXT_I_EVENT.Text = Session["PATIENT_INTEK_CASE_ID"].ToString();
        if (!Page.IsPostBack)
        {
            LoadData();
        }
    }
    public void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            EditOperation _objEdit = new EditOperation();
            _objEdit.Primary_Value = TXT_I_EVENT.Text;
            _objEdit.WebPage = this.Page;
            _objEdit.Xml_File = "Patient_Intake4.xml";
            _objEdit.LoadData();

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
        Response.Redirect("Bill_Sys_CO_Patient_Intake3.aspx");
    }
   
    protected void css_btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveOperation _objsave = new SaveOperation();
            _objsave.WebPage = this.Page;
            _objsave.Xml_File = "Patient_Intake4.xml";
            _objsave.SaveMethod();
           // LoadData();
            Bill_Sys_CheckoutBO _objsign = new Bill_Sys_CheckoutBO();
            if (_objsign.PatientIntekSignPath(TXT_I_EVENT.Text.ToString()))
            {
                String ch_EventID = TXT_I_EVENT.Text;
                FillPDFValue(ch_EventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);

            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_Patient_Intek_Signature.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=yes,width=500,height=350'); ", true);
            }
          //  FillPDFValue(TXT_I_EVENT.Text, "1", "PROCOMSYS");
        
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
    public string FillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string sz_CaseID = Session["PATIENT_INTEK_CASE_ID"].ToString();
        PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();

        string strGenFileName = "";
        try
        {
            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            string xmlpath = ConfigurationManager.AppSettings["PatientIntake_XML_Path"].ToString();
            string pdfpath = ConfigurationManager.AppSettings["PatientIntake_PDF_Path"].ToString();
            string CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            String szDefaultPath = CompanyName + "/" + sz_CaseID + "/Packet Document/";
            String szDestinationDir = CompanyName + "/" + sz_CaseID + "/Intake Sheet/";

            strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, sz_CaseID, CompanyName, sz_CaseID);

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
            SpecialityPDFFill spf = new SpecialityPDFFill();
            spf.sz_Session_Id = Session["PATIENT_INTEK_CASE_ID"].ToString();
            spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //spf.sz_EventID = Session["AOB_EVENT_ID"].ToString();
            spf.SZ_PT_FILE_NAME = strGenFileName;
            spf.SZ_PT_FILE_PATH = szDestinationDir;
            spf.SZ_SPECIALITY_CODE = "ININT";
            spf.SZ_SPECIALITY_NAME = "Intek Sheet";
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            spf.savePDFForminDocMang(spf);
            spf.SZ_SPECIALITY_CODE = "ININT";
            spf.SZ_SPECIALITY_NAME = "Intek Sheet";
            //  objCheckoutBO.savePDFForminDocMang(objAL);
            // Open file
            String szOpenFilePath = "";
            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
          //  Response.Redirect(szOpenFilePath, false);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
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


    protected void btnSavePrint_Click(object sender, EventArgs e)
    {
        Bill_Sys_CheckoutBO _objcheckOut = new Bill_Sys_CheckoutBO();
        objNF3Template = new Bill_Sys_NF3_Template();
        string sz_CompanyID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
        string sz_EventID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            SaveOperation _objsave = new SaveOperation();
            _objsave.WebPage = this.Page;
            _objsave.Xml_File = "Patient_Intake4.xml";
            _objsave.SaveMethod();

            #region" "Barcode functionality
            DataSet dset = new DataSet();
            dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);
            string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
            string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
            string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
            String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Signs/";
            //Added By-Sunil
            string sz_BarcodeImagePathlogical = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["PATIENT_LINE_CASE_ID"].ToString() + "/Signs/";
            //end
            //Added By-Sunil
            string CaseBarcodePathlogical = sz_BarcodeImagePathlogical + "BarcodeImg.jpg";
            //end
            if (!Directory.Exists(sz_BarcodeImagePath))
            {
                Directory.CreateDirectory(sz_BarcodeImagePath);
            }
            SpecialityPDFBO pdfbo = new SpecialityPDFBO();
            CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);
            #endregion

            //_objcheckOut.LienPatientPath(sz_EventID, "", CaseBarcodePath, "", "");
           // _objcheckOut.UpdatePatientIntekSign(sz_EventID, "", CaseBarcodePath);
            //Added BY--Sunil
            _objcheckOut.UpdatePatientIntekSign(sz_EventID, "", CaseBarcodePathlogical);
            //End
            PrintFillPDFValue(sz_EventID, sz_CompanyID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
        }
        catch (Exception ex)
        {
            Response.Write("Exp Msg= " + ex);
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

    public string PrintFillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string sz_CaseID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();

        string strGenFileName = "";
        try
        {
            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            string xmlpath = ConfigurationManager.AppSettings["PatientIntake_XML_Path"].ToString();
            string pdfpath = ConfigurationManager.AppSettings["PatientIntake_PDF_Path"].ToString();
            string CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            String szDefaultPath = CompanyName + "/" + sz_CaseID + "/Packet Document/";
            String szDestinationDir = CompanyName + "/" + sz_CaseID + "/Intake Sheet/";

            strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, sz_CaseID, CompanyName, sz_CaseID);

            if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName))
            {
                if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                {
                    Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                }
                File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName, objNF3Template.getPhysicalPath() + szDestinationDir + strGenFileName);
            }
            String szOpenFilePath = "";
            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
            //Response.Redirect(szOpenFilePath, false);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
        }
        catch (Exception ex)
        {
            Response.Write("Exp Msg= " + ex);
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
        return strGenFileName;
    }

}
