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

public partial class AJAX_Pages_Bill_Sys_Pre_Authsent : PageBase
{
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    protected void Page_Init(object sender, EventArgs e)
    {

        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string sType = browser.Type;
        string sName = browser.Browser;
        string szCSS;
        string _url = "";
        if (Request.RawUrl.IndexOf("?") > 0)
        {
            _url = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("?"));
        }
        else
        {
            _url = Request.RawUrl;
        }
        if (browser.Browser.ToLower().Contains("firefox"))
        {
            szCSS = "css/main-ff.css";
        }
        else
        {
            if (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple"))
            {
                szCSS = "css/main-ch.css";
            }
            else
            {
                szCSS = "css/main-ie.css";
            }
        }
        System.Text.StringBuilder b = new System.Text.StringBuilder();
        b.AppendLine("");
        if (_url.Contains("AJAX Pages")) { b.AppendLine("<link rel='shortcut icon' href='../Registration/icon.ico' />"); } else b.AppendLine("<link rel='shortcut icon' href='Registration/icon.ico' />");
        b.AppendLine("<link type='text/css' rel='stylesheet' href='css/intake-sheet-ff.css' />");
        b.AppendLine("<link href='Css/mainmaster.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link href='Css/UI.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link type='text/css' rel='stylesheet' href='css/style.css' />");
        b.AppendLine("<link href='css/style.css'  rel='stylesheet' type='text/css'  />");
        b.AppendLine("<link href='" + szCSS + "' type='text/css' rel='Stylesheet' />");
        this.framehead.InnerHtml = b.ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ss", "spanhide();", true);
        }
        if (Session["Caseid"] != null)
        {
            txtcaseid.Text = Session["Caseid"].ToString();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            DataSet ds = GetPatienView(txtcaseid.Text, txtCompanyID.Text);
        }
        ajAutoIns.ContextKey = Session["Caseid"].ToString();
        extddlInsuranceCompany.Flag_ID = txtCompanyID.Text;
        if (!IsPostBack)
        {
            extddlAttorney.Flag_ID = txtCompanyID.Text;
            extddlCaseStatus.Flag_ID = txtCompanyID.Text;
            extddlCaseType.Flag_ID = txtCompanyID.Text;
            extddlAdjuster.Flag_ID = txtCompanyID.Text.ToString();
            GetPatientDetails();


        }

    }
    public DataSet GetPatienView(string caseID, string companyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_PATIENTPOP_VIEW", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

       
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void txtInsuranceCompany_TextChanged(object sender, EventArgs args)
    {
        string strInsuranceID = hdninsurancecode.Value;

    }
    protected void extddlInsuranceCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void extddlAdjuster_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void GetPatientDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dspatientinfo = new DataSet();
        try
        {
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            dspatientinfo = GetPatienView(txtcaseid.Text, txtCompanyID.Text);
            if (dspatientinfo.Tables[0].Rows.Count > 0)
            {
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString() != "&nbsp;")
                {
                    txtPatientFName.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString() != "&nbsp;")
                {
                    txtPatientLName.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString() != "&nbsp;")
                {
                    txtMI.Text = dspatientinfo.Tables[0].Rows[0]["MI"].ToString();
                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString() != "&nbsp;")
                {
                    txtPatientAddress.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString() != "&nbsp;")
                {
                    txtPatientCity.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
                }
                if (dspatientinfo.Tables[0].Rows[0]["DOB"].ToString() != "&nbsp;")
                {
                    txtDateOfBirth.Text = dspatientinfo.Tables[0].Rows[0]["DOB"].ToString();
                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_GENDER"].ToString() != "&nbsp;")
                {
                    ddlSex.SelectedValue = dspatientinfo.Tables[0].Rows[0]["SZ_GENDER"].ToString();
                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString() != "&nbsp;")
                {
                    txtPatientCity.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString() != "&nbsp;")
                {
                    extddlPatientState.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();

                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString() != "&nbsp;")
                {
                    txthomephone.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();

                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString() != "&nbsp;")
                {
                    txtPatientZip.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();

                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_EMAIL"].ToString() != "&nbsp;")
                {
                    txtPatientEmail.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_EMAIL"].ToString();

                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_WORK_PHONE_EXTENSION"].ToString() != "&nbsp;")
                {
                    txtExtension.Text = dspatientinfo.Tables[0].Rows[0]["SZ_WORK_PHONE_EXTENSION"].ToString();

                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != "&nbsp;")
                {
                    extddlAttorney.Text = dspatientinfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString();

                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != "&nbsp;")
                {
                    extddlCaseType.Text = dspatientinfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString();

                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != "&nbsp;")
                {
                    extddlCaseStatus.Text = dspatientinfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString();

                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString() != "&nbsp;")
                {
                    txtSocialSecurityNumber.Text = dspatientinfo.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();

                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != "&nbsp;")
                {
                    txtPolicyHolder.Text = dspatientinfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString();

                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;" && dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")
                {
                    extddlInsuranceCompany.Text = dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString(); // Not in use
                    hdninsurancecode.Value = dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                    txtInsuranceCompany.Text = dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();

                    if (extddlInsuranceCompany.Text != "NA" && extddlInsuranceCompany.Text != "") //Not use
                        if (txtInsuranceCompany.Text != "" && txtInsuranceCompany.Text != "No suggestions found for your search")

                            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(hdninsurancecode.Value).Tables[0];
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    lstInsuranceCompanyAddress.DataBind();
                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "&nbsp;" && dspatientinfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "" && dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;" && dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")
                {
                    try
                    {
                        lstInsuranceCompanyAddress.SelectedValue = dspatientinfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString();
                        txtInsuranceAddress.Text = dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                        txtInsuranceCity.Text = dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                        txtInsuranceState.Text = dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                        txtInsuranceZip.Text = dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                        txtInsFax.Text = dspatientinfo.Tables[0].Rows[0]["sz_fax_number"].ToString();
                        txtInsPhone.Text = dspatientinfo.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                        txtInsContactPerson.Text = dspatientinfo.Tables[0].Rows[0]["sz_contact_person"].ToString();
                    }
                    catch
                    {
                    }
                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "&nbsp;" && dspatientinfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "")
                {
                    txtClaimNumber.Text = dspatientinfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                    if (txtClaimNumber.Text.Equals("NA"))
                    {
                        txtClaimNumber.Text = "";
                    }
                }
                if (dspatientinfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "&nbsp;" && dspatientinfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "")
                {
                    txtPolicyNumber.Text = dspatientinfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                    if (txtPolicyNumber.Text.Equals("NA"))
                    {
                        txtPolicyNumber.Text = "";
                    }
                }
                if (dspatientinfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "01/01/1900")
                {
                    txtATAccidentDate.Text = dspatientinfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
                }
                else
                {
                    txtATAccidentDate.Text = "";
                }
                txtATPlateNumber.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();
                txtATAddress.Text = dspatientinfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
                txtATCity.Text = dspatientinfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
                txtATReportNumber.Text = dspatientinfo.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString();
                txtATAdditionalPatients.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString();
                extddlATAccidentState.Text = dspatientinfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE_ID"].ToString();
                txtATHospitalName.Text = dspatientinfo.Tables[0].Rows[0]["SZ_HOSPITAL_NAME"].ToString();
                txtATHospitalAddress.Text = dspatientinfo.Tables[0].Rows[0]["SZ_HOSPITAL_ADDRESS"].ToString();
                txtATDescribeInjury.Text = dspatientinfo.Tables[0].Rows[0]["SZ_DESCRIBE_INJURY"].ToString();
                txtATAdmissionDate.Text = dspatientinfo.Tables[0].Rows[0]["DT_ADMISSION_DATE"].ToString();
                if (dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString() != "&nbsp;")
                {
                    string sz_patienttype = dspatientinfo.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString();
                    foreach (ListItem li in rdolstPatientType.Items)
                    {
                        if (li.Value.ToString() == sz_patienttype)
                        {
                            li.Selected = true;
                            break;
                        }
                    }
                }
                txtEmployerName.Text = dspatientinfo.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
                txtEmployerPhone.Text = dspatientinfo.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString();
                txtEmployerAddress.Text = dspatientinfo.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString();
                txtEmployerCity.Text = dspatientinfo.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString();
                extddlEmployerState.Text = dspatientinfo.Tables[0].Rows[0]["SZ_EMPLOYER_STATE_ID"].ToString();
                txtEmployerZip.Text = dspatientinfo.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();
                if (dspatientinfo.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "01/01/1900" && dspatientinfo.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "&nbsp;")
                {
                    txtDateofFirstTreatment.Text = dspatientinfo.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString();
                }
                else
                {
                    txtDateofFirstTreatment.Text = "";
                }
                txtChartNo.Text = dspatientinfo.Tables[0].Rows[0]["SZ_CHART_NO"].ToString();
                if (dspatientinfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != "&nbsp;" && dspatientinfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != "")
                {
                    extddlAdjuster.Text = dspatientinfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString();
                }
                txtAdjusterPhone.Text = dspatientinfo.Tables[0].Rows[0]["SZ_PHONE"].ToString();
                txtAdjusterExtension.Text = dspatientinfo.Tables[0].Rows[0]["SZ_EXTENSION"].ToString();
                txtfax.Text = dspatientinfo.Tables[0].Rows[0]["SZ_FAX"].ToString();
                txtEmail.Text = dspatientinfo.Tables[0].Rows[0]["SZ_EMAIL"].ToString();
            }



        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
}
