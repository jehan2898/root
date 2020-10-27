/*---------------------------------------------------------------------------------------------------------------------------------------------------------------
' NAME OF FORM		    :	FRMFORGOTPASSWORD.ASPX.CS
' PURPOSE				:	THIS FORM IS USED TO RETRIEVE PASSWORD
' DEVELOPED BY			:	KAMAL GARG
' START DATE			: 	12 JAN 2007
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' CHANGE ID	    CALL NO.	CHANGE DATE	    DEVELOPER'S NAME	        PURPOSE OF CHANGE
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' 
'---------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        # region Using
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ResourceHandler = Common.ResourceHandler; 
using StoredProceduresDefination = Common.StoredProceduresDefination; 
using BusinessForgotPassword = Security.Business.BusinessForgotPassword; 
using SystemConfiguration = Common.SystemConfiguration; 
using CommonFunctions = Common.CommonFunctions; 
using System.Configuration; 
using Common.Interface; 
using GeneralTools;

# endregion

namespace Security
{
	
	public partial class frmForgotPassword : PageBase
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
       
        #region Page Load
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["HelpFile"] = "../Help/ForgetPassword.htm";
                txtLoginID.Focus();
                Session["Msg"] = null;
            }
            //btnSubmit.Focus();
            Form1.Attributes.Add("onkeypress", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSubmit.UniqueID + "').click();return false;}} else {return true}; ");
			                       
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
			
		}
		#endregion
        
        #region Submit Button 
              
        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                BusinessForgotPassword objBusinessForgotPassword = new BusinessForgotPassword();
                try
                {
                    objBusinessForgotPassword.LoginID = txtLoginID.Text.Trim();
                    objBusinessForgotPassword.ForgotPassword();
                    if (objBusinessForgotPassword.Message == "")
                    {
                        //Session["Msg"] = "Password Has Been Successfully Send To Your Mail-ID";
                        Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER8");
                        Response.Redirect("../ErrorPages/frmOperationSuccess.aspx");
                    }
                    else
                    {
                        GeneralTools.MessageBox.ShowMessage(objBusinessForgotPassword.Message);

                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != "Thread was being aborted.")
                    {
                        GeneralTools.ExceptionLogger.ExceptionLog(ex);
                        //Session["Msg"] = "An error has occured during password updation";
                        Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER9");
                        Response.Redirect("../ErrorPages/ErrorPage.aspx");
                    }
                }
            }

        }
        #endregion

    }
}
