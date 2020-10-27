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

public partial class Bill_Sys_UpdateVisit : PageBase
{
    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    private ArrayList objAdd;

    DAO_NOTES_BO _DAO_NOTES_BO;
    DAO_NOTES_EO _DAO_NOTES_EO;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            btnSave.Attributes.Add("onclick", "return formValidator('form1','extddlDoctor,txtAppointmentDate,extddlVisitType,ddlHours,txtReDate,ddlReSchHours');");
            
           
            if (!IsPostBack)
            {
                string eventId = Request.QueryString["eventId"].ToString();
                BindTimeControl();
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlVisitType.Flag_ID = txtCompanyID.Text;
                // Set default 'C' for Visit Type
                Bill_Sys_PatientVisitBO objPV = new Bill_Sys_PatientVisitBO();
                extddlVisitType.Text = objPV.GetDefaultVisitType(txtCompanyID.Text);


                ///////////////////
                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                DataTable dt = new DataTable();
                dt = _bill_Sys_Calender.GET_EVENT_DETAIL(eventId);
                if (dt.Rows.Count > 0)
                {
                    for (int drRow = 0; drRow <= dt.Rows.Count - 1; drRow++)
                    {


                        ddlHours.SelectedValue = dt.Rows[drRow].ItemArray.GetValue(3).ToString().PadLeft(2, '0');
                        ddlMinutes.SelectedValue = dt.Rows[drRow].ItemArray.GetValue(1).ToString().PadLeft(2,'0');
                        ddlTime.SelectedValue = dt.Rows[drRow].ItemArray.GetValue(5).ToString();

                        extddlDoctor.Flag_ID = dt.Rows[drRow].ItemArray.GetValue(10).ToString();
                        extddlDoctor.Text = dt.Rows[drRow].ItemArray.GetValue(10).ToString();

                        extddlVisitType.Text = dt.Rows[drRow].ItemArray.GetValue(17).ToString();

                        txtPatientID.Text = dt.Rows[drRow].ItemArray.GetValue(12).ToString();
                        txtAppointmentDate.Text = dt.Rows[drRow].ItemArray.GetValue(13).ToString();
                        ddlStatus.SelectedValue = dt.Rows[drRow].ItemArray.GetValue(15).ToString();
                        txtCaseID.Text = dt.Rows[drRow].ItemArray.GetValue(16).ToString();
                        txtVisitStatus.Text = dt.Rows[drRow].ItemArray.GetValue(15).ToString();
                        txtReEventID.Text = dt.Rows[drRow].ItemArray.GetValue(18).ToString();
                        if (txtReEventID.Text == "" && ddlStatus.SelectedValue == "1")
                        {
                            txtReDate.Text = dt.Rows[drRow].ItemArray.GetValue(19).ToString();
                            ddlReSchHours.SelectedValue = dt.Rows[drRow].ItemArray.GetValue(20).ToString().PadLeft(2, '0');
                            ddlReSchMinutes.SelectedValue = dt.Rows[drRow].ItemArray.GetValue(21).ToString().PadLeft(2, '0');
                            ddlReSchTime.SelectedValue = dt.Rows[drRow].ItemArray.GetValue(22).ToString();
                            
                       
                            lblReDate.Visible = true;
                            lblReTIme.Visible = true;
                            txtReDate.Visible = true;
                            ddlReSchHours.Visible = true;
                            ddlReSchMinutes.Visible = true;
                            ddlReSchTime.Visible = true;
                        }
                        if (ddlStatus.SelectedValue == "2")
                        {
                            btnSave.Visible = true;
                            ddlHours.Enabled = false;
                            ddlMinutes.Enabled = false;
                            ddlTime.Enabled = false;
                            ddlStatus.Enabled = false;
                            extddlDoctor.Enabled = false;
                            txtAppointmentDate.Enabled = false;
                            
                        }

                        DataTable dtTest = _bill_Sys_Calender.GetReferringProcCodeList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extddlDoctor.Text).Tables[0];
                        ddlTestNames.DataSource = dtTest;
                        ddlTestNames.DataTextField = "DESCRIPTION";
                        ddlTestNames.DataValueField = "CODE";
                        ddlTestNames.DataBind();
                        ddlOldTestNames.DataSource = dtTest;
                        ddlOldTestNames.DataTextField = "DESCRIPTION";
                        ddlOldTestNames.DataValueField = "CODE";
                        ddlOldTestNames.DataBind();
                        

                        // if (dt.Rows[drRow].ItemArray.GetValue(14).ToString() == "True" || ddlStatus.SelectedValue=="2" )
                        // {
                        //btnUpdate.Visible = true;
                        DataSet ds = new DataSet();
                        Patient_TVBO _patient_TVBO = new Patient_TVBO();
                        ds = _patient_TVBO.GetAppointProcCode(Convert.ToInt32(eventId));
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            foreach (ListItem lst in ddlTestNames.Items)
                            {
                                if (lst.Value == dr.ItemArray.GetValue(0).ToString())
                                {
                                    lst.Selected = true;
                                }
                            }
                            foreach (ListItem lst in ddlOldTestNames.Items)
                            {
                                if (lst.Value == dr.ItemArray.GetValue(0).ToString())
                                {
                                    lst.Selected = true;
                                }
                            }
                        }
                        //}

                    }
                    ////////////////////
                }
            }
            lblMsg.Text = "";
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

  
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        objAdd = new ArrayList();
        Bill_Sys_Calender _bill_Sys_Calender;
        string eventId = Request.QueryString["eventId"].ToString();
        try
        {

            if (Convert.ToDateTime(txtAppointmentDate.Text) > Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")) && ddlStatus.SelectedValue=="2")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('Visit for future date cannot be completed...!');</script>");
            }
            else
            {
                //////////

                //Boolean iEvisitExists = false;
                //Boolean visitExists = false;
                //Boolean reVisitExists = false;
                //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings.Get("MyConnectionString"));
                //SqlCommand comd = new SqlCommand("SP_CHECK_UPDATE_INITIALE_VALUATIONEXISTS");
                //comd.CommandType = CommandType.StoredProcedure;
                //comd.Connection = con;
                //comd.Connection.Open();
                //comd.Parameters.AddWithValue("@SZ_CASE_ID", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
                //comd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //comd.Parameters.AddWithValue("@SZ_PATIENT_ID", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                //comd.Parameters.AddWithValue("@SZ_DOCTOR_ID", extddlDoctor.Text);
                //comd.Parameters.AddWithValue("@VISIT_DATE", txtAppointmentDate.Text);
                //comd.Parameters.AddWithValue("@I_EVENT_ID", eventId);
                ////int intCountVisits = Convert.ToInt32(comd.ExecuteScalar());
                //if (ddlStatus.SelectedValue == "1") { comd.Parameters.AddWithValue("@RE_VISIT_DATE", txtReDate.Text); }
                //SqlParameter objIEExists = new SqlParameter("@INITIAL_EXISTS", SqlDbType.Bit);
                //objIEExists.Direction = ParameterDirection.Output;
                //comd.Parameters.Add(objIEExists);
                //SqlParameter objVisitStatus = new SqlParameter("@VISIT_EXISTS", SqlDbType.Bit, 20);
                //objVisitStatus.Direction = ParameterDirection.Output;
                //comd.Parameters.Add(objVisitStatus);
                //SqlParameter objReVisitStatus = new SqlParameter("@RE_VISIT_EXISTS", SqlDbType.Bit, 20);
                //objReVisitStatus.Direction = ParameterDirection.Output;
                //comd.Parameters.Add(objReVisitStatus);
                //comd.ExecuteNonQuery();
                //comd.Connection.Close();

                //iEvisitExists = Convert.ToBoolean(objIEExists.Value);
                //visitExists = Convert.ToBoolean(objVisitStatus.Value);
                //if (objReVisitStatus.Value.ToString() != "") reVisitExists = Convert.ToBoolean(objReVisitStatus.Value);
                //if (iEvisitExists == false && extddlVisitType.Selected_Text != "IE")
                //{
                //    lblMsg.Text = "Schedule can not be saved patient is visiting first time hence there visit type should be Initial Evaluation.";
                //    lblMsg.Focus();
                //    lblMsg.Visible = true;
                //    return;
                //}
                //if (iEvisitExists == true && extddlVisitType.Selected_Text == "IE")
                //{
                //    lblMsg.Text = "Schedule can not be saved because patient already has Initial Evaluation.";
                //    lblMsg.Focus();
                //    lblMsg.Visible = true;
                //    return;
                //}
                //if (visitExists == true && ddlStatus.SelectedValue!="1")
                //{
                //    lblMsg.Text = "Schedule can not be saved because patient already has this visit";
                //    lblMsg.Focus();
                //    lblMsg.Visible = true;
                //    return;
                //}
                //if (reVisitExists == true && ddlStatus.SelectedValue == "1" && txtReEventID.Text == "")
                //{
                //    lblMsg.Text = "Schedule can not be saved because patient already has visit on re-schedule date";
                //    lblMsg.Focus();
                //    lblMsg.Visible = true;
                //    return;
                //}
                /////////

                ////////////RE-Schedule
                ////////////RE-Schedule add
                int reendMin = Convert.ToInt32(ddlReSchMinutes.SelectedValue) + Convert.ToInt32(30);
                int reendHr = Convert.ToInt32(ddlReSchHours.SelectedValue);
                string reendTime = ddlReSchTime.SelectedValue;
                if (reendMin >= 60)
                {
                    reendMin = reendMin - 60;
                    reendHr = reendHr + 1;
                    if (reendHr > 12)
                    {
                        reendHr = reendHr - 12;
                        if (ddlReSchHours.SelectedValue != "12")
                        {
                            if (reendTime == "AM")
                            {
                                reendTime = "PM";
                            }
                            else if (reendTime == "PM")
                            {
                                reendTime = "AM";
                            }
                        }
                    }
                    else if (reendHr == 12)
                    {
                        if (ddlReSchHours.SelectedValue != "12")
                        {
                            if (reendTime == "AM")
                            {
                                reendTime = "PM";
                            }
                            else if (reendTime == "PM")
                            {
                                reendTime = "AM";
                            }
                        }
                    }
                }
                if (ddlStatus.SelectedValue == "1" && txtReEventID.Text=="")
                {
                    _bill_Sys_Calender = new Bill_Sys_Calender();
                    objAdd = new ArrayList();
                    objAdd.Add(txtCaseID.Text);//Case Id
                    objAdd.Add(txtReDate.Text);//Appointment date
                    objAdd.Add(ddlReSchHours.SelectedValue.ToString() + "." + ddlReSchMinutes.SelectedValue.ToString());//Appointment time
                    objAdd.Add("");//Notes       
                    objAdd.Add(extddlDoctor.Text);
                    objAdd.Add("TY000000000000000003");
                    objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    objAdd.Add(ddlReSchTime.SelectedValue);
                    objAdd.Add(reendHr.ToString().PadLeft(2, '0') + "." + reendMin.ToString().PadLeft(2, '0'));
                    objAdd.Add(reendTime);
                    if (Convert.ToDateTime(txtReDate.Text) >= Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))) { objAdd.Add("0"); } else { objAdd.Add("2"); }
                    objAdd.Add(extddlVisitType.Text);
                    _bill_Sys_Calender.SaveEvent(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    ArrayList objGetEvent = new ArrayList();
                    objGetEvent.Add(txtCaseID.Text);
                    objGetEvent.Add(extddlDoctor.Text);
                    objGetEvent.Add(txtCompanyID.Text);
                    int reeventID = _bill_Sys_Calender.GetEventID(objGetEvent);
                    txtReEventID.Text= reeventID.ToString();
                    foreach (ListItem lstItem in ddlTestNames.Items)
                    {
                        if (lstItem.Selected == true)
                        {
                            objAdd = new ArrayList();
                            objAdd.Add(lstItem.Value);
                            objAdd.Add(reeventID);
                            if ( Convert.ToDateTime(txtReDate.Text) >= Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))) { objAdd.Add("0"); } else { objAdd.Add("2"); }
                            _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                        }
                    }
                    // Start : Save appointment Notes.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + txtReDate.Text;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                    lblMsg.Visible = true;
                    lblMsg.Text = "Appointment Added successfully.";

                }
                ////////////RE-Schedule add
                ////////////RE-Schedule Update
                else if (ddlStatus.SelectedValue == "1" && txtReEventID.Text != "")
                {
                    _bill_Sys_Calender = new Bill_Sys_Calender();
                    objAdd = new ArrayList();
                    objAdd.Add(txtReEventID.Text);//event Id
                    objAdd.Add(txtCaseID.Text);//Case Id
                    objAdd.Add(txtReDate.Text);//Appointment date
                    objAdd.Add(ddlReSchHours.SelectedValue.ToString() + "." + ddlReSchMinutes.SelectedValue.ToString());//Appointment time
                    objAdd.Add("");//Notes       
                    objAdd.Add(extddlDoctor.Text);
                    objAdd.Add("TY000000000000000003");
                    objAdd.Add(txtCompanyID.Text);

                    objAdd.Add(ddlReSchTime.SelectedValue);
                    objAdd.Add(reendHr.ToString().PadLeft(2, '0') + "." + reendMin.ToString().PadLeft(2, '0'));
                    objAdd.Add(reendTime);

                    if (Convert.ToDateTime(txtReDate.Text) >= Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))) { objAdd.Add("0"); } else { objAdd.Add("2"); } 
                    objAdd.Add(extddlVisitType.Text);
                    _bill_Sys_Calender.UPDATEEvent(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());

                    _bill_Sys_Calender.Delete_Event_RefferPrcedure(Convert.ToInt32(txtReEventID.Text));

                    foreach (ListItem lstItem in ddlTestNames.Items)
                    {
                        if (lstItem.Selected == true)
                        {
                            objAdd = new ArrayList();
                            objAdd.Add(lstItem.Value);
                            objAdd.Add(txtReEventID.Text);
                            if (ddlTestNames.Visible == true && Convert.ToDateTime(txtAppointmentDate.Text) >= Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))) { objAdd.Add("0"); } else { objAdd.Add("2"); }
                            _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                        }
                    }
                }
                ////////////RE-Schedule Update
                ////////////RE-Schedule

                 _bill_Sys_Calender = new Bill_Sys_Calender();
                objAdd = new ArrayList();
                objAdd.Add(eventId);//event Id
                objAdd.Add(txtCaseID.Text);//Case Id
                objAdd.Add(txtAppointmentDate.Text);//Appointment date
                objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());//Appointment time
                objAdd.Add("");//Notes       
                objAdd.Add(extddlDoctor.Text);
                objAdd.Add("TY000000000000000003");
                objAdd.Add(txtCompanyID.Text);

                    int endMin = Convert.ToInt32(ddlMinutes.SelectedValue) + Convert.ToInt32(30);
                    int endHr = Convert.ToInt32(ddlHours.SelectedValue);
                    string endTime = ddlTime.SelectedValue;
                    if (endMin >= 60)
                    {
                        endMin = endMin - 60;
                        endHr = endHr + 1;
                        if (endHr > 12)
                        {
                            endHr = endHr - 12;
                            if (ddlHours.SelectedValue != "12")
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
                            if (ddlHours.SelectedValue != "12")
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
                    objAdd.Add(ddlTime.SelectedValue);
                    objAdd.Add(endHr.ToString().PadLeft(2, '0') + "." + endMin.ToString().PadLeft(2, '0'));
                    objAdd.Add(endTime);


                    
                objAdd.Add(ddlStatus.SelectedValue); 
                objAdd.Add(extddlVisitType.Text);
                if (ddlStatus.SelectedValue == "1")
                {
                    objAdd.Add(txtReEventID.Text);
                    objAdd.Add(txtReDate.Text);
                }
                _bill_Sys_Calender.UPDATEEvent(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                _bill_Sys_Calender.Delete_Event_RefferPrcedure(Convert.ToInt32(eventId));

               
                ArrayList Olditems = new ArrayList();
                    foreach (ListItem lstItem in ddlTestNames.Items)
                    {

                        if (lstItem.Selected == true)
                        {
                            objAdd = new ArrayList();
                            objAdd.Add(lstItem.Value);
                            objAdd.Add(eventId);
                            if (ddlTestNames.Visible == true && Convert.ToDateTime(txtAppointmentDate.Text) >= Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))) { objAdd.Add("0"); } else { objAdd.Add("2"); }
                            _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                        }

                        if (ddlOldTestNames.Items.FindByValue(lstItem.Value).Selected == true && lstItem.Selected == false)
                        {
                            Olditems.Add(lstItem.Value);
                        }

                    }
               
                    //////////If appointment is updated from schedule to completed and remove any existing treatments
                    if (txtVisitStatus.Text == "0" && ddlStatus.SelectedValue != "0" && Olditems.Count>0)
                    {
                        _bill_Sys_Calender = new Bill_Sys_Calender();
                        objAdd = new ArrayList();
                        objAdd.Add(txtCaseID.Text);//Case Id
                        objAdd.Add(txtAppointmentDate.Text);//Appointment date
                        objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());//Appointment time
                        objAdd.Add("");//Notes       
                        objAdd.Add(extddlDoctor.Text);
                        objAdd.Add("TY000000000000000003");
                        objAdd.Add(txtCompanyID.Text);

                        objAdd.Add(ddlTime.SelectedValue);
                        objAdd.Add(endHr.ToString().PadLeft(2, '0') + "." + endMin.ToString().PadLeft(2, '0'));
                        objAdd.Add(endTime);
                        objAdd.Add("0");
                        objAdd.Add(extddlVisitType.Text);
                       
                        _bill_Sys_Calender.SaveEvent(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());

                        ArrayList objOldGetEvent = new ArrayList();
                        objOldGetEvent.Add(txtCaseID.Text);
                        objOldGetEvent.Add(extddlDoctor.Text);
                        objOldGetEvent.Add(txtCompanyID.Text);
                        int neweventID = _bill_Sys_Calender.GetEventID(objOldGetEvent);

                        foreach (Object obj in Olditems)
                        {
                            objAdd = new ArrayList();
                            objAdd.Add(obj.ToString());
                            objAdd.Add(neweventID);
                            objAdd.Add("0");
                            _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                        }
                       
                    }
                //////////If appointment is updated from schedule to completed and remove any existing treatments


                // Start : Save appointment Notes.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + txtAppointmentDate.Text;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                lblMsg.Visible = true;
                lblMsg.Text = "Appointment Updated successfully.";

                if (Request.QueryString["GRD_ID"] != null)
                    Session["GRD_ID"] = Request.QueryString["GRD_ID"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='Bill_SysPatientDesk.aspx';window.self.close(); </script>");
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
                    ddlReSchHours.Items.Add(i.ToString());
                }
                else
                {
                    ddlHours.Items.Add("0" + i.ToString());
                    ddlReSchHours.Items.Add("0" + i.ToString());

                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9)
                {
                    ddlMinutes.Items.Add(i.ToString());
                    ddlReSchMinutes.Items.Add(i.ToString());

                }
                else
                {
                    ddlMinutes.Items.Add("0" + i.ToString());
                    ddlReSchMinutes.Items.Add("0" + i.ToString());
                    
                }
            }
            ddlTime.Items.Add("AM");
            ddlTime.Items.Add("PM");
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
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (ddlStatus.SelectedValue == "1")
            {
                lblReDate.Visible = true;
                lblReTIme.Visible = true;
                txtReDate.Visible = true;
                ddlReSchHours.Visible = true;
                ddlReSchMinutes.Visible = true;
                ddlReSchTime.Visible = true;
            }
            else
            {
                lblReDate.Visible = false;
                lblReTIme.Visible = false;
                txtReDate.Visible = false;
                ddlReSchHours.Visible = false;
                ddlReSchMinutes.Visible = false;
                ddlReSchTime.Visible = false;
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
