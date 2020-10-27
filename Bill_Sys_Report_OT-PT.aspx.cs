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

public partial class Bill_Sys_ReportAndHistory_OT_PT : PageBase
{
    public OTPT objOTPT;
    public OTPT_DAO obj_OTPT_DAO;
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;
    private workers_templateC4_2 _workerstemplate;
    protected void Page_Load(object sender, EventArgs e)
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString(); //"sas0000001";
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;


            if (!IsPostBack)
            {
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
                _workerstemplate = new workers_templateC4_2();
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                txtPatientID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNumber.Text, "SP_MST_PATIENT_INFORMATION", "GETPATIENTID");
                txtWorkStatusID.Text = _workerstemplate.GetWorkStatusLatestID(txtBillNumber.Text);
                LoadData();
                //LoadOtherInformation();
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
        //#region "check version readonly or not"
        //string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        //if (app_status.Equals("True"))
        //{
        //    Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
        //    cv.MakeReadOnlyPage("Bill_Sys_ReturnToWorkC4_2.aspx");
        //}
        //#endregion
    }

    private void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds_report_info = new DataSet();
        objOTPT = new OTPT();
        try
        {
            ds_report_info = objOTPT.GET_Report_Information_OTPT(txtCaseID.Text, txtCompanyID.Text);
            if (ds_report_info.Tables.Count > 0)
            {
                if (ds_report_info.Tables[0].Rows.Count > 0)
                {
                    if (ds_report_info.Tables[0].Rows[0].ToString() != "" && ds_report_info.Tables[0].Rows[0].ToString() != null)
                    {
                        if (ds_report_info.Tables[0].Rows[0]["BT_OCCUPATIOANL_THERP_REPORT"].ToString().Trim()== "1")
                        {
                            chkOCCUPATIONAL.Checked = true;

                        }
                        else if (ds_report_info.Tables[0].Rows[0]["BT_OCCUPATIOANL_THERP_REPORT"].ToString().Trim()== "0")
                        {
                            chkOCCUPATIONAL.Checked = false;

                        }
                        if (ds_report_info.Tables[0].Rows[0]["BT_PHYSICAL_THERP_REPORT"].ToString().Trim()== "1")
                        {
                            chkPHYSICAL.Checked = true;

                        }
                        else if (ds_report_info.Tables[0].Rows[0]["BT_PHYSICAL_THERP_REPORT"].ToString().Trim()== "0")
                        {
                            chkPHYSICAL.Checked = false;

                        }
                        if (ds_report_info.Tables[0].Rows[0]["BT_48HR_INI_REPORT"].ToString().Trim()== "1")
                        {
                            chk48Hour.Checked = true;

                        }
                        else if (ds_report_info.Tables[0].Rows[0]["BT_48HR_INI_REPORT"].ToString().Trim()== "0")
                        {
                            chk48Hour.Checked = false;

                        }
                        if (ds_report_info.Tables[0].Rows[0]["BT_15DAY_INI_REPORT"].ToString().Trim()== "1")
                        {
                            chk15Day.Checked = true;

                        }
                        else if (ds_report_info.Tables[0].Rows[0]["BT_15DAY_INI_REPORT"].ToString().Trim()== "0")
                        {
                            chk15Day.Checked = false;

                        }
                        if (ds_report_info.Tables[0].Rows[0]["BT_90DAY_PRO_REPORT"].ToString().Trim()== "1")
                        {
                            chk90Day.Checked = true;

                        }
                        else if (ds_report_info.Tables[0].Rows[0]["BT_90DAY_PRO_REPORT"].ToString().Trim()== "0")
                        {
                            chk90Day.Checked = false;

                        }
                        if (ds_report_info.Tables[0].Rows[0]["BT_PPO"].ToString().Trim()== "1")
                        {
                            rdlstPPO.SelectedIndex = 0;

                        }
                        else if (ds_report_info.Tables[0].Rows[0]["BT_PPO"].ToString().Trim()== "0")
                        {
                            rdlstPPO.SelectedIndex = 1;

                        }
                        else if (ds_report_info.Tables[0].Rows[0]["BT_PPO"].ToString().Trim()== "2")
                        {
                            rdlstPPO.SelectedIndex = 2;

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
        obj_OTPT_DAO = new OTPT_DAO();
        objOTPT = new OTPT();
        string sz_bt_occup="0";
        string sz_bt_physical = "0";
        string sz_bt_48Hour = "0";
        string sz_bt_15Day = "0";
        string sz_bt_90Day = "0";
        string sz_bt_PPO = "2";
        try
        {
            if (chkOCCUPATIONAL.Checked == true)
            {
                sz_bt_occup = "1";
            }
            if (chkPHYSICAL.Checked == true)
            {
                sz_bt_physical = "1";
            }
            if (chk48Hour.Checked == true)
            {
                sz_bt_48Hour = "1";
            }
            if (chk15Day.Checked == true)
            {
                sz_bt_15Day = "1";
            }
            if (chk90Day.Checked == true)
            {
                sz_bt_90Day = "1";
            }
            //if (rdlstPPO.SelectedValue=="0")
            //{
            //    sz_bt_PPO = "0";
            //}
            //else if (rdlstPPO.SelectedValue == "1")
            //{
            //    sz_bt_PPO = "1";
            //}
            //else if (rdlstPPO.SelectedValue == "2")
            //{
            //    sz_bt_PPO = "2";
            //}
            obj_OTPT_DAO.SZ_CASE_ID = txtCaseID.Text;
            obj_OTPT_DAO.SZ_PATIENT_ID = txtPatientID.Text;
            obj_OTPT_DAO.SZ_BILL_NO = txtBillNumber.Text;
            obj_OTPT_DAO.SZ_BT_OCCUPATIONAL = sz_bt_occup;
            obj_OTPT_DAO.SZ_BT_PHYSICAL = sz_bt_physical;
            obj_OTPT_DAO.SZ_BT_48_HOUR = sz_bt_48Hour;
            obj_OTPT_DAO.SZ_BT_15_DAY = sz_bt_15Day;
            obj_OTPT_DAO.SZ_BT_90_DAY = sz_bt_90Day;
            obj_OTPT_DAO.SZ_BT_PPO = rdlstPPO.SelectedValue;
            obj_OTPT_DAO.SZ_COMPANY_ID = txtCompanyID.Text;
            objOTPT.Patient_Report_Info(obj_OTPT_DAO);
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
        WorkerTemplate _objWT;
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
            Response.Redirect("Bill_Sys_History_OT-PT.aspx", false);
            //DeleteLimitations();
            //SaveWorkStatusDetails();
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
        
        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.close('../Bill_Sys_ReturnToWorkC4_2.aspx'); ", true);
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


