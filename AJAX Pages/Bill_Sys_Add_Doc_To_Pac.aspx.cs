using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AJAX_Pages_Bill_Sys_Add_Doc_To_Pac : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDoctor();

        }
        BindGrid();
    }
    public void BindDoctor()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            Bill_Sys_DoctorBO objDocBO = new Bill_Sys_DoctorBO();
            DataSet doctorList = objDocBO.GetDoctorList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

           
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            ddlDoctor.DataSource = doctorList;
            ddlDoctor.TextField = "DESCRIPTION";
            ddlDoctor.ValueField = "CODE";
            ddlDoctor.DataBind();
            DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlDoctor.Items.Insert(0, Item);
            ddlDoctor.SelectedIndex = 0;
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
    public void BindGrid()
    {
        DataSet ds = new DataSet();
        Bill_Sys_ScanDco objScanDoc = new Bill_Sys_ScanDco();
        string szFrmDate = "";
        string Todate = "";
        if (dtfromdate.Value != null && dttodate.Value != null)
        {
            DateTime dtFrmdate = Convert.ToDateTime(dtfromdate.Value);
            DateTime dtTodate = Convert.ToDateTime(dttodate.Value);
            szFrmDate = dtFrmdate.ToString("MM/dd/yyyy");
            Todate = dtTodate.ToString("MM/dd/yyyy");
        }
        ds = objScanDoc.GetVisit(txtCompanyID.Text, szFrmDate, Todate, txtCaseNo.Text, ddlDoctor.SelectedItem.Value.ToString(), ddlBiled.SelectedItem.Value.ToString());
        grdVisits.DataSource = ds;
        grdVisits.DataBind();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

    }
}