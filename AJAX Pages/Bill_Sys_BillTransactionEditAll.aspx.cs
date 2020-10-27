using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using DevExpress.Web;

public partial class AJAX_Pages_Bill_Sys_BillTransactionEditAll : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szCaseID = Request.QueryString["CaseID"].ToString();
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        DataSet dset = new DataSet();
        Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        dset = _bill_Sys_Visit_BO.GetBillVisits(txtCompanyID.Text, szCaseID);
        grdVisits.DataSource = dset;
        grdVisits.DataBind();
    }
}