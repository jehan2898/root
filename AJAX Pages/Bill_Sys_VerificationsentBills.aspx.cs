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
using System.Text;


public partial class Bill_Sys_VerificationsentBills : PageBase
{
    Bill_Sys_NotesBO _bill_Sys_NotesBO;
    Bill_Sys_BillTransaction_BO _obj;
    Bill_Sys_UploadFile _objUploadFile;
    string strLinkPath = null;

    private void BindGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_NotesBO = new Bill_Sys_NotesBO();
        try
        {
            this.grdBillSearch.DataSource = this._bill_Sys_NotesBO.GET_VERIFICATION_SENT_BILLS(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.txtCaseID.Text).Tables[0];
            this.grdBillSearch.DataBind();
            this.con.SourceGrid=this.grdVeriFication;
            this.txtSearchBox.SourceGrid=this.grdVeriFication;
            this.grdVeriFication.Page=this.Page;
            this.grdVeriFication.PageNumberList=this.con;
            this.grdVeriFication.XGridBindSearch();
            for (int i = 0; i < this.grdVeriFication.Rows.Count; i++)
            {
                string str = this.grdVeriFication.DataKeys[i][2].ToString();
                string str2 = this.grdVeriFication.DataKeys[i][3].ToString();
                string str3 = this.grdVeriFication.DataKeys[i][4].ToString();
                LinkButton button = (LinkButton)this.grdVeriFication.Rows[i].FindControl("lnkM");
                LinkButton button2 = (LinkButton)this.grdVeriFication.Rows[i].FindControl("lnkP");
                LinkButton button3 = (LinkButton)this.grdVeriFication.Rows[i].FindControl("lnkDM");
                LinkButton button4 = (LinkButton)this.grdVeriFication.Rows[i].FindControl("lnkDP");
                LinkButton button5 = (LinkButton)this.grdVeriFication.Rows[i].FindControl("lnkEM");
                LinkButton button6 = (LinkButton)this.grdVeriFication.Rows[i].FindControl("lnkEP");
                if (str.ToLower() == "1")
                {
                    button4.ForeColor = System.Drawing.Color.Red;
                    button3.ForeColor = System.Drawing.Color.Red;
                    button4.Text = "+";
                    button3.Text = "-";
                }
                else
                {
                    button4.Text = "";
                    button3.Text = "";
                }
                if (str2.ToLower() == "1")
                {
                    button2.ForeColor = System.Drawing.Color.Red;
                    button.ForeColor = System.Drawing.Color.Red;
                    button2.Text = "+";
                    button.Text = "-";
                }
                else
                {
                    button2.Text = "";
                    button.Text = "";
                }
                if (str3.ToLower() == "1")
                {
                    button5.ForeColor = System.Drawing.Color.Red;
                    button6.ForeColor = System.Drawing.Color.Red;
                    button6.Text = "+";
                    button5.Text = "-";
                }
                else button5.Text = button6.Text = "";
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

    protected void btnBillUpdateStatus_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_ReportBO tbo = new Bill_Sys_ReportBO();
        try
        {
            if (this.drdUpdateStatus.Text != "NA")
            {
                ArrayList list = new ArrayList();
                string str = "";
                bool flag = false;
                for (int i = 0; i < this.grdVeriFication.Rows.Count; i++)
                {
                    CheckBox box = (CheckBox)this.grdVeriFication.Rows[i].FindControl("ChkDelete");
                    if (((box.Checked && (this.grdVeriFication.DataKeys[i]["SZ_BILL_STATUS_CODE"].ToString() != "LND")) && ((this.grdVeriFication.DataKeys[i]["SZ_BILL_STATUS_CODE"].ToString() != "SLD") && (this.grdVeriFication.DataKeys[i]["SZ_BILL_STATUS_CODE"].ToString() != "TRF"))) && (this.grdVeriFication.DataKeys[i]["SZ_BILL_STATUS_CODE"].ToString() != "DNL"))
                    {
                        if (!flag)
                        {
                            str = "'" + this.grdVeriFication.DataKeys[i]["SZ_BILL_NUMBER"].ToString() + "'";
                            flag = true;
                        }
                        else
                        {
                            str = str + ",'" + this.grdVeriFication.DataKeys[i]["SZ_BILL_NUMBER"].ToString() + "'";
                        }
                    }
                }
                if (str != "")
                {
                    list.Add(this.drdUpdateStatus.Text);
                    list.Add(str);
                    list.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    list.Add(DateTime.Today.ToShortDateString());
                    list.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    tbo.updateBillStatus(list);
                    this.usrMessage.PutMessage("Bill status updated successfully.");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                    int selectedIndex = this.grdVeriFication.PageNumberList.SelectedIndex;
                    this.grdVeriFication.PageIndex = selectedIndex;
                    this.grdVeriFication.XGridBind();
                    this.con.SelectedIndex = selectedIndex;
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

    protected void btnBulkPayment_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            foreach (DataGridItem item in this.grdBillSearch.Items)
            {
                CheckBox box = (CheckBox)item.Cells[12].Controls[1];
                if (box.Checked)
                {
                    this._bill_Sys_NotesBO = new Bill_Sys_NotesBO();
                    this._bill_Sys_NotesBO.UpdateBillStatusForVerification(item.Cells[2].Text.ToString(), 1, "");
                }
            }
            this.BindGrid();
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

    protected void btnCancelDesc_Click(object sender, EventArgs e)
    {
        this.txtSaveDate.Text = "";
        this.txtSaveDescription.Text = "";
        this.rdlVerification.ClearSelection();
        this.BindGrid();
    }

    protected void btnCancelSendRequest_Click(object sender, EventArgs e)
    {
        this.txtSaveDate.Text = "";
        this.txtSaveDescription.Text = "";
        this.rdlVerification.ClearSelection();
        this.BindGrid();
    }

    protected void btnGenDenTxt_Click(object sender, EventArgs e)
    {
        Bill_Sys_Case_Denial denial = new Bill_Sys_Case_Denial();
        this.lnkAddGeneralDenial.Text = "General Denial(" + denial.GetDenialCountForCase(this.txtCaseID.Text.ToString()).ToString() + ")";
    }

    protected void btnOk_Click(object sednder, EventArgs e)
    {
        this.BindGrid();
    }

    protected void btnPopUploadFile1_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
        try
        {
            if (!this.fuUploadReport.HasFile)
            {
                this.Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");
            }
            else
            {
                string str = template.getPhysicalPath();
                string companyName = "";
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                companyName = template.GetCompanyName(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this._obj = new Bill_Sys_BillTransaction_BO();
                ArrayList list3 = new ArrayList();
                list3 = (ArrayList)this.Session["NODETYPE"];
                list = this._obj.GetNodeID_Nodes(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, list3);
                Bill_Sys_RequiredDocumentBO tbo = new Bill_Sys_RequiredDocumentBO();
                for (int i = 0; i < list.Count; i++)
                {
                    list2.Add(tbo.GetNodePath(list[i].ToString(), this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                if (list2.Count > 1)
                {
                    for (int k = 0; k < list2.Count; k++)
                    {
                        string[] strArray = list2[k].ToString().Replace(@"\", "/").Split(new char[] { '/' });
                        if (strArray != null)
                        {
                            companyName = (strArray[0] + @"\Common Folder\" + this.txtCaseID.Text + @"\Verification Requests\").Replace(@"\", "/");
                            this.strLinkPath = companyName + this.upFile.FileName;
                            if (!Directory.Exists(str + companyName))
                            {
                                Directory.CreateDirectory(str + companyName);
                            }
                            this.upFile.SaveAs(str + companyName + this.upFile.FileName);
                        }
                    }
                }
                else
                {
                    for (int m = 0; m < list2.Count; m++)
                    {
                        companyName = companyName + "/" + list2[m].ToString() + "";
                        this.strLinkPath = companyName + this.fuUploadReport.FileName;
                        if (!Directory.Exists(str + companyName))
                        {
                            Directory.CreateDirectory(str + companyName);
                        }
                        this.fuUploadReport.SaveAs(str + companyName + this.fuUploadReport.FileName);
                    }
                }
                ArrayList arrayList = new ArrayList();
                ArrayList list5 = new ArrayList();
                for (int j = 0; j < list.Count; j++)
                {
                    arrayList.Add(this.txtCaseID.Text);
                    arrayList.Add("");
                    arrayList.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    arrayList.Add(this.fuUploadReport.FileName);
                    arrayList.Add(companyName);
                    arrayList.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
                    arrayList.Add(list[j]);
                    list5.Add(template.SaveDocumentData(arrayList));
                }
                if (list5 != null)
                {
                    for (int n = 0; n < list5.Count; n++)
                    {
                        template.InsertPaymentImage(this.Session["BillNo"].ToString(), this.txtCompanyID.Text, list5[n].ToString(), ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, this.Session["STATUS_CODE"].ToString(), "VERIFICATIONSENT");
                    }
                }
                this.lblMsg.Text = "File Upload Successfully";
                this.lblMsg.Visible = true;
                this.BindGrid();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            ArrayList list = new ArrayList();
            list.Add(this.txtViewBillNumber.Text);
            list.Add(this.txtVerificationNotes.Text.Replace("\r\n", "#"));
            list.Add(this.txtCompanyID.Text);
            list.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
            n_bo.SaveVerificationRequest(list);
            this.BindGrid();
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
        Bill_Sys_BillTransaction_BO n_bo;
        Bill_Sys_Verification_Desc desc;
        bool flag = false;
        string str = "";
        ArrayList list = new ArrayList();
        string str2 = "";
        bool flag2 = false;
        ArrayList list2 = new ArrayList();
        ArrayList list3 = new ArrayList();
        string str3 = "";
        if (this.rdlVerification.SelectedValue == "1")
        {
            this.ViewState["Process"] = "";
            this.ViewState["Process"] = "VR";
            ArrayList list4 = new ArrayList();
            ArrayList list5 = new ArrayList();
            for (int i = 0; i < this.grdVeriFication.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdVeriFication.Rows[i].FindControl("ChkDelete");
                if (box.Checked)
                {
                    string str4 = this.grdVeriFication.DataKeys[i][0].ToString();
                    str3 = this.grdVeriFication.DataKeys[i][4].ToString();
                    list2.Add(this.txtCaseID.Text);
                    list5.Add(str4);
                    list3.Add(str3);
                    str2 = str2 + str4 + ",";
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_bill_no=str4;
                    desc.sz_description=this.txtSaveDescription.Text;
                    desc.sz_verification_date=this.txtSaveDate.Text;
                    desc.i_verification=1;
                    desc.sz_company_id=this.txtCompanyID.Text;
                    desc.sz_user_id=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    desc.sz_flag="VR";
                    list4.Add(desc);
                    flag = true;
                }
            }
            this._objUploadFile = new Bill_Sys_UploadFile();
            this._objUploadFile.sz_bill_no=list5;
            this._objUploadFile.sz_company_id=this.txtCompanyID.Text;
            this._objUploadFile.sz_flag="VR";
            this._objUploadFile.sz_case_id=list2;
            this._objUploadFile.sz_speciality_id=list3;
            if (flag)
            {
                n_bo = new Bill_Sys_BillTransaction_BO();
                str = n_bo.InsertUpdateBillStatus(list4);
                this._objUploadFile.sz_StatusCode=str;
                this.ViewState["VSUpload"] = this._objUploadFile;
                list = n_bo.Get_Node_Type(list4);
                flag2 = true;
                if (list.Contains("NFVER"))
                {
                    list.Clear();
                    list.Add("NFVER");
                    this.Session["NODETYPE"] = list;
                }
                else
                {
                    this.Session["NODETYPE"] = list;
                }
                this.BindGrid();
            }
        }
        else if (this.rdlVerification.SelectedValue == "2")
        {
            ArrayList list6 = new ArrayList();
            ArrayList list7 = new ArrayList();
            for (int j = 0; j < this.grdVeriFication.Rows.Count; j++)
            {
                CheckBox box2 = (CheckBox)this.grdVeriFication.Rows[j].FindControl("ChkDelete");
                if (box2.Checked)
                {
                    string str5 = this.grdVeriFication.DataKeys[j][0].ToString();
                    str3 = this.grdVeriFication.DataKeys[j][4].ToString();
                    list7.Add(str5);
                    list2.Add(this.txtCaseID.Text);
                    list3.Add(str3);
                    str2 = str2 + str5 + ",";
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_bill_no=str5;
                    desc.sz_description=this.txtSaveDescription.Text;
                    desc.sz_verification_date=DBNull.Value.ToString();
                    desc.i_verification=2;
                    desc.sz_company_id=this.txtCompanyID.Text;
                    desc.sz_user_id=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    desc.sz_flag="VS";
                    list6.Add(desc);
                    flag = true;
                }
            }
            this._objUploadFile = new Bill_Sys_UploadFile();
            this._objUploadFile.sz_bill_no=list7;
            this._objUploadFile.sz_company_id=this.txtCompanyID.Text;
            this._objUploadFile.sz_flag="VS";
            this._objUploadFile.sz_case_id=list2;
            this._objUploadFile.sz_speciality_id=list3;
            if (flag)
            {
                n_bo = new Bill_Sys_BillTransaction_BO();
                str = n_bo.InsertUpdateBillStatus(list6);
                this._objUploadFile.sz_StatusCode=str;
                this.ViewState["VSUpload"] = this._objUploadFile;
                list = n_bo.Get_Node_Type(list6);
                flag2 = false;
                this.Session["NODETYPE"] = list;
                this.BindGrid();
            }
        }
        else if (this.rdlVerification.SelectedValue == "3")
        {
            this.ViewState["Process"] = "";
            this.ViewState["Process"] = "DEN";
            if (!this.hfdenialReason.Value.Equals(""))
            {
                char ch = ',';
                string[] strArray = this.hfdenialReason.Value.Split(new char[] { ch });
                ArrayList list8 = new ArrayList();
                ArrayList list9 = new ArrayList();
                ArrayList list10 = new ArrayList();
                bool flag3 = false;
                for (int k = 0; k < this.grdVeriFication.Rows.Count; k++)
                {
                    CheckBox box3 = (CheckBox)this.grdVeriFication.Rows[k].FindControl("ChkDelete");
                    if (box3.Checked)
                    {
                        string str6 = this.grdVeriFication.DataKeys[k][0].ToString();
                        str3 = this.grdVeriFication.DataKeys[k][4].ToString();
                        list3.Add(str3);
                        list2.Add(this.txtCaseID.Text);
                        str2 = str2 + str6 + ",";
                        desc = new Bill_Sys_Verification_Desc();
                        desc.sz_bill_no=str6;
                        desc.sz_description=this.txtSaveDescription.Text;
                        desc.sz_verification_date=this.txtSaveDate.Text;
                        desc.i_verification=3;
                        desc.sz_company_id=this.txtCompanyID.Text;
                        desc.sz_user_id=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                        desc.sz_flag="DEN";
                        list8.Add(desc);
                        flag = true;
                        list9.Add(str6);
                    }
                }
                this._objUploadFile = new Bill_Sys_UploadFile();
                this._objUploadFile.sz_bill_no=list9;
                this._objUploadFile.sz_company_id=this.txtCompanyID.Text;
                this._objUploadFile.sz_flag="DEN";
                this._objUploadFile.sz_case_id=list2;
                this._objUploadFile.sz_speciality_id=list3;
                if (flag)
                {
                    n_bo = new Bill_Sys_BillTransaction_BO();
                    str = n_bo.InsertUpdateBillStatus(list8);
                    this._objUploadFile.sz_StatusCode=str;
                    this.ViewState["VSUpload"] = this._objUploadFile;
                    flag2 = true;
                    list = n_bo.Get_Node_Type(list8);
                    flag2 = true;
                    if (this.hfremovedenialreason.Value != "")
                    {
                        string[] strArray2 = this.hfremovedenialreason.Value.Split(new char[] { ',' });
                        ArrayList list11 = new ArrayList();
                        for (int m = 0; m < (strArray2.Length - 1); m++)
                        {
                            list11.Add(strArray2[m].ToString());
                        }
                        for (int n = 0; n < (strArray.Length - 1); n++)
                        {
                            flag3 = false;
                            for (int num6 = 0; num6 < list11.Count; num6++)
                            {
                                if (strArray[n].ToString() == list11[num6].ToString())
                                {
                                    flag3 = true;
                                    break;
                                }
                            }
                            if (!flag3)
                            {
                                list10.Add(strArray[n].ToString());
                            }
                        }
                    }
                    else
                    {
                        for (int num7 = 0; num7 < (strArray.Length - 1); num7++)
                        {
                            list10.Add(strArray[num7].ToString());
                        }
                    }
                    if (str != "")
                    {
                        for (int num8 = 0; num8 < list9.Count; num8++)
                        {
                            n_bo.UpdateDenialReason(str, list10, list9[num8].ToString());
                        }
                        if (list.Contains("NFVER"))
                        {
                            list.Clear();
                            list.Add("NFDEN");
                            this.Session["NODETYPE"] = list;
                        }
                        else
                        {
                            this.Session["NODETYPE"] = list;
                        }
                        this.BindGrid();
                    }
                }
            }
        }
        this.Session["SPECIALITY"] = list3;
        this.rdlVerification.ClearSelection();
        if (!str.Equals("") && flag2)
        {
            this.Session["STATUS_CODE"] = str;
            hdnStatusCode.Value = str;
            this.ModalPopupExtender1.Hide();
            this.ModalPopupExtender2.Show();
            this.lblBillValue.Text = str2;
            hdnBillNumber.Value = str2;
            this.lblDescValue.Text = this.txtSaveDescription.Text;
            this.lblcurrntDateValue.Text = this.lblSaveDate.Text;
            this.lblveridatevalue.Text = this.txtSaveDate.Text;
        }
    }

    protected void btnSaveSendRequest_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = "";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            for (int i = 0; i < this.grdVerificationSend.Items.Count; i++)
            {
                Bill_Sys_Verification_Desc desc;
                TextBox box = (TextBox)this.grdVerificationSend.Items[i].FindControl("taxAns");
                if (box.Text.Trim().ToString() != "")
                {
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_answer=box.Text;
                    desc.sz_bill_no=this.grdVerificationSend.Items[i].Cells[0].Text.ToString();
                    desc.sz_verification_id=this.grdVerificationSend.Items[i].Cells[8].Text.ToString();
                    desc.sz_case_id=this.grdVerificationSend.Items[i].Cells[9].Text.ToString();
                    list.Add(desc);
                    if (str == "")
                    {
                        str = "'" + this.grdVerificationSend.Items[i].Cells[0].Text.ToString() + "'";
                    }
                    else
                    {
                        str = str + ",'" + this.grdVerificationSend.Items[i].Cells[0].Text.ToString() + "'";
                    }
                }
                else
                {
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_answer=box.Text;
                    desc.sz_bill_no=this.grdVerificationSend.Items[i].Cells[0].Text.ToString();
                    desc.sz_verification_id=this.grdVerificationSend.Items[i].Cells[8].Text.ToString();
                    desc.sz_answer_id=this.grdVerificationSend.Items[i].Cells[7].Text.ToString();
                    desc.sz_company_id=this.txtCompanyID.Text;
                    list2.Add(desc);
                }
            }
            Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
            if (list2.Count > 0)
            {
                new Bill_Sys_BillTransaction_BO().DeleteVerificationAns(list2);
                this.ModalPopupExtender4.Show();
            }
            if (list.Count > 0)
            {
                if (template.SetVerification_Answer(list, this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME) == 1)
                {
                    this.usrMessage1.PutMessage("Saved successfully.");
                    this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage1.Show();
                    DataSet set = new DataSet();
                    set = template.GetVerification_Answer(str, this.txtCompanyID.Text);
                    this.grdVerificationSend.DataSource = set.Tables[0];
                    this.grdVerificationSend.DataBind();
                    this.ModalPopupExtender4.Show();
                }
                else
                {
                    this.usrMessage1.PutMessage("Error in transaction");
                    this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage1.Show();
                    this.ModalPopupExtender4.Show();
                }
            }
            else if ((list2.Count <= 0) && (list.Count <= 0))
            {
                this.usrMessage1.PutMessage("add atleast one answer...");
                this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage1.Show();
                this.ModalPopupExtender4.Show();
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

    protected void btnSetDenial_Click(object sender, EventArgs e)
    {
        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string str = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
        ArrayList list = new ArrayList();
        string str2 = "";
        bool flag = false;
        for (int i = 0; i < this.grdBillSearch.Items.Count; i++)
        {
            CheckBox box = (CheckBox)this.grdBillSearch.Items[i].Cells[0x15].FindControl("chkDenial");
            string text1 = "'" + this.grdBillSearch.Items[i].Cells[2].Text + "'";
            if (box.Checked)
            {
                if (!flag)
                {
                    str2 = "'" + this.grdBillSearch.Items[i].Cells[2].Text + "'";
                    flag = true;
                }
                else
                {
                    str2 = str2 + ",'" + this.grdBillSearch.Items[i].Cells[2].Text + "'";
                }
            }
        }
        list.Add(str2);
        list.Add(this.txtCompanyID.Text);
        list.Add(str);
        new Bill_Sys_BillTransaction_BO().updateBillStatusToDenial(list);
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
        try
        {
            if (!this.fuUploadReport.HasFile)
            {
                this.Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');</script>");
                this.ModalPopupExtender3.Show();
            }
            else
            {
                string str = template.getPhysicalPath();
                string companyName = "";
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                companyName = template.GetCompanyName(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this._obj = new Bill_Sys_BillTransaction_BO();
                ArrayList list3 = new ArrayList();
                list3 = (ArrayList)this.Session["NODETYPE"];
                list = this._obj.GetNodeID_Nodes(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, list3);
                Bill_Sys_RequiredDocumentBO tbo = new Bill_Sys_RequiredDocumentBO();
                for (int i = 0; i < list.Count; i++)
                {
                    list2.Add(tbo.GetNodePath(list[i].ToString(), this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                if (list2.Count > 1)
                {
                    for (int k = 0; k < list2.Count; k++)
                    {
                        string[] strArray = list2[k].ToString().Replace(@"\", "/").Split(new char[] { '/' });
                        if (strArray != null)
                        {
                            companyName = (strArray[0] + @"\Common Folder\" + this.txtCaseID.Text + @"\Verification Requests\").Replace(@"\", "/");
                            this.strLinkPath = companyName + this.upFile.FileName;
                            if (!Directory.Exists(str + companyName))
                            {
                                Directory.CreateDirectory(str + companyName);
                            }
                            this.upFile.SaveAs(str + companyName + this.upFile.FileName);
                        }
                    }
                }
                else
                {
                    for (int m = 0; m < list2.Count; m++)
                    {
                        companyName = (list2[m].ToString() + @"\").Replace(@"\", "/");
                        this.strLinkPath = companyName + this.fuUploadReport.FileName;
                        if (!Directory.Exists(str + companyName))
                        {
                            Directory.CreateDirectory(str + companyName);
                        }
                        this.fuUploadReport.SaveAs(str + companyName + this.fuUploadReport.FileName);
                    }
                }
                ArrayList arrayList = new ArrayList();
                ArrayList list5 = new ArrayList();
                for (int j = 0; j < list.Count; j++)
                {
                    arrayList.Add(this.txtCaseID.Text);
                    arrayList.Add("");
                    arrayList.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    arrayList.Add(this.fuUploadReport.FileName);
                    arrayList.Add(companyName);
                    arrayList.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
                    arrayList.Add(list[j]);
                    list5.Add(template.SaveDocumentData(arrayList));
                }
                if (list5 != null)
                {
                    for (int n = 0; n < list5.Count; n++)
                    {
                        template.InsertPaymentImage(this.Session["BillNo"].ToString(), this.txtCompanyID.Text, list5[n].ToString(), ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, this.Session["STATUS_CODE"].ToString(), "VERIFICATIONSENT");
                    }
                }
                this.lblMsg.Text = "File Upload Successfully";
                this.lblMsg.Visible = true;
                this.BindGrid();
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

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
        new ArrayList();
        ArrayList list = new ArrayList();
        try
        {
            if (!this.upFile.HasFile)
            {
                this.Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");
                this.ModalPopupExtender2.Show();
            }
            else
            {
                Bill_Sys_UploadFile file = (Bill_Sys_UploadFile)this.ViewState["VSUpload"];
                file.sz_FileName=this.upFile.FileName;
                file.sz_File=this.upFile.FileBytes;
                file.sz_UserName=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME.ToString();
                file.sz_UserId=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                list = new FileUpload().UploadFile(file);
                char ch = ',';
                string[] strArray = this.lblBillValue.Text.Split(new char[] { ch });
                for (int i = 0; i < (strArray.Length - 1); i++)
                {
                    if (list != null)
                    {
                        template.InsertPaymentImage(strArray[i].ToString(), this.txtCompanyID.Text, list[0].ToString(), ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, this.Session["STATUS_CODE"].ToString(), "VERIFICATIONSENT");
                    }
                }
                this.lblMsg.Text = "File Upload Successfully";
                this.lblMsg.Visible = true;
                this.BindGrid();
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

    protected void btnVRSave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            ArrayList list = new ArrayList();
            list.Add(this.txtBillNumber.Text);
            list.Add(this.verificationReceivedNotes.Text.Replace("\r\n", "#"));
            list.Add(this.txtCompanyID.Text);
            list.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
            n_bo.saveVerificationReceivedInformation(list);
            this.BindGrid();
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

    public DataSet GetDenialInfo(string sz_CompanyID, string SZ_BILL_NUMBER)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MANAGE_GET_DENIAL_REASON", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            selectCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_BILL_NUMBER);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetPatientDeskList()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO tbo = new Bill_Sys_PatientBO();
        try
        {
            this.rptPatientDeskList.DataSource = tbo.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.txtCompanyID.Text);
            this.rptPatientDeskList.DataBind();
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

    public DataSet GetVerification(string sz_CompanyID, string sz_input_bill_number)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        connection = new SqlConnection(connectionString);
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MANAGE_VERIFICATION_ANSWER", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            selectCommand.Parameters.AddWithValue("@sz_input_bill_number", sz_input_bill_number);
            selectCommand.Parameters.AddWithValue("@bt_operation", 2);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public DataSet GetEORInfo(string sz_CompanyID, string SZ_BILL_NUMBER)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        connection = new SqlConnection(connectionString);
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MANAGE_GET_EOR_REASON", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);            
            selectCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_BILL_NUMBER);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
    }
    protected void grdBillSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void grdVeriFication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int num = 0;
        if (e.CommandName.ToString() == "PLS")
        {
            HideTemplates(this.grdVeriFication);
            this.grdVeriFication.Columns[0x10].Visible = true;
            num = Convert.ToInt32(e.CommandArgument);
            string str = "div";
            str = str + this.grdVeriFication.DataKeys[num][0].ToString();
            GridView view = (GridView)this.grdVeriFication.Rows[num].FindControl("grdVerification1");
            LinkButton lnkP = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkP");
            LinkButton lnkM = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkM");
            string str2 = "'" + this.grdVeriFication.DataKeys[num][0].ToString() + "'";
            string text = this.txtCompanyID.Text;
            DataSet verification = this.GetVerification(text, str2);
            view.DataSource = verification;
            view.DataBind();
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mp", "ShowChildGrid('" + str + "') ;", true);
            lnkP.Visible = false;
            lnkM.Visible = true;
        }
        if (e.CommandName.ToString() == "MNS")
        {
            this.grdVeriFication.Columns[0x10].Visible = false;
            num = Convert.ToInt32(e.CommandArgument);
            string str4 = "div";
            str4 = str4 + this.grdVeriFication.DataKeys[num][0].ToString();
            LinkButton lnkP = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkP");
            LinkButton lnkM = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkM");
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "HideChildGrid('" + str4 + "') ;", true);
            lnkP.Visible = true;
            lnkM.Visible = false;
        }
        if (e.CommandName.ToString() == "DenialPLS")
        {
            HideTemplates(this.grdVeriFication);
            this.grdVeriFication.Columns[0x11].Visible = true;
            this.grdVeriFication.Columns[0x10].Visible = false;
            num = Convert.ToInt32(e.CommandArgument);
            string str5 = "div1";
            str5 = str5 + this.grdVeriFication.DataKeys[num][0].ToString();
            GridView view2 = (GridView)this.grdVeriFication.Rows[num].FindControl("grdDenial");
            LinkButton lnkDP = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkDP");
            LinkButton lnkDM = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkDM");
            string str6 = this.grdVeriFication.DataKeys[num][0].ToString();
            string str7 = this.txtCompanyID.Text;
            DataSet denialInfo = this.GetDenialInfo(str7, str6);
            view2.DataSource = denialInfo;
            view2.DataBind();
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mp", "ShowDenialChildGrid('" + str5 + "') ;", true);
            lnkDP.Visible = false;
            lnkDM.Visible = true;           
        }
        if (e.CommandName.ToString() == "DenialMNS")
        {
            this.grdVeriFication.Columns[0x10].Visible = false;
            this.grdVeriFication.Columns[0x11].Visible = false;
            num = Convert.ToInt32(e.CommandArgument);
            string str8 = "div";
            str8 = str8 + this.grdVeriFication.DataKeys[num][0].ToString();
            LinkButton lnkDP = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkDP");
            LinkButton lnkDM = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkDM");
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "HideDenialChildGrid('" + str8 + "') ;", true);
            lnkDP.Visible = true;
            lnkDM.Visible = false;
        }
        if (e.CommandName.ToString() == "eorPLS")
        {
            HideTemplates(this.grdVeriFication);            
            this.grdVeriFication.Columns[0x13].Visible = true;
            num = Convert.ToInt32(e.CommandArgument);
            string str = "div2";
            str = str + this.grdVeriFication.DataKeys[num][0].ToString();
            GridView view = (GridView)this.grdVeriFication.Rows[num].FindControl("grdEor");
            LinkButton lnkNumEP = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkEP");
            LinkButton lnkNumEM = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkEM");
            string str2 = this.grdVeriFication.DataKeys[num][0].ToString();
            string text = this.txtCompanyID.Text;
            DataSet eor = this.GetEORInfo(text, str2);
            view.DataSource = eor;
            view.DataBind();
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mp", "ShowChildGrid('" + str + "') ;", true);
            lnkNumEP.Visible = false;
            lnkNumEM.Visible = true;
        }
        if (e.CommandName.ToString() == "eorMNS")
        {
            this.grdVeriFication.Columns[0x10].Visible = false;
            this.grdVeriFication.Columns[0x11].Visible = false;
            this.grdVeriFication.Columns[0x13].Visible = false;
            num = Convert.ToInt32(e.CommandArgument);
            string str8 = "div2";
            str8 = str8 + this.grdVeriFication.DataKeys[num][0].ToString();
            LinkButton lnkEP = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkEP");
            LinkButton lnkEM = (LinkButton)this.grdVeriFication.Rows[num].FindControl("lnkEM");
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "HideDenialChildGrid('" + str8 + "') ;", true);
            lnkEP.Visible = true;
            lnkEM.Visible = false;
        }
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdVeriFication.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void lnkscan_Click(object sender, EventArgs e)
    {
        int selectedIndex = this.grdBillSearch.SelectedIndex;
        LinkButton button = (LinkButton)sender;
        TableCell parent = (TableCell)button.Parent;
        DataGridItem item = (DataGridItem)parent.Parent;
        int itemIndex = item.ItemIndex;
        this.Session["ScanBillNo"] = item.Cells[2].Text;
        this.RedirectToScanApp(selectedIndex);
    }

    protected void lnkScanuplaod_Click(object sender, EventArgs e)
    {
        string str = this.Session["STATUS_CODE"].ToString();
        string str2 = ConfigurationManager.AppSettings["webscanurl"].ToString();
        this._obj = new Bill_Sys_BillTransaction_BO();
        string str3 = "";
        string str4 = "";
        string s = "";
        ArrayList list = new ArrayList();
        ArrayList list2 = new ArrayList();
        string str6 = ConfigurationManager.AppSettings["BASEPATH"].ToString().Replace("/", @"\");
        string str7 = "";
        string str8 = "";
        list2 = (ArrayList)this.Session["SPECIALITY"];
        list = (ArrayList)this.Session["NODETYPE"];
        if (list.Contains("NFVER"))
        {
            str4 = "OLD";
            str3 = "NFVER";
            str8 = "VR";
        }
        else if (list.Contains("NFDEN"))
        {
            str4 = "OLD";
            str3 = "NFDEN";
            str8 = "DEN";
        }
        else
        {
            str4 = "NEW";
        }
        if (str4 == "OLD")
        {
            str7 = list2[0].ToString();
            s = this._obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, str3);
        }
        else
        {
            for (int i = 1; i < list2.Count; i++)
            {
                if (str7 == "")
                {
                    str7 = list2[i].ToString();
                }
                else
                {
                    str7 = str7 + "," + list2[i].ToString();
                }
                string str9 = list2[i].ToString();
                string str10 = list2[i - 1].ToString();
                if (str9 != str10)
                {
                    if (list.Contains("VR"))
                    {
                        s = @"Common Folder\" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID + @"\" + ConfigurationManager.AppSettings["VR"].ToString() + @"\";
                    }
                    else
                    {
                        s = @"Common Folder\" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID + @"\" + ConfigurationManager.AppSettings["DEN"].ToString() + @"\";
                    }
                    break;
                }
            }
            if (this.ViewState["Process"].ToString() == "VR")
            {
                str8 = "VR";
            }
            else if (this.ViewState["Process"].ToString() == "DEN")
            {
                str8 = "DEN";
            }
            if (s == "")
            {
                string specName = new Bill_Sys_BillTransaction_BO().GetSpecName(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, list2[0].ToString());
                list[0].ToString();
                if (str8 == "VR")
                {
                    str7 = list2[0].ToString();
                    s = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + @"\" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID + @"\No Fault File\Medicals\" + specName + @"\" + ConfigurationManager.AppSettings["VR"].ToString() + @"\";
                }
                else if (str8 == "DEN")
                {
                    str7 = list2[0].ToString();
                    s = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + @"\" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID + @"\No Fault File\Medicals\" + specName + @"\" + ConfigurationManager.AppSettings["DEN"].ToString() + @"\";
                }
            }
            s = Convert.ToBase64String(Encoding.Unicode.GetBytes(s));
        }
        str2 = str2 + "&Flag=ReqVeriRadio&CompanyId=" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "&CaseId=" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID + "&UserName=" + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME + "&CompanyName=" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        str2 = str2 + "&CaseNo=" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO + "&PName=" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_NAME + "&NodeId=" + s + "&StatusID=" + str + "&UserId=" + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "starScript", "window.open('" + (str2 + "&CaseType=" + str4 + "&Speciality=" + str7.Trim() + "&Process=" + str8) + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }

    protected void lnkuplaod_Click(object sender, EventArgs e)
    {
        LinkButton button = (LinkButton)sender;
        TableCell parent = (TableCell)button.Parent;
        DataGridItem item = (DataGridItem)parent.Parent;
        int itemIndex = item.ItemIndex;
        this.Session["BillNo"] = item.Cells[2].Text;
        this.ModalPopupExtender3.Show();
    }

    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.grdVeriFication.Columns[0x10].Visible = false;
        this.grdVeriFication.Columns[0x11].Visible = false;
        try
        {
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.drdUpdateStatus.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.lblSaveDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            this.rdlVerification.Attributes.Add("onclick", "return Validate()");
            this.btnSaveDesc.Attributes.Add("onclick", "return checkdenial()");
            this.btnSaveDate.Attributes.Add("onclick", "return checkedDate()");
            this.btnBillUpdateStatus.Attributes.Add("onclick", "return confirm_update_bill_status();");
            this.con.SourceGrid=this.grdVeriFication;
            this.txtSearchBox.SourceGrid=this.grdVeriFication;
            this.grdVeriFication.Page=this.Page;
            this.grdVeriFication.PageNumberList=this.con;
            Bill_Sys_Case_Denial denial = new Bill_Sys_Case_Denial();
            if (!base.IsPostBack)
            {
                hdnCaseId.Value = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._objUploadFile = new Bill_Sys_UploadFile();
                if (this.Session["CASE_OBJECT"] != null)
                {
                    this.extddenial.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this.lblDateValue.Text = DateTime.Today.ToString("MM/dd/yyyy");
                    Bill_Sys_Case @case = new Bill_Sys_Case();
                    @case.SZ_CASE_ID=this.txtCaseID.Text;
                    this.Session["CASEINFO"] = @case;
                    this.Session["PassedCaseID"] = this.txtCaseID.Text;
                    string str2 = this.Session["PassedCaseID"].ToString();
                    this.Session["QStrCaseID"] = str2;
                    this.Session["Case_ID"] = str2;
                    this.Session["Archived"] = "0";
                    this.Session["QStrCID"] = str2;
                    this.Session["SelectedID"] = str2;
                    this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    this.Session["SN"] = "0";
                    this.Session["LastAction"] = "vb_CaseInformation.aspx";
                    this.Session["SZ_CASE_ID_NOTES"] = this.txtCaseID.Text;
                    this.Session["TM_SZ_CASE_ID"] = this.txtCaseID.Text;
                    this.GetPatientDeskList();
                }
                else
                {
                    base.Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }
                this.BindGrid();
            }
            this.lnkAddGeneralDenial.Text = "General Denial(" + denial.GetDenialCountForCase(this.txtCaseID.Text.ToString()).ToString() + ")";
            this.lblMsg.Visible = false;
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
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_VerificationsentBills.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void rdlVerification_SelectedIndex(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool flag = false;
        for (int i = 0; i < this.grdVeriFication.Rows.Count; i++)
        {
            CheckBox box = (CheckBox)this.grdVeriFication.Rows[i].FindControl("ChkDelete");
            if (box.Checked)
            {
                flag = true;
                break;
            }
        }
        if (flag)
        {
            if (this.rdlVerification.SelectedValue == "1")
            {
                this.ModalPopupExtender1.Show();
                this.lblSaveDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
                this.txtSaveDate.Text = "";
                this.txtSaveDescription.Text = "";
                this.lblSaveDatevalue.Visible = true;
                this.lblSaveDatevalue.Text = "Verification Date";
                this.imgSavebtnToDate.Visible = true;
                this.txtSaveDate.Visible = true;
                this.lblDenial.Visible = false;
                this.extddenial.Visible = false;
                this.lbSelectedDenial.Visible = false;
                this.btnAddDenial.Visible = false;
                this.btnRemoveDenial.Visible = false;
                this.hfdenialReason.Value = "";
                this.Session["NODETYPE"] = "";
                this.btnSaveDesc.Visible = false;
                this.btnSavesent.Visible = false;
                this.btnSaveDate.Visible = true;
                this.lblupverificationdesc.Text = "Verification Received";
                this.lblHeaderValue.Text = "Verification Received";
                this.lblHeader.Text = "Verification Recevied";
            }
            else
            {
                if (this.rdlVerification.SelectedValue == "2")
                {
                    string str = "";
                    for (int j = 0; j < this.grdVeriFication.Rows.Count; j++)
                    {
                        CheckBox box2 = (CheckBox)this.grdVeriFication.Rows[j].FindControl("ChkDelete");
                        if (box2.Checked)
                        {
                            if (str == "")
                            {
                                str = "'" + this.grdVeriFication.DataKeys[j][0].ToString() + "'";
                            }
                            else
                            {
                                str = str + ",'" + this.grdVeriFication.DataKeys[j][0].ToString() + "'";
                            }
                        }
                    }
                    try
                    {
                        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
                        DataSet set = new DataSet();
                        set = template.GetVerification_Answer(str, this.txtCompanyID.Text);
                        this.grdVerificationSend.DataSource = set.Tables[0];
                        this.grdVerificationSend.DataBind();
                        this.rdlVerification.ClearSelection();
                        this.BindGrid();
                        this.ModalPopupExtender4.Show();
                        return;
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
                if (this.rdlVerification.SelectedValue == "3")
                {
                    this.ModalPopupExtender1.Show();
                    this.lblSaveDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
                    this.txtSaveDate.Text = "";
                    this.lblSaveDatevalue.Text = "Denial Date";
                    this.lblSaveDatevalue.Visible = true;
                    this.imgSavebtnToDate.Visible = true;
                    this.lblVeridate.Visible = false;
                    this.txtSaveDate.Visible = true;
                    this.lblDenial.Visible = true;
                    this.lblDenial.Font.Bold = true;
                    this.extddenial.Visible = true;
                    this.lbSelectedDenial.Visible = true;
                    this.hfdenialReason.Value = "";
                    this.txtSaveDescription.Text = "";
                    this.txtSaveDate.Text = "";
                    this.btnAddDenial.Visible = true;
                    this.btnRemoveDenial.Visible = true;
                    this.extddenial.Text="--- Select ---";
                    this.Session["NODETYPE"] = "";
                    this.btnSaveDesc.Visible = true;
                    this.btnSaveDate.Visible = false;
                    this.btnSavesent.Visible = false;
                    this.lblupverificationdesc.Text = "Denial";
                    this.lblHeaderValue.Text = "Denial";
                    this.lblHeader.Text = "Denial";
                }
            }
        }
        else
        {
            this.rdlVerification.ClearSelection();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void RedirectToScanApp(int iindex)
    {
        iindex = this.grdBillSearch.SelectedIndex;
        string str = ConfigurationManager.AppSettings["webscanurl"].ToString();
        this._obj = new Bill_Sys_BillTransaction_BO();
        string str2 = this._obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "NFVER");
        str = str + "&Flag=ReqVeri&CompanyId=" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "&CaseId=" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID + "&UserName=" + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME + "&CompanyName=" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "starScript", "window.open('" + (str + "&CaseNo=" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO + "&PName=" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_NAME + "&NodeId=" + str2 + "&BillNo=" + this.Session["ScanBillNo"].ToString() + "&UserId=" + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID) + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }
    protected void lnkscanPopup_Click(object sender, EventArgs e)
    {
        int iindex = grdVerificationSend.SelectedIndex;
        LinkButton btn = (LinkButton)sender;
        TableCell tc = (TableCell)btn.Parent;
        DataGridItem it = (DataGridItem)tc.Parent;

        Bill_Sys_Verification_Desc _objDesc = new Bill_Sys_Verification_Desc();
        Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
        ArrayList objAL = new ArrayList();
        ArrayList arrNodeType = new ArrayList();
        string szCaesType = "";
        string szProcess = "";

        string[] ArrUpload = new string[3];
        ArrUpload = btn.CommandArgument.ToString().Split(';');
        string sz_Bill_Number = ArrUpload[0];
        string sz_Verification_ID = ArrUpload[1];
        string sz_specialty = ArrUpload[2];
        Session["specialty_ID"] = sz_specialty;

        int index = it.ItemIndex;
        Session["NODETYPESCAN"] = "";
        Session["NODETYPE"] = "";

        _objDesc.sz_bill_no = sz_Bill_Number;
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
        }
        Session["ScanBillNo"] = sz_Bill_Number;

        RedirectToScanApp_OnVerification(iindex, szCaesType, szProcess, arrNodeType[0].ToString(), sz_specialty, sz_Verification_ID);
    }

    protected void lnkuplaodPopup_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        Bill_Sys_Verification_Desc _objDesc;
        ArrayList arr_node_type = new ArrayList();
        ArrayList objAL = new ArrayList();
        Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();

        string[] ArrUpload = new string[3];
        ArrUpload = btn.CommandArgument.ToString().Split(';');
        string sz_Bill_Number = ArrUpload[0];
        string sz_Verification_ID = ArrUpload[1];
        string sz_specialty = ArrUpload[2];



        ArrayList arrBillNo = new ArrayList();
        ArrayList arrSpec = new ArrayList();
        ArrayList arrCaseId = new ArrayList();

        _objDesc = new Bill_Sys_Verification_Desc();
        _objDesc.sz_bill_no = sz_Bill_Number;
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
        arrBillNo.Add(sz_Bill_Number);
        arrSpec.Add(sz_specialty);

        Bill_Sys_UploadFile _objUploadFile = new Bill_Sys_UploadFile();
        _objUploadFile.sz_bill_no = arrBillNo;
        _objUploadFile.sz_company_id = txtCompanyID.Text;
        _objUploadFile.sz_flag = "VR";
        _objUploadFile.sz_case_id = arrCaseId;
        _objUploadFile.sz_speciality_id = arrSpec;
        _objUploadFile.sz_UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
        _objUploadFile.sz_UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString();

        ViewState["VSUpload"] = _objUploadFile;

        //string url = "AnswerVerificationPopup.aspx?BillNumber=" + sz_Bill_Number + "&Verification_ID=" + sz_Verification_ID + "&Specialty=" + sz_specialty + "";
        //PanelVerificationAnswer.ContentUrl = url;
        //PanelVerificationAnswer.Attributes.Add("Hide", "false");
        //this.ModalPopupExtender4.Show();
        ScriptManager.RegisterStartupScript(this, GetType(), "showUploadFilePopupOnAnswerVerification", "showUploadFilePopupOnAnswerVerification('" + sz_Bill_Number + "','" + sz_Verification_ID + "','" + sz_specialty + "');", true);
        //Page.RegisterStartupScript("ss", "<script language='javascript'>showUploadFilePopupOnAnswerVerification();</script>");
    }
    public void RedirectToScanApp_OnVerification(int iindex, string szCaseType, string szProcess, string szNodeType, string sz_specialty, string sz_Verification_ID)
    {
        iindex = (int)grdVerificationSend.SelectedIndex;
        string szUrl = ConfigurationManager.AppSettings["webscanurl"].ToString();
        Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
        string NodeId = "";

        //string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["NODETYPE"].ToString());
        //string szNodeType = "";
        ArrayList arrNodeType = new ArrayList();
        ArrayList arrSpeciality = new ArrayList();
        string szBasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
        arrSpeciality.Add(sz_specialty);

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
                if (szProcess == "VR")
                {
                    NodeId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "\\" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "\\" + "Nofault" + "\\" + "Medicals" + "\\" + szSpecName + "\\" + ConfigurationManager.AppSettings["VR"].ToString() + "\\";
                }
                else
                    if (szProcess == "DEN")
                    {
                        NodeId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "\\" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "\\" + "Nofault" + "\\" + "Medicals" + "\\" + szSpecName + "\\" + ConfigurationManager.AppSettings["DEN"].ToString() + "\\";
                    }
            }
            NodeId = Convert.ToBase64String(Encoding.Unicode.GetBytes(NodeId));
        }

        szUrl = szUrl + "&Flag=VerAns" + "&CompanyId=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "&CaseId=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "&UserName=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME + "&CompanyName=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        szUrl = szUrl + "&CaseNo=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO + "&PName=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME + "&NodeId=" + NodeId + "&BillNo=" + Session["ScanBillNo"].ToString() + "&UserId=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID + "&StatusID=" + sz_Verification_ID;
        szUrl = szUrl + "&CaseType=" + szCaseType + "&Speciality=" + arrSpeciality[0].ToString().Trim() + "&Process=" + szProcess;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szUrl + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }

    protected void HideTemplates(XGridView.XGridViewControl grdVeriFication)
    {
        for (int i = 0; i < grdVeriFication.Rows.Count; i++)
        {
            LinkButton lnkP = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkP");
            LinkButton lnkDP = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkDP");
            LinkButton lnkEP = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkEP");
            lnkP.Visible = true;
            lnkDP.Visible = true;
            lnkEP.Visible = true;
            LinkButton lnkM = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkM");
            LinkButton lnkDM = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkDM");
            LinkButton lnkEM = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkEM");
            lnkM.Visible = false;
            lnkDM.Visible = false;
            lnkEM.Visible = false;
        }
    }
}