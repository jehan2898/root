#region History
/* '---------------------------------------------------------------------------------------------------------------------------------------------------------------
' Name of Form		    :	MyMenu.aspx.cs
' Purpose				:	This form will provide central navigation form as main page of application
' Developed By			:	Adarsh Bajpai
' Start Date			: 	27 November 2007
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' Change ID	    Call No.	Change Date	    Developer's Name	        Purpose of Change
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
'---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
#endregion

#region Libraries

using ResourceHandler = Common.ResourceHandler; 
using StoredProceduresDefination = Common.StoredProceduresDefination; 
using BusinessUserMenu = Security.Business.BusinessUserMenu; 
using UserMenuData = Security.DataSet.UserMenuData; 
using SystemConfiguration = Common.SystemConfiguration; 
using CommonFunctions = Common.CommonFunctions; 
using System.Configuration; 
using Common.Interface; 
using GeneralTools; 
using System.Data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

#endregion

namespace Security
{
	public partial class MyMenu : PageBase 
	{ 
		System.Globalization.CultureInfo fmt = new System.Globalization.CultureInfo("en-US", false); 

		
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

        #region Page_Load
        protected void Page_Load(object sender, System.EventArgs e) 
		{ 
						
			System.Threading.Thread.CurrentThread.CurrentCulture = fmt; 
			System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            
            GeneralTools.ValidateUser.ValidateUserSession("Hurix");

			if (!(Page.IsPostBack)) 
			{ 
				try 
				{
                    UserMenuData dsMenu = new UserMenuData();
                    int intNoofMenus = 0;
                    string strUserName = "";
                    string strUserRole = "";
                    string strHomePage = "inner.html";
                    BusinessUserMenu BusinessUserMenu = new BusinessUserMenu();
                    BusinessUserMenu.GetUserMenuData(ref dsMenu, Convert.ToInt32(Session["UserID"]), ref intNoofMenus, ref strUserName, ref strUserRole);
                    lbl_wusr.Text = "Welcome " + strUserName + " - " + strUserRole + " | Logged on: " + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
                    MenuCell.Height = Convert.ToString(intNoofMenus * 27);

                    Session["RoleName"] = strUserRole;

                    WebMenu1.DataSource = dsMenu;
                    WebMenu1.DataBind();
                    Session["HelpFile"] = "../Help/WelcomeHelp.htm";

                    //HtmlControl frame1 = new System.Web.UI.HtmlControls.HtmlGenericControl("iframe");
                    //frame1 = (HtmlControl)this.FindControl("MyFrame");
                    //frame1.Attributes["src"] = strHomePage;
                    //frame1.Attributes["frameborder"] = "1";
                    //frame1.Attributes["scrolling"] = "auto";
             
                    lnkHelp.Attributes.Add("onclick", "return MyHelp();");
                    lnkHome.Attributes.Add("onclick", "return loadIframe('MyFrame','" + strHomePage + "');");
                    string strShowHelp=Common.SystemConfiguration.GetApplicationParameters("ShowHelp");
                    if (strShowHelp == "Yes")
                    {
                        lnkHelp.Visible = true;
                    }
                    else
                    {
                        lnkHelp.Visible = false;
                    }

                    if (Request.QueryString["CheckLogin"] == "False")
                    {
                        //frame1.Attributes["src"] = "frmChangePassword.aspx?CheckLogin=False";
                        //frame1.Attributes["frameborder"] = "0";
                        //frame1.Attributes["scrolling"] = "auto";
                        Session["HelpFile"] = "../Help/ChangePassword.htm";
                        return;
                    }				
				}  
				catch (Exception ex) 
				{
                    GeneralTools.ExceptionLogger.ExceptionLog(ex);
					string ErrorMessage;
					ErrorMessage = "While executing page an error caught" + Convert.ToChar(13) + "At : " + Request.Url.ToString() + Convert.ToChar(13) + "Error Message: " + ex.Message.ToString() + Convert.ToChar(13) + "Stack Trace : " + ex.StackTrace.ToString(); 
					GeneralTools.MessageBox.ShowMessage(ErrorMessage); 
				} 
			}
        }
        #endregion
    }
}