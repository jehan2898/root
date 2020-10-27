/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_GenerateMenu.aspx.cs
/*Purpose              :       To Add and Edit Menu
/*Author               :       Manoj c
/*Date of creation     :       16 Dec 2008  
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
public partial class Bill_Sys_GenerateMenu : PageBase
{
    private Bill_Sys_Menu _bill_Sys_Menu;
    private SaveOperation _saveOperation;
    private ListOperation _listOperation;
    private EditOperation _editOperation;
    private ArrayList _arrayList;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
            btnSave.Attributes.Add("onclick", "return formValidator('frmGenerateMenu','txtMenuName,txtMenuLink');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmGenerateMenu','txtMenuName,txtMenuLink');");
            if (!IsPostBack)
            {
                BindGrid();
                btnUpdate.Enabled = false;
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_GenerateMenu.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    #region "Event Handler"
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        {
            txtMenuCode.Text = GenerateMenuID();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "menu.xml";
            _saveOperation.SaveMethod();
            BindGrid();
            lblMsg.Visible = true;
            lblMsg.Text = " Menu Saved successfully ! ";
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = Session["Menu_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "menu.xml";
            _editOperation.UpdateMethod();
            BindGrid();
            lblMsg.Visible = true;
            lblMsg.Text = " Menu Updated successfully ! ";
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtMenuCode.Text = "";
            txtMenuDesc.Text = "";
            txtMenuLink.Text = "";
            txtMenuName.Text = "";
            extddlParentID.Text = "0";
            Session["Menu_ID"] = "";
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            lblMsg.Visible = false;
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
    protected void grdMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {            
            Session["Menu_ID"]= grdMenu.Items[grdMenu.SelectedIndex].Cells[1].Text;
            if(grdMenu.Items[grdMenu.SelectedIndex].Cells[2].Text!="&nbsp;") {txtMenuName.Text = grdMenu.Items[grdMenu.SelectedIndex].Cells[2].Text;}
            if (grdMenu.Items[grdMenu.SelectedIndex].Cells[3].Text != "&nbsp;") {txtMenuCode.Text = grdMenu.Items[grdMenu.SelectedIndex].Cells[3].Text;}
            if (grdMenu.Items[grdMenu.SelectedIndex].Cells[4].Text != "&nbsp;") {txtMenuLink.Text = grdMenu.Items[grdMenu.SelectedIndex].Cells[4].Text;}
            if (grdMenu.Items[grdMenu.SelectedIndex].Cells[5].Text != "&nbsp;") {chkIsAccessible.Checked = Convert.ToBoolean(grdMenu.Items[grdMenu.SelectedIndex].Cells[5].Text);}
            if (grdMenu.Items[grdMenu.SelectedIndex].Cells[6].Text != "&nbsp;") { extddlParentID.Text = grdMenu.Items[grdMenu.SelectedIndex].Cells[6].Text; }
            if (grdMenu.Items[grdMenu.SelectedIndex].Cells[7].Text != "&nbsp;") {txtMenuDesc.Text = grdMenu.Items[grdMenu.SelectedIndex].Cells[7].Text;}
            
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            lblMsg.Visible = false;
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
    protected void grdMenu_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdMenu.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
            lblMsg.Visible = false;
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
    #endregion
    #region "Fetch Method"
    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _listOperation = new ListOperation();
        try
        {
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "menu.xml";
            _listOperation.LoadList();
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
    private string GenerateMenuID()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _arrayList = new ArrayList();
        _bill_Sys_Menu = new Bill_Sys_Menu();
        string MenuID = "";
        try
        {
            if (extddlParentID.Text.ToString() != "NA" && extddlParentID.Text.ToString() != "")
            {
                _arrayList = _bill_Sys_Menu.GetMenuID(extddlParentID.Text.ToString());
                if (_arrayList[1].ToString() != null && _arrayList[1].ToString() != "")
                {
                    MenuID = _arrayList[0].ToString() + Convert.ToChar(((int)Convert.ToChar(_arrayList[1])) + 1);
                }
                else
                {
                    MenuID = _arrayList[0].ToString() + "A";
                }
            }
            else
            {
                _arrayList = _bill_Sys_Menu.GetMenuID("0");
                MenuID = Convert.ToChar(((int)Convert.ToChar(_arrayList[0])) + 1) + "A";
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
        return MenuID;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }   
    #endregion

    
}
