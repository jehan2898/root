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
using PDFValueReplacement;
using Vintasoft.Barcode;
using Vintasoft.Pdf;
using System.Drawing;
using System.Drawing.Drawing2D;
using SIGPLUSLib;
using System.IO;


public partial class Bill_Sys_Test_Facility_Intake : PageBase
{
    string CaseBarcodePath = "";
    string sz_CompanyID = "";
    string sz_EventID = "";
    string DoctorImagePath = "";
    
    string sz_Patient_path = "";
    string sz_Attorney_Path = "";
    string sz_ComapanyName = "";

    Bill_Sys_NF3_Template objNF3Template;
    private SaveOperation _saveOperation;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            txtId.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID; 
            LoadData();
            LoadInfo();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (rdoPregnant.SelectedValue.ToString().Equals(""))
        {
            txtPregnant.Text = "-1";
        }
        else
        {
            txtPregnant.Text = rdoPregnant.SelectedValue;

        }
        
        if (rdoIvContrast.SelectedValue.ToString().Equals(""))
        {
            txtIvContrast.Text = "-1";
        }
        else
        {
            txtIvContrast.Text = rdoIvContrast.SelectedValue;

        }

   

       
        _saveOperation = new SaveOperation();
        _saveOperation.WebPage = this.Page;
        _saveOperation.Xml_File = "Test_Facility_Intake.xml";
        _saveOperation.SaveMethod();
        Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
        if (_objCheckoutBO.ChekIntakeMriPath(Convert.ToInt32(txtId.Text)))
        {
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
            {
                sz_CompanyID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                Bill_Sys_NF3_Template obj1 = new Bill_Sys_NF3_Template();
                sz_ComapanyName = obj1.GetCompanyName(sz_CompanyID);
            }
            else
            {
                sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                sz_ComapanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            }

            SpecialityPDFFill spf = new SpecialityPDFFill();
            ArrayList obj = new ArrayList();
            spf.sz_EventID = "";
            spf.sz_CompanyID = sz_CompanyID;
            spf.sz_CompanyName = sz_ComapanyName;

            spf.sz_XMLPath = "MRI_INTAKE_XML_Path";
            spf.sz_PDFPath = "MRI_INTAKE_PDF_Path";

            spf.sz_Session_Id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            spf.SZ_SPECIALITY_NAME = "MRI";
            obj = FillPDFValue(spf);
            //Response.Redirect(obj[1].ToString(), false);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + obj[1] + "'); ", true);
            ////Response.Write("<script>window.open('" + obj[1] + "')</script>");
            //Response.Redirect(obj[1].ToString(), false);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + obj[1] + "'); ", true);
            //Response.Write("<script>window.open('" + obj[1] + "')</script>");

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
        else
        {
            // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_AC_Accu_Initial_Doctor_sign.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('MRISignature.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=yes,status=no,resizable=yes,width=1400,height=750'); ", true);
        }

        //Response.Redirect("Bill_Sys_Test_Facility_Intake_Signature.aspx");

    }



    public void LoadData()
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtId.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Test_Facility_Intake.xml";
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
       
        if (txtPregnant.Text.ToString().Equals("-1"))
        {


        }
        else
        {
            rdoPregnant.SelectedValue = txtPregnant.Text;

        }

        if (txtIvContrast.Text.ToString().Equals("-1"))
        {

        }
        else
        {

            rdoIvContrast.SelectedValue = txtIvContrast.Text;

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    public void LoadInfo()
    {


        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtId.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Test_Facility_Cmp_And_Patient_Information.xml";
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
        
        lblHeading.Text = txtCmpName.Text;
        lblAdd.Text = txtAdd.Text;
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
            _editOperation.Primary_Value = txtId.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Test_Facility_Intake.xml";
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
    public ArrayList FillPDFValue(SpecialityPDFFill p_objSepcialityPDF)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string sz_CompanyID = "";
        string sz_ComapanyName = "";

        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
        {
            sz_CompanyID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
             Bill_Sys_NF3_Template obj = new Bill_Sys_NF3_Template();
             sz_ComapanyName=obj.GetCompanyName(sz_CompanyID);
        }
        else
        {
            sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            sz_ComapanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
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

            strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, txtId.Text, p_objSepcialityPDF.sz_CompanyName, sz_CaseID);

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
            objAL.Add(txtId.Text);


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
        
        return null;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

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
            _objsave.Xml_File = "Test_Facility_Intake.xml";
            _objsave.SaveMethod();

            #region" "Barcode functionality
            DataSet dset = new DataSet();
            dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);
            string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
            string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
            string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
            String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Signs/";
            string sz_BarcodeImagePathLogical = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Signs/";
            sz_BarcodeImagePathLogical = sz_BarcodeImagePathLogical + "BarcodeImg.jpg";
            if (!Directory.Exists(sz_BarcodeImagePath))
            {
                Directory.CreateDirectory(sz_BarcodeImagePath);
            }
            
            SpecialityPDFBO pdfbo = new SpecialityPDFBO();
            CaseBarcodePath = pdfbo.GetBarCodePath(sz_CompanyID, sz_CaseId, sz_NodeId, sz_BarcodeImagePath);
            
            #endregion

            //_objcheckOut.LienPatientPath(sz_EventID, "", CaseBarcodePath, "", "");
            //_objcheckOut.TestIntakeMri(sz_EventID, CaseBarcodePath, "", "", "", "");
            _objcheckOut.TestIntakeMri(sz_EventID, sz_BarcodeImagePathLogical, "", "", "", "");
            PrintPatientIntakeFillPDFValue(sz_EventID, sz_CompanyID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
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

    public string PrintPatientIntakeFillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
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
            string xmlpath = ConfigurationManager.AppSettings["MRI_INTAKE_XML_Path"].ToString();
            string pdfpath = ConfigurationManager.AppSettings["MRI_INTAKE_PDF_Path"].ToString();
            string CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            String szDefaultPath = CompanyName + "/" + sz_CaseID + "/Packet Document/";
            String szDestinationDir = CompanyName + "/" + sz_CaseID + "/Patient_Intake/";

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
            szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL").ToString() + szDestinationDir + strGenFileName;
            //Response.Redirect(szOpenFilePath, false);
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
}
