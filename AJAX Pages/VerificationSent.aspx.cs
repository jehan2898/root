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

public partial class AJAX_Pages_VerificationSent : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
        btnSaveSendRequest.Attributes.Add("onclick", "clearData();");

        if (!base.IsPostBack)
        {
            hdnBillNumber.Value = Request.QueryString["billNo"].ToString();
            hdnSpecialty.Value = Request.QueryString["SpecialtyId"].ToString();
            hdnCaseId.Value = Request.QueryString["CaseId"].ToString();
            BindGrid();
        }
    }

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dset;
        try
        {
            string hdnBills = hdnBillNumber.Value.ToString();

            string[] sBillNumber = System.Text.RegularExpressions.Regex.Split(hdnBills, @"\,");
            string billNo = "";
            for (int i = 0; i < sBillNumber.Length; i++)
            {
                if (sBillNumber.Length > 0)
                {
                    if (sBillNumber[i] != "")
                    {
                        if (billNo == "")
                        {
                            billNo = "'" + sBillNumber[i].ToString() + "'";
                        }
                        else
                        {
                            billNo = billNo + "," + "'" + sBillNumber[i].ToString() + "'";
                        }
                    }
                }
            }
            Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
            dset = new DataSet();
            dset = template.GetVerification_Answer(billNo, this.txtCompanyID.Text);
            grdVerificationSend.DataSource = dset.Tables[0];
            grdVerificationSend.DataBind();
        }
        catch (Exception ex)
        {
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = ex.Message.ToString();
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

    protected void btnSaveSendRequest_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string billNo = "";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();

            for (int i = 0; i < this.grdVerificationSend.Items.Count; i++)
            {
                Bill_Sys_Verification_Desc desc;
                TextBox box = (TextBox)this.grdVerificationSend.Items[i].FindControl("txtBoxAns");

                string cleanstr = WebUtils.CleanText(box.Text);

                if (box.Text.Trim().ToString() != "")
                {
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_answer = cleanstr;
                    desc.sz_bill_no = this.grdVerificationSend.Items[i].Cells[0].Text.ToString();
                    desc.sz_verification_id = this.grdVerificationSend.Items[i].Cells[8].Text.ToString();
                    desc.sz_case_id = this.grdVerificationSend.Items[i].Cells[9].Text.ToString();
                    list.Add(desc);
                    if (billNo == "")
                    {
                        billNo = "'" + this.grdVerificationSend.Items[i].Cells[0].Text.ToString() + "'";
                    }
                    else
                    {
                        billNo = billNo + ",'" + this.grdVerificationSend.Items[i].Cells[0].Text.ToString() + "'";
                    }
                }
                else
                {
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_answer = box.Text;
                    desc.sz_bill_no = this.grdVerificationSend.Items[i].Cells[0].Text.ToString();
                    desc.sz_verification_id = this.grdVerificationSend.Items[i].Cells[8].Text.ToString();
                    desc.sz_answer_id = this.grdVerificationSend.Items[i].Cells[7].Text.ToString();
                    desc.sz_company_id = this.txtCompanyID.Text;
                    list2.Add(desc);
                }
            }
            Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
            if (list2.Count > 0)
            {
                new Bill_Sys_BillTransaction_BO().DeleteVerificationAns(list2);
            }
            if (list.Count > 0)
            {
                if (template.SetVerification_Answer(list, this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME) == 1)
                {
                    lblErrorMessage.Visible = false;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Record saved successfully";
                    BindGrid();
                }
                else
                {
                    lblMessage.Visible = false;
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "";
                    lblErrorMessage.Text = "Error in transaction";
                }
            }
            else if ((list2.Count <= 0) && (list.Count <= 0))
            {
                lblMessage.Visible = false;
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "";
                lblErrorMessage.Text = "Add atleast one answer";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = false;
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = "";
            lblErrorMessage.Text = ex.Message.ToString();
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