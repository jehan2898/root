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
using DevExpress.Web;
using System.Data.SqlClient;

public partial class AJAX_Pages_Bill_Sys_Billing_Summary : PageBase
{
    Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();

    protected void Page_Load(object sender, EventArgs e)
    {
        lstprovidernamename.Attributes.Add("onClick", "javascript:unselectcheckboxforcom('chkCheck');");
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtcaseid.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;

        if (!IsPostBack)
        {
            DataSet dspatientdetailsprovider = new DataSet();
            dspatientdetailsprovider = _Bill_Sys_Billing_Provider.PatientDetailsProvider(txtCompanyID.Text, txtcaseid.Text);
            grdPatientDetails.DataSource = dspatientdetailsprovider;
            grdPatientDetails.DataBind();
            Session["dsPatiendetails"] = dspatientdetailsprovider;



            DataSet dsattorneydetailsprovider = new DataSet();
            dsattorneydetailsprovider = _Bill_Sys_Billing_Provider.getAttorneyDetails(txtCompanyID.Text, txtcaseid.Text);
            if (dsattorneydetailsprovider.Tables.Count > 0)
            {
                if (dsattorneydetailsprovider.Tables[0].Rows.Count > 0)
                {
                    grdAttorneyDetails.DataSource = dsattorneydetailsprovider;
                    grdAttorneyDetails.DataBind();

                }
            }


            DataSet dsadjusterdetailsprovider = new DataSet();
            dsadjusterdetailsprovider = _Bill_Sys_Billing_Provider.getAdjusterDetails(txtCompanyID.Text, txtcaseid.Text);
            if (dsadjusterdetailsprovider.Tables.Count > 0)
            {
                if (dsadjusterdetailsprovider.Tables[0].Rows.Count > 0)
                {
                    grdadjuster.DataSource = dsadjusterdetailsprovider;
                    grdadjuster.DataBind();

                }
            }



            DataSet dsshowprovidername = new DataSet();
            dsshowprovidername = _Bill_Sys_Billing_Provider.ProviderName(txtCompanyID.Text);
            lstprovidernamename.DataTextField = "Value";
            lstprovidernamename.DataValueField = "Code";
            lstprovidernamename.DataSource = dsshowprovidername;
            lstprovidernamename.DataBind();

            for (int cnt = 0; cnt < lstprovidernamename.Items.Count; cnt++)
            {
                lstprovidernamename.Items[cnt].Selected = true;
                chkCheck.Checked = true;
            }

            string szofficeid = "";
            for (int i = 0; i < lstprovidernamename.Items.Count; i++)
            {
                if (szofficeid == "")
                {
                    szofficeid = "'" + lstprovidernamename.Items[i].Value.ToString() + "'";
                }
                else
                {
                    szofficeid = szofficeid + "," + "'" + lstprovidernamename.Items[i].Value.ToString() + "'";
                }
            }
            DataSet dsShowgrandTotal = new DataSet();
            dsShowgrandTotal = _Bill_Sys_Billing_Provider.getGrandTotal(szofficeid, txtCompanyID.Text, txtcaseid.Text);
            grdGrandTotals.DataSource = dsShowgrandTotal;
            grdGrandTotals.DataBind();
            Session["dsShowProvider"] = dsShowgrandTotal;

            sdsCompanyMaster.SelectCommand = "SP_GET_OFFICE_SUMMARY";
            sdsCompanyMaster.DataSourceMode = SqlDataSourceMode.DataSet;
            sdsCompanyMaster.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            sdsCompanyMaster.SelectParameters.Clear();
            sdsCompanyMaster.SelectParameters.Add("SZ_OFFICE_ID", DbType.String, szofficeid);
            sdsCompanyMaster.SelectParameters.Add("SZ_COMPANY_ID", DbType.String, txtCompanyID.Text);
            sdsCompanyMaster.SelectParameters.Add("SZ_CASE_ID", DbType.String, txtcaseid.Text);
            gridprovider.DataSource = sdsCompanyMaster;
            gridprovider.DataBind();


            DataSet dscasenotes = new DataSet();
            dscasenotes = _Bill_Sys_Billing_Provider.getCaseNotes(szofficeid, txtCompanyID.Text, txtcaseid.Text);
            grdccasenotes.DataSource = dscasenotes;
            grdccasenotes.DataBind();
            Session["dscasenotes"] = dscasenotes;



        }

    }


