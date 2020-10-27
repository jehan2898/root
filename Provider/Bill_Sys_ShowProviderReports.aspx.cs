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
using mbs.reports;

public partial class AJAX_Pages_Bill_Sys_ShowProviderReports : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            OfficeReport report = new OfficeReport();
            DatabaseConfig.ConnectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();// "Data Source=SERVER\\SQLEXPRESS2008;Initial Catalog=BillingSystemGreenBillsPrepod;User ID=tusharb;password=tusharb";
            //collect parameters 
            report.OfficeId = Request.QueryString["ofcid"];
            report.CompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            report.User = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            report.StartDate = DateTime.Parse(Session["ReportFromDate"].ToString());
            report.EndDate = DateTime.Parse(Session["ReportToDate"].ToString());
            byte[] PdfReport = report.RenderReport();
            if (report.Rendering == true)
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-length", PdfReport.Length.ToString());
                Response.AppendHeader("Content-Disposition", "inline;filename=" + report.Status);
                Response.OutputStream.Write(PdfReport, 0, PdfReport.Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.End();
                PdfReport = null;
                report = null;
            }
            else
            {
                throw new Exception(report.Status);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
