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
using System.IO; 

public partial class Bill_Sys_PopupNewVisit : PageBase
{
    private CaseDetailsBO objCaseDetails;
    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    private ArrayList objAdd;
    private Bill_Sys_PatientBO objPatientBO;
    DataSet dset;

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
            if (!IsPostBack)
            {
                btnSave.Attributes.Add("onclick", "return formValidator('form1','ddldoctor,txtAppointmentDate,extddlVisitType,ddlHours');");
                dset = new DataSet();
                objCaseDetails = new CaseDetailsBO();
                string sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                string sz_company_id = (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID).ToString();
                string sz_location_Id = objCaseDetails.GetPatientLocationID(sz_case_id, sz_company_id);
                
                
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlVisitType.Flag_ID = txtCompanyID.Text;
                
               
                checkForReferringFacility();

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    lblOffice.Visible = true;
                    extddlMedicalOffice.Visible = true;
                    extddlRoom.Flag_Key_Value = "REFERRING_ROOM_LIST";
                    extddlRoom.Flag_ID = txtCompanyID.Text;
                    extddlReferringFacility.Flag_ID = txtCompanyID.Text.ToString();
                    extddlMedicalOffice.Flag_ID = txtCompanyID.Text.ToString();
                    objPatientBO = new Bill_Sys_PatientBO();
                    String szOfficeID = objPatientBO.Get_PatientOfficeID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID,txtCompanyID.Text);
                    if (szOfficeID != "")
                    {
                        extddlMedicalOffice.Text = szOfficeID;
                        extddlMedicalOffice.Enabled = false;
                    }
                    BindReferringFacilityControls();
                }
                else
                {
                    lblOffice.Visible = false;
                    extddlMedicalOffice.Visible = false;
                    extddlRoom.Flag_ID = txtCompanyID.Text;
                    extddlReferringFacility.Flag_ID = txtCompanyID.Text.ToString();
                    BindControls();
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
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_PopupNewVisit.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #region "Check For Referring Facility"

    public void checkForReferringFacility()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_RoomDays _objRD = new Bill_Sys_RoomDays();
            if (!_objRD.checkReferringFacility(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
            {
                extddlReferringFacility.Visible = false;
                lblReferringFacility.Visible = false;
                btnSave.Attributes.Add("onclick", "return formValidator('form1','ddldoctor,txtAppointmentDate,extddlVisitType,ddlHours');");
            }
            else
            {
                extddlReferringFacility.Visible = true;
                lblReferringFacility.Visible = true;
                btnSave.Attributes.Add("onclick", "return formValidator('form1','ddldoctor,txtAppointmentDate,extddlVisitType,ddlHours');");
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

    public void getDoctorDefaultList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();

            DataSet dsDoctorName = _obj.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            ddlDoctor.DataSource = dsDoctorName;
            ListItem objLI = new ListItem("---select---", "NA");
            ddlDoctor.DataTextField = "DESCRIPTION";
            ddlDoctor.DataValueField = "CODE";
            ddlDoctor.DataBind();
            ddlDoctor.Items.Insert(0, objLI);
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

        if ((extddlReferringFacility.Visible == true && extddlReferringFacility.Text != "NA") || (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true))
        {
            saveReferringFacility();
        }
        else
        {
        #region "Save normal funtionality"
        int iDateCount = 4;
        String szCompletedMsg = "";
        int iCompleted = 0;
        String szFutureUncompletedMsg = "";
        int iFutureUncompleted = 0;
        String szMustExist_Initial_EvaluationMsg = "";
        int iMustExist_Initial_Evaluation = 0;
        String szAlreadyExistInitial_Evaluation = "";
        int iAlreadyExistInitial_Evaluation = 0;
        String szAlreadyExistVisit = "";
        int iAlreadyExistVisit = 0;
        _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        objAdd = new ArrayList();
        try
        {
            string[] saAppointmentDate = txtAppointmentDate.Text.Split(',');
            foreach (Object dtAppointmentDate in saAppointmentDate)
            {
                if (dtAppointmentDate.ToString() != "")
                {
                    #region "Save Single Visit"

                    if (Convert.ToDateTime(dtAppointmentDate) > Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")) && ddlTestNames.Visible == false)
                    {
                     //   Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('Visit for future date cannot be added...!');</script>");
                        if (iFutureUncompleted == iDateCount)
                        {
                            szFutureUncompletedMsg = szFutureUncompletedMsg + dtAppointmentDate.ToString() + " , <br/>";
                            iFutureUncompleted = 0;
                        }
                        else
                        {
                            szFutureUncompletedMsg = szFutureUncompletedMsg + dtAppointmentDate.ToString() + " ,";
                            iFutureUncompleted++;
                        }
                    }
                    else
                    {
                        //////////

                        Boolean iEvisitExists = false;
                        Boolean visitExists = false;
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings.Get("MyConnectionString"));
                        SqlCommand comd = new SqlCommand("SP_CHECK_INITIALE_VALUATIONEXISTS");
                        comd.CommandType = CommandType.StoredProcedure;
                        comd.Connection = con;
                        comd.Connection.Open();
                        comd.Parameters.AddWithValue("@SZ_CASE_ID", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
                        comd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        comd.Parameters.AddWithValue("@SZ_PATIENT_ID", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                        comd.Parameters.AddWithValue("@SZ_DOCTOR_ID", ddlDoctor.SelectedValue.ToString());
                        comd.Parameters.AddWithValue("@VISIT_DATE", dtAppointmentDate);
                        //int intCountVisits = Convert.ToInt32(comd.ExecuteScalar());

                        SqlParameter objIEExists = new SqlParameter("@INITIAL_EXISTS", SqlDbType.Bit);
                        objIEExists.Direction = ParameterDirection.Output;
                        comd.Parameters.Add(objIEExists);
                        SqlParameter objVisitStatus = new SqlParameter("@VISIT_EXISTS", SqlDbType.Bit, 20);
                        objVisitStatus.Direction = ParameterDirection.Output;
                        comd.Parameters.Add(objVisitStatus);
                        comd.ExecuteNonQuery();
                        comd.Connection.Close();

                        iEvisitExists = Convert.ToBoolean(objIEExists.Value);
                        visitExists = Convert.ToBoolean(objVisitStatus.Value);
                        if (ddlTestNames.Visible == false)
                        {
                            if (iEvisitExists == false && extddlVisitType.Selected_Text != "IE")
                            {
                                if (iMustExist_Initial_Evaluation == iDateCount)
                                {
                                    szMustExist_Initial_EvaluationMsg = szMustExist_Initial_EvaluationMsg + dtAppointmentDate.ToString() + " , <br/>";
                                    iMustExist_Initial_Evaluation = 0;
                                }
                                else
                                {
                                    szMustExist_Initial_EvaluationMsg = szMustExist_Initial_EvaluationMsg + dtAppointmentDate.ToString() + " , ";
                                    iMustExist_Initial_Evaluation++;
                                }
                                continue;
                            }
                            if (iEvisitExists == true && extddlVisitType.Selected_Text == "IE")
                            {
                                if (iAlreadyExistInitial_Evaluation == iDateCount)
                                {
                                    szAlreadyExistInitial_Evaluation = szAlreadyExistInitial_Evaluation + dtAppointmentDate.ToString() + " , <br/>";
                                    iAlreadyExistInitial_Evaluation = 0;
                                }
                                else
                                {
                                    szAlreadyExistInitial_Evaluation = szAlreadyExistInitial_Evaluation + dtAppointmentDate.ToString() + " , ";
                                    iAlreadyExistInitial_Evaluation++;
                                }
                                continue;
                            }
                            if (visitExists == true)
                            {
                                if (iAlreadyExistVisit == iDateCount)
                                {
                                    szAlreadyExistVisit = szAlreadyExistVisit + dtAppointmentDate.ToString() + " , <br/>";
                                    iAlreadyExistVisit = 0;
                                }
                                else
                                {
                                    szAlreadyExistVisit = szAlreadyExistVisit + dtAppointmentDate.ToString() + " , ";
                                    iAlreadyExistVisit++;
                                }
                                continue;
                            }
                        }
                        /////////


                        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                        objAdd = new ArrayList();
                        objAdd.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);//Case Id
                        objAdd.Add(dtAppointmentDate);//Appointment date
                        if (ddlTestNames.Visible == true)
                        {
                            objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());//Appointment time
                        }
                        else
                        {
                            objAdd.Add("8.30");//Appointment time
                        }

                        objAdd.Add("");//Notes       
                        objAdd.Add(ddlDoctor.SelectedValue.ToString());
                        objAdd.Add("TY000000000000000003");
                        objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                        if (ddlTestNames.Visible == true)
                        {
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

                        }
                        else
                        {
                            objAdd.Add("AM");
                            objAdd.Add("9.00");
                            objAdd.Add("AM");

                        }

                        // if (ddlTestNames.Visible == true && Convert.ToDateTime(dtAppointmentDate) >= Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))) { objAdd.Add("0"); } else { objAdd.Add("2"); }
                        if (ddlTestNames.Visible == true) { objAdd.Add(ddlStatus.SelectedValue);  }
                        else { objAdd.Add("2"); }
                        objAdd.Add(extddlVisitType.Text);
                        _bill_Sys_Calender.SaveEvent(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());

                        //int i_return = _bill_Sys_Visit_BO.InsertVisit(objAdd);

                        ArrayList objGetEvent = new ArrayList();
                        objGetEvent.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
                        objGetEvent.Add(ddlDoctor.SelectedValue.ToString());
                        objGetEvent.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        int eventID = _bill_Sys_Calender.GetEventID(objGetEvent);
                        if (ddlTestNames.Visible == true)
                        {
                           foreach (ListItem lstItem in ddlTestNames.Items)
                            {
                                if (lstItem.Selected == true)
                                {
                                    objAdd = new ArrayList();
                                    objAdd.Add(lstItem.Value);
                                    objAdd.Add(eventID);
                                    //if (Convert.ToDateTime(dtAppointmentDate) >= Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))) { objAdd.Add("0"); } else { objAdd.Add("2"); }
                                    if (ddlTestNames.Visible == true) { objAdd.Add(ddlStatus.SelectedValue); }
                                    else { objAdd.Add("2"); }
                                    _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                                }
                            }
                        }
                        // Start : Save appointment Notes.

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + dtAppointmentDate;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                        szCompletedMsg = szCompletedMsg + dtAppointmentDate + " , ";

                    }

                    #endregion
                }
            }
            if (szFutureUncompletedMsg == "" && szMustExist_Initial_EvaluationMsg == "" && szAlreadyExistInitial_Evaluation == "" && szAlreadyExistVisit == "")
            {
                lblMsg.Text = "Appointment scheduled successfully.";

                ClearControl();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='Bill_SysPatientDesk.aspx';window.self.close(); </script>");
            }
            else
            {
                if (szCompletedMsg != "")
                {
                    lblMsg.Text = lblMsg.Text + szCompletedMsg + " -- Completed.<br/>";
                }
                if(szFutureUncompletedMsg!="")
                {
                    lblMsg.Text = lblMsg.Text + szFutureUncompletedMsg + " -- Visit for future date cannot be added.<br/>";
                }
                if(szMustExist_Initial_EvaluationMsg != "")
                {
                    if (iMustExist_Initial_Evaluation > 2)
                    {
                        lblMsg.Text = lblMsg.Text + szMustExist_Initial_EvaluationMsg + "<br/> -- Schedule can not be saved patient is visiting first time hence there visit type should be Initial Evaluation.<br/>";
                    }
                    else
                    {
                        lblMsg.Text = lblMsg.Text + szMustExist_Initial_EvaluationMsg + " -- Schedule can not be saved patient is visiting first time hence there visit type should be Initial Evaluation.<br/>";
                    }
                }
                if(szAlreadyExistInitial_Evaluation != "")
                {
                    lblMsg.Text = lblMsg.Text + szAlreadyExistInitial_Evaluation + " -- Schedule can not be saved because patient already has Initial Evaluation.<br/>";
                }
                if(szAlreadyExistVisit != "")
                {
                    lblMsg.Text = lblMsg.Text + szAlreadyExistVisit + " -- Schedule can not be saved because patient already has this visit.<br/>";
                }
            }
            lblMsg.Focus();
            lblMsg.Visible = true;
        }//try
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
        #endregion
    
        }
    }
 
    private void SaveEvents()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender _bill_Sys_Calender;
        try
        {
            _bill_Sys_Calender = new Bill_Sys_Calender();
            ArrayList objGetEvent = new ArrayList();
            objGetEvent.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            objGetEvent.Add(ddlDoctor.SelectedValue.ToString());
            objGetEvent.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            int eventID = _bill_Sys_Calender.GetEventID(objGetEvent);

            if (ddlTestNames.Visible == true)
            {

                objAdd = new ArrayList();
                objAdd.Add(eventID);
                objAdd.Add(false);
                objAdd.Add(2);
                _bill_Sys_Calender.UPDATE_Event_Status(objAdd);
                foreach (ListItem lstItem in ddlTestNames.Items)
                {
                    if (lstItem.Selected == true)
                    {
                        objAdd = new ArrayList();
                        objAdd.Add(lstItem.Value);
                        objAdd.Add(eventID);
                        objAdd.Add(2);
                        _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                    }
                }
            }
            // Start : Save appointment Notes.

            _DAO_NOTES_EO = new DAO_NOTES_EO();
            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + txtAppointmentDate.Text;

            _DAO_NOTES_BO = new DAO_NOTES_BO();
            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
            ClearControl();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='Bill_SysPatientDesk.aspx';window.self.close(); </script>");            
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

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtAppointmentDate.Text = "";
            extddlVisitType.Text = "NA";
            ddlTestNames.Items.Clear();
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
            ddlHours.Items.Clear();
            ddlMinutes.Items.Clear();
            ddlTime.Items.Clear();
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

    #region "Bind Procedure codes for referring facility"

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
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                objArr.Add(txtCompanyID.Text);
            }
            else
            {
                objArr.Add(extddlReferringFacility.Text);
            }
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

    #region "Bind Referring Doctor"

    protected void BindReferringDoctor()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();
            objCaseDetails = new CaseDetailsBO();

            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            {
                DataSet dsDoctorName = _obj.GetReferralDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "GETDOCTORLIST");
                ddlDoctor.DataSource = dsDoctorName;
                ListItem objLI = new ListItem("---select---", "NA");
                ddlDoctor.DataTextField = "DESCRIPTION";
                ddlDoctor.DataValueField = "CODE";
                ddlDoctor.DataBind();
                ddlDoctor.Items.Insert(0, objLI);
            }
            else
            {
                string sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                string sz_company_id = (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID).ToString();
                string sz_location_Id = objCaseDetails.GetPatientLocationID(sz_case_id, sz_company_id);
                DataSet dset = objCaseDetails.DoctorName(sz_location_Id, sz_company_id);
                ddlDoctor.DataSource = dset;
                ddlDoctor.DataTextField = "DESCRIPTION";
                ddlDoctor.DataValueField = "CODE";
                ddlDoctor.DataBind();
                ListItem li = new ListItem();
                li.Value = "NA";
                li.Text = "--- Select ----";
                ddlDoctor.Items.Insert(0, li);
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

    #region "Save Referring facility event"

    protected void saveReferringFacility()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Boolean blDontKnowFunc = false;

        #region "Save Referring Facility"
        _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        String szError = "";
        String szProviousDateError = ""; 
        ArrayList objALTimeRangeError = new ArrayList();
        try
        {
            string[] saAppointmentDate = txtAppointmentDate.Text.Split(',');
            foreach (Object dtAppointmentDate in saAppointmentDate)
            {
                if (dtAppointmentDate.ToString() != "")
                {

                    #region "Check for Previous Date"
                    String szCurrentDateTime = hdnCurrentDate.Value;
                    DateTime dtClientDate = Convert.ToDateTime(hdnCurrentDate.Value);
                    DateTime dtEnteredDate = Convert.ToDateTime(Convert.ToDateTime(dtAppointmentDate).ToString("MM/dd/yyyy") + " " + ddlHours.SelectedValue + ":" + ddlMinutes.SelectedValue + " " + ddlTime.SelectedValue);
                    DateTime dtEnteredLastDate = dtEnteredDate.AddMinutes(30);

                    //if (dtEnteredDate.CompareTo(dtClientDate) < 0)
                    //{
                    //    szProviousDateError = szProviousDateError + Convert.ToDateTime(Convert.ToDateTime(dtAppointmentDate).ToString("MM/dd/yyyy") + " " + ddlHours.SelectedValue + ":" + ddlMinutes.SelectedValue + " " + ddlTime.SelectedValue) + " , ";
                    //    continue;
                    //}
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
                    objChekList.Add(Convert.ToDateTime(dtAppointmentDate).ToString("MM/dd/yyyy"));
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        objChekList.Add(txtCompanyID.Text);
                    }
                    else
                    {
                        objChekList.Add(extddlReferringFacility.Text);
                    }
                    objChekList.Add(StartTime);
                    objChekList.Add(EndTime);
                    if (!objRD.checkRoomTiming(objChekList))
                    {
                        objALTimeRangeError.Add(Convert.ToDateTime(dtAppointmentDate).ToString("MM/dd/yyyy"));
                        szError = szError + Convert.ToDateTime(dtAppointmentDate).ToString("MM/dd/yyyy") + " , ";
                        continue;
                    }


                    #endregion


                    string szDoctorID = "";
                    if (blDontKnowFunc)
                    {
                        #region "Add Referal Doctor"

                        Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
                        ArrayList arrOBJ = new ArrayList();

                        arrOBJ.Add(ddlDoctor.Text);
                        arrOBJ.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                        arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        szDoctorID = _bill_Sys_ReferalEvent.AddDoctor(arrOBJ);

                        #endregion

                        #region "Add Doctor Amount"
                        foreach (ListItem lst in ddlTestNames.Items)
                        {
                            if (lst.Selected == true)
                            {
                                arrOBJ = new ArrayList();
                                arrOBJ.Add(szDoctorID);
                                arrOBJ.Add(lst.Value);
                                arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                arrOBJ.Add(lst.Value);
                                _bill_Sys_ReferalEvent.AddDoctorAmount(arrOBJ);

                                arrOBJ = new ArrayList();
                                arrOBJ.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                                arrOBJ.Add(szDoctorID);
                                arrOBJ.Add(Convert.ToDateTime(dtAppointmentDate).ToString("MM/dd/yyyy"));
                                arrOBJ.Add(lst.Value);
                                arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                arrOBJ.Add(ddlType.SelectedValue);
                                _bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        szDoctorID = extddlReferringDoctor.SelectedItem.Value;
                    }

                    #region "Save Event"

                    string sz_date = Convert.ToDateTime(dtAppointmentDate).ToString("MM/dd/yyyy");
                    Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                    ArrayList objAdd = new ArrayList();
                    objAdd.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID); // Patient ID
                    objAdd.Add(sz_date); // Event Date
                    objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());
                    objAdd.Add(txtNotes.Text);
                    objAdd.Add(szDoctorID);
                    objAdd.Add(ddlTestNames.SelectedValue);
                    objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    objAdd.Add(ddlTime.SelectedValue);
                    objAdd.Add(dtEnteredLastDate.ToString("hh") + "." + dtEnteredLastDate.ToString("mm"));
                    objAdd.Add(dtEnteredLastDate.ToString("tt"));
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        objAdd.Add(txtCompanyID.Text);
                    }
                    else
                    {
                        objAdd.Add(extddlReferringFacility.Text);
                    }
                    objAdd.Add("False");
                    //objAdd.Add(chkTransportation.Checked);

                    // 16 April -- sachin
                    if (chkTransportation.Checked == true) { objAdd.Add(1); } else { objAdd.Add(0); }

                    if (chkTransportation.Checked == true) { objAdd.Add(Convert.ToInt32(extddlTransport.Text)); } else { objAdd.Add(null); }
                    //==============================================
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        objAdd.Add(extddlMedicalOffice.Text);
                    }


                    int eventID = _bill_Sys_Calender.Save_Event(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());

                    foreach (ListItem lst in ddlTestNames.Items)
                    {
                        if (lst.Selected == true)
                        {
                            objAdd = new ArrayList();
                            objAdd.Add(lst.Value);
                            objAdd.Add(eventID);
                            objAdd.Add(0);
                            _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                        }
                    }


                    #endregion


                    #region "Save appointment Notes."

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + sz_date;


                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                    #endregion

                    //if (flUpload.FileName != "")
                    //{
                    //    #region "Document Manager Node"



                    //    Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();

                    //    String szDefaultPath = objNF3Template.getPhysicalPath();
                    //    String szDestinationDir = "";

                    //    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                    //    {
                    //        szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    //    }
                    //    else
                    //    {
                    //        szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    //    }

                    //    szDestinationDir = szDestinationDir + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/No Fault File/Referral Sheet-INI Report/";

                    //    if (!Directory.Exists(szDefaultPath + szDestinationDir))
                    //    {
                    //        Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                    //    }

                    //    if (!File.Exists(szDefaultPath + szDestinationDir + flUpload.FileName))
                    //    {
                    //        flUpload.SaveAs(szDefaultPath + szDestinationDir + flUpload.FileName);

                    //        ArrayList objAL = new ArrayList();
                    //        //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                    //        //{
                    //        //    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    //        //}
                    //        //else
                    //        //{
                    //        //    objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    //        //}

                    //        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                    //        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
                    //        objAL.Add(flUpload.FileName);
                    //        objAL.Add(szDestinationDir);
                    //        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                    //        objAL.Add("Referral Sheet-INI Report");
                    //        objNF3Template.saveOutScheduleReportInDocumentManager(objAL);

                    //    }
                    //    #endregion
                    //}
                }
            }

            if (szError == "" && szProviousDateError == "")
            {
                lblMsg.Text = "Appointment scheduled successfully.";
                ClearControl();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='Bill_SysPatientDesk.aspx';window.self.close(); </script>");
            }
            else
            {
                if (objALTimeRangeError != null)
                {
                    Bill_Sys_RoomDays objRD = new Bill_Sys_RoomDays();
                    lblMsg.Text = " Add appointment between ---> ";
                    for (int ii = 0; ii < objALTimeRangeError.Count; ii++)
                    {
                        String szStartTime ="";
                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        {
                            szStartTime  = objRD.getRoomStart_EndTime(extddlRoom.Text, objALTimeRangeError[ii].ToString(), txtCompanyID.Text);
                        }
                        else
                        {
                            szStartTime = objRD.getRoomStart_EndTime(extddlRoom.Text, objALTimeRangeError[ii].ToString(), extddlReferringFacility.Text);
                        }
                        
                        if (szStartTime == "")
                        {
                            lblMsg.Text = lblMsg.Text + "<br/> " + objALTimeRangeError[ii].ToString() + " --- Holiday";
                        }
                        else
                        {
                            lblMsg.Text = lblMsg.Text + "<br/> " + objALTimeRangeError[ii].ToString() + " --- " + szStartTime;
                        }
                    }
                }
                if (szProviousDateError != "")
                {
                    lblMsg.Text = lblMsg.Text + "</br>" + szProviousDateError + " : Cannot add schedule on Previous Date and Time";
                }
                lblMsg.Focus();
                lblMsg.Visible = true;
            }

        }//try
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
        #endregion
    }


    #endregion

    #region "Drop Down Event"

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

    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        try
        {
            if (_bill_Sys_Calender.CheckReferralExists(ddlDoctor.SelectedValue.ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == true)
            {
                ddlTestNames.DataSource = _bill_Sys_Calender.GetReferringProcCodeList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlDoctor.SelectedValue.ToString());
                ddlTestNames.DataTextField = "DESCRIPTION";
                ddlTestNames.DataValueField = "CODE";
                ddlTestNames.DataBind();
                BindTimeControl();

                // Set default 'C' for Visit Type
                Bill_Sys_PatientVisitBO objPV = new Bill_Sys_PatientVisitBO();
                extddlVisitType.Text = objPV.GetDefaultVisitType(txtCompanyID.Text);

                // Make Visit Type visible false for Referal Doctor
                lblVisitType.Visible = false;
                extddlVisitType.Visible = false;
                lblTIme.Visible = true;
                ddlHours.Visible = true;
                ddlMinutes.Visible = true;
                ddlTime.Visible = true;
                lblProcedure.Visible = true;
                ddlTestNames.Visible = true;
                lblVisitStatus.Visible = true;
                ddlStatus.Visible = true;
                //ddlTestNames.DataSource = _bill_Sys_Calender.GetAssociatedProcCodeList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extddlDoctor.Text, "GETALLCODE", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, "");
                //ddlTestNames.DataTextField = "DESCRIPTION";
                //ddlTestNames.DataValueField = "CODE";
                //ddlTestNames.DataBind();
                //BindTimeControl();


            }
            else
            {
                extddlVisitType.Text = "NA";
                lblVisitType.Visible = true;
                extddlVisitType.Visible = true;
                lblTIme.Visible = false;
                ddlHours.Visible = false;
                ddlMinutes.Visible = false;
                ddlTime.Visible = false;
                lblProcedure.Visible = false;
                ddlTestNames.Visible = false;
                lblVisitStatus.Visible = false;
                ddlStatus.Visible = false;
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
    
    protected void extddlReferralDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        try
        {
            if (_bill_Sys_Calender.CheckReferralExists(ddlDoctor.SelectedValue.ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == true)
            {
                ddlTestNames.DataSource = _bill_Sys_Calender.GetReferringProcCodeList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlDoctor.SelectedValue.ToString());
                ddlTestNames.DataTextField = "DESCRIPTION";
                ddlTestNames.DataValueField = "CODE";
                ddlTestNames.DataBind();
                BindTimeControl();

                // Set default 'C' for Visit Type
                Bill_Sys_PatientVisitBO objPV = new Bill_Sys_PatientVisitBO();
                extddlVisitType.Text = objPV.GetDefaultVisitType(txtCompanyID.Text);

                // Make Visit Type visible false for Referal Doctor
                lblVisitType.Visible = false;
                extddlVisitType.Visible = false;
                lblTIme.Visible = true;
                ddlHours.Visible = true;
                ddlMinutes.Visible = true;
                ddlTime.Visible = true;
                lblProcedure.Visible = true;
                ddlTestNames.Visible = true;
                lblVisitStatus.Visible = true;
                ddlStatus.Visible = true;
            }
            else
            {
                extddlVisitType.Text = "NA";
                lblVisitType.Visible = true;
                extddlVisitType.Visible = true;
                lblTIme.Visible = false;
                ddlHours.Visible = false;
                ddlMinutes.Visible = false;
                ddlTime.Visible = false;
                lblProcedure.Visible = false;
                ddlTestNames.Visible = false;
                lblVisitStatus.Visible = false;
                ddlStatus.Visible = false;
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
    
    protected void extddlReferringFacility_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindControls(); 
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

    private void BindReferringFacilityControls()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            #region "Bind referring Doctor only"
            btnSave.Attributes.Add("onclick", "return formValidator('form1','extddlReferringFacility,extddlRoom,extddlReferringDoctor,txtAppointmentDate,ddlHours,ddlTestNames,flUpload');");
            extddlRoom.Visible = true;
            extddlRoom.Flag_ID = txtCompanyID.Text;
            BindReferringDoctor();
            //lblINIReport.Visible = true;
            //flUpload.Visible = true;
            lblNotes.Visible = true;
            txtNotes.Visible = true;
            lblRoom.Visible = true;
            BindReferringFacilityDoctor();
            extddlReferringDoctor.Visible = true;
            ddlDoctor.Visible = false;
            BindTimeControl();
            lblTIme.Visible = true;
            ddlHours.Visible = true;
            ddlMinutes.Visible = true;
            ddlTime.Visible = true;
            lblVisitType.Visible = false;
            extddlVisitType.Visible = false;
            lblProcedure.Visible = true;

            lblTransport.Visible = false;
            chkTransportation.Visible = false;
            extddlTransport.Visible = false;
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

    private void BindControls()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            if ((extddlReferringFacility.Visible == false || extddlReferringFacility.Text == "NA" || extddlReferringFacility.Text == ""))
            {
                btnSave.Attributes.Add("onclick", "return formValidator('form1','ddldoctor,txtAppointmentDate,extddlVisitType,ddlHours');");
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
                {
                    getDoctorDefaultList();
                }
                else
                {
                    dset = new DataSet();
                    objCaseDetails = new CaseDetailsBO();
                    string sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                    string sz_company_id = (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID).ToString();
                    string sz_location_Id = objCaseDetails.GetPatientLocationID(sz_case_id, sz_company_id);

                    dset = objCaseDetails.DoctorName(sz_location_Id, sz_company_id);
                    ddlDoctor.DataSource = dset;
                    ddlDoctor.DataTextField = "DESCRIPTION";
                    ddlDoctor.DataValueField = "CODE";
                    ddlDoctor.DataBind();
                    ListItem li = new ListItem();
                    li.Value = "NA";
                    li.Text = "--- Select ----";
                    ddlDoctor.Items.Insert(0, li);
                }
                extddlVisitType.Text = "NA";
                lblVisitType.Visible = true;
                extddlVisitType.Visible = true;
                lblTIme.Visible = false;
                ddlHours.Visible = false;
                ddlMinutes.Visible = false;
                ddlTime.Visible = false;
                lblProcedure.Visible = false;
                ddlTestNames.Visible = false;
                lblVisitStatus.Visible = false;
                ddlStatus.Visible = false;
                //lblINIReport.Visible = false;
                //flUpload.Visible = false;
                lblNotes.Visible = false;
                txtNotes.Visible = false;
                extddlRoom.Visible = false;
                lblRoom.Visible = false;
                extddlReferringDoctor.Visible = false;
                ddlDoctor.Visible = true;
                lblTIme.Visible = false;
                ddlHours.Visible = false;
                ddlMinutes.Visible = false;
                ddlTime.Visible = false;
                lblVisitType.Visible = true;
                extddlVisitType.Visible = true;
                lblTransport.Visible = false;
                chkTransportation.Visible = false;
            }
            else
            {
                btnSave.Attributes.Add("onclick", "return formValidator('form1','extddlReferringFacility,extddlRoom,extddlReferringDoctor,txtAppointmentDate,ddlHours,ddlTestNames,flUpload');");
                extddlRoom.Visible = true;
                extddlRoom.Flag_ID = txtCompanyID.Text;
                BindReferringDoctor();
                //lblINIReport.Visible = true;
                //flUpload.Visible = true;
                lblNotes.Visible = true;
                txtNotes.Visible = true;
                lblRoom.Visible = true;
                //extddlReferringDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                BindReferringFacilityDoctor();
                extddlReferringDoctor.Visible = true;
                ddlDoctor.Visible = false;
                BindTimeControl();
                lblTIme.Visible = true;
                ddlHours.Visible = true;
                ddlMinutes.Visible = true;
                ddlTime.Visible = true;
                lblVisitType.Visible = false;
                extddlVisitType.Visible = false;
                lblProcedure.Visible = true;
                lblTransport.Visible = true;
                chkTransportation.Visible = true;
            }
            #endregion


            if (extddlReferringFacility.Text != "NA" && extddlReferringFacility.Text != "")
            {
                lblTransport.Visible = true;
                chkTransportation.Visible = true;
                extddlTransport.Flag_ID = extddlReferringFacility.Text;
            }
            else
            {
                lblTransport.Visible = false;
                chkTransportation.Visible = false;
                extddlTransport.Visible = false;
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

    

    private void BindReferringFacilityDoctor()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();
            objCaseDetails = new CaseDetailsBO();

            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            {
                DataSet dsDoctorName = _obj.GetReferralDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "GETDOCTORLIST");
                extddlReferringDoctor.DataSource = dsDoctorName;
                ListItem objLI = new ListItem("---select---", "NA");
                extddlReferringDoctor.DataTextField = "DESCRIPTION";
                extddlReferringDoctor.DataValueField = "CODE";
                extddlReferringDoctor.DataBind();
                extddlReferringDoctor.Items.Insert(0, objLI);
            }
            else
            {
                string sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                string sz_company_id = (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID).ToString();
                string sz_location_Id = objCaseDetails.GetPatientLocationID(sz_case_id, sz_company_id);
                DataSet dset = objCaseDetails.DoctorName(sz_location_Id, sz_company_id);
                extddlReferringDoctor.DataSource = dset;
                extddlReferringDoctor.DataTextField = "DESCRIPTION";
                extddlReferringDoctor.DataValueField = "CODE";
                extddlReferringDoctor.DataBind();
                ListItem li = new ListItem();
                li.Value = "NA";
                li.Text = "--- Select ----";
                extddlReferringDoctor.Items.Insert(0, li);
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
            }
            else
            {
                extddlTransport.Visible = false;
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
