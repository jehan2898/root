/***********************************************************/
/*Project Name         :        Medical Billing System
/*Description          :        update Notes filed 
/*Author               :        Sandeep Y
/*Date of creation     :        15 Dec 2008
/*Modified By (Last)   :        Prashant zope
/*Modified By (S-Last) :        Sandeep y
/*Modified Date        :        28 april 2010
 ************************************************************/

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
using log4net;

public partial class Bill_Sys_AddReferredAppointment : PageBase
{
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private DataSet ds;
    private Patient_TVBO _patient_TVBO;
    Bill_Sys_Case _bill_Sys_Case;
    Boolean deleteflag = false;
    private static ILog log = LogManager.GetLogger("Bill_Sys_AddReferredAppointment");
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        btnSave.Attributes.Add("onclick", "return formValidator('form1','extddlDoctor,ddlType,ddlTestNames,ddlHours,ddlEndHours');");
        btnDelete.Attributes.Add("onclick", "return ConfirmDelete()");

        if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
        {
            lblChartNumber.Visible = true;
            txtChartNo.Visible = true;
        }
        else
        {
            lblChartNumber.Visible = false;
            txtChartNo.Visible = false;
        }

        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        if (!IsPostBack)
        {
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlMedicalOffice.Flag_ID = txtCompanyID.Text.ToString();
            extddlReferenceFacility.Flag_ID = txtCompanyID.Text.ToString();
            extddlInsuranceCompany.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            extddlTransport.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            BindTimeControl();
            string sz_datetime = Request.QueryString["_date"].ToString();
            if (sz_datetime.Substring(sz_datetime.IndexOf(" ") + 2, 1) == ".")
            {
                ddlHours.SelectedValue = sz_datetime.Substring(sz_datetime.IndexOf(" "), 2).Replace(" ", "0");
            }
            else
            {
                ddlHours.SelectedValue = sz_datetime.Substring(sz_datetime.IndexOf(" ") + 1, 2);
            }
            ddlMinutes.SelectedValue = sz_datetime.Substring(sz_datetime.IndexOf(".") + 1, 2);
            ddlTime.SelectedValue = sz_datetime.Substring(sz_datetime.IndexOf("|") + 1, 2);
            extddlReferenceFacility.Text = Session["TestFacilityID"].ToString();
            LoadTypes();

            int endMin = Convert.ToInt32(ddlMinutes.SelectedValue) + Convert.ToInt32(Session["INTERVAL"].ToString());
            int endHr = Convert.ToInt32(ddlHours.SelectedValue);
            string endTime = ddlTime.SelectedValue;
            if (endMin >= 60)
            {
                endMin = endMin - 60;
                endHr = endHr + 1;
                if (endHr > 12)
                {
                    endHr = endHr - 12;
                    if (endTime == "AM")
                    {
                        endTime = "PM";
                    }
                    else if (endTime == "PM")
                    {
                        endTime = "AM";
                    }
                }
                else if (endHr == 12)
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

            ddlEndHours.SelectedValue = endHr.ToString().PadLeft(2, '0');
            ddlEndMinutes.SelectedValue = endMin.ToString().PadLeft(2, '0');
            ddlEndTime.SelectedValue = endTime.ToString();


            if (Session["Flag"] != null)
            {
                _patient_TVBO = new Patient_TVBO();
                Session["PatientDataList"] = _patient_TVBO.GetSelectedPatientDataList(txtCompanyID.Text, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                SearchPatientList();
                btnClickSearch.Visible = false;
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
                extddlCaseType.Text = "";
                extddlInsuranceCompany.Text = "NA";

                //////////////////////////
            }
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                extddlMedicalOffice.Visible = true;
                extddlReferenceFacility.Visible = false;

                ddlType.Visible = false;
                lblTestFacility.Text = "Office Name";
            }
            extddlCaseStatus.Text = GetOpenCaseStatus();
            ////////////////
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
                Session["SCHEDULEDID"] = scheduledID;
                GETAppointPatientDetail(Convert.ToInt32(scheduledID));
                btnClickSearch.Visible = false;
                tdSerach.Visible = false;
                tdSerach.Height = "0px";

            }
            ///////////////

            Session["Office_Id"] = extddlMedicalOffice.Text;
        }

