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
using HP1;
using HP1.HP1BO;




public partial class AJAX_Pages_Bill_Sys_Bill_Hp1Form : PageBase
{


    string StrConnection = ConfigurationManager.AppSettings["Connection_String"].ToString();
    string sz_UserID = "";
    string sz_CompanyID = "", CmpName = "", sz_NodeName = "", sz_CaseNo = "", sz_Node_ID = "", szProviderSign1 = "", szProviderSign2 = "";
    string sz_CaseID = "", sz_BillNo = "", PatientID = "", sz_speciality = "", CheckDC = "", tempPath = "", FileName = "";
    Bill_Sys_NF3_Template objNF3Template;
    Bill_Sys_SystemObject objSysObject;

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
        //this.Hp1.InnerHtml = b.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        sz_UserID = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();

        if (Request.QueryString["caseid"] != null)
        {
            sz_CaseID = Request.QueryString["caseid"].ToString();
            Session["HP1_Case"] = Request.QueryString["caseid"].ToString();
            sz_BillNo = Request.QueryString["billnumber"].ToString();
            sz_speciality = Request.QueryString["speciality"].ToString();
            sz_CaseNo = Request.QueryString["caseno"].ToString();
            txtbillno.Text = sz_BillNo;
            hfbillno.Value = sz_BillNo;
            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
            PatientID = _caseDetailsBO.GetCasePatientID(Request.QueryString["caseid"].ToString(), "");
        }
        if (!IsPostBack)
        {
            GetRecord();
        }
    }

    public void GetRecord()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Hp1DAL objHp1 = new Hp1DAL();

            DataSet ds = objHp1.GetHp1Record(sz_CompanyID, sz_CaseID, sz_BillNo);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtMHPName.Text = ds.Tables[0].Rows[0]["PROVIDER_NAME"].ToString();
                    txtMHPLine.Text = "";
                    txtMHPAddress.Text = ds.Tables[0].Rows[0]["PROVIDER_ADDR"].ToString();
                    txtMHPCity.Text = ds.Tables[0].Rows[0]["PROVIDER_CITY"].ToString();
                    txtMHPState.Text = ds.Tables[0].Rows[0]["PROVIDER_STATE"].ToString();
                    txtMHPZipCode.Text = ds.Tables[0].Rows[0]["PROVIDER_ZIP"].ToString();
                    txtWCBCaseNo.Text = ds.Tables[0].Rows[0]["WCB_CASE_NO"].ToString();
                    txtWCBAuthoNo.Text = ds.Tables[0].Rows[0]["WCB_AUTH_NO"].ToString();
                    txtProWCBNo.Text = ds.Tables[0].Rows[0]["WCB_RATING_CODE"].ToString();
                    txtProTelNo.Text = ds.Tables[0].Rows[0]["PROVIDER_PHONE"].ToString();
                    txtBHPName.Text = "";
                    txtBHPLine.Text = "";
                    txtBHPAddress.Text = "";
                    txtBHPCity.Text = "";
                    txtBHPState.Text = "";
                    txtBHPZipCode.Text = "";
                    txtCarrierCaseNo.Text = ds.Tables[0].Rows[0]["CARRIER_CASE_NO"].ToString();
                    txtCarrierEmpId.Text = ds.Tables[0].Rows[0]["CARRIER_EMPLOYER_NO"].ToString();
                    txtdataofaccident.Text = ds.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
                    txtMCName.Text = ds.Tables[0].Rows[0]["INSURANCE_NAME"].ToString();
                    txtMCLine.Text = ds.Tables[0].Rows[0]["INSURANCE_ADDRESS_LINE"].ToString();
                    txtMCAddress.Text = ds.Tables[0].Rows[0]["INSURANCE_ADDRESS"].ToString();
                    txtMCCity.Text = ds.Tables[0].Rows[0]["INSURANCE_CITY"].ToString();
                    txtCPState.Text = ds.Tables[0].Rows[0]["INSURANCE_STATE"].ToString();
                    txtMCZipCode.Text = ds.Tables[0].Rows[0]["INSURANCE_ZIP"].ToString();
                    txtCountry.Text = ds.Tables[0].Rows[0]["COUNTRY_OF_SERVICE"].ToString();
                    txtClaimantSSNo.Text = ds.Tables[0].Rows[0]["PATIENT_SOC_NO"].ToString();
                    txtClaimantName.Text = ds.Tables[0].Rows[0]["PATIENT_NAME"].ToString();
                    txtMEName.Text = ds.Tables[0].Rows[0]["EMPLOYER_NAME"].ToString();
                    txtMELine.Text = "";
                    txtMEAddress.Text = ds.Tables[0].Rows[0]["EMPLOYER_ADDR"].ToString();
                    txtMECity.Text = ds.Tables[0].Rows[0]["EMPLOYER_CITY"].ToString();
                    txtMEState.Text = ds.Tables[0].Rows[0]["EMPLOYER_STATE"].ToString();
                    txtMEZipCode.Text = ds.Tables[0].Rows[0]["EMPLOYER_ZIP"].ToString();

                    txtPatientAccNoSectionA.Text = ds.Tables[0].Rows[0]["PATIENT_ACCOUNT_NO"].ToString();
                    txtTotalChargeSectionA.Text = ds.Tables[0].Rows[0]["BILL_AMT"].ToString();
                    txtAmountPaidSectionA.Text = ds.Tables[0].Rows[0]["BILL_PAID"].ToString();
                    txtAmountDisputeSectionA.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();

                    txtFedaralTaxIdSectionB.Text = "";
                    txtPatientAccNoSectionB.Text = "";
                    txtTotalChargeSectionB.Text = "";
                    txtAmountPaidSectionB.Text = "";
                    txtAmountDisputeSectionB.Text = "";
                    txtAmountFreeSubmited.Text = "";
                    txtfirsttreatmentdate.Text = ds.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString();

                    txtDiagnosisRenderByC.Text = ds.Tables[0].Rows[0]["DIGNOSIS_BY_FIRST"].ToString();

                    txtYourDiagnosis.Text = ds.Tables[0].Rows[0]["YOUR_DIGNOSIS"].ToString();

                    txt_I_Date.Text = "";
                    txtCondition.Text = ds.Tables[0].Rows[0]["CONDITION_OF_PATIENT"].ToString();

                    //if (ds.Tables[0].Rows[0]["HEALTH_PROVIDER_SIGN_DT1"].ToString() != "" && ds.Tables[0].Rows[0]["HEALTH_PROVIDER_SIGN_DT1"].ToString() != null)
                    //{
                    //    lblmsg.Visible = true;
                    //    hyLinkOpenPDF.Visible = true;
                    //    lblmsg2.Visible = true;
                    //}

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows[0]["CHK_PROVIDER_A"].ToString() == "1")
                    {
                        chkARequestForAdmin.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_PROVIDER_B"].ToString() == "1")
                    {
                        chkBRequestForArbitration.Checked = true;
                    }
                    txtNoOfMedicalBills.Text = ds.Tables[1].Rows[0]["NO_OF_BILLS"].ToString();
                    txtFeeSubmitted.Text = ds.Tables[1].Rows[0]["FEE_SUBMITTED"].ToString();
                    txtCheckMONo.Text = ds.Tables[1].Rows[0]["CHK_MO_NUMBER"].ToString();
                    if (ds.Tables[1].Rows[0]["CHK_MEDICAL"].ToString() == "1")
                    {
                        chkmedical.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_OUT_HOSP"].ToString() == "1")
                    {
                        chkoutHos.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_IN_HOSP"].ToString() == "1")
                    {
                        chkInpHos.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_CHIRO"].ToString() == "1")
                    {
                        chkChiropractic.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_PHYSICAL"].ToString() == "1")
                    {
                        chkPhysicalTherapy.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_OCCU_THP"].ToString() == "1")
                    {
                        chkOccupationalTherapy.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_PSYCHOLOGY"].ToString() == "1")
                    {
                        chkPsychology.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_PODIATRY"].ToString() == "1")
                    {
                        chlPodiatry.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_OSTEPATHIC"].ToString() == "1")
                    {
                        chkOsteopathic.Checked = true;
                    }
                    szProviderSign1 = ds.Tables[1].Rows[0]["HEALTH_PROVIDER_SIGN1"].ToString();
                    txtProviderSign1.Text = ds.Tables[1].Rows[0]["HEALTH_PROVIDER_SIGN1"].ToString();
                    TxtDateofHelthProvider.Text = ds.Tables[1].Rows[0]["HEALTH_PROVIDER_SIGN_DT1"].ToString();
                    txtFedaralTaxIdSectionA.Text = ds.Tables[1].Rows[0]["FEDERAL_TX_ID_A"].ToString();
                    if (ds.Tables[1].Rows[0]["CHK_SSN_A"].ToString() == "1")
                    {
                        chkSSNSectionA.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_EIN_A"].ToString() == "1")
                    {
                        chkEINSectionA.Checked = true;
                    }
                    txtFedaralTaxIdSectionB.Text = ds.Tables[1].Rows[0]["FEDERAL_TX_ID_B"].ToString();
                    if (ds.Tables[1].Rows[0]["CHK_SSN_B"].ToString() == "1")
                    {
                        chkSSNSectionB.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_EIN_B"].ToString() == "1")
                    {
                        chkEINSectionB.Checked = true;
                    }
                    txtPatientAccNoSectionB.Text = ds.Tables[1].Rows[0]["PATIENT_ACC_SEC_B"].ToString();
                    txtTotalChargeSectionB.Text = ds.Tables[1].Rows[0]["TOTAL_CHARGE_B"].ToString();
                    txtAmountPaidSectionB.Text = ds.Tables[1].Rows[0]["AMT_PAID_B"].ToString();
                    txtAmountDisputeSectionB.Text = ds.Tables[1].Rows[0]["AMT_DISPUT_B"].ToString();
                    txtAmountFreeSubmited.Text = ds.Tables[1].Rows[0]["AMT_FEE_SUB_B"].ToString();
                    txtArbitrationFees.Text = ds.Tables[1].Rows[0]["ARBITRATION_FEES"].ToString();
                    if (ds.Tables[1].Rows[0]["CHK_FIRST_TREAT_YES"].ToString() == "1")
                    {
                        chkFirstTreatmentYes.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_FIRST_TREAT_NO"].ToString() == "1")
                    {
                        chkFirstTreatmentNo.Checked = true;
                    }
                    txtPFirstTName.Text = ds.Tables[1].Rows[0]["FIRST_PROVIDER_NAME"].ToString();
                    txtPFirstTLine.Text = ds.Tables[1].Rows[0]["FIRST_PROVIDER_LINE"].ToString();
                    txtPFirstTAddress.Text = ds.Tables[1].Rows[0]["FIRST_PROVIDER_ADDR"].ToString();
                    txtPFirstTCity.Text = ds.Tables[1].Rows[0]["FIRST_PROVIDER_CITY"].ToString();
                    txtPFirstTState.Text = ds.Tables[1].Rows[0]["FIRST_PROVIDER_STATE"].ToString();
                    txtPFirstTZipCode.Text = ds.Tables[1].Rows[0]["FIRST_PROVIDER_ZIP"].ToString();
                    txtTreatementByC.Text = ds.Tables[1].Rows[0]["FIRST_PROVIDER_TREAT"].ToString();
                    txtdateproviderinitemc.Text = ds.Tables[1].Rows[0]["DT_DISCHARGE"].ToString();
                    txtTotalNoofTreatement.Text = ds.Tables[1].Rows[0]["NO_OF_TREATMENTS"].ToString();
                    if (ds.Tables[1].Rows[0]["CHK_TREAT_YES"].ToString() == "1")
                    {
                        chkAreYouStillReadingYes.Checked = true;
                    }
                    if (ds.Tables[1].Rows[0]["CHK_TREAT_NO"].ToString() == "1")
                    {
                        chkAreYouStillReadingNo.Checked = true;
                    }
                    txtJTotalAmount.Text = ds.Tables[1].Rows[0]["TOTAL_DISPUT_BILL_AMT"].ToString();
                    txtKAmountPreviously.Text = ds.Tables[1].Rows[0]["AMT_PAID_ON_CASE"].ToString();
                    txtLAmountPreviasly.Text = ds.Tables[1].Rows[0]["AMT_PAID_ON_DISPUT_BILLS"].ToString();
                    szProviderSign2= ds.Tables[1].Rows[0]["HEALTH_PROVIDER_SIGN2"].ToString();
                    txtProviderSign2.Text = ds.Tables[1].Rows[0]["HEALTH_PROVIDER_SIGN2"].ToString();
                    txthelthproviderdate.Text = ds.Tables[1].Rows[0]["HEALTH_PROVIDER_SIGN_DT2"].ToString();
                    if (ds.Tables[1].Rows[0]["PDF_EXIST"].ToString() != "" && ds.Tables[1].Rows[0]["PDF_EXIST"].ToString() != null)
                    {
                        string oldfilepath = ds.Tables[1].Rows[0]["PDF_EXIST"].ToString().Replace("\\", "/");
                        lblmsg.Visible = true;
                        lblmsg2.Visible = true;
                        hyLinkOpenPDF.Visible = true;
                        string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
                        //hyLinkOpenPDF.NavigateUrl = "http://localhost/MBSScans/" + filepath;
                        //hyLinkOpenPDF.NavigateUrl = "https://www.gogreenbills.com/MBSScans/"+filepath;
                        hyLinkOpenPDF.NavigateUrl = ds.Tables[1].Rows[0]["DocumentManagerURL"].ToString().Replace("\\", "/") + "/" + oldfilepath;
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

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //private void PrintPDF()
    //{

    //    try
    //    {


    //        Hp1DAL objHP1 = new Hp1DAL();
    //        CmpName = objHP1.GetPDFPath(sz_CompanyID, sz_CaseID, sz_speciality);

    //        GenerateHp1Pdf objHP1PDF = new GenerateHp1Pdf();

    //        Logicalpath = "Hp1_" + sz_BillNo + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff");

    //        tempPath = objHP1PDF.generateHP1(sz_CompanyID, sz_CaseID, sz_BillNo, CmpName, Logicalpath);

    //        //SaveLogicalPath();
    //        string BillPath = CmpName + "\\" + Logicalpath + ".pdf";
    //        string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
    //        string newpath = str + "/" + BillPath;
    //        newpath = newpath.Replace("\\", "/");
    //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + newpath + "');", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        //usrMessage.PutMessage(ex.ToString());
    //        //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
    //        //usrMessage.Show();
    //    }
    //    finally
    //    {

    //    }

    //}

    private void FindNodeType()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            Hp1DAL objHp1 = new Hp1DAL();
            DataTable dt = objHp1.FindNode(sz_CompanyID, sz_speciality);

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

    protected void btnsave_Click(object sender, EventArgs e)
    {
        insertRecord();
    }

    protected void btnSaveBottom_Click(object sender, EventArgs e)
    {
        insertRecord();
    }

    public void insertRecord()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Hp1DAO objDAO = new Hp1DAO();
            objDAO = BindObject();

            Hp1DAL objHp1 = new Hp1DAL();
            objHp1.SaveHP1(objDAO);



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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Hp1DAL objHp1 = new Hp1DAL();
            objSysObject = new Bill_Sys_SystemObject();
            string signNotRequired = (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_HP1_SIGN).ToString();
            string flg = objHp1.HP1CheckExist(sz_CompanyID, sz_CaseID, sz_BillNo);
            if (flg == "1")
            {
                if (signNotRequired == "1")
                {
                    PrintPDF();
                }
                else
                {
                    if (txtProviderSign1.Text.ToString() != "" && txtProviderSign2.Text.ToString() != "")
                    {
                        PrintPDF();
                    }
                    else
                    {
                        usrMessage.PutMessage("Please Save Provider Signs.");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                        usrMessage.Show();
                    }
                }
            }
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnPrinBottom_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Hp1DAL objHp1 = new Hp1DAL();
            string signNotRequired = (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_HP1_SIGN).ToString();
            string flg = objHp1.HP1CheckExist(sz_CompanyID, sz_CaseID, sz_BillNo);
            if (flg == "1")
            {
                if (signNotRequired == "1")
                {
                    PrintPDF();
                }
                else
                {
                    if (txtProviderSign1.Text.ToString() != "" && txtProviderSign2.Text.ToString() != "")
                    {
                        PrintPDF();
                    }
                    else
                    {
                        usrMessage.PutMessage("Please Save Provider Signs.");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                        usrMessage.Show();
                    }
                }
            }
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void PrintPDF()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Hp1DAO objDAO = new Hp1DAO();
            objDAO = BindObject();

            Hp1DAL objHp1 = new Hp1DAL();

            CmpName = objHp1.GetPDFPath(sz_CompanyID, sz_CaseID, sz_speciality);

            GenerateHp1Pdf objpdf = new GenerateHp1Pdf();

            FileName = "HP1_" + sz_BillNo + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff");

            tempPath = objpdf.generateHP1(sz_CompanyID, sz_CaseID, sz_BillNo, CmpName, FileName, objDAO);

            SaveLogicalPath();
            string BillPath = CmpName + "\\" + FileName + ".pdf";
            string str = HP1.ApplicationSettings.GetParameterValue("DocumentManagerURL");
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

    private void SaveLogicalPath()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        FindNodeType();

        try
        {
            //string strGenFileName = sz_BillNo + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff")+"MG2.pdf";
            string BillPath = CmpName + "\\" + FileName + ".pdf";

            string sz_UserName = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_NAME.ToString();
            Hp1DAL objHp1 = new Hp1DAL();
            objHp1.SaveLogicalPath(sz_BillNo, BillPath);

            objHp1.SaveUploadDocumentHP1(sz_CaseID, sz_CompanyID, FileName + ".pdf", BillPath, sz_NodeName, sz_UserName, sz_UserID, sz_Node_ID);


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

    public Hp1DAO BindObject()
    {
        Hp1DAL objHp1 = new Hp1DAL();
        DataTable dt=  objHp1.GetSignPaths(sz_CompanyID, sz_CaseID,sz_BillNo);
        if (dt.Rows.Count > 0)
        {
            txtProviderSign1.Text = dt.Rows[0]["HEALTH_PROVIDER_SIGN1"].ToString();
            txtProviderSign2.Text = dt.Rows[0]["HEALTH_PROVIDER_SIGN2"].ToString();
        }

        Bill_Sys_NF3_Template objNF3Template= new Bill_Sys_NF3_Template();
        Hp1DAO objDAO = new Hp1DAO();

        objDAO.billNo = sz_BillNo;
        objDAO.CompanyID = sz_CompanyID;
        objDAO.CaseID = sz_CaseID;
        objDAO.patientID = PatientID;
        objDAO.UserID = sz_UserID;

        if (chkARequestForAdmin.Checked)
        {
            objDAO.chkProviderA = "1";
        }
        else
        {
            objDAO.chkProviderA = "0";
        }
        if (chkBRequestForArbitration.Checked)
        {
            objDAO.chkProviderB = "1";
        }
        else
        {
            objDAO.chkProviderB = "0";
        }
        objDAO.noOfBillsAttached = txtNoOfMedicalBills.Text.ToString();
        objDAO.feesSubmitted = txtFeeSubmitted.Text.ToString();
        objDAO.chkMoNumber = txtCheckMONo.Text.ToString();
        if (chkmedical.Checked)
        {
            objDAO.chkMedical = "1";
        }
        else
        {
            objDAO.chkMedical = "0";
        }
        if (chkoutHos.Checked)
        {
            objDAO.chkOutPatHospital = "1";
        }
        else
        {
            objDAO.chkOutPatHospital = "0";
        }
        if (chkInpHos.Checked)
        {
            objDAO.chkInPatient = "1";
        }
        else
        {
            objDAO.chkInPatient = "0";
        }
        if (chkChiropractic.Checked)
        {
            objDAO.chkChiropractic = "1";
        }
        else
        {
            objDAO.chkChiropractic = "0";
        }
        if (chkPhysicalTherapy.Checked)
        {
            objDAO.chkPhysicalTherepy = "1";
        }
        else
        {
            objDAO.chkPhysicalTherepy = "0";
        }
        if (chkOccupationalTherapy.Checked)
        {
            objDAO.chkOcupational = "1";
        }
        else
        {
            objDAO.chkOcupational = "0";
        }
        if (chkPsychology.Checked)
        {
            objDAO.chkPsychology = "1";
        }
        else
        {
            objDAO.chkPsychology = "0";
        }
        if (chlPodiatry.Checked)
        {
            objDAO.chkPodiatry = "1";
        }
        else
        {
            objDAO.chkPodiatry = "0";
        }
        if (chkOsteopathic.Checked)
        {
            objDAO.chkOstepathic = "1";
        }
        else
        {
            objDAO.chkOstepathic = "0";
        }

        objDAO.healthProviderName = txtMHPName.Text.ToString();
        objDAO.healthProviderLine = txtMHPLine.Text.ToString();
        objDAO.healthProviderAddr = txtMHPAddress.Text.ToString();
        objDAO.healthProviderCity = txtMHPCity.Text.ToString();
        objDAO.healthProviderState = txtMHPState.Text.ToString();
        objDAO.healthProviderZip = txtMHPZipCode.Text.ToString();
        objDAO.WCBCaseNumber = txtWCBCaseNo.Text.ToString();
        objDAO.WCBAuthorizationNumber = txtWCBAuthoNo.Text.ToString();
        objDAO.WCBRatingCode = txtProWCBNo.Text.ToString();
        objDAO.providerPhone = txtProTelNo.Text.ToString();
        objDAO.healthProviderName2 = txtBHPName.Text.ToString();
        objDAO.healthProviderLine2 = txtBHPLine.Text.ToString();
        objDAO.healthProviderAddr2 = txtBHPAddress.Text.ToString();
        objDAO.healthProviderCity2 = txtBHPCity.Text.ToString();
        objDAO.healthProviderState2 = txtBHPState.Text.ToString();
        objDAO.healthProviderZip2 = txtBHPZipCode.Text.ToString();
        objDAO.carrierCaseNumber = txtCarrierCaseNo.Text.ToString();
        objDAO.employerID = txtCarrierEmpId.Text.ToString();
        objDAO.accidentDate = txtdataofaccident.Text.ToString();
        objDAO.carrier = txtMCName.Text.ToString();
        objDAO.carrierLine = txtMCLine.Text.ToString();
        objDAO.carrierAddr = txtMCAddress.Text.ToString();
        objDAO.carrierCity = txtMCCity.Text.ToString();
        objDAO.carrierState = txtCPState.Text.ToString();
        objDAO.carrierZip = txtMCZipCode.Text.ToString();
        objDAO.serviceCountry = txtCountry.Text.ToString();
        objDAO.socialSecurityNumber = txtClaimantSSNo.Text.ToString();
        objDAO.claimantName = txtClaimantName.Text.ToString();
        objDAO.employerName = txtMEName.Text.ToString();
        objDAO.employerAddr = txtMEAddress.Text.ToString();
        objDAO.employerLine = txtMELine.Text.ToString();
        objDAO.employerCity = txtMECity.Text.ToString();
        objDAO.employerState = txtMEState.Text.ToString();
        objDAO.employerZip = txtMEZipCode.Text.ToString();
        objDAO.healthProviderSignDate = TxtDateofHelthProvider.Text.ToString();
        string healthProviderSignPath = "";
        if (txtProviderSign1.Text.ToString() != "")
        {
            healthProviderSignPath = (objNF3Template.getPhysicalPath()) + txtProviderSign1.Text.ToString();
        }
       
        objDAO.healthProviderSign = healthProviderSignPath;
        if (chkSSNSectionA.Checked)
        {
            objDAO.chkSSN = "1";
        }
        else
        {
            objDAO.chkSSN = "0";
        }
        if (chkEINSectionA.Checked)
        {
            objDAO.chkEIN = "1";
        }
        else
        {
            objDAO.chkEIN = "0";
        }
        objDAO.federalTaxNumber = txtFedaralTaxIdSectionA.Text.ToString();
        objDAO.patientAccountNumber = txtPatientAccNoSectionA.Text.ToString();
        objDAO.totalCharge = txtTotalChargeSectionA.Text.ToString();
        objDAO.amountPaid = txtAmountPaidSectionA.Text.ToString();
        objDAO.amountDispute = txtAmountDisputeSectionA.Text.ToString();
        objDAO.federalTaxNumberB = txtFedaralTaxIdSectionB.Text.ToString();
        if (chkSSNSectionB.Checked)
        {
            objDAO.chkSSNB = "1";
        }
        else
        {
            objDAO.chkSSNB = "0";
        }
        if (chkEINSectionB.Checked)
        {
            objDAO.chkEINB = "1";
        }
        else
        {
            objDAO.chkEINB = "0";
        }
        objDAO.patientAccountNumberB = txtPatientAccNoSectionB.Text.ToString();
        objDAO.totalChargeB = txtTotalChargeSectionB.Text.ToString();
        objDAO.amountPaidB = txtAmountPaidSectionB.Text.ToString();
        objDAO.amountDisputeB = txtAmountDisputeSectionB.Text.ToString();
        objDAO.amountOfFeeSubmittedB = txtAmountFreeSubmited.Text.ToString();
        objDAO.firstTreatmentDate = txtfirsttreatmentdate.Text.ToString();
        objDAO.arbitrationFees = txtArbitrationFees.Text.ToString();
        if (chkFirstTreatmentYes.Checked)
        {
            objDAO.chkTreatmentRendered = "1";
        }
        else
        {
            objDAO.chkTreatmentRendered = "0";
        }
        if (chkFirstTreatmentNo.Checked)
        {
            objDAO.chkTreatmentRenderedno = "1";
        }
        else
        {
            objDAO.chkTreatmentRenderedno = "0";
        }
        //chkTreatmentRenderedno
        objDAO.firstTreatmentProvider = txtPFirstTName.Text.ToString();
        objDAO.firstTreatmentProviderLine = txtPFirstTLine.Text.ToString();
        objDAO.firstTreatmentProviderAddr = txtPFirstTAddress.Text.ToString();
        objDAO.firstTreatmentProviderCity = txtPFirstTCity.Text.ToString();
        objDAO.firstTreatmentProviderState = txtPFirstTState.Text.ToString();
        objDAO.firstTreatmentProviderZip = txtPFirstTZipCode.Text.ToString();
        objDAO.firstTreatmentProviderDiagnosis = txtDiagnosisRenderByC.Text.ToString();
        objDAO.firstTreatmentProviderTreatment = txtTreatementByC.Text.ToString();
        objDAO.lastTreatmentDate = txtdateproviderinitemc.Text.ToString();
        objDAO.newDiagnosis = txtYourDiagnosis.Text.ToString();
        objDAO.totalTreatmentNos = txtTotalNoofTreatement.Text.ToString();
        if (chkAreYouStillReadingYes.Checked)
        {
            objDAO.chkTreatingClaimant = "1";
        }
        else
        {
            objDAO.chkTreatingClaimant = "0";
        }
        if (chkAreYouStillReadingNo.Checked)
        {
            objDAO.chkTreatingClaimantno = "1";
        }
        else
        {
            objDAO.chkTreatingClaimantno = "0";
        }

        objDAO.dischargedDate = txt_I_Date.Text.ToString();
        objDAO.condition = txtCondition.Text.ToString();
        objDAO.totalBillAmount = txtJTotalAmount.Text.ToString();
        objDAO.casePaidAmount = txtKAmountPreviously.Text.ToString();
        objDAO.disputBillPaidAmount = txtLAmountPreviasly.Text.ToString();
        string healthProviderSignBPath = "";
        if (txtProviderSign2.Text.ToString() != "")
        {
            healthProviderSignBPath = (objNF3Template.getPhysicalPath()) + txtProviderSign2.Text.ToString();
        }
        objDAO.healthProviderSignB = healthProviderSignBPath;
        objDAO.healthProviderSignDateB = txthelthproviderdate.Text.ToString();

        return objDAO;
    }
}

