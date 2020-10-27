/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_ScheduleReport.aspx.cs
/*Purpose              :       Display Report
/*Author               :       Sandeep D
/*Date of creation     :       07 May 2009  
/*Modified By          :       
/*Modified Date        :        29 Jun 2009
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
using System.Data.Sql;
using System.Data.SqlClient;

public partial class Bill_Sys_CalPatientEntry : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    Bill_Sys_Schedular _obj;
    Calendar_DAO objCalendar = null;
    private Boolean blnWeekShortNames = true;
    private string szDateColor_NA = "#ff6347";
    private string szDateColor_TODAY = "#FFFF80";

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnShowGrid.Attributes.Add("onclick", "return formValidator('frmCalPatientEntry','txtDate,ddlInterval,extddlReferringFacility');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (Session["PopUp"] != null)
            {
                if (Session["TestFacilityID"] != null && Session["AppointmentDate"] != null)
                {
                    if (Session["PopUp"].ToString() == "True")
                    {
                        extddlReferringFacility.Flag_ID = txtCompanyID.Text.ToString();
                        extddlReferringFacility.Text = Session["TestFacilityID"].ToString();
                        string strSelectedDate = Session["AppointmentDate"].ToString();
                        Session["AppointmentDate"] = null;
                        Session["TestFacilityID"] = null;
                        GetCalenderDayAppointments(Convert.ToDateTime(strSelectedDate).ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
                        Session["PopUp"] = null;
                    }
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        extddlReferringFacility.Visible = false;
                        lblTestFacility.Visible = false;
                    }
                    extddlReferringFacility.Flag_ID = txtCompanyID.Text.ToString();
                    //txtDate.Text = DateTime.Today.Date.ToShortDateString();
                    _obj = new Bill_Sys_Schedular();
                    if (extddlReferringFacility.Visible == false)
                    {
                        GetCalenderDayAppointments(System.DateTime.Today.Date.ToString("MM/dd/yyyy"), txtCompanyID.Text);
                        // grdScheduleReport.DataSource = _obj.GET_EVENT_DETAIL(txtCompanyID.Text, Convert.ToDateTime(txtDate.Text), Convert.ToDecimal(ddlInterval.SelectedValue), txtCompanyID.Text);
                        //grdScheduleReport.DataBind();
                    }
                    else
                    {
                        GetCalenderDayAppointments(System.DateTime.Today.Date.ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
                    }
                    if (Request.QueryString["Flag"] != null)
                    {
                        Session["Flag"] = true;
                        lblHeaderPatientName.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME;
                    }
                    else
                    {
                        Session["Flag"] = null;
                        lblHeaderPatientName.Text = "";
                    }
                }
            }
            string strDate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");

            //objCalendar = new Calendar_DAO();
            //objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).AddMonths(-1).ToString("MMM").ToUpper().ToString();
            //objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).AddMonths(-1).ToString("MMM").ToUpper().ToString();
            //objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).AddMonths(-1).Year.ToString();

            //showCalendar(objCalendar);


            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

            showCalendar(objCalendar);


            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).AddMonths(1).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).AddMonths(1).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).AddMonths(1).Year.ToString();

            showCalendar(objCalendar);
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
            cv.MakeReadOnlyPage("Bill_Sys_CalPatientEntry.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
             _obj = new Bill_Sys_Schedular();
            decimal temp = 0.30M;

            grdScheduleReport.DataSource = _obj.GET_EVENT_DETAIL(txtCompanyID.Text, DateTime.Today, temp,extddlReferringFacility.Text);
            grdScheduleReport.DataBind();
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

    //private string GetOpenCaseStatus()
    //{
    //    String strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    //    SqlConnection sqlCon;
    //    SqlCommand sqlCmd;
    //    SqlDataAdapter sqlda;
    //    SqlDataReader dr;
    //    DataSet ds;
    //    string caseStatusID = "";
    //    sqlCon = new SqlConnection(strsqlCon);
    //    try
    //    {
    //        sqlCon.Open();
    //        sqlCmd = new SqlCommand("Select SZ_CASE_STATUS_ID FROM MST_CASE_STATUS WHERE SZ_STATUS_NAME='OPEN' AND SZ_COMPANY_ID='" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'", sqlCon);
    //        dr = sqlCmd.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            caseStatusID = Convert.ToString(dr[0].ToString());
    //        }
    //    }
    //    catch { }
    //    return caseStatusID;
    //}

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    _saveOperation = new SaveOperation();
    //    Billing_Sys_ManageNotesBO manageNotes;
    //    try
    //    {
    //        manageNotes = new Billing_Sys_ManageNotesBO();
    //        _saveOperation.WebPage = this.Page;
    //        _saveOperation.Xml_File = "Cal_Patient.xml";
    //        _saveOperation.SaveMethod();

    //        txtPatientID.Text = manageNotes.GetPatientLatestID();

    //        _saveOperation.WebPage = this.Page;
    //        _saveOperation.Xml_File = "Cal_PatientCaseMaster.xml";
    //        _saveOperation.SaveMethod();

    //        ClearControl();
    //        lblMsg.Visible = true;
            
    //        lblMsg.Text = " Patient Information Saved successfully ! ";
            
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    //private void ClearControl()
    //{
    //    try
    //    {
    //        txtBirthdate.Text = "";
    //        txtMI.Text = "";
    //        txtPatientAddress.Text = "";
    //        txtPatientAge.Text = "";
    //        txtPatientFName.Text = "";
    //        txtPatientID.Text = "";
    //        txtPatientLName.Text = "";
    //        txtPatientPhone.Text = "";
    //        txtState.Text = "";
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}
    protected void btnShowGrid_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string szScheduleDay = String.Format("{0:MM/dd/yyyy}", DateTime.Now.Date.ToShortDateString()); // MMDDYY format
            if (extddlReferringFacility.Visible == false)
            {
                GetCalenderDayAppointments(szScheduleDay, txtCompanyID.Text);
            }
            else
            {
                GetCalenderDayAppointments(szScheduleDay, extddlReferringFacility.Text);
            }
           // Bill_Sys_Schedular _obj = new Bill_Sys_Schedular();
           ////Previous Code
           // // if (extddlReferringFacility.e == false)
           // if (extddlReferringFacility.Visible == false)
           // {
           //     //grdScheduleReport.DataSource = _obj.GET_EVENT_DETAIL(txtCompanyID.Text, Convert.ToDateTime(txtDate.Text), Convert.ToDecimal(ddlInterval.SelectedValue), txtCompanyID.Text);
           // }
           // else
           // {
           //     //grdScheduleReport.DataSource = _obj.GET_EVENT_DETAIL(txtCompanyID.Text, Convert.ToDateTime(txtDate.Text), Convert.ToDecimal(ddlInterval.SelectedValue), extddlReferringFacility.Text);
           // }
           // grdScheduleReport.DataBind();
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


    //Changes Made By manoj
    private class Calendar_DAO
    {
        private string szInitDisplayMonth = null;
        private string szControlIDPrefix = null;
        private string szInitDisplayYear = null;

        public string InitialDisplayYear
        {
            get
            {
                return szInitDisplayYear;
            }
            set
            {
                szInitDisplayYear = value;
            }
        }

        public string InitialDisplayMonth
        {
            get
            {
                return szInitDisplayMonth;
            }
            set
            {
                szInitDisplayMonth = value;
            }
        }

        public string ControlIDPrefix
        {
            get
            {
                return szControlIDPrefix;
            }
            set
            {
                szControlIDPrefix = value;
            }
        }
    }
    private void showCalendar(Calendar_DAO objCalendar)
    {
        //Response.Write("<table border='1' width='300px'>");

        UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<table border='1' width='80px'>"));

        // start -- fill the long name of the month
        UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<tr><td colspan='7'>"));
        string szLongName = "<div align='center' style='font-size:11px;font-weight:bold'>@LONG_MONTH_NAME@</div>";
        szLongName = szLongName.Replace("@LONG_MONTH_NAME@", getLongMonthName(objCalendar.InitialDisplayMonth));

        UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl(szLongName));
        UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("</td>"));

        // -- ends

        // fill the weekdays first
        UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<tr>"));
        byte bytStartIndex = getStartIndex(getFirstDayOfMonth(objCalendar.InitialDisplayMonth, objCalendar.InitialDisplayYear));

        string[] szWeekdays = null;

        if (blnWeekShortNames == true)
        {
            szWeekdays = getShortNamesForWeekdays();
        }
        else
        {
            szWeekdays = getOrderedWeekdays();
        }

        for (int iWeekday = 0; iWeekday < szWeekdays.Length; iWeekday++)
        {
            //Response.Write("<td>" + szWeekdays[iWeekday] + "</td>");
            UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px'>" + szWeekdays[iWeekday] + "</td>"));
        }

        //Response.Write("</tr>");
        UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("</tr>"));

        // calendar days
        byte bytDayCounter = 1;
        int iWeekdays = 0;
        bool isFirstRow = true;
        byte bytTodaysDate = getTodaysDayOfTheMonth();

        LinkButton objLink = null;

        for (int i = 0; i < 6; i++)
        {
            //Response.Write("<tr>");
            UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<tr>"));

            for (iWeekdays = 0; iWeekdays < 7; iWeekdays++)
            {
                if (iWeekdays < bytStartIndex && isFirstRow == true)
                {
                    // skip these date boxes as the month does not have these starting days
                    //Response.Write("<td bgcolor='" + szDateColor_NA + "'> N/A </TD>"); // no days available for the month
                    UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' bgcolor='" + szDateColor_NA + "'> N/A </TD>"));
                }
                else
                {
                    if (bytDayCounter > getLastMonthInteger())
                    {
                        //Response.Write("<td bgcolor ='" + szDateColor_NA + "'> N/A </TD>"); // no days available for the month
                        UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' bgcolor ='" + szDateColor_NA + "'> N/A </TD>"));
                    }
                    else
                    {
                        if (bytTodaysDate == bytDayCounter && DateTime.Now.Month == getIntMonth(objCalendar.InitialDisplayMonth)) // check for month and year too.
                        {
                            string szOutput = "";
                            objLink = new LinkButton();
                            objLink.ID = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                            objLink.CommandArgument = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                            objLink.Text = "" + bytDayCounter;
                            objLink.Click += new EventHandler(Link1_Click);

                            szOutput = "<td style='width:10px;font-size:9px' align='center' bgcolor='@BG_COLOR@'>";
                            szOutput = szOutput.Replace("@BG_COLOR@", szDateColor_TODAY);

                            UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl(szOutput));
                            UpdatePanel1.ContentTemplateContainer.Controls.Add(objLink);
                            UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("</td>"));
                            //UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl(szOutput));
                        }
                        else
                        {
                            //Response.Write("<td align='center'>" + bytDayCounter + "</td>");
                            objLink = new LinkButton();
                            objLink.ID = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                            objLink.CommandArgument = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                            objLink.Text = "" + bytDayCounter;
                            objLink.Click += new EventHandler(Link1_Click);
                            UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' align='center'>"));
                            UpdatePanel1.ContentTemplateContainer.Controls.Add(objLink);
                            UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("</td>"));
                        }
                    }
                    bytDayCounter++;
                }
            }
            isFirstRow = false;
            //Response.Write("</tr>");
            UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("</tr>"));
        }
        //Response.Write("</table>");
        UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("</table>"));
    }
    private string getLongMonthName(string p_szShortMonth)
    {
        p_szShortMonth = p_szShortMonth.ToUpper();
        switch (p_szShortMonth)
        {
            case "JAN":
                return "January";
            case "FEB":
                return "February";
            case "MAR":
                return "March";
            case "APR":
                return "April";
            case "MAY":
                return "May";
            case "JUN":
                return "June";
            case "JUL":
                return "July";
            case "AUG":
                return "August";
            case "SEP":
                return "September";
            case "OCT":
                return "October";
            case "NOV":
                return "November";
            case "DEC":
                return "December";
            default:
                return "January";
        }
        return "January";
    }
    private string getFirstDayOfMonth(string p_szMonth, string p_szYear)
    {
        int iMonth = getIntMonth(p_szMonth);
        int iYear = Int32.Parse(p_szYear);
        int iDay = 01;
        DateTime objTDay = new DateTime(iYear, iMonth, iDay);
        return objTDay.DayOfWeek.ToString();
    }
    private byte getStartIndex(string p_szDayName)
    {
        string[] szWeekdays = getOrderedWeekdays();
        for (byte iWeekday = 0; iWeekday < szWeekdays.Length; iWeekday++)
        {
            if (p_szDayName.CompareTo(szWeekdays[iWeekday]) == 0)
            {
                return iWeekday;
            }
        }
        return 0;
    }
    private String[] getShortNamesForWeekdays()
    {
        string[] szWeekdays = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        return szWeekdays;
    }
    private String[] getOrderedWeekdays()
    {
        string[] szWeekdays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        return szWeekdays;
    }
    private byte getTodaysDayOfTheMonth()
    {
        return (byte)DateTime.Now.Day;
    }
    private int getLastMonthInteger()
    {
        return DateTime.DaysInMonth(Int32.Parse(objCalendar.InitialDisplayYear), getIntMonth(objCalendar.InitialDisplayMonth));
    }
    private int getIntMonth(string p_szMonth)
    {
        p_szMonth = p_szMonth.ToUpper();
        switch (p_szMonth)
        {
            case "JAN":
                return 1;
            case "FEB":
                return 2;
            case "MAR":
                return 3;
            case "APR":
                return 4;
            case "MAY":
                return 5;
            case "JUN":
                return 6;
            case "JUL":
                return 7;
            case "AUG":
                return 8;
            case "SEP":
                return 9;
            case "OCT":
                return 10;
            case "NOV":
                return 11;
            case "DEC":
                return 12;
            default:
                return 1;
        }
        return 1;
    }


    protected void Link1_Click(object sender, EventArgs e)
    {
        // day view starts

        LinkButton objLink = (LinkButton)sender;
        Label1.Text = "Refreshed at " + DateTime.Now.ToString() + " " + " pressed by " + objLink.CommandArgument;

        string[] szDate = null;
        szDate = objLink.CommandArgument.Split('_');

        string szYear = szDate[0];
        string szMonth = szDate[1];
        string szBogus = szDate[2];
        string szDay = szDate[3];

        DateTime objSource = new DateTime(Int32.Parse(szYear), getIntMonth(szMonth), Int32.Parse(szDay));
        //lblCurrentDate.Text = String.Format("{0:dddd, MMMM d, yyyy}", objSource);
        string szScheduleDay = String.Format("{0:MM/dd/yyyy}", objSource); // MMDDYY format
        if (extddlReferringFacility.Visible == false)
        {
            GetCalenderDayAppointments(szScheduleDay, txtCompanyID.Text);
        }
        else
        {
            GetCalenderDayAppointments(szScheduleDay, extddlReferringFacility.Text);
        }
        // day view
        //string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        //SqlConnection sqlCon = new SqlConnection(strConn);
        //try
        //{
        //    SqlCommand sqlCmd;
        //    SqlDataReader dr = null;

        //    sqlCon.Open();
        //    sqlCmd = new SqlCommand("GET_ROOM_DETAILS_TEMP", sqlCon);
        //    sqlCmd.CommandType = CommandType.StoredProcedure;

        //    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        //    sqlCmd.Parameters.AddWithValue("@DT_DATE", Convert.ToDateTime(szScheduleDay).ToString("MM/dd/yyyy"));
        //    sqlCmd.Parameters.AddWithValue("@I_INTERVAL", ddlInterval.SelectedValue);
        //    sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_ID", extddlReferringFacility.Text);
        //    dr = sqlCmd.ExecuteReader();

        //    Label1.Text = "<table width='100%'>";
        //    while (dr.Read())
        //    {
        //        Label1.Text = Label1.Text + "<tr>";
        //        //#FFFFEA
        //        Label1.Text = Label1.Text + "<td style='text-align:center;font-family:Times New Roman;font-weight:bold;font-size:14px;background-color:#E0E0E0;width:20px;align:center'>";
        //        //                Label1.Text = Label1.Text + "<div style='text-align:center;font-family:Times New Roman;font-weight:bold;font-size:14px;background-color:#E0E0E0;width:100%;height:100%;'>";
        //        Label1.Text = Label1.Text + dr[0];
        //        //                Label1.Text = Label1.Text + "</div>";
        //        Label1.Text = Label1.Text + "</td>";

        //        Label1.Text = Label1.Text + "<td width='90%' style='background-color:#FFFFEA'>";
        //        Label1.Text = Label1.Text + "<div style='text-align:center;font-size:10px;background-color:#FFFFD5;border-bottom: gray 1px solid;width:100%;height:20px;'>";

        //        // all columns will go here
        //        Label1.Text = Label1.Text + "<table width='100%'><tr>";

        //        for (int i = 1; i < 5; i++) // counter for number of rooms...
        //        {
        //            try
        //            {
        //                Label1.Text = Label1.Text + "<td>";
        //                Label1.Text = Label1.Text + dr[i];
        //                Label1.Text = Label1.Text + "</td>";
        //            }
        //            catch (Exception op)
        //            {
        //                Label1.Text = Label1.Text + "</td>";
        //                break;
        //            }
        //        }
        //        Label1.Text = Label1.Text + "</tr></table>";
        //        Label1.Text = Label1.Text + "</div>";

        //        Label1.Text = Label1.Text + "</td>";
        //        Label1.Text = Label1.Text + "</tr>";
        //    }
        //}
        //catch (SqlException ex)
        //{
        //    ex.Message.ToString();
        //}
        //finally
        //{
        //    if (sqlCon.State == ConnectionState.Open)
        //    {
        //        sqlCon.Close();
        //    }
        //}
        //Label1.Text = Label1.Text + "</table>";

        // day view ends
        
        
        
        //LinkButton objLink = (LinkButton)sender;
        ////Label1.Text = "Refreshed at " + DateTime.Now.ToString() + " " + " pressed by " + objLink.CommandArgument;

        //string[] szDate = null;
        //szDate = objLink.CommandArgument.Split('_');

        //string szYear = szDate[0];
        //string szMonth = szDate[1];
        //string szBogus = szDate[2];
        //string szDay = szDate[3];

        //DateTime objSource = new DateTime(Int32.Parse(szYear), getIntMonth(szMonth), Int32.Parse(szDay));

        //int iStartDifference = getStartOfWeekIndex("" + objSource.DayOfWeek);
        //DateTime objWeekStart = objSource.AddDays(-1 * iStartDifference);

        //int iEndDifference = getEndOfWeekIndex("" + objSource.DayOfWeek);
        //DateTime objWeekEnd = objSource.AddDays(iEndDifference);

        //Label1.Text = "<a href=''>" + objWeekStart.ToString() + " ---- " + objWeekEnd.ToString() + "</a>";
        //getWeekSchedule(objWeekStart, objWeekEnd);
    }


    private void GetCalenderDayAppointments(string p_szScheduleDay , string referralid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        lblCurrentDate.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(p_szScheduleDay));
        if (Session["AppointmentDate"] == null || Session["AppointmentDate"] == "" || Session["AppointmentDate"].ToString() != p_szScheduleDay)
        {
            Session["AppointmentDate"] = p_szScheduleDay;
        }
        if (Session["TestFacilityID"] == null || Session["TestFacilityID"] == "" || Session["TestFacilityID"].ToString() != referralid)
        {
            Session["TestFacilityID"] = referralid;
        }
        string szScheduleDay = p_szScheduleDay;

        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        SqlCommand sqlCmd;
        SqlDataReader dr = null;
        ArrayList _arrayList = new ArrayList();
        try
        {

            // start Code to fetch room names

            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_ROOM_NAMES_FOR_DAY_VIEW", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
           // sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO00023");
            sqlCmd.Parameters.AddWithValue("@DT_DATE", Convert.ToDateTime(szScheduleDay).ToString("MM/dd/yyyy"));
            sqlCmd.Parameters.AddWithValue("@I_INTERVAL", ddlInterval.SelectedValue);
            sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_ID", referralid);
            dr = sqlCmd.ExecuteReader();

            while(dr.Read())
            {
                 for (int i = 0; i < dr.FieldCount; i++) 
                 {
                     _arrayList.Add(dr[i]);
                 }
            }
            dr.Close();
            sqlCon.Close();

            //end  Code to fetch room names
           

            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_ROOM_DETAILS_FINAL", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

          sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            //sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID","CO00023");
            sqlCmd.Parameters.AddWithValue("@DT_DATE", Convert.ToDateTime(szScheduleDay).ToString("MM/dd/yyyy"));
            sqlCmd.Parameters.AddWithValue("@I_INTERVAL", ddlInterval.SelectedValue);
            sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_ID", referralid);
            dr = sqlCmd.ExecuteReader();

            Label1.Text = "<table width='100%'>";


           
            // adding arrayList values to table

            Label1.Text = Label1.Text + "<tr>";
            Label1.Text = Label1.Text + "<td>&nbsp;";
            Label1.Text = Label1.Text + "</td>";

            for (int icount = 0; icount < _arrayList.Count; icount++)
            {
                Label1.Text = Label1.Text + "<td style='text-align:center;'>";
                //Label1.Text = Label1.Text + "<div style='text-align:center;font-size:10px;background-color:#FFFFD5;border-bottom: gray 1px solid;width:100%;height:20px;'>";
                Label1.Text = Label1.Text + _arrayList[icount].ToString();
                //Label1.Text = Label1.Text + "</div>";
                Label1.Text = Label1.Text + "</td>";
            }               
            
            Label1.Text = Label1.Text + "</tr>";
            //
                // <tr>
                // <td>
                // &nbsp;
                //</td>
            // loop all the rooms



            // loop ends
                    
            //
            while (dr.Read())
            {
                Label1.Text = Label1.Text + "<tr>";
                //#FFFFEA
                Label1.Text = Label1.Text + "<td style='text-align:center;font-family:Times New Roman;font-weight:bold;font-size:12px;background-color:#E0E0E0;width:80px;align:left'>";
                //                Label1.Text = Label1.Text + "<div style='text-align:center;font-family:Times New Roman;font-weight:bold;font-size:14px;background-color:#E0E0E0;width:100%;height:100%;'>";
                Label1.Text = Label1.Text + dr[0];
                //                Label1.Text = Label1.Text + "</div>";
                Label1.Text = Label1.Text + "</td>";

                Label1.Text = Label1.Text + "<td width='90%' style='background-color:#FFFFEA;' colspan='" + _arrayList.Count.ToString() + "'  >";
                Label1.Text = Label1.Text + "<div style='text-align:center;font-size:12px;background-color:#FFFFD5;border-bottom: gray 1px solid;width:100%;height:20px;'>";

                // all columns will go here
                Label1.Text = Label1.Text + "<table width='100%'><tr>";

                for (int i = 1; i <= _arrayList.Count; i++) // counter for number of rooms...
                {
                    try
                    {
                        Label1.Text = Label1.Text + "<td >";
                        Label1.Text = Label1.Text + dr[i];
                        Label1.Text = Label1.Text + "</td>";
                    }
                    catch (Exception op)
                    {
                        Label1.Text = Label1.Text + "</td>";
                        break;
                    }
                }
                Label1.Text = Label1.Text + "</tr></table>";
                Label1.Text = Label1.Text + "</div>";

                Label1.Text = Label1.Text + "</td>";
                Label1.Text = Label1.Text + "</tr>";
            }
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        Label1.Text = Label1.Text + "</table>";
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    //***********//
}