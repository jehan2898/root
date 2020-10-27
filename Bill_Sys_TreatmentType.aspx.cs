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

public partial class Bill_Sys_TreatmentType : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrids();
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_TreatmentType.aspx");
        }
        #endregion
    }

    private void BindGrids()
    {
        DataSet ds = new DataSet();
        ds.ReadXml(Server.MapPath("XML/Demo_TreatmentGrid.xml"));
        grdTreatments.DataSource = ds;
        grdTreatments.DataBind();
    }

    protected void extddlDoctor_Treatment_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void extddlDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}