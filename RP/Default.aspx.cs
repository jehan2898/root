using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Bill_Sys_BillingCompanyObject obj = new Bill_Sys_BillingCompanyObject();
        obj.SZ_COMPANY_ID = "CO00023";
        Session["BILLING_COMPANY_OBJECT"] = obj;
    }
}