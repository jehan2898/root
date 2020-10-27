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
using gb.mbs.da.services.bill;

public partial class AJAX_Pages_EORReceived : PageBase
{
    Bill_Sys_UploadFile _objUploadFile;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.lblSaveDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            btnSavesent.Attributes.Add("onclick", "return CheckValid()");
            if (!base.IsPostBack)
            {
                hdnBillNumber.Value = Request.QueryString["billNo"].ToString();
                hdnSpecialty.Value = Request.QueryString["SpecialtyId"].ToString();
                hdnCaseId.Value = Request.QueryString["CaseId"].ToString();
                Bill_Sys_BillTransaction_BO n_bo=new Bill_Sys_BillTransaction_BO();
                DataSet ds= n_bo.FillEORReasons(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                ddlReasons.DataSource =ds;
                ddlReasons.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlReasons.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlReasons.DataBind();
                // Then add your first item
                ddlReasons.Items.Insert(0, new ListItem("-- Select --", "NA"));
                ddlReasons.SelectedIndex = 0;
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
        try
        {
            Bill_Sys_BillTransaction_BO n_bo;
            Bill_Sys_Verification_Desc desc;
            bool flag = false;
            bool flag2 = false;
            string statusCode = "";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            ArrayList list3 = new ArrayList();
            ArrayList listReasons = new ArrayList();

            this.ViewState["Process"] = "";
            this.ViewState["Process"] = "EOR";
            ArrayList list4 = new ArrayList();
            ArrayList list5 = new ArrayList();
            string specialty = "";
            string[] sBillNumber = System.Text.RegularExpressions.Regex.Split(hdnBillNumber.Value.ToString(), @"\,");
            for (int j = 0; j < sBillNumber.Length; j++)
            {
                if (!string.IsNullOrEmpty(hfEORReason.Value)) listReasons.AddRange(hfEORReason.Value.TrimEnd(',').Split(','));
                string sBillNo = sBillNumber[j].ToString(); // bill no
                specialty = hdnSpecialty.Value.ToString(); //Specialty
                list2.Add(txtCaseID.Text);
                list5.Add(sBillNo);
                list3.Add(specialty);
                desc = new Bill_Sys_Verification_Desc();
                desc._sz_verification_reasons = listReasons.ToString();
                desc.sz_bill_no = sBillNo;
                desc.sz_description = this.txtSaveDescription.Text;
                desc.sz_verification_date = this.txtSaveDate.Text;
                desc.i_verification = 4;
                desc.sz_company_id = this.txtCompanyID.Text;
                desc.sz_user_id = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                desc.sz_flag = "EOR";
                list4.Add(desc);
                flag = true;
            }

            this._objUploadFile = new Bill_Sys_UploadFile();
            this._objUploadFile.sz_bill_no = list5;
            this._objUploadFile.sz_company_id = this.txtCompanyID.Text;
            this._objUploadFile.sz_flag = "EOR";
            this._objUploadFile.sz_case_id = list2;
            this._objUploadFile.sz_speciality_id = list3;

            if (flag)
            {
                n_bo = new Bill_Sys_BillTransaction_BO();
                statusCode = n_bo.InsertUpdateBillStatus(list4);
                this._objUploadFile.sz_StatusCode = statusCode;
                this.ViewState["VSUpload"] = this._objUploadFile;
                list = n_bo.Get_Node_Type(list4);
                flag2 = true;
                if (statusCode != "")
                {
                    DataTable eorReasonsWithBill = new DataTable();
                    eorReasonsWithBill.Columns.Add("verificationReasonIds");
                    eorReasonsWithBill.Columns.Add("billNumber");
                    foreach (string billNo in sBillNumber)
                        foreach (string eorId in listReasons)
                            eorReasonsWithBill.Rows.Add(eorId, billNo);
                    n_bo.UpdateEORReason(statusCode, eorReasonsWithBill);
                }
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
            }

            if (!statusCode.Equals("") && flag2)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Record saved successfully";
                hdnStatusCode.Value = statusCode;
            }
            HtmlAnchor anchorScan = (HtmlAnchor)this.FindControl("anchorScan");
            anchorScan.Visible = true;
            BindGrid();
            lblScan.Visible = true;
            lblScan.InnerText = "[Scan/Upload here to add the same document against all selected bills]";
        }
        catch (Exception ex)
        {
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = ex.Message.ToString();
        }
    }

    public void BindGrid()
    {
        DataSet dset;
        try
        {
            string hdnBills = hdnBillNumber.Value.ToString();
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            string[] sBillNumber = System.Text.RegularExpressions.Regex.Split(hdnBills, @"\,");
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
            dtBillNumbers.Columns.Add("sz_bill_id", typeof(string));
            dtBillNumbers.Columns.Add("sz_assigned_lawfirm_id", typeof(string));
            dtBillNumbers.Columns.Add("sz_company_id", typeof(string));

            foreach (gbmodel.bill.Bill sBill in lstBills)
            {
                DataRow row = dtBillNumbers.NewRow();
                row["sz_bill_id"] = sBill.Number;
                dtBillNumbers.Rows.Add(row);
            }

            gbmodel.account.Account oAccount = new gbmodel.account.Account();
            oAccount.ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            SrvBill srvBill = new SrvBill();
            dset = new DataSet();
            //dset = srvBill.GetBilVerificationlDetails(dtBillNumbers, lstBills, oAccount);
            dset = n_bo.GetBilEORDetails(dtBillNumbers, oAccount.ID.ToString());
            grdEORReceived.DataSource = dset.Tables[0];
            grdEORReceived.DataBind();
        }
        catch (Exception ex)
        {
            lblScan.Visible = false;
            lblMessage.Visible = false;
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = ex.Message.ToString();
        }
    }

    protected void grdEORReceived_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            int rowIndex = e.Item.ItemIndex;
            gbmodel.bill.Bill oBillNo = new gbmodel.bill.Bill();
            gbmodel.bill.verification.Verification p_oVerification = new gbmodel.bill.verification.Verification();
            oBillNo.Number = grdEORReceived.DataKeys[rowIndex].ToString();
            p_oVerification.Id = e.Item.Cells[5].Text.ToString();
            gbmodel.account.Account oAccount = new gbmodel.account.Account();
            oAccount.ID = txtCompanyID.Text;
            SrvBill oSrvBill = new SrvBill();
            int i = oSrvBill.DeleteVerificationDescription(oBillNo, p_oVerification, oAccount);
            if (i > 0)
            {
                lblErrorMessage.Visible = false;
                lblMessage.Visible = true;
                lblMessage.Text = "Record deleted Successfully";
            }
            else
            {
                lblMessage.Visible = false;
                lblErrorMessage.Text = "";
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "An error occured while deleting the record";
            }

            BindGrid();
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
}