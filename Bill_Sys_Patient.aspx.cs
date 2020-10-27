/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_Patient.aspx.cs
/*Purpose              :       To Add and Edit Patient
/*Author               :       Sandeep Y
/*Date of creation     :       22 Dec 2008  s
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
using System.Collections;
using DotNetUtils;

public partial class Bill_Sys_Patient : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    Bill_Sys_CaseObject _objCaseObject;
    private DataTable dt = new DataTable();
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    SqlConnection SqlCon;

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this._listOperation.WebPage=this.Page;
            this._listOperation.Xml_File="Patient.xml";
            this._listOperation.LoadList();
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

    public DataSet BindMadatoryField()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet set2;
        try
        {
            DataSet dataSet = new DataSet();
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            connection.Open();
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.CommandText = "SP_GET_SELECTED_MANDATORY_FIELD";
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Connection = connection;
            selectCommand.Parameters.AddWithValue("@SZ_PAGE", "Bill_Sys_Patient.aspx");
            string str = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", str);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
            set2 = dataSet;
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
            return null;
        }
        return set2;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        
    }

    protected void btnAccept_ButtonClick(object sender, EventArgs e)
    {
        string[] strArray = this.hdninsurancecode.Value.Split(new char[] { '-' });
        string str2 = strArray[0].Trim();
        string str3 = strArray[1].Trim();
        this.extddlInsuranceCompany.Text=str2;
        this.ClearInsurancecontrol();
        this.extddlInsuranceCode.Text=this.extddlInsuranceCompany.Text;
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.extddlInsuranceCompany.Text);
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
            this.lstInsuranceCompanyAddress.SelectedValue = str3;
            ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSource.Tables[0].Rows[0][0].ToString());
            this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
            this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
            this.extddlInsuranceState.Text=insuranceAddressDetail[4].ToString();
            this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
            this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
            if (insuranceAddressDetail[8].ToString() != "")
            {
                this.extddlInsuranceState.Text=insuranceAddressDetail[8].ToString();
            }
            this.Page.MaintainScrollPositionOnPostBack = true;
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            ArrayList list2 = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(this.lstInsuranceCompanyAddress.SelectedValue);
            this.txtInsuranceAddress.Text = list2[2].ToString();
            this.txtInsuranceCity.Text = list2[3].ToString();
            this.txtInsuranceState.Text = list2[4].ToString();
            this.txtInsuranceZip.Text = list2[5].ToString();
            this.txtInsuranceStreet.Text = list2[6].ToString();
            if (insuranceAddressDetail[8].ToString() != "")
            {
                this.extddlInsuranceState.Text=list2[8].ToString();
            }
            this.Page.MaintainScrollPositionOnPostBack = true;
        }
    }

    protected void btnAddAttorney_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            PopupBO pbo = new PopupBO();
            ArrayList list = new ArrayList();
            list.Add("");
            list.Add(this.txtAttorneyFirstName.Text);
            list.Add(this.txtAttorneyLastName.Text);
            list.Add(this.txtAttorneyCity.Text);
            list.Add(this.txtAttorneyState.Text);
            list.Add(this.txtAttorneyZip.Text);
            list.Add(this.txtAttorneyPhoneNo.Text);
            list.Add(this.txtAttorneyFax.Text);
            list.Add(this.txtAttorneyEmailID.Text);
            list.Add(this.txtCompanyID.Text);
            list.Add(this.extddlAttorneyState.Text);
            pbo.saveAttorney(list);
            this.extddlAttorney.Text=pbo.getLatestID("SP_MST_ATTORNEY", this.txtCompanyID.Text);
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
            this.ClearControl();
            clearemployer();
            this.Page.MaintainScrollPositionOnPostBack = false;
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

    protected void btnClearAddress_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearAddressDetails();
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

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.SaveData();
            this._DAO_NOTES_EO = new DAO_NOTES_EO();
            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="CASE_CREATED";
            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC="";
            this._DAO_NOTES_BO = new DAO_NOTES_BO();
            this._DAO_NOTES_EO.SZ_USER_ID=(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            this._DAO_NOTES_EO.SZ_CASE_ID=(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            this._DAO_NOTES_EO.SZ_COMPANY_ID=(this.txtCompanyID.Text);
            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                base.Response.Redirect(" AJAX Pages/Bill_Sys_ReCaseDetails.aspx", false);
            }
            else
            {
                base.Response.Redirect("AJAX Pages/Bill_Sys_CaseDetails.aspx", false);
            }
            this.hidIsSaved.Value = "1";
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool flag = false;
        if (this.txtChartNo.Visible)
        {
            if (!this.txtChartNo.Text.Equals(""))
            {
                flag = true;
                if (!this._bill_Sys_PatientBO.ExistChartNumber(this.txtCompanyID.Text, this.txtChartNo.Text, "CHART"))
                {
                    this.SaveInformation();
                }
                else
                {
                    this.Page.RegisterStartupScript("mm", "<script language='javascript'>alert('" + this.txtChartNo.Text + "' + ' chart no is already exist ...!');</script>");
                }
            }
        }
        else if (this.txtRefChartNumber.Visible && !this.txtRefChartNumber.Text.Equals(""))
        {
            flag = true;
            if (!this._bill_Sys_PatientBO.ExistChartNumber(this.txtCompanyID.Text, this.txtRefChartNumber.Text, "REF"))
            {
                this.SaveInformation();
            }
            else
            {
                this.Page.RegisterStartupScript("mm", "<script language='javascript'>alert('" + this.txtRefChartNumber.Text + "' + ' chart no is already exist');</script>");
            }
        }
        if (!flag)
        {
            this.SaveInformation();
        }
    }

    protected void btnSaveAddress_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this._saveOperation = new SaveOperation();
        try
        {
            this.txtInsuranceCompanyID.Text = this.extddlInsuranceCompany.Text;
            ArrayList list = new ArrayList();
            PopupBO pbo = new PopupBO();
            list.Add(this.txtInsuranceCompanyID.Text);
            list.Add(this.txtInsuranceAddressCode.Text);
            list.Add(this.txtInsuranceStreetCode.Text);
            list.Add(this.txtInsuranceCityCode.Text);
            list.Add(this.extddlStateCode.Text);
            list.Add(this.txtInsuranceZipCode.Text);
            list.Add(this.txtCompanyID.Text);
            list.Add(this.extddlInsuranceStateNew.Text);
            if (this.IDDefault.Checked)
            {
                list.Add("1");
            }
            else
            {
                list.Add("0");
            }
            pbo.saveInsuranceAddressNew(list);
            this.ClearInsurancecontrol();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.extddlInsuranceCompany.Text);
            this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            this.lstInsuranceCompanyAddress.DataValueField = "CODE";
            this.lstInsuranceCompanyAddress.DataBind();
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.lstInsuranceCompanyAddress.SelectedValue = pbo.getLatestID("SP_MST_INSURANCE_ADDRESS", this.txtCompanyID.Text);
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(this.lstInsuranceCompanyAddress.SelectedValue);
            this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
            this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
            this.extddlInsuranceState.Text=insuranceAddressDetail[8].ToString();
            this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
            this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
            this.Page.MaintainScrollPositionOnPostBack = true;
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

    protected void btnSaveAdjuster_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            PopupBO pbo = new PopupBO();
            ArrayList list = new ArrayList();
            list.Add("");
            list.Add(this.txtAdjusterPopupName.Text);
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add("");
            list.Add(this.txtAdjusterPopupPhone.Text);
            list.Add(this.txtAdjusterPopupExtension.Text);
            list.Add(this.txtAdjusterPopupFax.Text);
            list.Add(this.txtAdjusterPopupEmail.Text);
            list.Add(this.txtCompanyID.Text);
            list.Add("");
            pbo.saveAdjuster(list);
            this.extddlAdjuster.Text=pbo.getLatestID("SP_MST_ADJUSTER", this.txtCompanyID.Text);
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

    protected void btnSaveInsurance_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            PopupBO pbo = new PopupBO();
            ArrayList list = new ArrayList();
            list.Add(this.txtInsuranceCompanyName.Text);
            list.Add(this.txtInsurancePhoneNumber.Text);
            list.Add(this.txtInsuranceEmail.Text);
            list.Add(this.txtCompanyID.Text);
            list.Add(this.txtInsCode.Text);
            pbo.saveInsurance(list);
            this.extddlInsuranceCompany.Text=pbo.getLatestID("SP_MST_INSURANCE_COMPANY", this.txtCompanyID.Text);
            this.extddlInsuranceCode.Text=this.extddlInsuranceCompany.Text;
            list = new ArrayList();
            list.Add(this.extddlInsuranceCompany.Text);
            list.Add(this.txtInsuranceAddressNew.Text);
            list.Add(this.txtInsuranceStreetNew.Text);
            list.Add(this.txtInsuranceCityNew.Text);
            list.Add(this.txtInsuranceStateNew.Text);
            list.Add(this.txtInsuranceZipNew.Text);
            list.Add(this.txtCompanyID.Text);
            list.Add(this.extddlInsuranceStateNew.Text);
            pbo.saveInsuranceAddress(list);
            this.ClearInsurancecontrol();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.extddlInsuranceCompany.Text);
            this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            this.lstInsuranceCompanyAddress.DataValueField = "CODE";
            this.lstInsuranceCompanyAddress.DataBind();
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.lstInsuranceCompanyAddress.SelectedValue = pbo.getLatestID("SP_MST_INSURANCE_ADDRESS", this.txtCompanyID.Text);
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(this.lstInsuranceCompanyAddress.SelectedValue);
            this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
            this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
            this.txtInsuranceState.Text = insuranceAddressDetail[4].ToString();
            this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
            this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
            this.extddlInsuranceStateNew.Text=insuranceAddressDetail[8].ToString();
            this.Page.MaintainScrollPositionOnPostBack = true;
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            if (this._associateDiagnosisCodeBO.GetCaseTypeName(this.extddlCaseType.Text) == "WC000000000000000001")
            {
                this.txtAssociateDiagnosisCode.Text = "1";
            }
            else
            {
                this.txtAssociateDiagnosisCode.Text = "0";
            }
            this._editOperation.Primary_Value=this.Session["PatientID"].ToString();
            this._editOperation.WebPage=this.Page;
            this._editOperation.Xml_File="Patient.xml";
            this._editOperation.UpdateMethod();
            this._editOperation.Primary_Value=this.txtAccidentID.Text;
            this._editOperation.WebPage=this.Page;
            this._editOperation.Xml_File="PatientAccidentInfo.xml";
            this._editOperation.UpdateMethod();
            this._editOperation.Primary_Value=this.txtCaseID.Text;
            this._editOperation.WebPage=this.Page;
            this._editOperation.Xml_File="PatientCaseMaster.xml";
            this._editOperation.UpdateMethod();
            this.BindGrid();
            this.ClearControl();
            this.lblMsg.Visible = true;
            this.Page.MaintainScrollPositionOnPostBack = false;
            this.lblMsg.Text = " Patient Information Updated successfully ! ";
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

    protected void chkAutoIncr_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void ClearAddressDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtInsuranceAddress.Text = "";
            this.txtInsuranceStreet.Text = "";
            this.txtInsuranceCity.Text = "";
            this.txtInsuranceState.Text = "";
            this.txtInsuranceZip.Text = "";
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

    private void ClearAdjusterControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtAdjusterExtension.Text = "";
            this.txtAdjusterPhone.Text = "";
            this.txtfax.Text = "";
            this.txtEmail.Text = "";
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
            this.txtCarrierCaseNo.Text = "";
            this.txtCaseID.Text = "";
            this.txtClaimNumber.Text = "";
            this.txtDateofAccident.Text = "";
            this.txtDateOfBirth.Text = "";
            this.txtDateOfInjury.Text = "";
            this.txtJobTitle.Text = "";
            this.txtMI.Text = "";
            this.txtPatientAddress.Text = "";
            this.txtPatientAge.Text = "";
            this.txtPatientCity.Text = "";
            this.txtPatientEmail.Text = "";
            this.txtPatientFName.Text = "";
            this.txtPatientID.Text = "";
            this.txtPatientLName.Text = "";
            this.txtPatientPhone.Text = "";
            this.txtPatientStreet.Text = "";
            this.txtPatientZip.Text = "";
            this.txtPolicyNumber.Text = "";
            this.txtSocialSecurityNumber.Text = "";
            this.txtState.Text = "";
            this.txtWCBNumber.Text = "";
            this.txtWorkActivites.Text = "";
            this.extddlAdjuster.Text="NA";
            this.extddlAttorney.Text="NA";
            this.extddlCaseStatus.Text="NA";
            this.extddlCaseType.Text="NA";
            this.extddlInsuranceCompany.Text="NA";
            this.extddlProvider.Text="NA";
            this.ddlSex.SelectedValue = "0";
            this.chkTransportation.Checked = false;
            this.chkWrongPhone.Checked = false;
            this.txtWorkPhone.Text = "";
            this.txtExtension.Text = "";
            this.txtPlatenumber.Text = "";
            this.txtAccidentAddress.Text = "";
            this.txtAccidentCity.Text = "";
            this.txtAccidentState.Text = "";
            this.txtPolicyReport.Text = "";
            this.txtListOfPatient.Text = "";
            this.txtEmployerName.Text = "";
            this.txtEmployerAddress.Text = "";
            this.txtEmployerCity.Text = "";
            this.txtEmployerState.Text = "";
            this.txtEmployerZip.Text = "";
            this.txtEmployerPhone.Text = "";
            this.txtInsuranceCity.Text = "";
            this.txtInsuranceState.Text = "";
            this.txtInsuranceZip.Text = "";
            this.lstInsuranceCompanyAddress.Items.Clear();
            this.txtChartNo.Text = "";
            this.ClearAdjusterControl();
            this.btnSave.Enabled = true;
            this.btnUpdate.Enabled = false;
            this.lblMsg.Visible = false;
            this.hlnkShowNotes.Visible = false;
            this.PopEx.Enabled=false;
            this.pnlShowNotes.Visible = false;
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

    private void ClearInsurancecontrol()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtInsuranceAddressCode.Text = "";
            this.txtInsuranceCityCode.Text = "";
            this.txtInsuranceState.Text = "";
            this.txtInsuranceZipCode.Text = "";
            this.txtInsuranceStreetCode.Text = "";
            this.txtInsuranceStateNewCode.Text = "";
            this.extddlStateCode.Text="";
            this.txtInsuranceState.Text = "";
            this.extddlInsuranceState.Text="";
            this.IDDefault.Checked = false;
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

    protected void extddlAdjuster_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearAdjusterControl();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            ArrayList adjusterDetail = new ArrayList();
            adjusterDetail = this._bill_Sys_PatientBO.GetAdjusterDetail(this.extddlAdjuster.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (adjusterDetail.Count > 0)
            {
                if (adjusterDetail.Count >= 1)
                {
                    this.txtAdjusterPhone.Text = adjusterDetail[0].ToString();
                }
                if (adjusterDetail.Count >= 2)
                {
                    this.txtAdjusterExtension.Text = adjusterDetail[1].ToString();
                }
                if (adjusterDetail.Count >= 3)
                {
                    this.txtfax.Text = adjusterDetail[2].ToString();
                }
                if (adjusterDetail.Count >= 4)
                {
                    this.txtEmail.Text = adjusterDetail[3].ToString();
                }
            }
            this.Page.MaintainScrollPositionOnPostBack = true;
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

    protected void extddlInsuranceCode_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearInsurancecontrol();
            this.extddlInsuranceCompany.Text=this.extddlInsuranceCode.Text;
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.extddlInsuranceCompany.Text);
            DataSet dataSource = new DataSet();
            dataSource = (DataSet)this.lstInsuranceCompanyAddress.DataSource;
            this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            this.lstInsuranceCompanyAddress.DataValueField = "CODE";
            this.lstInsuranceCompanyAddress.DataBind();
            if (dataSource.Tables[0].Rows.Count == 0)
            {
                this.lstInsuranceCompanyAddress.Items.Clear();
            }
            else
            {
                this.lstInsuranceCompanyAddress.DataBind();
                this.lstInsuranceCompanyAddress.SelectedIndex = 0;
                ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSource.Tables[0].Rows[0][0].ToString());
                this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
                this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
                this.extddlInsuranceState.Text=insuranceAddressDetail[4].ToString();
                this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
                this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
                if (insuranceAddressDetail[8].ToString() != "")
                {
                    this.extddlInsuranceState.Text=insuranceAddressDetail[8].ToString();
                }
                this.Page.MaintainScrollPositionOnPostBack = true;
            }
            new PopupBO();
            this.Page.MaintainScrollPositionOnPostBack = true;
            if (this.lstInsuranceCompanyAddress.Items.Count == 1)
            {
                ArrayList list2 = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSource.Tables[0].Rows[0][0].ToString());
                this.txtInsuranceAddress.Text = list2[2].ToString();
                this.txtInsuranceCity.Text = list2[3].ToString();
                this.txtInsuranceState.Text = list2[4].ToString();
                this.extddlInsuranceState.Text=list2[4].ToString();
                this.txtInsuranceZip.Text = list2[5].ToString();
                this.txtInsuranceStreet.Text = list2[6].ToString();
                if (list2[8].ToString() != "")
                {
                    this.extddlInsuranceState.Text=list2[8].ToString();
                }
                this.Page.MaintainScrollPositionOnPostBack = true;
            }
            ArrayList defaultDetailnew = new ArrayList();
            defaultDetailnew = this._bill_Sys_PatientBO.GetDefaultDetailnew(this.extddlInsuranceCompany.Text);
            if (defaultDetailnew.Count != 0)
            {
                this.txtInsuranceAddress.Text = defaultDetailnew[1].ToString();
                for (int i = 0; i < this.lstInsuranceCompanyAddress.Items.Count; i++)
                {
                    if (this.lstInsuranceCompanyAddress.Items[i].Value == defaultDetailnew[0].ToString())
                    {
                        this.lstInsuranceCompanyAddress.SelectedIndex = i;
                        break;
                    }
                }
                this.txtInsuranceCity.Text = defaultDetailnew[2].ToString();
                this.txtInsuranceZip.Text = defaultDetailnew[3].ToString();
                this.txtInsuranceStreet.Text = defaultDetailnew[4].ToString();
                this.txtInsuranceState.Text = defaultDetailnew[6].ToString();
                this.extddlInsuranceState.Text=defaultDetailnew[5].ToString();
                this.Page.MaintainScrollPositionOnPostBack = true;
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

    protected void extddlInsuranceCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearInsurancecontrol();
            this.extddlInsuranceCode.Text=this.extddlInsuranceCompany.Text;
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.extddlInsuranceCompany.Text);
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
                this.lstInsuranceCompanyAddress.SelectedIndex = 0;
                ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSource.Tables[0].Rows[0][0].ToString());
                this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
                this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
                this.extddlInsuranceState.Text=insuranceAddressDetail[4].ToString();
                this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
                this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
                if (insuranceAddressDetail[8].ToString() != "")
                {
                    this.extddlInsuranceState.Text=insuranceAddressDetail[8].ToString();
                }
                this.Page.MaintainScrollPositionOnPostBack = true;
            }
            new PopupBO();
            this.Page.MaintainScrollPositionOnPostBack = true;
            if (this.lstInsuranceCompanyAddress.Items.Count == 1)
            {
                ArrayList list2 = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSource.Tables[0].Rows[0][0].ToString());
                this.txtInsuranceAddress.Text = list2[2].ToString();
                this.txtInsuranceCity.Text = list2[3].ToString();
                this.txtInsuranceState.Text = list2[4].ToString();
                this.extddlInsuranceState.Text=list2[4].ToString();
                this.txtInsuranceZip.Text = list2[5].ToString();
                this.txtInsuranceStreet.Text = list2[6].ToString();
                if (list2[8].ToString() != "")
                {
                    this.extddlInsuranceState.Text=list2[8].ToString();
                }
                this.Page.MaintainScrollPositionOnPostBack = true;
            }
            ArrayList defaultDetailnew = new ArrayList();
            defaultDetailnew = this._bill_Sys_PatientBO.GetDefaultDetailnew(this.extddlInsuranceCompany.Text);
            if (defaultDetailnew.Count != 0)
            {
                this.txtInsuranceAddress.Text = defaultDetailnew[1].ToString();
                for (int i = 0; i < this.lstInsuranceCompanyAddress.Items.Count; i++)
                {
                    if (this.lstInsuranceCompanyAddress.Items[i].Value == defaultDetailnew[0].ToString())
                    {
                        this.lstInsuranceCompanyAddress.SelectedIndex = i;
                        break;
                    }
                }
                this.txtInsuranceCity.Text = defaultDetailnew[2].ToString();
                this.txtInsuranceZip.Text = defaultDetailnew[3].ToString();
                this.txtInsuranceStreet.Text = defaultDetailnew[4].ToString();
                this.txtInsuranceState.Text = defaultDetailnew[6].ToString();
                this.extddlInsuranceState.Text=defaultDetailnew[5].ToString();
                this.Page.MaintainScrollPositionOnPostBack = true;
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

    private string GetNFCaseType()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        string str2 = "";
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();
            SqlDataReader reader = new SqlCommand("SELECT SZ_CASE_TYPE_ID FROM MST_CASE_TYPE WHERE SZ_CASE_TYPE_ID='" + this.Session["CASETYPE"].ToString() + "' AND SZ_ABBRIVATION_ID='WC000000000000000002' AND SZ_COMPANY_ID='" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'", connection).ExecuteReader();
            while (reader.Read())
            {
                str2 = Convert.ToString(reader[0].ToString());
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string Elmahstr2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + Elmahstr2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return str2;
    }

    private string GetOpenCaseStatus()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        string str2 = "";
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();
            SqlDataReader reader = new SqlCommand("Select SZ_CASE_STATUS_ID FROM MST_CASE_STATUS WHERE SZ_STATUS_NAME='OPEN' AND SZ_COMPANY_ID='" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'", connection).ExecuteReader();
            while (reader.Read())
            {
                str2 = Convert.ToString(reader[0].ToString());
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string Elmahstr2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + Elmahstr2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return str2;
    }

    private void GetPreviousChartNo(string _companyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            DataTable table = new DataTable();
            bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
            table = this._bill_Sys_PatientBO.Get_Max_RFO_and_ChartNo(_companyID);
            if (flag && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
            {
                this.lblPchartno.Visible = true;
                this.lblPreChartNo.Visible = true;
                this.lblPreChartNo.Text = table.Rows[0]["RFO_CHART_NO"].ToString();
            }
            else if (!flag && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
            {
                this.lblPchartno.Visible = true;
                this.lblPreChartNo.Visible = true;
                this.lblPreChartNo.Text = table.Rows[0]["CHART_NO"].ToString();
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

    protected void GetRefCompany()
    {
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        string refCompany = "";
        refCompany = this._bill_Sys_PatientBO.GetRefCompany(this.txtCompanyID.Text);
        if ((refCompany == "True") && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
        {
            this.txtRefChartNumber.Visible = true;
            this.chkAutoIncr.Visible = false;
            this.lblChartNo.Visible = true;
        }
        else if ((refCompany == "False") && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "0"))
        {
            this.txtRefChartNumber.Visible = false;
            this.chkAutoIncr.Visible = false;
            this.lblChartNo.Visible = false;
        }
    }

    private string GetWCCaseType()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        string str2 = "";
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();
            SqlDataReader reader = new SqlCommand("Select SZ_CASE_TYPE_ID FROM MST_CASE_TYPE WHERE SZ_CASE_TYPE_ID='" + this.Session["CASETYPE"].ToString() + "' AND SZ_ABBRIVATION_ID='WC000000000000000001' AND SZ_COMPANY_ID='" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'", connection).ExecuteReader();
            while (reader.Read())
            {
                str2 = Convert.ToString(reader[0].ToString());
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string Elmahstr2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + Elmahstr2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return str2;
    }

    protected void grdPatientList_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
    }

    protected void grdPatientList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdPatientList.CurrentPageIndex = e.NewPageIndex;
            this.BindGrid();
            this.lblMsg.Visible = false;
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

    protected void grdPatientList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["PatientID"] = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[1].Text;
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[2].Text != "&nbsp;")
            {
                this.txtPatientFName.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[2].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[3].Text != "&nbsp;")
            {
                this.txtPatientLName.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[3].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[4].Text != "&nbsp;")
            {
                this.txtPatientAge.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[4].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[5].Text != "&nbsp;")
            {
                this.txtPatientAddress.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[5].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[6].Text != "&nbsp;")
            {
                this.txtPatientStreet.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[6].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[7].Text != "&nbsp;")
            {
                this.txtPatientCity.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[7].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[8].Text != "&nbsp;")
            {
                this.txtPatientZip.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[8].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[9].Text != "&nbsp;")
            {
                this.txtPatientPhone.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[9].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[10].Text != "&nbsp;")
            {
                this.txtPatientEmail.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[10].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[11].Text != "&nbsp;")
            {
                this.txtMI.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[11].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[12].Text != "&nbsp;")
            {
                this.txtWCBNumber.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[12].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[13].Text != "&nbsp;")
            {
                this.txtSocialSecurityNumber.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[13].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[15].Text != "&nbsp;")
            {
                this.txtDateOfBirth.Text = Convert.ToDateTime(this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[15].Text).ToShortDateString();
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x10].Text != "&nbsp;")
            {
                this.ddlSex.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x10].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x11].Text != "&nbsp;")
            {
                this.txtDateOfInjury.Text = Convert.ToDateTime(this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x11].Text).ToShortDateString();
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x12].Text != "&nbsp;")
            {
                this.txtJobTitle.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x12].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x13].Text != "&nbsp;")
            {
                this.txtWorkActivites.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x13].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[20].Text != "&nbsp;")
            {
                this.txtState.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[20].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x15].Text != "&nbsp;")
            {
                this.txtCarrierCaseNo.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x15].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x16].Text != "&nbsp;")
            {
                this.txtEmployerName.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x16].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x17].Text != "&nbsp;")
            {
                this.txtEmployerPhone.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x17].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x18].Text != "&nbsp;")
            {
                this.txtEmployerAddress.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x18].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x19].Text != "&nbsp;")
            {
                this.txtEmployerCity.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x19].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x1a].Text != "&nbsp;")
            {
                this.txtEmployerState.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x1a].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x1b].Text != "&nbsp;")
            {
                this.txtEmployerZip.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x1b].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x1c].Text != "&nbsp;")
            {
                this.txtWorkPhone.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x1c].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x1d].Text != "&nbsp;")
            {
                this.txtExtension.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x1d].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[30].Text != "&nbsp;")
            {
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[30].Text.ToString() == "True")
                {
                    this.chkWrongPhone.Checked = true;
                }
                else
                {
                    this.chkWrongPhone.Checked = false;
                }
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x1f].Text != "&nbsp;")
            {
                if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[0x1f].Text.ToString() == "True")
                {
                    this.chkTransportation.Checked = true;
                }
                else
                {
                    this.chkTransportation.Checked = false;
                }
            }
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet patientAccidentDetails = this._bill_Sys_PatientBO.GetPatientAccidentDetails(this.Session["PatientID"].ToString());
            if (patientAccidentDetails.Tables[0].Rows.Count > 0)
            {
                if (patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;")
                {
                    this.txtAccidentID.Text = patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                if (patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;")
                {
                    this.txtPlatenumber.Text = patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                }
                if (patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;")
                {
                    this.txtAccidentAddress.Text = patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                }
                if (patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;")
                {
                    this.txtAccidentCity.Text = patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                }
                if (patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() != "&nbsp;")
                {
                    this.txtAccidentState.Text = patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                }
                if (patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(6).ToString() != "&nbsp;")
                {
                    this.txtPolicyReport.Text = patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                }
                if (patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() != "&nbsp;")
                {
                    this.txtListOfPatient.Text = patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                }
                if (patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;")
                {
                    this.txtDateofAccident.Text = patientAccidentDetails.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                }
            }
            DataSet caseDetails = new Billing_Sys_ManageNotesBO().GetCaseDetails(this.Session["PatientID"].ToString());
            if (caseDetails.Tables[0].Rows.Count > 0)
            {
                this.txtPatientID.Text = this.Session["PatientID"].ToString();
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != ""))
                {
                    this.txtCaseID.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != ""))
                {
                    this.extddlCaseType.Text=caseDetails.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != ""))
                {
                    this.extddlProvider.Text=caseDetails.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != ""))
                {
                    this.extddlInsuranceCompany.Text=caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.extddlInsuranceCompany.Text).Tables[0];
                    this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    this.lstInsuranceCompanyAddress.DataValueField = "CODE";
                    this.lstInsuranceCompanyAddress.DataBind();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != ""))
                {
                    this.extddlCaseStatus.Text=caseDetails.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() != ""))
                {
                    this.extddlAttorney.Text=caseDetails.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() != ""))
                {
                    this.txtClaimNumber.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != ""))
                {
                    this.txtPolicyNumber.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() != ""))
                {
                    this.txtDateofAccident.Text = Convert.ToDateTime(caseDetails.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()).ToShortDateString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() != ""))
                {
                    this.extddlAdjuster.Text=caseDetails.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() != ""))
                {
                    this.txtAssociateDiagnosisCode.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != ""))
                {
                    this.txtPlatenumber.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() != ""))
                {
                    this.txtAccidentAddress.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x10).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x10).ToString() != ""))
                {
                    this.txtAccidentCity.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x10).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x11).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x11).ToString() != ""))
                {
                    this.txtAccidentState.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x11).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x12).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x12).ToString() != ""))
                {
                    this.txtPolicyReport.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x12).ToString();
                }
                if ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x13).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x13).ToString() != ""))
                {
                    this.txtListOfPatient.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0x13).ToString();
                }
                if (((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(20).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(20).ToString() != "")) && ((caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;") && (caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "")))
                {
                    try
                    {
                        this.lstInsuranceCompanyAddress.SelectedValue = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                        ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(this.lstInsuranceCompanyAddress.SelectedValue);
                        if (insuranceAddressDetail.Count > 0)
                        {
                            this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
                            this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
                            this.txtInsuranceState.Text = insuranceAddressDetail[4].ToString();
                            this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
                            this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
                        }
                    }
                    catch
                    {
                    }
                }
                if (this.txtAssociateDiagnosisCode.Text == "1")
                {
                    this.chkAssociateCode.Checked = true;
                }
                else
                {
                    this.chkAssociateCode.Checked = false;
                }
            }
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            ArrayList adjusterDetail = new ArrayList();
            adjusterDetail = this._bill_Sys_PatientBO.GetAdjusterDetail(this.extddlAdjuster.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (adjusterDetail.Count > 0)
            {
                this.txtAdjusterPhone.Text = adjusterDetail[0].ToString();
                this.txtAdjusterExtension.Text = adjusterDetail[1].ToString();
                this.txtfax.Text = adjusterDetail[2].ToString();
                this.txtEmail.Text = adjusterDetail[3].ToString();
            }
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.lblMsg.Visible = false;
            this.hlnkShowNotes.Visible = true;
            this.pnlShowNotes.Visible = true;
            this.PopEx.Enabled=true;
            this._objCaseObject = new Bill_Sys_CaseObject();
            this._objCaseObject.SZ_CASE_ID=this.txtCaseID.Text;
            this.Session["CASE_OBJECT"] = this._objCaseObject;
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

    protected void hlnkAssociate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ScriptManager.RegisterClientScriptBlock(this.hlnkAssociate, typeof(Button), "Msg", "window.open('Bill_Sys_AssociateDignosisCode.aspx?CaseId=" + this.Session["PassedCaseID"].ToString() + "','','titlebar=no,menubar=yes,resizable,alwaysRaised,scrollbars=yes,width=800,height=800,center ,top=0,left=0'); document.getElementById('ImageDiv').style.visibility = 'hidden'; ", true);
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

    protected void lstInsuranceCompanyAddress_Load(object sender, EventArgs e)
    {
    }

    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(this.lstInsuranceCompanyAddress.SelectedValue);
            this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
            this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
            this.txtInsuranceState.Text = insuranceAddressDetail[4].ToString();
            this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
            this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
            if (insuranceAddressDetail[8].ToString() != "")
            {
                this.extddlInsuranceState.Text=insuranceAddressDetail[8].ToString();
            }
            this.Page.MaintainScrollPositionOnPostBack = true;
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

    protected override void OnUnload(EventArgs e)
    {
        this.Session["Flag"] = null;
        base.OnUnload(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtUserId.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this.aceInsSearch.ContextKey = (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            DataSet set = new DataSet();
            set = this.BindMadatoryField();
            if (set.Tables[0].Rows.Count > 0)
            {
                string str = null;
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    if (set.Tables[0].Rows.Count > 1)
                    {
                        if (str == null)
                        {
                            str = "'frmPatient','txtPatientFName,txtPatientLName,extddlCaseType," + set.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                            string id = set.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                            id = "l" + id;
                            Label label = (Label)this.Page.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl(id);
                            label.Visible = true;
                        }
                        if (i == (set.Tables[0].Rows.Count - 1))
                        {
                            str = str + "," + set.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString() + "'";
                            string str3 = set.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                            str3 = "l" + str3;
                            Label label2 = (Label)this.Page.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl(str3);
                            label2.Visible = true;
                        }
                        else
                        {
                            str = str + "," + set.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                            string str4 = set.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                            str4 = "l" + str4;
                            Label label3 = (Label)this.Page.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl(str4);
                            label3.Visible = true;
                        }
                    }
                    else
                    {
                        str = "'frmPatient','txtPatientFName,txtPatientLName,extddlCaseType," + set.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString() + "'";
                        string str5 = set.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                        str5 = "l" + str5;
                        Label label4 = (Label)this.Page.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl(str5);
                        label4.Visible = true;
                    }
                }
                this.btnSave.Attributes.Add("onclick", "return formValidator(" + str + ");");
            }
            else
            {
                this.btnSave.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,extddlCaseType,ddlSex');");
            }
            this.btnUpdate.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,txtDateOfBirth,txtPatientAddress,txtPatientCity,txtPatientPhone,txtDateofAccident,extddlCaseType,extddlCaseStatus');");
            this.btnAddAttorney.Attributes.Add("onclick", "return PopupformValidator('frmAttorney','txtAttorneyFirstName,txtAttorneyLastName,txtAttorneyCity','AttorneyErrordiv');");
            this.btnSaveAdjuster.Attributes.Add("onclick", "return PopupformValidator('aspnetForm','txtAdjusterPopupName','AdjusterErrordiv');");
            this.txtPatientEmail.Attributes.Add("onfocusout", "return isEmail('frmPatient','txtPatientEmail');");
            this.btnSaveInsurance.Attributes.Add("onclick", "return PopupformValidator('aspnetForm','txtInsuranceCompanyName','InsuranceErrordiv');");
            this.txtInsuranceEmail.Attributes.Add("onfocusout", "return isEmail('frmPatient','txtInsuranceEmail');");
            this.lblChart.Visible = false;
            this.txtChartNo.Visible = false;
            this.txtRefChartNumber.Visible = false;
            this.chkAutoIncr.Visible = false;
            this.lblChartNo.Visible = false;
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
            if (!flag && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                this.lblChart.Visible = false;
                this.txtChartNo.Visible = false;
            }
            else if (!flag && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "0"))
            {
                this.lblChart.Visible = true;
                this.txtChartNo.Visible = true;
            }
            if (flag || (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            {
                this.lblLocation.Visible = false;
                this.extddlLocation.Visible = false;
            }
            else
            {
                this.lblLocation.Visible = true;
                this.extddlLocation.Visible = true;
            }
            this.extddlCaseType.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlProvider.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlCaseStatus.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlAttorney.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlAdjuster.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlInsuranceCompany.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlInsuranceCode.Flag_ID = this.txtCompanyID.Text.ToString();
            this.extddlLocation.Flag_ID = this.txtCompanyID.Text.ToString();
            if ((this.Session["Flag"] != null) && (this.Session["Flag"].ToString() == "true"))
            {
                this.grdPatientList.Visible = false;
                this.btnUpdate.Visible = false;
            }
            else if (!base.IsPostBack)
            {
                this.btnUpdate.Enabled = false;
            }
            this.extddlCaseStatus.Text = this.GetOpenCaseStatus();
            this.extddlCaseStatus.Enabled = false;
            if (Session["CASETYPE"] != null)
            {
                if (this.extddlCaseType.Text == this.GetWCCaseType())
                {
                    this.lblWcbNumber.Text = "Policy Number";
                }
                else if (this.extddlCaseType.Text == this.GetNFCaseType())
                {
                    this.lblWcbNumber.Text = "Policy Number";
                }
            }
            this.GetRefCompany();
            this.GetPreviousChartNo(this.txtCompanyID.Text);

            if (!IsPostBack)
            {
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).IS_EMPLOYER == "1")
                {
                    hdnIsempl.Value = "1";
                   // pnlInsurance.Visible = false;
                  //tblEmployerInfo.Visible = false;
                    Label10.Visible = false;
                    td1.Visible = false;
                    txtEmployerName.Visible = false;
                    td2.Visible = false;
                    txtEmployerAddress.Visible = false;
                    ltxtEmployerAddress.Visible = false;
                    td3.Visible = false;
                    txtEmployerCity.Visible = false;
                    ltxtEmployerCity.Visible = false;
                    td4.Visible = false;
                    extddlEmployerState.Visible = false;
                    ltxtEmployerState.Visible = false;
                    td5.Visible = false;
                    txtEmployerZip.Visible = false;
                    ltxtEmployerZip.Visible = false;
                    txtEmployerPhone.Visible = false;
                    ltxtEmployerPhone.Visible = false;
                  
                    td6.Visible = false;

                    tblEmployer.Visible = true;
                    extddlEmployer.Flag_ID = txtCompanyID.Text;
                }
                else
                {
                     hdnIsempl.Value = "0";
                    //pnlInsurance.Visible = true;
                   // tblEmployerInfo.Visible = true;
                    Label10.Visible = true;
                    td1.Visible = true;
                    txtEmployerName.Visible = true;
                    td2.Visible = true;
                    txtEmployerAddress.Visible = true;
                    ltxtEmployerAddress.Visible = true;
                    td3.Visible = true;
                    txtEmployerCity.Visible = true;
                    ltxtEmployerCity.Visible = true;
                    td4.Visible = true;
                    extddlEmployerState.Visible = true;
                    ltxtEmployerState.Visible = true;
                    td5.Visible = true;
                    txtEmployerZip.Visible = true;
                    ltxtEmployerZip.Visible = true;
                    txtEmployerPhone.Visible = true;
                    ltxtEmployerPhone.Visible = true;

                    td6.Visible = true;
                    tblEmployer.Visible = false;
                }
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
        
        this.txtInsuranceAddress.Text = "";
        this.txtInsuranceCity.Text = "";
        this.txtInsuranceZip.Text = "";
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_Patient.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SaveData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._saveOperation = new SaveOperation();
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            string s = txtEmployerName.Text;
 //           return;
            if (this._associateDiagnosisCodeBO.GetCaseTypeName(this.extddlCaseType.Text) == "WC000000000000000001")
            {
                this.txtAssociateDiagnosisCode.Text = "1";
            }
            else
            {
                this.txtAssociateDiagnosisCode.Text = "0";
            }
            if (this.chkAutoIncr.Checked)
            {
                this.txtRefChartNumber.Text = "";
                string maxChartNumber = "";
                maxChartNumber = this._bill_Sys_PatientBO.GetMaxChartNumber(this.txtCompanyID.Text);
                if (maxChartNumber != "")
                {
                    this.txtRefChartNumber.Text = maxChartNumber;
                }
                else
                {
                    this.txtRefChartNumber.Text = "1";
                }
            }
            string INS = extddlInsuranceCompany.Text;
            new Billing_Sys_ManageNotesBO();
            this._saveOperation.WebPage=this.Page;
            this._saveOperation.Xml_File="PatientDataEntry.xml";
            this._saveOperation.SaveMethod();
            this.lblMsg.Visible = true;
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.Session["PassedCaseID"] = this._bill_Sys_PatientBO.GetLatestEnterCaseID(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this._objCaseObject = new Bill_Sys_CaseObject();
            CaseDetailsBO sbo = new CaseDetailsBO();
            this._objCaseObject.SZ_PATIENT_ID=sbo.GetCasePatientID(this.Session["PassedCaseID"].ToString(), "");
            this._objCaseObject.SZ_CASE_ID=this.Session["PassedCaseID"].ToString();
            this._objCaseObject.SZ_PATIENT_NAME=sbo.GetPatientName(this._objCaseObject.SZ_PATIENT_ID);
            this._objCaseObject.SZ_CASE_NO=sbo.GetCaseNo(this._objCaseObject.SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this._objCaseObject.SZ_COMAPNY_ID=(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.Session["CASE_OBJECT"] = this._objCaseObject;
            this.Session["QStrCaseID"] = this._objCaseObject.SZ_CASE_ID;
            this.Session["Case_ID"] = this._objCaseObject.SZ_CASE_ID;
            this.Session["Archived"] = "0";
            this.Session["QStrCID"] = this._objCaseObject.SZ_CASE_ID;
            this.Session["SelectedID"] = this._objCaseObject.SZ_CASE_ID;
            this.Session["DataEntryFlag"] = true;
            this.hlnkShowNotes.Visible = true;
            this.PopEx.Enabled=true;
            this.pnlShowNotes.Visible = true;
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).IS_EMPLOYER == "1")
            {
                this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();

                _bill_Sys_PatientBO.SaveEmployer(extddlEmployer.Text, txtEmployerAddId.Text, txtCompanyID.Text, this.Session["PassedCaseID"].ToString());
            }
            this.lblMsg.Text = " Patient Information Saved successfully ! ";
            this.Page.MaintainScrollPositionOnPostBack = false;
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

    public void SaveInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtPatientType.Text = "";
            this.txtDateofAccident.Text = UtilDate.FormatDate("", this.txtDateofAccident.Text.ToString());
            if (this.txtDateOfBirth.Text != "")
            {
                this.txtDateOfBirth.Text = UtilDate.FormatDate("", this.txtDateOfBirth.Text.ToString());
            }
            if (this.txtDateOfInjury.Text != "")
            {
                this.txtDateOfInjury.Text = UtilDate.FormatDate("", this.txtDateOfInjury.Text.ToString());
            }
            DateTime time = new DateTime();
            if (this.txtDateofAccident.Text != "")
            {
                time = Convert.ToDateTime(this.txtDateofAccident.Text);
            }
            if (this.extddlLocation.Visible || (DateTime.Today >= time))
            {
                foreach (ListItem item in this.rdolstPatientType.Items)
                {
                    if (item.Selected)
                    {
                        this.txtPatientType.Text = item.Value.ToString();
                        break;
                    }
                }
                ArrayList list = new ArrayList();
                list.Add(this.txtPatientFName.Text);
                list.Add(this.txtPatientLName.Text);
                if (this.txtDateofAccident.Text != "")
                {
                    list.Add(this.txtDateofAccident.Text);
                }
                else
                {
                    list.Add(null);
                }
                list.Add(this.extddlCaseType.Text);
                if (this.txtDateOfBirth.Text != "")
                {
                    list.Add(this.txtDateOfBirth.Text);
                }
                else
                {
                    list.Add(null);
                }
                list.Add(this.txtCompanyID.Text);
                list.Add("existpatient");
                this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                string str = this._bill_Sys_PatientBO.CheckPatientExists(list);
                if (str != "")
                {
                    this.msgPatientExists.InnerHtml = str;
                    this.Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");
                }
                else
                {
                    this.SaveData();
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="CASE_CREATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC="";
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID=(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    this._DAO_NOTES_EO.SZ_CASE_ID=(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                    this._DAO_NOTES_EO.SZ_COMPANY_ID=this.txtCompanyID.Text;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        base.Response.Redirect("AJAX Pages/Bill_Sys_ReCaseDetails.aspx", false);
                    }
                    else
                    {
                        base.Response.Redirect("AJAX Pages/Bill_Sys_CaseDetails.aspx", false);
                    }
                    this.hidIsSaved.Value = "1";
                }
            }
            else
            {
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "ss", "<script language='javascript'>alert('Accident date should not be greater than current date ...!');</script>");
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

    public void clearemployer()
    {
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).IS_EMPLOYER == "1")
        {
            extddlEmployer.Text = "NA";
            this.lstEmployerAddress.Items.Clear();
            txtEmployerAddId.Text = "";
            this.txtEmployercmpAddress.Text = "";
            this.txtEmployercmpCity.Text = "";
            this.extddlEmployercmpState.Text = "NA";
            this.txtEmployercmpZip.Text = "";
            this.txtEmployercmpStreet.Text = "";
        }
    }

    protected void extddlEmployer_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            //this.extddlInsuranceCode.Text = this.extddlInsuranceCompany.Text;

            this.lstEmployerAddress.Items.Clear();
            txtEmployerAddId.Text = "";
            this.txtEmployercmpAddress.Text = "";
            this.txtEmployercmpCity.Text = "";
            this.extddlEmployercmpState.Text = "NA";
            this.txtEmployercmpZip.Text = "";
            this.txtEmployercmpStreet.Text = "";
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstEmployerAddress.DataSource = this._bill_Sys_PatientBO.GetEmployerCompanyAddress(this.extddlEmployer.Text, txtCompanyID.Text);
            DataSet dataSource = new DataSet();
            dataSource = (DataSet)this.lstEmployerAddress.DataSource;
            this.lstEmployerAddress.DataTextField = "DESCRIPTION";
            this.lstEmployerAddress.DataValueField = "CODE";
            if (dataSource.Tables[0].Rows.Count == 0)
            {
                this.lstEmployerAddress.Items.Clear();
                txtEmployerAddId.Text = "";
            }
            else
            {
                this.lstEmployerAddress.DataBind();
                this.lstEmployerAddress.SelectedIndex = 0;
                ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetEmployerAddressDetails(dataSource.Tables[0].Rows[0][0].ToString());
                txtEmployerAddId.Text = dataSource.Tables[0].Rows[0][0].ToString();
                this.txtEmployercmpAddress.Text = insuranceAddressDetail[2].ToString();
                this.txtEmployercmpCity.Text = insuranceAddressDetail[3].ToString();
                this.extddlEmployercmpState.Text = insuranceAddressDetail[4].ToString();
                this.txtEmployercmpZip.Text = insuranceAddressDetail[5].ToString();
                this.txtEmployercmpStreet.Text = insuranceAddressDetail[6].ToString();
                if (insuranceAddressDetail[8].ToString() != "")
                {
                    this.extddlEmployercmpState.Text = insuranceAddressDetail[8].ToString();
                }
                this.Page.MaintainScrollPositionOnPostBack = true;
            }
            //new PopupBO();
            //this.Page.MaintainScrollPositionOnPostBack = true;
            //if (this.lstInsuranceCompanyAddress.Items.Count == 1)
            //{
            //    ArrayList list2 = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(dataSource.Tables[0].Rows[0][0].ToString());
            //    this.txtInsuranceAddress.Text = list2[2].ToString();
            //    this.txtInsuranceCity.Text = list2[3].ToString();
            //    this.txtInsuranceState.Text = list2[4].ToString();
            //    this.extddlInsuranceState.Text = list2[4].ToString();
            //    this.txtInsuranceZip.Text = list2[5].ToString();
            //    this.txtInsuranceStreet.Text = list2[6].ToString();
            //    if (list2[8].ToString() != "")
            //    {
            //        this.extddlInsuranceState.Text = list2[8].ToString();
            //    }
            //    this.Page.MaintainScrollPositionOnPostBack = true;
            //}
            ArrayList defaultDetailnew = new ArrayList();
            defaultDetailnew = this._bill_Sys_PatientBO.GetDefaultDetail(this.extddlEmployer.Text);
            if (defaultDetailnew.Count != 0)
            {
                this.txtEmployercmpAddress.Text = defaultDetailnew[1].ToString();
                for (int i = 0; i < this.lstEmployerAddress.Items.Count; i++)
                {
                    if (this.lstEmployerAddress.Items[i].Value == defaultDetailnew[0].ToString())
                    {
                        this.lstEmployerAddress.SelectedIndex = i;
                        break;
                    }
                }
                txtEmployerAddId.Text = defaultDetailnew[0].ToString();
                this.txtEmployercmpCity.Text = defaultDetailnew[2].ToString();
                this.txtEmployercmpZip.Text = defaultDetailnew[3].ToString();
                this.txtEmployercmpStreet.Text = defaultDetailnew[4].ToString();
                this.extddlEmployercmpState.Text = defaultDetailnew[6].ToString();
                this.extddlInsuranceState.Text = defaultDetailnew[5].ToString();
                this.Page.MaintainScrollPositionOnPostBack = true;
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

    protected void lstEmployerAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.txtEmployercmpAddress.Text = "";
            this.txtEmployercmpCity.Text = "";
            this.extddlEmployercmpState.Text = "NA";
            this.txtEmployercmpZip.Text = "";
            this.txtEmployercmpStreet.Text = "";
            ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetEmployerAddressDetails(this.lstEmployerAddress.SelectedValue);
            txtEmployerAddId.Text = this.lstEmployerAddress.SelectedValue;

            this.txtEmployercmpAddress.Text = insuranceAddressDetail[2].ToString();
            this.txtEmployercmpCity.Text = insuranceAddressDetail[3].ToString();

            this.txtEmployercmpZip.Text = insuranceAddressDetail[5].ToString();
            this.txtEmployercmpStreet.Text = insuranceAddressDetail[6].ToString();
            if (insuranceAddressDetail[8].ToString() != "")
            {
                this.extddlEmployercmpState.Text = insuranceAddressDetail[8].ToString();
            }
            this.Page.MaintainScrollPositionOnPostBack = true;
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
}

