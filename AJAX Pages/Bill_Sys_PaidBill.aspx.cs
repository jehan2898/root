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
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;


public partial class Bill_Sys_PaidBills : PageBase
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private Bill_Sys_Case _bill_Sys_Case;
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    string strLinkPath = null;
    //ashutosh
    string paidId = "";
  
    Bill_Sys_NF3_Template objNF3Template;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.con.SourceGrid = grdPaidBillSearch;
            this.txtSearchBox.SourceGrid = grdPaidBillSearch;
            this.grdPaidBillSearch.Page = this.Page;
            this.grdPaidBillSearch.PageNumberList = this.con;

            this.conUnpaid.SourceGrid = grdUnpaidBill;
            this.txtUnpaidSrch.SourceGrid = grdUnpaidBill;
            this.grdUnpaidBill.Page = this.Page;
            this.grdUnpaidBill.PageNumberList = this.conUnpaid;
            
           // this.grdPaidBillSearch.Con = xcon;
           // this.grdPaidBillSearch.EnableViewState = false;
            if (Request.QueryString["popup"] != null)
            {
                if (Request.QueryString["popup"].ToString().Equals("done"))
                {
                    lblMsg.Text = "Report received successfully.";
                    lblMsg.Visible = true;
                }
            }
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
               // txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID; 
                if (lblMsg.Text.Equals(""))
                {
                    lblMsg.Visible = false;
                }
                setLabels();
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    grdCaseMaster.Columns[11].Visible = true;
                    grdCaseMaster.Columns[13].Visible = true;
                }
                if (Request.QueryString["Flag"] != null)
                {
                   if (Request.QueryString["Flag"].ToString().ToLower() == "paid")
                    {
                        SearchBilllist("GETPAIDLIST");
                       // lblHeader.Text = "Paid Bills";
                       //ashutosh ..adding xgrid..
                        tabcontainerPatientVisit.ActiveTabIndex = 0;
                        //this.grdPaidBillSearch.XGridBind();
                       //...
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "unpaid")
                    {
                        SearchBilllist("GETUNPAIDLIST");
                     
                        //lblHeader.Text = "Un-Paid Bills";
                        //ashutosh ..adding xgrid..
                        tabcontainerPatientVisit.ActiveTabIndex = 1;
                        //this.grdUnpaidBill.XGridBind();
                        //..
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missingattorney")
                    {
                        //searchcaselist("GET_MISSING_ATTORNEY_LIST");
                        //lblHeader.Text = "Missing Attorney";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missinginsurancecompany")
                    {
                        searchcaselist("GET_MISSING_INSURANCE_LIST");
                        //lblHeader.Text = "Missing Insurance Company";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missingprovider")
                    {
                        searchcaselist("GET_MISSING_PROVIDER_LIST");
                       // lblHeader.Text = "Missing Provider";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missingclaimnumber")
                    {
                        searchcaselist("GET_MISSING_CLAIM_LIST");
                        //lblHeader.Text = "Missing Claim Number";
                        grdCaseMaster.Columns[5].Visible = true;
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missingreportnumber")
                    {
                        searchcaselist("GET_MISSING_REPORT_LIST");
                        lblHeader.Text = "Missing Report Number";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "missingpolicyholder")
                    {
                        searchcaselist("GET_MISSING_POCLICY_HOLDER");
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
                                BindGridQueryString("False");
                                BindGridForExportToExcel("False");

                            }
                        }
                    }
                }

                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "0")
                {
                    grdAllReports.Columns[1].Visible = false;
                    grdExportToExcel.Columns[0].Visible = false;
                    grdBillSearch.Columns[4].Visible = false;
                    grdEEBillSearch.Columns[2].Visible = false; 
                    //ashutosh
                    grdUnpaidBill.Columns[4].Visible = false;
                }
                else
                {
                    grdBillSearch.Columns[4].Visible = true;
                    grdEEBillSearch.Columns[2].Visible = true;
                    //ashutosh
                    grdUnpaidBill.Columns[4].Visible = true;
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
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

    private void BindGridQueryString(string szFlag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        dvAllReport.Visible = true;
        grdAllReports.Visible = true;
        btnnext.Visible = true;
        //...ashutosh
        btnExportToExcel.Visible = true;
        tdPaidUnpaid.Visible = false;
        //...
        _reportBO = new Bill_Sys_ReportBO();
        try
        {
            grdAllReports.DataSource = _reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyID.Text, szFlag);
            grdAllReports.DataBind();
            if (Session["SORT_DS"] != null)
            {
                Session["SORT_DS"] = null;
                Session["SORT_DS"] = (DataSet)_reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyID.Text, szFlag);
            }
            else
            {
                Session["SORT_DS"] = (DataSet)_reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyID.Text, szFlag);
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

    protected void btnSpeciality_Click(object sender, EventArgs e)
    {
        Session["SpecialityID"] = hdnSpeciality.Value;
        Response.Redirect("Bill_Sys_SpecialityMissingReport.aspx", false);
    }

    #region "Bind Label For Dash Board"
    protected void setLabels()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO _obj = new DashBoardBO();
       Bill_Sys_BillTransaction_BO _billTransactionBO = new Bill_Sys_BillTransaction_BO();
        try
        {
            DayOfWeek day = Convert.ToDateTime(System.DateTime.Today.ToString()).DayOfWeek;
            int days = day - DayOfWeek.Sunday;

            DateTime start = Convert.ToDateTime(System.DateTime.Today.ToString()).AddDays(-days);
            DateTime end = start.AddDays(6);

            lblAppointmentToday.Text = _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "GET_APPOINTMENT");
            lblAppointmentWeek.Text = _obj.getAppoinmentCount(start.ToString(), end.ToString(), txtCompanyID.Text, "GET_APPOINTMENT");

            lblBillStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='/Ajax%20Pages/Bill_Sys_paid_Bills.aspx?Flag=Paid' onclick=\"javascript:OpenPage('Paid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_PAID_LIST_COUNT", txtCompanyID.Text) + "</a>";
            lblBillStatus.Text += " Paid Bills  </li>  <li> <a href='/Ajax%20Pages/Bill_Sys_paid_Bills.aspx?Flag=UnPaid' onclick=\"javascript:OpenPage('UnPaid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_UNPAID_LIST_COUNT", txtCompanyID.Text) + "</a> Un-Paid Bills </li></ul>";

            lblDesk.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' onclick=\"javascript:OpenPage('Litigation');\" > " + _billTransactionBO.GetCaseCount("SP_LITIGATION_WRITEOFF_DESK", "GET_LETIGATION_COUNT", txtCompanyID.Text) + "</a>" + " bills due for litigation";

            //lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=MissingProvider' onclick=\"javascript:OpenPage('MissingProvider');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_PROVIDER") + "</a>";
            //lblMissingInformation.Text += " provider information missing  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='/Ajax%20Pages/Bill_Sys_paid_Bills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            lblMissingInformation.Text += " insurance company missing </li>  <li> <a href='/Ajax%20Pages/Bill_Sys_paid_Bills.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_ATTORNEY") + "</a>";
            lblMissingInformation.Text += " attorney missing </li>  <li> <a href='/Ajax%20Pages/Bill_Sys_paid_Bills.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_CLAIM_NUMBER") + "</a> claim number missing </li>";
            lblMissingInformation.Text += "<li> <a href='/Ajax%20Pages/Bill_Sys_paid_Bills.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_REPORT_NUMBER") + "</a> report number missing </li>";
            lblMissingInformation.Text += "<li> <a href='/Ajax%20Pages/Bill_Sys_paid_Bills.aspx?Flag=MissingPolicyHolder'> " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_POLICY_HOLDER") + "</a> policy holder missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_ShowUnSentNF2.aspx' > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "UNSENTNF2") + "</a> unsent NF2 </li></ul>";
            

            //lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=R' onclick=\"javascript:OpenPage('Litigation');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a>" + " Received Report";
            lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='/Ajax%20Pages/Bill_Sys_ReffPaidBills.aspx' onclick=\"javascript:OpenPage('Litigation');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a>" + " Received Report";
            lblReport.Text += "</li>  <li> <a href='/Ajax%20Pages/Bill_Sys_paid_Bills.aspx?Flag=report&Type=P' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_PENDING_COUNT") + "</a> Pending Report </li></ul>";

            lblProcedureStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li>" + _obj.getBilledUnbilledProcCode(txtCompanyID.Text, "GET_BILLEDPROC") + " billed procedure codes";
            lblProcedureStatus.Text += "</li>  <li>" + _obj.getBilledUnbilledProcCode(txtCompanyID.Text, "GET_UNBILLEDPROC") + " Un-billed procedure codes </li></ul>";


            //lblVisits.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_VISIT_COUNT") + " Visits</li>";
            //lblVisits.Text += "<li>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_BILLED_VISIT_COUNT") + " Billed visits </li>";
            //lblVisits.Text += "<li>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT") + " Un-billed visits </li></ul>";

            lblTotalVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_VISIT_COUNT");
            lblBilledVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_BILLED_VISIT_COUNT");
            lblUnBilledVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT");

            // 8 April - add patient visit status block on page - sachin
            lblPatientVisitStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientscheduled' onclick=\"javascript:OpenPage('PatientScheduled');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_SCHEDULED_COUNT") + "</a> ";
            lblPatientVisitStatus.Text += " Patient Scheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientnoshows' onclick=\"javascript:OpenPage('PatientNoShows');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_NO_SHOWS") + "</a>";
            lblPatientVisitStatus.Text += " Patient No Shows </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientrescheduled' onclick=\"javascript:OpenPage('PatientRescheduled');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_RESCHEDULED") + "</a>";
            lblPatientVisitStatus.Text += " Patient Rescheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientvisitcompleted' onclick=\"javascript:OpenPage('PatientVisitcompleted');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_COMPLETED") + "</a>Patient Visit completed </li></ul>"; 



            DataTable dt = new DataTable();
            try
            {
                string companyID = txtCompanyID.Text;
                dt = _obj.getMissingSpecialityList(companyID);

                lblMissingSpecialityText.Text = "<table>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i % 4 == 0)
                    {
                        if (i != 0 && i % 4 == 0)
                        {
                            lblMissingSpecialityText.Text += "</tr>";
                        }
                        lblMissingSpecialityText.Text += "<tr><td><ul style=\"list-style-type:disc;padding-left:60px;\"><li><a href='#' onclick=\"javascript:OpenReport('" + dt.Rows[i][2].ToString() + "')\">" + dt.Rows[i][0].ToString() + "</a> - " + dt.Rows[i][1].ToString() + "</li></ul></td>";
                    }
                    else
                    {
                        lblMissingSpecialityText.Text += "<td><ul style=\"list-style-type:disc;padding-left:60px;\"><li><a href='#' onclick=\"javascript:OpenReport('" + dt.Rows[i][2].ToString() + "')\">" + dt.Rows[i][0].ToString() + "</a> - " + dt.Rows[i][1].ToString() + "</li><ul></td>";
                    }
                }
                lblMissingSpecialityText.Text += "</table>";
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
            ConfigDashBoard();

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

            DataTable dt = _objDashBoardBO.GetConfigDashBoard(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE);
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
                        grdTotalVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyID.Text, "TOTALCOUNT");
                        grdTotalVisit.DataBind();
                        grdVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyID.Text, "BILLEDVISIT");
                        grdVisit.DataBind();
                        grdUnVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyID.Text, "UNBILLEDVISIT");
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
    #endregion
    
    #region "grdAllReports"

    protected void grdAllReports_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO(); 
        DataView dv;
        DataSet ds = new DataSet();
        ds = (DataSet)Session["SORT_DS"];
        dv = ds.Tables[0].DefaultView;
        if (e.CommandName.ToString() == "workarea")
        {
            dvAllReport.Visible = true;
            Session["SZ_CASE_ID"] = e.Item.Cells[3].Text; //CASE_ID
            Session["PROVIDERNAME"] = e.Item.Cells[5].Text; // PATIENT NAME
            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[4].Text; // PATIENT ID
            _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[3].Text; // CASE ID
            _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[16].Text; // CASE ID
            _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[5].Text; // PATIENT NAME
            _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)_bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(e.Item.Cells[3].Text)).SZ_COMPANY_ID; // COMPANY ID
            Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

            _bill_Sys_Case = new Bill_Sys_Case();
            _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[3].Text; // CASE ID 

            Session["CASEINFO"] = _bill_Sys_Case;
            //ashutosh...
            Response.Redirect("../Bill_Sys_StatusProceudure.aspx", false);

        }

        if (e.CommandName.ToString() == "appointment")
        {
            dvAllReport.Visible = true;
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                Session["SZ_CASE_ID"] = e.Item.Cells[3].Text; //CASE_ID
                Session["PROVIDERNAME"] = e.Item.Cells[5].Text; // PATIENT NAME
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[4].Text; // PATIENT ID
                _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[3].Text; // CASE ID
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[16].Text; // CASE ID
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[5].Text; // PATIENT NAME
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)_bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(e.Item.Cells[3].Text)).SZ_COMPANY_ID; // COMPANY ID
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[3].Text; // CASE ID 

                Session["CASEINFO"] = _bill_Sys_Case;

                DateTime dtEventTime = new DateTime();
                dtEventTime = Convert.ToDateTime(e.Item.Cells[6].Text.ToString()); // EVENT DATE (DATE OF SERVICE)
                string szQueryString = "";
                szQueryString = "&idate=" + dtEventTime.ToShortDateString();
                Response.Redirect("../Bill_Sys_AppointPatientEntry.aspx?Flag=true" + szQueryString, false);
            }
            else
            {
                Session["SZ_CASE_ID"] = e.Item.Cells[3].Text; // CASE ID
                Session["PROVIDERNAME"] = e.Item.Cells[5].Text; // PATIENT NAME
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[4].Text; // PATIENT ID
                _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[3].Text; // CASE ID
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[16].Text; // CASE ID
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[5].Text; // PATIENT NAME
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)_bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(e.Item.Cells[1].Text)).SZ_COMPANY_ID;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[3].Text; // CASE ID

                Session["CASEINFO"] = _bill_Sys_Case;

                DateTime dtEventTime = new DateTime(); 
                dtEventTime = Convert.ToDateTime(e.Item.Cells[6].Text.ToString()); // DATE OF SERVICE
                string szQueryString = "?_day=" + dtEventTime.ToShortDateString();
                szQueryString = szQueryString + "&idate=" + dtEventTime.ToShortDateString();

                Response.Redirect("../Bill_Sys_ScheduleEvent.aspx" + szQueryString, false);
            }
        }

        #region "Grid Sorting"
        if (e.CommandName.ToString() == "CHART_NO")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "CASE_NO")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }

        else if (e.CommandName.ToString() == "PATIENT_NAME")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "DT_EVENT_DATE")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }

        dv.Sort = txtSort.Text;
        grdAllReports.DataSource = dv;
        grdAllReports.DataBind();
        #endregion



    }

    protected void grdAllReports_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdAllReports.CurrentPageIndex = e.NewPageIndex;
            if (Request.QueryString["Type"] != null)
            {
                if (Request.QueryString["Type"].ToString() == "R")
                {
                    BindGridQueryString("True");
                }
                else
                {
                    BindGridQueryString("False");
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


    #endregion

    #region "grdCaseMaster"

    private void searchcaselist(string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            DataSet objDSResult = new DataSet();
            objDSResult = _bill_Sys_BillingCompanyDetails_BO.SearchBills(flag, txtCompanyID.Text);
            
        
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
            {
                DataTable objDSLocationWise = new DataTable();
                objDSLocationWise = DisplayLocationInGrid(objDSResult);
                grdCaseMaster.DataSource = objDSLocationWise;
                grdCaseMaster.DataBind();

                for (int i = 0; i < grdCaseMaster.Items.Count; i++)
                {
                    string str = grdCaseMaster.Items[i].Cells[9].Text.ToString();
                    str = str.ToString().Trim();
                    if (str.ToString().Trim() == "&nbsp;")
                    {
                        ((Label)grdCaseMaster.Items[i].Cells[0].FindControl("lblLocationName")).Visible = true;
                        ((LinkButton)grdCaseMaster.Items[i].Cells[0].FindControl("lnkSelectCase2")).Visible = false;
                        ((LinkButton)grdCaseMaster.Items[i].Cells[0].FindControl("lnkPatientDesk")).Visible = false;

                    }
                }
            }
            else
            {
                grdCaseMaster.DataSource = objDSResult;
                grdCaseMaster.DataBind();  //

               

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

    protected void grdCaseMaster_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            if (e.CommandName.ToString() == "Select")
            {

                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[4].Text;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_CaseObject.SZ_CASE_NO = ((LinkButton)e.Item.FindControl("lnkSelectCase")).Text;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;
                Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
            }

            else if (e.CommandName.ToString() == "Bill Transaction")
            {

                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[4].Text;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;

                Response.Redirect("Bill_Sys_BillTransaction.aspx", false);
            }
            else if (e.CommandName.ToString() == "View Bills")
            {
                Session["SZ_CASE_ID"] = e.CommandArgument;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;

                Response.Redirect("Bill_Sys_BillSearch.aspx", false);
            }
            else if (e.CommandName.ToString() == "Document Manager")
            {
                // Create Session for document Manager
                Session["PassedCaseID"] = e.CommandArgument;
                String szURL = "";
                String szCaseID = Session["PassedCaseID"].ToString();
                Session["QStrCaseID"] = szCaseID;
                Session["Case_ID"] = szCaseID;
                Session["Archived"] = "0";
                Session["QStrCID"] = szCaseID;
                Session["SelectedID"] = szCaseID;
                Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["SN"] = "0";
                Session["LastAction"] = "vb_CaseInformation.aspx";
                szURL = "Document Manager/case/vb_CaseInformation.aspx";
                //    Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");
            }
            else if (e.CommandName.ToString() == "Calender Event")
            {

                LinkButton lnkPatient = (LinkButton)e.Item.Cells[0].FindControl("lnkSelectCase");
                Session["SZ_CASE_ID"] = lnkPatient.CommandArgument;
                Session["PROVIDERNAME"] = e.CommandArgument;

                Response.Redirect("Bill_Sys_ScheduleEvent.aspx", false);
                //Response.Redirect("Bill_Sys_CalendarEvent.aspx", false);
                //    string szTemplateURL="Bill_Sys_TemplateManager.aspx";
                //string szTemplateURL = "TemplateManager/Bill_Sys_GeneratePDF.aspx";
                //Session["PassedCaseID"] = e.CommandArgument;
                //Response.Write("<script language='javascript'>window.open('" + szTemplateURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
            }

            else if (e.CommandName.ToString() == "Template Manager")
            {
                Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                //       Session["PROVIDERNAME"] = e.Item.Cells[4].Text;
                //       Response.Redirect("TemplateManager/templates.aspx", false);

                String szURL = "";
                szURL = "TemplateManager/templates.aspx";


                //      Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");

                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");

                //    string szTemplateURL="Bill_Sys_TemplateManager.aspx";
                //string szTemplateURL = "TemplateManager/Bill_Sys_GeneratePDF.aspx";
                //Session["PassedCaseID"] = e.CommandArgument;
                //Response.Write("<script language='javascript'>window.open('" + szTemplateURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
            }
            else if (e.CommandName.ToString() == "Patient Desk")
            {

                LinkButton lnkPatient = (LinkButton)e.Item.Cells[32].FindControl("lnkPatientDesk");
                Session["SZ_CASE_ID"] = lnkPatient.CommandArgument;
                Session["PROVIDERNAME"] = e.CommandArgument;
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();

                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[4].Text;
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = txtCompanyID.Text;

                _bill_Sys_CaseObject.SZ_CASE_NO = ((LinkButton)e.Item.FindControl("lnkSelectCase")).Text;

                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;
                /////////////

                Response.Redirect("Bill_SysPatientDesk.aspx?Flag=true", false);

            }
            //else if (e.CommandName.ToString() == "PatientHistory")
            //{
            //    String szURL = "";
            //    Session["PassedCaseID"] = e.CommandArgument;
            //    szURL = "PatientHistory.aspx";
            //    Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");
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

    #endregion

    #region "grdBillSearch"

    private void SearchBilllist(string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //grdBillSearch.Visible = true;
        grdUnpaidBill.Visible = true;
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        decimal SumTotalBillAmount = 0;
        decimal SumTotalPaidAmount = 0;
        decimal SumTotalOutstandingAmount = 0;
        try
        {
            DataSet objNewDS = new DataSet();
            objNewDS = _bill_Sys_BillingCompanyDetails_BO.SearchBills(flag, txtCompanyID.Text);
           
           
            for (int i = 0; i < objNewDS.Tables[0].Rows.Count; i++)
            {
                SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objNewDS.Tables[0].Rows[i]["FLT_BILL_AMOUNT"].ToString());
                SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objNewDS.Tables[0].Rows[i]["PAID_AMOUNT"].ToString());
                SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objNewDS.Tables[0].Rows[i]["FLT_BALANCE"].ToString());

            }
            if (flag == "GETPAIDLIST")
            {
               // this.grdPaidBillSearch.XGridBind();
                this.grdPaidBillSearch.XGridBindSearch();
            }
            else if (flag == "GETUNPAIDLIST")
            {
              //  this.grdUnpaidBill.XGridBind();
                this.grdUnpaidBill.XGridBindSearch();
            }
            //grdBillSearch.DataSource = objNewDS;
            //grdBillSearch.DataBind();
           // grdPaidBillSearch.XGridBind();
            grdEEBillSearch.DataSource = objNewDS;
            grdEEBillSearch.DataBind();
           // grdUnpaidBill.DataSource = objNewDS;
           // grdUnpaidBill.DataBind();
            lblBalance.Visible = true;
            lblBalancevalue.Visible = true;
            lblBillAmount.Visible = true;
            lblBillAmountvalue.Visible = true;
            lblPaidAmount.Visible = true;
            lblPaidAmountvalue.Visible = true;
            lblBillAmountvalue.Text = "$" + SumTotalBillAmount.ToString("0.00");
            lblPaidAmountvalue.Text = "$" + SumTotalPaidAmount.ToString("0.00");
            lblBalancevalue.Text = "$" + SumTotalOutstandingAmount.ToString("0.00");

            //ashutosh...
            lblBalance1.Visible = true;
            lblBalancevalue1.Visible = true;
            lblBillAmount1.Visible = true;
            lblBillAmountvalue1.Visible = true;
            lblPaidAmount1.Visible = true;
            lblPaidAmountvalue1.Visible = true;
            lblBillAmountvalue1.Text = "$" + SumTotalBillAmount.ToString("0.00");
            lblPaidAmountvalue1.Text = "$" + SumTotalPaidAmount.ToString("0.00");
            lblBalancevalue1.Text = "$" + SumTotalOutstandingAmount.ToString("0.00");
            
            //
            if ((!txtSort.Text.Equals("")) || (!txtSearchOrder.Text.Equals("")))
            {
                DataTable dt = new DataTable();
                dt = objNewDS.Tables[0].Rows[0].Table;
                dt.DefaultView.Sort = txtSort.Text;
                grdBillSearch.DataSource = dt;
                grdBillSearch.DataBind();

            }
           
            //No need for total 
            //DataRow objDR;
            //objDR = objNewDS.Tables[0].NewRow();
            //objDR["SZ_BILL_NUMBER"] = "";
            //objDR["SZ_CASE_NO"] = "<b>Total</b>";
            //objDR["FLT_BILL_AMOUNT"] = SumTotalBillAmount;
            //objDR["PAID_AMOUNT"] = SumTotalPaidAmount;
            //objDR["FLT_BALANCE"] = SumTotalOutstandingAmount;
            //objNewDS.Tables[0].Rows.InsertAt(objDR, 0);
            //grdBillSearch.DataSource = objNewDS;
            //grdBillSearch.DataBind();
           
           
            
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

    protected void grdBillSearch_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Edit")
            {
                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[7].Text.ToString();
                CaseDetailsBO _obj_caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _obj_caseDetailsBO.GetCasePatientID(e.Item.Cells[7].Text.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[7].Text.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _obj_caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _obj_caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[3].Text.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["PassedCaseID"] = e.Item.Cells[7].Text;
                Session["SZ_BILL_NUMBER"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_BillTransaction.aspx?Type=Search", false);
            }


            #region "Grid Sorting"
            if (e.CommandName.ToString() == "CHART_NO")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "PATIENT_NAME")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "InsuranceName")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }


            else if (e.CommandName.ToString() == "BILLNUMBER")
            {
                if (txtSearchOrder.Text == "SZ_BILL_NUMBER_SORT ASC")
                {
                    txtSearchOrder.Text = "SZ_BILL_NUMBER_SORT" + "  DESC";
                    txtSort.Text = txtSearchOrder.Text;
                }
                else
                {
                    txtSearchOrder.Text = "SZ_BILL_NUMBER_SORT" + " ASC";
                    txtSort.Text = txtSearchOrder.Text;
                }

            }
            else if (e.CommandName.ToString() == "CASENUMBER")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "BILLDATE")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "BILLAMOUNT")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "PAIDAMOUNT")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "BALANCE")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }

            DataSet ds = new DataSet();
             Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails = new Bill_Sys_BillingCompanyDetails_BO();

            if (Request.QueryString["Flag"].ToString().ToLower() == "paid")
            {
                ds = _bill_Sys_BillingCompanyDetails.SearchBills("GETPAIDLIST", txtCompanyID.Text);
              
            }
            else if (Request.QueryString["Flag"].ToString().ToLower() == "unpaid")
            {
                ds = _bill_Sys_BillingCompanyDetails.SearchBills("GETUNPAIDLIST", txtCompanyID.Text);
            }

            
            //DataView objView = ds.Tables[0].DefaultView;
            //objView.Sort = txtSort.Text;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable objTable = new DataTable();
                objTable = ds.Tables[0].Rows[0].Table;
                objTable.DefaultView.Sort = txtSort.Text;
                _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                decimal SumTotalBillAmount = 0;
                decimal SumTotalPaidAmount = 0;
                decimal SumTotalOutstandingAmount = 0;


                for (int i = 0; i < objTable.Rows.Count; i++)
                {
                    SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(ds.Tables[0].Rows[i]["FLT_BILL_AMOUNT"].ToString());
                    SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(ds.Tables[0].Rows[i]["PAID_AMOUNT"].ToString());
                    SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(ds.Tables[0].Rows[i]["FLT_BALANCE"].ToString());

                }
                grdBillSearch.CurrentPageIndex = 0;
                grdBillSearch.DataSource = objTable;
                grdBillSearch.DataBind();
              
          
            }

               


            #endregion
           
            
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

    protected void grdBillSearch_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_NEW_BILL != "True")
            {
                LinkButton lnk = (LinkButton)e.Item.FindControl("lnkSelectCase");
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

   
    
    protected void grdBillSearch_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdBillSearch.CurrentPageIndex = e.NewPageIndex;
            if (Request.QueryString["Flag"].ToString().ToLower() == "paid")
            {
                lblHeader.Text = "Paid Bills";
                SearchBilllist("GETPAIDLIST");
            }
            else if (Request.QueryString["Flag"].ToString().ToLower() == "unpaid")
            {
                lblHeader.Text = "Un-Paid Bills";
                SearchBilllist("GETUNPAIDLIST");
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
    
    #endregion

    #region "Export to excel"

    private void ExportToExcelForCaseMaster()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdCaseMaster.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdCaseMaster.Columns.Count; i++)
                    {
                        if (grdCaseMaster.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdCaseMaster.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdCaseMaster.Columns.Count; j++)
                {
                    if (grdCaseMaster.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");


                        if (j == 1)
                        {
                            strHtml.Append(grdCaseMaster.Items[icount].Cells[33].Text);
                            if (((Label)grdCaseMaster.Items[icount].Cells[j].FindControl("lblLocationName")).Visible == true)
                            {
                                strHtml.Append("<b>Location</b>");
                            }
                        }

                        else
                        {
                            strHtml.Append(grdCaseMaster.Items[icount].Cells[j].Text);
                        }
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);


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

    private void ExportToExcelForBillSearch()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            strHtml.Append("<tr>");
            strHtml.Append("<td>");
            strHtml.Append("<b>" + lblBillAmount.Text + " " + "</b>" + lblBillAmountvalue.Text + "</td>");
            strHtml.Append("<td><b>" + lblPaidAmount.Text + " " + "</b>" + lblPaidAmountvalue.Text + "</td>");
            strHtml.Append("<td><b>" + lblBalance.Text + " " + "</b>" + lblBalancevalue.Text);
            strHtml.Append("</td>");
            strHtml.Append("</tr>");
            strHtml.Append("</table>");
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdEEBillSearch.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdEEBillSearch.Columns.Count; i++)
                    {
                        if (grdEEBillSearch.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdEEBillSearch.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdEEBillSearch.Columns.Count; j++)
                {
                    if (grdEEBillSearch.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdEEBillSearch.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);


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

    private void ExportToExcelForAllReport()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdAllReports.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdAllReports.Columns.Count; i++)
                    {
                        if (grdAllReports.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdAllReports.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdAllReports.Columns.Count; j++)
                {
                    if (grdAllReports.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        if (j == 4)
                        {
                            strHtml.Append(grdAllReports.Items[icount].Cells[2].Text);
                        }
                        else if (j == 5)
                        {
                            strHtml.Append(grdAllReports.Items[icount].Cells[3].Text);
                        }
                        else
                            strHtml.Append(grdAllReports.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);


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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if ((Request.QueryString["Flag"].ToString().ToLower() == "paid") || (Request.QueryString["Flag"].ToString().ToLower() == "unpaid"))
            {
                ExportToExcelForBillSearch();
            }
            else if (Request.QueryString["Flag"].ToString().ToLower() == "report")
            {
                if (Request.QueryString["Type"] != null)
                {
                    if (Request.QueryString["Type"].ToString() == "P")
                    {
                        ExportToExcelForPendingBills();
                    }
                    else
                    {
                        ExportToExcelForAllReport();    
                    }
                }
                
            }
             
            else
            {
                ExportToExcelForCaseMaster();
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
    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }


    #endregion


    #region "Display Location wise patient in grid"

    public DataTable  DisplayLocationInGrid(DataSet p_objDS)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet objDS = new DataSet();
        objDS = p_objDS;
        DataTable objDT = new DataTable();
        try
        {
            objDT.Columns.Add("SZ_CASE_ID");
            objDT.Columns.Add("SZ_CASE_NAME");
            objDT.Columns.Add("SZ_CASE_NO");
            objDT.Columns.Add("SZ_CASE_TYPE_ID");
            objDT.Columns.Add("SZ_CASE_TYPE_NAME");
            objDT.Columns.Add("SZ_PROVIDER_ID");
            objDT.Columns.Add("SZ_PROVIDER_NAME");
            objDT.Columns.Add("SZ_OFFICE_ID");
            objDT.Columns.Add("SZ_PATIENT_ID");
            objDT.Columns.Add("SZ_OFFICE_NAME");
            objDT.Columns.Add("SZ_PATIENT_NAME"); 
            objDT.Columns.Add("SZ_DOCTOR_ID");
            objDT.Columns.Add("SZ_CLAIM_AMOUNT");
            objDT.Columns.Add("SZ_DOCTOR_NAME");
            objDT.Columns.Add("SZ_PAID_AMOUNT");
            objDT.Columns.Add("SZ_BALANCE");
            objDT.Columns.Add("SZ_APPOINTMENT");
            objDT.Columns.Add("SZ_CASE_STATUS_ID");
            objDT.Columns.Add("SZ_INSURANCE_NAME");
            objDT.Columns.Add("SZ_INSURANCE_ID");
            objDT.Columns.Add("SZ_STATUS_NAME");
            objDT.Columns.Add("SZ_ATTORNEY_ID");
            objDT.Columns.Add("SZ_ATTORNEY_FIRST_NAME");
            objDT.Columns.Add("SZ_ADJUSTER_NAME");
            objDT.Columns.Add("SZ_ADJUSTER_ID");
            objDT.Columns.Add("SZ_CLAIM_NUMBER");
            objDT.Columns.Add("SZ_POLICY_NUMBER");
            objDT.Columns.Add("DT_DATE_OF_ACCIDENT");
            objDT.Columns.Add("BT_ASSOCIATE_DIAGNOSIS_CODE");
            objDT.Columns.Add("SZ_LOCATION_NAME");
            objDT.Columns.Add("TOTAL_DIAGNOSIS_CODE_COUNT");
             
            DataRow objDR;
            string sz_Location_Name = "NA";

            for (int i = 0; i < objDS.Tables[0].Rows.Count; i++)
            {
                if (objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString().Equals(sz_Location_Name))
                {
                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    objDR["SZ_CASE_NAME"] = objDS.Tables[0].Rows[i]["SZ_CASE_NAME"].ToString();
                    objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_CASE_TYPE_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_TYPE_ID"].ToString();
                    objDR["SZ_CASE_TYPE_NAME"] = objDS.Tables[0].Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    objDR["SZ_PROVIDER_ID"] = objDS.Tables[0].Rows[i]["SZ_PROVIDER_ID"].ToString();
                    objDR["SZ_PROVIDER_NAME"] = objDS.Tables[0].Rows[i]["SZ_PROVIDER_NAME"].ToString();
                    objDR["SZ_OFFICE_ID"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_ID"].ToString();
                    objDR["SZ_OFFICE_NAME"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_NAME"].ToString();
                    objDR["SZ_DOCTOR_ID"] = objDS.Tables[0].Rows[i]["SZ_DOCTOR_ID"].ToString();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_CLAIM_AMOUNT"] = objDS.Tables[0].Rows[i]["SZ_CLAIM_AMOUNT"].ToString();
                    objDR["SZ_DOCTOR_NAME"] = objDS.Tables[0].Rows[i]["SZ_DOCTOR_NAME"].ToString();
                    objDR["SZ_PAID_AMOUNT"] = objDS.Tables[0].Rows[i]["SZ_PAID_AMOUNT"].ToString();
                    objDR["SZ_BALANCE"] = objDS.Tables[0].Rows[i]["SZ_BALANCE"].ToString();
                    objDR["SZ_APPOINTMENT"] = objDS.Tables[0].Rows[i]["SZ_APPOINTMENT"].ToString();
                    objDR["SZ_CASE_STATUS_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_STATUS_ID"].ToString();
                    objDR["SZ_INSURANCE_NAME"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    objDR["SZ_INSURANCE_ID"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_ID"].ToString();
                    objDR["SZ_STATUS_NAME"] = objDS.Tables[0].Rows[i]["SZ_STATUS_NAME"].ToString();
                    objDR["SZ_ATTORNEY_ID"] = objDS.Tables[0].Rows[i]["SZ_ATTORNEY_ID"].ToString();
                    objDR["SZ_ATTORNEY_FIRST_NAME"] = objDS.Tables[0].Rows[i]["SZ_ATTORNEY_FIRST_NAME"].ToString();
                    objDR["SZ_ADJUSTER_NAME"] = objDS.Tables[0].Rows[i]["SZ_ADJUSTER_NAME"].ToString();
                    objDR["SZ_ADJUSTER_ID"] = objDS.Tables[0].Rows[i]["SZ_ADJUSTER_ID"].ToString();
                    objDR["SZ_CLAIM_NUMBER"] = objDS.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString();
                    objDR["SZ_POLICY_NUMBER"] = objDS.Tables[0].Rows[i]["SZ_POLICY_NUMBER"].ToString();
                    string str = objDS.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    string d2 = "";
                    if (str.ToString() != "")
                    {
                        DateTime d1 = Convert.ToDateTime(str);

                        d2 = d1.ToString("ddMMMyyyy");

                    }
                    objDR["DT_DATE_OF_ACCIDENT"] = d2.ToString();
                    objDR["BT_ASSOCIATE_DIAGNOSIS_CODE"] = objDS.Tables[0].Rows[i]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString();
                    objDR["SZ_LOCATION_NAME"] = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();
                    objDR["TOTAL_DIAGNOSIS_CODE_COUNT"] = objDS.Tables[0].Rows[i]["TOTAL_DIAGNOSIS_CODE_COUNT"].ToString();
                    objDT.Rows.Add(objDR);
                    sz_Location_Name = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();

                     
                }
                else
                {
                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_NO"] = "<b>Location Name</b>";
                    objDR["SZ_PATIENT_NAME"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString() + "<b>";
                    objDR["SZ_CASE_NO"] = "";
                    objDR["SZ_CASE_TYPE_ID"] = "";
                    objDR["SZ_CASE_TYPE_NAME"] = "";
                    objDR["SZ_PROVIDER_ID"] = "";
                    objDR["SZ_PROVIDER_NAME"] = "";
                    objDR["SZ_OFFICE_ID"] = "";
                    objDR["SZ_CASE_NAME"] = "";
                    objDR["SZ_DOCTOR_ID"] = "";
                    objDR["SZ_OFFICE_NAME"] = "";
                    objDR["SZ_DOCTOR_NAME"] = "";
                    objDR["SZ_CLAIM_AMOUNT"] = "";
                    objDR["SZ_PAID_AMOUNT"] = "";
                    objDR["SZ_BALANCE"] = "";
                    objDR["SZ_APPOINTMENT"] ="";
                    objDR["SZ_INSURANCE_NAME"] = "";
                    objDR["SZ_INSURANCE_ID"] = "";
                    objDR["SZ_STATUS_NAME"] = "";
                    objDR["SZ_ATTORNEY_ID"] = "";
                    objDR["SZ_ATTORNEY_FIRST_NAME"] = "";
                    objDR["SZ_ADJUSTER_NAME"] = "";
                    objDR["SZ_ADJUSTER_ID"] = "";
                    objDR["SZ_CLAIM_NUMBER"] = "";
                    objDR["SZ_POLICY_NUMBER"] = "";
                    objDR["DT_DATE_OF_ACCIDENT"] = "";
                    objDR["BT_ASSOCIATE_DIAGNOSIS_CODE"] = "";
                    objDR["SZ_LOCATION_NAME"] = "";
                    objDR["TOTAL_DIAGNOSIS_CODE_COUNT"] = "";
                    objDR["SZ_CASE_STATUS_ID"] = "";
                    int count=grdCaseMaster.Items.Count;
                 
                    objDT.Rows.Add(objDR);
                    
                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    objDR["SZ_CASE_NAME"] = objDS.Tables[0].Rows[i]["SZ_CASE_NAME"].ToString();
                    objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_CASE_TYPE_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_TYPE_ID"].ToString();
                    objDR["SZ_CASE_TYPE_NAME"] = objDS.Tables[0].Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    objDR["SZ_PROVIDER_ID"] = objDS.Tables[0].Rows[i]["SZ_PROVIDER_ID"].ToString();
                    objDR["SZ_PROVIDER_NAME"] = objDS.Tables[0].Rows[i]["SZ_PROVIDER_NAME"].ToString();
                    objDR["SZ_OFFICE_ID"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_ID"].ToString();
                    objDR["SZ_OFFICE_NAME"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_NAME"].ToString();
                    objDR["SZ_DOCTOR_ID"] = objDS.Tables[0].Rows[i]["SZ_DOCTOR_ID"].ToString();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_CLAIM_AMOUNT"] = objDS.Tables[0].Rows[i]["SZ_CLAIM_AMOUNT"].ToString();
                    objDR["SZ_DOCTOR_NAME"] = objDS.Tables[0].Rows[i]["SZ_DOCTOR_NAME"].ToString();
                    objDR["SZ_PAID_AMOUNT"] = objDS.Tables[0].Rows[i]["SZ_PAID_AMOUNT"].ToString();
                    objDR["SZ_BALANCE"] = objDS.Tables[0].Rows[i]["SZ_BALANCE"].ToString();
                    objDR["SZ_APPOINTMENT"] = objDS.Tables[0].Rows[i]["SZ_APPOINTMENT"].ToString();
                    objDR["SZ_CASE_STATUS_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_STATUS_ID"].ToString();
                    objDR["SZ_INSURANCE_NAME"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    objDR["SZ_INSURANCE_ID"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_ID"].ToString();
                    objDR["SZ_STATUS_NAME"] = objDS.Tables[0].Rows[i]["SZ_STATUS_NAME"].ToString();
                    objDR["SZ_ATTORNEY_ID"] = objDS.Tables[0].Rows[i]["SZ_ATTORNEY_ID"].ToString();
                    objDR["SZ_ATTORNEY_FIRST_NAME"] = objDS.Tables[0].Rows[i]["SZ_ATTORNEY_FIRST_NAME"].ToString();
                    objDR["SZ_ADJUSTER_NAME"] = objDS.Tables[0].Rows[i]["SZ_ADJUSTER_NAME"].ToString();
                    objDR["SZ_ADJUSTER_ID"] = objDS.Tables[0].Rows[i]["SZ_ADJUSTER_ID"].ToString();
                    objDR["SZ_CLAIM_NUMBER"] = objDS.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString();
                    objDR["SZ_POLICY_NUMBER"] = objDS.Tables[0].Rows[i]["SZ_POLICY_NUMBER"].ToString();
                    string str = objDS.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    string d2="";
                    if (str.ToString() != "")
                    {
                        DateTime d1 = Convert.ToDateTime(str);
                         
                          d2 = d1.ToString("ddMMMyyyy");
                         
                    }

                    objDR["DT_DATE_OF_ACCIDENT"] = d2.ToString();
                    objDR["BT_ASSOCIATE_DIAGNOSIS_CODE"] = d2.ToString();
                    objDR["SZ_LOCATION_NAME"] = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();
                    objDR["TOTAL_DIAGNOSIS_CODE_COUNT"] = objDS.Tables[0].Rows[i]["TOTAL_DIAGNOSIS_CODE_COUNT"].ToString();
                    objDT.Rows.Add(objDR);
                    
                    sz_Location_Name = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();

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
        return objDT;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion
    //protected void btnUploadFile_Click(object sender, EventArgs e)
    //{
    //    objNF3Template = new Bill_Sys_NF3_Template();
    //    try
    //    {
    //        String szDefaultPath = objNF3Template.getPhysicalPath();
    //        foreach (DataGridItem drg in grdAllReports.Items)
    //        {
    //            CheckBox drp = (CheckBox)drg.Cells[0].FindControl("chkSelect");
    //            if (drp.Checked == true)
    //            {

    //                String szDestinationDir = "";

    //                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
    //                {
    //                    szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                }
    //                else
    //                {
    //                    szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);

    //                }
    //                szDestinationDir = szDestinationDir + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/No Fault File/Medicals/" + drg.Cells[12].Text + "/";
    //                strLinkPath = szDestinationDir + fuUploadReport.FileName;
    //                if (!Directory.Exists(szDefaultPath + szDestinationDir))
    //                {
    //                    Directory.CreateDirectory(szDefaultPath + szDestinationDir);
    //                }
    //                if (!File.Exists(szDefaultPath + szDestinationDir + fuUploadReport.FileName))
    //                {
    //                    fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);
    //                    // Start : Save report under document manager.

    //                    ArrayList objAL = new ArrayList();
    //                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
    //                    {
    //                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                    }
    //                    else
    //                    {
    //                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
    //                    }

    //                    objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
    //                    objAL.Add(fuUploadReport.FileName);
    //                    objAL.Add(szDestinationDir);
    //                    objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
    //                    objAL.Add(drg.Cells[12].Text);
    //                    objNF3Template.saveReportInDocumentManager(objAL);
    //                    // End :   Save report under document manager.
    //                }
    //            }
    //        }

    //        Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent;
    //        ArrayList arrOBJ;
    //        foreach (DataGridItem drg in grdAllReports.Items)
    //        {
    //            CheckBox drp = (CheckBox)drg.Cells[0].FindControl("chkSelect");
    //            if (drp.Checked == true)
    //            {
    //                _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
    //                _bill_Sys_ProcedureCode_BO.UpdateReportProcedureCodeList(Convert.ToInt32(drg.Cells[1].Text), strLinkPath);
    //            }
    //        }
    //        GetProcedureList(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    private void GetProcedureList(string caseId, string companyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            grdAllReports.DataSource = _bill_Sys_ProcedureCode_BO.GetStatusProcedureCodeList(caseId, companyID);
            grdAllReports.DataBind();
            foreach (DataGridItem drg in grdAllReports.Items)
            {

                if (drg.Cells[6].Text == "Ready For Bill")
                {
                    CheckBox drp = (CheckBox)drg.Cells[0].FindControl("chkSelect");
                    drp.Enabled = false;
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
            string ProcGroupId="";
            int _isSameProcGroupID = 0;
            ArrayList objArrOneD = new ArrayList();
           try
            {
         
                foreach (DataGridItem drg in grdAllReports.Items)
                {
                    CheckBox drp = (CheckBox)drg.Cells[0].FindControl("chkSelect");
                    if (drp.Checked == true)
                    {

                        if (_isSameCaseID == 0)
                        {
                            _caseID = drg.Cells[3].Text;
                            _isSameCaseID = 1;
                        }
                        if (_caseID != Convert.ToString(drg.Cells[3].Text))
                        {
                            Page.RegisterStartupScript("pp", "<script language='javascript'> alert('please select same patient !');</script>");
                            return;

                        }
                        if (_isSameProcGroupID == 0)
                        {
                            ProcGroupId = drg.Cells[18].Text;
                            _isSameProcGroupID =1;
                        }
                        if (ProcGroupId != (drg.Cells[18].Text))
                        {
                            Page.RegisterStartupScript("pp", "<script language='javascript'> alert('please select same Speciality !');</script>");
                            return;
                        }

                        Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();

                        _dianosis_Association.EventProcID = drg.Cells[13].Text;
                        _dianosis_Association.DoctorID = drg.Cells[15].Text;
                        _dianosis_Association.CaseID = drg.Cells[3].Text;
                        _dianosis_Association.ProceuderGroupId = drg.Cells[18].Text;
                        _dianosis_Association.ProceuderGroupName = drg.Cells[19].Text;
                        _dianosis_Association.PatientId = drg.Cells[4].Text;
                        _dianosis_Association.DateOfService = drg.Cells[6].Text;
                        _dianosis_Association.ProcedureCode = drg.Cells[14].Text;
                        _dianosis_Association.CompanyId = drg.Cells[12].Text;
                        objArrOneD.Add(_dianosis_Association);
                        

                       
                        _ischeck = true;

                    }

                }
                if (_ischeck == false)
                {
                    Page.RegisterStartupScript("mm", "<script language='javascript'> alert('please select record from grid !');</script>");

                    //dt.Clear();
                    objArrOneD.Clear();
                    lblMsg.Text = "";
                    lblMsg.Visible = false;
                    return;
                }
            
                Session["DIAGNOS_ASSOCIATION_PAID"] = objArrOneD;
               
               
                Page.RegisterStartupScript("mm", "<script language='javascript'>showReceiveDocumentPopup();</script>");
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

    private void ExportToExcelForPendingBills()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdExportToExcel.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdExportToExcel.Columns.Count; i++)
                    {
                        if (grdExportToExcel.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdExportToExcel.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdExportToExcel.Columns.Count; j++)
                {
                    if (grdExportToExcel.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdExportToExcel.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);


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

    private void BindGridForExportToExcel(string szFlag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _reportBO = new Bill_Sys_ReportBO();
        try
        {
            grdExportToExcel.DataSource = _reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyID.Text, szFlag);
            grdExportToExcel.DataBind();
            
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
    protected void tab_changed(object sender, EventArgs e)
    {
        if(tabcontainerPatientVisit.ActiveTabIndex == 0)
        {
            lblHeader.Text = "Paid Bills";
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "0")
            {
                dvpaid.Attributes.Add("bgcolor", "green");
                
                //grdAllReports.Columns[1].Visible = false;
                //grdExportToExcel.Columns[0].Visible = false;
                //grdBillSearch.Columns[4].Visible = false;
                //grdEEBillSearch.Columns[2].Visible = false;
                //ashutosh
                //grdUnpaidBill.Columns[4].Visible = false;
                //this.grdPaidBillSearch.XGridBind();
                SearchBilllist("GETPAIDLIST");
                
            }
            else
            {
                grdBillSearch.Columns[4].Visible = true;
                grdEEBillSearch.Columns[2].Visible = true;
                //ashutosh
                grdUnpaidBill.Columns[4].Visible = true;
                SearchBilllist("GETPAIDLIST");
                
            }
        }
        else if (tabcontainerPatientVisit.ActiveTabIndex == 1)
        {
          SearchBilllist("GETUNPAIDLIST");
            
            lblHeader.Text = "Un-Paid Bills";
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "0")
            {
                grdAllReports.Columns[1].Visible = false;
                grdExportToExcel.Columns[0].Visible = false;
                grdBillSearch.Columns[4].Visible = false;
                grdEEBillSearch.Columns[2].Visible = false;
                //ashutosh
                
                grdUnpaidBill.Columns[4].Visible = false;
                this.grdUnpaidBill.PageNumberList = this.conUnpaid;
               
                //this.grdUnpaidBill.XGridBind();
               
            }
            else
            {
                grdBillSearch.Columns[4].Visible = true;
                grdEEBillSearch.Columns[2].Visible = true;
                //ashutosh
                grdUnpaidBill.Columns[4].Visible = true;
                SearchBilllist("GETUNPAIDLIST");
                
            }
        }
    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdPaidBillSearch.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
    protected void lnkExportToExcelUnpaid_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdUnpaidBill.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void grdPaidBillSearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int index = 0;

        try
        {
            if (e.CommandName.ToString() == "PLS")
            {
                for (int i = 0; i < grdPaidBillSearch.Rows.Count; i++)
                {
                    LinkButton minus1 = (LinkButton)grdPaidBillSearch.Rows[i].FindControl("lnkM");
                    LinkButton plus1 = (LinkButton)grdPaidBillSearch.Rows[i].FindControl("lnkP");
                    if (minus1.Visible)
                    {
                        minus1.Visible = false;
                        plus1.Visible = true;
                        GridView gv1 = (GridView)grdPaidBillSearch.Rows[i].FindControl("GridView2");
                        gv1.Visible = false;
                    }
                    grdPaidBillSearch.Columns[5].ItemStyle.Width = 150;
                    grdPaidBillSearch.Columns[6].ItemStyle.Width = 350;
                   
                    grdPaidBillSearch.Columns[13].ItemStyle.Width = 80;
                    grdPaidBillSearch.Columns[14].ItemStyle.Width = 90;
                    grdPaidBillSearch.Columns[15].ItemStyle.Width = 60;
                    
                    grdPaidBillSearch.Columns[23].ItemStyle.Width = 0;
                    grdPaidBillSearch.Columns[23].ItemStyle.Height = 0;
                }
              
                index = Convert.ToInt32(e.CommandArgument);
                paidId = grdPaidBillSearch.DataKeys[index][0].ToString();
                GridView gv = (GridView)grdPaidBillSearch.Rows[index].FindControl("GridView2");
                
                LinkButton plus = (LinkButton)grdPaidBillSearch.Rows[index].FindControl("lnkP");
                LinkButton minus = (LinkButton)grdPaidBillSearch.Rows[index].FindControl("lnkM");
                gv.Visible = true;
                Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
                DataSet ds = new DataSet();

                ds = _obj.GetBillDetail(grdPaidBillSearch.DataKeys[index][0].ToString());
                if (ds.Tables[0].Rows.Count == 0)
                {
                    grdPaidBillSearch.Columns[23].Visible = true;
                    gv.Visible = true;
                    grdPaidBillSearch.Columns[23].ItemStyle.Width= 0;
                    //grdPaidBillSearch.Rows[index].Cells[23].Width=0;

                    grdPaidBillSearch.Columns[23].ItemStyle.Height = 0;
                    gv.EmptyDataText = "No Records Found";
                    gv.DataBind();
                }
                else
                {
                    gv.DataSource = ds;
                    gv.DataBind();

                    grdPaidBillSearch.Columns[23].Visible = true;
                    gv.Visible = true;
                    
                }      

                plus.Visible = false;
                minus.Visible = true;
            }        

            if (e.CommandName.ToString() == "MNS")
            {
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";
                GridView gv = (GridView)grdPaidBillSearch.Rows[index].FindControl("GridView2");
                divname = divname + grdPaidBillSearch.DataKeys[index][0].ToString();
                LinkButton plus = (LinkButton)grdPaidBillSearch.Rows[index].FindControl("lnkP");
                LinkButton minus = (LinkButton)grdPaidBillSearch.Rows[index].FindControl("lnkM");
               // ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "HideChildGrid('" + divname + "') ;", true);
                grdPaidBillSearch.Columns[23].Visible = false;
                grdPaidBillSearch.Columns[23].ItemStyle.Width = 0;
                grdPaidBillSearch.Columns[23].ItemStyle.Height = 0;
                gv.Visible = false;
                plus.Visible = true;
                minus.Visible = false;
               
            }
            //if (e.CommandName.ToString() == "Edit")
            //{
            //    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            //    string billno = commandArgs[0];
            //    string caseId = commandArgs[1];
            //    string caseNo=commandArgs[2];
            //    _bill_Sys_Case = new Bill_Sys_Case();
            //    //_bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[7].Text.ToString();
            //    _bill_Sys_Case.SZ_CASE_ID = caseId;
            //    CaseDetailsBO _obj_caseDetailsBO = new CaseDetailsBO();
            //    Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            //    //_bill_Sys_CaseObject.SZ_PATIENT_ID = _obj_caseDetailsBO.GetCasePatientID(e.Item.Cells[7].Text.ToString(), "");
            //    //....ashutosh
            //    _bill_Sys_CaseObject.SZ_PATIENT_ID = _obj_caseDetailsBO.GetCasePatientID(caseId, "");
            //   // _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[7].Text.ToString();
            //    _bill_Sys_CaseObject.SZ_CASE_ID =caseId;
            //    _bill_Sys_CaseObject.SZ_COMAPNY_ID = _obj_caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
            //    _bill_Sys_CaseObject.SZ_PATIENT_NAME = _obj_caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
            //    //_bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[3].Text.ToString();
            //    _bill_Sys_CaseObject.SZ_CASE_NO = caseNo;
                
            //    Session["CASEINFO"] = _bill_Sys_Case;
            //    Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
            //  //  Session["PassedCaseID"] = e.Item.Cells[7].Text;
            //    Session["PassedCaseID"] = caseId;
            //    //Session["SZ_BILL_NUMBER"] = e.CommandArgument;
            //    Session["SZ_BILL_NUMBER"] = billno;
            //    Response.Redirect("Bill_Sys_BillTransaction.aspx?Type=Search", false);
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

    protected void grdUnpaidBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList objarr1 = new ArrayList();
        mbs.bl.litigation.Litigation objDAO = new mbs.bl.litigation.Litigation();
        string _CompanyName = "";
        int UnPaidindex = 0;

        try
        {
            if (e.CommandName.ToString() == "PLS")
            {
                for (int i = 0; i < grdUnpaidBill.Rows.Count; i++)
                {
                    LinkButton minus2 = (LinkButton)grdUnpaidBill.Rows[i].FindControl("lnkUnM");
                    LinkButton plus2 = (LinkButton)grdUnpaidBill.Rows[i].FindControl("lnkUnP");
                   
                    if (minus2.Visible)
                    {
                        minus2.Visible = false;
                        plus2.Visible = true;
                        GridView gv1 = (GridView)grdUnpaidBill.Rows[i].FindControl("GridViewUnpaid");
                        gv1.Visible = false;
                        //grdPaidBillSearch.Rows[i].Cells[23].Visible = false;
                    }
                    grdUnpaidBill.Columns[5].ItemStyle.Width = 150;
                    grdUnpaidBill.Columns[6].ItemStyle.Width = 350;

                    grdUnpaidBill.Columns[13].ItemStyle.Width = 80;
                    grdUnpaidBill.Columns[14].ItemStyle.Width = 90;
                    grdUnpaidBill.Columns[15].ItemStyle.Width = 60;
                    grdUnpaidBill.Columns[23].ItemStyle.Width = 0;
                    grdUnpaidBill.Columns[23].ItemStyle.Height = 0;
                }

                //grdLitigationDesk.Columns[11].Visible = true;
                UnPaidindex = Convert.ToInt32(e.CommandArgument);
                string divname = "div";

                divname = divname + grdUnpaidBill.DataKeys[UnPaidindex][0].ToString();
                paidId = grdUnpaidBill.DataKeys[UnPaidindex][0].ToString();
                GridView gv = (GridView)grdUnpaidBill.Rows[UnPaidindex].FindControl("GridViewUnpaid");

                LinkButton plus = (LinkButton)grdUnpaidBill.Rows[UnPaidindex].FindControl("lnkUnP");
                LinkButton minus = (LinkButton)grdUnpaidBill.Rows[UnPaidindex].FindControl("lnkUnM");

                //DataSet objds = new DataSet();
                //objds = objDAO.GetLitigatedBills(grdLitigationDesk.DataKeys[index][0].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extdlitigate.Text);
                //gv.DataSource = objds;
                //gv.DataBind();
                gv.Visible = true;
                Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
                DataSet ds = new DataSet();

                ds = _obj.GetBillDetail(grdUnpaidBill.DataKeys[UnPaidindex][0].ToString());
                if (ds.Tables[0].Rows.Count == 0)
                {

                    grdUnpaidBill.Columns[23].Visible = true;
                    gv.Visible = true;
                    grdUnpaidBill.Columns[23].ItemStyle.Width = 0;
                    grdUnpaidBill.Columns[23].ItemStyle.Height = 0;
                    gv.EmptyDataText = "No Records Found";
                    gv.DataBind();
                }
                else
                {
                    gv.DataSource = ds;
                    gv.DataBind();

                    grdUnpaidBill.Columns[23].Visible = true;
                    gv.Visible = true;
                    grdUnpaidBill.Columns[23].ItemStyle.Width = 0;
                    grdUnpaidBill.Columns[23].ItemStyle.Height = 0;
                }
                //Page.RegisterStartupScript("mm", "<script language='javascript'>ShowChildGrid('" + divname + "');</script>");
                plus.Visible = false;
                minus.Visible = true;
            }

            if (e.CommandName.ToString() == "MNS")
            {
                //grdLitigationDesk.Columns[11].Visible = false;

                UnPaidindex = Convert.ToInt32(e.CommandArgument);
                string divname = "div";
                GridView gv = (GridView)grdUnpaidBill.Rows[UnPaidindex].FindControl("GridViewUnpaid");
                divname = divname + grdUnpaidBill.DataKeys[UnPaidindex][0].ToString();
                LinkButton plus = (LinkButton)grdUnpaidBill.Rows[UnPaidindex].FindControl("lnkUnP");
                LinkButton minus = (LinkButton)grdUnpaidBill.Rows[UnPaidindex].FindControl("lnkUnM");
                // ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "HideChildGrid('" + divname + "') ;", true);
                grdUnpaidBill.Columns[23].Visible = false;
                grdUnpaidBill.Columns[23].ItemStyle.Width = 0;
                grdUnpaidBill.Columns[23].ItemStyle.Height = 0;
                gv.Visible = false;
                plus.Visible = true;
                minus.Visible = false;

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

}
