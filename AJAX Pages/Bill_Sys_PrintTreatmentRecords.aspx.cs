/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_SearchCase.aspx.cs
/*Purpose              :       To Search Case 
/*Author               :       Manoj c
/*Date of creation     :       15 Dec 2008  
/*Modified By          :
/*Modified Date        :
/************************************************************/

using AjaxControlToolkit;
using DevExpress.Web;
using ExtendedDropDownList;
using log4net;
using Reminders;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XGridPagination;
using XGridSearchTextBox;
using XGridView;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf.draw;

public partial class Bill_Sys_PatientList : PageBase
{
    private SqlCommand sqlCmd;
    private SqlConnection sqlCon;
    private SqlDataAdapter sqlda;
    private string strConn;
    private SqlDataReader dr;
    private DataSet ds;
    private DataTable dt;
    private static ILog log = LogManager.GetLogger("Bill_Sys_PatientList");
    private string szExcelFileNamePrefix;

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.clearcontrol();
            this.setDefault();
            this.grdPatientList.XGridBindSearch();
            this.SoftDelete();
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.ModalPopupPatientView.Hide();
    }

    //protected void btnExportToExcel_Click(object sender, EventArgs e)
    //{
    //    string str = "";
    //    string str2 = "";
    //    CaseDetailsBO sbo = new CaseDetailsBO();
    //    for (int i = 0; i < this.grdPatientList.Rows.Count; i++)
    //    {
    //        CheckBox box = (CheckBox)this.grdPatientList.Rows[i].FindControl("chkDelete");
    //        if (box.Checked)
    //        {
    //            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
    //            {
    //                LinkButton button = (LinkButton)this.grdPatientList.Rows[i].Cells[3].FindControl("lnkSelectRCase");
    //                str = button.Text.ToString();
    //                if (str2 == "")
    //                {
    //                    str2 = str;
    //                }
    //                else
    //                {
    //                    str2 = str2 + "," + str;
    //                }
    //            }
    //            else
    //            {
    //                LinkButton button2 = (LinkButton)this.grdPatientList.Rows[i].Cells[2].FindControl("lnkSelectCase");
    //                str = button2.Text.ToString();
    //                if (str2 == "")
    //                {
    //                    str2 = str;
    //                }
    //                else
    //                {
    //                    str2 = str2 + "," + str;
    //                }
    //            }
    //        }
    //    }
    //    if (str2 != "")
    //    {
    //        DataSet billInfo = new DataSet();
    //        billInfo = sbo.GetBillInfo(str2, this.txtCompanyID.Text);
    //        if (billInfo.Tables[0].Rows.Count != 0)
    //        {
    //            string str3 = ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString();
    //            string str4 = this.lfnFileName() + ".xls";
    //            File.Copy(ConfigurationSettings.AppSettings["ExportToExcelPath"].ToString(), (str3 + str4).Trim());
    //            new XGridViewControl().GenerateXL(billInfo.Tables[0], str3 + str4);
    //            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href =' " + (ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + str4) + "'", true);
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "ss", "alert('No Bills are available to Export to Excel sheet!');", true);
    //        }
    //    }
    //}

    protected void btnQuickSearch_Click(object sender, EventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtNORec.Text = "";
            this.txtViewLast.Text = "";
            this.fillcontrol();
            this.grdPatientList.XGridBindSearch();
            this.clearcontrol();
            this.SoftDelete();
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

    public void clearcontrol()
    {
        this.utxtDateofAccident.Text = "";
        this.utxtClaimNumber.Text = "";
        this.utxtDateofBirth.Text = "";
        this.utxtCaseType.Text = "";
        this.utxtCaseStatus.Text = "";
        this.utxtPatientName.Text = "";
        this.utxtInsuranceName.Text = "";
        this.utxtLocation.Text = "";
        this.utxtSSNNo.Text = "";
        this.utxtChartNo.Text = "";
        this.utxtCaseNo.Text = "";
    }

    protected void extddlInsurance_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.extddlInsurance.Text != "NA")
        {
            this.txtInsuranceCompany.Text = this.extddlInsurance.Selected_Text;
        }
        else
        {
            this.txtInsuranceCompany.Text = "";
        }
    }

    protected void extddlPatient_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        CaseDetailsBO sbo = new CaseDetailsBO();
        if (this.extddlPatient.Text != "NA")
        {
            if (this.chkJmpCaseDetails.Checked)
            {
                string caseIdByPatientID = sbo.GetCaseIdByPatientID(this.txtCompanyID.Text, this.extddlPatient.Text);
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    base.Response.Redirect("../Bill_Sys_ReCaseDetails.aspx?CaseID=" + caseIdByPatientID + "&cmp=" + this.txtCompanyID.Text + "", false);
                }
                else
                {
                    base.Response.Redirect("../Bill_Sys_CaseDetails.aspx?CaseID=" + caseIdByPatientID + "&cmp=" + this.txtCompanyID.Text + "", false);
                }
            }
            this.txtPatientName.Text = this.extddlPatient.Selected_Text;
        }
        else
        {
            this.txtPatientName.Text = "";
        }
    }

    public void fillcontrol()
    {
        log.Debug("Start FillControl");
        this.utxtCompanyID.Text = this.txtCompanyID.Text;
        this.utxtDateofAccident.Text = this.txtDateofAccident.Text;
        this.utxtClaimNumber.Text = this.txtClaimNumber.Text;
        this.utxtDateofBirth.Text = this.txtDateofBirth.Text;
        this.utxtCaseType.Text = this.extddlCaseType.Text;
        this.utxtCaseStatus.Text = this.extddlCaseStatus.Text;
        if ((this.extddlPatient.Text == "") || (this.extddlPatient.Text == "NA"))
        {
            this.utxtPatientName.Text = this.txtPatientName.Text;
        }
        else
        {
            this.utxtPatientName.Text = this.txtPatientName.Text;
        }
        this.utxtInsuranceName.Text = this.txtInsuranceCompany.Text;
        this.utxtLocation.Text = this.extddlLocation.Text;
        this.utxtSSNNo.Text = this.txtSSNNo.Text;
        this.utxtCaseNo.Text = this.txtCaseNo.Text;
        this.utxtChartNo.Text = this.txtChartNo.Text;
        log.Debug("End FillControl");
    }

    public DataSet GetPatienView(string caseID, string companyID)
    {
        this.sqlCon = new SqlConnection(this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("SP_PATIENTPOP_VIEW", this.sqlCon);
            this.sqlCmd.CommandType = CommandType.StoredProcedure;
            this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            this.sqlda = new SqlDataAdapter(this.sqlCmd);
            this.ds = new DataSet();
            this.sqlda.Fill(this.ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    protected void grdPatientList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Patient"))
        {
            string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
            string caseID = strArray[0].ToString();
            string companyID = strArray[1].ToString();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1")
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ss", "spanhide();", true);
            }
            DataSet patienView = this.GetPatienView(caseID, companyID);
            this.PatientDtlView.DataSource = patienView.Tables[0];
            this.PatientDtlView.DataBind();
            this.ModalPopupPatientView.Show();
        }
    }

    private string lfnFileName()
    {
        Random random = new Random();
        DateTime now = DateTime.Now;
        if (this.szExcelFileNamePrefix == null)
        {
            this.szExcelFileNamePrefix = "excel";
        }
        return (this.szExcelFileNamePrefix + "_" + random.Next(1, 0x2710).ToString() + "_" + now.ToString("yyyyMMddHHmmssms"));
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdPatientList.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        log.Debug("Search case page_load");
        string script = "";
        this.utxtUserId.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        script = "<script language=JavaScript> function autoComplete (field, select, property, forcematch) {var found = false;for (var i = 0; i < select.options.length; i++) {if (select.options[i][property].toUpperCase().indexOf(field.value.toUpperCase()) == 0) {\t\tfound=true; break;}}if (found) { select.selectedIndex = i; }else {select.selectedIndex = -1;}if (field.createTextRange) {if (forcematch && !found) {field.value=field.value.substring(0,field.value.length-1); return;}var cursorKeys ='8;46;37;38;39;40;33;34;35;36;45;';if (cursorKeys.indexOf(event.keyCode+';') == -1) {var r1 = field.createTextRange();var oldValue = r1.text;var newValue = found ? select.options[i][property] : oldValue;if (newValue != field.value) {field.value = newValue;var rNew = field.createTextRange();rNew.moveStart('character', oldValue.length) ;rNew.select();}}}} </script>";
        this.RegisterClientScriptBlock("ClientScript", script);
        this.ajAutoIns.ContextKey = (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        this.ajAutoName.ContextKey = (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        this.btnPrinTreatmentDetails.Attributes.Add("onclick", "return Validate()");
        if (!base.IsPostBack)
        {
            try
            {
                DataSet set = new DataSet();
                set = new Bill_Sys_ProcedureCode_BO().Get_Sys_Key("SS00041", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                {
                    if (set.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        this.Session["SendPatientToDoctor"] = false;
                    }
                    else
                    {
                        this.Session["SendPatientToDoctor"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Session["SendPatientToDoctor"] = false;
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
        }
        StringBuilder builder = new StringBuilder();
        StringBuilder builder2 = new StringBuilder();
        builder.Append("Case #,");
        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
        {
            builder2.Append("SZ_RECASE_NO,");
            this.grdPatientList.Columns[19].Visible = true;
        }
        else
        {
            builder2.Append("SZ_CASE_NO,");
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
        {
            builder.Append("Chart No,");
            builder2.Append("SZ_CHART_NO,");
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1")
        {
            this.extddlLocation.Visible = false;
            this.lblLocation.Visible = false;
        }
        else
        {
            this.extddlLocation.Visible = true;
            this.lblLocation.Visible = true;
        }
        builder.Append("Patient Name,Accident Date,Open Date,Insurance Name,Claim Number,Policy Number,Case Type,Case Status");
        builder2.Append("SZ_PATIENT_NAME,DT_DATE_OF_ACCIDENT,DT_DATE_OPEN,SZ_INSURANCE_NAME,SZ_CLAIM_NUMBER,SZ_POLICY_NUMBER,SZ_CASE_TYPE,SZ_STATUS_NAME");
        if (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower().Equals("admin"))
        {
            builder.Append(",Total,Paid,Pending");
            builder2.Append(",Total,Paid,Pending");
        }
        else
        {
            builder.Append(",Patient Phone");
            builder2.Append(",SZ_PATIENT_PHONE");
        }
        Bill_Sys_LoginBO nbo = new Bill_Sys_LoginBO();
        string str3 = nbo.getconfiguration(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if ((str3 != "") && (str3 != null))
        {
            this.grdPatientList.Columns[21].Visible = true;
            this.grdPatientList.Columns[22].Visible = true;
            builder.Append(",Case Type");
            builder2.Append(",SZ_CASE_TYPE");
            builder.Append(",Case Status");
            builder2.Append(",SZ_STATUS_NAME");
        }
        else
        {
            this.grdPatientList.Columns[21].Visible = true;
            this.grdPatientList.Columns[22].Visible = true;
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION == "1")
        {
            this.grdPatientList.Columns[23].Visible = true;
            builder.Append(",Patient ID");
            builder2.Append(",SZ_PATIENT_ID_LHR");
        }
        else
        {
            this.grdPatientList.Columns[23].Visible = false;
        }
        this.grdPatientList.ExportToExcelColumnNames = (builder.ToString());
        this.grdPatientList.ExportToExcelFields = (builder2.ToString());
        log.Debug("start Xgridbind.");
        this.con.SourceGrid = (this.grdPatientList);
        this.txtSearchBox.SourceGrid = (this.grdPatientList);
        this.grdPatientList.Page = (this.Page);
        this.grdPatientList.PageNumberList = (this.con);
        log.Debug("End Xgridbind.");
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
        {
            this.grdPatientList.Columns[4].Visible = true;
            this.txtChartNo.Visible = true;
            this.lblChart.Visible = true;
        }
        else
        {
            this.grdPatientList.Columns[4].Visible = false;
            this.txtChartNo.Visible = false;
            this.lblChart.Visible = false;
        }
        if (!base.IsPostBack)
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION != "1")
            {
                this.lblpatientid.Visible = false;
                this.txtpatientid.Visible = false;
            }
            else
            {
                this.lblpatientid.Visible = true;
                this.txtpatientid.Visible = true;
            }
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlInsurance.Visible = false;
            this.extddlPatient.Visible = false;
            this.fillcontrol();
            this.extddlCaseStatus.Flag_ID = (this.txtCompanyID.Text.ToString());
            this.extddlCaseType.Flag_ID = (this.txtCompanyID.Text.ToString());
            this.extddlLocation.Flag_ID = (this.txtCompanyID.Text.ToString());
            string caseSatusId = new CaseDetailsBO().GetCaseSatusId(this.txtCompanyID.Text);
            this.extddlCaseStatus.Text = (caseSatusId);
            this.fillcontrol();
            if (this.Session["CASE_LIST_GO_BUTTON"] != null)
            {
                this.utxtPatientName.Text = this.Session["CASE_LIST_GO_BUTTON"].ToString();
                this.Session["CASE_LIST_GO_BUTTON"] = null;
            }
            if (!base.IsPostBack)
            {
                DataSet userPreferences = new DataSet();
                userPreferences = new UserPreferences().GetUserPreferences(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, "SearchCase");
                if (userPreferences.Tables.Count > 0)
                {
                    if (userPreferences.Tables[0].Rows.Count > 0)
                    {
                        if (userPreferences.Tables[0].Rows[0]["sz_preferences"].ToString() != "")
                        {
                            string[] strArray = userPreferences.Tables[0].Rows[0]["sz_preferences"].ToString().Split(new char[] { ',' });
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                string[] strArray2 = strArray[i].ToString().Split(new char[] { '=' });
                                try
                                {
                                    if ((strArray2[0].ToString() == "DO_NOT_LOAD_PATIENT_LIST") && (strArray2[1].ToString() == "true"))
                                    {
                                        this.txtNORec.Text = "1";
                                        this.txtViewLast.Text = "";
                                        this.con.SourceGrid = (this.grdPatientList);
                                        this.txtSearchBox.SourceGrid = (this.grdPatientList);
                                        this.grdPatientList.Page = (this.Page);
                                        this.grdPatientList.PageNumberList = (this.con);
                                        this.grdPatientList.XGridBindSearch();
                                        break;
                                    }
                                    if ((strArray2[0].ToString() == "SHOW_ONLY_LAST") && (strArray2[1].ToString() != "00"))
                                    {
                                        this.txtNORec.Text = "";
                                        this.txtViewLast.Text = "";
                                        this.con.SourceGrid = (this.grdPatientList);
                                        this.txtSearchBox.SourceGrid = (this.grdPatientList);
                                        this.grdPatientList.Page = (this.Page);
                                        this.grdPatientList.PageNumberList = (this.con);
                                        this.grdPatientList.PageRowCount = (Convert.ToInt32(strArray2[1].ToString()));
                                        if (strArray2[1].ToString() == "00")
                                        {
                                            this.grdPatientList.PageRowCount = (50);
                                        }
                                        this.grdPatientList.XGridBind();
                                        break;
                                    }
                                    if ((strArray2[0].ToString() == "SHOW_ONLY_LAST40VIEWED") && (strArray2[1].ToString() != "00"))
                                    {
                                        this.txtNORec.Text = "";
                                        this.txtViewLast.Text = "1";
                                        this.con.SourceGrid = (this.grdPatientList);
                                        this.txtSearchBox.SourceGrid = this.grdPatientList;
                                        this.grdPatientList.Page = this.Page;
                                        this.grdPatientList.PageNumberList = this.con;
                                        this.grdPatientList.PageRowCount = (Convert.ToInt32(strArray2[1].ToString()));
                                        if (strArray2[1].ToString() == "00")
                                        {
                                            this.grdPatientList.PageRowCount = (50);
                                        }
                                        this.grdPatientList.XGridBind();
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.txtNORec.Text = "";
                                    this.con.SourceGrid = (this.grdPatientList);
                                    this.txtSearchBox.SourceGrid = (this.grdPatientList);
                                    this.grdPatientList.Page = (this.Page);
                                    this.grdPatientList.PageNumberList = (this.con);
                                    this.grdPatientList.XGridBindSearch();
                                }
                            }
                        }
                        else
                        {
                            this.txtNORec.Text = "";
                            this.con.SourceGrid = (this.grdPatientList);
                            this.txtSearchBox.SourceGrid = (this.grdPatientList);
                            this.grdPatientList.Page = (this.Page);
                            this.grdPatientList.PageNumberList = (this.con);
                            this.grdPatientList.XGridBindSearch();
                        }
                    }
                    else
                    {
                        this.txtNORec.Text = "";
                        this.con.SourceGrid = (this.grdPatientList);
                        this.txtSearchBox.SourceGrid = (this.grdPatientList);
                        this.grdPatientList.Page = (this.Page);
                        this.grdPatientList.PageNumberList = (this.con);
                        this.grdPatientList.XGridBindSearch();
                    }
                }
                else
                {
                    this.txtNORec.Text = "";
                    this.con.SourceGrid = (this.grdPatientList);
                    this.txtSearchBox.SourceGrid = (this.grdPatientList);
                    this.grdPatientList.Page = (this.Page);
                    this.grdPatientList.PageNumberList = (this.con);
                    this.grdPatientList.XGridBindSearch();
                }
            }
            log.Debug("Page_Load grdPatientList.XGridBindSearch() Completed");
            this.clearcontrol();
        }
        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
        {
            this.grdPatientList.Columns[3].Visible = true;
            this.grdPatientList.Columns[16].Visible = false;
            this.grdPatientList.Columns[17].Visible = false;
        }
        else
        {
            this.grdPatientList.Columns[17].Visible = false;
            this.grdPatientList.Columns[18].Visible = false;
            this.grdPatientList.Columns[2].Visible = true;
            this.grdPatientList.Columns[15].Visible = false;
            this.grdPatientList.Columns[16].Visible = true;
        }
        string str5 = nbo.getconfigurationlocation(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if ((str5 != "") && (str5 != null))
        {
            this.grdPatientList.Columns[16].Visible = false;
        }
        this.grdPatientList.Columns[6].Visible = false;
        this.grdPatientList.Columns[12].Visible = false;
        this.grdPatientList.Columns[13].Visible = false;
        this.grdPatientList.Columns[14].Visible = false;
        this.grdPatientList.Columns[16].Visible = false;
        this.grdPatientList.Columns[17].Visible = false;
        //if (((base.Request.QueryString["Type"] != null) && (base.Request.QueryString["Type"].ToString() == "Quick")) && ((((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_VIEW_BILL) == "True") || (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_VIEW_BILL == "True"))
        //{
        //    this.grdPatientList.Columns[15].Visible = true;
        //    for (int j = 0; j < this.grdPatientList.Rows.Count; j++)
        //    {
        //        HyperLink link = (HyperLink)this.grdPatientList.Rows[j].Cells[15].FindControl("lnkNew");
        //        HyperLink link2 = (HyperLink)this.grdPatientList.Rows[j].Cells[15].FindControl("lnkView");
        //        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_NEW_BILL == "True")
        //        {
        //            link.Visible = true;
        //        }
        //        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_VIEW_BILL == "True")
        //        {
        //            link2.Visible = true;
        //        }
        //    }
        //}
        this.fillcontrol();
        if (!base.IsPostBack && (this.Session["REMINDER"] != null))
        {
            this.Session["REMINDER"] = null;
            ReminderBO rbo = null;
            DataSet set4 = null;
            DataSet set5 = null;
            string str7 = "";
            rbo = new ReminderBO();
            set4 = new DataSet();
            str7 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            DateTime time = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            set4 = rbo.LoadReminderDetails(str7, time);
            set5 = rbo.LoadReminderDetailsforAdd(str7, time, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (((set4.Tables[0].Rows.Count > 0) || (set4.Tables[1].Rows.Count > 0)) || (set5.Tables[0].Rows.Count > 0))
            {
                this.Page.RegisterStartupScript("ss", "<script language='javascript'> ReminderPopup();</script>");
            }
        }

        if (txtStartDate.Text == string.Empty)
        {
            txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }
        if (txtEndDate.Text == string.Empty)
        {
            txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.SoftDelete();
    }

    public void setDefault()
    {
    }

    public void SoftDelete()
    {
        log.Debug("Start SoftDelete()");
        for (int i = 0; i < this.grdPatientList.Rows.Count; i++)
        {
            if (this.grdPatientList.DataKeys[i]["BT_DELETE"].ToString() == "True")
            {
                for (int j = 0; j < this.grdPatientList.Rows[i].Cells.Count; j++)
                {
                    this.grdPatientList.Rows[i].Cells[j].Style.Add("text-decoration", "line-through");
                }
            }
        }
        log.Debug("End SoftDelete()");
    }

    protected void btnPrinTreatmentDetails_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {
                int iDifference = (Convert.ToDateTime(txtEndDate.Text).Subtract(Convert.ToDateTime(txtStartDate.Text))).Days;
                iDifference = iDifference + 1;
                if (iDifference >= 1)
                {
                    if (iDifference > 31)
                    {
                        this.usrMessage.SetMessageType(0);
                        this.usrMessage.PutMessage("Please select date less than or equals to 31 days.");
                        this.usrMessage.Show();
                    }
                    else
                    {
                        string OutputFilePath = "";
                        string OpenPath = "";
                        OpenPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + "/Packet Document/";

                        string BasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
                        BasePath = BasePath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + "/Packet Document/";
                        if (!Directory.Exists(BasePath))
                        {
                            Directory.CreateDirectory(BasePath);
                        }

                        string newPdfFilename = "";
                        newPdfFilename = "TreatmentRecords_" + getFileName();
                        OutputFilePath = BasePath + newPdfFilename;
                        OpenPath = OpenPath + newPdfFilename;

                        string FontStyle = "Arial";
                        int MidHeadtSize = 9;
                        int SubTitle = 6;
                        string patientIdList = string.Empty;
                        for (int j = 0; j < grdPatientList.Rows.Count; j++)
                        {
                            CheckBox chkDelete = (CheckBox)grdPatientList.Rows[j].FindControl("chkDelete");
                            if (chkDelete.Checked == true)
                            {
                                //ds.Tables[0].Rows[index]["Name"].ToString();
                                string patientId = grdPatientList.DataKeys[j]["SZ_PATIENT_ID"].ToString();

                                if (patientIdList == "")
                                {
                                    patientIdList = "'" + patientId + "'";
                                }
                                else
                                {
                                    patientIdList = patientIdList + "," + "'" + patientId + "'";
                                }
                            }
                        }
                        DataSet dsPatient = GetPatienDetails(patientIdList, this.txtCompanyID.Text);

                        if (dsPatient != null)
                        {
                            MemoryStream m = new MemoryStream();

                            FileStream fs = new FileStream(OutputFilePath, System.IO.FileMode.OpenOrCreate);
                            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 25, 25, 18, 18);
                            PdfWriter writer = PdfWriter.GetInstance(document, fs);
                            document.Open();

                            if (dsPatient.Tables != null)
                            {
                                if (dsPatient.Tables.Count > 0)
                                {
                                    if (dsPatient.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dsPatient.Tables[0].Rows.Count; k++)
                                        {
                                            //create pdf

                                            float[] widthbase = { 4f };
                                            PdfPTable tblBase = new PdfPTable(widthbase);
                                            //tblBase.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                                            //tblBase.TotalWidth = 100;
                                            tblBase.WidthPercentage = 100;
                                            tblBase.TotalWidth = 700;

                                            tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                            tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                                            tblBase.DefaultCell.Colspan = 1;

                                            float[] width = { 4f };
                                            PdfPTable heading = new PdfPTable(width);
                                            heading.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                                            heading.DefaultCell.Border = Rectangle.NO_BORDER;
                                            heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                            heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                            heading.DefaultCell.Colspan = 6;
                                            heading.AddCell(new Phrase("Treatment Record", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                                            heading.AddCell(new Phrase(""));
                                            heading.DefaultCell.Colspan = 1;
                                            heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                            heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;


                                            float[] fPatientInfo = { 1f, 1f, 1f, 1.2f, 0.8f };
                                            PdfPTable TPatientInfo = new PdfPTable(fPatientInfo);
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.DefaultCell.FixedHeight = 14f;
                                            TPatientInfo.WidthPercentage = 100;

                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                                            TPatientInfo.AddCell(new Phrase(dsPatient.Tables[0].Rows[k]["SZ_PATIENT_LAST_NAME"].ToString(), iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.AddCell(new Phrase(dsPatient.Tables[0].Rows[k]["SZ_PATIENT_FIRST_NAME"].ToString(), iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                                            TPatientInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell(new Phrase("No Fault", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                            string sStateCity = dsPatient.Tables[0].Rows[k]["SZ_PATIENT_STATE"].ToString() + "," + dsPatient.Tables[0].Rows[k]["SZ_PATIENT_CITY"].ToString() + "," + dsPatient.Tables[0].Rows[k]["SZ_PATIENT_ZIP"].ToString();

                                            TPatientInfo.DefaultCell.FixedHeight = 14f;
                                            TPatientInfo.AddCell("");
                                            TPatientInfo.AddCell(new Phrase("Last Name", iTextSharp.text.FontFactory.GetFont(FontStyle, SubTitle, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.AddCell(new Phrase("First Name", iTextSharp.text.FontFactory.GetFont(FontStyle, SubTitle, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                                            TPatientInfo.AddCell("");
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell("");
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell("");
                                            TPatientInfo.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.DefaultCell.FixedHeight = 14f;

                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                                            TPatientInfo.AddCell(new Phrase(dsPatient.Tables[0].Rows[k]["SZ_PATIENT_ADDRESS"].ToString(), iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.AddCell(new Phrase(dsPatient.Tables[0].Rows[k]["SZ_PATIENT_STREET"].ToString(), iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                                            TPatientInfo.AddCell(new Phrase(sStateCity, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell(new Phrase("D/A:" + dsPatient.Tables[0].Rows[k]["DT_DATE_OF_ACCIDENT"].ToString(), iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

                                            TPatientInfo.DefaultCell.FixedHeight = 14f;
                                            iTextSharp.text.Font fntHeader = new Font(iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK));
                                            //float[] fPatientAddress = { 1f, 1f, 1f, 1f };
                                            //PdfPTable TPatientAddress = new PdfPTable(fPatientAddress);
                                            //TPatientAddress.AddCell(new Phrase("Address:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            //TPatientAddress.AddCell("");
                                            //TPatientAddress.AddCell(new Phrase("Street No. Apt. #", fntHeader));

                                            //TPatientAddress.AddCell(new Phrase("State  City Zip", fntHeader));
                                            //TPatientInfo.AddCell(TPatientAddress);
                                            TPatientInfo.AddCell("");
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell(new Phrase("Address: \t \t \t \t \t \t \t \t \t \t \t \t \t Street No. Apt. #", fntHeader));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell(new Phrase("\t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t State \t City \t Zip", fntHeader));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                                            TPatientInfo.AddCell("");
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell(new Phrase("Notes:", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell("");

                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                                            TPatientInfo.AddCell(new Phrase(dsPatient.Tables[0].Rows[k]["SZ_PATIENT_PHONE"].ToString(), iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.AddCell(new Phrase("\t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t" + dsPatient.Tables[0].Rows[k]["SZ_WORK_PHONE"].ToString(), iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                                            TPatientInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                            //TPatientInfo.DefaultCell.FixedHeight = 14f;
                                            iTextSharp.text.Font fntHeaderPhone = new Font(iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK));
                                            TPatientInfo.AddCell("");
                                            TPatientInfo.AddCell(new Phrase("Home Phone Number", fntHeaderPhone));
                                            TPatientInfo.AddCell(new Phrase("\t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t Business Number", fntHeaderPhone));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                                            TPatientInfo.AddCell(new Phrase("", fntHeaderPhone));
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell("");
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell("");
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            TPatientInfo.AddCell("");

                                            TPatientInfo.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                            TPatientInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            //TPatientInfo.DefaultCell.FixedHeight = 14f;


                                            float[] wd1 = { 1.9f, 7.4f, 1.5f, 3.7f };
                                            PdfPTable tblVisit = new PdfPTable(wd1);
                                            tblVisit.WidthPercentage = 100;
                                            tblVisit.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            tblVisit.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                                            tblVisit.AddCell(new Phrase("DATE", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                                            tblVisit.AddCell(new Phrase("TREATMENT", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                                            tblVisit.AddCell(new Phrase("EVAL", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                                            tblVisit.AddCell(new Phrase("SIGNATURE", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));



                                            float[] wdContent = { 0.5f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 1f };

                                            PdfPTable tblContentHeadings = new PdfPTable(wdContent);
                                            tblContentHeadings.WidthPercentage = 100;
                                            //tblContentHeadings.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                                            tblContentHeadings.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                                            tblContentHeadings.AddCell("");
                                            tblContentHeadings.AddCell("CP");
                                            tblContentHeadings.AddCell("HP");
                                            tblContentHeadings.AddCell("US");
                                            tblContentHeadings.AddCell("ES");
                                            tblContentHeadings.AddCell("AE");
                                            tblContentHeadings.AddCell("TE");
                                            tblContentHeadings.AddCell("TM");
                                            tblContentHeadings.AddCell("MR");
                                            tblContentHeadings.AddCell("TX");
                                            tblContentHeadings.AddCell("PB");
                                            tblContentHeadings.AddCell("DC");
                                            tblContentHeadings.AddCell("DR");
                                            tblContentHeadings.AddCell("");

                                            PdfPTable tblContent = new PdfPTable(wdContent);
                                            tblContent.WidthPercentage = 100;
                                            //tblContent.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                                            tblContent.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                                            int remainingDays = iDifference - 20;
                                            PdfPCell cell = null;
                                            if (iDifference < 20)
                                            {
                                                for (int j = 0; j < iDifference; j++)
                                                {
                                                    Paragraph para = new Paragraph(Convert.ToDateTime(txtStartDate.Text).AddDays(j).ToString("MM/dd/yyyy"));
                                                    cell = new PdfPCell(para);
                                                    cell.MinimumHeight = 32;
                                                    tblContent.AddCell(cell);
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    Convert.ToDateTime(txtStartDate.Text).AddDays(1);
                                                }
                                                tblBase.AddCell(heading);
                                                tblBase.AddCell(TPatientInfo);
                                                tblBase.AddCell(tblVisit);
                                                tblBase.AddCell(tblContentHeadings);
                                                tblBase.AddCell(tblContent);
                                                document.NewPage();
                                            }
                                            else
                                            {
                                                for (int j = 0; j < 20; j++)
                                                {
                                                    Paragraph para = new Paragraph(Convert.ToDateTime(txtStartDate.Text).AddDays(j).ToString("MM/dd/yyyy"));
                                                    cell = new PdfPCell(para);
                                                    cell.MinimumHeight = 32;
                                                    tblContent.AddCell(cell);
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    tblContent.AddCell(" ");
                                                    Convert.ToDateTime(txtStartDate.Text).AddDays(1);
                                                }
                                                tblBase.AddCell(heading);
                                                tblBase.AddCell(TPatientInfo);
                                                tblBase.AddCell(tblVisit);
                                                tblBase.AddCell(tblContentHeadings);
                                                tblBase.AddCell(tblContent);
                                            }




                                            PdfPTable tblContent2 = new PdfPTable(wdContent);
                                            tblContent2.WidthPercentage = 100;
                                            //tblContent.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                                            tblContent2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                                            if (iDifference > 20)
                                            {
                                                DateTime dtStartDateNew = new DateTime(2015, 1, 21);
                                                document.NewPage();
                                                PdfPCell pcell = null;
                                                for (int j = 0; j < remainingDays; j++)
                                                {
                                                    Paragraph para = new Paragraph(dtStartDateNew.AddDays(j).ToString("MM/dd/yyyy"));
                                                    pcell = new PdfPCell(para);
                                                    pcell.MinimumHeight = 32;
                                                    tblContent2.AddCell(pcell);
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    tblContent2.AddCell(" ");
                                                    dtStartDateNew.AddDays(1);
                                                }
                                                tblBase.AddCell(heading);
                                                tblBase.AddCell(TPatientInfo);
                                                tblBase.AddCell(tblVisit);
                                                tblBase.AddCell(tblContentHeadings);
                                                tblBase.AddCell(tblContent2);
                                            }
                                            document.Add(tblBase);
                                        }
                                    }
                                }
                            }
                            document.Close();
                        }
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + OpenPath + "');", true);
                    }
                }
                else
                {
                    this.usrMessage.SetMessageType(0);
                    this.usrMessage.PutMessage("End Date should be greater than Start Date.");
                    this.usrMessage.Show();
                }
            }
            else
            {
                this.usrMessage.SetMessageType(0);
                this.usrMessage.PutMessage("Start Date and End Date is required.");
                this.usrMessage.Show();
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

    private string getFileName()
    {
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms") + ".pdf";
        return szFileName;
    }

    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }

    public DataSet GetPatienDetails(string patientIds, string companyId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.sqlCon = new SqlConnection(this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("sp_get_treatment_record_information", this.sqlCon);
            this.sqlCmd.CommandType = CommandType.StoredProcedure;
            this.sqlCmd.Parameters.AddWithValue("@sz_patient_id_list", patientIds);
            this.sqlCmd.Parameters.AddWithValue("@sz_company_id", companyId);
            this.sqlda = new SqlDataAdapter(this.sqlCmd);
            this.ds = new DataSet();
            this.sqlda.Fill(this.ds);
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

       
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}