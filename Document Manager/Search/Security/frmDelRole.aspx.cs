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

public partial class Security_frmDelRole : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "if(confirm('Do you really want to delete this role?')==false){return false;} this.value='Wait...';this.disabled = true;" + GetPostBackEventReference(btnDelete)); 
         try
        {
            //if (txtRoleName.Visible == true)
            //{
            //    RFVRoleNameText.Enabled = true;
            //    REVRoleNameText.Enabled = true;
            //}
            //else
            //{
            //    RFVRoleNameText.Enabled = false;
            //    REVRoleNameText.Enabled = false;
            //}
            GeneralTools.ValidateUser.ValidateUserSession("Hurix");
            if (!(IsPostBack))
            {
                Session["HelpFile"] = "../Help/DeleteRole.htm";
                Session["Msg"] = null;
                BusinessActivityRole objBusinessActivityRole = new BusinessActivityRole();
                rowRoleNameText.Visible = false;
                objBusinessActivityRole.FillRolesData(ref ddlRoleName, 0);
                
            }
            ddlRoleName.Focus();
            Form2.Attributes.Add("onkeypress", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnDelete.UniqueID + "').click();return false;}} else {return true}; ");
        }
        catch (System.Exception ex)
        {
            GeneralTools.ExceptionLogger.ExceptionLog(ex);
            Response.Redirect("../ErrorPages/ErrorPage.aspx");
        } 

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (btnEdit.Text == "Edit")
        {
            btnDelete.Enabled = false;
            ddlRoleName.Enabled = false;
            txtRoleName.Visible = true;
            rowRoleNameText.Visible = true;
            txtRoleName.Text = "";
            //rowRoleNameDropDown.Visible = false;
            //RFVRoleNameText.Enabled = false;
            //REVRoleNameText.Enabled = false;
            btnEdit.Text = "Update";
            ViewState["ModeofOperation"] = "EDIT";
          
        }
        else
        {
            if (txtRoleName.Text.Trim() == "")
            {
                //GeneralTools.MessageBox.ShowMessage("Please enter Role Name");
                return;

            }
            if (txtRoleName.Visible == true)
            {
               if (txtRoleName.Text.Trim().Length > 30)
                {
                    GeneralTools.MessageBox.ShowMessage("Role Name Should be Max. 50 Char Long");
                    return;
                }
            }
            btnDelete.Enabled = true;
            rowRoleNameText.Visible = false;
            rowRoleNameDropDown.Visible = true;
            ddlRoleName.Enabled = true;
            txtRoleName.Visible = false;
            btnEdit.Text = "Edit";
            ViewState["ModeofOperation"] = "UPDATE";
            BusinessActivityRole objBusinessActivityRole = new BusinessActivityRole();
            try
            {
                int intRoleID;

                if (ddlRoleName.SelectedItem.Value == "Select")
                    intRoleID = 0;
                else
                    intRoleID = Convert.ToInt32(ddlRoleName.SelectedItem.Value);
                
                objBusinessActivityRole.EditActivityRoleData("UPDATE", txtRoleName.Text, intRoleID);

            }
            catch (System.Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    GeneralTools.ExceptionLogger.ExceptionLog(ex);
                    //Session["Msg"] = "An error has occurred while saving roles data";
                    Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER10");
                    Response.Redirect("../ErrorPages/ErrorPage.aspx");
                }
            } 
           
        } 
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Welcome.htm"); 
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {        
        BusinessActivityRole objBusinessActivityRole = new BusinessActivityRole();
        try
        {
            int intRoleID;

            if (ddlRoleName.SelectedItem.Value == "Select")
                intRoleID = 0;
            else
                intRoleID = Convert.ToInt32(ddlRoleName.SelectedItem.Value);

            objBusinessActivityRole.EditActivityRoleData("DELETE", txtRoleName.Text, intRoleID);

        }
        catch (System.Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                GeneralTools.ExceptionLogger.ExceptionLog(ex);
                //Session["Msg"] = "An error has occurred while saving roles data";
                Session["Msg"] = Common.CommonFunctions.GetResourceValue("en-US", "ER10");
                Response.Redirect("../ErrorPages/ErrorPage.aspx");
            }
        } 
    }
}
