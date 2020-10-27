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

public partial class AJAX_Pages_Bill_Sys_BillCount : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid = grdBillCount;
        this.txtSearchBox.SourceGrid = grdBillCount;
        this.grdBillCount.Page = this.Page;
        this.grdBillCount.PageNumberList = this.con;

        if (!IsPostBack)
        {
            ddlBilldate.Attributes.Add("onChange", "javascript:SetDate();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();

            grdBillCount.XGridBindSearch();

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grdBillCount.XGridBindSearch();
    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdBillCount.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
}
