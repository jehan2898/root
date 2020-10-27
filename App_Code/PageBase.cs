using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

/// <summary>
/// Summary description for PageBase
/// </summary>
public class PageBase : System.Web.UI.Page
{
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (HttpContext.Current.Session != null)
        {
            if (Context.Request.Cookies["sessionCookies"] == null)
            {
                HttpCookie sessionCookies = new HttpCookie("sessionCookies");
                sessionCookies.Value = "1";
                sessionCookies.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(sessionCookies);
            }
        }
        else
        {
            HttpCookie sessionCookies = new HttpCookie("sessionCookies");
            sessionCookies.Value = "0";
            sessionCookies.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(sessionCookies);
            Response.Cookies["sessionCookies"].Expires = DateTime.Now.AddDays(-1);
            string LogOutURL = ConfigurationManager.AppSettings["LogOutURL"].ToString();
            Response.Redirect(LogOutURL);
        }
        //AutoRedirect();
    }

    public void AutoRedirect()
    {
        //string LogOutURL = ConfigurationManager.AppSettings["LogOutURL"].ToString();
        int int_MilliSecondsTimeOut = (Session.Timeout * 60000);

        string str_Script = @"

   <script type='text/javascript'> 

        function readCookie(name) {
            var nameEQ = escape(name) + '=';
            var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++)
        {
            var c = ca[i];
            while (c.charAt(0) === ' ')
                c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) === 0)
                return unescape(c.substring(nameEQ.length, c.length));
        }
        return null;
    }

    intervalset = window.setInterval('Redirect()'," + int_MilliSecondsTimeOut.ToString() + @");

    function Redirect()
    {
            if (readCookie('sessionCookies') == 0) {
            alert('Your session has been expired and system redirects to login page now.!\n\n');
            window.location.href='http://cadev.zapto.org:5002/Bill_Sys_Logout.aspx'; 
            }
       else
        {
        intervalset = window.setInterval('Redirect()'," + int_MilliSecondsTimeOut.ToString() + @");
        }       
    }

</script>";

        ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", str_Script);

    }

}