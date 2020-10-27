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
public partial class Bill_Sys_AC_Accu_Initial_3 : PageBase
{
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        Bill_Sys_CheckoutBO _obj = new Bill_Sys_CheckoutBO();
        DataSet dsObj = _obj.PatientName(Session["AC_INITIAL_EVENT_ID"].ToString());
        Session["AC_Initial_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString(); 
        if (Request.QueryString["EID"] != null)
        {
            Session["AC_INITIAL_EVENT_ID"] = Request.QueryString["EID"].ToString();
        }
        txtEventID.Text =  Session["AC_INITIAL_EVENT_ID"].ToString();

        if (!IsPostBack)
        {
           LoadPatientInformation();
            LoadData();
            
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_AC_Accu_Initial_3.aspx");
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
        _saveOperation = new SaveOperation();
        try
        {
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "AccuInitial_3.xml";
            _saveOperation.SaveMethod();
             Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
             if (obj.ChekAccInitialDoctorPath(Convert.ToInt32(txtEventID.Text)))
             {


                 ArrayList objal = new ArrayList();
                 SpecialityPDFFill spf = new SpecialityPDFFill();
                 spf.sz_EventID = Session["AC_INITIAL_EVENT_ID"].ToString();
                 spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                 spf.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                 spf.SZ_SPECIALITY_NAME = "AC";
                 spf.SZ_SPECIALITY_CODE = "NFMAC";
                 spf.sz_XMLPath = "Accu_Initial_XML_Path";
                 spf.sz_PDFPath = "Accu_Initial_PDF_Path";

                 spf.sz_Session_Id = Session["AC_Initial_CaseID"].ToString();
                 spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                 //objal = spf.FillPDFValue(spf.sz_EventID, spf.sz_CompanyID, spf.sz_CompanyName, spf.sz_XMLPath, spf.sz_PDFPath, spf.sz_Session_Id, spf.SZ_USER_NAME,spf.SZ_SPECIALITY_NAME);
                 objal = spf.FillPDFValue(spf);
                 Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + objal[1] + "'); ", true);


                 spf.sz_Session_Id = Session["AC_Initial_CaseID"].ToString();  
                 spf.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                 spf.sz_EventID = Session["AC_INITIAL_EVENT_ID"].ToString();
                 spf.SZ_PT_FILE_NAME = objal[0].ToString();
                 spf.SZ_PT_FILE_PATH = objal[2].ToString();
                 spf.SZ_SPECIALITY_CODE = "NFMAC";
                 spf.SZ_SPECIALITY_NAME ="AC";
                 spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                 spf.savePDFForminDocMang(spf);
       
             }
             else
             {
                // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_AC_Accu_Initial_Doctor_sign.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
                 Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_AC_Initial_Signature.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
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
            _editOperation.Primary_Value = txtEventID.Text; ;// Session["AC_INITIAL_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "AccuInitial_Patient_Details.xml";
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
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Response.Redirect("Bill_Sys_AC_Accu_Initial_2.aspx", false);
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
            _editoperation.Primary_Value = txtEventID.Text;
            _editoperation.WebPage = this.Page;
            _editoperation.Xml_File = "AccuInitial_3.xml";
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
}
