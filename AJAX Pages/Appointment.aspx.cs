using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Web;
using DevExpress.Data;
using System.Collections;
using mbs.provider;


public partial class AJAX_Pages_Appointment : PageBase
{
    private readonly string STYLE_CALENDAR_HEADER = "width:25%;font-family:Calibri;font-size:14px;text-align:center;font-style:italic;color:Red";
    private readonly string STYLE_APPOINTMENT_PATIENT_NAME = "font-family:Calibri;font-size:14px;text-decoration:none;color:#333333;";
    Calendar_DAO objCalendar = null;
    private Boolean blnWeekShortNames = true;
    private string szDateColor_NA = "#ff6347";
    private string szDateColor_TODAY = "#FFFF80";
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;


    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack && !IsCallback)
        {
            Session["ROLL"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower();
            if (Request.QueryString["appointmentDate"] != null)
            {
                hdnEventDate.Value = Request.QueryString["appointmentDate"].ToString();
                AppointmentDate.Value = Request.QueryString["appointmentDate"].ToString();
            }
            if (Request.QueryString["interval"] != null)
            {
                hdnInterval.Value = Request.QueryString["interval"].ToString();
            }
            if (Request.QueryString["reffFacility"] != null)
            {
                hdnTestFacilty.Value = Request.QueryString["reffFacility"].ToString();
            }

            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                cmbFacility.Visible = false;
                lblTestFacility.Visible = false;
            }
            else
            {
                BindFacilityDropDown();
            }
            int iYear = DateTime.Today.Year;
            BindYearDropDown(iYear);
            ddlYearList.SelectedValue = iYear.ToString();
            ddlMonthList.SelectedValue = DateTime.Today.Month.ToString();

            Panel1.Controls.Clear();
            //txtEnteredDate.Text = calDate.SelectedDate.ToString();

            DateTime dtTemp;
            if (AppointmentDate.Value != null)
            {
                dtTemp = Convert.ToDateTime(AppointmentDate.Value);
            }
            else
            {
                dtTemp = Convert.ToDateTime(System.DateTime.Now.ToString());
            }

            Session["PRV_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(-1).ToString("MM/dd/yyyy");
            Session["CUR_MONTH"] = Convert.ToDateTime(dtTemp).ToString("MM/dd/yyyy");
            Session["NEXT_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(1).ToString("MM/dd/yyyy");

            Session["FROM"] = "FROM DATE";
            txtGetDay.Value = dtTemp.ToString("MMM").ToUpper().ToString() + "_" + dtTemp.Day.ToString();

            btnLoadAppointment.Attributes.Add("onclick", "javascript:return checkForTestFacility();");
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                LoadAppointment();
                lblAppointment.Text = "Showing appointments for " + Convert.ToDateTime(DateTime.Now.ToString()).ToString("dd-MMM-yyyy");
                lblAppointment.Visible = true;
            }
        }
        //else
        //{

        //    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY != true)
        //    {
        //        if (cmbFacility.Visible == true)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "alert('Please select a test facility.')", true);
        //            return;
        //        }
        //    }
        //}
        LoadCalendarAccordingToYearAndMonth();
        if (cmbFacility.Value != null)
        {
            hdnTestFacilty.Value = cmbFacility.SelectedItem.Text.ToString();
        }
        if (hdnEventDate.Value != "" && hdnInterval.Value != "" && hdnTestFacilty.Value != "")
        {
            AppointmentDate.Value = hdnEventDate.Value.ToString();
            cmbInterval.SelectedItem.Text = hdnInterval.Value.ToString();
            cmbFacility.Value = hdnTestFacilty.Value.ToString();
            LoadAppointment();
            lblAppointment.Text = "Showing appointments for " + Convert.ToDateTime(hdnEventDate.Value.ToString()).ToString("dd-MMM-yyyy");
            lblAppointment.Visible = true;
            hdnEventDate.Value = "";
            hdnInterval.Value = "";
        }
    }

    protected void LoadAppointment()
    {

        grvAppointments.Columns.Clear();
        DataSet dsRoomName = new DataSet();
        dsRoomName = getRooms();
        ArrayList rooms = new ArrayList();

        GridViewDataTextColumn colTime = new GridViewDataTextColumn();
        colTime.Caption = "Time";
        colTime.FieldName = "Time";
        colTime.CellStyle.HorizontalAlign = HorizontalAlign.Center;
        colTime.Width = new Unit(8, UnitType.Percentage);
        colTime.PropertiesEdit.EncodeHtml = false;
        this.grvAppointments.Columns.Add(colTime);

        for (int i = 0; i < dsRoomName.Tables[0].Rows.Count; i++)
        {
            string isRoomAvailable = "is" + dsRoomName.Tables[0].Rows[i]["SZ_ROOM_NAME"].ToString() + "Available";
            GridViewDataTextColumn colRoomAvlbl = new GridViewDataTextColumn();
            colRoomAvlbl.FieldName = isRoomAvailable;
            colRoomAvlbl.Visible = false;
            this.grvAppointments.Columns.Add(colRoomAvlbl);
            rooms.Add(isRoomAvailable);
        }

        for (int i = 0; i < dsRoomName.Tables[0].Rows.Count; i++)
        {
            string roomName = dsRoomName.Tables[0].Rows[i]["SZ_ROOM_NAME"].ToString();
            GridViewDataTextColumn colCTScan = new GridViewDataTextColumn();
            colCTScan.FieldName = roomName;
            colCTScan.CellStyle.HorizontalAlign = HorizontalAlign.Center;
            //colCTScan.Width = new Unit(15, UnitType.Percentage);
            double width = 100 / dsRoomName.Tables[0].Rows.Count;
            colCTScan.Width = new Unit(width, UnitType.Percentage);
            colCTScan.DataItemTemplate = new MyTemplate(rooms);
            this.grvAppointments.Columns.Add(colCTScan);
        }

        GridViewDataTextColumn colEvent = new GridViewDataTextColumn();
        colEvent.Caption = "Event ID";
        colEvent.FieldName = "i_event_id";
        colEvent.Visible = false;
        this.grvAppointments.Columns.Add(colEvent);
        this.grvAppointments.DataSource = getAppointmentData(dsRoomName);
        grvAppointments.DataBind();
    }

    protected void grvAppointments_DataBound(object sender, EventArgs e)
    {
    }

    protected void grvAppointments_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Data)
        {
            e.Row.Height = 50;
        }
    }

    private DataSet getRooms()
    {
        string[] sRooms = null;
        OutSchedule objScedule = new OutSchedule();
        DataSet ds = new DataSet();
        string reffComID = "";
        if (cmbFacility.Visible == true && hdnTestFacilty.Value.ToString() != "")
        {
            reffComID = cmbFacility.SelectedItem.Value.ToString();
        }
        else
        {
            reffComID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID; ;
        }
        ds = objScedule.GetRoomName(reffComID);
        DataTable dt = new DataTable();
        dt = ds.Tables[0].Clone();
        DataRow dr = dt.NewRow();
        //dr["SZ_ROOM_ID"] = "0";
        //dr["SZ_ROOM_NAME"] = "Time";
        //dt.Rows.Add(dr);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            sRooms = new string[ds.Tables[0].Rows.Count + 1];
            sRooms[0] = "Time";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr1 = dt.NewRow();
                dr1["SZ_ROOM_ID"] = ds.Tables[0].Rows[i]["SZ_ROOM_ID"].ToString();
                dr1["SZ_ROOM_NAME"] = ds.Tables[0].Rows[i]["SZ_ROOM_NAME"].ToString();
                dt.Rows.Add(dr1);
            }
            //sRooms[0] = "Time";
            //sRooms[1] = "MRI";
            //sRooms[2] = "XRay";
            //sRooms[3] = "CT-Scan";
        }
        DataSet tempds = new DataSet();
        tempds.Tables.Add(dt);
        return ds;

    }

    private DataSet getRoomDetailsForDay(string referingID, string apointmentdate, string interval, string userID, string companyID)
    {
        Session["INTERVAL"] = interval;
        OutSchedule objScedule = new OutSchedule();
        DataSet ds = new DataSet();
        string canCopy = (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_COPY_PATIENT_TO_TEST_FACILITY).ToString();
        if (canCopy == "1")
        {
            ds = objScedule.GetRoomAppointmentWithCopiedPatient(referingID, apointmentdate, interval, userID, companyID);
        }
        else
        {
            ds = objScedule.GetRoomAppointment(referingID, apointmentdate, interval, userID, companyID);
        }

        return ds;
    }

    private DataSet getAppointmentData(DataSet dsRoom)
    {
        DataSet ds = new DataSet();
        DataSet dsAppointment = new DataSet();
        DateTime dtTemp;
        if (AppointmentDate.Value != null)
        {
            dtTemp = Convert.ToDateTime(AppointmentDate.Value);
        }
        else
        {
            dtTemp = Convert.ToDateTime(System.DateTime.Now.ToString());
        }
        string appDate = Convert.ToDateTime(dtTemp).ToString("MM/dd/yyyy");
        string interval = cmbInterval.SelectedItem.ToString();

        string reffComID = "";
        if (cmbFacility.Visible == true)
        {
            reffComID = cmbFacility.SelectedItem.Value.ToString();
            hdnTestFacilty.Value = cmbFacility.SelectedItem.Text.ToString();
        }
        else
        {
            reffComID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            hdnTestFacilty.Value = "";
        }
        string UserID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        string CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        //string refComp=cmbFacility.SelectedItem.Value.ToString();
        dsAppointment = getRoomDetailsForDay(reffComID, appDate, interval, UserID, CompanyID);

        DataTable tblAppointments = new DataTable();

        tblAppointments.Columns.Add("Time");
        for (int i = 0; i < dsRoom.Tables[0].Rows.Count; i++)
        {
            string roomName = dsRoom.Tables[0].Rows[i]["SZ_ROOM_NAME"].ToString();
            tblAppointments.Columns.Add(roomName);
            string isRoomAvailable = "is" + dsRoom.Tables[0].Rows[i]["SZ_ROOM_NAME"].ToString() + "Available";
            tblAppointments.Columns.Add(isRoomAvailable);
        }

        tblAppointments.Columns.Add("i_event_id");
        tblAppointments.Columns.Add("sz_case_id");
        tblAppointments.Columns.Add("sz_company_id");
        tblAppointments.Columns.Add("sz_patient_id");
        tblAppointments.Columns.Add("SZ_ROOM_ID");
        tblAppointments.Columns.Add("sz_reffering_id");
        tblAppointments.Columns.Add("chartNo");
        tblAppointments.Columns.Add("Transport");
        tblAppointments.Columns.Add("ProcCount");
        tblAppointments.Columns.Add("isPatient");
        tblAppointments.Columns.Add("referingOffice");
        tblAppointments.Columns.Add("EventDate");
        tblAppointments.Columns.Add("Status");
        tblAppointments.Columns.Add("testFacility");

        string sCompareTime = "", sTime = "";
        for (int i = 0; i < dsAppointment.Tables[0].Rows.Count; i++)
        {
            string timeType = dsAppointment.Tables[0].Rows[i]["sz_type"].ToString();
            double allTime = Convert.ToDouble(dsAppointment.Tables[0].Rows[i]["sz_time"].ToString());
            if (timeType.ToLower().ToString() == "pm" && allTime < 12.00)
            {
                allTime += 12.00;
            }

            bool flag = false;
            for (int j = 0; j < dsRoom.Tables[0].Rows.Count; j++)
            {
                double start = Convert.ToDouble(dsRoom.Tables[0].Rows[j]["FL_START_TIME"].ToString());
                double end = Convert.ToDouble(dsRoom.Tables[0].Rows[j]["FL_END_TIME"].ToString());

                string isRoomAvailable = "is" + dsRoom.Tables[0].Rows[j]["SZ_ROOM_NAME"].ToString() + "Available";
                if (allTime >= start && allTime < end)
                {
                    flag = true;
                    break;
                }

            }
            if (flag)
            {
                DataRow dr = tblAppointments.NewRow();

                sTime = dsAppointment.Tables[0].Rows[i]["sz_time"].ToString() + dsAppointment.Tables[0].Rows[i]["sz_type"].ToString();// "08:00";

                if (sCompareTime.ToLower().Equals(sTime.ToLower()))
                {
                    //dr["Time"] = ""; mahesh 257
                    dr["Time"] = "<span style='color:white'>" + sTime + "</span>";
                }
                else
                {
                    dr["Time"] = sTime;
                }

                for (int j = 0; j < dsRoom.Tables[0].Rows.Count; j++)
                {
                    double start = Convert.ToDouble(dsRoom.Tables[0].Rows[j]["FL_START_TIME"].ToString());
                    double end = Convert.ToDouble(dsRoom.Tables[0].Rows[j]["FL_END_TIME"].ToString());

                    string isRoomAvailable = "is" + dsRoom.Tables[0].Rows[j]["SZ_ROOM_NAME"].ToString() + "Available";
                    if (allTime >= start && allTime < end)
                    {
                        dr[isRoomAvailable] = "1";
                        dr[isRoomAvailable] = dsRoom.Tables[0].Rows[j]["sz_room_id"].ToString();
                    }
                    else
                    {
                        dr[isRoomAvailable] = "0";
                    }
                }

                if (dsAppointment.Tables[0].Rows[i]["I_EVENT_ID"].ToString() != null && dsAppointment.Tables[0].Rows[i]["I_EVENT_ID"].ToString() != "")
                {
                    for (int j = 0; j < dsRoom.Tables[0].Rows.Count; j++)
                    {
                        string roomName = dsRoom.Tables[0].Rows[j]["SZ_ROOM_NAME"].ToString();
                        if (dsRoom.Tables[0].Rows[j]["SZ_ROOM_ID"].ToString() == dsAppointment.Tables[0].Rows[i]["SZ_ROOM_ID"].ToString())
                        {
                            dr[roomName] = dsAppointment.Tables[0].Rows[i]["sz_patient_name"].ToString();
                        }
                    }
                }

                sCompareTime = dsAppointment.Tables[0].Rows[i]["sz_time"].ToString() + dsAppointment.Tables[0].Rows[i]["sz_type"].ToString();// "08:00";
                dr["i_event_id"] = dsAppointment.Tables[0].Rows[i]["I_EVENT_ID"].ToString();
                dr["sz_case_id"] = dsAppointment.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                dr["sz_company_id"] = dsAppointment.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
                dr["sz_patient_id"] = dsAppointment.Tables[0].Rows[i]["SZ_PATIENT_ID"].ToString();
                dr["SZ_ROOM_ID"] = dsAppointment.Tables[0].Rows[i]["SZ_ROOM_ID"].ToString();
                if (cmbFacility.Visible == true)
                {
                    dr["sz_reffering_id"] = reffComID;
                }
                else
                {
                    dr["sz_reffering_id"] = reffComID;
                }
                dr["chartNo"] = dsAppointment.Tables[0].Rows[i]["chartNo"].ToString();
                dr["Transport"] = dsAppointment.Tables[0].Rows[i]["Transport"].ToString();
                dr["ProcCount"] = dsAppointment.Tables[0].Rows[i]["ProcCount"].ToString();
                dr["isPatient"] = dsAppointment.Tables[0].Rows[i]["isPatient"].ToString();
                dr["referingOffice"] = dsAppointment.Tables[0].Rows[i]["referingOffice"].ToString();
                dr["EventDate"] = dsAppointment.Tables[0].Rows[i]["EventDate"].ToString();
                dr["Status"] = dsAppointment.Tables[0].Rows[i]["Status"].ToString();
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    dr["testFacility"] = dsAppointment.Tables[0].Rows[i]["Status"].ToString();
                }
                else
                {
                    dr["testFacility"] = cmbFacility.SelectedItem.Text.ToString();
                }

                tblAppointments.Rows.Add(dr);
            }
        }

        ds.Tables.Add(tblAppointments);

        return ds;
    }

    private void LoadCalendarAccordingToYearAndMonth()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string strDate = "";
            strDate = Convert.ToDateTime(Session["PRV_MONTH"].ToString()).ToString("MM/dd/yyyy");
            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();


            showCalendar(objCalendar);

            strDate = Convert.ToDateTime(Session["CUR_MONTH"].ToString()).ToString("MM/dd/yyyy");
            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

            showCalendar(objCalendar);

            strDate = Convert.ToDateTime(Session["NEXT_MONTH"].ToString()).ToString("MM/dd/yyyy");
            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).AddMonths(1).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

            showCalendar(objCalendar);

            Session["FROM"] = null;
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

    protected void cbpCalendar_Callback(object sender, CallbackEventArgsBase e)
    {
        //Session["FROM"] = null;
        ////LinkButton objLink = (LinkButton)sender;
        ////Label1.Text = "Refreshed at " + DateTime.Now.ToString() + " " + " pressed by " + objLink.CommandArgument;

        //string[] szDate = null;
        ////szDate = objLink.CommandArgument.Split('_');
        //szDate = "2014_JAN_Link_9".Split('_');

        //string szYear = szDate[0];
        //string szMonth = szDate[1];
        //string szBogus = szDate[2];
        //string szDay = szDate[3];

        //DateTime objSource = new DateTime(Int32.Parse(szYear), getIntMonth(szMonth), Int32.Parse(szDay));
        //AppointmentDate.Value = Convert.ToDateTime(objSource).ToString("MM/dd/yyyy"); ;
        //LoadAppointment();

        ////Panel1.Controls.Clear();
        ////int iYear = objSource.Year;
        ////ddlYearList.SelectedValue = iYear.ToString();
        ////ddlMonthList.SelectedValue = objSource.Month.ToString();
        ////Session["PRV_MONTH"] = Convert.ToDateTime(objSource).AddMonths(-1).ToString("MM/dd/yyyy");
        ////Session["CUR_MONTH"] = Convert.ToDateTime(objSource).ToString("MM/dd/yyyy");
        ////Session["NEXT_MONTH"] = Convert.ToDateTime(objSource).AddMonths(1).ToString("MM/dd/yyyy");
        ////txtGetDay.Value = objSource.ToString("MMM").ToUpper().ToString() + "_" + objSource.Day.ToString();

        ////LoadCalendarAccordingToYearAndMonth();
    }

    private void showCalendar(Calendar_DAO objCalendar)
    {
        //Response.Write("<table border='1' width='300px'>");

        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("<table border='1' width='80px'>"));
        Panel1.Controls.Add(new LiteralControl("<table border='1' width='80px'>"));

        // start -- fill the long name of the month
        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("<tr><td colspan='7'>"));
        Panel1.Controls.Add(new LiteralControl("<tr><td colspan='7'>"));
        string szLongName = "<div align='center' style='font-size:11px;font-weight:bold'>@LONG_MONTH_NAME@</div>";
        szLongName = szLongName.Replace("@LONG_MONTH_NAME@", getLongMonthName(objCalendar.InitialDisplayMonth));

        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl(szLongName));
        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("</td>"));

        Panel1.Controls.Add(new LiteralControl(szLongName));
        Panel1.Controls.Add(new LiteralControl("</td>"));

        // -- ends

        // fill the weekdays first
        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("<tr>"));
        Panel1.Controls.Add(new LiteralControl("<tr>"));
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
            ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px'>" + szWeekdays[iWeekday] + "</td>"));
            Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px'>" + szWeekdays[iWeekday] + "</td>"));
        }

        //Response.Write("</tr>");
        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("</tr>"));
        Panel1.Controls.Add(new LiteralControl("</tr>"));
        // calendar days
        byte bytDayCounter = 1;
        int iWeekdays = 0;
        bool isFirstRow = true;
        byte bytTodaysDate = getTodaysDayOfTheMonth();
        int ClickDate = 0;
        int ClickMonth = 0;

        if (txtGetDay.Value.ToString() != "") { string[] strD = txtGetDay.Value.ToString().Split('_'); ClickMonth = getIntMonth(strD.GetValue(0).ToString()); ClickDate = Convert.ToInt32(strD.GetValue(1).ToString()); }

        LinkButton objLink = null;

        for (int i = 0; i < 6; i++)
        {
            Panel1.Controls.Add(new LiteralControl("<tr>"));

            for (iWeekdays = 0; iWeekdays < 7; iWeekdays++)
            {
                if (iWeekdays < bytStartIndex && isFirstRow == true)
                {
                    Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' bgcolor='" + szDateColor_NA + "'> N/A </TD>"));
                }
                else
                {
                    if (bytDayCounter > getLastMonthInteger())
                    {
                        Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' bgcolor ='" + szDateColor_NA + "'> N/A </TD>"));
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

                            Panel1.Controls.Add(new LiteralControl(szOutput));
                            Panel1.Controls.Add(objLink);
                            Panel1.Controls.Add(new LiteralControl("</td>"));
                        }
                        else if (ClickDate == bytDayCounter && ClickMonth == getIntMonth(objCalendar.InitialDisplayMonth)) // check for month and year too.
                        {
                            string szOutput = "";
                            objLink = new LinkButton();
                            objLink.ID = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                            objLink.CommandArgument = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                            objLink.Text = "" + bytDayCounter;
                            objLink.Attributes.Add("onclick", "document.getElementById('ctl00_ContentPlaceHolder1_txtGetDay').value='" + objCalendar.InitialDisplayMonth + "_" + bytDayCounter.ToString() + "';");
                            objLink.Click += new EventHandler(Link1_Click);

                            szOutput = "<td style='width:10px;font-size:9px' align='center' bgcolor='#7FFF00'>";
                            szOutput = szOutput.Replace("#7FFF00", "#7FFF00");

                            Panel1.Controls.Add(new LiteralControl(szOutput));
                            Panel1.Controls.Add(objLink);
                            Panel1.Controls.Add(new LiteralControl("</td>"));
                        }
                        else
                        {
                            if (Session["FROM"] == null)
                            {
                                objLink = new LinkButton();
                                objLink.ID = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                                objLink.CommandArgument = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                                objLink.Text = "" + bytDayCounter;
                                objLink.Click += new EventHandler(Link1_Click);
                                objLink.Attributes.Add("onclick", "document.getElementById('ctl00_ContentPlaceHolder1_txtGetDay').value='" + objCalendar.InitialDisplayMonth + "_" + bytDayCounter.ToString() + "';");
                                Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' align='center'>"));
                                Panel1.Controls.Add(objLink);
                                Panel1.Controls.Add(new LiteralControl("</td>"));
                            }
                            else
                            {
                                objLink = new LinkButton();
                                objLink.ID = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                                objLink.CommandArgument = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                                objLink.Text = "" + bytDayCounter;
                                objLink.Click += new EventHandler(Link1_Click);
                                objLink.Attributes.Add("onclick", "  document.getElementById('ctl00_ContentPlaceHolder1_txtGetDay').value='" + objCalendar.InitialDisplayMonth + "_" + bytDayCounter.ToString() + "';");
                                Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' align='center'>"));
                                Panel1.Controls.Add(objLink);
                                Panel1.Controls.Add(new LiteralControl("</td>"));
                            }
                        }
                    }
                    bytDayCounter++;
                }
            }
            isFirstRow = false;
            //Response.Write("</tr>");
            ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("</tr>"));
            Panel1.Controls.Add(new LiteralControl("</tr>"));
        }
        //Response.Write("</table>");
        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("</table>"));
        Panel1.Controls.Add(new LiteralControl("</table>"));

        //Modified By BowandBaan for Include Ajax Extension - Modification Ends
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

    private String[] getOrderedWeekdays()
    {
        string[] szWeekdays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        return szWeekdays;
    }

    private String[] getShortNamesForWeekdays()
    {
        string[] szWeekdays = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
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

    protected void Link1_Click(object sender, EventArgs e)
    {
        Session["FROM"] = null;
        LinkButton objLink = (LinkButton)sender;
        //Label1.Text = "Refreshed at " + DateTime.Now.ToString() + " " + " pressed by " + objLink.CommandArgument;

        string[] szDate = null;
        szDate = objLink.CommandArgument.Split('_');

        string szYear = szDate[0];
        string szMonth = szDate[1];
        string szBogus = szDate[2];
        string szDay = szDate[3];

        DateTime objSource = new DateTime(Int32.Parse(szYear), getIntMonth(szMonth), Int32.Parse(szDay));
        AppointmentDate.Value = Convert.ToDateTime(objSource).ToString("MM/dd/yyyy"); ;
        lblAppointment.Text = "Showing appointments for " + Convert.ToDateTime(objSource).ToString("dd-MMM-yyyy");
        lblAppointment.Visible = true;
        LoadAppointment();
        Panel1.Controls.Clear();
        int iYear = objSource.Year;
        ddlYearList.SelectedValue = iYear.ToString();
        ddlMonthList.SelectedValue = objSource.Month.ToString();
        Session["PRV_MONTH"] = Convert.ToDateTime(objSource).AddMonths(-1).ToString("MM/dd/yyyy");
        Session["CUR_MONTH"] = Convert.ToDateTime(objSource).ToString("MM/dd/yyyy");
        Session["NEXT_MONTH"] = Convert.ToDateTime(objSource).AddMonths(1).ToString("MM/dd/yyyy");
        txtGetDay.Value = objSource.ToString("MMM").ToUpper().ToString() + "_" + objSource.Day.ToString();

        LoadCalendarAccordingToYearAndMonth();

    }

    protected void btnLoadAppointment_Click(object sender, EventArgs e)
    {
        Panel1.Controls.Clear();
        DateTime dtTemp;
        if (AppointmentDate.Value != null)
        {
            dtTemp = Convert.ToDateTime(AppointmentDate.Value);
        }
        else
        {
            dtTemp = Convert.ToDateTime(System.DateTime.Now.ToString());
        }
        string date = dtTemp.ToString("MM/dd/yyyy");
        //hdnEventDate.Value = date;
        int iYear = dtTemp.Year;
        ddlYearList.SelectedValue = iYear.ToString();
        ddlMonthList.SelectedValue = DateTime.Today.Month.ToString();
        Session["PRV_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(-1).ToString("MM/dd/yyyy");
        Session["CUR_MONTH"] = Convert.ToDateTime(dtTemp).ToString("MM/dd/yyyy");
        Session["NEXT_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(1).ToString("MM/dd/yyyy");
        Session["FROM"] = "FROM DATE";

        txtGetDay.Value = dtTemp.ToString("MMM").ToUpper().ToString() + "_" + dtTemp.Day.ToString();

        LoadCalendarAccordingToYearAndMonth();
        //lblAppointment.Text = "Showing appointments for " + Convert.ToDateTime(dtTemp).ToString("dd-MMM-yyyy");
        //lblAppointment.Visible = true;
        LoadAppointment();
        lblAppointment.Text = "Showing appointments for " + Convert.ToDateTime(dtTemp).ToString("dd-MMM-yyyy");
        lblAppointment.Visible = true;
        //grvAppointments.Columns.Clear();
        //DataSet dsRoomName = new DataSet();
        //dsRoomName = getRooms();
    }

    protected void btnLoadCalendar_Click(object sender, EventArgs e)
    {
        Panel1.Controls.Clear();

        DateTime dtTemp = new DateTime(Convert.ToInt32(ddlYearList.SelectedValue), Convert.ToInt32(ddlMonthList.SelectedValue), 1);

        Session["PRV_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(-1).ToString("MM/dd/yyyy");
        Session["CUR_MONTH"] = Convert.ToDateTime(dtTemp).ToString("MM/dd/yyyy");
        Session["NEXT_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(1).ToString("MM/dd/yyyy");
        LoadCalendarAccordingToYearAndMonth();
    }

    private void BindYearDropDown(int p_iYear)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ddlYearList.Items.Clear();
            //for (int i = p_iYear; i > p_iYear - 20; i--)
            for (int i = p_iYear - 10; i <= (p_iYear + 1); i++)
                ddlYearList.Items.Add(i.ToString());
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

    private void BindFacilityDropDown()
    {
        string CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        OutSchedule objScedule = new OutSchedule();
        DataSet ds = new DataSet();
        ds = objScedule.GetReferingFacilities(CompanyID);
        cmbFacility.TextField = "DESCRIPTION";
        cmbFacility.ValueField = "CODE";
        cmbFacility.DataSource = ds;
        cmbFacility.DataBind();
    }

    protected void btnDeleteEvent_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_Calender _bill_Sys_Calender;
        try
        {
            if (hdnDeleteEvent.Value != "")
            {
                DateTime dtDate = Convert.ToDateTime(System.DateTime.Now.ToString());

                _bill_Sys_Calender = new Bill_Sys_Calender();
                int iCount = _bill_Sys_Calender.Delete_Event(Convert.ToInt32(hdnDeleteEvent.Value.ToString()), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                #region Activity_Log
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "EVENT_DELETED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Event Id : " + hdnDeleteEvent.Value.ToString() + " deleted.";
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                #endregion
                DateTime dtCurrentDate = Convert.ToDateTime(lblAppointment.Text.Trim().ToString().Substring(lblAppointment.Text.Trim().ToString().Length - 11, 11));
                string[] strSelectedDate = dtCurrentDate.ToString().Split('/');
                string strdt = strSelectedDate[0] + "/" + strSelectedDate[1] + "/" + strSelectedDate[2].Substring(0, 4).ToString();

                if (iCount > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(btnDeleteEvent, typeof(Button), "Msg", "alert('Appointment Deleted Successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(btnDeleteEvent, typeof(Button), "Msg", "alert('Can not delete appointment.')", true);
                }
                if (strdt != "")
                {
                    LoadAppointment();
                    //if (extddlReferringFacility.Visible == false)
                    //{
                    //    GetCalenderDayAppointments(Convert.ToDateTime(strdt).ToString("MM/dd/yyyy"), txtCompanyID.Text);
                    //}
                    //else
                    //{
                    //    GetCalenderDayAppointments(Convert.ToDateTime(strdt).ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
                    //}
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
        finally
        {
            _bill_Sys_Calender = null;
            hdnDeleteEvent.Value = "";
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #region unwanted code

    //private void ShowCalendar()
    //{
    //    DataSet dsRoomName = new DataSet();
    //    dsRoomName = getRooms();
    //    //string[] sRooms = getRooms();
    //    if (dsRoomName.Tables.Count > 0 && dsRoomName.Tables[0].Rows.Count > 0)
    //    {
    //        DataSet dsRoom = new DataSet();
    //        //dsRoom = getRoomDetailsForDay("CO000000000000000115", "02/09/2014");
    //        TableCell cell = null;
    //        TableCell[] tCellHolder = new TableCell[dsRoomName.Tables[0].Rows.Count];

    //        for (int i = 0; i < dsRoomName.Tables[0].Rows.Count; i++)
    //        {
    //            cell = new TableCell();
    //            Table t = null;
    //            if (dsRoomName.Tables[0].Rows[i]["SZ_ROOM_ID"].ToString() == "0")
    //            {
    //                t = getDataTable(dsRoomName.Tables[0].Rows[i]["SZ_ROOM_ID"].ToString(), dsRoom);
    //            }
    //            else
    //            {
    //                t = getDataInTable(dsRoomName.Tables[0].Rows[i]["SZ_ROOM_ID"].ToString(), dsRoom);
    //            }
    //            cell.Controls.Add(t);
    //            tCellHolder[i] = cell;
    //        }

    //        TableRow rowRooms = new TableRow();
    //        for (int i = 0; i < dsRoomName.Tables[0].Rows.Count; i++)
    //        {
    //            cell = new TableCell();
    //            cell.Text = (dsRoomName.Tables[0].Rows[i]["SZ_ROOM_NAME"].ToString());
    //            cell.Style.Value = STYLE_CALENDAR_HEADER;
    //            rowRooms.Cells.Add(cell);
    //        }

    //        TableRow row = new TableRow();
    //        for (int i = 0; i < dsRoomName.Tables[0].Rows.Count; i++)
    //        {
    //            row.Cells.Add(tCellHolder[i]);
    //        }

    //        this.tblRoomHolder.Rows.Add(rowRooms);
    //        this.tblRoomHolder.Rows.Add(row);
    //    }
    //}

    //private string[] getTime()
    //{
    //    string[] sTime = new string[5];
    //    sTime[0] = "6.00";
    //    sTime[1] = "7.00";
    //    sTime[2] = "8.00";
    //    sTime[3] = "9.00";
    //    sTime[4] = "10.00";

    //    return sTime;
    //}

    //private Table getDataTable(string roomID, DataSet dsRoom)
    //{
    //    Table t = new Table();
    //    string[] sTime = getData(roomID, dsRoom);

    //    TableRow row = null;
    //    TableCell cell = null;

    //    for (int i = 0; i < sTime.Length; i++)
    //    {
    //        row = new TableRow();
    //        cell = new TableCell();
    //        cell.Text = sTime[i];
    //        row.Cells.Add(cell);
    //        t.Rows.Add(row);
    //    }
    //    return t;
    //}

    //private string[] getData(string roomID, DataSet dsRoom)
    //{
    //    string[] sTime = null;
    //    sTime = new string[dsRoom.Tables[0].Rows.Count];
    //    string time = "";
    //    if (roomID.ToLower().ToString() == "0")
    //    {
    //        //sTime = new string[dsRoom.Tables[0].Rows.Count];
    //        //DataRow[] dr = dsRoom.Tables[0].Select(" distinct sz_time ", "i_id asc");
    //        //for (int i = 0; i < dsRoom.Tables[0].Rows.Count; i++)
    //        //{
    //        //    sTime[i] = dsRoom.Tables[0].Rows[i]["sz_time"].ToString() + " " + dsRoom.Tables[0].Rows[i]["sz_type"].ToString();
    //        //}
    //        for (int i = 0; i < dsRoom.Tables[0].Rows.Count; i++)
    //        {
    //            if (dsRoom.Tables[0].Rows[i]["sz_time"].ToString() != time)
    //            {
    //                sTime[i] = dsRoom.Tables[0].Rows[i]["sz_time"].ToString() + " " + dsRoom.Tables[0].Rows[i]["sz_type"].ToString();
    //            }
    //            else
    //            {
    //                sTime[i] = " ";
    //            }
    //            time = dsRoom.Tables[0].Rows[i]["sz_time"].ToString();
    //        }

    //    }
    //    else
    //    {
    //        OutSchedule objScedule = new OutSchedule();
    //        DataTable dt = new DataTable();
    //        dt = dsRoom.Tables[0].Clone();
    //        DataRow[] dr = dsRoom.Tables[0].Select(" SZ_ROOM_ID= '" + roomID + "'", "i_id asc");
    //        for (int i = 0; i < dr.Length; i++)
    //        {
    //            dt.ImportRow(dr[i]);
    //        }
    //        int k = 0;
    //        for (int i = 0; i < dsRoom.Tables[0].Rows.Count && i < 36; i++)
    //        {
    //            if (dsRoom.Tables[0].Rows[i]["SZ_ROOM_ID"].ToString() != "" && dsRoom.Tables[0].Rows[i]["SZ_ROOM_ID"].ToString() != null)
    //            {
    //                if (dt.Rows.Count > 0)
    //                {
    //                    if (dsRoom.Tables[0].Rows[i]["sz_time"].ToString() == dsRoom.Tables[0].Rows[i + 1]["sz_time"].ToString())
    //                    {
    //                        int j = i;
    //                        sTime[j] = "";
    //                        while (dsRoom.Tables[0].Rows[i]["sz_time"].ToString() == dsRoom.Tables[0].Rows[i + 1]["sz_time"].ToString())
    //                        {
    //                            if (dsRoom.Tables[0].Rows[i]["SZ_ROOM_NAME"].ToString() == dt.Rows[k]["SZ_ROOM_NAME"].ToString())
    //                            {
    //                                sTime[j] += "<a style='" + STYLE_APPOINTMENT_PATIENT_NAME + "'" + " href='javascript:OnAppointmentClick(1,2)'>" + dt.Rows[k]["sz_patient_id"].ToString() + "</a>";
    //                                sTime[j] += "\n";
    //                                k++;
    //                            }
    //                            else
    //                            {
    //                                sTime[j] = "ADD";
    //                            }
    //                            i++;
    //                        }
    //                    }

    //                }
    //                else
    //                {
    //                    sTime[i] = "ADD";
    //                }
    //            }
    //            else
    //            {
    //                sTime[i] = "ADD";
    //            }
    //        }
    //        //sTime[0] = "6.00";
    //        //sTime[1] = "7.00";
    //        //sTime[2] = "8.00";
    //        //sTime[3] = "9.00";
    //        //sTime[4] = "10.00";
    //    }
    //    return sTime;
    //}

    //private Table getDataInTable(string roomID, DataSet dsRoom)
    //{
    //    Table t1 = new Table();

    //    TableRow row1 = null;
    //    TableCell cell1 = null;

    //    string[] sTime = null;
    //    sTime = new string[dsRoom.Tables[0].Rows.Count];
    //    string time = "";
    //    if (roomID.ToLower().ToString() == "0")
    //    {
    //        //sTime = new string[dsRoom.Tables[0].Rows.Count];
    //        //DataRow[] dr = dsRoom.Tables[0].Select(" distinct sz_time ", "i_id asc");
    //        //for (int i = 0; i < dsRoom.Tables[0].Rows.Count; i++)
    //        //{
    //        //    sTime[i] = dsRoom.Tables[0].Rows[i]["sz_time"].ToString() + " " + dsRoom.Tables[0].Rows[i]["sz_type"].ToString();
    //        //}
    //        for (int i = 0; i < dsRoom.Tables[0].Rows.Count; i++)
    //        {
    //            row1 = new TableRow();
    //            cell1 = new TableCell();
    //            if (dsRoom.Tables[0].Rows[i]["sz_time"].ToString() != time)
    //            {
    //                cell1.Text = dsRoom.Tables[0].Rows[i]["sz_time"].ToString() + " " + dsRoom.Tables[0].Rows[i]["sz_type"].ToString();
    //            }
    //            else
    //            {
    //                cell1.Text = " ";
    //            }
    //            time = dsRoom.Tables[0].Rows[i]["sz_time"].ToString();
    //            row1.Cells.Add(cell1);
    //            t1.Rows.Add(row1);
    //        }

    //    }
    //    else
    //    {


    //        OutSchedule objScedule = new OutSchedule();
    //        DataTable dt = new DataTable();
    //        dt = dsRoom.Tables[0].Clone();
    //        DataRow[] dr = dsRoom.Tables[0].Select(" SZ_ROOM_ID= '" + roomID + "'", "i_id asc");
    //        for (int i = 0; i < dr.Length; i++)
    //        {
    //            dt.ImportRow(dr[i]);
    //        }
    //        int k = 0;
    //        for (int i = 0; i < dsRoom.Tables[0].Rows.Count && i < 36; i++)
    //        {
    //            row1 = new TableRow();
    //            cell1 = new TableCell();
    //            if (dsRoom.Tables[0].Rows[i]["SZ_ROOM_ID"].ToString() != "" && dsRoom.Tables[0].Rows[i]["SZ_ROOM_ID"].ToString() != null)
    //            {
    //                if (dt.Rows.Count > 0)
    //                {
    //                    if (dsRoom.Tables[0].Rows[i]["sz_time"].ToString() == dsRoom.Tables[0].Rows[i + 1]["sz_time"].ToString())
    //                    {
    //                        Table patientTable = new Table();
    //                        TableRow patientRow = null;
    //                        TableCell patientCell = null;
    //                        int j = i;
    //                        sTime[j] = "";
    //                        while (dsRoom.Tables[0].Rows[i]["sz_time"].ToString() == dsRoom.Tables[0].Rows[i + 1]["sz_time"].ToString())
    //                        {
    //                            patientRow = new TableRow();
    //                            if (dsRoom.Tables[0].Rows[i]["SZ_ROOM_NAME"].ToString() == dt.Rows[k]["SZ_ROOM_NAME"].ToString())
    //                            {
    //                                patientCell = new TableCell();
    //                                //patientCell = getPatientCell();
    //                                patientCell.Text = "<a style='" + STYLE_APPOINTMENT_PATIENT_NAME + "'" + " href='javascript:OnAppointmentClick(1,2)'>" + dt.Rows[k]["sz_patient_id"].ToString() + "</a>";
    //                                k++;
    //                            }
    //                            else
    //                            {
    //                                patientCell.Text = "ADD";
    //                            }
    //                            i++;
    //                            patientRow.Cells.Add(patientCell);
    //                            patientTable.Rows.Add(patientRow);
    //                        }

    //                        cell1.Controls.Add(patientTable);
    //                        row1.Cells.Add(cell1);
    //                        t1.Rows.Add(row1);
    //                    }
    //                    else
    //                    {
    //                        cell1.Text = "ADD";
    //                        row1.Cells.Add(cell1);
    //                        t1.Rows.Add(row1);
    //                    }
    //                }
    //                else
    //                {
    //                    cell1.Text = "ADD";
    //                    row1.Cells.Add(cell1);
    //                    t1.Rows.Add(row1);
    //                }
    //            }
    //            else
    //            {
    //                cell1.Text = "ADD";
    //                row1.Cells.Add(cell1);
    //                t1.Rows.Add(row1);
    //            }
    //        }
    //        //sTime[0] = "6.00";
    //        //sTime[1] = "7.00";
    //        //sTime[2] = "8.00";
    //        //sTime[3] = "9.00";
    //        //sTime[4] = "10.00";
    //        //row1.Cells.Add(cell1);
    //        //t1.Rows.Add(row1);
    //    }
    //    return t1;
    //}


    #endregion

    protected void btnReminder_Click(object sender, EventArgs e)
    {
        if (hdnDeleteEvent.Value != "")
        {

            string msg = SMSMessaging.SendReminder(Convert.ToInt32(hdnDeleteEvent.Value));
            if (msg != "Error")
            {
                this.usrMessage.PutMessage("Reminder Sent Successfully .");
                this.usrMessage.SetMessageType(0);
                this.usrMessage.Show();
            }
        }
    }
}

class MyTemplate : ITemplate
{
    ArrayList rooms = new ArrayList();
    public MyTemplate()
    {
    }
    public MyTemplate(ArrayList arr)
    {
        rooms = arr;
    }
    public void InstantiateIn(Control container)
    {
        //ASPxHyperLink link = new ASPxHyperLink();
        GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
        int i = gridContainer.ItemIndex;
        string sEventID = (string)gridContainer.Grid.GetRowValues(i, "i_event_id");
        string sCaseID = (string)gridContainer.Grid.GetRowValues(i, "sz_case_id");
        string sCompanyID = (string)gridContainer.Grid.GetRowValues(i, "sz_company_id");
        string sPatientID = (string)gridContainer.Grid.GetRowValues(i, "sz_patient_id");
        string sRoomID = (string)gridContainer.Grid.GetRowValues(i, "SZ_ROOM_ID");
        string sReffComp = (string)gridContainer.Grid.GetRowValues(i, "sz_reffering_id");
        string sChartNO = (string)gridContainer.Grid.GetRowValues(i, "chartNo");
        string sTransport = (string)gridContainer.Grid.GetRowValues(i, "Transport");
        string sProcCount = (string)gridContainer.Grid.GetRowValues(i, "ProcCount");
        string sIsPatient = (string)gridContainer.Grid.GetRowValues(i, "isPatient");
        string sRefOfficeID = (string)gridContainer.Grid.GetRowValues(i, "referingOffice");
        string sTime = (string)gridContainer.Grid.GetRowValues(i, "Time");
        string sEventDate = (string)gridContainer.Grid.GetRowValues(i, "EventDate");
        string sStatus = (string)gridContainer.Grid.GetRowValues(i, "Status");
        string sTestFacility = (string)gridContainer.Grid.GetRowValues(i, "testFacility");

        if (sTestFacility.Contains("\'"))
        {
            sTestFacility.Replace("\'", "\'\'");
        }
        int cnt = rooms.Count;

        if (gridContainer.Text != null)
        {
            if (gridContainer.Text.Trim().Length > 0)
            {
                if (!gridContainer.Text.ToLower().Contains("nbsp"))
                {
                    container.Controls.Add(getControl(gridContainer.Text, sEventID, sCaseID, sCompanyID, sPatientID, sChartNO, sTransport, sProcCount, sIsPatient, sReffComp, sRoomID, sRefOfficeID, sTime, sEventDate, sStatus));

                    for (int j = 0; j < rooms.Count; j++)
                    {
                        string isRoomAvailable = rooms[j].ToString();
                        string temp = gridContainer.Column.ToString();
                        if (isRoomAvailable.Contains(temp))
                        {
                            string isRoomAvlbl = (string)gridContainer.Grid.GetRowValues(i, isRoomAvailable);
                            if (isRoomAvlbl != "0" & isRoomAvlbl != null && isRoomAvlbl != "" && isRoomAvlbl != "&nbsp")
                            {
                                container.Controls.Add(getAddAppointmentControl(sReffComp, isRoomAvlbl, sRefOfficeID, sTime, sEventDate, sTestFacility));
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < rooms.Count; j++)
                    {
                        string isRoomAvailable = rooms[j].ToString();
                        string temp = gridContainer.Column.ToString();
                        if (isRoomAvailable.Contains(temp))
                        {
                            string isRoomAvlbl = (string)gridContainer.Grid.GetRowValues(i, isRoomAvailable);

                            if (isRoomAvlbl != "0" & isRoomAvlbl != null && isRoomAvlbl != "" && isRoomAvlbl != "&nbsp")
                            {
                                container.Controls.Add(getAddControl(sReffComp, isRoomAvlbl, sRefOfficeID, sTime, sEventDate, sTestFacility));
                            }
                        }
                    }

                }
            }
        }
    }

    private string cleanTime(string p_Time)
    {
        if (p_Time.ToLower().Contains("span"))
        {
            p_Time = p_Time.Replace("<span style='color:white'>", "");
            p_Time = p_Time.Replace("</span>", "");
        }

        return p_Time;
    }

    private Table getControl(string sPatientName, string sEventID, string sCaseID, string sCompanyID, string sPatientID, string sChartNO, string sTransport, string sProcCount, string sIsPatient, string sReffComp, string sRoomID, string sRefOfficeID, string sTime, string sEventDate, string sStatus)
    {
        Table t = new Table();
        TableRow row = new TableRow();


        if (sIsPatient == "1")
        {
            Image imgColor = new Image();

            HyperLink lnkPatient = new HyperLink();
            string linkText = sPatientName;
            if (sChartNO != "0" && sChartNO != "")
            {
                linkText += "(" + sChartNO + ")";
            }
            if (sTransport != "")
            {
                linkText += sTransport;
            }
            if (sProcCount != "")
            {
                linkText += sProcCount;
            }
            lnkPatient.Text = linkText;
            switch (sStatus)
            {
                case "1":
                    imgColor.ImageUrl = "~/AJAX Pages/Images/gray.jpg";
                    break;
                case "2":
                    imgColor.ImageUrl = "~/AJAX Pages/Images/green.jpg";
                    break;
                case "3":
                    imgColor.ImageUrl = "~/AJAX Pages/Images/red.jpg";
                    break;
            }
            lnkPatient.NavigateUrl = "javascript:OnAppointmentClick('" + sEventID + "','" + sCaseID + "','" + sCompanyID + "','" + sPatientID + "','" + sReffComp + "','" + sRoomID + "','View','" + sRefOfficeID + "','" + cleanTime(sTime) + "','" + sEventDate + "')";
            TableCell cell0 = new TableCell();
            cell0.Controls.Add(imgColor);
            TableCell cell1 = new TableCell();
            cell1.Controls.Add(lnkPatient);
            HyperLink link = new HyperLink();
            link.Text = "Delete";
            link.CssClass = "hldel";
            link.ForeColor = System.Drawing.Color.Red;
            link.NavigateUrl = "javascript:deleteAppointment('" + sPatientName + "')";
            LinkButton delLink = new LinkButton();
            delLink.Text = "Delete";
            delLink.ForeColor = System.Drawing.Color.Red;
            delLink.CssClass = "hldel";
            delLink.OnClientClick = "javascript:deleteAppointment('" + sPatientName + "','" + sEventID + "')";
            TableCell cell2 = new TableCell();

            cell2.Controls.Add(delLink);

            row.Cells.Add(cell0);
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
           
        }
        else
        {
            TableCell cell1 = new TableCell();
            Label patientX = new Label();
            patientX.Text = "XXXXX-XXXXXX";
            cell1.Controls.Add(patientX);
            row.Cells.Add(cell1);
        }

        t.Rows.Add(row);
        if (sIsPatient == "1")
        {
            TableRow row1 = new TableRow();

            LinkButton remLink = new LinkButton();
            remLink.Text = "Reminder";
            remLink.ForeColor = System.Drawing.Color.Green;
            remLink.CssClass = "hldel";
            remLink.OnClientClick = "javascript:SendReminder('" + sPatientName + "','" + sEventID + "')";
            TableCell cell3 = new TableCell();
            cell3.ColumnSpan = 3;
            cell3.Controls.Add(remLink);
            row1.Cells.Add(cell3);
            t.Rows.Add(row1);
        }
        return t;
    }

    private Table getAddControl(string sReffComp, string sRoomID, string sRefOfficeID, string sTime, string sEventDate, string sTestFacility)
    {
        Table t = new Table();
        TableRow row = new TableRow();

        //string sCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        //string roomID = "";

        ImageButton but = new ImageButton();
        HyperLink lnkAdd = new HyperLink();
        lnkAdd.ImageUrl = "images/appointment.jpg";
        lnkAdd.CssClass = "imgsize";
        //lnkAdd.Text = "New Appointment";
        lnkAdd.NavigateUrl = "javascript:AddAppointmentClick('" + sReffComp + "','" + sRoomID + "','" + sRefOfficeID + "','" + cleanTime(sTime) + "','" + sEventDate + "','" + sTestFacility + "')";
        TableCell cell1 = new TableCell();
        cell1.Controls.Add(lnkAdd);

        row.Cells.Add(cell1);
        t.Rows.Add(row);

        return t;
    }

    private Table getAddAppointmentControl(string sReffComp, string sRoomID, string sRefOfficeID, string sTime, string sEventDate, string sTestFacility)
    {
        Table t = new Table();
        TableRow row = new TableRow();

        ImageButton but = new ImageButton();
        HyperLink lnkAdd = new HyperLink();
        lnkAdd.Text = "Add Appointment";


        lnkAdd.NavigateUrl = "javascript:AddAppointmentClick('" + sReffComp + "','" + sRoomID + "','" + sRefOfficeID + "','" + cleanTime(sTime) + "','" + sEventDate + "','" + sTestFacility + "')";
        TableCell cell1 = new TableCell();
        string sRoll = System.Web.HttpContext.Current.Session["ROLL"].ToString();
        if (sRoll == "referring office")
        {
            lnkAdd.Visible = false;
        }
        cell1.Controls.Add(lnkAdd);
        row.Cells.Add(cell1);
        t.Rows.Add(row);

        return t;
    }

}

