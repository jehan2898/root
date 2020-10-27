using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_InvoiceSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            txtCompanyID.Text= ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlEmployer.Flag_ID = txtCompanyID.Text;
            dddlInvoiceDate.Attributes.Add("onChange", "javascript:SetInvoiceDate();");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        EmployerBO obj = new EmployerBO();

        DataSet ds = obj.SearchInvoice(txtInvoiceNo.Text, txtCompanyID.Text, txtInvoceFromDate.Text, txtInvoceToDate.Text, extddlEmployer.Text);
        grdVisits.DataSource = ds;
        grdVisits.DataBind();
    }

    
}