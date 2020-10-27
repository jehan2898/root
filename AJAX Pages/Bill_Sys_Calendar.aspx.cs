using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mbs.provider;
using DevExpress.Web;
using System.Collections;
using log4net;
using System.Configuration;
using System.Data.SqlClient;

public partial class AJAX_Pages_Bill_Sys_Calendar : PageBase
{
    Calendar_DAO objCalendar = null;
    private string szDateColor_NA = "#ff6347";
    private Boolean blnWeekShortNames = true;
    private string szDateColor_TODAY = "#FFFF80";
    private static ILog log = LogManager.GetLogger("AJAX_Pages_Bill_Sys_Calendar");

    protected void Page_Load(object sender, EventArgs e)
    {
        ajAutoName.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        if (!Page.IsPostBack)
        {
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            BindTimeControl();
            BindChangeTimeControl();
            btnDeletVisit.Attributes.Add("onclick", "return DeleteVisit()");
            btnTransportdelete.Attributes.Add("onclick", "return DeleteVisitTrans()");
            btnSave.Attributes.Add("onclick", "return SaveVisit()");
            btnchnagetime.Attributes.Add("onclick", "return  ChangeTime()");
            Session["PRV_MONTH"] = Convert.ToDateTime(DateTime.Today).AddMonths(-1).ToString("MM/dd/yyyy");
            Session["CUR_MONTH"] = Convert.ToDateTime(DateTime.Today).ToString("MM/dd/yyyy");
            Session["NEXT_MONTH"] = Convert.ToDateTime(DateTime.Today).AddMonths(1).ToString("MM/dd/yyyy");
            loadCal();
            txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            dtEdit.Date = DateTime.Now;
            int iYear = DateTime.Today.Year;
            //BindYearDropDown(iYear);
           // ddlYearList.SelectedValue = iYear.ToString();
           // ddlMonthList.SelectedValue = DateTime.Today.Month.ToString();
            lblMed.Visible = false;
            BindData(sender, e);
            lblMess.Visible = false;
        }
        loadCal();
    }

    private void BindData(object sender, EventArgs e)
    {
        txtGetDay.Value = DateTime.Now.ToString("MM/dd/yyy");
        Link1_Click(sender, e);
    }









    private void loadCal()
    {

        //LoadCalendarAccordingToYearAndMonth();
        lblMess.Visible = false;

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
            Panel1.Controls.Clear();
            string strDate = "";
            strDate = Convert.ToDateTime(Session["PRV_MONTH"].ToString()).ToString("MM/dd/yyyy");
            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();


            showCalendar(objCalendar, Convert.ToDateTime(strDate).ToString("MM"));

            strDate = Convert.ToDateTime(Session["CUR_MONTH"].ToString()).ToString("MM/dd/yyyy");
            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();


            showCalendar(objCalendar, Convert.ToDateTime(strDate).ToString("MM"));

            strDate = Convert.ToDateTime(Session["NEXT_MONTH"].ToString()).ToString("MM/dd/yyyy");
            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).AddMonths(1).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

            showCalendar(objCalendar, Convert.ToDateTime(strDate).ToString("MM"));

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

    private void showCalendar(Calendar_DAO objCalendar, string szMonth)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //Modified By BowandBaan for Include Ajax Extension - Modification Starts


