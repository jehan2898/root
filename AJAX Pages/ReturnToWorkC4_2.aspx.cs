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
public partial class ReturnToWorkC4_2 : PageBase
{

    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;
    private workers_templateC4_2 _workerstemplate;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        chkPatientReturnToWorkWithoutLimitation.Attributes.Add("onclick", "checkvalidate_b();");
        chkPatientcannotReturn.Attributes.Add("onclick", "checkvalidate_a();");
        chkPatientReturnToWorkWithlimitation.Attributes.Add("onclick", "checkvalidate_c();");
        chklstPatientLimitationAllReason.Attributes.Add("onclick", "checkvalidate_b();");

        try
        {
            if (Session["TEMPLATE_BILL_NO"] != null)
            {
                txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString(); //"sas0000001";
            }
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;


            if (!IsPostBack)
            {
                _workerstemplate = new workers_templateC4_2();
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                txtPatientID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNumber.Text, "SP_MST_PATIENT_INFORMATION", "GETPATIENTID");
                txtWorkStatusID.Text = _workerstemplate.GetWorkStatusLatestID(txtBillNumber.Text);
                LoadData();
                LoadOtherInformation();
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("ReturnToWorkC4_2.aspx");
        }
        #endregion
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void LoadData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtWorkStatusID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "WorkStatusC4_2.xml";
            _editOperation.LoadData();
            if (hdntxtPatientCanReturnWork.Text == "0")
            {
                chkPatientcannotReturn.Checked = true;
                txtPatientcannotReturn.Text = hdntxtPatientCanReturnWorkDescription.Text;
            }
            else if (hdntxtPatientCanReturnWork.Text == "1")
            {
                chkPatientReturnToWorkWithoutLimitation.Checked = true;
                txtPatientWorkWithoutLimitaion.Text = hdntxtPatientCanReturnWorkDescription.Text;
            }
            else if (hdntxtPatientCanReturnWork.Text == "2")
            {
                chkPatientReturnToWorkWithlimitation.Checked = true;
                txtPatientWorkWithLimitaion.Text = hdntxtPatientCanReturnWorkDescription.Text;
            }
            if (txtPatientWorkWithLimitaion.Text != "")
            {
                txtPatientWorkWithLimitaion.Text = Convert.ToDateTime(txtPatientWorkWithLimitaion.Text).ToShortDateString();
            }
            if (txtPatientWorkWithoutLimitaion.Text != "")
            {
                txtPatientWorkWithoutLimitaion.Text = Convert.ToDateTime(txtPatientWorkWithoutLimitaion.Text).ToShortDateString();
            }


            _editOperation.Primary_Value = txtBillNumber.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "providerinformation.xml";
            _editOperation.LoadData();

            if (txtProviderDate.Text != "")
            {
                txtProviderDate.Text = Convert.ToDateTime(txtProviderDate.Text).ToShortDateString();
            }
            txtAuthProviderName.Text = txtProviderName.Text;
            txtAuthProviderSpeciality.Text = txtProviderSpeciality.Text;
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
    private void SaveData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _saveOperation = new SaveOperation();
        _editOperation = new EditOperation();

