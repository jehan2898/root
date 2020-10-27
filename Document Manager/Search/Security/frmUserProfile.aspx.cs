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

public partial class Security_frmUserProfile : PageBase
{    
        protected System.Web.UI.WebControls.DropDownList ddlCompanyType;
		BusinessGeneralMaster objbusinessGeneralMaster= new BusinessGeneralMaster();
        BusinessUser objBusinessUser = new BusinessUser();
		
        #region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
            

			Session["Msg"]="";
        
            GeneralTools.ValidateUser.ValidateUserSession("Hurix");
            Session["HelpFile"] = "../Help/CreateUser.htm";
			if(!IsPostBack)
						
			{               
                    DataTable dtUserData = new DataTable();
                    objBusinessUser.GetUserData("All", ref dtUserData);
                    gdUsers.DataSource=dtUserData.DefaultView;
                    gdUsers.DataBind();                 
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

		}
		#endregion

		#region Submit Button

		protected void btnSubmit_ServerClick(object sender, System.EventArgs e)
		{
             
		}
		#endregion

        #region Gridview DataBound
        protected void gdUsers_DataBound(object sender, EventArgs e)
    {
       foreach (GridViewRow gdRow in gdUsers.Rows)
        {            
            LinkButton lnkReply = (LinkButton)gdRow.Cells[10].Controls[0];
            string strID;
            strID = "";
            strID = gdUsers.DataKeys[gdRow.RowIndex].Values[1].ToString();
            if (strID == "True")
            {               
                lnkReply.Text = "Deactivate";
            }
            else
            {
               lnkReply.Text = "Activate";
            }
        }
    }
        #endregion

        #region Gridview Row Command 
    

    public void gdUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string strUSERID;
        strUSERID = "";
        strUSERID = gdUsers.DataKeys[e.NewEditIndex].Values[0].ToString();          
        Response.Redirect("../UserRegistration/frmCreateUsers.aspx?OPR=E&UID=" + strUSERID);
       

    }
       
    protected void gdUsers_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string strUSERID, strStatus, strEvent;
        strStatus = "";
        strUSERID = "";
        strUSERID = gdUsers.DataKeys[e.NewSelectedIndex].Values[0].ToString();
        strStatus = gdUsers.DataKeys[e.NewSelectedIndex].Values[1].ToString();
        if (strStatus == "True")
        {
            strStatus = "0";
        }
        else
        {
            strStatus = "1";
        }
        objBusinessUser.ActivateUser(strUSERID, strStatus);
        DataTable dtUserData = new DataTable();
        objBusinessUser.GetUserData("All", ref dtUserData);
        gdUsers.DataSource = dtUserData.DefaultView;
        gdUsers.DataBind();
    }
        #endregion

        #region gdUsers_PageIndexChanging
    protected void gdUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdUsers.PageIndex=e.NewPageIndex;
        DataTable dtUserData = new DataTable();
        objBusinessUser.GetUserData("All", ref dtUserData);
        gdUsers.DataSource = dtUserData.DefaultView;
        gdUsers.DataBind();
    }
    #endregion
}
