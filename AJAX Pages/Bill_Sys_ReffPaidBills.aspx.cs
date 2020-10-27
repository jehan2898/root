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
using PDFValueReplacement;
using System.IO;
using System.Text;
using PDFValueReplacement;
using mbs.LienBills;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using mbs.bill;
using GeneratePDFFile;
using CUTEFORMCOLib;
using System.Data;
public partial class AJAX_Pages_Bill_Sys_ReffPaidBills : PageBase
{
    string PopUpFlag;
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    public DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    MUVGenerateFunction _MUVGenerateFunction;
    Bill_Sys_NF3_Template objNF3Template;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    string bt_include;
    String str_1500;
    private string SESSION_checks = "SESSION_Checked";
    private Bill_Sys_Menu _bill_Sys_Menu;
    private Bill_Sys_InsertDefaultValues objDefaultValue;
    CaseDetailsBO objCaseDetailsBO;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    StringBuilder szExportoExcelColumname = new StringBuilder();
    StringBuilder szExportoExcelField = new StringBuilder();
    private static log4net.ILog log = log4net.LogManager.GetLogger("AddBillDiffCase()");

    public void AddBillDiffCase()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            string str = "";
            DataTable doctorID = new DataTable();
            string latestBillID = "";
            int num = 0;
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            DataTable table2 = new DataTable();
            table2.Columns.Add("SZ_CASE_ID");
            table2.Columns.Add("SZ_PATIENT_ID");
            table2.Columns.Add("CHART NO");
            table2.Columns.Add("PATIENT NAME");
            table2.Columns.Add("DATE OF SERVICE");
            table2.Columns.Add("Patient name");
            table2.Columns.Add("Date Of Service");
            table2.Columns.Add("Procedure code");
            table2.Columns.Add("Description");
            table2.Columns.Add("Status");
            table2.Columns.Add("Code ID");
            table2.Columns.Add("EVENT ID");
            table2.Columns.Add("Doctor ID");
            table2.Columns.Add("CASE NO");
            table2.Columns.Add("Company ID");
            table2.Columns.Add("SZ_PATIENT_TREATMENT_ID");
            table2.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            table2.Columns.Add("SZ_STUDY_NUMBER");
            table2.Columns.Add("Insurance_Company");

            doctorID = new Bill_Sys_ReportBO().GetDoctorID(this.txtCompanyID.Text);
            for (int i = 0; i < this.grdAllReports.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdAllReports.Rows[i].FindControl("chkSelect");
                if (box.Checked)
                {
                    DataRow row = table2.NewRow();
                    row["SZ_CASE_ID"] = this.grdAllReports.DataKeys[i]["SZ_CASE_ID"].ToString();
                    row["SZ_PATIENT_ID"] = this.grdAllReports.DataKeys[i]["SZ_PATIENT_ID"].ToString();
                    row["CHART NO"] = this.grdAllReports.Rows[i].Cells[3].Text.ToString();
                    row["PATIENT NAME"] = this.grdAllReports.Rows[i].Cells[6].Text.ToString();
                    row["DATE OF SERVICE"] = this.grdAllReports.DataKeys[i]["DT_DATE_OF_SERVICE"].ToString();
                    row["Patient name"] = this.grdAllReports.Rows[i].Cells[7].Text.ToString();
                    row["Date Of Service"] = this.grdAllReports.DataKeys[i]["DT_DATE_OF_SERVICE"].ToString();
                    row["Procedure code"] = this.grdAllReports.Rows[i].Cells[9].Text.ToString();
                    row["Description"] = this.grdAllReports.Rows[i].Cells[10].Text.ToString();
                    row["Status"] = this.grdAllReports.Rows[i].Cells[14].Text.ToString();
                    row["Code ID"] = this.grdAllReports.DataKeys[i]["SZ_CODE_ID"].ToString();
                    row["EVENT ID"] = this.grdAllReports.DataKeys[i]["SZ_EVENT_ID"].ToString();
                    row["Doctor ID"] = this.grdAllReports.DataKeys[i]["DOCTOR_ID"].ToString();
                    row["CASE NO"] = this.grdAllReports.DataKeys[i]["CASE_NO"].ToString();
                    row["Company ID"] = this.grdAllReports.DataKeys[i]["Company_ID"].ToString();
                    row["SZ_PATIENT_TREATMENT_ID"] = this.grdAllReports.DataKeys[i]["SZ_PATIENT_TREATMENT_ID"].ToString();
                    row["SZ_PROCEDURE_GROUP_ID"] = this.grdAllReports.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                    row["SZ_STUDY_NUMBER"] = this.grdAllReports.Rows[i].Cells[0x1c].Text.ToString();
                    row["Insurance_Company"] = this.grdAllReports.DataKeys[i]["Insurance_Company"].ToString();
                    table2.Rows.Add(row);
                }
            }
            table2.DefaultView.Sort = "SZ_CASE_ID ASC";
            table2 = table2.DefaultView.ToTable();
            Bill_Sys_ReportBO tbo = new Bill_Sys_ReportBO();
            doctorID = tbo.GetDoctorID(this.txtCompanyID.Text);
            for (int j = 0; j < table2.Rows.Count; j++)
            {
                DataSet set;
                DataSet procCodeDetails;
                ArrayList list3;
                ArrayList list4;
                ArrayList list5;
                this.Session["Procedure_Code"] = table2.Rows[j]["SZ_PROCEDURE_GROUP_ID"].ToString();
                num = 0;
                string str3 = table2.Rows[j]["SZ_PATIENT_ID"].ToString();
                string str4 = table2.Rows[j]["SZ_PROCEDURE_GROUP_ID"].ToString();

                DataTable dtProcCode = new System.Data.DataTable();
                dtProcCode.Columns.Add("sz_type_code_id");

                foreach (DataRow row2 in table2.Rows)
                {
                    if ((str3 == row2["SZ_PATIENT_ID"].ToString()) && (str4 == row2["SZ_PROCEDURE_GROUP_ID"].ToString()))
                    {
                        num++;
                        DataRow drRow = dtProcCode.NewRow();
                        drRow["sz_type_code_id"] = row2["Code ID"].ToString();
                        dtProcCode.Rows.Add(drRow);
                    }
                }
                if (num == 1)
                {
                    this.txtCaseID.Text = table2.Rows[j]["SZ_CASE_ID"].ToString();
                    this.txtReadingDocID.Text = table2.Rows[j]["Doctor ID"].ToString();
                    this.txtPatientID.Text = table2.Rows[j]["SZ_PATIENT_ID"].ToString();
                    this.txtCaseNo.Text = table2.Rows[j]["CASE NO"].ToString();
                    string szInuranceID = table2.Rows[j]["Insurance_Company"].ToString();
                    string szSpecilaty = table2.Rows[j]["SZ_PROCEDURE_GROUP_ID"].ToString();
                    str = table2.Rows[j]["Company ID"].ToString();
                    Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                    obj2.SZ_CASE_ID = this.txtCaseID.Text;
                    obj2.SZ_COMAPNY_ID = str;
                    obj2.SZ_CASE_NO = this.txtCaseNo.Text;
                    obj2.SZ_PATIENT_ID = this.txtPatientID.Text;
                    this.Session["CASE_OBJECT"] = obj2;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        this.txtRefCompanyID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        this.txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    //set = new DataSet();
                    //set = ebo.GetDoctorDiagnosisCode(str4, this.txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                    if (lstDiagnosisCodes.Items.Count > 0)
                    {
                        BillTransactionEO objBillTransactionEO = new BillTransactionEO();
                        //list3 = new ArrayList();
                        //this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        //tbo = new Bill_Sys_ReportBO();
                        //list3.Add(this.txtCaseID.Text);
                        //list3.Add(this.txtBillDate.Text);
                        //list3.Add(this.txtCompanyID.Text);
                        //list3.Add(doctorID.Rows[0]["CODE"].ToString());
                        //list3.Add("0");
                        //list3.Add(this.txtReadingDocID.Text);
                        //list3.Add(this.txtRefCompanyID.Text);
                        //list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                        //list3.Add(this.Session["Procedure_Code"].ToString());
                        // tbo.InsertBillTransactionData(list3);

                        objBillTransactionEO.SZ_CASE_ID = this.txtCaseID.Text;
                        objBillTransactionEO.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
                        objBillTransactionEO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                        objBillTransactionEO.SZ_DOCTOR_ID = doctorID.Rows[0]["CODE"].ToString();
                        objBillTransactionEO.SZ_TYPE = "0";
                        objBillTransactionEO.SZ_READING_DOCTOR_ID = this.txtReadingDocID.Text;
                        objBillTransactionEO.SZ_Referring_Company_Id = this.txtRefCompanyID.Text;
                        objBillTransactionEO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        objBillTransactionEO.SZ_PROCEDURE_GROUP_ID = this.Session["Procedure_Code"].ToString();

                        //this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                        //latestBillID = this._bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(this.txtCompanyID.Text.ToString());
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = latestBillID + " Bill Created";
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = this.txtCaseID.Text;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                        //this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);

                        //tbo = new Bill_Sys_ReportBO();
                        //list3 = new ArrayList();
                        //list3.Add(this.txtPatientID.Text);
                        //list3.Add(table2.Rows[j]["Code ID"].ToString());
                        //list3.Add(this.txtCompanyID.Text);
                        //list3.Add(doctorID.Rows[0]["CODE"].ToString());
                        //tbo.GetTreatmentID(list3);

                        list4 = new ArrayList();
                        list4.Add(table2.Rows[j]["Procedure code"].ToString());
                        string str5 = table2.Rows[j]["Description"].ToString();
                        if (str5.Contains("&quot;"))
                        {
                            str5 = str5.Replace("&quot;", "\"");
                        }
                        if (str5.Contains("&amp;"))
                        {
                            str5 = str5.Replace("&amp;", "&");
                        }
                        list4.Add(str5);
                        list4.Add(this.txtCompanyID.Text);
                        tbo = new Bill_Sys_ReportBO();
                        procCodeDetails = new DataSet();
                        procCodeDetails = tbo.GetProcCodeDetails(list4);

                        int iProcCount = 0;


                        CyclicCode objCyclicCode = new CyclicCode();
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CYCLIC_PROCEDURE_CODE == "1")
                        {
                            objCyclicCode = GetCyclicCode(txtCaseID.Text, txtCompanyID.Text, szInuranceID, szSpecilaty, dtProcCode);
                            iProcCount = objCyclicCode.i_Count + 1;
                        }
                        // table2.Rows[j]["SZ_CASE_ID"].ToString()
                        //((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID



                        list5 = new ArrayList();
                        float contractAmount = 0;
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CYCLIC_PROCEDURE_CODE == "1")
                        {
                            BillProcedureCodeEO billProcedureCodeEO = new BillProcedureCodeEO();
                            DataTable dtAmt = objCyclicCode.ProcValue;
                            string szCaseID = table2.Rows[j]["SZ_CASE_ID"].ToString();
                            string szprocedurCode = procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString();
                            string szCompanyId = this.txtCompanyID.Text;
                            string szProcedurGroupID = table2.Rows[j]["SZ_PROCEDURE_GROUP_ID"].ToString();
                            BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                            //  list5.Add(procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString());
                            objBillProcedureCodeEO.SZ_PROCEDURE_ID = procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString();

                            //list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                            objBillProcedureCodeEO.FL_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();

                            //  list5.Add(latestBillID);

                            //list5.Add(table2.Rows[j]["DATE OF SERVICE"].ToString());
                            objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(table2.Rows[j]["DATE OF SERVICE"].ToString());

                            //  list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                            {
                                if (((table2.Rows[j]["SZ_STUDY_NUMBER"].ToString() == "") || (table2.Rows[j]["SZ_STUDY_NUMBER"].ToString() == null)) || (table2.Rows[j]["SZ_STUDY_NUMBER"].ToString() == "&nbsp;"))
                                {
                                    // list5.Add("1");
                                    objBillProcedureCodeEO.I_UNIT = "1";
                                }
                                else
                                {
                                    // list5.Add(table2.Rows[j]["SZ_STUDY_NUMBER"].ToString());
                                    objBillProcedureCodeEO.I_UNIT = table2.Rows[j]["SZ_STUDY_NUMBER"].ToString();
                                }
                            }
                            else
                            {
                                //list5.Add("1");
                                objBillProcedureCodeEO.I_UNIT = "1";
                            }


                            //  list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point


                            if (objCyclicCode.i_Flag == 0)
                            {
                                // list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                objBillProcedureCodeEO.DOCT_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                            }
                            else
                            {

                                //  iProcCount = GetProcCount(objCyclicCode.AllProc,iProcCount);
                                if (iProcCount >= 4)
                                {
                                    iProcCount = 4;
                                }
                                if (objCyclicCode.sz_configuraton == "specialty")
                                {

                                    DataRow[] drAmoutRow = dtAmt.Select("i_count=" + iProcCount);
                                    iProcCount++;
                                    // list5.Add(drAmoutRow[0]["mn_amount"].ToString());
                                    objBillProcedureCodeEO.DOCT_AMOUNT = drAmoutRow[0]["mn_amount"].ToString();

                                }

                                else
                                {
                                    //need a change-aarti

                                    DataRow[] drAmoutRow = dtAmt.Select("i_count=" + iProcCount + "and sz_type_code_id='" + table2.Rows[j]["Code ID"].ToString() + "'");
                                    iProcCount++;
                                    // list5.Add(drAmoutRow[0]["mn_amount"].ToString());
                                    if (drAmoutRow[0]["s_type"].ToString() != "amount")
                                    {
                                        double numAmount = 0;
                                        numAmount = Convert.ToDouble(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString()) * Convert.ToDouble(drAmoutRow[0]["n_percent"].ToString()) / 100;
                                        objBillProcedureCodeEO.DOCT_AMOUNT = numAmount.ToString();
                                    }
                                    else
                                    {
                                        objBillProcedureCodeEO.DOCT_AMOUNT = drAmoutRow[0]["mn_amount"].ToString();
                                    }


                                }
                            }


                            //  list5.Add("1");
                            objBillProcedureCodeEO.FLT_FACTOR = "1";
                            //   list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                            objBillProcedureCodeEO.FLT_PRICE = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();


                            //     list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                            objBillProcedureCodeEO.PROC_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();


                            //    list5.Add(doctorID.Rows[0]["CODE"].ToString());
                            objBillProcedureCodeEO.SZ_DOCTOR_ID = doctorID.Rows[0]["CODE"].ToString();

                            //list5.Add(table2.Rows[j]["SZ_CASE_ID"].ToString());
                            objBillProcedureCodeEO.SZ_CASE_ID = table2.Rows[j]["SZ_CASE_ID"].ToString();

                            //  list5.Add(table2.Rows[j]["Code ID"].ToString());
                            objBillProcedureCodeEO.SZ_TYPE_CODE_ID = table2.Rows[j]["Code ID"].ToString();
                            //  list5.Add("");
                            objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                            //list5.Add("");
                            objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                            //   list5.Add(table2.Rows[j]["SZ_PATIENT_TREATMENT_ID"].ToString());
                            objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID = table2.Rows[j]["SZ_PATIENT_TREATMENT_ID"].ToString();

                            //list5.Add((iProcCount-1).ToString());
                            objBillProcedureCodeEO.i_cyclic_id = (iProcCount - 1).ToString();

                            if (objCyclicCode.i_Flag == 0)
                            {
                                // list5.Add("0");
                                objBillProcedureCodeEO.bt_cyclic_applied = "0";
                            }
                            else
                            {
                                //list5.Add("1");
                                objBillProcedureCodeEO.bt_cyclic_applied = "1";
                            }
                            contractAmount = contractAmount + (float.Parse(objBillProcedureCodeEO.DOCT_AMOUNT) * float.Parse(objBillProcedureCodeEO.I_UNIT));
                            //list5.Add(objBillProcedureCodeEO);

                        }
                        //else
                        {

                            // list5.Add(procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString());
                            BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                            objBillProcedureCodeEO.SZ_PROCEDURE_ID = procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString();
                            // list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                            objBillProcedureCodeEO.FL_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                            //list5.Add(latestBillID);
                            //list5.Add(table2.Rows[j]["DATE OF SERVICE"].ToString());
                            objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(table2.Rows[j]["DATE OF SERVICE"].ToString());

                            //  list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                            {
                                if (((table2.Rows[j]["SZ_STUDY_NUMBER"].ToString() == "") || (table2.Rows[j]["SZ_STUDY_NUMBER"].ToString() == null)) || (table2.Rows[j]["SZ_STUDY_NUMBER"].ToString() == "&nbsp;"))
                                {
                                    // list5.Add("1");
                                    objBillProcedureCodeEO.I_UNIT = "1";
                                }
                                else
                                {
                                    //  list5.Add(table2.Rows[j]["SZ_STUDY_NUMBER"].ToString());
                                    objBillProcedureCodeEO.I_UNIT = table2.Rows[j]["SZ_STUDY_NUMBER"].ToString();
                                }
                            }
                            else
                            {
                                // list5.Add("1");
                                objBillProcedureCodeEO.I_UNIT = "1";
                            }


                            // list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                            objBillProcedureCodeEO.FLT_PRICE = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();

                            //list5.Add("1");
                            objBillProcedureCodeEO.FLT_FACTOR = "1";

                            //  list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                            objBillProcedureCodeEO.DOCT_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                            //list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                            objBillProcedureCodeEO.PROC_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();

                            //list5.Add(doctorID.Rows[0]["CODE"].ToString());
                            objBillProcedureCodeEO.SZ_DOCTOR_ID = doctorID.Rows[0]["CODE"].ToString();

                            //list5.Add(table2.Rows[j]["SZ_CASE_ID"].ToString());
                            objBillProcedureCodeEO.SZ_CASE_ID = table2.Rows[j]["SZ_CASE_ID"].ToString();

                            //list5.Add(table2.Rows[j]["Code ID"].ToString());
                            objBillProcedureCodeEO.SZ_TYPE_CODE_ID = table2.Rows[j]["Code ID"].ToString();
                            //  list5.Add("");
                            objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                            //list5.Add("");
                            objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";

                            //list5.Add(table2.Rows[j]["SZ_PATIENT_TREATMENT_ID"].ToString());
                            objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID = table2.Rows[j]["SZ_PATIENT_TREATMENT_ID"].ToString();
                            // this._bill_Sys_BillTransaction.SaveTransactionData(list5);
                            list5.Add(objBillProcedureCodeEO);
                        }

