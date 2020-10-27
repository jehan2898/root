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

public partial class UserControl_Reminder : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            extddlRType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //btnSave.Attributes.Add("onclick", "return formValidator('frmSetReminder','txtStartDate,extddlAssignTo,txtReminderDesc');");
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
                pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "none");

                //Daily
                pnlDaily.Style.Add(HtmlTextWriterStyle.Display, "none");

                //Weekly
                pnlWeekly.Style.Add(HtmlTextWriterStyle.Display, "none");

                //Monthly
                pnlMonthly.Style.Add(HtmlTextWriterStyle.Display, "none");

                //Yearly
                pnlYearly.Style.Add(HtmlTextWriterStyle.Display, "none");

                txtDDays.Text = "1";
                txtWRecur.Text = "1";
                txtMDay.Text = System.DateTime.Now.Day.ToString();
                txtMMonths.Text = "1";
                txtMEveryMonths.Text = "1";
                txtYDay.Text = System.DateTime.Now.Day.ToString();
                txtStartDate.Text = System.DateTime.Now.ToShortDateString();
                txtEndByDate.Text = System.DateTime.Now.ToShortDateString();

                chkRecurrence.Attributes.Add("onclick", "return MakeVisibleRecurrence();");
                // btnSave.Attributes.Add("onclick", "return add();");
                btnSave.Attributes.Add("onclick", "return Validate();");
                btnSave.Attributes.Add("onclick", "return ReminderType();");

                LoadMonthlyDropdown();
                LoadAssignToListBox();
                LoadYearlyDropdown();
                int iMonth = Convert.ToInt32(System.DateTime.Now.Month.ToString());
                drpYMonth.SelectedIndex = iMonth - 1;
                drpYMonthRecur.SelectedIndex = iMonth - 1;

                txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            }

        }
        catch (Exception ex)
        {

        }
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
        string strReminderStatus = "RS000000000000000001";
        

        try
        {
            //_saveOperation.WebPage = this.Page;
            //_saveOperation.Xml_File = "AddReminderXML.xml";
            //_saveOperation.SaveMethod();
            //ClearControl(); 

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

              


                objReminder = new ReminderBO();
                if (((Bill_Sys_CaseObject)Session["CASE_OBJECT"])!= null )
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
                        dsReminder = objReminder.SetReminderDetailsAdd(str_description, Assign_To, str_assigned_by, strReminderStatus, dt_start_date, dt_end_date, i_is_recurrence, i_recurrence_type, i_occurrence_end_count, i_day_option, i_d_day_count, i_d_every_weekday, i_w_recur_week_count, i_w_sunday, i_w_monday, i_w_tuesday, i_w_wednesday, i_w_thursday, i_w_friday, i_w_saturday, i_month_option, i_m_day, i_m_month_count, i_m_term, i_m_term_week, i_m_every_month_count, i_year_option, i_y_month, i_y_day, i_y_term, i_y_term_week, i_y_every_month_count, str_docotr_id, str_case_id, strCompany_id, "LOGIN", extddlRType.Text, extddlRType.Selected_Text.ToString());
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
                        dsReminder = objReminder.SetReminderDetailsAdd(str_description, AssignTo, str_assigned_by, strReminderStatus, dt_start_date, dt_end_date, i_is_recurrence, i_recurrence_type, i_occurrence_end_count, i_day_option, i_d_day_count, i_d_every_weekday, i_w_recur_week_count, i_w_sunday, i_w_monday, i_w_tuesday, i_w_wednesday, i_w_thursday, i_w_friday, i_w_saturday, i_month_option, i_m_day, i_m_month_count, i_m_term, i_m_term_week, i_m_every_month_count, i_year_option, i_y_month, i_y_day, i_y_term, i_y_term_week, i_y_every_month_count, str_docotr_id, str_case_id, strCompanyID,"LOGIN",extddlRType.Text,extddlRType.Selected_Text.ToString());

                    }

                }


            }
            if (dsReminder.Tables.Count > 0)
            {
                if (dsReminder.Tables[0].Rows.Count > 0)
                {
                    if (dsReminder.Tables[0].Rows[0]["result"].ToString() == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues();alert('Reminder details added successfully...!!')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues();alert('Failed to add reminder details..!!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "ClearValues();alert('Failed to add reminder details..!!')", true);
            }

        }
        catch (Exception ex)
        {
          
        }
        finally
        {
            objReminder = null;
            dsReminder = null;
        }




    }
   




















}









