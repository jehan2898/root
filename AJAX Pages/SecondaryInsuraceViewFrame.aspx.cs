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
using NOTES_OBJECT;
using NotesComponent;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using log4net;
using PDFValueReplacement;
using iTextSharp.text.pdf;
using iTextSharp;
using iTextSharp.text.html;
using iTextSharp.text;
using Reminders;
using Microsoft.ApplicationBlocks.Data;

public partial class AJAX_Pages_SecondaryInsuraceViewFrame : PageBase
{
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    private CaseDetailsBO _caseDetailsBO;
    string caseid;
    string companyid;
    string patientID;
    string insuranceType;
    string userID;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlCommand sqlCmd1;
    SqlDataReader dr;
    bool flag;

    protected void Page_Load(object sender, EventArgs e)
    {

        caseid = ((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID.ToString();
        companyid = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        patientID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;
        userID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        txtCompanyID .Text = companyid;
        txtPatientID.Text = patientID;
        lblerrormsg.Visible = false;
        LoadDataOnPage();
        if (!IsPostBack)
        {
            this.getSecInsuInfo();
            ajAutoIns.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (base.Request.QueryString["link"] != null)
            {
                this.Link.Value = base.Request.QueryString["link"].ToString();
            }
        }
    }

    public void LoadDataOnPage()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            _bill_Sys_CaseObject.SZ_PATIENT_ID = "";
            extddlInsuranceCompany.Flag_ID = txtCompanyID.Text.ToString();
            if (this.ddlInsuranceType.SelectedItem.Text == "Private")
            {
                this.insuranceType = "PRI";
            }
            if (this.ddlInsuranceType.SelectedItem.Text == "Secondary")
            {
                this.insuranceType = "SEC";
            }
            if (this.ddlInsuranceType.SelectedItem.Text == "Major Medical")
            {
                this.insuranceType = "MAJ";
            }
            if (this.ddlInsuranceType.SelectedItem.Text == "WC")
            {
                this.insuranceType = "WC";
            }
            if (this.ddlInsuranceType.SelectedItem.Text == "Auto")
            {
                this.insuranceType = "Auto";
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

    private void GetPatientDetails()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
           this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet set = this.getPatientInfo(this.txtPatientID.Text, ((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (((set.Tables[0].Rows.Count > 0) && (set.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;")) && (set.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != ""))
            {
                this.extddlInsuranceCompany.Text=set.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                this.hdninsurancecode.Value = set.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                this.txtInsuranceCompany.Text = set.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                this.txtInsuranceAddress.Text = set.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                this.txtInsuranceCity.Text = set.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                this.txtInsuranceState.Text = set.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                this.txtInsuranceZip.Text = set.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                this.txtInsFax.Text = set.Tables[0].Rows[0]["SZ_FAX_NUMBER"].ToString();
                this.txtInsPhone.Text = set.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                this.txtInsContactPerson.Text = set.Tables[0].Rows[0]["SZ_CONTACT_PERSON"].ToString();
                this.ddlInsuranceType.Text = set.Tables[0].Rows[0]["SZ_INSURANCE_TYPEID"].ToString();
                if (((this.extddlInsuranceCompany.Text != "NA") && (this.extddlInsuranceCompany.Text != "")) && ((this.txtInsuranceCompany.Text != "") && (this.txtInsuranceCompany.Text != "No suggestions found for your search")))
                {
                    this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                }
                this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.hdninsurancecode.Value).Tables[0];
                this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                this.lstInsuranceCompanyAddress.DataValueField = "CODE";
                this.lstInsuranceCompanyAddress.DataBind();
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

    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
            txtInsuranceAddress.Text = _arraylist[2].ToString();
            txtInsuranceCity.Text = _arraylist[3].ToString();
            txtInsuranceState.Text = _arraylist[4].ToString();
            txtInsuranceZip.Text = _arraylist[5].ToString();
            txtInsFax.Text = _arraylist[9].ToString();
            txtInsPhone.Text = _arraylist[10].ToString();
            txtInsContactPerson.Text = _arraylist[11].ToString();

            Page.MaintainScrollPositionOnPostBack = true;
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
    
    private void ClearInsurancecontrol()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            txtInsuranceState.Text = "";
            txtInsuranceAddress.Text = "";
            txtInsuranceCity.Text = "";
            txtInsuranceZip.Text = "";
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

    protected void txtInsuranceCompany_TextChanged(object sender, EventArgs args)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string strInsuranceID = hdninsurancecode.Value;
        int Index = 0;
        if (txtInsuranceCompany.Text != "")
        {
            if (strInsuranceID != "0" && strInsuranceID != "")
            {
                try
                {
                    ClearInsurancecontrol();
                    _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(strInsuranceID);
                    //
                    DataSet dslstInsuranceCompanyAddress = new DataSet();
                    dslstInsuranceCompanyAddress = (DataSet)lstInsuranceCompanyAddress.DataSource;
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    if (dslstInsuranceCompanyAddress.Tables[0].Rows.Count == 0)
                    {
                        lstInsuranceCompanyAddress.Items.Clear();
                    }
                    else
                    {

                        lstInsuranceCompanyAddress.DataBind();
                        for (int i = 0; i < dslstInsuranceCompanyAddress.Tables[0].Rows.Count; i++)
                        {
                            if (dslstInsuranceCompanyAddress.Tables[0].Rows[i][2].ToString().Equals("1"))
                            {
                                Index = i;
                            }
                        }
                        lstInsuranceCompanyAddress.SelectedIndex = Index;

                        ///vivek
                        ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[Index][0].ToString());
                        txtInsuranceAddress.Text = _arraylist[2].ToString();
                        txtInsuranceCity.Text = _arraylist[3].ToString();
                        txtInsuranceState.Text = _arraylist[4].ToString();
                        // extddlInsuranceState.Text = _arraylist[4].ToString();
                        txtInsuranceZip.Text = _arraylist[5].ToString();
                        //txtInsuranceStreet.Text = _arraylist[6].ToString();
                        if (_arraylist[8].ToString() != "")
                            // extddlInsuranceState.Text = _arraylist[8].ToString();
                            txtInsPhone.Text = _arraylist[10].ToString();
                        Page.MaintainScrollPositionOnPostBack = true;


                        ///


                    }

                    //


                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    lstInsuranceCompanyAddress.DataBind();
                    Page.MaintainScrollPositionOnPostBack = true;
                    //tabcontainerPatientEntry.ActiveTabIndex = 2;
                    //
                    if (lstInsuranceCompanyAddress.Items.Count == 1)
                    {
                        ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
                        txtInsuranceAddress.Text = _arraylist[2].ToString();
                        txtInsuranceCity.Text = _arraylist[3].ToString();
                        txtInsuranceState.Text = _arraylist[4].ToString();
                        // extddlInsuranceState.Text = _arraylist[4].ToString();
                        txtInsuranceZip.Text = _arraylist[5].ToString();
                        //txtInsuranceStreet.Text = _arraylist[6].ToString();
                        if (_arraylist[8].ToString() != "")
                            //extddlInsuranceState.Text = _arraylist[8].ToString();
                            Page.MaintainScrollPositionOnPostBack = true;


                    }


                    ArrayList _arraylistnew = new ArrayList();
                    //_arraylistnew = _bill_Sys_PatientBO.GetDefaultDetailnew(extddlInsuranceCompany.Text);
                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(hdninsurancecode.Value);
                    //_arraylistnew = _bill_Sys_PatientBO.GetDefaultDetailnew("II000000000000000733");
                    if (_arraylistnew.Count == 0)
                    {
                        //txtInsuranceAddress.Text = "";
                        //txtInsuranceCity.Text = "";
                        //txtInsuranceState.Text = "";
                        //txtInsuranceZip.Text = "";
                        //txtInsuranceStreet.Text = "";
                    }
                    else
                    {

                        txtInsuranceAddress.Text = _arraylistnew[1].ToString();
                        for (int i = 0; i < lstInsuranceCompanyAddress.Items.Count; i++)
                        {


                            if (lstInsuranceCompanyAddress.Items[i].Value == _arraylistnew[0].ToString())
                            {
                                lstInsuranceCompanyAddress.SelectedIndex = i;

                                break;
                            }
                        }

                        txtInsuranceCity.Text = _arraylistnew[2].ToString();
                        txtInsuranceZip.Text = _arraylistnew[3].ToString();
                        //txtInsuranceStreet.Text = _arraylistnew[4].ToString();
                        txtInsuranceState.Text = _arraylistnew[6].ToString();
                        //extddlInsuranceState.Text = _arraylistnew[5].ToString();
                        Page.MaintainScrollPositionOnPostBack = true;
                    }

                    //


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
            else
            {
                lstInsuranceCompanyAddress.Items.Clear();
                txtInsuranceAddress.Text = "";
                txtInsuranceCity.Text = "";
                txtInsuranceState.Text = "";
                //extddlInsuranceState.Text = "";
                txtInsuranceZip.Text = "";
                //txtInsuranceStreet.Text = "";
                txtInsPhone.Text = "";
                hdninsurancecode.Value = "";
            }
        }
        else
        {
            lstInsuranceCompanyAddress.Items.Clear();
            txtInsuranceAddress.Text = "";
            txtInsuranceCity.Text = "";
            txtInsuranceState.Text = "";
            //extddlInsuranceState.Text = "";
            txtInsuranceZip.Text = "";
            //txtInsuranceStreet.Text = "";
            txtInsPhone.Text = "";
            hdninsurancecode.Value = "";
            //Page.MaintainScrollPositionOnPostBack = true;
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void extddlInsuranceCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ClearInsurancecontrol();
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
            //
            DataSet dslstInsuranceCompanyAddress = new DataSet();
            dslstInsuranceCompanyAddress = (DataSet)lstInsuranceCompanyAddress.DataSource;
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            if (dslstInsuranceCompanyAddress.Tables[0].Rows.Count == 0)
            {
                lstInsuranceCompanyAddress.Items.Clear();
            }
            else
            {

                lstInsuranceCompanyAddress.DataBind();
                lstInsuranceCompanyAddress.SelectedIndex = 0;


                ///vivek
                ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
                txtInsuranceAddress.Text = _arraylist[2].ToString();
                txtInsuranceCity.Text = _arraylist[3].ToString();
                txtInsuranceState.Text = _arraylist[4].ToString();
                //extddlInsuranceState.Text = _arraylist[4].ToString();
                txtInsuranceZip.Text = _arraylist[5].ToString();
                //txtInsuranceStreet.Text = _arraylist[6].ToString();
                if (_arraylist[8].ToString() != "")
                    // extddlInsuranceState.Text = _arraylist[8].ToString();
                    txtInsPhone.Text = _arraylist[10].ToString();
                //Page.MaintainScrollPositionOnPostBack = true;


                ///


            }

            //


            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            lstInsuranceCompanyAddress.DataBind();
            //Page.MaintainScrollPositionOnPostBack = true;
            //tabcontainerPatientEntry.ActiveTabIndex = 2;
            //
            if (lstInsuranceCompanyAddress.Items.Count == 1)
            {
                ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
                txtInsuranceAddress.Text = _arraylist[2].ToString();
                txtInsuranceCity.Text = _arraylist[3].ToString();
                txtInsuranceState.Text = _arraylist[4].ToString();
                //extddlInsuranceState.Text = _arraylist[4].ToString();
                txtInsuranceZip.Text = _arraylist[5].ToString();
                //txtInsuranceStreet.Text = _arraylist[6].ToString();
                if (_arraylist[8].ToString() != "")
                    //extddlInsuranceState.Text = _arraylist[8].ToString();
                    Page.MaintainScrollPositionOnPostBack = true;


            }


            ArrayList _arraylistnew = new ArrayList();
            _arraylistnew = _bill_Sys_PatientBO.GetDefaultDetailnew(extddlInsuranceCompany.Text);
            //_arraylistnew = _bill_Sys_PatientBO.GetDefaultDetailnew("II000000000000000733");
            if (_arraylistnew.Count == 0)
            {
                //txtInsuranceAddress.Text = "";
                //txtInsuranceCity.Text = "";
                //txtInsuranceState.Text = "";
                //txtInsuranceZip.Text = "";
                //txtInsuranceStreet.Text = "";
            }
            else
            {

                txtInsuranceAddress.Text = _arraylistnew[1].ToString();
                for (int i = 0; i < lstInsuranceCompanyAddress.Items.Count; i++)
                {


                    if (lstInsuranceCompanyAddress.Items[i].Value == _arraylistnew[0].ToString())
                    {
                        lstInsuranceCompanyAddress.SelectedIndex = i;

                        break;
                    }
                }

                txtInsuranceCity.Text = _arraylistnew[2].ToString();
                txtInsuranceZip.Text = _arraylistnew[3].ToString();
                //txtInsuranceStreet.Text = _arraylistnew[4].ToString();
                txtInsuranceState.Text = _arraylistnew[6].ToString();
                // extddlInsuranceState.Text = _arraylistnew[5].ToString();
                //Page.MaintainScrollPositionOnPostBack = true;
            }

            //


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

    protected void btnsave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (insuranceType == null)
            {
                if (hdninsurancecode == null)
                {
                    lblerrormsg.Visible = true;
                    lblerrormsg.Text = "Please enter all mandatory field's.";
                                        return;
                }
                lblerrormsg.Visible = true;
                lblerrormsg.Text = "Please enter all mandatory field's.";
                return;
            }
            else
            {
                ArrayList _objAL = new ArrayList();
                _objAL.Add(caseid);
                _objAL.Add(txtCompanyID.Text);
                _objAL.Add(hdninsurancecode.Value);
                _objAL.Add(lstInsuranceCompanyAddress.Text);
                _objAL.Add(txtPatientID.Text);
                _objAL.Add(insuranceType);
                _objAL.Add(userID);
                _objAL.Add(userID);
                _objAL.Add(txtPolicyHolder.Text);
                _objAL.Add(txtPolicyHolderAddr.Text);
                _objAL.Add(txtPolicyCity.Text);
                _objAL.Add(extdlPolicyState.Text);
                _objAL.Add(txtPolicyZip.Text);
                _objAL.Add(txtPolicyPhone.Text);
                _objAL.Add(rdlPolicyHolderRelation.SelectedValue.ToString());
                _objAL.Add(txtInsuranceCompany.Text);
                _objAL.Add(lstInsuranceCompanyAddress.Text);
                _objAL.Add(txtSecondaryPolicyNumber.Text);
                _objAL.Add(txtSecondaryClaimNumber.Text);
                
                string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
                this.sqlCon = new SqlConnection(connectionString);
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_INSERT_SECINSURANCE_TYPE", this.sqlCon);
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASEID", _objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@Flag", "LIST");
                this.sqlCmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(this.sqlCmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string insType = this.insuranceType;
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (insType == dataSet.Tables[0].Rows[i]["SZ_INSURANCE_TYPE"].ToString())
                        {
                            this.lblerrormsg.Visible = true;
                            this.lblerrormsg.Text = this.ddlInsuranceType.SelectedItem.Text + " insurance already exist for this patient.";
                            return;
                        }
                    }
                }

                Insertsecondayinsurance(_objAL);
                bool flag1 = this.Link.Value == "fromIntake";
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

    public void Insertsecondayinsurance(ArrayList objAL)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction tr;
        tr = sqlCon.BeginTransaction();
        try
        {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand("SP_INSERT_SECINSURANCE_TYPE", this.sqlCon);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = tr;
                //DataSet dsInsert = SqlHelper.ExecuteDataset(sqlCon, CommandType.StoredProcedure, "SP_INSERT_SECINSURANCE_TYPE",
                command.Parameters.AddWithValue("@SZ_CASEID", objAL[0].ToString());
                command.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
                command.Parameters.AddWithValue("@SZ_INSURANCE_ID", objAL[2].ToString());
                command.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", objAL[3].ToString());
                command.Parameters.AddWithValue("@SZ_PATIENT_ID", objAL[4].ToString());
                command.Parameters.AddWithValue("@SZ_INSURANCETYPE", objAL[5].ToString());
                command.Parameters.AddWithValue("@SZ_CREATEDBY", objAL[6].ToString());
                command.Parameters.AddWithValue("@SZ_MODIFIEDBY", objAL[7].ToString());
                command.Parameters.AddWithValue("@Flag", "ADD");
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_NAME", objAL[8].ToString());
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ADDRESS", objAL[9].ToString());
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_CITY", objAL[10].ToString());
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_STATE", objAL[11].ToString());
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ZIP", objAL[12].ToString());
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_PHONE", objAL[13].ToString());
                command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_RELATION", objAL[14].ToString());
                command.Parameters.AddWithValue("@SZ_SEC_INSURANCE_ADDRESS", objAL[16].ToString());
                command.Parameters.AddWithValue("@SZ_POLICY_NUMBER", objAL[17].ToString()) ;
                command.Parameters.AddWithValue("@SZ_CLAIM_NUMBER", objAL[18].ToString());

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                this.secondaryInsuranceID.Value = ds.Tables[0].Rows[0][0].ToString();
                //this.ID.Value = ds.Tables[1].Rows[0][1].ToString();

            SqlCommand cmd = new SqlCommand("SP_SAVE_PRIVATE_INTAKE", this.sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tr;
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[0].ToString());
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
            cmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", objAL[2].ToString());
            cmd.Parameters.AddWithValue("@SZ_INSURANCE_NAME", objAL[15].ToString());
            cmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_NAME", objAL[8].ToString());
            cmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ADDRESS", objAL[9].ToString());
            cmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_CITY", objAL[10].ToString());
            cmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_STATE", objAL[11].ToString());
            cmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ZIP", objAL[12].ToString());
            cmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_PHONE", objAL[13].ToString());
            cmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_RELATION", objAL[14].ToString());
            cmd.Parameters.AddWithValue("@I_SEC_INSURANCE_ID", secondaryInsuranceID.Value);
            cmd.Parameters.AddWithValue("@SZ_POLICY_NUMBER", objAL[17].ToString());
            cmd.Parameters.AddWithValue("@SZ_CLAIM_NUMBER", objAL[18].ToString());

            

            cmd.ExecuteNonQuery();

            tr.Commit();
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.PutMessage("Saved sucessfully");
            usrMessage.Show();
        }
        catch (Exception ex)
        {
            tr.Rollback();
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
        //try
        //{
        //    this.sqlCon.Open();
        //    SqlCommand command = new SqlCommand("SP_SAVE_PRIVATE_INTAKE", this.sqlCon);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.AddWithValue("@SZ_CASE_ID", objAL[0].ToString());
        //    command.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
        //    command.Parameters.AddWithValue("@SZ_INSURANCE_ID", this.hdninsurancecode.Value);
        //    command.Parameters.AddWithValue("@SZ_INSURANCE_NAME", this.txtInsuranceCompany.Text);
        //    command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_NAME", this.txtPolicyHolder.Text);
        //    command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ADDRESS", this.txtPolicyHolderAddr.Text);
        //    command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_CITY", this.txtPolicyCity.Text);
        //    command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_STATE", this.extdlPolicyState.Text);
        //    command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ZIP", this.txtPolicyZip.Text);
        //    command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_PHONE", this.txtPolicyPhone.Text);
        //    command.Parameters.AddWithValue("@SZ_POLICY_HOLDER_RELATION", this.rdlPolicyHolderRelation.SelectedValue.ToString());
        //    command.ExecuteNonQuery();
        //}
        //catch (SqlException exception2)
        //{
        //    exception2.Message.ToString();
        //}
        //finally
        //{
        //    if (this.sqlCon.State == ConnectionState.Open)
        //    {
        //        this.sqlCon.Close();
        //    }
        //}

        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlInsuranceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInsuranceType.SelectedItem != null)
        {
            string secInsuranceType = ddlInsuranceType.SelectedItem.Text;

           if (secInsuranceType == "Major Medical")
                insuranceType = "MAJ";
            else if (secInsuranceType == "Private")
                insuranceType = "PRI";
           else if (secInsuranceType == "WC")
               insuranceType = "WC";
           else if (secInsuranceType == "Auto")
               insuranceType = "Auto";
            else
                insuranceType = "SEC";
        }
        else
            insuranceType = "SEC";
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.sqlCon = new SqlConnection(connectionString);
        this.sqlCon.Open();
        this.sqlCmd = new SqlCommand("SP_INSERT_SECINSURANCE_TYPE", this.sqlCon);
        this.sqlCmd.CommandType = CommandType.StoredProcedure;
        this.sqlCmd.Parameters.AddWithValue("@SZ_CASEID", this.caseid);
        this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", this.companyid);
        this.sqlCmd.Parameters.AddWithValue("@Flag", "LIST");
        this.sqlCmd.ExecuteNonQuery();
        SqlDataAdapter adapter = new SqlDataAdapter(this.sqlCmd);
        DataSet dataSet = new DataSet();
        adapter.Fill(dataSet);
        
        try
        {
            if ((dataSet != null) && (dataSet.Tables[0].Rows.Count > 0))
            {
                string insType = this.insuranceType;
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (insType == dataSet.Tables[0].Rows[i]["SZ_INSURANCE_TYPE"].ToString())
                    {
                        this.flag = true;
                        return;
                    }
                    this.flag = false;
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    private void getSecInsuInfo()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string str = "";
        string connectionString = "";
        DataSet dataSet = new DataSet();
        connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.sqlCon = new SqlConnection(connectionString);
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("SP_INSERT_SECINSURANCE_TYPE", this.sqlCon);
            this.sqlCmd.CommandType = CommandType.StoredProcedure;
            this.sqlCmd.Parameters.AddWithValue("@SZ_CASEID", this.caseid);
            this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", this.companyid);
            this.sqlCmd.Parameters.AddWithValue("@Flag", "LIST");
            this.sqlCmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(this.sqlCmd);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        if ((dataSet != null) && (dataSet.Tables[0].Rows.Count > 0))
        {
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (dataSet.Tables[0].Rows[i]["SZ_INSURANCE_TYPE"].ToString() == "SEC")
                {
                    str = dataSet.Tables[0].Rows[i]["SZ_INSURANCE_ID"].ToString();
                    this.ddlInsuranceType.SelectedIndex = 1;
                    this.insuranceType = "SEC";
                    this.secondaryInsuranceID.Value = dataSet.Tables[0].Rows[i]["I_SEC_INSURANCE_ID"].ToString();
                    break;
                }
                str = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                if (dataSet.Tables[0].Rows[i]["SZ_INSURANCE_TYPE"].ToString() == "PRI")
                {
                    this.ddlInsuranceType.SelectedIndex = 3;
                    this.insuranceType = "PRI";
                    this.secondaryInsuranceID.Value = dataSet.Tables[0].Rows[i]["I_SEC_INSURANCE_ID"].ToString();
                }
                if (dataSet.Tables[0].Rows[i]["SZ_INSURANCE_TYPE"].ToString() == "WC")
                {
                    this.ddlInsuranceType.SelectedIndex = 4;
                    this.insuranceType = "WC";
                    this.secondaryInsuranceID.Value = dataSet.Tables[0].Rows[i]["I_SEC_INSURANCE_ID"].ToString();
                }
                if (dataSet.Tables[0].Rows[i]["SZ_INSURANCE_TYPE"].ToString() == "Auto")
                {
                    this.ddlInsuranceType.SelectedIndex = 5;
                    this.insuranceType = "Auto";
                    this.secondaryInsuranceID.Value = dataSet.Tables[0].Rows[i]["I_SEC_INSURANCE_ID"].ToString();
                }
                else
                {
                    this.ddlInsuranceType.SelectedIndex = 2;
                    this.insuranceType = "MAJ";
                    this.secondaryInsuranceID.Value = dataSet.Tables[0].Rows[i]["I_SEC_INSURANCE_ID"].ToString();
                }                
            }
            txtPolicyHolder.Text = dataSet.Tables[0].Rows[0]["SZ_POLICY_HOLDER_NAME"].ToString();
            txtPolicyHolderAddr.Text = dataSet.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ADDRESS"].ToString();
            txtPolicyCity.Text = dataSet.Tables[0].Rows[0]["SZ_POLICY_HOLDER_CITY"].ToString();
            extdlPolicyState.Text = dataSet.Tables[0].Rows[0]["SZ_POLICY_HOLDER_STATE"].ToString();
            txtPolicyZip.Text = dataSet.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ZIP"].ToString();
            txtPolicyPhone.Text = dataSet.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE"].ToString();
            rdlPolicyHolderRelation.SelectedValue = dataSet.Tables[0].Rows[0]["SZ_POLICY_HOLDER_RELATION"].ToString();
        }
        

        this.hdninsurancecode.Value = str;
        string cmdText = "select SZ_INSURANCE_NAME from MST_INSURANCE_COMPANY where SZ_INSURANCE_ID = '" + str + "'";
        try
        {
            this.sqlCon.Open();
            this.sqlCmd1 = new SqlCommand(cmdText, this.sqlCon);
            this.dr = this.sqlCmd1.ExecuteReader();
            while (this.dr.Read())
            {
                if (this.dr["SZ_INSURANCE_NAME"] != DBNull.Value)
                {
                    this.txtInsuranceCompany.Text = this.dr["SZ_INSURANCE_NAME"].ToString();
                }
            }
        }
        catch (SqlException exception2)
        {
            exception2.Message.ToString();
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        int num2 = 0;
        if ((str != "0") && (str != ""))
        {
            try
            {
                this.ClearInsurancecontrol();
                this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(str);
                DataSet dataSource = new DataSet();
                dataSource = (DataSet)this.lstInsuranceCompanyAddress.DataSource;
                this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                this.lstInsuranceCompanyAddress.DataValueField = "CODE";
                if (dataSource.Tables[0].Rows.Count == 0)
                {
                    this.lstInsuranceCompanyAddress.Items.Clear();
                }
                else
                {
                    this.lstInsuranceCompanyAddress.DataBind();
                    for (int j = 0; j < dataSource.Tables[0].Rows.Count; j++)
                    {
                        if (dataSource.Tables[0].Rows[j][2].ToString().Equals("1"))
                        {
                            num2 = j;
                        }
                    }
                    this.lstInsuranceCompanyAddress.SelectedIndex = num2;
                    ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSource.Tables[0].Rows[num2][0].ToString());
                    this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
                    this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
                    this.txtInsuranceState.Text = insuranceAddressDetail[4].ToString();
                    this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
                    if (insuranceAddressDetail[8].ToString() != "")
                    {
                        this.txtInsPhone.Text = insuranceAddressDetail[10].ToString();
                    }
                    this.Page.MaintainScrollPositionOnPostBack = true;
                }
                this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                this.lstInsuranceCompanyAddress.DataValueField = "CODE";
                this.lstInsuranceCompanyAddress.DataBind();
                this.Page.MaintainScrollPositionOnPostBack = true;
                if (this.lstInsuranceCompanyAddress.Items.Count == 1)
                {
                    ArrayList list2 = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSource.Tables[0].Rows[0][0].ToString());
                    this.txtInsuranceAddress.Text = list2[2].ToString();
                    this.txtInsuranceCity.Text = list2[3].ToString();
                    this.txtInsuranceState.Text = list2[4].ToString();
                    this.txtInsuranceZip.Text = list2[5].ToString();
                    if (list2[8].ToString() != "")
                    {
                        this.Page.MaintainScrollPositionOnPostBack = true;
                    }
                }
                ArrayList list3 = new ArrayList();
                this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.hdninsurancecode.Value);
                if (list3.Count != 0)
                {
                    this.txtInsuranceAddress.Text = list3[1].ToString();
                    for (int k = 0; k < this.lstInsuranceCompanyAddress.Items.Count; k++)
                    {
                        if (this.lstInsuranceCompanyAddress.Items[k].Value == list3[0].ToString())
                        {
                            this.lstInsuranceCompanyAddress.SelectedIndex = k;
                            break;
                        }
                    }
                    this.txtInsuranceCity.Text = list3[2].ToString();
                    this.txtInsuranceZip.Text = list3[3].ToString();
                    this.txtInsuranceState.Text = list3[6].ToString();
                    this.Page.MaintainScrollPositionOnPostBack = true;
                }
                return;
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
        this.lstInsuranceCompanyAddress.Items.Clear();
        this.txtInsuranceAddress.Text = "";
        this.txtInsuranceCity.Text = "";
        this.txtInsuranceState.Text = "";
        this.txtInsuranceZip.Text = "";
        this.txtInsPhone.Text = "";
        this.hdninsurancecode.Value = "";

        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet getPatientInfo(string szPatientID, string szCompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = "";
        DataSet dataSet = new DataSet();
        connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.sqlCon = new SqlConnection(connectionString);
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("sp_get_secondary_insurance_for_patient", this.sqlCon);
            this.sqlCmd.CommandType = CommandType.StoredProcedure;
            this.sqlCmd.CommandTimeout = 0;
            this.sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", szPatientID);
            this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            SqlDataAdapter adapter = new SqlDataAdapter(this.sqlCmd);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return dataSet;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void Updatesecondayinsurance(ArrayList objAL)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.sqlCon = new SqlConnection(connectionString);
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("SP_INSERT_SECINSURANCE_TYPE", this.sqlCon);
            this.sqlCmd.CommandType = CommandType.StoredProcedure;
            this.sqlCmd.Parameters.AddWithValue("@I_SEC_INSURANCE_ID", objAL[0].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_CASEID", objAL[1].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", objAL[3].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", objAL[4].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", objAL[5].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_INSURANCETYPE", objAL[6].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_CREATEDBY", objAL[7].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_MODIFIEDBY", objAL[8].ToString());
            this.sqlCmd.Parameters.AddWithValue("@Flag", "UPDATE");
            this.sqlCmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_NAME", objAL[9].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ADDRESS", objAL[10].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_CITY", objAL[11].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_STATE", objAL[12].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_ZIP", objAL[13].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_PHONE", objAL[14].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_POLICY_HOLDER_RELATION", objAL[15].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_POLICY_NUMBER", objAL[16].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NUMBER", objAL[17].ToString());
            this.sqlCmd.ExecuteNonQuery();
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.PutMessage("Changes to the server were made successfully");
            this.usrMessage.Show();
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
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void Deletesecondayinsurance()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.sqlCon = new SqlConnection(connectionString);
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("SP_INSERT_SECINSURANCE_TYPE", this.sqlCon);
            this.sqlCmd.CommandType = CommandType.StoredProcedure;
            this.sqlCmd.Parameters.AddWithValue("@I_SEC_INSURANCE_ID", this.secondaryInsuranceID.Value);
            this.sqlCmd.Parameters.AddWithValue("@Flag", "DELETE");
            this.sqlCmd.ExecuteNonQuery();
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.PutMessage("Changes to the server were made successfully");
            this.usrMessage.Show();
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
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        if (this.flag)
        {
            this.lblerrormsg.Visible = true;
            this.lblerrormsg.Text = "You cannot update insurance type to " + this.ddlInsuranceType.SelectedItem.Text + " because it is already present.";
        }
        else
        {
            try
            {
                if (this.insuranceType == null)
                {
                    if (this.hdninsurancecode == null)
                    {
                        this.lblerrormsg.Visible = true;
                        this.lblerrormsg.Text = "Please enter all mandatory field(s)";
                    }
                    else
                    {
                        this.lblerrormsg.Visible = true;
                        this.lblerrormsg.Text = "Please enter all mandatory field(s)";
                    }
                }
                else
                {
                    ArrayList objAL = new ArrayList();
                    
                    objAL.Add(this.secondaryInsuranceID.Value);
                    objAL.Add(this.caseid);
                    objAL.Add(this.txtCompanyID.Text);
                    objAL.Add(this.hdninsurancecode.Value);
                    objAL.Add(this.lstInsuranceCompanyAddress.Text);
                    objAL.Add(this.txtPatientID.Text);
                    objAL.Add(this.insuranceType);
                    objAL.Add(this.userID);
                    objAL.Add(this.userID);
                    objAL.Add(txtPolicyHolder.Text);
                    objAL.Add(txtPolicyHolderAddr.Text);
                    objAL.Add(txtPolicyCity.Text);
                    objAL.Add(extdlPolicyState.Text);
                    objAL.Add(txtPolicyZip.Text);
                    objAL.Add(txtPolicyPhone.Text);
                    objAL.Add(rdlPolicyHolderRelation.SelectedValue.ToString());
                    objAL.Add(txtSecondaryPolicyNumber.Text);
                    objAL.Add(txtSecondaryClaimNumber.Text);
                    this.Updatesecondayinsurance(objAL);
                    string text = this.txtInsuranceCompany.Text;
                    string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
                    this.sqlCon = new SqlConnection(connectionString);
                    string cmdText = "update TXN_PRIVATE_INTAKE set SZ_INSURANCE_ID='" + this.hdninsurancecode.Value + "', SZ_INSURANCE_NAME='" + text + "' , SZ_SEC_INSURANCE_ADDRESS='" + this.lstInsuranceCompanyAddress.SelectedItem.Text + "', SZ_POLICY_HOLDER_NAME='" + txtPolicyHolder.Text + "', SZ_POLICY_HOLDER_ADDRESS='" + txtPolicyHolderAddr.Text + "', SZ_POLICY_HOLDER_CITY='" + txtPolicyCity.Text + "', SZ_POLICY_HOLDER_STATE='" + extdlPolicyState.Text + "', SZ_POLICY_HOLDER_ZIP='" + txtPolicyZip.Text + "', SZ_POLICY_HOLDER_PHONE='" + txtPolicyPhone.Text + "', SZ_POLICY_HOLDER_RELATION='" + rdlPolicyHolderRelation.SelectedValue.ToString() + "',SZ_POLICY_NUMBER='" + txtSecondaryPolicyNumber.Text + "',SZ_CLAIM_NUMBER='" + txtSecondaryClaimNumber.Text + "'   where SZ_CASE_ID='" + this.caseid + "' and SZ_COMPANY_ID='" + this.txtCompanyID.Text + "' and I_SEC_INSURANCE_ID='" + secondaryInsuranceID.Value + "'";
                    try
                    {
                        this.sqlCon.Open();
                        this.sqlCmd = new SqlCommand(cmdText, this.sqlCon);
                        this.sqlCmd.ExecuteNonQuery();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        this.sqlCon.Close();
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
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (this.insuranceType == null)
            {
                if (this.hdninsurancecode == null)
                {
                    this.lblerrormsg.Visible = true;
                    this.lblerrormsg.Text = "Please enter all mandatory field(s)";
                }
                else
                {
                    this.lblerrormsg.Visible = true;
                    this.lblerrormsg.Text = "Please enter all mandatory field(s)";
                }
            }
            else
            {
                this.Deletesecondayinsurance();
                this.getSecInsuInfo();
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
    protected void rdlPolicyHolderRelation_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (rdlPolicyHolderRelation.SelectedValue == "1")
            {
                _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                DataSet _patientDs = _bill_Sys_PatientBO.GetPatientInfo(patientID, companyid);
                if (_patientDs.Tables[0].Rows.Count > 0)
                {
                    txtPolicyHolder.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString() + " " + _patientDs.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                    txtPolicyHolderAddr.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                    txtPolicyCity.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
                    extdlPolicyState.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();
                    txtPolicyZip.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
                    txtPolicyPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();

                    if (extdlPolicyState.Text != "NA" && extdlPolicyState.Text != "")
                        txtPolicyState.Text = extdlPolicyState.Selected_Text;
                }                
            }
            else
            {
                txtPolicyHolder.Text = "";
                txtPolicyHolderAddr.Text = "";
                txtPolicyCity.Text = "";
                extdlPolicyState.Text = "";
                txtPolicyState.Text = "";
                txtPolicyZip.Text = "";
                txtPolicyPhone.Text = "";
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
}
