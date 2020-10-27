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
using NOTES_OBJECT;
using System.Data.Sql;
using System.Data.SqlClient;
using Reminders;

public partial class atnotes : PageBase
{
    private ListOperation _listOperation;
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            this.con.SourceGrid = grdNotes;
            this.txtSearchBox.SourceGrid = grdNotes;
            this.grdNotes.Page = this.Page;
            this.grdNotes.PageNumberList = this.con;
            chkReminderPopup.Attributes.Add("OnClick", "chkReminderPopup_onclick(this);");

            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;

            btnSave.Attributes.Add("onclick", "return formValidator('frmNotes','extddlNType','txtNoteDesc');");
            btndelete.Attributes.Add("onclick", "return ConfirmDelete();");
            softdelete.Attributes.Add("onclick", "return ConfirmDelete();");
            txtCompanyIDForNotes.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (Session["CASE_OBJECT"] != null)
            {
                txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                //////////////////////
                //CREATE SESSION FOR DOC MANAGER,TEMPLATE MANAGER,Notes,Bills

                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = txtCaseID.Text;

                Session["CASEINFO"] = _bill_Sys_Case;


                String szURL = "";
                String szCaseID = txtCaseID.Text;
                Session["QStrCaseID"] = szCaseID;
                Session["Case_ID"] = szCaseID;
                Session["Archived"] = "0";
                Session["QStrCID"] = szCaseID;
                Session["SelectedID"] = szCaseID;
                Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["SN"] = "0";
                Session["LastAction"] = "vb_CaseInformation.aspx";


                Session["SZ_CASE_ID_NOTES"] = txtCaseID.Text;

                Session["TM_SZ_CASE_ID"] = txtCaseID.Text;
                GetPatientDeskList();
                //
                ///////////////////
            }
            else
            {
                Response.Redirect("Bill_Sys_SearchCase.aspx", false);
            }

            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            txtNoteCode.Text = Note_Code.New_Note_Added;
            //if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower().Equals("admin"))
            //{
            //    btndelete.Visible = true;
            //    softdelete.Visible = false;
            //}
            //else
            //{
            //    btndelete.Visible = false;
            //    softdelete.Visible = true;
            //}
            if (!IsPostBack)
            {


                BindGrid();
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_NOTE_DELETE == "True")
                {
                    btndelete.Visible = true;
                }
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_NOTE_SOFT_DELETE == "True")
                {
                    softdelete.Visible = true;
                }
                extddlFilter.Text = "NTY0002";
                grdNotes.XGridBindSearch();
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_NotesPage.aspx");
        }
        #endregion
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Delete("DELETE");
    }

    protected void softDelete_Click(object sender, EventArgs e)
    {
        Delete("SOFTDELETE");
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
            _saveOperation.Xml_File = "notes.xml";

            //extddlNoteType.Text = extddlNType.Text;

            if (chkReminderPopup.Checked == true)
            {
                extddlNType.Text = "NTY0004";
            }

            _saveOperation.SaveMethod();
            grdNotes.XGridBindSearch();

            extddlFilter.Selected_Text = "GENERAL";
            if (extddlNType.Text == "NTY0004" && txtNoteDesc.Text != "")
            {
                try
                {
                    Reminders.ReminderBO objReminder = new ReminderBO();
                    DataSet dsReminder = null;
                    string str_description = "";
                    string str_assigned_to = "";
                    string str_assigned_by = "";
                    string str_case_id = "";
                    string str_docotr_id = "";
                    DateTime dt_start_date;
                    DateTime dt_end_date = Convert.ToDateTime(System.DateTime.Now.AddYears(2).ToShortDateString());
                    int i_is_recurrence = 0;
                    int i_recurrence_type = 0;
                    int i_occurrence_end_count = 0;
                    int i_day_option = 0;
                    int i_d_day_count = 0;
                    int i_d_every_weekday = 0;
                    int i_w_recur_week_count = 0;
                    int i_w_sunday = 0;
                    int i_w_monday = 0;
                    int i_w_tuesday = 0;
                    int i_w_wednesday = 0;
                    int i_w_thursday = 0;
                    int i_w_friday = 0;
                    int i_w_saturday = 0;
                    int i_month_option = 0;
                    int i_m_day = 0;
                    int i_m_month_count = 0;
                    int i_m_term = 100;
                    int i_m_term_week = 100;
                    int i_m_every_month_count = 0;
                    int i_year_option = 0;
                    int i_y_month = 100;
                    int i_y_day = 0;
                    int i_y_term = 100;
                    int i_y_term_week = 100;
                    int i_y_every_month_count = 100;
                    string strReminderStatus = "RS000000000000000001";

                    Session["ReminiderNotes"] = "";

                    str_description = txtNoteDesc.Text.Trim().ToString();
                    str_description = str_description.Replace('\n', ' ');
                    str_description = str_description.Replace('\r', ' ');

                    if (txtUserID.Text != "")
                    {
                        str_assigned_by = txtUserID.Text.Trim().ToString();
                    }
                    dt_start_date = System.DateTime.Now.Date;

                    str_assigned_to = txtUserID.Text.Trim().ToString();

                    i_occurrence_end_count = 1;



                    if (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]) != null)
                    {
                        str_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    }
                    string strCompanyID = txtCompanyID.Text;


                    dsReminder = objReminder.SetReminderDetailsForCase(str_description, str_assigned_to, str_assigned_by, strReminderStatus, dt_start_date, dt_end_date, i_is_recurrence, i_recurrence_type, i_occurrence_end_count, i_day_option, i_d_day_count, i_d_every_weekday, i_w_recur_week_count, i_w_sunday, i_w_monday, i_w_tuesday, i_w_wednesday, i_w_thursday, i_w_friday, i_w_saturday, i_month_option, i_m_day, i_m_month_count, i_m_term, i_m_term_week, i_m_every_month_count, i_year_option, i_y_month, i_y_day, i_y_term, i_y_term_week, i_y_every_month_count, str_docotr_id, str_case_id, strCompanyID, "CASE", "", "");
                    if (dsReminder.Tables.Count > 0)
                    {
                        if (dsReminder.Tables[0].Rows.Count > 0)
                        {
                            if (dsReminder.Tables[0].Rows[0]["result"].ToString() == "1")
                            {
                                Session["ReminiderNotes"] = true;
                                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues();alert('Reminder details added successfully...!!')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues();alert('Failed to add reminder details..!!')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues();alert('Failed to add reminder details..!!')", true);
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
            }
            ClearControl();
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

    #region "Fetch Method"
    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _listOperation = new ListOperation();
        try
        {
            //_listOperation.WebPage = this.Page;
            //_listOperation.Xml_File = "notes.xml";
            //_listOperation.LoadList();
            //SoftDelete();
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
    #endregion
    protected void btnFilter_Click(object sender, EventArgs e)
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _listOperation = new ListOperation();
        try
        {
            grdNotes.XGridBindSearch();
            //grdNotes.CurrentPageIndex = 0;      
            //_listOperation.WebPage = this.Page;
            //_listOperation.Xml_File = "NoteSearch.xml";
            //_listOperation.LoadList();          
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ClearControl();
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
    protected void grdNotes_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //grdNotes.CurrentPageIndex = e.NewPageIndex;
            //BindGrid();
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
    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtNoteDesc.Text = "";
            extddlNoteType.Text = "NA";
            extddlNType.Text = "NA";
            chkReminderPopup.Checked = false;
            BindGrid();
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
    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            grdPatientDeskList.DataSource = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdPatientDeskList.DataBind();

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


    private void Delete(string flg)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string id_ = "";
        for (int i = 0; i < grdNotes.Rows.Count; i++)
        {
            //CheckBox chkdelete = (CheckBox)grdNotes.Items[i].FindControl("chkdelete");
            CheckBox chkdelete = (CheckBox)grdNotes.Rows[i].Cells[7].FindControl("chkdelete");
            if (chkdelete.Checked)
            {
                string i_d = grdNotes.DataKeys[i]["I_NOTE_ID"].ToString();
                if (id_ == "")
                {
                    id_ = "'" + i_d;
                }
                else
                {
                    id_ = id_ + "'," + "'" + i_d;
                }
            }

        }
        if (id_ != "")
        {
            id_ = id_ + "'";
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection_string"].ToString());
                SqlCommand sqlcmd = new SqlCommand("sp_notes_deletes", con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@sz_Company_id", txtCompanyID.Text);
                sqlcmd.Parameters.Add("@i_note_id", id_);
                sqlcmd.Parameters.Add("@flag", flg);
                con.Open();
                sqlcmd.ExecuteNonQuery();
                con.Close();
                //BindGrid();
                grdNotes.XGridBindSearch();

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
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    public void SoftDelete()
    {
        //for (int i = 0; i < grdNotes.Items.Count; i++)
        //{
        //    string bt_delete = grdNotes.Items[i].Cells[6].Text.ToString();
        //    if (bt_delete.ToLower() == "true")
        //    {
        //        for (int k = 0; k < grdNotes.Items[i].Cells.Count; k++)
        //        {
        //           grdNotes.Items[i].Cells[k].Style.Add("text-decoration", "line-through");
        //        }


        //    }
        //}
    }

    #region "ExportTOExcel"
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdNotes.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
    #endregion

    protected void extddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _listOperation = new ListOperation();
        try
        {
            grdNotes.XGridBindSearch();
            //grdNotes.CurrentPageIndex = 0;      
            //_listOperation.WebPage = this.Page;
            //_listOperation.Xml_File = "NoteSearch.xml";
            //_listOperation.LoadList();          
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

    protected void chkReminderPopup_CheckedChanged(object sender, EventArgs e)
    {
        if (chkReminderPopup.Checked)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "ss", "chkReminderPopup_onclick();", true);
            // divReminder.Visible = true;
        }
    }

}