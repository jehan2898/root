using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ReferringProvider;
using DevExpress.Web;

public partial class Refering_provider_Patient : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsCallback)
        {
            if (!IsPostBack)
            {
                row2label.Visible = false;
                row2control.Visible = false;
                BindCaseType();
                BindCaseStatus();
            }
            BindGrid();
            ajAutoName.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        }
    }

    protected void ASPxCallback1_Callback(object sender, CallbackEventArgs e)
    {
        string sUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        BindGrid();
        string sFileName = null;
        string sFile = ReferringProvider.utils.ReferringProviderUtils.GetExportPhysicalPath(sUserName, ReferringProvider.utils.ReferringProviderExportTypes.PATIENT_LIST, out sFileName);
        System.IO.Stream stream = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
        grdVisitsExport.WriteXlsx(stream);
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

    public void BindCaseStatus()
    {
        try
        {
            ReferenceProvider objDocBO = new ReferenceProvider();
            DataSet casestatuslist = objDocBO.getCaseStatus(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            
            ddlcasestatus.DataSource = casestatuslist;
            ddlcasestatus.TextField = "DESC";
            ddlcasestatus.ValueField = "CODE";
            ddlcasestatus.DataBind();
            DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlcasestatus.Items.Insert(0, Item);
            ddlcasestatus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }
    }

    public void BindLocation()
    {
        try
        {
            ReferenceProvider objDocBO = new ReferenceProvider();
            DataSet locationlist = objDocBO.getLocation(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            ddllocation.DataSource = locationlist;
            ddllocation.TextField = "DESC";
            ddllocation.ValueField = "CODE";
            ddllocation.DataBind();
            DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddllocation.Items.Insert(0, Item);
            ddllocation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }
    }

    public void BindGrid()
    {
        DataSet dsPatient = new DataSet();
        ReferenceProvider objDocBO = new ReferenceProvider();
        RefferingProviderPatientDAO objPatientDao = new RefferingProviderPatientDAO();
        objPatientDao.CaseNo = txtCaseNO.Text.ToString();
        if (ddlcasetype.SelectedItem.Value.ToString() != "NA")
        {
            objPatientDao.CaseType = ddlcasetype.SelectedItem.Value.ToString();
        }
        else
        {
            objPatientDao.CaseType = "";
        }
        if (ddlcasestatus.SelectedItem.Value.ToString() != "NA")
        {
            objPatientDao.CaseStatus = ddlcasestatus.SelectedItem.Value.ToString();
        }
        else
        {
            objPatientDao.CaseStatus = "";
        }
        objPatientDao.PatientName = txtpatient.Text.ToString();
        objPatientDao.Insurance = txtinsurance.Text.ToString();
        objPatientDao.claimNumber = txtClaimnumber.Text.ToString();
        objPatientDao.SSNNumber = txtSSN.Text.ToString();

        objPatientDao.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        objPatientDao.ReferringOfficeID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_REFF_PROVIDER_ID;
        objPatientDao.userID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;

        dsPatient = objDocBO.getPatientList(objPatientDao);
        grdVisits.DataSource = dsPatient;
        grdVisits.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //BindGrid();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtCaseNO.Text = "";
        ddlcasestatus.SelectedIndex = 0;
        ddlcasetype.SelectedIndex = 0;
        ddllocation.SelectedIndex = 0;
        txtpatient.Text = "";
        txtinsurance.Text = "";
        txtClaimnumber.Text = "";
        txtSSN.Text = "";
        BindGrid();
    }
}