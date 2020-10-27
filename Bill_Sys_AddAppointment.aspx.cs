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
using System.Data.Sql;
using System.Data.SqlClient;
using Componend;
using System.IO;

public partial class Bill_Sys_AddAppointment : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private DataSet ds;
    private Patient_TVBO _patient_TVBO;
    private String strcompanyid;
    private PopupBO _popupBO;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;

    protected void Page_Load(object sender, EventArgs e)
    {
        //btnSave.Attributes.Add("onclick", "return formValidator('form1','txtPatientFName,txtPatientLName,extddlInsuranceCompany,extddlDoctor,ddlType,ddlTestNames');");
        txtTodayYear.Text = DateTime.Now.ToString("yyyy");
        txtTodayMonth.Text = DateTime.Now.ToString("MM");
        txtTodayDay.Text = DateTime.Now.ToString("dd");
        txtTodayHour.Text = DateTime.Now.ToString("HH");
        txtTodayMin.Text = DateTime.Now.ToString("mm");

        if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
        {
            lblChartNumber.Visible = true;
            txtRefChartNumber.Visible = true;
            
        }
        else
        {
            lblChartNumber.Visible = false;
            txtRefChartNumber.Visible = false;
        }


        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
        {
            // btnSave.Attributes.Add("onclick", "return formValidator('form1','flUpload,txtPatientFName,txtPatientLName,extddlMedicalOffice,extddlDoctor,ddlType,ddlTestNames,ddlHours,ddlEndHours');");

            if (Session["Flag"] != null)
            {
                btnSave.Attributes.Add("onclick", "return ValidateAllField('');");
            }else
            {
                btnSave.Attributes.Add("onclick", "return ValidateAllField('RF');");
                btnAddPatient.Visible = true;
            }
        }
        else
        {
           
            btnUpdate.Attributes.Add("onclick", "return formValidator('form1','extddlDoctor');");
            btnSave.Attributes.Add("onclick", "return ValidateAllField('');");
            btnAddPatient.Visible = false;
            extddlPatientState.Enabled = false;
        }
       // btnUpdate.Attributes.Add("onclick", "return formValidator('form1','txtPatientFName,txtPatientLName,extddlDoctor,ddlType,ddlTestNames');");
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        PopupBO _popupBO = new PopupBO();
        if (Session["CASE_OBJECT"]!= null)
        {
            ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID = _popupBO.GetCompanyID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);
        }
        else
        {
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            _bill_Sys_CaseObject.SZ_COMAPNY_ID =((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
        }

        if (!IsPostBack)
        {
            
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlInsuranceCompany.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            // 15 April 2010 Add transport dropdown list in page for save transport facility for perticular patient- sachin
            //extddlTransport.Flag_ID = txtCompanyID.Text.ToString();
            //========================================================================
            BindTimeControl();
            string sz_datetime = Request.QueryString["_date"].ToString();
            if (sz_datetime.Substring(sz_datetime.IndexOf(" ") + 2, 1) == ".")
            {
                ddlHours.SelectedValue = sz_datetime.Substring(sz_datetime.IndexOf(" "), 2).Replace(" ", "0");
            }
            else
            {
                ddlHours.SelectedValue = sz_datetime.Substring(sz_datetime.IndexOf(" ")+1, 2);
            }
            ddlMinutes.SelectedValue = sz_datetime.Substring(sz_datetime.IndexOf(".") + 1, 2);
            ddlTime.SelectedValue = sz_datetime.Substring(sz_datetime.IndexOf("|") + 1, 2);
            extddlReferenceFacility.Text = Session["TestFacilityID"].ToString();
            LoadTypes();
            
            int endMin=Convert.ToInt32(ddlMinutes.SelectedValue) + Convert.ToInt32(Session["INTERVAL"].ToString());
            int endHr = Convert.ToInt32(ddlHours.SelectedValue);
            string  endTime=ddlTime.SelectedValue;
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
           
            ddlEndHours.SelectedValue = endHr.ToString().PadLeft(2,'0');
            ddlEndMinutes.SelectedValue = endMin.ToString().PadLeft(2,'0');
            ddlEndTime.SelectedValue = endTime.ToString();

            
            if (Session["CASE_OBJECT"] != null)
            {
                _popupBO = new PopupBO();
                strcompanyid = _popupBO.GetCompanyID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                
            }
            if (Session["Flag"] != null)
            {
                _patient_TVBO = new Patient_TVBO();
                Session["PatientDataList"] = _patient_TVBO.GetSelectedPatientDataList(strcompanyid, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                SearchPatientList();
                btnClickSearch.Visible = false;
                btnAddPatient.Visible = false;
                extddlPatientState.Enabled = false;

            }
            else if (Session["DataEntryFlag"] != null)
            {
                _patient_TVBO = new Patient_TVBO();
                Session["PatientDataList"] = _patient_TVBO.GetSelectedPatientDataList(strcompanyid, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                SearchPatientList();
                btnClickSearch.Visible = false;
                btnAddPatient.Visible = false;
                Session["DataEntryFlag"] = null;
            }
            else
            {
                _patient_TVBO = new Patient_TVBO();
                Session["PatientDataList"] = _patient_TVBO.GetPatientDataList(txtCompanyID.Text);
                ///////////////////////////
                txtPatientID.Text = "";
                txtPatientFName.Text = "";
                txtMI.Text = "";
                txtPatientLName.Text = "";
                txtPatientPhone.Text = "";
                txtPatientAddress.Text = "";
                txtState.Text = "";
                txtBirthdate.Text = "";
                txtPatientAge.Text = "";
                txtSocialSecurityNumber.Text = "";
                txtCaseID.Text = "";
                extddlCaseType.Text = "NA";
                extddlInsuranceCompany.Text = "NA";
                //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                //{
                //    extddlDoctor.Visible = false;
                //    txtDoctorName.Visible = true;
                //}
                //////////////////////////
            }
           
            extddlMedicalOffice.Flag_ID = txtCompanyID.Text.ToString();
            extddlReferenceFacility.Flag_ID = txtCompanyID.Text.ToString();
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                extddlMedicalOffice.Visible = true;
                extddlReferenceFacility.Visible = false;
                lblTypetext.Visible = false;
                ddlType.Visible = false;
                lblTestFacility.Text = "Office Name";
            }
            
            extddlCaseStatus.Text = GetOpenCaseStatus();
            ////////////////
            if (sz_datetime.IndexOf("~") > 0)
            {
                string scheduledID="";
                if (sz_datetime.IndexOf("^") > 0)
                {
                    scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, ((sz_datetime.IndexOf("^")) - (sz_datetime.IndexOf("~") + 1)));
                }
                else {
                    scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, (sz_datetime.Length - (sz_datetime.IndexOf("~") + 1)));
                }
                _patient_TVBO = new Patient_TVBO();
                if (_patient_TVBO.getScheduleStatus(scheduledID))
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Visit Completed.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                Session["SCHEDULEDID"] = scheduledID;
                GETAppointPatientDetail(Convert.ToInt32(scheduledID));
                btnClickSearch.Visible = false;
                btnAddPatient.Visible = false;
                tdSerach.Visible = false;
                tdSerach.Height = "0px";
            }
            ///////////////

            #region "Check for pervious Date"

            
            string[] szArr;
            szArr = Request.QueryString["_date"].ToString().Split(' ');
            
            DateTime dtTodayDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy HH:mm tt"));
            DateTime sz_Evt_date = Convert.ToDateTime(Convert.ToDateTime(szArr[0].ToString()).ToString("MM/dd/yyyy") + " " + ddlHours.SelectedValue.ToString() + ":" + ddlMinutes.SelectedValue.ToString() + " " + ddlTime.SelectedValue.ToString());


            if (DateTime.Compare(sz_Evt_date, dtTodayDate) > 0)
            {
                txtPreviousDateFlag.Text = "future date";
            }
            else
            {
                txtPreviousDateFlag.Text = "previous date";
            }

            #endregion

        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_AddAppointment.aspx");
        }
        #endregion
        
    }

    private string GetOpenCaseStatus()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlda;
        SqlDataReader dr;
        DataSet ds;
        string caseStatusID = "";
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("Select SZ_CASE_STATUS_ID FROM MST_CASE_STATUS WHERE SZ_STATUS_NAME='OPEN' AND SZ_COMPANY_ID='" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'", sqlCon);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                caseStatusID = Convert.ToString(dr[0].ToString());
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
        return caseStatusID;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           // string id = Request.QueryString["_date"].ToString();
            //string caseid = Request.QueryString["CaseId"].ToString();
            if (ddlType.SelectedValue == "TY000000000000000001")
            {
               // Label1.Text = "Visits";
               // LoadTypes("visits");

            }
            else if (ddlType.SelectedValue == "TY000000000000000002")
            {
                //Label1.Text = "Treatments";
               // LoadTypes("treatments");

            }
            else if (ddlType.SelectedValue == "TY000000000000000003")
            {
               // Label1.Text = "Tests";
               // LoadTypes("tests");

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

    //private void LoadTypes(string p_szCodeType)
    //{
    //    ArrayList objArr = new ArrayList();
    //    objArr.Add(extddlDoctor.Text);
    //    objArr.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //    string szKeyCode = "";


    //    if (p_szCodeType.CompareTo("visits") == 0)
    //    {
    //        szKeyCode = "DOCTORS_VISITS";
    //    }
    //    else if (p_szCodeType.CompareTo("treatments") == 0)
    //    {
    //        szKeyCode = "DOCTORS_TREATMENTS";
    //    }
    //    else if (p_szCodeType.CompareTo("tests") == 0)
    //    {
    //        szKeyCode = "DOCTORS_TESTS";
    //    }
    //    objArr.Add(szKeyCode);
    //    string sz_datetime = Request.QueryString["_date"].ToString();
       
    //    objArr.Add(sz_datetime.Substring(sz_datetime.IndexOf("!")+1, 20));
    //    Bill_Sys_ManageVisitsTreatmentsTests_BO objBO = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
    //    ddlTestNames.Items.Clear();
    //    ddlTestNames.DataSource = objBO.GetDoctorSpecific_TypeList(objArr);
    //    ddlTestNames.DataTextField = "description";
    //    ddlTestNames.DataValueField = "code";
    //    ddlTestNames.DataBind();
    //    ddlTestNames.Items.Insert(0, "--- Select ---");
    //    ddlTestNames.Visible = true;

    //}

    private void LoadTypes()
    {
        ArrayList objArr = new ArrayList();

        objArr.Add(extddlReferenceFacility.Text);
        string sz_datetime = Request.QueryString["_date"].ToString();

            objArr.Add(sz_datetime.Substring(sz_datetime.IndexOf("!")+1, 20));
        Bill_Sys_ManageVisitsTreatmentsTests_BO objBO = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
        ddlTestNames.Items.Clear();
        ddlTestNames.DataSource = objBO.GetReferringProcCodeList(objArr);
        ddlTestNames.DataTextField = "description";
        ddlTestNames.DataValueField = "code";
        ddlTestNames.DataBind();
        ddlTestNames.Items.Insert(0, "--- Select ---");
        ddlTestNames.Visible = true;

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
                    ddlEndHours.Items.Add(i.ToString());
                }
                else
                {
                    ddlHours.Items.Add("0" + i.ToString());
                    ddlEndHours.Items.Add("0" + i.ToString());
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9)
                {
                    ddlMinutes.Items.Add(i.ToString());
                    ddlEndMinutes.Items.Add(i.ToString());
                }
                else
                {
                    ddlMinutes.Items.Add("0" + i.ToString());
                    ddlEndMinutes.Items.Add("0" + i.ToString());
                }
            }
            ddlTime.Items.Add("AM");
            ddlTime.Items.Add("PM");

            ddlEndTime.Items.Add("AM");
            ddlEndTime.Items.Add("PM");
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
        Billing_Sys_ManageNotesBO manageNotes;
        manageNotes = new Billing_Sys_ManageNotesBO();
        string _isExit = "";
        try
        {

           _isExit= check_appointment();
           if (_isExit !="")
           {
               Page.RegisterStartupScript("ss", "<script language='javascript'>alert('This patient aleardy schdule for same date'); </script>");
               return;
           }
            #region "Check for patient exists with entered details"
            ArrayList _arrayList = new ArrayList();

            _arrayList.Add(txtPatientFName.Text);
            _arrayList.Add(txtPatientLName.Text);
             _arrayList.Add(null); //  Birth Date
            _arrayList.Add(extddlCaseType.Text);
            _arrayList.Add(null); // Accident Date
            _arrayList.Add(txtCompanyID.Text);
            _arrayList.Add("existpatient");
            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            String iexists = _bill_Sys_PatientBO.CheckPatientExists(_arrayList);
            #endregion
            if (iexists != "" && txtPatientFName.ReadOnly == false)
            {
                msgPatientExists.InnerHtml = iexists;
                Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");
            }
            else
            {
                if (txtPatientFName.ReadOnly == false)
                {
                    SavePatient();
                    txtPatientID.Text = manageNotes.GetPatientLatestID();
                }
                if (Request.QueryString["_date"].ToString() != "")
                {

                    string[] szArr;
                    szArr = Request.QueryString["_date"].ToString().Split(' ');
                    DateTime dtPassDate = new DateTime();
                    dtPassDate = Convert.ToDateTime(szArr[0]);


                    // Check Previous Date


                    //if (dtPassDate >= DateTime.Today)
                    //{
                    // CODE FOR ADD PATIENT ENTRY ---------------------------------------------------
                    _saveOperation = new SaveOperation();

                    if (txtPatientID.Text != "")
                    {
                        //_editOperation.WebPage = this.Page;
                        //_editOperation.Xml_File = "Cal_Patient.xml";
                        //_editOperation.Primary_Value = txtPatientID.Text;
                        //_editOperation.UpdateMethod();    

                        // txtPatientID.Text = manageNotes.GetPatientLatestID();

                        //_editOperation.WebPage = this.Page;
                        //_editOperation.Xml_File = "Cal_PatientCaseMaster.xml";
                        //_editOperation.Primary_Value = txtCaseID.Text;
                        //_editOperation.UpdateMethod();            
                    }
                    else
                    {
                        Bill_Sys_ReferalEvent _ReferalEvent = new Bill_Sys_ReferalEvent();
                        ArrayList objArra = new ArrayList();
                        objArra.Add(txtPatientFName.Text);
                        objArra.Add(txtMI.Text);
                        objArra.Add(txtPatientLName.Text);
                        objArra.Add(txtPatientAge.Text);
                        objArra.Add(txtPatientAddress.Text);
                        objArra.Add(txtPatientPhone.Text);
                        objArra.Add(txtBirthdate.Text);
                        objArra.Add(txtState.Text);
                        // if (chkTransportation.Checked == true) { objArra.Add(1); } else { objArra.Add(0); }
                        objArra.Add(txtSocialSecurityNumber.Text);
                        objArra.Add(txtCompanyID.Text);
                        _ReferalEvent.AddPatient(objArra);
                        //_saveOperation.WebPage = this.Page;
                        //_saveOperation.Xml_File = "Cal_Patient.xml";
                        //_saveOperation.SaveMethod();

                        txtPatientID.Text = manageNotes.GetPatientLatestID();

                        _ReferalEvent = new Bill_Sys_ReferalEvent();
                        objArra = new ArrayList();
                        objArra.Add(extddlInsuranceCompany.Text);
                        objArra.Add(txtPatientID.Text);
                        objArra.Add(txtCompanyID.Text);
                        objArra.Add(extddlCaseStatus.Text);
                        objArra.Add(extddlCaseType.Text);
                        _ReferalEvent.AddPatientCase(objArra);

                        //_saveOperation.WebPage = this.Page;
                        //_saveOperation.Xml_File = "Cal_PatientCaseMaster.xml";
                        //_saveOperation.SaveMethod();
                    }
                    // END OF ADD PATIENT ENTRY CODE ------------------------------------------------


                    //string[] _char = Request.QueryString["_date"].ToString().Split(new char[] { ',' });
                    // for (int i = 0; i <= _char.Length - 1; i++)
                    // {
                    int eventID = 0;
                    if (Request.QueryString["_date"].ToString() != "")
                    {
                        ////////////////
                        string szDoctorID = "";
                        String sz_datetime = Request.QueryString["_date"].ToString();

                        if (sz_datetime.IndexOf("~") > 0)
                        {
                            string scheduledID = "";
                            if (sz_datetime.IndexOf("^") > 0)
                            {
                                scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, ((sz_datetime.IndexOf("^")) - (sz_datetime.IndexOf("~") + 1)));
                            }
                            else
                            {
                                scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, (sz_datetime.Length - (sz_datetime.IndexOf("~") + 1)));
                            }
                            Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
                            ArrayList arrOBJ = new ArrayList();

                            arrOBJ.Add(extddlDoctor.Text);
                            arrOBJ.Add(txtPatientID.Text);
                            arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            szDoctorID = _bill_Sys_ReferalEvent.AddDoctor(arrOBJ);

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
                                    arrOBJ.Add(txtPatientID.Text);
                                    arrOBJ.Add(szDoctorID);
                                    arrOBJ.Add(Request.QueryString["_date"].ToString().Substring(0, Request.QueryString["_date"].ToString().IndexOf(" ")));
                                    arrOBJ.Add(lst.Value);
                                    arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    arrOBJ.Add(ddlType.SelectedValue);
                                    _bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);
                                }
                            }
                        }
                        else if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        {

                            Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
                            ArrayList arrOBJ = new ArrayList();

                            ////arrOBJ.Add(extddlDoctor.Text);
                            ////arrOBJ.Add(txtPatientID.Text);
                            ////arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            ////szDoctorID = _bill_Sys_ReferalEvent.AddDoctor(arrOBJ);
                            szDoctorID = extddlDoctor.Text;
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

                                    //arrOBJ = new ArrayList();
                                    //arrOBJ.Add(txtPatientID.Text);
                                    //arrOBJ.Add(szDoctorID);
                                    //arrOBJ.Add(Request.QueryString["_date"].ToString().Substring(0, Request.QueryString["_date"].ToString().IndexOf(" ")));
                                    //arrOBJ.Add(lst.Value);
                                    //arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    //arrOBJ.Add(ddlType.SelectedValue);
                                    //_bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);
                                }
                            }
                        }
                        else
                        {
                            szDoctorID = extddlDoctor.Text;
                        }
                        ///////////////atul
                        if (chkTransportation.Checked == true && extddlTransport.Text == "NA")
                        {
                            Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select transport company !');</script>");
                            return;
                        }
                        string sz_date = Request.QueryString["_date"].ToString();
                        sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));
                        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                        ArrayList objAdd = new ArrayList();
                        objAdd.Add(txtPatientID.Text);
                        objAdd.Add(sz_date);
                        objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());
                        objAdd.Add(txtNotes.Text);
                        objAdd.Add(szDoctorID);
                        if (ddlTestNames.Items.Count > 1) { objAdd.Add(ddlTestNames.Items[1].Value); } else objAdd.Add("");

                        objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAdd.Add(ddlTime.SelectedValue);
                        objAdd.Add(ddlEndHours.SelectedValue.ToString() + "." + ddlEndMinutes.SelectedValue.ToString());
                        objAdd.Add(ddlEndTime.SelectedValue);
                        objAdd.Add(extddlReferenceFacility.Text);

                        //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        //{ objAdd.Add("True"); }
                        //else { objAdd.Add("False"); }
                        objAdd.Add("False");
                        // #149 12 April,2010 change the BT_TRANSPOTATION field patient table to txn_calender_event -- sachin
                        if (chkTransportation.Checked == true) { objAdd.Add(1); } else { objAdd.Add(0); }
                        //==================================
                        if (chkTransportation.Checked == true) { objAdd.Add(Convert.ToInt32(extddlTransport.Text)); } else { objAdd.Add(null); }

                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        { objAdd.Add(extddlMedicalOffice.Text); }


                        eventID = _bill_Sys_Calender.Save_Event(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());

                        foreach (ListItem lst in ddlTestNames.Items)
                        {
                            if (lst.Selected == true)
                            {
                                objAdd = new ArrayList();
                                objAdd.Add(lst.Value);
                                objAdd.Add(eventID);
                                //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                                //{ objAdd.Add(2); }
                                //else { objAdd.Add(0); }
                                objAdd.Add(0);
                                _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                            }
                        }

                        // Start : Save appointment Notes.

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + sz_datetime.Substring(0, sz_datetime.IndexOf("!")); //"Date : " + sz_datetime;


                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


                        // Document Manager Node : Start
                        if (flUpload.FileName != "")
                        {
                            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();

                            String szDefaultPath = objNF3Template.getPhysicalPath();
                            String szDestinationDir = "";

                            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                            {

                                szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                            }
                            else
                            {
                                szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);

                            }


                            szDestinationDir = szDestinationDir + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/No Fault File/Referral Sheet-INI Report/";

                            if (!Directory.Exists(szDefaultPath + szDestinationDir))
                            {
                                Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                            }

                            if (!File.Exists(szDefaultPath + szDestinationDir + flUpload.FileName))
                            {
                                flUpload.SaveAs(szDefaultPath + szDestinationDir + flUpload.FileName);

                                ArrayList objAL = new ArrayList();
                                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                                {
                                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                }
                                else
                                {
                                    objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                                }

                                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
                                objAL.Add(flUpload.FileName);
                                objAL.Add(szDestinationDir);
                                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                                objAL.Add("Referral Sheet-INI Report");
                                objNF3Template.saveOutScheduleReportInDocumentManager(objAL);

                            }
                        }
                        // Document Manager Node : End


                        // End 

                    }
                    //}
                    if (Request.QueryString["_date"].ToString().IndexOf("~") > 0)
                    {
                        Session["PopUp"] = null;
                    }
                    else
                    {
                        Session["PopUp"] = "True";
                    }
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href=window.parent.document.location.href;window.self.close();</script>");




                    // }
                    //else
                    //{
                    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('You can not schedule event for previous date ...!');</script>");
                    //}
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_AppointPatientEntry.aspx';</script>");

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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            // 16 April 2010 -- sachin
            if (chkTransportation.Checked == true && extddlTransport.Text == "NA")
            {
                Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select transport company !');</script>");
                return;
            }
           

            //string[] _char = Request.QueryString["_date"].ToString().Split(new char[] { ',' });
            // for (int i = 0; i <= _char.Length - 1; i++)
            // {
            int eventID = Convert.ToInt32(Session["SCHEDULEDID"].ToString());
           
               

                string sz_date = Request.QueryString["_date"].ToString();
                sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));
                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                ArrayList objAdd = new ArrayList();
                objAdd.Add(txtPatientID.Text);
                objAdd.Add(sz_date);
                objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());
                objAdd.Add(txtNotes.Text);
                objAdd.Add(extddlDoctor.Text);
                if (ddlTestNames.Items.Count > 1) { objAdd.Add(ddlTestNames.Items[1].Value); } else objAdd.Add(""); 
                //objAdd.Add(ddlTestNames.SelectedValue);
                objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                objAdd.Add(ddlTime.SelectedValue);
                objAdd.Add(ddlEndHours.SelectedValue.ToString() + "." + ddlEndMinutes.SelectedValue.ToString());
                objAdd.Add(ddlEndTime.SelectedValue);
                objAdd.Add(extddlReferenceFacility.Text);
                objAdd.Add(eventID);
                  // #149 12 April,2010 change the BT_TRANSPOTATION field patient table to txn_calender_event -- sachin
                if (chkTransportation.Checked == true) { objAdd.Add(1); } else { objAdd.Add(0); }
                //================================================
            //15 April 2010 add transport company field --- sachin
                if (chkTransportation.Checked == true && extddlTransport.Text != "NA") { objAdd.Add(Convert.ToInt32( extddlTransport.Text )); } else { objAdd.Add(null); }
                //================================================

                DateTime objTemp = new DateTime();
                objTemp = Convert.ToDateTime(sz_date);
            DateTime objDate =new DateTime();

                if (ddlTime.SelectedValue == "AM")
                {
                    objDate = new DateTime(objTemp.Year, objTemp.Month, objTemp.Day, Convert.ToInt32(ddlHours.SelectedValue), Convert.ToInt32(ddlEndMinutes.SelectedValue), 0);
                }
                else
                {
                    int _finalHours = 0;
                    if (Convert.ToInt32(ddlHours.SelectedValue) == 12)
                    {
                        _finalHours= Convert.ToInt32(ddlHours.SelectedValue);
                    }
                    else
                    {
                        _finalHours = Convert.ToInt32(ddlHours.SelectedValue) + 12;
                    }
                    objDate = new DateTime(objTemp.Year, objTemp.Month, objTemp.Day, _finalHours, Convert.ToInt32(ddlEndMinutes.SelectedValue), 0);
                }

                // if (DateTime.Compare(objDate, DateTime.Now) < 0)
                //if (0)
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('Cannot modify this appointment ...!');</script>");
                //}
                //else
                //{
                   // eventID = _bill_Sys_Calender.UPDATE_Event(objAdd); 
                    eventID = _bill_Sys_Calender.UPDATE_Event_Referral(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
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


                    // Start : Save appointment Notes.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_UPDATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + Request.QueryString["_date"].ToString().Substring(0, Request.QueryString["_date"].ToString().IndexOf("!"));

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


                    // End 


                    //}

                    Session["PopUp"] = "True";

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href=window.parent.document.location.href;window.self.close();</script>");
                //}
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_AppointPatientEntry.aspx';</script>");


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

    protected void btnSearhPatientList_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SearchPatientList();            
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

    protected void grdPatientList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        setReadOnly(true);
        try
        {
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[1].Text != "&nbsp;") { txtPatientID.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[1].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[2].Text != "&nbsp;") { txtPatientFName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[2].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[11].Text != "&nbsp;") { txtMI.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[11].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[3].Text != "&nbsp;") { txtPatientLName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[3].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[9].Text != "&nbsp;") { txtPatientPhone.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[9].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[5].Text != "&nbsp;") { txtPatientAddress.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[5].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text != "&nbsp;") { extddlPatientState.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[15].Text != "&nbsp;") { txtBirthdate.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[15].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[4].Text != "&nbsp;") { txtPatientAge.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[4].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[13].Text != "&nbsp;") { txtSocialSecurityNumber.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[13].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[31].Text != "&nbsp;") { if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[31].Text.ToString() == "True") { chkTransportation.Checked = true; } else { chkTransportation.Checked = false; } }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[32].Text != "&nbsp;") { txtRefChartNumber.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[32].Text; } else { txtRefChartNumber.Text = ""; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[7].Text != "&nbsp;") { txtPatientCity.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[7].Text; } else { txtPatientCity.Text = ""; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text != "&nbsp;") { extddlPatientState.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text; } else { extddlPatientState.Text = ""; }
            Billing_Sys_ManageNotesBO manageNotes = new Billing_Sys_ManageNotesBO();
            DataSet _ds = manageNotes.GetCaseDetails(txtPatientID.Text,((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                txtCaseID.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != _ds.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                {
                    extddlInsuranceCompany.Flag_ID = _ds.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                    extddlCaseType.Flag_ID = _ds.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                }
                else
                {
                    extddlInsuranceCompany.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString() != "")
                {
                    extddlMedicalOffice.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                    extddlMedicalOffice.Enabled = false;
                   
                    extddlDoctor.Flag_ID = _ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                    extddlDoctor.Flag_Key_Value = "GETREFFERDOCTORLIST";
                }
                else
                {
                    extddlMedicalOffice.Enabled = true;
                }
                extddlCaseType.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                extddlInsuranceCompany.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                if (txtPatientID.Text.ToString() != "")
                    {
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        _bill_Sys_CaseObject.SZ_PATIENT_ID = txtPatientID.Text;
                        _bill_Sys_CaseObject.SZ_CASE_ID = txtCaseID.Text;
                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != _ds.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                        {
                            _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, _ds.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                        }
                        else
                        {
                            _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            
                        }
                        _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                    }
                
            }
            btnSave.Attributes.Add("onclick", "return ValidateAllField('');");

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

    private void SearchPatientList()
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
            ds = (DataSet)Session["PatientDataList"];
            dt = ds.Tables[0].Clone();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Session["Flag"] != null || Session["DataEntryFlag"] != null)
                {

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
                    if (ds.Tables[0].Rows[0]["CHART NUMBER"].ToString() != "&nbsp;") { txtRefChartNumber.Text = ds.Tables[0].Rows[0]["CHART NUMBER"].ToString(); } else { txtRefChartNumber.Text = ""; }
                    
                    Billing_Sys_ManageNotesBO manageNotes = new Billing_Sys_ManageNotesBO();
                    DataSet _ds = manageNotes.GetCaseDetails(txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        txtCaseID.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != _ds.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                        {
                            extddlInsuranceCompany.Flag_ID = _ds.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                            extddlCaseType.Flag_ID = _ds.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                        }
                        if (_ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString() != "")
                        {
                            extddlMedicalOffice.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                            extddlMedicalOffice.Enabled = false;

                            extddlDoctor.Flag_ID = _ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                            extddlDoctor.Flag_Key_Value = "GETREFFERDOCTORLIST";
                        }
                        else
                        {
                            extddlMedicalOffice.Enabled = true;
                        }
                        extddlCaseType.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        extddlInsuranceCompany.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    }
                }
                else
                {
                    // dt = new DataTable();

                    if (txtPatientFirstName.Text != "" && txtPatientLastName.Text == "")
                    {
                        DataRow[] dr = ds.Tables[0].Select("SZ_PATIENT_FIRST_NAME LIKE '" + txtPatientFirstName.Text + "%'");
                        for (int i = 0; i < dr.Length; i++)
                        {
                            dt.ImportRow(dr[i]);
                        }

                    }
                    else if (txtPatientLastName.Text != "" && txtPatientFirstName.Text == "")
                    {
                        DataRow[] dr = ds.Tables[0].Select("SZ_PATIENT_LAST_NAME LIKE '" + txtPatientLastName.Text + "%'");
                        for (int i = 0; i < dr.Length; i++)
                        {
                            dt.ImportRow(dr[i]);
                        }

                    }
                    else if (txtPatientLastName.Text != "" && txtPatientFirstName.Text != "")
                    {
                        DataRow[] dr = ds.Tables[0].Select("SZ_PATIENT_FIRST_NAME LIKE '" + txtPatientFirstName.Text + "%' AND SZ_PATIENT_LAST_NAME LIKE '" + txtPatientLastName.Text + "%'");
                        for (int i = 0; i < dr.Length; i++)
                        {
                            dt.ImportRow(dr[i]);
                        }
                    }
                    grdPatientList.DataSource = dt;
                    grdPatientList.DataBind();
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
            ds=_patient_TVBO.GetAppointPatientDetails(i_schedule_id);

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

                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(33).ToString() != "&nbsp;") {extddlCaseType.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(33).ToString();}
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(32).ToString() != "&nbsp;") { extddlInsuranceCompany.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(32).ToString(); }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(35).ToString() != "&nbsp;") { extddlDoctor.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(35).ToString(); }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(36).ToString() != "&nbsp;") { ddlType.SelectedValue= ds.Tables[0].Rows[0].ItemArray.GetValue(36).ToString(); }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(37).ToString() != "&nbsp;") 
                    { string startTime = ds.Tables[0].Rows[0].ItemArray.GetValue(37).ToString();
                       ddlHours.SelectedValue=startTime.Substring(0,startTime.IndexOf(".")).PadLeft(2,'0');
                       ddlMinutes.SelectedValue = startTime.Substring(startTime.IndexOf(".") + 1, startTime.Length - (startTime.IndexOf(".") + 1)).PadLeft(2, '0');
                    }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(38).ToString() != "&nbsp;") 
                    {
                        string endTime = ds.Tables[0].Rows[0].ItemArray.GetValue(38).ToString();
                        ddlEndHours.SelectedValue = endTime.Substring(0, endTime.IndexOf(".")).PadLeft(2, '0');
                        ddlEndMinutes.SelectedValue = endTime.Substring(endTime.IndexOf(".") + 1, (endTime.Length - (endTime.IndexOf(".") + 1))).PadLeft(2, '0');
                    }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(39).ToString() != "&nbsp;") { ddlTime.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(39).ToString(); }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(40).ToString() != "&nbsp;") { ddlEndTime.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(40).ToString(); }
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(41).ToString() != "&nbsp;") { extddlDoctor.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(41).ToString(); }
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(51).ToString() != "&nbsp;") { extddlDoctor.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(51).ToString(); }
                    }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(42).ToString() != "&nbsp;") { txtNotes.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(42).ToString(); }
                   
                    btnSave.Visible = false;
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(43).ToString() != "&nbsp;")
                    {
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(43).ToString() == "True")
                        {
                            btnUpdate.Visible = false;
                            extddlDoctor.Enabled = false;
                            ddlType.Enabled = false;
                            //ddlTestNames.Enabled = false;
                            txtNotes.ReadOnly = true;
                        }
                        else
                        {
                            btnUpdate.Visible = true;
                        }
                    }
                    if (ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString() != "&nbsp;")
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString()) > 0 && chkTransportation.Checked)
                        {
                            extddlTransport.Flag_ID = extddlReferenceFacility.Text;
                            extddlTransport.Text = ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString();
                            extddlTransport.Visible = true;
                        }
                        else if (Convert.ToInt32(ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString()) == 0 && chkTransportation.Checked)
                        {
                            extddlTransport.Text = "NA";
                            extddlTransport.Visible = true;
                        }
                        else
                        {
                            extddlTransport.Visible = false;
                        }
                    }
                    else
                    {
                        extddlTransport.Text = "NA";
                    }
                    //extddlDoctor.Visible = false;
                   
                    //txtDoctorName.Visible = false;
             ds = new DataSet();
            _patient_TVBO = new Patient_TVBO();
            ds = _patient_TVBO.GetAppointProcCode(i_schedule_id);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (ListItem lst in ddlTestNames.Items)
                {
                    if (lst.Value ==   dr.ItemArray.GetValue(0).ToString())
                    {
                        lst.Selected = true;
                    }
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

    protected void grdPatientList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdPatientList.CurrentPageIndex = e.NewPageIndex;
            SearchPatientList();
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

    protected void extddlMedicalOffice_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            extddlDoctor.Flag_ID = extddlMedicalOffice.Text;
            extddlDoctor.Flag_Key_Value = "GETREFFERDOCTORLIST";
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

    #region "Add Patient for referring facility account only"

    protected void btnAddPatient_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ClearControlForAddPatient();
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
            {
                lblChartNumber.Visible = true;
                txtRefChartNumber.Visible = true;
                string ID = "";
                ID = _bill_Sys_PatientBO.GetMaxChartNumber(txtCompanyID.Text);
                if (ID != "")
                {
                    txtRefChartNumber.Text = ID;
                }
                else
                {
                    txtRefChartNumber.Text = "1";
                }
            }
            else
            {
                lblChartNumber.Visible = false;
                txtRefChartNumber.Visible = false;
            }
            setReadOnly(false);
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

    public void setReadOnly(bool value)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtRefChartNumber.ReadOnly = value;
            txtPatientFName.ReadOnly = value;
            txtMI.ReadOnly = value;
            txtPatientLName.ReadOnly = value;
            txtPatientPhone.ReadOnly = value;
            txtPatientAddress.ReadOnly = value;
            txtPatientCity.ReadOnly = value;
            txtState.ReadOnly = value;
            txtBirthdate.ReadOnly = value;
            txtPatientAge.ReadOnly = value;
            txtSocialSecurityNumber.ReadOnly = value;
            extddlInsuranceCompany.Enabled = !value;
            extddlCaseType.Enabled = !value;
            extddlMedicalOffice.Enabled = !value;
            extddlPatientState.Enabled = !value;
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

    public void ClearControlForAddPatient()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtRefChartNumber.Text = "";
            txtPatientFName.Text = "";
            txtMI.Text = "";
            txtPatientLName.Text = "";
            txtPatientPhone.Text = "";
            txtPatientAddress.Text = "";
            txtPatientCity.Text = "";
            txtState.Text = "";
            txtBirthdate.Text = "";
            txtPatientAge.Text = "";
            txtSocialSecurityNumber.Text = "";
            extddlInsuranceCompany.Text = "NA";
            extddlCaseType.Text = "NA";
            extddlMedicalOffice.Text = "NA";
            extddlPatientState.Text = "NA";
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

    public void SavePatient()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender objCal = new Bill_Sys_Calender();
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {

            extddlCaseStatus.Text = objCal.GetOpenCaseStatus(txtCompanyID.Text); 
            ArrayList objAL = new ArrayList();
            objAL.Add(txtPatientFName.Text);
            objAL.Add(txtPatientLName.Text);
            objAL.Add(extddlCaseType.Text);
            objAL.Add(txtPatientAge.Text); // Patient Age
            objAL.Add(txtPatientAddress.Text); // Patient Address
            objAL.Add(txtPatientCity.Text);   // Patient City
            objAL.Add(txtPatientPhone.Text);   // Patient Phone
            objAL.Add(extddlPatientState.Text);   // Patient State
            objAL.Add(txtCompanyID.Text);   // Patient State
            objAL.Add(txtMI.Text);   // Patient Middle Name
            objAL.Add(extddlCaseStatus.Text);   // Case Status id
            objAL.Add(extddlInsuranceCompany.Text);   // Insurance company id
            objAL.Add(txtRefChartNumber.Text);   // Insurance company id

            objCal.savePatientForReferringFacility(objAL);
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

   
    protected void chkTransportation_CheckedChanged1(object sender, EventArgs e)
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
                extddlTransport.Flag_ID = extddlReferenceFacility.Text;
            }
            else if (chkTransportation.Checked == false)
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    #region "Save entry for existing patient."


    protected void lfnSaveAppointment()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Billing_Sys_ManageNotesBO manageNotes;
        manageNotes = new Billing_Sys_ManageNotesBO();
        try
        {
            if (Request.QueryString["_date"].ToString() != "")
            {

                string[] szArr;
                szArr = Request.QueryString["_date"].ToString().Split(' ');
                DateTime dtPassDate = new DateTime();
                dtPassDate = Convert.ToDateTime(szArr[0]);


                // Check Previous Date


                //if (dtPassDate >= DateTime.Today)
                //{
                // CODE FOR ADD PATIENT ENTRY ---------------------------------------------------
                _saveOperation = new SaveOperation();

                if (txtPatientID.Text != "")
                {
                    //_editOperation.WebPage = this.Page;
                    //_editOperation.Xml_File = "Cal_Patient.xml";
                    //_editOperation.Primary_Value = txtPatientID.Text;
                    //_editOperation.UpdateMethod();    

                    // txtPatientID.Text = manageNotes.GetPatientLatestID();

                    //_editOperation.WebPage = this.Page;
                    //_editOperation.Xml_File = "Cal_PatientCaseMaster.xml";
                    //_editOperation.Primary_Value = txtCaseID.Text;
                    //_editOperation.UpdateMethod();            
                }
                else
                {
                    Bill_Sys_ReferalEvent _ReferalEvent = new Bill_Sys_ReferalEvent();
                    ArrayList objArra = new ArrayList();
                    objArra.Add(txtPatientFName.Text);
                    objArra.Add(txtMI.Text);
                    objArra.Add(txtPatientLName.Text);
                    objArra.Add(txtPatientAge.Text);
                    objArra.Add(txtPatientAddress.Text);
                    objArra.Add(txtPatientPhone.Text);
                    objArra.Add(txtBirthdate.Text);
                    objArra.Add(txtState.Text);
                    // if (chkTransportation.Checked == true) { objArra.Add(1); } else { objArra.Add(0); }
                    objArra.Add(txtSocialSecurityNumber.Text);
                    objArra.Add(txtCompanyID.Text);
                    _ReferalEvent.AddPatient(objArra);
                    //_saveOperation.WebPage = this.Page;
                    //_saveOperation.Xml_File = "Cal_Patient.xml";
                    //_saveOperation.SaveMethod();

                    txtPatientID.Text = manageNotes.GetPatientLatestID();

                    _ReferalEvent = new Bill_Sys_ReferalEvent();
                    objArra = new ArrayList();
                    objArra.Add(extddlInsuranceCompany.Text);
                    objArra.Add(txtPatientID.Text);
                    objArra.Add(txtCompanyID.Text);
                    objArra.Add(extddlCaseStatus.Text);
                    objArra.Add(extddlCaseType.Text);
                    _ReferalEvent.AddPatientCase(objArra);

                    //_saveOperation.WebPage = this.Page;
                    //_saveOperation.Xml_File = "Cal_PatientCaseMaster.xml";
                    //_saveOperation.SaveMethod();
                }
                // END OF ADD PATIENT ENTRY CODE ------------------------------------------------


                //string[] _char = Request.QueryString["_date"].ToString().Split(new char[] { ',' });
                // for (int i = 0; i <= _char.Length - 1; i++)
                // {
                int eventID = 0;
                if (Request.QueryString["_date"].ToString() != "")
                {
                    ////////////////
                    string szDoctorID = "";
                    String sz_datetime = Request.QueryString["_date"].ToString();

                    if (sz_datetime.IndexOf("~") > 0)
                    {
                        string scheduledID = "";
                        if (sz_datetime.IndexOf("^") > 0)
                        {
                            scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, ((sz_datetime.IndexOf("^")) - (sz_datetime.IndexOf("~") + 1)));
                        }
                        else
                        {
                            scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, (sz_datetime.Length - (sz_datetime.IndexOf("~") + 1)));
                        }
                        Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
                        ArrayList arrOBJ = new ArrayList();

                        arrOBJ.Add(extddlDoctor.Text);
                        arrOBJ.Add(txtPatientID.Text);
                        arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        szDoctorID = _bill_Sys_ReferalEvent.AddDoctor(arrOBJ);

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
                                arrOBJ.Add(txtPatientID.Text);
                                arrOBJ.Add(szDoctorID);
                                arrOBJ.Add(Request.QueryString["_date"].ToString().Substring(0, Request.QueryString["_date"].ToString().IndexOf(" ")));
                                arrOBJ.Add(lst.Value);
                                arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                arrOBJ.Add(ddlType.SelectedValue);
                                _bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);
                            }
                        }
                    }
                    else if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {

                        Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
                        ArrayList arrOBJ = new ArrayList();

                        ////arrOBJ.Add(extddlDoctor.Text);
                        ////arrOBJ.Add(txtPatientID.Text);
                        ////arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        ////szDoctorID = _bill_Sys_ReferalEvent.AddDoctor(arrOBJ);
                        szDoctorID = extddlDoctor.Text;
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

                                //arrOBJ = new ArrayList();
                                //arrOBJ.Add(txtPatientID.Text);
                                //arrOBJ.Add(szDoctorID);
                                //arrOBJ.Add(Request.QueryString["_date"].ToString().Substring(0, Request.QueryString["_date"].ToString().IndexOf(" ")));
                                //arrOBJ.Add(lst.Value);
                                //arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                //arrOBJ.Add(ddlType.SelectedValue);
                                //_bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);
                            }
                        }
                    }
                    else
                    {
                        szDoctorID = extddlDoctor.Text;
                    }
                    ///////////////atul
                    if (chkTransportation.Checked == true && extddlTransport.Text == "NA")
                    {
                        Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select transport company !');</script>");
                        return;
                    }
                    string sz_date = Request.QueryString["_date"].ToString();
                    sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));
                    Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                    ArrayList objAdd = new ArrayList();
                    objAdd.Add(txtPatientID.Text);
                    objAdd.Add(sz_date);
                    objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());
                    objAdd.Add(txtNotes.Text);
                    objAdd.Add(szDoctorID);
                    if (ddlTestNames.Items.Count > 1) { objAdd.Add(ddlTestNames.Items[1].Value); } else objAdd.Add("");

                    objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    objAdd.Add(ddlTime.SelectedValue);
                    objAdd.Add(ddlEndHours.SelectedValue.ToString() + "." + ddlEndMinutes.SelectedValue.ToString());
                    objAdd.Add(ddlEndTime.SelectedValue);
                    objAdd.Add(extddlReferenceFacility.Text);

                    //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    //{ objAdd.Add("True"); }
                    //else { objAdd.Add("False"); }
                    objAdd.Add("False");
                    // #149 12 April,2010 change the BT_TRANSPOTATION field patient table to txn_calender_event -- sachin
                    if (chkTransportation.Checked == true) { objAdd.Add(1); } else { objAdd.Add(0); }
                    //==================================
                    if (chkTransportation.Checked == true) { objAdd.Add(Convert.ToInt32(extddlTransport.Text)); } else { objAdd.Add(null); }

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    { objAdd.Add(extddlMedicalOffice.Text); }


                    eventID = _bill_Sys_Calender.Save_Event(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());

                    foreach (ListItem lst in ddlTestNames.Items)
                    {
                        if (lst.Selected == true)
                        {
                            objAdd = new ArrayList();
                            objAdd.Add(lst.Value);
                            objAdd.Add(eventID);
                            //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                            //{ objAdd.Add(2); }
                            //else { objAdd.Add(0); }
                            objAdd.Add(0);
                            _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                        }
                    }

                    // Start : Save appointment Notes.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + sz_datetime.Substring(0, sz_datetime.IndexOf("!")); //"Date : " + sz_datetime;


                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


                    // Document Manager Node : Start
                    if (flUpload.FileName != "")
                    {
                        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();

                        String szDefaultPath = objNF3Template.getPhysicalPath();
                        String szDestinationDir = "";

                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                        {

                            szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                        }
                        else
                        {
                            szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);

                        }


                        szDestinationDir = szDestinationDir + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/No Fault File/Referral Sheet-INI Report/";

                        if (!Directory.Exists(szDefaultPath + szDestinationDir))
                        {
                            Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                        }

                        if (!File.Exists(szDefaultPath + szDestinationDir + flUpload.FileName))
                        {
                            flUpload.SaveAs(szDefaultPath + szDestinationDir + flUpload.FileName);

                            ArrayList objAL = new ArrayList();
                            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                            {
                                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            }
                            else
                            {
                                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                            }

                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
                            objAL.Add(flUpload.FileName);
                            objAL.Add(szDestinationDir);
                            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add("Referral Sheet-INI Report");
                            objNF3Template.saveOutScheduleReportInDocumentManager(objAL);

                        }
                    }
                    // Document Manager Node : End


                    // End 

                }
                //}
                if (Request.QueryString["_date"].ToString().IndexOf("~") > 0)
                {
                    Session["PopUp"] = null;
                }
                else
                {
                    Session["PopUp"] = "True";
                }
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href=window.parent.document.location.href;window.self.close();</script>");




                // }
                //else
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('You can not schedule event for previous date ...!');</script>");
                //}
            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_AppointPatientEntry.aspx';</script>");


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

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SavePatient();
            Billing_Sys_ManageNotesBO manageNotes;
            manageNotes = new Billing_Sys_ManageNotesBO();
            txtPatientID.Text = manageNotes.GetPatientLatestID();
            lfnSaveAppointment();
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
    private string  check_appointment()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        string _isExit = "";
        try
        {
            string sz_datetime = Request.QueryString["_date"].ToString();
            string sz_date = Request.QueryString["_date"].ToString();
            sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));
          
            ArrayList objarr = new ArrayList();
            objarr.Add(txtCaseID.Text);
            objarr.Add(txtCompanyID.Text);
            objarr.Add(sz_datetime.Substring(sz_datetime.IndexOf("!") + 1, 20));
            objarr.Add(sz_date);
            _isExit = _bill_Sys_PatientBO.Check_Appointment(objarr);


            
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
        
        return _isExit;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
