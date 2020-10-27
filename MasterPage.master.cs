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

public partial class Shared_MasterPage : System.Web.UI.MasterPage
{
    private DAO_User c_objUser = null;
    private static ILog log = LogManager.GetLogger("Shared_MasterPage");
    private mbs.ApplicationSettings.ApplicationSettings_BO objAppSettings = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        string str;
        log.Debug("Start Master Page Init");
        HttpBrowserCapabilities browser = base.Request.Browser;
        string type = browser.Type;
        string text2 = browser.Browser;
        string rawUrl = "";
        if (base.Request.RawUrl.IndexOf("?") > 0)
        {
            rawUrl = base.Request.RawUrl.Substring(0, base.Request.RawUrl.IndexOf("?"));
        }
        else
        {
            rawUrl = base.Request.RawUrl;
        }
        if (browser.Browser.ToLower().Contains("firefox"))
        {
            str = "css/main-ff.css";
        }
        else if (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple"))
        {
            str = "css/main-ch.css";
        }
        else
        {
            str = "css/main-ie.css";
        }
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("");
        if (rawUrl.Contains("AJAX Pages"))
        {
            builder.AppendLine("<link rel='shortcut icon' href='../Registration/icon.ico' />");
        }
        else
        {
            builder.AppendLine("<link rel='shortcut icon' href='Registration/icon.ico' />");
        }
        builder.AppendLine("<link type='text/css' rel='stylesheet' href='css/intake-sheet-ff.css' />");
        builder.AppendLine("<link href='Css/mainmaster.css' rel='stylesheet' type='text/css' />");
        builder.AppendLine("<link href='Css/UI.css' rel='stylesheet' type='text/css' />");
        builder.AppendLine("<link type='text/css' rel='stylesheet' href='css/style.css' />");
        builder.AppendLine("<link href='css/style.css'  rel='stylesheet' type='text/css'  />");
        builder.AppendLine("<link href='" + str + "' type='text/css' rel='Stylesheet' />");
        this.masterhead.InnerHtml = builder.ToString();
        log.Debug("End Master Page Init method.");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        log.Debug("start Master Page Page_Load()");
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
            string str = this.objAppSettings.getParameterValue("site_logo").ParameterValue;
            this.ImageButton1.ImageUrl = str;
            XmlConfigurator.Configure();
            this.c_objUser = new DAO_User();
            string rawUrl = "";
            if (base.Request.RawUrl.IndexOf("?") > 0)
            {
                rawUrl = base.Request.RawUrl.Substring(0, base.Request.RawUrl.IndexOf("?"));
            }
            else
            {
                rawUrl = base.Request.RawUrl;
            }
          //  this.c_objUser.UserID=rawUrl.Substring(rawUrl.LastIndexOf("/") + 1, (rawUrl.LastIndexOf(".aspx") - rawUrl.LastIndexOf("/")) + 4);
            this.c_objUser.UserID = rawUrl.Substring(rawUrl.LastIndexOf("/") + 1, (rawUrl.LastIndexOf(".aspx") - rawUrl.LastIndexOf("/")) + 4);
            string[] strArray = base.Request.RawUrl.ToString().Split(new char[] { '?' });
            if ((strArray.Length > 1) && strArray[1].Equals("Menuflag=true"))
            {
                if (strArray[0].Contains("/AJAX Pages/Bill_Sys_OutScheduleReport.aspx"))
                {
                    this.c_objUser.UserID = "Bill_Sys_OutScheduleReport.aspx?Menuflag=true";
                }
                else
                {
                    this.c_objUser.UserID = "Bill_Sys_VerificationSent_PrintPOM.aspx";
                }
            }
            if (this.c_objUser.UserID == "Bill_Sys_ReferralBillTransaction.aspx")
            {
                this.c_objUser.UserID="Bill_Sys_BillTransaction.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_ReferringDoctor.aspx")
            {
                this.c_objUser.UserID="Bill_Sys_Doctor.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_ChangePasswordMaster.aspx")
            {
                this.c_objUser.UserID="Bill_Sys_UserMaster.aspx";
            }
            if ((this.c_objUser.UserID == "Bill_Sys_BillSearch.aspx") && (base.Request.QueryString["fromCase"] != null))
            {
                this.c_objUser.UserID="Bill_Sys_Notes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_BillTransaction.aspx")
            {
                this.c_objUser.UserID="Bill_Sys_Notes.aspx";
            }
            if ((this.c_objUser.UserID == "Bill_Sys_NewPaymentReport.aspx") && (base.Request.QueryString["fromCase"] != null))
            {
                this.c_objUser.UserID="Bill_Sys_Notes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_PaymentTransactions.aspx")
            {
                this.c_objUser.UserID="Bill_Sys_Notes.aspx";
            }
            if (this.c_objUser.UserID.Contains("Bill_Sys_IM_"))
            {
                this.c_objUser.UserID="Bill_Sys_IM_HistoryOfPresentIillness.aspx";
            }
            if (this.c_objUser.UserID.Contains("Bill_Sys_FUIM_"))
            {
                this.c_objUser.UserID="Bill_Sys_FUIM_StartExamination.aspx";
            }
            if (this.c_objUser.UserID.Contains("Bill_Sys_AC_"))
            {
                this.c_objUser.UserID="Bill_Sys_AC_AccuReEval.aspx";
            }
            if ((this.c_objUser.UserID == "Bill_Sys_MiscPaymentReport.aspx") && (base.Request.QueryString["fromCase"] != null))
            {
                this.c_objUser.UserID="Bill_Sys_Notes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_Misc_Payment.aspx")
            {
                this.c_objUser.UserID="Bill_Sys_Notes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_Invoice.aspx")
            {
                this.c_objUser.UserID="Bill_Sys_Notes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_PatientBillingSummary.aspx")
            {
                this.c_objUser.UserID="Bill_Sys_Notes.aspx";
            }
            if (this.c_objUser.UserID == "Bill_Sys_Invoice_Report.aspx")
            {
                if (base.Request.QueryString["fromCase"] != "False")
                {
                    this.c_objUser.UserID="Bill_Sys_Notes.aspx";
                }
                else
                {
                    this.c_objUser.UserID="Bill_Sys_BillSearch.aspx";
                }
            }
            if (this.c_objUser.UserID == "TemplateManager.aspx")
            {
                if (base.Request.QueryString["fromCase"] == "true")
                {
                    this.c_objUser.UserID="Bill_Sys_CheckOut.aspx";
                }
                else
                {
                    this.c_objUser.UserID="Bill_Sys_Notes.aspx";
                }
            }
            if (this.c_objUser.UserID == "Bill_Sys_PatientSearch.aspx")
            {
                this.c_objUser.UserID="Bill_Sys_CheckOut.aspx";
            }
            SpecialityPDFDAO ypdfdao = new SpecialityPDFDAO();
            if (base.Session["SPECIALITY_PDF_OBJECT"] != null)
            {
                ypdfdao = (SpecialityPDFDAO)base.Session["SPECIALITY_PDF_OBJECT"];
            }
            else
            {
                ypdfdao = null;
            }
            this.c_objUser.UserRoleID=((Bill_Sys_UserObject)base.Session["USER_OBJECT"]).SZ_USER_ROLE;
            ArrayList list = new ArrayList();
            list.Add(rawUrl);
            new OptionMenu(this.c_objUser).Initialize(this.problue, (Bill_Sys_BillingCompanyObject)base.Session["BILLING_COMPANY_OBJECT"], (Bill_Sys_UserObject)base.Session["USER_OBJECT"], (Bill_Sys_SystemObject)base.Session["SYSTEM_OBJECT"], this.c_objUser, ypdfdao, list);
            this.problue.AllExpanded=true;
            this.problue.AutoPostBack=false;
            if (((Bill_Sys_BillingCompanyObject)base.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                //this.lnkCalendar.HRef = "AJAX Pages/Bill_Sys_AppointPatientEntry.aspx";
            }
            else
            {
                //this.lnkCalendar.HRef = "Bill_Sys_ScheduleEvent.aspx?TOp=true";
            }
            this.ShowAssignedLinks(this.c_objUser.UserRoleID);
            log.Debug("End Master Page_Load()");
            DataSet set = new DataSet();
            set = new Bill_Sys_ProcedureCode_BO().Get_Sys_Key("SS00040", ((Bill_Sys_BillingCompanyObject)base.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0)) && (set.Tables[0].Rows[0][0].ToString() == "0"))
            {
                this.lnkshedulevisits.Visible = false;
            }
        }
        catch (Exception exception)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + exception.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + exception.StackTrace.ToString());
            if (exception.InnerException != null)
            {
                log.Debug("Shared_MasterPage. Method - Page_Load : " + exception.InnerException.Message.ToString());
                log.Debug("Shared_MasterPage. Method - Page_Load : " + exception.InnerException.StackTrace.ToString());
            }
            Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
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

    private void ShowAssignedLinks(string userroleid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        OptionMenuDataAccess access = new OptionMenuDataAccess();
        try
        {
            foreach (DataRow row in access.GetAssignedMenu(userroleid).Rows)
            {
                ((Bill_Sys_UserObject)base.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower();
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
                            //this.lnkCalendar.Visible = false;
                           this.lnkScheduleReport.Visible = true;
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
                this.lnkHome.Visible = false;
            }
        }
        catch (Exception exception)
        {
            log.Debug("Shared_MasterPage. Method - ShowAssignedLinks : " + exception.Message.ToString());
            log.Debug("Shared_MasterPage. Method - ShowAssignedLinks : " + exception.StackTrace.ToString());
            if (exception.InnerException != null)
            {
                log.Debug("Shared_MasterPage. Method - ShowAssignedLinks : " + exception.InnerException.Message.ToString());
                log.Debug("Shared_MasterPage. Method - ShowAssignedLinks : " + exception.InnerException.StackTrace.ToString());
            }
            Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
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

    public void reportingTokenGeneration(Object sender, EventArgs e)
    {
        ReportingDBIntegraion report = new ReportingDBIntegraion();
        string userId = ((Bill_Sys_UserObject)base.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
        String tokenGenareted = report.TokenGenration(userId);
        reportingIntegration.PostBackUrl = "https://reporting.gogreenbills.com//frmLogin.aspx?token=" + tokenGenareted;
        Response.Redirect("https://reporting.gogreenbills.com//frmLogin.aspx?token=" + tokenGenareted);
    }

}
