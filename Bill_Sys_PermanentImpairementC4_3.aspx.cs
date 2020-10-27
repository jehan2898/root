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
public partial class Bill_Sys_PermanentImpairementC4_3 : PageBase
{
    private EditOperation _editOperation;
    private SaveOperation _saveOperation;
    private ArrayList _arrayList;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;
    private Workers_TemplateC4_3 _workersTemplate;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString();// "sas0000019";// Session["TEMPLATE_BILL_NO"].ToString(); //"sas0000001";
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;

            
            if (!IsPostBack)
            {
                _workersTemplate = new Workers_TemplateC4_3();
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                txtPatientID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNumber.Text, "SP_MST_PATIENT_INFORMATION", "GETPATIENTID");
                txtPermanentImpairmentID.Text = _workersTemplate.GetWorkStatusLatestID(txtBillNumber.Text);
                LoadData();
                LoadOtherInformation();                
                LoadPermanentImpairementDetailsList();
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
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_PermanentImpairementC4_3.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtPermanentImpairmentID.Text;
            _editOperation.Xml_File = "PermanentImpairment.xml";
            _editOperation.WebPage = this.Page;
            _editOperation.LoadData();

            if (txtPatientReachedMMI.Text != "")
            {
                txtPatientReachedMMI.Text = Convert.ToDateTime(txtPatientReachedMMI.Text).ToShortDateString();
            }

            if (txtDateOfInjury.Text != "")
            {
                txtDateOfInjury.Text = Convert.ToDateTime(txtDateOfInjury.Text).ToShortDateString();
            }
            _editOperation.Primary_Value = txtBillNumber.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "providerinformation.xml";
            _editOperation.LoadData();

