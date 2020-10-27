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
using System.IO;
using System.Collections;
using System.Text;
using log4net;

public partial class AJAX_Pages_Bill_Sys_DenailPopup : PageBase
{
    Bill_Sys_UploadFile _objUploadFile;
    private static ILog log = LogManager.GetLogger("Bill_Sys_DenailPopup");
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            log.Debug("InsidePage load AJAX_Pages_Bill_Sys_DenailPopup ");
            lblSaveDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            log.Debug("company_id" + txtCompanyID.Text);
            txtbillnumber.Text = Request.QueryString["szBillNo"].ToString();
            log.Debug("Bill No" + txtbillnumber.Text);
            txtViewBillNumber.Text = Request.QueryString["szBillNo"].ToString();
            log.Debug("Bill No" + txtViewBillNumber.Text);
            txtCaseID.Text = Request.QueryString["szCaseId"].ToString();
            log.Debug("Case ID" + txtCaseID.Text);
            txtvisitdatedenial.Text = Session["PROC_DATE"].ToString();
            log.Debug("Proc Date" + txtvisitdatedenial.Text);
            btnDelete.Attributes.Add("onclick", "return confirm_bill_delete();");
            btnSavesent.Attributes.Add("onclick", "return checkedDate()");
            if (!IsPostBack)
            {
                hdnBillNo.Value = txtViewBillNumber.Text;
                //hdnSpecialty.Value = Request.QueryString["SpecialtyId"].ToString();
                hdnCaseId.Value =  Request.QueryString["szCaseId"].ToString();
                extddenial.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                log.Debug("Befor Bind");
                BindGridforDenial();
                log.Debug("After Bind");

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
    public void BindGridforDenial()
    {//atul
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dset;
        try
        {
            grdVerificationReq.Columns[10].Visible = true;
            Bill_Sys_NotesBO _bill_Sys_NotesBO = new Bill_Sys_NotesBO();
            dset = new DataSet();
            dset = _bill_Sys_NotesBO.GetBillDetailsFillGridDenial(txtCompanyID.Text, txtbillnumber.Text);
            grdVerificationReq.DataSource = dset.Tables[0];
            grdVerificationReq.DataBind();
            grdVerificationReq.Columns[10].Visible = false;

            for (int i = 0; i < grdVerificationReq.Items.Count; i++)
            {
                if (grdVerificationReq.Items[i].Cells[10].Text != "&nbsp;")
                {
                    hfindex.Value = "";
                    hfverificationId.Value = "";
                    hfindex.Value = i.ToString();
                    hfverificationId.Value = grdVerificationReq.Items[i].Cells[5].Text.ToString();
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

    protected void grdVerificationReq_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {

                //Session["TYPEID"] = e.Item.Cells[5].Text.ToString();
                //Session["ANSID"] = e.Item.Cells[12].Text.ToString();
                //szansid = e.Item.Cells[12].Text.ToString();


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
        Bill_Sys_BillTransaction_BO obj;
        Bill_Sys_Verification_Desc _objDesc;
        Boolean updateFlag = false;
        string sz_status_code = "";
        ArrayList arr_node_type = new ArrayList();
        string Bill_number = "";
        Boolean uploadflag = false;
        ArrayList arrCaseId = new ArrayList();
        ArrayList arrSpeciality = new ArrayList();
        string szSpec = "";
        ViewState["Process"] = "";
        ViewState["Process"] = "DEN";
        if (!hfdenialReason.Value.Equals(""))
        {
            char ch = ',';
            String[] DenialReason = hfdenialReason.Value.Split(ch);
            ArrayList objAL = new ArrayList();
            ArrayList objBillNo = new ArrayList();
            //ArrayList arrSpeciality=new ArrayList();
            ArrayList _denialReason = new ArrayList();
            Boolean flag = false;

            string szBillNo = txtbillnumber.Text;
            arrSpeciality.Add(Session["Speciality"].ToString());
            arrCaseId.Add(txtCaseID.Text);
            _objDesc = new Bill_Sys_Verification_Desc();
            _objDesc.sz_bill_no = szBillNo;
            _objDesc.sz_description = txtSaveDescription.Text;
            _objDesc.sz_verification_date = txtSaveDate.Text;
            _objDesc.i_verification = 3;
            _objDesc.sz_company_id = txtCompanyID.Text;
            _objDesc.sz_user_id = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            _objDesc.sz_flag = "DEN";
            objAL.Add(_objDesc);
            updateFlag = true;
            objBillNo.Add(szBillNo);

            _objUploadFile = new Bill_Sys_UploadFile();
            _objUploadFile.sz_bill_no = objBillNo;
            _objUploadFile.sz_company_id = txtCompanyID.Text;
            _objUploadFile.sz_flag = "DEN";
            _objUploadFile.sz_case_id = arrCaseId;
            _objUploadFile.sz_speciality_id = arrSpeciality;
            if (updateFlag)
            {

                obj = new Bill_Sys_BillTransaction_BO();
                sz_status_code = obj.InsertUpdateBillStatus(objAL);
                _objUploadFile.sz_StatusCode = sz_status_code;
                Session["VSUpload"] = _objUploadFile;

                uploadflag = true;

                arr_node_type = obj.Get_Node_Type(objAL);
                uploadflag = true;

                if (hfremovedenialreason.Value != "")
                {
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
                }
                else
                {
                    for (int j = 0; j < DenialReason.Length - 1; j++)
                    {
                        _denialReason.Add(DenialReason[j].ToString());
                    }
                }
                if (sz_status_code != "")
                {
                    for (int i = 0; i < objBillNo.Count; i++)
                    {

                        obj.UpdateDenialReason(sz_status_code, _denialReason, objBillNo[i].ToString());
                    }
                    if (arr_node_type.Contains("NFVER"))
                    {
                        arr_node_type.Clear();
                        arr_node_type.Add("NFDEN");
                        Session["NODETYPE"] = arr_node_type;
                    }
                    else
                    {
                        //Session["NODETYPE"] = "NFVER";
                        Session["NODETYPE"] = arr_node_type;
                    }

                    BindGridforDenial();
                    ClearControls();
                    #region Activity_Log
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "DENIAL_ADDED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Bill Id : " + szBillNo +", Reason : " + String.Join("," ,_denialReason.ToArray()) ;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    this._DAO_NOTES_EO.SZ_CASE_ID = new Bill_Sys_BillingCompanyDetails_BO().GetCaseID(szBillNo, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    #endregion
                    usrMessage.PutMessage("Denial Save Successfully ...!");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                }

            }


        }
    }

    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;

    protected void lnkuplaod_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        Bill_Sys_Verification_Desc _objDesc;
        ArrayList arr_node_type = new ArrayList();
        ArrayList objAL = new ArrayList();
        Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
        TableCell tc = (TableCell)btn.Parent;
        DataGridItem it = (DataGridItem)tc.Parent;
        int index = it.ItemIndex;
        Session["BillNo"] = txtViewBillNumber.Text;
        Session["NODETYPESCAN"] = "";
        Session["NODETYPE"] = "";
        Session["VERID"] = it.Cells[5].Text;

        ArrayList arrBillNo = new ArrayList();
        ArrayList arrSpec = new ArrayList();
        ArrayList arrCaseId = new ArrayList();
        if (it.Cells[1].Text.ToLower().Equals("denial"))
        {
            _objDesc = new Bill_Sys_Verification_Desc();
            _objDesc.sz_bill_no = txtViewBillNumber.Text;
            _objDesc.sz_description = "";
            _objDesc.sz_verification_date = "";
            _objDesc.i_verification = 0;
            _objDesc.sz_company_id = txtCompanyID.Text;
            _objDesc.sz_user_id = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            _objDesc.sz_flag = "DEN";
            objAL.Add(_objDesc);
            arr_node_type = obj.Get_Node_Type(objAL);

            if (arr_node_type.Contains("NFVER"))
            {
                arr_node_type.Clear();
                arr_node_type.Add("NFVER");
                Session["NODETYPE"] = arr_node_type;
            }
            else
            {
                //Session["NODETYPE"] = "NFVER";
                Session["NODETYPE"] = arr_node_type;
            }
            //Session["NODETYPE"] = "NFDEN";



            arrCaseId.Add(txtCaseID.Text);
            arrBillNo.Add(txtViewBillNumber.Text);
            arrSpec.Add(Session["Speciality"].ToString());

            Bill_Sys_UploadFile _objUploadFile = new Bill_Sys_UploadFile();
            _objUploadFile.sz_bill_no = arrBillNo;
            _objUploadFile.sz_company_id = txtCompanyID.Text;
            _objUploadFile.sz_flag = "DEN";
            _objUploadFile.sz_case_id = arrCaseId;
            _objUploadFile.sz_speciality_id = arrSpec;
            _objUploadFile.sz_UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            _objUploadFile.sz_UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString();
            ViewState["VSUpload"] = _objUploadFile;
        }
        //Page.RegisterStartupScript("ss", "<script language='javascript'>showUploadFilePopup();</script>");
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "showUploadFilePopup();", true);
    }
    protected void lnkscan_Click(object sender, EventArgs e)
    {
        Bill_Sys_NotesBO _Bill_Sys_NotesBO = new Bill_Sys_NotesBO();
        DataSet dsdetails = new DataSet();
        dsdetails = _Bill_Sys_NotesBO.GetPatientDenial(txtCompanyID.Text, txtCaseID.Text);
        txtcaseno.Text = dsdetails.Tables[0].Rows[0]["SZ_CASE_NO"].ToString();
        txtpatientname.Text = dsdetails.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString();
        int iindex = grdVerificationReq.SelectedIndex;
        LinkButton btn = (LinkButton)sender;
        TableCell tc = (TableCell)btn.Parent;
        DataGridItem it = (DataGridItem)tc.Parent;

        Bill_Sys_Verification_Desc _objDesc = new Bill_Sys_Verification_Desc();
        Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
        ArrayList objAL = new ArrayList();
        ArrayList arrNodeType = new ArrayList();
        string szCaesType = "";
        string szProcess = "";


        int index = it.ItemIndex;
        Session["NODETYPESCAN"] = "";
        Session["NODETYPE"] = "";
        //atul
        Session["SCANVERID"] = it.Cells[5].Text;

        _objDesc.sz_bill_no = txtViewBillNumber.Text;
        _objDesc.sz_company_id = txtCompanyID.Text;
        _objDesc.sz_flag = "DEN";

        objAL.Add(_objDesc);

        arrNodeType = _obj.Get_Node_Type(objAL);

        if (it.Cells[1].Text.ToLower().Equals("denial"))
        {
            szProcess = "DEN";

            if (arrNodeType.Contains("NFVER"))
            {
                szCaesType = "OLD";
                arrNodeType.Clear();
                arrNodeType.Add("NFDEN");
                Session["NODETYPE"] = arrNodeType;
            }
            else
            {
                szCaesType = "NEW";
                Session["NODETYPE"] = arrNodeType;
            }

            //Session["NODETYPESCAN"] = "NFDEN";
        }


        Session["ScanBillNo"] = txtViewBillNumber.Text;
        
        RedirectToScanApp(iindex, szCaesType, szProcess, arrNodeType[0].ToString());
       
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList objarr = new ArrayList();
        Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
        Bill_Sys_Verification_Desc _obj1;
        ArrayList _objAL = new ArrayList();
        try
        {
            for (int i = 0; i < grdVerificationReq.Items.Count; i++)
            {
                if (((CheckBox)grdVerificationReq.Items[i].Cells[8].FindControl("chkDelete")).Checked == true)
                {
                    string sz_status_code = grdVerificationReq.Items[i].Cells[9].Text.ToString();
                    if (hfconfirm.Value == "delete" || hfconfirm.Value == "no")
                    {

                        Bill_sys_Verification_Pop objpopup = new Bill_sys_Verification_Pop();

                        objpopup.sz_bill_no = txtViewBillNumber.Text;

                        objpopup.i_verification_id = grdVerificationReq.Items[i].Cells[5].Text.ToString();
                        objpopup.sz_company_id = txtCompanyID.Text;
                        objpopup.sz_bill_Status = sz_status_code;
                        objpopup.sz_flag = "DEL";
                        objarr.Add(objpopup);
                        _obj1 = new Bill_Sys_Verification_Desc();
                        _obj1.sz_bill_no = txtViewBillNumber.Text;
                        _obj1.sz_verification_id = grdVerificationReq.Items[i].Cells[5].Text.ToString();
                        _obj1.sz_company_id = txtCompanyID.Text;
                        _obj1.sz_answer_id = grdVerificationReq.Items[i].Cells[12].Text.ToString();
                        _objAL.Add(_obj1);

                    }

                    else if (hfconfirm.Value == "yes")
                    {
                        Bill_sys_Verification_Pop objpopup = new Bill_sys_Verification_Pop();
                        objpopup.sz_bill_no = txtViewBillNumber.Text;
                        objpopup.i_verification_id = grdVerificationReq.Items[i].Cells[5].Text.ToString();
                        objpopup.sz_company_id = txtCompanyID.Text;
                        objpopup.sz_bill_Status = sz_status_code;
                        if (hfverificationId.Value != grdVerificationReq.Items[i].Cells[5].Text.ToString())
                        {
                            objpopup.sz_flag = "DEL";
                        }
                        else
                        {
                            objpopup.sz_flag = "CONFIRM";
                        }

                        objarr.Add(objpopup);
                    }
                }

            }

            if (_objAL != null && _objAL.Count > 0)
            {

                try
                {
                    Bill_Sys_BillTransaction_BO obj2 = new Bill_Sys_BillTransaction_BO();
                    obj2.DeleteVerificationAns(_objAL);
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
            ArrayList FilePath = obj.DeleteVerificationNotes(objarr);
            Bill_Sys_NF3_Template objpath = new Bill_Sys_NF3_Template();
            string PhysicalPath = objpath.getPhysicalPath();
            {
                for (int i = 0; i < FilePath.Count; i++)
                {
                    string file_path = FilePath[i].ToString();
                    if (File.Exists(PhysicalPath + file_path))
                    {
                        if (!File.Exists(PhysicalPath + file_path + ".delete"))
                        {
                            File.Move(PhysicalPath + file_path, PhysicalPath + file_path + ".delete");
                        }
                    }

                }
            }

            BindGridforDenial();
            ClearControls();
            usrMessage.PutMessage("Delete Successfully ...!");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
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

    protected void btnUploadFile1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        ArrayList arrUpload = new ArrayList();
        ArrayList arr_ImgID = new ArrayList();
        try
        {
            if (!fuUploadReport.HasFile)
            {
                Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");
                return;
            }

            Bill_Sys_UploadFile _obj = (Bill_Sys_UploadFile)ViewState["VSUpload"];
            _obj.sz_FileName = fuUploadReport.FileName;
            _obj.sz_File = fuUploadReport.FileBytes;

            FileUpload _FileUpload = new FileUpload();
            arr_ImgID = _FileUpload.UploadFile(_obj);



            if (arr_ImgID != null)
            {
                objNF3Template.InsertPaymentImage(Session["BillNo"].ToString(), txtCompanyID.Text, arr_ImgID[0].ToString(), ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["VERID"].ToString(), "VERIFICATIONPOPUP");
            }

            usrMessage.PutMessage("File Upload Successfully!");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

            BindGridforDenial();
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

    public void RedirectToScanApp(int iindex, string szCaseType, string szProcess, string szNodeType)
    {
        iindex = (int)grdVerificationReq.SelectedIndex;
        string szUrl = ConfigurationManager.AppSettings["webscanurl"].ToString();
        Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
        string NodeId = "";
        string szDenial = "";

        //string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["NODETYPE"].ToString());
        //string szNodeType = "";
        ArrayList arrNodeType = new ArrayList();
        ArrayList arrSpeciality = new ArrayList();
        string szBasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
        string szSpeciality = "";

        arrSpeciality.Add(Session["Speciality"].ToString());

        if (szCaseType == "OLD")
        {
            NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szNodeType);
        }
        else
        {
            //arrSpeciality = (ArrayList)Session["Spec"];

            if (NodeId == "")
            {
                Bill_Sys_BillTransaction_BO _objBillTransaction = new Bill_Sys_BillTransaction_BO();
                string szSpecName = _objBillTransaction.GetSpecName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, arrSpeciality[0].ToString());
                //string arrSpe = arrNodeType[0].ToString();

                if (szProcess == "DEN")
                {
                    NodeId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "\\" + txtCaseID.Text + "\\" + "Nofault" + "\\" + "Medicals" + "\\" + szSpecName + "\\" + ConfigurationManager.AppSettings["DEN"].ToString() + "\\";
                }
            }
            NodeId = Convert.ToBase64String(Encoding.Unicode.GetBytes(NodeId));

        }

        szUrl = szUrl + "&Flag=Denial" + "&CompanyId=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "&CaseId=" + txtCaseID.Text + "&UserName=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME + "&CompanyName=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        szUrl = szUrl + "&CaseNo=" + txtcaseno.Text + "&PName=" + txtpatientname.Text + "&NodeId=" + NodeId + "&BillNo=" + Session["ScanBillNo"].ToString() + "&UserId=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID + "&StatusID=" + Session["SCANVERID"].ToString();
        szUrl = szUrl + "&CaseType=" + szCaseType + "&Speciality=" + arrSpeciality[0].ToString().Trim() + "&Process=" + szProcess;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szUrl + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }

    public void ClearControls()
    {
        extddenial.Text = "NA";
        hfdenialReason.Value = "";
        hfremovedenialreason.Value = "";
        lbSelectedDenial.Items.Clear();
        txtSaveDate.Text = "";
        txtSaveDescription.Text = "";
    }
}
