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
public partial class BillingInformationC4_2 : PageBase
{
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private SaveOperation _saveOperation;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;
    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (Session["TEMPLATE_BILL_NO"]!=null)
            {
            txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString();// "sas0000001";
            }
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
            if (!IsPostBack)
            {
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                txtInsuranceID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNumber.Text, "SP_TXN_BILLING_INFORMATION", "GETINSURANCEID");
                BindData();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("BillingInformationC4_2.aspx");
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
            _editOperation.Primary_Value = txtBillNumber.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "BillingInformation.xml";
            _editOperation.LoadData();

            //if (txtPPO.Text == "1")
            //{
                
            //    chkPPO.Checked = true;
            //}
            //else
            //{
            //    chkPPO.Checked = false;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            DataSet _ds = _billingCompanyDetailsBO.GetDignosisCodeList(txtBillNumber.Text, "LIST");
            grdBillingInformation.DataSource = _billingCompanyDetailsBO.GetDignosisCodeList(txtBillNumber.Text, "LIST");
            grdBillingInformation.DataBind();

            grdDignosisCode.DataSource = _billingCompanyDetailsBO.GetDignosisCodeList(txtBillNumber.Text,"GETDIGNOSISCODELIST");
            grdDignosisCode.DataBind();

            lblTotalCharges.Text = "Total Charges";
            lblAmountPaid.Text = "Amount Paid (Carrier Use only)";
            lblBalanceDue.Text = "Balance Due (Carrier Use only)";
            if (_ds.Tables[0].Rows.Count > 0)
            {
                lblTotalChargeAmt.Text = "$" + _ds.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                lblTotalPaidAmt.Text = "$" + _ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                lblTotalBalanceAmt.Text = "$" + _ds.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
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
    protected void btnSaveAndGoToNext_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            UpdateData();
            Response.Redirect("ExaminationandTreatmentC4_2.aspx", false);
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

    private void UpdateData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        Bill_Sys_DoctorBO _Bill_Sys_DoctorBO = new Bill_Sys_DoctorBO();
        try
        {
            if (chkPPO.Checked == true)
            {
                txtPPO.Text = "1";
            }
            else
            {
                txtPPO.Text = "0";
            }
          
            _editOperation.Primary_Value = txtBillNumber.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "BillingInformation.xml";
            _editOperation.UpdateMethod();
            _Bill_Sys_DoctorBO.UpdateBillinfoppoc4_2(txtBillNumber.Text, txtPPO.Text);

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
