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

public partial class Bill_Sys_ShowPatientVisitStatus : PageBase
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private Bill_Sys_Case _bill_Sys_Case;
    private Bill_Sys_ReportBO _reportBO;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                setLabels();
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    grdCaseMaster.Columns[11].Visible = true;
                    grdCaseMaster.Columns[13].Visible = true;
                }
                if (Request.QueryString["Flag"] != null)
                {
                    if (Request.QueryString["Flag"].ToString().ToLower() == "patientscheduled")
                    {
                        searchcaselist("GET_PATIENT_SCHEDULED");
                        lblHeader.Text = "Patient Scheduled";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "patientnoshows")
                    {
                        searchcaselist("GET_PATIENT_VISIT_NO_SHOWS");
                        lblHeader.Text = "Patient No Shows";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "patientrescheduled")
                    {
                        searchcaselist("GET_PATIENT_VISIT_RESCHEDULED");
                        lblHeader.Text = "Patient Rescheduled";
                    }
                    else if (Request.QueryString["Flag"].ToString().ToLower() == "patientvisitcompleted")
                    {
                        searchcaselist("GET_PATIENT_VISIT_COMPLETED");
                        lblHeader.Text = "Patient Visit completed";
                    }
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_PaidBills.aspx");
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
        grdAllReports.Visible = true;
        _reportBO = new Bill_Sys_ReportBO();
        try
        {
            grdAllReports.DataSource = _reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyID.Text, szFlag);
            grdAllReports.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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

            lblBillStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=Paid' onclick=\"javascript:OpenPage('Paid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_PAID_LIST_COUNT", txtCompanyID.Text) + "</a>";
            lblBillStatus.Text += " Paid Bills  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=UnPaid' onclick=\"javascript:OpenPage('UnPaid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_UNPAID_LIST_COUNT", txtCompanyID.Text) + "</a> Un-Paid Bills </li></ul>";

            lblDesk.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' onclick=\"javascript:OpenPage('Litigation');\" > " + _billTransactionBO.GetCaseCount("SP_LITIGATION_WRITEOFF_DESK", "GET_LETIGATION_COUNT", txtCompanyID.Text) + "</a>" + " bills due for litigation";

            //lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=MissingProvider' onclick=\"javascript:OpenPage('MissingProvider');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_PROVIDER") + "</a>";
            //lblMissingInformation.Text += " provider information missing  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            lblMissingInformation.Text += " insurance company missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_ATTORNEY") + "</a>";
            lblMissingInformation.Text += " attorney missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_CLAIM_NUMBER") + "</a> claim number missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_REPORT_NUMBER") + "</a> report number missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingPolicyHolder'> " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_POLICY_HOLDER") + "</a> policy holder missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_ShowUnSentNF2.aspx' > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "UNSENTNF2") + "</a> unsent NF2 </li></ul>";


            //lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=R' onclick=\"javascript:OpenPage('Litigation');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a>" + " Received Report";
            lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_ReffPaidBills.aspx' onclick=\"javascript:OpenPage('Litigation');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a>" + " Received Report";
            lblReport.Text += "</li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=P' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_PENDING_COUNT") + "</a> Pending Report </li></ul>";

            lblProcedureStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li>" + _obj.getBilledUnbilledProcCode(txtCompanyID.Text, "GET_BILLEDPROC") + " billed procedure codes";
            lblProcedureStatus.Text += "</li>  <li>" + _obj.getBilledUnbilledProcCode(txtCompanyID.Text, "GET_UNBILLEDPROC") + " Un-billed procedure codes </li></ul>";


            //lblVisits.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_VISIT_COUNT") + " Visits</li>";
            //lblVisits.Text += "<li>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_BILLED_VISIT_COUNT") + " Billed visits </li>";
            //lblVisits.Text += "<li>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT") + " Un-billed visits </li></ul>";

            lblTotalVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_VISIT_COUNT");
            lblBilledVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_BILLED_VISIT_COUNT");
            lblUnBilledVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT");

            // 8 April - add patient visit status block on page - sachin
            lblPatientVisitStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=PatientScheduled' onclick=\"javascript:OpenPage('PatientScheduled');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_SCHEDULED_COUNT") + "</a> ";
            lblPatientVisitStatus.Text += " Patient Scheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=PatientNoShows' onclick=\"javascript:OpenPage('PatientNoShows');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_NO_SHOWS") + "</a>";
            lblPatientVisitStatus.Text += " Patient No Shows </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=PatientRescheduled' onclick=\"javascript:OpenPage('PatientRescheduled');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_RESCHEDULED") + "</a>";
            lblPatientVisitStatus.Text += " Patient Rescheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=PatientVisitcompleted' onclick=\"javascript:OpenPage('PatientVisitcompleted');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_COMPLETED") + "</a>Patient Visit completed </li></ul>"; 


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
                string strError = ex.Message.ToString();
                strError = strError.Replace("\n", " ");
                Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
        if (e.CommandName.ToString() == "workarea")
        {

            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[1].Text;
            _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
            _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[2].Text;
            _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

            _bill_Sys_Case = new Bill_Sys_Case();
            _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

            Session["CASEINFO"] = _bill_Sys_Case;
            //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true )
            //{
            //    Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
            //}
            //else
            //{
            //    Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
            //}
            Response.Redirect("Bill_Sys_StatusProceudure.aspx", false);

        }

        if (e.CommandName.ToString() == "appointment")
        {
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                Session["SZ_CASE_ID"] = e.Item.Cells[0].Text;
                Session["PROVIDERNAME"] = e.Item.Cells[2].Text;
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[1].Text;
                _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[0].Text;
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[2].Text;
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[0].Text;

                Session["CASEINFO"] = _bill_Sys_Case;

                DateTime dtEventTime = new DateTime();
                dtEventTime = Convert.ToDateTime(e.Item.Cells[3].Text.ToString());
                string szQueryString = "";
                szQueryString = "&idate=" + dtEventTime.ToShortDateString();
                Response.Redirect("Bill_Sys_AppointPatientEntry.aspx?Flag=true" + szQueryString, false);
            }
            else
            {
                Session["SZ_CASE_ID"] = e.Item.Cells[0].Text;
                Session["PROVIDERNAME"] = e.Item.Cells[2].Text;
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[1].Text;
                _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[0].Text;
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[2].Text;
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[0].Text;

                Session["CASEINFO"] = _bill_Sys_Case;

                DateTime dtEventTime = new DateTime();
                dtEventTime = Convert.ToDateTime(e.Item.Cells[3].Text.ToString());
                string szQueryString = "?_day=" + dtEventTime.ToShortDateString();
                szQueryString = szQueryString + "&idate=" + dtEventTime.ToShortDateString();

                Response.Redirect("Bill_Sys_ScheduleEvent.aspx" + szQueryString, false);
            }
        }

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
                grdCaseMaster.DataBind();



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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
        grdBillSearch.Visible = true;
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
            DataRow objDR;
            objDR = objNewDS.Tables[0].NewRow();
            objDR["SZ_BILL_NUMBER"] = "";
            objDR["SZ_CASE_NO"] = "<b>Total</b>";
            objDR["FLT_BILL_AMOUNT"] = SumTotalBillAmount;
            objDR["PAID_AMOUNT"] = SumTotalPaidAmount;
            objDR["FLT_BALANCE"] = SumTotalOutstandingAmount;
            objNewDS.Tables[0].Rows.InsertAt(objDR, 0);


            grdBillSearch.DataSource = objNewDS;
            grdBillSearch.DataBind();
            grdEEBillSearch.DataSource = objNewDS;
            grdEEBillSearch.DataBind();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
                _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[3].Text.ToString();
                CaseDetailsBO _obj_caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _obj_caseDetailsBO.GetCasePatientID(e.Item.Cells[3].Text.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[3].Text.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _obj_caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _obj_caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[2].Text;

                Session["CASEINFO"] = _bill_Sys_Case;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["PassedCaseID"] = e.Item.Cells[3].Text;
                Session["SZ_BILL_NUMBER"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_BillTransaction.aspx?Type=Search", false);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
                            strHtml.Append(grdCaseMaster.Items[icount].Cells[33].Text);
                        else
                            strHtml.Append(grdCaseMaster.Items[icount].Cells[j].Text);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
                ExportToExcelForAllReport();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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

    public DataTable DisplayLocationInGrid(DataSet p_objDS)
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
                    objDR["DT_DATE_OF_ACCIDENT"] = objDS.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
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
                    objDR["SZ_APPOINTMENT"] = "";
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
                    int count = grdCaseMaster.Items.Count;



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
                    objDR["DT_DATE_OF_ACCIDENT"] = objDS.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    objDR["BT_ASSOCIATE_DIAGNOSIS_CODE"] = objDS.Tables[0].Rows[i]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        
        return objDT;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion
}
