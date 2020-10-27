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

public partial class AJAX_Pages_Bill_Sys_Assign_Attorny : PageBase
{
    private Patient_TVBO _patient_tvbo;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid = grdAttornyUser;
        this.txtSearchBox.SourceGrid = grdAttornyUser;
        this.grdAttornyUser.Page = this.Page;
        this.grdAttornyUser.PageNumberList = this.con;
        btnSave.Attributes.Add("onclick", "return checkvalue();");

        if (!IsPostBack)
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlAttorney.Flag_ID = txtCompanyID.Text.ToString();
            btnUpdate.Enabled = false;
            btnUpdate.Visible = false;
            grdAttornyUser.XGridBindSearch();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        _patient_tvbo = new Patient_TVBO();
        _patient_tvbo.Save_Attorny_User(extddlAssignAttorny.Text, extddlAttorney.Text, txtCompanyID.Text);
        grdAttornyUser.XGridBindSearch();

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        _patient_tvbo = new Patient_TVBO();
        //_patient_tvbo.Update_Attorny_User(extddlAssignAttorny.Text,extddlAttorney.Text,txtCompanyID.Text);

    }

    protected void btnExportToExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdAttornyUser.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
}
