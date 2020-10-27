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
    string DoctorImagePathlogical = "";
    
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
        DataSet dsObj = _objcheckOut.PatientName(Session["AC_EventID"].ToString());
        Session["AC_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString();
        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        sz_EventID = Session["AC_EventID"].ToString();     
        int flag = 0;
        try
        {
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["AC_CaseID"].ToString() + "/Signs/";
            //Added By-Sunil
            string SaveSignPathlogical = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["AC_CaseID"].ToString() + "/Signs/";
            //end
            if (!Directory.Exists(szDefaultPath))
            {
                Directory.CreateDirectory(szDefaultPath);
            }
            szDefaultPath = szDefaultPath + sz_EventID;
            //Added By-Sunil
            SaveSignPathlogical = SaveSignPathlogical + sz_EventID;
            //end
            DoctorImagePath = szDefaultPath + "_DoctorSign.jpg";
            DoctorImagePathlogical = SaveSignPathlogical + "_DoctorSign.jpg";
            signobj.SignSave(Request.Form["hidden"], DoctorImagePath);

            #region" "Barcode functionality
                DataSet dset = new DataSet();
                dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);
                string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
                string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
                string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
                String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["AC_CaseID"].ToString() + "/Signs/";
                //Added By-Sunil
                string sz_BarcodeImagePathlogical = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["AC_CaseID"].ToString() + "/Signs/";
                //end
                SpecialityPDFBO pdfbo = new SpecialityPDFBO();
                CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);
                //Added By-Sunil
                string CaseBarcodePathlogical = sz_BarcodeImagePathlogical + "BarcodeImg.jpg";
                //end
            #endregion

                //_objcheckOut.UpdateIMSignPathACCU_REEVAL(sz_EventID, DoctorImagePath, CaseBarcodePath);
                _objcheckOut.UpdateIMSignPathACCU_REEVAL(sz_EventID, DoctorImagePathlogical, CaseBarcodePathlogical);
               SpecialityPDFFill spf = new SpecialityPDFFill();
            ArrayList obj = new ArrayList();
            spf.sz_EventID = Session["AC_EventID"].ToString();
            spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            spf.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;

            spf.sz_XMLPath = "AC_REEVAL_XML_Path";
            spf.sz_PDFPath = "AC_REEVAL_PDF_Path";

            spf.sz_Session_Id = Session["AC_CaseID"].ToString();
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            spf.SZ_SPECIALITY_NAME = "AC";
            obj = spf.FillPDFValue(spf);
            Response.Redirect(obj[1].ToString(),false);
           //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + obj[1] + "'); ", true);
            //Response.Write("<script>window.open('" + obj[1] + "')</script>");

            spf.sz_Session_Id = Session["AC_CaseID"].ToString(); ;
            spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            spf.sz_EventID = Session["AC_EventID"].ToString();
            spf.SZ_PT_FILE_NAME = obj[0].ToString();
            spf.SZ_PT_FILE_PATH = obj[2].ToString();
            spf.SZ_SPECIALITY_CODE = "AC";
            spf.SZ_SPECIALITY_NAME = "NFMAC";
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            spf.savePDFForminDocMang(spf);
            
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
