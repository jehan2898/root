using AjaxControlToolkit;
using CUTEFORMCOLib;
using DevExpress.Web;
using ExtendedDropDownList;
using IntakePDFs;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bill_Sys_CO_Patient_IntakeSheet : Page, IRequiresSessionState
{
    
    private DataSet ds;

    private Bill_Sys_CaseObject _obhcaseno = new Bill_Sys_CaseObject();

    private string strConn;

    private SqlConnection conn;

    private SqlCommand cmd;

    private SqlDataAdapter sqlda;

    private SqlDataReader dr;

    private SqlDataReader dr1;

    private string CaseBarcodePath = "";

    private string caseId = "";

    private string companyId = "";

    private string Link = "";

    private string insuranceType = "";

    private bool flag;

    private Bill_Sys_NF3_Template objNF3Template;


    public Bill_Sys_CO_Patient_IntakeSheet()
    {
    }

    public void AddIntake()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int num = 0;
        int num1 = 0;
        if (this.chkReferral.Checked)
        {
            num = 1;
        }
        if (this.chkAdvertisement.Checked)
        {
            num1 = 1;
        }
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.conn = new SqlConnection(this.strConn);
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                this.conn.Open();
                this.cmd = new SqlCommand("SP_INSERT_INTAKE_DETAILS", this.conn);
                this.cmd.Parameters.AddWithValue("@SZ_CASE_ID", this.TXT_CASE_ID.Text);
                this.cmd.Parameters.AddWithValue("@BT_MARITAL_STATUS", this.rdbMStatus.SelectedValue);
                this.cmd.Parameters.AddWithValue("@BT_PREGNANT", this.rdbPregnant.SelectedValue);
                this.cmd.Parameters.AddWithValue("@BT_OCCUPATION", this.rdlOccupation.SelectedValue);
                this.cmd.Parameters.AddWithValue("@SZ_INJURIES_CASE", this.txtInjury.Text);
                this.cmd.Parameters.AddWithValue("@BT_REFERRAL", num);
                this.cmd.Parameters.AddWithValue("@SZ_REFFERED_BY_WHOM", this.txtReff.Text);
                this.cmd.Parameters.AddWithValue("@BT_ADVERTISE", num1);
                this.cmd.Parameters.AddWithValue("@SZ_WHICH_ADV", this.txtAdv.Text);
                this.cmd.Parameters.AddWithValue("@BT_INSURDS_RELATION", this.rblInsurdName.SelectedValue);
                this.cmd.Parameters.AddWithValue("@DT_INSURDS_DOB", this.txtInsurdsDOB.Text);
                this.cmd.Parameters.AddWithValue("@SZ_MEMBER_NO", this.txtMemberId.Text);
                this.cmd.Parameters.AddWithValue("@SZ_GROUP_NO", this.txtGroupNo.Text);
                this.cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.cmd.Parameters.AddWithValue("@flag", "ADD");
                this.cmd.Parameters.AddWithValue("@SZ_INSURD_NAME ", this.txtInsurdsName.Text);
                this.cmd.Parameters.AddWithValue("@SZ_SEC_INSURANCE_NAME ", this.txtInsCompName.Text);
                this.cmd.Parameters.AddWithValue("@SZ_SEC_INSURANCE_ADDRESS ", this.txtInsCmpAddress.Text);
                this.cmd.CommandType = CommandType.StoredProcedure;
                this.cmd.ExecuteNonQuery();
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
            
        }
        finally
        {
            this.conn.Close();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAOBPrintPDF_Click(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_AOBPDF_DATA");
        AOB aOB = new AOB();
        string str = aOB.GenerateAOBPDF(dataSet, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
        if (str == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "alert(Failed..)", true);
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str, "');"), true);
    }

    protected void btnDeclHippaPrint_Click(object sender, EventArgs e)
    {
        DeclarationHippaQueens declarationHippaQueen = new DeclarationHippaQueens();
        string sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string sZCOMPANYNAME = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        string text = this.TXT_CASE_ID.Text;
        string str = declarationHippaQueen.GenerateDeclarationPDF(text, sZCOMPANYID, sZCOMPANYNAME);
        if (str == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "alert(Failed To Load PDF..)", true);
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str, "');"), true);
    }

    protected void btnDeclHippaSave_Click(object sender, EventArgs e)
    {
        this.DeclHippaUpdatePatientGuarantor(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.txtdecsigndate.Text, this.txtprtintname.Text, this.txtdeclaraionname.Text);
    }

    protected void btnGuarantorPrint_Click(object sender, EventArgs e)
    {
        PaymentGuranteeForm paymentGuranteeForm = new PaymentGuranteeForm();
        string sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string sZCOMPANYNAME = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        string text = this.TXT_CASE_ID.Text;
        string str = paymentGuranteeForm.PrintPaymentGuaranteePDF(text, sZCOMPANYID, sZCOMPANYNAME);
        if (str == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "alert(Failed To Load PDF..)", true);
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str, "');"), true);
    }

    protected void btnGuarantorSave_Click(object sender, EventArgs e)
    {
        this.IntakeUpdatePatientGuarantor(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.txtSign.Text, this.txtIntials1.Text, this.txtIntials2.Text, this.txtIntials3.Text, this.txtIntials4.Text);
    }

    protected void btnHippaFillable_Click(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_HIPPA_PDF_DATA");
        HIPPAFillable hIPPAFillable = new HIPPAFillable();
        string str = hIPPAFillable.GenerateHIPPAFillable(dataSet, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
        if (str == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "alert(Failed..)", true);
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str, "');"), true);
    }

    protected void btnHIPPAPDF_Click(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_HIPPA_PDF_DATA");
        HIPPAQueeens hIPPAQueeen = new HIPPAQueeens();
        string str = hIPPAQueeen.GenerateHIPPAQueensPDF(dataSet, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
        if (str == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "alert(Failed..)", true);
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str, "');"), true);
    }

    protected void btnIntakePrintPDF_Click(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_INTAKEPDF_DATA");
        IntakePDF intakePDF = new IntakePDF();
        string str = intakePDF.GenerateIntakeNFWCPDF(dataSet, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
        if (str == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "alert(Failed..)", true);
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str, "');"), true);
    }

    protected void btnLienPDF_Click(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_LIENPDF_DATA");
        MedicalLien medicalLien = new MedicalLien();
        string str = medicalLien.GenerateLienPDF(dataSet, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
        if (str == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "alert(Failed..)", true);
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str, "');"), true);
    }

    protected void btnPrivateintakePDF_Click(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_PRIVATE_INTAKE_PDF_DATA");
        PrivateINTAKE privateINTAKE = new PrivateINTAKE();
        string str = privateINTAKE.GeneratePrivateIntakePDF(dataSet, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
        if (str == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "alert(Failed..)", true);
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str, "');"), true);
    }

    protected void btnSavePrint_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_CheckoutBO billSysCheckoutBO = new Bill_Sys_CheckoutBO();
        this.objNF3Template = new Bill_Sys_NF3_Template();
        string sZCOMAPNYID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
        string sZCASEID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
        try
        {
            this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
            this.conn = new SqlConnection(this.strConn);
            DataSet dataSet = new DataSet();
            try
            {
                try
                {
                    this.conn.Open();
                    this.cmd = new SqlCommand("SP_MST_AOB_NEW", this.conn);
                    this.cmd.Parameters.AddWithValue("@SZ_CASE_ID", this.TXT_CASE_ID.Text);
                    this.cmd.Parameters.AddWithValue("@SZ_OCA_NUMBER", this.TXT_OCA_OFFICIAL_FORM_NUMBER.Text);
                    this.cmd.Parameters.AddWithValue("@SZ_ASSIGN_TO", this.TXT_HERE_BY_ASSIGN_TO.Text);
                    this.cmd.Parameters.AddWithValue("@SZ_PROVIDER_ID", this.extddlOffice.Text);
                    this.cmd.Parameters.AddWithValue("@SZ_PROVIDER_NAME", this.extddlOffice.Selected_Text);
                    this.cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.cmd.Parameters.AddWithValue("@FLAG", "ADD");
                    this.cmd.CommandType = CommandType.StoredProcedure;
                    this.cmd.ExecuteNonQuery();
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
            }
            finally
            {
                this.conn.Close();
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

    protected void btnUpdateHippa_OnClick(object sender, EventArgs e)
    {
        string str = "";
        string str1 = "";
        string str2 = "";
        string str3 = "";
        string str4 = "";
        string str5 = "";
        try
        {
            str = (!this.chkHipaaMRF.Checked ? "0" : "1");
            str1 = (!this.chkHippaMRF2.Checked ? "0" : "1");
            str2 = (!this.chkHippaMRF3.Checked ? "0" : "1");
            str3 = (!this.chkini.Checked ? "0" : "1");
            str4 = (!this.chk10.Checked ? "0" : "1");
            str5 = (!this.chkOther.Checked ? "0" : "1");
            this.IntakeUpdateHippa(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.txtHipaaHealthProvider.Text, this.txtHipaaWhomeInfoSend.Text, str, this.txtMedialRecordDtFrom.Text, this.txtMedialRecordDtTo.Text, str1, str2, this.txtOther1.Text, this.txtOther2.Text, this.txtIncludeAlco.Text, this.txtIncludeMen.Text, this.txtIncludeHIV.Text, str3, this.txtInitials.Text, this.txtHelthCareProvider.Text, this.txtAttoenry.Text, str4, str5, this.txtExpiry.Text, this.txtPersonName.Text, this.txtAuthority.Text);
            this.w3loadHippaDetails();
        }
        catch (Exception exception)
        {
        }
    }

    protected void btnUpdateIntake_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int num = 0;
        int num1 = 0;
        if (this.chkReferral.Checked)
        {
            num = 1;
        }
        if (this.chkAdvertisement.Checked)
        {
            num1 = 1;
        }
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.conn = new SqlConnection(this.strConn);
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                this.conn.Open();
                this.cmd = new SqlCommand("SP_INSERT_INTAKE_DETAILS", this.conn);
                this.cmd.Parameters.AddWithValue("@SZ_CASE_ID", this.TXT_CASE_ID.Text);
                this.cmd.Parameters.AddWithValue("@BT_MARITAL_STATUS", this.rdbMStatus.SelectedValue);
                this.cmd.Parameters.AddWithValue("@BT_PREGNANT", this.rdbPregnant.SelectedValue);
                this.cmd.Parameters.AddWithValue("@BT_OCCUPATION", this.rdlOccupation.SelectedValue);
                this.cmd.Parameters.AddWithValue("@SZ_INJURIES_CASE", this.txtInjury.Text);
                this.cmd.Parameters.AddWithValue("@BT_REFERRAL", num);
                this.cmd.Parameters.AddWithValue("@SZ_REFFERED_BY_WHOM", this.txtReff.Text);
                this.cmd.Parameters.AddWithValue("@BT_ADVERTISE", num1);
                this.cmd.Parameters.AddWithValue("@SZ_WHICH_ADV", this.txtAdv.Text);
                this.cmd.Parameters.AddWithValue("@BT_INSURDS_RELATION", this.rblInsurdName.SelectedValue);
                this.cmd.Parameters.AddWithValue("@DT_INSURDS_DOB", this.txtInsurdsDOB.Text);
                this.cmd.Parameters.AddWithValue("@SZ_MEMBER_NO", this.txtMemberId.Text);
                this.cmd.Parameters.AddWithValue("@SZ_GROUP_NO", this.txtGroupNo.Text);
                this.cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.cmd.Parameters.AddWithValue("@flag", "ADD");
                this.cmd.Parameters.AddWithValue("@SZ_INSURD_NAME ", this.txtInsurdsName.Text);
                this.cmd.Parameters.AddWithValue("@SZ_SEC_INSURANCE_NAME ", this.txtInsCompName.Text);
                this.cmd.Parameters.AddWithValue("@SZ_SEC_INSURANCE_ADDRESS ", this.txtInsCmpAddress.Text);
                this.cmd.CommandType = CommandType.StoredProcedure;
                this.cmd.ExecuteNonQuery();
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
            
        }
        finally
        {
            this.conn.Close();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnw5update_onclick(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.conn = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.conn.Open();
                this.cmd = new SqlCommand("SP_SAVE_PRIVATE_INTAKE", this.conn);
                this.cmd.Parameters.AddWithValue("@SZ_CASE_ID", this.TXT_CASE_ID.Text);
                this.cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.cmd.Parameters.AddWithValue("@BT_MARITAL_STATUS", this.rdbw5maritalstatus.SelectedValue);
                this.cmd.Parameters.AddWithValue("@SZ_PRI_RELATIONSHIP", this.txtw5prirelationship.Text);
                this.cmd.Parameters.AddWithValue("@SZ_SEC_RELATIONSHIP", this.txtw5secrelationship.Text);
                this.cmd.Parameters.AddWithValue("@SZ_INSURANCE_NAME", this.txtw5secInsuranceName.Text);
                this.cmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", this.secInsuranceId.Value);
                this.cmd.Parameters.AddWithValue("@SZ_SEC_POLICY_HOLDER", this.txtw5secpolicyholder.Text);
                this.cmd.Parameters.AddWithValue("@MEMBER_ID", this.txtprivatememberid.Text);
                this.cmd.Parameters.AddWithValue("@MEDICARE#", this.txtprivatemedicare.Text);
                this.cmd.CommandType = CommandType.StoredProcedure;
                this.cmd.ExecuteNonQuery();
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
        }
        finally
        {
            this.conn.Close();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlInsuranceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (this.ddlInsuranceType.SelectedItem == null)
        {
            this.insuranceType = "SEC";
        }
        else
        {
            string text = this.ddlInsuranceType.SelectedItem.Text;
            if (text == "Major Medical")
            {
                this.insuranceType = "MAJ";
            }
            else if (text != "Private")
            {
                this.insuranceType = "SEC";
            }
            else
            {
                this.insuranceType = "PRI";
            }
        }
        DataSet secondaryInsuranceDetail = this.GetSecondaryInsuranceDetail(this.caseId, this.companyId, "LIST");
        if (secondaryInsuranceDetail == null)
        {
            this.txtw5secInsuranceName.Text = "";
            return;
        }
        if (secondaryInsuranceDetail.Tables[0].Rows.Count <= 0)
        {
            this.txtw5secInsuranceName.Text = "";
            return;
        }
        string str = this.insuranceType;
        for (int i = 0; i < secondaryInsuranceDetail.Tables[0].Rows.Count; i++)
        {
            if (str == secondaryInsuranceDetail.Tables[0].Rows[i]["SZ_INSURANCE_TYPE"].ToString())
            {
                this.secInsuranceId.Value = secondaryInsuranceDetail.Tables[0].Rows[i]["SZ_INSURANCE_ID"].ToString();
                string str1 = string.Concat("select SZ_INSURANCE_NAME from MST_INSURANCE_COMPANY where SZ_INSURANCE_ID = '", this.secInsuranceId.Value, "'");
                try
                {
                    try
                    {
                        string str2 = ConfigurationManager.AppSettings["Connection_String"].ToString();
                        this.conn = new SqlConnection(str2);
                        this.conn.Open();
                        this.cmd = new SqlCommand(str1, this.conn);
                        this.dr = this.cmd.ExecuteReader();
                        while (this.dr.Read())
                        {
                            if (this.dr["SZ_INSURANCE_NAME"] == DBNull.Value)
                            {
                                this.txtw5secInsuranceName.Text = "";
                            }
                            else
                            {
                                this.txtw5secInsuranceName.Text = this.dr["SZ_INSURANCE_NAME"].ToString();
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
                }
                finally
                {
                    if (this.conn.State == ConnectionState.Open)
                    {
                        this.conn.Close();
                    }
                }
                this.flag = true;
                return;
            }
            this.flag = false;
            this.txtw5secInsuranceName.Text = "";
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlw1InsuranceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlw1InsuranceType.SelectedItem == null)
        {
            this.insuranceType = "SEC";
        }
        else
        {
            string text = this.ddlw1InsuranceType.SelectedItem.Text;
            if (text == "Primary")
            {
                this.insuranceType = "PRIM";
            }
            else if (text == "Private")
            {
                this.insuranceType = "PRI";
            }
            else if (text != "Major Medical")
            {
                this.insuranceType = "SEC";
            }
            else
            {
                this.insuranceType = "MAJ";
            }
        }
        if (this.insuranceType == "PRIM")
        {
            string str = ConfigurationManager.AppSettings["Connection_String"].ToString();
            this.conn = new SqlConnection(str);
            DataSet dataSet = new DataSet();
            string str1 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID.ToString();
            DataSet patientDetailList = this.GetPatientDetailList(str1, "LIST");
            patientDetailList = this.GetPatientDetailList(str1, "INSURANCE");
            this.txtInsCompName.Text = patientDetailList.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
            this.txtInsCmpAddress.Text = patientDetailList.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
            return;
        }
        DataSet secondaryInsuranceDetail = this.GetSecondaryInsuranceDetail(this.caseId, this.companyId, "LIST");
        if (secondaryInsuranceDetail != null)
        {
            if (secondaryInsuranceDetail.Tables[0].Rows.Count > 0)
            {
                string str2 = this.insuranceType;
                for (int i = 0; i < secondaryInsuranceDetail.Tables[0].Rows.Count; i++)
                {
                    if (str2 == secondaryInsuranceDetail.Tables[0].Rows[i]["SZ_INSURANCE_TYPE"].ToString())
                    {
                        this.secInsuranceId.Value = secondaryInsuranceDetail.Tables[0].Rows[i]["SZ_INSURANCE_ID"].ToString();
                        string str3 = string.Concat("select * from MST_INSURANCE_COMPANY where SZ_INSURANCE_ID = '", this.secInsuranceId.Value, "'");
                        try
                        {
                            try
                            {
                                string str4 = ConfigurationManager.AppSettings["Connection_String"].ToString();
                                this.conn = new SqlConnection(str4);
                                this.conn.Open();
                                this.cmd = new SqlCommand(str3, this.conn);
                                this.dr = this.cmd.ExecuteReader();
                                while (this.dr.Read())
                                {
                                    if (this.dr["SZ_INSURANCE_NAME"] == DBNull.Value)
                                    {
                                        this.txtInsCompName.Text = "";
                                    }
                                    else
                                    {
                                        this.txtInsCompName.Text = this.dr["SZ_INSURANCE_NAME"].ToString();
                                    }
                                    if (this.dr["SZ_INSURANCE_ADDRESS"] == DBNull.Value)
                                    {
                                        this.txtInsCmpAddress.Text = "";
                                    }
                                    else
                                    {
                                        this.txtInsCmpAddress.Text = this.dr["SZ_INSURANCE_ADDRESS"].ToString();
                                    }
                                }
                            }
                            catch (SqlException sqlException)
                            {
                                sqlException.Message.ToString();
                            }
                        }
                        finally
                        {
                            if (this.conn.State == ConnectionState.Open)
                            {
                                this.conn.Close();
                            }
                        }
                        this.flag = true;
                        return;
                    }
                    this.txtInsCompName.Text = "";
                    this.txtInsCmpAddress.Text = "";
                    this.flag = false;
                }
                return;
            }
            this.txtInsCompName.Text = "";
            this.txtInsCmpAddress.Text = "";
        }
    }

    public void DeclHippaUpdatePatientGuarantor(string szcaseid, string szcompanyid, string sz_signature_date, string sz_print_name, string sz_declaration_name)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.conn = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.conn.Open();
                this.cmd = new SqlCommand("SP_SAVE_PATIENT_GUARANTOR_DECLARATION_HIPPA", this.conn);
                this.cmd.Parameters.AddWithValue("@SZ_CASE_ID", szcaseid);
                this.cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);
                this.cmd.Parameters.AddWithValue("@DT_SIGNATURE_DATE", sz_signature_date);
                this.cmd.Parameters.AddWithValue("@SZ_PRINT_NAME", sz_print_name);
                this.cmd.Parameters.AddWithValue("@SZ_DECLARATION_NAME", sz_declaration_name);
                this.cmd.CommandType = CommandType.StoredProcedure;
                this.cmd.ExecuteNonQuery();
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
        }
        finally
        {
            this.conn.Close();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetIntakeDetails(string sCaseId, string sCompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_INSERT_INTAKE_DETAILS", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", sCaseId);
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sCompanyID);
                sqlCommand.Parameters.AddWithValue("@flag", "LIST");
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                this.ds = new DataSet();
                sqlDataAdapter.Fill(this.ds);
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
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return this.ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetPatientDetailList(string patientid, string Flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_GET_PATIENT_INTAKE_DETAILS_LIST", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
                sqlCommand.Parameters.AddWithValue("@FLAG", Flag);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                this.ds = new DataSet();
                sqlDataAdapter.Fill(this.ds);
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
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return this.ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void GetPatientDetails(string Patient_ID)
    {
        //string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        //using (Utils utility = new Utils())
        //{
        //    utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        //}
        DataSet patientDetailList = this.GetPatientDetailList(Patient_ID, "LIST");
        try
        {
            if (patientDetailList.Tables[0].Rows.Count > 0)
            {
                this.txtPatientFirstName.Text = string.Concat(patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), " ", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                this.txtDOB.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                Label label = this.txtAddress;
                string[] str = new string[] { patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(5).ToString(), ",", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(6).ToString(), ",", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(7).ToString(), "  ", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() };
                label.Text = string.Concat(str);
                this.txtGendar.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                this.TXT_PATIENT_AGE.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                this.txtSocialSecurity.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                this.hippapatient.Text = string.Concat(patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), " ", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                this.hippadob.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                this.txthippassn.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                Label label1 = this.lblhippaddress;
                string[] strArrays = new string[] { patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(5).ToString(), ",", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(6).ToString(), ",", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(7).ToString(), "  ", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() };
                label1.Text = string.Concat(strArrays);
                this.lblhippatodaysdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                this.txtlienpatientname.Text = string.Concat(patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), " ", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                this.txtw5patientname.Text = string.Concat(patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), " ", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                this.txtw5ssn.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                this.txtw5mailingaddr.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                this.txtw5zip.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                this.txtw5state.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                this.txtw5city.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                this.txtw5dob.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                if (patientDetailList.Tables[0].Rows[0]["SZ_GENDER"].ToString().ToLower() != "female")
                {
                    this.rdbw5sex.SelectedIndex = 0;
                }
                else
                {
                    this.rdbw5sex.SelectedIndex = 1;
                }
                this.txtw5homeph.Text = patientDetailList.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
                this.txtw5cellph.Text = patientDetailList.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString();
                this.txtw5pripolicyholder.Text = patientDetailList.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString();
                this.lblinprivate.Text = string.Concat(patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), " ", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                this.lblmedicarepatient.Text = string.Concat(patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), " ", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                this.lbldeclartionssn.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                this.txtPatientName.Text = string.Concat(patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), " ", patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                this.txtSSN.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                this.txtMailingAddress.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                this.txtZip.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                this.txtState.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                this.txtCity.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                this.txtDtBirth.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                this.txtpindob.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                this.txtsecondpindob.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                this.txtAge.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                this.txtintakeHPH.Text = patientDetailList.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
                this.txtintakeCPH.Text = patientDetailList.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString();
                if (patientDetailList.Tables[0].Rows[0]["SZ_GENDER"].ToString().ToLower() != "female")
                {
                    this.rdbSex.SelectedIndex = 0;
                }
                else
                {
                    this.rdbSex.SelectedIndex = 1;
                }
                this.txtWCBNo.Text = Convert.ToString(patientDetailList.Tables[0].Rows[0]["SZ_WCB_NO"]);
                patientDetailList.Tables.Clear();
                patientDetailList = this.GetPatientDetailList(Patient_ID, "ACCIDENT");
                this.txtDateOfInjury.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                patientDetailList.Tables.Clear();
                patientDetailList = this.GetPatientDetailList(Patient_ID, "INSURANCE");
                this.txtTodayDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                this.txtClaimniNo.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                this.txtPolicyNo.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                this.txtAttorneyName.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                this.txtAttorneyPhn.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                this.txtInsCompanyName.Text = patientDetailList.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                this.txtlienattorneyname.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                this.txtw5priinsname.Text = patientDetailList.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                DataSet dataSet = new DataSet();
                dataSet = this.Getw5pPrivateIntakeDetails(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                string str1 = "";
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count <= 0)
                    {
                        this.txtInsCompName.Text = patientDetailList.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                        this.txtInsCmpAddress.Text = patientDetailList.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                        this.ddlw1InsuranceType.SelectedIndex = 1;
                    }
                    else
                    {
                        str1 = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                        string str2 = ConfigurationManager.AppSettings["Connection_String"].ToString();
                        this.conn = new SqlConnection(str2);
                        string str3 = string.Concat("select * from TXN_PRIVATE_INTAKE where SZ_INSURANCE_ID = '", str1, "'");
                        try
                        {
                            try
                            {
                                this.conn.Open();
                                this.cmd = new SqlCommand(str3, this.conn);
                                this.dr = this.cmd.ExecuteReader();
                                while (this.dr.Read())
                                {
                                    if (this.dr["SZ_INSURANCE_NAME"] != DBNull.Value)
                                    {
                                        this.txtInsCompName.Text = this.dr["SZ_INSURANCE_NAME"].ToString();
                                    }
                                    if (this.dr["SZ_SEC_INSURANCE_ADDRESS"] == DBNull.Value)
                                    {
                                        continue;
                                    }
                                    this.txtInsCmpAddress.Text = this.dr["SZ_SEC_INSURANCE_ADDRESS"].ToString();
                                }
                            }
                            catch (SqlException sqlException)
                            {
                                sqlException.Message.ToString();
                            }
                        }
                        finally
                        {
                            if (this.conn.State == ConnectionState.Open)
                            {
                                this.conn.Close();
                            }
                        }
                    }
                }
                string[] strArrays1 = new string[] { "select SZ_INSURANCE_TYPE from MST_SEC_INSURANCE_DETAIL where SZ_INSURANCE_ID = '", str1, "' and SZ_CASE_ID='", this.caseId, "' and SZ_COMPANY_ID='", this.companyId, "'" };
                string str4 = string.Concat(strArrays1);
                try
                {
                    try
                    {
                        this.conn.Open();
                        this.cmd = new SqlCommand(str4, this.conn);
                        this.dr = this.cmd.ExecuteReader();
                        while (this.dr.Read())
                        {
                            if (this.dr["SZ_INSURANCE_TYPE"] == DBNull.Value)
                            {
                                continue;
                            }
                            if (this.dr["SZ_INSURANCE_TYPE"].ToString() == "SEC")
                            {
                                this.ddlw1InsuranceType.SelectedIndex = 2;
                            }
                            else if (this.dr["SZ_INSURANCE_TYPE"].ToString() == "MAJ")
                            {
                                this.ddlw1InsuranceType.SelectedIndex = 3;
                            }
                            else if (this.dr["SZ_INSURANCE_TYPE"].ToString() != "PRI")
                            {
                                this.ddlw1InsuranceType.SelectedIndex = 1;
                            }
                            else
                            {
                                this.ddlw1InsuranceType.SelectedIndex = 4;
                            }
                        }
                    }
                    catch (SqlException sqlException1)
                    {
                        sqlException1.Message.ToString();
                    }
                }
                finally
                {
                    if (this.conn.State == ConnectionState.Open)
                    {
                        this.conn.Close();
                    }
                }
                if (this.txtInsCompanyName.Text == this.txtInsCompName.Text)
                {
                    this.ddlw1InsuranceType.SelectedIndex = 1;
                }
                if (Convert.ToString(patientDetailList.Tables[0].Rows[0]["Date_of_Accident"]) != "")
                {
                    TextBox textBox = this.txtliendateofaccident;
                    DateTime dateTime = Convert.ToDateTime(patientDetailList.Tables[0].Rows[0]["Date_of_Accident"].ToString());
                    textBox.Text = dateTime.ToString("MM/dd/yyyy");
                }
                if (Convert.ToString(patientDetailList.Tables[0].Rows[0]["Date_of_Accident"]) != "")
                {
                    TextBox textBox1 = this.txtDOA;
                    DateTime dateTime1 = Convert.ToDateTime(patientDetailList.Tables[0].Rows[0]["Date_of_Accident"].ToString());
                    textBox1.Text = dateTime1.ToString("MM/dd/yyyy");
                }
                if (patientDetailList.Tables[0].Rows[0]["Case_type"].ToString().ToLower().Trim() == "nofault" || patientDetailList.Tables[0].Rows[0]["Case_type"].ToString().ToLower().Trim() == "no fault")
                {
                    this.rblCaseType.Text = "1";
                }
                else if (patientDetailList.Tables[0].Rows[0]["Case_type"].ToString().ToLower().Trim() == "workers compensation" || patientDetailList.Tables[0].Rows[0]["Case_type"].ToString().ToLower().Trim() == "workers comp")
                {
                    this.rblCaseType.Text = "0";
                }
                else
                {
                    this.rblCaseType.Text = "2";
                }
                if (Convert.ToString(patientDetailList.Tables[0].Rows[0]["ADJUSTER_PHONE"]) != "")
                {
                    this.txtAdjusterPhn.Text = patientDetailList.Tables[0].Rows[0]["ADJUSTER_PHONE"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //using (Utils utility = new Utils())
            //{
            //    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            //}
            //string str2 = "Error Request=" + id + ".Please share with Technical support.";
            //base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        ////Method End
        //using (Utils utility = new Utils())
        //{
        //    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        //}
    }

    protected DataSet GetPDFData(string sCaseID, string sCompanyID, string sProcedureName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sProcedureName, sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", sCaseID);
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sCompanyID);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                this.ds = new DataSet();
                sqlDataAdapter.Fill(this.ds);
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
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return this.ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetSecondaryInsuranceDetail(string caseId, string companyId, string Flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.conn = new SqlConnection(str);
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                this.conn.Open();
                this.cmd = new SqlCommand("SP_INSERT_SECINSURANCE_TYPE", this.conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.cmd.Parameters.AddWithValue("@SZ_CASEID", caseId);
                this.cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
                this.cmd.Parameters.AddWithValue("@Flag", "LIST");
                this.cmd.ExecuteNonQuery();
                (new SqlDataAdapter(this.cmd)).Fill(dataSet);
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
        }
        finally
        {
            if (this.conn.State == ConnectionState.Open)
            {
                this.conn.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet Getw3pHippaDetails(string sCaseId, string sCompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_GET_W3_HIPPA_DETAILS", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", sCaseId);
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sCompanyID);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                this.ds = new DataSet();
                sqlDataAdapter.Fill(this.ds);
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
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return this.ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet Getw5pPrivateIntakeDetails(string sCaseId, string sCompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_GET_PRIVATE_INTAKE", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", sCaseId);
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sCompanyID);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                this.ds = new DataSet();
                sqlDataAdapter.Fill(this.ds);
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
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return this.ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void InsuranceInformation(string Patiend_ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataSet patientDetailList = this.GetPatientDetailList(Patiend_ID, "INSURANCE");
            this.TXT_INSURANCE_COMPANY_NAME.Text = patientDetailList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            this.TXT_INSURANCE_COMPANY_NAME1.Text = this.TXT_INSURANCE_COMPANY_NAME.Text;
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

    public void IntakeUpdateHippa(string szcaseid, string szcompanyid, string sznameandaddr, string szwhomthissend, string bt_med_rec1, string dt_medi_recfrom, string dt_medi_recto, string bt_med_rec2, string bt_med_rec3, string szother1, string szother2, string szincludealco, string szincludemen, string szincludehiv, string btinitial, string szinitial, string szhealthcareprov, string szattorney, string btreqofindidual, string btother, string dtauthwillexpire, string szpersonname, string szAuther)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.conn = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.conn.Open();
                this.cmd = new SqlCommand("SP_SAVE_HIPPA_INTAKE", this.conn);
                this.cmd.Parameters.AddWithValue("@SZ_CASE_ID", szcaseid);
                this.cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);
                this.cmd.Parameters.AddWithValue("@SZ_NAME_AND_ADDRESS", sznameandaddr);
                this.cmd.Parameters.AddWithValue("@SZ_WHOM_THIS_SEND", szwhomthissend);
                this.cmd.Parameters.AddWithValue("@BT_MEDIACL_RECORED_1", bt_med_rec1);
                this.cmd.Parameters.AddWithValue("@DT_MEDICAL_RECORED_FORM", dt_medi_recfrom);
                this.cmd.Parameters.AddWithValue("@DT_MEDICAL_RECORED_TO", dt_medi_recto);
                this.cmd.Parameters.AddWithValue("@BT_MEDIACL_RECORED_2", bt_med_rec2);
                this.cmd.Parameters.AddWithValue("@BT_MEDIACL_RECORED_3", bt_med_rec3);
                this.cmd.Parameters.AddWithValue("@SZ_OTHER_1", szother1);
                this.cmd.Parameters.AddWithValue("@SZ_OTHER_2", szother2);
                this.cmd.Parameters.AddWithValue("@SZ_INCLUDE_ALCO", szincludealco);
                this.cmd.Parameters.AddWithValue("@SZ_INCLUDE_MEN", szincludemen);
                this.cmd.Parameters.AddWithValue("@SZ_INCLUDE_HIV", szincludehiv);
                this.cmd.Parameters.AddWithValue("@BT_INITIALS", btinitial);
                this.cmd.Parameters.AddWithValue("@SZ_INITIALS", szinitial);
                this.cmd.Parameters.AddWithValue("@SZ_HELTH_CARE_PROVIDER", szhealthcareprov);
                this.cmd.Parameters.AddWithValue("@SZ_ATTOENRY", szattorney);
                this.cmd.Parameters.AddWithValue("@BT_AT_REQUEST_OF_INDIVIDUAL", btreqofindidual);
                this.cmd.Parameters.AddWithValue("@BT_OTHER", btother);
                this.cmd.Parameters.AddWithValue("@DT_AUTHORIZATION_WILL_EXPIRE", dtauthwillexpire);
                this.cmd.Parameters.AddWithValue("@SZ_PERSON_NAME", szpersonname);
                this.cmd.Parameters.AddWithValue("@SZ_AUTHOR", szAuther);
                this.cmd.CommandType = CommandType.StoredProcedure;
                this.cmd.ExecuteNonQuery();
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
            
        }
        finally
        {
            this.conn.Close();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void IntakeUpdatePatientGuarantor(string szcaseid, string szcompanyid, string sz_signature_date, string sz_initial_date1, string sz_initial_date2, string sz_initial_date3, string sz_initial_date4)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.conn = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.conn.Open();
                this.cmd = new SqlCommand("SP_SAVE_PATIENT_GUARANTOR_BILLING_AGREEMENT", this.conn);
                this.cmd.Parameters.AddWithValue("@SZ_CASE_ID", szcaseid);
                this.cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);
                this.cmd.Parameters.AddWithValue("@DT_SIGNATURE_DATE", sz_signature_date);
                this.cmd.Parameters.AddWithValue("@SZ_INIIAL_DATE1", sz_initial_date1);
                this.cmd.Parameters.AddWithValue("@SZ_INIIAL_DATE2", sz_initial_date2);
                this.cmd.Parameters.AddWithValue("@SZ_INIIAL_DATE3", sz_initial_date3);
                this.cmd.Parameters.AddWithValue("@SZ_INIIAL_DATE4", sz_initial_date4);
                this.cmd.CommandType = CommandType.StoredProcedure;
                this.cmd.ExecuteNonQuery();
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
        }
        finally
        {
            this.conn.Close();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    

    protected void lnkAOB_Click(object sender, EventArgs e)
    {
        this.Wizard5.Visible = false;
        this.Wizard2.Visible = true;
        this.Wizard1.Visible = false;
        this.Wizard3.Visible = false;
        this.Wizard4.Visible = false;
        this.Wizard8.Visible = false;
        this.Wizard6.Visible = false;
    }

    protected void lnkDeclaration_hippa_Click(object sender, EventArgs e)
    {
        this.Wizard8.Visible = true;
        this.Wizard4.Visible = false;
        this.Wizard2.Visible = false;
        this.Wizard1.Visible = false;
        this.Wizard3.Visible = false;
        this.Wizard5.Visible = false;
        this.Wizard6.Visible = false;
    }

    protected void lnkHippa_Click(object sender, EventArgs e)
    {
        this.Wizard5.Visible = false;
        this.Wizard3.Visible = true;
        this.Wizard2.Visible = false;
        this.Wizard1.Visible = false;
        this.Wizard4.Visible = false;
        this.Wizard8.Visible = false;
        this.Wizard6.Visible = false;
    }

    protected void lnkIntake_Click(object sender, EventArgs e)
    {
        this.Wizard5.Visible = false;
        this.Wizard1.Visible = true;
        this.Wizard2.Visible = false;
        this.Wizard3.Visible = false;
        this.Wizard4.Visible = false;
        this.Wizard8.Visible = false;
        this.Wizard6.Visible = false;
    }

    protected void lnkLIEN_Click(object sender, EventArgs e)
    {
        this.Wizard5.Visible = false;
        this.Wizard4.Visible = true;
        this.Wizard2.Visible = false;
        this.Wizard1.Visible = false;
        this.Wizard3.Visible = false;
        this.Wizard5.Visible = false;
        this.Wizard8.Visible = false;
        this.Wizard6.Visible = false;
    }

    protected void lnklnkPrintAll_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        char[] chrArray;
        string[] sZCOMPANYNAME;
        this.AddIntake();
        string str = ConfigurationSettings.AppSettings["BASEPATH"].ToString();
        ArrayList arrayLists = new ArrayList();
        try
        {
            DataSet dataSet = new DataSet();
            dataSet = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_INTAKEPDF_DATA");
            IntakePDF intakePDF = new IntakePDF();
            string str1 = intakePDF.GenerateIntakeNFWCPDF(dataSet, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
            str1 = str1.Replace("\\", "/");
            chrArray = new char[] { '/' };
            string[] strArrays = str1.Split(chrArray);
            int length = (int)strArrays.Length;
            sZCOMPANYNAME = new string[] { str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.TXT_CASE_ID.Text, "/Packet Document/", strArrays[length - 1].ToString() };
            str1 = string.Concat(sZCOMPANYNAME);
            this.Session["INTAKEWC"] = str1;
            arrayLists.Add(str1);
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
        try
        {
            DataSet pDFData = new DataSet();
            pDFData = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_AOBPDF_DATA");
            AOB aOB = new AOB();
            string str2 = aOB.GenerateAOBPDF(pDFData, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
            str2 = str2.Replace("\\", "/");
            string[] strArrays1 = str2.Split(new char[] { '/' });
            int num = (int)strArrays1.Length;
            string[] sZCOMPANYNAME1 = new string[] { str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.TXT_CASE_ID.Text, "/Packet Document/", strArrays1[num - 1].ToString() };
            str2 = string.Concat(sZCOMPANYNAME1);
            this.Session["AOB"] = str2;
            arrayLists.Add(str2);
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
        try
        {
            DataSet dataSet1 = new DataSet();
            dataSet1 = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_HIPPA_PDF_DATA");
            HIPPAQueeens hIPPAQueeen = new HIPPAQueeens();
            string str3 = hIPPAQueeen.GenerateHIPPAQueensPDF(dataSet1, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
            str3 = str3.Replace("\\", "/");
            string[] strArrays2 = str3.Split(new char[] { '/' });
            int length1 = (int)strArrays2.Length;
            string[] sZCOMPANYNAME2 = new string[] { str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.TXT_CASE_ID.Text, "/Packet Document/", strArrays2[length1 - 1].ToString() };
            str3 = string.Concat(sZCOMPANYNAME2);
            this.Session["HIPPAQueen"] = str3;
            arrayLists.Add(str3);
            HIPPAFillable hIPPAFillable = new HIPPAFillable();
            string str4 = hIPPAFillable.GenerateHIPPAFillable(dataSet1, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
            str4 = str3.Replace("\\", "/");
            string[] strArrays3 = str4.Split(new char[] { '/' });
            int num1 = (int)strArrays3.Length;
            string[] sZCOMPANYNAME3 = new string[] { str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.TXT_CASE_ID.Text, "/Packet Document/", strArrays3[num1 - 1].ToString() };
            str4 = string.Concat(sZCOMPANYNAME3);
            this.Session["HIPPA"] = str4;
            arrayLists.Add(str4);
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
        try
        {
            DataSet pDFData1 = new DataSet();
            pDFData1 = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_LIENPDF_DATA");
            MedicalLien medicalLien = new MedicalLien();
            string str5 = medicalLien.GenerateLienPDF(pDFData1, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
            str5 = str5.Replace("\\", "/");
            string[] strArrays4 = str5.Split(new char[] { '/' });
            int length2 = (int)strArrays4.Length;
            string[] sZCOMPANYNAME4 = new string[] { str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.TXT_CASE_ID.Text, "/Packet Document/", strArrays4[length2 - 1].ToString() };
            str5 = string.Concat(sZCOMPANYNAME4);
            this.Session["Lien"] = str5;
            arrayLists.Add(str5);
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
        try
        {
            DataSet dataSet2 = new DataSet();
            dataSet2 = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_PRIVATE_INTAKE_PDF_DATA");
            PrivateINTAKE privateINTAKE = new PrivateINTAKE();
            string str6 = privateINTAKE.GeneratePrivateIntakePDF(dataSet2, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
            str6 = str6.Replace("\\", "/");
            string[] strArrays5 = str6.Split(new char[] { '/' });
            int num2 = (int)strArrays5.Length;
            sZCOMPANYNAME = new string[] { str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.TXT_CASE_ID.Text, "/Packet Document/", strArrays5[num2 - 1].ToString() };
            str6 = string.Concat(sZCOMPANYNAME);
            this.Session["PrivateIntake"] = str6;
            arrayLists.Add(str6);
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
        try
        {
            DataSet pDFData2 = new DataSet();
            pDFData2 = this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_MST_PATIENT_GUARANTOR_BILLING_AGREEMENT");
            PaymentGuranteeForm paymentGuranteeForm = new PaymentGuranteeForm();
            string str7 = paymentGuranteeForm.print(pDFData2, this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
            str7 = str7.Replace("\\", "/");
            chrArray = new char[] { '/' };
            string[] strArrays6 = str7.Split(chrArray);
            int length3 = (int)strArrays6.Length;
            sZCOMPANYNAME = new string[] { str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.TXT_CASE_ID.Text, "/Packet Document/", strArrays6[length3 - 1].ToString() };
            str7 = string.Concat(sZCOMPANYNAME);
            this.Session["PaymentGuarantee"] = str7;
            arrayLists.Add(str7);
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
        try
        {
            DataSet dataSet3 = new DataSet();
            this.GetPDFData(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SP_GET_PRIVATE_INTAKE_PDF_DATA");
            DeclarationHippaQueens declarationHippaQueen = new DeclarationHippaQueens();
            string str8 = declarationHippaQueen.GenerateDeclarationPDF(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
            str8 = str8.Replace("\\", "/");
            chrArray = new char[] { '/' };
            string[] strArrays7 = str8.Split(chrArray);
            int num3 = (int)strArrays7.Length;
            sZCOMPANYNAME = new string[] { str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.TXT_CASE_ID.Text, "/Packet Document/", strArrays7[num3 - 1].ToString() };
            str8 = string.Concat(sZCOMPANYNAME);
            this.Session["Declaration"] = str8;
            arrayLists.Add(str8);
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
        string sZCOMAPNYID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
        string sZCASEID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
        string sZCOMPANYNAME5 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        this.objNF3Template = new Bill_Sys_NF3_Template();
        sZCOMPANYNAME = new string[] { this.objNF3Template.getPhysicalPath(), sZCOMPANYNAME5, "/", sZCASEID, "/Packet Document/_", sZCASEID, "1.pdf" };
        string str9 = string.Concat(sZCOMPANYNAME);
        MergePDF.MergePDFFiles(Convert.ToString(arrayLists[0]), Convert.ToString(arrayLists[1]), str9.ToString());
        sZCOMPANYNAME = new string[] { this.objNF3Template.getPhysicalPath(), sZCOMPANYNAME5, "/", sZCASEID, "/Packet Document/_", sZCASEID, "2.pdf" };
        string str10 = string.Concat(sZCOMPANYNAME);
        MergePDF.MergePDFFiles(str9.ToString(), Convert.ToString(arrayLists[2]), str10.ToString());
        sZCOMPANYNAME = new string[] { this.objNF3Template.getPhysicalPath(), sZCOMPANYNAME5, "/", sZCASEID, "/Packet Document/_", sZCASEID, "3.pdf" };
        string str11 = string.Concat(sZCOMPANYNAME);
        MergePDF.MergePDFFiles(str10.ToString(), Convert.ToString(arrayLists[3]), str11.ToString());
        sZCOMPANYNAME = new string[] { this.objNF3Template.getPhysicalPath(), sZCOMPANYNAME5, "/", sZCASEID, "/Packet Document/_", sZCASEID, "4.pdf" };
        string str12 = string.Concat(sZCOMPANYNAME);
        MergePDF.MergePDFFiles(str11.ToString(), Convert.ToString(arrayLists[4]), str12.ToString());
        sZCOMPANYNAME = new string[] { this.objNF3Template.getPhysicalPath(), sZCOMPANYNAME5, "/", sZCASEID, "/Packet Document/_", sZCASEID, "5.pdf" };
        string str13 = string.Concat(sZCOMPANYNAME);
        MergePDF.MergePDFFiles(str12.ToString(), Convert.ToString(arrayLists[5]), str13.ToString());
        sZCOMPANYNAME = new string[] { this.objNF3Template.getPhysicalPath(), sZCOMPANYNAME5, "/", sZCASEID, "/Packet Document/_", sZCASEID, "6.pdf" };
        string str14 = string.Concat(sZCOMPANYNAME);
        MergePDF.MergePDFFiles(str13.ToString(), Convert.ToString(arrayLists[6]), str14.ToString());
        sZCOMPANYNAME = new string[] { this.objNF3Template.getPhysicalPath(), sZCOMPANYNAME5, "/", sZCASEID, "/Packet Document/_", sZCASEID, "7.pdf" };
        string str15 = string.Concat(sZCOMPANYNAME);
        MergePDF.MergePDFFiles(str14.ToString(), Convert.ToString(arrayLists[7]), str15.ToString());
        sZCOMPANYNAME = new string[] { ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString(), sZCOMPANYNAME5, "/", sZCASEID, "/Packet Document/_", sZCASEID, "7.pdf" };
        string str16 = string.Concat(sZCOMPANYNAME);
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Msg", string.Concat("window.open('", str16.ToString(), "'); "), true);
        
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkPatientIntakeprivate_Click(object sender, EventArgs e)
    {
        this.Wizard5.Visible = true;
        this.Wizard4.Visible = false;
        this.Wizard2.Visible = false;
        this.Wizard1.Visible = false;
        this.Wizard3.Visible = false;
        this.Wizard8.Visible = false;
        this.Wizard6.Visible = false;
    }

    protected void lnkPaymentGuarantee_Click(object sender, EventArgs e)
    {
        this.Wizard5.Visible = false;
        this.Wizard1.Visible = false;
        this.Wizard2.Visible = false;
        this.Wizard3.Visible = false;
        this.Wizard4.Visible = false;
        this.Wizard6.Visible = true;
        this.Wizard8.Visible = false;
    }

    public void LoadDataAOB()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.conn = new SqlConnection(this.strConn);
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                this.conn.Open();
                this.cmd = new SqlCommand("SP_MST_AOB_NEW", this.conn);
                this.cmd.Parameters.AddWithValue("@SZ_CASE_ID", this.TXT_CASE_ID.Text);
                this.cmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
                this.cmd.CommandType = CommandType.StoredProcedure;
                (new SqlDataAdapter(this.cmd)).Fill(dataSet);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    this.TXT_AOB_PATIENT_NAME.Text = dataSet.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString();
                    this.TXT_ACCIDENT_ON_AFTER.Text = dataSet.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
                    this.txtdecdoa.Text = dataSet.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
                    if (dataSet.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != "")
                    {
                        this.extddlOffice.Text = dataSet.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString();
                    }
                    if (dataSet.Tables[0].Rows[0]["SZ_OCA_NUMBER"].ToString() != "&nbsp;")
                    {
                        this.TXT_OCA_OFFICIAL_FORM_NUMBER.Text = dataSet.Tables[0].Rows[0]["SZ_OCA_NUMBER"].ToString();
                    }
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
        }
        finally
        {
            this.conn.Close();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void LoadDataDeclarationHippa()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.conn = new SqlConnection(this.strConn);
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                this.conn.Open();
                this.cmd = new SqlCommand("SP_MST_PATIENT_DECLARATION_AND_HIPPA_QUEENS", this.conn);
                this.cmd.Parameters.AddWithValue("@SZ_CASE_ID", this.TXT_CASE_ID.Text);
                this.cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.cmd.CommandType = CommandType.StoredProcedure;
                (new SqlDataAdapter(this.cmd)).Fill(dataSet);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["SZ_DECLARATION_NAME"].ToString() != "")
                    {
                        this.txtdeclaraionname.Text = dataSet.Tables[0].Rows[0]["SZ_DECLARATION_NAME"].ToString();
                    }
                    if (dataSet.Tables[0].Rows[0]["SZ_PRINT_NAME"].ToString() != "")
                    {
                        this.txtprtintname.Text = dataSet.Tables[0].Rows[0]["SZ_PRINT_NAME"].ToString();
                    }
                    if (dataSet.Tables[0].Rows[0]["DT_SIGNATURE_DATE"].ToString() != "")
                    {
                        this.txtdecsigndate.Text = dataSet.Tables[0].Rows[0]["DT_SIGNATURE_DATE"].ToString();
                    }
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
        }
        finally
        {
            this.conn.Close();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void LoadDataHippa()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.w3loadHippaDetails();
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

    public void LoadDataPaymentGuarantee()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.conn = new SqlConnection(this.strConn);
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                this.conn.Open();
                this.cmd = new SqlCommand("SP_MST_PATIENT_GUARANTOR_BILLING_AGREEMENT", this.conn);
                this.cmd.Parameters.AddWithValue("@SZ_CASE_ID", this.TXT_CASE_ID.Text);
                this.cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.cmd.CommandType = CommandType.StoredProcedure;
                (new SqlDataAdapter(this.cmd)).Fill(dataSet);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["DT_SIGNATURE_DATE"].ToString() != "")
                    {
                        this.txtSign.Text = dataSet.Tables[0].Rows[0]["DT_SIGNATURE_DATE"].ToString();
                    }
                    if (dataSet.Tables[0].Rows[0]["SZ_INIIAL_DATE1"].ToString() != "")
                    {
                        this.txtIntials1.Text = dataSet.Tables[0].Rows[0]["SZ_INIIAL_DATE1"].ToString();
                    }
                    if (dataSet.Tables[0].Rows[0]["SZ_INIIAL_DATE2"].ToString() != "")
                    {
                        this.txtIntials2.Text = dataSet.Tables[0].Rows[0]["SZ_INIIAL_DATE2"].ToString();
                    }
                    if (dataSet.Tables[0].Rows[0]["SZ_INIIAL_DATE3"].ToString() != "")
                    {
                        this.txtIntials3.Text = dataSet.Tables[0].Rows[0]["SZ_INIIAL_DATE3"].ToString();
                    }
                    if (dataSet.Tables[0].Rows[0]["SZ_INIIAL_DATE4"].ToString() != "")
                    {
                        this.txtIntials4.Text = dataSet.Tables[0].Rows[0]["SZ_INIIAL_DATE4"].ToString();
                    }
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
           
        }
        finally
        {
            this.conn.Close();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void LoadIntakeDetails()
    {
        DataSet dataSet = new DataSet();
        DataSet dataSet1 = new DataSet();
        dataSet1 = this.Getw5pPrivateIntakeDetails(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        dataSet = this.GetIntakeDetails(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            this.rdbMStatus.Text = dataSet.Tables[0].Rows[0]["BT_MARITAL_STATUS"].ToString();
            this.rdbPregnant.Text = dataSet.Tables[0].Rows[0]["BT_PREGNANT"].ToString();
            this.rdlOccupation.Text = dataSet.Tables[0].Rows[0]["BT_OCCUPATION"].ToString();
            this.txtInjury.Text = dataSet.Tables[0].Rows[0]["SZ_INJURIES_CASE_ESTABLISHED"].ToString();
            this.txtInsurdsName.Text = dataSet.Tables[0].Rows[0]["SZ_INSURD_NAME"].ToString();
            if (dataSet.Tables[0].Rows[0]["BT_REFERRAL"].ToString() == "1")
            {
                this.chkReferral.Checked = true;
                this.txtReff.Text = dataSet.Tables[0].Rows[0]["SZ_REFFERED_BY_WHOM"].ToString();
            }
            if (dataSet.Tables[0].Rows[0]["BT_ADVERTISE"].ToString() == "1")
            {
                this.chkAdvertisement.Checked = true;
                this.txtAdv.Text = dataSet.Tables[0].Rows[0]["SZ_WHICH_ADV"].ToString();
            }
            this.rblInsurdName.Text = dataSet.Tables[0].Rows[0]["SZ_INSURDS_RELATION"].ToString();
            TextBox str = this.txtInsurdsDOB;
            DateTime dateTime = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["DT_INSURDS_DOB"].ToString());
            str.Text = dateTime.ToString("MM/dd/yyyy");
            this.txtMemberId.Text = dataSet.Tables[0].Rows[0]["SZ_MEMBER_NO"].ToString();
            this.txtGroupNo.Text = dataSet.Tables[0].Rows[0]["SZ_GROUP_NO"].ToString();
        }
        if (dataSet1.Tables[0].Rows.Count > 0)
        {
            this.rdbw5maritalstatus.Text = dataSet1.Tables[0].Rows[0]["BT_MARITAL_STATUS"].ToString();
            this.txtw5prirelationship.Text = dataSet1.Tables[0].Rows[0]["SZ_PRI_RELATIONSHIP"].ToString();
            this.txtw5secrelationship.Text = dataSet1.Tables[0].Rows[0]["SZ_SEC_RELATIONSHIP"].ToString();
            string str1 = dataSet1.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
            string str2 = string.Concat("select SZ_INSURANCE_NAME from MST_INSURANCE_COMPANY where SZ_INSURANCE_ID = '", str1, "'");
            try
            {
                try
                {
                    this.conn.Open();
                    this.cmd = new SqlCommand(str2, this.conn);
                    this.dr = this.cmd.ExecuteReader();
                    while (this.dr.Read())
                    {
                        if (this.dr["SZ_INSURANCE_NAME"] == DBNull.Value)
                        {
                            continue;
                        }
                        this.txtw5secInsuranceName.Text = this.dr["SZ_INSURANCE_NAME"].ToString();
                    }
                }
                catch (SqlException sqlException)
                {
                    sqlException.Message.ToString();
                }
            }
            finally
            {
                if (this.conn.State == ConnectionState.Open)
                {
                    this.dr.Close();
                    this.conn.Close();
                }
            }
            string[] strArrays = new string[] { "select SZ_INSURANCE_TYPE from MST_SEC_INSURANCE_DETAIL where SZ_INSURANCE_ID = '", str1, "' and SZ_CASE_ID='", this.caseId, "' and SZ_COMPANY_ID='", this.companyId, "'" };
            str2 = string.Concat(strArrays);
            try
            {
                try
                {
                    this.conn.Open();
                    this.cmd = new SqlCommand(str2, this.conn);
                    this.dr1 = this.cmd.ExecuteReader();
                    while (this.dr1.Read())
                    {
                        if (this.dr1["SZ_INSURANCE_TYPE"] == DBNull.Value)
                        {
                            continue;
                        }
                        if (this.dr1["SZ_INSURANCE_TYPE"].ToString() == "SEC")
                        {
                            this.ddlInsuranceType.SelectedIndex = 1;
                        }
                        else if (this.dr1["SZ_INSURANCE_TYPE"].ToString() != "MAJ")
                        {
                            if (this.dr1["SZ_INSURANCE_TYPE"].ToString() != "PRI")
                            {
                                continue;
                            }
                            this.ddlInsuranceType.SelectedIndex = 3;
                        }
                        else
                        {
                            this.ddlInsuranceType.SelectedIndex = 2;
                        }
                    }
                }
                catch (SqlException sqlException1)
                {
                    sqlException1.Message.ToString();
                }
            }
            finally
            {
                if (this.conn.State == ConnectionState.Open)
                {
                    this.dr1.Close();
                    this.conn.Close();
                }
            }
            this.txtw5secpolicyholder.Text = dataSet1.Tables[0].Rows[0]["SZ_SEC_POLICY_HOLDER"].ToString();
            this.txtprivatememberid.Text = dataSet1.Tables[0].Rows[0]["MEMBER_ID"].ToString();
            this.txtprivatemedicare.Text = dataSet1.Tables[0].Rows[0]["MEDICARE#"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.caseId = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this.companyId = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.Session["PATIENT_INTEK_CASE_ID"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            string str = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID.ToString();
            if (base.Request.QueryString["EID"] != null)
            {
                this.Session["PATIENT_INTEK_CASE_ID"] = base.Request.QueryString["EID"].ToString();
            }
            base.Title = "GreenYourBills.com - Intake Sheet";
            this.TXT_CASE_ID.Text = this.Session["PATIENT_INTEK_CASE_ID"].ToString();
            this.Session["AOB_CaseID"] = this.TXT_CASE_ID.Text;
            this.Session["PATIENT_LINE_CASE_ID"] = this.TXT_CASE_ID.Text;
            this.extddlOffice.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!base.IsPostBack)
            {
                this.Wizard1.Visible = true;
                this.Wizard2.Visible = false;
                this.Wizard3.Visible = false;
                this.Wizard4.Visible = false;
                this.Wizard5.Visible = false;
                this.Wizard8.Visible = false;
                this.Wizard6.Visible = false;
                this.GetPatientDetails(str);
                this.InsuranceInformation(str);
                this.LoadDataAOB();
                this.LoadIntakeDetails();
                this.LoadDataHippa();
                this.LoadDataPaymentGuarantee();
                this.LoadDataDeclarationHippa();
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

    protected void txtUpdate_Click(object sender, EventArgs e)
    {
    }

    public void w3loadHippaDetails()
    {
        DataSet dataSet = new DataSet();
        dataSet = this.Getw3pHippaDetails(this.TXT_CASE_ID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            this.txtHipaaHealthProvider.Text = dataSet.Tables[0].Rows[0]["SZ_NAME_AND_ADDRESS"].ToString();
            this.txtHipaaWhomeInfoSend.Text = dataSet.Tables[0].Rows[0]["SZ_WHOM_THIS_SEND"].ToString();
            if (dataSet.Tables[0].Rows[0]["BT_MEDIACL_RECORED_1"].ToString().ToLower() == "true")
            {
                this.chkHipaaMRF.Checked = true;
            }
            TextBox str = this.txtMedialRecordDtFrom;
            DateTime dateTime = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["DT_MEDICAL_RECORED_FORM"].ToString());
            str.Text = dateTime.ToString("MM/dd/yyyy");
            TextBox textBox = this.txtMedialRecordDtTo;
            DateTime dateTime1 = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["DT_MEDICAL_RECORED_TO"].ToString());
            textBox.Text = dateTime1.ToString("MM/dd/yyyy");
            if (dataSet.Tables[0].Rows[0]["BT_MEDIACL_RECORED_2"].ToString().ToLower() == "true")
            {
                this.chkHippaMRF2.Checked = true;
            }
            if (dataSet.Tables[0].Rows[0]["BT_MEDIACL_RECORED_3"].ToString().ToLower() == "true")
            {
                this.chkHippaMRF3.Checked = true;
            }
            this.txtOther1.Text = dataSet.Tables[0].Rows[0]["SZ_OTHER_1"].ToString();
            this.txtOther2.Text = dataSet.Tables[0].Rows[0]["SZ_OTHER_2"].ToString();
            this.txtIncludeAlco.Text = dataSet.Tables[0].Rows[0]["SZ_INCLUDE_ALCO"].ToString();
            this.txtIncludeMen.Text = dataSet.Tables[0].Rows[0]["SZ_INCLUDE_MEN"].ToString();
            this.txtIncludeHIV.Text = dataSet.Tables[0].Rows[0]["SZ_INCLUDE_HIV"].ToString();
            if (dataSet.Tables[0].Rows[0]["BT_INITIALS"].ToString().ToLower() == "true")
            {
                this.chkini.Checked = true;
            }
            this.txtInitials.Text = dataSet.Tables[0].Rows[0]["SZ_INITIALS"].ToString();
            this.txtHelthCareProvider.Text = dataSet.Tables[0].Rows[0]["SZ_HELTH_CARE_PROVIDER"].ToString();
            this.txtAttoenry.Text = dataSet.Tables[0].Rows[0]["SZ_ATTOENRY"].ToString();
            if (dataSet.Tables[0].Rows[0]["BT_AT_REQUEST_OF_INDIVIDUAL"].ToString().ToLower() == "true")
            {
                this.chk10.Checked = true;
            }
            if (dataSet.Tables[0].Rows[0]["BT_OTHER"].ToString().ToLower() == "true")
            {
                this.chkOther.Checked = true;
            }
            TextBox str1 = this.txtExpiry;
            DateTime dateTime2 = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["DT_AUTHORIZATION_WILL_EXPIRE"].ToString());
            str1.Text = dateTime2.ToString("MM/dd/yyyy");
            this.txtPersonName.Text = dataSet.Tables[0].Rows[0]["SZ_PERSON_NAME"].ToString();
            this.txtAuthority.Text = dataSet.Tables[0].Rows[0]["SZ_AUTHOR"].ToString();
        }
    }
}