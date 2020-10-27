/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_Doctortype.aspx.cs
/*Purpose              :       To Add and Edit Doctor Type 
/*Author               :       Manoj c
/*Date of creation     :       11 Dec 2008  
/*Modified By          :
/*Modified Date        :
/************************************************************/


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
public partial class Bill_Sys_DashBoard : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    Bill_Sys_BillTransaction_BO _billTransactionBO;
    Bill_Sys_BillingCompanyObject _billReadOnly;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            setLabels();
            DisplayMissingSpeciality();
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_DashBoard.aspx");
        }
        #endregion
    }

    protected void setLabels()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO _obj = new DashBoardBO();
        _billTransactionBO = new Bill_Sys_BillTransaction_BO();
        try
        {
            DayOfWeek day = Convert.ToDateTime(System.DateTime.Today.ToString()).DayOfWeek;
            int days = day - DayOfWeek.Sunday;

            DateTime start = Convert.ToDateTime(System.DateTime.Today.ToString()).AddDays(-days);
            DateTime end = start.AddDays(6);

            lblAppointmentToday.Text = _obj.getAppoinmentCount(System.DateTime.Today.ToString(),System.DateTime.Today.ToString(),txtCompanyID.Text,"GET_APPOINTMENT");
            lblAppointmentWeek.Text = _obj.getAppoinmentCount(start.ToString(), end.ToString(), txtCompanyID.Text, "GET_APPOINTMENT");

            lblBillStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=Paid' onclick=\"javascript:OpenPage('Paid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_PAID_LIST_COUNT", txtCompanyID.Text) + "</a>";
            lblBillStatus.Text += " Paid Bills  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=UnPaid' onclick=\"javascript:OpenPage('UnPaid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_UNPAID_LIST_COUNT", txtCompanyID.Text) + "</a> Un-Paid Bills </li></ul>";

            lblDesk.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='AJAX Pages/Bill_Sys_LitigationDesk.aspx?Type=Litigation' onclick=\"javascript:OpenPage('Litigation');\" > " + _billTransactionBO.GetCaseCount("SP_LITIGATION_WRITEOFF_DESK", "GET_LETIGATION_COUNT", txtCompanyID.Text) + "</a>" + " bills due for litigation";

            //lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=MissingProvider' onclick=\"javascript:OpenPage('MissingProvider');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_PROVIDER") + "</a>";
            //lblMissingInformation.Text += " provider information missing  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            lblMissingInformation.Text += " insurance company missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_ATTORNEY") + "</a>";
            lblMissingInformation.Text += " attorney missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_CLAIM_NUMBER") + "</a> claim number missing </li>";
            lblMissingInformation.Text +=   "<li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_REPORT_NUMBER") + "</a> report number missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingPolicyHolder'> " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_POLICY_HOLDER") + "</a> policy holder missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_ShowUnSentNF2.aspx' > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "UNSENTNF2") + "</a> unsent NF2 </li></ul>";
            
            
            
            // lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=R' onclick=\"javascript:OpenPage('Litigation');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a>" + " Received Report";
            lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_ReffPaidBills.aspx' onclick=\"javascript:OpenPage('Litigation');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a>" + " Received Report";
            lblReport.Text += "</li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=P' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_PENDING_COUNT") + "</a> Pending Report </li></ul>";

            lblProcedureStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li>" + _obj.getBilledUnbilledProcCode(txtCompanyID.Text, "GET_BILLEDPROC") + " billed procedure codes";
            lblProcedureStatus.Text += "</li>  <li>" + _obj.getBilledUnbilledProcCode(txtCompanyID.Text, "GET_UNBILLEDPROC") + " Un-billed procedure codes </li></ul>";


            //lblVisits.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a id='hlnkTotalVisit' href='#' onclick='javascript:OpenTotalVisitPopup();'>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_VISIT_COUNT") + " </a> Visits</li>";
            //lblVisits.Text += "<li><a id='hlnkVisit' href='#' onclick='javascript:OpenVisitPopup();'>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_BILLED_VISIT_COUNT") + " </a>Billed visits </li>";
            //lblVisits.Text += "<li><a id='hlnkUnVisit' href='#' onclick='javascript:OpenUnVisitPopup();'>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT") + "</a> Un-billed visits </a></li></ul>";

            lblTotalVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_VISIT_COUNT");
            lblBilledVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_BILLED_VISIT_COUNT");
            lblUnBilledVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT");

            // 8 April - add patient visit status block on page - sachin
            lblPatientVisitStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientscheduled' onclick=\"javascript:OpenPage('PatientScheduled');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_SCHEDULED_COUNT") + "</a> ";
            lblPatientVisitStatus.Text += " Patient Scheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientnoshows' onclick=\"javascript:OpenPage('PatientNoShows');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_NO_SHOWS") + "</a>";
            lblPatientVisitStatus.Text += " Patient No Shows </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientrescheduled' onclick=\"javascript:OpenPage('PatientRescheduled');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_RESCHEDULED") + "</a>";
            lblPatientVisitStatus.Text += " Patient Rescheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientvisitcompleted' onclick=\"javascript:OpenPage('PatientVisitcompleted');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_COMPLETED") + "</a>Patient Visit completed </li></ul>"; 

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
                        grdTotalVisit.DataSource  = _objDashBoardBO.getVisitDetails(txtCompanyID.Text, "TOTALCOUNT");
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

    private void DisplayMissingSpeciality()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO objDashBoard = new DashBoardBO();
        DataTable dt = new DataTable();
        try
        {
            string companyID = txtCompanyID.Text;
            dt = objDashBoard.getMissingSpecialityList(companyID);

            lblMissingSpecialityText.Text = "<table>";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 4 == 0)
                {
                    if (i != 0 && i % 4 == 0)
                    {
                        lblMissingSpecialityText.Text += "</tr>";
                    }
                    lblMissingSpecialityText.Text += "<tr><td><ul style=\"list-style-type:disc;padding-left:60px;\"><li><a href='#' onclick=\"javascript:OpenReport('" + dt.Rows[i][2].ToString() + "')\">" + dt.Rows[i][0].ToString() + "</a> - " + dt.Rows[i][1].ToString()+ "</li></ul></td>";
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
 
    protected void btnSpecial_Click(object sender, EventArgs e)
    {
        Session["SpecialityID"] = hdnSpeciality.Value;
        Response.Redirect("Bill_Sys_SpecialityMissingReport.aspx", false);
    }
}


