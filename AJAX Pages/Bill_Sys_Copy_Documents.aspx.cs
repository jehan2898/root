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
using System.Collections;
using System.IO;
using DevExpress.Web;

public partial class AJAX_Pages_Bill_Sys_Copy_Documents : PageBase
{
    Bill_Sys_DocumentTypeBO objBillSysDocBO = new Bill_Sys_DocumentTypeBO(ConfigurationManager.AppSettings["Connection_String"].ToString());
    DataSet dsSpecialityDoc = new DataSet();
    Bill_Sys_DisplaySpeciality objSpeciality = new Bill_Sys_DisplaySpeciality();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtUserName.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        txtCompanyIDCopy.Text = Request.QueryString["CompanyID"].ToString();
        txtCaseID.Text = Request.QueryString["CaseID"].ToString();
        btnCopyDoc.Attributes.Add("OnClick", "callforSearch();");


        if (!Page.IsPostBack)
        {
            hfselectedNode.Value = "";
        }

        if (hdnSearch.Value != "true")
        {
            BindGrid();

        }
        else
            hdnSearch.Value = "";

    }

    private void Page_PreRender(object sender, EventArgs e)
    {
        tvwmenu.Attributes.Add("OnClick", "CheckSelect()");
    }


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
        MenuTable = objSpeciality.GetMasterNodes(txtCompanyID.Text, txtNewCaseID.Text);
        //dsSpecialityDoc = objBillSysDocBO.GetSecialityDoc(txtCompanyID.Text, "");
        string szNodeId = "";
        if (MenuTable.Tables.Count > 0)
        {
            foreach (DataRow row in MenuTable.Tables[0].Rows)
            {
                TreeNode newNode = new TreeNode(row["SZ_NODE_NAME"].ToString(), row["I_NODE_ID"].ToString());
                
                newNode.PopulateOnDemand = true;
                newNode.Value = row["I_NODE_ID"].ToString();
                newNode.ToolTip = row["SZ_NODE_NAME"].ToString();
                newNode.ShowCheckBox = true;

                if (szNodeId != "")
                {
                    if (newNode.Value == szNodeId)
                    {
                        newNode.Checked = true;
                        newNode.Select();
                    }
                }
                newNode.ToolTip = row["SZ_NODE_NAME"].ToString() + "(" + row["I_NODE_ID"].ToString() + ")";
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                node.ChildNodes.Add(newNode);
            }
        }
    }

    void FillChildMenu(TreeNode node)
    {
        string MenuID = node.Value;
        DataSet ChildMenuTable = new DataSet();
        ChildMenuTable = objSpeciality.GetChildNodes(Convert.ToInt32(MenuID), txtCompanyID.Text, txtNewCaseID.Text);
        string szNodeId = "";
        //string szNodeId = "109";
        //dsSpecialityDoc = objBillSysDocBO.GetSecialityDoc(txtCompanyID.Text, "");
        if (ChildMenuTable.Tables.Count > 0)
        {
            foreach (DataRow row in ChildMenuTable.Tables[0].Rows)
            {
                TreeNode newNode = new TreeNode(row["SZ_NODE_NAME"].ToString(), row["I_NODE_ID"].ToString());
                newNode.PopulateOnDemand = false;
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                newNode.ShowCheckBox = true;
                if (szNodeId != "")
                {
                    if (newNode.Value == szNodeId)
                    {
                        newNode.Checked = true;
                        newNode.Selected = true;
                    }

                }
                string str = "";
                newNode.ToolTip = row["SZ_NODE_NAME"].ToString() + "(" + row["I_NODE_ID"].ToString() + ")";
                newNode.Value = row["I_NODE_ID"].ToString();
                node.ChildNodes.Add(newNode);
                FillChildMenu(newNode);
            }
        }
    }

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataSet ds = new DataSet();
            ds = objSpeciality.GetCaseDocuments(txtCompanyIDCopy.Text, txtCaseID.Text);
            txtNewCaseID.Text = objSpeciality.GetCopiedCaseID(txtCompanyIDCopy.Text, txtCompanyID.Text, Convert.ToInt32(txtCaseID.Text));
            grdDocuments.DataSource = ds;
            grdDocuments.DataBind();

            tvwmenu.PopulateNodesFromClient = true;
            tvwmenu.Nodes.RemoveAt(0);
            TreeNode node = new TreeNode("Document Manager", "0");
            if (tvwmenu.Nodes.Count == 0)
            {
                node.PopulateOnDemand = true;
            }

            tvwmenu.Nodes.Add(node);
            tvwmenu.ExpandAll();
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

    protected void btnCopyDoc_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string targetPath = "";
        string CopyPath = "";
        string SourcePath = "";
        string FileName = "";
        int flag = 0;
        string BasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
        try
        {
            if (hfselectedNode.Value != "0" && hfselectedNode.Value != "")
            {
                for (int i = 0; i < grdDocuments.VisibleRowCount; i++)
                {
                    GridViewDataColumn c = (GridViewDataColumn)grdDocuments.Columns[0];
                    CheckBox checkBox = (CheckBox)grdDocuments.FindRowCellTemplateControl(i, c, "chkall");
                    if (checkBox.Checked)
                    {
                        flag = 1;
                        CopyPath = grdDocuments.GetRowValues(i, "FilePath").ToString();
                        FileName = grdDocuments.GetRowValues(i, "Filename").ToString();
                        targetPath = objSpeciality.CopyDocuments(txtCompanyID.Text, txtNewCaseID.Text, Convert.ToInt32(hfselectedNode.Value.ToString()), FileName, txtUserName.Text);
                        SourcePath = BasePath + CopyPath + FileName;
                        targetPath = BasePath + targetPath;
                        if (!System.IO.Directory.Exists(targetPath))
                        {
                            System.IO.Directory.CreateDirectory(targetPath);
                        }
                        targetPath = targetPath + "\\" + FileName;
                        targetPath = targetPath.Replace("/", "\\");
                        SourcePath = SourcePath.Replace("/", "\\");
                        if (File.Exists(SourcePath))
                        {
                            System.IO.File.Copy(SourcePath, targetPath, true);
                            usrMessage1.PutMessage("Documents copied Successfully..");
                            usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                            usrMessage1.Show();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('File is physically not present.');", true);
                        }
                    }

                }
                if (flag == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Please Select Checkbox');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Please Select  Tree Node');", true);
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


            //Method End
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
    }


}