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
using Reminders;

public partial class AJAX_Pages_Bill_Sys_Reminder_Type : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        utxtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        btndelete.Attributes.Add("onclick", "return Confirm_Delete_Code();");
        btnSave.Attributes.Add("onclick", "return CheckControls();");
        this.con.SourceGrid = grdremindertype;
        this.txtSearchBox.SourceGrid = grdremindertype;
        this.grdremindertype.Page = this.Page;
        this.grdremindertype.PageNumberList = this.con;

        if (!IsPostBack)
        {
            fillcontrol();
            grdremindertype.XGridBindSearch();
            btnUpdate.Visible = false;
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillcontrol();
        grdremindertype.XGridBindSearch();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string szreturn = "";
        ReminderBO _ReminderBO = new ReminderBO();
        try
        {
           
                szreturn = _ReminderBO.AddReminderType(txtCompanyID.Text, txtremindertype.Text, utxtUserId.Text);
                if (szreturn == "added with success")
                {
                    usrMessage.PutMessage("Reminder Type save successfully ...");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                    grdremindertype.XGridBindSearch();
                    ClearControls();
                }

                else if (szreturn == "exists")
                {
                    usrMessage.PutMessage("Reminder Type is already Exists ...");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                    grdremindertype.XGridBindSearch();
                    ClearControls();

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
        ReminderBO objReminderBO = new ReminderBO();
        try
        {
            objReminderBO.UpdateReminderType(txtremindertype.Text, txtCompanyID.Text, utxtUserId.Text, txtremindertypeid.Text);
            usrMessage.PutMessage("Reminder Type Update successfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            grdremindertype.XGridBindSearch();
            ClearControls();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
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
        ClearControls();
    }

    protected void grdremindertype_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (e.CommandName == "Edit")
        {
            try
            {
                int i = Convert.ToInt32(e.CommandArgument.ToString());
                txtremindertype.Text = grdremindertype.DataKeys[i]["SZ_REMINDER_TYPE"].ToString();
                txtremindertypeid.Text = grdremindertype.DataKeys[i]["I_REMINDER_TYPE_ID"].ToString();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
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
    protected void grdremindertype_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = null;
        objReminder = new ReminderBO();
        try
        {

            for (int i = 0; i < grdremindertype.Rows.Count; i++)
            {
                CheckBox chkDelete = (CheckBox)grdremindertype.Rows[i].FindControl("chkDelete");
                string iremindertypeid = grdremindertype.DataKeys[i]["I_REMINDER_TYPE_ID"].ToString();
                if (chkDelete.Checked)
                {
                    objReminder.RemoveReminderType(iremindertypeid, txtCompanyID.Text, utxtUserId.Text);
                }

            }
            usrMessage.PutMessage("Reminder Type Delete successfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            grdremindertype.XGridBindSearch();
            ClearControls();
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
    public void fillcontrol()
    {

        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        utxtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        utxtremindertype.Text = txtremindertype.Text;
    }

    public void ClearControls()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtremindertype.Text = "";
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


