/* '---------------------------------------------------------------------------------------------------------------------------------------------------------------
' Name of Form		    :	Home.aspx.cs
' Purpose				:	This form is used to Change Password
' Developed By			:	Adarsh Bajpai
' Start Date			: 	26 November 2007
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' Change ID	    Call No.	Change Date	    Developer's Name	        Purpose of Change
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
'---------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Web.Security;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using ResourceHandler = Common.ResourceHandler; 
using StoredProceduresDefination = Common.StoredProceduresDefination; 
using BusinessUserLogin = Security.Business.BusinesUser;
using SystemConfiguration = Common.SystemConfiguration; 
using CommonFunctions = Common.CommonFunctions; 
using Common.Interface; 
using GeneralTools;
//using LGT.BusinessLogicLayer;

namespace Security
{
	public partial class Home : PageBase
	{

		bool blnRememberUser=false;

	#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {

                txtUserID.Attributes.Add("ontextchange", "return getCookie();");
                txtPassword.Attributes.Add("ontextchange", "return changeStatus();");


                if (!(Page.IsPostBack))
                {
                    Session.Clear();
                    Session.Abandon();
                    ViewState.Add("NoofTries", 0);
                    ViewState["Email"] = "NA";

                    Session["HelpFile"] = "../Help/CommonHelp.htm";

                    if (Request.Cookies["myCookie"] != null)
                    {

                        HttpCookie cookie = Request.Cookies.Get("myCookie");

                        txtUserID.Text = cookie.Values["LastMail"].ToString();

                        if (cookie.Values["LastMailIP"].ToString() != "")
                        {
                            txtPassword.Text = cookie.Values["LastMailIP"].ToString();
                            hflIsCookie.Value = "Y";
                        }
                        else
                        {
                            hflIsCookie.Value = "N";
                        }

                        if (txtUserID.Text != "" && txtPassword.Text != "")
                        {
                            chkRememberUser.Checked = true;
                            blnRememberUser = true;
                        }
                    }

                    lnkHelp.Attributes.Add("onclick", "return MyHelp();");
                    string strShowHelp = Common.SystemConfiguration.GetApplicationParameters("ShowHelp");
                    if (strShowHelp == "Yes")
                    {
                        lnkHelp.Visible = true;
                    }
                    else
                    {
                        lnkHelp.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralTools.ExceptionLogger.ExceptionLog(ex);
            }
		}
		# endregion

	#region Login Button Click

        protected void btnLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            int intNoofTries;
            
            BusinessUserLogin busUserLogin = new BusinessUserLogin();
            try
            {
                Session["HelpFile"] = "../Help/CommonHelp.htm";
                if (txtUserID.Text != ViewState["Email"].ToString())
                {
                    ViewState["NoofTries"] = 0;
                }

                ViewState["Email"]= txtUserID.Text;
                intNoofTries = ((int)(ViewState["NoofTries"]));
                intNoofTries = intNoofTries + 1;
                busUserLogin.UserName = txtUserID.Text;

                if (hflIsCookie.Value== "N")
                {
                    busUserLogin.Password = GeneralTools.DataEncrypt_Decrypt.EncryptString(txtPassword.Text, true);
                }
                else
                {
                    busUserLogin.Password = txtPassword.Text;
                }

                busUserLogin.NoofTries = intNoofTries;
                if (chkRememberUser.Checked == true)
                {
                    blnRememberUser = true;
                }
                else
                {
                    blnRememberUser = false;
                }

                HttpCookie cookie = Request.Cookies.Get("myCookie");


                //if (Page.IsValid)
                //{
                //    if (ITUser.Authenticate("slaxman", "keeda2005"))
                //    {
                //        Session["UserName"] = "slaxman";
                //        Session["UserType"] = "P";
                //        Session["Loggedin"] = "Yes";
                //    }
                //}


                busUserLogin.DoLogin(blnRememberUser, cookie);
                ViewState["NoofTries"] = busUserLogin.NoofTries;
            }
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    GeneralTools.ExceptionLogger.ExceptionLog(ex);
                    Response.Redirect("../ErrorPages/ErrorPage.aspx");
                }
            }
            finally
            {
            }
        }
		#endregion

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnLogin.Click += new System.Web.UI.ImageClickEventHandler(this.btnLogin_Click);

		}
		#endregion

    #region chkRememberUser_CheckedChanged

        protected void chkRememberUser_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkRememberUser.Checked==true)
			{
				blnRememberUser=true;
			}
			else
			{
				blnRememberUser=false;
			}

        }
        #endregion

    #region txtPassword_PreRender
        protected void txtPassword_PreRender(object sender, EventArgs e)
        {
            txtPassword.Attributes["value"] = txtPassword.Text;

        }
        #endregion

    }
}
