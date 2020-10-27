using AjaxControlToolkit;
using ASP;
using Componend;
using DevExpress.Web;
using DoseSpot.ClientTools.HelperClasses;
using Elmah;
using ExtendedDropDownList;
using log4net;
using NOTES_OBJECT;
using NotesComponent;
using PDFValueReplacement;
using Reminders;
using SautinSoft;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XGridPagination;
using XGridSearchTextBox;
using XGridView;

public partial class Bill_Sys_CaseDetails : PageBase, System.Web.SessionState.IRequiresSessionState
{
    private SqlConnection sqlCon;
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    private SaveOperation _saveOperation;

    private EditOperation _editOperation;

    private ListOperation _listOperation;

    private Billing_Sys_ManageNotesBO _manageNotesBO;

    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;

    private System.Collections.ArrayList _arrayList;

    private Bill_Sys_PatientBO _bill_Sys_PatientBO;

    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;

    private NotesOperation _notesOperation;

    private CaseDetailsBO _caseDetailsBO;

    private Bill_Sys_MemoBO _memoBO;

    private Patient_TVBO _patient_tvbo;

    private DoseSpotPatient currentPatient = new DoseSpotPatient();

    private string Key = ConfigurationManager.AppSettings["DosespotKey"].ToString();

    public string caseID = "";

    private string associatecaseno = "";

    private string associtecasenoAllow = "";

    private string associatecasenoNotAllow = "";

    private string associatecasenoUpdate = "";

    private string associatecasenoNull = "";

    private bool updateFlag = false;

    private Regex commonrange = new Regex("[^0-9)]");

    private static ILog log = LogManager.GetLogger("Bill_Sys_Casedetails");


