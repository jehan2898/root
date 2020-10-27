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

public partial class Bill_Sys_CalendarEvent : PageBase
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
            TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;             
          //  imgbtnEventDate.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtEventDate,'imgbtnEventDate','MM/dd/yyyy'); return false;");
            lblCaseID.Text = Session["SZ_CASE_ID"].ToString();
            txtCaseID.Text = Session["SZ_CASE_ID"].ToString();
            lblPatientName.Text = Session["PROVIDERNAME"].ToString();
            
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnSave.Attributes.Add("onclick", "return formValidator('frmCalendarEvent','txtDoctorName,txtEventDate,txtEventNotes');");
            if (!IsPostBack)
            {
                BindTimeControl();
                // Code added For month calender
             
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                GetMonthCalender(DateTime.Now.Month, DateTime.Now.Year,"","");
                lblDateTime.Text = ddlMonth.SelectedItem.Text + ", " + ddlYear.SelectedItem.Text;
                // End
            }
            if (ddlTime.SelectedItem.Text == "PM")
            {
                txtTime.Text = (12 + Convert.ToInt32(ddlHours.SelectedItem.Text)) + "." + ddlMinutes.SelectedItem.Text;
            }
            else
            {
                txtTime.Text =ddlHours.SelectedItem.Text + "." + ddlMinutes.SelectedItem.Text;
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
            cv.MakeReadOnlyPage("Bill_Sys_CalendarEvent.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void SaveEvent()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        { 
            _saveOperation.WebPage = this.Page;            
            _saveOperation.Xml_File = "CalendarEvent.xml";
            _saveOperation.SaveMethod();       
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {           
            SaveEvent();          
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Event successfully saved!'); ", true);
            ddlMonth.SelectedValue = Convert.ToDateTime(txtEventDate.Text).Month.ToString();
            ddlYear.SelectedValue = Convert.ToDateTime(txtEventDate.Text).Year.ToString();
            GetMonthCalender(Convert.ToDateTime(txtEventDate.Text).Month, Convert.ToDateTime(txtEventDate.Text).Year,"","");
            lblDateTime.Text = ddlMonth.SelectedItem.Text + ", " + ddlYear.SelectedItem.Text;
            Clear();
            lblMsg.Visible = true;
            lblMsg.Text = "Event Saved Successfully...!";
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

    private void Clear()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtDoctorName.Text = "";
            txtEventDate.Text = "";
            txtEventNotes.Text = "";
            ddlHours.SelectedIndex= 0;
            ddlMinutes.SelectedIndex= 0;
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_SearchCase.aspx", false);
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
                    ddlHours.Items.Add(i.ToString());
                }
                else
                {
                    ddlHours.Items.Add("0" + i.ToString());
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9)
                {
                    ddlMinutes.Items.Add(i.ToString());
                }
                else
                {
                    ddlMinutes.Items.Add("0" + i.ToString());
                }
            }
            ddlTime.Items.Add("AM");
            ddlTime.Items.Add("PM");
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

    // Code For Month Calender

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

    public string getDay(int i)
    {
        switch (i)
        {
            case 1: return "Sunday"; break;
            case 2: return "Monday"; break;
            case 3: return "Tuesday"; break;
            case 4: return "Wednesday"; break;
            case 5: return "Thrusday"; break;
            case 6: return "Friday"; break;
            case 7: return "Saturday"; break;


        }
        return "";
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

    public string GetMonthCalender(int p_intMonth, int p_intYear,string doctorName,string patientName)
    {
        GetMonthList(p_intMonth, p_intYear, doctorName, patientName);        
        return null;
    }


    private string GetMonthList(int p_intMonth, int p_intYear,string doctorName,string patientName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Session["CaseID"] = Session["SZ_CASE_ID"];      

        Bill_Sys_BillingCompanyDetails_BO _obj = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            VisibleLinkButton();
            VisibleLabels();
            int iDaysInMonths = 0;



            DateTime objDate = new DateTime(p_intYear, p_intMonth, 1);
            iDaysInMonths = System.DateTime.DaysInMonth(p_intYear, p_intMonth);
            int iStartNumber = getDayNumber(objDate.DayOfWeek.ToString());
            int j = 1;
           if(iDaysInMonths==31)
           {
              if (iStartNumber == 1)
                {
                    lblDay0.Text  = "1";               
                    lblDay1.Text = "2";
                    lblDay2.Text = "3";
                    lblDay3.Text = "4";
                    lblDay4.Text = "5";
                    lblDay5.Text = "6";
                    lblDay6.Text = "7";
                    lblDay7.Text = "8";
                    lblDay8.Text = "9";
                    lblDay9.Text = "10";
                    lblDay10.Text = "11";
                    lblDay11.Text = "12";
                    lblDay12.Text = "13";
                    lblDay13.Text = "14";
                    lblDay14.Text = "15";
                    lblDay15.Text = "16";
                    lblDay16.Text = "17";
                    lblDay17.Text = "18";
                    lblDay18.Text = "19";
                    lblDay19.Text = "20";
                    lblDay20.Text = "21";
                    lblDay21.Text = "22";
                    lblDay22.Text = "23";
                    lblDay23.Text = "24";
                    lblDay24.Text = "25";
                    lblDay25.Text = "26";
                    lblDay26.Text = "27";
                    lblDay27.Text = "28";
                    lblDay28.Text = "29";
                    lblDay29.Text = "30";
                    lblDay30.Text = "31";


                    lblDay31.Visible = false;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay31.Visible = false;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay0.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay0.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay1.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay1.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay2.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    
 
                }
           if (iStartNumber == 2)
                {
                    lblDay1.Text  = "1";                    
                    lblDay2.Text = "2";
                    lblDay3.Text = "3";
                    lblDay4.Text = "4";
                    lblDay5.Text = "5";
                    lblDay6.Text = "6";
                    lblDay7.Text = "7";
                    lblDay8.Text = "8";
                    lblDay9.Text = "9";
                    lblDay10.Text = "10";
                    lblDay11.Text = "11";
                    lblDay12.Text = "12";
                    lblDay13.Text = "13";
                    lblDay14.Text = "14";
                    lblDay15.Text = "15";
                    lblDay16.Text = "16";
                    lblDay17.Text = "17";
                    lblDay18.Text = "18";
                    lblDay19.Text = "19";
                    lblDay20.Text = "20";
                    lblDay21.Text = "21";
                    lblDay22.Text = "22";
                    lblDay23.Text = "23";
                    lblDay24.Text = "24";
                    lblDay25.Text = "25";
                    lblDay26.Text = "26";
                    lblDay27.Text = "27";
                    lblDay28.Text = "28";
                    lblDay29.Text = "29";
                    lblDay30.Text = "30";
                    lblDay31.Text = "31";

                    lblDay0.Visible = false;
                    lblDay31.Visible = true;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay31.Visible = true;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;
                    
                    lnkDay1.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay1.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    
 
                }
                if (iStartNumber == 3)
                {
                    lblDay2.Text = "1";
                    lblDay3.Text = "2";
                    lblDay4.Text = "3";
                    lblDay5.Text = "4";
                    lblDay6.Text = "5";
                    lblDay7.Text = "6";
                    lblDay8.Text = "7";
                    lblDay9.Text = "8";
                    lblDay10.Text = "9";
                    lblDay11.Text = "10";
                    lblDay12.Text = "11";
                    lblDay13.Text = "12";
                    lblDay14.Text = "13";
                    lblDay15.Text = "14";
                    lblDay16.Text = "15";
                    lblDay17.Text = "16";
                    lblDay18.Text = "17";
                    lblDay19.Text = "18";
                    lblDay20.Text = "19";
                    lblDay21.Text = "20";
                    lblDay22.Text = "21";
                    lblDay23.Text = "22";
                    lblDay24.Text = "23";
                    lblDay25.Text = "24";
                    lblDay26.Text = "25";
                    lblDay27.Text = "26";
                    lblDay28.Text = "27";
                    lblDay29.Text = "28";
                    lblDay30.Text = "29";
                    lblDay31.Text = "30";
                    lblDay32.Text = "31";

                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay32.Visible = true;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay32.Visible = true;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;


                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);

                }
                if (iStartNumber == 4)
                {
                    lblDay3.Text = "1";
                    lblDay4.Text = "2";
                    lblDay5.Text = "3";
                    lblDay6.Text = "4";
                    lblDay7.Text = "5";
                    lblDay8.Text = "6";
                    lblDay9.Text = "7";
                    lblDay10.Text = "8";
                    lblDay11.Text = "9";
                    lblDay12.Text = "10";
                    lblDay13.Text = "11";
                    lblDay14.Text = "12";
                    lblDay15.Text = "13";
                    lblDay16.Text = "14";
                    lblDay17.Text = "15";
                    lblDay18.Text = "16";
                    lblDay19.Text = "17";
                    lblDay20.Text = "18";
                    lblDay21.Text = "19";
                    lblDay22.Text = "20";
                    lblDay23.Text = "21";
                    lblDay24.Text = "22";
                    lblDay25.Text = "23";
                    lblDay26.Text = "24";
                    lblDay27.Text = "25";
                    lblDay28.Text = "26";
                    lblDay29.Text = "27";
                    lblDay30.Text = "28";
                    lblDay31.Text = "29";
                    lblDay32.Text = "30";
                    lblDay33.Text = "31";

                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay33.Visible = true;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay33.Visible = true;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;


                    lnkDay3.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text =_obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay33.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay33.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                }
                if (iStartNumber == 5)
                {
                    lblDay4.Text = "1";
                    lblDay5.Text = "2";
                    lblDay6.Text = "3";
                    lblDay7.Text = "4";
                    lblDay8.Text = "5";
                    lblDay9.Text = "6";
                    lblDay10.Text = "7";
                    lblDay11.Text = "8";
                    lblDay12.Text = "9";
                    lblDay13.Text = "10";
                    lblDay14.Text = "11";
                    lblDay15.Text = "12";
                    lblDay16.Text = "13";
                    lblDay17.Text = "14";
                    lblDay18.Text = "15";
                    lblDay19.Text = "16";
                    lblDay20.Text = "17";
                    lblDay21.Text = "18";
                    lblDay22.Text = "19";
                    lblDay23.Text = "20";
                    lblDay24.Text = "21";
                    lblDay25.Text = "22";
                    lblDay26.Text = "23";
                    lblDay27.Text = "24";
                    lblDay28.Text = "25";
                    lblDay29.Text = "26";
                    lblDay30.Text = "27";
                    lblDay31.Text = "28";
                    lblDay32.Text = "29";
                    lblDay33.Text = "30";
                    lblDay34.Text = "31";

                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay34.Visible = true;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay34.Visible = true;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;
                    
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay33.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay33.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay34.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay34.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);

                }
                if (iStartNumber == 6)
                {
                    lblDay5.Text = "1";
                    lblDay6.Text = "2";
                    lblDay7.Text = "3";
                    lblDay8.Text = "4";
                    lblDay9.Text = "5";
                    lblDay10.Text = "6";
                    lblDay11.Text = "7";
                    lblDay12.Text = "8";
                    lblDay13.Text = "9";
                    lblDay14.Text = "10";
                    lblDay15.Text = "11";
                    lblDay16.Text = "12";
                    lblDay17.Text = "13";
                    lblDay18.Text = "14";
                    lblDay19.Text = "15";
                    lblDay20.Text = "16";
                    lblDay21.Text = "17";
                    lblDay22.Text = "18";
                    lblDay23.Text = "19";
                    lblDay24.Text = "20";
                    lblDay25.Text = "21";
                    lblDay26.Text = "22";
                    lblDay27.Text = "23";
                    lblDay28.Text = "24";
                    lblDay29.Text = "25";
                    lblDay30.Text = "26";
                    lblDay31.Text = "27";
                    lblDay32.Text = "28";
                    lblDay33.Text = "29";
                    lblDay34.Text = "30";
                    lblDay35.Text = "31";


                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay4.Visible = false;
                    lblDay35.Visible = true;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay4.Visible = false;
                    lnkDay35.Visible = true;

                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;
                    
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay33.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay33.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay34.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay34.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay35.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay35.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);


                }
                if (iStartNumber == 7)
                {
                    lblDay6.Text = "1";
                    lblDay7.Text = "2";                                      
                    lblDay8.Text = "3";
                    lblDay9.Text = "4";
                    lblDay10.Text = "5";
                    lblDay11.Text = "6";
                    lblDay12.Text = "7";
                    lblDay13.Text = "8";
                    lblDay14.Text = "9";
                    lblDay15.Text = "10";
                    lblDay16.Text = "11";
                    lblDay17.Text = "12";
                    lblDay18.Text = "13";
                    lblDay19.Text = "14";
                    lblDay20.Text = "15";
                    lblDay21.Text = "16";
                    lblDay22.Text = "17";
                    lblDay23.Text = "18";
                    lblDay24.Text = "19";
                    lblDay25.Text = "20";
                    lblDay26.Text = "21";
                    lblDay27.Text = "22";
                    lblDay28.Text = "23";
                    lblDay29.Text = "24";
                    lblDay30.Text = "25";
                    lblDay31.Text = "26";
                    lblDay32.Text = "27";
                    lblDay33.Text = "28";
                    lblDay34.Text = "29";
                    lblDay35.Text = "30";
                    lblDay36.Text = "31";

                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay4.Visible = false;
                    lblDay5.Visible = false;
                    lblDay36.Visible = true;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay4.Visible = false;
                    lnkDay5.Visible = false;
                    lnkDay36.Visible = true;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay33.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay33.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay34.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay34.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay35.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay35.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay36.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay36.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);


                }
              
           }
            if(iDaysInMonths==30)
            {
                ShowThirtyDays(iStartNumber,doctorName,patientName);
            }
            if (iDaysInMonths == 28 || iDaysInMonths == 29)
            {
                ShowTwentyEightDays(iStartNumber, iDaysInMonths,doctorName,patientName);
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
        return null;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetMonthCalender(Convert.ToInt16(ddlMonth.SelectedValue), Convert.ToInt16(ddlYear.SelectedValue),"","");
        lblDateTime.Text = ddlMonth.SelectedItem.Text + ", " + ddlYear.SelectedItem.Text;
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetMonthCalender(Convert.ToInt16(ddlMonth.SelectedValue), Convert.ToInt16(ddlYear.SelectedValue),"","");
        lblDateTime.Text = ddlMonth.SelectedItem.Text + ", " + ddlYear.SelectedItem.Text;
    }

    protected void imgbtnPrevDate_Click(object sender, EventArgs e)
    {

        DateTime objDateTime = new DateTime(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue), 1);
        objDateTime = objDateTime.AddMonths(-1);

        ddlMonth.SelectedValue = objDateTime.Month.ToString();
        ddlYear.SelectedValue = objDateTime.Year.ToString();
        GetMonthCalender(objDateTime.Month, objDateTime.Year,"","");
        lblDateTime.Text = ddlMonth.SelectedItem.Text + ", " + ddlYear.SelectedItem.Text;
        lblMsg.Visible = false;
        
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        DateTime objDateTime = new DateTime(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue), 1);
        objDateTime = objDateTime.AddMonths(1);

        ddlMonth.SelectedValue = objDateTime.Month.ToString();
        ddlYear.SelectedValue = objDateTime.Year.ToString();
        GetMonthCalender(objDateTime.Month, objDateTime.Year,"","");
        lblDateTime.Text = ddlMonth.SelectedItem.Text + ", " + ddlYear.SelectedItem.Text;
        lblMsg.Visible = false;
    }

    protected void btnSetSession_Click(object sender, EventArgs e)
    {
        Session["CURRENT_DATE"] = hdnSessionValue.Value;
    }


    protected void lnkDay4_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay4.Text);
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
    private void ShowThirtyDays(int iStartNumber, string doctorName, string patientName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_BillingCompanyDetails_BO _obj = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            VisibleLinkButton();
            VisibleLabels();
             if (iStartNumber == 1)
                {
                    lblDay0.Text  = "1";               
                    lblDay1.Text = "2";
                    lblDay2.Text = "3";
                    lblDay3.Text = "4";
                    lblDay4.Text = "5";
                    lblDay5.Text = "6";
                    lblDay6.Text = "7";
                    lblDay7.Text = "8";
                    lblDay8.Text = "9";
                    lblDay9.Text = "10";
                    lblDay10.Text = "11";
                    lblDay11.Text = "12";
                    lblDay12.Text = "13";
                    lblDay13.Text = "14";
                    lblDay14.Text = "15";
                    lblDay15.Text = "16";
                    lblDay16.Text = "17";
                    lblDay17.Text = "18";
                    lblDay18.Text = "19";
                    lblDay19.Text = "20";
                    lblDay20.Text = "21";
                    lblDay21.Text = "22";
                    lblDay22.Text = "23";
                    lblDay23.Text = "24";
                    lblDay24.Text = "25";
                    lblDay25.Text = "26";
                    lblDay26.Text = "27";
                    lblDay27.Text = "28";
                    lblDay28.Text = "29";
                    lblDay29.Text = "30";


                    lblDay30.Visible = false;
                    lblDay31.Visible = false;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay30.Visible = false;
                    lnkDay31.Visible = false;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;
                  
                    lnkDay0.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay0.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay1.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay1.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                   
                    
 
                }
           if (iStartNumber == 2)
                {
                    lblDay1.Text  = "1";                    
                    lblDay2.Text = "2";
                    lblDay3.Text = "3";
                    lblDay4.Text = "4";
                    lblDay5.Text = "5";
                    lblDay6.Text = "6";
                    lblDay7.Text = "7";
                    lblDay8.Text = "8";
                    lblDay9.Text = "9";
                    lblDay10.Text = "10";
                    lblDay11.Text = "11";
                    lblDay12.Text = "12";
                    lblDay13.Text = "13";
                    lblDay14.Text = "14";
                    lblDay15.Text = "15";
                    lblDay16.Text = "16";
                    lblDay17.Text = "17";
                    lblDay18.Text = "18";
                    lblDay19.Text = "19";
                    lblDay20.Text = "20";
                    lblDay21.Text = "21";
                    lblDay22.Text = "22";
                    lblDay23.Text = "23";
                    lblDay24.Text = "24";
                    lblDay25.Text = "25";
                    lblDay26.Text = "26";
                    lblDay27.Text = "27";
                    lblDay28.Text = "28";
                    lblDay29.Text = "29";
                    lblDay30.Text = "30";
                   

                    lblDay0.Visible = false;

                    lblDay30.Visible = true;
                    lblDay31.Visible = false;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay30.Visible = true;
                    lnkDay31.Visible = false;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;
                    
                    lnkDay1.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay1.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    
                    
 
                }
                if (iStartNumber == 3)
                {
                    lblDay2.Text = "1";
                    lblDay3.Text = "2";
                    lblDay4.Text = "3";
                    lblDay5.Text = "4";
                    lblDay6.Text = "5";
                    lblDay7.Text = "6";
                    lblDay8.Text = "7";
                    lblDay9.Text = "8";
                    lblDay10.Text = "9";
                    lblDay11.Text = "10";
                    lblDay12.Text = "11";
                    lblDay13.Text = "12";
                    lblDay14.Text = "13";
                    lblDay15.Text = "14";
                    lblDay16.Text = "15";
                    lblDay17.Text = "16";
                    lblDay18.Text = "17";
                    lblDay19.Text = "18";
                    lblDay20.Text = "19";
                    lblDay21.Text = "20";
                    lblDay22.Text = "21";
                    lblDay23.Text = "22";
                    lblDay24.Text = "23";
                    lblDay25.Text = "24";
                    lblDay26.Text = "25";
                    lblDay27.Text = "26";
                    lblDay28.Text = "27";
                    lblDay29.Text = "28";
                    lblDay30.Text = "29";
                    lblDay31.Text = "30";
                    

                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay31.Visible = true;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay31.Visible = true;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;


                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    

                }
                if (iStartNumber == 4)
                {
                    lblDay3.Text = "1";
                    lblDay4.Text = "2";
                    lblDay5.Text = "3";
                    lblDay6.Text = "4";
                    lblDay7.Text = "5";
                    lblDay8.Text = "6";
                    lblDay9.Text = "7";
                    lblDay10.Text = "8";
                    lblDay11.Text = "9";
                    lblDay12.Text = "10";
                    lblDay13.Text = "11";
                    lblDay14.Text = "12";
                    lblDay15.Text = "13";
                    lblDay16.Text = "14";
                    lblDay17.Text = "15";
                    lblDay18.Text = "16";
                    lblDay19.Text = "17";
                    lblDay20.Text = "18";
                    lblDay21.Text = "19";
                    lblDay22.Text = "20";
                    lblDay23.Text = "21";
                    lblDay24.Text = "22";
                    lblDay25.Text = "23";
                    lblDay26.Text = "24";
                    lblDay27.Text = "25";
                    lblDay28.Text = "26";
                    lblDay29.Text = "27";
                    lblDay30.Text = "28";
                    lblDay31.Text = "29";
                    lblDay32.Text = "30";


                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay32.Visible = true;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay32.Visible = true;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;


                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    
                }
                if (iStartNumber == 5)
                {
                    lblDay4.Text = "1";
                    lblDay5.Text = "2";
                    lblDay6.Text = "3";
                    lblDay7.Text = "4";
                    lblDay8.Text = "5";
                    lblDay9.Text = "6";
                    lblDay10.Text = "7";
                    lblDay11.Text = "8";
                    lblDay12.Text = "9";
                    lblDay13.Text = "10";
                    lblDay14.Text = "11";
                    lblDay15.Text = "12";
                    lblDay16.Text = "13";
                    lblDay17.Text = "14";
                    lblDay18.Text = "15";
                    lblDay19.Text = "16";
                    lblDay20.Text = "17";
                    lblDay21.Text = "18";
                    lblDay22.Text = "19";
                    lblDay23.Text = "20";
                    lblDay24.Text = "21";
                    lblDay25.Text = "22";
                    lblDay26.Text = "23";
                    lblDay27.Text = "24";
                    lblDay28.Text = "25";
                    lblDay29.Text = "26";
                    lblDay30.Text = "27";
                    lblDay31.Text = "28";
                    lblDay32.Text = "29";
                    lblDay33.Text = "30";
             

                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay33.Visible = true;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay33.Visible = true;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;
                    
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay33.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay33.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    

                }
                if (iStartNumber == 6)
                {
                    lblDay5.Text = "1";
                    lblDay6.Text = "2";
                    lblDay7.Text = "3";
                    lblDay8.Text = "4";
                    lblDay9.Text = "5";
                    lblDay10.Text = "6";
                    lblDay11.Text = "7";
                    lblDay12.Text = "8";
                    lblDay13.Text = "9";
                    lblDay14.Text = "10";
                    lblDay15.Text = "11";
                    lblDay16.Text = "12";
                    lblDay17.Text = "13";
                    lblDay18.Text = "14";
                    lblDay19.Text = "15";
                    lblDay20.Text = "16";
                    lblDay21.Text = "17";
                    lblDay22.Text = "18";
                    lblDay23.Text = "19";
                    lblDay24.Text = "20";
                    lblDay25.Text = "21";
                    lblDay26.Text = "22";
                    lblDay27.Text = "23";
                    lblDay28.Text = "24";
                    lblDay29.Text = "25";
                    lblDay30.Text = "26";
                    lblDay31.Text = "27";
                    lblDay32.Text = "28";
                    lblDay33.Text = "29";
                    lblDay34.Text = "30";
         


                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay4.Visible = false;
                    lblDay34.Visible = true;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay4.Visible = false;
                    lnkDay34.Visible = true;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;
                    
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay33.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay33.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay34.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay34.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    


                }
                if (iStartNumber == 7)
                {
                    lblDay6.Text = "1";
                    lblDay7.Text = "2";                                      
                    lblDay8.Text = "3";
                    lblDay9.Text = "4";
                    lblDay10.Text = "5";
                    lblDay11.Text = "6";
                    lblDay12.Text = "7";
                    lblDay13.Text = "8";
                    lblDay14.Text = "9";
                    lblDay15.Text = "10";
                    lblDay16.Text = "11";
                    lblDay17.Text = "12";
                    lblDay18.Text = "13";
                    lblDay19.Text = "14";
                    lblDay20.Text = "15";
                    lblDay21.Text = "16";
                    lblDay22.Text = "17";
                    lblDay23.Text = "18";
                    lblDay24.Text = "19";
                    lblDay25.Text = "20";
                    lblDay26.Text = "21";
                    lblDay27.Text = "22";
                    lblDay28.Text = "23";
                    lblDay29.Text = "24";
                    lblDay30.Text = "25";
                    lblDay31.Text = "26";
                    lblDay32.Text = "27";
                    lblDay33.Text = "28";
                    lblDay34.Text = "29";
                    lblDay35.Text = "30";
                 

                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay4.Visible = false;
                    lblDay5.Visible = false;
                    lblDay35.Visible = true;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay4.Visible = false;
                    lnkDay5.Visible = false;
                    lnkDay35.Visible = true;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay33.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay33.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay34.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay34.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay35.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay35.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    


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
    private void ShowTwentyEightDays(int iStartNumber, int days, string doctorName, string patientName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_BillingCompanyDetails_BO _obj = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            VisibleLinkButton();
            VisibleLabels();
            if (days == 29)
            {
                if (iStartNumber == 1)
                {
                    lblDay0.Text = "1";
                    lblDay1.Text = "2";
                    lblDay2.Text = "3";
                    lblDay3.Text = "4";
                    lblDay4.Text = "5";
                    lblDay5.Text = "6";
                    lblDay6.Text = "7";
                    lblDay7.Text = "8";
                    lblDay8.Text = "9";
                    lblDay9.Text = "10";
                    lblDay10.Text = "11";
                    lblDay11.Text = "12";
                    lblDay12.Text = "13";
                    lblDay13.Text = "14";
                    lblDay14.Text = "15";
                    lblDay15.Text = "16";
                    lblDay16.Text = "17";
                    lblDay17.Text = "18";
                    lblDay18.Text = "19";
                    lblDay19.Text = "20";
                    lblDay20.Text = "21";
                    lblDay21.Text = "22";
                    lblDay22.Text = "23";
                    lblDay23.Text = "24";
                    lblDay24.Text = "25";
                    lblDay25.Text = "26";
                    lblDay26.Text = "27";
                    lblDay27.Text = "28";
                    lblDay28.Text = "29";



                    lblDay29.Visible = false;
                    lblDay30.Visible = false;
                    lblDay31.Visible = false;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay29.Visible = false;
                    lnkDay30.Visible = false;
                    lnkDay31.Visible = false;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay0.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay0.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay1.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay1.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);



                }
                if (iStartNumber == 2)
                {
                    lblDay1.Text = "1";
                    lblDay2.Text = "2";
                    lblDay3.Text = "3";
                    lblDay4.Text = "4";
                    lblDay5.Text = "5";
                    lblDay6.Text = "6";
                    lblDay7.Text = "7";
                    lblDay8.Text = "8";
                    lblDay9.Text = "9";
                    lblDay10.Text = "10";
                    lblDay11.Text = "11";
                    lblDay12.Text = "12";
                    lblDay13.Text = "13";
                    lblDay14.Text = "14";
                    lblDay15.Text = "15";
                    lblDay16.Text = "16";
                    lblDay17.Text = "17";
                    lblDay18.Text = "18";
                    lblDay19.Text = "19";
                    lblDay20.Text = "20";
                    lblDay21.Text = "21";
                    lblDay22.Text = "22";
                    lblDay23.Text = "23";
                    lblDay24.Text = "24";
                    lblDay25.Text = "25";
                    lblDay26.Text = "26";
                    lblDay27.Text = "27";
                    lblDay28.Text = "28";
                    lblDay29.Text = "29";


                    lblDay0.Visible = false;
                    lblDay29.Visible = true;
                    lblDay30.Visible = true;
                    lblDay31.Visible = true;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay29.Visible = true;
                    lnkDay30.Visible = false;
                    lnkDay31.Visible = false;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay1.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay1.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);



                }
                if (iStartNumber == 3)
                {
                    lblDay2.Text = "1";
                    lblDay3.Text = "2";
                    lblDay4.Text = "3";
                    lblDay5.Text = "4";
                    lblDay6.Text = "5";
                    lblDay7.Text = "6";
                    lblDay8.Text = "7";
                    lblDay9.Text = "8";
                    lblDay10.Text = "9";
                    lblDay11.Text = "10";
                    lblDay12.Text = "11";
                    lblDay13.Text = "12";
                    lblDay14.Text = "13";
                    lblDay15.Text = "14";
                    lblDay16.Text = "15";
                    lblDay17.Text = "16";
                    lblDay18.Text = "17";
                    lblDay19.Text = "18";
                    lblDay20.Text = "19";
                    lblDay21.Text = "20";
                    lblDay22.Text = "21";
                    lblDay23.Text = "22";
                    lblDay24.Text = "23";
                    lblDay25.Text = "24";
                    lblDay26.Text = "25";
                    lblDay27.Text = "26";
                    lblDay28.Text = "27";
                    lblDay29.Text = "28";
                    lblDay30.Text = "29";


                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay30.Visible = true;
                    lblDay31.Visible = false;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay30.Visible = true;
                    lnkDay31.Visible = false;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;


                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);


                }
                if (iStartNumber == 4)
                {
                    lblDay3.Text = "1";
                    lblDay4.Text = "2";
                    lblDay5.Text = "3";
                    lblDay6.Text = "4";
                    lblDay7.Text = "5";
                    lblDay8.Text = "6";
                    lblDay9.Text = "7";
                    lblDay10.Text = "8";
                    lblDay11.Text = "9";
                    lblDay12.Text = "10";
                    lblDay13.Text = "11";
                    lblDay14.Text = "12";
                    lblDay15.Text = "13";
                    lblDay16.Text = "14";
                    lblDay17.Text = "15";
                    lblDay18.Text = "16";
                    lblDay19.Text = "17";
                    lblDay20.Text = "18";
                    lblDay21.Text = "19";
                    lblDay22.Text = "20";
                    lblDay23.Text = "21";
                    lblDay24.Text = "22";
                    lblDay25.Text = "23";
                    lblDay26.Text = "24";
                    lblDay27.Text = "25";
                    lblDay28.Text = "26";
                    lblDay29.Text = "27";
                    lblDay30.Text = "28";
                    lblDay31.Text = "29";


                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay31.Visible = true;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay31.Visible = true;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;


                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);

                }
                if (iStartNumber == 5)
                {
                    lblDay4.Text = "1";
                    lblDay5.Text = "2";
                    lblDay6.Text = "3";
                    lblDay7.Text = "4";
                    lblDay8.Text = "5";
                    lblDay9.Text = "6";
                    lblDay10.Text = "7";
                    lblDay11.Text = "8";
                    lblDay12.Text = "9";
                    lblDay13.Text = "10";
                    lblDay14.Text = "11";
                    lblDay15.Text = "12";
                    lblDay16.Text = "13";
                    lblDay17.Text = "14";
                    lblDay18.Text = "15";
                    lblDay19.Text = "16";
                    lblDay20.Text = "17";
                    lblDay21.Text = "18";
                    lblDay22.Text = "19";
                    lblDay23.Text = "20";
                    lblDay24.Text = "21";
                    lblDay25.Text = "22";
                    lblDay26.Text = "23";
                    lblDay27.Text = "24";
                    lblDay28.Text = "25";
                    lblDay29.Text = "26";
                    lblDay30.Text = "27";
                    lblDay31.Text = "28";
                    lblDay32.Text = "29";


                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay32.Visible = true;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay32.Visible = true;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);

                }
                if (iStartNumber == 6)
                {
                    lblDay5.Text = "1";
                    lblDay6.Text = "2";
                    lblDay7.Text = "3";
                    lblDay8.Text = "4";
                    lblDay9.Text = "5";
                    lblDay10.Text = "6";
                    lblDay11.Text = "7";
                    lblDay12.Text = "8";
                    lblDay13.Text = "9";
                    lblDay14.Text = "10";
                    lblDay15.Text = "11";
                    lblDay16.Text = "12";
                    lblDay17.Text = "13";
                    lblDay18.Text = "14";
                    lblDay19.Text = "15";
                    lblDay20.Text = "16";
                    lblDay21.Text = "17";
                    lblDay22.Text = "18";
                    lblDay23.Text = "19";
                    lblDay24.Text = "20";
                    lblDay25.Text = "21";
                    lblDay26.Text = "22";
                    lblDay27.Text = "23";
                    lblDay28.Text = "24";
                    lblDay29.Text = "25";
                    lblDay30.Text = "26";
                    lblDay31.Text = "27";
                    lblDay32.Text = "28";
                    lblDay33.Text = "29";



                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay4.Visible = false;
                    lblDay33.Visible = true;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay4.Visible = false;
                    lnkDay33.Visible = true;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay33.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay33.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);


                }
                if (iStartNumber == 7)
                {
                    lblDay6.Text = "1";
                    lblDay7.Text = "2";
                    lblDay8.Text = "3";
                    lblDay9.Text = "4";
                    lblDay10.Text = "5";
                    lblDay11.Text = "6";
                    lblDay12.Text = "7";
                    lblDay13.Text = "8";
                    lblDay14.Text = "9";
                    lblDay15.Text = "10";
                    lblDay16.Text = "11";
                    lblDay17.Text = "12";
                    lblDay18.Text = "13";
                    lblDay19.Text = "14";
                    lblDay20.Text = "15";
                    lblDay21.Text = "16";
                    lblDay22.Text = "17";
                    lblDay23.Text = "18";
                    lblDay24.Text = "19";
                    lblDay25.Text = "20";
                    lblDay26.Text = "21";
                    lblDay27.Text = "22";
                    lblDay28.Text = "23";
                    lblDay29.Text = "24";
                    lblDay30.Text = "25";
                    lblDay31.Text = "26";
                    lblDay32.Text = "27";
                    lblDay33.Text = "28";
                    lblDay34.Text = "29";

                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay4.Visible = false;
                    lblDay5.Visible = false;
                    lblDay34.Visible = true;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay4.Visible = false;
                    lnkDay5.Visible = false;
                    lnkDay34.Visible = true;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay33.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay33.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay34.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay34.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);


                }
            }
            if (days == 28)
            {
                if (iStartNumber == 1)
                {
                    lblDay0.Text = "1";
                    lblDay1.Text = "2";
                    lblDay2.Text = "3";
                    lblDay3.Text = "4";
                    lblDay4.Text = "5";
                    lblDay5.Text = "6";
                    lblDay6.Text = "7";
                    lblDay7.Text = "8";
                    lblDay8.Text = "9";
                    lblDay9.Text = "10";
                    lblDay10.Text = "11";
                    lblDay11.Text = "12";
                    lblDay12.Text = "13";
                    lblDay13.Text = "14";
                    lblDay14.Text = "15";
                    lblDay15.Text = "16";
                    lblDay16.Text = "17";
                    lblDay17.Text = "18";
                    lblDay18.Text = "19";
                    lblDay19.Text = "20";
                    lblDay20.Text = "21";
                    lblDay21.Text = "22";
                    lblDay22.Text = "23";
                    lblDay23.Text = "24";
                    lblDay24.Text = "25";
                    lblDay25.Text = "26";
                    lblDay26.Text = "27";
                    lblDay27.Text = "28";


                    lblDay28.Visible = false;
                    lblDay29.Visible = false;
                    lblDay30.Visible = false;
                    lblDay31.Visible = false;
                    lblDay31.Visible = false;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;


                    lnkDay28.Visible = false;
                    lnkDay29.Visible = false;
                    lnkDay30.Visible = false;
                    lnkDay31.Visible = false;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay0.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay0.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay1.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay1.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);

                }
                if (iStartNumber == 2)
                {
                    lblDay1.Text = "1";
                    lblDay2.Text = "2";
                    lblDay3.Text = "3";
                    lblDay4.Text = "4";
                    lblDay5.Text = "5";
                    lblDay6.Text = "6";
                    lblDay7.Text = "7";
                    lblDay8.Text = "8";
                    lblDay9.Text = "9";
                    lblDay10.Text = "10";
                    lblDay11.Text = "11";
                    lblDay12.Text = "12";
                    lblDay13.Text = "13";
                    lblDay14.Text = "14";
                    lblDay15.Text = "15";
                    lblDay16.Text = "16";
                    lblDay17.Text = "17";
                    lblDay18.Text = "18";
                    lblDay19.Text = "19";
                    lblDay20.Text = "20";
                    lblDay21.Text = "21";
                    lblDay22.Text = "22";
                    lblDay23.Text = "23";
                    lblDay24.Text = "24";
                    lblDay25.Text = "25";
                    lblDay26.Text = "26";
                    lblDay27.Text = "27";
                    lblDay28.Text = "28";


                    lblDay0.Visible = false;
                    lblDay28.Visible = true;
                    lblDay29.Visible = false;
                    lblDay30.Visible = false;
                    lblDay31.Visible = false;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay28.Visible = true;
                    lnkDay29.Visible = false;
                    lnkDay30.Visible = false;
                    lnkDay31.Visible = false;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay1.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay1.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);

                }
                if (iStartNumber == 3)
                {
                    lblDay2.Text = "1";
                    lblDay3.Text = "2";
                    lblDay4.Text = "3";
                    lblDay5.Text = "4";
                    lblDay6.Text = "5";
                    lblDay7.Text = "6";
                    lblDay8.Text = "7";
                    lblDay9.Text = "8";
                    lblDay10.Text = "9";
                    lblDay11.Text = "10";
                    lblDay12.Text = "11";
                    lblDay13.Text = "12";
                    lblDay14.Text = "13";
                    lblDay15.Text = "14";
                    lblDay16.Text = "15";
                    lblDay17.Text = "16";
                    lblDay18.Text = "17";
                    lblDay19.Text = "18";
                    lblDay20.Text = "19";
                    lblDay21.Text = "20";
                    lblDay22.Text = "21";
                    lblDay23.Text = "22";
                    lblDay24.Text = "23";
                    lblDay25.Text = "24";
                    lblDay26.Text = "25";
                    lblDay27.Text = "26";
                    lblDay28.Text = "27";
                    lblDay29.Text = "28";


                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay29.Visible = true;
                    lblDay30.Visible = false;
                    lblDay31.Visible = false;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay29.Visible = true;
                    lnkDay30.Visible = false;
                    lnkDay31.Visible = false;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;


                    lnkDay2.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay2.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);

                }
                if (iStartNumber == 4)
                {
                    lblDay3.Text = "1";
                    lblDay4.Text = "2";
                    lblDay5.Text = "3";
                    lblDay6.Text = "4";
                    lblDay7.Text = "5";
                    lblDay8.Text = "6";
                    lblDay9.Text = "7";
                    lblDay10.Text = "8";
                    lblDay11.Text = "9";
                    lblDay12.Text = "10";
                    lblDay13.Text = "11";
                    lblDay14.Text = "12";
                    lblDay15.Text = "13";
                    lblDay16.Text = "14";
                    lblDay17.Text = "15";
                    lblDay18.Text = "16";
                    lblDay19.Text = "17";
                    lblDay20.Text = "18";
                    lblDay21.Text = "19";
                    lblDay22.Text = "20";
                    lblDay23.Text = "21";
                    lblDay24.Text = "22";
                    lblDay25.Text = "23";
                    lblDay26.Text = "24";
                    lblDay27.Text = "25";
                    lblDay28.Text = "26";
                    lblDay29.Text = "27";
                    lblDay30.Text = "28";


                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay30.Visible = true;
                    lblDay31.Visible = false;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay30.Visible = true;
                    lnkDay31.Visible = false;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;


                    lnkDay3.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay3.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);

                }
                if (iStartNumber == 5)
                {
                    lblDay4.Text = "1";
                    lblDay5.Text = "2";
                    lblDay6.Text = "3";
                    lblDay7.Text = "4";
                    lblDay8.Text = "5";
                    lblDay9.Text = "6";
                    lblDay10.Text = "7";
                    lblDay11.Text = "8";
                    lblDay12.Text = "9";
                    lblDay13.Text = "10";
                    lblDay14.Text = "11";
                    lblDay15.Text = "12";
                    lblDay16.Text = "13";
                    lblDay17.Text = "14";
                    lblDay18.Text = "15";
                    lblDay19.Text = "16";
                    lblDay20.Text = "17";
                    lblDay21.Text = "18";
                    lblDay22.Text = "19";
                    lblDay23.Text = "20";
                    lblDay24.Text = "21";
                    lblDay25.Text = "22";
                    lblDay26.Text = "23";
                    lblDay27.Text = "24";
                    lblDay28.Text = "25";
                    lblDay29.Text = "26";
                    lblDay30.Text = "27";
                    lblDay31.Text = "28";


                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay31.Visible = true;
                    lblDay32.Visible = false;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay31.Visible = true;
                    lnkDay32.Visible = false;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay4.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay4.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);

                }
                if (iStartNumber == 6)
                {
                    lblDay5.Text = "1";
                    lblDay6.Text = "2";
                    lblDay7.Text = "3";
                    lblDay8.Text = "4";
                    lblDay9.Text = "5";
                    lblDay10.Text = "6";
                    lblDay11.Text = "7";
                    lblDay12.Text = "8";
                    lblDay13.Text = "9";
                    lblDay14.Text = "10";
                    lblDay15.Text = "11";
                    lblDay16.Text = "12";
                    lblDay17.Text = "13";
                    lblDay18.Text = "14";
                    lblDay19.Text = "15";
                    lblDay20.Text = "16";
                    lblDay21.Text = "17";
                    lblDay22.Text = "18";
                    lblDay23.Text = "19";
                    lblDay24.Text = "20";
                    lblDay25.Text = "21";
                    lblDay26.Text = "22";
                    lblDay27.Text = "23";
                    lblDay28.Text = "24";
                    lblDay29.Text = "25";
                    lblDay30.Text = "26";
                    lblDay31.Text = "27";
                    lblDay32.Text = "28";



                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay4.Visible = false;
                    lblDay32.Visible = true;
                    lblDay33.Visible = false;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay4.Visible = false;
                    lnkDay32.Visible = true;
                    lnkDay33.Visible = false;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);


                }
                if (iStartNumber == 7)
                {
                    lblDay6.Text = "1";
                    lblDay7.Text = "2";
                    lblDay8.Text = "3";
                    lblDay9.Text = "4";
                    lblDay10.Text = "5";
                    lblDay11.Text = "6";
                    lblDay12.Text = "7";
                    lblDay13.Text = "8";
                    lblDay14.Text = "9";
                    lblDay15.Text = "10";
                    lblDay16.Text = "11";
                    lblDay17.Text = "12";
                    lblDay18.Text = "13";
                    lblDay19.Text = "14";
                    lblDay20.Text = "15";
                    lblDay21.Text = "16";
                    lblDay22.Text = "17";
                    lblDay23.Text = "18";
                    lblDay24.Text = "19";
                    lblDay25.Text = "20";
                    lblDay26.Text = "21";
                    lblDay27.Text = "22";
                    lblDay28.Text = "23";
                    lblDay29.Text = "24";
                    lblDay30.Text = "25";
                    lblDay31.Text = "26";
                    lblDay32.Text = "27";
                    lblDay33.Text = "28";


                    lblDay0.Visible = false;
                    lblDay1.Visible = false;
                    lblDay2.Visible = false;
                    lblDay3.Visible = false;
                    lblDay4.Visible = false;
                    lblDay5.Visible = false;
                    lblDay33.Visible = true;
                    lblDay34.Visible = false;
                    lblDay35.Visible = false;
                    lblDay36.Visible = false;
                    lblDay37.Visible = false;
                    lblDay38.Visible = false;
                    lblDay39.Visible = false;
                    lblDay40.Visible = false;
                    lblDay41.Visible = false;

                    lnkDay0.Visible = false;
                    lnkDay1.Visible = false;
                    lnkDay2.Visible = false;
                    lnkDay3.Visible = false;
                    lnkDay4.Visible = false;
                    lnkDay5.Visible = false;
                    lnkDay33.Visible = true;
                    lnkDay34.Visible = false;
                    lnkDay35.Visible = false;
                    lnkDay36.Visible = false;
                    lnkDay37.Visible = false;
                    lnkDay38.Visible = false;
                    lnkDay39.Visible = false;
                    lnkDay40.Visible = false;
                    lnkDay41.Visible = false;

                    lnkDay5.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay5.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay6.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay6.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay7.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay7.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay8.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay8.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay9.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay9.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay10.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay10.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay11.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay11.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay12.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay12.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay13.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay13.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay14.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay14.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay15.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay15.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay16.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay16.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay17.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay17.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay18.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay18.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay19.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay19.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay20.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay20.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay21.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay21.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay22.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay22.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay23.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay23.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay24.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay24.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay25.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay25.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay26.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay26.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay27.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay27.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay28.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay28.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay29.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay29.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay30.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay30.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay31.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay31.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay32.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay32.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);
                    lnkDay33.Text = _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + lblDay33.Text + "/" + ddlYear.SelectedValue,doctorName, patientName);

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

    private void VisibleLabels()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblDay0.Visible = true;
            lblDay1.Visible = true;
            lblDay2.Visible = true;
            lblDay3.Visible = true;
            lblDay4.Visible = true;
            lblDay5.Visible = true;
            lblDay6.Visible = true;
            lblDay7.Visible = true;
            lblDay8.Visible = true;
             lblDay9.Visible = true;
             lblDay10.Visible = true;
             lblDay11.Visible = true;
             lblDay12.Visible = true;
             lblDay13.Visible = true;
             lblDay14.Visible = true;
             lblDay15.Visible = true;
             lblDay16.Visible = true;
             lblDay17.Visible = true;
             lblDay18.Visible = true;
             lblDay19.Visible = true;
             lblDay20.Visible = true;
             lblDay21.Visible = true;
             lblDay22.Visible = true;
             lblDay23.Visible = true;
             lblDay24.Visible = true;
             lblDay25.Visible = true;
             lblDay26.Visible = true;
             lblDay27.Visible = true;
             lblDay28.Visible = true;
             lblDay29.Visible = true;             
             lblDay30.Visible = true;
             lblDay31.Visible = true;
             lblDay32.Visible = true;
             lblDay33.Visible = true;
             lblDay34.Visible = true;
             lblDay35.Visible = true;
             lblDay36.Visible = true;
             lblDay37.Visible = true;
             lblDay38.Visible = true;
             lblDay39.Visible = true;
             lblDay40.Visible = true;
             lblDay41.Visible = true;
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
    private void VisibleLinkButton()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lnkDay0.Visible = true;
            lnkDay1.Visible = true;
            lnkDay2.Visible = true;
            lnkDay3.Visible = true;
            lnkDay4.Visible = true;
            lnkDay5.Visible = true;
            lnkDay6.Visible = true;
            lnkDay7.Visible = true;
            lnkDay8.Visible = true;
            lnkDay9.Visible = true;
            lnkDay10.Visible = true;
            lnkDay11.Visible = true;
            lnkDay12.Visible = true;
            lnkDay13.Visible = true;
            lnkDay14.Visible = true;
            lnkDay15.Visible = true;
            lnkDay16.Visible = true;
            lnkDay17.Visible = true;
            lnkDay18.Visible = true;
            lnkDay19.Visible = true;
            lnkDay20.Visible = true;
            lnkDay21.Visible = true;
            lnkDay22.Visible = true;
            lnkDay23.Visible = true;
            lnkDay24.Visible = true;
            lnkDay25.Visible = true;
            lnkDay26.Visible = true;
            lnkDay27.Visible = true;
            lnkDay28.Visible = true;
            lnkDay29.Visible = true;
            lnkDay30.Visible = true;
            lnkDay31.Visible = true;
            lnkDay32.Visible = true;
            lnkDay33.Visible = true;
            lnkDay34.Visible = true;
            lnkDay35.Visible = true;
            lnkDay36.Visible = true;
            lnkDay37.Visible = true;
            lnkDay38.Visible = true;
            lnkDay39.Visible = true;
            lnkDay40.Visible = true;
            lnkDay41.Visible = true;
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
    protected void lnkDay5_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay5.Text);
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

    private void ShowLineView(string date)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["CURRENT_DATE"] = ddlMonth.SelectedValue + "/" + date + "/" + ddlYear.SelectedValue;
            Session["CALENDER_STATE"] = "Day";
            Session["CASE_ID"] = txtCaseID.Text;
            Session["CURRENT_TIME"] = null;
            Response.Redirect("Bill_Sys_LineViewDayCalander.aspx", false);
           
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
    protected void lnkDay0_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay0.Text);
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
    protected void lnkDay1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay1.Text);
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
    protected void lnkDay2_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay2.Text);
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
    protected void lnkDay3_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay3.Text);
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
    protected void lnkDay6_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay6.Text);
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
    protected void lnkDay7_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay7.Text);
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
    protected void lnkDay8_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay8.Text);
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
    protected void lnkDay9_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay9.Text);
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
    protected void lnkDay10_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay10.Text);
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
    protected void lnkDay11_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay11.Text);
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
    protected void lnkDay12_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay12.Text);
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
    protected void lnkDay13_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay13.Text);
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
    protected void lnkDay14_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay14.Text);
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
    protected void lnkDay15_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay15.Text);
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
    protected void lnkDay16_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay16.Text);
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
    protected void lnkDay17_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay17.Text);
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
    protected void lnkDay18_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay18.Text);
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
    protected void lnkDay19_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay19.Text);
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
    protected void lnkDay20_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay20.Text);
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
    protected void lnkDay21_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay21.Text);
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
    protected void lnkDay22_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay22.Text);
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
    protected void lnkDay23_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay23.Text);
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
    protected void lnkDay24_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay24.Text);
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
    protected void lnkDay25_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay25.Text);
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
    protected void lnkDay26_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay26.Text);
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
    protected void lnkDay27_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay27.Text);
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
    protected void lnkDay28_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay28.Text);
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
    protected void lnkDay29_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay29.Text);
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
    protected void lnkDay30_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay30.Text);
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
    protected void lnkDay31_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay31.Text);
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
    protected void lnkDay32_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay32.Text);
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
    protected void lnkDay33_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay33.Text);
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
    protected void lnkDay34_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay34.Text);
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
    protected void lnkDay35_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay35.Text);
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
    protected void lnkDay36_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay36.Text);
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
    protected void lnkDay37_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay37.Text);
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
    protected void lnkDay38_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay38.Text);
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
    protected void lnkDay39_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay39.Text);
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
    protected void lnkDay40_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay40.Text);
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
    protected void lnkDay41_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ShowLineView(lblDay41.Text);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            GetMonthCalender(Convert.ToInt16(ddlMonth.SelectedValue), Convert.ToInt16(ddlYear.SelectedValue),txtDoctorSearch.Text,txtProviderSearch.Text);
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
