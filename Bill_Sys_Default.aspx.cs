using log4net;
using LoginSecurityLibrary;
using mbs.ApplicationSettings;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class Bill_Sys_Default : System.Web.UI.Page
{
    private Bill_Sys_BillingCompanyObject _bill_Sys_BillingCompanyObject;
    private Bill_Sys_Case _bill_Sys_CaseInfo;
    private Bill_Sys_LoginBO _bill_Sys_LoginBO;
    private Bill_Sys_SystemObject _bill_Sys_SystemObject;
    private Bill_Sys_UserObject _bill_Sys_UserObject;
    private LoginSecurity _loginSecurity;
    //protected HtmlForm form1;
    private static ILog log = LogManager.GetLogger("Bill_Sys_Default.aspx");
    private ApplicationSettings_BO objAppSettings;

    protected string GetProcGroupCode(string ProceGroupID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = new DataSet();
        string str2 = "";
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_GET_PROCEDURE_GROUP_CODE", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_USER_ID", ProceGroupID);
            selectCommand.Parameters.AddWithValue("@FLAG", "USERID");
            new SqlDataAdapter(selectCommand).Fill(dataSet);
            if ((dataSet.Tables.Count >= 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                str2 = dataSet.Tables[0].Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string Elmahstr2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + Elmahstr2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return str2;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._loginSecurity = new LoginSecurity();
        try
        {
            this.UserLogin();
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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void UserLogin()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if ( this.Session["UserName"]==null)
        {

        }
        try
        {
            string str = "";
            string str2 = "";
            if (base.Request.QueryString["name"] != null)
            {
                str2 = base.Request.QueryString["name"].ToString();
            }
            new ArrayList();
            this._bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            DataSet set = new DataSet();
            set = this._bill_Sys_LoginBO.getLoginDetails(this.Session["UserName"].ToString(), str2, HttpContext.Current.Request.UserHostAddress);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                this.objAppSettings = (ApplicationSettings_BO) base.Application["OBJECT_APP_SETTINGS"];
                if (this.objAppSettings == null)
                {
                    this.objAppSettings = new ApplicationSettings_BO();
                    base.Application["OBJECT_APP_SETTINGS"] = this.objAppSettings;
                }
                if ((set.Tables[0].Rows.Count == 1) && (set.Tables[0].Rows[0][0].ToString() == "False"))
                {
                    string str3 = HttpContext.Current.Request.UserHostAddress.ToString();
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage("IPValidationCheck=false&InvalidIP=" + str3);
                    return;
                }
                this._bill_Sys_UserObject = new Bill_Sys_UserObject();
                this._bill_Sys_UserObject.SZ_USER_ID=set.Tables[0].Rows[0][0].ToString();
                this._bill_Sys_UserObject.SZ_USER_NAME=set.Tables[0].Rows[0][1].ToString();
                this._bill_Sys_UserObject.SZ_USER_ROLE=set.Tables[0].Rows[0][4].ToString();
                this._bill_Sys_UserObject.SZ_PROVIDER_ID=set.Tables[0].Rows[0][5].ToString();
                this._bill_Sys_UserObject.SZ_USER_ROLE_NAME=set.Tables[0].Rows[0][8].ToString();
                this._bill_Sys_UserObject.SZ_USER_EMAIL=set.Tables[0].Rows[0]["USER_EMAIL_ID"].ToString();
                this._bill_Sys_UserObject.DomainName=set.Tables[0].Rows[0]["DomainName"].ToString();
                this.Session["USER_OBJECT"] = this._bill_Sys_UserObject;
                this._bill_Sys_BillingCompanyObject = new Bill_Sys_BillingCompanyObject();
                this._bill_Sys_BillingCompanyObject.SZ_COMPANY_ID=set.Tables[0].Rows[0][2].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_COMPANY_NAME=set.Tables[0].Rows[0][3].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_PREFIX=set.Tables[0].Rows[0][6].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_ADDRESS=set.Tables[0].Rows[0][9].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_EMAIL=set.Tables[0].Rows[0][10].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_PHONE=set.Tables[0].Rows[0][11].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_FAX=set.Tables[0].Rows[0][12].ToString();
                if (set.Tables[0].Rows[0][7].ToString() != "")
                {
                    this._bill_Sys_BillingCompanyObject.BT_REFERRING_FACILITY=Convert.ToBoolean(set.Tables[0].Rows[0][7].ToString());
                }
                else
                {
                    this._bill_Sys_BillingCompanyObject.BT_REFERRING_FACILITY=false;
                }
                if (set.Tables[0].Rows[0][13].ToString() != "")
                {
                    this._bill_Sys_BillingCompanyObject.BT_LAW_FIRM=Convert.ToBoolean(set.Tables[0].Rows[0][13].ToString());
                }
                else
                {
                    this._bill_Sys_BillingCompanyObject.BT_LAW_FIRM=false;
                }
                if (set.Tables[0].Rows[0]["BT_ATTORNY"].ToString() != "")
                {
                    this._bill_Sys_BillingCompanyObject.BT_ATTORNY=Convert.ToBoolean(set.Tables[0].Rows[0]["BT_ATTORNY"].ToString());
                }
                else
                {
                    this._bill_Sys_BillingCompanyObject.BT_ATTORNY=false;
                }
                if (set.Tables[0].Rows[0]["BT_PROVIDER"].ToString() != "")
                {
                    this._bill_Sys_BillingCompanyObject.BT_PROVIDER=Convert.ToBoolean(set.Tables[0].Rows[0]["BT_PROVIDER"].ToString());
                }
                else
                {
                    this._bill_Sys_BillingCompanyObject.BT_PROVIDER=false;
                }
                this.Session["BILLING_COMPANY_OBJECT"] = this._bill_Sys_BillingCompanyObject;
                //Custom schedular
                Session["Billing_Company_ID"] = _bill_Sys_BillingCompanyObject.SZ_COMPANY_ID;
                this._bill_Sys_CaseInfo = new Bill_Sys_Case();
                this._bill_Sys_CaseInfo.SZ_CASE_ID="";
                this.Session["CASE_INFO"] = this._bill_Sys_CaseInfo;
                this._bill_Sys_SystemObject = new Bill_Sys_SystemObject();
                DataView view = new DataView(set.Tables[1]);
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00003'";
                this._bill_Sys_SystemObject.SZ_DEFAULT_LAW_FIRM=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00004'";
                this._bill_Sys_SystemObject.SZ_CHART_NO=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00007'";
                this._bill_Sys_SystemObject.SZ_LOCATION=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00008'";
                this._bill_Sys_SystemObject.SZ_CHECKINVALUE=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00009'";
                this._bill_Sys_SystemObject.AddVisits_SearchByChartNumber=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00014'";
                this._bill_Sys_SystemObject.SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00017'";
                this._bill_Sys_SystemObject.SZ_SHOW_PROVIDER_DISPLAY_NAME=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00018'";
                this._bill_Sys_SystemObject.SZ_SHOW_PATIENT_PHONE=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00019'";
                this._bill_Sys_SystemObject.SZ_SHOW_PATIENT_SIGNATURE_FOR_NF3=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00020'";
                this._bill_Sys_SystemObject.SZ_SHOW_DOCTOR_SIGNATURE_FOR_NF3=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                this._bill_Sys_SystemObject.SZ_SHOW_DATE_OF_FIRST_TREATMENT=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00022'";
                this._bill_Sys_SystemObject.SZ_SHOW_NEW_POM=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00023'";
                this._bill_Sys_SystemObject.SZ_ASSIGN_DIAGNOSIS_CODE_TO_VISIT=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00025'";
                this._bill_Sys_SystemObject.ASSOCIATE_CASE_TYPE_WITH_VISITS=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00026'";
                this._bill_Sys_SystemObject.ADD_LOCATION_TO_VISITS=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00024'";
                this._bill_Sys_SystemObject.SZ_SHOW_INSURANCE_WITH_BILL=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00029'";
                this._bill_Sys_SystemObject.SZ_ALLOW_TO_EDIT_NF2_PDF=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00028'";
                this._bill_Sys_SystemObject.SZ_SHOW_NF3_PROCEDURE_CODE=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00030'";
                this._bill_Sys_SystemObject.ALLOW_TO_ADD_VISIT_FOR_FUTURE_DATE=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00031'";
                this._bill_Sys_SystemObject.PHONE_FORMATE=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00032'";
                this._bill_Sys_SystemObject.DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_PATIENT_PHONE=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00033'";
                this._bill_Sys_SystemObject.DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_LOCATION=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00034'";
                this._bill_Sys_SystemObject.SZ_SHOW_ADD_TO_PREFERED_LIST=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00037'";
                this._bill_Sys_SystemObject.SZ_ADD_SECONDARY_INSURANCE=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00038'";
                this._bill_Sys_SystemObject.SZ_SHOW_ADD_TO_PREFERED_LIST_FOR_DIAGNOSIS_CODE=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00049'";
                this._bill_Sys_SystemObject.SZ_SHOW_PATIENT_WALK_IN_ON_WORKAREA=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00050'";
                this._bill_Sys_SystemObject.SZ_COPY_PATIENT_TO_TEST_FACILITY=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00054'";
                this._bill_Sys_SystemObject.SZ_HP1_Display=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00051'";
                this._bill_Sys_SystemObject.SZ_MG2_Display=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00062'";
                this._bill_Sys_SystemObject.SZ_ALLOW_TO_EDIT_CODE_AMOUNT=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00059'";
                this._bill_Sys_SystemObject.SZ_ALLOW_HP1_SIGN=(view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00057'";
                this._bill_Sys_SystemObject.SZ_ALLOW_MODIFIER_TO_UPDATE_FOR_PROCEDURE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00080'";
                this._bill_Sys_SystemObject.SZ_ADD_APPOINTMENT = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00082'";
                this._bill_Sys_SystemObject.SZ_ENABLE_CYCLIC_PROCEDURE_CODE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00084'";
                this._bill_Sys_SystemObject.IS_EMPLOYER = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00086'";
                this._bill_Sys_SystemObject.SZ_ENABLE_CONTRACT_PDF_GENERATION = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";


                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00012'";
                if (view.ToTable().Rows.Count > 0)
                {
                    if (view.ToTable().Rows[0][1].ToString() == this._bill_Sys_UserObject.SZ_USER_ID)
                    {
                        this.Session["IPAdmin"] = "True";
                    }
                    else
                    {
                        this.Session["IPAdmin"] = "False";
                    }
                }
                else
                {
                    this.Session["IPAdmin"] = "False";
                }
                DataTable table = set.Tables[2];
                foreach (DataRow row in table.Rows)
                {
                    if (row["SZ_CONFIGURATION"].ToString() == "SOFT DELETE")
                    {
                        this._bill_Sys_SystemObject.SZ_SOFT_DELETE="True";
                    }
                    else
                    {
                        if (row["SZ_CONFIGURATION"].ToString() == "HARD DELETE")
                        {
                            this._bill_Sys_SystemObject.SZ_HARD_DELETE="True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "NEW BILLS")
                        {
                            this._bill_Sys_SystemObject.SZ_NEW_BILL="True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "VIEW BILLS")
                        {
                            this._bill_Sys_SystemObject.SZ_VIEW_BILL="True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "DELETE BILLS")
                        {
                            this._bill_Sys_SystemObject.SZ_DELETE_BILLS="True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "DELETE VISIT")
                        {
                            this._bill_Sys_SystemObject.SZ_DELETE_VIEWS="True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "EMG BILL")
                        {
                            this._bill_Sys_SystemObject.SZ_EMG_BILL="True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "NOTE DELETE")
                        {
                            this._bill_Sys_SystemObject.SZ_NOTE_DELETE="True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "NOTE SOFT DELETE")
                        {
                            this._bill_Sys_SystemObject.SZ_NOTE_SOFT_DELETE="True";
                        }
                    }
                }
                this.Session["SYSTEM_OBJECT"] = this._bill_Sys_SystemObject;
                this._bill_Sys_LoginBO.ChangeLoginDate(((Bill_Sys_UserObject) this.Session["USER_OBJECT"]).SZ_USER_ID);
                this.Session["REMINDER"] = "TRUE";
                this.Session["IMDETAILS"] = "TRUE";
                string str4 = set.Tables[0].Rows[0][8].ToString();
                if (str4.ToLower() == "doctor")
                {
                    if ((set.Tables.Count > 3) && (set.Tables[3].Rows.Count > 0))
                    {
                        str = set.Tables[3].Rows[0][0].ToString();
                    }
                    if (str.ToString().Equals("IM"))
                    {
                        this.Session["PageRedirect"] = "~/Bill_Sys_IM_CheckOut.aspx";
                    }
                    else
                    {
                        this.Session["PageRedirect"] = @"~/AJAX Pages\Bill_Sys_PatientSearch.aspx";
                    }
                }
                else if (str4.ToLower() == "agent")
                {
                    this.Session["PageRedirect"] = "~/Agent/Bill_Sys_Agent_SearchCase.aspx";
                }
                else if (this._bill_Sys_BillingCompanyObject.BT_LAW_FIRM.ToString().Equals("True") && this._bill_Sys_BillingCompanyObject.BT_REFERRING_FACILITY.ToString().Equals("False"))
                {
                    if ((str4.ToLower() == "lawfirm attorney") && (this.Session["urlintegration"] != null))
                    {
                        string s = this.Session["urlintegration"].ToString();
                        s = base.Server.UrlEncode(s);
                        this.Session["PageRedirect"] = "~/ATT/Bill_Sys_AttorneySearch.aspx?dt=" + s;
                    }
                    if (this.Session["IntDocUrl"] != null)
                    {
                        Bill_Sys_DocumentManagerObject obj2 = new Bill_Sys_DocumentManagerObject();
                        string str6 = ((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        obj2 = (Bill_Sys_DocumentManagerObject) this.Session["IntDocUrl"];
                        string str7 = obj2.SZ_CASE_ID;
                        string str8 = obj2.SZ_CASE_NO;
                        string str9 = obj2.SZ_COMAPNY_ID;
                        string str10 = obj2.SZ_LAWFIRM_ID;
                        if (str6 == str10)
                        {
                            this.Session["PageRedirect"] = "~/Document Manager/case/vb_CaseInformation.aspx?caseid=" + str7 + "&caseno=" + str8 + "&cmpid=" + str9;
                        }
                        else
                        {
                            this.Session["PageRedirect"] = @"~/LF\Bill_Sys_SearchCase.aspx";
                        }
                    }
                    else
                    {
                        this.Session["PageRedirect"] = @"~/LF\Bill_Sys_SearchCase.aspx";
                    }
                }
                else if (this._bill_Sys_BillingCompanyObject.BT_PROVIDER.ToString().Equals("True"))
                {
                    if (str4.ToLower() == "provider")
                    {
                        this.Session["PageRedirect"] = @"~/Provider\Bill_Sys_SearchCase.aspx";
                    }
                }
                else if (this._bill_Sys_BillingCompanyObject.BT_ATTORNY.ToString().Equals("True") && this._bill_Sys_BillingCompanyObject.BT_REFERRING_FACILITY.ToString().Equals("False"))
                {
                    this.Session["PageRedirect"] = @"~/ATT\Bill_Sys_AttorneySearch.aspx";
                }
                else if (set.Tables[0].Rows[0][6].ToString() != "")
                {
                    this.Session["PageRedirect"] = "~/AJAX Pages/Bill_Sys_SearchCase.aspx";
                }
                else
                {
                    this.Session["PageRedirect"] = "~/Bill_Sys_BillingCompany.aspx";
                }
                if (set.Tables[0].Rows[0]["Force_PasswordChange"].ToString() == "True")
                {
                    base.Response.Redirect(@"AJAX Pages\Bill_Sys_ChangePassword.aspx", false);
                    return;
                }
                if (str4.ToLower() == "doctor")
                {
                    if (str.ToString().Equals("IM"))
                    {
                        base.Response.Redirect("Bill_Sys_IM_CheckOut.aspx", false);
                    }
                    else
                    {
                        base.Response.Redirect(@"AJAX Pages\Bill_Sys_PatientSearch.aspx", false);
                    }
                }
                else if (str4.ToLower() == "agent")
                {
                    base.Response.Redirect(@"Agent\Bill_Sys_Agent_SearchCase.aspx", false);
                }
                else if (this._bill_Sys_BillingCompanyObject.BT_LAW_FIRM.ToString().Equals("True") && this._bill_Sys_BillingCompanyObject.BT_REFERRING_FACILITY.ToString().Equals("False"))
                {
                    if ((str4.ToLower() == "lawfirm attorney") && (this.Session["urlintegration"] != null))
                    {
                        string str11 = this.Session["urlintegration"].ToString();
                        str11 = base.Server.UrlEncode(str11);
                        str11 = "ATT/Bill_Sys_AttorneySearch.aspx?dt=" + str11;
                        base.Response.Redirect(str11, false);
                    }
                    if (this.Session["IntDocUrl"] != null)
                    {
                        Bill_Sys_DocumentManagerObject obj3 = new Bill_Sys_DocumentManagerObject();
                        obj3 = (Bill_Sys_DocumentManagerObject) this.Session["IntDocUrl"];
                        string str12 = ((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        string str13 = obj3.SZ_CASE_ID;
                        string str14 = obj3.SZ_CASE_NO;
                        string str15 = obj3.SZ_COMAPNY_ID;
                        string str16 = obj3.SZ_LAWFIRM_ID;
                        if (str12 == str16)
                        {
                            string url = "Document Manager/case/vb_CaseInformation.aspx?caseid=" + str13 + "&caseno=" + str14 + "&cmpid=" + str15;
                            base.Response.Redirect(url, false);
                        }
                        else
                        {
                            base.Response.Redirect(@"LF\Bill_Sys_SearchCase.aspx", false);
                        }
                    }
                    else
                    {
                        base.Response.Redirect(@"LF\Bill_Sys_SearchCase.aspx", false);
                    }
                }
                else if (this._bill_Sys_BillingCompanyObject.BT_PROVIDER.ToString().Equals("True"))
                {
                    if (str4.ToLower() == "provider")
                    {
                        base.Response.Redirect(@"Provider\Bill_Sys_SearchCase.aspx", false);
                    }
                }
                else if (this._bill_Sys_BillingCompanyObject.BT_ATTORNY.ToString().Equals("True") && this._bill_Sys_BillingCompanyObject.BT_REFERRING_FACILITY.ToString().Equals("False"))
                {
                    base.Response.Redirect(@"ATT\Bill_Sys_AttorneySearch.aspx", false);
                }
                else if (set.Tables[0].Rows[0][6].ToString() != "")
                {
                    base.Response.Redirect("AJAX Pages/Bill_Sys_SearchCase.aspx", false);
                }
                else
                {
                    base.Response.Redirect("Bill_Sys_BillingCompany.aspx", false);
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            this._bill_Sys_BillingCompanyObject.SZ_READ_ONLY=this._bill_Sys_LoginBO.Readonly();
            this.Session["APPSTATUS"] = this._bill_Sys_BillingCompanyObject;
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

