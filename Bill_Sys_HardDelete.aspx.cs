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
using log4net;
using System.IO;
public partial class Bill_Sys_HardDelete : PageBase
{
    private CaseDetailsBO _caseDetailsBO;
    Bill_Sys_Case _bill_Sys_Case;
    private static ILog log = LogManager.GetLogger("Bill_Sys_HardDelete");
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {
                BindGrid();
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
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_HardDelete.aspx");
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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _caseDetailsBO = new CaseDetailsBO();
        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        try
        {
            bool flagdelete = false;
            foreach (DataGridItem grdItem in grdCaseMaster.Items)
            {
                CheckBox chkDelete = (CheckBox)grdItem.FindControl("chkHardDelete");
                if (chkDelete.Checked)
                {
                    LinkButton lnk = (LinkButton)grdItem.FindControl("lnkSelectCase");
                    _caseDetailsBO.HardDelete(lnk.CommandArgument.ToString(), txtCompanyID.Text);

                    flagdelete = true;
                    String szDefaultPath = objNF3Template.getPhysicalPath();
                    String szDestinationDir = "";

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                    {
                        szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                    else
                    {
                        szDestinationDir = objNF3Template.GetCompanyName(grdItem.Cells[31].Text);

                    }
                    szDestinationDir = szDestinationDir + "/" + lnk.CommandArgument.ToString();

                    if (Directory.Exists(szDefaultPath + szDestinationDir))
                    {
                        string[] files = Directory.GetFiles(szDefaultPath + szDestinationDir, "*.*", SearchOption.AllDirectories);
                        foreach (string file in files)
                        {
                            System.IO.File.Delete(file);
                        }

                        Directory.Delete(szDefaultPath + szDestinationDir, true);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "CASE_DELETED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Case Id " + lnk.CommandArgument.ToString() + "deleted.";
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);

                }
            }

            if (flagdelete == true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "mm", "<script language='javascript'>alert('Case deleted successfully');</script>");
            }
            BindGrid();
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _caseDetailsBO = new CaseDetailsBO();
        try
        {
            grdCaseMaster.DataSource = _caseDetailsBO.GetHardDeleteList(txtCompanyID.Text);
            grdCaseMaster.DataBind();

            foreach (DataGridItem grditem in grdCaseMaster.Items)
            {
                if (grditem.Cells[35].Text == "True")
                {
                    grditem.Cells[0].Style.Add("text-decoration", "line-through");
                    grditem.Cells[1].Style.Add("text-decoration", "line-through");
                    grditem.Cells[2].Style.Add("text-decoration", "line-through");
                    grditem.Cells[3].Style.Add("text-decoration", "line-through");
                    grditem.Cells[4].Style.Add("text-decoration", "line-through");
                    grditem.Cells[5].Style.Add("text-decoration", "line-through");
                    grditem.Cells[6].Style.Add("text-decoration", "line-through");
                    grditem.Cells[7].Style.Add("text-decoration", "line-through");
                    grditem.Cells[8].Style.Add("text-decoration", "line-through");
                    grditem.Cells[9].Style.Add("text-decoration", "line-through");
                    grditem.Cells[10].Style.Add("text-decoration", "line-through");
                    grditem.Cells[11].Style.Add("text-decoration", "line-through");
                    grditem.Cells[12].Style.Add("text-decoration", "line-through");
                    grditem.Cells[13].Style.Add("text-decoration", "line-through");
                    grditem.Cells[14].Style.Add("text-decoration", "line-through");
                    grditem.Cells[15].Style.Add("text-decoration", "line-through");
                    grditem.Cells[16].Style.Add("text-decoration", "line-through");
                    grditem.Cells[17].Style.Add("text-decoration", "line-through");
                    grditem.Cells[18].Style.Add("text-decoration", "line-through");
                    grditem.Cells[19].Style.Add("text-decoration", "line-through");
                    grditem.Cells[20].Style.Add("text-decoration", "line-through");
                    grditem.Cells[21].Style.Add("text-decoration", "line-through");
                    grditem.Cells[22].Style.Add("text-decoration", "line-through");
                    grditem.Cells[23].Style.Add("text-decoration", "line-through");
                    grditem.Cells[24].Style.Add("text-decoration", "line-through");
                    grditem.Cells[25].Style.Add("text-decoration", "line-through");
                    grditem.Cells[26].Style.Add("text-decoration", "line-through");
                    grditem.Cells[27].Style.Add("text-decoration", "line-through");
                    grditem.Cells[28].Style.Add("text-decoration", "line-through");
                    grditem.Cells[29].Style.Add("text-decoration", "line-through");
                    grditem.Cells[30].Style.Add("text-decoration", "line-through");
                    grditem.Cells[31].Style.Add("text-decoration", "line-through");
                    grditem.Cells[32].Style.Add("text-decoration", "line-through");
                    grditem.Cells[33].Style.Add("text-decoration", "line-through");
                    grditem.Cells[34].Style.Add("text-decoration", "line-through");
                    grditem.Cells[35].Style.Add("text-decoration", "line-through");
                }
            }

            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_HARD_DELETE != "True")
            {
                grdCaseMaster.Columns[36].Visible = false;
                btnDelete.Visible = false;
                btnUnDelete.Visible = false;
            }

        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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
    protected void grdCaseMaster_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "CaseSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }
            if (e.CommandName.ToString() == "ChartNoSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }
            if (e.CommandName.ToString() == "PatientSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }
            if (e.CommandName.ToString() == "DOASearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }
            if (e.CommandName.ToString() == "CASETYPESearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }
            if (e.CommandName.ToString() == "DOOSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }
            if (e.CommandName.ToString() == "Select")
            {

                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[34].Text;
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = e.Item.Cells[31].Text;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != e.Item.Cells[28].Text)
                {
                    Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
                }
                else
                {
                    Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
                }
            }

            else if (e.CommandName.ToString() == "Bill Transaction")
            {

                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[34].Text;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;

                Response.Redirect("Bill_Sys_BillTransaction.aspx", false);
            }
            else if (e.CommandName.ToString() == "View Bills")
            {
                Session["SZ_CASE_ID"] = e.CommandArgument;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;

                Response.Redirect("Bill_Sys_BillSearch.aspx", false);
            }
            else if (e.CommandName.ToString() == "Document Manager")
            {
                // Create Session for document Manager
                Session["PassedCaseID"] = e.CommandArgument;
                String szURL = "";
                String szCaseID = Session["PassedCaseID"].ToString();
                Session["QStrCaseID"] = szCaseID;
                Session["Case_ID"] = szCaseID;
                Session["Archived"] = "0";
                Session["QStrCID"] = szCaseID;
                Session["SelectedID"] = szCaseID;
                Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["SN"] = "0";
                Session["LastAction"] = "vb_CaseInformation.aspx";
                szURL = "Document Manager/case/vb_CaseInformation.aspx";
                //    Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");
            }
            else if (e.CommandName.ToString() == "Calender Event")
            {

                LinkButton lnkPatient = (LinkButton)e.Item.Cells[0].FindControl("lnkSelectCase");
                Session["SZ_CASE_ID"] = lnkPatient.CommandArgument;
                Session["PROVIDERNAME"] = e.CommandArgument;

                Response.Redirect("Bill_Sys_ScheduleEvent.aspx", false);
                //Response.Redirect("Bill_Sys_CalendarEvent.aspx", false);
                //    string szTemplateURL="Bill_Sys_TemplateManager.aspx";
                //string szTemplateURL = "TemplateManager/Bill_Sys_GeneratePDF.aspx";
                //Session["PassedCaseID"] = e.CommandArgument;
                //Response.Write("<script language='javascript'>window.open('" + szTemplateURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
            }
            else if (e.CommandName.ToString() == "Out Calender Event")
            {

                LinkButton lnkPatient = (LinkButton)e.Item.Cells[0].FindControl("lnkOutScheduleCalendarEvent");
                Session["SZ_CASE_ID"] = lnkPatient.CommandArgument;
                Session["PROVIDERNAME"] = e.CommandArgument;
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Response.Redirect("Bill_Sys_AppointPatientEntry.aspx?Flag=true", false);
                //Response.Redirect("Bill_Sys_CalendarEvent.aspx", false);
                //    string szTemplateURL="Bill_Sys_TemplateManager.aspx";
                //string szTemplateURL = "TemplateManager/Bill_Sys_GeneratePDF.aspx";
                //Session["PassedCaseID"] = e.CommandArgument;
                //Response.Write("<script language='javascript'>window.open('" + szTemplateURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
            }

            else if (e.CommandName.ToString() == "Template Manager")
            {
                Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                //       Session["PROVIDERNAME"] = e.Item.Cells[4].Text;
                //       Response.Redirect("TemplateManager/templates.aspx", false);

                String szURL = "";
                szURL = "TemplateManager/templates.aspx";


                //      Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");

                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");

                //    string szTemplateURL="Bill_Sys_TemplateManager.aspx";
                //string szTemplateURL = "TemplateManager/Bill_Sys_GeneratePDF.aspx";
                //Session["PassedCaseID"] = e.CommandArgument;
                //Response.Write("<script language='javascript'>window.open('" + szTemplateURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
            }
            //else if (e.CommandName.ToString() == "PatientHistory")
            //{
            //    String szURL = "";
            //    Session["PassedCaseID"] = e.CommandArgument;
            //    szURL = "PatientHistory.aspx";
            //    Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");
            //}
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.InnerException.StackTrace.ToString());
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
    protected void grdCaseMaster_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdCaseMaster.CurrentPageIndex = e.NewPageIndex;

        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.InnerException.StackTrace.ToString());
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
    protected void grdCaseMaster_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {

            }
            else
            {
                e.Item.Cells[0].Visible = false;
            }
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.InnerException.StackTrace.ToString());
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
    protected void btnUnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _caseDetailsBO = new CaseDetailsBO();
        try
        {
           
            foreach (DataGridItem grdItem in grdCaseMaster.Items)
            {
                CheckBox chkDelete = (CheckBox)grdItem.FindControl("chkHardDelete");
                if (chkDelete.Checked)
                {
                    LinkButton lnk = (LinkButton)grdItem.FindControl("lnkSelectCase");
                    _caseDetailsBO.UnDeleteCase(lnk.Text, txtCompanyID.Text);
                    
                }
            }
           
            BindGrid();
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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
