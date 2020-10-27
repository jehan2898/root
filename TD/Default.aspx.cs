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

        Bill_Sys_UserObject objUser = new Bill_Sys_UserObject();
        objUser.SZ_REFF_PROVIDER_ID = "OI000000000000000525";
        objUser.SZ_USER_ID = "US000000000000000662";
        objUser.SZ_USER_NAME = "testpro";
        Session["USER_OBJECT"] = objUser;
    }
}