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
using System.Globalization;

public partial class AJAX_Pages_Bill_Sys_Office : PageBase
{
    private Bill_Sys_DeleteBO _deleteOpeation;
    private LocationBO _Location;

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._deleteOpeation = new Bill_Sys_DeleteBO();
        string str = "";
        try
        {
            for (int i = 0; i < this.grdOfficeList.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdOfficeList.Rows[i].FindControl("ChkDelete");
                if (box.Checked && !this._deleteOpeation.deleteRecord("SP_MST_OFFICE", "@SZ_OFFICE_ID", this.grdOfficeList.DataKeys[i][0].ToString()))
                {
                    if (str == "")
                    {
                        str = this.grdOfficeList.Rows[i].Cells[2].Text.ToString();
                    }
                    else
                    {
                        str = str + " , " + this.grdOfficeList.Rows[i].Cells[2].Text.ToString();
                    }
                }
            }
            if (str != "")
            {
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "alert('Records for office " + str + "  exists.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "alert('Records for provider " + str + "  exists.');", true);
                }
            }
            else if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.grdOfficeList.XGridBindSearch();
                this.usrMessage.PutMessage("Office deleted successfully ...");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.grdOfficeList.XGridBindSearch();
                this.usrMessage.PutMessage("Provider deleted successfully ...");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (this.Page.IsValid)
        {
            Bill_Sys_DoctorBO rbo = new Bill_Sys_DoctorBO();
            int num = 0;
            try
            {
                string text;
                string str = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string str5 = "";
                int num2 = 0;
                if (this.extddlOfficeState.Text != "NA")
                {
                    str = this.extddlOfficeState.Text;
                    str2 = this.extddlOfficeState.Selected_Text.ToString();
                }
                if (this.extddlBillingState.Text != "NA")
                {
                    str3 = this.extddlBillingState.Text;
                    str4 = this.extddlBillingState.Selected_Text.ToString();
                }
                if (this.extddlLocation.Text != "NA")
                {
                    str5 = this.extddlLocation.Text;
                }
                if (this.chkSoftwareFee.Checked)
                {
                    this.txtSoftwareFee.Enabled = true;
                    num2 = 1;
                    text = this.txtSoftwareFee.Text;
                }
                else
                {
                    text = "";
                }
                num = rbo.InsertOfficeMaster(this.txtOffice.Text, this.txtCompanyID.Text, this.txtOfficeAdd.Text, this.txtOfficeCity.Text, str2, this.txtOfficeZip.Text, this.txtOfficePhone.Text, this.txtBillingAdd.Text, this.txtBillingCity.Text, str4, this.txtBillingZip.Text, this.txtBillingPhone.Text, this.txtNPI.Text, this.txtFederalTax.Text, this.txtFax.Text, this.txtEmail.Text, str, str3, this.txtBillingOfficeFlag.Text, this.txtPrefix.Text, str5, num2, text, this.txtOfficeCode.Text);
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
            if (num > 0)
            {
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    this.grdOfficeList.XGridBindSearch();
                    this.usrMessage.PutMessage("Office Saved Successfully ...!");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
                else
                {
                    this.grdOfficeList.XGridBindSearch();
                    this.usrMessage.PutMessage("Provider Saved Successfully ...!");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
            }
            else
            {
                this.usrMessage.PutMessage("Insertion Failed");
                this.usrMessage.SetMessageType(0);
                this.usrMessage.Show();
            }

            CLS();
            this.btnUpdate.Enabled = false;
            this.btnSave.Enabled = true;
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
        Bill_Sys_DoctorBO rbo = new Bill_Sys_DoctorBO();
        int num = 0;
        try
        {
            string text;
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            int num2 = 0;
            if (this.extddlOfficeState.Text != "NA")
            {
                str = this.extddlOfficeState.Text;
                str2 = this.extddlOfficeState.Selected_Text.ToString();
            }
            if (this.extddlBillingState.Text != "NA")
            {
                str3 = this.extddlBillingState.Text;
                str4 = this.extddlBillingState.Selected_Text.ToString();
            }
            if (this.extddlLocation.Text != "NA")
            {
                str5 = this.extddlLocation.Text;
            }
            if (this.chkSoftwareFee.Checked)
            {
                this.txtSoftwareFee.Enabled = true;
                num2 = 1;
                text = this.txtSoftwareFee.Text;
            }
            else
            {
                text = "";
            }
            num = rbo.UpdateOfficeMaster(this.txtOffice.Text, this.txtCompanyID.Text, this.txtOfficeAdd.Text, this.txtOfficeCity.Text, str2, this.txtOfficeZip.Text, this.txtOfficePhone.Text, this.txtBillingAdd.Text, this.txtBillingCity.Text, str4, this.txtBillingZip.Text, this.txtBillingPhone.Text, this.txtNPI.Text, this.txtFederalTax.Text, this.txtFax.Text, this.txtEmail.Text, str, str3, this.txtBillingOfficeFlag.Text, this.txtPrefix.Text, str5, this.Session["SZ_OFFICE_ID"].ToString(), num2, text, this.txtOfficeCode.Text);
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
        if (num > 0)
        {
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.grdOfficeList.XGridBindSearch();
                this.usrMessage.PutMessage("Office Updated Successfully ...!");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.grdOfficeList.XGridBindSearch();
                this.usrMessage.PutMessage("Provider Updated Successfully ...!");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
        }
        else
        {
            this.usrMessage.PutMessage("Updation Failed");
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
        CLS();
        this.btnUpdate.Enabled = false;
        this.btnSave.Enabled = true;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void extddlLocation_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._Location = new LocationBO();
            if (this.extddlLocation.Text != "NA")
            {
                DataSet set = this._Location.Location(this.extddlLocation.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.txtOfficeCity.Text = set.Tables[0].Rows[0]["SZ_CITY"].ToString();
                this.txtOfficeZip.Text = set.Tables[0].Rows[0]["SZ_ZIP"].ToString();
                this.txtOfficeAdd.Text = set.Tables[0].Rows[0]["SZ_ADDRESS"].ToString();
                this.extddlOfficeState.Text = set.Tables[0].Rows[0]["SZ_STATE_ID"].ToString();
                this.SetReadOnly();
            }
            else
            {
                this.txtOfficeCity.Text = "";
                this.txtOfficeZip.Text = "";
                this.txtOfficeAdd.Text = "";
                this.extddlOfficeState.Text = "NA";
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

    protected void grdOfficeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (e.CommandName == "Select")
        {
            int num = Convert.ToInt32(e.CommandArgument.ToString());
            try
            {
                this.txtOfficeCity.ReadOnly = false;
                this.txtOfficeZip.ReadOnly = false;
                this.txtOfficeAdd.ReadOnly = false;
                this.extddlOfficeState.Enabled = true;
                this.Session["SZ_OFFICE_ID"] = this.grdOfficeList.DataKeys[num][0].ToString();
                if (this.grdOfficeList.DataKeys[num]["SZ_OFFICE"].ToString() != "&nbsp;")
                {
                    this.txtOffice.Text = this.grdOfficeList.DataKeys[num]["SZ_OFFICE"].ToString();
                }
                else
                {
                    this.txtOffice.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_OFFICE_ADDRESS"].ToString() != "&nbsp;")
                {
                    this.txtOfficeAdd.Text = this.grdOfficeList.DataKeys[num]["SZ_OFFICE_ADDRESS"].ToString();
                }
                else
                {
                    this.txtOfficeAdd.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_OFFICE_CITY"].ToString() != "&nbsp;")
                {
                    this.txtOfficeCity.Text = this.grdOfficeList.DataKeys[num]["SZ_OFFICE_CITY"].ToString();
                }
                else
                {
                    this.txtOfficeCity.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_OFFICE_STATE_ID"].ToString() != "&nbsp;")
                {
                    this.extddlOfficeState.Text = this.grdOfficeList.DataKeys[num]["SZ_OFFICE_STATE_ID"].ToString();
                }
                else
                {
                    this.extddlOfficeState.Text = "NA";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_OFFICE_ZIP"].ToString() != "&nbsp;")
                {
                    this.txtOfficeZip.Text = this.grdOfficeList.DataKeys[num]["SZ_OFFICE_ZIP"].ToString();
                }
                else
                {
                    this.txtOfficeZip.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_OFFICE_PHONE"].ToString() != "&nbsp;")
                {
                    this.txtOfficePhone.Text = this.grdOfficeList.DataKeys[num]["SZ_OFFICE_PHONE"].ToString();
                }
                else
                {
                    this.txtOfficePhone.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_BILLING_ADDRESS"].ToString() != "&nbsp;")
                {
                    this.txtBillingAdd.Text = this.grdOfficeList.DataKeys[num]["SZ_BILLING_ADDRESS"].ToString();
                }
                else
                {
                    this.txtBillingAdd.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_BILLING_CITY"].ToString() != "&nbsp;")
                {
                    this.txtBillingCity.Text = this.grdOfficeList.DataKeys[num]["SZ_BILLING_CITY"].ToString();
                }
                else
                {
                    this.txtBillingCity.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_BILLING_STATE_ID"].ToString() != "&nbsp;")
                {
                    this.extddlBillingState.Text = this.grdOfficeList.DataKeys[num]["SZ_BILLING_STATE_ID"].ToString();
                }
                else
                {
                    this.extddlBillingState.Text = "NA";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_BILLING_ZIP"].ToString() != "&nbsp;")
                {
                    this.txtBillingZip.Text = this.grdOfficeList.DataKeys[num]["SZ_BILLING_ZIP"].ToString();
                }
                else
                {
                    this.txtBillingZip.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_BILLING_PHONE"].ToString() != "&nbsp;")
                {
                    this.txtBillingPhone.Text = this.grdOfficeList.DataKeys[num]["SZ_BILLING_PHONE"].ToString();
                }
                else
                {
                    this.txtBillingPhone.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_NPI"].ToString() != "&nbsp;")
                {
                    this.txtNPI.Text = this.grdOfficeList.DataKeys[num]["SZ_NPI"].ToString();
                }
                else
                {
                    this.txtNPI.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_FEDERAL_TAX_ID"].ToString() != "&nbsp;")
                {
                    this.txtFederalTax.Text = this.grdOfficeList.DataKeys[num]["SZ_FEDERAL_TAX_ID"].ToString();
                }
                else
                {
                    this.txtFederalTax.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_OFFICE_FAX"].ToString() != "&nbsp;")
                {
                    this.txtFax.Text = this.grdOfficeList.DataKeys[num]["SZ_OFFICE_FAX"].ToString();
                }
                else
                {
                    this.txtFax.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_OFFICE_EMAIL"].ToString() != "&nbsp;")
                {
                    this.txtEmail.Text = this.grdOfficeList.DataKeys[num]["SZ_OFFICE_EMAIL"].ToString();
                }
                else
                {
                    this.txtEmail.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_PREFIX"].ToString() != "&nbsp;")
                {
                    this.txtPrefix.Text = this.grdOfficeList.DataKeys[num]["SZ_PREFIX"].ToString();
                }
                else
                {
                    this.txtPrefix.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["SZ_LOCATION_ID"].ToString() != "&nbsp;")
                {
                    this.extddlLocation.Text = this.grdOfficeList.DataKeys[num]["SZ_LOCATION_ID"].ToString();
                }
                else
                {
                    this.extddlLocation.Text = "";
                }
                if (this.grdOfficeList.DataKeys[num]["sz_place_of_service"].ToString() != "&nbsp;")
                {
                    this.txtOfficeCode.Text = this.grdOfficeList.DataKeys[num]["sz_place_of_service"].ToString();
                }
                else
                {
                    this.txtOfficeCode.Text = "";
                }
                if ((this.grdOfficeList.DataKeys[num]["IS_SOFTWARE_FEE_ADDED"].ToString() != "&nbsp;") && (this.grdOfficeList.DataKeys[num]["IS_SOFTWARE_FEE_ADDED"].ToString() != ""))
                {
                    if (this.grdOfficeList.DataKeys[num]["IS_SOFTWARE_FEE_ADDED"].ToString().ToLower() == "true")
                    {
                        this.txtSoftwareFee.Text = Convert.ToDouble(this.grdOfficeList.DataKeys[num]["MN_SOFTWARE_FEE"]).ToString();
                        this.chkSoftwareFee.Checked = true;
                    }
                    else
                    {
                        this.txtSoftwareFee.Text = Convert.ToDouble(this.grdOfficeList.DataKeys[num]["MN_SOFTWARE_FEE"]).ToString();
                        this.chkSoftwareFee.Checked = false;
                    }
                }
                else
                {
                    this.txtSoftwareFee.Text = "";
                    this.chkSoftwareFee.Checked = false;
                }
                this.btnSave.Enabled = false;
                this.btnUpdate.Enabled = true;
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && !(((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
                {
                    this.SetReadOnly();
                }
                this.txtSoftwareFee.Enabled = true;
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

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdOfficeList.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid = this.grdOfficeList;
        this.txtSearchBox.SourceGrid = this.grdOfficeList;
        this.grdOfficeList.Page = this.Page;
        this.grdOfficeList.PageNumberList = this.con;
        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
        {
            this.lblLocation.Visible = false;
            this.extddlLocation.Visible = false;
        }
        else
        {
            this.lblLocation.Visible = true;
            this.extddlLocation.Visible = true;
            this.SetReadOnly();
        }
        this.btnSave.Attributes.Add("onclick", "return validate();");
        this.btnUpdate.Attributes.Add("onclick", "return validate();");
        this.btnUpdate.Attributes.Add("onclick", "return validate();");
        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.extddlLocation.Flag_ID = this.txtCompanyID.Text.ToString();
        if ((this.Session["Flag"] != null) && (this.Session["Flag"].ToString() == "true"))
        {
            this.grdOfficeList.Visible = false;
            this.btnUpdate.Visible = false;
        }
        else if (!base.IsPostBack)
        {
            this.txtBillingOfficeFlag.Text = "0";
            this.grdOfficeList.XGridBindSearch();
            this.btnUpdate.Enabled = false;
        }
        this._deleteOpeation = new Bill_Sys_DeleteBO();
        if (this._deleteOpeation.checkForDelete(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE))
        {
            this.btnDelete.Visible = false;
        }
        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
        {
            this.chkSameAddress.Visible = false;
            this.lblBillingAdd.Visible = false;
            this.txtBillingAdd.Visible = false;
            this.lblBillingCity.Visible = false;
            this.txtBillingCity.Visible = false;
            this.lblBillingPhone.Visible = false;
            this.txtBillingPhone.Visible = false;
            this.lblBillingState.Visible = false;
            this.extddlBillingState.Visible = false;
            this.lblBillingZip.Visible = false;
            this.txtBillingZip.Visible = false;
            this.lblNPI.Visible = false;
            this.txtNPI.Visible = false;
            this.lblFederalTax.Visible = false;
            this.txtFederalTax.Visible = false;
            this.lblName.Text = "Office Name";
            this.lblAddress.Text = "Office Address";
            this.lblCity.Text = "Office City";
            this.lblState.Text = "Office State";
            this.lblZip.Text = "Office Zip";
            this.lblPhone.Text = "Office Phone";
            this.lblFax.Text = "Office Fax";
            this.lblEmail.Text = "Office Email";
            this.lblPrefix.Visible = true;
            this.txtPrefix.Visible = true;
            this.Label1.Text = "Office";
            this.lblHeader.Text = "Office Master";
        }
        else
        {
            this.lblPrefix.Visible = false;
            this.txtPrefix.Visible = false;
            this.Label1.Text = "Provider";
            this.lblHeader.Text = "Provider Master";
        }
    }

    protected void SetReadOnly()
    {
        this.txtOfficeCity.ReadOnly = true;
        this.txtOfficeZip.ReadOnly = true;
        this.txtOfficeAdd.ReadOnly = true;
        this.txtSoftwareFee.Enabled = false;
        this.extddlOfficeState.Enabled = false;
    }
    public void CLS()
    {
        txtOffice.Text = "";
        txtOfficeAdd.Text = "";
        txtOfficeCity.Text = "";
        extddlOfficeState.Text = "NA";
        txtOfficePhone.Text = "";
        txtFax.Text = "";
        txtEmail.Text = "";
        txtOfficeCode.Text = "";
        if (txtPrefix.Visible)
        {
            txtPrefix.Text = "";
        }

        if (chkSameAddress.Visible)
        {
            chkSameAddress.Checked = false;
        }

        if (txtBillingAdd.Visible)
        {
            txtBillingAdd.Text = "";
        }


        if (txtBillingCity.Visible)
        {
            txtBillingCity.Text = "";
        }

        if (txtBillingZip.Visible)
        {
            txtBillingZip.Text = "";
        }

        if (txtBillingPhone.Visible)
        {
            txtBillingPhone.Text = "";
        }

        if (txtNPI.Visible)
        {
            txtNPI.Text = "";
        }

        if (txtFederalTax.Visible)
        {
            txtFederalTax.Text = "";
        }

        if (extddlBillingState.Visible)
        {
            extddlBillingState.Text = "NA";
        }

        if (extddlLocation.Visible)
        {
            extddlLocation.Text = "NA";
        }

        if (txtSoftwareFee.Visible)
        {
            txtSoftwareFee.Text = "";
        }

        if (chkSoftwareFee.Visible)
        {
            chkSoftwareFee.Checked = false;
        }

        if (txtOfficeZip.Visible)
        {
            txtOfficeZip.Text = "";
        }
    }
}
