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

public partial class Bill_Sys_CO_ViewDeatails : PageBase
{
    Bill_Sys_CheckoutBO _obj_CheckOutBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dset = new DataSet();
        _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
        try
        {
            string sz_UserID = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
            string sz_CaseID = Request.QueryString["CaseID"].ToString();
            string sz_ProcedureID = Request.QueryString["ProcID"].ToString();
            string sz_CompanyID = Request.QueryString["CompID"].ToString();

            dset = _obj_CheckOutBO.GetCaseDiagnosis(sz_CaseID, sz_CompanyID, sz_ProcedureID, sz_UserID);
            grddiagnosis.DataSource = dset;
            grddiagnosis.DataBind();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CO_ViewDeatails.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
