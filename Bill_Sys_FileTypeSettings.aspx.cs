/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_CaseType.aspx.cs
/*Purpose              :       To Add and Edit Case Type
/*Author               :       Sandeep Y
/*Date of creation     :       11 Dec 2008  
/*Modified By          :
/*Modified Date        :
/************************************************************/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Componend;

public partial class Bill_Sys_FileTypeSettings : PageBase
{
    private Bill_Sys_FileType_Settings _bill_Sys_FileType_Settings;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {


            btnUpdate.Attributes.Add("onclick", "return formValidator('frmFileTypeSettings','extDisplayType,extDisplayPosition,txtDescription');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (Session["Flag"] != null && Session["Flag"].ToString()=="true")
            {                
                TreeMenuControl1.Visible = false;
              
              
            }
            else
            {
                TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
                if (!IsPostBack)
                {
                    
                    _bill_Sys_FileType_Settings = new Bill_Sys_FileType_Settings();
                    DataSet dtTable=_bill_Sys_FileType_Settings.GET_Settings(txtCompanyID.Text);
                    if (dtTable.Tables.Count > 0)
                    {
                        extDisplayType.Text = dtTable.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        extDisplayPosition.Text = dtTable.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        txtDescription.Text = dtTable.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        if (extDisplayType.Text == "CI_0000003")
                        {
                            trDescription.Visible = false;
                            trDisplayPosition.Visible = false;
                            txtDescription.Text = "";
                            extDisplayPosition.Text = "NA";
                        }
                        else if (extDisplayType.Text == "CI_0000004")
                        {
                            trDescription.Visible = false;
                            trDisplayPosition.Visible = true;
                            txtDescription.Text = "";

                        }
                        else if (extDisplayType.Text == "CI_0000005")
                        {
                            trDescription.Visible = true;
                            trDisplayPosition.Visible = true;
                        }
                    }
                }
            }
            lblMsg.Text = "";

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_FileTypeSettings.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            _bill_Sys_FileType_Settings = new Bill_Sys_FileType_Settings();
            _bill_Sys_FileType_Settings.UpdateSettings(extDisplayType.Text, extDisplayPosition.Text, txtDescription.Text, txtCompanyID.Text);
            lblMsg.Text = "Updated Successfully!";
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected override void OnUnload(EventArgs e)
    { 
        Session["Flag"] = null;       
        base.OnUnload(e);
    }
    protected void extDisplayType_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (extDisplayType.Text == "CI_0000003")
            {
                trDescription.Visible = false;
                trDisplayPosition.Visible = false;
                txtDescription.Text= "";
                extDisplayPosition.Text = "NA";
            }
            else if (extDisplayType.Text == "CI_0000004")
            {
                trDescription.Visible = false;
                trDisplayPosition.Visible = true;
                txtDescription.Text = "";
               
            }
            else if (extDisplayType.Text == "CI_0000005")
            {
                trDescription.Visible = true;
                trDisplayPosition.Visible = true;
                   
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}

