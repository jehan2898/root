using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using HP1;
using HP1.HP1BO;

public partial class AJAX_Pages_Bill_Sys_Bill_HPJ1 : PageBase
{
    #region Variables

    bool saveFlag = false;
    Bill_Sys_SystemObject objSysObject;
    string CmpName, sz_CompanyID, sz_CaseID, sz_BillNo, sz_speciality, FileName, tempPath, sz_NodeName, sz_Node_ID, sz_UserID = "";

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            hdSave.Value = "0";
            sz_BillNo = Request.QueryString["billnumber"].ToString();
            Session["BillNumber"] = Request.QueryString["billnumber"].ToString();
            sz_CaseID = Request.QueryString["caseid"].ToString();
            Session["HPJ1_Case"] = Request.QueryString["caseid"].ToString();
            sz_speciality = Request.QueryString["speciality"].ToString();            
            
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();
            sz_CompanyID = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();
            txtBillNo.Text = sz_BillNo;
            hdbillno.Value = sz_BillNo;
            txtCaseID.Text = sz_CaseID;
            txtSpeciality.Text = sz_speciality;
            

            //LOAD DATA
            LoadData();

            #region check whether pdf has been printed previously

            DataSet dsPDF = GetHPJ1Record();
            if (dsPDF.Tables.Count > 0)
            {
                if (dsPDF.Tables[0].Rows.Count > 0)
                {
                    if ((dsPDF.Tables[0].Rows[0]["PDF_EXIST"].ToString() != "") && (dsPDF.Tables[0].Rows[0]["PDF_EXIST"].ToString() != null))
                    {
                        string oldfilepath = dsPDF.Tables[0].Rows[0]["PDF_EXIST"].ToString().Replace("\\", "/");
                        lblmsg.Visible = true;
                        lblmsg2.Visible = true;
                        hyLinkOpenPDF.Visible = true;
                        string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
                        hyLinkOpenPDF.NavigateUrl = str + "/" + oldfilepath;

                    }
                }
            }

            #endregion
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            #region get sign paths

            try
            {
                txtPhysicianSignPath.Text = Session["HPJ1_Physician_HPJ1_Sign_Path"].ToString();
                txtPhysicianSignSucc.Text = Session["HPJ1_Physician_HPJ1_Sign_Success"].ToString();
            }
            catch
            {
                txtPhysicianSignPath.Text = "";
                txtPhysicianSignSucc.Text = "0";
            }
            try
            {
                txtNotarySignPath.Text = Session["HPJ1_Notary_HPJ1_Sign_Path"].ToString();
                txtNotarySignSucc.Text = Session["HPJ1_Notary_HPJ1_Sign_Success"].ToString();
            }
            catch
            {
                txtNotarySignPath.Text = "";
                txtNotarySignSucc.Text = "0";
            }
            try
            {
                txtOtherSignPath.Text = Session["HPJ1_Other_HPJ1_Sign_Path"].ToString();
                txtOtherSignSucc.Text = Session["HPJ1_Other_HPJ1_Sign_Success"].ToString();
            }
            catch
            {
                txtOtherSignPath.Text = "";
                txtOtherSignSucc.Text = "0";
            }

            #endregion

