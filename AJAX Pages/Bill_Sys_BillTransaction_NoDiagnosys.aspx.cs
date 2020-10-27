/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_BillTransaction.aspx.cs
/*Purpose              :       To Add and Edit Bill Transaction
/*Author               :       Ananda Naphade
/*Date of creation     :       11 Aug 2010
/*Modified By          :
/*Modified Date        :
/************************************************************/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Componend;
using System.Data.SqlClient;
using PDFValueReplacement;
using System.IO;
using log4net;
using mbs.LienBills;
using iTextSharp.text.pdf;



public partial class Bill_Sys_BillTransaction_NoDiagnosys : PageBase
{
    #region "Local Variable"

    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
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

    private const int I_COL_GRID_COMPLETED_VISITS_ADDED_BY_DOCTOR = 12;
    private const int I_COL_GRID_COMPLETED_VISITS_FINALIZED = 13;
    private const int I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER = 15;
    #endregion

    #region "Page Event"
    protected void Page_PreRender(object sender, EventArgs e)
    {
        log.Debug("Inside Page_PreRender method in page :Bill_Sys_BillTransaction_NoDiagnosys ");
        string szAddedByDoctor = "";
        string szFinalized = "";
        for (int i = 0; i < grdCompleteVisit.Items.Count; i++)
        {
            log.Debug("Inside for loop ");
            string speciality_id = grdCompleteVisit.Items[i].Cells[6].Text;
            log.Debug("Speciality ID : " + speciality_id);
            string visite_type = grdCompleteVisit.Items[i].Cells[3].Text; //
            log.Debug("Visit Type : " + visite_type);
            szAddedByDoctor = grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_DOCTOR].Text; // visit added by doctor.
            log.Debug("Added By Doctor: " + szAddedByDoctor);
            szFinalized = grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_FINALIZED].Text; // visit added by doctor.
            log.Debug("Finalized : " + szFinalized);
            if (szAddedByDoctor != null)
            {
                log.Debug("Added By Doctor is not null");
                if (szAddedByDoctor.ToLower().Equals("true") || szAddedByDoctor.ToLower().Equals("1"))
                {
                    log.Debug("Added By Doctor is not null and szAddedByDoctor.ToLower().Equals('true') || szAddedByDoctor.ToLower().Equals('1')");
                    if (szFinalized != null)
                    {
                        log.Debug("Added By Doctor is not null and szAddedByDoctor.ToLower().Equals('true') || szAddedByDoctor.ToLower().Equals('1') and szFinalized != null");
                        if (szFinalized.ToLower().Equals("false") || szFinalized.ToLower().Equals("0"))
                        {
                            log.Debug("(szFinalized.ToLower().Equals('false') || szFinalized.ToLower().Equals('0'))");
                            CheckBox chkSelected = (CheckBox)grdCompleteVisit.Items[i].FindControl("chkSelectItem");
                            chkSelected.Enabled = false;
                            Label lbl = (Label)grdCompleteVisit.Items[i].FindControl("lblAddedByDoctor");
                            lbl.Text = grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text + ", [Doctor], Not Finalized";
                            log.Debug("inner if result : " + lbl.Text);
                            //grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text = "";
                            //grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text =
                            //    grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text + ", [Doctor], Not Finalized";
                        }
                        else
                        {
                            Label lbl = (Label)grdCompleteVisit.Items[i].FindControl("lblAddedByDoctor");
                            lbl.Text = grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text + ", [Doctor], Finalized";
                            log.Debug("inner else result : " + lbl.Text);
                            //grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text = "";
                            //grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text =
                            //    grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text + ", [Doctor], Finalized";
                        }
                    }
                }
                else
                {
                    Label lbl = (Label)grdCompleteVisit.Items[i].FindControl("lblAddedByDoctor");
                    lbl.Text = grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text + ", [User]";
                    log.Debug("Outer else result : " + lbl.Text);
                    //grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text = "";
                    //grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text =
                    //grdCompleteVisit.Items[i].Cells[I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER].Text + ", [User]";
                }
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        //  this.Page.LoadComplete += new EventHandler(Page_Load_Complete);
        log.Debug("page start method(page name Bill_Sys_BillTransaction_NoDiagnosys):");
        try
        {
            log.Debug("Inside Try catch block1");
            try
            {
                log.Debug("Inside Try catch block2");
                if ((Request.QueryString["CheckSession"] != "") && (Request.QueryString["CheckSession"] != null))
                {
                    log.Debug("Inside checkSession is not null");
                    if (Request.QueryString["CheckSession"] != ((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID.ToString())
                    {
                        log.Debug("Inside checkSession2 is not null");
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        string szCaseId1 = Request.QueryString["CheckSession"];
                        _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(szCaseId1, "");
                        _bill_Sys_CaseObject.SZ_CASE_ID = szCaseId1;
                        _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                        _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                        _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, _bill_Sys_CaseObject.SZ_COMAPNY_ID);
                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                        Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                        _bill_Sys_Case.SZ_CASE_ID = szCaseId1;
                        Session["CASEINFO"] = _bill_Sys_Case;
                        Response.Clear();
                        Response.ClearContent();
                        Response.Write("changed");
                        log.Debug("Inside checkSession2 is not null method end");
                    }
                    else
                    {
                        Response.Clear();
                        Response.ClearContent();
                        Response.Write("same");
                    }

                }
            }
            catch { }

            btnLoadProcedures.Visible = true;
            btnClearService.Visible = true;
            lnkAddDiagnosis.Visible = true;
            lnkbtnRemoveDiag.Visible = true;
            btnRemove.Visible = true;
            btnSave.Visible = true;
            btnUpdate.Visible = true;
            if (Request.QueryString["CaseID"] != null && Request.QueryString["pname"] != null && Request.QueryString["cmpid"].ToString() != null)
            {
                log.Debug("Inside Request Queryfor CaseId is not null");
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();

                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(Request.QueryString["CaseID"].ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = Request.QueryString["CaseID"].ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = Request.QueryString["pname"].ToString();
                _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, Request.QueryString["cmpid"].ToString());
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = Request.QueryString["CaseID"].ToString();
                Session["CASEINFO"] = _bill_Sys_Case;
                log.Debug("Inside Request Queryfor CaseId is not null for method end");
            }
            //for billsearch ajax page
            if (Request.QueryString["CaseID"] != null && Request.QueryString["bno"] != null)
            {
                log.Debug("Inside Request Queryfor CaseId and bill no is not null");
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                string szCaseId = Request.QueryString["CaseID"];
                byte[] ar = System.Convert.FromBase64String(szCaseId);
                szCaseId = System.Text.ASCIIEncoding.ASCII.GetString(ar);
                txtCaseIDdummy.Text = szCaseId;

                string szBillNumber = Request.QueryString["bno"];
                byte[] ar1 = System.Convert.FromBase64String(szBillNumber);
                Session["SZ_BILL_NUMBER"] = System.Text.ASCIIEncoding.ASCII.GetString(ar1);


                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(szCaseId, "");
                _bill_Sys_CaseObject.SZ_CASE_ID = szCaseId;
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, _bill_Sys_CaseObject.SZ_COMAPNY_ID);

                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = szCaseId;

                Session["CASEINFO"] = _bill_Sys_Case;
                log.Debug("Inside Request Queryfor CaseId and bill no is not null for method  end");
            }
            //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            //{
            //    Response.Redirect("..\\Bill_Sys_ReferralBillTransaction.aspx?Type=Search", false);
            //}
            dummybtnAddServices.Visible = true;
            dummybtnAddGroup.Visible = true;
            if (!IsPostBack)
            {


                //ashutosh..check for bill no. caseid case no

                if (Request.QueryString["BillNo"] != null && Request.QueryString["caseid"] != null && Request.QueryString["caseno"] != null)
                {

                    Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                    CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                    string szCaseId = Request.QueryString["CaseID"];
                    //byte[] ar = System.Convert.FromBase64String(szCaseId);
                    // szCaseId = System.Text.ASCIIEncoding.ASCII.GetString(ar);
                    _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(szCaseId, "");
                    _bill_Sys_CaseObject.SZ_CASE_ID = szCaseId;
                    _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                    _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                    _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, _bill_Sys_CaseObject.SZ_COMAPNY_ID);

                    Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                    Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                    _bill_Sys_Case.SZ_CASE_ID = szCaseId;


                    Session["CASEINFO"] = _bill_Sys_Case;

                    //  Session["PassedCaseID"] = e.Item.Cells[7].Text;
                    Session["PassedCaseID"] = szCaseId;
                    //Session["SZ_BILL_NUMBER"] = e.CommandArgument;
                    Session["SZ_BILL_NUMBER"] = Request.QueryString["BillNo"].ToString();
                }
                //
                CaseDetailsBO _objCaseDetailBO = new CaseDetailsBO();
                string sz_CaseStatus = _objCaseDetailBO.GetCaseStatusArchived(((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
                if (sz_CaseStatus == "2")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('You can not create bill for this patient..');window.document.location.href='Bill_Sys_CaseDetails.aspx';</script>");
                    return;
                }
                if (Request.QueryString["Type"] == null)
                {
                    if (Session["CASE_OBJECT"] != null)
                    {
                        txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        txtCaseIDdummy.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        BindVisitCompleteGrid();
                        dvcompletevisit.Visible = true;
                    }
                    Session["SZ_BILL_NUMBER"] = null;
                }

            }
            if (Session["CASE_OBJECT"] != null)
            {
                txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                txtCaseIDdummy.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                txtCaseNo.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = txtCaseID.Text;
                Session["CASEINFO"] = _bill_Sys_Case;
                String szURL = "";
                String szCaseID = txtCaseID.Text;
                Session["QStrCaseID"] = szCaseID;
                Session["Case_ID"] = szCaseID;
                Session["Archived"] = "0";
                Session["QStrCID"] = szCaseID;
                Session["SelectedID"] = szCaseID;
                Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["SN"] = "0";
                Session["LastAction"] = "vb_CaseInformation.aspx";
                Session["SZ_CASE_ID_NOTES"] = txtCaseID.Text;
                Session["TM_SZ_CASE_ID"] = txtCaseID.Text;
                GetPatientDeskList();
            }
            else
            {
                log.Debug("Inside CASE_OBJECT is  null,redirect page :Bill_Sys_SearchCase.aspx ");
                Response.Redirect("Bill_Sys_SearchCase.aspx", false);
            }

            lblLocationNote.Text = "";
            if (!IsPostBack)
            {
                log.Debug("If page is not Post Back ");
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
                {
                    log.Debug("sz_location is not 1,and there is no code here. ");
                }
                else
                {
                    log.Debug("sz_location is  1.");
                    CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                    string szLocationID = _caseDetailsBO.GetPatientLocationID(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    log.Debug("location ID : " + szLocationID);
                    if (szLocationID != null)
                    {
                        log.Debug("sz_location is  not null.");
                        if (szLocationID.Equals(""))
                        {
                            log.Debug("sz_location is   null.");
                            lblLocationNote.Text = "Note: There is no office location set for the patient / doctor";
                            log.Debug("sz_location is  not null." + lblLocationNote.Text);
                        }
                    }
                    DataSet dsDoctorName = _caseDetailsBO.DoctorName(szLocationID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    extddlDoctor.DataSource = dsDoctorName;
                    extddlDoctor.DataTextField = "DESCRIPTION";
                    extddlDoctor.DataValueField = "CODE";
                    extddlDoctor.DataBind();
                    ListItem objItem = new ListItem("---select---", "NA");
                    extddlDoctor.Items.Insert(0, objItem);
                }

                Bill_Sys_BillTransaction_BO _BillTransaction = new Bill_Sys_BillTransaction_BO();
                bool nf2 = _BillTransaction.checkNf2(txtCompanyID.Text, txtCaseID.Text);
                log.Debug("value of nf2(from method checkNf2 with parameter txtCompanyID and txtCaseID ) :" + nf2);
                if (nf2)
                {
                    txtNf2.Text = "1";
                    chkNf2.Checked = true;

                }
                else
                {
                    txtNf2.Text = "0";
                    chkNf2.Checked = false;
                }
            }

            string querystring;
            if (Request.QueryString["Type"] == null)
            {
                log.Debug("If type is null");
                if (dvcompletevisit.Visible == true)
                {
                    log.Debug("If type is null and dvcompletevisit.Visible == true ");
                    querystring = "";
                    log.Debug("set value of querystring is null");
                }
                else
                {
                    querystring = "flag";
                    log.Debug("set value of querystring as flag");
                }
            }
            else
            {
                log.Debug("If type is not null");
                querystring = Request.QueryString["Type"].ToString();
                log.Debug("value of querystring : " + querystring);
            }
            btnSave.Attributes.Add("onclick", "return ConfirmClaimInsurance();");
            btnUpdate.Attributes.Add("onclick", "return FormValidation();");

            btnAddServices.Attributes.Add("onclick", "return completeGridValidator('" + querystring + "');");
            btnAddGroup.Attributes.Add("onclick", "return CompleteVisitGroupVisit('" + querystring + "');");



            btnAddPreferedList.Attributes.Add("onclick", "return CheckValidateAddToPrefredList();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlDiagnosisType.Flag_ID = txtCompanyID.Text;
            extddlPreferredDiagnosisType.Flag_ID = txtCompanyID.Text;
            log.Debug("Diagnosis Type. Flag Id(extddlDiagnosisType.Flag_ID = txtCompanyID.Text) : " + extddlDiagnosisType.Flag_ID);
            string doctor_ID = "";
            if (!IsPostBack)
            {
                btnUpdate.Enabled = false;
                if (Session["SZ_BILL_NUMBER"] != null)
                {
                    log.Debug("If Bill Number is not null");
                    txtBillID.Text = Session["SZ_BILL_NUMBER"].ToString();
                    Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
                    doctor_ID = _bill_Sys_Visit_BO.GetDoctorID(txtBillID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                    BindDoctorsGrid(txtBillID.Text);
                    dvcompletevisit.Visible = true;

                    hndDoctorID.Value = doctor_ID;
                    BindTransactionData(txtBillID.Text);
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                }
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_ADD_TO_PREFERED_LIST == "1")
                {
                    btnAddPreferedList.Visible = true;
                }
                else
                {
                    btnAddPreferedList.Visible = false;
                }
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_ADD_TO_PREFERED_LIST_FOR_DIAGNOSIS_CODE == "1")
                {
                    lnkAddPreferedList.Visible = true;
                }
                else
                {
                    lnkAddPreferedList.Visible = false;
                }

                txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                txtBillDatedummy.Text = DateTime.Now.Date.ToShortDateString();
                Session["SELECTED_DIA_PRO_CODE"] = null;
                if (Request.QueryString["PopUp"] == null)
                {

                    log.Debug(" Request.QueryString['PopUp'] is null and there is no code here");
                }
                else
                {
                    if (Session["TEMP_DOCTOR_ID"] != null)
                    {
                        log.Debug(" Request.QueryString['PopUp'] is not null and temp doctor id is not null");
                        extddlDoctor.SelectedValue = Session["TEMP_DOCTOR_ID"].ToString();
                    }
                    GetProcedureCode(hndDoctorID.Value.ToString());
                }

                if (Session["SZ_BILL_NUMBER"] != null)
                {
                    log.Debug("If Bill Number is not null");
                    txtBillID.Text = Session["SZ_BILL_NUMBER"].ToString();
                    _editOperation = new EditOperation();
                    _editOperation.Primary_Value = Session["SZ_BILL_NUMBER"].ToString();
                    _editOperation.WebPage = this.Page;
                    _editOperation.Xml_File = "BillTransaction.xml";
                    _editOperation.LoadData();
                    txtBillDate.Text = String.Format("{0:MM/dd/yyyy}", txtBillDate.Text).ToString();
                    setDefaultValues(Session["SZ_BILL_NUMBER"].ToString());
                }
                BindLatestTransaction();
            }
            else
            {
                log.Debug("If Bill Number is null");
                if (Session["SELECTED_DIA_PRO_CODE"] != null)
                {
                    log.Debug("If Bill Number is null and SELECTED_DIA_PRO_CODE != null");
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["SELECTED_DIA_PRO_CODE"];
                    grdTransactionDetails.DataSource = dt;
                    grdTransactionDetails.DataBind();
                    Session["SELECTED_DIA_PRO_CODE"] = null;
                }
            }

            objCaseDetailsBO = new CaseDetailsBO();
            foreach (DataGridItem objItem in grdLatestBillTransaction.Items)
            {
                log.Debug("Inside foreach loop as DataGridItem objItem in grdLatestBillTransaction.Items ");
                if (objCaseDetailsBO.GetCaseType(objItem.Cells[1].Text) != "WC000000000000000001")
                {
                    objItem.Cells[13].Text = "";
                    objItem.Cells[14].Text = "";
                    objItem.Cells[15].Text = "";
                    objItem.Cells[16].Text = "";  // Added By Kapil 22 March 2012
                    log.Debug("set  objItem.Cells[13].Text,[14],[15] as null  ");
                }
            }
            txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ArrayList _objArrayList = _bill_Sys_BillTransaction.GetClaimInsurance(txtCaseID.Text);

            if (_objArrayList.Count > 0)
            {
                log.Debug("If ArrayList count is greater than 0 ");
                if (_objArrayList[0].ToString() != "" && _objArrayList[0].ToString() != "NA" && _objArrayList[2].ToString() != "" && _objArrayList[2].ToString() != "NA")
                {
                    if (_objArrayList[1].ToString() != "" && _objArrayList[1].ToString() != "")
                    {
                        log.Debug("_objArrayList[1].ToString() != '' && _objArrayList[1].ToString() != ''");
                        txtClaimInsurance.Text = "3";
                        log.Debug(" set ClaimInsurance=3");
                    }
                    else
                    {

                        txtClaimInsurance.Text = "2";
                        log.Debug("Else set ClaimInsurance=2");

                    }
                }
                else
                {
                    txtClaimInsurance.Text = "1";
                    log.Debug("Else set ClaimInsurance=1");
                }
            }
            else
            {
                txtClaimInsurance.Text = "0";
                log.Debug("If ArrayList count is equal or less than 0  set ClaimInsurance=0");
            }

            #region "Remove Procedure code logic"
            log.Debug("#region Remove Procedure code logic");
            if (!Page.IsPostBack)
            {
                log.Debug("If page is not post back ");
                Session["DELETED_PROC_CODES"] = null;
                log.Debug("set DELETED_PROC_CODES is null");
            }
            #endregion

            SetControlForUpdateBill();
            objCaseDetailsBO = new CaseDetailsBO();
            string szCaseType = objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            if (szCaseType == "WC000000000000000001")
            {
                grdTransactionDetails.Columns[12].Visible = false;
            }
            checksession();
            //atul 
            if (!IsPostBack)
            {
                log.Debug("If page is not post back go to method cheklimit()");
                checkLimit();
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

    protected void Page_Load_Complete(object sender, EventArgs e)
    {
        try
        {
            SessionCheck.Value = ((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID;
        }
        catch
        {
            SessionCheck.Value = "";
        }
    }
    private void checksession()
    {
        try
        {
            SessionCheck.Value = ((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID;
        }
        catch
        {
            SessionCheck.Value = "";
        }
    }


    // added amod - feb 02-feb-2010
    private DataSet GetBillingDoctor(string p_szCompanyID)
    {
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_MST_BILLING_DOCTOR", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");
            sqlCmd.Parameters.AddWithValue("@ID", p_szCompanyID);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
        {
            ds = null;
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        return ds;
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        Session["SZ_BILL_ID"] = null;
        Session["PassedCaseID"] = null;
    }
    #endregion

    #region "Load default values"

    protected void setDefaultValues(string p_szBillID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO.GetBillDiagnosisCode(p_szBillID).Tables[0];
            lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            lstDiagnosisCodes.DataValueField = "CODE";
            lstDiagnosisCodes.DataBind();
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            grdTransactionDetails.Visible = true;
            BindTransactionDetailsGrid(p_szBillID);
            ArrayList objAL = new ArrayList();
            objAL = _bill_Sys_BillTransaction.GetBillInfo(p_szBillID);
            if (objAL != null)
            {
                // changed amod 02-feb-2010. removed extended drop down and used simple
                // drop down instead
                // extddlDoctor.Text = objAL[0].ToString();
                // GetProcedureCode(extddlDoctor.Text);

                extddlDoctor.SelectedValue = objAL[0].ToString();
                //GetProcedureCode(extddlDoctor.SelectedValue);
                GetProcedureCode(hndDoctorID.Value.ToString());
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

    protected void getDefaultAssociatedDiagCode()
    {

    }

    #endregion

    #region "No Use"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (extddlDoctor.Text == "NA")
        //{
        //    _saveOperation = new SaveOperation();
        //    _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        //    ArrayList _arrayList;
        //    try
        //    {
        //        _saveOperation.WebPage = this.Page;
        //        _saveOperation.Xml_File = "BillTransaction.xml";
        //        _saveOperation.SaveMethod();

        //        BindLatestTransaction();
        //        if (grdLatestBillTransaction.Items.Count > 3)
        //        {
        //            //BindGrid();
        //        }
        //        lblMsg.Visible = true;
        //        lblMsg.Text = " Bill Saved successfully ! ";
        //        if (Session["AssociateDiagnosis"] != null)
        //        {
        //            if (Convert.ToBoolean(Session["AssociateDiagnosis"].ToString()) == true)
        //            {
        //                //txtBillNo.Text = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());
        //                //btnAddService.Enabled = true;
        //                btnSave.Enabled = false;
        //            }
        //            else
        //            {
        //                grdTransactionDetails.Visible = true;
        //            }
        //        }
        //        else
        //        {

        //            // txtBillNo.Text = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());
        //            // Session["BillID"] = txtBillNo.Text;
        //            // BindTransactionDetailsGrid(txtBillNo.Text);
        //            grdTransactionDetails.Visible = true;
        //            //lnlInitialReport.Visible = true;
        //            //lnkCopyOldrogressReport.Visible = true;
        //            //lnlProgessReport.Visible = true;
        //            //lnkReportOfMMI.Visible = true;
        //        }
        //    }

        //    catch (SqlException objSqlExcp)
        //    {
        //        ErrorDiv.InnerText = " Bill Number already exists";
        //    }

        //    catch (Exception ex)
        //    {
        //        string strError = ex.Message.ToString();
        //        strError = strError.Replace("\n", " ");
        //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        //    }
        //}
        //else
        //{
        //    ErrorDiv.InnerText = " Select Doctor ...!";
        //}
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _editOperation = new EditOperation();
        try
        {
            if (Session["SZ_BILL_NUMBER"] != null)
            {
                // string str = txtBillNo.Text;
                _editOperation.Primary_Value = Session["SZ_BILL_NUMBER"].ToString();
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "BillTransaction.xml";
                _editOperation.UpdateMethod();
                Session["SZ_BILL_NUMBER"] = null;
                Response.Redirect("Bill_Sys_BillSearch.aspx", false);
            }
            else
            {
                _editOperation.Primary_Value = Session["BillID"].ToString();
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "BillTransaction.xml";
                _editOperation.UpdateMethod();
                usrMessage.PutMessage("Bill Updated successfully !");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            BindLatestTransaction();
            //BindGrid();

        }
        catch (SqlException objSqlExcp)
        {
            usrMessage.PutMessage(" Bill Number already exists");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
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

    protected void lnlProgessReport_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["TEMPLATE_BILL_NO"] = Session["BillID"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('../Bill_Sys_PatientInformationC4_2.aspx'); ", true);
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

    protected void lnkShowGrid_Click(object sender, EventArgs e)
    {

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtBillDate.Text = "";
            extddlDoctor.SelectedValue = "NA";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;

            grdTransactionDetails.Visible = false;
            ClearControl();
            lblDateOfService.Style.Add("visibility", "hidden");//.Visible=false;
            txtDateOfservice.Style.Add("visibility", "hidden");//.Visible=false;
            Image1.Style.Add("visibility", "hidden");//.Visible=false;

            lblGroupServiceDate.Style.Add("visibility", "hidden");//.Visible=false;
            txtGroupDateofService.Style.Add("visibility", "hidden");//.Visible=false;
            imgbtnDateofService.Style.Add("visibility", "hidden");
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

    protected void extddlProcedureCode_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
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

    private void SetDataOfAssociate(string setID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataSet dataset = new DataSet();
            dataset = _bill_Sys_BillTransaction.GetAssociatedEntry(setID);

            for (int i = 0; i <= dataset.Tables[0].Rows.Count - 1; i++)
            {
                extddlDoctor.SelectedValue = dataset.Tables[0].Rows[i][1].ToString();
                hndDoctorID.Value = dataset.Tables[0].Rows[i][1].ToString();
            }
            grdTransactionDetails.Visible = true;
            extddlDoctor.Enabled = false;
            Session["AssociateDiagnosis"] = true;
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

    private void BindAssociateGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
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

    protected void lnlInitialReport_Click(object sender, EventArgs e)
    {
        Session["TEMPLATE_BILL_NO"] = Session["BillID"].ToString();
        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('../Bill_Sys_PatientInformation.aspx'); ", true);
    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "BillTransaction.xml";
            _saveOperation.SaveMethod();

            _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
            string billno = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());
            txtBillID.Text = billno;

            // Start : Update Visit Status.
            if (grdAllReports.Visible == true)
            {

                Bill_Sys_Calender _bill_Sys_Visit_BO = new Bill_Sys_Calender();
                ArrayList objList;
                foreach (DataGridItem dgItem in grdAllReports.Items)
                {
                    if (((CheckBox)dgItem.Cells[0].FindControl("chkselect")).Checked == true)
                    {
                        objList = new ArrayList();
                        objList.Add(Convert.ToInt32(dgItem.Cells[4].Text));
                        objList.Add("1");
                        objList.Add("2");
                        objList.Add(txtBillID.Text);
                        objList.Add(txtBillDate.Text);
                        _bill_Sys_Visit_BO.UPDATE_Event_Status(objList);
                        foreach (DataGridItem j in grdTransactionDetails.Items)
                        {
                            if (dgItem.Cells[2].Text == j.Cells[1].Text)
                            {
                                objList = new ArrayList();
                                objList.Add(j.Cells[12].Text);
                                objList.Add(Convert.ToInt32(dgItem.Cells[4].Text));
                                objList.Add("2");
                                _bill_Sys_Visit_BO.Save_Event_RefferPrcedure(objList);
                            }
                        }
                    }
                }
            }
            // End : Update Visit Status.

            // Start : Save Notes for Bill.

            _DAO_NOTES_EO = new DAO_NOTES_EO();
            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

            _DAO_NOTES_BO = new DAO_NOTES_BO();
            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

            // End 

            objCaseDetailsBO = new CaseDetailsBO();
            String patientID = objCaseDetailsBO.GetPatientID(billno);
            if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000001")
            {
                objDefaultValue = new Bill_Sys_InsertDefaultValues();
                if (grdLatestBillTransaction.Items.Count == 0)
                {
                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_DoctorOpinion.xml"), txtCompanyID.Text.ToString(), null, patientID);
                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_ExamInformation.xml"), txtCompanyID.Text.ToString(), null, patientID);
                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_History.xml"), txtCompanyID.Text.ToString(), null, patientID);
                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_PlanOfCare.xml"), txtCompanyID.Text.ToString(), null, patientID);
                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_WorkStatus.xml"), txtCompanyID.Text.ToString(), null, patientID);

                }
                else if (grdLatestBillTransaction.Items.Count >= 1)
                {

                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_DoctorsOpinionC4_2.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_ExaminationTreatment.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_PermanentImpairment.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_WorkStatusC4_2.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                }
            }
            ArrayList _arrayList;
            foreach (DataGridItem j in grdTransactionDetails.Items)
            {
                if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                {
                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    _arrayList = new ArrayList();
                    _arrayList.Add(j.Cells[2].Text.ToString());
                    if (j.Cells[6].Text.ToString() != "&nbsp;") { _arrayList.Add(j.Cells[6].Text.ToString()); } else { _arrayList.Add(0); }
                    _arrayList.Add(billno);
                    _arrayList.Add(Convert.ToDateTime(j.Cells[1].Text.ToString()));
                    _arrayList.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    _arrayList.Add(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString());
                    _arrayList.Add(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString());
                    _arrayList.Add(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString());
                    _arrayList.Add(j.Cells[9].Text.ToString());
                    if (j.Cells[8].Text.ToString() != "&nbsp;" && j.Cells[8].Text.ToString() != "") { _arrayList.Add(j.Cells[8].Text.ToString()); } else { _arrayList.Add(0); }

                    _arrayList.Add(hndDoctorID.Value.ToString());
                    _arrayList.Add(txtCaseID.Text);
                    _arrayList.Add(j.Cells[11].Text.ToString());
                    _arrayList.Add(j.Cells[12].Text.ToString());
                    _arrayList.Add(j.Cells[13].Text.ToString());
                    _bill_Sys_BillTransaction.SaveTransactionData(_arrayList);
                }
            }
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
            foreach (ListItem lstItem in lstDiagnosisCodes.Items)
            {
                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                _arrayList = new ArrayList();
                _arrayList.Add(lstItem.Value.ToString());
                _arrayList.Add(billno);
                _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arrayList);
            }

            BindLatestTransaction();
            ClearControl();
            usrMessage.PutMessage(" Bill Saved successfully ! ");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
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

    #endregion

    #region "grdLastestBillTransaction Events"

    private void BindLatestTransaction()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _listOperation = new ListOperation();
        try
        {
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "LatestBillTransaction.xml";
            _listOperation.LoadList();
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

    protected void grdLatestBillTransaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {

            grdAllReports.Visible = false;
            Session["BillID"] = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[1].Text;
            Session["SZ_BILL_NUMBER"] = Session["BillID"].ToString();
            txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            if (grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[12].Text != "&nbsp;")
            {
                //extddlDoctor.SelectedValue = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[12].Text;
                hndDoctorID.Value = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[12].Text;
            }
            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO.GetBillDiagnosisCode(Session["BillID"].ToString()).Tables[0];
            lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            lstDiagnosisCodes.DataValueField = "CODE";
            lstDiagnosisCodes.DataBind();
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            grdTransactionDetails.Visible = true;
            BindTransactionDetailsGrid(Session["BillID"].ToString());
            SetControlForUpdateBill();
            BindDoctorsGrid(Session["SZ_BILL_NUMBER"].ToString());
            dvcompletevisit.Visible = true;
            double total = 0;
            if (grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO objIslimit = new BillTransactionDAO();
                string sz_procedure_group_id1 = grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                string sz_visit_tyep1 = grdTransactionDetails.Items[0].Cells[13].Text.ToString();
                string sz_PG_Id1 = objIslimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id1);
                string szIsLimit = objIslimit.GET_IS_LIMITE(txtCompanyID.Text, sz_PG_Id1);

                if (szIsLimit != "" && szIsLimit != "NULL")
                {
                    for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                    {
                        if (grdTransactionDetails.Items[i].Cells[5].Text != "" && grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;")
                        {
                            total = total + Convert.ToDouble(grdTransactionDetails.Items[i].Cells[5].Text);
                        }
                        if (i == grdTransactionDetails.Items.Count - 1)
                        {


                            //  if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                            {
                                BillTransactionDAO objLimit = new BillTransactionDAO();
                                string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                //grdTransactionDetails.Items[i].Cells[9].Visible = true;
                                //grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                if (szReturn != "")
                                {
                                    if (Convert.ToDouble(szReturn) < total)
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                    }
                                    else
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                    }
                                }
                                total = 0;
                            }

                        }
                        else
                        {
                            if (grdTransactionDetails.Items[i].Cells[1].Text != grdTransactionDetails.Items[i + 1].Cells[1].Text)
                            {

                                //   if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                                {
                                    BillTransactionDAO objLimit = new BillTransactionDAO();
                                    string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                    string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                    string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                    string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                    //grdTransactionDetails.Items[i].Cells[9].Visible=true;
                                    //grdTransactionDetails.Items[i].Cells[9].Text=szReturn;
                                    if (szReturn != "")
                                    {
                                        if (Convert.ToDouble(szReturn) < total)
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                        }
                                        else
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                        }
                                    }
                                    total = 0;

                                }

                            }
                        }
                    }
                }
            }

            //btnLoadProcedures.Visible = false;
            //btnAddGroup.Visible = false;
            //btnAddServices.Visible = false;
            //btnClearService.Visible = false;
            //lnkAddDiagnosis.Visible = false;
            //lnkbtnRemoveDiag.Visible = false;
            //btnUpdate.Visible = false;
            //btnRemove.Visible = false;
            //btnSave.Visible = false;

            // gridmodelpopup();
            // hndPopUpvalue.Value = "GridPopUp";
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

    protected void grdLatestBillTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _MUVGenerateFunction = new MUVGenerateFunction();


            log.Debug("Start : grdLatestBillTransaction_ItemCommand");

            if (e.CommandName.ToString() == "Add IC9 Code")
            {
                Session["PassedBillID"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_BillIC9Code.aspx", false);
            }


            if (e.CommandName.ToString() == "Generate bill")
            {
                log.Debug("Generate bill");
                String[] szName = e.Item.Cells[3].Text.Split(' ');
                String szSpecility = szName[0].ToString();
                string szSpecialityID = e.Item.Cells[20].Text; // change column 19 t0 20, By Kapil 21 March 2012

                Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                Session["TM_SZ_BILL_ID"] = e.Item.Cells[1].Text;

                objNF3Template = new Bill_Sys_NF3_Template();

                // changes for Doc Manager for Bill path-- pravin
                objVerification_Desc = new Bill_Sys_Verification_Desc();

                objVerification_Desc.sz_bill_no = Session["TM_SZ_BILL_ID"].ToString();
                objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                objVerification_Desc.sz_flag = "BILL";

                ArrayList arrNf_Param = new ArrayList();
                ArrayList arrNf_NodeType = new ArrayList();
                string sz_Type = "";
                String szDestinationDir = "";

                arrNf_Param.Add(objVerification_Desc);

                arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Param);
                if (arrNf_NodeType.Contains("NFVER"))
                {
                    sz_Type = "OLD";
                    szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
                }
                else
                {
                    sz_Type = "NEW";
                    szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + szSpecility + "/" + "Bills/";
                }




                CaseDetailsBO objCaseDetails = new CaseDetailsBO();
                String szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
                {
                    String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    String szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";

                    //changes for Add only 1500 Form For Insurance Company -- pravin
                    objCaseDetailsBO = new CaseDetailsBO();
                    DataSet ds1500form = new DataSet();
                    string bt_1500_Form = "";
                    string szReturnPath = "";

                    ds1500form = objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID, Session["TM_SZ_BILL_ID"].ToString());

                    if (ds1500form.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds1500form.Tables[0].Rows.Count; i++)
                        {
                            bt_1500_Form = ds1500form.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                        }
                    }

                    if (bt_1500_Form == "1")
                    {
                        _MUVGenerateFunction = new MUVGenerateFunction();
                        ArrayList objAL = new ArrayList();
                        str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());

                        if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                        {
                            if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                            {
                                Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                            }
                            File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                        }
                        szReturnPath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500;

                        if (sz_Type == "OLD")
                        {
                            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                            objAL.Add(szDestinationDir + str_1500);
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objAL.Add(Session["TM_SZ_CASE_ID"]);
                            objAL.Add(str_1500);
                            objAL.Add(szDestinationDir);
                            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add(szSpecility);
                            objAL.Add("NF");
                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                            objNF3Template.saveGeneratedBillPath(objAL);
                        }
                        else
                        {
                            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                            objAL.Add(szDestinationDir + str_1500);
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objAL.Add(Session["TM_SZ_CASE_ID"]);
                            objAL.Add(str_1500);
                            objAL.Add(szDestinationDir);
                            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add(szSpecility);
                            objAL.Add("NF");
                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                            objAL.Add(arrNf_NodeType[0].ToString());
                            objNF3Template.saveGeneratedBillPath_New(objAL);
                        }



                        // Start : Save Notes for Bill.

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                        BindLatestTransaction();

                        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szReturnPath.ToString() + "'); ", true);
                    }
                    else
                    {


                        string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

                        string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                        String szFile3 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                        String szFile4 = ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                        String szPDFPage1;
                        String szXMLFileName;
                        String szOriginalPDFFileName;
                        String szLastXMLFileName;
                        String szLastOriginalPDFFileName;
                        String sz3and4Page;

                        Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
                        String szDiagPDFFilePosition = objConfiguration.getConfigurationSettings(szCompanyID, "GET_DIAG_PAGE_POSITION");
                        String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(szCompanyID, "DIAG_PAGE");

                        szXMLFileName = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                        szOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                        szLastXMLFileName = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                        szLastOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();


                        Boolean fAddDiag = true;



                        GeneratePDFFile.GenerateNF3PDF objGeneratePDF = new GeneratePDFFile.GenerateNF3PDF();
                        objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

                        // Note : Generate PDF with Billing Information table. **** II
                        String szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
                        log.Debug("Bill Details PDF File : " + szPDFFileName);

                        sz3and4Page = szFile3;//""objPDFReplacement.Merge3and4Page(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szFile3, szFile4);

                        szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());

                        log.Debug("Page1 : " + szPDFPage1);


                        String szGenereatedFileName = "";
                        // Merge **** I AND **** II
                        String szPDF_1_3;
                        String szPDF_1_4;
                        szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);
                        String szLastPDFFileName;
                        String szPDFPage3;
                        szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());

                        //ashutosh
                        string bt_CaseType;
                        string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        string strCaseID = e.Item.Cells[3].Text;
                        bt_include = _MUVGenerateFunction.get_bt_include(strComp, strCaseID, "", "Speciality");
                        bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000002", "CaseType");
                        if (bt_include == "True" && bt_CaseType == "True")
                        {
                            str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                        }


                        if (true)
                        {



                            MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                            szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);

                            //ashutosh
                            if (bt_include == "True" && bt_CaseType == "True")
                            {
                                MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szLastPDFFileName, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDefaultPath + szLastPDFFileName.Replace(".pdf", ".pdf"));
                            }
                            //

                            szGenereatedFileName = szDefaultPath + szLastPDFFileName;
                            log.Debug("GenereatedFileName : " + szGenereatedFileName);
                        }
                        else
                        {
                            szPDF_1_4 = objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);
                            String szNewXMLFileName = ConfigurationManager.AppSettings["1500_XML_FILE"].ToString();
                            String szNewPDFFileName = ConfigurationManager.AppSettings["1500_PDF_FILE"].ToString();
                            string new_file = "";
                            new_file = objPDFReplacement.ReplacePDFvalues(szNewXMLFileName, szNewPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                            MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_4, objNF3Template.getPhysicalPath() + szDefaultPath + new_file, objNF3Template.getPhysicalPath() + szDefaultPath + new_file.Replace(".pdf", "_MER.pdf"));
                            szLastPDFFileName = new_file.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);

                            //ashutosh
                            if (bt_include == "True" && bt_CaseType == "True")
                            {
                                MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + new_file, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500.Replace(".pdf", "_MER.pdf"));
                            }
                            //

                            szGenereatedFileName = szDefaultPath + szLastPDFFileName;
                        }


                        String szOpenFilePath = "";
                        szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szGenereatedFileName;


                        string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;


                        CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
                        string newPdfFilename = "";
                        string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                        objMyForm.initialize(KeyForCutePDF);

                        if (objMyForm == null)
                        {
                            // Response.Write("objMyForm not initialized");
                        }
                        else
                        {
                            if (System.IO.File.Exists(szFileNameWithFullPath))
                            {
                                //    objMyForm.mergePDF(szFileNameWithFullPath, szFileNameWithFullPath.Replace(".pdf", "_Temp.pdf").ToString(), szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString());

                                if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAT_THE_END)
                                {
                                    if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) == 5)
                                    {
                                    }
                                    else
                                    {
                                        //MergePDF.MergePDFFiles(szFileNameWithFullPath, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString());
                                        szPDFFileName = szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf");
                                    }
                                }
                            }

                        }

