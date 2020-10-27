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


public partial class AJAX_Pages_Bill_Sys_PatientBillingSummary : PageBase
{
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.txtBillStatusID.Text = this.extddlBillStatus.Text;
        this.txtOfficeId.Text = this.extddlOffice.Text;
        this.txtGroupId.Text = this.extddlSpeciality.Text;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.con.SourceGrid=this.grdBillSearch;
            this.txtSearchBox.SourceGrid=this.grdBillSearch;
            this.grdBillSearch.Page=this.Page;
            this.grdBillSearch.PageNumberList=this.con;
            this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            if (!this.Page.IsPostBack)
            {
                this.extddlBillStatus.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlSpeciality.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlOffice.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.txtCaseId.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            }
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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.grdBillSearch.XGridDatasetNumber=2;
        this.grdBillSearch.XGridBindSearch();
        DataTable table = new DataTable();
        table = this.grdBillSearch.XGridDataset;
        this.lblTotalBillAmount.Text = " Total Bill Amount = $" + Convert.ToString(Math.Round(Convert.ToDecimal(table.Rows[0][0].ToString()), 2));
        this.lblOutSratingAmount.Text = "Total Outstanding Amount = $" + Convert.ToString(Math.Round(Convert.ToDecimal(table.Rows[0][1].ToString()), 2));
        this.lblWrite.Text = "Total WriteOff Amount = $" + Convert.ToString(Math.Round(Convert.ToDecimal(table.Rows[0][2].ToString()), 2));
        this.lblPaid.Text = "Total Paid Amount = $" + Convert.ToString(Math.Round(Convert.ToDecimal(table.Rows[0][3].ToString()), 2));
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdBillSearch.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
}
