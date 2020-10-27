/*---------------------------------------------------------------------------------------------------------------------------------------------------------------
' ABBREVIATIONS USED  	:	S-Start, 	A-Add, 	U-Update, 	D-Delete, 	E-End 
'						    (e.g. SA1001 for start adding new contents and EA1001 for end adding)
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' Name of Form		    :	frmRoleActivityMapping.aspx.vb
' Purpose				:	This form will an interface to map activities with roles
' Developed By			:	Adarsh Kr. Bajpai
' Start Date			: 	16 JANUARY 2007
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
using GeneralTools; 
using Telerik.WebControls; 
using ResourceHandler = Common.ResourceHandler; 
using StoredProceduresDefination = Common.StoredProceduresDefination; 
using BusinessActivityRole = Security.Business.BusinessActivityRole; 
using BusinessRoleMaster = Security.Business.BusinessRoleMaster; 
using SystemConfiguration = Common.SystemConfiguration; 
using CommonFunctions = Common.CommonFunctions; 
using System.Configuration; 
using Common.Interface;

# endregion

namespace Security
{
	
	public partial class frmRoleActivityMapping : PageBase
	{
		#region Page Controls
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
			this.TvwProductTree.NodeCheck += new Telerik.WebControls.RadTreeView.RadTreeViewEventHandler(this.TvwProductTree_NodeCheck);

		}
		#endregion

		#region Page Events

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e) 
		{ 
			btnClose.Attributes.Add("onclick", "if(confirm('Do you really want to exit?')==false){return false;} this.value='Wait...';this.disabled = true;" + GetPostBackEventReference(btnClose)); 
			btnSave.Attributes.Add("onclick", "this.value='Wait...';this.disabled=true;" + GetPostBackEventReference(btnSave)); 
			try 
			{
                GeneralTools.ValidateUser.ValidateUserSession("Hurix");
                if (!(IsPostBack))
                {
                    Session["HelpFile"] = "../Help/ActivityRoleMapping.htm";
                    Session["Msg"] = null;
					BusinessActivityRole objBusinessActivityRole = new BusinessActivityRole(); 
					rowRoleNameText.Visible = false; 
					objBusinessActivityRole.FillRolesData(ref ddlRoleName, 0); 
					objBusinessActivityRole.PopulateMenuTree(TvwProductTree); 
					ViewState.Add("ModeofOperation","UPDATE");
				}
                ddlRoleName.Focus();
                Form2.Attributes.Add("onkeypress", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSave.UniqueID + "').click();return false;}} else {return true}; ");
			} 
			catch (System.Exception ex) 
			{
                GeneralTools.ExceptionLogger.ExceptionLog(ex);
                Response.Redirect("../ErrorPages/ErrorPage.aspx"); 
			} 

		} 
		# endregion

		# region Page Error
		protected void Page_Error(object sender, System.EventArgs e) 
		{ 
			string strErrorMessage = Server.GetLastError().Message; 
			Server.ClearError();
            Response.Redirect("../ErrorPages/ErrorPage.aspx?ErrorMessage=" + strErrorMessage);
		} 

		# endregion

		# endregion

		#region Role Name Selection Change
        protected void ddlRoleName_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                if (ddlRoleName.SelectedItem.Value == "Select")
                {
                    TvwProductTree.ClearCheckedNodes();
                    RadTreeNode objLVNd = new RadTreeNode();
                    objLVNd = TvwProductTree.FindNodeByValue("ROOT");
                    objLVNd.ExpandParentNodes();
                    objLVNd.CollapseChildNodes();
                }
                else
                {
                    BusinessActivityRole objBusinessActivityRole = new BusinessActivityRole();
                    objBusinessActivityRole.PopulateTreeForRole(ref TvwProductTree, Convert.ToInt32(ddlRoleName.SelectedItem.Value));
                }
            }
        } 

		# endregion
        		
		# region Add Button Click 
		protected void btnAdd_Click(object sender, System.EventArgs e) 
		{ 
			if (btnAdd.Text == "Add Role") 
			{ 
				ddlRoleName.Visible = false; 
				txtRoleName.Visible = true; 
				rowRoleNameText.Visible = true; 
				txtRoleName.Text="";
				rowRoleNameDropDown.Visible = false; 
				RFVRoleNameText.Enabled = false; 
				REVRoleNameText.Enabled = false; 
				btnAdd.Text = "Back"; 
				ViewState["ModeofOperation"]="INSERT";
				TvwProductTree.ClearCheckedNodes();
				RadTreeNode objLVNd = new RadTreeNode(); 
				objLVNd=TvwProductTree.FindNodeByValue("ROOT");
				objLVNd.ExpandParentNodes();
				objLVNd.CollapseChildNodes();
			} 
			else 
			{ 
				RFVRoleNameText.Enabled = false; 
				REVRoleNameText.Enabled = false; 
				rowRoleNameText.Visible = false; 
				rowRoleNameDropDown.Visible = true; 
				ddlRoleName.Visible = true; 
				txtRoleName.Visible = false; 
				btnAdd.Text = "Add Role"; 
				ViewState["ModeofOperation"]="UPDATE";
				TvwProductTree.ClearCheckedNodes();
				RadTreeNode objLVNd = new RadTreeNode(); 
				objLVNd=TvwProductTree.FindNodeByValue("ROOT");
				objLVNd.ExpandParentNodes();
				objLVNd.CollapseChildNodes();
			} 
		}

		# endregion

		# region Save Button Click 
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			
            string strBaseType=Session["UserID"].ToString();
			if (ddlRoleName.Visible==false)
			{
				REVRoleNameText.Enabled=true;
			}
	
			Page.Validate(); 
			if (Page.IsValid) 
			{ 
				if (ddlRoleName.Visible==true)
				{
					if(ddlRoleName.SelectedIndex==0)
					{
                        GeneralTools.MessageBox.ShowMessage(Common.CommonFunctions.GetResourceValue("en-US", "ER80"));
						return;
					}
				}
				if (txtRoleName.Visible==true)
				{
					if (txtRoleName.Text.Trim()=="")
					{
						GeneralTools.MessageBox.ShowMessage(Common.CommonFunctions.GetResourceValue("en-US", "ER81"));
						return;
					}

                    if (txtRoleName.Text.Trim().Length >30)
                    {
                        GeneralTools.MessageBox.ShowMessage("Role Name Should be Max. 50 Char Long");
                        return;
                    }
				}
				int intCount=0;
				ArrayList NodeList = TvwProductTree.CheckedNodes; 
				if(NodeList.Count==0)
				{
					intCount=1; 
				}
				if(intCount>0)
				{
					GeneralTools.MessageBox.ShowMessage( Common.CommonFunctions.GetResourceValue("en-US", "ER82"));
					return;
				}	 
			
					BusinessActivityRole objBusinessActivityRole = new BusinessActivityRole(); 
					try 
					{
						int intRoleID;

						if (ddlRoleName.SelectedItem.Value=="Select")
							intRoleID=0;
						else
							intRoleID=Convert.ToInt32(ddlRoleName.SelectedItem.Value);

						objBusinessActivityRole.SaveActivityRoleData(ref TvwProductTree,Convert.ToString(ViewState["ModeofOperation"]),txtRoleName.Text, intRoleID,strBaseType);

					} 
					catch (System.Exception ex) 
					{ 
						if(ex.Message!="Thread was being aborted.")
						{
                            GeneralTools.ExceptionLogger.ExceptionLog(ex);
                            //Session["Msg"] = "An error has occurred while saving roles data";
                            Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER10");
                            Response.Redirect("../ErrorPages/ErrorPage.aspx");
						}
					} 
				
				}
				
			} 
		#endregion

		# region Close Button Click 

		protected void btnClose_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Welcome.htm"); 
		}
		# endregion
                
        #region Role Tree node Click 
        protected void TvwProductTree_NodeCheck(object o, RadTreeNodeEventArgs e)
        {
            BusinessActivityRole objBusinessActivityRole = new BusinessActivityRole();
            objBusinessActivityRole.CheckUnCheckChilds(ref e.NodeChecked);
        }
        # endregion

}
}
