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
using mbs.bl;
using Componend;
using DocumentTypeBO;
using System.Data.SqlClient;
using mbs.bl.litigation;

public partial class AJAX_Pages_Bill_Sys_Email_Notification : PageBase

{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    string[] listButton;
    Bill_Sys_TransferDocumentBO objBillSysDocBO = new Bill_Sys_TransferDocumentBO(ConfigurationManager.AppSettings["Connection_String"].ToString());
    DataSet dsSpecialityDoc = new DataSet();
    Bill_Sys_DisplaySpeciality objSpeciality = new Bill_Sys_DisplaySpeciality();
    

   protected void addInterval(string groupname, int days, string CompanyId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    Connection = sqlConnection
                };
                if (!this.txtIntervalProvider.Visible)
                {
                    try
                    {
                        try
                        {
                            if (groupname != null && groupname != "" && CompanyId != null && CompanyId != "")
                            {
                                string[] strArrays = new string[] { "DELETE FROM MST_MISSING_DOCUMENTS_EMAIL_DATE WHERE SZ_GROUP_NAME LIKE '", groupname, "' AND SZ_COMPANY_ID='", CompanyId, "'" };
                                sqlCommand.CommandText = string.Concat(strArrays);
                                sqlConnection.Open();
                                sqlCommand.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                        }
                    }
                    finally
                    {
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            sqlConnection.Close();
                        }
                    }
                    object[] objArray = new object[] { "INSERT INTO MST_MISSING_DOCUMENTS_EMAIL_DATE(SZ_GROUP_NAME,SZ_COMPANY_ID,I_INTERVAL,DT_NEXT_UPDATE_DATE) VALUES('", groupname, "','", CompanyId, "',", days, ",'", DateTime.Today, "')" };
                    sqlCommand.CommandText = string.Concat(objArray);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    try
                    {
                        try
                        {
                            if (CompanyId != null && CompanyId != "")
                            {
                                sqlCommand.CommandText = string.Concat("DELETE FROM MST_MISSING_DOCUMENTS_EMAIL_DATE WHERE SZ_GROUP_NAME IS NULL AND SZ_COMPANY_ID='", CompanyId, "' AND SZ_NOTIFICATION_CODE='PR'");
                                sqlConnection.Open();
                                sqlCommand.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                        }
                    }
                    finally
                    {
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            sqlConnection.Close();
                        }
                    }
                    object[] companyId = new object[] { "INSERT INTO MST_MISSING_DOCUMENTS_EMAIL_DATE(SZ_COMPANY_ID,I_INTERVAL,DT_NEXT_UPDATE_DATE,SZ_NOTIFICATION_CODE) VALUES('", CompanyId, "',", days, ",'", DateTime.Today, "','PR')" };
                    sqlCommand.CommandText = string.Concat(companyId);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
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

           
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
            string text = "";
            int num = 0;
            int num1 = 1;
            this.hfselectedNode.Value.ToString();
            if (!this.ExtendedGroup.Visible)
            {
                if (this.txtNewGroup.Visible)
                {
                    if (this.txtNewGroup.Text.Equals(""))
                    {
                        this.usrMessage.PutMessage("Group is not added.");
                        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                        this.usrMessage.Show();
                    }
                    else if (this.hfselectedNode.Value.ToString().Equals("") || this.txtMisDocEmailAddress.Text == "")
                    {
                        this.hfselectedNode.Value = "";
                        this.tvwmenu.PopulateNodesFromClient = true;
                        this.tvwmenu.Nodes.RemoveAt(0);
                        TreeNode treeNode = new TreeNode("Document Manager", "0")
                        {
                            PopulateOnDemand = true
                        };
                        this.tvwmenu.Nodes.Add(treeNode);
                        this.tvwmenu.ExpandAll();
                        this.usrMessage.PutMessage("Nodes not selected or Email address missing");
                        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                        this.usrMessage.Show();
                    }
                    else if (this.txtInterval.Text == "" || !int.TryParse(this.txtInterval.Text.Trim(), out num1))
                    {
                        this.usrMessage.PutMessage("Please enter interval in number of days");
                        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                        this.usrMessage.Show();
                    }
                    else if ((new Notification()).CheckGroupName(this.txtNewGroup.Text, this.txtCompanyId.Text) != 0)
                    {
                        this.usrMessage.PutMessage("Group is already present");
                        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                        this.usrMessage.Show();
                    }
                    else
                    {
                        text = this.txtNewGroup.Text;
                        num = 1;
                    }
                }
            }
            else if (this.ExtendedGroup.SelectedValue.Equals("---Select---"))
            {
                this.usrMessage.PutMessage("Group is not selected.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            else if (this.hfselectedNode.Value.ToString().Equals("") || this.txtMisDocEmailAddress.Text == "")
            {
                this.hfselectedNode.Value = "";
                this.lbSelectedDocs.Items.Clear();
                this.tvwmenu.PopulateNodesFromClient = true;
                this.tvwmenu.Nodes.RemoveAt(0);
                this.ExtendedGroup.SelectedIndex = 0;
                TreeNode treeNode1 = new TreeNode("Document Manager", "0")
                {
                    PopulateOnDemand = true
                };
                this.tvwmenu.Nodes.Add(treeNode1);
                this.tvwmenu.ExpandAll();
                this.usrMessage.PutMessage("Nodes not selected or Email address missing");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            else if (this.txtInterval.Text == "" || !int.TryParse(this.txtInterval.Text.Trim(), out num1))
            {
                this.usrMessage.PutMessage("Please enter interval in number of days");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            else
            {
                text = this.ExtendedGroup.SelectedItem.Text.ToString();
                num = 1;
            }
            if (num == 1)
            {
                ArrayList arrayLists = new ArrayList();
                ArrayList arrayLists1 = new ArrayList();
                if (arrayLists == null)
                {
                    arrayLists = new ArrayList();
                }
                string[] strArrays = new string[100];
                strArrays = this.hfselectedNode.Value.Split(new char[] { ',' });
                ArrayList arrayLists2 = new ArrayList();
                for (int i = 0; i < (int)strArrays.Length - 1; i++)
                {
                    NotificationDAO notificationDAO = new NotificationDAO();
                    string str = strArrays[i];
                    char[] chrArray = new char[] { '~' };
                    notificationDAO.NODE_ID = str.Split(chrArray)[2];
                    notificationDAO.NOTIFICATION_ID = this.extddlNotification.Text;
                    notificationDAO.COMPANY_ID = this.txtCompanyId.Text;
                    notificationDAO.ORDER = i.ToString();
                    notificationDAO.USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    notificationDAO.GROUP_NAME = text;
                    string str1 = strArrays[i];
                    char[] chrArray1 = new char[] { '~' };
                    notificationDAO.DISCRIPTION = str1.Split(chrArray1)[0];
                    arrayLists1.Add(notificationDAO);
                    this.lbSelectedDocs.Items.Clear();
                }
                if ((new Notification()).SaveEmailNotification(arrayLists1, this.txtMisDocEmailAddress.Text) != 1)
                {
                    this.usrMessage.PutMessage("Error in transaction");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage.Show();
                }
                else
                {
                    this.addInterval(text, Convert.ToInt32(this.txtInterval.Text.Trim()), this.txtCompanyId.Text);
                    this.usrMessage.PutMessage("Saved successfully.");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
                this.hfselectedNode.Value = "";
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

    private void btnSave_Clicklaw()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int num = 1;
            this.hfselectedNodelaw.Value.ToString();
            if (this.hfselectedNodelaw.Value.ToString().Equals("") || this.txtEmailAddress.Text == "")
            {
                UploadEmailDocument uploadEmailDocument = new UploadEmailDocument();
                uploadEmailDocument.DeleteEmailSpecification(this.txtCompanyId.Text, this.extddlLawFirm.Text);
                this.hfselectedNodelaw.Value = "";
                this.lbSelectedDocslaw.Items.Clear();
                this.extddlLawFirm.Text = "NA";
                this.tvwmenulaw.Attributes.Add("onclick", "OnCheckBoxCheckChangedlaw(event)");
                this.tvwmenulaw.PopulateNodesFromClient = true;
                this.tvwmenulaw.Nodes.RemoveAt(0);
                TreeNode treeNode = new TreeNode("Document Manager", "0")
                {
                    PopulateOnDemand = true
                };
                this.tvwmenulaw.Nodes.Add(treeNode);
                this.tvwmenulaw.ExpandAll();
                this.setuplaoddcumentLawfirm();
                this.txtEmailAddress.Text = "";
                num = 0;
            }
            if (num == 1)
            {
                ArrayList arrayLists = new ArrayList();
                ArrayList arrayLists1 = new ArrayList();
                if (arrayLists == null)
                {
                    arrayLists = new ArrayList();
                }
                string[] strArrays = new string[100];
                strArrays = this.hfselectedNodelaw.Value.Split(new char[] { ',' });
                ArrayList arrayLists2 = new ArrayList();
                for (int i = 0; i < (int)strArrays.Length - 1; i++)
                {
                    UploadDoumentSpecification uploadDoumentSpecification = new UploadDoumentSpecification();
                    string str = strArrays[i];
                    char[] chrArray = new char[] { '~' };
                    uploadDoumentSpecification.NodeID = Convert.ToInt32(str.Split(chrArray)[1]);
                    uploadDoumentSpecification.CompanyID = this.txtCompanyId.Text;
                    uploadDoumentSpecification.UserID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    string str1 = strArrays[i];
                    char[] chrArray1 = new char[] { '~' };
                    uploadDoumentSpecification.DescriptionNode = str1.Split(chrArray1)[0];
                    uploadDoumentSpecification.LawFirmID = this.extddlLawFirm.Text.ToString();
                    uploadDoumentSpecification.orderID = i;
                    arrayLists1.Add(uploadDoumentSpecification);
                }
                UploadEmailDocument uploadEmailDocument1 = new UploadEmailDocument();
                uploadEmailDocument1.SaveEmailSpecification(arrayLists1, this.txtEmailAddress.Text.Trim());
                this.hfselectedNodelaw.Value = "";
                this.lbSelectedDocslaw.Items.Clear();
                this.extddlLawFirm.Text = "NA";
                this.tvwmenulaw.Attributes.Add("onclick", "OnCheckBoxCheckChangedlaw(event)");
                this.tvwmenulaw.PopulateNodesFromClient = true;
                this.tvwmenulaw.Nodes.RemoveAt(0);
                TreeNode treeNode1 = new TreeNode("Document Manager", "0")
                {
                    PopulateOnDemand = true
                };
                this.tvwmenulaw.Nodes.Add(treeNode1);
                this.tvwmenulaw.ExpandAll();
                this.setuplaoddcumentLawfirm();
                this.txtEmailAddress.Text = "";
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

    protected void btnSaveEmail_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int num = 0;
            string text = this.txtEmailAddress.Text;
            string[] strArrays = text.Split(new char[] { ',' });
            ArrayList arrayLists = new ArrayList();
            Notification notification = new Notification();
            if (!this.txtIntervalProvider.Visible || !(this.txtIntervalProvider.Text == "") && int.TryParse(this.txtIntervalProvider.Text.Trim(), out num))
            {
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    NotificationDAO notificationDAO = new NotificationDAO()
                    {
                        COMPANY_ID = this.txtCompanyId.Text,
                        EMAIL_ID = strArrays[i].ToString(),
                        NOTIFICATION_ID = this.extddlNotification.Text
                    };
                    if (!this.extddlLawFirm.Visible)
                    {
                        notificationDAO.SPECIFICATION = "NA";
                    }
                    else
                    {
                        notificationDAO.SPECIFICATION = this.extddlLawFirm.Text;
                    }
                    arrayLists.Add(notificationDAO);
                }
                if (notification.SaveEmail(arrayLists) != 1)
                {
                    this.usrMessage.PutMessage("Error in transaction");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage.Show();
                }
                else
                {
                    if (this.txtIntervalProvider.Visible)
                    {
                        this.addInterval("", Convert.ToInt32(this.txtIntervalProvider.Text.Trim()), this.txtCompanyId.Text);
                    }
                    this.usrMessage.PutMessage("Saved successfully.");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                    this.btnSave_Clicklaw();
                }
            }
            else
            {
                this.usrMessage.PutMessage("Please enter interval in number of days");
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

    protected void chkGroup_CheckedChanged(object sender, EventArgs e)
    {
        this.hfselectedNode.Value = "";
        if (this.chkGroup.Checked)
        {
            this.txtNewGroup.Visible = true;
            this.txtNewGroup.Text = "";
            this.ExtendedGroup.Visible = false;
            this.tvwmenu.PopulateNodesFromClient = true;
            this.tvwmenu.Nodes.RemoveAt(0);
            TreeNode treeNode = new TreeNode("Document Manager", "0")
            {
                PopulateOnDemand = true
            };
            this.tvwmenu.Nodes.Add(treeNode);
            this.tvwmenu.ExpandAll();
            this.lbSelectedDocs.Items.Clear();
            this.txtMisDocEmailAddress.Text = "";
            this.txtInterval.Text = "";
            return;
        }
        this.txtNewGroup.Visible = false;
        this.ExtendedGroup.Visible = true;
        this.ExtendedGroup.SelectedIndex = 0;
        this.tvwmenu.PopulateNodesFromClient = true;
        this.tvwmenu.Nodes.RemoveAt(0);
        TreeNode treeNode1 = new TreeNode("Document Manager", "0")
        {
            PopulateOnDemand = true
        };
        this.tvwmenu.Nodes.Add(treeNode1);
        this.tvwmenu.ExpandAll();
        this.lbSelectedDocs.Items.Clear();
        this.txtMisDocEmailAddress.Text = "";
        DataSet groupName = (new Notification()).GetGroupName(this.txtCompanyId.Text);
        this.ExtendedGroup.DataSource = groupName.Tables[0];
        this.txtInterval.Text = "";
        if (groupName.Tables[0].Rows.Count > 0)
        {
            this.ExtendedGroup.DataTextField = "DESCRIPTION";
            this.ExtendedGroup.DataBind();
            this.ExtendedGroup.Items.Insert(0, "---Select---");
            this.ExtendedGroup.SelectedIndex = 0;
        }
        if (this.ExtendedGroup.Items.Count == 0)
        {
            this.ExtendedGroup.Items.Insert(0, "---Select---");
            this.ExtendedGroup.SelectedIndex = 0;
        }
    }

    protected void extddlLawFirm_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        Notification notification = new Notification();
        if (this.extddlLawFirm.Text == "NA")
        {
            this.txtEmailAddress.Text = "";
        }
        else
        {
            dataSet = notification.GetEmail(Convert.ToInt32(this.extddlNotification.Text), this.txtCompanyId.Text, this.extddlLawFirm.Text);
            string str = "";
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                str = (str != "" ? string.Concat(str, ",", dataSet.Tables[0].Rows[i]["sz_email_id"].ToString()) : dataSet.Tables[0].Rows[i]["sz_email_id"].ToString());
            }
            this.txtEmailAddress.Text = str;
        }
        this.tvwmenulaw.Attributes.Add("onclick", "OnCheckBoxCheckChangedlaw(event)");
        this.tvwmenulaw.PopulateNodesFromClient = true;
        this.tvwmenulaw.Nodes.RemoveAt(0);
        TreeNode treeNode = new TreeNode("Document Manager", "0")
        {
            PopulateOnDemand = true
        };
        this.tvwmenulaw.Nodes.Add(treeNode);
        this.tvwmenulaw.ExpandAll();
        this.setuplaoddcumentLawfirm();
    }

    protected void extddlNotification_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtEmailAddress.Text = "";
        Notification notification = new Notification();
        this.txtIntervalProvider.Text = "";
        this.txtIntervalProvider.Visible = false;
        this.lblInterval.Visible = false;
        if (this.extddlNotification.Text == "NA")
        {
            if (this.extddlNotification.Text == "NA")
            {
                this.lblLawFirm.Visible = false;
                this.lblEmailAddress.Visible = false;
                this.extddlLawFirm.Visible = false;
                this.txtEmailAddress.Visible = false;
                this.lblNote.Visible = false;
                this.btnSaveEmail.Visible = false;
                this.lblheder.Text = "";
                this.tblMisDoc.Visible = false;
            }
            return;
        }
        if (this.extddlNotification.Selected_Text == "Provider Reports")
        {
            this.txtIntervalProvider.Text = this.GetInterval("", this.txtCompanyId.Text);
            this.txtIntervalProvider.Visible = true;
            this.lblInterval.Visible = true;
            this.tblMisDoc.Visible = false;
            this.lblEmailAddress.Visible = true;
            this.txtEmailAddress.Visible = true;
            this.lblNote.Visible = true;
            this.lblLawFirm.Visible = false;
            this.extddlLawFirm.Visible = false;
            this.tbllawfirm.Visible = false;
            this.btnSaveEmail.Visible = true;
            DataSet dataSet = new DataSet();
            dataSet = notification.GetEmail(Convert.ToInt32(this.extddlNotification.Text), this.txtCompanyId.Text, "NA");
            string str = "";
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                str = (str != "" ? string.Concat(str, ",", dataSet.Tables[0].Rows[i]["sz_email_id"].ToString()) : dataSet.Tables[0].Rows[i]["sz_email_id"].ToString());
            }
            this.txtEmailAddress.Text = str;
            return;
        }
        if (this.extddlNotification.Selected_Text == "Transfer of bills for litigation")
        {
            this.lblheder.Text = "Transfer of bills for litigation";
            this.lblLawFirm.Visible = true;
            this.lblEmailAddress.Visible = true;
            this.extddlLawFirm.Visible = true;
            this.txtEmailAddress.Visible = true;
            this.lblNote.Visible = true;
            this.btnSaveEmail.Visible = true;
            this.extddlLawFirm.Text = "NA";
            this.tblMisDoc.Visible = false;
            this.tbllawfirm.Visible = true;
            this.tvwmenulaw.Attributes.Add("onclick", "OnCheckBoxCheckChangedlaw(event)");
            this.tvwmenulaw.PopulateNodesFromClient = true;
            this.tvwmenulaw.Nodes.RemoveAt(0);
            TreeNode treeNode = new TreeNode("Document Manager", "0")
            {
                PopulateOnDemand = true
            };
            this.tvwmenulaw.Nodes.Add(treeNode);
            this.tvwmenulaw.ExpandAll();
            return;
        }
        if (this.extddlNotification.Selected_Text != "Missing Documents")
        {
            this.tblMisDoc.Visible = false;
            this.lblEmailAddress.Visible = true;
            this.txtEmailAddress.Visible = true;
            this.lblNote.Visible = true;
            this.lblLawFirm.Visible = false;
            this.extddlLawFirm.Visible = false;
            this.btnSaveEmail.Visible = true;
            DataSet email = new DataSet();
            email = notification.GetEmail(Convert.ToInt32(this.extddlNotification.Text), this.txtCompanyId.Text, "NA");
            string str1 = "";
            for (int j = 0; j < email.Tables[0].Rows.Count; j++)
            {
                str1 = (str1 != "" ? string.Concat(str1, ",", email.Tables[0].Rows[j]["sz_email_id"].ToString()) : email.Tables[0].Rows[j]["sz_email_id"].ToString());
            }
            this.txtEmailAddress.Text = str1;
            return;
        }
        this.lblLawFirm.Visible = false;
        this.lblEmailAddress.Visible = false;
        this.extddlLawFirm.Visible = false;
        this.txtEmailAddress.Visible = false;
        this.lblNote.Visible = false;
        this.btnSaveEmail.Visible = false;
        this.lblheder.Text = "Missing Documents";
        this.tblMisDoc.Visible = true;
        this.tbllawfirm.Visible = false;
        this.tvwmenu.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");
        this.lbSelectedDocs.Items.Clear();
        this.txtUserID.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        DataSet groupName = notification.GetGroupName(this.txtCompanyId.Text);
        this.ExtendedGroup.DataSource = groupName.Tables[0];
        if (groupName.Tables[0].Rows.Count > 0)
        {
            this.ExtendedGroup.DataTextField = "DESCRIPTION";
            this.ExtendedGroup.DataBind();
        }
        this.ExtendedGroup.Items.Insert(0, "---Select---");
        DataSet allSecialityDoc = new DataSet();
        allSecialityDoc = this.objBillSysDocBO.GetAllSecialityDoc(this.txtCompanyId.Text);
        ArrayList arrayLists = new ArrayList();
        for (int k = 0; k < allSecialityDoc.Tables[0].Rows.Count; k++)
        {
            DAO_Assign_Doc dAOAssignDoc = new DAO_Assign_Doc()
            {
                SelectedId = allSecialityDoc.Tables[0].Rows[k][0].ToString(),
                SelectedText = this.objBillSysDocBO.GetFullPathOfNode(allSecialityDoc.Tables[0].Rows[k][0].ToString()),
                ORDER = Convert.ToInt32(allSecialityDoc.Tables[0].Rows[k][2])
            };
            arrayLists.Add(dAOAssignDoc);
        }
        this.Session["SelectedDocList1"] = arrayLists;
        this.tvwmenu.PopulateNodesFromClient = true;
        this.tvwmenu.Nodes.RemoveAt(0);
        TreeNode treeNode1 = new TreeNode("Document Manager", "0")
        {
            PopulateOnDemand = true
        };
        this.tvwmenu.Nodes.Add(treeNode1);
        this.tvwmenu.ExpandAll();
        this.lbSelectedDocs.Items.Clear();
        this.txtMisDocEmailAddress.Text = "";
        this.txtNewGroup.Text = "";
        this.txtInterval.Text = "";
        DataSet dataSet1 = new DataSet();
        this.objBillSysDocBO.GetAllSecialityDoc(this.txtCompanyId.Text);
    }

    protected void ExtendedGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ExtendedGroup.SelectedValue.Equals("---Select---"))
        {
            this.tvwmenu.PopulateNodesFromClient = true;
            this.tvwmenu.Nodes.RemoveAt(0);
            TreeNode treeNode = new TreeNode("Document Manager", "0")
            {
                PopulateOnDemand = true
            };
            this.tvwmenu.Nodes.Add(treeNode);
            this.tvwmenu.ExpandAll();
            this.lbSelectedDocs.Items.Clear();
            this.txtMisDocEmailAddress.Text = "";
            this.txtInterval.Text = "";
            return;
        }
        if (this.lbSelectedDocs.Items.Count > 0)
        {
            for (int i = 0; i < this.lbSelectedDocs.Items.Count + 1; i++)
            {
                this.lbSelectedDocs.Items.RemoveAt(0);
            }
            this.lbSelectedDocs.Items.Clear();
        }
        this.txtMisDocEmailAddress.Text = "";
        this.hfselectedNode.Value = "";
        Notification notification = new Notification();
        this.dsSpecialityDoc = notification.GetEmailForMissingDocument(Convert.ToInt32(this.extddlNotification.Text), this.txtCompanyId.Text, this.ExtendedGroup.SelectedItem.Text.ToString());
        this.txtInterval.Text = this.GetInterval(this.ExtendedGroup.SelectedItem.Text.ToString(), this.txtCompanyId.Text);
        for (int j = 0; j < this.dsSpecialityDoc.Tables[0].Rows.Count; j++)
        {
            ListItem listItem = new ListItem()
            {
                Text = "",
                Value = ""
            };
            this.lbSelectedDocs.Items.Add(listItem);
        }
        string str = "";
        for (int k = 0; k < this.dsSpecialityDoc.Tables[1].Rows.Count; k++)
        {
            str = (str != "" ? string.Concat(str, ",", this.dsSpecialityDoc.Tables[1].Rows[k][0].ToString()) : this.dsSpecialityDoc.Tables[1].Rows[k][0].ToString());
        }
        this.txtMisDocEmailAddress.Text = str;
        this.tvwmenu.PopulateNodesFromClient = true;
        this.tvwmenu.Nodes.RemoveAt(0);
        TreeNode treeNode1 = new TreeNode("Document Manager", "0")
        {
            PopulateOnDemand = true
        };
        this.tvwmenu.Nodes.Add(treeNode1);
        this.tvwmenu.ExpandAll();
        int count = this.lbSelectedDocs.Items.Count;
        for (int l = 0; l < count; l++)
        {
            if (this.lbSelectedDocs.Items[l].Value.Equals(""))
            {
                this.lbSelectedDocs.Items.RemoveAt(l);
                l--;
                count--;
            }
        }
    }

    private void FillChildMenu(TreeNode node)
    {
        string value = node.Value;
        DataSet dataSet = new DataSet();
        dataSet = this.objBillSysDocBO.GetChildNodes(Convert.ToInt32(value), this.txtCompanyId.Text);
        Notification notification = new Notification();
        if (!this.ExtendedGroup.Visible)
        {
            this.dsSpecialityDoc = notification.GetEmailForMissingDocument(Convert.ToInt32(this.extddlNotification.Text), this.txtCompanyId.Text, "");
        }
        else
        {
            this.dsSpecialityDoc = notification.GetEmailForMissingDocument(Convert.ToInt32(this.extddlNotification.Text), this.txtCompanyId.Text, this.ExtendedGroup.SelectedItem.Text.ToString());
        }
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
                for (int i = 0; i < this.dsSpecialityDoc.Tables[0].Rows.Count; i++)
                {
                    if (row["I_NODE_ID"].ToString().Equals(this.dsSpecialityDoc.Tables[0].Rows[i][0].ToString()))
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
                        string[] value1 = new string[] { this.hfselectedNode.Value, str, ">>", row["SZ_NODE_NAME"].ToString(), "~", this.dsSpecialityDoc.Tables[0].Rows[i][1].ToString(), "~", row["I_NODE_ID"].ToString(), "," };
                        hiddenField.Value = string.Concat(value1);
                        ListItem listItem = new ListItem()
                        {
                            Text = string.Concat(str, ">>", row["SZ_NODE_NAME"].ToString()),
                            Value = string.Concat(this.dsSpecialityDoc.Tables[0].Rows[i][1].ToString(), "~", row["I_NODE_ID"].ToString())
                        };
                        this.lbSelectedDocs.Items.Add(listItem);
                        HiddenField hiddenField1 = this.hfOrder;
                        string[] strArrays1 = new string[] { this.hfOrder.Value, row["I_NODE_ID"].ToString(), "~", this.dsSpecialityDoc.Tables[0].Rows[i][2].ToString(), "," };
                        hiddenField1.Value = string.Concat(strArrays1);
                        DAO_Assign_Doc dAOAssignDoc = new DAO_Assign_Doc()
                        {
                            SelectedId = row["I_NODE_ID"].ToString(),
                            SelectedText = string.Concat(str, ">>", row["SZ_NODE_NAME"].ToString()),
                            ORDER = Convert.ToInt32(this.dsSpecialityDoc.Tables[0].Rows[i][2])
                        };
                        ArrayList arrayLists = new ArrayList();
                        if (this.Session["SelectedDocList1"] != null)
                        {
                            arrayLists = (ArrayList)this.Session["SelectedDocList1"];
                            this.Session["SelectedDocList1"] = arrayLists;
                            int num = 0;
                            int num1 = 0;
                            while (num1 < arrayLists.Count)
                            {
                                DAO_Assign_Doc dAOAssignDoc1 = new DAO_Assign_Doc();
                                if (!((DAO_Assign_Doc)arrayLists[num1]).SelectedId.Equals(dAOAssignDoc.SelectedId))
                                {
                                    num1++;
                                }
                                else
                                {
                                    num = 1;
                                    break;
                                }
                            }
                            if (num == 0)
                            {
                                arrayLists.Add(dAOAssignDoc);
                            }
                        }
                        else
                        {
                            arrayLists.Add(dAOAssignDoc);
                            this.Session["SelectedDocList1"] = arrayLists;
                        }
                        if (this.Session["SelectedDocList1"] != null)
                        {
                            ArrayList item = new ArrayList();
                            item = (ArrayList)this.Session["SelectedDocList1"];
                            for (int k = 0; k < item.Count; k++)
                            {
                                DAO_Assign_Doc dAOAssignDoc2 = new DAO_Assign_Doc();
                                if (((DAO_Assign_Doc)arrayLists[k]).SelectedId.Equals(row["I_NODE_ID"].ToString()))
                                {
                                    treeNode.Checked = true;
                                }
                            }
                        }
                    }
                    else if (this.Session["SelectedDocList1"] != null)
                    {
                        ArrayList arrayLists1 = new ArrayList();
                        ArrayList item1 = (ArrayList)this.Session["SelectedDocList1"];
                    }
                }
                node.ChildNodes.Add(treeNode);
                this.FillChildMenu(treeNode);
            }
        }
    }

    private void FillChildMenulaw(TreeNode node)
    {
        string value = node.Value;
        DataSet dataSet = new DataSet();
        dataSet = this.objBillSysDocBO.GetChildNodes(Convert.ToInt32(value), this.txtCompanyId.Text);
        UploadEmailDocument uploadEmailDocument = new UploadEmailDocument();
        this.dsSpecialityDoc = uploadEmailDocument.GetDataUploadDocument(this.txtCompanyId.Text, this.extddlLawFirm.Text);
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
                for (int i = 0; i < this.dsSpecialityDoc.Tables[0].Rows.Count; i++)
                {
                    if (row["I_NODE_ID"].ToString().Equals(this.dsSpecialityDoc.Tables[0].Rows[i][0].ToString()))
                    {
                        treeNode.Checked = true;
                        string str = node.ToolTip;
                        string[] strArrays = new string[10];
                        strArrays = node.ValuePath.Split(new char[] { '/' });
                        for (int j = 0; j < (int)strArrays.Length; j++)
                        {
                            str = str.Replace(string.Concat("(", strArrays[j], ")"), "");
                        }
                        HiddenField hiddenField = this.hfselectedNodelaw;
                        string[] value1 = new string[] { this.hfselectedNodelaw.Value, str, ">>", row["SZ_NODE_NAME"].ToString(), "~", row["I_NODE_ID"].ToString(), "," };
                        hiddenField.Value = string.Concat(value1);
                        ListItem listItem = new ListItem()
                        {
                            Text = string.Concat(str, ">>", row["SZ_NODE_NAME"].ToString()),
                            Value = row["I_NODE_ID"].ToString()
                        };
                        this.lbSelectedDocslaw.Items.Add(listItem);
                        HiddenField hiddenField1 = this.hfOrderlaw;
                        string[] strArrays1 = new string[] { this.hfOrderlaw.Value, row["I_NODE_ID"].ToString(), "~", this.dsSpecialityDoc.Tables[0].Rows[i][4].ToString(), "," };
                        hiddenField1.Value = string.Concat(strArrays1);
                        DAO_Assign_Doc dAOAssignDoc = new DAO_Assign_Doc()
                        {
                            SelectedId = row["I_NODE_ID"].ToString(),
                            SelectedText = string.Concat(str, ">>", row["SZ_NODE_NAME"].ToString()),
                            ORDER = Convert.ToInt32(this.dsSpecialityDoc.Tables[0].Rows[i][4])
                        };
                        ArrayList arrayLists = new ArrayList();
                        if (this.Session["SelectedDocList1law"] != null)
                        {
                            arrayLists = (ArrayList)this.Session["SelectedDocList1law"];
                            this.Session["SelectedDocList1law"] = arrayLists;
                            int num = 0;
                            int num1 = 0;
                            while (num1 < arrayLists.Count)
                            {
                                DAO_Assign_Doc dAOAssignDoc1 = new DAO_Assign_Doc();
                                if (!((DAO_Assign_Doc)arrayLists[num1]).SelectedId.Equals(dAOAssignDoc.SelectedId))
                                {
                                    num1++;
                                }
                                else
                                {
                                    num = 1;
                                    break;
                                }
                            }
                            if (num == 0)
                            {
                                arrayLists.Add(dAOAssignDoc);
                            }
                        }
                        else
                        {
                            arrayLists.Add(dAOAssignDoc);
                            this.Session["SelectedDocList1law"] = arrayLists;
                        }
                        if (this.Session["SelectedDocList1law"] != null)
                        {
                            ArrayList item = new ArrayList();
                            item = (ArrayList)this.Session["SelectedDocList1law"];
                            for (int k = 0; k < item.Count; k++)
                            {
                                DAO_Assign_Doc dAOAssignDoc2 = new DAO_Assign_Doc();
                                if (((DAO_Assign_Doc)arrayLists[k]).SelectedId.Equals(row["I_NODE_ID"].ToString()))
                                {
                                    treeNode.Checked = true;
                                }
                            }
                        }
                    }
                    else if (this.Session["SelectedDocList1law"] != null)
                    {
                        ArrayList arrayLists1 = new ArrayList();
                        ArrayList item1 = (ArrayList)this.Session["SelectedDocList1law"];
                    }
                }
                node.ChildNodes.Add(treeNode);
                this.FillChildMenulaw(treeNode);
            }
        }
    }

    public void FillMasterMenu(TreeNode node)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.objBillSysDocBO.GetMasterNodes(this.txtCompanyId.Text);
        Notification notification = new Notification();
        if (!this.ExtendedGroup.Visible)
        {
            this.dsSpecialityDoc = notification.GetEmailForMissingDocument(Convert.ToInt32(this.extddlNotification.Text), this.txtCompanyId.Text, "");
        }
        else
        {
            this.dsSpecialityDoc = notification.GetEmailForMissingDocument(Convert.ToInt32(this.extddlNotification.Text), this.txtCompanyId.Text, this.ExtendedGroup.SelectedItem.Text.ToString());
        }
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
                for (int i = 0; i < this.dsSpecialityDoc.Tables[0].Rows.Count; i++)
                {
                    if (row["I_NODE_ID"].ToString().Equals(this.dsSpecialityDoc.Tables[0].Rows[i][0].ToString()))
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
                        string[] value = new string[] { this.hfselectedNode.Value, str, ">>", row["SZ_NODE_NAME"].ToString(), "~", this.dsSpecialityDoc.Tables[0].Rows[i][1].ToString(), "~", row["I_NODE_ID"].ToString(), "," };
                        hiddenField.Value = string.Concat(value);
                        HiddenField hiddenField1 = this.hfOrder;
                        string[] value1 = new string[] { this.hfOrder.Value, row["I_NODE_ID"].ToString(), "~", this.dsSpecialityDoc.Tables[0].Rows[i][2].ToString(), "," };
                        hiddenField1.Value = string.Concat(value1);
                        ListItem listItem = new ListItem()
                        {
                            Text = string.Concat(str, ">>", row["SZ_NODE_NAME"].ToString()),
                            Value = string.Concat(this.dsSpecialityDoc.Tables[0].Rows[i][1].ToString(), "~", row["I_NODE_ID"].ToString())
                        };
                        this.lbSelectedDocs.Items.Insert(Convert.ToInt32(this.dsSpecialityDoc.Tables[0].Rows[i][2]), listItem);
                        DAO_Assign_Doc dAOAssignDoc = new DAO_Assign_Doc()
                        {
                            SelectedId = row["I_NODE_ID"].ToString(),
                            SelectedText = string.Concat(str, ">>", row["SZ_NODE_NAME"].ToString()),
                            ORDER = Convert.ToInt32(this.dsSpecialityDoc.Tables[0].Rows[i][2])
                        };
                        ArrayList arrayLists = new ArrayList();
                        if (this.Session["SelectedDocList1"] != null)
                        {
                            arrayLists = (ArrayList)this.Session["SelectedDocList1"];
                            arrayLists.Add(dAOAssignDoc);
                            this.Session["SelectedDocList1"] = arrayLists;
                        }
                        else
                        {
                            arrayLists.Add(dAOAssignDoc);
                            this.Session["SelectedDocList1"] = arrayLists;
                        }
                        if (this.Session["SelectedDocList1"] != null)
                        {
                            ArrayList item = new ArrayList();
                            item = (ArrayList)this.Session["SelectedDocList1"];
                            for (int k = 0; k < item.Count; k++)
                            {
                                DAO_Assign_Doc dAOAssignDoc1 = new DAO_Assign_Doc();
                                if (((DAO_Assign_Doc)arrayLists[k]).SelectedId.Equals(row["I_NODE_ID"].ToString()))
                                {
                                    treeNode.Checked = true;
                                }
                            }
                        }
                    }
                    else if (this.Session["SelectedDocList1"] != null)
                    {
                        ArrayList arrayLists1 = new ArrayList();
                        ArrayList item1 = (ArrayList)this.Session["SelectedDocList1"];
                    }
                }
                node.ChildNodes.Add(treeNode);
            }
        }
    }

    public void FillMasterMenulaw(TreeNode node)
    {
        DataSet dataSet = new DataSet();
        dataSet = this.objBillSysDocBO.GetMasterNodes(this.txtCompanyId.Text);
        UploadEmailDocument uploadEmailDocument = new UploadEmailDocument();
        this.dsSpecialityDoc = uploadEmailDocument.GetDataUploadDocument(this.txtCompanyId.Text, this.extddlLawFirm.Text);
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
                for (int i = 0; i < this.dsSpecialityDoc.Tables[0].Rows.Count; i++)
                {
                    if (row["I_NODE_ID"].ToString().Equals(this.dsSpecialityDoc.Tables[0].Rows[i][0].ToString()))
                    {
                        treeNode.Checked = true;
                        string str = node.ToolTip;
                        string[] strArrays = new string[10];
                        strArrays = node.ValuePath.Split(new char[] { '/' });
                        for (int j = 0; j < (int)strArrays.Length; j++)
                        {
                            str = str.Replace(string.Concat("(", strArrays[j], ")"), "");
                        }
                        HiddenField hiddenField = this.hfselectedNodelaw;
                        string[] value = new string[] { this.hfselectedNodelaw.Value, str, ">>", row["SZ_NODE_NAME"].ToString(), "~", row["I_NODE_ID"].ToString(), "," };
                        hiddenField.Value = string.Concat(value);
                        HiddenField hiddenField1 = this.hfOrderlaw;
                        string[] value1 = new string[] { this.hfOrderlaw.Value, row["I_NODE_ID"].ToString(), "~", this.dsSpecialityDoc.Tables[0].Rows[i][4].ToString(), "," };
                        hiddenField1.Value = string.Concat(value1);
                        ListItem listItem = new ListItem()
                        {
                            Text = string.Concat(str, ">>", row["SZ_NODE_NAME"].ToString()),
                            Value = row["I_NODE_ID"].ToString()
                        };
                        try
                        {
                            this.lbSelectedDocslaw.Items.Insert(Convert.ToInt32(this.dsSpecialityDoc.Tables[0].Rows[i][4]), listItem);
                        }
                        catch
                        {
                        }
                        DAO_Assign_Doc dAOAssignDoc = new DAO_Assign_Doc()
                        {
                            SelectedId = row["I_NODE_ID"].ToString(),
                            SelectedText = string.Concat(str, ">>", row["SZ_NODE_NAME"].ToString()),
                            ORDER = Convert.ToInt32(this.dsSpecialityDoc.Tables[0].Rows[i][4])
                        };
                        ArrayList arrayLists = new ArrayList();
                        if (this.Session["SelectedDocList1law"] != null)
                        {
                            arrayLists = (ArrayList)this.Session["SelectedDocList1law"];
                            arrayLists.Add(dAOAssignDoc);
                            this.Session["SelectedDocList1law"] = arrayLists;
                        }
                        else
                        {
                            arrayLists.Add(dAOAssignDoc);
                            this.Session["SelectedDocList1law"] = arrayLists;
                        }
                        if (this.Session["SelectedDocList1law"] != null)
                        {
                            ArrayList item = new ArrayList();
                            item = (ArrayList)this.Session["SelectedDocList1law"];
                            for (int k = 0; k < item.Count; k++)
                            {
                                DAO_Assign_Doc dAOAssignDoc1 = new DAO_Assign_Doc();
                                if (((DAO_Assign_Doc)arrayLists[k]).SelectedId.Equals(row["I_NODE_ID"].ToString()))
                                {
                                    treeNode.Checked = true;
                                }
                            }
                        }
                    }
                    else if (this.Session["SelectedDocList1law"] != null)
                    {
                        ArrayList arrayLists1 = new ArrayList();
                        ArrayList item1 = (ArrayList)this.Session["SelectedDocList1law"];
                    }
                }
                node.ChildNodes.Add(treeNode);
            }
        }
    }

    protected string GetInterval(string groupname, string CompanyId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str;
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    Connection = sqlConnection
                };
                string str1 = "";
                if (this.extddlNotification.Selected_Text != "Provider Reports")
                {
                    string[] strArrays = new string[] { "SELECT I_INTERVAL FROM MST_MISSING_DOCUMENTS_EMAIL_DATE WHERE SZ_GROUP_NAME LIKE '", groupname, "' AND SZ_COMPANY_ID='", CompanyId, "'" };
                    sqlCommand.CommandText = string.Concat(strArrays);
                    sqlConnection.Open();
                    str1 = sqlCommand.ExecuteScalar().ToString();
                }
                else
                {
                    sqlCommand.CommandText = string.Concat("SELECT I_INTERVAL FROM MST_MISSING_DOCUMENTS_EMAIL_DATE WHERE SZ_GROUP_NAME IS NULL AND SZ_COMPANY_ID='", CompanyId, "' AND SZ_NOTIFICATION_CODE='PR'");
                    sqlConnection.Open();
                    str1 = sqlCommand.ExecuteScalar().ToString();
                }
                str = str1;
            }
            catch (Exception ex)
            {
                str = "";
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
           
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return str;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
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

    public void Node_Populatelaw(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.ChildNodes.Count == 0)
        {
            switch (e.Node.Depth)
            {
                case 0:
                {
                    this.FillMasterMenulaw(e.Node);
                    return;
                }
                case 1:
                {
                    this.FillChildMenulaw(e.Node);
                    return;
                }
                case 2:
                {
                    this.FillChildMenulaw(e.Node);
                    return;
                }
                case 3:
                {
                    this.FillChildMenulaw(e.Node);
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
        this.extddlLawFirm.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.extddlNotification.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        if (!base.IsPostBack)
        {
            this.extddlLawFirm.Visible = false;
            this.txtEmailAddress.Visible = false;
            this.tblMisDoc.Visible = false;
            this.tbllawfirm.Visible = false;
        }
    }

    private void setuplaoddcumentLawfirm()
    {
        if (this.lbSelectedDocslaw.Items.Count > 0)
        {
            for (int i = 0; i < this.lbSelectedDocslaw.Items.Count + 1; i++)
            {
                this.lbSelectedDocslaw.Items.RemoveAt(0);
            }
            this.lbSelectedDocslaw.Items.Clear();
        }
        this.hfselectedNodelaw.Value = "";
        UploadEmailDocument uploadEmailDocument = new UploadEmailDocument();
        this.dsSpecialityDoc = uploadEmailDocument.GetDataUploadDocument(this.txtCompanyId.Text, this.extddlLawFirm.Text.ToString());
        for (int j = 0; j < this.dsSpecialityDoc.Tables[0].Rows.Count; j++)
        {
            ListItem listItem = new ListItem()
            {
                Text = "",
                Value = ""
            };
            this.lbSelectedDocslaw.Items.Add(listItem);
        }
        if (this.dsSpecialityDoc.Tables[0].Rows.Count > 0)
        {
            this.tvwmenulaw.PopulateNodesFromClient = true;
            this.tvwmenulaw.Nodes.RemoveAt(0);
            TreeNode treeNode = new TreeNode("Document Manager", "0")
            {
                PopulateOnDemand = true
            };
            this.tvwmenulaw.Nodes.Add(treeNode);
            this.tvwmenulaw.ExpandAll();
            int count = this.lbSelectedDocslaw.Items.Count;
            for (int k = 0; k < count; k++)
            {
                if (this.lbSelectedDocslaw.Items[k].Value.Equals(""))
                {
                    this.lbSelectedDocslaw.Items.RemoveAt(k);
                    k--;
                    count--;
                }
            }
        }
    }
}
