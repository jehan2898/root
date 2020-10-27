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

public partial class Bill_Sys_BillingOffice : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            btnSave.Attributes.Add("onclick", "return formValidator('aspnetForm','txtBilingOfficeName');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('aspnetForm','txtBilingOfficeName');");            
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
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
            cv.MakeReadOnlyPage("Bill_Sys_BillingOffice.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        #endregion
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
                _saveOperation.Xml_File = "BillingOffice.xml";
                _saveOperation.SaveMethod();
                BindGrid();
                ClearControl();
                lblMsg.Visible = true;
                lblMsg.Text = "Billing Office Saved Successfully ...!";
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

                    _editOperation.Xml_File = "BillingOffice.xml";
                    _editOperation.UpdateMethod();
                    BindGrid();
                    lblMsg.Visible = true;

                    lblMsg.Text = "Billing Office Updated Successfully ...!";
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
            _listOperation.Xml_File = "BillingOffice.xml";
            _listOperation.LoadList();
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
            Session["OfficeID"] = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[1].Text;
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[2].Text != "&nbsp;") { txtBilingOfficeName.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[2].Text; } else { txtBilingOfficeName.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[8].Text != "&nbsp;") { txtBillingOfficeAddress.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[8].Text; } else { txtBillingOfficeAddress.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[9].Text != "&nbsp;") { txtBillingOfficeCity.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[9].Text; } else { txtBillingOfficeCity.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[10].Text != "&nbsp;") { extddlBillingOfficeState.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[10].Text; } else { extddlBillingOfficeState.Text = "NA"; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[13].Text != "&nbsp;") { txtNPI.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[13].Text; } else { txtNPI.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[14].Text != "&nbsp;") { txtFederalTax.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[14].Text; } else { txtFederalTax.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[11].Text != "&nbsp;") { txtBillingOfficeZip.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[11].Text; } else { txtBillingOfficeZip.Text = ""; }
            if (grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[12].Text != "&nbsp;") { txtBillingOfficePhone.Text = grdOfficeList.Items[grdOfficeList.SelectedIndex].Cells[12].Text; } else { txtBillingOfficePhone.Text = ""; }
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
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
            txtBilingOfficeName.Text = "";
            txtBillingOfficeAddress.Text = "";
            txtBillingOfficeCity.Text = "";
            txtFederalTax.Text = "";
            txtNPI.Text = "";
            txtBillingOfficeZip.Text = "";
            extddlBillingOfficeState.Text = "NA";
            txtBillingOfficePhone.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
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
                    if (!_deleteOpeation.deleteRecord("SP_MST_OFFICE", "@SZ_OFFICE_ID", grdOfficeList.Items[i].Cells[1].Text,"BILLING_OFFICE_DELETE"))
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

}

