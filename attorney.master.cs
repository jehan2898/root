using DevExpress.Web;
using log4net;
using log4net.Config;
using mbs.ApplicationSettings;
using OboutInc.SlideMenu;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class attorney : System.Web.UI.MasterPage
{
    private DAO_User c_objUser = null;
    private static ILog log = LogManager.GetLogger("attorney");
    private mbs.ApplicationSettings.ApplicationSettings_BO objAppSettings = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        string str;
        attorney.log.Debug("Start Master Page Init");
        HttpBrowserCapabilities browser = base.Request.Browser;
        string type = browser.Type;
        string browser1 = browser.Browser;
        string str1 = "";
        str1 = (base.Request.RawUrl.IndexOf("?") <= 0 ? base.Request.RawUrl : base.Request.RawUrl.Substring(0, base.Request.RawUrl.IndexOf("?")));
        if (!browser.Browser.ToLower().Contains("firefox"))
        {
            str = (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple") ? "css/main-ch.css" : "css/main-ie.css");
        }
        else
        {
            str = "css/main-ff.css";
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("");
        if (!str1.Contains("AJAX Pages"))
        {
            stringBuilder.AppendLine("<link rel='shortcut icon' href='Registration/icon.ico' />");
        }
        else
        {
            stringBuilder.AppendLine("<link rel='shortcut icon' href='../Registration/icon.ico' />");
        }
        stringBuilder.AppendLine("<link type='text/css' rel='stylesheet' href='css/intake-sheet-ff.css' />");
        stringBuilder.AppendLine("<link href='Css/mainmaster.css' rel='stylesheet' type='text/css' />");
        stringBuilder.AppendLine("<link href='Css/UI.css' rel='stylesheet' type='text/css' />");
        stringBuilder.AppendLine("<link type='text/css' rel='stylesheet' href='css/style.css' />");
        stringBuilder.AppendLine("<link href='css/style.css'  rel='stylesheet' type='text/css'  />");
        stringBuilder.AppendLine(string.Concat("<link href='", str, "' type='text/css' rel='Stylesheet' />"));
        this.masterhead.InnerHtml = stringBuilder.ToString();
        attorney.log.Debug("End Master Page Init method.");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        attorney.log.Debug("start Master Page Page_Load()");
        if (base.Application["Error"] != null)
        {
            base.Response.Write(base.Application["Error"].ToString());
        }
        try
        {
            this.objAppSettings = (ApplicationSettings_BO)base.Application["OBJECT_APP_SETTINGS"];
            if (this.objAppSettings == null)
            {
                this.objAppSettings = new ApplicationSettings_BO();
                base.Application["OBJECT_APP_SETTINGS"] = this.objAppSettings;
            }
            string parameterValue = this.objAppSettings.getParameterValue("site_logo").ParameterValue;
            this.ImageButton1.ImageUrl = parameterValue;
            XmlConfigurator.Configure();
            this.c_objUser = new DAO_User();
            string str = "";
            str = (base.Request.RawUrl.IndexOf("?") <= 0 ? base.Request.RawUrl : base.Request.RawUrl.Substring(0, base.Request.RawUrl.IndexOf("?")));
            this.c_objUser.UserID = (str.Substring(str.LastIndexOf("/") + 1, str.LastIndexOf(".aspx") - str.LastIndexOf("/") + 4));
            string str1 = base.Request.RawUrl.ToString();
            string[] strArrays = str1.Split(new char[] { '?' });
            if ((int)strArrays.Length > 1 && strArrays[1].Equals("Menuflag=true"))
            {
                if (!strArrays[0].Contains("/AJAX Pages/Bill_Sys_OutScheduleReport.aspx"))
                {
                    this.c_objUser.UserID = "Bill_Sys_VerificationSent_PrintPOM.aspx";
                }
                else
                {
                    this.c_objUser.UserID = "Bill_Sys_OutScheduleReport.aspx?Menuflag=true";
                }
            }
            if (this.c_objUser.UserID == "Bill_Sys_ReferralBillTransaction.aspx")
            {
                this.c_objUser.UserID = "Bill_Sys_BillTransaction.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_ReferringDoctor.aspx")
            {
                this.c_objUser.UserID = "Bill_Sys_Doctor.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_ChangePasswordMaster.aspx")
            {
                this.c_objUser.UserID = "Bill_Sys_UserMaster.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_BillSearch.aspx" && base.Request.QueryString["fromCase"] != null)
            {
                this.c_objUser.UserID = "atnotes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_BillTransaction.aspx")
            {
                this.c_objUser.UserID = "atnotes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_NewPaymentReport.aspx" && base.Request.QueryString["fromCase"] != null)
            {
                this.c_objUser.UserID = "atnotes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_PaymentTransactions.aspx")
            {
                this.c_objUser.UserID = "atnotes.aspx";
            }
            if (this.c_objUser.UserID.Contains("Bill_Sys_IM_"))
            {
                this.c_objUser.UserID = "Bill_Sys_IM_HistoryOfPresentIillness.aspx";
            }
            if (this.c_objUser.UserID.Contains("Bill_Sys_FUIM_"))
            {
                this.c_objUser.UserID = "Bill_Sys_FUIM_StartExamination.aspx";
            }
            if (this.c_objUser.UserID.Contains("Bill_Sys_AC_"))
            {
                this.c_objUser.UserID = "Bill_Sys_AC_AccuReEval.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_MiscPaymentReport.aspx" && base.Request.QueryString["fromCase"] != null)
            {
                this.c_objUser.UserID = "atnotes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_Misc_Payment.aspx")
            {
                this.c_objUser.UserID = "atnotes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_Invoice.aspx")
            {
                this.c_objUser.UserID = "atnotes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_PatientBillingSummary.aspx")
            {
                this.c_objUser.UserID = "atnotes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_Invoice_Report.aspx")
            {
                if (base.Request.QueryString["fromCase"] == "False")
                {
                    this.c_objUser.UserID = "Bill_Sys_BillSearch.aspx";
                }
                else
                {
                    this.c_objUser.UserID = "atnotes.aspx";
                }
            }
            if (this.c_objUser.UserID == "TemplateManager.aspx")
            {
                if (base.Request.QueryString["fromCase"] != "true")
                {
                    this.c_objUser.UserID = "atnotes.aspx";
                }
                else
                {
                    this.c_objUser.UserID = "Bill_Sys_CheckOut.aspx";
                }
            }
            //--Add comment--//
            if (this.c_objUser.UserID == "atcasedetails.aspx")
            {
                this.c_objUser.UserID = "Bill_Sys_CaseDetails.aspx";
            }
            //---end---//
            if (this.c_objUser.UserID == "Bill_Sys_PatientSearch.aspx")
            {
                this.c_objUser.UserID = "Bill_Sys_CheckOut.aspx";
            }
            SpecialityPDFDAO specialityPDFDAO = new SpecialityPDFDAO();
            if (base.Session["SPECIALITY_PDF_OBJECT"] == null)
            {
                specialityPDFDAO = null;
            }
            else
            {
                specialityPDFDAO = (SpecialityPDFDAO)base.Session["SPECIALITY_PDF_OBJECT"];
            }
            this.c_objUser.UserRoleID = (((Bill_Sys_UserObject)base.Session["USER_OBJECT"]).SZ_USER_ROLE);
            ArrayList arrayLists = new ArrayList();
            arrayLists.Add(str);
            OptionMenu optionMenu = new OptionMenu(this.c_objUser);
            optionMenu.Initialize(this.problue, (Bill_Sys_BillingCompanyObject)base.Session["BILLING_COMPANY_OBJECT"], (Bill_Sys_UserObject)base.Session["USER_OBJECT"], (Bill_Sys_SystemObject)base.Session["SYSTEM_OBJECT"], this.c_objUser, specialityPDFDAO, arrayLists);
            this.problue.AllExpanded = true;
            this.problue.AutoPostBack = false;
            if (!((Bill_Sys_BillingCompanyObject)base.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
               // this.lnkScheduleReport.HRef = "Bill_Sys_ScheduleEvent.aspx?TOp=true";
            }
            else
            {
               // this.lnkScheduleReport.HRef = "AJAX Pages/Bill_Sys_AppointPatientEntry.aspx";
            }
            this.ShowAssignedLinks(this.c_objUser.UserRoleID);
            attorney.log.Debug("End Master Page_Load()");
            DataSet dataSet = new DataSet();
            Bill_Sys_ProcedureCode_BO billSysProcedureCodeBO = new Bill_Sys_ProcedureCode_BO();
            dataSet = billSysProcedureCodeBO.Get_Sys_Key("SS00040", ((Bill_Sys_BillingCompanyObject)base.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0][0].ToString() == "0")
            {
                this.lnkshedulevisits.Visible = false;
            }
            if (((Bill_Sys_BillingCompanyObject)base.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == "CO000000000000000114")
            {
               // this.lnkScheduleReport.Visible = false;
                this.lnkQuickSearch.Visible = false;
                this.A2.Visible = false;
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            attorney.log.Debug(string.Concat("attorney. Method - Page_Load : ", exception.Message.ToString()));
            attorney.log.Debug(string.Concat("attorney. Method - Page_Load : ", exception.StackTrace.ToString()));
            if (exception.InnerException != null)
            {
                attorney.log.Debug(string.Concat("attorney. Method - Page_Load : ", exception.InnerException.Message.ToString()));
                attorney.log.Debug(string.Concat("attorney. Method - Page_Load : ", exception.InnerException.StackTrace.ToString()));
            }
        }
    }

    private void ShowAssignedLinks(string userroleid)
    {
       
        OptionMenuDataAccess access = new OptionMenuDataAccess();
        try
        {
            foreach (DataRow row in access.GetAssignedMenu(userroleid).Rows)
            {
                ((Bill_Sys_UserObject)base.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower().Equals("attorney");
                switch (row[0].ToString())
                {
                    case "Search":
                        {
                            if (!(((Bill_Sys_UserObject)base.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower() != "doctor"))
                            {
                                break;
                            }
                            this.lnkHome.Visible = true;
                            continue;
                        }
                    case "Desk":
                        {
                            this.lnkDesks.Visible = true;
                            continue;
                        }
                    case "Data Entry":
                        {
                            this.lnkDataEntry.Visible = true;
                            continue;
                        }
                    case "Calendar":
                        {
                            // this.lnkCalendar.Visible = false;
                            //this.lnkScheduleReport.Visible = true;
                            continue;
                        }
                    case "Quick Bill Entry":
                        {
                            this.lnkQuickSearch.Visible = true;
                            continue;
                        }
                    case "Reports":
                        {
                            this.lnkReports.Visible = true;
                            continue;
                        }
                    case "Doctor Screen":
                        {
                            this.A2.Visible = false;
                            continue;
                        }
                    case "Billing":
                        {
                            this.lnkBillingReport.Visible = true;
                            continue;
                        }
                   default:
                        {
                            continue;
                        }
                }
                this.lnkHome.Visible = true;
            }
        }
        catch (Exception exception)
        {
            log.Debug("attorney. Method - ShowAssignedLinks : " + exception.Message.ToString());
            log.Debug("attorney. Method - ShowAssignedLinks : " + exception.StackTrace.ToString());
            if (exception.InnerException != null)
            {
                log.Debug("attorney. Method - ShowAssignedLinks : " + exception.InnerException.Message.ToString());
                log.Debug("attorney. Method - ShowAssignedLinks : " + exception.InnerException.StackTrace.ToString());
            }
        }
    }

}