                        // End Logic

                        string szFileNameForSaving = "";

                        // Save Entry in Table
                        if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                        {
                            szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
                        }

                        // End

                        if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
                        {
                            szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
                            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                        }
                        else
                        {
                            if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                            {
                                szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                            }
                            else
                            {
                                szFileNameForSaving = szOpenFilePath.ToString();
                                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                            }
                        }
                        String[] szTemp;
                        string szBillName = "";
                        szTemp = szFileNameForSaving.Split('/');
                        ArrayList objAL = new ArrayList();
                        szFileNameForSaving = szFileNameForSaving.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                        szBillName = szTemp[szTemp.Length - 1].ToString();

                        if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
                        {
                            if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                            {
                                Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                            }
                            File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
                        }
                        if (sz_Type == "OLD")
                        {
                            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                            objAL.Add(szDestinationDir + szBillName);
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objAL.Add(Session["TM_SZ_CASE_ID"]);
                            objAL.Add(szTemp[szTemp.Length - 1].ToString());
                            objAL.Add(szDestinationDir);
                            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add(szSpecility);
                            objAL.Add("NF");
                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                            objNF3Template.saveGeneratedBillPath(objAL);
                        }
                        else
                        {
                            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                            objAL.Add(szDestinationDir + szBillName);
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objAL.Add(Session["TM_SZ_CASE_ID"]);
                            objAL.Add(szTemp[szTemp.Length - 1].ToString());
                            objAL.Add(szDestinationDir);
                            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add(szSpecility);
                            objAL.Add("NF");
                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                            objAL.Add(arrNf_NodeType[0].ToString());
                            objNF3Template.saveGeneratedBillPath_New(objAL);
                        }



