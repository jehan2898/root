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

public partial class Bill_Sys_Test_PopupShowDiagnosisCode : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
        {
            string szCaseId = Request.QueryString["P_SZ_CASE_ID"].ToString();
            string szSpeciality = Request.QueryString["P_SZ_Speciality"].ToString();
        string sz_CompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        BindGrid_DisplayDiagonosisCodeForTest(sz_CompanyId, szSpeciality, szCaseId);

    }

      public void BindGrid_DisplayDiagonosisCodeForTest(string sz_companyId,string sz_speciality,string sz_caseId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_ReportBO _reportBO = new Bill_Sys_ReportBO();
        try
        {
            grdDisplayDiagonosisCode.DataSource = _reportBO.getAssociatedDiagnosisCodeForTest(sz_companyId, sz_speciality, sz_caseId);
            grdDisplayDiagonosisCode.DataBind();
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
