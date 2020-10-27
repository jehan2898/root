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
using System.IO;
using mbs.templatemanager;

public partial class AJAX_Pages_TemplateManager : PageBase
{
    private Bill_Sys_BillingCompanyObject objCommany;

    protected void Page_Load(object sender, EventArgs e)
    {
        objCommany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                Session["DiagnosysCode"] = "Null";
                mbs.templatemanager.TemplateManager tm = new mbs.templatemanager.TemplateManager(ConfigurationSettings.AppSettings["Connection_String"].ToString());
                DataSet ds = tm.GetTemplatesToRun(objCommany.SZ_COMPANY_ID, "OpenTemplate");
                lstTemplates.DataSource = ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListItem lst = new ListItem();
                    lst.Text = ds.Tables[0].Rows[i][1].ToString();
                    lst.Value = ds.Tables[0].Rows[i][2].ToString();
                    lstTemplates.Items.Add(lst);
                }
                if (Request.QueryString["fromCase"] != null)
                {
                    Bill_Sys_CaseObject _Bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                    _Bill_Sys_CaseObject.SZ_CASE_ID = Request.QueryString["caseId"].ToString();
                    _Bill_Sys_CaseObject.SZ_CASE_NO = Request.QueryString["CaseNo"].ToString();
                    _Bill_Sys_CaseObject.SZ_PATIENT_ID = Request.QueryString["PId"].ToString();
                    _Bill_Sys_CaseObject.SZ_PATIENT_NAME = Request.QueryString["PName"].ToString();
                    Session["CASE_OBJECT"] = _Bill_Sys_CaseObject;
                }
            }
        }
        if (Request.Browser.Browser == "IE")
        {

        }
        else
        {
            lblError.Text = "You are using " + Request.Browser.Browser + " browser. Template manager requires Internet Explorer or another browser which supports ActiveX controls.";
        }
    }
    protected void btnOpenTemplate_Click(object sender, EventArgs e)
    {
        if (Session["rtffile"] != null)
        {
            Session["rtffile"] = null;
        }
        Session["DiagnosysCode"] = "Null";       
    }
    protected void lstTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["rtffileName"] = lstTemplates.SelectedItem.Text;
    }
}
