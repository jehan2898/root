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

public partial class Bill_Sys_CO_Chiro_Ca: PageBase
{
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ED"] != null)
            {
                Session["CH_EventID"] = Request.QueryString["ED"].ToString();
                txtEventID.Text = ((SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"]).EventID;              

            }
            else
            {
            txtEventID.Text =  ((SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"]).EventID;
        }
            LBL_DATE.Text = DateTime.Now.ToString("MM/dd/yyyy");
           
            
            LoadData();
            LoadPatientData();
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
            CheckRadioList();
            _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "MST_CHIRO_CA.xml";
            _saveOperation.SaveMethod();
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
       
        Bill_Sys_CheckoutBO objChairo = new Bill_Sys_CheckoutBO();
        if (objChairo.CheckImagePath(Convert.ToInt16(txtEventID.Text), "SP_UPDATE_MST_CHIRO_CA"))
        {
            String ch_EventID = txtEventID.Text;
            FillPDFValue(ch_EventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);

        }
        else
        {
            #region "prashant for signpad
            // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_Co_DoctorSign_ChiroCaDatail.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
         //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_Co_PatientSign_ChiroCaDatail.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
            // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_CO_Chiro_Ca_Signature.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
            #endregion
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_CO_Chiro_Ca_Signature.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

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
            EditOperation _editOperation = new EditOperation();
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "MST_CHIRO_CA.xml";
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
        RetriveRadioList();
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void CheckRadioList()
    {
        if (rdl_KNEE.SelectedValue.ToString().Equals("")) 
        {
            txt_KNEE.Text = "-1";
        }
        else 
        {
            txt_KNEE.Text=rdl_KNEE.SelectedValue;
        }
        if (rdl_LOW_BACK.SelectedValue.ToString().Equals(""))
        {
            txt_LOW_BACK.Text = "-1";
        }
        else 
        {
            txt_LOW_BACK.Text = rdl_LOW_BACK.SelectedValue;
        }
        if (rdl_UPPER_BACK.SelectedValue.ToString().Equals(""))
        {
            txt_UPPER_BACK.Text = "-1";
        }
        else 
        {
            txt_UPPER_BACK.Text = rdl_UPPER_BACK.SelectedValue;
        }
        if (rdl_NECK.SelectedValue.ToString().Equals(""))
        {
            txt_NECK.Text = "-1";
        }
        else
        {
            txt_NECK.Text = rdl_NECK.SelectedValue;
        }
        if (rdl_SHOULER.SelectedValue.ToString().Equals(""))
        {
            txt_SHOULER.Text = "-1";
        }
        else 
        {
            txt_SHOULER.Text = rdl_SHOULER.SelectedValue;
        }
        
    }

    public void RetriveRadioList()
    {
        if (!txt_KNEE.Text.ToString().Equals("-1"))
        {
            rdl_KNEE.SelectedValue = txt_KNEE.Text;

        }
        if (!txt_LOW_BACK.Text.ToString().Equals("-1"))
        {
            rdl_LOW_BACK.SelectedValue = txt_LOW_BACK.Text;
        }
        if (!txt_UPPER_BACK.Text.ToString().Equals(""))
        {
            rdl_UPPER_BACK.SelectedValue = txt_UPPER_BACK.Text;
        }
        if (!txt_NECK.Text.ToString().Equals(""))
        {
            rdl_NECK.SelectedValue = txt_NECK.Text;
        }
        if (!txt_SHOULER.Text.ToString().Equals(""))
        {
            rdl_SHOULER.SelectedValue = txt_SHOULER.Text;
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

        DataSet dsObj = _obj.PatientName(txtEventID.Text);
        //Session["CH_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString();
       // Session["CH_CaseID"] = "6";

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
            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
            objAL.Add(sz_CompanyID);
            objAL.Add(EventID);
            objCheckoutBO.save_CHIRO_DocMang(objAL);
            // Open file
            String szOpenFilePath = "";
            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
            Response.Redirect(szOpenFilePath, false);
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
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_PatientInformetion.xml";
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

}
