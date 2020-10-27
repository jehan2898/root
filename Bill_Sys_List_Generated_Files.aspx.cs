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

public partial class Bill_Sys_List_Generated_Files : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ArrayList obj = new ArrayList();

        obj = (ArrayList)Session["ListOfFiles"];

        for (int i = 0; i < obj.Count; i++)
        {
            divGeneratedFiles.InnerHtml = divGeneratedFiles.InnerHtml + "<br><br> <a href='" + obj[i].ToString() + "'>" + "   " + obj[i].ToString() + "</a>";
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_List_Generated_Files.aspx");
        }
        #endregion
    }
}
