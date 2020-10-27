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
using System.IO;
using System.Data;
using System.Data.SqlClient;
using MG2;
using MG2PDF.DataAccessObject;
using ApplicationSettings = MG2.ApplicationSettings;

public partial class AJAX_Pages_FrmPopupPage : PageBase
{
    string StrConnection = ConfigurationManager.AppSettings["Connection_String"].ToString();
    string sz_UserID = "";
    string sz_CompanyID = "", CmpName="",sz_NodeName="",sz_CaseNo="",sz_Node_ID="";
    string sz_CaseID = "", sz_BillNo = "", PatientID = "", sz_speciality = "", CheckDC = "", tempPath="",Logicalpath="";

    protected void Page_Init(object sender, EventArgs e)
    {
        //System.Web.HttpBrowserCapabilities browser = Request.Browser;
        //string sType = browser.Type;
        //string sName = browser.Browser;
        //string szCSS;
        //string _url = "";
        
        //if (Request.RawUrl.IndexOf("?") > 0)
        //{
        //    _url = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("?"));
        //}
        //else
        //{
        //    _url = Request.RawUrl;
        //}
        //if (browser.Browser.ToLower().Contains("firefox"))
        //{
        //    szCSS = "css/main-ff.css";
        //}
        //else
        //{
        //    if (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple"))
        //    {
        //        szCSS = "css/main-ch.css";
        //    }
        //    else
        //    {
        //        szCSS = "css/main-ie.css";
        //    }
        //}
        //System.Text.StringBuilder b = new System.Text.StringBuilder();
        //b.AppendLine("");
        //if (_url.Contains("AJAX Pages")) { b.AppendLine("<link rel='shortcut icon' href='../Registration/icon.ico' />"); } else b.AppendLine("<link rel='shortcut icon' href='Registration/icon.ico' />");
        //b.AppendLine("<link type='text/css' rel='stylesheet' href='css/intake-sheet-ff.css' />");
        //b.AppendLine("<link href='Css/mainmaster.css' rel='stylesheet' type='text/css' />");
        //b.AppendLine("<link href='Css/UI.css' rel='stylesheet' type='text/css' />");
        //b.AppendLine("<link type='text/css' rel='stylesheet' href='css/style.css' />");
        //b.AppendLine("<link href='css/style.css'  rel='stylesheet' type='text/css'  />");
        //b.AppendLine("<link href='" + szCSS + "' type='text/css' rel='Stylesheet' />");
        //this.MG2PopupHead.InnerHtml = b.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        sz_UserID = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();
            
        if (Request.QueryString["caseid"] != null)
        {
            sz_CaseID = Request.QueryString["caseid"].ToString();
            sz_BillNo = Request.QueryString["billnumber"].ToString();
            sz_speciality = Request.QueryString["speciality"].ToString();
            sz_CaseNo = Request.QueryString["caseno"].ToString();
            txtbillno.Text = sz_BillNo;
           
            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
            PatientID = _caseDetailsBO.GetCasePatientID(Request.QueryString["caseid"].ToString(), "");
        }

        //ddlGuidline.Attributes.Add("onChange", "return SplitString();");
        if (!IsPostBack)
        {
            //ddlGuidline.Items.Add("--Select--");
            //ddlGuidline.Items.Add("K-E7E  ");
            //ddlGuidline.Items.Add("B-D9A  ");
            //ddlGuidline.Items.Add("N-D10D ");
            //ddlGuidline.Items.Add("S-D10EI");


            GetRecord();
            GetPatientDetails();
            getDoctorDefaultList();
      
        }
        
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        InsertRecord();
    }
    protected void btnSaveBottom_Click(object sender, EventArgs e)
    {
        InsertRecord();
    }

    public void InsertRecord()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            MG2DAO objMG2 = new MG2DAO();


