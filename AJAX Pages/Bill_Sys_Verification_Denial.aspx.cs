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
using PDFValueReplacement;
using System.IO;
using System.Data.SqlClient;
using RequiredDocuments;

public partial class AJAX_Pages_Bill_Sys_Verification_Denial : PageBase
{
    Bill_Sys_NotesBO _bill_Sys_NotesBO;
    Bill_Sys_BillTransaction_BO _obj;
   string strLinkPath = null;
    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnSaveDesc.Attributes.Add("onclick", "return checkdenial()");
            //btnSaveDesc.Attributes.Add("onclick", "return checkedDate()");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddenial.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {
                lblSaveDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
                
                txtViewBillNumber.Text = Request.QueryString["BillNo"].ToString();
                imgSavebtnToDate.Visible = true;
                txtSaveDate.Visible = true;
                BindGrid();
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


    protected void btnSaveDesc_Click(object sender, EventArgs e)
    {
        Boolean saveflag = false;
        ArrayList _denialReason = new ArrayList();
        Bill_Sys_BillTransaction_BO obj;
        Bill_Sys_Verification_Desc _objDesc;
        Boolean updateFlag = false;
        string stre = txtViewBillNumber.Text;
        char ch = ',';
        string[] s1 = stre.Split(ch);
        char ch1 = ',';
        String[] DenialReason = hfdenialReason.Value.Split(ch1);
        Boolean flag = false;
        ArrayList objSplit = new ArrayList();
        for (int i = 0; i < s1.Length; i++)
        {
            objSplit.Add(s1[i].ToString());
        }
        ArrayList sz_status_code = new ArrayList();
        for (int k = 0; k < objSplit.Count; k++)
        {
            ArrayList objAL = new ArrayList();
            String szBillNo = objSplit[k].ToString();

            _objDesc = new Bill_Sys_Verification_Desc();
            _objDesc.sz_bill_no = szBillNo;
            _objDesc.sz_description = txtSaveDescription.Text;
            _objDesc.sz_verification_date = txtSaveDate.Text;
            _objDesc.i_verification = 3;
            _objDesc.sz_company_id = txtCompanyID.Text;
            _objDesc.sz_user_id = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            _objDesc.sz_flag = "DEN";
            objAL.Add(_objDesc);
            obj = new Bill_Sys_BillTransaction_BO();
            sz_status_code.Add(obj.InsertUpdateBillStatus(objAL));
            updateFlag = true;
        }

        if (sz_status_code.Count>0)
        {
            obj = new Bill_Sys_BillTransaction_BO();
            String[] removedeial = hfremovedenialreason.Value.Split(',');
            ArrayList objRemove = new ArrayList();
            for (int i = 0; i < removedeial.Length - 1; i++)
            {
                objRemove.Add(removedeial[i].ToString());
            }
            for (int j = 0; j < DenialReason.Length - 1; j++)
            {
                flag = false;
                for (int k = 0; k < objRemove.Count; k++)
                {
                    if (DenialReason[j].ToString() == objRemove[k].ToString())
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    _denialReason.Add(DenialReason[j].ToString());
                }
            }

            if (sz_status_code.Count > 0)
            {

                for (int l = 0; l < objSplit.Count; l++)
                {
                    obj.UpdateDenialReason(sz_status_code[l].ToString(), _denialReason, objSplit[l].ToString());
                }
                lbSelectedDenial.Items.Clear();
                extddenial.Text = "--- Select ---";

            }
            BindGrid();
            saveflag = true;
        }
        else
        {
            usrMessage1.PutMessage("Please Add Denial reason");
            usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
            usrMessage1.Show();
        }
                
       if (updateFlag)
                {
                   
                    Session["NODETYPE"] = "NFDEN";
                    BindGrid();
                    usrMessage1.PutMessage("Saved successfully.");
                    usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage1.Show();
                }
            }
               
                ////uploadflag = true;
                //if (hfremovedenialreason.Value != "")
                //{
                //    String[] removedeial = hfremovedenialreason.Value.Split(',');
                //    ArrayList objRemove = new ArrayList();
                //    for (int i = 0; i < removedeial.Length - 1; i++)
                //    {
                //        objRemove.Add(removedeial[i].ToString());
                //    }
                //    for (int j = 0; j < DenialReason.Length - 1; j++)
                //    {
                //        flag = false;
                //        for (int k = 0; k < objRemove.Count; k++)
                //        {
                //            if (DenialReason[j].ToString() == objRemove[k].ToString())
                //            {
                //                flag = true;
                //                break;
                //            }
                //        }

                //        if (!flag)
                //        {
                //            _denialReason.Add(DenialReason[j].ToString());
                //        }
                //    }
                //}
                //else
                //{
                //    for (int j = 0; j < DenialReason.Length - 1; j++)
                //    {
                //        _denialReason.Add(DenialReason[j].ToString());
                //    }
                //}


