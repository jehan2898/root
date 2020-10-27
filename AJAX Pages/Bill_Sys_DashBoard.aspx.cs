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
    private string UserId;

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    UserId=((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
    //    if (!Page.IsPostBack)
    //    {
    //        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //        setLabels();
    //     //   DisplayMissingSpeciality();
    //    }
    //    #region "check version readonly or not"
    //    string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
    //    if (app_status.Equals("True"))
    //    {
    //        Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
    //        cv.MakeReadOnlyPage("Bill_Sys_DashBoard.aspx");
    //    }
    //    #endregion
    //    ImgBtn_Status.Click += new ImageClickEventHandler(refresh_click);
    //    ImgBtn_Report.Click += new ImageClickEventHandler(refresh_click);
    //    ImgBtn_Today.Click += new ImageClickEventHandler(refresh_click);
    //    ImgBtn_Visits.Click += new ImageClickEventHandler(refresh_click);
    //    ImgBtn_Weekly.Click += new ImageClickEventHandler(refresh_click);
    //    ImgBtn_Procedure.Click += new ImageClickEventHandler(refresh_click);
    //    ImgBtn_PateintsVisits.Click += new ImageClickEventHandler(refresh_click);
    //    ImgBtn_MissingInf.Click += new ImageClickEventHandler(refresh_click);
    //    ImgBtn_Desk.Click += new ImageClickEventHandler(refresh_click);
    //    ImgBtn_missingdoc.Click += new ImageClickEventHandler(refresh_click);
    //}

    //protected void setLabels()
    //{

    //    DashBoardBO _obj = new DashBoardBO();
    //    _billTransactionBO = new Bill_Sys_BillTransaction_BO();
    //    try
    //    {
    //        DayOfWeek day = Convert.ToDateTime(System.DateTime.Today.ToString()).DayOfWeek;
    //        int days = day - DayOfWeek.Sunday;

    //        DateTime start = Convert.ToDateTime(System.DateTime.Today.ToString()).AddDays(-days);
    //        DateTime end = start.AddDays(6);
    //        DataSet ds = new DataSet();
    //        ds=_obj.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), start.ToString(), end.ToString(), txtCompanyID.Text,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE,"ALL",UserId);

    //        LblNoshow_Today2.Text = ds.Tables[0].Rows[0][0].ToString();
    //        LblScheduled_Today2.Text = ds.Tables[0].Rows[0][1].ToString();
    //        LblComplete_Today2.Text = ds.Tables[0].Rows[0][2].ToString();
    //        try
    //        {
    //            LblNewReferral_Today2.Text = ds.Tables[0].Rows[0][3].ToString();
    //            Referral_today.InnerHtml = "New Referal Appointment";
    //        }
    //        catch 
    //        {
    //            Referral_today.InnerHtml = "-";
    //        }
    //        Lblshow_missingdoc.Text = "<a href='Bill_Sys_Notification_Report.aspx'>Show</a>";
    //        LblNoshow_Weekly2.Text = ds.Tables[25].Rows[0][0].ToString();
    //        LblScheduled_Weekly2.Text = ds.Tables[25].Rows[0][1].ToString();
    //        LblComplete_Weekly2.Text = ds.Tables[25].Rows[0][2].ToString();
    //        //"MISSING_INSURANCE_COMPANY"
    //        LblCompany_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + ds.Tables[1].Rows[0][0].ToString() + "</a>";
    //        //"MISSING_ATTORNEY"
    //        LblAttorney_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + ds.Tables[2].Rows[0][0].ToString() + "</a>";
    //        //"MISSING_CLAIM_NUMBER"
    //        LblClaim_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + ds.Tables[3].Rows[0][0].ToString() + "</a>";
    //        //"MISSING_REPORT_NUMBER"
    //        LblReport_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + ds.Tables[4].Rows[0][0].ToString() + "</a>";
    //        //"MISSING_POLICY_HOLDER"
    //        LblPolicyHldr_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingPolicyHolder'> " + ds.Tables[5].Rows[0][0].ToString() + "</a>";
    //        //"UNSENTNF2"
    //        Bill_Sys_BillingCompanyDetails_BO objOffID = new Bill_Sys_BillingCompanyDetails_BO();
    //        string sz_Off_Id = objOffID.GetRefDocID(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
    //        if (sz_Off_Id != "")
    //        {
    //            Missing_Inflbl.Text="-";
    //            LblUNSENTNF2_MissingInf2.Text = "";
    //        }
    //        else
    //        {
    //            Missing_Inflbl.Text="Unsent&nbsp;NF2";
    //            LblUNSENTNF2_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=UnSentNF2'> " + ds.Tables[14].Rows[0][0].ToString() + "</a>";
    //        }
    //        //"DOCUMENT_RECEIVED_COUNT"
    //        LblReceived_Report2.Text = "<a href='Bill_Sys_ReffPaidBills.aspx' title='View Details' onclick=\"javascript:OpenPage('Litigation');\" > " + ds.Tables[6].Rows[0][0].ToString() + "</a>";
    //        //"DOCUMENT_PENDING_COUNT"
    //        LblPending_Report2.Text = "<a href='Bill_Sys_paid_bills.aspx?Flag=report&Type=P' title='View Details' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + ds.Tables[7].Rows[0][0].ToString() + "</a>";
    //        //"GET_BILLEDPROC"
    //        LblBilled_Procedure2.Text = ds.Tables[9].Rows[0][0].ToString();
    //        //"GET_UNBILLEDPROC"
    //        LblUnBilled_Procedure2.Text = ds.Tables[10].Rows[0][0].ToString();
    //        //"GET_VISIT_COUNT"
    //        LblTotal_Visits2.Text = "<a href='#' title='View Details'> " + ds.Tables[11].Rows[0][0].ToString() + "</a>";
    //        //"GET_BILLED_VISIT_COUNT"
    //        LblBilled_Visits2.Text = "<a href='#' title='View Details'> " + ds.Tables[12].Rows[0][0].ToString() + "</a>";
    //        //"GET_UNBILLED_VISIT_COUNT"
    //        LblUnBilled_Visits2.Text = "<a href='#' title='View Details'> " + ds.Tables[13].Rows[0][0].ToString() + "</a>";
    //        //"GET_PATIENT_VISIT_SCHEDULED_COUNT"
    //        LblScheduled_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientscheduled' title='View Details' onclick=\"javascript:OpenPage('PatientScheduled');\" > " + ds.Tables[15].Rows[0][0].ToString() + "</a>";
    //        //"GET_PATIENT_VISIT_NO_SHOWS"
    //        LblNoshow_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientnoshows' title='View Details' onclick=\"javascript:OpenPage('PatientNoShows');\" > " + ds.Tables[16].Rows[0][0].ToString() + "</a>";
    //        //"GET_PATIENT_VISIT_RESCHEDULED"
    //        LblRescheduled_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientrescheduled' title='View Details' onclick=\"javascript:OpenPage('PatientRescheduled');\" > " + ds.Tables[17].Rows[0][0].ToString() + "</a>";
    //        //"GET_PATIENT_VISIT_COMPLETED"
    //        LblComplete_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientvisitcompleted' title='View Details' onclick=\"javascript:OpenPage('PatientVisitcompleted');\" > " + ds.Tables[18].Rows[0][0].ToString() + "</a>";
            
    //        //"GET_PAID_LIST_COUNT"
    //        LblPaid_Status2.Text = "<a href='Bill_Sys_paid_bills.aspx?Flag=Paid' title='View Details' onclick=\"javascript:OpenPage('Paid');\" > " +ds.Tables[19].Rows[0][0].ToString() + "</a>";
    //        //"GET_UNPAID_LIST_COUNT"
    //        LblUnPaid_Status2.Text = "<a href='Bill_Sys_paid_bills.aspx?Flag=UnPaid' title='View Details' onclick=\"javascript:OpenPage('UnPaid');\" > " + ds.Tables[20].Rows[0][0].ToString()+ "</a>";
    //        //"GET_LETIGATION_COUNT"
    //        LblBillsDue_Desk2.Text = "<a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' title='View Details' onclick=\"javascript:OpenPage('Litigation');\" > " + ds.Tables[21].Rows[0][0].ToString()+ "</a>";
    //        ConfigDashBoard(ds.Tables[8],ds.Tables[22],ds.Tables[23],ds.Tables[24]);
           
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    //protected void refresh_click(object sender, ImageClickEventArgs e)
    //{
    //    DashBoardBO _objUpdate = new DashBoardBO();
    //    DataSet ds = new DataSet();
    //    switch((((System.Web.UI.Control)(sender)).ID))
    //    {
    //        case "ImgBtn_Status":
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_PAID_LIST_COUNT",UserId);
    //            LblPaid_Status2.Text = "<a href='Bill_Sys_PaidBills.aspx?Flag=Paid' title='View Details' onclick=\"javascript:OpenPage('Paid');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_UNPAID_LIST_COUNT",UserId);
    //            LblUnPaid_Status2.Text = "<a href='Bill_Sys_PaidBills.aspx?Flag=UnPaid' title='View Details' onclick=\"javascript:OpenPage('UnPaid');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            break;
    //        case "ImgBtn_Report":
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "DOCUMENT_RECEIVED_COUNT",UserId);
    //            LblReceived_Report2.Text = "<a href='Bill_Sys_ReffPaidBills.aspx' title='View Details' onclick=\"javascript:OpenPage('Litigation');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "DOCUMENT_PENDING_COUNT",UserId);
    //            LblPending_Report2.Text = "<a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=P' title='View Details' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            break;
    //        case "ImgBtn_Today":
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_APPOINTMENT_DASHBOARD",UserId);
    //            LblNoshow_Today2.Text = ds.Tables[0].Rows[0][0].ToString();
    //            LblScheduled_Today2.Text = ds.Tables[0].Rows[0][1].ToString();
    //            LblComplete_Today2.Text = ds.Tables[0].Rows[0][2].ToString();
    //            try
    //            {
    //                LblNewReferral_Today2.Text = ds.Tables[0].Rows[0][3].ToString();
    //                Referral_today.InnerHtml = "New Referal Appointment";
    //            }
    //            catch
    //            {
    //                Referral_today.InnerHtml = "-";
    //            }
    //            break;
    //        case "ImgBtn_Visits":
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_VISIT_COUNT",UserId);
    //            LblTotal_Visits2.Text = "<a href='#' title='View Details'> " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_BILLED_VISIT_COUNT",UserId);
    //            LblBilled_Visits2.Text = "<a href='#' title='View Details'> " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_UNBILLED_VISIT_COUNT",UserId);
    //            LblUnBilled_Visits2.Text = "<a href='#' title='View Details'> " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "TotalBilledUnbilledVisit",UserId);
    //            grdTotalVisit.DataSource = ds.Tables[0];
    //            grdTotalVisit.DataBind();
    //            grdVisit.DataSource = ds.Tables[1];
    //            grdVisit.DataBind();
    //            grdUnVisit.DataSource = ds.Tables[2];
    //            grdUnVisit.DataBind();
                        
    //            break;
    //        case "ImgBtn_Procedure":
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_BILLEDPROC",UserId);
    //            LblBilled_Procedure2.Text = ds.Tables[0].Rows[0][0].ToString();
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_UNBILLEDPROC",UserId);
    //            LblUnBilled_Procedure2.Text = ds.Tables[0].Rows[0][0].ToString();
    //            break;
    //        case "ImgBtn_PateintsVisits":
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_PATIENT_VISIT_SCHEDULED_COUNT",UserId);
    //            LblScheduled_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientscheduled' title='View Details' onclick=\"javascript:OpenPage('PatientScheduled');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_PATIENT_VISIT_NO_SHOWS",UserId);
    //            LblNoshow_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientnoshows' title='View Details' onclick=\"javascript:OpenPage('PatientNoShows');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_PATIENT_VISIT_RESCHEDULED",UserId);
    //            LblRescheduled_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientrescheduled' title='View Details' onclick=\"javascript:OpenPage('PatientRescheduled');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_PATIENT_VISIT_COMPLETED",UserId);
    //            LblComplete_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientvisitcompleted' title='View Details' onclick=\"javascript:OpenPage('PatientVisitcompleted');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            break;
    //        case "ImgBtn_MissingInf":
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "MISSING_INSURANCE_COMPANY",UserId);
    //            LblCompany_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "MISSING_ATTORNEY",UserId);
    //            LblAttorney_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "MISSING_CLAIM_NUMBER",UserId);
    //            LblClaim_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "MISSING_REPORT_NUMBER",UserId);
    //            LblReport_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "MISSING_POLICY_HOLDER",UserId);
    //            LblPolicyHldr_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingPolicyHolder'> " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "UNSENTNF2",UserId);
    //            LblUNSENTNF2_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=UnSentNF2' > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            break;
    //        case "ImgBtn_Desk":
    //            ds = _objUpdate.AllDashBoardData(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_LETIGATION_COUNT",UserId);
    //            LblBillsDue_Desk2.Text = "<a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' title='View Details' onclick=\"javascript:OpenPage('Litigation');\" > " + ds.Tables[0].Rows[0][0].ToString() + "</a>";
    //            break;
    //        case "ImgBtn_Weekly":
    //            DayOfWeek day = Convert.ToDateTime(System.DateTime.Today.ToString()).DayOfWeek;
    //            int days = day - DayOfWeek.Sunday;
    //            DateTime start = Convert.ToDateTime(System.DateTime.Today.ToString()).AddDays(-days);
    //            DateTime end = start.AddDays(6);
    //            ds = _objUpdate.AllDashBoardData(start.ToString(), end.ToString(), "", "", txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_APPOINTMENT_DASHBOARD",UserId);
    //            LblNoshow_Weekly2.Text = ds.Tables[0].Rows[0][0].ToString();
    //            LblScheduled_Weekly2.Text = ds.Tables[0].Rows[0][1].ToString();
    //            LblComplete_Weekly2.Text = ds.Tables[0].Rows[0][2].ToString();
    //            break;
    //    }
    //}

    //private void ConfigDashBoard(DataTable dt,DataTable TotalCount,DataTable Billed,DataTable UnBilled)
    //{
    //    DashBoardBO _objDashBoardBO = new DashBoardBO();
    //    try
    //    {

    //       // DataTable dt = _objDashBoardBO.GetConfigDashBoard(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE);
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            switch (dr[0].ToString())
    //            {
    //                case "Daily Appointment":
    //                    TblToday.Visible = true;
    //                    break;
    //                case "Weekly Appointment":
    //                    TblWeekly.Visible = true;
    //                    break;
    //                case "Bill Status":
    //                    TblBillStat.Visible = true;
    //                    break;
    //                case "Desk":
    //                    Tbl_Desk.Visible = true;
    //                    break;
    //                case "Missing Information":
    //                    TblMissingInf.Visible = true;
    //                    break;
    //                case "Report Section":
    //                    TblReport.Visible = true;
    //                    break;
    //                case "Procedure Status":
    //                    TblProcedure.Visible = true;
    //                    break;
    //                case "Visits":
    //                    Tbl_Visits.Visible = true;
    //                   // grdTotalVisit.DataSource  = _objDashBoardBO.getVisitDetails(txtCompanyID.Text, "TOTALCOUNT");
    //                    grdTotalVisit.DataSource = TotalCount;
    //                    grdTotalVisit.DataBind();
    //                    //grdVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyID.Text, "BILLEDVISIT");
    //                    grdVisit.DataSource = Billed;
    //                    grdVisit.DataBind();
    //                    //grdUnVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyID.Text, "UNBILLEDVISIT");
    //                    grdUnVisit.DataSource = UnBilled;
    //                    grdUnVisit.DataBind();
    //                    break;
    //                case "Missing Speciality":
    //                    TblMissingSpecialty.Visible = true;
    //                    break;
    //                case "Patient Visit Status":
    //                    TblPatientVisits.Visible = true;
    //                    break;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    //private void DisplayMissingSpeciality()
    //{
    //    //DashBoardBO objDashBoard = new DashBoardBO();
    //    //DataTable dt = new DataTable();
    //    //try
    //    //{
    //    //    string companyID = txtCompanyID.Text;
    //    //    dt = objDashBoard.getMissingSpecialityList(companyID);

    //    //    lblMissingSpecialityText.Text = "<table>";

    //    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    //    {
    //    //        if (i % 4 == 0)
    //    //        {
    //    //            if (i != 0 && i % 4 == 0)
    //    //            {
    //    //                lblMissingSpecialityText.Text += "</tr>";
    //    //            }
    //    //            lblMissingSpecialityText.Text += "<tr><td><ul style=\"list-style-type:disc;padding-left:60px;\"><li><a href='#' onclick=\"javascript:OpenReport('" + dt.Rows[i][2].ToString() + "')\">" + dt.Rows[i][0].ToString() + "</a> - " + dt.Rows[i][1].ToString()+ "</li></ul></td>";
    //    //        }
    //    //        else
    //    //        {
    //    //            lblMissingSpecialityText.Text += "<td><ul style=\"list-style-type:disc;padding-left:60px;\"><li><a href='#' onclick=\"javascript:OpenReport('" + dt.Rows[i][2].ToString() + "')\">" + dt.Rows[i][0].ToString() + "</a> - " + dt.Rows[i][1].ToString() + "</li><ul></td>";
    //    //        }
    //    //    }
    //    //    lblMissingSpecialityText.Text += "</table>";
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    string strError = ex.Message.ToString();
    //    //    strError = strError.Replace("\n", " ");
    //    //    Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    //}
    //}
 
    //protected void btnSpecial_Click(object sender, EventArgs e)
    //{
    //    Session["SpecialityID"] = hdnSpeciality.Value;
    //    Response.Redirect("../Bill_Sys_SpecialityMissingReport.aspx", false);
    //}

    protected void btnSpecial_Click(object sender, EventArgs e)
    {
        this.Session["SpecialityID"] = this.hdnSpeciality.Value;
        base.Response.Redirect("../Bill_Sys_SpecialityMissingReport.aspx", false);
    }

    private void ConfigDashBoard(DataTable dt, DataTable TotalCount, DataTable Billed, DataTable UnBilled)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new DashBoardBO();
        try
        {
            foreach (DataRow row in dt.Rows)
            {
                switch (row[0].ToString())
                {
                    case "Daily Appointment":
                        {
                            this.TblToday.Visible = true;
                            continue;
                        }
                    case "Weekly Appointment":
                        {
                            this.TblWeekly.Visible = true;
                            continue;
                        }
                    case "Bill Status":
                        {
                            this.TblBillStat.Visible = true;
                            continue;
                        }
                    case "Desk":
                        {
                            this.Tbl_Desk.Visible = true;
                            continue;
                        }
                    case "Missing Information":
                        {
                            this.TblMissingInf.Visible = true;
                            continue;
                        }
                    case "Report Section":
                        {
                            this.TblReport.Visible = true;
                            continue;
                        }
                    case "Procedure Status":
                        {
                            this.TblProcedure.Visible = true;
                            continue;
                        }
                    case "Visits":
                        {
                            this.Tbl_Visits.Visible = true;
                            this.grdTotalVisit.DataSource = TotalCount;
                            this.grdTotalVisit.DataBind();
                            this.grdVisit.DataSource = Billed;
                            this.grdVisit.DataBind();
                            this.grdUnVisit.DataSource = UnBilled;
                            this.grdUnVisit.DataBind();
                            continue;
                        }
                    case "Missing Speciality":
                        {
                            this.TblMissingSpecialty.Visible = true;
                            continue;
                        }
                    case "Patient Visit Status":
                        {
                            this.TblPatientVisits.Visible = true;
                            continue;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void DisplayMissingSpeciality()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.UserId = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        if (!this.Page.IsPostBack)
        {
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.setLabels();
        }
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_DashBoard.aspx");
        }
        this.ImgBtn_Status.Click += new ImageClickEventHandler(this.refresh_click);
        this.ImgBtn_Report.Click += new ImageClickEventHandler(this.refresh_click);
        this.ImgBtn_Today.Click += new ImageClickEventHandler(this.refresh_click);
        this.ImgBtn_Visits.Click += new ImageClickEventHandler(this.refresh_click);
        this.ImgBtn_Weekly.Click += new ImageClickEventHandler(this.refresh_click);
        this.ImgBtn_Procedure.Click += new ImageClickEventHandler(this.refresh_click);
        this.ImgBtn_PateintsVisits.Click += new ImageClickEventHandler(this.refresh_click);
        this.ImgBtn_MissingInf.Click += new ImageClickEventHandler(this.refresh_click);
        this.ImgBtn_Desk.Click += new ImageClickEventHandler(this.refresh_click);
        this.ImgBtn_missingdoc.Click += new ImageClickEventHandler(this.refresh_click);
    }

    protected void refresh_click(object sender, ImageClickEventArgs e)
    {
        DashBoardBO dbo = new DashBoardBO();
        DataSet set = new DataSet();
        switch (((Control)sender).ID)
        {
            case "ImgBtn_Status":
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_PAID_LIST_COUNT", this.UserId);
                this.LblPaid_Status2.Text = "<a href='Bill_Sys_PaidBills.aspx?Flag=Paid' title='View Details' onclick=\"javascript:OpenPage('Paid');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_UNPAID_LIST_COUNT", this.UserId);
                this.LblUnPaid_Status2.Text = "<a href='Bill_Sys_PaidBills.aspx?Flag=UnPaid' title='View Details' onclick=\"javascript:OpenPage('UnPaid');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                return;

            case "ImgBtn_Report":
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "DOCUMENT_RECEIVED_COUNT", this.UserId);
                this.LblReceived_Report2.Text = "<a href='Bill_Sys_ReffPaidBills.aspx' title='View Details' onclick=\"javascript:OpenPage('Litigation');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "DOCUMENT_PENDING_COUNT", this.UserId);
                this.LblPending_Report2.Text = "<a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=P' title='View Details' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                return;

            case "ImgBtn_Today":
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_APPOINTMENT_DASHBOARD", this.UserId);
                this.LblNoshow_Today2.Text = set.Tables[0].Rows[0][0].ToString();
                this.LblScheduled_Today2.Text = set.Tables[0].Rows[0][1].ToString();
                this.LblComplete_Today2.Text = set.Tables[0].Rows[0][2].ToString();
                try
                {
                    this.LblNewReferral_Today2.Text = set.Tables[0].Rows[0][3].ToString();
                    this.Referral_today.InnerHtml = "New Referal Appointment";
                }
                catch
                {
                    this.Referral_today.InnerHtml = "-";
                }
                break;

            case "ImgBtn_Visits":
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_VISIT_COUNT", this.UserId);
                this.LblTotal_Visits2.Text = "<a href='#' title='View Details'> " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_BILLED_VISIT_COUNT", this.UserId);
                this.LblBilled_Visits2.Text = "<a href='#' title='View Details'> " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_UNBILLED_VISIT_COUNT", this.UserId);
                this.LblUnBilled_Visits2.Text = "<a href='#' title='View Details'> " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "TotalBilledUnbilledVisit", this.UserId);
                this.grdTotalVisit.DataSource = set.Tables[0];
                this.grdTotalVisit.DataBind();
                this.grdVisit.DataSource = set.Tables[1];
                this.grdVisit.DataBind();
                this.grdUnVisit.DataSource = set.Tables[2];
                this.grdUnVisit.DataBind();
                return;

            case "ImgBtn_Procedure":
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_BILLEDPROC", this.UserId);
                this.LblBilled_Procedure2.Text = set.Tables[0].Rows[0][0].ToString();
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_UNBILLEDPROC", this.UserId);
                this.LblUnBilled_Procedure2.Text = set.Tables[0].Rows[0][0].ToString();
                return;

            case "ImgBtn_PateintsVisits":
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_PATIENT_VISIT_SCHEDULED_COUNT", this.UserId);
                this.LblScheduled_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientscheduled' title='View Details' onclick=\"javascript:OpenPage('PatientScheduled');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_PATIENT_VISIT_NO_SHOWS", this.UserId);
                this.LblNoshow_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientnoshows' title='View Details' onclick=\"javascript:OpenPage('PatientNoShows');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_PATIENT_VISIT_RESCHEDULED", this.UserId);
                this.LblRescheduled_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientrescheduled' title='View Details' onclick=\"javascript:OpenPage('PatientRescheduled');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_PATIENT_VISIT_COMPLETED", this.UserId);
                this.LblComplete_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientvisitcompleted' title='View Details' onclick=\"javascript:OpenPage('PatientVisitcompleted');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                return;

            case "ImgBtn_MissingInf":
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "MISSING_INSURANCE_COMPANY", this.UserId);
                this.LblCompany_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "MISSING_ATTORNEY", this.UserId);
                this.LblAttorney_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "MISSING_CLAIM_NUMBER", this.UserId);
                this.LblClaim_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "MISSING_REPORT_NUMBER", this.UserId);
                this.LblReport_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "MISSING_POLICY_HOLDER", this.UserId);
                this.LblPolicyHldr_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingPolicyHolder'> " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "UNSENTNF2", this.UserId);
                this.LblUNSENTNF2_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=UnSentNF2' > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                return;

            case "ImgBtn_Desk":
                set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_LETIGATION_COUNT", this.UserId);
                this.LblBillsDue_Desk2.Text = "<a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' title='View Details' onclick=\"javascript:OpenPage('Litigation');\" > " + set.Tables[0].Rows[0][0].ToString() + "</a>";
                return;

            case "ImgBtn_Weekly":
                {
                    int dayOfWeek = (int)Convert.ToDateTime(DateTime.Today.ToString()).DayOfWeek;
                    DateTime time = Convert.ToDateTime(DateTime.Today.ToString()).AddDays((double)-dayOfWeek);
                    set = dbo.AllDashBoardData(time.ToString(), time.AddDays(6.0).ToString(), "", "", this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "GET_APPOINTMENT_DASHBOARD", this.UserId);
                    this.LblNoshow_Weekly2.Text = set.Tables[0].Rows[0][0].ToString();
                    this.LblScheduled_Weekly2.Text = set.Tables[0].Rows[0][1].ToString();
                    this.LblComplete_Weekly2.Text = set.Tables[0].Rows[0][2].ToString();
                    break;
                }
            default:
                return;
        }
    }

    protected void setLabels()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO dbo = new DashBoardBO();
        this._billTransactionBO = new Bill_Sys_BillTransaction_BO();
        try
        {
            int dayOfWeek = (int)Convert.ToDateTime(DateTime.Today.ToString()).DayOfWeek;
            DateTime time = Convert.ToDateTime(DateTime.Today.ToString()).AddDays((double)-dayOfWeek);
            DateTime time2 = time.AddDays(6.0);
            DataSet set = new DataSet();
            set = dbo.AllDashBoardData(DateTime.Today.ToString(), DateTime.Today.ToString(), time.ToString(), time2.ToString(), this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE, "ALL", this.UserId);
            this.LblNoshow_Today2.Text = set.Tables[0].Rows[0][0].ToString();
            this.LblScheduled_Today2.Text = set.Tables[0].Rows[0][1].ToString();
            this.LblComplete_Today2.Text = set.Tables[0].Rows[0][2].ToString();
            try
            {
                this.LblNewReferral_Today2.Text = set.Tables[0].Rows[0][3].ToString();
                this.Referral_today.InnerHtml = "New Referal Appointment";
            }
            catch
            {
                this.Referral_today.InnerHtml = "-";
            }
            this.Lblshow_missingdoc.Text = "<a href='Bill_Sys_Notification_Report.aspx'>Show</a>";
            this.LblNoshow_Weekly2.Text = set.Tables[0x19].Rows[0][0].ToString();
            this.LblScheduled_Weekly2.Text = set.Tables[0x19].Rows[0][1].ToString();
            this.LblComplete_Weekly2.Text = set.Tables[0x19].Rows[0][2].ToString();
            this.LblCompany_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + set.Tables[1].Rows[0][0].ToString() + "</a>";
            this.LblAttorney_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + set.Tables[2].Rows[0][0].ToString() + "</a>";
            this.LblClaim_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + set.Tables[3].Rows[0][0].ToString() + "</a>";
            this.LblReport_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + set.Tables[4].Rows[0][0].ToString() + "</a>";
            this.LblPolicyHldr_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=MissingPolicyHolder'> " + set.Tables[5].Rows[0][0].ToString() + "</a>";
            Bill_Sys_BillingCompanyDetails_BO s_bo = new Bill_Sys_BillingCompanyDetails_BO();
            if (s_bo.GetRefDocID(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString()) != "")
            {
                this.Missing_Inflbl.Text = "-";
                this.LblUNSENTNF2_MissingInf2.Text = "";
            }
            else
            {
                this.Missing_Inflbl.Text = "Unsent&nbsp;NF2";
                this.LblUNSENTNF2_MissingInf2.Text = "<a href='Bill_Sys_MissingInformation.aspx?Flag=UnSentNF2'> " + set.Tables[14].Rows[0][0].ToString() + "</a>";
            }
            this.LblReceived_Report2.Text = "<a href='Bill_Sys_ReffPaidBills.aspx' title='View Details' onclick=\"javascript:OpenPage('Litigation');\" > " + set.Tables[6].Rows[0][0].ToString() + "</a>";
            this.LblPending_Report2.Text = "<a href='Bill_Sys_paid_bills.aspx?Flag=report&Type=P' title='View Details' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + set.Tables[7].Rows[0][0].ToString() + "</a>";
            this.LblBilled_Procedure2.Text = set.Tables[9].Rows[0][0].ToString();
            this.LblUnBilled_Procedure2.Text = set.Tables[10].Rows[0][0].ToString();
            this.LblTotal_Visits2.Text = "<a href='#' title='View Details'> " + set.Tables[11].Rows[0][0].ToString() + "</a>";
            this.LblBilled_Visits2.Text = "<a href='#' title='View Details'> " + set.Tables[12].Rows[0][0].ToString() + "</a>";
            this.LblUnBilled_Visits2.Text = "<a href='#' title='View Details'> " + set.Tables[13].Rows[0][0].ToString() + "</a>";
            this.LblScheduled_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientscheduled' title='View Details' onclick=\"javascript:OpenPage('PatientScheduled');\" > " + set.Tables[15].Rows[0][0].ToString() + "</a>";
            this.LblNoshow_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientnoshows' title='View Details' onclick=\"javascript:OpenPage('PatientNoShows');\" > " + set.Tables[0x10].Rows[0][0].ToString() + "</a>";
            this.LblRescheduled_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientrescheduled' title='View Details' onclick=\"javascript:OpenPage('PatientRescheduled');\" > " + set.Tables[0x11].Rows[0][0].ToString() + "</a>";
            this.LblComplete_PateintsVisits2.Text = "<a href='../Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientvisitcompleted' title='View Details' onclick=\"javascript:OpenPage('PatientVisitcompleted');\" > " + set.Tables[0x12].Rows[0][0].ToString() + "</a>";
            this.LblPaid_Status2.Text = "<a href='Bill_Sys_paid_bills.aspx?Flag=Paid' title='View Details' onclick=\"javascript:OpenPage('Paid');\" > " + set.Tables[0x13].Rows[0][0].ToString() + "</a>";
            this.LblUnPaid_Status2.Text = "<a href='Bill_Sys_paid_bills.aspx?Flag=UnPaid' title='View Details' onclick=\"javascript:OpenPage('UnPaid');\" > " + set.Tables[20].Rows[0][0].ToString() + "</a>";
            this.LblBillsDue_Desk2.Text = "<a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' title='View Details' onclick=\"javascript:OpenPage('Litigation');\" > " + set.Tables[0x15].Rows[0][0].ToString() + "</a>";
            this.ConfigDashBoard(set.Tables[8], set.Tables[0x16], set.Tables[0x17], set.Tables[0x18]);
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