        _workerstemplate = new workers_templateC4_2();
        try
        {
            if (txtWorkStatusID.Text != "")
            {
                if (chkPatientcannotReturn.Checked == false && chkPatientReturnToWorkWithlimitation.Checked == false && chkPatientReturnToWorkWithoutLimitation.Checked == false)
                {
                    hdntxtPatientCanReturnWork.Text = "5";
                    txtPatientWorkWithLimitaion.Text = "";
                    txtPatientWorkWithoutLimitaion.Text = "";
                    txtQuantifyTheLimitaion.Text = "";
                    txtOtherLimitation.Text = "";
                    _editOperation.Xml_File = "WorkStatusC4_2.xml";
                    _editOperation.WebPage = this.Page;
                    _editOperation.Primary_Value = txtWorkStatusID.Text;
                    _editOperation.UpdateMethod();
                }

                else
                {

                    if (chkPatientcannotReturn.Checked)
                    {
                        hdntxtPatientCanReturnWork.Text = "0";
                        hdntxtPatientCanReturnWorkDescription.Text = txtPatientcannotReturn.Text;
                    }
                    if (chkPatientReturnToWorkWithoutLimitation.Checked)
                    {
                        hdntxtPatientCanReturnWork.Text = "1";
                        hdntxtPatientCanReturnWorkDescription.Text = txtPatientWorkWithoutLimitaion.Text;
                    }
                    else if (chkPatientReturnToWorkWithlimitation.Checked)
                    {
                        hdntxtPatientCanReturnWork.Text = "2";
                        hdntxtPatientCanReturnWorkDescription.Text = txtPatientWorkWithLimitaion.Text;
                    }
                    //rdlstWorkRestriction
                    _editOperation.Xml_File = "WorkStatusC4_2.xml";
                    _editOperation.WebPage = this.Page;
                    _editOperation.Primary_Value = txtWorkStatusID.Text;
                    _editOperation.UpdateMethod();
                }
            }
            else
            {
                if (chkPatientcannotReturn.Checked == false && chkPatientReturnToWorkWithlimitation.Checked == false && chkPatientReturnToWorkWithoutLimitation.Checked == false)
                {
                    hdntxtPatientCanReturnWork.Text = "5";
                    txtPatientWorkWithLimitaion.Text = "";
                    txtPatientWorkWithoutLimitaion.Text = "";
                    txtQuantifyTheLimitaion.Text = "";
                    txtOtherLimitation.Text = "";
                    _saveOperation.WebPage = this.Page;
                    _saveOperation.Xml_File = "WorkStatusC4_2.xml";
                    _saveOperation.SaveMethod();
                    txtWorkStatusID.Text = _workerstemplate.GetWorkStatusLatestID(txtBillNumber.Text);
                }
                else
                {

                    if (chkPatientcannotReturn.Checked)
                    {
                        hdntxtPatientCanReturnWork.Text = "0";
                        hdntxtPatientCanReturnWorkDescription.Text = txtPatientcannotReturn.Text;
                    }
                    if (chkPatientReturnToWorkWithoutLimitation.Checked)
                    {
                        hdntxtPatientCanReturnWork.Text = "1";
                        hdntxtPatientCanReturnWorkDescription.Text = txtPatientWorkWithoutLimitaion.Text;
                    }
                    else if (chkPatientReturnToWorkWithlimitation.Checked)
                    {
                        hdntxtPatientCanReturnWork.Text = "2";
                        hdntxtPatientCanReturnWorkDescription.Text = txtPatientWorkWithLimitaion.Text;

                    }
                    if (rdlstWorkRestriction.SelectedValue != "1")
                    {
                        txtDescribeWorkRestriction.Text = "";
                    }
                    _saveOperation.WebPage = this.Page;
                    _saveOperation.Xml_File = "WorkStatusC4_2.xml";
                    _saveOperation.SaveMethod();
                    txtWorkStatusID.Text = _workerstemplate.GetWorkStatusLatestID(txtBillNumber.Text);
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
    private void SaveWorkStatusDetails()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (chkPatientcannotReturn.Checked)
            {
            }
           else if (chkPatientReturnToWorkWithoutLimitation.Checked)
            {
            }
            else if (chkPatientReturnToWorkWithlimitation.Checked)
            {
                foreach (ListItem _objLI in chklstPatientLimitationAllReason.Items)
                {
                    if (_objLI.Selected == true)
                    {
                        ArrayList _objAL = new ArrayList();
                        _objAL.Add(txtWorkStatusID.Text);
                        _objAL.Add(_objLI.Text);
                        _objAL.Add(txtPatientID.Text.ToString());
                        _objAL.Add(txtCompanyID.Text.ToString());
                        if (_objLI.Text == "Other (explain):")
                        {
                            _objAL.Add(txtOtherLimitation.Text);
                        }
                        else
                        {
                            _objAL.Add("");
                        }
                        _objAL.Add("ADD");

                        _workerstemplate = new workers_templateC4_2();
                        _workerstemplate.SavePatientLimitations(_objAL);
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
    public void DeleteLimitations()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        WorkerTemplate _objWT;
        try
        {
            ArrayList _objAL = new ArrayList();
            _objAL.Add(txtWorkStatusID.Text.ToString());
            _objAL.Add("");
            _objAL.Add("");
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add("");
            _objAL.Add("DELETEALL");
            _workerstemplate = new workers_templateC4_2();
            _workerstemplate.SavePatientLimitations(_objAL);

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
    protected void btnSaveAndGoToNext_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            SaveData();
            DeleteLimitations();
            SaveWorkStatusDetails();
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

        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.close('../ReturnToWorkC4_2.aspx'); ", true);
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    protected void LoadOtherInformation()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        DataSet _objDataSet = new DataSet();
        try
        {
            _workerstemplate = new workers_templateC4_2();
            _objDataSet = _workerstemplate.GetWorkStatusLimitation(txtPatientID.Text);

            foreach (DataRow obj1 in _objDataSet.Tables[0].Rows)
            {
                foreach (ListItem _objLI in chklstPatientLimitationAllReason.Items)
                {
                    if (_objLI.Text == obj1[1].ToString())
                    {
                        _objLI.Selected = true;
                        if (obj1[1].ToString() == "Other (explain):")
                        {
                            txtOtherLimitation.Text = obj1[2].ToString();
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

}
