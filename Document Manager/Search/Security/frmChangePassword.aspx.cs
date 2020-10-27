/* '---------------------------------------------------------------------------------------------------------------------------------------------------------------
' Name of Form		    :	frmChangePassword.aspx.cs
' Purpose				:	This form is used to Change Password
' Developed By			:	Kamal Garg
' Start Date			: 	08 Jan 2007
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' Change ID	    Call No.	Change Date	    Developer's Name	        Purpose of Change
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
using BusinessChangePassword = Security.Business.BusinessChangePassword; 
using SystemConfiguration = Common.SystemConfiguration; 
using CommonFunctions = Common.CommonFunctions;
using System.Configuration;
using Common.Interface; 
using GeneralTools;
using System.Resources;
using System.Reflection;
# endregion

namespace Security
{
	
	public partial class frmChangePassword : PageBase
	{
		protected System.Web.UI.WebControls.Button btnReset;

		#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Session["Msg"]=null;
            GeneralTools.ValidateUser.ValidateUserSession("Hurix");
            txtOldPassword.Focus();
            if (Request.QueryString["CheckLogin"] == "False")
            {
                rowMessage.Visible = true;
            }
            else
            {
                rowMessage.Visible = false;
            }
            Session["HelpFile"] = "../Help/ChangePassword.htm";
            Form1.Attributes.Add("onkeypress", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSumbit.UniqueID + "').click();return false;}} else {return true}; ");
			    
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
		
		
		private void InitializeComponent()
		{    

		}
		#endregion

		#region Submit Button
		protected void btnSumbit_Click(object sender, System.EventArgs e) 
		{
            Page.Validate();
            if (Page.IsValid)
            {
			rfvConfPass.Enabled=true;
			if(txtCfmPassword.Text.Trim().Length==0)
			{
				GeneralTools.MessageBox.ShowMessage(Common.CommonFunctions.GetResourceValue("en-US", "ER78"));
				return;
				 
			}
			
			
				BusinessChangePassword objBusinessChangePassword = new BusinessChangePassword(); 
				try 
				{ 
					//objBusinessChangePassword.OldPassword = EncryptionDecption.DataEncryption_Decryption.EncryptString(txtOldPassword.Text.Trim(),true); 
					//objBusinessChangePassword.NewPassword = EncryptionDecption.DataEncryption_Decryption.EncryptString(txtNewPassword.Text.Trim(),true); 
					//objBusinessChangePassword.CfmPassword = EncryptionDecption.DataEncryption_Decryption.EncryptString(txtCfmPassword.Text.Trim(),true); 
                    objBusinessChangePassword.OldPassword = DataEncrypt_Decrypt.EncryptString(txtOldPassword.Text.Trim(),true);
                    objBusinessChangePassword.NewPassword = DataEncrypt_Decrypt.EncryptString(txtNewPassword.Text.Trim(), true);
                    objBusinessChangePassword.CfmPassword = DataEncrypt_Decrypt.EncryptString(txtCfmPassword.Text.Trim(), true); 
					objBusinessChangePassword.ChangePassword(); 
					if (objBusinessChangePassword.Message == "") 
					{
                        //Session["Msg"] = "Password Changed Successfully";
                        Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER7");
                        Response.Redirect("../ErrorPages/frmOperationSuccess.aspx"); 
                        //GeneralTools.MessageBox.ShowMessage("Password Changed Successfully");
                        //Response.Redirect("Welcome.htm");
                        
					} 
					else 
					{ 
						GeneralTools.MessageBox.ShowMessage(objBusinessChangePassword.Message); 
					} 
				} 
				catch (Exception ex) 
				{
                    if (ex.Message != "Thread was being aborted.")
                    {
                        GeneralTools.ExceptionLogger.ExceptionLog(ex);
                        Response.Redirect("../ErrorPages/ErrorPage.aspx");
                    }
                   
				} 
			
			}
		}
			#endregion

	}
}
