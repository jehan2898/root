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

public partial class AJAX_Pages_Bill_Sys_Casetype_BillConfig : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtUserid.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            btnsave.Attributes.Add("onclick", "return val_CheckControls();");
            btnUpdate.Attributes.Add("onclick", "return val_CheckControls();");
            btnDelete.Attributes.Add("onclick", "return callfordelete();");
            if (!IsPostBack)
            {
                extddlCaseType.Flag_ID = txtCompanyID.Text;
                btnUpdate.Enabled = false;
                hdndelete.Value = "";
            }

            if (hdndelete.Value != "true")
            {
                DataSet dscasetypewithbill = new DataSet();
                Bill_Sys_bill_type_config _Bill_Sys_bill_type_config = new Bill_Sys_bill_type_config();
                dscasetypewithbill = _Bill_Sys_bill_type_config.GetCaseTypewithbillList(txtCompanyID.Text);
                grdcasetypewithbill.DataSource = dscasetypewithbill;
                grdcasetypewithbill.DataBind();
            }
            else
            {
                hdndelete.Value = "";
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_bill_type_config _Bill_Sys_bill_type_config = new Bill_Sys_bill_type_config();
        try
        {
            if (extddlCaseType.Text != "NA" && extddlCaseType.Text != null)
            {
                if (extddlBillType.Text != "NA" && extddlBillType.Text != null)
                {
                    _Bill_Sys_bill_type_config.AddCasetypewithbill(txtCompanyID.Text, extddlCaseType.Text, txtUserid.Text, extddlBillType.Text, extddlBillType.Selected_Text, "1");
                    DataSet dscasetypewithbill = new DataSet();
                    dscasetypewithbill = _Bill_Sys_bill_type_config.GetCaseTypewithbillList(txtCompanyID.Text);
                    grdcasetypewithbill.DataSource = dscasetypewithbill;
                    grdcasetypewithbill.DataBind();
                    usrMessage.PutMessage("Save Successfully ...!");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                    ClearControl();
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        btnsave.Enabled = true;
        Bill_Sys_bill_type_config _Bill_Sys_bill_type_config = new Bill_Sys_bill_type_config();
        try
        {
            if (extddlCaseType.Text != "NA" && extddlCaseType.Text != null && extddlBillType.Text != "NA" && extddlBillType.Text != null)
            {
                _Bill_Sys_bill_type_config.UpdateCasetypewithbill(txtCompanyID.Text,extddlCaseType.Text,extddlBillType.Text,extddlBillType.Selected_Text,"1");
                DataSet dscasetypewithbill = new DataSet();
                dscasetypewithbill = _Bill_Sys_bill_type_config.GetCaseTypewithbillList(txtCompanyID.Text);
                grdcasetypewithbill.DataSource = dscasetypewithbill;
                grdcasetypewithbill.DataBind();
                usrMessage.PutMessage("Update Successfully ...!");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                ClearControl();
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
    protected void grdcasetypewithbill_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        btnUpdate.Enabled = true;
        btnsave.Enabled = false;
        extddlCaseType.Enabled = false;
        if (e.CommandArgs.CommandName == "Select")
        {
            try
            {
                if (Convert.ToString(grdcasetypewithbill.GetRowValues(grdcasetypewithbill.FocusedRowIndex, "SZ_CASE_TYPE_ID")) != "&nbsp;")
                {
                    extddlCaseType.Text = Convert.ToString(grdcasetypewithbill.GetRowValues(grdcasetypewithbill.FocusedRowIndex, "SZ_CASE_TYPE_ID")); 
                }
                else
                {
                    extddlCaseType.Text = "";
                }
                if (Convert.ToString(grdcasetypewithbill.GetRowValues(grdcasetypewithbill.FocusedRowIndex, "SZ_BILLTYPE_ABBRIVATION_ID")) != "&nbsp;")
                {
                    extddlBillType.Text = Convert.ToString(grdcasetypewithbill.GetRowValues(grdcasetypewithbill.FocusedRowIndex, "SZ_BILLTYPE_ABBRIVATION_ID"));
                } 
                else 
                {
                    extddlBillType.Text = ""; 
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
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void ClearControl()
    {
        extddlCaseType.Text = "";
        extddlBillType.Text = "";
        extddlCaseType.Enabled = true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_bill_type_config _Bill_Sys_bill_type_config = new Bill_Sys_bill_type_config();
        btnUpdate.Enabled = false;
        try
        {
            for (int i = 0; i < grdcasetypewithbill.VisibleRowCount; i++)
            {
                ASPxGridView _ASPxGridView = (ASPxGridView)grdid.FindControl("grdcasetypewithbill");
                GridViewDataColumn c = (GridViewDataColumn)grdcasetypewithbill.Columns[5];
                CheckBox checkBox = (CheckBox)grdcasetypewithbill.FindRowCellTemplateControl(i, c, "chkall");
                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        _Bill_Sys_bill_type_config.DeleteCasetypewithbill(txtCompanyID.Text, Convert.ToString(grdcasetypewithbill.GetRowValues(i, "SZ_CASE_TYPE_ID")));
                        ClearControl();
                        usrMessage.PutMessage("Delete Successfully ...!");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                    }
                }
            }
            DataSet dscasetypewithbill = new DataSet();
            dscasetypewithbill = _Bill_Sys_bill_type_config.GetCaseTypewithbillList(txtCompanyID.Text);
            grdcasetypewithbill.DataSource = dscasetypewithbill;
            grdcasetypewithbill.DataBind();

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
        btnsave.Enabled = true;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
