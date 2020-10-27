/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_BillTransaction.aspx.cs
/*Purpose              :       To Add and Edit Bill Transaction
/*Author               :       Sandeep Y
/*Date of creation     :       19 Dec 2008  
/*Modified By          :
/*Modified Date        :
/************************************************************/

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
using System.Data.SqlClient;
using PDFValueReplacement;
using System.IO;
using log4net;



public partial class Bill_Sys_BillTransaction : PageBase
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
    #endregion

    #region "Page Event"

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            if (Request.QueryString["CaseID"] != null && Request.QueryString["pname"]!=null &&  Request.QueryString["cmpid"].ToString()!=null)
            {
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


            }
            //for billsearch ajax page
            if (Request.QueryString["CaseID"] != null)
            {
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                string  szCaseId = Request.QueryString["CaseID"];
              //  byte[] ar = System.Convert.FromBase64String(szCaseId);
               // szCaseId = System.Text.ASCIIEncoding.ASCII.GetString(ar);
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(szCaseId, "");
                _bill_Sys_CaseObject.SZ_CASE_ID = szCaseId;
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID,_bill_Sys_CaseObject.SZ_COMAPNY_ID);

                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = szCaseId;

                Session["CASEINFO"] = _bill_Sys_Case;
            }
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                Response.Redirect("Bill_Sys_ReferralBillTransaction.aspx?Type=Search", false);
            }
            //btnSave.Attributes.Add("onclick", "return javascript:validate();");
            //btnSaveTransaction.Attributes.Add("onclick", "return javascript:validate();");
            //btnUpdate.Attributes.Add("onclick", "return javascript:validate();");
            //btnLoadProcedures.Attributes.Add("onclick", "return ValidateGridCheckBox();");
            //chkSelectItem.Attributes.Add("onclick", "return ValidateGridCheckBox();");


            if (!IsPostBack)
            {
                CaseDetailsBO _objCaseDetailBO = new CaseDetailsBO();
                string sz_CaseStatus = _objCaseDetailBO.GetCaseStatusArchived(((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
                if (sz_CaseStatus == "2")
                {

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('You can not create bill for this patient..');window.document.location.href='Bill_Sys_CaseDetails.aspx';</script>");
                    //Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
                    return;
                }
                if (Request.QueryString["Type"] == null)
                {
                    // added by shailesh to load the complete visit grid on new visit
                    if (Session["CASE_OBJECT"] != null)
                    {
                        txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        BindVisitCompleteGrid();
                        dvcompletevisit.Visible = true;
                        //btnLoadProcedures.Visible = true;
                        btnQuickBill.Visible = true;

                        btnQuickBill.Attributes.Add("onclick", "return QuickBillConfirmClaimInsurance();");
                    }
                    Session["SZ_BILL_NUMBER"] = null;
                }
                    //...ashutosh
                else 
                {
                    if (Request.QueryString["BillNo"]!=null && Request.QueryString["caseid"]!=null && Request.QueryString["caseno"]!=null)
                    {
                        
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        string szCaseId = Request.QueryString["CaseID"];
                        //byte[] ar = System.Convert.FromBase64String(szCaseId);
                       // szCaseId = System.Text.ASCIIEncoding.ASCII.GetString(ar);
                        _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(szCaseId, "");
                        _bill_Sys_CaseObject.SZ_CASE_ID = szCaseId;
                        _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                        _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                        _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, _bill_Sys_CaseObject.SZ_COMAPNY_ID);

                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                        Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                        _bill_Sys_Case.SZ_CASE_ID = szCaseId;

                        Session["CASEINFO"] = _bill_Sys_Case;
                    }
                }   
                    //...
            }
            ErrorDiv.InnerText = "";
            ErrorDiv.Style.Value = "color: red";
            if (Session["CASE_OBJECT"] != null)
            {
                txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
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
                Response.Redirect("Bill_Sys_SearchCase.aspx", false);
            }

            lblLocationNote.Text = "";



            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                // if the account is configured to have a location
                if (!IsPostBack)
                {


                    if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
                    {
                        extddlDoctor.DataSource = GetBillingDoctor(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        extddlDoctor.DataTextField = "DESCRIPTION";
                        extddlDoctor.DataValueField = "CODE";
                        extddlDoctor.DataBind();
                        ListItem objItem = new ListItem("---select---", "NA");
                        extddlDoctor.Items.Insert(0, objItem);


                    }
                    else
                    {
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        string szLocationID = _caseDetailsBO.GetPatientLocationID(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        if (szLocationID != null)
                        {
                            if (szLocationID.Equals(""))
                            {
                                lblLocationNote.Text = "Note: There is no office location set for the patient / doctor";
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
                    // commented Amod - 02-feb-2010 to remove extended control
                    // and add simple drop downlist instead
                    // extddlDoctor.Procedure_Name = "SP_MST_BILLING_DOCTOR";
                    // extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    // ends
                }
            }
            else
            {
                // pass the company id to the procedure sp_mst_doctor

                // commented Amod - 02-feb-2010 to remove extended control
                // ad add simple drop downlist instead

                // extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                // if the account is configured to have a location
                if (!IsPostBack)
                {
                    if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
                    {
                        Bill_Sys_DoctorBO objDoctor = new Bill_Sys_DoctorBO();
                        extddlDoctor.DataSource = objDoctor.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        extddlDoctor.DataTextField = "DESCRIPTION";
                        extddlDoctor.DataValueField = "CODE";
                        extddlDoctor.DataBind();
                        ListItem objItem = new ListItem("---select---", "NA");
                        extddlDoctor.Items.Insert(0, objItem);
                    }
                    else
                    {
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        string szLocationID = _caseDetailsBO.GetPatientLocationID(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                        if (szLocationID != null)
                        {
                            if (szLocationID.Equals(""))
                            {
                                lblLocationNote.Text = "Note: There is no office location set for the patient / doctor";
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
                }
            }
            string querystring;
            if (Request.QueryString["Type"] == null)
            {
                querystring = "";
            }
            else
            {
                querystring = Request.QueryString["Type"].ToString();
            }
            btnSave.Attributes.Add("onclick", "return ConfirmClaimInsurance();");
            // btnUpdate.Attributes.Add("onclick", "return formValidator('frmBillTrans','txtBillDate,extddlDoctor');");
            btnUpdate.Attributes.Add("onclick", "return FormValidation();");
            btnAddServices.Attributes.Add("onclick", "return completeGridValidator('" + querystring + "');");
            btnAddGroup.Attributes.Add("onclick", "return CompleteVisitGroupVisit('" + querystring + "');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlDiagnosisType.Flag_ID = txtCompanyID.Text;
            string doctor_ID = "";
            if (!IsPostBack)
            {
                btnUpdate.Enabled = false;
                if (Session["SZ_BILL_NUMBER"] != null)
                {
                    txtBillID.Text = Session["SZ_BILL_NUMBER"].ToString();
                    Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
                    doctor_ID = _bill_Sys_Visit_BO.GetDoctorID(txtBillID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    hndDoctorID.Value = doctor_ID;
                    BindTransactionData(txtBillID.Text);
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                }
                txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                Session["SELECTED_DIA_PRO_CODE"] = null;
                if (Request.QueryString["PopUp"] == null)
                {

                }
                else
                {
                    if (Session["TEMP_DOCTOR_ID"] != null)
                    {

                        // changed amod 02-feb-2010
                        // to remove extended drop down list and made it to
                        // simple drop down list
                        //extddlDoctor.Text = Session["TEMP_DOCTOR_ID"].ToString(); 
                        extddlDoctor.SelectedValue = Session["TEMP_DOCTOR_ID"].ToString();
                        //GetProcedureCode(extddlDoctor.SelectedValue); 
                        //GetProcedureCode(hndDoctorID.Value.ToString());
                    }
                    GetProcedureCode(hndDoctorID.Value.ToString());
                }

                BindLatestTransaction();
                if (Session["SZ_BILL_NUMBER"] != null)
                {
                    txtBillID.Text = Session["SZ_BILL_NUMBER"].ToString();
                    _editOperation = new EditOperation();
                    _editOperation.Primary_Value = Session["SZ_BILL_NUMBER"].ToString();
                    _editOperation.WebPage = this.Page;
                    _editOperation.Xml_File = "BillTransaction.xml";
                    _editOperation.LoadData();
                    txtBillDate.Text = String.Format("{0:MM/dd/yyyy}", txtBillDate.Text).ToString();
                    setDefaultValues(Session["SZ_BILL_NUMBER"].ToString());
                }
            }
            else
            {

                if (Session["SELECTED_DIA_PRO_CODE"] != null)
                {
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
                if (objCaseDetailsBO.GetCaseType(objItem.Cells[1].Text) != "WC000000000000000001")
                {
                    objItem.Cells[13].Text = "";
                    objItem.Cells[14].Text = "";
                    objItem.Cells[15].Text = "";
                }
            }

            txtBillDate.Text = DateTime.Now.Date.ToShortDateString();


            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ArrayList _objArrayList = _bill_Sys_BillTransaction.GetClaimInsurance(txtCaseID.Text);

            if (_objArrayList.Count > 0)
            {
                if (_objArrayList[0].ToString() != "" && _objArrayList[0].ToString() != "NA" && _objArrayList[2].ToString() != "" && _objArrayList[2].ToString() != "NA")
                {
                    if (_objArrayList[1].ToString() != "" && _objArrayList[1].ToString() != "")
                    {
                        txtClaimInsurance.Text = "3";
                    }
                    else
                    {

                        txtClaimInsurance.Text = "2";
                    }

                }
                else
                {
                    txtClaimInsurance.Text = "1";
                }
            }
            else
            {
                txtClaimInsurance.Text = "0";
            }

            #region "Remove Procedure code logic"

            if (!Page.IsPostBack)
            {
                Session["DELETED_PROC_CODES"] = null;
            }
            #endregion


            SetControlForUpdateBill();
            objCaseDetailsBO = new CaseDetailsBO();
            string szCaseType = objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            if (szCaseType == "WC000000000000000001")
            {
                grdTransactionDetails.Columns[12].Visible = false;
            }
            if (hndPopUpvalue.Value != null && hndPopUpvalue.Value != "" && hndPopUpvalue.Value.ToString() == "PopUpValue")
            {
                if (Request.QueryString["Type"] == null)
                {
                    showModalPopup();
                }
            }
            else if (hndPopUpvalue.Value != null && hndPopUpvalue.Value != "" && hndPopUpvalue.Value.ToString() == "GroupPopUpValue")
            {
                if (Request.QueryString["Type"] == null)
                {
                    showModalPopup();
                }
            }
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_BillTransaction. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Bill_Sys_BillTransaction. Method - Page_Load : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_BillTransaction. Method - Page_Load : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_BillTransaction. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
            }
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

    // added amod - feb 02-feb-2010
    private DataSet GetBillingDoctor(string p_szCompanyID)
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
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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

            #region "Maintain Logs"

            log.Debug("BillTransaction. Method - setDefaultValues : " + ex.Message.ToString());
            log.Debug("BillTransaction. Method - setDefaultValues : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("BillTransaction. Method - setDefaultValues  : " + ex.InnerException.Message.ToString());
                log.Debug("BillTransaction. Method - setDefaultValues  : " + ex.InnerException.StackTrace.ToString());
            }
            #endregion

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
        // changed amod 02-feb-2010 . changed from ext drop down to simple drop down

        //if (extddlDoctor.Text == "NA")

        //removed by shailesh 
        //if (extddlDoctor.SelectedValue == "NA")
        //{
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
                //BindGrid();
                //  ErrorDiv.InnerText = " Bill Updated successfully ! ";
                //   ErrorDiv.Style.Value = "color: blue";

                lblMsg.Visible = true;
                lblMsg.Text = " Bill Updated successfully ! ";

            }
            BindLatestTransaction();
            //BindGrid();

        }
        catch (SqlException objSqlExcp)
        {
            ErrorDiv.InnerText = " Bill Number already exists";
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
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_2.aspx'); ", true);
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

    protected void lnkShowGrid_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
            // txtBillNo.Text = "";
            txtBillDate.Text = "";

            // change amod. 02-feb-2010. Removed extended dropdown and added
            // simple drop down instead
            // extddlDoctor.Text = "NA";
            extddlDoctor.SelectedValue = "NA";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;

            grdTransactionDetails.Visible = false;
            lblMsg.Visible = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
                // changed. Amod 02-feb-2010. Remove extended ddl and added simple
                // drop down instead
                // extddlDoctor.Text = dataset.Tables[0].Rows[i][1].ToString();
                extddlDoctor.SelectedValue = dataset.Tables[0].Rows[i][1].ToString();

                // added shailesh 31Mar2010. to accept the doctorid
                hndDoctorID.Value = dataset.Tables[0].Rows[i][1].ToString();
            }
            grdTransactionDetails.Visible = true;
            extddlDoctor.Enabled = false;
            Session["AssociateDiagnosis"] = true;
        }
        catch(Exception ex)
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
        catch(Exception ex)
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

    protected void lnlInitialReport_Click(object sender, EventArgs e)
    {
        Session["TEMPLATE_BILL_NO"] = Session["BillID"].ToString();
        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformation.aspx'); ", true);
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
            // changed. Amod 02-feb-2010. Remove extended ddl and added simple
            // drop down instead
            // if (extddlDoctor.Text != "NA")

            // removed by shailesh, the field is not required
            //if (extddlDoctor.SelectedValue != "NA")
            //{
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
                //string patientID = Workers Compensation
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
            // btnAddService.Enabled = true;

            ArrayList _arrayList;
            foreach (DataGridItem j in grdTransactionDetails.Items)
            {
                if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                {
                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    _arrayList = new ArrayList();
                    // _arrayList.Add(j.Cells[2].Text.ToString());
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

                    // changed amod. 02-feb-2010 . Removed extended drop down list
                    // added simple ddl instead
                    //_arrayList.Add(extddlDoctor.Text);

                    // changed by shailesh 31Mar2010, accepted doctor id from hidden field.
                    //_arrayList.Add(extddlDoctor.SelectedValue);
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
            lblMsg.Visible = true;
            //btnSave.Enabled = false;
            lblMsg.Text = " Bill Saved successfully ! ";
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
            //}
            //else
            //{
            //    lblMsg.Visible = true;
            //    lblMsg.Text = " Select doctor ... ! ";
            //}

        }
        catch(Exception ex)
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
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
                // change amod. 02-feb-2010 removed extended dll and added simple
                // dll instead
                // extddlDoctor.Text = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[12].Text; 
                extddlDoctor.SelectedValue = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[12].Text;

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
            grdCompleteVisit.DataSource = null;
            grdCompleteVisit.DataBind();
            dvcompletevisit.Visible = false;
            btnLoadProcedures.Visible = false;
            btnQuickBill.Visible = false;
            gridmodelpopup();
            hndPopUpvalue.Value = "GridPopUp";
            
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

    protected void grdLatestBillTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

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


                Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                Session["TM_SZ_BILL_ID"] = e.Item.Cells[1].Text;

                objNF3Template = new Bill_Sys_NF3_Template();

                CaseDetailsBO objCaseDetails = new CaseDetailsBO();
                String szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
                {
                    String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    String szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
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

                    MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                    szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);
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
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + _objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyId, szCaseId, szSpecility, szCompanyName, szBillId, szUserName, szUserId)+ "'); ", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
                }


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
                    }
                }
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformation.aspx','WC4','menubar=no,scrollbars=yes'); ", true);
                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
            }
            if (e.CommandName.ToString() == "Doctor's Progress Report")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_2.aspx'); ", true);
                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
            }
            if (e.CommandName.ToString() == "Doctor's Report Of MMI")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_3.aspx'); ", true);
                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
            }
            if (e.CommandName.ToString() == "Make Payment")
            {
                //if (e.Item.Cells[5].Text.ToString() != "" && e.Item.Cells[5].Text.ToString() != "&nbsp;")
                //{
                //    if (e.Item.Cells[5].Text.ToString() != "0.00")
                //    {
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
            lblMsg.Visible = false;



        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : " + ex.Message.ToString());
            log.Debug("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : " + ex.InnerException.StackTrace.ToString());
            }
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

    #endregion

    #region "Service Event Handler"

    //protected void btnAddService_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Session["TM_SZ_BILL_ID"] = extddlDoctor.Text;
    //        Session["Date_Of_Service"] = txtDateOfservice.Text;
    //        if (txtDateOfServiceTo.Visible == true) { Session["TODate_Of_Service"] = txtDateOfServiceTo.Text; } else { Session["TODate_Of_Service"] = null; }
    //        Session["DOCTOR_ID"] = extddlDoctor.Text;
    //        Session["SZ_CASE_ID"] = txtCaseID.Text;
    //        Session["SELECTED_SERVICES"] = null;
    //        Createtable();
    //        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectDiagnosis.aspx'); ", true);

    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    //protected void btnUpdateService_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        UpdateTransactionDetails();
    //        // BindTransactionDetailsGrid(txtBillNo.Text);
    //        BindLatestTransaction();
    //        // BindIC9Grid(txtBillNo.Text.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
        dt.Columns.Add("FACTOR_AMOUNT");
        dt.Columns.Add("FLT_AMOUNT");
        dt.Columns.Add("FACTOR");
        dt.Columns.Add("PROC_AMOUNT");
        dt.Columns.Add("DOCT_AMOUNT");
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
            if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0) { dr["FACTOR_AMOUNT"] = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString(); }
            if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0) { dr["FACTOR"] = ((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString(); }
            dr["FLT_AMOUNT"] = j.Cells[6].Text.ToString();
            if (((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString(); } }
            dr["PROC_AMOUNT"] = j.Cells[9].Text.ToString();
            dr["DOCT_AMOUNT"] = j.Cells[10].Text.ToString();
            dt.Rows.Add(dr);

        }

        Session["SELECTED_SERVICES"] = dt;// arrDiagnosisObject;

    }

    private void BindTransactionData(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            //DataSet ds = new DataSet();
            //ds = _bill_Sys_BillTransaction.BindTransactionData(id);
            //int i = 0;
            //foreach (DataColumn c in ds.Tables[0].Columns)
            //{
            //    if (i == 8 || i == 9)
            //    {
            //        BoundColumn bc = new BoundColumn();
            //        bc.DataField = c.ColumnName;
            //        bc.HeaderText = c.ColumnName;
            //        //bc.DataFormatString = setFormating(c);
            //        grdTransactionDetails.Columns.Add(bc);
            //    }
            //    i++;
            //}
            grdTransactionDetails.DataSource = _bill_Sys_BillTransaction.BindTransactionData(id);
            grdTransactionDetails.DataBind();


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
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
                grdTransactionDetails.Columns[7].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[8].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void CalculateAmount(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindIC9CodeControl(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        ArrayList _arrayList;
        _bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {

            _arrayList = new ArrayList();
            _arrayList = _bill_Sys_BillingCompanyDetails_BO.GetIC9CodeData(id);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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

            // txtDateOfservice.Text = "";
            // txtDateOfServiceTo.Text = "";

            // btnAddService.Enabled = true;
            // btnFromDate.Visible = true;

            // changed. amod. 02-feb-2010 removed extended drop down and added simple ddl instead
            // extddlDoctor.Text = "NA";
            extddlDoctor.SelectedValue = "NA";
            txtBillDate.Text = "";
            //txtDateOfservice.Text = "";
            //txtDateOfServiceTo.Text = "";
            grdTransactionDetails.DataSource = null;
            grdTransactionDetails.DataBind();
            Session["SELECTED_DIA_PRO_CODE"] = null;
            Session["SZ_BILL_NUMBER"] = null;
            lstDiagnosisCodes.DataSource = null;
            lstDiagnosisCodes.DataBind();
            lblMsg.Visible = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SaveTransactionData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList _arrayList;
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            //if (txtDateOfServiceTo.Visible == true)
            // {
            //System.TimeSpan diffResult = Convert.ToDateTime(txtDateOfServiceTo.Text).Subtract(Convert.ToDateTime(txtDateOfservice.Text));
            //int datecount = diffResult.Days;
            //DateTime dtdate = Convert.ToDateTime(txtDateOfservice.Text);

            //dtdate = dtdate.AddDays(1);

            // }
            // else if (txtDateOfServiceTo.Visible == false)
            // {


            // }
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
            //txtDateOfservice.Text = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[3].Text;

            txtAmount.Text = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[8].Text;

            //btnAddService.Enabled = false;
            //btnFromDate.Visible = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdTransactionDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
                //BindTransactionDetailsGrid(txtBillNo.Text);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
        // ArrayList _arrayList;
        try
        {
            string billno = "";
            // change. amod 02-feb-2010. removed extended ddl and added simple ddl instead
            // if (extddlDoctor.Text != "NA")

            //changed by shailesh 31Mar2010, accepted doctor id from hidden field.
            //if (extddlDoctor.SelectedValue != "NA")
            //{

            #region "Save information into BillTransactionEO"

            BillTransactionEO _objBillEO = new BillTransactionEO();


            _objBillEO.SZ_CASE_ID = txtCaseID.Text;
            _objBillEO.SZ_COMPANY_ID = txtCompanyID.Text;
            _objBillEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);
            // change. amod 02-feb-2010. removed extended ddl and added simple ddl instead                
            // _objBillEO.SZ_DOCTOR_ID = extddlDoctor.Text;

            // changed by shailesh 31Mar2010, accepted doctor id from hidden field.
            //_objBillEO.SZ_DOCTOR_ID = extddlDoctor.SelectedValue;
            _objBillEO.SZ_DOCTOR_ID = hndDoctorID.Value.ToString();
            _objBillEO.SZ_TYPE = ddlType.Text;
            _objBillEO.SZ_TESTTYPE = ""; //ddlTestType.Text;
            _objBillEO.FLAG = "ADD";
            _objBillEO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            #endregion

            _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();

            ArrayList objALEventEO = new ArrayList();
            ArrayList objALEventRefferProcedureEO = new ArrayList();

            // Start : Update Visit Status.
            //if (grdAllReports.Visible == true)
            if (grdCompleteVisit.Visible == true)
            {

                Bill_Sys_Calender _bill_Sys_Visit_BO = new Bill_Sys_Calender();
                ArrayList objList;

                //foreach (DataGridItem dgItem in grdAllReports.Items)
                //{
                //    if (((CheckBox)dgItem.Cells[0].FindControl("chkselect")).Checked == true)
                //    {
                //        EventEO _objEventEO = new EventEO();
                //        _objEventEO.I_EVENT_ID = dgItem.Cells[4].Text;
                //        _objEventEO.BT_STATUS = "1";
                //        _objEventEO.I_STATUS = "2";
                //        _objEventEO.SZ_BILL_NUMBER = "";
                //        _objEventEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);
                //        objALEventEO.Add(_objEventEO);
                //        foreach (DataGridItem j in grdTransactionDetails.Items)
                //        {
                //            if (dgItem.Cells[2].Text == j.Cells[1].Text)
                //            {
                //                EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                //                objEventRefferProcedureEO.SZ_PROC_CODE = j.Cells[11].Text;
                //                objEventRefferProcedureEO.I_EVENT_ID = dgItem.Cells[4].Text;
                //                objEventRefferProcedureEO.I_STATUS = "2";
                //                objALEventRefferProcedureEO.Add(objEventRefferProcedureEO);
                //            }
                //        }
                //    }
                //}
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
                                if (DateTime.Compare(Convert.ToDateTime(item.Cells[3].Text), Convert.ToDateTime(j.Cells[1].Text)) == 0)
                                {
                                    EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                                    objEventRefferProcedureEO.SZ_PROC_CODE = j.Cells[11].Text;
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

                    if (j.Cells[6].Text.ToString() != "&nbsp;")
                    {
                        objBillProcedureCodeEO.FL_AMOUNT = j.Cells[6].Text.ToString();
                    }
                    else
                    {
                        objBillProcedureCodeEO.FL_AMOUNT = "0";
                    }

                    objBillProcedureCodeEO.SZ_BILL_NUMBER = "";

                    objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(j.Cells[1].Text.ToString());

                    objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                    objBillProcedureCodeEO.I_UNIT = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString();

                    objBillProcedureCodeEO.FLT_PRICE = ((Label)j.Cells[5].FindControl("lblPrice")).Text;
                    objBillProcedureCodeEO.DOCT_AMOUNT = ((Label)j.Cells[5].FindControl("lblPrice")).Text;

                    objBillProcedureCodeEO.FLT_FACTOR = ((Label)j.Cells[5].FindControl("lblFactor")).Text;

                    if (j.Cells[8].Text.ToString() != "&nbsp;" && j.Cells[8].Text.ToString() != "")
                    {
                        objBillProcedureCodeEO.PROC_AMOUNT = j.Cells[8].Text.ToString();
                    }
                    else
                    {
                        objBillProcedureCodeEO.PROC_AMOUNT = "0";
                    }

                    // change. amod 02-feb-2010. removed extended ddl and added simple ddl instead                
                    // objBillProcedureCodeEO.SZ_DOCTOR_ID = extddlDoctor.Text;

                    // changed by shailesh 31Mar2010, accepted doctor id from hidden field.
                    //objBillProcedureCodeEO.SZ_DOCTOR_ID = extddlDoctor.SelectedValue; //removed by shailesh

                    objBillProcedureCodeEO.SZ_DOCTOR_ID = hndDoctorID.Value.ToString();

                    objBillProcedureCodeEO.SZ_CASE_ID = txtCaseID.Text;

                    objBillProcedureCodeEO.SZ_TYPE_CODE_ID = j.Cells[11].Text.ToString();

                    if (szCaseType == "WC000000000000000002")
                    {
                        objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[12].Text.ToString();
                        objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[13].Text.ToString();
                    }
                    else
                    {
                        objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                        objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                    }
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
            objResult = objBT_DAO.SaveBillTransaction(_objBillEO, objALEventEO, objALEventRefferProcedureEO, _objALBillProcedureCodeEO, objALBillDiagnosisCodeEO);
            if (objResult.msg_code == "ERR")
            {
                lblMsg.Text = objResult.msg;
                lblMsg.Visible = true;
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

                BindLatestTransaction();
                BindVisitCompleteGrid();
                ClearControl();
                lblMsg.Visible = true;
                //btnSave.Enabled = false;
                lblMsg.Text = " Bill Saved successfully ! ";
                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000002")
                {
                    GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                else if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000001")
                {
                    // GeneratePDFForWorkerComp(txtBillID.Text, txtCaseID.Text,"1");
                }
            }
            //}
            //else
            //{
            //    lblMsg.Visible = true;
            //    lblMsg.Text = " Select doctor ... ! ";
            //}

        }
        catch(Exception ex)
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
            String szNewPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/No Fault File/Bills/" + hdnSpeciality.Value.Trim() + "/";
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
            objAL1.Add(hdnSpeciality.Value.Trim());
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
        try
        {
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
            string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
            String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DIAG_PAGE");

            String szNextDiagPDFFileName = "";
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
                return szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf");
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
        return null;
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
        try
        {
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
            string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
            String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DIAG_PAGE");

            String szNextDiagPDFFileName = "";
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
                return szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf");
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
        return null;
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

           string szFinalPath = _objNFBill.GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, rdbListPDFType.SelectedValue, szCompanyID, CmpName, UserId, UserName, CaseNO, hdnSpeciality.Value.ToString(), 0);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            // change. amod 02-feb-2010. removed extended ddl and added simple ddl instead
            // if (extddlDoctor.Text != "NA")

            // changed by shailesh 31Mar2010, accepted doctor id from hidden field.
            //if (extddlDoctor.SelectedValue != "NA")
            //{
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
                // change amod. 02-feb-2010 removed ext ddl and added simple ddl instead
                // _objBillEO.SZ_DOCTOR_ID = extddlDoctor.Text;
                // changed by shailesh 31Mar2010, accepted doctor id from hidden field.
                //_objBillEO.SZ_DOCTOR_ID = extddlDoctor.SelectedValue;
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
                        if (j.Cells[6].Text.ToString() != "&nbsp;")
                        {
                            objBillProcedureCodeEO.FL_AMOUNT = j.Cells[6].Text.ToString();
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

                        // 5 - SZ_COMPANY_ID
                        objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                        // 6 - I_UNIT
                        objBillProcedureCodeEO.I_UNIT = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString();

                        // 7 - FLT_PRICE , DOCT_AMOUNT
                        objBillProcedureCodeEO.FLT_PRICE = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString();
                        objBillProcedureCodeEO.DOCT_AMOUNT = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString();

                        // 8 - FLT_FACTOR
                        objBillProcedureCodeEO.FLT_FACTOR = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString();

                        // 9 -
                        //_arrayList.Add(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString());
                        // objBillProcedureCodeEO.I_UNIT = "1";

                        // 10 - PROC_AMOUNT
                        objBillProcedureCodeEO.PROC_AMOUNT = j.Cells[8].Text.ToString();


                        if (j.Cells[0].Text.ToString() != "" && j.Cells[0].Text.ToString() != "&nbsp;")
                        {
                            // 11 - SZ_TYPE_CODE_ID
                            objBillProcedureCodeEO.SZ_TYPE_CODE_ID = j.Cells[12].Text.ToString();
                            objBillProcedureCodeEO.FLAG = "UPDATE";
                            _objALBillProcedureCodeEO.Add(objBillProcedureCodeEO);
                            // _bill_Sys_BillTransaction.UpdateTransactionData(_arrayList);
                        }
                        else
                        {
                            // 11 - SZ_DOCTOR_ID
                            // changed amod. 02-feb-2010 removed extended ddl control
                            // and added simple ddl instead
                            // objBillProcedureCodeEO.SZ_DOCTOR_ID  = extddlDoctor.Text;

                            // changed by shailesh 31Mar2010, accepted doctor id from hidden field.
                            //objBillProcedureCodeEO.SZ_DOCTOR_ID = extddlDoctor.SelectedValue;
                            objBillProcedureCodeEO.SZ_DOCTOR_ID = hndDoctorID.Value.ToString();

                            // 12 - SZ_CASE_ID
                            objBillProcedureCodeEO.SZ_CASE_ID = txtCaseID.Text;

                            // 13 - SZ_TYPE_CODE_ID
                            objBillProcedureCodeEO.SZ_TYPE_CODE_ID = j.Cells[12].Text.ToString();
                            objBillProcedureCodeEO.FLAG = "ADD";

                            _objALBillProcedureCodeEO.Add(objBillProcedureCodeEO);

                            //  _bill_Sys_BillTransaction.SaveTransactionData(_arrayList);
                        }
                    }
                }

                ArrayList objALEventRefferProcedureEO = new ArrayList();
                foreach (DataGridItem j in grdTransactionDetails.Items)
                {
                    if (j.Cells[0].Text.ToString() != "" && j.Cells[0].Text.ToString() != "&nbsp;")
                    {
                    }
                    else
                    {
                        EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                        objEventRefferProcedureEO.SZ_EVENT_DATE = j.Cells[1].Text;
                        objEventRefferProcedureEO.SZ_PROC_CODE = j.Cells[11].Text;
                        objEventRefferProcedureEO.I_EVENT_ID = "";
                        objEventRefferProcedureEO.I_STATUS = "2";
                        objALEventRefferProcedureEO.Add(objEventRefferProcedureEO);
                    }
                }

                ArrayList objALBillDiagnosisCodeEO = new ArrayList();

                //   _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
                foreach (ListItem lstItem in lstDiagnosisCodes.Items)
                {
                    BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    _arrayList = new ArrayList();
                    _arrayList.Add(lstItem.Value.ToString());
                    objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID = lstItem.Value.ToString();
                    objALBillDiagnosisCodeEO.Add(objBillDiagnosisCodeEO);
                    _arrayList.Add(billno);
                    //      _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arrayList);
                    //      objALBillDiagnosisCodeEO.Add(objBillDiagnosisCodeEO);
                }

                lblMsg.Visible = true;

                ArrayList objALDeletedProcCodes = new ArrayList();
                objALDeletedProcCodes = (ArrayList)Session["DELETED_PROC_CODES"];

                BillTransactionDAO objBT_DAO = new BillTransactionDAO();
                Result objResult = new Result();
                objResult = objBT_DAO.UpdateBillTransaction(_objBillEO, _objALBillProcedureCodeEO, objALBillDiagnosisCodeEO, objALDeletedProcCodes, objALEventRefferProcedureEO);
                Session["DELETED_PROC_CODES"] = null;
                if (objResult.msg_code == "SCC")
                {

                    // Start : Update Bill Notes for Bill.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_UPDATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


                    // End 

                    lblMsg.Text = " Bill Updated successfully ! ";
                }
                else
                {
                    lblMsg.Text = objResult.msg;
                }

            }



            BindTransactionDetailsGrid(Session["BillID"].ToString());
            BindLatestTransaction();
            //BindGrid();

            //}
            //else
            //{
            //    lblMsg.Visible = true;
            //    lblMsg.Text = " Select doctor ... ! ";
            //}
        }
        catch (SqlException objSqlExcp)
        {
            ErrorDiv.InnerText = " Bill Number already exists";
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
            datatable.Columns.Add("FACTOR_AMOUNT");
            datatable.Columns.Add("FACTOR");
            datatable.Columns.Add("PROC_AMOUNT");
            datatable.Columns.Add("DOCT_AMOUNT");
            datatable.Columns.Add("I_UNIT");
            datatable.Columns.Add("SZ_TYPE_CODE_ID");
            datatable.Columns.Add("FLT_GROUP_AMOUNT");
            datatable.Columns.Add("I_GROUP_AMOUNT_ID");
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
                    if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0) { dr["FACTOR_AMOUNT"] = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString(); }
                    if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0) { dr["FACTOR"] = ((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString(); }
                    dr["FLT_AMOUNT"] = j.Cells[6].Text.ToString();
                    if (((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString(); } }
                    dr["PROC_AMOUNT"] = j.Cells[9].Text.ToString();
                    dr["DOCT_AMOUNT"] = j.Cells[10].Text.ToString();
                    dr["SZ_TYPE_CODE_ID"] = j.Cells[11].Text.ToString();
                    dr["FLT_GROUP_AMOUNT"] = j.Cells[12].Text.ToString();
                    dr["I_GROUP_AMOUNT_ID"] = j.Cells[13].Text.ToString();
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
                            //     _bill_Sys_BillTransaction.DeleteTransactionDetails(grdTransactionDetails.Items[i].Cells[0].Text);
                            //objALRemoveProcCodes.Add(grdTransactionDetails.Items[i].Cells[0].Text);
                            //grdTransactionDetails.Items[i].Cells.Clear();
                            //grdTransactionDetails.Items[i].Cells.Remove(grdTransactionDetails.CurrentR
                            string date = grdTransactionDetails.Items[i].Cells[1].Text;
                            string procCode = grdTransactionDetails.Items[i].Cells[3].Text;
                            for (int j = 0; j < datatable.Rows.Count; j++)
                            {

                                if (procCode == datatable.Rows[j][3].ToString() && DateTime.Compare(Convert.ToDateTime(date), Convert.ToDateTime(datatable.Rows[j][1].ToString())) == 0)
                                {
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
                                    datatable.Rows[j].Delete();
                                    flag = 1;
                                    break;
                                }
                                else
                                {
                                    flag = 2;
                                }
                            }
                            //grdTransactionDetails.Items[i].Cells.Clear();
                            //grdTransactionDetails.Items[i].Cells.RemoveAt(i);
                        }
                    }
                }
            }
            grdTransactionDetails.DataSource = datatable;
            grdTransactionDetails.DataBind();

            // add deleted procedure codes in session
            Session["DELETED_PROC_CODES"] = objALRemoveProcCodes;


            // BindTransactionDetailsGrid(Session["BillID"].ToString());
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

    #endregion

    #region "extddlDoctor Change event"
    protected void extddlDoctor_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // changed - 02-feb-2010 . Removed extended drop down list and added simple
        // drop down instead
        //if (extddlDoctor.Text != "NA")

        if (extddlDoctor.SelectedValue != "NA")
        {
            // changed - 02-feb-2010 . Removed extended drop down list and added simple
            // drop down instead
            //GetProcedureCode(extddlDoctor.Text);
            //Session["TEMP_DOCTOR_ID"] = extddlDoctor.Text;

            GetProcedureCode(extddlDoctor.SelectedValue);
            Session["TEMP_DOCTOR_ID"] = extddlDoctor.SelectedValue;

            _bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            string keyValue = _bill_Sys_LoginBO.getDefaultSettings(txtCompanyID.Text, "SS00005");
            if (keyValue == "1")
            {
                // Bind Visit Grid --------------------------
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
                // Bind Procedure Grid --------------------------
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

            // changed. amod. 02-feb-2010 removed extended drop down and added simple ddl instead
            // if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.Text, txtCompanyID.Text))

            // changed by shailesh, accepted doctor id from the hidden field.
            //if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.SelectedValue, txtCompanyID.Text))
            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[7].Visible = true;
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
            }
            #endregion



            #region "Get Diagnosis code associated with case according to speciality of Doctor"

            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();

            // changed. amod. 02-feb-2010 removed extended drop down and added simple ddl instead
            // lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, extddlDoctor.Text, txtCompanyID.Text).Tables[0];

            // changed by shailesh 31Mar2010, accepted doctor id from hidden field.
            //lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, extddlDoctor.SelectedValue, txtCompanyID.Text).Tables[0];
            lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, hndDoctorID.Value.ToString(), txtCompanyID.Text).Tables[0];
            lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            lstDiagnosisCodes.DataValueField = "CODE";
            lstDiagnosisCodes.DataBind();    //Bind select Doctor
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
            #endregion
            // -------------------------------------



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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "<script>javascript:showDiagnosisCodePopup();</script>", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

    #region "Generate PDF Logic"

    //private string MergePDF.MergePDFFiles(string p_szSource1, string p_szSource2, string p_szDestinationFileName)
    //{
    //    try
    //    {
    //        CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
    //        int iResult = 0;
    //        string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
    //        objMyForm.initialize(KeyForCutePDF);

    //        if (objMyForm == null)
    //        {

    //        }
    //        else
    //        {
    //            if (System.IO.File.Exists(p_szSource1) && System.IO.File.Exists(p_szSource2))
    //            {
    //                iResult = objMyForm.mergePDF(p_szSource1, p_szSource2, p_szDestinationFileName);
    //                log.Debug("Bill_Sys_BillTransaction. Method - MergePDF.MergePDFFiles : Merge Result " + iResult);
    //                //     iResult = objMyForm.mergePDF("D:/1.pdf", "D:/2.pdf", "D:/3.pdf");
    //            }
    //        }
    //        if (iResult == 0)
    //            return "FAIL";
    //        else
    //            return "SUCCESS";
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Debug("Bill_Sys_BillTransaction. Method - MergePDF.MergePDFFiles : " + ex.Message.ToString());
    //        throw ex;
    //    }
    //}

    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            String szSpecility = p_szSpeciality;


            Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            Session["TM_SZ_BILL_ID"] = p_szBillNumber;

            objNF3Template = new Bill_Sys_NF3_Template();

            CaseDetailsBO objCaseDetails = new CaseDetailsBO();
            String szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                String szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
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

                MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);
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
                _objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyId, szCaseId, szSpecility, szCompanyName, szBillId, szUserName, szUserId);
                //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + _objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyId, szCaseId, szSpecility, szCompanyName, szBillId, szUserName, szUserId) + "'); ", true);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
                    dr["SZ_TYPE_CODE_ID"] = j.Cells[11].Text.ToString();
                    dr["FLT_GROUP_AMOUNT"] = j.Cells[12].Text.ToString();
                    dr["I_GROUP_AMOUNT_ID"] = j.Cells[13].Text.ToString();
                    dt.Rows.Add(dr);
                }

            }

            string[] dateOfService = txtDateOfservice.Text.Split(',');
            _bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            string keyValue = _bill_Sys_LoginBO.getDefaultSettings(txtCompanyID.Text, "SS00005");
            if (keyValue == "1")
            {
                foreach (DataGridItem dgItem in grdAllReports.Items)
                {
                    if (((CheckBox)dgItem.Cells[0].FindControl("chkselect")).Checked == true)
                    {
                        foreach (DataGridItem j in grdProcedure.Items)
                        {
                            CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                            if (chkSelect.Checked)
                            {
                                dr = dt.NewRow();
                                dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                dr["DT_DATE_OF_SERVICE"] = dgItem.Cells[2].Text.ToString();//txtDateOfservice.Text;
                                dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();
                                dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                                dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                                dr["FACTOR_AMOUNT"] = j.Cells[5].Text.ToString();
                                dr["FACTOR"] = "1";
                                dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                                dr["I_UNIT"] = "1";
                                dr["PROC_AMOUNT"] = j.Cells[5].Text.ToString();
                                dr["DOCT_AMOUNT"] = j.Cells[5].Text.ToString();
                                dr["SZ_TYPE_CODE_ID"] = j.Cells[2].Text.ToString();
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }

                if (grdAllReports.Items.Count == 0)
                {
                    if (grdCompleteVisit.Items.Count > 0)
                    {
                        int flag = 0;
                        foreach (DataGridItem item in grdCompleteVisit.Items)
                        {
                            CheckBox chk1 = (CheckBox)item.FindControl("chkSelectItem");
                            if (chk1.Checked)
                            {
                                string visit_date = item.Cells[3].Text;
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
                                        if (flag == 2 || flag == 0)
                                        {
                                            dr = dt.NewRow();
                                            dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                            dr["DT_DATE_OF_SERVICE"] = visit_date;//txtDateOfservice.Text;
                                            dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();
                                            dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                                            dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                                            dr["FACTOR_AMOUNT"] = j.Cells[5].Text.ToString();
                                            dr["FACTOR"] = "1";
                                            dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                                            dr["I_UNIT"] = "1";
                                            dr["PROC_AMOUNT"] = j.Cells[5].Text.ToString();
                                            dr["DOCT_AMOUNT"] = j.Cells[5].Text.ToString();
                                            dr["SZ_TYPE_CODE_ID"] = j.Cells[2].Text.ToString();
                                            dt.Rows.Add(dr);
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
                                        //foreach (DataRow dr in dt.Rows)
                                        //{

                                        //    if (dr.Table[0].Cells[3].Text == j.Cells[3].Text && DateTime.Compare(Convert.ToDateTime(visit_date), Convert.ToDateTime(item1.Cells[1].Text)) == 0)
                                        //    {
                                        //        flag = 1;
                                        //        break;
                                        //    }
                                        //    else
                                        //    {
                                        //        flag = 2;
                                        //    }
                                        //}
                                        if (flag == 2)
                                        {
                                            dr = dt.NewRow();
                                            dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                            dr["DT_DATE_OF_SERVICE"] = visit_date;//txtDateOfservice.Text;
                                            dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();
                                            dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                                            dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                                            dr["FACTOR_AMOUNT"] = j.Cells[5].Text.ToString();
                                            dr["FACTOR"] = "1";
                                            dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                                            dr["I_UNIT"] = "1";
                                            dr["PROC_AMOUNT"] = j.Cells[5].Text.ToString();
                                            dr["DOCT_AMOUNT"] = j.Cells[5].Text.ToString();
                                            dr["SZ_TYPE_CODE_ID"] = j.Cells[2].Text.ToString();
                                            dt.Rows.Add(dr);
                                        }

                                    }

                                }
                            }

                        }
                    }

                    //foreach (Object dtServiceDate in dateOfService)
                    //{
                    //    if (dtServiceDate.ToString() != "")
                    //    {
                    //foreach (DataGridItem j in grdProcedure.Items)
                    //{
                    //    CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                    //    if (chkSelect.Checked)
                    //    {
                    //        dr = dt.NewRow();
                    //        dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                    //        dr["DT_DATE_OF_SERVICE"] = txtBillDate.Text;//txtDateOfservice.Text;
                    //        dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();
                    //        dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                    //        dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                    //        dr["FACTOR_AMOUNT"] = j.Cells[5].Text.ToString();
                    //        dr["FACTOR"] = "1";
                    //        dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                    //        dr["I_UNIT"] = "1";
                    //        dr["PROC_AMOUNT"] = j.Cells[5].Text.ToString();
                    //        dr["DOCT_AMOUNT"] = j.Cells[5].Text.ToString();
                    //        dr["SZ_TYPE_CODE_ID"] = j.Cells[2].Text.ToString();
                    //        dt.Rows.Add(dr);
                    //    }
                    //}
                    //    }
                    //}
                }
            }
            else
            {

                //foreach (Object dtServiceDate in dateOfService)
                //{
                //    if (dtServiceDate.ToString() != "")
                //    {
                foreach (DataGridItem j in grdProcedure.Items)
                {
                    CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                    if (chkSelect.Checked)
                    {
                        dr = dt.NewRow();
                        dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                        dr["DT_DATE_OF_SERVICE"] = txtBillDate.Text;//txtDateOfservice.Text;
                        dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();
                        dr["SZ_PROCEDURAL_CODE"] = j.Cells[3].Text.ToString();
                        dr["SZ_CODE_DESCRIPTION"] = j.Cells[4].Text.ToString();
                        dr["FACTOR_AMOUNT"] = j.Cells[5].Text.ToString();
                        dr["FACTOR"] = "1";
                        dr["FLT_AMOUNT"] = j.Cells[5].Text.ToString();
                        dr["I_UNIT"] = "1";
                        dr["PROC_AMOUNT"] = j.Cells[5].Text.ToString();
                        dr["DOCT_AMOUNT"] = j.Cells[5].Text.ToString();
                        dr["SZ_TYPE_CODE_ID"] = j.Cells[2].Text.ToString();
                        dt.Rows.Add(dr);
                    }
                }
                //    }
                //}
            }

            grdTransactionDetails.DataSource = dt;
            grdTransactionDetails.DataBind();

            #region "Disply Unit Column if speciality have unit bit set to true."

            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            // changed. amod. 02-feb-2010 removed extended drop down and added simple ddl instead
            // if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.Text, txtCompanyID.Text))

            //if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.SelectedValue, txtCompanyID.Text))
            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[7].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[8].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            dt.Columns.Add("FLT_GROUP_AMOUNT");
            dt.Columns.Add("I_GROUP_AMOUNT_ID");
            DataRow dr;

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
                    if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0) { dr["FACTOR_AMOUNT"] = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString(); }
                    if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0) { dr["FACTOR"] = ((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString(); }
                    dr["FLT_AMOUNT"] = j.Cells[6].Text.ToString();
                    if (((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString(); } }
                    dr["PROC_AMOUNT"] = j.Cells[9].Text.ToString();
                    dr["DOCT_AMOUNT"] = j.Cells[10].Text.ToString();
                    dr["SZ_TYPE_CODE_ID"] = j.Cells[11].Text.ToString();
                    dr["FLT_GROUP_AMOUNT"] = j.Cells[12].Text.ToString();
                    dr["I_GROUP_AMOUNT_ID"] = j.Cells[13].Text.ToString();
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
                                    dr["DT_DATE_OF_SERVICE"] = dgItem.Cells[2].Text.ToString();//txtGroupDateofService.Text;
                                    dr["SZ_PROCEDURE_ID"] = dtRow.ItemArray.GetValue(0);
                                    dr["SZ_PROCEDURAL_CODE"] = dtRow.ItemArray.GetValue(2);
                                    dr["SZ_CODE_DESCRIPTION"] = dtRow.ItemArray.GetValue(3);
                                    dr["FACTOR_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                    dr["FACTOR"] = "1";
                                    dr["FLT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                    dr["I_UNIT"] = "1";
                                    dr["PROC_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                    dr["DOCT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
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

                }

                if (grdAllReports.Items.Count == 0)
                {
                    //foreach (Object dtServiceDate in dateOfService)
                    //{
                    //    if (dtServiceDate.ToString() != "")
                    //    {

                    if (grdCompleteVisit.Items.Count > 0)
                    {
                        int flag = 0;
                        foreach (DataGridItem item in grdCompleteVisit.Items)
                        {
                            CheckBox chk1 = (CheckBox)item.FindControl("chkSelectItem");
                            if (chk1.Checked)
                            {
                                string visit_date = item.Cells[3].Text;
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
                                            if (flag == 2 || flag == 0)
                                            {
                                                dr = dt.NewRow();
                                                dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                                dr["DT_DATE_OF_SERVICE"] = visit_date; // dtServiceDate.ToString();//txtGroupDateofService.Text;
                                                dr["SZ_PROCEDURE_ID"] = dtRow.ItemArray.GetValue(0);
                                                dr["SZ_PROCEDURAL_CODE"] = dtRow.ItemArray.GetValue(2);
                                                dr["SZ_CODE_DESCRIPTION"] = dtRow.ItemArray.GetValue(3);
                                                dr["FACTOR_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                                dr["FACTOR"] = "1";
                                                dr["FLT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                                dr["I_UNIT"] = "1";
                                                dr["PROC_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                                dr["DOCT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
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
                                            dr["FACTOR_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                            dr["FACTOR"] = "1";
                                            dr["FLT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                            dr["I_UNIT"] = "1";
                                            dr["PROC_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                                            dr["DOCT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
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
                        }
                    }
                    //foreach (DataGridItem j in grdGroupProcCodeService.Items)
                    //{
                    //    CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                    //    if (chkSelect.Checked)
                    //    {
                    //        DataSet ds = _bill_Sys_BillTransaction.GroupProcedureCodeList(j.Cells[1].Text.ToString(), txtCompanyID.Text, j.Cells[2].Text.ToString());
                    //        int rowSt = 1;
                    //        foreach (DataRow dtRow in ds.Tables[0].Rows)
                    //        {
                    //            dr = dt.NewRow();
                    //            dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                    //            dr["DT_DATE_OF_SERVICE"] = txtBillDate.Text; // dtServiceDate.ToString();//txtGroupDateofService.Text;
                    //            dr["SZ_PROCEDURE_ID"] = dtRow.ItemArray.GetValue(0);
                    //            dr["SZ_PROCEDURAL_CODE"] = dtRow.ItemArray.GetValue(2);
                    //            dr["SZ_CODE_DESCRIPTION"] = dtRow.ItemArray.GetValue(3);
                    //            dr["FACTOR_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                    //            dr["FACTOR"] = "1";
                    //            dr["FLT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                    //            dr["I_UNIT"] = "1";
                    //            dr["PROC_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                    //            dr["DOCT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                    //            dr["SZ_TYPE_CODE_ID"] = dtRow.ItemArray.GetValue(1);
                    //            if (rowSt == ds.Tables[0].Rows.Count)
                    //            {
                    //                if (j.Cells[4].Text.ToString() != "") { dr["FLT_GROUP_AMOUNT"] = j.Cells[4].Text.ToString(); }
                    //            }
                    //            if (j.Cells[3].Text.ToString() != "") { dr["I_GROUP_AMOUNT_ID"] = j.Cells[3].Text.ToString(); }
                    //            dt.Rows.Add(dr);
                    //            rowSt = rowSt + 1;
                    //        }
                    //    }
                    //}
                    //    }
                    //}
                }
            }
            else
            {
                //foreach (Object dtServiceDate in dateOfService)
                //{
                //    if (dtServiceDate.ToString() != "")
                //    {
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
                            dr["DT_DATE_OF_SERVICE"] = txtBillDate;// dtServiceDate.ToString();//txtGroupDateofService.Text;
                            dr["SZ_PROCEDURE_ID"] = dtRow.ItemArray.GetValue(0);
                            dr["SZ_PROCEDURAL_CODE"] = dtRow.ItemArray.GetValue(2);
                            dr["SZ_CODE_DESCRIPTION"] = dtRow.ItemArray.GetValue(3);
                            dr["FACTOR_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                            dr["FACTOR"] = "1";
                            dr["FLT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                            dr["I_UNIT"] = "1";
                            dr["PROC_AMOUNT"] = dtRow.ItemArray.GetValue(4);
                            dr["DOCT_AMOUNT"] = dtRow.ItemArray.GetValue(4);
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
                //    }
                //}
            }
            grdTransactionDetails.DataSource = dt;
            grdTransactionDetails.DataBind();

            #region "Disply Unit Column if speciality have unit bit set to true."

            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            // changed. amod. 02-feb-2010 removed extended drop down and added simple ddl instead
            //if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.Text, txtCompanyID.Text))

            // changed by shailesh 31Mar2010, accepted doctor id from hidden field.
            //if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.SelectedValue, txtCompanyID.Text))
            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[7].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[8].Text;
                }
            }
            else
            {
                grdTransactionDetails.Columns[7].Visible = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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

            // changed. amod. 02-feb-2010 removed extended drop down and added simple ddl instead
            // objAL.Add(extddlDoctor.Text);

            // changed by shailesh 31Mar2010, accepted doctor id from hidden field. 
            //objAL.Add(extddlDoctor.SelectedValue);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            //int i = 0;
            //int x = 0;
            //int y = 0;
            //int flag = 0;
            int cnt = 0;
            //Random rand;
            
            #region "Add color to Grid"
            Random rand = new Random();

            for (int m = 0; m < grdCompleteVisit.Items.Count; m++)
            {
                string doctorname = grdCompleteVisit.Items[m].Cells[1].Text;
                arrlst.Add(doctorname);
                for (int n = 0; n < grdCompleteVisit.Items.Count; n++)
                {
                    if (doctorname == grdCompleteVisit.Items[n].Cells[1].Text)
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
                    if(doctorname == item.Cells[1].Text)
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnLoadProcedures_Click(object sender, EventArgs e)
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
            datatable.Columns.Add("FACTOR_AMOUNT");
            datatable.Columns.Add("FACTOR");
            datatable.Columns.Add("PROC_AMOUNT");
            datatable.Columns.Add("DOCT_AMOUNT");
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
            foreach (DataGridItem rw in grdCompleteVisit.Items)
            {
                CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {
                    string visit_type = rw.Cells[2].Text;
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
                            //if (ds.Tables[0].Rows.Count > 0)
                            //{
                            //    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            //    {
                            //        DataTable dtble = new DataTable();
                            //        dtble = _bill_Sys_Visit_BO.FillProcedureCodeGrid(ds.Tables[0].Rows[j][0].ToString(), txtCompanyID.Text);
                            //        arrLst.Add(dtble);
                            //    }
                            //}
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
                    dr["FACTOR_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["FACTOR"] = "1";
                    dr["FLT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["I_UNIT"] = "1";
                    dr["PROC_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["DOCT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["SZ_TYPE_CODE_ID"] = drow.ItemArray.GetValue(1).ToString();
                    datatable.Rows.Add(dr);
                }
            }

            grdTransactionDetails.DataSource = datatable;
            grdTransactionDetails.DataBind();

            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[7].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[8].Text;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            datatable.Columns.Add("FACTOR_AMOUNT");
            datatable.Columns.Add("FACTOR");
            datatable.Columns.Add("PROC_AMOUNT");
            datatable.Columns.Add("DOCT_AMOUNT");
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
            foreach (DataGridItem rw in grdCompleteVisit.Items)
            {
                CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {
                    string visit_type = rw.Cells[2].Text;
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
                            //if (ds.Tables[0].Rows.Count > 0)
                            //{
                            //    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            //    {
                            //        DataTable dtble = new DataTable();
                            //        dtble = _bill_Sys_Visit_BO.FillProcedureCodeGrid(ds.Tables[0].Rows[j][0].ToString(), txtCompanyID.Text);
                            //        arrLst.Add(dtble);
                            //    }
                            //}
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
                    dr["FACTOR_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["FACTOR"] = "1";
                    dr["FLT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["I_UNIT"] = "1";
                    dr["PROC_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["DOCT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["SZ_TYPE_CODE_ID"] = drow.ItemArray.GetValue(1).ToString();
                    datatable.Rows.Add(dr);
                }
            }

            grdTransactionDetails.DataSource = datatable;
            grdTransactionDetails.DataBind();

            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[7].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[8].Text;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            string docName = e.Item.Cells[1].Text;
            string visitype = e.Item.Cells[2].Text;
            string docID = e.Item.Cells[8].Text;
            if (chk != null)
            {
                chk.Attributes.Add("onclick", "return ValidateGridCheckBox('" + docName + "','" + visitype + "','" + docID + "');");
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

    protected void btnQuickBill_Click(object sender, EventArgs e)
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
            datatable.Columns.Add("FACTOR_AMOUNT");
            datatable.Columns.Add("FACTOR");
            datatable.Columns.Add("PROC_AMOUNT");
            datatable.Columns.Add("DOCT_AMOUNT");
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
            int flag = 0;
            foreach (DataGridItem rw in grdCompleteVisit.Items)
            {
                CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {
                    string visit_type = rw.Cells[2].Text;
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
                            if (dt.Rows.Count > 0)
                            {
                                arrLst.Add(dt);
                            }
                            else
                            {
                                flag = 1;
                            }
                            //if (ds.Tables[0].Rows.Count > 0)
                            //{
                            //    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            //    {
                            //        DataTable dtble = new DataTable();
                            //        dtble = _bill_Sys_Visit_BO.FillProcedureCodeGrid(ds.Tables[0].Rows[j][0].ToString(), txtCompanyID.Text);
                            //        arrLst.Add(dtble);
                            //    }
                            //}
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
                    dr["DT_DATE_OF_SERVICE"] = drow.ItemArray.GetValue(5).ToString();
                    dr["SZ_PROCEDURE_ID"] = drow.ItemArray.GetValue(0).ToString();
                    dr["SZ_PROCEDURAL_CODE"] = drow.ItemArray.GetValue(2).ToString();
                    dr["SZ_CODE_DESCRIPTION"] = drow.ItemArray.GetValue(3).ToString();
                    dr["FACTOR_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["FACTOR"] = "1";
                    dr["FLT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["I_UNIT"] = "1";
                    dr["PROC_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["DOCT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["SZ_TYPE_CODE_ID"] = drow.ItemArray.GetValue(1).ToString();
                    datatable.Rows.Add(dr);
                }
            }

            grdTransactionDetails.DataSource = datatable;
            grdTransactionDetails.DataBind();


            DataSet dset1 = new DataSet();
            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO1 = new Bill_Sys_AssociateDiagnosisCodeBO();

            dset1 = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, Doctor_Id, txtCompanyID.Text);
            
            if (dset1.Tables[0].Rows.Count == 0)
            {
                flag = 2;
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
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('No Procedure Code for visit. Cannot generate bill');</script>");
                grdTransactionDetails.DataSource = null;
                grdTransactionDetails.DataBind();
                return;
            }
            else if (flag == 2)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('No diagnosis codes associated with speciality. Cannot generate bill');</script>");
                grdTransactionDetails.DataSource = null;
                grdTransactionDetails.DataBind();
                return;
            }
            else
            {
                saveQuickBills(Doctor_Id);
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

    #region "Save Bill Information"

    protected void saveQuickBills(string sz_docID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        // ArrayList _arrayList;
        try
        {
            string billno = "";
            // change. amod 02-feb-2010. removed extended ddl and added simple ddl instead
            // if (extddlDoctor.Text != "NA")

            //changed by shailesh 31Mar2010, accepted doctor id from hidden field.
            //if (extddlDoctor.SelectedValue != "NA")
            //{

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

            // Start : Update Visit Status.
            //if (grdAllReports.Visible == true)
            if (grdCompleteVisit.Visible == true)
            {

                Bill_Sys_Calender _bill_Sys_Visit_BO = new Bill_Sys_Calender();
                ArrayList objList;

                //foreach (DataGridItem dgItem in grdAllReports.Items)
                //{
                //    if (((CheckBox)dgItem.Cells[0].FindControl("chkselect")).Checked == true)
                //    {
                //        EventEO _objEventEO = new EventEO();
                //        _objEventEO.I_EVENT_ID = dgItem.Cells[4].Text;
                //        _objEventEO.BT_STATUS = "1";
                //        _objEventEO.I_STATUS = "2";
                //        _objEventEO.SZ_BILL_NUMBER = "";
                //        _objEventEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);
                //        objALEventEO.Add(_objEventEO);
                //        foreach (DataGridItem j in grdTransactionDetails.Items)
                //        {
                //            if (dgItem.Cells[2].Text == j.Cells[1].Text)
                //            {
                //                EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                //                objEventRefferProcedureEO.SZ_PROC_CODE = j.Cells[11].Text;
                //                objEventRefferProcedureEO.I_EVENT_ID = dgItem.Cells[4].Text;
                //                objEventRefferProcedureEO.I_STATUS = "2";
                //                objALEventRefferProcedureEO.Add(objEventRefferProcedureEO);
                //            }
                //        }
                //    }
                //}
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
                                if (DateTime.Compare(Convert.ToDateTime(item.Cells[3].Text), Convert.ToDateTime(j.Cells[1].Text)) == 0)
                                {
                                    EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                                    objEventRefferProcedureEO.SZ_PROC_CODE = j.Cells[11].Text;
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

                    if (j.Cells[6].Text.ToString() != "&nbsp;")
                    {
                        objBillProcedureCodeEO.FL_AMOUNT = j.Cells[6].Text.ToString();
                    }
                    else
                    {
                        objBillProcedureCodeEO.FL_AMOUNT = "0";
                    }

                    objBillProcedureCodeEO.SZ_BILL_NUMBER = "";

                    objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(j.Cells[1].Text.ToString());

                    objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                    objBillProcedureCodeEO.I_UNIT = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString();

                    objBillProcedureCodeEO.FLT_PRICE = ((Label)j.Cells[5].FindControl("lblPrice")).Text;
                    objBillProcedureCodeEO.DOCT_AMOUNT = ((Label)j.Cells[5].FindControl("lblPrice")).Text;

                    objBillProcedureCodeEO.FLT_FACTOR = ((Label)j.Cells[5].FindControl("lblFactor")).Text;

                    if (j.Cells[8].Text.ToString() != "&nbsp;" && j.Cells[8].Text.ToString() != "")
                    {
                        objBillProcedureCodeEO.PROC_AMOUNT = j.Cells[8].Text.ToString();
                    }
                    else
                    {
                        objBillProcedureCodeEO.PROC_AMOUNT = "0";
                    }

                    // change. amod 02-feb-2010. removed extended ddl and added simple ddl instead                
                    // objBillProcedureCodeEO.SZ_DOCTOR_ID = extddlDoctor.Text;

                    // changed by shailesh 31Mar2010, accepted doctor id from hidden field.
                    //objBillProcedureCodeEO.SZ_DOCTOR_ID = extddlDoctor.SelectedValue; //removed by shailesh

                    objBillProcedureCodeEO.SZ_DOCTOR_ID = sz_docID;

                    objBillProcedureCodeEO.SZ_CASE_ID = txtCaseID.Text;

                    objBillProcedureCodeEO.SZ_TYPE_CODE_ID = j.Cells[11].Text.ToString();

                    if (szCaseType == "WC000000000000000002")
                    {
                        objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[12].Text.ToString();
                        objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[13].Text.ToString();
                    }
                    else
                    {
                        objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                        objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                    }
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
            objResult = objBT_DAO.SaveBillTransaction(_objBillEO, objALEventEO, objALEventRefferProcedureEO, _objALBillProcedureCodeEO, objALBillDiagnosisCodeEO);
            if (objResult.msg_code == "ERR")
            {
                lblMsg.Text = objResult.msg;
                lblMsg.Visible = true;
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

                BindLatestTransaction();
                BindVisitCompleteGrid();
                ClearControl();
                lblMsg.Visible = true;
                //btnSave.Enabled = false;
                lblMsg.Text = " Bill Saved successfully ! ";
                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000002")
                {
                    GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000003")
                {
                    GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                else if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000001")
                {
                    // GeneratePDFForWorkerComp(txtBillID.Text, txtCaseID.Text,"1");
                }
            }
            //}
            //else
            //{
            //    lblMsg.Visible = true;
            //    lblMsg.Text = " Select doctor ... ! ";
            //}

        }
        catch(Exception ex) {
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

    #endregion

    protected void btnAddServices_Click(object sender, EventArgs e)
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
            datatable.Columns.Add("FACTOR_AMOUNT");
            datatable.Columns.Add("FACTOR");
            datatable.Columns.Add("PROC_AMOUNT");
            datatable.Columns.Add("DOCT_AMOUNT");
            datatable.Columns.Add("I_UNIT");
            datatable.Columns.Add("SZ_TYPE_CODE_ID");
            datatable.Columns.Add("FLT_GROUP_AMOUNT");
            datatable.Columns.Add("I_GROUP_AMOUNT_ID");
            DataRow dr;

            if (Request.QueryString["Type"] != null)
            {
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
                        if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0) { dr["FACTOR_AMOUNT"] = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString(); }
                        if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0) { dr["FACTOR"] = ((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString(); }
                        dr["FLT_AMOUNT"] = j.Cells[6].Text.ToString();
                        if (((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString(); } }
                        dr["PROC_AMOUNT"] = j.Cells[9].Text.ToString();
                        dr["DOCT_AMOUNT"] = j.Cells[10].Text.ToString();
                        dr["SZ_TYPE_CODE_ID"] = j.Cells[11].Text.ToString();
                        dr["FLT_GROUP_AMOUNT"] = j.Cells[12].Text.ToString();
                        dr["I_GROUP_AMOUNT_ID"] = j.Cells[13].Text.ToString();
                        datatable.Rows.Add(dr);
                    }

                }
            }

            string Doctor_Id = "";
            ArrayList objarr;
            ArrayList arrLst = new ArrayList();
            DataSet dset = new DataSet();
            Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            dset = _bill_Sys_Visit_BO.GetVisitTypeList(txtCompanyID.Text, "GetVisitType");
            foreach (DataGridItem rw in grdCompleteVisit.Items)
            {
                CheckBox chk = (CheckBox)(rw.Cells[0].FindControl("chkSelectItem"));
                if (chk.Checked == true)
                {
                    string visit_type = rw.Cells[2].Text;
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
                            //if (ds.Tables[0].Rows.Count > 0)
                            //{
                            //    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            //    {
                            //        DataTable dtble = new DataTable();
                            //        dtble = _bill_Sys_Visit_BO.FillProcedureCodeGrid(ds.Tables[0].Rows[j][0].ToString(), txtCompanyID.Text);
                            //        arrLst.Add(dtble);
                            //    }
                            //}
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
                    dr["FACTOR_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["FACTOR"] = "1";
                    dr["FLT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["I_UNIT"] = "1";
                    dr["PROC_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["DOCT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["SZ_TYPE_CODE_ID"] = drow.ItemArray.GetValue(1).ToString();
                    datatable.Rows.Add(dr);
                }
            }

            grdTransactionDetails.DataSource = datatable;
            grdTransactionDetails.DataBind();

            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[7].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[8].Text;
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

            //GetProcedureCode(hndDoctorID.Value.ToString());

            modalpopupAddservice.Show();

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

    protected void showModalPopup()
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
            datatable.Columns.Add("FACTOR_AMOUNT");
            datatable.Columns.Add("FACTOR");
            datatable.Columns.Add("PROC_AMOUNT");
            datatable.Columns.Add("DOCT_AMOUNT");
            datatable.Columns.Add("I_UNIT");
            datatable.Columns.Add("SZ_TYPE_CODE_ID");
            datatable.Columns.Add("FLT_GROUP_AMOUNT");
            datatable.Columns.Add("I_GROUP_AMOUNT_ID");
            DataRow dr;

            //if (Request.QueryString["Type"] != null)
            //{
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
                        if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0) { dr["FACTOR_AMOUNT"] = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString(); }
                        if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0) { dr["FACTOR"] = ((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString(); }
                        dr["FLT_AMOUNT"] = j.Cells[6].Text.ToString();
                        if (((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString(); } }
                        dr["PROC_AMOUNT"] = j.Cells[9].Text.ToString();
                        dr["DOCT_AMOUNT"] = j.Cells[10].Text.ToString();
                        dr["SZ_TYPE_CODE_ID"] = j.Cells[11].Text.ToString();
                        dr["FLT_GROUP_AMOUNT"] = j.Cells[12].Text.ToString();
                        dr["I_GROUP_AMOUNT_ID"] = j.Cells[13].Text.ToString();
                        datatable.Rows.Add(dr);
                    }

                }
            //}

            string Doctor_Id = "";
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
                    ////////Not required ....S.D
                    //string visit_type = rw.Cells[2].Text;
                    //for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    //{
                    //    if (visit_type == dset.Tables[0].Rows[i][1].ToString())
                    //    {
                    //        objarr = new ArrayList();
                    //        objarr.Add(txtCompanyID.Text);
                    //        objarr.Add(rw.Cells[6].Text);
                    //        objarr.Add(visit_type);
                    //        objarr.Add(rw.Cells[7].Text);
                    //        DataTable dt = new DataTable();
                    //        dt = _bill_Sys_Visit_BO.GetProcedureCodeList(objarr);
                    //        //dt = _bill_Sys_Visit_BO.GetProcedureCodeList(txtCompanyID.Text, "GetProcedureCodes", rw.Cells[6].Text, visit_type);
                    //        if (dt.Rows.Count > 0)
                    //        {
                    //            string codes = dt.Rows[0][2].ToString();
                    //            string date = dt.Rows[0][5].ToString();
                    //            for (int j = 0; j < datatable.Rows.Count; j++)
                    //            {

                    //                if (codes == datatable.Rows[j][3].ToString() && DateTime.Compare(Convert.ToDateTime(date), Convert.ToDateTime(datatable.Rows[j][1].ToString())) == 0)
                    //                {
                    //                    flag = 1;
                    //                    break;
                    //                }
                    //                else
                    //                {
                    //                    flag = 2;
                    //                }
                    //            }
                    //        }
                    //        if (flag == 2 || flag == 0)
                    //        {
                    //            arrLst.Add(dt);
                    //        }
                    //        //if (ds.Tables[0].Rows.Count > 0)
                            //{
                            //    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            //    {
                            //        DataTable dtble = new DataTable();
                            //        dtble = _bill_Sys_Visit_BO.FillProcedureCodeGrid(ds.Tables[0].Rows[j][0].ToString(), txtCompanyID.Text);
                            //        arrLst.Add(dtble);
                            //    }
                            //}
                        //}
                    //}
                    ////////Not required ....S.D
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
            ////////Not required ....S.D
            //for (int i = 0; i < arrLst.Count; i++)
            //{
            //    DataTable dt1 = (DataTable)arrLst[i];
            //    foreach (DataRow drow in dt1.Rows)
            //    {
            //        dr = datatable.NewRow();
            //        dr["SZ_BILL_TXN_DETAIL_ID"] = "";
            //        dr["DT_DATE_OF_SERVICE"] = drow.ItemArray.GetValue(5).ToString();
            //        dr["SZ_PROCEDURE_ID"] = drow.ItemArray.GetValue(0).ToString();
            //        dr["SZ_PROCEDURAL_CODE"] = drow.ItemArray.GetValue(2).ToString();
            //        dr["SZ_CODE_DESCRIPTION"] = drow.ItemArray.GetValue(3).ToString();
            //        dr["FACTOR_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
            //        dr["FACTOR"] = "1";
            //        dr["FLT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
            //        dr["I_UNIT"] = "1";
            //        dr["PROC_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
            //        dr["DOCT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
            //        dr["SZ_TYPE_CODE_ID"] = drow.ItemArray.GetValue(1).ToString();
            //        datatable.Rows.Add(dr);
            //    }
            //}
            

            //grdTransactionDetails.DataSource = datatable;
            //grdTransactionDetails.DataBind();
            ////////Not required ....S.D
            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[7].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[8].Text;
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

            //GetProcedureCode(hndDoctorID.Value.ToString());
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
            datatable.Columns.Add("FACTOR_AMOUNT");
            datatable.Columns.Add("FACTOR");
            datatable.Columns.Add("PROC_AMOUNT");
            datatable.Columns.Add("DOCT_AMOUNT");
            datatable.Columns.Add("I_UNIT");
            datatable.Columns.Add("SZ_TYPE_CODE_ID");
            datatable.Columns.Add("FLT_GROUP_AMOUNT");
            datatable.Columns.Add("I_GROUP_AMOUNT_ID");
            DataRow dr;

            //if (Request.QueryString["Type"] != null)
            //{
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
                        if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0) { dr["FACTOR_AMOUNT"] = ((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString(); }
                        if (Convert.ToDecimal(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0) { dr["FACTOR"] = ((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString(); }
                        dr["FLT_AMOUNT"] = j.Cells[6].Text.ToString();
                        if (((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString() != "") { if (Convert.ToInt32(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0) { dr["I_UNIT"] = ((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString(); } }
                        dr["PROC_AMOUNT"] = j.Cells[9].Text.ToString();
                        dr["DOCT_AMOUNT"] = j.Cells[10].Text.ToString();
                        dr["SZ_TYPE_CODE_ID"] = j.Cells[11].Text.ToString();
                        dr["FLT_GROUP_AMOUNT"] = j.Cells[12].Text.ToString();
                        dr["I_GROUP_AMOUNT_ID"] = j.Cells[13].Text.ToString();
                        datatable.Rows.Add(dr);
                    }

                }
            //}

            string Doctor_Id = "";
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
                    string visit_type = rw.Cells[2].Text;
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
                            //if (ds.Tables[0].Rows.Count > 0)
                            //{
                            //    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            //    {
                            //        DataTable dtble = new DataTable();
                            //        dtble = _bill_Sys_Visit_BO.FillProcedureCodeGrid(ds.Tables[0].Rows[j][0].ToString(), txtCompanyID.Text);
                            //        arrLst.Add(dtble);
                            //    }
                            //}
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
                    dr["FACTOR_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["FACTOR"] = "1";
                    dr["FLT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["I_UNIT"] = "1";
                    dr["PROC_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["DOCT_AMOUNT"] = drow.ItemArray.GetValue(4).ToString();
                    dr["SZ_TYPE_CODE_ID"] = drow.ItemArray.GetValue(1).ToString();
                    datatable.Rows.Add(dr);
                }
            }
            

            grdTransactionDetails.DataSource = datatable;
            grdTransactionDetails.DataBind();

            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            if (_bill_Sys_BillTransaction.checkUnit(hndDoctorID.Value.ToString(), txtCompanyID.Text))
            {
                grdTransactionDetails.Columns[7].Visible = true;
                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    TextBox txtTemp = (TextBox)grdTransactionDetails.Items[i].FindControl("txtUnit");
                    txtTemp.Text = grdTransactionDetails.Items[i].Cells[8].Text;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
                        dt.Columns.Add("FACTOR_AMOUNT");
                        dt.Columns.Add("FACTOR");
                        dt.Columns.Add("PROC_AMOUNT");
                        dt.Columns.Add("DOCT_AMOUNT");
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
                                if (j["DOCTOR_AMOUNT"].ToString() == "0")
                                {
                                    dr["FACTOR_AMOUNT"] = j["FLT_AMOUNT"];
                                    dr["FLT_AMOUNT"] = Convert.ToDecimal(j["FLT_AMOUNT"].ToString()) * Convert.ToDecimal(j["FLT_KOEL"].ToString());
                                }
                                else
                                {
                                    dr["FACTOR_AMOUNT"] = j["DOCTOR_AMOUNT"];
                                    dr["FLT_AMOUNT"] = Convert.ToDecimal(j["DOCTOR_AMOUNT"].ToString()) * Convert.ToDecimal(j["FLT_KOEL"].ToString());
                                }
                                dr["FACTOR"] = j["FLT_KOEL"];
                                dr["PROC_AMOUNT"] = j["FLT_AMOUNT"];
                                dr["DOCT_AMOUNT"] = j["DOCTOR_AMOUNT"];
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //public void getDoctorDefaultList()
    //{
    //    try
    //    {
    //        Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();

    //        DataSet dsDoctorName = _obj.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //        ddlDoctor.DataSource = dsDoctorName;

    //        ddlDoctor.DataTextField = "DESCRIPTION";
    //        ddlDoctor.DataValueField = "CODE";
    //        ddlDoctor.DataBind();
    //        ddlDoctor.Items.Insert(0, "---select---");
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}


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
            ds = null;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
}

