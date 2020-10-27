using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using System.Web.Security;
using System.Collections;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using log4net;
using LoginSecurityLibrary;
using mbs.ApplicationSettings;

public partial class Bill_Sys_OTP : PageBase
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
    String strsqlCon;
    SqlDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblErrorMsg.Text = "OTP has been sent on your registered Email Address";
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string str = "";    

        if (!ValidateOtp(Convert.ToString(Session["Username"]), txtotp.Text))
        {
            lblErrorMsg.Text = "OTP Doesn't match.";
        }
        else
        {

            createLoginCookies(Convert.ToString(Session["Username"]),
                                      Convert.ToBoolean(Convert.ToBoolean(Session["Persist"])),
                                      Convert.ToString(Session["Roles"]));
            _bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            DataSet set = new DataSet();
            set = _bill_Sys_LoginBO.getLoginDetails(this.Session["UserName"].ToString(), "", HttpContext.Current.Request.UserHostAddress);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                this.objAppSettings = (ApplicationSettings_BO)base.Application["OBJECT_APP_SETTINGS"];
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
                this._bill_Sys_UserObject.SZ_USER_ID = set.Tables[0].Rows[0][0].ToString();
                this._bill_Sys_UserObject.SZ_USER_NAME = set.Tables[0].Rows[0][1].ToString();
                this._bill_Sys_UserObject.SZ_USER_ROLE = set.Tables[0].Rows[0][4].ToString();
                this._bill_Sys_UserObject.SZ_PROVIDER_ID = set.Tables[0].Rows[0][5].ToString();
                this._bill_Sys_UserObject.SZ_USER_ROLE_NAME = set.Tables[0].Rows[0][8].ToString();
                this._bill_Sys_UserObject.SZ_USER_EMAIL = set.Tables[0].Rows[0]["USER_EMAIL_ID"].ToString();
                this._bill_Sys_UserObject.DomainName = set.Tables[0].Rows[0]["DomainName"].ToString();
                this.Session["USER_OBJECT"] = this._bill_Sys_UserObject;
                this._bill_Sys_BillingCompanyObject = new Bill_Sys_BillingCompanyObject();
                this._bill_Sys_BillingCompanyObject.SZ_COMPANY_ID = set.Tables[0].Rows[0][2].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_COMPANY_NAME = set.Tables[0].Rows[0][3].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_PREFIX = set.Tables[0].Rows[0][6].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_ADDRESS = set.Tables[0].Rows[0][9].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_EMAIL = set.Tables[0].Rows[0][10].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_PHONE = set.Tables[0].Rows[0][11].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_FAX = set.Tables[0].Rows[0][12].ToString();
                if (set.Tables[0].Rows[0][7].ToString() != "")
                {
                    this._bill_Sys_BillingCompanyObject.BT_REFERRING_FACILITY = Convert.ToBoolean(set.Tables[0].Rows[0][7].ToString());
                }
                else
                {
                    this._bill_Sys_BillingCompanyObject.BT_REFERRING_FACILITY = false;
                }
                if (set.Tables[0].Rows[0][13].ToString() != "")
                {
                    this._bill_Sys_BillingCompanyObject.BT_LAW_FIRM = Convert.ToBoolean(set.Tables[0].Rows[0][13].ToString());
                }
                else
                {
                    this._bill_Sys_BillingCompanyObject.BT_LAW_FIRM = false;
                }
                if (set.Tables[0].Rows[0]["BT_ATTORNY"].ToString() != "")
                {
                    this._bill_Sys_BillingCompanyObject.BT_ATTORNY = Convert.ToBoolean(set.Tables[0].Rows[0]["BT_ATTORNY"].ToString());
                }
                else
                {
                    this._bill_Sys_BillingCompanyObject.BT_ATTORNY = false;
                }
                if (set.Tables[0].Rows[0]["BT_PROVIDER"].ToString() != "")
                {
                    this._bill_Sys_BillingCompanyObject.BT_PROVIDER = Convert.ToBoolean(set.Tables[0].Rows[0]["BT_PROVIDER"].ToString());
                }
                else
                {
                    this._bill_Sys_BillingCompanyObject.BT_PROVIDER = false;
                }
                this.Session["BILLING_COMPANY_OBJECT"] = this._bill_Sys_BillingCompanyObject;
                this._bill_Sys_CaseInfo = new Bill_Sys_Case();
                this._bill_Sys_CaseInfo.SZ_CASE_ID = "";
                this.Session["CASE_INFO"] = this._bill_Sys_CaseInfo;
                this._bill_Sys_SystemObject = new Bill_Sys_SystemObject();
                DataView view = new DataView(set.Tables[1]);
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00003'";
                this._bill_Sys_SystemObject.SZ_DEFAULT_LAW_FIRM = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00004'";
                this._bill_Sys_SystemObject.SZ_CHART_NO = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00007'";
                this._bill_Sys_SystemObject.SZ_LOCATION = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00008'";
                this._bill_Sys_SystemObject.SZ_CHECKINVALUE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00009'";
                this._bill_Sys_SystemObject.AddVisits_SearchByChartNumber = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00014'";
                this._bill_Sys_SystemObject.SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00017'";
                this._bill_Sys_SystemObject.SZ_SHOW_PROVIDER_DISPLAY_NAME = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00018'";
                this._bill_Sys_SystemObject.SZ_SHOW_PATIENT_PHONE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00019'";
                this._bill_Sys_SystemObject.SZ_SHOW_PATIENT_SIGNATURE_FOR_NF3 = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00020'";
                this._bill_Sys_SystemObject.SZ_SHOW_DOCTOR_SIGNATURE_FOR_NF3 = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                this._bill_Sys_SystemObject.SZ_SHOW_DATE_OF_FIRST_TREATMENT = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00022'";
                this._bill_Sys_SystemObject.SZ_SHOW_NEW_POM = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00023'";
                this._bill_Sys_SystemObject.SZ_ASSIGN_DIAGNOSIS_CODE_TO_VISIT = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00025'";
                this._bill_Sys_SystemObject.ASSOCIATE_CASE_TYPE_WITH_VISITS = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00026'";
                this._bill_Sys_SystemObject.ADD_LOCATION_TO_VISITS = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00024'";
                this._bill_Sys_SystemObject.SZ_SHOW_INSURANCE_WITH_BILL = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00029'";
                this._bill_Sys_SystemObject.SZ_ALLOW_TO_EDIT_NF2_PDF = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00028'";
                this._bill_Sys_SystemObject.SZ_SHOW_NF3_PROCEDURE_CODE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00030'";
                this._bill_Sys_SystemObject.ALLOW_TO_ADD_VISIT_FOR_FUTURE_DATE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00031'";
                this._bill_Sys_SystemObject.PHONE_FORMATE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00032'";
                this._bill_Sys_SystemObject.DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_PATIENT_PHONE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00033'";
                this._bill_Sys_SystemObject.DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_LOCATION = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00034'";
                this._bill_Sys_SystemObject.SZ_SHOW_ADD_TO_PREFERED_LIST = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00037'";
                this._bill_Sys_SystemObject.SZ_ADD_SECONDARY_INSURANCE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00038'";
                this._bill_Sys_SystemObject.SZ_SHOW_ADD_TO_PREFERED_LIST_FOR_DIAGNOSIS_CODE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00049'";
                this._bill_Sys_SystemObject.SZ_SHOW_PATIENT_WALK_IN_ON_WORKAREA = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00050'";
                this._bill_Sys_SystemObject.SZ_COPY_PATIENT_TO_TEST_FACILITY = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00054'";
                this._bill_Sys_SystemObject.SZ_HP1_Display = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00051'";
                this._bill_Sys_SystemObject.SZ_MG2_Display = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00062'";
                this._bill_Sys_SystemObject.SZ_ALLOW_TO_EDIT_CODE_AMOUNT = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00059'";
                this._bill_Sys_SystemObject.SZ_ALLOW_HP1_SIGN = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00057'";
                this._bill_Sys_SystemObject.SZ_ALLOW_MODIFIER_TO_UPDATE_FOR_PROCEDURE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00080'";
                this._bill_Sys_SystemObject.SZ_ADD_APPOINTMENT = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00082'";
                this._bill_Sys_SystemObject.SZ_ENABLE_CYCLIC_PROCEDURE_CODE = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";
                view.RowFilter = "SZ_SYS_SETTING_KEY_ID='SS00084'";
                this._bill_Sys_SystemObject.IS_EMPLOYER = (view.ToTable().Rows.Count > 0) ? view.ToTable().Rows[0][1].ToString() : "0";


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
                        this._bill_Sys_SystemObject.SZ_SOFT_DELETE = "True";
                    }
                    else
                    {
                        if (row["SZ_CONFIGURATION"].ToString() == "HARD DELETE")
                        {
                            this._bill_Sys_SystemObject.SZ_HARD_DELETE = "True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "NEW BILLS")
                        {
                            this._bill_Sys_SystemObject.SZ_NEW_BILL = "True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "VIEW BILLS")
                        {
                            this._bill_Sys_SystemObject.SZ_VIEW_BILL = "True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "DELETE BILLS")
                        {
                            this._bill_Sys_SystemObject.SZ_DELETE_BILLS = "True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "DELETE VISIT")
                        {
                            this._bill_Sys_SystemObject.SZ_DELETE_VIEWS = "True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "EMG BILL")
                        {
                            this._bill_Sys_SystemObject.SZ_EMG_BILL = "True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "NOTE DELETE")
                        {
                            this._bill_Sys_SystemObject.SZ_NOTE_DELETE = "True";
                            continue;
                        }
                        if (row["SZ_CONFIGURATION"].ToString() == "NOTE SOFT DELETE")
                        {
                            this._bill_Sys_SystemObject.SZ_NOTE_SOFT_DELETE = "True";
                        }
                    }
                }
                this.Session["SYSTEM_OBJECT"] = this._bill_Sys_SystemObject;
                this._bill_Sys_LoginBO.ChangeLoginDate(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                this.Session["REMINDER"] = "TRUE";
                this.Session["IMDETAILS"] = "TRUE";
                string str4 = set.Tables[0].Rows[0][8].ToString();
                this._bill_Sys_BillingCompanyObject.SZ_READ_ONLY = this._bill_Sys_LoginBO.Readonly();
                this.Session["APPSTATUS"] = this._bill_Sys_BillingCompanyObject;
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
                        string str6 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        obj2 = (Bill_Sys_DocumentManagerObject)this.Session["IntDocUrl"];
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
                        obj3 = (Bill_Sys_DocumentManagerObject)this.Session["IntDocUrl"];
                        string str12 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
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
                    return;
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
           
        }
    }
    public bool ValidateOtp(string userName, string otp)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool status = false;
        StringBuilder sqlString = new StringBuilder();
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(strsqlCon);
        con.Open();
        SqlCommand command = new SqlCommand();

        try
        {
            command.Connection = con;
            sqlString.Append("SELECT 1 FROM MST_USERS");
            sqlString.Append(" WHERE SZ_USER_NAME=@username AND sz_otp=@otp AND dt_otp_expiry >= SYSDATETIME()");
            command.CommandType = CommandType.Text;
            command.CommandText = sqlString.ToString();
            command.Parameters.AddWithValue("otp", otp);
            command.Parameters.AddWithValue("username", userName);
            reader = command.ExecuteReader();
            if (reader.HasRows)
                status = true;
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
       
        finally
        {
            if (null != reader || !reader.IsClosed)
                reader.Dispose();
            command.Parameters.Clear();
            command.Dispose();
        }
        return status;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
       public bool UpdateCookieCode(string userName, string code)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        bool status = false;
        StringBuilder sqlString = new StringBuilder();
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(strsqlCon);
        con.Open();
        SqlCommand command = new SqlCommand();

        try
        {
            command.Connection = con;

            sqlString.Append("UPDATE MST_USERS");
            sqlString.Append(" SET sz_detect_code = @code");
            sqlString.Append(",DT_LAST_LOGIN = GETDATE()");
            sqlString.Append(" WHERE SZ_USER_NAME = @username");
            command.CommandType = CommandType.Text;
            command.CommandText = sqlString.ToString();

            command.Parameters.AddWithValue("code", code);
            command.Parameters.AddWithValue("username", userName);

            if (command.ExecuteNonQuery() > 0)
                status = true;
        }
        catch (Exception ex)
        {
            status = false;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        finally
        {
            command.Parameters.Clear();
            command.Dispose();
        }
        return !status;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
   
    protected void createLoginCookies(string userName, bool persist, string roles)
    {
        if (createIdentificationCookie())
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30), persist, roles, FormsAuthentication.FormsCookiePath);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }

    }
    protected bool createIdentificationCookie()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string cookieCode = Bill_Sys_Utility.GenerateOtp(9);
            string cookieEncryptionKey = System.Configuration.ConfigurationManager.AppSettings.Get("COOKIE_ENCRYPT_KEY");
            string encryptedCookieCode = Bill_Sys_Utility.ComputeHMACSHA1(cookieCode, cookieEncryptionKey);
           

            if (Request.Cookies["GREENBILLS_DETECTION"] == null)
            {

                HttpCookie cookieCnt = new HttpCookie("LOGIN_COUNT", "0");
                cookieCnt.Expires = DateTime.Now.AddYears(1);
                cookieCnt.Value = "0";
                Response.Cookies.Add(cookieCnt);

                HttpCookie cookie1 = new HttpCookie("GREENBILLS_DETECTION");
                cookie1.Expires = DateTime.Now.AddYears(1);
                cookie1[cookieCnt.Value] = encryptedCookieCode;
                Response.Cookies.Add(cookie1);
            }
            else
            {


                HttpCookie cookie2 = Request.Cookies["GREENBILLS_DETECTION"];
                HttpCookie cookieCnt = Request.Cookies["LOGIN_COUNT"];
                int allowCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("MAX_USER_ALLOW"));
                if (cookieCnt == null)
                {
                     cookieCnt = new HttpCookie("LOGIN_COUNT", "0");
                     cookieCnt.Value = (Convert.ToInt32(cookie2.Values.Count) - 1).ToString();
                     cookieCnt.Expires = DateTime.Now.AddYears(1); 
                }
                
                if (Convert.ToInt32(cookieCnt.Value) < allowCount - 1)
                {
                    cookie2.Expires = DateTime.Now.AddYears(1);
                    cookieCnt.Value = (Convert.ToInt32(cookieCnt.Value) + 1).ToString();
                    cookie2[cookieCnt.Value] = encryptedCookieCode;

                    Response.Cookies.Add(cookie2);
                    Response.Cookies.Add(cookieCnt);
                }
                else
                {

                    return createIdentificationCookie(GetLastIndex());
                }
            }



            if (UpdateCookieCode( Convert.ToString(Session["Username"]), cookieCode))
                return true;
            else
                return false;

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
            return false;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected bool createIdentificationCookie(int cntval)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string cookieCode = Bill_Sys_Utility.GenerateOtp(6);
            string cookieEncryptionKey = System.Configuration.ConfigurationManager.AppSettings.Get("COOKIE_ENCRYPT_KEY");
            string encryptedCookieCode = Bill_Sys_Utility.ComputeHMACSHA1(cookieCode, cookieEncryptionKey);


            HttpCookie cookie2 = Request.Cookies["GREENBILLS_DETECTION"];


            cookie2.Expires = DateTime.Now.AddYears(1);

            cookie2[cntval.ToString()] = encryptedCookieCode;

            Response.Cookies.Add(cookie2);


            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, Convert.ToString(Session["Username"]), DateTime.Now, DateTime.Now.AddMinutes(30), Convert.ToBoolean(Session["persist"]), Convert.ToString(Session["roles"]), FormsAuthentication.FormsCookiePath);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);

            if (UpdateCookieCode( Convert.ToString(Session["Username"]), cookieCode))
                return true;
            else
                return false;

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
            return false;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        
        
    }


    public int GetLastIndex()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int ireturn = 0;
        StringBuilder sqlString = new StringBuilder();
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(strsqlCon);
        con.Open();
        SqlCommand command = new SqlCommand();
        try
        {
            command.Connection = con;
            sqlString.Append("select sz_detect_code, SZ_USER_NAME, DT_LAST_LOGIN from MST_USERS  where ISNULL(sz_detect_code,'')<>''");
            command.CommandType = CommandType.Text;
            command.CommandText = sqlString.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            command.Parameters.Clear();
            command.Dispose();

            HttpCookie cookie = Request.Cookies["GREENBILLS_DETECTION"];

            int iCount = 0;
            ArrayList arrDetectCode = new ArrayList();
            foreach (string val in cookie.Values)
            {
                int iFlag = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (cookie[val].Equals(Bill_Sys_Utility.ComputeHMACSHA1(ds.Tables[0].Rows[i]["sz_detect_code"].ToString(), System.Configuration.ConfigurationManager.AppSettings.Get("COOKIE_ENCRYPT_KEY"))))
                    {
                        iFlag = 1;
                        arrDetectCode.Add(ds.Tables[0].Rows[i]["sz_detect_code"].ToString());
                    }
                }
                if (iFlag == 0)
                {
                    return iCount;
                }

                iCount++;

            }

        
        
            string detectCode = string.Empty;
            for (int i = 0; i < arrDetectCode.Count; i++)
            {
                if (detectCode == string.Empty)
                {
                    detectCode = "'" + arrDetectCode[i].ToString() + "'";
                }
                else
                {
                    detectCode += ",'" + arrDetectCode[i].ToString() + "'";
                }
            }
            sqlString.Remove(0, sqlString.Length);
            sqlString.Append("select    sz_detect_code from MST_USERS where  DT_LAST_LOGIN in (");
            sqlString.Append("select  MIN(DT_LAST_LOGIN) from MST_USERS where  sz_detect_code in(" + detectCode + "))");
            sqlString.Append("and sz_detect_code in(" + detectCode + ")");


            con.Open();
            command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sqlString.ToString();
            SqlDataReader dr = command.ExecuteReader();
            string code = "";
            while (dr.Read())
            {
                code = dr[0].ToString();
            }
            dr.Close();
            command.Parameters.Clear();
            command.Dispose();
            if (code == "")
            {
                return ireturn;
            }
            else
            {
                foreach (string val in cookie.Values)
                {
                    if (cookie[val].Equals(Bill_Sys_Utility.ComputeHMACSHA1(code, System.Configuration.ConfigurationManager.AppSettings.Get("COOKIE_ENCRYPT_KEY"))))
                    {
                        return ireturn;
                    }
                    ireturn++;
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
        
          finally
            {

            }
            return 0;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    }


 
     