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
using System.IO;

public partial class Bill_Sys_AccuReEval : PageBase
{
    private SaveOperation _saveOperation;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if(Request.QueryString["EID"]!=null)
                Session["AC_EventID"] = Request.QueryString["EID"].ToString();

            if (Session["AC_EventID"] == null)
            {
                Session["AC_EventID"] = Session["SPECIALITY_PDF_OBJECT"].ToString();
            }
            txt_PATIENT_ID.Text = ((SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"]).EventID;
            Session["AC_EventID"] = ((SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"]).EventID;
            Bill_Sys_CheckoutBO _objcheckOut = new Bill_Sys_CheckoutBO();
            DataSet dsObj = _objcheckOut.PatientName(txt_PATIENT_ID.Text);
            Session["AC_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString();           
            LoadData();
            LoadPatientData();       

            if (!txtDoa.Text.ToString().Equals("")) 
            {
                txtDoa.Text  = Convert.ToDateTime(txtDoa.Text).ToString("MM/dd/yyyy");
            }
            if (!txtDoe.Text.ToString().Equals(""))
            {
                txtDoe.Text =  DateTime.Now.ToString("MM/dd/yyy");
            }

            Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
            if (obj.CheckImgPathAC(Convert.ToInt32(txt_PATIENT_ID.Text)))
            {
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
            }

        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_AccuReEval.aspx");
        }
        #endregion

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {      
            _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "MST_ACCU_RE_EVAL.xml";
            _saveOperation.SaveMethod();
            LoadData();

            Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
            if (obj.CheckImgPathAC(Convert.ToInt32(txt_PATIENT_ID.Text)))
            {
                ArrayList objal = new ArrayList();
                SpecialityPDFFill spf = new SpecialityPDFFill();
                spf.sz_EventID = Session["AC_EventID"].ToString();
                spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                spf.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                spf.SZ_SPECIALITY_NAME = "AC";
                spf.SZ_SPECIALITY_CODE = "NFMAC";
                spf.sz_XMLPath = "AC_REEVAL_XML_Path";
                spf.sz_PDFPath = "AC_REEVAL_PDF_Path";

                spf.sz_Session_Id = Session["AC_CaseID"].ToString();
                spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                //objal = spf.FillPDFValue(spf.sz_EventID, spf.sz_CompanyID, spf.sz_CompanyName, spf.sz_XMLPath, spf.sz_PDFPath, spf.sz_Session_Id, spf.SZ_USER_NAME,spf.SZ_SPECIALITY_NAME);
                objal = spf.FillPDFValue(spf);
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + objal[1] + "'); ", true);


                spf.sz_Session_Id = Session["AC_CaseID"].ToString(); ;
                spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                spf.sz_EventID = Session["AC_EventID"].ToString();
                spf.SZ_PT_FILE_NAME = objal[0].ToString();
                spf.SZ_PT_FILE_PATH = objal[2].ToString();
                spf.SZ_SPECIALITY_CODE = "NFMAC";
                spf.SZ_SPECIALITY_NAME = "AC";
                spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                spf.savePDFForminDocMang(spf);
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Update";
              //  Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_IM_DoctorSign_AccuReEval.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_Ac_AccuReval_Signature.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
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
    public void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           EditOperation _editoperation = new EditOperation();
           _editoperation.Primary_Value = txt_PATIENT_ID.Text;
           _editoperation.WebPage = this.Page;
           _editoperation.Xml_File = "MST_ACCU_RE_EVAL.xml";
           _editoperation.LoadData();            

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
        try
        {
            EditOperation _editoperation = new EditOperation();
            _editoperation.Primary_Value = txt_PATIENT_ID.Text;
            _editoperation.WebPage = this.Page;
            _editoperation.Xml_File = "AccuReVal.xml";
            _editoperation.LoadData();
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
        Bill_Sys_CheckoutBO _obj = new Bill_Sys_CheckoutBO();
     
        DataSet dsObj = _obj.PatientName(txt_PATIENT_ID.Text);
        Session["AC_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString();

        string sz_CaseID = Session["AC_CaseID"].ToString();
        PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
        string pdffilepath;
        string strGenFileName = "";
        try
        {
            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            string xmlpath = ConfigurationManager.AppSettings["AC_REEVAL_XML_Path"].ToString();
            string pdfpath = ConfigurationManager.AppSettings["AC_REEVAL_PDF_Path"].ToString();

            String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
            String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/No Fault File/Medicals/AC/";

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
            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
            objAL.Add(sz_CompanyID);
            objAL.Add(EventID);
            objCheckoutBO.save_AC_REEVAL_DM(objAL);
            // Open file
            String szOpenFilePath = "";
            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
            //Response.Redirect(szOpenFilePath, false);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);

            //Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
            //if (obj.CheckImgPath(Convert.ToInt32(Session["IMEventID"].ToString())))
            //{
            //    string sz_eventID = (string)Session["IMEventID"].ToString();
            //    FillPDFValue(sz_eventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
            //}
            //else
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_IM_DoctorSign_AccuReEval.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
            //}
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
