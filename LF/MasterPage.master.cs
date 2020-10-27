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

public partial class MasterPage : System.Web.UI.MasterPage
{
    private Bill_Sys_BillingCompanyObject obj = null;
    public string szCSS;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string sType = browser.Type;
        string sName = browser.Browser;

        if (browser.Browser.ToLower().Contains("firefox"))
        {
           szCSS = "css/main-ff.css";
            //szCSS = "css/main-ie.css";
        }
        else
        {
            if (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple"))
            {
                szCSS = "css/main-ch.css";
               // szCSS = "css/main-ie.css";
            }
            else
            {
                szCSS = "css/main-ie.css";
            }
        }

        System.Text.StringBuilder b = new System.Text.StringBuilder();

        b.AppendLine("<script type='text/javascript' src='js/CustomControls.ConfirmMessageJS.js'></script>");
        b.AppendLine("<script type='text/javascript' src='js/CustomControls.ContextMenuJS.js'></script>");
        b.AppendLine("<script type='text/javascript' src='js/CustomControls.GridViewJS.js'></script>");
        b.AppendLine("<script type='text/javascript' src='nitobi.callout/nitobi.toolkit.js'></script>");
        b.AppendLine("<script type='text/javascript' src='nitobi.callout/nitobi.callout.js'></script>");
        b.AppendLine("<link href='css/css.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link href='nitobi.callout/nitobi.callout.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link href='css/style.css'  rel='stylesheet' type='text/css'  />");
        b.AppendLine("<link href='" + szCSS + "' type='text/css' rel='Stylesheet' />");
        this.masterhead.InnerHtml = b.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //divViewBill.Visible = false;
       
       
        obj = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
        ajAutoComplete1.ContextKey = obj.SZ_COMPANY_ID;
        ajAutoComplete1.CompletionListElementID = divAutoComp.ClientID;

        //if (!this.IsPostBack)
        //{
        //    System.Web.HttpBrowserCapabilities browser = Request.Browser;
        //    string sType = browser.Type;
        //    string sName = browser.Browser;

        //    if (browser.Browser.ToLower().Contains("firefox"))
        //    {
        //        szCSS = "\"css/main-ff.css\"";
        //    }
        //    else
        //    {
        //        if (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple"))
        //        {
        //            szCSS = "\"css/main-ch.css\"";
        //        }
        //        else
        //        {
        //            szCSS = "\"css/main-ie.css\"";
        //        }
        //    }
        //}
          
        string szUrl= Request.Path.Substring(Request.Path.LastIndexOf('/')).ToString();

        if (szUrl.Equals("/WorkAreaWidget.aspx") || szUrl.Equals("\\WorkAreaWidget.aspx"))
        {
            tdViewBill.Visible = true;
            tdViewBill1.Visible = true;
            tdWorkArea.Visible = false;
          
        }

        else if (szUrl.Equals("/LF_ViewBill.aspx") || szUrl.Equals("\\LF_ViewBill.aspx"))
         {
           tdWorkArea.Visible = true;
             tdViewBill.Visible = false;
            tdViewBill1.Visible = false;
        }
        else
        {
            tdViewBill.Visible = false;
            tdViewBill1.Visible = false;
            tdWorkArea.Visible = false;

        }
    }
   
    protected void btnGo_Click(object sender, EventArgs e)
    {
        Session["PatientName"] = txtAUSearch.Text;
        Response.Redirect("Bill_Sys_SearchCase.aspx");
    }

    protected void txtAUSearch_TextChanged(object sender, EventArgs e)
    {
      
    }
}
