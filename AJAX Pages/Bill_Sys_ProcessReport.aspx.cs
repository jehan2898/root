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

public partial class AJAX_Pages_Bill_Sys_ProcessReport : PageBase
{

    protected void btnExportToExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdImportVisit.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.grdImportVisit.XGridBindSearch();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid=this.grdImportVisit;
        this.txtSearchBox.SourceGrid=this.grdImportVisit;
        this.grdImportVisit.Page=this.Page;
        this.grdImportVisit.PageNumberList=this.con;
        this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        this.ddlImportDateValue.Attributes.Add("onChange", "javascript:SetImportDate();");
        if (!base.IsPostBack)
        {
            this.txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlLocation.Flag_ID=this.txtCompanyId.Text;
            this.extddlCaseType.Flag_ID=this.txtCompanyId.Text;
            this.grdImportVisit.XGridBindSearch();
        }
    }
}
