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

public partial class Bill_Sys_CO_HIPPA : PageBase
{
    string CaseBarcodePath = "";
    Bill_Sys_NF3_Template objNF3Template;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["HIPPA_CaseID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
        TXT_I_EVENT.Text = Session["HIPPA_CaseID"].ToString();
     
        if (!Page.IsPostBack)
        {
            LoadData();
            LoadPatientInformation();
        }
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
            _objsave.Xml_File = "HIPPA.xml";
            _objsave.SaveMethod();
            LoadData();
            ArrayList objal = new ArrayList();
            SpecialityPDFFill spf = new SpecialityPDFFill();
            spf.sz_EventID = Session["HIPPA_CaseID"].ToString();
            spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            spf.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            spf.SZ_SPECIALITY_NAME = "HIPAA Forms";
            spf.SZ_SPECIALITY_CODE = "INHIP";
            spf.sz_XMLPath = "HIPPA_XML_Path";
            spf.sz_PDFPath = "HIPPA_PDF_Path";
            spf.sz_Session_Id = Session["HIPPA_CaseID"].ToString();
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            objal = spf.FillPDFValue(spf);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + objal[1] + "'); ", true);


            spf.sz_Session_Id = Session["HIPPA_CaseID"].ToString();
            spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            spf.sz_EventID = "";
            spf.SZ_PT_FILE_NAME = objal[0].ToString();
            spf.SZ_PT_FILE_PATH = objal[2].ToString();
            spf.SZ_SPECIALITY_CODE = "INHIP";
            spf.SZ_SPECIALITY_NAME = "HIPAA Forms";
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
            _objEdit.Xml_File = "HIPPA.xml";
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


    public void LoadPatientInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = TXT_I_EVENT.Text; 
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "AOBPatientInformation.xml";
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
    //public string FillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
    //{
    //    string sz_CaseID = "1";
    //    PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();

    //    string strGenFileName = "";
    //    try
    //    {
    //        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
    //        string xmlpath = ConfigurationManager.AppSettings["HIPPA_XML_Path"].ToString();
    //        string pdfpath = ConfigurationManager.AppSettings["HIPPA_PDF_Path"].ToString();

    //        String szDefaultPath = "PROCOMSYS" + "/" + sz_CaseID + "/Packet Document/";
    //        String szDestinationDir = "PROCOMSYS" + "/" + sz_CaseID + "/No Fault File/Medicals/CH/";

    //        strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, EventID, "PROCOMSYS", sz_CaseID);

    //        if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName))
    //        {
    //            if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
    //            {
    //                Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
    //            }
    //            File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName, objNF3Template.getPhysicalPath() + szDestinationDir + strGenFileName);
    //        }

    //        //Bill_Sys_CheckoutBO objCheckoutBO = new Bill_Sys_CheckoutBO();
    //        //ArrayList objAL = new ArrayList();
    //        //objAL.Add(sz_CaseID);
    //        //objAL.Add(strGenFileName);
    //        //objAL.Add(szDestinationDir);
    //        //objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
    //        //objAL.Add(sz_CompanyID);
    //        //objAL.Add(EventID);
    //        //objCheckoutBO.save_CHIRO_DocMang(objAL);
    //        // Open file
    //        //  String szOpenFilePath = "";
    //        //  szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
    //        //  Response.Redirect(szOpenFilePath, false);
    //        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
    //    }
    //   catch (Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    }
    //    return strGenFileName;
    //}


    protected void btnSavePrint_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_CheckoutBO _objcheckOut = new Bill_Sys_CheckoutBO();
        objNF3Template = new Bill_Sys_NF3_Template();
        string sz_CompanyID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
        string sz_EventID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;

        try
        {
            SaveOperation _objsave = new SaveOperation();
            _objsave.WebPage = this.Page;
            _objsave.Xml_File = "HIPPA.xml";
            _objsave.SaveMethod();

            //#region" "Barcode functionality
            //DataSet dset = new DataSet();
            //dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);
            //string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
            //string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
            //string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
            //String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Signs/";
            //if (!Directory.Exists(sz_BarcodeImagePath))
            //{
            //    Directory.CreateDirectory(sz_BarcodeImagePath);
            //}
            //SpecialityPDFBO pdfbo = new SpecialityPDFBO();
            //CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);
            //#endregion

            //_objcheckOut.LienPatientPath(sz_EventID, "", CaseBarcodePath, "", "");
            //_objcheckOut.MRI_Questionary_SaveSignPath(sz_EventID, CaseBarcodePath, "");
            PrintAOBFillPDFValue(sz_EventID, sz_CompanyID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
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

    public string PrintAOBFillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
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
            string xmlpath = ConfigurationManager.AppSettings["HIPPA_XML_Path"].ToString();
            string pdfpath = ConfigurationManager.AppSettings["HIPPA_PDF_Path"].ToString();
            string CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            String szDefaultPath = CompanyName + "/" + sz_CaseID + "/Packet Document/";
            String szDestinationDir = CompanyName + "/" + sz_CaseID + "/HIPPA/";

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
        
        return strGenFileName;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
}

