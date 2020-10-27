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
public partial class Bill_Sys_DoctorsInformationC4_2 : PageBase
{
    private EditOperation _editOperation;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;

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
            txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString();// "sas0000001";
            
            if (!IsPostBack)
            {
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();

                txtDoctorID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNumber.Text, "SP_MST_DOCTOR_INFORMATION", "GETDOCTORID"); //"DO00001";
                LoadData();
                passInformationToLoadData();
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
        _editOperation = new EditOperation();
        Bill_Sys_DoctorBO _Bill_Sys_DoctorBO = new Bill_Sys_DoctorBO();
        DataSet dsdocinfo = new DataSet();

        try
        {
             dsdocinfo = _Bill_Sys_DoctorBO.GetDoctorinfoc4_2(txtCompanyID.Text, txtBillNumber.Text, txtbillingGroup.Text);
             if (dsdocinfo.Tables.Count > 0)
            {
                if (dsdocinfo.Tables[0].Rows.Count > 0)
                {
                    txtbillingGroup.Text = dsdocinfo.Tables[0].Rows[0]["SZ_BILLING_GROUP"].ToString();
                }
            }
            _editOperation.Primary_Value = txtDoctorID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "DoctorInformation.xml";
            _editOperation.LoadData();
            
            if (txtTaxType.Text != "" && txtTaxType.Text.ToLower() != "null")
            {
                rdlstTaxType.SelectedIndex = Convert.ToInt32(txtTaxType.Text);
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

    private void UpdateData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        Bill_Sys_DoctorBO _Bill_Sys_DoctorBO = new Bill_Sys_DoctorBO();
        try
        {
            if (rdlstTaxType.SelectedIndex == -1)
            {
                txtTaxType.Text = "3";
            }
            else
            {
                txtTaxType.Text = rdlstTaxType.SelectedIndex.ToString();
            }
            
            //int bt_doctor_type = rdbProfession.SelectedIndex;//.Text;
            
            _Bill_Sys_DoctorBO.UpdateDoctorinfoc4_2(txtCompanyID.Text, txtBillNumber.Text, txtbillingGroup.Text);
            _editOperation.Primary_Value = txtDoctorID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "DoctorInformation.xml";
            _editOperation.UpdateMethod();
            usrMessage.PutMessage("updated succes fully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
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
            UpdateData();
            Response.Redirect("Bill_Sys_BillingInformationC4_2.aspx", false);
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

    public void passInformationToLoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dsdoctorinfo = new DataSet();
        Bill_Sys_DoctorBO _Bill_Sys_DoctorBO = new Bill_Sys_DoctorBO();
       
        try
        {
            dsdoctorinfo = _Bill_Sys_DoctorBO.GetDoctorinfofortaxid(txtDoctorID.Text, txtCompanyID.Text);
            if (dsdoctorinfo.Tables.Count > 0)
            {
                if (dsdoctorinfo.Tables[0].Rows.Count > 0)
                {
                    if (dsdoctorinfo.Tables[0].Rows[0].ToString() != "" && dsdoctorinfo.Tables[0].Rows[0].ToString() != null)
                    {
                        string taxidtype = dsdoctorinfo.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString();
                        if (taxidtype == "True")
                        {
                            rdlstTaxType.SelectedIndex =1;
                        
                        }
                        else if (taxidtype == "False")
                        {
                            rdlstTaxType.SelectedIndex = 0;
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

        //string strError = ex.Message.ToString();
        //strError = strError.Replace("\n", " ");
        //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);



    }


}
