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

public partial class AJAX_Pages_Bill_Sys_Nf2_Config_Setting : PageBase
{
    private Bill_Sys_Nf2_Config _Bill_Sys_Nf2_Config;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    public Bill_Sys_Nf2_Config.NF2_CONFIG_DAO _NF2_CONFIG_DAO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            ajAutoIns.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            if (!IsPostBack)
            {
                GetPatientDetails();
                txtPatientID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlInsuranceCompany.Flag_ID = txtCompanyID.Text;
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
    //protected void btnsave_Click(object sender, EventArgs e)
    //{

    //}
    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{

    //}

    protected void txtInsuranceCompany_TextChanged(object sender, EventArgs args)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strInsuranceID = hdninsurancecode.Value;

        if (txtInsuranceCompany.Text != "")
        {
            if (strInsuranceID != "0")
            {
                try
                {
                    ClearInsurancecontrol();
                    Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(strInsuranceID);
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    lstInsuranceCompanyAddress.DataBind();
                    Page.MaintainScrollPositionOnPostBack = true;


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
            else
            {
                lstInsuranceCompanyAddress.Items.Clear();
                hdninsurancecode.Value = "";
            }
        }
        else
        {
            lstInsuranceCompanyAddress.Items.Clear();
            txtInsuranceAddress.Text = "";
            txtInsuranceCity.Text = "";
            txtInsuranceState.Text = "";
            txtInsuranceZip.Text = "";
            txtInsPhone.Text = "";
            txtInsFax.Text = "";
            hdninsurancecode.Value = "";

            Page.MaintainScrollPositionOnPostBack = true;
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
            ClearInsurancecontrol();
            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            lstInsuranceCompanyAddress.DataBind();
            Page.MaintainScrollPositionOnPostBack = true;
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
    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
            txtInsuranceAddress.Text = _arraylist[2].ToString();
            txtInsuranceCity.Text = _arraylist[3].ToString();
            txtInsuranceState.Text = _arraylist[4].ToString();
            txtInsuranceZip.Text = _arraylist[5].ToString();
            txtInsFax.Text = _arraylist[9].ToString();
            txtInsPhone.Text = _arraylist[10].ToString();
            Page.MaintainScrollPositionOnPostBack = true;

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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtInsuranceAddress.Text = "";
            txtInsuranceCity.Text = "";
            txtInsuranceState.Text = "";
            txtInsuranceZip.Text = "";
            txtInsuranceZip.Text = "";
            txtInsFax.Text = "";

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
    private void GetPatientDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _Bill_Sys_Nf2_Config = new Bill_Sys_Nf2_Config();
            DataSet _patientDs = _Bill_Sys_Nf2_Config.GetNf2Info(txtCaseID.Text);
            if (_patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")
            {
                extddlInsuranceCompany.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString(); // Not in use
                hdninsurancecode.Value = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                txtInsuranceCompany.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();

                if (extddlInsuranceCompany.Text != "NA" && extddlInsuranceCompany.Text != "") //Not use
                    if (txtInsuranceCompany.Text != "" && txtInsuranceCompany.Text != "No suggestions found for your search")
                        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(hdninsurancecode.Value).Tables[0];
                lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                lstInsuranceCompanyAddress.DataValueField = "CODE";
                lstInsuranceCompanyAddress.DataBind();

            }
            if (_patientDs.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "" && _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")
            {
                try
                {
                    lstInsuranceCompanyAddress.SelectedValue = _patientDs.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString();
                    txtInsuranceAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                    txtInsuranceCity.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                    txtInsuranceState.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                    txtInsuranceZip.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                    txtInsFax.Text = _patientDs.Tables[0].Rows[0]["sz_fax_number"].ToString();
                    txtInsPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                }
                catch
                {
                }
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != "")
            {
                txtPolicyHolder.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString();
                if (txtPolicyHolder.Text.Equals("NA"))
                {
                    txtPolicyHolder.Text = "";
                }
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "")
            {
                txtClaimNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                if (txtClaimNumber.Text.Equals("NA"))
                {
                    txtClaimNumber.Text = "";
                }
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "")
            {
                txtPolicyNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                if (txtPolicyNumber.Text.Equals("NA"))
                {
                    txtPolicyNumber.Text = "";
                }
            }
            if (_patientDs.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString() != "01/01/1900")
            {
                txtdateofaccident.Text = _patientDs.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString();
            }
            else
            {
                txtdateofaccident.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;")
            {
                txtPatientFName.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
            }
            txtPatientLName.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
            txtMI.Text = _patientDs.Tables[0].Rows[0]["SZ_MI"].ToString();
            txtPatientAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ADDR"].ToString();
            txtPatientCity.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
            txtPatientZip.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
            txtState.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
            txtpatientphone.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
            txtWorkPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString();
            extddlPatientState.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();
            txtATAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
            txtATCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
            extddlATAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE_ID"].ToString();
            txtATHospitalName.Text = _patientDs.Tables[0].Rows[0]["SZ_HOSPITAL_NAME"].ToString();
            txtATHospitalAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_HOSPITAL_ADDRESS"].ToString();
            txtATAdmissionDate.Text = _patientDs.Tables[0].Rows[0]["DT_ADMISSION_DATE"].ToString();
            ddlSex.SelectedValue = _patientDs.Tables[0].Rows[0]["SZ_GENDER"].ToString();
            txtATDescribeInjury.Text = _patientDs.Tables[0].Rows[0]["SZ_DESCRIBE_INJURY"].ToString();
            txtSocialSecurityNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
            if (_patientDs.Tables[0].Rows[0]["POLICY_HOLDER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["POLICY_HOLDER"].ToString() != "")
            {
                txtPolicyHolder.Text = _patientDs.Tables[0].Rows[0]["POLICY_HOLDER"].ToString();
                if (txtPolicyHolder.Text.Equals("NA"))
                {
                    txtPolicyHolder.Text = "";
                }
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "")
            {
                txtPolicyNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                if (txtPolicyNumber.Text.Equals("NA"))
                {
                    txtPolicyNumber.Text = "";
                }
            }
            if (_patientDs.Tables[0].Rows[0]["DT_DOB"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_DOB"].ToString() != "01/01/1900")
            {
                txtDateOfBirth.Text = _patientDs.Tables[0].Rows[0]["DT_DOB"].ToString();

            }
            else
            {
                txtDateOfBirth.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["BT_BUS"].ToString() == "1")
            {
                chkbusschool.Checked = true;
            }
            else
            {
                chkbusschool.Checked = false;
            }
            if (_patientDs.Tables[0].Rows[0]["BT_TRUCK"].ToString() == "1")
            {
                chktruck.Checked = true;
            }
            else
            {
                chktruck.Checked = false;
            }
            if (_patientDs.Tables[0].Rows[0]["BT_MOTORCYCLE"].ToString() == "1")
            {
                chkmotorcycle.Checked = true;
            }
            else
            {
                chkmotorcycle.Checked = false;
            }
            if (_patientDs.Tables[0].Rows[0]["BT_AUTOMOBILE"].ToString() == "1")
            {
                chkautomobile.Checked = true;
            }
            else
            {
                chkautomobile.Checked = false;
            }


            if (_patientDs.Tables[0].Rows[0]["BT_YES_DRIVERMOTORVEHICLE"].ToString() == "1")
            {
                chkyesdrivermotor.Checked = true;
            }
            else
            {
                chkyesdrivermotor.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_NO_DRIVERMOTORVEHICLE"].ToString() == "1")
            {
                chknodrivermotor.Checked = true;
            }
            else
            {
                chknodrivermotor.Checked = false;
            }
            if (_patientDs.Tables[0].Rows[0]["BT_YES_PASSENGERMOTORVEHICLE"].ToString() == "1")
            {
                chkyespassengermotor.Checked = true;
            }
            else
            {
                chkyespassengermotor.Checked = false;
            }
            if (_patientDs.Tables[0].Rows[0]["BT_NO_PASSENGERMOTORVEHICLE"].ToString() == "1")
            {
                chknopassengermotor.Checked = true;
            }
            else
            {
                chknopassengermotor.Checked = false;
            }
            if (_patientDs.Tables[0].Rows[0]["BT_YES_PEDESTRIAN"].ToString() == "1")
            {
                chkyespedestrian.Checked = true;
            }
            else
            {
                chkyespedestrian.Checked = false;
            }
            if (_patientDs.Tables[0].Rows[0]["BT_NO_PEDESTRIAN"].ToString() == "1")
            {
                chknopedestrian.Checked = true;
            }
            else
            {
                chknopedestrian.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_YES_POLICYHOLDERHOUSEHOLD"].ToString() == "1")
            {
                chkyespolicyholder.Checked = true;
            }
            else
            {
                chkyespolicyholder.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_NO_POLICYHOLDERHOUSEHOLD"].ToString() == "1")
            {
                chknopolicyholder.Checked = true;
            }
            else
            {
                chknopolicyholder.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_YES_RELATIVERESIDEOWN"].ToString() == "1")
            {
                chkyesrelative.Checked = true;
            }
            else
            {
                chkyesrelative.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_NO_RELATIVERESIDEOWN"].ToString() == "1")
            {
                chknorelative.Checked = true;
            }
            else
            {
                chknorelative.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_YES_FURNISHINGHEALTHSERVICE"].ToString() == "1")
            {
                chkyesfurshing.Checked = true;
            }
            else
            {
                chkyesfurshing.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_NO_FURNISHINGHEALTHSERVICE"].ToString() == "1")
            {
                chknofurshing.Checked = true;
            }
            else
            {
                chknofurshing.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_YES_MOREHEALTHTREATMENT"].ToString() == "1")
            {
                chkyeshealth.Checked = true;
            }
            else
            {
                chkyeshealth.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_NO_MOREHEALTHTREATMENT"].ToString() == "1")
            {
                chknohealth.Checked = true;
            }
            else
            {
                chknohealth.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_YES_COURSEOFEMPLOYMENT"].ToString() == "1")
            {
                chkyescourseofemployment.Checked = true;
            }
            else
            {
                chkyescourseofemployment.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_NO_COURSEOFEMPLOYMENT"].ToString() == "1")
            {
                chknocourseofemployment.Checked = true;
            }
            else
            {
                chknocourseofemployment.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_YES_RECIEVING_UNEMPLOYMENT"].ToString() == "1")
            {
                chkyesaccident.Checked = true;
            }
            else
            {
                chkyesaccident.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_NO_RECIEVING_UNEMPLOYMENT"].ToString() == "1")
            {
                chknoaccident.Checked = true;
            }
            else
            {
                chknoaccident.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_YES_OTHER_EXP"].ToString() == "1")
            {
                chkyesexpenses.Checked = true;
            }
            else
            {
                chkyesexpenses.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_NO_OTHER_EXP"].ToString() == "1")
            {
                chknoexpenses.Checked = true;
            }
            else
            {
                chknoexpenses.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_YES_STATE_DISABILITY"].ToString() == "1")
            {
                chkyesnewyork.Checked = true;
            }
            else
            {
                chkyesnewyork.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_NO_STATE_DISABILITY"].ToString() == "1")
            {
                chknonewyork.Checked = true;
            }
            else
            {
                chknonewyork.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_YES_WORKERS_COMP"].ToString() == "1")
            {
                chkyeswc.Checked = true;
            }
            else
            {
                chkyeswc.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_NO_WORKERS_COMP"].ToString() == "1")
            {
                chknowc.Checked = true;
            }
            else
            {
                chknowc.Checked = false;
            }
            if (_patientDs.Tables[0].Rows[0]["BT_OUT_PATIENT"].ToString() == "1")
            {
                chkoutpatient.Checked = true;
            }
            else
            {
                chkoutpatient.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["BT_IN_PATIENT"].ToString() == "1")
            {
                chkinpatient.Checked = true;
            }
            else
            {
                chkinpatient.Checked = false;
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_MAKE"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_MAKE"].ToString() != "")
            {
                txtmake.Text = _patientDs.Tables[0].Rows[0]["SZ_MAKE"].ToString();

            }
            else
            {
                txtmake.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_YEAR"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_YEAR"].ToString() != "")
            {
                txtyear.Text = _patientDs.Tables[0].Rows[0]["SZ_YEAR"].ToString();

            }
            else
            {
                txtyear.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_PATIENT_POLICY_NAME"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_PATIENT_POLICY_NAME"].ToString() != "")
            {
                txtpatientpolicyname.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_POLICY_NAME"].ToString();

            }
            else
            {
                txtpatientpolicyname.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_NAME"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_NAME"].ToString() != "")
            {
                txtInsurername.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_NAME"].ToString();

            }
            else
            {
                txtInsurername.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_ADDRESS"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_ADDRESS"].ToString() != "")
            {
                txtInsureraddress.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_ADDRESS"].ToString();

            }
            else
            {
                txtInsureraddress.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_CITY"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_CITY"].ToString() != "")
            {
                txtinsurercity.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_CITY"].ToString();

            }
            else
            {
                txtinsurercity.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_STATE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_STATE_ID"].ToString() != "")
            {
                extddlinsurerstate.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_STATE_ID"].ToString();

            }
            else
            {
                extddlinsurerstate.Text = "NA";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_ZIP"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_ZIP"].ToString() != "")
            {
                txtinsurerzip.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_ZIP"].ToString();

            }
            else
            {
                txtinsurerzip.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_PHONE_NO"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_PHONE_NO"].ToString() != "")
            {
                txtinsurerphone.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURER_CLAIM_PHONE_NO"].ToString();

            }
            else
            {
                txtinsurerphone.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_HOME_PHONE"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_HOME_PHONE"].ToString() != "")
            {
                txtHomePhone.Text = _patientDs.Tables[0].Rows[0]["SZ_HOME_PHONE"].ToString();

            }
            else
            {
                txtHomePhone.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_BRIEF_DESCRIPTION"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_BRIEF_DESCRIPTION"].ToString() != "")
            {
                txtaccidentdesc.Text = _patientDs.Tables[0].Rows[0]["SZ_BRIEF_DESCRIPTION"].ToString();

            }
            else
            {
                txtaccidentdesc.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_AMOUNT_HEALTHBILL"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_AMOUNT_HEALTHBILL"].ToString() != "")
            {
                txthealthbills.Text = _patientDs.Tables[0].Rows[0]["SZ_AMOUNT_HEALTHBILL"].ToString();

            }
            else
            {
                txthealthbills.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_DID_YOU_LOOSETIME"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_DID_YOU_LOOSETIME"].ToString() != "")
            {
                txtlosetime.Text = _patientDs.Tables[0].Rows[0]["SZ_DID_YOU_LOOSETIME"].ToString();

            }
            else
            {
                txtlosetime.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["DT_ABSENCEFROM_WORK_BEGIN"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_ABSENCEFROM_WORK_BEGIN"].ToString() != "")
            {
                txtabsencedate.Text = _patientDs.Tables[0].Rows[0]["DT_ABSENCEFROM_WORK_BEGIN"].ToString();

            }
            else
            {
                txtabsencedate.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_RETURNED_TO_WORK"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_RETURNED_TO_WORK"].ToString() != "")
            {
                txtreturnwork.Text = _patientDs.Tables[0].Rows[0]["SZ_RETURNED_TO_WORK"].ToString();

            }
            else
            {
                txtreturnwork.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["DT_RETURN_TO_WORK"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_RETURN_TO_WORK"].ToString() != "")
            {
                txtreturneddate.Text = _patientDs.Tables[0].Rows[0]["DT_RETURN_TO_WORK"].ToString();

            }
            else
            {
                txtreturneddate.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_AMOUNT_OF_TIME"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_AMOUNT_OF_TIME"].ToString() != "")
            {
                txtlosttime.Text = _patientDs.Tables[0].Rows[0]["SZ_AMOUNT_OF_TIME"].ToString();

            }
            else
            {
                txtlosttime.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["FLT_GROSS_WEEKLY_EARNINGS"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["FLT_GROSS_WEEKLY_EARNINGS"].ToString() != "")
            {
                txtgrossaverage.Text = _patientDs.Tables[0].Rows[0]["FLT_GROSS_WEEKLY_EARNINGS"].ToString();

            }
            else
            {
                txtgrossaverage.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["I_NUMBEROFDAYS_WORK_WEEKLY"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["I_NUMBEROFDAYS_WORK_WEEKLY"].ToString() != "")
            {
                txtdaysperweek.Text = _patientDs.Tables[0].Rows[0]["I_NUMBEROFDAYS_WORK_WEEKLY"].ToString();

            }
            else
            {
                txtdaysperweek.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["I_NUMBEROFHOURS_WORK_WEEKLY"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["I_NUMBEROFHOURS_WORK_WEEKLY"].ToString() != "")
            {
                txthrsperday.Text = _patientDs.Tables[0].Rows[0]["I_NUMBEROFHOURS_WORK_WEEKLY"].ToString();

            }
            else
            {
                txthrsperday.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_NAME"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_NAME"].ToString() != "")
            {
                txtemployerfirstname.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_NAME"].ToString();

            }
            else
            {
                txtemployerfirstname.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_ADDRESS"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_ADDRESS"].ToString() != "")
            {
                txtemployerfirstaddress.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_ADDRESS"].ToString();

            }
            else
            {
                txtemployerfirstaddress.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_CITY"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_CITY"].ToString() != "")
            {
                txtemployerfirstcity.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_CITY"].ToString();

            }
            else
            {
                txtemployerfirstcity.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_STATE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_STATE_ID"].ToString() != "")
            {
                extddlemployerfirststate.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_STATE_ID"].ToString();

            }
            else
            {
                extddlemployerfirststate.Text = "";
            }
       
            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_OCCUPATION"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_OCCUPATION"].ToString() != "")
            {
                txtemployerfirstoccu.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERONE_OCCUPATION"].ToString();

            }
            else
            {
                txtemployerfirstoccu.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["DT_EMPLOYERONE_FROM"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_EMPLOYERONE_FROM"].ToString() != "")
            {
                txtemployerfirstfrmdate.Text = _patientDs.Tables[0].Rows[0]["DT_EMPLOYERONE_FROM"].ToString();

            }
            else
            {
                txtemployerfirstfrmdate.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["DT_EMPLOYERONE_TO"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_EMPLOYERONE_TO"].ToString() != "")
            {
                txtemployerfirsttodate.Text = _patientDs.Tables[0].Rows[0]["DT_EMPLOYERONE_TO"].ToString();

            }
            else
            {
                txtemployerfirsttodate.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_NAME"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_NAME"].ToString() != "")
            {
                txtemployertwoname.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_NAME"].ToString();

            }
            else
            {
                txtemployertwoname.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_ADDRESS"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_ADDRESS"].ToString() != "")
            {
                txtemployersecondadd.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_ADDRESS"].ToString();

            }
            else
            {
                txtemployersecondadd.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_CITY"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_CITY"].ToString() != "")
            {
                txtemployersecondcity.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_CITY"].ToString();

            }
            else
            {
                txtemployersecondcity.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_STATE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_STATE_ID"].ToString() != "")
            {
                extddlemployersecondstate.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_STATE_ID"].ToString();

            }
            else
            {
                extddlemployersecondstate.Text = "NA";
            }
          

            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_OCCUPATION"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_OCCUPATION"].ToString() != "")
            {
                txtemployersecondoccu.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTWO_OCCUPATION"].ToString();

            }
            else
            {
                txtemployersecondoccu.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["DT_EMPLOYERTWO_FROM"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_EMPLOYERTWO_FROM"].ToString() != "")
            {
                txtemployersecondfrmdate.Text = _patientDs.Tables[0].Rows[0]["DT_EMPLOYERTWO_FROM"].ToString();

            }
            else
            {
                txtemployersecondfrmdate.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["DT_EMPLOYERTWO_TO"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_EMPLOYERTWO_TO"].ToString() != "")
            {
                txtemployersecondtodate.Text = _patientDs.Tables[0].Rows[0]["DT_EMPLOYERTWO_TO"].ToString();

            }
            else
            {
                txtemployersecondtodate.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_NAME"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_NAME"].ToString() != "")
            {
                txtemployerthreename.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_NAME"].ToString();

            }
            else
            {
                txtemployerthreename.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_ADDRESS"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_ADDRESS"].ToString() != "")
            {
                txtemployerthreeaddress.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_ADDRESS"].ToString();

            }
            else
            {
                txtemployerthreeaddress.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_CITY"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_CITY"].ToString() != "")
            {
                txtemployerthreecity.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_CITY"].ToString();

            }
            else
            {
                txtemployerthreecity.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_STATE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_STATE_ID"].ToString() != "")
            {
                extddlemployerthreestate.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_STATE_ID"].ToString();

            }
            else
            {
                extddlemployerthreestate.Text = "NA";
            }
           
            if (_patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_OCCUPATION"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_OCCUPATION"].ToString() != "")
            {
                txtemployerthirdoccu.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYERTHREE_OCCUPATION"].ToString();

            }
            else
            {
                txtemployerthirdoccu.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["DT_EMPLOYERTHREE_FROM"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_EMPLOYERTHREE_FROM"].ToString() != "")
            {
                txtemployerthreefrmdate.Text = _patientDs.Tables[0].Rows[0]["DT_EMPLOYERTHREE_FROM"].ToString();

            }
            else
            {
                txtemployerthreefrmdate.Text = "";
            }

            if (_patientDs.Tables[0].Rows[0]["DT_EMPLOYERTHREE_TO"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_EMPLOYERTHREE_TO"].ToString() != "")
            {
                txtemployerthreetodate.Text = _patientDs.Tables[0].Rows[0]["DT_EMPLOYERTHREE_TO"].ToString();

            }
            else
            {
                txtemployerthreetodate.Text = "";
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            _NF2_CONFIG_DAO = new Bill_Sys_Nf2_Config.NF2_CONFIG_DAO();
            _NF2_CONFIG_DAO.SZ_COMPANY_ID = txtCompanyID.Text;
            _NF2_CONFIG_DAO.SZ_INSURANCE_ID = hdninsurancecode.Value;
            _NF2_CONFIG_DAO.SZ_INSURANCE_ADD_ID = lstInsuranceCompanyAddress.SelectedValue;
            _NF2_CONFIG_DAO.SZ_CASE_ID = txtCaseID.Text;
            _NF2_CONFIG_DAO.SZ_CLAIM_NO = txtClaimNumber.Text;
            _NF2_CONFIG_DAO.SZ_POLICY_NO = txtPolicyNumber.Text;
            _NF2_CONFIG_DAO.SZ_POLICYHOLDER = txtPolicyHolder.Text;
            _NF2_CONFIG_DAO.DT_DATEOF_ACCIDENT = txtdateofaccident.Text;
            _NF2_CONFIG_DAO.SZ_PATIENT_PHONE_NO = txtpatientphone.Text;
            _NF2_CONFIG_DAO.SZ_SOCIAL_SECURITY_NO = txtSocialSecurityNumber.Text;
            _NF2_CONFIG_DAO.SZ_GENDER = ddlSex.SelectedValue;
            _NF2_CONFIG_DAO.SZ_PATIENT_FIRST_NAME = txtPatientFName.Text;
            _NF2_CONFIG_DAO.SZ_PATIENT_LAST_NAME = txtPatientLName.Text;
            _NF2_CONFIG_DAO.MI = txtMI.Text;
            _NF2_CONFIG_DAO.DT_DOB = txtDateOfBirth.Text;
            _NF2_CONFIG_DAO.SZ_PATIENT_ADDRESS = txtPatientAddress.Text;
            _NF2_CONFIG_DAO.SZ_PATIENT_CITY = txtPatientCity.Text;
            _NF2_CONFIG_DAO.SZ_PATIENT_STATE = extddlPatientState.Text;
            _NF2_CONFIG_DAO.SZ_PATIENT_ZIP = txtPatientZip.Text;
            _NF2_CONFIG_DAO.SZ_ACCIDENT_ADDRESS = txtATAddress.Text;
            _NF2_CONFIG_DAO.SZ_ACCIDENT_CITY = txtATCity.Text;
            _NF2_CONFIG_DAO.SZ_ACCIDENT_STATE = extddlATAccidentState.Text;
            _NF2_CONFIG_DAO.SZ_HOME_PHONE = txtHomePhone.Text;
            _NF2_CONFIG_DAO.SZ_WORK_PHONE = txtWorkPhone.Text;
            _NF2_CONFIG_DAO.SZ_MAKE = txtmake.Text;
            _NF2_CONFIG_DAO.SZ_YEAR = txtyear.Text;
            _NF2_CONFIG_DAO.SZ_PATIENT_POLICY_NAME = txtpatientpolicyname.Text;
            _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_NAME = txtInsurername.Text;
            _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_ADDRESS = txtInsureraddress.Text;
            _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_CITY = txtinsurercity.Text;
            _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_STATE = extddlinsurerstate.Selected_Text;
            _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_STATE_ID = extddlinsurerstate.Text;
            _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_ZIP = txtinsurerzip.Text;
            _NF2_CONFIG_DAO.SZ_INSURER_CLAIM_PHONE_NO = txtinsurerphone.Text;
            _NF2_CONFIG_DAO.SZ_BRIEF_DESCRIPTION_OF_ACCIDENT = txtaccidentdesc.Text;
            _NF2_CONFIG_DAO.SZ_HOSPITAL_NAME = txtATHospitalName.Text;
            _NF2_CONFIG_DAO.SZ_HOSPITAL_ADDRESS = txtATHospitalAddress.Text;
            _NF2_CONFIG_DAO.DT_ADMISSION_DATE = txtATAdmissionDate.Text;
            if (chkbusschool.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_BUS = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_BUS = "0";
            }

            if (chktruck.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_TRUCK = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_TRUCK = "0";
            }
            if (chkautomobile.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_AUTOMOBILE = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_AUTOMOBILE = "0";
            }
            if (chkmotorcycle.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_MOTORCYCLE = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_MOTORCYCLE = "0";
            }
            if (chkyesdrivermotor.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_DRIVERMOTORVEHICLE = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_DRIVERMOTORVEHICLE = "0";
            }
            if (chknodrivermotor.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_DRIVERMOTORVEHICLE = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_DRIVERMOTORVEHICLE = "0";
            }
            if (chkyespassengermotor.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_PASSENGERMOTORVEHICLE = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_PASSENGERMOTORVEHICLE = "0";
            }

            if (chknopassengermotor.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_PASSENGERMOTORVEHICLE = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_PASSENGERMOTORVEHICLE = "0";
            }

            if (chkyespedestrian.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_PEDESTRIAN = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_PEDESTRIAN = "0";
            }
            if (chknopedestrian.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_PEDESTRIAN = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_PEDESTRIAN = "0";
            }

            if (chkyespolicyholder.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_POLICYHOLDERHOUSEHOLD = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_POLICYHOLDERHOUSEHOLD = "0";
            }
            if (chknopolicyholder.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_POLICYHOLDERHOUSEHOLD = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_POLICYHOLDERHOUSEHOLD = "0";
            }

            if (chkyesrelative.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_RELATIVERESIDEOWN = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_RELATIVERESIDEOWN = "0";
            }
            if (chknorelative.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_RELATIVERESIDEOWN = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_RELATIVERESIDEOWN = "0";
            }

            if (chkoutpatient.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_OUT_PATIENT = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_OUT_PATIENT = "0";
            }
            if (chkinpatient.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_IN_PATIENT = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_IN_PATIENT = "0";
            }

            if (chkoutpatient.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_OUT_PATIENT = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_OUT_PATIENT = "0";
            }
            if (chkinpatient.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_IN_PATIENT = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_IN_PATIENT = "0";
            }

            if (chkyesfurshing.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_FURNISHINGHEALTHSERVICE = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_FURNISHINGHEALTHSERVICE = "0";
            }
            if (chknofurshing.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_FURNISHINGHEALTHSERVICE = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_FURNISHINGHEALTHSERVICE = "0";
            }

            if (chkyeshealth.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_MOREHEALTHTREATMENT = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_MOREHEALTHTREATMENT = "0";
            }
            if (chknohealth.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_MOREHEALTHTREATMENT = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_MOREHEALTHTREATMENT = "0";
            }

            if (chkyescourseofemployment.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_COURSEOFEMPLOYMENT = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_COURSEOFEMPLOYMENT = "0";
            }
            if (chknocourseofemployment.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_COURSEOFEMPLOYMENT = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_COURSEOFEMPLOYMENT = "0";
            }

            if (chkyesaccident.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_RECIEVING_UNEMPLOYMENT = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_RECIEVING_UNEMPLOYMENT = "0";
            }
            if (chknoaccident.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_RECIEVING_UNEMPLOYMENT = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_RECIEVING_UNEMPLOYMENT = "0";
            }

            if (chkyesexpenses.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_OTHER_EXP = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_OTHER_EXP = "0";
            }
            if (chknoexpenses.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_OTHER_EXP = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_OTHER_EXP = "0";
            }

            if (chkyesnewyork.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_STATE_DISABILITY = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_STATE_DISABILITY = "0";
            }
            if (chknonewyork.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_STATE_DISABILITY = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_STATE_DISABILITY = "0";
            }

            if (chkyeswc.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_YES_WORKERS_COMP = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_YES_WORKERS_COMP = "0";
            }
            if (chknowc.Checked == true)
            {
                _NF2_CONFIG_DAO.BT_NO_WORKERS_COMP = "1";
            }
            else
            {
                _NF2_CONFIG_DAO.BT_NO_WORKERS_COMP = "0";
            }
            _NF2_CONFIG_DAO.SZ_AMOUNT_HEALTHBILL = txthealthbills.Text;
            _NF2_CONFIG_DAO.SZ_DID_YOU_LOOSETIME = txtlosetime.Text;
            _NF2_CONFIG_DAO.DT_ABSENCEFROM_WORK_BEGIN = txtabsencedate.Text;
            _NF2_CONFIG_DAO.SZ_RETURNED_TO_WORK = txtreturnwork.Text;
            _NF2_CONFIG_DAO.DT_RETURN_TO_WORK = txtreturneddate.Text;
            _NF2_CONFIG_DAO.SZ_AMOUNT_OF_TIME = txtlosttime.Text;
            _NF2_CONFIG_DAO.FLT_GROSS_WEEKLY_EARNINGS = txtgrossaverage.Text;
            _NF2_CONFIG_DAO.I_NUMBEROFDAYS_WORK_WEEKLY = txtdaysperweek.Text;
            _NF2_CONFIG_DAO.I_NUMBEROFHOURS_WORK_WEEKLY = txthrsperday.Text;
          
            _NF2_CONFIG_DAO.SZ_EMPLOYERONE_NAME = txtemployerfirstname.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERONE_ADDRESS = txtemployerfirstaddress.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERONE_CITY = txtemployerfirstcity.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERONE_STATE = extddlemployerfirststate.Selected_Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERONE_STATE_ID = extddlemployerfirststate.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERONE_OCCUPATION = txtemployerfirstoccu.Text;
            _NF2_CONFIG_DAO.DT_EMPLOYERONE_FROM = txtemployerfirstfrmdate.Text;
            _NF2_CONFIG_DAO.DT_EMPLOYERONE_TO = txtemployerfirsttodate.Text;


            _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_NAME = txtemployertwoname.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_ADDRESS = txtemployersecondadd.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_CITY = txtemployersecondcity.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_STATE = extddlemployersecondstate.Selected_Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_STATE_ID = extddlemployersecondstate.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERTWO_OCCUPATION = txtemployersecondoccu.Text;
            _NF2_CONFIG_DAO.DT_EMPLOYERTWO_FROM = txtemployersecondfrmdate.Text;
            _NF2_CONFIG_DAO.DT_EMPLOYERTWO_TO = txtemployersecondtodate.Text;

            _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_NAME = txtemployerthreename.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_ADDRESS = txtemployerthreeaddress.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_CITY = txtemployerthreecity.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_STATE = extddlemployerthreestate.Selected_Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_STATE_ID = extddlemployerthreestate.Text;
            _NF2_CONFIG_DAO.SZ_EMPLOYERTHREE_OCCUPATION = txtemployerthirdoccu.Text;
            _NF2_CONFIG_DAO.DT_EMPLOYERTHREE_FROM = txtemployerthreefrmdate.Text;
            _NF2_CONFIG_DAO.DT_EMPLOYERTHREE_TO = txtemployerthreetodate.Text;
            _NF2_CONFIG_DAO.SZ_HOSPITAL_NAME = txtATHospitalName.Text;
            _NF2_CONFIG_DAO.SZ_HOSPITAL_ADDRESS = txtATHospitalAddress.Text;
            _NF2_CONFIG_DAO.DT_ADMISSION_DATE = txtATAdmissionDate.Text;
            Bill_Sys_Nf2_Config _Bill_Sys_Nf2_Config = new Bill_Sys_Nf2_Config();
            _Bill_Sys_Nf2_Config.UpdateNf2INfo(_NF2_CONFIG_DAO);
            usrMessage.PutMessage("Update Successfully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
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
