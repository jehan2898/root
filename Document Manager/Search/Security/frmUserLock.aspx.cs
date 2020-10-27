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
using System.Configuration;
using ResourceHandler = Common.ResourceHandler;
using StoredProceduresDefination = Common.StoredProceduresDefination;
using BusinessUserLogin = Security.Business.BusinesUser;
using SystemConfiguration = Common.SystemConfiguration;
using CommonFunctions = Common.CommonFunctions;
using Common.Interface;
using GeneralTools;
using BusinessGeneralMaster = CommonMasters.Business.CommonMasters.BusinessGeneralMaster;

namespace Security
{
	
	public partial class frmUserLock : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            string strPwd = Request.QueryString["Pwd"].ToString();
            //string strEnyPwd  = EncryptionDecption.DataEncryption_Decryption.EncryptString(strPwd, true); 
            string strEnyPwd = DataEncrypt_Decrypt.EncryptString(strPwd, true); 
            string strMail = Request.QueryString["Mail"].ToString();
            BusinessGeneralMaster BGeneralMaster = new BusinessGeneralMaster();
            BGeneralMaster.UnlockUser(strMail, strEnyPwd);
            Session["HelpFile"] = "../Help/LockUser.htm";
		}

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
	}
}
