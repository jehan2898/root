using ASP;
using DocumentTypeBO;
using mbs.templatemanager;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class AJAX_Pages_Bill_Sys_ConfigureTemplate : PageBase
{
    Bill_Sys_TransferDocumentBO objBillSysDocBO = new Bill_Sys_TransferDocumentBO(ConfigurationManager.AppSettings["Connection_String"].ToString());
    DataSet dsSpecialityDoc = new DataSet();

    protected void btnDeleteTemplate_click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.lstTemplates.Items.Clear();
            TemplateManager templateManager = new TemplateManager(ConfigurationSettings.AppSettings["Connection_String"].ToString());
            templateManager.DeleteTemplates(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.nfSelectedNodeId.Value);
            DataSet templates = templateManager.GetTemplates(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.lstTemplates.DataSource = templates.Tables[0];
            for (int i = 0; i < templates.Tables[0].Rows.Count; i++)
            {
                ListItem listItem = new ListItem()
                {
                    Text = templates.Tables[0].Rows[i][1].ToString(),
                    Value = templates.Tables[0].Rows[i][2].ToString()
                };
                this.lstTemplates.Items.Add(listItem);
            }
            this.usrMessage.PutMessage("Template deleted.");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
            this.tvwmenu.PopulateNodesFromClient = true;
            this.tvwmenu.Nodes.RemoveAt(0);
            TreeNode treeNode = new TreeNode("Document Manager", "0")
            {
                PopulateOnDemand = true
            };
            this.tvwmenu.Nodes.Add(treeNode);
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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btntest_click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        try
        {
            string str1 = this.lstTemplates.SelectedItem.ToString();
            this.lstTemplates.SelectedValue.ToString();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_string"].ToString());
            SqlCommand sqlCommand = new SqlCommand();
            string[] strArrays = new string[] { "select I_NODE_ID  from MST_NODES where SZ_NODE_TYPE = '", this.lstTemplates.SelectedValue.ToString(), "' and SZ_COMPANY_ID  = '", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString(), "'" };
            sqlCommand.CommandText = string.Concat(strArrays);
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                str = sqlDataReader.GetValue(0).ToString();
            }
            sqlConnection.Close();
            this.txtnodevalue.Text = str;
            this.hfNodeType.Value = str;
            this.txtTemplateName.Text = str1;
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
        this.tvwmenu.PopulateNodesFromClient = true;
        this.tvwmenu.Nodes.RemoveAt(0);
        TreeNode treeNode = new TreeNode("Document Manager", "0")
        {
            PopulateOnDemand = true
        };
        this.tvwmenu.Nodes.Add(treeNode);
        this.tvwmenu.ExpandAll();
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.FileUploadControl.HasFile && this.FileUploadControl.PostedFile.ContentLength < 10240000)
            {
                string fileName = this.FileUploadControl.PostedFile.FileName;
                string str = "";
                str = this.FileUploadControl.FileName;
                TemplateManager templateManager = new TemplateManager(ConfigurationSettings.AppSettings["Connection_String"].ToString());
                DataSet templatePaths = templateManager.GetTemplatePaths();
                string str1 = "";
                if (templatePaths.Tables[0] != null && templatePaths.Tables[1] != null)
                {
                    str1 = string.Concat(templatePaths.Tables[1].Rows[0][0].ToString(), "/", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "/");
                    templatePaths.Tables[0].Rows[0][0].ToString();
                }
                string str2 = templateManager.InsertTemplate(this.txtTemplateName.Text, string.Concat(str1, "/", str), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, this.hfNodeType.Value);
                if (str2 != "inserted")
                {
                    throw new ArgumentException(str2);
                }
                if (!Directory.Exists(str1))
                {
                    Directory.CreateDirectory(str1);
                }
                this.FileUploadControl.SaveAs(string.Concat(str1, str));
                this.lblFileName.Text = string.Concat("Uploaded : ", this.FileUploadControl.FileName);
                this.lstTemplates.Items.Clear();
                DataSet templates = templateManager.GetTemplates(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.lstTemplates.DataSource = templates.Tables[0];
                for (int i = 0; i < templates.Tables[0].Rows.Count; i++)
                {
                    ListItem listItem = new ListItem()
                    {
                        Text = templates.Tables[0].Rows[i][1].ToString(),
                        Value = templates.Tables[0].Rows[i][2].ToString()
                    };
                    this.lstTemplates.Items.Add(listItem);
                }
                this.usrMessage.PutMessage("File uploaded successfully.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
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

    private void FillChildMenu(TreeNode node)
    {
        string value = node.Value;
        DataSet dataSet = new DataSet();
        dataSet = this.objBillSysDocBO.GetChildNodes(Convert.ToInt32(value), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        TemplateManager templateManager = new TemplateManager(ConfigurationSettings.AppSettings["Connection_String"].ToString());
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
                treeNode.Value = row["I_NODE_ID"].ToString();
                if (row["I_NODE_ID"].ToString().Equals(this.txtnodevalue.Text))
                {
                    treeNode.Checked = true;
                }
                node.ChildNodes.Add(treeNode);
                this.FillChildMenu(treeNode);
            }
        }
    }

    public void FillMasterMenu(TreeNode node)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.objBillSysDocBO.GetMasterNodes(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        TemplateManager templateManager = new TemplateManager(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        this.dsSpecialityDoc = templateManager.GetNodes(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
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
                treeNode.SelectAction = TreeNodeSelectAction.Expand;
                if (row["I_NODE_ID"].ToString().Equals(this.txtnodevalue.Text))
                {
                    treeNode.Checked = true;
                }
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
        this.btnupload.Attributes.Add("OnClick", "return CheckTemplateName()");
        this.lstTemplates.Attributes.Add("OnClick", "test()");
        if (!base.IsPostBack)
        {
            this.tvwmenu.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");
            TemplateManager templateManager = new TemplateManager(ConfigurationSettings.AppSettings["Connection_String"].ToString());
            DataSet templates = templateManager.GetTemplates(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.lstTemplates.DataSource = templates.Tables[0];
            for (int i = 0; i < templates.Tables[0].Rows.Count; i++)
            {
                ListItem listItem = new ListItem()
                {
                    Text = templates.Tables[0].Rows[i][1].ToString(),
                    Value = templates.Tables[0].Rows[i][2].ToString()
                };
                this.lstTemplates.Items.Add(listItem);
            }
        }
    }

}
