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
        HttpCookie loggedout = new HttpCookie("loggedout", "1");
        loggedout.Expires = DateTime.Now.AddYears(1);
        Response.Cookies.Add(loggedout);

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

        //if (Request.Cookies["loggedout"] != null)
        //{
        //    HttpCookie loggedout = new HttpCookie("loggedout", "0");
        //    loggedout.Expires = DateTime.Now.AddYears(1);
        //    Response.Cookies.Add(loggedout);
        //}


        FormsAuthentication.SignOut();
        //FormsAuthentication.RedirectToLoginPage();
        Response.Redirect("Bill_Sys_Login.aspx", false);

    }
}
