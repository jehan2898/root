using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_PopupWC42 : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdlCasID.Value = Request.QueryString["CaseId"].ToString();
            hdnPatientID.Value = Request.QueryString["PatientID"].ToString();
            txtPatientID.Text = Request.QueryString["PatientID"].ToString();
        }
    }
}