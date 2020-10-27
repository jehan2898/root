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
using Componend;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Web;
using printcollection;


public partial class AJAX_Pages_Bill_Sys_SearchInsuranceCompany : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblErr.Visible = false;
            DataSet dsinsurance = new DataSet();
            InsuranceCompanyReport _Insurancecompany = new InsuranceCompanyReport();
            //lstInsuranceCompany.Attributes.Add("onClick", "javascript:unselectcheckboxforcom('chkCheck');");mapSelectedClick
            btnSearch.Attributes.Add("onClick", "return mapSelectedClick()");
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Session["SZ_COMPANY_ID"] = txtCompanyID.Text;
                SqlDataSource1.SelectCommand = "SP_GET_INSURANCE_NAME_FOR_REPORT";
                SqlDataSource1.DataSourceMode = SqlDataSourceMode.DataSet;
                SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("SZ_COMPANY_ID", DbType.String, txtCompanyID.Text);
                dsinsurance=_Insurancecompany.GetinsuranceCompnaies(txtCompanyID.Text);
               // dslocation = Report_SiteSummary.getlocation("CO000000000000000081");
                //lstInsuranceCompany.DataTextField = "DESCRIPTION";
                //lstInsuranceCompany.DataValueField = "CODE";
                //lstInsuranceCompany.DataSource = dsinsurance.Tables[0];
                //lstInsuranceCompany.DataBind();
                grdInsuranceCompany.DataSource = dsinsurance.Tables[0];
                grdInsuranceCompany.DataBind();
                bindCaseStatusGrid();
                bindBillStatusGrid();
            }

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
    private void bindCaseStatusGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        InsuranceCompanyReport _Insurancecompany = new InsuranceCompanyReport();
        try
        {
            DataSet ds_CaseStatusID = new DataSet();
            ds_CaseStatusID = _Insurancecompany.GetStatusID("SP_GET_CASESTATUSID", txtCompanyID.Text);
            grdCaseStatus.DataSource = ds_CaseStatusID;
            grdCaseStatus.DataBind();

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
    private void bindBillStatusGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        InsuranceCompanyReport _Insurancecompany = new InsuranceCompanyReport();
        try
        {
            DataSet ds_BillStatusID = new DataSet();
            ds_BillStatusID = _Insurancecompany.GetStatusID("SP_GET_BILLSTATUSID", txtCompanyID.Text);
            grdBillStatus.DataSource = ds_BillStatusID;
            grdBillStatus.DataBind();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        InsuranceCompanyReport _Insurancecompany = new InsuranceCompanyReport();
        try
        {

            string final_insuranceID = "";
            for (int i = 0; i < grdInsuranceCompany.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdInsuranceCompany.Columns[0];
                CheckBox chk = (CheckBox)grdInsuranceCompany.FindRowCellTemplateControl(i, c, "chkall3");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        string insuranceName = grdInsuranceCompany.GetRowValues(i, "CODE").ToString();

                        final_insuranceID += "," + insuranceName;
                    }
                }
            }
            if (final_insuranceID != "")
            {
                final_insuranceID = final_insuranceID.Remove(0, 1);
            }
            string final_casestatusid = "";
            for (int i = 0; i < grdCaseStatus.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdCaseStatus.Columns[0];
                CheckBox chk = (CheckBox)grdCaseStatus.FindRowCellTemplateControl(i, c, "chkall1");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        string casestatusid = grdCaseStatus.GetRowValues(i, "SZ_CASE_STATUS_ID").ToString();

                        final_casestatusid += "," + casestatusid;
                    }
                }
            }
            if (final_casestatusid != "")
            {
                final_casestatusid = final_casestatusid.Remove(0, 1);
            }

            string final_billstatusid = "";
            for (int i = 0; i < grdBillStatus.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdBillStatus.Columns[0];
                CheckBox chk = (CheckBox)grdBillStatus.FindRowCellTemplateControl(i, c, "chkall2");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        string billstatusid = grdBillStatus.GetRowValues(i, "SZ_BILL_STATUS_ID").ToString();

                        final_billstatusid += "," + billstatusid;
                    }
                }
            }
            if (final_billstatusid != "")
            {
                final_billstatusid = final_billstatusid.Remove(0, 1);
            }
            if (final_insuranceID == "" && final_billstatusid == "")
            {
                lblErr.Visible = true;
                lblErr.Text = "Insurance company and Bill Status is mandatory";
                return;
            }
            if (final_billstatusid == "")
            {
                lblErr.Visible = true;
                lblErr.Text = "Please select Bill status";
                return;
            }
            if (final_insuranceID == "")
            {
                lblErr.Visible = true;
                lblErr.Text = "Please select Insurance company";
                return;
            }
            

            DataSet ds_CaseID = new DataSet();
            ds_CaseID = _Insurancecompany.GetCollectionReport("SP_GET_COLLECTION_REPORT_GRID", txtFromDate.Text, txtToDate.Text, txtCompanyID.Text, final_insuranceID, final_casestatusid, final_billstatusid);
            if (ds_CaseID != null)
            {
                //if (ds_CaseID.Tables.Count > 0)
                //{
                //    if (ds_CaseID.Tables[0].Rows.Count > 0)
                //    {
                        grcollectionreport.DataSource = ds_CaseID;
                        grcollectionreport.DataBind();
                //    }
                //}
            }
            
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

    private void BindGrid1()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        InsuranceCompanyReport _Insurancecompany = new InsuranceCompanyReport();
        try
        {
            DataSet ds_CaseID = new DataSet();
            ds_CaseID = _Insurancecompany.GetCaseID("SP_GET_CASEID", txtFromDate.Text, txtToDate.Text, txtCompanyID.Text);
            DataRow row;
            DataTable tbl = new DataTable();

            tbl.Columns.Add("SZ_PATIENT_NAME");
            tbl.Columns.Add("SZ_CASE_NO");
            tbl.Columns.Add("SZ_INSURANCE_NAME");
            tbl.Columns.Add("SZ_CLAIM_NUMBER");
            tbl.Columns.Add("SZ_POLICY_NO");
            tbl.Columns.Add("SZ_ADJUSTER_NAME");
            tbl.Columns.Add("SZ_PHONE");
            tbl.Columns.Add("SZ_CASE_ID");
            
            DataRow row1;
            DataTable tb2 = new DataTable();
            tb2.Columns.Add("SZ_CASE_ID");

            string final_insuranceNames = "";
            for (int i = 0; i < grdInsuranceCompany.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdInsuranceCompany.Columns[0];
                CheckBox chk = (CheckBox)grdInsuranceCompany.FindRowCellTemplateControl(i, c, "chkall3");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        string insuranceName = grdInsuranceCompany.GetRowValues(i, "DESCRIPTION").ToString();

                        final_insuranceNames += "^" + insuranceName;
                    }
                }
            }
            if (final_insuranceNames != "")
            {
                final_insuranceNames = final_insuranceNames.Remove(0, 1);
            }

            //string hiddenfield = listt_comma_seprate.Value;

            //hiddenfield = hiddenfield.Remove(0, 1);
            for (int i = 0; i < ds_CaseID.Tables[0].Rows.Count; i++)
            {
                string caseid_dataset = ds_CaseID.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();

                string InsuranceName = _Insurancecompany.GetInsuranceCompanyName(caseid_dataset, txtCompanyID.Text);

                string[] primeArray = final_insuranceNames.Split('^');
                for (int k = 0; k < primeArray.Length; k++)
                {
                    string Iname = primeArray[k].ToString();

                    if (Iname == InsuranceName)
                    {
                        row1 = tb2.NewRow();
                        row1["SZ_CASE_ID"] = caseid_dataset;
                        tb2.Rows.Add(row1);
                    }
                    else
                    {
                    }
                }
            }
           
            for (int i = 0; i < tb2.Rows.Count; i++)
            {
                string caseid_dataset1 =tb2.Rows[i][0].ToString();
                DataSet result = new DataSet();
                result = _Insurancecompany.GetResult("SP_GET_COLLECTION_REPORT", caseid_dataset1, txtCompanyID.Text);

                row = tbl.NewRow();
                row["SZ_PATIENT_NAME"] = result.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString();
                row["SZ_CASE_NO"] = result.Tables[0].Rows[0]["SZ_CASE_NO"].ToString();
                row["SZ_INSURANCE_NAME"] = result.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                row["SZ_CLAIM_NUMBER"] = result.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                row["SZ_POLICY_NO"] = result.Tables[0].Rows[0]["SZ_POLICY_NO"].ToString();
                row["SZ_ADJUSTER_NAME"] = result.Tables[0].Rows[0]["SZ_ADJUSTER_NAME"].ToString();
                row["SZ_PHONE"] = result.Tables[0].Rows[0]["SZ_PHONE"].ToString();
                row["SZ_CASE_ID"] = result.Tables[0].Rows[0]["SZ_CASE_ID"].ToString();
                tbl.Rows.Add(row);  
            }
            
            ViewState["VSgrcollectionreport"] = tbl;
            grcollectionreport.DataSource = tbl;
            grcollectionreport.DataBind();
            grcollectionreport.Columns[8].Visible = false;
            //string[] primeArray1 = hiddenfield.Split('^');
            //lstboxItems.Items.Clear();
            //for (int l = 0; l < primeArray1.Length; l++)
            //{
            //    string Iname1 = primeArray1[l].ToString();
            //    lstboxItems.Items.Add(Iname1);
            //}
           
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
   
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwo = new DataTable();
        dtgrdTwo = (DataTable)ViewState["VSgrcollectionreport"];

        grcollectionreport.DataSource = dtgrdTwo;
        grcollectionreport.DataBind();

        grdExportTwo.WriteXlsToResponse();
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        //lstboxItems.Items.Clear();
        txtFromDate.Text = "";
        txtToDate.Text = "";
        //ASPxComboBox1.Text = "";

    }
    protected void btnpdf_Click(object sender, EventArgs e)
    {
        Bill_Sys_NF3_Template _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
        //String filePath = _bill_Sys_NF3_Template.getPhysicalPath();
        //filePath = filePath + "Collection"+"/";
        String filePath = "Collection" + "/";
       // string filePath = ConfigurationSettings.AppSettings["COLLCTION_REPORT_PATH"];
        string final_caseid = "";
        for (int i = 0; i < grcollectionreport.VisibleRowCount; i++)
        {
                GridViewDataColumn c = (GridViewDataColumn)grcollectionreport.Columns[0];
                CheckBox chk = (CheckBox)grcollectionreport.FindRowCellTemplateControl(i, c, "chkall");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                      string  caseid = grcollectionreport.GetRowValues(i, "SZ_CASE_ID").ToString();

                      final_caseid += "," + caseid;
                    }
                }
        }
        if (final_caseid != "")
        {
            final_caseid = final_caseid.Remove(0, 1);
        }
        

        string final_casestatusid = "";
        for (int i = 0; i < grdCaseStatus.VisibleRowCount; i++)
        {
            GridViewDataColumn c = (GridViewDataColumn)grdCaseStatus.Columns[0];
            CheckBox chk = (CheckBox)grdCaseStatus.FindRowCellTemplateControl(i, c, "chkall1");
            if (chk != null)
            {
                if (chk.Checked)
                {
                    string casestatusid = grdCaseStatus.GetRowValues(i, "SZ_CASE_STATUS_ID").ToString();

                    final_casestatusid += "," + casestatusid;
                }
            }
        }
        if (final_casestatusid != "")
        {
            final_casestatusid = final_casestatusid.Remove(0, 1);
        }

        string final_billstatusid = "";
        for (int i = 0; i < grdBillStatus.VisibleRowCount; i++)
        {
            GridViewDataColumn c = (GridViewDataColumn)grdBillStatus.Columns[0];
            CheckBox chk = (CheckBox)grdBillStatus.FindRowCellTemplateControl(i, c, "chkall2");
            if (chk != null)
            {
                if (chk.Checked)
                {
                    string billstatusid = grdBillStatus.GetRowValues(i, "SZ_BILL_STATUS_ID").ToString();

                    final_billstatusid += "," + billstatusid;
                }
            }
        }
        if (final_billstatusid != "")
        {
            final_billstatusid = final_billstatusid.Remove(0, 1);
        }
        string companyid = txtCompanyID.Text;
        string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        string Fromdate = txtFromDate.Text;
        string Todate = txtToDate.Text;

        printcollection.printPdf objprintPdf = new printPdf();

        string fileName=objprintPdf.GenerateCollectionPDF(_bill_Sys_NF3_Template.getPhysicalPath()+filePath, final_caseid, companyid, UserName, Fromdate, Todate, final_casestatusid, final_billstatusid);

        string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
        //hyLinkOpenPDF.NavigateUrl = "http://localhost/MBSScans/" + filepath;
        //hyLinkOpenPDF.NavigateUrl = "https://www.gogreenbills.com/MBSScans/" + filepath;
        //hyLinkOpenPDF.NavigateUrl = str + "/" + filepath;
        string windowopen = str + "/" + filePath + "/" + fileName;
        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + windowopen + "'); ", true);
    }

}
