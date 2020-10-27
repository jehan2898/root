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

public partial class AJAX_Pages_Report_pom_popup : PageBase
{
    string pomid;
    string companyid;

    protected void Page_Init(object sender, EventArgs e)
    {
       // DivStatus2.Visible = true;
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
        this.framehead.InnerHtml = b.ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string selecttedrdb = Request.QueryString["selectedvalue"].ToString();
        if (selecttedrdb == "0")
        {
            if (Request.QueryString["PomID"] != null)
            {
                pomid = Request.QueryString["PomID"].ToString();

                companyid = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Bill_Sys_ReportBO _objReport = new Bill_Sys_ReportBO();
                DataSet dsBillView = _objReport.GetBillDetails(pomid, companyid);
                grdPaymentTransaction.DataSource = dsBillView;
                grdPaymentTransaction.DataBind();
                //ModalPopupExtender1.Show();


            }
        }
        else
        {

            if (Request.QueryString["PomID"] != null)
            {
                pomid = Request.QueryString["PomID"].ToString();
                companyid = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Bill_Sys_ReportBO _objReport = new Bill_Sys_ReportBO();
                DataSet dsBillView = _objReport.GetBillDetailsOther(pomid, companyid);
                grdPaymentTransaction.DataSource = dsBillView;
                grdPaymentTransaction.DataBind();
                //ModalPopupExtender1.Show();


            }
        }
    }
}
