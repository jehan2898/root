using ASP;
using Componend;
using CUTEFORMCOLib;
using ExtendedDropDownList;
using GeneratePDFFile;
using log4net;
using mbs.LienBills;
using PDFValueReplacement;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Bill_Sys_PopUpBillTransactionAllVisit : Page, IRequiresSessionState
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

    private Bill_Sys_DigosisCodeBO objDiagCodeBO;

    private DAO_NOTES_EO _DAO_NOTES_EO;

    private DAO_NOTES_BO _DAO_NOTES_BO;

    private Bill_Sys_DigosisCodeBO _digosisCodeBO;

    private static ILog log;

    private Bill_Sys_LoginBO _bill_Sys_LoginBO;

    private string bt_include;

    private string str_1500;

    private MUVGenerateFunction _MUVGenerateFunction;

 

    static Bill_Sys_PopUpBillTransactionAllVisit()
    {
        Bill_Sys_PopUpBillTransactionAllVisit.log = LogManager.GetLogger("Bill_Sys_PopUpBillTransaction");
    }

    public Bill_Sys_PopUpBillTransactionAllVisit()
    {
    }

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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void BindCompleteVisit()
    {
        string str = base.Request.QueryString["DID"].ToString();
        string str1 = base.Request.QueryString["P_CASE_ID"].ToString();
        DataSet dataSet = new DataSet();
        Bill_Sys_Visit_BO billSysVisitBO = new Bill_Sys_Visit_BO();
        dataSet = billSysVisitBO.GetCompletedVisitListAllVisit(str1.ToString(), this.txtCompanyID.Text, str.ToString());
        this.grdCompleteVisit.DataSource = dataSet.Tables[0];
        this.grdCompleteVisit.DataBind();
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Visit_BO billSysVisitBO = new Bill_Sys_Visit_BO();
        ArrayList arrayLists = new ArrayList();
        try
        {
            arrayLists.Add(this.txtCompanyID.Text);
            arrayLists.Add(this.extddlDoctor.Text);
            arrayLists.Add("");
            arrayLists.Add("");
            arrayLists.Add("1");
            arrayLists.Add(this.txtCaseID.Text);
            arrayLists.Add("");
            this.grdAllReports.DataSource = billSysVisitBO.VisitReport(arrayLists);
            this.grdAllReports.DataBind();
            this.cheks();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        this._bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {
            ArrayList arrayLists = new ArrayList();
            arrayLists = this._bill_Sys_BillingCompanyDetails_BO.GetIC9CodeData(id);
            if (arrayLists.Count > 0)
            {
                this.txtTransDetailID.Text = arrayLists[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindTransactionData(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            this.grdTransactionDetails.DataSource = this._bill_Sys_BillTransaction.BindTransactionData(id);
            this.grdTransactionDetails.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            this.grdTransactionDetails.DataSource = this._bill_Sys_BillTransaction.BindTransactionData(billnumber);
            this.grdTransactionDetails.DataBind();
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (!this._bill_Sys_BillTransaction.checkUnit(this.extddlDoctor.Text, this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
                for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                {
                    TextBox text = (TextBox)this.grdTransactionDetails.Items[i].FindControl("txtUnit");
                    text.Text = this.grdTransactionDetails.Items[i].Cells[8].Text;
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            this.txtBillDate.Text = "";
            this.extddlDoctor.Text = "NA";
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
                if (((CheckBox)this.grdDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode")).Checked)
                {
                    ListItem listItem = new ListItem(string.Concat(this.grdDiagonosisCode.Items[i].Cells[2].Text, '-', this.grdDiagonosisCode.Items[i].Cells[4].Text), this.grdDiagonosisCode.Items[i].Cells[1].Text);
                    if (!this.lstDiagnosisCodes.Items.Contains(listItem))
                    {
                        this.lstDiagnosisCodes.Items.Add(listItem);
                    }
                }
            }
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                {
                    if (this.grdTransactionDetails.Items[i].Cells[0].Text != "" && this.grdTransactionDetails.Items[i].Cells[0].Text != "&nbsp;")
                    {
                        if (((CheckBox)this.grdTransactionDetails.Items[i].FindControl("chkSelect")).Checked)
                        {
                            this._bill_Sys_BillTransaction.DeleteTransactionDetails(this.grdTransactionDetails.Items[i].Cells[0].Text);
                            this.grdTransactionDetails.Items[i].Cells.Clear();
                        }
                    }
                    else if (((CheckBox)this.grdTransactionDetails.Items[i].FindControl("chkSelect")).Checked)
                    {
                        this.grdTransactionDetails.Items[i].Cells.Clear();
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
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
            string text = "";
            if (this.extddlDoctor.Text == "NA")
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = " Select doctor ... ! ";
            }
            else
            {
                BillTransactionEO billTransactionEO = new BillTransactionEO()
                {
                    SZ_CASE_ID = this.txtCaseID.Text,
                    SZ_COMPANY_ID = this.txtCompanyID.Text,
                    DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text),
                    SZ_DOCTOR_ID = this.extddlDoctor.Text,
                    SZ_TYPE = this.ddlType.Text,
                    SZ_TESTTYPE = "",
                    FLAG = "ADD",
                    SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID
                };
                this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                ArrayList arrayLists = new ArrayList();
                ArrayList arrayLists1 = new ArrayList();
                Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
                foreach (DataGridItem item in this.grdAllReports.Items)
                {
                    if (!((CheckBox)item.Cells[0].FindControl("chkselect")).Checked)
                    {
                        continue;
                    }
                    EventEO eventEO = new EventEO()
                    {
                        I_EVENT_ID = item.Cells[4].Text,
                        BT_STATUS = "1",
                        I_STATUS = "2",
                        SZ_BILL_NUMBER = "",
                        DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text)
                    };
                    arrayLists.Add(eventEO);
                    foreach (DataGridItem dataGridItem in this.grdTransactionDetails.Items)
                    {
                        if (item.Cells[2].Text != dataGridItem.Cells[1].Text)
                        {
                            continue;
                        }
                        EventRefferProcedureEO eventRefferProcedureEO = new EventRefferProcedureEO()
                        {
                            SZ_PROC_CODE = dataGridItem.Cells[11].Text,
                            I_EVENT_ID = item.Cells[4].Text,
                            I_STATUS = "2"
                        };
                        arrayLists1.Add(eventRefferProcedureEO);
                    }
                }
                this.objCaseDetailsBO = new CaseDetailsBO();
                ArrayList arrayLists2 = new ArrayList();
                foreach (DataGridItem item1 in this.grdTransactionDetails.Items)
                {
                    if (!(item1.Cells[1].Text.ToString() != "") || !(item1.Cells[1].Text.ToString() != "&nbsp;") || !(item1.Cells[3].Text.ToString() != "") || !(item1.Cells[3].Text.ToString() != "&nbsp;") || !(item1.Cells[4].Text.ToString() != "") || !(item1.Cells[4].Text.ToString() != "&nbsp;"))
                    {
                        continue;
                    }
                    BillProcedureCodeEO billProcedureCodeEO = new BillProcedureCodeEO();
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    billProcedureCodeEO.SZ_PROCEDURE_ID = item1.Cells[2].Text.ToString();
                    if (item1.Cells[6].Text.ToString() == "&nbsp;")
                    {
                        billProcedureCodeEO.FL_AMOUNT = "0";
                    }
                    else
                    {
                        billProcedureCodeEO.FL_AMOUNT = item1.Cells[6].Text.ToString();
                    }
                    billProcedureCodeEO.SZ_BILL_NUMBER = "";
                    billProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(item1.Cells[1].Text.ToString());
                    billProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    billProcedureCodeEO.I_UNIT = ((TextBox)item1.Cells[7].FindControl("txtUnit")).Text.ToString();
                    billProcedureCodeEO.FLT_PRICE = ((Label)item1.Cells[5].FindControl("lblPrice")).Text;
                    billProcedureCodeEO.DOCT_AMOUNT = ((Label)item1.Cells[5].FindControl("lblPrice")).Text;
                    billProcedureCodeEO.FLT_FACTOR = ((Label)item1.Cells[5].FindControl("lblFactor")).Text;
                    if (!(item1.Cells[8].Text.ToString() != "&nbsp;") || !(item1.Cells[8].Text.ToString() != ""))
                    {
                        billProcedureCodeEO.PROC_AMOUNT = "0";
                    }
                    else
                    {
                        billProcedureCodeEO.PROC_AMOUNT = item1.Cells[8].Text.ToString();
                    }
                    billProcedureCodeEO.SZ_DOCTOR_ID = this.extddlDoctor.Text;
                    billProcedureCodeEO.SZ_CASE_ID = this.txtCaseID.Text;
                    billProcedureCodeEO.SZ_TYPE_CODE_ID = item1.Cells[11].Text.ToString();
                    billProcedureCodeEO.FLT_GROUP_AMOUNT = item1.Cells[12].Text.ToString();
                    billProcedureCodeEO.I_GROUP_AMOUNT_ID = item1.Cells[13].Text.ToString();
                    arrayLists2.Add(billProcedureCodeEO);
                }
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                ArrayList arrayLists3 = new ArrayList();
                foreach (ListItem listItem in this.lstDiagnosisCodes.Items)
                {
                    BillDiagnosisCodeEO billDiagnosisCodeEO = new BillDiagnosisCodeEO();
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    billDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID = listItem.Value.ToString();
                    arrayLists3.Add(billDiagnosisCodeEO);
                }
                BillTransactionDAO billTransactionDAO = new BillTransactionDAO();
                Result result = new Result();
                result = billTransactionDAO.SaveBillTransaction(billTransactionEO, arrayLists, arrayLists1, arrayLists2, arrayLists3);
                if (result.msg_code != "ERR")
                {
                    this.txtBillID.Text = result.bill_no;
                    text = this.txtBillID.Text;
                    this._DAO_NOTES_EO = new DAO_NOTES_EO()
                    {
                        SZ_MESSAGE_TITLE = "BILL_CREATED",
                        SZ_ACTIVITY_DESC = text
                    };
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    string patientID = this.objCaseDetailsBO.GetPatientID(text);
                    if (this.objCaseDetailsBO.GetCaseType(text) == "WC000000000000000001")
                    {
                        this.objDefaultValue = new Bill_Sys_InsertDefaultValues();
                        if (this.grdLatestBillTransaction.Items.Count == 0)
                        {
                            this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("../Config\\DV_DoctorOpinion.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                            this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("../Config\\DV_ExamInformation.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                            this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("../Config\\DV_History.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                            this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("../Config\\DV_PlanOfCare.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                            this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("../Config\\DV_WorkStatus.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        }
                        else if (this.grdLatestBillTransaction.Items.Count >= 1)
                        {
                            this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("../Config\\DV_DoctorsOpinionC4_2.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                            this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("../Config\\DV_ExaminationTreatment.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                            this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("../Config\\DV_PermanentImpairment.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                            this.objDefaultValue.InsertDefaultValues(base.Server.MapPath("../Config\\DV_WorkStatusC4_2.xml"), this.txtCompanyID.Text.ToString(), text, patientID);
                        }
                    }
                    this.ClearControl();
                    this.lblMsg.Visible = true;
                    this.lblMsg.Text = " Bill Saved successfully ! ";
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    if (this.objCaseDetailsBO.GetCaseType(text) == "WC000000000000000002")
                    {
                        this.GenerateAddedBillPDF(text, this._bill_Sys_BillTransaction.GetDoctorSpeciality(text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                    }
                    if (this.objCaseDetailsBO.GetCaseType(text) == "WC000000000000000003")
                    {
                        this.GenerateAddedBillPDF(text, this._bill_Sys_BillTransaction.GetDoctorSpeciality(text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                    }
                    else if (!(this.objCaseDetailsBO.GetCaseType(text) == "WC000000000000000001") && this.objCaseDetailsBO.GetCaseType(text) == "WC000000000000000004")
                    {
                        this.GenerateAddedBillPDF(text, this._bill_Sys_BillTransaction.GetDoctorSpeciality(text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                    }
                }
                else
                {
                    this.lblMsg.Text = result.msg;
                    this.lblMsg.Visible = true;
                }
                this.BindCompleteVisit();
                this.BindGrid();
                this.extddlDoctor.Text = this.Session["TEMP_DOCTOR_ID"].ToString();
                this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                this.Session["SELECTED_PROC_CODE"] = base.Request.QueryString["PROC_GROUP_ID"].ToString();
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
        if (this.extddlDoctor.Text != "NA")
        {
            this.ErrorDiv.InnerText = " Select Doctor ...!";
        }
        else
        {
            this._editOperation = new EditOperation();
            try
            {
                if (this.Session["SZ_BILL_NUMBER"] == null)
                {
                    this._editOperation.Primary_Value = this.Session["BillID"].ToString();
                    this._editOperation.WebPage = this.Page;
                    this._editOperation.Xml_File = "BillTransaction.xml";
                    this._editOperation.UpdateMethod();
                    this.lblMsg.Visible = true;
                    this.lblMsg.Text = " Bill Updated successfully ! ";
                }
                else
                {
                    this._editOperation.Primary_Value = this.Session["SZ_BILL_NUMBER"].ToString();
                    this._editOperation.WebPage = this.Page;
                    this._editOperation.Xml_File = "BillTransaction.xml";
                    this._editOperation.UpdateMethod();
                    this.Session["SZ_BILL_NUMBER"] = null;
                    base.Response.Redirect("Bill_Sys_BillSearch.aspx", false);
                }
            }
            catch (SqlException sqlException)
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
                string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
            //Method End
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
    }

    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList arrayLists;
        this._editOperation = new EditOperation();
        try
        {
            if (this.extddlDoctor.Text == "NA")
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = " Select doctor ... ! ";
            }
            else
            {
                this.Session["BillID"] = this.Session["SZ_BILL_NUMBER"].ToString();
                if (this.Session["BillID"] != null)
                {
                    this._editOperation.Primary_Value = this.Session["BillID"].ToString();
                    this._editOperation.WebPage = this.Page;
                    this._editOperation.Xml_File = "BillTransaction.xml";
                    this._editOperation.UpdateMethod();
                    this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                    string str = this.Session["BillID"].ToString();
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
                        if (item.Cells[6].Text.ToString() == "&nbsp;")
                        {
                            arrayLists.Add(0);
                        }
                        else
                        {
                            arrayLists.Add(item.Cells[6].Text.ToString());
                        }
                        arrayLists.Add(str);
                        if (!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;"))
                        {
                            arrayLists.Add("");
                        }
                        else
                        {
                            arrayLists.Add(Convert.ToDateTime(item.Cells[1].Text.ToString()));
                        }
                        arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        arrayLists.Add(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString());
                        arrayLists.Add(((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString());
                        arrayLists.Add(((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString());
                        arrayLists.Add(((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString());
                        arrayLists.Add(item.Cells[8].Text.ToString());
                        if (!(item.Cells[0].Text.ToString() != "") || !(item.Cells[0].Text.ToString() != "&nbsp;"))
                        {
                            arrayLists.Add(this.extddlDoctor.Text);
                            arrayLists.Add(this.txtCaseID.Text);
                            arrayLists.Add(item.Cells[12].Text.ToString());
                            this._bill_Sys_BillTransaction.SaveTransactionData(arrayLists);
                        }
                        else
                        {
                            arrayLists.Add(item.Cells[12].Text.ToString());
                            this._bill_Sys_BillTransaction.UpdateTransactionData(arrayLists);
                        }
                    }
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    this._bill_Sys_BillTransaction.DeleteTransactionDiagnosis(str);
                    foreach (ListItem listItem in this.lstDiagnosisCodes.Items)
                    {
                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        arrayLists = new ArrayList();
                        arrayLists.Add(listItem.Value.ToString());
                        arrayLists.Add(str);
                        this._bill_Sys_BillTransaction.SaveTransactionDiagnosis(arrayLists);
                    }
                    this.lblMsg.Visible = true;
                    this._DAO_NOTES_EO = new DAO_NOTES_EO()
                    {
                        SZ_MESSAGE_TITLE = "BILL_UPDATED",
                        SZ_ACTIVITY_DESC = str
                    };
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    this.lblMsg.Text = " Bill Updated successfully ! ";
                }
                this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
            }
        }
        catch (SqlException sqlException)
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataRow str;
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
            dataTable.Columns.Add("FLT_GROUP_AMOUNT");
            dataTable.Columns.Add("I_GROUP_AMOUNT_ID");
            dataTable.Columns.Add("SZ_VISIT_TYPE");
            dataTable.Columns.Add("BT_IS_LIMITE");
            foreach (DataGridItem item in this.grdTransactionDetails.Items)
            {
                if (!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;") || !(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;") || !(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;"))
                {
                    continue;
                }
                str = dataTable.NewRow();
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
                str["FLT_AMOUNT"] = item.Cells[6].Text.ToString();
                if (((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "" && Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0)
                {
                    str["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
                }
                str["PROC_AMOUNT"] = item.Cells[9].Text.ToString();
                str["DOCT_AMOUNT"] = item.Cells[10].Text.ToString();
                str["SZ_TYPE_CODE_ID"] = item.Cells[11].Text.ToString();
                str["I_GROUP_AMOUNT_ID"] = item.Cells[13].Text.ToString();
                str["SZ_VISIT_TYPE"] = item.Cells[15].Text.ToString();
                str["BT_IS_LIMITE"] = item.Cells[16].Text.ToString();
                BillTransactionDAO billTransactionDAO = new BillTransactionDAO();
                string str1 = item.Cells[2].Text.ToString();
                string str2 = item.Cells[15].Text.ToString();
                string procID = billTransactionDAO.GetProcID(this.txtCompanyID.Text, str1);
                if (billTransactionDAO.GetLimit(this.txtCompanyID.Text, str2, procID) == "")
                {
                    str["FLT_GROUP_AMOUNT"] = item.Cells[12].Text.ToString();
                }
                else
                {
                    str["FLT_GROUP_AMOUNT"] = "";
                }
                dataTable.Rows.Add(str);
            }
            string[] strArrays = this.txtDateOfservice.Text.Split(new char[] { ',' });
            this._bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            if (this._bill_Sys_LoginBO.getDefaultSettings(this.txtCompanyID.Text, "SS00005") != "1")
            {
                string[] strArrays1 = strArrays;
                for (int i = 0; i < (int)strArrays1.Length; i++)
                {
                    object obj = strArrays1[i];
                    if (obj.ToString() != "")
                    {
                        foreach (DataGridItem dataGridItem in this.grdProcedure.Items)
                        {
                            if (!((CheckBox)dataGridItem.FindControl("chkselect")).Checked)
                            {
                                continue;
                            }
                            str = dataTable.NewRow();
                            str["SZ_BILL_TXN_DETAIL_ID"] = "";
                            str["DT_DATE_OF_SERVICE"] = obj.ToString();
                            str["SZ_PROCEDURE_ID"] = dataGridItem.Cells[1].Text.ToString();
                            str["SZ_PROCEDURAL_CODE"] = dataGridItem.Cells[3].Text.ToString();
                            str["SZ_CODE_DESCRIPTION"] = dataGridItem.Cells[4].Text.ToString();
                            str["FACTOR_AMOUNT"] = dataGridItem.Cells[5].Text.ToString();
                            str["FACTOR"] = "1";
                            str["FLT_AMOUNT"] = dataGridItem.Cells[5].Text.ToString();
                            str["I_UNIT"] = "1";
                            str["PROC_AMOUNT"] = dataGridItem.Cells[5].Text.ToString();
                            str["DOCT_AMOUNT"] = dataGridItem.Cells[5].Text.ToString();
                            str["SZ_TYPE_CODE_ID"] = dataGridItem.Cells[2].Text.ToString();
                            dataTable.Rows.Add(str);
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridItem item1 in this.grdAllReports.Items)
                {
                    CheckBox checkBox = (CheckBox)item1.Cells[0].FindControl("chkSelect");
                    if (!((CheckBox)item1.Cells[0].FindControl("chkSelect")).Checked)
                    {
                        continue;
                    }
                    foreach (DataGridItem dataGridItem1 in this.grdProcedure.Items)
                    {
                        CheckBox checkBox1 = (CheckBox)dataGridItem1.FindControl("chkselect");
                        string str3 = item1.Cells[5].Text.ToString();
                        if (!checkBox1.Checked)
                        {
                            continue;
                        }
                        str = dataTable.NewRow();
                        str["SZ_BILL_TXN_DETAIL_ID"] = "";
                        DateTime dateTime1 = Convert.ToDateTime(item1.Cells[2].Text.ToString());
                        str["DT_DATE_OF_SERVICE"] = dateTime1.ToShortDateString();
                        str["SZ_PROCEDURE_ID"] = dataGridItem1.Cells[1].Text.ToString();
                        str["SZ_PROCEDURAL_CODE"] = dataGridItem1.Cells[3].Text.ToString();
                        str["SZ_CODE_DESCRIPTION"] = dataGridItem1.Cells[4].Text.ToString();
                        str["FACTOR_AMOUNT"] = dataGridItem1.Cells[5].Text.ToString();
                        str["FACTOR"] = "1";
                        str["FLT_AMOUNT"] = dataGridItem1.Cells[5].Text.ToString();
                        str["I_UNIT"] = "1";
                        str["PROC_AMOUNT"] = dataGridItem1.Cells[5].Text.ToString();
                        str["DOCT_AMOUNT"] = dataGridItem1.Cells[5].Text.ToString();
                        str["SZ_TYPE_CODE_ID"] = dataGridItem1.Cells[2].Text.ToString();
                        str["SZ_VISIT_TYPE"] = str3;
                        str["BT_IS_LIMITE"] = "1";
                        dataTable.Rows.Add(str);
                    }
                }
            }
            DataView dataViews = new DataView();
            dataTable.DefaultView.Sort = "DT_DATE_OF_SERVICE";
            double num = 0;
            this.grdTransactionDetails.DataSource = dataTable;
            this.grdTransactionDetails.DataBind();
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO billTransactionDAO1 = new BillTransactionDAO();
                string str4 = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                this.grdTransactionDetails.Items[0].Cells[15].Text.ToString();
                string procID1 = billTransactionDAO1.GetProcID(this.txtCompanyID.Text, str4);
                string sLIMITE = billTransactionDAO1.GET_IS_LIMITE(this.txtCompanyID.Text, procID1);
                if (sLIMITE != "" && sLIMITE != "NULL")
                {
                    for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                    {
                        if (this.grdTransactionDetails.Items[j].Cells[6].Text != "" && this.grdTransactionDetails.Items[j].Cells[6].Text != "&nbsp;")
                        {
                            num = num + Convert.ToDouble(this.grdTransactionDetails.Items[j].Cells[6].Text);
                        }
                        if (j == this.grdTransactionDetails.Items.Count - 1)
                        {
                            BillTransactionDAO billTransactionDAO2 = new BillTransactionDAO();
                            string str5 = this.grdTransactionDetails.Items[j].Cells[2].Text.ToString();
                            string str6 = this.grdTransactionDetails.Items[j].Cells[15].Text.ToString();
                            string procID2 = billTransactionDAO2.GetProcID(this.txtCompanyID.Text, str5);
                            string limit = billTransactionDAO2.GetLimit(this.txtCompanyID.Text, str6, procID2);
                            if (limit != "")
                            {
                                if (Convert.ToDouble(limit) >= num)
                                {
                                    this.grdTransactionDetails.Items[j].Cells[12].Text = num.ToString();
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[j].Cells[12].Text = limit;
                                }
                            }
                            num = 0;
                        }
                        else if (this.grdTransactionDetails.Items[j].Cells[1].Text != this.grdTransactionDetails.Items[j + 1].Cells[1].Text)
                        {
                            BillTransactionDAO billTransactionDAO3 = new BillTransactionDAO();
                            string str7 = this.grdTransactionDetails.Items[j].Cells[2].Text.ToString();
                            string str8 = this.grdTransactionDetails.Items[j].Cells[15].Text.ToString();
                            string procID3 = billTransactionDAO3.GetProcID(this.txtCompanyID.Text, str7);
                            string limit1 = billTransactionDAO3.GetLimit(this.txtCompanyID.Text, str8, procID3);
                            if (limit1 != "")
                            {
                                if (Convert.ToDouble(limit1) >= num)
                                {
                                    this.grdTransactionDetails.Items[j].Cells[12].Text = num.ToString();
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[j].Cells[12].Text = limit1;
                                }
                            }
                            num = 0;
                        }
                    }
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (!this._bill_Sys_BillTransaction.checkUnit(this.extddlDoctor.Text, this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
                for (int k = 0; k < this.grdTransactionDetails.Items.Count; k++)
                {
                    TextBox text = (TextBox)this.grdTransactionDetails.Items[k].FindControl("txtUnit");
                    text.Text = this.grdTransactionDetails.Items[k].Cells[8].Text;
                }
            }
            ScriptManager.RegisterStartupScript(this, base.GetType(), "closeScript", "CloseServiceSource();", true);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
    }

    private void CalculateAmount(string id)
    {
        string elmahid = string.Format("elmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
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
                utility.MethodEnd(elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + elmahid + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void cheks()
    {
        if (this.grdCompleteVisit.Items.Count > 0)
        {
            for (int i = 0; i < this.grdCompleteVisit.Items.Count; i++)
            {
                if (((CheckBox)this.grdCompleteVisit.Items[i].FindControl("chkSelectItem")).Checked)
                {
                    CheckBox checkBox = (CheckBox)this.grdAllReports.Items[i].FindControl("chkSelect");
                    checkBox.Checked = true;
                }
            }
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
            this.extddlDoctor.Text = "NA";
            this.txtBillDate.Text = "";
            this.grdTransactionDetails.DataSource = null;
            this.grdTransactionDetails.DataBind();
            this.Session["SELECTED_DIA_PRO_CODE"] = null;
            this.Session["SZ_BILL_NUMBER"] = null;
            this.lstDiagnosisCodes.DataSource = null;
            this.lstDiagnosisCodes.DataBind();
            this.lblMsg.Visible = false;
            this.btnSave.Enabled = true;
            this.btnUpdate.Enabled = false;
            this.lstDiagnosisCodes.Items.Clear();
            this.lstDiagnosisCodes.DataSource = null;
            this.lstDiagnosisCodes.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
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
        dataTable.Columns.Add("SZ_VISIT_TYPE");
        dataTable.Columns.Add("BT_IS_LIMITE");
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
            str["FLT_AMOUNT"] = item.Cells[6].Text.ToString();
            if (((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "" && Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0)
            {
                str["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
            }
            str["PROC_AMOUNT"] = item.Cells[9].Text.ToString();
            str["DOCT_AMOUNT"] = item.Cells[10].Text.ToString();
            str["SZ_VISIT_TYPE"] = item.Cells[15].Text.ToString();
            str["BT_IS_LIMITE"] = item.Cells[16].Text.ToString();
            dataTable.Rows.Add(str);
        }
        this.Session["SELECTED_SERVICES"] = dataTable;
    }

    protected void extddlDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.extddlDoctor.Text != "NA")
        {
            this.GetProcedureCode(this.extddlDoctor.Text);
            this.Session["TEMP_DOCTOR_ID"] = this.extddlDoctor.Text;
            this._bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            if (this._bill_Sys_LoginBO.getDefaultSettings(this.txtCompanyID.Text, "SS00005") != "1")
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
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    if (!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;") || !(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;") || !(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;"))
                    {
                        continue;
                    }
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
                    str["FLT_AMOUNT"] = item.Cells[6].Text.ToString();
                    if (((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "" && Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0)
                    {
                        str["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
                    }
                    str["PROC_AMOUNT"] = item.Cells[9].Text.ToString();
                    str["DOCT_AMOUNT"] = item.Cells[10].Text.ToString();
                    str["SZ_TYPE_CODE_ID"] = item.Cells[12].Text.ToString();
                    dataTable.Rows.Add(str);
                }
                this.grdTransactionDetails.DataSource = dataTable;
                this.grdTransactionDetails.DataBind();
            }
            else
            {
                this.BindGrid();
                this.lblDateOfService.Style.Add("visibility", "hidden");
                this.txtDateOfservice.Style.Add("visibility", "hidden");
                this.Image1.Style.Add("visibility", "hidden");
                this.lblGroupServiceDate.Style.Add("visibility", "hidden");
                this.txtGroupDateofService.Style.Add("visibility", "hidden");
                this.imgbtnDateofService.Style.Add("visibility", "hidden");
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (!this._bill_Sys_BillTransaction.checkUnit(this.extddlDoctor.Text, this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
            }
            Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = billSysAssociateDiagnosisCodeBO.GetCaseDiagnosisCode(this.txtCaseID.Text, this.extddlDoctor.Text, this.txtCompanyID.Text).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
        }
    }

    protected void extddlProcedureCode_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    }

    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string sZCOMPANYID;
        string sZCOMPANYNAME;
        string sZCOMAPNYID;
        string str;
        try
        {
            this._MUVGenerateFunction = new MUVGenerateFunction();
            string pSzSpeciality = p_szSpeciality;
            this.Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this.Session["TM_SZ_BILL_ID"] = p_szBillNumber;
            this.objNF3Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Verification_Desc billSysVerificationDesc = new Bill_Sys_Verification_Desc()
            {
                sz_bill_no = p_szBillNumber,
                sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID,
                sz_flag = "BILL"
            };
            ArrayList arrayLists = new ArrayList();
            ArrayList nodeType = new ArrayList();
            string str1 = "";
            string str2 = "";
            arrayLists.Add(billSysVerificationDesc);
            nodeType = this._bill_Sys_BillTransaction.Get_Node_Type(arrayLists);
            if (!nodeType.Contains("NFVER"))
            {
                str1 = "NEW";
                string[] strArrays = new string[] { ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Medicals/", pSzSpeciality, "/Bills/" };
                str2 = string.Concat(strArrays);
            }
            else
            {
                str1 = "OLD";
                string[] sZCOMPANYNAME1 = new string[] { ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Bills/", pSzSpeciality, "/" };
                str2 = string.Concat(sZCOMPANYNAME1);
            }
            CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
            string sZCOMPANYID1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                string str3 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                string str4 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                string str5 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                Bill_Sys_Configuration billSysConfiguration = new Bill_Sys_Configuration();
                string configurationSettings = billSysConfiguration.getConfigurationSettings(sZCOMPANYID1, "GET_DIAG_PAGE_POSITION");
                string configurationSettings1 = billSysConfiguration.getConfigurationSettings(sZCOMPANYID1, "DIAG_PAGE");
                string str6 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                string str7 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                string str8 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                string str9 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                GenerateNF3PDF generateNF3PDF = new GenerateNF3PDF();
                this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                string str10 = generateNF3PDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str5);
                Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill Details PDF File : ", str10));
                string str11 = this.objPDFReplacement.ReplacePDFvalues(str6, str7, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Page1 : ", str11));
                string str12 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str11, str10);
                string str13 = this.objPDFReplacement.ReplacePDFvalues(str8, str9, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                string sZCOMPANYID2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.bt_include = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID2, pSzSpeciality, "", "Speciality");
                string btInclude = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID2, "", "WC000000000000000002", "CaseType");
                if (this.bt_include == "True" && btInclude == "True")
                {
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                }
                MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str3, str12), string.Concat(this.objNF3Template.getPhysicalPath(), str3, str13), string.Concat(this.objNF3Template.getPhysicalPath(), str3, str13.Replace(".pdf", "_MER.pdf")));
                string str14 = str13.Replace(".pdf", "_MER.pdf");
                if (this.bt_include == "True" && btInclude == "True")
                {
                    MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str3, str14), string.Concat(this.objNF3Template.getPhysicalPath(), str3, this.str_1500), string.Concat(this.objNF3Template.getPhysicalPath(), str3, this.str_1500.Replace(".pdf", "_MER.pdf")));
                    str14 = this.str_1500.Replace(".pdf", "_MER.pdf");
                }
                string str15 = "";
                str15 = string.Concat(str3, str14);
                Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("GenereatedFileName : ", str15));
                string str16 = "";
                str16 = string.Concat(ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString(), str15);
                string str17 = string.Concat(this.objNF3Template.getPhysicalPath(), "/", str15);
                CutePDFDocumentClass cutePDFDocumentClass = new CutePDFDocumentClass();
                string str18 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                cutePDFDocumentClass.initialize(str18);
                if (cutePDFDocumentClass != null && File.Exists(str17) && configurationSettings1 != "CI_0000003" && this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5 && configurationSettings == "CK_0000003" && (!(configurationSettings1 == "CI_0000004") || this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))
                {
                    str10 = str17.Replace(".pdf", "_NewMerge.pdf");
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
                string[] strArrays1 = str19.Split(new char[] { '/' });
                ArrayList arrayLists1 = new ArrayList();
                str19 = str19.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                str20 = strArrays1[(int)strArrays1.Length - 1].ToString();
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
                    arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    arrayLists1.Add(this.Session["TM_SZ_CASE_ID"]);
                    arrayLists1.Add(strArrays1[(int)strArrays1.Length - 1].ToString());
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
                    arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    arrayLists1.Add(this.Session["TM_SZ_CASE_ID"]);
                    arrayLists1.Add(strArrays1[(int)strArrays1.Length - 1].ToString());
                    arrayLists1.Add(str2);
                    arrayLists1.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    arrayLists1.Add(pSzSpeciality);
                    arrayLists1.Add("NF");
                    arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    this.objNF3Template.saveGeneratedBillPath(arrayLists1);
                }
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CONTRACT_PDF_GENERATION == "1")
                {
                    arrayLists1.Clear();
                    string companyId = string.Empty;
                    companyId = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    CoverContract_pdf cpdf = new CoverContract_pdf();
                    string fileName = "Contract_" + str20;// this.Session["TM_SZ_BILL_ID"].ToString() + "_contract.pdf";
                    bool isGenerated = false;
                    cpdf.GenerateCoverContractGorMedicalFacility(companyId, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), this.objNF3Template.getPhysicalPath() + str2 + fileName, out isGenerated);
                    if (isGenerated)
                    {
                        arrayLists1.Clear();
                        if (str1 == "OLD")
                        {
                            arrayLists1.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            arrayLists1.Add(str2 + this.str_1500);
                            arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            arrayLists1.Add(this.Session["TM_SZ_CASE_ID"]);
                            arrayLists1.Add(fileName);
                            arrayLists1.Add(str2);
                            arrayLists1.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            arrayLists1.Add(pSzSpeciality);
                            arrayLists1.Add("NF");
                            arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillContractPath(arrayLists1);
                        }
                        else
                        {
                            arrayLists1.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            arrayLists1.Add(str2 + fileName);
                            arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            arrayLists1.Add(this.Session["TM_SZ_CASE_ID"]);
                            arrayLists1.Add(fileName);
                            arrayLists1.Add(str2);
                            arrayLists1.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            arrayLists1.Add(pSzSpeciality);
                            arrayLists1.Add("NF");
                            arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            arrayLists1.Add(nodeType[0].ToString());
                            this.objNF3Template.saveGeneratedBillContractPath_New(arrayLists1);
                        }
                    }
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO()
                {
                    SZ_MESSAGE_TITLE = "BILL_GENERATED",
                    SZ_ACTIVITY_DESC = str20
                };
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
            }
            else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
            {
                Bill_Sys_PVT_Template billSysPVTTemplate = new Bill_Sys_PVT_Template();
                bool bTREFERRINGFACILITY = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str21 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str22 = this.Session["TM_SZ_BILL_ID"].ToString();
                string sZUSERNAME = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    sZCOMPANYNAME = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template = new Bill_Sys_NF3_Template();
                    sZCOMPANYNAME = billSysNF3Template.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    sZCOMPANYID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                string str23 = billSysPVTTemplate.GeneratePVTBill(bTREFERRINGFACILITY, sZCOMPANYID, str21, pSzSpeciality, sZCOMPANYNAME, str22, sZUSERNAME, sZUSERID);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str23, "'); "), true);
            }
            else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) != "WC000000000000000004")
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
            }
            else
            {
                string str24 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                Bill_Sys_PVT_Template billSysPVTTemplate1 = new Bill_Sys_PVT_Template();
                bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str25 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str26 = this.Session["TM_SZ_BILL_ID"].ToString();
                string sZUSERNAME1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string sZUSERID1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    string sZCOMPANYNAME2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    sZCOMAPNYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template1 = new Bill_Sys_NF3_Template();
                    billSysNF3Template1.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    sZCOMAPNYID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                Lien lien = new Lien();
                string sZCOMPANYID3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.bt_include = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID3, pSzSpeciality, "", "Speciality");
                string btInclude1 = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID3, "", "WC000000000000000004", "CaseType");
                if (!(this.bt_include == "True") || !(btInclude1 == "True"))
                {
                    str = lien.GenratePdfForLien(sZCOMAPNYID, str26, pSzSpeciality, str25, sZUSERNAME1, this.txtCaseNo.Text, sZUSERID1);
                }
                else
                {
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                    string str27 = lien.GenratePdfForLienWithMuv(sZCOMAPNYID, str26, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str26, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, sZUSERNAME1, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, sZUSERID1);
                    MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str2, str27), string.Concat(this.objNF3Template.getPhysicalPath(), str24, this.str_1500), string.Concat(this.objNF3Template.getPhysicalPath(), str2, this.str_1500.Replace(".pdf", "_MER.pdf")));
                    str = string.Concat(ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString(), str2, this.str_1500.Replace(".pdf", "_MER.pdf"));
                    ArrayList arrayLists2 = new ArrayList();
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
                    this._DAO_NOTES_EO = new DAO_NOTES_EO()
                    {
                        SZ_MESSAGE_TITLE = "BILL_GENERATED",
                        SZ_ACTIVITY_DESC = this.str_1500
                    };
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                }
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str, "'); "), true);
            }
            Bill_Sys_BillTransaction_BO billSysBillTransactionBO = new Bill_Sys_BillTransaction_BO();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void getDefaultAssociatedDiagCode()
    {
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
            Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = billSysAssociateDiagnosisCodeBO.GetDoctorDiagnosisCode("", this.txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE").Tables[0];
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
        Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdPatientDeskList.DataSource = billSysPatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            this.grdPatientDeskList.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetProcedureCode(string doctorId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataTable dataTable = new DataTable();
            for (int i = 1; i <= 3; i++)
            {
                dataTable = this._bill_Sys_BillTransaction.GetDoctorProcedureCodeList(doctorId, string.Concat("TY00000000000000000", i.ToString()), this.txtCaseID.Text).Tables[0];
                if (dataTable.Rows.Count > 0)
                {
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
                        dataTable1.Columns.Add("FLT_GROUP_AMOUNT");
                        dataTable1.Columns.Add("I_GROUP_AMOUNT_ID");
                        string str = "";
                        string str1 = "";
                        foreach (DataRow row in dataTable.Rows)
                        {
                            str = row["CODE"].ToString().Substring(0, row["CODE"].ToString().IndexOf("|"));
                            str1 = row["CODE"].ToString().Substring(row["CODE"].ToString().IndexOf("|") + 1, row["CODE"].ToString().Length - (row["CODE"].ToString().IndexOf("|") + 1));
                            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            DataTable item = new DataTable();
                            item = this._bill_Sys_BillTransaction.GeSelectedDoctorProcedureCode_List(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, doctorId, str1).Tables[0];
                            foreach (DataRow dataRow in item.Rows)
                            {
                                DataRow num = dataTable1.NewRow();
                                num["SZ_BILL_TXN_DETAIL_ID"] = "";
                                string str2 = row["DESCRIPTION"].ToString().Substring(row["DESCRIPTION"].ToString().Substring(0, row["DESCRIPTION"].ToString().IndexOf("--")).Length + 2, row["DESCRIPTION"].ToString().Length - (row["DESCRIPTION"].ToString().Substring(0, row["DESCRIPTION"].ToString().IndexOf("--")).Length + 2));
                                num["DT_DATE_OF_SERVICE"] = str2.Substring(0, str2.IndexOf("--"));
                                num["SZ_PROCEDURE_ID"] = dataRow["SZ_PROCEDURE_ID"];
                                num["SZ_PROCEDURAL_CODE"] = dataRow["SZ_PROCEDURE_CODE"];
                                num["SZ_CODE_DESCRIPTION"] = dataRow["SZ_CODE_DESCRIPTION"];
                                if (dataRow["DOCTOR_AMOUNT"].ToString() != "0")
                                {
                                    num["FACTOR_AMOUNT"] = dataRow["DOCTOR_AMOUNT"];
                                    num["FLT_AMOUNT"] = Convert.ToDecimal(dataRow["DOCTOR_AMOUNT"].ToString()) * Convert.ToDecimal(dataRow["FLT_KOEL"].ToString());
                                }
                                else
                                {
                                    num["FACTOR_AMOUNT"] = dataRow["FLT_AMOUNT"];
                                    num["FLT_AMOUNT"] = Convert.ToDecimal(dataRow["FLT_AMOUNT"].ToString()) * Convert.ToDecimal(dataRow["FLT_KOEL"].ToString());
                                }
                                num["FACTOR"] = dataRow["FLT_KOEL"];
                                num["PROC_AMOUNT"] = dataRow["FLT_AMOUNT"];
                                num["DOCT_AMOUNT"] = dataRow["DOCTOR_AMOUNT"];
                                num["I_UNIT"] = "";
                                num["SZ_TYPE_CODE_ID"] = str1;
                                dataTable1.Rows.Add(num);
                            }
                        }
                        this.grdTransactionDetails.DataSource = dataTable1;
                        this.grdTransactionDetails.DataBind();
                    }
                    catch
                    {
                    }
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            this.grdProcedure.DataSource = this._bill_Sys_BillTransaction.GetDoctorSpecialityProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdProcedure.DataBind();
            this.grdGroupProcCodeService.DataSource = this._bill_Sys_BillTransaction.GetDoctorSpecialityGroupProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdGroupProcCodeService.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdAllReports_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        base.Request.QueryString["EID"].ToString();
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
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
        string sZCOMPANYID;
        string sZCOMPANYNAME;
        string str;
        string str1;
        try
        {
            this._MUVGenerateFunction = new MUVGenerateFunction();
            Bill_Sys_PopUpBillTransactionAllVisit.log.Debug("Start : grdLatestBillTransaction_ItemCommand");
            if (e.CommandName.ToString() == "Add IC9 Code")
            {
                this.Session["PassedBillID"] = e.CommandArgument;
                base.Response.Redirect("Bill_Sys_BillIC9Code.aspx", false);
            }
            if (e.CommandName.ToString() == "Generate bill")
            {
                Bill_Sys_PopUpBillTransactionAllVisit.log.Debug("Generate bill");
                string text = e.Item.Cells[3].Text;
                char[] chrArray = new char[] { ' ' };
                string str2 = text.Split(chrArray)[0].ToString();
                this.Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                this.Session["TM_SZ_BILL_ID"] = e.Item.Cells[1].Text;
                this.objNF3Template = new Bill_Sys_NF3_Template();
                CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
                string sZCOMPANYID1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                string[] strArrays = new string[] { ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Bills/", str2, "/" };
                string str3 = string.Concat(strArrays);
                if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
                {
                    string str4 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                    string str5 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                    string str6 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                    ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                    Bill_Sys_Configuration billSysConfiguration = new Bill_Sys_Configuration();
                    string configurationSettings = billSysConfiguration.getConfigurationSettings(sZCOMPANYID1, "GET_DIAG_PAGE_POSITION");
                    string configurationSettings1 = billSysConfiguration.getConfigurationSettings(sZCOMPANYID1, "DIAG_PAGE");
                    string str7 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                    string str8 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                    string str9 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                    string str10 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                    GenerateNF3PDF generateNF3PDF = new GenerateNF3PDF();
                    this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string str11 = generateNF3PDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str6);
                    Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill Details PDF File : ", str11));
                    string str12 = this.objPDFReplacement.ReplacePDFvalues(str7, str8, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Page1 : ", str12));
                    string str13 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str12, str11);
                    string str14 = this.objPDFReplacement.ReplacePDFvalues(str9, str10, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    string sZCOMPANYID2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    string text1 = e.Item.Cells[3].Text;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID2, text1, "", "Speciality");
                    string btInclude = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID2, "", "WC000000000000000002", "CaseType");
                    if (this.bt_include == "True" && btInclude == "True")
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                    }
                    MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str4, str13), string.Concat(this.objNF3Template.getPhysicalPath(), str4, str14), string.Concat(this.objNF3Template.getPhysicalPath(), str4, str14.Replace(".pdf", "_MER.pdf")));
                    string str15 = str14.Replace(".pdf", "_MER.pdf");
                    if (this.bt_include == "True" && btInclude == "True")
                    {
                        MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str4, str15), string.Concat(this.objNF3Template.getPhysicalPath(), str4, this.str_1500), string.Concat(this.objNF3Template.getPhysicalPath(), str4, str15.Replace(".pdf", ".pdf")));
                    }
                    string str16 = "";
                    str16 = string.Concat(str4, str15);
                    Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("GenereatedFileName : ", str16));
                    string str17 = "";
                    str17 = string.Concat(ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString(), str16);
                    string str18 = string.Concat(this.objNF3Template.getPhysicalPath(), "/", str16);
                    CutePDFDocumentClass cutePDFDocumentClass = new CutePDFDocumentClass();
                    string str19 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                    cutePDFDocumentClass.initialize(str19);
                    if (cutePDFDocumentClass != null && File.Exists(str18) && configurationSettings1 != "CI_0000003" && this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5 && configurationSettings == "CK_0000003" && (!(configurationSettings1 == "CI_0000004") || this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))
                    {
                        str11 = str18.Replace(".pdf", "_NewMerge.pdf");
                    }
                    string str20 = "";
                    if (File.Exists(str18) && File.Exists(str18.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str16 = str18.Replace(".pdf", "_New.pdf").ToString();
                    }
                    if (File.Exists(str18) && File.Exists(str18.Replace(".pdf", "_NewMerge.pdf").ToString()))
                    {
                        str20 = str17.Replace(".pdf", "_NewMerge.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str17.Replace(".pdf", "_NewMerge.pdf").ToString(), "'); "), true);
                    }
                    else if (!File.Exists(str18) || !File.Exists(str18.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str20 = str17.ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str17.ToString(), "'); "), true);
                    }
                    else
                    {
                        str20 = str17.Replace(".pdf", "_New.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str17.Replace(".pdf", "_New.pdf").ToString(), "'); "), true);
                    }
                    string str21 = "";
                    chrArray = new char[] { '/' };
                    string[] strArrays1 = str20.Split(chrArray);
                    ArrayList arrayLists = new ArrayList();
                    str20 = str20.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                    str21 = strArrays1[(int)strArrays1.Length - 1].ToString();
                    if (File.Exists(string.Concat(this.objNF3Template.getPhysicalPath(), str5, str21)))
                    {
                        if (!Directory.Exists(string.Concat(this.objNF3Template.getPhysicalPath(), str3)))
                        {
                            Directory.CreateDirectory(string.Concat(this.objNF3Template.getPhysicalPath(), str3));
                        }
                        File.Copy(string.Concat(this.objNF3Template.getPhysicalPath(), str5, str21), string.Concat(this.objNF3Template.getPhysicalPath(), str3, str21));
                    }
                    arrayLists.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                    arrayLists.Add(string.Concat(str3, str21));
                    arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    arrayLists.Add(this.Session["TM_SZ_CASE_ID"]);
                    arrayLists.Add(strArrays1[(int)strArrays1.Length - 1].ToString());
                    arrayLists.Add(str3);
                    arrayLists.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    arrayLists.Add(str2);
                    this.objNF3Template.saveGeneratedBillPath(arrayLists);
                    this._DAO_NOTES_EO = new DAO_NOTES_EO()
                    {
                        SZ_MESSAGE_TITLE = "BILL_GENERATED",
                        SZ_ACTIVITY_DESC = str21
                    };
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                }
                else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
                {
                    Bill_Sys_PVT_Template billSysPVTTemplate = new Bill_Sys_PVT_Template();
                    bool bTREFERRINGFACILITY = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                    string str22 = this.Session["TM_SZ_CASE_ID"].ToString();
                    string str23 = this.Session["TM_SZ_BILL_ID"].ToString();
                    string sZUSERNAME = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        sZCOMPANYNAME = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template = new Bill_Sys_NF3_Template();
                        sZCOMPANYNAME = billSysNF3Template.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", billSysPVTTemplate.GeneratePVTBill(bTREFERRINGFACILITY, sZCOMPANYID, str22, str2, sZCOMPANYNAME, str23, sZUSERNAME, sZUSERID), "'); "), true);
                }
                else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) != "WC000000000000000004")
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
                }
                else
                {
                    string str24 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                    Bill_Sys_PVT_Template billSysPVTTemplate1 = new Bill_Sys_PVT_Template();
                    bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                    string str25 = this.Session["TM_SZ_CASE_ID"].ToString();
                    string sZUSERNAME1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string sZUSERID1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    string str26 = this.Session["TM_SZ_BILL_ID"].ToString();
                    if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        string sZCOMPANYNAME1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        str = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    else
                    {
                        Bill_Sys_NF3_Template billSysNF3Template1 = new Bill_Sys_NF3_Template();
                        billSysNF3Template1.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        str = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    Lien lien = new Lien();
                    string sZCOMPANYID3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    string text2 = e.Item.Cells[3].Text;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID3, text2, "", "Speciality");
                    string btInclude1 = this._MUVGenerateFunction.get_bt_include(sZCOMPANYID3, "", "WC000000000000000004", "CaseType");
                    if (!(this.bt_include == "True") || !(btInclude1 == "True"))
                    {
                        str1 = lien.GenratePdfForLien(str, str26, str2, str25, sZUSERNAME1, this.txtCaseNo.Text, sZUSERID1);
                    }
                    else
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                        string str27 = lien.GenratePdfForLienWithMuv(str, str26, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str26, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, sZUSERNAME1, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, sZUSERID1);
                        MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str3, str27), string.Concat(this.objNF3Template.getPhysicalPath(), str24, this.str_1500), string.Concat(this.objNF3Template.getPhysicalPath(), str3, this.str_1500.Replace(".pdf", "_MER.pdf")));
                        str1 = string.Concat(ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString(), str3, this.str_1500.Replace(".pdf", "_MER.pdf"));
                        ArrayList arrayLists1 = new ArrayList();
                        arrayLists1.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        arrayLists1.Add(string.Concat(str3, this.str_1500.Replace(".pdf", "_MER.pdf")));
                        arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        arrayLists1.Add(this.Session["TM_SZ_CASE_ID"]);
                        arrayLists1.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        arrayLists1.Add(str3);
                        arrayLists1.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        arrayLists1.Add(str2);
                        arrayLists1.Add("NF");
                        arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(arrayLists1);
                        this._DAO_NOTES_EO = new DAO_NOTES_EO()
                        {
                            SZ_MESSAGE_TITLE = "BILL_GENERATED",
                            SZ_ACTIVITY_DESC = this.str_1500
                        };
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("window.open('", str1, "'); "), true);
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
                if (e.Item.Cells[10].Text == "1")
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Please first add services to bill!'); ", true);
                }
                else
                {
                    this.Session["PassedBillID"] = e.CommandArgument;
                    this.Session["Balance"] = e.Item.Cells[8].Text;
                    base.Response.Redirect("Bill_Sys_PaymentTransactions.aspx", false);
                }
            }
            this.lblMsg.Visible = false;
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : ", exception.Message.ToString()));
            Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : ", exception.StackTrace.ToString()));
            if (exception.InnerException != null)
            {
                Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : ", exception.InnerException.Message.ToString()));
                Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : ", exception.InnerException.StackTrace.ToString()));
            }
            Elmah.ErrorSignal.FromCurrentContext().Raise(exception1);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            if (e.Item.Cells[10].Text == "1")
            {
                e.Item.Cells[9].Text = "";
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            if (this.objCaseDetailsBO.GetCaseType(e.Item.Cells[1].Text) != "WC000000000000000001")
            {
                e.Item.Cells[12].Text = "";
                e.Item.Cells[13].Text = "";
                e.Item.Cells[14].Text = "";
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            this.grdAllReports.Visible = false;
            this.Session["BillID"] = this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[1].Text;
            this.Session["SZ_BILL_NUMBER"] = this.Session["BillID"].ToString();
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            if (this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[11].Text != "&nbsp;")
            {
                this.extddlDoctor.Text = this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[11].Text;
            }
            Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = billSysAssociateDiagnosisCodeBO.GetBillDiagnosisCode(this.Session["BillID"].ToString()).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            this.txtAmount.Text = this.grdTransactionDetails.Items[this.grdTransactionDetails.SelectedIndex].Cells[8].Text;
            this.lblMsg.Visible = false;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    

    protected void lnkAddGroupService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataRow str;
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
            dataTable.Columns.Add("FLT_GROUP_AMOUNT");
            dataTable.Columns.Add("I_GROUP_AMOUNT_ID");
            dataTable.Columns.Add("SZ_VISIT_TYPE");
            dataTable.Columns.Add("BT_IS_LIMITE");
            foreach (DataGridItem item in this.grdTransactionDetails.Items)
            {
                str = dataTable.NewRow();
                if (!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;") || !(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;") || !(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;"))
                {
                    continue;
                }
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
                str["FLT_AMOUNT"] = item.Cells[6].Text.ToString();
                if (((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "" && Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0)
                {
                    str["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
                }
                str["PROC_AMOUNT"] = item.Cells[9].Text.ToString();
                str["DOCT_AMOUNT"] = item.Cells[10].Text.ToString();
                str["SZ_TYPE_CODE_ID"] = item.Cells[11].Text.ToString();
                str["I_GROUP_AMOUNT_ID"] = item.Cells[13].Text.ToString();
                str["SZ_VISIT_TYPE"] = item.Cells[15].Text.ToString();
                str["BT_IS_LIMITE"] = item.Cells[16].Text.ToString();
                BillTransactionDAO billTransactionDAO = new BillTransactionDAO();
                string str1 = item.Cells[2].Text.ToString();
                string str2 = item.Cells[15].Text.ToString();
                string procID = billTransactionDAO.GetProcID(this.txtCompanyID.Text, str1);
                if (billTransactionDAO.GetLimit(this.txtCompanyID.Text, str2, procID) == "")
                {
                    str["FLT_GROUP_AMOUNT"] = item.Cells[12].Text.ToString();
                }
                else
                {
                    str["FLT_GROUP_AMOUNT"] = "";
                }
                dataTable.Rows.Add(str);
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            string[] strArrays = this.txtGroupDateofService.Text.Split(new char[] { ',' });
            this._bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            if (this._bill_Sys_LoginBO.getDefaultSettings(this.txtCompanyID.Text, "SS00005") != "1")
            {
                string[] strArrays1 = strArrays;
                for (int i = 0; i < (int)strArrays1.Length; i++)
                {
                    object obj = strArrays1[i];
                    if (obj.ToString() != "")
                    {
                        foreach (DataGridItem dataGridItem in this.grdGroupProcCodeService.Items)
                        {
                            if (!((CheckBox)dataGridItem.FindControl("chkselect")).Checked)
                            {
                                continue;
                            }
                            DataSet dataSet = this._bill_Sys_BillTransaction.GroupProcedureCodeList(dataGridItem.Cells[1].Text.ToString(), this.txtCompanyID.Text, dataGridItem.Cells[2].Text.ToString());
                            int num = 1;
                            foreach (DataRow row in dataSet.Tables[0].Rows)
                            {
                                str = dataTable.NewRow();
                                str["SZ_BILL_TXN_DETAIL_ID"] = "";
                                str["DT_DATE_OF_SERVICE"] = obj.ToString();
                                str["SZ_PROCEDURE_ID"] = row.ItemArray.GetValue(0);
                                str["SZ_PROCEDURAL_CODE"] = row.ItemArray.GetValue(2);
                                str["SZ_CODE_DESCRIPTION"] = row.ItemArray.GetValue(3);
                                str["FACTOR_AMOUNT"] = row.ItemArray.GetValue(4);
                                str["FACTOR"] = "1";
                                str["FLT_AMOUNT"] = row.ItemArray.GetValue(4);
                                str["I_UNIT"] = "1";
                                str["PROC_AMOUNT"] = row.ItemArray.GetValue(4);
                                str["DOCT_AMOUNT"] = row.ItemArray.GetValue(4);
                                str["SZ_TYPE_CODE_ID"] = row.ItemArray.GetValue(1);
                                if (num == dataSet.Tables[0].Rows.Count && dataGridItem.Cells[4].Text.ToString() != "")
                                {
                                    str["FLT_GROUP_AMOUNT"] = dataGridItem.Cells[4].Text.ToString();
                                }
                                if (dataGridItem.Cells[3].Text.ToString() != "")
                                {
                                    str["I_GROUP_AMOUNT_ID"] = dataGridItem.Cells[3].Text.ToString();
                                }
                                dataTable.Rows.Add(str);
                                num++;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridItem item1 in this.grdAllReports.Items)
                {
                    if (!((CheckBox)item1.Cells[0].FindControl("chkselect")).Checked)
                    {
                        continue;
                    }
                    string str3 = item1.Cells[5].Text.ToString();
                    foreach (DataGridItem dataGridItem1 in this.grdGroupProcCodeService.Items)
                    {
                        if (!((CheckBox)dataGridItem1.FindControl("chkselect")).Checked)
                        {
                            continue;
                        }
                        DataSet dataSet1 = this._bill_Sys_BillTransaction.GroupProcedureCodeList(dataGridItem1.Cells[1].Text.ToString(), this.txtCompanyID.Text, dataGridItem1.Cells[2].Text.ToString());
                        int num1 = 1;
                        foreach (DataRow dataRow in dataSet1.Tables[0].Rows)
                        {
                            str = dataTable.NewRow();
                            str["SZ_BILL_TXN_DETAIL_ID"] = "";
                            DateTime dateTime1 = Convert.ToDateTime(item1.Cells[2].Text.ToString());
                            str["DT_DATE_OF_SERVICE"] = dateTime1.ToShortDateString();
                            str["SZ_PROCEDURE_ID"] = dataRow.ItemArray.GetValue(0);
                            str["SZ_PROCEDURAL_CODE"] = dataRow.ItemArray.GetValue(2);
                            str["SZ_CODE_DESCRIPTION"] = dataRow.ItemArray.GetValue(3);
                            str["FACTOR_AMOUNT"] = dataRow.ItemArray.GetValue(4);
                            str["FACTOR"] = "1";
                            str["FLT_AMOUNT"] = dataRow.ItemArray.GetValue(4);
                            str["I_UNIT"] = "1";
                            str["PROC_AMOUNT"] = dataRow.ItemArray.GetValue(4);
                            str["DOCT_AMOUNT"] = dataRow.ItemArray.GetValue(4);
                            str["SZ_TYPE_CODE_ID"] = dataRow.ItemArray.GetValue(1);
                            str["SZ_VISIT_TYPE"] = str3;
                            str["BT_IS_LIMITE"] = "1";
                            if (num1 == dataSet1.Tables[0].Rows.Count && dataGridItem1.Cells[4].Text.ToString() != "")
                            {
                                str["FLT_GROUP_AMOUNT"] = dataGridItem1.Cells[4].Text.ToString();
                            }
                            if (dataGridItem1.Cells[3].Text.ToString() != "")
                            {
                                str["I_GROUP_AMOUNT_ID"] = dataGridItem1.Cells[3].Text.ToString();
                            }
                            dataTable.Rows.Add(str);
                            num1++;
                        }
                    }
                }
            }
            DataView dataViews = new DataView();
            dataTable.DefaultView.Sort = "DT_DATE_OF_SERVICE";
            this.grdTransactionDetails.DataSource = dataTable;
            this.grdTransactionDetails.DataBind();
            double num2 = 0;
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO billTransactionDAO1 = new BillTransactionDAO();
                string str4 = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                this.grdTransactionDetails.Items[0].Cells[15].Text.ToString();
                string procID1 = billTransactionDAO1.GetProcID(this.txtCompanyID.Text, str4);
                string sLIMITE = billTransactionDAO1.GET_IS_LIMITE(this.txtCompanyID.Text, procID1);
                if (sLIMITE != "" && sLIMITE != "NULL")
                {
                    for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                    {
                        if (this.grdTransactionDetails.Items[j].Cells[6].Text != "" && this.grdTransactionDetails.Items[j].Cells[6].Text != "&nbsp;")
                        {
                            num2 = num2 + Convert.ToDouble(this.grdTransactionDetails.Items[j].Cells[6].Text);
                        }
                        if (j == this.grdTransactionDetails.Items.Count - 1)
                        {
                            BillTransactionDAO billTransactionDAO2 = new BillTransactionDAO();
                            string str5 = this.grdTransactionDetails.Items[j].Cells[2].Text.ToString();
                            string str6 = this.grdTransactionDetails.Items[j].Cells[15].Text.ToString();
                            string procID2 = billTransactionDAO2.GetProcID(this.txtCompanyID.Text, str5);
                            string limit = billTransactionDAO2.GetLimit(this.txtCompanyID.Text, str6, procID2);
                            if (limit != "")
                            {
                                if (Convert.ToDouble(limit) >= num2)
                                {
                                    this.grdTransactionDetails.Items[j].Cells[12].Text = num2.ToString();
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[j].Cells[12].Text = limit;
                                }
                            }
                            num2 = 0;
                        }
                        else if (this.grdTransactionDetails.Items[j].Cells[1].Text != this.grdTransactionDetails.Items[j + 1].Cells[1].Text)
                        {
                            BillTransactionDAO billTransactionDAO3 = new BillTransactionDAO();
                            string str7 = this.grdTransactionDetails.Items[j].Cells[2].Text.ToString();
                            string str8 = this.grdTransactionDetails.Items[j].Cells[15].Text.ToString();
                            string procID3 = billTransactionDAO3.GetProcID(this.txtCompanyID.Text, str7);
                            string limit1 = billTransactionDAO3.GetLimit(this.txtCompanyID.Text, str8, procID3);
                            if (limit1 != "")
                            {
                                if (Convert.ToDouble(limit1) >= num2)
                                {
                                    this.grdTransactionDetails.Items[j].Cells[12].Text = num2.ToString();
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[j].Cells[12].Text = limit1;
                                }
                            }
                            num2 = 0;
                        }
                    }
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (!this._bill_Sys_BillTransaction.checkUnit(this.extddlDoctor.Text, this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
                for (int k = 0; k < this.grdTransactionDetails.Items.Count; k++)
                {
                    TextBox text = (TextBox)this.grdTransactionDetails.Items[k].FindControl("txtUnit");
                    text.Text = this.grdTransactionDetails.Items[k].Cells[8].Text;
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);  
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
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            if (base.Request.QueryString["P_CASE_ID"] != null)
            {
                string str = base.Request.QueryString["P_CASE_ID"].ToString();
                string str1 = base.Request.QueryString["P_PATIENT_ID"].ToString();
                string str2 = base.Request.QueryString["P_CASE_NO"].ToString();
                Bill_Sys_Case billSysCase = new Bill_Sys_Case()
                {
                    SZ_CASE_ID = str
                };
                CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject();
                //{
                    billSysCaseObject. SZ_PATIENT_ID = str1;
                    billSysCaseObject.SZ_CASE_ID = str;
                    billSysCaseObject.SZ_COMAPNY_ID = caseDetailsBO.GetPatientCompanyID(billSysCaseObject.SZ_PATIENT_ID);
                    billSysCaseObject.SZ_PATIENT_NAME = caseDetailsBO.GetPatientName(billSysCaseObject.SZ_PATIENT_ID);
                    billSysCaseObject.SZ_CASE_NO = str;
                //};
                this.Session["CASE_OBJECT"] = billSysCaseObject;
                this.Session["CASEINFO"] = billSysCase;
                this.Session["PassedCaseID"] = str;
                string str3 = str;
                this.Session["QStrCaseID"] = str3;
                this.Session["Case_ID"] = str3;
                this.Session["Archived"] = "0";
                this.Session["QStrCID"] = str3;
                this.Session["SelectedID"] = str3;
                this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["SN"] = "0";
                this.Session["LastAction"] = "vb_CaseInformation.aspx";
            }
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                base.Response.Redirect("Bill_Sys_ReferralBillTransaction.aspx?Type=Search", false);
            }
            this.btnSave.Attributes.Add("onclick", "return javascript:ConfirmClaimInsurance();");
            this.btnUpdate.Attributes.Add("onclick", "return javascript:validate();");
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
                Bill_Sys_Case billSysCase1 = new Bill_Sys_Case()
                {
                    SZ_CASE_ID = this.txtCaseID.Text
                };
                this.Session["CASEINFO"] = billSysCase1;
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
                this.extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            else
            {
                this.extddlDoctor.Procedure_Name = "SP_MST_BILLING_DOCTOR";
                this.extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            this.btnSave.Attributes.Add("onclick", "return ConfirmClaimInsurance();");
            this.btnUpdate.Attributes.Add("onclick", "return formValidator('frmBillTrans','txtBillDate,extddlDoctor');");
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlDiagnosisType.Flag_ID = this.txtCompanyID.Text;
            if (!base.IsPostBack)
            {
                this.btnUpdate.Enabled = false;
                if (this.Session["SZ_BILL_NUMBER"] != null)
                {
                    this.txtBillID.Text = this.Session["SZ_BILL_NUMBER"].ToString();
                    this.BindTransactionData(this.txtBillID.Text);
                    this.btnSave.Enabled = false;
                    this.btnUpdate.Enabled = true;
                }
                this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                this.Session["SELECTED_DIA_PRO_CODE"] = null;
                if (base.Request.QueryString["PopUp"] != null)
                {
                    if (this.Session["TEMP_DOCTOR_ID"] != null)
                    {
                        this.extddlDoctor.Text = this.Session["TEMP_DOCTOR_ID"].ToString();
                        this.GetProcedureCode(this.extddlDoctor.Text);
                    }
                    if (this.Session["SE_DIAGNOSIS_CODE"] != null)
                    {
                        DataTable dataTable = new DataTable();
                        int count = this.lstDiagnosisCodes.Items.Count;
                    }
                }
                if (this.Session["SZ_BILL_NUMBER"] != null)
                {
                    this.txtBillID.Text = this.Session["SZ_BILL_NUMBER"].ToString();
                    this._editOperation = new EditOperation()
                    {
                        Primary_Value = this.Session["SZ_BILL_NUMBER"].ToString(),
                        WebPage = this.Page,
                        Xml_File = "BillTransaction.xml"
                    };
                    this._editOperation.LoadData();
                    this.txtBillDate.Text = string.Format("{0:MM/dd/yyyy}", this.txtBillDate.Text).ToString();
                    this.setDefaultValues(this.Session["SZ_BILL_NUMBER"].ToString());
                }
            }
            else if (this.Session["SELECTED_DIA_PRO_CODE"] != null)
            {
                DataTable item = new DataTable();
                item = (DataTable)this.Session["SELECTED_DIA_PRO_CODE"];
                this.grdTransactionDetails.DataSource = item;
                this.grdTransactionDetails.DataBind();
                this.Session["SELECTED_DIA_PRO_CODE"] = null;
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            foreach (DataGridItem dataGridItem in this.grdLatestBillTransaction.Items)
            {
                if (this.objCaseDetailsBO.GetCaseType(dataGridItem.Cells[1].Text) == "WC000000000000000001")
                {
                    continue;
                }
                dataGridItem.Cells[12].Text = "";
                dataGridItem.Cells[13].Text = "";
                dataGridItem.Cells[14].Text = "";
            }
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ArrayList claimInsurance = this._bill_Sys_BillTransaction.GetClaimInsurance(this.txtCaseID.Text);
            if (claimInsurance.Count <= 0)
            {
                this.txtClaimInsurance.Text = "0";
            }
            else if (!(claimInsurance[0].ToString() != "") || !(claimInsurance[0].ToString() != "NA") || !(claimInsurance[2].ToString() != "") || !(claimInsurance[2].ToString() != "NA"))
            {
                this.txtClaimInsurance.Text = "1";
            }
            else if (!(claimInsurance[1].ToString() != "") || !(claimInsurance[1].ToString() != ""))
            {
                this.txtClaimInsurance.Text = "2";
            }
            else
            {
                this.txtClaimInsurance.Text = "3";
            }
            if (!this.Page.IsPostBack)
            {
                this.SetDoctor();
            }
            if (this.Hfiels.Value == "1")
            {
                this.BindGrid();
                this.Hfiels.Value = "0";
            }
            if (base.Request.QueryString["F"] != null)
            {
                this.BindCompleteVisit();
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - Page_Load : ", exception.Message.ToString()));
            Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - Page_Load : ", exception.StackTrace.ToString()));
            if (exception.InnerException != null)
            {
                Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - Page_Load : ", exception.InnerException.Message.ToString()));
                Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - Page_Load : ", exception.InnerException.StackTrace.ToString()));
            }
            string str4 = exception.Message.ToString();
            str4 = str4.Replace("\n", " ");
            base.Response.Redirect(string.Concat("Bill_Sys_ErrorPage.aspx?ErrMsg=", str4));
        }
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_PopUpBillTransaction.aspx");
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.Session["SZ_BILL_ID"] = null;
        this.Session["PassedCaseID"] = null;
    }

    private void SaveTransactionData()
    {
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
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
            DataSet dataSet = new DataSet();
            dataSet = this._bill_Sys_BillTransaction.GetAssociatedEntry(setID);
            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                this.extddlDoctor.Text = dataSet.Tables[0].Rows[i][1].ToString();
            }
            this.grdTransactionDetails.Visible = true;
            this.extddlDoctor.Enabled = false;
            this.Session["AssociateDiagnosis"] = true;
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = billSysAssociateDiagnosisCodeBO.GetBillDiagnosisCode(p_szBillID).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.grdTransactionDetails.Visible = true;
            this.BindTransactionDetailsGrid(p_szBillID);
            ArrayList arrayLists = new ArrayList();
            arrayLists = this._bill_Sys_BillTransaction.GetBillInfo(p_szBillID);
            if (arrayLists != null)
            {
                this.extddlDoctor.Text = arrayLists[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void SetDoctor()
    {
        try
        {
            this.extddlDoctor.Text = base.Request.QueryString["DID"].ToString();
            if (this.extddlDoctor.Text != "NA")
            {
                this.GetProcedureCode(this.extddlDoctor.Text);
                this.Session["TEMP_DOCTOR_ID"] = this.extddlDoctor.Text;
                this._bill_Sys_LoginBO = new Bill_Sys_LoginBO();
                if (this._bill_Sys_LoginBO.getDefaultSettings(this.txtCompanyID.Text, "SS00005") != "1")
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
                    dataTable.Columns.Add("SZ_VISIT_TYPE");
                    dataTable.Columns.Add("BT_IS_LIMITE");
                    foreach (DataGridItem item in this.grdTransactionDetails.Items)
                    {
                        if (!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;") || !(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;") || !(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;"))
                        {
                            continue;
                        }
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
                        str["FLT_AMOUNT"] = item.Cells[6].Text.ToString();
                        if (((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "" && Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0)
                        {
                            str["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
                        }
                        str["PROC_AMOUNT"] = item.Cells[9].Text.ToString();
                        str["DOCT_AMOUNT"] = item.Cells[10].Text.ToString();
                        str["SZ_TYPE_CODE_ID"] = item.Cells[12].Text.ToString();
                        str["SZ_VISIT_TYPE"] = item.Cells[15].Text.ToString();
                        str["BT_IS_LIMITE"] = item.Cells[16].Text.ToString();
                        dataTable.Rows.Add(str);
                    }
                    this.grdTransactionDetails.DataSource = dataTable;
                    this.grdTransactionDetails.DataBind();
                }
                else
                {
                    this.lblDateOfService.Style.Add("visibility", "hidden");
                    this.txtDateOfservice.Style.Add("visibility", "hidden");
                    this.Image1.Style.Add("visibility", "hidden");
                    this.lblGroupServiceDate.Style.Add("visibility", "hidden");
                    this.txtGroupDateofService.Style.Add("visibility", "hidden");
                    this.imgbtnDateofService.Style.Add("visibility", "hidden");
                }
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                if (!this._bill_Sys_BillTransaction.checkUnit(this.extddlDoctor.Text, this.txtCompanyID.Text))
                {
                    this.grdTransactionDetails.Columns[7].Visible = false;
                }
                else
                {
                    this.grdTransactionDetails.Columns[7].Visible = true;
                }
                Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
                this.lstDiagnosisCodes.DataSource = billSysAssociateDiagnosisCodeBO.GetCaseDiagnosisCode(this.txtCaseID.Text, this.extddlDoctor.Text, this.txtCompanyID.Text).Tables[0];
                this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                this.lstDiagnosisCodes.DataValueField = "CODE";
                this.lstDiagnosisCodes.DataBind();
                this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - Page_Load : ", exception.Message.ToString()));
            Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - Page_Load : ", exception.StackTrace.ToString()));
            if (exception.InnerException != null)
            {
                Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - Page_Load : ", exception.InnerException.Message.ToString()));
                Bill_Sys_PopUpBillTransactionAllVisit.log.Debug(string.Concat("Bill_Sys_BillTransaction. Method - Page_Load : ", exception.InnerException.StackTrace.ToString()));
            }
            string str1 = exception.Message.ToString();
            str1 = str1.Replace("\n", " ");
            base.Response.Redirect(string.Concat("Bill_Sys_ErrorPage.aspx?ErrMsg=", str1));
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
            ArrayList arrayLists = new ArrayList();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}