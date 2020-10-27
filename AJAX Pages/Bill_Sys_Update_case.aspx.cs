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
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using NOTES_OBJECT;
using NotesComponent;
using Componend;


public partial class AJAX_Pages_Bill_Sys_Update_case : PageBase
{
    SqlConnection sqlCon;
    private ListOperation _listOperation;
    private Billing_Sys_ManageNotesBO _manageNotesBO;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private ArrayList _arrayList;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private NotesOperation _notesOperation;
    public string caseID = "";
    Patient_TVBO _patient_tvbo;
    private string associatecaseno = "";
    private string associtecasenoAllow = ""; // concanate allow case
    private string associatecasenoNotAllow = "";// concanate caseno for  different address
    private string associatecasenoUpdate = ""; //only update sourcepath but all destinati path case same need
    private string associatecasenoNull = ""; // concanate  all source and destination null
    Boolean updateFlag = false;
    Regex commonrange = new Regex("[^0-9)]");

    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.con.SourceGrid = grdAttorney;
            this.txtSearchBox.SourceGrid = grdAttorney;
            this.grdAttorney.Page = this.Page;
            this.grdAttorney.PageNumberList = this.con;

            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            lnkUpdateAtt.Attributes.Add("onclick", "return showAttorney();");
            btnAttorneyAssign.Attributes.Add("onclick", "return showAttorney();");
            
            ajAutoIns.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            Ajautoattorney.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            Session["CASE_LIST_GO_BUTTON"] = null;
            bool bt_referring_facility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;

            extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlCaseStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlAttorney.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlAdjuster.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlInsuranceCompany.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlLocation.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlAttorneyAssign.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {
                if (Request.QueryString["CaseID"] != null)
                {
                    if (Request.QueryString["CaseID"].ToString() != "")
                    {
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(Request.QueryString["CaseID"].ToString(), "");
                        _bill_Sys_CaseObject.SZ_CASE_ID = Request.QueryString["CaseID"].ToString();
                        _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        Session["Company"] = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                        _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(Request.QueryString["CaseID"].ToString(), Session["Company"].ToString());
                        _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                    }
                    LoadDataOnPage();
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

    protected void txtAttorneyCompany_TextChanged(object sender, EventArgs e)
    {

    }

    protected void extddlAttorney_selectedIndex(object sender, EventArgs e)
    {

    }

    protected void carTabPage_ActiveTabChanged(object sender, EventArgs e)
    {

    }

    protected void txtInsuranceCompany_TextChanged(object sender, EventArgs e)
    {//Logging Start
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
                    _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(hdninsurancecode.Value);
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    lstInsuranceCompanyAddress.DataBind();
                    Page.MaintainScrollPositionOnPostBack = true;
                    //tabcontainerPatientEntry.ActiveTabIndex = 2;
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
            txtInsuranceStreet.Text = "";
            txtInsPhone.Text = "";
            hdninsurancecode.Value = "";
            //Page.MaintainScrollPositionOnPostBack = true;
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void extddlInsuranceCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
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
            txtInsuranceStreet.Text = _arraylist[6].ToString();
            txtInsFax.Text = _arraylist[9].ToString();
            txtInsPhone.Text = _arraylist[10].ToString();
            txtInsContactPerson.Text = _arraylist[11].ToString();

            Page.MaintainScrollPositionOnPostBack = true;
            //tabcontainerPatientEntry.ActiveTabIndex = 2;
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

    protected void extddlAdjuster_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void lnkUpdateAdu_Click(object sender, EventArgs e)
    {

    }

    protected void btnAssociate_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnDAssociate_Click(object sender, EventArgs e)
    {

    }

    protected void lnkUpdateAtt_Click(object sender, EventArgs e)
    {

    }

    protected void btnAttorneyAssign_Click(object sender, EventArgs e)
    {

    }

    protected void btndeleteAtt_Click(object sender, EventArgs e)
    {

    }

    protected void lnkAddAttorney_Click(object sender, EventArgs e)
    {

    }

    protected void btnPatientUpdate_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            string strCaseType = _associateDiagnosisCodeBO.GetCaseTypeName(extddlCaseType.Text);
            if (strCaseType == "WC000000000000000001")//(chkAssociateCode.Checked)
            {
                txtAssociateDiagnosisCode.Text = "1";
            }
            else
            {
                txtAssociateDiagnosisCode.Text = "0";
            }
            Boolean chartflag = false;
            if (txtChartNo.Visible == true)
            {
                if (!txtChartNo.Text.Equals(""))
                {
                    chartflag = true;
                    _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    //Label lblViewChartNo = (Label)DtlView.Items[0].FindControl("lblViewChartNo");
                    //if (!lblViewChartNo.Text.Equals(txtChartNo.Text))
                    //{
                    //    string flag = "CHART";
                    //    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    //    {
                    //        flag = "REF";
                    //    }
                    //    if (!_bill_Sys_PatientBO.ExistChartNumber(txtCompanyID.Text, txtChartNo.Text, flag))
                    //    {
                    //        UpdatePatientInformation();

                    //    }
                    //    else
                    //    {
                    //        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert( '" + txtChartNo.Text + "' + ' Chart No Allready Exist ...!');</script>");
                    //        // Page.RegisterStartupScript("mm", "<script language='javascript'>alert('" + txtChartNo.Text + "' + ' chart no is already exist ...!');</script>");
                    //        usrMessage.PutMessage(txtChartNo.Text + "  chart no is already exist ...!");
                    //        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                    //        usrMessage.Show();
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                        chartflag = false;
                    //}
                }

            }
            if (!chartflag)
            {

                UpdatePatientInformation();
            }

            // UpdatePatientInformation();
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            ////_bill_Sys_PatientBO.UpdateTemplateStatus(Session["TM_SZ_CASE_ID"].ToString(), chkStatusProc.Checked == true ? 1 : 0, txtNF2Date.Text);
            // lblMsg.Visible = true;
            Page.MaintainScrollPositionOnPostBack = false;
            //CheckTemplateStatus(Session["TM_SZ_CASE_ID"].ToString());
            LoadDataOnPage();
            // lblMsg.Text = " Patient Information Updated successfully ! ";
            usrMessage.PutMessage("Patient Information Updated successfully !");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            Session["AttornyID"] = extddlAttorney.Text; // Girish
            //SaveNotes();
            //LoadNoteGrid();
            //Tushar:- Comented Because Created New User Controll
            // GetPatientDeskList();
            //End                

            //TUSHAR:- To Add User Controll To Display PatientDeskList
            ////UserPatientInfoControl.GetPatienDeskList(((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            //////End Of Code
            ////if (!btassociate.Checked)  //Prashant Assocoite case no update 
            ////{
            ////    if (!associatecaseno.Equals(""))
            ////    {
            ////        UpdateCopyToCase(associatecaseno);
            ////    }
            ////}

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

    }

    protected void ddlInsuranceType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void ClearInsurancecontrol()
    {//Logging Start
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
            txtInsuranceStreet.Text = "";

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
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet _patientDs = _bill_Sys_PatientBO.GetPatientInfo(txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            if (_patientDs.Tables[0].Rows.Count > 0)
            {
                DtlView.DataSource = _patientDs;
                DtlView.DataBind();

                if (_patientDs.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;")
                {
                    txtPatientFName.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                    //lblViewFirstName.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                }
                //if (_patientDs.Tables[0].Rows[0]["sz_source_company_name"].ToString() != "")
                //{
                //    lblwalkin.Text = _patientDs.Tables[0].Rows[0]["sz_source_company_name"].ToString();

                //}
                txtPatientLName.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                //lblViewLastName.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                txtPatientAge.Text = _patientDs.Tables[0].Rows[0]["I_PATIENT_AGE"].ToString();

                txtPatientAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                //lblViewPatientAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();


                txtPatientCity.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
                //lblViewPatientCity.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();

                txtPatientZip.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
                //lblViewPatientZip.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();

                txtPatientPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
                //lblViewHomePhone.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();

                txtPatientEmail.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_EMAIL"].ToString();
                //lblViewPatientEmail.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_EMAIL"].ToString();

                txtWorkPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString();
                //lblViewWorkPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString();

                txtExtension.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_PHONE_EXTENSION"].ToString();
                //lblViewPatientExt.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_PHONE_EXTENSION"].ToString();

                if (_patientDs.Tables[0].Rows[0]["BT_WRONG_PHONE"].ToString() == "True" && _patientDs.Tables[0].Rows[0]["BT_WRONG_PHONE"].ToString() != "")
                {
                    CheckBox chkViewWrongPhone = (CheckBox)DtlView.FindControl("chkViewWrongPhone");
                    chkWrongPhone.Checked = true;
                    chkViewWrongPhone.Checked = true;
                }
                else
                {
                    //CheckBox chkViewWrongPhone = (CheckBox)DtlView.FindControl("chkViewWrongPhone");
                    chkWrongPhone.Checked = false;
                    //chkViewWrongPhone.Checked = false;
                }

                txtMI.Text = _patientDs.Tables[0].Rows[0]["MI"].ToString();
                Label lblViewMiddleName = (Label)DtlView.Items[0].FindControl("lblViewMiddleName");
                //Label lblViewMiddleName = (Label)DtlView.FindControl("lblViewMiddleName");
                lblViewMiddleName.Text = _patientDs.Tables[0].Rows[0]["MI"].ToString();

                txtWCBNo.Text = _patientDs.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();


                txtSocialSecurityNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                //lblViewSSN.Text = _patientDs.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();

                if (_patientDs.Tables[0].Rows[0]["DT_DOB"].ToString() != "01/01/1900" && _patientDs.Tables[0].Rows[0]["DT_DOB"].ToString() != "&nbsp;")
                {
                    txtDateOfBirth.Text = _patientDs.Tables[0].Rows[0]["DT_DOB"].ToString();
                    //lblViewDateofbirth.Text = _patientDs.Tables[0].Rows[0]["DT_DOB"].ToString();
                }
                else
                {
                    txtDateOfBirth.Text = "";
                    //lblViewDateofbirth.Text = "";
                }
                Label lblViewGender = (Label)DtlView.Items[0].FindControl("lblViewGender");
                ddlSex.SelectedValue = _patientDs.Tables[0].Rows[0]["SZ_GENDER"].ToString();
                lblViewGender.Text = ddlSex.SelectedItem.Text;

                if (_patientDs.Tables[0].Rows[0]["DT_INJURY"].ToString() != "01/01/1900" && _patientDs.Tables[0].Rows[0]["DT_INJURY"].ToString() != "&nbsp;")
                {
                    txtDateOfInjury.Text = _patientDs.Tables[0].Rows[0]["DT_INJURY"].ToString();
                }
                else
                {
                    txtDateOfInjury.Text = "";
                }


                txtJobTitle.Text = _patientDs.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString();
                txtWorkActivites.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES"].ToString();
                txtState.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
                txtCarrierCaseNo.Text = _patientDs.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString();

                txtEmployerName.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
                //lblViewEmployerName.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();

                txtEmployerPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString();
                //lblViewEmployerPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString();

                txtEmployerAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString();
                //lblViewEmployerAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString();

                txtEmployerCity.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString();
                //lblViewEmployerCity.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString();

                txtEmployerState.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString();
                //lblViewEmployerState.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString();

                txtEmployerZip.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();
                //lblViewEmployerZip.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();

                if (_patientDs.Tables[0].Rows[0]["BT_TRANSPORTATION"] == "True" && _patientDs.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"] != "")
                {
                    CheckBox chkTransportation = (CheckBox)DtlView.Items[0].FindControl("chkTransportation");
                    chkTransportation.Checked = true;
                    chkTransportation.Checked = true;
                }
                else
                {
                    chkTransportation.Checked = false;
                    chkTransportation.Checked = false;
                }
                extddlPatientState.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();

                Label lblViewPatientState = (Label)DtlView.Items[0].FindControl("lblViewPatientState");
                if (extddlPatientState.Text != "NA" && extddlPatientState.Text != "")
                    lblViewPatientState.Text = extddlPatientState.Selected_Text;
                extddlEmployerState.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_STATE_ID"].ToString();

                Label lblViewEmployerState = (Label)DtlView.Items[0].FindControl("lblViewEmployerState");
                if (extddlEmployerState.Text != "NA" && extddlEmployerState.Text != "")
                    lblViewEmployerState.Text = extddlEmployerState.Selected_Text;

                txtChartNo.Text = _patientDs.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString();
                Label lblViewChartNo = (Label)DtlView.Items[0].FindControl("lblViewChartNo");
                lblViewChartNo.Text = _patientDs.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString();

                if (_patientDs.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "01/01/1900" && _patientDs.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "&nbsp;")
                {
                    txtDateofFirstTreatment.Text = _patientDs.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString();
                    //lblViewDateOfFirstTreatment.Text = _patientDs.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString();
                }
                else
                {
                    txtDateofFirstTreatment.Text = "";
                    //lblViewDateOfFirstTreatment.Text = "";
                }


            }

            //_bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            //DataSet _patientAccidentDs = _bill_Sys_PatientBO.GetPatientAccidentDetails(txtPatientID.Text);
            //txtAccidentID
            //if (_patientDs.Tables[0].Rows.Count > 0)
            //{
            //ClearPatientAccidentControl();
            if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_INFO_ID"].ToString() != "&nbsp;")
            {
                txtAccidentID.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_INFO_ID"].ToString();
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString() != "&nbsp;")
            {
                txtPlatenumber.Text = _patientDs.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();
                //lblViewAccidentPlatenumber.Text = _patientDs.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "&nbsp;")
            {
                txtAccidentAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
                //lblViewAccidentAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "&nbsp;")
            {
                txtAccidentCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
                //lblViewAccidentCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "&nbsp;")
            {
                txtAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
                //lblViewAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString() != "&nbsp;")
            {
                txtPolicyReport.Text = _patientDs.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString();
                ////lblViewAccidentReportNumber.Text = _patientDs.Tables[0].Rows[0].ItemsArray.GetValue(6).ToString();
            }
            //if (_patientDs.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString() != "&nbsp;")
            //{
            //    txtListOfPatient.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString();
            //    ////lblViewAdditionalPatient.Text = _patientDs.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            //}
            if (_patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "01/01/1900")
            {
                txtDateofAccident.Text = _patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
                //lblViewAccidentDate.Text = _patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
            }
            else
            {
                txtDateofAccident.Text = "";
                //lblViewAccidentDate.Text = "";
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString() != "&nbsp;")
            {
                string sz_patienttype = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString();
                foreach (System.Web.UI.WebControls.ListItem li in rdolstPatientType.Items)
                {
                    if (li.Value.ToString() == sz_patienttype)
                    {
                        li.Selected = true;
                        //lblPatientType.Text = "Bicyclist Driver";
                        break;
                    }
                }

                foreach (System.Web.UI.WebControls.ListItem li in rdolstPatientType.Items)
                {
                    if (li.Selected == true)
                    {
                        Label lblPatientType = (Label)DtlView.Items[0].FindControl("lblPatientType");
                        lblPatientType.Text = li.Text.ToString();
                    }
                }

            }
            //   if (_patientDs.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() != "&nbsp;") { extddlAccidentState.Text = _patientDs.Tables[0].Rows[0].ItemArray.GetValue(10).ToString(); }
            //}


            //Billing_Sys_ManageNotesBO manageNotes = new Billing_Sys_ManageNotesBO();
            //DataSet _patientDs = manageNotes.GetCaseDetails(txtPatientID.Text);
            //if (_patientDs.Tables[0].Rows.Count > 0)
            //{
            Label lblVLocation = (Label)DtlView.Items[0].FindControl("lblVLocation1");
            lblVLocation.Text = _patientDs.Tables[0].Rows[0]["sz_location_Name"].ToString();
            //txtPatientID.Text = Session["PatientID"].ToString();
            if (_patientDs.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() != "") { txtCaseID.Text = _patientDs.Tables[0].Rows[0]["SZ_CASE_ID"].ToString(); }
            if (_patientDs.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != "")
            {

                extddlCaseType.Text = _patientDs.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString();

                Label lblViewCasetype = (Label)DtlView.Items[0].FindControl("lblViewCasetype");
                if (extddlCaseType.Text != "NA" && extddlCaseType.Text != "")
                    lblViewCasetype.Text = extddlCaseType.Selected_Text;
            }
            //if (_patientDs.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != "")
            //{
            //    extddlProvider.Text = _patientDs.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString();

            //}
            /*vivek patil*/
            if (_patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")
            {
                extddlInsuranceCompany.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString(); // Not in use
                hdninsurancecode.Value = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                txtInsuranceCompany.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();

                if (extddlInsuranceCompany.Text != "NA" && extddlInsuranceCompany.Text != "") //Not use
                    //  lblViewInsuranceName.Text = extddlInsuranceCompany.Selected_Text;
                    if (txtInsuranceCompany.Text != "" && txtInsuranceCompany.Text != "No suggestions found for your search")
                        //lblViewInsuranceName.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                //  lstInsuranceCompanyAddress.Items.Clear();
                //notuse  lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text).Tables[0];
                lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(hdninsurancecode.Value).Tables[0];
                lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                lstInsuranceCompanyAddress.DataValueField = "CODE";
                lstInsuranceCompanyAddress.DataBind();

            }
            if (_patientDs.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != "")
            {
                extddlCaseStatus.Text = _patientDs.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString();

                Label lblViewCaseStatus = (Label)DtlView.Items[0].FindControl("lblViewCaseStatus");
                if (extddlCaseStatus.Text != "NA" && extddlCaseStatus.Text != "")
                    lblViewCaseStatus.Text = extddlCaseStatus.Selected_Text;
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != "")
            {
                extddlAttorney.Text = _patientDs.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString();
                //hdnattorneycode.Value = extddlAttorney.Text;
                Patient_TVBO _objAtrnyInfo = new Patient_TVBO();
                //DataSet _attornyDs = _objAtrnyInfo.GetAttornyInfo(hdnattorneycode.Value.ToString());
                //if (_attornyDs.Tables[0].Rows.Count > 0)
                //{
                //    //txtattorneyaddress.Text = _attornyDs.Tables[0].Rows[0]["SZ_ATTORNEY_ADDRESS"].ToString();
                //    //txtattorneycity.Text = _attornyDs.Tables[0].Rows[0]["SZ_ATTORNEY_CITY"].ToString();
                //    //txtattorneState.Text = _attornyDs.Tables[0].Rows[0]["SZ_STATE_NAME"].ToString();
                //    //txtattorneyzip.Text = _attornyDs.Tables[0].Rows[0]["SZ_ATTORNEY_ZIP"].ToString();
                //    //txtattorneyphone.Text = _attornyDs.Tables[0].Rows[0]["SZ_ATTORNEY_PHONE"].ToString();
                //    //txtattorneyfax.Text = _attornyDs.Tables[0].Rows[0]["SZ_ATTORNEY_FAX"].ToString();

                //}
                Label lblViewAttorney = (Label)DtlView.Items[0].FindControl("lblViewAttorney");
                if (extddlAttorney.Text != "NA" && extddlAttorney.Text != "")
                    lblViewAttorney.Text = extddlAttorney.Selected_Text;
                txtAttorneyCompany.Text = extddlAttorney.Selected_Text;
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "")
            {
                txtClaimNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                //lblViewInsuranceClaimNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                if (txtClaimNumber.Text.Equals("NA"))
                {
                    txtClaimNumber.Text = "";
                    //lblViewInsuranceClaimNumber.Text = "";
                }
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "")
            {
                txtPolicyNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                //lblViewPolicyNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                if (txtPolicyNumber.Text.Equals("NA"))
                {
                    txtPolicyNumber.Text = "";
                    //lblViewPolicyNumber.Text = "";
                }
            }
            //    if (_patientDs.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() != "") { txtDateofAccident.Text = Convert.ToDateTime(_patientDs.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()).ToShortDateString(); }
            if (_patientDs.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != "")
            {
                extddlAdjuster.Text = _patientDs.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString();
                Label lblViewAdjusterName = (Label)DtlView.Items[0].FindControl("lblViewAdjusterName");
                if (extddlAdjuster.Text != "NA" && extddlAdjuster.Text != "")
                    lblViewAdjusterName.Text = extddlAdjuster.Selected_Text;
            }
            if (_patientDs.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString() != "")
            {
                txtAssociateDiagnosisCode.Text = _patientDs.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString();

            }
            if (_patientDs.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString() != "")
            {
                txtPlatenumber.Text = _patientDs.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString();
                //lblViewAccidentPlatenumber.Text = _patientDs.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString();
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "")
            {
                txtAccidentAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
                //lblViewAccidentAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "")
            {
                txtAccidentCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
                //lblViewAccidentCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "")
            {
                txtAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
                //lblViewAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
            }
            if (_patientDs.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString() != "")
            {
                txtPolicyReport.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString();

            }
            if (_patientDs.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString() != "")
            {
                //txtListOfPatient.Text = _patientDs.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString();

            }
            if (_patientDs.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString() != "")
            {
                extddlLocation.Text = _patientDs.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString();
                Label lblVLocation14 = (Label)DtlView.Items[0].FindControl("lblVLocation1");
                if (extddlLocation.Text != "NA" && extddlLocation.Text != "")
                    lblVLocation14.Text = extddlLocation.Selected_Text;
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
                    txtInsuranceStreet.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_STREET"].ToString();

                    txtInsFax.Text = _patientDs.Tables[0].Rows[0]["sz_fax_number"].ToString();
                    txtInsPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                    txtInsContactPerson.Text = _patientDs.Tables[0].Rows[0]["sz_contact_person"].ToString();
                    
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
            string secInsId = "";
            string secInsAddId = "";
            string insType = "";

            string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
            sqlCon = new SqlConnection(strConn);

            string query1 = "select SZ_INSURANCE_ID FROM  MST_SEC_INSURANCE_DETAIL  WHERE SZ_CASE_ID='" + txtCaseID.Text + "' AND SZ_COMPANY_ID='" + txtCompanyID.Text + "'";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd = new SqlCommand(query1, sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            SqlDataReader dr;
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand(query1, sqlCon);
                dr = sqlCmd.ExecuteReader();
                //if (dr.Read())
                //{
                while (dr.Read())
                {
                    if (dr["SZ_INSURANCE_ID"] != DBNull.Value)
                    {
                        secInsId = dr["SZ_INSURANCE_ID"].ToString();
                    }
                }
                //}
                //else
                //{
                //    txtSecInsName.Text = "";
                //}
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

            if (secInsId != "")
            {
                string query = "select SZ_INSURANCE_NAME FROM MST_INSURANCE_COMPANY WHERE SZ_INSURANCE_ID='" + secInsId + "'";
                //sqlCmd = new SqlCommand(query, sqlCon);
                //da = new SqlDataAdapter(sqlCmd);
                //SqlDataReader dr;
                try
                {
                    sqlCon.Open();
                    sqlCmd = new SqlCommand(query, sqlCon);
                    dr = sqlCmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["SZ_INSURANCE_NAME"] != DBNull.Value)
                        {
                            txtSecInsName.Text = dr["SZ_INSURANCE_NAME"].ToString();
                        }

                        //if (dr["SZ_INSURANCE_ID"] != DBNull.Value)
                        //{
                        //    secInsId = dr["SZ_INSURANCE_ID"].ToString();
                        //}
                        //if (dr["SZ_ADDRESS_ID"] != DBNull.Value)
                        //{
                        //    secInsAddId = dr["SZ_ADDRESS_ID"].ToString();
                        //}
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
            }
            else
            {
                txtSecInsName.Text = "";
            }

            string query2 = "select SZ_ADDRESS_ID, SZ_INSURANCE_TYPE from MST_SEC_INSURANCE_DETAIL where  SZ_CASE_ID='" + txtCaseID.Text + "' and SZ_COMPANY_ID='" + txtCompanyID.Text + "' and SZ_INSURANCE_ID='" + secInsId + "'";
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand(query2, sqlCon);
                dr = sqlCmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["SZ_ADDRESS_ID"] != DBNull.Value)
                    {
                        secInsAddId = dr["SZ_ADDRESS_ID"].ToString();
                    }
                    if (dr["SZ_INSURANCE_TYPE"] != DBNull.Value)
                    {
                        insType = dr["SZ_INSURANCE_TYPE"].ToString();
                    }
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
            if (insType != "")
            {
                if (insType == "SEC")
                {
                    ddlInsuranceType.SelectedIndex = 1;
                }
                else if (insType == "MAJ")
                {
                    ddlInsuranceType.SelectedIndex = 2;
                }
                else if (insType == "PRI")
                {
                    ddlInsuranceType.SelectedIndex = 3;
                }
                else
                {
                    ddlInsuranceType.SelectedIndex = 0;
                }
            }
            if (secInsAddId != "")
            {
                DataSet ds1 = null;
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
                    SqlCommand sqlcmd = new SqlCommand("SP_MST_INSURANCE_ADDRESS", con);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@SZ_INS_ADDRESS_ID", secInsAddId);

                    sqlcmd.Parameters.Add("@FLAG", "LIST");
                    da = new SqlDataAdapter(sqlcmd);
                    ds1 = new DataSet();
                    da.Fill(ds1);
                    con.Close();
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        txtSecInsAddress.Text = ds1.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                        txtInsCity.Text = ds1.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                        txtInsState.Text = ds1.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                        txtInsZip.Text = ds1.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                        txtSecInsPhone.Text = ds1.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                        txtSecInsFax.Text = ds1.Tables[0].Rows[0]["sz_fax_number"].ToString();
                        txtInsConatactPerson.Text = ds1.Tables[0].Rows[0]["sz_contact_person"].ToString();
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
            else
            {
                txtSecInsAddress.Text = "";
                txtInsCity.Text = "";
                txtInsState.Text = "";
                txtInsZip.Text = "";
                txtSecInsPhone.Text = "";
                txtSecInsFax.Text = "";
                txtInsConatactPerson.Text = "";
            }

            //txtAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ADDRESS"].ToString();
            //txtCity.Text = _patientDs.Tables[0].Rows[0]["SZ_CITY"].ToString();
            //txtAdjusterState.Text = _patientDs.Tables[0].Rows[0]["SZ_STATE"].ToString();
            //txtZip.Text = _patientDs.Tables[0].Rows[0]["SZ_ZIP"].ToString();
            //txtAdjusterPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_PHONE"].ToString();

            txtAdjusterExtension.Text = _patientDs.Tables[0].Rows[0]["SZ_EXTENSION"].ToString();
            txtfax.Text = _patientDs.Tables[0].Rows[0]["SZ_FAX"].ToString();
            txtEmail.Text = _patientDs.Tables[0].Rows[0]["SZ_EMAIL"].ToString();
            
            ////ClearPatientAccidentControl();
            txtATPlateNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();

            if (_patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "01/01/1900")
            {
                txtATAccidentDate.Text = _patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
            }
            else
            {
                txtATAccidentDate.Text = "";
            }
            txtATAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
            
            txtATCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
            
            txtATReportNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString();
            
            txtATAdditionalPatients.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString();
            
            extddlATAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE_ID"].ToString();
            Label lblViewAccidentState = (Label)DtlView.Items[0].FindControl("lblViewAccidentState");
            if (extddlATAccidentState.Text != "NA" && extddlATAccidentState.Text != "")
                lblViewAccidentState.Text = extddlATAccidentState.Selected_Text;

            txtATHospitalName.Text = _patientDs.Tables[0].Rows[0]["SZ_HOSPITAL_NAME"].ToString();
            
            txtATHospitalAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_HOSPITAL_ADDRESS"].ToString();

            
            txtATDescribeInjury.Text = _patientDs.Tables[0].Rows[0]["SZ_DESCRIBE_INJURY"].ToString();
            
            txtATAdmissionDate.Text = _patientDs.Tables[0].Rows[0]["DT_ADMISSION_DATE"].ToString();
            if (_patientDs.Tables[0].Rows[0]["sz_company_name"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["sz_company_name"].ToString() != "")
            {
                Label lblVCopyfrom = (Label)DtlView.Items[0].FindControl("lblcopyfrom");
                lblVCopyfrom.Text = _patientDs.Tables[0].Rows[0]["sz_company_name"].ToString();
            }

            lblMsg.Visible = false;

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

    public void LoadDataOnPage()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            _bill_Sys_CaseObject.SZ_PATIENT_ID = "";
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            //extddlProvider.Flag_ID = txtCompanyID.Text.ToString();
            //extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            //extddlAttorney.Flag_ID = txtCompanyID.Text.ToString();
            //extddlAdjuster.Flag_ID = txtCompanyID.Text.ToString();
            //extddlInsuranceCompany.Flag_ID = txtCompanyID.Text.ToString();


            extddlCaseType.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            //extddlProvider.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            extddlCaseStatus.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            extddlAttorney.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            extddlAdjuster.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            extddlInsuranceCompany.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;

            Session["PassedCaseID"]=Request.QueryString["CaseID"].ToString();
            if (Session["PassedCaseID"] != null)
            {
                txtPatientID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;

                GetPatientDetails();
                Session["Associate_Case_ID"] = Session["PassedCaseID"];
                txtCaseID.Text = Session["PassedCaseID"].ToString();
                EditOperation _editOperation = new EditOperation();
                _editOperation.Xml_File = "CaseDetails.xml";
                _editOperation.WebPage = this.Page;
                _editOperation.Primary_Value = Session["PassedCaseID"].ToString();
                _editOperation.LoadData();

                _editOperation = new EditOperation();
                _editOperation.Xml_File = "CaseDetailsForLabel.xml";
                _editOperation.WebPage = this.Page;
                _editOperation.Primary_Value = Session["PassedCaseID"].ToString();
                _editOperation.LoadData();

                Session["AttornyID"] = extddlAttorney.Text; // Girish
                //BindSuppliesGrid();
                //lblSaveCaseName.Text = txtCaseName.Text;

            }
            else
            {
                Response.Redirect("Bill_Sys_SearchCase.aspx", false);
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

    protected void UpdatePatientInformation()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList _objAL = new ArrayList();
            _objAL.Add(txtPatientFName.Text);
            _objAL.Add(txtPatientLName.Text);
            _objAL.Add(txtPatientAge.Text); 
            _objAL.Add(txtPatientAddress.Text);
            _objAL.Add(txtPatientStreet.Text);
            _objAL.Add(txtPatientCity.Text);
            _objAL.Add(txtPatientZip.Text);
            _objAL.Add(txtPatientPhone.Text);
            _objAL.Add(txtPatientEmail.Text);
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add(txtWorkPhone.Text);
            _objAL.Add(txtExtension.Text);
            _objAL.Add(txtMI.Text);
            //_objAL.Add(txtWCBNumber.Text);
            _objAL.Add(txtPolicyNumber.Text);
            _objAL.Add(txtSocialSecurityNumber.Text);
            _objAL.Add(txtDateOfBirth.Text);
            _objAL.Add(ddlSex.SelectedValue);
            _objAL.Add(txtDateOfInjury.Text);
            _objAL.Add(txtJobTitle.Text);
            _objAL.Add(txtWorkActivites.Text);
            _objAL.Add(txtState.Text);
            _objAL.Add(txtCarrierCaseNo.Text);

            _objAL.Add(txtEmployerName.Text);
            _objAL.Add(txtEmployerPhone.Text);
            _objAL.Add(txtEmployerAddress.Text);
            _objAL.Add(txtEmployerCity.Text);
            _objAL.Add(txtEmployerState.Text);
            _objAL.Add(txtEmployerZip.Text);
            //Label lblVLocationAdd = (Label)DtlView.Items[0].FindControl("lblVLocation1");
            _objAL.Add("");//_objAL.Add(lblVLocationAdd.Text);

            _objAL.Add("UPDATE");
            _objAL.Add(txtPatientID.Text);

            if (chkWrongPhone.Checked) { _objAL.Add("True"); } else { _objAL.Add("False"); }
            if (chkTransportation.Checked) { _objAL.Add("True"); } else { _objAL.Add("False"); }

            //_objAL.Add(txtDateofAccident.Text);
            //_objAL.Add(txtPlatenumber.Text);
            //_objAL.Add(txtPolicyReport.Text);

            _objAL.Add(extddlEmployerState.Text);
            _objAL.Add(extddlPatientState.Text);

            _objAL.Add(txtChartNo.Text);
            _objAL.Add(txtDateofFirstTreatment.Text);


            Patient_TVBO objPatientBO = new Patient_TVBO();
            objPatientBO.savePatientInformation(_objAL);

            _objAL = new ArrayList();
            _objAL.Add(txtAccidentID.Text);
            _objAL.Add(txtPatientID.Text);
            _objAL.Add(txtPlatenumber.Text);
            _objAL.Add(txtDateofAccident.Text);
            _objAL.Add(txtAccidentAddress.Text);
            _objAL.Add(txtAccidentCity.Text);
            _objAL.Add(txtAccidentState.Text);
            _objAL.Add(txtPolicyReport.Text);
            _objAL.Add(txtListOfPatient.Text);

            _objAL.Add(txtCompanyID.Text);

            if (txtAccidentID.Text != "")
            {
                _objAL.Add("UPDATE");
            }
            else
            {
                _objAL.Add("Add");
            }
            _objAL.Add(extddlATAccidentState.Text);
            _objAL.Add("");

            objPatientBO.savePatientAccidentInformation(_objAL);




            _objAL = new ArrayList();
            _objAL.Add(txtCaseID.Text);
            _objAL.Add("");
            _objAL.Add(extddlCaseType.Text);
            //if (!extddlInsuranceCompany.Text.Equals("") && !extddlInsuranceCompany.Text.Equals("NA"))
            //{
            //    _objAL.Add(extddlInsuranceCompany.Text);
            //}
            //else
            //{
            //    _objAL.Add("");
            //}
            if (!txtInsuranceCompany.Text.Equals("") && !txtInsuranceCompany.Text.Equals("No suggestions found for your search") && hdninsurancecode.Value != "")
            {
                _objAL.Add(hdninsurancecode.Value);
            }
            else
            {
                _objAL.Add("");
            }

            _objAL.Add(extddlCaseStatus.Text);
            _objAL.Add(extddlAttorney.Text);
            _objAL.Add(txtPatientID.Text);
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add(txtClaimNumber.Text);
            _objAL.Add(txtPolicyNumber.Text);
            _objAL.Add(txtDateofAccident.Text);
            _objAL.Add(extddlAdjuster.Text);
            _objAL.Add(txtAssociateDiagnosisCode.Text);
            _objAL.Add(lstInsuranceCompanyAddress.Text);
            _objAL.Add("UPDATE");
            _objAL.Add(txtPolicyHolder.Text);
            _objAL.Add(extddlLocation.Text);
            _objAL.Add(txtWCBNo.Text);
            //_objAL.Add (txtAdjusterPhone.Text);
            //_objAL.Add (txtAdjusterExtension.Text);
            //_objAL.Add (txtfax.Text);
            //_objAL.Add(txtEmail.Text);

            objPatientBO = new Patient_TVBO();
            objPatientBO.saveCaseInformation(_objAL);

            /*
             @DT_ACCIDENT_DATE", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_ADDRESS", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_CITY", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_STATE", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REPORT_NO", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FROM_CAR", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_STATE_ID", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_HOSPITAL_NAME", objAL[9].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_HOSPITAL_ADDRESS", objAL[10].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DESCRIBE_INJURY", objAL[11].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_ADMISSION_DATE", 
             */

            // Save Accident Information

            ArrayList objAccidentInfo = new ArrayList();
            objAccidentInfo.Add(txtPatientID.Text);
            objAccidentInfo.Add(txtATPlateNumber.Text);
            objAccidentInfo.Add(txtATAccidentDate.Text);
            objAccidentInfo.Add(txtATAddress.Text);
            objAccidentInfo.Add(txtATCity.Text);
            objAccidentInfo.Add(txtATReportNumber.Text);
            objAccidentInfo.Add(txtATAdditionalPatients.Text);
            objAccidentInfo.Add(extddlATAccidentState.Text);
            objAccidentInfo.Add(txtATHospitalName.Text);
            objAccidentInfo.Add(txtATHospitalAddress.Text);
            objAccidentInfo.Add(txtATDescribeInjury.Text);
            objAccidentInfo.Add(txtATAdmissionDate.Text);
            objAccidentInfo.Add("");
            objPatientBO.saveAccidentInformation(objAccidentInfo);
            // End

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
}//