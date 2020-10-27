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
using System.Text.RegularExpressions;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using mbs.provider;
using log4net;
using DevExpress.Web;
using System.Data.SqlClient;



public partial class Bill_Sys_UserMaster : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    private Bill_Sys_DoctorBO _objDoctorBO;
    private static RemoteCertificateValidationCallback ob_remote;
    private mbs.provider.ProviderServices obj_provider;
    private static ILog log = LogManager.GetLogger("Bill_Sys_UserMaster");

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            log.Debug("in Bill_Sys_UserMaster Page_Load");
            //ashutosh..
            //string latest_userID;
            //_objDoctorBO = new Bill_Sys_DoctorBO();
            //latest_userID = _objDoctorBO.GetLatestUserID(txtCompanyID.Text);
            //string strUserId = latest_userID.Substring(0, 2);
            //string strUserId1 = latest_userID.Substring(2, latest_userID.Length-2);

            //int i = (Convert.ToInt32(strUserId1) + 1);
            //txtCurUserID.Text = strUserId + i.ToString();
            //


            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; ;
            btnSave.Attributes.Add("onclick", "return formValidator('frmUserMaster','txtUserName,extddlUserRole,txtPassword,txtConfirmPassword,extddlProviderList,txtUserEmail');");
            btnUpdate.Attributes.Add("onclick", "return formValidatorUpdate('frmUserMaster','txtUserName,extddlUserRole,txtPassword,txtConfirmPassword,extddlProviderList,txtUserEmail');");


            if (extddlOfficeList.Visible)
            {
                btnSave.Attributes.Add("onclick", "return formValidator('frmUserMaster','txtUserName,extddlUserRole,txtPassword,txtConfirmPassword,extddlProviderList,txtUserEmail,extddlOfficeList');");
            }
            else
            {
                btnSave.Attributes.Add("onclick", "return formValidator('frmUserMaster','txtUserName,extddlUserRole,txtPassword,txtConfirmPassword,extddlProviderList,txtUserEmail');");
            }
            if (extddlOfficeList.Visible)
            {
                btnUpdate.Attributes.Add("onclick", "return formValidatorUpdate('frmUserMaster','txtUserName,extddlUserRole,txtPassword,txtConfirmPassword,extddlProviderList,txtUserEmail,extddlOfficeList');");
            }
            else
            {
                btnUpdate.Attributes.Add("onclick", "return formValidatorUpdate('frmUserMaster','txtUserName,extddlUserRole,txtPassword,txtConfirmPassword,extddlProviderList,txtUserEmail');");
            }


            btnSave.Attributes.Add("onclick", "return checkValid();");
            btnUpdate.Attributes.Add("onclick", "return checkValidupdate();");

            //btnDelete.Attributes.Add("onclick", "return check_record();");
            btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
            txtUserEmail.Attributes.Add("onfocusout", "return isEmail('frmUserMaster','txtUserEmail');");
            if (!IsPostBack)
            {
                lblMsg.Visible = false;
                lblMsg.Text = "";
                extddlOfficeList.Visible = false;
                lblReferringOffice.Visible = false;
                LstBxDoctorList.Items.Clear();
                _objDoctorBO = new Bill_Sys_DoctorBO();
                DataSet dset = new DataSet();
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                dset = _objDoctorBO.GetDoctorList(txtCompanyID.Text);
                if (dset.Tables[0].Rows.Count > 0)
                {
                    LstBxDoctorList.DataSource = dset;
                    LstBxDoctorList.DataTextField = "DESCRIPTION";
                    LstBxDoctorList.DataValueField = "CODE";
                    LstBxDoctorList.DataBind();
                }

                extddlUserRole.Flag_ID = txtCompanyID.Text;
                extddlProvider.Flag_ID = txtCompanyID.Text;
                //extdllReffProvider.Flag_ID = txtCompanyID.Text;
                extddlProvider.Visible = false;
                grvProvider.Visible = false;
                BindProviders();
                //extdllReffProvider.Visible = false;
                log.Debug("before BindGrid");
                BindGrid();
                
                log.Debug("after BindGrid");
                log.Debug("before BindProviderGrid");
                BindProviderGrid();
                log.Debug("after BindProviderGrid");
                //btnUpdate.Enabled = false;
          
            }
            
            lblMsg.Visible = false;
            lblMsg.Text = "";
            //ClearControl();
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
            cv.MakeReadOnlyPage("Bill_Sys_UserMaster.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //public struct IntPtr
    //{
    //    private unsafe void* m_value;
    //    public static readonly IntPtr Zero;
    //    internal bool IsNull();
    //    public IntPtr(int value);
    //    public IntPtr(long value);
    //    [CLSCompliant(false)]
    //    public unsafe IntPtr(void* value);
    //    public override bool Equals(object obj);
    //    public override int GetHashCode();
    //    public int ToInt32();
    //    public long ToInt64();
    //    public override string ToString();
    //    public static explicit operator IntPtr(int value);
    //    public static explicit operator IntPtr(long value);
    //    [CLSCompliant(false)]
    //    public static unsafe explicit operator IntPtr(void* value);
    //    [CLSCompliant(false)]
    //    public static unsafe explicit operator void*(IntPtr value);
    //    public static explicit operator int(IntPtr value);
    //    public static explicit operator long(IntPtr value);
    //    public static bool operator ==(IntPtr value1, IntPtr value2);
    //    public static bool operator !=(IntPtr value1, IntPtr value2);
    //    public static int Size { get; }
    //    [CLSCompliant(false)]
    //    public unsafe void* ToPointer();
    //    static IntPtr();
    //}

    #region "Event Handler"
    /*  protected void btnSave_Click(object sender, EventArgs e)
    {
        _saveOperation = new SaveOperation();
        string latest_userID;
        try
        {

            if (Page.IsValid)
            {
              
                txtPassword.Text = EncryptPassword();
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "user.xml";
                _saveOperation.SaveMethod();

                _objDoctorBO = new Bill_Sys_DoctorBO();
                latest_userID = _objDoctorBO.GetLatestUserID(txtCompanyID.Text);
                txtCurUserID.Text = latest_userID;

                _objDoctorBO.DelDocUserID(txtCompanyID.Text, latest_userID);
                foreach (ListItem li in LstBxDoctorList.Items)
                {
                    if (li.Selected == true)
                    {
                        txtDoctorID.Text = li.Value;
                        _objDoctorBO.InsertDoctorUserDetails(txtCompanyID.Text.Trim(), txtCurUserID.Text.Trim(), txtDoctorID.Text.Trim());
                    }
                }
                BindGrid();
                ClearControl();
                lblMsg.Visible = true;
              
                    lblMsg.Text="User Exists";
                
                lblMsg.Text = " User Information Saved successfully ! ";
                
                foreach (ListItem li in LstBxDoctorList.Items)
                {
                    if (li.Selected == true)
                    {
                        li.Selected = false;
                    }
                }
                LstBxDoctorList.Visible = false;
                lblDoctorlst.Visible = false;
            }
            else
            {
                lblMsg.Visible = false;
                lblMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }*/



    /*  protected void btnUpdate_Click(object sender, EventArgs e)
     {
          _editOperation = new EditOperation();
          try
          {
              if (Page.IsValid)
              {

                  string str = extddlProviderList.Text;
                  txtPassword.Text = EncryptPassword();
                  _editOperation.WebPage = this.Page;
                  _editOperation.Xml_File = "user.xml";
                  _editOperation.Primary_Value = Session["UserID"].ToString();
                  _editOperation.UpdateMethod();

                  string UserRole = extddlUserRole.Selected_Text.ToLower();

                  if (UserRole == "doctor")
                  {
                      string UserID = Session["UserID"].ToString();
                      _objDoctorBO = new Bill_Sys_DoctorBO();
                      _objDoctorBO.DelDocUserID(txtCompanyID.Text.Trim(), UserID);
                      foreach (ListItem li in LstBxDoctorList.Items)
                      {
                          if (li.Selected == true)
                          {
                              txtDoctorID.Text = li.Value;
                              _objDoctorBO.InsertDoctorUserDetails(txtCompanyID.Text.Trim(), UserID, txtDoctorID.Text.Trim());
                          }
                      }
                  }


                  BindGrid();
                  lblMsg.Visible = true;
                  //LstBxDoctorList.Visible = false;
                  //lblDoctorlst.Visible = false;
                  lblMsg.Text = " User Information Updated successfully ! ";
              }
              else
              {
                  lblMsg.Visible = false;
                  lblMsg.Text = "";
              }
          }
          catch (Exception ex)
          {
              string strError = ex.Message.ToString();
              strError = strError.Replace("\n", " ");
              Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
          }
      }
      */
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


    protected void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //DropDownList extddlUserRole = new DropDownList();
            //extddlUserRole.Items.Clear();
            txtUserEmail.Text = "";
            txtUserName.Enabled = true;
            txtUserName.Text = string.Empty;
            extddlUserRole.Text = "0";
            extddlOfficeList.Text = "0";
            Session["UserID"] = "";
            txtPassword.Attributes.Add("value", "");
            txtConfirmPassword.Attributes.Add("value", "");
            extddlProviderList.Visible = false;
            extddlOfficeList.Visible = false;
            lblReferringOffice.Visible = false;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            //lblMsg.Visible = false;
            //lblMsg.Text = "";
            extddlProvider.Visible = false;
            grvProvider.Visible = false;
            lblProvider.Visible = false;
            foreach (ListItem li in LstBxDoctorList.Items)
            {
                if (li.Selected == true)
                {
                    li.Selected = false;
                }
            }
            LstBxDoctorList.Visible = false;
            lblDoctorlst.Visible = false;
            chkAllowandshow.Checked = false;
            lblvalidateshow.Visible = false;
            chkAllowandshow.Visible = false;
            lblRefferingProvider.Visible = false;
            grdReffProvider.Visible = false;
            txtReffProvSearch.Visible = false;
            btnSearchRP.Visible = false;
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

    protected void grdUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtUserName.Enabled = false;
            RegularExpressionValidator1.Enabled = false;
            //  txtPassword.Enabled = false;
            // txtConfirmPassword.Enabled = false;
            LstBxDoctorList.Visible = false;
            lblDoctorlst.Visible = false;
            string RoleName = grdUser.Items[grdUser.SelectedIndex].Cells[4].Text.ToLower();
            Session["UserID"] = grdUser.Items[grdUser.SelectedIndex].Cells[1].Text;
            if (grdUser.Items[grdUser.SelectedIndex].Cells[2].Text != "&nbsp;") { txtUserName.Text = grdUser.Items[grdUser.SelectedIndex].Cells[2].Text; } else { txtUserName.Text = ""; }
            if (grdUser.Items[grdUser.SelectedIndex].Cells[3].Text != "&nbsp;") { extddlUserRole.Text = grdUser.Items[grdUser.SelectedIndex].Cells[3].Text; } else { extddlUserRole.Text = "NA"; }
            if (grdUser.Items[grdUser.SelectedIndex].Cells[3].Text == "USR00003")
            {
                extddlProviderList.Visible = true;
                GetProviderList();
                extddlProviderList.Text = grdUser.Items[grdUser.SelectedIndex].Cells[5].Text;
            }
            else
            {
                extddlProviderList.Visible = false;
                extddlProviderList.Text = "NA";
            }
            if (grdUser.Items[grdUser.SelectedIndex].Cells[4].Text.ToLower() == "referring office")
            {
                btnUpdate.Attributes.Add("onclick", "return formValidator('frmUserMaster','txtUserName,extddlUserRole,txtPassword,txtConfirmPassword,extddlProviderList,txtUserEmail,extddlOfficeList');");
                GetOfficeList();
                //change 8-10
                extddlOfficeList.Text = grdUser.Items[grdUser.SelectedIndex].Cells[10].Text;
            }
            else
            {
                lblReferringOffice.Visible = false;
                extddlOfficeList.Visible = false;
                extddlOfficeList.Text = "NA";
            }
            ////change 6-8-6
            if (grdUser.Items[grdUser.SelectedIndex].Cells[6].Text != "&nbsp;") { txtUserEmail.Text = grdUser.Items[grdUser.SelectedIndex].Cells[6].Text; } else { txtUserEmail.Text = ""; }
            ////change 7-9
            if (grdUser.Items[grdUser.SelectedIndex].Cells[9].Text != "&nbsp;")
            {
                txtPassword.Attributes.Add("value", DecryptPassword(grdUser.Items[grdUser.SelectedIndex].Cells[9].Text));
                txtConfirmPassword.Attributes.Add("value", DecryptPassword(grdUser.Items[grdUser.SelectedIndex].Cells[9].Text));
            }
            else
            {
                txtPassword.Attributes.Add("value", "");
                txtConfirmPassword.Attributes.Add("value", "");
            }
            if (RoleName == "reffering provider")
            {
                lblRefferingProvider.Visible = true;
                //extdllReffProvider.Visible = true;
                grdReffProvider.DataSource = null;
                string userid = grdUser.Items[grdUser.SelectedIndex].Cells[1].Text;
                Bill_Sys_BillingCompanyDetails_BO objval = new Bill_Sys_BillingCompanyDetails_BO();
                DataSet dsProv = new DataSet();
                dsProv = objval.getUserRefferingProviderList(userid);
                //grdReffProvider.DataSource = dsProv;
                BindProviderGrid();
                if (dsProv != null)
                {
                    if (dsProv.Tables.Count > 0 && dsProv.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsProv.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < grdReffProvider.Items.Count; j++)
                            {
                                CheckBox box = (CheckBox)this.grdReffProvider.Items[j].FindControl("chkSelete");
                                if (grdReffProvider.Items[j].Cells[2].Text.ToString() == dsProv.Tables[0].Rows[i]["CODE"].ToString())
                                {
                                    box.Checked = true;
                                }
                            }
                        }
                    }
                }

                //grdReffProvider.DataBind();


                grdReffProvider.Visible = true;
                txtReffProvSearch.Visible = true;
                btnSearchRP.Visible = true;

                //extdllReffProvider.Text = grdUser.Items[grdUser.SelectedIndex].Cells[9].Text;
            }
            else
            {
                lblRefferingProvider.Visible = false;
                //extdllReffProvider.Visible = false;
                grdReffProvider.Visible = false;
                txtReffProvSearch.Visible = false;
                btnSearchRP.Visible = false;
                //extdllReffProvider.Text = "NA";
            }
            if (RoleName == "doctor")
            {
                string UserId = Session["UserID"].ToString();
                LstBxDoctorList.Visible = true;
                lblDoctorlst.Visible = true;
                LstBxDoctorList.Items.Clear();
                _objDoctorBO = new Bill_Sys_DoctorBO();
                DataSet dset1 = new DataSet();
                dset1 = _objDoctorBO.GetDoctorList(txtCompanyID.Text);
                if (dset1.Tables[0].Rows.Count > 0)
                {
                    LstBxDoctorList.DataSource = dset1;
                    LstBxDoctorList.DataTextField = "DESCRIPTION";
                    LstBxDoctorList.DataValueField = "CODE";
                    LstBxDoctorList.DataBind();
                }

                GetDoctorUserID(UserId);
                lblvalidateshow.Visible = true;
                chkAllowandshow.Visible = true;
                Bill_Sys_BillingCompanyDetails_BO objval = new Bill_Sys_BillingCompanyDetails_BO();
                DataSet dsvalidate = new DataSet();
                dsvalidate = objval.GetValidateandallowfordoctor(txtCompanyID.Text, UserId);
                if (dsvalidate.Tables.Count > 0)
                {
                  
                    string szvalidateshow = dsvalidate.Tables[0].Rows[0]["BT_VALIDATE_AND_SHOW"].ToString();
                    if (szvalidateshow == "True")
                    {
                        chkAllowandshow.Checked = true;
                    }
                    else
                    {
                        chkAllowandshow.Checked = false;
                    }
                }

               


            }
            if (RoleName == "provider")
            {
                grvProvider.Visible = true;
                BindProviders();
                loadProviders();
            }
            else
            {
                grvProvider.Visible = false;
                lblProvider.Visible = false;
            }


            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
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
    protected void grdUser_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdUser.CurrentPageIndex = e.NewPageIndex;
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

    protected void grdReffProvider_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdReffProvider.CurrentPageIndex = e.NewPageIndex;
            BindProviderGrid();
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

    private string EncryptPassword()
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
            EncryptedPassword = Bill_Sys_EncryDecry.Encrypt(txtPassword.Text, strPassPhrase, strSaltValue, strHashAlgorithm, intPasswordIterations, strInitVector, intKeySize);
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

    private string DecryptPassword(string p_szPassword)
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
        string DecryptedPassword = "";
        try
        {
            DecryptedPassword = Bill_Sys_EncryDecry.Decrypt(p_szPassword, strPassPhrase, strSaltValue, strHashAlgorithm, intPasswordIterations, strInitVector, intKeySize);
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
       
        return DecryptedPassword;
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
            log.Debug("in BindGrid");
            log.Debug("xml  list. user.xml");
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "user.xml";
            _listOperation.LoadList();


            //For Binding Check Box
            for (int i = 0; i < grdUser.Items.Count; i++)
            {
                if (grdUser.Items[i].Cells[14].Text == "True")//change12-14
                {
                    CheckBox ch = (CheckBox)grdUser.Items[i].Cells[13].FindControl("chkDiagnosys");//change10-13
                    ch.Checked = true;
                }
            }
            //End

            log.Debug("after chkDiagnosys checked");
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

    private void BindProviderGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_BillingCompanyDetails_BO objval = new Bill_Sys_BillingCompanyDetails_BO();
            DataSet dsReffProviders = new DataSet();
            dsReffProviders=objval.getRefferingProviderList(txtCompanyID.Text);
            Session["Reff_Providers"] = dsReffProviders;
            grdReffProvider.DataSource = dsReffProviders;
            grdReffProvider.DataBind();
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
    protected void extddlUserRole_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblDoctorlst.Visible = false;
            lblReferringOffice.Visible = false;
            extddlOfficeList.Visible = false;
            extddlOfficeList.Text = "NA";
            chkAllowandshow.Visible = false;
            lblvalidateshow.Visible = false;
            grdReffProvider.Visible = false;
            grvProvider.Visible = false;
            lblProvider.Visible = false;
            txtReffProvSearch.Visible = false;
            btnSearchRP.Visible = false;
            lblRefferingProvider.Visible = false;

            LstBxDoctorList.Visible = false;
            string userrole = extddlUserRole.Selected_Text.ToLower();
            if (userrole == "doctor")
            {
                lblDoctorlst.Visible = true;
                LstBxDoctorList.Visible = true;
                chkAllowandshow.Visible = true;
                lblvalidateshow.Visible = true;

                extddlProvider.Visible = false;
                lblProvider.Visible = false;
                //extdllReffProvider.Visible = false;
                lblRefferingProvider.Visible = false;
                lblReferringOffice.Visible = false;
                extddlOfficeList.Visible = false;
            }
            else if (extddlUserRole.Text == "USR00003")
            {
                GetProviderList();

                lblDoctorlst.Visible = false;
                LstBxDoctorList.Visible = false;
                chkAllowandshow.Visible = false;
                lblvalidateshow.Visible = false;
                extddlProvider.Visible = false;
                lblProvider.Visible = false;
                //extdllReffProvider.Visible = false;
                lblRefferingProvider.Visible = false;
                lblReferringOffice.Visible = false;
                extddlOfficeList.Visible = false;
            }
            else if (extddlUserRole.Selected_Text.ToLower() == "referring office")
            {
                GetOfficeList();

                extddlProviderList.Visible = false;
                lblDoctorlst.Visible = false;
                LstBxDoctorList.Visible = false;
                chkAllowandshow.Visible = false;
                lblvalidateshow.Visible = false;
                extddlProvider.Visible = false;
                lblProvider.Visible = false;
                //extdllReffProvider.Visible = false;
                lblRefferingProvider.Visible = false;
                lblReferringOffice.Visible = true;
                extddlOfficeList.Visible = true;
            }
            else
            {
                extddlProviderList.Visible = false;
            }

            if (userrole == "provider")
            {
                grvProvider.Visible = true;
                BindProviders();
                ////extddlProvider.Visible = true;
                lblProvider.Visible = true;

                //extdllReffProvider.Visible = false;
                lblRefferingProvider.Visible = false;
                lblDoctorlst.Visible = false;
                LstBxDoctorList.Visible = false;
                chkAllowandshow.Visible = false;
                lblvalidateshow.Visible = false;
                lblReferringOffice.Visible = false;
                extddlOfficeList.Visible = false;
            }
            else if (userrole == "reffering provider")
            {
                //extdllReffProvider.Visible = true;
                lblRefferingProvider.Visible = true;
                grdReffProvider.Visible = true;
                txtReffProvSearch.Visible = true;
                btnSearchRP.Visible = true;

                extddlProvider.Visible = false;
                lblProvider.Visible = false;
                lblDoctorlst.Visible = false;
                LstBxDoctorList.Visible = false;
                chkAllowandshow.Visible = false;
                lblvalidateshow.Visible = false;
                lblReferringOffice.Visible = false;
                extddlOfficeList.Visible = false;
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

    private void GetOfficeList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblReferringOffice.Visible = true;
            extddlOfficeList.Visible = true;
            extddlOfficeList.Procedure_Name = "SP_MST_OFFICE";
            extddlOfficeList.Connection_Key = "Connection_String";
            extddlOfficeList.Selected_Text = "---Select---";
            extddlOfficeList.Flag_Key_Value = "OFFICE_LIST";
            extddlOfficeList.Flag_ID = txtCompanyID.Text;
            extddlOfficeList.DataBind();
            btnSave.Attributes.Add("onclick", "return formValidator('frmUserMaster','txtUserName,extddlUserRole,txtPassword,txtConfirmPassword,extddlProviderList,txtUserEmail,extddlOfficeList');");
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

    private void GetProviderList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            extddlProviderList.Visible = true;
            extddlProviderList.Procedure_Name = "SP_MST_PROVIDER";
            extddlProviderList.Connection_Key = "Connection_String";
            extddlProviderList.Selected_Text = "---Select---";
            extddlProviderList.Flag_Key_Value = "PROVIDER_LIST";
            extddlProviderList.Flag_ID = txtCompanyID.Text;
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
        String szListOfUsers = "";
        try
        {
            for (int i = 0; i < grdUser.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdUser.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    string RoleName = grdUser.Items[i].Cells[4].Text.ToLower();
                    if (RoleName.ToLower() == "reffering provider")
                    {
                        Bill_Sys_BillingCompanyDetails_BO objval = new Bill_Sys_BillingCompanyDetails_BO();
                        string sz_user_id = grdUser.Items[i].Cells[1].Text.ToLower();
                        objval.DeleteUserReffProvider(txtCompanyID.Text, sz_user_id);
                        //txtCompanyID.Text
                    }
                    if (!_deleteOpeation.deleteRecord("SP_MST_USERS", "@SZ_USER_ID", grdUser.Items[i].Cells[1].Text))
                    {
                        if (szListOfUsers == "")
                        {
                            szListOfUsers = grdUser.Items[i].Cells[2].Text;
                        }
                        else
                        {
                            szListOfUsers = szListOfUsers + " , " + grdUser.Items[i].Cells[2].Text;
                        }
                    }
                    if (RoleName == "doctor")
                    {
                        _objDoctorBO = new Bill_Sys_DoctorBO();
                        string UserID = grdUser.Items[i].Cells[1].Text.ToLower();
                        _objDoctorBO.DelDocUserID(txtCompanyID.Text.Trim(), UserID);
                    }
                    if (RoleName.ToLower() == "provider") //  Added By Kapil, Delete data from table 'TXN_USER_PROVIDER'
                    {
                        //Bill_Sys_DeleteBO _obj_delete_provider = new Bill_Sys_DeleteBO();
                        string sz_user_id = grdUser.Items[i].Cells[1].Text.ToLower();
                        obj_provider = new ProviderServices();
                        obj_provider.DeleteUserProvider(sz_user_id);
                        //_obj_delete_provider.DeleteUserProvider(sz_user_id);
                    }
                   
                }
            }
            if (szListOfUsers != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for User " + szListOfUsers + "  exists.'); ", true);
            }
            else
            {
                LstBxDoctorList.Visible = false;
                lblDoctorlst.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = "User deleted successfully ...";
            }
            ClearControl();
            BindGrid();
           
            //For Cache Refresh
            try
            {
                BlazeFast.server.SrvProcedures procs = new BlazeFast.server.SrvProcedures();
                procs.Refresh();
            }
            catch (Exception io)
            {
                log.Debug(" BlazeFast.server.SrvProcedures.Failed");
            }
            //For Cache Refresh
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

    public void GetDoctorUserID(string UsrId)
    {
        DataSet dset = new DataSet();
        string sz_CompanyId = txtCompanyID.Text.Trim();
        _objDoctorBO = new Bill_Sys_DoctorBO();
        dset = _objDoctorBO.GetDoctorID(sz_CompanyId, UsrId);
     
       
       
        //foreach (ListItem li in LstBxDoctorList.Items)
        //{
        //    string docId = li.Value;
        //    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
        //    {
        //        string dsetvalue = dset.Tables[0].Rows[i][0].ToString();
        //        if (docId == dsetvalue)
        //        {
        //            li.Selected = true;
        //            break;
        //        }
        //        else
        //        {
        //            li.Selected = false;
        //        }
        //    }
        //}
        for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
        {
            string dsetvalue = dset.Tables[0].Rows[i][0].ToString();
            foreach (ListItem li in LstBxDoctorList.Items)
            {
                string docId = li.Value;
                if (docId == dsetvalue)
                {
                    li.Selected = true;
                    break;
                }
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        lblMsg.Visible = false;
        _saveOperation = new SaveOperation();
        string latest_userID;
        txtIS_PROVIDER.Text = "0";
        try
        {
            if (Page.IsValid)
            {
                RegularExpressionValidator obj = new RegularExpressionValidator();
                string strexpression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

                Match validEmail = Regex.Match(txtUserEmail.Text, strexpression);
                if (validEmail.Success)
                {
                    string sz_is_provider = "";
                    if (extddlOfficeList.Text != "NA" && extddlOfficeList.Visible)
                    {
                        txtReffOffID.Text = extddlOfficeList.Text;
                    }
                    txtPassword.Text = EncryptPassword();
                    _saveOperation.WebPage = this.Page;
                    _saveOperation.Xml_File = "user.xml";

                    Int32 output;
                    Bill_Sys_BillingCompanyDetails_BO objval = new Bill_Sys_BillingCompanyDetails_BO();
                   

                    ArrayList arr = new ArrayList();
                    ArrayList arr_provider = new ArrayList();
                    ArrayList arr_reff_provider = new ArrayList();
                    arr.Insert(0, txtUserName.Text.ToLower());
                    arr.Insert(1, extddlUserRole.Text);
                    arr.Insert(2, txtPassword.Text);
                    arr.Insert(3, txtUserEmail.Text.ToLower());
                    arr.Insert(4, txtCompanyID.Text);

                    if (extddlOfficeList.Text != "NA")
                    {
                        arr.Insert(5, extddlOfficeList.Text);
                    }
                    else
                    {
                        arr.Insert(5, "NULL");
                    }

                    if (extddlUserRole.Selected_Text == "Provider")
                    {
                        txtIS_PROVIDER.Text = "1";
                        arr.Insert(6, txtIS_PROVIDER.Text);


                    }
                    else
                    {
                        arr.Insert(6, txtIS_PROVIDER.Text);
                    }
                    string szallowandshoe = "";
                    if (chkAllowandshow.Checked)
                    {
                        szallowandshoe = "1";
                        arr.Insert(7, szallowandshoe);
                    }
                    else
                    {
                        szallowandshoe = "0";
                        arr.Insert(7, szallowandshoe);

                    }
                    arr.Insert(8, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                    // Function for Inserting User Details...

                    output = objval.InsertUserMasterDetails(arr);
                    chkAllowandshow.Checked = false;
                    lblvalidateshow.Visible = false;
                     //function ends...

                    _objDoctorBO = new Bill_Sys_DoctorBO();
                    latest_userID = _objDoctorBO.GetLatestUserID(txtCompanyID.Text);
                    //ashutosh...
                    txtCurUserID.Text = latest_userID;

                    
                    //string[] strUserId = latest_userID.Split('0');
                    //string UserId =  strUserId[1].ToString();
                    //int i = (Convert.ToInt32(UserId) + 1);
                    //txtCurUserID.Text =strUserId[0].ToString()+ i.ToString();
                   
                    _objDoctorBO.DelDocUserID(txtCompanyID.Text, latest_userID);
                    foreach (ListItem li in LstBxDoctorList.Items)
                    {
                        if (li.Selected == true)
                        {
                            txtDoctorID.Text = li.Value;
                            _objDoctorBO.InsertDoctorUserDetails(txtCompanyID.Text.Trim(), txtCurUserID.Text.Trim(), txtDoctorID.Text.Trim());
                        }
                    }

                    foreach (ListItem li in LstBxDoctorList.Items)
                    {
                        if (li.Selected == true)
                        {
                            li.Selected = false;
                        }
                    }

                    LstBxDoctorList.Visible = false;
                    lblDoctorlst.Visible = false;
                 
                    // output = objval.InsertUserMasterDetails(txtUserName.Text, extddlUserRole.Text, txtPassword.Text, txtUserEmail.Text, txtCompanyID.Text);

                    //function end..

                    if (output == 1)
                    {
                        if (extddlUserRole.Selected_Text == "Provider")// Added By Kapil, Insert data into new table 'TXN_USER_PROVIDER'
                        {
                            string ProviderIds = "";
                            string ProviderNames = "";
                            for (int i = 0; i < grvProvider.VisibleRowCount; i++)
                            {
                                GridViewDataColumn c = (GridViewDataColumn)grvProvider.Columns[0];
                                CheckBox chk = (CheckBox)grvProvider.FindRowCellTemplateControl(i, c, "chkall1");
                                if (chk != null)
                                {
                                    if (chk.Checked)
                                    {
                                        ProviderIds = grvProvider.GetRowValues(i, "CODE").ToString();
                                        ProviderNames = grvProvider.GetRowValues(i, "DESCRIPTION").ToString();   
                                        //else
                                        //{
                                        //    ProviderIds += "," + grvProvider.GetRowValues(i, "CODE").ToString();
                                        //    ProviderNames += "," + grvProvider.GetRowValues(i, "DESCRIPTION").ToString();
                                        //}
                                        arr_provider.Insert(0, txtCompanyID.Text);
                                        arr_provider.Insert(1, txtCurUserID.Text);
                                        //arr_provider.Insert(2, extddlProvider.Text);
                                        //arr_provider.Insert(3, extddlProvider.Selected_Text);

                                        arr_provider.Insert(2, ProviderIds);
                                        arr_provider.Insert(3, ProviderNames);

                                        arr_provider.Insert(4, DateTime.Now);
                                        // Create new class library for this change
                                        obj_provider = new ProviderServices();
                                        obj_provider.InsertUserProviderDetails(arr_provider);
                                    }
                                }
                            }
                            
                        }
                        if (extddlUserRole.Selected_Text.ToLower() == "reffering provider")
                        {
                            bool selected=false;
                            for (int i = 0; i < grdReffProvider.Items.Count; i++)
                            {
                                string str = "";
                                CheckBox box = (CheckBox)this.grdReffProvider.Items[i].FindControl("chkSelete");
                                if (box.Checked)
                                {
                                    selected = true;
                                    arr_reff_provider.Add(grdReffProvider.Items[i].Cells[2].Text);
                                }
                            }
                            if (selected)
                            {
                                string userId = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                                objval.InsertUserReffProvider(arr_reff_provider, txtCurUserID.Text, txtCompanyID.Text, userId,"ADD");
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Please select reffering provoder.";
                                return;
                            }
                        }
                        string sz_user_name = txtUserName.Text.ToLower();
                        string sz_user_mail_id = txtUserEmail.Text.ToLower();
                        int email = EmailSend();
                        BindGrid();
                      
                        lblMsg.Visible = true;
                        lblMsg.Text = " User Information Saved successfully ";
                    }

                    else if (output == -1)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "User already exists! Please choose another user";
                    }
                    //function for sending email...
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Invalid Email Address";
                }
            }
            else
            {
                lblMsg.Visible = false;
                lblMsg.Text = "";
            }
            //For Cache Refresh
            try
            {
                BlazeFast.server.SrvUserManagement users = new BlazeFast.server.SrvUserManagement();
                users.Refresh();
                //BlazeFast.server.SrvProcedures procs = new BlazeFast.server.SrvProcedures();
                //procs.Refresh();
            }
            catch (Exception ex)
            {
                log.Debug(" BlazeFast.server.SrvProcedures.Failed");
            }
            //For Cache Refresh
            //ClearControl();
            
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
       
        ClearControl();
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
        string szallowandshoe = "";
        if (txtUserName.Text == "" && txtPassword.Text == "" && txtConfirmPassword.Text == "" && txtUserEmail.Text == "" && extddlUserRole.Text == "NA")
        {
            UpdateNoDiagnosysColomn();
        }
        else
        {
            txtUserName.Enabled = false;
            lblMsg.Visible = false;
            _editOperation = new EditOperation();


            int updateResult;
            try
            {
                if (Page.IsValid)
                {
                    RegularExpressionValidator obj = new RegularExpressionValidator();
                    string strexpression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

                    Match validEmail = Regex.Match(txtUserEmail.Text, strexpression);
                    if (validEmail.Success)
                    {
                        if (extddlOfficeList.Text != "NA" && extddlOfficeList.Visible)
                        {
                            txtReffOffID.Text = extddlOfficeList.Text;
                        }

                        string str = extddlProviderList.Text;
                        txtPassword.Text = EncryptPassword();
                        _editOperation.WebPage = this.Page;
                        _editOperation.Xml_File = "user.xml";
                        _editOperation.Primary_Value = Session["UserID"].ToString();
                        //_editOperation.UpdateMethod();

                        string UserRole = extddlUserRole.Selected_Text.ToLower();

                        if (UserRole == "doctor")
                        {
                            string UserID = Session["UserID"].ToString();
                            _objDoctorBO = new Bill_Sys_DoctorBO();
                            _objDoctorBO.DelDocUserID(txtCompanyID.Text.Trim(), UserID);
                            foreach (ListItem li in LstBxDoctorList.Items)
                            {
                                if (li.Selected == true)
                                {
                                    txtDoctorID.Text = li.Value;
                                    _objDoctorBO.InsertDoctorUserDetails(txtCompanyID.Text.Trim(), UserID, txtDoctorID.Text.Trim());
                                }
                            }

                           
                        }

                        Bill_Sys_BillingCompanyDetails_BO objUpdate = new Bill_Sys_BillingCompanyDetails_BO();
                        ArrayList updatearr = new ArrayList();
                        ArrayList arr_reff_provider = new ArrayList();

                        updatearr.Insert(0, txtUserName.Text);
                        updatearr.Insert(1, extddlUserRole.Text);
                        updatearr.Insert(2, txtPassword.Text);
                        updatearr.Insert(3, txtUserEmail.Text);
                        updatearr.Insert(4, txtCompanyID.Text);
                        updatearr.Insert(5, _editOperation.Primary_Value);
                        if (extddlOfficeList.Text != "NA" && extddlOfficeList.Visible)
                        {
                           
                            updatearr.Insert(6, extddlOfficeList.Text);
                        }
                        else
                        {
                            updatearr.Insert(6, "");
                        }
                       
                        if (chkAllowandshow.Checked)
                        {
                            szallowandshoe = "1";
                            updatearr.Insert(7, szallowandshoe);
                        }
                        else
                        {
                            szallowandshoe = "0";
                            updatearr.Insert(7, szallowandshoe);

                        }
                      
                        updateResult = objUpdate.UpdateUserMasterDetails(updatearr);

                        if (updateResult == 1)
                        {
                            lblMsg.Visible = true;
                            //LstBxDoctorList.Visible = false;
                            //lblDoctorlst.Visible = false;
                            UpdateNoDiagnosysColomn();
                            BindGrid();
                    
                            if (extddlUserRole.Selected_Text.ToLower() == "provider")
                            {
                                obj_provider = new ProviderServices();
                                obj_provider.DeleteUserProvider(_editOperation.Primary_Value);
                                string ProviderIds = "";
                                string ProviderNames = "";
                                ArrayList arr_provider = new ArrayList();
                                for (int i = 0; i < grvProvider.VisibleRowCount; i++)
                                {
                                    GridViewDataColumn c = (GridViewDataColumn)grvProvider.Columns[0];
                                    CheckBox chk = (CheckBox)grvProvider.FindRowCellTemplateControl(i, c, "chkall1");
                                    if (chk != null)
                                    {
                                        if (chk.Checked)
                                        {
                                            ProviderIds = grvProvider.GetRowValues(i, "CODE").ToString();
                                            ProviderNames = grvProvider.GetRowValues(i, "DESCRIPTION").ToString();
                                            arr_provider.Insert(0, txtCompanyID.Text);
                                            arr_provider.Insert(1, _editOperation.Primary_Value);
                                            
                                            arr_provider.Insert(2, ProviderIds);
                                            arr_provider.Insert(3, ProviderNames);

                                            arr_provider.Insert(4, DateTime.Now);
                                            obj_provider = new ProviderServices();
                                            obj_provider.InsertUserProviderDetails(arr_provider);
                                        }
                                    }
                                }

                            }
                            if (extddlUserRole.Selected_Text.ToLower() == "reffering provider")
                            {
                                bool selected = false;
                                for (int i = 0; i < grdReffProvider.Items.Count; i++)
                                {
                                    CheckBox box = (CheckBox)this.grdReffProvider.Items[i].FindControl("chkSelete");
                                    if (box.Checked)
                                    {
                                        selected = true;
                                        arr_reff_provider.Add(grdReffProvider.Items[i].Cells[2].Text);
                                    }
                                }
                                if (selected)
                                {
                                    Bill_Sys_BillingCompanyDetails_BO objval = new Bill_Sys_BillingCompanyDetails_BO();
                                    string userId = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                                    string selectedUserID = Session["UserID"].ToString();
                                    objval.InsertUserReffProvider(arr_reff_provider, selectedUserID, txtCompanyID.Text, userId, "UPDATE");
                                }
                                else
                                {
                                    lblMsg.Visible = true;
                                    lblMsg.Text = "Please select reffering provoder.";
                                    return;
                                }
                            }
                            lblMsg.Text = " User Information Updated successfully ! ";
                            RegularExpressionValidator1.Enabled = true;
                            ClearControl();
                        }
                        else if (updateResult == -1 || updateResult == 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "User already exists! Please choose another user";
                        }
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Invalid Email Address";
                    }
                }
                else
                {
                    lblMsg.Visible = false;
                    lblMsg.Text = "";
                }
           }
            catch (Exception ex)
            {
                this.usrMessage.PutMessage(ex.ToString());
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
                RegularExpressionValidator1.Enabled = false;
            }
            //For Cache Refresh
            try
            {
                BlazeFast.server.SrvProcedures procs = new BlazeFast.server.SrvProcedures();
                procs.Refresh();
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //using (Utils utility = new Utils())
                //{
                //    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                //}
                //string str2 = "Error Request=" + id + ".Please share with Technical support.";
                //base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }
            //Method End
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            //For Cache Refresh
        }
    }

    //public int EmaiSend()
    //{
    //    string pwd;
    //    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //    Service_Email se = new Service_Email();
    //    EmailSender.DAO_XEmailEntity d = new EmailSender.DAO_XEmailEntity();

    //    d.MessageKey = "userMst";
    //    d.ToEmail = txtUserEmail.Text;
    //    d.AccountPassword = txtUserName.Text;
    //    pwd = txtPassword.Text;
    //    txtPassword.Text = DecryptPassword(pwd);
    //    d.CC = txtPassword.Text;
    //    d.Subject = extddlUserRole.Selected_Text;
    //    d.Body = "hi";
    //    EmailSender.DAO_Message objAcknowleMessage = new EmailSender.DAO_Message();
    //    objAcknowleMessage = se.SendMail(d);
    //    if (objAcknowleMessage != null)
    //    {
    //        // Label1.Visible = true;
    //        //Label1.Text = "Message Sent";
    //        return 1;
    //    }
    //    else
    //    {
    //        // Label1.Visible = true;
    //        //Label1.Text = "problem while sending request";
    //        return 0;
    //    }

    //}

   

    public int EmailSend()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //SmtpClient smtpClient = new SmtpClient();
        //MailMessage message = new MailMessage();

        try
        {
            string pwd;
            pwd = txtPassword.Text;
            txtPassword.Text = DecryptPassword(pwd);
            string sz_message="Your Login ID : "+ txtUserName.Text+" & Password: "+ txtPassword.Text ;
           //sz_message=sz_message + txtPassword.Text;

            System.Net.Mail.MailMessage MyMailMessage = new System.Net.Mail.MailMessage("green.your.bills.support@procomsys.in", txtUserEmail.Text,
            "Welcome to Green Your Bills ", sz_message);

            MyMailMessage.IsBodyHtml = false;
            MyMailMessage.Body = "You are succesfully created new login/user for Green Your Bills  " +sz_message;
            //Proper Authentication Details need to be passed when sending email from gmail
            System.Net.NetworkCredential mailAuthentication = new
            System.Net.NetworkCredential("green.your.bills.support@procomsys.in", "gybsupport!@#");

            //Smtp Mail server of Gmail is "smpt.gmail.com" and it uses port no. 587
            //For different server like yahoo this details changes and you can
            //get it from respective server.
            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);

            //Enable SSL
            mailClient.EnableSsl = true;

            mailClient.UseDefaultCredentials = false;

            mailClient.Credentials = mailAuthentication;

            mailClient.Send(MyMailMessage);

            //string sz_from_mail = "atul.j@procomsys.in";
            //MailAddress fromAddress = new MailAddress(sz_from_mail);

            //// You can specify the host name or ipaddress of your server
            //// Default in IIS will be localhost 
            //smtpClient.Host = "localhost";

            ////Default port will be 25
            //smtpClient.Port = 587;

            ////From address will be given as a MailAddress Object
            //message.From = fromAddress;

            //// To address collection of MailAddress
            //message.To.Add(txtUserEmail.Text);
            //message.Subject = "Feedback";

            //// CC and BCC optional
            //// MailAddressCollection class is used to send the email to various users
            //// You can specify Address as new MailAddress("admin1@yoursite.com")
            //// message.CC.Add("");
            //// message.CC.Add("");

            //// You can specify Address directly as string
            ////message.Bcc.Add(new MailAddress(""));
            ////message.Bcc.Add(new MailAddress(""));

            ////Body can be Html or text format
            ////Specify true if it  is html message
            //message.IsBodyHtml = false;

            //// Message body content
            //message.Body = "Hi";

            //// Send SMTP mail
            //smtpClient.Send(message);

          
            lblMsg.Text = "Email successfully sent.";
            return 1;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Send Email Failed.<br>" + ex.Message;
            return 0;
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


    protected void UpdateNoDiagnosysColomn()
    {
        Bill_Sys_BillingCompanyDetails_BO objUpdate = new Bill_Sys_BillingCompanyDetails_BO();

        //Update Weather To Pass User To Diagnosys Page Or Not
        for (int i = 0; i < grdUser.Items.Count; i++)
        {
            CheckBox ch = (CheckBox)grdUser.Items[i].Cells[13].FindControl("chkDiagnosys");//change10-13
            objUpdate.UpdateDiagnosysPage(grdUser.Items[i].Cells[1].Text.ToString(), ch.Checked.ToString());
        }
        //End
        lblMsg.Visible = true;
        lblMsg.Text = "Records Updated Successfully...";
    }

    protected void extddlProvider_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
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

    //protected void extdllReffProvider_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //}

    protected void btnSearchRP_Click(object sender, EventArgs e)
    {
        //CODE
        //DESCRIPTION
        DataTable dtRP = new DataTable();
        dtRP.Columns.Add("CODE");
        dtRP.Columns.Add("DESCRIPTION");
        DataSet dsRP = new DataSet();
        dsRP = (DataSet)Session["Reff_Providers"];
        if (dsRP != null)
        {
            if (dsRP.Tables.Count > 0 && dsRP.Tables[0].Rows.Count > 0)
            {
                DataRow[] dr = dsRP.Tables[0].Select("DESCRIPTION  like '%" + txtReffProvSearch.Text+ "%'");
                if (dr.Length > 0)
                {
                    for (int i = 0; i < dr.Length ; i++)
                    {
                        DataRow drRp = dtRP.NewRow();
                        drRp["CODE"] = dr[i]["CODE"].ToString();
                        drRp["DESCRIPTION"] = dr[i]["DESCRIPTION"].ToString();
                        dtRP.Rows.Add(drRp);
                    }
                }
            }
        }
        grdReffProvider.DataSource = dtRP;
        grdReffProvider.DataBind();
    }

    //private void BindProviders()
    //{
    //    Bill_Sys_BillingCompanyDetails_BO objval = new Bill_Sys_BillingCompanyDetails_BO();
    //    DataSet dsProviders = new DataSet();
    //    dsProviders = objval.BindProviders(txtCompanyID.Text);
    //    grvProvider.DataSource = dsProviders;
    //    grvProvider.DataBind();
    //}

    private void loadProviders()
    {
        Bill_Sys_BillingCompanyDetails_BO objval = new Bill_Sys_BillingCompanyDetails_BO();
        DataSet dsProviders = new DataSet();
        dsProviders = objval.getProviders(txtCompanyID.Text, Session["UserID"].ToString());
        if (dsProviders != null)
        {
            if (dsProviders.Tables.Count > 0 && dsProviders.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < grvProvider.VisibleRowCount; i++)
                {
                    GridViewDataColumn c = (GridViewDataColumn)grvProvider.Columns[0];
                    CheckBox chk = (CheckBox)grvProvider.FindRowCellTemplateControl(i, c, "chkall1");

                    for (int j = 0; j < dsProviders.Tables[0].Rows.Count; j++)
                    {
                        if (dsProviders.Tables[0].Rows[j]["SZ_USER_PROVIDER_NAME_ID"].ToString() == grvProvider.GetRowValues(i, "CODE").ToString())
                        {
                            if (chk != null)
                            {
                                chk.Checked = true;
                            }
                        }
                    }
                }
            }
        }
    }
    protected void extddlProviderList_OnextendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {}
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

    private void BindProviders()
    {
        Bill_Sys_BillingCompanyDetails_BO objval = new Bill_Sys_BillingCompanyDetails_BO();
        DataSet dsProviders = new DataSet();
        dsProviders = objval.BindProviders(txtCompanyID.Text);
        grvProvider.DataSource = dsProviders;
        grvProvider.DataBind();
    }

    public void BindSearch()
    {
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
      
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            
            SqlCommand comm = new SqlCommand("proc_user_serach", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILLING_COMPANY", txtCompanyID.Text);
            comm.Parameters.AddWithValue("@sz_user_role_id", extddlUserRole.Text);
            comm.Parameters.AddWithValue("@sz_user_name", txtUserName.Text);
            comm.Parameters.AddWithValue("@IS_ACTIVE", rdoIsActive.Text);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
            grdUser.DataSource = ds;
            grdUser.DataBind();

           

        }
        catch(Exception ex)
        {

        }
    }

    private void UserUpdateByActiveDeactive(string userid,string disabled)
    {
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();

            SqlCommand comm = new SqlCommand("PROC_USER_ACTIVE_DEACTIVE", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILLING_COMPANY", txtCompanyID.Text);
            comm.Parameters.AddWithValue("@SZ_USER_ID", userid);
            comm.Parameters.AddWithValue("@bt_is_disabled", disabled);
            comm.ExecuteNonQuery();
            BindSearch();
            }
        catch(Exception ex)
        {


        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
   
    protected void Page_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < grdUser.Items.Count; i++)
        {
            if (grdUser.Items[i].Cells[16].Text.ToLower() == "false")
            {

                LinkButton lnk = (LinkButton)grdUser.Items[i].FindControl("lnkActive");
                lnk.Visible = false;
                LinkButton lnk1 = (LinkButton)grdUser.Items[i].FindControl("lnkDeactive");
                lnk1.Visible = true;
            }
            else
            {
                LinkButton lnk = (LinkButton)grdUser.Items[i].FindControl("lnkActive");
                lnk.Visible = true;
                LinkButton lnk1 = (LinkButton)grdUser.Items[i].FindControl("lnkDeactive");
                lnk1.Visible = false;
            }

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindSearch();
    }

   

    protected void grdUser_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if(e.CommandName.ToString()=="Active")
        {
            string userID = e.Item.Cells[1].Text.ToString();
            UserUpdateByActiveDeactive(userID, "0");
        }
        if(e.CommandName.ToString()=="DeActive")
        {
            string userID = e.Item.Cells[1].Text.ToString();
            UserUpdateByActiveDeactive(userID, "1");
        }
    }
}
