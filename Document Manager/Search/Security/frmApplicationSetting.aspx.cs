#region History
/*---------------------------------------------------------------------------------------------------------------------------------------------------------------
' ABBREVIATIONS USED  	:	S-Start, 	A-Add, 	U-Update, 	D-Delete, 	E-End 
'						    (e.g. SA1001 for start adding new contents and EA1001 for end adding)
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' Name of Form		    :	frmAppliocationSetting.aspx.cs
' Purpose				:	This form will used for Application Setting.
' Developed By			:	Bhilendra Yede
' Start Date			: 	01 FEB 2007
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' Change ID	    Call No.	Change Date	    Developer's Name	        Purpose of Change
'---------------------------------------------------------------------------------------------------------------------------------------------------------------
' 
'---------------------------------------------------------------------------------------------------------------------------------------------------------------*/

#endregion

#region Using

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

using ApplicationSettingData = Security.DataSet.ApplicationSettingData;
//using DataApplicationSetting = Security.DataLayer.DataApplicationSetting;
using BuisnessApplicationSetting = Security.Business.BusinessApplicationSetting ;

#endregion

namespace Security
{
	/// <summary>
	/// Summary description for frmApplicationSetting.
	/// </summary>
	public partial class frmApplicationSetting : PageBase
	{
        public string SortField;
	
		#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				try 
				{
                    ViewState.Add("LastSortField", "NA");
                    ViewState.Add("LastSortOrder", "NA");
					BindApplicationData();
                    trGridPaging.Visible = false;
                    Session["HelpFile"] = "../Help/ApplicationSettings.htm";
				} 
				catch(Exception ex)
				{
                    GeneralTools.ExceptionLogger.ExceptionLog(ex);
					throw ex;
				}
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
			this.dgApplicationSetting.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgApplicationSetting_PageIndexChanged);
			this.dgApplicationSetting.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgApplicationSetting_ItemDataBound_1);

		}
		#endregion

		#region Bind DataGrid Data
		public void BindApplicationData()
		{
			BuisnessApplicationSetting objBuisnessApplicationSetting = new BuisnessApplicationSetting();
			ApplicationSettingData  objApplicationSettingData = new ApplicationSettingData();

			if (objBuisnessApplicationSetting.GetApplicationSettingData(ref objApplicationSettingData)== true)
			{
				dgApplicationSetting.DataSource = objApplicationSettingData.Tables[ApplicationSettingData.TABLE_APPLICATIONSETTING].DefaultView ;
				dgApplicationSetting.DataBind ();

				ViewState.Add("ApplicationData",objApplicationSettingData);
			}
		}
		#endregion

		#region DataGrid ItemDataBound
		private void dgApplicationSetting_ItemDataBound_1(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				if(e.Item.ItemType.ToString()!="Header" && e.Item.ItemType.ToString()!="Footer")
				{
					TextBox txtParameterValue;
					txtParameterValue=(TextBox) e.Item.Cells[4].FindControl("txtParameterValue");
					txtParameterValue.Text=e.Item.Cells[3].Text.ToString();
				}
			}
			catch(Exception ex)
			{
                GeneralTools.ExceptionLogger.ExceptionLog(ex);
				throw ex;
			}

		}


		private void dgApplicationSetting_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgApplicationSetting.CurrentPageIndex =e.NewPageIndex ;
			BindApplicationData();
		
		}

		#endregion

		#region Submit Button

		protected void btnAppSubmit_Click(object sender, System.EventArgs e)
		{
			 Page.Validate();
             if (Page.IsValid)
             {
                 try
                 {
                     TextBox txtParameterValue;
                     ApplicationSettingData objApplicationSettingData = (ApplicationSettingData)ViewState["ApplicationData"];

                     for (int intCounter = 0; intCounter <= dgApplicationSetting.Items.Count - 1; intCounter++)
                     {
                         txtParameterValue = (TextBox)dgApplicationSetting.Items[intCounter].Cells[4].FindControl("txtParameterValue");
                         int intParameterID = Convert.ToInt32(dgApplicationSetting.Items[intCounter].Cells[0].Text);
                         objApplicationSettingData.Tables[ApplicationSettingData.TABLE_APPLICATIONSETTING].Rows[intCounter][ApplicationSettingData.FIELD_PARAMETER_VALUE] = txtParameterValue.Text;
                     }
                     BuisnessApplicationSetting objbuBuisnessApplicationSetting = new BuisnessApplicationSetting();
                     objbuBuisnessApplicationSetting.SaveAppicationSettingData(ref objApplicationSettingData);

                     //Session["Msg"]="Application Setting Update Successfully";
                     Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER6");
                     Response.Redirect("../ErrorPages/frmOperationSuccess.aspx");
                 }
                 catch (Exception ex)
                 {
                     if (ex.Message != "Thread was being aborted.")
                     {
                         GeneralTools.ExceptionLogger.ExceptionLog(ex);
                         //Session["Msg"]="An error has occurred while update Application Setting";
                         Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER59");
                         Response.Redirect("../ErrorPages/ErrorPage.aspx");
                     }
                 }
             }
		}
		
	#endregion


        protected void ddlGridPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgApplicationSetting.PageSize = Convert.ToInt32(ddlGridPaging.SelectedItem.Value);
            BindApplicationData();
        }

        #region Data Grid ApplicationSetting SortCommand

        protected void dgApplicationSetting_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            //SortField = (string)e.SortExpression;
            if (ViewState["LastSortField"].ToString() == "NA")
            {
                SortField = e.SortExpression + " ASC";
                ViewState["LastSortField"] = e.SortExpression;
                ViewState["LastSortOrder"] = "ASC";
            }
            else if (e.SortExpression == ViewState["LastSortField"].ToString())
            {
                if (ViewState["LastSortOrder"].ToString() == "ASC")
                {
                    SortField = e.SortExpression + " DESC";
                    ViewState["LastSortOrder"] = "DESC";
                }
                else
                {
                    SortField = e.SortExpression + " ASC";
                    ViewState["LastSortOrder"] = "ASC";
                }
            }
            else
            {
                SortField = e.SortExpression + " ASC";
                ViewState["LastSortField"] = e.SortExpression;
                ViewState["LastSortOrder"] = "ASC";
            }
            BuisnessApplicationSetting objBuisnessApplicationSetting = new BuisnessApplicationSetting();
            ApplicationSettingData objApplicationSettingData = new ApplicationSettingData();
            if (objBuisnessApplicationSetting.GetApplicationSettingData(ref objApplicationSettingData) == true)
            {                
                DataView objDataView = new DataView(objApplicationSettingData.Tables[ApplicationSettingData.TABLE_APPLICATIONSETTING]);
                objDataView.Sort = SortField;
                dgApplicationSetting.DataSource = objDataView;
                dgApplicationSetting.DataBind();
                     
            }

        }

        #endregion
    }
}
