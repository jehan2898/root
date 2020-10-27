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

public partial class Bill_Sys_TherapistInformation_OT_PT : PageBase
{
    private EditOperation _editOperation;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;
    private OTPT obj_OTPT;
    private OTPT_THERAPIST_DAO _OTPT_THERAPIST_DAO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString();// "sas0000001";
            if (Request.QueryString["billnumber"] != null)
            {
                Session["BILL_NO"] = Request.QueryString["billnumber"];
            }
            if (Request.QueryString["caseid"] != null)
            {
                Session["CASE_ID"] = Request.QueryString["caseid"];
            }
            txtBillNo.Text = Session["BILL_NO"].ToString();// "sas0000001";           
            txtCaseID.Text = Session["CASE_ID"].ToString();

            if (!IsPostBack)
            {
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();

                txtDoctorID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNo.Text, "SP_MST_DOCTOR_INFORMATION", "GETDOCTORID"); //"DO00001";
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
            cv.MakeReadOnlyPage("Bill_Sys_DoctorsInformationC4_2.aspx");
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
        obj_OTPT = new OTPT();
        DataSet ds_therapist_info = new DataSet();
        DataSet dsgettherapist = new DataSet();
        _editOperation = new EditOperation();
        try
        {
            ds_therapist_info = obj_OTPT.GET_Therapist_Info_OTPT(txtBillNo.Text);
           // dsgettherapist = obj_OTPT.GET_Therapist_Information_OTPT(txtCaseID.Text, txtCompanyID.Text);
            if (ds_therapist_info.Tables.Count > 0)
            {
                if (ds_therapist_info.Tables[0].Rows.Count > 0)
                {
                    if (ds_therapist_info.Tables[0].Rows[0].ItemArray[0].ToString() != "" && ds_therapist_info.Tables[0].Rows[0].ItemArray[0].ToString() != null)
                    {
                        txtTheripastName.Text = ds_therapist_info.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString();
                        txtbillingaddress.Text = ds_therapist_info.Tables[0].Rows[0]["SZ_OFFICE"].ToString();
                        txtfederaltaxidnumber.Text = ds_therapist_info.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString();
                        txtnynlicenseno.Text = ds_therapist_info.Tables[0].Rows[0]["SZ_DOCTOR_LICENSE_NUMBER"].ToString();
                        if (ds_therapist_info.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString().Trim()== "True")
                        {
                            rdlstTaxType.SelectedIndex = 0;

                        }
                        else if (ds_therapist_info.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString().Trim()== "False")
                        {
                            rdlstTaxType.SelectedIndex = 1;

                        }
            
                    }
                }
            }
            //if (dsgettherapist.Tables.Count > 0)
            //{
            //    if (dsgettherapist.Tables[0].Rows.Count > 0)
            //    {
            //        if (dsgettherapist.Tables[0].Rows[0].ToString() != "" && dsgettherapist.Tables[0].Rows[0].ToString() != null)
            //        {
            //            if (dsgettherapist.Tables[0].Rows[0]["BT_SSN_EIN"].ToString().Trim() == "0")
            //            {
            //                rdlstTaxType.SelectedIndex = 0;

            //            }
            //            else
            //            {
            //                rdlstTaxType.SelectedIndex = 1;
                        
            //            }
            //        }
            //    }
            //}
            
           
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

    //private void UpdateData()
    //{
    //    _editOperation = new EditOperation();
    //    try
    //    {
    //        _editOperation.Primary_Value = txtDoctorID.Text;
    //        _editOperation.WebPage = this.Page;
    //        _editOperation.Xml_File = "DoctorInformation.xml";
    //        _editOperation.UpdateMethod();
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}
    protected void btnSaveAndGoToNext_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
             saveData();
            //UpdateData();
            //Response.Redirect("Bill_Sys_BillingInformationC4_2.aspx", false);
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

     public void saveData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _OTPT_THERAPIST_DAO = new OTPT_THERAPIST_DAO();
        obj_OTPT = new OTPT();

        try
        {
            _OTPT_THERAPIST_DAO.SZ_CASE_ID = txtCaseID.Text;
            _OTPT_THERAPIST_DAO.SZ_BILL_NO = txtBillNo.Text;
           if (rdlstTaxType.SelectedValue == "0")
            {
                _OTPT_THERAPIST_DAO.SZ_BT_SSN_EIN = rdlstTaxType.SelectedValue;
               
            }
            else if (rdlstTaxType.SelectedValue == "1")
            {
                _OTPT_THERAPIST_DAO.SZ_BT_SSN_EIN = rdlstTaxType.SelectedValue;
            }
            _OTPT_THERAPIST_DAO.SZ_COMPANY_ID = txtCompanyID.Text;
            obj_OTPT.Patient_Therapist_Info(_OTPT_THERAPIST_DAO);

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
