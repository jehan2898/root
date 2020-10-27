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
using DocumentTypeBO;
public partial class AJAX_Pages_Bill_Sys_Document_Upload_Settings : PageBase
{
    Bill_Sys_DocumentTypeBO objBillSysDocBO = new Bill_Sys_DocumentTypeBO(ConfigurationManager.AppSettings["Connection_String"].ToString());
    DataSet dsSpecialityDoc = new DataSet();
    Bill_Sys_DisplaySpeciality objSpeciality = new Bill_Sys_DisplaySpeciality();
    public AJAX_Pages_Bill_Sys_Document_Upload_Settings()
    {
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!(new Bill_Sys_Doc_Upload_Settings()).SaveUploadDoc(this.txtCompanyID.Text, this.extddlDocSource.Text, this.ddlDocType.SelectedItem.ToString(), this.extddlSpeciality.Text, this.hfselectedNode.Value.ToString()))
            {
                this.usrMessage.PutMessage("Record  not saved.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.usrMessage.PutMessage("Record  saved successfully.");
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

    protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlDocType.SelectedItem.ToString() == "---Select---")
        {
            this.extddlSpeciality.Enabled = false;
            return;
        }
        this.extddlSpeciality.Enabled = true;
        this.extddlSpeciality.Text = "NA";
    }

    protected void extddlDocSource_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.extddlDocSource.Text == "NA")
        {
            this.extddlSpeciality.Text = "NA";
            this.extddlSpeciality.Enabled = false;
            try
            {
                while (this.ddlDocType.Items.Count != 0)
                {
                    this.ddlDocType.Items.RemoveAt(0);
                }
                this.ddlDocType.Enabled = false;
            }
            catch (Exception exception)
            {
            }
            return;
        }
        Bill_Sys_Doc_Upload_Settings billSysDocUploadSetting = new Bill_Sys_Doc_Upload_Settings();
        DataSet dataSet = new DataSet();
        dataSet = billSysDocUploadSetting.getDocType(this.txtCompanyID.Text, this.extddlDocSource.Text);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            this.ddlDocType.DataSource = dataSet;
            this.ddlDocType.DataTextField = "SZ_DOCUMENT_TYPE";
            this.ddlDocType.DataValueField = "I_DOC_UPLOAD_ID";
            this.ddlDocType.DataBind();
            this.ddlDocType.Items.Insert(0, "---Select---");
        }
        this.ddlDocType.Enabled = true;
        this.extddlSpeciality.Text = "NA";
        this.extddlSpeciality.Enabled = false;
    }

    protected void extddlSpeciality_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.extddlSpeciality.Text != "NA")
        {
            this.tvwmenu.PopulateNodesFromClient = true;
            this.tvwmenu.Nodes.RemoveAt(0);
            TreeNode treeNode = new TreeNode("Document Manager", "0");

            treeNode.PopulateOnDemand = true;
            
            this.tvwmenu.Nodes.Add(treeNode);
            this.tvwmenu.ExpandAll();
        }
    }

    private void FillChildMenu(TreeNode node)
    {
        string value = node.Value;
        DataSet dataSet = new DataSet();
        dataSet = this.objBillSysDocBO.GetChildNodes(Convert.ToInt32(value), this.txtCompanyID.Text);
        string str = this.SetNode();
        this.dsSpecialityDoc = this.objBillSysDocBO.GetSecialityDoc(this.txtCompanyID.Text, this.extddlSpeciality.Text);
        if (dataSet.Tables.Count > 0)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                TreeNode treeNode = new TreeNode(row["SZ_NODE_NAME"].ToString(), row["I_NODE_ID"].ToString());

                treeNode.PopulateOnDemand = false;
                treeNode.SelectAction = TreeNodeSelectAction.Expand;
                treeNode.ShowCheckBox = new bool?(true);

                if (str != "" && treeNode.Value == str)
                {
                    treeNode.Checked = true;
                    treeNode.Selected = true;
                }
                treeNode.ToolTip = string.Concat(row["SZ_NODE_NAME"].ToString(), "(", row["I_NODE_ID"].ToString(), ")");
                treeNode.Value = row["I_NODE_ID"].ToString();
                node.ChildNodes.Add(treeNode);
                this.FillChildMenu(treeNode);
            }
        }
    }

    public void FillMasterMenu(TreeNode node)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.objBillSysDocBO.GetMasterNodes(this.txtCompanyID.Text);
        this.dsSpecialityDoc = this.objBillSysDocBO.GetSecialityDoc(this.txtCompanyID.Text, this.extddlSpeciality.Text);
        string str = this.SetNode();
        if (dataSet.Tables.Count > 0)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                TreeNode treeNode = new TreeNode(row["SZ_NODE_NAME"].ToString(), row["I_NODE_ID"].ToString());

                treeNode.PopulateOnDemand = true;
                treeNode.Value = row["I_NODE_ID"].ToString();
                treeNode.ToolTip = row["SZ_NODE_NAME"].ToString();
                treeNode.ShowCheckBox = new bool?(true);

                if (str != "" && treeNode.Value == str)
                {
                    treeNode.Checked = true;
                    treeNode.Select();
                }
                treeNode.ToolTip = string.Concat(row["SZ_NODE_NAME"].ToString(), "(", row["I_NODE_ID"].ToString(), ")");
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
        this.tvwmenu.Attributes.Add("onclick", "check(event)");
        this.extddlDocSource.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.btnSave.Attributes.Add("onclick", "return Validate()");
        if (!this.Page.IsPostBack)
        {
            this.hfselectedNode.Value = "";
            this.extddlSpeciality.Enabled = false;
            this.ddlDocType.Enabled = false;
        }
    }

    public string SetNode()
    {
        string nodeId = "";
        if (this.extddlSpeciality.Text != "NA" && this.extddlDocSource.Text != "NA" && this.ddlDocType.SelectedValue.ToString() != "" && this.ddlDocType.SelectedValue.ToString() != "---Select---")
        {
            Bill_Sys_Doc_Upload_Settings billSysDocUploadSetting = new Bill_Sys_Doc_Upload_Settings();
            nodeId = billSysDocUploadSetting.GetNodeId(this.txtCompanyID.Text, this.extddlDocSource.Text, this.ddlDocType.SelectedItem.ToString(), this.extddlSpeciality.Text);
        }
        return nodeId;
    }
}
