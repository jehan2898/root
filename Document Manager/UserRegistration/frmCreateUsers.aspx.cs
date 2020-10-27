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
using System.Web.Mail; 
using System.Text; 
using GeneralTools; 
using System.Data.SqlClient; 
using BusinessCreateUsers = UserRegistration.Business.UserRegistration.BusinessCreateUsers;
using BusinessGeneralMaster = CommonMasters.Business.CommonMasters.BusinessGeneralMaster;
using GeneralMasterData = CommonMasters.DataSet.CommonMasters.GeneralMasterData;
using BusinessUser = Security.Business.BusinesUser;

namespace UserRegistration
{
	
	public partial class frmCreateUsers : PageBase
	{
		protected System.Web.UI.WebControls.DropDownList ddlCompanyType;
		BusinessGeneralMaster objbusinessGeneralMaster= new BusinessGeneralMaster();
        BusinessUser objBusinessUser = new BusinessUser();
		#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
            

			Session["Msg"]="";
            txtFName.Focus();
            //GeneralTools.ValidateUser.ValidateUserSession("Hurix");
            Session["HelpFile"] = "../Help/CreateUser.htm";
			if(!IsPostBack)
						
			{
                GeneralMasterData datGeneralMaster = new GeneralMasterData();
                objbusinessGeneralMaster.GetGeneralMasterData(ref datGeneralMaster,"User_Type", 13);
                ddlUserType.DataSource = datGeneralMaster;
                ddlUserType.DataValueField = datGeneralMaster.Tables[0].Columns[1].ToString();
                ddlUserType.DataTextField = datGeneralMaster.Tables[0].Columns[4].ToString();
                ddlUserType.DataBind();

                ddlUserType.Items.Insert(0, "-- Select --");
				objbusinessGeneralMaster.FillOtherData(ddlRoles,"tblRoles","RoleID","RoleName");
                ddlRoles.Items.Insert(0, "-- Select --");

                if (Request.QueryString["OPR"] == "I")
                {
                    lblPageHeader.Text = "Home - Add User";
                }
                else
                {
                    lblPageHeader.Text = "Home - Edit User";
                    DataTable dtUserData = new DataTable();
                    if (Request.QueryString["UID"].ToString() == "0")
                    {
                        objBusinessUser.GetUserData(Session["UserID"].ToString(), ref dtUserData);
                    }
                    else
                    {
                        objBusinessUser.GetUserData(Request.QueryString["UID"].ToString(), ref dtUserData);
                    }
                    txtFName.Text = dtUserData.Rows[0][0].ToString();
                    txtLName.Text = dtUserData.Rows[0][1].ToString();
                    txtEmail.Text = dtUserData.Rows[0][4].ToString();
                    ddlRoles.Items.FindByValue(dtUserData.Rows[0][5].ToString()).Selected=true;
                    ddlUserType.Items.FindByValue(dtUserData.Rows[0][3].ToString().Trim()).Selected = true;
                }
                if (Request.QueryString["OPR"] == "V")
                {
                    lblPageHeader.Text = "Home - View User";
                    txtFName.Enabled = false;
                    txtLName.Enabled = false;
                    txtEmail.Enabled = false;
                    ddlRoles.Enabled = false;
                    ddlUserType.Enabled = false;
                    btnSubmit.Visible = false;
                }
                else
                {
                    txtFName.Enabled = true;
                    txtLName.Enabled = true;
                    txtEmail.Enabled = true;
                    ddlRoles.Enabled = true;
                    ddlUserType.Enabled = true;
                    btnSubmit.Visible = true;
                }

			}
            txtFName.Focus();
            Form1.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSubmit.UniqueID + "').click();return false;}} else {return true}; ");
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

		protected void btnSubmit_ServerClick(object sender, System.EventArgs e)
		{
            string strUserID;
            strUserID = "0";
			 Page.Validate();
             if (Page.IsValid)
             {
                 try
                 {
                     BusinessCreateUsers objBusinessCreateUsers = new BusinessCreateUsers();
                     if (Request.QueryString["UID"].ToString() == "0")
                     {
                         strUserID = Session["UserID"].ToString();
                     }
                     else
                     {
                         strUserID = Request.QueryString["UID"].ToString();
                     }
                     
                     if (Request.QueryString["OPR"] == "I")
                     {
                         if (objBusinessCreateUsers.SaveData("INSERT",txtFName.Text, txtLName.Text, ddlUserType.SelectedItem.Value, txtEmail.Text, Convert.ToInt32(ddlRoles.SelectedItem.Value),"0"))
                         {
                             Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER46");
                             Response.Redirect(Request.ApplicationPath + "/ErrorPages/frmOperationSuccess.aspx");
                         }
                         else
                         {
                             Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER47");
                             Response.Redirect(Request.ApplicationPath + "/ErrorPages/ErrorPage.aspx");
                         }

                     }
                     if (Request.QueryString["OPR"] == "E")
                     {
                         if (objBusinessCreateUsers.SaveData("UPDATE", txtFName.Text, txtLName.Text, ddlUserType.SelectedItem.Value, txtEmail.Text, Convert.ToInt32(ddlRoles.SelectedItem.Value), strUserID))
                         {
                             Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER55");
                             Response.Redirect(Request.ApplicationPath + "/ErrorPages/frmOperationSuccess.aspx");
                         }
                         else
                         {
                             Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER47");
                             Response.Redirect(Request.ApplicationPath + "/ErrorPages/ErrorPage.aspx");
                         }
                     }                     

                 }

                 catch (Exception ex)
                 {
                     if (ex.Message != "Thread was being aborted.")
                     {
                         GeneralTools.ExceptionLogger.ExceptionLog(ex);
                         Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER40");
                         Response.Redirect(Request.ApplicationPath + "/ErrorPages/ErrorPage.aspx");
                     }

                 }
             }
		}
		#endregion
	}
}
