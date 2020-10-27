using AjaxControlToolkit;
using CUTEFORMCOLib;
using DevExpress.Web;
using log4net;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_Bill_Sys_Document_Manager_Popup : Page
{
    private Bill_Sys_Document_Manager _Bill_Sys_Document_Manager = new Bill_Sys_Document_Manager();
    private Bill_Sys_NF3_Template _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
    //protected DevExpress.Web.ASPxCallbackPanel ASPxCallbackPanel1;
    //protected DevExpress.Web.ASPxRoundPanel ASPxRoundPanel1;
    //protected DevExpress.Web.ASPxRoundPanel ASPxRoundPanel2;
    //protected Button btnAdd;
    //protected Button btnCheck;
    //protected Button btndelete;
    //protected Button btnDone;
    //protected Button btnmergeclose;
    //protected HtmlInputButton btnmovedown;
    //protected HtmlInputButton btnmoveup;
    //protected HtmlInputButton btnSelectOrder;
    //protected Button btnUpload;
    //protected HtmlForm form1;
    //protected DevExpress.Web.ASPxGridView grddocumnetmanager;
    //protected HiddenField hdnmerge;
    //protected HtmlHead Head1;
    //protected HtmlInputHidden hidnFile;
    //protected HtmlInputHidden hidnOrderFiles;
    //protected Label lblPatientName;
    //protected LinkButton lbnTest;
    //protected DevExpress.Web.ASPxLoadingPanel LoadingPanel;
    private static ILog log = LogManager.GetLogger("PDFValueReplacement");
    //protected ListBox lstPDF;
    //protected UserControl_ErrorMessageControl MessageControl1;
    //protected ModalPopupExtender ModalPopupExtenderMerge;
    //protected Label Msglbl;
    //protected HtmlGenericControl myiframe;
    //protected PanelContent PanelContent1;
    //protected PanelContent PanelContent2;
    //protected PanelContent PanelContent3;
    //protected Panel pnlMerge;
    //protected FileUpload ReportUpload;
    //protected ScriptManager ScriptManager1;
    //protected TextBox txtcaseid;
    //protected TextBox txtCompanyID;
    //protected TextBox txtmergerfilename;
    //protected Button UploadButton;
    //protected UserControl_ErrorMessageControl usrMessage;

    protected void btndelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Document_Manager manager = new Bill_Sys_Document_Manager();
        try
        {
            string str = "";
            for (int i = 0; i < this.grddocumnetmanager.VisibleRowCount; i++)
            {
                GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grddocumnetmanager.Columns[0];
                CheckBox box = (CheckBox)this.grddocumnetmanager.FindRowCellTemplateControl(i, gridViewDataColumn, "chkall");
                if (box != null)
                {
                    str = this.grddocumnetmanager.GetRowValues(i, new string[] { "ImageId" }).ToString();
                    if (box.Checked)
                    {
                        manager.deletenode(str);
                        string path = this._bill_Sys_NF3_Template.getPhysicalPath() + this.grddocumnetmanager.GetRowValues(i, new string[] { "File_Path" }).ToString() + this.grddocumnetmanager.GetRowValues(i, new string[] { "File_Name" }).ToString();
                        if (File.Exists(path))
                        {
                            string sourceFileName = path.Replace(@"\", "/");
                            File.Move(sourceFileName, sourceFileName + ".deleted");
                        }
                    }
                }
            }
            this.MessageControl1.PutMessage("PDF Delete Successfully ...!");
            this.MessageControl1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.MessageControl1.Show();
            DataSet documentmanagerInfo = new DataSet();
            documentmanagerInfo = manager.GetDocumentmanagerInfo(this.txtcaseid.Text, this.txtCompanyID.Text);
            this.grddocumnetmanager.DataSource = documentmanagerInfo;
            this.grddocumnetmanager.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        string str2 = this.hidnOrderFiles.Value;
        ArrayList list = new ArrayList();
        log.Debug(" INside BtnMergeDoc_OnClick");
        try
        {
            if (this.hidnFile.Value == "")
            {
                log.Debug("File name is emplty");
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mmSelect", "alert('Please Enter the File Name without Extension');", true);
                this.ModalPopupExtenderMerge.Show();
            }
            else
            {
                string[] strArray = str2.Split(new char[] { ',' });
                ArrayList list2 = new ArrayList();
                for (int i = 0; i <= (strArray.Length - 1); i++)
                {
                    log.Debug(" INside CreatePDF ");
                    string path = ApplicationSettings.GetParameterValue("MergePth");
                    log.Debug(" szPath = " + path);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        log.Debug(" Directory Created = " + path);
                    }
                    string str4 = null;
                    str4 = DateTime.Now.ToString("MMddyyHmmssff");
                    str4 = path + str + "_" + this.txtcaseid.Text + "_Blank_" + str4 + ".pdf";
                    int num2 = 0;
                    log.Debug(" Call to merge = ");
                    string str5 = ApplicationSettings.GetParameterValue("BlankMergePth");
                    num2 = this.CreatePDFBlank(strArray[i].ToString(), str5, str4);
                    if (File.Exists(str4))
                    {
                        list2.Add(strArray[i].ToString());
                    }
                    log.Debug(" VAlue of p" + num2);
                }
                for (int j = 0; j < list2.Count; j++)
                {
                    string[] strArray2 = list2[j].ToString().Replace(@"\", "/").Split(new char[] { '/' });
                    string str6 = "";
                    foreach (string str7 in strArray2)
                    {
                        str6 = str7;
                    }
                    Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO rdao = new Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO();
                    rdao._FILE_NAME = str6;
                    rdao.FILE_PATH = list2[j].ToString();
                    list.Add(rdao);
                }
                string str8 = this._bill_Sys_NF3_Template.getPhysicalPath() + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.txtcaseid.Text + "/Packeted Doc/" + this.hidnFile.Value + ".pdf";
                if (!Directory.Exists(this._bill_Sys_NF3_Template.getPhysicalPath() + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.txtcaseid.Text + "/Packeted Doc/"))
                {
                    Directory.CreateDirectory(this._bill_Sys_NF3_Template.getPhysicalPath() + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.txtcaseid.Text + "/Packeted Doc/");
                }
                if (this.CreatePDF(str8, list) == 1)
                {
                    Bill_Sys_Document_Manager manager = new Bill_Sys_Document_Manager();
                    manager.getlogical(this.txtcaseid.Text, this.hidnFile.Value + ".pdf", this.txtCompanyID.Text);
                    this.txtmergerfilename.Text = "";
                    DataSet documentmanagerInfo = new DataSet();
                    documentmanagerInfo = manager.GetDocumentmanagerInfo(this.txtcaseid.Text, this.txtCompanyID.Text);
                    this.grddocumnetmanager.DataSource = documentmanagerInfo;
                    this.grddocumnetmanager.DataBind();
                    for (int k = 0; k < this.grddocumnetmanager.VisibleRowCount; k++)
                    {
                        string str9 = "";
                        if (k == 0)
                        {
                            GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grddocumnetmanager.Columns[0];
                            CheckBox box = (CheckBox)this.grddocumnetmanager.FindRowCellTemplateControl(k, gridViewDataColumn, "chkall");
                            str9 = this.grddocumnetmanager.GetRowValues(k, new string[] { "File_Name" }).ToString();
                            box.Visible = false;
                        }
                        else if (str9 == this.grddocumnetmanager.GetRowValues(k, new string[] { "File_Name" }).ToString())
                        {
                            GridViewDataColumn column2 = (GridViewDataColumn)this.grddocumnetmanager.Columns[0];
                            CheckBox box2 = (CheckBox)this.grddocumnetmanager.FindRowCellTemplateControl(k, column2, "chkall");
                            box2.Visible = false;
                        }
                        else
                        {
                            str9 = this.grddocumnetmanager.GetRowValues(k, new string[] { "File_Name" }).ToString();
                        }
                    }
                    this.usrMessage.PutMessage("Merge Successfully ...!");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                    this.ModalPopupExtenderMerge.Show();
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
            string errorMessage = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + errorMessage);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void BtnMerge_OnClick(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new ArrayList();
        this.ModalPopupExtenderMerge.Show();
        DataTable table = new DataTable();
        table.Columns.Add("File_Path");
        table.Columns.Add("File_Name");
        string str = "";
        this.txtmergerfilename.Text = "";
        try
        {
            for (int i = 0; i < this.grddocumnetmanager.VisibleRowCount; i++)
            {
                GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grddocumnetmanager.Columns[0];
                CheckBox box = (CheckBox)this.grddocumnetmanager.FindRowCellTemplateControl(i, gridViewDataColumn, "chkall");
                if ((box != null) && box.Checked)
                {
                    string path = this._bill_Sys_NF3_Template.getPhysicalPath() + this.grddocumnetmanager.GetRowValues(i, new string[] { "File_Path" }).ToString() + this.grddocumnetmanager.GetRowValues(i, new string[] { "File_Name" }).ToString();
                    if (File.Exists(path))
                    {
                        DataRow row = table.NewRow();
                        row["File_Path"] = path;
                        row["File_Name"] = this.grddocumnetmanager.GetRowValues(i, new string[] { "File_Name" }).ToString();
                        table.Rows.Add(row);
                    }
                    else if (str == "")
                    {
                        str = this.grddocumnetmanager.GetRowValues(i, new string[] { "File_Name" }).ToString();
                    }
                    else
                    {
                        str = str + "," + this.grddocumnetmanager.GetRowValues(i, new string[] { "File_Name" }).ToString();
                    }
                }
            }
            if (str != "")
            {
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "mm", "<script language='javascript'>alert('" + str + " These Files are  Physically not present ');</script>");
            }
            if (table.Rows.Count > 0)
            {
                DataSet set = new DataSet();
                set.Tables.Add(table.Copy());
                this.lstPDF.DataSource = set;
                this.lstPDF.DataTextField = "File_Name";
                this.lstPDF.DataValueField = "File_Path";
                this.lstPDF.DataBind();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void BtnMergeDoc_OnClick(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList list = new ArrayList();
        log.Debug(" INside BtnMergeDoc_OnClick");
        try
        {
            if (this.txtmergerfilename.Text == "")
            {
                log.Debug("File name is emplty");
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mmSelect", "alert('Please Enter the File Name without Extension');", true);
                this.ModalPopupExtenderMerge.Show();
            }
            else
            {
                for (int i = 0; i < this.lstPDF.Items.Count; i++)
                {
                    Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO rdao = new Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO();
                    rdao._FILE_NAME = this.lstPDF.Items[i].Text;
                    rdao.FILE_PATH = this.lstPDF.Items[i].Value.ToString();
                    list.Add(rdao);
                }
                string str = this._bill_Sys_NF3_Template.getPhysicalPath() + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.txtcaseid.Text + "/Packeted Doc/" + this.txtmergerfilename.Text + ".pdf";
                if (!Directory.Exists(this._bill_Sys_NF3_Template.getPhysicalPath() + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.txtcaseid.Text + "/Packeted Doc/"))
                {
                    Directory.CreateDirectory(this._bill_Sys_NF3_Template.getPhysicalPath() + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.txtcaseid.Text + "/Packeted Doc/");
                }
                if (this.CreatePDF(str, list) == 1)
                {
                    Bill_Sys_Document_Manager manager = new Bill_Sys_Document_Manager();
                    manager.getlogical(this.txtcaseid.Text, this.txtmergerfilename.Text + ".pdf", this.txtCompanyID.Text);
                    this.txtmergerfilename.Text = "";
                    DataSet documentmanagerInfo = new DataSet();
                    documentmanagerInfo = manager.GetDocumentmanagerInfo(this.txtcaseid.Text, this.txtCompanyID.Text);
                    this.grddocumnetmanager.DataSource = documentmanagerInfo;
                    this.grddocumnetmanager.DataBind();
                    for (int j = 0; j < this.grddocumnetmanager.VisibleRowCount; j++)
                    {
                        string str2 = "";
                        if (j == 0)
                        {
                            GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grddocumnetmanager.Columns[0];
                            CheckBox box = (CheckBox)this.grddocumnetmanager.FindRowCellTemplateControl(j, gridViewDataColumn, "chkall");
                            str2 = this.grddocumnetmanager.GetRowValues(j, new string[] { "File_Name" }).ToString();
                            box.Visible = false;
                        }
                        else if (str2 == this.grddocumnetmanager.GetRowValues(j, new string[] { "File_Name" }).ToString())
                        {
                            GridViewDataColumn column2 = (GridViewDataColumn)this.grddocumnetmanager.Columns[0];
                            CheckBox box2 = (CheckBox)this.grddocumnetmanager.FindRowCellTemplateControl(j, column2, "chkall");
                            box2.Visible = false;
                        }
                        else
                        {
                            str2 = this.grddocumnetmanager.GetRowValues(j, new string[] { "File_Name" }).ToString();
                        }
                    }
                    this.usrMessage.PutMessage("Merge Successfully ...!");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                    this.ModalPopupExtenderMerge.Show();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private int CreatePDF(string p_szPDFFile, ArrayList p_objMergeList11)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        log.Debug(" INside CreatePDF ");
        string path = ApplicationSettings.GetParameterValue("MergePth");
        log.Debug(" szPath = " + path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            log.Debug(" Directory Created = " + path);
        }
        int num = 0;
        try
        {
            string str2 = null;
            str2 = DateTime.Now.ToString("MMddyyHmmssff");
            str2 = path + "Merge_" + str2 + ".pdf";
            if (p_objMergeList11.Count >= 2)
            {
                for (int i = 0; i <= (p_objMergeList11.Count - 2); i++)
                {
                    if (i == 0)
                    {
                        Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO rdao = new Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO();
                        Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO rdao2 = new Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO();
                        rdao = (Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO)p_objMergeList11[i];
                        rdao2 = (Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO)p_objMergeList11[i + 1];
                        log.Debug(" Call to merge = ");
                        log.Debug(" VAlue of j" + this.CreatePDFNormal(rdao.FILE_PATH, rdao2.FILE_PATH, str2));
                    }
                    else
                    {
                        string str3 = null;
                        str3 = DateTime.Now.ToString("MMddyyHmmssff");
                        str3 = path + str3 + ".pdf";
                        Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO rdao3 = new Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO();
                        rdao3 = (Bill_Sys_Document_Manager.Bill_Sys_Document_ManagerDAO)p_objMergeList11[i + 1];
                        log.Debug(" Call to merge = ");
                        log.Debug(" VAlue of j" + this.CreatePDFNormal(str2, rdao3.FILE_PATH, str3));
                        str2 = str3;
                    }
                }
                num = 1;
                try
                {
                    log.Debug(" Before copying file");
                    File.Copy(str2, p_szPDFFile);
                    this.usrMessage.PutMessage("copy Successfully ...!");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                    log.Debug(" after copying file");
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    using (Utils utility = new Utils())
                    {
                        utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                    }
                    string errorMessage = "Error Request=" + id + ".Please share with Technical support.";
                    base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + errorMessage);

                }
            }
        }
        catch (Exception ex)
        {
            num = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        if (num == 1)
        {
            return 1;
        }
        return 0;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public int CreatePDFBlank(string p_szPDFFile1, string p_szPDFFile2, string p_szDesFile)
    {
        int iResult = 0;
        try
        {
            CUTEFORMCOLib.CutePDFUtilities objMyForm = (CUTEFORMCOLib.CutePDFUtilities)Server.CreateObject("CutePDF.Utilities");

            string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
            objMyForm.initialize(KeyForCutePDF);
            string errormessage = string.Empty;
            iResult = objMyForm.openFile(p_szPDFFile1);
            if (iResult != 0)
            {
                iResult = objMyForm.appendFile(p_szPDFFile2);

            }
            if (iResult != 0)
            {
                iResult = objMyForm.saveFile(p_szDesFile);

            }


            if (iResult == 0)
                return iResult;
            else
                return iResult;



        }
        catch (Exception ex)
        {

        }
        finally { }
        return iResult;

    }

    public int CreatePDFNormal(string p_szPDFFile1, string p_szPDFFile2, string p_szDesFile)
    {
        int iResult = 0;
        try
        {
            CUTEFORMCOLib.CutePDFUtilities objMyForm = (CUTEFORMCOLib.CutePDFUtilities)Server.CreateObject("CutePDF.Utilities");

            string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
            objMyForm.initialize(KeyForCutePDF);
            string errormessage = string.Empty;
            iResult = objMyForm.openFile(p_szPDFFile1);
            if (iResult != 0)
            {
                iResult = objMyForm.appendFile(p_szPDFFile2);

            }
            if (iResult != 0)
            {
                iResult = objMyForm.saveFile(p_szDesFile);

            }


            if (iResult == 0)
                return iResult;
            else
                return iResult;



        }
        catch (Exception ex)
        {

        }
        finally { }
        return iResult;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.txtcaseid.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        this.btnAdd.Attributes.Add("onclick", "return callforMerge();");
        this.btnUpload.Attributes.Add("onclick", "return UploadDoc();");
        if (!base.IsPostBack)
        {
            this.hdnmerge.Value = "";
            this.lblPatientName.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_NAME;
            if (this.hdnmerge.Value != "true")
            {
                DataSet documentmanagerInfo = new DataSet();
                documentmanagerInfo = this._Bill_Sys_Document_Manager.GetDocumentmanagerInfo(this.txtcaseid.Text, this.txtCompanyID.Text);
                int count = documentmanagerInfo.Tables[0].Rows.Count;
                this.grddocumnetmanager.DataSource = documentmanagerInfo;
                grddocumnetmanager.SettingsPager.PageSize = count;
                this.grddocumnetmanager.DataBind();
                for (int i = 0; i < this.grddocumnetmanager.VisibleRowCount; i++)
                {
                    string str = "";
                    if (i == 0)
                    {
                        GridViewDataColumn column = (GridViewDataColumn)this.grddocumnetmanager.Columns[0];
                        CheckBox box = (CheckBox)this.grddocumnetmanager.FindRowCellTemplateControl(i, column, "chkall");
                        str = this.grddocumnetmanager.GetRowValues(i, new string[] { "File_Name" }).ToString();
                        box.Visible = false;
                    }
                    else if (str == this.grddocumnetmanager.GetRowValues(i, new string[] { "File_Name" }).ToString())
                    {
                        GridViewDataColumn column2 = (GridViewDataColumn)this.grddocumnetmanager.Columns[0];
                        CheckBox box2 = (CheckBox)this.grddocumnetmanager.FindRowCellTemplateControl(i, column2, "chkall");
                        box2.Visible = false;
                    }
                    else
                    {
                        str = this.grddocumnetmanager.GetRowValues(i, new string[] { "File_Name" }).ToString();
                    }
                    string str2 = "";
                    GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grddocumnetmanager.Columns[0];
                    CheckBox box3 = (CheckBox)this.grddocumnetmanager.FindRowCellTemplateControl(i, gridViewDataColumn, "chkall");
                    if (str2 == "")
                    {
                        box3.Visible = true;
                    }
                    else
                    {
                        box3.Visible = true;
                    }
                }
            }
            else
            {
                this.hdnmerge.Value = "";
            }
        }
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (this.ReportUpload.HasFile)
        {
            for (int i = 0; i < this.grddocumnetmanager.VisibleRowCount; i++)
            {
                GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grddocumnetmanager.Columns[0];
                CheckBox box = (CheckBox)this.grddocumnetmanager.FindRowCellTemplateControl(i, gridViewDataColumn, "chkall");
                if (box.Checked)
                {
                    string str = this.grddocumnetmanager.GetRowValues(i, new string[] { "NodeID" }).ToString();
                    Bill_Sys_Document_Manager manager = new Bill_Sys_Document_Manager();
                    string pathFromNodeId = manager.GetPathFromNodeId(str);
                    if (((pathFromNodeId == "0") || (pathFromNodeId == "")) || ((pathFromNodeId.ToUpper() == "NULL") || (pathFromNodeId == null)))
                    {
                        this.MessageControl1.PutMessage("Folder NodeType not Found Contact to admin");
                        this.MessageControl1.SetMessageType(0);
                        this.MessageControl1.Show();
                        log.Debug(" NodeType not Found ");
                        return;
                    }
                    log.Debug("szPath " + pathFromNodeId);
                    string str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + @"\" + pathFromNodeId + @"\";
                    log.Debug("szPath " + pathFromNodeId);
                    string str4 = ApplicationSettings.GetParameterValue("PhysicalBasePath");
                    log.Debug("szBasePath " + str4);
                    string fileName = this.ReportUpload.FileName;
                    log.Debug("szFileName " + fileName);
                    if (!Directory.Exists(str4 + str3))
                    {
                        log.Debug("File not Exists ");
                        Directory.CreateDirectory(str4 + str3);
                    }
                    if (File.Exists(str4 + str3 + fileName))
                    {
                        this.MessageControl1.PutMessage("File already Exists");
                        this.MessageControl1.SetMessageType(0);
                        this.MessageControl1.Show();
                        log.Debug("File already Exists");
                        return;
                    }
                    try
                    {
                        this.ReportUpload.SaveAs(str4 + str3 + fileName);
                        log.Debug("File Saved ");
                        string str6 = manager.SaveFileInDOc(str, fileName, str3, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        if (((str6 != "0") && (str6 != "")) && ((str6.ToUpper() != "NULL") && (str6 != null)))
                        {
                            DataSet documentmanagerInfo = new DataSet();
                            documentmanagerInfo = new Bill_Sys_Document_Manager().GetDocumentmanagerInfo(this.txtcaseid.Text, this.txtCompanyID.Text);
                            this.grddocumnetmanager.DataSource = documentmanagerInfo;
                            this.grddocumnetmanager.DataBind();
                            this.MessageControl1.PutMessage("File uploaded Successfully..! ");
                            this.MessageControl1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                            this.MessageControl1.Show();
                            log.Debug("File uploaded Successfully");
                        }
                        else
                        {
                            this.MessageControl1.PutMessage(" DB error to add File ");
                            this.MessageControl1.SetMessageType(0);
                            this.MessageControl1.Show();
                            log.Debug("DB error to ADD File");
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
                        base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

                    }
                    return;
                }
            }
        }
        else
        {
            this.MessageControl1.PutMessage("File was not selected for upload");
            this.MessageControl1.SetMessageType(0);
            this.MessageControl1.Show();
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}

