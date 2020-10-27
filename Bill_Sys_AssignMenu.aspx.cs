/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_AssignMenu.aspx.cs
/*Purpose              :       Display Menu
/*Author               :       Sandeep Y
/*Date of creation     :       15 Dec 2008  
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
using System.Data.SqlClient;

public partial class Bill_Sys_AssignMenu : PageBase
{
    Bill_Sys_Menu objBillSysMenu;

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
            btnSave.Attributes.Add("onclick", "return formValidator('frmAssignMenu','ddlUserRole');");

            if (Page.IsPostBack == false)
                PopulateddlUserRole();
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
            cv.MakeReadOnlyPage("Bill_Sys_AssignMenu.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void PopulateddlUserRole()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            objBillSysMenu = new Bill_Sys_Menu();

            ddlUserRole.DataSource = objBillSysMenu.GetRoleList();
            
            ddlUserRole.DataTextField = "Description";
            ddlUserRole.DataValueField = "Code";
            ddlUserRole.DataBind();

            ListItem objListItem = new ListItem(" --- Select ---","NA");
            ddlUserRole.Items.Insert(0, objListItem);
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

    public void Node_Populate(object sender,System.Web.UI.WebControls.TreeNodeEventArgs e)

        {
            if(e.Node.ChildNodes.Count == 0)
            {           
               switch( e.Node.Depth )
                { 
                    case 0:               
                        FillMasterMenu(e.Node);   
                        break;
                    case 1:
                        FillChildMenu(e.Node);
                        break;
                    case 2:
                        FillChildMenu(e.Node);
                        break;
                    case 3:
                        FillChildMenu(e.Node);
                        break;
                }   
            }           
        }

    public void FillMasterMenu(TreeNode node)
        {
        objBillSysMenu = new Bill_Sys_Menu();
            //string connString = ConfigurationManager.AppSettings["Connection_String"];
            //SqlConnection connection = new SqlConnection(connString);
            //SqlCommand command = new SqlCommand("select i_menu_id,sz_menu_name from mst_menu where i_parent_id is null", connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet MenuTable = new DataSet();
            MenuTable = objBillSysMenu.GetMasterMenu();
         //   adapter.Fill(MenuTable);
            if (MenuTable.Tables.Count > 0)
            {
                foreach (DataRow row in MenuTable.Tables[0].Rows)
                {
                    TreeNode newNode = new
                    TreeNode(row["sz_menu_name"].ToString(), row["i_menu_id"].ToString());
                    newNode.PopulateOnDemand = true;
                    newNode.ShowCheckBox = true;
                    newNode.SelectAction = TreeNodeSelectAction.Expand;
                    node.ChildNodes.Add(newNode);
                    
                    
                }
            }
        }


    void FillChildMenu(TreeNode node)
        {
            string MenuID = node.Value;
            objBillSysMenu = new Bill_Sys_Menu();
            //string connString = ConfigurationManager.AppSettings["Connection_String"];
            //SqlConnection connection = new SqlConnection(connString);
            //SqlCommand command = new SqlCommand("select i_menu_id,sz_menu_name from mst_menu where i_parent_id = '" + MenuID + "'", connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ChildMenuTable = new DataSet();
            ChildMenuTable = objBillSysMenu.GetChildMenu(Convert.ToInt32(MenuID));
          //  adapter.Fill(ChildMenuTable);
            if (ChildMenuTable.Tables.Count > 0)
            {
                foreach (DataRow row in ChildMenuTable.Tables[0].Rows)
                {
                    TreeNode newNode = new TreeNode(row["sz_menu_name"].ToString(), row["i_menu_id"].ToString());
                    newNode.PopulateOnDemand = true;
                    newNode.SelectAction = TreeNodeSelectAction.None;
                    newNode.ShowCheckBox = true;
                    node.ChildNodes.Add(newNode);
                   
                }
            }
        }

    protected void tvwmenu_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        
    }

    protected void tvwmenu_SelectedNodeChanged(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try {
            objBillSysMenu = new Bill_Sys_Menu();
            for (int i = 0; i < tvwmenu.CheckedNodes.Count; i++)
            {
                if (i == 0)
                    objBillSysMenu.DeleteTxnMenuRole(ddlUserRole.SelectedValue);
                objBillSysMenu.SaveTxnMenuRole(ddlUserRole.SelectedValue ,  Convert.ToInt32(tvwmenu.CheckedNodes[i].Value));
            }
            BindTreeView();
            lblMsg.Visible = true;
            lblMsg.Text = "Menu Saved Successfully...!";
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


    protected void set(int menuid)
    {
 
    }

    //protected void extddlUserRole_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    //{
      
    //}

    protected void ddlUserRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTreeView();
        lblMsg.Visible = false;
    }


    protected void BindTreeView()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            foreach (TreeNode treenode in tvwmenu.Nodes[0].ChildNodes)
            {
                if (treenode.ChildNodes.Count > 0)
                {
                    foreach (TreeNode childTreenode in treenode.ChildNodes)
                    {

                        childTreenode.Checked = false;

                    }
                }

                treenode.Checked = false;

            }

            //string connString = ConfigurationManager.AppSettings["Connection_String"];
            //SqlConnection connection = new SqlConnection(connString);
            //SqlCommand command = new SqlCommand("select i_menu_id from TXN_USER_ACCESS where SZ_USER_ROLE_ID = '" + ddlUserRole.SelectedValue + "'", connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(command);


            objBillSysMenu = new Bill_Sys_Menu();
            DataSet ChildMenuTable = new DataSet();
            ChildMenuTable = objBillSysMenu.GetSelectedMenu(ddlUserRole.SelectedValue);
            // adapter.Fill(ChildMenuTable);
            if (ChildMenuTable.Tables.Count > 0)
            {
                foreach (DataRow row in ChildMenuTable.Tables[0].Rows)
                {
                    String menuid = row["i_menu_id"].ToString();

                    foreach (TreeNode treenode in tvwmenu.Nodes[0].ChildNodes)
                    {
                        if (treenode.ChildNodes.Count > 0)
                        {

                            TreeNode parentTreenode = new TreeNode();
                            TreeNode _parentTreenode = new TreeNode();
                            parentTreenode = treenode;
                            Boolean status = false;
                            int st=1;
                            sa:
                            foreach (TreeNode childTreenode in parentTreenode.ChildNodes)
                            {
                                if (childTreenode.Value == menuid)
                                {
                                    status = false;
                                    childTreenode.Checked = true;
                                    break;
                                }
                                if (childTreenode.ChildNodes.Count > 0)
                                {
                                    status = true;
                                    _parentTreenode = parentTreenode;
                                    parentTreenode = childTreenode;
                                    goto sa;
                                }
                                
                                if (status == true && parentTreenode.ChildNodes.Count == st)
                                {
                                    status = false;
                                    parentTreenode = _parentTreenode;

                                    childTreenode.Parent.Checked = true;
                                }
                                st = st + 1;
                            }
                        }

                        if (treenode.Value == menuid)
                        {
                            treenode.Checked = true;
                            break;
                        }


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
}