    protected void btnReportSearch_Click(object sender, EventArgs e)
    {

        Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
        DataSet dsShowgrandTotal = new DataSet();
        string szofficeid = "";
        if (lstprovidernamename.SelectedItem != null)
        {
            bool flagc = false;
            for (int cntLst = 0; cntLst < lstprovidernamename.Items.Count; cntLst++)
            {
                if (lstprovidernamename.Items[cntLst].Selected == true)
                {
                    flagc = true;
                }
                if (flagc == true)
                {
                    if (lstprovidernamename.Items[cntLst].Selected == true)
                    {
                        if (szofficeid == "")
                        {
                            szofficeid = "'" + lstprovidernamename.Items[cntLst].Value.ToString() + "'";
                        }
                        else
                        {
                            szofficeid = szofficeid + "," + "'" + lstprovidernamename.Items[cntLst].Value.ToString() + "'";
                        }
                    }
                }

            }
            dsShowgrandTotal = _Bill_Sys_Billing_Provider.getGrandTotal(szofficeid, txtCompanyID.Text, txtcaseid.Text);
            grdGrandTotals.DataSource = dsShowgrandTotal;
            grdGrandTotals.DataBind();
            Session["dsShowProvider"] = dsShowgrandTotal;

            sdsCompanyMaster.SelectCommand = "SP_GET_OFFICE_SUMMARY";
            sdsCompanyMaster.DataSourceMode = SqlDataSourceMode.DataSet;
            sdsCompanyMaster.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            sdsCompanyMaster.SelectParameters.Clear();
            sdsCompanyMaster.SelectParameters.Add("SZ_OFFICE_ID", DbType.String, szofficeid);
            sdsCompanyMaster.SelectParameters.Add("SZ_COMPANY_ID", DbType.String, txtCompanyID.Text);
            sdsCompanyMaster.SelectParameters.Add("SZ_CASE_ID", DbType.String, txtcaseid.Text);
            gridprovider.DataSource = sdsCompanyMaster;
            gridprovider.DataBind();


            DataSet dscasenotes = new DataSet();
            dscasenotes = _Bill_Sys_Billing_Provider.getCaseNotes(szofficeid, txtCompanyID.Text, txtcaseid.Text);
            grdccasenotes.DataSource = dscasenotes;
            grdccasenotes.DataBind();
            Session["dscasenotes"] = dscasenotes;


        }
    }
    protected void grdGrandTotals_PageIndexChanged(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        ds = (DataSet)Session["dsShowProvider"];
        grdGrandTotals.DataSource = ds;
        grdGrandTotals.DataBind();
    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        DataSet dsgrdtotals = new DataSet();
        dsgrdtotals = (DataSet)Session["dsShowProvider"];
        grdGrandTotals.DataSource = dsgrdtotals;
        grdGrandTotals.DataBind();
        exporter.WriteXlsToResponse();
    }

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        Session["SZ_OFFICE_ID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        Session["SZ_COMPANY_ID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        Session["SZ_CASE_ID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        string szofficeid = Session["SZ_OFFICE_ID"].ToString();
        sdsProviderMaster.SelectCommand = "SP_GET_OFFICE_BILL_REPORT";
        sdsProviderMaster.DataSourceMode = SqlDataSourceMode.DataSet;
        sdsProviderMaster.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        sdsProviderMaster.SelectParameters.Clear();
        sdsProviderMaster.SelectParameters.Add("SZ_OFFICE_ID", DbType.String, szofficeid);
        sdsProviderMaster.SelectParameters.Add("SZ_COMPANY_ID", DbType.String, txtCompanyID.Text);
        sdsProviderMaster.SelectParameters.Add("SZ_CASE_ID", DbType.String, txtcaseid.Text);

    }
    protected void btnXlsExport2_Click(object sender, EventArgs e)
    {
        string szofficeid = Session["SZ_OFFICE_ID"].ToString();

        sdsProviderMaster.SelectCommand = "SP_GET_OFFICE_BILL_REPORT";
        sdsProviderMaster.DataSourceMode = SqlDataSourceMode.DataSet;
        sdsProviderMaster.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        sdsProviderMaster.SelectParameters.Clear();
        sdsProviderMaster.SelectParameters.Add("SZ_OFFICE_ID", DbType.String, szofficeid);
        grdExport2.WriteXlsToResponse();
    }

    protected void btnXlsExport1_Click(object sender, EventArgs e)
    {
        string szofficeid = "";
        for (int cntLst = 0; cntLst < lstprovidernamename.Items.Count; cntLst++)
        {
            if (lstprovidernamename.Items[cntLst].Selected == true)
            {
                if (szofficeid == "")
                {
                    szofficeid = "'" + lstprovidernamename.Items[cntLst].Value.ToString() + "'";
                }
                else
                {
                    szofficeid = szofficeid + "," + "'" + lstprovidernamename.Items[cntLst].Value.ToString() + "'";
                }
            }
        }
        sdsCompanyMaster.SelectCommand = "SP_GET_OFFICE_SUMMARY";
        sdsCompanyMaster.DataSourceMode = SqlDataSourceMode.DataSet;
        sdsCompanyMaster.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        sdsCompanyMaster.SelectParameters.Clear();
        sdsCompanyMaster.SelectParameters.Add("SZ_OFFICE_ID", DbType.String, szofficeid);
        sdsCompanyMaster.SelectParameters.Add("SZ_COMPANY_ID", DbType.String, txtCompanyID.Text);
        sdsCompanyMaster.SelectParameters.Add("SZ_CASE_ID", DbType.String, txtcaseid.Text);
        gridprovider.DataSource = sdsCompanyMaster;
        gridprovider.DataBind();
        grdExportThirtyFour.WriteXlsToResponse();

    }


    protected void grdccasenotes_PageIndexChanged(object sender, EventArgs e)
    {
        DataSet dscasenotes = new DataSet();
        dscasenotes = (DataSet)Session["dscasenotes"];
        grdccasenotes.DataSource = dscasenotes;
        grdccasenotes.DataBind();
    }


}
