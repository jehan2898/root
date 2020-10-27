using System;
using System.Data;
using System.Configuration;
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
using log4net;
using mbs.LienBills;
using iTextSharp.text.pdf;
using System.Collections;
using Microsoft.SqlServer.Management.Common;

/// <summary>
/// Summary description for MUVGenerateFunction
/// </summary>
public class MUVGenerateFunction : PageBase
{
    private static ILog log = LogManager.GetLogger("MUVGenerateFunction");
    public MUVGenerateFunction()
    {

    }

    public ArrayList GetDiagnosis(string diagnosis1)
    {
        string diagnosis2 = "";
        string diagnosis3 = "";
        string diagnosis4 = "";
        if (diagnosis1.Trim() != "")
        {
            if (diagnosis1.Contains("."))
            {
                diagnosis2 = diagnosis1.Substring(0, diagnosis1.IndexOf('.'));
                diagnosis3 = ".";
                diagnosis4 = diagnosis1.Substring(diagnosis1.IndexOf('.') + 1, diagnosis1.Length - diagnosis2.Length - 1);
            }
            else
            {
                diagnosis2 = diagnosis1;
                diagnosis3 = "";
                diagnosis4 = "";
            }
        }
        else
        {
            diagnosis2 = "";
            diagnosis3 = "";
            diagnosis4 = "";
        }
        ArrayList al = new ArrayList();
        al.Add(diagnosis2);
        al.Add(diagnosis3);
        al.Add(diagnosis4);
        return al;
    }

    public string FillPdf(string BillNo)
    {

        string CmpId = "";
        string CmpName = "";
        string szCaseID = "";
        string strGenFileName = "";
        string szFinal1500Pdf = "";
        PDFValueReplacement.PDFValueReplacement objPdf = new PDFValueReplacement.PDFValueReplacement();
        DataSet ds = getValueForPdf(BillNo);
        DataSet dsProcCode = getProcedureCodeValueForPdf(BillNo);

        CmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        szCaseID = Session["TM_SZ_CASE_ID"].ToString();
        DataTable dtAddProcCode = new DataTable();
        dtAddProcCode.Columns.Add("MONTH");
        dtAddProcCode.Columns.Add("DAY");
        dtAddProcCode.Columns.Add("YEAR");
        dtAddProcCode.Columns.Add("TO_MONTH");
        dtAddProcCode.Columns.Add("TO_DAY");
        dtAddProcCode.Columns.Add("TO_YEAR");
        dtAddProcCode.Columns.Add("SZ_PROCEDURE_CODE");
        dtAddProcCode.Columns.Add("PlaceOfService");
        dtAddProcCode.Columns.Add("DiagnosisPointer");
        dtAddProcCode.Columns.Add("FL_AMOUNT");
        dtAddProcCode.Columns.Add("I_UNIT");
        dtAddProcCode.Columns.Add("paid_amount");
        dtAddProcCode.Columns.Add("flt_balance");
        dtAddProcCode.Columns.Add("TOTAL");
        dtAddProcCode.Columns.Add("SZ_NPI");
        dtAddProcCode.Columns.Add("SZ_MODIFIER");

        ArrayList arrPaths = new ArrayList();
        ArrayList arrNewPAth = new ArrayList();
        int iFlag = 1;
        int count = -1;

        for (int i = 0; i < dsProcCode.Tables[0].Rows.Count; i++)
        {
            if (count == -1)
            {
                dtAddProcCode.Clear();
            }
            DataRow drAddProcCode = dtAddProcCode.NewRow();
            drAddProcCode["MONTH"] = dsProcCode.Tables[0].Rows[i]["MONTH"].ToString();
            drAddProcCode["DAY"] = dsProcCode.Tables[0].Rows[i]["DAY"].ToString();
            drAddProcCode["YEAR"] = dsProcCode.Tables[0].Rows[i]["YEAR"].ToString();
            drAddProcCode["TO_MONTH"] = dsProcCode.Tables[0].Rows[i]["TO_MONTH"].ToString();
            drAddProcCode["TO_DAY"] = dsProcCode.Tables[0].Rows[i]["TO_DAY"].ToString();
            drAddProcCode["TO_YEAR"] = dsProcCode.Tables[0].Rows[i]["TO_YEAR"].ToString();
            drAddProcCode["PlaceOfService"] = dsProcCode.Tables[0].Rows[i]["PlaceOfService"].ToString();
            drAddProcCode["SZ_PROCEDURE_CODE"] = dsProcCode.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString();
            drAddProcCode["DiagnosisPointer"] = dsProcCode.Tables[0].Rows[i]["DiagnosisPointer"].ToString();
            drAddProcCode["FL_AMOUNT"] = dsProcCode.Tables[0].Rows[i]["FL_AMOUNT"].ToString();
            drAddProcCode["I_UNIT"] = dsProcCode.Tables[0].Rows[i]["I_UNIT"].ToString();
            drAddProcCode["paid_amount"] = dsProcCode.Tables[0].Rows[i]["paid_amount"].ToString();
            drAddProcCode["flt_balance"] = dsProcCode.Tables[0].Rows[i]["flt_balance"].ToString();
            drAddProcCode["TOTAL"] = dsProcCode.Tables[0].Rows[i]["TOTAL"].ToString();
            drAddProcCode["SZ_NPI"] = dsProcCode.Tables[0].Rows[i]["SZ_NPI"].ToString();
            drAddProcCode["SZ_MODIFIER"] = dsProcCode.Tables[0].Rows[i]["SZ_MODIFIER"].ToString();

            dtAddProcCode.Rows.Add(drAddProcCode);
            count++;

            if (count >= 5 && i != dsProcCode.Tables[0].Rows.Count - 1)
            {
                string pdfFilePathnew = ConfigurationManager.AppSettings["UnEncrypted1500_1"].ToString();
                count = -1;
                DataSet dscountgrester = new DataSet();
                dscountgrester.Tables.Add(dtAddProcCode.Copy());
                string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                string szPdFIndex = i.ToString();
                string szNewPdfPath = Get1500FormPdfChangeField("page_" + szPdFIndex, dscountgrester, ds, BillNo, szCaseID, CmpId, CmpName, pdfFilePathnew);
                arrNewPAth.Add(szURLDocumentManager_OCT + "/" + szNewPdfPath);

            }
            else if (i == dsProcCode.Tables[0].Rows.Count - 1)
            {
                string pdfFilePath = ConfigurationManager.AppSettings["UnEncrypted1500"].ToString();
                count = -1;
                DataSet dscountgrester = new DataSet();
                string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                dscountgrester.Tables.Add(dtAddProcCode.Copy());
                string szPdFIndex = i.ToString();
                string szNewPdfPath = Get1500FormPdf("page_" + szPdFIndex, dscountgrester, ds, BillNo, szCaseID, CmpId, CmpName, pdfFilePath);
                arrNewPAth.Add(szURLDocumentManager_OCT + "/" + szNewPdfPath);

            }
        }
        string szNewFile = arrNewPAth[0].ToString();

        for (int i = 0; i < arrNewPAth.Count - 1; i++)
        {

            string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
            strGenFileName = BillNo + "_" + szCaseID + "_" + i.ToString() + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf";
            string NewPdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
            string szFirstMerge = MergePDF.MergePDFFiles(szNewFile, arrNewPAth[i + 1].ToString(), NewPdfPath);
            szNewFile = NewPdfPath;

        }
        if (szNewFile != null || szNewFile != "")
        {
            string url = szNewFile;
            string[] u = url.Split('/');
            szFinal1500Pdf = u[u.Length - 1];
        }

        string szFinalOtptPdf = szFinal1500Pdf;

        return szFinalOtptPdf;
    }

    public string FillPdf(string BillNo, ServerConnection conn)
    {

        string CmpId = "";
        string CmpName = "";
        string szCaseID = "";
        string strGenFileName = "";
        string szFinal1500Pdf = "";
        PDFValueReplacement.PDFValueReplacement objPdf = new PDFValueReplacement.PDFValueReplacement();
        DataSet ds = getValueForPdf(BillNo, conn);
        DataSet dsProcCode = getProcedureCodeValueForPdf(BillNo, conn);

        CmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        szCaseID = Session["TM_SZ_CASE_ID"].ToString();
        DataTable dtAddProcCode = new DataTable();
        dtAddProcCode.Columns.Add("MONTH");
        dtAddProcCode.Columns.Add("DAY");
        dtAddProcCode.Columns.Add("YEAR");
        dtAddProcCode.Columns.Add("TO_MONTH");
        dtAddProcCode.Columns.Add("TO_DAY");
        dtAddProcCode.Columns.Add("TO_YEAR");
        dtAddProcCode.Columns.Add("SZ_PROCEDURE_CODE");
        dtAddProcCode.Columns.Add("PlaceOfService");
        dtAddProcCode.Columns.Add("DiagnosisPointer");
        dtAddProcCode.Columns.Add("FL_AMOUNT");
        dtAddProcCode.Columns.Add("I_UNIT");
        dtAddProcCode.Columns.Add("paid_amount");
        dtAddProcCode.Columns.Add("flt_balance");
        dtAddProcCode.Columns.Add("TOTAL");
        dtAddProcCode.Columns.Add("SZ_NPI");
        dtAddProcCode.Columns.Add("SZ_MODIFIER");

        ArrayList arrPaths = new ArrayList();
        ArrayList arrNewPAth = new ArrayList();
        int iFlag = 1;
        int count = -1;

        for (int i = 0; i < dsProcCode.Tables[0].Rows.Count; i++)
        {
            if (count == -1)
            {
                dtAddProcCode.Clear();
            }
            DataRow drAddProcCode = dtAddProcCode.NewRow();
            drAddProcCode["MONTH"] = dsProcCode.Tables[0].Rows[i]["MONTH"].ToString();
            drAddProcCode["DAY"] = dsProcCode.Tables[0].Rows[i]["DAY"].ToString();
            drAddProcCode["YEAR"] = dsProcCode.Tables[0].Rows[i]["YEAR"].ToString();
            drAddProcCode["TO_MONTH"] = dsProcCode.Tables[0].Rows[i]["TO_MONTH"].ToString();
            drAddProcCode["TO_DAY"] = dsProcCode.Tables[0].Rows[i]["TO_DAY"].ToString();
            drAddProcCode["TO_YEAR"] = dsProcCode.Tables[0].Rows[i]["TO_YEAR"].ToString();
            drAddProcCode["PlaceOfService"] = dsProcCode.Tables[0].Rows[i]["PlaceOfService"].ToString();
            drAddProcCode["SZ_PROCEDURE_CODE"] = dsProcCode.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString();
            drAddProcCode["DiagnosisPointer"] = dsProcCode.Tables[0].Rows[i]["DiagnosisPointer"].ToString();
            drAddProcCode["FL_AMOUNT"] = dsProcCode.Tables[0].Rows[i]["FL_AMOUNT"].ToString();
            drAddProcCode["I_UNIT"] = dsProcCode.Tables[0].Rows[i]["I_UNIT"].ToString();
            drAddProcCode["paid_amount"] = dsProcCode.Tables[0].Rows[i]["paid_amount"].ToString();
            drAddProcCode["flt_balance"] = dsProcCode.Tables[0].Rows[i]["flt_balance"].ToString();
            drAddProcCode["TOTAL"] = dsProcCode.Tables[0].Rows[i]["TOTAL"].ToString();
            drAddProcCode["SZ_NPI"] = dsProcCode.Tables[0].Rows[i]["SZ_NPI"].ToString();
            drAddProcCode["SZ_MODIFIER"] = dsProcCode.Tables[0].Rows[i]["SZ_MODIFIER"].ToString();

            dtAddProcCode.Rows.Add(drAddProcCode);
            count++;

            if (count >= 5 && i != dsProcCode.Tables[0].Rows.Count - 1)
            {
                string pdfFilePathnew = ConfigurationManager.AppSettings["UnEncrypted1500_1"].ToString();
                count = -1;
                DataSet dscountgrester = new DataSet();
                dscountgrester.Tables.Add(dtAddProcCode.Copy());
                string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                string szPdFIndex = i.ToString();
                string szNewPdfPath = Get1500FormPdfChangeField("page_" + szPdFIndex, dscountgrester, ds, BillNo, szCaseID, CmpId, CmpName, pdfFilePathnew);
                arrNewPAth.Add(szURLDocumentManager_OCT + "/" + szNewPdfPath);

            }
            else if (i == dsProcCode.Tables[0].Rows.Count - 1)
            {
                string pdfFilePath = ConfigurationManager.AppSettings["UnEncrypted1500"].ToString();
                count = -1;
                DataSet dscountgrester = new DataSet();
                string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                dscountgrester.Tables.Add(dtAddProcCode.Copy());
                string szPdFIndex = i.ToString();
                string szNewPdfPath = Get1500FormPdf("page_" + szPdFIndex, dscountgrester, ds, BillNo, szCaseID, CmpId, CmpName, pdfFilePath);
                arrNewPAth.Add(szURLDocumentManager_OCT + "/" + szNewPdfPath);

            }
        }
        string szNewFile = arrNewPAth[0].ToString();

        for (int i = 0; i < arrNewPAth.Count - 1; i++)
        {

            string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
            strGenFileName = BillNo + "_" + szCaseID + "_" + i.ToString() + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf";
            string NewPdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
            string szFirstMerge = MergePDF.MergePDFFiles(szNewFile, arrNewPAth[i + 1].ToString(), NewPdfPath);
            szNewFile = NewPdfPath;

        }
        if (szNewFile != null || szNewFile != "")
        {
            string url = szNewFile;
            string[] u = url.Split('/');
            szFinal1500Pdf = u[u.Length - 1];
        }

        string szFinalOtptPdf = szFinal1500Pdf;

        return szFinalOtptPdf;
    }

