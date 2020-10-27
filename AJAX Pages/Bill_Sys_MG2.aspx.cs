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
using MG2;
using MG2PDF.DataAccessObject;
//using CaseMG2;
//using CaseMG2PDF.DataAccessObject;

public partial class AJAX_Pages_Bill_Sys_MG2 : PageBase
{
    string sz_Node_ID, sz_NodeName, sz_CompanyID, sz_CaseID, Logicalpath, sz_UserID, sz_doctorID, sz_Caseno, sz_speciality = "", CheckDC = "", tempPath="";
    string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
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
                string CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                //bindXml();
                GetMG2GridDetails(CaseID);

                extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();

                DataSet dsDoctorName = _obj.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                DDLAttendingDoctors.DataSource = dsDoctorName;
                ListItem objLI = new ListItem("---select---", "NA");
                DDLAttendingDoctors.DataTextField = "DESCRIPTION";
                DDLAttendingDoctors.DataValueField = "CODE";
                DDLAttendingDoctors.DataBind();
                Session["I_ID"] = null;
                LoadData();
               // GetRecord();
                //GetPatientDetails();
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

    ////COMMENT FOR TEST
    //private void bindXml()
    //{
    //    try
    //    {
    //        // Create a DataSet and fill it with data from an XML file. 
    //        DataSet xmlDataSet = new sDataSet();
    //        xmlDataSet.ReadXml(Server.MapPath("RegDetails.xml"));

