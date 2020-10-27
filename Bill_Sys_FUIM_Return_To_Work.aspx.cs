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
using PDFValueReplacement;
using Vintasoft.Barcode;
using Vintasoft.Pdf;

public partial class Bill_Sys_FUIM_Return_To_Work : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        Bill_Sys_CheckoutBO _obj = new Bill_Sys_CheckoutBO();
        DataSet dsObj = _obj.PatientName(Session["IM_FW_EVENT_ID"].ToString());
        Session["IM_FOLLOWUP_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString(); 

        if (Request.QueryString["EID"] != null)
        {
            Session["IM_FW_EVENT_ID"] = Request.QueryString["EID"].ToString();
        }
        TXT_EVENT_ID.Text = Session["IM_FW_EVENT_ID"].ToString();
        if (!IsPostBack)
        {
            LoadPatientData();
            LoadData();
        }
        TXT_DOS.Text = DateTime.Today.Date.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_FUIM_Return_To_Work.aspx");
        }
        #endregion
    }

    protected void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {

            _editOperation.Primary_Value = Session["IM_FW_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Bill_Sys_IM_Return_To_Work.xml";
            _editOperation.LoadData();

            if (TXT_PATIENT_WORKING_YES.Text != "-1")
            {
                if (TXT_PATIENT_WORKING_YES.Text == "0")
                {
                    RDO_PATIENT_WORKING_YES.SelectedIndex = 0;
                }
                else if (TXT_PATIENT_WORKING_YES.Text == "1")
                {
                    RDO_PATIENT_WORKING_YES.SelectedIndex = 1;
                }

            }
            if (TXT_PATIENT_WORK_RESTRICT_YES.Text != "-1")
            {
                if (TXT_PATIENT_WORK_RESTRICT_YES.Text == "0")
                {
                    RDO_PATIENT_WORK_RESTRICT_YES.SelectedIndex = 0;
                }
                else if (TXT_PATIENT_WORK_RESTRICT_YES.Text == "1")
                {
                    RDO_PATIENT_WORK_RESTRICT_YES.SelectedIndex = 1;
                }

            }
            if (TXT_DISCUSS_LIMIT_PATIENT.Text != "-1")
            {
                if (TXT_DISCUSS_LIMIT_PATIENT.Text == "0")
                {
                    RDO_DISCUSS_LIMIT_PATIENT.SelectedIndex = 0;
                }
                else if (TXT_DISCUSS_LIMIT_PATIENT.Text == "1")
                {
                    RDO_DISCUSS_LIMIT_PATIENT.SelectedIndex = 1;
                }

            }
            if (TXT_PATIENT_BENEFIT_REHABILITATION.Text != "-1")
            {
                if (TXT_PATIENT_BENEFIT_REHABILITATION.Text == "0")
                {
                    RDO_PATIENT_BENEFIT_REHABILITATION.SelectedIndex = 0;
                }
                else if (TXT_PATIENT_BENEFIT_REHABILITATION.Text == "1")
                {
                    RDO_PATIENT_BENEFIT_REHABILITATION.SelectedIndex = 1;
                }

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

    public void LoadPatientData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = TXT_EVENT_ID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_FalloUP_PatientInformetion.xml";
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
    protected void BTN_PREVIOUS_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Bill_Sys_FUIM_Doctors_opinion.aspx");
    }
    protected void BTN_SAVE_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        TXT_PATIENT_WORKING_YES.Text=RDO_PATIENT_WORKING_YES.SelectedValue.ToString();
        TXT_PATIENT_WORK_RESTRICT_YES.Text=RDO_PATIENT_WORK_RESTRICT_YES.SelectedValue.ToString();
        TXT_DISCUSS_LIMIT_PATIENT.Text=RDO_DISCUSS_LIMIT_PATIENT.SelectedValue.ToString();
        TXT_PATIENT_BENEFIT_REHABILITATION.Text=RDO_PATIENT_BENEFIT_REHABILITATION.SelectedValue.ToString();



        _saveOperation = new SaveOperation();
        // Create object of SaveOperation. With the help of this object you save information into table.
        try
        {
            if (Page.IsValid)  // Check for Validation.
            {
                _saveOperation.WebPage = this.Page;  // Pass Current web form to SaveOperation.
                _saveOperation.Xml_File = "Bill_Sys_IM_Return_To_Work.xml";  // Pass xml file to SaveOperation
                _saveOperation.SaveMethod(); // Call  save method of SaveOperation. Will save all information from web form.
            }
            Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
            if (obj.ChekIMFollowupSignPath(Convert.ToInt32(TXT_EVENT_ID.Text)))
             {
                 ArrayList objal = new ArrayList();
                 SpecialityPDFFill spf = new SpecialityPDFFill();
                 spf.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                 spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                 spf.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                 spf.SZ_SPECIALITY_NAME = "IM";
                 spf.SZ_SPECIALITY_CODE = "NFMIM";
                 spf.sz_XMLPath = "IM_FOLLOWUP_XML_Path";
                 spf.sz_PDFPath = "IM_FOLLOWUP_PDF_Path";

                 spf.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();
                 spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                 
                 objal = spf.FillPDFValue(spf);
                 Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + objal[1] + "'); ", true);


                 spf.sz_Session_Id = Session["IM_FOLLOWUP_CaseID"].ToString();  
                 spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                 spf.sz_EventID = Session["IM_FW_EVENT_ID"].ToString();
                 spf.SZ_PT_FILE_NAME = objal[0].ToString();
                 spf.SZ_PT_FILE_PATH = objal[2].ToString();
                 spf.SZ_SPECIALITY_CODE = "IM";
                 spf.SZ_SPECIALITY_NAME = "NFMIM";
                 spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                 spf.savePDFForminDocMang(spf);

                 //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_IM_Followup_DoctorSign.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
                 
             }
             else
                 {
                     //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_IM_Followup_DoctorSign.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);  comment for  signature pad
                     Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_IM_FUIM_Signature.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
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

    //public string FillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
    //{
    //    string sz_CaseID = Session["IM_FOLLOWUP_CaseID"].ToString();
    //    PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
    //    string pdffilepath;
    //    string strGenFileName = "";
    //    try
    //    {
    //        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
    //        string xmlpath = ConfigurationManager.AppSettings["IM_FOLLOWUP_XML_Path"].ToString();
    //        string pdfpath = ConfigurationManager.AppSettings["IM_FOLLOWUP_PDF_Path"].ToString();

    //        String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
    //        String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/No Fault File/Medicals/IM/";

    //        strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, EventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, sz_CaseID);

    //        if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName))
    //        {
    //            if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
    //            {
    //                Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
    //            }
    //            File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName, objNF3Template.getPhysicalPath() + szDestinationDir + strGenFileName);
    //        }

    //        Bill_Sys_CheckoutBO objCheckoutBO = new Bill_Sys_CheckoutBO();
    //        ArrayList objAL = new ArrayList();
    //        objAL.Add(sz_CaseID);
    //        objAL.Add(strGenFileName);
    //        objAL.Add(szDestinationDir);
    //        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
    //        objAL.Add(sz_CompanyID);
    //        objAL.Add(EventID);
    //        objCheckoutBO.save_IM_DocMang(objAL);
    //        // Open file
    //        String szOpenFilePath = "";
    //        szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
    //        //Response.Redirect(szOpenFilePath, false);
    //        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
    //    }
    //   catch (Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    }
    //    return strGenFileName;
    //}
}
