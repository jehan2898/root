using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Componend;
using log4net;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.IO;
using System.Data;
using System.Configuration;

public partial class AJAX_Pages_Default : System.Web.UI.Page
{
    ListOperation _listOperation;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_BillTransaction_BO _billTransactionBO;
    Bill_Sys_Case _bill_Sys_Case;
    CaseDetailsBO _caseDetailsBO;

    protected void Page_Load(object sender, EventArgs e)

    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.con.SourceGrid = this.grdvisits;
            this.txtSearchBox.SourceGrid = this.grdvisits;
            this.grdvisits.Page = this.Page;
            this.grdvisits.PageNumberList = this.con;
            if (!IsPostBack)
            {
                this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
                extddlDoctorName.Flag_ID = txtCompanyID.Text.ToString();
                extddlSpeciality.Flag_ID = txtCompanyID.Text.ToString();
                grdvisits.XGridBindSearch();
            }
        }
        catch(Exception ex)
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


    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdvisits.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {

            this.con.SourceGrid = this.grdvisits;
            this.txtSearchBox.SourceGrid = this.grdvisits;
            this.grdvisits.Page = this.Page;
            this.grdvisits.PageNumberList = this.con;
            grdvisits.XGridBindSearch();
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