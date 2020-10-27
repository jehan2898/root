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
using System.Data.SqlClient;
using System.Security.Cryptography;

public partial class AJAX_Pages_Bill_Sys_ChangePassword : PageBase
{
    String strConn;
    SqlConnection conn;
    SqlCommand SqlCmd;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ClearControls()
    {
        txtNewPassword.Text = "";
        txtConfirmPassword.Text = "";
        //txtdays.Value = "";
        //rdbNeverExp.Checked = false;
        //rdbPrompt.Checked = false;
        lblMsg.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strConfirmPassword, strdays, NextExpiry, UID;
        if (txtNewPassword.Text == "" || txtConfirmPassword.Text == "")
        {
            lblMsg.Text = "Please enter new password.";
            return;
        }
        //if (!rdbPrompt.Checked && !rdbNeverExp.Checked)
        //{
        //    lblMsg.Text = "Please select at least one condition.";
        //    return;
        //}
        //if (rdbPrompt.Checked)
        //{
        //    if (txtdays.Value == "")
        //    {
        //        lblMsg.Text = "Please enter days";
        //        return;
        //    }
        //    if (txtdays.Value == "0")
        //    {
        //        lblMsg.Text = "Number of days must be more than 0.";
        //        return;
        //    }
        //}
        try
        {
            Bill_Sys_LoginBO _obj = new Bill_Sys_LoginBO();
            strdays = "30";
            strConfirmPassword = txtConfirmPassword.Text;
            if (strdays != "")
            {
                DateTime today = System.DateTime.Now;
                NextExpiry = today.AddDays(Convert.ToDouble(strdays)).ToString();
            }
            else
            {
                strdays = "0";
                NextExpiry = DBNull.Value.ToString();
            }
            UID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            int iResult = 0;
            iResult = _obj.ForceChangePassword(strdays, NextExpiry, strConfirmPassword, UID);
            if (iResult == 1)
            {
                lblMsg.Text = "Password changed successfully.";
                Session.Abandon();
                Response.Redirect("../Bill_Sys_Login.aspx", false);
            }
            else
            {
                lblMsg.Text = "Please type a different password!";
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
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
