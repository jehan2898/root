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

public partial class AJAX_Pages_Bill_Sys_RefferingOffice : PageBase
{
    private Bill_Sys_DeleteBO _deleteOpeation;
    private LocationBO _Location;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid = grdOfficeList;
        this.txtSearchBox.SourceGrid = grdOfficeList;
        this.grdOfficeList.Page = this.Page;
        this.grdOfficeList.PageNumberList = this.con;


        bool bt_referring_facility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;

        if ((bt_referring_facility == true) || (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
        {
            lblLocation.Visible = false;
            extddlLocation.Visible = false;
        }
        else
        {
            lblLocation.Visible = true;
            extddlLocation.Visible = true;
            SetReadOnly();
        }

        btnSave.Attributes.Add("onclick", "return validate();");
        btnUpdate.Attributes.Add("onclick", "return validate();");
        btnUpdate.Attributes.Add("onclick", "return validate();");
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        // btnClear.Attributes.Add("onclick", "return Clear();");
        extddlLocation.Flag_ID = txtCompanyID.Text.ToString();

        if (Session["Flag"] != null && Session["Flag"].ToString() == "true")
        {
            //TreeMenuControl1.Visible = false;
            grdOfficeList.Visible = false;
            btnUpdate.Visible = false;
        }
        else
        {
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
            if (!IsPostBack)
            {
                //BindGrid();

                txtBillingOfficeFlag.Text = "0";
                grdOfficeList.XGridBindSearch();
                btnUpdate.Enabled = false;
            }
        }

        _deleteOpeation = new Bill_Sys_DeleteBO();
        if (_deleteOpeation.checkForDelete(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE))
        {
            btnDelete.Visible = false;
        }


        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
        {
            chkSameAddress.Visible = false;
            lblBillingAdd.Visible = false;
            txtBillingAdd.Visible = false;
            lblBillingCity.Visible = false;
            txtBillingCity.Visible = false;
            lblBillingPhone.Visible = false;
            txtBillingPhone.Visible = false;
            lblBillingState.Visible = false;
            extddlBillingState.Visible = false;
            lblBillingZip.Visible = false;
            txtBillingZip.Visible = false;
            lblNPI.Visible = false;
            txtNPI.Visible = false;
            lblFederalTax.Visible = false;
            txtFederalTax.Visible = false;
            lblName.Text = "Office Name";
            lblAddress.Text = "Office Address";
            lblCity.Text = "Office City";
            lblState.Text = "Office State";
            lblZip.Text = "Office Zip";
            lblPhone.Text = "Office Phone";
            lblFax.Text = "Office Fax";
            lblEmail.Text = "Office Email";
            lblPrefix.Visible = true;
            txtPrefix.Visible = true;
            Label1.Text = "Office";
            lblHeader.Text = "Office Master";
        }
        else
        {
            lblPrefix.Visible = false;
            txtPrefix.Visible = false;
            Label1.Text = "Provider";
            lblHeader.Text = "Provider Master";

        }


    }

    protected void SetReadOnly()
    {
        txtOfficeCity.ReadOnly = true;
        txtOfficeZip.ReadOnly = true;
        txtOfficeAdd.ReadOnly = true;
        txtSoftwareFee.Enabled = false;
        extddlOfficeState.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (Page.IsValid)
        {
            Bill_Sys_DoctorRefferingBO obj = new Bill_Sys_DoctorRefferingBO();
            //Bill_Sys_DoctorBO obj = new Bill_Sys_DoctorBO();
            int i = 0;
            try
            {

                string szOfficeStateID = "";
                string szOfficeState = "";
                string szBillingStateID = "";
                string szBillingState = "";
                string szLocationID = "";
                int BT_SoftFee = 0;
                int bt_reffering = 0;
                bool bt_referring_facility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                if (bt_referring_facility == true)
                {
                    bt_reffering = 0;
                }
                else
                {
                    bt_reffering = 1;
                }
                string szSoftwareFee;
                if (extddlOfficeState.Text != "NA")
                {
                    szOfficeStateID = extddlOfficeState.Text;
                    szOfficeState = extddlOfficeState.Selected_Text.ToString();
                }
                if (extddlBillingState.Text != "NA")
                {
                    szBillingStateID = extddlBillingState.Text;
                    szBillingState = extddlBillingState.Selected_Text.ToString();
                }
                if (extddlLocation.Text != "NA")
                {
                    szLocationID = extddlLocation.Text;
                }

                // added by Kapil 05 Jan 2012
                if (chkSoftwareFee.Checked == true)
                {
                    txtSoftwareFee.Enabled = true;
                    BT_SoftFee = 1;
                    szSoftwareFee = txtSoftwareFee.Text;
                }
                else
                {
                    szSoftwareFee = "";
                }
                
                i = obj.InsertOfficeMaster(txtOffice.Text, txtCompanyID.Text, txtOfficeAdd.Text, txtOfficeCity.Text, szOfficeState, txtOfficeZip.Text, txtOfficePhone.Text, txtBillingAdd.Text, txtBillingCity.Text, szBillingState, txtBillingZip.Text, txtBillingPhone.Text, txtNPI.Text, txtFederalTax.Text, txtFax.Text, txtEmail.Text, szOfficeStateID, szBillingStateID, txtBillingOfficeFlag.Text, txtPrefix.Text, szLocationID, BT_SoftFee, szSoftwareFee, txtOfficeCode.Text, bt_reffering);
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

            if (i > 0)
            {

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {


                    grdOfficeList.XGridBindSearch();
                    usrMessage.PutMessage("Office Saved Successfully ...!");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();

                }
                else
                {


                    grdOfficeList.XGridBindSearch();
                    usrMessage.PutMessage("Provider Saved Successfully ...!");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                }

            }
            else
            {
                usrMessage.PutMessage("Insertion Failed");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
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
        Bill_Sys_DoctorRefferingBO obj = new Bill_Sys_DoctorRefferingBO();
        //Bill_Sys_DoctorBO obj = new Bill_Sys_DoctorBO();
        int i = 0;
        try
        {

            string szOfficeStateID = "";
            string szOfficeState = "";
            string szBillingStateID = "";
            string szBillingState = "";
            string szLocationID = "";
            int BT_SoftFee = 0;
            string szSoftwareFee;
            if (extddlOfficeState.Text != "NA")
            {
                szOfficeStateID = extddlOfficeState.Text;
                szOfficeState = extddlOfficeState.Selected_Text.ToString();
            }
            if (extddlBillingState.Text != "NA")
            {
                szBillingStateID = extddlBillingState.Text;
                szBillingState = extddlBillingState.Selected_Text.ToString();
            }
            if (extddlLocation.Text != "NA")
            {
                szLocationID = extddlLocation.Text;
            }

            // added by Kapil 05 Jan 2012
            if (chkSoftwareFee.Checked == true)
            {
                txtSoftwareFee.Enabled = true;
                BT_SoftFee = 1;
                szSoftwareFee = txtSoftwareFee.Text;
            }
            else
            {
                szSoftwareFee = "";
            }
            i = obj.UpdateOfficeMaster(txtOffice.Text, txtCompanyID.Text, txtOfficeAdd.Text, txtOfficeCity.Text, szOfficeState, txtOfficeZip.Text, txtOfficePhone.Text, txtBillingAdd.Text, txtBillingCity.Text, szBillingState, txtBillingZip.Text, txtBillingPhone.Text, txtNPI.Text, txtFederalTax.Text, txtFax.Text, txtEmail.Text, szOfficeStateID, szBillingStateID, txtBillingOfficeFlag.Text, txtPrefix.Text, szLocationID, Session["SZ_OFFICE_ID"].ToString(), BT_SoftFee, szSoftwareFee, txtOfficeCode.Text);
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

        if (i > 0)
        {


            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {

                grdOfficeList.XGridBindSearch();
                usrMessage.PutMessage("Office Updated Successfully ...!");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();

            }
            else
            {

                grdOfficeList.XGridBindSearch();
                usrMessage.PutMessage("Provider Updated Successfully ...!");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();

            }

        }
        else
        {
            usrMessage.PutMessage("Updation Failed");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }
        btnUpdate.Enabled = false;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _deleteOpeation = new Bill_Sys_DeleteBO();
        String szListOfOffice = "";
        try
        {
            for (int i = 0; i < grdOfficeList.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdOfficeList.Rows[i].FindControl("ChkDelete");
                if (chkDelete1.Checked)
                {
                    if (!_deleteOpeation.deleteRecord("SP_MST_OFFICE_REFF", "@SZ_OFFICE_ID", grdOfficeList.DataKeys[i][0].ToString()))
                    {
                        if (szListOfOffice == "")
                        {
                            szListOfOffice = grdOfficeList.Rows[i].Cells[2].Text.ToString();
                        }
                        else
                        {
                            szListOfOffice = szListOfOffice + " , " + grdOfficeList.Rows[i].Cells[2].Text.ToString();
                        }
                    }
                }
            } if (szListOfOffice != "")
            {
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for office " + szListOfOffice + "  exists.'); ", true);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Records for office " + szListOfOffice + "  exists.');", true);
                }
                else
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for provider " + szListOfOffice + "  exists.'); ", true);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Records for provider " + szListOfOffice + "  exists.');", true);
                }

            }
            else
            {

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {


                    grdOfficeList.XGridBindSearch();
                    usrMessage.PutMessage("Office deleted successfully ...");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();


                }
                else
                {


                    grdOfficeList.XGridBindSearch();
                    usrMessage.PutMessage("Provider deleted successfully ...");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
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
    //protected void btnClear_Click(object sender, EventArgs e)
    //{

    //}
    protected void grdOfficeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            //txtDoctorName.Text = grdRffDoc.Rows[index].Cells[2].Text.ToString();
            //txtLicenseNumber.Text = grdRffDoc.Rows[index].Cells[5].Text.ToString();
            //txtAssignNumber.Text = grdRffDoc.Rows[index].Cells[7].Text.ToString();
            //extddlDoctorType.Text = grdRffDoc.DataKeys[index][2].ToString();
            //extddlOffice.Text = grdRffDoc.DataKeys[index][1].ToString();



            try
            {

                txtOfficeCity.ReadOnly = false;
                txtOfficeZip.ReadOnly = false;
                txtOfficeAdd.ReadOnly = false;
                extddlOfficeState.Enabled = true;

                Session["SZ_OFFICE_ID"] = grdOfficeList.DataKeys[index][0].ToString();
                if (grdOfficeList.DataKeys[index]["SZ_OFFICE"].ToString() != "&nbsp;") { txtOffice.Text = grdOfficeList.DataKeys[index]["SZ_OFFICE"].ToString(); } else { txtOffice.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_OFFICE_ADDRESS"].ToString() != "&nbsp;") { txtOfficeAdd.Text = grdOfficeList.DataKeys[index]["SZ_OFFICE_ADDRESS"].ToString(); } else { txtOfficeAdd.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_OFFICE_CITY"].ToString() != "&nbsp;") { txtOfficeCity.Text = grdOfficeList.DataKeys[index]["SZ_OFFICE_CITY"].ToString(); } else { txtOfficeCity.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_OFFICE_STATE_ID"].ToString() != "&nbsp;") { extddlOfficeState.Text = grdOfficeList.DataKeys[index]["SZ_OFFICE_STATE_ID"].ToString(); } else { extddlOfficeState.Text = "NA"; }
                if (grdOfficeList.DataKeys[index]["SZ_OFFICE_ZIP"].ToString() != "&nbsp;") { txtOfficeZip.Text = grdOfficeList.DataKeys[index]["SZ_OFFICE_ZIP"].ToString(); } else { txtOfficeZip.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_OFFICE_PHONE"].ToString() != "&nbsp;") { txtOfficePhone.Text = grdOfficeList.DataKeys[index]["SZ_OFFICE_PHONE"].ToString(); } else { txtOfficePhone.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_BILLING_ADDRESS"].ToString() != "&nbsp;") { txtBillingAdd.Text = grdOfficeList.DataKeys[index]["SZ_BILLING_ADDRESS"].ToString(); } else { txtBillingAdd.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_BILLING_CITY"].ToString() != "&nbsp;") { txtBillingCity.Text = grdOfficeList.DataKeys[index]["SZ_BILLING_CITY"].ToString(); } else { txtBillingCity.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_BILLING_STATE_ID"].ToString() != "&nbsp;") { extddlBillingState.Text = grdOfficeList.DataKeys[index]["SZ_BILLING_STATE_ID"].ToString(); } else { extddlBillingState.Text = "NA"; }
                if (grdOfficeList.DataKeys[index]["SZ_BILLING_ZIP"].ToString() != "&nbsp;") { txtBillingZip.Text = grdOfficeList.DataKeys[index]["SZ_BILLING_ZIP"].ToString(); } else { txtBillingZip.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_BILLING_PHONE"].ToString() != "&nbsp;") { txtBillingPhone.Text = grdOfficeList.DataKeys[index]["SZ_BILLING_PHONE"].ToString(); } else { txtBillingPhone.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_NPI"].ToString() != "&nbsp;") { txtNPI.Text = grdOfficeList.DataKeys[index]["SZ_NPI"].ToString(); } else { txtNPI.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_FEDERAL_TAX_ID"].ToString() != "&nbsp;") { txtFederalTax.Text = grdOfficeList.DataKeys[index]["SZ_FEDERAL_TAX_ID"].ToString(); } else { txtFederalTax.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_OFFICE_FAX"].ToString() != "&nbsp;") { txtFax.Text = grdOfficeList.DataKeys[index]["SZ_OFFICE_FAX"].ToString(); } else { txtFax.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_OFFICE_EMAIL"].ToString() != "&nbsp;") { txtEmail.Text = grdOfficeList.DataKeys[index]["SZ_OFFICE_EMAIL"].ToString(); } else { txtEmail.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_PREFIX"].ToString() != "&nbsp;") { txtPrefix.Text = grdOfficeList.DataKeys[index]["SZ_PREFIX"].ToString(); } else { txtPrefix.Text = ""; }
                if (grdOfficeList.DataKeys[index]["SZ_LOCATION_ID"].ToString() != "&nbsp;") { extddlLocation.Text = grdOfficeList.DataKeys[index]["SZ_LOCATION_ID"].ToString(); } else { extddlLocation.Text = ""; }
                if (grdOfficeList.DataKeys[index]["sz_place_of_service"].ToString() != "&nbsp;") { txtOfficeCode.Text = grdOfficeList.DataKeys[index]["sz_place_of_service"].ToString(); } else { txtOfficeCode.Text = ""; }
                // added by Kapil 05 Jan 2012

                if (grdOfficeList.DataKeys[index]["IS_SOFTWARE_FEE_ADDED"].ToString() != "&nbsp;" && grdOfficeList.DataKeys[index]["IS_SOFTWARE_FEE_ADDED"].ToString() != "")
                {
                    if (grdOfficeList.DataKeys[index]["IS_SOFTWARE_FEE_ADDED"].ToString().ToLower() == "true")
                    {
                        //txtSoftwareFee.Text = grdOfficeList.DataKeys[index]["MN_SOFTWARE_FEE"].ToString();                        
                        //float.Parse(this.txtSoftwareFee.Text, System.Globalization.NumberStyles.Currency);
                        txtSoftwareFee.Text = Convert.ToDouble(grdOfficeList.DataKeys[index]["MN_SOFTWARE_FEE"]).ToString();
                        chkSoftwareFee.Checked = true;
                    }
                    else
                    {
                        // NumberFormatInfo numberInfo = CultureInfo.CurrentCulture.NumberFormat;
                        //txtSoftwareFee.Text="";
                        //double dbl = Convert.ToDouble(grdOfficeList.DataKeys[index]["MN_SOFTWARE_FEE"]);
                        //dbl = Math.Round(dbl,2);
                        //txtSoftwareFee.Text = dbl.ToString("c", numberInfo);

                        txtSoftwareFee.Text = (Convert.ToDouble(grdOfficeList.DataKeys[index]["MN_SOFTWARE_FEE"])).ToString();
                        chkSoftwareFee.Checked = false;
                    }
                }
                else
                {
                    txtSoftwareFee.Text = "";
                    chkSoftwareFee.Checked = false;
                }

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;


                bool bt_referring_facility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;

                if ((bt_referring_facility == true) || (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
                {
                }
                else
                {
                    SetReadOnly();
                }
                //Page.RegisterStartupScript("TestString", "<script language='javascript'> SoftwareFee(); </script>"); 
                txtSoftwareFee.Enabled = true;
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
    protected void extddlLocation_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _Location = new LocationBO();
            if (extddlLocation.Text != "NA")
            {
                DataSet dsLocation = _Location.Location(extddlLocation.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                txtOfficeCity.Text = dsLocation.Tables[0].Rows[0]["SZ_CITY"].ToString();
                txtOfficeZip.Text = dsLocation.Tables[0].Rows[0]["SZ_ZIP"].ToString();
                txtOfficeAdd.Text = dsLocation.Tables[0].Rows[0]["SZ_ADDRESS"].ToString();
                extddlOfficeState.Text = dsLocation.Tables[0].Rows[0]["SZ_STATE_ID"].ToString();
                SetReadOnly();
            }
            else
            {
                txtOfficeCity.Text = "";
                txtOfficeZip.Text = "";
                txtOfficeAdd.Text = "";
                extddlOfficeState.Text = "NA";
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
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdOfficeList.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);

    }
}