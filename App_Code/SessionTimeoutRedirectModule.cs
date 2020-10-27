using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionTimeoutRedirectModule
/// </summary>
public class SessionTimeoutRedirectModule:IHttpModule
{
   
    
    public SessionTimeoutRedirectModule()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void Init(HttpApplication app)
    {
        app.PreRequestHandlerExecute += new EventHandler(OnBeginRequest);
    }
    public void OnBeginRequest(Object s, EventArgs e)
    {
        HttpApplication app = s as HttpApplication;
        if (app.Context.Session != null)
        {
            if (app.Context.Session.IsNewSession)
            {
                HttpCookie newSessionIdCookie =app.Context.Request.Cookies["ASP.NET_SessionId"];
                if (newSessionIdCookie != null)
                {
                    string newSessionIdCookieValue = newSessionIdCookie.Value;
                    if (newSessionIdCookieValue != string.Empty)
                    {
                        // This means Session was timed Out and New Session was started
                        if (!app.Context.Request.Url.ToString().ToUpper().Contains("BILL_SYS_LOGIN.ASPX"))
                        {

                            app.Context.Response.Redirect("/Bill_Sys_Login.aspx");
                        }
                    }
                }
            }
        }

    }

    public void Dispose() { }
}