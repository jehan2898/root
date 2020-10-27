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

public partial class Security_Help : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlControl frame1 = new System.Web.UI.HtmlControls.HtmlGenericControl("iframe");
        frame1 = (HtmlControl)this.FindControl("MyFrame");
        if (Session["HelpFile"] == null)
        {
            frame1.Attributes["src"] = "../Help/WelcomeHelp.htm";
        }
        else
        {
            frame1.Attributes["src"] = Session["HelpFile"].ToString();
        }
        frame1.Attributes["frameborder"] = "1";
        frame1.Attributes["scrolling"] = "auto";
    }
}
