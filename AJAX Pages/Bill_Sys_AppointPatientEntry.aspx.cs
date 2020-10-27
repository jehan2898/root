using AjaxControlToolkit;
using Componend;
using ExtendedDropDownList;
using Scheduling;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Bill_Sys_AppointPatientEntry : Page, IRequiresSessionState
{

    private Bill_Sys_Schedular _obj;

    private Calendar_DAO objCalendar;

    private bool blnWeekShortNames = true;

    private string szDateColor_NA = "#ff6347";

    private string szDateColor_TODAY = "#FFFF80";

    private string UserID;

    private DataSet ds;

    private Patient_TVBO _patient_TVBO;

    private string strcompanyid;

    private PopupBO _popupBO;

    private DAO_NOTES_EO _DAO_NOTES_EO;

    private DAO_NOTES_BO _DAO_NOTES_BO;

    private string canCopy = "";

    public Bill_Sys_AppointPatientEntry()
    {
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._obj = new Bill_Sys_Schedular();
            decimal num = new decimal(30, 0, 0, false, 2);
            if (this.extddlReferringFacility.Visible)
            {
                this.grdScheduleReport.DataSource = this._obj.GET_EVENT_DETAIL(this.txtCompanyID.Text, DateTime.Today, num, this.extddlReferringFacility.Text);
            }
            else
            {
                this.grdScheduleReport.DataSource = this._obj.GET_EVENT_DETAIL(this.txtCompanyID.Text, DateTime.Today, num, this.txtCompanyID.Text);
            }
            this.grdScheduleReport.DataBind();
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
                if (i <= 9)
                {
                    this.ddlHours.Items.Add(string.Concat("0", i.ToString()));
                    this.ddlEndHours.Items.Add(string.Concat("0", i.ToString()));
                }
                else
                {
                    this.ddlHours.Items.Add(i.ToString());
                    this.ddlEndHours.Items.Add(i.ToString());
                }
            }
            for (int j = 0; j <= 60; j++)
            {
                if (j <= 9)
                {
                    this.ddlMinutes.Items.Add(string.Concat("0", j.ToString()));
                    this.ddlEndMinutes.Items.Add(string.Concat("0", j.ToString()));
                }
                else
                {
                    this.ddlMinutes.Items.Add(j.ToString());
                    this.ddlEndMinutes.Items.Add(j.ToString());
                }
            }
            this.ddlTime.Items.Add("AM");
            this.ddlTime.Items.Add("PM");
            this.ddlEndTime.Items.Add("AM");
            this.ddlEndTime.Items.Add("PM");
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

    private void BindYearDropDown(int p_iYear)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ddlYearList.Items.Clear();
            for (int i = p_iYear - 10; i <= p_iYear + 1; i++)
            {
                this.ddlYearList.Items.Add(i.ToString());
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
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string appointPatientDetail = "default";
        try
        {
            try
            {
                if (this.hdnObj.Value != "")
                {
                    if ((new Bill_Sys_BillingCompanyDetails_BO()).GetRefDocID(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString()) == "")
                    {
                        this.btnAddPatient.Visible = true;
                    }
                    else
                    {
                        this.btnAddPatient.Visible = false;
                    }
                    PopupBO popupBO = new PopupBO();
                    if (this.Session["CASE_OBJECT"] == null)
                    {
                        Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject()
                        {
                            SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID
                        };
                        this.Session["CASE_OBJECT"] = billSysCaseObject;
                    }
                    else
                    {
                        ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID = popupBO.GetCompanyID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                    }
                    this.extddlDoctor.Flag_ID = this.txtCompanyID.Text.ToString();
                    this.extddlInsuranceCompany.Flag_ID = this.txtCompanyID.Text.ToString();
                    this.extddlCaseStatus.Flag_ID = this.txtCompanyID.Text.ToString();
                    this.extddlCaseType.Flag_ID = this.txtCompanyID.Text.ToString();
                    this.extddlMedicalOffice.Flag_ID = this.txtCompanyID.Text.ToString();
                    this.extddlReferenceFacility.Flag_ID = this.txtCompanyID.Text.ToString();
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        this.extddlTransport.Flag_ID = this.extddlReferenceFacility.Text;
                    }
                    else
                    {
                        this.extddlTransport.Flag_ID = this.txtCompanyID.Text;
                    }
                    this.BindTimeControl();
                    string value = this.hdnObj.Value;
                    if (value.Substring(value.IndexOf(" ") + 2, 1) != ".")
                    {
                        this.ddlHours.SelectedValue = value.Substring(value.IndexOf(" ") + 1, 2);
                    }
                    else
                    {
                        this.ddlHours.SelectedValue = value.Substring(value.IndexOf(" "), 2).Replace(" ", "0");
                    }
                    this.ddlMinutes.SelectedValue = value.Substring(value.IndexOf(".") + 1, 2);
                    this.ddlTime.SelectedValue = value.Substring(value.IndexOf("|") + 1, 2);
                    this.extddlReferenceFacility.Text = this.Session["TestFacilityID"].ToString();
                    int num = Convert.ToInt32(this.ddlMinutes.SelectedValue) + Convert.ToInt32(this.Session["INTERVAL"].ToString());
                    int num1 = Convert.ToInt32(this.ddlHours.SelectedValue);
                    string selectedValue = this.ddlTime.SelectedValue;
                    if (num >= 60)
                    {
                        num = num - 60;
                        num1++;
                        if (num1 > 12)
                        {
                            num1 = num1 - 12;
                            if (this.ddlHours.SelectedValue != "12")
                            {
                                if (selectedValue == "AM")
                                {
                                    selectedValue = "PM";
                                }
                                else if (selectedValue == "PM")
                                {
                                    selectedValue = "AM";
                                }
                            }
                        }
                        else if (num1 == 12 && this.ddlHours.SelectedValue != "12")
                        {
                            if (selectedValue == "AM")
                            {
                                selectedValue = "PM";
                            }
                            else if (selectedValue == "PM")
                            {
                                selectedValue = "AM";
                            }
                        }
                    }
                    this.ddlEndHours.SelectedValue = num1.ToString().PadLeft(2, '0');
                    this.ddlEndMinutes.SelectedValue = num.ToString().PadLeft(2, '0');
                    this.ddlEndTime.SelectedValue = selectedValue.ToString();
                    if (this.Session["CASE_OBJECT"] != null)
                    {
                        popupBO = new PopupBO();
                        this.strcompanyid = popupBO.GetCompanyID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                    }
                    if (this.Session["Flag"] != null)
                    {
                        this._patient_TVBO = new Patient_TVBO();
                        this.Session["PatientDataList"] = this._patient_TVBO.GetSelectedPatientDataListNEW(this.strcompanyid, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID, this.UserID);
                        this.SearchPatientList();
                        this.btnClickSearch.Visible = false;
                    }
                    else if (this.Session["DataEntryFlag"] == null)
                    {
                        this._patient_TVBO = new Patient_TVBO();
                        this.Session["PatientDataList"] = this._patient_TVBO.GetPatientDataListNEW(this.txtCompanyID.Text, this.UserID);
                        this.txtPatientID.Text = "";
                        this.txtPatientFName.Text = "";
                        this.txtMI.Text = "";
                        this.txtPatientLName.Text = "";
                        this.txtPatientPhone.Text = "";
                        this.txtPatientAddress.Text = "";
                        this.txtState.Text = "";
                        this.txtBirthdate.Text = "";
                        this.txtPatientAge.Text = "";
                        this.txtSocialSecurityNumber.Text = "";
                        this.txtCaseID.Text = "";
                        this.extddlCaseType.Text = "NA";
                        this.extddlInsuranceCompany.Text = "NA";
                        this.extddlPatientState.Text = "NA";
                        this.extddlMedicalOffice.Text = "NA";
                        this.btnClickSearch.Visible = true;
                    }
                    else
                    {
                        this._patient_TVBO = new Patient_TVBO();
                        this.Session["PatientDataList"] = this._patient_TVBO.GetSelectedPatientDataListNEW(this.strcompanyid, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID, this.UserID);
                        this.SearchPatientList();
                        this.btnClickSearch.Visible = false;
                        this.Session["DataEntryFlag"] = null;
                    }
                    if (this.hdnOperationType.Value.ToString() != "TestFacilityUpdate")
                    {
                        this.LoadProcedureGrid();
                    }
                    else
                    {
                        this.LoadProcedureGrid();
                    }
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        this.extddlMedicalOffice.Visible = true;
                        this.extddlReferenceFacility.Visible = false;
                        this.lblTypetext.Visible = false;
                        this.ddlType.Visible = false;
                        this.lblTestFacility.Text = "Office Name";
                    }
                    this.extddlCaseStatus.Text = this.GetOpenCaseStatus();
                    if (value.IndexOf("~") > 0)
                    {
                        string str = "";
                        str = (value.IndexOf("^") <= 0 ? value.Substring(value.IndexOf("~") + 1, value.Length - (value.IndexOf("~") + 1)) : value.Substring(value.IndexOf("~") + 1, value.IndexOf("^") - (value.IndexOf("~") + 1)));
                        this._patient_TVBO = new Patient_TVBO();
                        if (this._patient_TVBO.getScheduleStatus(str))
                        {
                            this.lblMsg.Visible = true;
                            this.lblMsg.Text = "Visit Completed.";
                            this.lblMsg.ForeColor = Color.Red;
                            this.Session["VISIT_COMPLETED"] = "YES";
                        }
                        this.Session["SCHEDULEDID"] = str;
                        if (this.hdnOperationType.Value.ToString() != "TestFacilityUpdate")
                        {
                            this.LoadProcedureGrid();
                        }
                        else
                        {
                            this.ddlTestNames.Visible = false;
                            this.divProcedureCode.Visible = true;
                            this.SelectSavedProcedureCodes(str);
                        }
                        appointPatientDetail = this.GETAppointPatientDetail(Convert.ToInt32(str));
                        this.btnClickSearch.Visible = false;
                        this.tdSerach.Visible = false;
                        this.tdSerach.Height = "0px";
                    }
                    this.setControlAccordingOperation();
                    if (this.txtPatientFName.Text != "")
                    {
                        this.lnkPatientDesk.Visible = true;
                    }
                    else
                    {
                        this.lnkPatientDesk.Visible = false;
                    }
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.Trim().ToLower().ToString() == "referring office")
                    {
                        Bill_Sys_BillingCompanyDetails_BO billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                        string refDocID = billSysBillingCompanyDetailsBO.GetRefDocID(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                        if (refDocID != "")
                        {
                            this.extddlMedicalOffice.Text = refDocID;
                            this.extddlMedicalOffice.Enabled = false;
                            this.extddlDoctor.Procedure_Name = "SP_GET_REF_DOC";
                            this.extddlDoctor.Connection_Key = "Connection_String";
                            this.extddlDoctor.Selected_Text = "---Select---";
                            this.extddlDoctor.Flag_Key_Value = this.txtCompanyID.Text;
                            this.extddlDoctor.Flag_ID = refDocID;
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
        }
        finally
        {
            if (appointPatientDetail.ToLower().Equals("default") || appointPatientDetail.ToLower().Equals("success"))
            {
                this.ModalPopupExtender.Show();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAddPatient_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.txtPatientID.Text = "";
            this.txtPatientFName.Text = "";
            this.txtMI.Text = "";
            this.txtPatientLName.Text = "";
            this.txtPatientPhone.Text = "";
            this.txtPatientAddress.Text = "";
            this.txtBirthdate.Text = "";
            this.txtPatientAge.Text = "";
            this.txtSocialSecurityNumber.Text = "";
            this.txtCaseID.Text = "";
            this.extddlPatientState.Text = "NA";
            this.extddlCaseType.Text = "NA";
            this.extddlInsuranceCompany.Text = "NA";
            this.TextBox3.Text = "";
            this.txtPatientFName.ReadOnly = false;
            this.txtMI.ReadOnly = false;
            this.txtPatientLName.ReadOnly = false;
            this.txtPatientPhone.ReadOnly = false;
            this.txtPatientAddress.ReadOnly = false;
            this.extddlPatientState.Enabled = true;
            this.extddlInsuranceCompany.Enabled = true;
            this.extddlCaseType.Enabled = true;
            this.extddlMedicalOffice.Enabled = true;
            this.txtCaseID.Text = "";
            this.TextBox3.ReadOnly = false;
            this.lblSSN.Visible = false;
            this.txtSocialSecurityNumber.Visible = false;
            this.lblBirthdate.Visible = false;
            this.txtBirthdate.Visible = false;
            this.lblAge.Visible = false;
            this.txtPatientAge.Visible = false;
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1")
            {
                this.lblChartNumber.Visible = false;
                this.txtRefChartNumber.Visible = false;
            }
            else
            {
                this.lblChartNumber.Visible = true;
                this.txtRefChartNumber.Visible = true;
                string maxChartNumber = "";
                maxChartNumber = billSysPatientBO.GetMaxChartNumber(this.txtCompanyID.Text);
                if (maxChartNumber == "")
                {
                    this.txtRefChartNumber.Text = "1";
                }
                else
                {
                    this.txtRefChartNumber.Text = maxChartNumber;
                }
            }
            this.ModalPopupExtender.Show();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearValues();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender billSysCalender;
        try
        {
            try
            {
                if (this.hdnEventId.Value != "" && this.hdnEventDeleteDate.Value != "")
                {
                    Convert.ToDateTime(DateTime.Now.ToString());
                    string[] strArrays = this.hdnEventDeleteDate.Value.ToString().Split(new char[] { '|' });
                    string[] strArrays1 = this.hdnEventDeleteDate.Value.ToString().Split(new char[] { '-' });
                    string[] strArrays2 = strArrays[1].Split(new char[] { ' ' });
                    string[] str = new string[] { strArrays[0].ToString(), " ", strArrays2[0].Replace(".", ":"), ":00", strArrays1[0].Substring(strArrays1[0].Length - 2, 2).ToString() };
                    Convert.ToDateTime(string.Concat(str));
                    billSysCalender = new Bill_Sys_Calender();
                    int num = billSysCalender.Delete_Event(Convert.ToInt32(this.hdnEventId.Value.ToString()), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    DateTime dateTime = Convert.ToDateTime(this.lblCurrentDate.Text.Trim().ToString());
                    string[] strArrays3 = dateTime.ToString().Split(new char[] { '/' });
                    string[] str1 = new string[] { strArrays3[0], "/", strArrays3[1], "/", strArrays3[2].Substring(0, 4).ToString() };
                    string str2 = string.Concat(str1);
                    if (num <= 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnDelete, typeof(Button), "Msg", "alert('Can not delete appointment.')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnDelete, typeof(Button), "Msg", "alert('Appointment Deleted Successfully.')", true);
                    }
                    if (str2 != "")
                    {
                        if (this.extddlReferringFacility.Visible)
                        {
                            DateTime dateTime1 = Convert.ToDateTime(str2);
                            this.GetCalenderDayAppointments(dateTime1.ToString("MM/dd/yyyy"), this.extddlReferringFacility.Text);
                        }
                        else
                        {
                            DateTime dateTime2 = Convert.ToDateTime(str2);
                            this.GetCalenderDayAppointments(dateTime2.ToString("MM/dd/yyyy"), this.txtCompanyID.Text);
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
            
        }
        finally
        {
            billSysCalender = null;
            this.hdnEventId.Value = "";
            this.hdnEventDeleteDate.Value = "";
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDeleteEvent_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender billSysCalender;
        try
        {
            try
            {
                string value = this.hdnObj.Value;
                string str = "";
                str = (value.IndexOf("^") <= 0 ? value.Substring(value.IndexOf("~") + 1, value.Length - (value.IndexOf("~") + 1)) : value.Substring(value.IndexOf("~") + 1, value.IndexOf("^") - (value.IndexOf("~") + 1)));
                this.hdnEventId.Value = str;
                this.hdnEventDeleteDate.Value = value.Substring(0, value.IndexOf(" "));
                if (this.hdnEventId.Value != "" && this.hdnEventDeleteDate.Value != "")
                {
                    billSysCalender = new Bill_Sys_Calender();
                    int num = billSysCalender.Delete_Event(Convert.ToInt32(this.hdnEventId.Value.ToString()), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    string value1 = this.hdnEventDeleteDate.Value;
                    if (num <= 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnDelete, typeof(Button), "Msg", "alert('Can not delete appointment.')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnDelete, typeof(Button), "Msg", "alert('Appointment Deleted Successfully.')", true);
                    }
                    if (value1 != "")
                    {
                        if (this.extddlReferringFacility.Visible)
                        {
                            DateTime dateTime = Convert.ToDateTime(value1);
                            this.GetCalenderDayAppointments(dateTime.ToString("MM/dd/yyyy"), this.extddlReferringFacility.Text);
                        }
                        else
                        {
                            DateTime dateTime1 = Convert.ToDateTime(value1);
                            this.GetCalenderDayAppointments(dateTime1.ToString("MM/dd/yyyy"), this.txtCompanyID.Text);
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
        }
        finally
        {
            billSysCalender = null;
            this.hdnEventId.Value = "";
            this.hdnEventDeleteDate.Value = "";
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.txtEnteredDate.Text == "")
            {
                DateTime date = DateTime.Now.Date;
                string str = string.Format("{0:MM/dd/yyyy}", date.ToShortDateString());
                if (this.extddlReferringFacility.Visible)
                {
                    this.GetCalenderDayAppointments(str, this.extddlReferringFacility.Text);
                }
                else
                {
                    this.GetCalenderDayAppointments(str, this.txtCompanyID.Text);
                }
            }
            else
            {
                this.Panel1.Controls.Clear();
                DateTime dateTime = Convert.ToDateTime(this.txtEnteredDate.Text);
                HttpSessionState session = this.Session;
                DateTime dateTime1 = Convert.ToDateTime(dateTime).AddMonths(-1);
                session["PRV_MONTH"] = dateTime1.ToString("MM/dd/yyyy");
                HttpSessionState httpSessionStates = this.Session;
                DateTime dateTime2 = Convert.ToDateTime(dateTime);
                httpSessionStates["CUR_MONTH"] = dateTime2.ToString("MM/dd/yyyy");
                HttpSessionState session1 = this.Session;
                DateTime dateTime3 = Convert.ToDateTime(dateTime).AddMonths(1);
                session1["NEXT_MONTH"] = dateTime3.ToString("MM/dd/yyyy");
                this.Session["FROM"] = "FROM DATE";
                HtmlInputHidden htmlInputHidden = this.txtGetDay;
                string str1 = dateTime.ToString("MMM").ToUpper().ToString();
                int day = dateTime.Day;
                htmlInputHidden.Value = string.Concat(str1, "_", day.ToString());
                this.LoadCalendarAccordingToYearAndMonth();
                string str2 = string.Format("{0:MM/dd/yyyy}", dateTime);
                if (this.extddlReferringFacility.Visible)
                {
                    this.GetCalenderDayAppointments(str2, this.extddlReferringFacility.Text);
                }
                else
                {
                    this.GetCalenderDayAppointments(str2, this.txtCompanyID.Text);
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

    protected void btnLoadCalendar_Click(object sender, EventArgs e)
    {
        this.Panel1.Controls.Clear();
        DateTime dateTime = new DateTime(Convert.ToInt32(this.ddlYearList.SelectedValue), Convert.ToInt32(this.ddlMonthList.SelectedValue), 1);
        HttpSessionState session = this.Session;
        DateTime dateTime1 = Convert.ToDateTime(dateTime).AddMonths(-1);
        session["PRV_MONTH"] = dateTime1.ToString("MM/dd/yyyy");
        HttpSessionState str = this.Session;
        DateTime dateTime2 = Convert.ToDateTime(dateTime);
        str["CUR_MONTH"] = dateTime2.ToString("MM/dd/yyyy");
        HttpSessionState httpSessionStates = this.Session;
        DateTime dateTime3 = Convert.ToDateTime(dateTime).AddMonths(1);
        httpSessionStates["NEXT_MONTH"] = dateTime3.ToString("MM/dd/yyyy");
        this.LoadCalendarAccordingToYearAndMonth();
    }

    protected void btnLoadPageData_Click(object sender, EventArgs e)
    {
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
    }

    protected void btnPECancel_Click(object sender, EventArgs e)
    {
    }

    protected void btnPEOk_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        calOperation _calOperation = new calOperation();
        calPatientEO _calPatientEO = new calPatientEO();
        calEvent _calEvent = new calEvent();
        CalendarTransaction calendarTransaction = new CalendarTransaction();
        calResult _calResult = new calResult();
        ArrayList arrayLists = new ArrayList();
        ArrayList arrayLists1 = new ArrayList();
        string text = "";
        Billing_Sys_ManageNotesBO billingSysManageNotesBO = new Billing_Sys_ManageNotesBO();
        try
        {
            if (!this.txtPatientFName.ReadOnly)
            {
                _calPatientEO = this.create_calPatientEO();
                this.txtPatientID.Text = billingSysManageNotesBO.GetPatientLatestID();
                _calOperation.add_patient = true;
            }
            if (this.hdnObj.Value.ToString() != "")
            {
                string str = this.check_appointment();
                if (str == "")
                {
                    string[] strArrays = this.hdnObj.Value.ToString().Split(new char[] { ' ' });
                    Convert.ToDateTime(strArrays[0]);
                    if (this.hdnObj.Value.ToString() != "")
                    {
                        string text1 = "";
                        string str1 = this.hdnObj.Value.ToString();
                        if (str1.IndexOf("~") > 0)
                        {
                            if (str1.IndexOf("^") <= 0)
                            {
                                str1.Substring(str1.IndexOf("~") + 1, str1.Length - (str1.IndexOf("~") + 1));
                            }
                            else
                            {
                                str1.Substring(str1.IndexOf("~") + 1, str1.IndexOf("^") - (str1.IndexOf("~") + 1));
                            }
                            Bill_Sys_ReferalEvent billSysReferalEvent = new Bill_Sys_ReferalEvent();
                            ArrayList arrayLists2 = new ArrayList();
                            arrayLists2.Add(this.extddlDoctor.Text);
                            arrayLists2.Add(this.txtPatientID.Text);
                            arrayLists2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            text1 = billSysReferalEvent.AddDoctor(arrayLists2);
                            for (int i = 0; i < this.grdProcedureCode.Items.Count; i++)
                            {
                                if (((CheckBox)this.grdProcedureCode.Items[i].FindControl("chkSelect")).Checked)
                                {
                                    text = this.grdProcedureCode.Items[i].Cells[1].Text;
                                    arrayLists2 = new ArrayList();
                                    arrayLists2.Add(text1);
                                    arrayLists2.Add(text);
                                    arrayLists2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    arrayLists2.Add(text);
                                    billSysReferalEvent.AddDoctorAmount(arrayLists2);
                                    arrayLists2 = new ArrayList();
                                    arrayLists2.Add(this.txtPatientID.Text);
                                    arrayLists2.Add(text1);
                                    arrayLists2.Add(this.hdnObj.Value.ToString().Substring(0, this.hdnObj.Value.ToString().IndexOf(" ")));
                                    arrayLists2.Add(text);
                                    arrayLists2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    arrayLists2.Add(this.ddlType.SelectedValue);
                                    billSysReferalEvent.AddPatientProc(arrayLists2);
                                }
                            }
                        }
                        else if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            text1 = this.extddlDoctor.Text;
                        }
                        else
                        {
                            Bill_Sys_ReferalEvent billSysReferalEvent1 = new Bill_Sys_ReferalEvent();
                            ArrayList arrayLists3 = new ArrayList();
                            text1 = this.extddlDoctor.Text;
                            for (int j = 0; j < this.grdProcedureCode.Items.Count; j++)
                            {
                                if (((CheckBox)this.grdProcedureCode.Items[j].FindControl("chkSelect")).Checked)
                                {
                                    text = this.grdProcedureCode.Items[j].Cells[1].Text;
                                    calDoctorAmount _calDoctorAmount = new calDoctorAmount()
                                    {
                                        SZ_DOCTOR_ID = text1,
                                        SZ_PROCEDURE_ID = text,
                                        SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID,
                                        SZ_TYPE_CODE_ID = text
                                    };
                                    arrayLists.Add(_calDoctorAmount);
                                    _calOperation.bt_add_doctor_amount = true;
                                }
                            }
                        }
                        string str2 = this.hdnObj.Value.ToString();
                        str2 = str2.Substring(0, str2.IndexOf(" "));
                        Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
                        ArrayList arrayLists4 = new ArrayList();
                        _calEvent.SZ_PATIENT_ID = this.txtPatientID.Text;
                        _calEvent.DT_EVENT_DATE = str2;
                        _calEvent.DT_EVENT_TIME = string.Concat(this.ddlHours.SelectedValue.ToString(), ".", this.ddlMinutes.SelectedValue.ToString());
                        _calEvent.SZ_EVENT_NOTES = this.txtNotes.Text;
                        _calEvent.SZ_DOCTOR_ID = text1;
                        text = "";
                        int num = 0;
                        if (num < this.grdProcedureCode.Items.Count)
                        {
                            text = this.grdProcedureCode.Items[num].Cells[1].Text;
                        }
                        if (text == null)
                        {
                            _calEvent.DT_EVENT_DATE = str2;
                        }
                        else
                        {
                            _calEvent.SZ_TYPE_CODE_ID = text;
                        }
                        _calEvent.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _calEvent.DT_EVENT_TIME_TYPE = this.ddlTime.SelectedValue;
                        _calEvent.DT_EVENT_END_TIME = string.Concat(this.ddlEndHours.SelectedValue.ToString(), ".", this.ddlEndMinutes.SelectedValue.ToString());
                        _calEvent.DT_EVENT_END_TIME_TYPE = this.ddlEndTime.SelectedValue;
                        _calEvent.SZ_REFERENCE_ID = this.extddlReferenceFacility.Text;
                        _calEvent.BT_STATUS = "0";
                        if (!this.chkTransportation.Checked)
                        {
                            _calEvent.BT_TRANSPORTATION = "0";
                        }
                        else
                        {
                            _calEvent.BT_TRANSPORTATION = "1";
                        }
                        _calEvent.DT_EVENT_DATE = str2;
                        if (this.chkTransportation.Checked)
                        {
                            _calEvent.I_TRANSPORTATION_COMPANY = this.extddlTransport.Text;
                        }
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            _calEvent.SZ_OFFICE_ID = this.extddlMedicalOffice.Text;
                        }
                        for (int k = 0; k < this.grdProcedureCode.Items.Count; k++)
                        {
                            if (((CheckBox)this.grdProcedureCode.Items[k].FindControl("chkSelect")).Checked)
                            {
                                text = this.grdProcedureCode.Items[k].Cells[1].Text;
                                calProcedureCodeEO _calProcedureCodeEO = new calProcedureCodeEO()
                                {
                                    SZ_PROC_CODE = text,
                                    I_EVENT_ID = "",
                                    I_STATUS = "0"
                                };
                                arrayLists1.Add(_calProcedureCodeEO);
                            }
                        }
                        _calResult = calendarTransaction.fnc_SaveAppointment(_calOperation, _calPatientEO, arrayLists, _calEvent, arrayLists1, this.txtUserId.Text);
                        if (_calResult.msg_code == "SUCCESS")
                        {
                            this._DAO_NOTES_EO = new DAO_NOTES_EO()
                            {
                                SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED",
                                SZ_ACTIVITY_DESC = string.Concat("Date : ", str1.Substring(0, str1.IndexOf("!")))
                            };
                            this._DAO_NOTES_BO = new DAO_NOTES_BO();
                            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            this._DAO_NOTES_EO.SZ_CASE_ID = this.txtCaseID.Text;
                            this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        }
                    }
                    if (this.hdnObj.Value.ToString().IndexOf("~") <= 0)
                    {
                        this.Session["PopUp"] = "True";
                    }
                    else
                    {
                        this.Session["PopUp"] = null;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", string.Concat("alert('", str, "')"), true);
                    this.ModalPopupExtender.Show();
                    return;
                }
            }
            string[] strArrays1 = this.hdnObj.Value.ToString().Split(new char[] { '/' });
            string[] strArrays2 = new string[] { strArrays1[0], "/", strArrays1[1], "/", strArrays1[2].Substring(0, 4).ToString() };
            string str3 = string.Concat(strArrays2);
            if (str3 != "")
            {
                if (this.extddlReferringFacility.Visible)
                {
                    DateTime dateTime = Convert.ToDateTime(str3);
                    this.GetCalenderDayAppointments(dateTime.ToString("MM/dd/yyyy"), this.extddlReferringFacility.Text);
                }
                else
                {
                    DateTime dateTime1 = Convert.ToDateTime(str3);
                    this.GetCalenderDayAppointments(dateTime1.ToString("MM/dd/yyyy"), this.txtCompanyID.Text);
                }
                if (this.txtPatientExistMsg.Value != "")
                {
                    DateTime dateTime2 = Convert.ToDateTime(str3);
                    string str4 = string.Format("{0:MM/dd/yyyy}", dateTime2.ToString("MM/dd/yyyy"));
                    if (this.extddlReferringFacility.Visible)
                    {
                        this.GetCalenderDayAppointments(str4, this.extddlReferringFacility.Text);
                    }
                    else
                    {
                        this.GetCalenderDayAppointments(str4, this.txtCompanyID.Text);
                    }
                }
            }
            if (_calResult.msg_code != "SUCCESS")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", string.Concat("alert('", _calResult.msg, "')"), true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Appointment details saved successfully.')", true);
            }
            this.ClearValues();
            this.txtPatientExistMsg.Value = "";
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
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        OutSchedulePatientDAO outSchedulePatientDAO = new OutSchedulePatientDAO();
        string caseIdForDocumentPath = "";
        string str = "";
        calOperation _calOperation = new calOperation();
        calPatientEO _calPatientEO = new calPatientEO();
        calEvent _calEvent = new calEvent();
        CalendarTransaction calendarTransaction = new CalendarTransaction();
        calResult _calResult = new calResult();
        ArrayList arrayLists = new ArrayList();
        ArrayList arrayLists1 = new ArrayList();
        string text = "";
        Billing_Sys_ManageNotesBO billingSysManageNotesBO = new Billing_Sys_ManageNotesBO();
        try
        {
            if (this.txtPatientFName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Patient First Name should not be Empty.')", true);
                this.ModalPopupExtender.Show();
            }
            else if (this.txtPatientLName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Patient Last name should not be Empty.')", true);
                this.ModalPopupExtender.Show();
            }
            else if (this.extddlReferenceFacility.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Please Select any one Reference Facility.')", true);
                this.ModalPopupExtender.Show();
            }
            else if (this.extddlDoctor.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Please Select any one Referring Doctor.')", true);
                this.ModalPopupExtender.Show();
            }
            else if (!this.chkTransportation.Checked || !(this.extddlTransport.Text == "NA"))
            {
                if (!this.txtPatientFName.ReadOnly)
                {
                    ArrayList arrayLists2 = new ArrayList();
                    arrayLists2.Add(this.txtPatientFName.Text);
                    arrayLists2.Add(this.txtPatientLName.Text);
                    arrayLists2.Add(null);
                    arrayLists2.Add(this.extddlCaseType.Text);
                    if (this.txtBirthdate.Text == "")
                    {
                        arrayLists2.Add(null);
                    }
                    else
                    {
                        arrayLists2.Add(this.txtBirthdate.Text);
                    }
                    arrayLists2.Add(this.txtCompanyID.Text);
                    arrayLists2.Add("existpatient");
                    string str1 = (new Bill_Sys_PatientBO()).CheckPatientExists(arrayLists2);
                    if (!(str1 != "") || !(this.txtPatientExistMsg.Value == ""))
                    {
                        _calPatientEO = this.create_calPatientEO();
                        this.txtPatientID.Text = billingSysManageNotesBO.GetPatientLatestID();
                        _calOperation.add_patient = true;
                    }
                    else
                    {
                        this.msgPatientExists.InnerHtml = str1;
                        this.ModalPopupExtender.Show();
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "javascript:openExistsPage();", true);
                        return;
                    }
                }
                if (this.hdnObj.Value.ToString() != "")
                {
                    string str2 = this.check_appointment();
                    if (str2 != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", string.Concat("alert('", str2, "')"), true);
                        this.ModalPopupExtender.Show();
                        return;
                    }
                    else if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(this.check_appointment_for_period() != ""))
                    {
                        string[] strArrays = this.hdnObj.Value.ToString().Split(new char[] { ' ' });
                        Convert.ToDateTime(strArrays[0]);
                        if (this.hdnObj.Value.ToString() != "")
                        {
                            string text1 = "";
                            string str3 = this.hdnObj.Value.ToString();
                            if (str3.IndexOf("~") > 0)
                            {
                                if (str3.IndexOf("^") <= 0)
                                {
                                    str3.Substring(str3.IndexOf("~") + 1, str3.Length - (str3.IndexOf("~") + 1));
                                }
                                else
                                {
                                    str3.Substring(str3.IndexOf("~") + 1, str3.IndexOf("^") - (str3.IndexOf("~") + 1));
                                }
                                Bill_Sys_ReferalEvent billSysReferalEvent = new Bill_Sys_ReferalEvent();
                                ArrayList arrayLists3 = new ArrayList();
                                arrayLists3.Add(this.extddlDoctor.Text);
                                arrayLists3.Add(this.txtPatientID.Text);
                                arrayLists3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                text1 = billSysReferalEvent.AddDoctor(arrayLists3);
                                for (int i = 0; i < this.grdProcedureCode.Items.Count; i++)
                                {
                                    if (((CheckBox)this.grdProcedureCode.Items[i].FindControl("chkSelect")).Checked)
                                    {
                                        text = this.grdProcedureCode.Items[i].Cells[1].Text;
                                        arrayLists3 = new ArrayList();
                                        arrayLists3.Add(text1);
                                        arrayLists3.Add(text);
                                        arrayLists3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                        arrayLists3.Add(text);
                                        billSysReferalEvent.AddDoctorAmount(arrayLists3);
                                        arrayLists3 = new ArrayList();
                                        arrayLists3.Add(this.txtPatientID.Text);
                                        arrayLists3.Add(text1);
                                        arrayLists3.Add(this.hdnObj.Value.ToString().Substring(0, this.hdnObj.Value.ToString().IndexOf(" ")));
                                        arrayLists3.Add(text);
                                        arrayLists3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                        arrayLists3.Add(this.ddlType.SelectedValue);
                                        billSysReferalEvent.AddPatientProc(arrayLists3);
                                    }
                                }
                            }
                            else if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                            {
                                text1 = this.extddlDoctor.Text;
                            }
                            else
                            {
                                Bill_Sys_ReferalEvent billSysReferalEvent1 = new Bill_Sys_ReferalEvent();
                                ArrayList arrayLists4 = new ArrayList();
                                text1 = this.extddlDoctor.Text;
                                for (int j = 0; j < this.grdProcedureCode.Items.Count; j++)
                                {
                                    if (((CheckBox)this.grdProcedureCode.Items[j].FindControl("chkSelect")).Checked)
                                    {
                                        text = this.grdProcedureCode.Items[j].Cells[1].Text;
                                        calDoctorAmount _calDoctorAmount = new calDoctorAmount()
                                        {
                                            SZ_DOCTOR_ID = text1,
                                            SZ_PROCEDURE_ID = text,
                                            SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID,
                                            SZ_TYPE_CODE_ID = text
                                        };
                                        arrayLists.Add(_calDoctorAmount);
                                        _calOperation.bt_add_doctor_amount = true;
                                    }
                                }
                            }
                            if (this.extddlMedicalOffice.Visible && this.extddlMedicalOffice.Text != "")
                            {
                            }
                            string str4 = this.hdnObj.Value.ToString();
                            str4 = str4.Substring(0, str4.IndexOf(" "));
                            Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
                            ArrayList arrayLists5 = new ArrayList();
                            _calEvent.SZ_PATIENT_ID = this.txtPatientID.Text;
                            _calEvent.DT_EVENT_DATE = str4;
                            _calEvent.DT_EVENT_TIME = string.Concat(this.ddlHours.SelectedValue.ToString(), ".", this.ddlMinutes.SelectedValue.ToString());
                            _calEvent.SZ_EVENT_NOTES = this.txtNotes.Text;
                            _calEvent.SZ_DOCTOR_ID = text1;
                            text = "";
                            int num = 0;
                            if (num < this.grdProcedureCode.Items.Count)
                            {
                                text = this.grdProcedureCode.Items[num].Cells[1].Text;
                            }
                            if (text == null)
                            {
                                _calEvent.DT_EVENT_DATE = str4;
                            }
                            else
                            {
                                _calEvent.SZ_TYPE_CODE_ID = text;
                            }
                            _calEvent.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            _calEvent.DT_EVENT_TIME_TYPE = this.ddlTime.SelectedValue;
                            _calEvent.DT_EVENT_END_TIME = string.Concat(this.ddlEndHours.SelectedValue.ToString(), ".", this.ddlEndMinutes.SelectedValue.ToString());
                            _calEvent.DT_EVENT_END_TIME_TYPE = this.ddlEndTime.SelectedValue;
                            _calEvent.SZ_REFERENCE_ID = this.extddlReferenceFacility.Text;
                            _calEvent.BT_STATUS = "0";
                            if (!this.chkTransportation.Checked)
                            {
                                _calEvent.BT_TRANSPORTATION = "0";
                            }
                            else
                            {
                                _calEvent.BT_TRANSPORTATION = "1";
                            }
                            _calEvent.DT_EVENT_DATE = str4;
                            if (this.chkTransportation.Checked)
                            {
                                _calEvent.I_TRANSPORTATION_COMPANY = this.extddlTransport.Text;
                            }
                            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                            {
                                _calEvent.SZ_OFFICE_ID = this.extddlMedicalOffice.Text;
                            }
                            for (int k = 0; k < this.grdProcedureCode.Items.Count; k++)
                            {
                                if (((CheckBox)this.grdProcedureCode.Items[k].FindControl("chkSelect")).Checked)
                                {
                                    text = this.grdProcedureCode.Items[k].Cells[1].Text;
                                    calProcedureCodeEO _calProcedureCodeEO = new calProcedureCodeEO()
                                    {
                                        SZ_PROC_CODE = text,
                                        I_EVENT_ID = "",
                                        I_STATUS = "0"
                                    };
                                    arrayLists1.Add(_calProcedureCodeEO);
                                }
                            }
                            if (this.canCopy != "1")
                            {
                                _calResult = calendarTransaction.fnc_SaveAppointment(_calOperation, _calPatientEO, arrayLists, _calEvent, arrayLists1, this.txtUserId.Text);
                            }
                            else
                            {
                                if (!this.txtPatientFName.ReadOnly)
                                {
                                    ArrayList arrayLists6 = new ArrayList();
                                    arrayLists6.Add(this.txtPatientFName.Text);
                                    arrayLists6.Add(this.txtPatientLName.Text);
                                    arrayLists6.Add(null);
                                    arrayLists6.Add(this.extddlCaseType.Text);
                                    if (this.txtBirthdate.Text == "")
                                    {
                                        arrayLists6.Add(null);
                                    }
                                    else
                                    {
                                        arrayLists6.Add(this.txtBirthdate.Text);
                                    }
                                    arrayLists6.Add(this.extddlReferringFacility.Text);
                                    arrayLists6.Add("existpatient");
                                    string str5 = (new Bill_Sys_PatientBO()).CheckPatientExists(arrayLists6);
                                    if (!(str5 != "") || !(this.txtPatientExistMsg.Value == ""))
                                    {
                                        outSchedulePatientDAO = this.create_outPatientDAO();
                                        this.txtPatientID.Text = billingSysManageNotesBO.GetPatientLatestID();
                                        outSchedulePatientDAO.addPatient = true;
                                    }
                                    else
                                    {
                                        this.msgPatientExists.InnerHtml = str5;
                                        this.ModalPopupExtender.Show();
                                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "javascript:openExistsPage();", true);
                                        return;
                                    }
                                }
                                string str6 = str3.Substring(str3.IndexOf("!") + 1, 20);
                                OutSchedulePatient outSchedulePatient = new OutSchedulePatient();
                                if (this.txtPatientFName.ReadOnly && _calEvent.SZ_PATIENT_ID != null)
                                {
                                    str = outSchedulePatient.GetCaseIdForDocumentPath(_calEvent.SZ_PATIENT_ID);
                                }
                                _calResult = outSchedulePatient.AddVisit(_calOperation, outSchedulePatientDAO, arrayLists, _calEvent, arrayLists1, this.txtUserId.Text, str6, this.extddlReferringFacility.Text, this.txtCompanyID.Text, this.extddlDoctor.Text);
                                if (_calResult.msg_code == "SUCCESS")
                                {
                                    caseIdForDocumentPath = outSchedulePatient.GetCaseIdForDocumentPath(_calResult.sz_patient_id);
                                }
                                if (str != "")
                                {
                                    string physicalPath = (new Bill_Sys_NF3_Template()).getPhysicalPath();
                                    DataSet nodeIdForCopyDocument = outSchedulePatient.GetNodeIdForCopyDocument(this.extddlReferringFacility.Text);
                                    if (nodeIdForCopyDocument != null && nodeIdForCopyDocument.Tables.Count > 0 && nodeIdForCopyDocument.Tables[0].Rows.Count > 0)
                                    {
                                        for (int l = 0; l < nodeIdForCopyDocument.Tables[0].Rows.Count; l++)
                                        {
                                            string str7 = nodeIdForCopyDocument.Tables[0].Rows[l]["SZ_NODE_TYPE"].ToString();
                                            string sourcePath = outSchedulePatient.GetSourcePath(this.txtCompanyID.Text, str7, str);
                                            string destPath = outSchedulePatient.GetDestPath(this.extddlReferringFacility.Text, caseIdForDocumentPath);
                                            if (sourcePath != "")
                                            {
                                                string str8 = string.Concat(physicalPath, sourcePath);
                                                string str9 = string.Concat(physicalPath, destPath, "/", sourcePath);
                                                Bill_Sys_AppointPatientEntry.DirectoryCopy(str8, str9);
                                            }
                                        }
                                    }
                                }
                            }
                            if (_calResult.msg_code == "SUCCESS")
                            {
                                this._DAO_NOTES_EO = new DAO_NOTES_EO()
                                {
                                    SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED",
                                    SZ_ACTIVITY_DESC = string.Concat("Date : ", str3.Substring(0, str3.IndexOf("!")))
                                };
                                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                                this._DAO_NOTES_EO.SZ_CASE_ID = this.txtCaseID.Text;
                                this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                            }
                        }
                        if (this.hdnObj.Value.ToString().IndexOf("~") <= 0)
                        {
                            this.Session["PopUp"] = "True";
                        }
                        else
                        {
                            this.Session["PopUp"] = null;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('The patient is already scheduled for this date and time period.')", true);
                        this.ModalPopupExtender.Show();
                        return;
                    }
                }
                string[] strArrays1 = this.hdnObj.Value.ToString().Split(new char[] { '/' });
                string[] strArrays2 = new string[] { strArrays1[0], "/", strArrays1[1], "/", strArrays1[2].Substring(0, 4).ToString() };
                string str10 = string.Concat(strArrays2);
                if (str10 != "")
                {
                    if (this.extddlReferringFacility.Visible)
                    {
                        DateTime dateTime = Convert.ToDateTime(str10);
                        this.GetCalenderDayAppointments(dateTime.ToString("MM/dd/yyyy"), this.extddlReferringFacility.Text);
                    }
                    else
                    {
                        DateTime dateTime1 = Convert.ToDateTime(str10);
                        this.GetCalenderDayAppointments(dateTime1.ToString("MM/dd/yyyy"), this.txtCompanyID.Text);
                    }
                    if (this.txtPatientExistMsg.Value != "")
                    {
                        DateTime dateTime2 = Convert.ToDateTime(str10);
                        string str11 = string.Format("{0:MM/dd/yyyy}", dateTime2.ToString("MM/dd/yyyy"));
                        if (this.extddlReferringFacility.Visible)
                        {
                            this.GetCalenderDayAppointments(str11, this.extddlReferringFacility.Text);
                        }
                        else
                        {
                            this.GetCalenderDayAppointments(str11, this.txtCompanyID.Text);
                        }
                    }
                }
                if (_calResult.msg_code != "SUCCESS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", string.Concat("alert('", _calResult.msg, "')"), true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.btnSave, typeof(Button), "Msg", "alert('Appointment saved successfully.')", true);
                }
                this.ClearValues();
                this.txtPatientExistMsg.Value = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('please select transport company.')", true);
                this.ModalPopupExtender.Show();
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

    protected void btnSearhPatientList_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            try
            {
                this.Session["Page_Index"] = "";
                this.SearchPatientList();
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
           
        
        }
        finally
        {
            this.ModalPopupExtender.Show();
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
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.updateAppointmentFromTestFacility();
            }
            else
            {
                this.updateAppointmentFromBillingCompany();
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

    protected void btnYes_Click(object sender, EventArgs e)
    {
    }

    private string check_appointment()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
        string str = "";
        try
        {
            if (this.hdnObj.Value != "")
            {
                string str1 = "";
                for (int i = 0; i < this.grdProcedureCode.Items.Count; i++)
                {
                    if (((CheckBox)this.grdProcedureCode.Items[i].FindControl("chkSelect")).Checked)
                    {
                        str1 = (str1 != "" ? string.Concat(str1, ",", this.grdProcedureCode.Items[i].Cells[1].Text) : this.grdProcedureCode.Items[i].Cells[1].Text);
                    }
                }
                string value = this.hdnObj.Value;
                string value1 = this.hdnObj.Value;
                value1 = value1.Substring(0, value1.IndexOf(" "));
                ArrayList arrayLists = new ArrayList();
                arrayLists.Add(this.txtCaseID.Text);
                arrayLists.Add(this.txtCompanyID.Text);
                arrayLists.Add(value.Substring(value.IndexOf("!") + 1, 20));
                arrayLists.Add(value1);
                arrayLists.Add(str1);
                arrayLists.Add(this.txtPatientID.Text);
                str = billSysPatientBO.Check_Appointment(arrayLists);
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
        return str;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string check_appointment_for_period()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
        string str = "";
        try
        {
            if (this.hdnObj.Value != "")
            {
                string value = this.hdnObj.Value;
                string value1 = this.hdnObj.Value;
                value1 = value1.Substring(0, value1.IndexOf(" "));
                ArrayList arrayLists = new ArrayList();
                arrayLists.Add(this.txtCaseID.Text);
                arrayLists.Add(this.txtCompanyID.Text);
                arrayLists.Add(value.Substring(value.IndexOf("!") + 1, 20));
                arrayLists.Add(value1);
                arrayLists.Add(string.Concat(this.ddlHours.SelectedValue, ".", this.ddlMinutes.SelectedValue));
                arrayLists.Add(string.Concat(this.ddlEndHours.SelectedValue, ".", this.ddlEndMinutes.SelectedValue));
                arrayLists.Add(this.ddlTime.SelectedValue);
                str = billSysPatientBO.Check_Appointment_For_Period(arrayLists);
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
        return str;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
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
            if (this.chkTransportation.Checked)
            {
                this.extddlTransport.Visible = true;
                this.extddlTransport.Flag_ID = this.extddlReferenceFacility.Text;
            }
            else if (!this.chkTransportation.Checked)
            {
                this.extddlTransport.Visible = false;
            }
            this.ModalPopupExtender.Show();
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
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtRefChartNumber.Text = "";
            this.txtPatientFName.Text = "";
            this.txtMI.Text = "";
            this.txtPatientLName.Text = "";
            this.txtPatientPhone.Text = "";
            this.txtPatientAddress.Text = "";
            this.TextBox3.Text = "";
            this.txtState.Text = "";
            this.txtBirthdate.Text = "";
            this.txtPatientAge.Text = "";
            this.txtSocialSecurityNumber.Text = "";
            this.extddlInsuranceCompany.Text = "NA";
            this.extddlCaseType.Text = "NA";
            this.extddlMedicalOffice.Text = "NA";
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
        this.txtPatientID.Text = "";
        this.txtPatientFName.Text = "";
        this.txtMI.Text = "";
        this.txtPatientLName.Text = "";
        this.txtPatientPhone.Text = "";
        this.txtPatientAddress.Text = "";
        this.txtState.Text = "";
        this.txtBirthdate.Text = "";
        this.txtPatientAge.Text = "";
        this.txtSocialSecurityNumber.Text = "";
        this.txtCaseID.Text = "";
        this.Session["SZ_CASE_ID"] = "";
        this.extddlCaseType.Text = "NA";
        this.extddlInsuranceCompany.Text = "NA";
        this.txtNotes.Text = "";
        this.ddlTestNames.Items.Clear();
        this.hdnObj.Value = "";
        this.hdnEventDeleteDate.Value = "";
        this.hdnEventId.Value = "";
        this.ddlEndHours.Items.Clear();
        this.ddlEndMinutes.Items.Clear();
        this.ddlEndTime.Items.Clear();
        this.ddlHours.Items.Clear();
        this.ddlMinutes.Items.Clear();
        this.ddlTime.Items.Clear();
        this.grdPatientList.DataSource = null;
        this.grdPatientList.DataBind();
        this.lblMsg.Text = "";
        this.txtPatientFirstName.Text = "";
        this.txtPatientLastName.Text = "";
        this.txtRefChartNumber.Text = "";
        this.ModalPopupExtender.Hide();
    }

    private calPatientEO create_calPatientEO()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
        calPatientEO _calPatientEO = new calPatientEO();
        Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
        try
        {
            this.extddlCaseStatus.Text = billSysCalender.GetOpenCaseStatus(this.txtCompanyID.Text);
            ArrayList arrayLists = new ArrayList();
            _calPatientEO.SZ_PATIENT_FIRST_NAME = this.txtPatientFName.Text;
            _calPatientEO.SZ_PATIENT_LAST_NAME = this.txtPatientLName.Text;
            _calPatientEO.SZ_CASE_TYPE_ID = this.extddlCaseType.Text;
            _calPatientEO.SZ_PATIENT_ADDRESS = this.txtPatientAddress.Text;
            _calPatientEO.SZ_PATIENT_CITY = this.TextBox3.Text;
            _calPatientEO.SZ_PATIENT_PHONE = this.txtPatientPhone.Text;
            _calPatientEO.SZ_PATIENT_STATE_ID = this.extddlPatientState.Text;
            _calPatientEO.SZ_COMPANY_ID = this.txtCompanyID.Text;
            _calPatientEO.MI = this.txtMI.Text;
            _calPatientEO.SZ_CASE_STATUS_ID = this.extddlCaseStatus.Text;
            _calPatientEO.SZ_INSURANCE_ID = this.extddlInsuranceCompany.Text;
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
        return _calPatientEO;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private OutSchedulePatientDAO create_outPatientDAO()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
        OutSchedulePatientDAO outSchedulePatientDAO = new OutSchedulePatientDAO();
        Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
        try
        {
            this.extddlCaseStatus.Text = billSysCalender.GetOpenCaseStatus(this.extddlReferringFacility.Text);
            ArrayList arrayLists = new ArrayList();
            outSchedulePatientDAO.sPatientFirstName = this.txtPatientFName.Text;
            outSchedulePatientDAO.sPatientLastName = this.txtPatientLName.Text;
            outSchedulePatientDAO.sCaseTypeID = this.extddlCaseType.Text;
            outSchedulePatientDAO.sPatientAddress = this.txtPatientAddress.Text;
            outSchedulePatientDAO.sPatientCity = this.TextBox3.Text;
            outSchedulePatientDAO.sPatientPhone = this.txtPatientPhone.Text;
            outSchedulePatientDAO.sPatientState = this.extddlPatientState.Text;
            outSchedulePatientDAO.sSourceCompanyID = this.txtCompanyID.Text;
            outSchedulePatientDAO.sPatientMI = this.txtMI.Text;
            outSchedulePatientDAO.sCaseStatusID = this.extddlCaseStatus.Text;
            outSchedulePatientDAO.sInsuranceID = this.extddlInsuranceCompany.Text;
            outSchedulePatientDAO.sDestinationCompanyID = this.extddlReferringFacility.Text;
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
        return outSchedulePatientDAO;

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
            if (!(this.ddlType.SelectedValue == "TY000000000000000001") && !(this.ddlType.SelectedValue == "TY000000000000000002"))
            {
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

    private static void DirectoryCopy(string sourceDirName, string destDirName)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirName);
        if (directoryInfo.Exists)
        {
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            if (!directoryInfo.Exists)
            {
                throw new DirectoryNotFoundException(string.Concat("Source directory does not exist or could not be found: ", sourceDirName));
            }
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
            FileInfo[] files = directoryInfo.GetFiles();
            for (int i = 0; i < (int)files.Length; i++)
            {
                FileInfo fileInfo = files[i];
                string str = Path.Combine(destDirName, fileInfo.Name);
                if (!File.Exists(str))
                {
                    fileInfo.CopyTo(str, false);
                }
            }
            DirectoryInfo[] directoryInfoArray = directories;
            for (int j = 0; j < (int)directoryInfoArray.Length; j++)
            {
                DirectoryInfo directoryInfo1 = directoryInfoArray[j];
                string str1 = Path.Combine(destDirName, directoryInfo1.Name);
                Bill_Sys_AppointPatientEntry.DirectoryCopy(directoryInfo1.FullName, str1);
            }
        }
    }

    private void DisplayControlForAddVisit()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtRefChartNumber.ReadOnly = true;
            this.txtPatientFName.ReadOnly = true;
            this.txtMI.ReadOnly = true;
            this.txtPatientLName.ReadOnly = true;
            this.txtPatientPhone.ReadOnly = true;
            this.txtPatientAddress.ReadOnly = true;
            this.TextBox3.ReadOnly = true;
            this.txtState.ReadOnly = true;
            this.txtBirthdate.ReadOnly = true;
            this.txtPatientAge.ReadOnly = true;
            this.txtSocialSecurityNumber.ReadOnly = true;
            this.extddlInsuranceCompany.Enabled = false;
            this.extddlCaseType.Enabled = false;
            this.extddlMedicalOffice.Enabled = true;
            this.extddlPatientState.Enabled = false;
            this.lblSSN.Visible = false;
            this.txtSocialSecurityNumber.Visible = false;
            this.lblBirthdate.Visible = false;
            this.txtBirthdate.Visible = false;
            this.lblAge.Visible = false;
            this.txtPatientAge.Visible = false;
            this.lblChartNumber.Visible = false;
            this.txtRefChartNumber.Visible = false;
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.Trim().ToLower().ToString() == "referring office")
            {
                Bill_Sys_BillingCompanyDetails_BO billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                string refDocID = billSysBillingCompanyDetailsBO.GetRefDocID(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                if (refDocID != "")
                {
                    this.extddlMedicalOffice.Text = refDocID;
                    this.extddlMedicalOffice.Enabled = false;
                    this.extddlDoctor.Procedure_Name = "SP_GET_REF_DOC";
                    this.extddlDoctor.Connection_Key = "Connection_String";
                    this.extddlDoctor.Selected_Text = "---Select---";
                    this.extddlDoctor.Flag_Key_Value = this.txtCompanyID.Text;
                    this.extddlDoctor.Flag_ID = refDocID;
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

    protected void DisplayProcedureGridColumns(bool value)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdProcedureCode.Columns[3].Visible = value;
            this.grdProcedureCode.Columns[4].Visible = value;
            this.grdProcedureCode.Columns[5].Visible = value;
            this.grdProcedureCode.Columns[6].Visible = value;
            this.grdProcedureCode.Columns[9].Visible = value;
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

    protected void extddlMedicalOffice_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.extddlDoctor.Flag_ID = this.extddlMedicalOffice.Text;
            this.extddlDoctor.Flag_Key_Value = "newGETREFFERDOCTORLIST";
            this.ModalPopupExtender.Show();
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

    private string GETAppointPatientDetail(int i_schedule_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str;
        try
        {
            DataTable dataTable = new DataTable();
            this.ds = new DataSet();
            this._patient_TVBO = new Patient_TVBO();
            this.ds = this._patient_TVBO.GetAppointPatientDetails(i_schedule_id);
            if (this.ds == null)
            {
                this.usrMessage.PutMessage("Could not load appointment data. Possible reasons - Case type is not added to the case.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
                str = "missing_case_type";
            }
            else if (this.ds.Tables[0] == null)
            {
                this.usrMessage.PutMessage("Could not load appointment data. Possible reasons - Case type is not added to the case.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
                str = "missing_case_type";
            }
            else if (this.ds.Tables[0].Rows.Count <= 0)
            {
                this.usrMessage.PutMessage("Could not load appointment data. Possible reasons - Case type is not added to the case.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
                str = "missing_case_type";
            }
            else
            {
                this.ds.Tables[0].Clone();
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;")
                {
                    this.txtPatientID.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;")
                {
                    this.txtPatientFName.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;")
                {
                    this.txtMI.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;")
                {
                    this.txtPatientLName.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;")
                {
                    this.txtPatientPhone.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;")
                {
                    this.txtPatientAddress.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                }
                if (this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString() == "&nbsp;")
                {
                    this.extddlPatientState.Text = "NA";
                }
                else
                {
                    this.extddlPatientState.Text = this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString() != "&nbsp;")
                {
                    this.txtBirthdate.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;")
                {
                    this.txtPatientAge.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() != "&nbsp;")
                {
                    this.txtSocialSecurityNumber.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "&nbsp;")
                {
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "True")
                    {
                        this.chkTransportation.Checked = false;
                    }
                    else
                    {
                        this.chkTransportation.Checked = true;
                    }
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(34).ToString() != "&nbsp;")
                {
                    this.txtCaseID.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(34).ToString();
                }
                this.extddlInsuranceCompany.Flag_ID = this.ds.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                this.extddlCaseType.Flag_ID = this.ds.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(33).ToString() != "&nbsp;")
                {
                    this.extddlCaseType.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(33).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(32).ToString() != "&nbsp;")
                {
                    this.extddlInsuranceCompany.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(32).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(35).ToString() != "&nbsp;")
                {
                    this.extddlDoctor.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(35).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(36).ToString() != "&nbsp;")
                {
                    this.ddlType.SelectedValue = this.ds.Tables[0].Rows[0].ItemArray.GetValue(36).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(37).ToString() != "&nbsp;")
                {
                    string str1 = this.ds.Tables[0].Rows[0].ItemArray.GetValue(37).ToString();
                    this.ddlHours.SelectedValue = str1.Substring(0, str1.IndexOf(".")).PadLeft(2, '0');
                    this.ddlMinutes.SelectedValue = str1.Substring(str1.IndexOf(".") + 1, str1.Length - (str1.IndexOf(".") + 1)).PadLeft(2, '0');
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(38).ToString() != "&nbsp;")
                {
                    string str2 = this.ds.Tables[0].Rows[0].ItemArray.GetValue(38).ToString();
                    this.ddlEndHours.SelectedValue = str2.Substring(0, str2.IndexOf(".")).PadLeft(2, '0');
                    this.ddlEndMinutes.SelectedValue = str2.Substring(str2.IndexOf(".") + 1, str2.Length - (str2.IndexOf(".") + 1)).PadLeft(2, '0');
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(39).ToString() != "&nbsp;")
                {
                    this.ddlTime.SelectedValue = this.ds.Tables[0].Rows[0].ItemArray.GetValue(39).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(40).ToString() != "&nbsp;")
                {
                    this.ddlEndTime.SelectedValue = this.ds.Tables[0].Rows[0].ItemArray.GetValue(40).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(42).ToString() != "&nbsp;")
                {
                    this.txtNotes.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(42).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(44).ToString() != "&nbsp;")
                {
                    this.txtPatientCompany.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(44).ToString();
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(43).ToString() != "&nbsp;" && this.ds.Tables[0].Rows[0].ItemArray.GetValue(43).ToString() == "True")
                {
                    this.extddlDoctor.Enabled = false;
                    this.ddlType.Enabled = false;
                    this.txtNotes.ReadOnly = true;
                }
                if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(45).ToString() != "")
                {
                    this.extddlMedicalOffice.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(45).ToString();
                }
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(51).ToString() != "&nbsp;")
                    {
                        this.extddlDoctor.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(51).ToString();
                    }
                    this.txtRefChartNumber.Text = "";
                }
                else
                {
                    this.extddlDoctor.Flag_ID = this.extddlMedicalOffice.Text;
                    this.extddlDoctor.Flag_Key_Value = "newGETREFFERDOCTORLIST";
                    if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(41).ToString() != "&nbsp;")
                    {
                        this.extddlDoctor.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(41).ToString();
                    }
                    if (this.ds.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString() != "&nbsp;")
                    {
                        this.txtRefChartNumber.Text = this.ds.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString();
                    }
                }
                if (this.ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString() == "&nbsp;")
                {
                    this.extddlTransport.Text = "NA";
                }
                else if (Convert.ToInt32(this.ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString()) > 0 && this.chkTransportation.Checked)
                {
                    this.extddlTransport.Flag_ID = this.extddlReferenceFacility.Text;
                    this.extddlTransport.Text = this.ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString();
                    this.extddlTransport.Visible = true;
                }
                else if (Convert.ToInt32(this.ds.Tables[0].Rows[0]["TRANSPORTATION_COMPANY"].ToString()) != 0 || !this.chkTransportation.Checked)
                {
                    this.extddlTransport.Visible = false;
                }
                else
                {
                    this.extddlTransport.Text = "NA";
                    this.extddlTransport.Visible = true;
                }
                this.ds = new DataSet();
                this._patient_TVBO = new Patient_TVBO();
                this.ds = this._patient_TVBO.GetAppointProcCode(i_schedule_id);
                foreach (DataRow row in this.ds.Tables[0].Rows)
                {
                    foreach (DataGridItem item in this.grdProcedureCode.Items)
                    {
                        if (item.Cells[1].Text != row.ItemArray.GetValue(0).ToString())
                        {
                            continue;
                        }
                        CheckBox checkBox = (CheckBox)item.Cells[1].FindControl("chkSelect");
                        checkBox.Checked = true;
                        item.BackColor = Color.LightSeaGreen;
                    }
                }
                str = "success";
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
            return null;
        }
        return str;
        
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetCalenderDayAppointments(string p_szScheduleDay, string referralid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        this.Session["INTERVAL"] = this.ddlInterval.Text.Substring(2, 2);
        this.lblCurrentDate.Text = string.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(p_szScheduleDay));
        if (this.Session["AppointmentDate"] == null || this.Session["AppointmentDate"] == null || this.Session["AppointmentDate"].ToString() != p_szScheduleDay)
        {
            this.Session["AppointmentDate"] = p_szScheduleDay;
        }
        if (this.Session["TestFacilityID"] == null || this.Session["TestFacilityID"] == null || this.Session["TestFacilityID"].ToString() != referralid)
        {
            this.Session["TestFacilityID"] = referralid;
        }
        string pSzScheduleDay = p_szScheduleDay;
        string str = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        SqlDataReader sqlDataReader = null;
        ArrayList arrayLists = new ArrayList();
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GET_ROOM_NAMES_FOR_DAY_VIEW", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                SqlParameterCollection parameters = sqlCommand.Parameters;
                DateTime dateTime = Convert.ToDateTime(pSzScheduleDay);
                parameters.AddWithValue("@DT_DATE", dateTime.ToString("MM/dd/yyyy"));
                sqlCommand.Parameters.AddWithValue("@I_INTERVAL", this.ddlInterval.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@SZ_REFERRING_ID", referralid);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    arrayLists.Add(sqlDataReader[0]);
                }
                sqlDataReader.Close();
                sqlConnection.Close();
                if (this.canCopy != "1")
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("GET_ROOM_DETAILS_FINAL", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    SqlParameterCollection sqlParameterCollection = sqlCommand.Parameters;
                    DateTime dateTime1 = Convert.ToDateTime(pSzScheduleDay);
                    sqlParameterCollection.AddWithValue("@DT_DATE", dateTime1.ToString("MM/dd/yyyy"));
                    sqlCommand.Parameters.AddWithValue("@I_INTERVAL", this.ddlInterval.SelectedValue);
                    sqlCommand.Parameters.AddWithValue("@SZ_REFERRING_ID", referralid);
                    sqlCommand.Parameters.AddWithValue("@SZ_USER_ID", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    sqlDataReader = sqlCommand.ExecuteReader();
                }
                else
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("GET_ROOM_DETAILS_FINAL_FOR_OUTSCHEDULE_PATIENT", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    SqlParameterCollection parameters1 = sqlCommand.Parameters;
                    DateTime dateTime2 = Convert.ToDateTime(pSzScheduleDay);
                    parameters1.AddWithValue("@DT_DATE", dateTime2.ToString("MM/dd/yyyy"));
                    sqlCommand.Parameters.AddWithValue("@I_INTERVAL", this.ddlInterval.SelectedValue);
                    sqlCommand.Parameters.AddWithValue("@SZ_REFERRING_ID", referralid);
                    sqlCommand.Parameters.AddWithValue("@SZ_USER_ID", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    sqlDataReader = sqlCommand.ExecuteReader();
                }
                this.Label1.Text = "<table width='100%'>";
                this.Label1.Text = string.Concat(this.Label1.Text, "<tr>");
                this.Label1.Text = string.Concat(this.Label1.Text, "<td style='text-align:center;font-family:Times New Roman;font-weight:bold;font-size:12px;width:80px;align:left'>&nbsp;");
                this.Label1.Text = string.Concat(this.Label1.Text, "</td>");
                for (int i = 0; i < arrayLists.Count; i++)
                {
                    Label label1 = this.Label1;
                    object[] text = new object[] { this.Label1.Text, "<td style='width:", 100 / arrayLists.Count - 2, "%; text-align:center;'>" };
                    label1.Text = string.Concat(text);
                    this.Label1.Text = string.Concat(this.Label1.Text, arrayLists[i].ToString());
                    this.Label1.Text = string.Concat(this.Label1.Text, "</td>");
                }
                this.Label1.Text = string.Concat(this.Label1.Text, "</tr>");
                while (sqlDataReader.Read())
                {
                    this.Label1.Text = string.Concat(this.Label1.Text, "<tr>");
                    this.Label1.Text = string.Concat(this.Label1.Text, "<td style='text-align:center;font-family:Times New Roman;font-weight:bold;font-size:12px;background-color:#E0E0E0;width:80px;align:left'>");
                    this.Label1.Text = string.Concat(this.Label1.Text, sqlDataReader[1]);
                    this.Label1.Text = string.Concat(this.Label1.Text, "</td>");
                    Label label = this.Label1;
                    string text1 = this.Label1.Text;
                    int count = arrayLists.Count;
                    label.Text = string.Concat(text1, "<td width='90%' style='background-color:#FFFFEA;' colspan='", count.ToString(), "'  >");
                    this.Label1.Text = string.Concat(this.Label1.Text, "<div style='text-align:center;font-size:12px;background-color:#FFFFD5;border-bottom: gray 1px solid;width:100%;height:100%;'>");
                    this.Label1.Text = string.Concat(this.Label1.Text, "<table width='100%'><tr>");
                    for (int j = 1; j <= arrayLists.Count; j++)
                    {
                        try
                        {
                            Label label11 = this.Label1;
                            object[] objArray = new object[] { this.Label1.Text, "<td style='width:", 100 / arrayLists.Count, "%; text-align:center;'>" };
                            label11.Text = string.Concat(objArray);
                            this.Label1.Text = string.Concat(this.Label1.Text, sqlDataReader[j + 1]);
                            this.Label1.Text = string.Concat(this.Label1.Text, "</td>");
                        }
                        catch (Exception exception)
                        {
                            this.Label1.Text = string.Concat(this.Label1.Text, "</td>");
                            break;
                        }
                    }
                    this.Label1.Text = string.Concat(this.Label1.Text, "</tr></table>");
                    this.Label1.Text = string.Concat(this.Label1.Text, "</div>");
                    this.Label1.Text = string.Concat(this.Label1.Text, "</td>");
                    this.Label1.Text = string.Concat(this.Label1.Text, "</tr>");
                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        this.Label1.Text = string.Concat(this.Label1.Text, "</table>");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getFirstDayOfMonth(string p_szMonth, string p_szYear)
    {
        int intMonth = this.getIntMonth(p_szMonth);
        int num = int.Parse(p_szYear);
        DateTime dateTime = new DateTime(num, intMonth, 1);
        return dateTime.DayOfWeek.ToString();
    }

    private int getIntMonth(string p_szMonth)
    {
        p_szMonth = p_szMonth.ToUpper();
        string pSzMonth = p_szMonth;
        string str = pSzMonth;
        if (pSzMonth != null)
        {
            switch (str)
            {
                case "JAN":
                    {
                        return 1;
                    }
                case "FEB":
                    {
                        return 2;
                    }
                case "MAR":
                    {
                        return 3;
                    }
                case "APR":
                    {
                        return 4;
                    }
                case "MAY":
                    {
                        return 5;
                    }
                case "JUN":
                    {
                        return 6;
                    }
                case "JUL":
                    {
                        return 7;
                    }
                case "AUG":
                    {
                        return 8;
                    }
                case "SEP":
                    {
                        return 9;
                    }
                case "OCT":
                    {
                        return 10;
                    }
                case "NOV":
                    {
                        return 11;
                    }
                case "DEC":
                    {
                        return 12;
                    }
            }
        }
        return 1;
    }

    private int getLastMonthInteger()
    {
        return DateTime.DaysInMonth(int.Parse(this.objCalendar.InitialDisplayYear), this.getIntMonth(this.objCalendar.InitialDisplayMonth));
    }

    private string getLongMonthName(string p_szShortMonth)
    {
        p_szShortMonth = p_szShortMonth.ToUpper();
        string pSzShortMonth = p_szShortMonth;
        string str = pSzShortMonth;
        if (pSzShortMonth != null)
        {
            switch (str)
            {
                case "JAN":
                    {
                        return "January";
                    }
                case "FEB":
                    {
                        return "February";
                    }
                case "MAR":
                    {
                        return "March";
                    }
                case "APR":
                    {
                        return "April";
                    }
                case "MAY":
                    {
                        return "May";
                    }
                case "JUN":
                    {
                        return "June";
                    }
                case "JUL":
                    {
                        return "July";
                    }
                case "AUG":
                    {
                        return "August";
                    }
                case "SEP":
                    {
                        return "September";
                    }
                case "OCT":
                    {
                        return "October";
                    }
                case "NOV":
                    {
                        return "November";
                    }
                case "DEC":
                    {
                        return "December";
                    }
            }
        }
        return "January";
    }

    private string GetOpenCaseStatus()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string str = ConfigurationManager.AppSettings["Connection_String"].ToString();
        string str1 = "";
        SqlConnection sqlConnection = new SqlConnection(str);
        try
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(string.Concat("Select SZ_CASE_STATUS_ID FROM MST_CASE_STATUS WHERE SZ_STATUS_NAME='OPEN' AND SZ_COMPANY_ID='", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "'"), sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                str1 = Convert.ToString(sqlDataReader[0].ToString());
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
        return str1;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string[] getOrderedWeekdays()
    {
        string[] strArrays = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        return strArrays;
    }

    private string[] getShortNamesForWeekdays()
    {
        string[] strArrays = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        return strArrays;
    }

    private byte getStartIndex(string p_szDayName)
    {
        string[] orderedWeekdays = this.getOrderedWeekdays();
        for (byte i = 0; i < (int)orderedWeekdays.Length; i = (byte)(i + 1))
        {
            if (p_szDayName.CompareTo(orderedWeekdays[i]) == 0)
            {
                return i;
            }
        }
        return (byte)0;
    }

    private byte getTodaysDayOfTheMonth()
    {
        return (byte)DateTime.Now.Day;
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
            this.Session["Page_Index"] = e.NewPageIndex.ToString();
            this.SearchPatientList();
            this.ModalPopupExtender.Show();
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

    protected void grdPatientList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            try
            {
                this.lnkPatientDesk.Visible = true;
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[1].Text != "&nbsp;")
                {
                    this.txtPatientID.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[1].Text;
                }
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[2].Text != "&nbsp;")
                {
                    this.txtPatientFName.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[2].Text;
                }
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[11].Text != "&nbsp;")
                {
                    this.txtMI.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[11].Text;
                }
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[3].Text != "&nbsp;")
                {
                    this.txtPatientLName.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[3].Text;
                }
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[9].Text != "&nbsp;")
                {
                    this.txtPatientPhone.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[9].Text;
                }
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[5].Text != "&nbsp;")
                {
                    this.txtPatientAddress.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[5].Text;
                }
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[20].Text != "&nbsp;")
                {
                    this.extddlPatientState.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[20].Text;
                }
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[15].Text != "&nbsp;")
                {
                    this.txtBirthdate.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[15].Text;
                }
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[4].Text != "&nbsp;")
                {
                    this.txtPatientAge.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[4].Text;
                }
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[13].Text != "&nbsp;")
                {
                    this.txtSocialSecurityNumber.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[13].Text;
                }
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[31].Text != "&nbsp;")
                {
                    if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[31].Text.ToString() != "True")
                    {
                        this.chkTransportation.Checked = false;
                    }
                    else
                    {
                        this.chkTransportation.Checked = true;
                    }
                }
                Billing_Sys_ManageNotesBO billingSysManageNotesBO = new Billing_Sys_ManageNotesBO();
                DataSet caseDetails = billingSysManageNotesBO.GetCaseDetails(this.txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                if (caseDetails.Tables[0].Rows.Count > 0)
                {
                    this.txtCaseID.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                    {
                        this.extddlInsuranceCompany.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this.extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                        this.extddlInsuranceCompany.Flag_ID = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                        this.extddlCaseType.Flag_ID = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                    }
                    if (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(21).ToString() == "")
                    {
                        this.extddlMedicalOffice.Enabled = true;
                    }
                    else
                    {
                        this.extddlMedicalOffice.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                        this.extddlMedicalOffice.Enabled = false;
                        this.extddlDoctor.Flag_ID = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                        this.extddlDoctor.Flag_Key_Value = "newGETREFFERDOCTORLIST";
                    }
                    this.extddlCaseType.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    this.extddlInsuranceCompany.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    if (this.txtPatientID.Text.ToString() != "")
                    {
                        CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
                        Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject()
                        {
                            SZ_PATIENT_ID = this.txtPatientID.Text,
                            SZ_CASE_ID = this.txtCaseID.Text
                        };
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                        {
                            billSysCaseObject.SZ_CASE_NO = caseDetailsBO.GetCaseNo(billSysCaseObject.SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        }
                        else
                        {
                            billSysCaseObject.SZ_CASE_NO = caseDetailsBO.GetCaseNo(billSysCaseObject.SZ_CASE_ID, caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                        }
                        billSysCaseObject.SZ_PATIENT_NAME = caseDetailsBO.GetPatientName(billSysCaseObject.SZ_PATIENT_ID);
                        this.Session["CASE_OBJECT"] = billSysCaseObject;
                    }
                }
                this.DisplayControlForAddVisit();
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
        }
        finally
        {
            this.ModalPopupExtender.Show();
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
                DropDownList dropDownList = (DropDownList)e.Item.Cells[5].FindControl("ddlReSchHours");
                DropDownList dropDownList1 = (DropDownList)e.Item.Cells[5].FindControl("ddlReSchMinutes");
                DropDownList dropDownList2 = (DropDownList)e.Item.Cells[5].FindControl("ddlReSchTime");
                for (int i = 0; i <= 12; i++)
                {
                    if (i <= 9)
                    {
                        dropDownList.Items.Add(string.Concat("0", i.ToString()));
                    }
                    else
                    {
                        dropDownList.Items.Add(i.ToString());
                    }
                }
                for (int j = 0; j < 60; j++)
                {
                    if (j <= 9)
                    {
                        dropDownList1.Items.Add(string.Concat("0", j.ToString()));
                    }
                    else
                    {
                        dropDownList1.Items.Add(j.ToString());
                    }
                }
                dropDownList2.Items.Add("AM");
                dropDownList2.Items.Add("PM");
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

    protected void Link1_Click(object sender, EventArgs e)
    {
        if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && this.extddlReferenceFacility.Text == "NA")
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", "checkForTestFacility();", true);
        }
        this.Session["FROM"] = null;
        LinkButton linkButton = (LinkButton)sender;
        Label label1 = this.Label1;
        DateTime now = DateTime.Now;
        label1.Text = string.Concat("Refreshed at ", now.ToString(), "  pressed by ", linkButton.CommandArgument);
        string[] strArrays = null;
        strArrays = linkButton.CommandArgument.Split(new char[] { '\u005F' });
        string str = strArrays[0];
        string str1 = strArrays[1];
        string str2 = strArrays[2];
        string str3 = strArrays[3];
        DateTime dateTime = new DateTime(int.Parse(str), this.getIntMonth(str1), int.Parse(str3));
        string str4 = string.Format("{0:MM/dd/yyyy}", dateTime);
        if (!this.extddlReferringFacility.Visible)
        {
            this.GetCalenderDayAppointments(str4, this.txtCompanyID.Text);
            return;
        }
        this.GetCalenderDayAppointments(str4, this.extddlReferringFacility.Text);
    }

    protected void lnkPatientDesk_Click(object sender, EventArgs e)
    {
        if (this.txtCaseID.Text != "")
        {
            this.Session["SZ_CASE_ID"] = this.txtCaseID.Text;
            this.Session["PROVIDERNAME"] = this.txtCaseID.Text;
            CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
            Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject();
            // {
            billSysCaseObject.SZ_PATIENT_ID = this.txtPatientID.Text;
            billSysCaseObject.SZ_CASE_ID = this.txtCaseID.Text;
            billSysCaseObject.SZ_PATIENT_NAME = string.Concat(this.txtPatientFName.Text, this.txtPatientLName.Text);
            billSysCaseObject.SZ_COMAPNY_ID = this.txtCompanyID.Text;
            billSysCaseObject.SZ_CASE_NO = caseDetailsBO.GetCaseNo(billSysCaseObject.SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            // };
            this.Session["CASE_OBJECT"] = billSysCaseObject;
            Bill_Sys_Case billSysCase = new Bill_Sys_Case()
            {
                SZ_CASE_ID = this.txtCaseID.Text
            };
            this.Session["CASEINFO"] = billSysCase;
            this.Session["QStrCaseID"] = this.txtCaseID.Text;
            this.Session["Case_ID"] = this.txtCaseID.Text;
            this.Session["Archived"] = "0";
            this.Session["QStrCID"] = this.txtCaseID.Text;
            this.Session["SelectedID"] = this.txtCaseID.Text;
            base.Response.Redirect("~/Bill_SysPatientDesk.aspx?Flag=true", true);
        }
    }

    private void LoadCalendarAccordingToYearAndMonth()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = "";
            DateTime dateTime = Convert.ToDateTime(this.Session["PRV_MONTH"].ToString());
            str = dateTime.ToString("MM/dd/yyyy");
            this.objCalendar = new Calendar_DAO();
            Calendar_DAO calendarDAO = this.objCalendar;
            DateTime dateTime1 = Convert.ToDateTime(str);
            calendarDAO.ControlIDPrefix = string.Concat("Calendar_", dateTime1.ToString("MMM").ToUpper().ToString());
            Calendar_DAO str1 = this.objCalendar;
            DateTime dateTime2 = Convert.ToDateTime(str);
            str1.InitialDisplayMonth = dateTime2.ToString("MMM").ToUpper().ToString();
            Calendar_DAO calendarDAO1 = this.objCalendar;
            int year = Convert.ToDateTime(str).Year;
            calendarDAO1.InitialDisplayYear = year.ToString();
            this.showCalendar(this.objCalendar);
            DateTime dateTime3 = Convert.ToDateTime(this.Session["CUR_MONTH"].ToString());
            str = dateTime3.ToString("MM/dd/yyyy");
            this.objCalendar = new Calendar_DAO();
            Calendar_DAO calendarDAO2 = this.objCalendar;
            DateTime dateTime4 = Convert.ToDateTime(str);
            calendarDAO2.ControlIDPrefix = string.Concat("Calendar_", dateTime4.ToString("MMM").ToUpper().ToString());
            Calendar_DAO str2 = this.objCalendar;
            DateTime dateTime5 = Convert.ToDateTime(str);
            str2.InitialDisplayMonth = dateTime5.ToString("MMM").ToUpper().ToString();
            Calendar_DAO str3 = this.objCalendar;
            int num = Convert.ToDateTime(str).Year;
            str3.InitialDisplayYear = num.ToString();
            this.showCalendar(this.objCalendar);
            DateTime dateTime6 = Convert.ToDateTime(this.Session["NEXT_MONTH"].ToString());
            str = dateTime6.ToString("MM/dd/yyyy");
            this.objCalendar = new Calendar_DAO();
            Calendar_DAO calendarDAO3 = this.objCalendar;
            DateTime dateTime7 = Convert.ToDateTime(str).AddMonths(1);
            calendarDAO3.ControlIDPrefix = string.Concat("Calendar_", dateTime7.ToString("MMM").ToUpper().ToString());
            Calendar_DAO str4 = this.objCalendar;
            DateTime dateTime8 = Convert.ToDateTime(str);
            str4.InitialDisplayMonth = dateTime8.ToString("MMM").ToUpper().ToString();
            Calendar_DAO calendarDAO4 = this.objCalendar;
            int year1 = Convert.ToDateTime(str).Year;
            calendarDAO4.InitialDisplayYear = year1.ToString();
            this.showCalendar(this.objCalendar);
            this.Session["FROM"] = null;
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

    private void LoadProcedureGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrayLists = new ArrayList();
            arrayLists.Add(this.extddlReferenceFacility.Text);
            string value = this.hdnObj.Value;
            arrayLists.Add(value.Substring(value.IndexOf("!") + 1, 20));
            Bill_Sys_ManageVisitsTreatmentsTests_BO billSysManageVisitsTreatmentsTestsBO = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
            this.grdProcedureCode.DataSource = billSysManageVisitsTreatmentsTestsBO.GetReferringProcCodeList(arrayLists);
            this.grdProcedureCode.DataBind();
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

    private void LoadTypes()
    {
        ArrayList arrayLists = new ArrayList();
        arrayLists.Add(this.extddlReferenceFacility.Text);
        string value = this.hdnObj.Value;
        arrayLists.Add(value.Substring(value.IndexOf("!") + 1, 20));
        Bill_Sys_ManageVisitsTreatmentsTests_BO billSysManageVisitsTreatmentsTestsBO = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
        this.ddlTestNames.Items.Clear();
        this.ddlTestNames.DataSource = billSysManageVisitsTreatmentsTestsBO.GetReferringProcCodeList(arrayLists);
        this.ddlTestNames.DataTextField = "description";
        this.ddlTestNames.DataValueField = "code";
        this.ddlTestNames.DataBind();
        this.ddlTestNames.Items.Insert(0, "--- Select ---");
        this.ddlTestNames.Visible = true;
        this.grdProcedureCode.Visible = false;
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
            this.UserID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            if (base.Request.QueryString["Flag"] != null && base.Request.QueryString["Flag"].ToString() == "true" && base.Request.QueryString["CaseID"] != null)
            {
                this.Session["SZ_CASE_ID"] = base.Request.QueryString["CaseID"].ToString();
                this.Session["PROVIDERNAME"] = base.Request.QueryString["CaseID"].ToString();
                CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject()
                {
                    SZ_PATIENT_ID = caseDetailsBO.GetCasePatientID(base.Request.QueryString["CaseID"].ToString(), ""),
                    SZ_CASE_ID = base.Request.QueryString["CaseID"].ToString()
                };
                this.Session["CASE_OBJECT"] = billSysCaseObject;
            }
            this.txtUserId.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.btnGo.Attributes.Add("onclick", "javascript:return checkForTestFacility();");
            this.canCopy = ((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_COPY_PATIENT_TO_TEST_FACILITY.ToString();
            if (this.canCopy == "1")
            {
                string str = string.Concat("Patient will be copied to ", this.extddlReferringFacility.Selected_Text, " account");
                this.lblMsg2.Visible = true;
                this.lblMsg2.Text = str;
                this.lblMsg2.ForeColor = Color.Red;
            }
            if (this.Session["PopUp"] != null)
            {
                if (this.Session["TestFacilityID"] != null && this.Session["AppointmentDate"] != null && this.Session["PopUp"].ToString() == "True")
                {
                    this.extddlReferringFacility.Flag_ID = this.txtCompanyID.Text.ToString();
                    this.extddlReferringFacility.Text = this.Session["TestFacilityID"].ToString();
                    string str1 = this.Session["AppointmentDate"].ToString();
                    this.Session["AppointmentDate"] = null;
                    this.Session["TestFacilityID"] = null;
                    DateTime dateTime = Convert.ToDateTime(str1);
                    this.GetCalenderDayAppointments(dateTime.ToString("MM/dd/yyyy"), this.extddlReferringFacility.Text);
                    this.Session["PopUp"] = null;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        this.extddlReferringFacility.Visible = false;
                    }
                }
            }
            else if (!this.Page.IsPostBack)
            {
                this.Session["Page_Index"] = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    this.extddlReferringFacility.Visible = false;
                }
                this.extddlReferringFacility.Flag_ID = this.txtCompanyID.Text.ToString();
                this.extddlMedicalOffice.Flag_ID = this.txtCompanyID.Text.ToString();
                this._obj = new Bill_Sys_Schedular();
                if (this.extddlReferringFacility.Visible)
                {
                    DateTime date = DateTime.Today.Date;
                    this.GetCalenderDayAppointments(date.ToString("MM/dd/yyyy"), this.extddlReferringFacility.Text);
                }
                else if (base.Request.QueryString["idate"] == null)
                {
                    DateTime date1 = DateTime.Today.Date;
                    this.GetCalenderDayAppointments(date1.ToString("MM/dd/yyyy"), this.txtCompanyID.Text);
                }
                else if (base.Request.QueryString["idate"].ToString() == "")
                {
                    DateTime dateTime1 = DateTime.Today.Date;
                    this.GetCalenderDayAppointments(dateTime1.ToString("MM/dd/yyyy"), this.txtCompanyID.Text);
                }
                else
                {
                    this.GetCalenderDayAppointments(base.Request.QueryString["idate"].ToString(), this.txtCompanyID.Text);
                }
                if (base.Request.QueryString["Flag"] == null)
                {
                    this.Session["Flag"] = null;
                    this.lblHeaderPatientName.Text = "";
                }
                else
                {
                    this.Session["Flag"] = true;
                    this.lblHeaderPatientName.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_NAME;
                    this.btnAddPatient.Visible = false;
                }
            }
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.btnAddPatient.Visible = false;
            }
            else if (this.Session["Flag"] == null)
            {
                if ((new Bill_Sys_BillingCompanyDetails_BO()).GetRefDocID(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString()) == "")
                {
                    this.btnAddPatient.Visible = true;
                }
                else
                {
                    this.btnAddPatient.Visible = false;
                }
            }
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1")
            {
                this.txtRefChartNumber.Visible = false;
                this.lblChartNumber.Visible = false;
            }
            if (!this.Page.IsPostBack)
            {
                this.txtPatientExistMsg.Value = "";
                int year = DateTime.Today.Year;
                this.BindYearDropDown(year);
                this.ddlYearList.SelectedValue = year.ToString();
                this.ddlMonthList.SelectedValue = DateTime.Today.Month.ToString();
                HttpSessionState session = this.Session;
                DateTime dateTime2 = Convert.ToDateTime(DateTime.Today);
                DateTime dateTime3 = dateTime2.AddMonths(-1);
                session["PRV_MONTH"] = dateTime3.ToString("MM/dd/yyyy");
                HttpSessionState httpSessionStates = this.Session;
                DateTime dateTime4 = Convert.ToDateTime(DateTime.Today);
                httpSessionStates["CUR_MONTH"] = dateTime4.ToString("MM/dd/yyyy");
                HttpSessionState session1 = this.Session;
                DateTime dateTime5 = Convert.ToDateTime(DateTime.Today);
                DateTime dateTime6 = dateTime5.AddMonths(1);
                session1["NEXT_MONTH"] = dateTime6.ToString("MM/dd/yyyy");
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    this.lblTestFacility1.Visible = false;
                }
            }
            this.LoadCalendarAccordingToYearAndMonth();
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
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
        Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.extddlCaseStatus.Text = billSysCalender.GetOpenCaseStatus(this.txtCompanyID.Text);
            ArrayList arrayLists = new ArrayList();
            arrayLists.Add(this.txtPatientFName.Text);
            arrayLists.Add(this.txtPatientLName.Text);
            arrayLists.Add(this.extddlCaseType.Text);
            arrayLists.Add(this.txtPatientAge.Text);
            arrayLists.Add(this.txtPatientAddress.Text);
            arrayLists.Add(this.TextBox3.Text);
            arrayLists.Add(this.txtPatientPhone.Text);
            arrayLists.Add(this.txtState.Text);
            arrayLists.Add(this.txtCompanyID.Text);
            arrayLists.Add(this.txtMI.Text);
            arrayLists.Add(this.extddlCaseStatus.Text);
            arrayLists.Add(this.extddlInsuranceCompany.Text);
            arrayLists.Add(this.txtRefChartNumber.Text);
            billSysCalender.savePatientForReferringFacility(arrayLists);
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
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            try
            {
                DataTable dataTable = new DataTable();
                this.ds = new DataSet();
                this.ds = (DataSet)this.Session["PatientDataList"];
                dataTable = this.ds.Tables[0].Clone();
                if (this.ds.Tables[0].Rows.Count > 0)
                {
                    if (this.Session["Flag"] != null || this.Session["DataEntryFlag"] != null)
                    {
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "&nbsp;")
                        {
                            this.txtPatientID.Text = "";
                        }
                        else
                        {
                            this.txtPatientID.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "&nbsp;")
                        {
                            this.txtPatientFName.Text = "";
                        }
                        else
                        {
                            this.txtPatientFName.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() == "&nbsp;")
                        {
                            this.txtMI.Text = "";
                        }
                        else
                        {
                            this.txtMI.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "&nbsp;")
                        {
                            this.txtPatientLName.Text = "";
                        }
                        else
                        {
                            this.txtPatientLName.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() == "&nbsp;")
                        {
                            this.txtPatientPhone.Text = "";
                        }
                        else
                        {
                            this.txtPatientPhone.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "&nbsp;")
                        {
                            this.txtPatientAddress.Text = "";
                        }
                        else
                        {
                            this.txtPatientAddress.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString() == "&nbsp;")
                        {
                            this.txtBirthdate.Text = "";
                        }
                        else
                        {
                            this.txtBirthdate.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() == "&nbsp;")
                        {
                            this.txtPatientAge.Text = "";
                        }
                        else
                        {
                            this.txtPatientAge.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "&nbsp;")
                        {
                            this.txtSocialSecurityNumber.Text = "";
                        }
                        else
                        {
                            this.txtSocialSecurityNumber.Text = this.ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                        }
                        if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "&nbsp;")
                        {
                            if (this.ds.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() != "True")
                            {
                                this.chkTransportation.Checked = false;
                            }
                            else
                            {
                                this.chkTransportation.Checked = true;
                            }
                        }
                        if (this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString() == "&nbsp;")
                        {
                            this.extddlPatientState.Text = "NA";
                        }
                        else
                        {
                            this.extddlPatientState.Text = this.ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();
                        }
                        if (this.ds.Tables[0].Rows[0]["CHART NUMBER"].ToString() == "&nbsp;")
                        {
                            this.txtRefChartNumber.Text = "";
                        }
                        else
                        {
                            this.txtRefChartNumber.Text = this.ds.Tables[0].Rows[0]["CHART NUMBER"].ToString();
                        }
                        Billing_Sys_ManageNotesBO billingSysManageNotesBO = new Billing_Sys_ManageNotesBO();
                        DataSet caseDetails = billingSysManageNotesBO.GetCaseDetails(this.txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        if (caseDetails.Tables[0].Rows.Count > 0)
                        {
                            this.txtCaseID.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                            {
                                this.extddlInsuranceCompany.Flag_ID = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                this.extddlCaseType.Flag_ID = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                            }
                            if (caseDetails.Tables[0].Rows[0]["SZ_OFFICE_ID"].ToString() == "")
                            {
                                this.extddlMedicalOffice.Enabled = true;
                            }
                            else
                            {
                                this.extddlMedicalOffice.Text = caseDetails.Tables[0].Rows[0]["SZ_OFFICE_ID"].ToString();
                                this.extddlMedicalOffice.Enabled = false;
                                this.extddlDoctor.Flag_ID = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                                this.extddlDoctor.Flag_Key_Value = "newGETREFFERDOCTORLIST";
                            }
                            this.extddlCaseType.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            this.extddlInsuranceCompany.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        }
                    }
                    else
                    {
                        if (this.txtPatientFirstName.Text != "" && this.txtPatientLastName.Text == "")
                        {
                            DataRow[] dataRowArray = this.ds.Tables[0].Select(string.Concat("SZ_PATIENT_FIRST_NAME LIKE '", this.txtPatientFirstName.Text, "%'"));
                            for (int i = 0; i < (int)dataRowArray.Length; i++)
                            {
                                dataTable.ImportRow(dataRowArray[i]);
                            }
                        }
                        else if (this.txtPatientLastName.Text != "" && this.txtPatientFirstName.Text == "")
                        {
                            DataRow[] dataRowArray1 = this.ds.Tables[0].Select(string.Concat("SZ_PATIENT_LAST_NAME LIKE '", this.txtPatientLastName.Text, "%'"));
                            for (int j = 0; j < (int)dataRowArray1.Length; j++)
                            {
                                dataTable.ImportRow(dataRowArray1[j]);
                            }
                        }
                        else if (this.txtPatientLastName.Text != "" && this.txtPatientFirstName.Text != "")
                        {
                            DataTable item = this.ds.Tables[0];
                            string[] text = new string[] { "SZ_PATIENT_FIRST_NAME LIKE '", this.txtPatientFirstName.Text, "%' AND SZ_PATIENT_LAST_NAME LIKE '", this.txtPatientLastName.Text, "%'" };
                            DataRow[] dataRowArray2 = item.Select(string.Concat(text));
                            for (int k = 0; k < (int)dataRowArray2.Length; k++)
                            {
                                dataTable.ImportRow(dataRowArray2[k]);
                            }
                        }
                        if (dataTable.Rows.Count <= 0)
                        {
                            this.grdPatientList.DataSource = null;
                            this.grdPatientList.DataBind();
                            this.Session["dtView"] = "";
                        }
                        else if (this.Session["Page_Index"].ToString() == "")
                        {
                            this.grdPatientList.CurrentPageIndex = 0;
                            this.grdPatientList.DataSource = dataTable;
                            this.grdPatientList.DataBind();
                        }
                        else
                        {
                            this.grdPatientList.CurrentPageIndex = Convert.ToInt32(this.Session["Page_Index"].ToString());
                            this.grdPatientList.DataSource = dataTable;
                            this.grdPatientList.DataBind();
                        }
                        this.ModalPopupExtender.Show();
                    }
                }
                if ((new Bill_Sys_BillingCompanyDetails_BO()).GetRefDocID(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString()) == "")
                {
                    this.btnAddPatient.Visible = true;
                }
                else
                {
                    this.btnAddPatient.Visible = false;
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
        }
        finally
        {
            this.ModalPopupExtender.Show();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void SelectSavedProcedureCodes(string i_schedule_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["BILLED_EVENT"] = "";
            this.Session["VISIT_COMPLETED"] = "";
            this.ds = new DataSet();
            this._patient_TVBO = new Patient_TVBO();
            this.ds = this._patient_TVBO.GetAppointProcCode(Convert.ToInt32(i_schedule_id));
            DataSet dataSet = new DataSet();
            dataSet = (DataSet)this.grdProcedureCode.DataSource;
            DataSet dataSet1 = new DataSet();
            DataTable dataTable = new DataTable();
            dataSet1.Tables.Add(dataTable);
            dataSet1.Tables[0].Columns.Add("CODE");
            dataSet1.Tables[0].Columns.Add("DESCRIPTION");
            dataSet1.Tables[0].Columns.Add("I_RESCHEDULE_ID");
            dataSet1.Tables[0].Columns.Add("I_EVENT_PROC_ID");
            foreach (DataRow row in this.ds.Tables[0].Rows)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (dataSet.Tables[0].Rows[i][0].ToString().Equals(row.ItemArray.GetValue(0).ToString()))
                    {
                        DataRow str = dataSet1.Tables[0].NewRow();
                        str["CODE"] = dataSet.Tables[0].Rows[i][0].ToString();
                        str["DESCRIPTION"] = dataSet.Tables[0].Rows[i][1].ToString();
                        str["I_RESCHEDULE_ID"] = dataSet.Tables[0].Rows[i][2].ToString();
                        str["I_EVENT_PROC_ID"] = dataSet.Tables[0].Rows[i][3].ToString();
                        dataSet1.Tables[0].Rows.Add(str);
                        dataSet.Tables[0].Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            dataSet1.Tables[0].Merge(dataSet.Tables[0]);
            this.grdProcedureCode.DataSource = dataSet1.Tables[0];
            this.grdProcedureCode.DataBind();
            foreach (DataRow dataRow in this.ds.Tables[0].Rows)
            {
                foreach (DataGridItem item in this.grdProcedureCode.Items)
                {
                    if (item.Cells[1].Text != dataRow.ItemArray.GetValue(0).ToString())
                    {
                        continue;
                    }
                    CheckBox checkBox = (CheckBox)item.Cells[1].FindControl("chkSelect");
                    checkBox.Checked = true;
                    DropDownList dropDownList = (DropDownList)item.Cells[4].FindControl("ddlStatus");
                    dropDownList.SelectedValue = dataRow.ItemArray.GetValue(1).ToString();
                    TextBox textBox = (TextBox)item.Cells[7].FindControl("txtStudyNo");
                    textBox.Text = dataRow.ItemArray.GetValue(7).ToString();
                    TextBox str1 = (TextBox)item.FindControl("txtProcNotes");
                    str1.Text = dataRow.ItemArray.GetValue(8).ToString();
                    if (dropDownList.SelectedValue == "2")
                    {
                        this.lblMsg.Visible = true;
                        this.lblMsg.Text = "Visit completed.";
                        this.lblMsg.ForeColor = Color.Red;
                        this.Session["VISIT_COMPLETED"] = "YES";
                    }
                    if (dataRow.ItemArray.GetValue(2) != DBNull.Value && Convert.ToInt32(dataRow.ItemArray.GetValue(1).ToString()) != 0)
                    {
                        TextBox textBox1 = (TextBox)item.Cells[5].FindControl("txtReScheduleDate");
                        textBox1.Text = dataRow.ItemArray.GetValue(2).ToString();
                        textBox1.ReadOnly = true;
                        string str2 = dataRow.ItemArray.GetValue(3).ToString();
                        DropDownList dropDownList1 = (DropDownList)item.Cells[6].FindControl("ddlReSchHours");
                        dropDownList1.SelectedValue = str2.Substring(0, str2.IndexOf(".")).PadLeft(2, '0');
                        dropDownList1.Enabled = false;
                        DropDownList dropDownList2 = (DropDownList)item.Cells[6].FindControl("ddlReSchMinutes");
                        dropDownList2.SelectedValue = str2.Substring(str2.IndexOf(".") + 1, str2.Length - (str2.IndexOf(".") + 1)).PadLeft(2, '0');
                        dropDownList2.Enabled = false;
                        DropDownList str3 = (DropDownList)item.Cells[6].FindControl("ddlReSchTime");
                        str3.SelectedValue = dataRow.ItemArray.GetValue(4).ToString();
                        str3.Enabled = false;
                        dropDownList.Enabled = false;
                        checkBox.Enabled = false;
                        textBox.ReadOnly = true;
                        str1.ReadOnly = true;
                    }
                    item.BackColor = Color.LightSeaGreen;
                    if (dataRow.ItemArray.GetValue(9).ToString() != "True")
                    {
                        continue;
                    }
                    this.Session["BILLED_EVENT"] = "BILLED";
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

    protected void setControlAccordingOperation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.hdnOperationType.Value.ToString() == "TestFacilityUpdate")
            {
                this.DisplayProcedureGridColumns(true);
                this.extddlMedicalOffice.Visible = true;
                this.extddlReferenceFacility.Visible = false;
                this.lblTestFacility.Text = "Office Name";
                this.btnUpdate.Visible = true;
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "1" || ((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "True")
                {
                    this.btnDeleteEvent.Visible = true;
                }
                else
                {
                    this.btnDeleteEvent.Visible = false;
                }
                this.btnSave.Visible = false;
                this.btnDuplicateSaveClick.Visible = false;
                this.grdProcedureCode.Visible = true;
                this.ddlTestNames.Visible = false;
                this.divProcedureCode.Visible = true;
                this.divProcedureCode.Style.Add("HEIGHT", "250px");
                this.divProcedureCode.Style.Add("OVERFLOW", "scroll");
                this.divProcedureCode.Style.Add("WIDTH", "100%");
                this.lblSSN.Visible = true;
                this.txtSocialSecurityNumber.Visible = true;
                this.lblBirthdate.Visible = true;
                this.txtBirthdate.Visible = true;
                this.lblAge.Visible = true;
                this.txtPatientAge.Visible = true;
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "0")
                {
                    this.lblChartNumber.Visible = true;
                    this.txtRefChartNumber.Visible = true;
                }
                this.txtRefChartNumber.ReadOnly = true;
                this.txtPatientFName.ReadOnly = true;
                this.txtMI.ReadOnly = true;
                this.txtPatientLName.ReadOnly = true;
                this.txtPatientPhone.ReadOnly = true;
                this.txtPatientAddress.ReadOnly = true;
                this.TextBox3.ReadOnly = true;
                this.txtState.ReadOnly = true;
                this.txtBirthdate.ReadOnly = true;
                this.txtPatientAge.ReadOnly = true;
                this.txtSocialSecurityNumber.ReadOnly = true;
                this.extddlInsuranceCompany.Enabled = false;
                this.extddlCaseType.Enabled = false;
                this.extddlMedicalOffice.Enabled = true;
                this.extddlPatientState.Enabled = false;
                this.btnUpdate.Attributes.Add("Onclick", "return val_updateTestFacility();");
                if (this.Session["BILLED_EVENT"].ToString() == "BILLED")
                {
                    this.lblMsg.Text = "Bill generated for selected appointment.";
                    this.btnUpdate.Visible = false;
                    this.btnDeleteEvent.Visible = false;
                    this.Session["BILLED_EVENT"] = "";
                }
            }
            else if (this.hdnOperationType.Value.ToString() != "BillngCompanyUpdate")
            {
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    this.btnDuplicateSaveClick.Attributes.Add("Onclick", "return Val_Add_UpdateVisitForBillingCompany();");
                    this.extddlMedicalOffice.Visible = false;
                    this.extddlReferenceFacility.Visible = true;
                    this.lblTestFacility.Text = "Test Facility";
                    this.lblChartNumber.Visible = false;
                    this.txtRefChartNumber.Visible = false;
                }
                else
                {
                    this.btnDuplicateSaveClick.Attributes.Add("Onclick", "return Val_AddVisitForTestFacility();");
                    this.extddlMedicalOffice.Visible = true;
                    this.extddlMedicalOffice.Enabled = true;
                    this.extddlReferenceFacility.Visible = false;
                    this.lblTypetext.Visible = false;
                    this.ddlType.Visible = false;
                    this.lblTestFacility.Text = "Office Name";
                    if (!(this.extddlMedicalOffice.Text != "NA") && !(this.extddlMedicalOffice.Text != ""))
                    {
                        this.extddlMedicalOffice.Text = "NA";
                    }
                    this.extddlDoctor.Text = "NA";
                    this.extddlTransport.Text = "NA";
                    this.extddlTransport.Visible = false;
                    this.chkTransportation.Checked = false;
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "0")
                    {
                        this.lblChartNumber.Visible = true;
                        this.txtRefChartNumber.Visible = true;
                    }
                }
                this.DisplayProcedureGridColumns(false);
                this.lblSSN.Visible = false;
                this.txtSocialSecurityNumber.Visible = false;
                this.lblBirthdate.Visible = false;
                this.txtBirthdate.Visible = false;
                this.lblAge.Visible = false;
                this.txtPatientAge.Visible = false;
                this.txtNotes.Text = "";
                this.btnUpdate.Visible = false;
                this.btnDeleteEvent.Visible = false;
                this.btnSave.Visible = true;
                this.btnDuplicateSaveClick.Visible = true;
                this.txtRefChartNumber.ReadOnly = true;
                this.txtPatientFName.ReadOnly = true;
                this.txtMI.ReadOnly = true;
                this.txtPatientLName.ReadOnly = true;
                this.txtPatientPhone.ReadOnly = true;
                this.txtPatientAddress.ReadOnly = true;
                this.TextBox3.ReadOnly = true;
                this.txtState.ReadOnly = true;
                this.txtBirthdate.ReadOnly = true;
                this.txtPatientAge.ReadOnly = true;
                this.txtSocialSecurityNumber.ReadOnly = true;
                this.extddlInsuranceCompany.Enabled = false;
                this.extddlCaseType.Enabled = false;
                this.extddlPatientState.Enabled = false;
                if (this.txtPatientFName.Text == "")
                {
                    this.btnClickSearch.Visible = true;
                    this.tdSerach.Visible = true;
                }
            }
            else
            {
                this.DisplayProcedureGridColumns(false);
                this.btnSave.Visible = false;
                this.btnDuplicateSaveClick.Visible = false;
                this.btnUpdate.Visible = true;
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "1" || ((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_DELETE_VIEWS == "True")
                {
                    this.btnDeleteEvent.Visible = true;
                }
                else
                {
                    this.btnDeleteEvent.Visible = false;
                }
                this.lblSSN.Visible = false;
                this.txtSocialSecurityNumber.Visible = false;
                this.lblBirthdate.Visible = false;
                this.txtBirthdate.Visible = false;
                this.lblAge.Visible = false;
                this.txtPatientAge.Visible = false;
                this.lblChartNumber.Visible = false;
                this.txtRefChartNumber.Visible = false;
                this.extddlPatientState.Enabled = false;
                this.btnUpdate.Attributes.Add("Onclick", "return Val_AddVisitForTestFacility();");
                if (this.Session["VISIT_COMPLETED"] != null && this.Session["VISIT_COMPLETED"].ToString() == "YES")
                {
                    this.btnUpdate.Visible = false;
                    this.btnDeleteEvent.Visible = false;
                    this.Session["VISIT_COMPLETED"] = "";
                }
            }
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1")
            {
                this.txtRefChartNumber.Visible = false;
                this.lblChartNumber.Visible = false;
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

    public void setReadOnly(bool value)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtRefChartNumber.ReadOnly = value;
            this.txtPatientFName.ReadOnly = value;
            this.txtMI.ReadOnly = value;
            this.txtPatientLName.ReadOnly = value;
            this.txtPatientPhone.ReadOnly = value;
            this.txtPatientAddress.ReadOnly = value;
            this.TextBox3.ReadOnly = value;
            this.txtState.ReadOnly = value;
            this.txtBirthdate.ReadOnly = value;
            this.txtPatientAge.ReadOnly = value;
            this.txtSocialSecurityNumber.ReadOnly = value;
            this.extddlInsuranceCompany.Enabled = !value;
            this.extddlCaseType.Enabled = !value;
            this.extddlMedicalOffice.Enabled = !value;
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
        this.Panel1.Controls.Add(new LiteralControl("<table border='1' width='80px'>"));
        this.Panel1.Controls.Add(new LiteralControl("<tr><td colspan='7'>"));
        string str = "<div align='center' style='font-size:11px;font-weight:bold'>@LONG_MONTH_NAME@</div>";
        str = str.Replace("@LONG_MONTH_NAME@", this.getLongMonthName(objCalendar.InitialDisplayMonth));
        this.Panel1.Controls.Add(new LiteralControl(str));
        this.Panel1.Controls.Add(new LiteralControl("</td>"));
        this.Panel1.Controls.Add(new LiteralControl("<tr>"));
        byte startIndex = this.getStartIndex(this.getFirstDayOfMonth(objCalendar.InitialDisplayMonth, objCalendar.InitialDisplayYear));
        string[] strArrays = null;
        strArrays = (!this.blnWeekShortNames ? this.getOrderedWeekdays() : this.getShortNamesForWeekdays());
        for (int i = 0; i < (int)strArrays.Length; i++)
        {
            this.Panel1.Controls.Add(new LiteralControl(string.Concat("<td style='width:10px;font-size:9px'>", strArrays[i], "</td>")));
        }
        this.Panel1.Controls.Add(new LiteralControl("</tr>"));
        byte num = 1;
        int k = 0;
        bool flag = true;
        byte todaysDayOfTheMonth = this.getTodaysDayOfTheMonth();
        int num1 = 0;
        int intMonth = 0;
        if (this.txtGetDay.Value.ToString() != "")
        {
            string[] strArrays1 = this.txtGetDay.Value.ToString().Split(new char[] { '\u005F' });
            intMonth = this.getIntMonth(strArrays1.GetValue(0).ToString());
            num1 = Convert.ToInt32(strArrays1.GetValue(1).ToString());
        }
        LinkButton linkButton = null;
        for (int j = 0; j < 6; j++)
        {
            this.Panel1.Controls.Add(new LiteralControl("<tr>"));
            for (k = 0; k < 7; k++)
            {
                if (k >= startIndex || !flag)
                {
                    if (num > this.getLastMonthInteger())
                    {
                        this.Panel1.Controls.Add(new LiteralControl(string.Concat("<td style='width:10px;font-size:9px' bgcolor ='", this.szDateColor_NA, "'> N/A </TD>")));
                    }
                    else if (todaysDayOfTheMonth == num && DateTime.Now.Month == this.getIntMonth(objCalendar.InitialDisplayMonth))
                    {
                        string str1 = "";
                        linkButton = new LinkButton();
                        object[] initialDisplayYear = new object[] { objCalendar.InitialDisplayYear, "_", objCalendar.InitialDisplayMonth, "_Link_", num };
                        linkButton.ID = string.Concat(initialDisplayYear);
                        object[] objArray = new object[] { objCalendar.InitialDisplayYear, "_", objCalendar.InitialDisplayMonth, "_Link_", num };
                        linkButton.CommandArgument = string.Concat(objArray);
                        linkButton.Text = string.Concat("", num);
                        linkButton.Click += new EventHandler(this.Link1_Click);
                        str1 = "<td style='width:10px;font-size:9px' align='center' bgcolor='@BG_COLOR@'>".Replace("@BG_COLOR@", this.szDateColor_TODAY);
                        this.Panel1.Controls.Add(new LiteralControl(str1));
                        this.Panel1.Controls.Add(linkButton);
                        this.Panel1.Controls.Add(new LiteralControl("</td>"));
                    }
                    else if (num1 == num && intMonth == this.getIntMonth(objCalendar.InitialDisplayMonth))
                    {
                        string str2 = "";
                        linkButton = new LinkButton();
                        object[] initialDisplayYear1 = new object[] { objCalendar.InitialDisplayYear, "_", objCalendar.InitialDisplayMonth, "_Link_", num };
                        linkButton.ID = string.Concat(initialDisplayYear1);
                        object[] objArray1 = new object[] { objCalendar.InitialDisplayYear, "_", objCalendar.InitialDisplayMonth, "_Link_", num };
                        linkButton.CommandArgument = string.Concat(objArray1);
                        linkButton.Text = string.Concat("", num);
                        AttributeCollection attributes = linkButton.Attributes;
                        string[] initialDisplayMonth = new string[] { "  document.getElementById('ctl00_ContentPlaceHolder1_txtGetDay').value='", objCalendar.InitialDisplayMonth, "_", num.ToString(), "';" };
                        attributes.Add("onclick", string.Concat(initialDisplayMonth));
                        linkButton.Click += new EventHandler(this.Link1_Click);
                        str2 = "<td style='width:10px;font-size:9px' align='center' bgcolor='#7FFF00'>".Replace("#7FFF00", "#7FFF00");
                        this.Panel1.Controls.Add(new LiteralControl(str2));
                        this.Panel1.Controls.Add(linkButton);
                        this.Panel1.Controls.Add(new LiteralControl("</td>"));
                    }
                    else if (this.Session["FROM"] != null)
                    {
                        linkButton = new LinkButton();
                        object[] initialDisplayYear2 = new object[] { objCalendar.InitialDisplayYear, "_", objCalendar.InitialDisplayMonth, "_Link_", num };
                        linkButton.ID = string.Concat(initialDisplayYear2);
                        object[] objArray2 = new object[] { objCalendar.InitialDisplayYear, "_", objCalendar.InitialDisplayMonth, "_Link_", num };
                        linkButton.CommandArgument = string.Concat(objArray2);
                        linkButton.Text = string.Concat("", num);
                        linkButton.Click += new EventHandler(this.Link1_Click);
                        this.Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' align='center'>"));
                        this.Panel1.Controls.Add(linkButton);
                        this.Panel1.Controls.Add(new LiteralControl("</td>"));
                    }
                    else
                    {
                        linkButton = new LinkButton();
                        object[] initialDisplayYear3 = new object[] { objCalendar.InitialDisplayYear, "_", objCalendar.InitialDisplayMonth, "_Link_", num };
                        linkButton.ID = string.Concat(initialDisplayYear3);
                        object[] objArray3 = new object[] { objCalendar.InitialDisplayYear, "_", objCalendar.InitialDisplayMonth, "_Link_", num };
                        linkButton.CommandArgument = string.Concat(objArray3);
                        linkButton.Text = string.Concat("", num);
                        linkButton.Click += new EventHandler(this.Link1_Click);
                        AttributeCollection attributeCollection = linkButton.Attributes;
                        string[] initialDisplayMonth1 = new string[] { "  document.getElementById('ctl00_ContentPlaceHolder1_txtGetDay').value='", objCalendar.InitialDisplayMonth, "_", num.ToString(), "';" };
                        attributeCollection.Add("onclick", string.Concat(initialDisplayMonth1));
                        this.Panel1.Controls.Add(new LiteralControl("<td style='width:10px;font-size:9px' align='center'>"));
                        this.Panel1.Controls.Add(linkButton);
                        this.Panel1.Controls.Add(new LiteralControl("</td>"));
                    }
                    num = (byte)(num + 1);
                }
                else
                {
                    this.Panel1.Controls.Add(new LiteralControl(string.Concat("<td style='width:10px;font-size:9px' bgcolor='", this.szDateColor_NA, "'> N/A </TD>")));
                }
            }
            flag = false;
            this.Panel1.Controls.Add(new LiteralControl("</tr>"));
        }
        this.Panel1.Controls.Add(new LiteralControl("</table>"));
    }

    protected void updateAppointmentFromBillingCompany()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string text = "";
            if (this.txtPatientFName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Patient First Name should not be Empty.')", true);
                this.ModalPopupExtender.Show();
            }
            else if (this.txtPatientLName.Text.Trim().ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Patient Last name should not be Empty.')", true);
                this.ModalPopupExtender.Show();
            }
            else if (this.ddlType.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please Select any one Type.')", true);
                this.ModalPopupExtender.Show();
            }
            else if (this.extddlDoctor.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please Select any one Referring Doctor.')", true);
                this.ModalPopupExtender.Show();
            }
            else if (!this.chkTransportation.Checked || !(this.extddlTransport.Text == "NA"))
            {
                int num = Convert.ToInt32(this.Session["SCHEDULEDID"].ToString());
                string value = this.hdnObj.Value;
                value = value.Substring(0, value.IndexOf(" "));
                Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
                ArrayList arrayLists = new ArrayList();
                arrayLists.Add(this.txtPatientID.Text);
                arrayLists.Add(value);
                arrayLists.Add(string.Concat(this.ddlHours.SelectedValue.ToString(), ".", this.ddlMinutes.SelectedValue.ToString()));
                arrayLists.Add(this.txtNotes.Text);
                arrayLists.Add(this.extddlDoctor.Text);
                text = "";
                int num1 = 0;
                if (num1 < this.grdProcedureCode.Items.Count)
                {
                    text = this.grdProcedureCode.Items[num1].Cells[1].Text;
                }
                if (text == null)
                {
                    arrayLists.Add("");
                }
                else
                {
                    arrayLists.Add(text);
                }
                arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                arrayLists.Add(this.ddlTime.SelectedValue);
                arrayLists.Add(string.Concat(this.ddlEndHours.SelectedValue.ToString(), ".", this.ddlEndMinutes.SelectedValue.ToString()));
                arrayLists.Add(this.ddlEndTime.SelectedValue);
                arrayLists.Add(this.extddlReferenceFacility.Text);
                arrayLists.Add(num);
                if (!this.chkTransportation.Checked)
                {
                    arrayLists.Add(0);
                }
                else
                {
                    arrayLists.Add(1);
                }
                if (!this.chkTransportation.Checked || !(this.extddlTransport.Text != "NA"))
                {
                    arrayLists.Add(null);
                }
                else
                {
                    arrayLists.Add(Convert.ToInt32(this.extddlTransport.Text));
                }
                DateTime dateTime = new DateTime();
                dateTime = Convert.ToDateTime(value);
                if (this.ddlTime.SelectedValue != "AM")
                {
                    int num2 = 0;
                    num2 = (Convert.ToInt32(this.ddlHours.SelectedValue) != 12 ? Convert.ToInt32(this.ddlHours.SelectedValue) + 12 : Convert.ToInt32(this.ddlHours.SelectedValue));
                    DateTime dateTime1 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, num2, Convert.ToInt32(this.ddlEndMinutes.SelectedValue), 0);
                }
                else
                {
                    DateTime dateTime2 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt32(this.ddlHours.SelectedValue), Convert.ToInt32(this.ddlEndMinutes.SelectedValue), 0);
                }
                num = billSysCalender.UPDATE_Event_Referral(arrayLists, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                billSysCalender.Delete_Event_RefferPrcedure(num);
                for (int i = 0; i < this.grdProcedureCode.Items.Count; i++)
                {
                    if (((CheckBox)this.grdProcedureCode.Items[i].FindControl("chkSelect")).Checked)
                    {
                        text = this.grdProcedureCode.Items[i].Cells[1].Text;
                        arrayLists = new ArrayList();
                        arrayLists.Add(text);
                        arrayLists.Add(num);
                        if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            arrayLists.Add(0);
                        }
                        else
                        {
                            arrayLists.Add(2);
                        }
                        billSysCalender.Save_Event_RefferPrcedure(arrayLists);
                    }
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO()
                {
                    SZ_MESSAGE_TITLE = "APPOINTMENT_UPDATED",
                    SZ_ACTIVITY_DESC = string.Concat("Date : ", value)
                };
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = this.txtCaseID.Text;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                this.Session["PopUp"] = "True";
                string[] strArrays = this.hdnObj.Value.ToString().Split(new char[] { '/' });
                string[] str = new string[] { strArrays[0], "/", strArrays[1], "/", strArrays[2].Substring(0, 4).ToString() };
                string str1 = string.Concat(str);
                if (str1 != "")
                {
                    if (this.extddlReferringFacility.Visible)
                    {
                        DateTime dateTime3 = Convert.ToDateTime(str1);
                        this.GetCalenderDayAppointments(dateTime3.ToString("MM/dd/yyyy"), this.extddlReferringFacility.Text);
                    }
                    else
                    {
                        DateTime dateTime4 = Convert.ToDateTime(str1);
                        this.GetCalenderDayAppointments(dateTime4.ToString("MM/dd/yyyy"), this.txtCompanyID.Text);
                    }
                }
                this.ClearValues();
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Appointment updated successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('please select transport company.')", true);
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

    protected void updateAppointmentFromTestFacility()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string[] str;
        DateTime dateTime;
        char[] chrArray;
        int num = 0;
        try
        {
            bool flag = false;
            if (!this.chkTransportation.Checked || !(this.extddlTransport.Text == "NA"))
            {
                foreach (DataGridItem item in this.grdProcedureCode.Items)
                {
                    if (!((CheckBox)item.Cells[1].FindControl("chkSelect")).Checked || !(((DropDownList)item.Cells[4].FindControl("ddlStatus")).SelectedValue != "0"))
                    {
                        continue;
                    }
                    num = 1;
                }
                if (num == 1)
                {
                    foreach (DataGridItem dataGridItem in this.grdProcedureCode.Items)
                    {
                        if (!((CheckBox)dataGridItem.Cells[1].FindControl("chkSelect")).Checked)
                        {
                            continue;
                        }
                        DropDownList dropDownList = (DropDownList)dataGridItem.Cells[4].FindControl("ddlStatus");
                        if (dropDownList.SelectedValue != "0")
                        {
                            if (Convert.ToInt32(dropDownList.SelectedValue) != 1)
                            {
                                continue;
                            }
                            TextBox textBox = (TextBox)dataGridItem.Cells[5].FindControl("txtReScheduleDate");
                            if (textBox.Text != "")
                            {
                                try
                                {
                                    DateTime.Parse(textBox.Text);
                                }
                                catch (Exception exception)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please enter valid rescheduled date.')", true);
                                    this.ModalPopupExtender.Show();
                                    return;
                                }
                                DropDownList dropDownList1 = (DropDownList)dataGridItem.Cells[6].FindControl("ddlReSchHours");
                                DropDownList dropDownList2 = (DropDownList)dataGridItem.Cells[6].FindControl("ddlReSchMinutes");
                                DropDownList dropDownList3 = (DropDownList)dataGridItem.Cells[6].FindControl("ddlReSchTime");
                                if (dropDownList1.SelectedValue != "00")
                                {
                                    str = new string[7];
                                    dateTime = Convert.ToDateTime(textBox.Text);
                                    str[0] = dateTime.ToString("MM/dd/yyyy");
                                    str[1] = " ";
                                    str[2] = dropDownList1.SelectedValue;
                                    str[3] = ":";
                                    str[4] = dropDownList2.SelectedValue;
                                    str[5] = " ";
                                    str[6] = dropDownList3.SelectedValue;
                                    DateTime dateTime1 = Convert.ToDateTime(string.Concat(str));
                                    DateTime dateTime2 = dateTime1.AddMinutes(30);
                                    string value = this.hdnObj.Value;
                                    chrArray = new char[] { '!' };
                                    string str1 = value.Split(chrArray)[1];
                                    chrArray = new char[] { '~' };
                                    string str2 = str1.Split(chrArray)[0].ToString();
                                    Bill_Sys_RoomDays billSysRoomDay = new Bill_Sys_RoomDays();
                                    ArrayList arrayLists = new ArrayList();
                                    arrayLists.Add(str2);
                                    dateTime = Convert.ToDateTime(textBox.Text);
                                    arrayLists.Add(dateTime.ToString("MM/dd/yyyy"));
                                    arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    int hour = dateTime1.Hour;
                                    string str3 = hour.ToString();
                                    hour = dateTime1.Minute;
                                    arrayLists.Add(string.Concat(str3, ".", hour.ToString()));
                                    hour = dateTime2.Hour;
                                    string str4 = hour.ToString();
                                    hour = dateTime2.Minute;
                                    arrayLists.Add(string.Concat(str4, ".", hour.ToString()));
                                    if (billSysRoomDay.checkRoomTiming(arrayLists))
                                    {
                                        continue;
                                    }
                                    dateTime = Convert.ToDateTime(textBox.Text);
                                    string roomStartEndTime = billSysRoomDay.getRoomStart_EndTime(str2, dateTime.ToString("MM/dd/yyyy"), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    Button button = this.btnUpdate;
                                    Type type = typeof(Button);
                                    str = new string[] { "alert('Please add visit on ", null, null, null, null };
                                    dateTime = Convert.ToDateTime(textBox.Text);
                                    str[1] = dateTime.ToString("MM/dd/yyyy");
                                    str[2] = " between ";
                                    str[3] = roomStartEndTime;
                                    str[4] = ".')";
                                    ScriptManager.RegisterClientScriptBlock(button, type, "Msg", string.Concat(str), true);
                                    this.ModalPopupExtender.Show();
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please enter valid rescheduled time.')", true);
                                    this.ModalPopupExtender.Show();
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please enter valid rescheduled date.')", true);
                                this.ModalPopupExtender.Show();
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('Please select procedure status.')", true);
                            this.ModalPopupExtender.Show();
                            return;
                        }
                    }
                }
                if (!flag)
                {
                    int num1 = Convert.ToInt32(this.Session["SCHEDULEDID"].ToString());
                    Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
                    if (this.extddlMedicalOffice.Visible && this.extddlMedicalOffice.Text != "" && this.extddlMedicalOffice.Text != "NA")
                    {
                        ArrayList arrayLists1 = new ArrayList();
                        arrayLists1.Add(this.txtPatientID.Text);
                        arrayLists1.Add(this.extddlMedicalOffice.Text);
                        arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        arrayLists1.Add(this.extddlDoctor.Text);
                        billSysCalender.Update_Office_Id(arrayLists1);
                    }
                    ArrayList arrayLists2 = new ArrayList();
                    billSysCalender.Delete_Event_RefferPrcedure(num1);
                    ArrayList arrayLists3 = new ArrayList();
                    foreach (DataGridItem lightSeaGreen in this.grdProcedureCode.Items)
                    {
                        CheckBox checkBox = (CheckBox)lightSeaGreen.Cells[1].FindControl("chkSelect");
                        if (!checkBox.Checked)
                        {
                            continue;
                        }
                        TextBox textBox1 = (TextBox)lightSeaGreen.Cells[5].FindControl("txtStudyNo");
                        TextBox textBox2 = (TextBox)lightSeaGreen.Cells[8].FindControl("txtProcNotes");
                        DropDownList dropDownList4 = (DropDownList)lightSeaGreen.Cells[4].FindControl("ddlStatus");
                        arrayLists2 = new ArrayList();
                        arrayLists2.Add(lightSeaGreen.Cells[1].Text);
                        arrayLists2.Add(num1);
                        arrayLists2.Add(dropDownList4.SelectedValue);
                        int eventID = 0;
                        if (Convert.ToInt32(dropDownList4.SelectedValue) == 1)
                        {
                            TextBox textBox3 = (TextBox)lightSeaGreen.Cells[5].FindControl("txtReScheduleDate");
                            DropDownList dropDownList5 = (DropDownList)lightSeaGreen.Cells[6].FindControl("ddlReSchHours");
                            DropDownList dropDownList6 = (DropDownList)lightSeaGreen.Cells[6].FindControl("ddlReSchMinutes");
                            DropDownList dropDownList7 = (DropDownList)lightSeaGreen.Cells[6].FindControl("ddlReSchTime");
                            if (arrayLists3.Count > 0)
                            {
                                foreach (AddedEvetDetail arrayList in arrayLists3)
                                {
                                    if (!(arrayList.EventDate == Convert.ToDateTime(textBox3.Text)) || !(arrayList.EventTime == Convert.ToDecimal(string.Concat(dropDownList5.SelectedValue.ToString(), ".", dropDownList6.SelectedValue.ToString()))))
                                    {
                                        continue;
                                    }
                                    eventID = arrayList.EventID;
                                }
                            }
                            if (eventID == 0 && checkBox.Enabled)
                            {
                                Bill_Sys_Calender billSysCalender1 = new Bill_Sys_Calender();
                                ArrayList arrayLists4 = new ArrayList();
                                arrayLists4.Add(this.txtPatientID.Text);
                                arrayLists4.Add(textBox3.Text);
                                arrayLists4.Add(string.Concat(dropDownList5.SelectedValue.ToString(), ".", dropDownList6.SelectedValue.ToString()));
                                arrayLists4.Add(this.txtNotes.Text);
                                arrayLists4.Add(this.extddlDoctor.Text);
                                arrayLists4.Add(lightSeaGreen.Cells[1].Text);
                                arrayLists4.Add(this.txtPatientCompany.Text);
                                arrayLists4.Add(dropDownList7.SelectedValue);
                                decimal num2 = Convert.ToDecimal(string.Concat(dropDownList5.SelectedValue.ToString(), ".", dropDownList6.SelectedValue.ToString()));
                                num2 = num2 + Convert.ToDecimal(string.Concat("0.", this.Session["INTERVAL"].ToString()));
                                string selectedValue = dropDownList7.SelectedValue;
                                if (Convert.ToDecimal(string.Concat(dropDownList5.SelectedValue.ToString(), ".", dropDownList6.SelectedValue.ToString())) < Convert.ToDecimal(12) && num2 >= Convert.ToDecimal(12))
                                {
                                    if (selectedValue == "AM")
                                    {
                                        selectedValue = "PM";
                                    }
                                    else if (selectedValue == "PM")
                                    {
                                        selectedValue = "AM";
                                    }
                                }
                                arrayLists4.Add(num2);
                                arrayLists4.Add(selectedValue);
                                arrayLists4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                arrayLists4.Add("False");
                                if (!this.chkTransportation.Checked)
                                {
                                    arrayLists4.Add(0);
                                }
                                else
                                {
                                    arrayLists4.Add(1);
                                }
                                if (!this.chkTransportation.Checked)
                                {
                                    arrayLists4.Add(0);
                                }
                                else
                                {
                                    arrayLists4.Add(Convert.ToInt32(this.extddlTransport.Text));
                                }
                                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                                {
                                    arrayLists4.Add(this.extddlMedicalOffice.Text);
                                }
                                eventID = billSysCalender1.Save_Event(arrayLists4, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                                AddedEvetDetail addedEvetDetail = new AddedEvetDetail()
                                {
                                    EventID = eventID,
                                    EventDate = Convert.ToDateTime(textBox3.Text),
                                    EventTime = Convert.ToDecimal(string.Concat(dropDownList5.SelectedValue.ToString(), ".", dropDownList6.SelectedValue.ToString()))
                                };
                                arrayLists3.Add(addedEvetDetail);
                            }
                            if (eventID != 0)
                            {
                                ArrayList arrayLists5 = new ArrayList();
                                arrayLists5.Add(lightSeaGreen.Cells[1].Text);
                                arrayLists5.Add(eventID);
                                arrayLists5.Add(0);
                                billSysCalender.Save_Event_RefferPrcedure(arrayLists5);
                            }
                            arrayLists2.Add(textBox3.Text);
                            arrayLists2.Add(string.Concat(dropDownList5.SelectedValue.ToString(), ".", dropDownList6.SelectedValue.ToString()));
                            arrayLists2.Add(dropDownList7.SelectedValue);
                            arrayLists2.Add(eventID);
                            arrayLists2.Add(textBox1.Text);
                            arrayLists2.Add(textBox2.Text);
                            billSysCalender.Save_Event_RefferPrcedure(arrayLists2);
                        }
                        else if (Convert.ToInt32(dropDownList4.SelectedValue) != 2)
                        {
                            Bill_Sys_Calender billSysCalender2 = new Bill_Sys_Calender();
                            arrayLists2.Add(textBox1.Text);
                            arrayLists2.Add(textBox2.Text);
                            billSysCalender.Save_Event_OtherVType(arrayLists2);
                        }
                        else
                        {
                            Bill_Sys_Calender billSysCalender3 = new Bill_Sys_Calender();
                            arrayLists2.Add(textBox1.Text);
                            arrayLists2.Add(textBox2.Text);
                            billSysCalender.Save_Event_OtherVType(arrayLists2);
                            Bill_Sys_ReferalEvent billSysReferalEvent = new Bill_Sys_ReferalEvent();
                            ArrayList arrayLists6 = new ArrayList();
                            arrayLists6.Add(this.extddlDoctor.Text);
                            arrayLists6.Add(lightSeaGreen.Cells[1].Text);
                            arrayLists6.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            arrayLists6.Add(lightSeaGreen.Cells[1].Text);
                            billSysReferalEvent.AddDoctorAmount(arrayLists6);
                        }
                        lightSeaGreen.BackColor = Color.LightSeaGreen;
                        ArrayList arrayLists7 = new ArrayList();
                        TextBox textBox4 = (TextBox)lightSeaGreen.Cells[5].FindControl("txtReScheduleDate");
                        DropDownList dropDownList8 = (DropDownList)lightSeaGreen.Cells[6].FindControl("ddlReSchHours");
                        DropDownList dropDownList9 = (DropDownList)lightSeaGreen.Cells[6].FindControl("ddlReSchMinutes");
                        DropDownList dropDownList10 = (DropDownList)lightSeaGreen.Cells[6].FindControl("ddlReSchTime");
                        decimal num3 = Convert.ToDecimal(string.Concat(dropDownList8.SelectedValue.ToString(), ".", dropDownList9.SelectedValue.ToString()));
                        num3 = num3 + Convert.ToDecimal(string.Concat("0.", this.Session["INTERVAL"].ToString()));
                        string selectedValue1 = dropDownList10.SelectedValue;
                        if (Convert.ToDecimal(string.Concat(dropDownList8.SelectedValue.ToString(), ".", dropDownList9.SelectedValue.ToString())) < Convert.ToDecimal(12) && num3 >= Convert.ToDecimal(12))
                        {
                            if (selectedValue1 == "AM")
                            {
                                selectedValue1 = "PM";
                            }
                            else if (selectedValue1 == "PM")
                            {
                                selectedValue1 = "AM";
                            }
                        }
                        arrayLists7.Add(lightSeaGreen.Cells[1].Text);
                        arrayLists7.Add(eventID);
                        arrayLists7.Add(dropDownList4.SelectedValue);
                        arrayLists7.Add(textBox1.Text);
                        arrayLists7.Add(textBox2.Text);
                        arrayLists7.Add(textBox4.Text);
                        arrayLists7.Add(num3.ToString());
                        arrayLists7.Add(selectedValue1.ToString());
                        if (dropDownList4.SelectedValue == "1")
                        {
                            continue;
                        }
                        billSysCalender.Update_ReShedule_Info(arrayLists7);
                    }
                    billSysCalender = new Bill_Sys_Calender();
                    arrayLists2 = new ArrayList();
                    arrayLists2.Add(num1);
                    arrayLists2.Add(false);
                    arrayLists2.Add(this.txtNotes.Text);
                    billSysCalender.UPDATE_EventNotes_Status(arrayLists2, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    ArrayList arrayLists8 = new ArrayList();
                    arrayLists8.Add(num1);
                    if (!this.chkTransportation.Checked)
                    {
                        arrayLists8.Add(0);
                    }
                    else
                    {
                        arrayLists8.Add(1);
                    }
                    if (!this.chkTransportation.Checked)
                    {
                        arrayLists8.Add(null);
                    }
                    else
                    {
                        arrayLists8.Add(Convert.ToInt32(this.extddlTransport.Text));
                    }
                    billSysCalender.UPDATE_TransportationCompany_Event(arrayLists8);
                    if (this.extddlDoctor.Visible && this.extddlDoctor.Text != "" && this.extddlDoctor.Text != "NA")
                    {
                        ArrayList arrayLists9 = new ArrayList();
                        arrayLists9.Add(num1);
                        arrayLists9.Add(this.extddlDoctor.Text);
                        billSysCalender.Update_Doctor_Id(arrayLists9);
                    }
                    string str5 = this.hdnObj.Value.ToString();
                    chrArray = new char[] { '/' };
                    string[] strArrays = str5.Split(chrArray);
                    str = new string[] { strArrays[0], "/", strArrays[1], "/", strArrays[2].Substring(0, 4).ToString() };
                    string str6 = string.Concat(str);
                    if (str6 != "")
                    {
                        dateTime = Convert.ToDateTime(str6);
                        this.GetCalenderDayAppointments(dateTime.ToString("MM/dd/yyyy"), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                    this.ClearValues();
                    this.Session["PopUp"] = "True";
                    if (base.Request.QueryString["From"] != null)
                    {
                        if (base.Request.QueryString["GRD_ID"] != null)
                        {
                            this.Session["GRD_ID"] = base.Request.QueryString["GRD_ID"].ToString();
                        }
                        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('div1').style.visibility = 'hidden';window.parent.document.location.href='Bill_SysPatientDesk.aspx';</script>");
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_AppointPatientEntry.aspx';</script>");
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.btnUpdate, typeof(Button), "Msg", "alert('please select transport company.')", true);
                this.ModalPopupExtender.Show();
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