                        // Start : Save Notes for Bill.

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                        BindLatestTransaction();
                    }
                }
                else if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
                {
                    Bill_Sys_PVT_Template _objPvtBill;
                    _objPvtBill = new Bill_Sys_PVT_Template();
                    bool isReferingFacility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                    string szCompanyId;
                    string szCaseId = Session["TM_SZ_CASE_ID"].ToString();
                    // string szSpecility ;
                    string szCompanyName;
                    string szBillId = Session["TM_SZ_BILL_ID"].ToString();
                    string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                    {
                        Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                        szCompanyName = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        szCompanyId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        szCompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        szCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + _objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyId, szCaseId, szSpecility, szCompanyName, szBillId, szUserName, szUserId) + "'); ", true);
                }
                else if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
                {
                    String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    //Generate bill
                    bool isReferingFacility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                    string szCompanyId;
                    string szCaseId = Session["TM_SZ_CASE_ID"].ToString();
                    // string szSpecility ;
                    string szCompanyName;
                    string szBillId = Session["TM_SZ_BILL_ID"].ToString();
                    string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                    {
                        Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                        szCompanyName = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        szCompanyId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        szCompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        szCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    mbs.LienBills.Lien obj = new Lien();
                    string path;
                    //Tushar
                    string bt_CaseType;
                    _MUVGenerateFunction = new MUVGenerateFunction();
                    objNF3Template = new Bill_Sys_NF3_Template();
                    string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    string strCaseID = e.Item.Cells[3].Text;
                    objCaseDetailsBO = new CaseDetailsBO();
                    DataSet ds1500form = new DataSet();
                    string bt_1500_Form = "";

                    ds1500form = objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szBillId);
                    if (ds1500form.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1500form.Tables[0].Rows.Count; j++)
                        {
                            bt_1500_Form = ds1500form.Tables[0].Rows[j]["BT_1500_FORM"].ToString();
                        }
                    }
                    if (bt_1500_Form == "1")
                    {
                        ArrayList objAL = new ArrayList();
                        str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                        if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + str_1500))
                        {
                            if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                            {
                                Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                            }
                            File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                        }
                        path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500;

                        if (sz_Type == "OLD")
                        {
                            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                            objAL.Add(szDestinationDir + str_1500);
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objAL.Add(Session["TM_SZ_CASE_ID"]);
                            objAL.Add(str_1500);
                            objAL.Add(szDestinationDir);
                            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add(szSpecility);
                            objAL.Add("NF");
                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                            objNF3Template.saveGeneratedBillPath(objAL);
                        }
                        else
                        {

                            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                            objAL.Add(szDestinationDir + str_1500);
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objAL.Add(Session["TM_SZ_CASE_ID"]);
                            objAL.Add(str_1500);
                            objAL.Add(szDestinationDir);
                            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add(szSpecility);
                            objAL.Add("NF");
                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                            objAL.Add(arrNf_NodeType[0].ToString());
                            objNF3Template.saveGeneratedBillPath_New(objAL);
                        }

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                    }
                    else
                    {
                        bt_include = _MUVGenerateFunction.get_bt_include(strComp, strCaseID, "", "Speciality");
                        bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000004", "CaseType");
                        if (bt_include == "True" && bt_CaseType == "True")
                        {
                            str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                            String FileName;
                            FileName = obj.GenratePdfForLienWithMuv(szCompanyId, szBillId, _bill_Sys_BillTransaction.GetDoctorSpeciality(szBillId, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szUserName, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO, szUserId);
                            MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDestinationDir + FileName, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                            path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf");

                            ArrayList objAL = new ArrayList();
                            if (sz_Type == "OLD")
                            {
                                objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                                objAL.Add(szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                objAL.Add(Session["TM_SZ_CASE_ID"]);
                                objAL.Add(str_1500.Replace(".pdf", "_MER.pdf"));
                                objAL.Add(szDestinationDir);
                                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                                objAL.Add(szSpecility);
                                objAL.Add("NF");
                                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                                objNF3Template.saveGeneratedBillPath(objAL);
                            }
                            else
                            {
                                objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                                objAL.Add(szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                objAL.Add(Session["TM_SZ_CASE_ID"]);
                                objAL.Add(str_1500.Replace(".pdf", "_MER.pdf"));
                                objAL.Add(szDestinationDir);
                                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                                objAL.Add(szSpecility);
                                objAL.Add("NF");
                                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                                objAL.Add(arrNf_NodeType[0].ToString());
                                objNF3Template.saveGeneratedBillPath_New(objAL);
                            }


                            // Start : Save Notes for Bill.

                            _DAO_NOTES_EO = new DAO_NOTES_EO();
                            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                            _DAO_NOTES_BO = new DAO_NOTES_BO();
                            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                            _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                            _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                        }
                        else
                        {
                            path = obj.GenratePdfForLien(szCompanyId, szBillId, _bill_Sys_BillTransaction.GetDoctorSpeciality(szBillId, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szUserName, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO, szUserId);
                        }
                    }
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + path + "');", true);
                }
                else if (objCaseDetailsBO.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000006")
                {
                    String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    //Generate bill
                    bool isReferingFacility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                    string szCompanyId;
                    string szCaseId = Session["TM_SZ_CASE_ID"].ToString();
                    // string szSpecility ;
                    string szCompanyName;
                    string szBillId = Session["TM_SZ_BILL_ID"].ToString();
                    string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                    {
                        Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                        szCompanyName = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        szCompanyId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        szCompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        szCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    string path;
                    _MUVGenerateFunction = new MUVGenerateFunction();
                    objCaseDetailsBO = new CaseDetailsBO();
                    DataSet ds1500form = new DataSet();
                    ArrayList objAL = new ArrayList();
                    str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                    if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }
                    path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500;
                    if (sz_Type == "OLD")
                    {
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + str_1500);
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(str_1500);
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(szSpecility);
                        objAL.Add("NF");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objNF3Template.saveGeneratedBillPath(objAL);
                    }
                    else
                    {
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + str_1500);
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(str_1500);
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(szSpecility);
                        objAL.Add("NF");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objAL.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL);
                    }
                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;
                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
                }

                //Bill_Sys_GenerateFUReport _Bill_Sys_GenerateFUReport = new Bill_Sys_GenerateFUReport();
                //DataSet dsFUReport = new DataSet();
                //dsFUReport = _Bill_Sys_GenerateFUReport.GetFUInformation(Session["TM_SZ_BILL_ID"].ToString(), szSpecialityID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //if (dsFUReport != null)
                //{
                //    string szFilename = dsFUReport.Tables[0].Rows[0]["sz_file_name"].ToString();
                //    string szFilepath = dsFUReport.Tables[0].Rows[0]["sz_file_path"].ToString();
                //    string szImageID = dsFUReport.Tables[0].Rows[0]["i_image_id"].ToString();
                //    string szBasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();

                //    File.Move(szBasePath + szFilepath + szFilename, szBasePath + szFilepath + szFilename + ".deleted");

                //    szFilepath = szFilepath.Replace("\\", "/");
                //    _Bill_Sys_GenerateFUReport.DeleteFUReportFile(szFilename, szFilepath, szImageID, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szSpecialityID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["TM_SZ_BILL_ID"].ToString());
                //}

                /* Added by: Kunal
                 * For generating FUReport for doctors visit*/
                //if (hndIsAddedByDoctor.Value.ToLower() == "true" && hndFinalised.Value.ToLower() == "true")
                //{
                //    ArrayList arrBillNo = new ArrayList();
                //    arrBillNo.Add(Session["TM_SZ_BILL_ID"].ToString());
                //    Bill_Sys_GenerateFUReport obj_Bill_Sys_GenerateFUReport = new Bill_Sys_GenerateFUReport();
                //    string szSpecialityName = szSpecility;
                //    //string szSpecialityID = szSpecialityID;

                //    string strUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                //    string strUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                //    string strCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //    string strCheckPath = Server.MapPath("../Images/checkbox.JPG");
                //    string strUnCheckPath = Server.MapPath("../Images/uncheckbox.JPG");

                //    if (szSpecialityName == "AC" || szSpecialityName == "Accupuncture" || szSpecialityName == "Acupuncture")
                //    {
                //        obj_Bill_Sys_GenerateFUReport.billsperpatient(arrBillNo, strUserName, strUserId, strCompanyId, strCheckPath, strUnCheckPath, szSpecialityID);
                //    }
                //    else if (szSpecialityName == "SYN")
                //    {
                //        obj_Bill_Sys_GenerateFUReport.SYNbillsperpatient(arrBillNo, strUserName, strUserId, strCompanyId, strCheckPath, strUnCheckPath, szSpecialityID);
                //    }
                //    else if (szSpecialityName == "CH")
                //    {
                //        obj_Bill_Sys_GenerateFUReport.CHBillsPerPatient_iText(arrBillNo, strUserName, strUserId, strCompanyId, strCheckPath, strUnCheckPath, szSpecialityID);
                //    }
                //    else if (szSpecialityName == "PT")
                //    {
                //        obj_Bill_Sys_GenerateFUReport.PTTest(arrBillNo, strUserName, strUserId, strCompanyId, strCheckPath, strUnCheckPath, szSpecialityID);
                //    }
                //    else if (szSpecialityName == "WB")
                //    {
                //        obj_Bill_Sys_GenerateFUReport.WBBillsPerPatient(arrBillNo, strUserName, strUserId, strCompanyId, strCheckPath, strUnCheckPath, szSpecialityID);
                //    }
                //}

            }
            if (e.CommandName.ToString() == "Doctor's Initial Report")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                foreach (DataGridItem objItem in grdLatestBillTransaction.Items)
                {
                    if (objItem.Cells[13].Text != "" && objItem.Cells[14].Text == "")
                    {
                        objItem.Cells[14].Text = "";
                        objItem.Cells[15].Text = "";
                        objItem.Cells[16].Text = "";  // Added By Kapil
                    }
                }
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('../Bill_Sys_PatientInformation.aspx','WC4','menubar=no,scrollbars=yes'); ", true);
            }
            if (e.CommandName.ToString() == "Doctor's Progress Report")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('../Bill_Sys_PatientInformationC4_2.aspx'); ", true);
            }
            if (e.CommandName.ToString() == "Doctor's Report Of MMI")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_3.aspx'); ", true);
            }
            if (e.CommandName.ToString() == "Make Payment")
            {
                if (e.Item.Cells[11].Text != "1")
                {
                    Session["PassedBillID"] = e.CommandArgument;
                    Session["Balance"] = e.Item.Cells[9].Text;
                    Response.Redirect("Bill_Sys_PaymentTransactions.aspx", false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Please first add services to bill!'); ", true);
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

    protected void grdLatestBillTransaction_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (e.Item.Cells[11].Text == "1")
            {
                e.Item.Cells[10].Text = "";
            }
            objCaseDetailsBO = new CaseDetailsBO();
            if (objCaseDetailsBO.GetCaseType(e.Item.Cells[1].Text) != "WC000000000000000001")
            {
                e.Item.Cells[13].Text = "";
                e.Item.Cells[14].Text = "";
                e.Item.Cells[15].Text = "";
                e.Item.Cells[16].Text = ""; // Added By Kapil
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

    #region "Service Event Handler"

    protected void btnClearService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblDateOfService.Style.Add("visibility", "hidden");//.Visible=false;
            txtDateOfservice.Style.Add("visibility", "hidden");//.Visible=false;
            Image1.Style.Add("visibility", "hidden");//.Visible=false;

            lblGroupServiceDate.Style.Add("visibility", "hidden");//.Visible=false;
            txtGroupDateofService.Style.Add("visibility", "hidden");//.Visible=false;
            imgbtnDateofService.Style.Add("visibility", "hidden");

            ClearControl();

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

    #region "Service Fetch Method"

    private void Createtable()
    {
        ArrayList arrDiagnosisObject = new ArrayList();

        DataTable dt = new DataTable();
        dt.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
        dt.Columns.Add("DT_DATE_OF_SERVICE");
        dt.Columns.Add("SZ_PROCEDURE_ID");
        dt.Columns.Add("SZ_PROCEDURAL_CODE");
        dt.Columns.Add("SZ_CODE_DESCRIPTION");
        //dt.Columns.Add("FACTOR_AMOUNT");
        dt.Columns.Add("FLT_AMOUNT");
        //dt.Columns.Add("FACTOR");
        //dt.Columns.Add("PROC_AMOUNT");
        //dt.Columns.Add("DOCT_AMOUNT");
        dt.Columns.Add("I_UNIT");
        DataRow dr;

        foreach (DataGridItem j in grdTransactionDetails.Items)
        {

            dr = dt.NewRow();
            if (j.Cells[0].Text.ToString() != "&nbsp;" && j.Cells[0].Text.ToString() != "") { dr["SZ_BILL_TXN_DETAIL_ID"] = j.Cells[0].Text.ToString(); } else { dr["SZ_BILL_TXN_DETAIL_ID"] = ""; }
            dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(j.Cells[1].Text.ToString()).ToShortDateString();// Session["Date_Of_Service"].ToString();
            dr["SZ_PROCEDURE_ID"] = j.Cells[2].Text.ToString();
            dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
            dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
            //if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0) { dr["FACTOR_AMOUNT"] = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString(); }
            //if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0) { dr["FACTOR"] = ((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString(); }
            dr["FLT_AMOUNT"] = j.Cells[6].Text.ToString();
            if (((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString(); } }
            //dr["PROC_AMOUNT"] = j.Cells[9].Text.ToString();
            //dr["DOCT_AMOUNT"] = j.Cells[10].Text.ToString();
            dt.Rows.Add(dr);

        }

        Session["SELECTED_SERVICES"] = dt;// arrDiagnosisObject;

    }

    private void BindTransactionData(string id_)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            grdTransactionDetails.DataSource = _bill_Sys_BillTransaction.BindTransactionData(id_);
            grdTransactionDetails.DataBind();
            double total = 0;
            if (grdTransactionDetails.Items.Count > 0)
            {
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    if (grdTransactionDetails.Items[i].Cells[5].Text != "" && grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;")
                    {
                        total = total + Convert.ToDouble(grdTransactionDetails.Items[i].Cells[5].Text);
                    }
                    if (i == grdTransactionDetails.Items.Count - 1)
                    {


                        // if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                        {
                            BillTransactionDAO objLimit = new BillTransactionDAO();
                            string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                            string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                            string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                            string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                            //grdTransactionDetails.Items[i].Cells[9].Visible = true;
                            //  grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                            if (szReturn != "")
                            {
                                if (Convert.ToDouble(szReturn) < total)
                                {
                                    grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                }
                                else
                                {
                                    grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                }
                            }
                            total = 0;
                        }

                    }
                    else
                    {
                        if (grdTransactionDetails.Items[i].Cells[1].Text != grdTransactionDetails.Items[i + 1].Cells[1].Text)
                        {

                            // if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                            {
                                BillTransactionDAO objLimit = new BillTransactionDAO();
                                string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                //grdTransactionDetails.Items[i].Cells[9].Visible=true;
                                // grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                if (szReturn != "")
                                {
                                    if (Convert.ToDouble(szReturn) < total)
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                    }
                                    else
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                    }
                                }
                                total = 0;

                            }

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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindTransactionDetailsGrid(string billnumber)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            grdTransactionDetails.DataSource = _bill_Sys_BillTransaction.BindTransactionData(billnumber); //BindTransactionData
            grdTransactionDetails.DataBind();

            #region "Disply Unit Column if speciality have unit bit set to true."

            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            // change. amod 02-feb-2010. removed extended ddl and added simple ddl instead
            //if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.Text, txtCompanyID.Text))

            //changed by shailesh 31Mar2010, accepted doctor id from hidden field.
            //if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.SelectedValue, txtCompanyID.Text))
            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[6].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[7].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
            }
            #endregion


            if (grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO objIslimit = new BillTransactionDAO();

                string sz_procedure_group_id1 = grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                string sz_visit_tyep1 = grdTransactionDetails.Items[0].Cells[13].Text.ToString();
                string sz_PG_Id1 = objIslimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id1);
                string szIsLimit = objIslimit.GET_IS_LIMITE(txtCompanyID.Text, sz_PG_Id1);
                double total = 0;
                if (szIsLimit != "" && szIsLimit != "NULL")
                {
                    for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                    {
                        if (grdTransactionDetails.Items[i].Cells[5].Text != "" && grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;")
                        {
                            total = total + Convert.ToDouble(grdTransactionDetails.Items[i].Cells[5].Text);
                        }
                        if (i == grdTransactionDetails.Items.Count - 1)
                        {


                            // if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                            {
                                BillTransactionDAO objLimit = new BillTransactionDAO();
                                string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                //grdTransactionDetails.Items[i].Cells[9].Visible = true;

                                if (szReturn != "")
                                {
                                    if (Convert.ToDouble(szReturn) < total)
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                    }
                                    else
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                    }
                                }
                                total = 0;
                            }

                        }
                        else
                        {
                            if (grdTransactionDetails.Items[i].Cells[1].Text != grdTransactionDetails.Items[i + 1].Cells[1].Text)
                            {

                                //if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                                {
                                    BillTransactionDAO objLimit = new BillTransactionDAO();
                                    string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                    string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                    string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                    string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                    //grdTransactionDetails.Items[i].Cells[9].Visible=true;
                                    //  grdTransactionDetails.Items[i].Cells[9].Text = szReturn;

                                    if (szReturn != "")
                                    {
                                        if (Convert.ToDouble(szReturn) < total)
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                        }
                                        else
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                        }
                                    }
                                    total = 0;
                                }

                            }
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void CalculateAmount(string id_)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id_, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _bill_Sys_Menu = new Bill_Sys_Menu();
        decimal amount;
        try
        {
            amount = _bill_Sys_Menu.GetICcodeAmount(id);
            //txtAmount.Text = Convert.ToDecimal(amount * Convert.ToDecimal(txtUnit.Text)).ToString();
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

    private void BindIC9CodeControl(string id_)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        ArrayList _arrayList;
        _bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {

            _arrayList = new ArrayList();
            _arrayList = _bill_Sys_BillingCompanyDetails_BO.GetIC9CodeData(id_);
            if (_arrayList.Count > 0)
            {
                txtTransDetailID.Text = _arrayList[0].ToString();

                // txtBillNo.Text = _arrayList[2].ToString();
                //txtUnit.Text = _arrayList[3].ToString();
                //txtAmount.Text = _arrayList[4].ToString();
                //txtDescription.Text = _arrayList[6].ToString();
                //txtTempAmt.Value = _bill_Sys_Menu.GetICcodeAmount(extddlDiagnosisCode.Text.ToString()).ToString();
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

    private void UpdateTransactionDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        ArrayList _arrayList;
        try
        {
            _arrayList = new ArrayList();


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

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            extddlDoctor.SelectedValue = "NA";
            txtBillDate.Text = "";
            grdTransactionDetails.DataSource = null;
            grdTransactionDetails.DataBind();
            Session["SELECTED_DIA_PRO_CODE"] = null;
            Session["SZ_BILL_NUMBER"] = null;
            lstDiagnosisCodes.DataSource = null;
            lstDiagnosisCodes.DataBind();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            lstDiagnosisCodes.Items.Clear();
            lstDiagnosisCodes.DataSource = null;
            lstDiagnosisCodes.DataBind();
            grdAllReports.DataSource = null;
            grdAllReports.DataBind();
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

    #region "grdTransactionDetails Event"

    protected void grdTransactionDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtTransDetailID.Text = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[1].Text;
            txtAmount.Text = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[8].Text;
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

    protected void grdTransactionDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            grdTransactionDetails.CurrentPageIndex = e.NewPageIndex;
            if (Session["SELECTED_DIA_PRO_CODE"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)Session["SELECTED_DIA_PRO_CODE"];
                grdTransactionDetails.DataSource = dt;
                grdTransactionDetails.DataBind();
            }
            else
            {
                BindTransactionDetailsGrid(Session["BillID"].ToString());
            }
        }
        catch { }
    }

    #endregion

    #region "Save Bill Information"

    protected void btnSaveWithTransaction_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            string billno = "";
            #region "Save information into BillTransactionEO"
            BillTransactionEO _objBillEO = new BillTransactionEO();
            _objBillEO.SZ_CASE_ID = txtCaseID.Text;
            _objBillEO.SZ_COMPANY_ID = txtCompanyID.Text;
            _objBillEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);
            _objBillEO.SZ_DOCTOR_ID = hndDoctorID.Value.ToString();
            _objBillEO.SZ_TYPE = ddlType.Text;
            _objBillEO.SZ_TESTTYPE = ""; //ddlTestType.Text;
            _objBillEO.FLAG = "ADD";
            _objBillEO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            #endregion

            _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();

            ArrayList objALEventEO = new ArrayList();
            ArrayList objALEventRefferProcedureEO = new ArrayList();

            if (grdCompleteVisit.Visible == true) //check
            {
                Bill_Sys_Calender _bill_Sys_Visit_BO = new Bill_Sys_Calender();
                ArrayList objList;
                foreach (DataGridItem item in grdTransactionDetails.Items)
                {
                    string eventID = item.Cells[12].Text;
                    if (objALEventEO.Count != 0)
                    {
                        string flagC = "0";
                        for (int i = 0; i < objALEventEO.Count; i++)
                        {
                            EventEO _objEventEOCheck = new EventEO();
                            _objEventEOCheck = (EventEO)objALEventEO[i];
                            if ((eventID == _objEventEOCheck.I_EVENT_ID) && (_objEventEOCheck.I_EVENT_ID != "&nbsp;"))
                            {
                                flagC = "1";
                            }
                        }
                        if (flagC == "0")
                        {
                            EventEO _objEventEO = new EventEO();
                            _objEventEO.I_EVENT_ID = eventID;
                            _objEventEO.BT_STATUS = "1";
                            _objEventEO.I_STATUS = "2";
                            _objEventEO.SZ_BILL_NUMBER = "";
                            _objEventEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);

                            //_objEventEO.Check_Daily_Limit=
                            objALEventEO.Add(_objEventEO);
                        }
                    }
                    else
                    {
                        EventEO _objEventEO = new EventEO();
                        _objEventEO.I_EVENT_ID = eventID;
                        _objEventEO.BT_STATUS = "1";
                        _objEventEO.I_STATUS = "2";
                        _objEventEO.SZ_BILL_NUMBER = "";
                        _objEventEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);

                        objALEventEO.Add(_objEventEO);
                    }

                    int flag = 0;
                    for (int i = 0; i < objALEventRefferProcedureEO.Count; i++)
                    {
                        EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                        objEventRefferProcedureEO = (EventRefferProcedureEO)objALEventRefferProcedureEO[i];
                        if (objEventRefferProcedureEO.I_EVENT_ID == eventID)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag != 1)
                    {
                        foreach (DataGridItem j in grdTransactionDetails.Items)
                        {
                            if (j.Cells[1].Text != "")
                            {
                                if (item.Cells[12].Text.Equals(j.Cells[12].Text))
                                {
                                    EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                                    objEventRefferProcedureEO.SZ_PROC_CODE = j.Cells[8].Text;
                                    objEventRefferProcedureEO.I_EVENT_ID = j.Cells[12].Text;
                                    objEventRefferProcedureEO.I_STATUS = "2";
                                    objALEventRefferProcedureEO.Add(objEventRefferProcedureEO);
                                }
                            }
                        }
                    }
                }
            }
            objCaseDetailsBO = new CaseDetailsBO();
            string szCaseType = objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            ArrayList _objALBillProcedureCodeEO = new ArrayList();
            foreach (DataGridItem j in grdTransactionDetails.Items)
            {
                if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                {
                    BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();
                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

                    objBillProcedureCodeEO.SZ_PROCEDURE_ID = j.Cells[2].Text.ToString();
                    hdnSpeciality.Value = j.Cells[2].Text.ToString();
                    if (j.Cells[6].Text.ToString() != "&nbsp;")
                    {
                        objBillProcedureCodeEO.FL_AMOUNT = j.Cells[5].Text.ToString();
                    }
                    else
                    {
                        objBillProcedureCodeEO.FL_AMOUNT = "0";
                    }

                    objBillProcedureCodeEO.SZ_BILL_NUMBER = "";

                    objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(j.Cells[1].Text.ToString());

                    objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                    objBillProcedureCodeEO.I_UNIT = ((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString();

                    objBillProcedureCodeEO.FLT_PRICE = j.Cells[5].Text.ToString();

                    objBillProcedureCodeEO.SZ_DOCTOR_ID = hndDoctorID.Value.ToString();

                    objBillProcedureCodeEO.SZ_CASE_ID = txtCaseID.Text;

                    objBillProcedureCodeEO.SZ_TYPE_CODE_ID = j.Cells[8].Text.ToString();

                    //prashant 8_match 2011

                    //if (szCaseType == "WC000000000000000002")
                    //{
                    //    objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[9].Text.ToString();
                    //    objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[10].Text.ToString();
                    //}
                    //else
                    //{
                    //    objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                    //    objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                    //}





                    if (j.Cells[9].Text.ToString() != "&nbsp;")
                    {
                        objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[9].Text.ToString();

                    }
                    else
                    {
                        objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";

                    } if (j.Cells[10].Text.ToString() != "&nbsp;" && j.Cells[10].Text.ToString() != "&nbsp;" && j.Cells[10].Text.ToString() != "&nbsp;")
                    {
                        objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[10].Text.ToString();
                    }
                    else
                    {
                        objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                    }



                    //==============================
                    _objALBillProcedureCodeEO.Add(objBillProcedureCodeEO);
                }
            }
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            // _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
            ArrayList objALBillDiagnosisCodeEO = new ArrayList();
            foreach (ListItem lstItem in lstDiagnosisCodes.Items)
            {
                BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID = lstItem.Value.ToString();
                objALBillDiagnosisCodeEO.Add(objBillDiagnosisCodeEO);
            }


            // Call Save with Transaction.

            BillTransactionDAO objBT_DAO = new BillTransactionDAO();
            Result objResult = new Result();
            objResult = objBT_DAO.SaveBillTransactions(_objBillEO, objALEventEO, objALEventRefferProcedureEO, _objALBillProcedureCodeEO, objALBillDiagnosisCodeEO);
            if (objResult.msg_code == "ERR")
            {
                usrMessage.PutMessage(objResult.msg);
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
            else
            {
                txtBillID.Text = objResult.bill_no;
                billno = txtBillID.Text;

                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                // End 

                String patientID = objCaseDetailsBO.GetPatientID(billno);

                if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000001")
                {
                    //string patientID = Workers Compensation
                    objDefaultValue = new Bill_Sys_InsertDefaultValues();
                    if (grdLatestBillTransaction.Items.Count == 0)
                    {

                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_DoctorOpinion.xml"), txtCompanyID.Text.ToString(), null, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_ExamInformation.xml"), txtCompanyID.Text.ToString(), null, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_History.xml"), txtCompanyID.Text.ToString(), null, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_PlanOfCare.xml"), txtCompanyID.Text.ToString(), null, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_WorkStatus.xml"), txtCompanyID.Text.ToString(), null, patientID);

                    }
                    else if (grdLatestBillTransaction.Items.Count >= 1)
                    {

                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_DoctorsOpinionC4_2.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_ExaminationTreatment.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_PermanentImpairment.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_WorkStatusC4_2.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                    }
                }

                BindLatestTransaction();
                BindVisitCompleteGrid();
                ClearControl();
                usrMessage.PutMessage(" Bill Saved successfully ! ");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000002")
                {
                    //Bill_Generation _Bill_Generation = new Bill_Generation();
                    //_Bill_Generation.sz_bill_no = billno;
                    //_Bill_Generation.sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    //_Bill_Generation.sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                    //_Bill_Generation.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    //_Bill_Generation.sz_company_name = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;

                    //Bill_Sys_Verification_Desc objVerification_Desc = new Bill_Sys_Verification_Desc();

                    //objVerification_Desc.sz_bill_no = billno;
                    //objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    //objVerification_Desc.sz_flag = "BILL";

                    //ArrayList arrNf_Param = new ArrayList();
                    //ArrayList arrNf_NodeType = new ArrayList();
                    //string sz_Type = "";
                    //String szDestinationDir = "";

                    //arrNf_Param.Add(objVerification_Desc);

                    //arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Param);

                    //_Bill_Generation.sz_node_type = arrNf_NodeType;
                    //_Bill_Generation.sz_speciality = _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    //_Bill_Generation.sz_userid = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    //_Bill_Generation.sz_username = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;

                    //Nofault_Bill_Generation _Nofault_Bill_Generation = new Nofault_Bill_Generation();
                    //string strpath = _Nofault_Bill_Generation.NofaultBillGeneration(_Bill_Generation);
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + strpath + "'); ", true);

                    GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000003")
                {
                    GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                else if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000001")
                {
                    hdnWCPDFBillNumber.Value = billno.ToString();
                    pnlPDFWorkerCompAdd.Visible = true;
                    pnlPDFWorkerCompAdd.Width = System.Web.UI.WebControls.Unit.Pixel(100);
                    pnlPDFWorkerCompAdd.Height = System.Web.UI.WebControls.Unit.Pixel(100);
                    //GeneratePDFForWorkerComp(txtBillID.Text, txtCaseID.Text,"1");
                }
                if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000004")
                {
                    objNF3Template = new Bill_Sys_NF3_Template();
                    mbs.LienBills.Lien obj = new Lien();
                    _MUVGenerateFunction = new MUVGenerateFunction();
                    string sz_speciality = _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    string path;
                    //Tushar
                    string bt_CaseType, bt_include;
                    string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    Session["TM_SZ_BILL_ID"] = billno;

                    // Changes for Doc manager for New Bill path --pravin

                    objVerification_Desc = new Bill_Sys_Verification_Desc();

                    objVerification_Desc.sz_bill_no = billno;
                    objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    objVerification_Desc.sz_flag = "BILL";

                    ArrayList arrNf_Param = new ArrayList();
                    ArrayList arrNf_NodeType = new ArrayList();
                    string sz_Type = "";
                    String szDestinationDir = "";

                    arrNf_Param.Add(objVerification_Desc);

                    arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Param);

                    if (arrNf_NodeType.Contains("NFVER"))
                    {
                        sz_Type = "OLD";
                        szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + sz_speciality + "/";
                    }
                    else
                    {
                        sz_Type = "NEW";
                        szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + sz_speciality + "/" + "Bills/";
                    }
                    String szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    objCaseDetailsBO = new CaseDetailsBO();
                    DataSet ds1500form = new DataSet();

                    string bt_1500_Form = "";
                    ds1500form = objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["TM_SZ_BILL_ID"].ToString());

                    if (ds1500form.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds1500form.Tables[0].Rows.Count; i++)
                        {
                            bt_1500_Form = ds1500form.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                        }
                    }
                    if (bt_1500_Form == "1")
                    {
                        str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());

                        if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                        {
                            if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                            {
                                Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                            }
                            File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                        }
                        path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500;

                        ArrayList objAL = new ArrayList();
                        if (sz_Type == "OLD")
                        {
                            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                            objAL.Add(szDestinationDir + str_1500);
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objAL.Add(Session["TM_SZ_CASE_ID"]);
                            objAL.Add(str_1500);
                            objAL.Add(szDestinationDir);
                            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add(sz_speciality);
                            objAL.Add("NF");
                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                            objNF3Template.saveGeneratedBillPath(objAL);
                        }
                        else
                        {
                            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                            objAL.Add(szDestinationDir + str_1500);
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objAL.Add(Session["TM_SZ_CASE_ID"]);
                            objAL.Add(str_1500);
                            objAL.Add(szDestinationDir);
                            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add(sz_speciality);
                            objAL.Add("NF");
                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                            objAL.Add(arrNf_NodeType[0].ToString());
                            objNF3Template.saveGeneratedBillPath_New(objAL);
                        }
                        // Start : Save Notes for Bill.

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                    }
                    else
                    {
                        bt_include = _MUVGenerateFunction.get_bt_include(strComp, sz_speciality, "", "Speciality");
                        bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000004", "CaseType");
                        if (bt_include == "True" && bt_CaseType == "True")
                        {
                            String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                            // String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + sz_speciality + "/";
                            string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                            string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                            str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                            String FileName;
                            FileName = obj.GenratePdfForLienWithMuv(strComp, billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szUserName, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO, szUserId);
                            MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDestinationDir + FileName, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                            path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf");

                            ArrayList objAL = new ArrayList();
                            if (sz_Type == "OLD")
                            {
                                objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                                objAL.Add(szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                objAL.Add(Session["TM_SZ_CASE_ID"]);
                                objAL.Add(str_1500.Replace(".pdf", "_MER.pdf"));
                                objAL.Add(szDestinationDir);
                                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                                objAL.Add(sz_speciality);
                                objAL.Add("NF");
                                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                                objNF3Template.saveGeneratedBillPath(objAL);
                            }
                            else
                            {
                                objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                                objAL.Add(szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                objAL.Add(Session["TM_SZ_CASE_ID"]);
                                objAL.Add(str_1500.Replace(".pdf", "_MER.pdf"));
                                objAL.Add(szDestinationDir);
                                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                                objAL.Add(sz_speciality);
                                objAL.Add("NF");
                                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                                objAL.Add(arrNf_NodeType[0].ToString());
                                objNF3Template.saveGeneratedBillPath_New(objAL);
                            }

                            //objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                            //objAL.Add(szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                            //objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            //objAL.Add(Session["TM_SZ_CASE_ID"]);
                            //objAL.Add(str_1500.Replace(".pdf", "_MER.pdf"));
                            //objAL.Add(szDestinationDir);
                            //objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            //objAL.Add(sz_speciality);
                            //objAL.Add("NF");
                            //objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                            //objNF3Template.saveGeneratedBillPath(objAL);

                            // Start : Save Notes for Bill.

                            _DAO_NOTES_EO = new DAO_NOTES_EO();
                            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                            _DAO_NOTES_BO = new DAO_NOTES_BO();
                            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                            _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                            _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                        }
                        else
                        {
                            path = obj.GenratePdfForLien(txtCompanyID.Text, billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                        }
                    }
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + path + "');", true);
                }
                else if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000006")
                {
                    objNF3Template = new Bill_Sys_NF3_Template();
                    _MUVGenerateFunction = new MUVGenerateFunction();
                    string sz_speciality = _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    string path;
                    //Tushar
                    string bt_CaseType, bt_include;
                    string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    Session["TM_SZ_BILL_ID"] = billno;
                    // Changes for Doc manager for New Bill path --pravin
                    objVerification_Desc = new Bill_Sys_Verification_Desc();
                    objVerification_Desc.sz_bill_no = billno;
                    objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    objVerification_Desc.sz_flag = "BILL";
                    ArrayList arrNf_Param = new ArrayList();
                    ArrayList arrNf_NodeType = new ArrayList();
                    string sz_Type = "";
                    String szDestinationDir = "";
                    arrNf_Param.Add(objVerification_Desc);
                    arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Param);
                    if (arrNf_NodeType.Contains("NFVER"))
                    {
                        sz_Type = "OLD";
                        szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + sz_speciality + "/";
                    }
                    else
                    {
                        sz_Type = "NEW";
                        szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + sz_speciality + "/" + "Bills/";
                    }
                    String szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }
                    path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500;

                    ArrayList objAL = new ArrayList();
                    if (sz_Type == "OLD")
                    {
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + str_1500);
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(str_1500);
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(sz_speciality);
                        objAL.Add("LN");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objNF3Template.saveGeneratedBillPath(objAL);
                    }
                    else
                    {
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + str_1500);
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(str_1500);
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(sz_speciality);
                        objAL.Add("LN");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objAL.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL);
                    }
                    // Start : Save Notes for Bill.
                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;
                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                }

                /* Added by: Kunal
                 * For generating FUReport for doctors visit*/
                //if (hndIsAddedByDoctor.Value.ToLower() == "true" && hndFinalised.Value.ToLower() == "true")
                //{
                //    ArrayList arrBillNo = new ArrayList();
                //    arrBillNo.Add(billno);
                //    Bill_Sys_GenerateFUReport obj_Bill_Sys_GenerateFUReport = new Bill_Sys_GenerateFUReport();
                //    string szSpecialityName = _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //    string szSpecialityID = obj_Bill_Sys_GenerateFUReport.GetSpecId(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                //    string strUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                //    string strUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                //    string strCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //    string strCheckPath = Server.MapPath("../Images/checkbox.JPG");
                //    string strUnCheckPath = Server.MapPath("../Images/uncheckbox.JPG");

                //    if (szSpecialityName == "AC" || szSpecialityName == "Accupuncture" || szSpecialityName == "Acupuncture")
                //    {
                //        obj_Bill_Sys_GenerateFUReport.billsperpatient(arrBillNo, strUserName, strUserId, strCompanyId, strCheckPath, strUnCheckPath, szSpecialityID);
                //    }
                //    else if (szSpecialityName == "SYN")
                //    {
                //        obj_Bill_Sys_GenerateFUReport.SYNbillsperpatient(arrBillNo, strUserName, strUserId, strCompanyId, strCheckPath, strUnCheckPath, szSpecialityID);
                //    }
                //    else if (szSpecialityName == "CH")
                //    {
                //        obj_Bill_Sys_GenerateFUReport.CHBillsPerPatient_iText(arrBillNo, strUserName, strUserId, strCompanyId, strCheckPath, strUnCheckPath, szSpecialityID);
                //    }
                //    else if (szSpecialityName == "PT")
                //    {
                //        obj_Bill_Sys_GenerateFUReport.PTTest(arrBillNo, strUserName, strUserId, strCompanyId, strCheckPath, strUnCheckPath, szSpecialityID);
                //    }
                //    else if (szSpecialityName == "WB")
                //    {
                //        obj_Bill_Sys_GenerateFUReport.WBBillsPerPatient(arrBillNo, strUserName, strUserId, strCompanyId, strCheckPath, strUnCheckPath, szSpecialityID);
                //    }
                //}
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

    #endregion

    #region "Worker Compenstion PDF"
    public void GeneratePDFForWorkerComp(string szBillNumber, string szCaseID, string p_szPDFNo)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        String szURLDocumentManager = "";
        _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
        szURLDocumentManager = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString();
        String szDefaultPhysicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";
        string strGenFileName = "";
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        string strSpeName = _bill_Sys_BillTransaction.GetDoctorSpeciality(szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

        try
        {
            if (p_szPDFNo == "1")
            {
                PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
                strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                //strGenFileName = lfnMergeDiagCodePage(szDefaultPhysicalPath, strGenFileName, 4);
                ArrayList objAL = new ArrayList();
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                objAL.Add(szBillNumber);
                objAL.Add(szCaseID);
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName + "'); ", true);
            }

            if (p_szPDFNo == "2")
            {
                PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C42"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
                strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                //strGenFileName = lfnMergeDiagCodePage(szDefaultPhysicalPath, strGenFileName, 4);
                ArrayList objAL = new ArrayList();
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                objAL.Add(szBillNumber);
                objAL.Add(szCaseID);
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName + "'); ", true);
            }

            if (p_szPDFNo == "3")
            {
                PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C43"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
                strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                //strGenFileName = lfnMergeDiagCodePageForC43(szDefaultPhysicalPath, strGenFileName, 2);
                ArrayList objAL = new ArrayList();
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                objAL.Add(szBillNumber);
                objAL.Add(szCaseID);
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName + "'); ", true);
            }
            // Copy generated file from Packet Document to WC File.
            objNF3Template = new Bill_Sys_NF3_Template();
            String szBasePhysicalPath = objNF3Template.getPhysicalPath();
            String szNewPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/No Fault File/Bills/" + strSpeName.Trim() + "/";
            if (File.Exists(szBasePhysicalPath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName))
            {
                if (!Directory.Exists(szBasePhysicalPath + szNewPath))
                {
                    Directory.CreateDirectory(szBasePhysicalPath + szNewPath);
                }
                File.Copy(szBasePhysicalPath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName, szBasePhysicalPath + szNewPath + strGenFileName);
            }

            ArrayList objAL1 = new ArrayList();
            objAL1.Add(szBillNumber); // SZ_BILL_NUMBER
            objAL1.Add(szNewPath + strGenFileName); // SZ_BILL_PATH
            objAL1.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            objAL1.Add(szCaseID);
            objAL1.Add(strGenFileName); // SZ_BILL_NAME
            objAL1.Add(szNewPath); // SZ_BILL_FILE_PATH
            objAL1.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
            objAL1.Add(strSpeName.Trim());
            objAL1.Add("WC");
            objAL1.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
            objNF3Template.saveGeneratedBillPath(objAL1);

            _DAO_NOTES_EO = new DAO_NOTES_EO();
            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = strGenFileName;

            _DAO_NOTES_BO = new DAO_NOTES_BO();
            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

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

    private string lfnMergeDiagCodePage(string p_szDefaultPath, string p_szGeneratedFileName, int i_NumberOfRecords)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        String szNextDiagPDFFileName = "";
        try
        {
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
            string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
            String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DIAG_PAGE");


            GeneratePDFFile.GenerateC42PDF objGeneratePDF = new GeneratePDFFile.GenerateC42PDF();

            if (szGenerateNextDiagPage == "CI_0000005" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }

            if (szGenerateNextDiagPage == "CI_0000004" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }
            if (szNextDiagPDFFileName == "")
            {
                return p_szGeneratedFileName;
            }
            else
            {
                MergePDF.MergePDFFiles(p_szDefaultPath + p_szGeneratedFileName, p_szDefaultPath + szNextDiagPDFFileName, p_szDefaultPath + szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf"));
                
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
        return szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf");

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string lfnMergeDiagCodePageForC43(string p_szDefaultPath, string p_szGeneratedFileName, int i_NumberOfRecords)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        String szNextDiagPDFFileName = "";
        try
        {
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
            string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
            String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DIAG_PAGE");

            GeneratePDFFile.GenerateC43PDF objGeneratePDF = new GeneratePDFFile.GenerateC43PDF();

            if (szGenerateNextDiagPage == "CI_0000005" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }

            if (szGenerateNextDiagPage == "CI_0000004" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }
            if (szNextDiagPDFFileName == "")
            {
                return p_szGeneratedFileName;
            }
            else
            {
                MergePDF.MergePDFFiles(p_szDefaultPath + p_szGeneratedFileName, p_szDefaultPath + szNextDiagPDFFileName, p_szDefaultPath + szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf"));
               
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
        return szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf");

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

   

    protected void btnGenerateWCPDF_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            // GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, rdbListPDFType.SelectedValue);
            string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            string CaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            string szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string CaseNO = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
            // string speciality = Session["WC_Speciality"].ToString();
            WC_Bill_Generation _objNFBill = new WC_Bill_Generation();
            Bill_Sys_BillTransaction_BO objBill = new Bill_Sys_BillTransaction_BO();

            // commented and below line to fix to resolve -- PR-002-45
            string szSpecialty = objBill.GetDoctorSpeciality(hdnWCPDFBillNumber.Value, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (szSpecialty != null)
                szSpecialty = szSpecialty.Trim();

            string szFinalPath = _objNFBill.GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, rdbListPDFType.SelectedValue, szCompanyID, CmpName, UserId, UserName, CaseNO, szSpecialty, 0);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);
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


    protected void btnGenerateWCPDFAdd_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            // GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, rdbListPDFType.SelectedValue);
            string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            string CaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            string szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string CaseNO = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
            // string speciality = Session["WC_Speciality"].ToString();
            WC_Bill_Generation _objNFBill = new WC_Bill_Generation();
            Bill_Sys_BillTransaction_BO objBill = new Bill_Sys_BillTransaction_BO();

            // commented and below line to fix to resolve -- PR-002-45
            string szSpecialty = objBill.GetDoctorSpeciality(txtBillID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (szSpecialty != null)
                szSpecialty = szSpecialty.Trim();
            string szFinalPath = _objNFBill.GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, rdbListPDFType1.SelectedValue, szCompanyID, CmpName, UserId, UserName, CaseNO, szSpecialty, 0);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);
            pnlPDFWorkerCompAdd.Visible = false;

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

    #region "Update Bill Information"
    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _editOperation = new EditOperation();
        try
        {
            Session["BillID"] = Session["SZ_BILL_NUMBER"].ToString();
            if (Session["BillID"] != null)
            {
                #region "Save information into BillTransactionEO"
                BillTransactionEO _objBillEO = new BillTransactionEO();
                _objBillEO.SZ_BILL_NUMBER = Session["BillID"].ToString();
                _objBillEO.SZ_BILL_ID = Session["BillID"].ToString();
                _objBillEO.SZ_CASE_ID = txtCaseID.Text;
                _objBillEO.SZ_COMPANY_ID = txtCompanyID.Text;
                _objBillEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);
                _objBillEO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                _objBillEO.SZ_DOCTOR_ID = hndDoctorID.Value.ToString();
                _objBillEO.SZ_TYPE = ddlType.Text;
                _objBillEO.SZ_TESTTYPE = ""; //ddlTestType.Text;
                _objBillEO.FLAG = "UPDATE";
                #endregion
                ArrayList objALBillProcedureCodeEO = new ArrayList();
                ArrayList _objALBillProcedureCodeEO = new ArrayList();
                _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                string billno = Session["BillID"].ToString();
                ArrayList _arrayList;
                foreach (DataGridItem j in grdTransactionDetails.Items)
                {
                    if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();
                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

                        // 0 - SZ_BILL_TXN_DETAIL_ID
                        if (j.Cells[0].Text.ToString() != "" && j.Cells[0].Text.ToString() != "&nbsp;")
                        {
                            objBillProcedureCodeEO.SZ_BILL_TXN_DETAIL_ID = j.Cells[0].Text.ToString();
                        }
                        else
                        {
                            objBillProcedureCodeEO.SZ_BILL_TXN_DETAIL_ID = "";
                        }

                        // 1 - SZ_PROCEDURE_ID
                        objBillProcedureCodeEO.SZ_PROCEDURE_ID = j.Cells[2].Text.ToString();

                        // 2 - FL_AMOUNT
                        if (j.Cells[5].Text.ToString() != "&nbsp;")
                        {
                            objBillProcedureCodeEO.FL_AMOUNT = j.Cells[5].Text.ToString();
                        }
                        else
                        {
                            objBillProcedureCodeEO.FL_AMOUNT = "0";
                        }

                        // 3 - SZ_BILL_NUMBER
                        objBillProcedureCodeEO.SZ_BILL_NUMBER = billno;

                        // 4 - DT_DATE_OF_SERVICE
                        if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;")
                        {
                            objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(j.Cells[1].Text.ToString());
                        }
                        else
                        {
                            // objBillProcedureCodeEO.DT_DATE_OF_SERVICE = "";
                        }
                        objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        objBillProcedureCodeEO.I_UNIT = ((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString();
                        objBillProcedureCodeEO.FLT_PRICE = j.Cells[5].Text.ToString();
                        //changes by anupam 7Mar2011 -- start
                        string szCaseType = objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);

                        //prashant 8_mar 2011
                        //if (szCaseType == "WC000000000000000002")
                        //{
                        //    objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[9].Text.ToString();
                        //    objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[10].Text.ToString();
                        //}
                        //else
                        //{
                        //    objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                        //    objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                        //}
                        //if (j.Cells[9].Text.ToString() != "&nbsp;")
                        //{
                        //    objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[9].Text.ToString();
                        //    objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[10].Text.ToString();
                        //}
                        //else
                        //{
                        //     objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                        //     objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                        //}

                        if (j.Cells[9].Text.ToString() != "&nbsp;")
                        {
                            objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[9].Text.ToString();

                        }
                        else
                        {
                            objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";

                        } if (j.Cells[10].Text.ToString() != "&nbsp;" && j.Cells[10].Text.ToString() != "&nbsp;" && j.Cells[10].Text.ToString() != "&nbsp;")
                        {
                            objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[10].Text.ToString();
                        }
                        else
                        {
                            objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                        }


                        //===============
                        //changes by anupam 7Mar2011 -- end
                        // objBillProcedureCodeEO.I_UNIT = "1";
                        if (j.Cells[0].Text.ToString() != "" && j.Cells[0].Text.ToString() != "&nbsp;")
                        {
                            objBillProcedureCodeEO.SZ_TYPE_CODE_ID = j.Cells[8].Text.ToString();
                            objBillProcedureCodeEO.FLAG = "UPDATE";
                            _objALBillProcedureCodeEO.Add(objBillProcedureCodeEO);
                        }
                        else
                        {
                            objBillProcedureCodeEO.SZ_DOCTOR_ID = hndDoctorID.Value.ToString();
                            objBillProcedureCodeEO.SZ_CASE_ID = txtCaseID.Text;
                            objBillProcedureCodeEO.SZ_TYPE_CODE_ID = j.Cells[8].Text.ToString();
                            objBillProcedureCodeEO.FLAG = "ADD";
                            _objALBillProcedureCodeEO.Add(objBillProcedureCodeEO);
                        }
                    }
                }

                ArrayList objALEventRefferProcedureEO = new ArrayList();
                ArrayList EventList = new ArrayList();
                foreach (DataGridItem j in grdTransactionDetails.Items)
                {
                    if (j.Cells[0].Text.ToString() != "" && j.Cells[0].Text.ToString() != "&nbsp;")
                    {
                        if (!(EventList.Contains(j.Cells[12].Text)))
                            EventList.Add(j.Cells[12].Text);
                    }
                    else
                    {
                        if (!(EventList.Contains(j.Cells[12].Text)))
                            EventList.Add(j.Cells[12].Text);
                        EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                        objEventRefferProcedureEO.SZ_EVENT_DATE = j.Cells[1].Text;
                        objEventRefferProcedureEO.SZ_PROC_CODE = j.Cells[8].Text;
                        objEventRefferProcedureEO.I_EVENT_ID = j.Cells[12].Text;
                        objEventRefferProcedureEO.I_STATUS = "2";
                        objALEventRefferProcedureEO.Add(objEventRefferProcedureEO);
                    }
                }

                ArrayList objALBillDiagnosisCodeEO = new ArrayList();
                foreach (ListItem lstItem in lstDiagnosisCodes.Items)
                {
                    BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    _arrayList = new ArrayList();
                    _arrayList.Add(lstItem.Value.ToString());
                    objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID = lstItem.Value.ToString();
                    objALBillDiagnosisCodeEO.Add(objBillDiagnosisCodeEO);
                    _arrayList.Add(billno);
                }
                ArrayList objALDeletedProcCodes = new ArrayList();
                objALDeletedProcCodes = (ArrayList)Session["DELETED_PROC_CODES"];
                BillTransactionDAO objBT_DAO = new BillTransactionDAO();
                Result objResult = new Result();
                objResult = objBT_DAO.UpdateBillTransactions(_objBillEO, _objALBillProcedureCodeEO, objALBillDiagnosisCodeEO, objALDeletedProcCodes, objALEventRefferProcedureEO, EventList);
                Session["DELETED_PROC_CODES"] = null;
                if (objResult.msg_code == "SCC")
                {
                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_UPDATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                    double total = 0;
                    if (grdTransactionDetails.Items.Count > 0)
                    {
                        BillTransactionDAO objIslimit = new BillTransactionDAO();
                        string sz_procedure_group_id1 = grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                        string sz_visit_tyep1 = grdTransactionDetails.Items[0].Cells[13].Text.ToString();
                        string sz_PG_Id1 = objIslimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id1);
                        string szIsLimit = objIslimit.GET_IS_LIMITE(txtCompanyID.Text, sz_PG_Id1);

                        if (szIsLimit != "" && szIsLimit != "NULL")
                        {
                            for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                            {
                                grdTransactionDetails.Items[i].Cells[9].Text = "";
                                if (grdTransactionDetails.Items[i].Cells[5].Text != "" && grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;")
                                {
                                    total = total + Convert.ToDouble(grdTransactionDetails.Items[i].Cells[5].Text);
                                }
                                if (i == grdTransactionDetails.Items.Count - 1)
                                {


                                    // if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                                    {
                                        BillTransactionDAO objLimit = new BillTransactionDAO();
                                        string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                        string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                        string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                        string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                        //grdTransactionDetails.Items[i].Cells[9].Visible = true;
                                        //grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                        if (szReturn != "")
                                        {
                                            if (Convert.ToDouble(szReturn) < total)
                                            {
                                                grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                            }
                                            else
                                            {
                                                grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                            }
                                        }
                                        total = 0;
                                    }

                                }
                                else
                                {
                                    if (grdTransactionDetails.Items[i].Cells[1].Text != grdTransactionDetails.Items[i + 1].Cells[1].Text)
                                    {

                                        //if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                                        {
                                            BillTransactionDAO objLimit = new BillTransactionDAO();
                                            string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                            string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                            string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                            string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                            //grdTransactionDetails.Items[i].Cells[9].Visible=true;
                                            //grdTransactionDetails.Items[i].Cells[9].Text=szReturn;
                                            if (szReturn != "")
                                            {
                                                if (Convert.ToDouble(szReturn) < total)
                                                {
                                                    grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                                }
                                                else
                                                {
                                                    grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                                }
                                            }
                                            total = 0;

                                        }

                                    }
                                }
                            }
                        }
                    }
                    usrMessage.PutMessage(" Bill Updated successfully ! ");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                }
                else
                {
                    usrMessage.PutMessage(objResult.msg);
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    usrMessage.Show();
                }
            }
            BindTransactionDetailsGrid(Session["BillID"].ToString());
            BindLatestTransaction();
        }
        catch (SqlException objSqlExcp)
        {
            usrMessage.PutMessage(" Bill Number already exists");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
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
    #endregion

    #region "Remove Procedure Codes"

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        ArrayList objALRemoveProcCodes = new ArrayList();
        if (Session["DELETED_PROC_CODES"] != null)
        {

            objALRemoveProcCodes = (ArrayList)Session["DELETED_PROC_CODES"];
        }

        try
        {
            DataTable datatable = new DataTable();

            datatable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            datatable.Columns.Add("DT_DATE_OF_SERVICE");
            datatable.Columns.Add("SZ_PROCEDURE_ID");
            datatable.Columns.Add("SZ_PROCEDURAL_CODE");
            datatable.Columns.Add("SZ_CODE_DESCRIPTION");
            datatable.Columns.Add("FLT_AMOUNT");
            datatable.Columns.Add("I_UNIT");
            datatable.Columns.Add("SZ_TYPE_CODE_ID");
            datatable.Columns.Add("FLT_GROUP_AMOUNT");
            datatable.Columns.Add("I_GROUP_AMOUNT_ID");
            datatable.Columns.Add("I_EventID");
            datatable.Columns.Add("SZ_VISIT_TYPE");
            datatable.Columns.Add("BT_IS_LIMITE");
            DataRow dr;
            int flag = 0;
            if (grdTransactionDetails.Items.Count > 0)
            {
                foreach (DataGridItem j in grdTransactionDetails.Items)
                {
                    dr = datatable.NewRow();
                    if (j.Cells[0].Text.ToString() != "&nbsp;" && j.Cells[0].Text.ToString() != "") { dr["SZ_BILL_TXN_DETAIL_ID"] = j.Cells[0].Text.ToString(); } else { dr["SZ_BILL_TXN_DETAIL_ID"] = ""; }
                    dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(j.Cells[1].Text.ToString()).ToShortDateString();// Session["Date_Of_Service"].ToString();
                    dr["SZ_PROCEDURE_ID"] = j.Cells[2].Text.ToString();
                    dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                    dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                    dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                    if (((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString(); } }
                    dr["SZ_TYPE_CODE_ID"] = j.Cells[8].Text.ToString();
                    dr["FLT_GROUP_AMOUNT"] = j.Cells[9].Text.ToString();
                    dr["I_GROUP_AMOUNT_ID"] = j.Cells[10].Text.ToString();
                    dr["I_EventID"] = j.Cells[12].Text.ToString();
                    dr["SZ_VISIT_TYPE"] = j.Cells[13].Text.ToString();
                    dr["BT_IS_LIMITE"] = j.Cells[14].Text.ToString();
                    datatable.Rows.Add(dr);
                }
            }
            if (grdTransactionDetails.Items.Count > 0)
            {
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    if (grdTransactionDetails.Items[i].Cells[0].Text != "" && grdTransactionDetails.Items[i].Cells[0].Text != "&nbsp;")
                    {
                        CheckBox chkRemove = (CheckBox)grdTransactionDetails.Items[i].FindControl("chkSelect");
                        if (chkRemove.Checked)
                        {
                            string date = grdTransactionDetails.Items[i].Cells[1].Text;
                            string procCode = grdTransactionDetails.Items[i].Cells[3].Text;
                            for (int j = 0; j < datatable.Rows.Count; j++)
                            {
                                if (procCode == datatable.Rows[j][3].ToString() && DateTime.Compare(Convert.ToDateTime(date), Convert.ToDateTime(datatable.Rows[j][1].ToString())) == 0)
                                {
                                    objALRemoveProcCodes.Add(datatable.Rows[j][0].ToString());
                                    datatable.Rows[j].Delete();
                                    flag = 1;
                                    break;
                                }
                                else
                                {
                                    flag = 2;
                                }
                            }

                        }
                    }
                    else
                    {
                        CheckBox chkRemove = (CheckBox)grdTransactionDetails.Items[i].FindControl("chkSelect");
                        if (chkRemove.Checked)
                        {
                            string date = grdTransactionDetails.Items[i].Cells[1].Text;
                            string procCode = grdTransactionDetails.Items[i].Cells[3].Text;
                            for (int j = 0; j < datatable.Rows.Count; j++)
                            {

                                if (procCode == datatable.Rows[j][3].ToString() && DateTime.Compare(Convert.ToDateTime(date), Convert.ToDateTime(datatable.Rows[j][1].ToString())) == 0)
                                {
                                    objALRemoveProcCodes.Add(datatable.Rows[j][0].ToString());
                                    datatable.Rows[j].Delete();
                                    flag = 1;
                                    break;
                                }
                                else
                                {
                                    flag = 2;
                                }
                            }
                        }
                    }
                }
            }
            grdTransactionDetails.DataSource = datatable;
            grdTransactionDetails.DataBind();
            double total = 0;
            if (grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO objIslimit = new BillTransactionDAO();
                string sz_procedure_group_id1 = grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                string sz_visit_tyep1 = grdTransactionDetails.Items[0].Cells[13].Text.ToString();
                string sz_PG_Id1 = objIslimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id1);
                string szIsLimit = objIslimit.GET_IS_LIMITE(txtCompanyID.Text, sz_PG_Id1);

                if (szIsLimit != "" && szIsLimit != "NULL")
                {
                    for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                    {
                        // grdTransactionDetails.Items[i].Cells[9].Text = "";
                        if (grdTransactionDetails.Items[i].Cells[5].Text != "" && grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;")
                        {
                            total = total + Convert.ToDouble(grdTransactionDetails.Items[i].Cells[5].Text);
                        }
                        if (i == grdTransactionDetails.Items.Count - 1)
                        {


                            // if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                            {
                                BillTransactionDAO objLimit = new BillTransactionDAO();
                                string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                //grdTransactionDetails.Items[i].Cells[9].Visible = true;
                                //grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                if (szReturn != "")
                                {
                                    if (Convert.ToDouble(szReturn) < total)
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                    }
                                    else
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                    }
                                }
                                total = 0;
                            }

                        }
                        else
                        {
                            if (grdTransactionDetails.Items[i].Cells[1].Text != grdTransactionDetails.Items[i + 1].Cells[1].Text)
                            {

                                //if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                                {
                                    BillTransactionDAO objLimit = new BillTransactionDAO();
                                    string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                    string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                    string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                    string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                    //grdTransactionDetails.Items[i].Cells[9].Visible=true;
                                    //grdTransactionDetails.Items[i].Cells[9].Text=szReturn;
                                    if (szReturn != "")
                                    {
                                        if (Convert.ToDouble(szReturn) < total)
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                        }
                                        else
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                        }
                                    }
                                    total = 0;

                                }

                            }
                        }
                    }
                }
            }
            // add deleted procedure codes in session
            Session["DELETED_PROC_CODES"] = objALRemoveProcCodes;
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

    #region "extddlDoctor Change event"
    protected void extddlDoctor_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (extddlDoctor.SelectedValue != "NA")
        {
            GetProcedureCode(extddlDoctor.SelectedValue);
            Session["TEMP_DOCTOR_ID"] = extddlDoctor.SelectedValue;
            _bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            string keyValue = _bill_Sys_LoginBO.getDefaultSettings(txtCompanyID.Text, "SS00005");
            if (keyValue == "1")
            {
                BindGrid();
                lblDateOfService.Style.Add("visibility", "hidden");//.Visible=false;
                txtDateOfservice.Style.Add("visibility", "hidden");//.Visible=false;
                Image1.Style.Add("visibility", "hidden");//.Visible=false;

                lblGroupServiceDate.Style.Add("visibility", "hidden");//.Visible=false;
                txtGroupDateofService.Style.Add("visibility", "hidden");//.Visible=false;
                imgbtnDateofService.Style.Add("visibility", "hidden");//.Visible=false;
            }
            else
            {
                #region "Bind Grid"

                DataTable dt = new DataTable();

                dt.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
                dt.Columns.Add("DT_DATE_OF_SERVICE");
                dt.Columns.Add("SZ_PROCEDURE_ID");
                dt.Columns.Add("SZ_PROCEDURAL_CODE");
                dt.Columns.Add("SZ_CODE_DESCRIPTION");
                dt.Columns.Add("FLT_AMOUNT");
                dt.Columns.Add("FACTOR_AMOUNT");
                dt.Columns.Add("FACTOR");
                dt.Columns.Add("PROC_AMOUNT");
                dt.Columns.Add("DOCT_AMOUNT");
                dt.Columns.Add("I_UNIT");
                dt.Columns.Add("SZ_TYPE_CODE_ID");

                // added amod. 02-feb-2010. These columns were not
                dt.Columns.Add("FLT_GROUP_AMOUNT");
                dt.Columns.Add("I_GROUP_AMOUNT_ID");

                DataRow dr;

                foreach (DataGridItem j in grdTransactionDetails.Items)
                {
                    if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                    {
                        dr = dt.NewRow();
                        if (j.Cells[0].Text.ToString() != "&nbsp;" && j.Cells[0].Text.ToString() != "") { dr["SZ_BILL_TXN_DETAIL_ID"] = j.Cells[0].Text.ToString(); } else { dr["SZ_BILL_TXN_DETAIL_ID"] = ""; }
                        dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(j.Cells[1].Text.ToString()).ToShortDateString();// Session["Date_Of_Service"].ToString();
                        dr["SZ_PROCEDURE_ID"] = j.Cells[2].Text.ToString();
                        dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                        dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                        if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0) { dr["FACTOR_AMOUNT"] = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString(); }
                        if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0) { dr["FACTOR"] = ((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString(); }
                        dr["FLT_AMOUNT"] = j.Cells[6].Text.ToString();
                        if (((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString(); } }
                        dr["PROC_AMOUNT"] = j.Cells[9].Text.ToString();
                        dr["DOCT_AMOUNT"] = j.Cells[10].Text.ToString();
                        dr["SZ_TYPE_CODE_ID"] = j.Cells[12].Text.ToString();

                        dr["FLT_GROUP_AMOUNT"] = j.Cells[13].Text.ToString();
                        dr["I_GROUP_AMOUNT_ID"] = j.Cells[14].Text.ToString();
                        dt.Rows.Add(dr);
                    }
                }

                grdTransactionDetails.DataSource = dt;
                grdTransactionDetails.DataBind();
                #endregion
            }

            #region "Disply Unit Column if speciality have unit bit set to true."

            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[6].Visible = true;
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
            }
            #endregion
            #region "Get Diagnosis code associated with case according to speciality of Doctor"
            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();
            lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, hndDoctorID.Value.ToString(), txtCompanyID.Text).Tables[0];
            lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            lstDiagnosisCodes.DataValueField = "CODE";
            lstDiagnosisCodes.DataBind();    //Bind select Doctor
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
            #endregion
        }
        else
        {
            grdAllReports.DataSource = null;
            grdAllReports.DataBind();
        }
    }
    #endregion

    #region "Code For Diagnosis Code pop up."

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        ArrayList _arrayList;
        Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            for (int i = 0; i < grdDiagonosisCode.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode");
                if (chk.Checked)
                {
                    ListItem objListItem = new ListItem(grdDiagonosisCode.Items[i].Cells[2].Text + '-' + grdDiagonosisCode.Items[i].Cells[4].Text, grdDiagonosisCode.Items[i].Cells[1].Text);
                    if (!lstDiagnosisCodes.Items.Contains(objListItem))
                    {
                        lstDiagnosisCodes.Items.Add(objListItem);
                    }


                }
            }
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();

            //ModalPopupExtender1.Show();

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

    protected void btnAddPreferredDiagnosis_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList _arrayList;
        Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            for (int i = 0; i < grdPreferredDiagonosisCode.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdPreferredDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode");
                if (chk.Checked)
                {
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem(grdPreferredDiagonosisCode.Items[i].Cells[2].Text + '-' + grdPreferredDiagonosisCode.Items[i].Cells[4].Text, grdPreferredDiagonosisCode.Items[i].Cells[1].Text);
                    if (!lstDiagnosisCodes.Items.Contains(objListItem))
                    {
                        lstDiagnosisCodes.Items.Add(objListItem);
                    }
                }
            }
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();

            //ModalPopupExtender1.Show();

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

    private void BindDiagnosisGrid(string typeid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdDiagonosisCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid);
            grdDiagonosisCode.DataBind();

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
    private void BindPreferredListDiagnosisGrid(string typeid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdPreferredDiagonosisCode.DataSource = _digosisCodeBO.GetPreferredListDiagnosisCode(txtCompanyID.Text, typeid);
            grdPreferredDiagonosisCode.DataBind();

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

    private void BindDiagnosisGrid(string typeid, string code, string description)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdDiagonosisCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid, code, description);
            grdDiagonosisCode.DataBind();

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

    private void BindAddPreferredListDiagnosisGrid(string typeid, string code, string description)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdPreferredDiagonosisCode.DataSource = _digosisCodeBO.GetDiagnosisCodePreferredList(txtCompanyID.Text, typeid, code, description);
            grdPreferredDiagonosisCode.DataBind();
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

    protected void grdDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            if (extddlDiagnosisType.Text == "NA")
            {
                BindDiagnosisGrid("");
            }
            else
            {
                BindDiagnosisGrid(extddlDiagnosisType.Text);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);
            ModalPopupExtender1.Show();
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

    protected void grdPreferredDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdPreferredDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            if (extddlPreferredDiagnosisType.Text == "NA")
            {
                BindPreferredListDiagnosisGrid("");
            }
            else
            {
                BindPreferredListDiagnosisGrid(extddlPreferredDiagnosisType.Text);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);
            
            ModalPopupExtender2.Show();
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

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindDiagnosisGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);
            ModalPopupExtender1.Show();
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

    protected void btnPreferredSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_ADD_TO_PREFERED_LIST_FOR_DIAGNOSIS_CODE == "1")
            {
                grdPreferredDiagonosisCode.CurrentPageIndex = 0;
                BindAddPreferredListDiagnosisGrid(extddlPreferredDiagnosisType.Text, txtPreferedCode.Text, txtPreferedDesc.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);
                ModalPopupExtender2.Show();
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

    protected void lnkbtnRemoveDiag_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        ArrayList _arrayList;
        try
        {
            for (int i = 0; i < lstDiagnosisCodes.Items.Count; i++)
            {
                if (lstDiagnosisCodes.Items[i].Selected == true)
                {
                    //#region  Prashant
                    //_arrayList = new ArrayList();
                    //_arrayList.Add(lstDiagnosisCodes.Items[i].Value.ToString());
                    //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    //{
                    //    _arrayList.Add("");
                    //}
                    //else
                    //{
                    //    _arrayList.Add("");
                    //}

                    //_arrayList.Add(txtCaseID.Text);
                    //_arrayList.Add(txtCompanyID.Text);
                    //if (_associateDiagnosisCodeBO.DeleteAssociateDiagonisCode(_arrayList))
                    //{
                    //}
                    //else
                    //{
                    //    // szDiagIDs += "  " + dgiItem.Cells[2].Text.ToString() + ",";
                    //}
                    //#endregion                   


                    lstDiagnosisCodes.Items.Remove(lstDiagnosisCodes.Items[i]);
                    i = i - 1;


                }
            }
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
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

    #region "Add Service"

    protected void Button1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (lstDiagnosisCodes.Items.Count == 0)
            {
                lstDiagnosisCodes.Items.Clear();
                Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();
                lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, hndDoctorID.Value, txtCompanyID.Text).Tables[0];
                lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                lstDiagnosisCodes.DataValueField = "CODE";
                lstDiagnosisCodes.DataBind();    //Bind select Doctor
                lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            dt.Columns.Add("DT_DATE_OF_SERVICE");
            dt.Columns.Add("SZ_PROCEDURE_ID");
            dt.Columns.Add("SZ_PROCEDURAL_CODE");
            dt.Columns.Add("SZ_CODE_DESCRIPTION");
            dt.Columns.Add("FLT_AMOUNT");
            dt.Columns.Add("I_UNIT");
            dt.Columns.Add("SZ_TYPE_CODE_ID");
            dt.Columns.Add("FLT_GROUP_AMOUNT");
            dt.Columns.Add("I_GROUP_AMOUNT_ID");
            dt.Columns.Add("I_EventID");
            dt.Columns.Add("SZ_VISIT_TYPE");
            dt.Columns.Add("BT_IS_LIMITE");
            DataRow dr;
            int iFlagLimite = 0;
            string DOS = "";
            string eventid = "";



            foreach (DataGridItem j in grdTransactionDetails.Items)
            {
                if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                {
                    dr = dt.NewRow();
                    if (j.Cells[0].Text.ToString() != "&nbsp;" && j.Cells[0].Text.ToString() != "") { dr["SZ_BILL_TXN_DETAIL_ID"] = j.Cells[0].Text.ToString(); } else { dr["SZ_BILL_TXN_DETAIL_ID"] = ""; }
                    dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(j.Cells[1].Text.ToString()).ToShortDateString();// Session["Date_Of_Service"].ToString();
                    DOS = Convert.ToDateTime(j.Cells[1].Text.ToString()).ToShortDateString();// Session["Date_Of_Service"].ToString();
                    dr["SZ_PROCEDURE_ID"] = j.Cells[2].Text.ToString();
                    dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                    dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                    dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                    if (((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString(); } }
                    dr["SZ_TYPE_CODE_ID"] = j.Cells[8].Text.ToString();
                    BillTransactionDAO objLimit = new BillTransactionDAO();
                    string sz_procedure_group_id = j.Cells[2].Text.ToString();
                    string sz_visit_tyep = j.Cells[13].Text.ToString();
                    string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                    string szIsLimit = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                    if (szIsLimit != "")
                    {
                        dr["FLT_GROUP_AMOUNT"] = "";
                    }
                    else
                    {
                        dr["FLT_GROUP_AMOUNT"] = j.Cells[9].Text.ToString();
                    }


                    dr["I_GROUP_AMOUNT_ID"] = j.Cells[10].Text.ToString();
                    dr["I_EventID"] = j.Cells[12].Text.ToString();
                    //j.Cells[13].Text = "";
                    dr["SZ_VISIT_TYPE"] = j.Cells[13].Text.ToString();
                    dr["BT_IS_LIMITE"] = j.Cells[14].Text.ToString();
                    eventid = j.Cells[12].Text.ToString();
                    dt.Rows.Add(dr);
                }
            }

            string[] dateOfService = txtDateOfservice.Text.Split(',');
            _bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            foreach (DataGridItem dgItem in grdAllReports.Items)
            {
                if (((CheckBox)dgItem.Cells[0].FindControl("chkselect")).Checked == true)
                {
                    foreach (DataGridItem j in grdProcedure.Items)
                    {
                        CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                        CheckBox chkLimit = (CheckBox)dgItem.FindControl("chkLimit");
                        //if (chkLimit.Checked)
                        {
                            iFlagLimite = 1;
                        }
                        //else
                        //{
                        //    iFlagLimite = 0;
                        //}
                        string sz_visit_type = dgItem.Cells[5].Text.ToString();
                        if (chkSelect.Checked)
                        {
                            dr = dt.NewRow();
                            dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                            // dr["DT_DATE_OF_SERVICE"] = dgItem.Cells[2].Text.ToString();//txtDateOfservice.Text;
                            dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(dgItem.Cells[2].Text.ToString()).ToShortDateString();
                            dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();
                            dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                            dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                            dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                            dr["I_UNIT"] = "1";
                            dr["SZ_TYPE_CODE_ID"] = j.Cells[2].Text.ToString();
                            dr["I_EventID"] = dgItem.Cells[4].Text.ToString();
                            dr["SZ_VISIT_TYPE"] = sz_visit_type;
                            if (iFlagLimite == 1)
                            {
                                dr["BT_IS_LIMITE"] = "1";
                            }
                            else
                            {
                                dr["BT_IS_LIMITE"] = "0";
                            }
                            dt.Rows.Add(dr);

                        }
                    }
                }
            }

            if (grdAllReports.Items.Count == 0)
            {
                if (grdCompleteVisit.Items.Count > 0) //check
                {
                    int flag = 0;
                    foreach (DataGridItem item in grdCompleteVisit.Items) // check
                    {
                        CheckBox chk1 = (CheckBox)item.FindControl("chkSelectItem");
                        if (chk1.Checked)
                        {
                            string visit_date = item.Cells[1].Text;  //
                            foreach (DataGridItem j in grdProcedure.Items)
                            {
                                CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                                CheckBox chkLimit = (CheckBox)item.FindControl("chkLimit");
                                //if (chkLimit.Checked)
                                {
                                    iFlagLimite = 1;
                                }
                                //else
                                //{
                                //    iFlagLimite = 0;
                                //}
                                string sz_visit_type = item.Cells[3].Text.ToString(); //

                                if (chkSelect.Checked)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i][0].ToString() == j.Cells[1].Text && DateTime.Compare(Convert.ToDateTime(visit_date), Convert.ToDateTime(dt.Rows[i][1].ToString())) == 0)
                                        {
                                            flag = 1;
                                            break;
                                        }
                                        else
                                        {
                                            flag = 2;
                                        }
                                    }
                                    if (flag == 2 || flag == 0)
                                    {
                                        dr = dt.NewRow();
                                        dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                        // dr["DT_DATE_OF_SERVICE"] = visit_date;//txtDateOfservice.Text;
                                        dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(visit_date).ToShortDateString();
                                        dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();
                                        dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                                        dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                                        dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                                        dr["I_UNIT"] = "1";
                                        dr["SZ_TYPE_CODE_ID"] = j.Cells[2].Text.ToString();
                                        dr["I_EventID"] = item.Cells[7].Text;
                                        dr["SZ_VISIT_TYPE"] = sz_visit_type;
                                        if (iFlagLimite == 1)
                                        {
                                            dr["BT_IS_LIMITE"] = "1";
                                        }
                                        else
                                        {
                                            dr["BT_IS_LIMITE"] = "0";
                                        }
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (grdCompleteVisit.Visible) //check
                    {
                        int flag = 0;
                        foreach (DataGridItem item in grdTransactionDetails.Items)
                        {
                            string visit_date = item.Cells[1].Text;
                            if (visit_date != "")
                            {
                                foreach (DataGridItem j in grdProcedure.Items)
                                {
                                    CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");

                                    if (chkSelect.Checked)
                                    {
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            if (dt.Rows[i][0].ToString() == j.Cells[1].Text && DateTime.Compare(Convert.ToDateTime(visit_date), Convert.ToDateTime(dt.Rows[i][1].ToString())) == 0)
                                            {
                                                flag = 1;
                                                break;
                                            }
                                            else
                                            {
                                                flag = 2;
                                            }
                                        }

                                        if (flag == 2)
                                        {
                                            dr = dt.NewRow();
                                            dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                            // dr["DT_DATE_OF_SERVICE"] = visit_date;//txtDateOfservice.Text;
                                            dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(visit_date).ToShortDateString();
                                            dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();
                                            dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                                            dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                                            dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                                            dr["I_UNIT"] = "1";
                                            dr["SZ_TYPE_CODE_ID"] = j.Cells[2].Text.ToString();
                                            dr["I_EventID"] = item.Cells[12].Text;
                                            dt.Rows.Add(dr);
                                        }
                                    }
                                }
                            }
                        }


                    }
                    else
                    {
                        foreach (DataGridItem j in grdProcedure.Items)
                        {
                            CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                            if (chkSelect.Checked)
                            {
                                dr = dt.NewRow();
                                dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                // dr["DT_DATE_OF_SERVICE"] = DOS;
                                dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(DOS).ToShortDateString();
                                dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();
                                dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                                dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                                dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                                dr["I_UNIT"] = "1";
                                dr["SZ_TYPE_CODE_ID"] = j.Cells[2].Text.ToString();
                                dr["I_EventID"] = "";
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridItem j in grdProcedure.Items)
                {
                    CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                    if (chkSelect.Checked)
                    {
                        dr = dt.NewRow();
                        dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                        //dr["DT_DATE_OF_SERVICE"] = txtBillDate.Text;//txtDateOfservice.Text;
                        dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(txtBillDate.Text).ToShortDateString();
                        dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();
                        dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                        dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                        dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                        dr["I_UNIT"] = "1";
                        dr["SZ_TYPE_CODE_ID"] = j.Cells[2].Text.ToString();
                        dr["I_EventID"] = "";
                        dt.Rows.Add(dr);
                    }
                }
            }
            DataView dv = new DataView();

            dv = dt.DefaultView;

            dv.Sort = "DT_DATE_OF_SERVICE";
            double total = 0;
            grdTransactionDetails.DataSource = dt;
            grdTransactionDetails.DataBind();
            if (grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO objIslimit = new BillTransactionDAO();
                string sz_procedure_group_id1 = grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                string sz_visit_tyep1 = grdTransactionDetails.Items[0].Cells[13].Text.ToString();
                string sz_PG_Id1 = objIslimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id1);
                string szIsLimit = objIslimit.GET_IS_LIMITE(txtCompanyID.Text, sz_PG_Id1);

                if (szIsLimit != "" && szIsLimit != "NULL")
                {
                    for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                    {
                        if (grdTransactionDetails.Items[i].Cells[5].Text != "" && grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;")
                        {
                            total = total + Convert.ToDouble(grdTransactionDetails.Items[i].Cells[5].Text);
                        }
                        if (i == grdTransactionDetails.Items.Count - 1)
                        {


                            // if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                            {
                                BillTransactionDAO objLimit = new BillTransactionDAO();
                                string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                //grdTransactionDetails.Items[i].Cells[9].Visible = true;
                                //grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                if (szReturn != "")
                                {
                                    if (Convert.ToDouble(szReturn) < total)
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                    }
                                    else
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                    }
                                }
                                total = 0;
                            }

                        }
                        else
                        {
                            if (grdTransactionDetails.Items[i].Cells[1].Text != grdTransactionDetails.Items[i + 1].Cells[1].Text)
                            {

                                //if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                                {
                                    BillTransactionDAO objLimit = new BillTransactionDAO();
                                    string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                    string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                    string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                    string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                    //grdTransactionDetails.Items[i].Cells[9].Visible=true;
                                    //grdTransactionDetails.Items[i].Cells[9].Text=szReturn;
                                    if (szReturn != "")
                                    {
                                        if (Convert.ToDouble(szReturn) < total)
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                        }
                                        else
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                        }
                                    }
                                    total = 0;

                                }

                            }
                        }
                    }
                }
            }

            #region "Disply Unit Column if speciality have unit bit set to true."
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[6].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[7].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[6].Visible = false;
            }
            #endregion
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

    #endregion

    #region "Add Group Service"
    protected void lnkAddGroupService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (lstDiagnosisCodes.Items.Count == 0)
            {
                lstDiagnosisCodes.Items.Clear();
                Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();
                lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, hndDoctorID.Value, txtCompanyID.Text).Tables[0];
                lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                lstDiagnosisCodes.DataValueField = "CODE";
                lstDiagnosisCodes.DataBind();    //Bind select Doctor
                lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
            }


            DataTable dt = new DataTable();
            dt.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            dt.Columns.Add("DT_DATE_OF_SERVICE");
            dt.Columns.Add("SZ_PROCEDURE_ID");
            dt.Columns.Add("SZ_PROCEDURAL_CODE");
            dt.Columns.Add("SZ_CODE_DESCRIPTION");
            dt.Columns.Add("FLT_AMOUNT");
            dt.Columns.Add("I_UNIT");
            dt.Columns.Add("SZ_TYPE_CODE_ID");
            dt.Columns.Add("FLT_GROUP_AMOUNT");
            dt.Columns.Add("I_GROUP_AMOUNT_ID");
            dt.Columns.Add("I_EVENTID");
            dt.Columns.Add("SZ_VISIT_TYPE");
            dt.Columns.Add("BT_IS_LIMITE");
            DataRow dr;
            int iFlagLimite = 0;
            foreach (DataGridItem j in grdTransactionDetails.Items)
            {
                dr = dt.NewRow();
                if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                {
                    if (j.Cells[0].Text.ToString() != "&nbsp;" && j.Cells[0].Text.ToString() != "") { dr["SZ_BILL_TXN_DETAIL_ID"] = j.Cells[0].Text.ToString(); } else { dr["SZ_BILL_TXN_DETAIL_ID"] = ""; }
                    dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(j.Cells[1].Text.ToString()).ToShortDateString();// Session["Date_Of_Service"].ToString();
                    dr["SZ_PROCEDURE_ID"] = j.Cells[2].Text.ToString();
                    dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                    dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                    dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                    // dr["FLT_AMOUNT"] = "";
                    if (((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString(); } }
                    dr["SZ_TYPE_CODE_ID"] = j.Cells[8].Text.ToString();
                    //dr["FLT_GROUP_AMOUNT"] = j.Cells[9].Text.ToString();
                    //dr["FLT_GROUP_AMOUNT"] = "";
                    BillTransactionDAO objLimit = new BillTransactionDAO();
                    string sz_procedure_group_id = j.Cells[2].Text.ToString();
                    string sz_visit_tyep = j.Cells[13].Text.ToString();
                    string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);

                    string szIsLimit = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                    if (szIsLimit != "")
                    {
                        dr["FLT_GROUP_AMOUNT"] = "";
                    }
                    else
                    {
                        dr["FLT_GROUP_AMOUNT"] = j.Cells[9].Text.ToString();
                    }
                    dr["I_GROUP_AMOUNT_ID"] = j.Cells[10].Text.ToString();
                    dr["I_EventID"] = j.Cells[12].Text.ToString();
                    dr["SZ_VISIT_TYPE"] = j.Cells[13].Text.ToString();
                    dr["BT_IS_LIMITE"] = j.Cells[14].Text.ToString();
                    dt.Rows.Add(dr);
                }
            }
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            string[] dateOfService = txtGroupDateofService.Text.Split(',');
            _bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            string keyValue = _bill_Sys_LoginBO.getDefaultSettings(txtCompanyID.Text, "SS00005");
            if (keyValue == "1")
            {
                foreach (DataGridItem dgItem in grdAllReports.Items)
                {
                    if (((CheckBox)dgItem.Cells[0].FindControl("chkselect")).Checked == true)
                    {
                        string sz_visit_type = dgItem.Cells[5].Text.ToString();
                        foreach (DataGridItem j in grdGroupProcCodeService.Items)
                        {
                            CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                            CheckBox chkLimit = (CheckBox)dgItem.FindControl("chkLimit");
                            /// if (chkLimit.Checked)
                            {
                                iFlagLimite = 1;
                            }
                            //else
                            //{
                            //    iFlagLimite = 0;
                            //}
                            if (chkSelect.Checked)
                            {
                                DataSet ds = _bill_Sys_BillTransaction.GroupProcedureCodeList(j.Cells[1].Text.ToString(), txtCompanyID.Text, j.Cells[2].Text.ToString());
                                int rowSt = 1;
                                foreach (DataRow dtRow in ds.Tables[0].Rows)
                                {
                                    dr = dt.NewRow();
                                    dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                    //   dr["DT_DATE_OF_SERVICE"] = dgItem.Cells[2].Text.ToString();//txtGroupDateofService.Text;
                                    dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(dgItem.Cells[2].Text.ToString()).ToShortDateString();
                                    dr["SZ_PROCEDURE_ID"] = dtRow.ItemArray.GetValue(0);
                                    dr["SZ_PROCEDURAL_CODE"] = dtRow.ItemArray.GetValue(2);
                                    dr["SZ_CODE_DESCRIPTION"] = dtRow.ItemArray.GetValue(3);
                                    dr["FLT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                    dr["I_UNIT"] = "1";
                                    dr["SZ_TYPE_CODE_ID"] = dtRow.ItemArray.GetValue(1);
                                    if (rowSt == ds.Tables[0].Rows.Count)
                                    {
                                        if (j.Cells[4].Text.ToString() != "") { dr["FLT_GROUP_AMOUNT"] = j.Cells[4].Text.ToString(); }
                                    }
                                    if (j.Cells[3].Text.ToString() != "") { dr["I_GROUP_AMOUNT_ID"] = j.Cells[3].Text.ToString(); }
                                    dr["I_EventID"] = dgItem.Cells[4].Text.ToString();
                                    if (iFlagLimite == 1)
                                    {
                                        dr["BT_IS_LIMITE"] = "1";
                                    }
                                    else
                                    {
                                        dr["BT_IS_LIMITE"] = "0";
                                    }
                                    dr["SZ_VISIT_TYPE"] = sz_visit_type;
                                    dt.Rows.Add(dr);
                                    rowSt = rowSt + 1;
                                }
                            }
                        }
                    }
                }

                if (grdAllReports.Items.Count == 0)
                {
                    if (grdCompleteVisit.Items.Count > 0) //check
                    {
                        int flag = 0;
                        foreach (DataGridItem item in grdCompleteVisit.Items)//check
                        {
                            CheckBox chk1 = (CheckBox)item.FindControl("chkSelectItem");
                            if (chk1.Checked)
                            {
                                string visit_date = item.Cells[1].Text;  //
                                foreach (DataGridItem j in grdGroupProcCodeService.Items)
                                {
                                    CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                                    CheckBox chkLimit = (CheckBox)item.FindControl("chkLimit");
                                    //if (chkLimit.Checked)
                                    {
                                        iFlagLimite = 1;
                                    }
                                    //else
                                    //{
                                    //    iFlagLimite = 0;
                                    //}
                                    if (chkSelect.Checked)
                                    {
                                        DataSet ds = _bill_Sys_BillTransaction.GroupProcedureCodeList(j.Cells[1].Text.ToString(), txtCompanyID.Text, j.Cells[2].Text.ToString());
                                        int rowSt = 1;
                                        foreach (DataRow dtRow in ds.Tables[0].Rows)
                                        {
                                            //for (int i = 0; i < dt.Rows.Count; i++)
                                            //{
                                            //    if (dt.Rows[i][2].ToString() == dtRow.ItemArray.GetValue(0).ToString() && DateTime.Compare(Convert.ToDateTime(visit_date), Convert.ToDateTime(dt.Rows[i][1].ToString())) == 0)
                                            //    {
                                            //        flag = 1;
                                            //        break;
                                            //    }
                                            //    else
                                            //    {
                                            //        flag = 2;
                                            //    }
                                            //}
                                            //if (flag == 2 || flag == 0)
                                            //{
                                            dr = dt.NewRow();
                                            dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                            // dr["DT_DATE_OF_SERVICE"] = visit_date; // dtServiceDate.ToString();//txtGroupDateofService.Text;
                                            dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(visit_date).ToShortDateString();
                                            dr["SZ_PROCEDURE_ID"] = dtRow.ItemArray.GetValue(0);
                                            dr["SZ_PROCEDURAL_CODE"] = dtRow.ItemArray.GetValue(2);
                                            dr["SZ_CODE_DESCRIPTION"] = dtRow.ItemArray.GetValue(3);
                                            dr["FLT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                            dr["I_UNIT"] = "1";
                                            dr["SZ_TYPE_CODE_ID"] = dtRow.ItemArray.GetValue(1);
                                            if (rowSt == ds.Tables[0].Rows.Count)
                                            {
                                                if (j.Cells[4].Text.ToString() != "") { dr["FLT_GROUP_AMOUNT"] = j.Cells[4].Text.ToString(); }
                                            }
                                            if (j.Cells[3].Text.ToString() != "") { dr["I_GROUP_AMOUNT_ID"] = j.Cells[3].Text.ToString(); }
                                            dr["I_EventID"] = item.Cells[7].Text;
                                            dr["SZ_VISIT_TYPE"] = item.Cells[3].Text; //
                                            if (iFlagLimite == 1)
                                            {
                                                dr["BT_IS_LIMITE"] = "1";
                                            }
                                            else
                                            {
                                                dr["BT_IS_LIMITE"] = "0";
                                            }
                                            dt.Rows.Add(dr);
                                            rowSt = rowSt + 1;
                                            // }
                                        }
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        int flag = 0;
                        foreach (DataGridItem item in grdTransactionDetails.Items)
                        {
                            string visit_date = item.Cells[1].Text;
                            foreach (DataGridItem j in grdGroupProcCodeService.Items)
                            {
                                CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                                if (chkSelect.Checked)
                                {

                                    DataSet ds = _bill_Sys_BillTransaction.GroupProcedureCodeList(j.Cells[1].Text.ToString(), txtCompanyID.Text, j.Cells[2].Text.ToString());
                                    int rowSt = 1;
                                    foreach (DataRow dtRow in ds.Tables[0].Rows)
                                    {
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            if (visit_date != "")
                                            {
                                                if (dt.Rows[i][2].ToString() == dtRow.ItemArray.GetValue(0).ToString() && DateTime.Compare(Convert.ToDateTime(visit_date), Convert.ToDateTime(dt.Rows[i][1].ToString())) == 0)
                                                {
                                                    flag = 1;
                                                    break;
                                                }
                                                else
                                                {
                                                    flag = 2;
                                                }
                                            }
                                            else
                                            {
                                                flag = 0;
                                            }
                                        }
                                        if (flag == 2)
                                        {
                                            dr = dt.NewRow();
                                            dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                            dr["DT_DATE_OF_SERVICE"] = visit_date; // dtServiceDate.ToString();//txtGroupDateofService.Text;
                                            dr["SZ_PROCEDURE_ID"] = dtRow.ItemArray.GetValue(0);
                                            dr["SZ_PROCEDURAL_CODE"] = dtRow.ItemArray.GetValue(2);
                                            dr["SZ_CODE_DESCRIPTION"] = dtRow.ItemArray.GetValue(3);
                                            dr["FLT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                            dr["I_UNIT"] = "1";
                                            dr["SZ_TYPE_CODE_ID"] = dtRow.ItemArray.GetValue(1);
                                            if (rowSt == ds.Tables[0].Rows.Count)
                                            {
                                                if (j.Cells[4].Text.ToString() != "") { dr["FLT_GROUP_AMOUNT"] = j.Cells[4].Text.ToString(); }
                                            }
                                            if (j.Cells[3].Text.ToString() != "") { dr["I_GROUP_AMOUNT_ID"] = j.Cells[3].Text.ToString(); }
                                            dr["I_EventID"] = item.Cells[12].Text;
                                            dt.Rows.Add(dr);
                                            rowSt = rowSt + 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridItem j in grdGroupProcCodeService.Items)
                {
                    CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                    if (chkSelect.Checked)
                    {
                        DataSet ds = _bill_Sys_BillTransaction.GroupProcedureCodeList(j.Cells[1].Text.ToString(), txtCompanyID.Text, j.Cells[2].Text.ToString());
                        int rowSt = 1;
                        foreach (DataRow dtRow in ds.Tables[0].Rows)
                        {
                            dr = dt.NewRow();
                            dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                            dr["DT_DATE_OF_SERVICE"] = txtBillDate.Text;// dtServiceDate.ToString();//txtGroupDateofService.Text;
                            dr["SZ_PROCEDURE_ID"] = dtRow.ItemArray.GetValue(0);
                            dr["SZ_PROCEDURAL_CODE"] = dtRow.ItemArray.GetValue(2);
                            dr["SZ_CODE_DESCRIPTION"] = dtRow.ItemArray.GetValue(3);
                            dr["FLT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                            dr["I_UNIT"] = "1";
                            dr["SZ_TYPE_CODE_ID"] = dtRow.ItemArray.GetValue(1);
                            if (rowSt == ds.Tables[0].Rows.Count)
                            {
                                if (j.Cells[4].Text.ToString() != "") { dr["FLT_GROUP_AMOUNT"] = j.Cells[4].Text.ToString(); }
                            }
                            if (j.Cells[3].Text.ToString() != "") { dr["I_GROUP_AMOUNT_ID"] = j.Cells[3].Text.ToString(); }
                            dt.Rows.Add(dr);
                            rowSt = rowSt + 1;
                        }
                    }
                }
            }

            DataView dv = new DataView();

            dv = dt.DefaultView;

            dv.Sort = "DT_DATE_OF_SERVICE";
            grdTransactionDetails.DataSource = dt;
            grdTransactionDetails.DataBind();


            double total = 0;
            if (grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO objIslimit = new BillTransactionDAO();
                string sz_procedure_group_id1 = grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                string sz_visit_tyep1 = grdTransactionDetails.Items[0].Cells[13].Text.ToString();
                string sz_PG_Id1 = objIslimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id1);
                string szIsLimit = objIslimit.GET_IS_LIMITE(txtCompanyID.Text, sz_PG_Id1);

                if (szIsLimit != "" && szIsLimit != "NULL")
                {
                    for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                    {
                        //grdTransactionDetails.Items[i].Cells[9].Text = "";
                        if (grdTransactionDetails.Items[i].Cells[5].Text != "" && grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;")
                        {
                            total = total + Convert.ToDouble(grdTransactionDetails.Items[i].Cells[5].Text);
                        }
                        if (i == grdTransactionDetails.Items.Count - 1)
                        {


                            //  if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                            {
                                BillTransactionDAO objLimit = new BillTransactionDAO();
                                string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                //grdTransactionDetails.Items[i].Cells[9].Visible = true;
                                //grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                if (szReturn != "")
                                {
                                    if (Convert.ToDouble(szReturn) < total)
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                    }
                                    else
                                    {
                                        grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                    }
                                }
                                total = 0;
                            }

                        }
                        else
                        {
                            if (grdTransactionDetails.Items[i].Cells[1].Text != grdTransactionDetails.Items[i + 1].Cells[1].Text)
                            {

                                //   if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                                {
                                    BillTransactionDAO objLimit = new BillTransactionDAO();
                                    string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                    string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                    string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                    string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                    //grdTransactionDetails.Items[i].Cells[9].Visible=true;
                                    //grdTransactionDetails.Items[i].Cells[9].Text=szReturn;
                                    if (szReturn != "")
                                    {
                                        if (Convert.ToDouble(szReturn) < total)
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                        }
                                        else
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                        }
                                    }
                                    total = 0;

                                }

                            }
                        }
                    }
                }
            }


            #region "Disply Unit Column if speciality have unit bit set to true."
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[6].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[7].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
            }
            #endregion
            // modalpopupaddgroup.Show();
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

    #region "Bind Visit Grid"

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        ArrayList objAL = new ArrayList();
        try
        {
            objAL.Add(txtCompanyID.Text);
            objAL.Add(hndDoctorID.Value.ToString());
            objAL.Add("");
            objAL.Add("");
            objAL.Add("1");
            objAL.Add(txtCaseID.Text);
            objAL.Add("");
            grdAllReports.DataSource = _bill_Sys_Visit_BO.VisitReport(objAL);
            grdAllReports.DataBind();
            grdAllReports.Visible = true;
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
    public void BindDoctorsGrid(string szBillNumber)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrlst = new ArrayList();
            DataSet dset = new DataSet();
            Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            dset = _bill_Sys_Visit_BO.GetBillDoctorList(txtCompanyID.Text, szBillNumber, "GetBillDoctor");
            grdCompleteVisit.DataSource = dset.Tables[0];
            grdCompleteVisit.DataBind();

            for (int i = 0; i < grdCompleteVisit.Items.Count; i++)
            {
                string speciality_id = grdCompleteVisit.Items[i].Cells[6].Text;
                string visite_type = grdCompleteVisit.Items[i].Cells[3].Text; //
            }
            checkLimit();
            int cnt = 0;
            #region "Add color to Grid"
            Random rand = new Random();

            for (int m = 0; m < grdCompleteVisit.Items.Count; m++)
            {
                string doctorname = grdCompleteVisit.Items[m].Cells[2].Text;//
                arrlst.Add(doctorname);
                for (int n = 0; n < grdCompleteVisit.Items.Count; n++)
                {
                    if (doctorname == grdCompleteVisit.Items[n].Cells[2].Text)//
                    {
                        cnt++;
                    }
                }
                m = cnt - 1;
            }

            for (int j = 0; j < arrlst.Count; j++)
            {

                byte i = (byte)rand.Next(0, 255);
                byte x = (byte)rand.Next(0, 255);
                byte y = (byte)rand.Next(0, 255);
                // int i = 255;
                // int x = 255;
                //int y = 255;
                string doctorname = arrlst[j].ToString();
                foreach (DataGridItem item in grdCompleteVisit.Items)
                {
                    if (doctorname == item.Cells[2].Text)//
                    {
                        if (i > 150 && x > 150 && y > 150)
                        {
                            i = (byte)(i - 100);
                            x = (byte)(x - 100);
                        }

                        //item.Cells[1].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[2].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[3].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[4].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[5].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[6].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[7].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[8].BackColor = System.Drawing.Color.FromArgb(i, x, y);

                        item.Cells[1].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[2].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[3].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[4].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[5].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[6].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[7].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[8].ForeColor = System.Drawing.Color.FromArgb(y, x, i);

                    }
                }

            }

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
    public void BindVisitCompleteGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrlst = new ArrayList();
            DataSet dset = new DataSet();
            Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            dset = _bill_Sys_Visit_BO.GetCompletedVisitList(txtCaseID.Text, txtCompanyID.Text, "VISITCOMPLETEDETAILS");
            grdCompleteVisit.DataSource = dset.Tables[0];
            grdCompleteVisit.DataBind();

            int cnt = 0;
            //Random rand;

            #region "Add color to Grid"
            Random rand = new Random();

            for (int m = 0; m < grdCompleteVisit.Items.Count; m++)
            {
                string doctorname = grdCompleteVisit.Items[m].Cells[2].Text;//
                arrlst.Add(doctorname);
                for (int n = 0; n < grdCompleteVisit.Items.Count; n++)
                {
                    if (doctorname == grdCompleteVisit.Items[n].Cells[2].Text)//
                    {
                        cnt++;
                    }
                }
                m = cnt - 1;
            }

            for (int j = 0; j < arrlst.Count; j++)
            {

                byte i = (byte)rand.Next(0, 255);
                byte x = (byte)rand.Next(0, 255);
                byte y = (byte)rand.Next(0, 255);
                // int i = 255;
                // int x = 255;
                //int y = 255;
                string doctorname = arrlst[j].ToString();
                foreach (DataGridItem item in grdCompleteVisit.Items)
                {
                    if (doctorname == item.Cells[2].Text)
                    {
                        if (i > 150 && x > 150 && y > 150)
                        {
                            i = (byte)(i - 100);
                            x = (byte)(x - 100);
                        }

                        //item.Cells[1].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[2].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[3].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[4].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[5].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[6].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[7].BackColor = System.Drawing.Color.FromArgb(i, x, y);
                        //item.Cells[8].BackColor = System.Drawing.Color.FromArgb(i, x, y);

                        item.Cells[1].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[2].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[3].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[4].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[5].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[6].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[7].ForeColor = System.Drawing.Color.FromArgb(y, x, i);
                        item.Cells[8].ForeColor = System.Drawing.Color.FromArgb(y, x, i);

                    }
                }

            }

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
    protected void grdCompleteVisit_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            LoadProcedure(e.CommandArgument.ToString(), e);
        }
        if (e.CommandName == "Count")
        {
            dummybtnAddServices.Visible = true;
            hndPopUpvalue.Value = "PopUpValue";

            string sz_docId = e.Item.Cells[8].Text;
            string SZ_Is_added_by_doctor = e.Item.Cells[12].Text;
            string SZ_finalised = e.Item.Cells[13].Text;
            string SZ_SpecialityID = e.Item.Cells[6].Text;
            string SZ_Event_Id = e.Item.Cells[7].Text;
            string SZ_Is_Rff = e.Item.Cells[9].Text;
            CheckBox chk = (CheckBox)e.Item.FindControl("chkSelectItem");
            chk.Checked = true;
            RefferalModelPopUp(sz_docId, SZ_Is_added_by_doctor, SZ_finalised, SZ_SpecialityID, SZ_Event_Id, SZ_Is_Rff);

        }
    }
    protected void btnLoadProcedures_Click(object sender, EventArgs e)
    {
        //LoadProcedure();
    }
    protected void btnclearDiaProc_Click(object sender, EventArgs e)
    {
        lstDiagnosisCodes.Items.Clear();
        grdTransactionDetails.DataSource = null;
        grdTransactionDetails.DataBind();
    }
    public void LoadProcedure(string I_Event_Id, DataGridCommandEventArgs index)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {


            DataTable datatable = new DataTable();
            datatable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            datatable.Columns.Add("DT_DATE_OF_SERVICE");
            datatable.Columns.Add("SZ_PROCEDURE_ID");
            datatable.Columns.Add("SZ_PROCEDURAL_CODE");
            datatable.Columns.Add("SZ_CODE_DESCRIPTION");
            datatable.Columns.Add("FLT_AMOUNT");
            datatable.Columns.Add("I_UNIT");
            datatable.Columns.Add("SZ_TYPE_CODE_ID");
            datatable.Columns.Add("FLT_GROUP_AMOUNT");
            datatable.Columns.Add("I_GROUP_AMOUNT_ID");
            datatable.Columns.Add("I_EventID");
            DataRow dr;

            string Doctor_Id = "";
            ArrayList objarr;
            ArrayList arrLst = new ArrayList();
            DataSet dset = new DataSet();
            Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            dset = _bill_Sys_Visit_BO.GetVisitTypeList(txtCompanyID.Text, "GetVisitType");
            if (index.Item.Cells[9].Text != "1")
            {


                CheckBox chk = (CheckBox)(index.Item.FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {

                    string visit_type = index.Item.Cells[2].Text;
                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        if (visit_type == dset.Tables[0].Rows[i][1].ToString())
                        {
                            objarr = new ArrayList();
                            objarr.Add(txtCompanyID.Text);
                            objarr.Add(index.Item.Cells[6].Text);
                            objarr.Add(visit_type);
                            objarr.Add(index.Item.Cells[7].Text);
                            DataTable dt = new DataTable();
                            dt = _bill_Sys_Visit_BO.GetProcedureCodeList(objarr);
                            //dt = _bill_Sys_Visit_BO.GetProcedureCodeList(txtCompanyID.Text, "GetProcedureCodes", rw.Cells[6].Text, visit_type);
                            arrLst.Add(dt);
                        }
                    }
                    Doctor_Id = index.Item.Cells[8].Text;
                    if (hndDoctorID.Value != Doctor_Id)
                    {
                        grdTransactionDetails.DataSource = null;
                        grdTransactionDetails.DataBind();
                        lstDiagnosisCodes.Items.Clear();
                        Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();
                        lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, Doctor_Id, txtCompanyID.Text).Tables[0];
                        lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                        lstDiagnosisCodes.DataValueField = "CODE";
                        lstDiagnosisCodes.DataBind();    //Bind select Doctor
                        lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
                    }
                    else
                    {

                    }
                    hndDoctorID.Value = index.Item.Cells[8].Text;
                }
            }
            else
            {
                foreach (DataGridItem j in grdCompleteVisit.Items) //check
                {
                    CheckBox chk = (CheckBox)(j.FindControl("chkSelectItem"));
                    if (chk.Checked == true)
                    {
                        string visit_type = j.Cells[3].Text; //
                        for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                        {
                            if (visit_type == dset.Tables[0].Rows[i][1].ToString())
                            {
                                objarr = new ArrayList();
                                objarr.Add(txtCompanyID.Text);
                                objarr.Add(j.Cells[6].Text);
                                objarr.Add(visit_type);
                                objarr.Add(j.Cells[7].Text);
                                DataTable dt = new DataTable();
                                dt = _bill_Sys_Visit_BO.GetProcedureCodeList(objarr);
                                //dt = _bill_Sys_Visit_BO.GetProcedureCodeList(txtCompanyID.Text, "GetProcedureCodes", rw.Cells[6].Text, visit_type);
                                arrLst.Add(dt);
                            }
                        }
                        Doctor_Id = index.Item.Cells[8].Text;
                        if (hndDoctorID.Value != Doctor_Id)
                        {
                            grdTransactionDetails.DataSource = null;
                            grdTransactionDetails.DataBind();
                            lstDiagnosisCodes.Items.Clear();
                            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();
                            lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, Doctor_Id, txtCompanyID.Text).Tables[0];
                            lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                            lstDiagnosisCodes.DataValueField = "CODE";
                            lstDiagnosisCodes.DataBind();    //Bind select Doctor
                            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
                        }
                        else
                        {


                        }
                        hndDoctorID.Value = index.Item.Cells[8].Text;
                    }
                }
            }

            if (index.Item.Cells[9].Text != "1")
            {
                btnAddServices.Visible = true;
                btnAddGroup.Visible = true;
                foreach (DataGridItem j in grdTransactionDetails.Items)
                {
                    if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                    {
                        dr = datatable.NewRow();
                        if (j.Cells[0].Text.ToString() != "&nbsp;" && j.Cells[0].Text.ToString() != "") { dr["SZ_BILL_TXN_DETAIL_ID"] = j.Cells[0].Text.ToString(); } else { dr["SZ_BILL_TXN_DETAIL_ID"] = ""; }
                        dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(j.Cells[1].Text.ToString()).ToShortDateString();// Session["Date_Of_Service"].ToString();
                        dr["SZ_PROCEDURE_ID"] = j.Cells[2].Text.ToString();
                        dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                        dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                        dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                        if (((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString(); } }
                        dr["SZ_TYPE_CODE_ID"] = j.Cells[8].Text.ToString();
                        dr["FLT_GROUP_AMOUNT"] = j.Cells[9].Text.ToString();
                        dr["I_GROUP_AMOUNT_ID"] = j.Cells[10].Text.ToString();
                        dr["I_EventID"] = j.Cells[12].Text.ToString();
                        datatable.Rows.Add(dr);
                    }
                }
            }

            if (index.Item.Cells[9].Text == "1")
            {
                btnAddServices.Visible = false;
                btnAddGroup.Visible = false;
                GetProcedureCode(Doctor_Id);
                hndDoctorID.Value = Doctor_Id;
                for (int i = 0; i < arrLst.Count; i++)
                {
                    DataTable dt1 = (DataTable)arrLst[i];
                    foreach (DataRow drow in dt1.Rows)
                    {
                        dr = datatable.NewRow();
                        dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                        dr["DT_DATE_OF_SERVICE"] = drow.ItemArray.GetValue(5).ToString();
                        dr["SZ_PROCEDURE_ID"] = drow.ItemArray.GetValue(0).ToString();
                        dr["SZ_PROCEDURAL_CODE"] = drow.ItemArray.GetValue(2).ToString();
                        dr["SZ_CODE_DESCRIPTION"] = drow.ItemArray.GetValue(3).ToString();
                        dr["FLT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                        dr["I_UNIT"] = "1";
                        dr["SZ_TYPE_CODE_ID"] = drow.ItemArray.GetValue(1).ToString();
                        dr["I_EventID"] = drow.ItemArray.GetValue(6).ToString();
                        datatable.Rows.Add(dr);
                    }
                }

                grdTransactionDetails.DataSource = datatable;
                grdTransactionDetails.DataBind();

                Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

                if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
                {
                    grdTransactionDetails.Columns[6].Visible = true;
                    for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                    {
                        TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                        txtTemp.Text = grdTransactionDetails.Items[i].Cells[7].Text;
                    }
                }
                else
                {
                    grdTransactionDetails.Columns[7].Visible = false;
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

    protected void chkSelectItem_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable datatable = new DataTable();
            datatable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            datatable.Columns.Add("DT_DATE_OF_SERVICE");
            datatable.Columns.Add("SZ_PROCEDURE_ID");
            datatable.Columns.Add("SZ_PROCEDURAL_CODE");
            datatable.Columns.Add("SZ_CODE_DESCRIPTION");
            datatable.Columns.Add("FLT_AMOUNT");
            datatable.Columns.Add("I_UNIT");
            datatable.Columns.Add("SZ_TYPE_CODE_ID");
            datatable.Columns.Add("FLT_GROUP_AMOUNT");
            datatable.Columns.Add("I_GROUP_AMOUNT_ID");
            DataRow dr;

            string Doctor_Id = "";
            ArrayList objarr;
            ArrayList arrLst = new ArrayList();
            DataSet dset = new DataSet();
            Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            dset = _bill_Sys_Visit_BO.GetVisitTypeList(txtCompanyID.Text, "GetVisitType");
            foreach (DataGridItem rw in grdCompleteVisit.Items) //check
            {
                CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {
                    string visit_type = rw.Cells[3].Text; //
                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        if (visit_type == dset.Tables[0].Rows[i][1].ToString())
                        {
                            objarr = new ArrayList();
                            objarr.Add(txtCompanyID.Text);
                            objarr.Add(rw.Cells[6].Text);
                            objarr.Add(visit_type);
                            objarr.Add(rw.Cells[7].Text);
                            DataTable dt = new DataTable();
                            dt = _bill_Sys_Visit_BO.GetProcedureCodeList(objarr);
                            //dt = _bill_Sys_Visit_BO.GetProcedureCodeList(txtCompanyID.Text, "GetProcedureCodes", rw.Cells[6].Text, visit_type);
                            arrLst.Add(dt);
                        }
                    }
                    Doctor_Id = rw.Cells[8].Text;
                }
            }

            GetProcedureCode(Doctor_Id);
            hndDoctorID.Value = Doctor_Id;
            for (int i = 0; i < arrLst.Count; i++)
            {
                DataTable dt1 = (DataTable)arrLst[i];
                foreach (DataRow drow in dt1.Rows)
                {
                    dr = datatable.NewRow();
                    dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                    dr["DT_DATE_OF_SERVICE"] = drow.ItemArray.GetValue(5).ToString();
                    dr["SZ_PROCEDURE_ID"] = drow.ItemArray.GetValue(0).ToString();
                    dr["SZ_PROCEDURAL_CODE"] = drow.ItemArray.GetValue(2).ToString();
                    dr["SZ_CODE_DESCRIPTION"] = drow.ItemArray.GetValue(3).ToString();
                    dr["FLT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["I_UNIT"] = "1";
                    dr["SZ_TYPE_CODE_ID"] = drow.ItemArray.GetValue(1).ToString();
                    datatable.Rows.Add(dr);
                }
            }

            grdTransactionDetails.DataSource = datatable;
            grdTransactionDetails.DataBind();

            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[6].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[7].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
            }
            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();
            lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, hndDoctorID.Value.ToString(), txtCompanyID.Text).Tables[0];
            lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            lstDiagnosisCodes.DataValueField = "CODE";
            lstDiagnosisCodes.DataBind();    //Bind select Doctor
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
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

    public void Item_Bound(Object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            CheckBox chk = (CheckBox)(e.Item.FindControl("chkSelectItem"));
            string docName = e.Item.Cells[2].Text;
            string visitype = e.Item.Cells[3].Text;
            string docID = e.Item.Cells[8].Text;
            if (chk != null)
            {
                chk.Attributes.Add("onclick", "return ValidateGridCheckBox('" + docName + "','" + visitype + "','" + docID + "'," + e.Item.ItemIndex + ");");
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

    protected void btnQuickBill_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (hdnQuick.Value.Equals("true"))
        {
            // hdnQuick.Value = "false";
            try
            {
                DataTable datatable = new DataTable();

                datatable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
                datatable.Columns.Add("DT_DATE_OF_SERVICE");
                datatable.Columns.Add("SZ_PROCEDURE_ID");
                datatable.Columns.Add("SZ_PROCEDURAL_CODE");
                datatable.Columns.Add("SZ_CODE_DESCRIPTION");
                datatable.Columns.Add("FLT_AMOUNT");
                datatable.Columns.Add("I_UNIT");
                datatable.Columns.Add("SZ_TYPE_CODE_ID");
                datatable.Columns.Add("FLT_GROUP_AMOUNT");
                datatable.Columns.Add("I_GROUP_AMOUNT_ID");
                datatable.Columns.Add("I_EventID");
                datatable.Columns.Add("SZ_VISIT_TYPE");
                datatable.Columns.Add("BT_IS_LIMITE");
                DataRow dr;

                string Doctor_Id = "";
                ArrayList objarr;
                ArrayList arrLst = new ArrayList();
                DataSet dset = new DataSet();
                Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
                dset = _bill_Sys_Visit_BO.GetVisitTypeList(txtCompanyID.Text, "GetVisitType");
                int flag = 0;
                foreach (DataGridItem rw in grdCompleteVisit.Items) //check
                {
                    CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                    if (chk.Checked == true)
                    {
                        string visit_type = rw.Cells[3].Text; //
                        for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                        {
                            if (visit_type == dset.Tables[0].Rows[i][1].ToString())
                            {
                                objarr = new ArrayList();
                                objarr.Add(txtCompanyID.Text);
                                objarr.Add(rw.Cells[6].Text);
                                objarr.Add(visit_type);
                                objarr.Add(rw.Cells[7].Text);
                                DataTable dt = new DataTable();
                                dt = _bill_Sys_Visit_BO.GetProcedureCodeList(objarr);
                                if (dt.Rows.Count > 0)
                                {
                                    arrLst.Add(dt);
                                }
                                else
                                {
                                    flag = 1;
                                }
                            }
                        }
                        Doctor_Id = rw.Cells[8].Text;
                    }
                }
                for (int i = 0; i < arrLst.Count; i++)
                {
                    DataTable dt1 = (DataTable)arrLst[i];
                    foreach (DataRow drow in dt1.Rows)
                    {
                        dr = datatable.NewRow();
                        dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                        dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(drow.ItemArray.GetValue(5)).ToString();
                        dr["SZ_PROCEDURE_ID"] = drow.ItemArray.GetValue(0).ToString();
                        dr["SZ_PROCEDURAL_CODE"] = drow.ItemArray.GetValue(2).ToString();
                        dr["SZ_CODE_DESCRIPTION"] = drow.ItemArray.GetValue(3).ToString();
                        dr["FLT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                        dr["I_UNIT"] = "1";
                        dr["SZ_TYPE_CODE_ID"] = drow.ItemArray.GetValue(1).ToString();
                        dr["I_EventID"] = drow.ItemArray.GetValue(6).ToString();
                        dr["BT_IS_LIMITE"] = "";
                        dr["SZ_VISIT_TYPE"] = drow.ItemArray.GetValue(7).ToString();
                        datatable.Rows.Add(dr);
                    }
                }
                DataView dv = new DataView();

                dv = datatable.DefaultView;

                dv.Sort = "DT_DATE_OF_SERVICE";
                double total = 0;
                grdTransactionDetails.DataSource = datatable;
                grdTransactionDetails.DataBind();

                if (grdTransactionDetails.Items.Count > 0)
                {
                    BillTransactionDAO objIslimit = new BillTransactionDAO();
                    string sz_procedure_group_id1 = grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                    string sz_visit_tyep1 = grdTransactionDetails.Items[0].Cells[13].Text.ToString();
                    string sz_PG_Id1 = objIslimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id1);
                    string szIsLimit = objIslimit.GET_IS_LIMITE(txtCompanyID.Text, sz_PG_Id1);

                    if (szIsLimit != "" && szIsLimit != "NULL")
                    {
                        for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                        {
                            if (grdTransactionDetails.Items[i].Cells[5].Text != "" && grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;")
                            {
                                total = total + Convert.ToDouble(grdTransactionDetails.Items[i].Cells[5].Text);
                            }
                            if (i == grdTransactionDetails.Items.Count - 1)
                            {


                                // if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                                {
                                    BillTransactionDAO objLimit = new BillTransactionDAO();
                                    string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                    string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                    string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                    string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                    //grdTransactionDetails.Items[i].Cells[9].Visible = true;
                                    //grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                    if (szReturn != "")
                                    {
                                        if (Convert.ToDouble(szReturn) < total)
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                        }
                                        else
                                        {
                                            grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                        }
                                    }
                                    total = 0;
                                }

                            }
                            else
                            {
                                if (grdTransactionDetails.Items[i].Cells[1].Text != grdTransactionDetails.Items[i + 1].Cells[1].Text)
                                {

                                    //if (grdTransactionDetails.Items[i].Cells[14].Text == "1")
                                    {
                                        BillTransactionDAO objLimit = new BillTransactionDAO();
                                        string sz_procedure_group_id = grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                        string sz_visit_tyep = grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                                        string sz_PG_Id = objLimit.GetProcID(txtCompanyID.Text, sz_procedure_group_id);
                                        string szReturn = objLimit.GetLimit(txtCompanyID.Text, sz_visit_tyep, sz_PG_Id);
                                        //grdTransactionDetails.Items[i].Cells[9].Visible=true;
                                        //grdTransactionDetails.Items[i].Cells[9].Text=szReturn;
                                        if (szReturn != "")
                                        {
                                            if (Convert.ToDouble(szReturn) < total)
                                            {
                                                grdTransactionDetails.Items[i].Cells[9].Text = szReturn;
                                            }
                                            else
                                            {
                                                grdTransactionDetails.Items[i].Cells[9].Text = total.ToString();
                                            }
                                        }
                                        total = 0;

                                    }

                                }
                            }
                        }
                    }
                }
                DataSet dset1 = new DataSet();
                Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();
                dset1 = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, Doctor_Id, txtCompanyID.Text);
                if (dset1.Tables[0].Rows.Count == 0)
                {
                    //flag = 2;
                }
                else
                {
                    lstDiagnosisCodes.DataSource = dset1.Tables[0];
                    lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                    lstDiagnosisCodes.DataValueField = "CODE";
                    lstDiagnosisCodes.DataBind();
                }
                if (flag == 1)
                {
                    // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('No Procedure Code for visit. Cannot generate bill');</script>");
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "ss", "alert('No Procedure Code for visit. Cannot generate bill');", true);
                    grdTransactionDetails.DataSource = null;
                    grdTransactionDetails.DataBind();
                    return;
                }
                else if (flag == 2)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "ss", "alert('No diagnosis codes associated with speciality. Cannot generate bill');", true);
                    // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", );
                    grdTransactionDetails.DataSource = null;
                    grdTransactionDetails.DataBind();
                    return;
                }
                else
                {
                    saveQuickBills(Doctor_Id);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + pdfpath.ToString() + "'); ", true);
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #region "Save Bill Information"

    protected void saveQuickBills(string sz_docID)
    {
        // ArrayList _arrayList;
        try
        {
            string billno = "";
            #region "Save information into BillTransactionEO"

            BillTransactionEO _objBillEO = new BillTransactionEO();


            _objBillEO.SZ_CASE_ID = txtCaseID.Text;
            _objBillEO.SZ_COMPANY_ID = txtCompanyID.Text;
            _objBillEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);
            // change. amod 02-feb-2010. removed extended ddl and added simple ddl instead                
            // _objBillEO.SZ_DOCTOR_ID = extddlDoctor.Text;

            // changed by shailesh 31Mar2010, accepted doctor id from hidden field.
            //_objBillEO.SZ_DOCTOR_ID = extddlDoctor.SelectedValue;
            _objBillEO.SZ_DOCTOR_ID = sz_docID;
            _objBillEO.SZ_TYPE = ddlType.Text;
            _objBillEO.SZ_TESTTYPE = ""; //ddlTestType.Text;
            _objBillEO.FLAG = "ADD";
            _objBillEO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            #endregion

            _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();

            ArrayList objALEventEO = new ArrayList();
            ArrayList objALEventRefferProcedureEO = new ArrayList();
            if (grdCompleteVisit.Visible == true)
            {
                Bill_Sys_Calender _bill_Sys_Visit_BO = new Bill_Sys_Calender();
                ArrayList objList;
                foreach (DataGridItem item in grdCompleteVisit.Items)
                {
                    CheckBox chk1 = (CheckBox)item.FindControl("chkSelectItem");
                    if (chk1.Checked == true)
                    {
                        string eventID = item.Cells[7].Text;
                        EventEO _objEventEO = new EventEO();
                        _objEventEO.I_EVENT_ID = eventID;
                        _objEventEO.BT_STATUS = "1";
                        _objEventEO.I_STATUS = "2";
                        _objEventEO.SZ_BILL_NUMBER = "";
                        _objEventEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);
                        objALEventEO.Add(_objEventEO);
                        foreach (DataGridItem j in grdTransactionDetails.Items)
                        {
                            if (j.Cells[1].Text != "")
                            {
                                if (DateTime.Compare(Convert.ToDateTime(item.Cells[1].Text), Convert.ToDateTime(j.Cells[1].Text)) == 0)
                                {
                                    EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                                    objEventRefferProcedureEO.SZ_PROC_CODE = j.Cells[8].Text;
                                    objEventRefferProcedureEO.I_EVENT_ID = eventID;
                                    objEventRefferProcedureEO.I_STATUS = "2";
                                    objALEventRefferProcedureEO.Add(objEventRefferProcedureEO);
                                }
                            }
                        }

                    }
                }

            }
            // End : Update Visit Status.
            objCaseDetailsBO = new CaseDetailsBO();
            string szCaseType = objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            // btnAddService.Enabled = true;
            ArrayList _objALBillProcedureCodeEO = new ArrayList();
            foreach (DataGridItem j in grdTransactionDetails.Items)
            {
                if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                {
                    BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();
                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

                    objBillProcedureCodeEO.SZ_PROCEDURE_ID = j.Cells[2].Text.ToString();

                    if (j.Cells[5].Text.ToString() != "&nbsp;")
                    {
                        objBillProcedureCodeEO.FL_AMOUNT = j.Cells[5].Text.ToString();
                    }
                    else
                    {
                        objBillProcedureCodeEO.FL_AMOUNT = "0";
                    }

                    objBillProcedureCodeEO.SZ_BILL_NUMBER = "";

                    objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(j.Cells[1].Text.ToString());

                    objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                    objBillProcedureCodeEO.I_UNIT = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString();

                    objBillProcedureCodeEO.FLT_PRICE = j.Cells[5].Text.ToString();//((Label)j.Cells[5].FindControl("lblPrice")).Text;
                    objBillProcedureCodeEO.DOCT_AMOUNT = j.Cells[5].Text.ToString();//((Label)j.Cells[5].FindControl("lblPrice")).Text;

                    //objBillProcedureCodeEO.FLT_FACTOR = j.Cells[5].Text.ToString();//((Label)j.Cells[5].FindControl("lblFactor")).Text;

                    //if (j.Cells[8].Text.ToString() != "&nbsp;" && j.Cells[8].Text.ToString() != "")
                    //{
                    //    objBillProcedureCodeEO.PROC_AMOUNT = j.Cells[8].Text.ToString();
                    //}
                    //else
                    //{
                    objBillProcedureCodeEO.PROC_AMOUNT = j.Cells[5].Text.ToString();
                    //}
                    objBillProcedureCodeEO.SZ_DOCTOR_ID = sz_docID;

                    objBillProcedureCodeEO.SZ_CASE_ID = txtCaseID.Text;

                    objBillProcedureCodeEO.SZ_TYPE_CODE_ID = j.Cells[8].Text.ToString();

                    //if (szCaseType == "WC000000000000000002")
                    //{
                    //    objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[12].Text.ToString();
                    //    objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[13].Text.ToString();
                    //}
                    //else
                    //{
                    //objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                    //objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                    //}
                    //if (j.Cells[9].Text.ToString() != "&nbsp;")
                    //{
                    //    objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[9].Text.ToString();
                    //    objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[10].Text.ToString();
                    //}
                    //else
                    //{
                    //    objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                    //    objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                    //}

                    if (j.Cells[9].Text.ToString() != "&nbsp;")
                    {
                        objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[9].Text.ToString();

                    }
                    else
                    {
                        objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";

                    } if (j.Cells[10].Text.ToString() != "&nbsp;" && j.Cells[10].Text.ToString() != "&nbsp;" && j.Cells[10].Text.ToString() != "&nbsp;")
                    {
                        objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[10].Text.ToString();
                    }
                    else
                    {
                        objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                    }


                    _objALBillProcedureCodeEO.Add(objBillProcedureCodeEO);
                }
            }
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ArrayList objALBillDiagnosisCodeEO = new ArrayList();
            foreach (ListItem lstItem in lstDiagnosisCodes.Items)
            {
                BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID = lstItem.Value.ToString();
                objALBillDiagnosisCodeEO.Add(objBillDiagnosisCodeEO);
            }


            // Call Save with Transaction.
            BillTransactionDAO objBT_DAO = new BillTransactionDAO();
            Result objResult = new Result();
            objResult = objBT_DAO.SaveBillTransactions(_objBillEO, objALEventEO, objALEventRefferProcedureEO, _objALBillProcedureCodeEO, objALBillDiagnosisCodeEO);
            if (objResult.msg_code == "ERR")
            {
                usrMessage.PutMessage(objResult.msg);
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
            else
            {
                txtBillID.Text = objResult.bill_no;
                billno = txtBillID.Text;
                // Start : Save Notes for Bill.
                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;
                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                // End 
                String patientID = objCaseDetailsBO.GetPatientID(billno);
                if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000001")
                {
                    //string patientID = Workers Compensation
                    objDefaultValue = new Bill_Sys_InsertDefaultValues();
                    if (grdLatestBillTransaction.Items.Count == 0)
                    {
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_DoctorOpinion.xml"), txtCompanyID.Text.ToString(), null, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_ExamInformation.xml"), txtCompanyID.Text.ToString(), null, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_History.xml"), txtCompanyID.Text.ToString(), null, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_PlanOfCare.xml"), txtCompanyID.Text.ToString(), null, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_WorkStatus.xml"), txtCompanyID.Text.ToString(), null, patientID);
                    }
                    else if (grdLatestBillTransaction.Items.Count >= 1)
                    {

                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_DoctorsOpinionC4_2.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_ExaminationTreatment.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_PermanentImpairment.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                        objDefaultValue.InsertDefaultValues(Server.MapPath("..\\Config\\DV_WorkStatusC4_2.xml"), txtCompanyID.Text.ToString(), billno, patientID);
                    }
                }

                BindLatestTransaction();
                BindVisitCompleteGrid();
                ClearControl();
                usrMessage.PutMessage(" Bill Saved successfully ! ");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000002")
                {
                    //Bill_Generation _Bill_Generation = new Bill_Generation();
                    //_Bill_Generation.sz_bill_no = billno;
                    //_Bill_Generation.sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    //_Bill_Generation.sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                    //_Bill_Generation.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    //_Bill_Generation.sz_company_name = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;

                    //Bill_Sys_Verification_Desc objVerification_Desc = new Bill_Sys_Verification_Desc();

                    //objVerification_Desc.sz_bill_no = billno;
                    //objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    //objVerification_Desc.sz_flag = "BILL";

                    //ArrayList arrNf_Param = new ArrayList();
                    //ArrayList arrNf_NodeType = new ArrayList();
                    //string sz_Type = "";
                    //String szDestinationDir = "";

                    //arrNf_Param.Add(objVerification_Desc);

                    //arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Param);

                    //_Bill_Generation.sz_node_type = arrNf_NodeType;
                    //_Bill_Generation.sz_speciality = _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    //_Bill_Generation.sz_userid = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    //_Bill_Generation.sz_username = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;

                    //Nofault_Bill_Generation _Nofault_Bill_Generation = new Nofault_Bill_Generation();
                    //string strpath = _Nofault_Bill_Generation.NofaultBillGeneration(_Bill_Generation);
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + strpath + "'); ", true);

                    GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000003")
                {
                    GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                else if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000004")
                {
                    mbs.LienBills.Lien obj = new Lien();
                    string path;
                    //Tushar
                    string bt_CaseType;
                    string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    string sz_speciality = _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    bt_include = _MUVGenerateFunction.get_bt_include(strComp, sz_speciality, "", "Speciality");
                    bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000004", "CaseType");
                    if (bt_include == "True" && bt_CaseType == "True")
                    {
                        String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                        String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + sz_speciality + "/";
                        string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                        str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                        String FileName;
                        FileName = obj.GenratePdfForLienWithMuv(strComp, billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szUserName, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO, szUserId);
                        MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDestinationDir + FileName, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                        path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf");

                        ArrayList objAL = new ArrayList();
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(str_1500.Replace(".pdf", "_MER.pdf"));
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(sz_speciality);
                        objAL.Add("NF");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objNF3Template.saveGeneratedBillPath(objAL);

                        // Start : Save Notes for Bill.

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                    }
                    else
                    {
                        path = obj.GenratePdfForLien(txtCompanyID.Text, billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                    }
                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + path + "');", true);
                    pdfpath = path;
                }
                //else if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000006")
                //{
                //    objNF3Template = new Bill_Sys_NF3_Template();
                //    _MUVGenerateFunction = new MUVGenerateFunction();
                //    sz_speciality = _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //    string path;
                //    //Tushar
                //    string bt_CaseType, bt_include;
                //    string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //    Session["TM_SZ_BILL_ID"] = billno;
                //    // Changes for Doc manager for New Bill path --pravin
                //    objVerification_Desc = new Bill_Sys_Verification_Desc();
                //    objVerification_Desc.sz_bill_no = billno;
                //    objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //    objVerification_Desc.sz_flag = "BILL";
                //    arrNf_Param = new ArrayList();
                //    arrNf_NodeType = new ArrayList();
                //    sz_Type = "";
                //    szDestinationDir = "";
                //    arrNf_Param.Add(objVerification_Desc);
                //    arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Param);
                //    if (arrNf_NodeType.Contains("NFVER"))
                //    {
                //        sz_Type = "OLD";
                //        szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + sz_speciality + "/";
                //    }
                //    else
                //    {
                //        sz_Type = "NEW";
                //        szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + sz_speciality + "/" + "Bills/";
                //    }
                //    String szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                //    str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                //    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                //    {
                //        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                //        {
                //            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                //        }
                //        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                //    }
                //    path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500;

                //    ArrayList objAL = new ArrayList();
                //    if (sz_Type == "OLD")
                //    {
                //        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                //        objAL.Add(szDestinationDir + str_1500);
                //        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //        objAL.Add(Session["TM_SZ_CASE_ID"]);
                //        objAL.Add(str_1500);
                //        objAL.Add(szDestinationDir);
                //        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                //        objAL.Add(sz_speciality);
                //        objAL.Add("LN");
                //        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                //        objNF3Template.saveGeneratedBillPath(objAL);
                //    }
                //    else
                //    {
                //        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                //        objAL.Add(szDestinationDir + str_1500);
                //        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //        objAL.Add(Session["TM_SZ_CASE_ID"]);
                //        objAL.Add(str_1500);
                //        objAL.Add(szDestinationDir);
                //        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                //        objAL.Add(sz_speciality);
                //        objAL.Add("LN");
                //        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                //        objAL.Add(arrNf_NodeType[0].ToString());
                //        objNF3Template.saveGeneratedBillPath_New(objAL);
                //    }
                //    // Start : Save Notes for Bill.
                //    _DAO_NOTES_EO = new DAO_NOTES_EO();
                //    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;
                //    _DAO_NOTES_BO = new DAO_NOTES_BO();
                //    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                //    _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                //    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //}
            }
        }
        catch { }
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
            _MUVGenerateFunction = new MUVGenerateFunction();

            String szSpecility = p_szSpeciality;
            Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            Session["TM_SZ_BILL_ID"] = p_szBillNumber;

            objNF3Template = new Bill_Sys_NF3_Template();

            // changes for Doc Manager for Bill path-- pravin
            Bill_Sys_Verification_Desc objVerification_Desc = new Bill_Sys_Verification_Desc();

            objVerification_Desc.sz_bill_no = p_szBillNumber;
            objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            objVerification_Desc.sz_flag = "BILL";

            ArrayList arrNf_Param = new ArrayList();
            ArrayList arrNf_NodeType = new ArrayList();
            string sz_Type = "";
            string szReturnPath = "";
            String szDestinationDir = "";

            arrNf_Param.Add(objVerification_Desc);

            arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Param);
            if (arrNf_NodeType.Contains("NFVER"))
            {
                sz_Type = "OLD";
                szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
            }
            else
            {
                sz_Type = "NEW";
                szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + szSpecility + "/" + "Bills/";
            }

            CaseDetailsBO objCaseDetails = new CaseDetailsBO();
            String szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                String szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                //String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";

                //changes for Add only 1500 Form For Insurance Company -- pravin
                objCaseDetailsBO = new CaseDetailsBO();
                DataSet ds1500form = new DataSet();
                string bt_1500_Form = "";
                ds1500form = objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID, Session["TM_SZ_BILL_ID"].ToString());

                if (ds1500form.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1500form.Tables[0].Rows.Count; i++)
                    {
                        bt_1500_Form = ds1500form.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (bt_1500_Form == "1")
                {

                    ArrayList objAL = new ArrayList();
                    str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());

                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }
                    szReturnPath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500;

                    if (sz_Type == "OLD")
                    {
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + str_1500);
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(str_1500);
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(szSpecility);
                        objAL.Add("NF");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objNF3Template.saveGeneratedBillPath(objAL);

                        objNF3Template.saveGeneratedBillPath(objAL);
                    }
                    else
                    {
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + str_1500);
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(str_1500);
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(szSpecility);
                        objAL.Add("NF");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objAL.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL);
                    }



                    // Start : Save Notes for Bill.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                    BindLatestTransaction();

                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szReturnPath.ToString() + "'); ", true);
                }
                else
                {
                    string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

                    string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                    String szFile3 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                    String szFile4 = ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                    String szPDFPage1;
                    String szXMLFileName;
                    String szOriginalPDFFileName;
                    String szLastXMLFileName;
                    String szLastOriginalPDFFileName;
                    String sz3and4Page;
                    Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
                    String szDiagPDFFilePosition = objConfiguration.getConfigurationSettings(szCompanyID, "GET_DIAG_PAGE_POSITION");
                    String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(szCompanyID, "DIAG_PAGE");

                    szXMLFileName = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                    szOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                    szLastXMLFileName = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                    szLastOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();


                    Boolean fAddDiag = true;



                    GeneratePDFFile.GenerateNF3PDF objGeneratePDF = new GeneratePDFFile.GenerateNF3PDF();
                    objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

                    // Note : Generate PDF with Billing Information table. **** II
                    String szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
                    log.Debug("Bill Details PDF File : " + szPDFFileName);

                    sz3and4Page = szFile3;//""objPDFReplacement.Merge3and4Page(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szFile3, szFile4);

                    szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());

                    log.Debug("Page1 : " + szPDFPage1);


                    // Merge **** I AND **** II
                    String szPDF_1_3;
                    // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
                    szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);

                    String szLastPDFFileName;
                    String szPDFPage3;
                    szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());

                    //Tushar
                    string bt_CaseType;
                    string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    bt_include = _MUVGenerateFunction.get_bt_include(strComp, szSpecility, "", "Speciality");
                    bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000002", "CaseType");
                    if (bt_include == "True" && bt_CaseType == "True")
                    {
                        str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                    }



                    MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                    szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);

                    //Tushar
                    if (bt_include == "True" && bt_CaseType == "True")
                    {
                        MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szLastPDFFileName, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500.Replace(".pdf", "_MER.pdf"));
                        szLastPDFFileName = str_1500.Replace(".pdf", "_MER.pdf");
                    }
                    //

                    //szLastPDFFileName = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDefaultPath + szLastPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                    String szGenereatedFileName = "";
                    szGenereatedFileName = szDefaultPath + szLastPDFFileName;
                    log.Debug("GenereatedFileName : " + szGenereatedFileName);


                    String szOpenFilePath = "";
                    szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szGenereatedFileName;


                    string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;


                    CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
                    string newPdfFilename = "";
                    string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                    objMyForm.initialize(KeyForCutePDF);

                    if (objMyForm == null)
                    {
                        // Response.Write("objMyForm not initialized");
                    }
                    else
                    {
                        if (System.IO.File.Exists(szFileNameWithFullPath))
                        {
                            //    objMyForm.mergePDF(szFileNameWithFullPath, szFileNameWithFullPath.Replace(".pdf", "_Temp.pdf").ToString(), szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString());

                            if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAT_THE_END)
                            {
                                if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) == 5)
                                {
                                }
                                else
                                {
                                    //MergePDF.MergePDFFiles(szFileNameWithFullPath, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString());
                                    szPDFFileName = szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf");
                                }
                            }

                        }

                    }

                    // End Logic

                    string szFileNameForSaving = "";

                    // Save Entry in Table
                    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
                    }

                    // End

                    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
                    {
                        szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
                        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                    }
                    else
                    {
                        if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                        {
                            szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                        }
                        else
                        {
                            szFileNameForSaving = szOpenFilePath.ToString();
                            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                        }
                    }
                    pdfpath = szFileNameForSaving;
                    String[] szTemp;
                    string szBillName = "";
                    szTemp = szFileNameForSaving.Split('/');
                    ArrayList objAL = new ArrayList();
                    szFileNameForSaving = szFileNameForSaving.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                    szBillName = szTemp[szTemp.Length - 1].ToString();

                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
                    }

                    if (sz_Type == "OLD")
                    {
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + szBillName);
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(szTemp[szTemp.Length - 1].ToString());
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(szSpecility);
                        objAL.Add("NF");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objNF3Template.saveGeneratedBillPath(objAL);
                    }
                    else
                    {
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + szBillName);
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(szTemp[szTemp.Length - 1].ToString());
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(szSpecility);
                        objAL.Add("NF");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objAL.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL);
                    }

                    // Start : Save Notes for Bill.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                    BindLatestTransaction();


                    // End 

                }
            }
            else if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
            {
                Bill_Sys_PVT_Template _objPvtBill;
                _objPvtBill = new Bill_Sys_PVT_Template();
                bool isReferingFacility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string szCompanyId;
                string szCaseId = Session["TM_SZ_CASE_ID"].ToString();
                // string szSpecility ;
                string szCompanyName;
                string szBillId = Session["TM_SZ_BILL_ID"].ToString();
                string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szCompanyName = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    szCompanyId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    szCompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    szCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                //_objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyId, szCaseId, szSpecility, szCompanyName, szBillId, szUserName, szUserId);
                //string path = _objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyId, szCaseId, szSpecility, szCompanyName, szBillId, szUserName, szUserId);
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + _objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyId, szCaseId, szSpecility, szCompanyName, szBillId, szUserName, szUserId) + "'); ", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
            }

            Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();


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

    protected void showModalPopup()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //DataTable datatable = new DataTable();
            //datatable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            //datatable.Columns.Add("DT_DATE_OF_SERVICE");
            //datatable.Columns.Add("SZ_PROCEDURE_ID");
            //datatable.Columns.Add("SZ_PROCEDURAL_CODE");
            //datatable.Columns.Add("SZ_CODE_DESCRIPTION");
            //datatable.Columns.Add("FLT_AMOUNT");
            //datatable.Columns.Add("I_UNIT");
            //datatable.Columns.Add("SZ_TYPE_CODE_ID");
            //datatable.Columns.Add("FLT_GROUP_AMOUNT");
            //datatable.Columns.Add("I_GROUP_AMOUNT_ID");
            //DataRow dr;
            //foreach (DataGridItem j in grdTransactionDetails.Items)
            //{
            //    if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
            //    {
            //        dr = datatable.NewRow();
            //        if (j.Cells[0].Text.ToString() != "&nbsp;" && j.Cells[0].Text.ToString() != "") { dr["SZ_BILL_TXN_DETAIL_ID"] = j.Cells[0].Text.ToString(); } else { dr["SZ_BILL_TXN_DETAIL_ID"] = ""; }
            //        dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(j.Cells[1].Text.ToString()).ToShortDateString();// Session["Date_Of_Service"].ToString();
            //        dr["SZ_PROCEDURE_ID"] = j.Cells[2].Text.ToString();
            //        dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
            //        dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();

            //        dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
            //        if (((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString(); } }
            //        dr["SZ_TYPE_CODE_ID"] = j.Cells[9].Text.ToString();
            //        dr["FLT_GROUP_AMOUNT"] = j.Cells[10].Text.ToString();
            //        dr["I_GROUP_AMOUNT_ID"] = j.Cells[11].Text.ToString();
            //        datatable.Rows.Add(dr);
            //    }
            //}
            string Doctor_Id = "";
            string finalised = "";
            string Is_added_by_doctor = "";
            string SpecialityID = "";
            ArrayList objarr;
            ArrayList arrLst = new ArrayList();
            DataSet dset = new DataSet();
            Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            dset = _bill_Sys_Visit_BO.GetVisitTypeList(txtCompanyID.Text, "GetVisitType");
            int flag = 0;
            foreach (DataGridItem rw in grdCompleteVisit.Items)
            {
                CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {
                    Doctor_Id = rw.Cells[8].Text;
                    //Is_added_by_doctor = rw.Cells[12].Text;
                    //finalised = rw.Cells[13].Text;
                    SpecialityID = rw.Cells[6].Text;
                }
            }
            //if (finalised != null && Is_added_by_doctor != null)
            //{
            //    hndFinalised.Value = finalised;
            //    hndIsAddedByDoctor.Value = Is_added_by_doctor;
            //    //hndSpecialityID.Value = SpecialityID;
            //    Session["Procedure_Code"] = SpecialityID;
            //}
            if (Doctor_Id != "")
            {
                GetProcedureCode(Doctor_Id);
                hndDoctorID.Value = Doctor_Id;
            }
            else
            {
                GetProcedureCode(hndDoctorID.Value.ToString());
            }
            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[6].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[7].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
            }

            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();

            //lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, hndDoctorID.Value.ToString(), txtCompanyID.Text).Tables[0];
            //lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            //lstDiagnosisCodes.DataValueField = "CODE";
            //lstDiagnosisCodes.DataBind();    //Bind select Doctor
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();

            if (hndPopUpvalue.Value.ToString() == "PopUpValue")
            {
                hndPopUpvalue.Value = "";
                modalpopupAddservice.Show();
            }
            else if (hndPopUpvalue.Value.ToString() == "GroupPopUpValue")
            {
                hndPopUpvalue.Value = "";
                modalpopupaddgroup.Show();
            }
            for (int iProcCount = 0; iProcCount < grdProcedure.Items.Count; iProcCount++)
            {
                CheckBox chk1 = (CheckBox)(grdProcedure.Items[iProcCount].FindControl("chkselect"));
                chk1.Checked = false;

            }
            foreach (DataGridItem rw in grdCompleteVisit.Items)
            {
                CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {
                    string Event_Id = rw.Cells[7].Text;
                    string Is_Rff = rw.Cells[9].Text;
                    Bill_Sys_BillTransaction_BO objRefferal = new Bill_Sys_BillTransaction_BO();
                    DataSet dsRefferal = new DataSet();
                    //if (Is_Rff == "1")
                    //{
                    dsRefferal = objRefferal.GET_PROC_CODE_USING_EVENT_ID(Event_Id);
                    if (dsRefferal.Tables.Count > 0)
                    {
                        if (dsRefferal.Tables[0].Rows.Count > 0)
                        {
                            for (int iReffCount = 0; iReffCount < dsRefferal.Tables[0].Rows.Count; iReffCount++)
                            {
                                for (int iProcCount = 0; iProcCount < grdProcedure.Items.Count; iProcCount++)
                                {
                                    CheckBox chk1 = (CheckBox)(grdProcedure.Items[iProcCount].FindControl("chkselect"));
                                    if (dsRefferal.Tables[0].Rows[iReffCount]["SZ_PROC_CODE"].ToString() == grdProcedure.Items[iProcCount].Cells[2].Text)
                                    {
                                        chk1.Checked = true;
                                    }

                                }
                            }
                        }
                    }

                    // }
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

    protected void showAddToPreferedListModalPopup()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //DataTable datatable = new DataTable();
            //datatable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            //datatable.Columns.Add("DT_DATE_OF_SERVICE");
            //datatable.Columns.Add("SZ_PROCEDURE_ID");
            //datatable.Columns.Add("SZ_PROCEDURAL_CODE");
            //datatable.Columns.Add("SZ_CODE_DESCRIPTION");
            //datatable.Columns.Add("FLT_AMOUNT");
            //datatable.Columns.Add("I_UNIT");
            //datatable.Columns.Add("SZ_TYPE_CODE_ID");
            //datatable.Columns.Add("FLT_GROUP_AMOUNT");
            //datatable.Columns.Add("I_GROUP_AMOUNT_ID");
            //DataRow dr;
            //foreach (DataGridItem j in grdTransactionDetails.Items)
            //{
            //    if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
            //    {
            //        dr = datatable.NewRow();
            //        if (j.Cells[0].Text.ToString() != "&nbsp;" && j.Cells[0].Text.ToString() != "") { dr["SZ_BILL_TXN_DETAIL_ID"] = j.Cells[0].Text.ToString(); } else { dr["SZ_BILL_TXN_DETAIL_ID"] = ""; }
            //        dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(j.Cells[1].Text.ToString()).ToShortDateString();// Session["Date_Of_Service"].ToString();
            //        dr["SZ_PROCEDURE_ID"] = j.Cells[2].Text.ToString();
            //        dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
            //        dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();

            //        dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
            //        if (((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString(); } }
            //        dr["SZ_TYPE_CODE_ID"] = j.Cells[9].Text.ToString();
            //        dr["FLT_GROUP_AMOUNT"] = j.Cells[10].Text.ToString();
            //        dr["I_GROUP_AMOUNT_ID"] = j.Cells[11].Text.ToString();
            //        datatable.Rows.Add(dr);
            //    }
            //}
            string Doctor_Id = "";
            string finalised = "";
            string Is_added_by_doctor = "";
            string SpecialityID = "";
            ArrayList objarr;
            ArrayList arrLst = new ArrayList();
            DataSet dset = new DataSet();
            Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            dset = _bill_Sys_Visit_BO.GetVisitTypeList(txtCompanyID.Text, "GetVisitType");
            int flag = 0;
            foreach (DataGridItem rw in grdCompleteVisit.Items)
            {
                CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {
                    Doctor_Id = rw.Cells[8].Text;
                    //Is_added_by_doctor = rw.Cells[12].Text;
                    //finalised = rw.Cells[13].Text;
                    SpecialityID = rw.Cells[6].Text;
                }
            }
            //if (finalised != null && Is_added_by_doctor != null)
            //{
            //    hndFinalised.Value = finalised;
            //    hndIsAddedByDoctor.Value = Is_added_by_doctor;
            //    //hndSpecialityID.Value = SpecialityID;
            //    Session["Procedure_Code"] = SpecialityID;
            //}
            if (Doctor_Id != "")
            {
                GetAddToPreferedListProcedureCode(Doctor_Id);
                hndDoctorID.Value = Doctor_Id;
            }
            else
            {
                GetAddToPreferedListProcedureCode(hndDoctorID.Value.ToString());
            }
            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[6].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[7].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
            }

            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();

            //lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, hndDoctorID.Value.ToString(), txtCompanyID.Text).Tables[0];
            //lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            //lstDiagnosisCodes.DataValueField = "CODE";
            //lstDiagnosisCodes.DataBind();    //Bind select Doctor
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();

            if (hndPopUpvalue.Value.ToString() == "PopUpValue")
            {
                hndPopUpvalue.Value = "";
                modalpopupAddservice.Show();
            }
            else if (hndPopUpvalue.Value.ToString() == "GroupPopUpValue")
            {
                hndPopUpvalue.Value = "";
                modalpopupaddgroup.Show();
            }
            for (int iProcCount = 0; iProcCount < grdProcedure.Items.Count; iProcCount++)
            {
                CheckBox chk1 = (CheckBox)(grdProcedure.Items[iProcCount].FindControl("chkselect"));
                chk1.Checked = false;

            }
            foreach (DataGridItem rw in grdCompleteVisit.Items)
            {
                CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {
                    string Event_Id = rw.Cells[7].Text;
                    string Is_Rff = rw.Cells[9].Text;
                    Bill_Sys_BillTransaction_BO objRefferal = new Bill_Sys_BillTransaction_BO();
                    DataSet dsRefferal = new DataSet();
                    //if (Is_Rff == "1")
                    //{
                    dsRefferal = objRefferal.GET_PROC_CODE_USING_EVENT_ID(Event_Id);
                    if (dsRefferal.Tables.Count > 0)
                    {
                        if (dsRefferal.Tables[0].Rows.Count > 0)
                        {
                            for (int iReffCount = 0; iReffCount < dsRefferal.Tables[0].Rows.Count; iReffCount++)
                            {
                                for (int iProcCount = 0; iProcCount < grdProcedure.Items.Count; iProcCount++)
                                {
                                    CheckBox chk1 = (CheckBox)(grdProcedure.Items[iProcCount].FindControl("chkselect"));
                                    if (dsRefferal.Tables[0].Rows[iReffCount]["SZ_PROC_CODE"].ToString() == grdProcedure.Items[iProcCount].Cells[2].Text)
                                    {
                                        chk1.Checked = true;
                                    }

                                }
                            }
                        }
                    }

                    // }
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

    public void gridmodelpopup()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable datatable = new DataTable();
            datatable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            datatable.Columns.Add("DT_DATE_OF_SERVICE");
            datatable.Columns.Add("SZ_PROCEDURE_ID");
            datatable.Columns.Add("SZ_PROCEDURAL_CODE");
            datatable.Columns.Add("SZ_CODE_DESCRIPTION");
            datatable.Columns.Add("FLT_AMOUNT");
            datatable.Columns.Add("I_UNIT");
            datatable.Columns.Add("SZ_TYPE_CODE_ID");
            datatable.Columns.Add("FLT_GROUP_AMOUNT");
            datatable.Columns.Add("I_GROUP_AMOUNT_ID");
            datatable.Columns.Add("I_EventID");
            DataRow dr;
            foreach (DataGridItem j in grdTransactionDetails.Items)
            {
                if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                {
                    dr = datatable.NewRow();
                    if (j.Cells[0].Text.ToString() != "&nbsp;" && j.Cells[0].Text.ToString() != "") { dr["SZ_BILL_TXN_DETAIL_ID"] = j.Cells[0].Text.ToString(); } else { dr["SZ_BILL_TXN_DETAIL_ID"] = ""; }
                    dr["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(j.Cells[1].Text.ToString()).ToShortDateString();// Session["Date_Of_Service"].ToString();
                    dr["SZ_PROCEDURE_ID"] = j.Cells[2].Text.ToString();
                    dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                    dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                    dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                    if (((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[6].FindControl("txtUnit")).Text.ToString(); } }
                    dr["SZ_TYPE_CODE_ID"] = j.Cells[8].Text.ToString();
                    dr["FLT_GROUP_AMOUNT"] = j.Cells[9].Text.ToString();
                    dr["I_GROUP_AMOUNT_ID"] = j.Cells[10].Text.ToString();
                    dr["I_EventID"] = j.Cells[12].Text.ToString();
                    datatable.Rows.Add(dr);
                }
            }
            string Doctor_Id = "";
            ArrayList objarr;
            ArrayList arrLst = new ArrayList();
            DataSet dset = new DataSet();
            Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            dset = _bill_Sys_Visit_BO.GetVisitTypeList(txtCompanyID.Text, "GetVisitType");
            int flag = 0;
            foreach (DataGridItem rw in grdCompleteVisit.Items) //check
            {
                CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {
                    string visit_type = rw.Cells[3].Text; //
                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        if (visit_type == dset.Tables[0].Rows[i][1].ToString())
                        {
                            objarr = new ArrayList();
                            objarr.Add(txtCompanyID.Text);
                            objarr.Add(rw.Cells[6].Text);
                            objarr.Add(visit_type);
                            objarr.Add(rw.Cells[7].Text);
                            DataTable dt = new DataTable();
                            dt = _bill_Sys_Visit_BO.GetProcedureCodeList(objarr);
                            if (dt.Rows.Count > 0)
                            {
                                string codes = dt.Rows[0][2].ToString();
                                string date = dt.Rows[0][5].ToString();
                                for (int j = 0; j < datatable.Rows.Count; j++)
                                {
                                    if (codes == datatable.Rows[j][3].ToString() && DateTime.Compare(Convert.ToDateTime(date), Convert.ToDateTime(datatable.Rows[j][1].ToString())) == 0)
                                    {
                                        flag = 1;
                                        break;
                                    }
                                    else
                                    {
                                        flag = 2;
                                    }
                                }
                            }
                            if (flag == 2 || flag == 0)
                            {
                                arrLst.Add(dt);
                            }
                        }
                    }
                    Doctor_Id = rw.Cells[8].Text;
                }
            }
            if (Doctor_Id != "")
            {
                GetProcedureCode(Doctor_Id);
                hndDoctorID.Value = Doctor_Id;
            }
            else
            {
                GetProcedureCode(hndDoctorID.Value.ToString());
            }
            for (int i = 0; i < arrLst.Count; i++)
            {
                DataTable dt1 = (DataTable)arrLst[i];
                foreach (DataRow drow in dt1.Rows)
                {
                    dr = datatable.NewRow();
                    dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                    dr["DT_DATE_OF_SERVICE"] = drow.ItemArray.GetValue(5).ToString();
                    dr["SZ_PROCEDURE_ID"] = drow.ItemArray.GetValue(0).ToString();
                    dr["SZ_PROCEDURAL_CODE"] = drow.ItemArray.GetValue(2).ToString();
                    dr["SZ_CODE_DESCRIPTION"] = drow.ItemArray.GetValue(3).ToString();
                    dr["FLT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["I_UNIT"] = "1";
                    dr["SZ_TYPE_CODE_ID"] = drow.ItemArray.GetValue(1).ToString();
                    datatable.Rows.Add(dr);
                }
            }
            grdTransactionDetails.DataSource = datatable;
            grdTransactionDetails.DataBind();

            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[6].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[7].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
            }


            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();
            lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, hndDoctorID.Value.ToString(), txtCompanyID.Text).Tables[0];
            lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            lstDiagnosisCodes.DataValueField = "CODE";
            lstDiagnosisCodes.DataBind();    //Bind select Doctor
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
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

    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            grdPatientDeskList.DataSource = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            grdPatientDeskList.DataBind();

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

    private void GetDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagnosisCode("", txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE").Tables[0];
            lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            lstDiagnosisCodes.DataValueField = "CODE";
            lstDiagnosisCodes.DataBind();
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

    private void GetProcedureCode(string doctorId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataTable dtProcCode = new DataTable();
            for (int i = 1; i <= 3; i++)
            {
                dtProcCode = _bill_Sys_BillTransaction.GetDoctorProcedureCodeList(doctorId, "TY00000000000000000" + i.ToString(), txtCaseID.Text).Tables[0];
                if (dtProcCode.Rows.Count > 0)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
                        dt.Columns.Add("DT_DATE_OF_SERVICE");
                        dt.Columns.Add("SZ_PROCEDURE_ID");
                        dt.Columns.Add("SZ_PROCEDURAL_CODE");
                        dt.Columns.Add("SZ_CODE_DESCRIPTION");
                        dt.Columns.Add("FLT_AMOUNT");
                        dt.Columns.Add("I_UNIT");
                        dt.Columns.Add("SZ_TYPE_CODE_ID");
                        dt.Columns.Add("FLT_GROUP_AMOUNT");
                        dt.Columns.Add("I_GROUP_AMOUNT_ID");
                        DataTable dataRowServices;
                        DataRow dr;
                        string szprocedureCode = "";
                        string szTypeCode = "";
                        bool stat = false;
                        foreach (DataRow drPrcList in dtProcCode.Rows)
                        {
                            szprocedureCode = drPrcList["CODE"].ToString().Substring(0, drPrcList["CODE"].ToString().IndexOf("|"));
                            szTypeCode = drPrcList["CODE"].ToString().Substring(drPrcList["CODE"].ToString().IndexOf("|") + 1, drPrcList["CODE"].ToString().Length - (drPrcList["CODE"].ToString().IndexOf("|") + 1));
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            dataRowServices = new DataTable();
                            dataRowServices = _bill_Sys_BillTransaction.GeSelectedDoctorProcedureCode_List(szprocedureCode, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, doctorId, szTypeCode).Tables[0];
                            foreach (DataRow j in dataRowServices.Rows)
                            {
                                dr = dt.NewRow();
                                dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                string dtDateOfService = drPrcList["DESCRIPTION"].ToString().Substring(drPrcList["DESCRIPTION"].ToString().Substring(0, drPrcList["DESCRIPTION"].ToString().IndexOf("--")).Length + 2, drPrcList["DESCRIPTION"].ToString().Length - (drPrcList["DESCRIPTION"].ToString().Substring(0, drPrcList["DESCRIPTION"].ToString().IndexOf("--")).Length + 2));
                                dr["DT_DATE_OF_SERVICE"] = dtDateOfService.Substring(0, dtDateOfService.IndexOf("--"));
                                dr["SZ_PROCEDURE_ID"] = j["SZ_PROCEDURE_ID"];
                                dr["SZ_PROCEDURAL_CODE"] = j["SZ_PROCEDURE_CODE"];
                                dr["SZ_CODE_DESCRIPTION"] = j["SZ_CODE_DESCRIPTION"];
                                dr["I_UNIT"] = "";
                                dr["SZ_TYPE_CODE_ID"] = szTypeCode;
                                dt.Rows.Add(dr);
                            }
                        }
                        grdTransactionDetails.DataSource = dt;
                        grdTransactionDetails.DataBind();
                    }
                    catch { }
                }
            }
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            grdProcedure.DataSource = _bill_Sys_BillTransaction.GetDoctorSpecialityProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdProcedure.DataBind();
            grdGroupProcCodeService.DataSource = _bill_Sys_BillTransaction.GetDoctorSpecialityGroupProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdGroupProcCodeService.DataBind();
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
    private void GetAddToPreferedListProcedureCode(string doctorId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataTable dtProcCode = new DataTable();
            for (int i = 1; i <= 3; i++)
            {
                dtProcCode = _bill_Sys_BillTransaction.GetDoctorProcedureCodeList(doctorId, "TY00000000000000000" + i.ToString(), txtCaseID.Text).Tables[0];
                if (dtProcCode.Rows.Count > 0)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
                        dt.Columns.Add("DT_DATE_OF_SERVICE");
                        dt.Columns.Add("SZ_PROCEDURE_ID");
                        dt.Columns.Add("SZ_PROCEDURAL_CODE");
                        dt.Columns.Add("SZ_CODE_DESCRIPTION");
                        dt.Columns.Add("FLT_AMOUNT");
                        dt.Columns.Add("I_UNIT");
                        dt.Columns.Add("SZ_TYPE_CODE_ID");
                        dt.Columns.Add("FLT_GROUP_AMOUNT");
                        dt.Columns.Add("I_GROUP_AMOUNT_ID");
                        DataTable dataRowServices;
                        DataRow dr;
                        string szprocedureCode = "";
                        string szTypeCode = "";
                        bool stat = false;
                        foreach (DataRow drPrcList in dtProcCode.Rows)
                        {
                            szprocedureCode = drPrcList["CODE"].ToString().Substring(0, drPrcList["CODE"].ToString().IndexOf("|"));
                            szTypeCode = drPrcList["CODE"].ToString().Substring(drPrcList["CODE"].ToString().IndexOf("|") + 1, drPrcList["CODE"].ToString().Length - (drPrcList["CODE"].ToString().IndexOf("|") + 1));
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            dataRowServices = new DataTable();
                            dataRowServices = _bill_Sys_BillTransaction.GeSelectedDoctorProcedureCode_List(szprocedureCode, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, doctorId, szTypeCode).Tables[0];
                            foreach (DataRow j in dataRowServices.Rows)
                            {
                                dr = dt.NewRow();
                                dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                string dtDateOfService = drPrcList["DESCRIPTION"].ToString().Substring(drPrcList["DESCRIPTION"].ToString().Substring(0, drPrcList["DESCRIPTION"].ToString().IndexOf("--")).Length + 2, drPrcList["DESCRIPTION"].ToString().Length - (drPrcList["DESCRIPTION"].ToString().Substring(0, drPrcList["DESCRIPTION"].ToString().IndexOf("--")).Length + 2));
                                dr["DT_DATE_OF_SERVICE"] = dtDateOfService.Substring(0, dtDateOfService.IndexOf("--"));
                                dr["SZ_PROCEDURE_ID"] = j["SZ_PROCEDURE_ID"];
                                dr["SZ_PROCEDURAL_CODE"] = j["SZ_PROCEDURE_CODE"];
                                dr["SZ_CODE_DESCRIPTION"] = j["SZ_CODE_DESCRIPTION"];
                                dr["I_UNIT"] = "";
                                dr["SZ_TYPE_CODE_ID"] = szTypeCode;
                                dt.Rows.Add(dr);
                            }
                        }
                        grdTransactionDetails.DataSource = dt;
                        grdTransactionDetails.DataBind();
                    }
                    catch { }
                }
            }
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            grdProcedure.DataSource = _bill_Sys_BillTransaction.GetDoctorSpecialityAddToPrecedureListProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdProcedure.DataBind();
            grdGroupProcCodeService.DataSource = _bill_Sys_BillTransaction.GetDoctorSpecialityGroupProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdGroupProcCodeService.DataBind();
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

    private void SetControlForUpdateBill()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblDateOfService.Style.Add("visibility", "hidden");//.Visible=false;
            txtDateOfservice.Style.Add("visibility", "hidden");//.Visible=false;
            Image1.Style.Add("visibility", "hidden");//.Visible=false;

            lblGroupServiceDate.Style.Add("visibility", "hidden");//.Visible=false;
            txtGroupDateofService.Style.Add("visibility", "hidden");//.Visible=false;
            imgbtnDateofService.Style.Add("visibility", "hidden");
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

    private DataSet Getspeciality(string p_szCompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_MST_PROCEDURE_GROUP", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_PROCEDURE_GROUP_LIST");
            sqlCmd.Parameters.AddWithValue("@ID", p_szCompanyID);
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
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAddServices_Click(object sender, EventArgs e)
    {
        // LoadProcedure();
        dummybtnAddServices.Visible = true;
        hndPopUpvalue.Value = "PopUpValue";
        showModalPopup();
    }
    protected void btnAddPreferedList_Click(object sender, EventArgs e)
    {
        // LoadProcedure();
        dummybtnAddServices.Visible = true;
        hndPopUpvalue.Value = "PopUpValue";
        showAddToPreferedListModalPopup();
    }
    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        dummybtnAddGroup.Visible = true;
        hndPopUpvalue.Value = "GroupPopUpValue";
        showModalPopup();
    }


    protected void btnNf2_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            _bill_Sys_PatientBO.UpdateTemplateStatus(txtCaseID.Text, chkNf2.Checked == true ? 1 : 0, "");
            if (chkNf2.Checked == true)
            {
                txtNf2.Text = "1";
            }
            else
            {
                txtNf2.Text = "0";
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

    public void checkLimit()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            for (int i = 0; i < grdCompleteVisit.Items.Count; i++)
            {
                string speciality_id = grdCompleteVisit.Items[i].Cells[6].Text;
                string visite_type = grdCompleteVisit.Items[i].Cells[3].Text; //
                BillTransactionDAO objLimit = new BillTransactionDAO();
                string szReturn = objLimit.GetLimit(txtCompanyID.Text, visite_type, speciality_id);
                if (szReturn == "")
                {
                    CheckBox chk = (CheckBox)grdCompleteVisit.Items[i].Cells[10].FindControl("chkLimit");
                    chk.Checked = false;
                    chk.Enabled = false;
                }
                else
                {
                    CheckBox chk = (CheckBox)grdCompleteVisit.Items[i].Cells[10].FindControl("chkLimit");
                    chk.Checked = true;
                    chk.Enabled = true;
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

    protected void RefferalModelPopUp(string SZ_Doctor_Id, string SZ_Is_added_by_doctor, string SZ_finalised, string SZ_SpecialityID, string SZ_Event_Id, string SZ_Is_Rff)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            string Doctor_Id = "";
            string finalised = "";
            string Is_added_by_doctor = "";
            string SpecialityID = "";
            ArrayList objarr;
            ArrayList arrLst = new ArrayList();
            DataSet dset = new DataSet();
            Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            dset = _bill_Sys_Visit_BO.GetVisitTypeList(txtCompanyID.Text, "GetVisitType");
            int flag = 0;

            Doctor_Id = SZ_Doctor_Id;
            //Is_added_by_doctor = SZ_Is_added_by_doctor;
            //finalised = SZ_finalised;
            SpecialityID = SZ_SpecialityID;

            //if (finalised != null && Is_added_by_doctor != null)
            //{
            //    hndFinalised.Value = finalised;
            //    hndIsAddedByDoctor.Value = Is_added_by_doctor;
            //    //hndSpecialityID.Value = SpecialityID;
            //    Session["Procedure_Code"] = SpecialityID;
            //}

            if (Doctor_Id != "")
            {
                GetProcedureCode(Doctor_Id);
                hndDoctorID.Value = Doctor_Id;
            }
            else
            {
                GetProcedureCode(hndDoctorID.Value.ToString());
            }
            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[6].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[7].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
            }

            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();

            //lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, hndDoctorID.Value.ToString(), txtCompanyID.Text).Tables[0];
            //lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            //lstDiagnosisCodes.DataValueField = "CODE";
            //lstDiagnosisCodes.DataBind();    //Bind select Doctor
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();

            if (hndPopUpvalue.Value.ToString() == "PopUpValue")
            {
                hndPopUpvalue.Value = "";
                modalpopupAddservice.Show();
            }
            else if (hndPopUpvalue.Value.ToString() == "GroupPopUpValue")
            {
                hndPopUpvalue.Value = "";
                modalpopupaddgroup.Show();
            }

            for (int iProcCount = 0; iProcCount < grdProcedure.Items.Count; iProcCount++)
            {
                CheckBox chk1 = (CheckBox)(grdProcedure.Items[iProcCount].FindControl("chkselect"));
                chk1.Checked = false;

            }

            string Event_Id = SZ_Event_Id;
            string Is_Rff = SZ_Is_Rff;
            Bill_Sys_BillTransaction_BO objRefferal = new Bill_Sys_BillTransaction_BO();
            DataSet dsRefferal = new DataSet();
            //if (Is_Rff == "1")
            //{
            dsRefferal = objRefferal.GET_PROC_CODE_USING_EVENT_ID(Event_Id);
            if (dsRefferal.Tables.Count > 0)
            {
                if (dsRefferal.Tables[0].Rows.Count > 0)
                {
                    for (int iReffCount = 0; iReffCount < dsRefferal.Tables[0].Rows.Count; iReffCount++)
                    {
                        for (int iProcCount = 0; iProcCount < grdProcedure.Items.Count; iProcCount++)
                        {
                            CheckBox chk1 = (CheckBox)(grdProcedure.Items[iProcCount].FindControl("chkselect"));
                            if (dsRefferal.Tables[0].Rows[iReffCount]["SZ_PROC_CODE"].ToString() == grdProcedure.Items[iProcCount].Cells[2].Text)
                            {
                                chk1.Checked = true;
                            }

                        }
                    }
                }
            }

            //}

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

