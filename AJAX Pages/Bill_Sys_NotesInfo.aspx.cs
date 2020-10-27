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

public partial class AJAX_Pages_Bill_Sys_NotesInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        exddluserlist.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.con.SourceGrid = grdUserlist;
        this.txtSearchBox.SourceGrid = grdUserlist;
        this.grdUserlist.Page = this.Page;
        this.grdUserlist.PageNumberList = this.con;
        if (!IsPostBack)
        {
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate1();");
            grdUserlist.XGridBindSearch();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION) != "1")
            {
                grdUserlist.Columns[9].Visible = false;
            }
            else
            {
                grdUserlist.Columns[9].Visible = true;
            }

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grdUserlist.XGridBindSearch();
    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdUserlist.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);


    }
}
