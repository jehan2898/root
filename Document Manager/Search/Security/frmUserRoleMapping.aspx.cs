
#region Page History 
/*'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' ABBREVIATIONS USED  	:	S-START, 	A-ADD, 	U-UPDATE, 	D-DELETE, 	E-END 
'						    (E.G. SA1001 FOR START ADDING NEW CONTENTS AND EA1001 FOR END ADDING)
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' NAME OF FORM		    :	FRMUSERROLEMAPPING.ASPX.CS
' PURPOSE				:	THIS FORM WILL PROVIDE INTERFACE FOR ROLE MAPPING WITH USERS
' DEVELOPED BY			:	KAMAL GARG
' START DATE			: 	12 JAN 2007
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' CHANGE ID	    CALL NO.	CHANGE DATE	    DEVELOPER'S NAME	        PURPOSE OF CHANGE
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' 
'---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
#endregion

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
using BusinessGeneralMaster = CommonMasters.Business.CommonMasters.BusinessGeneralMaster;
using UserRoleData=Security.DataSet.UserRoleData;
using BusinessUserRoles = Security.Business.BusinessUserRoles; 
using SystemConfiguration = Common.SystemConfiguration; 
using CommonFunctions = Common.CommonFunctions; 
using System.Configuration; 
using Common.Interface; 
//using BusinessGeneralMaster = Common.CommonMasters.Business.BusinessGeneralMaster;

# endregion

namespace Security
{
	
	public partial class frmUserRoleMapping : PageBase
	{
		BusinessUserRoles objbusUserRole = new BusinessUserRoles(); 

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
	
		# region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //GeneralTools.ValidateUser.ValidateUserSession("Hurix");
			if(!IsPostBack)
			{
				try 
				{
                    //Session["HelpFile"] = "../Help/UserRoleMapping.htm";
                    //Session["Msg"]=null;
					lbxRoles.ClearSelection();
					
					if(Session["CompanyType"].ToString()=="E")
					{
						objbusUserRole.PopulateUserDetails(ref ddlUserName,"Employee");
					}
					else if(Session["CompanyType"].ToString()=="P")
					{
						objbusUserRole.PopulateUserDetailsUnitWise(ref ddlUserName,"Partner",Convert.ToInt32(Session["CompanyUnitID"]));
					}
					else if(Session["CompanyType"].ToString()=="C")
					{
						objbusUserRole.PopulateUserDetailsUnitWise(ref ddlUserName,"Customer",Convert.ToInt32(Session["CompanyUnitID"]));
					}

					BusinessUserRoles objBusinessUserRoles = new BusinessUserRoles(); 
					objBusinessUserRoles.PopulateRoleDetails(ref lbxRoles,Session["CompanyType"].ToString());
				
				} 
				catch(Exception ex)
				{
                    GeneralTools.ExceptionLogger.ExceptionLog(ex);
				}
			}
            ddlUserName.Focus();
            Form2.Attributes.Add("onkeypress", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSave.UniqueID + "').click();return false;}} else {return true}; ");
			      
		}
		#endregion

		# region Clear Button
		protected void btnClear_Click(object sender, System.EventArgs e)
		{
			ddlUserName.ClearSelection();
			lbxRoles.ClearSelection();
		}
		#endregion

		#region User Name Selection
		protected void ddlUserName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BusinessUserRoles objbusUserRole = new BusinessUserRoles(); 
			try 
			{ 
				lbxRoles.ClearSelection();
				objbusUserRole.PopulateSeletedRoles(lbxRoles,ddlUserName.SelectedItem.Value);
				
			}
			catch (Exception ex) 
			{
                GeneralTools.ExceptionLogger.ExceptionLog(ex);
                Response.Redirect("../ErrorPages/ErrorPage.aspx"); 
			} 
		}
		#endregion
       
		# region Save Button Click
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			Page.Validate(); 
			if (Page.IsValid) 
			{ 
				BusinessUserRoles objbusUserRole = new BusinessUserRoles(); 
				try 
				{

                    string strEmailID, strRoles = "", strRolestxt = "";
					strEmailID=ddlUserName.SelectedItem.Value;
					for (int i=0;i<=lbxRoles.Items.Count-1; i++)
					{

						if (lbxRoles.Items[i].Selected==true)
						{
							if (strRoles == "") 
							{ 
								strRoles = strRoles + lbxRoles.Items[i].Value;
                                strRolestxt = strRolestxt + lbxRoles.Items[i].Text;
							} 
							else 
							{ 
								strRoles = strRoles + "," + lbxRoles.Items[i].Value;
                                strRolestxt = strRolestxt + "<BR>" + lbxRoles.Items[i].Text; 
							
							} 	
														
						}
					}
				if (objbusUserRole.BSaveUserRoleData(strEmailID,strRoles)==true)
				{
                    //Kamal 16 April 2007 Mail Instance
                    //*************************************************************************************
                    //Getting Mail Senders Address
                    string strUserName = "";
                    string strMailFrom, strSubject, strBody;
                    strMailFrom = HttpContext.Current.Session["UserEmail"].ToString();
                    strSubject = "Document Manager New privileges";

                    //Constructing Message body

                    strBody = "Hi ,<BR>";

                    strBody = strBody + "<BR>Your privileges has been changed.Now you have following privileges. <BR><BR><BR>";

                    strBody = strBody + strRolestxt + "<BR><BR><BR>";
                    strBody = strBody + "From:<BR>";
                    strBody = strBody + "Document Manager Administrator<BR>";


                    //Sending Password Mail
                    GeneralTools.MailFunctions.SendMail(strMailFrom, strEmailID, strSubject, strBody);

                    //**************************************************************************************
                    //Session["Msg"]="Roles Assigned Successfully";
                    Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER11");
                    Response.Redirect("../ErrorPages/frmOperationSuccess.aspx"); 
				}
				
				} 
				catch (System.Exception ex) 
				{
                    if (ex.Message != "Thread was being aborted.")
                    {
                        GeneralTools.ExceptionLogger.ExceptionLog(ex);
                        //Session["Msg"] = "An error has occurred while saving data";
                        Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER44");
                        Response.Redirect("../ErrorPages/ErrorPage.aspx");
                    }
				} 
			} 
		}
		#endregion

	}
}
