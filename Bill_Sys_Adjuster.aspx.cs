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
public partial class Billing_Sys_Adjuster : PageBase
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
           
            btnSave.Attributes.Add("onclick", "return formValidator('aspnetForm','txtAdjusterName');");
            //     btnSave.Attributes.Add("onclick", "return isLocalEmail('aspnetForm','txtEmail');");
            
            btnUpdate.Attributes.Add("onclick", "return formValidator('aspnetForm','txtAdjusterName');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
            if (Session["Flag"] != null && Session["Flag"].ToString() == "true")
            {
                //TreeMenuControl1.Visible = false;
                grdAdjuster.Visible = false;
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
            cv.MakeReadOnlyPage("Bill_Sys_Adjuster.aspx");
        }
        #endregion
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
        _saveOperation = new SaveOperation();
        try
        {
            if (Page.IsValid)
            {
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "Adjuster.xml";
                _saveOperation.SaveMethod();
                BindGrid();
                ClearControl();
                lblMsg.Visible = true;
                lblMsg.Text = "Adjuster Saved Successfully...!";
                
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


    #region "Fetch Method"

    private void BindGrid()
    {
        _listOperation = new ListOperation();
        try
        {
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "Adjuster.xml";
            _listOperation.LoadList();

        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
    #endregion

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
                _editOperation.Primary_Value = Session["AdjusterID"].ToString();
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "Adjuster.xml";
                _editOperation.UpdateMethod();
                BindGrid();
                lblMsg.Visible = true;
                lblMsg.Text = "Client Type Updated Successfully...!";
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
    protected void grdAdjuster_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["AdjusterID"] = grdAdjuster.Items[grdAdjuster.SelectedIndex].Cells[1].Text;
            txtAdjusterName.Text = grdAdjuster.Items[grdAdjuster.SelectedIndex].Cells[2].Text;
            if (grdAdjuster.Items[grdAdjuster.SelectedIndex].Cells[3].Text.ToString() != "&nbsp;") { txtPhone.Text = grdAdjuster.Items[grdAdjuster.SelectedIndex].Cells[3].Text; } else { txtPhone.Text = ""; }
            if (grdAdjuster.Items[grdAdjuster.SelectedIndex].Cells[4].Text.ToString() != "&nbsp;") { txtExtension.Text = grdAdjuster.Items[grdAdjuster.SelectedIndex].Cells[4].Text; } else { txtExtension.Text = ""; }
            if (grdAdjuster.Items[grdAdjuster.SelectedIndex].Cells[5].Text.ToString()!="&nbsp;") { txtFax.Text = grdAdjuster.Items[grdAdjuster.SelectedIndex].Cells[5].Text;} else { txtFax.Text = ""; }
            if (grdAdjuster.Items[grdAdjuster.SelectedIndex].Cells[6].Text.ToString() != "&nbsp;") { txtEmail.Text = grdAdjuster.Items[grdAdjuster.SelectedIndex].Cells[6].Text; } else { txtEmail.Text = ""; }
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
    protected void grdAdjuster_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdAdjuster.CurrentPageIndex = e.NewPageIndex;
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


    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtAdjusterName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtExtension.Text = "";
            txtFax.Text = "";
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _deleteOpeation = new Bill_Sys_DeleteBO();
        String szListOfAdjuster = "";
        try
        {
            for (int i = 0; i < grdAdjuster.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdAdjuster.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    if (!_deleteOpeation.deleteRecord("SP_MST_ADJUSTER", "@SZ_ADJUSTER_ID", grdAdjuster.Items[i].Cells[1].Text))
                    {
                        if (szListOfAdjuster == "")
                        {
                            szListOfAdjuster = grdAdjuster.Items[i].Cells[2].Text ;
                        }
                        else
                        {
                            szListOfAdjuster = szListOfAdjuster + " , " + grdAdjuster.Items[i].Cells[2].Text ;
                        }
                    }
                }
            }
            if (szListOfAdjuster != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Adjuster " + szListOfAdjuster + "  exists.'); ", true);
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Adjuster deleted successfully ...";
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