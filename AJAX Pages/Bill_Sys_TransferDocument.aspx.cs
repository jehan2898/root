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

public partial class AJAX_Pages_Bill_Sys_TransferDocument : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    string[] listButton;
    Bill_Sys_TransferDocumentBO objBillSysDocBO = new Bill_Sys_TransferDocumentBO(ConfigurationManager.AppSettings["Connection_String"].ToString());
    DataSet dsSpecialityDoc = new DataSet();
    Bill_Sys_DisplaySpeciality objSpeciality = new Bill_Sys_DisplaySpeciality();

    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            tvwmenu.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");
            btnAssign.Attributes.Add("onclick", "order()");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {
                Session["ALL_SPEC"] = null;
                txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;

                //load speciality in the dropdownlist.
                DataSet dsSpeciality = objSpeciality.GetSpecialityList(txtCompanyID.Text);
                ListItem list2 = new ListItem();
                ListItem list3 = new ListItem();
                list2.Text = "---Select---";
                list2.Value = "0";
                list3.Text = "ALL";
                list3.Value = "1";

                ddlSpeciality.Items.Add(list2);
                ddlSpeciality.Items.Add(list3);
                for (int i = 0; i < dsSpeciality.Tables[0].Rows.Count; i++)
                {
                    ListItem list = new ListItem();
                    list.Text = dsSpeciality.Tables[0].Rows[i][1].ToString();
                    list.Value = dsSpeciality.Tables[0].Rows[i][0].ToString();
                    ddlSpeciality.Items.Add(list);
                }
                hfselectedNodeinListbox.Value = "";

                //Load All the documents in the session, which are available in the database.
                DataSet ds = new DataSet();
                ds = objBillSysDocBO.GetAllSecialityDoc(txtCompanyID.Text);
                ArrayList lstDoc = new ArrayList();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                    DAO.SelectedId = ds.Tables[0].Rows[i][0].ToString();
                    DAO.SelectedText = objBillSysDocBO.GetFullPathOfNode(ds.Tables[0].Rows[i][0].ToString());
                    DAO.SelectedSpeciality = objBillSysDocBO.GetSpecialityNameUsingId(ds.Tables[0].Rows[i][1].ToString());
                    DAO.SelectedSpecialityID = ds.Tables[0].Rows[i][1].ToString();
                    DAO.ORDER = Convert.ToInt32(ds.Tables[0].Rows[i][2]);
                    DAO.REQUIRED_MULTIPLE = Convert.ToBoolean(ds.Tables[0].Rows[i][3]);
                    lstDoc.Add(DAO);
                }
                Session["SelectedDocList"] = lstDoc;
            }

            //load Listbox on the Step 1 of the wizard. if selected speciality is not '---Select---'
            if (!ddlSpeciality.SelectedValue.Equals("0") && !ddlSpeciality.SelectedValue.Equals("1"))
            {
                string a= hfselectedNode.Value;
                tvwmenu.Visible = true;
                DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                ArrayList lstDoc = new ArrayList();
                lstDoc = (ArrayList)Session["SelectedDocList"];
                lbSelectedDocs.Items.Clear();
                if (lstDoc != null)
                {
                   

                    for (int i = 0; i < lstDoc.Count; i++)
                    {   
                            int iFlag=0;
                            DAO = (DAO_Assign_Doc)lstDoc[i];
                            ListItem lst = new ListItem();

                            for (int cntRows = 0; cntRows < lbSelectedDocs.Items.Count; cntRows++)
                            { 
                                string[] arrNodteId =lbSelectedDocs.Items[cntRows].Value.ToString().Split('~');
                                string szNodeId=arrNodteId[1].ToString();
                                if (szNodeId == DAO.SelectedId.ToString())
                                {
                                    iFlag = 1;
                                    break;
                                }

                            }
                            if (iFlag != 1)
                            {
                                
                                lst.Text = DAO.SelectedText;
                                lst.Value = DAO.SelectedSpecialityID + "~" + DAO.SelectedId;
                                if (DAO.SelectedSpecialityID.Equals(ddlSpeciality.SelectedValue))
                                {
                                    lbSelectedDocs.Items.Add(lst);
                                }
                            }
                    }
                }
            }
            else if (ddlSpeciality.SelectedValue.Equals("1") )
            {
               

            }
            else
            {
                //load treeview
                tvwmenu.Visible = true;
                tvwmenu.ExpandAll();
                tvwmenu.ShowExpandCollapse = true;
            }
           // Label3.Text = "";
            //lblmsg.Text = "";

            //load treeview and listbox on the step 1 of the wizard if user clicks on 'Previous' button on the step 2 of the wizard.
            if (Wizard1.ActiveStepIndex == 1)
            {
                if (Session["SelectedDocList"] != null)
                {
                    hfselectedNode.Value = "";
                    dsSpecialityDoc = objBillSysDocBO.GetSecialityDoc(txtCompanyID.Text, ddlSpeciality.SelectedValue);
                    for (int i = 0; i < dsSpecialityDoc.Tables[0].Rows.Count; i++)
                    {
                        ListItem list = new ListItem();
                        list.Text = "";
                        list.Value = "";
                        lbSelectedDocs.Items.Add(list);
                    }
                    //tvwmenu.PopulateNodesFromClient = true;
                    //tvwmenu.Nodes.RemoveAt(0);
                    //TreeNode node = new TreeNode("Document Manager", "0");
                    //node.PopulateOnDemand = true;
                    //tvwmenu.Nodes.Add(node);
                    //tvwmenu.ExpandAll();
                    int count = lbSelectedDocs.Items.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (lbSelectedDocs.Items[i].Value.Equals(""))
                        {
                            lbSelectedDocs.Items.RemoveAt(i);
                            i--;
                            count--;
                        }
                        else
                        {
                            
                        }
                    }
                }                
            }
           // if (IsPostBack)
            {
                DataSet dsDoc = new DataSet();
                dsDoc = objBillSysDocBO.GetAllSecialityDoc(txtCompanyID.Text);
                lbAllAssignedDoc.Items.Clear();
                for (int i = 0; i < dsDoc.Tables[0].Rows.Count; i++)
                {
                    DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                    DAO.SelectedId = dsDoc.Tables[0].Rows[i][0].ToString();
                    DAO.SelectedText = objBillSysDocBO.GetFullPathOfNode(dsDoc.Tables[0].Rows[i][0].ToString());
                    DAO.SelectedSpeciality = objBillSysDocBO.GetSpecialityNameUsingId(dsDoc.Tables[0].Rows[i][1].ToString());
                    DAO.SelectedSpecialityID = dsDoc.Tables[0].Rows[i][1].ToString();
                    DAO.ORDER = Convert.ToInt32(dsDoc.Tables[0].Rows[i][2]);
                    DAO.REQUIRED_MULTIPLE = Convert.ToBoolean(dsDoc.Tables[0].Rows[i][3]);
                    ListItem lst = new ListItem();
                    lst.Text = DAO.SelectedSpeciality.ToString() + DAO.SelectedText.ToString();
                    lst.Value = DAO.SelectedId.ToString();
                    lbAllAssignedDoc.Items.Add(lst);

                }
            }
        }
        catch (Exception ex)
        {
            this.Session["SendPatientToDoctor"] = false;
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
        Bill_Sys_GetDocumentTypeTree objGetProc = new Bill_Sys_GetDocumentTypeTree();
        DataSet MenuTable = new DataSet();
        MenuTable = objBillSysDocBO.GetMasterNodes(txtCompanyID.Text);
        if (Session["ALL_SPEC"] != null)
        {
            dsSpecialityDoc = objGetProc.GeALLtNodeId(txtCompanyID.Text);
        }
        else
        {
            dsSpecialityDoc = objBillSysDocBO.GetSecialityDoc(txtCompanyID.Text, ddlSpeciality.SelectedValue);
        }
        if (MenuTable.Tables.Count > 0)
        {
            foreach (DataRow row in MenuTable.Tables[0].Rows)
            {
                TreeNode newNode = new TreeNode(row["SZ_NODE_NAME"].ToString(), row["I_NODE_ID"].ToString());
                newNode.PopulateOnDemand = true;
                newNode.Value = row["I_NODE_ID"].ToString();
                newNode.ToolTip = row["SZ_NODE_NAME"].ToString();
                newNode.ShowCheckBox = true;
               
                newNode.ToolTip = node.ToolTip + ">>" + row["SZ_NODE_NAME"].ToString() + "(" + row["I_NODE_ID"].ToString() + ")";
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                for (int i = 0; i < dsSpecialityDoc.Tables[0].Rows.Count; i++)
                {
                    if (row["I_NODE_ID"].ToString().Equals(dsSpecialityDoc.Tables[0].Rows[i][0].ToString()))
                    {
                        newNode.Checked = true;
                        string path = node.ToolTip;
                        string[] nodeid = new string[10];
                        nodeid = node.ValuePath.Split('/');
                        for (int j = 0; j < nodeid.Length; j++)
                        {
                            path = path.Replace("(" + nodeid[j] + ")", "");
                        }
                        if (Session["ALL_SPEC"] == null)
                        {

                            hfselectedNode.Value = hfselectedNode.Value + path + ">>" + row["SZ_NODE_NAME"].ToString() + "~" + dsSpecialityDoc.Tables[0].Rows[i][1].ToString() + "~" + row["I_NODE_ID"].ToString() + ",";
                        }
                        hfOrder.Value = hfOrder.Value + row["I_NODE_ID"].ToString() + "~" + dsSpecialityDoc.Tables[0].Rows[i][2].ToString() + ",";
                        ListItem list1 = new ListItem();
                        list1.Text = path + ">>" + row["SZ_NODE_NAME"].ToString();
                        list1.Value = dsSpecialityDoc.Tables[0].Rows[i][1].ToString() + "~" + row["I_NODE_ID"].ToString();
                        if (Session["ALL_SPEC"] != null)
                        {
                            lbSelectedDocs.Items.Add(list1);
                            
                        }
                       // lbSelectedDocs.Items.Insert(Convert.ToInt32(dsSpecialityDoc.Tables[0].Rows[i][2]), list1);
                        //lbSelectedDocs.Items.RemoveAt(Convert.ToInt32(dsSpecialityDoc.Tables[0].Rows[i][2]) + 1);
                        DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                        DAO.SelectedId = row["I_NODE_ID"].ToString();
                        DAO.SelectedText = path + ">>" + row["SZ_NODE_NAME"].ToString();
                        DAO.SelectedSpeciality = ddlSpeciality.SelectedItem.Text;
                        DAO.SelectedSpecialityID = dsSpecialityDoc.Tables[0].Rows[i][1].ToString();
                        DAO.ORDER = Convert.ToInt32(dsSpecialityDoc.Tables[0].Rows[i][2]);
                        DAO.REQUIRED_MULTIPLE = Convert.ToBoolean(dsSpecialityDoc.Tables[0].Rows[i][3]);
                        ArrayList lstDoc = new ArrayList();
                        if (Session["SelectedDocList"] == null)
                        {
                            lstDoc.Add(DAO);
                            Session["SelectedDocList"] = lstDoc;
                        }
                        else
                        {
                            lstDoc = (ArrayList)Session["SelectedDocList"];
                            lstDoc.Add(DAO);
                            Session["SelectedDocList"] = lstDoc;
                        }
                        if (Session["SelectedDocList"] != null)
                        {
                            ArrayList lstDoc1 = new ArrayList();
                            lstDoc1 = (ArrayList)Session["SelectedDocList"];
                            for (int k = 0; k < lstDoc1.Count; k++)
                            {
                                DAO_Assign_Doc dao = new DAO_Assign_Doc();
                                dao = (DAO_Assign_Doc)lstDoc[k];
                                if (dao.SelectedId.Equals(row["I_NODE_ID"].ToString()) && dao.SelectedSpecialityID.Equals(ddlSpeciality.SelectedValue.ToString()))
                                {
                                    newNode.Checked = true;
                                }
                            }
                        }
                    }
                    else if (Session["SelectedDocList"] != null)
                    {
                        ArrayList lstDoc = new ArrayList();
                        lstDoc = (ArrayList)Session["SelectedDocList"];
                        for (int k = 0; k < lstDoc.Count; k++)
                        {
                            DAO_Assign_Doc dao = new DAO_Assign_Doc();
                            dao = (DAO_Assign_Doc)lstDoc[k];
                            if (dao.SelectedId.Equals(row["I_NODE_ID"].ToString()) && dao.SelectedSpecialityID.Equals(ddlSpeciality.SelectedValue.ToString()))
                            {
                                newNode.Checked = true;
                            }
                        }
                    }
                }
                node.ChildNodes.Add(newNode);
            }
        }
    }

    void FillChildMenu(TreeNode node)
    {
        string MenuID = node.Value;
        Bill_Sys_GetDocumentTypeTree objGetProc = new Bill_Sys_GetDocumentTypeTree();
        DataSet ChildMenuTable = new DataSet();
        ChildMenuTable = objBillSysDocBO.GetChildNodes(Convert.ToInt32(MenuID), txtCompanyID.Text);
        if (Session["ALL_SPEC"] != null)
        {
            dsSpecialityDoc = objGetProc.GeALLtNodeId(txtCompanyID.Text);
        }
        else
        {
            dsSpecialityDoc = objBillSysDocBO.GetSecialityDoc(txtCompanyID.Text, ddlSpeciality.SelectedValue);
        }
        if (ChildMenuTable.Tables.Count > 0)
        {
            foreach (DataRow row in ChildMenuTable.Tables[0].Rows)
            {
                TreeNode newNode = new TreeNode(row["SZ_NODE_NAME"].ToString(), row["I_NODE_ID"].ToString());
                newNode.PopulateOnDemand = false;
                newNode.SelectAction = TreeNodeSelectAction.Expand;
               
            
                string str = "";
                newNode.ToolTip = node.ToolTip + ">>" + row["SZ_NODE_NAME"].ToString() + "(" + row["I_NODE_ID"].ToString() + ")";
                newNode.Value = row["I_NODE_ID"].ToString();
                if (Session["ALL_SPEC"] != null)
                {
                    if (Session["ALL_SPEC"] != null && newNode.ToolTip.ToString().Contains(Session["ALL_SPEC"].ToString()))
                    {
                        newNode.ShowCheckBox = false;
                    }
                    else
                    {
                        newNode.ShowCheckBox = true;
                    }
                }
                else
                {
                    newNode.ShowCheckBox = true;
                }
                for (int i = 0; i < dsSpecialityDoc.Tables[0].Rows.Count; i++)
                {
                    if (row["I_NODE_ID"].ToString().Equals(dsSpecialityDoc.Tables[0].Rows[i][0].ToString()))
                    {
                        newNode.Checked = true;
                        string path = node.ToolTip;
                        string[] nodeid = new string[10];
                        nodeid = node.ValuePath.Split('/');
                        for (int j = 0; j < nodeid.Length; j++)
                        {
                            path = path.Replace("(" + nodeid[j] + ")", "");
                        }
                        if (Session["ALL_SPEC"] == null)
                        {
                            hfselectedNode.Value = hfselectedNode.Value + path + ">>" + row["SZ_NODE_NAME"].ToString() + "~" + dsSpecialityDoc.Tables[0].Rows[i][1].ToString() + "~" + row["I_NODE_ID"].ToString() + ",";
                        }
                        ListItem list1 = new ListItem();
                        list1.Text = path + ">>" + row["SZ_NODE_NAME"].ToString();
                        list1.Value = dsSpecialityDoc.Tables[0].Rows[i][1].ToString() + "~" + row["I_NODE_ID"].ToString();
                    //   lbSelectedDocs.Items.Insert(0 ,list1);
                      // lbSelectedDocs.Items.RemoveAt(Convert.ToInt32(dsSpecialityDoc.Tables[0].Rows[i][2]) + 1);
                        hfOrder.Value = hfOrder.Value + row["I_NODE_ID"].ToString() + "~" + dsSpecialityDoc.Tables[0].Rows[i][2].ToString() + ",";
                        DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                        DAO.SelectedId = row["I_NODE_ID"].ToString();
                        DAO.SelectedText = path + ">>" + row["SZ_NODE_NAME"].ToString();
                        DAO.SelectedSpeciality = ddlSpeciality.SelectedItem.Text;
                        DAO.SelectedSpecialityID = dsSpecialityDoc.Tables[0].Rows[i][1].ToString();
                        DAO.ORDER = Convert.ToInt32(dsSpecialityDoc.Tables[0].Rows[i][2]);
                        DAO.REQUIRED_MULTIPLE = Convert.ToBoolean(dsSpecialityDoc.Tables[0].Rows[i][3]);
                        ArrayList lstDoc = new ArrayList();

                        if (Session["SelectedDocList"] == null)
                        {
                            lstDoc.Add(DAO);
                            Session["SelectedDocList"] = lstDoc;
                        }
                        else
                        {
                            lstDoc = (ArrayList)Session["SelectedDocList"];
                            Session["SelectedDocList"] = lstDoc;
                            int flag = 0;
                            for (int l = 0; l < lstDoc.Count; l++)
                            {
                                DAO_Assign_Doc DAO1 = new DAO_Assign_Doc();
                                DAO1 = (DAO_Assign_Doc)lstDoc[l];
                                if (DAO1.SelectedId.Equals(DAO.SelectedId) && DAO1.SelectedSpecialityID.Equals(DAO.SelectedSpecialityID))
                                {
                                    flag = 1;
                                    break;
                                }
                            }
                            if (flag == 0)
                            {
                                lstDoc.Add(DAO);
                            }
                        }
                        if (Session["SelectedDocList"] != null)
                        {
                            ArrayList lstDoc1 = new ArrayList();
                            lstDoc1 = (ArrayList)Session["SelectedDocList"];
                            for (int k = 0; k < lstDoc1.Count; k++)
                            {
                                DAO_Assign_Doc dao = new DAO_Assign_Doc();
                                dao = (DAO_Assign_Doc)lstDoc[k];
                                if (dao.SelectedId.Equals(row["I_NODE_ID"].ToString()) && dao.SelectedSpecialityID.Equals(ddlSpeciality.SelectedValue.ToString()))
                                {
                                    newNode.Checked = true;
                                }
                            }
                        }

                    }
                    else if (Session["SelectedDocList"] != null)
                    {
                        ArrayList lstDoc = new ArrayList();
                        lstDoc = (ArrayList)Session["SelectedDocList"];
                        for (int k = 0; k < lstDoc.Count; k++)
                        {
                            DAO_Assign_Doc dao = new DAO_Assign_Doc();
                            dao = (DAO_Assign_Doc)lstDoc[k];
                            if (dao.SelectedId.Equals(row["I_NODE_ID"].ToString()) && dao.SelectedSpecialityID.Equals(ddlSpeciality.SelectedValue.ToString()))
                            {
                                newNode.Checked = true;
                            }
                        }
                    }
                }
                node.ChildNodes.Add(newNode);
                FillChildMenu(newNode);
            }
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (ddlSpeciality.SelectedValue.Equals("0"))
            {
                usrMessage.PutMessage("Speciality is not selected.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            } if (ddlSpeciality.SelectedValue.Equals("1"))
            {
                 ArrayList lstDoc = (ArrayList)Session["SelectedDocList"];
                if (lstDoc == null)
                {
                    lstDoc = new ArrayList();
                }
                string[] Documents = new string[100];
                Documents = hfselectedNode.Value.Split(',');
                lbSelectedDocs.Items.Clear();
                ArrayList lstRemoved = new ArrayList();
                for (int i = 0; i < Documents.Length - 1; i++)
                {
                    for (int k = 0; k < ddlSpeciality.Items.Count; k++)
                    {
                        if (ddlSpeciality.Items[k].Text != "0" && ddlSpeciality.Items[k].Text != "1")
                        {
                                DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                                DAO.SelectedText = Documents[i].Split('~')[0];
                                DAO.SelectedId = Documents[i].Split('~')[2];
                                DAO.SelectedSpeciality = ddlSpeciality.Items[k].Text;
                                DAO.SelectedSpecialityID = ddlSpeciality.Items[k].Value;
                                DAO.ORDER = i;
                                for (int j = 0; j < lstDoc.Count; j++)
                                {
                                    DAO_Assign_Doc DAO1 = new DAO_Assign_Doc();
                                    DAO1 = (DAO_Assign_Doc)lstDoc[j];
                                    if (DAO1.SelectedSpecialityID.Equals(DAO.SelectedSpecialityID))
                                    {
                                        lstDoc.Remove(DAO1);
                                        lstRemoved.Add(DAO1);
                                        j--;
                                    }
                                }
                        }
                }
             }


             if (Documents.Length - 1 == 0)
             {
                 for (int j = 0; j < lstDoc.Count; j++)
                 {
                     DAO_Assign_Doc DAO1 = new DAO_Assign_Doc();
                     DAO1 = (DAO_Assign_Doc)lstDoc[j];
                     if (ddlSpeciality.SelectedValue.Equals(DAO1.SelectedSpecialityID))
                     {
                         lstDoc.Remove(DAO1);
                         lstRemoved.Add(DAO1);
                         j--;
                     }
                 }
             }

             for (int i = 0; i < Documents.Length - 1; i++)
             {
                 ListItem list = new ListItem();
                 list.Text = "";
                 list.Value = "";
                 if (!Documents[i].Split('~')[0].Equals(""))
                 {
                     lbSelectedDocs.Items.Add(list);
                 }
             }
             hfselectedNode.Value = "";

             ArrayList lstDoc1 = new ArrayList();
             DataSet ds = new DataSet();
             ds = objBillSysDocBO.GetAllSecialityDoc(txtCompanyID.Text);
             for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
             {
                 DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                 DAO.SelectedId = ds.Tables[0].Rows[i][0].ToString();
                 DAO.SelectedText = objBillSysDocBO.GetFullPathOfNode(ds.Tables[0].Rows[i][0].ToString());
                 DAO.SelectedSpeciality = objBillSysDocBO.GetSpecialityNameUsingId(ds.Tables[0].Rows[i][1].ToString());
                 DAO.SelectedSpecialityID = ds.Tables[0].Rows[i][1].ToString();
                 DAO.ORDER = Convert.ToInt32(ds.Tables[0].Rows[i][2]);
                 DAO.REQUIRED_MULTIPLE = Convert.ToBoolean(ds.Tables[0].Rows[i][3]);
                 lstDoc1.Add(DAO);
             }

             for (int i = 0; i < Documents.Length - 1; i++)
             {
                 for (int iCount = 2; iCount < ddlSpeciality.Items.Count; iCount++)
                 {

                     ListItem list = new ListItem();
                     if (!Documents[i].Split('~')[0].Equals(""))
                     {
                         list.Text = Documents[i].Split('~')[0];
                         list.Value = Documents[i].Split('~')[1] + "~" + Documents[i].Split('~')[2];
                         lbSelectedDocs.Items.Insert(i, list);
                         lbSelectedDocs.Items.RemoveAt(i + 1);
                         DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                         DAO.SelectedText = Documents[i].Split('~')[0];
                         DAO.SelectedId = Documents[i].Split('~')[2];
                         DAO.SelectedSpeciality = ddlSpeciality.Items[iCount].Text;
                         DAO.SelectedSpecialityID = ddlSpeciality.Items[iCount].Value;
                         DAO.ORDER = i;
                         int flag = 0;
                         for (int j = 0; j < lstDoc1.Count; j++)
                         {
                             DAO_Assign_Doc DAO1 = new DAO_Assign_Doc();
                             DAO1 = (DAO_Assign_Doc)lstDoc1[j];
                             if (DAO.SelectedSpecialityID.Equals(DAO1.SelectedSpecialityID) && DAO.SelectedId.Equals(DAO1.SelectedId))
                             {
                                 DAO.REQUIRED_MULTIPLE = DAO1.REQUIRED_MULTIPLE;
                                 flag = 1;
                                 int ins = 0;
                                 for (int k = 0; k < lstDoc.Count; k++)
                                 {
                                     DAO_Assign_Doc DAODocInSession = new DAO_Assign_Doc();
                                     DAODocInSession = (DAO_Assign_Doc)lstDoc[k];
                                     if (DAO.SelectedSpecialityID.Equals(DAODocInSession.SelectedSpecialityID) && DAO.SelectedId.Equals(DAODocInSession.SelectedId))
                                     {
                                         ins = 1;
                                     }
                                     else
                                     {
                                         ins = 0;
                                     }
                                 }
                                 if (ins == 0)
                                 {
                                     lstDoc.Add(DAO);
                                     lstRemoved.Remove(DAO);
                                     hfselectedNode.Value = hfselectedNode.Value + list.Text + "~" + list.Value + ",";
                                 }
                                 break;
                             }

                         }
                         if (flag == 0)
                         {
                             lstDoc.Add(DAO);
                             hfselectedNode.Value = hfselectedNode.Value + list.Text + "~" + list.Value + ",";
                         }
                     }
                 }
             }
             Session["SelectedDocList"] = lstDoc;
             Session["RemovedDoc"] = lstRemoved;
             usrMessage.PutMessage("Documents Assigned Successfully!");
             usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
             usrMessage.Show();
             //Label3.Text = "Documents Assigned


        
           }
           else
            {
                ArrayList lstDoc = (ArrayList)Session["SelectedDocList"];
                if (lstDoc == null)
                {
                    lstDoc = new ArrayList();
                }
                string[] Documents = new string[100];
                Documents = hfselectedNode.Value.Split(',');
                lbSelectedDocs.Items.Clear();
                ArrayList lstRemoved = new ArrayList();
                for (int i = 0; i < Documents.Length - 1; i++)
                {
                    DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                    DAO.SelectedText = Documents[i].Split('~')[0];
                    DAO.SelectedId = Documents[i].Split('~')[2];
                    DAO.SelectedSpeciality = ddlSpeciality.SelectedItem.Text;
                    DAO.SelectedSpecialityID = ddlSpeciality.SelectedItem.Value;
                    DAO.ORDER = i;
                    for (int j = 0; j < lstDoc.Count; j++)
                    {
                        DAO_Assign_Doc DAO1 = new DAO_Assign_Doc();
                        DAO1 = (DAO_Assign_Doc)lstDoc[j];
                        if (DAO1.SelectedSpecialityID.Equals(DAO.SelectedSpecialityID))
                        {
                            lstDoc.Remove(DAO1);
                            lstRemoved.Add(DAO1);
                            j--;
                        }
                    }
                }
                if (Documents.Length - 1 == 0)
                {
                    for (int j = 0; j < lstDoc.Count; j++)
                    {
                        DAO_Assign_Doc DAO1 = new DAO_Assign_Doc();
                        DAO1 = (DAO_Assign_Doc)lstDoc[j];
                        if (ddlSpeciality.SelectedValue.Equals(DAO1.SelectedSpecialityID))
                        {
                            lstDoc.Remove(DAO1);
                            lstRemoved.Add(DAO1);
                            j--;
                        }
                    }
                }
                
                for (int i = 0; i < Documents.Length - 1; i++)
                {
                    ListItem list = new ListItem();
                    list.Text = "";
                    list.Value = "";
                    if (!Documents[i].Split('~')[0].Equals(""))
                    {
                        lbSelectedDocs.Items.Add(list);
                    }
                }
                hfselectedNode.Value = "";

                ArrayList lstDoc1 = new ArrayList();
                DataSet ds = new DataSet();
                ds = objBillSysDocBO.GetAllSecialityDoc(txtCompanyID.Text);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                    DAO.SelectedId = ds.Tables[0].Rows[i][0].ToString();
                    DAO.SelectedText = objBillSysDocBO.GetFullPathOfNode(ds.Tables[0].Rows[i][0].ToString());
                    DAO.SelectedSpeciality = objBillSysDocBO.GetSpecialityNameUsingId(ds.Tables[0].Rows[i][1].ToString());
                    DAO.SelectedSpecialityID = ds.Tables[0].Rows[i][1].ToString();
                    DAO.ORDER = Convert.ToInt32(ds.Tables[0].Rows[i][2]);
                    DAO.REQUIRED_MULTIPLE = Convert.ToBoolean(ds.Tables[0].Rows[i][3]);
                    lstDoc1.Add(DAO);
                }

                for (int i = 0; i < Documents.Length - 1; i++)
                {
                    ListItem list = new ListItem();
                    if (!Documents[i].Split('~')[0].Equals(""))
                    {
                        list.Text = Documents[i].Split('~')[0];
                        list.Value = Documents[i].Split('~')[1] + "~" + Documents[i].Split('~')[2];
                        lbSelectedDocs.Items.Insert(i, list);
                        lbSelectedDocs.Items.RemoveAt(i + 1);
                        DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                        DAO.SelectedText = Documents[i].Split('~')[0];
                        DAO.SelectedId = Documents[i].Split('~')[2];
                        DAO.SelectedSpeciality = ddlSpeciality.SelectedItem.Text;
                        DAO.SelectedSpecialityID = ddlSpeciality.SelectedItem.Value;
                        DAO.ORDER = i;
                        int flag = 0;
                        for (int j = 0; j < lstDoc1.Count; j++)
                        {
                            DAO_Assign_Doc DAO1 = new DAO_Assign_Doc();
                            DAO1 = (DAO_Assign_Doc)lstDoc1[j];
                            if (DAO.SelectedSpecialityID.Equals(DAO1.SelectedSpecialityID) && DAO.SelectedId.Equals(DAO1.SelectedId))
                            {
                                DAO.REQUIRED_MULTIPLE = DAO1.REQUIRED_MULTIPLE;
                                flag = 1;
                                int ins = 0;
                                for (int k = 0; k < lstDoc.Count; k++)
                                {
                                    DAO_Assign_Doc DAODocInSession = new DAO_Assign_Doc();
                                    DAODocInSession = (DAO_Assign_Doc)lstDoc[k];
                                    if (DAO.SelectedSpecialityID.Equals(DAODocInSession.SelectedSpecialityID) && DAO.SelectedId.Equals(DAODocInSession.SelectedId))
                                    {
                                        ins = 1;
                                    }
                                    else
                                    {
                                        ins = 0;
                                    }
                                }
                                if (ins == 0)
                                {
                                    lstDoc.Add(DAO);
                                    lstRemoved.Remove(DAO);
                                    hfselectedNode.Value = hfselectedNode.Value + list.Text + "~" + list.Value + ",";
                                }                                
                                break;
                            }

                        }
                        if (flag == 0)
                        {
                            lstDoc.Add(DAO);
                            hfselectedNode.Value = hfselectedNode.Value + list.Text + "~" + list.Value + ",";
                        }
                    }
                }
                Session["SelectedDocList"] = lstDoc;
                Session["RemovedDoc"] = lstRemoved;
                usrMessage.PutMessage("Documents Assigned Successfully!");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                //Label3.Text = "Documents Assigned Successfully!";
            }
        }
        catch (Exception ex)
        {
            this.Session["SendPatientToDoctor"] = false;
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
        DAO_Assign_Doc DAO = new DAO_Assign_Doc();
        ArrayList lstDoc = new ArrayList();
        lstDoc = (ArrayList)Session["SelectedDocList"];
        lbAllSelectedDocuments.Items.Clear();
        lbselect.Items.Clear();
        if (lstDoc != null)
        {
            hfselectedNodeinListbox.Value = "";
            for (int i = 0; i < lstDoc.Count; i++)
            {
                DAO = (DAO_Assign_Doc)lstDoc[i];
                if (DAO.REQUIRED_MULTIPLE)
                {
                    ListItem lst = new ListItem();
                    lst.Text = DAO.SelectedSpeciality + DAO.SelectedText;
                    lst.Value = DAO.SelectedSpecialityID + "~" + DAO.SelectedId;
                    hfselectedNodeinListbox.Value = hfselectedNodeinListbox.Value + lst.Value + ",";
                    lbselect.Items.Add(lst);
                }
                else
                {
                    ListItem lst = new ListItem();
                    lst.Text = DAO.SelectedSpeciality + DAO.SelectedText;
                    lst.Value = DAO.SelectedSpecialityID + "~" + DAO.SelectedId;
                    lbAllSelectedDocuments.Items.Add(lst);
                }
            }
        }
    }

    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DAO_Assign_Doc DAO = new DAO_Assign_Doc();
            ArrayList lstDoc = new ArrayList();
            ArrayList lstDoc1 = new ArrayList();
            lstDoc = (ArrayList)Session["SelectedDocList"];
            lbAllSelectedDocuments.Items.Clear();
            int order = 0;

            string[] arrSpeciality = new string[1000];
            string[] arrNode = new string[1000];
            int count = 0;

            ArrayList lstRemoved = (ArrayList)Session["RemovedDoc"];
            if (lstRemoved!=null)
            {
                for (int i = 0; i < lstRemoved.Count; i++)
                {
                    DAO_Assign_Doc dao = new DAO_Assign_Doc();
                    dao = (DAO_Assign_Doc)lstRemoved[i];
                    if (Session["ALL_SPEC"] == null)
                    {
                        objBillSysDocBO.RemoveSpecialityDoc(dao.SelectedSpecialityID, txtCompanyID.Text, dao.SelectedId);
                    }
                }
                lstRemoved.Clear();
            }
            
            for (int i = 0; i < lstDoc.Count; i++)
            {
                int iflag = 0;
                DAO = (DAO_Assign_Doc)lstDoc[i];
                string[] selectedNode = new string[500];
                selectedNode = hfselectedNodeinListbox.Value.Split(',');
                for (int j = 0; j < selectedNode.Length - 1; j++)
                {
                    if (!selectedNode[j].Equals("") && selectedNode[j].Split('~')[1].Equals(DAO.SelectedId) && selectedNode[j].Split('~')[0].Equals(DAO.SelectedSpecialityID))
                    {
                        objBillSysDocBO.AssignDocToSpeciality(selectedNode[j].Split('~')[1], ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, DAO.SelectedSpecialityID, txtCompanyID.Text, DAO.ORDER, true);
                        iflag = 1;
                        DAO.REQUIRED_MULTIPLE = true;
                        arrSpeciality[i] = DAO.SelectedSpecialityID;
                        arrNode[i] = selectedNode[j].Split('~')[1];
                        count++;
                    }
                    order = DAO.ORDER;
                }
                if (iflag == 0)
                {
                    objBillSysDocBO.AssignDocToSpeciality(DAO.SelectedId, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, DAO.SelectedSpecialityID, txtCompanyID.Text, DAO.ORDER, false);
                    DAO.REQUIRED_MULTIPLE = false;
                    arrSpeciality[i] = DAO.SelectedSpecialityID;
                    arrNode[i] = DAO.SelectedId;
                    count++;
                }
                lstDoc1.Add(DAO);
            }
            DataSet ds = new DataSet();

            //ds = objBillSysDocBO.GetSecialityDoc(txtCompanyID.Text, arrSpeciality[0]);
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{

            //    int flag = 0;
            //    for (int j = 0; j < count; j++)
            //    {
            //        if ((Convert.ToInt32(ds.Tables[0].Rows[i][0]) == Convert.ToInt32(arrNode[j])) && ds.Tables[0].Rows[i][1].Equals(arrSpeciality[j]))
            //        {
            //            flag = 1;
            //        }
            //    }
            //    if (flag == 0)
            //    {
            //        if (arrSpeciality[i] == null)
            //        {
            //            break;
            //        }
            //        else
            //        {
            //            objBillSysDocBO.RemoveSpecialityDoc(arrSpeciality[i], txtCompanyID.Text, arrNode[i]);
            //        }
            //    }
            //    ds = objBillSysDocBO.GetSecialityDoc(txtCompanyID.Text, arrSpeciality[i]);
            //}
            Session["SelectedDocList"] = lstDoc1;
            //lblmsg.Text = "Documents Saved Successfully!";

            lstDoc = (ArrayList)Session["SelectedDocList"];
            lbAllSelectedDocuments.Items.Clear();
            lbselect.Items.Clear();
            if (lstDoc != null)
            {
                hfselectedNodeinListbox.Value = "";
                for (int i = 0; i < lstDoc.Count; i++)
                {
                    DAO = (DAO_Assign_Doc)lstDoc[i];
                    if (DAO.REQUIRED_MULTIPLE)
                    {
                        ListItem lst = new ListItem();
                        lst.Text = DAO.SelectedSpeciality + DAO.SelectedText;
                        lst.Value = DAO.SelectedSpecialityID + "~" + DAO.SelectedId;
                        hfselectedNodeinListbox.Value = hfselectedNodeinListbox.Value + lst.Value + ",";
                        lbselect.Items.Add(lst);
                    }
                    else
                    {
                        ListItem lst = new ListItem();
                        lst.Text = DAO.SelectedSpeciality + DAO.SelectedText;
                        lst.Value = DAO.SelectedSpecialityID + "~" + DAO.SelectedId;
                        lbAllSelectedDocuments.Items.Add(lst);
                    }
                }
            }
            ds = objBillSysDocBO.GetAllSecialityDoc(txtCompanyID.Text);
            lstDoc.Clear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DAO_Assign_Doc DAONew = new DAO_Assign_Doc();
                DAONew.SelectedId = ds.Tables[0].Rows[i][0].ToString();
                DAONew.SelectedText = objBillSysDocBO.GetFullPathOfNode(ds.Tables[0].Rows[i][0].ToString());
                DAONew.SelectedSpeciality = objBillSysDocBO.GetSpecialityNameUsingId(ds.Tables[0].Rows[i][1].ToString());
                DAONew.SelectedSpecialityID = ds.Tables[0].Rows[i][1].ToString();
                DAONew.ORDER = Convert.ToInt32(ds.Tables[0].Rows[i][2]);
                DAONew.REQUIRED_MULTIPLE = Convert.ToBoolean(ds.Tables[0].Rows[i][3]);
                lstDoc.Add(DAONew);
            }
      
            Session["SelectedDocList"] = lstDoc;

            if (lstRemoved != null)
            {
                lstRemoved.Clear();
            }
            
            Session["RemovedDoc"] = lstRemoved;
            MessageControl1.PutMessage("Documents Saved Successfully!");
            MessageControl1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            MessageControl1.Show();
            //Session["SelectedDocList"] = null;
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
    protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSpeciality.SelectedValue.Equals("0"))
        {
            hfIsAll.Value = "0";
            tvwmenu.PopulateNodesFromClient = true;
            tvwmenu.Nodes.RemoveAt(0);
            TreeNode node = new TreeNode("Document Manager", "0");
            node.PopulateOnDemand = true;
            tvwmenu.Nodes.Add(node);
            tvwmenu.ExpandAll();
            lbSelectedDocs.Items.Clear();
            Session["ALL_SPEC"] = null;

            btnAssignAll.Visible = false;
            btnRemove.Visible = false;
            btnAssign.Enabled = true;

        } if (ddlSpeciality.SelectedValue.Equals("1"))
        {
            hfIsAll.Value = "1";
            Bill_Sys_GetDocumentTypeTree objNodeid = new Bill_Sys_GetDocumentTypeTree();
           string szMedNodeId= objNodeid.GetNodeId(txtCompanyID.Text, "NFMED");
           if (szMedNodeId != "")
           {
               Session["ALL_SPEC"] = szMedNodeId;
           }
           else
           {
               Session["ALL_SPEC"] = "YES";
           }
          // lbSelectedDocs.Items.Clear();
           Bill_Sys_GetDocumentTypeTree objGetProc = new Bill_Sys_GetDocumentTypeTree();
           DataSet dsSpecDoc = objGetProc.GeALLtNodeId(txtCompanyID.Text);



           tvwmenu.Visible = true;
           DAO_Assign_Doc DAO = new DAO_Assign_Doc();
           ArrayList lstDoc = new ArrayList();
           lstDoc = (ArrayList)Session["SelectedDocList"];
           lbSelectedDocs.Items.Clear();
           hfselectedNode.Value = "";
           if (lstDoc != null)
           {

               lbSelectedDocs.Items.Clear();
               for (int i = 0; i < lstDoc.Count; i++)
               {
                   int iFlag = 0;
                   DAO = (DAO_Assign_Doc)lstDoc[i];
                   ListItem lst = new ListItem();

                   for (int cntRows = 0; cntRows < lbSelectedDocs.Items.Count; cntRows++)
                   {
                       string[] arrNodteId = lbSelectedDocs.Items[cntRows].Value.ToString().Split('~');
                       string szNodeId = arrNodteId[1].ToString();
                       if (szNodeId == DAO.SelectedId.ToString())
                       {
                           iFlag = 1;
                           break;
                       }

                   }
                   if (iFlag != 1)
                   {

                       lst.Text = DAO.SelectedText;
                       lst.Value = DAO.SelectedSpecialityID + "~" + DAO.SelectedId;

                       for (int l = 0; l < dsSpecDoc.Tables[0].Rows.Count; l++)
                       {
                           if (DAO.SelectedId.Equals(dsSpecDoc.Tables[0].Rows[l][0].ToString()))
                           {
                               lbSelectedDocs.Items.Add(lst);
                               hfselectedNode.Value = hfselectedNode.Value + DAO.SelectedText + "~" + "1" + "~" + DAO.SelectedId + ",";
                           }
                       }

                   }
               }
           }


            tvwmenu.PopulateNodesFromClient = true;
            tvwmenu.Nodes.RemoveAt(0);
            TreeNode node = new TreeNode("Document Manager", "0");
            node.PopulateOnDemand = true;
            tvwmenu.Nodes.Add(node);
            tvwmenu.ExpandAll();
            //lbSelectedDocs.Items.Clear();
            //hfselectedNode.Value = "";
          
            btnAssignAll.Visible = true;
            btnRemove.Visible = true;
            btnAssign.Enabled = false;
            //Button b = (Button)Wizard1.FindControl("btnnext");
          //  b.Enabled = false;
            
        }
        else
        {
            hfIsAll.Value = "0";
            btnAssignAll.Visible = false;
            btnRemove.Visible = false;
            btnAssign.Enabled = true;
           // lbSelectedDocs.Items.Clear();
            Session["ALL_SPEC"] = null;
            dsSpecialityDoc = objBillSysDocBO.GetSecialityDoc(txtCompanyID.Text, ddlSpeciality.SelectedValue);

            for (int i = 0; i < dsSpecialityDoc.Tables[0].Rows.Count; i++)
            {
                ListItem list = new ListItem();
                list.Text = "";
                list.Value = "";
                lbSelectedDocs.Items.Add(list);
            }
            tvwmenu.PopulateNodesFromClient = true;
            tvwmenu.Nodes.RemoveAt(0);
            TreeNode node = new TreeNode("Document Manager", "0");
            node.PopulateOnDemand = true;
            tvwmenu.Nodes.Add(node);
            tvwmenu.ExpandAll();
            int count = lbSelectedDocs.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (lbSelectedDocs.Items[i].Value.Equals(""))
                {
                    lbSelectedDocs.Items.RemoveAt(i);
                    i--;
                    count--;
                }
            }
        }        
    }
    protected void btnAssignAll_Click(object sender, EventArgs e)
    {
        string[] Documents = new string[500];
                 Documents = hfselectedNode.Value.Split(',');
              
                int lstcnt=  lbSelectedDocs.Items.Count;

                DataSet dsSpeciality = objSpeciality.GetSpecialityList(txtCompanyID.Text);
                for (int j = 0; j < Documents.Length - 1; j++)
                {
                    string nodename = Documents[j].Split('~')[0].ToString();
                    string nodeid = Documents[j].Split('~')[2].ToString();
                 //   string order = Documents[j].Split('~')[1].ToString();
                    for (int i = 0; i < dsSpeciality.Tables[0].Rows.Count; i++)
                    {
                        // objBillSysDocBO.AssignDocToSpeciality(nodeid, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ddlSpeciality.Items[i].Value.ToString(), txtCompanyID.Text, order, true);
                       // if (ddlSpeciality.Items[i].Value.ToString() != "0" && ddlSpeciality.Items[i].Value.ToString() != "1")
                        {
                            objBillSysDocBO.AssignDocToSpeciality(nodeid, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, dsSpeciality.Tables[0].Rows[i][0].ToString(), txtCompanyID.Text, 0, true);
                        }
                        Bill_Sys_GetDocumentTypeTree objAddAll = new Bill_Sys_GetDocumentTypeTree();
                        objAddAll.AddAll(txtCompanyID.Text, nodeid, dsSpeciality.Tables[0].Rows[0][0].ToString(), "1", nodename);
                    }

                }
                DataSet ds1 = new DataSet();
                ds1 = objBillSysDocBO.GetAllSecialityDoc(txtCompanyID.Text);
                ArrayList lstDoc2 = new ArrayList();
                for (int y = 0; y < ds1.Tables[0].Rows.Count; y++)
                {
                    DAO_Assign_Doc DAO2 = new DAO_Assign_Doc();
                    DAO2.SelectedId = ds1.Tables[0].Rows[y][0].ToString();
                    DAO2.SelectedText = objBillSysDocBO.GetFullPathOfNode(ds1.Tables[0].Rows[y][0].ToString());
                    DAO2.SelectedSpeciality = objBillSysDocBO.GetSpecialityNameUsingId(ds1.Tables[0].Rows[y][1].ToString());
                    DAO2.SelectedSpecialityID = ds1.Tables[0].Rows[y][1].ToString();
                    DAO2.ORDER = Convert.ToInt32(ds1.Tables[0].Rows[y][2]);
                    DAO2.REQUIRED_MULTIPLE = Convert.ToBoolean(ds1.Tables[0].Rows[y][3]);
                    lstDoc2.Add(DAO2);
                }
                Session["SelectedDocList"] = lstDoc2;

                tvwmenu.Visible = true;
                DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                ArrayList lstDoc = new ArrayList();
                lstDoc = (ArrayList)Session["SelectedDocList"];
                lbSelectedDocs.Items.Clear();
                if (lstDoc != null)
                {


                    for (int l = 0; l < lstDoc.Count; l++)
                    {
                        int iFlag = 0;
                        DAO = (DAO_Assign_Doc)lstDoc[l];
                        ListItem lst = new ListItem();

                        for (int cntRows = 0; cntRows < lbSelectedDocs.Items.Count; cntRows++)
                        {
                            string[] arrNodteId = lbSelectedDocs.Items[cntRows].Value.ToString().Split('~');
                            string szNodeId = arrNodteId[1].ToString();
                            if (szNodeId == DAO.SelectedId.ToString())
                            {
                                iFlag = 1;
                                break;
                            }

                        }
                        if (iFlag != 1)
                        {

                            lst.Text = DAO.SelectedText;
                            lst.Value = DAO.SelectedSpecialityID + "~" + DAO.SelectedId;
                            if (DAO.SelectedSpecialityID.Equals(ddlSpeciality.SelectedValue))
                            {
                                lbSelectedDocs.Items.Add(lst);
                            }
                        }
                    }
                }


                DataSet dsDoc = new DataSet();
                dsDoc = objBillSysDocBO.GetAllSecialityDoc(txtCompanyID.Text);
                lbAllAssignedDoc.Items.Clear();
                for (int m = 0; m < dsDoc.Tables[0].Rows.Count; m++)
                {
                    DAO_Assign_Doc DAO1 = new DAO_Assign_Doc();
                    DAO1.SelectedId = dsDoc.Tables[0].Rows[m][0].ToString();
                    DAO1.SelectedText = objBillSysDocBO.GetFullPathOfNode(dsDoc.Tables[0].Rows[m][0].ToString());
                    DAO1.SelectedSpeciality = objBillSysDocBO.GetSpecialityNameUsingId(dsDoc.Tables[0].Rows[m][1].ToString());
                    DAO1.SelectedSpecialityID = dsDoc.Tables[0].Rows[m][1].ToString();
                    DAO1.ORDER = Convert.ToInt32(dsDoc.Tables[0].Rows[m][2]);
                    DAO1.REQUIRED_MULTIPLE = Convert.ToBoolean(dsDoc.Tables[0].Rows[m][3]);
                    ListItem lst = new ListItem();
                    lst.Text = DAO1.SelectedSpeciality.ToString() + DAO1.SelectedText.ToString();
                    lst.Value = DAO1.SelectedId.ToString();
                    lbAllAssignedDoc.Items.Add(lst);

                }



                Bill_Sys_GetDocumentTypeTree objGetProc = new Bill_Sys_GetDocumentTypeTree();
                DataSet dsSpecDoc = objGetProc.GeALLtNodeId(txtCompanyID.Text);



                tvwmenu.Visible = true;
                DAO_Assign_Doc DAO3 = new DAO_Assign_Doc();
                ArrayList lstDoc3 = new ArrayList();
                lstDoc3 = (ArrayList)Session["SelectedDocList"];
                lbSelectedDocs.Items.Clear();
                hfselectedNode.Value = "";
                if (lstDoc3 != null)
                {

                    lbSelectedDocs.Items.Clear();
                    for (int i = 0; i < lstDoc3.Count; i++)
                    {
                        int iFlag = 0;
                        DAO3 = (DAO_Assign_Doc)lstDoc3[i];
                        ListItem lst = new ListItem();

                        for (int cntRows = 0; cntRows < lbSelectedDocs.Items.Count; cntRows++)
                        {
                            string[] arrNodteId = lbSelectedDocs.Items[cntRows].Value.ToString().Split('~');
                            string szNodeId = arrNodteId[1].ToString();
                            if (szNodeId == DAO3.SelectedId.ToString())
                            {
                                iFlag = 1;
                                break;
                            }

                        }
                        if (iFlag != 1)
                        {

                            lst.Text = DAO3.SelectedText;
                            lst.Value = DAO3.SelectedSpecialityID + "~" + DAO3.SelectedId;

                            for (int l = 0; l < dsSpecDoc.Tables[0].Rows.Count; l++)
                            {
                                if (DAO3.SelectedId.Equals(dsSpecDoc.Tables[0].Rows[l][0].ToString()))
                                {
                                    lbSelectedDocs.Items.Add(lst);
                                    hfselectedNode.Value = hfselectedNode.Value + DAO3.SelectedText + "~" + "1" + "~" + DAO3.SelectedId + ",";
                                }
                            }

                        }
                    }
                }



                tvwmenu.PopulateNodesFromClient = true;
                tvwmenu.Nodes.RemoveAt(0);
                TreeNode node = new TreeNode("Document Manager", "0");
                node.PopulateOnDemand = true;
                tvwmenu.Nodes.Add(node);
                tvwmenu.ExpandAll();

    }



    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string[] Documents = new string[500];
        Documents = hfselectedNode.Value.Split(',');
        DataSet dsSpeciality = objSpeciality.GetSpecialityList(txtCompanyID.Text);
        for (int j = 0; j < Documents.Length - 1; j++)
        {
            string nodeid = Documents[j].Split('~')[2].ToString();
           // string order = Documents[j].Split('~')[1].ToString();
            for (int i = 0; i < dsSpeciality.Tables[0].Rows.Count; i++)
            {
                // objBillSysDocBO.AssignDocToSpeciality(nodeid, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ddlSpeciality.Items[i].Value.ToString(), txtCompanyID.Text, order, true);
               
                    objBillSysDocBO.RemoveSpecialityDoc(dsSpeciality.Tables[0].Rows[i][0].ToString(),txtCompanyID.Text, nodeid);
                
            }
            Bill_Sys_GetDocumentTypeTree objAddAll = new Bill_Sys_GetDocumentTypeTree();
            objAddAll.ReMoveAll(txtCompanyID.Text, nodeid);

        }
        lbSelectedDocs.Items.Clear();


        DataSet ds1 = new DataSet();
        ds1 = objBillSysDocBO.GetAllSecialityDoc(txtCompanyID.Text);
        ArrayList lstDoc2 = new ArrayList();
        for (int y = 0; y < ds1.Tables[0].Rows.Count; y++)
        {
            DAO_Assign_Doc DAO2 = new DAO_Assign_Doc();
            DAO2.SelectedId = ds1.Tables[0].Rows[y][0].ToString();
            DAO2.SelectedText = objBillSysDocBO.GetFullPathOfNode(ds1.Tables[0].Rows[y][0].ToString());
            DAO2.SelectedSpeciality = objBillSysDocBO.GetSpecialityNameUsingId(ds1.Tables[0].Rows[y][1].ToString());
            DAO2.SelectedSpecialityID = ds1.Tables[0].Rows[y][1].ToString();
            DAO2.ORDER = Convert.ToInt32(ds1.Tables[0].Rows[y][2]);
            DAO2.REQUIRED_MULTIPLE = Convert.ToBoolean(ds1.Tables[0].Rows[y][3]);
            lstDoc2.Add(DAO2);
        }
        Session["SelectedDocList"] = lstDoc2;


        tvwmenu.Visible = true;
        DAO_Assign_Doc DAO = new DAO_Assign_Doc();
        ArrayList lstDoc = new ArrayList();
        lstDoc = (ArrayList)Session["SelectedDocList"];
        lbSelectedDocs.Items.Clear();
        if (lstDoc != null)
        {


            for (int l = 0; l < lstDoc.Count; l++)
            {
                int iFlag = 0;
                DAO = (DAO_Assign_Doc)lstDoc[l];
                ListItem lst = new ListItem();

                for (int cntRows = 0; cntRows < lbSelectedDocs.Items.Count; cntRows++)
                {
                    string[] arrNodteId = lbSelectedDocs.Items[cntRows].Value.ToString().Split('~');
                    string szNodeId = arrNodteId[1].ToString();
                    if (szNodeId == DAO.SelectedId.ToString())
                    {
                        iFlag = 1;
                        break;
                    }

                }
                if (iFlag != 1)
                {

                    lst.Text = DAO.SelectedText;
                    lst.Value = DAO.SelectedSpecialityID + "~" + DAO.SelectedId;
                    if (DAO.SelectedSpecialityID.Equals(ddlSpeciality.SelectedValue))
                    {
                        lbSelectedDocs.Items.Add(lst);
                    }
                }
            }
        }


        DataSet dsDoc = new DataSet();
        dsDoc = objBillSysDocBO.GetAllSecialityDoc(txtCompanyID.Text);
        lbAllAssignedDoc.Items.Clear();
        for (int m = 0; m < dsDoc.Tables[0].Rows.Count; m++)
        {
            DAO_Assign_Doc DAO1 = new DAO_Assign_Doc();
            DAO1.SelectedId = dsDoc.Tables[0].Rows[m][0].ToString();
            DAO1.SelectedText = objBillSysDocBO.GetFullPathOfNode(dsDoc.Tables[0].Rows[m][0].ToString());
            DAO1.SelectedSpeciality = objBillSysDocBO.GetSpecialityNameUsingId(dsDoc.Tables[0].Rows[m][1].ToString());
            DAO1.SelectedSpecialityID = dsDoc.Tables[0].Rows[m][1].ToString();
            DAO1.ORDER = Convert.ToInt32(dsDoc.Tables[0].Rows[m][2]);
            DAO1.REQUIRED_MULTIPLE = Convert.ToBoolean(dsDoc.Tables[0].Rows[m][3]);
            ListItem lst = new ListItem();
            lst.Text = DAO1.SelectedSpeciality.ToString() + DAO1.SelectedText.ToString();
            lst.Value = DAO1.SelectedId.ToString();
            lbAllAssignedDoc.Items.Add(lst);

        }

        Bill_Sys_GetDocumentTypeTree objGetProc = new Bill_Sys_GetDocumentTypeTree();
        DataSet dsSpecDoc = objGetProc.GeALLtNodeId(txtCompanyID.Text);



        tvwmenu.Visible = true;
        DAO_Assign_Doc DAO3 = new DAO_Assign_Doc();
        ArrayList lstDoc3 = new ArrayList();
        lstDoc3 = (ArrayList)Session["SelectedDocList"];
        lbSelectedDocs.Items.Clear();
        hfselectedNode.Value = "";
        if (lstDoc3 != null)
        {

            lbSelectedDocs.Items.Clear();
            for (int i = 0; i < lstDoc3.Count; i++)
            {
                int iFlag = 0;
                DAO3 = (DAO_Assign_Doc)lstDoc3[i];
                ListItem lst = new ListItem();

                for (int cntRows = 0; cntRows < lbSelectedDocs.Items.Count; cntRows++)
                {
                    string[] arrNodteId = lbSelectedDocs.Items[cntRows].Value.ToString().Split('~');
                    string szNodeId = arrNodteId[1].ToString();
                    if (szNodeId == DAO3.SelectedId.ToString())
                    {
                        iFlag = 1;
                        break;
                    }

                }
                if (iFlag != 1)
                {

                    lst.Text = DAO3.SelectedText;
                    lst.Value = DAO3.SelectedSpecialityID + "~" + DAO3.SelectedId;

                    for (int l = 0; l < dsSpecDoc.Tables[0].Rows.Count; l++)
                    {
                        if (DAO3.SelectedId.Equals(dsSpecDoc.Tables[0].Rows[l][0].ToString()))
                        {
                            lbSelectedDocs.Items.Add(lst);
                            hfselectedNode.Value = hfselectedNode.Value + DAO3.SelectedText + "~" + "1" + "~" + DAO3.SelectedId + ",";
                        }
                    }

                }
            }
        }

        tvwmenu.PopulateNodesFromClient = true;
        tvwmenu.Nodes.RemoveAt(0);
        TreeNode node = new TreeNode("Document Manager", "0");
        node.PopulateOnDemand = true;
        tvwmenu.Nodes.Add(node);
        tvwmenu.ExpandAll();

        
    }
}