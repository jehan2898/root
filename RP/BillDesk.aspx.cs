using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ReferringProvider;
using DevExpress.Web;

public partial class Refering_provider_Bill : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCaseType();
            BindBillStatus();
            BindSpeciality();
            
        }
        BindGrid();
    }

    protected void ASPxCallback1_Callback(object sender, CallbackEventArgs e)
    {
        string sUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        BindGrid();
        string sFileName = null;
        string sFile = ReferringProvider.utils.ReferringProviderUtils.GetExportPhysicalPath(sUserName, ReferringProvider.utils.ReferringProviderExportTypes.BILL_LIST, out sFileName);
        System.IO.Stream stream = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
        grdBillsExport.WriteXlsx(stream);
        stream.Close();

        ArrayList list = new ArrayList();
        list.Add(ReferringProvider.utils.ReferringProviderUtils.GetExportRelativeURL(sFileName));
        Session["Download_Files"] = list;
    }

    public void BindCaseType()
    {
        try
        {
            ReferenceProvider objDocBO = new ReferenceProvider();
            DataSet casetypelist = objDocBO.getCaseType(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            //txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            ddlcasetype.DataSource = casetypelist;
            ddlcasetype.TextField = "DESC";
            ddlcasetype.ValueField = "CODE";
            ddlcasetype.DataBind();
            DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlcasetype.Items.Insert(0, Item);
            ddlcasetype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }
    }

    public void BindBillStatus()
    {
        try
        {
            ReferenceProvider objDocBO = new ReferenceProvider();
            DataSet casetypelist = objDocBO.getBillStatus(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            //txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            ddlBillstatus.DataSource = casetypelist;
            ddlBillstatus.TextField = "DESC";
            ddlBillstatus.ValueField = "CODE";
            ddlBillstatus.DataBind();
            DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlBillstatus.Items.Insert(0, Item);
            ddlBillstatus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }
    }

    public void BindSpeciality()
    {
        try
        {
            ReferenceProvider objDocBO = new ReferenceProvider();
            DataSet casetypelist = objDocBO.getSpecialty(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            //txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            ddlspeciality.DataSource = casetypelist;
            ddlspeciality.TextField = "DESC";
            ddlspeciality.ValueField = "CODE";
            ddlspeciality.DataBind();
            DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlspeciality.Items.Insert(0, Item);
            ddlspeciality.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }
    }

    public void BindGrid()
    {
        DataSet dsBills = new DataSet();
        ReferenceProvider objDocBO = new ReferenceProvider();
        RefferingProviderBillDAO objBillDao = new RefferingProviderBillDAO();

        objBillDao.BillNumber = Billno.Text.ToString(); 
        objBillDao.CaseNumber = txtCaseno.Text.ToString();
        
        if (ddlcasetype.SelectedItem.Value.ToString() != "NA")
        {
            objBillDao.CaseTypeID = ddlcasetype.SelectedItem.Value.ToString();
        }
        else
        {
            objBillDao.CaseTypeID = "";
        }

        if (ddlBillstatus.SelectedItem.Value.ToString() != "NA")
        {
            objBillDao.BillStatusID = ddlBillstatus.SelectedItem.Value.ToString();
        }
        else
        {
            objBillDao.BillStatusID = "";
        }
        if (ddlspeciality.SelectedItem.Value.ToString() != "NA")
        {
            objBillDao.SpecialtyID= ddlspeciality.SelectedItem.Value.ToString();
        }
        else
        {
            objBillDao.SpecialtyID = "";
        }

        string szFrmDate = "";
        string Todate = "";
        if (dtfromdate.Value != null && dttodate.Value != null)
        {
            DateTime dtFrmdate = Convert.ToDateTime(dtfromdate.Value);
            DateTime dtTodate = Convert.ToDateTime(dttodate.Value);
            szFrmDate = dtFrmdate.ToString("MM/dd/yyyy");
            Todate = dtTodate.ToString("MM/dd/yyyy");
        }
        objBillDao.FromDate = szFrmDate;
        objBillDao.ToDate = Todate;

        objBillDao.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        objBillDao.ReferringOfficeID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_REFF_PROVIDER_ID;
        objBillDao.UserID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;

        dsBills = objDocBO.getBillList(objBillDao);
        grdBills.DataSource = dsBills;
        grdBills.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //BindGrid();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Billno.Text = "";
        txtCaseno.Text = "";
        ddlBillstatus.SelectedIndex = 0;
        ddlcasetype.SelectedIndex = 0;
        ddlspeciality.SelectedIndex = 0;
        ddlDateValues.SelectedIndex = 0;
        dtfromdate.Value=null;
        dttodate.Value = null;
        BindGrid();
    }
}