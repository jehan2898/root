using AjaxControlToolkit;
using ExtendedDropDownList;
using Reminders;
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_Bill_Sys_Case_Reminder : Page, IRequiresSessionState
{

    public AJAX_Pages_Bill_Sys_Case_Reminder()
    {
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ReminderBO reminderBO = null;
        DataSet dataSet = null;
        string str = "";
        string str1 = "";
        string sZCASEID = "";
        string str2 = "";
        DateTime dateTime = DateTime.Now.AddYears(2);
        DateTime dateTime1 = Convert.ToDateTime(dateTime.ToShortDateString());
        int num = 0;
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        int num8 = 0;
        int num9 = 0;
        int num10 = 0;
        int num11 = 0;
        int num12 = 0;
        int num13 = 0;
        int num14 = 0;
        int num15 = 0;
        int num16 = 0;
        int selectedIndex = 100;
        int selectedIndex1 = 100;
        int num17 = 0;
        int num18 = 0;
        int selectedIndex2 = 100;
        int num19 = 0;
        int selectedIndex3 = 100;
        int selectedIndex4 = 100;
        int selectedIndex5 = 100;
        string str3 = "RS000000000000000001";
        try
        {
            try
            {
                if (this.txtReminderDesc.Text.Trim().ToString() == "")
                {
                    if (!this.chkRecurrence.Checked)
                    {
                        this.pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "none");
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Note description should not be empty...!!')", true);
                        return;
                    }
                    else
                    {
                        this.pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Note description should not be empty...!!')", true);
                        return;
                    }
                }
                else if (this.txtStartDate.Text == "")
                {
                    if (!this.chkRecurrence.Checked)
                    {
                        this.pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "none");
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('start date should not be empty...!!')", true);
                        return;
                    }
                    else
                    {
                        this.pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('start date should not be empty...!!')", true);
                        return;
                    }
                }
                else if (!this.rbtnWeekly.Checked || this.chkWSunday.Checked || this.chkWMonday.Checked || this.chkWTuesday.Checked || this.chkWWednesday.Checked || this.chkWThursday.Checked || this.chkWFriday.Checked || this.chkWSaturday.Checked)
                {
                    str = this.txtReminderDesc.Text.Trim().ToString();
                    str = str.Replace('\n', ' ');
                    str = str.Replace('\r', ' ');
                    if (this.txtUserID.Text != "")
                    {
                        str1 = this.txtUserID.Text.Trim().ToString();
                    }
                    DateTime dateTime2 = Convert.ToDateTime(this.txtStartDate.Text.Trim().ToString());
                    this.lsbAssignTo.SelectedValue.ToString();
                    if (!this.chkRecurrence.Checked)
                    {
                        num2 = 1;
                        dateTime1 = Convert.ToDateTime(this.txtStartDate.Text.Trim().ToString());
                        reminderBO = new ReminderBO();
                        if ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"] != null)
                        {
                            sZCASEID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        }
                        if (this.Session["SZ_REMINDER_DOCTOR_ID"] != null)
                        {
                            str2 = this.Session["SZ_REMINDER_DOCTOR_ID"].ToString();
                        }
                        string value = "";
                        string sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        foreach (ListItem item in this.lsbAssignTo.Items)
                        {
                            if (!item.Selected)
                            {
                                continue;
                            }
                            value = item.Value;
                            dataSet = reminderBO.SetReminderDetailsAdd(str, value, str1, str3, dateTime2, dateTime1, num, num1, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11, num12, num13, num14, num15, num16, selectedIndex, selectedIndex1, num17, num18, selectedIndex2, num19, selectedIndex3, selectedIndex4, selectedIndex5, str2, sZCASEID, sZCOMPANYID, "CASE", this.extddlRType.Text, this.extddlRType.Selected_Text.ToString());
                        }
                    }
                    else
                    {
                        num = 1;
                        if (this.rbtnNoEndDate.Checked)
                        {
                            DateTime dateTime3 = DateTime.Now.AddYears(2);
                            dateTime1 = Convert.ToDateTime(dateTime3.ToShortDateString());
                        }
                        else if (this.rbtnEndBy.Checked)
                        {
                            dateTime1 = Convert.ToDateTime(this.txtEndByDate.Text.Trim().ToString());
                        }
                        else if (this.rbtnEndAfter.Checked)
                        {
                            num1 = 4;
                            num2 = Convert.ToInt32(this.txtEndAfter.Text.Trim().ToString());
                            DateTime dateTime4 = Convert.ToDateTime(this.txtStartDate.Text.Trim().ToString());
                            dateTime1 = dateTime4.AddDays(Convert.ToDouble(num2));
                        }
                        if (!(dateTime2.ToString() != "") || !(dateTime1.ToString() != "") || !(dateTime2 > dateTime1))
                        {
                            if (this.rbtnDaily.Checked)
                            {
                                num1 = 0;
                                if (!this.rbtnDEvery.Checked)
                                {
                                    num5 = 1;
                                    num3 = 1;
                                }
                                else
                                {
                                    num4 = Convert.ToInt32(this.txtDDays.Text.Trim().ToString());
                                    num3 = 0;
                                }
                            }
                            else if (this.rbtnWeekly.Checked)
                            {
                                num1 = 1;
                                num6 = Convert.ToInt32(this.txtWRecur.Text.Trim().ToString());
                                if (this.chkWSunday.Checked)
                                {
                                    num7 = 1;
                                }
                                if (this.chkWMonday.Checked)
                                {
                                    num8 = 1;
                                }
                                if (this.chkWTuesday.Checked)
                                {
                                    num9 = 1;
                                }
                                if (this.chkWWednesday.Checked)
                                {
                                    num10 = 1;
                                }
                                if (this.chkWThursday.Checked)
                                {
                                    num11 = 1;
                                }
                                if (this.chkWFriday.Checked)
                                {
                                    num12 = 1;
                                }
                                if (this.chkWSaturday.Checked)
                                {
                                    num13 = 1;
                                }
                            }
                            else if (this.rbtnMonthly.Checked)
                            {
                                num1 = 2;
                                if (!this.rbtnMDay.Checked)
                                {
                                    num14 = 1;
                                    selectedIndex = this.drpMTerm.SelectedIndex;
                                    selectedIndex1 = this.drpMDayRecur.SelectedIndex;
                                    num17 = Convert.ToInt32(this.txtMEveryMonths.Text.Trim().ToString());
                                }
                                else
                                {
                                    num14 = 0;
                                    num15 = Convert.ToInt32(this.txtMDay.Text.Trim().ToString());
                                    num16 = Convert.ToInt32(this.txtMMonths.Text.Trim().ToString());
                                }
                            }
                            else if (this.rbtnYearly.Checked)
                            {
                                num1 = 3;
                                if (!this.rbtnYEvery.Checked)
                                {
                                    num18 = 1;
                                    selectedIndex3 = this.drpYTerm.SelectedIndex;
                                    selectedIndex4 = this.drpYDayRecur.SelectedIndex;
                                    selectedIndex5 = this.drpYMonthRecur.SelectedIndex;
                                }
                                else
                                {
                                    num18 = 0;
                                    selectedIndex2 = this.drpYMonth.SelectedIndex;
                                    num19 = Convert.ToInt32(this.txtYDay.Text.Trim().ToString());
                                }
                            }
                            reminderBO = new ReminderBO();
                            if ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"] != null)
                            {
                                sZCASEID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                            }
                            if (this.Session["SZ_REMINDER_DOCTOR_ID"] != null)
                            {
                                str2 = this.Session["SZ_REMINDER_DOCTOR_ID"].ToString();
                            }
                            string value1 = "";
                            string sZCOMPANYID1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            foreach (ListItem listItem in this.lsbAssignTo.Items)
                            {
                                if (!listItem.Selected)
                                {
                                    continue;
                                }
                                value1 = listItem.Value;
                                dataSet = reminderBO.SetReminderDetailsAdd(str, value1, str1, str3, dateTime2, dateTime1, num, num1, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11, num12, num13, num14, num15, num16, selectedIndex, selectedIndex1, num17, num18, selectedIndex2, num19, selectedIndex3, selectedIndex4, selectedIndex5, str2, sZCASEID, sZCOMPANYID1, "CASE", this.extddlRType.Text, this.extddlRType.Selected_Text.ToString());
                            }
                        }
                        else
                        {
                            this.pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                            ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Reminder start date should not be greated than reminder end date...!!')", true);
                            return;
                        }
                    }
                    if (dataSet.Tables.Count <= 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ClearValues();alert('Failed to add reminder details..!!')", true);
                    }
                    else if (dataSet.Tables[0].Rows.Count <= 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ClearValues();alert('Failed to add reminder details..!!')", true);
                    }
                    else if (dataSet.Tables[0].Rows[0]["result"].ToString() == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ClearValues();alert('Reminder details added successfully...!!')", true);
                    }
                }
                else
                {
                    this.pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "block");
                    ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "ReminderTypeBasedVisible();alert('Plase select week days in weekly recurrence...!!')", true);
                    return;
                }
            }
            catch (Exception exception)
            {
            }
        }
        finally
        {
            reminderBO = null;
            dataSet = null;
        }
    }

    public void LoadAssignToListBox()
    {
        ReminderBO reminderBO = new ReminderBO();
        string sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        DataSet dataSet = new DataSet();
        dataSet = reminderBO.GetAddReminderList(sZCOMPANYID);
        this.lsbAssignTo.DataSource = dataSet.Tables[0];
        this.lsbAssignTo.DataTextField = "Description";
        this.lsbAssignTo.DataValueField = "Code";
        this.lsbAssignTo.DataBind();
        for (int i = 0; i < this.lsbAssignTo.Items.Count; i++)
        {
            if (this.lsbAssignTo.Items[i].Text == ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME)
            {
                this.lsbAssignTo.SelectedIndex = i;
            }
        }
    }

    public void LoadMonthlyDropdown()
    {
        this.drpMTerm.Items.Clear();
        this.drpMTerm.Items.Insert(0, "first");
        this.drpMTerm.Items.Insert(1, "second");
        this.drpMTerm.Items.Insert(2, "third");
        this.drpMTerm.Items.Insert(3, "fourth");
        this.drpMTerm.Items.Insert(4, "last");
        this.drpMTerm.SelectedIndex = 0;
        this.drpMDayRecur.Items.Clear();
        this.drpMDayRecur.Items.Insert(0, "day");
        this.drpMDayRecur.Items.Insert(1, "weekday");
        this.drpMDayRecur.Items.Insert(2, "weekend day");
        this.drpMDayRecur.Items.Insert(3, "sunday");
        this.drpMDayRecur.Items.Insert(4, "monday");
        this.drpMDayRecur.Items.Insert(5, "tuesday");
        this.drpMDayRecur.Items.Insert(6, "wednesday");
        this.drpMDayRecur.Items.Insert(7, "thursday");
        this.drpMDayRecur.Items.Insert(8, "friday");
        this.drpMDayRecur.Items.Insert(9, "saturday");
        this.drpMDayRecur.SelectedIndex = 0;
    }

    public void LoadYearlyDropdown()
    {
        this.drpYTerm.Items.Clear();
        this.drpYTerm.Items.Insert(0, "first");
        this.drpYTerm.Items.Insert(1, "second");
        this.drpYTerm.Items.Insert(2, "third");
        this.drpYTerm.Items.Insert(3, "fourth");
        this.drpYTerm.Items.Insert(4, "last");
        this.drpYTerm.SelectedIndex = 0;
        this.drpYDayRecur.Items.Clear();
        this.drpYDayRecur.Items.Insert(0, "day");
        this.drpYDayRecur.Items.Insert(1, "weekday");
        this.drpYDayRecur.Items.Insert(2, "weekend day");
        this.drpYDayRecur.Items.Insert(3, "sunday");
        this.drpYDayRecur.Items.Insert(4, "monday");
        this.drpYDayRecur.Items.Insert(5, "tuesday");
        this.drpYDayRecur.Items.Insert(6, "wednesday");
        this.drpYDayRecur.Items.Insert(7, "thursday");
        this.drpYDayRecur.Items.Insert(8, "friday");
        this.drpYDayRecur.Items.Insert(9, "saturday");
        this.drpYDayRecur.SelectedIndex = 0;
        this.drpYMonth.Items.Clear();
        this.drpYMonth.Items.Insert(0, "January");
        this.drpYMonth.Items.Insert(1, "February");
        this.drpYMonth.Items.Insert(2, "March");
        this.drpYMonth.Items.Insert(3, "April");
        this.drpYMonth.Items.Insert(4, "May");
        this.drpYMonth.Items.Insert(5, "June");
        this.drpYMonth.Items.Insert(6, "July");
        this.drpYMonth.Items.Insert(7, "August");
        this.drpYMonth.Items.Insert(8, "September");
        this.drpYMonth.Items.Insert(9, "October");
        this.drpYMonth.Items.Insert(10, "November");
        this.drpYMonth.Items.Insert(11, "December");
        this.drpYMonthRecur.Items.Clear();
        this.drpYMonthRecur.Items.Insert(0, "January");
        this.drpYMonthRecur.Items.Insert(1, "February");
        this.drpYMonthRecur.Items.Insert(2, "March");
        this.drpYMonthRecur.Items.Insert(3, "April");
        this.drpYMonthRecur.Items.Insert(4, "May");
        this.drpYMonthRecur.Items.Insert(5, "June");
        this.drpYMonthRecur.Items.Insert(6, "July");
        this.drpYMonthRecur.Items.Insert(7, "August");
        this.drpYMonthRecur.Items.Insert(8, "September");
        this.drpYMonthRecur.Items.Insert(9, "October");
        this.drpYMonthRecur.Items.Insert(10, "November");
        this.drpYMonthRecur.Items.Insert(11, "December");
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
            this.extddlRType.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            AttributeCollection attributes = this.imgbtnStartDate.Attributes;
            string[] clientID = new string[] { "cal1x.select(document.forms[0].", this.txtStartDate.ClientID, ",'", this.imgbtnStartDate.ClientID, "','MM/dd/yyyy'); return false;" };
            attributes.Add("onclick", string.Concat(clientID));
            AttributeCollection attributeCollection = this.ImgEndByDate.Attributes;
            string[] strArrays = new string[] { "cal1x.select(document.forms[0].", this.txtEndByDate.ClientID, ",'", this.ImgEndByDate.ClientID, "','MM/dd/yyyy'); return false;" };
            attributeCollection.Add("onclick", string.Concat(strArrays));
            this.txtDDays.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            this.txtWRecur.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            this.txtMDay.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            this.txtMMonths.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            this.txtMEveryMonths.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            this.txtYDay.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            this.txtEndAfter.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            this.txtEndAfter.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
            this.txtDDays.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            this.txtWRecur.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            this.txtMMonths.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            this.txtMEveryMonths.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            this.txtEndAfter.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            this.txtEndAfter.Attributes.Add("onblur", "javascript:return ZeroNotAllowed(this);");
            this.txtMDay.Attributes.Add("onblur", "javascript:return DayValidation(this);");
            this.txtYDay.Attributes.Add("onblur", "javascript:return DayValidation(this);");
            this.drpYMonth.Attributes.Add("onClick", "javascript:return NewDayValidation();");
            if (!base.IsPostBack)
            {
                this.pnlRecurrence.Style.Add(HtmlTextWriterStyle.Display, "none");
                this.pnlDaily.Style.Add(HtmlTextWriterStyle.Display, "none");
                this.pnlWeekly.Style.Add(HtmlTextWriterStyle.Display, "none");
                this.pnlMonthly.Style.Add(HtmlTextWriterStyle.Display, "none");
                this.pnlYearly.Style.Add(HtmlTextWriterStyle.Display, "none");
                this.txtDDays.Text = "1";
                this.txtWRecur.Text = "1";
                this.txtMDay.Text = DateTime.Now.Day.ToString();
                this.txtMMonths.Text = "1";
                this.txtMEveryMonths.Text = "1";
                this.txtYDay.Text = DateTime.Now.Day.ToString();
                this.txtStartDate.Text = DateTime.Now.ToShortDateString();
                this.txtEndByDate.Text = DateTime.Now.ToShortDateString();
                this.chkRecurrence.Attributes.Add("onclick", "return MakeVisibleRecurrence();");
                this.btnSave.Attributes.Add("onclick", "return Validate();");
                this.btnSave.Attributes.Add("onclick", "return ReminderType();");
                this.LoadMonthlyDropdown();
                this.LoadAssignToListBox();
                this.LoadYearlyDropdown();
                int month = DateTime.Now.Month;
                int num = Convert.ToInt32(month.ToString());
                this.drpYMonth.SelectedIndex = num - 1;
                this.drpYMonthRecur.SelectedIndex = num - 1;
                this.txtUserID.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
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
}