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

public partial class Bill_Sys_Logout : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["UserID"] != null)
        {
            Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
        }

       if (Request.Cookies["UserName"] != null)
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
        }
        if (Request.Cookies["DefaultURL"] != null)
        {
            Response.Cookies["DefaultURL"].Expires = DateTime.Now.AddDays(-1);
        } 

        Session["UserID"] = null;
        Session["UserName"] = null;
        Session["DefaultURL"] = null;
        Session["USER_OBJECT"] = null;
        Session["BILLING_COMPANY_OBJECT"] = null;
        Session.Abandon();
        FormsAuthentication.SignOut();
        //FormsAuthentication.RedirectToLoginPage();
        Response.Redirect("index.aspx", false);

    }
}
