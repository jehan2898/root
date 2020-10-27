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
using System.Data;
using System.Data.SqlClient;

public partial class Bill_Sys_UpdateOutScheduleVisit : PageBase
{
    DataSet ds;
    Patient_TVBO _patient_TVBO;
    Bill_Sys_Calender _objCalendar;

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["eventId"] != null)
                {
                    txtEventID.Text = Request.QueryString["eventId"].ToString();
                }
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlReferringFacility.Flag_ID = txtCompanyID.Text.ToString();
                extddlReferringDoctor.Flag_ID = txtCompanyID.Text.ToString(); 
                extddlRoom.Flag_ID = txtCompanyID.Text;
                BindTimeControl();
                GETAppointPatientDetail(Convert.ToInt32(txtEventID.Text));
                extddlReferringFacility.Enabled = false;
                extddlRoom.Enabled = false;
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
    #endregion

    #region "Bind Time Control"
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
    #endregion

    #region "Transportation checkbox checked event"
    protected void chkTransportation_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (chkTransportation.Checked)
            {
                extddlTransport.Visible = true;
                extddlTransport.Text = "NA";
            }
            else
            {
                extddlTransport.Visible = false;
                extddlTransport.Text = "NA";
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
    #endregion

    #region "Load saved values and bind to control"

    private void GETAppointPatientDetail(int i_schedule_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _patient_TVBO = new Patient_TVBO();
        try
        {
            #region "Other details"
            ds = _patient_TVBO.getOutscheduleDetail(Convert.ToInt32(txtEventID.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SZ_ROOM_ID"].ToString() != "&nbsp;") { extddlRoom.Text = ds.Tables[0].Rows[0]["SZ_ROOM_ID"].ToString(); }
                if (ds.Tables[0].Rows[0]["SZ_BILLING_DOCTOR_ID"].ToString() != "&nbsp;") { extddlReferringDoctor.Text = ds.Tables[0].Rows[0]["SZ_BILLING_DOCTOR_ID"].ToString(); }
                if (ds.Tables[0].Rows[0]["SZ_REFERENCE_ID"].ToString() != "&nbsp;") { extddlReferringFacility.Text = ds.Tables[0].Rows[0]["SZ_REFERENCE_ID"].ToString(); extddlTransport.Flag_ID = extddlReferringFacility.Text; }
                if (ds.Tables[0].Rows[0]["DT_EVENT_DATE"].ToString() != "&nbsp;") { txtAppointmentDate.Text = ds.Tables[0].Rows[0]["DT_EVENT_DATE"].ToString(); }
                if (ds.Tables[0].Rows[0]["DT_EVENT_TIME"].ToString() != "&nbsp;") 
                { 
                    String []szTime = ds.Tables[0].Rows[0]["DT_EVENT_TIME"].ToString().Split('.');
                    if(Convert.ToInt32(szTime[0].ToString()) < 10)
                        ddlHours.SelectedValue = "0" + szTime[0].ToString();
                    else
                        ddlHours.SelectedValue = szTime[0].ToString();
                    ddlMinutes.SelectedValue = szTime[1].ToString();
                }
                if (ds.Tables[0].Rows[0]["DT_EVENT_TIME_TYPE"].ToString() != "&nbsp;") { ddlTime.SelectedValue =  ds.Tables[0].Rows[0]["DT_EVENT_TIME_TYPE"].ToString(); }
                if (ds.Tables[0].Rows[0]["BT_TRANSPORTATION"].ToString() != "&nbsp;") { if (ds.Tables[0].Rows[0]["BT_TRANSPORTATION"].ToString().ToLower() == "false") { chkTransportation.Checked = false; } else { chkTransportation.Checked = true; extddlTransport.Visible = true; } }
                if (ds.Tables[0].Rows[0]["SZ_EVENT_NOTES"].ToString() != "&nbsp;") { txtNotes.Text = ds.Tables[0].Rows[0]["SZ_EVENT_NOTES"].ToString(); }
                if (ds.Tables[0].Rows[0]["I_TRANSPORTATION_COMPANY"].ToString() != "&nbsp;") { extddlTransport.Text = ds.Tables[0].Rows[0]["I_TRANSPORTATION_COMPANY"].ToString(); }
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_ID"].ToString() != "&nbsp;") { txtPatientID.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_ID"].ToString(); }
                
            }
            #endregion
            #region "Load Procedure Codes"
            ds = new DataSet();
            BindReferringProcedureCodes();
            _patient_TVBO = new Patient_TVBO();
            ds = _patient_TVBO.GetAppointProcCode(i_schedule_id);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (ListItem lst in ddlTestNames.Items)
                {
                    if (lst.Value == dr.ItemArray.GetValue(0).ToString())
                    {
                        lst.Selected = true;
                    }
                }
            }
            #endregion
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

    #endregion

    protected void extddlReferringFacility_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            extddlRoom.Flag_ID = txtCompanyID.Text;
            BindTimeControl();
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

    #region "Bind Room Details"

    protected void extddlRoom_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindReferringProcedureCodes();
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

    private void BindReferringProcedureCodes()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList objArr = new ArrayList();
            objArr.Add(extddlReferringFacility.Text);
            objArr.Add(extddlRoom.Text);
            Bill_Sys_ManageVisitsTreatmentsTests_BO objBO = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
            ddlTestNames.Items.Clear();
            ddlTestNames.DataSource = objBO.GetReferringProcCodeList(objArr);
            ddlTestNames.DataTextField = "description";
            ddlTestNames.DataValueField = "code";
            ddlTestNames.DataBind();
            ddlTestNames.Items.Insert(0, "--- Select ---");
            ddlTestNames.Visible = true;
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
    
    #endregion
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        try
        {
            String szError = "";

            #region "Check for Previous Date"
            DateTime dtEnteredDate = Convert.ToDateTime(Convert.ToDateTime(txtAppointmentDate.Text).ToString("MM/dd/yyyy") + " " + ddlHours.SelectedValue + ":" + ddlMinutes.SelectedValue + " " + ddlTime.SelectedValue);
            DateTime dtEnteredLastDate = dtEnteredDate.AddMinutes(30);
            #endregion


            #region "Check For Room Days and Time"

            Decimal StartTime = 0.00M;
            Decimal EndTime = 0.00M;
            if (ddlTime.SelectedValue == "PM" && (Convert.ToInt32(ddlHours.SelectedValue) < 12))
            {
                StartTime = 12.00M + Convert.ToDecimal(ddlHours.SelectedValue.ToString()) + Convert.ToDecimal("." + ddlMinutes.SelectedValue.ToString());
            }
            else
            {
                StartTime = Convert.ToDecimal(ddlHours.SelectedValue.ToString()) + Convert.ToDecimal("." + ddlMinutes.SelectedValue.ToString());
            }

            if (dtEnteredLastDate.ToString("tt") == "PM" && (Convert.ToInt32(dtEnteredLastDate.ToString("hh")) < 12))
            {
                EndTime = 12M + Convert.ToDecimal(dtEnteredLastDate.ToString("hh") + "." + dtEnteredLastDate.ToString("mm"));
            }
            else
            {
                EndTime = Convert.ToDecimal(dtEnteredLastDate.ToString("hh") + "." + dtEnteredLastDate.ToString("mm"));
            }

            Bill_Sys_RoomDays objRD = new Bill_Sys_RoomDays();
            ArrayList objChekList = new ArrayList();
            objChekList.Add(extddlRoom.Text);
            objChekList.Add(Convert.ToDateTime(txtAppointmentDate.Text).ToString("MM/dd/yyyy"));
            objChekList.Add(extddlReferringFacility.Text);
            objChekList.Add(StartTime);
            objChekList.Add(EndTime);
            if (!objRD.checkRoomTiming(objChekList))
            {
                szError = szError + Convert.ToDateTime(txtAppointmentDate.Text).ToString("MM/dd/yyyy");
            }


            #endregion


            int eventID = Convert.ToInt32(txtEventID.Text);

            string sz_date = txtAppointmentDate.Text;
           
            ArrayList objAdd = new ArrayList();
            objAdd.Add(txtPatientID.Text);
            objAdd.Add(sz_date);
            objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());
            objAdd.Add(txtNotes.Text);
            objAdd.Add(extddlReferringDoctor.Text);
            if (ddlTestNames.Items.Count > 1) { objAdd.Add(ddlTestNames.Items[1].Value); } else objAdd.Add("");
            objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            objAdd.Add(ddlTime.SelectedValue);
            objAdd.Add((Convert.ToInt32(dtEnteredLastDate.ToString("hh")).ToString() + "." + dtEnteredLastDate.ToString("mm").ToString()));
            objAdd.Add(dtEnteredLastDate.ToString("tt"));
            objAdd.Add(extddlReferringFacility.Text);
            objAdd.Add(eventID);
            if (chkTransportation.Checked == true) { objAdd.Add(1); } else { objAdd.Add(0); }
            if (chkTransportation.Checked == true && extddlTransport.Text != "NA") { objAdd.Add(Convert.ToInt32(extddlTransport.Text)); } else { objAdd.Add(null); }
            eventID = _bill_Sys_Calender.UPDATE_Event_Referral(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());

            #region "Update Procedure Codes"
            _bill_Sys_Calender.Delete_Event_RefferPrcedure(eventID);
            foreach (ListItem lst in ddlTestNames.Items)
            {
                if (lst.Selected == true)
                {
                    objAdd = new ArrayList();
                    objAdd.Add(lst.Value);
                    objAdd.Add(eventID);
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    { objAdd.Add(2); }
                    else { objAdd.Add(0); }
                    _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                }
            }

            if (szError == "")
            {
                lblMsg.Text = "Appointment scheduled successfully.";
                if(Request.QueryString["GRD_ID"] != null)
                    Session["GRD_ID"] = Request.QueryString["GRD_ID"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='Bill_SysPatientDesk.aspx';window.self.close(); </script>");
            }
            else
            {
                Bill_Sys_RoomDays objRD1 = new Bill_Sys_RoomDays();
                String szStartTime = objRD1.getRoomStart_EndTime(extddlRoom.Text, txtAppointmentDate.Text, extddlReferringFacility.Text);
                lblMsg.Text = " Add appointment between ---> ";
                lblMsg.Text = lblMsg.Text + szStartTime;
                lblMsg.Focus();
                lblMsg.Visible = true;
            }
            #endregion
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