            objMG2.sz_CompanyID = sz_CompanyID;
            objMG2.sz_CaseID = sz_CaseID;
            objMG2.sz_UserID = sz_UserID;
            objMG2.attendingDoctorNameAddress = DDLAttendingDoctors.SelectedItem.Text;
            objMG2.approvalRequest = TxtApproval.Text;
            objMG2.dateOfService = TxtDateofService.Text;
            objMG2.datesOfDeniedRequest = TxtDateofApplicable.Text;
            if (Chkdid.Checked)
                objMG2.chkDid = "1";
            else
                objMG2.chkDid = "0";
            if (Chkdidnot.Checked)
                objMG2.chkDidNot = "1";
            else
                objMG2.chkDidNot = "0";
            objMG2.contactDate = TxtSpoke.Text;
            objMG2.personContacted = Txtspecktoanyone.Text;

            if (ChkAcopy.Checked)
                objMG2.chkCopySent = "1";
            else
                objMG2.chkCopySent = "0";
            objMG2.faxEmail = TxtAddressRequired.Text;

            if (ChkIAmnot.Checked)
                objMG2.chkCopyNotSent = "1";
            else
                objMG2.chkCopyNotSent = "0";
            objMG2.indicatedFaxEmail = Txtaboveon.Text;
            objMG2.providerSignDate = TxtProviderDate.Text;

            if (ChktheSelf.Checked)
                objMG2.chkNoticeGiven = "1";
            else
                objMG2.chkNoticeGiven = "0";
            objMG2.printCarrierEmployerNoticeName = TxtByPrintNameD.Text;
            objMG2.noticeTitle = TxtTitleD.Text;
            objMG2.noticeCarrierSignDate = TxtDateD.Text;
            objMG2.carrierDenial = TxtSectionE.Text;

            if (ChkGranted.Checked)
                objMG2.chkGranted = "1";
            else
                objMG2.chkGranted = "0";
            if (ChkGrantedinPart.Checked)
                objMG2.chkGrantedInParts = "1";
            else
                objMG2.chkGrantedInParts = "0";
            if (ChkWithoutPrejudice.Checked)
                objMG2.chkWithoutPrejudice = "1";
            else
                objMG2.chkWithoutPrejudice = "0";
            if (ChkDenied.Checked)
                objMG2.chkDenied = "1";
            else
                objMG2.chkDenied = "0";
            if (ChkBurden.Checked)
                objMG2.chkBurden = "1";
            else
                objMG2.chkBurden = "0";
            if (ChkSubstantially.Checked)
                objMG2.chkSubstantiallySimilar = "1";
            else
                objMG2.chkSubstantiallySimilar = "0";
            objMG2.medicalProfessional = TxtMedicalProfes.Text;
            if (ChkMadeE.Checked)
                objMG2.chkMedicalArbitrator = "1";
            else
                objMG2.chkMedicalArbitrator = "0";
            if (ChkChairE.Checked)
                objMG2.chkWCBHearing = "1";
            else
                objMG2.chkWCBHearing = "0";
            objMG2.printCarrierEmployerResponseName = TxtByPrintNameE.Text;
            objMG2.responseTitle = TxtTitleE.Text;
            //cmd.Parameters.AddWithValue("@sz_signature_E",TxtSignatureE .Text );
            objMG2.responseCarrierSignDate = TxtDateE.Text;

            objMG2.printDenialCarrierName = TxtByPrintNameF.Text;
            objMG2.denialTitle = TxtTitleF.Text;
            //cmd.Parameters.AddWithValue("@sz_signature_F", TxtSignatureF.Text);
            objMG2.denialCarrierSignDate = TxtDateF.Text;


            if (ChkIrequestG.Checked)
                objMG2.chkRequestWC = "1";
            else
                objMG2.chkRequestWC = "0";
            if (ChkMadeG.Checked)
                objMG2.chkMedicalArbitratorByWC = "1";
            else
                objMG2.chkMedicalArbitratorByWC = "0";
            if (ChkChairG.Checked)
                objMG2.chkWCBHearingByWC = "1";
            else
                objMG2.chkWCBHearingByWC = "0";
            //cmd.Parameters.AddWithValue("@sz_claimant_signature",TxtClairmantSignature .Text );
            objMG2.claimantSignDate = TxtClaimantDate.Text;

