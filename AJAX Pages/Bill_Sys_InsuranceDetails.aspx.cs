using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using log4net;
using mbs.LienBills;
using UB4ValueRelacement;
using System.Xml.Xsl;


public partial class AJAX_Pages_Bill_Sys_InsuranceDetails : PageBase
{
    #region "Local Variable"

    //private SaveOperation _saveOperation;
    //private EditOperation _editOperation;
    //private ListOperation _listOperation;
    private Bill_Sys_Menu _bill_Sys_Menu;
    private Bill_Sys_InsertDefaultValues objDefaultValue;
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    CaseDetailsBO objCaseDetailsBO;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    Bill_Sys_NF3_Template objNF3Template;
    Bill_Sys_DigosisCodeBO objDiagCodeBO;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private static ILog log = LogManager.GetLogger("Bill_Sys_BillTransaction");
    private Bill_Sys_LoginBO _bill_Sys_LoginBO;
    private Bill_Sys_NF3_Template _bill_Sys_NF3_Template;
    Bill_Sys_Verification_Desc objVerification_Desc;
    private string pdfpath;
    string bt_include;
    String str_1500;
    MUVGenerateFunction _MUVGenerateFunction;
    string billid = "";
    string Bill_Number = "";
    string Speciality = "";
    string SpecilaityID = "";

    private const int I_COL_GRID_COMPLETED_VISITS_ADDED_BY_DOCTOR = 12;
    private const int I_COL_GRID_COMPLETED_VISITS_FINALIZED = 13;
    private const int I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER = 15;
    private Bill_Sys_SystemObject objSystemObject;

    //bool flag = true;
    
    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (Request.QueryString["billid"].ToString().ToLower() != "undefined")
            Bill_Number = Request.QueryString["billid"].ToString();
        if (Request.QueryString["speciality"].ToString().ToLower() != "undefined")
            Speciality = Request.QueryString["speciality"].ToString();
        if (Request.QueryString["specialityid"].ToString().ToLower() != "undefined")
            SpecilaityID = Request.QueryString["specialityid"].ToString();
        if (!Page.IsPostBack)
        {
            try
            {
                BillTransactionDAO insObj = new BillTransactionDAO();
                string caseid = Request.QueryString["caseid"].ToString();
                string flag = Request.QueryString["flag"].ToString();
                string casetype = "";
                if (Bill_Number != "" && Bill_Number != null)
                {
                    Session["TM_SZ_BILL_ID"] = Bill_Number;
                    objCaseDetailsBO = new CaseDetailsBO();
                    casetype = objCaseDetailsBO.GetCaseType(Bill_Number);
                    if (casetype == "WC000000000000000002") { rdlbillType.SelectedValue = "WC000000000000000002"; }
                    else if (casetype == "WC000000000000000003") { rdlbillType.SelectedValue = "WC000000000000000003"; }
                    else if (casetype == "WC000000000000000004") { rdlbillType.SelectedValue = "WC000000000000000004"; }
                    else if (casetype == "WC000000000000000001") { rdlbillType.SelectedValue = "WC000000000000000001_1"; }
                    bool SecondBillExist = false;
                    SecondBillExist = insObj.checkBillExist(Bill_Number);
                    if (SecondBillExist)
                    {
                        hdnSecondExist.Value = "1";
                    }
                }

                
                DataSet dsInsuranceDetails = new DataSet();
                dsInsuranceDetails = insObj.GetInsuranceDetails(caseid);
                if (rdlBill.SelectedValue == "Primary")
                {
                    Session["GenerateBill_insType"] = "Primary";
                }
                else
                {
                    Session["GenerateBill_insType"] = "Secondary";
                }

                if (dsInsuranceDetails.Tables.Count > 0)
                {

                    #region test export excel dataset
                    convert(dsInsuranceDetails);
                    #endregion
                    if (dsInsuranceDetails.Tables[0].Rows.Count > 0)
                    {
                        txtPriInsName.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                        txtPriInsCity.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                        extdlPriInsState.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_INSURANCE_STATE_ID"].ToString();
                        txtPriInsAddress.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                        txtPriInsZip.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();

                        if (extdlPriInsState.Text != "NA" && extdlPriInsState.Text != "")
                            txtPriInsStateExtd.Text = extdlPriInsState.Selected_Text;

                        txtPriPolicyNo.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_POLICY_NO"].ToString();
                        txtPriPolicyHolder.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString();
                        txtPriPolicyHolderAddr.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ADDRESS"].ToString();
                        txtPriPolicyCity.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_POLICY_HOLDER_CITY"].ToString();
                        txtPriPolicyZip.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ZIP"].ToString();
                        txtPriPolicyPh.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE"].ToString();
                        extdlPriPolicyState.Text = dsInsuranceDetails.Tables[0].Rows[0]["SZ_POLICY_HOLDER_STATE_ID"].ToString();

                        if (extdlPriPolicyState.Text != "NA" && extdlPriPolicyState.Text != "")
                            txtPriPolicyState.Text = extdlPriPolicyState.Selected_Text;

                        string rel = dsInsuranceDetails.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString();
                        if (rel != null && rel != "")
                        {
                            if (rel == "1")
                                rdlPriPolicyRelation.SelectedValue = "1";
                            else if (rel == "2")
                                rdlPriPolicyRelation.SelectedValue = "2";
                            else if (rel == "3")
                                rdlPriPolicyRelation.SelectedValue = "3";
                            else if (rel == "4")
                                rdlPriPolicyRelation.SelectedValue = "4";
                        }
                    }

                    if (dsInsuranceDetails.Tables[1].Rows.Count > 0)
                    {
                        txtSecInsName.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                        txtSecInsCity.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                        txtSecInsAddress.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                        txtSecInsZip.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_INSURANCE_ZIP"].ToString();

                        extdlSecInsState.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_INSURANCE_STATE_ID"].ToString();

                        if (extdlSecInsState.Text != "NA" && extdlSecInsState.Text != "")
                            txtSecInsStateExtd.Text = extdlSecInsState.Selected_Text;

                        txtSecPolicyHolder.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_POLICY_HOLDER_NAME"].ToString();
                        txtSecPolicyAddr.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_POLICY_HOLDER_ADDRESS"].ToString();
                        txtSecPolicyCity.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_POLICY_HOLDER_CITY"].ToString();
                        extdlSecPolicyState.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_POLICY_HOLDER_STATE"].ToString();
                        txtSecPolicyZip.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_POLICY_HOLDER_ZIP"].ToString();
                        txtSecPolicyPh.Text = dsInsuranceDetails.Tables[1].Rows[0]["SZ_POLICY_HOLDER_PHONE"].ToString();

                        if (extdlSecPolicyState.Text != "NA" && extdlSecPolicyState.Text != "")
                            txtSecPolicyState.Text = extdlSecPolicyState.Selected_Text;

                        string rel = dsInsuranceDetails.Tables[1].Rows[0]["SZ_POLICY_HOLDER_RELATION"].ToString();
                        if (rel != null && rel != "")
                        {
                            if (rel == "1")
                                rdlSecPolicyRelation.SelectedValue = "1";
                            else if (rel == "2")
                                rdlSecPolicyRelation.SelectedValue = "2";
                            else if (rel == "3")
                                rdlSecPolicyRelation.SelectedValue = "3";
                            else if (rel == "4")
                                rdlSecPolicyRelation.SelectedValue = "4";
                        }

                    }
                }

                if (flag == "generate")
                {
                    btnRegenerateBill.Visible = true;
                    btnGenerateSecondBill.Visible = true;
                    btnGenerateBill.Visible = false;
                }
                else
                {
                    btnRegenerateBill.Visible = false;
                    btnGenerateSecondBill.Visible = false;
                    btnGenerateBill.Visible = true;
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
        if (hdnSecondExist.Value == "1")
        {
            btnGenerateSecondBill.Enabled = false;

            //Method End
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
    }

    protected void btnGenerateBill_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //debug & check ddlType.Text 
            string str = "";
            string PathOpenPdf = "";
            
            string companyID = "";
            string noofTransaction = Session["GenerateBill_NoOfTransaction"].ToString();
            int num;
            try
            {
                num = Convert.ToInt16(noofTransaction);
            }
            catch
            {
                num = 0;
            }


            BillTransactionEO neo = new BillTransactionEO();
            neo.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            neo.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            neo.DT_BILL_DATE = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            neo.SZ_DOCTOR_ID = Session["GenerateBill_BillNo"].ToString();
            neo.SZ_TYPE = "0";
            neo.SZ_TESTTYPE = "";
            neo.FLAG = "ADD";
            neo.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            companyID = neo.SZ_COMPANY_ID;

            this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
            
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            ArrayList list3 = new ArrayList();
            ArrayList list4 = new ArrayList();

            list = (ArrayList)Session["GenerateBill_ProcedureCodes1"];
            list2 = (ArrayList)Session["GenerateBill_ProcedureCodes2"];
            list3 = (ArrayList)Session["GenerateBill_ProcedureCodes3"];
            list4 = (ArrayList)Session["GenerateBill_DiagnosticCodes"];
            if (rdlBill.SelectedValue == "Primary")
            {
                Session["GenerateBill_insType"] = "Primary";
            }
            else
            {
                Session["GenerateBill_insType"] = "Secondary";
            }

            BillTransactionDAO ndao = new BillTransactionDAO();
            Result result = new Result();
            result = ndao.SaveBillTransactions(neo, list, list2, list3, list4);
            

            if (result.msg_code == "ERR")
            {
                this.usrMessage.PutMessage(result.msg);
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            else
            {
                billid = result.bill_no;
                str = billid;
                hdBillNumber.Value = str;

                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str;

                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = companyID;
                this._DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                
                CaseDetailsBO objCaseDetailsBO = new CaseDetailsBO();
                string patientID = objCaseDetailsBO.GetPatientID(str);

                string billType = rdlbillType.SelectedValue.ToString();
                
                #region WC XML
                //if (objCaseDetailsBO.GetCaseType(str) == "WC000000000000000001")
                if (billType.Contains("WC000000000000000001"))
                {
                    this.objDefaultValue = new Bill_Sys_InsertDefaultValues();
                    if (num == 0)
                    {
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_DoctorOpinion.xml"), companyID, null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_ExamInformation.xml"), companyID, null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_History.xml"), companyID, null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_PlanOfCare.xml"), companyID, null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_WorkStatus.xml"), companyID, null, patientID);
                    }
                    else if (num >= 1)
                    {
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_DoctorsOpinionC4_2.xml"), companyID, str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_ExaminationTreatment.xml"), companyID, str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_PermanentImpairment.xml"), companyID, str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_WorkStatusC4_2.xml"), companyID, str, patientID);
                    }
                }
                #endregion

