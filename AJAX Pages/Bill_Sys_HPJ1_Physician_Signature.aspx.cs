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

public partial class AJAX_Pages_Bill_Sys_HPJ1_Physician_Signature : PageBase
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