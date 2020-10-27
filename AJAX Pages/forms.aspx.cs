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
using System.Collections.Generic;

using DevExpress.Web;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class forms : PageBase
{
    string sz_Node_ID, sz_NodeName, sz_CompanyID, sz_CaseID, Logicalpath, sz_UserID, sz_doctorID, sz_Caseno, sz_speciality = "";

    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (!Page.IsPostBack)
        {
            try
            {
                btnClear.Attributes.Add("OnClick", "return Clear()");
                btnClearBottom.Attributes.Add("OnClick", "return Clear()");

                //bindXml();

                extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();

                DataSet dsDoctorName = _obj.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                DDLAttendingDoctors.DataSource = dsDoctorName;
                ListItem objLI = new ListItem("---select---", "NA");
                DDLAttendingDoctors.DataTextField = "DESCRIPTION";
                DDLAttendingDoctors.DataValueField = "CODE";
                DDLAttendingDoctors.DataBind();

                LoadData();
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

    

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        SaveData();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //if (TxtInsuranceNameAdd.Text.Trim() != "" || TxtInsuranceNameAdd.Text.Trim() != ",")
        //{

        //    try
        //    {
        //        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        //        sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        //        MBS_CaseMG2 oj = new MBS_CaseMG2();
        //        string flg = oj.MG2CheckExist(sz_CompanyID, sz_CaseID);
        //        if (flg == "1")
        //            PrintPDF();
        //        else
        //        {
        //            usrMessage.PutMessage("Please Save Record First.");
        //            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
        //            usrMessage.Show();
        //        }

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
    }

    private void PrintPDF()
    {
        //try
        //{
        //    sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        //    sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        //    sz_speciality = hdSpeciality.Value.ToString();

        //    MBS_CaseMG2 oj = new MBS_CaseMG2();
        //    string CmpName = oj.GetPDFPath(sz_CompanyID, sz_CaseID);
        //    hdCmpName.Value = CmpName;

        //    GenerateCaseMG2Pdf obj = new GenerateCaseMG2Pdf();

        //    Logicalpath = "MG2_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff");
        //    hdLogicalpath.Value = Logicalpath;

        //    string tempPath = obj.generateMG2(sz_CompanyID, sz_CaseID, CmpName, Logicalpath, hdID.Value.ToString());

        //    SaveLogicalPath();
        //    string BillPath = CmpName + "\\" + Logicalpath + ".pdf";
        //    string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
        //    string newpath = str + "/" + BillPath;
        //    newpath = newpath.Replace("\\", "/");
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + newpath + "');", true);
        //}
        //catch (Exception ex)
        //{
        //    //usrMessage.PutMessage(ex.ToString());
        //    //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
        //    //usrMessage.Show();
        //}
        //finally
        //{

        //}
    }

    private void SaveLogicalPath()
    {
        //FindNodeType();

        //try
        //{
        //    //string strGenFileName = sz_BillNo + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff")+"MG2.pdf";

        //    sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        //    sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();

        //    string BillPath = hdCmpName.Value.ToString() + "\\" + hdLogicalpath.Value.ToString() + ".pdf";


        //    string sz_UserName = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_NAME.ToString();
        //    sz_UserID = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();

        //    MBS_CaseMG2 oj = new MBS_CaseMG2();
        //    oj.SaveLogicalPath(hdID.Value.ToString(), sz_CompanyID, sz_CaseID, BillPath);

        //    oj.SaveUploadDocumentMG2(sz_CaseID, sz_CompanyID, hdLogicalpath.Value.ToString() + ".pdf", BillPath, hdNodeName.Value.ToString(), sz_UserName, sz_UserID, hdNodeID.Value.ToString());


        //}
        //catch (Exception ex)
        //{
        //    //usrMessage.PutMessage(ex.ToString());
        //    //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
        //    //usrMessage.Show();
        //}
        //finally
        //{

        //}
    }

    private void FindNodeType()
    {
        //try
        //{
        //    MBS_CaseMG2 oj = new MBS_CaseMG2();
        //    DataTable dt = oj.FindNode(sz_CompanyID, sz_speciality);

        //    sz_Node_ID = dt.Rows[0]["I_NODE_ID"].ToString();
        //    sz_NodeName = dt.Rows[0]["SZ_NODE_TYPE"].ToString();

        //    hdNodeID.Value = sz_Node_ID;
        //    hdNodeName.Value = sz_NodeName;
        //}
        //catch (Exception ex)
        //{
        //    //usrMessage.PutMessage(ex.ToString());
        //    //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
        //    //usrMessage.Show();
        //}
        //finally
        //{

        //}
    }

    protected void DDLAttendingDoctors_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sz_doctorID = DDLAttendingDoctors.SelectedValue.ToString();
        FillDoctorInfo(sz_doctorID);
    }

    protected void btnSaveBottom_Click(object sender, EventArgs e)
    {
        SaveData();
    }

    protected void btnPrinBottom_Click(object sender, EventArgs e)
    {
        //if (TxtInsuranceNameAdd.Text.Trim() != "" || TxtInsuranceNameAdd.Text.Trim() != ",")
        //{

        //    try
        //    {
        //        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        //        sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        //        MBS_CaseMG2 oj = new MBS_CaseMG2();
        //        string flg = oj.MG2CheckExist(sz_CompanyID, sz_CaseID);
        //        if (flg == "1")
        //            PrintPDF();
        //        else
        //        {
        //            usrMessage.PutMessage("Please Save Record First.");
        //            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
        //            usrMessage.Show();
        //        }

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
    }

    protected void btnCancelBottom_Click(object sender, EventArgs e)
    {

    }

    protected void extddlSpeciality_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            hdSpeciality.Value = extddlSpeciality.Selected_Text;
            DataSet dsProcCode = new DataSet();
            dsProcCode = GetDoctorSpecialityProcedureCodeList(extddlSpeciality.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdProcedure.DataSource = dsProcCode;
            grdProcedure.DataBind();
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

    private DataSet GetDoctorSpecialityProcedureCodeList(string sz_Speciality, string sz_CompanyId)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("GET_DOCT_SPECIALITY_PROCEDURECODE_MG2", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", sz_Speciality);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void LoadData()
    {
        string sz_caseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        DataSet dsRecord = GetInitialRecords(sz_caseID);

        if (dsRecord.Tables.Count > 0)
        {
            if (dsRecord.Tables[0].Rows.Count > 0)
            {
                Txtwcbcasenumber.Text = dsRecord.Tables[0].Rows[0]["WCB_CASE_NO"].ToString();
                TxtCarrierCaseNo.Text = dsRecord.Tables[0].Rows[0]["CARRIER_CASE_NUMBE"].ToString();

                TxtDateofInjury.Text = dsRecord.Tables[0].Rows[0]["DATE_OF_INJURY"].ToString();
                TxtFirstName.Text = dsRecord.Tables[0].Rows[0]["FIRST NAME"].ToString();
                TxtMiddleName.Text = dsRecord.Tables[0].Rows[0]["MIDDLE NAME"].ToString();
                TxtLastName.Text = dsRecord.Tables[0].Rows[0]["LAST NAME"].ToString();
                TxtSocialSecurityNo.Text = dsRecord.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                TxtPatientAddress.Text = dsRecord.Tables[0].Rows[0]["PATIENT ADDRESS"].ToString();
                TxtEmployerNameAdd.Text = dsRecord.Tables[0].Rows[0]["EMPLOYER INFO"].ToString();
                TxtInsuranceNameAdd.Text = dsRecord.Tables[0].Rows[0]["INSURANCE INFO"].ToString();

                TxtPatientName.Text = dsRecord.Tables[0].Rows[0]["FIRST NAME"].ToString();
                TxtWCBCaseNumber2.Text = dsRecord.Tables[0].Rows[0]["WCB_CASE_NO"].ToString();
                TxtDateofInjury2.Text = dsRecord.Tables[0].Rows[0]["DATE_OF_INJURY"].ToString();
            }
        }

    }

    private DataSet GetInitialRecords(string sz_caseId)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG2_INITIAL_DETAILS", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_caseId);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void FillDoctorInfo(string sz_doctorID)
    {
        DataSet dsdocInfo = GetDoctorInfo(sz_doctorID);
        if (dsdocInfo.Tables.Count > 0)
        {
            if (dsdocInfo.Tables[0].Rows.Count > 0)
            {
                TxtIndividualProvider.Text = dsdocInfo.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
                TxtTelephone.Text = dsdocInfo.Tables[0].Rows[0]["SZ_OFFICE_PHONE"].ToString();
                TxtFaxNo.Text = dsdocInfo.Tables[0].Rows[0]["SZ_OFFICE_FAX"].ToString();
            }
        }
    }

    private DataSet GetDoctorInfo(string sz_doctorID)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_Get_Doctor_info", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_doctorID);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SaveData()
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();
        string bodyInitial, guidelineSection = "";
        string sz_Caseno = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
        string sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        string sz_companyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        if (ddlGuidline.SelectedItem.Text != "--Select--")
        {
            ArrayList ar = new ArrayList();
            string spt = ddlGuidline.SelectedItem.Text;
            string[] wrd = spt.Split('-');
            foreach (string word in wrd)
            {
                ar.Add(word);
            }
            bodyInitial = ar[0].ToString();
            guidelineSection = ar[1].ToString();
        }
        else
        {
            bodyInitial = TxtGuislineChar.Text;

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

            guidelineSection = TxtGuidline1.Text + "" + TxtGuidline2.Text + "" + TxtGuidline3.Text + "" + TxtGuidline4.Text + "" + TxtGuidline5.Text; ;
        }

        try
        {
            string user_id = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            con.Open();

            #region save mg2 details

            SqlParameter BT_CONTACTED_CARRIER_I_DID, BT_CONTACTED_CARRIER_I_DID_NOT, BT_COPY_FORM_SENT, BT_NOT_EQUIPPED_TO_SEND_FORM, BT_CARRIER_GIVES_NOTICE, BT_GRANTED;
            SqlParameter BT_GRANTED_IN_PART, BT_WITHOUT_PREJUDICE, BT_DENIED, BT_BURDEN_OF_PROOF_NOT_MET, BT_REQUEST_PENDING_DENIED;
            SqlParameter BT_DECISION_BY_MEDICAL_ARBITRATOR, BT_DECISION_AT_WCB_HEARING, BT_REQUEST_WCB_REVIEW, BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR;
            SqlParameter BT_REQUEST_DECISION_BY_WCB_HEARING;


            if (Chkdid.Checked)
                BT_CONTACTED_CARRIER_I_DID = new SqlParameter("@BT_CONTACTED_CARRIER_I_DID", 1);
            else
                BT_CONTACTED_CARRIER_I_DID = new SqlParameter("@BT_CONTACTED_CARRIER_I_DID", 0);

            if (Chkdidnot.Checked)
                BT_CONTACTED_CARRIER_I_DID_NOT = new SqlParameter("@BT_CONTACTED_CARRIER_I_DID_NOT", 1);
            else
                BT_CONTACTED_CARRIER_I_DID_NOT = new SqlParameter("@BT_CONTACTED_CARRIER_I_DID_NOT", 0);

            if (ChkAcopy.Checked)
                BT_COPY_FORM_SENT = new SqlParameter("@BT_COPY_FORM_SENT", 1);
            else
                BT_COPY_FORM_SENT = new SqlParameter("@BT_COPY_FORM_SENT", 0);

            if (ChkIAmnot.Checked)
                BT_NOT_EQUIPPED_TO_SEND_FORM = new SqlParameter("@BT_NOT_EQUIPPED_TO_SEND_FORM", 1);
            else
                BT_NOT_EQUIPPED_TO_SEND_FORM = new SqlParameter("@BT_NOT_EQUIPPED_TO_SEND_FORM", 0);

            if (ChktheSelf.Checked)
                BT_CARRIER_GIVES_NOTICE = new SqlParameter("@BT_CARRIER_GIVES_NOTICE", 1);
            else
                BT_CARRIER_GIVES_NOTICE = new SqlParameter("@BT_CARRIER_GIVES_NOTICE", 0);

            if (ChkGranted.Checked)
                BT_GRANTED = new SqlParameter("@BT_GRANTED", 1);
            else
                BT_GRANTED = new SqlParameter("@BT_GRANTED", 0);

            if (ChkGrantedinPart.Checked)
                BT_GRANTED_IN_PART = new SqlParameter("@BT_GRANTED_IN_PART", 1);
            else
                BT_GRANTED_IN_PART = new SqlParameter("@BT_GRANTED_IN_PART", 0);

            if (ChkWithoutPrejudice.Checked)
                BT_WITHOUT_PREJUDICE = new SqlParameter("@BT_WITHOUT_PREJUDICE", 1);
            else
                BT_WITHOUT_PREJUDICE = new SqlParameter("@BT_WITHOUT_PREJUDICE", 0);

            if (ChkDenied.Checked)
                BT_DENIED = new SqlParameter("@BT_DENIED", 1);
            else
                BT_DENIED = new SqlParameter("@BT_DENIED", 0);

            if (ChkBurden.Checked)
                BT_BURDEN_OF_PROOF_NOT_MET = new SqlParameter("@BT_BURDEN_OF_PROOF_NOT_MET", 1);
            else
                BT_BURDEN_OF_PROOF_NOT_MET = new SqlParameter("@BT_BURDEN_OF_PROOF_NOT_MET", 0);

            if (ChkSubstantially.Checked)
                BT_REQUEST_PENDING_DENIED = new SqlParameter("@BT_REQUEST_PENDING_DENIED", 1);
            else
                BT_REQUEST_PENDING_DENIED = new SqlParameter("@BT_REQUEST_PENDING_DENIED", 0);

            if (ChkMadeE.Checked)
                BT_DECISION_BY_MEDICAL_ARBITRATOR = new SqlParameter("@BT_DECISION_BY_MEDICAL_ARBITRATOR", 1);
            else
                BT_DECISION_BY_MEDICAL_ARBITRATOR = new SqlParameter("@BT_DECISION_BY_MEDICAL_ARBITRATOR", 0);

            if (ChkChairE.Checked)
                BT_DECISION_AT_WCB_HEARING = new SqlParameter("@BT_DECISION_AT_WCB_HEARING", 1);
            else
                BT_DECISION_AT_WCB_HEARING = new SqlParameter("@BT_DECISION_AT_WCB_HEARING", 0);

            if (ChkIrequestG.Checked)
                BT_REQUEST_WCB_REVIEW = new SqlParameter("@BT_REQUEST_WCB_REVIEW", 1);
            else
                BT_REQUEST_WCB_REVIEW = new SqlParameter("@BT_REQUEST_WCB_REVIEW", 0);

            if (ChkMadeG.Checked)
                BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR = new SqlParameter("@BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR", 1);
            else
                BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR = new SqlParameter("@BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR", 0);

            if (ChkChairG.Checked)
                BT_REQUEST_DECISION_BY_WCB_HEARING = new SqlParameter("@BT_REQUEST_DECISION_BY_WCB_HEARING", 1);
            else
                BT_REQUEST_DECISION_BY_WCB_HEARING = new SqlParameter("@BT_REQUEST_DECISION_BY_WCB_HEARING", 0);

            DataSet dsSave = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_SAVE_MG2_DETAILS",
                new SqlParameter("@SZ_CASE_NO", sz_Caseno),
                new SqlParameter("@SZ_CASE_ID", sz_CaseID),
                new SqlParameter("@SZ_COMPANY_ID", sz_companyId),
                new SqlParameter("@I_EVENT_ID", ""),
                new SqlParameter("@BT_APPROVAL", 0),
                new SqlParameter("@SZ_WCB_CASE_NO", Txtwcbcasenumber.Text),
                new SqlParameter("@SZ_CARRIER_CASE_NO", TxtCarrierCaseNo.Text),
                new SqlParameter("@DT_OF_INJURY", TxtDateofInjury.Text),
                new SqlParameter("@SZ_PATIENTS_FIRST_NAME", TxtFirstName.Text),
                new SqlParameter("@SZ_PATIENTS_MIDDLE_NAME", TxtMiddleName.Text),
                new SqlParameter("@SZ_PATIENTS_LAST_NAME", TxtLastName.Text),
                new SqlParameter("@SZ_PATIENTS_ADDRESS", TxtPatientAddress.Text),
                new SqlParameter("@SZ_SOCIAL_SECURITY_NO", TxtSocialSecurityNo.Text),
                new SqlParameter("@SZ_EMPLYER_NAME_ADDR", TxtEmployerNameAdd.Text),
                new SqlParameter("@SZ_INSURANCE_CARRIER_NAME_ADDR", TxtInsuranceNameAdd.Text),
                new SqlParameter("@SZ_ATTENDING_DOCTOR_NAME_ADDR", DDLAttendingDoctors.SelectedItem.Text),
                new SqlParameter("@SZ_WCB_AUTH_NO", TxtIndividualProvider.Text),
                new SqlParameter("@SZ_TEL_NO", TxtTelephone.Text),
                new SqlParameter("@SZ_FAX_NO", TxtFaxNo.Text),
                new SqlParameter("@SZ_GUDELINE_CHAR", bodyInitial),
                new SqlParameter("@SZ_GUDELINE", guidelineSection),
                new SqlParameter("@DT_SERVICE_SUPPORTING_MEDICAL", TxtDateofService.Text),
                new SqlParameter("@SZ_APPROVEL_REQ_FOR", TxtApproval.Text),
                new SqlParameter("@DT_PREV_DENIED_REQUEST", TxtDateofApplicable.Text),
                BT_CONTACTED_CARRIER_I_DID, BT_CONTACTED_CARRIER_I_DID_NOT,
                new SqlParameter("@SZ_CONTACTED_CARRIER", TxtSpoke.Text),
                new SqlParameter("@SZ_SPOKE_TO", Txtspecktoanyone.Text),
                BT_COPY_FORM_SENT,
                new SqlParameter("@SZ_COPY_FORM_SENT_EMAIL_FAX", TxtAddressRequired.Text),
                BT_NOT_EQUIPPED_TO_SEND_FORM,
                new SqlParameter("@SZ_FORM_MAILED_TO_PARTIES", Txtaboveon.Text),
                new SqlParameter("@DT_REQUEST", TxtProviderDate.Text),
                BT_CARRIER_GIVES_NOTICE,
                new SqlParameter("@SZ_CARRIER_GIVES_NOTICE_BY", TxtByPrintNameD.Text),
                new SqlParameter("@SZ_CARRIER_GIVES_NOTICE_TITLE", TxtTitleD.Text),
                new SqlParameter("@DT_CARRIER_GIVES_NOTICE", TxtDateD.Text),
                new SqlParameter("@SZ_CARRIER_RESPONSE", TxtSectionE.Text),
                BT_GRANTED, BT_GRANTED_IN_PART, BT_WITHOUT_PREJUDICE, BT_DENIED, BT_BURDEN_OF_PROOF_NOT_MET, BT_REQUEST_PENDING_DENIED,
                new SqlParameter("@SZ_PROFESSIONAL_REVIEWED_DENIAL", TxtMedicalProfes.Text),
                BT_DECISION_BY_MEDICAL_ARBITRATOR, BT_DECISION_AT_WCB_HEARING,
                new SqlParameter("@SZ_DECISION_BY", TxtByPrintNameE.Text),
                new SqlParameter("@SZ_DECISION_TITLE", TxtTitleE.Text),
                new SqlParameter("@DT_DECISION_BY", TxtDateE.Text),
                new SqlParameter("@SZ_DENIAL_DISCUSSED_RESOLVED_BY", TxtByPrintNameF.Text),
                new SqlParameter("@SZ_DENIAL_DISCUSSED_RESOLVED_TITLE", TxtTitleF.Text),
                new SqlParameter("@DT_DENIAL_DISCUSSED_RESOLVED", TxtDateF.Text),
                BT_REQUEST_WCB_REVIEW, BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR, BT_REQUEST_DECISION_BY_WCB_HEARING,
                new SqlParameter("@DT_REQUEST_DECISION", TxtClaimantDate.Text),
                new SqlParameter("@SZ_SPECIALITY", extddlSpeciality.Text),
                new SqlParameter("@SZ_USER_ID", user_id),
                new SqlParameter("@SZ_STATUS", "SAVED"));


            if (dsSave.Tables.Count > 0)
            {
                if (dsSave.Tables[0].Rows.Count > 0)
                {
                    hdID.Value = dsSave.Tables[0].Rows[0][0].ToString();
                }
            }

            #endregion

            #region save procedure codes

            if (hdID.Value.ToString() != "")
            {
                #region previously saved procedure codes

                DataSet dsprevproccode = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "sp_save_mg2_procedure_codes",
                            new SqlParameter("@I_ID", hdID.Value.ToString()),
                            new SqlParameter("@SZ_CASE_ID", ""),
                            new SqlParameter("@SZ_COMPANY_ID", ""),
                            new SqlParameter("@SZ_PROCEDURE_ID", ""),
                            new SqlParameter("@SZ_TYPE_CODE_ID", ""),
                            new SqlParameter("@SZ_PROCEDURE_CODE", ""),
                            new SqlParameter("@SZ_CODE_DESCRIPTION", ""),
                            new SqlParameter("@FLT_AMOUNT", ""),
                            new SqlParameter("@FLAG", "DELETE"));

                #endregion

                #region save procedure codes

                foreach (DataGridItem j in grdProcedure.Items)
                {
                    CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                    if (chkSelect.Checked)
                    {
                        double amt = 0;
                        try
                        { amt = Convert.ToDouble(j.Cells[5].Text.ToString()); }
                        catch
                        { amt = 0; }

                        DataSet dssaveproccode = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "sp_save_mg2_procedure_codes",
                            new SqlParameter("@I_ID", hdID.Value.ToString()),
                            new SqlParameter("@SZ_CASE_ID", sz_CaseID),
                            new SqlParameter("@SZ_COMPANY_ID", sz_companyId),
                            new SqlParameter("@SZ_PROCEDURE_ID", j.Cells[1].Text.ToString()),
                            new SqlParameter("@SZ_TYPE_CODE_ID", j.Cells[2].Text.ToString()),
                            new SqlParameter("@SZ_PROCEDURE_CODE", j.Cells[3].Text.ToString()),
                            new SqlParameter("@SZ_CODE_DESCRIPTION", j.Cells[4].Text.ToString()),
                            new SqlParameter("@FLT_AMOUNT", amt),
                            new SqlParameter("@FLAG", "INSERT"));
                    }
                }

                #endregion
            }

            #endregion

            usrMessage.PutMessage("Records Saved Successfullyt.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
    
}
