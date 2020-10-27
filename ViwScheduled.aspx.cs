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

public partial class ViwScheduled : PageBase
{
    DataSet ds;
    Patient_TVBO _patient_TVBO;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", "return formValidator('form1','ddlStatus');");
        btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
        if (!IsPostBack)
        {
                BindTimeControl();
            string id = Request.QueryString["id"].ToString();
            txtEventID.Text = id;
            Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
            DataTable dt = new DataTable();
            dt = _bill_Sys_Calender.GET_EVENT_DETAIL(id);
            if (dt.Rows.Count > 0)
            {
                for (int drRow = 0; drRow <= dt.Rows.Count - 1; drRow++)
                {
                    lblDoctorName.Text = dt.Rows[drRow].ItemArray.GetValue(0).ToString();
                    lblType.Text = dt.Rows[drRow].ItemArray.GetValue(1).ToString();

                    lblHours.Text = dt.Rows[drRow].ItemArray.GetValue(3).ToString();
                    lblMinutes.Text = dt.Rows[drRow].ItemArray.GetValue(4).ToString();
                    lblTime.Text = dt.Rows[drRow].ItemArray.GetValue(5).ToString();
                    lblEndHours.Text = dt.Rows[drRow].ItemArray.GetValue(6).ToString();
                    lblEndMinutes.Text = dt.Rows[drRow].ItemArray.GetValue(7).ToString();
                    lblEndTime.Text = dt.Rows[drRow].ItemArray.GetValue(8).ToString();
                    txtNotes.Text = dt.Rows[drRow].ItemArray.GetValue(9).ToString();
                    lblDoctorID.Text = dt.Rows[drRow].ItemArray.GetValue(10).ToString();
                    lblTypeCode.Text = dt.Rows[drRow].ItemArray.GetValue(11).ToString();
                    lblPatientID.Text = dt.Rows[drRow].ItemArray.GetValue(12).ToString();
                    lblDate.Text = dt.Rows[drRow].ItemArray.GetValue(13).ToString();
                    ddlStatus.SelectedValue = dt.Rows[drRow].ItemArray.GetValue(15).ToString();
                    txtCaseID.Text = dt.Rows[drRow].ItemArray.GetValue(16).ToString();

                    if (_bill_Sys_Calender.CheckReferralExists(lblDoctorID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == true)
                    {
                        if (ddlStatus.SelectedValue == "2")
                        {
                            ddlTestNames.DataSource = _bill_Sys_Calender.GetAssociatedProcCodeList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, dt.Rows[drRow].ItemArray.GetValue(10).ToString(), "GETCOMPLETEVISITCODE", txtCaseID.Text, id);
                        }
                        else
                        {
                            ddlTestNames.DataSource = _bill_Sys_Calender.GetAssociatedProcCodeList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, dt.Rows[drRow].ItemArray.GetValue(10).ToString(), "GETALLCODE", txtCaseID.Text, id);
                        }
                        ddlTestNames.DataTextField = "DESCRIPTION";
                        ddlTestNames.DataValueField = "CODE";
                        ddlTestNames.DataBind();
                    }
                    else
                    {
                        ddlTestNames.DataSource = _bill_Sys_Calender.GetReferringProcCodeList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, dt.Rows[drRow].ItemArray.GetValue(10).ToString());
                        ddlTestNames.DataTextField = "DESCRIPTION";
                        ddlTestNames.DataValueField = "CODE";
                        ddlTestNames.DataBind();
                    }

                    // if (dt.Rows[drRow].ItemArray.GetValue(14).ToString() == "True" || ddlStatus.SelectedValue=="2" )
                    // {
                        //btnUpdate.Visible = true;
                        ds = new DataSet();
                        _patient_TVBO = new Patient_TVBO();
                        ds = _patient_TVBO.GetAppointProcCode(Convert.ToInt32(id));
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            foreach (ListItem lst in ddlTestNames.Items)
                            {
                                if (lst.Value == dr.ItemArray.GetValue(0).ToString())
                                {
                                    lst.Selected=true;
                                }
                            }

                        }
                    //}
                        if (ddlStatus.SelectedValue != "0")
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = ddlStatus.SelectedItem.Text;
                        btnSave.Visible = false;
                    }
                    if (dt.Rows[drRow]["BT_STATUS"].ToString().ToLower() == "false")
                    {
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnDelete.Visible = false;

                    }
                }
                
               
            }

            
            
            GETAppointPatientDetail(Convert.ToInt32(id));
            //}
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("ViwScheduled.aspx");
        }
        #endregion
    }
       
    
    private void GETAppointPatientDetail(int i_schedule_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable dt = new DataTable();
            ds = new DataSet();
            _patient_TVBO = new Patient_TVBO();
            ds = _patient_TVBO.GetAppointPatientDetails(i_schedule_id);

            dt = ds.Tables[0].Clone();

            if (ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;") { txtPatientID.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;") { txtPatientFName.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;") { txtMI.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;") { txtPatientLName.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;") { txtPatientPhone.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;") { txtPatientAddress.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(22).ToString() != "&nbsp;") { txtState.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(22).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString() != "&nbsp;") { txtBirthdate.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;") { txtPatientAge.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() != "&nbsp;") { txtSocialSecurityNumber.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "&nbsp;") { if (ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() == "True") { chkTransportation.Checked = true; } else { chkTransportation.Checked = false; } }

            if (ds.Tables[0].Rows[0].ItemArray.GetValue(34).ToString() != "&nbsp;") { txtCaseID.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(34).ToString(); }
            extddlInsuranceCompany.Flag_ID = ds.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
            extddlCaseType.Flag_ID = ds.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();

            if (ds.Tables[0].Rows[0].ItemArray.GetValue(33).ToString() != "&nbsp;") { extddlCaseType.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(33).ToString(); }
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(32).ToString() != "&nbsp;") { extddlInsuranceCompany.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(32).ToString(); }
           
           
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(42).ToString() != "&nbsp;") { txtNotes.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(42).ToString(); }

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
   
    private void ShowDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //string[] _char = Request.QueryString["_date"].ToString().Split(new char[] { ',' });
            //for (int i = 0; i <= _char.Length - 1; i++)
            //{
            //    if (_char[i].ToString() != "")
            //    {
            //        //Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
            //        //ArrayList objAdd = new ArrayList();
            //        //objAdd.Add(Request.QueryString["CaseId"].ToString());
            //        //objAdd.Add(_char[i].ToString());
            //        //objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());
            //        //objAdd.Add(txtNotes.Text);
            //        //objAdd.Add(extddlDoctor.Text);
            //        //objAdd.Add(ddlTestNames.SelectedValue);
            //        //objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            //        //objAdd.Add(ddlTime.SelectedValue);
            //        //objAdd.Add(ddlEndHours.SelectedValue.ToString() + "." + ddlEndMinutes.SelectedValue.ToString());
            //        //objAdd.Add(ddlEndTime.SelectedValue);
            //        //_bill_Sys_Calender.SaveEvent(objAdd);
            //    }
            //}


            ScheduleReportBO _obj = new ScheduleReportBO();
            _obj.DeleteEvent(txtEventID.Text);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden';  var parentWindow = window.parent;    parentWindow.SelectAndClosePopup();  </script>");

           // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.reload(); parent.document.getElementById('lblMsg').value='Event deleted successfully.';</script>");

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
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
                    lblMessage.Text = "Please enter Res-Schedule Date and Time";
                    _valid = false;
                }
            }
            else if (ddlStatus.SelectedValue == "2")
            {
                if (ddlTestNames.GetSelectedIndices().Length == 0)
                {
                    lblMessage.Text = "Please select procedure";
                    _valid = false;
                }

                DateTime dtPassDate;
                dtPassDate  = Convert.ToDateTime(lblDate.Text);
                if (dtPassDate > DateTime.Now)
                {
                    lblMessage.Text = "Cannot complete visit at future date.";
                    _valid = false;
                }
            }
            else if (ddlStatus.SelectedValue == "3")
            {
                DateTime dtPassDate;
                dtPassDate = Convert.ToDateTime(lblDate.Text);
                if (dtPassDate > DateTime.Now)
                {
                    lblMessage.Text = "Cannot no show visit at future date.";
                    _valid = false;
                }
            }
            if (_valid == true)
            {
               string eventID = Request.QueryString["id"].ToString();
                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                ArrayList objAdd;
                if (ddlStatus.SelectedValue == "2")
                {
                    foreach (ListItem lst in ddlTestNames.Items)
                    {
                        if (lst.Selected == true)
                        {
                            objAdd = new ArrayList();
                            objAdd.Add(lst.Value);
                            objAdd.Add(eventID);
                            objAdd.Add(ddlStatus.SelectedValue);
                            _bill_Sys_Calender.Update_Event_RefferPrcedure(objAdd);

                            Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
                            ArrayList arrOBJ = new ArrayList();
                            arrOBJ.Add(lblDoctorID.Text);
                            arrOBJ.Add(lst.Value);
                            arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            arrOBJ.Add(lst.Value);
                            _bill_Sys_ReferalEvent.AddDoctorAmount(arrOBJ);

                            _bill_Sys_Calender.UpdateProcedure(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, lblDoctorID.Text, lst.Value, true,"");
                        }
                        else
                        {

                            _bill_Sys_Calender.UpdateProcedure(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, lblDoctorID.Text, lst.Value, false, eventID);
                        }
                    }
                }
                else if (ddlStatus.SelectedValue == "1")
                {
                    
                     objAdd = new ArrayList();
                    objAdd.Add(txtCaseID.Text);
                    objAdd.Add(txtReScheduleDate.Text);
                    objAdd.Add(ddlReSchHours.SelectedValue.ToString() + "." + ddlReSchMinutes.SelectedValue.ToString());
                    objAdd.Add(txtNotes.Text);
                    objAdd.Add(lblDoctorID.Text);
                    objAdd.Add(lblTypeCode.Text);
                    objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    objAdd.Add(ddlReSchTime.SelectedValue);
                    ////////////////
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

                    //ddlEndHours.SelectedValue = endHr.ToString().PadLeft(2, '0');
                    //ddlEndMinutes.SelectedValue = endMin.ToString().PadLeft(2, '0');
                    //ddlEndTime.SelectedValue = endTime.ToString();
                    /////////////////
                    objAdd.Add(endHr.ToString().PadLeft(2, '0').ToString() + "." + endMin.ToString().PadLeft(2, '0').ToString());
                    objAdd.Add(endTime.ToString());
                    _bill_Sys_Calender.SaveEvent(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    objAdd = new ArrayList();
                    objAdd.Add(txtCaseID.Text);
                    objAdd.Add(lblDoctorID.Text);
                    objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    int reScheventID = _bill_Sys_Calender.GetEventID(objAdd);
                    if (_bill_Sys_Calender.CheckReferralExists(lblDoctorID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == true)
                    {


                        foreach (ListItem lstItem in ddlTestNames.Items)
                        {
                            if (lstItem.Selected == true)
                            {
                                objAdd = new ArrayList();
                                objAdd.Add(lstItem.Value);
                                objAdd.Add(reScheventID);
                                objAdd.Add(0);
                                 _bill_Sys_Calender.Update_Event_RefferPrcedure(objAdd,eventID);
                  //              _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);


                                _bill_Sys_Calender.UpdateProcedure(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, lblDoctorID.Text, lstItem.Value, true,"");
                            }
                            else
                            {
                                _bill_Sys_Calender.UpdateProcedure(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, lblDoctorID.Text, lstItem.Value, false, reScheventID.ToString());
                            }
                        }
                    }

                }

                _bill_Sys_Calender = new Bill_Sys_Calender();
                objAdd = new ArrayList();
                objAdd.Add(eventID);
                objAdd.Add(false);
                objAdd.Add(ddlStatus.SelectedValue);
                _bill_Sys_Calender.UPDATE_Event_Status(objAdd);
                // Start : Save appointment Notes.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_UPDATED";
              //  _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + lblDate.Text;

                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + lblDate.Text.Substring(0, lblDate.Text.IndexOf(" ")) + " " + lblHours.Text + ":" + lblMinutes.Text + " " + lblTime.Text;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


                // End 
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.parent.document.location.reload(); parent.document.getElementById('lblMsg').value='Event added successfully.';</script>");
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }
}
