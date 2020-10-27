using AC_Notes;
using CUTEFORMCOLib;
using ExtendedDropDownList;
using GeneratePDFFile;
using log4net;
using MBS.CHNOTES;
using mbs.LienBills;
using MBS.PTNOTES;
using PDFValueReplacement;
using PMNotes;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XGridPagination;
using XGridSearchTextBox;
using XGridView;

public partial class Bill_Sys_MasterBilling : Page, IRequiresSessionState
{

    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;

    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;

    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;

    private DAO_NOTES_BO _DAO_NOTES_BO;

    private DAO_NOTES_EO _DAO_NOTES_EO;

    private Bill_Sys_DigosisCodeBO _digosisCodeBO;

    private MUVGenerateFunction _MUVGenerateFunction;

    private Bill_Sys_ReportBO _reportBO;

    private ArrayList EventId;

    private static ILog log;

    private ArrayList objAL;

    private CaseDetailsBO objCaseDetailsBO;

    private Bill_Sys_NF3_Template objNF3Template;

    private PDFValueReplacement.PDFValueReplacement objPDFReplacement;

    private Bill_Sys_SystemObject objSystemObject;

    private ArrayList pdfbills;

    private SqlConnection Sqlcon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);

    private string sz_CompanyID = "";

    private string sz_UserID = "";

    private string szLastPDFFileName = "";



    static Bill_Sys_MasterBilling()
    {
        Bill_Sys_MasterBilling.log = LogManager.GetLogger("Master Billing");
    }

    public Bill_Sys_MasterBilling()
    {
    }

    protected void AddBillDiffCase()
    {
        DataSet dataSet;
        DataSet procCodeDetails;
        ArrayList arrayLists;
        ArrayList arrayLists1;
        ArrayList arrayLists2;
        try
        {
            ArrayList arrayLists3 = new ArrayList();
            ArrayList arrayLists4 = new ArrayList();
            this.pdfbills = new ArrayList();
            this.EventId = new ArrayList();
            string str = "";
            string latestBillID = "";
            int num = 0;
            Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SZ_CASE_ID");
            dataTable.Columns.Add("SZ_PATIENT_ID");
            dataTable.Columns.Add("CHART NO");
            dataTable.Columns.Add("PATIENT NAME");
            dataTable.Columns.Add("DATE OF SERVICE");
            dataTable.Columns.Add("Patient name");
            dataTable.Columns.Add("Date Of Service");
            dataTable.Columns.Add("Procedure code");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("Status");
            dataTable.Columns.Add("Code ID");
            dataTable.Columns.Add("EVENT ID");
            dataTable.Columns.Add("Doctor ID");
            dataTable.Columns.Add("CASE NO");
            dataTable.Columns.Add("Company ID");
            dataTable.Columns.Add("SZ_PATIENT_TREATMENT_ID");
            dataTable.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            dataTable.Columns.Add("PDF_NO");
            Bill_Sys_ReportBO billSysReportBO = new Bill_Sys_ReportBO();
            foreach (GridViewRow row in this.grdMasterBilling.Rows)
            {
                if (!((CheckBox)row.FindControl("chkSelect")).Checked)
                {
                    continue;
                }
                DataSet proc = this.GetProc(row.Cells[18].Text);
                if (proc.Tables[0].Rows.Count <= 0)
                {
                    continue;
                }
                for (int i = 0; i < proc.Tables[0].Rows.Count; i++)
                {
                    DataRow text = dataTable.NewRow();
                    text["EVENT ID"] = row.Cells[18].Text;
                    text["SZ_CASE_ID"] = row.Cells[11].Text;
                    text["SZ_PATIENT_ID"] = row.Cells[12].Text;
                    text["CHART NO"] = row.Cells[13].Text;
                    text["PATIENT NAME"] = row.Cells[1].Text;
                    text["DATE OF SERVICE"] = row.Cells[7].Text;
                    text["Patient name"] = row.Cells[1].Text;
                    text["Date Of Service"] = row.Cells[7].Text;
                    text["Procedure code"] = proc.Tables[0].Rows[i]["ProceCode"].ToString();
                    text["Description"] = proc.Tables[0].Rows[i]["Description"].ToString();
                    text["Status"] = row.Cells[16].Text;
                    text["Code ID"] = proc.Tables[0].Rows[i]["CodeID"].ToString();
                    text["Doctor ID"] = row.Cells[14].Text;
                    text["CASE NO"] = row.Cells[0].Text;
                    text["Company ID"] = row.Cells[19].Text;
                    text["SZ_PATIENT_TREATMENT_ID"] = proc.Tables[0].Rows[i]["SZ_PATIENT_TREATMENT_ID"].ToString();
                    text["SZ_PROCEDURE_GROUP_ID"] = row.Cells[21].Text;
                    DropDownList dropDownList = (DropDownList)row.FindControl("drpWC");
                    if (dropDownList == null)
                    {
                        text["PDF_NO"] = "0";
                    }
                    else
                    {
                        text["PDF_NO"] = dropDownList.SelectedValue;
                    }
                    dataTable.Rows.Add(text);
                }
            }
            dataTable.DefaultView.Sort = "SZ_CASE_ID ASC";
            billSysReportBO = new Bill_Sys_ReportBO();
            string str1 = "";
            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                num = 0;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (!(dataTable.Rows[j]["SZ_PATIENT_ID"].ToString() == dataRow["SZ_PATIENT_ID"].ToString()) || !(dataTable.Rows[j]["SZ_PROCEDURE_GROUP_ID"].ToString() == dataRow["SZ_PROCEDURE_GROUP_ID"].ToString()) || !(dataTable.Rows[j]["Doctor ID"].ToString() != dataRow["Doctor ID"].ToString()))
                    {
                        continue;
                    }
                    ScriptManager.RegisterStartupScript(this, base.GetType(), "CheckDoctor", "<script type='text/javascript'>alert('Please Select Same Doctor For Same patient')</script>", false);
                    return;
                }
            }
            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                num = 0;
                foreach (DataRow row1 in dataTable.Rows)
                {
                    if (!(dataTable.Rows[k]["SZ_PATIENT_ID"].ToString() == row1["SZ_PATIENT_ID"].ToString()) || !(dataTable.Rows[k]["SZ_PROCEDURE_GROUP_ID"].ToString() == row1["SZ_PROCEDURE_GROUP_ID"].ToString()))
                    {
                        continue;
                    }
                    num++;
                }
                dataSet = new DataSet();
                if (billSysAssociateDiagnosisCodeBO.GetDoctorDiagCodeforMasterBilling(dataTable.Rows[k]["SZ_PROCEDURE_GROUP_ID"].ToString(), dataTable.Rows[k]["SZ_CASE_ID"].ToString(), "GET_DOCTOR_DIAGNOSIS_CODE").Tables[0].Rows.Count == 0)
                {
                    str1 = (str1 != "" ? string.Concat(str1, ",", dataTable.Rows[k]["CASE NO"].ToString()) : dataTable.Rows[k]["CASE NO"].ToString());
                }
                k = k + (num - 1);
            }
            if (str1 == "")
            {
                for (int l = 0; l < dataTable.Rows.Count; l++)
                {
                    this.Session["Procedure_Code"] = dataTable.Rows[l]["SZ_PROCEDURE_GROUP_ID"].ToString();
                    num = 0;
                    string str2 = dataTable.Rows[l]["SZ_PATIENT_ID"].ToString();
                    string str3 = dataTable.Rows[l]["SZ_PROCEDURE_GROUP_ID"].ToString();
                    foreach (DataRow dataRow1 in dataTable.Rows)
                    {
                        if (!(str2 == dataRow1["SZ_PATIENT_ID"].ToString()) || !(str3 == dataRow1["SZ_PROCEDURE_GROUP_ID"].ToString()))
                        {
                            continue;
                        }
                        num++;
                        this.EventId.Add(dataRow1["EVENT ID"].ToString());
                    }
                    if (num == 1)
                    {
                        this.txtCaseID.Text = dataTable.Rows[l]["SZ_CASE_ID"].ToString();
                        this.txtReadingDocID.Text = dataTable.Rows[l]["Doctor ID"].ToString();
                        this.txtPatientID.Text = dataTable.Rows[l]["SZ_PATIENT_ID"].ToString();
                        this.txtCaseNo.Text = dataTable.Rows[l]["CASE NO"].ToString();
                        str = dataTable.Rows[l]["Company ID"].ToString();
                        Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject()
                        {
                            SZ_CASE_ID = this.txtCaseID.Text,
                            SZ_COMAPNY_ID = str,
                            SZ_CASE_NO = this.txtCaseNo.Text,
                            SZ_PATIENT_ID = this.txtPatientID.Text
                        };
                        this.Session["CASE_OBJECT"] = billSysCaseObject;
                        if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                        {
                            this.txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        }
                        else
                        {
                            this.txtRefCompanyID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                        }
                        dataSet = new DataSet();
                        dataSet = billSysAssociateDiagnosisCodeBO.GetDoctorDiagCodeforMasterBilling(str3, this.txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                        if (dataSet.Tables[0].Rows.Count <= 0)
                        {
                            arrayLists3.Add(dataTable.Rows[l]["CASE NO"].ToString());
                        }
                        else
                        {
                            arrayLists = new ArrayList();
                            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            billSysReportBO = new Bill_Sys_ReportBO();
                            arrayLists.Add(this.txtCaseID.Text);
                            arrayLists.Add(this.txtBillDate.Text);
                            arrayLists.Add(this.txtCompanyID.Text);
                            arrayLists.Add(dataTable.Rows[l]["Doctor ID"].ToString());
                            arrayLists.Add("0");
                            arrayLists.Add("");
                            arrayLists.Add("");
                            arrayLists.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                            arrayLists.Add(this.Session["Procedure_Code"].ToString());
                            billSysReportBO.InsertBillTransactionData(arrayLists);
                            this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                            latestBillID = this._bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(this.txtCompanyID.Text.ToString());
                            this._DAO_NOTES_EO = new DAO_NOTES_EO()
                            {
                                SZ_MESSAGE_TITLE = "BILL_CREATED",
                                SZ_ACTIVITY_DESC = latestBillID
                            };
                            this._DAO_NOTES_BO = new DAO_NOTES_BO();
                            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            this._DAO_NOTES_EO.SZ_CASE_ID = this.txtCaseID.Text;
                            this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                            billSysReportBO = new Bill_Sys_ReportBO();
                            arrayLists = new ArrayList();
                            arrayLists.Add(this.txtPatientID.Text);
                            arrayLists.Add(dataTable.Rows[l]["Code ID"].ToString());
                            arrayLists.Add(this.txtCompanyID.Text);
                            arrayLists.Add(dataTable.Rows[l]["Doctor ID"].ToString());
                            billSysReportBO.GetTreatmentID(arrayLists);
                            arrayLists1 = new ArrayList();
                            arrayLists1.Add(dataTable.Rows[l]["Procedure code"].ToString());
                            arrayLists1.Add(dataTable.Rows[l]["Description"].ToString());
                            arrayLists1.Add(this.txtCompanyID.Text);
                            billSysReportBO = new Bill_Sys_ReportBO();
                            procCodeDetails = new DataSet();
                            procCodeDetails = billSysReportBO.GetProcCodeDetails(arrayLists1);
                            if (procCodeDetails.Tables[0].Rows.Count != 0)
                            {
                                arrayLists2 = new ArrayList();
                                arrayLists2.Add(procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString());
                                arrayLists2.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                arrayLists2.Add(latestBillID);
                                arrayLists2.Add(dataTable.Rows[l]["DATE OF SERVICE"].ToString());
                                arrayLists2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                arrayLists2.Add("1");
                                arrayLists2.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                arrayLists2.Add("1");
                                arrayLists2.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                arrayLists2.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                arrayLists2.Add(dataTable.Rows[l]["Doctor ID"].ToString());
                                arrayLists2.Add(dataTable.Rows[l]["SZ_CASE_ID"].ToString());
                                arrayLists2.Add(dataTable.Rows[l]["Code ID"].ToString());
                                arrayLists2.Add("");
                                arrayLists2.Add("");
                                arrayLists2.Add(dataTable.Rows[l]["SZ_PATIENT_TREATMENT_ID"].ToString());
                                this._bill_Sys_BillTransaction.SaveTransactionData(arrayLists2);
                                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                this._bill_Sys_BillTransaction.DeleteTransactionDiagnosis(latestBillID);
                                if (dataSet.Tables[0].Rows.Count > 0)
                                {
                                    for (int m = 0; m < dataSet.Tables[0].Rows.Count; m++)
                                    {
                                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                        arrayLists2 = new ArrayList();
                                        arrayLists2.Add(dataSet.Tables[0].Rows[m]["CODE"].ToString());
                                        arrayLists2.Add(latestBillID);
                                        this._bill_Sys_BillTransaction.SaveTransactionDiagnosis(arrayLists2);
                                    }
                                }
                                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                                {
                                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                    string docSpeciality = this._bill_Sys_BillTransaction.GetDocSpeciality(latestBillID);
                                    this.pdfbills.Add(latestBillID);
                                    this.GenerateAddedBillPDF(latestBillID, docSpeciality);
                                    arrayLists4.Add(dataTable.Rows[l]["CASE NO"].ToString());
                                }
                                else
                                {
                                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                    string docSpeciality1 = this._bill_Sys_BillTransaction.GetDocSpeciality(latestBillID);
                                    this.pdfbills.Add(latestBillID);
                                    this.GenerateAddedBillPDF(latestBillID, docSpeciality1);
                                    arrayLists4.Add(dataTable.Rows[l]["CASE NO"].ToString());
                                }
                            }
                            else
                            {
                                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "mm", string.Concat("<script language='javascript'>alert('Procedure Code/Description for Code ", arrayLists1[0].ToString(), " not found');</script>"));
                                return;
                            }
                        }
                        for (int n = 0; n < this.EventId.Count; n++)
                        {
                            string[] strArrays = new string[] { "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='", latestBillID, "', @FLAG='UPDATE_BILL_NUMBER',@SZ_EVENT_ID ='", this.EventId[n].ToString(), "', @SZ_CASE_ID='", this.txtCaseID.Text, "',@SZ_COMPANY_ID='", this.txtCompanyID.Text, "'" };
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat(strArrays), this.Sqlcon);
                            sqlDataAdapter.Fill(new DataSet());
                        }
                        this.EventId.Clear();
                    }
                    else if (num > 1)
                    {
                        this.txtCaseID.Text = dataTable.Rows[l]["SZ_CASE_ID"].ToString();
                        this.txtReadingDocID.Text = dataTable.Rows[l]["Doctor ID"].ToString();
                        this.txtPatientID.Text = dataTable.Rows[l]["SZ_PATIENT_ID"].ToString();
                        this.txtCaseNo.Text = dataTable.Rows[l]["CASE NO"].ToString();
                        str = dataTable.Rows[l]["Company ID"].ToString();
                        Bill_Sys_CaseObject billSysCaseObject1 = new Bill_Sys_CaseObject()
                        {
                            SZ_CASE_ID = this.txtCaseID.Text,
                            SZ_COMAPNY_ID = str,
                            SZ_CASE_NO = this.txtCaseNo.Text,
                            SZ_PATIENT_ID = this.txtPatientID.Text
                        };
                        this.Session["CASE_OBJECT"] = billSysCaseObject1;
                        if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                        {
                            this.txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        }
                        else
                        {
                            this.txtRefCompanyID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                        }
                        dataSet = new DataSet();
                        dataSet = billSysAssociateDiagnosisCodeBO.GetDoctorDiagCodeforMasterBilling(dataTable.Rows[l]["SZ_PROCEDURE_GROUP_ID"].ToString(), this.txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                        if (dataSet.Tables[0].Rows.Count <= 0)
                        {
                            arrayLists3.Add(dataTable.Rows[l]["CASE NO"].ToString());
                        }
                        else
                        {
                            arrayLists = new ArrayList();
                            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            billSysReportBO = new Bill_Sys_ReportBO();
                            arrayLists.Add(this.txtCaseID.Text);
                            arrayLists.Add(this.txtBillDate.Text);
                            arrayLists.Add(this.txtCompanyID.Text);
                            arrayLists.Add(dataTable.Rows[l]["Doctor ID"].ToString());
                            arrayLists.Add("0");
                            arrayLists.Add("");
                            arrayLists.Add("");
                            arrayLists.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                            arrayLists.Add(this.Session["Procedure_Code"].ToString());
                            billSysReportBO.InsertBillTransactionData(arrayLists);
                            this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                            latestBillID = this._bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(this.txtCompanyID.Text.ToString());
                            this._DAO_NOTES_EO = new DAO_NOTES_EO()
                            {
                                SZ_MESSAGE_TITLE = "BILL_CREATED",
                                SZ_ACTIVITY_DESC = latestBillID
                            };
                            this._DAO_NOTES_BO = new DAO_NOTES_BO();
                            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            this._DAO_NOTES_EO.SZ_CASE_ID = this.txtCaseID.Text;
                            this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                            billSysReportBO = new Bill_Sys_ReportBO();
                            arrayLists = new ArrayList();
                            arrayLists.Add(this.txtPatientID.Text);
                            arrayLists.Add(dataTable.Rows[l]["Code ID"].ToString());
                            arrayLists.Add(this.txtCompanyID.Text);
                            arrayLists.Add(dataTable.Rows[l]["Doctor ID"].ToString());
                            billSysReportBO.GetTreatmentID(arrayLists);
                            arrayLists1 = new ArrayList();
                            arrayLists1.Add(dataTable.Rows[l]["Procedure code"].ToString());
                            arrayLists1.Add(dataTable.Rows[l]["Description"].ToString());
                            arrayLists1.Add(this.txtCompanyID.Text);
                            billSysReportBO = new Bill_Sys_ReportBO();
                            procCodeDetails = new DataSet();
                            if (billSysReportBO.GetProcCodeDetails(arrayLists1).Tables[0].Rows.Count != 0)
                            {
                                int num1 = 0;
                                while (num1 < num)
                                {
                                    arrayLists1 = new ArrayList();
                                    arrayLists1.Add(dataTable.Rows[num1 + l]["Procedure code"].ToString());
                                    arrayLists1.Add(dataTable.Rows[num1 + l]["Description"].ToString());
                                    arrayLists1.Add(this.txtCompanyID.Text);
                                    billSysReportBO = new Bill_Sys_ReportBO();
                                    procCodeDetails = new DataSet();
                                    procCodeDetails = billSysReportBO.GetProcCodeDetails(arrayLists1);
                                    if (procCodeDetails.Tables[0].Rows.Count != 0)
                                    {
                                        arrayLists2 = new ArrayList();
                                        arrayLists2.Add(procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString());
                                        arrayLists2.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                        arrayLists2.Add(latestBillID);
                                        arrayLists2.Add(dataTable.Rows[num1 + l]["DATE OF SERVICE"].ToString());
                                        arrayLists2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                        arrayLists2.Add("1");
                                        arrayLists2.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                        arrayLists2.Add("1");
                                        arrayLists2.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                        arrayLists2.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                        arrayLists2.Add(dataTable.Rows[num1 + l]["Doctor ID"].ToString());
                                        arrayLists2.Add(dataTable.Rows[num1 + l]["SZ_CASE_ID"].ToString());
                                        arrayLists2.Add(dataTable.Rows[num1 + l]["Code ID"].ToString());
                                        arrayLists2.Add("");
                                        arrayLists2.Add("");
                                        arrayLists2.Add(dataTable.Rows[num1 + l]["SZ_PATIENT_TREATMENT_ID"].ToString());
                                        this._bill_Sys_BillTransaction.SaveTransactionData(arrayLists2);
                                        num1++;
                                    }
                                    else
                                    {
                                        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "mm", string.Concat("<script language='javascript'>alert('Procedure Code/Description for Code ", arrayLists1[0].ToString(), " not found');</script>"));
                                        return;
                                    }
                                }
                                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                this._bill_Sys_BillTransaction.DeleteTransactionDiagnosis(latestBillID);
                                if (dataSet.Tables[0].Rows.Count > 0)
                                {
                                    for (int o = 0; o < dataSet.Tables[0].Rows.Count; o++)
                                    {
                                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                        arrayLists2 = new ArrayList();
                                        arrayLists2.Add(dataSet.Tables[0].Rows[o]["CODE"].ToString());
                                        arrayLists2.Add(latestBillID);
                                        this._bill_Sys_BillTransaction.SaveTransactionDiagnosis(arrayLists2);
                                    }
                                }
                                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                                {
                                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                    string docSpeciality2 = this._bill_Sys_BillTransaction.GetDocSpeciality(latestBillID);
                                    this.pdfbills.Add(latestBillID);
                                    this.GenerateAddedBillPDF(latestBillID, docSpeciality2);
                                    arrayLists4.Add(dataTable.Rows[l]["CASE NO"].ToString());
                                }
                                else
                                {
                                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                    string docSpeciality3 = this._bill_Sys_BillTransaction.GetDocSpeciality(latestBillID);
                                    this.pdfbills.Add(latestBillID);
                                    this.GenerateAddedBillPDF(latestBillID, docSpeciality3);
                                    arrayLists4.Add(dataTable.Rows[l]["CASE NO"].ToString());
                                }
                            }
                            else
                            {
                                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "mm", string.Concat("<script language='javascript'>alert('Procedure Code/Description for Code ", arrayLists1[0].ToString(), " not found');</script>"));
                                return;
                            }
                        }
                        l = l + (num - 1);
                        for (int p = 0; p < this.EventId.Count; p++)
                        {
                            string[] strArrays1 = new string[] { "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='", latestBillID, "', @FLAG='UPDATE_BILL_NUMBER',@SZ_EVENT_ID ='", this.EventId[p].ToString(), "',@SZ_CASE_ID='", this.txtCaseID.Text, "',@SZ_COMPANY_ID='", this.txtCompanyID.Text, "'" };
                            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(string.Concat(strArrays1), this.Sqlcon);
                            sqlDataAdapter1.Fill(new DataSet());
                        }
                        this.EventId.Clear();
                    }
                }
                string str4 = "";
                if (arrayLists4.Count > 0)
                {
                    for (int q = 0; q < arrayLists4.Count; q++)
                    {
                        str4 = (q != arrayLists4.Count - 1 ? string.Concat(str4, arrayLists4[q].ToString(), ", ") : string.Concat(str4, arrayLists4[q].ToString()));
                    }
                    this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "mm", string.Concat("<script language='javascript'>alert('Bill Created Successfully For CaseNo ", str4, " ');</script>"));
                }
                string str5 = "";
                if (arrayLists3.Count > 0)
                {
                    for (int r = 0; r < arrayLists3.Count; r++)
                    {
                        str5 = string.Concat(str5, arrayLists3[r].ToString(), ", ");
                    }
                    ScriptManager.RegisterStartupScript(this, base.GetType(), "MM1231", string.Concat("<script type='text/javascript'>alert('Cannot create bill for ", str5, " as no diagnosis code is not assigned for the patient')</script>"), false);
                }
                if (this.pdfbills.Count > 0)
                {
                    string str6 = this.grdMasterBilling.DataKeys[0].Value.ToString();
                    string str7 = str6;
                    if (str6 != null)
                    {
                        switch (str7)
                        {
                            case "AC":
                            case "Accupuncture":
                            case "Acupuncture":
                                {
                                    this.billsperpatient(this.pdfbills);
                                    break;
                                }
                            case "CH":
                                {
                                    this.CHBillsPerPatient_iText(this.pdfbills);
                                    break;
                                }
                            case "PT":
                                {
                                    this.PTTest(this.pdfbills);
                                    break;
                                }
                            case "Pain Management":
                            case "PM":
                                {
                                    this.PMTest(this.pdfbills);
                                    break;
                                }
                        }
                    }
                    this.grdMasterBilling.XGridBindSearch();
                    if (this.grdMasterBilling.RecordCount > 0)
                    {
                        for (int s = 0; s < this.grdMasterBilling.Rows.Count; s++)
                        {
                            string str8 = this.grdMasterBilling.DataKeys[s]["BT_FINALIZE"].ToString();
                            CheckBox checkBox = (CheckBox)this.grdMasterBilling.Rows[s].FindControl("chkSelect");
                            if (str8.ToLower() != "false")
                            {
                                checkBox.Enabled = true;
                            }
                            else
                            {
                                checkBox.Enabled = false;
                            }
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, base.GetType(), "MM12323", string.Concat("<script type='text/javascript'>alert('Cannot create bill for ", str1, " as no diagnosis code is assigned for the patient')</script>"), false);
            }
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            this.usrMessage.PutMessage(string.Concat("ERROR", exception.ToString()));
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            this.usrMessage.Show();
        }
    }

    protected void AddBillSameCase()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList arrayLists;
        try
        {
            this.pdfbills = new ArrayList();
            this.EventId = new ArrayList();
            string text = "";
            ArrayList arrayLists1 = new ArrayList();
            string str = "";
            string text1 = "";
            int num = 0;
            int num1 = 0;
            string latestBillID = "";
            int num2 = 0;
            string str1 = "";
            foreach (GridViewRow row in this.grdMasterBilling.Rows)
            {
                if (!((CheckBox)row.Cells[10].FindControl("chkSelect")).Checked)
                {
                    continue;
                }
                str = row.Cells[12].Text;
                text1 = row.Cells[3].Text;
            }
            foreach (GridViewRow gridViewRow in this.grdMasterBilling.Rows)
            {
                if (!((CheckBox)gridViewRow.Cells[10].FindControl("chkSelect")).Checked)
                {
                    continue;
                }
                if (str != gridViewRow.Cells[12].Text)
                {
                    num = 2;
                    break;
                }
                else if (text1 == gridViewRow.Cells[3].Text)
                {
                    num = 1;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, base.GetType(), "CheckDoctor", "<script type='text/javascript'>alert('Please Select Same Doctor For Same patient')</script>", false);
                    return;
                }
            }
            if (num == 2)
            {
                ScriptManager.RegisterStartupScript(this, base.GetType(), "yu12323", "<script type='text/javascript'>alert('Select visits for same patient')</script>", false);
            }
            else if (num == 1)
            {
                ArrayList arrayLists2 = new ArrayList();
                foreach (GridViewRow row1 in this.grdMasterBilling.Rows)
                {
                    if (!((CheckBox)row1.Cells[10].FindControl("chkSelect")).Checked || !(row1.Cells[4].Text == "&nbsp;") && !(row1.Cells[5].Text == "&nbsp;") && !(row1.Cells[22].Text == "&nbsp;"))
                    {
                        continue;
                    }
                    arrayLists2.Add(row1.Cells[0].Text);
                }
                string str2 = "";
                string str3 = "";
                if (arrayLists2.Count <= 0)
                {
                    foreach (GridViewRow gridViewRow1 in this.grdMasterBilling.Rows)
                    {
                        if (!((CheckBox)gridViewRow1.Cells[10].FindControl("chkSelect")).Checked)
                        {
                            continue;
                        }
                        str1 = gridViewRow1.Cells[21].Text;
                        this.Session["Procedure_Code"] = str1;
                    }
                    foreach (GridViewRow row2 in this.grdMasterBilling.Rows)
                    {
                        if (!((CheckBox)row2.Cells[10].FindControl("chkSelect")).Checked)
                        {
                            continue;
                        }
                        if (str1 != row2.Cells[21].Text)
                        {
                            num2 = 2;
                            break;
                        }
                        else
                        {
                            num2 = 1;
                        }
                    }
                    if (num2 != 2)
                    {
                        Bill_Sys_ReportBO billSysReportBO = new Bill_Sys_ReportBO();
                        Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
                        foreach (GridViewRow gridViewRow2 in this.grdMasterBilling.Rows)
                        {
                            if (!((CheckBox)gridViewRow2.Cells[10].FindControl("chkSelect")).Checked)
                            {
                                continue;
                            }
                            num1 = 1;
                            this.txtCaseID.Text = gridViewRow2.Cells[11].Text;
                            this.txtReadingDocID.Text = gridViewRow2.Cells[14].Text;
                            this.txtPatientID.Text = gridViewRow2.Cells[12].Text;
                            this.txtCaseNo.Text = gridViewRow2.Cells[0].Text;
                            text = gridViewRow2.Cells[19].Text;
                            this.EventId.Add(gridViewRow2.Cells[25].Text);
                            DropDownList dropDownList = (DropDownList)gridViewRow2.FindControl("drpWC");
                            if (dropDownList == null)
                            {
                                continue;
                            }
                            string selectedValue = dropDownList.SelectedValue;
                        }
                        Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject()
                        {
                            SZ_CASE_ID = this.txtCaseID.Text,
                            SZ_COMAPNY_ID = text,
                            SZ_CASE_NO = this.txtCaseNo.Text,
                            SZ_PATIENT_ID = this.txtPatientID.Text
                        };
                        this.Session["CASE_OBJECT"] = billSysCaseObject;
                        if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                        {
                            this.txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        }
                        else
                        {
                            this.txtRefCompanyID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                        }
                        if (num1 == 1)
                        {
                            DataSet dataSet = new DataSet();
                            dataSet = billSysAssociateDiagnosisCodeBO.GetDoctorDiagCodeforMasterBilling(str1, this.txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                            if (dataSet.Tables[0].Rows.Count <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, base.GetType(), "z23", "<script type='text/javascript'>alert('No diagnosis code assign for the patient');</script>", false);
                            }
                            else
                            {
                                ArrayList arrayLists3 = new ArrayList();
                                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                billSysReportBO = new Bill_Sys_ReportBO();
                                arrayLists3.Add(this.txtCaseID.Text);
                                arrayLists3.Add(this.txtBillDate.Text);
                                arrayLists3.Add(this.txtCompanyID.Text);
                                arrayLists3.Add(this.txtReadingDocID.Text);
                                arrayLists3.Add("0");
                                arrayLists3.Add("");
                                arrayLists3.Add("");
                                arrayLists3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                                arrayLists3.Add(this.Session["Procedure_Code"].ToString());
                                billSysReportBO.InsertBillTransactionData(arrayLists3);
                                this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                                latestBillID = this._bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(this.txtCompanyID.Text.ToString());
                                this._DAO_NOTES_EO = new DAO_NOTES_EO()
                                {
                                    SZ_MESSAGE_TITLE = "BILL_CREATED",
                                    SZ_ACTIVITY_DESC = latestBillID
                                };
                                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                                this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                                foreach (GridViewRow row3 in this.grdMasterBilling.Rows)
                                {
                                    arrayLists3 = new ArrayList();
                                    if (!((CheckBox)row3.Cells[10].FindControl("chkSelect")).Checked)
                                    {
                                        continue;
                                    }
                                    DataSet proc = this.GetProc(row3.Cells[18].Text);
                                    if (proc.Tables[0].Rows.Count <= 0)
                                    {
                                        continue;
                                    }
                                    int num3 = 0;
                                    while (num3 < proc.Tables[0].Rows.Count)
                                    {
                                        billSysReportBO = new Bill_Sys_ReportBO();
                                        arrayLists3 = new ArrayList();
                                        arrayLists3.Add(this.txtPatientID.Text);
                                        arrayLists3.Add(proc.Tables[0].Rows[num3]["CodeID"].ToString());
                                        arrayLists3.Add(this.txtCompanyID.Text);
                                        arrayLists3.Add(row3.Cells[14].Text);
                                        billSysReportBO.GetTreatmentID(arrayLists3);
                                        ArrayList arrayLists4 = new ArrayList();
                                        arrayLists4.Add(proc.Tables[0].Rows[num3]["ProceCode"].ToString());
                                        arrayLists4.Add(proc.Tables[0].Rows[num3]["Description"].ToString());
                                        arrayLists4.Add(this.txtCompanyID.Text);
                                        billSysReportBO = new Bill_Sys_ReportBO();
                                        DataSet procCodeDetails = new DataSet();
                                        procCodeDetails = billSysReportBO.GetProcCodeDetails(arrayLists4);
                                        if (procCodeDetails.Tables[0].Rows.Count != 0)
                                        {
                                            arrayLists = new ArrayList();
                                            arrayLists.Add(procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString());
                                            arrayLists.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                            arrayLists.Add(latestBillID);
                                            arrayLists.Add(row3.Cells[7].Text);
                                            arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                            arrayLists.Add("1");
                                            arrayLists.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                            arrayLists.Add("1");
                                            arrayLists.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                            arrayLists.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                            arrayLists.Add(row3.Cells[14].Text);
                                            arrayLists.Add(row3.Cells[11].Text);
                                            arrayLists.Add(proc.Tables[0].Rows[num3]["CodeID"].ToString());
                                            arrayLists.Add("");
                                            arrayLists.Add("");
                                            arrayLists.Add(proc.Tables[0].Rows[num3]["SZ_PATIENT_TREATMENT_ID"].ToString());
                                            this._bill_Sys_BillTransaction.SaveTransactionData(arrayLists);
                                            num3++;
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, base.GetType(), "zu1323", "<script type='text/javascript'>alert('Procedure Code/Description do not match');</script>", false);
                                            return;
                                        }
                                    }
                                }
                                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                this._bill_Sys_BillTransaction.DeleteTransactionDiagnosis(latestBillID);
                                if (dataSet.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                                    {
                                        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                        arrayLists = new ArrayList();
                                        arrayLists.Add(dataSet.Tables[0].Rows[i]["CODE"].ToString());
                                        arrayLists.Add(latestBillID);
                                        this._bill_Sys_BillTransaction.SaveTransactionDiagnosis(arrayLists);
                                    }
                                }
                                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                                {
                                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                    string docSpeciality = this._bill_Sys_BillTransaction.GetDocSpeciality(latestBillID);
                                    this.pdfbills.Add(latestBillID);
                                    this.GenerateAddedBillPDF(latestBillID, docSpeciality);
                                    arrayLists1.Add(this.txtCaseNo.Text);
                                }
                                else
                                {
                                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                    string docSpeciality1 = this._bill_Sys_BillTransaction.GetDocSpeciality(latestBillID);
                                    this.pdfbills.Add(latestBillID);
                                    this.GenerateAddedBillPDF(latestBillID, docSpeciality1);
                                    arrayLists1.Add(this.txtCaseNo.Text);
                                }
                            }
                        }
                        for (int j = 0; j < this.EventId.Count; j++)
                        {
                            string[] strArrays = new string[] { "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='", latestBillID, "', @FLAG='UPDATE_BILL_NUMBER',@SZ_EVENT_ID ='", this.EventId[j].ToString(), "',@SZ_CASE_ID='", this.txtCaseID.Text, "',@SZ_COMPANY_ID='", this.txtCompanyID.Text, "'" };
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat(strArrays), this.Sqlcon);
                            sqlDataAdapter.Fill(new DataSet());
                        }
                        this.EventId.Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, base.GetType(), "zu1323", "<script type='text/javascript'>alert('Select visits for same Speciality');</script>", false);
                    }
                    if (arrayLists1.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, base.GetType(), "mmasd", string.Concat("<script language='javascript'>alert('Bill Created Successfully For CaseNo ", arrayLists1[0].ToString(), " ');</script>"), false);
                    }
                    if (this.pdfbills.Count > 0)
                    {
                        string str4 = this.grdMasterBilling.DataKeys[0].Value.ToString();
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("Specialty : ", str4));
                        string str5 = str4;
                        string str6 = str5;
                        if (str5 != null)
                        {
                            switch (str6)
                            {
                                case "AC":
                                case "Accupuncture":
                                    {
                                        Bill_Sys_MasterBilling.log.Debug("Call to billsperpatient(pdfbills);");
                                        this.billsperpatient(this.pdfbills);
                                        Bill_Sys_MasterBilling.log.Debug("FUReport generated Successfully!");
                                        break;
                                    }
                                case "SYN":
                                    {
                                        Bill_Sys_MasterBilling.log.Debug("Call to SYNbillsperpatient(pdfbills);");
                                        Bill_Sys_MasterBilling.log.Debug("FUReport generated Successfully!");
                                        break;
                                    }
                                case "CH":
                                    {
                                        Bill_Sys_MasterBilling.log.Debug("Call to CHBillsPerPatient_iText(pdfbills);");
                                        this.CHBillsPerPatient_iText(this.pdfbills);
                                        Bill_Sys_MasterBilling.log.Debug("FUReport generated Successfully!");
                                        break;
                                    }
                                case "PT":
                                    {
                                        Bill_Sys_MasterBilling.log.Debug("Call to PTTest(pdfbills);");
                                        this.PTTest(this.pdfbills);
                                        Bill_Sys_MasterBilling.log.Debug("FUReport generated Successfully!");
                                        break;
                                    }
                                case "WB":
                                    {
                                        Bill_Sys_MasterBilling.log.Debug("Call to WBBillsPerPatient(pdfbills);");
                                        Bill_Sys_MasterBilling.log.Debug("FUReport generated Successfully!");
                                        break;
                                    }
                                case "Pain Management":
                                case "PM":
                                    {
                                        Bill_Sys_MasterBilling.log.Debug("Call to PMTest(pdfbills);");
                                        this.PMTest(this.pdfbills);
                                        Bill_Sys_MasterBilling.log.Debug("FUReport generated Successfully!");
                                        break;
                                    }
                            }
                        }
                        Bill_Sys_MasterBilling.log.Debug("Bind grdMasterBilling.XGridBindSearch()");
                        this.grdMasterBilling.XGridBindSearch();
                        Bill_Sys_MasterBilling.log.Debug("Bind Successfully!");
                        if (this.grdMasterBilling.RecordCount > 0)
                        {
                            for (int k = 0; k < this.grdMasterBilling.Rows.Count; k++)
                            {
                                string str7 = this.grdMasterBilling.DataKeys[k]["BT_FINALIZE"].ToString();
                                CheckBox checkBox = (CheckBox)this.grdMasterBilling.Rows[k].FindControl("chkSelect");
                                if (str7.ToLower() != "false")
                                {
                                    checkBox.Enabled = true;
                                }
                                else
                                {
                                    checkBox.Enabled = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int l = 0; l < arrayLists2.Count; l++)
                    {
                        int num4 = 0;
                        str3 = arrayLists2[l].ToString();
                        str2 = string.Concat(str2, arrayLists2[l].ToString(), ",");
                        for (int m = 0; m < arrayLists2.Count; m++)
                        {
                            if (str3 == arrayLists2[m].ToString())
                            {
                                num4++;
                            }
                        }
                        if (num4 > 1)
                        {
                            l = l + (num4 - 1);
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, base.GetType(), "zu12323", string.Concat("<script type='text/javascript'>alert('You do not have a claim number or an insurance company or an insurance company address added to these Case NO ", str2, ". You cannot proceed furher.')</script>"), false);
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

    protected void billsperpatient(ArrayList pdfbills)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        try
        {
            try
            {
                for (int i = 0; i < pdfbills.Count; i++)
                {
                    string[] str = new string[] { "exec SP_PDFBILLS_MASTERBILLING_New  @SZ_BILL_NUMBER='", pdfbills[i].ToString(), "',@SZ_COMPANY_ID='", this.sz_CompanyID, "', @FLAG='PDF'" };
                    SqlCommand sqlCommand = new SqlCommand(string.Concat(str), sqlConnection)
                    {
                        CommandTimeout = 0
                    };
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataSet dataSet = new DataSet();
                    sqlCommand.CommandTimeout = 0;
                    sqlDataAdapter.Fill(dataSet);
                    if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        object[] item = new object[] { "FUReport_", pdfbills[i], "_", null, null };
                        item[3] = DateTime.Now.ToString("yyyyMMddhhmmss");
                        item[4] = ".pdf";
                        string str1 = string.Concat(item);
                        string str2 = string.Concat(dataSet.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, dataSet.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')), "/", dataSet.Tables[0].Rows[0]["CASEID"].ToString(), "/No Fault File/Medicals/AC/FUReport/");
                        string str3 = string.Concat(dataSet.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, dataSet.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')), "\\", dataSet.Tables[0].Rows[0]["CASEID"].ToString(), "\\No Fault File\\Medicals\\AC\\FUReport\\");
                        string.Concat(ConfigurationManager.AppSettings["DocumentManagerURL"], str2, str1);
                        string str4 = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                        if (!Directory.Exists(string.Concat(str4, "/", str3)))
                        {
                            Directory.CreateDirectory(string.Concat(str4, "/", str3));
                        }
                    (new GenerateHp1()).GenerateHp1PDF(string.Concat(str4, "//", str3, str1), dataSet);
                        string str5 = dataSet.Tables[0].Rows[0]["CASEID"].ToString();
                        string[] strArrays = new string[] { "exec SP_INSERT_AC_BILLING_REPORT_TO_DOCMANAGER @SZ_CASE_ID='", dataSet.Tables[0].Rows[0]["CASEID"].ToString(), "', @SZ_FILE_NAME='", str1, "', @SZ_FILE_PATH='", str2, "', @SZ_LOGIN_ID ='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "'" };
                        sqlCommand = new SqlCommand(string.Concat(strArrays), sqlConnection)
                        {
                            CommandTimeout = 0
                        };
                        sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        str3 = string.Concat(dataSet.Tables[1].Rows[0][0].ToString(), str3);
                        string str6 = dataSet.Tables[2].Rows[0][0].ToString();
                        sqlCommand = new SqlCommand(string.Concat("exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='", pdfbills[i].ToString(), "', @FLAG='GET_EVENT_ID'"), sqlConnection)
                        {
                            CommandTimeout = 0
                        };
                        sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        DataSet dataSet1 = new DataSet();
                        sqlDataAdapter.Fill(dataSet1);
                        if (dataSet1.Tables.Count >= 0)
                        {
                            for (int j = 0; j < dataSet1.Tables[0].Rows.Count; j++)
                            {
                                string[] sZUSERNAME = new string[] { "exec SP_UPLOAD_REPORT_FOR_VISIT  @SZ_USER_NAME='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "',@SZ_FILE_NAME='", str1, "',@SZ_COMPANY_ID='", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "', @SZ_CASE_ID='", str5.ToString(), "', @SZ_USER_ID ='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, "',@SZ_PROCEDURE_GROUP_ID ='", this.Session["Procedure_Code"].ToString(), "',@I_IMAGE_ID ='", str6.ToString(), "',@SZ_EVENT_ID ='", dataSet1.Tables[0].Rows[j][0].ToString(), "'" };
                                sqlCommand = new SqlCommand(string.Concat(sZUSERNAME), sqlConnection)
                                {
                                    CommandTimeout = 0
                                };
                                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                                dataSet = new DataSet();
                                sqlDataAdapter.Fill(dataSet);
                            }
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
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }


        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void BindReffGrid()
    {
    }

    protected void Btn_Patient_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.pdfbills = new ArrayList();
            ArrayList arrayLists = new ArrayList();
            foreach (GridViewRow row in this.grdMasterBilling.Rows)
            {
                if (!((CheckBox)row.Cells[10].FindControl("chkSelect")).Checked || !(row.Cells[4].Text == "&nbsp;") && !(row.Cells[5].Text == "&nbsp;") && !(row.Cells[22].Text == "&nbsp;"))
                {
                    continue;
                }
                arrayLists.Add(row.Cells[0].Text);
            }
            string str = "";
            string str1 = "";
            if (arrayLists.Count <= 0)
            {
                this.AddBillDiffCase();
            }
            else
            {
                for (int i = 0; i < arrayLists.Count; i++)
                {
                    int num = 0;
                    str1 = arrayLists[i].ToString();
                    str = string.Concat(str, arrayLists[i].ToString(), ",");
                    for (int j = 0; j < arrayLists.Count; j++)
                    {
                        if (str1 == arrayLists[j].ToString())
                        {
                            num++;
                        }
                    }
                    if (num > 1)
                    {
                        i = i + (num - 1);
                    }
                }
                this.popupmsg.InnerText = string.Concat("You do not have a claim number or an insurance company or an insurance company address added to these Case NO: '", str, "'.You cannot proceed furher");
                ScriptManager.RegisterStartupScript(this, base.GetType(), "MM1234", "<script type='text/javascript'>openExistsPage1();</script>", false);
            }
        }
        catch (Exception ex)
        {
            Exception exception = ex;
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

    protected void Btn_Selected_Click(object sender, EventArgs e)
    {
        this.AddBillSameCase();
    }

    protected void CHBillsPerPatient(ArrayList pdfbills)
    {
    }

    protected void CHBillsPerPatient_iText(ArrayList pdfbills)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        CHNotes_PDF cHNotesPDF = new CHNotes_PDF();
        ArrayList arrayLists = new ArrayList();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        try
        {
            try
            {
                Bill_Sys_MasterBilling.log.Debug("Start PTTest method.");
                for (int i = 0; i < pdfbills.Count; i++)
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat("exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='", pdfbills[i].ToString(), "', @FLAG='PDF'"), sqlConnection);
                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);
                    if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("SP_PDFBILLS_MASTERBILLING :", dataSet.Tables[0].Rows.Count));
                        object[] item = new object[] { "FUReport_", pdfbills[i], "_", null, null };
                        item[3] = DateTime.Now.ToString("yyyyMMddhhmmss");
                        item[4] = ".pdf";
                        string str = string.Concat(item);
                        string str1 = string.Concat(dataSet.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, dataSet.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')), "/", dataSet.Tables[0].Rows[0]["CASEID"].ToString(), "/No Fault File/Medicals/CH/FUReport/");
                        string str2 = string.Concat(dataSet.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, dataSet.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')), "\\", dataSet.Tables[0].Rows[0]["CASEID"].ToString(), "\\No Fault File\\Medicals\\CH\\FUReport\\");
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("pdfpath : ", ConfigurationManager.AppSettings["DocumentManagerURL"], str1, str));
                        string physicalPath = (new Bill_Sys_NF3_Template()).getPhysicalPath();
                        Bill_Sys_MasterBilling.log.Debug("Start creating directory if directory not exist.");
                        if (!Directory.Exists(string.Concat(physicalPath, str1)))
                        {
                            Directory.CreateDirectory(string.Concat(physicalPath, str1));
                        }
                        Bill_Sys_MasterBilling.log.Debug("creating directory successful.");
                        string str3 = string.Concat(physicalPath, str1, str);
                        string str4 = pdfbills[i].ToString();
                        string sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        string sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        cHNotesPDF.GenerateCHReport(str3, str4, sZUSERID, sZCOMPANYID);
                        string str5 = dataSet.Tables[0].Rows[0]["CASEID"].ToString();
                        string[] sZUSERNAME = new string[] { "exec SP_INSERT_CH_BILLING_REPORT_TO_DOCMANAGER @SZ_CASE_ID='", str5, "', @SZ_FILE_NAME='", str, "', @SZ_FILE_PATH='", str1, "', @SZ_LOGIN_ID ='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "'" };
                        sqlDataAdapter = new SqlDataAdapter(string.Concat(sZUSERNAME), sqlConnection);
                        dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        Bill_Sys_MasterBilling.log.Debug("executed SP_INSERT_CH_BILLING_REPORT_TO_DOCMANAGER ");
                        str2 = string.Concat(dataSet.Tables[1].Rows[0][0].ToString(), str2).Replace("\\\\", "/").Replace("\\", "/");
                        string str6 = dataSet.Tables[2].Rows[0][0].ToString();
                        sqlDataAdapter = new SqlDataAdapter(string.Concat("exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='", pdfbills[i].ToString(), "', @FLAG='GET_EVENT_ID'"), sqlConnection);
                        DataSet dataSet1 = new DataSet();
                        sqlDataAdapter.Fill(dataSet1);
                        Bill_Sys_MasterBilling.log.Debug("ececuted SP_PDFBILLS_MASTERBILLING ");
                        if (dataSet1.Tables.Count >= 0)
                        {
                            for (int j = 0; j < dataSet1.Tables[0].Rows.Count; j++)
                            {
                                string[] strArrays = new string[] { "exec SP_UPLOAD_REPORT_FOR_VISIT  @SZ_USER_NAME='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "',@SZ_FILE_NAME='", str, "',@SZ_COMPANY_ID='", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "', @SZ_CASE_ID='", str5.ToString(), "', @SZ_USER_ID ='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, "',@SZ_PROCEDURE_GROUP_ID ='", this.Session["Procedure_Code"].ToString(), "',@I_IMAGE_ID ='", str6.ToString(), "',@SZ_EVENT_ID ='", dataSet1.Tables[0].Rows[j][0].ToString(), "'" };
                                sqlDataAdapter = new SqlDataAdapter(string.Concat(strArrays), sqlConnection);
                                dataSet = new DataSet();
                                sqlDataAdapter.Fill(dataSet);
                                Bill_Sys_MasterBilling.log.Debug("executed SP_UPLOAD_REPORT_FOR_VISIT  ");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }

        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs args)
    {
        if (this.ddlSpeciality.SelectedValue == "NA")
        {
            this.Btn_Selected.Visible = false;
            this.Btn_Patient.Visible = false;
            this.grdMasterBilling.Visible = false;
            this.lblCaseType.Visible = false;
            this.extddlCaseType.Visible = false;
            return;
        }
        this.con1.SourceGrid = this.grdMasterBilling;
        this.txtSearchBox.SourceGrid = this.grdMasterBilling;
        this.grdMasterBilling.Page = this.Page;
        this.grdMasterBilling.PageNumberList = this.con1;
        this.grdMasterBilling.XGridKey = "MasterBilling";
        this.txtSpeciality.Text = this.ddlSpeciality.SelectedValue.ToString();
        this.grdMasterBilling.XGridBindSearch();
        if (this.grdMasterBilling.RecordCount > 0)
        {
            this.Btn_Patient.Visible = true;
            this.Btn_Selected.Visible = true;
            this.lblCaseType.Visible = true;
            this.extddlCaseType.Visible = true;
            for (int i = 0; i < this.grdMasterBilling.Rows.Count; i++)
            {
                string str = this.grdMasterBilling.DataKeys[i]["BT_FINALIZE"].ToString();
                CheckBox checkBox = (CheckBox)this.grdMasterBilling.Rows[i].FindControl("chkSelect");
                if (str.ToLower() != "false")
                {
                    checkBox.Enabled = true;
                }
                else
                {
                    checkBox.Enabled = false;
                }
            }
        }
    }

    protected void extddlCaseType_SelectedIndexChanged(object sender, EventArgs args)
    {
        this.con1.SourceGrid = this.grdMasterBilling;
        this.txtSearchBox.SourceGrid = this.grdMasterBilling;
        this.grdMasterBilling.Page = this.Page;
        this.grdMasterBilling.PageNumberList = this.con1;
        this.txtCaseType.Text = this.extddlCaseType.Text;
        this.txtSpeciality.Text = this.ddlSpeciality.SelectedValue.ToString();
        this.grdMasterBilling.XGridBindSearch();
        if (this.grdMasterBilling.RecordCount > 0)
        {
            this.Btn_Patient.Visible = true;
            this.Btn_Selected.Visible = true;
            for (int i = 0; i < this.grdMasterBilling.Rows.Count; i++)
            {
                string str = this.grdMasterBilling.DataKeys[i]["BT_FINALIZE"].ToString();
                CheckBox checkBox = (CheckBox)this.grdMasterBilling.Rows[i].FindControl("chkSelect");
                if (str.ToLower() != "false")
                {
                    checkBox.Enabled = true;
                }
                else
                {
                    checkBox.Enabled = false;
                }
            }
        }
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
        string[] companyName;
        try
        {
            string pSzSpeciality = p_szSpeciality;
            this.Session["TM_SZ_CASE_ID"] = this.txtCaseID.Text;
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
            string str = "";
            string str1 = "";
            arrayLists.Add(billSysVerificationDesc);
            nodeType = this._bill_Sys_BillTransaction.Get_Node_Type(arrayLists);
            if (!nodeType.Contains("NFVER"))
            {
                str = "NEW";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    companyName = new string[] { ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Medicals/", pSzSpeciality, "/Bills/" };
                    str1 = string.Concat(companyName);
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template = new Bill_Sys_NF3_Template();
                    companyName = new string[] { billSysNF3Template.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Medicals/", pSzSpeciality, "/Bills/" };
                    str1 = string.Concat(companyName);
                }
            }
            else
            {
                str = "OLD";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    companyName = new string[] { ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Bills/", pSzSpeciality, "/" };
                    str1 = string.Concat(companyName);
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template1 = new Bill_Sys_NF3_Template();
                    companyName = new string[] { billSysNF3Template1.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/No Fault File/Bills/", pSzSpeciality, "/" };
                    str1 = string.Concat(companyName);
                }
            }
            CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
            string sZCOMPANYID1 = "";
            sZCOMPANYID1 = (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) ? ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID : ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
            if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                string str2 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str2 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template2 = new Bill_Sys_NF3_Template();
                    str2 = string.Concat(billSysNF3Template2.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                }
                string str3 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str3 = string.Concat(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template3 = new Bill_Sys_NF3_Template();
                    str3 = string.Concat(billSysNF3Template3.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), "/", this.Session["TM_SZ_CASE_ID"].ToString(), "/Packet Document/");
                }
                string str4 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                string str5 = "";
                Bill_Sys_Configuration billSysConfiguration = new Bill_Sys_Configuration();
                string configurationSettings = billSysConfiguration.getConfigurationSettings(sZCOMPANYID1, "GET_DIAG_PAGE_POSITION");
                string configurationSettings1 = billSysConfiguration.getConfigurationSettings(sZCOMPANYID1, "DIAG_PAGE");
                string str6 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                string str7 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                string str8 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                string str9 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                GenerateNF3PDF generateNF3PDF = new GenerateNF3PDF();
                this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                string str10 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str10 = generateNF3PDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str4);
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template4 = new Bill_Sys_NF3_Template();
                    str10 = generateNF3PDF.GeneratePDF(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, billSysNF3Template4.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str4);
                }
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str5 = this.objPDFReplacement.ReplacePDFvalues(str6, str7, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template5 = new Bill_Sys_NF3_Template();
                    str5 = this.objPDFReplacement.ReplacePDFvalues(str6, str7, this.Session["TM_SZ_BILL_ID"].ToString(), billSysNF3Template5.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                }
                string str11 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str11 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str5, str10);
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template6 = new Bill_Sys_NF3_Template();
                    str11 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, billSysNF3Template6.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str5, str10);
                }
                string str12 = "";
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    str12 = this.objPDFReplacement.ReplacePDFvalues(str8, str9, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    Bill_Sys_NF3_Template billSysNF3Template7 = new Bill_Sys_NF3_Template();
                    str12 = this.objPDFReplacement.ReplacePDFvalues(str8, str9, this.Session["TM_SZ_BILL_ID"].ToString(), billSysNF3Template7.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                }
                MergePDF.MergePDFFiles(string.Concat(this.objNF3Template.getPhysicalPath(), str2, str11), string.Concat(this.objNF3Template.getPhysicalPath(), str2, str12), string.Concat(this.objNF3Template.getPhysicalPath(), str2, str12.Replace(".pdf", "_MER.pdf")));
                this.szLastPDFFileName = str12.Replace(".pdf", "_MER.pdf");
                string str13 = "";
                str13 = string.Concat(str2, this.szLastPDFFileName);
                string str14 = "";
                str14 = string.Concat(ApplicationSettings.GetParameterValue("DocumentManagerURL"), str13);
                string str15 = string.Concat(this.objNF3Template.getPhysicalPath(), "/", str13);
                CutePDFDocumentClass cutePDFDocumentClass = new CutePDFDocumentClass();
                string str16 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                cutePDFDocumentClass.initialize(str16);
                if (cutePDFDocumentClass != null && File.Exists(str15) && configurationSettings1 != "CI_0000003" && this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5 && configurationSettings == "CK_0000003" && (configurationSettings1 != "CI_0000004" || this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))
                {
                    str10 = str15.Replace(".pdf", "_NewMerge.pdf");
                }
                string str17 = "";
                if (File.Exists(str15) && File.Exists(str15.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str13 = str15.Replace(".pdf", "_New.pdf").ToString();
                }
                if (!File.Exists(str15) || !File.Exists(str15.Replace(".pdf", "_NewMerge.pdf").ToString()))
                {
                    str17 = (!File.Exists(str15) || !File.Exists(str15.Replace(".pdf", "_New.pdf").ToString()) ? str14.ToString() : str14.Replace(".pdf", "_New.pdf").ToString());
                }
                else
                {
                    str17 = str14.Replace(".pdf", "_NewMerge.pdf").ToString();
                }
                string str18 = "";
                string[] strArrays = str17.Split(new char[] { '/' });
                ArrayList arrayLists1 = new ArrayList();
                str17 = str17.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                str18 = strArrays[(int)strArrays.Length - 1].ToString();
                if (File.Exists(string.Concat(this.objNF3Template.getPhysicalPath(), str3, str18)))
                {
                    if (!Directory.Exists(string.Concat(this.objNF3Template.getPhysicalPath(), str1)))
                    {
                        Directory.CreateDirectory(string.Concat(this.objNF3Template.getPhysicalPath(), str1));
                    }
                    File.Copy(string.Concat(this.objNF3Template.getPhysicalPath(), str3, str18), string.Concat(this.objNF3Template.getPhysicalPath(), str1, str18));
                }
                if (str != "OLD")
                {
                    arrayLists1.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                    arrayLists1.Add(string.Concat(str1, str18));
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
                    arrayLists1.Add(str1);
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
                    arrayLists1.Add(string.Concat(str1, str18));
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
                    arrayLists1.Add(str1);
                    arrayLists1.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    arrayLists1.Add(pSzSpeciality);
                    arrayLists1.Add("NF");
                    arrayLists1.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    this.objNF3Template.saveGeneratedBillPath(arrayLists1);
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO()
                {
                    SZ_MESSAGE_TITLE = "BILL_GENERATED",
                    SZ_ACTIVITY_DESC = str18
                };
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = this.txtCaseID.Text;
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY || !(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                else
                {
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
            }
            else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
            {
                Bill_Sys_PVT_Template billSysPVTTemplate = new Bill_Sys_PVT_Template();
                bool bTREFERRINGFACILITY = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str19 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str20 = this.Session["TM_SZ_BILL_ID"].ToString();
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
                    sZCOMPANYID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                billSysPVTTemplate.GeneratePVTBill(bTREFERRINGFACILITY, sZCOMPANYID, str19, pSzSpeciality, sZCOMPANYNAME, str20, sZUSERNAME, sZUSERID);
            }
            else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000001")
            {
                if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    this.hdnWCPDFBillNumber.Value = this.Session["TM_SZ_BILL_ID"].ToString();
                    string sZUSERID1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    string sZUSERNAME1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string sZCOMPANYNAME1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    string sZCOMPANYID2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    string sZCASENO = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                    WC_Bill_Generation wCBillGeneration = new WC_Bill_Generation();
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    string str21 = "";
                    foreach (GridViewRow row in this.grdMasterBilling.Rows)
                    {
                        if (!((CheckBox)row.Cells[10].FindControl("chkSelect")).Checked)
                        {
                            continue;
                        }
                        DropDownList dropDownList = (DropDownList)row.FindControl("drpWC");
                        str21 = (dropDownList == null ? "0" : dropDownList.SelectedValue);
                    }
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", wCBillGeneration.GeneratePDFForWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str21, sZCOMPANYID2, sZCOMPANYNAME1, pSzSpeciality, sZUSERNAME1, sZCASENO, this._bill_Sys_BillTransaction.GetDoctorSpeciality(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), 0), "');"), true);
                }
                else
                {
                    string sZUSERID2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    string sZUSERNAME2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string sZCOMPANYNAME2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    sZCOMPANYID1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    string str22 = (new WC_Bill_Generation()).GeneratePDFForReferalWorkerComp((string)this.Session["TM_SZ_BILL_ID"], ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, sZCOMPANYID1, sZCOMPANYNAME2, sZUSERID2, sZUSERNAME2, p_szSpeciality);
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("window.open('", str22, "');"), true);
                }
            }
            else if (caseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
            {
                (new Lien()).GenratePdfForLien(this.txtCompanyID.Text, p_szBillNumber, this._bill_Sys_BillTransaction.GetDoctorSpeciality(p_szBillNumber, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
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
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GET_COMPLIANTS_USING_EVENTID(string sz_Event_ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_MasterBilling.log.Debug("Start GET_COMPLIANTS_USING_EVENTID method.");
        DataSet dataSet = new DataSet();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_GET_PT_COMPLAINTS", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@I_EVENT_ID", sz_Event_ID);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
                Bill_Sys_MasterBilling.log.Debug(string.Concat("SP_GET_PT_COMPLAINTS : ", dataSet.Tables[0].Rows.Count));
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GET_PROCEDURECODE_USING_EVENTID(string sz_Event_ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = new DataSet();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GET_PROCEDURE_CODE_USING_EVENT_ID", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@I_EVENT_ID", sz_Event_ID);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getApplicationSetting(string p_szKey)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        sqlConnection.Open();
        string str = "";
        SqlDataReader sqlDataReader = (new SqlCommand(string.Concat("select ParameterValue from tblapplicationsettings where parametername = '", p_szKey.ToString().Trim(), "'"), sqlConnection)).ExecuteReader();
        while (sqlDataReader.Read())
        {
            str = sqlDataReader["parametervalue"].ToString();
        }
        return str;
    }

    public DataSet GetChairoView(string eventID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = new DataSet();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_CHAIRO_NOTES", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@I_EVENT_ID ", eventID);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
            }
            catch (SqlException ex)
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public string GetCompanyName(string szCompanyId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string str = "";
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GET_COMPANY_NAME", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                str = dataSet.Tables[0].Rows[0][0].ToString();
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return str;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getPacketDocumentFolder(string p_szPath, string p_szCompanyID, string p_szCaseID)
    {
        p_szPath = string.Concat(p_szPath, p_szCompanyID, "/", p_szCaseID);
        if (!Directory.Exists(p_szPath))
        {
            Directory.CreateDirectory(p_szPath);
            Directory.CreateDirectory(string.Concat(p_szPath, "/Packet Document"));
        }
        else if (!Directory.Exists(string.Concat(p_szPath, "/Packet Document")))
        {
            Directory.CreateDirectory(string.Concat(p_szPath, "/Packet Document"));
        }
        return string.Concat(p_szPath, "/Packet Document/");
    }

    public DataSet GetProc(string eventid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                if (this.Sqlcon.State == ConnectionState.Closed)
                {
                    this.Sqlcon.Open();
                }
                SqlCommand sqlCommand = new SqlCommand("get_proce_data", this.Sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@sz_company_id", this.txtCompanyID.Text);
                sqlCommand.Parameters.AddWithValue("@I_event_id", eventid);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
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
        finally
        {
            if (this.Sqlcon.State == ConnectionState.Open)
            {
                this.Sqlcon.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetPtview(string eventID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_MasterBilling.log.Debug("Start GetPtView method.");
        DataSet dataSet = new DataSet();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_PT_NOTES", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@I_EVENT_ID ", eventID);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
                Bill_Sys_MasterBilling.log.Debug(string.Concat("SP_PT_NOTES : ", dataSet.Tables[0].Rows.Count));
                Bill_Sys_MasterBilling.log.Debug("End of GetPtView method.");
            }
            catch (SqlException ex)
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdMasterBilling_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (e.CommandName.ToString() == "Editt")
        {
            try
            {
                Bil_Sys_Associate_Diagnosis bilSysAssociateDiagnosi = new Bil_Sys_Associate_Diagnosis();
                string[] strArrays = e.CommandArgument.ToString().Split(new char[] { ',' });
                string str = "";
                int num = 0;
                string[] strArrays1 = strArrays;
                for (int i = 0; i < (int)strArrays1.Length; i++)
                {
                    string str1 = strArrays1[i];
                    if (str1 == "")
                    {
                        break;
                    }
                    str = string.Concat(str, str1);
                    num++;
                }
                bilSysAssociateDiagnosi.EventProcID = str;
                bilSysAssociateDiagnosi.DoctorID = strArrays[num + 1].ToString();
                bilSysAssociateDiagnosi.CaseID = strArrays[num + 2].ToString();
                bilSysAssociateDiagnosi.ProceuderGroupId = strArrays[num + 3].ToString();
                this.Session["DIAGNOS_ASSOCIATION"] = bilSysAssociateDiagnosi;
                ScriptManager.RegisterStartupScript(this, base.GetType(), "MM123", "<script type='text/javascript'>showReceiveDocumentPopup();</script>", false);
                this.divid.Visible = true;
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
        if (e.CommandName.ToString() == "DocManager")
        {
            CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
            Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject();
            //{
            billSysCaseObject.SZ_PATIENT_ID = caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
            billSysCaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
            billSysCaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            billSysCaseObject.SZ_CASE_NO = caseDetailsBO.GetCaseNo(billSysCaseObject.SZ_CASE_ID, billSysCaseObject.SZ_COMAPNY_ID.ToString());
            billSysCaseObject.SZ_PATIENT_NAME = caseDetailsBO.GetPatientName(billSysCaseObject.SZ_PATIENT_ID);
            // };
            this.Session["CASE_OBJECT"] = billSysCaseObject;
            this.Session["Case_ID"] = e.CommandArgument.ToString();
            this.Session["QStrCaseID"] = e.CommandArgument.ToString();
            this.Session["QStrCID"] = e.CommandArgument.ToString();
            this.Session["SN"] = "0";
            this.Session["Archived"] = "0";
            ScriptManager.RegisterStartupScript(this, base.GetType(), "MMss1231", "<script type='text/javascript'>window.open('../Document Manager/case/vb_CaseInformation.aspx', 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');</script>", false);
        }
    }

   

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat("window.location.href ='", ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET"), this.grdMasterBilling.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")), "';"), true);
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
            this.Btn_Patient.Attributes.Add("onclick", "return Validatedelete();");
            this.Btn_Selected.Attributes.Add("onclick", "return Validatedelete();");
            this.sz_CompanyID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            this.txtCompanyID.Text = this.sz_CompanyID;
            this.txtFromDate.Text = DateTime.Parse("Jan 1, 2000").ToString();
            this.txtToDate.Text = DateTime.Parse("Jan 1, 2010").ToString();
            this.con1.SourceGrid = this.grdMasterBilling;
            this.txtSearchBox.SourceGrid = this.grdMasterBilling;
            this.grdMasterBilling.Page = this.Page;
            this.grdMasterBilling.PageNumberList = this.con1;
            this.extddlCaseType.Flag_ID = this.txtCompanyID.Text;
            if (this.grdMasterBilling.RecordCount > 0)
            {
                for (int i = 0; i < this.grdMasterBilling.Rows.Count; i++)
                {
                    string str = this.grdMasterBilling.DataKeys[i]["BT_FINALIZE"].ToString();
                    CheckBox checkBox = (CheckBox)this.grdMasterBilling.Rows[i].FindControl("chkSelect");
                    if (str.ToLower() != "false")
                    {
                        checkBox.Enabled = true;
                    }
                    else
                    {
                        checkBox.Enabled = false;
                    }
                }
            }
            if (!this.Page.IsPostBack)
            {
                this.lblCaseType.Visible = false;
                this.extddlCaseType.Visible = false;
                this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                this.grdMasterBilling.XGridBind();
                if (this.grdMasterBilling.RecordCount <= 0)
                {
                    this.Btn_Selected.Visible = false;
                    this.Btn_Patient.Visible = false;
                }
                else
                {
                    this.Btn_Patient.Visible = true;
                    this.Btn_Selected.Visible = true;
                }
                for (int j = 0; j < this.grdMasterBilling.Rows.Count; j++)
                {
                    string str1 = this.grdMasterBilling.DataKeys[j]["BT_FINALIZE"].ToString();
                    CheckBox checkBox1 = (CheckBox)this.grdMasterBilling.Rows[j].FindControl("chkSelect");
                    if (str1.ToLower() != "false")
                    {
                        checkBox1.Enabled = true;
                    }
                    else
                    {
                        checkBox1.Enabled = false;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < this.grdMasterBilling.Rows.Count; i++)
        {
            string str = this.grdMasterBilling.DataKeys[i]["BT_FINALIZE"].ToString();
            CheckBox checkBox = (CheckBox)this.grdMasterBilling.Rows[i].FindControl("chkSelect");
            if (str.ToLower() != "false")
            {
                checkBox.Enabled = true;
            }
            else
            {
                checkBox.Enabled = false;
            }
        }
    }

    protected void PMTest(ArrayList pdfbills)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        PMNotes_PDF pMNotesPDF = new PMNotes_PDF();
        ArrayList arrayLists = new ArrayList();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        try
        {
            try
            {
                Bill_Sys_MasterBilling.log.Debug("Start PMTest method.");
                for (int i = 0; i < pdfbills.Count; i++)
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat("exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='", pdfbills[i].ToString(), "', @FLAG='PDF'"), sqlConnection);
                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);
                    if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("SP_PDFBILLS_MASTERBILLING :", dataSet.Tables[0].Rows.Count));
                        object[] item = new object[] { "FUReport_", pdfbills[i], "_", null, null };
                        item[3] = DateTime.Now.ToString("yyyyMMddhhmmss");
                        item[4] = ".pdf";
                        string str = string.Concat(item);
                        string str1 = string.Concat(dataSet.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, dataSet.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')), "/", dataSet.Tables[0].Rows[0]["CASEID"].ToString(), "/No Fault File/Medicals/PM/FUReport/");
                        string str2 = string.Concat(dataSet.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, dataSet.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')), "\\", dataSet.Tables[0].Rows[0]["CASEID"].ToString(), "\\No Fault File\\Medicals\\PM\\FUReport\\");
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("pdfpath : ", ConfigurationManager.AppSettings["DocumentManagerURL"], str1, str));
                        Bill_Sys_MasterBilling.log.Debug("Start creating directory if directory not exist.");
                        string physicalPath = (new Bill_Sys_NF3_Template()).getPhysicalPath();
                        if (!Directory.Exists(string.Concat(physicalPath, str1)))
                        {
                            Directory.CreateDirectory(string.Concat(physicalPath, str1));
                        }
                        Bill_Sys_MasterBilling.log.Debug("creating directory successful.");
                        string str3 = string.Concat(physicalPath, str1, str);
                        string str4 = pdfbills[i].ToString();
                        string sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        string sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        pMNotesPDF.GeneratePMReport(str3, str4, sZUSERID, sZCOMPANYID);
                        string str5 = dataSet.Tables[0].Rows[0]["CASEID"].ToString();
                        string str6 = this.Session["Procedure_Code"].ToString();
                        string[] sZUSERNAME = new string[] { "exec SP_INSERT_PM_BILLING_REPORT_TO_DOCMANAGER @SZ_CASE_ID='", str5, "', @SZ_FILE_NAME='", str, "', @SZ_FILE_PATH='", str1, "', @SZ_LOGIN_ID ='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "',@SZ_SPECIALITY='", str6, "'" };
                        sqlDataAdapter = new SqlDataAdapter(string.Concat(sZUSERNAME), sqlConnection);
                        dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        str2 = string.Concat(dataSet.Tables[1].Rows[0][0].ToString(), str2).Replace("\\\\", "/").Replace("\\", "/");
                        Bill_Sys_MasterBilling.log.Debug("document close.");
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("SP_INSERT_PM_BILLING_REPORT_TO_DOCMANAGER : ", dataSet.Tables[0].Rows.Count));
                        string str7 = dataSet.Tables[2].Rows[0][0].ToString();
                        sqlDataAdapter = new SqlDataAdapter(string.Concat("exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='", pdfbills[i].ToString(), "', @FLAG='GET_EVENT_ID'"), sqlConnection);
                        DataSet dataSet1 = new DataSet();
                        sqlDataAdapter.Fill(dataSet1);
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("SP_PDFBILLS_MASTERBILLING : ", dataSet1.Tables[0].Rows.Count));
                        if (dataSet1.Tables.Count >= 0)
                        {
                            for (int j = 0; j < dataSet1.Tables[0].Rows.Count; j++)
                            {
                                string[] strArrays = new string[] { "exec SP_UPLOAD_REPORT_FOR_VISIT  @SZ_USER_NAME='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "',@SZ_FILE_NAME='", str, "',@SZ_COMPANY_ID='", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "', @SZ_CASE_ID='", str5.ToString(), "', @SZ_USER_ID ='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, "',@SZ_PROCEDURE_GROUP_ID ='", this.Session["Procedure_Code"].ToString(), "',@I_IMAGE_ID ='", str7.ToString(), "',@SZ_EVENT_ID ='", dataSet1.Tables[0].Rows[j][0].ToString(), "'" };
                                sqlDataAdapter = new SqlDataAdapter(string.Concat(strArrays), sqlConnection);
                                dataSet = new DataSet();
                                sqlDataAdapter.Fill(dataSet);
                                Bill_Sys_MasterBilling.log.Debug("SP_UPLOAD_REPORT_FOR_VISIT executed.");
                            }
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
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }

        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void PTBillsPerPatient(ArrayList pdfbills)
    {
    }

    protected void PTTest(ArrayList pdfbills)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        PTNotes_PDF pTNotesPDF = new PTNotes_PDF();
        ArrayList arrayLists = new ArrayList();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        try
        {
            try
            {
                Bill_Sys_MasterBilling.log.Debug("Start PTTest method.");
                for (int i = 0; i < pdfbills.Count; i++)
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat("exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='", pdfbills[i].ToString(), "', @FLAG='PDF'"), sqlConnection);
                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);
                    if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("SP_PDFBILLS_MASTERBILLING :", dataSet.Tables[0].Rows.Count));
                        object[] item = new object[] { "FUReport_", pdfbills[i], "_", null, null };
                        item[3] = DateTime.Now.ToString("yyyyMMddhhmmss");
                        item[4] = ".pdf";
                        string str = string.Concat(item);
                        string str1 = string.Concat(dataSet.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, dataSet.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')), "/", dataSet.Tables[0].Rows[0]["CASEID"].ToString(), "/No Fault File/Medicals/PT/FUReport/");
                        string str2 = string.Concat(dataSet.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, dataSet.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')), "\\", dataSet.Tables[0].Rows[0]["CASEID"].ToString(), "\\No Fault File\\Medicals\\PT\\FUReport\\");
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("pdfpath : ", ConfigurationManager.AppSettings["DocumentManagerURL"], str1, str));
                        Bill_Sys_MasterBilling.log.Debug("Start creating directory if directory not exist.");
                        string physicalPath = (new Bill_Sys_NF3_Template()).getPhysicalPath();
                        if (!Directory.Exists(string.Concat(physicalPath, str1)))
                        {
                            Directory.CreateDirectory(string.Concat(physicalPath, str1));
                        }
                        Bill_Sys_MasterBilling.log.Debug("creating directory successful.");
                        string str3 = string.Concat(physicalPath, str1, str);
                        string str4 = pdfbills[i].ToString();
                        string sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        string sZCOMPANYID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        pTNotesPDF.GeneratePTReport(str3, str4, sZUSERID, sZCOMPANYID);
                        string str5 = dataSet.Tables[0].Rows[0]["CASEID"].ToString();
                        string[] sZUSERNAME = new string[] { "exec SP_INSERT_PT_BILLING_REPORT_TO_DOCMANAGER @SZ_CASE_ID='", str5, "', @SZ_FILE_NAME='", str, "', @SZ_FILE_PATH='", str1, "', @SZ_LOGIN_ID ='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "'" };
                        sqlDataAdapter = new SqlDataAdapter(string.Concat(sZUSERNAME), sqlConnection);
                        dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        str2 = string.Concat(dataSet.Tables[1].Rows[0][0].ToString(), str2).Replace("\\\\", "/").Replace("\\", "/");
                        Bill_Sys_MasterBilling.log.Debug("document close.");
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("SP_INSERT_PT_BILLING_REPORT_TO_DOCMANAGER : ", dataSet.Tables[0].Rows.Count));
                        string str6 = dataSet.Tables[2].Rows[0][0].ToString();
                        sqlDataAdapter = new SqlDataAdapter(string.Concat("exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='", pdfbills[i].ToString(), "', @FLAG='GET_EVENT_ID'"), sqlConnection);
                        DataSet dataSet1 = new DataSet();
                        sqlDataAdapter.Fill(dataSet1);
                        Bill_Sys_MasterBilling.log.Debug(string.Concat("SP_PDFBILLS_MASTERBILLING : ", dataSet1.Tables[0].Rows.Count));
                        if (dataSet1.Tables.Count >= 0)
                        {
                            for (int j = 0; j < dataSet1.Tables[0].Rows.Count; j++)
                            {
                                string[] strArrays = new string[] { "exec SP_UPLOAD_REPORT_FOR_VISIT  @SZ_USER_NAME='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "',@SZ_FILE_NAME='", str, "',@SZ_COMPANY_ID='", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "', @SZ_CASE_ID='", str5.ToString(), "', @SZ_USER_ID ='", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, "',@SZ_PROCEDURE_GROUP_ID ='", this.Session["Procedure_Code"].ToString(), "',@I_IMAGE_ID ='", str6.ToString(), "',@SZ_EVENT_ID ='", dataSet1.Tables[0].Rows[j][0].ToString(), "'" };
                                sqlDataAdapter = new SqlDataAdapter(string.Concat(strArrays), sqlConnection);
                                dataSet = new DataSet();
                                sqlDataAdapter.Fill(dataSet);
                                Bill_Sys_MasterBilling.log.Debug("SP_UPLOAD_REPORT_FOR_VISIT executed.");
                            }
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
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}