    //        // Bind the grid to the DataSet. 
    //        ASPxGridView1.DataSource = xmlDataSet.Tables[0];
    //        ASPxGridView1.KeyFieldName = "CreatedBy";
    //        ASPxGridView1.DataBind();
    //    }
    //    catch (Exception ex)
    //    { }
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        SaveData();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (TxtInsuranceNameAdd.Text.Trim() != "" || TxtInsuranceNameAdd.Text.Trim() != ",")
        {

            try
            {
                sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                string i_Id = Session["I_ID"].ToString();
                MBS_CASEWISE_MG2 objMBS = new MBS_CASEWISE_MG2();
                DataTable flg = objMBS.GetMG2Record(sz_CompanyID, sz_CaseID, i_Id);
                if (flg.Rows.Count > 0)
                {
                    PrintPDF();
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
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //private void PrintPDF()
    //{
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
    //}

    private void PrintPDF()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        { 
            string i_id="";
            if(Session["I_ID"]!=null)
            {
                i_id = Session["I_ID"].ToString();
            }

            MBS_CASEWISE_MG2 obj = new MBS_CASEWISE_MG2();
            string CmpName = obj.GetPDFPath(sz_CompanyID, sz_CaseID, extddlSpeciality.Selected_Text);

            Logicalpath = "MG2_" + i_id + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff");

            tempPath = obj.generateMG2New(sz_CompanyID, sz_CaseID, i_id, CmpName, Logicalpath);
            tempPath = tempPath.Replace('\\', '/');
            SaveLogicalPath();
            string phpath= ConfigurationManager.AppSettings["BASEPATH"].ToString();
            phpath = phpath.Replace('\\', '/');
            string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
            string openpath = tempPath.Replace(phpath, str);

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + openpath + "');", true);
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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (TxtInsuranceNameAdd.Text.Trim() != "" || TxtInsuranceNameAdd.Text.Trim() != ",")
        {

            try
            {
                sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                string i_Id = Session["I_ID"].ToString();
                MBS_CASEWISE_MG2 objMBS = new MBS_CASEWISE_MG2();
                DataTable flg = objMBS.GetMG2Record(sz_CompanyID, sz_CaseID, i_Id);
                if (flg.Rows.Count > 0)
                {
                    PrintPDF();
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

    protected void btnCancelBottom_Click(object sender, EventArgs e)
    {

    }

    protected void extddlSpeciality_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    {
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
            ex.Message.ToString();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;
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
    {
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
    {
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
        int I_ID = 0;
        if(Session["I_ID"] !=null)
        {
            I_ID = Convert.ToInt32(Session["I_ID"].ToString());
        }
        //NEW CODE

        string bodyInitial, guidelineSection = "";
        string sz_Caseno = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
        string sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        string sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        string sz_UserID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        AddMG2Casewise objMG2 = new AddMG2Casewise();


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
        //objMG2.sz_BillNo = sz_BillNo;
        //objMG2.PatientID = PatientID;

        objMG2.I_ID = I_ID;
        objMG2.sz_procedure_group_id = extddlSpeciality.Text;

        MBS_CASEWISE_MG2 oj = new MBS_CASEWISE_MG2();
        string i_ids= oj.SaveMG2(objMG2);
        //END

        ////COMMENT FOR TEST
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        //DataSet ds = new DataSet();
        //string bodyInitial, guidelineSection = "";
        //string sz_Caseno = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
        //string sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        //string sz_companyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        //if (ddlGuidline.SelectedItem.Text != "--Select--")
        //{
        //    ArrayList ar = new ArrayList();
        //    string spt = ddlGuidline.SelectedItem.Text;
        //    string[] wrd = spt.Split('-');
        //    foreach (string word in wrd)
        //    {
        //        ar.Add(word);
        //    }
        //    bodyInitial = ar[0].ToString();
        //    guidelineSection = ar[1].ToString();
        //}
        //else
        //{
        //    bodyInitial = TxtGuislineChar.Text;

        //    if (TxtGuidline1.Text == "")
        //        TxtGuidline1.Text = " ";
        //    if (TxtGuidline2.Text == "")
        //        TxtGuidline2.Text = " ";
        //    if (TxtGuidline3.Text == "")
        //        TxtGuidline3.Text = " ";
        //    if (TxtGuidline4.Text == "")
        //        TxtGuidline4.Text = " ";
        //    if (TxtGuidline5.Text == "")
        //        TxtGuidline5.Text = " ";

        //    guidelineSection = TxtGuidline1.Text + "" + TxtGuidline2.Text + "" + TxtGuidline3.Text + "" + TxtGuidline4.Text + "" + TxtGuidline5.Text; ;
        //}
        ////END COMMENT

        ////COMMENT FOR TEST
        //try
        //{
        //    string user_id = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        //    con.Open();

        //    #region save mg2 details

        //    SqlParameter BT_CONTACTED_CARRIER_I_DID, BT_CONTACTED_CARRIER_I_DID_NOT, BT_COPY_FORM_SENT, BT_NOT_EQUIPPED_TO_SEND_FORM, BT_CARRIER_GIVES_NOTICE, BT_GRANTED;
        //    SqlParameter BT_GRANTED_IN_PART, BT_WITHOUT_PREJUDICE, BT_DENIED, BT_BURDEN_OF_PROOF_NOT_MET, BT_REQUEST_PENDING_DENIED;
        //    SqlParameter BT_DECISION_BY_MEDICAL_ARBITRATOR, BT_DECISION_AT_WCB_HEARING, BT_REQUEST_WCB_REVIEW, BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR;
        //    SqlParameter BT_REQUEST_DECISION_BY_WCB_HEARING;


        //    if (Chkdid.Checked)
        //        BT_CONTACTED_CARRIER_I_DID = new SqlParameter("@BT_CONTACTED_CARRIER_I_DID", 1);
        //    else
        //        BT_CONTACTED_CARRIER_I_DID = new SqlParameter("@BT_CONTACTED_CARRIER_I_DID", 0);

        //    if (Chkdidnot.Checked)
        //        BT_CONTACTED_CARRIER_I_DID_NOT = new SqlParameter("@BT_CONTACTED_CARRIER_I_DID_NOT", 1);
        //    else
        //        BT_CONTACTED_CARRIER_I_DID_NOT = new SqlParameter("@BT_CONTACTED_CARRIER_I_DID_NOT", 0);

        //    if (ChkAcopy.Checked)
        //        BT_COPY_FORM_SENT = new SqlParameter("@BT_COPY_FORM_SENT", 1);
        //    else
        //        BT_COPY_FORM_SENT = new SqlParameter("@BT_COPY_FORM_SENT", 0);

        //    if (ChkIAmnot.Checked)
        //        BT_NOT_EQUIPPED_TO_SEND_FORM = new SqlParameter("@BT_NOT_EQUIPPED_TO_SEND_FORM", 1);
        //    else
        //        BT_NOT_EQUIPPED_TO_SEND_FORM = new SqlParameter("@BT_NOT_EQUIPPED_TO_SEND_FORM", 0);

        //    if (ChktheSelf.Checked)
        //        BT_CARRIER_GIVES_NOTICE = new SqlParameter("@BT_CARRIER_GIVES_NOTICE", 1);
        //    else
        //        BT_CARRIER_GIVES_NOTICE = new SqlParameter("@BT_CARRIER_GIVES_NOTICE", 0);

        //    if (ChkGranted.Checked)
        //        BT_GRANTED = new SqlParameter("@BT_GRANTED", 1);
        //    else
        //        BT_GRANTED = new SqlParameter("@BT_GRANTED", 0);

        //    if (ChkGrantedinPart.Checked)
        //        BT_GRANTED_IN_PART = new SqlParameter("@BT_GRANTED_IN_PART", 1);
        //    else
        //        BT_GRANTED_IN_PART = new SqlParameter("@BT_GRANTED_IN_PART", 0);

        //    if (ChkWithoutPrejudice.Checked)
        //        BT_WITHOUT_PREJUDICE = new SqlParameter("@BT_WITHOUT_PREJUDICE", 1);
        //    else
        //        BT_WITHOUT_PREJUDICE = new SqlParameter("@BT_WITHOUT_PREJUDICE", 0);

        //    if (ChkDenied.Checked)
        //        BT_DENIED = new SqlParameter("@BT_DENIED", 1);
        //    else
        //        BT_DENIED = new SqlParameter("@BT_DENIED", 0);

        //    if (ChkBurden.Checked)
        //        BT_BURDEN_OF_PROOF_NOT_MET = new SqlParameter("@BT_BURDEN_OF_PROOF_NOT_MET", 1);
        //    else
        //        BT_BURDEN_OF_PROOF_NOT_MET = new SqlParameter("@BT_BURDEN_OF_PROOF_NOT_MET", 0);

        //    if (ChkSubstantially.Checked)
        //        BT_REQUEST_PENDING_DENIED = new SqlParameter("@BT_REQUEST_PENDING_DENIED", 1);
        //    else
        //        BT_REQUEST_PENDING_DENIED = new SqlParameter("@BT_REQUEST_PENDING_DENIED", 0);

        //    if (ChkMadeE.Checked)
        //        BT_DECISION_BY_MEDICAL_ARBITRATOR = new SqlParameter("@BT_DECISION_BY_MEDICAL_ARBITRATOR", 1);
        //    else
        //        BT_DECISION_BY_MEDICAL_ARBITRATOR = new SqlParameter("@BT_DECISION_BY_MEDICAL_ARBITRATOR", 0);

        //    if (ChkChairE.Checked)
        //        BT_DECISION_AT_WCB_HEARING = new SqlParameter("@BT_DECISION_AT_WCB_HEARING", 1);
        //    else
        //        BT_DECISION_AT_WCB_HEARING = new SqlParameter("@BT_DECISION_AT_WCB_HEARING", 0);

        //    if (ChkIrequestG.Checked)
        //        BT_REQUEST_WCB_REVIEW = new SqlParameter("@BT_REQUEST_WCB_REVIEW", 1);
        //    else
        //        BT_REQUEST_WCB_REVIEW = new SqlParameter("@BT_REQUEST_WCB_REVIEW", 0);

        //    if (ChkMadeG.Checked)
        //        BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR = new SqlParameter("@BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR", 1);
        //    else
        //        BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR = new SqlParameter("@BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR", 0);

        //    if (ChkChairG.Checked)
        //        BT_REQUEST_DECISION_BY_WCB_HEARING = new SqlParameter("@BT_REQUEST_DECISION_BY_WCB_HEARING", 1);
        //    else
        //        BT_REQUEST_DECISION_BY_WCB_HEARING = new SqlParameter("@BT_REQUEST_DECISION_BY_WCB_HEARING", 0);

        //    DataSet dsSave = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_SAVE_MG2_DETAILS",
        //        new SqlParameter("@SZ_CASE_NO", sz_Caseno),
        //        new SqlParameter("@SZ_CASE_ID", sz_CaseID),
        //        new SqlParameter("@SZ_COMPANY_ID", sz_companyId),
        //        new SqlParameter("@I_EVENT_ID", ""),
        //        new SqlParameter("@BT_APPROVAL", 0),
        //        new SqlParameter("@SZ_WCB_CASE_NO", Txtwcbcasenumber.Text),
        //        new SqlParameter("@SZ_CARRIER_CASE_NO", TxtCarrierCaseNo.Text),
        //        new SqlParameter("@DT_OF_INJURY", TxtDateofInjury.Text),
        //        new SqlParameter("@SZ_PATIENTS_FIRST_NAME", TxtFirstName.Text),
        //        new SqlParameter("@SZ_PATIENTS_MIDDLE_NAME", TxtMiddleName.Text),
        //        new SqlParameter("@SZ_PATIENTS_LAST_NAME", TxtLastName.Text),
        //        new SqlParameter("@SZ_PATIENTS_ADDRESS", TxtPatientAddress.Text),
        //        new SqlParameter("@SZ_SOCIAL_SECURITY_NO", TxtSocialSecurityNo.Text),
        //        new SqlParameter("@SZ_EMPLYER_NAME_ADDR", TxtEmployerNameAdd.Text),
        //        new SqlParameter("@SZ_INSURANCE_CARRIER_NAME_ADDR", TxtInsuranceNameAdd.Text),
        //        new SqlParameter("@SZ_ATTENDING_DOCTOR_NAME_ADDR", DDLAttendingDoctors.SelectedItem.Text),
        //        new SqlParameter("@SZ_WCB_AUTH_NO", TxtIndividualProvider.Text),
        //        new SqlParameter("@SZ_TEL_NO", TxtTelephone.Text),
        //        new SqlParameter("@SZ_FAX_NO", TxtFaxNo.Text),
        //        new SqlParameter("@SZ_GUDELINE_CHAR", bodyInitial),
        //        new SqlParameter("@SZ_GUDELINE", guidelineSection),
        //        new SqlParameter("@DT_SERVICE_SUPPORTING_MEDICAL", TxtDateofService.Text),
        //        new SqlParameter("@SZ_APPROVEL_REQ_FOR", TxtApproval.Text),
        //        //new SqlParameter("@DT_PREV_DENIED_REQUEST", TxtDateofApplicable.Text),
        //        new SqlParameter("@DT_PREV_DENIED_REQUEST", TxtDateofApplicable.Text),
        //        BT_CONTACTED_CARRIER_I_DID, BT_CONTACTED_CARRIER_I_DID_NOT,
        //        new SqlParameter("@SZ_CONTACTED_CARRIER", TxtSpoke.Text),
        //        new SqlParameter("@SZ_SPOKE_TO", Txtspecktoanyone.Text),
        //        BT_COPY_FORM_SENT,
        //        new SqlParameter("@SZ_COPY_FORM_SENT_EMAIL_FAX", TxtAddressRequired.Text),
        //        BT_NOT_EQUIPPED_TO_SEND_FORM,
        //        new SqlParameter("@SZ_FORM_MAILED_TO_PARTIES", Txtaboveon.Text),
        //        new SqlParameter("@DT_REQUEST", TxtProviderDate.Text),
        //        BT_CARRIER_GIVES_NOTICE,
        //        new SqlParameter("@SZ_CARRIER_GIVES_NOTICE_BY", TxtByPrintNameD.Text),
        //        new SqlParameter("@SZ_CARRIER_GIVES_NOTICE_TITLE", TxtTitleD.Text),
        //        new SqlParameter("@DT_CARRIER_GIVES_NOTICE", TxtDateD.Text),
        //        new SqlParameter("@SZ_CARRIER_RESPONSE", TxtSectionE.Text),
        //        BT_GRANTED, BT_GRANTED_IN_PART, BT_WITHOUT_PREJUDICE, BT_DENIED, BT_BURDEN_OF_PROOF_NOT_MET, BT_REQUEST_PENDING_DENIED,
        //        new SqlParameter("@SZ_PROFESSIONAL_REVIEWED_DENIAL", TxtMedicalProfes.Text),
        //        BT_DECISION_BY_MEDICAL_ARBITRATOR, BT_DECISION_AT_WCB_HEARING,
        //        new SqlParameter("@SZ_DECISION_BY", TxtByPrintNameE.Text),
        //        new SqlParameter("@SZ_DECISION_TITLE", TxtTitleE.Text),
        //        new SqlParameter("@DT_DECISION_BY", TxtDateE.Text),
        //        new SqlParameter("@SZ_DENIAL_DISCUSSED_RESOLVED_BY", TxtByPrintNameF.Text),
        //        new SqlParameter("@SZ_DENIAL_DISCUSSED_RESOLVED_TITLE", TxtTitleF.Text),
        //        new SqlParameter("@DT_DENIAL_DISCUSSED_RESOLVED", TxtDateF.Text),
        //        BT_REQUEST_WCB_REVIEW, BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR, BT_REQUEST_DECISION_BY_WCB_HEARING,
        //        new SqlParameter("@DT_REQUEST_DECISION", TxtClaimantDate.Text),
        //        new SqlParameter("@SZ_SPECIALITY", extddlSpeciality.Text),
        //        new SqlParameter("@SZ_USER_ID", user_id),
        //        new SqlParameter("@SZ_STATUS", "SAVED"),
        //        new SqlParameter("@I_I_ID", I_ID)
        //        );

           
        //    if (dsSave.Tables.Count > 0)
        //    {
        //        if (dsSave.Tables[0].Rows.Count > 0)
        //        {
        //            hdID.Value = dsSave.Tables[0].Rows[0][0].ToString();
        //            Session["I_ID"] = dsSave.Tables[0].Rows[0][0].ToString();
        //        }
        //    }
        //    //END COMMENT
            //#endregion
        if (i_ids != "0") {
            Session["I_ID"] = i_ids;
        }
        try{
            #region save procedure codes

            if (Session["I_ID"].ToString() != "")
            {
                #region previously saved procedure codes

                DataSet dsprevproccode = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "sp_save_mg2_procedure_codes",
                            new SqlParameter("@I_ID", Session["I_ID"].ToString()),
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
                            new SqlParameter("@I_ID", Session["I_ID"].ToString()),
                            new SqlParameter("@SZ_CASE_ID", sz_CaseID),
                            new SqlParameter("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID),
                            new SqlParameter("@SZ_PROCEDURE_ID", j.Cells[1].Text.ToString()),
                            new SqlParameter("@SZ_TYPE_CODE_ID", j.Cells[2].Text.ToString()),
                            new SqlParameter("@SZ_PROCEDURE_CODE", j.Cells[3].Text.ToString()),
                            new SqlParameter("@SZ_CODE_DESCRIPTION", j.Cells[4].Text.ToString()),
                            new SqlParameter("@FLT_AMOUNT", amt),
                            new SqlParameter("@FLAG", "INSERT")
                            );
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

    protected void carTabPage_ActiveTabChanged(object source, DevExpress.Web.TabControlEventArgs e)
    {
        string caseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        DataSet dsAvailableMg2 = GetMG2GridDetails(caseID);
        ASPxGridView1.DataSource = dsAvailableMg2;
        ASPxGridView1.DataBind();
        int index = carTabPage.ActiveTabIndex;
    }

    private DataSet getAvailbaleMg2(string caseID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());

            con.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("SP_GET_MG2_DETAIL", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);

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
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkView_Command(object sender, CommandEventArgs e)
    {

    }

    protected void ASPxGridView1_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        /*//add for test
        try
        {
            Session["I_ID"] = ASPxGridView1.GetRowValues(e.VisibleIndex, "I_ID").ToString();
            //string sI_ID = ASPxGridView1.GetRowValues(e.VisibleIndex, "I_ID").ToString();
            string sI_ID = Session["I_ID"].ToString();
            DataSet ds = GetMG2etails(sI_ID);
            if (ds != null)
            {
                if (ds.Tables.Count != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Txtwcbcasenumber.Text = ds.Tables[0].Rows[0]["SZ_WCB_CASE_NO"].ToString();
                        TxtCarrierCaseNo.Text = ds.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString();
                        TxtDateofInjury.Text = ds.Tables[0].Rows[0]["DT_OF_INJURY"].ToString();
                        TxtFirstName.Text = ds.Tables[0].Rows[0]["SZ_PATIENTS_FIRST_NAME"].ToString();
                        TxtMiddleName.Text = ds.Tables[0].Rows[0]["SZ_PATIENTS_MIDDLE_NAME"].ToString();
                        TxtLastName.Text = ds.Tables[0].Rows[0]["SZ_PATIENTS_LAST_NAME"].ToString();
                        TxtSocialSecurityNo.Text = ds.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                        TxtPatientAddress.Text = ds.Tables[0].Rows[0]["SZ_PATIENTS_ADDRESS"].ToString();
                        TxtEmployerNameAdd.Text = ds.Tables[0].Rows[0]["SZ_EMPLYER_NAME_ADDR"].ToString();
                        for (int i = 0; i < DDLAttendingDoctors.Items.Count; i++)
                        {
                            if (DDLAttendingDoctors.Items[i].Text == ds.Tables[0].Rows[0]["SZ_ATTENDING_DOCTOR_NAME_ADDR"].ToString())
                            {
                                DDLAttendingDoctors.SelectedValue = DDLAttendingDoctors.Items[i].Value;
                                break;
                            }

                        }
                        TxtInsuranceNameAdd.Text = ds.Tables[0].Rows[0]["SZ_INSURANCE_CARRIER_NAME_ADDR"].ToString();
                        //DDLAttendingDoctors.SelectedValue = ds.Tables[0].Rows[0]["SZ_ATTENDING_DOCTOR_NAME_ADDR"].ToString();
                        TxtIndividualProvider.Text = ds.Tables[0].Rows[0]["SZ_WCB_AUTH_NO"].ToString();
                        TxtTelephone.Text = ds.Tables[0].Rows[0]["SZ_TEL_NO"].ToString();
                        TxtFaxNo.Text = ds.Tables[0].Rows[0]["SZ_FAX_NO"].ToString();


                        for (int i = 0; i < ddlGuidline.Items.Count; i++)
                        {
                            if (ddlGuidline.Items[i].Text == ds.Tables[0].Rows[0]["SZ_GUDELINE_CHAR"].ToString() + "-" + ds.Tables[0].Rows[0]["SZ_GUDELINE"].ToString())
                            {
                                ddlGuidline.SelectedIndex = i;
                                break;
                            }

                        }

                        TxtApproval.Text = ds.Tables[0].Rows[0]["SZ_APPROVEL_REQ_FOR"].ToString();
                        TxtDateofService.Text = ds.Tables[0].Rows[0]["DT_SERVICE_SUPPORTING_MEDICAL"].ToString();
                        TxtDateofApplicable.Text = ds.Tables[0].Rows[0]["DT_PREV_DENIED_REQUEST"].ToString();

                        if (ds.Tables[0].Rows[0]["BT_CONTACTED_CARRIER_I_DID"].ToString() == "True")
                        {
                            Chkdid.Checked = true;
                            TxtSpoke.Text = ds.Tables[0].Rows[0]["SZ_SPOKE_TO"].ToString();
                        }
                        else
                        {
                            Chkdidnot.Checked = false;
                            TxtSpoke.Text = ds.Tables[0].Rows[0]["SZ_SPOKE_TO"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["BT_COPY_FORM_SENT"].ToString() == "True")
                        {
                            ChkAcopy.Checked = true;
                            TxtAddressRequired.Text = ds.Tables[0].Rows[0]["SZ_COPY_FORM_SENT_EMAIL_FAX"].ToString();
                        }
                        else
                        {
                            ChkAcopy.Checked = false;
                            TxtAddressRequired.Text = ds.Tables[0].Rows[0]["SZ_COPY_FORM_SENT_EMAIL_FAX"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["BT_NOT_EQUIPPED_TO_SEND_FORM"].ToString() == "True")
                        {
                            ChkIAmnot.Checked = true;
                            Txtaboveon.Text = ds.Tables[0].Rows[0]["SZ_FORM_MAILED_TO_PARTIES"].ToString();
                        }
                        else
                        {
                            ChkIAmnot.Checked = false;
                            Txtaboveon.Text = ds.Tables[0].Rows[0]["SZ_FORM_MAILED_TO_PARTIES"].ToString();
                        }
                        TxtProviderDate.Text = ds.Tables[0].Rows[0]["DT_REQUEST"].ToString();
                        TxtPatientName.Text = TxtFirstName.Text + TxtMiddleName.Text + TxtLastName.Text;
                        TxtWCBCaseNumber2.Text = ds.Tables[0].Rows[0]["SZ_WCB_CASE_NO"].ToString();
                        TxtDateofInjury2.Text = ds.Tables[0].Rows[0]["DT_OF_INJURY"].ToString();

                        if (ds.Tables[0].Rows[0]["BT_CARRIER_GIVES_NOTICE"].ToString() == "True")
                        {
                            ChktheSelf.Checked = true;
                            TxtByPrintNameD.Text = ds.Tables[0].Rows[0]["SZ_CARRIER_GIVES_NOTICE_BY"].ToString();
                            TxtTitleD.Text = ds.Tables[0].Rows[0]["SZ_CARRIER_GIVES_NOTICE_TITLE"].ToString();
                            TxtSignatureD.Text = ds.Tables[0].Rows[0]["DT_CARRIER_GIVES_NOTICE"].ToString();
                        }
                        else
                        {
                            ChktheSelf.Checked = false;
                            TxtByPrintNameD.Text = ds.Tables[0].Rows[0]["SZ_CARRIER_GIVES_NOTICE_BY"].ToString();
                            TxtTitleD.Text = ds.Tables[0].Rows[0]["SZ_CARRIER_GIVES_NOTICE_TITLE"].ToString();
                            TxtSignatureD.Text = ds.Tables[0].Rows[0]["DT_CARRIER_GIVES_NOTICE"].ToString();
                        }
                        TxtSectionE.Text = ds.Tables[0].Rows[0]["SZ_CARRIER_RESPONSE"].ToString();
                        if (ds.Tables[0].Rows[0]["BT_GRANTED"].ToString() == "True")
                        {
                            ChkGranted.Checked = true;
                        }
                        else
                        {
                            ChkGranted.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["BT_GRANTED_IN_PART"].ToString() == "True")
                        {
                            ChkGrantedinPart.Checked = true;
                        }
                        else
                        {
                            ChkGrantedinPart.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["BT_WITHOUT_PREJUDICE"].ToString() == "True")
                        {
                            ChkWithoutPrejudice.Checked = true;
                        }
                        else
                        {
                            ChkWithoutPrejudice.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["BT_DENIED"].ToString() == "True")
                        {
                            ChkDenied.Checked = true;
                        }
                        else
                        {
                            ChkDenied.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["BT_BURDEN_OF_PROOF_NOT_MET"].ToString() == "True")
                        {
                            ChkBurden.Checked = true;
                        }
                        else
                        {
                            ChkBurden.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["BT_REQUEST_PENDING_DENIED"].ToString() == "True")
                        {
                            ChkSubstantially.Checked = true;
                        }
                        else
                        {
                            ChkSubstantially.Checked = false;
                        }
                        TxtMedicalProfes.Text = ds.Tables[0].Rows[0]["SZ_PROFESSIONAL_REVIEWED_DENIAL"].ToString();

                        if (ds.Tables[0].Rows[0]["BT_DECISION_BY_MEDICAL_ARBITRATOR"].ToString() == "True")
                        {
                            ChkMadeE.Checked = true;
                        }
                        else
                        {
                            ChkMadeE.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["BT_DECISION_AT_WCB_HEARING"].ToString() == "True")
                        {
                            ChkChairE.Checked = true;
                        }
                        else
                        {
                            ChkChairE.Checked = false;
                        }
                        TxtByPrintNameE.Text = ds.Tables[0].Rows[0]["SZ_DECISION_BY"].ToString();
                        TxtTitleE.Text = ds.Tables[0].Rows[0]["SZ_DECISION_TITLE"].ToString();
                        TxtDateE.Text = ds.Tables[0].Rows[0]["DT_DECISION_BY"].ToString();
                        TxtByPrintNameF.Text = ds.Tables[0].Rows[0]["SZ_DENIAL_DISCUSSED_RESOLVED_BY"].ToString();
                        TxtTitleF.Text = ds.Tables[0].Rows[0]["SZ_DENIAL_DISCUSSED_RESOLVED_TITLE"].ToString();
                        TxtDateF.Text = ds.Tables[0].Rows[0]["DT_DENIAL_DISCUSSED_RESOLVED"].ToString();

                        if (ds.Tables[0].Rows[0]["BT_REQUEST_WCB_REVIEW"].ToString() == "True")
                        {
                            ChkIrequestG.Checked = true;
                        }
                        else
                        {
                            ChkIrequestG.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["BT_REQUEST_DECISION_BY_MEDICAL_ARBITRATOR"].ToString() == "True")
                        {
                            ChkMadeG.Checked = true;
                        }
                        else
                        {
                            ChkMadeG.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0]["BT_REQUEST_DECISION_BY_WCB_HEARING"].ToString() == "True")
                        {
                            ChkChairG.Checked = true;
                        }
                        else
                        {
                            ChkChairG.Checked = false;
                        }

                        TxtClaimantDate.Text = ds.Tables[0].Rows[0]["DT_REQUEST_DECISION"].ToString();
                        extddlSpeciality.Text = ds.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString();
                        //end comment

                        DataSet dsProcCode;
                        try
                        {
                            hdSpeciality.Value = extddlSpeciality.Selected_Text;
                            dsProcCode = new DataSet();
                            dsProcCode = GetDoctorSpecialityProcedureCodeList(extddlSpeciality.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            grdProcedure.DataSource = dsProcCode;
                            grdProcedure.DataBind();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        
                        //string CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                        DataSet dsGetProcCodes = GetProcedureCodes(sI_ID);
                        if (dsGetProcCodes != null)
                        {
                            if (dsGetProcCodes.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < grdProcedure.Items.Count; i++)
                                {
                                    CheckBox chk = (CheckBox)grdProcedure.Items[i].FindControl("chkselect");
                                    for (int j = 0; j < dsGetProcCodes.Tables[0].Rows.Count; j++)
                                    {
                                        if (grdProcedure.DataKeys[i].ToString().Equals(dsGetProcCodes.Tables[0].Rows[j]["SZ_TYPE_CODE_ID"].ToString()))
                                        {
                                            chk.Checked = true;
                                        }
                                    }
                                }
                            }
                        }
                        carTabPage.ActiveTabIndex = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }*/

        Session["I_ID"] = ASPxGridView1.GetRowValues(e.VisibleIndex, "I_ID").ToString();

        string i_id = Session["I_ID"].ToString();
        GetRecord();

        DataSet dsProcCode;
        try
        {
            hdSpeciality.Value = extddlSpeciality.Selected_Text;
            dsProcCode = new DataSet();
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
        //string CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        DataSet dsGetProcCodes = GetProcedureCodes(i_id);
        if (dsGetProcCodes != null)
        {
            if (dsGetProcCodes.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < grdProcedure.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)grdProcedure.Items[i].FindControl("chkselect");
                    for (int j = 0; j < dsGetProcCodes.Tables[0].Rows.Count; j++)
                    {
                        if (grdProcedure.DataKeys[i].ToString().Equals(dsGetProcCodes.Tables[0].Rows[j]["SZ_TYPE_CODE_ID"].ToString()))
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetMG2GridDetails(string szCaseId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG2_DETAIL", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ASPxGridView1.DataSource = ds;
                    //ASPxGridView1.KeyFieldName = "CreatedBy";
                    ASPxGridView1.DataBind();
                }
            }
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
            }
        }
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetMG2etails(string Id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG2_DETAIL_FROM_IDENTITY", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_ID", Id);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
            }
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
            }
        }
        return ds;
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

    public DataSet GetProcedureCodes(string sI_ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG2_PROC_CODE_FROM_IDENTITY", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_I_ID", sI_ID);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
            }
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
            }
        }
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //add new methid
    public void GetRecord()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            MBS_CASEWISE_MG2 oj = new MBS_CASEWISE_MG2();
            string sz_CaseID12 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            string sz_CompanyID12 = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            DataTable dt = oj.GetMG2Record(sz_CompanyID12, sz_CaseID12, Session["I_ID"].ToString());

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

                for (int i = 0; i < DDLAttendingDoctors.Items.Count; i++)
                {
                    if (DDLAttendingDoctors.Items[i].Text == dt.Rows[0]["sz_doctor_ID"].ToString())
                    {
                        DDLAttendingDoctors.SelectedValue = DDLAttendingDoctors.Items[i].Value;
                        break;
                    }

                }
                //DDLAttendingDoctors.SelectedValue = dt.Rows[0]["sz_doctor_ID"].ToString();
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
                    string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
                    //hyLinkOpenPDF.NavigateUrl = "http://localhost/MBSScans/" + filepath;
                    //hyLinkOpenPDF.NavigateUrl = "https://www.gogreenbills.com/MBSScans/"+filepath;
                    hyLinkOpenPDF.NavigateUrl = str + "/" + filepath;
                }
                else
                { }

                //ddlGuidline.Text = dt.Rows[0]["sz_Guidline_Char"].ToString() + "-" + dt.Rows[0]["sz_Guidline"].ToString();
                //string gdl = dt.Rows[0]["sz_Guidline"].ToString();
                //if (gdl != "")
                //{

                //    TxtGuidline1.Text = gdl[0].ToString();
                //    TxtGuidline2.Text = gdl[1].ToString();
                //    TxtGuidline3.Text = gdl[2].ToString();
                //    TxtGuidline4.Text = gdl[3].ToString();
                //    TxtGuidline5.Text = gdl[4].ToString();
                //}
                for (int i = 0; i < ddlGuidline.Items.Count; i++)
                {
                    if (ddlGuidline.Items[i].Text == dt.Rows[0]["sz_Guidline_Char"].ToString() + "-" + dt.Rows[0]["sz_Guidline"].ToString())
                    {
                        ddlGuidline.SelectedIndex = i;
                        break;
                    }

                }

                extddlSpeciality.Text = dt.Rows[0]["sz_procedure_group_id"].ToString(); ;
            }

            carTabPage.ActiveTabIndex = 0;

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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            MBS_CASEWISE_MG2 oj = new MBS_CASEWISE_MG2();
            DataTable dt = oj.GetMG2Patient(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString());

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

    //end new method

    public DataSet GetMG2RecordById(String i_Id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();
        try
        {
            con = new SqlConnection(strConn);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_MG2_DETAILS_BY_ID", con);
            cmd.Parameters.AddWithValue("@I_ID", i_Id);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
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
}