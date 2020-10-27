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

public partial class AJAX_Pages_Bill_Sys_ViewDocPopUp : PageBase
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
            txtEventID.Text = Request.QueryString["Eve"].ToString();
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
        Bill_Sys_Upload_VisitReport objDelete = new Bill_Sys_Upload_VisitReport();
        string szOutPut = "";
        for (int i = 0; i < grdViewDoc.Rows.Count; i++)
        {
            CheckBox grdChk = (CheckBox)grdViewDoc.Rows[i].FindControl("ChkDelete");
            if (grdChk.Checked)
            {
                string szImageId = grdViewDoc.DataKeys[i]["I_IMAGE_ID"].ToString();
                string msg = "Delete-Visit-Document File " + grdViewDoc.DataKeys[i]["File_Name"].ToString() + " is deleted by user " + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME + "on " + DateTime.Now.ToString("MM/dd/yyyy") + " with Image ID " + szImageId;
                string szResult = objDelete.DeleteFile(txtCompnyID.Text, szImageId, "F", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, msg, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                if (szResult == "YES")
                {
                    if (szOutPut == "")
                    {
                        szOutPut = grdViewDoc.DataKeys[i]["File_Name"].ToString() + "is delete";
                    }
                    else
                    {
                        szOutPut = "\\n" + grdViewDoc.DataKeys[i]["File_Name"].ToString() + "is delete";
                    }
                    #region Activity_Log
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "DOC_DELETED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Document Id  : " + grdViewDoc.DataKeys[i]["I_IMAGE_ID"].ToString() + " Deleted . ";
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    this._DAO_NOTES_EO.SZ_CASE_ID = this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    #endregion
                }
                else if (szResult == "NO")
                {
                    if (szOutPut == "")
                    {
                        szOutPut = grdViewDoc.DataKeys[i]["File_Name"].ToString() + "is not delete";
                    }
                    else
                    {
                        szOutPut = "\\n" + grdViewDoc.DataKeys[i]["File_Name"].ToString() + "is not delete";
                    }
                }


            }
        }
        if (szOutPut != "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('" + szOutPut + "');</script>");
            grdViewDoc.XGridBindSearch();
        }
    }
}
