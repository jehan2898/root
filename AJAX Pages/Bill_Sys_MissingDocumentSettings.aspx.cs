
/***********************************************************/
/*Project Name         :       Medical Billing System
/*Description          :       Display tree menu on the page, Remove GridView, display listbox showing the documents added
/*Author               :       Sandeep Y
/*Date of creation     :       15 Dec 2008
/*Modified By (Last)   :       Ananda Naphade
/*Modified By (S-Last) :       Prashant Zope
/*Modified Date        :       13 May 2010
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
using DocumentTypeBO;
using System.Data.SqlClient;

public partial class Bill_Sys_MissingDocumentSettings : PageBase
{

    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    string[] listButton;
    Bill_Sys_DocumentTypeBO objBillSysDocBO = new Bill_Sys_DocumentTypeBO(ConfigurationManager.AppSettings["Connection_String"].ToString());
    DataSet dsRequiredDoc = new DataSet();
    DAO_Assign_Doc DAO = new DAO_Assign_Doc();
    DataSet dsDocuments = new DataSet();
    ArrayList objarrlst = new ArrayList();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {            
            tvwmenu.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
     
           
            if (!IsPostBack)
            {
                txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                DataSet dsUserRole = objBillSysDocBO.GetUserRole(txtCompanyID.Text);
                ListItem list2 = new ListItem();
                list2.Text = "---Select---";
                list2.Value = "0";
                //ddUserRole.Items.Add(list2);
                for (int i = 0; i < dsUserRole.Tables[0].Rows.Count; i++)
                {
                    ListItem list = new ListItem();
                    list.Text = dsUserRole.Tables[0].Rows[i][1].ToString();
                    list.Value = dsUserRole.Tables[0].Rows[i][0].ToString(); ;
                    //ddUserRole.Items.Add(list);
                }

                lbSelectedDocs.Items.Clear();
                hfselectedNode.Value = "";
                dsDocuments = objBillSysDocBO.GetMissingDocumentsCompanyWise(txtCompanyID.Text);
                
                
                tvwmenu.PopulateNodesFromClient = true;
                tvwmenu.Nodes.RemoveAt(0);
                TreeNode node = new TreeNode("Document Manager", "0");
                node.PopulateOnDemand = true;
                tvwmenu.Nodes.Add(node);
                tvwmenu.ExpandAll();
            }
           // dsDocuments = GetDocumentsAssignedToCompany(txtCompanyID.Text);
            tvwmenu.Visible = true;
            //tvwmenu.ExpandAll();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_DocumentType.aspx");
        }
        #endregion

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string szNodes = hfselectedNode.Value;
    //    string szSelectedNodes = hfselectedNodeinListbox.Value;
    //    string[] Nodes = new string[500];
    //    Nodes = szNodes.Split(',');
    //    string[] SelectedNodes = new string[500];
    //    SelectedNodes = szSelectedNodes.Split(',');
    //    int iflag = 0;
    //    for (int i = 0; i < Nodes.Length; i++)
    //    {
    //        if (!Nodes[i].Equals(""))
    //        {
    //            for (int j = 0; j < SelectedNodes.Length; j++)
    //            {
    //                if (SelectedNodes[j].Equals(Nodes[i]))
    //                {
    //                    iflag = 1;
    //                }
    //            }
    //            objBillSysDocBO.AddSelectedRequiredDocuments(Nodes[i], extddlCaseType.Selected_Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, txtCompanyID.Text, i, iflag);
    //            iflag = 0;
    //        }
    //    }
    //    ClientScript.RegisterStartupScript(this.GetType(), "pagestart", "alert('hi'); window.onload();", true);
    //}

    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{

    //}

    //protected void btnClear_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ClearControl();
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    //private void ClearControl()
    //{
    //    try
    //    {
    //        extddlCaseType.Text = "NA";
    //        btnSave.Enabled = true;
    //        lblMsg.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    public void Node_Populate(object sender, System.Web.UI.WebControls.TreeNodeEventArgs e)
    {
        if (e.Node.ChildNodes.Count == 0)
        {
            switch (e.Node.Depth)
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
        DataSet MenuTable = new DataSet();
        MenuTable = objBillSysDocBO.GetMasterNodes(txtCompanyID.Text);
        if (MenuTable.Tables.Count > 0)
        {
            foreach (DataRow row in MenuTable.Tables[0].Rows)
            {
                TreeNode newNode = new
                TreeNode(row["SZ_NODE_NAME"].ToString(), row["I_NODE_ID"].ToString());
                newNode.PopulateOnDemand = true;
                newNode.Value = row["I_NODE_ID"].ToString();
                newNode.ToolTip = row["SZ_NODE_NAME"].ToString();
                newNode.ShowCheckBox = true;
                newNode.ToolTip = node.ToolTip + ">>" + row["SZ_NODE_NAME"].ToString() + "(" + row["I_NODE_ID"].ToString() + ")";
                for (int i = 0; i < dsDocuments.Tables[0].Rows.Count; i++)
                {
                    if (row["I_NODE_ID"].ToString().Equals(dsDocuments.Tables[0].Rows[i][1].ToString()))
                    {
                        newNode.Checked = true;
                        string path = node.ToolTip;
                        string[] nodeid = new string[10];
                        nodeid = node.ValuePath.Split('/');
                        for (int j = 0; j < nodeid.Length; j++)
                        {
                            path = path.Replace("(" + nodeid[j] + ")", "");
                        }
                        hfselectedNode.Value = hfselectedNode.Value + path + ">>" + row["SZ_NODE_NAME"].ToString() + "~" + row["I_NODE_ID"].ToString() + ",";
                        ListItem lst = new ListItem();
                        lst.Text = objBillSysDocBO.GetFullPathOfNode(dsDocuments.Tables[0].Rows[i][1].ToString());
                        lst.Value = dsDocuments.Tables[0].Rows[i][1].ToString();
                        lbSelectedDocs.Items.Add(lst);
                    }
                }
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                node.ChildNodes.Add(newNode);
            }
        }
    }

    void FillChildMenu(TreeNode node)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string MenuID = node.Value;
            DataSet ChildMenuTable = new DataSet();
            ChildMenuTable = objBillSysDocBO.GetChildNodes(Convert.ToInt32(MenuID), txtCompanyID.Text);
            if (ChildMenuTable.Tables.Count > 0)
            {
                foreach (DataRow row in ChildMenuTable.Tables[0].Rows)
                {
                    TreeNode newNode = new TreeNode(row["SZ_NODE_NAME"].ToString(), row["I_NODE_ID"].ToString());
                    newNode.PopulateOnDemand = false;
                    newNode.SelectAction = TreeNodeSelectAction.Expand;
                    newNode.ShowCheckBox = true;
                    string str = "";
                    newNode.ToolTip = node.ToolTip + ">>" + row["SZ_NODE_NAME"].ToString() + "(" + row["I_NODE_ID"].ToString() + ")";
                    for (int i = 0; i < dsDocuments.Tables[0].Rows.Count; i++)
                    {
                        if (row["I_NODE_ID"].ToString().Equals(dsDocuments.Tables[0].Rows[i][1].ToString()))
                        {
                            newNode.Checked = true;
                            string path = node.ToolTip;
                            string[] nodeid = new string[10];
                            nodeid = node.ValuePath.Split('/');
                            for (int j = 0; j < nodeid.Length; j++)
                            {
                                path = path.Replace("(" + nodeid[j] + ")", "");
                            }
                            hfselectedNode.Value = hfselectedNode.Value + path + ">>" + row["SZ_NODE_NAME"].ToString() + "~" + row["I_NODE_ID"].ToString() + ",";
                            ListItem lst = new ListItem();
                            lst.Text = objBillSysDocBO.GetFullPathOfNode(dsDocuments.Tables[0].Rows[i][1].ToString());
                            lst.Value = dsDocuments.Tables[0].Rows[i][1].ToString();
                            lbSelectedDocs.Items.Add(lst);
                        }
                    }
                    newNode.Value = row["I_NODE_ID"].ToString();
                    node.ChildNodes.Add(newNode);
                    FillChildMenu(newNode);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //protected void ddlUserRole_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lbSelectedDocs.Items.Clear();
    //    hfselectedNode.Value = "";
    //    dsDocuments = objBillSysDocBO.GetDocumentsAssignedToUserRole(txtCompanyID.Text, ddUserRole.SelectedValue);
    //    tvwmenu.PopulateNodesFromClient = true;
    //    tvwmenu.Nodes.RemoveAt(0);
    //    TreeNode node = new TreeNode("Document Manager", "0");
    //    node.PopulateOnDemand = true;
    //    tvwmenu.Nodes.Add(node);
    //    tvwmenu.ExpandAll();
    //}

    //protected void btnAssign_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddUserRole.SelectedValue.Equals("0"))
    //        {
    //            usrMessage.PutMessage("User role is not selected.");
    //            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
    //            usrMessage.Show();
    //        }
    //        else
    //        {
    //            ArrayList lstDoc = new ArrayList();
    //            string[] Documents = new string[100];
    //            lbSelectedDocs.Items.Clear();
    //            Documents = hfselectedNode.Value.Split(',');
    //            for (int i = 0; i < Documents.Length - 1; i++)
    //            {
    //                DAO_Assign_Doc DAO = new DAO_Assign_Doc();
    //                DAO.SelectedText = Documents[i].Split('~')[0];
    //                DAO.SelectedId = Documents[i].Split('~')[1];
    //                DAO.SelectedRole = ddUserRole.SelectedItem.Text;
    //                DAO.SelectedRoleID = ddUserRole.SelectedItem.Value;
    //                lstDoc.Add(DAO);
    //            }
    //            for (int i = 0; i < lstDoc.Count; i++)
    //            {
    //                DAO_Assign_Doc DAO = new DAO_Assign_Doc();
    //                DAO = (DAO_Assign_Doc)lstDoc[i];
    //                objBillSysDocBO.AssignDocumentsToUserRole(DAO.SelectedId, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, txtCompanyID.Text, DAO.SelectedRoleID);
    //                ListItem lst = new ListItem();
    //                lst.Text = DAO.SelectedText.ToString();
    //                lst.Value = DAO.SelectedId.ToString();
    //                lbSelectedDocs.Items.Add(lst);
    //            }
    //            dsDocuments = objBillSysDocBO.GetDocumentsAssignedToUserRole(txtCompanyID.Text, ddUserRole.SelectedValue);
    //            for (int i = 0; i < dsDocuments.Tables[0].Rows.Count; i++)
    //            {
    //                int flag = 0;
    //                for (int j = 0; j < lstDoc.Count; j++)
    //                {
    //                    DAO_Assign_Doc DAO = new DAO_Assign_Doc();
    //                    DAO = (DAO_Assign_Doc)lstDoc[j];

    //                    if (dsDocuments.Tables[0].Rows[i][1].ToString().Equals(DAO.SelectedId))
    //                    {
    //                        flag = 1;
    //                    }
    //                }
    //                if (flag == 1)
    //                {

    //                }
    //                else
    //                {
    //                    objBillSysDocBO.RemoveUserRoleDoc(ddUserRole.SelectedValue, txtCompanyID.Text, dsDocuments.Tables[0].Rows[i][1].ToString());
    //                }
    //            }
    //            usrMessage.PutMessage("Documents Assigned Successfully!");
    //            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
    //            usrMessage.Show();
    //        }            
    //    }
    //    catch (Exception ex)
    //    {
    //        usrMessage.PutMessage(ex.ToString());
    //        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
    //        usrMessage.Show();
    //    }
    //}

    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList lstDoc = new ArrayList();
        string[] Documents = new string[100];
        string EmailIDs = "";
        lbSelectedEmailIds.Items.Clear();
        Documents = hfEmailId.Value.Split(',');

        try
        {
            for (int i = 0; i < Documents.Length - 1; i++)
            {
                lbSelectedEmailIds.Items.Add(Documents[i]);
                lstDoc.Add(Documents[i]);
                EmailIDs = EmailIDs + Documents[i] + ",";
            }

            if (txtNoOfDays.Text == "")
            {

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "Validation();", true);

            }
            else if (lbSelectedEmailIds.Items.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "Validation1();", true);
            }
            else
            {
                objBillSysDocBO.SaveEmailSettings(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, txtCompanyID.Text, lstDoc, txtNoOfDays.Text);
            }
            usrMessage1.PutMessage("Mail Settings Saved Successfully...");
            usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage1.Show();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList lstDoc = new ArrayList();
        string[] Documents = new string[100];       
        lbSelectedDocs.Items.Clear();
        Documents = hfselectedNode.Value.Split(',');
        objarrlst = (ArrayList)Session["Document_List"];
        try
        {
            for (int i = 0; i < Documents.Length - 1; i++)
            {
                Bil_Sys_Missing_Document objMissDoc = new Bil_Sys_Missing_Document();
                DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                DAO.SelectedText = Documents[i].Split('~')[0];
                DAO.SelectedId = Documents[i].Split('~')[1];
                lstDoc.Add(DAO);

            }
            for (int i = 0; i < lstDoc.Count; i++)
            {

                DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                DAO = (DAO_Assign_Doc)lstDoc[i];
                objBillSysDocBO.SaveMissingDocument(DAO.SelectedId, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, txtCompanyID.Text);
                ListItem lst = new ListItem();
                lst.Text = DAO.SelectedText.ToString();
                lst.Value = DAO.SelectedId.ToString();
                lbSelectedDocs.Items.Add(lst);
            }
            dsDocuments = objBillSysDocBO.GetMissingDocumentsCompanyWise(txtCompanyID.Text);
            for (int i = 0; i < dsDocuments.Tables[0].Rows.Count; i++)
            {
                int flag = 0;
                for (int j = 0; j < lstDoc.Count; j++)
                {
                    DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                    DAO = (DAO_Assign_Doc)lstDoc[j];

                    if (dsDocuments.Tables[0].Rows[i][1].ToString().Equals(DAO.SelectedId))
                    {
                        flag = 1;
                    }
                }
                if (flag == 1)
                {

                }
                else
                {
                    objBillSysDocBO.RemoveMissingDoc(txtCompanyID.Text, dsDocuments.Tables[0].Rows[i][1].ToString(), ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                }
                usrMessage.PutMessage("Documents Saved Successfully...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        hfEmailId.Value="";
        lbSelectedEmailIds.Items.Clear();
        DataSet objds = new DataSet();
        try
        {
            objds = objBillSysDocBO.GetEmailSettingsCompanyWise(txtCompanyID.Text);
            if (objds.Tables.Count > 0)
            {
                for (int i = 0; i < objds.Tables[0].Rows.Count; i++)
                {
                    txtNoOfDays.Text = objds.Tables[0].Rows[0][0].ToString();
                    lbSelectedEmailIds.Items.Add(objds.Tables[0].Rows[i][1].ToString());
                    hfEmailId.Value = hfEmailId.Value + objds.Tables[0].Rows[i][1].ToString() + ",";
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
  
}
