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

public partial class AJAX_Pages_Bill_Sys_ViewVisit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTimeControl();
            BindChangeTimeControl();
            txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCaseID.Text = Request.QueryString["CaseID"].ToString();
            txtEventID.Text = Request.QueryString["eventid"].ToString();
            txtDocName.Text = Request.QueryString["docname"].ToString();
            txtPatientName.Text = Request.QueryString["Name"].ToString();
            txtStartTime.Text = Request.QueryString["stime"].ToString();
            txtEndTime.Text = Request.QueryString["etime"].ToString();
            txtDoctorid.Text = Request.QueryString["szdoctorid"].ToString();
            txtHaveLogin.Text = Request.QueryString["szhavelogin"].ToString();
            txtGroupCode.Text = Request.QueryString["szgroupcode"].ToString();
            btnchnagetime.Attributes.Add("onclick", "return  ChangeTime()");
            if (txtHaveLogin.Text == "1")
            {
                //ddlStatus.Text = "1";
                //ddlStatus.Enabled = false;
                //ddlStatus.Visible = true;
                //tdReDate.Style.Add("visibility", "visible");
                //tdReDateValue.Style.Add("visibility", "visible");
                //tdReTime.Style.Add("visibility", "visible");
                //tdReTimeValue.Style.Add("visibility", "visible");


            }

        }
    }
    private void BindTimeControl()
    {//Logging Start
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

    private void BindChangeTimeControl()
    {//Logging Start
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

    protected void btnSave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Boolean _valid = true;

            if (ddlStatus.SelectedValue == "1")
            {
                if (txtReScheduleDate.Text == "" && ddlReSchHours.SelectedValue == "00")
                {
                    lblMessage.Text = "Please enter Re-Schedule Date and Time";
                    _valid = false;
                }
            }
            if (_valid == true)
            {
                string eventID = Request.QueryString["eventid"].ToString();
                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                Bill_Sys_Event_BO _Bill_Sys_Event_BO = new Bill_Sys_Event_BO();
                ArrayList objAdd;
                if (txtHaveLogin.Text == "1")
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

                        _Bill_Sys_Event_BO.UpdateRescheduledoctorvisits(txtEventID.Text, txtNotes.Text, txtReScheduleDate.Text, txtGroupCode.Text, ddlReSchHours.SelectedValue.ToString() + "." + ddlReSchMinutes.SelectedValue.ToString(), ddlReSchTime.SelectedValue, endHr.ToString().PadLeft(2, '0').ToString() + "." + endMin.ToString().PadLeft(2, '0').ToString(), endTime);

                    }
                    else if (ddlStatus.SelectedValue == "3")
                    {
                        Bill_Sys_Calender _bill_Sys_Calender1 = new Bill_Sys_Calender();
                        ArrayList objAdd1 = new ArrayList();
                        objAdd1.Add(eventID);
                        objAdd1.Add(false);
                        objAdd1.Add(ddlStatus.SelectedValue);
                        _bill_Sys_Calender1.UPDATE_Event_Status(objAdd1);
                    }
                    else
                    {
                        usrMessage.PutMessage("Note: You cannot mark a visit as Completed if that patient visit is to be finalized by the doctor.");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                        return;

                    }
                }
                else
                {
                    if (ddlStatus.SelectedValue == "1")
                    {
                        lblMessage.Text = "";
                        objAdd = new ArrayList();
                        objAdd.Add(txtCaseID.Text);
                        objAdd.Add(txtReScheduleDate.Text);
                        objAdd.Add(ddlReSchHours.SelectedValue.ToString() + "." + ddlReSchMinutes.SelectedValue.ToString());
                        objAdd.Add(txtNotes.Text);
                        objAdd.Add(txtDoctorid.Text);
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
                    objAdd.Add(eventID);
                    objAdd.Add(false);
                    objAdd.Add(ddlStatus.SelectedValue);
                    _bill_Sys_Calender.UPDATE_Event_Status(objAdd);
                }
                usrMessage.PutMessage("Save Sucessfully ...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
        }
        catch(Exception ex)
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

    protected void btnchnagetime_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Event_BO _Bill_Sys_Event_BO = new Bill_Sys_Event_BO();
        try
        {
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
            _Bill_Sys_Event_DAO.I_EVENT_ID = txtEventID.Text;
            _Bill_Sys_Event_DAO.SZ_COMPANY_ID = txtCompanyId.Text;
            _Bill_Sys_Event_DAO.DT_EVENT_TIME = ddlchangeReSchHours.SelectedValue.ToString() + "." + ddlchangeReSchMinutes.SelectedValue.ToString();
            _Bill_Sys_Event_DAO.DT_EVENT_TIME_TYPE = ddlchangeReSchTime.SelectedValue;
            _Bill_Sys_Event_DAO.DT_EVENT_END_TIME = endHr.ToString().PadLeft(2, '0').ToString() + "." + endMin.ToString().PadLeft(2, '0').ToString();
            _Bill_Sys_Event_DAO.DT_EVENT_END_TIME_TYPE = endTime.ToString();
            arradd.Add(_Bill_Sys_Event_DAO);
            _Bill_Sys_Event_BO.UpdateVisitTime(arradd);
            usrMessage.PutMessage("Save Sucessfully ...");
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
}
