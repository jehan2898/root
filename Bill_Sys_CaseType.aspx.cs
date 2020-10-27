/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_CaseType.aspx.cs
/*Purpose              :       To Add and Edit Case Type
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

public partial class Bill_Sys_CaseType : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;


    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this._listOperation.WebPage=this.Page;
            this._listOperation.Xml_File="CaseType.xml";
            this._listOperation.LoadList();
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
            this.ClearControl();
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
        this._deleteOpeation = new Bill_Sys_DeleteBO();
        string text = "";
        try
        {
            for (int i = 0; i < this.grdCaseType.Items.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdCaseType.Items[i].FindControl("chkDelete");
                if (box.Checked && !this._deleteOpeation.deleteRecord("SP_MST_CASE_TYPE", "@SZ_CASE_TYPE_ID", this.grdCaseType.Items[i].Cells[1].Text))
                {
                    if (text == "")
                    {
                        text = this.grdCaseType.Items[i].Cells[2].Text;
                    }
                    else
                    {
                        text = text + " , " + this.grdCaseType.Items[i].Cells[2].Text;
                    }
                }
            }
            if (text != "")
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Case Type " + text + "  exists.'); ", true);
            }
            else
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Case Type deleted successfully ...";
            }
            this.BindGrid();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._saveOperation = new SaveOperation();
        try
        {
            this._saveOperation.WebPage=this.Page;
            this._saveOperation.Xml_File="CaseType.xml";
            this._saveOperation.SaveMethod();
            this.BindGrid();
            this.ClearControl();
            this.lblMsg.Visible = true;
            this.lblMsg.Text = "Case Type Saved Successfully ...!";
            base.Response.Write("<script>window.opener.location.replace('Bill_Sys_CaseMaster.aspx')</script>");
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
        this._editOperation = new EditOperation();
        try
        {
            this._editOperation.Primary_Value=this.Session["CaseTypeID"].ToString();
            this._editOperation.WebPage=this.Page;
            this._editOperation.Xml_File="CaseType.xml";
            this._editOperation.UpdateMethod();
            this.BindGrid();
            this.lblMsg.Visible = true;
            this.lblMsg.Text = "Case Type Updated Successfully ...!";
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
            this.txtCaseType.Text = "";
            this.txtCaseTypeAbbrivation.Text = "";
            this.extddlAbbrevation.Text="NA";
            this.btnUpdate.Enabled = false;
            this.btnSave.Enabled = true;
            this.lblMsg.Visible = false;
            this.chkinclude_1500.Checked = false;
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

    protected void grdCaseType_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
    }

    protected void grdCaseType_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdCaseType.CurrentPageIndex = e.NewPageIndex;
            this.BindGrid();
            this.lblMsg.Visible = false;
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

    protected void grdCaseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["CaseTypeID"] = this.grdCaseType.Items[this.grdCaseType.SelectedIndex].Cells[1].Text;
            if (this.grdCaseType.Items[this.grdCaseType.SelectedIndex].Cells[2].Text != "&nbsp;")
            {
                this.txtCaseType.Text = this.grdCaseType.Items[this.grdCaseType.SelectedIndex].Cells[2].Text;
            }
            if (this.grdCaseType.Items[this.grdCaseType.SelectedIndex].Cells[3].Text != "&nbsp;")
            {
                this.txtCaseTypeAbbrivation.Text = this.grdCaseType.Items[this.grdCaseType.SelectedIndex].Cells[3].Text;
            }
            if (this.grdCaseType.Items[this.grdCaseType.SelectedIndex].Cells[4].Text != "&nbsp;")
            {
                this.extddlAbbrevation.Text=this.grdCaseType.Items[this.grdCaseType.SelectedIndex].Cells[4].Text;
            }
            else
            {
                this.extddlAbbrevation.Text="NA";
            }
            if (this.grdCaseType.Items[this.grdCaseType.SelectedIndex].Cells[7].Text == "True")
            {
                this.chkinclude_1500.Checked = true;
            }
            else
            {
                this.chkinclude_1500.Checked = false;
            }
            if (grdCaseType.Items[grdCaseType.SelectedIndex].Cells[8].Text == "True")
            {
                chkDiagnosisCode.Checked = true;
            }
            else
            {
                chkDiagnosisCode.Checked = false;
            }
            if (grdCaseType.Items[grdCaseType.SelectedIndex].Cells[9].Text == "True")
            {
                chkModifyProcedureCode.Checked = true;
            }
            else
            {
                chkModifyProcedureCode.Checked = false;
            }
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.lblMsg.Visible = false;
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

    protected override void OnUnload(EventArgs e)
    {
        this.Session["Flag"] = null;
        base.OnUnload(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.btnSave.Attributes.Add("onclick", "return formValidator('frmCaseType','txtCaseType,txtCaseTypeAbbrivation');");
            this.btnUpdate.Attributes.Add("onclick", "return formValidator('frmCaseType','txtCaseType,txtCaseTypeAbbrivation');");
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if ((this.Session["Flag"] != null) && (this.Session["Flag"].ToString() == "true"))
            {
                this.grdCaseType.Visible = false;
                this.btnUpdate.Visible = false;
            }
            else if (!base.IsPostBack)
            {
                this.BindGrid();
                this.btnUpdate.Enabled = false;
            }
            this._deleteOpeation = new Bill_Sys_DeleteBO();
            if (this._deleteOpeation.checkForDelete(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE))
            {
                this.btnDelete.Visible = false;
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
        
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_CaseType.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

}

