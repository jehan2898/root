/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_UserRole.aspx.cs
/*Purpose              :       To Add and Edit User Role
/*Author               :       Sandeep Y
/*Date of creation     :       07 Sept 2009 
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

public partial class Bill_Sys_AssignMenuToRole : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_Menu _objMenu;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           // saveSelectedDoc.Attributes.Add("onclick", "return formValidator('frmAssignMenuToRole','extCIddlMainMenu,extCIddlRoleName');");
            saveSelectedDoc.Attributes.Add("onclick", "return SelectedMenus();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            BindRoleList();
            BindMainMenuList();
            BindMenuListBox();
            BindRoleMenuListBox();
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_AssignMenuToRole.aspx");
        }
        #endregion
    }

    protected void BindRoleList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _objMenu = new Bill_Sys_Menu();
            ListItem _objLI = new ListItem("--- Select Role --- ", "-1");
            extCIddlRoleName.DataSource = _objMenu.GetRoleList(txtCompanyID.Text);
            extCIddlRoleName.DataValueField = "CODE";
            extCIddlRoleName.DataTextField = "DESCRIPTION";
            extCIddlRoleName.DataBind();
            extCIddlRoleName.Items.Insert(0, _objLI);

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

    protected void BindMainMenuList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ListItem _objLI = new ListItem("--- Select Menu --- ", "-1");
            _objMenu = new Bill_Sys_Menu();
            extCIddlMainMenu.DataSource = _objMenu.GetMainMenuList(txtCompanyID.Text);
            extCIddlMainMenu.DataValueField = "CODE";
            extCIddlMainMenu.DataTextField = "DESCRIPTION";
            extCIddlMainMenu.DataBind();
            extCIddlMainMenu.Items.Insert(0, _objLI);
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

    protected void extCIddlMainMenu_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            BindMenuListBox();
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

    protected void BindMenuListBox()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _objMenu = new Bill_Sys_Menu();
            if (extCIddlMainMenu.Text != "NA")
            {
                lstMenu.DataSource = _objMenu.GetChildMenu(Convert.ToInt32(extCIddlMainMenu.Text));
                lstMenu.DataTextField = "SZ_MENU_NAME";
                lstMenu.DataValueField = "I_MENU_ID";
                lstMenu.DataBind();
            }
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
    protected void extCIddlRoleName_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindRoleMenuListBox();
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

    protected void BindRoleMenuListBox()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _objMenu = new Bill_Sys_Menu();
            lstMenusToRole.DataSource = _objMenu.GetSelectedMenu(extCIddlRoleName.Text);
            lstMenusToRole.DataTextField = "MENU";
            lstMenusToRole.DataValueField = "i_menu_id";
            lstMenusToRole.DataBind();

            if (extCIddlMainMenu.Text != "NA")
            {
                lstMenu.DataSource = _objMenu.GetChildMenu(Convert.ToInt32(extCIddlMainMenu.Text));
                lstMenu.DataTextField = "SZ_MENU_NAME";
                lstMenu.DataValueField = "I_MENU_ID";
                lstMenu.DataBind();
            }

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

    protected void saveSelectedDoc_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _objMenu = new Bill_Sys_Menu();
            _objMenu.DeleteTxnMenuRole(extCIddlRoleName.Text);
            if (Request.Form["_ctl0:ContentPlaceHolder1:lbTest"] != null)
            {
                String szIDs = Request.Form["_ctl0:ContentPlaceHolder1:lbTest"].ToString();
                String[] IDs = szIDs.Split(',');

                for (int i = 0; i < IDs.Length; i++)
                {
                    _objMenu.SaveTxnMenuRole(extCIddlRoleName.Text, Convert.ToInt32(IDs[i].ToString()));
                }
            }
            lblMsg.Text = "Menu saved Successfully ...!";
            lblMsg.Visible = true;
            BindRoleMenuListBox();
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

    protected void btnMoveLeft_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //for (int i = 0; i < lstMenu.Items.Count; i++)
            //{
            //    if (lstMenu.Items[i].Selected)
            //    {
            //        string oldText = lstMenu.Items[i].Text;
            //        ListItem objListItem = new ListItem();
            //        objListItem.Text = extCIddlMainMenu.Selected_Text + " >> " + lstMenu.Items[i].Text;
            //        objListItem.Value = lstMenu.Items[i].Value;

            //        if (lstMenusToRole.Items.Contains(objListItem) == false)
            //        {
            //            lstMenusToRole.Items.Add(objListItem);
            //            lstMenusToRole.SelectionMode = ListSelectionMode.Multiple;
            //        }
            //    }
            //}
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

    protected void btnMoveRight_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i < lstMenusToRole.Items.Count; i++)
            {
                if (lstMenusToRole.Items[i].Selected)
                {
                    if (lstMenu.Items.Contains(lstMenusToRole.Items[i]) == false)
                    {
                        lstMenu.Items.Add(lstMenusToRole.Items[i]);
                        lstMenu.SelectionMode = ListSelectionMode.Multiple;
                    }
                }
            }
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
    
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i < lstMenusToRole.Items.Count; i++)
            {
                if (lstMenusToRole.Items[i].Selected)
                {
                    lstMenusToRole.Items.Remove(lstMenusToRole.Items[i]);
                    i = i - 1;
                }
            }
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
    
    protected void extCIddlRoleName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRoleMenuListBox();
    }
    
    protected void extCIddlMainMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMenuListBox();
    }
}

