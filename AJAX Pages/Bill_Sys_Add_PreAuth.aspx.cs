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
using DevExpress.Web;

public partial class AJAX_Pages_Bill_Sys_Add_PreAuth : PageBase
{
    Bill_Sys_Add_PreAuth _Bill_Sys_Add_PreAuth = new Bill_Sys_Add_PreAuth();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        DataSet dspreauth = new DataSet();
        dspreauth = _Bill_Sys_Add_PreAuth.Get_PreAuthorisation(txtCompanyID.Text);
        gridpreauthorisation.DataSource = dspreauth;
        gridpreauthorisation.DataBind();


    }
    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
         Session["Caseid"] = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        ((ASPxGridView)sender).DataSource = _Bill_Sys_Add_PreAuth.Get_PreAuthorisation_For_Specialty(txtCompanyID.Text);
    }
}