            objMG2.WCBCaseNumber = Txtwcbcasenumber.Text;
            objMG2.carrierCaseNumber = TxtCarrierCaseNo.Text;
            objMG2.dateOfInjury = TxtDateofInjury.Text;
            objMG2.firstName = TxtFirstName.Text;
            objMG2.middleName = TxtMiddleName.Text;
            objMG2.lastName = TxtLastName.Text;
            objMG2.patientAddress = TxtPatientAddress.Text;
            objMG2.employerNameAddress = TxtEmployerNameAdd.Text;
            objMG2.insuranceNameAddress = TxtInsuranceNameAdd.Text;
            objMG2.sz_DoctorID = DDLAttendingDoctors.SelectedValue;//DDLAttendingDoctors.Text
            objMG2.providerWCBNumber = TxtIndividualProvider.Text;
            objMG2.doctorPhone = TxtTelephone.Text;
            objMG2.doctorFax = TxtFaxNo.Text;
            
            //objMG2.guidelineSection = ddlGuidline.SelectedItem.Text;
            if (ddlGuidline.SelectedItem.Text != "--Select--")
            {
                ArrayList ar = new ArrayList();
                string spt = ddlGuidline.SelectedItem.Text;
                string[] wrd = spt.Split('-');
                foreach (string word in wrd)
                {
                    ar.Add(word);
                }
                objMG2.bodyInitial = ar[0].ToString();
                objMG2.guidelineSection = ar[1].ToString();
            }
            else
            {
                objMG2.bodyInitial = TxtGuislineChar.Text;

                if (TxtGuidline1.Text == "")
                    TxtGuidline1.Text = " ";
                if (TxtGuidline2.Text == "")
                    TxtGuidline2.Text = " ";
                if (TxtGuidline3.Text == "")
                    TxtGuidline3.Text = " ";
                if (TxtGuidline4.Text == "")
                    TxtGuidline4.Text = " ";
                if (TxtGuidline5.Text == "")
                    TxtGuidline5.Text = " ";

                objMG2.guidelineSection = TxtGuidline1.Text + "" + TxtGuidline2.Text + "" + TxtGuidline3.Text + "" + TxtGuidline4.Text + "" + TxtGuidline5.Text; ;
            }
            objMG2.socialSecurityNumber = TxtSocialSecurityNo.Text;
            objMG2.sz_BillNo = sz_BillNo;
            objMG2.PatientID= PatientID;


            MBS_MG2 oj = new MBS_MG2();
            oj.SaveMG2(objMG2);

            

            usrMessage.PutMessage("Record Saved Successfully");
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

    public void GetRecord()
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            MBS_MG2 oj = new MBS_MG2();
            DataTable dt = oj.GetMG2Record(sz_CompanyID, sz_CaseID, sz_BillNo);