    public string ReplacePDFvalues(string pdfFileName, string billNumber, string szCompanyID, string szCaseID, string szCompID)
    {
        string OutputFilePath = "", newPdfFilename = "";
        string code_length = "";
        newPdfFilename = getFileName(billNumber) + ".pdf";
        log.Debug(newPdfFilename);
        try

        {
            string compId = szCompID;
            DataSet dsPdfValue = getPDFData(billNumber, compId);
            DataSet dsProcValue = getServiceTableData(billNumber, compId);
            if (dsProcValue.Tables[0].Rows.Count <= 6)
            {
                log.Debug("in ReplacePDFvalues");
                //newPdfFilename = getFileName(billNumber) + ".pdf";
                //log.Debug(newPdfFilename);


                string pdfTemplate = pdfFileName;
                OutputFilePath = getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), szCompanyID, szCaseID) + newPdfFilename;
                log.Debug("OutputFilePath" + OutputFilePath);


                PdfReader pdfReader = new PdfReader(pdfTemplate);
                //PdfReader.unethicalreading = true; 
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(OutputFilePath, FileMode.Create));
                AcroFields pdfFormFields = pdfStamper.AcroFields;
                if (dsPdfValue != null)
                {
                    if (dsPdfValue.Tables[0] != null)
                    {
                        //pdfFormFields.SetField("Name", "Insurance company name");
                        pdfFormFields.SetField("Name", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
                        pdfFormFields.SetField("2", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString());
                        //pdfFormFields.SetField("3", "Insurance company address");
                        pdfFormFields.SetField("3", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS2"].ToString());
                        //pdfFormFields.SetField("4", "Insurance company address");
                        pdfFormFields.SetField("4", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString());
                        //pdfFormFields.SetField("8", "Yes");
                        //pdfFormFields.SetField("9", "Yes");
                        //pdfFormFields.SetField("10", "Yes");
                        //pdfFormFields.SetField("11", "Yes");
                        if (dsPdfValue.Tables[0].Rows[0]["SZ_CASE_TYPE_ABBRIVATION"].ToString() == "PVT")

                        {
                            pdfFormFields.SetField("12", "1");
                        }
                        else
                        {
                            pdfFormFields.SetField("14", "1");
                        }
                        //pdfFormFields.SetField("13", "Yes");

                        //Amod
                        //pdfFormFields.SetField("14", "1");
                        //pdfFormFields.SetField("15", "Insured ID No");

                        // Amod
                        if (dsPdfValue.Tables[0].Rows[0]["SZ_CLAIM_NO"] != "null")
                        {
                            pdfFormFields.SetField("15", dsPdfValue.Tables[0].Rows[0]["SZ_CLAIM_NO"].ToString());
                        }

                        ////pdfFormFields.SetField("16", "Patien's Name LFM");
                        ////pdfFormFields.SetField("17", "Patient's DOB  MM");
                        ////pdfFormFields.SetField("18", "Patient's DOB  DD");
                        ////pdfFormFields.SetField("19", "Patient's DOB  YYYY");
                        ////pdfFormFields.SetField("20", "Yes");
                        ////pdfFormFields.SetField("21", "Yes");
                        ////pdfFormFields.SetField("22", "Insured's Name LFM");
                        ////pdfFormFields.SetField("23", "Patient's Address No,Street");
                        ////pdfFormFields.SetField("24", "Patient's Address city");
                        ////pdfFormFields.SetField("25", "Patient's Address state");
                        ////pdfFormFields.SetField("26", "Patient's Address zip");
                        ////pdfFormFields.SetField("27", "Patient's Address tele 1");
                        ////pdfFormFields.SetField("28", "Patient's Address tele 2");
                        pdfFormFields.SetField("16", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString());

                        // Amod - if the month is 1 digit then prefix it with 0
                        if (dsPdfValue.Tables[0].Rows[0]["DOB_MM"] != null)
                        {
                            if (!dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString().Equals(""))
                            {
                                try
                                {
                                    int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                                    if (i <= 9)
                                    {
                                        pdfFormFields.SetField("17", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                                    }
                                    else
                                    {
                                        pdfFormFields.SetField("17", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                                    }
                                }
                                catch (Exception i)
                                {
                                    pdfFormFields.SetField("17", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                                }
                            }
                        }

                        //pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                        if (dsPdfValue.Tables[0].Rows[0]["DOB_DD"] != null)
                        {
                            if (!dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString().Equals(""))
                            {
                                try
                                {
                                    int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                                    if (i <= 9)
                                    {
                                        pdfFormFields.SetField("18", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                                    }
                                    else
                                    {
                                        pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                                    }
                                }
                                catch (Exception i)
                                {
                                    pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                                }
                            }
                        }
                        pdfFormFields.SetField("19", dsPdfValue.Tables[0].Rows[0]["DOB_YY"].ToString());
                        if (dsPdfValue.Tables[0].Rows[0]["MALE"].ToString() == "1")
                        {
                            pdfFormFields.SetField("20", "No");
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["FEMALE"].ToString() == "1")
                        {
                            pdfFormFields.SetField("21", "No");
                        }

                        pdfFormFields.SetField("22", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString());
                        pdfFormFields.SetField("23", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString());
                        pdfFormFields.SetField("24", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
                        pdfFormFields.SetField("25", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
                        pdfFormFields.SetField("26", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());
                        pdfFormFields.SetField("37", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_1_3"].ToString());
                        pdfFormFields.SetField("28", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_4_E"].ToString());

                        if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "1")
                        {
                            pdfFormFields.SetField("29", "1");
                        }
                        else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "2")
                        {
                            pdfFormFields.SetField("30", "1");
                        }
                        else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "3")
                        {
                            pdfFormFields.SetField("31", "1");
                        }
                        else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "4")
                        {
                            pdfFormFields.SetField("32", "1");
                        }
                        //pdfFormFields.SetField("29", "Yes");
                        //pdfFormFields.SetField("30", "Yes");
                        //pdfFormFields.SetField("31", "Yes");
                        //pdfFormFields.SetField("32", "Yes");

                        //pdfFormFields.SetField("33", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ADDRESS"].ToString());
                        //pdfFormFields.SetField("34", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_CITY"].ToString());
                        //pdfFormFields.SetField("35", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_STATE_ID"].ToString());
                        //pdfFormFields.SetField("36", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ZIP"].ToString());
                        //pdfFormFields.SetField("37", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE_1_3"].ToString());
                        //pdfFormFields.SetField("38", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE_4_E"].ToString());

                        pdfFormFields.SetField("33", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString());
                        pdfFormFields.SetField("34", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
                        pdfFormFields.SetField("35", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
                        pdfFormFields.SetField("36", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());
                        pdfFormFields.SetField("27", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_1_3"].ToString());
                        //pdfFormFields.SetField("38", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_1_3"].ToString() + " " + dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_4_E"].ToString());
                        pdfFormFields.SetField("38", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_4_E"].ToString());

                        if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "1")
                        {
                            pdfFormFields.SetField("39", "1");
                        }
                        else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "2")
                        {
                            pdfFormFields.SetField("40", "1");
                        }
                        else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "3")
                        {
                            pdfFormFields.SetField("41", "1");
                        }

                        //pdfFormFields.SetField("39", "Yes");
                        //pdfFormFields.SetField("40", "Yes");
                        //pdfFormFields.SetField("41", "Yes");
                        //pdfFormFields.SetField("42", "Yes");//?????????/
                        //pdfFormFields.SetField("43", "Yes");
                        //pdfFormFields.SetField("44", "Yes");
                        //pdfFormFields.SetField("45", "OTHER INSUREDS NAME LFM");
                        ////pdfFormFields.SetField("46", "OTHER INSUREDS policy/grp no");
                        //pdfFormFields.SetField("46", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_NO"].ToString());
                        //pdfFormFields.SetField("47", "OTHER INSUREDS DOB MM");
                        //pdfFormFields.SetField("48", "OTHER INSUREDS DOB DD");
                        //pdfFormFields.SetField("49", "OTHER INSUREDS DOB YYYY");
                        //pdfFormFields.SetField("50", "Yes");
                        //pdfFormFields.SetField("51", "Yes");
                        //pdfFormFields.SetField("52", "EMPLOYERS NAME OR SCHOOL NAME");
                        //pdfFormFields.SetField("53", "INSURANCE PLAN NAME OR PROGRAM NAME");
                        //pdfFormFields.SetField("54", "Yes");
                        //pdfFormFields.SetField("55", "Yes");
                        //pdfFormFields.SetField("56", "Yes");
                        //pdfFormFields.SetField("57", "Yes");

                        //pdfFormFields.SetField("59", "Yes");






                        if (dsPdfValue.Tables[0].Rows[0]["SZ_CASE_TYPE_ABBRIVATION"].ToString() == "WC")
                        {
                            pdfFormFields.SetField("54", "1");
                            pdfFormFields.SetField("57", "1");
                            pdfFormFields.SetField("60", "1");
                        }
                        else if (dsPdfValue.Tables[0].Rows[0]["SZ_CASE_TYPE_ABBRIVATION"].ToString() == "NF")
                        {
                            pdfFormFields.SetField("56", "1");
                            pdfFormFields.SetField("58", dsPdfValue.Tables[0].Rows[0]["SZ_ACCIDENT_STATE_ID"].ToString());
                            pdfFormFields.SetField("55", "1");
                            pdfFormFields.SetField("60", "1");
                        }
                        else if (dsPdfValue.Tables[0].Rows[0]["SZ_CASE_TYPE_ABBRIVATION"].ToString() == "PVT")
                        {
                            pdfFormFields.SetField("55", "1");
                            pdfFormFields.SetField("57", "1");
                            pdfFormFields.SetField("60", "1");

                        }
                        else
                        {
                            pdfFormFields.SetField("59", "1");
                            pdfFormFields.SetField("55", "1");

                        }



                        //pdfFormFields.SetField("61", "RESERVED FOR LOCAL USE");
                        pdfFormFields.SetField("62", dsPdfValue.Tables[0].Rows[0]["SZ_WCB_NO"].ToString());
                        //pdfFormFields.SetField("63", "INSUREDS DOB MM");
                        //pdfFormFields.SetField("64", "INSUREDS DOB DD");
                        //pdfFormFields.SetField("65", "INSUREDS DOB YYYY");
                        if (dsPdfValue.Tables[0].Rows[0]["DOB_MM"] != null)
                        {
                            if (!dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString().Equals(""))
                            {
                                try
                                {
                                    int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                                    if (i <= 9)
                                    {
                                        pdfFormFields.SetField("63", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                                    }
                                    else
                                    {
                                        pdfFormFields.SetField("63", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                                    }
                                }
                                catch (Exception i)
                                {
                                    pdfFormFields.SetField("63", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                                }
                            }
                        }

                        //pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                        if (dsPdfValue.Tables[0].Rows[0]["DOB_DD"] != null)
                        {
                            if (!dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString().Equals(""))
                            {
                                try
                                {
                                    int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                                    if (i <= 9)
                                    {
                                        pdfFormFields.SetField("64", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                                    }
                                    else
                                    {
                                        pdfFormFields.SetField("64", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                                    }
                                }
                                catch (Exception i)
                                {
                                    pdfFormFields.SetField("64", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                                }
                            }
                        }
                        pdfFormFields.SetField("65", dsPdfValue.Tables[0].Rows[0]["DOB_YY"].ToString());
                        if (dsPdfValue.Tables[0].Rows[0]["MALE"].ToString() == "1")
                        {
                            pdfFormFields.SetField("66", "Yes");
                        }
                        else if (dsPdfValue.Tables[0].Rows[0]["FEMALE"].ToString() == "1")
                        {
                            pdfFormFields.SetField("67", "No");
                        }
                        //pdfFormFields.SetField("66", "Yes");
                        //pdfFormFields.SetField("67", "Yes");
                        //pdfFormFields.SetField("68", "EMPLOYERS NAME OR SCHOOL NAME");
                        ////pdfFormFields.SetField("69", "INSURANCE PLAN NAME OR PROGRAM NAME");
                        //pdfFormFields.SetField("Name", );
                        //Amod
                        pdfFormFields.SetField("69", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
                        //pdfFormFields.SetField("70", "Yes");
                        pdfFormFields.SetField("71", "No");

                        //Amod
                        pdfFormFields.SetField("72", "SIGNATURE ON FILE");

                        //Amod
                        pdfFormFields.SetField("73", dsPdfValue.Tables[0].Rows[0]["SZ_TODAY"].ToString());

                        //Amod
                        pdfFormFields.SetField("74", "SIGNATURE ON FILE");

                        //Amod
                        pdfFormFields.SetField("75", dsPdfValue.Tables[0].Rows[0]["SZ_MM_ACCIDENT"].ToString());

                        //Amod
                        pdfFormFields.SetField("76", dsPdfValue.Tables[0].Rows[0]["SZ_DD_ACCIDENT"].ToString());

                        //Amod
                        pdfFormFields.SetField("77", dsPdfValue.Tables[0].Rows[0]["SZ_YY_ACCIDENT"].ToString());

                        //Aarti
                        pdfFormFields.SetField("730", "431");

                        //pdfFormFields.SetField("78", "first treat Date MM");
                        //pdfFormFields.SetField("79", "first treat Date DD");
                        //pdfFormFields.SetField("80", "first treat Date YYYY");
                        //pdfFormFields.SetField("81", "unable work from Date MM");
                        //pdfFormFields.SetField("82", "unable work from Date DD");
                        //pdfFormFields.SetField("83", "unable work from Date YYYY");
                        //pdfFormFields.SetField("84", "unable work to Date MM");
                        //pdfFormFields.SetField("85", "unable work to Date DD");
                        //pdfFormFields.SetField("86", "unable work to Date YYYY");
                        //pdfFormFields.SetField("87", "NAME OF REFERRING PROVIDER OR OTHER SOURCE");
                        pdfFormFields.SetField("87", dsPdfValue.Tables[0].Rows[0]["sz_reff_doctor"].ToString());
                        pdfFormFields.SetField("90", dsPdfValue.Tables[0].Rows[0]["sz_reff_doctor_npi"].ToString());

                        if (dsPdfValue.Tables[0].Rows[0]["IS_OUT_LAB_YES"].ToString() == "1")
                        {
                            pdfFormFields.SetField("98", "No");
                        }
                        else if (dsPdfValue.Tables[0].Rows[0]["IS_OUT_LAB_NO"].ToString() == "1")
                        {
                            pdfFormFields.SetField("99", "No");
                        }

                        string diaPointer = "";
                        if (dsPdfValue.Tables[0].Rows[0]["sz_abbrivation_id"].ToString() == "WC000000000000000003")
                        {
                            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != null)
                            {
                                pdfFormFields.SetField("102", dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString());
                                diaPointer = "A";
                            }
                            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != null)
                            {
                                pdfFormFields.SetField("104", dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString());
                                diaPointer += "B";
                            }
                            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != null)
                            {
                                pdfFormFields.SetField("108", dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString());
                                diaPointer += "C";
                            }
                            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != null)
                            {
                                pdfFormFields.SetField("110", dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString());
                                diaPointer += "D";
                            }
                        }
                        else
                        {
                            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != null)
                            {
                                string[] diag1 = dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString().Split('.');
                                if (diag1.Length > 1)
                                {
                                    pdfFormFields.SetField("102", diag1[0].ToString());
                                    pdfFormFields.SetField("104", diag1[1].ToString());
                                }
                                else
                                {
                                    pdfFormFields.SetField("102", diag1[0].ToString());
                                }
                                diaPointer = "A";
                            }
                            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != null)
                            {
                                string[] diag2 = dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString().Split('.');
                                if (diag2.Length > 1)
                                {
                                    pdfFormFields.SetField("105", diag2[0].ToString());
                                    pdfFormFields.SetField("107", diag2[1].ToString());
                                }
                                else
                                {
                                    pdfFormFields.SetField("105", diag2[0].ToString());
                                }

                                diaPointer += "B";
                            }
                            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != null)
                            {
                                string[] diag3 = dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString().Split('.');
                                if (diag3.Length > 1)
                                {
                                    pdfFormFields.SetField("108", diag3[0].ToString());
                                    pdfFormFields.SetField("110", diag3[1].ToString());
                                }
                                else
                                {
                                    pdfFormFields.SetField("108", diag3[0].ToString());
                                }
                                diaPointer += "C";
                            }
                            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != null)
                            {
                                string[] diag4 = dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString().Split('.');
                                if (diag4.Length > 1)
                                {
                                    pdfFormFields.SetField("111", diag4[0].ToString());
                                    pdfFormFields.SetField("113", diag4[1].ToString());
                                }
                                else
                                {
                                    pdfFormFields.SetField("111", diag4[0].ToString());
                                }
                                diaPointer += "D";
                            }
                        }

                        ////pdfFormFields.SetField("102", "diagnosis pre");
                        ////pdfFormFields.SetField("104", "dia pro");

                        ////pdfFormFields.SetField("105", "diagnosis pre");
                        ////pdfFormFields.SetField("107", "dia pro");

                        ////pdfFormFields.SetField("108", "diagnosis pre");
                        ////pdfFormFields.SetField("110", "dia pro");

                        ////pdfFormFields.SetField("111", "diagnosis pre");
                        ////pdfFormFields.SetField("113", "dia pro");
                        string firstModifier = "";
                        string multiModifier = "";

                        string codeDesc = "";

                        double totcharge = 0.0, paidAmt = 0.0, balAmt = 0.0;
                        if (dsProcValue != null)
                        {
                            if (dsProcValue.Tables.Count > 0 && dsProcValue.Tables[0].Rows.Count > 0)
                            {
                                //////////////////////1/////////////////////////////
                                for (int i = 0; i < dsProcValue.Tables[0].Rows.Count; i++)
                                {
                                    firstModifier = "";
                                    multiModifier = "";
                                    if (dsPdfValue.Tables[0].Rows[0]["multiple_modifier"].ToString() == "1")
                                    {
                                        if (dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString() != "" && dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString() != null)
                                        {
                                            string[] modifiers = dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString().Split(',');
                                            if (modifiers.Length > 1)
                                            {
                                                firstModifier = modifiers[0].ToString();
                                                for (int j = 1; j < modifiers.Length; j++)
                                                {
                                                    multiModifier += modifiers[j].ToString() + ",";
                                                }
                                                multiModifier = multiModifier.Substring(0, multiModifier.Length - 1);
                                            }
                                            else
                                            {
                                                firstModifier = modifiers[0].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        firstModifier = dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString();
                                        multiModifier = "";
                                    }
                                    if (i == 0)
                                    {
                                        //-add Description dt 04-12-2015

                                        //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString();
                                        pdfFormFields.SetField("117", codeDesc);

                                        pdfFormFields.SetField("120", dsProcValue.Tables[0].Rows[i]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("121", dsProcValue.Tables[0].Rows[i]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("122", dsProcValue.Tables[0].Rows[i]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("123", dsProcValue.Tables[0].Rows[i]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("124", dsProcValue.Tables[0].Rows[i]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("125", dsProcValue.Tables[0].Rows[i]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("126", dsProcValue.Tables[0].Rows[i]["PALCE OF SERVICE"].ToString());
                                        //string code_length = "";
                                        code_length = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString();
                                        if (code_length.Length <= 7)
                                        {
                                            pdfFormFields.SetField("128", dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
                                        }
                                        else
                                        {
                                            pdfFormFields.SetField("128", "");
                                        }

                                        pdfFormFields.SetField("129", firstModifier);
                                        pdfFormFields.SetField("130", multiModifier);
                                        pdfFormFields.SetField("133", diaPointer);
                                        pdfFormFields.SetField("c1", dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                                        totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                        pdfFormFields.SetField("138", dsProcValue.Tables[0].Rows[i]["SZ_NPI"].ToString());
                                        pdfFormFields.SetField("136", dsProcValue.Tables[0].Rows[i]["I_UNIT"].ToString());
                                    }
                                    ///////////////////////2/////////////////////////////
                                    if (i == 1)
                                    {
                                        //-add Description dt 04-12-2015
                                        //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                                        pdfFormFields.SetField("139", codeDesc);

                                        pdfFormFields.SetField("142", dsProcValue.Tables[0].Rows[1]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("143", dsProcValue.Tables[0].Rows[1]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("144", dsProcValue.Tables[0].Rows[1]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("145", dsProcValue.Tables[0].Rows[1]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("146", dsProcValue.Tables[0].Rows[1]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("147", dsProcValue.Tables[0].Rows[1]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("148", dsProcValue.Tables[0].Rows[1]["PALCE OF SERVICE"].ToString());
                                        //pdfFormFields.SetField("150", dsProcValue.Tables[0].Rows[1]["SZ_PROCEDURE_CODE"].ToString());
                                        code_length = dsProcValue.Tables[0].Rows[1]["SZ_PROCEDURE_CODE"].ToString();
                                        if (code_length.Length <= 7)
                                        {
                                            pdfFormFields.SetField("150", dsProcValue.Tables[0].Rows[1]["SZ_PROCEDURE_CODE"].ToString());
                                        }
                                        else
                                        {
                                            pdfFormFields.SetField("150", "");
                                        }
                                        pdfFormFields.SetField("151", firstModifier);
                                        pdfFormFields.SetField("152", multiModifier);
                                        pdfFormFields.SetField("155", diaPointer);
                                        pdfFormFields.SetField("c2", dsProcValue.Tables[0].Rows[1]["FL_AMOUNT"].ToString());
                                        totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                        pdfFormFields.SetField("160", dsProcValue.Tables[0].Rows[1]["SZ_NPI"].ToString());
                                        pdfFormFields.SetField("158", dsProcValue.Tables[0].Rows[1]["I_UNIT"].ToString());

                                    }
                                    //////////////////////3/////////////////////////////
                                    if (i == 2)
                                    {
                                        //-add Description dt 04-12-2015
                                        //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                                        pdfFormFields.SetField("161", codeDesc);

                                        pdfFormFields.SetField("164", dsProcValue.Tables[0].Rows[2]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("165", dsProcValue.Tables[0].Rows[2]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("166", dsProcValue.Tables[0].Rows[2]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("167", dsProcValue.Tables[0].Rows[2]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("168", dsProcValue.Tables[0].Rows[2]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("169", dsProcValue.Tables[0].Rows[2]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("170", dsProcValue.Tables[0].Rows[2]["PALCE OF SERVICE"].ToString());
                                        //pdfFormFields.SetField("172", dsProcValue.Tables[0].Rows[2]["SZ_PROCEDURE_CODE"].ToString());
                                        code_length = dsProcValue.Tables[0].Rows[2]["SZ_PROCEDURE_CODE"].ToString();
                                        if (code_length.Length <= 7)
                                        {
                                            pdfFormFields.SetField("172", dsProcValue.Tables[0].Rows[2]["SZ_PROCEDURE_CODE"].ToString());
                                        }
                                        else
                                        {
                                            pdfFormFields.SetField("172", "");
                                        }
                                        pdfFormFields.SetField("173", firstModifier);
                                        pdfFormFields.SetField("174", multiModifier);
                                        pdfFormFields.SetField("177", diaPointer);
                                        pdfFormFields.SetField("c3", dsProcValue.Tables[0].Rows[2]["FL_AMOUNT"].ToString());
                                        totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                        pdfFormFields.SetField("182", dsProcValue.Tables[0].Rows[2]["SZ_NPI"].ToString());
                                        pdfFormFields.SetField("180", dsProcValue.Tables[0].Rows[2]["I_UNIT"].ToString());
                                    }
                                    //////////////////////4////////////////////////////
                                    if (i == 3)
                                    {
                                        //-add Description dt 04-12-2015
                                        //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                                        pdfFormFields.SetField("183", codeDesc);

                                        pdfFormFields.SetField("186", dsProcValue.Tables[0].Rows[3]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("187", dsProcValue.Tables[0].Rows[3]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("188", dsProcValue.Tables[0].Rows[3]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("189", dsProcValue.Tables[0].Rows[3]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("190", dsProcValue.Tables[0].Rows[3]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("191", dsProcValue.Tables[0].Rows[3]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("192", dsProcValue.Tables[0].Rows[3]["PALCE OF SERVICE"].ToString());
                                        //pdfFormFields.SetField("194", dsProcValue.Tables[0].Rows[3]["SZ_PROCEDURE_CODE"].ToString());
                                        code_length = dsProcValue.Tables[0].Rows[3]["SZ_PROCEDURE_CODE"].ToString();
                                        if (code_length.Length <= 7)
                                        {
                                            pdfFormFields.SetField("194", dsProcValue.Tables[0].Rows[3]["SZ_PROCEDURE_CODE"].ToString());
                                        }
                                        else
                                        {
                                            pdfFormFields.SetField("194", "");
                                        }
                                        pdfFormFields.SetField("195", firstModifier);
                                        pdfFormFields.SetField("196", multiModifier);
                                        pdfFormFields.SetField("199", diaPointer);
                                        pdfFormFields.SetField("c4", dsProcValue.Tables[0].Rows[3]["FL_AMOUNT"].ToString());
                                        totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                        pdfFormFields.SetField("204", dsProcValue.Tables[0].Rows[3]["SZ_NPI"].ToString());
                                        pdfFormFields.SetField("202", dsProcValue.Tables[0].Rows[3]["I_UNIT"].ToString());
                                    }
                                    if (i == 4)
                                    {
                                        //////////////////////5/////////////////////////////
                                        //-add Description dt 04-12-2015
                                        //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                                        pdfFormFields.SetField("205", codeDesc);

                                        pdfFormFields.SetField("208", dsProcValue.Tables[0].Rows[4]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("209", dsProcValue.Tables[0].Rows[4]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("210", dsProcValue.Tables[0].Rows[4]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("211", dsProcValue.Tables[0].Rows[4]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("212", dsProcValue.Tables[0].Rows[4]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("213", dsProcValue.Tables[0].Rows[4]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("214", dsProcValue.Tables[0].Rows[4]["PALCE OF SERVICE"].ToString());
                                        //pdfFormFields.SetField("216", dsProcValue.Tables[0].Rows[4]["SZ_PROCEDURE_CODE"].ToString());
                                        code_length = dsProcValue.Tables[0].Rows[4]["SZ_PROCEDURE_CODE"].ToString();
                                        if (code_length.Length <= 7)
                                        {
                                            pdfFormFields.SetField("216", dsProcValue.Tables[0].Rows[4]["SZ_PROCEDURE_CODE"].ToString());
                                        }
                                        else
                                        {
                                            pdfFormFields.SetField("216", "");
                                        }
                                        pdfFormFields.SetField("217", firstModifier);
                                        pdfFormFields.SetField("218", multiModifier);
                                        pdfFormFields.SetField("221", diaPointer);
                                        pdfFormFields.SetField("c5", dsProcValue.Tables[0].Rows[4]["FL_AMOUNT"].ToString());
                                        totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                        pdfFormFields.SetField("226", dsProcValue.Tables[0].Rows[4]["SZ_NPI"].ToString());
                                        pdfFormFields.SetField("224", dsProcValue.Tables[0].Rows[4]["I_UNIT"].ToString());
                                    }
                                    if (i == 5)
                                    {
                                        //////////////////////6/////////////////////////////
                                        //-add Description dt 04-12-2015
                                        //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                                        pdfFormFields.SetField("227", codeDesc);

                                        pdfFormFields.SetField("230", dsProcValue.Tables[0].Rows[5]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("231", dsProcValue.Tables[0].Rows[5]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("232", dsProcValue.Tables[0].Rows[5]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("233", dsProcValue.Tables[0].Rows[5]["DOS_MM"].ToString());
                                        pdfFormFields.SetField("234", dsProcValue.Tables[0].Rows[5]["DOS_DD"].ToString());
                                        pdfFormFields.SetField("235", dsProcValue.Tables[0].Rows[5]["DOS_YY"].ToString());
                                        pdfFormFields.SetField("236", dsProcValue.Tables[0].Rows[5]["PALCE OF SERVICE"].ToString());
                                        //pdfFormFields.SetField("238", dsProcValue.Tables[0].Rows[5]["SZ_PROCEDURE_CODE"].ToString());
                                        code_length = dsProcValue.Tables[0].Rows[5]["SZ_PROCEDURE_CODE"].ToString();
                                        if (code_length.Length <= 7)
                                        {
                                            pdfFormFields.SetField("238", dsProcValue.Tables[0].Rows[5]["SZ_PROCEDURE_CODE"].ToString());
                                        }
                                        else
                                        {
                                            pdfFormFields.SetField("238", "");
                                        }
                                        pdfFormFields.SetField("239", firstModifier);
                                        pdfFormFields.SetField("240", multiModifier);
                                        pdfFormFields.SetField("243", diaPointer);
                                        pdfFormFields.SetField("c6", dsProcValue.Tables[0].Rows[5]["FL_AMOUNT"].ToString());
                                        totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                        pdfFormFields.SetField("248", dsProcValue.Tables[0].Rows[5]["SZ_NPI"].ToString());
                                        pdfFormFields.SetField("246", dsProcValue.Tables[0].Rows[5]["I_UNIT"].ToString());
                                    }
                                }
                            }
                        }
                        ////pdfFormFields.SetField("249", "provider TAX I.D.");
                        ////pdfFormFields.SetField("250", "Yes");//
                        ////pdfFormFields.SetField("251", "Yes");
                        pdfFormFields.SetField("249", dsPdfValue.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
                        //pdfFormFields.SetField("250", "Yes");//

                        pdfFormFields.SetField("251", "No");

                        //if (dsPdfValue.Tables[0].Rows[0]["TAX_TYPE"].ToString().ToLower() == "false")
                        //{
                        //    pdfFormFields.SetField("250", "No");
                        //}
                        //else if (dsPdfValue.Tables[0].Rows[0]["TAX_TYPE"].ToString().ToLower() == "true")
                        //{
                        //    pdfFormFields.SetField("251", "No");
                        //}

                        ////pdfFormFields.SetField("252", "Pat acc no");
                        //pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_BILL_NUMBER"].ToString());
                        pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_CASE_NO"].ToString());
                        ////pdfFormFields.SetField("253", "Yes");
                        if (dsPdfValue.Tables[0].Rows[0]["ACCEPT_ASSIGNMENT"].ToString() == "1")
                        {
                            pdfFormFields.SetField("253", "No");
                        }
                        ////pdfFormFields.SetField("254", "Yes");//

                        ////pdfFormFields.SetField("261", "Doctor Name");
                        balAmt = totcharge;
                        pdfFormFields.SetField("C255", Convert.ToString(totcharge));
                        pdfFormFields.SetField("C257", paidAmt.ToString());

                        //Amod commented
                        //pdfFormFields.SetField("C259", balAmt.ToString());
                        //pdfFormFields.SetField("C259", Convert.ToString(totcharge));

                        pdfFormFields.SetField("261", dsPdfValue.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
                        pdfFormFields.SetField("31SIGN", dsPdfValue.Tables[0].Rows[0]["SIGNATURE_ON_FILE"].ToString());
                        pdfFormFields.SetField("262", dsPdfValue.Tables[0].Rows[0]["DATE"].ToString());
                        pdfFormFields.SetField("263", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString());
                        ////pdfFormFields.SetField("264", "Provider address");
                        ////pdfFormFields.SetField("265", "Provider city,state,zip");
                        pdfFormFields.SetField("264", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
                        pdfFormFields.SetField("265", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS2"].ToString());

                        ////pdfFormFields.SetField("266", "Provider NPI");
                        //pdfFormFields.SetField("266", dsProcValue.Tables[0].Rows[0]["SZ_NPI"].ToString());
                        pdfFormFields.SetField("266", dsPdfValue.Tables[0].Rows[0]["MST_OFFICE_NPI"].ToString());
                        pdfFormFields.SetField("267", dsPdfValue.Tables[0].Rows[0]["32b"].ToString());
                        // Amod - commented
                        //pdfFormFields.SetField("267", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_1_3"].ToString() + dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_4_E"].ToString());

                        ////pdfFormFields.SetField("273", "provider NPI");
                        //pdfFormFields.SetField("273", dsProcValue.Tables[0].Rows[0]["SZ_NPI"].ToString());
                        pdfFormFields.SetField("273", dsPdfValue.Tables[0].Rows[0]["MST_OFFICE_NPI_BILLING"].ToString());
                        pdfFormFields.SetField("274", dsPdfValue.Tables[0].Rows[0]["33b"].ToString());
                        ////pdfFormFields.SetField("268", "provider phone 3");
                        ////pdfFormFields.SetField("269", "provider phone rem");
                        pdfFormFields.SetField("268", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_1_3"].ToString());
                        pdfFormFields.SetField("269", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_4_E"].ToString());

                        ////pdfFormFields.SetField("270", "Billing provider name");
                        pdfFormFields.SetField("270", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_NAME_BILLING"].ToString());
                        pdfFormFields.SetField("271", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS_BILLING"].ToString());
                        pdfFormFields.SetField("272", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS2_BILLING"].ToString());

                        //Date-08_Sept_2016 change- add bill comment
                        pdfFormFields.SetField("RxComment", dsPdfValue.Tables[0].Rows[0]["sz_bill_comment"].ToString());

                        pdfStamper.FormFlattening = true;
                        pdfStamper.Close();
                    }
                }
                return newPdfFilename;
            }
            else
            {
                //dsPdfValue
                //dsProcValue 
                ArrayList List = new ArrayList();

                int dscount = dsProcValue.Tables[0].Rows.Count;
                int count = 0;
                int loop = dscount / 6;
                for (int i = 0; i < loop; i++)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("SZ_PROCEDURE_CODE");
                    dt.Columns.Add("SZ_CODE_DESCRIPTION");
                    dt.Columns.Add("FL_AMOUNT");
                    dt.Columns.Add("FL_GROUP_AMOUNT");
                    dt.Columns.Add("I_GROUP_AMOUNT_id");
                    dt.Columns.Add("DOS_MM");
                    dt.Columns.Add("DOS_DD");
                    dt.Columns.Add("DOS_YY");
                    dt.Columns.Add("SZ_NPI");
                    dt.Columns.Add("PALCE OF SERVICE");
                    dt.Columns.Add("SZ_MODIFIER");
                    dt.Columns.Add("I_UNIT");
                    int loopend = count + 6;
                    for (int j = count; j < loopend; j++)
                    // for (int j = count; j <= count + 6; j++)
                    {
                        DataRow drRow = dt.NewRow();
                        drRow["SZ_PROCEDURE_CODE"] = dsProcValue.Tables[0].Rows[j]["SZ_PROCEDURE_CODE"];
                        drRow["SZ_CODE_DESCRIPTION"] = dsProcValue.Tables[0].Rows[j]["SZ_CODE_DESCRIPTION"];
                        drRow["FL_AMOUNT"] = dsProcValue.Tables[0].Rows[j]["FL_AMOUNT"];
                        drRow["FL_GROUP_AMOUNT"] = dsProcValue.Tables[0].Rows[j]["FL_GROUP_AMOUNT"];
                        drRow["I_GROUP_AMOUNT_id"] = dsProcValue.Tables[0].Rows[j]["I_GROUP_AMOUNT_id"];
                        drRow["DOS_MM"] = dsProcValue.Tables[0].Rows[j]["DOS_MM"];
                        drRow["DOS_DD"] = dsProcValue.Tables[0].Rows[j]["DOS_DD"];
                        drRow["DOS_YY"] = dsProcValue.Tables[0].Rows[j]["DOS_YY"];
                        drRow["SZ_NPI"] = dsProcValue.Tables[0].Rows[j]["SZ_NPI"];
                        drRow["PALCE OF SERVICE"] = dsProcValue.Tables[0].Rows[j]["PALCE OF SERVICE"];
                        drRow["SZ_MODIFIER"] = dsProcValue.Tables[0].Rows[j]["SZ_MODIFIER"];
                        drRow["I_UNIT"] = dsProcValue.Tables[0].Rows[j]["I_UNIT"];
                        dt.Rows.Add(drRow);
                        count++;
                    }
                    DataSet dsProc = new DataSet();
                    dsProc.Tables.Add(dt.Copy());
                    string newSubPdfFilename = getSubFileName(i) + ".pdf";
                    PdfPrint(pdfFileName, dsPdfValue, dsProc, szCompanyID, szCaseID, newSubPdfFilename);
                    List.Add(newSubPdfFilename);
                }
                //for (int k = count; k <= dscount; k++ )
                {
                    DataTable oDT = new DataTable();
                    oDT.Columns.Add("SZ_PROCEDURE_CODE");
                    oDT.Columns.Add("SZ_CODE_DESCRIPTION");
                    oDT.Columns.Add("FL_AMOUNT");
                    oDT.Columns.Add("FL_GROUP_AMOUNT");
                    oDT.Columns.Add("I_GROUP_AMOUNT_id");
                    oDT.Columns.Add("DOS_MM");
                    oDT.Columns.Add("DOS_DD");
                    oDT.Columns.Add("DOS_YY");
                    oDT.Columns.Add("SZ_NPI");
                    oDT.Columns.Add("PALCE OF SERVICE");
                    oDT.Columns.Add("SZ_MODIFIER");
                    oDT.Columns.Add("I_UNIT");
                    for (int j = count; j < dscount; j++)
                    // for (int j = count; j <= count + 6; j++)
                    {
                        DataRow drrRow = oDT.NewRow();
                        drrRow["SZ_PROCEDURE_CODE"] = dsProcValue.Tables[0].Rows[j]["SZ_PROCEDURE_CODE"];
                        drrRow["SZ_CODE_DESCRIPTION"] = dsProcValue.Tables[0].Rows[j]["SZ_CODE_DESCRIPTION"];
                        drrRow["FL_AMOUNT"] = dsProcValue.Tables[0].Rows[j]["FL_AMOUNT"];
                        drrRow["FL_GROUP_AMOUNT"] = dsProcValue.Tables[0].Rows[j]["FL_GROUP_AMOUNT"];
                        drrRow["I_GROUP_AMOUNT_id"] = dsProcValue.Tables[0].Rows[j]["I_GROUP_AMOUNT_id"];
                        drrRow["DOS_MM"] = dsProcValue.Tables[0].Rows[j]["DOS_MM"];
                        drrRow["DOS_DD"] = dsProcValue.Tables[0].Rows[j]["DOS_DD"];
                        drrRow["DOS_YY"] = dsProcValue.Tables[0].Rows[j]["DOS_YY"];
                        drrRow["SZ_NPI"] = dsProcValue.Tables[0].Rows[j]["SZ_NPI"];
                        drrRow["PALCE OF SERVICE"] = dsProcValue.Tables[0].Rows[j]["PALCE OF SERVICE"];
                        drrRow["SZ_MODIFIER"] = dsProcValue.Tables[0].Rows[j]["SZ_MODIFIER"];
                        drrRow["I_UNIT"] = dsProcValue.Tables[0].Rows[j]["I_UNIT"];
                        oDT.Rows.Add(drrRow);
                        count++;
                    }
                    DataSet ds1Proc = new DataSet();
                    ds1Proc.Tables.Add(oDT.Copy());
                    if (oDT.Rows.Count > 0)
                    {
                        string newSubPdfFilename = getSubFileName(count) + ".pdf";
                        PdfPrint(pdfFileName, dsPdfValue, ds1Proc, szCompanyID, szCaseID, newSubPdfFilename);
                        List.Add(newSubPdfFilename);
                    }
                }
                string FirstPath = getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), szCompanyID, szCaseID);

                string F0 = "";
                string f1 = "";
                string f2 = "";
                if (List.Count == 2)
                {
                    F0 = List[0].ToString();
                    f1 = List[1].ToString();
                    f2 = getFileName(billNumber + "_merge") + ".pdf";
                    MergePDF.MergePDFFiles(FirstPath + F0, FirstPath + f1, FirstPath + f2);
                    File.Copy(FirstPath + f2, FirstPath + newPdfFilename);
                    //merge
                }
                else
                {
                    F0 = List[0].ToString();
                    f1 = "";
                    f2 = "";
                    for (int i = 0; i < List.Count - 1; i++)
                    {
                        f1 = List[i + 1].ToString();
                        f2 = getFileName(billNumber + "_" + i.ToString() + "_merge") + ".pdf";
                        //"get file name call karaych";
                        //murhe(fp/po,fp+/p1,fp+p2)
                        MergePDF.MergePDFFiles(FirstPath + F0, FirstPath + f1, FirstPath + f2);
                        F0 = f2;

                    }

                    //murhe(fp/po,fp+/p1,fp+p2)

                    File.Copy(FirstPath + f2, FirstPath + newPdfFilename);
                    //file.copy(fp/f2,fp+newPdfFilename);
                }


            }
            return newPdfFilename;
        }
        catch (Exception ex)
        {

            return ex.Message;
        }

    }
    public DataSet getPDFData(string billNumber, string company_ID)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            try
            {
                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                    comm = new SqlCommand("SP_GET_PRIVATE_BILL_NEW_SEC", conn);
                else
                    comm = new SqlCommand("SP_GET_PRIVATE_BILL_NEW", conn);
            }
            catch (Exception ex)
            {
                comm = new SqlCommand("SP_GET_PRIVATE_BILL_NEW", conn);
            }
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", billNumber);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", company_ID);

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex_getdataset)
        {
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }
    public DataSet getServiceTableData(string billNumber, string company_ID)
    {
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_GET_PROC_CODE_OF_BILL", conn);

            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", billNumber);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", company_ID);
            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex_getdataset)
        {
        }
        finally
        {
            conn.Close();
        }

        return ds;

    }
    private string getPacketDocumentFolder(string p_szPath, string p_szCompanyID, string p_szCaseID)
    {
        p_szPath = p_szPath + p_szCompanyID + "/" + p_szCaseID;
        if (Directory.Exists(p_szPath))
        {
            if (!Directory.Exists(p_szPath + "/Packet Document"))
            {
                Directory.CreateDirectory(p_szPath + "/Packet Document");
            }
        }
        else
        {
            Directory.CreateDirectory(p_szPath);
            Directory.CreateDirectory(p_szPath + "/Packet Document");
        }
        return p_szPath + "/Packet Document/";
    }
    public string getApplicationSetting(String p_szKey)
    {
        SqlConnection myConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        myConn.Open();
        String szParamValue = "";

        SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = '" + p_szKey.ToString().Trim() + "'", myConn);
        cmdQuery.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlDataReader dr;
        dr = cmdQuery.ExecuteReader();

        while (dr.Read())
        {
            szParamValue = dr["parametervalue"].ToString();
        }
        return szParamValue;
    }
    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;

    }
    private string getSubFileName(int pdfCount)
    {
        int i_PdfCount = 0;
        i_PdfCount = pdfCount;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = pdfCount.ToString() + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }
    public DataSet getValueForPdf(string bill_number)
    {
        DataSet ds = new DataSet();
        try
        {
            String strsqlCon;
            SqlConnection conn;
            SqlDataAdapter sqlda;
            SqlCommand comm;


            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            

            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "records from office and days"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            try
            {
                if (Session["GenerateBill_insType"].ToString() == "Secondary")
                    comm.CommandText = "sp_get_1500_form_details_sec";
                else
                    comm.CommandText = "sp_get_1500_form_details";
            }
            catch
            {
                comm.CommandText = "sp_get_1500_form_details";
            }
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", bill_number);


            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                return ds;
            }
            return ds;

            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return ds;
        }
    }

    public DataSet getValueForPdf(string bill_number,ServerConnection conn)
    {
        DataSet ds = new DataSet();
        try
        {
            //String strsqlCon;
            //SqlConnection conn;
            SqlDataAdapter sqlda;
            SqlCommand comm;


            //strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();


            //conn = new SqlConnection(strsqlCon);
            //conn.Open();
            #region "records from office and days"
            string Query = "";
         
          
            
            try
            {
                if (Session["GenerateBill_insType"].ToString() == "Secondary")
                       Query = Query + "Exec sp_get_1500_form_details_sec ";
                   
                else
                 Query = Query + "Exec sp_get_1500_form_details ";
                
            }
            catch
            {
                Query = Query + "Exec sp_get_1500_form_details ";
            }
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", bill_number, ",");
            Query = Query.TrimEnd(',');
            ds = conn.ExecuteWithResults(Query);


            

            if (ds.Tables[0].Rows.Count != 0)
            {
                return ds;
            }
            return ds;

            #endregion
        }
        catch (Exception ex)
        {
            throw ex;
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //return ds;
        }
    }
    public DataSet getProcedureCodeValueForPdf(string bill_number)
    {
        DataSet ds = new DataSet();
        try
        {
            String strsqlCon;
            SqlConnection conn;
            SqlDataAdapter sqlda;
            SqlCommand comm;


            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            

            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "records from office and days"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "sp_get_procedure_code_For_1500_form_details";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", bill_number);


            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                return ds;
            }
            return ds;

            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return ds;
        }
    }
    public DataSet getProcedureCodeValueForPdf(string bill_number,ServerConnection conn)
    {
        DataSet ds = new DataSet();
        try
        {
            //String strsqlCon;
            //SqlConnection conn;
            SqlDataAdapter sqlda;
            SqlCommand comm;


            //strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();


            //conn = new SqlConnection(strsqlCon);
            //conn.Open();
            #region "records from office and days"
            string Query = "";
            Query = Query + "Exec sp_get_procedure_code_For_1500_form_details ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", bill_number, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "GET_CASE_TYPE", ",");

            Query = Query.TrimEnd(',');

            ds = conn.ExecuteWithResults(Query);

            if (ds.Tables[0].Rows.Count != 0)
            {
                return ds;
            }
            return ds;

            #endregion
        }
        catch (Exception ex)
        {
            throw ex;
           // Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //return ds;
        }
    }
    public string get_bt_include(string sz_CompanyID, string ProcGroup, string CaseTypeId, string Flag)
    {
        string sz_bt_include = "";
        SqlConnection sqlCon;
        String strsqlCon;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlCon = new SqlConnection(strsqlCon);
        try
        {

            SqlCommand sqlCmd;
            SqlDataReader dr;

            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_bt_include_1500", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP", ProcGroup);
            sqlCmd.Parameters.AddWithValue("@SZ_ABBRIVATION_ID", CaseTypeId);
            sqlCmd.Parameters.AddWithValue("@flag", Flag);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                sz_bt_include = dr[0].ToString();
                return sz_bt_include;
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return sz_bt_include;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return sz_bt_include;
    }

   

    internal string ReplacePDFvalues(object pdfFileName, string szBillNumber, string cmpId, string szCaseID, string cmpName)
    {
        throw new NotImplementedException();
    }

    public string Get1500FormPdfChangeField(string szNewName, DataSet ds_table, DataSet ds, string szBillNumber, string szCaseID, string CmpId, string CmpName, string pdfname)
    {
        log.Debug("in Get1500formPdfChangeField");
        string strGenFileName = "";
        string ReadpdfFilePath = ConfigurationManager.AppSettings["UnEncrypted1500"].ToString();
        log.Debug("ReadpdfFilePath " + ReadpdfFilePath);
        string returnPath = "";
        strGenFileName = szBillNumber + "_" + szCaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff") + ".pdf";
        log.Debug("strGenFileName " + strGenFileName);
        string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
        log.Debug("szURLDocumentManager_OCT " + szURLDocumentManager_OCT);
        string pdfPath = "";
        if (Directory.Exists(szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/"))
        {
            pdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
        }
        else
        {
            Directory.CreateDirectory(szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/");
            pdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
        }
        PdfReader reader = new PdfReader(pdfname);
        PdfStamper stamper = new PdfStamper(reader, System.IO.File.Create(pdfPath));
        AcroFields fields = stamper.AcroFields;

        #region code for generate pdf Kapil
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows[0].ItemArray.GetValue(9).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_ID", "1");//other
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(3).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_Medicare", "1");//medicare
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(4).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_Medicaid", "1");//medicaid
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(5).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_Tricare", "1");//tricare campus
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(6).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_CHAMPVA", "1");//champva
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(7).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_Group_Health", "1");//group health plan
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(8).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_FECA", "1");//feca
            }

            if (ds.Tables[1].Rows[0].ItemArray.GetValue(12).ToString() == "1")
            {
                fields.SetField("SexM", "Patient_Sex_Y");
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(13).ToString() == "1")
            {
                fields.SetField("SexF", "Patient_Sex_N");//feca
            }

            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Accept_Assignment", "Accept_Assignment_Y");
            }
            else
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(76).ToString() == "1")
                {
                    fields.SetField("Accept_Assignment", "Accept_Assignment_Y");
                }
                else
                {
                    fields.SetField("Accept_Assignment_No", "Accept_Assignment_N");
                }
            if (ds.Tables[1].Rows[0].ItemArray.GetValue(91).ToString() == "1")
            {
                fields.SetField("Condition_Related_to_Employment_Y", "Employment_Y");
            }

            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Condition_Related_to_Employment_N", "Employment_N");
            }
            else
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(92).ToString() == "1")
                {
                    fields.SetField("Condition_Related_to_Employment_N", "Employment_N");
                }
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Condition_Related_to_Employment_N", "Employment_N");
            }
            //Change added on for columbus imaging in GOGYB

            if (ds.Tables[1].Rows[0].ItemArray.GetValue(93).ToString() == "1")
            {
                fields.SetField("Condition_Related_to_Auto_Accident_Y", "Auto_Accident_Y");
            }

            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Condition_Related_to_Auto_Accident_N", "Auto_Accident_N");
            }
            else
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(94).ToString() == "1")
                {
                    fields.SetField("Condition_Related_to_Auto_Accident_N", "Auto_Accident_N");
                }
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Condition_Related_to_Auto_Accident_N", "Auto_Accident_N");
            }
            //Change added on for columbus imaging in GOGYB
            if (ds.Tables[1].Rows[0].ItemArray.GetValue(95).ToString() == "1")
            {
                fields.SetField("Condition_Related_to_Other_Accident_Y", "Other_Accident_Y");
            }

            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Condition_Related_to_Other_Accident_N", "Other_Accident_N");
            }
            else
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(96).ToString() == "1")
                {
                    fields.SetField("Condition_Related_to_Other_Accident_N", "Other_Accident_N");
                }
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Condition_Related_to_Other_Accident_N", "Other_Accident_N");
            }
            //Change added on for columbus imaging in GOGYB

            if (ds.Tables[1].Rows[0].ItemArray.GetValue(100).ToString() == "1")
            {
                fields.SetField("SSN", "SN");
            }
            if (ds.Tables[1].Rows[0].ItemArray.GetValue(101).ToString() == "1")
            {
                fields.SetField("EIN", "EN");
            }
            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Patient_Relationship_to_Insured_Self", "Patient_Insured_Self");
            }
            else
            {
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(102).ToString() == "1")
                {
                    fields.SetField("Patient_Relationship_to_Insured_Self", "Patient_Insured_Self");
                }
            }

            if (CmpId == "CO000000000000000095")
            {
                //if (ds.Tables[1].Rows[0].ItemArray.GetValue(12).ToString() == "1")
                //{
                //    fields.SetField("Insured_Male_Sex", "Insured_Sex_M");
                //}
                //if (ds.Tables[1].Rows[0].ItemArray.GetValue(13).ToString() == "1")
                //{
                //    fields.SetField("Insured_Female_Sex", "Insured_Sex_F");
                //}
                fields.SetField("Insured_Male_Sex", "");
                fields.SetField("Insured_Female_Sex", "");
            }
            else
            {
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(103).ToString() == "1")
                {
                    fields.SetField("Insured_Male_Sex", "Insured_Sex_M");
                }
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(104).ToString() == "1")
                {
                    fields.SetField("Insured_Female_Sex", "Insured_Sex_F");
                }
            }


            Bill_Sys_LoginBO obj = new Bill_Sys_LoginBO();
            string value = obj.getDefaultSettings(CmpId, "SS00031");
            string szinsuredareacode = "";
            string szinsuredphone = "";

            if (value == "1")
            {

                if (ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString() != "")
                {
                    string sz_phone_area_code = "";
                    string szptphno = "";
                    string szptphno1 = "";
                    string szptphno2 = "";
                    string szFinalPhone = "";
                    string sz_patientph = ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString();
                    sz_patientph = sz_patientph.Replace("/", "");
                    sz_patientph = sz_patientph.Replace("-", "");
                    sz_patientph = sz_patientph.Replace("\\", "");
                    sz_patientph = sz_patientph.Replace("(", "");
                    sz_patientph = sz_patientph.Replace(")", "");

                    if (sz_patientph.Length > 3)
                    {
                        sz_phone_area_code = sz_patientph.Substring(0, 3);

                        szptphno = sz_patientph.Substring(3, sz_patientph.Length - 3);// sz_patientph.Substring(4, 10);
                        if (szptphno.Length > 3)
                        {
                            szptphno1 = szptphno.Substring(0, 3);
                            szptphno2 = szptphno.Substring(3, szptphno.Length - 3);
                            szFinalPhone = szptphno1 + "-" + szptphno2;
                            fields.SetField("Patients_Phone_Number_Area_Code", sz_phone_area_code);//Patient Phone area code"
                            fields.SetField("Patients_Phone_Number", szFinalPhone);//Patient Phone"
                            szinsuredareacode = sz_phone_area_code;
                            szinsuredphone = szFinalPhone;
                        }
                        else
                        {
                            fields.SetField("Patients_Phone_Number_Area_Code", sz_phone_area_code);//Patient Phone area code"
                            fields.SetField("Patients_Phone_Number", szptphno);//Patient Phone"
                            szinsuredareacode = sz_phone_area_code;
                            szinsuredphone = szptphno;
                        }

                    }
                    else
                    {
                        fields.SetField("Patients_Phone_Number_Area_Code", sz_patientph);//Patient Phone area code"
                        fields.SetField("Patients_Phone_Number", "");//Patient Phone"
                        szinsuredareacode = sz_patientph;
                        szinsuredphone = "";
                    }
                }
                else
                {
                    fields.SetField("Patients_Phone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_patient_phone_areacode"].ToString());//Patient Phone area code"
                    fields.SetField("Patients_Phone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
                    szinsuredareacode = ds.Tables[1].Rows[0]["sz_patient_phone_areacode"].ToString();
                    szinsuredphone = ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString();
                }

            }
            else
            {
                fields.SetField("Patients_Phone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
                szinsuredphone = ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString();
            }

            fields.SetField("Patient_Last_First_Name_Middle", ds.Tables[1].Rows[0].ItemArray.GetValue(10).ToString());
            fields.SetField("Patients_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(14).ToString());
            fields.SetField("Patients_City", ds.Tables[1].Rows[0].ItemArray.GetValue(15).ToString());
            fields.SetField("Patients_State", ds.Tables[1].Rows[0].ItemArray.GetValue(16).ToString());
            fields.SetField("Patients_Zip_Code", ds.Tables[1].Rows[0].ItemArray.GetValue(17).ToString());
            //fields.SetField("Patients_Phone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
            fields.SetField("Insureds_Employers_Name_or_School_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(85).ToString());

            fields.SetField("Carrier_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(20).ToString());//ins name

            fields.SetField("Carrier_First_Line_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(23).ToString());//ins adrr
            fields.SetField("Carrier_City_State_Zip_", ds.Tables[1].Rows[0].ItemArray.GetValue(24).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(25).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(29).ToString());

            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Insureds_ID_Number_1a", ds.Tables[1].Rows[0].ItemArray.GetValue(33).ToString());
                fields.SetField("Insureds_Name", "SAME");
                fields.SetField("Insured_Address", "SAME");
                fields.SetField("Insured_City", "");
                fields.SetField("Insured_State", "");
                fields.SetField("Insured_Zip", "");
                fields.SetField("Insured_Telephone_Number", "");
                fields.SetField("Insured_Telephone_Number_Area_Code", "");
            }
            else
            {
                fields.SetField("Insureds_ID_Number_1a", ds.Tables[1].Rows[0].ItemArray.GetValue(111).ToString());
                fields.SetField("Insureds_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(22).ToString());
                fields.SetField("Insured_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(114).ToString());
                fields.SetField("Insured_City", ds.Tables[1].Rows[0].ItemArray.GetValue(115).ToString());//SUNIL FOR POLICY CITY
                fields.SetField("Insured_State", ds.Tables[1].Rows[0].ItemArray.GetValue(116).ToString());//SUNIL FOR POLICY STATE
                fields.SetField("Insured_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(117).ToString());//SUNIL FOR POLICY ZIP

                if (ds.Tables[1].Rows[0].ItemArray.GetValue(102).ToString() == "1")
                {
                    fields.SetField("Insured_Telephone_Number", szinsuredphone);//change for insured code -sunil
                    fields.SetField("Insured_Telephone_Number_Area_Code", szinsuredareacode);//change for insured code -sunil
                }
                else
                {
                    fields.SetField("Insured_Telephone_Number", "");
                    fields.SetField("Insured_Telephone_Number_Area_Code", "");//change for insured code -sunil
                }
            }
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Insureds_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(10).ToString());
                fields.SetField("Insured_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(14).ToString());
                fields.SetField("Insured_City", ds.Tables[1].Rows[0].ItemArray.GetValue(15).ToString());
                fields.SetField("Insured_State", ds.Tables[1].Rows[0].ItemArray.GetValue(16).ToString());
                fields.SetField("Insured_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(17).ToString());
                fields.SetField("Insured_Telephone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
                fields.SetField("Insured_Telephone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_patient_phone_areacode"].ToString());
            }
            //Change added on for columbus imaging in GOGYB

            //fields.SetField("Insureds_ID_Number_1a", ds.Tables[1].Rows[0].ItemArray.GetValue(111).ToString());
            //fields.SetField("Insureds_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(22).ToString());
            ////fields.SetField("Insured_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(14).ToString());
            //fields.SetField("Insured_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(114).ToString());//SUNIL FOR POLICY HOLDER ADD


            //if (ds.Tables[1].Rows[0].ItemArray.GetValue(110).ToString() == "1")
            //{
            //    fields.SetField("Insured_City", ds.Tables[1].Rows[0].ItemArray.GetValue(15).ToString());//ins city
            //    fields.SetField("Insured_State", ds.Tables[1].Rows[0].ItemArray.GetValue(16).ToString());//ins state
            //    fields.SetField("Insured_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(17).ToString());//ins zip
            //    if (ds.Tables[1].Rows[0].ItemArray.GetValue(102).ToString() == "1")
            //    {
            //        fields.SetField("Insured_Telephone_Number", szinsuredphone);
            //        fields.SetField("Insured_Telephone_Number_Area_Code", szinsuredareacode);
            //    }
            //    else
            //    {
            //        fields.SetField("Insured_Telephone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
            //        fields.SetField("Insured_Telephone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_insurance_phone_areacode"].ToString());
            //    }
            //}
            //else
            //{
            //    fields.SetField("Insured_City", ds.Tables[1].Rows[0].ItemArray.GetValue(24).ToString());//ins city
            //    fields.SetField("Insured_State", ds.Tables[1].Rows[0].ItemArray.GetValue(25).ToString());//ins state
            //    fields.SetField("Insured_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(29).ToString());//ins zip
            //    if (ds.Tables[1].Rows[0].ItemArray.GetValue(102).ToString() == "1")
            //    {
            //        fields.SetField("Insured_Telephone_Number", szinsuredphone);
            //        fields.SetField("Insured_Telephone_Number_Area_Code", szinsuredareacode);
            //    }
            //    else
            //    {
            //        fields.SetField("Insured_Telephone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(30).ToString());
            //        fields.SetField("Insured_Telephone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_insurance_phone_areacode"].ToString());
            //    }
            //}

            //fields.SetField("Patients_Phone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_patient_phone_areacode"].ToString());//Patient Phone area code"
            //fields.SetField("Insured_Telephone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_insurance_phone_areacode"].ToString());//Insurance Phone area code//sunil
            fields.SetField("Employers_Name_School_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(19).ToString());
            fields.SetField("Insureds_Employers_Name_or_School_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(19).ToString());
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Patients_Account_No", ds.Tables[1].Rows[0].ItemArray.GetValue(35).ToString());
            }
            //Change added on for columbus imaging in GOGYB
            else
            {
                fields.SetField("Patients_Account_No", ds.Tables[1].Rows[0].ItemArray.GetValue(35).ToString());//patient's a/c no.
            }

            fields.SetField("Other_Insured_Policy_Number_9a", ds.Tables[1].Rows[0].ItemArray.GetValue(84).ToString());
            fields.SetField("Insured_Policy_Group_FECA_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(87).ToString());
            //change added on 03/04/2015 for adding the reffering doctor for LHR
            //try
            //{
                fields.SetField("Name_Referring_Physician_17", ds.Tables[1].Rows[0].ItemArray.GetValue(124).ToString());
            //}
            //catch (Exception io) 
            //{

            //}
            //change added on 03/04/2015 for adding the reffering doctor for LHR

            if (CmpId == "CO000000000000000095")
            {
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(41).ToString() == "1900")
                {
                    fields.SetField("Patient_Birthdate_MM", "");//patients dob(month)
                    fields.SetField("Patient_Birthdate_DD", "");//patients dob(day)
                    fields.SetField("Patient_Birthdate_YYYY", "");//patients dob(year)
                }
                else
                {
                    fields.SetField("Patient_Birthdate_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(40).ToString());//patients dob(month)
                    fields.SetField("Patient_Birthdate_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(39).ToString());//patients dob(day)
                    fields.SetField("Patient_Birthdate_YYYY", ds.Tables[1].Rows[0].ItemArray.GetValue(41).ToString());//patients dob(year)
                }
            }
            else
            {
                fields.SetField("Patient_Birthdate_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(40).ToString());//patients dob(month)
                fields.SetField("Patient_Birthdate_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(39).ToString());//patients dob(day)
                fields.SetField("Patient_Birthdate_YYYY", ds.Tables[1].Rows[0].ItemArray.GetValue(41).ToString());//patients dob(year)
            }
            ArrayList diagnosis1 = GetDiagnosis(ds.Tables[1].Rows[0].ItemArray.GetValue(44).ToString());
            fields.SetField("Diagnosis_Code_1a_21", diagnosis1[0].ToString());
            fields.SetField("Diagnosis_Code_1b_21", diagnosis1[1].ToString());
            fields.SetField("Diagnosis_Code_1c_21", diagnosis1[2].ToString());
            diagnosis1 = GetDiagnosis(ds.Tables[1].Rows[0].ItemArray.GetValue(45).ToString());
            fields.SetField("Diagnosis_Code_3a_21", diagnosis1[0].ToString());
            fields.SetField("Diagnosis_Code_3b_21", diagnosis1[1].ToString());
            fields.SetField("Diagnosis_Code_3c_21", diagnosis1[2].ToString());
            diagnosis1 = GetDiagnosis(ds.Tables[1].Rows[0].ItemArray.GetValue(46).ToString());
            fields.SetField("Diagnosis_Code_2a_21", diagnosis1[0].ToString());
            fields.SetField("Diagnosis_Code_2b_21", diagnosis1[1].ToString());
            fields.SetField("Diagnosis_Code_2c_21", diagnosis1[2].ToString());
            diagnosis1 = GetDiagnosis(ds.Tables[1].Rows[0].ItemArray.GetValue(47).ToString());
            fields.SetField("Diagnosis_Code_4a_21", diagnosis1[0].ToString());
            fields.SetField("Diagnosis_Code_4b_21", diagnosis1[1].ToString());
            fields.SetField("Diagnosis_Code_4c_21", diagnosis1[2].ToString());

            // fields.SetField("topmostSubform[0].CMS1500Form[0].Diagnosis1[1]", ds.Tables[1].Rows[0].ItemArray.GetValue(46).ToString());
            //fields.SetField("topmostSubform[0].CMS1500Form[0].Diagnosis1[3]", ds.Tables[1].Rows[0].ItemArray.GetValue(47).ToString());

            if (ds.Tables[1].Rows[0]["sz_date_phone_key_1500"].ToString()=="1")
            {
                fields.SetField("Signature", "SIGNATURE ON FILE");
            }
            else
                fields.SetField("Signature", ds.Tables[1].Rows[0].ItemArray.GetValue(10).ToString());
            fields.SetField("Signature_Date", ds.Tables[1].Rows[0].ItemArray.GetValue(113).ToString());// Sunil Change for bill date
            if (ds.Tables[1].Rows[0]["sz_date_phone_key_1500"].ToString() == "1")
            {
                fields.SetField("Insureds_or_Authorize_Persons_Signature", "SIGNATURE ON FILE");
            }
            else
                fields.SetField("Insureds_or_Authorize_Persons_Signature", ds.Tables[1].Rows[0].ItemArray.GetValue(10).ToString());

            if (CmpId == "CO000000000000000095")
            {
                //fields.SetField("Insured_Birthdate_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(39).ToString());//insured's dob(day)
                //fields.SetField("Insured_Birthdate_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(40).ToString());//insured's dob(month)
                //fields.SetField("Insured_Birthdate_YYYY", ds.Tables[1].Rows[0].ItemArray.GetValue(41).ToString());//insured's dob(year)
                fields.SetField("Insured_Birthdate_DD", "");//insured's dob(day)
                fields.SetField("Insured_Birthdate_MM", "");//insured's dob(month)
                fields.SetField("Insured_Birthdate_YYYY", "");//insured's dob(year)
            }
            else
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(105).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(106).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(106).ToString() != "0")
                {
                    fields.SetField("Insured_Birthdate_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(105).ToString());//insured's dob(day)
                    fields.SetField("Insured_Birthdate_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(106).ToString());//insured's dob(month)
                    fields.SetField("Insured_Birthdate_YYYY", ds.Tables[1].Rows[0].ItemArray.GetValue(107).ToString());//insured's dob(year)
                }
            fields.SetField("Employers_Name_School_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(84).ToString());//Employer's name
            fields.SetField("Federal_Tax_ID_or_SSN", ds.Tables[1].Rows[0].ItemArray.GetValue(74).ToString());//Federal tax Id

            //fields.SetField("topmostSubform[0].CMS1500Form[0].OtherInsuredPolicyNumber[18]", ds.Tables[1].Rows[0].ItemArray.GetValue(77).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(78).ToString());//service location info.
            //fields.SetField("topmostSubform[0].CMS1500Form[0].OtherInsuredPolicyNumber[19]", ds.Tables[1].Rows[0].ItemArray.GetValue(88).ToString() + "," +ds.Tables[1].Rows[0].ItemArray.GetValue(80).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(81).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(82).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(83).ToString());//billing provider info.
            fields.SetField("Service_Facility_Location_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(77).ToString());
            fields.SetField("Service_Facility_Location_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(78).ToString());
            // fields.SetField("Service_Facility_Location_City_State_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(105).ToString());
            fields.SetField("Billing_Provider_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(88).ToString());
            fields.SetField("Billing_Provider_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(80).ToString());
            fields.SetField("Billing_Provider_City_State_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(81).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(82).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(83).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(109).ToString());

            if (CmpId == "CO000000000000000095")
            {
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(118).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(119).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(120).ToString() != "0")
                {
                    fields.SetField("Date_Current_Illness_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(119).ToString());
                    fields.SetField("Date_Current_Illness_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(120).ToString());
                    fields.SetField("Date_Current_Illness_YY", ds.Tables[1].Rows[0].ItemArray.GetValue(121).ToString());
                }
            }
            else
            {
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(97).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(98).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(99).ToString() != "0")
                {
                    fields.SetField("Date_Current_Illness_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(97).ToString());
                    fields.SetField("Date_Current_Illness_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(98).ToString());
                    fields.SetField("Date_Current_Illness_YY", ds.Tables[1].Rows[0].ItemArray.GetValue(99).ToString());
                }
            }
            fields.SetField("Service_Facility_Location_City_State_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(108).ToString());
            fields.SetField("Signature_of_Physician_or_Supplier", ds.Tables[1].Rows[0].ItemArray.GetValue(112).ToString());//Display Reading Doc Name--Kunal
            fields.SetField("Signature_of_Physician_or_Supplier_Date", ds.Tables[1].Rows[0].ItemArray.GetValue(113).ToString());//Display Date--Kunal
            if (ds_table.Tables[0].Rows.Count > 0)
            {
                Bill_Sys_LoginBO _bill_Sys_LoginBO = new Bill_Sys_LoginBO();
                string SZ_SHOW_PROCCODE_TOTAL_1500FORM = "0";
                SZ_SHOW_PROCCODE_TOTAL_1500FORM = _bill_Sys_LoginBO.getDefaultSettings(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SS00035");

                if (SZ_SHOW_PROCCODE_TOTAL_1500FORM == "0")
                {
                    for (int i = 0; i < ds_table.Tables[0].Rows.Count; i++)
                    {
                        fields.SetField("Date_From_" + i.ToString() + "_MM", ds_table.Tables[0].Rows[i]["MONTH"].ToString());

                        fields.SetField("Date_From_" + i.ToString() + "_DD", ds_table.Tables[0].Rows[i]["DAY"].ToString());

                        fields.SetField("Date_From_" + i.ToString() + "_YY", ds_table.Tables[0].Rows[i]["YEAR"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_MM", ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_DD", ds_table.Tables[0].Rows[i]["TO_DAY"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_YY", ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString());

                        fields.SetField("Place_of_Service_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["PlaceOfService"].ToString());

                        fields.SetField("Procedure_CPT_HCPCS_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());

                        fields.SetField("Diagnosis_Pointer_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["DiagnosisPointer"].ToString());

                        fields.SetField("Dollar_Charges_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString());

                        fields.SetField("Days_or_Units_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["I_UNIT"].ToString());

                        if (CmpId != "CO000000000000000095")
                        {
                            fields.SetField("Rendering_Providers_NPI_Number_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["SZ_NPI"].ToString());
                        }

                        fields.SetField("Procedure_Modifer_" + i.ToString() + "a" + "", ds_table.Tables[0].Rows[i]["SZ_MODIFIER"].ToString());
                    }
                    //fields.SetField("Total_Charge_Dollars", ds_table.Tables[0].Rows[0]["TOTAL"].ToString());
                    //fields.SetField("Amount_Paid_Dollars", ds_table.Tables[0].Rows[0]["paid_amount"].ToString());
                    //fields.SetField("Balance_Due_Dollars", ds_table.Tables[0].Rows[0]["flt_balance"].ToString());
                }
                else
                {
                    double ProcTotal = 0;
                    for (int i = 0; i < ds_table.Tables[0].Rows.Count; i++)
                    {
                        double ProcAmt = 0;
                        fields.SetField("Date_From_" + i.ToString() + "_MM", ds_table.Tables[0].Rows[i]["MONTH"].ToString());

                        fields.SetField("Date_From_" + i.ToString() + "_DD", ds_table.Tables[0].Rows[i]["DAY"].ToString());

                        fields.SetField("Date_From_" + i.ToString() + "_YY", ds_table.Tables[0].Rows[i]["YEAR"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_MM", ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_DD", ds_table.Tables[0].Rows[i]["TO_DAY"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_YY", ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString());

                        fields.SetField("Place_of_Service_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["PlaceOfService"].ToString());

                        fields.SetField("Procedure_CPT_HCPCS_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());

                        fields.SetField("Diagnosis_Pointer_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["DiagnosisPointer"].ToString());

                        fields.SetField("Dollar_Charges_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString());

                        fields.SetField("Days_or_Units_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["I_UNIT"].ToString());

                        if (CmpId != "CO000000000000000095")
                        {
                            fields.SetField("Rendering_Providers_NPI_Number_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["SZ_NPI"].ToString());
                        }

                        fields.SetField("Procedure_Modifer_" + i.ToString() + "a" + "", ds_table.Tables[0].Rows[i]["SZ_MODIFIER"].ToString());

                        if (Convert.ToString(ds_table.Tables[0].Rows[i]["FL_AMOUNT"]) != "")
                        {
                            ProcAmt = Convert.ToDouble(ds_table.Tables[0].Rows[i]["FL_AMOUNT"]);
                            ProcTotal = ProcTotal + ProcAmt;
                        }
                    }
                    fields.SetField("Total_Charge_Dollars", ProcTotal.ToString());
                    fields.SetField("Amount_Paid_Dollars", ds_table.Tables[0].Rows[0]["paid_amount"].ToString());
                    fields.SetField("Balance_Due_Dollars", ProcTotal.ToString());
                }

            }
            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Insurance_Plan_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(20).ToString());
                fields.SetField("Another_Plan_N", "Plan_N");
                fields.SetField("Outside_Lab_N", "Lab_N");
                fields.SetField("NPI_Number_a", ds.Tables[1].Rows[0].ItemArray.GetValue(43).ToString());
                fields.SetField("Name_Referring_Physician_17", ds.Tables[1].Rows[0].ItemArray.GetValue(122).ToString());
            }
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("NPI_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(43).ToString());
            }
            //Change added on for columbus imaging in GOGYB

            fields.RenameField("Type_of_Coverage_ID", "Type_of_Coverage_ID" + szNewName);
            fields.RenameField("Type_of_Coverage_Medicare", "Type_of_Coverage_Medicare" + szNewName);
            fields.RenameField("Type_of_Coverage_Medicaid", "Type_of_Coverage_Medicaid" + szNewName);
            fields.RenameField("Type_of_Coverage_Tricare", "Type_of_Coverage_Tricare" + szNewName);
            fields.RenameField("Type_of_Coverage_CHAMPVA", "Type_of_Coverage_CHAMPVA" + szNewName);
            fields.RenameField("Type_of_Coverage_Group_Health", "Type_of_Coverage_Group_Health" + szNewName);
            fields.RenameField("Type_of_Coverage_FECA", "Type_of_Coverage_FECA" + szNewName);
            fields.RenameField("SexM", "SexM" + szNewName);
            fields.RenameField("SexF", "SexF" + szNewName);
            fields.RenameField("Accept_Assignment", "Accept_Assignment" + szNewName);
            fields.RenameField("Accept_Assignment_No", "Accept_Assignment_No" + szNewName);
            fields.RenameField("Condition_Related_to_Employment_Y", "Condition_Related_to_Employment_Y" + szNewName);
            fields.RenameField("Condition_Related_to_Employment_N", "Condition_Related_to_Employment_N" + szNewName);
            fields.RenameField("Condition_Related_to_Auto_Accident_Y", "Condition_Related_to_Auto_Accident_Y" + szNewName);
            fields.RenameField("Condition_Related_to_Auto_Accident_N", "Condition_Related_to_Auto_Accident_N" + szNewName);
            fields.RenameField("Condition_Related_to_Other_Accident_Y", "Condition_Related_to_Other_Accident_Y" + szNewName);
            fields.RenameField("Condition_Related_to_Other_Accident_N", "Condition_Related_to_Other_Accident_N" + szNewName);
            fields.RenameField("Date_Current_Illness_MM", "Date_Current_Illness_MM" + szNewName);
            fields.RenameField("Date_Current_Illness_DD", "Date_Current_Illness_DD" + szNewName);
            fields.RenameField("Date_Current_Illness_YY", "Date_Current_Illness_YY" + szNewName);
            fields.RenameField("SSN", "SSN" + szNewName);
            fields.RenameField("EIN", "EIN" + szNewName);
            fields.RenameField("Patient_Relationship_to_Insured_Self", "Patient_Relationship_to_Insured_Self" + szNewName);
            fields.RenameField("Insured_Male_Sex", "Insured_Male_Sex" + szNewName);
            fields.RenameField("Insured_Female_Sex", "Insured_Female_Sex" + szNewName);
            fields.RenameField("Patient_Last_First_Name_Middle", "Patient_Last_First_Name_Middle" + szNewName);
            fields.RenameField("Patients_Address", "Patients_Address" + szNewName);
            fields.RenameField("Patients_City", "Patients_City" + szNewName);
            fields.RenameField("Patients_State", "Patients_State" + szNewName);
            fields.RenameField("Patients_Zip_Code", "Patients_Zip_Code" + szNewName);
            fields.RenameField("Patients_Phone_Number", "Patients_Phone_Number" + szNewName);
            fields.RenameField("Insureds_Employers_Name_or_School_Name", "Insureds_Employers_Name_or_School_Name" + szNewName);
            fields.RenameField("First Name Middle Initial Last NameEMPLOYER", "First Name Middle Initial Last NameEMPLOYER" + szNewName);
            fields.RenameField("Carrier_Name", "Carrier_Name" + szNewName);
            fields.RenameField("Carrier_First_Line_Address", "Carrier_First_Line_Address" + szNewName);
            fields.RenameField("Carrier_City_State_Zip_", "Carrier_City_State_Zip_" + szNewName);
            fields.RenameField("Insureds_ID_Number_1a", "Insureds_ID_Number_1a" + szNewName);
            fields.RenameField("Insureds_Name", "Insureds_Name" + szNewName);

            fields.RenameField("Insured_Address", "Insured_Address" + szNewName);
            fields.RenameField("Insured_City", "Insured_City" + szNewName);
            fields.RenameField("Insured_State", "Insured_State" + szNewName);
            fields.RenameField("Patients_Phone_Number_Area_Code", "Patients_Phone_Number_Area_Code" + szNewName);
            fields.RenameField("Insured_Telephone_Number_Area_Code", "Insured_Telephone_Number_Area_Code" + szNewName);
            fields.RenameField("Insured_Zip", "Insured_Zip" + szNewName);
            fields.RenameField("Insured_Telephone_Number", "Insured_Telephone_Number" + szNewName);
            fields.RenameField("Employers_Name_School_Name", "Employers_Name_School_Name" + szNewName);
            fields.RenameField("Insureds_Employers_Name_or_School_Name", "Insureds_Employers_Name_or_School_Name" + szNewName);
            fields.RenameField("Patients_Account_No", "Patients_Account_No" + szNewName);
            fields.RenameField("Other_Insured_Policy_Number_9a", "Other_Insured_Policy_Number_9a" + szNewName);
            fields.RenameField("Insured_Policy_Group_FECA_Number", "Insured_Policy_Group_FECA_Number" + szNewName);
            fields.RenameField("Patient_Birthdate_MM", "Patient_Birthdate_MM" + szNewName);
            fields.RenameField("Patient_Birthdate_DD", "Patient_Birthdate_DD" + szNewName);
            fields.RenameField("Patient_Birthdate_YYYY", "Patient_Birthdate_YYYY" + szNewName);
            fields.RenameField("Diagnosis_Code_1a_21", "Diagnosis_Code_1a_21" + szNewName);
            fields.RenameField("Diagnosis_Code_1b_21", "Diagnosis_Code_1b_21" + szNewName);
            fields.RenameField("Diagnosis_Code_1c_21", "Diagnosis_Code_1c_21" + szNewName);
            fields.RenameField("Diagnosis_Code_3a_21", "Diagnosis_Code_3a_21" + szNewName);
            fields.RenameField("Diagnosis_Code_3b_21", "Diagnosis_Code_3b_21" + szNewName);
            fields.RenameField("Diagnosis_Code_3c_21", "Diagnosis_Code_3c_21" + szNewName);
            fields.RenameField("Diagnosis_Code_2a_21", "Diagnosis_Code_2a_21" + szNewName);
            fields.RenameField("Diagnosis_Code_2b_21", "Diagnosis_Code_2b_21" + szNewName);
            fields.RenameField("Diagnosis_Code_2c_21", "Diagnosis_Code_2c_21" + szNewName);
            fields.RenameField("Diagnosis_Code_4a_21", "Diagnosis_Code_4a_21" + szNewName);
            fields.RenameField("Diagnosis_Code_4b_21", "Diagnosis_Code_4b_21" + szNewName);
            fields.RenameField("Diagnosis_Code_4c_21", "Diagnosis_Code_4c_21" + szNewName);

            fields.RenameField("Signature", "Signature" + szNewName);
            fields.RenameField("Signature_Date", "Signature_Date" + szNewName);
            fields.RenameField("Insureds_or_Authorize_Persons_Signature", "Insureds_or_Authorize_Persons_Signature" + szNewName);

            fields.RenameField("Insured_Birthdate_DD", "Insured_Birthdate_DD" + szNewName);
            fields.RenameField("Insured_Birthdate_MM", "Insured_Birthdate_MM" + szNewName);
            fields.RenameField("Insured_Birthdate_YYYY", "Insured_Birthdate_YYYY" + szNewName);
            fields.RenameField("Employers_Name_School_Name", "Employers_Name_School_Name" + szNewName);
            fields.RenameField("Federal_Tax_ID_or_SSN", "Federal_Tax_ID_or_SSN" + szNewName);
            fields.RenameField("Service_Facility_Location_Name", "Service_Facility_Location_Name" + szNewName);

            fields.RenameField("Service_Facility_Location_Address", "Service_Facility_Location_Address" + szNewName);
            fields.RenameField("Service_Facility_Location_City_State_Zip", "Service_Facility_Location_City_State_Zip" + szNewName);
            fields.RenameField("Billing_Provider_Name", "Billing_Provider_Name" + szNewName);
            fields.RenameField("Billing_Provider_Address", "Billing_Provider_Address" + szNewName);
            fields.RenameField("Billing_Provider_City_State_Zip", "Billing_Provider_City_State_Zip" + szNewName);

            fields.RenameField("Date_From_0_MM", "Date_From_0_MM" + szNewName);
            fields.RenameField("Date_From_1_MM", "Date_From_1_MM" + szNewName);
            fields.RenameField("Date_From_2_MM", "Date_From_2_MM" + szNewName);
            fields.RenameField("Date_From_3_MM", "Date_From_3_MM" + szNewName);
            fields.RenameField("Date_From_4_MM", "Date_From_4_MM" + szNewName);
            fields.RenameField("Date_From_5_MM", "Date_From_5_MM" + szNewName);

            fields.RenameField("Date_From_0_DD", "Date_From_0_DD" + szNewName);
            fields.RenameField("Date_From_1_DD", "Date_From_1_DD" + szNewName);
            fields.RenameField("Date_From_2_DD", "Date_From_2_DD" + szNewName);
            fields.RenameField("Date_From_3_DD", "Date_From_3_DD" + szNewName);
            fields.RenameField("Date_From_4_DD", "Date_From_4_DD" + szNewName);
            fields.RenameField("Date_From_5_DD", "Date_From_5_DD" + szNewName);

            fields.RenameField("Date_From_0_YY", "Date_From_0_YY" + szNewName);
            fields.RenameField("Date_From_1_YY", "Date_From_1_YY" + szNewName);
            fields.RenameField("Date_From_2_YY", "Date_From_2_YY" + szNewName);
            fields.RenameField("Date_From_3_YY", "Date_From_3_YY" + szNewName);
            fields.RenameField("Date_From_4_YY", "Date_From_4_YY" + szNewName);
            fields.RenameField("Date_From_5_YY", "Date_From_5_YY" + szNewName);

            fields.RenameField("Date_To_0_MM", "Date_To_0_MM" + szNewName);
            fields.RenameField("Date_To_1_MM", "Date_To_1_MM" + szNewName);
            fields.RenameField("Date_To_2_MM", "Date_To_2_MM" + szNewName);
            fields.RenameField("Date_To_3_MM", "Date_To_3_MM" + szNewName);
            fields.RenameField("Date_To_4_MM", "Date_To_4_MM" + szNewName);
            fields.RenameField("Date_To_5_MM", "Date_To_5_MM" + szNewName);

            fields.RenameField("Date_To_0_DD", "Date_To_0_DD" + szNewName);
            fields.RenameField("Date_To_1_DD", "Date_To_1_DD" + szNewName);
            fields.RenameField("Date_To_2_DD", "Date_To_2_DD" + szNewName);
            fields.RenameField("Date_To_3_DD", "Date_To_3_DD" + szNewName);
            fields.RenameField("Date_To_4_DD", "Date_To_4_DD" + szNewName);
            fields.RenameField("Date_To_5_DD", "Date_To_5_DD" + szNewName);

            fields.RenameField("Date_To_0_YY", "Date_To_0_YY" + szNewName);
            fields.RenameField("Date_To_1_YY", "Date_To_1_YY" + szNewName);
            fields.RenameField("Date_To_2_YY", "Date_To_2_YY" + szNewName);
            fields.RenameField("Date_To_3_YY", "Date_To_3_YY" + szNewName);
            fields.RenameField("Date_To_4_YY", "Date_To_4_YY" + szNewName);
            fields.RenameField("Date_To_5_YY", "Date_To_5_YY" + szNewName);

            fields.RenameField("Place_of_Service_0", "Place_of_Service_0" + szNewName);
            fields.RenameField("Place_of_Service_1", "Place_of_Service_1" + szNewName);
            fields.RenameField("Place_of_Service_2", "Place_of_Service_2" + szNewName);
            fields.RenameField("Place_of_Service_3", "Place_of_Service_3" + szNewName);
            fields.RenameField("Place_of_Service_4", "Place_of_Service_4" + szNewName);
            fields.RenameField("Place_of_Service_5", "Place_of_Service_5" + szNewName);

            fields.RenameField("Procedure_CPT_HCPCS_0", "Procedure_CPT_HCPCS_0" + szNewName);
            fields.RenameField("Procedure_CPT_HCPCS_1", "Procedure_CPT_HCPCS_1" + szNewName);
            fields.RenameField("Procedure_CPT_HCPCS_2", "Procedure_CPT_HCPCS_2" + szNewName);
            fields.RenameField("Procedure_CPT_HCPCS_3", "Procedure_CPT_HCPCS_3" + szNewName);
            fields.RenameField("Procedure_CPT_HCPCS_4", "Procedure_CPT_HCPCS_4" + szNewName);
            fields.RenameField("Procedure_CPT_HCPCS_5", "Procedure_CPT_HCPCS_5" + szNewName);

            fields.RenameField("Diagnosis_Pointer_0", "Diagnosis_Pointer_0" + szNewName);
            fields.RenameField("Diagnosis_Pointer_1", "Diagnosis_Pointer_1" + szNewName);
            fields.RenameField("Diagnosis_Pointer_2", "Diagnosis_Pointer_2" + szNewName);
            fields.RenameField("Diagnosis_Pointer_3", "Diagnosis_Pointer_3" + szNewName);
            fields.RenameField("Diagnosis_Pointer_4", "Diagnosis_Pointer_4" + szNewName);
            fields.RenameField("Diagnosis_Pointer_5", "Diagnosis_Pointer_5" + szNewName);

            fields.RenameField("Dollar_Charges_0", "Dollar_Charges_0" + szNewName);
            fields.RenameField("Dollar_Charges_1", "Dollar_Charges_1" + szNewName);
            fields.RenameField("Dollar_Charges_2", "Dollar_Charges_2" + szNewName);
            fields.RenameField("Dollar_Charges_3", "Dollar_Charges_3" + szNewName);
            fields.RenameField("Dollar_Charges_4", "Dollar_Charges_4" + szNewName);
            fields.RenameField("Dollar_Charges_5", "Dollar_Charges_5" + szNewName);

            fields.RenameField("Days_or_Units_0", "Days_or_Units_0" + szNewName);
            fields.RenameField("Days_or_Units_1", "Days_or_Units_1" + szNewName);
            fields.RenameField("Days_or_Units_2", "Days_or_Units_2" + szNewName);
            fields.RenameField("Days_or_Units_3", "Days_or_Units_3" + szNewName);
            fields.RenameField("Days_or_Units_4", "Days_or_Units_4" + szNewName);
            fields.RenameField("Days_or_Units_5", "Days_or_Units_5" + szNewName);

            fields.RenameField("Rendering_Providers_NPI_Number_0", "Rendering_Providers_NPI_Number_0" + szNewName);
            fields.RenameField("Rendering_Providers_NPI_Number_1", "Rendering_Providers_NPI_Number_1" + szNewName);
            fields.RenameField("Rendering_Providers_NPI_Number_2", "Rendering_Providers_NPI_Number_2" + szNewName);
            fields.RenameField("Rendering_Providers_NPI_Number_3", "Rendering_Providers_NPI_Number_3" + szNewName);
            fields.RenameField("Rendering_Providers_NPI_Number_4", "Rendering_Providers_NPI_Number_4" + szNewName);
            fields.RenameField("Rendering_Providers_NPI_Number_5", "Rendering_Providers_NPI_Number_5" + szNewName);

            fields.RenameField("Total_Charge_Dollars", "Total_Charge_Dollars" + szNewName);
            fields.RenameField("Amount_Paid_Dollars", "Amount_Paid_Dollars" + szNewName);
            fields.RenameField("Balance_Due_Dollars", "Balance_Due_Dollars" + szNewName);
            fields.RenameField("Signature_of_Physician_or_Supplier", "Signature_of_Physician_or_Supplier" + szNewName);
            fields.RenameField("Signature_of_Physician_or_Supplier_Date", "Signature_of_Physician_or_Supplier_Date" + szNewName);
            //fields.RenameField("Name_Referring_Physician_17", "Name_Referring_Physician_17" + szNewName);
            //change added on 03/04/2015 for adding the reffering doctor for LHR
            fields.RenameField("Name_Referring_Physician_17", "Name_Referring_Physician_17" + szNewName);
            //change added on 03/04/2015 for adding the reffering doctor for LHR

            fields.RenameField("Insurance_Plan_Name", "Insurance_Plan_Name" + szNewName);
            fields.RenameField("Another_Plan_N", "Another_Plan_N" + szNewName);
            fields.RenameField("Outside_Lab_N", "Outside_Lab_N" + szNewName);
            fields.RenameField("NPI_Number_a", "NPI_Number_a" + szNewName);
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.RenameField("NPI_Number", "NPI_Number" + szNewName);
            }
            //Change added on for columbus imaging in GOGYB
            
            stamper.Close();

        }
        #endregion
        string openPath = CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
        returnPath = openPath;
        return returnPath;

    }

    public string Get1500FormPdf(string szNewName, DataSet ds_table, DataSet ds, string szBillNumber, string szCaseID, string CmpId, string CmpName, string pdfname)
    {
        string strGenFileName = "";
        string ReadpdfFilePath = ConfigurationManager.AppSettings["UnEncrypted1500"].ToString();
        string returnPath = "";
        strGenFileName = szBillNumber + "_" + szCaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff") + ".pdf";
        string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
        string pdfPath = "";
        if (Directory.Exists(szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/"))
        {
            pdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
        }
        else
        {
            Directory.CreateDirectory(szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/");
            pdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
        }
        PdfReader reader = new PdfReader(pdfname);
        PdfStamper stamper = new PdfStamper(reader, System.IO.File.Create(pdfPath));
        AcroFields fields = stamper.AcroFields;
        #region code for generate pdf Pravin
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows[0].ItemArray.GetValue(9).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_ID", "1");//other
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(3).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_Medicare", "1");//medicare
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(4).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_Medicaid", "1");//medicaid
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(5).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_Tricare", "1");//tricare campus
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(6).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_CHAMPVA", "1");//champva
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(7).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_Group_Health", "1");//group health plan
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(8).ToString() == "1")
            {
                fields.SetField("Type_of_Coverage_FECA", "1");//feca
            }

            if (ds.Tables[1].Rows[0].ItemArray.GetValue(12).ToString() == "1")
            {
                fields.SetField("SexM", "Patient_Sex_Y");
            }
            else if (ds.Tables[1].Rows[0].ItemArray.GetValue(13).ToString() == "1")
            {
                fields.SetField("SexF", "Patient_Sex_N");//feca
            }

            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Accept_Assignment", "Accept_Assignment_Y");
            }
            else
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(76).ToString() == "1")
                {
                    fields.SetField("Accept_Assignment", "Accept_Assignment_Y");
                }
                else
                {
                    fields.SetField("Accept_Assignment_No", "Accept_Assignment_N");
                }
            if (ds.Tables[1].Rows[0].ItemArray.GetValue(91).ToString() == "1")
            {
                fields.SetField("Condition_Related_to_Employment_Y", "Employment_Y");
            }

            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Condition_Related_to_Employment_N", "Employment_N");
            }
            else
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(92).ToString() == "1")
                {
                    fields.SetField("Condition_Related_to_Employment_N", "Employment_N");
                }
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Condition_Related_to_Employment_N", "Employment_N");
            }
            //Change added on for columbus imaging in GOGYB


            if (ds.Tables[1].Rows[0].ItemArray.GetValue(93).ToString() == "1")
            {
                fields.SetField("Condition_Related_to_Auto_Accident_Y", "Auto_Accident_Y");
            }

            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Condition_Related_to_Auto_Accident_N", "Auto_Accident_N");
            }
            else
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(94).ToString() == "1")
                {
                    fields.SetField("Condition_Related_to_Auto_Accident_N", "Auto_Accident_N");
                }
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Condition_Related_to_Auto_Accident_N", "Auto_Accident_N");
            }
            //Change added on for columbus imaging in GOGYB
            if (ds.Tables[1].Rows[0].ItemArray.GetValue(95).ToString() == "1")
            {
                fields.SetField("Condition_Related_to_Other_Accident_Y", "Other_Accident_Y");
            }

            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Condition_Related_to_Other_Accident_N", "Other_Accident_N");
            }
            else
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(96).ToString() == "1")
                {
                    fields.SetField("Condition_Related_to_Other_Accident_N", "Other_Accident_N");
                }
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Condition_Related_to_Other_Accident_N", "Other_Accident_N");
            }
            //Change added on for columbus imaging in GOGYB

            if (ds.Tables[1].Rows[0].ItemArray.GetValue(100).ToString() == "1")
            {
                fields.SetField("SSN", "SN");
            }
            if (ds.Tables[1].Rows[0].ItemArray.GetValue(101).ToString() == "1")
            {
                fields.SetField("EIN", "EN");
            }

           if (ds.Tables[1].Rows[0]["sz_date_phone_key_1500"].ToString()=="1")
            {
                fields.SetField("Patient_Relationship_to_Insured_Self", "Patient_Insured_Self");
            }
            else
            {
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(102).ToString() == "1")
                {
                    fields.SetField("Patient_Relationship_to_Insured_Self", "Patient_Insured_Self");
                }
            }

            if (CmpId == "CO000000000000000095")
            {
                //if (ds.Tables[1].Rows[0].ItemArray.GetValue(12).ToString() == "1")
                //{
                //    fields.SetField("Insured_Male_Sex", "Insured_Sex_M");
                //}
                //if (ds.Tables[1].Rows[0].ItemArray.GetValue(13).ToString() == "1")
                //{
                //    fields.SetField("Insured_Female_Sex", "Insured_Sex_F");
                //}
                fields.SetField("Insured_Male_Sex", "");
                fields.SetField("Insured_Female_Sex", "");
            }
            else
            {

                if (ds.Tables[1].Rows[0].ItemArray.GetValue(103).ToString() == "1")
                {
                    fields.SetField("Insured_Male_Sex", "Insured_Sex_M");
                }
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(104).ToString() == "1")
                {
                    fields.SetField("Insured_Female_Sex", "Insured_Sex_F");
                }
            }


            Bill_Sys_LoginBO obj = new Bill_Sys_LoginBO();
            string value = obj.getDefaultSettings(CmpId, "SS00031");
            string szinsuredareacode = "";
            string szinsuredphone = "";
            if (value == "1")
            {
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString() != "")
                {
                    string sz_phone_area_code = "";
                    string szptphno = "";
                    string szptphno1 = "";
                    string szptphno2 = "";
                    string szFinalPhone = "";
                    string sz_patientph = ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString();
                    sz_patientph = sz_patientph.Replace("/", "");
                    sz_patientph = sz_patientph.Replace("-", "");
                    sz_patientph = sz_patientph.Replace("\\", "");
                    sz_patientph = sz_patientph.Replace("(", "");
                    sz_patientph = sz_patientph.Replace(")", "");

                    if (sz_patientph.Length > 3)
                    {
                        sz_phone_area_code = sz_patientph.Substring(0, 3);

                        szptphno = sz_patientph.Substring(3, sz_patientph.Length - 3);// sz_patientph.Substring(4, 10);
                        if (szptphno.Length > 3)
                        {
                            szptphno1 = szptphno.Substring(0, 3);
                            szptphno2 = szptphno.Substring(3, szptphno.Length - 3);
                            szFinalPhone = szptphno1 + "-" + szptphno2;
                            fields.SetField("Patients_Phone_Number_Area_Code", sz_phone_area_code);//Patient Phone area code"
                            fields.SetField("Patients_Phone_Number", szFinalPhone);//Patient Phone"
                            szinsuredareacode = sz_phone_area_code;
                            szinsuredphone = szFinalPhone;

                        }
                        else
                        {
                            fields.SetField("Patients_Phone_Number_Area_Code", sz_phone_area_code);//Patient Phone area code"
                            fields.SetField("Patients_Phone_Number", szptphno);//Patient Phone"
                            szinsuredareacode = sz_phone_area_code;
                            szinsuredphone = szptphno;
                        }

                    }
                    else
                    {
                        fields.SetField("Patients_Phone_Number_Area_Code", sz_patientph);//Patient Phone area code"
                        fields.SetField("Patients_Phone_Number", "");//Patient Phone"
                        szinsuredareacode = sz_patientph;
                        szinsuredphone = "";
                    }
                }
                else
                {
                    fields.SetField("Patients_Phone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_patient_phone_areacode"].ToString());//Patient Phone area code"
                    fields.SetField("Patients_Phone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
                    szinsuredareacode = ds.Tables[1].Rows[0]["sz_patient_phone_areacode"].ToString();
                    szinsuredphone = ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString();
                }

            }
            else
            {
                fields.SetField("Patients_Phone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
                szinsuredphone = ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString();
            }


            fields.SetField("Patient_Last_First_Name_Middle", ds.Tables[1].Rows[0].ItemArray.GetValue(10).ToString());
            fields.SetField("Patients_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(14).ToString());
            fields.SetField("Patients_City", ds.Tables[1].Rows[0].ItemArray.GetValue(15).ToString());
            fields.SetField("Patients_State", ds.Tables[1].Rows[0].ItemArray.GetValue(16).ToString());
            fields.SetField("Patients_Zip_Code", ds.Tables[1].Rows[0].ItemArray.GetValue(17).ToString());
            //fields.SetField("Patients_Phone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
            fields.SetField("Insureds_Employers_Name_or_School_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(85).ToString());

            fields.SetField("Carrier_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(20).ToString());//ins name

            fields.SetField("Carrier_First_Line_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(23).ToString());//ins adrr
            fields.SetField("Carrier_City_State_Zip_", ds.Tables[1].Rows[0].ItemArray.GetValue(24).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(25).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(29).ToString());
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Insureds_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(10).ToString());
                fields.SetField("Insured_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(14).ToString());
                fields.SetField("Insured_City", ds.Tables[1].Rows[0].ItemArray.GetValue(15).ToString());
                fields.SetField("Insured_State", ds.Tables[1].Rows[0].ItemArray.GetValue(16).ToString());
                fields.SetField("Insured_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(17).ToString());
                fields.SetField("Insured_Telephone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
                fields.SetField("Insured_Telephone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_patient_phone_areacode"].ToString());
            }

            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Insureds_ID_Number_1a", ds.Tables[1].Rows[0].ItemArray.GetValue(33).ToString());
                fields.SetField("Insureds_Name", "SAME");
                fields.SetField("Insured_Address", "SAME");
                fields.SetField("Insured_City", "");
                fields.SetField("Insured_State", "");
                fields.SetField("Insured_Zip", "");
                fields.SetField("Insured_Telephone_Number", "");
                fields.SetField("Insured_Telephone_Number_Area_Code", "");
            }
            else if (CmpId == "CO000000000000000095")
            {

            }
            else
            {
                fields.SetField("Insureds_ID_Number_1a", ds.Tables[1].Rows[0].ItemArray.GetValue(111).ToString());
                fields.SetField("Insureds_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(22).ToString());
                fields.SetField("Insured_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(114).ToString());
                fields.SetField("Insured_City", ds.Tables[1].Rows[0].ItemArray.GetValue(115).ToString());//SUNIL FOR POLICY CITY
                fields.SetField("Insured_State", ds.Tables[1].Rows[0].ItemArray.GetValue(116).ToString());//SUNIL FOR POLICY STATE
                fields.SetField("Insured_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(117).ToString());//SUNIL FOR POLICY ZIP

                if (ds.Tables[1].Rows[0].ItemArray.GetValue(102).ToString() == "1")
                {
                    fields.SetField("Insured_Telephone_Number", szinsuredphone);//change for insured code -sunil
                    fields.SetField("Insured_Telephone_Number_Area_Code", szinsuredareacode);//change for insured code -sunil
                }
                else
                {
                    fields.SetField("Insured_Telephone_Number", "");
                    fields.SetField("Insured_Telephone_Number_Area_Code", "");//change for insured code -sunil
                }
            }
            //Change added on 18/03/2015 for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Insureds_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(10).ToString());
                fields.SetField("Insured_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(14).ToString());
                fields.SetField("Insured_City", ds.Tables[1].Rows[0].ItemArray.GetValue(15).ToString());
                fields.SetField("Insured_State", ds.Tables[1].Rows[0].ItemArray.GetValue(16).ToString());
                fields.SetField("Insured_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(17).ToString());
                fields.SetField("Insured_Telephone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
            }
            //Change added on for columbus imaging in GOGYB

            //if (ds.Tables[1].Rows[0].ItemArray.GetValue(110).ToString() == "1")
            //{
            //    fields.SetField("Insured_City", ds.Tables[1].Rows[0].ItemArray.GetValue(15).ToString());//ins city
            //    fields.SetField("Insured_State", ds.Tables[1].Rows[0].ItemArray.GetValue(16).ToString());//ins state
            //    fields.SetField("Insured_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(17).ToString());//ins zip
            //    if (ds.Tables[1].Rows[0].ItemArray.GetValue(102).ToString() == "1")
            //    {
            //        fields.SetField("Insured_Telephone_Number", szinsuredphone);//change for insured code -sunil
            //        fields.SetField("Insured_Telephone_Number_Area_Code", szinsuredareacode);//change for insured code -sunil
            //    }
            //    else
            //    {
            //        fields.SetField("Insured_Telephone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(18).ToString());
            //        fields.SetField("Insured_Telephone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_insurance_phone_areacode"].ToString());//change for insured code -sunil
            //    }
            //}
            //else
            //{
            //    fields.SetField("Insured_City", ds.Tables[1].Rows[0].ItemArray.GetValue(24).ToString());//ins city
            //    fields.SetField("Insured_State", ds.Tables[1].Rows[0].ItemArray.GetValue(25).ToString());//ins state
            //    fields.SetField("Insured_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(29).ToString());//ins zip
            //    if (ds.Tables[1].Rows[0].ItemArray.GetValue(102).ToString() == "1")
            //    {
            //        fields.SetField("Insured_Telephone_Number", szinsuredphone);//change for insured code -sunil
            //        fields.SetField("Insured_Telephone_Number_Area_Code", szinsuredareacode);//change for insured code -sunil
            //    }
            //    else
            //    {
            //        fields.SetField("Insured_Telephone_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(30).ToString());
            //        fields.SetField("Insured_Telephone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_insurance_phone_areacode"].ToString());//change for insured code -sunil
            //    }
            // }

            //fields.SetField("Patients_Phone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_patient_phone_areacode"].ToString());//Patient Phone area code"
            //fields.SetField("Insured_Telephone_Number_Area_Code", ds.Tables[1].Rows[0]["sz_insurance_phone_areacode"].ToString());//Insurance Phone area code sunil

            fields.SetField("Employers_Name_School_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(19).ToString());
            fields.SetField("Insureds_Employers_Name_or_School_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(19).ToString());
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("Patients_Account_No", ds.Tables[1].Rows[0].ItemArray.GetValue(35).ToString());
            }
            //Change added on for columbus imaging in GOGYB
            else
            {
                fields.SetField("Patients_Account_No", ds.Tables[1].Rows[0].ItemArray.GetValue(35).ToString());//patient's a/c no.
            }

            fields.SetField("Other_Insured_Policy_Number_9a", ds.Tables[1].Rows[0].ItemArray.GetValue(84).ToString());
            fields.SetField("Insured_Policy_Group_FECA_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(87).ToString());
            //change added on 03/04/2015 for adding the reffering doctor for LHR
            //try
            //{
            fields.SetField("Name_Referring_Physician_17", ds.Tables[1].Rows[0].ItemArray.GetValue(124).ToString());
            //}
            //catch (Exception io) 
            //{

            //}
            //change added on 03/04/2015 for adding the reffering doctor for LHR

            if (CmpId == "CO000000000000000095")
            {
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(41).ToString() == "1900")
                {
                    fields.SetField("Patient_Birthdate_MM", "");//patients dob(month)
                    fields.SetField("Patient_Birthdate_DD", "");//patients dob(day)
                    fields.SetField("Patient_Birthdate_YYYY", "");//patients dob(year)
                }
                else
                {
                    fields.SetField("Patient_Birthdate_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(40).ToString());//patients dob(month)
                    fields.SetField("Patient_Birthdate_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(39).ToString());//patients dob(day)
                    fields.SetField("Patient_Birthdate_YYYY", ds.Tables[1].Rows[0].ItemArray.GetValue(41).ToString());//patients dob(year)
                }
            }
            else
            {
                fields.SetField("Patient_Birthdate_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(40).ToString());//patients dob(month)
                fields.SetField("Patient_Birthdate_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(39).ToString());//patients dob(day)
                fields.SetField("Patient_Birthdate_YYYY", ds.Tables[1].Rows[0].ItemArray.GetValue(41).ToString());//patients dob(year)
            }
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("NPI_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(43).ToString());
            }
            //Change added on for columbus imaging in GOGYB
            ArrayList diagnosis1 = GetDiagnosis(ds.Tables[1].Rows[0].ItemArray.GetValue(44).ToString());
            fields.SetField("Diagnosis_Code_1a_21", diagnosis1[0].ToString());
            fields.SetField("Diagnosis_Code_1b_21", diagnosis1[1].ToString());
            fields.SetField("Diagnosis_Code_1c_21", diagnosis1[2].ToString());
            diagnosis1 = GetDiagnosis(ds.Tables[1].Rows[0].ItemArray.GetValue(45).ToString());
            fields.SetField("Diagnosis_Code_3a_21", diagnosis1[0].ToString());
            fields.SetField("Diagnosis_Code_3b_21", diagnosis1[1].ToString());
            fields.SetField("Diagnosis_Code_3c_21", diagnosis1[2].ToString());
            diagnosis1 = GetDiagnosis(ds.Tables[1].Rows[0].ItemArray.GetValue(46).ToString());
            fields.SetField("Diagnosis_Code_2a_21", diagnosis1[0].ToString());
            fields.SetField("Diagnosis_Code_2b_21", diagnosis1[1].ToString());
            fields.SetField("Diagnosis_Code_2c_21", diagnosis1[2].ToString());
            diagnosis1 = GetDiagnosis(ds.Tables[1].Rows[0].ItemArray.GetValue(47).ToString());
            fields.SetField("Diagnosis_Code_4a_21", diagnosis1[0].ToString());
            fields.SetField("Diagnosis_Code_4b_21", diagnosis1[1].ToString());
            fields.SetField("Diagnosis_Code_4c_21", diagnosis1[2].ToString());

            // fields.SetField("topmostSubform[0].CMS1500Form[0].Diagnosis1[1]", ds.Tables[1].Rows[0].ItemArray.GetValue(46).ToString());
            //fields.SetField("topmostSubform[0].CMS1500Form[0].Diagnosis1[3]", ds.Tables[1].Rows[0].ItemArray.GetValue(47).ToString());

            if (ds.Tables[1].Rows[0]["sz_date_phone_key_1500"].ToString() == "1")
            {
                fields.SetField("Signature", "SIGNATURE ON FILE");
            }
            else
                fields.SetField("Signature", ds.Tables[1].Rows[0].ItemArray.GetValue(10).ToString());
            fields.SetField("Signature_Date", ds.Tables[1].Rows[0].ItemArray.GetValue(113).ToString());// Sunil Change for bill date

            if (ds.Tables[1].Rows[0]["sz_date_phone_key_1500"].ToString() == "1")
            {
                fields.SetField("Insureds_or_Authorize_Persons_Signature", "SIGNATURE ON FILE");
            }
            else
                fields.SetField("Insureds_or_Authorize_Persons_Signature", ds.Tables[1].Rows[0].ItemArray.GetValue(10).ToString());

            if (CmpId == "CO000000000000000095")
            {
                //fields.SetField("Insured_Birthdate_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(39).ToString());//insured's dob(day)
                //fields.SetField("Insured_Birthdate_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(40).ToString());//insured's dob(month)
                //fields.SetField("Insured_Birthdate_YYYY", ds.Tables[1].Rows[0].ItemArray.GetValue(41).ToString());//insured's dob(year)
                fields.SetField("Insured_Birthdate_DD", "");//insured's dob(day)
                fields.SetField("Insured_Birthdate_MM", "");//insured's dob(month)
                fields.SetField("Insured_Birthdate_YYYY", "");//insured's dob(year)
            }
            else
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(105).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(106).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(107).ToString() != "0")
                {
                    fields.SetField("Insured_Birthdate_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(105).ToString());//insured's dob(day)
                    fields.SetField("Insured_Birthdate_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(106).ToString());//insured's dob(month)
                    fields.SetField("Insured_Birthdate_YYYY", ds.Tables[1].Rows[0].ItemArray.GetValue(107).ToString());//insured's dob(year)
                }
            fields.SetField("Employers_Name_School_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(84).ToString());//Employer's name
            fields.SetField("Federal_Tax_ID_or_SSN", ds.Tables[1].Rows[0].ItemArray.GetValue(74).ToString());//Federal tax Id

            //fields.SetField("topmostSubform[0].CMS1500Form[0].OtherInsuredPolicyNumber[18]", ds.Tables[1].Rows[0].ItemArray.GetValue(77).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(78).ToString());//service location info.
            //fields.SetField("topmostSubform[0].CMS1500Form[0].OtherInsuredPolicyNumber[19]", ds.Tables[1].Rows[0].ItemArray.GetValue(88).ToString() + "," +ds.Tables[1].Rows[0].ItemArray.GetValue(80).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(81).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(82).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(83).ToString());//billing provider info.
            fields.SetField("Service_Facility_Location_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(77).ToString());
            fields.SetField("Service_Facility_Location_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(78).ToString());
            // fields.SetField("Service_Facility_Location_City_State_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(105).ToString());
            fields.SetField("Billing_Provider_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(88).ToString());
            fields.SetField("Billing_Provider_Address", ds.Tables[1].Rows[0].ItemArray.GetValue(80).ToString());
            fields.SetField("Billing_Provider_City_State_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(81).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(82).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(83).ToString() + "," + ds.Tables[1].Rows[0].ItemArray.GetValue(109).ToString());

            if (CmpId == "CO000000000000000095")
            {
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(118).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(119).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(120).ToString() != "0")
                {
                    fields.SetField("Date_Current_Illness_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(119).ToString());
                    fields.SetField("Date_Current_Illness_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(120).ToString());
                    fields.SetField("Date_Current_Illness_YY", ds.Tables[1].Rows[0].ItemArray.GetValue(121).ToString());
                }
            }
            else
            {
                if (ds.Tables[1].Rows[0].ItemArray.GetValue(97).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(98).ToString() != "0" || ds.Tables[1].Rows[0].ItemArray.GetValue(99).ToString() != "0")
                {
                    fields.SetField("Date_Current_Illness_MM", ds.Tables[1].Rows[0].ItemArray.GetValue(97).ToString());
                    fields.SetField("Date_Current_Illness_DD", ds.Tables[1].Rows[0].ItemArray.GetValue(98).ToString());
                    fields.SetField("Date_Current_Illness_YY", ds.Tables[1].Rows[0].ItemArray.GetValue(99).ToString());
                }
            }
            fields.SetField("Service_Facility_Location_City_State_Zip", ds.Tables[1].Rows[0].ItemArray.GetValue(108).ToString());
            fields.SetField("Signature_of_Physician_or_Supplier", ds.Tables[1].Rows[0].ItemArray.GetValue(112).ToString());//Display Reading Doc Name--Kunal
            fields.SetField("Signature_of_Physician_or_Supplier_Date", ds.Tables[1].Rows[0].ItemArray.GetValue(113).ToString());//Display Date--Kunal
            log.Debug("Count of ds_table" + ds_table.Tables[0].Rows.Count.ToString());
            if (ds_table.Tables[0].Rows.Count > 0)
            {
                Bill_Sys_LoginBO _bill_Sys_LoginBO = new Bill_Sys_LoginBO();
                string SZ_SHOW_PROCCODE_TOTAL_1500FORM = "0";
                SZ_SHOW_PROCCODE_TOTAL_1500FORM = _bill_Sys_LoginBO.getDefaultSettings(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SS00035");
                log.Debug("SZ_SHOW_PROCCODE_TOTAL_1500FORM " + SZ_SHOW_PROCCODE_TOTAL_1500FORM);
                if (SZ_SHOW_PROCCODE_TOTAL_1500FORM == "0")
                {
                    for (int i = 0; i < ds_table.Tables[0].Rows.Count; i++)
                    {
                        
                        fields.SetField("Date_From_" + i.ToString() + "_MM", ds_table.Tables[0].Rows[i]["MONTH"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_MM" + " replace by " + ds_table.Tables[0].Rows[i]["MONTH"].ToString());

                        fields.SetField("Date_From_" + i.ToString() + "_DD", ds_table.Tables[0].Rows[i]["DAY"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_DD" + " replace by " + ds_table.Tables[0].Rows[i]["DAY"].ToString());

                        fields.SetField("Date_From_" + i.ToString() + "_YY", ds_table.Tables[0].Rows[i]["YEAR"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_YY" + " replace by " + ds_table.Tables[0].Rows[i]["YEAR"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_MM", ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_MM" + " replace by " + ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_DD", ds_table.Tables[0].Rows[i]["TO_DAY"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_DD" + " replace by " + ds_table.Tables[0].Rows[i]["TO_DAY"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_YY", ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_YY" + " replace by " + ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString());

                        fields.SetField("Place_of_Service_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["PlaceOfService"].ToString());
                        log.Debug("Place_of_Service_" + i.ToString() + " replace by " + ds_table.Tables[0].Rows[i]["PlaceOfService"].ToString());

                        fields.SetField("Procedure_CPT_HCPCS_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
                        log.Debug("Procedure_CPT_HCPCS_" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());

                        fields.SetField("Diagnosis_Pointer_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["DiagnosisPointer"].ToString());
                        log.Debug("Diagnosis_Pointer_" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["DiagnosisPointer"].ToString());

                        fields.SetField("Dollar_Charges_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                        log.Debug("Dollar_Charges_" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString());

                        fields.SetField("Days_or_Units_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["I_UNIT"].ToString());
                        log.Debug("Days_or_Units_" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["I_UNIT"].ToString());

                        log.Debug("CmpId " + CmpId);
                        if (CmpId != "CO000000000000000095")
                        {
                            
                            fields.SetField("Rendering_Providers_NPI_Number_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["SZ_NPI"].ToString());
                            log.Debug("Rendering_Providers_NPI_Number_" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["SZ_NPI"].ToString());
                        }

                        fields.SetField("Procedure_Modifer_" + i.ToString() + "a" + "", ds_table.Tables[0].Rows[i]["SZ_MODIFIER"].ToString());
                        log.Debug("Procedure_Modifer_" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["SZ_MODIFIER"].ToString());
                    }
                    fields.SetField("Total_Charge_Dollars", ds_table.Tables[0].Rows[0]["TOTAL"].ToString());
                    log.Debug("Total_Charge_Dollars" + ds_table.Tables[0].Rows[0]["TOTAL"].ToString());

                    fields.SetField("Amount_Paid_Dollars", ds_table.Tables[0].Rows[0]["paid_amount"].ToString());
                    log.Debug("Amount_Paid_Dollars" + ds_table.Tables[0].Rows[0]["paid_amount"].ToString());

                    fields.SetField("Balance_Due_Dollars", ds_table.Tables[0].Rows[0]["flt_balance"].ToString());
                    log.Debug("Balance_Due_Dollars" + ds_table.Tables[0].Rows[0]["flt_balance"].ToString());
                }
                else
                {
                    double ProcTotal = 0;
                    for (int i = 0; i < ds_table.Tables[0].Rows.Count; i++)
                    {
                        double ProcAmt = 0;
                        fields.SetField("Date_From_" + i.ToString() + "_MM", ds_table.Tables[0].Rows[i]["MONTH"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_MM" + " replace by " + ds_table.Tables[0].Rows[i]["MONTH"].ToString());

                        fields.SetField("Date_From_" + i.ToString() + "_DD", ds_table.Tables[0].Rows[i]["DAY"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_DD" + " replace by " + ds_table.Tables[0].Rows[i]["DAY"].ToString());

                        fields.SetField("Date_From_" + i.ToString() + "_YY", ds_table.Tables[0].Rows[i]["YEAR"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_YY" + " replace by " + ds_table.Tables[0].Rows[i]["YEAR"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_MM", ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_MM" + " replace by " + ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_DD", ds_table.Tables[0].Rows[i]["TO_DAY"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_DD" + " replace by " + ds_table.Tables[0].Rows[i]["TO_DAY"].ToString());

                        fields.SetField("Date_To_" + i.ToString() + "_YY", ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_YY" + " replace by " + ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString());

                        fields.SetField("Place_of_Service_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["PlaceOfService"].ToString());
                        log.Debug("PlaceOfService" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["PlaceOfService"].ToString());

                        fields.SetField("Procedure_CPT_HCPCS_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
                        log.Debug("Procedure_CPT_HCPCS_" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["Procedure_CPT_HCPCS_"].ToString());

                        fields.SetField("Diagnosis_Pointer_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["DiagnosisPointer"].ToString());
                        log.Debug("Diagnosis_Pointer_" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["Diagnosis_Pointer_"].ToString());

                        fields.SetField("Dollar_Charges_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                        log.Debug("Dollar_Charges_" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["Dollar_Charges_"].ToString());

                        fields.SetField("Days_or_Units_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["I_UNIT"].ToString());
                        log.Debug("Days_or_Units_" + i.ToString() + "" + " replace by " + ds_table.Tables[0].Rows[i]["Days_or_Units_"].ToString());

                        log.Debug("CmpId " + CmpId);
                        if (CmpId != "CO000000000000000095")
                        {
                            fields.SetField("Rendering_Providers_NPI_Number_" + i.ToString() + "", ds_table.Tables[0].Rows[i]["SZ_NPI"].ToString());
                            log.Debug("Date_From_" + i.ToString() + "_DD" + " replace by " + ds_table.Tables[0].Rows[i]["DAY"].ToString());
                        }

                        fields.SetField("Procedure_Modifer_" + i.ToString() + "a" + "", ds_table.Tables[0].Rows[i]["SZ_MODIFIER"].ToString());
                        log.Debug("Date_From_" + i.ToString() + "_DD" + " replace by " + ds_table.Tables[0].Rows[i]["DAY"].ToString());

                        log.Debug("FL_AMOUNT " + ds_table.Tables[0].Rows[i]["FL_AMOUNT"]);
                        if (Convert.ToString(ds_table.Tables[0].Rows[i]["FL_AMOUNT"]) != "")
                        {
                            ProcAmt = Convert.ToDouble(ds_table.Tables[0].Rows[i]["FL_AMOUNT"]);
                            ProcTotal = ProcTotal + ProcAmt;
                        }
                    }
                    fields.SetField("Total_Charge_Dollars", ProcTotal.ToString());
                    log.Debug("Total_Charge_Dollars" + ProcTotal.ToString());

                    fields.SetField("Amount_Paid_Dollars", ds_table.Tables[0].Rows[0]["paid_amount"].ToString());
                    log.Debug("Total_Charge_Dollars" + ds_table.Tables[0].Rows[0]["paid_amount"].ToString());

                    fields.SetField("Balance_Due_Dollars", ProcTotal.ToString());
                    log.Debug("Total_Charge_Dollars" + ProcTotal.ToString());
                }


            }
            if (CmpId == "CO000000000000000095")
            {
                fields.SetField("Insurance_Plan_Name", ds.Tables[1].Rows[0].ItemArray.GetValue(20).ToString());
                fields.SetField("Another_Plan_N", "Plan_N");
                fields.SetField("Outside_Lab_N", "Lab_N");
                fields.SetField("NPI_Number_a", ds.Tables[1].Rows[0].ItemArray.GetValue(43).ToString());
                fields.SetField("Name_Referring_Physician_17", ds.Tables[1].Rows[0].ItemArray.GetValue(122).ToString());
            }
            else
            {
                fields.SetField("Name_Referring_Physician_17", ds.Tables[1].Rows[0].ItemArray.GetValue(122).ToString());
            }
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.SetField("NPI_Number", ds.Tables[1].Rows[0].ItemArray.GetValue(43).ToString());
            }
            //Change added on for columbus imaging in GOGYB

            fields.RenameField("Type_of_Coverage_ID", "Type_of_Coverage_ID" + szNewName);
            fields.RenameField("Type_of_Coverage_Medicare", "Type_of_Coverage_Medicare" + szNewName);
            fields.RenameField("Type_of_Coverage_Medicaid", "Type_of_Coverage_Medicaid" + szNewName);
            fields.RenameField("Type_of_Coverage_Tricare", "Type_of_Coverage_Tricare" + szNewName);
            fields.RenameField("Type_of_Coverage_CHAMPVA", "Type_of_Coverage_CHAMPVA" + szNewName);
            fields.RenameField("Type_of_Coverage_Group_Health", "Type_of_Coverage_Group_Health" + szNewName);
            fields.RenameField("Type_of_Coverage_FECA", "Type_of_Coverage_FECA" + szNewName);
            fields.RenameField("SexM    ", "SexM" + szNewName);
            fields.RenameField("SexF", "SexF" + szNewName);
            fields.RenameField("Accept_Assignment", "Accept_Assignment" + szNewName);
            fields.RenameField("Accept_Assignment_No", "Accept_Assignment_No" + szNewName);
            fields.RenameField("Condition_Related_to_Employment_Y", "Condition_Related_to_Employment_Y" + szNewName);
            fields.RenameField("Condition_Related_to_Employment_N", "Condition_Related_to_Employment_N" + szNewName);
            fields.RenameField("Condition_Related_to_Auto_Accident_Y", "Condition_Related_to_Auto_Accident_Y" + szNewName);
            fields.RenameField("Condition_Related_to_Auto_Accident_N", "Condition_Related_to_Auto_Accident_N" + szNewName);
            fields.RenameField("Condition_Related_to_Other_Accident_Y", "Condition_Related_to_Other_Accident_Y" + szNewName);
            fields.RenameField("Condition_Related_to_Other_Accident_N", "Condition_Related_to_Other_Accident_N" + szNewName);
            fields.RenameField("Date_Current_Illness_MM", "Date_Current_Illness_MM" + szNewName);
            fields.RenameField("Date_Current_Illness_DD", "Date_Current_Illness_DD" + szNewName);
            fields.RenameField("Date_Current_Illness_YY", "Date_Current_Illness_YY" + szNewName);
            fields.RenameField("SSN", "SSN" + szNewName);
            fields.RenameField("EIN", "EIN" + szNewName);
            fields.RenameField("Patient_Relationship_to_Insured_Self", "Patient_Relationship_to_Insured_Self" + szNewName);
            fields.RenameField("Insured_Male_Sex", "Insured_Male_Sex" + szNewName);
            fields.RenameField("Insured_Female_Sex", "Insured_Female_Sex" + szNewName);
            fields.RenameField("Patient_Last_First_Name_Middle", "Patient_Last_First_Name_Middle" + szNewName);
            fields.RenameField("Patients_Address", "Patients_Address" + szNewName);
            fields.RenameField("Patients_City", "Patients_City" + szNewName);
            fields.RenameField("Patients_State", "Patients_State" + szNewName);
            fields.RenameField("Patients_Zip_Code", "Patients_Zip_Code" + szNewName);
            fields.RenameField("Patients_Phone_Number", "Patients_Phone_Number" + szNewName);
            fields.RenameField("Insureds_Employers_Name_or_School_Name", "Insureds_Employers_Name_or_School_Name" + szNewName);
            fields.RenameField("First Name Middle Initial Last NameEMPLOYER", "First Name Middle Initial Last NameEMPLOYER" + szNewName);
            fields.RenameField("Carrier_Name", "Carrier_Name" + szNewName);
            fields.RenameField("Carrier_First_Line_Address", "Carrier_First_Line_Address" + szNewName);
            fields.RenameField("Carrier_City_State_Zip_", "Carrier_City_State_Zip_" + szNewName);
            fields.RenameField("Insureds_ID_Number_1a", "Insureds_ID_Number_1a" + szNewName);
            fields.RenameField("Insureds_Name", "Insureds_Name" + szNewName);

            fields.RenameField("Insured_Address", "Insured_Address" + szNewName);
            fields.RenameField("Insured_City", "Insured_City" + szNewName);
            fields.RenameField("Insured_State", "Insured_State" + szNewName);
            fields.RenameField("Patients_Phone_Number_Area_Code", "Patients_Phone_Number_Area_Code" + szNewName);
            fields.RenameField("Insured_Telephone_Number_Area_Code", "Insured_Telephone_Number_Area_Code" + szNewName);
            fields.RenameField("Insured_Zip", "Insured_Zip" + szNewName);
            fields.RenameField("Insured_Telephone_Number", "Insured_Telephone_Number" + szNewName);
            fields.RenameField("Employers_Name_School_Name", "Employers_Name_School_Name" + szNewName);
            fields.RenameField("Insureds_Employers_Name_or_School_Name", "Insureds_Employers_Name_or_School_Name" + szNewName);
            fields.RenameField("Patients_Account_No", "Patients_Account_No" + szNewName);
            fields.RenameField("Other_Insured_Policy_Number_9a", "Other_Insured_Policy_Number_9a" + szNewName);
            fields.RenameField("Insured_Policy_Group_FECA_Number", "Insured_Policy_Group_FECA_Number" + szNewName);
            fields.RenameField("Patient_Birthdate_MM", "Patient_Birthdate_MM" + szNewName);
            fields.RenameField("Patient_Birthdate_DD", "Patient_Birthdate_DD" + szNewName);
            fields.RenameField("Patient_Birthdate_YYYY", "Patient_Birthdate_YYYY" + szNewName);
            fields.RenameField("Diagnosis_Code_1a_21", "Diagnosis_Code_1a_21" + szNewName);
            fields.RenameField("Diagnosis_Code_1b_21", "Diagnosis_Code_1b_21" + szNewName);
            fields.RenameField("Diagnosis_Code_1c_21", "Diagnosis_Code_1c_21" + szNewName);
            fields.RenameField("Diagnosis_Code_3a_21", "Diagnosis_Code_3a_21" + szNewName);
            fields.RenameField("Diagnosis_Code_3b_21", "Diagnosis_Code_3b_21" + szNewName);
            fields.RenameField("Diagnosis_Code_3c_21", "Diagnosis_Code_3c_21" + szNewName);
            fields.RenameField("Diagnosis_Code_2a_21", "Diagnosis_Code_2a_21" + szNewName);
            fields.RenameField("Diagnosis_Code_2b_21", "Diagnosis_Code_2b_21" + szNewName);
            fields.RenameField("Diagnosis_Code_2c_21", "Diagnosis_Code_2c_21" + szNewName);
            fields.RenameField("Diagnosis_Code_4a_21", "Diagnosis_Code_4a_21" + szNewName);
            fields.RenameField("Diagnosis_Code_4b_21", "Diagnosis_Code_4b_21" + szNewName);
            fields.RenameField("Diagnosis_Code_4c_21", "Diagnosis_Code_4c_21" + szNewName);

            fields.RenameField("Signature", "Signature" + szNewName);
            fields.RenameField("Signature_Date", "Signature_Date" + szNewName);
            fields.RenameField("Insureds_or_Authorize_Persons_Signature", "Insureds_or_Authorize_Persons_Signature" + szNewName);

            fields.RenameField("Insured_Birthdate_DD", "Insured_Birthdate_DD" + szNewName);
            fields.RenameField("Insured_Birthdate_MM", "Insured_Birthdate_MM" + szNewName);
            fields.RenameField("Insured_Birthdate_YYYY", "Insured_Birthdate_YYYY" + szNewName);
            fields.RenameField("Employers_Name_School_Name", "Employers_Name_School_Name" + szNewName);
            fields.RenameField("Federal_Tax_ID_or_SSN", "Federal_Tax_ID_or_SSN" + szNewName);
            fields.RenameField("Service_Facility_Location_Name", "Service_Facility_Location_Name" + szNewName);

            fields.RenameField("Service_Facility_Location_Address", "Service_Facility_Location_Address" + szNewName);
            fields.RenameField("Service_Facility_Location_City_State_Zip", "Service_Facility_Location_City_State_Zip" + szNewName);
            fields.RenameField("Billing_Provider_Name", "Billing_Provider_Name" + szNewName);
            fields.RenameField("Billing_Provider_Address", "Billing_Provider_Address" + szNewName);
            fields.RenameField("Billing_Provider_City_State_Zip", "Billing_Provider_City_State_Zip" + szNewName);

            fields.RenameField("Date_From_0_MM", "Date_From_0_MM" + szNewName);
            fields.RenameField("Date_From_1_MM", "Date_From_1_MM" + szNewName);
            fields.RenameField("Date_From_2_MM", "Date_From_2_MM" + szNewName);
            fields.RenameField("Date_From_3_MM", "Date_From_3_MM" + szNewName);
            fields.RenameField("Date_From_4_MM", "Date_From_4_MM" + szNewName);
            fields.RenameField("Date_From_5_MM", "Date_From_5_MM" + szNewName);

            fields.RenameField("Date_From_0_DD", "Date_From_0_DD" + szNewName);
            fields.RenameField("Date_From_1_DD", "Date_From_1_DD" + szNewName);
            fields.RenameField("Date_From_2_DD", "Date_From_2_DD" + szNewName);
            fields.RenameField("Date_From_3_DD", "Date_From_3_DD" + szNewName);
            fields.RenameField("Date_From_4_DD", "Date_From_4_DD" + szNewName);
            fields.RenameField("Date_From_5_DD", "Date_From_5_DD" + szNewName);

            fields.RenameField("Date_From_0_YY", "Date_From_0_YY" + szNewName);
            fields.RenameField("Date_From_1_YY", "Date_From_1_YY" + szNewName);
            fields.RenameField("Date_From_2_YY", "Date_From_2_YY" + szNewName);
            fields.RenameField("Date_From_3_YY", "Date_From_3_YY" + szNewName);
            fields.RenameField("Date_From_4_YY", "Date_From_4_YY" + szNewName);
            fields.RenameField("Date_From_5_YY", "Date_From_5_YY" + szNewName);

            fields.RenameField("Date_To_0_MM", "Date_To_0_MM" + szNewName);
            fields.RenameField("Date_To_1_MM", "Date_To_1_MM" + szNewName);
            fields.RenameField("Date_To_2_MM", "Date_To_2_MM" + szNewName);
            fields.RenameField("Date_To_3_MM", "Date_To_3_MM" + szNewName);
            fields.RenameField("Date_To_4_MM", "Date_To_4_MM" + szNewName);
            fields.RenameField("Date_To_5_MM", "Date_To_5_MM" + szNewName);

            fields.RenameField("Date_To_0_DD", "Date_To_0_DD" + szNewName);
            fields.RenameField("Date_To_1_DD", "Date_To_1_DD" + szNewName);
            fields.RenameField("Date_To_2_DD", "Date_To_2_DD" + szNewName);
            fields.RenameField("Date_To_3_DD", "Date_To_3_DD" + szNewName);
            fields.RenameField("Date_To_4_DD", "Date_To_4_DD" + szNewName);
            fields.RenameField("Date_To_5_DD", "Date_To_5_DD" + szNewName);

            fields.RenameField("Date_To_0_YY", "Date_To_0_YY" + szNewName);
            fields.RenameField("Date_To_1_YY", "Date_To_1_YY" + szNewName);
            fields.RenameField("Date_To_2_YY", "Date_To_2_YY" + szNewName);
            fields.RenameField("Date_To_3_YY", "Date_To_3_YY" + szNewName);
            fields.RenameField("Date_To_4_YY", "Date_To_4_YY" + szNewName);
            fields.RenameField("Date_To_5_YY", "Date_To_5_YY" + szNewName);

            fields.RenameField("Place_of_Service_0", "Place_of_Service_0" + szNewName);
            fields.RenameField("Place_of_Service_1", "Place_of_Service_1" + szNewName);
            fields.RenameField("Place_of_Service_2", "Place_of_Service_2" + szNewName);
            fields.RenameField("Place_of_Service_3", "Place_of_Service_3" + szNewName);
            fields.RenameField("Place_of_Service_4", "Place_of_Service_4" + szNewName);
            fields.RenameField("Place_of_Service_5", "Place_of_Service_5" + szNewName);

            fields.RenameField("Procedure_CPT_HCPCS_0", "Procedure_CPT_HCPCS_0" + szNewName);
            fields.RenameField("Procedure_CPT_HCPCS_1", "Procedure_CPT_HCPCS_1" + szNewName);
            fields.RenameField("Procedure_CPT_HCPCS_2", "Procedure_CPT_HCPCS_2" + szNewName);
            fields.RenameField("Procedure_CPT_HCPCS_3", "Procedure_CPT_HCPCS_3" + szNewName);
            fields.RenameField("Procedure_CPT_HCPCS_4", "Procedure_CPT_HCPCS_4" + szNewName);
            fields.RenameField("Procedure_CPT_HCPCS_5", "Procedure_CPT_HCPCS_5" + szNewName);

            fields.RenameField("Diagnosis_Pointer_0", "Diagnosis_Pointer_0" + szNewName);
            fields.RenameField("Diagnosis_Pointer_1", "Diagnosis_Pointer_1" + szNewName);
            fields.RenameField("Diagnosis_Pointer_2", "Diagnosis_Pointer_2" + szNewName);
            fields.RenameField("Diagnosis_Pointer_3", "Diagnosis_Pointer_3" + szNewName);
            fields.RenameField("Diagnosis_Pointer_4", "Diagnosis_Pointer_4" + szNewName);
            fields.RenameField("Diagnosis_Pointer_5", "Diagnosis_Pointer_5" + szNewName);

            fields.RenameField("Dollar_Charges_0", "Dollar_Charges_0" + szNewName);
            fields.RenameField("Dollar_Charges_1", "Dollar_Charges_1" + szNewName);
            fields.RenameField("Dollar_Charges_2", "Dollar_Charges_2" + szNewName);
            fields.RenameField("Dollar_Charges_3", "Dollar_Charges_3" + szNewName);
            fields.RenameField("Dollar_Charges_4", "Dollar_Charges_4" + szNewName);
            fields.RenameField("Dollar_Charges_5", "Dollar_Charges_5" + szNewName);

            fields.RenameField("Days_or_Units_0", "Days_or_Units_0" + szNewName);
            fields.RenameField("Days_or_Units_1", "Days_or_Units_1" + szNewName);
            fields.RenameField("Days_or_Units_2", "Days_or_Units_2" + szNewName);
            fields.RenameField("Days_or_Units_3", "Days_or_Units_3" + szNewName);
            fields.RenameField("Days_or_Units_4", "Days_or_Units_4" + szNewName);
            fields.RenameField("Days_or_Units_5", "Days_or_Units_5" + szNewName);

            fields.RenameField("Rendering_Providers_NPI_Number_0", "Rendering_Providers_NPI_Number_0" + szNewName);
            fields.RenameField("Rendering_Providers_NPI_Number_1", "Rendering_Providers_NPI_Number_1" + szNewName);
            fields.RenameField("Rendering_Providers_NPI_Number_2", "Rendering_Providers_NPI_Number_2" + szNewName);
            fields.RenameField("Rendering_Providers_NPI_Number_3", "Rendering_Providers_NPI_Number_3" + szNewName);
            fields.RenameField("Rendering_Providers_NPI_Number_4", "Rendering_Providers_NPI_Number_4" + szNewName);
            fields.RenameField("Rendering_Providers_NPI_Number_5", "Rendering_Providers_NPI_Number_5" + szNewName);
            fields.RenameField("Total_Charge_Dollars", "Total_Charge_Dollars" + szNewName);
            fields.RenameField("Amount_Paid_Dollars", "Amount_Paid_Dollars" + szNewName);
            fields.RenameField("Balance_Due_Dollars", "Balance_Due_Dollars" + szNewName);
            fields.RenameField("Signature_of_Physician_or_Supplier", "Signature_of_Physician_or_Supplier" + szNewName);
            fields.RenameField("Signature_of_Physician_or_Supplier_Date", "Signature_of_Physician_or_Supplier_Date" + szNewName);
            //change added on 03/04/2015 for adding the reffering doctor for LHR
            fields.RenameField("Name_Referring_Physician_17", "Name_Referring_Physician_17" + szNewName);
            //change added on 03/04/2015 for adding the reffering doctor for LHR

            fields.RenameField("Insurance_Plan_Name", "Insurance_Plan_Name" + szNewName);
            fields.RenameField("Another_Plan_N", "Another_Plan_N" + szNewName);
            fields.RenameField("Outside_Lab_N", "Outside_Lab_N" + szNewName);
            fields.RenameField("NPI_Number_a", "NPI_Number_a" + szNewName);
            //Change added on for columbus imaging in GOGYB
            if (CmpId == "CO000000000000000129")
            {
                fields.RenameField("NPI_Number", "NPI_Number" + szNewName);
            }
            //Change added on for columbus imaging in GOGYB

            
            stamper.Close();
        }

        #endregion
        string openPath = CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
        returnPath = openPath;
        return returnPath;

    }
    public void PdfPrint(string pdfFileName, DataSet dsPdfValue, DataSet dsProcValue, string szCompanyID, string szCaseID, string newPdfFilename)
    {
        string pdfTemplate = pdfFileName;
        string code_length = "";
        string OutputFilePath = getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), szCompanyID, szCaseID) + newPdfFilename;

        log.Debug("OutputFilePath " + OutputFilePath);
        // DataSet dsPdfValue = getPDFData(billNumber, compId);
        // DataSet dsProcValue = getServiceTableData(billNumber, compId);

        PdfReader pdfReader = new PdfReader(pdfTemplate);
        //PdfReader.unethicalreading = true; 
        PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(OutputFilePath, FileMode.Create));
        AcroFields pdfFormFields = pdfStamper.AcroFields;
        if (dsPdfValue != null)
        {
            if (dsPdfValue.Tables[0] != null)
            {
                //pdfFormFields.SetField("Name", "Insurance company name");
                pdfFormFields.SetField("Name", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
                pdfFormFields.SetField("2", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString());
                //pdfFormFields.SetField("3", "Insurance company address");
                pdfFormFields.SetField("3", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS2"].ToString());
                //pdfFormFields.SetField("4", "Insurance company address");
                pdfFormFields.SetField("4", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString());
                //pdfFormFields.SetField("8", "Yes");
                //pdfFormFields.SetField("9", "Yes");
                //pdfFormFields.SetField("10", "Yes");
                //pdfFormFields.SetField("11", "Yes");
                if (dsPdfValue.Tables[0].Rows[0]["SZ_CASE_TYPE_ABBRIVATION"].ToString() == "PVT")
                {
                    pdfFormFields.SetField("12", "1");
                }
                else
                {
                    pdfFormFields.SetField("14", "1");
                }
                //pdfFormFields.SetField("13", "Yes");

                //Amod
                //pdfFormFields.SetField("14", "1");
                //pdfFormFields.SetField("15", "Insured ID No");

                // Amod
                if (dsPdfValue.Tables[0].Rows[0]["SZ_CLAIM_NO"] != "null")
                {
                    pdfFormFields.SetField("15", dsPdfValue.Tables[0].Rows[0]["SZ_CLAIM_NO"].ToString());
                }

                ////pdfFormFields.SetField("16", "Patien's Name LFM");
                ////pdfFormFields.SetField("17", "Patient's DOB  MM");
                ////pdfFormFields.SetField("18", "Patient's DOB  DD");
                ////pdfFormFields.SetField("19", "Patient's DOB  YYYY");
                ////pdfFormFields.SetField("20", "Yes");
                ////pdfFormFields.SetField("21", "Yes");
                ////pdfFormFields.SetField("22", "Insured's Name LFM");
                ////pdfFormFields.SetField("23", "Patient's Address No,Street");
                ////pdfFormFields.SetField("24", "Patient's Address city");
                ////pdfFormFields.SetField("25", "Patient's Address state");
                ////pdfFormFields.SetField("26", "Patient's Address zip");
                ////pdfFormFields.SetField("27", "Patient's Address tele 1");
                ////pdfFormFields.SetField("28", "Patient's Address tele 2");
                pdfFormFields.SetField("16", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString());

                // Amod - if the month is 1 digit then prefix it with 0
                if (dsPdfValue.Tables[0].Rows[0]["DOB_MM"] != null)
                {
                    if (!dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString().Equals(""))
                    {
                        try
                        {
                            int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                            if (i <= 9)
                            {
                                pdfFormFields.SetField("17", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                            }
                            else
                            {
                                pdfFormFields.SetField("17", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                            }
                        }
                        catch (Exception i)
                        {
                            pdfFormFields.SetField("17", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                        }
                    }
                }

                //pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                if (dsPdfValue.Tables[0].Rows[0]["DOB_DD"] != null)
                {
                    if (!dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString().Equals(""))
                    {
                        try
                        {
                            int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                            if (i <= 9)
                            {
                                pdfFormFields.SetField("18", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                            }
                            else
                            {
                                pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                            }
                        }
                        catch (Exception i)
                        {
                            pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                        }
                    }
                }
                pdfFormFields.SetField("19", dsPdfValue.Tables[0].Rows[0]["DOB_YY"].ToString());
                if (dsPdfValue.Tables[0].Rows[0]["MALE"].ToString() == "1")
                {
                    pdfFormFields.SetField("20", "No");
                }
                if (dsPdfValue.Tables[0].Rows[0]["FEMALE"].ToString() == "1")
                {
                    pdfFormFields.SetField("21", "No");
                }

                pdfFormFields.SetField("22", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString());
                pdfFormFields.SetField("23", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString());
                pdfFormFields.SetField("24", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
                pdfFormFields.SetField("25", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
                pdfFormFields.SetField("26", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());
                pdfFormFields.SetField("37", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_1_3"].ToString());
                pdfFormFields.SetField("28", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_4_E"].ToString());

                if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "1")
                {
                    pdfFormFields.SetField("29", "1");
                }
                else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "2")
                {
                    pdfFormFields.SetField("30", "1");
                }
                else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "3")
                {
                    pdfFormFields.SetField("31", "1");
                }
                else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "4")
                {
                    pdfFormFields.SetField("32", "1");
                }
                //pdfFormFields.SetField("29", "Yes");
                //pdfFormFields.SetField("30", "Yes");
                //pdfFormFields.SetField("31", "Yes");
                //pdfFormFields.SetField("32", "Yes");

                //pdfFormFields.SetField("33", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ADDRESS"].ToString());
                //pdfFormFields.SetField("34", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_CITY"].ToString());
                //pdfFormFields.SetField("35", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_STATE_ID"].ToString());
                //pdfFormFields.SetField("36", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ZIP"].ToString());
                //pdfFormFields.SetField("37", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE_1_3"].ToString());
                //pdfFormFields.SetField("38", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE_4_E"].ToString());

                pdfFormFields.SetField("33", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString());
                pdfFormFields.SetField("34", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
                pdfFormFields.SetField("35", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
                pdfFormFields.SetField("36", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());
                pdfFormFields.SetField("27", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_1_3"].ToString());
                //pdfFormFields.SetField("38", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_1_3"].ToString() + " " + dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_4_E"].ToString());
                pdfFormFields.SetField("38", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_4_E"].ToString());

                if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "1")
                {
                    pdfFormFields.SetField("39", "1");
                }
                else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "2")
                {
                    pdfFormFields.SetField("40", "1");
                }
                else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "3")
                {
                    pdfFormFields.SetField("41", "1");
                }

                //pdfFormFields.SetField("39", "Yes");
                //pdfFormFields.SetField("40", "Yes");
                //pdfFormFields.SetField("41", "Yes");
                //pdfFormFields.SetField("42", "Yes");//?????????/
                //pdfFormFields.SetField("43", "Yes");
                //pdfFormFields.SetField("44", "Yes");
                //pdfFormFields.SetField("45", "OTHER INSUREDS NAME LFM");
                ////pdfFormFields.SetField("46", "OTHER INSUREDS policy/grp no");
                //pdfFormFields.SetField("46", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_NO"].ToString());
                //pdfFormFields.SetField("47", "OTHER INSUREDS DOB MM");
                //pdfFormFields.SetField("48", "OTHER INSUREDS DOB DD");
                //pdfFormFields.SetField("49", "OTHER INSUREDS DOB YYYY");
                //pdfFormFields.SetField("50", "Yes");
                //pdfFormFields.SetField("51", "Yes");
                //pdfFormFields.SetField("52", "EMPLOYERS NAME OR SCHOOL NAME");
                //pdfFormFields.SetField("53", "INSURANCE PLAN NAME OR PROGRAM NAME");
                //pdfFormFields.SetField("54", "Yes");
                //pdfFormFields.SetField("55", "Yes");
                //pdfFormFields.SetField("56", "Yes");
                //pdfFormFields.SetField("57", "Yes");

                //pdfFormFields.SetField("59", "Yes");



                if (dsPdfValue.Tables[0].Rows[0]["SZ_CASE_TYPE_ABBRIVATION"].ToString() == "WC")
                {
                    pdfFormFields.SetField("54", "1");
                    pdfFormFields.SetField("57", "1");
                    pdfFormFields.SetField("60", "1");
                }
                else if (dsPdfValue.Tables[0].Rows[0]["SZ_CASE_TYPE_ABBRIVATION"].ToString() == "NF")
                {
                    pdfFormFields.SetField("56", "1");
                    pdfFormFields.SetField("58", dsPdfValue.Tables[0].Rows[0]["SZ_ACCIDENT_STATE_ID"].ToString());
                    pdfFormFields.SetField("55", "1");
                    pdfFormFields.SetField("60", "1");
                }
                else if (dsPdfValue.Tables[0].Rows[0]["SZ_CASE_TYPE_ABBRIVATION"].ToString() == "PVT")
                {
                    pdfFormFields.SetField("55", "1");
                    pdfFormFields.SetField("57", "1");
                    pdfFormFields.SetField("60", "1");

                }
                else
                {
                    pdfFormFields.SetField("59", "1");
                    pdfFormFields.SetField("55", "1");

                }



                //pdfFormFields.SetField("61", "RESERVED FOR LOCAL USE");
                pdfFormFields.SetField("62", dsPdfValue.Tables[0].Rows[0]["SZ_WCB_NO"].ToString());
                //pdfFormFields.SetField("63", "INSUREDS DOB MM");
                //pdfFormFields.SetField("64", "INSUREDS DOB DD");
                //pdfFormFields.SetField("65", "INSUREDS DOB YYYY");
                if (dsPdfValue.Tables[0].Rows[0]["DOB_MM"] != null)
                {
                    if (!dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString().Equals(""))
                    {
                        try
                        {
                            int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                            if (i <= 9)
                            {
                                pdfFormFields.SetField("63", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                            }
                            else
                            {
                                pdfFormFields.SetField("63", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                            }
                        }
                        catch (Exception i)
                        {
                            pdfFormFields.SetField("63", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
                        }
                    }
                }

                //pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                if (dsPdfValue.Tables[0].Rows[0]["DOB_DD"] != null)
                {
                    if (!dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString().Equals(""))
                    {
                        try
                        {
                            int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                            if (i <= 9)
                            {
                                pdfFormFields.SetField("64", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                            }
                            else
                            {
                                pdfFormFields.SetField("64", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                            }
                        }
                        catch (Exception i)
                        {
                            pdfFormFields.SetField("64", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
                        }
                    }
                }
                pdfFormFields.SetField("65", dsPdfValue.Tables[0].Rows[0]["DOB_YY"].ToString());
                if (dsPdfValue.Tables[0].Rows[0]["MALE"].ToString() == "1")
                {
                    pdfFormFields.SetField("66", "Yes");
                }
                else if (dsPdfValue.Tables[0].Rows[0]["FEMALE"].ToString() == "1")
                {
                    pdfFormFields.SetField("67", "No");
                }
                //pdfFormFields.SetField("66", "Yes");
                //pdfFormFields.SetField("67", "Yes");
                //pdfFormFields.SetField("68", "EMPLOYERS NAME OR SCHOOL NAME");
                ////pdfFormFields.SetField("69", "INSURANCE PLAN NAME OR PROGRAM NAME");
                //pdfFormFields.SetField("Name", );
                //Amod
                pdfFormFields.SetField("69", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
                //pdfFormFields.SetField("70", "Yes");
                pdfFormFields.SetField("71", "No");

                //Amod
                pdfFormFields.SetField("72", "SIGNATURE ON FILE");

                //Amod
                pdfFormFields.SetField("73", dsPdfValue.Tables[0].Rows[0]["SZ_TODAY"].ToString());

                //Amod
                pdfFormFields.SetField("74", "SIGNATURE ON FILE");

                //Amod
                pdfFormFields.SetField("75", dsPdfValue.Tables[0].Rows[0]["SZ_MM_ACCIDENT"].ToString());

                //Amod
                pdfFormFields.SetField("76", dsPdfValue.Tables[0].Rows[0]["SZ_DD_ACCIDENT"].ToString());

                //Amod
                pdfFormFields.SetField("77", dsPdfValue.Tables[0].Rows[0]["SZ_YY_ACCIDENT"].ToString());

                //Aarti 
                pdfFormFields.SetField("730", "431");

                //pdfFormFields.SetField("78", "first treat Date MM");
                //pdfFormFields.SetField("79", "first treat Date DD");
                //pdfFormFields.SetField("80", "first treat Date YYYY");
                //pdfFormFields.SetField("81", "unable work from Date MM");
                //pdfFormFields.SetField("82", "unable work from Date DD");
                //pdfFormFields.SetField("83", "unable work from Date YYYY");
                //pdfFormFields.SetField("84", "unable work to Date MM");
                //pdfFormFields.SetField("85", "unable work to Date DD");
                //pdfFormFields.SetField("86", "unable work to Date YYYY");
                //pdfFormFields.SetField("87", "NAME OF REFERRING PROVIDER OR OTHER SOURCE");
                pdfFormFields.SetField("87", dsPdfValue.Tables[0].Rows[0]["sz_reff_doctor"].ToString());
                pdfFormFields.SetField("90", dsPdfValue.Tables[0].Rows[0]["sz_reff_doctor_npi"].ToString());

                if (dsPdfValue.Tables[0].Rows[0]["IS_OUT_LAB_YES"].ToString() == "1")
                {
                    pdfFormFields.SetField("98", "No");
                }
                else if (dsPdfValue.Tables[0].Rows[0]["IS_OUT_LAB_NO"].ToString() == "1")
                {
                    pdfFormFields.SetField("99", "No");
                }

                string diaPointer = "";
                if (dsPdfValue.Tables[0].Rows[0]["sz_abbrivation_id"].ToString() == "WC000000000000000003")
                {
                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != null)
                    {
                        pdfFormFields.SetField("102", dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString());
                        diaPointer = "A";
                    }
                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != null)
                    {
                        pdfFormFields.SetField("104", dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString());
                        diaPointer += "B";
                    }
                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != null)
                    {
                        pdfFormFields.SetField("108", dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString());
                        diaPointer += "C";
                    }
                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != null)
                    {
                        pdfFormFields.SetField("110", dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString());
                        diaPointer += "D";
                    }
                }
                else
                {
                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != null)
                    {
                        string[] diag1 = dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString().Split('.');
                        if (diag1.Length > 1)
                        {
                            pdfFormFields.SetField("102", diag1[0].ToString());
                            pdfFormFields.SetField("104", diag1[1].ToString());
                        }
                        else
                        {
                            pdfFormFields.SetField("102", diag1[0].ToString());
                        }
                        diaPointer = "A";
                    }
                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != null)
                    {
                        string[] diag2 = dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString().Split('.');
                        if (diag2.Length > 1)
                        {
                            pdfFormFields.SetField("105", diag2[0].ToString());
                            pdfFormFields.SetField("107", diag2[1].ToString());
                        }
                        else
                        {
                            pdfFormFields.SetField("105", diag2[0].ToString());
                        }

                        diaPointer += "B";
                    }
                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != null)
                    {
                        string[] diag3 = dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString().Split('.');
                        if (diag3.Length > 1)
                        {
                            pdfFormFields.SetField("108", diag3[0].ToString());
                            pdfFormFields.SetField("110", diag3[1].ToString());
                        }
                        else
                        {
                            pdfFormFields.SetField("108", diag3[0].ToString());
                        }
                        diaPointer += "C";
                    }
                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != null)
                    {
                        string[] diag4 = dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString().Split('.');
                        if (diag4.Length > 1)
                        {
                            pdfFormFields.SetField("111", diag4[0].ToString());
                            pdfFormFields.SetField("113", diag4[1].ToString());
                        }
                        else
                        {
                            pdfFormFields.SetField("111", diag4[0].ToString());
                        }
                        diaPointer += "D";
                    }
                }

                ////pdfFormFields.SetField("102", "diagnosis pre");
                ////pdfFormFields.SetField("104", "dia pro");

                ////pdfFormFields.SetField("105", "diagnosis pre");
                ////pdfFormFields.SetField("107", "dia pro");

                ////pdfFormFields.SetField("108", "diagnosis pre");
                ////pdfFormFields.SetField("110", "dia pro");

                ////pdfFormFields.SetField("111", "diagnosis pre");
                ////pdfFormFields.SetField("113", "dia pro");
                string firstModifier = "";
                string multiModifier = "";
                string codeDesc = "";

                double totcharge = 0.0, paidAmt = 0.0, balAmt = 0.0;
                if (dsProcValue != null)
                {
                    if (dsProcValue.Tables.Count > 0 && dsProcValue.Tables[0].Rows.Count > 0)
                    {
                        //////////////////////1/////////////////////////////
                        for (int i = 0; i < dsProcValue.Tables[0].Rows.Count; i++)
                        {
                            firstModifier = "";
                            multiModifier = "";
                            if (dsPdfValue.Tables[0].Rows[0]["multiple_modifier"].ToString() == "1")
                            {
                                if (dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString() != "" && dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString() != null)
                                {
                                    string[] modifiers = dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString().Split(',');
                                    if (modifiers.Length > 1)
                                    {
                                        firstModifier = modifiers[0].ToString();
                                        for (int j = 1; j < modifiers.Length; j++)
                                        {
                                            multiModifier += modifiers[j].ToString() + ",";
                                        }
                                        multiModifier = multiModifier.Substring(0, multiModifier.Length - 1);
                                    }
                                    else
                                    {
                                        firstModifier = modifiers[0].ToString();
                                    }
                                }
                            }
                            else
                            {
                                firstModifier = dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString();
                                multiModifier = "";
                            }
                            if (i == 0)
                            {
                                //-add Description dt 04-12-2015
                                //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString();
                                pdfFormFields.SetField("117", codeDesc);

                                pdfFormFields.SetField("120", dsProcValue.Tables[0].Rows[i]["DOS_MM"].ToString());
                                pdfFormFields.SetField("121", dsProcValue.Tables[0].Rows[i]["DOS_DD"].ToString());
                                pdfFormFields.SetField("122", dsProcValue.Tables[0].Rows[i]["DOS_YY"].ToString());
                                pdfFormFields.SetField("123", dsProcValue.Tables[0].Rows[i]["DOS_MM"].ToString());
                                pdfFormFields.SetField("124", dsProcValue.Tables[0].Rows[i]["DOS_DD"].ToString());
                                pdfFormFields.SetField("125", dsProcValue.Tables[0].Rows[i]["DOS_YY"].ToString());
                                pdfFormFields.SetField("126", dsProcValue.Tables[0].Rows[i]["PALCE OF SERVICE"].ToString());
                                //pdfFormFields.SetField("128", dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
                                //string code_length = "";
                                code_length = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString();
                                if (code_length.Length <= 7)
                                {
                                    pdfFormFields.SetField("128", dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
                                }
                                else
                                {
                                    pdfFormFields.SetField("128", "");
                                }
                                pdfFormFields.SetField("129", firstModifier);
                                pdfFormFields.SetField("130", multiModifier);
                                pdfFormFields.SetField("133", diaPointer);
                                pdfFormFields.SetField("c1", dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                                totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                pdfFormFields.SetField("138", dsProcValue.Tables[0].Rows[i]["SZ_NPI"].ToString());
                                pdfFormFields.SetField("136", dsProcValue.Tables[0].Rows[i]["I_UNIT"].ToString());
                            }
                            //////////////////////2/////////////////////////////
                            if (i == 1)
                            {
                                //-add Description dt 04-12-2015
                                //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                                pdfFormFields.SetField("139", codeDesc);

                                pdfFormFields.SetField("142", dsProcValue.Tables[0].Rows[1]["DOS_MM"].ToString());
                                pdfFormFields.SetField("143", dsProcValue.Tables[0].Rows[1]["DOS_DD"].ToString());
                                pdfFormFields.SetField("144", dsProcValue.Tables[0].Rows[1]["DOS_YY"].ToString());
                                pdfFormFields.SetField("145", dsProcValue.Tables[0].Rows[1]["DOS_MM"].ToString());
                                pdfFormFields.SetField("146", dsProcValue.Tables[0].Rows[1]["DOS_DD"].ToString());
                                pdfFormFields.SetField("147", dsProcValue.Tables[0].Rows[1]["DOS_YY"].ToString());
                                pdfFormFields.SetField("148", dsProcValue.Tables[0].Rows[1]["PALCE OF SERVICE"].ToString());
                                //pdfFormFields.SetField("150", dsProcValue.Tables[0].Rows[1]["SZ_PROCEDURE_CODE"].ToString());
                                code_length = dsProcValue.Tables[0].Rows[1]["SZ_PROCEDURE_CODE"].ToString();
                                if (code_length.Length <= 7)
                                {
                                    pdfFormFields.SetField("150", dsProcValue.Tables[0].Rows[1]["SZ_PROCEDURE_CODE"].ToString());
                                }
                                else
                                {
                                    pdfFormFields.SetField("150", "");
                                }
                                pdfFormFields.SetField("151", firstModifier);
                                pdfFormFields.SetField("152", multiModifier);
                                pdfFormFields.SetField("155", diaPointer);
                                pdfFormFields.SetField("c2", dsProcValue.Tables[0].Rows[1]["FL_AMOUNT"].ToString());
                                totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                pdfFormFields.SetField("160", dsProcValue.Tables[0].Rows[1]["SZ_NPI"].ToString());
                                pdfFormFields.SetField("158", dsProcValue.Tables[0].Rows[1]["I_UNIT"].ToString());

                            }
                            //////////////////////3/////////////////////////////
                            if (i == 2)
                            {
                                //-add Description dt 04-12-2015
                                //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                                pdfFormFields.SetField("161", codeDesc);

                                pdfFormFields.SetField("164", dsProcValue.Tables[0].Rows[2]["DOS_MM"].ToString());
                                pdfFormFields.SetField("165", dsProcValue.Tables[0].Rows[2]["DOS_DD"].ToString());
                                pdfFormFields.SetField("166", dsProcValue.Tables[0].Rows[2]["DOS_YY"].ToString());
                                pdfFormFields.SetField("167", dsProcValue.Tables[0].Rows[2]["DOS_MM"].ToString());
                                pdfFormFields.SetField("168", dsProcValue.Tables[0].Rows[2]["DOS_DD"].ToString());
                                pdfFormFields.SetField("169", dsProcValue.Tables[0].Rows[2]["DOS_YY"].ToString());
                                pdfFormFields.SetField("170", dsProcValue.Tables[0].Rows[2]["PALCE OF SERVICE"].ToString());
                                //pdfFormFields.SetField("172", dsProcValue.Tables[0].Rows[2]["SZ_PROCEDURE_CODE"].ToString());
                                code_length = dsProcValue.Tables[0].Rows[2]["SZ_PROCEDURE_CODE"].ToString();
                                if (code_length.Length <= 7)
                                {
                                    pdfFormFields.SetField("172", dsProcValue.Tables[0].Rows[2]["SZ_PROCEDURE_CODE"].ToString());
                                }
                                else
                                {
                                    pdfFormFields.SetField("172", "");
                                }
                                pdfFormFields.SetField("173", firstModifier);
                                pdfFormFields.SetField("174", multiModifier);
                                pdfFormFields.SetField("177", diaPointer);
                                pdfFormFields.SetField("c3", dsProcValue.Tables[0].Rows[2]["FL_AMOUNT"].ToString());
                                totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                pdfFormFields.SetField("182", dsProcValue.Tables[0].Rows[2]["SZ_NPI"].ToString());
                                pdfFormFields.SetField("180", dsProcValue.Tables[0].Rows[2]["I_UNIT"].ToString());
                            }
                            //////////////////////4////////////////////////////
                            if (i == 3)
                            {
                                //-add Description dt 04-12-2015
                                //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                                pdfFormFields.SetField("183", codeDesc);

                                pdfFormFields.SetField("186", dsProcValue.Tables[0].Rows[3]["DOS_MM"].ToString());
                                pdfFormFields.SetField("187", dsProcValue.Tables[0].Rows[3]["DOS_DD"].ToString());
                                pdfFormFields.SetField("188", dsProcValue.Tables[0].Rows[3]["DOS_YY"].ToString());
                                pdfFormFields.SetField("189", dsProcValue.Tables[0].Rows[3]["DOS_MM"].ToString());
                                pdfFormFields.SetField("190", dsProcValue.Tables[0].Rows[3]["DOS_DD"].ToString());
                                pdfFormFields.SetField("191", dsProcValue.Tables[0].Rows[3]["DOS_YY"].ToString());
                                pdfFormFields.SetField("192", dsProcValue.Tables[0].Rows[3]["PALCE OF SERVICE"].ToString());
                                //pdfFormFields.SetField("194", dsProcValue.Tables[0].Rows[3]["SZ_PROCEDURE_CODE"].ToString());
                                code_length = dsProcValue.Tables[0].Rows[3]["SZ_PROCEDURE_CODE"].ToString();
                                if (code_length.Length <= 7)
                                {
                                    pdfFormFields.SetField("194", dsProcValue.Tables[0].Rows[3]["SZ_PROCEDURE_CODE"].ToString());
                                }
                                else
                                {
                                    pdfFormFields.SetField("194", "");
                                }
                                pdfFormFields.SetField("195", firstModifier);
                                pdfFormFields.SetField("196", multiModifier);
                                pdfFormFields.SetField("199", diaPointer);
                                pdfFormFields.SetField("c4", dsProcValue.Tables[0].Rows[3]["FL_AMOUNT"].ToString());
                                totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                pdfFormFields.SetField("204", dsProcValue.Tables[0].Rows[3]["SZ_NPI"].ToString());
                                pdfFormFields.SetField("202", dsProcValue.Tables[0].Rows[3]["I_UNIT"].ToString());
                            }
                            if (i == 4)
                            {
                                //////////////////////5/////////////////////////////
                                //-add Description dt 04-12-2015
                                //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                                pdfFormFields.SetField("205", codeDesc);

                                pdfFormFields.SetField("208", dsProcValue.Tables[0].Rows[4]["DOS_MM"].ToString());
                                pdfFormFields.SetField("209", dsProcValue.Tables[0].Rows[4]["DOS_DD"].ToString());
                                pdfFormFields.SetField("210", dsProcValue.Tables[0].Rows[4]["DOS_YY"].ToString());
                                pdfFormFields.SetField("211", dsProcValue.Tables[0].Rows[4]["DOS_MM"].ToString());
                                pdfFormFields.SetField("212", dsProcValue.Tables[0].Rows[4]["DOS_DD"].ToString());
                                pdfFormFields.SetField("213", dsProcValue.Tables[0].Rows[4]["DOS_YY"].ToString());
                                pdfFormFields.SetField("214", dsProcValue.Tables[0].Rows[4]["PALCE OF SERVICE"].ToString());
                                //pdfFormFields.SetField("216", dsProcValue.Tables[0].Rows[4]["SZ_PROCEDURE_CODE"].ToString());
                                code_length = dsProcValue.Tables[0].Rows[4]["SZ_PROCEDURE_CODE"].ToString();
                                if (code_length.Length <= 7)
                                {
                                    pdfFormFields.SetField("216", dsProcValue.Tables[0].Rows[4]["SZ_PROCEDURE_CODE"].ToString());
                                }
                                else
                                {
                                    pdfFormFields.SetField("216", "");
                                }
                                pdfFormFields.SetField("217", firstModifier);
                                pdfFormFields.SetField("218", multiModifier);
                                pdfFormFields.SetField("221", diaPointer);
                                pdfFormFields.SetField("c5", dsProcValue.Tables[0].Rows[4]["FL_AMOUNT"].ToString());
                                totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                pdfFormFields.SetField("226", dsProcValue.Tables[0].Rows[4]["SZ_NPI"].ToString());
                                pdfFormFields.SetField("224", dsProcValue.Tables[0].Rows[4]["I_UNIT"].ToString());
                            }
                            if (i == 5)
                            {
                                //////////////////////6/////////////////////////////
                                //-add Description dt 04-12-2015
                                //codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                                pdfFormFields.SetField("227", codeDesc);

                                pdfFormFields.SetField("230", dsProcValue.Tables[0].Rows[5]["DOS_MM"].ToString());
                                pdfFormFields.SetField("231", dsProcValue.Tables[0].Rows[5]["DOS_DD"].ToString());
                                pdfFormFields.SetField("232", dsProcValue.Tables[0].Rows[5]["DOS_YY"].ToString());
                                pdfFormFields.SetField("233", dsProcValue.Tables[0].Rows[5]["DOS_MM"].ToString());
                                pdfFormFields.SetField("234", dsProcValue.Tables[0].Rows[5]["DOS_DD"].ToString());
                                pdfFormFields.SetField("235", dsProcValue.Tables[0].Rows[5]["DOS_YY"].ToString());
                                pdfFormFields.SetField("236", dsProcValue.Tables[0].Rows[5]["PALCE OF SERVICE"].ToString());
                                //pdfFormFields.SetField("238", dsProcValue.Tables[0].Rows[5]["SZ_PROCEDURE_CODE"].ToString());
                                code_length = dsProcValue.Tables[0].Rows[5]["SZ_PROCEDURE_CODE"].ToString();
                                if (code_length.Length <= 7)
                                {
                                    pdfFormFields.SetField("238", dsProcValue.Tables[0].Rows[5]["SZ_PROCEDURE_CODE"].ToString());
                                }
                                else
                                {
                                    pdfFormFields.SetField("238", "");
                                }
                                pdfFormFields.SetField("239", firstModifier);
                                pdfFormFields.SetField("240", multiModifier);
                                pdfFormFields.SetField("243", diaPointer);
                                pdfFormFields.SetField("c6", dsProcValue.Tables[0].Rows[5]["FL_AMOUNT"].ToString());
                                totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
                                pdfFormFields.SetField("248", dsProcValue.Tables[0].Rows[5]["SZ_NPI"].ToString());
                                pdfFormFields.SetField("246", dsProcValue.Tables[0].Rows[5]["I_UNIT"].ToString());
                            }
                        }
                    }
                }
                ////pdfFormFields.SetField("249", "provider TAX I.D.");
                ////pdfFormFields.SetField("250", "Yes");//
                ////pdfFormFields.SetField("251", "Yes");
                pdfFormFields.SetField("249", dsPdfValue.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
                //pdfFormFields.SetField("250", "Yes");//
                pdfFormFields.SetField("251", "No");

                //if (dsPdfValue.Tables[0].Rows[0]["TAX_TYPE"].ToString().ToLower() == "false")
                //{
                //    pdfFormFields.SetField("250", "No");
                //}
                //else if (dsPdfValue.Tables[0].Rows[0]["TAX_TYPE"].ToString().ToLower() == "true")
                //{
                //    pdfFormFields.SetField("251", "No");
                //}

                ////pdfFormFields.SetField("252", "Pat acc no");
                //pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_BILL_NUMBER"].ToString());
                pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_CASE_NO"].ToString());
                ////pdfFormFields.SetField("253", "Yes");
                if (dsPdfValue.Tables[0].Rows[0]["ACCEPT_ASSIGNMENT"].ToString() == "1")
                {
                    pdfFormFields.SetField("253", "No");
                }
                ////pdfFormFields.SetField("254", "Yes");//

                ////pdfFormFields.SetField("261", "Doctor Name");
                balAmt = totcharge;
                pdfFormFields.SetField("C255", Convert.ToString(totcharge));
                pdfFormFields.SetField("C257", paidAmt.ToString());

                //Amod commented
                //pdfFormFields.SetField("C259", balAmt.ToString());
                //pdfFormFields.SetField("C259", Convert.ToString(totcharge));

                pdfFormFields.SetField("261", dsPdfValue.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
                pdfFormFields.SetField("31SIGN", dsPdfValue.Tables[0].Rows[0]["SIGNATURE_ON_FILE"].ToString());
                pdfFormFields.SetField("262", dsPdfValue.Tables[0].Rows[0]["DATE"].ToString());
                pdfFormFields.SetField("263", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString());
                ////pdfFormFields.SetField("264", "Provider address");
                ////pdfFormFields.SetField("265", "Provider city,state,zip");
                pdfFormFields.SetField("264", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
                pdfFormFields.SetField("265", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS2"].ToString());

                ////pdfFormFields.SetField("266", "Provider NPI");
                //pdfFormFields.SetField("266", dsPdfValue.Tables[0].Rows[0]["SZ_NPI"].ToString());
                pdfFormFields.SetField("266", dsPdfValue.Tables[0].Rows[0]["MST_OFFICE_NPI"].ToString());
                pdfFormFields.SetField("267", dsPdfValue.Tables[0].Rows[0]["32b"].ToString());

                // Amod - commented
                //pdfFormFields.SetField("267", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_1_3"].ToString() + dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_4_E"].ToString());

                ////pdfFormFields.SetField("273", "provider NPI");
                pdfFormFields.SetField("273", dsPdfValue.Tables[0].Rows[0]["MST_OFFICE_NPI_BILLING"].ToString());
                pdfFormFields.SetField("274", dsPdfValue.Tables[0].Rows[0]["33b"].ToString());

                ////pdfFormFields.SetField("268", "provider phone 3");
                ////pdfFormFields.SetField("269", "provider phone rem");
                pdfFormFields.SetField("268", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_1_3"].ToString());
                pdfFormFields.SetField("269", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_4_E"].ToString());

                ////pdfFormFields.SetField("270", "Billing provider name");
                pdfFormFields.SetField("270", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_NAME_BILLING"].ToString());
                pdfFormFields.SetField("271", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS_BILLING"].ToString());
                pdfFormFields.SetField("272", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS2_BILLING"].ToString());

                //Date-08_Sept_2016 change- add bill comment
                pdfFormFields.SetField("RxComment", dsPdfValue.Tables[0].Rows[0]["sz_bill_comment"].ToString());

                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
            }
        }

        //return newPdfFilename;

    }
   
}
