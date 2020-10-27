/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_UserMaster.aspx.cs
/*Purpose              :       To Add and Edit user details 
/*Author               :       Manoj c
/*Date of creation     :       12 Dec 2008  
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
using System.Data.SqlClient;

public partial class Bill_Sys_ChangePasswordMaster : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private string strcon;
    SqlDataReader dr;
    private string struname;
    SqlConnection sqlcon;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnSave.Attributes.Add("onclick", "return formValidator('frmUserMaster','txtUserName,txtOldPassword,txtNewPassword,txtConfirmPassword');");
            strcon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            txtUserName.Text = GetUserName(Session["UserID"].ToString());
            txtUserName.Enabled = false;
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
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
            cv.MakeReadOnlyPage("Bill_Sys_ChangePasswordMaster.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected string GetUserName(string uid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();

            SqlCommand comm = new SqlCommand("Select SZ_USER_NAME,SZ_PASSWORD FROM MST_USERS WHERE SZ_USER_ID='" + uid + "'", sqlcon);
            dr=comm.ExecuteReader();
            while (dr.Read())
            {
                struname = dr[0].ToString();
                Session["User_Password"] = dr[1].ToString();
            }
            
            sqlcon.Close();
            return struname;
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
            return null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    #region "Event Handler"
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            if (txtNewPassword.Text == txtConfirmPassword.Text)
            {
                txtOldPassword.Text = EncryptPassword(txtOldPassword.Text);
                if (Session["User_Password"].ToString() == txtOldPassword.Text)
                {
                    txtNewPassword.Text = EncryptPassword(txtNewPassword.Text);
                    sqlcon = new SqlConnection(strcon);
                    sqlcon.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "SP_MST_USERS_CHANGEPASSWORD";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = sqlcon;
                    comm.Parameters.AddWithValue("@SZ_USER_ID", Session["UserID"].ToString());
                    comm.Parameters.AddWithValue("@SZ_USER_NAME", txtUserName.Text);
                    comm.Parameters.AddWithValue("@SZ_PASSWORD", txtNewPassword.Text);
                    comm.ExecuteNonQuery();
                    sqlcon.Close();
                    lblMsg.Visible = true;
                    lblMsg.Text = " User Password Updated successfully ! ";
                }
                else
                {
                    lblMsg.Text = "Please enter your password correctly!";
                    lblMsg.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "Passwords do not match!";
                lblMsg.Visible = true;
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            Session["UserID"] = "";
            lblMsg.Visible = false;
            lblMsg.Text = "";
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
    private string EncryptPassword(string strtext)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strPassPhrase = "Pas5pr@se";        // can be any string
        string strSaltValue = "s@1tValue";              // can be any string
        string strHashAlgorithm = "SHA1";           // can be "MD5"
        int intPasswordIterations = 2;           // can be any number
        string strInitVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        int intKeySize = 256;
        string EncryptedPassword = "";
        try
        {
            EncryptedPassword = Bill_Sys_EncryDecry.Encrypt(strtext, strPassPhrase, strSaltValue, strHashAlgorithm, intPasswordIterations, strInitVector, intKeySize);
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
        
        return EncryptedPassword;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion
}