                        //this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        //this._bill_Sys_BillTransaction.DeleteTransactionDiagnosis(latestBillID);

                        ArrayList list6 = new ArrayList();
                        if (lstDiagnosisCodes.Items.Count > 0)
                        {
                            for (int k = 0; k < lstDiagnosisCodes.Items.Count; k++)
                            {
                                BillDiagnosisCodeEO billDiagnosisCodeEO = new BillDiagnosisCodeEO();

                                billDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID = lstDiagnosisCodes.Items[k].Value;
                                list6.Add(billDiagnosisCodeEO);


                            }
                        }

                        Referral_Bill_Transaction objReferral_Bill_Transaction = new Referral_Bill_Transaction();
                        Result result = new Result();
                        result = objReferral_Bill_Transaction.saveReferralBill(objBillTransactionEO, _DAO_NOTES_EO, list5, list6,contractAmount);
                        if (result.msg_code != "ERR")
                        {
                            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                            {
                                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                string docSpeciality = this._bill_Sys_BillTransaction.GetDocSpeciality(result.bill_no);
                                this.GenerateAddedBillPDF(result.bill_no, docSpeciality);
                                list2.Add(table2.Rows[j]["CASE NO"].ToString());
                            }
                            else
                            {
                                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                string str7 = this._bill_Sys_BillTransaction.GetDocSpeciality(result.bill_no);
                                this.GenerateAddedBillPDF(result.bill_no, str7);
                                list2.Add(table2.Rows[j]["CASE NO"].ToString());
                            }
                        }
                        else
                        {
                            this.usrMessage.PutMessage(result.msg);
                            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                            this.usrMessage.Show();
                        }
                    }
                    else
                    {
                        list.Add(table2.Rows[j]["CASE NO"].ToString());
                    }
                }
                else if (num > 1)
                {
                    this.txtCaseID.Text = table2.Rows[j]["SZ_CASE_ID"].ToString();
                    this.txtReadingDocID.Text = table2.Rows[j]["Doctor ID"].ToString();
                    this.txtPatientID.Text = table2.Rows[j]["SZ_PATIENT_ID"].ToString();
                    this.txtCaseNo.Text = table2.Rows[j]["CASE NO"].ToString();
                    string szInuranceID = table2.Rows[j]["Insurance_Company"].ToString();
                    string szSpecilaty = table2.Rows[j]["SZ_PROCEDURE_GROUP_ID"].ToString();
                    str = table2.Rows[j]["Company ID"].ToString();
                    Bill_Sys_CaseObject obj3 = new Bill_Sys_CaseObject();
                    obj3.SZ_CASE_ID = this.txtCaseID.Text;
                    obj3.SZ_COMAPNY_ID = str;
                    obj3.SZ_CASE_NO = this.txtCaseNo.Text;
                    obj3.SZ_PATIENT_ID = this.txtPatientID.Text;
                    this.Session["CASE_OBJECT"] = obj3;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        this.txtRefCompanyID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        this.txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    //set = new DataSet();
                    //set = ebo.GetDoctorDiagnosisCode(table2.Rows[j]["SZ_PROCEDURE_GROUP_ID"].ToString(), this.txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                    if (lstDiagnosisCodes.Items.Count > 0)
                    {
                        //list3 = new ArrayList();
                        //this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        //tbo = new Bill_Sys_ReportBO();
                        //list3.Add(this.txtCaseID.Text);
                        //list3.Add(this.txtBillDate.Text);
                        //list3.Add(this.txtCompanyID.Text);
                        //list3.Add(doctorID.Rows[0]["CODE"].ToString());
                        //list3.Add("0");
                        //list3.Add(this.txtReadingDocID.Text);
                        //list3.Add(this.txtRefCompanyID.Text);
                        //list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                        //list3.Add(this.Session["Procedure_Code"].ToString());
                        //tbo.InsertBillTransactionData(list3);



                        BillTransactionEO objBillTransactionEO = new BillTransactionEO();
                        objBillTransactionEO.SZ_CASE_ID = this.txtCaseID.Text;
                        objBillTransactionEO.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
                        objBillTransactionEO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                        objBillTransactionEO.SZ_DOCTOR_ID = doctorID.Rows[0]["CODE"].ToString();
                        objBillTransactionEO.SZ_TYPE = "0";
                        objBillTransactionEO.SZ_READING_DOCTOR_ID = this.txtReadingDocID.Text;
                        objBillTransactionEO.SZ_Referring_Company_Id = this.txtRefCompanyID.Text;
                        objBillTransactionEO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        objBillTransactionEO.SZ_PROCEDURE_GROUP_ID = this.Session["Procedure_Code"].ToString();

                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = latestBillID + " Bill Created";
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = this.txtCaseID.Text;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;


                        //this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                        //latestBillID = this._bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(this.txtCompanyID.Text.ToString());
                        //this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        //this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_CREATED";
                        //this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=latestBillID;
                        //this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        //this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        //this._DAO_NOTES_EO.SZ_CASE_ID=this.txtCaseID.Text;
                        //this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        //this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        //tbo = new Bill_Sys_ReportBO();
                        //list3 = new ArrayList();
                        //list3.Add(this.txtPatientID.Text);
                        //list3.Add(table2.Rows[j]["Code ID"].ToString());
                        //list3.Add(this.txtCompanyID.Text);
                        //list3.Add(doctorID.Rows[0]["CODE"].ToString());
                        //tbo.GetTreatmentID(list3);


                        list4 = new ArrayList();
                        list4.Add(table2.Rows[j]["Procedure code"].ToString());
                        string str8 = table2.Rows[j]["Description"].ToString();
                        if (str8.Contains("&quot;"))
                        {
                            str8 = str8.Replace("&quot;", "\"");
                        }
                        if (str8.Contains("&amp;"))
                        {
                            str8 = str8.Replace("&amp;", "&");
                        }
                        list4.Add(str8);
                        list4.Add(this.txtCompanyID.Text);

                        tbo = new Bill_Sys_ReportBO();
                        procCodeDetails = new DataSet();
                        procCodeDetails = tbo.GetProcCodeDetails(list4);
                        int iProcCount = 0;


                        CyclicCode objCyclicCode = new CyclicCode();
                       
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CYCLIC_PROCEDURE_CODE == "1")
                        {
                            objCyclicCode = GetCyclicCode(txtCaseID.Text, txtCompanyID.Text, szInuranceID, szSpecilaty, dtProcCode);
                            iProcCount = objCyclicCode.i_Count + 1;
                        }
                        float contractAmount = 0;
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CYCLIC_PROCEDURE_CODE == "1")
                        {
                            DataTable dtAmt = objCyclicCode.ProcValue;
                            list5 = new ArrayList();
                            for (int m = 0; m < num; m++)
                            {
                                list4 = new ArrayList();
                                list4.Add(table2.Rows[m + j]["Procedure code"].ToString());
                                string str9 = table2.Rows[m + j]["Description"].ToString();
                                if (str9.Contains("&quot;"))
                                {
                                    str9 = str9.Replace("&quot;", "\"");
                                }
                                if (str9.Contains("&amp;"))
                                {
                                    str9 = str9.Replace("&amp;", "&");
                                }
                                list4.Add(str9);
                                list4.Add(this.txtCompanyID.Text);
                                tbo = new Bill_Sys_ReportBO();
                                procCodeDetails = new DataSet();
                                procCodeDetails = tbo.GetProcCodeDetails(list4);
                                BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                                //list5.Add(procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString());
                                objBillProcedureCodeEO.SZ_PROCEDURE_ID = procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString();

                                // list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                objBillProcedureCodeEO.FL_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();

                                // list5.Add(latestBillID);
                                //  list5.Add(table2.Rows[m + j]["DATE OF SERVICE"].ToString());
                                objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(table2.Rows[m + j]["DATE OF SERVICE"].ToString());

                                //   list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                                {
                                    if (((table2.Rows[m + j]["SZ_STUDY_NUMBER"].ToString() == "") || (table2.Rows[m + j]["SZ_STUDY_NUMBER"].ToString() == null)) || (table2.Rows[m + j]["SZ_STUDY_NUMBER"].ToString() == "&nbsp;"))
                                    {
                                        // list5.Add("1");
                                        objBillProcedureCodeEO.I_UNIT = "1";
                                    }
                                    else
                                    {
                                        //list5.Add(table2.Rows[m + j]["SZ_STUDY_NUMBER"].ToString());
                                        objBillProcedureCodeEO.I_UNIT = table2.Rows[m + j]["SZ_STUDY_NUMBER"].ToString();
                                    }
                                }
                                else
                                {
                                    //list5.Add("1");
                                    objBillProcedureCodeEO.I_UNIT = "1";
                                }
                                // list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point                               
                                if (objCyclicCode.i_Flag == 0)
                                {
                                    //  list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                    objBillProcedureCodeEO.DOCT_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                                }
                                else
                                {

                                    //                                    iProcCount = GetProcCount(objCyclicCode.AllProc, iProcCount);
                                    if (iProcCount >= 4)
                                    {
                                        iProcCount = 4;
                                    }
                                    if (objCyclicCode.sz_configuraton == "specialty")
                                    {

                                        DataRow[] drAmoutRow = dtAmt.Select("i_count=" + iProcCount);
                                        iProcCount++;
                                        // list5.Add(drAmoutRow[0]["mn_amount"].ToString());
                                        objBillProcedureCodeEO.DOCT_AMOUNT = drAmoutRow[0]["mn_amount"].ToString();

                                    }
                                    else
                                    {

                                        DataRow[] drAmoutRow = dtAmt.Select("i_count=" + iProcCount + "and sz_type_code_id='" + table2.Rows[m + j]["Code ID"].ToString() + "'");
                                        iProcCount++;
                                        // list5.Add(drAmoutRow[0]["mn_amount"].ToString());
                                        // objBillProcedureCodeEO.DOCT_AMOUNT = drAmoutRow[0]["mn_amount"].ToString();
                                        if (drAmoutRow[0]["s_type"].ToString() != "amount")
                                        {
                                            double numAmount = 0;
                                            numAmount = Convert.ToDouble(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString()) * Convert.ToDouble(drAmoutRow[0]["n_percent"].ToString()) / 100;
                                            objBillProcedureCodeEO.DOCT_AMOUNT = numAmount.ToString();
                                        }
                                        else
                                        {
                                            objBillProcedureCodeEO.DOCT_AMOUNT = drAmoutRow[0]["mn_amount"].ToString();
                                        }

                                    }
                                }

                                //  list5.Add("1");
                                objBillProcedureCodeEO.FLT_FACTOR = "1";
                                // list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point                                
                                objBillProcedureCodeEO.FLT_PRICE = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();

                                //  list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                objBillProcedureCodeEO.PROC_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();

                                //   list5.Add(doctorID.Rows[0]["CODE"].ToString());
                                objBillProcedureCodeEO.SZ_DOCTOR_ID = doctorID.Rows[0]["CODE"].ToString();

                                //list5.Add(table2.Rows[m + j]["SZ_CASE_ID"].ToString());
                                objBillProcedureCodeEO.SZ_CASE_ID = table2.Rows[m + j]["SZ_CASE_ID"].ToString();
                                // list5.Add(table2.Rows[m + j]["Code ID"].ToString());
                                objBillProcedureCodeEO.SZ_TYPE_CODE_ID = table2.Rows[m + j]["Code ID"].ToString();
                                // list5.Add("");
                                objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                                //  list5.Add("");
                                objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";

                                //  list5.Add(table2.Rows[m + j]["SZ_PATIENT_TREATMENT_ID"].ToString());
                                objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID = table2.Rows[m + j]["SZ_PATIENT_TREATMENT_ID"].ToString();
                                //     list5.Add((iProcCount-1).ToString());
                                objBillProcedureCodeEO.i_cyclic_id = (iProcCount - 1).ToString();
                                if (objCyclicCode.i_Flag == 0)
                                {
                                    //list5.Add("0");
                                    objBillProcedureCodeEO.bt_cyclic_applied = "0";
                                }
                                else
                                {
                                    //list5.Add("1");
                                    objBillProcedureCodeEO.bt_cyclic_applied = "1";
                                }
                                // this._bill_Sys_BillTransaction.SaveTransactionData(list5);
                                contractAmount = contractAmount + (float.Parse(objBillProcedureCodeEO.DOCT_AMOUNT) * float.Parse(objBillProcedureCodeEO.I_UNIT));
                                //list5.Add(objBillProcedureCodeEO);
                            }
                        }
                       // else
                        {
                            list5 = new ArrayList();
                            for (int m = 0; m < num; m++)
                            {
                                list4 = new ArrayList();
                                list4.Add(table2.Rows[m + j]["Procedure code"].ToString());
                                string str9 = table2.Rows[m + j]["Description"].ToString();
                                if (str9.Contains("&quot;"))
                                {
                                    str9 = str9.Replace("&quot;", "\"");
                                }
                                if (str9.Contains("&amp;"))
                                {
                                    str9 = str9.Replace("&amp;", "&");
                                }
                                list4.Add(str9);
                                list4.Add(this.txtCompanyID.Text);
                                tbo = new Bill_Sys_ReportBO();
                                procCodeDetails = new DataSet();
                                procCodeDetails = tbo.GetProcCodeDetails(list4);

                                BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();
                                //    list5.Add(procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString());
                                objBillProcedureCodeEO.SZ_PROCEDURE_ID = procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString();
                                //  list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                objBillProcedureCodeEO.FL_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                                //list5.Add(latestBillID);
                                //     list5.Add(table2.Rows[m + j]["DATE OF SERVICE"].ToString());
                                objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(table2.Rows[m + j]["DATE OF SERVICE"].ToString());

                                //   list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                                {
                                    if (((table2.Rows[m + j]["SZ_STUDY_NUMBER"].ToString() == "") || (table2.Rows[m + j]["SZ_STUDY_NUMBER"].ToString() == null)) || (table2.Rows[m + j]["SZ_STUDY_NUMBER"].ToString() == "&nbsp;"))
                                    {
                                        // list5.Add("1");
                                        objBillProcedureCodeEO.I_UNIT = "1";
                                    }
                                    else
                                    {
                                        //  list5.Add(table2.Rows[m + j]["SZ_STUDY_NUMBER"].ToString());
                                        objBillProcedureCodeEO.I_UNIT = table2.Rows[m + j]["SZ_STUDY_NUMBER"].ToString();
                                    }
                                }
                                else
                                {
                                    // list5.Add("1");
                                    objBillProcedureCodeEO.I_UNIT = "1";
                                }

                                //list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                objBillProcedureCodeEO.FLT_PRICE = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                                //  list5.Add("1");
                                objBillProcedureCodeEO.FLT_FACTOR = "1";

                                //    list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                objBillProcedureCodeEO.DOCT_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();

                                //list5.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                objBillProcedureCodeEO.PROC_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                                // list5.Add(doctorID.Rows[0]["CODE"].ToString());
                                objBillProcedureCodeEO.SZ_DOCTOR_ID = doctorID.Rows[0]["CODE"].ToString();
                                //  list5.Add(table2.Rows[m + j]["SZ_CASE_ID"].ToString());
                                objBillProcedureCodeEO.SZ_CASE_ID = table2.Rows[m + j]["SZ_CASE_ID"].ToString();
                                //list5.Add(table2.Rows[m + j]["Code ID"].ToString());
                                objBillProcedureCodeEO.SZ_TYPE_CODE_ID = table2.Rows[m + j]["Code ID"].ToString();
                                //  list5.Add("");
                                objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                                //  list5.Add("");
                                objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                                //list5.Add(table2.Rows[m + j]["SZ_PATIENT_TREATMENT_ID"].ToString());
                                objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID = table2.Rows[m + j]["SZ_PATIENT_TREATMENT_ID"].ToString();
                                list5.Add(objBillProcedureCodeEO);
                                // this._bill_Sys_BillTransaction.SaveTransactionData(list5);
                            }
                        }



                        //this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        //this._bill_Sys_BillTransaction.DeleteTransactionDiagnosis(latestBillID);
                        ArrayList list6 = new ArrayList();
                        if (lstDiagnosisCodes.Items.Count > 0)
                        {
                            for (int n = 0; n < lstDiagnosisCodes.Items.Count; n++)
                            {
                                //this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                //list5 = new ArrayList();
                                //list5.Add(set.Tables[0].Rows[n]["CODE"].ToString());
                                //list5.Add(latestBillID);
                                //list5.Add(txtCompanyID.Text);
                                //this._bill_Sys_BillTransaction.SaveTransactionDiagnosis(list5);

                                BillDiagnosisCodeEO billDiagnosisCodeEO = new BillDiagnosisCodeEO();

                                billDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID = lstDiagnosisCodes.Items[n].Value;

                                list6.Add(billDiagnosisCodeEO);
                            }
                        }
                        Referral_Bill_Transaction objReferral_Bill_Transaction = new Referral_Bill_Transaction();
                        Result result = new Result();
                        result = objReferral_Bill_Transaction.saveReferralBill(objBillTransactionEO, _DAO_NOTES_EO, list5, list6,contractAmount);
                        if (result.msg_code != "ERR")
                        {
                            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                            {
                                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                string str10 = this._bill_Sys_BillTransaction.GetDocSpeciality(result.bill_no);
                                this.GenerateAddedBillPDF(result.bill_no, str10);
                                list2.Add(table2.Rows[j]["CASE NO"].ToString());
                            }
                            else
                            {
                                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                string str11 = this._bill_Sys_BillTransaction.GetDocSpeciality(result.bill_no);
                                this.GenerateAddedBillPDF(result.bill_no, str11);
                                list2.Add(table2.Rows[j]["CASE NO"].ToString());
                            }
                        }
                        else
                        {
                            this.usrMessage.PutMessage(result.msg);
                            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                            this.usrMessage.Show();
                        }
                    }
                    else
                    {
                        list.Add(table2.Rows[j]["CASE NO"].ToString());
                    }
                    j += num - 1;
                }
            }
            string str12 = "";
            if (list2.Count > 0)
            {
                for (int num7 = 0; num7 < list2.Count; num7++)
                {
                    if (num7 == (list2.Count - 1))
                    {
                        str12 = str12 + list2[num7].ToString();
                    }
                    else
                    {
                        str12 = str12 + list2[num7].ToString() + ", ";
                    }
                }
                hDnl.Value = "";
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "mm", "<script language='javascript'>alert('Bill Created Successfully For " + str12 + " CaseNo');</script>");
                this.usrMessage.PutMessage("Bill Created Successfully For " + str12 + " CaseNo");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            string str13 = "";
            if (list.Count > 0)
            {
                for (int num8 = 0; num8 < list.Count; num8++)
                {
                    str13 = str13 + list[num8].ToString() + ", ";
                }
                hDnl.Value = "";
                this.usrMessage.PutMessage("Cannot create bill for '" + str13 + "' as no diagnosis code assign for the patient");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            this.grdAllReports.XGridBindSearch();
        }
        catch (Exception ex)
        {
            hDnl.Value = "";
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

    public void AddBillSameCase()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = "";
            ArrayList list = new ArrayList();
            DataTable doctorID = new DataTable();
            string str2 = "";
            int num = 0;
            int num2 = 0;
            string latestBillID = "";
            int num3 = 0;
            string str4 = "";
            for (int i = 0; i < this.grdAllReports.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdAllReports.Rows[i].FindControl("chkSelect");
                if (box.Checked)
                {
                    str2 = this.grdAllReports.DataKeys[i]["SZ_PATIENT_ID"].ToString();
                }
            }
            for (int j = 0; j < this.grdAllReports.Rows.Count; j++)
            {
                CheckBox box2 = (CheckBox)this.grdAllReports.Rows[j].FindControl("chkSelect");
                if (box2.Checked)
                {
                    if (str2 == this.grdAllReports.DataKeys[j]["SZ_PATIENT_ID"].ToString())
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 2;
                        break;
                    }
                }
            }
            switch (num)
            {
                case 2:
                    hDnl.Value = "";
                    this.Page.ClientScript.RegisterStartupScript(base.GetType(), "mm", "<script language='javascript'>alert('Select visits for same patient');</script>");
                    this.usrMessage.PutMessage("Select visits for same patient");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage.Show();
                    return;

                case 1:
                    {
                        ArrayList list5 = new ArrayList();
                        for (int k = 0; k < this.grdAllReports.Rows.Count; k++)
                        {
                            CheckBox box3 = (CheckBox)this.grdAllReports.Rows[k].FindControl("chkSelect");
                            if (box3.Checked && ((((this.grdAllReports.DataKeys[k]["Insurance_Company"].ToString() == "&nbsp;") || (this.grdAllReports.DataKeys[k]["Insurance_Address"].ToString() == "&nbsp;")) || ((this.grdAllReports.DataKeys[k]["CLAIM_NO"].ToString() == "&nbsp;") || (this.grdAllReports.DataKeys[k]["Insurance_Company"].ToString() == ""))) || ((this.grdAllReports.DataKeys[k]["Insurance_Address"].ToString() == "") || (this.grdAllReports.DataKeys[k]["CLAIM_NO"].ToString() == ""))))
                            {
                                list5.Add(this.grdAllReports.DataKeys[k]["CASE_NO"].ToString());
                            }
                        }
                        string str5 = "";
                        string str6 = "";
                        if ((list5.Count > 0) && (this.PopUpFlag == "true"))
                        {
                            for (int m = 0; m < list5.Count; m++)
                            {
                                int num8 = 0;
                                str6 = list5[m].ToString();
                                str5 = str5 + list5[m].ToString() + ",";
                                for (int n = 0; n < list5.Count; n++)
                                {
                                    if (str6 == list5[n].ToString())
                                    {
                                        num8++;
                                    }
                                }
                                if (num8 > 1)
                                {
                                    m += num8 - 1;
                                }
                            }
                            hDnl.Value = "";
                            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "invalid123", "alert('You do not have a claim number or an insurance company or an insurance company address added to these Case NO " + str5 + ". You cannot proceed furher.')", true);
                            this.msgPatientExists.InnerText = "You do not have a claim number or an insurance company or an insurance company address added to these Case NO '" + str5 + "'. You cannot proceed furher.";
                            this.Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");
                        }
                        else
                        {
                            this.PopUpFlag = "true";
                            for (int num10 = 0; num10 < this.grdAllReports.Rows.Count; num10++)
                            {
                                CheckBox box4 = (CheckBox)this.grdAllReports.Rows[num10].FindControl("chkSelect");
                                if (box4.Checked)
                                {
                                    str4 = this.grdAllReports.DataKeys[num10]["SZ_PROCEDURE_GROUP_ID"].ToString();
                                    this.Session["Procedure_Code"] = str4;
                                }
                            }
                            for (int num11 = 0; num11 < this.grdAllReports.Rows.Count; num11++)
                            {
                                CheckBox box5 = (CheckBox)this.grdAllReports.Rows[num11].FindControl("chkSelect");
                                if (box5.Checked)
                                {
                                    if (str4 == this.grdAllReports.DataKeys[num11]["SZ_PROCEDURE_GROUP_ID"].ToString())
                                    {
                                        num3 = 1;
                                    }
                                    else
                                    {
                                        num3 = 2;
                                        break;
                                    }
                                }
                            }
                            if (num3 == 2)
                            {
                                hDnl.Value = "";
                                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "mm", "<script language='javascript'>alert('Select visits for same Speciality');</script>");
                                this.usrMessage.PutMessage("Select visits for same Speciality");
                                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                                this.usrMessage.Show();
                            }
                            else
                            {
                                Bill_Sys_ReportBO tbo = new Bill_Sys_ReportBO();
                                doctorID = tbo.GetDoctorID(this.txtCompanyID.Text);
                                Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
                                string szInuranceID = "", szSpecilaty = "";
                                DataTable dtProcCode = new System.Data.DataTable();
                                dtProcCode.Columns.Add("sz_type_code_id");
                                for (int num12 = 0; num12 < this.grdAllReports.Rows.Count; num12++)
                                {
                                    CheckBox box6 = (CheckBox)this.grdAllReports.Rows[num12].FindControl("chkSelect");
                                    if (box6.Checked)
                                    {
                                        num2 = 1;
                                        this.txtCaseID.Text = this.grdAllReports.DataKeys[num12]["SZ_CASE_ID"].ToString();
                                        this.txtReadingDocID.Text = this.grdAllReports.DataKeys[num12]["DOCTOR_ID"].ToString();
                                        this.txtPatientID.Text = this.grdAllReports.DataKeys[num12]["SZ_PATIENT_ID"].ToString();
                                        this.txtCaseNo.Text = this.grdAllReports.DataKeys[num12]["CASE_NO"].ToString();
                                        szSpecilaty = grdAllReports.DataKeys[num12]["SZ_PROCEDURE_GROUP_ID"].ToString();
                                        szInuranceID = this.grdAllReports.DataKeys[num12]["Insurance_Company"].ToString();
                                        str = this.grdAllReports.DataKeys[num12]["Company_ID"].ToString();
                                        DataRow drRow = dtProcCode.NewRow();
                                        drRow["sz_type_code_id"] = this.grdAllReports.DataKeys[num12]["SZ_CODE_ID"].ToString();
                                        dtProcCode.Rows.Add(drRow);
                                    }
                                }
                                Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                                obj2.SZ_CASE_ID = this.txtCaseID.Text;
                                obj2.SZ_COMAPNY_ID = str;
                                obj2.SZ_CASE_NO = this.txtCaseNo.Text;
                                obj2.SZ_PATIENT_ID = this.txtPatientID.Text;
                                this.Session["CASE_OBJECT"] = obj2;
                                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                                {
                                    this.txtRefCompanyID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                                }
                                else
                                {
                                    this.txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                }
                                if (num2 == 1)
                                {
                                    //DataSet set = new DataSet();
                                    //set = ebo.GetDoctorDiagnosisCode(str4, this.txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                                    if (lstDiagnosisCodes.Items.Count > 0)
                                    {
                                        ArrayList list4;
                                        ArrayList list2 = new ArrayList();
                                        //this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                        //tbo = new Bill_Sys_ReportBO();
                                        //list2.Add(this.txtCaseID.Text);
                                        //list2.Add(this.txtBillDate.Text);
                                        //list2.Add(this.txtCompanyID.Text);
                                        //list2.Add(doctorID.Rows[0]["CODE"].ToString());
                                        //list2.Add("0");
                                        //list2.Add(this.txtReadingDocID.Text);
                                        //list2.Add(this.txtRefCompanyID.Text);
                                        //list2.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                                        //list2.Add(this.Session["Procedure_Code"].ToString());
                                        //tbo.InsertBillTransactionData(list2);
                                        BillTransactionEO objBillTransactionEO = new BillTransactionEO();
                                        objBillTransactionEO.SZ_CASE_ID = this.txtCaseID.Text;
                                        objBillTransactionEO.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
                                        objBillTransactionEO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                                        objBillTransactionEO.SZ_DOCTOR_ID = doctorID.Rows[0]["CODE"].ToString();
                                        objBillTransactionEO.SZ_TYPE = "0";
                                        objBillTransactionEO.SZ_READING_DOCTOR_ID = this.txtReadingDocID.Text;
                                        objBillTransactionEO.SZ_Referring_Company_Id = this.txtRefCompanyID.Text;
                                        objBillTransactionEO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                                        objBillTransactionEO.SZ_PROCEDURE_GROUP_ID = this.Session["Procedure_Code"].ToString();

                                        //this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                                        //latestBillID = this._bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(this.txtCompanyID.Text.ToString());
                                        //this._DAO_NOTES_EO = new DAO_NOTES_EO();
                                        //this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_CREATED";
                                        //this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=latestBillID;
                                        //this._DAO_NOTES_BO = new DAO_NOTES_BO();
                                        //this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                                        //this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                                        //this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"])    .SZ_COMPANY_ID;
                                        //this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);

                                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = latestBillID + " Bill Created";
                                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                                        int iProcCount = 0;


                                        CyclicCode objCyclicCode = new CyclicCode();
                                        DataTable dtAmt = new DataTable();
                                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CYCLIC_PROCEDURE_CODE == "1")
                                        {
                                            objCyclicCode = GetCyclicCode(txtCaseID.Text, txtCompanyID.Text, szInuranceID, szSpecilaty, dtProcCode);
                                            if (objCyclicCode.i_Flag == 1)
                                            {
                                                iProcCount = objCyclicCode.i_Count + 1;
                                                dtAmt = objCyclicCode.ProcValue;
                                            }
                                        }
                                        float contractAmount = 0; 
                                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CYCLIC_PROCEDURE_CODE == "1")
                                        {
                                            //list4 = new ArrayList();
                                            for (int num13 = 0; num13 < this.grdAllReports.Rows.Count; num13++)
                                            {
                                                list2 = new ArrayList();
                                                CheckBox box7 = (CheckBox)this.grdAllReports.Rows[num13].FindControl("chkSelect");
                                                if (box7.Checked)
                                                {
                                                    //tbo = new Bill_Sys_ReportBO();
                                                    //list2 = new ArrayList();
                                                    //list2.Add(this.txtPatientID.Text);
                                                    //list2.Add(this.grdAllReports.DataKeys[num13]["SZ_CODE_ID"].ToString());
                                                    //list2.Add(this.txtCompanyID.Text);
                                                    //list2.Add(doctorID.Rows[0]["CODE"].ToString());
                                                    //tbo.GetTreatmentID(list2);

                                                    ArrayList list3 = new ArrayList();
                                                    list3.Add(this.grdAllReports.Rows[num13].Cells[9].Text.ToString());
                                                    string str7 = this.grdAllReports.Rows[num13].Cells[10].Text.ToString();
                                                    if (str7.Contains("&quot;"))
                                                    {
                                                        str7 = str7.Replace("&quot;", "\"");
                                                    }
                                                    if (str7.Contains("&amp;"))
                                                    {
                                                        str7 = str7.Replace("&amp;", "&");
                                                    }
                                                    list3.Add(str7);
                                                    list3.Add(this.txtCompanyID.Text);
                                                    tbo = new Bill_Sys_ReportBO();
                                                    DataSet procCodeDetails = new DataSet();
                                                    procCodeDetails = tbo.GetProcCodeDetails(list3);


                                                    BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();
                                                    //  list4.Add(procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString());
                                                    objBillProcedureCodeEO.SZ_PROCEDURE_ID = procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString();
                                                    //   list4.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                                    objBillProcedureCodeEO.FL_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                                                    //  list4.Add(latestBillID);

                                                    // list4.Add(this.grdAllReports.DataKeys[num13]["DT_DATE_OF_SERVICE"].ToString());
                                                    objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(this.grdAllReports.DataKeys[num13]["DT_DATE_OF_SERVICE"].ToString());
                                                    //    list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                                    objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                                                    {
                                                        if (((this.grdAllReports.DataKeys[num13]["SZ_STUDY_NUMBER"].ToString() == "") || (this.grdAllReports.DataKeys[num13]["SZ_STUDY_NUMBER"].ToString() == null)) || (this.grdAllReports.DataKeys[num13]["SZ_STUDY_NUMBER"].ToString() == "&nbsp;"))
                                                        {
                                                            // list4.Add("1");
                                                            objBillProcedureCodeEO.I_UNIT = "1";
                                                        }
                                                        else
                                                        {
                                                            // list4.Add(this.grdAllReports.DataKeys[num13]["SZ_STUDY_NUMBER"].ToString());
                                                            objBillProcedureCodeEO.I_UNIT = this.grdAllReports.DataKeys[num13]["SZ_STUDY_NUMBER"].ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // list4.Add("1");
                                                        objBillProcedureCodeEO.I_UNIT = "1";
                                                    }
                                                    // list4.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point

                                                    if (objCyclicCode.i_Flag == 0)
                                                    {
                                                        // list4.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());
                                                        objBillProcedureCodeEO.DOCT_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                                                    }
                                                    else
                                                    {
                                                        //need a chnage-aarti

                                                        //   iProcCount = GetProcCount(objCyclicCode.AllProc, iProcCount);
                                                        if (iProcCount >= 4)
                                                        {
                                                            iProcCount = 4;
                                                        }
                                                        if (objCyclicCode.sz_configuraton == "specialty")
                                                        {

                                                            DataRow[] drAmoutRow = dtAmt.Select("i_count=" + iProcCount);
                                                            iProcCount++;
                                                            // list4.Add(drAmoutRow[0]["mn_amount"].ToString());
                                                            objBillProcedureCodeEO.DOCT_AMOUNT = drAmoutRow[0]["mn_amount"].ToString();

                                                        }
                                                        else
                                                        {

                                                            DataRow[] drAmoutRow = dtAmt.Select("i_count=" + iProcCount + "and sz_type_code_id='" + this.grdAllReports.DataKeys[num13]["SZ_CODE_ID"].ToString() + "'");
                                                            iProcCount++;
                                                            // list4.Add(drAmoutRow[0]["mn_amount"].ToString());
                                                            //objBillProcedureCodeEO.DOCT_AMOUNT = drAmoutRow[0]["mn_amount"].ToString();
                                                            if (drAmoutRow[0]["s_type"].ToString() != "amount")
                                                            {
                                                                double numAmount = 0;
                                                                numAmount = Convert.ToDouble(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString()) * Convert.ToDouble(drAmoutRow[0]["n_percent"].ToString()) / 100;
                                                                objBillProcedureCodeEO.DOCT_AMOUNT = numAmount.ToString();
                                                            }
                                                            else
                                                            {
                                                                objBillProcedureCodeEO.DOCT_AMOUNT = drAmoutRow[0]["mn_amount"].ToString();
                                                            }

                                                        }
                                                    }

                                                    //  list4.Add("1");
                                                    objBillProcedureCodeEO.FLT_FACTOR = "1";
                                                    //    list4.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                                    objBillProcedureCodeEO.FLT_PRICE = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();


                                                    // list4.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                                    objBillProcedureCodeEO.PROC_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();


                                                    // list4.Add(doctorID.Rows[0]["CODE"].ToString());
                                                    objBillProcedureCodeEO.SZ_DOCTOR_ID = doctorID.Rows[0]["CODE"].ToString();
                                                    // list4.Add(this.grdAllReports.DataKeys[num13]["SZ_CASE_ID"].ToString());
                                                    objBillProcedureCodeEO.SZ_CASE_ID = this.grdAllReports.DataKeys[num13]["SZ_CASE_ID"].ToString();
                                                    // list4.Add(this.grdAllReports.DataKeys[num13]["SZ_CODE_ID"].ToString());
                                                    objBillProcedureCodeEO.SZ_TYPE_CODE_ID = this.grdAllReports.DataKeys[num13]["SZ_CODE_ID"].ToString();
                                                    // list4.Add("");
                                                    objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                                                    //  list4.Add("");
                                                    objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";

                                                    // list4.Add(this.grdAllReports.DataKeys[num13]["SZ_PATIENT_TREATMENT_ID"].ToString());
                                                    objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID = this.grdAllReports.DataKeys[num13]["SZ_PATIENT_TREATMENT_ID"].ToString();
                                                    // list4.Add((iProcCount-1).ToString());
                                                    objBillProcedureCodeEO.i_cyclic_id = (iProcCount - 1).ToString();
                                                    if (objCyclicCode.i_Flag == 0)
                                                    {
                                                        //  list4.Add("0");
                                                        objBillProcedureCodeEO.bt_cyclic_applied = "0";
                                                    }
                                                    else
                                                    {
                                                        // list4.Add("1");
                                                        objBillProcedureCodeEO.bt_cyclic_applied = "1";
                                                    }

                                                    // this._bill_Sys_BillTransaction.SaveTransactionData(list4);
                                                    //list4.Add(objBillProcedureCodeEO);
                                                    contractAmount = contractAmount + (float.Parse( objBillProcedureCodeEO.DOCT_AMOUNT) * float.Parse( objBillProcedureCodeEO.I_UNIT));
                                                }
                                            }
                                        }
                                        //else
                                        {
                                            list4 = new ArrayList();
                                            for (int num13 = 0; num13 < this.grdAllReports.Rows.Count; num13++)
                                            {
                                                list2 = new ArrayList();
                                                CheckBox box7 = (CheckBox)this.grdAllReports.Rows[num13].FindControl("chkSelect");
                                                if (box7.Checked)
                                                {
                                                    tbo = new Bill_Sys_ReportBO();
                                                    list2 = new ArrayList();
                                                    list2.Add(this.txtPatientID.Text);
                                                    list2.Add(this.grdAllReports.DataKeys[num13]["SZ_CODE_ID"].ToString());
                                                    list2.Add(this.txtCompanyID.Text);
                                                    list2.Add(doctorID.Rows[0]["CODE"].ToString());
                                                    tbo.GetTreatmentID(list2);
                                                    ArrayList list3 = new ArrayList();
                                                    list3.Add(this.grdAllReports.Rows[num13].Cells[9].Text.ToString());
                                                    string str7 = this.grdAllReports.Rows[num13].Cells[10].Text.ToString();
                                                    if (str7.Contains("&quot;"))
                                                    {
                                                        str7 = str7.Replace("&quot;", "\"");
                                                    }
                                                    if (str7.Contains("&amp;"))
                                                    {
                                                        str7 = str7.Replace("&amp;", "&");
                                                    }
                                                    list3.Add(str7);
                                                    list3.Add(this.txtCompanyID.Text);
                                                    tbo = new Bill_Sys_ReportBO();
                                                    DataSet procCodeDetails = new DataSet();
                                                    procCodeDetails = tbo.GetProcCodeDetails(list3);

                                                    BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();
                                                    //list4 = new ArrayList();
                                                    //  list4.Add(procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString());
                                                    objBillProcedureCodeEO.SZ_PROCEDURE_ID = procCodeDetails.Tables[0].Rows[0]["PROC_ID"].ToString();
                                                    // list4.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                                    objBillProcedureCodeEO.FL_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                                                    //list4.Add(latestBillID);

                                                    // list4.Add(this.grdAllReports.DataKeys[num13]["DT_DATE_OF_SERVICE"].ToString());
                                                    objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(this.grdAllReports.DataKeys[num13]["DT_DATE_OF_SERVICE"].ToString());

                                                    //  list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                                    objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                                                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                                                    {
                                                        if (((this.grdAllReports.DataKeys[num13]["SZ_STUDY_NUMBER"].ToString() == "") || (this.grdAllReports.DataKeys[num13]["SZ_STUDY_NUMBER"].ToString() == null)) || (this.grdAllReports.DataKeys[num13]["SZ_STUDY_NUMBER"].ToString() == "&nbsp;"))
                                                        {
                                                            // list4.Add("1");
                                                            objBillProcedureCodeEO.I_UNIT = "1";
                                                        }
                                                        else
                                                        {
                                                            //  list4.Add(this.grdAllReports.DataKeys[num13]["SZ_STUDY_NUMBER"].ToString());
                                                            objBillProcedureCodeEO.I_UNIT = this.grdAllReports.DataKeys[num13]["SZ_STUDY_NUMBER"].ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // list4.Add("1");
                                                        objBillProcedureCodeEO.I_UNIT = "1";
                                                    }
                                                    // list4.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                                    objBillProcedureCodeEO.FLT_PRICE = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                                                    //  list4.Add("1");
                                                    objBillProcedureCodeEO.FLT_FACTOR = "1";
                                                    //  list4.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                                    objBillProcedureCodeEO.DOCT_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                                                    // list4.Add(procCodeDetails.Tables[0].Rows[0]["AMT"].ToString());//check point
                                                    objBillProcedureCodeEO.PROC_AMOUNT = procCodeDetails.Tables[0].Rows[0]["AMT"].ToString();
                                                    // list4.Add(doctorID.Rows[0]["CODE"].ToString());
                                                    objBillProcedureCodeEO.SZ_DOCTOR_ID = doctorID.Rows[0]["CODE"].ToString();

                                                    // list4.Add(this.grdAllReports.DataKeys[num13]["SZ_CASE_ID"].ToString());
                                                    objBillProcedureCodeEO.SZ_CASE_ID = this.grdAllReports.DataKeys[num13]["SZ_CASE_ID"].ToString();
                                                    // list4.Add(this.grdAllReports.DataKeys[num13]["SZ_CODE_ID"].ToString());
                                                    objBillProcedureCodeEO.SZ_TYPE_CODE_ID = this.grdAllReports.DataKeys[num13]["SZ_CODE_ID"].ToString();
                                                    // list4.Add("");
                                                    objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                                                    //  list4.Add("");
                                                    objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                                                    //  list4.Add(this.grdAllReports.DataKeys[num13]["SZ_PATIENT_TREATMENT_ID"].ToString());
                                                    objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID = this.grdAllReports.DataKeys[num13]["SZ_PATIENT_TREATMENT_ID"].ToString();
                                                    list4.Add(objBillProcedureCodeEO);
                                                    // this._bill_Sys_BillTransaction.SaveTransactionData(list4);
                                                }
                                            }//end
                                        }
                                        //this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                        //this._bill_Sys_BillTransaction.DeleteTransactionDiagnosis(latestBillID);
                                        ArrayList list6 = new ArrayList();
                                        if (lstDiagnosisCodes.Items.Count > 0)
                                        {
                                            for (int num14 = 0; num14 < lstDiagnosisCodes.Items.Count; num14++)
                                            {
                                                //this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                                //list4 = new ArrayList();
                                                //list4.Add(set.Tables[0].Rows[num14]["CODE"].ToString());
                                                //list4.Add(latestBillID);

                                                //this._bill_Sys_BillTransaction.SaveTransactionDiagnosis(list4);
                                                BillDiagnosisCodeEO billDiagnosisCodeEO = new BillDiagnosisCodeEO();

                                                billDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID = lstDiagnosisCodes.Items[num14].Value;
                                                list6.Add(billDiagnosisCodeEO);
                                            }
                                        }
                                        Referral_Bill_Transaction objReferral_Bill_Transaction = new Referral_Bill_Transaction();
                                        Result result = new Result();
                                        result = objReferral_Bill_Transaction.saveReferralBill(objBillTransactionEO, _DAO_NOTES_EO, list4, list6, contractAmount);
                                        if (result.msg_code != "ERR")
                                        {
                                            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                                            {
                                                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                                string docSpeciality = this._bill_Sys_BillTransaction.GetDocSpeciality(result.bill_no);
                                                this.GenerateAddedBillPDF(result.bill_no, docSpeciality);
                                                list.Add(this.txtCaseNo.Text);
                                            }
                                            else
                                            {
                                                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                                string str9 = this._bill_Sys_BillTransaction.GetDocSpeciality(result.bill_no);
                                                this.GenerateAddedBillPDF(result.bill_no, str9);
                                                list.Add(this.txtCaseNo.Text);
                                            }
                                        }
                                        else
                                        {
                                            this.usrMessage.PutMessage(result.msg);
                                            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                                            this.usrMessage.Show();
                                        }
                                    }
                                    else
                                    {
                                        hDnl.Value = "";
                                        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "mm", "<script language='javascript'>alert('No diagnosis code assign for the patient');</script>");
                                        this.usrMessage.PutMessage("No diagnosis code assign for th");
                                        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                                        this.usrMessage.Show();
                                    }
                                }
                            }
                            if (list.Count > 0)
                            {
                                string str10 = list[0].ToString();
                                hDnl.Value = "";
                                this.usrMessage.PutMessage("Bill Created Successfully For " + str10 + " CaseNo");
                                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                                this.usrMessage.Show();
                                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "mm", "<script language='javascript'>alert('Bill Created Successfully For " + str10 + " CaseNo');</script>");
                            }
                            this.grdAllReports.XGridBindSearch();
                        }
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            hDnl.Value = "";
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

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + this.grdAllReports.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")) + "';", true);
    }

    protected void btnPerPatient_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this.grdAllReports.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdAllReports.Rows[i].FindControl("chkSelect");
                if (box.Checked && ((((this.grdAllReports.DataKeys[i]["Insurance_Company"].ToString() == "&nbsp;") || (this.grdAllReports.DataKeys[i]["Insurance_Address"].ToString() == "&nbsp;")) || ((this.grdAllReports.DataKeys[i]["CLAIM_NO"].ToString() == "&nbsp;") || (this.grdAllReports.DataKeys[i]["Insurance_Company"].ToString() == ""))) || ((this.grdAllReports.DataKeys[i]["Insurance_Address"].ToString() == "") || (this.grdAllReports.DataKeys[i]["CLAIM_NO"].ToString() == ""))))
                {
                    list.Add(this.grdAllReports.DataKeys[i]["CASE_NO"].ToString());
                }
            }
            string str = "";
            string str2 = "";
            if (list.Count > 0)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    int num3 = 0;
                    str2 = list[j].ToString();
                    str = str + list[j].ToString() + ",";
                    for (int k = 0; k < list.Count; k++)
                    {
                        if (str2 == list[k].ToString())
                        {
                            num3++;
                        }
                    }
                    if (num3 > 1)
                    {
                        j += num3 - 1;
                    }
                }
                hDnl.Value = "";
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "lksd23", "alert('You do not have a claim number or an insurance company or an insurance company address added to these Case NO: " + str + ".You cannot proceed furher')", true);
                this.usrMessage.PutMessage("You do not have a claim number or an insurance company or an insurance company address added to these Case NO: " + str + ".You cannot proceed furher");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                this.popupmsg.InnerText = "You do not have a claim number or an insurance company or an insurance company address added to these Case NO: '" + str + "'.You cannot proceed furher";
                this.Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage1();</script>");
            }
            else
            {
                this.AddBillDiffCase();


            }
            this.grdAllReports.XGridBindSearch();
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
            hDnl.Value = "";
            this.Session["Procedure_Code"] = null;
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnRevert_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.grdAllReports.Rows.Count; i++)
        {
            CheckBox box = (CheckBox)this.grdAllReports.Rows[i].FindControl("chkSelect");
            if (box.Checked)
            {
                new Bill_Sys_ReportBO().RevertReport(Convert.ToInt32(this.grdAllReports.DataKeys[i]["SZ_PATIENT_TREATMENT_ID"].ToString()));
                this.usrMessage.PutMessage("Report Reverted Successfully");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
        }
        this.grdAllReports.XGridBindSearch();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.txtCaseType.Text = this.extddlCaseType.Text;
        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
        {
            this.grdAllReports.XGridBindSearch();
        }
    }

    protected void btnSelectedBill_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.AddBillSameCase();
            this.grdAllReports.XGridBindSearch();
            hDnl.Value = "";
        }
        catch (Exception ex)
        {
            hDnl.Value = "";
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
            this.Session["Procedure_Code"] = null;
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void ConfigDashBoard()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO dbo = new DashBoardBO();
        try
        {
            foreach (DataRow row in dbo.GetConfigDashBoard(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE).Rows)
            {
                switch (row[0].ToString())
                {
                    case "Daily Appointment":
                        {
                            this.tblDailyAppointment.Visible = true;
                            continue;
                        }
                    case "Weekly Appointment":
                        {
                            this.tblWeeklyAppointment.Visible = true;
                            continue;
                        }
                    case "Bill Status":
                        {
                            this.tblBillStatus.Visible = true;
                            continue;
                        }
                    case "Desk":
                        {
                            this.tblDesk.Visible = true;
                            continue;
                        }
                    case "Missing Information":
                        {
                            this.tblMissingInfo.Visible = true;
                            continue;
                        }
                    case "Report Section":
                        {
                            this.tblReportSection.Visible = true;
                            continue;
                        }
                    case "Procedure Status":
                        {
                            this.tblBilledUnbilledProcCode.Visible = true;
                            continue;
                        }
                    case "Visits":
                        {
                            this.tblVisits.Visible = true;
                            this.grdTotalVisit.DataSource = dbo.getVisitDetails(this.txtCompanyID.Text, "TOTALCOUNT");
                            this.grdTotalVisit.DataBind();
                            this.grdVisit.DataSource = dbo.getVisitDetails(this.txtCompanyID.Text, "BILLEDVISIT");
                            this.grdVisit.DataBind();
                            this.grdUnVisit.DataSource = dbo.getVisitDetails(this.txtCompanyID.Text, "UNBILLEDVISIT");
                            this.grdUnVisit.DataBind();
                            continue;
                        }
                    case "Missing Speciality":
                        {
                            this.tblMissingSpeciality.Visible = true;
                            continue;
                        }
                    case "Patient Visit Status":
                        {
                            this.tblPatientVisitStatus.Visible = true;
                            continue;
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
            this.Session["TM_SZ_CASE_ID"] = this.txtCaseID.Text;
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
            if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                string str3 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
                    str3 = template.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                string str4 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template2 = new Bill_Sys_NF3_Template();
                    str4 = template2.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                string str5 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template3 = new Bill_Sys_NF3_Template();
                    str5 = template3.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
                }
                else
                {
                    str5 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
                }
                string str6 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                string str7 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                string str8 = "";
                Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
                string str13 = configuration.getConfigurationSettings(str2, "GET_DIAG_PAGE_POSITION");
                string str14 = configuration.getConfigurationSettings(str2, "DIAG_PAGE");
                string str9 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                string str10 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                string str11 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                string str12 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                GenerateNF3PDF enfpdf = new GenerateNF3PDF();
                this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                string str15 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template4 = new Bill_Sys_NF3_Template();
                    str15 = enfpdf.GeneratePDF(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, template4.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str6);
                }
                else
                {
                    str15 = enfpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str6);
                }
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template5 = new Bill_Sys_NF3_Template();
                    str8 = this.objPDFReplacement.ReplacePDFvalues(str9, str10, this.Session["TM_SZ_BILL_ID"].ToString(), template5.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    str8 = this.objPDFReplacement.ReplacePDFvalues(str9, str10, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                }
                string str16 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template6 = new Bill_Sys_NF3_Template();
                    str16 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, template6.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str8, str15);
                }
                else
                {
                    str16 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str8, str15);
                }
                string str17 = "";
                string str18 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template7 = new Bill_Sys_NF3_Template();
                    str18 = this.objPDFReplacement.ReplacePDFvalues(str11, str12, this.Session["TM_SZ_BILL_ID"].ToString(), template7.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID), this.Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    str18 = this.objPDFReplacement.ReplacePDFvalues(str11, str12, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                }
                string str20 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.bt_include = this._MUVGenerateFunction.get_bt_include(str20, str, "", "Speciality");
                string str19 = this._MUVGenerateFunction.get_bt_include(str20, "", "WC000000000000000002", "CaseType");
                if ((this.bt_include == "True") && (str19 == "True"))
                {
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                }
                MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str3 + str16, this.objNF3Template.getPhysicalPath() + str3 + str18, this.objNF3Template.getPhysicalPath() + str3 + str18.Replace(".pdf", "_MER.pdf"));
                str17 = str18.Replace(".pdf", "_MER.pdf");
                if ((this.bt_include == "True") && (str19 == "True"))
                {
                    MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str3 + str17, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                    str17 = this.str_1500.Replace(".pdf", "_MER.pdf");
                }
                string str21 = "";
                str21 = str3 + str17;
                string str22 = "";
                str22 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str21;
                string path = this.objNF3Template.getPhysicalPath() + "/" + str21;
                CutePDFDocumentClass class2 = new CutePDFDocumentClass();
                string str24 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                class2.initialize(str24);
                if ((((class2 != null) && File.Exists(path)) && ((str14 != "CI_0000003") && (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5))) && ((str13 == "CK_0000003") && ((str14 != "CI_0000004") || (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))))
                {
                    str15 = path.Replace(".pdf", "_NewMerge.pdf");
                }
                string str25 = "";
                if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str21 = path.Replace(".pdf", "_New.pdf").ToString();
                }
                if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_NewMerge.pdf").ToString()))
                {
                    str25 = str22.Replace(".pdf", "_NewMerge.pdf").ToString();
                }
                else if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str25 = str22.Replace(".pdf", "_New.pdf").ToString();
                }
                else
                {
                    str25 = str22.ToString();
                }
                string str26 = "";
                string[] strArray = str25.Split(new char[] { '/' });
                ArrayList list = new ArrayList();
                str25 = str25.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                str26 = strArray[strArray.Length - 1].ToString();
                if (File.Exists(this.objNF3Template.getPhysicalPath() + str4 + str26))
                {
                    if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str5))
                    {
                        Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str5);
                    }
                    File.Copy(this.objNF3Template.getPhysicalPath() + str4 + str26, this.objNF3Template.getPhysicalPath() + str5 + str26);
                }
                list.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                list.Add(str5 + str26);
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    list.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                }
                else
                {
                    list.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                }
                list.Add(this.Session["TM_SZ_CASE_ID"]);
                list.Add(strArray[strArray.Length - 1].ToString());
                list.Add(str5);
                list.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                list.Add(str);
                list.Add("NF");
                list.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                this.objNF3Template.saveGeneratedBillPath(list);
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CYCLIC_PROCEDURE_CODE == "1")
                {
                    list.Clear();
                    string companyId = string.Empty;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        companyId=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        companyId=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    CoverContract_pdf cpdf = new CoverContract_pdf();
                    string fileName = "Contract_" + strArray[strArray.Length - 1].ToString();// this.Session["TM_SZ_BILL_ID"].ToString() + "_contract.pdf";
                    bool isGenerated = false;
                    cpdf.GenerateCoverContract(companyId, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), this.objNF3Template.getPhysicalPath() + str5+ fileName,out isGenerated);
                    if (isGenerated)
                    {
                        list.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list.Add(str5 + fileName);
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                        {
                            list.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        }
                        else
                        {
                            list.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        }
                        list.Add(this.Session["TM_SZ_CASE_ID"]);
                        list.Add(fileName);
                        list.Add(str5);
                        list.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list.Add(str);
                        list.Add("NF");
                        list.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillContractPath(list);
                    }

                }
               
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str26;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = this.txtCaseID.Text;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
            }
            else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
            {
                string str27;
                string companyName;
                Bill_Sys_PVT_Template template8 = new Bill_Sys_PVT_Template();
                bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str28 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str30 = this.Session["TM_SZ_BILL_ID"].ToString();
                string str31 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string str32 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    companyName = new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    str27 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    companyName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    str27 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                template8.GeneratePVTBill(flag, str27, str28, str, companyName, str30, str31, str32);
            }
            else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
            {
                string str35;
                string str40;
                string str33 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template10 = new Bill_Sys_NF3_Template();
                    str33 = template10.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
                }
                else
                {
                    str33 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
                }
                string str34 = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    Bill_Sys_NF3_Template template11 = new Bill_Sys_NF3_Template();
                    str34 = template11.GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    str34 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                new Bill_Sys_PVT_Template();
                // ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str36 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str37 = this.Session["TM_SZ_BILL_ID"].ToString();
                string str38 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string str39 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    str35 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    //   ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    str35 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                Lien lien = new Lien();
                this._MUVGenerateFunction = new MUVGenerateFunction();
                this.objNF3Template = new Bill_Sys_NF3_Template();
                string str42 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.bt_include = this._MUVGenerateFunction.get_bt_include(str42, str, "", "Speciality");
                string str41 = this._MUVGenerateFunction.get_bt_include(str42, "", "WC000000000000000004", "CaseType");
                if ((this.bt_include == "True") && (str41 == "True"))
                {
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                    MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str33 + lien.GenratePdfForLienWithMuv(str35, str37, str, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str38, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str39), this.objNF3Template.getPhysicalPath() + str34 + this.str_1500, this.objNF3Template.getPhysicalPath() + str33 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                    str40 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str33 + this.str_1500.Replace(".pdf", "_MER.pdf");
                    ArrayList list2 = new ArrayList();
                    list2.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                    list2.Add(str33 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                    list2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    list2.Add(this.Session["TM_SZ_CASE_ID"]);
                    list2.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                    list2.Add(str33);
                    list2.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    list2.Add(str);
                    list2.Add("NF");
                    list2.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                    this.objNF3Template.saveGeneratedBillPath(list2);
                    
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                }
                else
                {
                    str40 = lien.GenratePdfForLien(str35, str37, str, str36, str38, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str39);
                }
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + str40 + "');", true);
            }
            else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000001")
            {
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    string str44 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    string str45 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string str46 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    //   ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    // ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                    string str47 = new WC_Bill_Generation().GeneratePDFForReferalWorkerComp((string)this.Session["TM_SZ_BILL_ID"], ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str2, str46, str44, str45, p_szSpeciality);
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + str47 + "');", true);
                }
                else
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
                }
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getFileName(string p_szBillNumber)
    {
        DateTime now = DateTime.Now;
        return (p_szBillNumber + "_" + this.getRandomNumber() + "_" + now.ToString("yyyyMMddHHmmssms"));
    }

    private string getRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 0x2710).ToString();
    }

    protected void grdAllReports_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        if (e.CommandName.ToString() == "Edit")
        {
            int num = Convert.ToInt32(e.CommandArgument.ToString());
            try
            {
                Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
                diagnosis.EventProcID = this.grdAllReports.DataKeys[num]["SZ_PATIENT_TREATMENT_ID"].ToString();
                diagnosis.DoctorID = this.grdAllReports.DataKeys[num]["DOCTOR_ID"].ToString();
                diagnosis.CaseID = this.grdAllReports.DataKeys[num]["SZ_CASE_ID"].ToString();
                diagnosis.ProceuderGroupId = this.grdAllReports.DataKeys[num]["SZ_PROCEDURE_GROUP_ID"].ToString();
                this.Session["DIAGNOS_ASSOCIATION"] = diagnosis;
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mmopen", "showReceiveDocumentPopup();", true);
            }
            catch
            {
            }
        }
        else if (e.CommandName.ToString() == "PatientClick")
        {
            new CaseDetailsBO();
            Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
            int num2 = Convert.ToInt32(e.CommandArgument.ToString());
            obj2.SZ_PATIENT_ID = this.grdAllReports.DataKeys[num2]["SZ_PATIENT_ID"].ToString();
            obj2.SZ_CASE_ID = this.grdAllReports.DataKeys[num2]["SZ_CASE_ID"].ToString();
            obj2.SZ_CASE_NO = this.grdAllReports.DataKeys[num2]["CASE_NO"].ToString();
            obj2.SZ_PATIENT_NAME = this.grdAllReports.DataKeys[num2]["PATIENT_NAME"].ToString();
            obj2.SZ_COMAPNY_ID = this._bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(this.grdAllReports.DataKeys[num2]["SZ_CASE_ID"].ToString()).SZ_COMPANY_ID;
            this.Session["CASE_OBJECT"] = obj2;
            Bill_Sys_Case @case = new Bill_Sys_Case();
            @case.SZ_CASE_ID = this.grdAllReports.DataKeys[num2]["SZ_CASE_ID"].ToString();
            this.Session["CASEINFO"] = @case;
            base.Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
        }
    }

    protected void grdAllReports_RowEditing(object sender, GridViewEditEventArgs e)
    {
        e.Cancel = true;
    }

    

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.con.SourceGrid = this.grdAllReports;
        this.txtSearchBox.SourceGrid = this.grdAllReports;
        this.grdAllReports.Page = this.Page;
        this.grdAllReports.PageNumberList = this.con;
        try
        {
            this.PopUpFlag = "true";
            this.btnRevert.Attributes.Add("onclick", "return isRecordsselected();");
            //  this.btnSelectedBill.Attributes.Add("onclick", "return isRecordsselected();");
            this.txtUpdate3.Attributes.Add("onclick", "javascript:ClosePopup();");
            // btnPerPatient.Attributes.Add("onclick", "javascript:DisableButton();");
            btnPerPatient.Attributes.Add("onclick", "return isRecordsselected();");
            this.btnSelectedBill.Attributes.Add("onclick", "return isRecordsselected();");
            if (!base.IsPostBack)
            {
                hDnl.Value = "";
                Bill_Sys_ReportBO tbo = new Bill_Sys_ReportBO();
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlCaseType.Text = tbo.GetNoFaultId(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.txtCaseType.Text = this.extddlCaseType.Text;
                this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                this.Session["RefGridData"] = null;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    this.grdAllReports.XGridBindSearch();
                    this.Session[this.SESSION_checks] = null;
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "0")
                    {
                        this.grdAllReports.Columns[3].Visible = false;
                    }
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION == "1")
                    {
                        this.grdAllReports.Columns[12].Visible = true;
                        this.grdAllReports.Columns[11].Visible = false;
                    }
                    else
                    {
                        this.grdAllReports.Columns[12].Visible = false;
                        this.grdAllReports.Columns[11].Visible = true;
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

    protected void setLabels()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO dbo = new DashBoardBO();
        Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
        try
        {
            int dayOfWeek = (int)Convert.ToDateTime(DateTime.Today.ToString()).DayOfWeek;
            DateTime time = Convert.ToDateTime(DateTime.Today.ToString()).AddDays((double)-dayOfWeek);
            DateTime time2 = time.AddDays(6.0);
            this.lblAppointmentToday.Text = dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "GET_APPOINTMENT");
            this.lblAppointmentWeek.Text = dbo.getAppoinmentCount(time.ToString(), time2.ToString(), this.txtCompanyID.Text, "GET_APPOINTMENT");
            this.lblBillStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='../Ajax%20Pages/Bill_Sys_ReffPaidBills.aspx?Flag=Paid' onclick=\"javascript:OpenPage('Paid');\" > " + n_bo.GetCaseCount("SP_MST_CASE_MASTER", "GET_PAID_LIST_COUNT", this.txtCompanyID.Text) + "</a>";
            object text = this.lblBillStatus.Text;
            this.lblBillStatus.Text = string.Concat(new object[] { text, " Paid Bills  </li>  <li> <a href='../Ajax%20Pages/Bill_Sys_ReffPaidBills.aspx?Flag=UnPaid' onclick=\"javascript:OpenPage('UnPaid');\" > ", n_bo.GetCaseCount("SP_MST_CASE_MASTER", "GET_UNPAID_LIST_COUNT", this.txtCompanyID.Text), "</a> Un-Paid Bills </li></ul>" });
            this.lblDesk.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' onclick=\"javascript:OpenPage('Litigation');\" > " + n_bo.GetCaseCount("SP_LITIGATION_WRITEOFF_DESK", "GET_LETIGATION_COUNT", this.txtCompanyID.Text) + "</a> bills due for litigation";
            this.lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='../Ajax%20Pages/Bill_Sys_ReffPaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            this.lblMissingInformation.Text = this.lblMissingInformation.Text + " insurance company missing </li>  <li> <a href=../Ajax%20Pages/Bill_Sys_ReffPaidBills.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "MISSING_ATTORNEY") + "</a>";
            this.lblMissingInformation.Text = this.lblMissingInformation.Text + " attorney missing </li>  <li> <a href='../Ajax%20Pages/Bill_Sys_ReffPaidBills.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "MISSING_CLAIM_NUMBER") + "</a> claim number missing </li>";
            this.lblMissingInformation.Text = this.lblMissingInformation.Text + "<li> <a href='../Ajax%20Pages/Bill_Sys_ReffPaidBills.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "MISSING_REPORT_NUMBER") + "</a> report number missing </li>";
            this.lblMissingInformation.Text = this.lblMissingInformation.Text + "<li> <a href='../Ajax%20Pages/Bill_Sys_ReffPaidBills.aspx?Flag=MissingPolicyHolder'> " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "MISSING_POLICY_HOLDER") + "</a> policy holder missing </li>";
            this.lblMissingInformation.Text = this.lblMissingInformation.Text + "<li> <a href='Bill_Sys_ShowUnSentNF2.aspx' > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "UNSENTNF2") + "</a> unsent NF2 </li></ul>";
            this.lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_ReffPaidBills.aspx' onclick=\"javascript:OpenPage('Litigation');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a> Received Report";
            this.lblReport.Text = this.lblReport.Text + "</li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=P' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "DOCUMENT_PENDING_COUNT") + "</a> Pending Report </li></ul>";
            this.lblProcedureStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li>" + dbo.getBilledUnbilledProcCode(this.txtCompanyID.Text, "GET_BILLEDPROC") + " billed procedure codes";
            this.lblTotalVisit.Text = dbo.getTotalVisits(this.txtCompanyID.Text, "GET_VISIT_COUNT");
            this.lblBilledVisit.Text = dbo.getTotalVisits(this.txtCompanyID.Text, "GET_BILLED_VISIT_COUNT");
            this.lblUnBilledVisit.Text = dbo.getTotalVisits(this.txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT");
            this.lblPatientVisitStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientscheduled' onclick=\"javascript:OpenPage('PatientScheduled');\" > " + dbo.getPatientVisitStatusCount(this.txtCompanyID.Text, "GET_PATIENT_VISIT_SCHEDULED_COUNT") + "</a> ";
            this.lblPatientVisitStatus.Text = this.lblPatientVisitStatus.Text + " Patient Scheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientnoshows' onclick=\"javascript:OpenPage('PatientNoShows');\" > " + dbo.getPatientVisitStatusCount(this.txtCompanyID.Text, "GET_PATIENT_VISIT_NO_SHOWS") + "</a>";
            this.lblPatientVisitStatus.Text = this.lblPatientVisitStatus.Text + " Patient No Shows </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientrescheduled' onclick=\"javascript:OpenPage('PatientRescheduled');\" > " + dbo.getPatientVisitStatusCount(this.txtCompanyID.Text, "GET_PATIENT_VISIT_RESCHEDULED") + "</a>";
            this.lblPatientVisitStatus.Text = this.lblPatientVisitStatus.Text + " Patient Rescheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientvisitcompleted' onclick=\"javascript:OpenPage('PatientVisitcompleted');\" > " + dbo.getPatientVisitStatusCount(this.txtCompanyID.Text, "GET_PATIENT_VISIT_COMPLETED") + "</a>Patient Visit completed </li></ul>";
            this.ConfigDashBoard();
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

    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        this.grdAllReports.XGridBindSearch();
    }
    public CyclicCode GetCyclicCode(string szCaseID, string szCmpD, string szInsuranceID, string szSpecilatyID, DataTable tblProcCode)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        CyclicCode objCyclicCode = new CyclicCode();
        DataSet dsValues = new DataSet();

        try
        {
            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction_bo = new Bill_Sys_BillTransaction_BO();
            dsValues = _bill_Sys_BillTransaction_bo.GetCodeAmount(szCaseID, szCmpD, szInsuranceID, szSpecilatyID, tblProcCode);
            if (dsValues.Tables.Count > 0)
            {
                if (dsValues.Tables[0].Rows.Count > 0)
                {
                    objCyclicCode.sz_configuraton = dsValues.Tables[0].Rows[0]["configuration"].ToString();
                    if (dsValues.Tables[1].Rows.Count > 0)
                    {
                        objCyclicCode.i_Flag = 1;
                        objCyclicCode.ProcValue = dsValues.Tables[1];

                        if (dsValues.Tables[2].Rows.Count > 0)
                        {
                            try
                            {

                                objCyclicCode.i_Count = Convert.ToInt32(dsValues.Tables[2].Rows[0]["Count"]);

                            }
                            catch (Exception ex)
                            {
                                objCyclicCode.i_Count = 0;

                            }

                        }
                        else
                        {
                            objCyclicCode.i_Count = 0;
                        }
                    }
                    else
                    {
                        objCyclicCode.i_Flag = 0;
                    }

                }
                else
                {
                    objCyclicCode.i_Flag = 0;
                }
            }
            else
            {
                objCyclicCode.i_Flag = 0;
            }

            if (dsValues.Tables.Count == 4)
            {
                objCyclicCode.AllProc = dsValues.Tables[3];
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


        return objCyclicCode;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    //public int GetProcCount(DataTable dtValues,int LastCount)
    //{
    //    int iReturn = 1;
    //    if (dtValues != null)
    //    {
    //        if (dtValues.Rows.Count>0)
    //        {
    //            try
    //            {
    //                if(LastCount==0)
    //                {
    //                    DataRow[] drCount1 = dtValues.Select("i_cyclic_id=1");
    //                    if (drCount1.Length > 0)
    //                    {
    //                        DataRow[] drCount2 = dtValues.Select("i_cyclic_id=2");
    //                        if (drCount2.Length > 0)
    //                        {
    //                            DataRow[] drCount3 = dtValues.Select("i_cyclic_id=3");
    //                            if (drCount3.Length > 0)
    //                            {
    //                                iReturn = 4;
    //                            }else
    //                            {
    //                                iReturn = 3;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            iReturn = 2;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        iReturn = 1;
    //                    }

    //                }
    //                else if (LastCount == 2)
    //                {
    //                    DataRow[] drCount2 = dtValues.Select("i_cyclic_id=2");
    //                    if (drCount2.Length > 0)
    //                    {
    //                        DataRow[] drCount3 = dtValues.Select("i_cyclic_id=3");
    //                        if (drCount3.Length > 0)
    //                        {
    //                            iReturn = 4;
    //                        }
    //                        else
    //                        {
    //                            iReturn = 3;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        iReturn = 2;
    //                    }

    //                }
    //                else if (LastCount == 3)
    //                {

    //                        DataRow[] drCount3 = dtValues.Select("i_cyclic_id=3");
    //                        if (drCount3.Length > 0)
    //                        {
    //                            iReturn = 4;
    //                        }
    //                        else
    //                        {
    //                            iReturn = 3;
    //                        }


    //                } else if (LastCount >= 4)
    //                {
    //                    iReturn = 4;
    //                }

    //            }
    //            catch (Exception ex)
    //            {

    //                throw;
    //            }
    //        }
    //    }
    //    return iReturn;
    //}

    protected void btnAddDGCodes_Click(object sender, EventArgs e)
    {
        this.AddSelectedDiagnosisCodes();
    }

    protected void AddSelectedDiagnosisCodes()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            string str = this.hndSeletedDGCodes.Value.Replace("--,", "*").Replace("--", "");

            if (str != "")
            {
                string[] strArrays = str.Split(new char[] { '*' });
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

    protected void btnRemoveDGCodes_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            ArrayList arrayLists = new ArrayList();
            for (int i = 0; i < this.lstDiagnosisCodes.Items.Count; i++)
            {
                if (this.lstDiagnosisCodes.Items[i].Selected)
                {
                    string text = this.lstDiagnosisCodes.Items[i].Text;
                    this.lstDiagnosisCodes.Items.Remove(this.lstDiagnosisCodes.Items[i]);
                    i--;
                    string value = this.hndSeletedDGCodes.Value;
                    string[] strArrays = this.hndSeletedDGCodes.Value.Split(new char[] { ',' });
                    for (int j = 0; j < (int)strArrays.Length; j++)
                    {
                        string str = strArrays[j];
                        char[] chrArray = new char[] { '~' };
                        if (text == str.Split(chrArray)[1])
                        {
                            value = (j != (int)strArrays.Length - 1 ? value.Replace(string.Concat(strArrays[j], ","), "").ToString() : value.Replace(string.Concat(",", strArrays[j]), "").ToString());
                            this.hndSeletedDGCodes.Value = value;
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
}

//204


