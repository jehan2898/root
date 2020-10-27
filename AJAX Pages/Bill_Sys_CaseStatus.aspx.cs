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
using log4net;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using Componend;

public partial class AJAX_Pages_Bill_Sys_CaseStatus : PageBase
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

            btnSave.Attributes.Add("onclick", "return formValidator('aspnetForm','txtCaseStatus');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('aspnetForm','txtCaseStatus');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            this.con.SourceGrid = grdCaseStatusList;
            this.grdCaseStatusList.Page = this.Page;
            this.grdCaseStatusList.PageNumberList = this.con;
            if (Session["Flag"] != null && Session["Flag"].ToString() == "true")
            {
                grdCaseStatusList.Visible = false;
                btnUpdate.Visible = false;
            }
            else
            {
                if (!IsPostBack)
                {
                 
                    grdCaseStatusList.XGridBind();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("AJAX Pages/Bill_Sys_CaseStatus.aspx");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (txtCaseStatus.Text == "")
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Insert all records";
        }
        else
        {
            txtddlStatus.Text = ddl_Case_Status.SelectedValue.ToString();
            utxtCaseStatus.Text = txtCaseStatus.Text.Trim();
            _saveOperation = new SaveOperation();
            try
            {
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "CaseStatus.xml";
                _saveOperation.SaveMethod();
                ClearControl();
                grdCaseStatusList.XGridBind();
                lblMsg.Visible = true;
                lblMsg.Text = "Case Status Saved Successfully ...!";

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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        txtddlStatus.Text = ddl_Case_Status.SelectedValue.ToString();
        utxtCaseStatus.Text = txtCaseStatus.Text.Trim();
        _editOperation = new EditOperation();
        try
        {
            if (Session["CaseStatusID"].ToString() != "")
            {
                _editOperation.Primary_Value = Session["CaseStatusID"].ToString();
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "CaseStatus.xml";
                _editOperation.UpdateMethod();
                grdCaseStatusList.XGridBind();
                lblMsg.Visible = true;
                lblMsg.Text = "Case Status Updated Successfully ...!";
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


   
    protected void grdCaseStatusList_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void grdCaseStatusList_DeleteCommand(object source, DataGridCommandEventArgs e)
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            txtCaseStatus.Text = "";
            ddl_Case_Status.SelectedValue = "0";
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void grdCaseStatusList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
        String szListOfCaseStatus = "";
        try
        {
            for (int i = 0; i < grdCaseStatusList.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdCaseStatusList.Rows[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    if (!_deleteOpeation.deleteRecord("SP_MST_CASE_STATUS", "@SZ_CASE_STATUS_ID", grdCaseStatusList.DataKeys[i][0].ToString()))
                    {
                        if (szListOfCaseStatus == "")
                        {
                            szListOfCaseStatus = grdCaseStatusList.DataKeys[i][1].ToString();
                        }
                        else
                        {
                            szListOfCaseStatus = szListOfCaseStatus + " , " + grdCaseStatusList.DataKeys[i][1].ToString();
                        }
                    }
                }
            }
            if (szListOfCaseStatus != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Case Status " + szListOfCaseStatus + "  exists.'); ", true);
            }
            else
            {
                ClearControl();
                grdCaseStatusList.XGridBind();
                lblMsg.Visible = true;
                lblMsg.Text = "Case Status deleted successfully ...";
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


    protected void grdCaseStatusList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int index = Convert.ToInt16(e.CommandArgument.ToString() );
        try
        {

            Session["CaseStatusID"] = grdCaseStatusList.DataKeys[index][0].ToString();
            txtCaseStatus.Text= grdCaseStatusList.DataKeys[index][1].ToString();
          
            if (grdCaseStatusList.DataKeys[index][1].ToString() != "&nbsp;") { txtCaseStatus.Text =grdCaseStatusList.DataKeys[index][1].ToString(); }
            if ("0" == grdCaseStatusList.DataKeys[index][2].ToString().ToLower())
            {
                foreach (ListItem li in ddl_Case_Status.Items)
                {
                    if (li.Value == "0")
                    {
                        ddl_Case_Status.SelectedValue = "0";
                        break;
                    }
                }
            }
            else if ("1" == grdCaseStatusList.DataKeys[index][2].ToString().ToLower())
            {
                foreach (ListItem li in ddl_Case_Status.Items)
                {
                    if (li.Value == "1")
                    {
                        ddl_Case_Status.SelectedValue = "1";
                        break;
                    }
                }
            }
            else if ("2" == grdCaseStatusList.DataKeys[index][2].ToString().ToLower())
            {
                foreach (ListItem li in ddl_Case_Status.Items)
                {
                    if (li.Value == "2")
                    {
                        ddl_Case_Status.SelectedValue = "2";
                        break;
                    }
                }
            }
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}