            if (txtProviderDate.Text != "")
            {
                txtProviderDate.Text = Convert.ToDateTime(txtProviderDate.Text).ToShortDateString();
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

    protected void LoadOtherInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet _objDataSet = new DataSet();
        try
        {
            _workersTemplate = new Workers_TemplateC4_3();
            _objDataSet = _workersTemplate.GetWorkStatusLimitation(txtPermanentImpairmentID.Text);

            foreach (DataRow obj1 in _objDataSet.Tables[0].Rows)
            {
                foreach (ListItem _objLI in chklstPatientLimitationAllReason.Items)
                {
                    if (_objLI.Text == obj1[1].ToString())
                    {
                        _objLI.Selected = true;
                    }
                    if (obj1[1].ToString() == "Other (explain):")
                    {
                        txtOtherLimitation.Text = obj1[2].ToString();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void LoadPermanentImpairementDetailsList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _workersTemplate = new Workers_TemplateC4_3();
        try
        {
            if (txtPermanentImpairmentID.Text != "")
            {
                DataSet _ds = _workersTemplate.GetImpairmentData(txtPermanentImpairmentID.Text);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        if (_ds.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == "0")
                        {
                            chkScheduleLoss.Checked = true;
                            txtSLBodyParts.Text=_ds.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                            txtSLImpairment.Text=_ds.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                            txtSLDignosticFinding.Text=_ds.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();
                            txtSLImpairmentDetermine.Text=_ds.Tables[0].Rows[i].ItemArray.GetValue(4).ToString();
                           
                        }
                        if (_ds.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == "1")
                        {
                            chkDisfigurement.Checked = true;
                            txtDisfigurementDesc.Text = _ds.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();
                        }
                        if (_ds.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == "2")
                        {
                            chkNonScheduleLoss.Checked = true;
                            txtNSLBodyParts.Text = _ds.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                            txtNSLImpairment.Text=_ds.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                            txtNSLDescribeDiagnosis.Text=_ds.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();
                            txtNSLImpairmentDetermine.Text = _ds.Tables[0].Rows[i].ItemArray.GetValue(4).ToString();
                        }
                        if (_ds.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == "3")
                        {
                            chkMultipleImpairments.Checked = true;
                            txtCombinedAggregate.Text = _ds.Tables[0].Rows[i].ItemArray.GetValue(6).ToString();
                            txtExplainAggregateImpairment.Text = _ds.Tables[0].Rows[i].ItemArray.GetValue(7).ToString();

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
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
        _saveOperation = new SaveOperation();
        _editOperation = new EditOperation();
        _workersTemplate = new Workers_TemplateC4_3();
        try
        {
            if (txtPermanentImpairmentID.Text != "")
            {
                _editOperation.Primary_Value = txtPermanentImpairmentID.Text;
                _editOperation.Xml_File = "PermanentImpairment.xml";
                _editOperation.WebPage = this.Page;
                _editOperation.UpdateMethod();
            }
            else
            {
                if (rdlstPatientBenefit.SelectedValue != "0")
                {
                    txtPatientBenefitDesc.Text = "";
                }
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "PermanentImpairment.xml";
                _saveOperation.SaveMethod();
                txtPermanentImpairmentID.Text = _workersTemplate.GetWorkStatusLatestID(txtBillNumber.Text);
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
    private void SaveWorkStatusDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DeleteLimitations();
            foreach (ListItem _objLI in chklstPatientLimitationAllReason.Items)
            {
                if (_objLI.Selected == true)
                {
                    ArrayList _objAL = new ArrayList();
                    _objAL.Add(txtPermanentImpairmentID.Text);
                    _objAL.Add(_objLI.Text);
                    _objAL.Add(txtPatientID.Text.ToString());
                    _objAL.Add(txtCompanyID.Text.ToString());
                    _objAL.Add("ADD");
                    if (_objLI.Text=="Other (explain):")
                    {
                        _objAL.Add(txtOtherLimitation.Text.ToString());
                    }
                    else
                    {
                        _objAL.Add("");
                    }
                    _workersTemplate = new Workers_TemplateC4_3();
                    _workersTemplate.SavePatientLimitations(_objAL);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void SavePermanentImpairementDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _workersTemplate = new Workers_TemplateC4_3();
        try
        {
            _workersTemplate.DeleteData(txtPermanentImpairmentID.Text);
            if (txtPermanentImpairmentID.Text != "")
            {
                if (chkScheduleLoss.Checked)
                {
                    _arrayList = new ArrayList();
                    _arrayList.Add("0");
                    _arrayList.Add(txtSLBodyParts.Text);
                    _arrayList.Add(txtSLImpairment.Text);
                    _arrayList.Add(txtSLDignosticFinding.Text);
                    _arrayList.Add(txtSLImpairmentDetermine.Text);
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add(txtPermanentImpairmentID.Text);
                    _workersTemplate.SaveData(_arrayList);
                }
                if (chkDisfigurement.Checked)
                {
                    _arrayList = new ArrayList();
                    _arrayList.Add("1");
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add(txtDisfigurementDesc.Text);
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add(txtPermanentImpairmentID.Text);
                    _workersTemplate.SaveData(_arrayList);
                }
                if (chkNonScheduleLoss.Checked)
                {
                    _arrayList = new ArrayList();
                    _arrayList.Add("2");
                    _arrayList.Add(txtNSLBodyParts.Text);
                    _arrayList.Add(txtNSLImpairment.Text);
                    _arrayList.Add(txtNSLDescribeDiagnosis.Text);
                    _arrayList.Add(txtNSLImpairmentDetermine.Text);
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add(txtPermanentImpairmentID.Text);
                    _workersTemplate.SaveData(_arrayList);
                }
                if (chkMultipleImpairments.Checked)
                {
                    _arrayList = new ArrayList();
                    _arrayList.Add("3");
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add("");
                    _arrayList.Add(txtCombinedAggregate.Text);
                    _arrayList.Add(txtExplainAggregateImpairment.Text);
                    _arrayList.Add(txtPermanentImpairmentID.Text);
                    _workersTemplate.SaveData(_arrayList);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public void DeleteLimitations()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ArrayList _objAL = new ArrayList();
            _objAL.Add(txtPermanentImpairmentID.Text);
            _objAL.Add("");
            _objAL.Add("");            
            _objAL.Add(txtCompanyID.Text.ToString());
            _objAL.Add("DELETEALL");
            _objAL.Add("");
            _workersTemplate = new Workers_TemplateC4_3();
            _workersTemplate.SavePatientLimitations(_objAL);

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


    protected void btnSaveAndGoToNext_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveData();
           
            SaveWorkStatusDetails();
            SavePermanentImpairementDetails();
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
