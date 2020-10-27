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


public partial class Bill_Sys_MRI_QUESTIONAIRE : PageBase
{
    private SaveOperation _saveOperation;

    string sz_CompanyID = "";
    string sz_ComapanyName = "";

    string sz_CompanyName = "";
    string sz_EventID = "";
    string CaseBarcodePath = "";
    Bill_Sys_NF3_Template objNF3Template;

    protected void Page_Load(object sender, EventArgs e)
    {
        TXT_CURRENT_DATE.Text = DateTime.Now.Month.ToString() + " / " + DateTime.Now.Day.ToString() + " / " + DateTime.Now.Year.ToString();
        if (!Page.IsPostBack)
        {
            //TXT_EVENT_ID.Text = "1";
            
            TXT_EVENT_ID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;

            LoadData();
            LoadPatientData();
            
        }
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        {
            CheckRadio();


            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "MRI_Questionary.xml";
            _saveOperation.SaveMethod();


            Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
            if (_objCheckoutBO.ChekMRIQuestionaire(Convert.ToInt32(TXT_EVENT_ID.Text)))
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

                spf.sz_XMLPath = "MRI_QUESTIONAIRE_XML_Path";
                spf.sz_PDFPath = "MRI_QUESTIONAIRE_PDF_Path";

                spf.sz_Session_Id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                spf.SZ_USER_NAME = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                spf.SZ_SPECIALITY_NAME = "MRI";
                obj = FillPDFValue(spf);
                //Response.Redirect(obj[1].ToString(), false);
                //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + obj[1] + "'); ", true);
                Response.Write("<script>window.open('" + obj[1] + "','pdf','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=400')</script>");

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
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_MRI_Signature.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=400'); ", true);
            }




            //ClearControl();
            //BindGrid();
            lblMsg.Visible = true;
            //lblMsg.Text = "Case Status Saved Successfully ...!";
            lblMsg.Text = "MRI-QUESTIONARY report is saved successfully....";
            //Response.Write("<script>window.opener.location.replace('Bill_Sys_CaseMaster.aspx')</script>");
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
            _editoperation.Primary_Value = TXT_EVENT_ID.Text;
            _editoperation.WebPage = this.Page;
            _editoperation.Xml_File = "MRI_Questionary.xml";
            _editoperation.LoadData();

            if (TXT_QUES1.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES1.SelectedValue = TXT_QUES1.Text;
            }

            if (TXT_QUES2.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES2.SelectedValue = TXT_QUES2.Text;
            }

            if (TXT_QUES3.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES3.SelectedValue = TXT_QUES3.Text;
            }

            if (TXT_QUES4.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES4.SelectedValue = TXT_QUES4.Text;
            }

            if (TXT_QUES5.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES5.SelectedValue = TXT_QUES5.Text;
            }

            if (TXT_QUES6.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES6.SelectedValue = TXT_QUES6.Text;
            }

            if (TXT_QUES7.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES7.SelectedValue = TXT_QUES7.Text;
            }

            if (TXT_QUES8.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES8.SelectedValue = TXT_QUES8.Text;
            }

            if (TXT_QUES9.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES9.SelectedValue = TXT_QUES9.Text;
            }

            if (TXT_QUES10.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES10.SelectedValue = TXT_QUES10.Text;
            }

            if (TXT_QUES11.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES11.SelectedValue = TXT_QUES11.Text;
            }

            if (TXT_QUES12.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES12.SelectedValue = TXT_QUES12.Text;
            }

            if (TXT_QUES13.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES13.SelectedValue = TXT_QUES13.Text;
            }

            if (TXT_QUES14.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES14.SelectedValue = TXT_QUES14.Text;
            }

            if (TXT_QUES15.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES15.SelectedValue = TXT_QUES15.Text;
            }

            if (TXT_QUES16.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES16.SelectedValue = TXT_QUES16.Text;
            }

            if (TXT_QUES17.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES17.SelectedValue = TXT_QUES17.Text;
            }

            if (TXT_QUES18.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES18.SelectedValue = TXT_QUES18.Text;
            }

            if (TXT_QUES19.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES19.SelectedValue = TXT_QUES19.Text;
            }

            if (TXT_QUES20.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES20.SelectedValue = TXT_QUES20.Text;
            }

            if (TXT_QUES21.Text.Equals("-1"))
            { }
            else
            {
                RDO_QUES21.SelectedValue = TXT_QUES21.Text;
            }

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
            _editoperation.Primary_Value = TXT_EVENT_ID.Text;
            _editoperation.WebPage = this.Page;
            _editoperation.Xml_File = "MRI_PATIENT_INFORMATION.xml";
            _editoperation.LoadData();
            lblCompName.Text = TXT_CMP_NAME.Text;

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

            strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, TXT_EVENT_ID.Text, p_objSepcialityPDF.sz_CompanyName, sz_CaseID);

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
            objAL.Add(TXT_EVENT_ID.Text);


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
            CheckRadio();
            SaveOperation _objsave = new SaveOperation();
            _objsave.WebPage = this.Page;
            _objsave.Xml_File = "MRI_Questionary.xml";
            _objsave.SaveMethod();

