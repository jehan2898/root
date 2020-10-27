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
using System.Data.SqlClient;

public partial class AJAX_Pages_Bill_Sys_ViewNotes : PageBase
{
    string caseId = "";
    Bill_Sys_NotesBO objnotes = new Bill_Sys_NotesBO();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            caseId = Request.QueryString["CaseID"].ToString();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            DataSet dsnotes = new DataSet();
            dsnotes=objnotes.GetNotesDetails(caseId, txtCompanyID.Text);
            grdViewNotes.DataSource = dsnotes;
            grdViewNotes.DataBind();
        }

    }
}
