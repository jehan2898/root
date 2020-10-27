/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_ReferralBillTransaction.aspx.cs
/*Purpose              :       To Add and Edit Bill Transaction
/*Author               :       Sandeep Y
/*Date of creation     :       19 Dec 2008  
/*Modified By          :
/*Modified Date        :
/************************************************************/

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
using System.Data.SqlClient;
using PDFValueReplacement;
using System.IO;
using mbs.LienBills;
using iTextSharp.text.pdf;
using GeneratePDFFile;
using CUTEFORMCOLib;

public partial class Bill_Sys_ReferralBillTransaction : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_Menu _bill_Sys_Menu;
    private Bill_Sys_InsertDefaultValues objDefaultValue;
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    CaseDetailsBO objCaseDetailsBO;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    Bill_Sys_NF3_Template objNF3Template;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private MUVGenerateFunction _MUVGenerateFunction;
    Bill_Sys_Verification_Desc objVerification_Desc;
    string bt_include;
    String str_1500;


    private void BindAssociateGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindDiagnosisGrid(string typeid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid);
            this.grdDiagonosisCode.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindDiagnosisGrid(string typeid, string code, string description)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid, code, description);
            this.grdDiagonosisCode.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindIC9CodeControl(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        this._bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {
            ArrayList list = new ArrayList();
            list = this._bill_Sys_BillingCompanyDetails_BO.GetIC9CodeData(id);
            if (list.Count > 0)
            {
                this.txtTransDetailID.Text = list[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindLatestTransaction()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
          
            this._listOperation.WebPage=this.Page;
            this._listOperation.Xml_File="MRILatestBillTransaction.xml";
            this._listOperation.LoadList();
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.grdLatestBillTransaction.Columns[11].Visible = false;
                this.grdLatestBillTransaction.Columns[12].Visible = true;
                this.grdLatestBillTransaction.Columns[13].Visible = false;
            }
            else
            {
                this.grdLatestBillTransaction.Items[11].Visible = true;
                this.grdLatestBillTransaction.Items[12].Visible = true;
                this.grdLatestBillTransaction.Items[13].Visible = true;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindTransactionData(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            DataSet set = new DataSet();
            set = this._bill_Sys_BillTransaction.BindTransactionData(id);
            this.grdTransactionDetails.DataSource = set;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                grdTransactionDetails.Columns[6].Visible = false;
                grdTransactionDetails.Columns[7].Visible = true;
            }
            else
            {
                grdTransactionDetails.Columns[6].Visible = true;
                grdTransactionDetails.Columns[7].Visible = false;
            }
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                this.extddlspeciality.Text=set.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP_ID"].ToString();
                if (this.extddlspeciality.Text != "NA")
                {
                    if (this._bill_Sys_BillTransaction.checkUnitRefferal(set.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP_ID"].ToString(), this.txtCompanyID.Text))
                    {
                       // this.grdTransactionDetails.Columns[7].Visible = true;
                        this.grdTransactionDetails.Columns[8].Visible = true;
                        for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                            {
                                if (this.grdTransactionDetails.Items[j].Cells[0].Text == set.Tables[0].Rows[i]["SZ_BILL_TXN_DETAIL_ID"].ToString())
                                {
                                    TextBox box = (TextBox)this.grdTransactionDetails.Items[j].FindControl("txtUnit");
                                    if (((set.Tables[0].Rows[i][8].ToString().Trim() != "0") && (set.Tables[0].Rows[i][8].ToString().Trim() != "&nbsp;")) && (set.Tables[0].Rows[i][8].ToString().Trim() != ""))
                                    {
                                        box.Text = set.Tables[0].Rows[i][8].ToString();
                                    }
                                    else
                                    {
                                        box.Text = "1";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //this.grdTransactionDetails.Columns[7].Visible = false;
                        this.grdTransactionDetails.Columns[8].Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindTransactionDetailsGrid(string billnumber)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            DataSet set = new DataSet();
            set = this._bill_Sys_BillTransaction.BindTransactionData(billnumber);
            this.grdTransactionDetails.DataSource = set;
            this.grdTransactionDetails.DataBind();

            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                grdTransactionDetails.Columns[6].Visible = false;
                grdTransactionDetails.Columns[7].Visible = true;
            }
            else
            {
                grdTransactionDetails.Columns[6].Visible = true;
                grdTransactionDetails.Columns[7].Visible = false;
            }
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                this.extddlspeciality.Text=(set.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP_ID"].ToString());
                if (this.extddlspeciality.Text != "NA")
                {
                    if (this._bill_Sys_BillTransaction.checkUnitRefferal(set.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP_ID"].ToString(), this.txtCompanyID.Text))
                    {
                      //  this.grdTransactionDetails.Columns[7].Visible = true;
                        this.grdTransactionDetails.Columns[8].Visible = true;
                        for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                            {
                                if (this.grdTransactionDetails.Items[j].Cells[0].Text == set.Tables[0].Rows[i]["SZ_BILL_TXN_DETAIL_ID"].ToString())
                                {
                                    TextBox box = (TextBox)this.grdTransactionDetails.Items[j].FindControl("txtUnit");
                                    if (((set.Tables[0].Rows[i][8].ToString().Trim() != "0") && (set.Tables[0].Rows[i][8].ToString().Trim() != "&nbsp;")) && (set.Tables[0].Rows[i][8].ToString().Trim() != ""))
                                    {
                                        box.Text = set.Tables[0].Rows[i][8].ToString();
                                    }
                                    else
                                    {
                                        box.Text = "1";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                       // this.grdTransactionDetails.Columns[7].Visible = false;
                        this.grdTransactionDetails.Columns[8].Visible = false;
                    }
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAddService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["TM_SZ_BILL_ID"] = this.extddlDoctor.Text;
            this.Session["Date_Of_Service"] = this.txtDateOfservice.Text;
            if (this.txtDateOfServiceTo.Visible)
            {
                this.Session["TODate_Of_Service"] = this.txtDateOfServiceTo.Text;
            }
            else
            {
                this.Session["TODate_Of_Service"] = null;
            }
            this.Session["DOCTOR_ID"] = this.extddlDoctor.Text;
            this.Session["SZ_CASE_ID"] = this.txtCaseID.Text;
            this.Session["SELECTED_SERVICES"] = null;
            this.Createtable();
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectDiagnosis.aspx'); ", true);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            this.extddlDoctor.Text="NA";
            this.btnSave.Enabled = true;
            this.btnUpdate.Enabled = false;
            this.grdTransactionDetails.Visible = false;
            this.lblMsg.Visible = false;
            this.ClearControl();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnClearService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearControl();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnFromTo_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtDateOfServiceTo.Visible = true;
            this.lblDateOfService.Visible = true;
            this.lblTo.Visible = true;
            this.imgbtnFromTo.Visible = true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnGenerateWCPDF_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            string str2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            string str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
         //   ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            string str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string str5 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
            string str6 = new WC_Bill_Generation().GeneratePDFForWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.rdbListPDFType.SelectedValue, str4, str3, str, str2, str5, this.hdnSpeciality.Value.ToString(), 1);
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + str6 + "');", true);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i < this.grdDiagonosisCode.Items.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode");
                if (box.Checked)
                {
                    ListItem item = new ListItem(this.grdDiagonosisCode.Items[i].Cells[2].Text + '-' + this.grdDiagonosisCode.Items[i].Cells[4].Text, this.grdDiagonosisCode.Items[i].Cells[1].Text);
                    if (!this.lstDiagnosisCodes.Items.Contains(item))
                    {
                        this.lstDiagnosisCodes.Items.Add(item);
                    }
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnOn_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtDateOfServiceTo.Visible = false;
            this.lblDateOfService.Visible = false;
            this.lblTo.Visible = false;
            this.imgbtnFromTo.Visible = false;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            DataTable table = new DataTable();
            table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            table.Columns.Add("DT_DATE_OF_SERVICE");
            table.Columns.Add("SZ_PROCEDURE_ID");
            table.Columns.Add("SZ_PROCEDURAL_CODE");
            table.Columns.Add("SZ_CODE_DESCRIPTION");
            table.Columns.Add("FLT_AMOUNT");
            table.Columns.Add("FACTOR_AMOUNT");
            table.Columns.Add("FACTOR");
            table.Columns.Add("PROC_AMOUNT");
            table.Columns.Add("DOCT_AMOUNT");
            table.Columns.Add("I_UNIT");
            table.Columns.Add("SZ_TYPE_CODE_ID");
            table.Columns.Add("SZ_PATIENT_TREATMENT_ID");
            table.Columns.Add("SZ_STUDY_NUMBER");
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    DataRow row = table.NewRow();
                    if ((item.Cells[0].Text.ToString() != "&nbsp;") && (item.Cells[0].Text.ToString() != ""))
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
                    }
                    else
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                    }
                    row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
                    row["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
                    row["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
                    row["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
                    if (Convert.ToDecimal(((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0M)
                    {
                        row["FACTOR_AMOUNT"] = ((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString();
                    }
                    if (Convert.ToDecimal(((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0M)
                    {
                        row["FACTOR"] = ((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString();
                    }
                    if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        row["FLT_AMOUNT"] =((TextBox)item.Cells[7].FindControl("txtAmt")).Text.ToString();
                    }
                    else
                    {
                        row["FLT_AMOUNT"] = item.Cells[6].Text.ToString();
                    }
                    //if ((((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0))
                    //{
                    //    row["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
                    //}
                     if ((((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString()) > 0))
                    {
                        row["I_UNIT"] = ((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString();
                    }

                    //row["PROC_AMOUNT"] = item.Cells[9].Text.ToString();
                    //row["DOCT_AMOUNT"] = item.Cells[10].Text.ToString();
                    //row["SZ_TYPE_CODE_ID"] = item.Cells[12].Text.ToString();
                    //row["SZ_PATIENT_TREATMENT_ID"] = item.Cells[13].Text.ToString();
                    //row["SZ_STUDY_NUMBER"] = item.Cells[14].Text.ToString();

                     row["PROC_AMOUNT"] = item.Cells[10].Text.ToString();
                    row["DOCT_AMOUNT"] = item.Cells[11].Text.ToString();
                    row["SZ_TYPE_CODE_ID"] = item.Cells[13].Text.ToString();
                    row["SZ_PATIENT_TREATMENT_ID"] = item.Cells[14].Text.ToString();
                    row["SZ_STUDY_NUMBER"] = item.Cells[15].Text.ToString();
                    table.Rows.Add(row);
                }
            }
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                {
                    if ((this.grdTransactionDetails.Items[i].Cells[0].Text != "") && (this.grdTransactionDetails.Items[i].Cells[0].Text != "&nbsp;"))
                    {
                        CheckBox box = (CheckBox)this.grdTransactionDetails.Items[i].FindControl("chkSelect");
                        if (box.Checked)
                        {
                            string text = this.grdTransactionDetails.Items[i].Cells[1].Text;
                            string str2 = this.grdTransactionDetails.Items[i].Cells[3].Text;
                            for (int j = 0; j < table.Rows.Count; j++)
                            {
                                if ((str2 == table.Rows[j][3].ToString()) && (DateTime.Compare(Convert.ToDateTime(text), Convert.ToDateTime(table.Rows[j][1].ToString())) == 0))
                                {
                                    table.Rows[j].Delete();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        CheckBox box2 = (CheckBox)this.grdTransactionDetails.Items[i].FindControl("chkSelect");
                        if (box2.Checked)
                        {
                            string str3 = this.grdTransactionDetails.Items[i].Cells[1].Text;
                            string str4 = this.grdTransactionDetails.Items[i].Cells[3].Text;
                            for (int k = 0; k < table.Rows.Count; k++)
                            {
                                if ((str4 == table.Rows[k][3].ToString()) && (DateTime.Compare(Convert.ToDateTime(str3), Convert.ToDateTime(table.Rows[k][1].ToString())) == 0))
                                {
                                    table.Rows[k].Delete();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            this.grdTransactionDetails.DataSource = table;
            this.grdTransactionDetails.DataBind();

               if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                grdTransactionDetails.Columns[6].Visible = false;
                grdTransactionDetails.Columns[7].Visible = true;
            }
            else
            {
                grdTransactionDetails.Columns[6].Visible = true;
                grdTransactionDetails.Columns[7].Visible = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
        this._saveOperation = new SaveOperation();
        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            this._saveOperation.WebPage=this.Page;
            this._saveOperation.Xml_File="BillTransaction.xml";
            this._saveOperation.SaveMethod();
            this.BindLatestTransaction();
            int count = this.grdLatestBillTransaction.Items.Count;
            this.lblMsg.Visible = true;
            this.lblMsg.Text = " Bill Saved successfully ! ";
            if (this.Session["AssociateDiagnosis"] != null)
            {
                if (Convert.ToBoolean(this.Session["AssociateDiagnosis"].ToString()))
                {
                    this.btnAddService.Enabled = true;
                    this.btnSave.Enabled = false;
                }
                else
                {
                    this.grdTransactionDetails.Visible = true;
                }
            }
            else
            {
                this.grdTransactionDetails.Visible = true;
            }
        }
        catch (SqlException)
        {
            this.ErrorDiv.InnerText = " Bill Number already exists";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int num = 0;
            DataSet set = (DataSet)this.Session["ProcedureCoes"];
            if (set.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    if ((((row["INSURANCE_ID"].ToString() == "") || (row["INSURANCE_ID"].ToString() == null)) || ((row["INSURANCE_ADDR"].ToString() == "") || (row["INSURANCE_ADDR"].ToString() == null))) || ((row["CLAIM_NO"].ToString() == "") || (row["CLAIM_NO"].ToString() == null)))
                    {
                        num = 1;
                        break;
                    }
                }
                if (num == 0)
                {
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        this.txtReferringCompanyID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        this.txtReferringCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    BillTransactionEO neo = new BillTransactionEO();
                    neo.SZ_CASE_ID=this.txtCaseID.Text;
                    neo.DT_BILL_DATE=Convert.ToDateTime(this.txtBillDate.Text);
                    neo.SZ_COMPANY_ID=this.txtCompanyID.Text;
                    neo.SZ_DOCTOR_ID=this.extddlDoctor.Text;
                    neo.SZ_TYPE=this.ddlType.Text;
                    neo.SZ_TESTTYPE="";
                    neo.SZ_READING_DOCTOR_ID=this.extddlReadingDoctor.Text;
                    neo.SZ_Referring_Company_Id=this.txtReferringCompanyID.Text;
                    neo.SZ_USER_ID=(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    ArrayList list = new ArrayList();
                    foreach (DataGridItem item in this.grdTransactionDetails.Items)
                    {
                        BillProcedureCodeEO eeo = new BillProcedureCodeEO();
                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        eeo.DT_DATE_OF_SERVICE=Convert.ToDateTime(item.Cells[1].Text.ToString());
                        eeo.SZ_PROCEDURE_ID=item.Cells[2].Text.ToString();
                       
                        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            string amt = ((TextBox)item.Cells[7].FindControl("txtAmt")).Text.ToString();

                            if (amt != "&nbsp;")
                            {
                                eeo.FL_AMOUNT = amt;
                            }
                            else
                            {
                                eeo.FL_AMOUNT = "0";
                            }
                        }
                        else
                        {
                            if (item.Cells[6].Text.ToString() != "&nbsp;")
                            {
                                eeo.FL_AMOUNT = item.Cells[6].Text.ToString();
                            }
                            else
                            {
                                eeo.FL_AMOUNT = "0";
                            }
                        }
                        eeo.SZ_BILL_NUMBER="";
                        eeo.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_EMG_BILL== "True")
                        {
                            //if (((item.Cells[14].Text.ToString() == "1") || (item.Cells[14].Text.ToString() == null)) || (item.Cells[14].Text.ToString() == "&nbsp;"))
                            //{
                            //    eeo.I_UNIT="1";
                            //}
                            //else
                            //{
                            //    eeo.I_UNIT=item.Cells[14].Text.ToString();
                            //}

                            if (((item.Cells[15].Text.ToString() == "1") || (item.Cells[15].Text.ToString() == null)) || (item.Cells[15].Text.ToString() == "&nbsp;"))
                            {
                                eeo.I_UNIT = "1";
                            }
                            else
                            {
                                eeo.I_UNIT = item.Cells[15].Text.ToString();
                            }
                        }
                        else
                        {
                            //eeo.I_UNIT=((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
                            eeo.I_UNIT = ((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString();
                        }
                        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            eeo.FLT_PRICE = ((TextBox)item.Cells[7].FindControl("txtAmt")).Text.ToString();
                            eeo.DOCT_AMOUNT = ((TextBox)item.Cells[7].FindControl("txtAmt")).Text.ToString();
                        }
                        else
                        {
                            eeo.FLT_PRICE = ((Label)item.Cells[5].FindControl("lblPrice")).Text;
                            eeo.DOCT_AMOUNT = ((Label)item.Cells[5].FindControl("lblPrice")).Text;
                        }
                     //   eeo.FLT_PRICE=((Label)item.Cells[5].FindControl("lblPrice")).Text;
                       // eeo.DOCT_AMOUNT=((Label)item.Cells[5].FindControl("lblPrice")).Text;
                        eeo.FLT_FACTOR=((Label)item.Cells[5].FindControl("lblFactor")).Text;
                        //if ((item.Cells[9].Text.ToString() != "&nbsp;") && (item.Cells[9].Text.ToString() != ""))
                        //{
                        //    eeo.PROC_AMOUNT=item.Cells[9].Text.ToString();
                        //}
                        //else
                        //{
                        //    eeo.PROC_AMOUNT="0";
                        //}

                        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            eeo.PROC_AMOUNT = ((TextBox)item.Cells[7].FindControl("txtAmt")).Text.ToString();
                        }
                        else
                        {
                            if ((item.Cells[10].Text.ToString() != "&nbsp;") && (item.Cells[10].Text.ToString() != ""))
                            {
                                eeo.PROC_AMOUNT = item.Cells[10].Text.ToString();
                            }
                            else
                            {
                                eeo.PROC_AMOUNT = "0";
                            }
                        }

                        //if ((item.Cells[10].Text.ToString() != "&nbsp;") && (item.Cells[10].Text.ToString() != ""))
                        //{
                        //    eeo.PROC_AMOUNT = item.Cells[10].Text.ToString();
                        //}
                        //else
                        //{
                        //    eeo.PROC_AMOUNT = "0";
                        //}
                        eeo.SZ_DOCTOR_ID=this.extddlDoctor.Text;
                        eeo.SZ_CASE_ID=this.txtCaseID.Text;
                      //  eeo.SZ_TYPE_CODE_ID=item.Cells[12].Text.ToString();
                        eeo.SZ_TYPE_CODE_ID = item.Cells[13].Text.ToString();
                        eeo.FLT_GROUP_AMOUNT="";
                        eeo.I_GROUP_AMOUNT_ID="";
                       // eeo.SZ_PATIENT_TREATMENT_ID=item.Cells[13].Text.ToString();
                        eeo.SZ_PATIENT_TREATMENT_ID = item.Cells[14].Text.ToString();
                        list.Add(eeo);
                    }
                    ArrayList list2 = new ArrayList();
                    foreach (ListItem item2 in this.lstDiagnosisCodes.Items)
                    {
                        BillDiagnosisCodeEO eeo2 = new BillDiagnosisCodeEO();
                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        eeo2.SZ_DIAGNOSIS_CODE_ID=item2.Value.ToString();
                        list2.Add(eeo2);
                    }
                    BillTransactionDAO ndao = new BillTransactionDAO();
                    Result result = new Result();
                    result = ndao.SaveBillTransactionForTest(neo, list, list2, this.extddlspeciality.Text);
                    if (result.msg_code == "ERR")
                    {
                        this.lblMsg.Text = result.msg;
                        this.lblMsg.Visible = true;
                    }
                    else
                    {
                        this.lblMsg.Text = "Bill saved successfully";
                        this.lblMsg.Visible = true;
                        this.txtBillID.Text = result.bill_no;
                        string text = this.txtBillID.Text;
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_CREATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=text;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        this.objCaseDetailsBO = new CaseDetailsBO();
                        string patientID = this.objCaseDetailsBO.GetPatientID(text);
                        if (this.objCaseDetailsBO.GetCaseType(text) == "WC000000000000000001")
                        {
                            this.objDefaultValue = new Bill_Sys_InsertDefaultValues();
                            if (this.grdLatestBillTransaction.Items.Count == 0)
                            {
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_DoctorOpinion.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_ExamInformation.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_History.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_PlanOfCare.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_WorkStatus.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                            }
                            else if (this.grdLatestBillTransaction.Items.Count >= 1)
                            {
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_DoctorsOpinionC4_2.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_ExaminationTreatment.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_PermanentImpairment.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_WorkStatusC4_2.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                            }
                        }
                        this.btnAddService.Enabled = true;
                        this.BindLatestTransaction();
                        this.ClearControl();
                        this.lblMsg.Visible = true;
                        this.lblMsg.Text = " Bill Saved successfully ! ";
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                        {
                            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            string docSpeciality = this._bill_Sys_BillTransaction.GetDocSpeciality(text);
                            this.GenerateAddedBillPDF(text, docSpeciality);
                        }
                        else
                        {
                            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            string str4 = this._bill_Sys_BillTransaction.GetDocSpeciality(text);
                            this.GenerateAddedBillPDF(text, str4);
                        }
                    }
                }
                else
                {
                    this.popupmsg.InnerText = "You do not have a claim number or an insurance company or an insurance company address added to case. Cannot proceed futher.";
                    this.Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage1();</script>");
                }
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindDiagnosisGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
            ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        try
        {
            if (this.Session["SZ_BILL_NUMBER"] != null)
            {
                this._editOperation.Primary_Value=this.Session["SZ_BILL_NUMBER"].ToString();
                this._editOperation.WebPage=this.Page;
                this._editOperation.Xml_File="BillTransaction.xml";
                this._editOperation.UpdateMethod();
                this.Session["SZ_BILL_NUMBER"] = null;
                base.Response.Redirect("Bill_Sys_BillSearch.aspx", false);
            }
            else
            {
                this._editOperation.Primary_Value=this.Session["BillID"].ToString();
                this._editOperation.WebPage=this.Page;
                this._editOperation.Xml_File="BillTransaction.xml";
                this._editOperation.UpdateMethod();
                this.lblMsg.Visible = true;
                this.lblMsg.Text = " Bill Updated successfully ! ";
            }
            this.BindLatestTransaction();
        }
        catch (SqlException)
        {
            this.ErrorDiv.InnerText = " Bill Number already exists";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        try
        {
            if (this.Session["SZ_BILL_NUMBER"] != null)
            {
                ArrayList list;
                this._editOperation.Primary_Value=this.Session["SZ_BILL_NUMBER"].ToString();
                this._editOperation.WebPage=this.Page;
                this._editOperation.Xml_File="ReffeBillTransaction.xml";
                this._editOperation.UpdateMethod();
                this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                string str = this.Session["SZ_BILL_NUMBER"].ToString();
                this.btnAddService.Enabled = true;
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    if (((!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;")) || (!(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;")))
                    {
                        continue;
                    }
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    list = new ArrayList();
                    if ((item.Cells[0].Text.ToString() != "") && (item.Cells[0].Text.ToString() != "&nbsp;"))
                    {
                        list.Add(item.Cells[0].Text.ToString());
                    }
                    list.Add(item.Cells[2].Text.ToString());
                    if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        string szamt = ((TextBox)item.Cells[7].FindControl("txtAmt")).Text.ToString();
                        if (szamt != "&nbsp;")
                        {
                            list.Add(szamt);
                        }
                        else
                        {
                            list.Add(0);
                        }
                    }
                    else
                    {
                        if (item.Cells[6].Text.ToString() != "&nbsp;")
                        {
                            list.Add(item.Cells[6].Text.ToString());
                        }
                        else
                        {
                            list.Add(0);
                        }
                    }
                    list.Add(str);
                    list.Add(Convert.ToDateTime(item.Cells[1].Text.ToString()));
                    list.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                   // list.Add(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString());
                    list.Add(((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString());
                    list.Add(((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString());
                    list.Add(((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString());
                    //list.Add(item.Cells[9].Text.ToString());
                    //list.Add(item.Cells[8].Text.ToString());

                    list.Add(item.Cells[10].Text.ToString());
                    list.Add(item.Cells[9].Text.ToString());

                    if ((item.Cells[0].Text.ToString() != "") && (item.Cells[0].Text.ToString() != "&nbsp;"))
                    {
                        list.Add(item.Cells[12].Text.ToString());
                        this._bill_Sys_BillTransaction.UpdateTransactionData(list);
                    }
                    else
                    {
                        list.Add(this.extddlDoctor.Text);
                        list.Add(this.txtCaseID.Text);
                        //list.Add(item.Cells[12].Text.ToString());
                        list.Add(item.Cells[13].Text.ToString());
                        this._bill_Sys_BillTransaction.SaveTransactionData(list);
                    }
                }
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                this._bill_Sys_BillTransaction.DeleteTransactionDiagnosis(str);
                foreach (ListItem item2 in this.lstDiagnosisCodes.Items)
                {
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    list = new ArrayList();
                    list.Add(item2.Value.ToString());
                    list.Add(str);
                    this._bill_Sys_BillTransaction.SaveTransactionDiagnosis(list);
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_UPDATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=str;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID=this.txtCompanyID.Text;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                this.lblMsg.Visible = true;
                this.lblMsg.Text = " Bill Updated successfully ! ";
            }
            if (this.Session["BillID"] == null)
            {
                this.BindTransactionDetailsGrid(this.Session["SZ_BILL_NUMBER"].ToString());
            }
            else
            {
                this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
            }
            this.BindLatestTransaction();
        }
        catch (SqlException)
        {
            this.ErrorDiv.InnerText = " Bill Number already exists";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdateService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.UpdateTransactionDetails();
            this.BindLatestTransaction();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void CalculateAmount(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {
            this._bill_Sys_Menu.GetICcodeAmount(id);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtDateOfservice.Text = "";
            this.txtDateOfServiceTo.Text = "";
            this.btnAddService.Enabled = true;
            this.extddlDoctor.Text="NA";
            this.extddlReadingDoctor.Text="NA";
            this.txtDateOfservice.Text = "";
            this.txtDateOfServiceTo.Text = "";
            this.grdTransactionDetails.DataSource = null;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                grdTransactionDetails.Columns[6].Visible = false;
                grdTransactionDetails.Columns[7].Visible = true;
            }
            else
            {
                grdTransactionDetails.Columns[6].Visible = true;
                grdTransactionDetails.Columns[7].Visible = false;
            }
            this.Session["SELECTED_DIA_PRO_CODE"] = null;
            this.Session["SZ_BILL_NUMBER"] = null;
            this.Session["SE_DIAGNOSIS_CODE"] = null;
            this.Session["SELECTED_DIA_PRO_CODE"] = null;
            this.lstDiagnosisCodes.Items.Clear();
            this.lstDiagnosisCodes.DataSource = null;
            this.lstDiagnosisCodes.DataBind();
            this.lblMsg.Visible = false;
            this.btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void Createtable()
    {
        new ArrayList();
        DataTable table = new DataTable();
        table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
        table.Columns.Add("DT_DATE_OF_SERVICE");
        table.Columns.Add("SZ_PROCEDURE_ID");
        table.Columns.Add("SZ_PROCEDURAL_CODE");
        table.Columns.Add("SZ_CODE_DESCRIPTION");
        table.Columns.Add("FACTOR_AMOUNT");
        table.Columns.Add("FLT_AMOUNT");
        table.Columns.Add("FACTOR");
        table.Columns.Add("PROC_AMOUNT");
        table.Columns.Add("DOCT_AMOUNT");
        table.Columns.Add("I_UNIT");
        foreach (DataGridItem item in this.grdTransactionDetails.Items)
        {
            DataRow row = table.NewRow();
            if ((item.Cells[0].Text.ToString() != "&nbsp;") && (item.Cells[0].Text.ToString() != ""))
            {
                row["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
            }
            else
            {
                row["SZ_BILL_TXN_DETAIL_ID"] = "";
            }
            row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
            row["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
            row["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
            row["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
            if (Convert.ToDecimal(((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0M)
            {
                row["FACTOR_AMOUNT"] = ((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString();
            }
            if (Convert.ToDecimal(((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0M)
            {
                row["FACTOR"] = ((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString();
            }
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                row["FLT_AMOUNT"] = ((TextBox)item.Cells[7].FindControl("txtAmt")).Text.ToString();
            }
            else
            {
                row["FLT_AMOUNT"] = item.Cells[6].Text.ToString();
            }
            //if ((((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0))
            //{
            //    row["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
            //}
            if ((((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString()) > 0))
            {
                row["I_UNIT"] = ((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString();
            }
            //row["PROC_AMOUNT"] = item.Cells[9].Text.ToString();
            //row["DOCT_AMOUNT"] = item.Cells[10].Text.ToString();
            row["PROC_AMOUNT"] = item.Cells[10].Text.ToString();
            row["DOCT_AMOUNT"] = item.Cells[11].Text.ToString();
            table.Rows.Add(row);
        }
        this.Session["SELECTED_SERVICES"] = table;
    }

    protected void extddlIC9Code_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_Menu = new Bill_Sys_Menu();
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            this.btnSave.Attributes.Add("onclick", "return Amountvalidate();");
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void extddlProcedureCode_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    }

    protected void extddlspeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.Session["ProcedureCoes"] = null;
        try
        {
            DataSet set = new DataSet();
            set = new Bill_Sys_BillTransaction_BO().GetAssociateDiagCode(this.extddlspeciality.Text, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.txtCompanyID.Text);
            this.lstDiagnosisCodes.DataSource = set.Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            set = new DataSet();
            set = new Bill_Sys_BillTransaction_BO().GetProcedureCodes(this.extddlspeciality.Text ,((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID, this.txtCompanyID.Text, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            DataTable table = new DataTable();
            table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            table.Columns.Add("DT_DATE_OF_SERVICE");
            table.Columns.Add("SZ_PROCEDURE_ID");
            table.Columns.Add("SZ_PROCEDURAL_CODE");
            table.Columns.Add("SZ_CODE_DESCRIPTION");
            table.Columns.Add("FLT_AMOUNT");
            table.Columns.Add("FACTOR_AMOUNT");
            table.Columns.Add("FACTOR");
            table.Columns.Add("PROC_AMOUNT");
            table.Columns.Add("DOCT_AMOUNT");
            table.Columns.Add("I_UNIT");
            table.Columns.Add("SZ_TYPE_CODE_ID");
            table.Columns.Add("SZ_PATIENT_TREATMENT_ID");
            table.Columns.Add("SZ_STUDY_NUMBER");
            foreach (DataRow row2 in set.Tables[0].Rows)
            {
                DataRow row = table.NewRow();
                row["SZ_BILL_TXN_DETAIL_ID"] = "";
                row["DT_DATE_OF_SERVICE"] = row2["DT_DATE_OF_SERVICE"];
                row["SZ_PROCEDURE_ID"] = row2["SZ_PROCEDURE_ID"];
                row["SZ_PROCEDURAL_CODE"] = row2["SZ_PROCEDURE_CODE"];
                row["SZ_CODE_DESCRIPTION"] = row2["SZ_CODE_DESCRIPTION"];
                if (row2["DOCTOR_AMOUNT"].ToString() == "0")
                {
                    row["FACTOR_AMOUNT"] = row2["FLT_AMOUNT"];
                    row["FLT_AMOUNT"] = Convert.ToDecimal(row2["FLT_AMOUNT"].ToString()) * Convert.ToDecimal(row2["FLT_KOEL"].ToString());
                }
                else
                {
                    row["FACTOR_AMOUNT"] = row2["DOCTOR_AMOUNT"];
                    row["FLT_AMOUNT"] = Convert.ToDecimal(row2["DOCTOR_AMOUNT"].ToString()) * Convert.ToDecimal(row2["FLT_KOEL"].ToString());
                }
                row["FACTOR"] = row2["FLT_KOEL"];
                row["PROC_AMOUNT"] = row2["FLT_AMOUNT"];
                row["DOCT_AMOUNT"] = row2["DOCTOR_AMOUNT"];
                row["I_UNIT"] = "";
                row["SZ_TYPE_CODE_ID"] = row2["SZ_TYPE_CODE_ID"];
                row["SZ_PATIENT_TREATMENT_ID"] = row2["I_EVENT_PROCID"];
                row["SZ_STUDY_NUMBER"] = row2["SZ_STUDY_NUMBER"];
                table.Rows.Add(row);
            }
            this.Session["ProcedureCoes"] = set;
            this.grdTransactionDetails.DataSource = table;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                grdTransactionDetails.Columns[6].Visible = false;
                grdTransactionDetails.Columns[7].Visible = true;
            }
            else
            {
                grdTransactionDetails.Columns[6].Visible = true;
                grdTransactionDetails.Columns[7].Visible = false;
            }
            Bill_Sys_BillTransaction_BO n_bo2 = new Bill_Sys_BillTransaction_BO();
            //if (this.extddlspeciality.Text != "NA")
            //{
            //    if (n_bo2.checkUnitRefferal(this.extddlspeciality.Text, this.txtCompanyID.Text))
            //    {
            //        this.grdTransactionDetails.Columns[7].Visible = true;
            //    }
            //    else
            //    {
            //        this.grdTransactionDetails.Columns[7].Visible = false;
            //    }
            //}

            if (this.extddlspeciality.Text != "NA")
            {
                if (n_bo2.checkUnitRefferal(this.extddlspeciality.Text, this.txtCompanyID.Text))
                {
                   // this.grdTransactionDetails.Columns[8].Visible = true;
                    this.grdTransactionDetails.Columns[9].Visible = true;
                }
                else
                {
                  //  this.grdTransactionDetails.Columns[8].Visible = false;
                    this.grdTransactionDetails.Columns[9].Visible = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._MUVGenerateFunction = new MUVGenerateFunction();
            string str = p_szSpeciality;
            this.hdnSpeciality.Value = p_szSpeciality;
            this.Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this.Session["TM_SZ_BILL_ID"] = p_szBillNumber;
            this.objNF3Template = new Bill_Sys_NF3_Template();
            CaseDetailsBO sbo = new CaseDetailsBO();
            string str2 = "";
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
            {
                str2 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            }
            else
            {
                str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            this.objVerification_Desc = new Bill_Sys_Verification_Desc();
            this.objVerification_Desc.sz_bill_no=p_szBillNumber;
            this.objVerification_Desc.sz_company_id=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.objVerification_Desc.sz_flag="BILL";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            string str3 = "";
            string str4 = "";
            list.Add(this.objVerification_Desc);
            list2 = this._bill_Sys_BillTransaction.Get_Node_Type(list);
            if (list2.Contains("NFVER"))
            {
                str3 = "OLD";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
                    str4 = template.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
                }
                else
                {
                    str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
                }
            }
            else
            {
                str3 = "NEW";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template2 = new Bill_Sys_NF3_Template();
                    str4 = template2.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + str + "/Bills/";
                }
                else
                {
                    str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + str + "/Bills/";
                }
            }
            if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                string str5 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template3 = new Bill_Sys_NF3_Template();
                    str5 = template3.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    str5 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                string str6 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template4 = new Bill_Sys_NF3_Template();
                    str6 = template4.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    str6 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                string str7 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                string str8 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                string str9 = "";
                Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
                string str14 = configuration.getConfigurationSettings(str2, "GET_DIAG_PAGE_POSITION");
                string str15 = configuration.getConfigurationSettings(str2, "DIAG_PAGE");
                string str10 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                string str11 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                string str12 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                string str13 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                GenerateNF3PDF enfpdf = new GenerateNF3PDF();
                this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                string str16 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template5 = new Bill_Sys_NF3_Template();
                    str16 = enfpdf.GeneratePDF(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, template5.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str7);
                }
                else
                {
                    str16 = enfpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str7);
                }
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template6 = new Bill_Sys_NF3_Template();
                    str9 = this.objPDFReplacement.ReplacePDFvalues(str10, str11, this.Session["TM_SZ_BILL_ID"].ToString(), template6.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    str9 = this.objPDFReplacement.ReplacePDFvalues(str10, str11, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                }
                string str17 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template7 = new Bill_Sys_NF3_Template();
                    str17 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, template7.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str9, str16);
                }
                else
                {
                    str17 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str9, str16);
                }
                string str18 = "";
                string str19 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template8 = new Bill_Sys_NF3_Template();
                    str19 = this.objPDFReplacement.ReplacePDFvalues(str12, str13, this.Session["TM_SZ_BILL_ID"].ToString(), template8.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    str19 = this.objPDFReplacement.ReplacePDFvalues(str12, str13, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                }
                string str21 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.bt_include = this._MUVGenerateFunction.get_bt_include(str21, str, "", "Speciality");
                string str20 = this._MUVGenerateFunction.get_bt_include(str21, "", "WC000000000000000002", "CaseType");
                if ((this.bt_include == "True") && (str20 == "True"))
                {
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                }
                MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str5 + str17, this.objNF3Template.getPhysicalPath() + str5 + str19, this.objNF3Template.getPhysicalPath() + str5 + str19.Replace(".pdf", "_MER.pdf"));
                str18 = str19.Replace(".pdf", "_MER.pdf");
                if ((this.bt_include == "True") && (str20 == "True"))
                {
                    MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str5 + str18, this.objNF3Template.getPhysicalPath() + str5 + this.str_1500, this.objNF3Template.getPhysicalPath() + str5 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                    str18 = this.str_1500.Replace(".pdf", "_MER.pdf");
                }
                string str22 = "";
                str22 = str5 + str18;
                string str23 = "";
                str23 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str22;
                string path = this.objNF3Template.getPhysicalPath() + "/" + str22;
                CutePDFDocumentClass class2 = new CutePDFDocumentClass();
                string str25 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                class2.initialize(str25);
                if ((((class2 != null) && File.Exists(path)) && ((str15 != "CI_0000003") && (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5))) && ((str14 == "CK_0000003") && ((str15 != "CI_0000004") || (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))))
                {
                    str16 = path.Replace(".pdf", "_NewMerge.pdf");
                }
                string str26 = "";
                if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str22 = path.Replace(".pdf", "_New.pdf").ToString();
                }
                if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_NewMerge.pdf").ToString()))
                {
                    str26 = str23.Replace(".pdf", "_NewMerge.pdf").ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str23.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                }
                else if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str26 = str23.Replace(".pdf", "_New.pdf").ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str23.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                }
                else
                {
                    str26 = str23.ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str23.ToString() + "'); ", true);
                }
                string str27 = "";
                string[] strArray = str26.Split(new char[] { '/' });
                ArrayList list3 = new ArrayList();
                str26 = str26.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                str27 = strArray[strArray.Length - 1].ToString();
                if (File.Exists(this.objNF3Template.getPhysicalPath() + str6 + str27))
                {
                    if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str4))
                    {
                        Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str4);
                    }
                    File.Copy(this.objNF3Template.getPhysicalPath() + str6 + str27, this.objNF3Template.getPhysicalPath() + str4 + str27);
                }
                if (str3 == "OLD")
                {
                    list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                    list3.Add(str4 + str27);
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    }
                    else
                    {
                        list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                    list3.Add(this.Session["TM_SZ_CASE_ID"]);
                    list3.Add(strArray[strArray.Length - 1].ToString());
                    list3.Add(str4);
                    list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    list3.Add(str);
                    list3.Add("NF");
                    list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    this.objNF3Template.saveGeneratedBillPath(list3);
                }
                else
                {
                    list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                    list3.Add(str4 + str27);
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    }
                    else
                    {
                        list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                    list3.Add(this.Session["TM_SZ_CASE_ID"]);
                    list3.Add(strArray[strArray.Length - 1].ToString());
                    list3.Add(str4);
                    list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    list3.Add(str);
                    list3.Add("NF");
                    list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    list3.Add(list2[0].ToString());
                    this.objNF3Template.saveGeneratedBillPath_New(list3);
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_GENERATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=str27;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                this.BindLatestTransaction();
            }
            else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
            {
                string str29;
                string str34;
                string str28 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template9 = new Bill_Sys_NF3_Template();
                    str28 = template9.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    str28 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    new Bill_Sys_NF3_Template();
                }
                new Bill_Sys_PVT_Template();
               // ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str30 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str31 = this.Session["TM_SZ_BILL_ID"].ToString();
                string str32 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string str33 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    str29 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                else
                {
                   // ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    str29 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                Lien lien = new Lien();
                string str36 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.bt_include = this._MUVGenerateFunction.get_bt_include(str36, str, "", "Speciality");
                string str35 = this._MUVGenerateFunction.get_bt_include(str36, "", "WC000000000000000004", "CaseType");
                if ((this.bt_include == "True") && (str35 == "True"))
                {
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                    MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str4 + lien.GenratePdfForLienWithMuv(str29, str31, str, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str32, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str33), this.objNF3Template.getPhysicalPath() + str28 + this.str_1500, this.objNF3Template.getPhysicalPath() + str4 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                    str34 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str4 + this.str_1500.Replace(".pdf", "_MER.pdf");
                    ArrayList list4 = new ArrayList();
                    if (str3 == "OLD")
                    {
                        list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list4.Add(str4 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list4.Add(this.Session["TM_SZ_CASE_ID"]);
                        list4.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list4.Add(str4);
                        list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list4.Add(str);
                        list4.Add("NF");
                        list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(list4);
                    }
                    else
                    {
                        list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list4.Add(str4 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list4.Add(this.Session["TM_SZ_CASE_ID"]);
                        list4.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list4.Add(str4);
                        list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list4.Add(str);
                        list4.Add("NF");
                        list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        list4.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list4);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=this.str_1500;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                }
                else
                {
                    str34 = lien.GenratePdfForLien(str29, str31, str, str30, str32, this.txtCaseNo.Text, str33);
                }
                ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + str34 + "');", true);
            }
            else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
            {
                string str38;
                string companyName;
                Bill_Sys_PVT_Template template11 = new Bill_Sys_PVT_Template();
                bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str39 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str41 = this.Session["TM_SZ_BILL_ID"].ToString();
                string str42 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string str43 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    companyName = new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    str38 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    companyName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    str38 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + template11.GeneratePVTBill(flag, str38, str39, str, companyName, str41, str42, str43) + "'); ", true);
            }
            else
            {
                string str44 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                string str45 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string str46 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
             //   ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
               // ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                WC_Bill_Generation generation = new WC_Bill_Generation();
                this.hdnWCPDFBillNumber.Value = (string)this.Session["TM_SZ_BILL_ID"];
                ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + generation.GeneratePDFForReferalWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str2, str46, str44, str45, this.hdnSpeciality.Value.ToString()) + "');", true);
            }
            new Bill_Sys_BillTransaction_BO();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetDoctorDiagnosisCode("", this.txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE").Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO tbo = new Bill_Sys_PatientBO();
        try
        {
            this.grdPatientDeskList.DataSource = tbo.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            this.grdPatientDeskList.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetProcedureCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            table = this._bill_Sys_BillTransaction.GetPatientDoctor_List(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID, this.txtCompanyID.Text).Tables[0];
            foreach (DataRow row in table.Rows)
            {
                table2 = this._bill_Sys_BillTransaction.GetDoctorProcedureCodeList(row["SZ_DOCTOR_ID"].ToString(), "TY000000000000000003", this.txtCaseID.Text).Tables[0];
                try
                {
                    DataTable table3 = new DataTable();
                    table3.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
                    table3.Columns.Add("DT_DATE_OF_SERVICE");
                    table3.Columns.Add("SZ_PROCEDURE_ID");
                    table3.Columns.Add("SZ_PROCEDURAL_CODE");
                    table3.Columns.Add("SZ_CODE_DESCRIPTION");
                    table3.Columns.Add("FLT_AMOUNT");
                    table3.Columns.Add("FACTOR_AMOUNT");
                    table3.Columns.Add("FACTOR");
                    table3.Columns.Add("PROC_AMOUNT");
                    table3.Columns.Add("DOCT_AMOUNT");
                    table3.Columns.Add("I_UNIT");
                    table3.Columns.Add("SZ_TYPE_CODE_ID");
                    table3.Columns.Add("SZ_PATIENT_TREATMENT_ID");
                    string str = "";
                    string str2 = "";
                    string str3 = "";
                    foreach (DataRow row3 in table2.Rows)
                    {
                        str = row3["CODE"].ToString().Substring(0, row3["CODE"].ToString().IndexOf("|"));
                        str2 = row3["CODE"].ToString().Substring(row3["CODE"].ToString().IndexOf("|") + 1, row3["CODE"].ToString().Length - (row3["CODE"].ToString().IndexOf("|") + 1));
                        str3 = row3["SZ_PATIENT_TREATMENT_ID"].ToString();
                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        DataTable table4 = new DataTable();
                        table4 = this._bill_Sys_BillTransaction.GeSelectedDoctorProcedureCode_List(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, row["SZ_DOCTOR_ID"].ToString(), str2).Tables[0];
                        foreach (DataRow row4 in table4.Rows)
                        {
                            DataRow row2 = table3.NewRow();
                            row2["SZ_BILL_TXN_DETAIL_ID"] = "";
                            string str4 = row3["DESCRIPTION"].ToString().Substring(row3["DESCRIPTION"].ToString().Substring(0, row3["DESCRIPTION"].ToString().IndexOf("--")).Length + 2, row3["DESCRIPTION"].ToString().Length - (row3["DESCRIPTION"].ToString().Substring(0, row3["DESCRIPTION"].ToString().IndexOf("--")).Length + 2));
                            row2["DT_DATE_OF_SERVICE"] = str4.Substring(0, str4.IndexOf("--"));
                            row2["SZ_PROCEDURE_ID"] = row4["SZ_PROCEDURE_ID"];
                            row2["SZ_PROCEDURAL_CODE"] = row4["SZ_PROCEDURE_CODE"];
                            row2["SZ_CODE_DESCRIPTION"] = row4["SZ_CODE_DESCRIPTION"];
                            if (row4["DOCTOR_AMOUNT"].ToString() == "0")
                            {
                                row2["FACTOR_AMOUNT"] = row4["FLT_AMOUNT"];
                                row2["FLT_AMOUNT"] = Convert.ToDecimal(row4["FLT_AMOUNT"].ToString()) * Convert.ToDecimal(row4["FLT_KOEL"].ToString());
                            }
                            else
                            {
                                row2["FACTOR_AMOUNT"] = row4["DOCTOR_AMOUNT"];
                                row2["FLT_AMOUNT"] = Convert.ToDecimal(row4["DOCTOR_AMOUNT"].ToString()) * Convert.ToDecimal(row4["FLT_KOEL"].ToString());
                            }
                            row2["FACTOR"] = row4["FLT_KOEL"];
                            row2["PROC_AMOUNT"] = row4["FLT_AMOUNT"];
                            row2["DOCT_AMOUNT"] = row4["DOCTOR_AMOUNT"];
                            row2["I_UNIT"] = "";
                            row2["SZ_TYPE_CODE_ID"] = str2;
                            row2["SZ_PATIENT_TREATMENT_ID"] = str3;
                            table3.Rows.Add(row2);
                        }
                    }
                    this.grdTransactionDetails.DataSource = table3;
                    this.grdTransactionDetails.DataBind();
                    if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        grdTransactionDetails.Columns[6].Visible = false;
                        grdTransactionDetails.Columns[7].Visible = true;
                    }
                    else
                    {
                        grdTransactionDetails.Columns[6].Visible = true;
                        grdTransactionDetails.Columns[7].Visible = false;
                    }
                    continue;
                }
                catch
                {
                    continue;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            if (this.extddlDiagnosisType.Text == "NA")
            {
                this.BindDiagnosisGrid("");
            }
            else
            {
                this.BindDiagnosisGrid(this.extddlDiagnosisType.Text);
            }
            ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdLatestBillTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Add IC9 Code")
            {
                this.Session["PassedBillID"] = e.CommandArgument;
                base.Response.Redirect("Bill_Sys_BillIC9Code.aspx", false);
            }
            if (e.CommandName.ToString() == "Generate bill")
            {
                this._MUVGenerateFunction = new MUVGenerateFunction();
                this.objNF3Template = new Bill_Sys_NF3_Template();
                string str = this.objNF3Template.getGroup(e.Item.Cells[1].Text);
                this.Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                this.Session["TM_SZ_BILL_ID"] = e.Item.Cells[1].Text;
                CaseDetailsBO sbo = new CaseDetailsBO();
                string str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.objVerification_Desc = new Bill_Sys_Verification_Desc();
                this.objVerification_Desc.sz_bill_no=this.Session["TM_SZ_BILL_ID"].ToString();
                this.objVerification_Desc.sz_company_id=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.objVerification_Desc.sz_flag="BILL";
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                string str3 = "";
                string str4 = "";
                list.Add(this.objVerification_Desc);
                list2 = new Bill_Sys_BillTransaction_BO().Get_Node_Type(list);
                if (list2.Contains("NFVER"))
                {
                    str3 = "OLD";
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
                        str4 = template.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
                    }
                    else
                    {
                        str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
                    }
                }
                else
                {
                    str3 = "NEW";
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        Bill_Sys_NF3_Template template2 = new Bill_Sys_NF3_Template();
                        str4 = template2.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + str + "/Bills/";
                    }
                    else
                    {
                        str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + str + "/Bills/";
                    }
                }
                if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
                {
                    string str5 = "";
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        Bill_Sys_NF3_Template template3 = new Bill_Sys_NF3_Template();
                        str5 = template3.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    }
                    else
                    {
                        str5 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    }
                    string str6 = "";
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        Bill_Sys_NF3_Template template4 = new Bill_Sys_NF3_Template();
                        str6 = template4.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    }
                    else
                    {
                        str6 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    }
                    string str7 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                    ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                    string str8 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                    string str9 = "";
                    Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
                    string str14 = configuration.getConfigurationSettings(str2, "GET_DIAG_PAGE_POSITION");
                    string str15 = configuration.getConfigurationSettings(str2, "DIAG_PAGE");
                    string str10 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                    string str11 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                    string str12 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                    string str13 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                    GenerateNF3PDF enfpdf = new GenerateNF3PDF();
                    this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string str16 = "";
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        Bill_Sys_NF3_Template template5 = new Bill_Sys_NF3_Template();
                        str16 = enfpdf.GeneratePDF(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, template5.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str7);
                    }
                    else
                    {
                        str16 = enfpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str7);
                    }
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        Bill_Sys_NF3_Template template6 = new Bill_Sys_NF3_Template();
                        str9 = this.objPDFReplacement.ReplacePDFvalues(str10, str11, this.Session["TM_SZ_BILL_ID"].ToString(), template6.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                    }
                    else
                    {
                        str9 = this.objPDFReplacement.ReplacePDFvalues(str10, str11, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    }
                    string str17 = "";
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        Bill_Sys_NF3_Template template7 = new Bill_Sys_NF3_Template();
                        str17 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, template7.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str9, str16);
                    }
                    else
                    {
                        str17 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str9, str16);
                    }
                    string str18 = "";
                    string str19 = "";
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        Bill_Sys_NF3_Template template8 = new Bill_Sys_NF3_Template();
                        str19 = this.objPDFReplacement.ReplacePDFvalues(str12, str13, this.Session["TM_SZ_BILL_ID"].ToString(), template8.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                    }
                    else
                    {
                        str19 = this.objPDFReplacement.ReplacePDFvalues(str12, str13, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    }
                    string text = e.Item.Cells[3].Text;
                    string str21 = e.Item.Cells[0x10].Text;
                    string str22 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(str22, str21, "", "Speciality");
                    string str20 = this._MUVGenerateFunction.get_bt_include(str22, "", "WC000000000000000002", "CaseType");
                    if ((this.bt_include == "True") && (str20 == "True"))
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                    }
                    MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str5 + str17, this.objNF3Template.getPhysicalPath() + str5 + str19, this.objNF3Template.getPhysicalPath() + str5 + str19.Replace(".pdf", "_MER.pdf"));
                    str18 = str19.Replace(".pdf", "_MER.pdf");
                    if ((this.bt_include == "True") && (str20 == "True"))
                    {
                        MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str5 + str18, this.objNF3Template.getPhysicalPath() + str5 + this.str_1500, this.objNF3Template.getPhysicalPath() + str5 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        str18 = this.str_1500.Replace(".pdf", "_MER.pdf");
                    }
                    string str23 = "";
                    str23 = str5 + str18;
                    string str24 = "";
                    str24 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str23;
                    string path = this.objNF3Template.getPhysicalPath() + "/" + str23;
                    CutePDFDocumentClass class2 = new CutePDFDocumentClass();
                    string str26 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                    class2.initialize(str26);
                    if ((((class2 != null) && File.Exists(path)) && ((str15 != "CI_0000003") && (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5))) && ((str14 == "CK_0000003") && ((str15 != "CI_0000004") || (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))))
                    {
                        str16 = path.Replace(".pdf", "_NewMerge.pdf");
                    }
                    string str27 = "";
                    if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str23 = path.Replace(".pdf", "_New.pdf").ToString();
                    }
                    if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_NewMerge.pdf").ToString()))
                    {
                        str27 = str24.Replace(".pdf", "_NewMerge.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                    }
                    else if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str27 = str24.Replace(".pdf", "_New.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                    }
                    else
                    {
                        str27 = str24.ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.ToString() + "'); ", true);
                    }
                    string str28 = "";
                    string[] strArray = str27.Split(new char[] { '/' });
                    ArrayList list3 = new ArrayList();
                    str27 = str27.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                    str28 = strArray[strArray.Length - 1].ToString();
                    if (File.Exists(this.objNF3Template.getPhysicalPath() + str6 + str28))
                    {
                        if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str4))
                        {
                            Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str4);
                        }
                        File.Copy(this.objNF3Template.getPhysicalPath() + str6 + str28, this.objNF3Template.getPhysicalPath() + str4 + str28);
                    }
                    if (str3 == "OLD")
                    {
                        list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list3.Add(str4 + str28);
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                        {
                            list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        }
                        else
                        {
                            list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        }
                        list3.Add(this.Session["TM_SZ_CASE_ID"]);
                        list3.Add(strArray[strArray.Length - 1].ToString());
                        list3.Add(str4);
                        list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list3.Add(str);
                        list3.Add("NF");
                        list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(list3);
                    }
                    else
                    {
                        list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list3.Add(str4 + str28);
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                        {
                            list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        }
                        else
                        {
                            list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        }
                        list3.Add(this.Session["TM_SZ_CASE_ID"]);
                        list3.Add(strArray[strArray.Length - 1].ToString());
                        list3.Add(str4);
                        list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list3.Add(str);
                        list3.Add("NF");
                        list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        list3.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list3);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=str28;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                    {
                        this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                }
                else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
                {
                    string str29;
                    string companyName;
                    Bill_Sys_PVT_Template template9 = new Bill_Sys_PVT_Template();
                    bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                    string str30 = this.Session["TM_SZ_CASE_ID"].ToString();
                    string str32 = this.Session["TM_SZ_BILL_ID"].ToString();
                    string str33 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string str34 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        companyName = new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        str29 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                        companyName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        str29 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    companyName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + template9.GeneratePVTBill(flag, str29, str30, str, companyName, str32, str33, str34) + "'); ", true);
                }
                else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
                {
                    string str36;
                    string str41;
                    string str35 = "";
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        Bill_Sys_NF3_Template template11 = new Bill_Sys_NF3_Template();
                        str35 = template11.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    }
                    else
                    {
                        str35 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    }
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        new Bill_Sys_NF3_Template();
                    }
                    new Bill_Sys_PVT_Template();
                   // ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                    string str37 = this.Session["TM_SZ_CASE_ID"].ToString();
                    string str38 = this.Session["TM_SZ_BILL_ID"].ToString();
                    string str39 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string str40 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        str36 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                       // ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        str36 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    Lien lien = new Lien();
                    string str43 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(str43, str, "", "Speciality");
                    string str42 = this._MUVGenerateFunction.get_bt_include(str43, "", "WC000000000000000004", "CaseType");
                    if ((this.bt_include == "True") && (str42 == "True"))
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                        MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str4 + lien.GenratePdfForLienWithMuv(str36, str38, str, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str39, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str40), this.objNF3Template.getPhysicalPath() + str35 + this.str_1500, this.objNF3Template.getPhysicalPath() + str4 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        str41 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str4 + this.str_1500.Replace(".pdf", "_MER.pdf");
                        ArrayList list4 = new ArrayList();
                        if (str3 == "OLD")
                        {
                            list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list4.Add(str4 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list4.Add(this.Session["TM_SZ_CASE_ID"]);
                            list4.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list4.Add(str4);
                            list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list4.Add(str);
                            list4.Add("NF");
                            list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(list4);
                        }
                        else
                        {
                            list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list4.Add(str4 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list4.Add(this.Session["TM_SZ_CASE_ID"]);
                            list4.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list4.Add(str4);
                            list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list4.Add(str);
                            list4.Add("NF");
                            list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            list4.Add(list2[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(list4);
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=this.str_1500;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                    else
                    {
                        str41 = lien.GenratePdfForLien(str36, str38, str, str37, str39, this.txtCaseNo.Text, str40);
                    }
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + str41 + "');", true);
                }
                else
                {
                    string str45 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    string str46 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string str47 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                  //  ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                  //  ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                    string str48 = new WC_Bill_Generation().GeneratePDFForReferalWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str2, str47, str45, str46, this.hdnSpeciality.Value.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + str48 + "');", true);
                }
            }
            if (e.CommandName.ToString() == "Doctor's Initial Report")
            {
                this.Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformation.aspx'); ", true);
            }
            if (e.CommandName.ToString() == "Doctor's Progress Report")
            {
                this.Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_2.aspx'); ", true);
            }
            if (e.CommandName.ToString() == "Doctor's Report Of MMI")
            {
                this.Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_3.aspx'); ", true);
            }
            if (e.CommandName.ToString() == "Make Payment")
            {
                if (e.Item.Cells[9].Text != "1")
                {
                    this.Session["PassedBillID"] = e.CommandArgument;
                    this.Session["Balance"] = e.Item.Cells[7].Text;
                    base.Response.Redirect("Bill_Sys_PaymentTransactions.aspx", false);
                }
                else
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Please first add services to bill!'); ", true);
                }
            }
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdLatestBillTransaction_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.Item.Cells[9].Text == "1")
            {
                e.Item.Cells[8].Text = "";
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            if (this.objCaseDetailsBO.GetCaseType(e.Item.Cells[1].Text) != "WC000000000000000001")
            {
                e.Item.Cells[11].Text = "";
                e.Item.Cells[12].Text = "";
                e.Item.Cells[13].Text = "";
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdLatestBillTransaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["BillID"] = this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[1].Text;
            this.Session["SZ_BILL_NUMBER"] = this.Session["BillID"].ToString();
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            if (this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[10].Text != "&nbsp;")
            {
                this.extddlDoctor.Text=this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[10].Text;
            }
            if (this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[15].Text != "&nbsp;")
            {
                this.extddlReadingDoctor.Text=this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[15].Text;
            }
            if (this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[0x11].Text != "&nbsp;")
            {
                this.extddlspeciality.Text=this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[0x11].Text;
            }
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetBillDiagnosisCode(this.Session["BillID"].ToString()).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.btnAddService.Enabled = true;
            this.grdTransactionDetails.Visible = true;
            this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            if (this.extddlspeciality.Text != "NA")
            {
                if (n_bo.checkUnitRefferal(this.extddlspeciality.Text, this.txtCompanyID.Text))
                {
                   // this.grdTransactionDetails.Columns[7].Visible = true;
                    this.grdTransactionDetails.Columns[8].Visible = true;
                }
                else
                {
                  //  this.grdTransactionDetails.Columns[7].Visible = false;
                    this.grdTransactionDetails.Columns[8].Visible = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdTransactionDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdTransactionDetails.CurrentPageIndex = e.NewPageIndex;
            if (this.Session["SELECTED_DIA_PRO_CODE"] != null)
            {
                DataTable table = new DataTable();
                table = (DataTable)this.Session["SELECTED_DIA_PRO_CODE"];
                this.grdTransactionDetails.DataSource = table;
                this.grdTransactionDetails.DataBind();
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    grdTransactionDetails.Columns[6].Visible = false;
                    grdTransactionDetails.Columns[7].Visible = true;
                }
                else
                {
                    grdTransactionDetails.Columns[6].Visible = true;
                    grdTransactionDetails.Columns[7].Visible = false;
                }
            }
            else
            {
                this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdTransactionDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtTransDetailID.Text = this.grdTransactionDetails.Items[this.grdTransactionDetails.SelectedIndex].Cells[1].Text;
            this.txtDateOfservice.Text = this.grdTransactionDetails.Items[this.grdTransactionDetails.SelectedIndex].Cells[3].Text;
           // this.txtAmount.Text = this.grdTransactionDetails.Items[this.grdTransactionDetails.SelectedIndex].Cells[8].Text;
            this.txtAmount.Text = this.grdTransactionDetails.Items[this.grdTransactionDetails.SelectedIndex].Cells[9].Text;
            this.btnAddService.Enabled = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    

    protected void lnkbtnRemoveDiag_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i < this.lstDiagnosisCodes.Items.Count; i++)
            {
                if (this.lstDiagnosisCodes.Items[i].Selected)
                {
                    this.lstDiagnosisCodes.Items.Remove(this.lstDiagnosisCodes.Items[i]);
                    i--;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkShowGrid_Click(object sender, EventArgs e)
    {
    }

    protected void lnlInitialReport_Click(object sender, EventArgs e)
    {
        this.Session["TEMPLATE_BILL_NO"] = this.Session["BillID"].ToString();
        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformation.aspx'); ", true);
    }

    protected void lnlProgessReport_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["TEMPLATE_BILL_NO"] = this.Session["BillID"].ToString();
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_2.aspx'); ", true);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!base.IsPostBack && (base.Request.QueryString["Type"] == null))
            {
                this.Session["SZ_BILL_NUMBER"] = null;
            }
            this.ErrorDiv.InnerText = "";
            this.ErrorDiv.Style.Value = "color: red";
            if (this.Session["CASE_OBJECT"] != null)
            {
                this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this.txtCaseNo.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                Bill_Sys_Case @case = new Bill_Sys_Case();
                @case.SZ_CASE_ID=this.txtCaseID.Text;
                this.Session["CASEINFO"] = @case;
                string text = this.txtCaseID.Text;
                this.Session["QStrCaseID"] = text;
                this.Session["Case_ID"] = text;
                this.Session["Archived"] = "0";
                this.Session["QStrCID"] = text;
                this.Session["SelectedID"] = text;
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
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.extddlDoctor.Procedure_Name="SP_MST_BILLING_DOCTOR";
                this.extddlDoctor.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlReadingDoctor.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            else
            {
                this.extddlDoctor.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            this.btnAddService.Attributes.Add("onclick", "return formValidator('frmBillTrans','txtDateOfservice,txtDateOfServiceTo,extddlDoctor');");
            this.btnSave.Attributes.Add("onclick", "return FormValication();");
            this.btnUpdate.Attributes.Add("onclick", "return FormValication();");
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.txtReferringCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!base.IsPostBack)
            {
                this.extddlspeciality.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlDiagnosisType.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (this.Session["SZ_BILL_NUMBER"] != null)
                {
                    this.txtBillID.Text = this.Session["SZ_BILL_NUMBER"].ToString();
                    this.BindTransactionData(this.txtBillID.Text);
                    this.btnSave.Enabled = false;
                    this.btnUpdate.Enabled = true;
                }
                this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                this.Session["SELECTED_DIA_PRO_CODE"] = null;
                if (base.Request.QueryString["PopUp"] == null)
                {
                    this.Session["TEMP_DOCTOR_ID"] = null;
                    this.Session["SE_DIAGNOSIS_CODE"] = null;
                }
                this.BindLatestTransaction();
                int count = this.grdLatestBillTransaction.Items.Count;
                this.btnUpdate.Enabled = false;
                if (this.Session["SZ_BILL_NUMBER"] != null)
                {
                    this._editOperation = new EditOperation();
                    this._editOperation.Primary_Value=this.Session["SZ_BILL_NUMBER"].ToString();
                    this._editOperation.WebPage=this.Page;
                    this._editOperation.Xml_File="MRILatestBillTransaction.xml";
                    this._editOperation.LoadData();
                    this.txtBillDate.Text = string.Format("{0:MM/dd/yyyy}", this.txtBillDate.Text).ToString();
                    this.btnUpdate.Enabled = true;
                    this.btnSave.Enabled = false;
                    this.setDefaultValues(this.Session["SZ_BILL_NUMBER"].ToString());
                }
            }
            else if (this.Session["SELECTED_DIA_PRO_CODE"] != null)
            {
                DataTable table = new DataTable();
                table = (DataTable)this.Session["SELECTED_DIA_PRO_CODE"];
                this.grdTransactionDetails.DataSource = table;
                this.grdTransactionDetails.DataBind();
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    grdTransactionDetails.Columns[6].Visible = false;
                    grdTransactionDetails.Columns[7].Visible = true;
                }
                else
                {
                    grdTransactionDetails.Columns[6].Visible = true;
                    grdTransactionDetails.Columns[7].Visible = false;
                }
                this.Session["SELECTED_DIA_PRO_CODE"] = null;
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            foreach (DataGridItem item in this.grdLatestBillTransaction.Items)
            {
                if (this.objCaseDetailsBO.GetCaseType(item.Cells[1].Text) != "WC000000000000000001")
                {
                    item.Cells[11].Text = "";
                    item.Cells[12].Text = "";
                    item.Cells[13].Text = "";
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
       
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_ReferralBillTransaction.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.Session["SZ_BILL_ID"] = null;
        this.Session["PassedCaseID"] = null;
    }

    protected void rdoFromTo_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtDateOfServiceTo.Visible = true;
            this.lblDateOfService.Visible = true;
            this.lblTo.Visible = true;
            this.imgbtnFromTo.Visible = true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void rdoOn_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtDateOfServiceTo.Visible = false;
            this.lblDateOfService.Visible = false;
            this.lblTo.Visible = false;
            this.imgbtnFromTo.Visible = false;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SaveTransactionData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            if (this.txtDateOfServiceTo.Visible)
            {
                int days = Convert.ToDateTime(this.txtDateOfServiceTo.Text).Subtract(Convert.ToDateTime(this.txtDateOfservice.Text)).Days;
                DateTime time = Convert.ToDateTime(this.txtDateOfservice.Text).AddDays(1.0);
            }
            else
            {
                bool visible = this.txtDateOfServiceTo.Visible;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SetDataOfAssociate(string setID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataSet associatedEntry = new DataSet();
            associatedEntry = this._bill_Sys_BillTransaction.GetAssociatedEntry(setID);
            for (int i = 0; i <= (associatedEntry.Tables[0].Rows.Count - 1); i++)
            {
                this.extddlDoctor.Text=associatedEntry.Tables[0].Rows[i][1].ToString();
            }
            this.grdTransactionDetails.Visible = true;
            this.extddlDoctor.Enabled = false;
            this.btnAddService.Enabled = false;
            this.btnClearService.Enabled = false;
            this.Session["AssociateDiagnosis"] = true;
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void setDefaultSettings(string p_szBillID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["BillID"] = p_szBillID;
            this.Session["SZ_BILL_NUMBER"] = this.Session["BillID"].ToString();
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetBillDiagnosisCode(this.Session["BillID"].ToString()).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.grdTransactionDetails.Visible = true;
            this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void setDefaultValues(string p_szBillID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetBillDiagnosisCode(p_szBillID).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.btnAddService.Enabled = true;
            this.grdTransactionDetails.Visible = true;
            this.BindTransactionDetailsGrid(p_szBillID);
            ArrayList billInfo = new ArrayList();
            billInfo = this._bill_Sys_BillTransaction.GetBillInfo(p_szBillID);
            if (billInfo != null)
            {
                this.extddlDoctor.Text=billInfo[0].ToString();
                this.extddlReadingDoctor.Text=billInfo[1].ToString();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void UpdateTransactionDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            new ArrayList();
        }
        catch (Exception exception)
        {
            string str = exception.Message.ToString().Replace("\n", " ");
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str);
        }
    }

    protected void btnAddDGCodes_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            string StrDGCodes = (hndSeletedDGCodes.Value.Replace("--,", "*")).Replace("--", "");
            string[] ArrCodes = StrDGCodes.Split('*');

            for (int i = 0; i < ArrCodes.Length; i++)
            {
                string[] arrInsert = ArrCodes[i].Split('~');
                ListItem item = new ListItem(arrInsert[1], arrInsert[2]);
                if (!this.lstDiagnosisCodes.Items.Contains(item))
                {
                    this.lstDiagnosisCodes.Items.Add(item);
                }

            }
            //this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
         
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

 
}