            #region" "Barcode functionality
            DataSet dset = new DataSet();
            dset = _objcheckOut.GetNodeID(sz_CompanyID, sz_EventID);
            string sz_NodeId = dset.Tables[0].Rows[0][1].ToString();
            string sz_CaseId = dset.Tables[0].Rows[0][0].ToString();
            string barcodeValue = sz_CompanyID + "@" + sz_CaseId + "@" + sz_NodeId;
            String sz_BarcodeImagePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Signs/";
            //Added By-Sunil
            string sz_BarcodeImagePathlogical = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Signs/";
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
           // _objcheckOut.MRI_Questionary_SaveSignPath(sz_EventID, CaseBarcodePath, "");
            //Added By-Sunil
            _objcheckOut.MRI_Questionary_SaveSignPath(sz_EventID, CaseBarcodePathlogical, "");
            //end
            PrintAOBFillPDFValue(sz_EventID, sz_CompanyID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
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
            string xmlpath = ConfigurationManager.AppSettings["MRI_QUESTIONAIRE_XML_Path"].ToString();
            string pdfpath = ConfigurationManager.AppSettings["MRI_QUESTIONAIRE_PDF_Path"].ToString();
            string CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            String szDefaultPath = CompanyName + "/" + sz_CaseID + "/Packet Document/";
            String szDestinationDir = CompanyName + "/" + sz_CaseID + "/MRI/";

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
    public void CheckRadio()
    {
        if (RDO_QUES1.SelectedValue.ToString().Equals(""))
        { TXT_QUES1.Text = "-1"; }
        else
        { TXT_QUES1.Text = RDO_QUES1.SelectedValue; }

        if (RDO_QUES2.SelectedValue.ToString().Equals(""))
        { TXT_QUES2.Text = "-1"; }
        else
        { TXT_QUES2.Text = RDO_QUES2.SelectedValue; }

        if (RDO_QUES3.SelectedValue.ToString().Equals(""))
        { TXT_QUES3.Text = "-1"; }
        else
        { TXT_QUES3.Text = RDO_QUES3.SelectedValue; }

        if (RDO_QUES4.SelectedValue.ToString().Equals(""))
        { TXT_QUES4.Text = "-1"; }
        else
        { TXT_QUES4.Text = RDO_QUES4.SelectedValue; }

        if (RDO_QUES5.SelectedValue.ToString().Equals(""))
        { TXT_QUES5.Text = "-1"; }
        else
        { TXT_QUES5.Text = RDO_QUES5.SelectedValue; }

        if (RDO_QUES6.SelectedValue.ToString().Equals(""))
        { TXT_QUES6.Text = "-1"; }
        else
        { TXT_QUES6.Text = RDO_QUES6.SelectedValue; }

        if (RDO_QUES7.SelectedValue.ToString().Equals(""))
        { TXT_QUES7.Text = "-1"; }
        else
        { TXT_QUES7.Text = RDO_QUES7.SelectedValue; }

        if (RDO_QUES8.SelectedValue.ToString().Equals(""))
        { TXT_QUES8.Text = "-1"; }
        else
        { TXT_QUES8.Text = RDO_QUES8.SelectedValue; }

        if (RDO_QUES9.SelectedValue.ToString().Equals(""))
        { TXT_QUES9.Text = "-1"; }
        else
        { TXT_QUES9.Text = RDO_QUES9.SelectedValue; }

        if (RDO_QUES10.SelectedValue.ToString().Equals(""))
        { TXT_QUES10.Text = "-1"; }
        else
        { TXT_QUES10.Text = RDO_QUES10.SelectedValue; }

        if (RDO_QUES11.SelectedValue.ToString().Equals(""))
        { TXT_QUES11.Text = "-1"; }
        else
        { TXT_QUES11.Text = RDO_QUES11.SelectedValue; }

        if (RDO_QUES12.SelectedValue.ToString().Equals(""))
        { TXT_QUES12.Text = "-1"; }
        else
        { TXT_QUES12.Text = RDO_QUES12.SelectedValue; }

        if (RDO_QUES13.SelectedValue.ToString().Equals(""))
        { TXT_QUES13.Text = "-1"; }
        else
        { TXT_QUES13.Text = RDO_QUES13.SelectedValue; }

        if (RDO_QUES14.SelectedValue.ToString().Equals(""))
        { TXT_QUES14.Text = "-1"; }
        else
        { TXT_QUES14.Text = RDO_QUES14.SelectedValue; }

        if (RDO_QUES15.SelectedValue.ToString().Equals(""))
        { TXT_QUES15.Text = "-1"; }
        else
        { TXT_QUES15.Text = RDO_QUES15.SelectedValue; }

        if (RDO_QUES16.SelectedValue.ToString().Equals(""))
        { TXT_QUES16.Text = "-1"; }
        else
        { TXT_QUES16.Text = RDO_QUES16.SelectedValue; }

        if (RDO_QUES17.SelectedValue.ToString().Equals(""))
        { TXT_QUES17.Text = "-1"; }
        else
        { TXT_QUES17.Text = RDO_QUES17.SelectedValue; }

        if (RDO_QUES18.SelectedValue.ToString().Equals(""))
        { TXT_QUES18.Text = "-1"; }
        else
        { TXT_QUES18.Text = RDO_QUES18.SelectedValue; }

        if (RDO_QUES19.SelectedValue.ToString().Equals(""))
        { TXT_QUES19.Text = "-1"; }
        else
        { TXT_QUES19.Text = RDO_QUES19.SelectedValue; }

        if (RDO_QUES20.SelectedValue.ToString().Equals(""))
        { TXT_QUES20.Text = "-1"; }
        else
        { TXT_QUES20.Text = RDO_QUES20.SelectedValue; }

        if (RDO_QUES21.SelectedValue.ToString().Equals(""))
        { TXT_QUES21.Text = "-1"; }
        else
        { TXT_QUES21.Text = RDO_QUES21.SelectedValue; }

    }
   
}
