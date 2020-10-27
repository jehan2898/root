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
using mbs.ApplicationSettings;

public partial class Agent_MasterPage_Agent : System.Web.UI.MasterPage
{
    private DAO_User c_objUser = null;
    private static ILog log = LogManager.GetLogger("Shared_MasterPage");
    private mbs.ApplicationSettings.ApplicationSettings_BO objAppSettings = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        log.Debug("Start Master Page Init");
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string sType = browser.Type;
        string sName = browser.Browser;
        string szCSS;
        string _url = "";
        if (Request.RawUrl.IndexOf("?") > 0)
        {
            _url = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("?"));
        }
        else
        {
            _url = Request.RawUrl;
        }
        if (browser.Browser.ToLower().Contains("firefox"))
        {
            szCSS = "css/main-ff.css";
        }
        else
        {
            if (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple"))
            {
                szCSS = "css/main-ch.css";
            }
            else
            {
                szCSS = "css/main-ie.css";
            }
        }
        System.Text.StringBuilder b = new System.Text.StringBuilder();
        b.AppendLine("");
        if (_url.Contains("AJAX Pages")) { b.AppendLine("<link rel='shortcut icon' href='../Registration/icon.ico' />"); } else b.AppendLine("<link rel='shortcut icon' href='Registration/icon.ico' />");
        b.AppendLine("<link type='text/css' rel='stylesheet' href='css/intake-sheet-ff.css' />");
        b.AppendLine("<link href='Css/mainmaster.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link href='Css/UI.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link type='text/css' rel='stylesheet' href='css/style.css' />");
        b.AppendLine("<link href='css/style.css'  rel='stylesheet' type='text/css'  />");
        b.AppendLine("<link href='" + szCSS + "' type='text/css' rel='Stylesheet' />");
        this.masterhead.InnerHtml = b.ToString();
        log.Debug("End Master Page Init method.");
    }

    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
 

        log.Debug("start Master Page Page_Load()");
        if (Application["Error"] != null) Response.Write(Application["Error"].ToString());
        try
        {
            objAppSettings = (mbs.ApplicationSettings.ApplicationSettings_BO)Application["OBJECT_APP_SETTINGS"];
            if (objAppSettings == null)
            {
                objAppSettings = new mbs.ApplicationSettings.ApplicationSettings_BO();
                Application["OBJECT_APP_SETTINGS"] = objAppSettings;
            }
            string sitePath = ((mbs.ApplicationSettings.ApplicationSettings_DO)objAppSettings.getParameterValue(mbs.ApplicationSettings.ApplicationSettings_BO.KEY_SITE_PATH)).ParameterValue;
            ImageButton1.ImageUrl = sitePath;
            log4net.Config.XmlConfigurator.Configure();
            //if (!IsPostBack)
            //{
            c_objUser = new DAO_User();
            string _url = "";
            if (Request.RawUrl.IndexOf("?") > 0)
            {
                _url = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("?"));
            }
            else
            {
                _url = Request.RawUrl;
            }
            c_objUser.UserID = _url.Substring(_url.LastIndexOf("/") + 1, (_url.LastIndexOf(".aspx") - _url.LastIndexOf("/")) + 4);
            //if (c_objUser.UserID == "Bill_Sys_BillingDoctor.aspx")
            //{
            //    c_objUser.UserID = "Bill_Sys_Doctor.aspx";
            //}

            //if (_url.Contains("AJAX Pages")) { A1.Attributes.Add("onclick", "javascript:OpenAjaxTicket();"); } else A1.Attributes.Add("onclick", "javascript:OpenTicket();");

            string outschedule = Request.RawUrl.ToString();
            string[] arroutschedule = outschedule.Split('?');

            if (arroutschedule.Length > 1)
            {
                if (arroutschedule[1].Equals("Menuflag=true"))
                {
                    //TUSHAR:-There Are Two I_PARENT_ID FOR Bill_Sys_OutScheduleReport.aspx So I Have To Check it With Menu Link.
                    if (arroutschedule[0].Contains("/AJAX Pages/Bill_Sys_OutScheduleReport.aspx"))
                    {
                        c_objUser.UserID = "Bill_Sys_OutScheduleReport.aspx?Menuflag=true";
                    }
                    else
                    {
                        c_objUser.UserID = "Bill_Sys_VerificationSent_PrintPOM.aspx";
                    }
                    //End Code
                }
            }
            if (c_objUser.UserID == "Bill_Sys_ReferralBillTransaction.aspx")
            {
                c_objUser.UserID = "Bill_Sys_BillTransaction.aspx";
            }
            if (c_objUser.UserID == "Bill_Sys_ReferringDoctor.aspx")
            {
                c_objUser.UserID = "Bill_Sys_Doctor.aspx";
            }
            if (c_objUser.UserID == "Bill_Sys_ChangePasswordMaster.aspx")
            {
                c_objUser.UserID = "Bill_Sys_UserMaster.aspx";
            }
            if (c_objUser.UserID == "Bill_Sys_BillSearch.aspx")
            {
                if (Request.QueryString["fromCase"] != null)
                {
                    c_objUser.UserID = "Bill_Sys_Notes.aspx";
                }
            }
            if (c_objUser.UserID == "Bill_Sys_BillTransaction.aspx")
            {
                c_objUser.UserID = "Bill_Sys_Notes.aspx";
            }

            if (c_objUser.UserID == "Bill_Sys_NewPaymentReport.aspx")
            {
                if (Request.QueryString["fromCase"] != null)
                {
                    c_objUser.UserID = "Bill_Sys_Notes.aspx";
                }
            }
            if (c_objUser.UserID == "Bill_Sys_PaymentTransactions.aspx")
            {
                c_objUser.UserID = "Bill_Sys_Notes.aspx";
            }

            if (c_objUser.UserID.Contains("Bill_Sys_IM_"))
            {
                c_objUser.UserID = "Bill_Sys_IM_HistoryOfPresentIillness.aspx";
            }

            if (c_objUser.UserID.Contains("Bill_Sys_FUIM_"))
            {
                c_objUser.UserID = "Bill_Sys_FUIM_StartExamination.aspx";
            }

            if (c_objUser.UserID.Contains("Bill_Sys_AC_"))
            {
                c_objUser.UserID = "Bill_Sys_AC_AccuReEval.aspx";
            }

            if (c_objUser.UserID == "Bill_Sys_MiscPaymentReport.aspx")
            {
                if (Request.QueryString["fromCase"] != null)
                {
                    c_objUser.UserID = "Bill_Sys_Notes.aspx";
                }
            }
            if (c_objUser.UserID == "Bill_Sys_Misc_Payment.aspx")
            {
                c_objUser.UserID = "Bill_Sys_Notes.aspx";
            }

            if (c_objUser.UserID == "Bill_Sys_Invoice.aspx")
            {
                c_objUser.UserID = "Bill_Sys_Notes.aspx";
            }
            if (c_objUser.UserID == "Bill_Sys_PatientBillingSummary.aspx")
            {
                c_objUser.UserID = "Bill_Sys_Notes.aspx";
            }
            if (c_objUser.UserID == "Bill_Sys_Invoice_Report.aspx")
            {
                if (Request.QueryString["fromCase"] != "False")
                {
                    c_objUser.UserID = "Bill_Sys_Notes.aspx";
                }
                else
                {
                    c_objUser.UserID = "Bill_Sys_BillSearch.aspx";
                }
            }
            if (c_objUser.UserID == "TemplateManager.aspx")
            {
                if (Request.QueryString["fromCase"] == "true")
                {
                    c_objUser.UserID = "Bill_Sys_CheckOut.aspx";
                }
                else
                {
                    c_objUser.UserID = "Bill_Sys_Notes.aspx";
                }
            }
            if (c_objUser.UserID == "Bill_Sys_PatientSearch.aspx")
            {
                c_objUser.UserID = "Bill_Sys_CheckOut.aspx";
            }


            SpecialityPDFDAO _objSpecialityDAO = new SpecialityPDFDAO();
            if (Session["SPECIALITY_PDF_OBJECT"] != null)
                _objSpecialityDAO = (SpecialityPDFDAO)Session["SPECIALITY_PDF_OBJECT"];
            else
            {
                _objSpecialityDAO = null;
            }

            c_objUser.UserRoleID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;

            // Pass new parameter to objMenu.Initialize as ArrayList which will be useful while display left 
            // hand side menu.

            ArrayList objHelper = new ArrayList();
            objHelper.Add(_url); // url of page

            OptionMenu objMenu = new OptionMenu(c_objUser);
            objMenu.Initialize(problue, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]), ((Bill_Sys_UserObject)Session["USER_OBJECT"]), ((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]), c_objUser, _objSpecialityDAO, objHelper);
            problue.AllExpanded = true;
            problue.AutoPostBack = false;




            //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            //{
            //    if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower() == "agent")
            //        lnkScheduleReport.HRef = "Bill_Sys_AppointPatientEntry_Agent.aspx";//"Agent/Bill_Sys_AppointPatientEntry_Agent.aspx";
            //    else
            //        lnkScheduleReport.HRef = "AJAX Pages/Bill_Sys_AppointPatientEntry.aspx";
            //}
            //else
            //{
            //    lnkScheduleReport.HRef = "Bill_Sys_ScheduleEvent.aspx?TOp=true";
            //}

            ShowAssignedLinks(c_objUser.UserRoleID);
            log.Debug("End Master Page_Load()");
            DataSet dsBit = new System.Data.DataSet();
            Bill_Sys_ProcedureCode_BO objPBO = new Bill_Sys_ProcedureCode_BO();
            dsBit = objPBO.Get_Sys_Key("SS00040", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (dsBit.Tables.Count > 0 && dsBit.Tables[0].Rows.Count > 0)
            {
                string szBitVal = dsBit.Tables[0].Rows[0][0].ToString();
                if (szBitVal == "0")
                {
                    //lnkshedulevisits.Visible = false;

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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void ShowAssignedLinks(string userroleid)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        OptionMenuDataAccess _optMenuDataAccess = new OptionMenuDataAccess();
        try
        {
            DataTable dt = _optMenuDataAccess.GetAssignedMenu(userroleid);
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr[0].ToString())
                {
                    case "Search":
                        if (((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ROLE_NAME.ToLower() != "doctor")
                        {
                            lnkHome.Visible = true;
                        }
                        else
                        {
                            // lnkHome.Visible = false;
                        }
                        break;
                    case "Desk":
                        //lnkDesks.Visible = true;
                        break;
                    case "Data Entry":
                        lnkDataEntry.Visible = true;
                        break;
                    case "Calendar":
                        lnkScheduleReport.Visible = true;
                        break;
                    case "Quick Bill Entry":
                        //lnkQuickSearch.Visible = true;
                        break;
                    case "Reports":
                        //lnkReports.Visible = true;
                        break;
                    //...ashutosh
                    case "Doctor Screen":
                        //A2.Visible = false;
                        break;
                    //...     
                    //...Tushar
                    case "Billing":
                        //lnkBillingReport.Visible = true;
                        break;
                    //...            
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
