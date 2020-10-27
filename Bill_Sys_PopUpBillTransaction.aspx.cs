/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_BillTransaction.aspx.cs
/*Purpose              :       Popup page to add Bills.
/*Author               :       Sandeep Y
/*Date of creation     :       24 Nov 2009
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
using mbs.LienBills;



public partial class Bill_Sys_PopUpBillTransaction : PageBase
{
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
    private static ILog log = LogManager.GetLogger("Bill_Sys_PopUpBillTransaction");
    private Bill_Sys_LoginBO _bill_Sys_LoginBO;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (Request.QueryString["P_CASE_ID"] != null)
            {
                string sz_p_case_id = Request.QueryString["P_CASE_ID"].ToString();
                string sz_p_patient_id = Request.QueryString["P_PATIENT_ID"].ToString();
                string sz_p_case_no = Request.QueryString["P_CASE_NO"].ToString(); ;


                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = sz_p_case_id;
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = sz_p_patient_id;
                _bill_Sys_CaseObject.SZ_CASE_ID = sz_p_case_id;
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = sz_p_case_no;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["CASEINFO"] = _bill_Sys_Case;
                Session["PassedCaseID"] = sz_p_case_id;
                String szURL = "";
                String szCaseID = sz_p_case_id;
                Session["QStrCaseID"] = szCaseID;
                Session["Case_ID"] = szCaseID;
                Session["Archived"] = "0";
                Session["QStrCID"] = szCaseID;
                Session["SelectedID"] = szCaseID;
                Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["SN"] = "0";
                Session["LastAction"] = "vb_CaseInformation.aspx";
                szURL = "Document Manager/case/vb_CaseInformation.aspx";
            }


            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                Response.Redirect("Bill_Sys_ReferralBillTransaction.aspx?Type=Search", false);
            }
            btnSave.Attributes.Add("onclick", "return javascript:ConfirmClaimInsurance();");
            btnUpdate.Attributes.Add("onclick", "return javascript:validate();");
            if (!IsPostBack)
            {
                if (Request.QueryString["Type"] == null)
                {
                    Session["SZ_BILL_NUMBER"] = null;
                }
            }

            

            ErrorDiv.InnerText = "";
            ErrorDiv.Style.Value = "color: red";
            if (Session["CASE_OBJECT"] != null)
            {
                txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                txtCaseNo.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                //////////////////////
                //CREATE SESSION FOR DOC MANAGER,TEMPLATE MANAGER,Notes,Bills

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
                //
                ///////////////////
            }
            else
            {
                Response.Redirect("Bill_Sys_SearchCase.aspx", false);
            }

            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                extddlDoctor.Procedure_Name = "SP_MST_BILLING_DOCTOR";
                extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            else
            {
                extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            //btnAddService.Attributes.Add("onclick", "return Amountvalidate();");
            //btnUpdateService.Attributes.Add("onclick", "return Amountvalidate();");
            //btnAddService.Attributes.Add("onclick", "return formValidator('frmBillTrans','txtDateOfservice,txtDateOfServiceTo,extddlDoctor');");

            // imgbtnFromTo.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateOfServiceTo,'imgbtnOpenedDate','MM/dd/yyyy'); return false;");
            //  imgbtnDateofService.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateOfservice,'imgbtnOpenedDate','MM/dd/yyyy'); return false;");
            //  imgbtnOpenedDate.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtBillDate,'imgbtnOpenedDate','MM/dd/yyyy'); return false;");
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; ;
            btnSave.Attributes.Add("onclick", "return ConfirmClaimInsurance();");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmBillTrans','txtBillDate,extddlDoctor');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlDiagnosisType.Flag_ID = txtCompanyID.Text;
           
            if (!IsPostBack)
            {
                btnUpdate.Enabled = false;
                
                if (Session["SZ_BILL_NUMBER"] != null )
                {
                    txtBillID.Text = Session["SZ_BILL_NUMBER"].ToString();
                    BindTransactionData(txtBillID.Text);
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;

                    

                }
                txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                //Div1.Visible = true;
                Session["SELECTED_DIA_PRO_CODE"] = null;
                if (Request.QueryString["PopUp"] == null)
                {
                    
                }
                else
                {
                    if (Session["TEMP_DOCTOR_ID"] != null)

                    { extddlDoctor.Text = Session["TEMP_DOCTOR_ID"].ToString(); GetProcedureCode(extddlDoctor.Text); }
                    if (Session["SE_DIAGNOSIS_CODE"] != null)
                    {
                        DataTable dt = new DataTable();
                        if (lstDiagnosisCodes.Items.Count > 0)
                        {
                        }
                        else
                        {
                        }
                    }
                }

               // BindLatestTransaction();
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
                    objItem.Cells[12].Text = "";
                    objItem.Cells[13].Text = "";
                    objItem.Cells[14].Text = "";
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
            if (!Page.IsPostBack)
            {
                SetDoctor();
            }
            BindGrid();
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
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_PopUpBillTransaction.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void SetDoctor()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            extddlDoctor.Text = Request.QueryString["DID"].ToString();

            if (extddlDoctor.Text != "NA")
            {   
                GetProcedureCode(extddlDoctor.Text);
                Session["TEMP_DOCTOR_ID"] = extddlDoctor.Text;

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
                            dt.Rows.Add(dr);
                        }

                    }

                    grdTransactionDetails.DataSource = dt;
                    grdTransactionDetails.DataBind();

                    #endregion


                }

                #region "Disply Unit Column if speciality have unit bit set to true."

                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.Text, txtCompanyID.Text))
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
                lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text, extddlDoctor.Text, txtCompanyID.Text).Tables[0];
                lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                lstDiagnosisCodes.DataValueField = "CODE";
                lstDiagnosisCodes.DataBind();
                lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
                #endregion
                // -------------------------------------



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
                extddlDoctor.Text = objAL[0].ToString();
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

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        Session["SZ_BILL_ID"] = null;
        Session["PassedCaseID"] = null;

    }

    protected void getDefaultAssociatedDiagCode()
    { 

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (extddlDoctor.Text == "NA")
        {
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
              //  BindLatestTransaction();
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
            
        }
        else
        {
            ErrorDiv.InnerText = " Select Doctor ...!";
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //private void BindLatestTransaction()
    //{
    //    _listOperation = new ListOperation();
    //    try
    //    {
    //        _listOperation.WebPage = this.Page;
    //        _listOperation.Xml_File = "LatestBillTransaction.xml";
    //        _listOperation.LoadList();
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

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
            extddlDoctor.Text = "NA";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;

            grdTransactionDetails.Visible = false;
            lblMsg.Visible = false;
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

    protected void grdLatestBillTransaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            // lnlInitialReport.Visible = true;
            // lnkCopyOldrogressReport.Visible = true;
            // lnlProgessReport.Visible = true;
            // lnkReportOfMMI.Visible = true;
            grdAllReports.Visible = false;
            Session["BillID"] = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[1].Text;
            Session["SZ_BILL_NUMBER"] = Session["BillID"].ToString();
            //  txtBillNo.Text = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[1].Text;
            //if (grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[3].Text != "&nbsp;") { txtBillDate.Text = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[3].Text; }
            txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            if (grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[11].Text != "&nbsp;") { extddlDoctor.Text = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[11].Text; }
            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO.GetBillDiagnosisCode(Session["BillID"].ToString()).Tables[0];
            lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            lstDiagnosisCodes.DataValueField = "CODE";
            lstDiagnosisCodes.DataBind();
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
         //   Session["SE_DIAGNOSIS_CODE"] = _bill_Sys_AssociateDiagnosisCodeBO.GetBillDiagnosisCode(Session["BillID"].ToString()).Tables[0];
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;

            //btnAddService.Enabled = true;
            //btnFromDate.Visible = false;
            grdTransactionDetails.Visible = true;
            BindTransactionDetailsGrid(Session["BillID"].ToString());
            // BindIC9Grid(Session["BillID"].ToString());



            //ClearControl();
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
                    szBillName =  szTemp[szTemp.Length - 1].ToString();
                    
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
                    objAL.Add(szTemp[szTemp.Length-1].ToString());
                    objAL.Add(szDestinationDir);
                    objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                    objAL.Add(szSpecility);
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

                 //   BindLatestTransaction();


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
                        szCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
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
                    Bill_Sys_PVT_Template _objPvtBill;
                    _objPvtBill = new Bill_Sys_PVT_Template();
                    bool isReferingFacility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                    string szCompanyId;
                    string szCaseId = Session["TM_SZ_CASE_ID"].ToString();
                    string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    // string szSpecility ;
                    string szCompanyName;
                    string szBillId = Session["TM_SZ_BILL_ID"].ToString();
                   
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                    {
                        Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                        szCompanyName = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        szCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                        szCompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        szCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    mbs.LienBills.Lien obj = new Lien();
                    string path = obj.GenratePdfForLien(szCompanyId, szBillId, szSpecility, szCaseId, szUserName, txtCaseNo.Text, szUserId);
                   // ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + path + "');", true);
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + path + "'); ", true);

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
                }


            }
            if (e.CommandName.ToString() == "Doctor's Initial Report")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformation.aspx'); ", true);
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
                if (e.Item.Cells[10].Text != "1")
                {
                    Session["PassedBillID"] = e.CommandArgument;
                    Session["Balance"] = e.Item.Cells[8].Text;
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

    protected void grdLatestBillTransaction_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            //if (e.Item.Cells[5].Text == "" || e.Item.Cells[5].Text == "&nbsp;" || e.Item.Cells[5].Text == "0.00")
            //{
            //    e.Item.Cells[7].Text = "";
            //}
            if (e.Item.Cells[10].Text == "1")
            {
                e.Item.Cells[9].Text = "";
            }
            objCaseDetailsBO = new CaseDetailsBO();
            if (objCaseDetailsBO.GetCaseType(e.Item.Cells[1].Text) != "WC000000000000000001")
            {
                e.Item.Cells[12].Text = "";
                e.Item.Cells[13].Text = "";
                e.Item.Cells[14].Text = "";
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

    //protected void extddlIC9Code_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    _bill_Sys_Menu = new Bill_Sys_Menu();
    //    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //    try
    //    {


    //        btnSave.Attributes.Add("onclick", "return Amountvalidate();");
    //        lblMsg.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

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
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            grdTransactionDetails.DataSource = _bill_Sys_BillTransaction.BindTransactionData(billnumber); //BindTransactionData
            grdTransactionDetails.DataBind();


            #region "Disply Unit Column if speciality have unit bit set to true."

            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.Text, txtCompanyID.Text))
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
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
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

            //txtDateOfservice.Text = "";
            //txtDateOfServiceTo.Text = "";

           // btnAddService.Enabled = true;
            //btnFromDate.Visible = true;
            extddlDoctor.Text = "NA";
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

    //protected void btnOn_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        txtDateOfServiceTo.Visible = false;
    //        lblDateOfService.Visible = false;
    //        lblTo.Visible = false;
    //        imgbtnFromTo.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    //protected void btnFromTo_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        txtDateOfServiceTo.Visible = true;
    //        lblDateOfService.Visible = true;
    //        lblTo.Visible = true;
    //        imgbtnFromTo.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

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

            //string _casetype = "";
            //_bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            //_casetype=_bill_Sys_BillTransaction.GetCaseType(Session["PassedCaseID"].ToString());


            for (int i = 0; i <= dataset.Tables[0].Rows.Count - 1; i++)
            {
                extddlDoctor.Text = dataset.Tables[0].Rows[i][1].ToString();



            }


            //  BindTransactionDetailsGrid(txtBillNo.Text);
            grdTransactionDetails.Visible = true;
            extddlDoctor.Enabled = false;

            //btnAddService.Enabled = false;

            //btnClearService.Enabled = false;
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

    protected void lnlInitialReport_Click(object sender, EventArgs e)
    {
        Session["TEMPLATE_BILL_NO"] = Session["BillID"].ToString();
        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformation.aspx'); ", true);
    }

    #region "Save Bill Information"
    //protected void btnSave_Click1(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (extddlDoctor.Text != "NA")
    //        {
    //            _saveOperation = new SaveOperation();
    //            _saveOperation.WebPage = this.Page;
    //            _saveOperation.Xml_File = "BillTransaction.xml";
    //            _saveOperation.SaveMethod();


                
    //            _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
    //            string billno = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());
    //            txtBillID.Text = billno;

    //             // Start : Update Visit Status.
    //       //     if (grdAllReports.Visible==true)
    //            {

    //                Bill_Sys_Calender _bill_Sys_Visit_BO = new Bill_Sys_Calender();
    //                ArrayList objList;
    //                foreach (DataGridItem dgItem in grdAllReports.Items)
    //                {
    //                    if (((CheckBox)dgItem.Cells[0].FindControl("chkselect")).Checked == true)
    //                    {
    //                        objList = new ArrayList();
    //                        objList.Add(Convert.ToInt32(dgItem.Cells[4].Text));
    //                        objList.Add("1");
    //                        objList.Add("2");
    //                        objList.Add(txtBillID.Text);
    //                        objList.Add(txtBillDate.Text);
    //                        _bill_Sys_Visit_BO.UPDATE_Event_Status(objList);
    //                        foreach (DataGridItem j in grdTransactionDetails.Items)
    //                        {
    //                            if (dgItem.Cells[2].Text == j.Cells[1].Text)
    //                            {
    //                                objList = new ArrayList();
    //                                objList.Add(j.Cells[12].Text);
    //                                objList.Add(Convert.ToInt32(dgItem.Cells[4].Text));
    //                                objList.Add("2");
    //                                _bill_Sys_Visit_BO.Save_Event_RefferPrcedure(objList);
    //                            }
    //                        }
    //                    }
    //                }
               
    //            }
    //            // End : Update Visit Status.

    //            // Start : Save Notes for Bill.

    //            _DAO_NOTES_EO = new DAO_NOTES_EO();
    //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
    //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

    //            _DAO_NOTES_BO = new DAO_NOTES_BO();
    //            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
    //            _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
    //            _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
    //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

    //            // End 

    //            objCaseDetailsBO = new CaseDetailsBO();
    //            String patientID = objCaseDetailsBO.GetPatientID(billno);
    //            if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000001")
    //            {
    //                //string patientID = Workers Compensation
    //                objDefaultValue = new Bill_Sys_InsertDefaultValues();
    //                if (grdLatestBillTransaction.Items.Count == 0)
    //                {

    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_DoctorOpinion.xml"), txtCompanyID.Text.ToString(), null, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_ExamInformation.xml"), txtCompanyID.Text.ToString(), null, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_History.xml"), txtCompanyID.Text.ToString(), null, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_PlanOfCare.xml"), txtCompanyID.Text.ToString(), null, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_WorkStatus.xml"), txtCompanyID.Text.ToString(), null, patientID);

    //                }
    //                else if (grdLatestBillTransaction.Items.Count >= 1)
    //                {

    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_DoctorsOpinionC4_2.xml"), txtCompanyID.Text.ToString(), billno, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_ExaminationTreatment.xml"), txtCompanyID.Text.ToString(), billno, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_PermanentImpairment.xml"), txtCompanyID.Text.ToString(), billno, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_WorkStatusC4_2.xml"), txtCompanyID.Text.ToString(), billno, patientID);
    //                }
    //            }
    //            // btnAddService.Enabled = true;

    //            ArrayList _arrayList;
    //            foreach (DataGridItem j in grdTransactionDetails.Items)
    //            {
    //                if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
    //                {
    //                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                    _arrayList = new ArrayList();
    //                    // _arrayList.Add(j.Cells[2].Text.ToString());
    //                    _arrayList.Add(j.Cells[2].Text.ToString());
    //                    if (j.Cells[6].Text.ToString() != "&nbsp;") { _arrayList.Add(j.Cells[6].Text.ToString()); } else { _arrayList.Add(0); }
    //                    _arrayList.Add(billno);
    //                    _arrayList.Add(Convert.ToDateTime(j.Cells[1].Text.ToString()));
    //                    _arrayList.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                    _arrayList.Add(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString());
    //                    _arrayList.Add(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString());
    //                    _arrayList.Add(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString());
    //                    _arrayList.Add(j.Cells[9].Text.ToString());
    //                    if (j.Cells[8].Text.ToString() != "&nbsp;" && j.Cells[8].Text.ToString() != "") { _arrayList.Add(j.Cells[8].Text.ToString()); } else { _arrayList.Add(0); }
    //                    _arrayList.Add(extddlDoctor.Text);
    //                    _arrayList.Add(txtCaseID.Text);
    //                    _arrayList.Add(j.Cells[12].Text.ToString());
    //                    _bill_Sys_BillTransaction.SaveTransactionData(_arrayList);
    //                }
    //            }
    //            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //            _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
    //            foreach (ListItem lstItem in lstDiagnosisCodes.Items)
    //            {
    //                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                _arrayList = new ArrayList();
    //                _arrayList.Add(lstItem.Value.ToString());
    //                _arrayList.Add(billno);
    //                _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arrayList);
    //            }

    //         //   BindLatestTransaction();
    //            ClearControl();
    //            lblMsg.Visible = true;
    //            //btnSave.Enabled = false;
    //            lblMsg.Text = " Bill Saved successfully ! ";
    //            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //         //   GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno,((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));

    //            if (Request.QueryString["F"].ToString() == "L")
    //            {
    //                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden'; var url='Bill_Sys_ThirtyDaysUnbilledVisits.aspx'; window.parent.document.location.href=url; parent.document.getElementById('lblMsg').visible=true;parent.document.getElementById('lblMsg').value='Bill added successfully.'</script>");
    //            }
    //            else
    //            {
    //                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden'; var url='Bill_Sys_UnbilledVisits.aspx'; window.parent.document.location.href=url;parent.document.getElementById('lblMsg').visible=true;parent.document.getElementById('lblMsg').value='Bill added successfully.' </script>");
    //            }
    //        }
    //        else
    //        {
    //            lblMsg.Visible = true;
    //            lblMsg.Text = " Select doctor ... ! ";
    //        }

    //    }
    //    catch { }

    //}

    #region "Save Bill Information"
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string billno = "";
            if (extddlDoctor.Text != "NA")
            {

                #region "Save information into BillTransactionEO"

                BillTransactionEO _objBillEO = new BillTransactionEO();
                _objBillEO.SZ_CASE_ID = txtCaseID.Text;
                _objBillEO.SZ_COMPANY_ID = txtCompanyID.Text;
                _objBillEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);
                _objBillEO.SZ_DOCTOR_ID = extddlDoctor.Text;
                _objBillEO.SZ_TYPE = ddlType.Text;
                _objBillEO.SZ_TESTTYPE = ""; //ddlTestType.Text;
                _objBillEO.FLAG = "ADD";
                #endregion



                _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();

                ArrayList objALEventEO = new ArrayList();
                ArrayList objALEventRefferProcedureEO = new ArrayList();


                // Start : Update Visit Status.
                //if (grdAllReports.Visible == true)
                //{

                    Bill_Sys_Calender _bill_Sys_Visit_BO = new Bill_Sys_Calender();
                    ArrayList objList;

                    foreach (DataGridItem dgItem in grdAllReports.Items)
                    {
                        if (((CheckBox)dgItem.Cells[0].FindControl("chkselect")).Checked == true)
                        {
                            EventEO _objEventEO = new EventEO();
                            _objEventEO.I_EVENT_ID = dgItem.Cells[4].Text;
                            _objEventEO.BT_STATUS = "1";
                            _objEventEO.I_STATUS = "2";
                            _objEventEO.SZ_BILL_NUMBER = "";
                            _objEventEO.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);
                            objALEventEO.Add(_objEventEO);
                            foreach (DataGridItem j in grdTransactionDetails.Items)
                            {
                                if (dgItem.Cells[2].Text == j.Cells[1].Text)
                                {
                                    EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                                    objEventRefferProcedureEO.SZ_PROC_CODE = j.Cells[11].Text;
                                    objEventRefferProcedureEO.I_EVENT_ID = dgItem.Cells[4].Text;
                                    objEventRefferProcedureEO.I_STATUS = "2";
                                    objALEventRefferProcedureEO.Add(objEventRefferProcedureEO);
                                }
                            }
                        }
                    }

                //}
                // End : Update Visit Status.



                objCaseDetailsBO = new CaseDetailsBO();


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

                        objBillProcedureCodeEO.SZ_DOCTOR_ID = extddlDoctor.Text;

                        objBillProcedureCodeEO.SZ_CASE_ID = txtCaseID.Text;

                        objBillProcedureCodeEO.SZ_TYPE_CODE_ID = j.Cells[11].Text.ToString();

                        objBillProcedureCodeEO.FLT_GROUP_AMOUNT = j.Cells[12].Text.ToString();

                        objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = j.Cells[13].Text.ToString();


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
                    else if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000004")
                    {
                        GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                    } if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000003")
                    {
                        GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                    } 
                }
                if (Request.QueryString["F"].ToString() == "L")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden'; var url='Bill_Sys_ThirtyDaysUnbilledVisits.aspx'; window.parent.document.location.href=url; parent.document.getElementById('lblMsg').value='Bill added successfully.'</script>");
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden'; var url='Bill_Sys_UnbilledVisits.aspx'; window.parent.document.location.href=url;parent.document.getElementById('lblMsg').value='Bill added successfully.' </script>");
                }
                Session["SELECTED_PROC_CODE"] = Request.QueryString["PROC_GROUP_ID"].ToString();
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = " Select doctor ... ! ";
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



    //protected void btnSave_Click1(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (extddlDoctor.Text != "NA")
    //        {
    //            _saveOperation = new SaveOperation();
    //            _saveOperation.WebPage = this.Page;
    //            _saveOperation.Xml_File = "BillTransaction.xml";
    //            _saveOperation.SaveMethod();



    //            _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
    //            string billno = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());
    //            txtBillID.Text = billno;

    //            // Start : Update Visit Status.
    //         //   if (grdAllReports.Visible == true)
    //            {

    //                Bill_Sys_Calender _bill_Sys_Visit_BO = new Bill_Sys_Calender();
    //                ArrayList objList;
    //                foreach (DataGridItem dgItem in grdAllReports.Items)
    //                {
    //                    if (((CheckBox)dgItem.Cells[0].FindControl("chkselect")).Checked == true)
    //                    {
    //                        objList = new ArrayList();
    //                        objList.Add(Convert.ToInt32(dgItem.Cells[4].Text));
    //                        objList.Add("1");
    //                        objList.Add("2");
    //                        objList.Add(txtBillID.Text);
    //                        objList.Add(txtBillDate.Text);
    //                        _bill_Sys_Visit_BO.UPDATE_Event_Status(objList);
    //                        foreach (DataGridItem j in grdTransactionDetails.Items)
    //                        {
    //                            if (dgItem.Cells[2].Text == j.Cells[1].Text)
    //                            {
    //                                objList = new ArrayList();
    //                                objList.Add(j.Cells[12].Text);
    //                                objList.Add(Convert.ToInt32(dgItem.Cells[4].Text));
    //                                objList.Add("2");
    //                                _bill_Sys_Visit_BO.Save_Event_RefferPrcedure(objList);
    //                            }
    //                        }
    //                    }
    //                }

    //            }
    //            // End : Update Visit Status.

    //            // Start : Save Notes for Bill.

    //            _DAO_NOTES_EO = new DAO_NOTES_EO();
    //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
    //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

    //            _DAO_NOTES_BO = new DAO_NOTES_BO();
    //            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
    //            _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
    //            _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
    //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

    //            // End 

    //            objCaseDetailsBO = new CaseDetailsBO();
    //            String patientID = objCaseDetailsBO.GetPatientID(billno);
    //            if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000001")
    //            {
    //                //string patientID = Workers Compensation
    //                objDefaultValue = new Bill_Sys_InsertDefaultValues();
    //                if (grdLatestBillTransaction.Items.Count == 0)
    //                {

    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_DoctorOpinion.xml"), txtCompanyID.Text.ToString(), null, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_ExamInformation.xml"), txtCompanyID.Text.ToString(), null, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_History.xml"), txtCompanyID.Text.ToString(), null, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_PlanOfCare.xml"), txtCompanyID.Text.ToString(), null, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_WorkStatus.xml"), txtCompanyID.Text.ToString(), null, patientID);

    //                }
    //                else if (grdLatestBillTransaction.Items.Count >= 1)
    //                {

    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_DoctorsOpinionC4_2.xml"), txtCompanyID.Text.ToString(), billno, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_ExaminationTreatment.xml"), txtCompanyID.Text.ToString(), billno, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_PermanentImpairment.xml"), txtCompanyID.Text.ToString(), billno, patientID);
    //                    objDefaultValue.InsertDefaultValues(Server.MapPath("Config\\DV_WorkStatusC4_2.xml"), txtCompanyID.Text.ToString(), billno, patientID);
    //                }
    //            }
    //            // btnAddService.Enabled = true;

    //            ArrayList _arrayList;
    //            foreach (DataGridItem j in grdTransactionDetails.Items)
    //            {
    //                if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
    //                {
    //                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                    _arrayList = new ArrayList();
    //                    // _arrayList.Add(j.Cells[2].Text.ToString());
    //                    _arrayList.Add(j.Cells[2].Text.ToString());
    //                    if (j.Cells[6].Text.ToString() != "&nbsp;") { _arrayList.Add(j.Cells[6].Text.ToString()); } else { _arrayList.Add(0); }
    //                    _arrayList.Add(billno);
    //                    _arrayList.Add(Convert.ToDateTime(j.Cells[1].Text.ToString()));
    //                    _arrayList.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                    _arrayList.Add(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString());
    //                    _arrayList.Add(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString());
    //                    _arrayList.Add(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString());
    //                    _arrayList.Add(j.Cells[9].Text.ToString());
    //                    if (j.Cells[8].Text.ToString() != "&nbsp;" && j.Cells[8].Text.ToString() != "") { _arrayList.Add(j.Cells[8].Text.ToString()); } else { _arrayList.Add(0); }
    //                    _arrayList.Add(extddlDoctor.Text);
    //                    _arrayList.Add(txtCaseID.Text);
    //                    _arrayList.Add(j.Cells[11].Text.ToString());
    //                    _arrayList.Add(j.Cells[12].Text.ToString());
    //                    _arrayList.Add(j.Cells[13].Text.ToString());
    //                    _bill_Sys_BillTransaction.SaveTransactionData(_arrayList);
    //                }
    //            }
    //            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //            _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
    //            foreach (ListItem lstItem in lstDiagnosisCodes.Items)
    //            {
    //                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                _arrayList = new ArrayList();
    //                _arrayList.Add(lstItem.Value.ToString());
    //                _arrayList.Add(billno);
    //                _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arrayList);
    //            }

    //         //   BindLatestTransaction();
    //            ClearControl();
    //            lblMsg.Visible = true;
    //            //btnSave.Enabled = false;
    //            lblMsg.Text = " Bill Saved successfully ! ";
    //            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //            GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
    //            if (Request.QueryString["F"].ToString() == "L")
    //            {
    //                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden'; var url='Bill_Sys_ThirtyDaysUnbilledVisits.aspx'; window.parent.document.location.href=url; parent.document.getElementById('lblMsg').value='Bill added successfully.'</script>");
    //            }
    //            else
    //            {
    //                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden'; var url='Bill_Sys_UnbilledVisits.aspx'; window.parent.document.location.href=url;parent.document.getElementById('lblMsg').value='Bill added successfully.' </script>");
    //            }
    //        }
    //        else
    //        {
    //            lblMsg.Visible = true;
    //            lblMsg.Text = " Select doctor ... ! ";
    //        }

    //    }
    //    catch { }

    //}

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
            if (extddlDoctor.Text != "NA")
            {
                Session["BillID"] = Session["SZ_BILL_NUMBER"].ToString();
                if (Session["BillID"] != null)
                {
                    // string str = txtBillNo.Text;
                    _editOperation.Primary_Value = Session["BillID"].ToString();
                    _editOperation.WebPage = this.Page;
                    _editOperation.Xml_File = "BillTransaction.xml";
                    _editOperation.UpdateMethod();

                    _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                    string billno = Session["BillID"].ToString();
                    //btnAddService.Enabled = true;
                    ArrayList _arrayList;
                    foreach (DataGridItem j in grdTransactionDetails.Items)
                    {
                        if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;" && j.Cells[3].Text.ToString() != "" && j.Cells[3].Text.ToString() != "&nbsp;" && j.Cells[4].Text.ToString() != "" && j.Cells[4].Text.ToString() != "&nbsp;")
                        {
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            _arrayList = new ArrayList();
                            if (j.Cells[0].Text.ToString() != "" && j.Cells[0].Text.ToString() != "&nbsp;")
                            { _arrayList.Add(j.Cells[0].Text.ToString()); }
                            _arrayList.Add(j.Cells[2].Text.ToString());
                            if (j.Cells[6].Text.ToString() != "&nbsp;") { _arrayList.Add(j.Cells[6].Text.ToString()); } else { _arrayList.Add(0); }
                            _arrayList.Add(billno);
                            if (j.Cells[1].Text.ToString() != "" && j.Cells[1].Text.ToString() != "&nbsp;")
                            { _arrayList.Add(Convert.ToDateTime(j.Cells[1].Text.ToString())); }
                            else
                            { _arrayList.Add(""); }
                            _arrayList.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            _arrayList.Add(((TextBox)j.Cells[7].FindControl("txtUnit")).Text.ToString());
                            _arrayList.Add(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString());
                            _arrayList.Add(((Label)j.Cells[5].FindControl("lblPrice")).Text.ToString());
                            _arrayList.Add(((Label)j.Cells[5].FindControl("lblFactor")).Text.ToString());
                         //   _arrayList.Add(j.Cells[9].Text.ToString());
                            _arrayList.Add(j.Cells[8].Text.ToString());

                            if (j.Cells[0].Text.ToString() != "" && j.Cells[0].Text.ToString() != "&nbsp;")
                            {
                                _arrayList.Add(j.Cells[12].Text.ToString());
                                _bill_Sys_BillTransaction.UpdateTransactionData(_arrayList);
                            }
                            else
                            {
                                _arrayList.Add(extddlDoctor.Text);
                                _arrayList.Add(txtCaseID.Text);
                                _arrayList.Add(j.Cells[12].Text.ToString());
                                _bill_Sys_BillTransaction.SaveTransactionData(_arrayList);
                            }
                        }
                    }

                    ///

                    //

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

                    lblMsg.Visible = true;


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
                BindTransactionDetailsGrid(Session["BillID"].ToString());
             //   BindLatestTransaction();
                //BindGrid();

            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = " Select doctor ... ! ";
            }
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
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

        try
        {
            if (grdTransactionDetails.Items.Count > 0)
            {

                for (int i = 0; i < grdTransactionDetails.Items.Count; i++)
                {
                    if (grdTransactionDetails.Items[i].Cells[0].Text != "" && grdTransactionDetails.Items[i].Cells[0].Text != "&nbsp;")
                    {
                        CheckBox chkRemove = (CheckBox)grdTransactionDetails.Items[i].FindControl("chkSelect");
                        if (chkRemove.Checked)
                        {
                            _bill_Sys_BillTransaction.DeleteTransactionDetails(grdTransactionDetails.Items[i].Cells[0].Text);
                             grdTransactionDetails.Items[i].Cells.Clear();
                           
                        }
                        //                        BindTransactionDetailsGrid(Session["BillID"].ToString());
                    }
                    else
                    {
                        CheckBox chkRemove = (CheckBox)grdTransactionDetails.Items[i].FindControl("chkSelect");
                        if (chkRemove.Checked)
                        {
                            grdTransactionDetails.Items[i].Cells.Clear();
                        }
                    }

                }
            }
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
                if (dtProcCode.Rows.Count>0)
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


                    //DateTime endDate;
                    //DateTime startDate = Convert.ToDateTime(txtDateOfservice.Text.ToString());
                    //if (txtDateOfServiceTo.Text != "" && txtDateOfServiceTo.Visible==true) { endDate = Convert.ToDateTime(txtDateOfServiceTo.Text); }
                    //else { endDate = Convert.ToDateTime(txtDateOfservice.Text.ToString()); }
                    //TimeSpan ts = endDate.Subtract(startDate);
                    //DateTime currentDate = startDate;

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
                        //dataRowServices = _bill_Sys_BillTransaction.GeSelectedDoctorProcedureCode_List(szprocedureCode, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extddlDoctor.Text, ddlType.SelectedValue).Tables[0];
                        dataRowServices = _bill_Sys_BillTransaction.GeSelectedDoctorProcedureCode_List(szprocedureCode, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, doctorId, szTypeCode).Tables[0];

                        //if (szprocedureCode == "")
                        //{ szprocedureCode = szprocedureCode + "'" + lstItem.Value.ToString() + "'"; }
                        //else
                        //{ szprocedureCode = szprocedureCode + ",'" + lstItem.Value.ToString() + "'"; }
                        //stat = true;

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
                            //dr["FACTOR_AMOUNT"] = 
                            dr["FACTOR"] = j["FLT_KOEL"];
                            dr["PROC_AMOUNT"] = j["FLT_AMOUNT"];
                            dr["DOCT_AMOUNT"] = j["DOCTOR_AMOUNT"];
                            dr["I_UNIT"] = "";
                            dr["SZ_TYPE_CODE_ID"] = szTypeCode;
                            dt.Rows.Add(dr);
                        }

                    }





                    //for (int i = 0; i <= ts.Days; i++)
                    //{

                    //currentDate = currentDate.AddDays(1);
                    //}

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

    #region "extddlDoctor Change event"
    protected void extddlDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (extddlDoctor.Text != "NA")
        {
            GetProcedureCode(extddlDoctor.Text);
            Session["TEMP_DOCTOR_ID"] = extddlDoctor.Text;

            _bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            string keyValue=_bill_Sys_LoginBO.getDefaultSettings(txtCompanyID.Text, "SS00005");
            if (keyValue=="1")
            {
                // Bind Visit Grid --------------------------
                BindGrid();
                lblDateOfService.Style.Add("visibility","hidden");//.Visible=false;
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
                        dt.Rows.Add(dr);
                    }

                }

                grdTransactionDetails.DataSource = dt;
                grdTransactionDetails.DataBind();

                #endregion

                
            }

            #region "Disply Unit Column if speciality have unit bit set to true."

            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.Text, txtCompanyID.Text))
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
            lstDiagnosisCodes.DataSource = _bill_Sys_AssociateDiagnosisCodeBO1.GetCaseDiagnosisCode(txtCaseID.Text,extddlDoctor.Text,txtCompanyID.Text).Tables[0];
            lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            lstDiagnosisCodes.DataValueField = "CODE";
            lstDiagnosisCodes.DataBind();
            lblDiagnosisCodeCount.Text = lstDiagnosisCodes.Items.Count.ToString();
            #endregion
            // -------------------------------------


           
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
            objAL.Add(extddlDoctor.Text);
            objAL.Add("");
            objAL.Add("");
            objAL.Add("1");
            objAL.Add(txtCaseID.Text);
            objAL.Add("");
            grdAllReports.DataSource = _bill_Sys_Visit_BO.VisitReport(objAL);            
            grdAllReports.DataBind();
           // grdAllReports.Visible = true;
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
            string keyValue=_bill_Sys_LoginBO.getDefaultSettings(txtCompanyID.Text, "SS00005");
            if (keyValue == "1")
            {
                

                foreach (DataGridItem dgItem in grdAllReports.Items)
                {
                    if (((CheckBox)dgItem.Cells[0].FindControl("chkselect")).Checked==true)
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
            }
            else
            {

                foreach (Object dtServiceDate in dateOfService)
                {
                    if (dtServiceDate.ToString() != "")
                    {
                        foreach (DataGridItem j in grdProcedure.Items)
                        {
                            CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                            if (chkSelect.Checked)
                            {
                                dr = dt.NewRow();
                                dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                                dr["DT_DATE_OF_SERVICE"] = dtServiceDate.ToString();//txtDateOfservice.Text;
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

            grdTransactionDetails.DataSource = dt;
            grdTransactionDetails.DataBind();

            #region "Disply Unit Column if speciality have unit bit set to true."

            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.Text, txtCompanyID.Text))
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
            }
            else
            {
                foreach (Object dtServiceDate in dateOfService)
                {
                    if (dtServiceDate.ToString() != "")
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
                                    dr["DT_DATE_OF_SERVICE"] = dtServiceDate.ToString();//txtGroupDateofService.Text;
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
            grdTransactionDetails.DataSource = dt;
            grdTransactionDetails.DataBind();

            #region "Disply Unit Column if speciality have unit bit set to true."

            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (_bill_Sys_BillTransaction.checkUnit(extddlDoctor.Text, txtCompanyID.Text))
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

    protected void lnkbtnRemoveDiag_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i < lstDiagnosisCodes.Items.Count; i++)
            {
                if (lstDiagnosisCodes.Items[i].Selected == true)
                {
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


    #region "Code For Diagnosis Code pop up."

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
        _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdDiagonosisCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid);
            grdDiagonosisCode.DataBind();

        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }

    private void BindDiagnosisGrid(string typeid, string code, string description)
    {
        _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdDiagonosisCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid, code, description);
            grdDiagonosisCode.DataBind();

        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }

    protected void grdDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
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
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
   
    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        try
        {
            BindDiagnosisGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "<script>javascript:showDiagnosisCodePopup();</script>", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }

    #endregion


    private void GenerateAddedBillPDF(string p_szBillNumber , string p_szSpeciality)
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
                    szBillName =  szTemp[szTemp.Length - 1].ToString();
                    
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
                    objAL.Add(szTemp[szTemp.Length-1].ToString());
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

                  //  BindLatestTransaction();


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
                string sz_openpath=    _objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyId, szCaseId, szSpecility, szCompanyName, szBillId, szUserName, szUserId);
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + sz_openpath + "'); ", true);

            }
            else if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
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
                mbs.LienBills.Lien obj = new Lien();
                string path = obj.GenratePdfForLien(szCompanyId, szBillId, szSpecility, szCaseId, szUserName, txtCaseNo.Text, szUserId);
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + path + "'); ", true);
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + path + "');", true);

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
    protected void grdAllReports_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String EventID = Request.QueryString["EID"].ToString();
        try
        {
            if (e.Item.Cells[4].Text == EventID)
            {
                CheckBox chk = (CheckBox)e.Item.FindControl("chkSelect");
                chk.Checked = true;
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


    protected void Button2_Click(object sender, EventArgs e)
    {
        
    }
    
}

