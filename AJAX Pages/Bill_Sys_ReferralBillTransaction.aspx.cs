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
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
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
using Elmah;

public partial class Bill_Sys_ReferralBillTransaction : PageBase, IRequiresSessionState
{   
    private SaveOperation _saveOperation;

    private EditOperation _editOperation;

    private ListOperation _listOperation;

    private Bill_Sys_Menu _bill_Sys_Menu;

    private Bill_Sys_InsertDefaultValues objDefaultValue;

    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;

    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;

    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;

    private CaseDetailsBO objCaseDetailsBO;

    private PDFValueReplacement.PDFValueReplacement objPDFReplacement;

    private Bill_Sys_NF3_Template objNF3Template;

    private DAO_NOTES_EO _DAO_NOTES_EO;

    private DAO_NOTES_BO _DAO_NOTES_BO;

    private Bill_Sys_DigosisCodeBO _digosisCodeBO;

    private MUVGenerateFunction _MUVGenerateFunction;

    private Bill_Sys_Verification_Desc objVerification_Desc;

    private string bt_include;

    private string str_1500;


    private void BindAssociateGrid()
    {
        try
        {
            this._bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        }
        catch
        {
        }
    }

    private void BindDiagnosisGrid(string typeid)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid);
            this.grdDiagonosisCode.DataBind();
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void BindDiagnosisGrid(string typeid, string code, string description)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid, code, description);
            this.grdDiagonosisCode.DataBind();
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void BindIC9CodeControl(string id_)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        this._bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {
            ArrayList arrayLists = new ArrayList();
            arrayLists = this._bill_Sys_BillingCompanyDetails_BO.GetIC9CodeData(id_);
            if (arrayLists.Count > 0)
            {
                this.txtTransDetailID.Text = arrayLists[0].ToString();
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void BindLatestTransaction()
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this._listOperation.WebPage=this.Page;
            this._listOperation.Xml_File="MRILatestBillTransaction.xml";
            this._listOperation.LoadList();
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.grdLatestBillTransaction.Items[11].Visible = true;
                this.grdLatestBillTransaction.Items[12].Visible = true;
                this.grdLatestBillTransaction.Items[13].Visible = true;
            }
            else
            {
                this.grdLatestBillTransaction.Columns[11].Visible = false;
                this.grdLatestBillTransaction.Columns[12].Visible = true;
                this.grdLatestBillTransaction.Columns[13].Visible = false;
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void BindTransactionData(string id_)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(id_, MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            DataSet dataSet = new DataSet();
            dataSet = this._bill_Sys_BillTransaction.BindTransactionData(id_);
            this.grdTransactionDetails.DataSource = dataSet;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT != "1")
            {
                this.grdTransactionDetails.Columns[6].Visible = true;
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            else
            {
                this.grdTransactionDetails.Columns[6].Visible = false;
                this.grdTransactionDetails.Columns[7].Visible = true;
            }
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                this.extddlspeciality.Text=dataSet.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP_ID"].ToString();
                if (this.extddlspeciality.Text != "NA")
                {
                    if (!this._bill_Sys_BillTransaction.checkUnitRefferal(dataSet.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP_ID"].ToString(), this.txtCompanyID.Text))
                    {
                        this.grdTransactionDetails.Columns[8].Visible = false;
                    }
                    else
                    {
                        this.grdTransactionDetails.Columns[8].Visible = true;
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                            {
                                if (this.grdTransactionDetails.Items[j].Cells[0].Text == dataSet.Tables[0].Rows[i]["SZ_BILL_TXN_DETAIL_ID"].ToString())
                                {
                                    TextBox textBox = (TextBox)this.grdTransactionDetails.Items[j].FindControl("txtUnit");
                                    if (!(dataSet.Tables[0].Rows[i][8].ToString().Trim() != "0") || !(dataSet.Tables[0].Rows[i][8].ToString().Trim() != "&nbsp;") || !(dataSet.Tables[0].Rows[i][8].ToString().Trim() != ""))
                                    {
                                        textBox.Text = "1";
                                    }
                                    else
                                    {
                                        textBox.Text = dataSet.Tables[0].Rows[i][8].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void BindTransactionDetailsGrid(string billnumber)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            DataSet dataSet = new DataSet();
            dataSet = this._bill_Sys_BillTransaction.BindTransactionData(billnumber);
            this.grdTransactionDetails.DataSource = dataSet;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT!= "1")
            {
                this.grdTransactionDetails.Columns[6].Visible = true;
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            else
            {
                this.grdTransactionDetails.Columns[6].Visible = false;
                this.grdTransactionDetails.Columns[7].Visible = true;
            }
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                this.extddlspeciality.Text=dataSet.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP_ID"].ToString();
                if (this.extddlspeciality.Text != "NA")
                {
                    if (!this._bill_Sys_BillTransaction.checkUnitRefferal(dataSet.Tables[0].Rows[0]["SZ_PROCEDURE_GROUP_ID"].ToString(), this.txtCompanyID.Text))
                    {
                        this.grdTransactionDetails.Columns[8].Visible = false;
                    }
                    else
                    {
                        this.grdTransactionDetails.Columns[8].Visible = true;
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                            {
                                if (this.grdTransactionDetails.Items[j].Cells[0].Text == dataSet.Tables[0].Rows[i]["SZ_BILL_TXN_DETAIL_ID"].ToString())
                                {
                                    TextBox textBox = (TextBox)this.grdTransactionDetails.Items[j].FindControl("txtUnit");
                                    if (!(dataSet.Tables[0].Rows[i][8].ToString().Trim() != "0") || !(dataSet.Tables[0].Rows[i][8].ToString().Trim() != "&nbsp;") || !(dataSet.Tables[0].Rows[i][8].ToString().Trim() != ""))
                                    {
                                        textBox.Text = "1";
                                    }
                                    else
                                    {
                                        textBox.Text = dataSet.Tables[0].Rows[i][8].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAddDGCodes_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            string str1 = this.hndSeletedDGCodes.Value.Replace("--,", "*").Replace("--", "");
            string[] strArrays = str1.Split(new char[] { '*' });
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string[] strArrays1 = strArrays[i].Split(new char[] { '~' });
                ListItem listItem = new ListItem(strArrays1[1], strArrays1[2]);
                if (!this.lstDiagnosisCodes.Items.Contains(listItem))
                {
                    this.lstDiagnosisCodes.Items.Add(listItem);
                }
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str2 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str2));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAddService_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["TM_SZ_BILL_ID"] = this.extddlDoctor.Text;
            this.Session["Date_Of_Service"] = this.txtDateOfservice.Text;
            if (!this.txtDateOfServiceTo.Visible)
            {
                this.Session["TODate_Of_Service"] = null;
            }
            else
            {
                this.Session["TODate_Of_Service"] = this.txtDateOfServiceTo.Text;
            }
            this.Session["DOCTOR_ID"] = this.extddlDoctor.Text;
            this.Session["SZ_CASE_ID"] = this.txtCaseID.Text;
            this.Session["SELECTED_SERVICES"] = null;
            this.Createtable();
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectDiagnosis.aspx'); ", true);
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
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
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnClearService_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearControl();
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnFromTo_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtDateOfServiceTo.Visible = true;
            this.lblDateOfService.Visible = true;
            this.lblTo.Visible = true;
            this.imgbtnFromTo.Visible = true;
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnGenerateWCPDF_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            string sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            string sZUSERNAME = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            string sZCOMPANYNAME = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            string sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string sZCASENO = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
            string str1 = (new WC_Bill_Generation()).GeneratePDFForWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.rdbListPDFType.SelectedValue, sZCOMPANYID, sZCOMPANYNAME, sZUSERID, sZUSERNAME, sZCASENO, this.hdnSpeciality.Value.ToString(), 1);
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str1, "');"), true);
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str2 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str2));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i < this.grdDiagonosisCode.Items.Count; i++)
            {
                if (((CheckBox)this.grdDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode")).Checked)
                {
                    ListItem listItem = new ListItem(string.Concat(this.grdDiagonosisCode.Items[i].Cells[2].Text, '-', this.grdDiagonosisCode.Items[i].Cells[4].Text), this.grdDiagonosisCode.Items[i].Cells[1].Text);
                    if (!this.lstDiagnosisCodes.Items.Contains(listItem))
                    {
                        this.lstDiagnosisCodes.Items.Add(listItem);
                    }
                }
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnOn_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtDateOfServiceTo.Visible = false;
            this.lblDateOfService.Visible = false;
            this.lblTo.Visible = false;
            this.imgbtnFromTo.Visible = false;
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            dataTable.Columns.Add("DT_DATE_OF_SERVICE");
            dataTable.Columns.Add("SZ_PROCEDURE_ID");
            dataTable.Columns.Add("SZ_PROCEDURAL_CODE");
            dataTable.Columns.Add("SZ_CODE_DESCRIPTION");
            dataTable.Columns.Add("FLT_AMOUNT");
            dataTable.Columns.Add("FACTOR_AMOUNT");
            dataTable.Columns.Add("FACTOR");
            dataTable.Columns.Add("PROC_AMOUNT");
            dataTable.Columns.Add("DOCT_AMOUNT");
            dataTable.Columns.Add("I_UNIT");
            dataTable.Columns.Add("SZ_TYPE_CODE_ID");
            dataTable.Columns.Add("SZ_PATIENT_TREATMENT_ID");
            dataTable.Columns.Add("SZ_STUDY_NUMBER");
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    DataRow shortDateString = dataTable.NewRow();
                    if (!(item.Cells[0].Text.ToString() != "&nbsp;") || !(item.Cells[0].Text.ToString() != ""))
                    {
                        shortDateString["SZ_BILL_TXN_DETAIL_ID"] = "";
                    }
                    else
                    {
                        shortDateString["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
                    }
                    DateTime dateTime = Convert.ToDateTime(item.Cells[1].Text.ToString());
                    shortDateString["DT_DATE_OF_SERVICE"] = dateTime.ToShortDateString();
                    shortDateString["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
                    shortDateString["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
                    shortDateString["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
                    if (Convert.ToDecimal(((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString()) > new decimal(0))
                    {
                        shortDateString["FACTOR_AMOUNT"] = ((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString();
                    }
                    if (Convert.ToDecimal(((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString()) > new decimal(0))
                    {
                        shortDateString["FACTOR"] = ((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString();
                    }
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT != "1")
                    {
                        shortDateString["FLT_AMOUNT"] = item.Cells[6].Text.ToString();
                    }
                    else
                    {
                        shortDateString["FLT_AMOUNT"] = ((TextBox)item.Cells[7].FindControl("txtAmt")).Text.ToString();
                    }
                    if (((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString() != "" && Convert.ToInt32(((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString()) > 0)
                    {
                        shortDateString["I_UNIT"] = ((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString();
                    }
                    shortDateString["PROC_AMOUNT"] = item.Cells[10].Text.ToString();
                    shortDateString["DOCT_AMOUNT"] = item.Cells[11].Text.ToString();
                    shortDateString["SZ_TYPE_CODE_ID"] = item.Cells[13].Text.ToString();
                    shortDateString["SZ_PATIENT_TREATMENT_ID"] = item.Cells[14].Text.ToString();
                    shortDateString["SZ_STUDY_NUMBER"] = item.Cells[15].Text.ToString();
                    dataTable.Rows.Add(shortDateString);
                }
            }
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                {
                    if (this.grdTransactionDetails.Items[i].Cells[0].Text != "" && this.grdTransactionDetails.Items[i].Cells[0].Text != "&nbsp;")
                    {
                        if (((CheckBox)this.grdTransactionDetails.Items[i].FindControl("chkSelect")).Checked)
                        {
                            string text = this.grdTransactionDetails.Items[i].Cells[1].Text;
                            string text1 = this.grdTransactionDetails.Items[i].Cells[3].Text;
                            int num = 0;
                            while (num < dataTable.Rows.Count)
                            {
                                if (!(text1 == dataTable.Rows[num][3].ToString()) || DateTime.Compare(Convert.ToDateTime(text), Convert.ToDateTime(dataTable.Rows[num][1].ToString())) != 0)
                                {
                                    num++;
                                }
                                else
                                {
                                    dataTable.Rows[num].Delete();
                                }
                            }
                        }
                    }
                    else if (((CheckBox)this.grdTransactionDetails.Items[i].FindControl("chkSelect")).Checked)
                    {
                        string str1 = this.grdTransactionDetails.Items[i].Cells[1].Text;
                        string text2 = this.grdTransactionDetails.Items[i].Cells[3].Text;
                        int num1 = 0;
                        while (num1 < dataTable.Rows.Count)
                        {
                            if (!(text2 == dataTable.Rows[num1][3].ToString()) || DateTime.Compare(Convert.ToDateTime(str1), Convert.ToDateTime(dataTable.Rows[num1][1].ToString())) != 0)
                            {
                                num1++;
                            }
                            else
                            {
                                dataTable.Rows[num1].Delete();
                                break;
                            }
                        }
                    }
                }
            }
            this.grdTransactionDetails.DataSource = dataTable;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT != "1")
            {
                this.grdTransactionDetails.Columns[6].Visible = true;
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            else
            {
                this.grdTransactionDetails.Columns[6].Visible = false;
                this.grdTransactionDetails.Columns[7].Visible = true;
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str2 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str2));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
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
            if (this.Session["AssociateDiagnosis"] == null)
            {
                this.grdTransactionDetails.Visible = true;
            }
            else if (!Convert.ToBoolean(this.Session["AssociateDiagnosis"].ToString()))
            {
                this.grdTransactionDetails.Visible = true;
            }
            else
            {
                this.btnAddService.Enabled = true;
                this.btnSave.Enabled = false;
            }
        }
        catch (SqlException sqlException)
        {
            this.ErrorDiv.InnerText = " Bill Number already exists";
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            int num = 0;
            DataSet item = (DataSet)this.Session["ProcedureCoes"];
            if (item.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in item.Tables[0].Rows)
                {
                    if (!(row["INSURANCE_ID"].ToString() == "") && row["INSURANCE_ID"].ToString() != null && !(row["INSURANCE_ADDR"].ToString() == "") && row["INSURANCE_ADDR"].ToString() != null && !(row["CLAIM_NO"].ToString() == "") && row["CLAIM_NO"].ToString() != null)
                    {
                        continue;
                    }
                    num = 1;
                    break;
                }
                if (num != 0)
                {
                    this.popupmsg.InnerText = "You do not have a claim number or an insurance company or an insurance company address added to case. Cannot proceed futher.";
                    this.Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage1();</script>");
                }
                else
                {
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        this.txtReferringCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                        this.txtReferringCompanyID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    BillTransactionEO billTransactionEO = new BillTransactionEO();
                    billTransactionEO.SZ_CASE_ID=this.txtCaseID.Text;
                    billTransactionEO.DT_BILL_DATE=Convert.ToDateTime(this.txtBillDate.Text);
                    billTransactionEO.SZ_COMPANY_ID=this.txtCompanyID.Text;
                    billTransactionEO.SZ_DOCTOR_ID=this.extddlDoctor.Text;
                    billTransactionEO.SZ_TYPE=this.ddlType.Text;
                    billTransactionEO.SZ_TESTTYPE="";
                    billTransactionEO.SZ_READING_DOCTOR_ID=this.extddlReadingDoctor.Text;
                    billTransactionEO.SZ_Referring_Company_Id=this.txtReferringCompanyID.Text;
                    billTransactionEO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    ArrayList arrayLists = new ArrayList();
                    foreach (DataGridItem dataGridItem in this.grdTransactionDetails.Items)
                    {
                        BillProcedureCodeEO billProcedureCodeEO = new BillProcedureCodeEO();
                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        billProcedureCodeEO.DT_DATE_OF_SERVICE=Convert.ToDateTime(dataGridItem.Cells[1].Text.ToString());
                        billProcedureCodeEO.SZ_PROCEDURE_ID=dataGridItem.Cells[2].Text.ToString();
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            string str = ((TextBox)dataGridItem.Cells[7].FindControl("txtAmt")).Text.ToString();
                            if (str == "&nbsp;")
                            {
                                billProcedureCodeEO.FL_AMOUNT = "0";
                            }
                            else
                            {
                                billProcedureCodeEO.FL_AMOUNT = str;
                            }
                        }
                        else if (dataGridItem.Cells[6].Text.ToString() == "&nbsp;")
                        {
                            billProcedureCodeEO.FL_AMOUNT="0";
                        }
                        else
                        {
                            billProcedureCodeEO.FL_AMOUNT=dataGridItem.Cells[6].Text.ToString();
                        }
                        billProcedureCodeEO.SZ_BILL_NUMBER="";
                        billProcedureCodeEO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_EMG_BILL != "True")
                        {
                            billProcedureCodeEO.I_UNIT=((TextBox)dataGridItem.Cells[8].FindControl("txtUnit")).Text.ToString();
                        }
                        else if (dataGridItem.Cells[15].Text.ToString() == "1" || dataGridItem.Cells[15].Text.ToString() == null || dataGridItem.Cells[15].Text.ToString() == "&nbsp;")
                        {
                            billProcedureCodeEO.I_UNIT="1";
                        }
                        else
                        {
                            billProcedureCodeEO.I_UNIT=dataGridItem.Cells[15].Text.ToString();
                        }
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT != "1")
                        {
                            billProcedureCodeEO.FLT_PRICE=((Label)dataGridItem.Cells[5].FindControl("lblPrice")).Text;
                            billProcedureCodeEO.DOCT_AMOUNT=((Label)dataGridItem.Cells[5].FindControl("lblPrice")).Text;
                        }
                        else
                        {
                            billProcedureCodeEO.FLT_PRICE=((TextBox)dataGridItem.Cells[7].FindControl("txtAmt")).Text.ToString();
                            billProcedureCodeEO.DOCT_AMOUNT=((TextBox)dataGridItem.Cells[7].FindControl("txtAmt")).Text.ToString();
                        }
                        billProcedureCodeEO.FLT_FACTOR=((Label)dataGridItem.Cells[5].FindControl("lblFactor")).Text;
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            billProcedureCodeEO.PROC_AMOUNT=((TextBox)dataGridItem.Cells[7].FindControl("txtAmt")).Text.ToString();
                        }
                        else if (!(dataGridItem.Cells[10].Text.ToString() != "&nbsp;") || !(dataGridItem.Cells[10].Text.ToString() != ""))
                        {
                            billProcedureCodeEO.PROC_AMOUNT="0";
                        }
                        else
                        {
                            billProcedureCodeEO.PROC_AMOUNT=dataGridItem.Cells[10].Text.ToString();
                        }
                        billProcedureCodeEO.SZ_DOCTOR_ID=this.extddlDoctor.Text;
                        billProcedureCodeEO.SZ_CASE_ID=this.txtCaseID.Text;
                        billProcedureCodeEO.SZ_TYPE_CODE_ID=dataGridItem.Cells[13].Text.ToString();
                        billProcedureCodeEO.FLT_GROUP_AMOUNT="";
                        billProcedureCodeEO.I_GROUP_AMOUNT_ID="";
                        billProcedureCodeEO.SZ_PATIENT_TREATMENT_ID=dataGridItem.Cells[14].Text.ToString();
                        arrayLists.Add(billProcedureCodeEO);
                    }
                    ArrayList arrayLists1 = new ArrayList();
                    foreach (ListItem listItem in this.lstDiagnosisCodes.Items)
                    {
                        BillDiagnosisCodeEO billDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        billDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID=listItem.Value.ToString();
                        arrayLists1.Add(billDiagnosisCodeEO);
                    }
                    BillTransactionDAO billTransactionDAO = new BillTransactionDAO();
                    Result result = new Result();
                    result = billTransactionDAO.SaveBillTransactionForTest(billTransactionEO, arrayLists, arrayLists1, this.extddlspeciality.Text);
                    if (result.msg_code != "ERR")
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
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("Config\\DV_DoctorOpinion.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("Config\\DV_ExamInformation.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("Config\\DV_History.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("Config\\DV_PlanOfCare.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("Config\\DV_WorkStatus.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                            }
                            else if (this.grdLatestBillTransaction.Items.Count >= 1)
                            {
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("Config\\DV_DoctorsOpinionC4_2.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("Config\\DV_ExaminationTreatment.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("Config\\DV_PermanentImpairment.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                                this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("Config\\DV_WorkStatusC4_2.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                            }
                        }
                        this.btnAddService.Enabled = true;
                        this.BindLatestTransaction();
                        this.ClearControl();
                        this.lblMsg.Visible = true;
                        this.lblMsg.Text = " Bill Saved successfully ! ";
                        if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                        {
                            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            this.GenerateAddedBillPDF(text, this._bill_Sys_BillTransaction.GetDocSpeciality(text));
                        }
                        else
                        {
                            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            this.GenerateAddedBillPDF(text, this._bill_Sys_BillTransaction.GetDocSpeciality(text));
                        }
                    }
                    else
                    {
                        this.lblMsg.Text = result.msg;
                        this.lblMsg.Visible = true;
                    }
                }
            }
        }
        catch
        {
        }
    }

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindDiagnosisGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
            ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        try
        {
            if (this.Session["SZ_BILL_NUMBER"] == null)
            {
                this._editOperation.Primary_Value=this.Session["BillID"].ToString();
                this._editOperation.WebPage=this.Page;
                this._editOperation.Xml_File="BillTransaction.xml";
                this._editOperation.UpdateMethod();
                this.lblMsg.Visible = true;
                this.lblMsg.Text = " Bill Updated successfully ! ";
            }
            else
            {
                this._editOperation.Primary_Value=this.Session["SZ_BILL_NUMBER"].ToString();
                this._editOperation.WebPage=this.Page;
                this._editOperation.Xml_File="BillTransaction.xml";
                this._editOperation.UpdateMethod();
                this.Session["SZ_BILL_NUMBER"] = null;
                base.Response.Redirect("Bill_Sys_BillSearch.aspx", false);
            }
            this.BindLatestTransaction();
        }
        catch (SqlException sqlException)
        {
            this.ErrorDiv.InnerText = " Bill Number already exists";
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        ArrayList arrayLists;
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        try
        {
            if (this.Session["SZ_BILL_NUMBER"] != null)
            {
                this._editOperation.Primary_Value=this.Session["SZ_BILL_NUMBER"].ToString();
                this._editOperation.WebPage=this.Page;
                this._editOperation.Xml_File="ReffeBillTransaction.xml";
                this._editOperation.UpdateMethod();
                this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                string str1 = this.Session["SZ_BILL_NUMBER"].ToString();
                this.btnAddService.Enabled = true;
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    if (!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;") || !(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;") || !(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;"))
                    {
                        continue;
                    }
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    arrayLists = new ArrayList();
                    if (item.Cells[0].Text.ToString() != "" && item.Cells[0].Text.ToString() != "&nbsp;")
                    {
                        arrayLists.Add(item.Cells[0].Text.ToString());
                    }
                    arrayLists.Add(item.Cells[2].Text.ToString());
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        string str2 = ((TextBox)item.Cells[7].FindControl("txtAmt")).Text.ToString();
                        if (str2 == "&nbsp;")
                        {
                            arrayLists.Add(0);
                        }
                        else
                        {
                            arrayLists.Add(str2);
                        }
                    }
                    else if (item.Cells[6].Text.ToString() == "&nbsp;")
                    {
                        arrayLists.Add(0);
                    }
                    else
                    {
                        arrayLists.Add(item.Cells[6].Text.ToString());
                    }
                    arrayLists.Add(str1);
                    arrayLists.Add(Convert.ToDateTime(item.Cells[1].Text.ToString()));
                    arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    arrayLists.Add(((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString());
                    arrayLists.Add(((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString());
                    arrayLists.Add(((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString());
                    arrayLists.Add(item.Cells[10].Text.ToString());
                    arrayLists.Add(item.Cells[9].Text.ToString());
                    if (!(item.Cells[0].Text.ToString() != "") || !(item.Cells[0].Text.ToString() != "&nbsp;"))
                    {
                        arrayLists.Add(this.extddlDoctor.Text);
                        arrayLists.Add(this.txtCaseID.Text);
                        arrayLists.Add(item.Cells[13].Text.ToString());
                        this._bill_Sys_BillTransaction.SaveTransactionData(arrayLists);
                    }
                    else
                    {
                        arrayLists.Add(item.Cells[12].Text.ToString());
                        this._bill_Sys_BillTransaction.UpdateTransactionData(arrayLists);
                    }
                }
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                this._bill_Sys_BillTransaction.DeleteTransactionDiagnosis(str1);
                foreach (ListItem listItem in this.lstDiagnosisCodes.Items)
                {
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    arrayLists = new ArrayList();
                    arrayLists.Add(listItem.Value.ToString());
                    arrayLists.Add(str1);
                    this._bill_Sys_BillTransaction.SaveTransactionDiagnosis(arrayLists);
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_UPDATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=str1;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID=this.txtCompanyID.Text;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                this.lblMsg.Visible = true;
                this.lblMsg.Text = " Bill Updated successfully ! ";
            }
            if (this.Session["BillID"] != null)
            {
                this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
            }
            else
            {
                this.BindTransactionDetailsGrid(this.Session["SZ_BILL_NUMBER"].ToString());
            }
            this.BindLatestTransaction();
        }
        catch (SqlException sqlException)
        {
            this.ErrorDiv.InnerText = " Bill Number already exists";
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str3 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str3));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdateService_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.UpdateTransactionDetails();
            this.BindLatestTransaction();
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void CalculateAmount(string id_)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {
            this._bill_Sys_Menu.GetICcodeAmount(id_);
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void ClearControl()
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
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
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT!= "1")
            {
                this.grdTransactionDetails.Columns[6].Visible = true;
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            else
            {
                this.grdTransactionDetails.Columns[6].Visible = false;
                this.grdTransactionDetails.Columns[7].Visible = true;
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
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void Createtable()
    {
        ArrayList arrayLists = new ArrayList();
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
        dataTable.Columns.Add("DT_DATE_OF_SERVICE");
        dataTable.Columns.Add("SZ_PROCEDURE_ID");
        dataTable.Columns.Add("SZ_PROCEDURAL_CODE");
        dataTable.Columns.Add("SZ_CODE_DESCRIPTION");
        dataTable.Columns.Add("FACTOR_AMOUNT");
        dataTable.Columns.Add("FLT_AMOUNT");
        dataTable.Columns.Add("FACTOR");
        dataTable.Columns.Add("PROC_AMOUNT");
        dataTable.Columns.Add("DOCT_AMOUNT");
        dataTable.Columns.Add("I_UNIT");
        foreach (DataGridItem item in this.grdTransactionDetails.Items)
        {
            DataRow str = dataTable.NewRow();
            if (!(item.Cells[0].Text.ToString() != "&nbsp;") || !(item.Cells[0].Text.ToString() != ""))
            {
                str["SZ_BILL_TXN_DETAIL_ID"] = "";
            }
            else
            {
                str["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
            }
            DateTime dateTime = Convert.ToDateTime(item.Cells[1].Text.ToString());
            str["DT_DATE_OF_SERVICE"] = dateTime.ToShortDateString();
            str["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
            str["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
            str["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
            if (Convert.ToDecimal(((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString()) > new decimal(0))
            {
                str["FACTOR_AMOUNT"] = ((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString();
            }
            if (Convert.ToDecimal(((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString()) > new decimal(0))
            {
                str["FACTOR"] = ((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString();
            }
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT!= "1")
            {
                str["FLT_AMOUNT"] = item.Cells[6].Text.ToString();
            }
            else
            {
                str["FLT_AMOUNT"] = ((TextBox)item.Cells[7].FindControl("txtAmt")).Text.ToString();
            }
            if (((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString() != "" && Convert.ToInt32(((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString()) > 0)
            {
                str["I_UNIT"] = ((TextBox)item.Cells[8].FindControl("txtUnit")).Text.ToString();
            }
            str["PROC_AMOUNT"] = item.Cells[10].Text.ToString();
            str["DOCT_AMOUNT"] = item.Cells[11].Text.ToString();
            dataTable.Rows.Add(str);
        }
        this.Session["SELECTED_SERVICES"] = dataTable;
    }

    protected void extddlIC9Code_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_Menu = new Bill_Sys_Menu();
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            this.btnSave.Attributes.Add("onclick", "return Amountvalidate();");
            this.lblMsg.Visible = false;
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void extddlProcedureCode_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    }

    protected void extddlspeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this.Session["ProcedureCoes"] = null;
        try
        {
            DataSet dataSet = new DataSet();
            dataSet = (new Bill_Sys_BillTransaction_BO()).GetAssociateDiagCode(this.extddlspeciality.Text, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.txtCompanyID.Text);
            this.lstDiagnosisCodes.DataSource = dataSet.Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            dataSet = new DataSet();
            dataSet = (new Bill_Sys_BillTransaction_BO()).GetProcedureCodes(this.extddlspeciality.Text, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID, this.txtCompanyID.Text, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            dataTable.Columns.Add("DT_DATE_OF_SERVICE");
            dataTable.Columns.Add("SZ_PROCEDURE_ID");
            dataTable.Columns.Add("SZ_PROCEDURAL_CODE");
            dataTable.Columns.Add("SZ_CODE_DESCRIPTION");
            dataTable.Columns.Add("FLT_AMOUNT");
            dataTable.Columns.Add("FACTOR_AMOUNT");
            dataTable.Columns.Add("FACTOR");
            dataTable.Columns.Add("PROC_AMOUNT");
            dataTable.Columns.Add("DOCT_AMOUNT");
            dataTable.Columns.Add("I_UNIT");
            dataTable.Columns.Add("SZ_TYPE_CODE_ID");
            dataTable.Columns.Add("SZ_PATIENT_TREATMENT_ID");
            dataTable.Columns.Add("SZ_STUDY_NUMBER");
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                DataRow item = dataTable.NewRow();
                item["SZ_BILL_TXN_DETAIL_ID"] = "";
                item["DT_DATE_OF_SERVICE"] = row["DT_DATE_OF_SERVICE"];
                item["SZ_PROCEDURE_ID"] = row["SZ_PROCEDURE_ID"];
                item["SZ_PROCEDURAL_CODE"] = row["SZ_PROCEDURE_CODE"];
                item["SZ_CODE_DESCRIPTION"] = row["SZ_CODE_DESCRIPTION"];
                if (row["DOCTOR_AMOUNT"].ToString() != "0")
                {
                    item["FACTOR_AMOUNT"] = row["DOCTOR_AMOUNT"];
                    item["FLT_AMOUNT"] = Convert.ToDecimal(row["DOCTOR_AMOUNT"].ToString()) * Convert.ToDecimal(row["FLT_KOEL"].ToString());
                }
                else
                {
                    item["FACTOR_AMOUNT"] = row["FLT_AMOUNT"];
                    item["FLT_AMOUNT"] = Convert.ToDecimal(row["FLT_AMOUNT"].ToString()) * Convert.ToDecimal(row["FLT_KOEL"].ToString());
                }
                item["FACTOR"] = row["FLT_KOEL"];
                item["PROC_AMOUNT"] = row["FLT_AMOUNT"];
                item["DOCT_AMOUNT"] = row["DOCTOR_AMOUNT"];
                item["I_UNIT"] = "";
                item["SZ_TYPE_CODE_ID"] = row["SZ_TYPE_CODE_ID"];
                item["SZ_PATIENT_TREATMENT_ID"] = row["I_EVENT_PROCID"];
                item["SZ_STUDY_NUMBER"] = row["SZ_STUDY_NUMBER"];
                dataTable.Rows.Add(item);
            }
            this.Session["ProcedureCoes"] = dataSet;
            this.grdTransactionDetails.DataSource = dataTable;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT != "1")
            {
                this.grdTransactionDetails.Columns[6].Visible = true;
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            else
            {
                this.grdTransactionDetails.Columns[6].Visible = false;
                this.grdTransactionDetails.Columns[7].Visible = true;
            }
            Bill_Sys_BillTransaction_BO billSysBillTransactionBO = new Bill_Sys_BillTransaction_BO();
            if (this.extddlspeciality.Text != "NA")
            {
                if (!billSysBillTransactionBO.checkUnitRefferal(this.extddlspeciality.Text, this.txtCompanyID.Text))
                {
                    this.grdTransactionDetails.Columns[9].Visible = false;
                }
                else
                {
                    this.grdTransactionDetails.Columns[9].Visible = true;
                }
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality)
    {
        string sZCOMPANYID;
        string str;
        string sZCOMAPNYID;
        string sZCOMPANYNAME;
        string[] companyName;
        try
        {
            this._MUVGenerateFunction = new MUVGenerateFunction();
            string pSzSpeciality = p_szSpeciality;
            this.hdnSpeciality.Value = p_szSpeciality;
            this.Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this.Session["TM_SZ_BILL_ID"] = p_szBillNumber;
            this.objNF3Template = new Bill_Sys_NF3_Template();
            CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
            string sZCOMPANYID1 = "";
            sZCOMPANYID1 = (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) ? ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID : ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
            this.objVerification_Desc = new Bill_Sys_Verification_Desc();
            this.objVerification_Desc.sz_bill_no=p_szBillNumber;
            this.objVerification_Desc.sz_company_id=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.objVerification_Desc.sz_flag="BILL";
            ArrayList arrayLists = new ArrayList();
            ArrayList nodeType = new ArrayList();
            string str1 = "";
            string str2 = "";
            arrayLists.Add(this.objVerification_Desc);
            nodeType = this._bill_Sys_BillTransaction.Get_Node_Type(arrayLists);
            if (!nodeType.Contains("NFVER"))
            {
                str1 = "NEW";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    companyName = new string[] { ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Medicals/", pSzSpeciality, "/Bills/" };
                    str2 = string.Concat(companyName);
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template = new Bill_Sys_NF3_Template();
                    companyName = new string[] { billSysNF3Template.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Medicals/", pSzSpeciality, "/Bills/" };
                    str2 = string.Concat(companyName);
                }
            }
            else
            {
                str1 = "OLD";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    companyName = new string[] { ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Bills/", pSzSpeciality, "/" };
                    str2 = string.Concat(companyName);
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template1 = new Bill_Sys_NF3_Template();
                    companyName = new string[] { billSysNF3Template1.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Bills/", pSzSpeciality, "/" };
                    str2 = string.Concat(companyName);
                }
            }
            if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                string str3 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str3 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template2 = new Bill_Sys_NF3_Template();
                    str3 = string.Concat(billSysNF3Template2.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                }
                string str4 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str4 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template3 = new Bill_Sys_NF3_Template();
                    str4 = string.Concat(billSysNF3Template3.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                }
                string str5 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                string str6 = "";
                Bill_Sys_Configuration billSysConfiguration = new Bill_Sys_Configuration();
                string configurationSettings = billSysConfiguration.getConfigurationSettings(sZCOMPANYID1, "GET_DIAG_PAGE_POSITION");
                string configurationSettings1 = billSysConfiguration.getConfigurationSettings(sZCOMPANYID1, "DIAG_PAGE");
                string str7 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                string str8 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                string str9 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                string str10 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                GenerateNF3PDF generateNF3PDF = new GenerateNF3PDF();
                this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                string str11 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str11 = generateNF3PDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str5);
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template4 = new Bill_Sys_NF3_Template();
                    str11 = generateNF3PDF.GeneratePDF(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, billSysNF3Template4.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str5);
                }
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str6 = this.objPDFReplacement.ReplacePDFvalues(str7, str8, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template5 = new Bill_Sys_NF3_Template();
                    str6 = this.objPDFReplacement.ReplacePDFvalues(str7, str8, this.Session["TM_SZ_BILL_ID"].ToString(), billSysNF3Template5.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                }
                string str12 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str12 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str6, str11);
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template6 = new Bill_Sys_NF3_Template();
                    str12 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, billSysNF3Template6.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str6, str11);
                }
                string str13 = "";
                string str14 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str14 = this.objPDFReplacement.ReplacePDFvalues(str9, str10, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template7 = new Bill_Sys_NF3_Template();
                    str14 = this.objPDFReplacement.ReplacePDFvalues(str9, str10, this.Session["TM_SZ_BILL_ID"].ToString(), billSysNF3Template7.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                }
                string sZCOMPANYID2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.bt_include = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID2, pSzSpeciality, "", "Speciality");
                string btInclude = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID2, "", "WC000000000000000002", "CaseType");
                if (this.bt_include == "True" && btInclude == "True")
                {
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                }
                MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str3, str12), string.Concat(this.objNF3Template.getPhysicalPath(), str3, str14), string.Concat(this.objNF3Template.getPhysicalPath(), str3, str14.Replace(".pdf", "_MER.pdf")));
                str13 = str14.Replace(".pdf", "_MER.pdf");
                if (this.bt_include == "True" && btInclude == "True")
                {
                    MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str3, str13), string.Concat(this.objNF3Template.getPhysicalPath(), str3, this.str_1500), string.Concat(this.objNF3Template.getPhysicalPath(), str3, this.str_1500.Replace(".pdf", "_MER.pdf")));
                    str13 = this.str_1500.Replace(".pdf", "_MER.pdf");
                }
                string str15 = "";
                str15 = string.Concat(str3, str13);
                string str16 = "";
                str16 = string.Concat(ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString(), str15);
                string str17 = string.Concat(this.objNF3Template.getPhysicalPath(), "/", str15);
                CutePDFDocumentClass cutePDFDocumentClass = new CutePDFDocumentClass();
                string str18 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                cutePDFDocumentClass.initialize(str18);
                if (cutePDFDocumentClass != null && File.Exists(str17) && configurationSettings1 != "CI_0000003" && this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5 && configurationSettings == "CK_0000003" && (configurationSettings1 != "CI_0000004" || this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))
                {
                    str11 = str17.Replace(".pdf", "_NewMerge.pdf");
                }
                string str19 = "";
                if (File.Exists(str17) && File.Exists(str17.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str15 = str17.Replace(".pdf", "_New.pdf").ToString();
                }
                if (File.Exists(str17) && File.Exists(str17.Replace(".pdf", "_NewMerge.pdf").ToString()))
                {
                    str19 = str16.Replace(".pdf", "_NewMerge.pdf").ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str16.Replace(".pdf", "_NewMerge.pdf").ToString(), "'); "), true);
                }
                else if (!File.Exists(str17) || !File.Exists(str17.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str19 = str16.ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str16.ToString(), "'); "), true);
                }
                else
                {
                    str19 = str16.Replace(".pdf", "_New.pdf").ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str16.Replace(".pdf", "_New.pdf").ToString(), "'); "), true);
                }
                string str20 = "";
                string[] strArrays = str19.Split(new char[] { '/' });
                ArrayList arrayLists1 = new ArrayList();
                str19 = str19.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                str20 = strArrays[(int)strArrays.Length - 1].ToString();
                if (File.Exists(string.Concat(this.objNF3Template.getPhysicalPath(), str4, str20)))
                {
                    if (!Directory.Exists(string.Concat(this.objNF3Template.getPhysicalPath(), str2)))
                    {
                        Directory.CreateDirectory(string.Concat(this.objNF3Template.getPhysicalPath(), str2));
                    }
                    File.Copy(string.Concat(this.objNF3Template.getPhysicalPath(), str4, str20), string.Concat(this.objNF3Template.getPhysicalPath(), str2, str20));
                }
                if (str1 != "OLD")
                {
                    arrayLists1.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                    arrayLists1.Add(string.Concat(str2, str20));
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                    else
                    {
                        arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    }
                    arrayLists1.Add(this.Session["TM_SZ_CASE_ID"]);
                    arrayLists1.Add(strArrays[(int)strArrays.Length - 1].ToString());
                    arrayLists1.Add(str2);
                    arrayLists1.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    arrayLists1.Add(pSzSpeciality);
                    arrayLists1.Add("NF");
                    arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    arrayLists1.Add(nodeType[0].ToString());
                    this.objNF3Template.saveGeneratedBillPath_New(arrayLists1);
                }
                else
                {
                    arrayLists1.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                    arrayLists1.Add(string.Concat(str2, str20));
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                    else
                    {
                        arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    }
                    arrayLists1.Add(this.Session["TM_SZ_CASE_ID"]);
                    arrayLists1.Add(strArrays[(int)strArrays.Length - 1].ToString());
                    arrayLists1.Add(str2);
                    arrayLists1.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    arrayLists1.Add(pSzSpeciality);
                    arrayLists1.Add("NF");
                    arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    this.objNF3Template.saveGeneratedBillPath(arrayLists1);
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_GENERATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=str20;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                else
                {
                    this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                this.BindLatestTransaction();
            }
            else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
            {
                string str21 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str21 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template8 = new Bill_Sys_NF3_Template();
                    str21 = string.Concat(billSysNF3Template8.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                }
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template billSysNF3Template9 = new Bill_Sys_NF3_Template();
                }
                Bill_Sys_PVT_Template billSysPVTTemplate = new Bill_Sys_PVT_Template();
                string str22 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str23 = this.Session["TM_SZ_BILL_ID"].ToString();
                string sZUSERNAME = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                else
                {
                    (new Bill_Sys_NF3_Template()).GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                Lien lien = new Lien();
                string sZCOMPANYID3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.bt_include = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID3, pSzSpeciality, "", "Speciality");
                string btInclude1 = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID3, "", "WC000000000000000004", "CaseType");
                if (!(this.bt_include == "True") || !(btInclude1 == "True"))
                {
                    str = lien.GenratePdfForLien(sZCOMPANYID, str23, pSzSpeciality, str22, sZUSERNAME, this.txtCaseNo.Text, sZUSERID);
                }
                else
                {
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                    MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str2, lien.GenratePdfForLienWithMuv(sZCOMPANYID, str23, pSzSpeciality, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, sZUSERNAME, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, sZUSERID)), string.Concat(this.objNF3Template.getPhysicalPath(), str21, this.str_1500), string.Concat(this.objNF3Template.getPhysicalPath(), str2, this.str_1500.Replace(".pdf", "_MER.pdf")));
                    str = string.Concat(ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString(), str2, this.str_1500.Replace(".pdf", "_MER.pdf"));
                    ArrayList arrayLists2 = new ArrayList();
                    if (str1 != "OLD")
                    {
                        arrayLists2.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        arrayLists2.Add(string.Concat(str2, this.str_1500.Replace(".pdf", "_MER.pdf")));
                        arrayLists2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        arrayLists2.Add(this.Session["TM_SZ_CASE_ID"]);
                        arrayLists2.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        arrayLists2.Add(str2);
                        arrayLists2.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        arrayLists2.Add(pSzSpeciality);
                        arrayLists2.Add("NF");
                        arrayLists2.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        arrayLists2.Add(nodeType[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(arrayLists2);
                    }
                    else
                    {
                        arrayLists2.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        arrayLists2.Add(string.Concat(str2, this.str_1500.Replace(".pdf", "_MER.pdf")));
                        arrayLists2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        arrayLists2.Add(this.Session["TM_SZ_CASE_ID"]);
                        arrayLists2.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        arrayLists2.Add(str2);
                        arrayLists2.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        arrayLists2.Add(pSzSpeciality);
                        arrayLists2.Add("NF");
                        arrayLists2.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(arrayLists2);
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
                ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str, "');"), true);
            }
            else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) != "WC000000000000000003")
            {
                string sZUSERID1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                string sZUSERNAME1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string sZCOMPANYNAME1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                sZCOMPANYID1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                WC_Bill_Generation wCBillGeneration = new WC_Bill_Generation();
                this.hdnWCPDFBillNumber.Value = (string)this.Session["TM_SZ_BILL_ID"];
                ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", wCBillGeneration.GeneratePDFForReferalWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, sZCOMPANYID1, sZCOMPANYNAME1, sZUSERID1, sZUSERNAME1, this.hdnSpeciality.Value.ToString()), "');"), true);
            }
            else
            {
                Bill_Sys_PVT_Template billSysPVTTemplate1 = new Bill_Sys_PVT_Template();
                bool bTREFERRINGFACILITY = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str24 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str25 = this.Session["TM_SZ_BILL_ID"].ToString();
                string sZUSERNAME2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string sZUSERID2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    sZCOMPANYNAME = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    sZCOMAPNYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                else
                {
                    sZCOMPANYNAME = (new Bill_Sys_NF3_Template()).GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    sZCOMAPNYID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", billSysPVTTemplate1.GeneratePVTBill(bTREFERRINGFACILITY, sZCOMAPNYID, str24, pSzSpeciality, sZCOMPANYNAME, str25, sZUSERNAME2, sZUSERID2), "'); "), true);
            }
            Bill_Sys_BillTransaction_BO billSysBillTransactionBO = new Bill_Sys_BillTransaction_BO();
        }
        catch (Exception exception)
        {
            throw;
        }
    }

    private void GetDiagnosisCode()
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = billSysAssociateDiagnosisCodeBO.GetDoctorDiagnosisCode("", this.txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE").Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void GetPatientDeskList()
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdPatientDeskList.DataSource = billSysPatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            this.grdPatientDeskList.DataBind();
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void GetProcedureCode()
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable dataTable = new DataTable();
            DataTable item = new DataTable();
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            dataTable = this._bill_Sys_BillTransaction.GetPatientDoctor_List(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID, this.txtCompanyID.Text).Tables[0];
            foreach (DataRow row in dataTable.Rows)
            {
                item = this._bill_Sys_BillTransaction.GetDoctorProcedureCodeList(row["SZ_DOCTOR_ID"].ToString(), "TY000000000000000003", this.txtCaseID.Text).Tables[0];
                try
                {
                    DataTable dataTable1 = new DataTable();
                    dataTable1.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
                    dataTable1.Columns.Add("DT_DATE_OF_SERVICE");
                    dataTable1.Columns.Add("SZ_PROCEDURE_ID");
                    dataTable1.Columns.Add("SZ_PROCEDURAL_CODE");
                    dataTable1.Columns.Add("SZ_CODE_DESCRIPTION");
                    dataTable1.Columns.Add("FLT_AMOUNT");
                    dataTable1.Columns.Add("FACTOR_AMOUNT");
                    dataTable1.Columns.Add("FACTOR");
                    dataTable1.Columns.Add("PROC_AMOUNT");
                    dataTable1.Columns.Add("DOCT_AMOUNT");
                    dataTable1.Columns.Add("I_UNIT");
                    dataTable1.Columns.Add("SZ_TYPE_CODE_ID");
                    dataTable1.Columns.Add("SZ_PATIENT_TREATMENT_ID");
                    string str1 = "";
                    string str2 = "";
                    string str3 = "";
                    foreach (DataRow dataRow in item.Rows)
                    {
                        str1 = dataRow["CODE"].ToString().Substring(0, dataRow["CODE"].ToString().IndexOf("|"));
                        str2 = dataRow["CODE"].ToString().Substring(dataRow["CODE"].ToString().IndexOf("|") + 1, dataRow["CODE"].ToString().Length - (dataRow["CODE"].ToString().IndexOf("|") + 1));
                        str3 = dataRow["SZ_PATIENT_TREATMENT_ID"].ToString();
                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        DataTable item1 = new DataTable();
                        item1 = this._bill_Sys_BillTransaction.GeSelectedDoctorProcedureCode_List(str1, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, row["SZ_DOCTOR_ID"].ToString(), str2).Tables[0];
                        foreach (DataRow row1 in item1.Rows)
                        {
                            DataRow num = dataTable1.NewRow();
                            num["SZ_BILL_TXN_DETAIL_ID"] = "";
                            string str4 = dataRow["DESCRIPTION"].ToString().Substring(dataRow["DESCRIPTION"].ToString().Substring(0, dataRow["DESCRIPTION"].ToString().IndexOf("--")).Length + 2, dataRow["DESCRIPTION"].ToString().Length - (dataRow["DESCRIPTION"].ToString().Substring(0, dataRow["DESCRIPTION"].ToString().IndexOf("--")).Length + 2));
                            num["DT_DATE_OF_SERVICE"] = str4.Substring(0, str4.IndexOf("--"));
                            num["SZ_PROCEDURE_ID"] = row1["SZ_PROCEDURE_ID"];
                            num["SZ_PROCEDURAL_CODE"] = row1["SZ_PROCEDURE_CODE"];
                            num["SZ_CODE_DESCRIPTION"] = row1["SZ_CODE_DESCRIPTION"];
                            if (row1["DOCTOR_AMOUNT"].ToString() != "0")
                            {
                                num["FACTOR_AMOUNT"] = row1["DOCTOR_AMOUNT"];
                                num["FLT_AMOUNT"] = Convert.ToDecimal(row1["DOCTOR_AMOUNT"].ToString()) * Convert.ToDecimal(row1["FLT_KOEL"].ToString());
                            }
                            else
                            {
                                num["FACTOR_AMOUNT"] = row1["FLT_AMOUNT"];
                                num["FLT_AMOUNT"] = Convert.ToDecimal(row1["FLT_AMOUNT"].ToString()) * Convert.ToDecimal(row1["FLT_KOEL"].ToString());
                            }
                            num["FACTOR"] = row1["FLT_KOEL"];
                            num["PROC_AMOUNT"] = row1["FLT_AMOUNT"];
                            num["DOCT_AMOUNT"] = row1["DOCTOR_AMOUNT"];
                            num["I_UNIT"] = "";
                            num["SZ_TYPE_CODE_ID"] = str2;
                            num["SZ_PATIENT_TREATMENT_ID"] = str3;
                            dataTable1.Rows.Add(num);
                        }
                    }
                    this.grdTransactionDetails.DataSource = dataTable1;
                    this.grdTransactionDetails.DataBind();
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT != "1")
                    {
                        this.grdTransactionDetails.Columns[6].Visible = true;
                        this.grdTransactionDetails.Columns[7].Visible = false;
                    }
                    else
                    {
                        this.grdTransactionDetails.Columns[6].Visible = false;
                        this.grdTransactionDetails.Columns[7].Visible = true;
                    }
                }
                catch
                {
                }
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str5 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str5));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void grdDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            if (this.extddlDiagnosisType.Text != "NA")
            {
                this.BindDiagnosisGrid(this.extddlDiagnosisType.Text);
            }
            else
            {
                this.BindDiagnosisGrid("");
            }
            ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void grdLatestBillTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string sZCOMPANYID;
        string sZCOMPANYNAME;
        string str;
        string str1;
        string[] companyName;
        string str2 = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str2, MethodBase.GetCurrentMethod());
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
                string group = this.objNF3Template.getGroup(e.Item.Cells[1].Text);
                this.Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                this.Session["TM_SZ_BILL_ID"] = e.Item.Cells[1].Text;
                CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
                string sZCOMPANYID1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.objVerification_Desc = new Bill_Sys_Verification_Desc();
                this.objVerification_Desc.sz_bill_no=this.Session["TM_SZ_BILL_ID"].ToString();
                this.objVerification_Desc.sz_company_id=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.objVerification_Desc.sz_flag="BILL";
                ArrayList arrayLists = new ArrayList();
                ArrayList nodeType = new ArrayList();
                string str3 = "";
                string str4 = "";
                arrayLists.Add(this.objVerification_Desc);
                nodeType = (new Bill_Sys_BillTransaction_BO()).Get_Node_Type(arrayLists);
                if (!nodeType.Contains("NFVER"))
                {
                    str3 = "NEW";
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        companyName = new string[] { ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Medicals/", group, "/Bills/" };
                        str4 = string.Concat(companyName);
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template = new Bill_Sys_NF3_Template();
                        companyName = new string[] { billSysNF3Template.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Medicals/", group, "/Bills/" };
                        str4 = string.Concat(companyName);
                    }
                }
                else
                {
                    str3 = "OLD";
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        companyName = new string[] { ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Bills/", group, "/" };
                        str4 = string.Concat(companyName);
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template1 = new Bill_Sys_NF3_Template();
                        companyName = new string[] { billSysNF3Template1.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Bills/", group, "/" };
                        str4 = string.Concat(companyName);
                    }
                }
                if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
                {
                    string str5 = "";
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        str5 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template2 = new Bill_Sys_NF3_Template();
                        str5 = string.Concat(billSysNF3Template2.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                    }
                    string str6 = "";
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        str6 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template3 = new Bill_Sys_NF3_Template();
                        str6 = string.Concat(billSysNF3Template3.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                    }
                    string str7 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                    ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                    string str8 = "";
                    Bill_Sys_Configuration billSysConfiguration = new Bill_Sys_Configuration();
                    string configurationSettings = billSysConfiguration.getConfigurationSettings(sZCOMPANYID1, "GET_DIAG_PAGE_POSITION");
                    string configurationSettings1 = billSysConfiguration.getConfigurationSettings(sZCOMPANYID1, "DIAG_PAGE");
                    string str9 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                    string str10 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                    string str11 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                    string str12 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                    GenerateNF3PDF generateNF3PDF = new GenerateNF3PDF();
                    this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string str13 = "";
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        str13 = generateNF3PDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str7);
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template4 = new Bill_Sys_NF3_Template();
                        str13 = generateNF3PDF.GeneratePDF(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, billSysNF3Template4.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str7);
                    }
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        str8 = this.objPDFReplacement.ReplacePDFvalues(str9, str10, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template5 = new Bill_Sys_NF3_Template();
                        str8 = this.objPDFReplacement.ReplacePDFvalues(str9, str10, this.Session["TM_SZ_BILL_ID"].ToString(), billSysNF3Template5.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                    }
                    string str14 = "";
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        str14 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str8, str13);
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template6 = new Bill_Sys_NF3_Template();
                        str14 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, billSysNF3Template6.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str8, str13);
                    }
                    string str15 = "";
                    string str16 = "";
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        str16 = this.objPDFReplacement.ReplacePDFvalues(str11, str12, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template7 = new Bill_Sys_NF3_Template();
                        str16 = this.objPDFReplacement.ReplacePDFvalues(str11, str12, this.Session["TM_SZ_BILL_ID"].ToString(), billSysNF3Template7.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                    }
                    string text = e.Item.Cells[3].Text;
                    string text1 = e.Item.Cells[16].Text;
                    string sZCOMPANYID2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID2, text1, "", "Speciality");
                    string btInclude = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID2, "", "WC000000000000000002", "CaseType");
                    if (this.bt_include == "True" && btInclude == "True")
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                    }
                    MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str5, str14), string.Concat(this.objNF3Template.getPhysicalPath(), str5, str16), string.Concat(this.objNF3Template.getPhysicalPath(), str5, str16.Replace(".pdf", "_MER.pdf")));
                    str15 = str16.Replace(".pdf", "_MER.pdf");
                    if (this.bt_include == "True" && btInclude == "True")
                    {
                        MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str5, str15), string.Concat(this.objNF3Template.getPhysicalPath(), str5, this.str_1500), string.Concat(this.objNF3Template.getPhysicalPath(), str5, this.str_1500.Replace(".pdf", "_MER.pdf")));
                        str15 = this.str_1500.Replace(".pdf", "_MER.pdf");
                    }
                    string str17 = "";
                    str17 = string.Concat(str5, str15);
                    string str18 = "";
                    str18 = string.Concat(ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString(), str17);
                    string str19 = string.Concat(this.objNF3Template.getPhysicalPath(), "/", str17);
                    CutePDFDocumentClass cutePDFDocumentClass = new CutePDFDocumentClass();
                    string str20 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                    cutePDFDocumentClass.initialize(str20);
                    if (cutePDFDocumentClass != null && File.Exists(str19) && configurationSettings1 != "CI_0000003" && this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5 && configurationSettings == "CK_0000003" && (configurationSettings1 != "CI_0000004" || this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))
                    {
                        str13 = str19.Replace(".pdf", "_NewMerge.pdf");
                    }
                    string str21 = "";
                    if (File.Exists(str19) && File.Exists(str19.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str17 = str19.Replace(".pdf", "_New.pdf").ToString();
                    }
                    if (File.Exists(str19) && File.Exists(str19.Replace(".pdf", "_NewMerge.pdf").ToString()))
                    {
                        str21 = str18.Replace(".pdf", "_NewMerge.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str18.Replace(".pdf", "_NewMerge.pdf").ToString(), "'); "), true);
                    }
                    else if (!File.Exists(str19) || !File.Exists(str19.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str21 = str18.ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str18.ToString(), "'); "), true);
                    }
                    else
                    {
                        str21 = str18.Replace(".pdf", "_New.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str18.Replace(".pdf", "_New.pdf").ToString(), "'); "), true);
                    }
                    string str22 = "";
                    string[] strArrays = str21.Split(new char[] { '/' });
                    ArrayList arrayLists1 = new ArrayList();
                    str21 = str21.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                    str22 = strArrays[(int)strArrays.Length - 1].ToString();
                    if (File.Exists(string.Concat(this.objNF3Template.getPhysicalPath(), str6, str22)))
                    {
                        if (!Directory.Exists(string.Concat(this.objNF3Template.getPhysicalPath(), str4)))
                        {
                            Directory.CreateDirectory(string.Concat(this.objNF3Template.getPhysicalPath(), str4));
                        }
                        File.Copy(string.Concat(this.objNF3Template.getPhysicalPath(), str6, str22), string.Concat(this.objNF3Template.getPhysicalPath(), str4, str22));
                    }
                    if (str3 != "OLD")
                    {
                        arrayLists1.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        arrayLists1.Add(string.Concat(str4, str22));
                        if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                        {
                            arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        }
                        else
                        {
                            arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        }
                        arrayLists1.Add(this.Session["TM_SZ_CASE_ID"]);
                        arrayLists1.Add(strArrays[(int)strArrays.Length - 1].ToString());
                        arrayLists1.Add(str4);
                        arrayLists1.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        arrayLists1.Add(group);
                        arrayLists1.Add("NF");
                        arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        arrayLists1.Add(nodeType[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(arrayLists1);
                    }
                    else
                    {
                        arrayLists1.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        arrayLists1.Add(string.Concat(str4, str22));
                        if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                        {
                            arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        }
                        else
                        {
                            arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        }
                        arrayLists1.Add(this.Session["TM_SZ_CASE_ID"]);
                        arrayLists1.Add(strArrays[(int)strArrays.Length - 1].ToString());
                        arrayLists1.Add(str4);
                        arrayLists1.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        arrayLists1.Add(group);
                        arrayLists1.Add("NF");
                        arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(arrayLists1);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=str22;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                    {
                        this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                        this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                }
                else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
                {
                    Bill_Sys_PVT_Template billSysPVTTemplate = new Bill_Sys_PVT_Template();
                    bool bTREFERRINGFACILITY = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                    string str23 = this.Session["TM_SZ_CASE_ID"].ToString();
                    string str24 = this.Session["TM_SZ_BILL_ID"].ToString();
                    string sZUSERNAME = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        sZCOMPANYNAME = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                        sZCOMPANYNAME = (new Bill_Sys_NF3_Template()).GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    sZCOMPANYNAME = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", billSysPVTTemplate.GeneratePVTBill(bTREFERRINGFACILITY, sZCOMPANYID, str23, group, sZCOMPANYNAME, str24, sZUSERNAME, sZUSERID), "'); "), true);
                }
                else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) != "WC000000000000000004")
                {
                    string sZUSERID1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    string sZUSERNAME1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string sZCOMPANYNAME1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    sZCOMPANYID1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    string str25 = (new WC_Bill_Generation()).GeneratePDFForReferalWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, sZCOMPANYID1, sZCOMPANYNAME1, sZUSERID1, sZUSERNAME1, this.hdnSpeciality.Value.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str25, "');"), true);
                }
                else
                {
                    string str26 = "";
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        str26 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template8 = new Bill_Sys_NF3_Template();
                        str26 = string.Concat(billSysNF3Template8.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                    }
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                    {
                        Bill_Sys_NF3_Template billSysNF3Template9 = new Bill_Sys_NF3_Template();
                    }
                    Bill_Sys_PVT_Template billSysPVTTemplate1 = new Bill_Sys_PVT_Template();
                    string str27 = this.Session["TM_SZ_CASE_ID"].ToString();
                    string str28 = this.Session["TM_SZ_BILL_ID"].ToString();
                    string sZUSERNAME2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string sZUSERID2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        str = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                        (new Bill_Sys_NF3_Template()).GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        str = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    Lien lien = new Lien();
                    string sZCOMPANYID3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID3, group, "", "Speciality");
                    string btInclude1 = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID3, "", "WC000000000000000004", "CaseType");
                    if (!(this.bt_include == "True") || !(btInclude1 == "True"))
                    {
                        str1 = lien.GenratePdfForLien(str, str28, group, str27, sZUSERNAME2, this.txtCaseNo.Text, sZUSERID2);
                    }
                    else
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                        MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str4, lien.GenratePdfForLienWithMuv(str, str28, group, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, sZUSERNAME2, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, sZUSERID2)), string.Concat(this.objNF3Template.getPhysicalPath(), str26, this.str_1500), string.Concat(this.objNF3Template.getPhysicalPath(), str4, this.str_1500.Replace(".pdf", "_MER.pdf")));
                        str1 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str4 + this.str_1500.Replace(".pdf", "_MER.pdf");
                        ArrayList arrayLists2 = new ArrayList();
                        if (str3 != "OLD")
                        {
                            arrayLists2.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            arrayLists2.Add(string.Concat(str4, this.str_1500.Replace(".pdf", "_MER.pdf")));
                            arrayLists2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            arrayLists2.Add(this.Session["TM_SZ_CASE_ID"]);
                            arrayLists2.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                            arrayLists2.Add(str4);
                            arrayLists2.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            arrayLists2.Add(group);
                            arrayLists2.Add("NF");
                            arrayLists2.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            arrayLists2.Add(nodeType[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(arrayLists2);
                        }
                        else
                        {
                            arrayLists2.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            arrayLists2.Add(string.Concat(str4, this.str_1500.Replace(".pdf", "_MER.pdf")));
                            arrayLists2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            arrayLists2.Add(this.Session["TM_SZ_CASE_ID"]);
                            arrayLists2.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                            arrayLists2.Add(str4);
                            arrayLists2.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            arrayLists2.Add(group);
                            arrayLists2.Add("NF");
                            arrayLists2.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(arrayLists2);
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
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str1, "');"), true);
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
                if (e.Item.Cells[9].Text == "1")
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Please first add services to bill!'); ", true);
                }
                else
                {
                    this.Session["PassedBillID"] = e.CommandArgument;
                    this.Session["Balance"] = e.Item.Cells[7].Text;
                    base.Response.Redirect("Bill_Sys_PaymentTransactions.aspx", false);
                }
            }
            this.lblMsg.Visible = false;
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str2, MethodBase.GetCurrentMethod());
            }
            string str29 = string.Concat("Error Request=", str2, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str29));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str2, MethodBase.GetCurrentMethod());
        }
    }

    protected void grdLatestBillTransaction_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
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
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void grdLatestBillTransaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
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
            if (this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[17].Text != "&nbsp;")
            {
                this.extddlspeciality.Text=this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[17].Text;
            }
            Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = billSysAssociateDiagnosisCodeBO.GetBillDiagnosisCode(this.Session["BillID"].ToString()).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.btnAddService.Enabled = true;
            this.grdTransactionDetails.Visible = true;
            this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
            Bill_Sys_BillTransaction_BO billSysBillTransactionBO = new Bill_Sys_BillTransaction_BO();
            if (this.extddlspeciality.Text != "NA")
            {
                if (!billSysBillTransactionBO.checkUnitRefferal(this.extddlspeciality.Text, this.txtCompanyID.Text))
                {
                    this.grdTransactionDetails.Columns[8].Visible = false;
                }
                else
                {
                    this.grdTransactionDetails.Columns[8].Visible = true;
                }
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void grdTransactionDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            this.grdTransactionDetails.CurrentPageIndex = e.NewPageIndex;
            if (this.Session["SELECTED_DIA_PRO_CODE"] == null)
            {
                this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
            }
            else
            {
                DataTable dataTable = new DataTable();
                dataTable = (DataTable)this.Session["SELECTED_DIA_PRO_CODE"];
                this.grdTransactionDetails.DataSource = dataTable;
                this.grdTransactionDetails.DataBind();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT != "1")
                {
                    this.grdTransactionDetails.Columns[6].Visible = true;
                    this.grdTransactionDetails.Columns[7].Visible = false;
                }
                else
                {
                    this.grdTransactionDetails.Columns[6].Visible = false;
                    this.grdTransactionDetails.Columns[7].Visible = true;
                }
            }
        }
        catch
        {
        }
    }

    protected void grdTransactionDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtTransDetailID.Text = this.grdTransactionDetails.Items[this.grdTransactionDetails.SelectedIndex].Cells[1].Text;
            this.txtDateOfservice.Text = this.grdTransactionDetails.Items[this.grdTransactionDetails.SelectedIndex].Cells[3].Text;
            this.txtAmount.Text = this.grdTransactionDetails.Items[this.grdTransactionDetails.SelectedIndex].Cells[9].Text;
            this.btnAddService.Enabled = false;
            this.lblMsg.Visible = false;
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    

    protected void lnkbtnRemoveDiag_Click(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
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
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
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
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["TEMPLATE_BILL_NO"] = this.Session["BillID"].ToString();
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_2.aspx'); ", true);
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!base.IsPostBack && base.Request.QueryString["Type"] == null)
            {
                this.Session["SZ_BILL_NUMBER"] = null;
            }
            this.ErrorDiv.InnerText = "";
            this.ErrorDiv.Style.Value = "color: red";
            if (this.Session["CASE_OBJECT"] == null)
            {
                base.Response.Redirect("Bill_Sys_SearchCase.aspx", false);
            }
            else
            {
                this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this.txtCaseNo.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                Bill_Sys_Case billSysCase = new Bill_Sys_Case();
                billSysCase.SZ_CASE_ID=this.txtCaseID.Text;
                this.Session["CASEINFO"] = billSysCase;
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
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.extddlDoctor.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            else
            {
                this.extddlDoctor.Procedure_Name="SP_MST_BILLING_DOCTOR";
                this.extddlDoctor.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlReadingDoctor.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
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
                DataTable dataTable = new DataTable();
                dataTable = (DataTable)this.Session["SELECTED_DIA_PRO_CODE"];
                this.grdTransactionDetails.DataSource = dataTable;
                this.grdTransactionDetails.DataBind();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT != "1")
                {
                    this.grdTransactionDetails.Columns[6].Visible = true;
                    this.grdTransactionDetails.Columns[7].Visible = false;
                }
                else
                {
                    this.grdTransactionDetails.Columns[6].Visible = false;
                    this.grdTransactionDetails.Columns[7].Visible = true;
                }
                this.Session["SELECTED_DIA_PRO_CODE"] = null;
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            foreach (DataGridItem item in this.grdLatestBillTransaction.Items)
            {
                if (this.objCaseDetailsBO.GetCaseType(item.Cells[1].Text) == "WC000000000000000001")
                {
                    continue;
                }
                item.Cells[11].Text = "";
                item.Cells[12].Text = "";
                item.Cells[13].Text = "";
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_ReferralBillTransaction.aspx");
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.Session["SZ_BILL_ID"] = null;
        this.Session["PassedCaseID"] = null;
    }

    protected void rdoFromTo_CheckedChanged(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtDateOfServiceTo.Visible = true;
            this.lblDateOfService.Visible = true;
            this.lblTo.Visible = true;
            this.imgbtnFromTo.Visible = true;
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void rdoOn_CheckedChanged(object sender, EventArgs e)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtDateOfServiceTo.Visible = false;
            this.lblDateOfService.Visible = false;
            this.lblTo.Visible = false;
            this.imgbtnFromTo.Visible = false;
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void SaveTransactionData()
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            if (!this.txtDateOfServiceTo.Visible)
            {
                bool visible = this.txtDateOfServiceTo.Visible;
            }
            else
            {
                DateTime dateTime = Convert.ToDateTime(this.txtDateOfServiceTo.Text);
                TimeSpan timeSpan = dateTime.Subtract(Convert.ToDateTime(this.txtDateOfservice.Text));
                int days = timeSpan.Days;
                DateTime dateTime1 = Convert.ToDateTime(this.txtDateOfservice.Text);
                dateTime1.AddDays(1);
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void SetDataOfAssociate(string setID)
    {
        try
        {
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataSet dataSet = new DataSet();
            dataSet = this._bill_Sys_BillTransaction.GetAssociatedEntry(setID);
            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                this.extddlDoctor.Text=dataSet.Tables[0].Rows[i][1].ToString();
            }
            this.grdTransactionDetails.Visible = true;
            this.extddlDoctor.Enabled = false;
            this.btnAddService.Enabled = false;
            this.btnClearService.Enabled = false;
            this.Session["AssociateDiagnosis"] = true;
        }
        catch
        {
        }
    }

    public void setDefaultSettings(string p_szBillID)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["BillID"] = p_szBillID;
            this.Session["SZ_BILL_NUMBER"] = this.Session["BillID"].ToString();
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = billSysAssociateDiagnosisCodeBO.GetBillDiagnosisCode(this.Session["BillID"].ToString()).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.grdTransactionDetails.Visible = true;
            this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    protected void setDefaultValues(string p_szBillID)
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = billSysAssociateDiagnosisCodeBO.GetBillDiagnosisCode(p_szBillID).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.btnAddService.Enabled = true;
            this.grdTransactionDetails.Visible = true;
            this.BindTransactionDetailsGrid(p_szBillID);
            ArrayList arrayLists = new ArrayList();
            arrayLists = this._bill_Sys_BillTransaction.GetBillInfo(p_szBillID);
            if (arrayLists != null)
            {
                this.extddlDoctor.Text=arrayLists[0].ToString();
                this.extddlReadingDoctor.Text=arrayLists[1].ToString();
            }
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }

    private void UpdateTransactionDetails()
    {
        string str = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils util = new Utils())
        {
            util.MethodStart(str, MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            ArrayList arrayLists = new ArrayList();
        }
        catch (Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
            using (Utils util1 = new Utils())
            {
                util1.MethodEnd(str, MethodBase.GetCurrentMethod());
            }
            string str1 = string.Concat("Error Request=", str, ".Please share with Technical support.");
            base.Response.Redirect(string.Concat("../Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
        }
        using (Utils util2 = new Utils())
        {
            util2.MethodEnd(str, MethodBase.GetCurrentMethod());
        }
    }
}