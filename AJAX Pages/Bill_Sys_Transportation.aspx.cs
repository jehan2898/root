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

public partial class AJAX_Pages_Bill_Sys_Transportation : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        this.con.SourceGrid = grdTransport;
        this.txtSearchBox.SourceGrid = grdTransport;
        this.grdTransport.Page = this.Page;
        this.grdTransport.PageNumberList = this.con;

        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");

        if (!IsPostBack)
        {
            txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlTransport.Flag_ID = txtCompanyId.Text;
            grdTransport.XGridBindSearch();

        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grdTransport.XGridBindSearch();
    }
    protected void btnExportToExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdTransport.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
}
