using DevExpress.Web;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class AJAX_Pages_Bill_sys_openVerificationDocs : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = "";
        int num = 0;
        if (base.Request.QueryString["bno"] != null || base.Request.QueryString["doctype"] != null)
        {
            str = base.Request.QueryString["bno"].ToString();
            num = Convert.ToInt32(base.Request.QueryString["doctype"].ToString());
        }
        DataSet billSearchDocuments = (new Bill_Sys_BillSearchDocuments()).GET_BillSearch_Documents(str, num);
        this.grddocumnetmanager.DataSource = billSearchDocuments;
        this.grddocumnetmanager.DataBind();
    }
}