    public void adjuster()
    {
        try
        {
            this.ClearAdjusterControl();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet adjusterDetail = this._bill_Sys_PatientBO.GetAdjusterDetail(this.extddlAdjuster.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "FORTEST");
            if (adjusterDetail.Tables[0].Rows.Count > 0)
            {
                this.hdadjusterCode.Value = this.extddlAdjuster.Text;
                this.txtAddress.Text = adjusterDetail.Tables[0].Rows[0]["SZ_ADDRESS"].ToString();
                this.txtCity.Text = adjusterDetail.Tables[0].Rows[0]["SZ_CITY"].ToString();
                this.txtZip.Text = adjusterDetail.Tables[0].Rows[0]["SZ_ZIP"].ToString();
                this.txtAdjusterState.Text = adjusterDetail.Tables[0].Rows[0]["SZ_STATE"].ToString();
                this.txtAdjusterPhone.Text = adjusterDetail.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                this.txtAdjusterExtension.Text = adjusterDetail.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                this.txtfax.Text = adjusterDetail.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                this.txtEmail.Text = adjusterDetail.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            }
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.tabcontainerPatientEntry.ActiveTabIndex = 2;
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    public bool allowAssociate()
    {
        string text = this.txtAssociateCases.Text;
        bool flag = false;
        string text2;
        if (!this.commonrange.IsMatch(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString()))
        {
            text2 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
        }
        else
        {
            text2 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString().Remove(0, 2);
        }
        bool result;
        try
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new System.FormatException();
            }
            if (text.IndexOf(',') == -1)
            {
                text += ",";
            }
            if (!this.txtInsuranceAddress.Text.Equals(""))
            {
                string text3 = this.lstInsuranceCompanyAddress.Text;
            }
            string[] array = text.Split(new char[]
            {
                ','
            });
            if (!array[1].ToString().Equals(""))
            {
                this.updateFlag = false;
                if (!this.CheckUpdate(array, 0))
                {
                    result = false;
                    return result;
                }
            }
            this.updateFlag = true;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != "")
                {
                    Regex regex = new Regex("[^0-9]");
                    string text4;
                    if (regex.IsMatch(array[i]))
                    {
                        text4 = array[i].Remove(0, 2);
                    }
                    else
                    {
                        text4 = array[i].ToString();
                    }
                    Patient_TVBO patient_TVBO = new Patient_TVBO();
                    string text5 = patient_TVBO.SavetoSaveToAllowed(text4, this.txtCompanyID.Text, text2, "InsAddress");
                    if (text5 != null)
                    {
                        if (!(text5 == "Same"))
                        {
                            if (!(text5 == "Update"))
                            {
                                if (text5 == "NotAllowed")
                                {
                                    result = false;
                                    return result;
                                }
                                if (text5 == "null")
                                {
                                    this.associtecasenoAllow = this.associtecasenoAllow + text4.ToString() + ",";
                                    this.associatecasenoNull = this.associatecasenoNull + array[i].ToString() + ",";
                                    flag = false;
                                }
                            }
                            else
                            {
                                if (this.CheckUpdate(array, i))
                                {
                                    patient_TVBO.UpdatetoSaveToAllowed(text4.ToString(), this.txtCompanyID.Text, text2, "InsAddressUpdate");
                                    this.associtecasenoAllow = this.associtecasenoAllow + text4.ToString() + ",";
                                    this.LoadDataOnPage();
                                    result = true;
                                    return result;
                                }
                                result = false;
                                return result;
                            }
                        }
                        else
                        {
                            this.associtecasenoAllow = this.associtecasenoAllow + text4.ToString() + ",";
                            flag = true;
                        }
                    }
                }
            }
        }
        catch (System.FormatException)
        {
        }
        result = flag;
        return result;
    }

    protected void BindSuppliesGrid()
    {
        try
        {
            this.grdAssignSupplies.DataSource = new CaseDetailsBO().GetCaseSupplies(this.txtCaseID.Text, this.txtCompanyID.Text);
            this.grdAssignSupplies.DataBind();
            for (int i = 0; i < this.grdAssignSupplies.Items.Count; i++)
            {
                if (this.grdAssignSupplies.Items[i].Cells[1].Text == "1")
                {
                    System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)this.grdAssignSupplies.Items[i].FindControl("chkAssignSupplies");
                    checkBox.Checked = true;
                }
            }
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void btnDosespot_Click(object sender, EventArgs e)
    {
        string str = "";
        this.currentPatient.Prefix = "";
        this.currentPatient.Suffix = "";
        this.currentPatient.FirstName = this.txtPatientFName.Text.Trim();
        this.currentPatient.MiddleName = this.txtMI.Text.Trim();
        this.currentPatient.LastName = this.txtPatientLName.Text.Trim();
        this.currentPatient.Address1 = this.txtPatientAddress.Text.Trim();
        this.currentPatient.Address2 = "";
        this.currentPatient.City = this.txtPatientCity.Text.Trim();
        this.currentPatient.State = this.extddlPatientState.Selected_Text.ToString().Trim();
        this.currentPatient.ZipCode = this.txtPatientZip.Text.Trim();
        if (this.txtDateOfBirth.Text.Trim().Equals(""))
        {
            str = string.Concat(str, "Date of Birth cannot be blank! ");
        }
        else
        {
            this.currentPatient.DateOfBirth = Convert.ToDateTime(this.txtDateOfBirth.Text.Trim());
        }
        this.currentPatient.Email = this.txtPatientEmail.Text.Trim();
        this.currentPatient.Gender = this.ddlSex.SelectedValue.Trim();
        this.currentPatient.PrimaryPhone = this.txtPatientPhone.Text.Trim();
        this.currentPatient.PrimaryPhoneType = "Home";
        if (this.txtCellNo.Text.Trim().Equals(""))
        {
            this.currentPatient.PhoneAdditional1 = "";
            this.currentPatient.PhoneAdditionalType1 = "";
        }
        else
        {
            this.currentPatient.PhoneAdditional1 = this.txtCellNo.Text.Trim();
            this.currentPatient.PhoneAdditionalType1 = "Cell";
        }
        this.currentPatient.PhoneAdditional2 = "";
        this.currentPatient.PhoneAdditionalType2 = "";
        this.currentPatient.Height = new float?(0f);
        this.currentPatient.Weight = new float?(0f);
        this.currentPatient.HeightMetric = new float?(0f);
        this.currentPatient.WeightMetric = new float?(0f);
        if (!this.txtDosespotPatientId.Text.Trim().Equals(""))
        {
            this.currentPatient.PatientId = new int?(Convert.ToInt32(this.txtDosespotPatientId.Text.Trim()));
        }
        else
        {
            this.currentPatient.PatientId = new int?(0);
        }
        List<ValidationResult> validationResults = this.currentPatient.Validate();
        for (int i = 0; i < validationResults.Count; i++)
        {
            str = string.Concat(str, validationResults[i].ErrorDescription, ", ");
        }
        str = str.Replace("'", "");
        if (!DoseSpotPatientValidation.IsValid(validationResults))
        {
            this.Page.RegisterStartupScript("mm", string.Concat("<script language='javascript'>alert('", str, "');</script>"));
            return;
        }
        if (!str.Equals(""))
        {
            this.Page.RegisterStartupScript("mm", string.Concat("<script language='javascript'>alert('", str, "');</script>"));
            return;
        }
        try
        {
            string singleSignOnQueryStringForPatient = SingleSignOnUtils.GetSingleSignOnQueryStringForPatient(this.Key, Convert.ToInt32(this.txtClinicId.Text), Convert.ToInt32(this.txtDosespotUserId.Text), this.currentPatient);
            string singleSignOnPageLocation = SingleSignOnUtils.GetSingleSignOnPageLocation(ConfigurationManager.AppSettings["DoseSpotBaseURL"].ToString(), Convert.ToBoolean(ConfigurationManager.AppSettings["DosespotProductionBit"].ToString()));
            int? patientId = this.currentPatient.PatientId;
            if ((patientId.GetValueOrDefault() != 0 ? true : !patientId.HasValue))
            {
                base.Response.Redirect(string.Concat(singleSignOnPageLocation, singleSignOnQueryStringForPatient));
            }
            else
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(singleSignOnPageLocation, singleSignOnQueryStringForPatient));
                httpWebRequest.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                string item = "";
                item = response.Headers["Location"];
                response.Close();
                if (!item.Equals(""))
                {
                    this.currentPatient.PatientId = new int?(Convert.ToInt32(item.Substring(item.IndexOf("=") + 1)));
                    this.SaveDosespotPatientID(Convert.ToInt32(this.currentPatient.PatientId));
                    singleSignOnQueryStringForPatient = SingleSignOnUtils.GetSingleSignOnQueryStringForPatient(this.Key, Convert.ToInt32(this.txtClinicId.Text), Convert.ToInt32(this.txtDosespotUserId.Text), this.currentPatient);
                    base.Response.Redirect(string.Concat(singleSignOnPageLocation, singleSignOnQueryStringForPatient));
                }
                else
                {
                    base.Response.Redirect(string.Concat(singleSignOnPageLocation, singleSignOnQueryStringForPatient));
                }
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            this.usrMessage3.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            this.usrMessage3.PutMessage(exception.ToString());
            this.usrMessage3.Show();
        }
    }
    private void SaveDosespotPatientID(int iPatientID)
    {
        try
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            SqlCommand sqlCommand = new SqlCommand("SP_DOSESPOT_UPDATE_PATIENT_ID", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@PATIENT_ID", iPatientID);
            sqlCommand.Parameters.Add("@SZ_CASE_ID", this.txtCaseID.Text.Trim());
            sqlCommand.Parameters.Add("@FLAG", "UPDATE");
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        catch (System.Exception ex)
        {
            this.usrMessage3.SetMessageType(0);
            this.usrMessage3.PutMessage(ex.ToString());
            this.usrMessage3.Show();
        }
    }

    private void GetDosespotPatientID()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SP_DOSESPOT_UPDATE_PATIENT_ID", connection)
        {
            CommandType = CommandType.StoredProcedure,
            Parameters =
            {
                {
                    "@SZ_CASE_ID",
                    this.txtCaseID.Text.Trim()
                },
                {
                    "@FLAG",
                    "LIST"
                }
            }
        });
        DataSet dataSet = new DataSet();
        sqlDataAdapter.Fill(dataSet);
        int num = 0;
        if (dataSet != null)
        {
            num = System.Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString());
        }
        this.txtDosespotPatientId.Text = num.ToString();
    }

    private int GetDosespotUserDetails()
    {
        int result = 0;
        string text = "";
        string text2 = "";
        try
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SP_DOSESPOT_USER_DETAILS", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    {
                        "@SZ_COMPANY_ID",
                        this.txtCompanyID.Text
                    },
                    {
                        "@SZ_USER_ID",
                        ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID
                    },
                    {
                        "@FLAG",
                        "LIST"
                    }
                }
            });
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            if (dataSet != null)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    text = dataSet.Tables[0].Rows[0]["ClinicId"].ToString();
                    text2 = dataSet.Tables[0].Rows[0]["UserId"].ToString();
                    result = 1;
                }
            }
            this.txtClinicId.Text = text;
            this.txtDosespotUserId.Text = text2;
        }
        catch (System.Exception ex)
        {
            this.usrMessage3.SetMessageType(0);
            this.usrMessage3.PutMessage(ex.ToString());
            this.usrMessage3.Show();
        }
        return result;
    }

    protected void btnAddAttorney_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._patient_tvbo = new Patient_TVBO();
        try
        {
            this._patient_tvbo.SaveAttorneyinfo("", this.txtAttorneyFirstName.Text, this.txtAttorneyLastName.Text, this.txtboxAttorneyCity.Text, this.extddlState.Selected_Text.ToString(), this.txtboxAttorneyZip.Text, this.txtAttorneyPhoneNo.Text, this.txtboxAttorneyFax.Text, this.txtAttorneyEmailID.Text, this.txtCompanyID.Text, this.extddlState.Text.ToString(), this.txtboxAttorneyAddress.Text, this.extddlAtttype.Text);
            this.extddlAttorneyAssign.Flag_ID = this.txtCompanyID.Text.ToString();
            this.usrMessage3.PutMessage("Attorney Saved Successfully");
            this.usrMessage3.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage3.Show();
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

        base.Response.Redirect(base.Request.Url.AbsoluteUri);
        this.tabcontainerPatientEntry.ActiveTabIndex = 4;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnadjcall_Click(object sender, EventArgs e)
    {
        if (this.Session["attoenyset"] == null)
        {
            this.extddlAdjuster.Text = "NA";
        }
        else
        {
            string str = this.Session["attoenyset"].ToString();
            this.extddlAdjuster.Text = str;
            this.Session["attoenyset"] = null;
        }
        this.adjuster();
    }

    protected void btnAssignSupplies_Click(object sender, System.EventArgs e)
    {
        try
        {
            CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
            caseDetailsBO.DeleteCaseSupplies(this.txtCaseID.Text, this.txtCompanyID.Text);
            for (int i = 0; i < this.grdAssignSupplies.Items.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)this.grdAssignSupplies.Items[i].FindControl("chkAssignSupplies");
                if (checkBox.Checked)
                {
                    caseDetailsBO.InsertCaseSupplies(this.grdAssignSupplies.Items[i].Cells[2].Text, this.txtCaseID.Text, this.txtCompanyID.Text);
                }
            }
            this.BindSuppliesGrid();
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void btnAssociate_Click(object sender, System.EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (this.allowAssociate())
        {
            this.CopyToCase();
            CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
            try
            {
                if (this.txtAssociateCases.Text != "")
                {
                    string[] strArrays = this.associtecasenoAllow.Split(new char[] { ',' });
                    for (int i = 0; i < (int)strArrays.Length; i++)
                    {
                        caseDetailsBO.SaveAssociateCases(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, strArrays[i].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "ADD");
                        for (int j = i + 1; j < (int)strArrays.Length; j++)
                        {
                            caseID = caseDetailsBO.GetCaseID(strArrays[i].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            if (caseID.Equals(""))
                            {
                                caseID = caseDetailsBO.GetCaseIDEmpty(strArrays[i].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            }
                            caseDetailsBO.SaveAssociateCases(caseID, strArrays[j].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "ADD");
                            caseID = "";
                        }
                    }
                }
                this.GetAssociateCases();
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
        else
        {
            this.usrMessage.PutMessage("Associate case not Allowed");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
            this.usrMessage.Show();
        }
    }

    protected void btnattcall_Click(object sender, System.EventArgs e)
    {
        this.setattorney();
        this.tabcontainerPatientEntry.ActiveTabIndex = 1;
    }

    protected void btnAttorneyAssign_Click(object sender, System.EventArgs e)
    {
        this._patient_tvbo = new Patient_TVBO();
        string text = "";
        string text2;
        if (this.chkAttorneyAssign.Checked)
        {
            text2 = "1";
        }
        else
        {
            text2 = "0";
        }
        if (this.chkAttorneyAssign.Checked)
        {
            text = this._patient_tvbo.GetUserIdForAttornyUser(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.extddlAttorneyAssign.Text);
            this._patient_tvbo.SaveAttorneyUser(text, this.extddlAttorneyAssign.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.txtCaseID.Text, text2);
            if (text == "")
            {
                this.Page.RegisterStartupScript("mm", "<script language='javascript'>alert('There is no user account added for the selected attorney. Contact your administrator to add or associate a user account to the selected attorney');</script>");
            }
        }
        else
        {
            this._patient_tvbo.SaveAttorneyUser(text, this.extddlAttorneyAssign.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.txtCaseID.Text, text2);
        }
        this.grdAttorney.XGridBindSearch();
    }

    protected void btnClear_Click(object sender, System.EventArgs e)
    {
        try
        {
            this.ClearControl();
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void btnDAssociate_Click(object sender, EventArgs e)
    {

        string text = this.txtAssociateCases.Text;
        string str = null;
        string str1 = "";
        bool flag = false;
        string str2 = "";
        string str3 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
        try
        {
            try
            {
                CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
                DataSet dataSet = new DataSet();
                dataSet = caseDetailsBO.GetAssociateCases(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "LIST");
                if (string.IsNullOrEmpty(text))
                {
                    throw new FormatException();
                }
                if (text.IndexOf(',') == -1)
                {
                    text = string.Concat(text, ",");
                }
                if (!this.txtInsuranceAddress.Text.Equals(""))
                {
                    string text1 = this.lstInsuranceCompanyAddress.Text;
                }
                string[] strArrays = text.Split(new char[] { ',' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
                    {
                        if (strArrays[i].ToString().Equals(dataSet.Tables[0].Rows[j]["SZ_CASE_NO"].ToString().Trim()))
                        {
                            flag = true;
                            Regex regex = new Regex("[^0-9]");
                            str2 = (!regex.IsMatch(strArrays[i]) ? strArrays[i].ToString() : strArrays[i].Remove(0, 2));
                            if (regex.IsMatch(str3))
                            {
                                str3 = str3.Remove(0, 2);
                            }
                        }
                        if (flag)
                        {
                            (new Patient_TVBO()).UpdatetoSaveToAllowed(str2.ToString(), this.txtCompanyID.Text, str3, "Delete");
                        }
                    }
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        str1 = string.Concat(str1, strArrays[i].ToString(), ",");
                    }
                }
                this.GetAssociateCases();
            }
            catch (FormatException formatException)
            {
                str = "";
                str = "Please enter DeAssociate Case No.";
                this.usrMessage.PutMessage(str);
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                this.usrMessage.Show();
            }
        }
        finally
        {
            if (string.IsNullOrEmpty(str))
            {
                if (str1.Equals("") || str1.Equals(","))
                {
                    str = "";
                    str = "The  case no’s successfully DeAssociate.";
                    this.usrMessage.PutMessage(str);
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
                else
                {
                    str = "";
                    str = string.Concat(str1, " not DeAssociate");
                    this.usrMessage.PutMessage(str);
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                    this.usrMessage.Show();
                }
            }
        }
    }

    protected void btndeleteAtt_Click(object sender, System.EventArgs e)
    {
        this._patient_tvbo = new Patient_TVBO();
        try
        {
            for (int i = 0; i < this.grdAttorney.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)this.grdAttorney.Rows[i].FindControl("chkDeleteAtt");
                if (checkBox.Checked)
                {
                    string text = this.grdAttorney.DataKeys[i]["SZ_ATTORNEY_ID"].ToString();
                    string text2 = this.grdAttorney.DataKeys[i]["SZ_COMPANY_ID"].ToString();
                    string text3 = this.grdAttorney.DataKeys[i]["ID"].ToString();
                    this._patient_tvbo.DeleteCaseAttorny(text, text2, text3);
                }
            }
            this.grdAttorney.XGridBindSearch();
        }
        catch (System.Exception ex)
        {
            string str = ex.Message.ToString().Replace("\n", " ");
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str);
        }
    }

    protected void btnFilter_Click(object sender, System.EventArgs e)
    {
        this.LoadNoteGrid();
    }

    protected void btnPatientUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            foreach (ListItem item in this.rdolstPatientType.Items)
            {
                if (!item.Selected)
                {
                    continue;
                }
                this.txtPatientType.Text = item.Value.ToString();
                break;
            }
            if (this._associateDiagnosisCodeBO.GetCaseTypeName(this.extddlCaseType.Text) != "WC000000000000000001")
            {
                this.txtAssociateDiagnosisCode.Text = "0";
            }
            else
            {
                this.txtAssociateDiagnosisCode.Text = "1";
            }
            bool flag = false;
            if (this.txtChartNo.Visible && !this.txtChartNo.Text.Equals(""))
            {
                flag = true;
                string str = "CHART";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    str = "REF";
                }
                this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                if (((Label)this.DtlView.Items[0].FindControl("lblViewChartNo")).Text.Equals(this.txtChartNo.Text))
                {
                    flag = false;
                }
                else if (!this._bill_Sys_PatientBO.ExistChartNumber(this.txtCompanyID.Text, this.txtChartNo.Text, str))
                {
                    this.UpdatePatientInformation();
                }
                else
                {
                    this.usrMessage.PutMessage(string.Concat(this.txtChartNo.Text, "  chart no is already exist ...!"));
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                    this.usrMessage.Show();
                    return;
                }
            }
            if (!flag)
            {
                this.UpdatePatientInformation();
            }
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this._bill_Sys_PatientBO.UpdateTemplateStatus(this.Session["TM_SZ_CASE_ID"].ToString(), (this.chkStatusProc.Checked ? 1 : 0), this.txtNF2Date.Text);
            (new Bill_Sys_PatientBO()).UpdateInsuranceInformation(this.Session["TM_SZ_CASE_ID"].ToString(), this.txtCompanyID.Text, this.txtcarriercode.Text);
            this.Page.MaintainScrollPositionOnPostBack = false;
            this.CheckTemplateStatus(this.Session["TM_SZ_CASE_ID"].ToString());
            this.LoadDataOnPage();
            this.usrMessage.PutMessage(" Patient Information Updated successfully ! ");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
            this.Session["AttornyID"] = this.extddlAttorney.Text;
            this.SaveNotes();
            this.LoadNoteGrid();
            this.UserPatientInfoControl.GetPatienDeskList(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (!this.btassociate.Checked && !this.associatecaseno.Equals(""))
            {
                this.UpdateCopyToCase(this.associatecaseno);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._saveOperation = new SaveOperation();
        try
        {
            this._saveOperation.WebPage = this.Page;
            this._saveOperation.Xml_File = "notes.xml";
            this._saveOperation.SaveMethod();
            this.ClearControl();
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

    protected void btnSaveAddress_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this._saveOperation = new SaveOperation();
        try
        {
            this.txtInsuranceCompanyID.Text = this.hdninsurancecode.Value;
            ArrayList arrayLists = new ArrayList();
            PopupBO popupBO = new PopupBO();
            arrayLists.Add(this.txtInsuranceCompanyID.Text);
            arrayLists.Add(this.txtInsuranceAddressCD.Text);
            arrayLists.Add(this.txtInsuranceStreetCD.Text);
            arrayLists.Add(this.txtInsuranceCityCD.Text);
            arrayLists.Add(this.extddlInsuranceState.Text);
            arrayLists.Add(this.txtInsuranceZipCD.Text);
            arrayLists.Add(this.txtCompanyID.Text);
            arrayLists.Add(this.extddlInsuranceStateNew.Text);
            if (!this.IDDefault.Checked)
            {
                arrayLists.Add("0");
            }
            else
            {
                arrayLists.Add("1");
            }
            popupBO.saveInsuranceAddressCaseDetails(arrayLists);
            this.ClearInsurancecontrol();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.hdninsurancecode.Value);
            this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            this.lstInsuranceCompanyAddress.DataValueField = "CODE";
            this.lstInsuranceCompanyAddress.DataBind();
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.lstInsuranceCompanyAddress.SelectedValue = popupBO.getLatestID("SP_MST_INSURANCE_ADDRESS", this.txtCompanyID.Text);
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(this.lstInsuranceCompanyAddress.SelectedValue);
            this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
            this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
            this.txtInsuranceState.Text = insuranceAddressDetail[4].ToString();
            this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
            this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
            this.Page.MaintainScrollPositionOnPostBack = true;
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

    protected void btnSaveAdjuster_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            PopupBO popupBO = new PopupBO();
            ArrayList arrayLists = new ArrayList();
            arrayLists.Add("");
            arrayLists.Add(this.txtAdjusterPopupName.Text);
            arrayLists.Add(this.txtAdjusterPopupPhone.Text);
            arrayLists.Add(this.txtAdjusterPopupExtension.Text);
            arrayLists.Add(this.txtAdjusterPopupFax.Text);
            arrayLists.Add(this.txtAdjusterPopupEmail.Text);
            arrayLists.Add(this.txtCompanyID.Text);
            popupBO.saveAdjuster(arrayLists);
            
            this.extddlAdjuster.Text = popupBO.getLatestID("SP_MST_ADJUSTER", this.txtCompanyID.Text);
            this._DAO_NOTES_EO = new DAO_NOTES_EO();
            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "ADJUSTER_ADDED";
            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "adjuster id : " + this.extddlAdjuster.Text;
            this._DAO_NOTES_BO = new DAO_NOTES_BO();
            this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
            if (this.extddlAdjuster.Text != "")
            {
                this.txtAdjusterPhone.Text = this.txtAdjusterPopupPhone.Text;
                this.txtAdjusterExtension.Text = this.txtAdjusterPopupExtension.Text;
                this.txtfax.Text = this.txtAdjusterPopupFax.Text;
                this.txtEmail.Text = this.txtAdjusterPopupEmail.Text;
            }
            this.txtAdjusterPopupName.Text = "";
            this.txtAdjusterPopupPhone.Text = "";
            this.txtAdjusterPopupExtension.Text = "";
            this.txtAdjusterPopupFax.Text = "";
            this.txtAdjusterPopupEmail.Text = "";
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

    protected void btnSearchInsCompany_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.hdninsurancecode.Value = (new CaseDetailsBO()).SearchInsuranceCompany(this.txtSearchName.Text, this.txtSearchCode.Text, this.txtCompanyID.Text);
            this.ClearInsurancecontrol();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.hdninsurancecode.Value);
            this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            this.lstInsuranceCompanyAddress.DataValueField = "CODE";
            this.lstInsuranceCompanyAddress.DataBind();
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.tabcontainerPatientEntry.ActiveTabIndex = 2;
            this.txtSearchName.Text = "";
            this.txtSearchCode.Text = "";
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

    protected void btnUpdateAdjuster_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._patient_tvbo = new Patient_TVBO();
        try
        {
            this.txtAdjusterid.Text = this.extddlAdjuster.Text;
            string text = this.txtAdjusterid.Text;
            this.MessageControl2.PutMessage("Attorney Updated Successfully");
            this.MessageControl2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.MessageControl2.Show();
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

    protected void btnUpdateAttorney_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._patient_tvbo = new Patient_TVBO();
        try
        {
            this._patient_tvbo.UpdateAttornyInfo(this.txtAttorneyid.Text, this.txtAttFirstName.Text, this.txtAttLastName.Text, this.txtAttCity.Text, this.extddlstateAtt.Selected_Text, this.txtAttZip.Text, this.txtAttPhoneNo.Text, this.txtAttFax.Text, this.txtAttEmailID.Text, this.txtCompanyID.Text, this.extddlstateAtt.Text, this.txtAttAddress.Text, this.extddlAttorneyType.Text);
            this.extddlAttorneyAssign.Flag_ID = this.txtCompanyID.Text.ToString();
            this.grdAttorney.XGridBind();
            this.usrMessage1.PutMessage("Attorney Updated Successfully");
            this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage1.Show();
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


    public void CheckMemoExists()
    {
        this._memoBO = new Bill_Sys_MemoBO();
        try
        {
            if (this._memoBO.GET_MEMO(this.txtCompanyID.Text, this.txtCaseID.Text).ToString() != "")
            {
                this.hlnkShowMemo.Style.Add("color", "Red");
                this.hlnkShowMemo.Style.Add("font-weight", "bold");
            }
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    private void CheckTemplateStatus(string p_szCaseID)
    {
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            DataTable templateStatus = this._bill_Sys_PatientBO.GetTemplateStatus(p_szCaseID);
            if (System.Convert.ToBoolean(templateStatus.Rows[0].ItemArray.GetValue(0)))
            {
                this.chkStatusProc.Checked = true;
                this.txtNF2Date.Text = templateStatus.Rows[0].ItemArray.GetValue(1).ToString();
            }
            else
            {
                this.chkStatusProc.Checked = false;
                this.txtNF2Date.Text = "";
            }
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    public bool CheckUpdate(string[] szCase, int j)
    {
        bool flag = true;
        Regex regex = new Regex("[[A-z]");
        if (regex.IsMatch(szCase[0]) && !szCase[1].Equals(""))
        {
            string text = szCase[1];
            szCase[1] = szCase[0];
            szCase[0] = text;
        }
        bool result;
        for (int i = j + 1; i < szCase.Length; i++)
        {
            if (szCase[i] != "")
            {
                Regex regex2 = new Regex("[^0-9]");
                string text2;
                if (regex2.IsMatch(szCase[i]))
                {
                    text2 = szCase[i].Remove(0, 2);
                }
                else
                {
                    text2 = szCase[i].ToString();
                }
                string text3;
                if (regex2.IsMatch(szCase[j]))
                {
                    text3 = szCase[j].Remove(0, 2);
                }
                else
                {
                    text3 = szCase[j].ToString();
                }
                Patient_TVBO patient_TVBO = new Patient_TVBO();
                string text4 = patient_TVBO.SavetoSaveToAllowed(text2, this.txtCompanyID.Text, text3, "InsAddress");
                if (text4 != null)
                {
                    if (!(text4 == "Same"))
                    {
                        if (text4 == "NotAllowed")
                        {
                            result = false;
                            return result;
                        }
                    }
                    else if (this.updateFlag)
                    {
                        flag = true;
                        this.associtecasenoAllow = this.associtecasenoAllow + text2 + ",";
                    }
                }
            }
        }
        result = flag;
        return result;
    }

    private void ClearAdjusterControl()
    {
        try
        {
            this.txtAdjusterExtension.Text = "";
            this.txtAdjusterPhone.Text = "";
            this.txtfax.Text = "";
            this.txtEmail.Text = "";
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    private void ClearControl()
    {
        try
        {
            this.LoadNoteGrid();
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void ClearControlAttorney()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtboxAttorneyAddress.Text = "";
            this.txtboxAttorneyCity.Text = "";
            this.txtAttorneyEmailID.Text = "";
            this.txtboxAttorneyFax.Text = "";
            this.txtAttorneyFirstName.Text = "";
            this.txtAttorneyLastName.Text = "";
            this.txtAttorneyPhoneNo.Text = "";
            this.extddlState.Text = "NA";
            this.extddlAtttype.Text = "NA";
            this.txtboxAttorneyZip.Text = "";
            this.btnAddAttorney.Enabled = true;
            this.chkDefaultFirm.Checked = false;
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

    private void ClearInsurancecontrol()
    {
        try
        {
            this.txtInsuranceAddressCD.Text = "";
            this.txtInsuranceCityCD.Text = "";
            this.txtInsuranceState.Text = "";
            this.txtInsuranceZipCD.Text = "";
            this.txtInsuranceStreetCD.Text = "";
            this.extddlInsuranceStateNew.Text = "";
            this.IDDefault.Checked = false;
            this.txtInsuranceAddress.Text = "";
            this.txtInsuranceCity.Text = "";
            this.txtInsuranceZip.Text = "";
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    private void ClearPatientAccidentControl()
    {
        try
        {
            this.txtATAccidentDate.Text = "";
            this.txtATAddress.Text = "";
            this.txtATCity.Text = "";
            this.txtATReportNumber.Text = "";
            this.txtATAdditionalPatients.Text = "";
            this.extddlATAccidentState.Text = "NA";
            this.txtATHospitalName.Text = "";
            this.txtATHospitalAddress.Text = "";
            this.txtATDescribeInjury.Text = "";
            this.txtATAdmissionDate.Text = "";
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void CopyToCase()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = this.associtecasenoAllow;
        string str1 = null;
        try
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    throw new FormatException();
                }
                if (str.IndexOf(',') == -1)
                {
                    str = string.Concat(str, ",");
                }
                if (!this.txtInsuranceAddress.Text.Equals(""))
                {
                    string text = this.lstInsuranceCompanyAddress.Text;
                }
                string[] strArrays = str.Split(new char[] { ',' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    Patient_TVBO patientTVBO = new Patient_TVBO();
                    string str2 = "";
                    str2 = (this.commonrange.IsMatch(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString()) ? ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString().Remove(0, 2) : ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString());
                    patientTVBO.UpdatetoSaveToAllowed(strArrays[i].ToString(), this.txtCompanyID.Text, str2, "InsAddressUpdate");
                }
                this.LoadDataOnPage();
            }
            catch (FormatException ex)
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
            if (string.IsNullOrEmpty(str1))
            {
                if (!this.associatecasenoNotAllow.Equals(""))
                {
                    str1 = string.Concat(this.associatecasenoNotAllow, " not allow to asscociate");
                    this.usrMessage.PutMessage(str1);
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                    this.usrMessage.Show();
                }
                else
                {
                    str1 = "The Insurance Company and Address copied to case no’s successfully.";
                    this.usrMessage.PutMessage(str1);
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlInsuranceType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string text = "";
        string text2 = "";
        string text4;
        if (this.ddlInsuranceType.SelectedItem != null)
        {
            string text3 = this.ddlInsuranceType.SelectedItem.Text;
            if (text3 == "Major Medical")
            {
                text4 = "MAJ";
            }
            else if (text3 == "Private")
            {
                text4 = "PRI";
            }
            else
            {
                text4 = "SEC";
            }
        }
        else
        {
            text4 = "SEC";
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.sqlCon = new SqlConnection(connectionString);
        string cmdText = string.Concat(new string[]
        {
            "select SZ_INSURANCE_ID, SZ_ADDRESS_ID FROM MST_SEC_INSURANCE_DETAIL WHERE SZ_CASE_ID='",
            this.txtCaseID.Text,
            "' AND SZ_COMPANY_ID='",
            this.txtCompanyID.Text,
            "' AND SZ_INSURANCE_TYPE='",
            text4,
            "'"
        });
        SqlCommand sqlCommand = new SqlCommand(cmdText, this.sqlCon);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        try
        {
            this.sqlCon.Open();
            sqlCommand = new SqlCommand(cmdText, this.sqlCon);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (sqlDataReader["SZ_INSURANCE_ID"] != System.DBNull.Value)
                {
                    text = sqlDataReader["SZ_INSURANCE_ID"].ToString();
                }
                if (sqlDataReader["SZ_ADDRESS_ID"] != System.DBNull.Value)
                {
                    text2 = sqlDataReader["SZ_ADDRESS_ID"].ToString();
                }
            }
        }
        catch (System.Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        if (text != "")
        {
            string cmdText2 = "select SZ_INSURANCE_NAME FROM MST_INSURANCE_COMPANY WHERE SZ_INSURANCE_ID='" + text + "'";
            try
            {
                this.sqlCon.Open();
                SqlDataReader sqlDataReader = new SqlCommand(cmdText2, this.sqlCon).ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["SZ_INSURANCE_NAME"] != System.DBNull.Value)
                    {
                        this.txtSecInsName.Text = sqlDataReader["SZ_INSURANCE_NAME"].ToString();
                    }
                }
            }
            catch (SqlException ex2)
            {
                ex2.Message.ToString();
            }
            finally
            {
                if (this.sqlCon.State == ConnectionState.Open)
                {
                    this.sqlCon.Close();
                }
            }
        }
        else
        {
            this.txtSecInsName.Text = "";
        }
        if (text2 != "")
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SP_MST_INSURANCE_ADDRESS", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters =
                    {
                        {
                            "@SZ_INS_ADDRESS_ID",
                            text2
                        },
                        {
                            "@FLAG",
                            "LIST"
                        }
                    }
                });
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                sqlConnection.Close();
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    this.txtSecInsAddress.Text = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                    this.txtInsCity.Text = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                    this.txtInsState.Text = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                    this.txtInsZip.Text = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                    this.txtSecInsPhone.Text = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                    this.txtSecInsFax.Text = dataSet.Tables[0].Rows[0]["sz_fax_number"].ToString();
                    this.txtInsConatactPerson.Text = dataSet.Tables[0].Rows[0]["sz_contact_person"].ToString();
                }
                return;
            }
            catch (System.Exception ex3)
            {
                this.usrMessage.SetMessageType(0);
                this.usrMessage.PutMessage(ex3.ToString());
                this.usrMessage.Show();
                return;
            }
        }
        this.txtSecInsAddress.Text = "";
        this.txtInsCity.Text = "";
        this.txtInsState.Text = "";
        this.txtInsZip.Text = "";
        this.txtSecInsPhone.Text = "";
        this.txtSecInsFax.Text = "";
        this.txtInsConatactPerson.Text = "";
    }

    protected void extddlAdjuster_extendDropDown_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        this.adjuster();
    }

    protected void extddlAttorney_selectedIndex(object sender, System.EventArgs e)
    {
        this.setattorney();
        this.tabcontainerPatientEntry.ActiveTabIndex = 1;
    }

    protected void extddlInsuranceCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearInsurancecontrol();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.extddlInsuranceCompany.Text);
            DataSet dataSet = new DataSet();
            dataSet = (DataSet)this.lstInsuranceCompanyAddress.DataSource;
            this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            this.lstInsuranceCompanyAddress.DataValueField = "CODE";
            if (dataSet.Tables[0].Rows.Count != 0)
            {
                this.lstInsuranceCompanyAddress.DataBind();
                this.lstInsuranceCompanyAddress.SelectedIndex = 0;
                ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSet.Tables[0].Rows[0][0].ToString());
                this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
                this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
                this.txtInsuranceState.Text = insuranceAddressDetail[4].ToString();
                this.extddlInsuranceState.Text = insuranceAddressDetail[4].ToString();
                this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
                this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
                if (insuranceAddressDetail[8].ToString() != "")
                {
                    this.extddlInsuranceState.Text = insuranceAddressDetail[8].ToString();
                }
                this.txtInsPhone.Text = insuranceAddressDetail[10].ToString();
                this.Page.MaintainScrollPositionOnPostBack = true;
            }
            else
            {
                this.lstInsuranceCompanyAddress.Items.Clear();
            }
            this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            this.lstInsuranceCompanyAddress.DataValueField = "CODE";
            this.lstInsuranceCompanyAddress.DataBind();
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.tabcontainerPatientEntry.ActiveTabIndex = 2;
            if (this.lstInsuranceCompanyAddress.Items.Count == 1)
            {
                ArrayList arrayLists = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSet.Tables[0].Rows[0][0].ToString());
                this.txtInsuranceAddress.Text = arrayLists[2].ToString();
                this.txtInsuranceCity.Text = arrayLists[3].ToString();
                this.txtInsuranceState.Text = arrayLists[4].ToString();
                this.extddlInsuranceState.Text = arrayLists[4].ToString();
                this.txtInsuranceZip.Text = arrayLists[5].ToString();
                this.txtInsuranceStreet.Text = arrayLists[6].ToString();
                if (arrayLists[8].ToString() != "")
                {
                    this.extddlInsuranceState.Text = arrayLists[8].ToString();
                }
                this.Page.MaintainScrollPositionOnPostBack = true;
            }
            ArrayList defaultDetailnew = new ArrayList();
            defaultDetailnew = this._bill_Sys_PatientBO.GetDefaultDetailnew(this.extddlInsuranceCompany.Text);
            if (defaultDetailnew.Count != 0)
            {
                this.txtInsuranceAddress.Text = defaultDetailnew[1].ToString();
                int num = 0;
                while (num < this.lstInsuranceCompanyAddress.Items.Count)
                {
                    if (this.lstInsuranceCompanyAddress.Items[num].Value != defaultDetailnew[0].ToString())
                    {
                        num++;
                    }
                    else
                    {
                        this.lstInsuranceCompanyAddress.SelectedIndex = num;
                        break;
                    }
                }
                this.txtInsuranceCity.Text = defaultDetailnew[2].ToString();
                this.txtInsuranceZip.Text = defaultDetailnew[3].ToString();
                this.txtInsuranceStreet.Text = defaultDetailnew[4].ToString();
                this.txtInsuranceState.Text = defaultDetailnew[6].ToString();
                this.extddlInsuranceState.Text = defaultDetailnew[5].ToString();
                this.Page.MaintainScrollPositionOnPostBack = true;
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

    protected void extddlRefferingDoctor_extendDropDown_SelectedIndexChanged(object sender, System.EventArgs e)
    {
    }

    protected void extddlRefferingOffice_extendDropDown_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        this.extddlRefferingDoctor.Flag_ID = this.extddlRefferingOffice.Text.ToString();
        this.tabcontainerPatientEntry.ActiveTabIndex = 5;
    }

    protected string GeneratePDF()
    {
        GeneratePatientInfoPDF generatePatientInfoPDF = new GeneratePatientInfoPDF();
        string text = "";
        try
        {
            string text2 = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["PATIENT_INFO_HTML"]);
            text2 = generatePatientInfoPDF.getReplacedString(text2, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            PdfMetamorphosis pdfMetamorphosis = new PdfMetamorphosis();
            pdfMetamorphosis.Serial = "10007706603";
            string str = this.getFileName("P") + ".htm";
            text = this.getFileName("P") + ".pdf";
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + str);
            streamWriter.Write(text2);
            streamWriter.Close();
            pdfMetamorphosis.HtmlToPdfConvertFile(ConfigurationManager.AppSettings["EXCEL_SHEET"] + str, ConfigurationManager.AppSettings["EXCEL_SHEET"] + text);
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
        return text;
    }

    public void getAdjusterdata(string sz_Companyid, string sz_adjuster)
    {
        try
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SP_GET_ADJUSTER", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    {
                        "@SZ_COMPANY_ID",
                        sz_Companyid
                    },
                    {
                        "@SZ_ADJUSTER_ID",
                        sz_adjuster
                    },
                    {
                        "@FLAG",
                        "LIST"
                    }
                }
            });
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            sqlConnection.Close();
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                this.txtAdjusterPopupPhone.Text = dataSet.Tables[0].Rows[0]["SZ_PHONE"].ToString();
                this.txtAdjusterPopupExtension.Text = dataSet.Tables[0].Rows[0]["SZ_EXTENSION"].ToString();
                this.txtAdjusterPopupFax.Text = dataSet.Tables[0].Rows[0]["SZ_FAX"].ToString();
                this.txtAdjusterPopupEmail.Text = dataSet.Tables[0].Rows[0]["SZ_EMAIL"].ToString();
            }
        }
        catch (System.Exception ex)
        {
            this.usrMessage.SetMessageType(0);
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.Show();
        }
    }

    private string getApplicationSetting(string p_szKey)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        sqlConnection.Open();
        string result = "";
        SqlDataReader sqlDataReader = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = '" + p_szKey.ToString().Trim() + "'", sqlConnection).ExecuteReader();
        while (sqlDataReader.Read())
        {
            result = sqlDataReader["parametervalue"].ToString();
        }
        return result;
    }

    public void GetAssociateCases()
    {
        CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
        this.divAssociateCaseID.Controls.Clear();
        try
        {
            DataSet dataSet = new DataSet();
            dataSet = caseDetailsBO.GetAssociateCases(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "LIST");
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                System.Web.UI.WebControls.LinkButton linkButton = new System.Web.UI.WebControls.LinkButton();
                linkButton.ID = "lnk" + dataSet.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                linkButton.Text = dataSet.Tables[0].Rows[i]["NAME"].ToString() + " ";
                linkButton.CssClass = "lbl";
                linkButton.CommandArgument = dataSet.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                linkButton.Command += new System.Web.UI.WebControls.CommandEventHandler(this.OnClick);
                this.divAssociateCaseID.Controls.Add(linkButton);
                this.associatecaseno = this.associatecaseno + dataSet.Tables[0].Rows[i]["SZ_CASE_NO"].ToString() + ",";
            }
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    public string GetCompanyName(string szCompanyId)
    {
        Bill_Sys_CaseDetails.log.Debug("Get Company Name;");
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string text = "";
        try
        {
            sqlConnection.Open();
            Bill_Sys_CaseDetails.log.Debug("GET_COMPANY_NAME @SZ_COMPANY_ID='" + szCompanyId + "'");
            SqlCommand sqlCommand = new SqlCommand("GET_COMPANY_NAME", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            text = dataSet.Tables[0].Rows[0][0].ToString();
            Bill_Sys_CaseDetails.log.Debug("Company Name : " + dataSet.Tables[0].Rows[0][0].ToString());
        }
        catch (System.Exception ex)
        {
            Bill_Sys_CaseDetails.log.Debug("exGetCompanyName : " + ex.Message.ToString());
            Bill_Sys_CaseDetails.log.Debug("exGetCompanyName : " + ex.StackTrace.ToString());
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        Bill_Sys_CaseDetails.log.Debug("Return Company Name : " + text);
        return text;
    }

    private string getFileName(string p_szBillNumber)
    {
        System.DateTime now = System.DateTime.Now;
        return string.Concat(new string[]
        {
            p_szBillNumber,
            "_",
            this.getRandomNumber(),
            "_",
            now.ToString("yyyyMMddHHmmssms")
        });
    }

    private string getPacketDocumentFolder(string p_szPath, string p_szCompanyID, string p_szCaseID)
    {
        p_szPath = p_szPath + p_szCompanyID + "/" + p_szCaseID;
        if (System.IO.Directory.Exists(p_szPath))
        {
            if (!System.IO.Directory.Exists(p_szPath + "/Packet Document"))
            {
                System.IO.Directory.CreateDirectory(p_szPath + "/Packet Document");
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory(p_szPath);
            System.IO.Directory.CreateDirectory(p_szPath + "/Packet Document");
        }
        return p_szPath + "/Packet Document/";
    }

    private void GetPatientDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlDataReader sqlDataReader;
        string[] text;
        try
        {
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet patientInfo = this._bill_Sys_PatientBO.GetPatientInfo(this.txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (patientInfo.Tables[0].Rows.Count > 0)
            {
                this.DtlView.DataSource = patientInfo;
                this.DtlView.DataBind();
                if (patientInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;")
                {
                    this.txtPatientFName.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                }
                if (patientInfo.Tables[0].Rows[0]["sz_source_company_name"].ToString() != "")
                {
                    this.lblwalkin.Text = patientInfo.Tables[0].Rows[0]["sz_source_company_name"].ToString();
                }
                this.txtPatientLName.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                this.txtPatientAge.Text = patientInfo.Tables[0].Rows[0]["I_PATIENT_AGE"].ToString();
                this.txtPatientAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                this.txtPatientCity.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
                this.txtPatientZip.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
                this.txtPatientPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
                this.txtPatientEmail.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_EMAIL"].ToString();
                this.txtWorkPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString();
                this.txtExtension.Text = patientInfo.Tables[0].Rows[0]["SZ_WORK_PHONE_EXTENSION"].ToString();
                this.txtCellNo.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_CELLNO"].ToString();
                if (!(patientInfo.Tables[0].Rows[0]["BT_WRONG_PHONE"].ToString() == "True") || !(patientInfo.Tables[0].Rows[0]["BT_WRONG_PHONE"].ToString() != ""))
                {
                    this.chkWrongPhone.Checked = false;
                }
                else
                {
                    CheckBox checkBox = (CheckBox)this.DtlView.FindControl("chkViewWrongPhone");
                    this.chkWrongPhone.Checked = true;
                    checkBox.Checked = true;
                }
                this.txtMI.Text = patientInfo.Tables[0].Rows[0]["MI"].ToString();
                Label str = (Label)this.DtlView.Items[0].FindControl("lblViewMiddleName");
                str.Text = patientInfo.Tables[0].Rows[0]["MI"].ToString();
                this.txtWCBNo.Text = patientInfo.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
                this.txtSocialSecurityNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                if (!(patientInfo.Tables[0].Rows[0]["DT_DOB"].ToString() != "01/01/1900") || !(patientInfo.Tables[0].Rows[0]["DT_DOB"].ToString() != "&nbsp;"))
                {
                    this.txtDateOfBirth.Text = "";
                }
                else
                {
                    this.txtDateOfBirth.Text = patientInfo.Tables[0].Rows[0]["DT_DOB"].ToString();
                }
                Label label = (Label)this.DtlView.Items[0].FindControl("lblViewGender");
                this.ddlSex.SelectedValue = patientInfo.Tables[0].Rows[0]["SZ_GENDER"].ToString();
                label.Text = this.ddlSex.SelectedItem.Text;
                if (!(patientInfo.Tables[0].Rows[0]["DT_INJURY"].ToString() != "01/01/1900") || !(patientInfo.Tables[0].Rows[0]["DT_INJURY"].ToString() != "&nbsp;"))
                {
                    this.txtDateOfInjury.Text = "";
                }
                else
                {
                    this.txtDateOfInjury.Text = patientInfo.Tables[0].Rows[0]["DT_INJURY"].ToString();
                }
                this.txtJobTitle.Text = patientInfo.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString();
                this.txtWorkActivites.Text = patientInfo.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES"].ToString();
                this.txtState.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
                this.txtCarrierCaseNo.Text = patientInfo.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString();
                this.txtEmployerName.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
                this.txtEmployerPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString();
                this.txtEmployerAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString();
                this.txtEmployerCity.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString();
                this.txtEmployerState.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString();
                this.txtEmployerZip.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();
                if (patientInfo.Tables[0].Rows[0]["BT_TRANSPORTATION"] != null || patientInfo.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"] == null)
                {
                    this.chkTransportation.Checked = false;
                    this.chkTransportation.Checked = false;
                }
                else
                {
                    CheckBox checkBox1 = (CheckBox)this.DtlView.Items[0].FindControl("chkTransportation");
                    checkBox1.Checked = true;
                    checkBox1.Checked = true;
                }
                this.extddlPatientState.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();
                Label selectedText = (Label)this.DtlView.Items[0].FindControl("lblViewPatientState");
                if (this.extddlPatientState.Text != "NA" && this.extddlPatientState.Text != "")
                {
                    selectedText.Text = this.extddlPatientState.Selected_Text;
                }
                this.extddlEmployerState.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_STATE_ID"].ToString();
                Label selectedText1 = (Label)this.DtlView.Items[0].FindControl("lblViewEmployerState");
                if (this.extddlEmployerState.Text != "NA" && this.extddlEmployerState.Text != "")
                {
                    selectedText1.Text = this.extddlEmployerState.Selected_Text;
                }
                this.txtChartNo.Text = patientInfo.Tables[0].Rows[0]["SZ_CHART_NO"].ToString();
                Label str1 = (Label)this.DtlView.Items[0].FindControl("lblViewChartNo");
                str1.Text = patientInfo.Tables[0].Rows[0]["SZ_CHART_NO"].ToString();
                if (!(patientInfo.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "01/01/1900") || !(patientInfo.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "&nbsp;"))
                {
                    this.txtDateofFirstTreatment.Text = "";
                }
                else
                {
                    this.txtDateofFirstTreatment.Text = patientInfo.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString();
                }
            }
            this.ClearPatientAccidentControl();
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_INFO_ID"].ToString() != "&nbsp;")
            {
                this.txtAccidentID.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_INFO_ID"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString() != "&nbsp;")
            {
                this.txtPlatenumber.Text = patientInfo.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "&nbsp;")
            {
                this.txtAccidentAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "&nbsp;")
            {
                this.txtAccidentCity.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "&nbsp;")
            {
                this.txtAccidentState.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString() != "&nbsp;")
            {
                this.txtPolicyReport.Text = patientInfo.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString() != "&nbsp;")
            {
                this.txtListOfPatient.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString();
            }
            if (!(patientInfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "&nbsp;") || !(patientInfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "01/01/1900"))
            {
                this.txtDateofAccident.Text = "";
            }
            else
            {
                this.txtDateofAccident.Text = patientInfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString() != "&nbsp;")
            {
                string str2 = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString();
                foreach (ListItem item in this.rdolstPatientType.Items)
                {
                    if (item.Value.ToString() != str2)
                    {
                        continue;
                    }
                    item.Selected = true;
                    break;
                }
                foreach (ListItem listItem in this.rdolstPatientType.Items)
                {
                    if (!listItem.Selected)
                    {
                        continue;
                    }
                    Label label1 = (Label)this.DtlView.Items[0].FindControl("lblPatientType");
                    label1.Text = listItem.Text.ToString();
                }
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_SPECIALTY"].ToString() != "&nbsp;")
            {
                string str3 = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_SPECIALTY"].ToString();
                this.txtAccidentSpecialty.Text = str3;
                Label label2 = (Label)this.DtlView.Items[0].FindControl("lblAccidentSpecialty");
                label2.Text = str3;
            }
            Label label3 = (Label)this.DtlView.Items[0].FindControl("lblVLocation1");
            label3.Text = patientInfo.Tables[0].Rows[0]["sz_location_Name"].ToString();
            if (patientInfo.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() != "")
            {
                this.txtCaseID.Text = patientInfo.Tables[0].Rows[0]["SZ_CASE_ID"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != "")
            {
                this.extddlCaseType.Text = patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString();
                this.txtCaseTypeID.Text = patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString();
                Label selectedText2 = (Label)this.DtlView.Items[0].FindControl("lblViewCasetype");
                if (this.extddlCaseType.Text != "NA" && this.extddlCaseType.Text != "")
                {
                    selectedText2.Text = this.extddlCaseType.Selected_Text;
                }
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != "")
            {
                this.extddlProvider.Text = patientInfo.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")
            {
                this.extddlInsuranceCompany.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                this.hdninsurancecode.Value = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                this.txtInsuranceCompany.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                if (this.extddlInsuranceCompany.Text != "NA" && this.extddlInsuranceCompany.Text != "" && this.txtInsuranceCompany.Text != "" && this.txtInsuranceCompany.Text != "No suggestions found for your search")
                {
                    this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                }
                this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.hdninsurancecode.Value).Tables[0];
                this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                this.lstInsuranceCompanyAddress.DataValueField = "CODE";
                this.lstInsuranceCompanyAddress.DataBind();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != "")
            {
                this.extddlCaseStatus.Text = patientInfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString();
                Label selectedText3 = (Label)this.DtlView.Items[0].FindControl("lblViewCaseStatus");
                if (this.extddlCaseStatus.Text != "NA" && this.extddlCaseStatus.Text != "")
                {
                    selectedText3.Text = this.extddlCaseStatus.Selected_Text;
                }
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != "")
            {
                this.extddlAttorney.Text = patientInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString();
                this.hdnattorneycode.Value = this.extddlAttorney.Text;
                Label selectedText4 = (Label)this.DtlView.Items[0].FindControl("lblViewAttorney");
                if (this.extddlAttorney.Text != "NA" && this.extddlAttorney.Text != "")
                {
                    selectedText4.Text = this.extddlAttorney.Selected_Text;                    
                    txtAttorneyCompany.Text = this.extddlAttorney.Selected_Text;
                }
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "")
            {
                this.txtClaimNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                if (this.txtClaimNumber.Text.Equals("NA"))
                {
                    this.txtClaimNumber.Text = "";                    
                }
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "")
            {
                this.txtPolicyNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                if (this.txtPolicyNumber.Text.Equals("NA"))
                {
                    this.txtPolicyNumber.Text = "";
                }
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != "")
            {
                this.extddlAdjuster.Text = patientInfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString();
                Label label4 = (Label)this.DtlView.Items[0].FindControl("lblViewAdjusterName");
                if (this.extddlAdjuster.Text != "NA" && this.extddlAdjuster.Text != "")
                {
                    label4.Text = this.extddlAdjuster.Selected_Text;
                }
            }
            if (patientInfo.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString() != "")
            {
                this.txtAssociateDiagnosisCode.Text = patientInfo.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString() != "")
            {
                this.txtPlatenumber.Text = patientInfo.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "")
            {
                this.txtAccidentAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "")
            {
                this.txtAccidentCity.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "")
            {
                this.txtAccidentState.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString() != "")
            {
                this.txtPolicyReport.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString() != "")
            {
                this.txtListOfPatient.Text = patientInfo.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString() != "")
            {
                this.extddlLocation.Text = patientInfo.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString();
                Label selectedText5 = (Label)this.DtlView.Items[0].FindControl("lblVLocation1");
                if (this.extddlLocation.Text != "NA" && this.extddlLocation.Text != "")
                {
                    selectedText5.Text = this.extddlLocation.Selected_Text;
                }
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_RELATION"].ToString() != "&nbsp;")
            {
                string str4 = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_RELATION"].ToString();
                foreach (ListItem item1 in this.rdlPatient_relation.Items)
                {
                    if (item1.Value.ToString() != str4)
                    {
                        continue;
                    }
                    item1.Selected = true;
                    break;
                }
                foreach (ListItem listItem1 in this.rdlPatient_relation.Items)
                {
                    if (!listItem1.Selected)
                    {
                        continue;
                    }
                    Label str5 = (Label)this.DtlView.Items[0].FindControl("lblPatientRelation");
                    str5.Text = listItem1.Text.ToString();
                }
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_STATUS"].ToString() != "&nbsp;")
            {
                string str6 = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_STATUS"].ToString();
                foreach (ListItem item2 in this.rdlPatient_Status.Items)
                {
                    if (item2.Value.ToString() != str6)
                    {
                        continue;
                    }
                    item2.Selected = true;
                    break;
                }
                foreach (ListItem listItem2 in this.rdlPatient_Status.Items)
                {
                    if (!listItem2.Selected)
                    {
                        continue;
                    }
                    Label label5 = (Label)this.DtlView.Items[0].FindControl("lblPatientStatus");
                    label5.Text = listItem2.Text.ToString();
                }
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "" && patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")
            {
                try
                {
                    this.lstInsuranceCompanyAddress.SelectedValue = patientInfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString();
                    this.txtInsuranceAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                    this.txtInsuranceCity.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                    this.txtInsuranceState.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                    this.txtInsuranceZip.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                    this.txtInsuranceStreet.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_STREET"].ToString();
                    this.txtInsFax.Text = patientInfo.Tables[0].Rows[0]["sz_fax_number"].ToString();
                    this.txtInsPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                    this.txtInsContactPerson.Text = patientInfo.Tables[0].Rows[0]["sz_contact_person"].ToString();
                }
                catch
                {
                }
            }
            string str7 = ConfigurationManager.AppSettings["Connection_String"].ToString();
            this.sqlCon = new SqlConnection(str7);
            DataSet dataSet = new DataSet();
            try
            {
                try
                {
                    text = new string[] { "SELECT *   FROM mst_case_type_wise_ui_access_control WHERE sz_case_type_id = '", this.txtCaseTypeID.Text, "' and sz_company_id = '", this.txtCompanyID.Text, "' and sz_page_name = 'Workarea' " };
                    string str8 = string.Concat(text);
                    this.sqlCon.Open();
                    SqlCommand sqlCommand = new SqlCommand(str8, this.sqlCon);
                    (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
                }
                catch (SqlException sqlException)
                {
                    sqlException.Message.ToString();
                }
            }
            finally
            {
                if (this.sqlCon.State == ConnectionState.Open)
                {
                    this.sqlCon.Close();
                }
            }
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    string str9 = dataSet.Tables[0].Rows[i]["sz_control_name"].ToString();
                    string str10 = dataSet.Tables[0].Rows[i]["sz_control_type"].ToString();
                    if (dataSet.Tables[0].Rows[i]["sz_access_type"].ToString() == "hidden")
                    {
                        if (str10 == "LinkButton")
                        {
                            if (this.lnkNF2Envelope.ID.ToString() == str9)
                            {
                                this.lnkNF2Envelope.Visible = false;
                            }
                            if (this.lnkGenerateNF2.ID.ToString() == str9)
                            {
                                this.lnkGenerateNF2.Visible = false;
                            }
                        }
                        if (str10 == "CheckBox" && this.chkStatusProc.ID.ToString() == str9)
                        {
                            this.chkStatusProc.Visible = false;
                        }
                        if (str10 == "TextBox" && this.txtNF2Date.ID.ToString() == str9)
                        {
                            this.txtNF2Date.Visible = false;
                        }
                        if (str10 == "ImageButton" && this.imgbtnNF2Date.ID.ToString() == str9)
                        {
                            this.imgbtnNF2Date.Visible = false;
                        }
                    }
                }
            }
            if (this.txtAssociateDiagnosisCode.Text != "1")
            {
                this.chkAssociateCode.Checked = false;
            }
            else
            {
                this.chkAssociateCode.Checked = true;
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != "")
            {
                this.txtPolicyHolder.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString();
                if (this.txtPolicyHolder.Text.Equals("NA"))
                {
                    this.txtPolicyHolder.Text = "";
                }
                this.txtPolicyHolderAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ADDRESS"].ToString();
                if (this.txtPolicyHolderAddress.Text.Equals("NA"))
                {
                    this.txtPolicyHolderAddress.Text = "";
                }
                this.txtPolicyCity.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER_CITY"].ToString();
                if (this.txtPolicyCity.Text.Equals("NA"))
                {
                    this.txtPolicyCity.Text = "";
                }
                this.extdlPolicyState.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER_STATE_ID"].ToString();
                this.txtPolicyZip.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ZIP"].ToString();
                if (this.txtPolicyZip.Text.Equals("NA"))
                {
                    this.txtPolicyZip.Text = "";
                }
                this.txtPolicyPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE"].ToString();
                if (this.txtPolicyPhone.Text.Equals("NA"))
                {
                    this.txtPolicyPhone.Text = "";
                }
                this.rdlPolicyHolderRelation.Text = patientInfo.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString();
            }
            string str11 = "";
            string str12 = "";
            string str13 = "";
            text = new string[] { "select SZ_INSURANCE_ID FROM  MST_SEC_INSURANCE_DETAIL  WHERE SZ_CASE_ID='", this.txtCaseID.Text, "' AND SZ_COMPANY_ID='", this.txtCompanyID.Text, "'" };
            string str14 = string.Concat(text);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(str14, this.sqlCon));
            try
            {
                try
                {
                    this.sqlCon.Open();
                    sqlDataReader = (new SqlCommand(str14, this.sqlCon)).ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        if (sqlDataReader["SZ_INSURANCE_ID"] == DBNull.Value)
                        {
                            continue;
                        }
                        str11 = sqlDataReader["SZ_INSURANCE_ID"].ToString();
                    }
                }
                catch (SqlException sqlException1)
                {
                    sqlException1.Message.ToString();
                }
            }
            finally
            {
                if (this.sqlCon.State == ConnectionState.Open)
                {
                    this.sqlCon.Close();
                }
            }
            if (str11 == "")
            {
                this.txtSecInsName.Text = "";
            }
            else
            {
                string str15 = string.Concat("select SZ_INSURANCE_NAME FROM MST_INSURANCE_COMPANY WHERE SZ_INSURANCE_ID='", str11, "'");
                try
                {
                    try
                    {
                        this.sqlCon.Open();
                        sqlDataReader = (new SqlCommand(str15, this.sqlCon)).ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            if (sqlDataReader["SZ_INSURANCE_NAME"] == DBNull.Value)
                            {
                                continue;
                            }
                            this.txtSecInsName.Text = sqlDataReader["SZ_INSURANCE_NAME"].ToString();
                        }
                    }
                    catch (SqlException sqlException2)
                    {
                        sqlException2.Message.ToString();
                    }
                }
                finally
                {
                    if (this.sqlCon.State == ConnectionState.Open)
                    {
                        this.sqlCon.Close();
                    }
                }
                string str16 = string.Concat("select SZ_POLICY_NUMBER,SZ_CLAIM_NUMBER FROM TXN_PRIVATE_INTAKE WHERE SZ_INSURANCE_ID='", str11, "'");
                try
                {
                    try
                    {
                        this.sqlCon.Open();
                        sqlDataReader = (new SqlCommand(str16, this.sqlCon)).ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            if (sqlDataReader["SZ_POLICY_NUMBER"] != DBNull.Value)
                            {
                                this.txtPolicy.Text = sqlDataReader["SZ_POLICY_NUMBER"].ToString();
                            }
                            if (sqlDataReader["SZ_CLAIM_NUMBER"] == DBNull.Value)
                            {
                                continue;
                            }
                            this.txtClaim.Text = sqlDataReader["SZ_CLAIM_NUMBER"].ToString();
                        }
                    }
                    catch (SqlException sqlException3)
                    {
                        sqlException3.Message.ToString();
                    }
                }
                finally
                {
                    if (this.sqlCon.State == ConnectionState.Open)
                    {
                        this.sqlCon.Close();
                    }
                }
            }
            text = new string[] { "select SZ_ADDRESS_ID, SZ_INSURANCE_TYPE from MST_SEC_INSURANCE_DETAIL where  SZ_CASE_ID='", this.txtCaseID.Text, "' and SZ_COMPANY_ID='", this.txtCompanyID.Text, "' and SZ_INSURANCE_ID='", str11, "'" };
            string str17 = string.Concat(text);
            try
            {
                try
                {
                    this.sqlCon.Open();
                    sqlDataReader = (new SqlCommand(str17, this.sqlCon)).ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        if (sqlDataReader["SZ_ADDRESS_ID"] != DBNull.Value)
                        {
                            str12 = sqlDataReader["SZ_ADDRESS_ID"].ToString();
                        }
                        if (sqlDataReader["SZ_INSURANCE_TYPE"] == DBNull.Value)
                        {
                            continue;
                        }
                        str13 = sqlDataReader["SZ_INSURANCE_TYPE"].ToString();
                    }
                }
                catch (SqlException sqlException4)
                {
                    sqlException4.Message.ToString();
                }
            }
            finally
            {
                if (this.sqlCon.State == ConnectionState.Open)
                {
                    this.sqlCon.Close();
                }
            }
            if (str13 != "")
            {
                if (str13 == "SEC")
                {
                    this.ddlInsuranceType.SelectedIndex = 1;
                }
                else if (str13 == "MAJ")
                {
                    this.ddlInsuranceType.SelectedIndex = 2;
                }
                else if (str13 != "PRI")
                {
                    this.ddlInsuranceType.SelectedIndex = 0;
                }
                else
                {
                    this.ddlInsuranceType.SelectedIndex = 3;
                }
            }
            if (str12 == "")
            {
                this.txtSecInsAddress.Text = "";
                this.txtInsCity.Text = "";
                this.txtInsState.Text = "";
                this.txtInsZip.Text = "";
                this.txtSecInsPhone.Text = "";
                this.txtSecInsFax.Text = "";
                this.txtInsConatactPerson.Text = "";
            }
            else
            {
                DataSet dataSet1 = null;
                try
                {
                    SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
                    SqlCommand sqlCommand1 = new SqlCommand("SP_MST_INSURANCE_ADDRESS", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand1.Parameters.Add("@SZ_INS_ADDRESS_ID", str12);
                    sqlCommand1.Parameters.Add("@FLAG", "LIST");
                    sqlDataAdapter = new SqlDataAdapter(sqlCommand1);
                    dataSet1 = new DataSet();
                    sqlDataAdapter.Fill(dataSet1);
                    sqlConnection.Close();
                    if (dataSet1.Tables[0].Rows.Count > 0)
                    {
                        this.txtSecInsAddress.Text = dataSet1.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                        this.txtInsCity.Text = dataSet1.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                        this.txtInsState.Text = dataSet1.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                        this.txtInsZip.Text = dataSet1.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                        this.txtSecInsPhone.Text = dataSet1.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                        this.txtSecInsFax.Text = dataSet1.Tables[0].Rows[0]["sz_fax_number"].ToString();
                        this.txtInsConatactPerson.Text = dataSet1.Tables[0].Rows[0]["sz_contact_person"].ToString();
                    }
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                    this.usrMessage.PutMessage(exception.ToString());
                    this.usrMessage.Show();
                }
            }
            this.txtAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_ADDRESS"].ToString();
            this.txtCity.Text = patientInfo.Tables[0].Rows[0]["SZ_CITY"].ToString();
            this.txtAdjusterState.Text = patientInfo.Tables[0].Rows[0]["SZ_STATE"].ToString();
            this.txtZip.Text = patientInfo.Tables[0].Rows[0]["SZ_ZIP"].ToString();
            this.txtAdjusterPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_PHONE"].ToString();
            this.txtAdjusterExtension.Text = patientInfo.Tables[0].Rows[0]["SZ_EXTENSION"].ToString();
            this.txtfax.Text = patientInfo.Tables[0].Rows[0]["SZ_FAX"].ToString();
            this.txtEmail.Text = patientInfo.Tables[0].Rows[0]["SZ_EMAIL"].ToString();
            this.ClearPatientAccidentControl();
            this.txtATPlateNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();
            if (patientInfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() == "01/01/1900")
            {
                this.txtATAccidentDate.Text = "";
            }
            else
            {
                this.txtATAccidentDate.Text = patientInfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
            }
            this.txtATAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
            this.txtATCity.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
            this.txtATReportNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString();
            this.txtATAdditionalPatients.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString();
            this.extddlATAccidentState.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE_ID"].ToString();
            Label selectedText6 = (Label)this.DtlView.Items[0].FindControl("lblViewAccidentState");
            if (this.extddlATAccidentState.Text != "NA" && this.extddlATAccidentState.Text != "")
            {
                selectedText6.Text = this.extddlATAccidentState.Selected_Text;
            }
            this.txtATHospitalName.Text = patientInfo.Tables[0].Rows[0]["SZ_HOSPITAL_NAME"].ToString();
            this.txtATHospitalAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_HOSPITAL_ADDRESS"].ToString();
            this.txtATDescribeInjury.Text = patientInfo.Tables[0].Rows[0]["SZ_DESCRIBE_INJURY"].ToString();
            this.txtATAdmissionDate.Text = patientInfo.Tables[0].Rows[0]["DT_ADMISSION_DATE"].ToString();
            if (patientInfo.Tables[0].Rows[0]["sz_company_name"].ToString() != "&nbsp;" && patientInfo.Tables[0].Rows[0]["sz_company_name"].ToString() != "")
            {
                Label label6 = (Label)this.DtlView.Items[0].FindControl("lblcopyfrom");
                label6.Text = patientInfo.Tables[0].Rows[0]["sz_company_name"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["sz_reffering_office_id"].ToString() != "")
            {
                this.extddlRefferingOffice.Text = patientInfo.Tables[0].Rows[0]["sz_reffering_office_id"].ToString();
                this.extddlRefferingDoctor.Flag_ID = this.extddlRefferingOffice.Text.ToString();
                DataSet reffOfficeInformation = (new Bill_Sys_DoctorRefferingBO()).getReffOfficeInformation(this.extddlRefferingOffice.Text.ToString());
                if (reffOfficeInformation != null && reffOfficeInformation.Tables.Count > 0 && reffOfficeInformation.Tables[0].Rows.Count > 0)
                {
                    this.txtOfficeAddress.Text = reffOfficeInformation.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString();
                    this.txtOfficeCity.Text = reffOfficeInformation.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString();
                    this.txtOfficeState.Text = reffOfficeInformation.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString();
                    this.txtOfficeZip.Text = reffOfficeInformation.Tables[0].Rows[0]["SZ_OFFICE_ZIP"].ToString();
                }
            }
            if (patientInfo.Tables[0].Rows[0]["sz_reffering_doctor_id"].ToString() != "")
            {
                this.extddlRefferingDoctor.Text = patientInfo.Tables[0].Rows[0]["sz_reffering_doctor_id"].ToString();
                DataSet reffDocInformation = (new Bill_Sys_RefferinDocBilling()).getReffDocInformation(this.extddlRefferingDoctor.Text.ToString());
                if (reffDocInformation != null && reffDocInformation.Tables.Count > 0 && reffDocInformation.Tables[0].Rows.Count > 0)
                {
                    this.txtDoctorNPI.Text = reffDocInformation.Tables[0].Rows[0]["sz_npi"].ToString();
                }
            }
            this.lblMsg.Visible = false;
        }
        catch (Exception ex)
        {
            Exception exception2 = ex;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            this.lblMsg.Text = exception2.ToString();
            this.lblMsg.Visible = true;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetPatienView(string caseID, string companyID)
    {
        DataSet dataSet = new DataSet();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SP_PATIENTPOP_VIEW", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            new SqlDataAdapter(sqlCommand).Fill(dataSet);
        }
        catch (System.Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
    }

    private string getRandomNumber()
    {
        System.Random random = new System.Random();
        return random.Next(1, 10000).ToString();
    }

    public DataSet GetViewBillInfo(string caseID, string companyID)
    {
        DataSet dataSet = new DataSet();
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.sqlCon = new SqlConnection(connectionString);
        try
        {
            this.sqlCon.Open();
            SqlCommand sqlCommand = new SqlCommand("SP_CD_VIEW_BILLS", this.sqlCon);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
        }
        catch (System.Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return dataSet;
    }

    protected void grdPatientDeskList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "genpdf")
            {
                string str = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"];
                string text = str + this.GeneratePDF();
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(System.Web.UI.WebControls.GridView), "Msg", "window.open('" + text.ToString() + "'); ", true);
            }
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void hlnkAssociate_Click(object sender, System.EventArgs e)
    {
    }

    protected void hlnkPatientDesk_Click(object sender, System.EventArgs e)
    {
    }

    protected void LinkButton1_Click(object sender, System.EventArgs e)
    {
        string sZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
        string sZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.rpt_ViewBills.DataSource = this.GetViewBillInfo(sZ_CASE_ID, sZ_COMPANY_ID);
        this.rpt_ViewBills.DataBind();
        this.tblViewBills.Visible = true;
    }

    protected void lnkAddAttorney_Click(object sender, System.EventArgs e)
    {
        this.mp3.Show();
        this.ClearControlAttorney();
        this.tabcontainerPatientEntry.ActiveTabIndex = 4;
    }

    protected void lnkAddbills_Click(object sender, System.EventArgs e)
    {
        try
        {
            this.Session["PassedCaseID"] = this.txtCaseID.Text;
            base.Response.Redirect("Bill_Sys_BillTransaction.aspx", false);
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void lnkDocumentManager_Click(object sender, System.EventArgs e)
    {
        try
        {
            this.Session["PassedCaseID"] = this.txtCaseID.Text;
            string value = this.Session["PassedCaseID"].ToString();
            this.Session["QStrCaseID"] = value;
            this.Session["Case_ID"] = value;
            this.Session["Archived"] = "0";
            this.Session["QStrCID"] = value;
            this.Session["SelectedID"] = value;
            this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            this.Session["SN"] = "0";
            this.Session["LastAction"] = "vb_CaseInformation.aspx";
            string str = "Document Manager/case/vb_CaseInformation.aspx";
            base.Response.Write("<script language='javascript'>window.open('" + str + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }


    protected void lnkGenerateNF2_Click(object sender, System.EventArgs e)
    {
        try
        {
            PDFValueReplacement.PDFValueReplacement pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
            string str = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
            string text = ConfigurationManager.AppSettings["NF2_PDF_FILE"];
            string text2 = ConfigurationManager.AppSettings["NF2_XML_FILE"];
            string str2 = pDFValueReplacement.ReplacePDFvalues(text2, text, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
            string str3 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str + str2;
            this.CheckTemplateStatus(this.Session["TM_SZ_CASE_ID"].ToString());
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(System.Web.UI.WebControls.GridView), "Msg", "window.open('" + str3 + "'); ", true);
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void lnkManageNotes_Click(object sender, System.EventArgs e)
    {
        try
        {
            this.Session["Manage_Case_ID"] = this.txtCaseID.Text;
            base.Response.Redirect("Bill_Sys_ManageNotes.aspx", false);
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }
    protected void lnkNF2Envelope_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            PDFValueReplacement.PDFValueReplacement pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
            string str = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
            string item = ConfigurationManager.AppSettings["NF2_ENVELOPE_PDF_FILE"];
            string item1 = ConfigurationManager.AppSettings["NF2_ENVELOPE_XML_FILE"];
            string str1 = pDFValueReplacement.ReplacePDFvalues(item1, item, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
            string str2 = string.Concat(ApplicationSettings.GetParameterValue("DocumentManagerURL"), str, str1);
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str2, "'); "), true);
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

    protected void lnkNotes_Click(object sender, System.EventArgs e)
    {
        try
        {
            this.Session["SZ_CASE_ID_NOTES"] = this.txtCaseID.Text;
            base.Response.Redirect("Bill_Sys_Notes.aspx", false);
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void lnkPaidBills_Click(object sender, System.EventArgs e)
    {
    }

    protected void lnkTemplateManager_Click(object sender, System.EventArgs e)
    {
        try
        {
            this.Session["TM_SZ_CASE_ID"] = this.txtCaseID.Text;
            string str = "TemplateManager/templates.aspx";
            base.Response.Write("<script language='javascript'>window.open('" + str + "', 'AdditionalData');</script>");
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void lnkUnpaidBills_Click(object sender, System.EventArgs e)
    {
    }

    protected void lnkUpdateAdu_Click(object sender, System.EventArgs e)
    {
        this.ModalPopupaduster.Show();
        this.txtAdjusterid.Text = this.extddlAdjuster.Text;
        string text = this.txtAdjusterid.Text;
        string text2 = this.txtCompanyID.Text;
        Patient_TVBO patient_TVBO = new Patient_TVBO();
        DataSet dataSet = new DataSet();
        dataSet = patient_TVBO.GetAdjusterInfoForUpdate(text, text2);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            this.txtAdjusterPopupName1.Text = dataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            this.txtAdjusterPopupPhone1.Text = dataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            this.txtAdjusterPopupExtension1.Text = dataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            this.txtAdjusterPopupFax1.Text = dataSet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            this.txtAdjusterPopupEmail1.Text = dataSet.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            this.txtAdjusterPopupName1.Enabled = false;
        }
        this.tabcontainerPatientEntry.ActiveTabIndex = 2;
    }

    protected void lnkUpdateAtt_Click(object sender, EventArgs e)
    {
        this.mp2.Show();
        this.txtAttorneyid.Text = this.extddlAttorneyAssign.Text;
        string text = this.txtAttorneyid.Text;
        this._patient_tvbo = new Patient_TVBO();
        DataSet attornyInfoForUpdate = this._patient_tvbo.GetAttornyInfoForUpdate(text);
        if (attornyInfoForUpdate.Tables[0].Rows.Count > 0)
        {
            this.txtAttFirstName.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            this.txtAttLastName.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            this.txtAttCity.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            this.txtAttAddress.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
            this.txtAttZip.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            this.txtAttPhoneNo.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            this.txtAttFax.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            this.txtAttEmailID.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            this.extddlstateAtt.Text = attornyInfoForUpdate.Tables[0].Rows[0]["SZ_ATTORNEY_STATE_ID"].ToString();
            this.extddlAttorneyType.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
            if (attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(13).ToString() != "True")
            {
                this.chkDefaultFirmAtt.Checked = false;
            }
            else
            {
                this.chkDefaultFirmAtt.Checked = true;
            }
            this.txtAttFirstName.Enabled = false;
            this.txtAttLastName.Enabled = false;
        }
        this.tabcontainerPatientEntry.ActiveTabIndex = 4;
    }

    protected void lnkViewBills_Click(object sender, System.EventArgs e)
    {
        try
        {
            this.Session["PassedCaseID"] = this.txtCaseID.Text;
            base.Response.Redirect("Bill_Sys_BillSearch.aspx", false);
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    public void LoadDataOnPage()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            (new Bill_Sys_CaseObject()).SZ_PATIENT_ID = "";
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlCaseType.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlProvider.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlCaseStatus.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlAttorney.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlAdjuster.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlInsuranceCompany.Flag_ID = this.txtCompanyID.Text.ToString();
            if (this.Session["PassedCaseID"] == null)
            {
                base.Response.Redirect("Bill_Sys_SearchCase.aspx", false);
            }
            else
            {
                this.txtPatientID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID;
                this.GetPatientDetails();
                this.Session["Associate_Case_ID"] = this.Session["PassedCaseID"];
                this.txtCaseID.Text = this.Session["PassedCaseID"].ToString();
                EditOperation editOperation = new EditOperation()
                {
                    Xml_File = "CaseDetails.xml",
                    WebPage = this.Page,
                    Primary_Value = this.Session["PassedCaseID"].ToString()
                };
                editOperation.LoadData();
                editOperation = new EditOperation()
                {
                    Xml_File = "CaseDetailsForLabel.xml",
                    WebPage = this.Page,
                    Primary_Value = this.Session["PassedCaseID"].ToString()
                };
                editOperation.LoadData();
                this.Session["AttornyID"] = this.extddlAttorney.Text;
                this.BindSuppliesGrid();
                string str = (new Bill_Sys_PatientBO()).CheckCarriercode(this.txtCaseID.Text, this.txtCompanyID.Text);
                this.txtcarriercode.Text = str;
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

    public void LoadNoteGrid()
    {
        this._listOperation = new ListOperation();
        try
        {
            this.grdNotes.CurrentPageIndex = 0;
            this._listOperation.WebPage = this.Page;
            this._listOperation.Xml_File = "NoteSearch.xml";
            this._listOperation.LoadList();
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void lstEmployerAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.txtEmployercmpAddress.Text = "";
            this.txtEmployercmpCity.Text = "";
            this.extddlEmployercmpState.Text = "NA";
            this.txtEmployercmpZip.Text = "";
            this.txtEmployercmpStreet.Text = "";
            ArrayList employerAddressDetails = this._bill_Sys_PatientBO.GetEmployerAddressDetails(this.lstEmployerAddress.SelectedValue);
            this.txtEmployerAddId.Text = this.lstEmployerAddress.SelectedValue;
            this.txtEmployercmpAddress.Text = employerAddressDetails[2].ToString();
            this.txtEmployercmpCity.Text = employerAddressDetails[3].ToString();
            this.txtEmployercmpZip.Text = employerAddressDetails[5].ToString();
            this.txtEmployercmpStreet.Text = employerAddressDetails[6].ToString();
            if (employerAddressDetails[8].ToString() != "")
            {
                this.extddlEmployercmpState.Text = employerAddressDetails[8].ToString();
            }
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.tabcontainerPatientEntry.ActiveTabIndex = 1;
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

    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            System.Collections.ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(this.lstInsuranceCompanyAddress.SelectedValue);
            this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
            this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
            this.txtInsuranceState.Text = insuranceAddressDetail[4].ToString();
            this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
            this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
            this.txtInsFax.Text = insuranceAddressDetail[9].ToString();
            this.txtInsPhone.Text = insuranceAddressDetail[10].ToString();
            this.txtInsContactPerson.Text = insuranceAddressDetail[11].ToString();
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.tabcontainerPatientEntry.ActiveTabIndex = 2;
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    private void OnClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        base.Response.Redirect("Bill_Sys_CaseDetails.aspx?CaseID=" + e.CommandArgument);
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.Page.LoadComplete += new System.EventHandler(this.Page_Load_Complete);
        Bill_Sys_CaseDetails.log.Debug(string.Concat(new string[]
        {
            "Bill_Sys_Casedetails. Method - Page_Load_Start : ",
            System.DateTime.Now.Hour.ToString(),
            ":",
            System.DateTime.Now.Minute.ToString(),
            ":",
            System.DateTime.Now.Second.ToString(),
            ":",
            System.DateTime.Now.Millisecond.ToString()
        }));
        this.con.SourceGrid = this.grdAttorney;
        this.txtSearchBox.SourceGrid = this.grdAttorney;
        this.grdAttorney.Page = this.Page;
        this.grdAttorney.PageNumberList = this.con;
        try
        {
            try
            {
                if (base.Request.QueryString["CheckSession"] != "" && base.Request.QueryString["CheckSession"] != null)
                {
                    if (base.Request.QueryString["CheckSession"] != ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString())
                    {
                        Bill_Sys_CaseObject bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
                        string text = base.Request.QueryString["CheckSession"];
                        bill_Sys_CaseObject.SZ_PATIENT_ID = caseDetailsBO.GetCasePatientID(text, "");
                        bill_Sys_CaseObject.SZ_CASE_ID = text;
                        bill_Sys_CaseObject.SZ_PATIENT_NAME = caseDetailsBO.GetPatientName(bill_Sys_CaseObject.SZ_PATIENT_ID);
                        bill_Sys_CaseObject.SZ_COMAPNY_ID = caseDetailsBO.GetPatientCompanyID(bill_Sys_CaseObject.SZ_PATIENT_ID);
                        bill_Sys_CaseObject.SZ_CASE_NO = caseDetailsBO.GetCaseNo(bill_Sys_CaseObject.SZ_CASE_ID, bill_Sys_CaseObject.SZ_COMAPNY_ID);
                        this.Session["CASE_OBJECT"] = bill_Sys_CaseObject;
                        Bill_Sys_Case bill_Sys_Case = new Bill_Sys_Case();
                        bill_Sys_Case.SZ_CASE_ID = text;
                        this.Session["CASEINFO"] = bill_Sys_Case;
                        base.Response.Clear();
                        base.Response.ClearContent();
                        base.Response.Write("changed");
                    }
                    else
                    {
                        base.Response.Clear();
                        base.Response.ClearContent();
                        base.Response.Write("same");
                    }
                }
            }
            catch
            {
            }
            if (base.Request.QueryString["Status"] != null && base.Request.QueryString["Status"].ToString() == "Report")
            {
                CaseDetailsBO caseDetailsBO2 = new CaseDetailsBO();
                Bill_Sys_CaseObject bill_Sys_CaseObject2 = new Bill_Sys_CaseObject();
                bill_Sys_CaseObject2.SZ_PATIENT_ID = caseDetailsBO2.GetCasePatientID(base.Request.QueryString["case"].ToString(), "");
                bill_Sys_CaseObject2.SZ_CASE_ID = base.Request.QueryString["case"].ToString();
                bill_Sys_CaseObject2.SZ_PATIENT_NAME = base.Request.QueryString["PName"].ToString();
                this.Session["CASE_OBJECT"] = bill_Sys_CaseObject2;
                bill_Sys_CaseObject2.SZ_CASE_NO = base.Request.QueryString["csno"].ToString();
                bill_Sys_CaseObject2.SZ_COMAPNY_ID = base.Request.QueryString["cmpid"].ToString();
                this.Session["company"] = base.Request.QueryString["cmpid"].ToString();
                Bill_Sys_Case bill_Sys_Case2 = new Bill_Sys_Case();
                bill_Sys_Case2.SZ_CASE_ID = base.Request.QueryString["case"].ToString();
                this.Session["CASEINFO"] = bill_Sys_Case2;
            }
            this.Session["CASE_LIST_GO_BUTTON"] = null;
            bool bT_REFERRING_FACILITY = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlLocation.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlAttorneyAssign.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlAtttype.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlRefferingOffice.Flag_ID = this.txtCompanyID.Text.ToString();
            this.btnSaveAdjuster.Attributes.Add("onclick", "return PopupformValidator('aspnetForm','txtAdjusterPopupName','AdjusterErrordiv');");
            this.lnkGenerateNF2.Attributes.Add("onclick", "return ShowGenerateNF2Link();");
            PopupBO popupBO = new PopupBO();
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != popupBO.GetCompanyID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID))
            {
                base.Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
            }
            this.txtCompanyIDForNotes.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.btnPatientUpdate.Attributes.Add("onclick", "return formValidator('frmPatient','tabcontainerPatientEntry:tabpnlPersonalInformation:txtPatientFName,tabcontainerPatientEntry:tabpnlPersonalInformation:txtPatientLName,tabcontainerPatientEntry:tabpnlPersonalInformation:extddlCaseType,tabcontainerPatientEntry:tabpnlPersonalInformation:extddlCaseStatus');");
            this.lnkUpdateAtt.Attributes.Add("onclick", "return showAttorney();");
            this.btnAttorneyAssign.Attributes.Add("onclick", "return showAttorney();");
            this.btnAddAttorney.Attributes.Add("onclick", "return AttorneyAdd();");
            if (!base.IsPostBack)
            {
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).IS_EMPLOYER == "1")
                {
                    this.tabpnlInsuranceInformation.Visible = false;
                    this.tr1.Visible = false;
                    this.tr2.Visible = false;
                    this.tr3.Visible = false;
                    this.tr4.Visible = false;
                    this.tr5.Visible = false;
                }
                else
                {
                    this.trEmp1.Visible = false;
                    this.trEmp2.Visible = false;
                    this.trEmp3.Visible = false;
                    this.trEmp4.Visible = false;
                }
                this.ajAutoIns.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.Ajautoattorney.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.ajAutoEmployer.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (base.Request.QueryString["CaseID"] != null && base.Request.QueryString["CaseID"].ToString() != "")
                {
                    CaseDetailsBO caseDetailsBO3 = new CaseDetailsBO();
                    Bill_Sys_CaseObject bill_Sys_CaseObject3 = new Bill_Sys_CaseObject();
                    bill_Sys_CaseObject3.SZ_PATIENT_ID = caseDetailsBO3.GetCasePatientID(base.Request.QueryString["CaseID"].ToString(), "");
                    bill_Sys_CaseObject3.SZ_CASE_ID = base.Request.QueryString["CaseID"].ToString();
                    if (base.Request.QueryString["cmp"] != null)
                    {
                        bill_Sys_CaseObject3.SZ_CASE_NO = caseDetailsBO3.GetCaseNo(bill_Sys_CaseObject3.SZ_CASE_ID, base.Request.QueryString["cmp"].ToString());
                        this.Session["company"] = base.Request.QueryString["cmp"].ToString();
                    }
                    else
                    {
                        bill_Sys_CaseObject3.SZ_CASE_NO = caseDetailsBO3.GetCaseNo(bill_Sys_CaseObject3.SZ_CASE_ID, this.Session["company"].ToString());
                    }
                    bill_Sys_CaseObject3.SZ_PATIENT_NAME = caseDetailsBO3.GetPatientName(bill_Sys_CaseObject3.SZ_PATIENT_ID);
                    bill_Sys_CaseObject3.SZ_COMAPNY_ID = this.Session["company"].ToString();
                    this.Session["CASE_OBJECT"] = bill_Sys_CaseObject3;
                }
                if (this.Session["CASE_OBJECT"] != null)
                {
                    this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this.Session["company"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    base.Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }
                this.LoadNoteGrid();
                this.caseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this.ShowPopupNotes(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                this.grdAssociatedDiagnosisCode.Visible = false;
                this._bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                this.grdAssociatedDiagnosisCode.DataSource = this._bill_Sys_ProcedureCode_BO.GetAssociatedDiagnosisCode_List(this.caseID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID).Tables[0];
                this.grdAssociatedDiagnosisCode.DataBind();
                Bill_Sys_Case bill_Sys_Case3 = new Bill_Sys_Case();
                bill_Sys_Case3.SZ_CASE_ID = this.txtCaseID.Text;
                this.Session["CASEINFO"] = bill_Sys_Case3;
                this.Session["PassedCaseID"] = this.txtCaseID.Text;
                string value = this.Session["PassedCaseID"].ToString();
                this.Session["QStrCaseID"] = value;
                this.Session["Case_ID"] = value;
                this.Session["Archived"] = "0";
                this.Session["QStrCID"] = value;
                this.Session["SelectedID"] = value;
                this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["SN"] = "0";
                this.Session["LastAction"] = "vb_CaseInformation.aspx";
                this.Session["SZ_CASE_ID_NOTES"] = this.txtCaseID.Text;
                this.Session["TM_SZ_CASE_ID"] = this.txtCaseID.Text;
                this.CheckTemplateStatus(this.txtCaseID.Text);
                this.LoadDataOnPage();
                int dosespotUserDetails = this.GetDosespotUserDetails();
                if (dosespotUserDetails != 1)
                {
                    this.btnDosespot.Visible = false;
                }
                else
                {
                    this.btnDosespot.Visible = true;
                    this.GetDosespotPatientID();
                }
                this.UserPatientInfoControl.GetPatienDeskList(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            }
            if (!base.IsPostBack)
            {
                ReminderBO reminderBO = new ReminderBO();
                DataSet dataSet = new DataSet();
                string sZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                string text2 = this.txtCaseID.Text;
                System.DateTime dateTime = System.Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                if (reminderBO.LoadReminderDetailsForCaseDeatils(sZ_USER_ID, dateTime, text2).Tables[0].Rows.Count > 0)
                {
                    this.Page.RegisterStartupScript("ss", "<script language='javascript'> ShowReminder();</script>");
                }
            }
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && ((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
            {
                System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblView");
                System.Web.UI.WebControls.Label label2 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblViewChartNo");
                this.lblChart.Visible = true;
                this.txtChartNo.Visible = true;
                label.Visible = true;
                label2.Visible = true;
            }
            else
            {
                System.Web.UI.WebControls.Label label3 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblView");
                System.Web.UI.WebControls.Label label4 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblViewChartNo");
                this.lblChart.Visible = false;
                this.txtChartNo.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
            }
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && ((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PATIENT_WALK_IN_ON_WORKAREA == "1")
            {
                this.lbl.Visible = true;
                this.lblwalkin.Visible = true;
            }
            else
            {
                this.lbl.Visible = false;
                this.lblwalkin.Visible = false;
            }
            this.txtUserID.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this.txtNoteCode.Text = Note_Code.New_Note_Added;
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_Copy_From.ToString() == "1")
            {
                System.Web.UI.WebControls.Label label5 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblcopyfrom");
                System.Web.UI.WebControls.Label label6 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblcopyfrm");
                label6.Visible = false;
                label5.Visible = false;
            }
            else
            {
                System.Web.UI.WebControls.Label label7 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblcopyfrom");
                System.Web.UI.WebControls.Label label8 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblcopyfrm");
                label8.Visible = true;
                label7.Visible = true;
            }
            this.GetAssociateCases();
            this.CheckMemoExists();
            if (bT_REFERRING_FACILITY || ((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1")
            {
                System.Web.UI.WebControls.Label label9 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblVLocation1");
                System.Web.UI.WebControls.Label label10 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblLocation");
                label9.Visible = false;
                label10.Visible = false;
                this.lblLocationddl.Visible = false;
                this.extddlLocation.Visible = false;
            }
            else
            {
                System.Web.UI.WebControls.Label label11 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblVLocation1");
                System.Web.UI.WebControls.Label label12 = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblLocation");
                label11.Visible = true;
                label12.Visible = true;
                this.lblLocationddl.Visible = true;
                this.extddlLocation.Visible = true;
            }
            if (!base.IsPostBack)
            {
                if (this.txtCompanyID.Text == "CO000000000000000118")
                {
                    this.txtcarriercode.Visible = true;
                    this.lblcarriercode.Visible = true;
                }
                else
                {
                    this.txtcarriercode.Visible = false;
                    this.lblcarriercode.Visible = false;
                }
                this.setattorney();
                this.grdAttorney.XGridBindSearch();
            }
            if (!base.IsPostBack)
            {
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).IS_EMPLOYER == "1")
                {
                    Bill_Sys_PatientBO bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    DataSet caseEmployer = bill_Sys_PatientBO.GetCaseEmployer(this.txtCaseID.Text, this.txtCompanyID.Text);
                    if (caseEmployer.Tables.Count > 0)
                    {
                        if (caseEmployer.Tables[0].Rows.Count > 0)
                        {
                            this.lstEmployerAddress.DataSource = this._bill_Sys_PatientBO.GetEmployerCompanyAddress(caseEmployer.Tables[0].Rows[0]["SZ_EMPLOYER_ID"].ToString(), this.txtCompanyID.Text);
                            DataSet dataSet2 = new DataSet();
                            dataSet2 = (DataSet)this.lstEmployerAddress.DataSource;
                            this.lstEmployerAddress.DataTextField = "DESCRIPTION";
                            this.lstEmployerAddress.DataValueField = "CODE";
                            this.lstEmployerAddress.DataBind();
                            for (int i = 0; i < this.lstEmployerAddress.Items.Count; i++)
                            {
                                if (this.lstEmployerAddress.Items[i].Value == caseEmployer.Tables[0].Rows[0]["SZ_EMP_ADDRESS_ID"].ToString())
                                {
                                    this.lstEmployerAddress.SelectedIndex = i;
                                    break;
                                }
                            }
                            this.txtEmployerAddId.Text = caseEmployer.Tables[0].Rows[0]["SZ_EMP_ADDRESS_ID"].ToString();
                            this.hdnEmployerId.Value = caseEmployer.Tables[0].Rows[0]["SZ_EMPLOYER_ID"].ToString();
                            this.txtEmployerCompany.Text = caseEmployer.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
                            this.txtEmployercmpAddress.Text = caseEmployer.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString();
                            this.txtEmployercmpCity.Text = caseEmployer.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString();
                            this.txtEmployercmpZip.Text = caseEmployer.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();
                            this.extddlEmployercmpState.Text = caseEmployer.Tables[0].Rows[0]["SZ_EMPLOYER_STATE_ID"].ToString();
                            this.txtEmployercmpStreet.Text = caseEmployer.Tables[0].Rows[0]["SZ_EMPLOYER_STREET"].ToString();
                        }
                        else
                        {
                            this.lstEmployerAddress.Items.Clear();
                            this.txtEmployerAddId.Text = "";
                            this.hdnEmployerId.Value = "";
                            this.txtEmployerName.Text = "";
                            this.txtEmployercmpAddress.Text = "";
                            this.txtEmployercmpCity.Text = "";
                            this.txtEmployercmpZip.Text = "";
                            this.extddlEmployercmpState.Text = "";
                            this.txtEmployercmpStreet.Text = "";
                        }
                    }
                    else
                    {
                        this.lstEmployerAddress.Items.Clear();
                        this.txtEmployerAddId.Text = "";
                        this.hdnEmployerId.Value = "";
                        this.txtEmployerName.Text = "";
                        this.txtEmployercmpAddress.Text = "";
                        this.txtEmployercmpCity.Text = "";
                        this.txtEmployercmpZip.Text = "";
                        this.extddlEmployercmpState.Text = "";
                        this.txtEmployercmpStreet.Text = "";
                    }
                }
            }
            Bill_Sys_CaseDetails.log.Debug(string.Concat(new string[]
            {
                "Bill_Sys_Casedetails. Method - Page_Load_End : ",
                System.DateTime.Now.Hour.ToString(),
                ":",
                System.DateTime.Now.Minute.ToString(),
                ":",
                System.DateTime.Now.Second.ToString(),
                ":",
                System.DateTime.Now.Millisecond.ToString()
            }));
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_CaseDetails.aspx");
        }
    }

    protected void Page_Load_Complete(object sender, System.EventArgs e)
    {
        try
        {
            this.SessionCheck.Value = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
        }
        catch
        {
            this.SessionCheck.Value = "";
        }
    }

    protected void Page_LoadComplete(object sender, System.EventArgs e)
    {
    }

    protected void rptPatientDeskList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "genpdf")
            {
                string str = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"];
                string text = str + this.GeneratePDF();
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(System.Web.UI.WebControls.GridView), "Msg", "window.open('" + text.ToString() + "'); ", true);
            }
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    private void SaveNotes()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._notesOperation = new NotesOperation();
        try
        {
            this._notesOperation.WebPage = this.Page;
            this._notesOperation.Xml_File = "InformationNotesXML.xml";
            this._notesOperation.Case_ID = this.txtCaseID.Text;
            this._notesOperation.User_ID = this.txtUserID.Text;
            this._notesOperation.Company_ID = this.txtCompanyID.Text;
            this._notesOperation.SaveNotesOperation();
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

    public void setattorney()
    {
        string text = this.extddlAttorney.Text;
        this.hdnattorneycode.Value = text;
        if (text != "NA")
        {
            DataSet attornyInfo = new Patient_TVBO().GetAttornyInfo(text);
            if (attornyInfo.Tables[0].Rows.Count > 0)
            {
                this.txtattorneycity.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_city"].ToString();
                this.txtattorneyzip.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_zip"].ToString();
                this.txtattorneState.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_state"].ToString();
                this.txtattorneyphone.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_PHONE"].ToString();
                this.txtattorneyfax.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_FAX"].ToString();
                this.txtattorneyaddress.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_Address"].ToString();
            }
            else
            {
                this.txtattorneycity.Text = "";
                this.txtattorneyzip.Text = "";
                this.txtattorneState.Text = "";
                this.txtattorneyaddress.Text = "";
            }
        }
        else
        {
            this.txtattorneycity.Text = "";
            this.txtattorneyzip.Text = "";
            this.txtattorneState.Text = "";
            this.txtattorneyaddress.Text = "";
            this.txtattorneyphone.Text = "";
            this.txtattorneyfax.Text = "";
        }
    }

    private void ShowPopupNotes(string szCaseid)
    {
        this._manageNotesBO = new Billing_Sys_ManageNotesBO();
        this._arrayList = new System.Collections.ArrayList();
        try
        {
            this._arrayList = this._manageNotesBO.GetPopupNotesDesc(szCaseid, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            for (int i = 0; i < this._arrayList.Count; i++)
            {
                base.Response.Write("<script language='javascript'>alert('" + this._arrayList[i].ToString() + "');</script>");
            }
        }
        catch (System.Exception ex)
        {
            this.usrMessage.PutMessage(ex.ToString());
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
    }

    protected void txtAttorneyCompany_TextChanged(object sender, System.EventArgs args)
    {
        string value = this.hdnattorney.Value;
        this.extddlAttorney.Text = value;
        this.hdnattorneycode.Value = this.extddlAttorney.Text;
        this.Session["AttornyID"] = this.extddlAttorney.Text;
        if (this.txtAttorneyCompany.Text != "")
        {
            if (value != "0" && value != "")
            {
                try
                {
                    DataSet attornyInfo = new Patient_TVBO().GetAttornyInfo(value);
                    if (attornyInfo.Tables[0].Rows.Count > 0)
                    {
                        this.txtattorneycity.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_city"].ToString();
                        this.txtattorneyzip.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_zip"].ToString();
                        this.txtattorneState.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_state"].ToString();
                        this.txtattorneyphone.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_PHONE"].ToString();
                        this.txtattorneyfax.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_FAX"].ToString();
                        this.txtattorneyaddress.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_Address"].ToString();
                    }
                    else
                    {
                        this.txtattorneycity.Text = "";
                        this.txtattorneyzip.Text = "";
                        this.txtattorneState.Text = "";
                        this.txtattorneyaddress.Text = "";
                    }
                }
                catch (System.Exception ex)
                {
                    this.usrMessage.PutMessage(ex.ToString());
                    this.usrMessage.SetMessageType(0);
                    this.usrMessage.Show();
                }
            }
            else
            {
                this.txtattorneycity.Text = "";
                this.txtattorneyzip.Text = "";
                this.txtattorneState.Text = "";
                this.txtattorneyaddress.Text = "";
                this.txtattorneyphone.Text = "";
                this.txtattorneyfax.Text = "";
                this.hdnattorney.Value = "";
            }
        }
        else
        {
            this.txtattorneycity.Text = "";
            this.txtattorneyzip.Text = "";
            this.txtattorneState.Text = "";
            this.txtattorneyaddress.Text = "";
            this.txtattorneyphone.Text = "";
            this.txtattorneyfax.Text = "";
            this.hdnattorney.Value = "";
        }
        this.tabcontainerPatientEntry.ActiveTabIndex = 1;
    }


    protected void txtInsuranceCompany_TextChanged1(object sender, System.EventArgs args)
    {
        if (this.hdadjusterCode.Value != "" && this.hdadjusterCode.Value != "0")
        {
            this.hdacode.Value = this.hdadjusterCode.Value;
            this.hdadjusterCode.Value = "";
            this.getAdjusterdata(this.txtCompanyID.Text, this.hdacode.Value);
        }
        else
        {
            this.txtAdjusterPopupExtension1.Text = "";
            this.txtAdjusterPopupFax1.Text = "";
            this.txtAdjusterPopupEmail1.Text = "";
            this.hdacode.Value = "";
            this.Session["attoenyset"] = null;
        }
    }

    protected void UpdateCopyToCase(string associatecase)
    {
        string text = associatecase;
        string text2 = null;
        bool flag = false;
        try
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new System.FormatException();
            }
            if (text.IndexOf(',') == -1)
            {
                text += ",";
            }
            string value = "";
            if (!this.txtInsuranceAddress.Text.Equals(""))
            {
                value = this.lstInsuranceCompanyAddress.Text;
            }
            string[] array = text.Split(new char[]
            {
                ','
            });
            for (int i = 0; i < array.Length; i++)
            {
                Patient_TVBO patient_TVBO = new Patient_TVBO();
                patient_TVBO.UpdateInsurancetoCase(new System.Collections.ArrayList
                {
                    array[i].ToString(),
                    this.hdninsurancecode.Value,
                    value,
                    this.txtCompanyID.Text,
                    this.txtClaimNumber.Text,
                    this.txtPolicyNumber.Text,
                    this.extddlAdjuster.Text,
                    this.txtPolicyHolder.Text,
                    "COPYTO"
                });
            }
        }
        catch (System.FormatException)
        {
            text2 = "";
            flag = true;
        }
        finally
        {
            if (!flag)
            {
                text2 = "The Insurance Company and Address Updated to " + this.associatecaseno + "successfully.";
            }
            this.usrMessage.PutMessage(text2);
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
        }
    }

    protected void UpdateData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        try
        {
            this._editOperation.Primary_Value = this.txtCaseID.Text.ToString();
            this._editOperation.WebPage = this.Page;
            this._editOperation.Xml_File = "CaseMaster.xml";
            this._editOperation.UpdateMethod();
            this._editOperation.Primary_Value = this.txtCaseID.Text.ToString();
            this._editOperation.WebPage = this.Page;
            this._editOperation.Xml_File = "PatientAcccidentInfoEntry.xml";
            this._editOperation.UpdateMethod();
            this.LoadDataOnPage();
            this.lblMsg.Visible = true;
            this.lblMsg.Text = "Case Details Updated Successfully ...!";
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


    protected void UpdatePatientInformation()
    {
        try
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            arrayList.Add(this.txtPatientFName.Text);
            arrayList.Add(this.txtPatientLName.Text);
            arrayList.Add(this.txtPatientAge.Text);
            arrayList.Add(this.txtPatientAddress.Text);
            arrayList.Add(this.txtPatientStreet.Text);
            arrayList.Add(this.txtPatientCity.Text);
            arrayList.Add(this.txtPatientZip.Text);
            arrayList.Add(this.txtPatientPhone.Text);
            arrayList.Add(this.txtPatientEmail.Text);
            arrayList.Add(this.txtCompanyID.Text);
            arrayList.Add(this.txtWorkPhone.Text);
            arrayList.Add(this.txtExtension.Text);
            arrayList.Add(this.txtMI.Text);
            arrayList.Add(this.txtPolicyNumber.Text);
            arrayList.Add(this.txtSocialSecurityNumber.Text);
            arrayList.Add(this.txtDateOfBirth.Text);
            arrayList.Add(this.ddlSex.SelectedValue);
            arrayList.Add(this.txtDateOfInjury.Text);
            arrayList.Add(this.txtJobTitle.Text);
            arrayList.Add(this.txtWorkActivites.Text);
            arrayList.Add(this.txtState.Text);
            arrayList.Add(this.txtCarrierCaseNo.Text);
            arrayList.Add(this.txtEmployerName.Text);
            arrayList.Add(this.txtEmployerPhone.Text);
            arrayList.Add(this.txtEmployerAddress.Text);
            arrayList.Add(this.txtEmployerCity.Text);
            arrayList.Add(this.txtEmployerState.Text);
            arrayList.Add(this.txtEmployerZip.Text);
            System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)this.DtlView.Items[0].FindControl("lblVLocation1");
            arrayList.Add(label.Text);
            arrayList.Add("UPDATE");
            arrayList.Add(this.txtPatientID.Text);
            if (this.chkWrongPhone.Checked)
            {
                arrayList.Add("True");
            }
            else
            {
                arrayList.Add("False");
            }
            if (this.chkTransportation.Checked)
            {
                arrayList.Add("True");
            }
            else
            {
                arrayList.Add("False");
            }
            arrayList.Add(this.extddlEmployerState.Text);
            arrayList.Add(this.extddlPatientState.Text);
            arrayList.Add(this.txtChartNo.Text);
            arrayList.Add(this.txtDateofFirstTreatment.Text);
            arrayList.Add(this.rdlPatient_relation.SelectedValue.ToString());
            arrayList.Add(this.rdlPatient_Status.SelectedValue.ToString());            
            arrayList.Add(this.txtCellNo.Text);
            Patient_TVBO patient_TVBO = new Patient_TVBO();
            patient_TVBO.savePatientInformation(arrayList);
            arrayList = new System.Collections.ArrayList();
            arrayList.Add(this.txtAccidentID.Text);
            arrayList.Add(this.txtPatientID.Text);
            arrayList.Add(this.txtPlatenumber.Text);
            arrayList.Add(this.txtDateofAccident.Text);
            arrayList.Add(this.txtAccidentAddress.Text);
            arrayList.Add(this.txtAccidentCity.Text);
            arrayList.Add(this.txtAccidentState.Text);
            arrayList.Add(this.txtPolicyReport.Text);
            arrayList.Add(this.txtListOfPatient.Text);
            arrayList.Add(this.txtCompanyID.Text);
            if (this.txtAccidentID.Text != "")
            {
                arrayList.Add("UPDATE");
            }
            else
            {
                arrayList.Add("Add");
            }
            arrayList.Add(this.extddlATAccidentState.Text);
            arrayList.Add(this.txtPatientType.Text);
            patient_TVBO.savePatientAccidentInformation(arrayList);
            arrayList = new System.Collections.ArrayList();
            arrayList.Add(this.txtCaseID.Text);
            arrayList.Add("");
            arrayList.Add(this.extddlCaseType.Text);
            if (!this.txtInsuranceCompany.Text.Equals("") && !this.txtInsuranceCompany.Text.Equals("No suggestions found for your search") && this.hdninsurancecode.Value != "")
            {
                arrayList.Add(this.hdninsurancecode.Value);
            }
            else
            {
                arrayList.Add("");
            }
            arrayList.Add(this.extddlCaseStatus.Text);
            arrayList.Add(this.extddlAttorney.Text);
            arrayList.Add(this.txtPatientID.Text);
            arrayList.Add(this.txtCompanyID.Text);
            if (this.txtClaimNumber.Text != "")
            {
                arrayList.Add(this.txtClaimNumber.Text);
            }
            else
            {
                arrayList.Add("");
            }
            if (this.txtPolicyNumber.Text != "")
            {
                arrayList.Add(this.txtPolicyNumber.Text);
            }
            else
            {
                arrayList.Add("");
            }
            arrayList.Add(this.txtDateofAccident.Text);
            arrayList.Add(this.extddlAdjuster.Text);
            arrayList.Add(this.txtAssociateDiagnosisCode.Text);
            if (!this.lstInsuranceCompanyAddress.Text.Equals(""))
            {
                arrayList.Add(this.lstInsuranceCompanyAddress.Text);
            }
            else
            {
                arrayList.Add("");
            }
            arrayList.Add("UPDATE");
            if (this.txtPolicyHolder.Text != "")
            {
                arrayList.Add(this.txtPolicyHolder.Text);
            }
            else
            {
                arrayList.Add("");
            }
            arrayList.Add(this.extddlLocation.Text);
            arrayList.Add(this.txtWCBNo.Text);
            arrayList.Add(this.txtPolicyHolderAddress.Text);
            arrayList.Add(this.txtPolicyCity.Text);
            arrayList.Add(this.extdlPolicyState.Text);
            arrayList.Add(this.txtPolicyZip.Text);
            arrayList.Add(this.txtPolicyPhone.Text);
            arrayList.Add(this.rdlPolicyHolderRelation.SelectedValue.ToString());
            arrayList.Add(this.rdlPatientMarritalStatus.SelectedValue.ToString());
            patient_TVBO = new Patient_TVBO();
            patient_TVBO.saveCaseInformation(arrayList);
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).IS_EMPLOYER == "1")
            {
                this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                this._bill_Sys_PatientBO.SaveEmployer(this.hdnEmployerId.Value, this.txtEmployerAddId.Text, this.txtCompanyID.Text, this.txtCaseID.Text);
            }
            patient_TVBO.saveAccidentInformation(new System.Collections.ArrayList
            {
                this.txtPatientID.Text,
                this.txtATPlateNumber.Text,
                this.txtATAccidentDate.Text,
                this.txtATAddress.Text,
                this.txtATCity.Text,
                this.txtATReportNumber.Text,
                this.txtATAdditionalPatients.Text,
                this.extddlATAccidentState.Text,
                this.txtATHospitalName.Text,
                this.txtATHospitalAddress.Text,
                this.txtATDescribeInjury.Text,
                this.txtATAdmissionDate.Text,
                this.txtPatientType.Text,
                this.txtAccidentSpecialty.Text
            });
            Bill_Sys_Reffering_Case bill_Sys_Reffering_Case = new Bill_Sys_Reffering_Case();
            System.Collections.ArrayList arrayList2 = new System.Collections.ArrayList();
            string value = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            arrayList2.Add(this.txtCompanyID.Text.ToString());
            arrayList2.Add(value);
            arrayList2.Add("UPDATE");
            arrayList2.Add(this.extddlRefferingOffice.Text.ToString());
            arrayList2.Add(this.extddlRefferingDoctor.Text.ToString());
            if (this.extddlRefferingOffice.Text.ToString() != "NA" && this.extddlRefferingDoctor.Text.ToString() != "NA")
            {
                bill_Sys_Reffering_Case.saveRefferingInformation(arrayList2);
            }
            this._DAO_NOTES_EO = new DAO_NOTES_EO();
            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "PATIENT_UPDATED";
            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "PatientId : " + this.txtPatientID + ", Name : " + this.txtPatientFName + " " + txtPatientLName.Text;
            this._DAO_NOTES_BO = new DAO_NOTES_BO();
            this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
        }
        catch (System.Exception var_7_9B7)
        {
            throw;
        }
    }

    protected void txtEmployerCompany_TextChanged(object sender, EventArgs args)
    {
        this.lstEmployerAddress.Items.Clear();
        this.txtEmployerAddId.Text = "";
        this.txtEmployercmpAddress.Text = "";
        this.txtEmployercmpCity.Text = "";
        this.extddlEmployercmpState.Text = "NA";
        this.txtEmployercmpZip.Text = "";
        this.txtEmployercmpStreet.Text = "";
        string value = this.hdnEmployerId.Value;
        if (value != null && (value != "0" || value != ""))
        {
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstEmployerAddress.DataSource = this._bill_Sys_PatientBO.GetEmployerCompanyAddress(value, this.txtCompanyID.Text);
            DataSet dataSet = new DataSet();
            dataSet = (DataSet)this.lstEmployerAddress.DataSource;
            this.lstEmployerAddress.DataTextField = "DESCRIPTION";
            this.lstEmployerAddress.DataValueField = "CODE";
            this.lstEmployerAddress.DataBind();
            this.Page.MaintainScrollPositionOnPostBack = true;
            if (dataSet.Tables[0].Rows.Count != 0)
            {
                this.lstEmployerAddress.DataBind();
                this.lstEmployerAddress.SelectedIndex = 0;
                ArrayList employerAddressDetails = this._bill_Sys_PatientBO.GetEmployerAddressDetails(dataSet.Tables[0].Rows[0][0].ToString());
                this.txtEmployerAddId.Text = dataSet.Tables[0].Rows[0][0].ToString();
                this.txtEmployercmpAddress.Text = employerAddressDetails[2].ToString();
                this.txtEmployercmpCity.Text = employerAddressDetails[3].ToString();
                this.extddlEmployercmpState.Text = employerAddressDetails[4].ToString();
                this.txtEmployercmpZip.Text = employerAddressDetails[5].ToString();
                this.txtEmployercmpStreet.Text = employerAddressDetails[6].ToString();
                if (employerAddressDetails[8].ToString() != "")
                {
                    this.extddlEmployercmpState.Text = employerAddressDetails[8].ToString();
                }
                this.Page.MaintainScrollPositionOnPostBack = true;
            }
            else
            {
                this.lstEmployerAddress.Items.Clear();
                this.txtEmployerAddId.Text = "";
            }
            ArrayList arrayLists = new ArrayList();
            arrayLists = this._bill_Sys_PatientBO.GetDefaultDetail(value);
            if (arrayLists.Count != 0)
            {
                this.txtEmployercmpAddress.Text = arrayLists[1].ToString();
                int num = 0;
                while (num < this.lstInsuranceCompanyAddress.Items.Count)
                {
                    if (this.lstEmployerAddress.Items[num].Value != arrayLists[0].ToString())
                    {
                        num++;
                    }
                    else
                    {
                        this.lstEmployerAddress.SelectedIndex = num;
                        break;
                    }
                }
                this.txtEmployerAddId.Text = arrayLists[0].ToString();
                this.txtEmployercmpCity.Text = arrayLists[2].ToString();
                this.txtEmployercmpZip.Text = arrayLists[3].ToString();
                this.txtEmployercmpStreet.Text = arrayLists[4].ToString();
                this.extddlEmployercmpState.Text = arrayLists[6].ToString();
                this.extddlInsuranceState.Text = arrayLists[5].ToString();
                this.Page.MaintainScrollPositionOnPostBack = true;
            }
        }
        this.tabcontainerPatientEntry.ActiveTabIndex = 1;
    }

    protected void txtInsuranceCompany_TextChanged(object sender, EventArgs args)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string value = this.hdninsurancecode.Value;
        int num = 0;
        if (this.txtInsuranceCompany.Text == "")
        {
            this.lstInsuranceCompanyAddress.Items.Clear();
            this.txtInsuranceAddress.Text = "";
            this.txtInsuranceCity.Text = "";
            this.txtInsuranceState.Text = "";
            this.extddlInsuranceState.Text = "";
            this.txtInsuranceZip.Text = "";
            this.txtInsuranceStreet.Text = "";
            this.txtInsPhone.Text = "";
            this.hdninsurancecode.Value = "";
        }
        else
        {
            string str = value;
            string str1 = str;
            if (str != null && (str1 == "0" || str1 == ""))
            {
                this.lstInsuranceCompanyAddress.Items.Clear();
                this.txtInsuranceAddress.Text = "";
                this.txtInsuranceCity.Text = "";
                this.txtInsuranceState.Text = "";
                this.extddlInsuranceState.Text = "";
                this.txtInsuranceZip.Text = "";
                this.txtInsuranceStreet.Text = "";
                this.txtInsPhone.Text = "";
                this.hdninsurancecode.Value = "";
                return;
            }
            try
            {
                this.ClearInsurancecontrol();
                this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(value);
                DataSet dataSet = new DataSet();
                dataSet = (DataSet)this.lstInsuranceCompanyAddress.DataSource;
                this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                this.lstInsuranceCompanyAddress.DataValueField = "CODE";
                if (dataSet.Tables[0].Rows.Count != 0)
                {
                    this.lstInsuranceCompanyAddress.DataBind();
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (dataSet.Tables[0].Rows[i][2].ToString().Equals("1"))
                        {
                            num = i;
                        }
                    }
                    this.lstInsuranceCompanyAddress.SelectedIndex = num;
                    ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSet.Tables[0].Rows[num][0].ToString());
                    this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
                    this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
                    this.txtInsuranceState.Text = insuranceAddressDetail[4].ToString();
                    this.extddlInsuranceState.Text = insuranceAddressDetail[4].ToString();
                    this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
                    this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
                    if (insuranceAddressDetail[8].ToString() != "")
                    {
                        this.extddlInsuranceState.Text = insuranceAddressDetail[8].ToString();
                    }
                    this.txtInsPhone.Text = insuranceAddressDetail[10].ToString();
                    this.Page.MaintainScrollPositionOnPostBack = true;
                }
                else
                {
                    this.lstInsuranceCompanyAddress.Items.Clear();
                }
                this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                this.lstInsuranceCompanyAddress.DataValueField = "CODE";
                this.lstInsuranceCompanyAddress.DataBind();
                this.Page.MaintainScrollPositionOnPostBack = true;
                this.tabcontainerPatientEntry.ActiveTabIndex = 2;
                if (this.lstInsuranceCompanyAddress.Items.Count == 1)
                {
                    ArrayList arrayLists = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSet.Tables[0].Rows[0][0].ToString());
                    this.txtInsuranceAddress.Text = arrayLists[2].ToString();
                    this.txtInsuranceCity.Text = arrayLists[3].ToString();
                    this.txtInsuranceState.Text = arrayLists[4].ToString();
                    this.extddlInsuranceState.Text = arrayLists[4].ToString();
                    this.txtInsuranceZip.Text = arrayLists[5].ToString();
                    this.txtInsuranceStreet.Text = arrayLists[6].ToString();
                    if (arrayLists[8].ToString() != "")
                    {
                        this.extddlInsuranceState.Text = arrayLists[8].ToString();
                    }
                    this.Page.MaintainScrollPositionOnPostBack = true;
                }
                ArrayList arrayLists1 = new ArrayList();
                this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.hdninsurancecode.Value);
                if (arrayLists1.Count != 0)
                {
                    this.txtInsuranceAddress.Text = arrayLists1[1].ToString();
                    int num1 = 0;
                    while (num1 < this.lstInsuranceCompanyAddress.Items.Count)
                    {
                        if (this.lstInsuranceCompanyAddress.Items[num1].Value != arrayLists1[0].ToString())
                        {
                            num1++;
                        }
                        else
                        {
                            this.lstInsuranceCompanyAddress.SelectedIndex = num1;
                            break;
                        }
                    }
                    this.txtInsuranceCity.Text = arrayLists1[2].ToString();
                    this.txtInsuranceZip.Text = arrayLists1[3].ToString();
                    this.txtInsuranceStreet.Text = arrayLists1[4].ToString();
                    this.txtInsuranceState.Text = arrayLists1[6].ToString();
                    this.extddlInsuranceState.Text = arrayLists1[5].ToString();
                    this.Page.MaintainScrollPositionOnPostBack = true;
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

}
