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
using DevExpress.Web;

public partial class AJAX_Pages_Bill_Sys_Modifier : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        btnAdd.Attributes.Add("onclick", "return CheckModifierExists();");
        btnUpdate.Attributes.Add("onclick", "return CheckModifierExists();");
        btnDelete.Attributes.Add("onclick", "return checkModifierChecked();");
        try
        {
            if (!IsPostBack)
            {
                bindGrid();
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
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

    private void bindGrid()
    {
        DataSet ds = new DataSet();
        Bill_Sys_Modifier objModifier = new Bill_Sys_Modifier();
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        ds = objModifier.GetModifierData(txtCompanyID.Text);
        grdModifier.DataSource = ds;
        grdModifier.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_Modifier objModifier = new Bill_Sys_Modifier();
            string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            string msg = objModifier.ModifierData(txtCompanyID.Text, UserId, txtModifier.Text, txtIDCode.Text, txtModifierDesc.Text, "", "ADD");
            bindGrid();
            if (msg == "SUCCESS")
            {
                this.usrMessage.PutMessage("Modifier added succesfully.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.usrMessage.PutMessage(msg);
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_Modifier objModifier = new Bill_Sys_Modifier();
            string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            string msg = objModifier.ModifierData(txtCompanyID.Text, UserId, txtModifier.Text, txtIDCode.Text, txtModifierDesc.Text, txtSelectedModifierID.Text, "UPDATE");
            bindGrid();
            if (msg == "SUCCESS")
            {
                this.usrMessage.PutMessage("Modifier updated succesfully.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.usrMessage.PutMessage(msg);
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_Modifier objModifier = new Bill_Sys_Modifier();
            string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            DataSet dsModifier = new DataSet();
            dsModifier = objModifier.SearchModifierData(txtCompanyID.Text, UserId, txtModifier.Text, txtIDCode.Text, txtModifierDesc.Text, "LIST");
            grdModifier.DataSource = dsModifier;
            grdModifier.DataBind();
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
        try
        {
            string finalModifierID = "";
            for (int i = 0; i < grdModifier.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdModifier.Columns[5];
                CheckBox chk = (CheckBox)grdModifier.FindRowCellTemplateControl(i, c, "chkall1");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        string ModifierID = grdModifier.GetRowValues(i, "i_modifier_id").ToString();

                        finalModifierID += "," + ModifierID;
                    }
                }
            }
            if (finalModifierID != "")
            {
                finalModifierID = finalModifierID.Remove(0, 1);
            }
            Bill_Sys_Modifier objModifier = new Bill_Sys_Modifier();
            string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            string msg = objModifier.DeleteModifierData(txtCompanyID.Text, finalModifierID, "DELETE");
            bindGrid();
            if (msg == "SUCCESS")
            {
                this.usrMessage.PutMessage("Modifier deleted succesfully.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.usrMessage.PutMessage(msg);
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
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

    protected void grdModifier_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandArgs.CommandName == "select")
            {
                ASPxGridView grid = (ASPxGridView)sender;
                object id_ = e.KeyValue;
                txtModifier.Text = grid.GetRowValuesByKeyValue(id_, "sz_modifier").ToString();
                txtIDCode.Text = grid.GetRowValuesByKeyValue(id_, "sz_code").ToString();
                txtModifierDesc.Text = grid.GetRowValuesByKeyValue(id_, "sz_modifier_desc").ToString();
                txtSelectedModifierID.Text = grid.GetRowValuesByKeyValue(id_, "i_modifier_id").ToString();
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
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
}