                this.usrMessage.PutMessage(" Bill Saved successfully ! ");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();

                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                
                
                #region NF3
                //if (objCaseDetailsBO.GetCaseType(str) == "WC000000000000000002")
                if (rdlbillType.SelectedValue == "WC000000000000000002")
                {
                    this.GenerateAddedBillPDF(str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                #endregion

                #region PVT
                //if (objCaseDetailsBO.GetCaseType(str) == "WC000000000000000003")
                if (rdlbillType.SelectedValue == "WC000000000000000003")
                {
                    this.GenerateAddedBillPDF(str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                #endregion                    

                #region WC
                //else if (objCaseDetailsBO.GetCaseType(str) == "WC000000000000000001")
                else if (billType.Contains("WC000000000000000001"))
                {
                    //this.hdnWCPDFBillNumber.Value = str.ToString();
                    //this.pnlPDFWorkerCompAdd.Visible = true;
                    //this.pnlPDFWorkerCompAdd.Width = Unit.Pixel(100);
                    //this.pnlPDFWorkerCompAdd.Height = Unit.Pixel(100);
                    try
                    {
                        string p_szPDFNo = "1";
                        if (billType == "WC000000000000000001_1") { p_szPDFNo = "1"; }
                        else if (billType == "WC000000000000000001_2") { p_szPDFNo = "2"; }
                        else if (billType == "WC000000000000000001_3") { p_szPDFNo = "3"; }
                        else if (billType == "WC000000000000000001_4") { p_szPDFNo = "4"; }
                        else if (billType == "WC000000000000000001_5") { p_szPDFNo = "5"; }
                        else if (billType == "WC000000000000000001_6") { p_szPDFNo = "6"; }

                        string usrID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        string str2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                        string str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        string CaseId = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        string str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        string str5 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                        WC_Bill_Generation generation = new WC_Bill_Generation();
                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        //ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + generation.GeneratePDFForWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.rdbListPDFType.SelectedValue, str4, str3, str, str2, str5, this._bill_Sys_BillTransaction.GetDoctorSpeciality(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), 0) + "');", true);

                        if (p_szPDFNo == "6")
                        {
                            PathOpenPdf = generation.GeneratePDFForReferalWorkerComp(str, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID,  str4, str3, usrID, str2, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                        }
                        else
                        {
                            PathOpenPdf = generation.GeneratePDFForWorkerComp(str, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, p_szPDFNo, str4, str3, usrID, str2, str5, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), 0);
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
                #endregion

                #region Lien
                //if (objCaseDetailsBO.GetCaseType(str) == "WC000000000000000004")
                if (rdlbillType.SelectedValue == "WC000000000000000004")
                {
                    string str6;
                    this.objNF3Template = new Bill_Sys_NF3_Template();
                    Lien lien = new Lien();
                    this._MUVGenerateFunction = new MUVGenerateFunction();
                    string doctorSpeciality = this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    string str9 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.Session["TM_SZ_BILL_ID"] = str;
                    this.objVerification_Desc = new Bill_Sys_Verification_Desc();
                    this.objVerification_Desc.sz_bill_no = str;
                    this.objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.objVerification_Desc.sz_flag = "BILL";
                    ArrayList list5 = new ArrayList();
                    ArrayList list6 = new ArrayList();
                    string str10 = "";
                    string str11 = "";
                    list5.Add(this.objVerification_Desc);
                    list6 = this._bill_Sys_BillTransaction.Get_Node_Type(list5);
                    if (list6.Contains("NFVER"))
                    {
                        str10 = "OLD";
                        str11 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + doctorSpeciality + "/";
                    }
                    else
                    {
                        str10 = "NEW";
                        str11 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + doctorSpeciality + "/Bills/";
                    }
                    string str12 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    objCaseDetailsBO = new CaseDetailsBO();
                    DataSet set = new DataSet();
                    string str13 = "";
                    set = objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.Session["TM_SZ_BILL_ID"].ToString());
                    if (set.Tables[0].Rows.Count > 0)
                    {
                        for (int k = 0; k < set.Tables[0].Rows.Count; k++)
                        {
                            str13 = set.Tables[0].Rows[k]["BT_1500_FORM"].ToString();
                        }
                    }
                    if (str13 == "1")
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                        if (File.Exists(this.objNF3Template.getPhysicalPath() + str12 + this.str_1500))
                        {
                            if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str11))
                            {
                                Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str11);
                            }
                            File.Copy(this.objNF3Template.getPhysicalPath() + str12 + this.str_1500, this.objNF3Template.getPhysicalPath() + str11 + this.str_1500);
                        }
                        str6 = System.Configuration.ConfigurationManager.AppSettings["DocumentManagerURL"].ToString() + str11 + this.str_1500;
                        ArrayList list7 = new ArrayList();
                        if (str10 == "OLD")
                        {
                            list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list7.Add(str11 + this.str_1500);
                            list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list7.Add(this.Session["TM_SZ_CASE_ID"]);
                            list7.Add(this.str_1500);
                            list7.Add(str11);
                            list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list7.Add(doctorSpeciality);
                            list7.Add("LN");
                            list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(list7);
                        }
                        else
                        {
                            list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list7.Add(str11 + this.str_1500);
                            list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list7.Add(this.Session["TM_SZ_CASE_ID"]);
                            list7.Add(this.str_1500);
                            list7.Add(str11);
                            list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list7.Add(doctorSpeciality);
                            list7.Add("LN");
                            list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            list7.Add(list6[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(list7);
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                    else
                    {
                        string str8 = this._MUVGenerateFunction.get_bt_include(str9, doctorSpeciality, "", "Speciality");
                        string str7 = this._MUVGenerateFunction.get_bt_include(str9, "", "WC000000000000000004", "CaseType");
                        if ((str8 == "True") && (str7 == "True"))
                        {
                            string str14 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                            string str15 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            string str16 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                            this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                            MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str11 + lien.GenratePdfForLienWithMuv(str9, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str16, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str15), this.objNF3Template.getPhysicalPath() + str14 + this.str_1500, this.objNF3Template.getPhysicalPath() + str11 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            str6 = System.Configuration.ConfigurationManager.AppSettings["DocumentManagerURL"].ToString() + str11 + this.str_1500.Replace(".pdf", "_MER.pdf");
                            ArrayList list8 = new ArrayList();
                            if (str10 == "OLD")
                            {
                                list8.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list8.Add(str11 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list8.Add(this.Session["TM_SZ_CASE_ID"]);
                                list8.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(str11);
                                list8.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list8.Add(doctorSpeciality);
                                list8.Add("LN");
                                list8.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                this.objNF3Template.saveGeneratedBillPath(list8);
                            }
                            else
                            {
                                list8.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list8.Add(str11 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list8.Add(this.Session["TM_SZ_CASE_ID"]);
                                list8.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(str11);
                                list8.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list8.Add(doctorSpeciality);
                                list8.Add("LN");
                                list8.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                list8.Add(list6[0].ToString());
                                this.objNF3Template.saveGeneratedBillPath_New(list8);
                            }
                            this._DAO_NOTES_EO = new DAO_NOTES_EO();
                            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                            this._DAO_NOTES_BO = new DAO_NOTES_BO();
                            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                            this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        }
                        else
                        {
                            str6 = lien.GenratePdfForLien(companyID, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                        }
                    }
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + str6 + "');", true);
                }
                #endregion

                #region UB4

                if (rdlbillType.SelectedValue == "WC000000000000000005")
                {
                    string ub4str = "";
                    string pdfFileName = System.Configuration.ConfigurationManager.AppSettings["UB4_PDF_FILE"].ToString();

                    UB4DAO objub4dao = new UB4DAO();
                    objub4dao.pdfFileName = pdfFileName;
                    objub4dao.szCaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    objub4dao.szCompID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    objub4dao.szCompName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    objub4dao.szUserName = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    objub4dao.szUserID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    objub4dao.billNumber = hdBillNumber.Value.ToString();
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    objub4dao.szSpecilaity = this._bill_Sys_BillTransaction.GetDoctorSpeciality(hdBillNumber.Value.ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                    UB4ValueRelacement.UB4ValuReplacement objub4 = new UB4ValueRelacement.UB4ValuReplacement();
                    if (rdlBill.SelectedValue == "Primary")
                        ub4str = objub4.ReplacePDFvalues(objub4dao, "Primary");
                    else if (rdlBill.SelectedValue == "Secondary")
                        ub4str = objub4.ReplacePDFvalues(objub4dao, "Secondary");
                    PathOpenPdf = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + ub4str;
                    
                }

                #endregion


                if (PathOpenPdf != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + PathOpenPdf + "');", true);
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

    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._MUVGenerateFunction = new MUVGenerateFunction();
            string str = p_szSpeciality;
            this.Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this.Session["TM_SZ_BILL_ID"] = p_szBillNumber;
            this.objNF3Template = new Bill_Sys_NF3_Template();
            this.objVerification_Desc = new Bill_Sys_Verification_Desc();
            this.objVerification_Desc.sz_bill_no = p_szBillNumber;
            this.objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.objVerification_Desc.sz_flag = "BILL";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            string str2 = "";
            string str3 = "";
            string str4 = "";
            list.Add(this.objVerification_Desc);
            list2 = this._bill_Sys_BillTransaction.Get_Node_Type(list);
            if (list2.Contains("NFVER"))
            {
                str2 = "OLD";
                str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
            }
            else
            {
                str2 = "NEW";
                str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + str + "/Bills/";
            }
            CaseDetailsBO sbo = new CaseDetailsBO();
            string str5 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            #region NF3
            if (rdlbillType.SelectedValue == "WC000000000000000002")
            {
                string str6 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                string str7 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                this.objCaseDetailsBO = new CaseDetailsBO();
                DataSet set = new DataSet();
                string str8 = "";
                set = this.objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, this.Session["TM_SZ_BILL_ID"].ToString());
                if (set.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {
                        str8 = set.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (str8 == "1")
                {
                    this._MUVGenerateFunction = new MUVGenerateFunction();
                    ArrayList list3 = new ArrayList();
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                    
                    if (File.Exists(this.objNF3Template.getPhysicalPath() + str7 + this.str_1500))
                    {
                        if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str4))
                        {
                            Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str4);
                        }
                        File.Copy(this.objNF3Template.getPhysicalPath() + str7 + this.str_1500, this.objNF3Template.getPhysicalPath() + str4 + this.str_1500);
                    }
                    str3 = System.Configuration.ConfigurationManager.AppSettings["DocumentManagerURL"].ToString() + str4 + this.str_1500;
                    if (str2 == "OLD")
                    {
                        list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list3.Add(str4 + this.str_1500);
                        list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list3.Add(this.Session["TM_SZ_CASE_ID"]);
                        list3.Add(this.str_1500);
                        list3.Add(str4);
                        list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list3.Add(str);
                        list3.Add("NF");
                        list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(list3);
                    }
                    else
                    {
                        list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list3.Add(str4 + this.str_1500);
                        list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list3.Add(this.Session["TM_SZ_CASE_ID"]);
                        list3.Add(this.str_1500);
                        list3.Add(str4);
                        list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list3.Add(str);
                        list3.Add("NF");
                        list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        list3.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list3);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    //this.BindLatestTransaction();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str3.ToString() + "'); ", true);
                }
                else
                {
                    string str9 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                    ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                    string str10 = System.Configuration.ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                    System.Configuration.ConfigurationManager.AppSettings["NF3_PAGE4"].ToString();
                    Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
                    string str16 = configuration.getConfigurationSettings(str5, "GET_DIAG_PAGE_POSITION");
                    string str17 = configuration.getConfigurationSettings(str5, "DIAG_PAGE");
                    string str12 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                    string strSec = ConfigurationManager.AppSettings["NF3_XML_FILE_SEC"].ToString();
                    string str13 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                    string str14 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                    string str15 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                    GeneratePDFFile.GenerateNF3PDF enfpdf = new GeneratePDFFile.GenerateNF3PDF();
                    this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                    //string str18 = "";
                    string str18 = enfpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str9);
                    log.Debug("Bill Details PDF File : " + str18);

                    string str11 = "";
                    if (rdlBill.SelectedValue == "Primary")
                    {
                        str11 = this.objPDFReplacement.ReplacePDFvalues(str12, str13, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    }
                    else if (rdlBill.SelectedValue == "Secondary")
                    {
                        str11 = this.objPDFReplacement.ReplacePDFvalues(strSec, str13, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    }

                    log.Debug("Page1 : " + str11);
                    string str19 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str11, str18);
                    string str21 = this.objPDFReplacement.ReplacePDFvalues(str14, str15, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    string str23 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(str23, str, "", "Speciality");
                    string str22 = this._MUVGenerateFunction.get_bt_include(str23, "", "WC000000000000000002", "CaseType");
                    if ((this.bt_include == "True") && (str22 == "True"))
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());                        
                    }
                    MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str6 + str19, this.objNF3Template.getPhysicalPath() + str6 + str21, this.objNF3Template.getPhysicalPath() + str6 + str21.Replace(".pdf", "_MER.pdf"));
                    string str20 = str21.Replace(".pdf", "_MER.pdf");
                    if ((this.bt_include == "True") && (str22 == "True"))
                    {
                        MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str6 + str20, this.objNF3Template.getPhysicalPath() + str6 + this.str_1500, this.objNF3Template.getPhysicalPath() + str6 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        str20 = this.str_1500.Replace(".pdf", "_MER.pdf");
                    }
                    string str24 = "";
                    str24 = str6 + str20;
                    log.Debug("GenereatedFileName : " + str24);
                    string str25 = "";
                    str25 = System.Configuration.ConfigurationManager.AppSettings["DocumentManagerURL"].ToString() + str24;
                    string path = this.objNF3Template.getPhysicalPath() + "/" + str24;
                    CUTEFORMCOLib.CutePDFDocumentClass class2 = new CUTEFORMCOLib.CutePDFDocumentClass();
                    string str27 = System.Configuration.ConfigurationManager.AppSettings["CutePDFSerialKey"].ToString();
                    class2.initialize(str27);
                    if ((((class2 != null) && File.Exists(path)) && ((str17 != "CI_0000003") && (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5))) && ((str16 == "CK_0000003") && ((str17 != "CI_0000004") || (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))))
                    {
                        str18 = path.Replace(".pdf", "_NewMerge.pdf");
                    }
                    string str28 = "";
                    if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str24 = path.Replace(".pdf", "_New.pdf").ToString();
                    }
                    if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_NewMerge.pdf").ToString()))
                    {
                        str28 = str25.Replace(".pdf", "_NewMerge.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str25.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                    }
                    else if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str28 = str25.Replace(".pdf", "_New.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str25.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                    }
                    else
                    {
                        str28 = str25.ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str25.ToString() + "'); ", true);
                    }
                    this.pdfpath = str28;
                    string str29 = "";
                    string[] strArray = str28.Split(new char[] { '/' });
                    ArrayList list4 = new ArrayList();
                    str28 = str28.Remove(0, System.Configuration.ConfigurationManager.AppSettings["DocumentManagerURL"].ToString().Length);
                    str29 = strArray[strArray.Length - 1].ToString();
                    if (File.Exists(this.objNF3Template.getPhysicalPath() + str7 + str29))
                    {
                        if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str4))
                        {
                            Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str4);
                        }
                        File.Copy(this.objNF3Template.getPhysicalPath() + str7 + str29, this.objNF3Template.getPhysicalPath() + str4 + str29);
                    }
                    if (str2 == "OLD")
                    {
                        list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list4.Add(str4 + str29);
                        list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list4.Add(this.Session["TM_SZ_CASE_ID"]);
                        list4.Add(strArray[strArray.Length - 1].ToString());
                        list4.Add(str4);
                        list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list4.Add(str);
                        list4.Add("NF");
                        list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(list4);
                    }
                    else
                    {
                        list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list4.Add(str4 + str29);
                        list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list4.Add(this.Session["TM_SZ_CASE_ID"]);
                        list4.Add(strArray[strArray.Length - 1].ToString());
                        list4.Add(str4);
                        list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list4.Add(str);
                        list4.Add("NF");
                        list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        list4.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list4);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str29;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    //this.BindLatestTransaction();
                }
            }
            #endregion

            #region PVT
            else if (rdlbillType.SelectedValue == "WC000000000000000003")
            {
                string str30;
                string companyName;
                Bill_Sys_PVT_Template template = new Bill_Sys_PVT_Template();
                bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str31 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str33 = this.Session["TM_SZ_BILL_ID"].ToString();
                string str34 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string str35 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template objNf3 = new Bill_Sys_NF3_Template();
                    companyName = objNf3.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    str30 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    companyName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    str30 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }

                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + template.GeneratePVTBill(flag, str30, str31, str, companyName, str33, str34, str35) + "'); ", true);
                
            }
            #endregion

            else
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
            }
            new Bill_Sys_BillTransaction_BO();
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

   

