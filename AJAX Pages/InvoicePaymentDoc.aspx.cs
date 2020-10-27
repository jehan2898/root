using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InvoicePaymentDoc : System.Web.UI.Page
{
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid = grdViewDoc;
        this.txtSearchBox.SourceGrid = grdViewDoc;
        this.grdViewDoc.Page = this.Page;
        this.grdViewDoc.PageNumberList = this.con;
        if (!Page.IsPostBack)
        {
            txtinvoiceid.Text = Request.QueryString["invoiceid"].ToString();
            txtpaymetID.Text = Request.QueryString["paymentid"].ToString();
            txtCompnyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            grdViewDoc.XGridBindSearch();
        }
        btnDelete.Attributes.Add("onclick", "return confirm_delete();");

    }
    protected void Page_Init(object sender, EventArgs e)
    {

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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        EmployerBO objDelete = new EmployerBO();
        string szOutPut = "";
        ArrayList arrlst =new  ArrayList();
        for (int i = 0; i < grdViewDoc.Rows.Count; i++)
        {
            CheckBox grdChk = (CheckBox)grdViewDoc.Rows[i].FindControl("ChkDelete");
            if (grdChk.Checked)
            {
                string szImageId = grdViewDoc.DataKeys[i]["I_IMAGE_ID"].ToString();
                arrlst.Add(szImageId);
            }
        }
        string rRetrun = objDelete.DeletePaymetDocs(arrlst,txtpaymetID.Text,txtCompnyID.Text,txtinvoiceid.Text);
        if (rRetrun == "deleted")
        {

            this.usrMessage1.PutMessage("Payment Image(s) successfully ...");
            this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage1.Show();
        }
        else
        {

            this.usrMessage1.PutMessage("Error "+ rRetrun);
            this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            this.usrMessage1.Show();
        }


    }
}