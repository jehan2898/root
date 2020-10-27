
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
public partial class Bill_Sys_DocumentType : PageBase
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

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!this.ddUserRole.SelectedValue.Equals("0"))
            {
                ArrayList arrayLists = new ArrayList();
                string[] strArrays = new string[100];
                this.lbSelectedDocs.Items.Clear();
                strArrays = this.hfselectedNode.Value.Split(new char[] { ',' });
                for (int i = 0; i < (int)strArrays.Length - 1; i++)
                {
                    DAO_Assign_Doc dAOAssignDoc = new DAO_Assign_Doc();
                    string str = strArrays[i];
                    char[] chrArray = new char[] { '~' };
                    dAOAssignDoc.SelectedText = str.Split(chrArray)[0];
                    string str1 = strArrays[i];
                    char[] chrArray1 = new char[] { '~' };
                    dAOAssignDoc.SelectedId = str1.Split(chrArray1)[1];
                    dAOAssignDoc.SelectedRole = this.ddUserRole.SelectedItem.Text;
                    dAOAssignDoc.SelectedRoleID = this.ddUserRole.SelectedItem.Value;
                    arrayLists.Add(dAOAssignDoc);
                }
                for (int j = 0; j < arrayLists.Count; j++)
                {
                    DAO_Assign_Doc item = new DAO_Assign_Doc();
                    item = (DAO_Assign_Doc)arrayLists[j];
                    this.objBillSysDocBO.AssignDocumentsToUserRole(item.SelectedId, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, this.txtCompanyID.Text, item.SelectedRoleID);
                    ListItem listItem = new ListItem()
                    {
                        Text = item.SelectedText.ToString(),
                        Value = item.SelectedId.ToString()
                    };
                    this.lbSelectedDocs.Items.Add(listItem);
                }
                this.dsDocuments = this.objBillSysDocBO.GetDocumentsAssignedToUserRole(this.txtCompanyID.Text, this.ddUserRole.SelectedValue);
                for (int k = 0; k < this.dsDocuments.Tables[0].Rows.Count; k++)
                {
                    int num = 0;
                    for (int l = 0; l < arrayLists.Count; l++)
                    {
                        DAO_Assign_Doc dAOAssignDoc1 = new DAO_Assign_Doc();
                        dAOAssignDoc1 = (DAO_Assign_Doc)arrayLists[l];
                        if (this.dsDocuments.Tables[0].Rows[k][1].ToString().Equals(dAOAssignDoc1.SelectedId))
                        {
                            num = 1;
                        }
                    }
                    if (num != 1)
                    {
                        this.objBillSysDocBO.RemoveUserRoleDoc(this.ddUserRole.SelectedValue, this.txtCompanyID.Text, this.dsDocuments.Tables[0].Rows[k][1].ToString());
                    }
                }
                this.usrMessage.PutMessage("Documents Assigned Successfully!");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.usrMessage.PutMessage("User role is not selected.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
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

    protected void ddlUserRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lbSelectedDocs.Items.Clear();
        this.hfselectedNode.Value = "";
        this.dsDocuments = this.objBillSysDocBO.GetDocumentsAssignedToUserRole(this.txtCompanyID.Text, this.ddUserRole.SelectedValue);
        this.tvwmenu.PopulateNodesFromClient = true;
        this.tvwmenu.Nodes.RemoveAt(0);
        TreeNode treeNode = new TreeNode("Document Manager", "0")
        {
            PopulateOnDemand = true
        };
        this.tvwmenu.Nodes.Add(treeNode);
        this.tvwmenu.ExpandAll();
        int count = this.lbSelectedDocs.Items.Count;
        for (int i = 0; i < count; i++)
        {
            if (this.lbSelectedDocs.Items[i].Value.Equals(""))
            {
                this.lbSelectedDocs.Items.RemoveAt(i);
                i--;
                count--;
            }
        }
    }

    private void FillChildMenu(TreeNode node)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string value = node.Value;
            DataSet dataSet = new DataSet();
            dataSet = this.objBillSysDocBO.GetChildNodes(Convert.ToInt32(value), this.txtCompanyID.Text);
            if (dataSet.Tables.Count > 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    TreeNode treeNode = new TreeNode(row["SZ_NODE_NAME"].ToString(), row["I_NODE_ID"].ToString())
                    {
                        PopulateOnDemand = false,
                        SelectAction = TreeNodeSelectAction.Expand,
                        ShowCheckBox = new bool?(true)
                    };
                    string[] toolTip = new string[] { node.ToolTip, ">>", row["SZ_NODE_NAME"].ToString(), "(", row["I_NODE_ID"].ToString(), ")" };
                    treeNode.ToolTip = string.Concat(toolTip);
                    for (int i = 0; i < this.dsDocuments.Tables[0].Rows.Count; i++)
                    {
                        if (row["I_NODE_ID"].ToString().Equals(this.dsDocuments.Tables[0].Rows[i][1].ToString()))
                        {
                            treeNode.Checked = true;
                            string str = node.ToolTip;
                            string[] strArrays = new string[10];
                            strArrays = node.ValuePath.Split(new char[] { '/' });
                            for (int j = 0; j < (int)strArrays.Length; j++)
                            {
                                str = str.Replace(string.Concat("(", strArrays[j], ")"), "");
                            }
                            HiddenField hiddenField = this.hfselectedNode;
                            string[] value1 = new string[] { this.hfselectedNode.Value, str, ">>", row["SZ_NODE_NAME"].ToString(), "~", row["I_NODE_ID"].ToString(), "," };
                            hiddenField.Value = string.Concat(value1);
                            ListItem listItem = new ListItem()
                            {
                                Text = this.objBillSysDocBO.GetFullPathOfNode(this.dsDocuments.Tables[0].Rows[i][1].ToString()),
                                Value = this.dsDocuments.Tables[0].Rows[i][1].ToString()
                            };
                            this.lbSelectedDocs.Items.Add(listItem);
                        }
                    }
                    treeNode.Value = row["I_NODE_ID"].ToString();
                    node.ChildNodes.Add(treeNode);
                    this.FillChildMenu(treeNode);
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

    public void FillMasterMenu(TreeNode node)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.objBillSysDocBO.GetMasterNodes(this.txtCompanyID.Text);
        if (dataSet.Tables.Count > 0)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                TreeNode treeNode = new TreeNode(row["SZ_NODE_NAME"].ToString(), row["I_NODE_ID"].ToString())
                {
                    PopulateOnDemand = true,
                    Value = row["I_NODE_ID"].ToString(),
                    ToolTip = row["SZ_NODE_NAME"].ToString(),
                    ShowCheckBox = new bool?(true)
                };
                string[] toolTip = new string[] { node.ToolTip, ">>", row["SZ_NODE_NAME"].ToString(), "(", row["I_NODE_ID"].ToString(), ")" };
                treeNode.ToolTip = string.Concat(toolTip);
                for (int i = 0; i < this.dsDocuments.Tables[0].Rows.Count; i++)
                {
                    if (row["I_NODE_ID"].ToString().Equals(this.dsDocuments.Tables[0].Rows[i][1].ToString()))
                    {
                        treeNode.Checked = true;
                        string str = node.ToolTip;
                        string[] strArrays = new string[10];
                        strArrays = node.ValuePath.Split(new char[] { '/' });
                        for (int j = 0; j < (int)strArrays.Length; j++)
                        {
                            str = str.Replace(string.Concat("(", strArrays[j], ")"), "");
                        }
                        HiddenField hiddenField = this.hfselectedNode;
                        string[] value = new string[] { this.hfselectedNode.Value, str, ">>", row["SZ_NODE_NAME"].ToString(), "~", row["I_NODE_ID"].ToString(), "," };
                        hiddenField.Value = string.Concat(value);
                        ListItem listItem = new ListItem()
                        {
                            Text = this.objBillSysDocBO.GetFullPathOfNode(this.dsDocuments.Tables[0].Rows[i][1].ToString()),
                            Value = this.dsDocuments.Tables[0].Rows[i][1].ToString()
                        };
                        this.lbSelectedDocs.Items.Add(listItem);
                    }
                }
                treeNode.SelectAction = TreeNodeSelectAction.Expand;
                node.ChildNodes.Add(treeNode);
            }
        }
    }

    public void Node_Populate(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.ChildNodes.Count == 0)
        {
            switch (e.Node.Depth)
            {
                case 0:
                    {
                        this.FillMasterMenu(e.Node);
                        return;
                    }
                case 1:
                    {
                        this.FillChildMenu(e.Node);
                        return;
                    }
                case 2:
                    {
                        this.FillChildMenu(e.Node);
                        return;
                    }
                case 3:
                    {
                        this.FillChildMenu(e.Node);
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.tvwmenu.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!base.IsPostBack)
            {
                this.txtUserID.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                DataSet userRole = this.objBillSysDocBO.GetUserRole(this.txtCompanyID.Text);
                ListItem listItem = new ListItem()
                {
                    Text = "---Select---",
                    Value = "0"
                };
                this.ddUserRole.Items.Add(listItem);
                for (int i = 0; i < userRole.Tables[0].Rows.Count; i++)
                {
                    ListItem listItem1 = new ListItem()
                    {
                        Text = userRole.Tables[0].Rows[i][1].ToString(),
                        Value = userRole.Tables[0].Rows[i][0].ToString()
                    };
                    this.ddUserRole.Items.Add(listItem1);
                }
                this.lbSelectedDocs.Items.Clear();
            }
            this.dsDocuments = this.objBillSysDocBO.GetDocumentsAssignedToUserRole(this.txtCompanyID.Text, this.ddUserRole.SelectedValue);
            this.tvwmenu.Visible = true;
            this.tvwmenu.ExpandAll();
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
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_DocumentType.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
