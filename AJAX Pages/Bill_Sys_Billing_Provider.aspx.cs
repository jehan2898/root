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

public partial class AJAX_Pages_Bill_Sys_Billing_Provider : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szofficeid = Request.QueryString["szofficeid"].ToString();
        txtofficeid.Text = szofficeid;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        if (!IsPostBack)
        {
            DataSet dsbillingprovider = new DataSet();
            Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
            dsbillingprovider = _Bill_Sys_Billing_Provider.BillingProvider(txtCompanyID.Text);
            grdbillingprovider.DataSource = dsbillingprovider;
            grdbillingprovider.DataBind();
            tblbillingaddress.Visible = false;
            tbldefaultgaddress.Visible = false;
        }
      
        
    }
    protected void grdbillingprovider_OnRowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {

        if (e.CommandArgs.CommandName == "Select")
        {

            tblbillingaddress.Visible = true;
            tbldefaultgaddress.Visible = true;
            string casetypeid = e.KeyValue.ToString();
            object keyValue = grdbillingprovider.GetRowValues(grdbillingprovider.FocusedRowIndex, new string[] { grdbillingprovider.KeyFieldName });
            //string szcasetypid=Convert.ToString(grdbillingprovider.GetRowValues(id., "SZ_CASE_TYPE_ID"));
            Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
            DataSet dsprovideradd = new DataSet();
            dsprovideradd = _Bill_Sys_Billing_Provider.BillingProviderAddress(casetypeid, txtCompanyID.Text, txtofficeid.Text);
            if (dsprovideradd.Tables.Count > 1)
            {
                if (dsprovideradd.Tables[1].Rows.Count > 0)
                {
                    txtOffice.Text = dsprovideradd.Tables[1].Rows[0]["SZ_OFFICE"].ToString();
                    txtOfficeAdd.Text = dsprovideradd.Tables[1].Rows[0]["SZ_OFFICE_ADDRESS"].ToString();
                    txtOfficeCity.Text = dsprovideradd.Tables[1].Rows[0]["SZ_OFFICE_CITY"].ToString();
                    txtOfficeZip.Text = dsprovideradd.Tables[1].Rows[0]["SZ_OFFICE_ZIP"].ToString();
                    txtOfficePhone.Text = dsprovideradd.Tables[1].Rows[0]["SZ_OFFICE_PHONE"].ToString();
                    txtFax.Text = dsprovideradd.Tables[1].Rows[0]["SZ_OFFICE_FAX"].ToString();
                    extddlOfficeState.Text = dsprovideradd.Tables[1].Rows[0]["SZ_OFFICE_STATE_ID"].ToString();
                }
            }
            if (dsprovideradd.Tables.Count > 0)
            {

                if (dsprovideradd.Tables[0].Rows.Count > 0)
                {
                    txtbillingprovideraddress.Text = dsprovideradd.Tables[0].Rows[0]["SZ_PROVIDER_ADDRESS"].ToString();
                    txtproviderbillingname.Text = dsprovideradd.Tables[0].Rows[0]["SZ_PROVIDER_NAME"].ToString();
                    txtbillingproviderphone.Text = dsprovideradd.Tables[0].Rows[0]["SZ_PROVIDER_PHONE"].ToString();
                    extddlBillingProviderState.Text = dsprovideradd.Tables[0].Rows[0]["SZ_PROVIDER_STATE"].ToString();
                    txtbillingproviderzip.Text = dsprovideradd.Tables[0].Rows[0]["SZ_PROVIDER_ZIP"].ToString();
                    txtbillingprovidercity.Text = dsprovideradd.Tables[0].Rows[0]["SZ_PROVIDER_CITY"].ToString();
                    txtbillingproviderfax.Text = dsprovideradd.Tables[0].Rows[0]["SZ_PROVIDER_FAX"].ToString();
                }
                else
                {

                    ClearControl();
                }
            }

          
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
        string szcasetypid=Convert.ToString(grdbillingprovider.GetRowValues(grdbillingprovider.FocusedRowIndex, "SZ_CASE_TYPE_ID"));
        //txtcasetypeid.Text = szcasetypid;
        _Bill_Sys_Billing_Provider.AddBillingaddress(txtCompanyID.Text, szcasetypid, txtbillingprovideraddress.Text, txtbillingprovidercity.Text, txtofficeid.Text, txtproviderbillingname.Text, txtbillingproviderphone.Text, extddlBillingProviderState.Text, txtbillingproviderzip.Text, txtbillingproviderfax.Text);
        ClearControl();
        usrMessage.PutMessage("Billing Provider Saved Successfully ...!");
        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
        usrMessage.Show();
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string szcasetypid = Convert.ToString(grdbillingprovider.GetRowValues(grdbillingprovider.FocusedRowIndex, "SZ_CASE_TYPE_ID"));
        Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
        _Bill_Sys_Billing_Provider.RemoveBillingaddresscasetype(txtCompanyID.Text, szcasetypid, txtofficeid.Text);
        ClearControl();
        usrMessage.PutMessage("Billing Provider Delete Successfully ...!");
        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
        usrMessage.Show();
    }
    public void ClearControl()
    {
        txtbillingprovideraddress.Text = "";
        txtproviderbillingname.Text = "";
        txtbillingproviderphone.Text = "";
        extddlBillingProviderState.Text = "NA";
        txtbillingproviderzip.Text = "";
        txtbillingprovidercity.Text = "";
        txtbillingproviderfax.Text = "";
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
        //_Bill_Sys_Billing_Provider.UpdateProvider(txtOfficeCity.Text,txtofficeid.Text,txtCompanyID.Text); //SVN
    }

}
