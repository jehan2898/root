using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
public partial class Bill_Sys_WeekCalendarEvents : PageBase
{
    private string szDateColor_NA = "#ff6347";
    private string szDateColor_TODAY = "#FFFF80";
    private Boolean blnWeekShortNames = true;

    Calendar_DAO objCalendar = null;

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
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    extddlReferringFacility.Visible = false;
                    lblReferringFacility.Visible = false;
                }
                extddlReferringFacility.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                callSchedule(DateTime.Today);
            }
            string strDate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");

            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).AddMonths(-1).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).AddMonths(-1).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).AddMonths(-1).Year.ToString();

            showCalendar(objCalendar);


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
            cv.MakeReadOnlyPage("Bill_Sys_WeekCalendarEvents.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Link1_Click(object sender, EventArgs e)
    {
        LinkButton objLink = (LinkButton)sender;
        Label1.Text = "Refreshed at " + DateTime.Now.ToString() + " " + " pressed by " + objLink.CommandArgument;

        string[] szDate = null;
        szDate = objLink.CommandArgument.Split('_');

        string szYear = szDate[0];
        string szMonth = szDate[1];
        string szBogus = szDate[2];
        string szDay = szDate[3];

        DateTime objSource = new DateTime(Int32.Parse(szYear), getIntMonth(szMonth),Int32.Parse(szDay));
        Session["PREVIOUS_DATE"] = objSource;
        int iStartDifference = getStartOfWeekIndex(""+objSource.DayOfWeek);
        DateTime objWeekStart = objSource.AddDays(-1 * iStartDifference);
        
        int iEndDifference = getEndOfWeekIndex("" + objSource.DayOfWeek);
        DateTime objWeekEnd = objSource.AddDays(iEndDifference);

        Label1.Text = "<a href=''>" + objWeekStart.ToString() + " ---- " + objWeekEnd.ToString() + "</a>";
        getWeekSchedule(objWeekStart, objWeekEnd);
    }

    private void callSchedule(DateTime p_szDate)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

        //    Label1.Text = "Refreshed at " + DateTime.Now.ToString() + " " + " pressed by " + objLink.CommandArgument;



        string szYear = p_szDate.Year.ToString();
        string szMonth = p_szDate.Month.ToString();
        string szDay = p_szDate.Day.ToString();

        DateTime objSource = new DateTime(Int32.Parse(szYear), Int32.Parse(szMonth), Int32.Parse(szDay));
            Session["PREVIOUS_DATE"] = objSource;
            int iStartDifference = getStartOfWeekIndex("" + objSource.DayOfWeek);
            DateTime objWeekStart = objSource.AddDays(-1 * iStartDifference);

            int iEndDifference = getEndOfWeekIndex("" + objSource.DayOfWeek);
            DateTime objWeekEnd = objSource.AddDays(iEndDifference);

            Label1.Text = "<a href=''>" + objWeekStart.ToString() + " ---- " + objWeekEnd.ToString() + "</a>";
            getWeekSchedule(objWeekStart, objWeekEnd);
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


    private void getWeekSchedule(DateTime objWeekStart, DateTime objWeekEnd)
    {
        TimeSpan objSpan = objWeekEnd.Subtract(objWeekStart);

        Label1.Text = Label1.Text + " __ " + objSpan.Days;
        DateTime objTemp = objWeekStart;

        int iCounter = 0;
        Label1.Text = "<table width='100%'>";
        ArrayList objDataList = GetDataForWeeklyEvents(objWeekStart, objWeekEnd);
     //   ArrayList objDataList = getWeekScheduleData();

        while (iCounter < 6)
        {
            Label1.Text = Label1.Text + "<tr>";
            Label1.Text = Label1.Text + "<td width='100%'>";
            Label1.Text = Label1.Text + "<div style='background-color:#FFFFD5;border: gray 1px solid;width:100%;height:200px;overflow:auto;'>";
            Label1.Text = Label1.Text + String.Format("{0:dddd, MMMM d, yyyy}", objTemp); //Weekday
            Label1.Text = Label1.Text + "<div style='width:100%;height:20px'>&nbsp;</div>";

            // Data loop goes here
      //      Label1.Text = Label1.Text + objTemp.ToString();
            string[] sTmpArray = null;
            for (int i = 0; i < objDataList.Count; i++)
            {
                Schedule_DAO obj = (Schedule_DAO)objDataList[i];
                sTmpArray = objTemp.ToString("MM/dd/yyyy").Split(' ');
                if(sTmpArray[0].Equals(obj.Date))
                {
                    Label1.Text = Label1.Text + "<p style='background-color: #FFFF66;margin-top:1px;margin-bottom:1px'>";
                    Label1.Text = Label1.Text + obj.Time + "   " + obj.Description;
                    Label1.Text = Label1.Text + "</p>";
                }
            }

            // Data loop ends here
            
            //Label1.Text = Label1.Text + "Nunc aliquet pharetra leo. Nulla facilisi. Proin consectetuer dictum felis. Cras lacinia. Quisque bibendum libero id quam. Etiam mollis euismod ante. Duis vitae est sodales justo cursus ullamcorper. Donec volutpat eros sit amet nibh aliquam malesuada. Morbi nunc. Fusce vestibulum ipsum id massa. Ut a orci. Sed et sapien sit amet tellus faucibus hendrerit. Maecenas bibendum, felis a dictum mollis, tellus tortor blandit odio, et convallis massa lorem at metus. ";
            
            Label1.Text = Label1.Text + "</div>";
            Label1.Text = Label1.Text + "</td>";
            Label1.Text = Label1.Text + "</tr>";
            objTemp = objTemp.AddDays(1);
            iCounter++;
        }
        Label1.Text = Label1.Text + "</table>";
       // return "";
    }

    //private ArrayList getWeekScheduleData()
    //{
    //    ArrayList objList = new ArrayList();
    //    Schedule_DAO objDAO = null;
    //    objDAO = new Schedule_DAO("05/05/2009", "Party hangover");
    //    objList.Add(objDAO);

    //    objDAO = new Schedule_DAO("05/05/2009", "Meet Arjun");
    //    objList.Add(objDAO);

    //    objDAO = new Schedule_DAO("05/05/2009", "Server maintenance @ Infosys");
    //    objList.Add(objDAO);

    //    objDAO = new Schedule_DAO("05/05/2009", "Server maintenance @ Infosys");
    //    objList.Add(objDAO);

    //    objDAO = new Schedule_DAO("05/05/2009", "Server maintenance @ Infosys");
    //    objList.Add(objDAO);

    //    objDAO = new Schedule_DAO("05/05/2009", "Server maintenance @ Infosys");
    //    objList.Add(objDAO);

    //    return objList;
    //}

    //string p_szMonth
    private void showCalendar(Calendar_DAO objCalendar)
    {
        //Response.Write("<table border='1' width='300px'>");

        UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<table border='1' width='80px'>"));

        // start -- fill the long name of the month
        UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<tr><td colspan='7'>"));
        string szLongName = "<div align='center' style='font-size:11px;font-weight:bold'>@LONG_MONTH_NAME@</div>";
        szLongName = szLongName.Replace("@LONG_MONTH_NAME@",getLongMonthName(objCalendar.InitialDisplayMonth));

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
        else{
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

    private byte getTodaysDayOfTheMonth()
    {
        return (byte)DateTime.Now.Day;
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

    private string getFirstDayOfMonth(string p_szMonth, string p_szYear)
    {
        int iMonth = getIntMonth(p_szMonth);
        int iYear = Int32.Parse(p_szYear);
        int iDay = 01;
        DateTime objTDay = new DateTime(iYear, iMonth, iDay);
        return objTDay.DayOfWeek.ToString();
    }

    private int getLastMonthInteger()
    {
        return DateTime.DaysInMonth(Int32.Parse(objCalendar.InitialDisplayYear),getIntMonth(objCalendar.InitialDisplayMonth));
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

    private int getEndOfWeekIndex(string p_szWeekDayName)
    {
        p_szWeekDayName = p_szWeekDayName.ToLower();
        switch (p_szWeekDayName)
        {
            case "monday":
                return 5;
            case "tuesday":
                return 4;
            case "wednesday":
                return 3;
            case "thursday":
                return 2;
            case "friday":
                return 1;
            case "saturday":
                return 0;
            case "sunday":
                return 0;
            default:
                return 0;
        }
        return 0;
    }

    private int getStartOfWeekIndex(string p_szWeekDayName)
    {
        p_szWeekDayName = p_szWeekDayName.ToLower();
        switch (p_szWeekDayName)
        {
            case "monday":
                return 0;
            case "tuesday":
                return 1;
            case "wednesday":
                return 2;
            case "thursday":
                return 3;
            case "friday":
                return 4;
            case "saturday":
                return 5;
            case "sunday":
                return 6;
            default:
                return 0;
        }
        return 0;
    }

    private class Schedule_DAO
    {
        private string szDescription = null;
        private string szDate = null;
        private string szTime = null;

        public Schedule_DAO(string p_szDate, string p_szDescription, string p_szTime)
        {
            this.szDate = p_szDate;
            this.szDescription = p_szDescription;
            this.szTime = p_szTime;
        }
        public string Date
        {
            get
            {
                return szDate;
            }
            set
            {
                szDate = value;
            }
        }

        public string Description
        {
            get
            {
                return szDescription;
            }
            set
            {
                szDescription = value;
            }
        }
        public string Time
        {
            get
            {
                return szTime;
            }
            set
            {
                szTime = value;
            }
        }
    }


    private SqlConnection _sqlCon;
    private SqlCommand _sqlCmd;
    private SqlDataReader _sqlDr;
    private ArrayList _arrayList;
    private ArrayList GetDataForWeeklyEvents(DateTime objStartDateTime,DateTime objEndDateTime)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        _arrayList = new ArrayList();
        try
        {
            _sqlCon.Open();

            _sqlCmd = new SqlCommand("GET_ROOM_DETAILS_WEEK", _sqlCon);
            _sqlCmd.CommandType = CommandType.StoredProcedure;
            _sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", "");
            _sqlCmd.Parameters.AddWithValue("@DT_START_DATE", objStartDateTime);
            _sqlCmd.Parameters.AddWithValue("@DT_END_DATE", objEndDateTime);
            _sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            _sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_ID", extddlReferringFacility.Text);
            _sqlDr = _sqlCmd.ExecuteReader();
            while (_sqlDr.Read())
            {
                Schedule_DAO objDAO = null;
                objDAO = new Schedule_DAO(_sqlDr["DT_EVENT_DATE"].ToString(), _sqlDr["DESCRIPTION"].ToString(), _sqlDr["TIME"].ToString());
                _arrayList.Add(objDAO);
            }
            return _arrayList;
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
            return null;
        }
        
        finally
        {
            if (_sqlCon.State == ConnectionState.Open)
            {
                _sqlCon.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    protected void extddlReferringFacility_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (Session["PREVIOUS_DATE"] != null)
            {
                callSchedule(Convert.ToDateTime(Session["PREVIOUS_DATE"]));
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
}