using Componend;
using ExtendedDropDownList;
using RequiredDocuments;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bill_Sys_RequiredDocuments : Page, IRequiresSessionState
{
  

    private SaveOperation _saveOperation;

    private EditOperation _editOperation;

    private ListOperation _listOperation;

    private Bill_Sys_NF3_Template _nf3Template;

    private readonly int COL_I_DOCUMENT_TYPE_ID = 19;

    private readonly int COL_I_NODE_TYPE_ID = 20;

    private readonly int COL_I_TRANSACTION_ID = 1;

    private readonly int COL_I_IMAGE_ID = 23;


    public Bill_Sys_RequiredDocuments()
    {
    }

    private void BindAllControls()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        try
        {
            this._editOperation.WebPage = this.Page;
            this._editOperation.Xml_File = "DocumentXML.xml";
            this._editOperation.Primary_Value = this.txtCaseID.Text;
            this._editOperation.LoadData();
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

    private void BindCaseDocumentGrid()
    {
        this._listOperation = new ListOperation()
        {
            WebPage = this.Page,
            Xml_File = "CaseTypeDocumentXML.xml"
        };
        this._listOperation.LoadList();
        for (int i = 0; i < this.grdDocumentGrid.Items.Count; i++)
        {
            if (this.grdDocumentGrid.Items[i].Cells[13].Text != "" && this.grdDocumentGrid.Items[i].Cells[13].Text != "&nbsp;")
            {
                ((ExtendedDropDownList.ExtendedDropDownList)this.grdDocumentGrid.Items[i].FindControl("extddlAssignTo")).Text = this.grdDocumentGrid.Items[i].Cells[13].Text;
            }
            else if (this.grdDocumentGrid.Items[i].Cells[15].Text != "" && this.grdDocumentGrid.Items[i].Cells[15].Text != "&nbsp;")
            {
                CheckBox checkBox = (CheckBox)this.grdDocumentGrid.Items[i].FindControl("chkRecieved");
                if (this.grdDocumentGrid.Items[i].Cells[15].Text != "1")
                {
                    this.grdDocumentGrid.Items[i].Cells[8].Text = "";
                    this.grdDocumentGrid.Items[i].Cells[9].Text = "";
                    this.grdDocumentGrid.Items[i].Cells[10].Text = "";
                    this.grdDocumentGrid.Items[i].Cells[11].Text = "";
                }
                else
                {
                    this.grdDocumentGrid.Items[i].Cells[8].Text = "";
                    this.grdDocumentGrid.Items[i].Cells[9].Text = "";
                }
            }
            if (this.grdDocumentGrid.Items[i].Cells[14].Text != "" && this.grdDocumentGrid.Items[i].Cells[14].Text != "&nbsp;")
            {
                TextBox textBox = (TextBox)this.grdDocumentGrid.Items[i].FindControl("txtNotes");
                textBox.Text = this.grdDocumentGrid.Items[i].Cells[14].Text;
            }
            if (this.grdDocumentGrid.Items[i].Cells[15].Text != "" && this.grdDocumentGrid.Items[i].Cells[15].Text != "&nbsp;")
            {
                CheckBox checkBox1 = (CheckBox)this.grdDocumentGrid.Items[i].FindControl("chkRecieved");
                if (this.grdDocumentGrid.Items[i].Cells[15].Text != "1")
                {
                    checkBox1.Checked = false;
                }
                else
                {
                    checkBox1.Checked = true;
                }
            }
        }
    }

    protected void btnDeleteDocument_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList arrayLists = new ArrayList();
        for (int i = 0; i < this.grdDocumentGrid.Items.Count; i++)
        {
            if (((CheckBox)this.grdDocumentGrid.Items[i].FindControl("delDocument")).Checked)
            {
                Bill_Sys_RequiredDAO billSysRequiredDAO = new Bill_Sys_RequiredDAO()
                {
                    CaseId = this.txtCaseID.Text,
                    CompanyId = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID,
                    TransactionID = this.grdDocumentGrid.Items[i].Cells[this.COL_I_TRANSACTION_ID].Text,
                    ImageID = this.grdDocumentGrid.Items[i].Cells[24].Text
                };
                if (billSysRequiredDAO.ImageID != "" && !billSysRequiredDAO.ImageID.Contains("nbsp"))
                {
                    arrayLists.Add(billSysRequiredDAO);
                }
            }
        }
        try
        {
            (new Bill_Sys_RequiredDocumentBO()).DeleteRequiredDocuments(arrayLists);
            this.usrMessage.PutMessage("Changes to the server were made successfully");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
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
        
        this.BindCaseDocumentGrid();
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDocUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._saveOperation = new SaveOperation();
        this._editOperation = new EditOperation();
        try
        {
            for (int i = 0; i < this.grdDocumentGrid.Items.Count; i++)
            {
                if (!(this.grdDocumentGrid.Items[i].Cells[1].Text != "") || !(this.grdDocumentGrid.Items[i].Cells[1].Text != "&nbsp;"))
                {
                    this.txtDocID.Text = this.grdDocumentGrid.Items[i].Cells[3].Text;
                    if (!(this.grdDocumentGrid.Items[i].Cells[8].Text != "") || !(this.grdDocumentGrid.Items[i].Cells[8].Text != "&nbsp;"))
                    {
                        this.txtAssignOn.Text = "";
                    }
                    else
                    {
                        this.txtAssignOn.Text = this.grdDocumentGrid.Items[i].Cells[8].Text;
                    }
                    ExtendedDropDownList.ExtendedDropDownList extendedDropDownList = (ExtendedDropDownList.ExtendedDropDownList)this.grdDocumentGrid.Items[i].FindControl("extddlAssignTo");
                    if (!(extendedDropDownList.Text != "NA") || !(extendedDropDownList.Text != ""))
                    {
                        this.txtAssignTo.Text = "";
                    }
                    else
                    {
                        this.txtAssignTo.Text = extendedDropDownList.Text;
                    }
                    TextBox textBox = (TextBox)this.grdDocumentGrid.Items[i].FindControl("txtNotes");
                    if (textBox.Text == "")
                    {
                        this.txtNotes.Text = "";
                    }
                    else
                    {
                        this.txtNotes.Text = textBox.Text;
                    }
                    if (!((CheckBox)this.grdDocumentGrid.Items[i].FindControl("chkRecieved")).Checked)
                    {
                        this.txtRecieved.Text = "0";
                    }
                    else
                    {
                        this.txtRecieved.Text = "1";
                    }
                    this._saveOperation.WebPage = this.Page;
                    this._saveOperation.Xml_File = "CaseTypeDocumentXML.xml";
                    this._saveOperation.SaveMethod();
                }
                else
                {
                    this.txtDocID.Text = this.grdDocumentGrid.Items[i].Cells[1].Text;
                    if (!(this.grdDocumentGrid.Items[i].Cells[8].Text != "") || !(this.grdDocumentGrid.Items[i].Cells[8].Text != "&nbsp;"))
                    {
                        this.txtAssignOn.Text = "";
                    }
                    else
                    {
                        this.txtAssignOn.Text = this.grdDocumentGrid.Items[i].Cells[8].Text;
                    }
                    ExtendedDropDownList.ExtendedDropDownList extendedDropDownList1 = (ExtendedDropDownList.ExtendedDropDownList)this.grdDocumentGrid.Items[i].FindControl("extddlAssignTo");
                    if (!(extendedDropDownList1.Text != "NA") || !(extendedDropDownList1.Text != ""))
                    {
                        this.txtAssignTo.Text = "";
                    }
                    else
                    {
                        this.txtAssignTo.Text = extendedDropDownList1.Text;
                    }
                    TextBox textBox1 = (TextBox)this.grdDocumentGrid.Items[i].FindControl("txtNotes");
                    if (textBox1.Text == "")
                    {
                        this.txtNotes.Text = "";
                    }
                    else
                    {
                        this.txtNotes.Text = textBox1.Text;
                    }
                    if (!((CheckBox)this.grdDocumentGrid.Items[i].FindControl("chkRecieved")).Checked)
                    {
                        this.txtRecieved.Text = "0";
                    }
                    else
                    {
                        this.txtRecieved.Text = "1";
                    }
                    this._editOperation.WebPage = this.Page;
                    this._editOperation.Xml_File = "CaseTypeDocumentXML.xml";
                    this._editOperation.Primary_Value = this.grdDocumentGrid.Items[i].Cells[1].Text;
                    this._editOperation.UpdateMethod();
                }
            }
            this.BindCaseDocumentGrid();
            this.usrMessage.PutMessage("Changes to the server were made successfully");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
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

    protected void btnFileUpload_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._saveOperation = new SaveOperation();
            this._editOperation = new EditOperation();
            this._nf3Template = new Bill_Sys_NF3_Template();
            string physicalPath = this._nf3Template.getPhysicalPath();
            for (int i = 0; i < this.grdDocumentGrid.Items.Count; i++)
            {
                System.Web.UI.WebControls.FileUpload fileUpload = (System.Web.UI.WebControls.FileUpload)this.grdDocumentGrid.Items[i].FindControl("fileuploadDocument");
                string str = "";
                if (fileUpload.FileName != "")
                {
                    Bill_Sys_RequiredDocumentBO billSysRequiredDocumentBO = new Bill_Sys_RequiredDocumentBO();
                    string nodePath = billSysRequiredDocumentBO.GetNodePath(this.grdDocumentGrid.Items[i].Cells[this.COL_I_NODE_TYPE_ID].Text, this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    nodePath = nodePath.Replace("\\", "/");
                    str = string.Concat(physicalPath, nodePath);
                    if (!Directory.Exists(str))
                    {
                        Directory.CreateDirectory(str);
                    }
                    fileUpload.SaveAs(string.Concat(str, "/", fileUpload.FileName));
                    ArrayList arrayLists = new ArrayList();
                    arrayLists.Add(this.txtCaseID.Text);
                    arrayLists.Add(this.grdDocumentGrid.Items[i].Cells[18].Text.Replace("/", ""));
                    arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    arrayLists.Add(fileUpload.FileName);
                    arrayLists.Add(string.Concat(nodePath, "/"));
                    arrayLists.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    arrayLists.Add(this.grdDocumentGrid.Items[i].Cells[this.COL_I_NODE_TYPE_ID].Text.ToString());
                   
                    string str1 = this._nf3Template.SaveDocumentData(arrayLists);
                    if (!(this.grdDocumentGrid.Items[i].Cells[1].Text != "") || !(this.grdDocumentGrid.Items[i].Cells[1].Text != "&nbsp;"))
                    {
                        this.txtDocID.Text = this.grdDocumentGrid.Items[i].Cells[this.COL_I_DOCUMENT_TYPE_ID].Text;
                        if (!(this.grdDocumentGrid.Items[i].Cells[8].Text != "") || !(this.grdDocumentGrid.Items[i].Cells[8].Text != "&nbsp;"))
                        {
                            this.txtAssignOn.Text = "";
                        }
                        else
                        {
                            this.txtAssignOn.Text = this.grdDocumentGrid.Items[i].Cells[8].Text;
                        }
                        ExtendedDropDownList.ExtendedDropDownList extendedDropDownList = (ExtendedDropDownList.ExtendedDropDownList)this.grdDocumentGrid.Items[i].FindControl("extddlAssignTo");
                        if (!(extendedDropDownList.Text != "NA") || !(extendedDropDownList.Text != ""))
                        {
                            this.txtAssignTo.Text = "";
                        }
                        else
                        {
                            this.txtAssignTo.Text = extendedDropDownList.Text;
                        }
                        TextBox textBox = (TextBox)this.grdDocumentGrid.Items[i].FindControl("txtNotes");
                        if (textBox.Text == "")
                        {
                            this.txtNotes.Text = "";
                        }
                        else
                        {
                            this.txtNotes.Text = textBox.Text;
                        }
                        this.txtRecieved.Text = "1";
                        this.txtImageId.Text = str1;
                        this._saveOperation.WebPage = this.Page;
                        this._saveOperation.Xml_File = "CaseTypeDocumentXML.xml";
                        this._saveOperation.SaveMethod();
                    }
                    else
                    {
                        this.txtDocID.Text = this.grdDocumentGrid.Items[i].Cells[1].Text;
                        if (!(this.grdDocumentGrid.Items[i].Cells[8].Text != "") || !(this.grdDocumentGrid.Items[i].Cells[8].Text != "&nbsp;"))
                        {
                            this.txtAssignOn.Text = "";
                        }
                        else
                        {
                            this.txtAssignOn.Text = this.grdDocumentGrid.Items[i].Cells[8].Text;
                        }
                        ExtendedDropDownList.ExtendedDropDownList extendedDropDownList1 = (ExtendedDropDownList.ExtendedDropDownList)this.grdDocumentGrid.Items[i].FindControl("extddlAssignTo");
                        if (!(extendedDropDownList1.Text != "NA") || !(extendedDropDownList1.Text != ""))
                        {
                            this.txtAssignTo.Text = "";
                        }
                        else
                        {
                            this.txtAssignTo.Text = extendedDropDownList1.Text;
                        }
                        TextBox textBox1 = (TextBox)this.grdDocumentGrid.Items[i].FindControl("txtNotes");
                        if (textBox1.Text == "")
                        {
                            this.txtNotes.Text = "";
                        }
                        else
                        {
                            this.txtNotes.Text = textBox1.Text;
                        }
                        this.txtRecieved.Text = "1";
                        this.txtImageId.Text = str1;
                        this._editOperation.WebPage = this.Page;
                        this._editOperation.Xml_File = "CaseTypeDocumentXML.xml";
                        this._editOperation.Primary_Value = this.grdDocumentGrid.Items[i].Cells[1].Text;
                        this._editOperation.UpdateMethod();
                    }
                }
            }
            this.BindCaseDocumentGrid();
            this.usrMessage.PutMessage("Changes to the server were made successfully");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        try
        {
            this._editOperation.WebPage = this.Page;
            this._editOperation.Xml_File = "DocumentXML.xml";
            this._editOperation.Primary_Value = this.txtCaseID.Text;
            this._editOperation.UpdateMethod();
            this.usrMessage.PutMessage("Changes to the server were made successfully");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
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

    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdPatientDeskList.DataSource = billSysPatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.txtCompanyID.Text);
            this.grdPatientDeskList.DataBind();
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

    protected void grdDocumentGrid_ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.Equals("Show Doc"))
            {
                string str = e.CommandArgument.ToString();
                Bill_Sys_RequiredDocumentBO billSysRequiredDocumentBO = new Bill_Sys_RequiredDocumentBO();
                string str1 = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
                string imageFullPath = billSysRequiredDocumentBO.GetImageFullPath(str);
                if (imageFullPath == null || !(imageFullPath != "") || !(imageFullPath != "There is no row at position 0."))
                {
                    ScriptManager.RegisterStartupScript(this, base.GetType(), "opendocument", "alert('Document is not available. Please upload documents');", true);
                }
                else
                {
                    string str2 = string.Concat(str1, billSysRequiredDocumentBO.GetImageFullPath(str));
                    ScriptManager.RegisterStartupScript(this, base.GetType(), "opendocument", string.Concat("window.open('", str2, "', 'opendocument','channelmode=no,location=yes,toolbar=yes,menubar=1,resizable=1,scrollbars=1, width=600,height=550'); "), true);
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

    protected void grdDocumentGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ExtendedDropDownList.ExtendedDropDownList sZCOMPANYID = (ExtendedDropDownList.ExtendedDropDownList)e.Item.FindControl("extddlAssignTo");
            if (sZCOMPANYID != null)
            {
                sZCOMPANYID.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
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

    protected void grdDocumentGrid_SelectedIndexChanged1(object sender, EventArgs e)
    {
        this.RedirectToScanApp(this.grdDocumentGrid.SelectedIndex);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!base.IsPostBack)
            {
                this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this.txtUserID.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                hdnCaseId.Value = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                hdnPatientId.Value = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;
                this.BindCaseDocumentGrid();
                this.GetPatientDeskList();
            }
            if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
            {
                (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_RequiredDocuments.aspx");
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

    public void RedirectToScanApp(int iindex)
    {
        iindex = this.grdDocumentGrid.SelectedIndex;
        this.txtDocID.Text = this.grdDocumentGrid.Items[iindex].Cells[3].Text;
        if (!(this.grdDocumentGrid.Items[iindex].Cells[8].Text != "") || !(this.grdDocumentGrid.Items[iindex].Cells[8].Text != "&nbsp;"))
        {
            this.txtAssignOn.Text = "";
        }
        else
        {
            this.txtAssignOn.Text = this.grdDocumentGrid.Items[iindex].Cells[8].Text;
        }
        ExtendedDropDownList.ExtendedDropDownList extendedDropDownList = (ExtendedDropDownList.ExtendedDropDownList)this.grdDocumentGrid.Items[iindex].FindControl("extddlAssignTo");
        if (!(extendedDropDownList.Text != "NA") || !(extendedDropDownList.Text != ""))
        {
            this.txtAssignTo.Text = "";
        }
        else
        {
            this.txtAssignTo.Text = extendedDropDownList.Text;
        }
        TextBox textBox = (TextBox)this.grdDocumentGrid.Items[iindex].FindControl("txtNotes");
        if (textBox.Text == "")
        {
            this.txtNotes.Text = "";
        }
        else
        {
            this.txtNotes.Text = textBox.Text;
        }
        this.txtRecieved.Text = this.grdDocumentGrid.Items[iindex].Cells[5].Text;
        string str = ConfigurationManager.AppSettings["webscanurl"].ToString();
        string[] text = new string[] { str, "NodeTypeID=", this.grdDocumentGrid.Items[iindex].Cells[this.COL_I_NODE_TYPE_ID].Text, "&CaseDocumentID=", this.grdDocumentGrid.Items[iindex].Cells[1].Text, "&CaseId=", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, "&UserName=", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "&CompanyName=", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME };
        str = string.Concat(text);
        string[] strArrays = new string[] { str, "&Received=", this.txtRecieved.Text, "&Notes=", this.txtNotes.Text, "&Assignto=", this.txtAssignTo.Text, "&AssignOn=", this.txtAssignOn.Text, "&DocType=", this.grdDocumentGrid.Items[iindex].Cells[18].Text.Replace("/", "").Substring(this.grdDocumentGrid.Items[iindex].Cells[18].Text.Replace("/", "").LastIndexOf("->") + 3, this.grdDocumentGrid.Items[iindex].Cells[18].Text.Replace("/", "").Length - (this.grdDocumentGrid.Items[iindex].Cells[18].Text.Replace("/", "").LastIndexOf("->") + 3)), "&DocTypeId=", this.grdDocumentGrid.Items[iindex].Cells[3].Text };
        str = string.Concat(strArrays);
        string[] sZCOMPANYID = new string[] { str, "&CompanyId=", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "&UserId=", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, "&Flag=ReqDoc&CaseNo=", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, "&PName=", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_NAME };
        str = string.Concat(sZCOMPANYID);
        ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", string.Concat("window.open('", str, "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); "), true);
    }
}