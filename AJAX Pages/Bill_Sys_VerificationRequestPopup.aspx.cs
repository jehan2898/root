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

public partial class Bill_Sys_VerificationRequestPopup : PageBase
{
    string strLinkPath = null;
    Boolean saveFlag = false;
    string szansid = "";

    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extBillStatus.Flag_ID = txtCompanyID.Text;
            extddenial.Flag_ID=txtCompanyID.Text;
            txtViewBillNumber.Text = Request.QueryString["BillNo"].ToString();
            Session["Spec"] = Request.QueryString["Spe"].ToString();
            saveFlag = false;
            btnDelete.Attributes.Add("onclick", "return confirm_bill_delete();");
            txtSaveDate.Attributes.Add("onblur", "return FromDateValidation();");
            btnSave.Attributes.Add("onclick","return CheckedBillStatus();");
            btnSaveden.Attributes.Add("onclick", "return checkdenial();");
            btnUpdate.Attributes.Add("onclick", "return CheckedBillStatus();");
             btnUpdateden.Attributes.Add("onclick", "return checkdenial();");
                       
            if (!IsPostBack)
            {
                hdnBillNo.Value = txtViewBillNumber.Text;
                hdnCaseId.Value = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                string sz_bill_status_code = "";
                txtVerificationNotes.Text = "";
                btnUpdate.Enabled = false;
                btnUpdatesent.Enabled = false;
                btnUpdateden.Enabled = false;
                Bill_Sys_NotesBO _bill_Sys_NotesBO = new Bill_Sys_NotesBO();
                DataSet dset = new DataSet();
                dset = _bill_Sys_NotesBO.GetBillDetailsVerificationPopUp(txtCompanyID.Text, txtViewBillNumber.Text);
                for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                {

                    txtVisitDate.Text = dset.Tables[0].Rows[i]["DT_VISIT_DATE"].ToString();

                    extBillStatus.Text = "--- Select ---"; //dset.Tables[0].Rows[i]["STATUS_ID"].ToString();
                    sz_bill_status_code = dset.Tables[0].Rows[i]["SZ_BILL_STATUS_CODE"].ToString();
                    extddenial.Text = "--- Select ---";
                }
                txtVerificationDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                BindGrid();
                hfdenialreason.Value = "";
                hfremovedenialreason.Value = "";
                if (sz_bill_status_code.Equals("VR"))
                {
                    lbldate.Visible = true;
                    txtSaveDate.Visible = true;
                    imgSavebtnToDate.Visible = true;
                    lbSelectedDenial.Visible = false;
                    btnRemoveDenial.Visible = false;
                    btnAddDenial.Visible = false;
                    extddenial.Visible = false;
                    lblDenial.Visible = false;
                    btnSave.Visible = true;
                    btnSaveden.Visible = false;
                    btnSavesent.Visible = false;
                    btnUpdate.Visible = true;
                    btnUpdateden.Visible = false;
                    btnUpdatesent.Visible = false;
                    txtVerificationAns.Visible = false;
                    lblAns.Visible = false;
                }
                else if (sz_bill_status_code.Equals("DEN"))
                {
                    lbSelectedDenial.Visible = true;
                    btnRemoveDenial.Visible = true;
                    btnAddDenial.Visible = true;
                    extddenial.Visible = true;
                    lblDenial.Visible = true;
                    txtSaveDate.Visible = false;
                    btnSave.Visible = false;
                    btnSaveden.Visible = true;
                    btnSavesent.Visible = false;
                    btnUpdate.Visible = false;
                    btnUpdateden.Visible = true;
                    btnUpdatesent.Visible = false; ;
                    txtVerificationAns.Visible = false;
                    lblAns.Visible = false;
                }
                else if (sz_bill_status_code.Equals("VS"))
                {
                    btnSave.Visible = false;
                    btnSaveden.Visible = false;
                    btnSavesent.Visible = true;
                    btnUpdate.Visible = false;
                    txtVerificationAns.Visible = false;
                    lblAns.Visible = false;
                }
                else
                {
                    btnSave.Visible = true;
                    btnSaveden.Visible = false;
                    btnSavesent.Visible = false;
                    btnUpdate.Visible = true;
                    btnUpdateden.Visible = false;
                    btnUpdatesent.Visible = false;
                    txtVerificationAns.Visible = false;
                    lblAns.Visible = false;
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_VerificationRequestPopup.aspx");
        }
        #endregion

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
        //atul
        DataSet dset;
        try
        {
            grdVerificationReq.Columns[13].Visible = true;
            Bill_Sys_NotesBO _bill_Sys_NotesBO = new Bill_Sys_NotesBO();
            dset = new DataSet();
            dset = _bill_Sys_NotesBO.GetBillDetailsFillGridNew(txtCompanyID.Text, txtViewBillNumber.Text);
            grdVerificationReq.DataSource = dset.Tables[0];
            grdVerificationReq.DataBind();
            grdVerificationReq.Columns[13].Visible = false;

            for (int i = 0; i < grdVerificationReq.Items.Count; i++)
            {
                if (grdVerificationReq.Items[i].Cells[13].Text != "&nbsp;")
                {
                    hfindex.Value = "";
                    hfverificationId.Value = "";
                    hfindex.Value = i.ToString();
                    hfverificationId.Value = grdVerificationReq.Items[i].Cells[5].Text.ToString();
                }
                if (grdVerificationReq.Items[i].Cells[12].Text == "vs")
                {
                    grdVerificationReq.Items[i].Cells[10].Text = "";
                }
            }
            ClearControls();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (!saveFlag)
        {
            Boolean saveflag = false;
            string status_id = "";
            try
            {
                string sz_Bill_status_id = extBillStatus.Text;
                Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
                string sz_status_code = _obj.GetStatusCode(txtCompanyID.Text, sz_Bill_status_id);
                //VerificationReceived
                if (sz_status_code == "vr")
                {
                    Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                    ArrayList objAL = new ArrayList();
                    Bill_Sys_Verification_Desc _objDesc = new Bill_Sys_Verification_Desc();
                    _objDesc.sz_bill_no = txtViewBillNumber.Text;
                    _objDesc.sz_description = txtVerificationNotes.Text;
                    _objDesc.sz_verification_date = txtSaveDate.Text;
                    _objDesc.i_verification = 1;
                    _objDesc.sz_company_id = txtCompanyID.Text;
                    _objDesc.sz_user_id = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    _objDesc.sz_flag = "VR";
                    objAL.Add(_objDesc);
                    obj.InsertUpdateBillStatus(objAL);
                    BindGrid();
                    saveflag = true;
                }

                //Verification Request
                if (sz_status_code == "vs")
                {
                    Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                    ArrayList objAL = new ArrayList();
                    Bill_Sys_Verification_Desc _objDesc = new Bill_Sys_Verification_Desc();
                    _objDesc.sz_bill_no = txtViewBillNumber.Text;
                    _objDesc.sz_description = txtVerificationNotes.Text;
                    _objDesc.sz_verification_date = DBNull.Value.ToString();
                    _objDesc.i_verification = 2;
                    _objDesc.sz_company_id = txtCompanyID.Text;
                    _objDesc.sz_user_id = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    _objDesc.sz_flag = "VS";
                    objAL.Add(_objDesc);
                    obj.InsertUpdateBillStatus(objAL);
                    BindGrid();
                    saveflag = true;
                }

                // Denial status
                if (sz_status_code == "den")
                {
                    ArrayList _denialReason = new ArrayList();
                    Boolean flag = false;
                    //txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    if (!hfdenialreason.Value.Equals(""))
                    {
                        char ch = ',';
                        String[] DenialReason = hfdenialreason.Value.Split(ch);
                        Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                        ArrayList objAL = new ArrayList();
                        Bill_Sys_Verification_Desc _objDesc = new Bill_Sys_Verification_Desc();
                        _objDesc.sz_bill_no = txtViewBillNumber.Text;
                        _objDesc.sz_description = txtVerificationNotes.Text;
                        _objDesc.sz_verification_date = DBNull.Value.ToString();
                        _objDesc.i_verification = 3;
                        _objDesc.sz_company_id = txtCompanyID.Text;
                        _objDesc.sz_user_id = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                        _objDesc.sz_flag = "DEN";
                        objAL.Add(_objDesc);
                        status_id = obj.InsertUpdateBillStatus(objAL);
                        if (status_id != "")
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

                            if (status_id != "")
                            {

                                obj.UpdateDenialReason(status_id, _denialReason, txtViewBillNumber.Text);
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
                    }
                    ClearControls();
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='Bill_Sys_VerificationsentBills.aspx';window.self.close(); </script>");
                    if (saveflag)
                    {
                        usrMessage1.PutMessage("Billed status saved!");
                        usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage1.Show();
                    }
                }

                hfdenialreason.Value = "";
                hfremovedenialreason.Value = "";
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdVerificationReq_ItemCommand(object source, DataGridCommandEventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {

                lblBillStatus.Text = e.Item.Cells[1].Text.ToString();
                if (e.Item.Cells[1].Text.ToString() == "Denial")
                {
                    lbldenialdate.Visible = true;
                    txtSaveDate1.Visible = true;
                    ImageButton1.Visible = true;
                }
                else
                {

                    lbldenialdate.Visible = false;
                    txtSaveDate1.Visible = false;
                    ImageButton1.Visible = false;


                }
                Session["TYPEID"] = e.Item.Cells[5].Text.ToString();
                Session["ANSID"] = e.Item.Cells[15].Text.ToString();
                szansid = e.Item.Cells[15].Text.ToString();
                if (e.Item.Cells[2].Text.ToString() == "&nbsp;")
                {
                    txtVerificationNotes.Text = "";
                }
                else
                {
                    //atul
                    txtVerificationNotes.Text = e.Item.Cells[2].Text.ToString();
                }
                if (lblBillStatus.Text.ToLower() == "verification received")
                {
                    txtSaveDate.Visible = true;
                    imgSavebtnToDate.Visible = true;
                    lbldate.Visible = true;
                    lblDenial.Visible = false;
                    extddenial.Visible = false;
                    btnAddDenial.Visible = false;
                    btnRemoveDenial.Visible = false;
                    lblselect.Visible = false;
                    lbSelectedDenial.Visible = false;
                    txtSaveDate.Text = e.Item.Cells[3].Text.ToString();
                    btnUpdate.Visible = true;
                    btnUpdateden.Visible = false;
                    btnUpdatesent.Visible = false;
                    btnUpdate.Enabled = true;
                    btnUpdateden.Enabled = false;
                    btnUpdatesent.Enabled = false;
                    txtVerificationAns.Visible = true;
                    if (e.Item.Cells[7].Text.ToString() != "&nbsp;")
                    {
                        txtVerificationAns.Text = e.Item.Cells[7].Text.ToString();
                    }
                    else
                    {
                        txtVerificationAns.Text = "";
                    }
                    lblAns.Visible = true;
                }
                else if (lblBillStatus.Text.ToLower() == "denial")
                {
                    Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
                    string sz_denial_reason = e.Item.Cells[6].Text.ToString();
                    hfdenialreason.Value = "";
                    txtSaveDate1.Text = e.Item.Cells[3].Text.ToString();
                    hfremovedenialreason.Value = "";
                    lbSelectedDenial.ClearSelection();
                    if (!(sz_denial_reason.Equals("") || sz_denial_reason.Equals("&nbsp;")))
                    {
                        String[] _denial_reason = sz_denial_reason.Split(',');
                        ArrayList _denialreason = _obj.GetDenialReason(_denial_reason,txtCompanyID.Text);
                        lbSelectedDenial.Items.Clear();
                        for (int i = 0; i < _denial_reason.Length; i++)
                        {
                            ListItem list = new ListItem();
                            list.Value = _denialreason[i].ToString();
                            list.Text = _denial_reason[i].ToString();
                            lbSelectedDenial.Items.Add(list);
                            hfdenialreason.Value = hfdenialreason.Value + _denialreason[i].ToString() + ",";
                        }
                        btnUpdate.Visible = false;
                        btnUpdateden.Visible = true;
                        btnUpdatesent.Visible = false;
                        btnUpdate.Enabled = false;
                        btnUpdateden.Enabled = true;
                        btnUpdatesent.Enabled = false;
                       
                    }
                    else
                    {
                        lbSelectedDenial.Items.Clear();
                        btnUpdate.Visible = false;
                        btnUpdateden.Visible = true;
                        btnUpdatesent.Visible = false;
                        btnUpdate.Enabled = false;
                        btnUpdateden.Enabled = true;
                        btnUpdatesent.Enabled = false;
                       
                    }
                    lblDenial.Visible = true;
                    extddenial.Visible = true;
                    btnAddDenial.Visible = true;
                    btnRemoveDenial.Visible = true;
                    lblselect.Visible = true;
                    lbSelectedDenial.Visible = true;
                    txtVerificationAns.Visible = false;
                    lblAns.Visible = false;
                    txtSaveDate.Visible = false;
                    lbldate.Visible = false;
                    imgSavebtnToDate.Visible = false;
                }
                else
                {
                    txtSaveDate.Visible = false;
                    imgSavebtnToDate.Visible = false;
                    lbldate.Visible = false;
                    lblDenial.Visible = false;
                    extddenial.Visible = false;
                    btnAddDenial.Visible = false;
                    btnRemoveDenial.Visible = false;
                    lblselect.Visible = false;
                    lbSelectedDenial.Visible = false;
                    btnUpdate.Enabled = false;
                    btnUpdateden.Enabled = false;
                    btnUpdatesent.Enabled = true;
                    txtVerificationAns.Visible = false;
                    lblAns.Visible = false;
                }
                extBillStatus.Enabled = false;
                extBillStatus.Visible = false;
                lblBillStatus.Visible = true;
                btnSave.Enabled = false;
                

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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (!saveFlag)
        {
            try
            {

                string sz_status_code = "";
                if (lblBillStatus.Text.ToLower() == "verification received")
                {
                    sz_status_code = "vr";
                }
                else if (lblBillStatus.Text.ToLower() == "verification sent")
                {
                    sz_status_code = "vs";
                }
                else if (lblBillStatus.Text.ToLower() == "denial")
                {
                    sz_status_code = "den";
                }
                Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();

                if (sz_status_code == "vr")
                {
                    Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                    ArrayList objAL = new ArrayList();
                    Bill_Sys_Verification_Desc _obj1 = new Bill_Sys_Verification_Desc();
                    ArrayList _objAL = new ArrayList();

                    objAL.Add(txtViewBillNumber.Text);
                    objAL.Add(txtVerificationNotes.Text);
                    objAL.Add(txtCompanyID.Text);
                    objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    objAL.Add(Session["TYPEID"].ToString());
                    objAL.Add(txtSaveDate.Text);
                    obj.UpdateVerificationReceivedInformation(objAL);
                    _obj1.sz_bill_no = txtViewBillNumber.Text;
                    _obj1.sz_answer = txtVerificationAns.Text;
                    _obj1.sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    _obj1.sz_verification_id = Session["TYPEID"].ToString();
                    _obj1.sz_company_id = txtCompanyID.Text;
                    _obj1.sz_answer_id = Session["ANSID"].ToString();
                    string szUserName = (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
                    _objAL.Add(_obj1);
                    //change
                    if (txtVerificationAns.Text.Trim().ToString().Equals(""))
                    {
                        Bill_Sys_BillTransaction_BO obj2 = new Bill_Sys_BillTransaction_BO();
                        obj2.DeleteVerificationAns(_objAL);
                        BindGrid();
                        txtVerificationAns.Text = "";
                    }
                    else
                    {

                        Bill_Sys_NF3_Template objNF3 = new Bill_Sys_NF3_Template();
                        objNF3.SetVerification_Answer(_objAL, txtCompanyID.Text, szUserName);
                        BindGrid();
                        txtVerificationAns.Text = "";
                    }
                }

                //Verification Request
                if (sz_status_code == "vs")
                {
                    Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                    ArrayList objAL = new ArrayList();

                    objAL.Add(txtViewBillNumber.Text);
                    objAL.Add(txtVerificationNotes.Text);
                    objAL.Add(txtCompanyID.Text);
                    objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    objAL.Add(Session["TYPEID"].ToString());
                    obj.UpdateVerificationRequest(objAL);

                    BindGrid();
                }

                // Denial status
                if (sz_status_code == "den")
                {

                    ArrayList _objRemove = new ArrayList();
                    ArrayList _objAdd = new ArrayList();
                    Boolean denialremove = false;
                    string UserID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    ArrayList objAL = new ArrayList();
                    string szListOfBillIDs = "";
                    string szBillNumbers = "";
                    string SZDENIALUSERID = "";
                    Boolean flag = false;

                    objAL.Add(txtViewBillNumber.Text);
                    objAL.Add(txtCompanyID.Text);
                    objAL.Add(UserID);
                    objAL.Add(txtVerificationNotes.Text);
                    objAL.Add(Session["TYPEID"].ToString());
                    objAL.Add(txtSaveDate1.Text);
                    Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                    obj.UpdateDenialNotes(objAL);
                    string removedenail = hfremovedenialreason.Value;

                    if (!hfdenialreason.Value.Equals(""))
                    {
                        String[] adddenial = hfdenialreason.Value.Split(',');
                        if (!hfremovedenialreason.Value.Equals(""))
                        {
                            String[] removedenial = hfremovedenialreason.Value.Split(',');
                            for (int i = 0; i < adddenial.Length - 1; i++)
                            {
                                denialremove = false;
                                for (int j = 0; j < removedenial.Length - 1; j++)
                                {
                                    if (adddenial[i].ToString() == removedenial[j].ToString())
                                    {
                                        _objRemove.Add(removedenial[j].ToString());

                                        denialremove = true;
                                        break;
                                    }
                                }
                                if (!denialremove)
                                {

                                    _objAdd.Add(adddenial[i].ToString());

                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < adddenial.Length - 1; i++)
                            {
                                _objAdd.Add(adddenial[i].ToString());
                            }
                        }
                    }
                    if (_objAdd.Count > 0)
                    {
                        obj.UpdateDeleteDenilReason(_objAdd, "UPDATE", Session["TYPEID"].ToString());

                    }
                    if (_objRemove.Count > 0)
                    {
                        obj.UpdateDeleteDenilReason(_objRemove, "DELETE", Session["TYPEID"].ToString());
                    }

                    BindGrid();
                }
                ClearControls();
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
            txtVerificationNotes.Text = "";
            extBillStatus.Selected_Text = "--- Select ---";
            extddenial.Selected_Text = "--- Select ---";
            hfdenialreason.Value = "";
            hfremovedenialreason.Value = "";
            lbSelectedDenial.Items.Clear();
            txtSaveDate.Text = "";
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {//Logging Start
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
                if (((CheckBox)grdVerificationReq.Items[i].Cells[11].FindControl("chkDelete")).Checked == true)
                {
                    string sz_status_code = grdVerificationReq.Items[i].Cells[12].Text.ToString();
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
                                _obj1.sz_answer_id = grdVerificationReq.Items[i].Cells[15].Text.ToString();

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
        ArrayList FilePath =  obj.DeleteVerificationNotes(objarr);
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

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ClearControls();
            extBillStatus.Enabled = true;
            extBillStatus.Visible = true;
            lblBillStatus.Visible = false;
            txtSaveDate1.Visible = false;
            lbldenialdate.Visible = false;
            ImageButton1.Visible = false;
            lblBillStatus.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            lblDenial.Visible = false;
            extddenial.Visible = false;
            lbSelectedDenial.Visible = false;
            btnAddDenial.Visible = false;
            btnRemoveDenial.Visible = false;
            lblselect.Visible = false;
            txtVerificationAns.Visible = false;
            txtVerificationAns.Text = "";
            lblAns.Visible = false;
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

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {

    }

    protected void extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (extBillStatus.Selected_Text.ToLower() == "denial")
        {
            lblDenial.Visible = true;
            extddenial.Visible = true;

            lbSelectedDenial.Visible = true;
            btnAddDenial.Visible = true;
            btnRemoveDenial.Visible = true;
            lblselect.Visible = true;
            btnSave.Visible = false;
            btnSaveden.Visible = true;
            btnSavesent.Visible = false;
            btnUpdate.Visible = false;
            btnUpdateden.Visible = true;
            btnUpdatesent.Visible = false;
        }
        else
        {
            lblDenial.Visible = false;
            extddenial.Visible = false;

            lbSelectedDenial.Visible = false;
            btnAddDenial.Visible = false;
            btnRemoveDenial.Visible = false;
            lblselect.Visible = false;
        }
        if (extBillStatus.Selected_Text.ToLower() == "verification received" )
        {
            lbldate.Visible = true;
            txtSaveDate.Visible = true;
            imgSavebtnToDate.Visible = true;
            btnSave.Visible = true;
            btnSaveden.Visible = false;
            btnSavesent.Visible = false;
            btnUpdate.Visible = true;
            btnUpdateden.Visible = false;
            btnUpdatesent.Visible = false;
        }
        else
        {
            lbldate.Visible = false;
            txtSaveDate.Visible = false;
            imgSavebtnToDate.Visible = false;
        }
        if (extBillStatus.Selected_Text == "---Select---")
        {
            lblDenial.Visible = false;
            extddenial.Visible = false;
            lbSelectedDenial.Visible = false;
            btnAddDenial.Visible = false;
            btnRemoveDenial.Visible = false;
            lblselect.Visible = false;
            lbldate.Visible = false;
            txtSaveDate.Visible = false;
            imgSavebtnToDate.Visible = false;
            btnUpdate.Visible = true;
            btnUpdateden.Visible = false;
            btnUpdatesent.Visible = false;
        }
        if (extBillStatus.Selected_Text.ToLower().Equals("verification sent"))
        {
            btnSave.Visible = false;
            btnSaveden.Visible = false;
            btnSavesent.Visible = true;
            btnUpdate.Visible = false;
            btnUpdateden.Visible = false;
            btnUpdatesent.Visible = true;
        }
    }

    protected void lnkscan_Click(object sender, EventArgs e)
    {
        int iindex = grdVerificationReq.SelectedIndex;
        LinkButton btn = (LinkButton)sender;
        TableCell tc = (TableCell)btn.Parent;
        DataGridItem it = (DataGridItem)tc.Parent;

        Bill_Sys_Verification_Desc _objDesc =new Bill_Sys_Verification_Desc();
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
        else
        {
            szProcess = "VR";
            if (arrNodeType.Contains("NFVER"))
            {
                szCaesType = "OLD";
                arrNodeType.Clear();
                arrNodeType.Add("NFVER");
                Session["NODETYPE"] = arrNodeType;
            }
            else
            {
                szCaesType = "NEW";
                Session["NODETYPE"] = arrNodeType;
            }
            //Session["NODETYPESCAN"] = "NFVER";
        }
        Session["ScanBillNo"] = txtViewBillNumber.Text;

        RedirectToScanApp(iindex, szCaesType, szProcess, arrNodeType[0].ToString());
    }

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
        ///atul
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

            

            arrCaseId.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            arrBillNo.Add(txtViewBillNumber.Text);
            arrSpec.Add(Session["Spec"].ToString());

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
        else
        {
            _objDesc = new Bill_Sys_Verification_Desc();
            _objDesc.sz_bill_no = txtViewBillNumber.Text;
            _objDesc.sz_description = "";
            _objDesc.sz_verification_date = "";
            _objDesc.i_verification = 1;
            _objDesc.sz_company_id = txtCompanyID.Text;
            _objDesc.sz_user_id = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            _objDesc.sz_flag = "VR";
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
            //Session["NODETYPE"]="NFVER";

           
            arrCaseId.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            arrBillNo.Add(txtViewBillNumber.Text);
            arrSpec.Add(Session["Spec"].ToString());

            Bill_Sys_UploadFile _objUploadFile = new Bill_Sys_UploadFile();
            _objUploadFile.sz_bill_no = arrBillNo;
            _objUploadFile.sz_company_id = txtCompanyID.Text;
            _objUploadFile.sz_flag = "VR";
            _objUploadFile.sz_case_id = arrCaseId;
            _objUploadFile.sz_speciality_id = arrSpec;
            _objUploadFile.sz_UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            _objUploadFile.sz_UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString();
            ViewState["VSUpload"] = _objUploadFile;

        }
        Page.RegisterStartupScript("ss", "<script language='javascript'>showUploadFilePopup();</script>");
        

    }

    public void RedirectToScanApp(int iindex, string szCaseType, string szProcess, string szNodeType)
    {
        iindex = (int)grdVerificationReq.SelectedIndex;
        string szUrl = ConfigurationManager.AppSettings["webscanurl"].ToString();
       Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
       string NodeId = "";

       //string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["NODETYPE"].ToString());
       //string szNodeType = "";
       ArrayList arrNodeType = new ArrayList();
       ArrayList arrSpeciality = new ArrayList();
       string szBasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
       string szSpeciality = "";
       arrSpeciality.Add(Session["Spec"].ToString());

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
                if (szProcess=="VR")
                {
                    NodeId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "\\" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "\\" + "Nofault" + "\\" + "Medicals" + "\\" + szSpecName + "\\" + ConfigurationManager.AppSettings["VR"].ToString() + "\\";
                }
                else
                    if (szProcess=="DEN")
                    {
                        NodeId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "\\" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "\\" + "Nofault" + "\\" + "Medicals" + "\\" + szSpecName + "\\" + ConfigurationManager.AppSettings["DEN"].ToString() + "\\";
                    }
            }
            NodeId = Convert.ToBase64String(Encoding.Unicode.GetBytes(NodeId));
        }

        szUrl = szUrl + "&Flag=ReqVeri" + "&CompanyId=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "&CaseId=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "&UserName=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME + "&CompanyName=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        szUrl = szUrl + "&CaseNo=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO + "&PName=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME + "&NodeId=" + NodeId + "&BillNo=" + Session["ScanBillNo"].ToString() + "&UserId=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID + "&StatusID=" + Session["SCANVERID"].ToString() ;
        szUrl = szUrl + "&CaseType=" + szCaseType + "&Speciality=" + arrSpeciality[0].ToString().Trim() + "&Process=" + szProcess;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szUrl + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {//Logging Start
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


            //String szDefaultPath = objNF3Template.getPhysicalPath();
            //int ImageId = 0;
            //String szDestinationDir = "";
            //ArrayList arr_NodeId = new ArrayList();
            //ArrayList arr_NodePath = new ArrayList();

            //szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            //Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();

            ////string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["NODETYPE"].ToString());
            //ArrayList arrSession = new ArrayList();
            //arrSession = (ArrayList)Session["NODETYPE"];
            //arr_NodeId = _obj.GetNodeID_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, arrSession);

            //RequiredDocuments.Bill_Sys_RequiredDocumentBO bo = new RequiredDocuments.Bill_Sys_RequiredDocumentBO();

            //for (int i = 0; i < arr_NodeId.Count; i++)
            //{
            //    //string szNodePath = bo.GetNodePath(NodeId, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            //    arr_NodePath.Add(bo.GetNodePath(arr_NodeId[i].ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
            //}

            //ArrayList arrDirPath = new ArrayList();

            //if (arr_NodePath.Count > 1)
            //{
            //    for (int j = 0; j < arr_NodePath.Count; j++)
            //    {
            //        string pa = arr_NodePath[j].ToString();
            //        pa = pa.Replace("\\", "/");
            //        string[] arr = pa.Split(new Char[] { '/' });
            //        if (arr != null)
            //        {
            //            szDestinationDir = arr[0] + "\\" + "Common Folder" + "\\" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "\\" + "Verification Requests" + "\\";
            //            szDestinationDir = szDestinationDir.Replace("\\", "/");
            //            strLinkPath = szDestinationDir + fuUploadReport.FileName;
            //            if (!Directory.Exists(szDefaultPath + szDestinationDir))
            //            {
            //                Directory.CreateDirectory(szDefaultPath + szDestinationDir);
            //            }
            //            fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);
            //            //arrDirPath.Add(szDestinationDir);
            //        }
            //    }
            //}
            //else
            //{
            //    for (int j = 0; j < arr_NodePath.Count; j++)
            //    {
            //        szDestinationDir = arr_NodePath[j].ToString() + "\\";
            //        szDestinationDir = szDestinationDir.Replace("\\", "/");
            //        strLinkPath = szDestinationDir + fuUploadReport.FileName;
            //        if (!Directory.Exists(szDefaultPath + szDestinationDir))
            //        {
            //            Directory.CreateDirectory(szDefaultPath + szDestinationDir);
            //        }

            //        fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);
            //    }
            //}


            //ArrayList objAL = new ArrayList();
            //ArrayList arr_ImgID = new ArrayList();

            //for (int k = 0; k < arr_NodeId.Count; k++)
            //{

            //    objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            //    objAL.Add("");
            //    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            //    objAL.Add(fuUploadReport.FileName);
            //    objAL.Add(szDestinationDir);
            //    objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
            //    objAL.Add(arr_NodeId[k]);


            //    //string ImgId = objNF3Template.SaveDocumentData(objAL);
            //    arr_ImgID.Add(objNF3Template.SaveDocumentData(objAL));
            //}

            if (arr_ImgID != null)
            {
                //for (int m = 0; m < arr_ImgID.Count; m++)
                //{
                    //objNF3Template.InsertPaymentImage(Session["BillNo"].ToString(), txtCompanyID.Text, ImgId, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["VERID"].ToString(), "VERIFICATIONPOPUP");
                    objNF3Template.InsertPaymentImage(Session["BillNo"].ToString(), txtCompanyID.Text, arr_ImgID[0].ToString(), ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["VERID"].ToString(), "VERIFICATIONPOPUP");
                //}
                
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

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}

