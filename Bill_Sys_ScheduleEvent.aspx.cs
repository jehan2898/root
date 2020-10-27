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
using System.Xml;
using Componend;

public partial class Bill_Sys_ScheduleEvent : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
                if (Request.QueryString["csid"] != null)
            {
                Session["SZ_CASE_ID"] = Request.QueryString["csid"].ToString();
            }
            if (Request.QueryString["Top"] != null)
            {
                
            }
            else
            {
                if (Request.QueryString["Flag"] != null)
                {
                    txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    hdnCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                }
                else
                {
                    if (Session["SZ_CASE_ID"] != null)
                    {
                        if (Session["SZ_CASE_ID"].ToString() != "")
                        {
                            txtCaseID.Text = Session["SZ_CASE_ID"].ToString();
                            hdnCaseID.Text = Session["SZ_CASE_ID"].ToString();
                        }
                    }
                   
                    if (Request.QueryString["scase"] != null)
                    {
                        if (Request.QueryString["scase"].ToString() != "")
                        {
                            txtCaseID.Text = Request.QueryString["scase"].ToString();
                            hdnCaseID.Text = Request.QueryString["scase"].ToString();
                        }
                    }                    
                }
            }
        
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                if (Request.QueryString["_day"] != null)
                {
                    DisplayCalender(Convert.ToDateTime(Request.QueryString["_day"].ToString()));
                    ddlMonth.SelectedValue = Convert.ToDateTime(Request.QueryString["_day"].ToString()).Month.ToString();
                    ddlYear.SelectedValue = Convert.ToDateTime(Request.QueryString["_day"].ToString()).Year.ToString();
                }
                else if (Request.QueryString["idate"] != null)
                {
                    DisplayCalender(Convert.ToDateTime(Request.QueryString["idate"].ToString()));
                    ddlMonth.SelectedValue = Convert.ToDateTime(Request.QueryString["idate"].ToString()).Month.ToString();
                    ddlYear.SelectedValue = Convert.ToDateTime(Request.QueryString["idate"].ToString()).Year.ToString();
                }
                else // when there is no date supplied
                {
                    DisplayCalender(DateTime.Now.Date);
                    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            
                }
               // DisplayCalender(DateTime.Now.Date);
                GetDoctorList();
                

                if (Request.QueryString["_day"] != null)
                {
                    DayCalender(Convert.ToDateTime(Request.QueryString["_day"].ToString()));
                    tabcontainerPatientVisit.ActiveTabIndex = 1;
                }
                else if (Request.QueryString["idate"] != null)
                {
                    DayCalender(Convert.ToDateTime(Request.QueryString["idate"].ToString()));
                    tabcontainerPatientVisit.ActiveTabIndex = 1;
                }
                else
                {
                    DayCalender(DateTime.Now.Date);
                    tabcontainerPatientVisit.ActiveTabIndex = 0;
                }

            }
            else
            {
                DisplayCalender(Convert.ToDateTime(Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/" + Convert.ToInt16(ddlYear.SelectedValue)));
            }
           
           
           
            //lblMsg.Text = "";
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
            cv.MakeReadOnlyPage("Bill_Sys_ScheduleEvent.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
    //protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DisplayCalender(Convert.ToDateTime(Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/" + Convert.ToInt16(ddlYear.SelectedValue)));
    //}

    //protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DisplayCalender(Convert.ToDateTime(Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/" + Convert.ToInt16(ddlYear.SelectedValue)));
    //}

    protected void imgbtnPrevDate_Click(object sender, EventArgs e)
    {

        DateTime objDateTime = new DateTime(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue), 1);
        objDateTime = objDateTime.AddMonths(-1);

        ddlMonth.SelectedValue = objDateTime.Month.ToString();
        ddlYear.SelectedValue = objDateTime.Year.ToString();
        DisplayCalender(Convert.ToDateTime( Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/" + Convert.ToInt16(ddlYear.SelectedValue)));

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        DateTime objDateTime = new DateTime(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue), 1);
        objDateTime = objDateTime.AddMonths(1);

        ddlMonth.SelectedValue = objDateTime.Month.ToString();
        ddlYear.SelectedValue = objDateTime.Year.ToString();
        DisplayCalender(Convert.ToDateTime( Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/"  + Convert.ToInt16(ddlYear.SelectedValue)));

    }

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
            ArrayList obj ;
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
            if (strStartTime.Substring((strStartTime.Length - 2), 2)=="PM")
            {
                startTime = startTime + Convert.ToDecimal(12.00);
            }
            obj = new ArrayList();
            obj.Add("Appointment Scheduler End Time");
            obj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            string strendTime = _bill_Sys_Calender.GetSystemSettingValue(obj);
            decimal endTime = Convert.ToDecimal(strendTime.Substring(0, (strendTime.Length - 2)).Replace(":", "."));
            if (strendTime.Substring((strendTime.Length - 2), 2)=="PM")
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
            for (decimal iday = startTime; iday < endTime; iday = iday + Convert.ToDecimal(ddlInterval.SelectedItem.Value))
            {
                
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
                //        obj.Add(iday-12);
                //    }
                //    else
                //    {
                //        obj.Add(iday-13);
                //        obj.Add(iday-12);
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

                              "<td align='left'  style='width:20%;border-width: 1px; border-style: solid;background-color:#CCCCCC;" +
                               "font-size: 8pt; font-weight: bold;color:steelblue;'  >" + strFirstCol + " </td>";// +
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
                for (int i = 0; i < daySheduledt.Rows.Count; i++)
                {
                    //+ daySheduledt.Rows[i].ItemArray.GetValue(3).ToString() + " "
                    strLblHeading = strLblHeading + daySheduledt.Rows[i].ItemArray.GetValue(1).ToString() + " "
                                    + "<a href='#' style='font-size:11px;' onclick=\" document.getElementById('divid').style.position = 'absolute'; document.getElementById('divid').style.left= '350px'; document.getElementById('divid').style.top= '250px';document.getElementById('divid').style.zIndex= '1';  document.getElementById('divid').style.visibility='visible'; document.getElementById('frameeditexpanse').src='ViwScheduled.aspx?id=" + daySheduledt.Rows[i].ItemArray.GetValue(0).ToString() + "'; \" >" + daySheduledt.Rows[i].ItemArray.GetValue(4).ToString() + daySheduledt.Rows[i].ItemArray.GetValue(6).ToString() + daySheduledt.Rows[i].ItemArray.GetValue(7).ToString() + "</a> " + "<br/>";
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



                }
                strFirstCol = strFirstCol + ddlInterval.SelectedItem.Value;
                strLblHeading = strLblHeading + "<a href='#' onclick=\" var _time='" + strFirstCol + "';  var _date       = new Date(); _date='" + _date + "'; setDiv(_date,_time);\" style='color: Black' title='" + getMonth(_date.Month) + " " + _date.Day.ToString() + "'>" +
                        "Add" + "</a>";
                innerTabel = innerTabel + "<td align='center'  style='width:80%;border-width: 1px;height: 20px; border-style: solid;background-color:#CCCCCC;" +
                    "font-size: 8pt; font-weight: bold;color:steelblue;'>" + strLblHeading + "</td>" + "</tr>";
            }
            innerTabel = innerTabel + "</table>";
            //UpdatePanel11.ContentTemplateContainer.Controls.Add(new LiteralControl(innertabel));
           // DivDayView.Controls.Add(new LiteralControl(innerTabel));
            //DivDayView.Controls.Add(new LiteralControl(innerTabel));
            //UpdatePanel3.ContentTemplateContainer.Controls.Add(new LiteralControl(innerTabel));
            DivDayView.InnerHtml = innerTabel;
        }
        catch(Exception ex)
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

    private void DisplayCalender(DateTime _date)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int setNumber = ComputeLastDayOfMonth(_date);
            int totalDays = DateTime.DaysInMonth(_date.Year, _date.Month);
            string innertabel = "";
            int dateStatus = 1;
            LinkButton objLink = null;
            string currentDate = _date.Month.ToString() + "/" + dateStatus.ToString() + "/" + _date.Year.ToString();
            string szEndDate = _date.Month.ToString() + "/" + totalDays + "/" + _date.Year.ToString();
            DateTime dtEndDate = new DateTime();
            dtEndDate = Convert.ToDateTime(szEndDate);
            szEndDate = dtEndDate.ToString("MM/dd/yyyy");
            Cal.Controls.Clear();
            Bill_Sys_DisplaySpeciality _objDS = new Bill_Sys_DisplaySpeciality();

            Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
            ArrayList obj = new ArrayList();
            obj.Add(txtCaseID.Text);
            obj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            obj.Add(currentDate);
            obj.Add(szEndDate);
            obj.Add(ddlDoctorList.SelectedValue);
            DataTable dt001 = _objDS.getSpecialityCount(obj);
       
            // get all the data for the month here in a table

            string topLabel = "<table id='Calendar1' cellspacing='0' cellpadding='2' rules='all'" +
                "title='Calendar' border='1' style='width: 100%; height: 400px; font-size: 9pt;" +
                "font-family: Verdana; color: Black; border-width: 1px; border-style: Solid; border-color: Silver;" +
                "background-color: White; border-collapse: collapse;'>";

            Cal.Controls.Add(new LiteralControl(topLabel));
            string dayHeader = "<tr>" +
                         " <th align='center' abbr='Sunday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                          "  font-size: 8pt; font-weight: bold;'>" +
                          "  Sun</th>" +
                        "<th align='center' abbr='Monday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold;'>" +
                          "  Mon</th>" +
                        "<th align='center' abbr='Tuesday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold; width: 123px;'>" +
                          "  Tue</th>" +
                        "<th align='center' abbr='Wednesday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold;'>" +
                          "  Wed</th>" +
                        "<th align='center' abbr='Thursday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold;'>" +
                          "  Thu</th>" +
                        "<th align='center' abbr='Friday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold;'>" +
                          "  Fri</th>" +
                        "<th align='center' abbr='Saturday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold;'>" +
                           " Sat</th>" +

                    "</tr>";

            Cal.Controls.Add(new LiteralControl(dayHeader));


            for (int i = 0; i <= 5; i++)
            {
                innertabel = "<tr>";
                Cal.Controls.Add(new LiteralControl(innertabel));
                for (int j = 1; j <= 7; j++)
                {
                    if (setNumber == j && dateStatus <= totalDays)
                    {
                         currentDate = _date.Month.ToString() + "/" + dateStatus.ToString() + "/" + _date.Year.ToString();

                        innertabel = "<td   style='color: #999999; width: 115px; height: 85px; border-width: 1px; border-style: solid;'>" +
                        "<div id=\"Fdiv" + dateStatus.ToString() + "\" style='vertical-align:top; height:90%;width:auto;  text-align:left;'>" +
                        "<table><tr><td style='vertical-align:top; width:10%;'>";
                        objLink = new LinkButton();
                        objLink.ID = _date.Year.ToString() + "_" + _date.Month.ToString() + "_Link_" + dateStatus.ToString();
                        objLink.CommandArgument = _date.Year.ToString() + "_" + _date.Month.ToString() + "_Link_" + dateStatus.ToString();
                        objLink.Text = "" + dateStatus.ToString();
                        objLink.Click += new EventHandler(Link1_Click);

                        Cal.Controls.Add(new LiteralControl(innertabel));
                        Cal.Controls.Add(objLink);
                        Cal.Controls.Add(new LiteralControl("</td><td style='vertical-align:top; width:90%;'>"));

                        //   "<a href='#' onclick=\" var _date       = new Date(); _date='" + currentDate + "'; setDiv(_date);\" style='color: Black' title='" + getMonth(_date.Month) + " " + dateStatus.ToString() + "'>" +
                        //"" + dateStatus.ToString() + "</a> 
                        // + ;

                        // New Logic

                        //DataTable dt001 = _bill_Sys_Calender.GET_DAY_EVENT(obj);
                        //dt001.t
                        if (currentDate != "")
                        {
                            try
                            {
                                DateTime dt = new DateTime();
                                dt = Convert.ToDateTime(currentDate);
                                currentDate = dt.ToString("MM/dd/yyyy");

                            }
                            catch (Exception ex)
                            {
                                
                                
                            }
                        }

                        DataRow[] dr = dt001.Select("event_date = '" + currentDate + "'");

                        for (int drCount = 0; drCount < dr.Length; drCount++)
                        {
                            if (drCount <= 3)
                            {
                                innertabel = "<a href='#' style='font-size:10px;color:Brown;' onclick=\" document.getElementById('divid').style.position = 'absolute'; document.getElementById('divid').style.left= '350px'; document.getElementById('divid').style.top= '250px';document.getElementById('divid').style.zIndex= '1';  document.getElementById('divid').style.visibility='visible'; document.getElementById('frameeditexpanse').src='Bill_Sys_ViewPatientASpeciality.aspx?id=" + dr[drCount]["SZ_DOCTOR_ID"].ToString()  + "&date=" + currentDate + "'; \" >" + dr[drCount]["Speciality"].ToString() + " - " + dr[drCount]["SP_COUNT"].ToString() + "</a><br/>";
                                Cal.Controls.Add(new LiteralControl(innertabel));
                            }
                            else
                            {
                                objLink = new LinkButton();
                                objLink.ID = "More" + _date.Year.ToString() + "_" + _date.Month.ToString() + "_Link_" + dateStatus.ToString();
                                objLink.CommandArgument = _date.Year.ToString() + "_" + _date.Month.ToString() + "_Link_" + dateStatus.ToString();
                                objLink.Text = "More..";
                                objLink.Click += new EventHandler(Link1_Click);


                                //Cal.Controls.Add(new LiteralControl(innertabel));
                                Cal.Controls.Add(objLink);
                                break;
                                //innertabel =  " <a href='#' style='font-size:9px;' onClick=\" window.parent.document.location.href='Bill_Sys_ScheduleEvent.aspx?_day=" + currentDate.ToString() + "'; \"  >" + dt001.Rows[drCount].ItemArray.GetValue(1).ToString() + "</a>";
                                // Cal.Controls.Add(new LiteralControl(innertabel));
                            }
                        }

                        innertabel = "</td></tr></table></div>" +
                            "<div id=\"Sdiv" + dateStatus.ToString() + "\" style=' text-align:right;height:auto; width:auto; '> " +

                            //"<a href='#' onclick=\"  if (document.getElementById('Sdiv" + dateStatus.ToString() + "').style.backgroundColor=='#e0e0f6' || document.getElementById('Sdiv" + dateStatus.ToString() + "').style.backgroundColor=='rgb(224, 224, 246)') { document.getElementById('Sdiv" + dateStatus.ToString() + "').style.backgroundColor='white' ; document.getElementById('Fdiv" + dateStatus.ToString() + "').style.backgroundColor='white';  for(var H=0;H<dateArray.length;H++){  if (dateArray[H]=='" + currentDate + "') { dateArray[H]=''; } } } else { dateArray[dateArray.length]='" + currentDate + "'; " +
                            //" document.getElementById('Sdiv" + dateStatus.ToString() + "').style.backgroundColor='#E0E0F6'; document.getElementById('Fdiv" + dateStatus.ToString() + "').style.backgroundColor='#E0E0F6';} \" style='color: Black' title='Select'>Select</a>" +
                            "</div>" +
                            "</td>";
                        Cal.Controls.Add(new LiteralControl(innertabel));
                        //dateArray[dateArray.length]='" + currentDate + "'; document.getElementById('td" + dateStatus.ToString() + "').style.background-color='black';
                        dateStatus = dateStatus + 1;
                        setNumber = setNumber + 1;
                    }
                    else
                    {
                        innertabel = "<td align='left' valign='top' style='color: #999999; width: 8%; height: 85px; border-width: 1px; border-style: solid;'>" +
                         "</td>";
                        Cal.Controls.Add(new LiteralControl(innertabel));
                    }
                }
                setNumber = 1;
                innertabel = "</tr>";
                Cal.Controls.Add(new LiteralControl(innertabel));
            }
            Cal.Controls.Add(new LiteralControl("</table>"));
            //Cal.InnerHtml = topLabel + dayHeader + innertabel + "</table>";
        }
        catch(Exception ex)
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


    //private void DisplayCalender(DateTime _date)
    //{
    //    try
    //    {
    //        Cal.Controls.Clear();

    //        // get all the data for the month here in a table

    //        string topLabel = "<table id='Calendar1' cellspacing='0' cellpadding='2' rules='all'" +
    //            "title='Calendar' border='1' style='width: 100%; height: 400px; font-size: 9pt;" +
    //            "font-family: Verdana; color: Black; border-width: 1px; border-style: Solid; border-color: Silver;" +
    //            "background-color: White; border-collapse: collapse;'>";

    //        Cal.Controls.Add(new LiteralControl(topLabel));
    //        string dayHeader = "<tr>" +
    //                     " <th align='center' abbr='Sunday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
    //                      "  font-size: 8pt; font-weight: bold;'>" +
    //                      "  Sun</th>" +
    //                    "<th align='center' abbr='Monday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
    //                     "   font-size: 8pt; font-weight: bold;'>" +
    //                      "  Mon</th>" +
    //                    "<th align='center' abbr='Tuesday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
    //                     "   font-size: 8pt; font-weight: bold; width: 123px;'>" +
    //                      "  Tue</th>" +
    //                    "<th align='center' abbr='Wednesday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
    //                     "   font-size: 8pt; font-weight: bold;'>" +
    //                      "  Wed</th>" +
    //                    "<th align='center' abbr='Thursday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
    //                     "   font-size: 8pt; font-weight: bold;'>" +
    //                      "  Thu</th>" +
    //                    "<th align='center' abbr='Friday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
    //                     "   font-size: 8pt; font-weight: bold;'>" +
    //                      "  Fri</th>" +
    //                    "<th align='center' abbr='Saturday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
    //                     "   font-size: 8pt; font-weight: bold;'>" +
    //                       " Sat</th>" +

    //                "</tr>";

    //        Cal.Controls.Add(new LiteralControl(dayHeader));

    //        int setNumber = ComputeLastDayOfMonth(_date);
    //        int totalDays = DateTime.DaysInMonth(_date.Year, _date.Month);
    //        string innertabel = "";
    //        int dateStatus = 1;
    //        LinkButton objLink = null;
    //        for (int i = 0; i <= 5; i++)
    //        {
    //            innertabel = "<tr>";
    //            Cal.Controls.Add(new LiteralControl(innertabel));
    //            for (int j = 1; j <= 7; j++)
    //            {
    //                if (setNumber == j && dateStatus <= totalDays)
    //                {
    //                    string currentDate = _date.Month.ToString() + "/" + dateStatus.ToString() + "/" + _date.Year.ToString();

    //                    innertabel = "<td   style='color: #999999; width: 115px; height: 85px; border-width: 1px; border-style: solid;'>" +
    //                    "<div id=\"Fdiv" + dateStatus.ToString() + "\" style='vertical-align:top; height:90%;width:auto;  text-align:left;'>" +
    //                    "<table><tr><td style='vertical-align:top; width:10%;'>";
    //                    objLink = new LinkButton();
    //                    objLink.ID = _date.Year.ToString() + "_" + _date.Month.ToString() + "_Link_" + dateStatus.ToString();
    //                    objLink.CommandArgument = _date.Year.ToString() + "_" + _date.Month.ToString() + "_Link_" + dateStatus.ToString();
    //                    objLink.Text = "" + dateStatus.ToString();
    //                    objLink.Click += new EventHandler(Link1_Click);

    //                    Cal.Controls.Add(new LiteralControl(innertabel));
    //                    Cal.Controls.Add(objLink);
    //                    Cal.Controls.Add(new LiteralControl("</td><td style='vertical-align:top; width:90%;'>"));

    //                    //   "<a href='#' onclick=\" var _date       = new Date(); _date='" + currentDate + "'; setDiv(_date);\" style='color: Black' title='" + getMonth(_date.Month) + " " + dateStatus.ToString() + "'>" +
    //                    //"" + dateStatus.ToString() + "</a> 
    //                    // + ;

    //                    // New Logic
    //                    Bill_Sys_DisplaySpeciality _objDS = new Bill_Sys_DisplaySpeciality();

    //                    Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
    //                    ArrayList obj = new ArrayList();
    //                    obj.Add(txtCaseID.Text);
    //                    obj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                    obj.Add(currentDate);
    //                    DataTable dt001 = _objDS.getSpecialityCount(obj);
    //                    //DataTable dt001 = _bill_Sys_Calender.GET_DAY_EVENT(obj);
    //                    for (int drCount = 0; drCount <= dt001.Rows.Count - 1; drCount++)
    //                    {
    //                        if (drCount <= 3)
    //                        {
    //                            innertabel = "<a href='#' style='font-size:10px;color:Brown;' onclick=\" document.getElementById('divid').style.position = 'absolute'; document.getElementById('divid').style.left= '350px'; document.getElementById('divid').style.top= '250px';document.getElementById('divid').style.zIndex= '1';  document.getElementById('divid').style.visibility='visible'; document.getElementById('frameeditexpanse').src='Bill_Sys_ViewPatientASpeciality.aspx?id=" + dt001.Rows[drCount]["SZ_DOCTOR_ID"].ToString() + "&date=" + currentDate + "'; \" >" + dt001.Rows[drCount]["Speciality"].ToString() + " - " + dt001.Rows[drCount]["SP_COUNT"].ToString() + "</a><br/>";
    //                            Cal.Controls.Add(new LiteralControl(innertabel));
    //                        }
    //                        else
    //                        {
    //                            objLink = new LinkButton();
    //                            objLink.ID = "More" + _date.Year.ToString() + "_" + _date.Month.ToString() + "_Link_" + dateStatus.ToString();
    //                            objLink.CommandArgument = _date.Year.ToString() + "_" + _date.Month.ToString() + "_Link_" + dateStatus.ToString();
    //                            objLink.Text = "More..";
    //                            objLink.Click += new EventHandler(Link1_Click);


    //                            //Cal.Controls.Add(new LiteralControl(innertabel));
    //                            Cal.Controls.Add(objLink);
    //                            break;
    //                            //innertabel =  " <a href='#' style='font-size:9px;' onClick=\" window.parent.document.location.href='Bill_Sys_ScheduleEvent.aspx?_day=" + currentDate.ToString() + "'; \"  >" + dt001.Rows[drCount].ItemArray.GetValue(1).ToString() + "</a>";
    //                            // Cal.Controls.Add(new LiteralControl(innertabel));
    //                        }
    //                    }

    //                    innertabel = "</td></tr></table></div>" +
    //                        "<div id=\"Sdiv" + dateStatus.ToString() + "\" style=' text-align:right;height:auto; width:auto; '> " +

    //                        //"<a href='#' onclick=\"  if (document.getElementById('Sdiv" + dateStatus.ToString() + "').style.backgroundColor=='#e0e0f6' || document.getElementById('Sdiv" + dateStatus.ToString() + "').style.backgroundColor=='rgb(224, 224, 246)') { document.getElementById('Sdiv" + dateStatus.ToString() + "').style.backgroundColor='white' ; document.getElementById('Fdiv" + dateStatus.ToString() + "').style.backgroundColor='white';  for(var H=0;H<dateArray.length;H++){  if (dateArray[H]=='" + currentDate + "') { dateArray[H]=''; } } } else { dateArray[dateArray.length]='" + currentDate + "'; " +
    //                        //" document.getElementById('Sdiv" + dateStatus.ToString() + "').style.backgroundColor='#E0E0F6'; document.getElementById('Fdiv" + dateStatus.ToString() + "').style.backgroundColor='#E0E0F6';} \" style='color: Black' title='Select'>Select</a>" +
    //                        "</div>" +
    //                        "</td>";
    //                    Cal.Controls.Add(new LiteralControl(innertabel));
    //                    //dateArray[dateArray.length]='" + currentDate + "'; document.getElementById('td" + dateStatus.ToString() + "').style.background-color='black';
    //                    dateStatus = dateStatus + 1;
    //                    setNumber = setNumber + 1;
    //                }
    //                else
    //                {
    //                    innertabel = "<td align='left' valign='top' style='color: #999999; width: 8%; height: 85px; border-width: 1px; border-style: solid;'>" +
    //                     "</td>";
    //                    Cal.Controls.Add(new LiteralControl(innertabel));
    //                }
    //            }
    //            setNumber = 1;
    //            innertabel = "</tr>";
    //            Cal.Controls.Add(new LiteralControl(innertabel));
    //        }
    //        Cal.Controls.Add(new LiteralControl("</table>"));
    //        //Cal.InnerHtml = topLabel + dayHeader + innertabel + "</table>";
    //    }
    //    catch
    //    {
    //    }
    //}

    private void DayCalenderfordoctor(DateTime _date)
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
            for (decimal iday = startTime; iday < endTime; iday = iday + Convert.ToDecimal(ddlInterval.SelectedItem.Value))
            {
               // txtDate.Text = Convert.ToString(_date);
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
                obj.Add(ddlDoctorList.SelectedValue);
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
                //        obj.Add(iday-12);
                //    }
                //    else
                //    {
                //        obj.Add(iday-13);
                //        obj.Add(iday-12);
                //    }

                //    obj.Add("PM");
                //}

                DataTable daySheduledt = _bill_Sys_Calender.GET_DAY_SHEDULED_EVENT_FOR_SEARCH_BY_DOCTOR(obj);
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

                              "<td align='left'  style='width:20%;border-width: 1px; border-style: solid;background-color:#CCCCCC;" +
                               "font-size: 8pt; font-weight: bold;color:steelblue;'  >" + strFirstCol + " </td>";// +
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
                for (int i = 0; i < daySheduledt.Rows.Count; i++)
                {
                    //+ daySheduledt.Rows[i].ItemArray.GetValue(3).ToString() + " "
                    strLblHeading = strLblHeading + daySheduledt.Rows[i].ItemArray.GetValue(1).ToString() + " "
                                    + "<a href='#' style='font-size:11px;' onclick=\" document.getElementById('divid').style.position = 'absolute'; document.getElementById('divid').style.left= '350px'; document.getElementById('divid').style.top= '250px';document.getElementById('divid').style.zIndex= '1';  document.getElementById('divid').style.visibility='visible'; document.getElementById('frameeditexpanse').src='ViwScheduled.aspx?id=" + daySheduledt.Rows[i].ItemArray.GetValue(0).ToString() + "'; \" >" + daySheduledt.Rows[i].ItemArray.GetValue(4).ToString() + daySheduledt.Rows[i].ItemArray.GetValue(6).ToString() + daySheduledt.Rows[i].ItemArray.GetValue(7).ToString() + "</a> " + "<br/>";
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



                }
                strFirstCol = strFirstCol + ddlInterval.SelectedItem.Value;
                strLblHeading = strLblHeading + "<a href='#' onclick=\" var _time='" + strFirstCol + "';  var _date       = new Date(); _date='" + _date + "'; setDiv(_date,_time);\" style='color: Black' title='" + getMonth(_date.Month) + " " + _date.Day.ToString() + "'>" +
                        "Add" + "</a>";
                innerTabel = innerTabel + "<td align='center'  style='width:80%;border-width: 1px;height: 20px; border-style: solid;background-color:#CCCCCC;" +
                    "font-size: 8pt; font-weight: bold;color:steelblue;'>" + strLblHeading + "</td>" + "</tr>";
            }
            innerTabel = innerTabel + "</table>";
            //UpdatePanel11.ContentTemplateContainer.Controls.Add(new LiteralControl(innertabel));
            // DivDayView.Controls.Add(new LiteralControl(innerTabel));
            //DivDayView.Controls.Add(new LiteralControl(innerTabel));
            //UpdatePanel3.ContentTemplateContainer.Controls.Add(new LiteralControl(innerTabel));
            DivDayView.InnerHtml = innerTabel;
        }
        catch(Exception ex)
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

    private void GetDoctorList()
    {
         Bill_Sys_Calender _bill_Sys_Calender=new Bill_Sys_Calender();
         ddlDoctorList.DataSource=_bill_Sys_Calender.GetDoctorlList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
         ddlDoctorList.DataValueField = "CODE";
         ddlDoctorList.DataTextField = "DESCRIPTION";
         ddlDoctorList.DataBind();
        
    } 
    protected void Link1_Click(object sender, EventArgs e)
    {
        // day view starts

        LinkButton objLink = (LinkButton)sender;
       // Label1.Text = "Refreshed at " + DateTime.Now.ToString() + " " + " pressed by " + objLink.CommandArgument;

        string[] szDate = null;
        szDate = objLink.CommandArgument.Split('_');

        string szYear = szDate[0];
        string szMonth = szDate[1];
        string szBogus = szDate[2];
        string szDay = szDate[3];

        txtDate.Text = szMonth + '/' + szDay + '/' + szYear;
        DayCalender(Convert.ToDateTime(szMonth + '/' + szDay + '/' + szYear));
        tabcontainerPatientVisit.ActiveTabIndex = 1;


        //DisplayCalender(Convert.ToDateTime(szMonth + '/' + szDay + '/' + szYear));
        //DateTime objSource = new DateTime(Int32.Parse(szYear), getIntMonth(szMonth), Int32.Parse(szDay));
        ////lblCurrentDate.Text = String.Format("{0:dddd, MMMM d, yyyy}", objSource);
        //string szScheduleDay = String.Format("{0:MM/dd/yyyy}", objSource); // MMDDYY format
        //if (extddlReferringFacility.Visible == false)
        //{
        //    GetCalenderDayAppointments(szScheduleDay, txtCompanyID.Text);
        //}
        //else
        //{
        //    GetCalenderDayAppointments(szScheduleDay, extddlReferringFacility.Text);
        //}
        //DisplayCalender(DateTime.Now.Date);
    }


    public string getMonth(int i)
    {
        switch (i)
        {
            case 1: return "January"; break;
            case 2: return "Feb"; break;
            case 3: return "April"; break;
            case 4: return "March"; break;
            case 5: return "May"; break;
            case 6: return "June"; break;
            case 7: return "July"; break;
            case 8: return "August"; break;
            case 9: return "September"; break;
            case 10: return "Octomber"; break;
            case 11: return "November"; break;
            case 12: return "December"; break;

        }
        return "";
    }

    public int ComputeLastDayOfMonth(DateTime TheDate)
    {

        return getDayNumber(TheDate.AddDays(-(TheDate.Day- 1)).DayOfWeek.ToString());

    }

    public int getDayNumber(string p_szDay)
    {
        switch (p_szDay)
        {
            case "Sunday": return 1; break;
            case "Monday": return 2; break;
            case "Tuesday": return 3; break;
            case "Wednesday": return 4; break;
            case "Thursday": return 5; break;
            case "Friday": return 6; break;
            case "Saturday": return 7; break;


        }
        return 0;
    }

    protected void btnHdn_Click(object sender, EventArgs e)
    {
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
                tabcontainerPatientVisit.ActiveTabIndex = 1;

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
        //if (Request.QueryString["_day"] != null)
        //{
        //    DayCalender(Convert.ToDateTime(Request.QueryString["_day"].ToString()));
        //    tabcontainerPatientVisit.ActiveTabIndex = 1;
        //}
        //else if (Request.QueryString["idate"] != null)
        //{
        //    DayCalender(Convert.ToDateTime(Request.QueryString["idate"].ToString()));
        //    tabcontainerPatientVisit.ActiveTabIndex = 1;
        //}
        //else
        //{
        //    DayCalender(DateTime.Now.Date);
        //    tabcontainerPatientVisit.ActiveTabIndex = 1;
        //}
    }
    protected void btnsearchbydoctor_Click(object sender, EventArgs e)
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
                DayCalenderfordoctor(dt);
                tabcontainerPatientVisit.ActiveTabIndex = 1;

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
          
            
            //if (Request.QueryString["_day"] != null)
            //{
            //    DayCalenderfordoctor(Convert.ToDateTime(Request.QueryString["_day"].ToString()));
            //    tabcontainerPatientVisit.ActiveTabIndex = 1;
            //}
            //else if (Request.QueryString["idate"] != null)
            //{
            //    DayCalenderfordoctor(Convert.ToDateTime(Request.QueryString["idate"].ToString()));
            //    tabcontainerPatientVisit.ActiveTabIndex = 1;
            //}
            //else
            //{
            //    DayCalenderfordoctor(DateTime.Now.Date);
            //    tabcontainerPatientVisit.ActiveTabIndex = 1;
            //}
        


    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        DisplayCalender(Convert.ToDateTime(Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/" + Convert.ToInt16(ddlYear.SelectedValue)));
    
    }

    protected void tabcontainerPatientVisit_ActiveTabChanged(object sender, EventArgs e)
    {
        int i = tabcontainerPatientVisit.ActiveTabIndex;
        if (i == 0)
        {
            DisplayCalender(Convert.ToDateTime(Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/" + Convert.ToInt16(ddlYear.SelectedValue)));
        }


    }

    protected void txtUpdate_Click(object sender, EventArgs e)
    {

        if (txtDate.Text == DateTime.Now.ToString("MM/dd/yyyy"))
        {
            if (Request.QueryString["_day"] != null)
            {
                DayCalender(Convert.ToDateTime(Request.QueryString["_day"].ToString()));
                tabcontainerPatientVisit.ActiveTabIndex = 1;
            }
            else if (Request.QueryString["idate"] != null)
            {
                DayCalender(Convert.ToDateTime(Request.QueryString["idate"].ToString()));
                tabcontainerPatientVisit.ActiveTabIndex = 1;
            }
            else
            {
                DayCalender(DateTime.Now.Date);
                tabcontainerPatientVisit.ActiveTabIndex = 1;
            }
        }
        else
        {
            if (txtDate.Text != "")
            {
                DayCalender(Convert.ToDateTime(txtDate.Text));
            }
        }
       
    }
   
   
}
