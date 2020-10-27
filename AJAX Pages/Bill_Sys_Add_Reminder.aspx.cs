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
using Reminders;


public partial class AJAX_Pages_Bill_Sys_Add_Reminder : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            this.con.SourceGrid = grdaccountreminder;
            this.txtSearchBox.SourceGrid = grdaccountreminder;
            this.grdaccountreminder.Page = this.Page;
            this.grdaccountreminder.PageNumberList = this.con;
            extddlRType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            imgbtnStartDate.Attributes.Add("onclick", "cal1x.select(document.forms[0]." + txtStartDate.ClientID + ",'" + imgbtnStartDate.ClientID + "','MM/dd/yyyy'); return false;");
            ImgEndByDate.Attributes.Add("onclick", "cal1x.select(document.forms[0]." + txtEndByDate.ClientID + ",'" + ImgEndByDate.ClientID + "','MM/dd/yyyy'); return false;");

            txtDDays.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            txtWRecur.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            txtMDay.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            txtMMonths.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            txtMEveryMonths.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            txtYDay.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            txtEndAfter.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            txtEndAfter.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");

            txtDDays.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            txtWRecur.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            txtMMonths.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            txtMEveryMonths.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            txtEndAfter.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            txtEndAfter.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");

            txtMDay.Attributes.Add("onblur", "javascript:return DayValidation(this);");
            txtYDay.Attributes.Add("onblur", "javascript:return DayValidation(this);");
            drpYMonth.Attributes.Add("onClick", "javascript:return NewDayValidation();");
           
            if (!IsPostBack)
            {
                fillcontrol();
                grdaccountreminder.XGridBindSearch();
                btnupdate.Visible = false;
                //pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "none");
                ////Daily
                pnlDaily.Style.Add(HtmlTextWriterStyle.Display, "none");
                
                ////Weekly
                pnlWeekly.Style.Add(HtmlTextWriterStyle.Display, "none");

                ////Monthly
                pnlMonthly.Style.Add(HtmlTextWriterStyle.Display, "none");

                ////Yearly
                pnlYearly.Style.Add(HtmlTextWriterStyle.Display, "none");
                
                txtDDays.Text = "1";
                txtWRecur.Text = "1";
                txtMDay.Text = System.DateTime.Now.Day.ToString();  
                txtMMonths.Text = "1";
                txtMEveryMonths.Text = "1";
                txtYDay.Text = System.DateTime.Now.Day.ToString();
                txtStartDate.Text = System.DateTime.Now.ToShortDateString();
                txtEndByDate.Text = System.DateTime.Now.ToShortDateString();
                chkRecurrence.Checked = true;
                chkRecurrence.Attributes.Add("onclick", "return MakeVisibleRecurrence();");
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "mmmmmm", "MakeVisibleRecurrence();", true);
                // btnSave.Attributes.Add("onclick", "return add();");
                btnSave.Attributes.Add("onclick", "return Validate();");
                btnSave.Attributes.Add("onclick", "return ReminderType();");

                pnlRecurrence.Visible = true;
                LoadMonthlyDropdown();
                LoadAssignToListBox();
                LoadYearlyDropdown();
                int iMonth = Convert.ToInt32(System.DateTime.Now.Month.ToString());
                drpYMonth.SelectedIndex = iMonth - 1;
                drpYMonthRecur.SelectedIndex = iMonth - 1;
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                rbtnDEvery.Style.Add(HtmlTextWriterStyle.Display, "block");
                lblDEvery.Style.Add(HtmlTextWriterStyle.Display, "block");
                txtDDays.Style.Add(HtmlTextWriterStyle.Display, "block");
                lblDDays.Style.Add(HtmlTextWriterStyle.Display, "block");
                rbtnDEveryWeekday.Style.Add(HtmlTextWriterStyle.Display, "block");
                lblDEveryweekday.Style.Add(HtmlTextWriterStyle.Display, "block");
                pnlDaily.Style.Add(HtmlTextWriterStyle.Display, "block");
                
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
    public void fillcontrol()
    {

        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtuseridforre.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
       
    }

    public void LoadMonthlyDropdown()
    {
        drpMTerm.Items.Clear();
        drpMTerm.Items.Insert(0, "first");
        drpMTerm.Items.Insert(1, "second");
        drpMTerm.Items.Insert(2, "third");
        drpMTerm.Items.Insert(3, "fourth");
        drpMTerm.Items.Insert(4, "last");
        drpMTerm.SelectedIndex = 0;

        drpMDayRecur.Items.Clear();
        drpMDayRecur.Items.Insert(0, "day");
        drpMDayRecur.Items.Insert(1, "weekday");
        drpMDayRecur.Items.Insert(2, "weekend day");
        drpMDayRecur.Items.Insert(3, "sunday");
        drpMDayRecur.Items.Insert(4, "monday");
        drpMDayRecur.Items.Insert(5, "tuesday");
        drpMDayRecur.Items.Insert(6, "wednesday");
        drpMDayRecur.Items.Insert(7, "thursday");
        drpMDayRecur.Items.Insert(8, "friday");
        drpMDayRecur.Items.Insert(9, "saturday");
        drpMDayRecur.SelectedIndex = 0;
    }

    public void LoadAssignToListBox()
    {
        ReminderBO _obj = new ReminderBO();
        string strCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        DataSet ds = new DataSet();
        ds = _obj.GetAddReminderList(strCompanyID);
        lsbAssignTo.DataSource = ds.Tables[0];
        lsbAssignTo.DataTextField = "Description";
        lsbAssignTo.DataValueField = "Code";
        lsbAssignTo.DataBind();
        for (int i = 0; i < lsbAssignTo.Items.Count; i++)
        {
            if (lsbAssignTo.Items[i].Text == ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME)
            {
                lsbAssignTo.SelectedIndex = i;
            }
        }
    }

    public void LoadYearlyDropdown()
     {
        drpYTerm.Items.Clear();
        drpYTerm.Items.Insert(0, "first");
        drpYTerm.Items.Insert(1, "second");
        drpYTerm.Items.Insert(2, "third");
        drpYTerm.Items.Insert(3, "fourth");
        drpYTerm.Items.Insert(4, "last");
        drpYTerm.SelectedIndex = 0;

        drpYDayRecur.Items.Clear();
        drpYDayRecur.Items.Insert(0, "day");
        drpYDayRecur.Items.Insert(1, "weekday");
        drpYDayRecur.Items.Insert(2, "weekend day");
        drpYDayRecur.Items.Insert(3, "sunday");
        drpYDayRecur.Items.Insert(4, "monday");
        drpYDayRecur.Items.Insert(5, "tuesday");
        drpYDayRecur.Items.Insert(6, "wednesday");
        drpYDayRecur.Items.Insert(7, "thursday");
        drpYDayRecur.Items.Insert(8, "friday");
        drpYDayRecur.Items.Insert(9, "saturday");
        drpYDayRecur.SelectedIndex = 0;

        drpYMonth.Items.Clear();
        drpYMonth.Items.Insert(0, "January");
        drpYMonth.Items.Insert(1, "February");
        drpYMonth.Items.Insert(2, "March");
        drpYMonth.Items.Insert(3, "April");
        drpYMonth.Items.Insert(4, "May");
        drpYMonth.Items.Insert(5, "June");
        drpYMonth.Items.Insert(6, "July");
        drpYMonth.Items.Insert(7, "August");
        drpYMonth.Items.Insert(8, "September");
        drpYMonth.Items.Insert(9, "October");
        drpYMonth.Items.Insert(10, "November");
        drpYMonth.Items.Insert(11, "December");

        drpYMonthRecur.Items.Clear();
        drpYMonthRecur.Items.Insert(0, "January");
        drpYMonthRecur.Items.Insert(1, "February");
        drpYMonthRecur.Items.Insert(2, "March");
        drpYMonthRecur.Items.Insert(3, "April");
        drpYMonthRecur.Items.Insert(4, "May");
        drpYMonthRecur.Items.Insert(5, "June");
        drpYMonthRecur.Items.Insert(6, "July");
        drpYMonthRecur.Items.Insert(7, "August");
        drpYMonthRecur.Items.Insert(8, "September");
        drpYMonthRecur.Items.Insert(9, "October");
        drpYMonthRecur.Items.Insert(10, "November");
        drpYMonthRecur.Items.Insert(11, "December");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        ReminderBO objReminder = null;
        DataSet dsReminder = null;
        string str_description = "";
        string str_assigned_to = "";
        string str_assigned_by = "";
        string str_case_id = "";
        string str_docotr_id = "";
        DateTime dt_start_date;
        DateTime dt_end_date = Convert.ToDateTime(System.DateTime.Now.AddYears(2).ToShortDateString());
        int i_is_recurrence = 0;
        int i_recurrence_type = 0;
        int i_occurrence_end_count = 0;
        int i_day_option = 0;
        int i_d_day_count = 0;
        int i_d_every_weekday = 0;
        int i_w_recur_week_count = 0;
        int i_w_sunday = 0;
        int i_w_monday = 0;
        int i_w_tuesday = 0;
        int i_w_wednesday = 0;
        int i_w_thursday = 0;
        int i_w_friday = 0;
        int i_w_saturday = 0;
        int i_month_option = 0;
        int i_m_day = 0;
        int i_m_month_count = 0;
        int i_m_term = 100;
        int i_m_term_week = 100;
        int i_m_every_month_count = 0;
        int i_year_option = 0;
        int i_y_month = 100;
        int i_y_day = 0;
        int i_y_term = 100;
        int i_y_term_week = 100;
        int i_y_every_month_count = 100;
        int i_end_date_count = 0;
        string strReminderStatus = "RS000000000000000001";

        try
        {
            if (txtReminderDesc.Text.Trim().ToString() == "")
            {
                if (chkRecurrence.Checked == true)
                {
                    pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Note description should not be empty...!!')", true);
                    return;
                }
                else
                {
                    pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "none");
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Note description should not be empty...!!')", true);
                    return;
                }
            }

            if (txtStartDate.Text == "")
            {
                if (chkRecurrence.Checked == true)
                {
                    pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('start date should not be empty...!!')", true);
                    return;
                }
                else
                {
                    pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "none");
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('start date should not be empty...!!')", true);
                    return;
                }
            }



            if (rbtnWeekly.Checked == true)
            {

                if (chkWSunday.Checked == false && chkWMonday.Checked == false && chkWTuesday.Checked == false && chkWWednesday.Checked == false && chkWThursday.Checked == false && chkWFriday.Checked == false && chkWSaturday.Checked == false)
                {
                    pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Plase select week days in weekly recurrence...!!')", true);
                    return;
                }
            }

            str_description = txtReminderDesc.Text.Trim().ToString();

            str_description = str_description.Replace('\n', ' ');
            str_description = str_description.Replace('\r', ' ');

            if (txtUserID.Text != "")
            {
                str_assigned_by = txtUserID.Text.Trim().ToString();
            }
            dt_start_date = Convert.ToDateTime(txtStartDate.Text.Trim().ToString());

            str_assigned_to = lsbAssignTo.SelectedValue.ToString();

            if (chkRecurrence.Checked == true)
            {
                i_is_recurrence = 1;
                if (rbtnNoEndDate.Checked == true)
                {
                    dt_end_date = Convert.ToDateTime(System.DateTime.Now.AddYears(2).ToShortDateString());
                }
                else if (rbtnEndBy.Checked == true)
                {
                    dt_end_date = Convert.ToDateTime(txtEndByDate.Text.Trim().ToString());
                }
                else if (rbtnEndAfter.Checked == true)
                {
                    i_recurrence_type = 4;

                    i_occurrence_end_count = Convert.ToInt32(txtEndAfter.Text.Trim().ToString());
                    dt_end_date = Convert.ToDateTime(txtStartDate.Text.Trim().ToString()).AddDays(Convert.ToDouble(i_occurrence_end_count));
                }

                if (dt_start_date.ToString() != "" && dt_end_date.ToString() != "")
                {
                    if (dt_start_date > dt_end_date)
                    {
                        pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Reminder start date should not be greated than reminder end date...!!')", true);
                        return;
                    }
                }

                if (rbtnDaily.Checked == true)
                {
                    i_recurrence_type = 0;

                    if (rbtnDEvery.Checked == true)
                    {
                        i_d_day_count = Convert.ToInt32(txtDDays.Text.Trim().ToString());
                        i_day_option = 0;
                    }
                    else
                    {
                        i_d_every_weekday = 1;
                        i_day_option = 1;
                    }
                }
                else if (rbtnWeekly.Checked == true)
                {
                    i_recurrence_type = 1;

                    i_w_recur_week_count = Convert.ToInt32(txtWRecur.Text.Trim().ToString());
                    if (chkWSunday.Checked == true)
                    {
                        i_w_sunday = 1;
                    }
                    if (chkWMonday.Checked == true)
                    {
                        i_w_monday = 1;
                    }
                    if (chkWTuesday.Checked == true)
                    {
                        i_w_tuesday = 1;
                    }
                    if (chkWWednesday.Checked == true)
                    {
                        i_w_wednesday = 1;
                    }
                    if (chkWThursday.Checked == true)
                    {
                        i_w_thursday = 1;
                    }
                    if (chkWFriday.Checked == true)
                    {
                        i_w_friday = 1;
                    }
                    if (chkWSaturday.Checked == true)
                    {
                        i_w_saturday = 1;
                    }
                }
                else if (rbtnMonthly.Checked == true)
                {
                    i_recurrence_type = 2;

                    if (rbtnMDay.Checked == true)
                    {
                        i_month_option = 0;
                        i_m_day = Convert.ToInt32(txtMDay.Text.Trim().ToString());
                        i_m_month_count = Convert.ToInt32(txtMMonths.Text.Trim().ToString());
                    }
                    else
                    {
                        i_month_option = 1;
                        i_m_term = drpMTerm.SelectedIndex;
                        i_m_term_week = drpMDayRecur.SelectedIndex;
                        i_m_every_month_count = Convert.ToInt32(txtMEveryMonths.Text.Trim().ToString());
                    }
                }
                else if (rbtnYearly.Checked == true)
                {
                    i_recurrence_type = 3;

                    if (rbtnYEvery.Checked == true)
                    {
                        i_year_option = 0;
                        i_y_month = drpYMonth.SelectedIndex;
                        i_y_day = Convert.ToInt32(txtYDay.Text.Trim().ToString());
                    }
                    else
                    {
                        i_year_option = 1;
                        i_y_term = drpYTerm.SelectedIndex;
                        i_y_term_week = drpYDayRecur.SelectedIndex;
                        i_y_every_month_count = drpYMonthRecur.SelectedIndex;
                    }
                }

                if (rbtnNoEndDate.Checked == true)
                {
                    i_end_date_count = 1;
                }
                else if (rbtnEndBy.Checked == true)
                {
                    i_end_date_count = 0;
                
                }

               

                objReminder = new ReminderBO();
                if (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]) != null)
                {
                    str_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                }
                if (Session["SZ_REMINDER_DOCTOR_ID"] != null)
                {
                    str_docotr_id = Session["SZ_REMINDER_DOCTOR_ID"].ToString();
                }
                string Assign_To = "";
                string strCompany_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;


                foreach (ListItem listitem in lsbAssignTo.Items)
                {
                    if (listitem.Selected)
                    {
                        Assign_To = listitem.Value;
                        dsReminder = objReminder.SetReminderDetailsAddaccount(str_description, Assign_To, str_assigned_by, strReminderStatus, dt_start_date, dt_end_date, i_is_recurrence, i_recurrence_type, i_occurrence_end_count, i_day_option, i_d_day_count, i_d_every_weekday, i_w_recur_week_count, i_w_sunday, i_w_monday, i_w_tuesday, i_w_wednesday, i_w_thursday, i_w_friday, i_w_saturday, i_month_option, i_m_day, i_m_month_count, i_m_term, i_m_term_week, i_m_every_month_count, i_year_option, i_y_month, i_y_day, i_y_term, i_y_term_week, i_y_every_month_count, str_docotr_id, "", strCompany_id, "LOGIN", extddlRType.Text, extddlRType.Selected_Text.ToString(), i_end_date_count);
                    }
                }

            }
            else
            {
                i_occurrence_end_count = 1;
                dt_end_date = Convert.ToDateTime(txtStartDate.Text.Trim().ToString());

                objReminder = new ReminderBO();
                if (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]) != null)
                {
                    str_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                }

                if (Session["SZ_REMINDER_DOCTOR_ID"] != null)
                {
                    str_docotr_id = Session["SZ_REMINDER_DOCTOR_ID"].ToString();
                }
                string AssignTo = "";
                string strCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                foreach (ListItem listitem in lsbAssignTo.Items)
                {
                    if (listitem.Selected)
                    {
                        AssignTo = listitem.Value;
                        dsReminder = objReminder.SetReminderDetailsAddaccount(str_description, AssignTo, str_assigned_by, strReminderStatus, dt_start_date, dt_end_date, i_is_recurrence, i_recurrence_type, i_occurrence_end_count, i_day_option, i_d_day_count, i_d_every_weekday, i_w_recur_week_count, i_w_sunday, i_w_monday, i_w_tuesday, i_w_wednesday, i_w_thursday, i_w_friday, i_w_saturday, i_month_option, i_m_day, i_m_month_count, i_m_term, i_m_term_week, i_m_every_month_count, i_year_option, i_y_month, i_y_day, i_y_term, i_y_term_week, i_y_every_month_count, str_docotr_id, "", strCompanyID, "LOGIN", extddlRType.Text, extddlRType.Selected_Text.ToString(), i_end_date_count);

                    }

                }


            }
            if (dsReminder.Tables.Count > 0)
            {
                if (dsReminder.Tables[0].Rows.Count > 0)
                {
                    if (dsReminder.Tables[0].Rows[0]["result"].ToString() == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues1();alert('Reminder details added successfully...!!')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues1();alert('Failed to add reminder details..!!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues1();alert('Failed to add reminder details..!!')", true);
            }

          
            grdaccountreminder.XGridBindSearch();
            pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
            pnlDaily.Style.Add(HtmlTextWriterStyle.Display, "block");

            ////Weekly
            pnlWeekly.Style.Add(HtmlTextWriterStyle.Display, "block");

            ////Monthly
            pnlMonthly.Style.Add(HtmlTextWriterStyle.Display, "block");

            ////Yearly
            pnlYearly.Style.Add(HtmlTextWriterStyle.Display, "block");
            rbtnDEvery.Style.Add(HtmlTextWriterStyle.Display, "block");
            lblDEvery.Style.Add(HtmlTextWriterStyle.Display, "block");
            txtDDays.Style.Add(HtmlTextWriterStyle.Display, "block");
            lblDDays.Style.Add(HtmlTextWriterStyle.Display, "block");
            rbtnDEveryWeekday.Style.Add(HtmlTextWriterStyle.Display, "block");
            lblDEveryweekday.Style.Add(HtmlTextWriterStyle.Display, "block");
            pnlDaily.Style.Add(HtmlTextWriterStyle.Display, "block");
          
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
            objReminder = null;
            dsReminder = null;
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void grdaccountreminder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            hdncheck.Value = "Edit";
            btnSave.Visible = false;
            btnupdate.Visible = true;
            int i = Convert.ToInt32(e.CommandArgument.ToString());
            txtReminderID.Text = grdaccountreminder.DataKeys[i]["I_REMINDER_ID"].ToString();
            extddlRType.Text = grdaccountreminder.DataKeys[i]["sz_reminder_type_id"].ToString();
            txtReminderDesc.Text = grdaccountreminder.DataKeys[i]["SZ_REMINDER_DESC"].ToString();
            txtStartDate.Text = grdaccountreminder.DataKeys[i]["dt_start_date"].ToString();
            string date_count = grdaccountreminder.DataKeys[i]["i_date_count"].ToString();
            string chkrecurrenace = grdaccountreminder.DataKeys[i]["is_recurrence"].ToString();
            lsbAssignTo.Text = grdaccountreminder.DataKeys[i]["SZ_REMINDER_ASSIGN_TO"].ToString();
            if (chkrecurrenace == "1")
            {
                chkRecurrence.Checked = true;
                pnlRecurrence.Visible = true;
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "abx", "MakeVisibleRecurrence();", true);
            }
            else
            {
                chkRecurrence.Checked = false;
            }
            string szrecurrancetype = grdaccountreminder.DataKeys[i]["recurrence_type"].ToString();

            if(szrecurrancetype=="0")
            {
                string d_day_option = grdaccountreminder.DataKeys[i]["d_day_option"].ToString();
                if (d_day_option == "0")
                {
                    rbtnDEvery.Checked = true;
                }
                else
                {
                    rbtnDEvery.Checked = false;
                }
                txtDDays.Text = grdaccountreminder.DataKeys[i]["d_day_count"].ToString();
                string d_every_weekday = grdaccountreminder.DataKeys[i]["d_every_weekday"].ToString();
                if (d_every_weekday == "1")
                {
                    rbtnDEveryWeekday.Checked = true;
                }
                else
                {
                    rbtnDEveryWeekday.Checked = false;
                }
               
                pnlRecurrence.Visible = true;
                
                rbtnDaily.Checked = true;
                rbtnMonthly.Checked = false;
                rbtnWeekly.Checked = false;
                pnlDaily.Visible=true;
                hdncheck.Value = "Edit";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "abxc", "ReminderTypeBasedVisibleNew();", true);
            }
            else if (szrecurrancetype == "1")
            {
                 txtWRecur.Text = grdaccountreminder.DataKeys[i]["w_recur_week_count"].ToString();
                 string w_sunday = grdaccountreminder.DataKeys[i]["w_sunday"].ToString();
                 string w_monday = grdaccountreminder.DataKeys[i]["w_monday"].ToString();
                 string w_tuesday = grdaccountreminder.DataKeys[i]["w_tuesday"].ToString();
                 string w_wednesday = grdaccountreminder.DataKeys[i]["w_wednesday"].ToString();
                 string w_thursday = grdaccountreminder.DataKeys[i]["w_thursday"].ToString();
                 string w_friday = grdaccountreminder.DataKeys[i]["w_friday"].ToString();
                 string w_saturday = grdaccountreminder.DataKeys[i]["w_saturday"].ToString();

                 if (w_sunday == "1")
                 {
                     chkWSunday.Checked = true;
                 }
                 else if (w_sunday == "0")
                 {
                     chkWSunday.Checked = false;
                 }
                 if (w_monday == "1")
                 {
                     chkWMonday.Checked = true;
                 }
                 else if (w_monday == "0")
                 {
                     chkWMonday.Checked = false;
                 }
                 if (w_tuesday == "1")
                 {
                     chkWTuesday.Checked = true;
                 }
                 else if (w_tuesday == "0")
                 {
                     chkWTuesday.Checked = false;
                 }
                 if (w_wednesday == "1")
                 {
                     chkWWednesday.Checked = true;
                 }
                 else if (w_wednesday == "0")
                 {
                     chkWWednesday.Checked = false;
                 }
                 if (w_thursday == "1")
                 {
                     chkWThursday.Checked = true;
                 }
                 else
                 {
                     chkWThursday.Checked = false;
                 }
                 if (w_friday == "1")
                 {
                     chkWFriday.Checked = true;
                 }
                 else
                 {
                     chkWFriday.Checked = false;
                 }
                 if (w_saturday == "1")
                 {
                     chkWSaturday.Checked = true;
                 }
                 else
                 {
                     chkWSaturday.Checked = false;
                 }
                pnlWeekly.Visible = true;
                rbtnDaily.Checked = false;
                rbtnMonthly.Checked = false;
                rbtnWeekly.Checked=true;
                hdncheck.Value = "Edit";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "abxcdd", "ReminderTypeBasedVisibleNew();", true);
                
                
                
            }
            else if (szrecurrancetype == "2")
            {
                //hdncheck.Value = "Edit";
                pnlMonthly.Visible = true;
                pnlMonthly.Style.Add(HtmlTextWriterStyle.Display, "block");
                rbtnMonthly.Style.Add(HtmlTextWriterStyle.Display, "block");
                txtMDay.Style.Add(HtmlTextWriterStyle.Display, "block");
                txtMMonths.Style.Add(HtmlTextWriterStyle.Display, "block");
                //drpYTerm.Style.Add(HtmlTextWriterStyle.Display, "block");
                //drpYDayRecur.Style.Add(HtmlTextWriterStyle.Display, "block");
                txtMEveryMonths.Style.Add(HtmlTextWriterStyle.Display, "block");
                //txtMDay.Text = "";
                //rbtnMThe.Checked = true;
                rbtnMonthly.Checked = true;
                pnlDaily.Style.Add(HtmlTextWriterStyle.Display, "none");
                pnlWeekly.Style.Add(HtmlTextWriterStyle.Display, "none");
                string m_month_option = grdaccountreminder.DataKeys[i]["m_month_option"].ToString();
                if(m_month_option =="0")
                {
                    rbtnMDay.Checked = true;
                    txtMDay.Text = grdaccountreminder.DataKeys[i]["m_day"].ToString();
                    txtMMonths.Text = grdaccountreminder.DataKeys[i]["m_month_count"].ToString();
                    drpMTerm.SelectedIndex = 0;
                    drpMDayRecur.SelectedIndex = 0;
                    txtMEveryMonths.Text = "1";
                    
                   
                }
                else if (m_month_option == "1")
                {
                    //rbtnMThe.Checked = true;
                    hdncheck.Value = "Edit";
                    string mterm = (grdaccountreminder.DataKeys[i]["m_term"].ToString());
                    string mtremweek = (grdaccountreminder.DataKeys[i]["m_term_week"].ToString());
                    if (mterm == "0")
                    {
                        drpMTerm.Text = "first";
                    }
                    else if (mterm == "1")
                    {
                        drpMTerm.Text = "second";
                                         
                    }
                     else if (mterm == "2")
                    {

                        drpMTerm.Text = "third";
                    }
                     else if (mterm == "3")
                    {

                        drpMTerm.Text = "fourth";
                    }
                     else if (mterm == "4")
                    {
                        drpMTerm.Text = "last";
                    
                    }


                    if (mtremweek == "0")
                    {
                        drpMDayRecur.Text = "day";
                    }
                    else if (mtremweek == "1")
                    {
                        drpMDayRecur.Text = "weekday";
                    }
                    else if (mtremweek == "2")
                    {
                        drpMDayRecur.Text = "weekend day";
                    }
                    else if (mtremweek == "3")
                    {
                        drpMDayRecur.Text = "sunday";
                    }
                    else if (mtremweek == "4")
                    {
                        drpMDayRecur.Text = "monday";
                    }
                    else if (mtremweek == "5")
                    {
                        drpMDayRecur.Text = "tuesday";

                    }
                    else if (mtremweek == "6")
                    {
                        drpMDayRecur.Text = "wednesday";
                    }
                    else if (mtremweek == "7")
                    {
                        drpMDayRecur.Text = "thursday";
                    }
                    else if (mtremweek == "8")
                    {
                        drpMDayRecur.Text = "friday";
                    }
                    else if (mtremweek == "9")
                    {
                        drpMDayRecur.Text = "saturday";
                    }
                    txtMMonths.Text = grdaccountreminder.DataKeys[i]["m_every_month_count"].ToString();
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "abxcdd", "ReminderTypeBasedVisibleNew();", true);
                }
               
            
            }

            if (date_count == "1")
            {
                rbtnNoEndDate.Checked = true;
            }
            else if (date_count == "0")
            {
                rbtnEndBy.Checked = true;
                
                txtEndByDate.Text = grdaccountreminder.DataKeys[i]["dt_reminder_date"].ToString();

            }

           



        
        }
    }
    protected void grdaccountreminder_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        ReminderBO objReminder = null;
        objReminder = new ReminderBO();
        try
        {

            for (int i = 0; i < grdaccountreminder.Rows.Count; i++)
            {
                CheckBox chkDelete = (CheckBox)grdaccountreminder.Rows[i].FindControl("chkDelete");
                string i_reminder_id=grdaccountreminder.DataKeys[i]["I_REMINDER_ID"].ToString();
                if (chkDelete.Checked)
                {
                    objReminder.RemoveAddReminder(i_reminder_id, txtCompanyID.Text);
                }
            
            }
            ScriptManager.RegisterClientScriptBlock(btndelete, typeof(Button), "Msg", "ClearValues();alert('Reminder Delete successfully...!!')", true);
            grdaccountreminder.XGridBindSearch();
            btnSave.Visible = true;
            btnupdate.Visible = false;
        
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        ReminderBO objReminder = null;
        //DataSet dsReminder = null;
        string str_description = "";
        string str_assigned_to = "";
        string str_assigned_by = "";
        string str_case_id = "";
        string str_docotr_id = "";
        DateTime dt_start_date;
        DateTime dt_end_date = Convert.ToDateTime(System.DateTime.Now.AddYears(2).ToShortDateString());
        int i_is_recurrence = 0;
        int i_recurrence_type = 0;
        int i_occurrence_end_count = 0;
        int i_day_option = 0;
        int i_d_day_count = 0;
        int i_d_every_weekday = 0;
        int i_w_recur_week_count = 0;
        int i_w_sunday = 0;
        int i_w_monday = 0;
        int i_w_tuesday = 0;
        int i_w_wednesday = 0;
        int i_w_thursday = 0;
        int i_w_friday = 0;
        int i_w_saturday = 0;
        int i_month_option = 0;
        int i_m_day = 0;
        int i_m_month_count = 0;
        int i_m_term = 100;
        int i_m_term_week = 100;
        int i_m_every_month_count = 0;
        int i_year_option = 0;
        int i_y_month = 100;
        int i_y_day = 0;
        int i_y_term = 100;
        int i_y_term_week = 100;
        int i_y_every_month_count = 100;
        int i_end_date_count = 0;
         string strReminderStatus = "RS000000000000000001";

        try
        {
            
            if (txtReminderDesc.Text.Trim().ToString() == "")
            {
                if (chkRecurrence.Checked == true)
                {
                    pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Note description should not be empty...!!')", true);
                    return;
                }
                else
                {
                    pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "none");
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Note description should not be empty...!!')", true);
                    return;
                }
            }

            if (txtStartDate.Text == "")
            {
                if (chkRecurrence.Checked == true)
                {
                    pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('start date should not be empty...!!')", true);
                    return;
                }
                else
                {
                    pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "none");
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('start date should not be empty...!!')", true);
                    return;
                }
            }



            if (rbtnWeekly.Checked == true)
            {

                if (chkWSunday.Checked == false && chkWMonday.Checked == false && chkWTuesday.Checked == false && chkWWednesday.Checked == false && chkWThursday.Checked == false && chkWFriday.Checked == false && chkWSaturday.Checked == false)
                {
                    pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Plase select week days in weekly recurrence...!!')", true);
                    return;
                }
            }

            str_description = txtReminderDesc.Text.Trim().ToString();

            str_description = str_description.Replace('\n', ' ');
            str_description = str_description.Replace('\r', ' ');

            if (txtUserID.Text != "")
            {
                str_assigned_by = txtUserID.Text.Trim().ToString();
            }
            dt_start_date = Convert.ToDateTime(txtStartDate.Text.Trim().ToString());

            str_assigned_to = lsbAssignTo.SelectedValue.ToString();

            if (chkRecurrence.Checked == true)
            {
                i_is_recurrence = 1;
                if (rbtnNoEndDate.Checked == true)
                {
                    dt_end_date = Convert.ToDateTime(System.DateTime.Now.AddYears(2).ToShortDateString());
                }
                else if (rbtnEndBy.Checked == true)
                {
                    dt_end_date = Convert.ToDateTime(txtEndByDate.Text.Trim().ToString());
                }
                else if (rbtnEndAfter.Checked == true)
                {
                    i_recurrence_type = 4;

                    i_occurrence_end_count = Convert.ToInt32(txtEndAfter.Text.Trim().ToString());
                    dt_end_date = Convert.ToDateTime(txtStartDate.Text.Trim().ToString()).AddDays(Convert.ToDouble(i_occurrence_end_count));
                }

                if (dt_start_date.ToString() != "" && dt_end_date.ToString() != "")
                {
                    if (dt_start_date > dt_end_date)
                    {
                        pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Reminder start date should not be greated than reminder end date...!!')", true);
                        return;
                    }
                }

                if (rbtnDaily.Checked == true)
                {
                    i_recurrence_type = 0;

                    if (rbtnDEvery.Checked == true)
                    {
                        i_d_day_count = Convert.ToInt32(txtDDays.Text.Trim().ToString());
                        i_day_option = 0;
                    }
                    else
                    {
                        i_d_every_weekday = 1;
                        i_day_option = 1;
                    }
                }
                else if (rbtnWeekly.Checked == true)
                {
                    i_recurrence_type = 1;

                    i_w_recur_week_count = Convert.ToInt32(txtWRecur.Text.Trim().ToString());
                    if (chkWSunday.Checked == true)
                    {
                        i_w_sunday = 1;
                    }
                    if (chkWMonday.Checked == true)
                    {
                        i_w_monday = 1;
                    }
                    if (chkWTuesday.Checked == true)
                    {
                        i_w_tuesday = 1;
                    }
                    if (chkWWednesday.Checked == true)
                    {
                        i_w_wednesday = 1;
                    }
                    if (chkWThursday.Checked == true)
                    {
                        i_w_thursday = 1;
                    }
                    if (chkWFriday.Checked == true)
                    {
                        i_w_friday = 1;
                    }
                    if (chkWSaturday.Checked == true)
                    {
                        i_w_saturday = 1;
                    }
                }
                else if (rbtnMonthly.Checked == true)
                {
                    i_recurrence_type = 2;

                    if (rbtnMDay.Checked == true)
                    {
                        i_month_option = 0;
                        i_m_day = Convert.ToInt32(txtMDay.Text.Trim().ToString());
                        i_m_month_count = Convert.ToInt32(txtMMonths.Text.Trim().ToString());
                    }
                    else
                    {
                        i_month_option = 1;
                        i_m_term = drpMTerm.SelectedIndex;
                        i_m_term_week = drpMDayRecur.SelectedIndex;
                        i_m_every_month_count = Convert.ToInt32(txtMEveryMonths.Text.Trim().ToString());
                    }
                }
                else if (rbtnYearly.Checked == true)
                {
                    i_recurrence_type = 3;

                    if (rbtnYEvery.Checked == true)
                    {
                        i_year_option = 0;
                        i_y_month = drpYMonth.SelectedIndex;
                        i_y_day = Convert.ToInt32(txtYDay.Text.Trim().ToString());
                    }
                    else
                    {
                        i_year_option = 1;
                        i_y_term = drpYTerm.SelectedIndex;
                        i_y_term_week = drpYDayRecur.SelectedIndex;
                        i_y_every_month_count = drpYMonthRecur.SelectedIndex;
                    }
                }


                if (rbtnNoEndDate.Checked == true)
                {
                    i_end_date_count = 1;
                }
                else if (rbtnEndBy.Checked == true)
                {
                    i_end_date_count = 0;

                }
                objReminder = new ReminderBO();
                if (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]) != null)
                {
                    str_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                }
                if (Session["SZ_REMINDER_DOCTOR_ID"] != null)
                {
                    str_docotr_id = Session["SZ_REMINDER_DOCTOR_ID"].ToString();
                }
                string Assign_To = "";
                string strCompany_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;


                foreach (ListItem listitem in lsbAssignTo.Items)
                {
                    if (listitem.Selected)
                    {
                        Assign_To = listitem.Value;
                        objReminder.SetReminderDetailsUpdate(str_description, Assign_To, str_assigned_by, strReminderStatus, dt_start_date, dt_end_date, i_is_recurrence, i_recurrence_type, i_occurrence_end_count, i_day_option, i_d_day_count, i_d_every_weekday, i_w_recur_week_count, i_w_sunday, i_w_monday, i_w_tuesday, i_w_wednesday, i_w_thursday, i_w_friday, i_w_saturday, i_month_option, i_m_day, i_m_month_count, i_m_term, i_m_term_week, i_m_every_month_count, i_year_option, i_y_month, i_y_day, i_y_term, i_y_term_week, i_y_every_month_count, str_docotr_id, "", strCompany_id, "LOGIN", extddlRType.Text, extddlRType.Selected_Text.ToString(), txtReminderID.Text, i_end_date_count);
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues1();alert('Reminder details Update successfully...!!')", true);
                    }
                }

            }
            else
            {
                i_occurrence_end_count = 1;
                dt_end_date = Convert.ToDateTime(txtStartDate.Text.Trim().ToString());

                objReminder = new ReminderBO();
                if (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]) != null)
                {
                    str_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                }

                if (Session["SZ_REMINDER_DOCTOR_ID"] != null)
                {
                    str_docotr_id = Session["SZ_REMINDER_DOCTOR_ID"].ToString();
                }
                string AssignTo = "";
                string strCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                foreach (ListItem listitem in lsbAssignTo.Items)
                {
                    if (listitem.Selected)
                    {
                        AssignTo = listitem.Value;
                        objReminder.SetReminderDetailsUpdate(str_description, AssignTo, str_assigned_by, strReminderStatus, dt_start_date, dt_end_date, i_is_recurrence, i_recurrence_type, i_occurrence_end_count, i_day_option, i_d_day_count, i_d_every_weekday, i_w_recur_week_count, i_w_sunday, i_w_monday, i_w_tuesday, i_w_wednesday, i_w_thursday, i_w_friday, i_w_saturday, i_month_option, i_m_day, i_m_month_count, i_m_term, i_m_term_week, i_m_every_month_count, i_year_option, i_y_month, i_y_day, i_y_term, i_y_term_week, i_y_every_month_count, str_docotr_id, "", strCompanyID, "LOGIN", extddlRType.Text, extddlRType.Selected_Text.ToString(), txtReminderID.Text, i_end_date_count);
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues1();alert('Reminder details Update successfully...!!')", true);

                    }
                   
                }

            }
            grdaccountreminder.XGridBindSearch();
            pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
            pnlDaily.Style.Add(HtmlTextWriterStyle.Display, "block");

            ////Weekly
            pnlWeekly.Style.Add(HtmlTextWriterStyle.Display, "block");

            ////Monthly
            pnlMonthly.Style.Add(HtmlTextWriterStyle.Display, "block");

            ////Yearly
            pnlYearly.Style.Add(HtmlTextWriterStyle.Display, "block");
            rbtnDEvery.Style.Add(HtmlTextWriterStyle.Display, "block");
            lblDEvery.Style.Add(HtmlTextWriterStyle.Display, "block");
            txtDDays.Style.Add(HtmlTextWriterStyle.Display, "block");
            lblDDays.Style.Add(HtmlTextWriterStyle.Display, "block");
            rbtnDEveryWeekday.Style.Add(HtmlTextWriterStyle.Display, "block");
            lblDEveryweekday.Style.Add(HtmlTextWriterStyle.Display, "block");
            pnlDaily.Style.Add(HtmlTextWriterStyle.Display, "block");
            btnupdate.Visible = false;
            btnSave.Visible = true;

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
            objReminder = null;
            //dsReminder = null;
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
