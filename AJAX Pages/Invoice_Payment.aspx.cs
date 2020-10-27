using AjaxControlToolkit;
using DevExpress.Web;
using ExtendedDropDownList;
using Ionic.Zip;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XGridPagination;
using XGridSearchTextBox;
using XGridView;

public partial class AJAX_Pages_Invoice_Payment : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        con.SourceGrid = this.grdInvoiceSearch;
        txtSearchBox.SourceGrid = this.grdInvoiceSearch;
        grdInvoiceSearch.Page = this.Page;
        grdInvoiceSearch.PageNumberList = this.con;
     
        if (!IsPostBack)
        {
            txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            ajAutoEmp.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            grdInvoiceSearch.XGridBindSearch();
            

        }


        
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdInvoiceSearch.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            con.SourceGrid = this.grdInvoiceSearch;
            txtSearchBox.SourceGrid = this.grdInvoiceSearch;
            grdInvoiceSearch.Page = this.Page;
            grdInvoiceSearch.PageNumberList = this.con;
             grdInvoiceSearch.XGridBindSearch();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    
}