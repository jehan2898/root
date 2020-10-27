/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_CaseStatus.aspx.cs
/*Purpose              :       To Add and Edit Case Status
/*Author               :       Sandeep Y
/*Date of creation     :       11 Dec 2008  
/*Modified By          :
/*Modified Date        :
/************************************************************/


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

public partial class Bill_Sys_Office : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    private LocationBO _Location;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
                      
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

            btnSave.Attributes.Add("onclick", "return formValidator('aspnetForm','txtOffice');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('aspnetForm','txtOffice');");            
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
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
                    BindGrid();
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
            }
            else
            {
                lblPrefix.Visible = false;
                txtPrefix.Visible = false;
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
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_Office.aspx");
        }
        #endregion
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected override void OnUnload(EventArgs e)
    {
        Session["Flag"] = null;
        base.OnUnload(e);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        {
            if (Page.IsValid)
            {
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "Office.xml";
                _saveOperation.SaveMethod();
                BindGrid();
                ClearControl();
                lblMsg.Visible = true;
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    lblMsg.Text = "Office Saved Successfully ...!";
                }
                else
                {
                    lblMsg.Text = "Provider Saved Successfully ...!";
                }

                Response.Write("<script>window.opener.location.replace('Bill_Sys_CaseMaster.aspx')</script>");
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            if (Page.IsValid)
            {
                if (Session["OfficeID"].ToString() != "")
                {
                    _editOperation.Primary_Value = Session["OfficeID"].ToString();
                    _editOperation.WebPage = this.Page;

                    _editOperation.Xml_File = "Office.xml";
                    _editOperation.UpdateMethod();
                    BindGrid();
                    lblMsg.Visible = true;

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        lblMsg.Text = "Office Updated Successfully ...!";
                    }
                    else
                    {
                        lblMsg.Text = "Provider Updated Successfully ...!";
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
    
    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _listOperation = new ListOperation();
        try
        {
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "Office.xml";
            _listOperation.LoadList();

            bool bt_referring_facility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;

            if ((bt_referring_facility == true) || (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            {
                grdOfficeList.Columns[19].Visible = false;
            }
            else
            {
                grdOfficeList.Columns[19].Visible = true;
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
    
    protected void grdOfficeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            txtOfficeCity.ReadOnly = false;
            txtOfficeZip.ReadOnly = false;
            txtOfficeAdd.ReadOnly = false;
            extddlOfficeState.Enabled = true;

            Session["OfficeID"] = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[1].Text;
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[2].Text != "&nbsp;") { txtOffice.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[2].Text; } else { txtOffice.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[3].Text != "&nbsp;") { txtOfficeAdd.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[3].Text; } else { txtOfficeAdd.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[4].Text != "&nbsp;") { txtOfficeCity.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[4].Text; } else { txtOfficeCity.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[5].Text != "&nbsp;") { extddlOfficeState.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[5].Text; } else { extddlOfficeState.Text = "NA"; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[6].Text != "&nbsp;") { txtOfficeZip.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[6].Text; } else { txtOfficeZip.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[7].Text != "&nbsp;") { txtOfficePhone.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[7].Text; } else { txtOfficePhone.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[8].Text != "&nbsp;") { txtBillingAdd.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[8].Text; } else { txtBillingAdd.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[9].Text != "&nbsp;") { txtBillingCity.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[9].Text; } else { txtBillingCity.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[10].Text != "&nbsp;") { extddlBillingState.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[10].Text; } else { extddlBillingState.Text = "NA"; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[11].Text != "&nbsp;") { txtBillingZip.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[11].Text; } else { txtBillingZip.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[12].Text != "&nbsp;") { txtBillingPhone.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[12].Text; } else { txtBillingPhone.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[13].Text != "&nbsp;") { txtNPI.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[13].Text; } else { txtNPI.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[14].Text != "&nbsp;") { txtFederalTax.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[14].Text; } else { txtFederalTax.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[15].Text != "&nbsp;") { txtFax.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[15].Text; } else { txtFax.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[16].Text != "&nbsp;") { txtEmail.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[16].Text; } else { txtEmail.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[17].Text != "&nbsp;") { txtPrefix.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[17].Text; } else { txtPrefix.Text = ""; }
           if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[18].Text != "&nbsp;") { extddlLocation.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[18].Text; } else { extddlLocation.Text = ""; }


            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            lblMsg.Visible = false;

            bool bt_referring_facility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;

            if ((bt_referring_facility == true) || (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            {
            }
            else
            {
                SetReadOnly();
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
    protected void grdOfficeList_DeleteCommand(object source, DataGridCommandEventArgs e)
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
    protected void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtOffice.Text = "";
            txtBillingAdd.Text = "";
            txtBillingCity.Text = "";
            txtBillingPhone.Text = "";
            extddlBillingState.Text = "NA";
            txtBillingZip.Text = "";
            txtFederalTax.Text = "";
            txtNPI.Text = "";
            txtOfficeAdd.Text = "";
            txtOfficeCity.Text = "";
            txtOfficePhone.Text = "";
            extddlOfficeState.Text = "NA";
            txtOfficeZip.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            extddlLocation.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            lblMsg.Visible = false;
            chkSameAddress.Checked = false;
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ClearControl();
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
    protected void grdOfficeList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdOfficeList.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
            lblMsg.Visible = false;
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
            for (int i = 0; i < grdOfficeList.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdOfficeList.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    if (!_deleteOpeation.deleteRecord("SP_MST_OFFICE", "@SZ_OFFICE_ID", grdOfficeList.Items[i].Cells[1].Text))
                    {
                        if (szListOfOffice == "")
                        {
                            szListOfOffice = grdOfficeList.Items[i].Cells[2].Text;
                        }
                        else
                        {
                            szListOfOffice = szListOfOffice + " , " + grdOfficeList.Items[i].Cells[2].Text;
                        }
                    }
                }
            }
            if (szListOfOffice != "")
            {
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for office " + szListOfOffice + "  exists.'); ", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for provider " + szListOfOffice + "  exists.'); ", true);
                }
                
            }
            else
            {
                lblMsg.Visible = true;
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    lblMsg.Text = "Office deleted successfully ...";
                }
                else
                {
                    lblMsg.Text = "Provider deleted successfully ...";
                }
                
            }
            BindGrid();
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

    protected void extddlLocation_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _Location  = new LocationBO();

            DataSet dsLocation = _Location.Location(extddlLocation.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            txtOfficeCity.Text = dsLocation.Tables[0].Rows[0]["SZ_CITY"].ToString();
            txtOfficeZip.Text = dsLocation.Tables[0].Rows[0]["SZ_ZIP"].ToString();
            txtOfficeAdd.Text = dsLocation.Tables[0].Rows[0]["SZ_ADDRESS"].ToString();
            extddlOfficeState.Text = dsLocation.Tables[0].Rows[0]["SZ_STATE_ID"].ToString();
            SetReadOnly();
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

    protected void SetReadOnly()
    {
        txtOfficeCity.ReadOnly = true;
        txtOfficeZip.ReadOnly = true;
        txtOfficeAdd.ReadOnly = true;
        extddlOfficeState.Enabled = false;
    }
}

