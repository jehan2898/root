using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_Bill_Sys_HPJ1_NotaryPublic_Signature : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EventId"] != null)
        {
            Session["EventID"] = Request.QueryString["EventId"].ToString();
        }
        if (Request.QueryString["billNo"] != null)
        {
            Session["BillNumber"] = Request.QueryString["billNo"].ToString();
        }
        if (Request.QueryString["SignDt"] != null)
        {
            Session["SignDt"] = Request.QueryString["SignDt"].ToString();
        }
    }
}