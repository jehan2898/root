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

public partial class Bill_Sys_PatientInformationOT_PT : PageBase
{
    private OTPT _objOTPT;
    private OTPT_PATIENT_DAO _OTPT_PATIENT_DAO;
    private EditOperation _editOperation;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;
    private Bill_Sys_OCT_Bills obj_OCT_Bills = new Bill_Sys_OCT_Bills();
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        //btnSaveAndGoToNext.Attributes.Add("onclick", "return ConfirmUpdate();");
        try
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (Request.QueryString["billnumber"] != null)
            {
                Session["BILL_NO"] = Request.QueryString["billnumber"];
            }
            if (Request.QueryString["caseid"] != null)
            {
                Session["CASE_ID"] = Request.QueryString["caseid"];
            }
            txtBillNumber.Text = Session["BILL_NO"].ToString();// "sas0000001";           
            txtCaseID.Text = Session["CASE_ID"].ToString();
            if (!IsPostBack)
            {
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                txtPatientID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNumber.Text, "SP_MST_PATIENT_INFORMATION", "GETPATIENTID");
                BindTimeControl();
                LoadData();
               
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
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_PatientInformationC4_2.aspx");
        }
        #endregion
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
                    //ddlEndHours.Items.Add(i.ToString());
                }
                else
                {
                    ddlHours.Items.Add("0" + i.ToString());
                    //ddlEndHours.Items.Add("0" + i.ToString());
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9)
                {
                    ddlMinutes.Items.Add(i.ToString());
                    //ddlEndMinutes.Items.Add(i.ToString());
                }
                else
                {
                    ddlMinutes.Items.Add("0" + i.ToString());
                    //ddlEndMinutes.Items.Add("0" + i.ToString());
                }
            }
            ddlTime.Items.Add("AM");
            ddlTime.Items.Add("PM");

            //ddlEndTime.Items.Add("AM");
            //ddlEndTime.Items.Add("PM");
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
    private void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds_patient_info = new DataSet();
        DataSet ds_get_patient_info = new DataSet();
        _objOTPT = new OTPT();
        _editOperation = new EditOperation();
        try
        {
            
            string sz_Bill_No = txtBillNumber.Text;
            string sz_Case_Id = txtCaseID.Text;
            ds_patient_info = _objOTPT.GET_Patient_Info_OTPT(sz_Case_Id);
            ds_get_patient_info = _objOTPT.GET_Patient_Information_OTPT(sz_Case_Id, txtCompanyID.Text);
            if (ds_patient_info.Tables.Count > 0)
            {
                if (ds_patient_info.Tables[0].Rows.Count > 0)
                {
                    if (ds_patient_info.Tables[0].Rows[0].ToString() != "" && ds_patient_info.Tables[0].Rows[0].ToString() != null)
                    {
                        txtPatientFirstName.Text = ds_patient_info.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                        txtPatientMiddleName.Text = ds_patient_info.Tables[0].Rows[0]["MI"].ToString();
                        txtPatientLastName.Text = ds_patient_info.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                        txtPatientStreet.Text = ds_patient_info.Tables[0].Rows[0]["SZ_PATIENT_STREET"].ToString();
                        txtPatientCity.Text = ds_patient_info.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
                        extddlPatientState.Text = ds_patient_info.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
                        txtPatientZip.Text = ds_patient_info.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
                        txtPatientPhone.Text = ds_patient_info.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
                        txtDateOfInjury.Text = ds_patient_info.Tables[0].Rows[0]["DT_INJURY"].ToString();
                        txtPatientDOB.Text = ds_patient_info.Tables[0].Rows[0]["DT_DOB"].ToString();
                        txtSSN.Text = ds_patient_info.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                        txtEmployer.Text = ds_patient_info.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
                        txtWCBCaseNumber.Text = ds_patient_info.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
                        txtCaseCarrierNo.Text = ds_patient_info.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                        txtInsuranceCarr.Text = ds_patient_info.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                        if (txtDateOfInjury.Text.Trim() != "")
                        {
                            DateTime dt = new DateTime();
                            dt = Convert.ToDateTime(txtDateOfInjury.Text);
                            txtDateOfInjury.Text = dt.ToString("MM/dd/yyyy");

                        }
                        if (txtPatientDOB.Text != "")
                        {
                            DateTime dt = new DateTime();
                            dt = Convert.ToDateTime(txtPatientDOB.Text);
                            txtPatientDOB.Text = dt.ToString("MM/dd/yyyy");

                        }

                    }
                }
            }
            if (ds_get_patient_info.Tables.Count > 0)
            {
                if (ds_get_patient_info.Tables[0].Rows.Count > 0)
                {
                    if (ds_get_patient_info.Tables[0].Rows[0].ToString() != "" && ds_get_patient_info.Tables[0].Rows[0].ToString() != null)
                    {
                        txtaddcitytown.Text = ds_get_patient_info.Tables[0].Rows[0]["SZ_PATIENT_INJURY_PLACE"].ToString();
                        if (ds_get_patient_info.Tables[0].Rows[0]["SZ_TREATEMNET_UNDER"].ToString().Trim() == "0")
                        {
                            rdlstTaxType.SelectedIndex = 0;
                        }
                        else if (ds_get_patient_info.Tables[0].Rows[0]["SZ_TREATEMNET_UNDER"].ToString().Trim() == "1")
                        {
                            rdlstTaxType.SelectedIndex = 1;
                        }
                        else if (ds_get_patient_info.Tables[0].Rows[0]["SZ_TREATEMNET_UNDER"].ToString().Trim() == "2")
                        {
                            rdlstTaxType.SelectedIndex = 2;
                        }
                        string sztimeinjury = ds_get_patient_info.Tables[0].Rows[0]["SZ_TIME_OF_INJURY"].ToString();
                        //string sztreatmentunder = ds_get_patient_info.Tables[0].Rows[0]["SZ_TREATEMNET_UNDER"].ToString();
                        //if (sztreatmentunder == "0")
                        //{
                        //    rdlstTaxType.SelectedIndex = 0;
                        //}
                        //else if (sztreatmentunder == "1")
                        //{
                        //    rdlstTaxType.SelectedIndex = 1;
                        //}
                        txtHistoryInjuryDate.Text = ds_get_patient_info.Tables[0].Rows[0]["DT_DATE_OF_PRV_HISTROY"].ToString();
                        string ddltime = ds_get_patient_info.Tables[0].Rows[0]["SZ_TIME_TYPE"].ToString();
                        string strtimehr = sztimeinjury.Substring(0, 2);
                        string strtimemin = sztimeinjury.Substring(3, 2);
                        
                        if (ddltime.Trim() == "AM")
                        {
                            ddlTime.SelectedIndex = 0;
                        }
                        else if (ddltime.Trim() == "PM")
                        {
                            ddlTime.SelectedIndex = 1;
                        }
                        int iHH = Convert.ToInt32(strtimehr.Trim());
                        ddlHours.SelectedIndex = iHH;
                        int iMM = Convert.ToInt32(strtimemin.Trim());
                        ddlMinutes.SelectedIndex = iMM;
                        txtReffering.Text = ds_get_patient_info.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString();
                        txtrefferingaddress.Text = ds_get_patient_info.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString();
                        txtrefferingtelno.Text = ds_get_patient_info.Tables[0].Rows[0]["SZ_REFFERING_TELEPHONE_NO"].ToString();
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
    protected void btnSaveAndGoToNext_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
            {
                saveData();
                Response.Redirect("Bill_Sys_Report_OT-PT.aspx", false);
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
    public void saveData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _OTPT_PATIENT_DAO = new OTPT_PATIENT_DAO();
        _objOTPT = new OTPT();
        try
        {
            _OTPT_PATIENT_DAO.SZ_PATIENT_ID = txtPatientID.Text;
            _OTPT_PATIENT_DAO.SZ_CASE_ID = txtCaseID.Text;
            _OTPT_PATIENT_DAO.SZ_BILL_NO = txtBillNumber.Text;
            _OTPT_PATIENT_DAO.SZ_PATIENT_INJURY_PLACE = txtaddcitytown.Text;
            string sztimeofenjury =ddlHours.Text+":"+ddlMinutes.Text;
            string sztimetype = ddlTime.Text;
            _OTPT_PATIENT_DAO.SZ_TIME_OF_INJURY = sztimeofenjury;
            _OTPT_PATIENT_DAO.SZ_TIME_TYPE = sztimetype;
            _OTPT_PATIENT_DAO.SZ_TREATEMNET_UNDER = rdlstTaxType.SelectedValue;
            //if (rdlstTaxType.SelectedValue == "0")
            //{
            //    _OTPT_PATIENT_DAO.SZ_TREATEMNET_UNDER = rdlstTaxType.SelectedItem.Value;
            //}
            //else if (rdlstTaxType.SelectedValue == "1")
            //{
            //    _OTPT_PATIENT_DAO.SZ_TREATEMNET_UNDER = rdlstTaxType.SelectedItem.Value;
            //}
            //else if (rdlstTaxType.SelectedValue == "2")
            //{
            //    _OTPT_PATIENT_DAO.SZ_TREATEMNET_UNDER = rdlstTaxType.SelectedItem.Value;
            //}

            _OTPT_PATIENT_DAO.DT_DATE_OF_PRV_HISTROY = txtHistoryInjuryDate.Text;
            _OTPT_PATIENT_DAO.SZ_COMPANY_ID = txtCompanyID.Text;
            _OTPT_PATIENT_DAO.SZ_REFFERING_PHYSICIAN = txtReffering.Text;
            _OTPT_PATIENT_DAO.SZ_REFFERING_PHYSICIAN_ADDRESS = txtrefferingaddress.Text;
            _OTPT_PATIENT_DAO.SZ_REFFERING_TELEPHONE_NO = txtrefferingtelno.Text;
            _objOTPT.Save_Patient_Info(_OTPT_PATIENT_DAO);

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
