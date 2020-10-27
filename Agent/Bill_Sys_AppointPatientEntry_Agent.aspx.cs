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
using Componend;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Scheduling;
using Visit_Application;

public partial class Agent_Bill_Sys_AppointPatientEntry_Visit : PageBase
{
    #region "Local Variables"

    FillPatientGrid FPG = new FillPatientGrid();
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    Bill_Sys_Schedular _obj;
    Calendar_DAO objCalendar = null;
    private Boolean blnWeekShortNames = true;
    private string szDateColor_NA = "#ff6347";
    private string szDateColor_TODAY = "#FFFF80";
    private string UserID;
    private DataSet ds;
    private Patient_TVBO _patient_TVBO;
    private String strcompanyid;
    private PopupBO _popupBO;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    string canCopy = "";

    #endregion

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {

        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            UserID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            if (Request.QueryString["Flag"] != null)
            {
                if (Request.QueryString["Flag"].ToString() == "true")
                {
                    if (Request.QueryString["CaseID"] != null)
                    {
                        Session["SZ_CASE_ID"] = Request.QueryString["CaseID"].ToString();
                        Session["PROVIDERNAME"] = Request.QueryString["CaseID"].ToString();
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(Request.QueryString["CaseID"].ToString(), "");
                        _bill_Sys_CaseObject.SZ_CASE_ID = Request.QueryString["CaseID"].ToString();
                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                    }
                }
            }
            txtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnGo.Attributes.Add("onclick", "javascript:return checkForTestFacility();");
            canCopy = (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_COPY_PATIENT_TO_TEST_FACILITY).ToString();
            if (canCopy == "1")
            {
                string temp = "Patient will be copied to " + extddlReferringFacility.Selected_Text + " account";
                lblMsg2.Visible = true;
                lblMsg2.Text = temp;
                lblMsg2.ForeColor = System.Drawing.Color.Red;
                //btnDuplicateSaveClick.Attributes.Add("onclick", "javascript:return yesnopopup();");
            }

            if (Session["PopUp"] != null)
            {
                if (Session["TestFacilityID"] != null && Session["AppointmentDate"] != null)
                {
                    if (Session["PopUp"].ToString() == "True")
                    {
                        extddlReferringFacility.Flag_ID = txtCompanyID.Text.ToString();
                        extddlReferringFacility.Text = Session["TestFacilityID"].ToString();
                        string strSelectedDate = Session["AppointmentDate"].ToString();
                        Session["AppointmentDate"] = null;
                        Session["TestFacilityID"] = null;
                        GetCalenderDayAppointments(Convert.ToDateTime(strSelectedDate).ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
                        Session["PopUp"] = null;
                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        {
                            extddlReferringFacility.Visible = false;
                        }
                    }
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    Session["Page_Index"] = "";

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        extddlReferringFacility.Visible = false;
                    }
                    extddlReferringFacility.Flag_ID = txtCompanyID.Text.ToString();
                    extddlMedicalOffice.Flag_ID = txtCompanyID.Text.ToString();

                    _obj = new Bill_Sys_Schedular();
                    if (extddlReferringFacility.Visible == false)
                    {
                        if (Request.QueryString["idate"] != null)
                        {
                            if (Request.QueryString["idate"].ToString() != "")
                            {
                                GetCalenderDayAppointments(Request.QueryString["idate"].ToString(), txtCompanyID.Text);
                            }
                            else
                            {
                                GetCalenderDayAppointments(System.DateTime.Today.Date.ToString("MM/dd/yyyy"), txtCompanyID.Text);
                            }
                        }
                        else // From Calendar Popup
                        {
                            GetCalenderDayAppointments(System.DateTime.Today.Date.ToString("MM/dd/yyyy"), txtCompanyID.Text);

                        }

                    }
                    else
                    {
                        GetCalenderDayAppointments(System.DateTime.Today.Date.ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
                    }
                    if (Request.QueryString["Flag"] != null)
                    {
                        Session["Flag"] = true;
                        lblHeaderPatientName.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME;
                        btnAddPatient.Visible = false;
                    }
                    else
                    {
                        Session["Flag"] = null;
                        lblHeaderPatientName.Text = "";
                    }
                }
            }
            //string strDate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");

            //objCalendar = new Calendar_DAO();
            //objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).AddMonths(-1).ToString("MMM").ToUpper().ToString();
            //objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).AddMonths(-1).ToString("MMM").ToUpper().ToString();
            //objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).AddMonths(-1).Year.ToString();

            //showCalendar(objCalendar);

            //objCalendar = new Calendar_DAO();
            //objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            //objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            //objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

            //showCalendar(objCalendar);


            //objCalendar = new Calendar_DAO();
            //objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).AddMonths(1).ToString("MMM").ToUpper().ToString();
            //objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).AddMonths(1).ToString("MMM").ToUpper().ToString();
            //objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).AddMonths(1).Year.ToString();

            //showCalendar(objCalendar);

            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                if (Session["Flag"] == null)
                {
                    Bill_Sys_BillingCompanyDetails_BO objOffID3 = new Bill_Sys_BillingCompanyDetails_BO();
                    string sz_Off_Id3 = objOffID3.GetRefDocID(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    if (sz_Off_Id3 != "")
                        btnAddPatient.Visible = false;
                    else
                        btnAddPatient.Visible = true;

                }
            }
            else
            {
                btnAddPatient.Visible = false;
            }
            // Display Chart number according to setting
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                txtRefChartNumber.Visible = false;
                lblChartNumber.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                //txtEnteredDate.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
                txtPatientExistMsg.Value = "";
                int iYear = DateTime.Today.Year;
                BindYearDropDown(iYear);
                ddlYearList.SelectedValue = iYear.ToString();
                ddlMonthList.SelectedValue = DateTime.Today.Month.ToString();
                Session["PRV_MONTH"] = Convert.ToDateTime(DateTime.Today).AddMonths(-1).ToString("MM/dd/yyyy");
                Session["CUR_MONTH"] = Convert.ToDateTime(DateTime.Today).ToString("MM/dd/yyyy");
                Session["NEXT_MONTH"] = Convert.ToDateTime(DateTime.Today).AddMonths(1).ToString("MM/dd/yyyy");
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    lblTestFacility1.Visible = false;
                }
            }
            LoadCalendarAccordingToYearAndMonth();
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

        }//Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

    private void BindYearDropDown(int p_iYear)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ddlYearList.Items.Clear();
            //for (int i = p_iYear; i > p_iYear - 20; i--)
            for (int i = p_iYear - 10; i <= (p_iYear + 1); i++)
                ddlYearList.Items.Add(i.ToString());
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

        }//Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void LoadCalendarAccordingToYearAndMonth()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            string strDate = "";
            strDate = Convert.ToDateTime(Session["PRV_MONTH"].ToString()).ToString("MM/dd/yyyy");
            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();


            showCalendar(objCalendar);

            strDate = Convert.ToDateTime(Session["CUR_MONTH"].ToString()).ToString("MM/dd/yyyy");
            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

            showCalendar(objCalendar);

            strDate = Convert.ToDateTime(Session["NEXT_MONTH"].ToString()).ToString("MM/dd/yyyy");
            objCalendar = new Calendar_DAO();
            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).AddMonths(1).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

            showCalendar(objCalendar);

            Session["FROM"] = null;
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

        }//Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void LoadTypes()
    {
        ArrayList objArr = new ArrayList();

        objArr.Add(extddlReferenceFacility.Text);
        string sz_datetime = hdnObj.Value;

        objArr.Add(sz_datetime.Substring(sz_datetime.IndexOf("!") + 1, 20));
        Bill_Sys_ManageVisitsTreatmentsTests_BO objBO = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
        ddlTestNames.Items.Clear();
        ddlTestNames.DataSource = objBO.GetReferringProcCodeList(objArr);
        ddlTestNames.DataTextField = "description";
        ddlTestNames.DataValueField = "code";
        ddlTestNames.DataBind();
        ddlTestNames.Items.Insert(0, "--- Select ---");
        ddlTestNames.Visible = true;

        grdProcedureCode.Visible = false;
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        } 
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string GetOpenCaseStatus()
    {
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
        catch { }
        return caseStatusID;
    }

    #region "Patient Search"

    protected void btnSearhPatientList_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            Session["Page_Index"] = "";
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            ModalPopupExtender.Show();
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdPatientList_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            lnkPatientDesk.Visible = true;
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[1].Text != "&nbsp;") { txtPatientID.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[1].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[2].Text != "&nbsp;") { txtPatientFName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[2].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[11].Text != "&nbsp;") { txtMI.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[11].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[3].Text != "&nbsp;") { txtPatientLName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[3].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[9].Text != "&nbsp;") { txtPatientPhone.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[9].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[5].Text != "&nbsp;") { txtPatientAddress.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[5].Text; }
            // if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text != "&nbsp;") { txtState.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text != "&nbsp;") { extddlPatientState.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[15].Text != "&nbsp;") { txtBirthdate.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[15].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[4].Text != "&nbsp;") { txtPatientAge.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[4].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[13].Text != "&nbsp;") { txtSocialSecurityNumber.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[13].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[31].Text != "&nbsp;") { if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[31].Text.ToString() == "True") { chkTransportation.Checked = true; } else { chkTransportation.Checked = false; } }
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
                    extddlDoctor.Flag_Key_Value = "newGETREFFERDOCTORLIST";
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
            DisplayControlForAddVisit();

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
            ModalPopupExtender.Show();
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void DisplayControlForAddVisit()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
  
        try
        {
            txtRefChartNumber.ReadOnly = true;
            txtPatientFName.ReadOnly = true;
            txtMI.ReadOnly = true;
            txtPatientLName.ReadOnly = true;
            txtPatientPhone.ReadOnly = true;
            txtPatientAddress.ReadOnly = true;
            TextBox3.ReadOnly = true;
            txtState.ReadOnly = true;
            txtBirthdate.ReadOnly = true;
            txtPatientAge.ReadOnly = true;
            txtSocialSecurityNumber.ReadOnly = true;

            // allow to modify
            extddlInsuranceCompany.Enabled = false;
            extddlCaseType.Enabled = false;
            extddlMedicalOffice.Enabled = true;
            extddlPatientState.Enabled = false;

            // Hide some controls from user

            lblSSN.Visible = false;
            txtSocialSecurityNumber.Visible = false;
            lblBirthdate.Visible = false;
            txtBirthdate.Visible = false;
            lblAge.Visible = false;
            txtPatientAge.Visible = false;
            lblChartNumber.Visible = false;
            txtRefChartNumber.Visible = false;

            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.Trim().ToLower().ToString() == "referring office")
                {
                    Bill_Sys_BillingCompanyDetails_BO objOffID = new Bill_Sys_BillingCompanyDetails_BO();
                    string sz_Off_Id = objOffID.GetRefDocID(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    if (sz_Off_Id != "")
                    {
                        extddlMedicalOffice.Text = sz_Off_Id;
                        extddlMedicalOffice.Enabled = false;

                        extddlDoctor.Procedure_Name = "SP_GET_REF_DOC";
                        extddlDoctor.Connection_Key = "Connection_String";
                        extddlDoctor.Selected_Text = "---Select---";
                        extddlDoctor.Flag_Key_Value = txtCompanyID.Text;
                        extddlDoctor.Flag_ID = sz_Off_Id;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SearchPatientList()
    {//Logging Start
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
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;") { txtPatientID.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); } else { txtPatientID.Text = ""; }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;") { txtPatientFName.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(); } else { txtPatientFName.Text = ""; }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;") { txtMI.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString(); } else { txtMI.Text = ""; }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;") { txtPatientLName.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(); } else { txtPatientLName.Text = ""; }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;") { txtPatientPhone.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString(); } else { txtPatientPhone.Text = ""; }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;") { txtPatientAddress.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(); } else { txtPatientAddress.Text = ""; }
                    //if (ds.Tables[0].Rows[0].ItemArray.GetValue(22).ToString() != "&nbsp;") { txtState.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(22).ToString(); }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString() != "&nbsp;") { txtBirthdate.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString(); } else { txtBirthdate.Text = ""; }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;") { txtPatientAge.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(); } else { txtPatientAge.Text = ""; }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() != "&nbsp;") { txtSocialSecurityNumber.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString(); } else { txtSocialSecurityNumber.Text = ""; }
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "&nbsp;") { if (ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() == "True") { chkTransportation.Checked = true; } else { chkTransportation.Checked = false; } }
                    if (ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString() != "&nbsp;") { extddlPatientState.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString(); } else { extddlPatientState.Text = "NA"; }
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
                        if (_ds.Tables[0].Rows[0]["SZ_OFFICE_ID"].ToString() != "")
                        {
                            extddlMedicalOffice.Text = _ds.Tables[0].Rows[0]["SZ_OFFICE_ID"].ToString();
                            extddlMedicalOffice.Enabled = false;

                            extddlDoctor.Flag_ID = _ds.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                            extddlDoctor.Flag_Key_Value = "newGETREFFERDOCTORLIST";
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
                    if (dt.Rows.Count > 0)
                    {
                        if (Session["Page_Index"].ToString() != "")
                        {
                            grdPatientList.CurrentPageIndex = Convert.ToInt32(Session["Page_Index"].ToString());
                            grdPatientList.DataSource = dt;
                            grdPatientList.DataBind();
                        }
                        else
                        {
                            grdPatientList.CurrentPageIndex = 0;
                            grdPatientList.DataSource = dt;
                            grdPatientList.DataBind();
                        }

                    }
                    else
                    {
                        grdPatientList.DataSource = null;
                        grdPatientList.DataBind();
                        Session["dtView"] = "";
                    }
                    ModalPopupExtender.Show();

                }
            }
            Bill_Sys_BillingCompanyDetails_BO objOffID2 = new Bill_Sys_BillingCompanyDetails_BO();
            string sz_Off_Id2 = objOffID2.GetRefDocID(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
            if (sz_Off_Id2 != "")
                btnAddPatient.Visible = false;
            else
                btnAddPatient.Visible = true;
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
            ModalPopupExtender.Show();
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdPatientList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            //Modified By BowandBaan for Include Ajax Extension - Modification Starts
            Session["Page_Index"] = e.NewPageIndex.ToString();
            //grdPatientList.CurrentPageIndex = e.NewPageIndex;
            //Modified By BowandBaan for Include Ajax Extension - Modification Ends
            SearchPatientList();
            ModalPopupExtender.Show();
        }
        catch { }
    }

    #endregion

    #region "Load patient details according to event id"

    private string GETAppointPatientDetail(int i_schedule_id)
    {//Logging Start
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

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0].Clone();

                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;") { txtPatientID.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;") { txtPatientFName.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;") { txtMI.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;") { txtPatientLName.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;") { txtPatientPhone.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;") { txtPatientAddress.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(); }
                        if (ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString() != "&nbsp;") { extddlPatientState.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString(); } else { extddlPatientState.Text = "NA"; }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString() != "&nbsp;") { txtBirthdate.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;") { txtPatientAge.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() != "&nbsp;") { txtSocialSecurityNumber.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "&nbsp;") { if (ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() == "True") { chkTransportation.Checked = true; } else { chkTransportation.Checked = false; } }

                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(34).ToString() != "&nbsp;") { txtCaseID.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(34).ToString(); }
                        extddlInsuranceCompany.Flag_ID = ds.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                        extddlCaseType.Flag_ID = ds.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();

                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(33).ToString() != "&nbsp;") { extddlCaseType.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(33).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(32).ToString() != "&nbsp;") { extddlInsuranceCompany.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(32).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(35).ToString() != "&nbsp;") { extddlDoctor.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(35).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(36).ToString() != "&nbsp;") { ddlType.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(36).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(37).ToString() != "&nbsp;")
                        {
                            string startTime = ds.Tables[0].Rows[0].ItemArray.GetValue(37).ToString();
                            ddlHours.SelectedValue = startTime.Substring(0, startTime.IndexOf(".")).PadLeft(2, '0');
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

                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(42).ToString() != "&nbsp;") { txtNotes.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(42).ToString(); }
                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(44).ToString() != "&nbsp;") { txtPatientCompany.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(44).ToString(); }

                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(43).ToString() != "&nbsp;")
                        {
                            if (ds.Tables[0].Rows[0].ItemArray.GetValue(43).ToString() == "True")
                            {
                                extddlDoctor.Enabled = false;
                                ddlType.Enabled = false;
                                txtNotes.ReadOnly = true;
                            }
                            else
                            {
                            }
                        }

                        if (ds.Tables[0].Rows[0].ItemArray.GetValue(45).ToString() != "") { extddlMedicalOffice.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(45).ToString(); /*extddlMedicalOffice.Enabled = false;*/ }

                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        {
                            extddlDoctor.Flag_ID = extddlMedicalOffice.Text;
                            extddlDoctor.Flag_Key_Value = "newGETREFFERDOCTORLIST";
                            if (ds.Tables[0].Rows[0].ItemArray.GetValue(41).ToString() != "&nbsp;") { extddlDoctor.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(41).ToString(); }
                            if (ds.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString() != "&nbsp;") { txtRefChartNumber.Text = ds.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString(); }
                        }
                        else
                        {
                            if (ds.Tables[0].Rows[0].ItemArray.GetValue(51).ToString() != "&nbsp;") { extddlDoctor.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(51).ToString(); }
                            txtRefChartNumber.Text = "";
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
                                //extddlTransport.Style.Add("Visibility","hidden");
                                extddlTransport.Visible = false;
                            }
                        }
                        else
                        {
                            extddlTransport.Text = "NA";
                        }
                        ds = new DataSet();
                        _patient_TVBO = new Patient_TVBO();
                        ds = _patient_TVBO.GetAppointProcCode(i_schedule_id);
                        // new logic
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            foreach (DataGridItem lst in grdProcedureCode.Items)
                            {
                                if (lst.Cells[1].Text == dr.ItemArray.GetValue(0).ToString())
                                {
                                    CheckBox chkSelect = (CheckBox)lst.Cells[1].FindControl("chkSelect");
                                    chkSelect.Checked = true;
                                    lst.BackColor = System.Drawing.Color.LightSeaGreen;
                                }
                            }
                        }
                        return "success";
                    }
                    else
                    {
                        usrMessage.PutMessage("Could not load appointment data. Possible reasons - Case type is not added to the case.");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                        usrMessage.Show();
                        return "missing_case_type";
                    }
                }
                else
                {
                    usrMessage.PutMessage("Could not load appointment data. Possible reasons - Case type is not added to the case.");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    usrMessage.Show();
                    return "missing_case_type";
                }
            }
            else
            {
                usrMessage.PutMessage("Could not load appointment data. Possible reasons - Case type is not added to the case.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
                return "missing_case_type";
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
        return null;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

    protected void extddlMedicalOffice_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            extddlDoctor.Flag_ID = extddlMedicalOffice.Text;
            extddlDoctor.Flag_Key_Value = "newGETREFFERDOCTORLIST";
            ModalPopupExtender.Show();
        }
        catch
        {
        }
    }

    #region "Button click on Patient Appointment Page."


    /* Purpose : Display controls according to operation i.e. ADD APPOINTMENT / UPDATE APPOINTMENT
     * 
     */


    protected void DisplayProcedureGridColumns(bool value)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            grdProcedureCode.Columns[3].Visible = value;
            grdProcedureCode.Columns[4].Visible = value;
            grdProcedureCode.Columns[5].Visible = value;
            grdProcedureCode.Columns[6].Visible = value;
            grdProcedureCode.Columns[9].Visible = value;
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

    protected void setControlAccordingOperation()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (hdnOperationType.Value.ToString() == "TestFacilityUpdate") // When user want to update event from Test facility company login.
            {
                DisplayProcedureGridColumns(true);
                extddlMedicalOffice.Visible = true;
                extddlReferenceFacility.Visible = false;
                lblTestFacility.Text = "Office Name";
                btnUpdate.Visible = true;
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "1") || (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "True"))
                {
                    btnDeleteEvent.Visible = true;
                }
                else
                {
                    btnDeleteEvent.Visible = false;
                }

                btnSave.Visible = false;
                btnDuplicateSaveClick.Visible = false;
                grdProcedureCode.Visible = true;
                ddlTestNames.Visible = false;
                divProcedureCode.Visible = true;
                divProcedureCode.Style.Add("HEIGHT", "250px");
                divProcedureCode.Style.Add("OVERFLOW", "scroll");
                divProcedureCode.Style.Add("WIDTH", "100%");


                lblSSN.Visible = true;
                txtSocialSecurityNumber.Visible = true;
                lblBirthdate.Visible = true;
                txtBirthdate.Visible = true;
                lblAge.Visible = true;
                txtPatientAge.Visible = true;
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "0"))
                {
                    lblChartNumber.Visible = true;
                    txtRefChartNumber.Visible = true;
                }

                // Set Read Only

                txtRefChartNumber.ReadOnly = true;
                txtPatientFName.ReadOnly = true;
                txtMI.ReadOnly = true;
                txtPatientLName.ReadOnly = true;
                txtPatientPhone.ReadOnly = true;
                txtPatientAddress.ReadOnly = true;
                TextBox3.ReadOnly = true;
                txtState.ReadOnly = true;
                txtBirthdate.ReadOnly = true;
                txtPatientAge.ReadOnly = true;
                txtSocialSecurityNumber.ReadOnly = true;
                extddlInsuranceCompany.Enabled = false;
                extddlCaseType.Enabled = false;
                extddlMedicalOffice.Enabled = true;
                extddlPatientState.Enabled = false;
                btnUpdate.Attributes.Add("Onclick", "return val_updateTestFacility();");
                //

                if (Session["BILLED_EVENT"].ToString() == "BILLED")
                {
                    lblMsg.Text = "Bill generated for selected appointment.";
                    btnUpdate.Visible = false;
                    btnDeleteEvent.Visible = false;
                    Session["BILLED_EVENT"] = "";
                }
            }
            else if (hdnOperationType.Value.ToString() == "BillngCompanyUpdate") // When user want to update event from billing company login.
            {
                DisplayProcedureGridColumns(false);
                btnSave.Visible = false;
                btnDuplicateSaveClick.Visible = false;
                btnUpdate.Visible = true;

                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "1") || (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "True"))
                {
                    btnDeleteEvent.Visible = true;
                }
                else
                {
                    btnDeleteEvent.Visible = false;
                }
                lblSSN.Visible = false;
                txtSocialSecurityNumber.Visible = false;
                lblBirthdate.Visible = false;
                txtBirthdate.Visible = false;
                lblAge.Visible = false;
                txtPatientAge.Visible = false;
                lblChartNumber.Visible = false;
                txtRefChartNumber.Visible = false;
                extddlPatientState.Enabled = false;
                btnUpdate.Attributes.Add("Onclick", "return Val_AddVisitForTestFacility();");

                if (Session["VISIT_COMPLETED"] != null)
                {
                    if (Session["VISIT_COMPLETED"].ToString() == "YES")
                    {
                        btnUpdate.Visible = false;
                        btnDeleteEvent.Visible = false;
                        Session["VISIT_COMPLETED"] = "";
                    }
                }
            }
            else
            {
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    btnDuplicateSaveClick.Attributes.Add("Onclick", "return Val_AddVisitForTestFacility();");
                    extddlMedicalOffice.Visible = true;

                    extddlMedicalOffice.Enabled = true;
                    extddlReferenceFacility.Visible = false;
                    lblTypetext.Visible = false;
                    ddlType.Visible = false;
                    lblTestFacility.Text = "Office Name";

                    if (extddlMedicalOffice.Text != "NA" || extddlMedicalOffice.Text != "")
                    {
                        // if has value then no need to assign to NA
                    }
                    else
                        extddlMedicalOffice.Text = "NA";
                    extddlDoctor.Text = "NA";
                    extddlTransport.Text = "NA";
                    extddlTransport.Visible = false;
                    chkTransportation.Checked = false;

                    if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "0"))
                    {
                        lblChartNumber.Visible = true;
                        txtRefChartNumber.Visible = true;
                    }
                }
                else
                {
                    btnDuplicateSaveClick.Attributes.Add("Onclick", "return Val_Add_UpdateVisitForBillingCompany();");
                    extddlMedicalOffice.Visible = false;
                    extddlReferenceFacility.Visible = true;
                    lblTestFacility.Text = "Test Facility";
                    lblChartNumber.Visible = false;
                    txtRefChartNumber.Visible = false;
                }
                DisplayProcedureGridColumns(false);
                lblSSN.Visible = false;
                txtSocialSecurityNumber.Visible = false;
                lblBirthdate.Visible = false;
                txtBirthdate.Visible = false;
                lblAge.Visible = false;
                txtPatientAge.Visible = false;
                txtNotes.Text = "";

                btnUpdate.Visible = false;
                btnDeleteEvent.Visible = false;
                btnSave.Visible = true;
                btnDuplicateSaveClick.Visible = true;
                //ddlTestNames.Visible = true;
                //divProcedureCode.Visible = false;
                //divProcedureCode.Style.Add("HEIGHT", "0px");
                //divProcedureCode.Style.Add("OVERFLOW", "hidden");
                //divProcedureCode.Style.Add("WIDTH", "0%");


                // Set Read Only

                txtRefChartNumber.ReadOnly = true;
                txtPatientFName.ReadOnly = true;
                txtMI.ReadOnly = true;
                txtPatientLName.ReadOnly = true;
                txtPatientPhone.ReadOnly = true;
                txtPatientAddress.ReadOnly = true;
                TextBox3.ReadOnly = true;
                txtState.ReadOnly = true;
                txtBirthdate.ReadOnly = true;
                txtPatientAge.ReadOnly = true;
                txtSocialSecurityNumber.ReadOnly = true;
                extddlInsuranceCompany.Enabled = false;
                extddlCaseType.Enabled = false;
                extddlPatientState.Enabled = false;

                if (txtPatientFName.Text == "")
                {
                    btnClickSearch.Visible = true;
                    tdSerach.Visible = true;
                }
                //
            }

            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                txtRefChartNumber.Visible = false;
                lblChartNumber.Visible = false;
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string sFlag = "default";

        try
        {
            if (hdnObj.Value != "")
            {
                //ModalPopupExtender.Show();
                Bill_Sys_BillingCompanyDetails_BO objOffID1 = new Bill_Sys_BillingCompanyDetails_BO();
                string sz_Off_Id1 = objOffID1.GetRefDocID(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                if (sz_Off_Id1 != "")
                    btnAddPatient.Visible = false;
                else
                    btnAddPatient.Visible = true;

                PopupBO _popupBO = new PopupBO();
                if (Session["CASE_OBJECT"] != null)
                {
                    ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID = _popupBO.GetCompanyID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                }
                else
                {
                    Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                    _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                }

                extddlDoctor.Flag_ID = txtCompanyID.Text.ToString();
                extddlInsuranceCompany.Flag_ID = txtCompanyID.Text.ToString();
                extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
                extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
                extddlMedicalOffice.Flag_ID = txtCompanyID.Text.ToString();
                extddlReferenceFacility.Flag_ID = txtCompanyID.Text.ToString();

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    extddlTransport.Flag_ID = txtCompanyID.Text;
                }
                else
                {
                    extddlTransport.Flag_ID = extddlReferenceFacility.Text;
                }
                BindTimeControl();
                string sz_datetime = hdnObj.Value;
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

                ddlEndHours.SelectedValue = endHr.ToString().PadLeft(2, '0');
                ddlEndMinutes.SelectedValue = endMin.ToString().PadLeft(2, '0');
                ddlEndTime.SelectedValue = endTime.ToString();

                if (Session["CASE_OBJECT"] != null)
                {
                    _popupBO = new PopupBO();
                    strcompanyid = _popupBO.GetCompanyID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID);

                }
                if (Session["Flag"] != null)
                {
                    //_patient_TVBO = new Patient_TVBO();
                    //Session["PatientDataList"] = _patient_TVBO.GetSelectedPatientDataListNEW(strcompanyid, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID, UserID);
                    Session["PatientDataList"] = FPG.GetSelectedPatientDataListNEW(strcompanyid, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID, UserID);
                    SearchPatientList();
                    btnClickSearch.Visible = false;
                }
                else if (Session["DataEntryFlag"] != null)
                {
                    //_patient_TVBO = new Patient_TVBO();
                    //Session["PatientDataList"] = _patient_TVBO.GetSelectedPatientDataListNEW(strcompanyid, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID, UserID);
                    Session["PatientDataList"] = FPG.GetSelectedPatientDataListNEW(strcompanyid, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID, UserID);
                    SearchPatientList();
                    btnClickSearch.Visible = false;
                    Session["DataEntryFlag"] = null;
                }
                else // When user click on Calendar link on the top the page.
                {
                    //_patient_TVBO = new Patient_TVBO();
                    //Session["PatientDataList"] = _patient_TVBO.GetPatientDataListNEW(txtCompanyID.Text, UserID);
                    Session["PatientDataList"] = FPG.GetPatientDataListNEW(txtCompanyID.Text, UserID);

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
                    extddlPatientState.Text = "NA";
                    extddlMedicalOffice.Text = "NA";
                    btnClickSearch.Visible = true;
                }

                if (hdnOperationType.Value.ToString() == "TestFacilityUpdate")
                {
                    LoadProcedureGrid();
                }
                else
                {
                    //LoadTypes();
                    LoadProcedureGrid();
                }
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    extddlMedicalOffice.Visible = true;
                    extddlReferenceFacility.Visible = false;
                    lblTypetext.Visible = false;
                    ddlType.Visible = false;
                    lblTestFacility.Text = "Office Name";
                }

                extddlCaseStatus.Text = GetOpenCaseStatus();

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
                    _patient_TVBO = new Patient_TVBO();
                    if (_patient_TVBO.getScheduleStatus(scheduledID))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Visit Completed.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        Session["VISIT_COMPLETED"] = "YES";
                    }
                    Session["SCHEDULEDID"] = scheduledID;
                    if (hdnOperationType.Value.ToString() == "TestFacilityUpdate")
                    {
                        ddlTestNames.Visible = false;
                        divProcedureCode.Visible = true;
                        SelectSavedProcedureCodes(scheduledID);
                    }
                    else
                    {
                        //ddlTestNames.Visible = true;
                        //divProcedureCode.Visible = false;
                        //LoadTypes();
                        LoadProcedureGrid();
                    }
                    sFlag = GETAppointPatientDetail(Convert.ToInt32(scheduledID));
                    btnClickSearch.Visible = false;
                    tdSerach.Visible = false;
                    tdSerach.Height = "0px";
                }
                setControlAccordingOperation();
                if (txtPatientFName.Text == "")
                {
                    lnkPatientDesk.Visible = false;
                }
                else
                {
                    lnkPatientDesk.Visible = true;
                }
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.Trim().ToLower().ToString() == "referring office")
                    {
                        Bill_Sys_BillingCompanyDetails_BO objOffID = new Bill_Sys_BillingCompanyDetails_BO();
                        string sz_Off_Id = objOffID.GetRefDocID(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                        if (sz_Off_Id != "")
                        {
                            extddlMedicalOffice.Text = sz_Off_Id;
                            extddlMedicalOffice.Enabled = false;

                            extddlDoctor.Procedure_Name = "SP_GET_REF_DOC";
                            extddlDoctor.Connection_Key = "Connection_String";
                            extddlDoctor.Selected_Text = "---Select---";
                            extddlDoctor.Flag_Key_Value = txtCompanyID.Text;
                            extddlDoctor.Flag_ID = sz_Off_Id;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            if (sFlag.ToLower().Equals("default") || sFlag.ToLower().Equals("success"))
            {
                ModalPopupExtender.Show();
            }
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
  
        Bill_Sys_Calender _bill_Sys_Calender;
        try
        {
            if (hdnEventId.Value != "" && hdnEventDeleteDate.Value != "")
            {
                DateTime dtDate = Convert.ToDateTime(System.DateTime.Now.ToString());
                string[] strDate = hdnEventDeleteDate.Value.ToString().Split('|');
                string[] strT = hdnEventDeleteDate.Value.ToString().Split('-');

                string[] strS = strDate[1].Split(' ');

                DateTime dtEventDate = Convert.ToDateTime(strDate[0].ToString() + " " + strS[0].Replace(".", ":") + ":00" + strT[0].Substring((strT[0].Length - 2), 2).ToString());

                _bill_Sys_Calender = new Bill_Sys_Calender();
                int iCount = _bill_Sys_Calender.Delete_Event(Convert.ToInt32(hdnEventId.Value.ToString()), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                DateTime dtCurrentDate = Convert.ToDateTime(lblCurrentDate.Text.Trim().ToString());
                string[] strSelectedDate = dtCurrentDate.ToString().Split('/');
                string strdt = strSelectedDate[0] + "/" + strSelectedDate[1] + "/" + strSelectedDate[2].Substring(0, 4).ToString();

                if (iCount > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(btnDelete, typeof(Button), "Msg", "alert('Appointment Deleted Successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(btnDelete, typeof(Button), "Msg", "alert('Can not delete appointment.')", true);
                }
                if (strdt != "")
                {
                    if (extddlReferringFacility.Visible == false)
                    {
                        GetCalenderDayAppointments(Convert.ToDateTime(strdt).ToString("MM/dd/yyyy"), txtCompanyID.Text);
                    }
                    else
                    {
                        GetCalenderDayAppointments(Convert.ToDateTime(strdt).ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            _bill_Sys_Calender = null;
            hdnEventId.Value = "";
            hdnEventDeleteDate.Value = "";
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    protected void btnDeleteEvent_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_Calender _bill_Sys_Calender;
        try
        {
            string sz_datetime = hdnObj.Value;
            string scheduledID = "";
            if (sz_datetime.IndexOf("^") > 0)
            {
                scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, ((sz_datetime.IndexOf("^")) - (sz_datetime.IndexOf("~") + 1)));
            }
            else
            {
                scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, (sz_datetime.Length - (sz_datetime.IndexOf("~") + 1)));
            }

            hdnEventId.Value = scheduledID;
            hdnEventDeleteDate.Value = sz_datetime.Substring(0, sz_datetime.IndexOf(" "));
            if (hdnEventId.Value != "" && hdnEventDeleteDate.Value != "")
            {
                _bill_Sys_Calender = new Bill_Sys_Calender();
                int iCount = _bill_Sys_Calender.Delete_Event(Convert.ToInt32(hdnEventId.Value.ToString()), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID); // event id , company id
                String strDeleteDate = hdnEventDeleteDate.Value;
                if (iCount > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(btnDelete, typeof(Button), "Msg", "alert('Appointment Deleted Successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(btnDelete, typeof(Button), "Msg", "alert('Can not delete appointment.')", true);
                }
                if (strDeleteDate != "")
                {
                    if (extddlReferringFacility.Visible == false)
                    {
                        GetCalenderDayAppointments(Convert.ToDateTime(strDeleteDate).ToString("MM/dd/yyyy"), txtCompanyID.Text);
                    }
                    else
                    {
                        GetCalenderDayAppointments(Convert.ToDateTime(strDeleteDate).ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            _bill_Sys_Calender = null;
            hdnEventId.Value = "";
            hdnEventDeleteDate.Value = "";
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearValues();
    }

    #endregion

    #region "Add Appointment - LOGIC(Save) - OLD LOGIC"

    //protected void btnSave_Click_OLD(object sender, EventArgs e)
    //{
    //    Billing_Sys_ManageNotesBO manageNotes;
    //    manageNotes = new Billing_Sys_ManageNotesBO();
    //    try
    //    {
    //        #region "Validation"
    //        if (txtPatientFName.Text.Trim().ToString() == "")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Patient First Name should not be Empty...!!')", true);
    //            ModalPopupExtender.Show();
    //            return;
    //        }
    //        if (txtPatientLName.Text.Trim().ToString() == "")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Patient Last name should not be Empty...!!')", true);
    //            ModalPopupExtender.Show();
    //            return;
    //        }
    //        if (extddlReferenceFacility.Text == "NA")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Please Select any one Reference Facility...!!')", true);
    //            ModalPopupExtender.Show();
    //            return;
    //        }
    //        if (extddlDoctor.Text == "NA")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Please Select any one Referring Doctor...!!')", true);
    //            ModalPopupExtender.Show();
    //            return;
    //        }
    //        if (ddlTestNames.SelectedIndex == -1)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Please Select any one Procedure Code...!!')", true);
    //            ModalPopupExtender.Show();
    //            return;
    //        }
    //        #endregion

    //        if (txtPatientFName.ReadOnly == false)
    //        {
    //            SavePatient();
    //            txtPatientID.Text = manageNotes.GetPatientLatestID();
    //        }
    //        if (hdnObj.Value.ToString() != "")
    //        {
    //            string[] szArr;
    //            szArr = hdnObj.Value.ToString().Split(' ');
    //            DateTime dtPassDate = new DateTime();
    //            dtPassDate = Convert.ToDateTime(szArr[0]);

    //            if (txtPatientID.Text != "")
    //            {
    //            }
    //            else
    //            {
    //                // Start : No use ------------------------
    //                Bill_Sys_ReferalEvent _ReferalEvent = new Bill_Sys_ReferalEvent();
    //                ArrayList objArra = new ArrayList();
    //                objArra.Add(txtPatientFName.Text);
    //                objArra.Add(txtMI.Text);
    //                objArra.Add(txtPatientLName.Text);
    //                objArra.Add(txtPatientAge.Text);
    //                objArra.Add(txtPatientAddress.Text);
    //                objArra.Add(txtPatientPhone.Text);
    //                objArra.Add(txtBirthdate.Text);
    //                objArra.Add(txtState.Text);
    //                objArra.Add(txtSocialSecurityNumber.Text);
    //                objArra.Add(txtCompanyID.Text);
    //                _ReferalEvent.AddPatient(objArra);
    //                txtPatientID.Text = manageNotes.GetPatientLatestID();

    //                _ReferalEvent = new Bill_Sys_ReferalEvent();
    //                objArra = new ArrayList();
    //                objArra.Add(extddlInsuranceCompany.Text);
    //                objArra.Add(txtPatientID.Text);
    //                objArra.Add(txtCompanyID.Text);
    //                objArra.Add(extddlCaseStatus.Text);
    //                objArra.Add(extddlCaseType.Text);
    //                _ReferalEvent.AddPatientCase(objArra);
    //                // End : No use ------------------------
    //            }

    //            int eventID = 0;
    //            if (hdnObj.Value.ToString() != "")
    //            {
    //                string szDoctorID = "";
    //                String sz_datetime = hdnObj.Value.ToString();

    //                if (sz_datetime.IndexOf("~") > 0)
    //                {
    //                    string scheduledID = "";
    //                    if (sz_datetime.IndexOf("^") > 0)
    //                    {
    //                        scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, ((sz_datetime.IndexOf("^")) - (sz_datetime.IndexOf("~") + 1)));
    //                    }
    //                    else
    //                    {
    //                        scheduledID = sz_datetime.Substring(sz_datetime.IndexOf("~") + 1, (sz_datetime.Length - (sz_datetime.IndexOf("~") + 1)));
    //                    }
    //                    Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
    //                    ArrayList arrOBJ = new ArrayList();

    //                    arrOBJ.Add(extddlDoctor.Text);
    //                    arrOBJ.Add(txtPatientID.Text);
    //                    arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                    szDoctorID = _bill_Sys_ReferalEvent.AddDoctor(arrOBJ);

    //                    foreach (ListItem lst in ddlTestNames.Items)
    //                    {
    //                        if (lst.Selected == true)
    //                        {
    //                            arrOBJ = new ArrayList();
    //                            arrOBJ.Add(szDoctorID);
    //                            arrOBJ.Add(lst.Value);
    //                            arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                            arrOBJ.Add(lst.Value);
    //                            _bill_Sys_ReferalEvent.AddDoctorAmount(arrOBJ);

    //                            arrOBJ = new ArrayList();
    //                            arrOBJ.Add(txtPatientID.Text);
    //                            arrOBJ.Add(szDoctorID);
    //                            arrOBJ.Add(hdnObj.Value.ToString().Substring(0, hdnObj.Value.ToString().IndexOf(" ")));
    //                            arrOBJ.Add(lst.Value);
    //                            arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                            arrOBJ.Add(ddlType.SelectedValue);
    //                            _bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);
    //                        }
    //                    }
    //                }
    //                else if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
    //                {

    //                    Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
    //                    ArrayList arrOBJ = new ArrayList();
    //                    szDoctorID = extddlDoctor.Text;
    //                    foreach (ListItem lst in ddlTestNames.Items)
    //                    {
    //                        if (lst.Selected == true)
    //                        {
    //                            arrOBJ = new ArrayList();
    //                            arrOBJ.Add(szDoctorID);
    //                            arrOBJ.Add(lst.Value);
    //                            arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                            arrOBJ.Add(lst.Value);
    //                            _bill_Sys_ReferalEvent.AddDoctorAmount(arrOBJ);
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    szDoctorID = extddlDoctor.Text;
    //                }

    //                if (chkTransportation.Checked == true && extddlTransport.Text == "NA")
    //                {
    //                    Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select transport company !');</script>");
    //                    return;
    //                }
    //                string sz_date = hdnObj.Value.ToString();
    //                sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));
    //                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
    //                ArrayList objAdd = new ArrayList();
    //                objAdd.Add(txtPatientID.Text);  // SZ_CASE_ID
    //                objAdd.Add(sz_date); // DT_EVENT_DATE
    //                objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString()); // DT_EVENT_TIME
    //                objAdd.Add(txtNotes.Text); // SZ_EVENT_NOTES
    //                objAdd.Add(szDoctorID); // SZ_DOCTOR_ID
    //                if (ddlTestNames.Items.Count > 1) { objAdd.Add(ddlTestNames.Items[1].Value); } else objAdd.Add(""); // SZ_TYPE_CODE_ID
    //                objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID); // SZ_COMPANY_ID
    //                objAdd.Add(ddlTime.SelectedValue); // DT_EVENT_TIME_TYPE
    //                objAdd.Add(ddlEndHours.SelectedValue.ToString() + "." + ddlEndMinutes.SelectedValue.ToString()); // DT_EVENT_END_TIME
    //                objAdd.Add(ddlEndTime.SelectedValue); // DT_EVENT_END_TIME_TYPE
    //                objAdd.Add(extddlReferenceFacility.Text); // SZ_REFERENCE_ID
    //                objAdd.Add("False"); // BT_STATUS
    //                // #149 12 April,2010 change the BT_TRANSPOTATION field patient table to txn_calender_event -- sachin
    //                if (chkTransportation.Checked == true) { objAdd.Add(1); } else { objAdd.Add(0); } // BT_TRANSPORTATION
    //                //==================================
    //                if (chkTransportation.Checked == true) { objAdd.Add(Convert.ToInt32(extddlTransport.Text)); } else { objAdd.Add(null); } //I_TRANSPORTATION_COMPANY

    //                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true) //SZ_OFFICE_ID
    //                { objAdd.Add(extddlMedicalOffice.Text); }


    //                eventID = _bill_Sys_Calender.Save_Event(objAdd);

    //                foreach (ListItem lst in ddlTestNames.Items)
    //                {
    //                    if (lst.Selected == true)
    //                    {
    //                        objAdd = new ArrayList();
    //                        objAdd.Add(lst.Value);
    //                        objAdd.Add(eventID);
    //                        objAdd.Add(0);
    //                        _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
    //                    }
    //                }

    //                // Start : Save appointment Notes.

    //                _DAO_NOTES_EO = new DAO_NOTES_EO();
    //                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
    //                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + sz_datetime.Substring(0, sz_datetime.IndexOf("!")); //"Date : " + sz_datetime;


    //                _DAO_NOTES_BO = new DAO_NOTES_BO();
    //                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
    //                _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
    //                _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
    //                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


    //                // Document Manager Node : Start
    //                if (flUpload.FileName != "")
    //                {
    //                    Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();

    //                    String szDefaultPath = objNF3Template.getPhysicalPath();
    //                    String szDestinationDir = "";

    //                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
    //                    {
    //                        szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

    //                    }
    //                    else
    //                    {
    //                        szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
    //                    }
    //                    szDestinationDir = szDestinationDir + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/No Fault File/Referral Sheet-INI Report/";
    //                    if (!Directory.Exists(szDefaultPath + szDestinationDir))
    //                    {
    //                        Directory.CreateDirectory(szDefaultPath + szDestinationDir);
    //                    }
    //                    if (!File.Exists(szDefaultPath + szDestinationDir + flUpload.FileName))
    //                    {
    //                        flUpload.SaveAs(szDefaultPath + szDestinationDir + flUpload.FileName);

    //                        ArrayList objAL = new ArrayList();
    //                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
    //                        {
    //                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                        }
    //                        else
    //                        {
    //                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
    //                        }

    //                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
    //                        objAL.Add(flUpload.FileName);
    //                        objAL.Add(szDestinationDir);
    //                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
    //                        objAL.Add("Referral Sheet-INI Report");
    //                        objNF3Template.saveOutScheduleReportInDocumentManager(objAL);

    //                    }
    //                }
    //                // Document Manager Node : End
    //            }
    //            if (hdnObj.Value.ToString().IndexOf("~") > 0)
    //            {
    //                Session["PopUp"] = null;
    //            }
    //            else
    //            {
    //                Session["PopUp"] = "True";
    //            }


    //        }
    //       // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_AppointPatientEntry.aspx';</script>");
    //        //Modified By BowandBaan for Include Ajax Extension - Modification Starts
    //        string[] strSelectedDate = hdnObj.Value.ToString().Split('/');
    //        string strDate = strSelectedDate[0] + "/" + strSelectedDate[1] + "/" + strSelectedDate[2].Substring(0, 4).ToString();
    //        if (strDate != "")
    //        {
    //            if (extddlReferringFacility.Visible == false)
    //            {
    //                GetCalenderDayAppointments(Convert.ToDateTime(strDate).ToString("MM/dd/yyyy"), txtCompanyID.Text);
    //            }
    //            else
    //            {
    //                GetCalenderDayAppointments(Convert.ToDateTime(strDate).ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
    //            }


    //        }
    //        ClearValues();
    //        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Appointment details saved successfully...!')", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("~/Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    protected void chkTransportation_CheckedChanged(object sender, EventArgs e)
    {//Logging Start
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
            ModalPopupExtender.Show();
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

    #endregion

    #region "Add Patient for referring facility account only"

    protected void btnAddPatient_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {

            // Clear control
            txtPatientID.Text = "";
            txtPatientFName.Text = "";
            txtMI.Text = "";
            txtPatientLName.Text = "";
            txtPatientPhone.Text = "";
            txtPatientAddress.Text = "";
            txtBirthdate.Text = "";
            txtPatientAge.Text = "";
            txtSocialSecurityNumber.Text = "";
            txtCaseID.Text = "";
            extddlPatientState.Text = "NA";
            extddlCaseType.Text = "NA";
            extddlInsuranceCompany.Text = "NA";
            TextBox3.Text = "";
            //

            // Read Only

            txtPatientFName.ReadOnly = false;
            txtMI.ReadOnly = false;
            txtPatientLName.ReadOnly = false;
            txtPatientPhone.ReadOnly = false;
            txtPatientAddress.ReadOnly = false;
            extddlPatientState.Enabled = true;
            extddlInsuranceCompany.Enabled = true;
            extddlCaseType.Enabled = true;
            extddlMedicalOffice.Enabled = true;
            txtCaseID.Text = "";
            TextBox3.ReadOnly = false;


            //
            // Visible false
            lblSSN.Visible = false;
            txtSocialSecurityNumber.Visible = false;
            lblBirthdate.Visible = false;
            txtBirthdate.Visible = false;
            lblAge.Visible = false;
            txtPatientAge.Visible = false;


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
            ModalPopupExtender.Show();
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

    public void setReadOnly(bool value)
    {//Logging Start
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
            TextBox3.ReadOnly = value;
            txtState.ReadOnly = value;
            txtBirthdate.ReadOnly = value;
            txtPatientAge.ReadOnly = value;
            txtSocialSecurityNumber.ReadOnly = value;
            extddlInsuranceCompany.Enabled = !value;
            extddlCaseType.Enabled = !value;
            extddlMedicalOffice.Enabled = !value;
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

    public void ClearControlForAddPatient()
    {//Logging Start
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
            TextBox3.Text = "";
            txtState.Text = "";
            txtBirthdate.Text = "";
            txtPatientAge.Text = "";
            txtSocialSecurityNumber.Text = "";
            extddlInsuranceCompany.Text = "NA";
            extddlCaseType.Text = "NA";
            extddlMedicalOffice.Text = "NA";
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

    public void SavePatient()
    {//Logging Start
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
            objAL.Add(TextBox3.Text);   // Patient City
            objAL.Add(txtPatientPhone.Text);   // Patient Phone
            objAL.Add(txtState.Text);   // Patient State
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    #endregion

    #region "Update appointment for Test Facility (Display procedure grid)"

    private void LoadProcedureGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ArrayList objArr = new ArrayList();
            objArr.Add(extddlReferenceFacility.Text);
            string sz_datetime = hdnObj.Value;
            objArr.Add(sz_datetime.Substring(sz_datetime.IndexOf("!") + 1, 20));
            Bill_Sys_ManageVisitsTreatmentsTests_BO objBO = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
            grdProcedureCode.DataSource = objBO.GetReferringProcCodeList(objArr);
            grdProcedureCode.DataBind();
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

            //Method End

            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
    }


    protected void SelectSavedProcedureCodes(String i_schedule_id)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            // Start : Select procedure codes from grid which was selected by user.
            Session["BILLED_EVENT"] = "";
            Session["VISIT_COMPLETED"] = "";
            ds = new DataSet();
            _patient_TVBO = new Patient_TVBO();
            ds = _patient_TVBO.GetAppointProcCode(Convert.ToInt32(i_schedule_id));
            DataSet GridDs = new DataSet();

            GridDs = (DataSet)grdProcedureCode.DataSource;
            DataSet NewDs = new DataSet();
            DataTable newdt = new DataTable();
            NewDs.Tables.Add(newdt);
            NewDs.Tables[0].Columns.Add("CODE");
            NewDs.Tables[0].Columns.Add("DESCRIPTION");
            NewDs.Tables[0].Columns.Add("I_RESCHEDULE_ID");
            NewDs.Tables[0].Columns.Add("I_EVENT_PROC_ID");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                for (int i = 0; i < GridDs.Tables[0].Rows.Count; i++)
                {
                    if (GridDs.Tables[0].Rows[i][0].ToString().Equals(dr.ItemArray.GetValue(0).ToString()))
                    {
                        DataRow row = NewDs.Tables[0].NewRow();
                        row["CODE"] = GridDs.Tables[0].Rows[i][0].ToString();
                        row["DESCRIPTION"] = GridDs.Tables[0].Rows[i][1].ToString();
                        row["I_RESCHEDULE_ID"] = GridDs.Tables[0].Rows[i][2].ToString();
                        row["I_EVENT_PROC_ID"] = GridDs.Tables[0].Rows[i][3].ToString();
                        NewDs.Tables[0].Rows.Add(row);
                        GridDs.Tables[0].Rows.RemoveAt(i);
                        i--;
                        // GridDs.Tables[0].Rows.InsertAt(row, 0);
                    }
                }
            }
            NewDs.Tables[0].Merge(GridDs.Tables[0]);
            grdProcedureCode.DataSource = NewDs.Tables[0];
            grdProcedureCode.DataBind();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataGridItem lst in grdProcedureCode.Items)
                {
                    if (lst.Cells[1].Text == dr.ItemArray.GetValue(0).ToString())
                    {

                        CheckBox chkSelect = (CheckBox)lst.Cells[1].FindControl("chkSelect");
                        chkSelect.Checked = true;
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
                            Session["VISIT_COMPLETED"] = "YES";
                            //txtEventStatus.Text = "2";
                        }



                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                        {
                            if (Convert.ToInt32(dr.ItemArray.GetValue(1).ToString()) != 0)
                            {
                                TextBox txtDate = (TextBox)lst.Cells[5].FindControl("txtReScheduleDate");
                                txtDate.Text = dr.ItemArray.GetValue(2).ToString();
                                txtDate.ReadOnly = true;
                                string startTime = dr.ItemArray.GetValue(3).ToString();
                                DropDownList ddlHR = (DropDownList)lst.Cells[6].FindControl("ddlReSchHours");
                                ddlHR.SelectedValue = startTime.Substring(0, startTime.IndexOf(".")).PadLeft(2, '0');
                                ddlHR.Enabled = false;
                                DropDownList ddlMM = (DropDownList)lst.Cells[6].FindControl("ddlReSchMinutes");
                                ddlMM.SelectedValue = startTime.Substring(startTime.IndexOf(".") + 1, startTime.Length - (startTime.IndexOf(".") + 1)).PadLeft(2, '0');
                                ddlMM.Enabled = false;
                                DropDownList ddlTIME = (DropDownList)lst.Cells[6].FindControl("ddlReSchTime");
                                ddlTIME.SelectedValue = dr.ItemArray.GetValue(4).ToString();
                                ddlTIME.Enabled = false;
                                ddlStatu.Enabled = false;
                                chkSelect.Enabled = false;
                                txtStudyNumber.ReadOnly = true;
                                txt_temp_Notes.ReadOnly = true;
                            }
                        }
                        lst.BackColor = System.Drawing.Color.LightSeaGreen;
                        if (dr.ItemArray.GetValue(9).ToString() == "True")
                        {
                            Session["BILLED_EVENT"] = "BILLED";
                        }
                    }
                }
            }

            // End : Select procedure codes from grid which was selected by user.
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

    protected void grdProcedureCode_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
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
        catch { }
    }

    #endregion

    #region "Save appointment using  transaction"

    protected void btnLoadPageData_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {

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

    protected void btnPECancel_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            //ModalPopupExtender.Show();
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

    protected void btnPEOk_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        Billing_Sys_ManageNotesBO manageNotes;

        calOperation objcalOperation = new calOperation();
        calPatientEO objcalPatientEO = new calPatientEO();
        calEvent objcalEvent = new calEvent();
        CalendarTransaction objCalTran = new CalendarTransaction();
        calResult objResult = new calResult();

        ArrayList objALDoctorAmount = new ArrayList();
        ArrayList objALProcedureCodes = new ArrayList();
        String szProcedurecode = "";

        manageNotes = new Billing_Sys_ManageNotesBO();
        try
        {

            #region "The complete save logic"

            if (txtPatientFName.ReadOnly == false)
            {
                objcalPatientEO = create_calPatientEO();
                txtPatientID.Text = manageNotes.GetPatientLatestID();
                objcalOperation.add_patient = true;
            }
            if (hdnObj.Value.ToString() != "")
            {
                String _isExit = check_appointment();
                if (_isExit != "")
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('" + _isExit + "')", true);
                    ModalPopupExtender.Show();
                    return;
                }



                string[] szArr;
                szArr = hdnObj.Value.ToString().Split(' ');
                DateTime dtPassDate = new DateTime();
                dtPassDate = Convert.ToDateTime(szArr[0]);

                int eventID = 0;
                if (hdnObj.Value.ToString() != "")
                {
                    string szDoctorID = "";
                    String sz_datetime = hdnObj.Value.ToString();

                    if (sz_datetime.IndexOf("~") > 0)
                    {
                        #region "DONT KNOW USE OF THIS BLOCK
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

                        // Start : Use Grid instead of Listbox for Procedure code while adding new visit.
                        // OLD CODE
                        //foreach (ListItem lst in ddlTestNames.Items)
                        //{
                        //    if (lst.Selected == true)
                        //    {
                        //        arrOBJ = new ArrayList();
                        //        arrOBJ.Add(szDoctorID);
                        //        arrOBJ.Add(lst.Value);
                        //        arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        //        arrOBJ.Add(lst.Value);
                        //        _bill_Sys_ReferalEvent.AddDoctorAmount(arrOBJ);

                        //        arrOBJ = new ArrayList();
                        //        arrOBJ.Add(txtPatientID.Text);
                        //        arrOBJ.Add(szDoctorID);
                        //        arrOBJ.Add(hdnObj.Value.ToString().Substring(0, hdnObj.Value.ToString().IndexOf(" ")));
                        //        arrOBJ.Add(lst.Value);
                        //        arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        //        arrOBJ.Add(ddlType.SelectedValue);
                        //        _bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);
                        //    }
                        //}
                        // NEW CODE

                        for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
                        {

                            CheckBox chkTemp = (CheckBox)grdProcedureCode.Items[iGrdRow].FindControl("chkSelect");
                            if (chkTemp.Checked == true)
                            {
                                szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                                arrOBJ = new ArrayList();
                                arrOBJ.Add(szDoctorID);
                                arrOBJ.Add(szProcedurecode); // procedure code
                                arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                arrOBJ.Add(szProcedurecode); // procedure code
                                _bill_Sys_ReferalEvent.AddDoctorAmount(arrOBJ);

                                arrOBJ = new ArrayList();
                                arrOBJ.Add(txtPatientID.Text);
                                arrOBJ.Add(szDoctorID);
                                arrOBJ.Add(hdnObj.Value.ToString().Substring(0, hdnObj.Value.ToString().IndexOf(" ")));
                                arrOBJ.Add(szProcedurecode); // procedure code
                                arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                arrOBJ.Add(ddlType.SelectedValue);
                                _bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);
                            }
                        }

                        // End
                        #endregion
                    }
                    else if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        #region "Doctor amount objects"
                        Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
                        ArrayList arrOBJ = new ArrayList();
                        szDoctorID = extddlDoctor.Text;
                        // Start : Use Grid instead of Listbox for Procedure code while adding new visit.
                        // OLD CODE
                        //foreach (ListItem lst in ddlTestNames.Items)
                        //{
                        //    if (lst.Selected == true)
                        //    {
                        //        calDoctorAmount objDA = new calDoctorAmount();
                        //        objDA.SZ_DOCTOR_ID = szDoctorID;
                        //        objDA.SZ_PROCEDURE_ID = lst.Value;
                        //        objDA.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        //        objDA.SZ_TYPE_CODE_ID = lst.Value;
                        //        objALDoctorAmount.Add(objDA);
                        //        objcalOperation.bt_add_doctor_amount = true;
                        //    }
                        //}
                        // NEW CODE
                        for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
                        {

                            CheckBox chkTemp = (CheckBox)grdProcedureCode.Items[iGrdRow].FindControl("chkSelect");
                            if (chkTemp.Checked == true)
                            {
                                szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                                calDoctorAmount objDA = new calDoctorAmount();
                                objDA.SZ_DOCTOR_ID = szDoctorID;
                                objDA.SZ_PROCEDURE_ID = szProcedurecode;
                                objDA.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                objDA.SZ_TYPE_CODE_ID = szProcedurecode;
                                objALDoctorAmount.Add(objDA);
                                objcalOperation.bt_add_doctor_amount = true;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        szDoctorID = extddlDoctor.Text;
                    }
                    #region "Create Event Object"
                    string sz_date = hdnObj.Value.ToString();
                    sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));
                    Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                    ArrayList objAdd = new ArrayList();
                    objcalEvent.SZ_PATIENT_ID = txtPatientID.Text;
                    objcalEvent.DT_EVENT_DATE = sz_date;
                    objcalEvent.DT_EVENT_TIME = ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString();
                    objcalEvent.SZ_EVENT_NOTES = txtNotes.Text;
                    objcalEvent.SZ_DOCTOR_ID = szDoctorID;
                    // Start : Use Grid instead of Listbox for Procedure code while adding new visit.
                    // OLD CODE
                    //if (ddlTestNames.Items.Count > 1) 
                    //{ 
                    //    objcalEvent.SZ_TYPE_CODE_ID = ddlTestNames.Items[1].Value;
                    //} 
                    //else 
                    //objcalEvent.DT_EVENT_DATE = sz_date;
                    //NEW CODE
                    szProcedurecode = "";
                    for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
                    {
                        szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                        break;
                    }

                    if (szProcedurecode != null)
                    {
                        objcalEvent.SZ_TYPE_CODE_ID = szProcedurecode;
                    }
                    else
                        objcalEvent.DT_EVENT_DATE = sz_date;

                    objcalEvent.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    objcalEvent.DT_EVENT_TIME_TYPE = ddlTime.SelectedValue;
                    objcalEvent.DT_EVENT_END_TIME = ddlEndHours.SelectedValue.ToString() + "." + ddlEndMinutes.SelectedValue.ToString();
                    objcalEvent.DT_EVENT_END_TIME_TYPE = ddlEndTime.SelectedValue;
                    objcalEvent.SZ_REFERENCE_ID = extddlReferenceFacility.Text;
                    objcalEvent.BT_STATUS = "0";
                    if (chkTransportation.Checked == true)
                    {
                        objcalEvent.BT_TRANSPORTATION = "1";
                    }
                    else
                    {
                        objcalEvent.BT_TRANSPORTATION = "0";
                    }
                    objcalEvent.DT_EVENT_DATE = sz_date;
                    if (chkTransportation.Checked == true)
                    {
                        objcalEvent.I_TRANSPORTATION_COMPANY = extddlTransport.Text;
                    }
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        objcalEvent.SZ_OFFICE_ID = extddlMedicalOffice.Text;
                    #endregion

                    #region "Create Procedure objects"
                    // Start : Use Grid instead of Listbox for Procedure code while adding new visit.
                    // OLD CODE
                    //foreach (ListItem lst in ddlTestNames.Items)
                    //{
                    //    if (lst.Selected == true)
                    //    {
                    //        calProcedureCodeEO objcalProcedureCodes = new calProcedureCodeEO();
                    //        objcalProcedureCodes.SZ_PROC_CODE = lst.Value;
                    //        objcalProcedureCodes.I_EVENT_ID  = "";
                    //        objcalProcedureCodes.I_STATUS  = "0";
                    //        objALProcedureCodes.Add(objcalProcedureCodes);
                    //    }
                    //}

                    for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
                    {
                        CheckBox chkTemp = (CheckBox)grdProcedureCode.Items[iGrdRow].FindControl("chkSelect");
                        if (chkTemp.Checked == true)
                        {
                            szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                            calProcedureCodeEO objcalProcedureCodes = new calProcedureCodeEO();
                            objcalProcedureCodes.SZ_PROC_CODE = szProcedurecode;
                            objcalProcedureCodes.I_EVENT_ID = "";
                            objcalProcedureCodes.I_STATUS = "0";
                            objALProcedureCodes.Add(objcalProcedureCodes);
                        }
                    }
                    //NEW

                    #endregion

                    objResult = objCalTran.fnc_SaveAppointment(objcalOperation, objcalPatientEO, objALDoctorAmount, objcalEvent, objALProcedureCodes, txtUserId.Text);

                    // Start : Save appointment Notes.

                    if (objResult.msg_code == "SUCCESS")
                    {
                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + sz_datetime.Substring(0, sz_datetime.IndexOf("!")); //"Date : " + sz_datetime;
                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                    }
                }
                if (hdnObj.Value.ToString().IndexOf("~") > 0)
                {
                    Session["PopUp"] = null;
                }
                else
                {
                    Session["PopUp"] = "True";
                }
            }

            string[] strSelectedDate = hdnObj.Value.ToString().Split('/');
            string strDate = strSelectedDate[0] + "/" + strSelectedDate[1] + "/" + strSelectedDate[2].Substring(0, 4).ToString();
            if (strDate != "")
            {
                if (extddlReferringFacility.Visible == false)
                {
                    GetCalenderDayAppointments(Convert.ToDateTime(strDate).ToString("MM/dd/yyyy"), txtCompanyID.Text);
                }
                else
                {
                    GetCalenderDayAppointments(Convert.ToDateTime(strDate).ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
                }
                if (txtPatientExistMsg.Value != "")
                {
                    string szScheduleDay = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(strDate).ToString("MM/dd/yyyy")); // MMDDYY format
                    if (extddlReferringFacility.Visible == false)
                    {
                        GetCalenderDayAppointments(szScheduleDay, txtCompanyID.Text);
                    }
                    else
                    {
                        GetCalenderDayAppointments(szScheduleDay, extddlReferringFacility.Text);
                    }

                }
            }
            if (objResult.msg_code == "SUCCESS")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Appointment details saved successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('" + objResult.msg + "')", true);
            }
            ClearValues();
            txtPatientExistMsg.Value = "";
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


        Billing_Sys_ManageNotesBO manageNotes;
        OutSchedulePatientDAO objOutPatientDAO = new OutSchedulePatientDAO();
        string newCaseID = "";
        string oldCaseID = "";
        calOperation objcalOperation = new calOperation();
        calPatientEO objcalPatientEO = new calPatientEO();
        calEvent objcalEvent = new calEvent();
        CalendarTransaction objCalTran = new CalendarTransaction();
        calResult objResult = new calResult();

        ArrayList objALDoctorAmount = new ArrayList();
        ArrayList objALProcedureCodes = new ArrayList();
        String szProcedurecode = "";
        Bill_Sys_Calender _bill_Sys_Calender;
        manageNotes = new Billing_Sys_ManageNotesBO();

        try
        {

            #region "Validation"



            if (txtPatientFName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Patient First Name should not be Empty.')", true);
                ModalPopupExtender.Show();
                return;
            }
            if (txtPatientLName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Patient Last name should not be Empty.')", true);
                ModalPopupExtender.Show();
                return;
            }
            if (extddlReferenceFacility.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Please Select any one Reference Facility.')", true);
                ModalPopupExtender.Show();
                return;
            }
            if (extddlDoctor.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Please Select any one Referring Doctor.')", true);
                ModalPopupExtender.Show();
                return;
            }
            if (chkTransportation.Checked == true && extddlTransport.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('please select transport company.')", true);
                ModalPopupExtender.Show();
                return;
            }

            #endregion

            #region "The complete save logic"

            //if (txtRefChartNumber.Visible == true)
            //{
            //    if (!txtRefChartNumber.Text.Equals(""))
            //    {

            //        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();

            //        string flag = "CHART";
            //        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            //        {
            //            flag = "REF";
            //        }
            //        if (_bill_Sys_PatientBO.ExistChartNumber(txtCompanyID.Text, txtRefChartNumber.Text, flag))
            //        {
            //            usrMessage.PutMessage(txtRefChartNumber.Text + "  chart no is already exist ...!");
            //            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
            //            usrMessage.Show();
            //            return;

            //        }
            //    }

            //    }



            if (txtPatientFName.ReadOnly == false)
            {

                #region "Check patient is already exist with entered details"

                ArrayList _arrayList = new ArrayList();

                _arrayList.Add(txtPatientFName.Text); // Patient First Name
                _arrayList.Add(txtPatientLName.Text); // Patient Last Name
                _arrayList.Add(null); // Date of Accident
                _arrayList.Add(extddlCaseType.Text); // Case Type 

                if (txtBirthdate.Text != "") { _arrayList.Add(txtBirthdate.Text); } else { _arrayList.Add(null); } // Date of Birth
                _arrayList.Add(txtCompanyID.Text); // Company ID
                _arrayList.Add("existpatient"); // Flag which is used in stored procedure.
                Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                String iexists = _bill_Sys_PatientBO.CheckPatientExists(_arrayList);


                if (iexists != "" && txtPatientExistMsg.Value == "")
                {
                    msgPatientExists.InnerHtml = iexists;
                    ModalPopupExtender.Show();
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "javascript:openExistsPage();", true);
                    return;
                }

                #endregion


                objcalPatientEO = create_calPatientEO();
                txtPatientID.Text = manageNotes.GetPatientLatestID();
                objcalOperation.add_patient = true;
            }


            if (hdnObj.Value.ToString() != "")
            {
                String _isExit = check_appointment();
                if (_isExit != "")
                {
                    //ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('The patient is already scheduled for this date.')", true);
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('" + _isExit + "')", true);
                    ModalPopupExtender.Show();
                    return;
                }

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false)
                {
                    String _isExitForPeriod = check_appointment_for_period();
                    if (_isExitForPeriod != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('The patient is already scheduled for this date and time period.')", true);
                        ModalPopupExtender.Show();
                        return;
                    }
                }

                string[] szArr;
                szArr = hdnObj.Value.ToString().Split(' ');
                DateTime dtPassDate = new DateTime();
                dtPassDate = Convert.ToDateTime(szArr[0]);

                int eventID = 0;
                if (hdnObj.Value.ToString() != "")
                {
                    string szDoctorID = "";
                    String sz_datetime = hdnObj.Value.ToString();

                    if (sz_datetime.IndexOf("~") > 0)
                    {
                        #region "DONT KNOW USE OF THIS BLOCK
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

                        // Start : Use Grid instead of Listbox for Procedure code while adding new visit.
                        // OLD CODE
                        //foreach (ListItem lst in ddlTestNames.Items)
                        //{
                        //    if (lst.Selected == true)
                        //    {
                        //        arrOBJ = new ArrayList();
                        //        arrOBJ.Add(szDoctorID);
                        //        arrOBJ.Add(lst.Value);
                        //        arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        //        arrOBJ.Add(lst.Value);
                        //        _bill_Sys_ReferalEvent.AddDoctorAmount(arrOBJ);

                        //        arrOBJ = new ArrayList();
                        //        arrOBJ.Add(txtPatientID.Text);
                        //        arrOBJ.Add(szDoctorID);
                        //        arrOBJ.Add(hdnObj.Value.ToString().Substring(0, hdnObj.Value.ToString().IndexOf(" ")));
                        //        arrOBJ.Add(lst.Value);
                        //        arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        //        arrOBJ.Add(ddlType.SelectedValue);
                        //        _bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);
                        //    }
                        //}
                        // NEW CODE

                        for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
                        {

                            CheckBox chkTemp = (CheckBox)grdProcedureCode.Items[iGrdRow].FindControl("chkSelect");
                            if (chkTemp.Checked == true)
                            {
                                szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                                arrOBJ = new ArrayList();
                                arrOBJ.Add(szDoctorID);
                                arrOBJ.Add(szProcedurecode); // procedure code
                                arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                arrOBJ.Add(szProcedurecode); // procedure code
                                _bill_Sys_ReferalEvent.AddDoctorAmount(arrOBJ);

                                arrOBJ = new ArrayList();
                                arrOBJ.Add(txtPatientID.Text);
                                arrOBJ.Add(szDoctorID);
                                arrOBJ.Add(hdnObj.Value.ToString().Substring(0, hdnObj.Value.ToString().IndexOf(" ")));
                                arrOBJ.Add(szProcedurecode); // procedure code
                                arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                arrOBJ.Add(ddlType.SelectedValue);
                                _bill_Sys_ReferalEvent.AddPatientProc(arrOBJ);
                            }
                        }

                        // End
                        #endregion
                    }
                    else if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        #region "Doctor amount objects"
                        Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
                        ArrayList arrOBJ = new ArrayList();
                        szDoctorID = extddlDoctor.Text;
                        // Start : Use Grid instead of Listbox for Procedure code while adding new visit.
                        // OLD CODE
                        //foreach (ListItem lst in ddlTestNames.Items)
                        //{
                        //    if (lst.Selected == true)
                        //    {
                        //        calDoctorAmount objDA = new calDoctorAmount();
                        //        objDA.SZ_DOCTOR_ID = szDoctorID;
                        //        objDA.SZ_PROCEDURE_ID = lst.Value;
                        //        objDA.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        //        objDA.SZ_TYPE_CODE_ID = lst.Value;
                        //        objALDoctorAmount.Add(objDA);
                        //        objcalOperation.bt_add_doctor_amount = true;
                        //    }
                        //}
                        // NEW CODE
                        for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
                        {

                            CheckBox chkTemp = (CheckBox)grdProcedureCode.Items[iGrdRow].FindControl("chkSelect");
                            if (chkTemp.Checked == true)
                            {
                                szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                                calDoctorAmount objDA = new calDoctorAmount();
                                objDA.SZ_DOCTOR_ID = szDoctorID;
                                objDA.SZ_PROCEDURE_ID = szProcedurecode;
                                objDA.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                objDA.SZ_TYPE_CODE_ID = szProcedurecode;
                                objALDoctorAmount.Add(objDA);
                                objcalOperation.bt_add_doctor_amount = true;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        szDoctorID = extddlDoctor.Text;
                    }

                    //Code To update Office Id
                    if (extddlMedicalOffice.Visible == true && extddlMedicalOffice.Text != "" && extddlMedicalOffice.Text != "NA")
                    {
                        //ArrayList objUpdateOffice = new ArrayList();
                        //_bill_Sys_Calender = new Bill_Sys_Calender();
                        //objUpdateOffice.Add(txtPatientID.Text);
                        //objUpdateOffice.Add(extddlMedicalOffice.Text);
                        //objUpdateOffice.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        //objUpdateOffice.Add(extddlDoctor.Text);
                        //_bill_Sys_Calender.Update_Office_Id(objUpdateOffice);
                    }

                    //end code


                    #region "Create Event Object"
                    string sz_date = hdnObj.Value.ToString();
                    sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));
                    _bill_Sys_Calender = new Bill_Sys_Calender();
                    ArrayList objAdd = new ArrayList();
                    objcalEvent.SZ_PATIENT_ID = txtPatientID.Text;
                    objcalEvent.DT_EVENT_DATE = sz_date;
                    objcalEvent.DT_EVENT_TIME = ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString();
                    objcalEvent.SZ_EVENT_NOTES = txtNotes.Text;
                    objcalEvent.SZ_DOCTOR_ID = szDoctorID;
                    // Start : Use Grid instead of Listbox for Procedure code while adding new visit.
                    // OLD CODE
                    //if (ddlTestNames.Items.Count > 1) 
                    //{ 
                    //    objcalEvent.SZ_TYPE_CODE_ID = ddlTestNames.Items[1].Value;
                    //} 
                    //else 
                    //objcalEvent.DT_EVENT_DATE = sz_date;
                    //NEW CODE
                    szProcedurecode = "";
                    for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
                    {
                        szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                        break;
                    }

                    if (szProcedurecode != null)
                    {
                        objcalEvent.SZ_TYPE_CODE_ID = szProcedurecode;
                    }
                    else
                        objcalEvent.DT_EVENT_DATE = sz_date;

                    objcalEvent.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    objcalEvent.DT_EVENT_TIME_TYPE = ddlTime.SelectedValue;
                    objcalEvent.DT_EVENT_END_TIME = ddlEndHours.SelectedValue.ToString() + "." + ddlEndMinutes.SelectedValue.ToString();
                    objcalEvent.DT_EVENT_END_TIME_TYPE = ddlEndTime.SelectedValue;
                    objcalEvent.SZ_REFERENCE_ID = extddlReferenceFacility.Text;
                    objcalEvent.BT_STATUS = "0";
                    if (chkTransportation.Checked == true)
                    {
                        objcalEvent.BT_TRANSPORTATION = "1";
                    }
                    else
                    {
                        objcalEvent.BT_TRANSPORTATION = "0";
                    }
                    objcalEvent.DT_EVENT_DATE = sz_date;
                    if (chkTransportation.Checked == true)
                    {
                        objcalEvent.I_TRANSPORTATION_COMPANY = extddlTransport.Text;
                    }
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        objcalEvent.SZ_OFFICE_ID = extddlMedicalOffice.Text;
                    #endregion

                    #region "Create Procedure objects"
                    // Start : Use Grid instead of Listbox for Procedure code while adding new visit.
                    // OLD CODE
                    //foreach (ListItem lst in ddlTestNames.Items)
                    //{
                    //    if (lst.Selected == true)
                    //    {
                    //        calProcedureCodeEO objcalProcedureCodes = new calProcedureCodeEO();
                    //        objcalProcedureCodes.SZ_PROC_CODE = lst.Value;
                    //        objcalProcedureCodes.I_EVENT_ID  = "";
                    //        objcalProcedureCodes.I_STATUS  = "0";
                    //        objALProcedureCodes.Add(objcalProcedureCodes);
                    //    }
                    //}

                    for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
                    {
                        CheckBox chkTemp = (CheckBox)grdProcedureCode.Items[iGrdRow].FindControl("chkSelect");
                        if (chkTemp.Checked == true)
                        {
                            szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                            calProcedureCodeEO objcalProcedureCodes = new calProcedureCodeEO();
                            objcalProcedureCodes.SZ_PROC_CODE = szProcedurecode;
                            objcalProcedureCodes.I_EVENT_ID = "";
                            objcalProcedureCodes.I_STATUS = "0";
                            objALProcedureCodes.Add(objcalProcedureCodes);
                        }
                    }
                    //NEW

                    #endregion

                    if (canCopy == "1")
                    {

                        if (txtPatientFName.ReadOnly == false)
                        {

                            #region "Check patient is already exist with entered details"

                            ArrayList _arrayList = new ArrayList();

                            _arrayList.Add(txtPatientFName.Text); // Patient First Name
                            _arrayList.Add(txtPatientLName.Text); // Patient Last Name
                            _arrayList.Add(null); // Date of Accident
                            _arrayList.Add(extddlCaseType.Text); // Case Type 

                            if (txtBirthdate.Text != "") { _arrayList.Add(txtBirthdate.Text); } else { _arrayList.Add(null); } // Date of Birth
                            _arrayList.Add(extddlReferringFacility.Text); // Company ID
                            _arrayList.Add("existpatient"); // Flag which is used in stored procedure.
                            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                            String iexists = _bill_Sys_PatientBO.CheckPatientExists(_arrayList);


                            if (iexists != "" && txtPatientExistMsg.Value == "")
                            {
                                msgPatientExists.InnerHtml = iexists;
                                ModalPopupExtender.Show();
                                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "javascript:openExistsPage();", true);
                                return;
                            }

                            #endregion


                            objOutPatientDAO = create_outPatientDAO();
                            txtPatientID.Text = manageNotes.GetPatientLatestID();
                            objOutPatientDAO.addPatient = true;
                        }


                        string roomID = sz_datetime.Substring(sz_datetime.IndexOf("!") + 1, 20);
                        OutSchedulePatient objOutTran = new OutSchedulePatient();
                        if (txtPatientFName.ReadOnly == true)
                        {
                            if (objcalEvent.SZ_PATIENT_ID != null)
                            {
                                oldCaseID = objOutTran.GetCaseIdForDocumentPath(objcalEvent.SZ_PATIENT_ID);
                            }

                        }
                        objResult = objOutTran.AddVisit(objcalOperation, objOutPatientDAO, objALDoctorAmount, objcalEvent, objALProcedureCodes, txtUserId.Text, roomID, extddlReferringFacility.Text, txtCompanyID.Text, extddlDoctor.Text);
                        if (objResult.msg_code == "SUCCESS")
                        {
                            newCaseID = objOutTran.GetCaseIdForDocumentPath(objResult.sz_patient_id);
                        }

                        if (oldCaseID != "")
                        {

                            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
                            string szDefaultPath = objNF3Template.getPhysicalPath();
                            DataSet ds = objOutTran.GetNodeIdForCopyDocument(extddlReferringFacility.Text);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            string inodeType = ds.Tables[0].Rows[i]["SZ_NODE_TYPE"].ToString();
                                            string sourcePath = objOutTran.GetSourcePath(txtCompanyID.Text, inodeType, oldCaseID);
                                            string destPath = objOutTran.GetDestPath(extddlReferringFacility.Text, newCaseID);
                                            if (sourcePath != "")
                                            {
                                                string fullSourcePath = szDefaultPath + sourcePath;
                                                string fullDestPath = szDefaultPath + destPath + "/" + sourcePath;
                                                DirectoryCopy(fullSourcePath, fullDestPath);
                                            }
                                        }

                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        objResult = objCalTran.fnc_SaveAppointment(objcalOperation, objcalPatientEO, objALDoctorAmount, objcalEvent, objALProcedureCodes, txtUserId.Text);
                    }
                    //Nirmal



                    // Start : Save appointment Notes.

                    if (objResult.msg_code == "SUCCESS")
                    {
                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + sz_datetime.Substring(0, sz_datetime.IndexOf("!")); //"Date : " + sz_datetime;
                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                    }
                }
                if (hdnObj.Value.ToString().IndexOf("~") > 0)
                {
                    Session["PopUp"] = null;
                }
                else
                {
                    Session["PopUp"] = "True";
                }
            }

            string[] strSelectedDate = hdnObj.Value.ToString().Split('/');
            string strDate = strSelectedDate[0] + "/" + strSelectedDate[1] + "/" + strSelectedDate[2].Substring(0, 4).ToString();
            if (strDate != "")
            {
                if (extddlReferringFacility.Visible == false)
                {
                    GetCalenderDayAppointments(Convert.ToDateTime(strDate).ToString("MM/dd/yyyy"), txtCompanyID.Text);
                }
                else
                {
                    GetCalenderDayAppointments(Convert.ToDateTime(strDate).ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
                }
                if (txtPatientExistMsg.Value != "")
                {
                    string szScheduleDay = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(strDate).ToString("MM/dd/yyyy")); // MMDDYY format
                    if (extddlReferringFacility.Visible == false)
                    {
                        GetCalenderDayAppointments(szScheduleDay, txtCompanyID.Text);
                    }
                    else
                    {
                        GetCalenderDayAppointments(szScheduleDay, extddlReferringFacility.Text);
                    }

                }
            }
            if (objResult.msg_code == "SUCCESS")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('Appointment saved successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "alert('" + objResult.msg + "')", true);
            }
            ClearValues();
            //if (txtPatientExistMsg.Value != "")
            //{
            //    string szScheduleDay1 = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(strDate).ToString("MM/dd/yyyy")); // MMDDYY format
            //    // javascript:__doPostBack('ct100$ContentPlaceHolder1$2010_MAY_Link_24','');
            //    DateTime dtSelectedDate = Convert.ToDateTime(szScheduleDay1);
            //    //String szdoPostBack = "javascript:__doPostBack('ct100$ContentPlaceHolder1$" + dtSelectedDate.Year.ToString() + "_" + dtSelectedDate.ToString("MMM").ToUpper().ToString() + "_Link_" + dtSelectedDate.Day.ToString() + "'";
            //    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Msg", "javascript:Test();", true);
            //}
            txtPatientExistMsg.Value = "";
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private static void DirectoryCopy(string sourceDirName, string destDirName)
    {
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);

        //if (System.IO.Directory.Exists(destDirName))
        //{
        //    //MessageBox.Show("Directory already Exists !!");
        //}
        //else
        //{
        if (dir.Exists)
        {
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                if (File.Exists(temppath))
                {
                }
                else
                {
                    file.CopyTo(temppath, false);
                }

            }
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }
        }
        else
        {
        }
        //}
    }


    protected void btnYes_Click(object sender, EventArgs e)
    {

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        //   btnNo.Attributes.Add("onclick", "NoMassage");
    }

    //public void PerformClear()
    //{
    //}
    //public void PerformSearch()
    //{

    //}
    //public void PerformSave()
    //{
    //}
    //protected void btnYes_Click(object sender, EventArgs e)
    //{
    //    btnYes.Attributes.Add("onclick", "YesMassage");
    //}

    //protected void btnNo_Click(object sender, EventArgs e)
    //{
    //    btnNo.Attributes.Add("onclick", "NoMassage");
    //    ModalPopupExtender.Show();
    //    return;
    //}
    //NIRMAL
    private OutSchedulePatientDAO create_outPatientDAO()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        OutSchedulePatientDAO objoutPatientDAO = new OutSchedulePatientDAO();
        Bill_Sys_Calender objCal = new Bill_Sys_Calender();
        try
        {
            extddlCaseStatus.Text = objCal.GetOpenCaseStatus(extddlReferringFacility.Text);
            ArrayList objAL = new ArrayList();
            objoutPatientDAO.sPatientFirstName = txtPatientFName.Text;
            objoutPatientDAO.sPatientLastName = txtPatientLName.Text;
            objoutPatientDAO.sCaseTypeID = extddlCaseType.Text;
            objoutPatientDAO.sPatientAddress = txtPatientAddress.Text;
            objoutPatientDAO.sPatientCity = TextBox3.Text; //city
            objoutPatientDAO.sPatientPhone = txtPatientPhone.Text;
            objoutPatientDAO.sPatientState = extddlPatientState.Text;
            objoutPatientDAO.sSourceCompanyID = txtCompanyID.Text;
            objoutPatientDAO.sPatientMI = txtMI.Text;
            objoutPatientDAO.sCaseStatusID = extddlCaseStatus.Text;
            objoutPatientDAO.sInsuranceID = extddlInsuranceCompany.Text;
            objoutPatientDAO.sDestinationCompanyID = extddlReferringFacility.Text;

            // objCal.savePatientForReferringFacility(objAL);
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
        return objoutPatientDAO;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    //END NIRMAL


    private calPatientEO create_calPatientEO()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        calPatientEO objcalPatientEO = new calPatientEO();
        Bill_Sys_Calender objCal = new Bill_Sys_Calender();
        try
        {
            extddlCaseStatus.Text = objCal.GetOpenCaseStatus(txtCompanyID.Text);
            ArrayList objAL = new ArrayList();
            objcalPatientEO.SZ_PATIENT_FIRST_NAME = txtPatientFName.Text;
            objcalPatientEO.SZ_PATIENT_LAST_NAME = txtPatientLName.Text;
            objcalPatientEO.SZ_CASE_TYPE_ID = extddlCaseType.Text;
            objcalPatientEO.SZ_PATIENT_ADDRESS = txtPatientAddress.Text;
            objcalPatientEO.SZ_PATIENT_CITY = TextBox3.Text; //city
            objcalPatientEO.SZ_PATIENT_PHONE = txtPatientPhone.Text;
            objcalPatientEO.SZ_PATIENT_STATE_ID = extddlPatientState.Text;
            objcalPatientEO.SZ_COMPANY_ID = txtCompanyID.Text;
            objcalPatientEO.MI = txtMI.Text;
            objcalPatientEO.SZ_CASE_STATUS_ID = extddlCaseStatus.Text;
            objcalPatientEO.SZ_INSURANCE_ID = extddlInsuranceCompany.Text;
            // objCal.savePatientForReferringFacility(objAL);
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
        return objcalPatientEO;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    #endregion

    #region "Update appointment Logic"

    protected void btnUpdate_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false)
            {
                updateAppointmentFromBillingCompany();
            }
            else
            {
                updateAppointmentFromTestFacility();
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

    protected void updateAppointmentFromBillingCompany()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            String szProcedurecode = "";
            #region "Check Validation"

            if (txtPatientFName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Patient First Name should not be Empty.')", true);
                ModalPopupExtender.Show();
                return;
            }

            if (txtPatientLName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Patient Last name should not be Empty.')", true);
                ModalPopupExtender.Show();
                return;
            }

            if (ddlType.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Please Select any one Type.')", true);
                ModalPopupExtender.Show();
                return;
            }

            if (extddlDoctor.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Please Select any one Referring Doctor.')", true);
                ModalPopupExtender.Show();
                return;
            }

            //if (ddlTestNames.SelectedIndex == -1)
            //{
            //    ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Please Select any one Procedure Code...!!')", true);
            //    ModalPopupExtender.Show();
            //    return;
            //}

            #endregion


            if (chkTransportation.Checked == true && extddlTransport.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('please select transport company.')", true);
                return;
            }
            int eventID = Convert.ToInt32(Session["SCHEDULEDID"].ToString());
            string sz_date = hdnObj.Value;
            sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));
            Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
            ArrayList objAdd = new ArrayList();
            objAdd.Add(txtPatientID.Text);
            objAdd.Add(sz_date);
            objAdd.Add(ddlHours.SelectedValue.ToString() + "." + ddlMinutes.SelectedValue.ToString());
            objAdd.Add(txtNotes.Text);
            objAdd.Add(extddlDoctor.Text);
            // Use grid instead on Listbox on procedure code.
            // OLD LOGIC
            //if (ddlTestNames.Items.Count > 1) { objAdd.Add(ddlTestNames.Items[1].Value); } else objAdd.Add("");

            // NEW LOGIC
            szProcedurecode = "";
            for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
            {
                szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                break;
            }

            if (szProcedurecode != null) { objAdd.Add(szProcedurecode); } else objAdd.Add("");


            objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            objAdd.Add(ddlTime.SelectedValue);
            objAdd.Add(ddlEndHours.SelectedValue.ToString() + "." + ddlEndMinutes.SelectedValue.ToString());
            objAdd.Add(ddlEndTime.SelectedValue);
            objAdd.Add(extddlReferenceFacility.Text);
            objAdd.Add(eventID);
            if (chkTransportation.Checked == true) { objAdd.Add(1); } else { objAdd.Add(0); }
            if (chkTransportation.Checked == true && extddlTransport.Text != "NA") { objAdd.Add(Convert.ToInt32(extddlTransport.Text)); } else { objAdd.Add(null); }
            DateTime objTemp = new DateTime();
            objTemp = Convert.ToDateTime(sz_date);
            DateTime objDate = new DateTime();
            if (ddlTime.SelectedValue == "AM")
            {
                objDate = new DateTime(objTemp.Year, objTemp.Month, objTemp.Day, Convert.ToInt32(ddlHours.SelectedValue), Convert.ToInt32(ddlEndMinutes.SelectedValue), 0);
            }
            else
            {
                int _finalHours = 0;
                if (Convert.ToInt32(ddlHours.SelectedValue) == 12)
                {
                    _finalHours = Convert.ToInt32(ddlHours.SelectedValue);
                }
                else
                {
                    _finalHours = Convert.ToInt32(ddlHours.SelectedValue) + 12;
                }
                objDate = new DateTime(objTemp.Year, objTemp.Month, objTemp.Day, _finalHours, Convert.ToInt32(ddlEndMinutes.SelectedValue), 0);
            }

            eventID = _bill_Sys_Calender.UPDATE_Event_Referral(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
            _bill_Sys_Calender.Delete_Event_RefferPrcedure(eventID);
            // OLD CODE
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

            // NEW CODE
            for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
            {
                CheckBox chkTemp = (CheckBox)grdProcedureCode.Items[iGrdRow].FindControl("chkSelect");
                if (chkTemp.Checked == true)
                {
                    szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                    objAdd = new ArrayList();
                    objAdd.Add(szProcedurecode);
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
            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + sz_date;
            _DAO_NOTES_BO = new DAO_NOTES_BO();
            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
            _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
            Session["PopUp"] = "True";
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_AppointPatientEntry.aspx';</script>");
            //Modified By BowandBaan for Include Ajax Extension - Modification Starts      

            string[] strSelectedDate = hdnObj.Value.ToString().Split('/');
            string strDate = strSelectedDate[0] + "/" + strSelectedDate[1] + "/" + strSelectedDate[2].Substring(0, 4).ToString();
            if (strDate != "")
            {
                if (extddlReferringFacility.Visible == false)
                {
                    GetCalenderDayAppointments(Convert.ToDateTime(strDate).ToString("MM/dd/yyyy"), txtCompanyID.Text);
                }
                else
                {
                    GetCalenderDayAppointments(Convert.ToDateTime(strDate).ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
                }

            }
            ClearValues();
            ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Appointment updated successfully.')", true);
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_AppointPatientEntry.aspx';</script>");
            //Modified By BowandBaan for Include Ajax Extension - Modification Ends    
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

    protected void updateAppointmentFromTestFacility()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        /*
         * Use of int Flag = 0; 
         * Flag is used to find procedure codes from "Procedure grid" have status other than
         * visit not completed(Schedule). If we found procedure codes with status other than visit not completed.(Schedule)
         * Then check for Errors. 
         * For Example:
         * If status is Re-schedule then user have to enter valid date and time.
         */


        int Flag = 0;
        try
        {
            bool blError = false;
            // 16 April 2010 check the extddltransport dropdown it fill or not --- sachin
            if (chkTransportation.Checked == true && extddlTransport.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('please select transport company.')", true);
                ModalPopupExtender.Show();
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
                            //lblMsg.Text = "Please select procedure status";
                            //lblMsg.Visible = true;
                            //lblMsg.ForeColor = System.Drawing.Color.Red;
                            //blError = true;
                            //break;

                            ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Please select procedure status.')", true);
                            ModalPopupExtender.Show();
                            return;
                        }

                        if (Convert.ToInt32(ddlStatu.SelectedValue) == 1)
                        {
                            TextBox txtDate = (TextBox)lst.Cells[5].FindControl("txtReScheduleDate");
                            if (txtDate.Text == "")
                            {
                                //lblMsg.Text = "Please enter valid rescheduled date";
                                //lblMsg.Visible = true;
                                //lblMsg.ForeColor = System.Drawing.Color.Red;
                                //blError = true;
                                //break;
                                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Please enter valid rescheduled date.')", true);
                                ModalPopupExtender.Show();
                                return;

                            }
                            try
                            {
                                DateTime departDate = DateTime.Parse(txtDate.Text);
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
                                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Please enter valid rescheduled date.')", true);
                                ModalPopupExtender.Show();
                                return;
                            }

                            DropDownList ddlHR = (DropDownList)lst.Cells[6].FindControl("ddlReSchHours");
                            DropDownList ddlReMIn = (DropDownList)lst.Cells[6].FindControl("ddlReSchMinutes");
                            DropDownList ddlReSchTime = (DropDownList)lst.Cells[6].FindControl("ddlReSchTime");
                            if (ddlHR.SelectedValue == "00")
                            {
                                //lblMsg.Text = "Please enter valid rescheduled time";
                                //lblMsg.Visible = true;
                                //lblMsg.ForeColor = System.Drawing.Color.Red;
                                //blError = true;
                                //break;
                                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Please enter valid rescheduled time.')", true);
                                ModalPopupExtender.Show();
                                return;
                            }

                            #region "Check for Previous Date"
                            DateTime dtEnteredDate = Convert.ToDateTime(Convert.ToDateTime(txtDate.Text).ToString("MM/dd/yyyy") + " " + ddlHR.SelectedValue + ":" + ddlReMIn.SelectedValue + " " + ddlReSchTime.SelectedValue);
                            DateTime dtEnteredLastDate = dtEnteredDate.AddMinutes(30);
                            #endregion


                            #region "Check For Room Days and Time"

                            String szTemp = hdnObj.Value;
                            String[] szArr = szTemp.Split('!');
                            String[] szArr1 = szArr[1].Split('~');
                            String szRoomID = szArr1[0].ToString();


                            Bill_Sys_RoomDays objRD = new Bill_Sys_RoomDays();
                            ArrayList objChekList = new ArrayList();
                            objChekList.Add(szRoomID);
                            objChekList.Add(Convert.ToDateTime(txtDate.Text).ToString("MM/dd/yyyy"));
                            objChekList.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objChekList.Add(dtEnteredDate.Hour.ToString() + "." + dtEnteredDate.Minute.ToString());
                            objChekList.Add(dtEnteredLastDate.Hour.ToString() + "." + dtEnteredLastDate.Minute.ToString());
                            if (!objRD.checkRoomTiming(objChekList))
                            {
                                String szMsg = objRD.getRoomStart_EndTime(szRoomID, Convert.ToDateTime(txtDate.Text).ToString("MM/dd/yyyy"), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Please add visit on " + Convert.ToDateTime(txtDate.Text).ToString("MM/dd/yyyy") + " between " + szMsg + ".')", true);
                                ModalPopupExtender.Show();
                                return;
                            }
                            #endregion
                        }
                    }
                }

            }
            if (blError == false)
            {
                int eventID = Convert.ToInt32(Session["SCHEDULEDID"].ToString());
                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();

                //Code To update Office Id
                if (extddlMedicalOffice.Visible == true && extddlMedicalOffice.Text != "" && extddlMedicalOffice.Text != "NA")
                {
                    ArrayList objUpdateOffice = new ArrayList();

                    objUpdateOffice.Add(txtPatientID.Text);
                    objUpdateOffice.Add(extddlMedicalOffice.Text);
                    objUpdateOffice.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    objUpdateOffice.Add(extddlDoctor.Text);
                    _bill_Sys_Calender.Update_Office_Id(objUpdateOffice);
                }

                //end code


                ArrayList objAdd = new ArrayList();
                _bill_Sys_Calender.Delete_Event_RefferPrcedure(eventID);
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
                        int _evenTID = 0;
                        /////////////////// Start : if Status is Reschduled
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
                            if (_evenTID == 0 && chkSelect.Enabled == true)
                            {
                                Bill_Sys_Calender _bill_Calender = new Bill_Sys_Calender();
                                ArrayList _objAdd = new ArrayList();
                                _objAdd.Add(txtPatientID.Text);
                                _objAdd.Add(txtDate.Text);
                                _objAdd.Add(ddlHR.SelectedValue.ToString() + "." + ddlMM.SelectedValue.ToString());
                                _objAdd.Add(txtNotes.Text);
                                _objAdd.Add(extddlDoctor.Text);
                                _objAdd.Add(lst.Cells[1].Text);
                                _objAdd.Add(txtPatientCompany.Text);
                                _objAdd.Add(ddlTIME.SelectedValue);
                                decimal endTime = Convert.ToDecimal(ddlHR.SelectedValue.ToString() + "." + ddlMM.SelectedValue.ToString());
                                endTime = endTime + Convert.ToDecimal("0." + Session["INTERVAL"].ToString());
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
                                _objAdd.Add(endTime);
                                _objAdd.Add(endTimeType);
                                _objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                _objAdd.Add("False");
                                // #149 13 April,2010 change the BT_TRANSPOTATION field patient table to txn_calender_event -- sachin
                                if (chkTransportation.Checked == true) { _objAdd.Add(1); } else { _objAdd.Add(0); }
                                //==================================
                                if (chkTransportation.Checked == true) { _objAdd.Add(Convert.ToInt32(extddlTransport.Text)); } else { _objAdd.Add(0); }

                                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                                {
                                    _objAdd.Add(extddlMedicalOffice.Text); //_objAdd.Add(Session["Office_Id"].ToString());/*_objAdd.Add(extddlMedicalOffice.Text);*/ 
                                }
                                _evenTID = _bill_Calender.Save_Event(_objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());



                                _addedEvent = new AddedEvetDetail();
                                _addedEvent.EventID = _evenTID;
                                _addedEvent.EventDate = Convert.ToDateTime(txtDate.Text);
                                _addedEvent.EventTime = Convert.ToDecimal(ddlHR.SelectedValue.ToString() + "." + ddlMM.SelectedValue.ToString());
                                objAddedEvent.Add(_addedEvent);

                            }
                            if (_evenTID != 0)
                            {
                                ArrayList oAdd = new ArrayList();
                                oAdd.Add(lst.Cells[1].Text);
                                oAdd.Add(_evenTID);
                                oAdd.Add(0);
                                _bill_Sys_Calender.Save_Event_RefferPrcedure(oAdd);
                            }

                            objAdd.Add(txtDate.Text);
                            objAdd.Add(ddlHR.SelectedValue.ToString() + "." + ddlMM.SelectedValue.ToString());
                            objAdd.Add(ddlTIME.SelectedValue);
                            objAdd.Add(_evenTID);
                            objAdd.Add(txtStudyNumber.Text);
                            objAdd.Add(txtProcNotes.Text);

                            _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);


                        }
                        ///////////////////End : if Status is Reschduled


                    ///////////////////Start : if Status is Visit done
                        else if (Convert.ToInt32(ddlStatu.SelectedValue) == 2)
                        {
                            Bill_Sys_Calender _bill_Calender = new Bill_Sys_Calender();
                            objAdd.Add(txtStudyNumber.Text);
                            objAdd.Add(txtProcNotes.Text);
                            _bill_Sys_Calender.Save_Event_OtherVType(objAdd);

                            //_bill_Sys_Calender.Update_Visit_Complete();
                            Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent = new Bill_Sys_ReferalEvent();
                            ArrayList arrOBJ = new ArrayList();
                            arrOBJ.Add(extddlDoctor.Text);
                            arrOBJ.Add(lst.Cells[1].Text);
                            arrOBJ.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            arrOBJ.Add(lst.Cells[1].Text);
                            _bill_Sys_ReferalEvent.AddDoctorAmount(arrOBJ);
                        }
                        ///////////////////End : if Status is Visit done

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
                _bill_Sys_Calender.UPDATE_EventNotes_Status(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString()); // add one filed array list(txtnotes)


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
                if (extddlDoctor.Visible == true && extddlDoctor.Text != "" && extddlDoctor.Text != "NA")
                {
                    ArrayList objUpdateDoctor = new ArrayList();
                    objUpdateDoctor.Add(eventID);
                    objUpdateDoctor.Add(extddlDoctor.Text);
                    _bill_Sys_Calender.Update_Doctor_Id(objUpdateDoctor);
                }

                //end code




                string[] strSelectedDate = hdnObj.Value.ToString().Split('/');
                string strDate = strSelectedDate[0] + "/" + strSelectedDate[1] + "/" + strSelectedDate[2].Substring(0, 4).ToString();

                if (strDate != "")
                {
                    GetCalenderDayAppointments(Convert.ToDateTime(strDate).ToString("MM/dd/yyyy"), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                }
                ClearValues();


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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

    #region "Function used to display Calendar"

    private void BindGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            _obj = new Bill_Sys_Schedular();
            decimal temp = 0.30M;
            if (extddlReferringFacility.Visible == false)
            {
                grdScheduleReport.DataSource = _obj.GET_EVENT_DETAIL(txtCompanyID.Text, DateTime.Today, temp, txtCompanyID.Text);
            }
            else
            {
                grdScheduleReport.DataSource = _obj.GET_EVENT_DETAIL(txtCompanyID.Text, DateTime.Today, temp, extddlReferringFacility.Text);
            }

            grdScheduleReport.DataBind();
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



    private void showCalendar(Calendar_DAO objCalendar)
    {
        //Modified By BowandBaan for Include Ajax Extension - Modification Starts


        //Response.Write("<table border='1' width='300px'>");

        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("<table border='1' width='80px'>"));
        Panel1.Controls.Add(new LiteralControl("<table border='1' width='80px'>"));

        // start -- fill the long name of the month
        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("<tr><td colspan='7'>"));
        Panel1.Controls.Add(new LiteralControl("<tr><td colspan='7'>"));
        string szLongName = "<div align='center' style='font-size:11px;font-weight:bold'>@LONG_MONTH_NAME@</div>";
        szLongName = szLongName.Replace("@LONG_MONTH_NAME@", getLongMonthName(objCalendar.InitialDisplayMonth));

        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl(szLongName));
        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("</td>"));

        Panel1.Controls.Add(new LiteralControl(szLongName));
        Panel1.Controls.Add(new LiteralControl("</td>"));

        // -- ends

        // fill the weekdays first
        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("<tr>"));
        Panel1.Controls.Add(new LiteralControl("<tr>"));
        byte bytStartIndex = getStartIndex(getFirstDayOfMonth(objCalendar.InitialDisplayMonth, objCalendar.InitialDisplayYear));

        string[] szWeekdays = null;

        if (blnWeekShortNames == true)
        {
            szWeekdays = getShortNamesForWeekdays();
        }
        else
        {
            szWeekdays = getOrderedWeekdays();
        }

        for (int iWeekday = 0; iWeekday < szWeekdays.Length; iWeekday++)
        {
            //Response.Write("<td>" + szWeekdays[iWeekday] + "</td>");
            ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px'>" + szWeekdays[iWeekday] + "</td>"));
            Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px'>" + szWeekdays[iWeekday] + "</td>"));
        }

        //Response.Write("</tr>");
        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("</tr>"));
        Panel1.Controls.Add(new LiteralControl("</tr>"));
        // calendar days
        byte bytDayCounter = 1;
        int iWeekdays = 0;
        bool isFirstRow = true;
        byte bytTodaysDate = getTodaysDayOfTheMonth();
        int ClickDate = 0;
        int ClickMonth = 0;

        if (txtGetDay.Value.ToString() != "") { string[] strD = txtGetDay.Value.ToString().Split('_'); ClickMonth = getIntMonth(strD.GetValue(0).ToString()); ClickDate = Convert.ToInt32(strD.GetValue(1).ToString()); }

        LinkButton objLink = null;

        for (int i = 0; i < 6; i++)
        {
            Panel1.Controls.Add(new LiteralControl("<tr>"));

            for (iWeekdays = 0; iWeekdays < 7; iWeekdays++)
            {
                if (iWeekdays < bytStartIndex && isFirstRow == true)
                {
                    Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' bgcolor='" + szDateColor_NA + "'> N/A </TD>"));
                }
                else
                {
                    if (bytDayCounter > getLastMonthInteger())
                    {
                        Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' bgcolor ='" + szDateColor_NA + "'> N/A </TD>"));
                    }
                    else
                    {
                        if (bytTodaysDate == bytDayCounter && DateTime.Now.Month == getIntMonth(objCalendar.InitialDisplayMonth)) // check for month and year too.
                        {
                            string szOutput = "";
                            objLink = new LinkButton();
                            objLink.ID = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                            objLink.CommandArgument = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                            objLink.Text = "" + bytDayCounter;
                            objLink.Click += new EventHandler(Link1_Click);

                            szOutput = "<td style='width:10px;font-size:9px' align='center' bgcolor='@BG_COLOR@'>";
                            szOutput = szOutput.Replace("@BG_COLOR@", szDateColor_TODAY);

                            Panel1.Controls.Add(new LiteralControl(szOutput));
                            Panel1.Controls.Add(objLink);
                            Panel1.Controls.Add(new LiteralControl("</td>"));
                        }
                        else if (ClickDate == bytDayCounter && ClickMonth == getIntMonth(objCalendar.InitialDisplayMonth)) // check for month and year too.
                        {
                            string szOutput = "";
                            objLink = new LinkButton();
                            objLink.ID = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                            objLink.CommandArgument = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                            objLink.Text = "" + bytDayCounter;
                            objLink.Attributes.Add("onclick", "  document.getElementById('ctl00_ContentPlaceHolder1_txtGetDay').value='" + objCalendar.InitialDisplayMonth + "_" + bytDayCounter.ToString() + "';");
                            objLink.Click += new EventHandler(Link1_Click);

                            szOutput = "<td style='width:10px;font-size:9px' align='center' bgcolor='#7FFF00'>";
                            szOutput = szOutput.Replace("#7FFF00", "#7FFF00");

                            Panel1.Controls.Add(new LiteralControl(szOutput));
                            Panel1.Controls.Add(objLink);
                            Panel1.Controls.Add(new LiteralControl("</td>"));
                        }
                        else
                        {
                            if (Session["FROM"] == null)
                            {
                                objLink = new LinkButton();
                                objLink.ID = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                                objLink.CommandArgument = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                                objLink.Text = "" + bytDayCounter;
                                objLink.Click += new EventHandler(Link1_Click);
                                objLink.Attributes.Add("onclick", "  document.getElementById('ctl00_ContentPlaceHolder1_txtGetDay').value='" + objCalendar.InitialDisplayMonth + "_" + bytDayCounter.ToString() + "';");
                                Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' align='center'>"));
                                Panel1.Controls.Add(objLink);
                                Panel1.Controls.Add(new LiteralControl("</td>"));
                            }
                            else
                            {
                                objLink = new LinkButton();
                                objLink.ID = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                                objLink.CommandArgument = objCalendar.InitialDisplayYear + "_" + objCalendar.InitialDisplayMonth + "_Link_" + bytDayCounter;
                                objLink.Text = "" + bytDayCounter;
                                objLink.Click += new EventHandler(Link1_Click);
                                //objLink.Attributes.Add("onclick", "  document.getElementById('ctl00_ContentPlaceHolder1_txtGetDay').value='" + objCalendar.InitialDisplayMonth + "_" + bytDayCounter.ToString() + "';");
                                Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' align='center'>"));
                                Panel1.Controls.Add(objLink);
                                Panel1.Controls.Add(new LiteralControl("</td>"));
                            }
                        }
                    }
                    bytDayCounter++;
                }
            }
            isFirstRow = false;
            //Response.Write("</tr>");
            ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("</tr>"));
            Panel1.Controls.Add(new LiteralControl("</tr>"));
        }
        //Response.Write("</table>");
        ////UpdatePanel7.ContentTemplateContainer.Controls.Add(new LiteralControl("</table>"));
        Panel1.Controls.Add(new LiteralControl("</table>"));

        //Modified By BowandBaan for Include Ajax Extension - Modification Ends
    }

    private string getLongMonthName(string p_szShortMonth)
    {
        p_szShortMonth = p_szShortMonth.ToUpper();
        switch (p_szShortMonth)
        {
            case "JAN":
                return "January";
            case "FEB":
                return "February";
            case "MAR":
                return "March";
            case "APR":
                return "April";
            case "MAY":
                return "May";
            case "JUN":
                return "June";
            case "JUL":
                return "July";
            case "AUG":
                return "August";
            case "SEP":
                return "September";
            case "OCT":
                return "October";
            case "NOV":
                return "November";
            case "DEC":
                return "December";
            default:
                return "January";
        }
        return "January";
    }

    private string getFirstDayOfMonth(string p_szMonth, string p_szYear)
    {
        int iMonth = getIntMonth(p_szMonth);
        int iYear = Int32.Parse(p_szYear);
        int iDay = 01;
        DateTime objTDay = new DateTime(iYear, iMonth, iDay);
        return objTDay.DayOfWeek.ToString();
    }

    private byte getStartIndex(string p_szDayName)
    {
        string[] szWeekdays = getOrderedWeekdays();
        for (byte iWeekday = 0; iWeekday < szWeekdays.Length; iWeekday++)
        {
            if (p_szDayName.CompareTo(szWeekdays[iWeekday]) == 0)
            {
                return iWeekday;
            }
        }
        return 0;
    }

    private String[] getShortNamesForWeekdays()
    {
        string[] szWeekdays = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        return szWeekdays;
    }

    private String[] getOrderedWeekdays()
    {
        string[] szWeekdays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        return szWeekdays;
    }

    private byte getTodaysDayOfTheMonth()
    {
        return (byte)DateTime.Now.Day;
    }

    private int getLastMonthInteger()
    {
        return DateTime.DaysInMonth(Int32.Parse(objCalendar.InitialDisplayYear), getIntMonth(objCalendar.InitialDisplayMonth));
    }

    private int getIntMonth(string p_szMonth)
    {
        p_szMonth = p_szMonth.ToUpper();
        switch (p_szMonth)
        {
            case "JAN":
                return 1;
            case "FEB":
                return 2;
            case "MAR":
                return 3;
            case "APR":
                return 4;
            case "MAY":
                return 5;
            case "JUN":
                return 6;
            case "JUL":
                return 7;
            case "AUG":
                return 8;
            case "SEP":
                return 9;
            case "OCT":
                return 10;
            case "NOV":
                return 11;
            case "DEC":
                return 12;
            default:
                return 1;
        }
        return 1;
    }

    protected void Link1_Click(object sender, EventArgs e)
    {
        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false)
        {
            if (extddlReferenceFacility.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "checkForTestFacility();", true);
            }
        }

        // day view starts
        Session["FROM"] = null;
        LinkButton objLink = (LinkButton)sender;
        Label1.Text = "Refreshed at " + DateTime.Now.ToString() + " " + " pressed by " + objLink.CommandArgument;

        string[] szDate = null;
        szDate = objLink.CommandArgument.Split('_');

        string szYear = szDate[0];
        string szMonth = szDate[1];
        string szBogus = szDate[2];
        string szDay = szDate[3];

        DateTime objSource = new DateTime(Int32.Parse(szYear), getIntMonth(szMonth), Int32.Parse(szDay));
        //lblCurrentDate.Text = String.Format("{0:dddd, MMMM d, yyyy}", objSource);
        string szScheduleDay = String.Format("{0:MM/dd/yyyy}", objSource); // MMDDYY format
        if (extddlReferringFacility.Visible == false)
        {
            GetCalenderDayAppointments(szScheduleDay, txtCompanyID.Text);
        }
        else
        {
            GetCalenderDayAppointments(szScheduleDay, extddlReferringFacility.Text);
        }
    }

    //protected void ddlInterval_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlInterval.SelectedIndex >= 0)
    //        {
    //            string strSelectedDate = Session["AppointmentDate"].ToString();

    //            if (strSelectedDate != "")
    //            {

    //                if (extddlReferringFacility.Visible == false)
    //                {
    //                    GetCalenderDayAppointments(Convert.ToDateTime(strSelectedDate).ToString("MM/dd/yyyy"), txtCompanyID.Text);
    //                }
    //                else
    //                {
    //                    GetCalenderDayAppointments(Convert.ToDateTime(strSelectedDate).ToString("MM/dd/yyyy"), extddlReferringFacility.Text);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("~/Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    private void GetCalenderDayAppointments(string p_szScheduleDay, string referralid)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        Session["INTERVAL"] = ddlInterval.Text.Substring(2, 2);
        lblCurrentDate.Text = String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(p_szScheduleDay));
        if (Session["AppointmentDate"] == null || Session["AppointmentDate"] == "" || Session["AppointmentDate"].ToString() != p_szScheduleDay)
        {
            Session["AppointmentDate"] = p_szScheduleDay;
        }
        if (Session["TestFacilityID"] == null || Session["TestFacilityID"] == "" || Session["TestFacilityID"].ToString() != referralid)
        {
            Session["TestFacilityID"] = referralid;
        }
        string szScheduleDay = p_szScheduleDay;

        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        SqlCommand sqlCmd;
        SqlDataReader dr = null;
        ArrayList _arrayList = new ArrayList();
        try
        {

            // start Code to fetch room names

            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_ROOM_NAMES_FOR_DAY_VIEW", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            // sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", "CO00023");
            sqlCmd.Parameters.AddWithValue("@DT_DATE", Convert.ToDateTime(szScheduleDay).ToString("MM/dd/yyyy"));
            sqlCmd.Parameters.AddWithValue("@I_INTERVAL", ddlInterval.SelectedValue);
            sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_ID", referralid);
            dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                // for (int i = 0; i < dr.FieldCount; i++) 
                //{
                _arrayList.Add(dr[0]);
                // }
            }
            dr.Close();
            sqlCon.Close();

            //end  Code to fetch room names

            if (canCopy == "1")
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("GET_ROOM_DETAILS_FINAL_FOR_OUTSCHEDULE_PATIENT", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID","CO00023");
                sqlCmd.Parameters.AddWithValue("@DT_DATE", Convert.ToDateTime(szScheduleDay).ToString("MM/dd/yyyy"));
                sqlCmd.Parameters.AddWithValue("@I_INTERVAL", ddlInterval.SelectedValue);
                sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_ID", referralid);
                sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                dr = sqlCmd.ExecuteReader();
            }
            else
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("GET_ROOM_DETAILS_FINAL", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID","CO00023");
                sqlCmd.Parameters.AddWithValue("@DT_DATE", Convert.ToDateTime(szScheduleDay).ToString("MM/dd/yyyy"));
                sqlCmd.Parameters.AddWithValue("@I_INTERVAL", ddlInterval.SelectedValue);
                sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_ID", referralid);
                sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                dr = sqlCmd.ExecuteReader();
            }
            Label1.Text = "<table width='100%'>";



            // adding arrayList values to table

            Label1.Text = Label1.Text + "<tr>";
            Label1.Text = Label1.Text + "<td style='text-align:center;font-family:Times New Roman;font-weight:bold;font-size:12px;width:80px;align:left'>&nbsp;";
            Label1.Text = Label1.Text + "</td>";

            for (int icount = 0; icount < _arrayList.Count; icount++)
            {
                Label1.Text = Label1.Text + "<td style='width:" + ((100 / _arrayList.Count) - 2) + "%; text-align:center;'>";
                //Label1.Text = Label1.Text + "<div style='text-align:center;font-size:10px;background-color:#FFFFD5;border-bottom: gray 1px solid;width:100%;height:20px;'>";
                Label1.Text = Label1.Text + _arrayList[icount].ToString();
                //Label1.Text = Label1.Text + "</div>";
                Label1.Text = Label1.Text + "</td>";
            }

            Label1.Text = Label1.Text + "</tr>";
            //
            // <tr>
            // <td>
            // &nbsp;
            //</td>
            // loop all the rooms



            // loop ends

            //
            while (dr.Read())
            {
                Label1.Text = Label1.Text + "<tr>";
                //#FFFFEA
                Label1.Text = Label1.Text + "<td style='text-align:center;font-family:Times New Roman;font-weight:bold;font-size:12px;background-color:#E0E0E0;width:80px;align:left'>";
                //                Label1.Text = Label1.Text + "<div style='text-align:center;font-family:Times New Roman;font-weight:bold;font-size:14px;background-color:#E0E0E0;width:100%;height:100%;'>";
                Label1.Text = Label1.Text + dr[1];
                //                Label1.Text = Label1.Text + "</div>";
                Label1.Text = Label1.Text + "</td>";

                Label1.Text = Label1.Text + "<td width='90%' style='background-color:#FFFFEA;' colspan='" + _arrayList.Count.ToString() + "'  >";
                Label1.Text = Label1.Text + "<div style='text-align:center;font-size:12px;background-color:#FFFFD5;border-bottom: gray 1px solid;width:100%;height:100%;'>";

                // all columns will go here
                Label1.Text = Label1.Text + "<table width='100%'><tr>";

                for (int i = 1; i <= _arrayList.Count; i++) // counter for number of rooms...
                {
                    try
                    {
                        Label1.Text = Label1.Text + "<td style='width:" + (100 / _arrayList.Count) + "%; text-align:center;'>";
                        Label1.Text = Label1.Text + dr[i + 1];
                        Label1.Text = Label1.Text + "</td>";
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
                        Label1.Text = Label1.Text + "</td>";
                        break;
                    }
                }
                Label1.Text = Label1.Text + "</tr></table>";
                Label1.Text = Label1.Text + "</div>";

                Label1.Text = Label1.Text + "</td>";
                Label1.Text = Label1.Text + "</tr>";
            }
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        Label1.Text = Label1.Text + "</table>";
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
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
                    ddlHours.Items.Add(i.ToString());
                    ddlEndHours.Items.Add(i.ToString());
                }
                else
                {
                    ddlHours.Items.Add("0" + i.ToString());
                    ddlEndHours.Items.Add("0" + i.ToString());
                }
            }
            for (int i = 0; i <= 60; i++)
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void ClearValues()
    {
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
        Session["SZ_CASE_ID"] = "";
        extddlCaseType.Text = "NA";
        extddlInsuranceCompany.Text = "NA";
        txtNotes.Text = "";
        ddlTestNames.Items.Clear();
        hdnObj.Value = "";
        hdnEventDeleteDate.Value = "";
        hdnEventId.Value = "";

        ddlEndHours.Items.Clear();
        ddlEndMinutes.Items.Clear();
        ddlEndTime.Items.Clear();
        ddlHours.Items.Clear();
        ddlMinutes.Items.Clear();
        ddlTime.Items.Clear();

        grdPatientList.DataSource = null;
        grdPatientList.DataBind();

        lblMsg.Text = "";
        txtPatientFirstName.Text = "";
        txtPatientLastName.Text = "";
        txtRefChartNumber.Text = "";
        ModalPopupExtender.Hide();
    }

    #endregion

    #region "Allow user to add only single visit on date for patient"

    private string check_appointment()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        string _isExit = "";
        try
        {
            if (hdnObj.Value != "")
            {
                //changes by avinash

                string szProcedurecode = "";
                for (int iGrdRow = 0; iGrdRow < grdProcedureCode.Items.Count; iGrdRow++)
                {
                    CheckBox chkTemp = (CheckBox)grdProcedureCode.Items[iGrdRow].FindControl("chkSelect");
                    if (chkTemp.Checked == true)
                    {
                        if (szProcedurecode == "")
                            szProcedurecode = grdProcedureCode.Items[iGrdRow].Cells[1].Text;
                        else
                            szProcedurecode = szProcedurecode + "," + grdProcedureCode.Items[iGrdRow].Cells[1].Text;

                    }
                }

                string sz_datetime = hdnObj.Value;
                string sz_date = hdnObj.Value;
                sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));

                ArrayList objarr = new ArrayList();
                objarr.Add(txtCaseID.Text);  // CASE ID
                objarr.Add(txtCompanyID.Text); // COMPANY ID
                objarr.Add(sz_datetime.Substring(sz_datetime.IndexOf("!") + 1, 20)); // ROOM ID
                objarr.Add(sz_date); // DATE
                objarr.Add(szProcedurecode);
                objarr.Add(txtPatientID.Text);
                _isExit = _bill_Sys_PatientBO.Check_Appointment(objarr);
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
        return _isExit;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string check_appointment_for_period()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        string _isExit = "";
        try
        {
            if (hdnObj.Value != "")
            {
                string sz_datetime = hdnObj.Value;
                string sz_date = hdnObj.Value;
                sz_date = sz_date.Substring(0, sz_date.IndexOf(" "));

                ArrayList objarr = new ArrayList();
                objarr.Add(txtCaseID.Text);  // CASE ID
                objarr.Add(txtCompanyID.Text); // COMPANY ID
                objarr.Add(sz_datetime.Substring(sz_datetime.IndexOf("!") + 1, 20)); // ROOM ID
                objarr.Add(sz_date); // DATE
                objarr.Add(ddlHours.SelectedValue + "." + ddlMinutes.SelectedValue); // Start Time
                objarr.Add(ddlEndHours.SelectedValue + "." + ddlEndMinutes.SelectedValue); // End Time
                objarr.Add(ddlTime.SelectedValue); // Start Type
                _isExit = _bill_Sys_PatientBO.Check_Appointment_For_Period(objarr);
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
        return _isExit;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    #endregion

    protected void btnLoadCalendar_Click(object sender, EventArgs e)
    {
        Panel1.Controls.Clear();

        DateTime dtTemp = new DateTime(Convert.ToInt32(ddlYearList.SelectedValue), Convert.ToInt32(ddlMonthList.SelectedValue), 1);

        Session["PRV_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(-1).ToString("MM/dd/yyyy");
        Session["CUR_MONTH"] = Convert.ToDateTime(dtTemp).ToString("MM/dd/yyyy");
        Session["NEXT_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(1).ToString("MM/dd/yyyy");
        LoadCalendarAccordingToYearAndMonth();
    }


    protected void btnGo_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {

            if (txtEnteredDate.Text != "")
            {
                Panel1.Controls.Clear();

                DateTime dtTemp = Convert.ToDateTime(txtEnteredDate.Text);

                Session["PRV_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(-1).ToString("MM/dd/yyyy");
                Session["CUR_MONTH"] = Convert.ToDateTime(dtTemp).ToString("MM/dd/yyyy");
                Session["NEXT_MONTH"] = Convert.ToDateTime(dtTemp).AddMonths(1).ToString("MM/dd/yyyy");

                Session["FROM"] = "FROM DATE";
                txtGetDay.Value = dtTemp.ToString("MMM").ToUpper().ToString() + "_" + dtTemp.Day.ToString();

                LoadCalendarAccordingToYearAndMonth();

                string szScheduleDay = String.Format("{0:MM/dd/yyyy}", dtTemp); // MMDDYY format
                if (extddlReferringFacility.Visible == false)
                {
                    GetCalenderDayAppointments(szScheduleDay, txtCompanyID.Text);
                }
                else
                {
                    GetCalenderDayAppointments(szScheduleDay, extddlReferringFacility.Text);
                }
            }
            else
            {
                string szScheduleDay = String.Format("{0:MM/dd/yyyy}", DateTime.Now.Date.ToShortDateString()); // MMDDYY format
                if (extddlReferringFacility.Visible == false)
                {
                    GetCalenderDayAppointments(szScheduleDay, txtCompanyID.Text);
                }
                else
                {
                    GetCalenderDayAppointments(szScheduleDay, extddlReferringFacility.Text);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #region "Patient Desk"

    protected void lnkPatientDesk_Click(object sender, EventArgs e)
    {
        if (txtCaseID.Text != "")
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

            Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
            _bill_Sys_Case.SZ_CASE_ID = txtCaseID.Text;
            Session["CASEINFO"] = _bill_Sys_Case;
            Session["QStrCaseID"] = txtCaseID.Text;
            Session["Case_ID"] = txtCaseID.Text;
            Session["Archived"] = "0";
            Session["QStrCID"] = txtCaseID.Text;
            Session["SelectedID"] = txtCaseID.Text;

            ///////////

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_SysPatientDesk.aspx?Flag=true';</script>");
            Response.Redirect("~/Bill_SysPatientDesk.aspx?Flag=true", true);
        }
    }

    #endregion

}

#region "Helper Class"

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

class Calendar_DAO
{
    private string szInitDisplayMonth = null;
    private string szControlIDPrefix = null;
    private string szInitDisplayYear = null;

    public string InitialDisplayYear
    {
        get
        {
            return szInitDisplayYear;
        }
        set
        {
            szInitDisplayYear = value;
        }
    }

    public string InitialDisplayMonth
    {
        get
        {
            return szInitDisplayMonth;
        }
        set
        {
            szInitDisplayMonth = value;
        }
    }

    public string ControlIDPrefix
    {
        get
        {
            return szControlIDPrefix;
        }
        set
        {
            szControlIDPrefix = value;
        }
    }
}

#endregion
