using AjaxControlToolkit;
using ASP;
using DevExpress.Web;
using DoseSpot.ClientTools.HelperClasses;
using DoseSpotService.DoseSpotAPI;
using ExtendedDropDownList;
using log4net;
using Reminders;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
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

public partial class Bill_Sys_PatientList : PageBase, IRequiresSessionState
{
    

    private SqlCommand sqlCmd;

    private SqlConnection sqlCon;

    private SqlDataAdapter sqlda;

    private string strConn;

    private SqlDataReader dr;

    private DataSet ds;

    private DataTable dt;

    private static ILog log;

    private string szExcelFileNamePrefix;

    private string Key = ConfigurationManager.AppSettings["DosespotKey"].ToString();

    static Bill_Sys_PatientList()
    {
        Bill_Sys_PatientList.log = LogManager.GetLogger("Bill_Sys_PatientList");
    }

    public Bill_Sys_PatientList()
    {
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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

    protected void btnDosespot_Click(object sender, EventArgs e)
    {
        this.GetDosespotUserDetails();
        string text = this.txtClinicId.Text;
        Convert.ToInt32(this.txtDosespotUserId.Text);
        string str = EncryptionCommon.CreatePhrase();
        string text1 = this.txtClinicId.Text;
        string str1 = this.txtDosespotUserId.Text;
        string str2 = "zoJk1Ga8aXgkCrU7vqCvvebory3NafUzZNDhWUetYJ1mKCsrsslkGG7to0Za52c9U44qDX54SLWVo1pRtvX5PLI8ClUddzovPiLi31yVkbzo5YUFk/0x8w";
        string str3 = "24GhIluoPngKqHsOw/XhpUg9g4t4Gbs1KQDHBKqhh41wfXRrvmdeg+DhiRP7FiNcPovq3aZuZKruMo8LnC+cYQ";
        string[] strArrays = new string[] { "http://my.staging.dosespot.com/LoginSingleSignOn.aspx?b=2&SingleSignOnClinicId=", text1, "&SingleSignOnUserId=", str1, "&SingleSignOnPhraseLength=", str, "&SingleSignOnCode=", str2, "&SingleSignOnUserIdVerify=", str3, "&RefillsErrors=1" };
        string str4 = string.Concat(strArrays);
        base.Response.Redirect(str4);
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string str = "";
        string str1 = "";
        CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
        for (int i = 0; i < this.grdPatientList.Rows.Count; i++)
        {
            if (((CheckBox)this.grdPatientList.Rows[i].FindControl("chkDelete")).Checked)
            {
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    LinkButton linkButton = (LinkButton)this.grdPatientList.Rows[i].Cells[2].FindControl("lnkSelectCase");
                    str = linkButton.Text.ToString();
                    str1 = (!(str1 == "") ? string.Concat(str1, ",", str) : str);
                }
                else
                {
                    LinkButton linkButton1 = (LinkButton)this.grdPatientList.Rows[i].Cells[3].FindControl("lnkSelectRCase");
                    str = linkButton1.Text.ToString();
                    str1 = (!(str1 == "") ? string.Concat(str1, ",", str) : str);
                }
            }
        }
        if (str1 != "")
        {
            DataSet dataSet = new DataSet();
            dataSet = caseDetailsBO.GetBillInfo(str1, this.txtCompanyID.Text);
            if (dataSet.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, base.GetType(), "ss", "alert('No Bills are available to Export to Excel sheet!');", true);
            }
            else
            {
                string str2 = ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString();
                string str3 = string.Concat(this.lfnFileName(), ".xls");
                File.Copy(ConfigurationSettings.AppSettings["ExportToExcelPath"].ToString(), string.Concat(str2, str3).Trim());
                (new XGridViewControl()).GenerateXL(dataSet.Tables[0], string.Concat(str2, str3));
                ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat(" window.location.href =' ", ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString(), str3, "'"), true);
            }
        }
    }

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSoftDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
        string str = "";
        try
        {
            for (int i = 0; i < this.grdPatientList.Rows.Count; i++)
            {
                if (((CheckBox)this.grdPatientList.Rows[i].FindControl("chkDelete")).Checked)
                {
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        LinkButton linkButton = (LinkButton)this.grdPatientList.Rows[i].Cells[2].FindControl("lnkSelectCase");
                        str = linkButton.Text.ToString();
                    }
                    else
                    {
                        LinkButton linkButton1 = (LinkButton)this.grdPatientList.Rows[i].Cells[3].FindControl("lnkSelectRCase");
                        str = linkButton1.Text.ToString();
                    }
                    caseDetailsBO.SoftDelete(str, this.txtCompanyID.Text, true);
                }
            }
            this.fillcontrol();
            this.grdPatientList.XGridBindSearch();
            Bill_Sys_PatientList.log.Debug("grdPatientList.XGridBindSearch() Completed.");
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
        if (!(this.extddlInsurance.Text != "NA"))
        {
            this.txtInsuranceCompany.Text = "";
        }
        else
        {
            this.txtInsuranceCompany.Text = this.extddlInsurance.Selected_Text;
        }
    }

    protected void extddlPatient_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
        if (!(this.extddlPatient.Text != "NA"))
        {
            this.txtPatientName.Text = "";
        }
        else
        {
            if (this.chkJmpCaseDetails.Checked)
            {
                string caseIdByPatientID = caseDetailsBO.GetCaseIdByPatientID(this.txtCompanyID.Text, this.extddlPatient.Text);
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    base.Response.Redirect(string.Concat("../Bill_Sys_CaseDetails.aspx?CaseID=", caseIdByPatientID, "&cmp=", this.txtCompanyID.Text), false);
                }
                else
                {
                    base.Response.Redirect(string.Concat("../Bill_Sys_ReCaseDetails.aspx?CaseID=", caseIdByPatientID, "&cmp=", this.txtCompanyID.Text), false);
                }
            }
            this.txtPatientName.Text = this.extddlPatient.Selected_Text;
        }
    }

    public void fillcontrol()
    {
        Bill_Sys_PatientList.log.Debug("Start FillControl");
        this.utxtCompanyID.Text = this.txtCompanyID.Text;
        this.utxtDateofAccident.Text = this.txtDateofAccident.Text;
        this.utxtClaimNumber.Text = this.txtClaimNumber.Text;
        this.utxtDateofBirth.Text = this.txtDateofBirth.Text;
        this.utxtCaseType.Text = this.extddlCaseType.Text;
        this.utxtCaseStatus.Text = this.extddlCaseStatus.Text;
        if ((this.extddlPatient.Text == "" ? false : !(this.extddlPatient.Text == "NA")))
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
        Bill_Sys_PatientList.log.Debug("End FillControl");
    }

    private int GetDosespotUserDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int num = 0;
        string str = "";
        string str1 = "";
        try
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            SqlCommand sqlCommand = new SqlCommand("SP_DOSESPOT_USER_DETAILS", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlCommand.Parameters.Add("@SZ_COMPANY_ID", this.txtCompanyID.Text);
            sqlCommand.Parameters.Add("@SZ_USER_ID", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            sqlCommand.Parameters.Add("@FLAG", "LIST");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            if (dataSet != null)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    str = dataSet.Tables[0].Rows[0]["ClinicId"].ToString();
                    str1 = dataSet.Tables[0].Rows[0]["UserId"].ToString();
                    num = 1;
                }
            }
            this.txtClinicId.Text = str;
            this.txtDosespotUserId.Text = str1;
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
       
        return num;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetPatienView(string caseID, string companyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = ConfigurationManager.AppSettings["Connection_String"].ToString();
        string str1 = str;
        this.strConn = str;
        this.sqlCon = new SqlConnection(str1);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_PATIENTPOP_VIEW", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
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

    protected void grdPatientList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Patient"))
        {
            string[] strArrays = e.CommandArgument.ToString().Split(new char[] { ',' });
            string str = strArrays[0].ToString();
            string str1 = strArrays[1].ToString();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "ss", "spanhide();", true);
            }
            DataSet patienView = this.GetPatienView(str, str1);
            this.PatientDtlView.DataSource = patienView.Tables[0];
            this.PatientDtlView.DataBind();
            this.ModalPopupPatientView.Show();
        }
        
    }

    protected void grdPatientList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string phone = this.grdPatientList.DataKeys[e.Row.RowIndex].Values[5].ToString();
            HtmlAnchor imgsms = (HtmlAnchor)e.Row.FindControl("hrefsms");
            if (phone == "" || phone == null || phone == "&nbsp;")
            {
                imgsms.Visible = false;
            }
            else {

                imgsms.Attributes.Add("onclick", "sendreminderframe("+ phone.Replace("(","").Replace(")","").Replace(" ","").Replace("-","") + ");");
            }

            //if (!((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower().Equals("attorney"))
            //{
            //    HtmlAnchor htmlAnchor = (HtmlAnchor)e.Row.FindControl("lnkCase");
            //    if (htmlAnchor != null)
            //    {
            //        string str = this.grdPatientList.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //        string sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //        string[] strArrays = new string[] { "../AJAX Pages/Bill_Sys_CaseDetails.aspx?CaseID=", str, "&cmp=", sZCOMPANYID, "'" };
            //        htmlAnchor.HRef = string.Concat(strArrays);
            //    }
            //}
            //else
            //{
            //    HtmlAnchor htmlAnchor1 = (HtmlAnchor)e.Row.FindControl("lnkCase");
            //    if (htmlAnchor1 != null)
            //    {
            //        string str1 = this.grdPatientList.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //        string sZCOMPANYID1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //        string str2 = "Pas5pr@se";
            //        string str3 = "s@1tValue";
            //        string str4 = "SHA1";
            //        int num = 2;
            //        string str5 = "@1B2c3D4e5F6g7H8";
            //        int num1 = 256;
            //        string[] strArrays1 = new string[] { "CaseID=", str1, "&cmp=", sZCOMPANYID1, "'" };
            //        string str6 = string.Concat(strArrays1);
            //        string str7 = Bill_Sys_EncryDecry.Encrypt(str6, str2, str3, str4, num, str5, num1);
            //        htmlAnchor1.HRef = string.Concat("../AJAX Pages/atcasedetails.aspx?dt=", str7);
            //        return;
            //    }
            //}


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
        string[] str = new string[] { this.szExcelFileNamePrefix, "_", null, null, null };
        int num = random.Next(1, 10000);
        str[2] = num.ToString();
        str[3] = "_";
        str[4] = now.ToString("yyyyMMddHHmmssms");
        return string.Concat(str);
    }

    protected void lnkDosespotErrors_Click(object sender, EventArgs e)
    {
        this.GetDosespotUserDetails();
        string text = this.txtClinicId.Text;
        Convert.ToInt32(this.txtDosespotUserId.Text);
        EncryptionCommon.CreatePhrase();
        string str = this.txtClinicId.Text;
        string text1 = this.txtDosespotUserId.Text;
        string str1 = "";
        string str2 = "";
        this.SingleSignonCode(str, text1, out str1, out str2, true);
        string[] strArrays = new string[] { "http://my.staging.dosespot.com/LoginSingleSignOn.aspx?b=2&SingleSignOnClinicId=", str, "&SingleSignOnUserId=", text1, "&SingleSignOnPhraseLength=32&SingleSignOnCode=", str1, "&SingleSignOnUserIdVerify=", str2, "&RefillsErrors=0" };
        string str3 = string.Concat(strArrays);
        base.Response.Redirect(str3);
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat(" window.location.href ='", ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString(), this.grdPatientList.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()), "';"), true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int i;
        bool flag;
        HttpCookie loggedout = new HttpCookie("loggedout", "0");
        loggedout.Expires = DateTime.Now.AddYears(1);
        Response.Cookies.Add(loggedout);

        Bill_Sys_PatientList.log.Debug("Search case page_load");
        this.utxtUserId.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        this.RegisterClientScriptBlock("ClientScript", "<script language=JavaScript> function autoComplete (field, select, property, forcematch) {var found = false;for (var i = 0; i < select.options.length; i++) {if (select.options[i][property].toUpperCase().indexOf(field.value.toUpperCase()) == 0) {\t\tfound=true; break;}}if (found) { select.selectedIndex = i; }else {select.selectedIndex = -1;}if (field.createTextRange) {if (forcematch && !found) {field.value=field.value.substring(0,field.value.length-1); return;}var cursorKeys ='8;46;37;38;39;40;33;34;35;36;45;';if (cursorKeys.indexOf(event.keyCode+';') == -1) {var r1 = field.createTextRange();var oldValue = r1.text;var newValue = found ? select.options[i][property] : oldValue;if (newValue != field.value) {field.value = newValue;var rNew = field.createTextRange();rNew.moveStart('character', oldValue.length) ;rNew.select();}}}} </script>");
        this.ajAutoIns.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.ajAutoName.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.btnSoftDelete.Attributes.Add("onclick", "return Validate()");
        this.btnExportToExcel.Attributes.Add("onclick", "return ValidateExportBill()");
        if (!base.IsPostBack)
        {
            try
            {
                DataSet dataSet = new DataSet();
                dataSet = (new Bill_Sys_ProcedureCode_BO()).Get_Sys_Key("SS00041", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0][0].ToString() != "0")
                    {
                        this.Session["SendPatientToDoctor"] = true;
                    }
                    else
                    {
                        this.Session["SendPatientToDoctor"] = false;
                    }
                }
            }
            catch (Exception exception)
            {
                this.Session["SendPatientToDoctor"] = false;
            }
        }
        StringBuilder stringBuilder = new StringBuilder();
        StringBuilder stringBuilder1 = new StringBuilder();
        stringBuilder.Append("Case #,");
        if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
        {
            stringBuilder1.Append("SZ_CASE_NO,");
            this.grdPatientList.Columns[33].Visible = false;
        }
        else
        {
            stringBuilder1.Append("SZ_RECASE_NO,");
            this.grdPatientList.Columns[25].Visible = true;
            this.grdPatientList.Columns[33].Visible = true;
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
        {
            stringBuilder.Append("Chart No,");
            stringBuilder1.Append("SZ_CHART_NO,");
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
        {
            this.extddlLocation.Visible = true;
            this.lblLocation.Visible = true;
        }
        else
        {
            this.extddlLocation.Visible = false;
            this.lblLocation.Visible = false;
        }
        stringBuilder.Append("Patient Name,Accident Date,Open Date,Insurance Name,Claim Number,Policy Number,Case Type,Case Status");
        stringBuilder1.Append("SZ_PATIENT_NAME,DT_DATE_OF_ACCIDENT,DT_DATE_OPEN,SZ_INSURANCE_NAME,SZ_CLAIM_NUMBER,SZ_POLICY_NUMBER,SZ_CASE_TYPE,SZ_STATUS_NAME");
        if (!((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower().Equals("admin"))
        {
            stringBuilder.Append(",Patient Phone");
            stringBuilder1.Append(",SZ_PATIENT_PHONE");
        }
        else
        {
            stringBuilder.Append(",Total,Paid,Pending");
            stringBuilder1.Append(",Total,Paid,Pending");
        }
        Bill_Sys_LoginBO billSysLoginBO = new Bill_Sys_LoginBO();
        string str = billSysLoginBO.getconfiguration(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if (!(str != "") || str == null)
        {
            this.grdPatientList.Columns[27].Visible = false;
            this.grdPatientList.Columns[28].Visible = false;
        }
        else
        {
            this.grdPatientList.Columns[27].Visible = true;
            this.grdPatientList.Columns[28].Visible = true;
            stringBuilder.Append(",Case Type");
            stringBuilder1.Append(",SZ_CASE_TYPE");
            stringBuilder.Append(",Case Status");
            stringBuilder1.Append(",SZ_STATUS_NAME");
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION != "1")
        {
            this.grdPatientList.Columns[29].Visible = false;
        }
        else
        {
            this.grdPatientList.Columns[29].Visible = true;
            stringBuilder.Append(",Patient ID");
            stringBuilder1.Append(",SZ_PATIENT_ID_LHR");
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_DATE_OF_FIRST_TREATMENT != "1")
        {
            this.grdPatientList.Columns[30].Visible = false;
        }
        else
        {
            this.grdPatientList.Columns[30].Visible = true;
            stringBuilder.Append(",Date Of First Treatment");
            stringBuilder1.Append(",DT_FIRST_TREATMENT");
        }
        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
        {
            stringBuilder.Append(",Provider Name");
            stringBuilder1.Append(",Provider_Name");
        }
        this.grdPatientList.ExportToExcelColumnNames = stringBuilder.ToString();
        this.grdPatientList.ExportToExcelFields = stringBuilder1.ToString();
        Bill_Sys_PatientList.log.Debug("start Xgridbind.");
        this.con.SourceGrid = this.grdPatientList;
        this.txtSearchBox.SourceGrid = this.grdPatientList;
        this.grdPatientList.Page = this.Page;
        this.grdPatientList.PageNumberList = this.con;
        Bill_Sys_PatientList.log.Debug("End Xgridbind.");
         if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1")
        {
            this.grdPatientList.Columns[4].Visible = false;
            this.txtChartNo.Visible = false;
            this.lblChart.Visible = false;
        }
        else
        {
            this.grdPatientList.Columns[4].Visible = true;
            this.txtChartNo.Visible = true;
            this.lblChart.Visible = true;
        }
        if (!base.IsPostBack)
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION == "1")
            {
                this.lblpatientid.Visible = true;
                this.txtpatientid.Visible = true;
            }
            else
            {
                this.lblpatientid.Visible = false;
                this.txtpatientid.Visible = false;
            }
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlInsurance.Visible = false;
            this.extddlPatient.Visible = false;
            this.fillcontrol();
            this.extddlCaseStatus.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlCaseType.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlLocation.Flag_ID = this.txtCompanyID.Text.ToString();
            string caseSatusId = (new CaseDetailsBO()).GetCaseSatusId(this.txtCompanyID.Text);
            this.extddlCaseStatus.Text = caseSatusId;
            this.fillcontrol();
            if (this.Session["CASE_LIST_GO_BUTTON"] != null)
            {
                this.utxtPatientName.Text = this.Session["CASE_LIST_GO_BUTTON"].ToString();
                this.Session["CASE_LIST_GO_BUTTON"] = null;
            }
            if (!base.IsPostBack)
            {
                DataSet userPreferences = new DataSet();
                UserPreferences userPreference = new UserPreferences();
                userPreferences = userPreference.GetUserPreferences(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, "SearchCase");
                if (userPreferences.Tables.Count <= 0)
                {
                    this.txtNORec.Text = "";
                    this.con.SourceGrid = this.grdPatientList;
                    this.txtSearchBox.SourceGrid = this.grdPatientList;
                    this.grdPatientList.Page = this.Page;
                    this.grdPatientList.PageNumberList = this.con;
                    this.grdPatientList.XGridBindSearch();
                }
                else if (userPreferences.Tables[0].Rows.Count <= 0)
                {
                    this.txtNORec.Text = "";
                    this.con.SourceGrid = this.grdPatientList;
                    this.txtSearchBox.SourceGrid = this.grdPatientList;
                    this.grdPatientList.Page = this.Page;
                    this.grdPatientList.PageNumberList = this.con;
                }
                else if (!(userPreferences.Tables[0].Rows[0]["sz_preferences"].ToString() != ""))
                {
                    this.txtNORec.Text = "";
                    this.con.SourceGrid = this.grdPatientList;
                    this.txtSearchBox.SourceGrid = this.grdPatientList;
                    this.grdPatientList.Page = this.Page;
                    this.grdPatientList.PageNumberList = this.con;
                    this.grdPatientList.XGridBindSearch();
                }
                else
                {
                    string str1 = userPreferences.Tables[0].Rows[0]["sz_preferences"].ToString();
                    char[] chrArray = new char[] { ',' };
                    string[] strArrays = str1.Split(chrArray);
                    for (i = 0; i < (int)strArrays.Length; i++)
                    {
                        string str2 = strArrays[i].ToString();
                        chrArray = new char[] { '=' };
                        string[] strArrays1 = str2.Split(chrArray);
                        try
                        {
                            if (!(strArrays1[0].ToString() != "DO_NOT_LOAD_PATIENT_LIST" ? true : !(strArrays1[1].ToString() == "true")))
                            {
                                this.txtNORec.Text = "1";
                                this.txtViewLast.Text = "";
                                this.con.SourceGrid = this.grdPatientList;
                                this.txtSearchBox.SourceGrid = this.grdPatientList;
                                this.grdPatientList.Page = this.Page;
                                this.grdPatientList.PageNumberList = this.con;
                                this.grdPatientList.XGridBindSearch();
                                break;
                            }
                            else if (!(strArrays1[0].ToString() != "SHOW_ONLY_LAST" ? true : !(strArrays1[1].ToString() != "00")))
                            {
                                this.txtNORec.Text = "";
                                this.txtViewLast.Text = "";
                                this.con.SourceGrid = this.grdPatientList;
                                this.txtSearchBox.SourceGrid = this.grdPatientList;
                                this.grdPatientList.Page = this.Page;
                                this.grdPatientList.PageNumberList = this.con;
                                this.grdPatientList.PageRowCount = Convert.ToInt32(strArrays1[1].ToString());
                                if (strArrays1[1].ToString() == "00")
                                {
                                    this.grdPatientList.PageRowCount = 50;
                                }
                                this.grdPatientList.XGridBind();
                                break;
                            }
                            else if ((strArrays1[0].ToString() != "SHOW_ONLY_LAST40VIEWED" ? false : strArrays1[1].ToString() != "00"))
                            {
                                this.txtNORec.Text = "";
                                this.txtViewLast.Text = "1";
                                this.con.SourceGrid = this.grdPatientList;
                                this.txtSearchBox.SourceGrid = this.grdPatientList;
                                this.grdPatientList.Page = this.Page;
                                this.grdPatientList.PageNumberList = this.con;
                                this.grdPatientList.PageRowCount = Convert.ToInt32(strArrays1[1].ToString());
                                if (strArrays1[1].ToString() == "00")
                                {
                                    this.grdPatientList.PageRowCount = 50;
                                }
                                this.grdPatientList.XGridBind();
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.txtNORec.Text = "";
                            this.con.SourceGrid = this.grdPatientList;
                            this.txtSearchBox.SourceGrid = this.grdPatientList;
                            this.grdPatientList.Page = this.Page;
                            this.grdPatientList.PageNumberList = this.con;
                            this.grdPatientList.XGridBindSearch();
                            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                            using (Utils utility = new Utils())
                            {
                                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                            }
                            string Elmahstr2 = "Error Request=" + id + ".Please share with Technical support.";
                            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + Elmahstr2);
                        }
                    }
                }
                //Method End
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
            }
            Bill_Sys_PatientList.log.Debug("Page_Load grdPatientList.XGridBindSearch() Completed");
            int count = this.grdPatientList.Rows.Count;
            this.clearcontrol();
        }
        if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
        {
            this.grdPatientList.Columns[23].Visible = false;
            this.grdPatientList.Columns[24].Visible = false;
            this.grdPatientList.Columns[2].Visible = true;
            this.grdPatientList.Columns[21].Visible = false;
            this.grdPatientList.Columns[22].Visible = true;
        }
        else
        {
            this.grdPatientList.Columns[3].Visible = true;
            this.grdPatientList.Columns[19].Visible = false;
            this.grdPatientList.Columns[20].Visible = false;
        }
        string str3 = billSysLoginBO.getconfigurationlocation(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if ((str3 == "" ? false : str3 != null))
        {
            this.grdPatientList.Columns[22].Visible = false;
        }
        if (!((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower().Equals("admin"))
        {
            this.grdPatientList.Columns[6].Visible = true;
            this.grdPatientList.Columns[12].Visible = false;
            this.grdPatientList.Columns[13].Visible = false;
            this.grdPatientList.Columns[14].Visible = false;
        }
        else
        {
            this.grdPatientList.Columns[12].Visible = true;
            this.grdPatientList.Columns[13].Visible = true;
            this.grdPatientList.Columns[14].Visible = true;
            this.grdPatientList.Columns[6].Visible = false;
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PATIENT_PHONE == "1")
        {
            this.grdPatientList.Columns[6].Visible = true;
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHECKINVALUE == "1")
        {
            this.grdPatientList.Columns[17].Visible = true;
            this.grdPatientList.Columns[18].Visible = true;
        }
        DataSet sysKey = new DataSet();
        sysKey = (new Bill_Sys_ProcedureCode_BO()).Get_Sys_Key("SS00040", this.txtCompanyID.Text);
        if ((sysKey.Tables.Count <= 0 || sysKey.Tables[0].Rows.Count <= 0 ? false : sysKey.Tables[0].Rows[0][0].ToString() == "0"))
        {
            this.grdPatientList.Columns[32].Visible = false;
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SOFT_DELETE == "True")
        {
            this.grdPatientList.Columns[35].Visible = true;
            this.btnSoftDelete.Visible = true;
        }
        if (base.Request.QueryString["Type"] == null || !(base.Request.QueryString["Type"].ToString() == "Quick"))
        {
            flag = true;
        }
        else
        {
            flag = (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_NEW_BILL == "True" ? false : !(((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_VIEW_BILL == "True"));
        }
        if (!flag)
        {
            this.grdPatientList.Columns[15].Visible = true;
            for (i = 0; i < this.grdPatientList.Rows.Count; i++)
            {
                HyperLink hyperLink = (HyperLink)this.grdPatientList.Rows[i].Cells[15].FindControl("lnkNew");
                HyperLink hyperLink1 = (HyperLink)this.grdPatientList.Rows[i].Cells[15].FindControl("lnkView");
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_NEW_BILL== "True")
                {
                    hyperLink.Visible = true;
                }
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_VIEW_BILL == "True")
                {
                    hyperLink1.Visible = true;
                }
            }
        }
        this.fillcontrol();
        if (!base.IsPostBack)
        {
         int dose=this.GetDosespotUserDetails();
            if (dose == 1)
            {
                string text = this.txtClinicId.Text;
                int num = Convert.ToInt32(this.txtDosespotUserId.Text);
                string str4 = EncryptionCommon.CreatePhrase();
                string str5 = EncryptionCommon.CreatePhraseEncryptedCombinedString(str4, text);
                string str6 = EncryptionCommon.EncryptUserId(str4, num, text);
                APISoapClient aPISoapClient = new APISoapClient("APISoap12");
                SingleSignOn singleSignOn = new SingleSignOn()
                {
                    SingleSignOnClinicId = Convert.ToInt32(text),
                    SingleSignOnUserId = num
                };
                str5 = "";
                str6 = "";
                this.SingleSignonCode(this.txtClinicId.Text, num.ToString(), out str5, out str6, false);
                singleSignOn.SingleSignOnCode = str5;
                singleSignOn.SingleSignOnUserIdVerify = str6;
                GetRefillRequestsTransmissionErrorsRequest getRefillRequestsTransmissionErrorsRequest = new GetRefillRequestsTransmissionErrorsRequest();
                RefillRequestsTransmissionErrorsMessageRequest refillRequestsTransmissionErrorsMessageRequest = new RefillRequestsTransmissionErrorsMessageRequest()
                {
                    SingleSignOn = singleSignOn,
                    ClinicianId = num
                };
                getRefillRequestsTransmissionErrorsRequest.GetRefillRequestsTransmissionErrorsMessageRequest = refillRequestsTransmissionErrorsMessageRequest;
                RefillRequestsTransmissionErrorsMessageResult refillRequestsTransmissionErrors = aPISoapClient.GetRefillRequestsTransmissionErrors(refillRequestsTransmissionErrorsMessageRequest);
                int refillRequestsCount = refillRequestsTransmissionErrors.RefillRequestsTransmissionErrors[0].RefillRequestsCount;
                int transactionErrorsCount = refillRequestsTransmissionErrors.RefillRequestsTransmissionErrors[0].TransactionErrorsCount;
                string str7 = string.Concat(transactionErrorsCount.ToString(), " Transmission Error / ", refillRequestsCount.ToString(), " Refill requests");
                this.lnkDosespotErrors.Text = str7;
                this.lnkDosespotErrors.Visible = true;
            }
            else
            {
                this.lnkDosespotErrors.Text = "";
                this.lnkDosespotErrors.Visible = false;
            }

        }
        if ((base.IsPostBack ? false : this.Session["REMINDER"] != null))
        {
            this.Session["REMINDER"] = null;
            ReminderBO reminderBO = null;
            DataSet dataSet1 = null;
            DataSet dataSet2 = null;
            string sZUSERID = "";
            reminderBO = new ReminderBO();
            dataSet1 = new DataSet();
            sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            DateTime dateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            dataSet1 = reminderBO.LoadReminderDetails(sZUSERID, dateTime);
            dataSet2 = reminderBO.LoadReminderDetailsforAdd(sZUSERID, dateTime, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if ((dataSet1.Tables[0].Rows.Count > 0 || dataSet1.Tables[1].Rows.Count > 0 ? true : dataSet2.Tables[0].Rows.Count > 0))
            {
                this.Page.RegisterStartupScript("ss", "<script language='javascript'> ReminderPopup();</script>");
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.SoftDelete();
    }

    public void setDefault()
    {
    }

    private void SingleSignonCode(string ClinicKey, string UserID, out string SingleSignOnCode, out string SingleSignOnUserIdVerify, bool IsUrlEncoding)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string base64String;
        SingleSignOnCode = "";
        SingleSignOnUserIdVerify = "";
        string str = EncryptionCommon.CreatePhrase();
        byte[] bytes = EncodingUtility.GetBytes(string.Concat(str, this.Key), EncodingUtility.encodingOptions.UTF8);
        SHA512Managed sHA512Managed = new SHA512Managed();
        try
        {
            base64String = Convert.ToBase64String(sHA512Managed.ComputeHash(bytes));
        }
        
        finally
        {
            if (sHA512Managed != null)
            {
                ((IDisposable)sHA512Managed).Dispose();
            }
        }
        if (base64String.Substring(base64String.Length - 2, 2).Equals("=="))
        {
            base64String = base64String.Substring(0, base64String.Length - 2);
        }
        SingleSignOnCode = (IsUrlEncoding ? HttpUtility.UrlEncode(string.Concat(str, base64String)) : string.Concat(str, base64String));
        base64String = "";
        SingleSignOnUserIdVerify = string.Concat(UserID, str.Substring(0, 22), this.Key);
        bytes = EncodingUtility.GetBytes(SingleSignOnUserIdVerify, EncodingUtility.encodingOptions.UTF8);
        sHA512Managed = new SHA512Managed();
        try
        {
            base64String = Convert.ToBase64String(sHA512Managed.ComputeHash(bytes));
        }
        finally
        {
            if (sHA512Managed != null)
            {
                ((IDisposable)sHA512Managed).Dispose();
            }
        }
        if (base64String.Substring(base64String.Length - 2, 2).Equals("=="))
        {
            base64String = base64String.Substring(0, base64String.Length - 2);
        }
        SingleSignOnUserIdVerify = (IsUrlEncoding ? HttpUtility.UrlEncode(base64String) : base64String);
    }

    public void SoftDelete()
    {
        Bill_Sys_PatientList.log.Debug("Start SoftDelete()");
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
        Bill_Sys_PatientList.log.Debug("End SoftDelete()");
    }
}