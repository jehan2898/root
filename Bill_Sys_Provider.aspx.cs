/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_Provider.aspx.cs
/*Purpose              :       To Add and Edit Payment Scheme 
/*Author               :       Manoj c
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
public partial class Bill_Sys_Provider : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            
            btnSave.Attributes.Add("onclick", "return formValidator('frmProvider','txtProviderName,extddlCompanyList');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmProvider','txtProviderName,extddlCompanyList');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (Session["Flag"] != null && Session["Flag"].ToString() == "true")
            {
                //TreeMenuControl1.Visible = false;
                grdProvider.Visible = false;
                btnUpdate.Visible = false;
            }
            else
            {
                if (!IsPostBack)
                {
                    //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; ;                    
                    BindGrid();
                    btnUpdate.Enabled = false;
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
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_Provider.aspx");
        }
        #endregion
        //Method End
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
    #region "Event Handler"    
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
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "Provider.xml";
                _saveOperation.SaveMethod();
                BindGrid();
                lblMsg.Visible = true;
                lblMsg.Text = " Provider Information Saved successfully ! ";
                Response.Write("<script>window.opener.location.replace('Bill_Sys_CaseMaster.aspx')</script>");
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
    protected void grdProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
            Session["ProviderID"] = grdProvider.Items[grdProvider.SelectedIndex].Cells[1].Text;
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[2].Text != "&nbsp;") { txtProviderName.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[2].Text; }
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[3].Text != "&nbsp;") txtProviderType.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[3].Text;

            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[5].Text != "&nbsp;") txtProviderLocalAddress.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[5].Text;
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[6].Text != "&nbsp;") txtProviderLocalCity.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[6].Text;
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[7].Text != "&nbsp;") txtProviderLocalState.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[7].Text;
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[8].Text != "&nbsp;") txtProviderLocalZip.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[8].Text;
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[9].Text != "&nbsp;") txtProviderContact.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[9].Text;
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[10].Text != "&nbsp;") txtProviderPermAddress.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[10].Text;
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[11].Text != "&nbsp;") txtProviderPermCity.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[11].Text;
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[12].Text != "&nbsp;") txtProviderPermState.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[12].Text;
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[13].Text != "&nbsp;") txtProviderPermZip.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[13].Text;
            if (grdProvider.Items[grdProvider.SelectedIndex].Cells[14].Text != "&nbsp;") txtProviderPermPhone.Text = grdProvider.Items[grdProvider.SelectedIndex].Cells[14].Text;
           
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
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Provider.xml";
            _editOperation.Primary_Value = Session["ProviderID"].ToString();
            _editOperation.UpdateMethod();
            BindGrid();
            lblMsg.Visible = true;
            lblMsg.Text = " Provider Information Updated successfully ! ";
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
            txtProviderName.Text = "";
            txtProviderType.Text = "";
            txtProviderLocalAddress.Text = "";
            txtProviderLocalCity.Text = "";
            txtProviderLocalState.Text = "";
            txtProviderLocalZip.Text = "";
            txtProviderContact.Text = "";
            txtProviderPermAddress.Text = "";
            txtProviderPermCity.Text = "";
            txtProviderPermState.Text = "";
            txtProviderPermZip.Text = "";
            txtProviderPermPhone.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            lblMsg.Text = "";
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
    protected void grdProvider_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdProvider.CurrentPageIndex = e.NewPageIndex;
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
    #endregion
    #region "Fetch Method"
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
            _listOperation.Xml_File = "Provider.xml";
            _listOperation.WebPage = this.Page;
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
    #endregion
}
