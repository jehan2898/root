using DevExpress.Web;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_Bill_Sys_Billing_Summary_Searchcase : Page, IRequiresSessionState
{
    private Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();


    public AJAX_Pages_Bill_Sys_Billing_Summary_Searchcase()
    {
    }

    protected void btnReportSearch_Click(object sender, EventArgs e)
    {
        Bill_Sys_Billing_Provider billSysBillingProvider = new Bill_Sys_Billing_Provider();
        DataSet dataSet = new DataSet();
        string str = "";
        if (this.lstprovidernamename.SelectedItem != null)
        {
            bool flag = false;
            for (int i = 0; i < this.lstprovidernamename.Items.Count; i++)
            {
                if (this.lstprovidernamename.Items[i].Selected)
                {
                    flag = true;
                }
                if (flag && this.lstprovidernamename.Items[i].Selected)
                {
                    str = (str != "" ? string.Concat(str, ",'", this.lstprovidernamename.Items[i].Value.ToString(), "'") : string.Concat("'", this.lstprovidernamename.Items[i].Value.ToString(), "'"));
                }
            }
            dataSet = billSysBillingProvider.getGrandTotal(str, this.txtCompanyID.Text, this.txtcaseid.Text);
            this.grdGrandTotals.DataSource = dataSet;
            this.grdGrandTotals.DataBind();
            this.Session["dsShowProvider"] = dataSet;
            this.sdsCompanyMaster.SelectCommand = "SP_GET_OFFICE_SUMMARY";
            this.sdsCompanyMaster.DataSourceMode = SqlDataSourceMode.DataSet;
            this.sdsCompanyMaster.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            this.sdsCompanyMaster.SelectParameters.Clear();
            this.sdsCompanyMaster.SelectParameters.Add("SZ_OFFICE_ID", DbType.String, str);
            this.sdsCompanyMaster.SelectParameters.Add("SZ_COMPANY_ID", DbType.String, this.txtCompanyID.Text);
            this.sdsCompanyMaster.SelectParameters.Add("SZ_CASE_ID", DbType.String, this.txtcaseid.Text);
            this.gridprovider.DataSource = this.sdsCompanyMaster;
            this.gridprovider.DataBind();
            DataSet caseNotes = new DataSet();
            caseNotes = billSysBillingProvider.getCaseNotes(str, this.txtCompanyID.Text, this.txtcaseid.Text);
            this.grdccasenotes.DataSource = caseNotes;
            this.grdccasenotes.DataBind();
            this.Session["dscasenotes"] = caseNotes;
        }
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        dataSet = (DataSet)this.Session["dsShowProvider"];
        this.grdGrandTotals.DataSource = dataSet;
        this.grdGrandTotals.DataBind();
        this.exporter.WriteXlsToResponse();
    }

    protected void btnXlsExport1_Click(object sender, EventArgs e)
    {
        string str = "";
        for (int i = 0; i < this.lstprovidernamename.Items.Count; i++)
        {
            if (this.lstprovidernamename.Items[i].Selected)
            {
                str = (str != "" ? string.Concat(str, ",'", this.lstprovidernamename.Items[i].Value.ToString(), "'") : string.Concat("'", this.lstprovidernamename.Items[i].Value.ToString(), "'"));
            }
        }
        this.sdsCompanyMaster.SelectCommand = "SP_GET_OFFICE_SUMMARY";
        this.sdsCompanyMaster.DataSourceMode = SqlDataSourceMode.DataSet;
        this.sdsCompanyMaster.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        this.sdsCompanyMaster.SelectParameters.Clear();
        this.sdsCompanyMaster.SelectParameters.Add("SZ_OFFICE_ID", DbType.String, str);
        this.sdsCompanyMaster.SelectParameters.Add("SZ_COMPANY_ID", DbType.String, this.txtCompanyID.Text);
        this.sdsCompanyMaster.SelectParameters.Add("SZ_CASE_ID", DbType.String, this.txtcaseid.Text);
        this.gridprovider.DataSource = this.sdsCompanyMaster;
        this.gridprovider.DataBind();
        this.grdExportThirtyFour.WriteXlsToResponse();
    }

    protected void btnXlsExport2_Click(object sender, EventArgs e)
    {
        string str = this.Session["SZ_OFFICE_ID"].ToString();
        this.sdsProviderMaster.SelectCommand = "SP_GET_OFFICE_BILL_REPORT";
        this.sdsProviderMaster.DataSourceMode = SqlDataSourceMode.DataSet;
        this.sdsProviderMaster.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        this.sdsProviderMaster.SelectParameters.Clear();
        this.sdsProviderMaster.SelectParameters.Add("SZ_OFFICE_ID", DbType.String, str);
        this.grdExport2.WriteXlsToResponse();
    }

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        this.Session["SZ_OFFICE_ID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        this.Session["SZ_COMPANY_ID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        this.Session["SZ_CASE_ID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        string str = this.Session["SZ_OFFICE_ID"].ToString();
        this.sdsProviderMaster.SelectCommand = "SP_GET_OFFICE_BILL_REPORT";
        this.sdsProviderMaster.DataSourceMode = SqlDataSourceMode.DataSet;
        this.sdsProviderMaster.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        this.sdsProviderMaster.SelectParameters.Clear();
        this.sdsProviderMaster.SelectParameters.Add("SZ_OFFICE_ID", DbType.String, str);
        this.sdsProviderMaster.SelectParameters.Add("SZ_COMPANY_ID", DbType.String, this.txtCompanyID.Text);
        this.sdsProviderMaster.SelectParameters.Add("SZ_CASE_ID", DbType.String, this.txtcaseid.Text);
    }

    protected void grdccasenotes_PageIndexChanged(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        dataSet = (DataSet)this.Session["dscasenotes"];
        this.grdccasenotes.DataSource = dataSet;
        this.grdccasenotes.DataBind();
    }

    protected void grdGrandTotals_PageIndexChanged(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        dataSet = (DataSet)this.Session["dsShowProvider"];
        this.grdGrandTotals.DataSource = dataSet;
        this.grdGrandTotals.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.lstprovidernamename.Attributes.Add("onClick", "javascript:unselectcheckboxforcom('chkCheck');");
        string str = base.Request.QueryString["szcaseid"].ToString();
        this.txtcaseid.Text = str;
        string str1 = base.Request.QueryString["szcompanyid"].ToString();
        this.txtCompanyID.Text = str1;
        if (!base.IsPostBack)
        {
            DataSet dataSet = new DataSet();
            dataSet = this._Bill_Sys_Billing_Provider.PatientDetailsProvider(this.txtCompanyID.Text, this.txtcaseid.Text);
            this.grdPatientDetails.DataSource = dataSet;
            this.grdPatientDetails.DataBind();
            this.Session["dsPatiendetails"] = dataSet;
            DataSet attorneyDetails = new DataSet();
            attorneyDetails = this._Bill_Sys_Billing_Provider.getAttorneyDetails(this.txtCompanyID.Text, this.txtcaseid.Text);
            if (attorneyDetails.Tables.Count > 0 && attorneyDetails.Tables[0].Rows.Count > 0)
            {
                this.grdAttorneyDetails.DataSource = attorneyDetails;
                this.grdAttorneyDetails.DataBind();
            }
            DataSet adjusterDetails = new DataSet();
            adjusterDetails = this._Bill_Sys_Billing_Provider.getAdjusterDetails(this.txtCompanyID.Text, this.txtcaseid.Text);
            if (adjusterDetails.Tables.Count > 0 && adjusterDetails.Tables[0].Rows.Count > 0)
            {
                this.grdadjuster.DataSource = adjusterDetails;
                this.grdadjuster.DataBind();
            }
            DataSet dataSet1 = new DataSet();
            dataSet1 = this._Bill_Sys_Billing_Provider.ProviderName(this.txtCompanyID.Text);
            this.lstprovidernamename.DataTextField = "Value";
            this.lstprovidernamename.DataValueField = "Code";
            this.lstprovidernamename.DataSource = dataSet1;
            this.lstprovidernamename.DataBind();
        }
    }
}