        //Response.Write("<table border='1' width='300px'>");

        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("<table border='1' width='80px'>"));
        try
        {
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

            //if (txtGetDay.Value.ToString() != "") { string[] strD = txtGetDay.Value.ToString().Split('_'); ClickMonth = getIntMonth(strD.GetValue(0).ToString()); ClickDate = Convert.ToInt32(strD.GetValue(1).ToString()); }

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
                                string szClickDate = szMonth + "/" + bytDayCounter + "/" + objCalendar.InitialDisplayYear;
                                //objLink.Attributes.Add("onclick", " return CloseModalPopup();");
                                objLink.Text = "" + bytDayCounter;
                                //objLink.Click += new EventHandler(Link1_Click);
                                objLink.Attributes.Add("onclick", "return  CloseModalPopup('" + szClickDate + "');");


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
                                //objLink.Attributes.Add("onclick", "  document.getElementById('ctl00_ContentPlaceHolder1_txtGetDay').value='" + objCalendar.InitialDisplayMonth + "_" + bytDayCounter.ToString() + "'; return true;");

                                string szClickDate = szMonth + "/" + bytDayCounter + "/" + objCalendar.InitialDisplayYear;
                                objLink.Attributes.Add("onclick", " return CloseModalPopup();");

                                // objLink.Attributes.Add("onclick", " return CloseModalPopup();");
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
                                    string szClickDate = szMonth + "/" + bytDayCounter + "/" + objCalendar.InitialDisplayYear;

                                    objLink.Attributes.Add("onclick", "return  CloseModalPopup('" + szClickDate + "');");
                                    // objLink.Attributes.Add("onclick", "   return true;");
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
                                    // objLink.Click += new EventHandler(Link1_Click);
                                    //objLink.Attributes.Add("onclick", "  document.getElementById('ctl00_ContentPlaceHolder1_txtGetDay').value='" + objCalendar.InitialDisplayMonth + "_" + bytDayCounter.ToString() + "';  return true;");
                                    string szClickDate = szMonth + "/" + bytDayCounter + "/" + objCalendar.InitialDisplayYear;
                                    objLink.Attributes.Add("onclick", " return CloseModalPopup();");

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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMed.Visible = true;
            lblMed.Text = " Visits of Date: " + dtEdit.Date.ToString("MM/dd/yyyy");
            Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
            DataSet dsVisits = new DataSet();
            dsVisits = objGetVisits.GetCaledarVisits(dtEdit.Date.ToString("MM/dd/yyyy"), txtCompanyId.Text, extddlSpeciality.Text, extddlDoctor.Text, txtPatientName.Text);
            grdVisits.DataSource = dsVisits;
            grdVisits.DataBind();
            ViewState["griddata"] = dsVisits;
            
            carTabPage.ActiveTabIndex = 0;
            loadCal();
            clear();

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

    protected void btnLoadCalendar_Click(object sender, EventArgs e)
    {
        //lblMed.Visible = false;
        //DateTime dtTemp = new DateTime(Convert.ToInt32(ddlYearList.SelectedValue), Convert.ToInt32(ddlMonthList.SelectedValue), 1);

        //Session["PRV_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(-1).ToString("MM/dd/yyyy");
        //Session["CUR_MONTH"] = Convert.ToDateTime(dtTemp).ToString("MM/dd/yyyy");
        //Session["NEXT_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(1).ToString("MM/dd/yyyy");
        //LoadCalendarAccordingToYearAndMonth();

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
           /// ddlYearList.Items.Clear();
            //for (int i = p_iYear; i > p_iYear - 20; i--)
           // for (int i = p_iYear - 10; i <= (p_iYear + 1); i++)
               // ddlYearList.Items.Add(i.ToString());
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("~/Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);

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

    protected void btnDeletVisit_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string szListOfProcedureCode = "";
            for (int i = 0; i < grdVisits.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdVisits.Columns[18]; // checkbox column
                CheckBox chkSelected = (CheckBox)grdVisits.FindRowCellTemplateControl(i, c, "chkSelect");
                if (chkSelected.Checked == true)
                {
                    string iEventID = grdVisits.GetRowValues(i, "I_EVENT_ID").ToString();
                    Bill_Sys_DeleteBO _deleteOpeation = new Bill_Sys_DeleteBO();
                    if (!_deleteOpeation.deleteRecord("SP_TXN_CALENDAR_EVENT", "@I_EVENT_ID", iEventID))
                    {
                        if (szListOfProcedureCode == "")
                        {
                            // Org  -- szListOfProcedureCode = gridTabInfo.Items[i].Cells[1].Text + "-" + gridTabInfo.Items[i].Cells[3].Text;
                            szListOfProcedureCode = grdVisits.GetRowValues(i, "DT_EVENT_DATE").ToString();
                        }
                        else
                        {
                            // Org  -- szListOfProcedureCode = szListOfProcedureCode + " , " + gridTabInfo.Items[i].Cells[1].Text + "-" + gridTabInfo.Items[i].Cells[3].Text;
                            szListOfProcedureCode = szListOfProcedureCode + " , " + grdVisits.GetRowValues(i, "DT_EVENT_DATE").ToString();
                        }
                    }
                }
            }
            Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
            DataSet dsVisits = new DataSet();
            dsVisits = objGetVisits.GetCaledarVisits(txtGetDay.Value, txtCompanyId.Text, extddlSpeciality.Text, extddlDoctor.Text, txtPatientName.Text);
            grdVisits.DataSource = dsVisits;
            grdVisits.DataBind();
            ViewState["griddata"] = dsVisits;
            if (szListOfProcedureCode != "")
            {
                usrMessage.PutMessage("Error  visits not deleted for " + szListOfProcedureCode + " event dates");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
            else
            {
                usrMessage.PutMessage("Visit Delete Sucessfully ...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            //LoadCalendarAccordingToYearAndMonth();
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

    protected void carTabPage_ActiveTabChanged(object source, DevExpress.Web.TabControlEventArgs e)
    {
        //  loadCal();
        string szcaseid = "";
        int iActiveIndex = carTabPage.ActiveTabIndex;
        if (iActiveIndex == 1)
        {

            Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
            DataSet dsVisits = new DataSet();
            dsVisits = objGetVisits.GetCaledarVisits(txtGetDay.Value, txtCompanyId.Text, extddlSpeciality.Text, extddlDoctor.Text, txtPatientName.Text);
            if (dsVisits.Tables.Count > 0)
            {
                if (dsVisits.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsVisits.Tables[0].Rows.Count; i++)
                    {
                        if (szcaseid == "")
                        {
                            szcaseid = "'" + dsVisits.Tables[0].Rows[i]["SZ_CASE_ID"].ToString() + "'";
                        }
                        else
                        {
                            if (dsVisits.Tables[0].Rows[i]["SZ_CASE_ID"].ToString() != "")
                            {
                                szcaseid = szcaseid + ",'" + dsVisits.Tables[0].Rows[i]["SZ_CASE_ID"].ToString() + "'";
                            }
                        }

                    }
                    Bill_Sys_Event_BO objShowVisit = new Bill_Sys_Event_BO();
                    DataSet dsTransportVisits = new DataSet();
                    dsTransportVisits = objShowVisit.GetTransportVisits(txtCompanyId.Text, szcaseid);
                    if (dsTransportVisits.Tables.Count > 0)
                    {
                        if (dsTransportVisits.Tables[0].Rows.Count > 0)
                        {
                            grdTransportVisits.DataSource = dsTransportVisits;
                            grdTransportVisits.DataBind();
                        }
                    }
                }
            }
        }
        else if (iActiveIndex == 2)
        {
            if (txtDate.Text != null)
            {
                string Date;

                if (txtDate.Text != "")
                {
                    Date = txtDate.Text;
                }
                else
                {
                    Date = DateTime.Now.Date.ToString("MM/dd/yyyy");
                }

                txtDate.Text = Date;
                DayCalender(Convert.ToDateTime(txtDate.Text));
                carTabPage.ActiveTabIndex = 2;
            }
            else
            {
                DayCalender(DateTime.Now.Date);
                txtDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
            }
        }
        loadCal();
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMed.Visible = true;
            lblMed.Text = " Visits of Date: " + txtGetDay.Value;
            Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
            DataSet dsVisits = new DataSet();
            dsVisits = objGetVisits.GetCaledarVisits(txtGetDay.Value, txtCompanyId.Text, extddlSpeciality.Text, extddlDoctor.Text, txtPatientName.Text);
            grdVisits.DataSource = dsVisits;
            grdVisits.DataBind();
            ViewState["griddata"] = dsVisits;
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

        
        loadCal();
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int iFlag = 0;
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        Bill_Sys_Event_BO _Bill_Sys_Event_BO = new Bill_Sys_Event_BO();
        ArrayList objAdd;
        try
        {
            Boolean _valid = true;
            //if (ddlStatus.SelectedValue == "1")
            //{
            //    if (txtReScheduleDate.Text == "" && ddlReSchHours.SelectedValue == "00")
            //    {
            //        lblMessage.Text = "Please enter Re-Schedule Date and Time";
            //        _valid = false;
            //    }
            //}
            if (_valid == true)
            {

                for (int i = 0; i < grdVisits.VisibleRowCount; i++)
                {
                    GridViewDataColumn c = (GridViewDataColumn)grdVisits.Columns[18]; // checkbox column
                    CheckBox chkSelected = (CheckBox)grdVisits.FindRowCellTemplateControl(i, c, "chkSelect");
                    string iEventID = "";
                    string szcaseID = "";
                    string szDoctorID = "";
                    string szhave_login = "";
                    string szgroupcode = "";
                    if (chkSelected.Checked == true)
                    {
                        iEventID = grdVisits.GetRowValues(i, "I_EVENT_ID").ToString();
                        szcaseID = grdVisits.GetRowValues(i, "SZ_CASE_ID").ToString();
                        szDoctorID = grdVisits.GetRowValues(i, "SZ_DOCTOR_ID").ToString();
                        szhave_login = grdVisits.GetRowValues(i, "IS_HAVE_LOGIN").ToString();
                        szgroupcode = grdVisits.GetRowValues(i, "GROUP_CODE").ToString();
                        if (szhave_login == "1")
                        {
                            if (ddlStatus.SelectedValue == "1")
                            {
                                int endMin = Convert.ToInt32(ddlReSchMinutes.SelectedValue) + Convert.ToInt32("30");
                                int endHr = Convert.ToInt32(ddlReSchHours.SelectedValue);
                                string endTime = ddlReSchTime.SelectedValue;
                                if (endMin >= 60)
                                {
                                    endMin = endMin - 60;
                                    endHr = endHr + 1;
                                    if (endHr > 12)
                                    {
                                        endHr = endHr - 12;
                                        if (ddlReSchHours.SelectedValue != "12")
                                        {
                                            if (endTime == "AM")
                                            {
                                                endTime = "PM";
                                            }
                                            else if (endTime == "PM")
                                            {
                                                endTime = "AM";
                                            }
                                        }
                                    }
                                    else if (endHr == 12)
                                    {
                                        if (ddlReSchHours.SelectedValue != "12")
                                        {
                                            if (endTime == "AM")
                                            {
                                                endTime = "PM";
                                            }
                                            else if (endTime == "PM")
                                            {
                                                endTime = "AM";
                                            }
                                        }
                                    }
                                }

                                _Bill_Sys_Event_BO.UpdateRescheduledoctorvisits(iEventID, txtNotes.Text, txtReScheduleDate.Text, szgroupcode, ddlReSchHours.SelectedValue.ToString() + "." + ddlReSchMinutes.SelectedValue.ToString(), ddlReSchTime.SelectedValue, endHr.ToString().PadLeft(2, '0').ToString() + "." + endMin.ToString().PadLeft(2, '0').ToString(), endTime);

                            }
                            else if (ddlStatus.SelectedValue == "3")
                            {
                                Bill_Sys_Calender _bill_Sys_Calender1 = new Bill_Sys_Calender();
                                ArrayList objAdd1 = new ArrayList();
                                objAdd1.Add(iEventID);
                                objAdd1.Add(false);
                                objAdd1.Add(ddlStatus.SelectedValue);
                                _bill_Sys_Calender1.UPDATE_Event_Status(objAdd1);

                            }

                            else
                            {
                                iFlag = 1;

                            }
                        }
                        else
                        {
                            if (ddlStatus.SelectedValue == "1")
                            {
                                objAdd = new ArrayList();
                                objAdd.Add(szcaseID);
                                objAdd.Add(txtReScheduleDate.Text);
                                objAdd.Add(ddlReSchHours.SelectedValue.ToString() + "." + ddlReSchMinutes.SelectedValue.ToString());
                                objAdd.Add(txtNotes.Text);
                                objAdd.Add(szDoctorID);
                                objAdd.Add("TY000000000000000003");
                                objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                objAdd.Add(ddlReSchTime.SelectedValue);
                                int endMin = Convert.ToInt32(ddlReSchMinutes.SelectedValue) + Convert.ToInt32("30");
                                int endHr = Convert.ToInt32(ddlReSchHours.SelectedValue);
                                string endTime = ddlReSchTime.SelectedValue;
                                if (endMin >= 60)
                                {
                                    endMin = endMin - 60;
                                    endHr = endHr + 1;
                                    if (endHr > 12)
                                    {
                                        endHr = endHr - 12;
                                        if (ddlReSchHours.SelectedValue != "12")
                                        {
                                            if (endTime == "AM")
                                            {
                                                endTime = "PM";
                                            }
                                            else if (endTime == "PM")
                                            {
                                                endTime = "AM";
                                            }
                                        }
                                    }
                                    else if (endHr == 12)
                                    {
                                        if (ddlReSchHours.SelectedValue != "12")
                                        {
                                            if (endTime == "AM")
                                            {
                                                endTime = "PM";
                                            }
                                            else if (endTime == "PM")
                                            {
                                                endTime = "AM";
                                            }
                                        }
                                    }
                                }
                                objAdd.Add(endHr.ToString().PadLeft(2, '0').ToString() + "." + endMin.ToString().PadLeft(2, '0').ToString());
                                objAdd.Add(endTime.ToString());
                                _bill_Sys_Calender.SaveEvent(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                            }


                            _bill_Sys_Calender = new Bill_Sys_Calender();
                            objAdd = new ArrayList();
                            objAdd.Add(iEventID);
                            objAdd.Add(false);
                            objAdd.Add(ddlStatus.SelectedValue);
                            _bill_Sys_Calender.UPDATE_Event_Status(objAdd);




                        }

                    }

                }



                lblMessage.Text = "";
                usrMessage.PutMessage("Save Sucessfully ...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();

                Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
                DataSet dsVisits = new DataSet();
                dsVisits = objGetVisits.GetCaledarVisits(txtGetDay.Value, txtCompanyId.Text, extddlSpeciality.Text, extddlDoctor.Text, txtPatientName.Text);
                grdVisits.DataSource = dsVisits;
                grdVisits.DataBind();
                ViewState["griddata"] = dsVisits;
                clear();




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
        loadCal();

        if (iFlag == 1)
        {

            lblMess.Visible = true;

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindTimeControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i <= 12; i++)
            {
                if (i > 9)
                {
                    ddlReSchHours.Items.Add(i.ToString());

                }
                else
                {
                    ddlReSchHours.Items.Add("0" + i.ToString());

                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9)
                {
                    ddlReSchMinutes.Items.Add(i.ToString());

                }
                else
                {
                    ddlReSchMinutes.Items.Add("0" + i.ToString());

                }
            }
            ddlReSchTime.Items.Add("AM");
            ddlReSchTime.Items.Add("PM");



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

    public void clear()
    {
        txtNotes.Text = "";
        ddlStatus.SelectedIndex = 0;
        ddlchangeReSchHours.SelectedIndex = 0;
        ddlchangeReSchMinutes.SelectedIndex = 0;
        ddlchangeReSchTime.SelectedIndex = 0;
        extddlSpeciality.Text = "NA";
        extddlDoctor.Text = "NA";
        txtPatientName.Text = "";
    }

    protected void btnTransportdelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrtransport = new ArrayList();
            for (int i = 0; i < grdTransportVisits.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdTransportVisits.Columns[10]; // checkbox column
                CheckBox chktransport = (CheckBox)grdTransportVisits.FindRowCellTemplateControl(i, c, "chkSelect");
                if (chktransport.Checked == true)
                {
                    string strtransportid = grdTransportVisits.GetRowValues(i, "I_TRANS_ID").ToString();
                    arrtransport.Add(strtransportid);

                }
            }
            BillSearchDAO _objBillSearchDAO = new BillSearchDAO();
            _objBillSearchDAO.Delete_Trans_Data(arrtransport, txtCompanyId.Text);
            Link1_Click(sender, e);
            loadTrans();
            carTabPage.ActiveTabIndex = 1;

            usrMessage.PutMessage("Delete Successfully ...!");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
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

    protected void loadTrans()
    {
        string szcaseid = "";
        Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
        DataSet dsVisits = new DataSet();
        dsVisits = objGetVisits.GetCaledarVisits(txtGetDay.Value, txtCompanyId.Text, extddlSpeciality.Text, extddlDoctor.Text, txtPatientName.Text);
        if (dsVisits.Tables.Count > 0)
        {
            if (dsVisits.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsVisits.Tables[0].Rows.Count; i++)
                {
                    if (szcaseid == "")
                    {
                        szcaseid = "'" + dsVisits.Tables[0].Rows[i]["SZ_CASE_ID"].ToString() + "'";
                    }
                    else
                    {
                        if (dsVisits.Tables[0].Rows[i]["SZ_CASE_ID"].ToString() != "")
                        {
                            szcaseid = szcaseid + ",'" + dsVisits.Tables[0].Rows[i]["SZ_CASE_ID"].ToString() + "'";
                        }
                    }

                }
                Bill_Sys_Event_BO objShowVisit = new Bill_Sys_Event_BO();
                DataSet dsTransportVisits = new DataSet();
                dsTransportVisits = objShowVisit.GetTransportVisits(txtCompanyId.Text, szcaseid);

                grdTransportVisits.DataSource = dsTransportVisits;
                grdTransportVisits.DataBind();

            }
        }



        loadCal();
    }

    protected void btnchnagetime_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string iEventID = "";
        try
        {

            for (int i = 0; i < grdVisits.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdVisits.Columns[17]; // checkbox column
                CheckBox chkSelected = (CheckBox)grdVisits.FindRowCellTemplateControl(i, c, "chkSelect");
                if (chkSelected.Checked == true)
                {
                    iEventID = grdVisits.GetRowValues(i, "I_EVENT_ID").ToString();
                    Bill_Sys_Event_BO _Bill_Sys_Event_BO = new Bill_Sys_Event_BO();
                    ArrayList arradd = new ArrayList();
                    int endMin = Convert.ToInt32(ddlchangeReSchMinutes.SelectedValue) + Convert.ToInt32("30");
                    int endHr = Convert.ToInt32(ddlchangeReSchHours.SelectedValue);
                    string endTime = ddlchangeReSchTime.SelectedValue;
                    if (endMin >= 60)
                    {
                        endMin = endMin - 60;
                        endHr = endHr + 1;
                        if (endHr > 12)
                        {
                            endHr = endHr - 12;
                            if (ddlchangeReSchHours.SelectedValue != "12")
                            {
                                if (endTime == "AM")
                                {
                                    endTime = "PM";
                                }
                                else if (endTime == "PM")
                                {
                                    endTime = "AM";
                                }
                            }
                        }
                        else if (endHr == 12)
                        {
                            if (ddlchangeReSchHours.SelectedValue != "12")
                            {
                                if (endTime == "AM")
                                {
                                    endTime = "PM";
                                }
                                else if (endTime == "PM")
                                {
                                    endTime = "AM";
                                }
                            }
                        }
                    }
                    Bill_Sys_Event_DAO _Bill_Sys_Event_DAO = new Bill_Sys_Event_DAO();
                    _Bill_Sys_Event_DAO.I_EVENT_ID = iEventID;
                    _Bill_Sys_Event_DAO.SZ_COMPANY_ID = txtCompanyId.Text;
                    _Bill_Sys_Event_DAO.DT_EVENT_TIME = ddlchangeReSchHours.SelectedValue.ToString() + "." + ddlchangeReSchMinutes.SelectedValue.ToString();
                    _Bill_Sys_Event_DAO.DT_EVENT_TIME_TYPE = ddlchangeReSchTime.SelectedValue;
                    _Bill_Sys_Event_DAO.DT_EVENT_END_TIME = endHr.ToString().PadLeft(2, '0').ToString() + "." + endMin.ToString().PadLeft(2, '0').ToString();
                    _Bill_Sys_Event_DAO.DT_EVENT_END_TIME_TYPE = endTime.ToString();
                    arradd.Add(_Bill_Sys_Event_DAO);
                    _Bill_Sys_Event_BO.UpdateVisitTime(arradd);

                }
            }
            lblMessage.Text = "";
            usrMessage.PutMessage("Save Sucessfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

            Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
            DataSet dsVisits = new DataSet();
            dsVisits = objGetVisits.GetCaledarVisits(txtGetDay.Value, txtCompanyId.Text, extddlSpeciality.Text, extddlDoctor.Text, txtPatientName.Text);
            grdVisits.DataSource = dsVisits;
            grdVisits.DataBind();
            ViewState["griddata"] = dsVisits;
            clear();
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

    private void BindChangeTimeControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i <= 12; i++)
            {
                if (i > 9)
                {
                    ddlchangeReSchHours.Items.Add(i.ToString());

                }
                else
                {
                    ddlchangeReSchHours.Items.Add("0" + i.ToString());

                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9)
                {
                    ddlchangeReSchMinutes.Items.Add(i.ToString());

                }
                else
                {
                    ddlchangeReSchMinutes.Items.Add("0" + i.ToString());

                }
            }
            ddlchangeReSchTime.Items.Add("AM");
            ddlchangeReSchTime.Items.Add("PM");



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

    protected void btnSearch_Onclick(object sender, EventArgs e)
    {
        Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
        DataSet dsVisits = new DataSet();
        dsVisits = objGetVisits.GetCaledarVisits(txtGetDay.Value, txtCompanyId.Text, extddlSpeciality.Text, extddlDoctor.Text, txtPatientName.Text);
        grdVisits.DataSource = dsVisits;
        grdVisits.DataBind();
        ViewState["griddata"] = dsVisits;
        carTabPage.ActiveTabIndex = 0;

    }

    // New Methods

    private void DayCalender(DateTime _date)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_Calender _bill_Sys_Calender;
            ArrayList obj;
            string szEventDate = _date.ToString("MM/dd/yyyy");
            SchedulingBO _objDS = new SchedulingBO();
            ArrayList obj2 = new ArrayList();
            obj2.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            obj2.Add(szEventDate);
            DataSet DSRecords = _objDS.getSpecialityCountForDay(obj2);
            string topLabel = "<table id='Calendar2' cellspacing='0' cellpadding='2' " +
            " border='1' style='width: 100%;font-size: 9pt;" +
            "font-family: Verdana; color: Black; border-width: 1px; border-style: Solid; border-color: Silver;" +
            "background-color: White; border-collapse: collapse;'>";
            string innerTabel = topLabel;
            _bill_Sys_Calender = new Bill_Sys_Calender();
            obj = new ArrayList();
            obj.Add("Appointment Scheduler Start Time");
            obj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            string strStartTime = _bill_Sys_Calender.GetSystemSettingValue(obj);
            decimal startTime = Convert.ToDecimal(strStartTime.Substring(0, (strStartTime.Length - 2)).Replace(":", "."));
            if (strStartTime.Substring((strStartTime.Length - 2), 2) == "PM")
            {
                startTime = startTime + Convert.ToDecimal(12.00);
            }
            obj = new ArrayList();
            obj.Add("Appointment Scheduler End Time");
            obj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            string strendTime = _bill_Sys_Calender.GetSystemSettingValue(obj);
            decimal endTime = Convert.ToDecimal(strendTime.Substring(0, (strendTime.Length - 2)).Replace(":", "."));
            if (strendTime.Substring((strendTime.Length - 2), 2) == "PM")
            {
                endTime = endTime + Convert.ToDecimal(12.00);
            }
            DateTime currentDate = _date;
            currentDate = currentDate.AddHours((Double)(Math.Truncate(startTime)));
            currentDate = currentDate.AddMinutes((Double)((startTime - (Math.Truncate(startTime))) * 100));
            DateTime currentStratDate = currentDate;
            currentDate = _date;
            currentDate = currentDate.AddHours((Double)(Math.Truncate(endTime)));
            currentDate = currentDate.AddMinutes((Double)((endTime - (Math.Truncate(endTime))) * 100));
            DateTime currentEndDate = currentDate;
            int iCount = 0;
            for (decimal iday = startTime; iday < endTime; iday = iday + Convert.ToDecimal(ddlInterval.SelectedItem.Value))
            {
                iCount++;
                decimal remPart = iday - Math.Truncate(iday);
                if (remPart >= (decimal)0.60)
                {

                    iday = Math.Truncate(iday) + (1 + ((decimal)0.60 - remPart));
                }
                _bill_Sys_Calender = new Bill_Sys_Calender();
                obj = new ArrayList();
                obj.Add(txtCaseID.Text);
                obj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                obj.Add(iday);
                decimal endiDay = iday + Convert.ToDecimal(ddlInterval.SelectedItem.Value);
                decimal endremPart = endiDay - Math.Truncate(endiDay);
                if (endremPart >= (decimal)0.60)
                {

                    endiDay = Math.Truncate(endiDay) + (1 + ((decimal)0.60 - endremPart));
                }
                obj.Add(endiDay);
                obj.Add(_date);
                //if (iday <= 12)
                //{
                //    if (iday == 1)
                //    {
                //        obj.Add(0.00);
                //        obj.Add(iday);
                //    }
                //    else
                //    {
                //        obj.Add(iday);
                //        obj.Add(iday + Convert.ToDecimal(ddlInterval.SelectedItem.Value));
                //    }
                //    obj.Add("AM");
                //}
                //else if (iday > 12)
                //{
                //    if (iday == 13)
                //    {
                //        obj.Add(0);
                //        obj.Add(iday - 12);
                //    }
                //    else
                //    {
                //        obj.Add(iday - 13);
                //        obj.Add(iday - 12);
                //    }

                //    obj.Add("PM");
                //}

                DataTable daySheduledt = _bill_Sys_Calender.GET_DAY_SHEDULED_EVENT(obj);
                string strFirstCol = "";
                //if (iday <= 12)
                //{
                //    if (iday == 1)
                //    {
                //        strFirstCol = Convert.ToString("00 AM To " + iday + " AM");
                //    }
                //    else
                //    {
                //        strFirstCol = Convert.ToString((iday - Convert.ToDecimal(ddlInterval.SelectedItem.Value)) + " AM To " + iday + " AM");
                //    }

                //}
                //else
                //{
                //    if (iday == 13)
                //    {
                //        strFirstCol = Convert.ToString("00 PM To " + (iday - 12) + " PM");
                //    }
                //    else
                //    {

                //        strFirstCol = Convert.ToString((iday - 13) + " PM To " + (iday - 12) + " PM");
                //    }
                //}
                if (iday >= 13)
                {
                    strFirstCol = Convert.ToString((iday - 12) + " PM");
                }
                else if (iday == 12 || (iday < 13 && iday > 12))
                {
                    strFirstCol = Convert.ToString((iday) + " PM");
                }
                else
                {

                    strFirstCol = Convert.ToString((iday) + " AM");
                }
                //strFirstCol = iday.ToString();

                innerTabel = innerTabel + "<tr>" +
                              "<td align='left' style='width:20%;border-width: 1px; border-style: solid;background-color:#CCCCCC;" +
                              "font-size: 8pt; font-weight: bold;color:steelblue;'>  <a href='#' onclick=\"GetTime('" + strFirstCol + "')\" >" + strFirstCol + "</a></td>";

                //style='width:20%;border-width: 1px; border-style: solid;background-color:#CCCCCC;" + "font-size: 8pt; font-weight: bold;color:steelblue;'
                //<asp:LinkButton ID="lnkTime" OnClick="lnkTime_OnClick" runat="server">LinkButton</asp:LinkButton>
                //<a id='btnTimelink' href='Bill_Sys_Calendar.aspx?Time=" + strFirstCol + "&EventDate=" + txtDate.Text + "'; \" >" + strFirstCol + "</a>

                //+ "<a href='#' onclick=\'Timelink_OnClick()'\" style='color: Black' title='" + getMonth(_date.Month) + " " + _date.Day.ToString() + "'>" +
                //    strFirstCol + "</a>" + " </td>";

                //+"<a href='#' onclick=\" var _time='" + strFirstCol + "';  var _date = new Date(); _date='" + _date + "'; setDiv(_date,_time);\" style='color: Black' title='" + getMonth(_date.Month) + " " + _date.Day.ToString() + "'>" +
                //        strFirstCol + "</a>" + " </td>";
                // +

                //"<td align='left'  style='border-width: 1px; border-style: solid;background-color:#CCCCCC;" +
                //"   font-size: 8pt; font-weight: bold;color:whitesmoke;'  ></td>" +
                //"<td align='left'  style='border-width: 1px; border-style: solid;background-color:#CCCCCC;" +
                //"   font-size: 8pt; font-weight: bold;color:whitesmoke;'  > </td>" +
                //"<td align='left'  style='border-width: 1px; border-style: solid;background-color:#CCCCCC;" +
                //"   font-size: 8pt; font-weight: bold;color:whitesmoke;'  ></td>" +
                //"<td align='left'  style='border-width: 1px; border-style: solid;background-color:#CCCCCC;" +
                //"   font-size: 8pt; font-weight: bold;color:whitesmoke;'  ></td>" +
                // "</tr>";
                string strLblHeading = "";
                //for (int i = 0; i < daySheduledt.Rows.Count; i++)
                //{
                //+ daySheduledt.Rows[i].ItemArray.GetValue(3).ToString() + " "
                //strLblHeading = strLblHeading + daySheduledt.Rows[i].ItemArray.GetValue(1).ToString() + " "
                //+ "<a href='#' style='font-size:11px;' onclick=\" document.getElementById('divid').style.position = 'absolute'; document.getElementById('divid').style.left= '350px'; document.getElementById('divid').style.top= '250px';document.getElementById('divid').style.zIndex= '1';  document.getElementById('divid').style.visibility='visible'; document.getElementById('frameeditexpanse').src='ViwScheduled.aspx?id=" + daySheduledt.Rows[i].ItemArray.GetValue(0).ToString() + "'; \" >" + daySheduledt.Rows[i].ItemArray.GetValue(4).ToString() + daySheduledt.Rows[i].ItemArray.GetValue(6).ToString() + daySheduledt.Rows[i].ItemArray.GetValue(7).ToString() + "</a> " + "<br/>";
                //+ daySheduledt.Rows[i].ItemArray.GetValue(2).ToString() + "<br/>";
                //innerTabel = innerTabel + //"<tr>" +
                //    //     "<td align='center'  style='border-width: 1px;height: 20px; border-style: solid;background-color:#CCCCCC;" +
                //    //"font-size: 8pt; font-weight: bold;color:white;'></td>" +
                //            "<td align='center'  style='border-width: 1px;height: 20px; border-style: solid;background-color:#CCCCCC;" +
                //           "font-size: 8pt; font-weight: bold;color:white;'>" + daySheduledt.Rows[i].ItemArray.GetValue(1).ToString() + " </td>" +
                //           "<td align='center'  style='border-width: 1px;height: 20px; border-style: solid;background-color:#CCCCCC;" +
                //           "font-size: 8pt; font-weight: bold;color:white;'>" + daySheduledt.Rows[i].ItemArray.GetValue(3).ToString() + " </td>" +
                //           "<td align='center'  style='border-width: 1px;height: 20px; border-style: solid;background-color:#CCCCCC;" +
                //           "font-size: 8pt; font-weight: bold;color:white;'><a href='#' style='font-size:8px;' onclick=\" document.getElementById('divid').style.position = 'absolute'; document.getElementById('divid').style.left= '350px'; document.getElementById('divid').style.top= '250px'; document.getElementById('divid').style.visibility='visible'; document.getElementById('frameeditexpanse').src='ViwScheduled.aspx?id=" + daySheduledt.Rows[i].ItemArray.GetValue(0).ToString() + "'; \" >" + daySheduledt.Rows[i].ItemArray.GetValue(4).ToString() + "</a> </td>" +


                //           "<td align='center'  style='border-width: 1px;height: 20px; border-style: solid;background-color:#CCCCCC;" +
                //           "font-size: 8pt; font-weight: bold;color:white;'>" + daySheduledt.Rows[i].ItemArray.GetValue(2).ToString() + " </td>";// +
                // "</tr>";
                //}

                string StringToBind = "";

                for (int i = 0; i < DSRecords.Tables[1].Rows.Count; i++)
                {
                    string FinalString = "";
                    DateTime FirstTime;
                    DateTime LastTime;
                    DateTime CheckTime;
                    ArrayList arrCount = new ArrayList();
                    ArrayList arrRecords = new ArrayList();
                    bool Result;
                    int Counter = 0;
                    //strFirstCol = strFirstCol + ddlInterval.SelectedItem.Value;

                    for (int j = 0; j < DSRecords.Tables[0].Rows.Count; j++)
                    {
                        if (DSRecords.Tables[1].Rows[i]["Speciality"].ToString() == DSRecords.Tables[0].Rows[j]["Speciality"].ToString())
                        {
                            string CheckDate;
                            if (txtDate.Text == "")
                            {
                                CheckDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
                            }
                            else
                            {
                                CheckDate = txtDate.Text;
                            }

                            strFirstCol = strFirstCol.Replace(".", ":");
                            string chekdatetime = CheckDate + " " + strFirstCol;
                            FirstTime = DateTime.Parse(chekdatetime);
                            LastTime = FirstTime.AddMinutes(30);
                            string timeCheck = DSRecords.Tables[0].Rows[j]["event_date"].ToString() + " " + DSRecords.Tables[0].Rows[j]["event_time"].ToString().Replace(".", ":") + " " + DSRecords.Tables[0].Rows[j]["DT_EVENT_TIME_TYPE"].ToString();
                            CheckTime = DateTime.Parse(timeCheck);
                            //bool Result = IsTimeOfDayBetween(CheckTime, FirstTime, LastTime);
                            if (((CheckTime > FirstTime) || (CheckTime == FirstTime)) && (CheckTime < LastTime))
                            {
                                Result = true;
                            }
                            else
                            {
                                Result = false;
                            }

                            if (Result == true)
                            {
                                Counter += 1;
                            }
                        }
                    }

                    if (Counter > 0)
                    {
                        FinalString = DSRecords.Tables[1].Rows[i]["Speciality"].ToString() + " - " + Counter;

                        if (StringToBind == "")
                        {
                            StringToBind = FinalString;
                        }
                        else
                        {
                            StringToBind = StringToBind + ", " + FinalString;
                        }
                    }
                }
                if (StringToBind == "")
                {
                    //strLblHeading = strLblHeading + "<a href='#' style='font-size:10px;color:Brown;' onclick=\" document.getElementById('divid').style.position = 'absolute'; document.getElementById('divid').style.left= '350px'; document.getElementById('divid').style.top= '250px';document.getElementById('divid').style.zIndex= '1';  document.getElementById('divid').style.visibility='visible'; \" >" + 0 + "</a>";
                    strLblHeading = strLblHeading + "0";
                    innerTabel = innerTabel + "<td align='center'  style='width:80%;border-width: 1px;height: 20px; border-style: solid;background-color:#CCCCCC;" +
                        "font-size: 8pt; font-weight: bold;color:steelblue;'>" + strLblHeading + "</td>" + "</tr>";
                }
                else
                {
                    //strLblHeading = strLblHeading + "<a href='#' style='font-size:10px;color:Brown;' onclick=\" document.getElementById('divid').style.position = 'absolute'; document.getElementById('divid').style.left= '350px'; document.getElementById('divid').style.top= '250px';document.getElementById('divid').style.zIndex= '1';  document.getElementById('divid').style.visibility='visible'; \" >" + StringToBind + "</a>";
                    strLblHeading = strLblHeading + StringToBind;
                    innerTabel = innerTabel + "<td align='center'  style='width:80%;border-width: 1px;height: 20px; border-style: solid;background-color:#CCCCCC;" +
                        "font-size: 8pt; font-weight: bold;color:steelblue;'>" + strLblHeading + "</td>" + "</tr>";
                }
            }

            innerTabel = innerTabel + "</table>";
            //UpdatePanel11.ContentTemplateContainer.Controls.Add(new LiteralControl(innertabel));
            // DivDayView.Controls.Add(new LiteralControl(innerTabel));
            //DivDayView.Controls.Add(new LiteralControl(innerTabel));
            //UpdatePanel3.ContentTemplateContainer.Controls.Add(new LiteralControl(innerTabel));
            DivDayView.InnerHtml = innerTabel;
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

    protected void btnshowall_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        if (txtDate.Text != "")
        {
            try
            {
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(txtDate.Text);
                txtDate.Text = dt.ToString("MM/dd/yyyy");
                DayCalender(dt);
                carTabPage.ActiveTabIndex = 2;

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
        else
        {
            usrMessage.PutMessage("Please Select Date");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    
    protected void GetVisitsByDateAndTime(string Date, string SZ_COMPANY_ID, string Time, string Time_Type, string EndTime, string LastTimeType)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMed.Visible = true;
            lblMed.Text = " Visits of Date: " + Date + " " + Time + " " + Time_Type;
            SchedulingBO objGetVisits = new SchedulingBO();
            DataSet dsVisits = new DataSet();
            dsVisits = objGetVisits.GetCaledarVisitsByTime(Date, SZ_COMPANY_ID, Time, Time_Type.Trim(), EndTime, LastTimeType);
            Session["ExportScheduledVisits"] = dsVisits;
            grdVisits.DataSource = dsVisits;
            grdVisits.DataBind();
            ViewState["griddata"] = dsVisits;
            carTabPage.ActiveTabIndex = 0;
            loadCal();
            clear();

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

    public static string GetLast(string source, int tail_length)
    {
        if (tail_length >= source.Length)
            return source;
        return source.Substring(source.Length - tail_length);
    }

    protected void lnkTime_OnClick(object sender, EventArgs e)
    {
        if (txtGetTime.Value != null)
        {
            string Date;
            string Time_Type = GetLast(txtGetTime.Value, 3);
            string Time = (txtGetTime.Value).Replace(Time_Type, "");

            if (txtDate.Text != "")
            {
                Date = txtDate.Text;
            }
            else
            {
                Date = DateTime.Now.Date.ToString("MM/dd/yyyy");
            }

            string chekdatetime = Date.ToString() + " " + (txtGetTime.Value).Replace(".", ":");
            DateTime tempTime = DateTime.Parse(chekdatetime.Replace(".", ":")).AddMinutes(30);
            string LastTimeType = GetLast(tempTime.ToString(), 2);
            string strRemove = GetLast(tempTime.ToString(), 6);
            string LastTime = (GetLast((tempTime.ToString().Replace(strRemove, "")), 5).Replace(" ", "")).ToString().Replace(":", ".");

            GetVisitsByDateAndTime(Date, txtCompanyId.Text, Time, Time_Type, LastTime, LastTimeType);

            DayCalender(Convert.ToDateTime(Date));
            carTabPage.ActiveTabIndex = 0;
        }
        else
        {
            DayCalender(DateTime.Now.Date);
            txtDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
        }
    }

    protected void ShowAllVisits_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (txtDate.Text != "")
            {
                txtGetDay.Value = txtDate.Text;
                lblMed.Visible = true;
                lblMed.Text = " Visits of Date: " + txtGetDay.Value;
                Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
                DataSet dsVisits = new DataSet();
                dsVisits = objGetVisits.GetCaledarVisits(txtGetDay.Value, txtCompanyId.Text, extddlSpeciality.Text, extddlDoctor.Text, txtPatientName.Text);
                Session["ExportScheduledVisits"] = dsVisits;
                grdVisits.DataSource = dsVisits;
                grdVisits.DataBind();
                ViewState["griddata"] = dsVisits;
                carTabPage.ActiveTabIndex = 0;
                loadCal();
                clear();
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

    protected void btnXlsExport2_Click(object sender, EventArgs e)
    {
        DataSet dtgrdOne = new DataSet();
        dtgrdOne = (DataSet)ViewState["griddata"];
        grdVisits.DataSource = dtgrdOne;
        grdVisits.DataBind();
        

        string sFileName = null;
     
      
       
        sFileName =  "Visits_" + DateTime.Now.ToString("MM_dd_yyyyhhmm") + ".xlsx";
        string sFile = ConfigurationManager.AppSettings["EXCEL_SHEET"] + sFileName;
        grdVisits.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), "All");
        //string sFile = gb.web.utils.DownloadFilesUtils.GetExportPhysicalPath(sUserName, gb.web.utils.DownloadFilesExportTypes.RECONCILIATION_LIST, out sFileName);
        System.IO.Stream stream = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
        grdExport.WriteXlsx(stream);
        stream.Close();
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + sFileName + "');", true);
        //GridViewDataHyperLinkColumn hlnk = (GridViewDataHyperLinkColumn)grdPatientList.FindControl("_self");

        //        GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;


       
        
        
    }
    private static string getUploadDocumentPhysicalPath()
    {
        string str = "";
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlDataReader reader = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", connection).ExecuteReader();
            while (reader.Read())
            {
                str = reader["parametervalue"].ToString();
            }
        }
        catch (SqlException exception)
        {
            exception.Message.ToString();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str;
    }

    protected void dtEdit_DateChanged(object sender, EventArgs e)
    {
        Link1_Click(sender, e);
    }
}