                //if (sz_status_code != "")
                //{
                //    for (int i = 0; i < objBillNo.Count; i++)
                //    {

                //        obj.UpdateDenialReason(sz_status_code, _denialReason, objBillNo[i].ToString());
                //    }
                   
               // }
          
       
            
    protected void btnCancelDesc_Click(object sender, EventArgs e)
    {
        ClearControls();
        
    }

    public void ClearControls()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtSaveDate.Text = "";
            txtSaveDescription.Text = "";
            extddenial.Selected_Text = "--- Select ---";
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
    public void BindGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dset;
        try
        {
            string str = txtViewBillNumber.Text;
            string str2 = str.Replace(",", "','");
            str = "'" + str2 + "'";
            txtTest.Text = str;
            Bill_Sys_NotesBO _bill_Sys_NotesBO = new Bill_Sys_NotesBO();
            dset = new DataSet();
            dset = _bill_Sys_NotesBO.PopulateDenialGrid(txtCompanyID.Text, txtTest.Text);
            grdVerificationDen.DataSource = dset.Tables[0];
            grdVerificationDen.DataBind();

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

    protected void lnkscan_Click(object sender, EventArgs e)
    {
        int iindex = grdVerificationDen.SelectedIndex;
        LinkButton btn = (LinkButton)sender;
        TableCell tc = (TableCell)btn.Parent;
        DataGridItem it = (DataGridItem)tc.Parent;
        int index = it.ItemIndex;
        Session["NODETYPESCAN"] = "";
        Session["SCANVERID"] = it.Cells[5].Text;
        if (it.Cells[1].Text.ToLower().Equals("denial"))
        {
            Session["NODETYPESCAN"] = "NFDEN";
        }
        else
        {
            Session["NODETYPESCAN"] = "NFVER";
        }
        Session["ScanBillNo"] = txtViewBillNumber.Text;

        RedirectToScanApp(iindex);

    }
    public void RedirectToScanApp(int iindex)
    {
        iindex = (int)grdVerificationDen.SelectedIndex;
        string szUrl = ConfigurationManager.AppSettings["webscanurl"].ToString();
        Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
        string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["NODETYPESCAN"].ToString());
        szUrl = szUrl + "&Flag=ReqVeri" + "&CompanyId=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "&CaseId=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "&UserName=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME + "&CompanyName=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        szUrl = szUrl + "&CaseNo=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO + "&PName=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME + "&NodeId=" + NodeId + "&BillNo=" + Session["ScanBillNo"].ToString() + "&UserId=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID + "&StatusID=" + Session["SCANVERID"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szUrl + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }
    protected void lnkuplaod_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        TableCell tc = (TableCell)btn.Parent;
        DataGridItem it = (DataGridItem)tc.Parent;
        int index = it.ItemIndex;
        Session["BillNo"] = txtViewBillNumber.Text;
        Session["NODETYPESCAN"] = "";

        Session["VERID"] = it.Cells[5].Text;

        if (it.Cells[1].Text.ToLower().Equals("denial"))
        {
            Session["NODETYPE"] = "NFDEN";
        }
        else
        {
            Session["NODETYPE"] = "NFVER";
        }
        Page.RegisterStartupScript("ss", "<script language='javascript'>showUploadFilePopup();</script>");
   }

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        try
        {
            if (!fuUploadReport.HasFile)
            {
                Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");

                return;
            }

            String szDefaultPath = objNF3Template.getPhysicalPath();
            int ImageId = 0;
            String szDestinationDir = "";

            szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();

            string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["NODETYPE"].ToString());

            RequiredDocuments.Bill_Sys_RequiredDocumentBO bo = new RequiredDocuments.Bill_Sys_RequiredDocumentBO();
            string szNodePath = bo.GetNodePath(NodeId, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            szDestinationDir = szNodePath + "\\";
            szDestinationDir = szDestinationDir.Replace("\\", "/");
            strLinkPath = szDestinationDir + fuUploadReport.FileName;
            if (!Directory.Exists(szDefaultPath + szDestinationDir))
            {
                Directory.CreateDirectory(szDefaultPath + szDestinationDir);
            }

            fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);


            ArrayList objAL = new ArrayList();

            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            objAL.Add("");
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            objAL.Add(fuUploadReport.FileName);
            objAL.Add(szDestinationDir);
            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
            objAL.Add(NodeId);


            string ImgId = objNF3Template.SaveDocumentData(objAL);

            if (!ImgId.Equals(""))
            {

                objNF3Template.InsertPaymentImage(Session["BillNo"].ToString(), txtCompanyID.Text, ImgId, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["VERID"].ToString(), "VERIFICATIONPOPUP");
            }

            usrMessage1.PutMessage("File Upload Successfully!");
            usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage1.Show();

            BindGrid();
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