            //SAVE DATA
            SaveData();
            if (saveFlag)
            {
                hdSave.Value = "1";
                btnPhySign.Enabled = true;
                btnNotarySign.Enabled = true;
                btnOtherSign.Enabled = true;

                usrMessage.PutMessage("Records Saved Successfully.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
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
            objSysObject = new Bill_Sys_SystemObject();
            string signNotRequired = (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_HP1_SIGN).ToString();

            if (hdSave.Value.ToString() == "1")
            {
                if (signNotRequired == "1")
                {
                    PrintPDF();
                }
                else
                {
                    if ((txtPhysicianSignPath.Text.ToString() != "") && (txtNotarySignPath.Text.ToString() != "") && (txtOtherSignPath.Text.ToString() != ""))
                    {
                        PrintPDF();
                    }
                    else
                    {
                        usrMessage.PutMessage("Please Save Signs.");
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void LoadData()
    {
        DataSet dsRecord = new DataSet();
        dsRecord = GetInitialRecord();

        if (dsRecord.Tables.Count > 0)
        {
            if (dsRecord.Tables[0].Rows.Count > 0)
            {
                #region Health Provider

                txtHPName1.Text = dsRecord.Tables[0].Rows[0]["OFFICE_NAME"].ToString();
                txtHPAddr.Text = dsRecord.Tables[0].Rows[0]["OFFICE_ADDRESS"].ToString();
                txtHPCity.Text = dsRecord.Tables[0].Rows[0]["OFFICE_CITY"].ToString();
                txtHPZip.Text = dsRecord.Tables[0].Rows[0]["OFFICE_ZIP"].ToString();

                extdlHPState.Text = dsRecord.Tables[0].Rows[0]["OFFICE_STATE_ID"].ToString();
                if (extdlHPState.Text != "NA" && extdlHPState.Text != "")
                    txtHPState.Text = extdlHPState.Selected_Text;

                #endregion

                #region Insurance

                txtCEName1.Text = dsRecord.Tables[0].Rows[0]["INSURANCE_NAME"].ToString();
                txtCEAddress.Text = dsRecord.Tables[0].Rows[0]["INSURANCE_ADDRESS"].ToString();
                txtCECity.Text = dsRecord.Tables[0].Rows[0]["INSURANCE_CITY"].ToString();
                txtCEZip.Text = dsRecord.Tables[0].Rows[0]["INSURANCE_ZIP"].ToString();

                extdlCEState.Text = dsRecord.Tables[0].Rows[0]["INSURANCE_STATE_ID"].ToString();
                if (extdlCEState.Text != "NA" && extdlCEState.Text != "")
                    txtCEState.Text = extdlCEState.Selected_Text;

                #endregion

                #region Other

                txtBillAmt.Text = dsRecord.Tables[0].Rows[0]["BILL_AMOUNT"].ToString();
                txtWCBCaseNo.Text = dsRecord.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString();
                txtWCBAuthNo.Text = dsRecord.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
                txtDtofAccident.Text = dsRecord.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString();
                txtCarrierCaseNo.Text = dsRecord.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                txtCountyInjOcc.Text = dsRecord.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
                txtEmployer.Text = dsRecord.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();

                #endregion
            }
        }
    }
    private DataSet GetInitialRecord()
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
            SqlCommand sqlCmd = new SqlCommand("SP_GET_HPJ1_INITIAL_DETAILS", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", txtBillNo.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);

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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        int btPhy, btNotary, btOther;

        #region convert sign bit

        try { btPhy = Convert.ToInt16(txtPhysicianSignSucc.Text); }
        catch { btPhy = 0; }
        try { btNotary = Convert.ToInt16(txtNotarySignSucc.Text); }
        catch { btNotary = 0; }
        try { btOther = Convert.ToInt16(txtOtherSignSucc.Text); }
        catch { btOther = 0; }

        #endregion

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_SAVE_HPJ1_DETAILS", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", txtCaseID.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", txtBillNo.Text);
            sqlCmd.Parameters.AddWithValue("@FLT_BILL_AMOUNT", txtBillAmt.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_TEXTBOX1", TextBox1.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_TEXTBOX2", TextBox2.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_TEXTBOX3", TextBox3.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_TEXTBOX4", TextBox4.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_TEXTBOX5", TextBox5.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_HEALTH_PROVIDER_NAME", txtHPName1.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_HEALTH_PROVIDER_ADDRESS", txtHPAddr.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_HEALTH_PROVIDER_CITY", txtHPCity.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_HEALTH_PROVIDER_STATE_ID", extdlHPState.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_HEALTH_PROVIDER_STATE", txtHPState.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_HEALTH_PROVIDER_ZIP", txtHPZip.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_EMPLOYER_NAME", txtCEName1.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_EMPLOYER_ADDRESS", txtCEAddress.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_EMPLOYER_CITY", txtCECity.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_EMPLOYER_STATE_ID", extdlCEState.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_EMPLOYER_STATE", txtCEState.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_EMPLOYER_ZIP", txtCEZip.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_WCB_CASE_NUMBER", txtWCBCaseNo.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_WCB_AUTH_NUMBER", txtWCBAuthNo.Text);
            sqlCmd.Parameters.AddWithValue("@DT_OF_ACCIDENT", txtDtofAccident.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_CASE_NUMBER", txtCarrierCaseNo.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_EMPLOYER_ID", txtCarrierID.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_COUNTY_IN_WHICH_INJURY_OCCURRED", txtCountyInjOcc.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER", txtEmployer.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_STATE_NY_COUNTY_OF", txtCountyof.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_STATE_NY_COUNTY_SS", txtSS.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_STATE_NY_DULY_LICENSED_PERSON", txtLicensedHolder.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_SUBSCRIBED_AND_SWORN", txtSworn.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_SUBSCRIBED_AND_SWORN_DAY1", txtDayof1.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_SUBSCRIBED_AND_SWORN_DAY2", txtDayof2.Text);
            sqlCmd.Parameters.AddWithValue("@BT_PHYSICIANS_SIGN_SUCCESS", btPhy);
            sqlCmd.Parameters.AddWithValue("@SZ_PHYSICIANS_SIGN_PATH", txtPhysicianSignPath.Text);
            sqlCmd.Parameters.AddWithValue("@DT_PHYSICIANS_SIGN_DATE", txtPhySignDt.Text);
            sqlCmd.Parameters.AddWithValue("@BT_OTHERS_SIGN_SUCCESS", btOther);
            sqlCmd.Parameters.AddWithValue("@SZ_OTHERS_SIGN_PATH", txtOtherSignPath.Text);
            sqlCmd.Parameters.AddWithValue("@DT_OTHERS_SIGN_DATE", txtOtherSignDt.Text);
            sqlCmd.Parameters.AddWithValue("@BT_NOTARY_PUBLIC_SIGN_SUCCESS", btNotary);
            sqlCmd.Parameters.AddWithValue("@SZ_NOTARY_PUBLIC_SIGN_PATH", txtNotarySignPath.Text);
            sqlCmd.Parameters.AddWithValue("@DT_NOTARY_PUBLIC_SIGN_DATE", txtNotarySignDt.Text);

            int cnt = sqlCmd.ExecuteNonQuery();
            if (cnt > 0)
                saveFlag = true;
            else
                saveFlag = false;
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
            saveFlag = false;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private DataSet GetHPJ1Record()
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
            SqlCommand sqlCmd = new SqlCommand("SP_GET_HPJ1_RECORDS", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", txtCaseID.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", txtBillNo.Text);
            

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
    private void PrintPDF()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            sz_CompanyID = txtCompanyID.Text;
            sz_CaseID = txtCaseID.Text;
            sz_speciality = txtSpeciality.Text;
            sz_BillNo = txtBillNo.Text;

            Hp1DAO objDAO = new Hp1DAO();
            //objDAO = BindObject();

            Hp1DAL objHp1 = new Hp1DAL();            

            CmpName = objHp1.GetPDFPath(sz_CompanyID, sz_CaseID, sz_speciality);

            GenerateHp1Pdf objpdf = new GenerateHp1Pdf();

            FileName = "HP1_" + sz_BillNo + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff");

            txtFileName.Text = FileName;

            //tempPath = objpdf.generateHP1(sz_CompanyID, sz_CaseID, sz_BillNo, CmpName, FileName, objDAO);

            SaveLogicalPath();
            string BillPath = CmpName + "\\" + FileName + ".pdf";
            string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
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
            sz_CompanyID = txtCompanyID.Text;
            sz_CaseID = txtCaseID.Text;
            sz_speciality = txtSpeciality.Text;
            sz_BillNo = txtBillNo.Text;
            FileName = txtFileName.Text;

            //string strGenFileName = sz_BillNo + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff")+"MG2.pdf";
            string BillPath = CmpName + "\\" + FileName + ".pdf";

            string sz_UserName = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_NAME.ToString();
            sz_UserID = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
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
    private void FindNodeType()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            sz_CompanyID = txtCompanyID.Text;
            sz_CaseID = txtCaseID.Text;
            sz_speciality = txtSpeciality.Text;
            sz_BillNo = txtBillNo.Text;

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
}