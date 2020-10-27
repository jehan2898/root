using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Configuration;

public partial class AJAX_Pages_Bill_Sys_InsurancePriorityBilling : PageBase
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private Bill_Sys_Case _bill_Sys_Case;
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    string strLinkPath = null;
    Bill_Sys_NF3_Template objNF3Template;

    private Bill_Sys_SystemObject objSessionSystem;
    private Bill_Sys_BillingCompanyObject objSessionBillingCompany;
    private Bill_Sys_BillingCompanyObject objSessionCompanyAppStatus;
    private Bill_Sys_UserObject objSessionUser;

    const int COLIDX_GRIDPAIDBILLS_CHECKBOX = 0;
    const int COLIDX_GRIDPAIDBILLS_TESTDATA = 22;
    const int COLIDX_GRIDPAIDBILLS_CHART_NO = 1;
    const int COLIDX_GRIDBILLS_CHART_NO = 4;
    const int COLIDX_GRIDPAIDBILLS_LHRCODE = 21;
    const int COLIDX_GRIDPAIDBILLS_VISIT_STATUS = 11;
    const int COLIDX_GRIDPAIDBILLS_DAYS = 23;
    const int COLIDX_GRIDPAIDBILLS_CASE_TYPE = 24;
    StringBuilder szExportoExcelColumname = new StringBuilder();
    StringBuilder szExportoExcelField = new StringBuilder();
    //const int COLIDX_GRIDPAIDBILLS_EDIT_PROCEDURE_CODE = 21;
    //const int COLIDX_GRIDPAIDBILLS_VIEW_DOCUMENTS = 22;	
    string caseId = "";
    string companyID = "";
    string proc_id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        objSessionSystem = (Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"];
        objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
        objSessionCompanyAppStatus = (Bill_Sys_BillingCompanyObject)Session["APPSTATUS"];
        objSessionUser = (Bill_Sys_UserObject)Session["USER_OBJECT"];

        this.con1.SourceGrid = grdpaidbills;
        this.txtSearchBox1.SourceGrid = grdpaidbills;
        this.grdpaidbills.Page = this.Page;
        this.grdpaidbills.PageNumberList = this.con1;

        this.XGridPaginationDropDownUnPaid.SourceGrid = grdBills;
        this.XGridSearchTextBoxUnPaid.SourceGrid = grdBills;
        this.grdBills.Page = this.Page;
        this.grdBills.PageNumberList = this.XGridPaginationDropDownUnPaid;
        txtUpdate1.Attributes.Add("onclick", "javascript:CloseEditProcPopup();");
        txtUpdate2.Attributes.Add("onclick", "javascript:CloseDocumentPopup();");
        txtUpdate3.Attributes.Add("onclick", "javascript:CloseDocPopup();");
        txtUpdate4.Attributes.Add("onclick", "javascript:CloseDocPopup();");
        ddlDateValues.Attributes.Add("onChange", "javascript:SetVisitDate();");
        btnUPdate.Attributes.Add("onclick", "return confirm_update_bill_status();");
        if (chkAOb.Checked == true)
            txtAOb.Text = "1";
        else
            txtAOb.Text = "0";
        if (chkReport.Checked == true)
            txtReport.Text = "1";
        else
            txtReport.Text = "0";
        if (chkReferral.Checked == true)
            txtReferral.Text = "1";
        else
            txtReferral.Text = "0";
        if (chkLien.Checked == true)
            txtLien.Text = "1";
        else
            txtLien.Text = "0";
        if (chkComp.Checked == true)
            txtComp.Text = "1";
        else
            txtComp.Text = "0";
        if (chkAdditionalReports.Checked == true)
        {
            txtAdditionalReports.Text = "1";
        }
        else
            txtAdditionalReports.Text = "0";

        try
        {
            if (Request.QueryString["popup"] != null)
            {
                if (Request.QueryString["popup"].ToString().Equals("done"))
                {
                    lblMsg.Text = "Report received successfully.";
                    lblMsg.Visible = true;
                }
                if (Request.QueryString["popup"].ToString().Equals("done1"))
                {
                    lblMsg.Visible = false;
                }
            }
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
            {
                szExportoExcelColumname.Append("Chart No,");
                szExportoExcelField.Append("SZ_CHART_NO,");
            }

            szExportoExcelColumname.Append("Case #,Patient Name,Date of Service,Procedure Code,Description,");
            szExportoExcelField.Append("CASE_NO,PATIENT_NAME,DT_DATE_OF_SERVICE,SZ_CODE,SZ_CODE_DESCRIPTION,");

            string sIsShowProcedureCode3 = objSessionSystem.SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION;
            if (sIsShowProcedureCode3 != "1")
            {
                szExportoExcelColumname.Append("Status,");
                szExportoExcelField.Append("SZ_STATUS,");
                grdpaidbills.ExportToExcelColumnNames = szExportoExcelColumname.ToString();
                //grdPatientList.Con = xcon;
                grdpaidbills.ExportToExcelFields = szExportoExcelField.ToString();

            }
            else
            {
                szExportoExcelColumname.Append("LHR Code,");
                szExportoExcelField.Append("SZ_LHR_CODE,");
                szExportoExcelColumname.Append("No. of Days,");
                szExportoExcelField.Append("I_NO_OF_DAYS,");
                szExportoExcelColumname.Append("Case Type,");
                szExportoExcelField.Append("SZ_CASE_TYPE_NAME,");
                szExportoExcelColumname.Append("Patient Id,");
                szExportoExcelField.Append("sz_remote_case_id,");
                szExportoExcelColumname.Append("Appointment Id");
                szExportoExcelField.Append("sz_remote_appointment_id");
                grdpaidbills.ExportToExcelColumnNames = szExportoExcelColumname.ToString();
                grdpaidbills.ExportToExcelFields = szExportoExcelField.ToString();
            }
            if (!IsPostBack)
            {



                try
                {
                    if (Request.QueryString["Save"].ToString().Equals("done"))
                    {
                        ArrayList arrPgeValue = new ArrayList();
                        arrPgeValue = (ArrayList)Session["PAGE_VALUES"];
                        txtVisitDate.Text = arrPgeValue[0].ToString();
                        txtToVisitDate.Text = arrPgeValue[1].ToString();
                        extddlCaseType.Text = arrPgeValue[2].ToString();
                        txtNumberOfDays.Text = arrPgeValue[3].ToString();
                        drpdown_Documents.Text = arrPgeValue[4].ToString();
                        if (arrPgeValue[5].ToString() == "1")
                        {
                            chkAOb.Checked = true;
                        }
                        if (arrPgeValue[6].ToString() == "1")
                        {
                            chkReport.Checked = true;
                        }
                        if (arrPgeValue[7].ToString() == "1")
                        {
                            chkAOb.Checked = true;
                        }

                        Session["CON_VAL"] = arrPgeValue[8].ToString();
                        if (arrPgeValue[9].ToString() == "1")
                        {
                            chkLien.Checked = true;
                        }
                        if (arrPgeValue[10].ToString() == "1")
                        {
                            chkComp.Checked = true;
                        }
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
                txtCompanyid.Text = objSessionBillingCompany.SZ_COMPANY_ID;
                extddlCaseType.Flag_ID = txtCompanyid.Text;
                extddlSpecialty.Flag_ID = txtCompanyid.Text;
                extddlInsurance.Flag_ID = txtCompanyid.Text;
                if (lblMsg.Text.Equals(""))
                {
                    lblMsg.Visible = false;
                }
                if (Request.QueryString["Flag"] != null)
                {
                    if (Request.QueryString["Flag"].ToString().ToLower() == "paid")
                    {
                        //SearchBilllist("GETPAIDLIST");
                        lblHeader.Text = "Paid Bills";  // these should come from a property file   
                        txtHeading.Text = "Paid Bills";
                        BindBillsGrid("paid");
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "unpaid")
                    {
                        //SearchBilllist("GETUNPAIDLIST");
                        lblHeader.Text = "Un-Paid Bills";
                        txtHeading.Text = "Un-Paid Bills";
                        BindBillsGrid("unpaid");
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missingattorney")
                    {
                        //searchcaselist("GET_MISSING_ATTORNEY_LIST");
                        lblHeader.Text = "Missing Attorney";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missinginsurancecompany")
                    {
                        //searchcaselist("GET_MISSING_INSURANCE_LIST");
                        lblHeader.Text = "Missing Insurance Company";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missingprovider")
                    {
                        //searchcaselist("GET_MISSING_PROVIDER_LIST");
                        lblHeader.Text = "Missing Provider";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missingclaimnumber")
                    {
                        //searchcaselist("GET_MISSING_CLAIM_LIST");
                        lblHeader.Text = "Missing Claim Number";
                        //grdCaseMaster.Columns[5].Visible = true;
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missingreportnumber")
                    {
                        //searchcaselist("GET_MISSING_REPORT_LIST");
                        lblHeader.Text = "Missing Report Number";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missingpolicyholder")
                    {
                        //searchcaselist("GET_MISSING_POCLICY_HOLDER");
                        lblHeader.Text = "Missing Policy Holder";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "report")
                    {
                        if (Request.QueryString["Type"] != null)
                        {
                            if (Request.QueryString["Type"].ToString() == "R")
                            {
                                lblHeader.Text = "Received Report";
                                BindGridQueryString("True");
                            }
                            else
                            {

                                lblHeader.Text = "Pending Report";
                                txtHeading.Text = "Pending Report";
                                BindGridQueryString("False");
                                //BindGridForExportToExcel("False");
                            }
                        }
                    }
                }


                //if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "0")
                if (objSessionSystem.SZ_CHART_NO == "0")
                {
                    grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_CHART_NO].Visible = false;
                    grdBills.Columns[COLIDX_GRIDBILLS_CHART_NO].Visible = false;
                }
                else
                {
                    grdBills.Columns[COLIDX_GRIDBILLS_CHART_NO].Visible = true;
                }
                if (objSessionSystem.SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION != "1")
                {
                    ddlDateValues.Visible = false;
                    lblvisitdate.Visible = false;
                    lblfrom.Visible = false;
                    lblto.Visible = false;
                    txtVisitDate.Visible = false;
                    imgVisit.Visible = false;
                    btnSearch.Visible = false;
                    txtToVisitDate.Visible = false;
                    imgVisite1.Visible = false;
                    txtNumberOfDays.Visible = false;
                    extddlCaseType.Visible = false;
                    lblCaseType.Visible = false;
                    lblNoOfDays.Visible = false;
                    lblAppointmentid.Visible = false;
                    lblpatientid.Visible = false;
                    txtappointmentid.Visible = false;
                    txtpatientid.Visible = false;
                }
                else
                {
                    ddlDateValues.Visible = true;
                    lblvisitdate.Visible = true;
                    lblfrom.Visible = true;
                    lblto.Visible = true;
                    txtVisitDate.Visible = true;
                    imgVisit.Visible = true;
                    btnSearch.Visible = true;
                    txtToVisitDate.Visible = true;
                    imgVisite1.Visible = true;
                    txtNumberOfDays.Visible = true;
                    extddlCaseType.Visible = true;
                    lblCaseType.Visible = true;
                    lblNoOfDays.Visible = true;
                    lblAppointmentid.Visible = true;
                    lblpatientid.Visible = true;
                    txtappointmentid.Visible = true;
                    txtpatientid.Visible = true;
                }
            }
            txtVisit.Text = drpdown_Documents.SelectedValue.ToString();
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

        #region "check version readonly or not"
        string app_status = objSessionCompanyAppStatus.SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_paid_bills.aspx");
        }
        #endregion

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        int Page_index = grdpaidbills.PageNumberList.SelectedIndex;
        grdpaidbills.PageIndex = Page_index;
        grdpaidbills.XGridBind();
        con1.SelectedIndex = Page_index;
        img2.Visible = false;
    }
    protected void drpdown_Documents_SelectionChanged(object sender, EventArgs e)
    {

        grdpaidbills.XGridBindSearch();

    }


    private void BindGridQueryString(string szFlag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        txtVisit.Text = drpdown_Documents.SelectedValue.ToString();
        ReceivedReport.Visible = true;
        txtireortreceive.Text = szFlag;

        if (Session["CON_VAL"] != null)
        {
            string szIndex = Session["CON_VAL"].ToString();
            int iIndex = Convert.ToInt32(szIndex) - 1;
            if (iIndex == 0)
            {

                grdpaidbills.XGridBindSearch();
                grdpaidbills.Visible = true;
                btnnext.Visible = true;
                img2.Visible = false;
            }
            else
            {
                //grdpaidbills.XGridBindSearch();
                grdpaidbills.Visible = true;
                btnnext.Visible = true;
                try
                {
                    int Page_index = iIndex;
                    grdpaidbills.Start_Index = grdpaidbills.PageRowCount * Page_index + 1;
                    grdpaidbills.PageIndex = Page_index;
                    grdpaidbills.XGridBind();
                }
                catch { }
                // grdpaidbills.XGridBindSearch();
                // int Page_index = iIndex;
                // grdpaidbills.PageIndex = Page_index;
                // grdpaidbills.PageNumberList.SelectedIndex = Page_index + 1;
                // con1.SelectedIndex = Page_index + 1;
                // grdpaidbills.XGridBindSearch();
                // grdpaidbills.PageIndex = Page_index+1;
                // grdpaidbills.PageNumberList.SelectedIndex = Page_index + 1;
                // grdpaidbills.XGridBindSearch();
                ////
                Session["CON_VAL"] = null;
                img2.Visible = false;


            }

        }
        else
        {
            grdpaidbills.XGridBindSearch();
            grdpaidbills.Visible = true;
            btnnext.Visible = true;
        }
        _reportBO = new Bill_Sys_ReportBO();
        try
        {
            //grdpaidbills.DataSource = _reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyid.Text, szFlag);
            //grdpaidbills.DataBind();
            Bill_Sys_ProcedureCode_BO obj = new Bill_Sys_ProcedureCode_BO();

            string sIsShowProcedureCode = objSessionSystem.SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION;
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
            {
                szExportoExcelColumname.Append("Chart No,");
                szExportoExcelField.Append("SZ_CHART_NO,");
            }
            if (sIsShowProcedureCode != "1")
            {
                //Edit Procedure Code
                // grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_EDIT_PROCEDURE_CODE].Visible = false;
                //View Document
                btnnext.Visible = true;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_CHECKBOX].Visible = true;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_TESTDATA].Visible = false;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_DAYS].Visible = false;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_CASE_TYPE].Visible = false;
                //COLIDX_GRIDPAIDBILLS_CASE_TYPE
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_LHRCODE].Visible = false;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_VISIT_STATUS].Visible = true;
                drpdown_Documents.Visible = false;
                chkAOb.Visible = false;
                chkReferral.Visible = false;
                chkReport.Visible = false;
                chkLien.Visible = false;
                chkComp.Visible = false;
                szExportoExcelColumname.Append("Status,");
                szExportoExcelField.Append("SZ_STATUS,");
                grdpaidbills.ExportToExcelColumnNames = szExportoExcelColumname.ToString();
                //grdPatientList.Con = xcon;
                grdpaidbills.ExportToExcelFields = szExportoExcelField.ToString();
            }
            else
            {    //Edit Procedure Code
                //grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_EDIT_PROCEDURE_CODE].Visible = true;
                //View Document
                btnnext.Visible = false;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_CHECKBOX].Visible = false;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_TESTDATA].Visible = true;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_DAYS].Visible = true;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_LHRCODE].Visible = true;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_CASE_TYPE].Visible = true;
                grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_VISIT_STATUS].Visible = false;
                drpdown_Documents.Visible = true;
                chkAOb.Visible = true;
                chkReferral.Visible = true;
                chkReport.Visible = true;
                chkLien.Visible = true;
                chkComp.Visible = true;
                szExportoExcelColumname.Append("LHR Code,");
                szExportoExcelField.Append("SZ_LHR_CODE,");
                szExportoExcelColumname.Append("No. of Days,");
                szExportoExcelField.Append("I_NO_OF_DAYS,");
                szExportoExcelColumname.Append("Case Type,");
                szExportoExcelField.Append("SZ_CASE_TYPE_NAME,");
                szExportoExcelColumname.Append("Patient Id,");
                szExportoExcelField.Append("SZ_PATIENT_ID,");
                grdpaidbills.ExportToExcelColumnNames = szExportoExcelColumname.ToString();
                grdpaidbills.ExportToExcelFields = szExportoExcelField.ToString();


            }
            if (Session["SORT_DS"] != null)
            {
                Session["SORT_DS"] = null;
                Session["SORT_DS"] = (DataSet)_reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyid.Text, szFlag);
            }
            else
            {
                Session["SORT_DS"] = (DataSet)_reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyid.Text, szFlag);
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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (grdpaidbills.DataSource != null)
            Session["Pending_Report_Datatable"] = ((DataSet)(grdpaidbills.DataSource)).Tables[0];
        try
        {
            int Page_index = grdpaidbills.PageNumberList.SelectedIndex;
            grdpaidbills.PageIndex = Page_index;
            con1.SelectedIndex = Page_index;
            //  con1.SelectedIndex = grdpaidbills.PageIndex;

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
        if (grdBills.Visible == true)
        {
            try
            {
                BillsSum.DataSource = ((DataSet)(grdBills.DataSource)).Tables[2];
                BillsSum.DataBind();
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


        }
        Bill_Sys_ProcedureCode_BO objGetImageID = new Bill_Sys_ProcedureCode_BO();
        DataSet ds = objGetImageID.GetImgIdUsingEvenetProcId(txtCompanyid.Text);
        Session["Associate"] = ds.Tables[0];

        if (grdpaidbills.Visible == true)
        {
            DataView dv = null;
            for (int j = 0; j < grdpaidbills.Rows.Count; j++)
            {

                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION != "1")
                {
                    //lnkCaseno
                    LinkButton lnk = (LinkButton)grdpaidbills.Rows[j].FindControl("lnkCaseno");
                    lnk.Enabled = false;
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dv = ds.Tables[0].DefaultView;
                    dv.Sort = ds.Tables[0].Columns[0].ColumnName;
                    if (dv.Find(grdpaidbills.DataKeys[j]["I_EVENT_PROC_ID"].ToString().Trim()) != -1)
                    {
                        grdpaidbills.Rows[j].BackColor = System.Drawing.Color.Yellow;
                    }
                }

                if (grdpaidbills.Rows[j].Cells[10].Text.ToLower().Contains("unknown"))
                {
                    grdpaidbills.Rows[j].Cells[10].Text = "";
                    grdpaidbills.Rows[j].Cells[9].Text = "";
                }

            }

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindBillsGrid(string szFlag)
    {
        PaidBills.Visible = true;
        txtireortreceive.Text = szFlag;

        if (objSessionBillingCompany.BT_REFERRING_FACILITY == true)
            grdBills.Columns[COLIDX_GRIDBILLS_CHART_NO].SortExpression = "MST_PATIENT.I_RFO_CHART_NO";  //Chart No
        else
            grdBills.Columns[COLIDX_GRIDBILLS_CHART_NO].SortExpression = "MST_PATIENT.SZ_CHART_NO"; //Chart No
        grdBills.XGridBindSearch();
        grdBills.Visible = true;
        _reportBO = new Bill_Sys_ReportBO();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        // BindReffGrid();
        grdpaidbills.XGridBindSearch();
    }

    protected void btnSearchvisit_Click(object sender, EventArgs e)
    {
        //----Added by Kunal 24-Apr-2012------------------------------
        ArrayList arrPgeValue = new ArrayList();
        arrPgeValue.Add(txtVisitDate.Text);
        arrPgeValue.Add(txtToVisitDate.Text);
        arrPgeValue.Add(extddlCaseType.Text);
        arrPgeValue.Add(txtNumberOfDays.Text);
        arrPgeValue.Add(drpdown_Documents.SelectedValue);
        if (chkAOb.Checked)
        {
            arrPgeValue.Add("1");
        }
        else
        {
            arrPgeValue.Add("0");
        }

        if (chkReport.Checked)
        {
            arrPgeValue.Add("1");
        }
        else
        {
            arrPgeValue.Add("0");
        }

        if (chkReferral.Checked)
        {
            arrPgeValue.Add("1");
        }
        else
        {
            arrPgeValue.Add("0");
        }
        arrPgeValue.Add(con1.SelectedValue);
        if (chkLien.Checked)
        {
            arrPgeValue.Add("1");
        }
        else
        {
            arrPgeValue.Add("0");
        }
        if (chkComp.Checked)
        {
            arrPgeValue.Add("1");
        }
        else
        {
            arrPgeValue.Add("0");
        }
        Session["PAGE_VALUES"] = arrPgeValue;
        //-----------------------------------------------------------------
        this.BindGridQueryString("false");
    }

    protected void grdBills_RowBound(object sender, GridViewRowEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (objSessionSystem.SZ_NEW_BILL != "True")
            {

                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkSelectBill");
                if (lnk != null)
                {
                    lnk.Enabled = false;
                }
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
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdpaidbills.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
    protected void lnkExcelUnPaid_onclick(object sender, EventArgs e)
    {
        if (objSessionSystem.SZ_CHART_NO != "0")
        {
            grdBills.ExportToExcelFields = "sz_bill_number,sz_case_no,sz_chart_no,sz_patient_name,sz_insurance_name,dt_bill_date,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE";
            grdBills.ExportToExcelColumnNames = "Bill Number,Case #,Chart #,Patient name,Insurance Name,Bill Date,Bill Amount,Paid Amount,Balance";
        }
        else
        {
            grdBills.ExportToExcelFields = "sz_bill_number,sz_case_no,sz_patient_name,sz_insurance_name,dt_bill_date,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE";
            grdBills.ExportToExcelColumnNames = "Bill Number,Case #,Patient name,Insurance Name,Bill Date,Bill Amount,Paid Amount,Balance";
        }
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdBills.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }


    private void ConfigDashBoard()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO _objDashBoardBO = new DashBoardBO();
        try
        {

            DataTable dt = _objDashBoardBO.GetConfigDashBoard(objSessionUser.SZ_USER_ROLE);
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr[0].ToString())
                {
                    case "Daily Appointment":
                        tblDailyAppointment.Visible = true;
                        break;
                    case "Weekly Appointment":
                        tblWeeklyAppointment.Visible = true;
                        break;
                    case "Bill Status":
                        tblBillStatus.Visible = true;
                        break;
                    case "Desk":
                        tblDesk.Visible = true;
                        break;
                    case "Missing Information":
                        tblMissingInfo.Visible = true;
                        break;
                    case "Report Section":
                        tblReportSection.Visible = true;
                        break;
                    case "Procedure Status":
                        tblBilledUnbilledProcCode.Visible = true;
                        break;
                    case "Visits":
                        tblVisits.Visible = true;
                        grdTotalVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyid.Text, "TOTALCOUNT");
                        grdTotalVisit.DataBind();
                        grdVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyid.Text, "BILLEDVISIT");
                        grdVisit.DataBind();
                        grdUnVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyid.Text, "UNBILLEDVISIT");
                        grdUnVisit.DataBind();
                        break;
                    case "Missing Speciality":
                        tblMissingSpeciality.Visible = true;
                        break;
                    case "Patient Visit Status":
                        tblPatientVisitStatus.Visible = true;
                        break;

                }
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

    protected void grdpaidbills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();

        //DataView dv;
        //DataSet ds = new DataSet();
        //ds = (DataSet)Session["SORT_DS"];
        //dv = ds.Tables[0].DefaultView;

        if (e.CommandName.ToString() == "CaseNo")
        {

            int i = Convert.ToInt32(e.CommandArgument.ToString());

            hdnmdltxtCaseID.Text = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();
            caseId = hdnmdltxtCaseID.Text;
            companyID = txtCompanyid.Text;
            hdnmdltxtProcID.Text = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
            proc_id = hdnmdltxtProcID.Text;
            hdnmdltxtSpeciality.Text = grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString();
            if ((grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString().ToUpper() == "OT" || grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString() == ""))
            {
                extddlSpecialty.Visible = true;
            }
            else
            {
                extddlSpecialty.Visible = false;
            }

            try
            {
                Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                grdViewDocuments.DataSource = _bill_Sys_ProcedureCode_BO.GetDocsNew(caseId, txtCompanyid.Text, hdnmdltxtProcID.Text);
                grdViewDocuments.DataBind();
                Bill_Sys_Upload_VisitReport obj = new Bill_Sys_Upload_VisitReport();
                for (int i1 = 0; i1 < grdViewDocuments.Items.Count; i1++)
                {
                    string szDoctype = obj.GetFiledDocType(grdViewDocuments.Items[i1].Cells[3].Text, hdnmdltxtProcID.Text);
                    if (szDoctype != "")
                    {
                        DropDownList drp = (DropDownList)grdViewDocuments.Items[i1].FindControl("ddlreport");
                        CheckBox chk = (CheckBox)grdViewDocuments.Items[i1].FindControl("chkView");
                        drp.Text = szDoctype;
                        chk.Checked = true;
                    }
                }
                mpCaseNo.Show();

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

        if (e.CommandName.ToString() == "workarea")
        {
            int i = Convert.ToInt32(e.CommandArgument.ToString());

            Session["SZ_CASE_ID"] = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString(); //CASE_ID
            Session["PROVIDERNAME"] = grdpaidbills.DataKeys[i]["PATIENT_NAME"].ToString(); // PATIENT NAME
            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            _bill_Sys_CaseObject.SZ_PATIENT_ID = grdpaidbills.DataKeys[i]["SZ_PATIENT_ID"].ToString(); // PATIENT ID
            _bill_Sys_CaseObject.SZ_CASE_ID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();  // CASE ID
            _bill_Sys_CaseObject.SZ_CASE_NO = grdpaidbills.DataKeys[i]["CASE_NO"].ToString();  // CASE ID
            _bill_Sys_CaseObject.SZ_PATIENT_NAME = grdpaidbills.DataKeys[i]["PATIENT_NAME"].ToString();  // PATIENT NAME
            _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)_bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString())).SZ_COMPANY_ID; // COMPANY ID
            Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

            _bill_Sys_Case = new Bill_Sys_Case();
            _bill_Sys_Case.SZ_CASE_ID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString(); // CASE ID 

            Session["CASEINFO"] = _bill_Sys_Case;
            Response.Redirect("../Bill_Sys_StatusProceudure.aspx", false);
        }

        if (e.CommandName.ToString() == "appointment")
        {
            int i = Convert.ToInt32(e.CommandArgument.ToString());
            if (objSessionBillingCompany.BT_REFERRING_FACILITY == true)
            {
                Session["SZ_CASE_ID"] = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString(); //CASE_ID
                Session["PROVIDERNAME"] = grdpaidbills.DataKeys[i]["PATIENT_NAME"].ToString(); // PATIENT NAME
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = grdpaidbills.DataKeys[i]["SZ_PATIENT_ID"].ToString(); // PATIENT ID
                _bill_Sys_CaseObject.SZ_CASE_ID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString(); // CASE ID
                _bill_Sys_CaseObject.SZ_CASE_NO = grdpaidbills.DataKeys[i]["CASE_NO"].ToString();// CASE ID
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = grdpaidbills.DataKeys[i]["PATIENT_NAME"].ToString(); // PATIENT NAME
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)_bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString())).SZ_COMPANY_ID; // COMPANY ID
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();  // CASE ID 

                Session["CASEINFO"] = _bill_Sys_Case;

                DateTime dtEventTime = new DateTime();
                dtEventTime = Convert.ToDateTime(grdpaidbills.DataKeys[i]["DT_DATE_OF_SERVICE"]);  // EVENT DATE (DATE OF SERVICE)
                string szQueryString = "";
                szQueryString = "&idate=" + dtEventTime.ToShortDateString();
                Response.Redirect("Bill_Sys_AppointPatientEntry.aspx?Flag=true" + szQueryString, false);
            }
            else
            {

                Session["SZ_CASE_ID"] = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString(); //CASE_ID
                Session["PROVIDERNAME"] = grdpaidbills.DataKeys[i]["PATIENT_NAME"].ToString(); // PATIENT NAME
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = grdpaidbills.DataKeys[i]["SZ_PATIENT_ID"].ToString(); // PATIENT ID
                _bill_Sys_CaseObject.SZ_CASE_ID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString(); // CASE ID
                _bill_Sys_CaseObject.SZ_CASE_NO = grdpaidbills.DataKeys[i]["CASE_NO"].ToString();// CASE ID
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = grdpaidbills.DataKeys[i]["PATIENT_NAME"].ToString(); // PATIENT NAME
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)_bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString())).SZ_COMPANY_ID; // COMPANY ID
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString(); // CASE ID

                Session["CASEINFO"] = _bill_Sys_Case;

                DateTime dtEventTime = new DateTime();
                dtEventTime = Convert.ToDateTime(grdpaidbills.DataKeys[i]["DT_DATE_OF_SERVICE"]);// DATE OF SERVICE
                string szQueryString = "?_day=" + dtEventTime.ToShortDateString();
                szQueryString = szQueryString + "&idate=" + dtEventTime.ToShortDateString();

                Response.Redirect("Bill_Sys_ScheduleEvent.aspx" + szQueryString, false);
            }
        }
        if (e.CommandName.ToString() == "edit")
        {
            int i = Convert.ToInt32(e.CommandArgument.ToString());

            DataSet dsRoom = new DataSet();
            Bill_Sys_BillTransaction_BO objTransaction = new Bill_Sys_BillTransaction_BO();

            if (grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString() != "")
            {
                dsRoom = objTransaction.GetRoomId(grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString(), txtCompanyid.Text);
                string szRoomId = dsRoom.Tables[0].Rows[0][0].ToString();
                string ProcId = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
                Session["GETROOMID"] = szRoomId;
                Session["EVENTPROCID"] = ProcId;
            }
            else
            {
                string ProcId = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
                Session["GETROOMID"] = "All";
                Session["EVENTPROCID"] = ProcId;
            }

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mmupdateproc", "showProcPopup();", true);
        }
        if (e.CommandName.ToString() == "view")
        {
            int i = Convert.ToInt32(e.CommandArgument.ToString());
            string szCaseID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();
            string szEventProcID = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
            string szSpeciality = grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString();

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mmupdate", "ViewDocumentPopup('" + szCaseID + "','" + szEventProcID + "','" + szSpeciality + "');", true);
        }


        if (e.CommandName.ToString() == "Edit")
        {


            int i = Convert.ToInt32(e.CommandArgument.ToString());
            DataSet dsRoom = new DataSet();





            Bill_Sys_BillTransaction_BO objTransaction = new Bill_Sys_BillTransaction_BO();
            if ((grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString().ToUpper() != "OT" && grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString().ToUpper() != ""))
            {
                dsRoom = objTransaction.GetRoomId(grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString(), txtCompanyid.Text);
                string szRoomId = dsRoom.Tables[0].Rows[0][0].ToString();
                string ProcId1 = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
                Session["GETROOMID"] = szRoomId;
                Session["EVENTPROCID"] = ProcId1;
            }
            else
            {
                string ProcId1 = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
                Session["GETROOMID"] = "All";
                Session["EVENTPROCID"] = ProcId1;
            }


            string ProcId = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
            string szCaseID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();
            //string szEventProcID = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
            string procGId = grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
            string Patientid = grdpaidbills.DataKeys[i]["SZ_PATIENT_ID"].ToString();
            string EventID = grdpaidbills.DataKeys[i]["I_EVENT_ID"].ToString();
            string szSpeciality = grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString();
            //string patientname = grdpaidbills.Rows[i].Cells[7].Text;
            string patientname = grdpaidbills.DataKeys[i]["PATIENT_NAME"].ToString();
            //string dateofservice = grdpaidbills.Rows[i].Cells[8].Text;
            string dateofservice = grdpaidbills.DataKeys[i]["DT_DATE_OF_SERVICE"].ToString();
            string lhrcode = grdpaidbills.Rows[i].Cells[21].Text;
            //string caseno = grdpaidbills.Rows[i].Cells[2].Text;
            string caseno = grdpaidbills.DataKeys[i]["CASE_NO"].ToString();
            string szCaseType = grdpaidbills.Rows[i].Cells[24].Text;



            bool _ischeck = false;
            string _caseID = "";
            int _isSameCaseID = 0;
            string ProcGroupId = "";
            string PatientID = "";
            int _isSameProcGroupID = 0;
            ArrayList objArrOneD = new ArrayList();
            ArrayList arrEventID = new ArrayList();
            arrEventID.Add(grdpaidbills.DataKeys[i]["I_EVENT_ID"].ToString());


            Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();

            _dianosis_Association.EventProcID = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
            _dianosis_Association.DoctorID = grdpaidbills.DataKeys[i]["SZ_DOCTOR_ID"].ToString();
            _dianosis_Association.CaseID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();
            _dianosis_Association.ProceuderGroupId = grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
            _dianosis_Association.ProceuderGroupName = grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString();
            _dianosis_Association.PatientId = grdpaidbills.DataKeys[i]["SZ_PATIENT_ID"].ToString();
            _dianosis_Association.DateOfService = grdpaidbills.DataKeys[i]["DT_DATE_OF_SERVICE"].ToString();
            _dianosis_Association.ProcedureCode = grdpaidbills.DataKeys[i]["SZ_PROC_CODE"].ToString();
            _dianosis_Association.CompanyId = txtCompanyid.Text;
            objArrOneD.Add(_dianosis_Association);

            Session["DIAGNOS_ASSOCIATION_PAID"] = objArrOneD;

            DataSet dscode = new DataSet();
            dscode = objTransaction.GetRoomId(procGId, txtCompanyid.Text);
            string sz_proc_code = grdpaidbills.Rows[i].Cells[9].Text;
            string sz_proc_desc = grdpaidbills.Rows[i].Cells[10].Text;

            Bill_Sys_ProcedureCode_BO obj = new Bill_Sys_ProcedureCode_BO();
            DataSet dsSys = obj.Get_Sys_Key("SS00014", txtCompanyid.Text);
            if (dsSys.Tables[0].Rows[0][0].ToString() == "1")
            {
                Session["EVENT_ID"] = arrEventID;
            }
            ArrayList arrPgeValue = new ArrayList();
            arrPgeValue.Add(txtVisitDate.Text);
            arrPgeValue.Add(txtToVisitDate.Text);
            arrPgeValue.Add(extddlCaseType.Text);
            arrPgeValue.Add(txtNumberOfDays.Text);
            arrPgeValue.Add(drpdown_Documents.SelectedValue);
            if (chkAOb.Checked)
            {
                arrPgeValue.Add("1");
            }
            else
            {
                arrPgeValue.Add("0");
            }

            if (chkReport.Checked)
            {
                arrPgeValue.Add("1");
            }
            else
            {
                arrPgeValue.Add("0");
            }

            if (chkReferral.Checked)
            {
                arrPgeValue.Add("1");
            }
            else
            {
                arrPgeValue.Add("0");
            }
            arrPgeValue.Add(con1.SelectedValue);
            if (chkLien.Checked)
            {
                arrPgeValue.Add("1");
            }
            else
            {
                arrPgeValue.Add("0");
            }
            if (chkComp.Checked)
            {
                arrPgeValue.Add("1");
            }
            else
            {
                arrPgeValue.Add("0");
            }
            Session["PAGE_VALUES"] = arrPgeValue;
            img2.Visible = true;
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mmupdateproc", "showEditPopup('" + szCaseID + "','" + ProcId + "','" + procGId + "','" + Patientid + "','" + EventID + "','" + szSpeciality + "','" + Server.UrlEncode(sz_proc_desc) + "','" + sz_proc_code + "','" + szCaseType + "','" + patientname + "','" + dateofservice + "','" + lhrcode + "','" + caseno + "');", true);
        }
    }

    protected void grdViewDocuments_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string PathNotFound = " ";
        if (e.CommandName == "View")
        {
            string path = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
            string physicalpath = ConfigurationManager.AppSettings["FILE_URL"].ToString();
            string filepath = e.Item.Cells[2].Text;
            string filename = e.Item.Cells[3].Text;
            string fullpath = path + filepath + filename;
            string fullphysicalpath = physicalpath + filepath + filename;
            if (File.Exists(fullphysicalpath))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + fullpath + "');", true);
            }
            else
            {
                msg1.PutMessage("File is not available \n" + PathNotFound);
                msg1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                msg1.Show();

            }
            mpCaseNo.Show();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + fullpath + "');", true);
        }

    }

    protected void grdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int i = Convert.ToInt32(e.CommandArgument.ToString());
            _bill_Sys_Case = new Bill_Sys_Case();
            _bill_Sys_Case.SZ_CASE_ID = grdBills.DataKeys[i]["sz_case_id"].ToString();
            CaseDetailsBO _obj_caseDetailsBO = new CaseDetailsBO();
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            _bill_Sys_CaseObject.SZ_PATIENT_ID = _obj_caseDetailsBO.GetCasePatientID(_bill_Sys_Case.SZ_CASE_ID, "");
            _bill_Sys_CaseObject.SZ_CASE_ID = _bill_Sys_Case.SZ_CASE_ID;
            _bill_Sys_CaseObject.SZ_COMAPNY_ID = _obj_caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
            _bill_Sys_CaseObject.SZ_PATIENT_NAME = _obj_caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
            _bill_Sys_CaseObject.SZ_CASE_NO = grdBills.DataKeys[i]["sz_case_no"].ToString();

            Session["CASEINFO"] = _bill_Sys_Case;
            Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
            Session["PassedCaseID"] = _bill_Sys_Case.SZ_CASE_ID;
            Session["SZ_BILL_NUMBER"] = grdBills.DataKeys[i]["sz_bill_number"].ToString();
            Response.Redirect("Bill_Sys_BillTransaction.aspx?Type=Search", false);
        }
    }

    protected void grdBills_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void grdpaidbills_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool _ischeck = false;
        string _caseID = "";
        int _isSameCaseID = 0;
        string ProcGroupId = "";
        string PatientID = "";
        int _isSameProcGroupID = 0;
        ArrayList objArrOneD = new ArrayList();
        ArrayList arrEventID = new ArrayList();
        try
        {
            for (int j = 0; j < grdpaidbills.Rows.Count; j++)
            {
                CheckBox drp = (CheckBox)grdpaidbills.Rows[j].FindControl("chkSelect");
                if (drp.Checked == true)
                {
                    if (_isSameCaseID == 0)
                    {
                        _caseID = grdpaidbills.DataKeys[j]["SZ_CASE_ID"].ToString();
                        PatientID = grdpaidbills.DataKeys[j]["SZ_PATIENT_ID"].ToString();
                        _isSameCaseID = 1;
                    }
                    if (_caseID != Convert.ToString(grdpaidbills.DataKeys[j]["SZ_CASE_ID"].ToString()))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mmer", "alert('please select same patient !');", true);
                        return;

                    }
                    if (_isSameProcGroupID == 0)
                    {
                        ProcGroupId = grdpaidbills.DataKeys[j]["SZ_PROCEDURE_GROUP_ID"].ToString();
                        _isSameProcGroupID = 1;
                    }
                    if (ProcGroupId != (grdpaidbills.DataKeys[j]["SZ_PROCEDURE_GROUP_ID"].ToString()))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mmer1", "alert('please select same Speciality !');", true);
                        return;
                    }

                    arrEventID.Add(grdpaidbills.DataKeys[j]["I_EVENT_ID"].ToString());


                    Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();

                    _dianosis_Association.EventProcID = grdpaidbills.DataKeys[j]["I_EVENT_PROC_ID"].ToString();
                    _dianosis_Association.DoctorID = grdpaidbills.DataKeys[j]["SZ_DOCTOR_ID"].ToString();
                    _dianosis_Association.CaseID = grdpaidbills.DataKeys[j]["SZ_CASE_ID"].ToString();
                    _dianosis_Association.ProceuderGroupId = grdpaidbills.DataKeys[j]["SZ_PROCEDURE_GROUP_ID"].ToString();
                    _dianosis_Association.ProceuderGroupName = grdpaidbills.DataKeys[j]["sz_procedure_group"].ToString();
                    _dianosis_Association.PatientId = grdpaidbills.DataKeys[j]["SZ_PATIENT_ID"].ToString();
                    _dianosis_Association.DateOfService = grdpaidbills.DataKeys[j]["DT_DATE_OF_SERVICE"].ToString();
                    _dianosis_Association.ProcedureCode = grdpaidbills.DataKeys[j]["SZ_PROC_CODE"].ToString();
                    _dianosis_Association.CompanyId = txtCompanyid.Text;
                    objArrOneD.Add(_dianosis_Association);
                    _ischeck = true;
                }
            }

            if (_ischeck == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mmSelect", "alert('please select record from grid !');", true);
                //dt.Clear();
                objArrOneD.Clear();
                lblMsg.Text = "";
                lblMsg.Visible = false;
            }
            else
            {
                Session["DIAGNOS_ASSOCIATION_PAID"] = objArrOneD;

                DataSet ds = new DataSet();
                Bill_Sys_BillTransaction_BO objTransaction = new Bill_Sys_BillTransaction_BO();
                ds = objTransaction.GetRoomId(ProcGroupId, txtCompanyid.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Bill_Sys_ProcedureCode_BO obj = new Bill_Sys_ProcedureCode_BO();
                    DataSet dsSys = obj.Get_Sys_Key("SS00014", txtCompanyid.Text);
                    if (dsSys.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        string szRoomId = ds.Tables[0].Rows[0][0].ToString();
                        ArrayList arr = new ArrayList();
                        arr.Add(txtCompanyid.Text);
                        arr.Add(szRoomId);
                        Bill_Sys_ManageVisitsTreatmentsTests_BO obj1 = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
                        DataSet ProCod = obj1.GetReferringProcCodeList(arr);
                        Session["PROCEDURE_CODE"] = ProCod;
                        Session["EVENT_ID"] = arrEventID;
                    }
                    else
                    {
                        Session["PROCEDURE_CODE"] = null;
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mmPopup", "showReceiveDocumentPopup();", true);
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

    protected void btnUPdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        bool docSelected = true;
        try
        {
            bool chkDocCategory = true;


            if (extddlSpecialty.Visible && extddlSpecialty.Text == "NA")
            {

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds42d", "alert('Please Select Specialty.');", true);
                mpCaseNo.Show();
                return;
            }
            for (int i = 0; i < grdViewDocuments.Items.Count; i++)
            {
                if (((CheckBox)(grdViewDocuments.Items[i].Cells[0].FindControl("chkView"))).Checked == true)
                {
                    docSelected = true;
                    if (((DropDownList)(grdViewDocuments.Items[i].Cells[4].FindControl("ddlreport"))).SelectedValue == "8")
                    {
                        chkDocCategory = false;
                    }
                }
            }
            if (chkDocCategory == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds42d", "alert('Please Select Document Category.');", true);
                mpCaseNo.Show();
                return;
            }


            // ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds4", "alert('Doctor assign successfully.');", true);
            if (docSelected == true)
            {
                objNF3Template = new Bill_Sys_NF3_Template();
                String szDefaultPath = objNF3Template.getPhysicalPath();
                int ImageId = 0;
                string PathNotFound = "";
                foreach (DataGridItem drg in grdViewDocuments.Items)
                {
                    CheckBox drp = (CheckBox)drg.Cells[0].FindControl("chkView");
                    DropDownList drpReport = (DropDownList)drg.Cells[0].FindControl("ddlreport");
                    if (drp.Checked == true)
                    {
                        String szDestinationDir = "";

                        //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        if (objSessionBillingCompany.BT_REFERRING_FACILITY == true)
                        {
                            //szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            szDestinationDir = objNF3Template.GetCompanyName(objSessionBillingCompany.SZ_COMPANY_ID);
                        }
                        else
                        {
                            //szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            szDestinationDir = objNF3Template.GetCompanyName(objSessionBillingCompany.SZ_COMPANY_ID);

                        }
                        // szDestinationDir = szDestinationDir + "/" + txtCaseId.Text + "/No Fault File/Medicals/" + txtSpecility.Text + "/";
                        if (drpReport.SelectedValue == "0")
                        {
                            szDestinationDir = szDestinationDir + "/" + hdnmdltxtCaseID.Text + "/No Fault File/Medicals/" + hdnmdltxtSpeciality.Text + "/";
                        }
                        else if (drpReport.SelectedValue == "1")
                        {
                            szDestinationDir = szDestinationDir + "/" + hdnmdltxtCaseID.Text + "/No Fault File/Medicals/" + hdnmdltxtSpeciality.Text + "/Referral/";
                        }

                        else if (drpReport.SelectedValue == "2")
                        {
                            szDestinationDir = szDestinationDir + "/" + hdnmdltxtCaseID.Text + "/No Fault File/AOB/";
                        }
                        else if (drpReport.SelectedValue == "3")
                        {
                            szDestinationDir = szDestinationDir + "/" + hdnmdltxtCaseID.Text + "/No Fault File/Comp Authorization/";
                        }
                        else if (drpReport.SelectedValue == "4")
                        {
                            szDestinationDir = szDestinationDir + "/" + hdnmdltxtCaseID.Text + "/No Fault File/HIPPA Consent/";
                        }
                        else if (drpReport.SelectedValue == "5")
                        {
                            szDestinationDir = szDestinationDir + "/" + hdnmdltxtCaseID.Text + "/No Fault File/Lien Form/";
                        }
                        else if (drpReport.SelectedValue == "6")
                        {
                            szDestinationDir = szDestinationDir + "/" + hdnmdltxtCaseID.Text + "/No Fault File/MISC/";
                        }
                        else if (drpReport.SelectedValue == "7")
                        {
                            szDestinationDir = szDestinationDir + "/" + hdnmdltxtCaseID.Text + "/No Fault File/MISC/";
                        }


                        strLinkPath = drg.Cells[2].Text + drg.Cells[3].Text;
                        if (!Directory.Exists(szDefaultPath + szDestinationDir))
                        {
                            Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                        }



                        Bill_Sys_FileType_Settings _Bill_Sys_FileType_Settings = new Bill_Sys_FileType_Settings();
                        DataSet dscode = new DataSet();
                        dscode = _Bill_Sys_FileType_Settings.GET_IMAGE_ID(hdnmdltxtProcID.Text, drg.Cells[3].Text, txtCompanyid.Text);
                        if (dscode.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dscode.Tables[0].Rows.Count; i++)
                            {
                                ArrayList arr = new ArrayList();
                                arr.Add(dscode.Tables[0].Rows[i]["I_ID"].ToString());
                                arr.Add(hdnmdltxtProcID.Text);
                                _Bill_Sys_FileType_Settings.LhrDeleteDocuments(arr);


                            }

                            for (int i = 0; i < dscode.Tables[0].Rows.Count; i++)
                            {
                                // Bill_Sys_FileType_Settings _Bill_Sys_FileType_Settings = new Bill_Sys_FileType_Settings();
                                DataSet dsimageid = new DataSet();
                                dsimageid = _Bill_Sys_FileType_Settings.GET_IMAGE_ID_LHR(dscode.Tables[0].Rows[i]["I_IMAGE_ID"].ToString());
                                string sz_path = ConfigurationManager.AppSettings["BASEPATH"].ToString() + dscode.Tables[0].Rows[0]["SZ_FILE_PATH"].ToString() + dscode.Tables[0].Rows[0]["SZ_FILE_NAME"].ToString();
                                string szFinal = sz_path.Replace("/", "\\");
                                if (dsimageid.Tables[0].Rows.Count > 0)
                                {


                                }
                                else
                                {
                                    ArrayList arr = new ArrayList();
                                    arr.Add(dscode.Tables[0].Rows[i]["I_IMAGE_ID"].ToString());
                                    arr.Add(drg.Cells[3].Text);
                                    _Bill_Sys_FileType_Settings.Deletelhrdocuments(arr);
                                    string FinalPath = "";
                                    //szDefaultPath;
                                    try
                                    {
                                        System.IO.File.Move(@szFinal, @szFinal + ".deleted");
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

                                }
                            }





                        }


                        if (File.Exists(szDefaultPath + strLinkPath))
                        {

                            File.Copy(szDefaultPath + strLinkPath, szDefaultPath + szDestinationDir + drg.Cells[3].Text, true);
                            ArrayList objAL = new ArrayList();
                            //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                            if (objSessionBillingCompany.BT_REFERRING_FACILITY == true)
                            {
                                objAL.Add(objSessionBillingCompany.SZ_COMPANY_ID);
                            }
                            else
                            {
                                //objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                objAL.Add(objSessionBillingCompany.SZ_COMPANY_ID);
                            }

                            objAL.Add(hdnmdltxtCaseID.Text);
                            objAL.Add(drg.Cells[3].Text);
                            objAL.Add(szDestinationDir);
                            //objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add(objSessionUser.SZ_USER_NAME);
                            objAL.Add(hdnmdltxtSpeciality.Text);
                            // ImageId = objNF3Template.saveReportInDocumentManager(objAL);

                            if (drpReport.SelectedValue == "0")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager(objAL);
                            }
                            else if (drpReport.SelectedValue == "1")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager_Referral(objAL);
                            }
                            else if (drpReport.SelectedValue == "2")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager_AOB(objAL);
                            }
                            else if (drpReport.SelectedValue == "3")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager_NFCA(objAL);
                            }
                            else if (drpReport.SelectedValue == "4")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager_NFHC(objAL);
                            }
                            else if (drpReport.SelectedValue == "5")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager_NFLF(objAL);
                            }
                            else if (drpReport.SelectedValue == "6")
                            {
                                ImageId = saveReportInDocumentManager_NFMIS(objAL);
                            }
                            else if (drpReport.SelectedValue == "7")
                            {
                                if (File.Exists(szDefaultPath + strLinkPath))
                                {
                                    string szImgID = "";
                                    string szDestinationDir2 = "";
                                    szDestinationDir2 = drg.Cells[2].Text.Replace("/", "\\");
                                    szImgID = objNF3Template.GetImageID(drg.Cells[3].Text, drg.Cells[2].Text, szDestinationDir2);
                                    ImageId = Convert.ToInt32(szImgID);
                                }
                            }

                            if ((ImageId.ToString().Trim() != "0") && (ImageId.ToString().Trim() != ""))
                            {
                                _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                                _bill_Sys_ProcedureCode_BO.assignLHRDocument(objAL, ImageId, drpReport.SelectedValue, Convert.ToInt32(hdnmdltxtProcID.Text));
                            }

                            // End :   Save report under document manager.
                            //}
                            if (drpReport.SelectedValue == "0")
                            {
                                Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent;
                                ArrayList arrOBJ;

                                _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                                _bill_Sys_ProcedureCode_BO.UpdateReportProcedureCodeList(Convert.ToInt32(hdnmdltxtProcID.Text), strLinkPath, ImageId);
                            }
                            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds7", "alert('Document Received  successfully.');", true);
                        }
                        else
                        {
                            if (PathNotFound == "")
                            {
                                PathNotFound = szDefaultPath + strLinkPath + ",\n";
                            }
                            else
                            {
                                PathNotFound = PathNotFound + szDefaultPath + strLinkPath + ",\n";
                            }
                        }
                    }
                }
                if (PathNotFound != "")
                {
                    msg1.PutMessage("These file are not available.");
                    msg1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    msg1.Show();
                    mpCaseNo.Show();
                    // ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds10", "alert('These file are not available');", true);
                    return;
                }
            }
            string notAdded = "";
            if (notAdded == "")
                msg1.PutMessage(" Document Saved  successfully.");
            msg1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            msg1.Show();
            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds28", "alert('Saved  successfully.');", true);
            mpCaseNo.Show();

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

    protected void extddlSpecialty_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnmdltxtSpeciality.Text = extddlSpecialty.Selected_Text;
        mpCaseNo.Show();
    }

    public int saveReportInDocumentManager_NFMIS(ArrayList objAL)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        int returnValue = 0;
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_SAVE_REPORT_IN_DM_NFMIS", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
            if (objAL[5].ToString().Equals("X-RAY"))
            {
                sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
            }
            else
            {

                sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
            }
            SqlParameter sqlReturn = new SqlParameter();
            sqlReturn.Direction = ParameterDirection.ReturnValue;

            sqlCmd.Parameters.Add(sqlReturn);
            sqlCmd.ExecuteNonQuery();
            returnValue = (int)sqlReturn.Value;
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return returnValue;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

}