    protected void btnRegenerateBill_Click(object sender, EventArgs e)
    {
        Bill_Number = Session["TM_SZ_BILL_ID"].ToString();
        regenarate(Bill_Number);
    }

    protected void btnGenerateSecondBill_Click(object sender, EventArgs e)
    {

        string SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
        string SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
        string old_bill_number = Bill_Number;

        BillTransactionDAO ndao = new BillTransactionDAO();

        bool SecondBillExist = false;
        SecondBillExist = ndao.checkBillExist(Bill_Number);
        if (SecondBillExist)
        {
            hdnSecondExist.Value = "1";

            this.usrMessage.PutMessage("Second Bill Already Exist.");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            this.usrMessage.Show();
        }
        else
        {

            Result result = new Result();
            result = ndao.SaveSecondBillTransactions(SZ_COMPANY_ID, SZ_CASE_ID, SZ_USER_ID, old_bill_number);
            hdnSecondExist.Value = "1";
            btnGenerateSecondBill.Enabled = false;
            if (result.msg_code == "ERR")
            {
                this.usrMessage.PutMessage(result.msg);
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            else
            {
                Bill_Number = result.bill_no;
                regenarate(Bill_Number);
            }
        }
    }

    private string getApplicationSetting(String p_szKey)
    {
        SqlConnection myConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        myConn.Open();
        String szParamValue = "";

        SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = '" + p_szKey.ToString().Trim() + "'", myConn);
        SqlDataReader dr;
        dr = cmdQuery.ExecuteReader();

        while (dr.Read())
        {
            szParamValue = dr["parametervalue"].ToString();
        }
        return szParamValue;
    }

    public void regenarate(string bill_number)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (rdlBill.SelectedValue == "Primary")
        {
            Session["GenerateBill_insType"] = "Primary";
        }
        else if (rdlBill.SelectedValue == "Secondary")
        {
            Session["GenerateBill_insType"] = "Secondary";
        }
        log.Debug("Re-Generate bill");
        string PathOpenPdf = "";
        Bill_Number = bill_number;
        //Bill_Number = Session["TM_SZ_BILL_ID"].ToString();
        string str = Speciality;//e.Item.Cells[3].Text.Split(new char[] { ' ' })[0].ToString();//speciality
        string text = SpecilaityID;// e.Item.Cells[20].Text;//speciality id
        this.Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;//e.CommandArgument;
        //this.Session["TM_SZ_BILL_ID"] = Bill_Number;// e.Item.Cells[1].Text;//bill number
        this.objNF3Template = new Bill_Sys_NF3_Template();
        this.objVerification_Desc = new Bill_Sys_Verification_Desc();
        this.objVerification_Desc.sz_bill_no = Bill_Number;
        this.objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.objVerification_Desc.sz_flag = "BILL";
        ArrayList list = new ArrayList();
        ArrayList list2 = new ArrayList();
        string str2 = "";
        string str3 = "";
        list.Add(this.objVerification_Desc);
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        list2 = this._bill_Sys_BillTransaction.Get_Node_Type(list);
        if (list2.Contains("NFVER"))
        {
            str2 = "OLD";
            str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
        }
        else
        {
            str2 = "NEW";
            str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + str + "/Bills/";
        }
        CaseDetailsBO sbo = new CaseDetailsBO();
        string str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        objCaseDetailsBO = new CaseDetailsBO();

        # region Nofault-reagion
        if (rdlbillType.SelectedValue == "WC000000000000000002")
        //if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
        {

            string str5 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
            string str6 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
            this.objCaseDetailsBO = new CaseDetailsBO();
            DataSet set = new DataSet();
            string str7 = "";
            string str8 = "";
            set = this.objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, Bill_Number);
            if (set.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    str7 = set.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                }
            }
            if (str7 == "1")
            {
                this._MUVGenerateFunction = new MUVGenerateFunction();
                ArrayList list3 = new ArrayList();
                this.str_1500 = this._MUVGenerateFunction.FillPdf(Bill_Number);
                if (File.Exists(this.objNF3Template.getPhysicalPath() + str6 + this.str_1500))
                {
                    if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str3))
                    {
                        Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str3);
                    }
                    File.Copy(this.objNF3Template.getPhysicalPath() + str6 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500);
                }
                str8 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str3 + this.str_1500;
                if (str2 == "OLD")
                {
                    list3.Add(Bill_Number);
                    list3.Add(str3 + this.str_1500);
                    list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    list3.Add(this.Session["TM_SZ_CASE_ID"]);
                    list3.Add(this.str_1500);
                    list3.Add(str3);
                    list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    list3.Add(str);
                    list3.Add("NF");
                    list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    this.objNF3Template.saveGeneratedBillPath(list3);
                }
                else
                {
                    list3.Add(Bill_Number);
                    list3.Add(str3 + this.str_1500);
                    list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    list3.Add(this.Session["TM_SZ_CASE_ID"]);
                    list3.Add(this.str_1500);
                    list3.Add(str3);
                    list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    list3.Add(str);
                    list3.Add("NF");
                    list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    list3.Add(list2[0].ToString());
                    this.objNF3Template.saveGeneratedBillPath_New(list3);
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str8.ToString() + "'); ", true);
            }
            else
            {
                string str9 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                string str10 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
                string str16 = configuration.getConfigurationSettings(str4, "GET_DIAG_PAGE_POSITION");
                string str17 = configuration.getConfigurationSettings(str4, "DIAG_PAGE");
                string str12 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                string str13 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                string str14 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                string str15 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                string strSec = ConfigurationManager.AppSettings["NF3_XML_FILE_SEC"].ToString();
                GeneratePDFFile.GenerateNF3PDF enfpdf = new GeneratePDFFile.GenerateNF3PDF();
                this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

                string str18 = enfpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), Bill_Number, "", str9);
                log.Debug("Bill Details PDF File : " + str18);
                string str11 = "";
                if (rdlBill.SelectedValue == "Primary")
                {
                    str11 = this.objPDFReplacement.ReplacePDFvalues(str12, str13, Bill_Number, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                }
                else if (rdlBill.SelectedValue == "Secondary")
                {
                    str11 = this.objPDFReplacement.ReplacePDFvalues(strSec, str13, Bill_Number, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                }
                log.Debug("Page1 : " + str11);
                string str19 = "";
                string str20 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), Bill_Number, str11, str18);
                string str22 = this.objPDFReplacement.ReplacePDFvalues(str14, str15, Bill_Number, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                string str24 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                string str25 = Speciality; //e.Item.Cells[3].Text;//speciality
                _MUVGenerateFunction = new MUVGenerateFunction();
                this.bt_include = this._MUVGenerateFunction.get_bt_include(str24, str25, "", "Speciality");
                string str23 = this._MUVGenerateFunction.get_bt_include(str24, "", "WC000000000000000002", "CaseType");
                if ((this.bt_include == "True") && (str23 == "True"))
                {
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(Bill_Number);
                }
                MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str5 + str20, this.objNF3Template.getPhysicalPath() + str5 + str22, this.objNF3Template.getPhysicalPath() + str5 + str22.Replace(".pdf", "_MER.pdf"));
                string str21 = str22.Replace(".pdf", "_MER.pdf");
                if ((this.bt_include == "True") && (str23 == "True"))
                {
                    MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str5 + str21, this.objNF3Template.getPhysicalPath() + str5 + this.str_1500, this.objNF3Template.getPhysicalPath() + str5 + str21.Replace(".pdf", ".pdf"));
                }
                str19 = str5 + str21;
                log.Debug("GenereatedFileName : " + str19);
                string str26 = "";
                str26 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str19;
                string path = this.objNF3Template.getPhysicalPath() + "/" + str19;
                CUTEFORMCOLib.CutePDFDocumentClass class2 = new CUTEFORMCOLib.CutePDFDocumentClass();
                string str28 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                class2.initialize(str28);
                if ((((class2 != null) && File.Exists(path)) && ((str17 != "CI_0000003") && (this.objNF3Template.getDiagnosisCodeCount(Bill_Number) >= 5))) && ((str16 == "CK_0000003") && ((str17 != "CI_0000004") || (this.objNF3Template.getDiagnosisCodeCount(Bill_Number) != 5))))
                {
                    str18 = path.Replace(".pdf", "_NewMerge.pdf");
                }
                string str29 = "";
                if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str19 = path.Replace(".pdf", "_New.pdf").ToString();
                }
                if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_NewMerge.pdf").ToString()))
                {
                    str29 = str26.Replace(".pdf", "_NewMerge.pdf").ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str26.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                }
                else if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str29 = str26.Replace(".pdf", "_New.pdf").ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str26.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                }
                else
                {
                    str29 = str26.ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str26.ToString() + "'); ", true);
                }
                string str30 = "";
                string[] strArray2 = str29.Split(new char[] { '/' });
                ArrayList list4 = new ArrayList();
                str29 = str29.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                str30 = strArray2[strArray2.Length - 1].ToString();
                if (File.Exists(this.objNF3Template.getPhysicalPath() + str6 + str30))
                {
                    if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str3))
                    {
                        Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str3);
                    }
                    File.Copy(this.objNF3Template.getPhysicalPath() + str6 + str30, this.objNF3Template.getPhysicalPath() + str3 + str30);
                }
                if (str2 == "OLD")
                {
                    list4.Add(Bill_Number);
                    list4.Add(str3 + str30);
                    list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    list4.Add(this.Session["TM_SZ_CASE_ID"]);
                    list4.Add(strArray2[strArray2.Length - 1].ToString());
                    list4.Add(str3);
                    list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    list4.Add(str);
                    list4.Add("NF");
                    list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    this.objNF3Template.saveGeneratedBillPath(list4);
                }
                else
                {
                    list4.Add(Bill_Number);
                    list4.Add(str3 + str30);
                    list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    list4.Add(this.Session["TM_SZ_CASE_ID"]);
                    list4.Add(strArray2[strArray2.Length - 1].ToString());
                    list4.Add(str3);
                    list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    list4.Add(str);
                    list4.Add("NF");
                    list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    list4.Add(list2[0].ToString());
                    this.objNF3Template.saveGeneratedBillPath_New(list4);
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str30;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);

            }

        }
        #endregion

        #region private-region
        else if (rdlbillType.SelectedValue == "WC000000000000000003")
        //else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
        {
            string str31;
            string companyName;
            Bill_Sys_PVT_Template template = new Bill_Sys_PVT_Template();
            bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
            string str32 = this.Session["TM_SZ_CASE_ID"].ToString();
            string str34 = Bill_Number;
            string str35 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            string str36 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
            {
                companyName = new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                str31 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            }
            else
            {
                companyName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                str31 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            if (rdlBill.SelectedValue == "Primary")
            {
                Session["GenerateBill_insType"] = "Primary";
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + template.GeneratePVTBill(flag, str31, str32, str, companyName, str34, str35, str36) + "'); ", true);
            }
            else if (rdlBill.SelectedValue == "Secondary")
            {
                Session["GenerateBill_insType"] = "Secondary";
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + template.GeneratePVTBill(flag, str31, str32, str, companyName, str34, str35, str36) + "'); ", true);
            }
        }
        #endregion

        #region lien-region
        else if (rdlbillType.SelectedValue == "WC000000000000000004")
        //else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
        {
            string str38;
            string str42;
            string str37 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
            bool isReferingFacility = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
            this.Session["TM_SZ_CASE_ID"].ToString();
            string str39 = Bill_Number;
            string str40 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            string str41 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
            {
                Bill_Sys_NF3_Template objNF3 = new Bill_Sys_NF3_Template();
                string compname = objNF3.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                str38 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            }
            else
            {
                string compname = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                str38 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            Lien lien = new Lien();
            this._MUVGenerateFunction = new MUVGenerateFunction();
            this.objCaseDetailsBO = new CaseDetailsBO();
            DataSet set2 = new DataSet();
            string str43 = "";
            set2 = this.objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, str39);
            if (set2.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < set2.Tables[0].Rows.Count; j++)
                {
                    str43 = set2.Tables[0].Rows[j]["BT_1500_FORM"].ToString();
                }
            }
            if (str43 == "1")
            {
                ArrayList list5 = new ArrayList();
                this.str_1500 = this._MUVGenerateFunction.FillPdf(Bill_Number);
                if (File.Exists(this.objNF3Template.getPhysicalPath() + str37 + this.str_1500))
                {
                    if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str3))
                    {
                        Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str3);
                    }
                    File.Copy(this.objNF3Template.getPhysicalPath() + str37 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500);
                }
                str42 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str3 + this.str_1500;
                if (str2 == "OLD")
                {
                    list5.Add(Bill_Number);
                    list5.Add(str3 + this.str_1500);
                    list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    list5.Add(this.Session["TM_SZ_CASE_ID"]);
                    list5.Add(this.str_1500);
                    list5.Add(str3);
                    list5.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    list5.Add(str);
                    list5.Add("NF");
                    list5.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    this.objNF3Template.saveGeneratedBillPath(list5);
                }
                else
                {
                    list5.Add(Bill_Number);
                    list5.Add(str3 + this.str_1500);
                    list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    list5.Add(this.Session["TM_SZ_CASE_ID"]);
                    list5.Add(this.str_1500);
                    list5.Add(str3);
                    list5.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    list5.Add(str);
                    list5.Add("NF");
                    list5.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    list5.Add(list2[0].ToString());
                    this.objNF3Template.saveGeneratedBillPath_New(list5);
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
            }
            else
            {
                this.objNF3Template = new Bill_Sys_NF3_Template();
                string str45 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                string str46 = Speciality; //e.Item.Cells[3].Text;//speciality
                this.bt_include = this._MUVGenerateFunction.get_bt_include(str45, str46, "", "Speciality");
                string str44 = this._MUVGenerateFunction.get_bt_include(str45, "", "WC000000000000000004", "CaseType");
                if ((this.bt_include == "True") && (str44 == "True"))
                {
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(Bill_Number);
                    MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str3 + lien.GenratePdfForLienWithMuv(str38, str39, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str39, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str40, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str41), this.objNF3Template.getPhysicalPath() + str37 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                    str42 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str3 + this.str_1500.Replace(".pdf", "_MER.pdf");
                    ArrayList list6 = new ArrayList();
                    if (str2 == "OLD")
                    {
                        list6.Add(Bill_Number);
                        list6.Add(str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list6.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list6.Add(this.Session["TM_SZ_CASE_ID"]);
                        list6.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list6.Add(str3);
                        list6.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list6.Add(str);
                        list6.Add("NF");
                        list6.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(list6);
                    }
                    else
                    {
                        list6.Add(Bill_Number);
                        list6.Add(str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list6.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list6.Add(this.Session["TM_SZ_CASE_ID"]);
                        list6.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list6.Add(str3);
                        list6.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list6.Add(str);
                        list6.Add("NF");
                        list6.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        list6.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list6);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                }
                else
                {
                    str42 = lien.GenratePdfForLien(str38, str39, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str39, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str40, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str41);
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + str42 + "');", true);
        }
        #endregion

        #region WC-region
        else if (rdlbillType.SelectedValue.Contains("WC000000000000000001"))
        //else if (this.objCaseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000001")
        {
            try
            {
                str = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                str2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                string CaseId = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                string str5 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                WC_Bill_Generation generation = new WC_Bill_Generation();
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                string pdfNo = rdlbillType.SelectedValue.Substring(rdlbillType.SelectedValue.Length - 1);

                if (pdfNo == "6")
                {
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + generation.GeneratePDFForReferalWorkerComp(Bill_Number, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID,  str4, str3, str, str2, this._bill_Sys_BillTransaction.GetDoctorSpeciality(Bill_Number, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)) + "');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + generation.GeneratePDFForWorkerComp(Bill_Number, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, pdfNo, str4, str3, str, str2, str5, this._bill_Sys_BillTransaction.GetDoctorSpeciality(Bill_Number, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), 0) + "');", true);
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string errorMessage = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + errorMessage);

            }
        }
        #endregion

        #region UB4

        else if (rdlbillType.SelectedValue == "WC000000000000000005")
        {
            string ub4str = "";
            string pdfFileName = System.Configuration.ConfigurationManager.AppSettings["UB4_PDF_FILE"].ToString();

            UB4DAO objub4dao = new UB4DAO();
            objub4dao.pdfFileName = pdfFileName;
            objub4dao.szCaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            objub4dao.szCompID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            objub4dao.szCompName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            objub4dao.szUserName = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
            objub4dao.szUserID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            objub4dao.billNumber = Bill_Number;
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            objub4dao.szSpecilaity = this._bill_Sys_BillTransaction.GetDoctorSpeciality(Bill_Number, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            UB4ValueRelacement.UB4ValuReplacement objub4 = new UB4ValueRelacement.UB4ValuReplacement();
            if (rdlBill.SelectedValue == "Primary")
                ub4str = objub4.ReplacePDFvalues(objub4dao, "Primary");
            else if (rdlBill.SelectedValue == "Secondary")
                ub4str = objub4.ReplacePDFvalues(objub4dao, "Secondary");
            //getApplicationSetting("DocumentUploadLocationPhysical")
            PathOpenPdf = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + ub4str;

        }

        #endregion

        else if (this.objCaseDetailsBO.GetCaseType(Bill_Number) == "WC000000000000000006")
        {
            string str48 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
            bool compname = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
            this.Session["TM_SZ_CASE_ID"].ToString();
            this.Session["TM_SZ_BILL_ID"].ToString();
            string userName = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            string userID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
            {
                Bill_Sys_NF3_Template objNF3 = new Bill_Sys_NF3_Template();
                string compName = objNF3.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                string compID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            }
            else
            {
                string compName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                string compID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            this._MUVGenerateFunction = new MUVGenerateFunction();
            this.objCaseDetailsBO = new CaseDetailsBO();
            new DataSet();
            ArrayList list7 = new ArrayList();
            this.str_1500 = this._MUVGenerateFunction.FillPdf(Bill_Number);
            if (File.Exists(this.objNF3Template.getPhysicalPath() + str48 + this.str_1500))
            {
                if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str3))
                {
                    Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str3);
                }
                File.Copy(this.objNF3Template.getPhysicalPath() + str48 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500);
            }
            string text2 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str3 + this.str_1500;
            if (str2 == "OLD")
            {
                list7.Add(Bill_Number);
                list7.Add(str3 + this.str_1500);
                list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                list7.Add(this.Session["TM_SZ_CASE_ID"]);
                list7.Add(this.str_1500);
                list7.Add(str3);
                list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                list7.Add(str);
                list7.Add("NF");
                list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                this.objNF3Template.saveGeneratedBillPath(list7);
            }
            else
            {
                list7.Add(Bill_Number);
                list7.Add(str3 + this.str_1500);
                list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                list7.Add(this.Session["TM_SZ_CASE_ID"]);
                list7.Add(this.str_1500);
                list7.Add(str3);
                list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                list7.Add(str);
                list7.Add("NF");
                list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                list7.Add(list2[0].ToString());
                this.objNF3Template.saveGeneratedBillPath_New(list7);
            }
            this._DAO_NOTES_EO = new DAO_NOTES_EO();
            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
            this._DAO_NOTES_BO = new DAO_NOTES_BO();
            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
        }
        else
        {
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
        }
        if (PathOpenPdf != "")
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + PathOpenPdf + "');", true);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //public void convert(DataSet dsInsuranceDetails)
    //{
    //    try
    //    {
    
    //        string FileName = "Insurance";
    //        DataTable dt = new DataTable();
    //        dt = dsInsuranceDetails.Tables[0];
    //        if (dt.Rows.Count > 0)
    //        {
    //            string filename = FileName + ".xls";
    //            System.IO.StringWriter tw = new System.IO.StringWriter();
    //            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
    //            DataGrid dgGrid = new DataGrid();
    //            dgGrid.DataSource = dt;
    //            dgGrid.DataBind();

    //            //Get the HTML for the control.
    //            dgGrid.RenderControl(hw);
    //            //Write the HTML back to the browser.
    //            //Response.ContentType = application/vnd.ms-excel;
    //            Response.ContentType = "application/vnd.ms-excel";
    //            Response.AppendHeader("Content-Disposition",
    //                                  "attachment; filename=" + filename + "");
    //            this.EnableViewState = false;
    //            Response.Write(tw.ToString());
    //            Response.End();
    //        }
    //    }
    //    catch (System.Threading.ThreadAbortException lException)
    //    {

    //    }
    //}

    public void convert(DataSet dsInsuranceDetails)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataTable dtRecords = new DataTable();
        dtRecords = dsInsuranceDetails.Tables[0];
        string XlsPath = Server.MapPath(@"~/Add_data/test.xls");
        string attachment = string.Empty;
        if (XlsPath.IndexOf("\\") != -1)
        {
            string[] strFileName = XlsPath.Split(new char[] { '\\' });
            attachment = "attachment; filename=" + strFileName[strFileName.Length - 1];
        }
        else
            attachment = "attachment; filename=" + XlsPath;
        try
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = string.Empty;

            foreach (DataColumn datacol in dtRecords.Columns)
            {
                Response.Write(tab + datacol.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");

            foreach (DataRow dr in dtRecords.Rows)
            {
                tab = "";
                for (int j = 0; j < dtRecords.Columns.Count; j++)
                {
                    Response.Write(tab + Convert.ToString(dr[j]));
                    tab = "\t";
                }

                Response.Write("\n");
            }
            //Response.End();
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