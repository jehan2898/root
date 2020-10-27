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

public partial class Bill_Sys_SelectBillType : PageBase
{
    private Bill_Sys_NF3_Template _bill_Sys_NF3_Template;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
            
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
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_SelectBillType.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
   

    protected void drpSelectBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
        try
        {
            String szURLDocumentManager = "";
            szURLDocumentManager = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString();
            String szDefaultPhysicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";

        if (drpSelectBill.SelectedValue == "01")
        {
            string strGenFileName = "";
            PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
            string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
            strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
            strGenFileName = lfnMergeDiagCodePage(szDefaultPhysicalPath, strGenFileName,4);
            ArrayList objAL = new ArrayList();
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/" + strGenFileName);
            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
            objAL.Add(Session["TM_SZ_CASE_ID"].ToString());
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/" + strGenFileName + "'); ", true);
        }

        if (drpSelectBill.SelectedValue == "02")
        {
            string strGenFileName = "";
            PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
            string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C42"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
            strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
            strGenFileName = lfnMergeDiagCodePage(szDefaultPhysicalPath, strGenFileName, 4);
            ArrayList objAL = new ArrayList();
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/" + strGenFileName);
            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
            objAL.Add(Session["TM_SZ_CASE_ID"].ToString());
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/" + strGenFileName + "'); ", true);
        }

        if (drpSelectBill.SelectedValue == "03")
        {

            string strGenFileName = "";
            PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
            string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C43"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
            strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
            strGenFileName = lfnMergeDiagCodePageForC43(szDefaultPhysicalPath, strGenFileName, 2);
            ArrayList objAL = new ArrayList();
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/" + strGenFileName);
            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
            objAL.Add(Session["TM_SZ_CASE_ID"].ToString());
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/" + strGenFileName + "'); ", true);
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

    private string lfnMergeDiagCodePage(string p_szDefaultPath,string p_szGeneratedFileName,int i_NumberOfRecords)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
            string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
            String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DIAG_PAGE");
            
            String szNextDiagPDFFileName ="";
            GeneratePDFFile.GenerateC42PDF objGeneratePDF = new GeneratePDFFile.GenerateC42PDF();

            if (szGenerateNextDiagPage == "CI_0000005" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName  = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }

            if (szGenerateNextDiagPage == "CI_0000004" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }
            if (szNextDiagPDFFileName == "")
            {
                return p_szGeneratedFileName;
            }
            else
            {
                MergePDF.MergePDFFiles(p_szDefaultPath+p_szGeneratedFileName,p_szDefaultPath+szNextDiagPDFFileName,p_szDefaultPath+szNextDiagPDFFileName.Replace(".pdf","_Merge.pdf"));
                return szNextDiagPDFFileName.Replace(".pdf","_Merge.pdf");
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
            return null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    private string lfnMergeDiagCodePageForC43(string p_szDefaultPath, string p_szGeneratedFileName, int i_NumberOfRecords)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
            string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
            String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DIAG_PAGE");

            String szNextDiagPDFFileName = "";
            GeneratePDFFile.GenerateC43PDF objGeneratePDF = new GeneratePDFFile.GenerateC43PDF();

            if (szGenerateNextDiagPage == "CI_0000005" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }

            if (szGenerateNextDiagPage == "CI_0000004" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }
            if (szNextDiagPDFFileName == "")
            {
                return p_szGeneratedFileName;
            }
            else
            {
                MergePDF.MergePDFFiles(p_szDefaultPath + p_szGeneratedFileName, p_szDefaultPath + szNextDiagPDFFileName, p_szDefaultPath + szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf"));
                return szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf");
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
            return null;

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    
}
