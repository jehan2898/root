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

public partial class Bill_Sys_History_OT_PT : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;
    private workers_templateC4_2 _workerstemplate;
    private OTPT_HISTROY_DAO _OTPT_HISTROY_DAO;
    private OTPT _objOTPT;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //chkPatientReturnToWorkWithoutLimitation.Attributes.Add("onclick", "checkvalidate_a();");
        //chkPatientReturnToWorkWithlimitation.Attributes.Add("onclick", "checkvalidate_b();");
        //chklstPatientLimitationAllReason.Attributes.Add("onclick", "checkvalidate_a();");

        try
        {
            //txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString(); //"sas0000001";
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
            if (Request.QueryString["billnumber"] != null)
            {
                Session["BILL_NO"] = Request.QueryString["billnumber"];
            }
            if (Request.QueryString["caseid"] != null)
            {
                Session["CASE_ID"] = Request.QueryString["caseid"];
            }
            txtBillNumber.Text = Session["BILL_NO"].ToString();// "sas0000001";           
            txtCaseID.Text = Session["CASE_ID"].ToString();

            if (!IsPostBack)
            {
                _workerstemplate = new workers_templateC4_2();
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                txtPatientID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNumber.Text, "SP_MST_PATIENT_INFORMATION", "GETPATIENTID");
                txtWorkStatusID.Text = _workerstemplate.GetWorkStatusLatestID(txtBillNumber.Text);
                 LoadData();
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
            cv.MakeReadOnlyPage("Bill_Sys_ReturnToWorkC4_2.aspx");
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
        _objOTPT = new OTPT();
        _OTPT_HISTROY_DAO = new OTPT_HISTROY_DAO();
        DataSet dsgethistroy = new DataSet();
        try
        {
            dsgethistroy = _objOTPT.GET_Histroy_Information_OTPT(txtCaseID.Text, txtCompanyID.Text);
            if (dsgethistroy.Tables.Count > 0)
            {
                if (dsgethistroy.Tables[0].Rows.Count > 0)
                {
                    if (dsgethistroy.Tables[0].Rows[0].ToString() != "" && dsgethistroy.Tables[0].Rows[0].ToString() != null)
                    {
                        txtDiagnosis.Text = dsgethistroy.Tables[0].Rows[0]["SZ_PHYSICIAN_REFERRING"].ToString();
                        txtprehistroyinjury.Text = dsgethistroy.Tables[0].Rows[0]["SZ_PRE_INJURY_HISTROY"].ToString();

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
        _OTPT_HISTROY_DAO = new OTPT_HISTROY_DAO();
        _objOTPT = new OTPT();
        try
        {
            _OTPT_HISTROY_DAO.SZ_PATIENT_ID = txtPatientID.Text;
            _OTPT_HISTROY_DAO.SZ_CASE_ID = txtCaseID.Text;
            _OTPT_HISTROY_DAO.SZ_BILL_NO = txtBillNumber.Text;
            _OTPT_HISTROY_DAO.SZ_PHYSICIAN_REFERRING = txtDiagnosis.Text;
            _OTPT_HISTROY_DAO.SZ_PRE_INJURY_HISTROY = txtprehistroyinjury.Text;
            _OTPT_HISTROY_DAO.SZ_COMPANY_ID = txtCompanyID.Text;
            _objOTPT.Save_History_Info(_OTPT_HISTROY_DAO);
            
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
            Response.Redirect("Bill_Sys_Treatment_OT-PT.aspx", false);
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