            if (dt.Rows.Count > 0)
            {
                //DDLGiidline.Text = dt.Rows[0]["sz_guidelines_reference"].ToString();
                TxtApproval.Text = dt.Rows[0]["sz_approval_request"].ToString();
                TxtDateofService.Text = dt.Rows[0]["sz_wcb_case_file"].ToString();
                TxtDateofApplicable.Text = dt.Rows[0]["sz_applicable"].ToString();
                if (dt.Rows[0]["bt_did"].ToString() == "1")
                    Chkdid.Checked = true;
                else
                    Chkdid.Checked = false;
                if (dt.Rows[0]["bt_not_did"].ToString() == "1")
                    Chkdidnot.Checked = true;
                else
                    Chkdidnot.Checked = false;
                TxtSpoke.Text = dt.Rows[0]["sz_spoke"].ToString();
                Txtspecktoanyone.Text = dt.Rows[0]["sz_spoke_anyone"].ToString();
                if (dt.Rows[0]["bt_a_copy"].ToString() == "1")
                    ChkAcopy.Checked = true;
                else
                    ChkAcopy.Checked = false;
                TxtAddressRequired.Text = dt.Rows[0]["sz_fund_by"].ToString();
                if (dt.Rows[0]["bt_equipped"].ToString() == "1")
                    ChkIAmnot.Checked = true;
                else
                    ChkIAmnot.Checked = false;
                Txtaboveon.Text = dt.Rows[0]["sz_indicated"].ToString();
                //TxtProviderSig.Text = dt.Rows[0]["sz_provider_signature"].ToString();

                if (dt.Rows[0]["dt_provider_signature_date"].ToString() != "" && dt.Rows[0]["dt_provider_signature_date"].ToString() != "01/01/1900")
                    TxtProviderDate.Text = dt.Rows[0]["dt_provider_signature_date"].ToString();
                else
                    TxtProviderDate.Text = "";

                if (dt.Rows[0]["bt_self_insurrer"].ToString() == "1")
                    ChktheSelf.Checked = true;
                else
                    ChktheSelf.Checked = false;
                TxtByPrintNameD.Text = dt.Rows[0]["sz_print_name_D"].ToString();
                TxtTitleD.Text = dt.Rows[0]["sz_title_D"].ToString();
                //TxtSignatureD.Text = dt.Rows[0]["sz_signature_D"].ToString();

                if (dt.Rows[0]["dt_date_D"].ToString() != "" && dt.Rows[0]["dt_date_D"].ToString() != "01/01/1900")
                    TxtDateD.Text = dt.Rows[0]["dt_date_D"].ToString();
                else
                    TxtDateD.Text = "";

                TxtSectionE.Text = dt.Rows[0]["sz_section_E"].ToString();
                if (dt.Rows[0]["bt_granted"].ToString() == "1")
                    ChkGranted.Checked = true;
                else
                    ChkGranted.Checked = false;
                if (dt.Rows[0]["bt_granted_in_part"].ToString() == "1")
                    ChkGrantedinPart.Checked = true;
                else
                    ChkGrantedinPart.Checked = false;
                if (dt.Rows[0]["bt_without_prejudice"].ToString() == "1")
                    ChkWithoutPrejudice.Checked = true;
                else
                    ChkWithoutPrejudice.Checked = false;
                if (dt.Rows[0]["bt_denied"].ToString() == "1")
                    ChkDenied.Checked = true;
                else
                    ChkDenied.Checked = false;
                if (dt.Rows[0]["bt_burden"].ToString() == "1")
                    ChkBurden.Checked = true;
                else
                    ChkBurden.Checked = false;
                if (dt.Rows[0]["bt_substantialy"].ToString() == "1")
                    ChkSubstantially.Checked = true;
                else
                    ChkSubstantially.Checked = false;
                TxtMedicalProfes.Text = dt.Rows[0]["sz_if_applicable"].ToString();
                if (dt.Rows[0]["bt_made_E"].ToString() == "1")
                    ChkMadeE.Checked = true;
                else
                    ChkMadeE.Checked = false;
                if (dt.Rows[0]["bt_chair_E"].ToString() == "1")
                    ChkChairE.Checked = true;
                else
                    ChkChairE.Checked = false;
                TxtByPrintNameE.Text = dt.Rows[0]["sz_print_name_E"].ToString();
                TxtTitleE.Text = dt.Rows[0]["sz_title_E"].ToString();
                //TxtSignatureE.Text = dt.Rows[0]["sz_signature_E"].ToString();

                if (dt.Rows[0]["dt_date_E"].ToString() != "" && dt.Rows[0]["dt_date_E"].ToString() != "01/01/1900")
                    TxtDateE.Text = dt.Rows[0]["dt_date_E"].ToString();
                else
                    TxtDateE.Text = "";

                TxtByPrintNameF.Text = dt.Rows[0]["sz_print_name_F"].ToString();
                TxtTitleF.Text = dt.Rows[0]["sz_title_F"].ToString();
                //TxtSignatureF.Text = dt.Rows[0]["sz_signature_F"].ToString();

                if (dt.Rows[0]["dt_date_F"].ToString() != "" && dt.Rows[0]["dt_date_F"].ToString() != "01/01/1900")
                    TxtDateF.Text = dt.Rows[0]["dt_date_F"].ToString();
                else
                    TxtDateF.Text = "";

                if (dt.Rows[0]["bt_i_request"].ToString() == "1")
                    ChkIrequestG.Checked = true;
                else
                    ChkIrequestG.Checked = false;
                if (dt.Rows[0]["bt_made_G"].ToString() == "1")
                    ChkMadeG.Checked = true;
                else
                    ChkMadeG.Checked = false;
                if (dt.Rows[0]["bt_chair_G"].ToString() == "1")
                    ChkChairG.Checked = true;
                else
                    ChkChairG.Checked = false;
                //TxtClairmantSignature.Text = dt.Rows[0]["sz_claimant_signature"].ToString();

                if (dt.Rows[0]["dt_claimant_date"].ToString() != "" && dt.Rows[0]["dt_claimant_date"].ToString() != "01/01/1900")
                    TxtClaimantDate.Text = dt.Rows[0]["dt_claimant_date"].ToString();
                else
                    TxtClaimantDate.Text = "";
                Txtwcbcasenumber.Text = dt.Rows[0]["sz_wcb_case_no"].ToString();
                TxtCarrierCaseNo.Text = dt.Rows[0]["sz_carrier_case_no"].ToString();
                TxtDateofInjury.Text = dt.Rows[0]["sz_date_of_injury"].ToString();
                TxtFirstName.Text = dt.Rows[0]["sz_patient_firstname"].ToString();
                TxtMiddleName.Text = dt.Rows[0]["sz_patient_middlename"].ToString();
                TxtLastName.Text = dt.Rows[0]["sz_patient_lastname"].ToString();
                TxtPatientAddress.Text = dt.Rows[0]["sz_patient_address"].ToString();
                TxtEmployerNameAdd.Text = dt.Rows[0]["sz_employee_name_address"].ToString();
                TxtInsuranceNameAdd.Text = dt.Rows[0]["sz_insurance_name_address"].ToString();

                DDLAttendingDoctors.SelectedValue = dt.Rows[0]["sz_doctor_ID"].ToString();
                CheckDC = dt.Rows[0]["sz_doctor_ID"].ToString();

                TxtIndividualProvider.Text = dt.Rows[0]["sz_individual_provider"].ToString();
                TxtTelephone.Text = dt.Rows[0]["sz_teltphone_no"].ToString();
                TxtFaxNo.Text = dt.Rows[0]["sz_fax_no"].ToString();
                TxtGuislineChar.Text = dt.Rows[0]["sz_Guidline_Char"].ToString();

                TxtWCBCaseNumber2.Text = dt.Rows[0]["sz_wcb_case_no"].ToString();

                if (dt.Rows[0]["sz_date_of_injury"].ToString() != "" && dt.Rows[0]["sz_date_of_injury"].ToString() != "01/01/1900")
                    TxtDateofInjury2.Text = dt.Rows[0]["sz_date_of_injury"].ToString();
                else
                    TxtDateofInjury2.Text = "";

                TxtPatientName.Text = TxtFirstName.Text + " " + TxtMiddleName.Text + " " + TxtLastName.Text;
                                              

                TxtSocialSecurityNo.Text = dt.Rows[0]["sz_security_no"].ToString();

                string filepath = dt.Rows[0]["sz_pdf_url"].ToString();
                if (filepath != "")
                {
                    filepath = filepath.Replace("\\", "/");
                    lblmsg.Visible = true;
                    lblmsg2.Visible = true;
                    hyLinkOpenPDF.Visible = true;
                    //string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
                    string str= dt.Rows[0]["DocumentManagerURL"].ToString();
                    //hyLinkOpenPDF.NavigateUrl = "http://localhost/MBSScans/" + filepath;
                    //hyLinkOpenPDF.NavigateUrl = "https://www.gogreenbills.com/MBSScans/"+filepath;
                    hyLinkOpenPDF.NavigateUrl = str+"/" + filepath;
                }
                else
                { }

                ddlGuidline.Text = dt.Rows[0]["sz_Guidline_Char"].ToString() + "-" + dt.Rows[0]["sz_Guidline"].ToString();
                string gdl = dt.Rows[0]["sz_Guidline"].ToString();
                if (gdl != "")
                {

                    TxtGuidline1.Text = gdl[0].ToString();
                    TxtGuidline2.Text = gdl[1].ToString();
                    TxtGuidline3.Text = gdl[2].ToString();
                    TxtGuidline4.Text = gdl[3].ToString();
                    TxtGuidline5.Text = gdl[4].ToString();
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
           
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void GetPatientDetails()
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            MBS_MG2 oj = new MBS_MG2();
            DataTable dt = oj.GetMG2Patient(PatientID);

            if (dt.Rows.Count > 0)
            {
                TxtFirstName.Text = dt.Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                TxtMiddleName.Text = dt.Rows[0]["MI"].ToString();
                TxtLastName.Text = dt.Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                TxtPatientAddress.Text = dt.Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                TxtSocialSecurityNo.Text = dt.Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                TxtEmployerNameAdd.Text = (dt.Rows[0]["SZ_EMPLOYER_NAME"].ToString() + " , " + dt.Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString());
                TxtInsuranceNameAdd.Text = (dt.Rows[0]["SZ_INSURANCE_NAME"].ToString() + " ," + dt.Rows[0]["sz_ins_address"].ToString());
                Txtwcbcasenumber.Text = dt.Rows[0]["SZ_WCB_NO"].ToString();
                TxtWCBCaseNumber2.Text = dt.Rows[0]["SZ_WCB_NO"].ToString();

                if (dt.Rows[0]["DT_INJURY"].ToString() != "" && dt.Rows[0]["DT_INJURY"].ToString() != "01/01/1900")
                {
                    TxtDateofInjury.Text = dt.Rows[0]["DT_INJURY"].ToString();
                    TxtDateofInjury2.Text = dt.Rows[0]["DT_INJURY"].ToString();
                }
                else
                {
                    TxtDateofInjury.Text = "";
                    TxtDateofInjury2.Text = "";
                }

                TxtPatientName.Text = TxtFirstName.Text + " " + TxtMiddleName.Text + " " + TxtLastName.Text;
                TxtCarrierCaseNo.Text = dt.Rows[0]["Case_No"].ToString();
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
           
        }

        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (TxtInsuranceNameAdd.Text.Trim() != "" || TxtInsuranceNameAdd.Text.Trim() != ",")
        {
          
            try
            {
                MBS_MG2 oj = new MBS_MG2();         
                string flg = oj.MG2CheckExist(sz_CompanyID, sz_CaseID ,sz_BillNo);
                if (flg == "1")
                    PrintPDF();
                else
                {
                    usrMessage.PutMessage("Please Save Record First.");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    usrMessage.Show();
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
                
            }
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnPrinBottom_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (TxtInsuranceNameAdd.Text.Trim() != "" || TxtInsuranceNameAdd.Text.Trim() != ",")
        {
          
            try
            {
              
                MBS_MG2 oj = new MBS_MG2();
                string flg = oj.MG2CheckExist(sz_CompanyID, sz_CaseID, sz_BillNo);
                if (flg == "1")
                    PrintPDF();
                else
                {
                    usrMessage.PutMessage("Please Save Record First.");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    usrMessage.Show();
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
               
            }
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void PrintPDF()
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            

            MBS_MG2 oj = new MBS_MG2();
            CmpName = oj.GetPDFPath(sz_CompanyID, sz_CaseID, sz_speciality);

            GenerateMG2Pdf obj = new GenerateMG2Pdf();

            Logicalpath = "MG2_"+sz_BillNo + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff");

            tempPath = obj.generateMG2(sz_CompanyID, sz_CaseID, sz_BillNo, CmpName,Logicalpath );

            SaveLogicalPath();
            string BillPath = CmpName + "\\" + Logicalpath + ".pdf";
            string str = MG2.ApplicationSettings.GetParameterValue("DocumentManagerURL");
            string newpath = str + "/" + BillPath;
            newpath = newpath.Replace("\\", "/");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + newpath + "');", true);
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
            
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void getDoctorDefaultList()
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
            Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();

            DataSet dsDoctorName = _obj.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            DDLAttendingDoctors.DataSource = dsDoctorName;
            ListItem objLI = new ListItem("---select---", "NA");
            DDLAttendingDoctors.DataTextField = "DESCRIPTION";
            DDLAttendingDoctors.DataValueField = "CODE";
            DDLAttendingDoctors.DataBind();
            DDLAttendingDoctors.Items.Insert(0, objLI);

            if (CheckDC == "")
            {
                Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
                DataSet dset = _bill_Sys_Visit_BO.GetBillDoctorList(sz_CompanyID, sz_BillNo, "GetBillDoctor");
                DDLAttendingDoctors.DataSource = dset.Tables[0];

                string nm = dset.Tables[0].Rows[0]["DOCTORID"].ToString();

                DDLAttendingDoctors.SelectedValue = nm;

                FillDoctorInfo();     
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
           
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void FillDoctorInfo()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //SqlConnection con = new SqlConnection(StrConnection);
        //con.Open();
        try
        {
        //    SqlCommand cmd = new SqlCommand("SP_Get_Doctor_info", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", DDLAttendingDoctors.SelectedValue);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

            MBS_MG2 oj = new MBS_MG2();
            DataTable dt = new DataTable();
            dt = oj.GetDoctorInfo(DDLAttendingDoctors.SelectedValue);
            //da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                TxtIndividualProvider.Text = dt.Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
                TxtTelephone.Text = dt.Rows[0]["SZ_OFFICE_PHONE"].ToString();
                TxtFaxNo.Text = dt.Rows[0]["SZ_OFFICE_FAX"].ToString();
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
            //con.Close();
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
    protected void DDLAttendingDoctors_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDoctorInfo();
    }

    private void SaveLogicalPath()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        FindNodeType();
       
        try
        {
            //string strGenFileName = sz_BillNo + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff")+"MG2.pdf";
            string BillPath = CmpName + "\\" + Logicalpath + ".pdf";

          
            string sz_UserName=((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_NAME.ToString();
            MBS_MG2 oj = new MBS_MG2();
            oj.SaveLogicalPath(sz_BillNo, BillPath);

            oj.SaveUploadDocumentMG2(sz_CaseID, sz_CompanyID, Logicalpath + ".pdf", BillPath, sz_NodeName, sz_UserName, sz_UserID,sz_Node_ID );

       
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
            
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void FindNodeType()
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            MBS_MG2 oj = new MBS_MG2();
            DataTable dt = oj.FindNode(sz_CompanyID, sz_speciality);

            sz_Node_ID = dt.Rows[0]["I_NODE_ID"].ToString();
            sz_NodeName = dt.Rows[0]["SZ_NODE_TYPE"].ToString();
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
            
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlGuidline_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGuidline.SelectedItem.Text != "--Select--")
        {
            string stln = ddlGuidline.SelectedItem.Text;
            ArrayList ar = new ArrayList();
            for (int i = 0; i < stln.Length; i++)
            {
                ar.Add(stln[i]);
            }

            TxtGuislineChar.Text = ar[0].ToString();
            TxtGuidline1.Text = ar[2].ToString();
            TxtGuidline2.Text = ar[3].ToString();
            TxtGuidline3.Text = ar[4].ToString();
            TxtGuidline4.Text = ar[5].ToString();
            TxtGuidline5.Text = ar[6].ToString();
        }
        else
        {
            TxtGuislineChar.Text = "";
            TxtGuidline1.Text = "";
            TxtGuidline2.Text = "";
            TxtGuidline3.Text = "";
            TxtGuidline4.Text = "";
            TxtGuidline5.Text = "";
        }
    }
    
}
