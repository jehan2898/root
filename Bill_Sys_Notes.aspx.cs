using AjaxControlToolkit;
using Componend;
using ExtendedDropDownList;
using NOTES_OBJECT;
using Reminders;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using XGridPagination;
using XGridSearchTextBox;
using XGridView;

public partial class Bill_Sys_NotesPage : Page, IRequiresSessionState
{

    private ListOperation _listOperation;

    private SaveOperation _saveOperation;

    private EditOperation _editOperation;



    public Bill_Sys_NotesPage()
    {
    }

    private void BindGrid()
    {
        this._listOperation = new ListOperation();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearControl();
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        this.Delete("DELETE");
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this.grdNotes.XGridBindSearch();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._saveOperation = new SaveOperation();
        try
        {
            this._saveOperation.WebPage = this.Page;
            this._saveOperation.Xml_File = "notes.xml";
            if (this.chkReminderPopup.Checked)
            {
                this.extddlNType.Text = "NTY0004";
            }
            this._saveOperation.SaveMethod();
            this.grdNotes.XGridBindSearch();
            this.extddlFilter.Selected_Text = "GENERAL";
            if (this.extddlNType.Text == "NTY0004" && this.txtNoteDesc.Text != "")
            {
                try
                {
                    ReminderBO reminderBO = new ReminderBO();
                    DataSet dataSet = null;
                    string str = "";
                    string str1 = "";
                    string str2 = "";
                    string sZCASEID = "";
                    string str3 = "";
                    DateTime dateTime = DateTime.Now.AddYears(2);
                    DateTime dateTime1 = Convert.ToDateTime(dateTime.ToShortDateString());
                    int num = 0;
                    int num1 = 0;
                    int num2 = 0;
                    int num3 = 0;
                    int num4 = 0;
                    int num5 = 0;
                    int num6 = 0;
                    int num7 = 0;
                    int num8 = 0;
                    int num9 = 0;
                    int num10 = 0;
                    int num11 = 0;
                    int num12 = 0;
                    int num13 = 0;
                    int num14 = 0;
                    int num15 = 0;
                    int num16 = 0;
                    int num17 = 100;
                    int num18 = 100;
                    int num19 = 0;
                    int num20 = 0;
                    int num21 = 100;
                    int num22 = 0;
                    int num23 = 100;
                    int num24 = 100;
                    int num25 = 100;
                    string str4 = "RS000000000000000001";
                    this.Session["ReminiderNotes"] = "";
                    str = this.txtNoteDesc.Text.Trim().ToString();
                    str = str.Replace('\n', ' ');
                    str = str.Replace('\r', ' ');
                    if (this.txtUserID.Text != "")
                    {
                        str2 = this.txtUserID.Text.Trim().ToString();
                    }
                    DateTime date = DateTime.Now.Date;
                    str1 = this.txtUserID.Text.Trim().ToString();
                    num2 = 1;
                    if ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"] != null)
                    {
                        sZCASEID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    }
                    string text = this.txtCompanyID.Text;
                    dataSet = reminderBO.SetReminderDetailsForCase(str, str1, str2, str4, date, dateTime1, num, num1, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11, num12, num13, num14, num15, num16, num17, num18, num19, num20, num21, num22, num23, num24, num25, str3, sZCASEID, text, "CASE", "", "");
                    if (dataSet.Tables.Count <= 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ClearValues();alert('Failed to add reminder details..!!')", true);
                    }
                    else if (dataSet.Tables[0].Rows.Count <= 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ClearValues();alert('Failed to add reminder details..!!')", true);
                    }
                    else if (dataSet.Tables[0].Rows[0]["result"].ToString() == "1")
                    {
                        this.Session["ReminiderNotes"] = true;
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ClearValues();alert('Reminder details added successfully...!!')", true);
                    }
                }
                catch (Exception exception)
                {
                    exception.ToString();
                }
            }
            this.ClearControl();
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

    protected void chkReminderPopup_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chkReminderPopup.Checked)
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "ss", "chkReminderPopup_onclick();", true);
        }
    }

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtNoteDesc.Text = "";
            this.extddlNoteType.Text = "NA";
            this.extddlNType.Text = "NA";
            this.chkReminderPopup.Checked = false;
            this.BindGrid();
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

    private void Delete(string flg)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        for (int i = 0; i < this.grdNotes.Rows.Count; i++)
        {
            if (((CheckBox)this.grdNotes.Rows[i].Cells[7].FindControl("chkdelete")).Checked)
            {
                string str1 = this.grdNotes.DataKeys[i]["I_NOTE_ID"].ToString();
                str = (str != "" ? string.Concat(str, "','", str1) : string.Concat("'", str1));
            }
        }
        if (str != "")
        {
            str = string.Concat(str, "'");
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_string"].ToString());
                SqlCommand sqlCommand = new SqlCommand("sp_notes_deletes", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.Add("@sz_Company_id", this.txtCompanyID.Text);
                sqlCommand.Parameters.Add("@i_note_id", str);
                sqlCommand.Parameters.Add("@flag", flg);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                this.grdNotes.XGridBindSearch();
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

    protected void extddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this.grdNotes.XGridBindSearch();
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
            this.grdPatientDeskList.DataSource = billSysPatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
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

    protected void grdNotes_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat(" window.location.href ='", ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString(), this.grdNotes.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()), "';"), true);
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
            this.con.SourceGrid = this.grdNotes;
            this.txtSearchBox.SourceGrid = this.grdNotes;
            this.grdNotes.Page = this.Page;
            this.grdNotes.PageNumberList = this.con;
            this.chkReminderPopup.Attributes.Add("OnClick", "chkReminderPopup_onclick(this);");
            this.btnSave.Attributes.Add("onclick", "return formValidator('frmNotes','extddlNType','txtNoteDesc');");
            this.btndelete.Attributes.Add("onclick", "return ConfirmDelete();");
            this.softdelete.Attributes.Add("onclick", "return ConfirmDelete();");
            this.txtCompanyIDForNotes.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (this.Session["CASE_OBJECT"] == null)
            {
                base.Response.Redirect("Bill_Sys_SearchCase.aspx", false);
            }
            else
            {
                this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                Bill_Sys_Case billSysCase = new Bill_Sys_Case()
                {
                    SZ_CASE_ID = this.txtCaseID.Text
                };
                this.Session["CASEINFO"] = billSysCase;
                string text = this.txtCaseID.Text;
                this.Session["QStrCaseID"] = text;
                this.Session["Case_ID"] = text;
                this.Session["Archived"] = "0";
                this.Session["QStrCID"] = text;
                this.Session["SelectedID"] = text;
                this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["SN"] = "0";
                this.Session["LastAction"] = "vb_CaseInformation.aspx";
                this.Session["SZ_CASE_ID_NOTES"] = this.txtCaseID.Text;
                this.Session["TM_SZ_CASE_ID"] = this.txtCaseID.Text;
                this.GetPatientDeskList();
            }
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.txtUserID.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this.txtNoteCode.Text = Note_Code.New_Note_Added;
            if (!base.IsPostBack)
            {
                this.BindGrid();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_NOTE_DELETE == "True")
                {
                    this.btndelete.Visible = true;
                }
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_NOTE_SOFT_DELETE == "True")
                {
                    this.softdelete.Visible = true;
                }
                this.extddlFilter.Text = "NTY0002";
                this.grdNotes.XGridBindSearch();
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
       
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_NotesPage.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void SoftDelete()
    {
    }

    protected void softDelete_Click(object sender, EventArgs e)
    {
        this.Delete("SOFTDELETE");
    }
}