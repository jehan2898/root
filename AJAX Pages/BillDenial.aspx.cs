using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using gbmodel = gb.mbs.da.model;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using gb.mbs.da.services.bill;

public partial class AJAX_Pages_BillDenial : PageBase
{
    Bill_Sys_UploadFile _objUploadFile;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.lblSaveDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            btnDelete.Attributes.Add("onclick", "return confirm_bill_delete();");
            btnSavesent.Attributes.Add("onclick", "return checkdenial()");

            if (!base.IsPostBack)
            {
                this.extddenial.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                hdnBillNumber.Value = Request.QueryString["billNo"].ToString();
                hdnSpecialty.Value = Request.QueryString["SpecialtyId"].ToString();
                hdnCaseId.Value = Request.QueryString["CaseId"].ToString();
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = false;
            lblScan.Visible = false;
            lblErrorMessage.Text = "";
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = ex.Message.ToString();
        }
    }

    protected void btnSaveDesc_Click(object sender, EventArgs e)
    {
        Bill_Sys_BillTransaction_BO n_bo;
        Bill_Sys_Verification_Desc desc;
        bool flag = false;
        string statusCode = "";
        ArrayList list = new ArrayList();
        string str2 = "";
        bool flag2 = false;
        ArrayList list2 = new ArrayList();
        ArrayList list3 = new ArrayList();
        string str3 = "";
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
            string specialty = "";
            string[] sBillNumber = System.Text.RegularExpressions.Regex.Split(hdnBillNumber.Value.ToString(), @"\,");
            for (int j = 0; j < sBillNumber.Length; j++)
            {
                    string sBillNo = sBillNumber[j].ToString(); // bill no
                    specialty = hdnSpecialty.Value.ToString(); //Specialty
                    list3.Add(specialty);
                    list2.Add(this.txtCaseID.Text);
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_bill_no = sBillNo;
                    desc.sz_description = this.txtSaveDescription.Text;
                    desc.sz_verification_date = this.txtSaveDate.Text;
                    desc.i_verification = 3;
                    desc.sz_company_id = this.txtCompanyID.Text;
                    desc.sz_user_id = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    desc.sz_flag = "DEN";
                    list8.Add(desc);
                    flag = true;
                    list9.Add(sBillNo);
            }
            this._objUploadFile = new Bill_Sys_UploadFile();
            this._objUploadFile.sz_bill_no = list9;
            this._objUploadFile.sz_company_id = this.txtCompanyID.Text;
            this._objUploadFile.sz_flag = "DEN";
            this._objUploadFile.sz_case_id = list2;
            this._objUploadFile.sz_speciality_id = list3;
            if (flag)
            {
                n_bo = new Bill_Sys_BillTransaction_BO();
                statusCode = n_bo.InsertUpdateBillStatus(list8);
                this._objUploadFile.sz_StatusCode = statusCode;
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
                if (statusCode != "")
                {
                    hdnStatusCode.Value = statusCode;
                    for (int num8 = 0; num8 < list9.Count; num8++)
                    {
                        n_bo.UpdateDenialReason(statusCode, list10, list9[num8].ToString());
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
                    HtmlAnchor anchorScan = (HtmlAnchor)this.FindControl("anchorScan");
                    anchorScan.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Record saved successfully";
                    this.BindGrid();
                    lblScan.Visible = true;
                    lblScan.InnerText = "[Scan/Upload here to add the same document against all selected bills]";
                }
            }
        }
    }

    public void BindGrid()
    {
        DataSet dset;
        try
        {
            string hdnIns = hdnBillNumber.Value.ToString();

            string[] sBillNumber = System.Text.RegularExpressions.Regex.Split(hdnIns, @"\,");
            gbmodel.bill.Bill billNo = new gbmodel.bill.Bill();
            List<gbmodel.bill.Bill> lstBills = new List<gbmodel.bill.Bill>();
            for (int i = 0; i < sBillNumber.Length; i++)
            {
                billNo = new gbmodel.bill.Bill();
                if (sBillNumber.Length > 0)
                {
                    if (sBillNumber[i] != "")
                    {
                        billNo.Number = sBillNumber[i].ToString();
                        lstBills.Add(billNo);
                    }
                }
            }

            DataTable dtBillNumbers = new DataTable();

            //for each of your properties
            dtBillNumbers.Columns.Add("sz_bill_id", typeof(string));

            foreach (gbmodel.bill.Bill sBill in lstBills)
            {
                DataRow row = dtBillNumbers.NewRow();

                //foreach of your properties
                row["sz_bill_id"] = sBill.Number;

                dtBillNumbers.Rows.Add(row);
            }

            gbmodel.account.Account oAccount = new gbmodel.account.Account();
            oAccount.ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            SrvBill srvBill = new SrvBill();
            dset = srvBill.GetBillDenialDetails(dtBillNumbers, lstBills, oAccount);
            grdVerificationReq.DataSource = dset.Tables[0];
            grdVerificationReq.DataBind();

            for (int i = 0; i < grdVerificationReq.Items.Count; i++)
            {
                if (grdVerificationReq.Items[i].Cells[9].Text != "&nbsp;")
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
            lblScan.Visible = false;
            lblMessage.Visible = false;
            lblErrorMessage.Text = "";
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = ex.Message.ToString();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ArrayList objarr = new ArrayList();
        Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
        Bill_Sys_Verification_Desc _obj1;
        ArrayList _objAL = new ArrayList();
        try
        {
            for (int i = 0; i < grdVerificationReq.Items.Count; i++)
            {
                if (((CheckBox)grdVerificationReq.Items[i].Cells[12].FindControl("chkDelete")).Checked == true)
                {
                    string sz_status_code = grdVerificationReq.Items[i].Cells[9].Text.ToString();
                    if (hfconfirm.Value == "delete" || hfconfirm.Value == "no")
                    {
                        Bill_sys_Verification_Pop objpopup = new Bill_sys_Verification_Pop();

                        objpopup.sz_bill_no = grdVerificationReq.Items[i].Cells[0].Text.ToString();
                        objpopup.i_verification_id = grdVerificationReq.Items[i].Cells[5].Text.ToString();
                        objpopup.sz_company_id = txtCompanyID.Text;
                        objpopup.sz_bill_Status = sz_status_code;
                        objpopup.sz_flag = "DEL";
                        objarr.Add(objpopup);
                        _obj1 = new Bill_Sys_Verification_Desc();
                        _obj1.sz_bill_no = grdVerificationReq.Items[i].Cells[0].Text.ToString();
                        _obj1.sz_verification_id = grdVerificationReq.Items[i].Cells[5].Text.ToString();
                        _obj1.sz_company_id = txtCompanyID.Text;
                        _obj1.sz_answer_id = grdVerificationReq.Items[i].Cells[12].Text.ToString();
                        _objAL.Add(_obj1);
                    }

                    else if (hfconfirm.Value == "yes")
                    {
                        Bill_sys_Verification_Pop objpopup = new Bill_sys_Verification_Pop();
                        objpopup.sz_bill_no = grdVerificationReq.Items[i].Cells[0].Text.ToString();
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
                    lblMessage.Visible = false;
                    lblErrorMessage.Text = "";
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = ex.Message.ToString();
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

            BindGrid();
            lblErrorMessage.Visible = true;
            lblScan.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "Record deleted successfully";
        }
        catch (Exception ex)
        {
            lblMessage.Visible = false;
            lblScan.Visible = false;
            lblErrorMessage.Text = "";
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = ex.Message.ToString();
        }
    }
}