using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mbs.lawfirm;

public partial class AJAX_Pages_Bill_Sys_ConnectionProcedures : PageBase
{
    private Bill_Sys_ReferalEvent _objReferal;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //missing procedure grid

        this.XGridPaginationDropDown1.SourceGrid = MissingProcGrid;
        this.XGridSearchTextBox1.SourceGrid = MissingProcGrid;
        this.MissingProcGrid.Page = this.Page;
        this.MissingProcGrid.PageNumberList = this.XGridPaginationDropDown1;

        if (!IsPostBack)
        {
            ListItem objTest = new ListItem();
            objTest.Text = "--- Select ---";
            objTest.Value = "NA";
            ddlProcedureCode.Items.Insert(0, objTest);
            extddlBillCompany.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //code to display missing procedure grid
        try
        {
            txtCompanyId.Text = extddlBillCompany.Text;
            txtProcCodeId.Text = ddlProcedureCode.Text;
            txtProcGroupId.Text = extddlSpeciality.Text;
            txtLocationId.Text = "";
            MissingProcGrid.XGridBind();
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

    protected void extddlSpeciality_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _objReferal = new Bill_Sys_ReferalEvent();
        DataSet dset = new DataSet();
        ListItem li;
        try
        {
            string speciality = extddlSpeciality.Text.ToString();
           
            dset = _objReferal.GetProcedureCodeAndDescription(speciality, extddlBillCompany.Text);

            ddlProcedureCode.DataSource = dset;
            ddlProcedureCode.DataValueField = "Code";
            ddlProcedureCode.DataTextField = "Description";
            ddlProcedureCode.DataBind();
            ListItem objTest = new ListItem();
            objTest.Text = "--- Select ---";
            objTest.Value = "NA";
            ddlProcedureCode.Items.Insert(0, objTest);
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
    protected void lnkExportMissingGrd_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + MissingProcGrid.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void extddlBillCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtCompanyId.Text = extddlBillCompany.Text;
            extddlSpeciality.Flag_ID = txtCompanyId.Text;

            ListItem objTest = new ListItem();
            objTest.Text = "--- Select ---";
            objTest.Value = "NA";
            ddlProcedureCode.Items.Clear();
            ddlProcedureCode.Items.Insert(0, objTest);

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
