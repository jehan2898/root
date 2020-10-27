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

public partial class AJAX_Pages_Bill_Sys_Upadte_Ins : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string szCaseId = Request.QueryString["CaseID"].ToString();
            string szEventID = Request.QueryString["EventID"].ToString();
            TextBox txtCaseIDIns = (TextBox)inscnt.FindControl("txtCaseID");
            TextBox txtEventID = (TextBox)inscnt.FindControl("txtEventID");
            
            txtCaseIDIns.Text = szCaseId;
            txtEventID.Text = szEventID;

        }

    }
}