        // check delete button assign or not Prashant

        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "True")
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }
            
           
      

        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_AddReferredAppointment.aspx");
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
        if (txtCompanyID.Text !="CO000000000000000081")
        {
            ArrayList objArr = new ArrayList();

            objArr.Add(extddlReferenceFacility.Text);
            string sz_datetime = Request.QueryString["_date"].ToString();

            objArr.Add(sz_datetime.Substring(sz_datetime.IndexOf("!") + 1, 20));
            Bill_Sys_ManageVisitsTreatmentsTests_BO objBO = new Bill_Sys_ManageVisitsTreatmentsTests_BO();

            grdProcedureCode.DataSource = objBO.GetReferringProcCodeList(objArr);
            //ddlTestNames.DataTextField = "description";
            //ddlTestNames.DataValueField = "code";
            grdProcedureCode.DataBind();
        }
        else
        {
             ArrayList objArr = new ArrayList();
             string scheduledID = "";
             String sz_datetime = Request.QueryString["_date"].ToString();
             if (sz_datetime.IndexOf("^") > 0)
             {
                 scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, ((sz_datetime.IndexOf("^")) - (sz_datetime.IndexOf("~") + 1)));
             }
             else
             {
                 scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, (sz_datetime.Length - (sz_datetime.IndexOf("~") + 1)));
             }
             objArr.Add("LHR");
          

             objArr.Add(scheduledID);
             Bill_Sys_ManageVisitsTreatmentsTests_BO objBO = new Bill_Sys_ManageVisitsTreatmentsTests_BO();

             grdProcedureCode.DataSource = objBO.GetReferringProcCodeList(objArr);
             //ddlTestNames.DataTextField = "description";
             //ddlTestNames.DataValueField = "code";
             grdProcedureCode.DataBind();

          
        }
        //ddlTestNames.Items.Insert(0, "--- Select ---");
        //ddlTestNames.Visible = true;

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
        try
        {
            // CODE FOR ADD PATIENT ENTRY ---------------------------------------------------
            _saveOperation = new SaveOperation();
            Billing_Sys_ManageNotesBO manageNotes;
            manageNotes = new Billing_Sys_ManageNotesBO();
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

                    arrOBJ.Add(lblDoctor.Text);
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
                else
                {
                    szDoctorID = extddlDoctor.Text;
                }
                ///////////////

                string sz_date = Request.QueryString["_date"].ToString();
                sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));
                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                ArrayList objAdd = new ArrayList();
                objAdd.Add(txtPatientID.Text);
                objAdd.Add(sz_date);
                objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());
                objAdd.Add(txtNotes.Text);
                objAdd.Add(szDoctorID);
                //objAdd.Add(ddlTestNames.SelectedValue);
                if (ddlTestNames.Items.Count > 1) { objAdd.Add(ddlTestNames.Items[1].Value); } else objAdd.Add(""); 
                objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                objAdd.Add(ddlTime.SelectedValue);
                objAdd.Add(ddlEndHours.SelectedValue.ToString() + "." + ddlEndMinutes.SelectedValue.ToString());
                objAdd.Add(ddlEndTime.SelectedValue);
                objAdd.Add(extddlReferenceFacility.Text);

                eventID = _bill_Sys_Calender.Save_Event(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "EVENT_ADDED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Event Id : " + eventID + " added.";
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                foreach (ListItem lst in ddlTestNames.Items)
                {
                    if (lst.Selected == true)
                    {
                        objAdd = new ArrayList();
                        objAdd.Add(lst.Value);
                        objAdd.Add(eventID);
                        _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                    }
                }

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
        try
        {
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[1].Text != "&nbsp;") { txtPatientID.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[1].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[2].Text != "&nbsp;") { txtPatientFName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[2].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[11].Text != "&nbsp;") { txtMI.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[11].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[3].Text != "&nbsp;") { txtPatientLName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[3].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[9].Text != "&nbsp;") { txtPatientPhone.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[9].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[5].Text != "&nbsp;") { txtPatientAddress.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[5].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text != "&nbsp;") { txtState.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[15].Text != "&nbsp;") { txtBirthdate.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[15].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[4].Text != "&nbsp;") { txtPatientAge.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[4].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[13].Text != "&nbsp;") { txtSocialSecurityNumber.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[13].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[31].Text != "&nbsp;") { if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[31].Text.ToString() == "True") { chkTransportation.Checked = true; } else { chkTransportation.Checked = false; } }
            Billing_Sys_ManageNotesBO manageNotes = new Billing_Sys_ManageNotesBO();
            DataSet _ds = manageNotes.GetCaseDetails(txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                txtCaseID.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                extddlCaseType.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                extddlInsuranceCompany.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString() != "")
                {
                    extddlMedicalOffice.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                    extddlMedicalOffice.Enabled = false;
                }
                else
                {
                    extddlMedicalOffice.Enabled = true;
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
            if (Session["Flag"] != null)
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
                Billing_Sys_ManageNotesBO manageNotes = new Billing_Sys_ManageNotesBO();
                DataSet _ds = manageNotes.GetCaseDetails(txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    txtCaseID.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    extddlCaseType.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    extddlInsuranceCompany.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    if (_ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString() != "")
                    {
                        extddlMedicalOffice.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                        extddlMedicalOffice.Enabled = false;
                    }
                    else
                    {
                        extddlMedicalOffice.Enabled = true;
                    }
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
            ds = _patient_TVBO.GetAppointPatientDetails(i_schedule_id);
            if (ds.Tables[0].Rows.Count != 0)
            {
                dt = ds.Tables[0].Clone();

                if (ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;") { txtPatientID.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;") { txtPatientFName.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;") { txtMI.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;") { txtPatientLName.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;") { txtPatientPhone.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;") { txtPatientAddress.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(52).ToString() != "&nbsp;") { txtState.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(52).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString() != "&nbsp;") { txtBirthdate.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;") { txtPatientAge.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() != "&nbsp;") { txtSocialSecurityNumber.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "&nbsp;") { if (ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() == "True") { chkTransportation.Checked = true; } else { chkTransportation.Checked = false; } }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;") { txtPatientFName.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(34).ToString() != "&nbsp;") { txtCaseID.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(34).ToString(); }
                extddlInsuranceCompany.Flag_ID = ds.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                extddlCaseType.Flag_ID = ds.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(46).ToString() != "&nbsp;") { txtStudyNumber.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(46).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(33).ToString() != "") { extddlCaseType.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(33).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(32).ToString() != "") { extddlInsuranceCompany.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(32).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(35).ToString() != "") { lblDoctor.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(35).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(36).ToString() != "") { ddlType.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(36).ToString(); }

              if (ds.Tables[0].Rows[0].ItemArray.GetValue(6).ToString() != "&nbsp;") { TextBox3.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(6).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(37).ToString() != "")
                {
                    string startTime = ds.Tables[0].Rows[0].ItemArray.GetValue(37).ToString();
                    ddlHours.SelectedValue = startTime.Substring(0, startTime.IndexOf(".")).PadLeft(2, '0');
                    ddlMinutes.SelectedValue = startTime.Substring(startTime.IndexOf(".") + 1, startTime.Length - (startTime.IndexOf(".") + 1)).PadLeft(2, '0');
                }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(38).ToString() != "")
                {
                    string endTime = ds.Tables[0].Rows[0].ItemArray.GetValue(38).ToString();
                    ddlEndHours.SelectedValue = endTime.Substring(0, endTime.IndexOf(".")).PadLeft(2, '0');
                    ddlEndMinutes.SelectedValue = endTime.Substring(endTime.IndexOf(".") + 1, (endTime.Length - (endTime.IndexOf(".") + 1))).PadLeft(2, '0');
                }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(39).ToString() != "") { ddlTime.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(39).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(40).ToString() != "") { ddlEndTime.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(40).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(41).ToString() != "") { extddlDoctor.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(41).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(42).ToString() != "") { txtNotes.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(42).ToString(); }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(45).ToString() != "") { extddlMedicalOffice.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(45).ToString(); /*extddlMedicalOffice.Enabled = false;*/ }
                btnSave.Visible = false;
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(43).ToString() != "&nbsp;")
                {
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(43).ToString() == "True")
                    {
                        btnUpdate.Visible = false; ;
                        //extddlDoctor.Enabled = false;
                        ddlType.Enabled = false;
                        ddlTestNames.Enabled = false;
                        txtNotes.ReadOnly = true;
                       // btnDelete.Visible = false;
                        //deleteflag = true;

                    }
                    else
                    {
                        //lblDoctor.Visible = true;
                        //extddlDoctor.Visible = false;
                        btnUpdate.Visible = true;
                    }
                }
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(44).ToString() != "&nbsp;") { txtPatientCompany.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(44).ToString(); }
                if (ds.Tables[0].Rows[0]["EVENT_DATE"].ToString() != "&nbsp;") { txtAppDate.Text = ds.Tables[0].Rows[0]["EVENT_DATE"].ToString(); }
                // 12April,2010 add chart no on page-- sachin
                if (ds.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString() != "&nbsp;") { txtChartNo.Text = ds.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString(); }
                // 15 April 2010 add TRANSPORTATION_COMPANY field -- sachin
                if (ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString() != "&nbsp;")
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString()) > 0 && chkTransportation.Checked)
                    {
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
                //=================================================================================
                //txtDoctorName.Visible = false;
                ds = new DataSet();
                _patient_TVBO = new Patient_TVBO();
                ds = _patient_TVBO.GetAppointProcCode(i_schedule_id);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    foreach (DataGridItem lst in grdProcedureCode.Items)
                    {
                        if (lst.Cells[1].Text == dr.ItemArray.GetValue(0).ToString())
                        {
                            CheckBox chkSelect = (CheckBox)lst.Cells[1].FindControl("chkSelect");
                            chkSelect.Checked = true;
                            //chkSelect.Enabled = false;
                            DropDownList ddlStatu = (DropDownList)lst.Cells[4].FindControl("ddlStatus");
                            ddlStatu.SelectedValue = dr.ItemArray.GetValue(1).ToString();
                            TextBox txtStudyNumber = (TextBox)lst.Cells[7].FindControl("txtStudyNo");
                            txtStudyNumber.Text = dr.ItemArray.GetValue(7).ToString();
                            TextBox txt_temp_Notes = (TextBox)lst.FindControl("txtProcNotes");
                            txt_temp_Notes.Text = dr.ItemArray.GetValue(8).ToString();

                            if (ddlStatu.SelectedValue == "2")
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Visit completed.";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                txtEventStatus.Text = "2";
                            }

                            if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            {
                                //if (Convert.ToInt32(dr.ItemArray.GetValue(1).ToString()) == 1)
                                
                                if (Convert.ToInt32(dr.ItemArray.GetValue(1).ToString()) != 0)
                                {
                                    TextBox txtDate = (TextBox)lst.Cells[5].FindControl("txtReScheduleDate");
                                    txtDate.Text = dr.ItemArray.GetValue(2).ToString();
                                    string startTime = dr.ItemArray.GetValue(3).ToString();
                                    DropDownList ddlHR = (DropDownList)lst.Cells[6].FindControl("ddlReSchHours");
                                    ddlHR.SelectedValue = startTime.Substring(0, startTime.IndexOf(".")).PadLeft(2, '0');
                                    DropDownList ddlMM = (DropDownList)lst.Cells[6].FindControl("ddlReSchMinutes");
                                    ddlMM.SelectedValue = startTime.Substring(startTime.IndexOf(".") + 1, startTime.Length - (startTime.IndexOf(".") + 1)).PadLeft(2, '0');
                                    DropDownList ddlTIME = (DropDownList)lst.Cells[6].FindControl("ddlReSchTime");
                                    ddlTIME.SelectedValue = dr.ItemArray.GetValue(4).ToString();
                                }
                            }
                            //lst.Cells[8].Text = dr.ItemArray.GetValue(1).ToString();
                            //lst.Cells[6].Text = dr.ItemArray.GetValue(5).ToString();
                            //lst.Cells[7].Text = dr.ItemArray.GetValue(6).ToString();
                            
                            lst.BackColor = System.Drawing.Color.LightSeaGreen;
                        }

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

    protected void grdProcedureCode_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.Item.Cells[5].Controls.Count > 0)
            {
                DropDownList dlReSchHours;
                dlReSchHours = (DropDownList)e.Item.Cells[5].FindControl("ddlReSchHours");
                DropDownList dlReSchMinutes;
                dlReSchMinutes = (DropDownList)e.Item.Cells[5].FindControl("ddlReSchMinutes");
                DropDownList dlReSchTime;
                dlReSchTime = (DropDownList)e.Item.Cells[5].FindControl("ddlReSchTime");


                for (int i = 0; i <= 12; i++)
                {
                    if (i > 9)
                    {
                        dlReSchHours.Items.Add(i.ToString());

                    }
                    else
                    {
                        dlReSchHours.Items.Add("0" + i.ToString());

                    }
                }
                for (int i = 0; i < 60; i++)
                {
                    if (i > 9)
                    {
                        dlReSchMinutes.Items.Add(i.ToString());

                    }
                    else
                    {
                        dlReSchMinutes.Items.Add("0" + i.ToString());

                    }
                }
                dlReSchTime.Items.Add("AM");
                dlReSchTime.Items.Add("PM");
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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int Flag=0;
        try
        {
            bool blError = false;
            // 16 April 2010 check the extddltransport dropdown it fill or not --- sachin
            if (chkTransportation.Checked == true && extddlTransport.Text == "NA")
            {
                Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select transport company !');</script>");
                return;
            }
            //======================================================

            //Change No Compulsion For Selecting codes while changing doctor or transfer or office --Tushar 20April
            foreach (DataGridItem lst in grdProcedureCode.Items)
            {

                CheckBox chkSelect = (CheckBox)lst.Cells[1].FindControl("chkSelect");
                if (chkSelect.Checked == true)
                {
                    DropDownList ddlStatu = (DropDownList)lst.Cells[4].FindControl("ddlStatus");
                    if (ddlStatu.SelectedValue != "0")
                    {
                        Flag = 1;
                    }
                }
            }
            //end code








            ////////////////////VALIDATION
            if (Flag == 1)
            {
                foreach (DataGridItem lst in grdProcedureCode.Items)
                {

                    CheckBox chkSelect = (CheckBox)lst.Cells[1].FindControl("chkSelect");
                    if (chkSelect.Checked == true)
                    {
                        DropDownList ddlStatu = (DropDownList)lst.Cells[4].FindControl("ddlStatus");
                        if (ddlStatu.SelectedValue == "0")
                        {
                            lblMsg.Text = "Please select procedure status";
                            lblMsg.Visible = true;
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            blError = true;
                            break;
                        }

                        if (Convert.ToInt32(ddlStatu.SelectedValue) == 1)
                        {
                            TextBox txtDate = (TextBox)lst.Cells[5].FindControl("txtReScheduleDate");
                            if (txtDate.Text == "")
                            {
                                lblMsg.Text = "Please enter valid rescheduled date";
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                blError = true;
                                break;
                            }
                            try
                            {
                                DateTime departDate = DateTime.Parse(txtDate.Text);
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Text = "Please enter valid rescheduled date";
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                blError = true;
                                break;
                            }

                            DropDownList ddlHR = (DropDownList)lst.Cells[6].FindControl("ddlReSchHours");
                            if (ddlHR.SelectedValue == "00")
                            {
                                lblMsg.Text = "Please enter valid rescheduled time";
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                blError = true;
                                break;
                            }

                        }
                    }
                }
            }
            ///////////////////

            if (blError == false)
            {

                int eventID = Convert.ToInt32(Session["SCHEDULEDID"].ToString());



                //string sz_date = Request.QueryString["_date"].ToString();
                //sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));
                //Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                //ArrayList objAdd = new ArrayList();
                //objAdd.Add(txtPatientID.Text);
                //objAdd.Add(sz_date);
                //objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());
                //objAdd.Add(txtNotes.Text);
                //objAdd.Add(extddlDoctor.Text);
                //objAdd.Add(ddlTestNames.SelectedValue);
                //objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //objAdd.Add(ddlTime.SelectedValue);
                //objAdd.Add(ddlEndHours.SelectedValue.ToString() + "." + ddlEndMinutes.SelectedValue.ToString());
                //objAdd.Add(ddlEndTime.SelectedValue);
                //objAdd.Add(extddlReferenceFacility.Text);
                //objAdd.Add(eventID);

                //eventID = _bill_Sys_Calender.UPDATE_Event(objAdd);
                // _bill_Sys_Calender.Delete_Event_RefferPrcedure(eventID);
                //foreach (ListItem lst in ddlTestNames.Items)
                //{
                //    if (lst.Selected == true)
                //    {
                //        objAdd = new ArrayList();
                //        objAdd.Add(lst.Value);
                //        objAdd.Add(eventID);
                //        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                //        { objAdd.Add(2); }
                //        else { objAdd.Add(0); }
                //        _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                //    }
                //}
                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                ArrayList objAdd = new ArrayList();
               // if (txtCompanyID.Text != "CO000000000000000081")
                {
                    _bill_Sys_Calender.Delete_Event_RefferPrcedure(eventID);
                }
                AddedEvetDetail _addedEvent;
                ArrayList objAddedEvent = new ArrayList();
                 
                    foreach (DataGridItem lst in grdProcedureCode.Items)
                    {

                        CheckBox chkSelect = (CheckBox)lst.Cells[1].FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            TextBox txtStudyNumber = (TextBox)lst.Cells[5].FindControl("txtStudyNo");
                            TextBox txtProcNotes = (TextBox)lst.Cells[8].FindControl("txtProcNotes");
                            DropDownList ddlStatu = (DropDownList)lst.Cells[4].FindControl("ddlStatus");
                            objAdd = new ArrayList();
                            objAdd.Add(lst.Cells[1].Text);
                            objAdd.Add(eventID);
                            objAdd.Add(ddlStatu.SelectedValue);
                            //objAdd.Add(txtStudyNumber.Text);
                            int _evenTID = 0;
                            ///////////////////if Status is Reschduled


                            if (Convert.ToInt32(ddlStatu.SelectedValue) == 1)
                            {

                                TextBox txtDate = (TextBox)lst.Cells[5].FindControl("txtReScheduleDate");
                                DropDownList ddlHR = (DropDownList)lst.Cells[6].FindControl("ddlReSchHours");
                                DropDownList ddlMM = (DropDownList)lst.Cells[6].FindControl("ddlReSchMinutes");
                                DropDownList ddlTIME = (DropDownList)lst.Cells[6].FindControl("ddlReSchTime");
                                if (objAddedEvent.Count > 0)
                                {
                                    foreach (AddedEvetDetail _Obj in objAddedEvent)
                                    {


                                        if (_Obj.EventDate == Convert.ToDateTime(txtDate.Text) && _Obj.EventTime == Convert.ToDecimal(ddlHR.SelectedValue.ToString() + "." + ddlMM.SelectedValue.ToString()))
                                        {
                                            _evenTID = _Obj.EventID;
                                        }

                                    }
                                }
                                if (_evenTID == 0)
                                {
                                    Bill_Sys_Calender _bill_Calender = new Bill_Sys_Calender();
                                    ArrayList _objAdd = new ArrayList();
                                    _objAdd.Add(txtPatientID.Text);//SZ_CASE_ID 0
                                    _objAdd.Add(txtDate.Text);//DT_EVENT_DATE 1
                                    _objAdd.Add(ddlHR.SelectedValue.ToString() + "." + ddlMM.SelectedValue.ToString());//DT_EVENT_TIME 2
                                    _objAdd.Add(txtNotes.Text);//SZ_EVENT_NOTES 3
                                    _objAdd.Add(extddlDoctor.Text);//SZ_DOCTOR_ID 4
                                    _objAdd.Add(lst.Cells[1].Text);//SZ_TYPE_CODE_ID 5
                                    _objAdd.Add(txtPatientCompany.Text);//SZ_COMPANY_ID6
                                    _objAdd.Add(ddlTIME.SelectedValue);//DT_EVENT_TIME_TYPE7
                                 // DateTime dtEnteredLastDate = Convert.ToDateTime(Convert.ToDateTime(ddlHR.SelectedValue.ToString() + "." + ddlMM.SelectedValue.ToString())).ToString("MM/dd/yyyy");
                                    //DateTime dtEnteredLastDate = Convert.ToDateTime(Convert.ToDateTime(ddlHR.SelectedValue + "." + ddlMM.SelectedValue));


                                    DateTime dtEnteredDate = Convert.ToDateTime(Convert.ToDateTime(txtDate.Text).ToString("MM/dd/yyyy") + " " + ddlHR.SelectedValue + ":" + ddlMM.SelectedValue + " " + ddlTIME.SelectedValue);
                                    log.Debug("Entered date = " + dtEnteredDate);
                                    DateTime dtEnteredLastDate = dtEnteredDate.AddMinutes(Convert.ToInt32(Session["INTERVAL"]));
                                    log.Debug("EnteredLast date = " + dtEnteredLastDate);
                                    Decimal endTime = 0.00M;                                   
                                    endTime = Convert.ToDecimal(dtEnteredLastDate.ToString("hh") + "." + dtEnteredLastDate.ToString("mm"));
                                    log.Debug("end Time = " + endTime);
                                    string endTimeType = ddlTIME.SelectedValue;
                                    if (Convert.ToDecimal(ddlHR.SelectedValue.ToString() + "." + ddlMM.SelectedValue.ToString()) < Convert.ToDecimal(12.00))
                                    {
                                        if (endTime >= Convert.ToDecimal(12.00))
                                        {
                                            if (endTimeType == "AM")
                                            {
                                                endTimeType = "PM";
                                            }
                                            else if (endTimeType == "PM")
                                            {
                                                endTimeType = "AM";
                                            }
                                        }
                                    }
                                    _objAdd.Add(endTime); //DT_EVENT_END_TIME 8
                                    _objAdd.Add(endTimeType);//DT_EVENT_END_TIME_TYPE9
                                    _objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);//SZ_REFERENCE_ID10
                                    _objAdd.Add("False");//@BT_STATUS11
                                    // #149 13 April,2010 change the BT_TRANSPOTATION field patient table to txn_calender_event -- sachin
                                    if (chkTransportation.Checked == true) { _objAdd.Add(1); } else { _objAdd.Add(0); }//BT_TRANSPORTATION11\2
                                    //==================================
                                    if (chkTransportation.Checked == true) { _objAdd.Add(Convert.ToInt32(extddlTransport.Text)); } else { _objAdd.Add(0); }//I_TRANSPORTATION_COMPANY13

                                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                                    { _objAdd.Add(Session["Office_Id"].ToString());/*_objAdd.Add(extddlMedicalOffice.Text);*/ } //SZ_OFFICE_ID14
                                    _evenTID = _bill_Calender.Save_Event(_objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());



                                    _addedEvent = new AddedEvetDetail();
                                    _addedEvent.EventID = _evenTID;
                                    _addedEvent.EventDate = Convert.ToDateTime(txtDate.Text);
                                    _addedEvent.EventTime = Convert.ToDecimal(ddlHR.SelectedValue.ToString() + "." + ddlMM.SelectedValue.ToString());
                                    objAddedEvent.Add(_addedEvent);

                                }
                                ArrayList oAdd = new ArrayList();
                                oAdd.Add(lst.Cells[1].Text);
                                oAdd.Add(_evenTID);
                                oAdd.Add(0);
                                //oAdd.Add(txtStudyNumber.Text);
                                _bill_Sys_Calender.Save_Event_RefferPrcedure(oAdd);



                                objAdd.Add(txtDate.Text);
                                objAdd.Add(ddlHR.SelectedValue.ToString() + "." + ddlMM.SelectedValue.ToString());
                                objAdd.Add(ddlTIME.SelectedValue);
                                objAdd.Add(_evenTID);
                                objAdd.Add(txtStudyNumber.Text);
                                objAdd.Add(txtProcNotes.Text);

                                _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);

                            }
                            ///////////////////if Status is Reschduled


                            ///////////////////if Status is Visit done
                            else if (Convert.ToInt32(ddlStatu.SelectedValue) == 2)
                            {
                                Bill_Sys_Calender _bill_Calender = new Bill_Sys_Calender();
                                objAdd.Add(txtStudyNumber.Text);
                                objAdd.Add(txtProcNotes.Text);
                                //if (txtCompanyID.Text != "CO000000000000000081")
                                {
                                    _bill_Sys_Calender.Save_Event_OtherVType(objAdd);
                                }
                                ////else
                                //{
                                //    _bill_Sys_Calender.update_Event_OtherVType(objAdd);

                                //}

                                //_bill_Sys_Calender.Update_Visit_Complete();
                                Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
                                ArrayList arrOBJ = new ArrayList();
                                arrOBJ.Add(extddlDoctor.Text);
                                arrOBJ.Add(lst.Cells[1].Text);
                                arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                arrOBJ.Add(lst.Cells[1].Text);
                                _bill_Sys_ReferalEvent.AddDoctorAmount(arrOBJ);

                                //arrOBJ = new ArrayList();
                                //arrOBJ.Add(txtPatientID.Text);
                                //arrOBJ.Add(extddlDoctor.Text);
                                //arrOBJ.Add(Request.QueryString["_date"].ToString().Substring(0, Request.QueryString["_date"].ToString().IndexOf(" ")));
                                //arrOBJ.Add(lst.Cells[1].Text);
                                //arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                //arrOBJ.Add(ddlType.SelectedValue);
                                //_bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);

                            }
                            ///////////////////if Status is Visit done

                            else
                            {
                                Bill_Sys_Calender _bill_Calender = new Bill_Sys_Calender();
                                objAdd.Add(txtStudyNumber.Text);
                                objAdd.Add(txtProcNotes.Text);
                                _bill_Sys_Calender.Save_Event_OtherVType(objAdd);
                            }
                            lst.BackColor = System.Drawing.Color.LightSeaGreen;


                            //Code to update Procedure Code Details
                            ArrayList objArr = new ArrayList();
                            TextBox txtDateComplete = (TextBox)lst.Cells[5].FindControl("txtReScheduleDate");
                            DropDownList ddlHRComplete = (DropDownList)lst.Cells[6].FindControl("ddlReSchHours");
                            DropDownList ddlMMComplete = (DropDownList)lst.Cells[6].FindControl("ddlReSchMinutes");
                            DropDownList ddlTIMEComplete = (DropDownList)lst.Cells[6].FindControl("ddlReSchTime");

                            decimal endTime1 = Convert.ToDecimal(ddlHRComplete.SelectedValue.ToString() + "." + ddlMMComplete.SelectedValue.ToString());
                            endTime1 = endTime1 + Convert.ToDecimal("0." + Session["INTERVAL"].ToString());
                            string endTimeType1 = ddlTIMEComplete.SelectedValue;
                            if (Convert.ToDecimal(ddlHRComplete.SelectedValue.ToString() + "." + ddlMMComplete.SelectedValue.ToString()) < Convert.ToDecimal(12.00))
                            {
                                if (endTime1 >= Convert.ToDecimal(12.00))
                                {
                                    if (endTimeType1 == "AM")
                                    {
                                        endTimeType1 = "PM";
                                    }
                                    else if (endTimeType1 == "PM")
                                    {
                                        endTimeType1 = "AM";
                                    }
                                }
                            }

                            objArr.Add(lst.Cells[1].Text);
                            objArr.Add(_evenTID);
                            objArr.Add(ddlStatu.SelectedValue);
                            objArr.Add(txtStudyNumber.Text);
                            objArr.Add(txtProcNotes.Text);
                            objArr.Add(txtDateComplete.Text);
                            objArr.Add(endTime1.ToString());
                            objArr.Add(endTimeType1.ToString());
                            if (ddlStatu.SelectedValue != "1")
                                _bill_Sys_Calender.Update_ReShedule_Info(objArr);
                            //end Code
                        }
                    }
                
                _bill_Sys_Calender = new Bill_Sys_Calender();
                objAdd = new ArrayList();
                objAdd.Add(eventID);
               
                objAdd.Add(false);
                objAdd.Add(txtNotes.Text);

                //_bill_Sys_Calender.UPDATE_Event_Status(objAdd); // Pass only two argument
                _bill_Sys_Calender.UPDATE_EventNotes_Status(objAdd,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString()); // add one filed array list(txtnotes)
                

                // 16 April 2010 update the transport company for particular event --- sachin
                //if (bt_Status1 == true || bt_Status3 == true || bt_Status2==true)
                //{
                //_bill_Sys_Calender = new Bill_Sys_Calender();
                ArrayList objUpdate = new ArrayList();
                objUpdate.Add(eventID);

                if (chkTransportation.Checked == true) { objUpdate.Add(1); } else { objUpdate.Add(0); }
                if (chkTransportation.Checked == true) { objUpdate.Add(Convert.ToInt32(extddlTransport.Text)); } else { objUpdate.Add(null); }

                _bill_Sys_Calender.UPDATE_TransportationCompany_Event(objUpdate);

                //}


                //Code To update Doctor Id  
                   if(extddlDoctor.Visible==true  && extddlDoctor.Text!="" && extddlDoctor.Text!="NA")
                {
                  ArrayList  objUpdateDoctor = new ArrayList();
                  objUpdateDoctor.Add(eventID);
                  objUpdateDoctor.Add(extddlDoctor.Text);
                  _bill_Sys_Calender.Update_Doctor_Id(objUpdateDoctor);
                }

                //end code


                //Code To update Office Id
                if (extddlMedicalOffice.Visible == true && extddlMedicalOffice.Text != "" && extddlMedicalOffice.Text != "NA")
                {
                    ArrayList objUpdateOffice = new ArrayList();
                    
                    objUpdateOffice.Add(txtPatientID.Text);
                    objUpdateOffice.Add(extddlMedicalOffice.Text);
                    objUpdateOffice.Add(txtPatientCompany.Text);
                    objUpdateOffice.Add(extddlDoctor.Text);
                    _bill_Sys_Calender.Update_Office_Id(objUpdateOffice);
                }

                //end code

                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "EVENT_UPDATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Event Id : " + eventID + " updated .";
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                Session["PopUp"] = "True";

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href=window.parent.document.location.href;window.self.close();</script>");

                if (Request.QueryString["From"] == null)
                {

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_AppointPatientEntry.aspx';</script>");
                }
                else
                {
                    if (Request.QueryString["GRD_ID"] != null)
                        Session["GRD_ID"] = Request.QueryString["GRD_ID"].ToString();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('div1').style.visibility = 'hidden';window.parent.document.location.href='Bill_SysPatientDesk.aspx';</script>");
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

    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int eventID = Convert.ToInt32(Session["SCHEDULEDID"].ToString());
    //        bool blError = false;
    //        ArrayList arrlst = new ArrayList();
    //        foreach (DataGridItem lst in grdProcedureCode.Items)
    //        {
    //            CheckBox chkSelect = (CheckBox)lst.Cells[1].FindControl("chkSelect");
    //            if (chkSelect.Checked == true)
    //            {
    //                //DropDownList ddlStatu = (DropDownList)lst.Cells[4].FindControl("ddlStatus");

    //                //if (ddlStatu.SelectedValue == "1" || ddlStatu.SelectedValue == "3" || ddlStatu.SelectedValue == "0")
    //                //{

    //                //int eventID = Convert.ToInt32(Session["SCHEDULEDID"].ToString());
    //                //Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
    //                //ArrayList objAdd = new ArrayList();
    //                //_bill_Sys_Calender.Delete_SchedhuledEvent(eventID);

    //                //}
    //                //else if (ddlStatu.SelectedValue == "2")
    //                //{
    //                //    lblMsg.Text = "You cannot delete completed visit";
    //                //    lblMsg.Visible = true;
    //                //    lblMsg.ForeColor = System.Drawing.Color.Red;
    //                //    //blError = true;
    //                //    break;
    //                //}
    //                //if (lst.Cells[8].Text == "2")
    //                //{
    //                //    lblMsg.Text = "You cannot delete completed visit";
    //                //    lblMsg.Visible = true;
    //                //    lblMsg.ForeColor = System.Drawing.Color.Red;
    //                //}

    //                    arrlst.Add(lst.Cells[7].Text);

    //            }

    //        }
    //        if(
    //        for (int i = 0; i < arrlst.Count; i++)
    //        {
    //            Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
    //            ArrayList objAdd = new ArrayList();
    //            _bill_Sys_Calender.Delete_SchedhuledEvent(eventID, arrlst[i].ToString(), "DELETE");
    //            blError = true;
    //        }
    //        if (blError == true)
    //        {
    //            Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
    //            _bill_Sys_Calender.Delete_SchedhuledEvent(eventID, "", "DELETE_EVENT_TXN");
    //        }

    //        LoadTypes();
    //        GETAppointPatientDetail(Convert.ToInt32(Session["SCHEDULEDID"]));
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int eventID = Convert.ToInt32(Session["SCHEDULEDID"].ToString());
            string szStatusFlag = txtEventStatus.Text;
            //if (szStatusFlag == "2")
            //{
            //    lblMsg.Text = "You cannot delete completed visit";
            //    lblMsg.Visible = true;
            //    lblMsg.ForeColor = System.Drawing.Color.Red;
            //}
            //else
            //{
                DateTime sz_Evt_date = Convert.ToDateTime(txtAppDate.Text + " " + ddlHours.SelectedValue.ToString() + ":" + ddlMinutes.SelectedValue.ToString() + " " + ddlTime.SelectedValue.ToString());
                DateTime sz_date = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy HH:mm tt"));
                if (DateTime.Compare(sz_Evt_date, sz_date) > 0)
                {
                    Bill_Sys_Calender objCal = new Bill_Sys_Calender();
                    objCal.Delete_PatientOfficeID(txtPatientID.Text, extddlMedicalOffice.Text, eventID,txtCompanyID.Text);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_AppointPatientEntry.aspx';window.self.close();</script>");
                }
            
                else
                {
                    //lblMsg.Text = "You can delete only future appointment";
                    //lblMsg.Visible = true;
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                    Bill_Sys_Calender objCal = new Bill_Sys_Calender();
                    objCal.Delete_PatientOfficeID(txtPatientID.Text, extddlMedicalOffice.Text, eventID, txtCompanyID.Text);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_AppointPatientEntry.aspx';window.self.close();</script>");

                }
            this._DAO_NOTES_EO = new DAO_NOTES_EO();
            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "EVENT_DELETED";
            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Event Id : " + eventID + " deleted.";
            this._DAO_NOTES_BO = new DAO_NOTES_BO();
            this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
            //}
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

    class AddedEvetDetail
    {
        int eventID;
        public int EventID
        {
            get
            {
                return eventID;
            }
            set
            {
                eventID = value;
            }
        }
        DateTime eventDate;
        public DateTime EventDate
        {
            get
            {
                return eventDate;
            }
            set
            {
                eventDate = value;
            }
        }
        decimal eventTime;
        public decimal EventTime
        {
            get
            {
                return eventTime;
            }
            set
            {
                eventTime = value;
            }
        }
    }

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void lnkPatientDesk_Click(object sender, EventArgs e)
    {
        Session["SZ_CASE_ID"] = txtCaseID.Text;
        Session["PROVIDERNAME"] = txtCaseID.Text;
        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();

        _bill_Sys_CaseObject.SZ_PATIENT_ID = txtPatientID.Text;
        _bill_Sys_CaseObject.SZ_CASE_ID = txtCaseID.Text;
        _bill_Sys_CaseObject.SZ_PATIENT_NAME = txtPatientFName.Text + txtPatientLName.Text;
        _bill_Sys_CaseObject.SZ_COMAPNY_ID = txtCompanyID.Text;

        //_bill_Sys_CaseObject.SZ_CASE_NO =t
        _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);


        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

        _bill_Sys_Case = new Bill_Sys_Case();
        _bill_Sys_Case.SZ_CASE_ID = txtCaseID.Text; 
        Session["CASEINFO"] = _bill_Sys_Case;
        Session["QStrCaseID"] = txtCaseID.Text;
        Session["Case_ID"] = txtCaseID.Text;
        Session["Archived"] = "0";
        Session["QStrCID"] = txtCaseID.Text;
        Session["SelectedID"] = txtCaseID.Text;

        ///////////

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_SysPatientDesk.aspx?Flag=true';</script>");
        //Response.Redirect("Bill_SysPatientDesk.aspx?Flag=true", true);
    }
}
