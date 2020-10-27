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

public partial class AJAX_Pages_Bill_Sys_Missing_Lit_Info_Desk : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid = grdMissingInfoDesk;
        this.txtSearchBox.SourceGrid = grdMissingInfoDesk;
        this.grdMissingInfoDesk.Page = this.Page;
        this.grdMissingInfoDesk.PageNumberList = this.con;
        if (!Page.IsPostBack)
        {
            //tblSrch.Visible = false;
        }
        
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdMissingInfoDesk.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
    protected void btnSearch_onclick(object sender, EventArgs e)
    {
        //tblSrch.Visible = true;
        txtIndex.Text = ddlIndex.Text;
        txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();
        grdMissingInfoDesk.XGridBindSearch();
    }
}
