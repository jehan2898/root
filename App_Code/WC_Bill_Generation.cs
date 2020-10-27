using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using PDFValueReplacement;
using System.Collections;
using log4net;
using PDFValueReplacement;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using iTextSharp.text;
using PsychologistReport;
using Microsoft.SqlServer.Management.Common;

/// <summary>
/// Summary description for WC_Bill_Generation
/// </summary>
public class WC_Bill_Generation : PageBase
{
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    Bill_Sys_NF3_Template objNF3Template;
    private Bill_Sys_NF3_Template _bill_Sys_NF3_Template;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    private static ILog log = LogManager.GetLogger("PDFValueReplacement");
    MUVGenerateFunction _MUVGenerateFunction;
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_Verification_Desc objVerification_Desc;

    CaseDetailsBO objCaseDetailsBO;

    public WC_Bill_Generation()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string funGenerateBill(string p_szCaseID, string p_szCompanyID, string p_szBillID)
    {
        string str = "";
        return str;

    }
    public string GetTreatmentPdf(string p_szCaseID, string p_szCompanyID, string p_szBillID, string szFilepath)
    {
        string szHtmlPath = ConfigurationSettings.AppSettings["WC_HTML"].ToString();
        string szLine = "";
        string szPdfPath = "";
        StreamReader objReader;
        objReader = new StreamReader(szHtmlPath);
        do
        {
            szLine = szLine + objReader.ReadLine() + "\r\n";
        } while (objReader.Peek() != -1);

        objReader.Close();
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        try
        {

            objSqlConn.Open();
            SqlCommand objSqlComm1 = new SqlCommand("GET_PROCEDURE_WC", objSqlConn);
            objSqlComm1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader objDR;
            objSqlComm1.CommandType = CommandType.StoredProcedure;
            objSqlComm1.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillID);
            objSqlComm1.Parameters.AddWithValue("@Flag", "PATIENT_INFO");
            objDR = objSqlComm1.ExecuteReader();
            string szName = "";
            string DOA = "";
            while (objDR.Read())
            {
                szName = objDR[0].ToString();
                DOA = objDR[1].ToString();
            }
            szLine = szLine.Replace("SZ_PattientName", szName);
            szLine = szLine.Replace("DOI", DOA);
            string szTabRecord = GetRecords(p_szBillID);
            szLine = szLine.Replace("ADD PROCEDURE CODE TABLE", szTabRecord);
            szPdfPath = GeneratePDF(szLine, p_szBillID, szFilepath);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objSqlConn.Close();
        }
        return szPdfPath;
    }
    public string GetTreatmentPdf(string p_szCaseID, string p_szCompanyID, string p_szBillID, string szFilepath, ServerConnection conn)
    {
        string szHtmlPath = ConfigurationSettings.AppSettings["WC_HTML"].ToString();
        string szLine = "";
        string szPdfPath = "";
        StreamReader objReader;
        objReader = new StreamReader(szHtmlPath);
        do
        {
            szLine = szLine + objReader.ReadLine() + "\r\n";
        } while (objReader.Peek() != -1);

        objReader.Close();
        SqlDataReader objDR = null;
        try
        {
            string Query = "";
            Query = Query + "Exec GET_PROCEDURE_WC ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", p_szBillID, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@Flag", "PATIENT_INFO", ",");
            Query = Query.TrimEnd(',');

            objDR = conn.ExecuteReader(Query);


            string szName = "";
            string DOA = "";
            while (objDR.Read())
            {
                szName = objDR[0].ToString();
                DOA = objDR[1].ToString();
            }
            objDR.Close();
            szLine = szLine.Replace("SZ_PattientName", szName);
            szLine = szLine.Replace("DOI", DOA);
            string szTabRecord = GetRecords(p_szBillID, conn);
            szLine = szLine.Replace("ADD PROCEDURE CODE TABLE", szTabRecord);
            szPdfPath = GeneratePDF(szLine, p_szBillID, szFilepath);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            if (objDR != null && !objDR.IsClosed) { objDR.Close(); }
        }
        return szPdfPath;
    }
    protected string GeneratePDF(string strHtml, string szBillNo, string szFilePath)
    {
        GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
        string pdffilename = "";
        try
        {
            //  string szFileData = File.ReadAllText(ConfigurationManager.AppSettings["NF2_SENT_MAIL_HTML"]);
            //  szFileData = objPDF.getNF2MailDetails(szFileData, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            //string strHtml = GenerateHTML();
            //szFileData = szFileData.Replace("VL_SZ_TABLE_DATA", strHtml);
            //szFileData = szFileData.Replace("VL_SZ_CASE_COUNT", Session["VL_COUNT"].ToString());


            SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();
            objHTMToPDF.Serial = "10007706603";
            string htmfilename = getFileName(szBillNo) + ".htm";
            pdffilename = getFileName(szBillNo) + ".pdf";
            log4net.Config.XmlConfigurator.Configure();
            log.Debug("atul1 " + szFilePath);

            if (!Directory.Exists(szFilePath))
            {
                Directory.CreateDirectory(szFilePath);
                log.Debug("atul2 " + szFilePath);
            }
            StreamWriter sw = new StreamWriter(szFilePath + htmfilename);
            sw.Write(strHtml);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(szFilePath + htmfilename, szFilePath + pdffilename);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        string path = szFilePath + pdffilename;
        log4net.Config.XmlConfigurator.Configure();
        log.Debug("atul " + path);
        return path;


    }

    public string GetRecords(string szBillId)
    {
        string szTableString = "";
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        try
        {

            objSqlConn.Open();
            SqlCommand objSqlCommwc = new SqlCommand("GET_PROCEDURE_WC", objSqlConn);
            objSqlCommwc.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader objDR;
            objSqlCommwc.CommandType = CommandType.StoredProcedure;
            objSqlCommwc.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillId);
            objSqlCommwc.Parameters.AddWithValue("@Flag", "PATIENT_INFO");
            objDR = objSqlCommwc.ExecuteReader();
            string szName = "";
            string DOA = "";
            while (objDR.Read())
            {
                szName = objDR[0].ToString();
                DOA = objDR[1].ToString();
            }
            objDR.Close();
            objSqlConn.Close();

            int noOfRecordsPerpage = Convert.ToInt32(ConfigurationSettings.AppSettings["WC_TREATMENT_RECORD_PER_PAGE"].ToString());
            string szCount = GetProcedureNumber(szBillId);
            objSqlConn.Open();
            SqlCommand objSqlComm = new SqlCommand("GET_PROCEDURE_WC", objSqlConn);
            objSqlComm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader rd;
            objSqlComm.CommandType = CommandType.StoredProcedure;
            objSqlComm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillId);
            objSqlComm.Parameters.AddWithValue("@FLAG", "CODE");
            rd = objSqlComm.ExecuteReader();
            szTableString = @" <table width='100%' border='1' ><tr colspan='15' align='center'><td> <b>Use WCB Codes</b></td></tr><tr><td colspan='6' >Dates of Service</td><td colspan='1' ></td><td colspan='1'></td><td colspan='2'>Procedures, Services or Supplies</td><td colspan='5'>&nbsp;</td></tr> <tr><td colspan='3' >From</td><td colspan='3' >To</td><td colspan='1'></td><td colspan='1'></td><td colspan='2'></td><td colspan='5'></td></tr> <tr><td width='4%' >MM</td><td width='4%' >DD</td><td width='4%' > YY</td><td width='4%' >MM</td><td width='4%' >DD</td><td width='4%'> YY</td><td width='4%'> Place of Service</td><td width='4%'>Leave Blank</td><td width='9%'> CPT/HCPCS</td><td width='12%'>MODIFIER</td><td width='9%' align='center' >Diagnosis Code</td><td width='10%' align='center'>$&nbsp;Charges</td><td width='4%' align='center' > Days/Units</td><td width='6%' align='center'>COB</td><td width='18%' align='center'>Zip code where service was rendered</td></tr>";
            Decimal dbTotal = 0.0M;
            int count = 0;
            while (rd.Read())
            {

                if (count > noOfRecordsPerpage)
                {
                    szTableString += "</table><span style='page-break-before: always;'>";
                    szTableString += "<table width='100%'  ><tr><td width='60%' align='left'><b>Patient's Name</b>     " + szName + "</td><td width='40%' align='right' ><b>Date of injury/onset of illness </b>    " + DOA + "</td></tr></table>";
                    szTableString += @" <table width='100%' border='1' ><tr colspan='15' align='center'><td> <b>Use WCB Codes</b></td></tr><tr><td colspan='6' >Dates of Service</td><td colspan='1' ></td><td colspan='1'></td><td colspan='2'>Procedures, Services or Supplies</td><td colspan='5'>&nbsp;</td></tr> <tr><td colspan='3' >From</td><td colspan='3' >To</td><td colspan='1'></td><td colspan='1'></td><td colspan='2'></td><td colspan='5'></td></tr> <tr><td width='4%' >MM</td><td width='4%' >DD</td><td width='4%' > YY</td><td width='4%' >MM</td><td width='4%' >DD</td><td width='4%'> YY</td><td width='4%'> Place of Service</td><td width='4%'>Leave Blank</td><td width='9%'> CPT/HCPCS</td><td width='12%'>MODIFIER</td><td width='9%' align='center' >Diagnosis Code</td><td width='10%' align='center'>$&nbsp;Charges</td><td width='4%' align='center' > Days/Units</td><td width='6%' align='center'>COB</td><td width='18%' align='center'>Zip code where service was rendered</td></tr>";
                    szTableString += "<tr> <td  width='4%' align='center'>" + rd["MONTH"].ToString() + "</td> <td  width='4%' align='center' > " + rd["DAY"].ToString() + "</td> <td  width='4%' align='center' > " + rd["YEAR"].ToString() + "</td> <td  width='4%' align='center' >" + rd["TO_MONTH"].ToString() + "</td><td  width='4%' align='center' >" + rd["TO_DAY"].ToString() + "</td> <td  width='4%' align='center' > " + rd["TO_YEAR"].ToString() + "</td> <td  width='4%'>" + rd["PLACE_OF_SERVICE"].ToString() + "</td> <td width='4%'>&nbsp;</td> <td width='10%'> " + rd["SZ_PROCEDURE_CODE"].ToString() + "</td><td  width='10%'>" + rd["SZ_MODIFIER"].ToString() + "</td> <td  width='10%'>" + szCount + "</td> <td  width='10%' align='right' > " + rd["FL_AMOUNT"].ToString() + "</td> <td  width='4%' align='center' >" + rd["I_UNIT"].ToString() + "</td> <td  width='4%'>&nbsp;</td> <td width='20%' align='center'>" + rd["ZIP_CODE"].ToString() + "</td> </tr>";
                    count = 0;
                }
                else
                {

                    szTableString += "<tr> <td  width='4%' align='center' >" + rd["MONTH"].ToString() + "</td> <td  width='4%' align='center'> " + rd["DAY"].ToString() + "</td> <td  width='4%' align='center' > " + rd["YEAR"].ToString() + "</td> <td  width='4%' align='center' >" + rd["TO_MONTH"].ToString() + "</td><td  width='4%' align='center' >" + rd["TO_DAY"].ToString() + "</td> <td  width='4%' align='center' > " + rd["TO_YEAR"].ToString() + "</td> <td  width='4%'>" + rd["PLACE_OF_SERVICE"].ToString() + "</td> <td width='4%'>&nbsp;</td> <td width='10%'> " + rd["SZ_PROCEDURE_CODE"].ToString() + "</td><td  width='10%'>" + rd["SZ_MODIFIER"].ToString() + "</td> <td  width='10%'>" + szCount + "</td> <td width='10%' align='right'> " + rd["FL_AMOUNT"].ToString() + "</td> <td width='4%' align='center' >" + rd["I_UNIT"].ToString() + "</td> <td  width='6%'>&nbsp;</td> <td width='20%' align='center' >" + rd["ZIP_CODE"].ToString() + "</td> </tr>";

                    count = count + 1;
                }

            }
            objSqlConn.Close();
            objSqlComm.Cancel();
            objSqlConn.Open();
            SqlCommand objSqlComm1 = new SqlCommand("SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW_TEST_1", objSqlConn);
            objSqlComm1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader rd1;
            objSqlComm1.CommandType = CommandType.StoredProcedure;
            objSqlComm1.Parameters.AddWithValue("@SZ_BILL_ID", szBillId);
            objSqlComm1.Parameters.AddWithValue("@FLAG", "TOTAL_AMOUNT");
            rd1 = objSqlComm1.ExecuteReader();
            string szTotalAmouunt = "";
            string szPaidAmount = "";
            string szBalance = "";



            while (rd1.Read())
            {
                szTotalAmouunt = decimal.Parse(rd1[0].ToString()).ToString("c");
                szPaidAmount = decimal.Parse(rd1[1].ToString()).ToString("c");
                szBalance = decimal.Parse(rd1[2].ToString()).ToString("c");
            }
            DataSet dsbillinfo = new DataSet();
            dsbillinfo = Getbillinfo(szBillId);
            if (dsbillinfo.Tables.Count > 0)
            {
                if (dsbillinfo.Tables[0].Rows.Count > 0)
                {
                    if (dsbillinfo.Tables[0].Rows[0].ToString() != "" && dsbillinfo.Tables[0].Rows[0].ToString() != null)
                    {
                        string btppo = dsbillinfo.Tables[0].Rows[0]["BT_PPO"].ToString();
                        if (btppo == "True")
                        {
                            szTableString += "</table></span>";
                            szTableString += "<table width='100%' ><tr><td  width='70%'><input id='Checkbox1' type='checkbox' checked='checked'/> Check here if services were provided by a WCB preferred provider organization (PPO)</td><td ><table width='73%'  border='1 ><tr><td >Total Charge</td><td >Amount Paid (Carrier Use Only)</td><td >Balance Due (Carrier Use Only)</td></tr><tr><td align='right' >" + szTotalAmouunt + "</td><td  align='right' >" + szPaidAmount + "</td><td align='right'>" + szBalance + "</td></tr></table></td></tr></table>";
                        }
                        else
                        {
                            szTableString += "</table></span>";
                            szTableString += "<table width='100%' ><tr><td  width='70%'><input id='Checkbox1' type='checkbox' /> Check here if services were provided by a WCB preferred provider organization (PPO)</td><td ><table width='73%' border='1 ><tr><td >Total Charge</td><td >Amount Paid (Carrier Use Only)</td><td >Balance Due (Carrier Use Only)</td></tr><tr><td align='right' >" + szTotalAmouunt + "</td><td  align='right' >" + szPaidAmount + "</td><td align='right'>" + szBalance + "</td></tr></table></td></tr></table>";
                        }

                    }
                }

            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objSqlConn.Close();
        }

        return szTableString;
    }

    public string GetRecords(string szBillId, ServerConnection conn)
    {
        string szTableString = "";
        SqlDataReader objDR = null;
        try
        {
            string Query = "";
            Query = Query + "Exec GET_PROCEDURE_WC ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", szBillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@Flag", "PATIENT_INFO", ",");
            Query = Query.TrimEnd(',');

            objDR = conn.ExecuteReader(Query);
            string szName = "";
            string DOA = "";
            while (objDR.Read())
            {
                szName = objDR[0].ToString();
                DOA = objDR[1].ToString();
            }
            objDR.Close();


            int noOfRecordsPerpage = Convert.ToInt32(ConfigurationSettings.AppSettings["WC_TREATMENT_RECORD_PER_PAGE"].ToString());
            string szCount = GetProcedureNumber(szBillId, conn);
            Query = "";
            Query = Query + "Exec GET_PROCEDURE_WC ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", szBillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@Flag", "CODE", ",");
            Query = Query.TrimEnd(',');

            objDR = conn.ExecuteReader(Query);

            szTableString = @" <table width='100%' border='1' ><tr colspan='15' align='center'><td> <b>Use WCB Codes</b></td></tr><tr><td colspan='6' >Dates of Service</td><td colspan='1' ></td><td colspan='1'></td><td colspan='2'>Procedures, Services or Supplies</td><td colspan='5'>&nbsp;</td></tr> <tr><td colspan='3' >From</td><td colspan='3' >To</td><td colspan='1'></td><td colspan='1'></td><td colspan='2'></td><td colspan='5'></td></tr> <tr><td width='4%' >MM</td><td width='4%' >DD</td><td width='4%' > YY</td><td width='4%' >MM</td><td width='4%' >DD</td><td width='4%'> YY</td><td width='4%'> Place of Service</td><td width='4%'>Leave Blank</td><td width='9%'> CPT/HCPCS</td><td width='12%'>MODIFIER</td><td width='9%' align='center' >Diagnosis Code</td><td width='10%' align='center'>$&nbsp;Charges</td><td width='4%' align='center' > Days/Units</td><td width='6%' align='center'>COB</td><td width='18%' align='center'>Zip code where service was rendered</td></tr>";
            Decimal dbTotal = 0.0M;
            int count = 0;
            while (objDR.Read())
            {

                if (count > noOfRecordsPerpage)
                {
                    szTableString += "</table><span style='page-break-before: always;'>";
                    szTableString += "<table width='100%'  ><tr><td width='60%' align='left'><b>Patient's Name</b>     " + szName + "</td><td width='40%' align='right' ><b>Date of injury/onset of illness </b>    " + DOA + "</td></tr></table>";
                    szTableString += @" <table width='100%' border='1' ><tr colspan='15' align='center'><td> <b>Use WCB Codes</b></td></tr><tr><td colspan='6' >Dates of Service</td><td colspan='1' ></td><td colspan='1'></td><td colspan='2'>Procedures, Services or Supplies</td><td colspan='5'>&nbsp;</td></tr> <tr><td colspan='3' >From</td><td colspan='3' >To</td><td colspan='1'></td><td colspan='1'></td><td colspan='2'></td><td colspan='5'></td></tr> <tr><td width='4%' >MM</td><td width='4%' >DD</td><td width='4%' > YY</td><td width='4%' >MM</td><td width='4%' >DD</td><td width='4%'> YY</td><td width='4%'> Place of Service</td><td width='4%'>Leave Blank</td><td width='9%'> CPT/HCPCS</td><td width='12%'>MODIFIER</td><td width='9%' align='center' >Diagnosis Code</td><td width='10%' align='center'>$&nbsp;Charges</td><td width='4%' align='center' > Days/Units</td><td width='6%' align='center'>COB</td><td width='18%' align='center'>Zip code where service was rendered</td></tr>";
                    szTableString += "<tr> <td  width='4%' align='center'>" + objDR["MONTH"].ToString() + "</td> <td  width='4%' align='center' > " + objDR["DAY"].ToString() + "</td> <td  width='4%' align='center' > " + objDR["YEAR"].ToString() + "</td> <td  width='4%' align='center' >" + objDR["TO_MONTH"].ToString() + "</td><td  width='4%' align='center' >" + objDR["TO_DAY"].ToString() + "</td> <td  width='4%' align='center' > " + objDR["TO_YEAR"].ToString() + "</td> <td  width='4%'>" + objDR["PLACE_OF_SERVICE"].ToString() + "</td> <td width='4%'>&nbsp;</td> <td width='10%'> " + objDR["SZ_PROCEDURE_CODE"].ToString() + "</td><td  width='10%'>" + objDR["SZ_MODIFIER"].ToString() + "</td> <td  width='10%'>" + szCount + "</td> <td  width='10%' align='right' > " + objDR["FL_AMOUNT"].ToString() + "</td> <td  width='4%' align='center' >" + objDR["I_UNIT"].ToString() + "</td> <td  width='4%'>&nbsp;</td> <td width='20%' align='center'>" + objDR["ZIP_CODE"].ToString() + "</td> </tr>";
                    count = 0;
                }
                else
                {

                    szTableString += "<tr> <td  width='4%' align='center' >" + objDR["MONTH"].ToString() + "</td> <td  width='4%' align='center'> " + objDR["DAY"].ToString() + "</td> <td  width='4%' align='center' > " + objDR["YEAR"].ToString() + "</td> <td  width='4%' align='center' >" + objDR["TO_MONTH"].ToString() + "</td><td  width='4%' align='center' >" + objDR["TO_DAY"].ToString() + "</td> <td  width='4%' align='center' > " + objDR["TO_YEAR"].ToString() + "</td> <td  width='4%'>" + objDR["PLACE_OF_SERVICE"].ToString() + "</td> <td width='4%'>&nbsp;</td> <td width='10%'> " + objDR["SZ_PROCEDURE_CODE"].ToString() + "</td><td  width='10%'>" + objDR["SZ_MODIFIER"].ToString() + "</td> <td  width='10%'>" + szCount + "</td> <td width='10%' align='right'> " + objDR["FL_AMOUNT"].ToString() + "</td> <td width='4%' align='center' >" + objDR["I_UNIT"].ToString() + "</td> <td  width='6%'>&nbsp;</td> <td width='20%' align='center' >" + objDR["ZIP_CODE"].ToString() + "</td> </tr>";

                    count = count + 1;
                }

            }
            objDR.Close();
            Query = "";
            Query = Query + "Exec SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW_TEST_1 ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", szBillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "TOTAL_AMOUNT", ",");
            Query = Query.TrimEnd(',');

            objDR = conn.ExecuteReader(Query);

            string szTotalAmouunt = "";
            string szPaidAmount = "";
            string szBalance = "";



            while (objDR.Read())
            {
                szTotalAmouunt = decimal.Parse(objDR[0].ToString()).ToString("c");
                szPaidAmount = decimal.Parse(objDR[1].ToString()).ToString("c");
                szBalance = decimal.Parse(objDR[2].ToString()).ToString("c");
            }
            objDR.Close();
            DataSet dsbillinfo = new DataSet();
            dsbillinfo = Getbillinfo(szBillId, conn);
            if (dsbillinfo.Tables.Count > 0)
            {
                if (dsbillinfo.Tables[0].Rows.Count > 0)
                {
                    if (dsbillinfo.Tables[0].Rows[0].ToString() != "" && dsbillinfo.Tables[0].Rows[0].ToString() != null)
                    {
                        string btppo = dsbillinfo.Tables[0].Rows[0]["BT_PPO"].ToString();
                        if (btppo == "True")
                        {
                            szTableString += "</table></span>";
                            szTableString += "<table width='100%' ><tr><td  width='70%'><input id='Checkbox1' type='checkbox' checked='checked'/> Check here if services were provided by a WCB preferred provider organization (PPO)</td><td ><table width='73%'  border='1 ><tr><td >Total Charge</td><td >Amount Paid (Carrier Use Only)</td><td >Balance Due (Carrier Use Only)</td></tr><tr><td align='right' >" + szTotalAmouunt + "</td><td  align='right' >" + szPaidAmount + "</td><td align='right'>" + szBalance + "</td></tr></table></td></tr></table>";
                        }
                        else
                        {
                            szTableString += "</table></span>";
                            szTableString += "<table width='100%' ><tr><td  width='70%'><input id='Checkbox1' type='checkbox' /> Check here if services were provided by a WCB preferred provider organization (PPO)</td><td ><table width='73%' border='1 ><tr><td >Total Charge</td><td >Amount Paid (Carrier Use Only)</td><td >Balance Due (Carrier Use Only)</td></tr><tr><td align='right' >" + szTotalAmouunt + "</td><td  align='right' >" + szPaidAmount + "</td><td align='right'>" + szBalance + "</td></tr></table></td></tr></table>";
                        }

                    }
                }

            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            if (objDR != null && !objDR.IsClosed) { objDR.Close(); }

        }

        return szTableString;
    }

    public DataSet Getbillinfo(string Sz_Billnumber)
    {
        SqlConnection sqlCon = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_BILL_INFO_C4_2", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", Sz_Billnumber);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }
    public DataSet Getbillinfo(string Sz_Billnumber, ServerConnection conn)
    {

        DataSet ds = new DataSet();
        try
        {
            string Query = "";
            Query = Query + "Exec SP_GET_BILL_INFO_C4_2 ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", Sz_Billnumber, ",");

            Query = Query.TrimEnd(',');
            ds = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {

        }
        return ds;
    }
    public int GetProcedureCount(string szBillNo)
    {
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        int iCount = 0;
        try
        {

            objSqlConn.Open();
            SqlCommand objSqlComm1 = new SqlCommand("SP_WORKER_PROCEDURE_CODE_COUNT", objSqlConn);
            objSqlComm1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader objDR;
            objSqlComm1.CommandType = CommandType.StoredProcedure;
            objSqlComm1.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNo);
            objDR = objSqlComm1.ExecuteReader();

            string szCount = "";
            while (objDR.Read())
            {
                szCount = objDR[0].ToString();


            }

            if (szCount != "")
            {
                iCount = Convert.ToInt32(szCount);
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objSqlConn.Close();
        }
        return iCount;

    }
    public int GetProcedureCount(string szBillNo, ServerConnection conn)
    {
        SqlDataReader objDR = null;
        int iCount = 0;
        try
        {
            string Query = "";
            Query = Query + "Exec SP_WORKER_PROCEDURE_CODE_COUNT ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", szBillNo, ",");

            Query = Query.TrimEnd(',');


            objDR = conn.ExecuteReader(Query);

            string szCount = "";
            while (objDR.Read())
            {
                szCount = objDR[0].ToString();


            }

            if (szCount != "")
            {
                iCount = Convert.ToInt32(szCount);
            }
            objDR.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            if (objDR != null && !objDR.IsClosed)
                objDR.Close();
        }
        return iCount;

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
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }


    #region "Generate PDF Logic"

    public string GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality, string CaseId, string CmpId, string CmpName, string UserId, string UserName, string CaseNO)
    {
        string returnPath = "";
        try
        {
            String szSpecility = p_szSpeciality;





            objNF3Template = new Bill_Sys_NF3_Template();

            CaseDetailsBO objCaseDetails = new CaseDetailsBO();
            String szCompanyID = CmpId;
            if (objCaseDetails.GetCaseType(p_szBillNumber) == "WC000000000000000002")
            {
                String szDefaultPath = CmpName + "/" + CaseId + "/Packet Document/";
                String szSourceDir = CmpName + "/" + CaseId + "/Packet Document/";
                String szDestinationDir = CmpName + "/" + CaseId + "/No Fault File/Bills/" + szSpecility + "/";
                string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

                string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                String szFile3 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                String szFile4 = ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                String szPDFPage1;
                String szXMLFileName;
                String szOriginalPDFFileName;
                String szLastXMLFileName;
                String szLastOriginalPDFFileName;
                String sz3and4Page;
                Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
                String szDiagPDFFilePosition = objConfiguration.getConfigurationSettings(szCompanyID, "GET_DIAG_PAGE_POSITION");
                String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(szCompanyID, "DIAG_PAGE");

                szXMLFileName = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                szOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                szLastXMLFileName = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                szLastOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();


                Boolean fAddDiag = true;

                GeneratePDFFile.GenerateNF3PDF objGeneratePDF = new GeneratePDFFile.GenerateNF3PDF();
                objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

                // Note : Generate PDF with Billing Information table. **** II
                String szPDFFileName = objGeneratePDF.GeneratePDF(CmpId, CmpName, UserId, UserName, CaseId, p_szBillNumber, "", strPath);


                sz3and4Page = szFile3;//""objPDFReplacement.Merge3and4Page(CmpId, CmpName, CaseId.ToString(), p_szBillNumber, szFile3, szFile4);

                szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, p_szBillNumber, CmpName, CaseId);




                // Merge **** I AND **** II
                String szPDF_1_3;
                // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
                szPDF_1_3 = objPDFReplacement.MergePDFFiles(CmpId, CmpName, CaseId, p_szBillNumber, szPDFPage1, szPDFFileName);

                String szLastPDFFileName;
                String szPDFPage3;
                szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, p_szBillNumber, CmpName, CaseId);

                MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(CmpId, CmpName, CaseId.ToString(), p_szBillNumber, szPDF_1_3, szPDFPage3);
                //szLastPDFFileName = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName,  ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDefaultPath + szLastPDFFileName, p_szBillNumber, CmpName, CaseId.ToString());
                String szGenereatedFileName = "";
                szGenereatedFileName = szDefaultPath + szLastPDFFileName;



                String szOpenFilePath = "";
                szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szGenereatedFileName;


                string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;


                CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
                string newPdfFilename = "";
                string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                objMyForm.initialize(KeyForCutePDF);

                if (objMyForm == null)
                {
                    // Response.Write("objMyForm not initialized");
                }
                else
                {
                    if (System.IO.File.Exists(szFileNameWithFullPath))
                    {
                        //    objMyForm.mergePDF(szFileNameWithFullPath, szFileNameWithFullPath.Replace(".pdf", "_Temp.pdf").ToString(), szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString());

                        if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(p_szBillNumber) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAT_THE_END)
                        {
                            if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(p_szBillNumber) == 5)
                            {
                            }
                            else
                            {
                                //MergePDF.MergePDFFiles(szFileNameWithFullPath, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString());
                                szPDFFileName = szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf");
                            }
                        }

                    }

                }

                // End Logic

                string szFileNameForSaving = "";

                // Save Entry in Table
                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                {
                    szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
                }

                // End

                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
                {
                    szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                    returnPath = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf");
                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "');", true);
                }
                else
                {
                    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                        // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "');", true);
                        returnPath = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                    }
                    else
                    {
                        szFileNameForSaving = szOpenFilePath.ToString();
                        //  Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenFilePath.ToString() + "');", true);
                        returnPath = szOpenFilePath.ToString();
                    }
                }
                String[] szTemp;
                string szBillName = "";
                szTemp = szFileNameForSaving.Split('/');
                ArrayList objAL = new ArrayList();
                szFileNameForSaving = szFileNameForSaving.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                szBillName = szTemp[szTemp.Length - 1].ToString();

                if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
                {
                    if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                    {
                        Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                    }
                    File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
                }

                objAL.Add(p_szBillNumber);
                objAL.Add(szDestinationDir + szBillName);
                objAL.Add(CmpId);
                objAL.Add(CaseId);
                objAL.Add(szTemp[szTemp.Length - 1].ToString());
                objAL.Add(szDestinationDir);
                objAL.Add(UserName);
                objAL.Add(szSpecility);
                objAL.Add("NF");
                objAL.Add(CaseNO);
                objNF3Template.saveGeneratedBillPath(objAL);

                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = UserId;
                _DAO_NOTES_EO.SZ_CASE_ID = CaseId;
                _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);




                // End 


            }
            else
            {
                //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
            }

            Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return returnPath;
    }

    #endregion




    public string GeneratePDFForWorkerComp(string szBillNumber, string szCaseID, string p_szPDFNo, string CmpId, string CmpName, string UserId, string UserName, string CaseNO, string speciality, int CmpBit)
    {
        speciality = speciality.Trim();
        string returnPath = "";
        DataSet ds1500form = new DataSet();
        string bt_1500_Form = "";
        int iCount = 0;

        iCount = GetProcedureCount(szBillNumber);
        _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
        String szDefaultPhysicalPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/";

        // changes for Doc manager for News Bill Path-- pravin
        objVerification_Desc = new Bill_Sys_Verification_Desc();
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();


        objVerification_Desc.sz_bill_no = szBillNumber;
        objVerification_Desc.sz_company_id = CmpId;
        objVerification_Desc.sz_flag = "BILL";

        ArrayList arrNf_Param = new ArrayList();
        ArrayList arrNf_NodeType = new ArrayList();
        string sz_Type = "";
        String szDestinationDir = "";


        arrNf_Param.Add(objVerification_Desc);

        arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Param);
        if (arrNf_NodeType.Contains("NFVER"))
        {
            sz_Type = "OLD";

            szDestinationDir = CmpName + "/" + szCaseID + "/No Fault File/Bills/" + speciality + "/";
        }
        else
        {
            sz_Type = "NEW";
            szDestinationDir = CmpName + "/" + szCaseID + "/No Fault File/Medicals/" + speciality + "/" + "Bills/";
        }
        if (p_szPDFNo == "5")
        {
            PsychologyBillPDF objPsyPdf = new PsychologyBillPDF();

            string szFilename = objPsyPdf.PsyPDF(szBillNumber, szCaseID, CmpId, CmpName, UserId, UserName, CaseNO, speciality, szDestinationDir);
            Bill_Sys_NF3_Template objNF3Templatepsypdf = new Bill_Sys_NF3_Template();
            ArrayList objAL = new ArrayList();
            if (sz_Type == "OLD")
            {
                objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                objAL.Add(szDestinationDir + szFilename); // SZ_BILL_PATH
                objAL.Add(CmpId);
                objAL.Add(szCaseID);
                objAL.Add(szFilename); // SZ_BILL_NAME
                objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                objAL.Add(UserName);
                objAL.Add(speciality.Trim());
                objAL.Add("WC");
                objAL.Add(CaseNO);
                objNF3Templatepsypdf.saveGeneratedBillPath(objAL);
            }
            else
            {
                objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                objAL.Add(szDestinationDir + szFilename); // SZ_BILL_PATH
                objAL.Add(CmpId);
                objAL.Add(szCaseID);
                objAL.Add(szFilename); // SZ_BILL_NAME
                objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                objAL.Add(UserName);
                objAL.Add(speciality.Trim());
                objAL.Add("WC");
                objAL.Add(CaseNO);
                objAL.Add(arrNf_NodeType[0].ToString());
                objNF3Templatepsypdf.saveGeneratedBillPath_New(objAL);
            }


            _DAO_NOTES_EO = new DAO_NOTES_EO();
            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szFilename;

            _DAO_NOTES_BO = new DAO_NOTES_BO();
            _DAO_NOTES_EO.SZ_USER_ID = UserId;
            _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
            _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

            string PathOpenPdf = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + szFilename;

            return PathOpenPdf;

        }

        log.Debug("In Function");
        if (iCount > 5)
        {
            string szURLDocumentManager = ApplicationSettings.GetParameterValue("DocumentManagerURL");

            try
            {
                string szProcCodePathPdf = GetTreatmentPdf(szCaseID, CmpId, szBillNumber, szDefaultPhysicalPath1);
                string xmlFilePath_wc5 = "";
                string xmlFilePath1_wc5 = "";
                string PathOpen = "";
                objNF3Template = new Bill_Sys_NF3_Template();
                objCaseDetailsBO = new CaseDetailsBO();
                //changes for Add only 1500 Form For Insurance Company -- pravin

                ds1500form = objCaseDetailsBO.Get1500FormBitForInsurance(CmpId, szBillNumber);
                if (ds1500form.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1500form.Tables[0].Rows.Count; i++)
                    {
                        bt_1500_Form = ds1500form.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (bt_1500_Form == "1")
                {

                    string str_1500 = "";
                    String szSourceDir = CmpName + "/" + szCaseID + "/Packet Document/";


                    string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                    Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                    str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, szBillNumber, CmpName, szCaseID, CmpId);



                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }

                    returnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + str_1500;
                    ArrayList objAL = new ArrayList();
                    if (sz_Type == "OLD")
                    {
                        objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL.Add(szDestinationDir + str_1500); // SZ_BILL_PATH
                        objAL.Add(CmpId);
                        objAL.Add(szCaseID);
                        objAL.Add(str_1500); // SZ_BILL_NAME
                        objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                        objAL.Add(UserName);
                        objAL.Add(speciality.Trim());
                        objAL.Add("WC");
                        objAL.Add(CaseNO);
                        objNF3Template.saveGeneratedBillPath(objAL);
                    }
                    else
                    {
                        objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL.Add(szDestinationDir + str_1500); // SZ_BILL_PATH
                        objAL.Add(CmpId);
                        objAL.Add(szCaseID);
                        objAL.Add(str_1500); // SZ_BILL_NAME
                        objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                        objAL.Add(UserName);
                        objAL.Add(speciality.Trim());
                        objAL.Add("WC");
                        objAL.Add(CaseNO);
                        objAL.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL);
                    }


                    // Start : Save Notes for Bill.
                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = UserId;
                    _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


                }
                else
                {
                    if (p_szPDFNo == "1")
                    {
                        _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
                        GenerateItextPdf objGenerateItextPdf = new GenerateItextPdf();
                        String szDefaultPhysicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/";
                        PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                        if (CmpBit == 1)
                        {
                            xmlFilePath_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_P1"].ToString();
                        }
                        else
                        {
                            xmlFilePath_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_P1"].ToString();
                        }
                        string PdfPath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P1"].ToString();
                        //string strGenFileName = _pDFValueReplacement.ReplacePDFvalues(xmlFilePath_wc5, PdfPath, szBillNumber, CmpName, szCaseID);

                        Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                        Bill_Sys_Data obj1 = new Bill_Sys_Data();
                        //obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, xmlFilePath_wc5, PdfPath, CmpId, szCaseID, CmpName);
                        obj1 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC40Part1(CmpId, szCaseID, szBillNumber, CmpName);
                        string strGenFileName = obj1.billurl;

                        ArrayList objAL = new ArrayList();
                        objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                        objAL.Add(szBillNumber);
                        objAL.Add(szCaseID);
                        objAL.Add(CmpId);
                        _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
                        string openPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                        string szNewPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_" + szCaseID + "1.pdf";
                        string szFirstMerge = MergePDF.MergePDFFiles(openPath, szProcCodePathPdf, szNewPath);


                        if (CmpBit == 1)
                        {
                            xmlFilePath1_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_P2"].ToString();
                        }
                        else
                        {
                            xmlFilePath1_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_P2"].ToString();
                        }
                        string PdfPath1 = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P2"].ToString();
                        //string strGenFileName1 = _pDFValueReplacement.ReplacePDFvalues(xmlFilePath1_wc5, PdfPath1, szBillNumber, CmpName, szCaseID);

                        //Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                        Bill_Sys_Data obj2 = new Bill_Sys_Data();
                        // obj2 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, xmlFilePath1_wc5, PdfPath1, CmpId, szCaseID, CmpName);
                        obj2 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC40Part2(CmpId, szCaseID, szBillNumber, CmpName);
                        string strGenFileName1 = obj2.billurl;

                        ArrayList objAL1 = new ArrayList();
                        objAL1.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName1);
                        objAL1.Add(szBillNumber);
                        objAL1.Add(szCaseID);
                        objAL1.Add(CmpId);
                        _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL1);
                        string openPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName1;
                        string szNewPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_" + szCaseID + "2.pdf";
                        // string szcopyto = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/No Fault File/" + "Bills/" + speciality.Trim() + "/";
                        string szcopyto = _bill_Sys_NF3_Template.getPhysicalPath() + szDestinationDir;
                        // PathOpen =  ApplicationSettings.GetParameterValue("DocumentManagerURL") +CmpName + "/" + szCaseID + "/No Fault File/" + "Bills/"+speciality.Trim()+"/";
                        PathOpen = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir;
                        string szSecondMerge = MergePDF.MergePDFFiles(szNewPath, openPath1, szNewPath1);

                        if (File.Exists(szNewPath1))
                        {
                            if (!Directory.Exists(szcopyto))
                            {
                                Directory.CreateDirectory(szcopyto);
                            }
                            File.Copy(szNewPath1, szcopyto + szBillNumber + "_MRG.pdf", true);
                        }
                        string szFinalPath = PathOpen + szBillNumber + "_MRG.pdf";


                        //Tushar  : To Mearge MUV Billing PDF
                        string bt_CaseType, bt_include, str_1500;
                        _MUVGenerateFunction = new MUVGenerateFunction();
                        string prefix = speciality.Substring(0, 2);
                        string PrcoedureGroup;
                        if (prefix.ToString().Equals("PR"))
                        {
                            PrcoedureGroup = _bill_Sys_NF3_Template.getGroup(szBillNumber);
                        }
                        else
                        {
                            PrcoedureGroup = speciality;
                        }
                        bt_include = _MUVGenerateFunction.get_bt_include(CmpId, PrcoedureGroup, "", "Speciality");
                        bt_CaseType = _MUVGenerateFunction.get_bt_include(CmpId, "", "WC000000000000000001", "CaseType");
                        if (bt_include == "True" && bt_CaseType == "True")
                        {
                            Session["TM_SZ_CASE_ID"] = szCaseID;
                            str_1500 = _MUVGenerateFunction.FillPdf(szBillNumber.ToString());

                            MergePDF.MergePDFFiles(szcopyto + szBillNumber + "_MRG.pdf", objNF3Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + str_1500, szcopyto + szBillNumber + "_MRG.pdf");

                            //returnPath =  ApplicationSettings.GetParameterValue("DocumentManagerURL") + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_MRG.pdf";
                        }
                        //End Of Code



                        //  @SZ_BILL_NUMBER='RR463',@SZ_BILL_PATH='IDF Diagnostic Facility/3211/No Fault File/Bills/MRI/RR463_9967_201007191920522052.pdf',@SZ_BILL_NAME='RR463_9967_201007191920522052.pdf',@SZ_USER_NAME='mri',@SZ_SPECIALITY='MRI',@SZ_COMPANY_ID='CO000000000000000043',@SZ_CASE_ID='57',@SZ_BILL_FILE_PATH='IDF Diagnostic Facility/3211/No Fault File/Bills/MRI/',@CASE_TYPE='NF'    

                        ArrayList objAL2 = new ArrayList();
                        // changes for Doc manager for News Bill Path-- pravin

                        if (sz_Type == "OLD")
                        {
                            objAL2.Add(szBillNumber); // SZ_BILL_NUMBER
                            objAL2.Add(szDestinationDir + szBillNumber + "_MRG.pdf"); // SZ_BILL_PATH
                            objAL2.Add(CmpId);
                            objAL2.Add(szCaseID);
                            objAL2.Add(szBillNumber + "_MRG.pdf"); // SZ_BILL_NAME
                            objAL2.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                            objAL2.Add(UserName);
                            objAL2.Add(speciality.Trim());
                            objAL2.Add("WC");
                            objAL2.Add(CaseNO);
                            objNF3Template.saveGeneratedBillPath(objAL2);
                        }
                        else
                        {
                            objAL2.Add(szBillNumber); // SZ_BILL_NUMBER
                            objAL2.Add(szDestinationDir + szBillNumber + "_MRG.pdf"); // SZ_BILL_PATH
                            objAL2.Add(CmpId);
                            objAL2.Add(szCaseID);
                            objAL2.Add(szBillNumber + "_MRG.pdf"); // SZ_BILL_NAME
                            objAL2.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                            objAL2.Add(UserName);
                            objAL2.Add(speciality.Trim());
                            objAL2.Add("WC");
                            objAL2.Add(CaseNO);
                            objAL2.Add(arrNf_NodeType[0].ToString());
                            objNF3Template.saveGeneratedBillPath_New(objAL2);
                        }



                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = strGenFileName;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = UserId;
                        _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                        returnPath = szFinalPath;



                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);
                    }
                    if (p_szPDFNo == "2")
                    {

                        log4net.Config.XmlConfigurator.Configure();
                        log4net.Config.XmlConfigurator.Configure();
                        GenerateItextPdf objGenerateItextPdf = new GenerateItextPdf();
                        String szDefaultPhysicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/";
                        PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                        if (CmpBit == 1)
                        {
                            try
                            {
                                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                                {
                                    xmlFilePath_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1_SEC"].ToString();
                                    log.Debug("atul " + ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1_SEC"].ToString());
                                }
                                else
                                {
                                    xmlFilePath_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString();
                                    log.Debug("atul " + ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString());
                                }
                            }
                            catch
                            {
                                xmlFilePath_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString();
                                log.Debug("atul " + ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString());
                            }
                        }
                        else
                        {
                            try
                            {
                                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                                {
                                    xmlFilePath_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1_SEC"].ToString();
                                    log.Debug("atul " + ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1_SEC"].ToString());
                                }
                                else
                                {
                                    xmlFilePath_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString();
                                    log.Debug("atul " + ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString());
                                }
                            }
                            catch
                            {
                                xmlFilePath_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString();
                                log.Debug("atul " + ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString());
                            }
                        }
                        string PdfPath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_2_P1"].ToString();
                        //string strGenFileName = _pDFValueReplacement.ReplacePDFvalues(xmlFilePath_wc5, PdfPath, szBillNumber, CmpName, szCaseID);

                        Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                        Bill_Sys_Data obj1 = new Bill_Sys_Data();
                        //obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, xmlFilePath_wc5, PdfPath, CmpId, szCaseID, CmpName);
                        obj1 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC42Part1(CmpId, szCaseID, szBillNumber, CmpName);
                        string strGenFileName = obj1.billurl;

                        log.Debug("atul " + strGenFileName);
                        ArrayList objAL = new ArrayList();
                        objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                        objAL.Add(szBillNumber);
                        objAL.Add(szCaseID);
                        objAL.Add(CmpId);
                        _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
                        string openPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                        log.Debug("openPath " + openPath);
                        string szNewPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_" + szCaseID + "1.pdf";
                        log.Debug("szNewPath " + szNewPath);
                        string szFirstMerge = MergePDF.MergePDFFiles(openPath, szProcCodePathPdf, szNewPath);
                        if (CmpBit == 1)
                        {
                            xmlFilePath1_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P2"].ToString();
                        }
                        else
                        {
                            xmlFilePath1_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P2"].ToString();
                        }
                        string PdfPath1 = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_2_P2"].ToString();
                        log.Debug("xmlFilePath1_wc5 " + xmlFilePath1_wc5);
                        //string strGenFileName1 = _pDFValueReplacement.ReplacePDFvalues(xmlFilePath1_wc5, PdfPath1, szBillNumber, CmpName, szCaseID);

                        // Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                        Bill_Sys_Data obj2 = new Bill_Sys_Data();
                        // obj2 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, xmlFilePath1_wc5, PdfPath1, CmpId, szCaseID, CmpName);
                        obj2 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC42Part2(CmpId, szCaseID, szBillNumber, CmpName);
                        string strGenFileName1 = obj2.billurl;

                        log.Debug("strGenFileName1 " + strGenFileName1);
                        ArrayList objAL1 = new ArrayList();
                        objAL1.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName1);
                        objAL1.Add(szBillNumber);
                        objAL1.Add(szCaseID);
                        objAL1.Add(CmpId);
                        _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL1);
                        string openPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName1;
                        log.Debug("openPath1 " + openPath1);
                        string szNewPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_" + szCaseID + "2.pdf";
                        log.Debug("szNewPath1 " + szNewPath1);
                        // string szcopyto = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/No Fault File/" + "Bills/" + speciality.Trim() + "/";
                        string szcopyto = _bill_Sys_NF3_Template.getPhysicalPath() + szDestinationDir;
                        log.Debug("szcopyto " + szcopyto);
                        // PathOpen =  ApplicationSettings.GetParameterValue("DocumentManagerURL") + CmpName + "/" + szCaseID + "/No Fault File/" + "Bills/" + speciality.Trim() + "/";
                        PathOpen = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir;
                        log.Debug("PathOpen " + PathOpen);
                        string szSecondMerge = MergePDF.MergePDFFiles(szNewPath, openPath1, szNewPath1);
                        log.Debug("szNewPath1 " + szNewPath1);
                        if (File.Exists(szNewPath1))
                        {
                            if (!Directory.Exists(szcopyto))
                            {
                                Directory.CreateDirectory(szcopyto);
                            }
                            log.Debug("szNewPath1 " + szcopyto + szBillNumber + "_MRG.pdf");
                            File.Copy(szNewPath1, szcopyto + szBillNumber + "_MRG.pdf", true);
                        }
                        string szFinalPath = PathOpen + szBillNumber + "_MRG.pdf";

                        //Tushar  : To Mearge MUV Billing PDF
                        string bt_CaseType, bt_include, str_1500;
                        _MUVGenerateFunction = new MUVGenerateFunction();
                        string prefix = speciality.Substring(0, 2);
                        string PrcoedureGroup;
                        if (prefix.ToString().Equals("PR"))
                        {
                            PrcoedureGroup = _bill_Sys_NF3_Template.getGroup(szBillNumber);
                        }
                        else
                        {
                            PrcoedureGroup = speciality;
                        }
                        bt_include = _MUVGenerateFunction.get_bt_include(CmpId, PrcoedureGroup, "", "Speciality");
                        bt_CaseType = _MUVGenerateFunction.get_bt_include(CmpId, "", "WC000000000000000001", "CaseType");
                        if (bt_include == "True" && bt_CaseType == "True")
                        {
                            Session["TM_SZ_CASE_ID"] = szCaseID;
                            str_1500 = _MUVGenerateFunction.FillPdf(szBillNumber.ToString());
                            MergePDF.MergePDFFiles(szcopyto + szBillNumber + "_MRG.pdf", objNF3Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + str_1500, szcopyto + szBillNumber + "_MRG.pdf");

                            //szFinalPath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_MRG.pdf";
                        }
                        //End Of Code
                        ArrayList objAL3 = new ArrayList();
                        // changes for Doc manager for News Bill Path-- pravin
                        if (sz_Type == "OLD")
                        {

                            objAL3.Add(szBillNumber); // SZ_BILL_NUMBER
                            objAL3.Add(szDestinationDir + szBillNumber + "_MRG.pdf"); // SZ_BILL_PATH
                            objAL3.Add(CmpId);
                            objAL3.Add(szCaseID);
                            objAL3.Add(szBillNumber + "_MRG.pdf"); // SZ_BILL_NAME
                            objAL3.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                            objAL3.Add(UserName);
                            objAL3.Add(speciality.Trim());
                            objAL3.Add("WC");
                            objAL3.Add(CaseNO);
                            objNF3Template.saveGeneratedBillPath(objAL3);
                        }
                        else
                        {
                            objAL3.Add(szBillNumber); // SZ_BILL_NUMBER
                            objAL3.Add(szDestinationDir + szBillNumber + "_MRG.pdf"); // SZ_BILL_PATH
                            objAL3.Add(CmpId);
                            objAL3.Add(szCaseID);
                            objAL3.Add(szBillNumber + "_MRG.pdf"); // SZ_BILL_NAME
                            objAL3.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                            objAL3.Add(UserName);
                            objAL3.Add(speciality.Trim());
                            objAL3.Add("WC");
                            objAL3.Add(CaseNO);
                            objAL3.Add(arrNf_NodeType[0].ToString());
                            objNF3Template.saveGeneratedBillPath_New(objAL3);
                        }


                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = strGenFileName;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = UserId;
                        _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                        returnPath = szFinalPath;
                        returnPath = szFinalPath;
                        //http://localhost/MBSScans/Blue%20Ridge%20Medical/1321/No%20Fault%20File/Bills/Physiotheropy/sa39_MRG.pdf
                        //http://localhost/MBSScans/Blue Ridge Medical/1321/No Fault File/Bills/Physiotheropy /sa39_MRG.pdf
                        //  ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);
                    }
                    if (p_szPDFNo == "4")  //Add For Greater than count 6--sunil
                    {
                        string strGenFileName = "";
                        if (CmpBit == 1)
                        {

                        }
                        else
                        {
                            Bill_Sys_OCT_Bills obj_OCT = new Bill_Sys_OCT_Bills();
                            string sz_Bill_ID = szBillNumber;
                            DataSet ds1 = new DataSet();
                            DataSet ds_table = new DataSet();
                            ds1 = obj_OCT.GET_OCT_Bills_detail(sz_Bill_ID);
                            ds_table = obj_OCT.GET_OCT_Bills_Table(sz_Bill_ID);
                            DataTable dtoptpt = new DataTable();
                            dtoptpt.Columns.Add("MONTH");
                            dtoptpt.Columns.Add("DAY");
                            dtoptpt.Columns.Add("YEAR");
                            dtoptpt.Columns.Add("TO_MONTH");
                            dtoptpt.Columns.Add("TO_DAY");
                            dtoptpt.Columns.Add("TO_YEAR");
                            dtoptpt.Columns.Add("SZ_PROCEDURE_CODE");
                            dtoptpt.Columns.Add("SZ_DIGNOSIS");
                            dtoptpt.Columns.Add("DC_1");
                            dtoptpt.Columns.Add("DC_2");
                            dtoptpt.Columns.Add("DC_3");
                            dtoptpt.Columns.Add("DC_4");
                            dtoptpt.Columns.Add("DC_5");
                            dtoptpt.Columns.Add("DC_6");
                            dtoptpt.Columns.Add("FL_AMOUNT");
                            dtoptpt.Columns.Add("I_UNIT");
                            dtoptpt.Columns.Add("BILL_AMOUNT");
                            dtoptpt.Columns.Add("PAID_AMOUNT");
                            dtoptpt.Columns.Add("BALANCE");
                            dtoptpt.Columns.Add("ZIP_CODE");
                            dtoptpt.Columns.Add("PLACE_OF_SERVICE");

                            ArrayList arrPaths = new ArrayList();
                            ArrayList arrNewPAth = new ArrayList();
                            int iFlag = 1;
                            int count = -1;
                            for (int i = 0; i < ds_table.Tables[0].Rows.Count; i++)
                            {
                                if (count == -1)
                                {
                                    dtoptpt.Clear();
                                }
                                DataRow drotpt = dtoptpt.NewRow();
                                drotpt["MONTH"] = ds_table.Tables[0].Rows[i]["MONTH"].ToString();
                                drotpt["DAY"] = ds_table.Tables[0].Rows[i]["DAY"].ToString();
                                drotpt["YEAR"] = ds_table.Tables[0].Rows[i]["YEAR"].ToString();
                                drotpt["TO_MONTH"] = ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString();
                                drotpt["TO_DAY"] = ds_table.Tables[0].Rows[i]["TO_DAY"].ToString();
                                drotpt["TO_YEAR"] = ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString();
                                drotpt["SZ_PROCEDURE_CODE"] = ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString();
                                drotpt["SZ_DIGNOSIS"] = ds_table.Tables[0].Rows[i]["SZ_DIGNOSIS"].ToString();
                                drotpt["DC_1"] = ds_table.Tables[0].Rows[i]["DC_1"].ToString();
                                drotpt["DC_2"] = ds_table.Tables[0].Rows[i]["DC_2"].ToString();
                                drotpt["DC_3"] = ds_table.Tables[0].Rows[i]["DC_3"].ToString();
                                drotpt["DC_4"] = ds_table.Tables[0].Rows[i]["DC_4"].ToString();
                                drotpt["DC_5"] = ds_table.Tables[0].Rows[i]["DC_5"].ToString();
                                drotpt["DC_6"] = ds_table.Tables[0].Rows[i]["DC_6"].ToString();
                                drotpt["FL_AMOUNT"] = ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString();
                                drotpt["I_UNIT"] = ds_table.Tables[0].Rows[i]["I_UNIT"].ToString();
                                drotpt["BILL_AMOUNT"] = ds_table.Tables[0].Rows[i]["BILL_AMOUNT"].ToString();
                                drotpt["PAID_AMOUNT"] = ds_table.Tables[0].Rows[i]["PAID_AMOUNT"].ToString();
                                drotpt["BALANCE"] = ds_table.Tables[0].Rows[i]["BALANCE"].ToString();
                                drotpt["ZIP_CODE"] = ds_table.Tables[0].Rows[i]["ZIP_CODE"].ToString();
                                drotpt["PLACE_OF_SERVICE"] = ds_table.Tables[0].Rows[i]["PLACE_OF_SERVICE"].ToString();
                                dtoptpt.Rows.Add(drotpt);
                                count++;

                                if (count >= 5 && i != ds_table.Tables[0].Rows.Count - 1)
                                {
                                    string pdfFilePathnew = ConfigurationManager.AppSettings["PDF_FILE_PATH_OCT_1"].ToString();
                                    count = -1;
                                    DataSet dscountgrester = new DataSet();
                                    dscountgrester.Tables.Add(dtoptpt.Copy());
                                    string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                                    string szPdFIndex = i.ToString();
                                    string szNewPdfPath = Getotptpdfchangefield("page_" + szPdFIndex, dscountgrester, ds1, szBillNumber, szCaseID, CmpId, CmpName, pdfFilePathnew);
                                    arrNewPAth.Add(szURLDocumentManager_OCT + "/" + szNewPdfPath);

                                }
                                else if (i == ds_table.Tables[0].Rows.Count - 1)
                                {
                                    string pdfFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_OCT"].ToString();
                                    count = -1;
                                    DataSet dscountgrester = new DataSet();
                                    string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                                    dscountgrester.Tables.Add(dtoptpt.Copy());
                                    string szPdFIndex = i.ToString();
                                    string szNewPdfPath = Getotptpdf("page_" + szPdFIndex, dscountgrester, ds1, szBillNumber, szCaseID, CmpId, CmpName, pdfFilePath);
                                    arrNewPAth.Add(szURLDocumentManager_OCT + "/" + szNewPdfPath);

                                }
                            }

                            string szNewFile = arrNewPAth[0].ToString();

                            for (int i = 0; i < arrNewPAth.Count - 1; i++)
                            {

                                string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                                strGenFileName = szBillNumber + "_" + szCaseID + "_" + i.ToString() + ".pdf";
                                string NewPdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                                string szFirstMerge = MergePDF.MergePDFFiles(szNewFile, arrNewPAth[i + 1].ToString(), NewPdfPath);
                                szNewFile = NewPdfPath;

                            }

                            string szFinalOtptPdf = szNewFile;
                            string sztimepath = DateTime.Now.ToString("MMddyyyyhhmmss");
                            string szcopyto = _bill_Sys_NF3_Template.getPhysicalPath() + szDestinationDir;
                            if (File.Exists(szNewFile))
                            {
                                if (!Directory.Exists(szcopyto))
                                {
                                    Directory.CreateDirectory(szcopyto);
                                }
                                log.Debug("szNewPath1 " + szcopyto + szBillNumber + "_" + sztimepath + "_1MRG.pdf");
                                File.Copy(szNewFile, szcopyto + szBillNumber + "_" + sztimepath + "_1MRG.pdf", true);
                            }
                            PathOpen = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir;
                            string szFinalPath = PathOpen + szBillNumber + "_" + sztimepath + "_1MRG.pdf";



                            //Tushar  : To Mearge MUV Billing PDF
                            string bt_CaseType, bt_include, str_1500;
                            _MUVGenerateFunction = new MUVGenerateFunction();
                            string prefix = speciality.Substring(0, 2);
                            string PrcoedureGroup;
                            if (prefix.ToString().Equals("PR"))
                            {
                                PrcoedureGroup = _bill_Sys_NF3_Template.getGroup(szBillNumber);
                            }
                            else
                            {
                                PrcoedureGroup = speciality;
                            }
                            bt_include = _MUVGenerateFunction.get_bt_include(CmpId, PrcoedureGroup, "", "Speciality");
                            bt_CaseType = _MUVGenerateFunction.get_bt_include(CmpId, "", "WC000000000000000001", "CaseType");
                            if (bt_include == "True" && bt_CaseType == "True")
                            {
                                Session["TM_SZ_CASE_ID"] = szCaseID;
                                str_1500 = _MUVGenerateFunction.FillPdf(szBillNumber.ToString());
                                MergePDF.MergePDFFiles(szcopyto + szBillNumber + "_" + sztimepath + "_1MRG.pdf", objNF3Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + str_1500, szcopyto + szBillNumber + "_" + sztimepath + "_1MRG.pdf");

                                //szFinalPath =  ApplicationSettings.GetParameterValue("DocumentManagerURL") + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_MRG.pdf";
                            }
                            //End Of Code
                            ArrayList objAL3 = new ArrayList();
                            // changes for Doc manager for News Bill Path-- pravin
                            if (sz_Type == "OLD")
                            {

                                objAL3.Add(szBillNumber); // SZ_BILL_NUMBER
                                objAL3.Add(szDestinationDir + szBillNumber + "_" + sztimepath + "_1MRG.pdf"); // SZ_BILL_PATH
                                objAL3.Add(CmpId);
                                objAL3.Add(szCaseID);
                                objAL3.Add(szBillNumber + "_" + sztimepath + "_1MRG.pdf"); // SZ_BILL_NAME
                                objAL3.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                                objAL3.Add(UserName);
                                objAL3.Add(speciality.Trim());
                                objAL3.Add("WC");
                                objAL3.Add(CaseNO);
                                objNF3Template.saveGeneratedBillPath(objAL3);
                            }
                            else
                            {
                                objAL3.Add(szBillNumber); // SZ_BILL_NUMBER
                                objAL3.Add(szDestinationDir + szBillNumber + "_" + sztimepath + "_1MRG.pdf"); // SZ_BILL_PATH
                                objAL3.Add(CmpId);
                                objAL3.Add(szCaseID);
                                objAL3.Add(szBillNumber + "_" + sztimepath + "_1MRG.pdf"); // SZ_BILL_NAME
                                objAL3.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                                objAL3.Add(UserName);
                                objAL3.Add(speciality.Trim());
                                objAL3.Add("WC");
                                objAL3.Add(CaseNO);
                                objAL3.Add(arrNf_NodeType[0].ToString());
                                objNF3Template.saveGeneratedBillPath_New(objAL3);
                            }


                            _DAO_NOTES_EO = new DAO_NOTES_EO();
                            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = strGenFileName;

                            _DAO_NOTES_BO = new DAO_NOTES_BO();
                            _DAO_NOTES_EO.SZ_USER_ID = UserId;
                            _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                            _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                            returnPath = szFinalPath;


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //  Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
            }
        }
        else
        {
            String szURLDocumentManager = "";
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            objNF3Template = new Bill_Sys_NF3_Template();
            objCaseDetailsBO = new CaseDetailsBO();
            szURLDocumentManager = ApplicationSettings.GetParameterValue("DocumentManagerURL");
            String szDefaultPhysicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/";
            string strGenFileName = "";
            try
            {           //changes for Add only 1500 Form For Insurance Company -- pravin

                ds1500form = objCaseDetailsBO.Get1500FormBitForInsurance(CmpId, szBillNumber);
                if (ds1500form.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1500form.Tables[0].Rows.Count; i++)
                    {
                        bt_1500_Form = ds1500form.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (bt_1500_Form == "1")
                {
                    String szSourceDir = CmpName + "/" + szCaseID + "/Packet Document/";
                    string str_1500 = "";


                    string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                    Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                    str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, szBillNumber, CmpName, szCaseID, CmpId);


                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }

                    returnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + str_1500;
                    ArrayList objAL = new ArrayList();
                    if (sz_Type == "OLD")
                    {
                        objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL.Add(szDestinationDir + str_1500); // SZ_BILL_PATH
                        objAL.Add(CmpId);
                        objAL.Add(szCaseID);
                        objAL.Add(str_1500); // SZ_BILL_NAME
                        objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                        objAL.Add(UserName);
                        objAL.Add(speciality.Trim());
                        objAL.Add("WC");
                        objAL.Add(CaseNO);
                        objNF3Template.saveGeneratedBillPath(objAL);
                    }
                    else
                    {
                        objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL.Add(szDestinationDir + str_1500); // SZ_BILL_PATH
                        objAL.Add(CmpId);
                        objAL.Add(szCaseID);
                        objAL.Add(str_1500); // SZ_BILL_NAME
                        objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                        objAL.Add(UserName);
                        objAL.Add(speciality.Trim());
                        objAL.Add("WC");
                        objAL.Add(CaseNO);
                        objAL.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL);
                    }


                    // Start : Save Notes for Bill.
                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = UserId;
                    _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


                }
                else
                {
                    if (p_szPDFNo == "1")
                    {
                        PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                        string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
                        if (CmpBit == 1)
                        {
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName);
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }
                        else
                        {
                            log.Debug("In First Pdf");
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            GenerateItextPdf objGenerateItextPdf = new GenerateItextPdf();
                            // obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName);
                            obj1 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC40LessThan5(CmpId, szCaseID, szBillNumber, CmpName);
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }

                        //ArrayList objAL = new ArrayList();
                        //objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                        //objAL.Add(szBillNumber);
                        //objAL.Add(szCaseID);
                        //objAL.Add(CmpId);
                        //_bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
                        string openPath = szURLDocumentManager + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;

                        returnPath = openPath;
                    }

                    if (p_szPDFNo == "2")
                    {
                        PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                        GenerateItextPdf objGenerateItextPdf = new GenerateItextPdf();
                        string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C42"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
                        if (CmpBit == 1)
                        {
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            try
                            {
                                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"] == "Secondary")
                                    obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C42_SEC"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName);
                                else
                                    obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName);
                            }
                            catch
                            { obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName); }
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }
                        else
                        {
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            try
                            {
                                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"] == "Secondary")
                                    obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42_SEC"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName);
                                else
                                    obj1 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC42LessThan5(CmpId, szCaseID, szBillNumber, CmpName);
                                //obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName);

                            }
                            catch { obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName); }
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }

                        //ArrayList objAL = new ArrayList();
                        //objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                        //objAL.Add(szBillNumber);
                        //objAL.Add(szCaseID);
                        //objAL.Add(CmpId);
                        //_bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);

                        string openPath = szURLDocumentManager + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;

                        returnPath = openPath;
                    }

                    if (p_szPDFNo == "3")
                    {
                        PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                        string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C43"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";

                        if (CmpBit == 1)
                        {
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName);
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }
                        else
                        {
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName);
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }

                        //ArrayList objAL = new ArrayList();
                        //objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                        //objAL.Add(szBillNumber);
                        //objAL.Add(szCaseID);
                        //objAL.Add(CmpId);
                        //_bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);

                        string openPath = szURLDocumentManager + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;

                        returnPath = openPath;
                    }
                    if (p_szPDFNo == "4")   //Kapil
                    {

                        string ReadpdfFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_OCT"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";

                        if (CmpBit == 1)
                        {

                        }
                        else
                        {
                            Bill_Sys_OCT_Bills obj_OCT = new Bill_Sys_OCT_Bills();
                            string sz_Bill_ID = szBillNumber;
                            DataSet ds1 = new DataSet();
                            DataSet ds_table = new DataSet();
                            ds1 = obj_OCT.GET_OCT_Bills_detail(sz_Bill_ID);
                            ds_table = obj_OCT.GET_OCT_Bills_Table(sz_Bill_ID);

                            strGenFileName = szBillNumber + "_" + szCaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmm") + ".pdf";
                            //strGenFileName=newPdfFilename;
                            //string Dr_Cohen_Electronic_Script = ConfigurationManager.AppSettings["PDF_FILE_PATH_PATIENT_INFORMATION"].ToString();
                            // string pdfPath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString()) + newPdfFilename;
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
                            PdfReader reader = new PdfReader(ReadpdfFilePath);
                            PdfStamper stamper = new PdfStamper(reader, System.IO.File.Create(pdfPath));
                            AcroFields fields = stamper.AcroFields;
                            #region code for generate pdf Kapil
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0]["OCCUPATIONAL THERAPISTS REPORT"].ToString() == "1")
                                {
                                    fields.SetField("OCCUPATIONAL THERAPISTS REPORT", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["OCCUPATIONAL THERAPISTS REPORT"].ToString() == "0")
                                {
                                    fields.SetField("OCCUPATIONAL THERAPISTS REPORT", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["PHYSICAL THERAPISTS REPORT"].ToString() == "1")
                                {
                                    fields.SetField("PHYSICAL THERAPISTS REPORT", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["PHYSICAL THERAPISTS REPORT"].ToString() == "0")
                                {
                                    fields.SetField("PHYSICAL THERAPISTS REPORT", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["BT_PPO"].ToString() == "1")
                                {
                                    fields.SetField("Check Box14", "1");
                                    fields.SetField("Check Box15", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["BT_PPO"].ToString() == "0")
                                {
                                    fields.SetField("Check Box14", "0");
                                    fields.SetField("Check Box15", "1");
                                }


                                if (ds1.Tables[0].Rows[0]["Check Box16"].ToString() == "1")
                                {
                                    fields.SetField("Check Box16", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box16"].ToString() == "0")
                                {
                                    fields.SetField("Check Box16", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box17"].ToString() == "1")
                                {
                                    fields.SetField("Check Box17", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box17"].ToString() == "0")
                                {
                                    fields.SetField("Check Box17", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box18"].ToString() == "1")
                                {
                                    fields.SetField("Check Box18", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box18"].ToString() == "0")
                                {
                                    fields.SetField("Check Box18", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString() == null || ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString() == "")
                                {
                                    fields.SetField("WCB CASE NORow1", "-");
                                }
                                else
                                {
                                    fields.SetField("WCB CASE NORow1", ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString() == null || ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString() == "")
                                {
                                    fields.SetField("CARRIER CASE NO IF KNOWNRow1", "-");
                                }
                                else
                                {
                                    fields.SetField("CARRIER CASE NO IF KNOWNRow1", ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString() == null || ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString() == "")
                                {
                                    fields.SetField("DATE OF INJURYRow1", "-");
                                }
                                else
                                {
                                    fields.SetField("DATE OF INJURYRow1", ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text46"].ToString() == null || ds1.Tables[0].Rows[0]["Text46"].ToString() == "")
                                {
                                    fields.SetField("Text46", "-");
                                }
                                else
                                {
                                    fields.SetField("Text46", ds1.Tables[0].Rows[0]["Text46"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString() == null || ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString() == "")
                                {
                                    fields.SetField("ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1", "-");
                                }
                                else
                                {
                                    fields.SetField("ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1", ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString() == null || ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString() == "")
                                {
                                    fields.SetField("INJURED PERSONS SOCIAL SECURITY NUMBERRow1", "-");
                                }
                                else
                                {
                                    fields.SetField("INJURED PERSONS SOCIAL SECURITY NUMBERRow1", ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString() == null || ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString() == "")
                                {
                                    fields.SetField("INJURED PERSON", "-");
                                }
                                else
                                {
                                    fields.SetField("INJURED PERSON", ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text32"].ToString() == null || ds1.Tables[0].Rows[0]["Text32"].ToString() == "")
                                {
                                    fields.SetField("Text32", "-");
                                }
                                else
                                {
                                    fields.SetField("Text32", ds1.Tables[0].Rows[0]["Text32"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString() == null || ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString() == "")
                                {
                                    fields.SetField("TELEPHONE NO", "-");
                                }
                                else
                                {
                                    fields.SetField("TELEPHONE NO", ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString() == null || ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString() == "")
                                {
                                    fields.SetField("First Name Middle Initial Last NameEMPLOYER", "-");
                                }
                                else
                                {
                                    fields.SetField("First Name Middle Initial Last NameEMPLOYER", ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString() == null || ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString() == "")
                                {
                                    fields.SetField("ADDRESS Include Apt NoEMPLOYER", "-");
                                }
                                else
                                {
                                    fields.SetField("ADDRESS Include Apt NoEMPLOYER", ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString() == null || ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString() == "")
                                {
                                    fields.SetField("PATIENTS DATE OF BIRTH", "-");
                                }
                                else
                                {
                                    fields.SetField("PATIENTS DATE OF BIRTH", ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString());

                                }
                                if (ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString() == null || ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString() == "")
                                {
                                    fields.SetField("INSURANCE CARRIER", "-");
                                }
                                else
                                {
                                    fields.SetField("INSURANCE CARRIER", ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text30"].ToString() == null || ds1.Tables[0].Rows[0]["Text30"].ToString() == "")
                                {
                                    fields.SetField("Text30", "-");
                                }
                                else
                                {
                                    fields.SetField("Text30", ds1.Tables[0].Rows[0]["Text30"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString() == null || ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString() == "")
                                {
                                    fields.SetField("REFERRING PHYSICIAN PODIATRIST", "-");
                                }
                                else
                                {
                                    fields.SetField("REFERRING PHYSICIAN PODIATRIST", ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text31"].ToString() == null || ds1.Tables[0].Rows[0]["Text31"].ToString() == "")
                                {
                                    fields.SetField("Text31", "-");
                                }
                                else
                                {
                                    fields.SetField("Text31", ds1.Tables[0].Rows[0]["Text31"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString() == null || ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString() == "")
                                {
                                    fields.SetField("TELEPHONE NO_2", "-");
                                }
                                else
                                {
                                    fields.SetField("TELEPHONE NO_2", ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString() == "")
                                {
                                    fields.SetField("REFERRING PHYSICIAN PODIATRIST", "-");
                                }
                                else
                                {
                                    fields.SetField("REFERRING PHYSICIAN PODIATRIST", ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString() == "")
                                {
                                    fields.SetField("Text31", "-");
                                }
                                else
                                {
                                    fields.SetField("Text31", ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box19"].ToString() == "1")
                                {
                                    fields.SetField("Check Box19", "0");
                                    fields.SetField("Check Box20", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box19"].ToString() == "0")
                                {
                                    fields.SetField("Check Box19", "1");
                                    fields.SetField("Check Box20", "0");
                                }
                                //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "1")
                                //{
                                //    fields.SetField("Check Box20", "1");
                                //}
                                //else if (ds1.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "0")
                                //{
                                //    fields.SetField("Check Box20", "0");
                                //}
                                if (ds1.Tables[0].Rows[0]["Text43"].ToString() == null || ds1.Tables[0].Rows[0]["Text43"].ToString() == "")
                                {
                                    fields.SetField("Text43", "-");
                                }
                                else
                                {
                                    fields.SetField("Text43", ds1.Tables[0].Rows[0]["Text43"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text33"].ToString() == null || ds1.Tables[0].Rows[0]["Text33"].ToString() == "")
                                {
                                    fields.SetField("Text33", "-");
                                }
                                else
                                {
                                    fields.SetField("Text33", ds1.Tables[0].Rows[0]["Text33"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString() == null || ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString() == "")
                                {
                                    fields.SetField("patient history of preexisting injury disease", "-");
                                }
                                else
                                {
                                    fields.SetField("patient history of preexisting injury disease", ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "0")
                                {
                                    fields.SetField("Check Box34", "1");
                                    fields.SetField("Check Box35", "0");
                                    fields.SetField("Check Box36", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "1")
                                {
                                    fields.SetField("Check Box34", "0");
                                    fields.SetField("Check Box35", "1");
                                    fields.SetField("Check Box36", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "2")
                                {
                                    fields.SetField("Check Box34", "0");
                                    fields.SetField("Check Box35", "0");
                                    fields.SetField("Check Box36", "1");
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box24"].ToString() == "1")
                                {
                                    fields.SetField("Check Box23", "1");
                                    fields.SetField("Check Box24", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box24"].ToString() == "0")
                                {
                                    fields.SetField("Check Box23", "0");
                                    fields.SetField("Check Box24", "1");
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box21"].ToString() == "1")
                                {
                                    fields.SetField("Check Box21", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box21"].ToString() == "0")
                                {
                                    fields.SetField("Check Box21", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["a Your evaluation"].ToString() == null || ds1.Tables[0].Rows[0]["a Your evaluation"].ToString() == "")
                                {
                                    fields.SetField("a Your evaluation", "-");
                                }
                                else
                                {
                                    fields.SetField("a Your evaluation", ds1.Tables[0].Rows[0]["a Your evaluation"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString() == null || ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString() == "")
                                {
                                    fields.SetField("b 1 Patients condition and progress", "-");
                                }
                                else
                                {
                                    fields.SetField("b 1 Patients condition and progress", ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString());
                                }
                                //if (ds1.Tables[0].Rows[0]["Check Box21"].ToString()== "1")
                                //{
                                //    fields.SetField("Check Box21", "1");
                                //}
                                //else if (ds1.Tables[0].Rows[0]["Check Box21"].ToString()== "0")
                                //{
                                //    fields.SetField("Check Box21", "0");
                                //}
                                if (ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString() == null || ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString() == "")
                                {
                                    fields.SetField("Treatment and planned future treatment", "-");
                                }
                                else
                                {
                                    fields.SetField("Treatment and planned future treatment", ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "1")
                                {
                                    fields.SetField("Check Box22", "1");
                                    fields.SetField("Check Box25", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "0")
                                {
                                    fields.SetField("Check Box22", "0");
                                    fields.SetField("Check Box25", "1");
                                }

                                if (ds1.Tables[0].Rows[0]["Text44"].ToString() == null || ds1.Tables[0].Rows[0]["Text44"].ToString() == "")
                                {
                                    fields.SetField("Text44", "-");
                                }
                                else
                                {
                                    fields.SetField("Text44", ds1.Tables[0].Rows[0]["Text44"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text45"].ToString() == null || ds1.Tables[0].Rows[0]["Text45"].ToString() == "")
                                {
                                    fields.SetField("Text45", "-");
                                }
                                else
                                {
                                    fields.SetField("Text45", ds1.Tables[0].Rows[0]["Text45"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString() == null || ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString() == "")
                                {
                                    fields.SetField("4 Dates of visits on which this report is based", "-");
                                }
                                else
                                {
                                    fields.SetField("4 Dates of visits on which this report is based", ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString() == null || ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString() == "")
                                {
                                    fields.SetField("If no was patient referred back to attending doctor D Yes D No", "-");
                                }
                                else
                                {
                                    fields.SetField("If no was patient referred back to attending doctor D Yes D No", ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box26"].ToString() == "1")
                                {
                                    fields.SetField("Check Box26", "1");
                                    fields.SetField("Check Box27", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box26"].ToString() == "0")
                                {
                                    fields.SetField("Check Box26", "0");
                                    fields.SetField("Check Box27", "1");
                                }
                                if (ds1.Tables[0].Rows[0]["H_2"].ToString() == null || ds1.Tables[0].Rows[0]["H_2"].ToString() == "")
                                {
                                    fields.SetField("H_2", "-");
                                }
                                else
                                {
                                    fields.SetField("H_2", ds1.Tables[0].Rows[0]["H_2"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box28"].ToString() == "1")
                                {
                                    fields.SetField("Check Box28", "1");
                                    fields.SetField("Check Box29", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box28"].ToString() == "0")
                                {
                                    fields.SetField("Check Box28", "0");
                                    fields.SetField("Check Box29", "1");
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "1")
                                {
                                    fields.SetField("Check Box22", "1");
                                    fields.SetField("Check Box25", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "0")
                                {
                                    fields.SetField("Check Box22", "0");
                                    fields.SetField("Check Box25", "1");
                                }
                                if (ds1.Tables[0].Rows[0]["Text37"].ToString() == null || ds1.Tables[0].Rows[0]["Text37"].ToString() == "")
                                {
                                    fields.SetField("Text37", "-");
                                }
                                else
                                {
                                    fields.SetField("Text37", ds1.Tables[0].Rows[0]["Text37"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text38"].ToString() == null || ds1.Tables[0].Rows[0]["Text38"].ToString() == "")
                                {
                                    fields.SetField("Text38", "-");
                                }
                                else
                                {
                                    fields.SetField("Text38", ds1.Tables[0].Rows[0]["Text38"].ToString());
                                }
                                //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(51).ToString() == null || ds1.Tables[0].Rows[0].ItemArray.GetValue(51).ToString() == "")
                                //{
                                //    fields.SetField("1", "-");
                                //}
                                //else
                                //{
                                //    fields.SetField("1", ds1.Tables[0].Rows[0].ItemArray.GetValue(51).ToString());
                                //}
                                //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(52).ToString() == null || ds1.Tables[0].Rows[0].ItemArray.GetValue(52).ToString() == "")
                                //{
                                //    fields.SetField("2", "-");
                                //}
                                //else
                                //{
                                //    fields.SetField("2", ds1.Tables[0].Rows[0].ItemArray.GetValue(52).ToString());
                                //}
                                //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(53).ToString() == null || ds1.Tables[0].Rows[0].ItemArray.GetValue(53).ToString() == "")
                                //{
                                //    fields.SetField("3", "-");
                                //}
                                //else
                                //{
                                //    fields.SetField("3", ds1.Tables[0].Rows[0].ItemArray.GetValue(53).ToString());
                                //}
                                //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(54).ToString() == null || ds1.Tables[0].Rows[0].ItemArray.GetValue(54).ToString() == "")
                                //{
                                //    fields.SetField("4", "-");
                                //}
                                //else
                                //{
                                //    fields.SetField("4", ds1.Tables[0].Rows[0].ItemArray.GetValue(54).ToString());
                                //}
                                if (ds1.Tables[0].Rows[0]["Check Box40"].ToString() == "True")
                                {
                                    fields.SetField("Check Box40", "1");
                                    fields.SetField("Check Box41", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box40"].ToString() == "False")
                                {
                                    fields.SetField("Check Box40", "0");
                                    fields.SetField("Check Box41", "1");
                                }
                                //if (ds1.Tables[0].Rows[0]["Check Box41"].ToString()== "1")
                                //{
                                //    fields.SetField("Check Box41", "1");
                                //}
                                //else if (ds1.Tables[0].Rows[0]["Check Box41"].ToString()== "0")
                                //{
                                //    fields.SetField("Check Box41", "0");
                                //}
                                if (ds1.Tables[0].Rows[0]["Text39"].ToString() == null || ds1.Tables[0].Rows[0]["Text39"].ToString() == "")
                                {
                                    fields.SetField("TELEPHONE NO_2", "-");
                                }
                                else
                                {
                                    fields.SetField("TELEPHONE NO_2", ds1.Tables[0].Rows[0]["Text39"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString() == null || ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString() == "")
                                {
                                    fields.SetField("9 NYS License Number", "-");
                                }
                                else
                                {
                                    fields.SetField("9 NYS License Number", ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString() == "")
                                {
                                    fields.SetField("Text39", "-");
                                }
                                else
                                {
                                    fields.SetField("Text39", ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString() == null || ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString() == "")
                                {
                                    fields.SetField("10 Patients Account Number", "-");
                                }
                                else
                                {
                                    fields.SetField("10 Patients Account Number", ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString() == null || ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString() == "")
                                {
                                    fields.SetField("WCB CASE NORow1", "-");
                                }
                                else
                                {
                                    fields.SetField("WCB CASE NORow1", ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString() == "")
                                {
                                    fields.SetField("15 Therapists Name Address  Phone No", "-");
                                }
                                else
                                {
                                    fields.SetField("15 Therapists Name Address  Phone No", ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == "")
                                {
                                    fields.SetField("16 Therapists Billing Name Address  Phone No", "-");
                                }
                                else
                                {
                                    fields.SetField("16 Therapists Billing Name Address  Phone No", ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == "")
                                {
                                    fields.SetField("txtcheck", "-");
                                }
                                else
                                {
                                    fields.SetField("txtcheck", ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == "")
                                {
                                    fields.SetField("txtcheck1", "-");
                                }
                                else
                                {
                                    fields.SetField("txtcheck1", ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString());
                                }
                                if (ds_table.Tables[0].Rows.Count > 0)
                                {
                                    string Fmmtext = "FmmText";
                                    string FddText = "FddText";
                                    string FyyText = "FyyText";
                                    string TmmText = "TmmText";
                                    string TddText = "TddText";
                                    string TyyText = "TyyText";
                                    string BText = "BText";
                                    string DCText = "DCText";
                                    string DMText = "DMText";
                                    //string EText = "EText";
                                    string EText = "DC";
                                    string FText = "FText";
                                    string GText = "GText";
                                    string HText = "HText";
                                    string IText = "IText";
                                    for (int i = 0; i < ds_table.Tables[0].Rows.Count; i++)
                                    {
                                        //fields.SetField(Fmmtext+i.ToString(), ds_table.Tables[0].Rows[i].ItemArray.GetValue("MONTH").ToString()); 
                                        fields.SetField(Fmmtext + i.ToString(), ds_table.Tables[0].Rows[i]["MONTH"].ToString());

                                        fields.SetField(FddText + i.ToString(), ds_table.Tables[0].Rows[i]["DAY"].ToString());

                                        fields.SetField(FyyText + i.ToString(), ds_table.Tables[0].Rows[i]["YEAR"].ToString());

                                        fields.SetField(TmmText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString());

                                        fields.SetField(TddText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_DAY"].ToString());

                                        fields.SetField(TyyText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString());

                                        fields.SetField(BText + i.ToString(), ds_table.Tables[0].Rows[i]["PLACE_OF_SERVICE"].ToString());

                                        fields.SetField(DCText + i.ToString(), ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());

                                        //fields.SetField(DMText+i.ToString(), ds_table.Tables[0].Rows[i].ItemArray.GetValue("").ToString()); 
                                        fields.SetField(EText + i.ToString(), ds_table.Tables[0].Rows[i]["DC_1"].ToString());

                                        fields.SetField(FText + i.ToString(), ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString());

                                        fields.SetField(GText + i.ToString(), ds_table.Tables[0].Rows[i]["I_UNIT"].ToString());

                                        //fields.SetField(HText+i.ToString(), ds_table.Tables[0].Rows[i].ItemArray.GetValue("I_UNIT").ToString()); 

                                        fields.SetField(IText + i.ToString(), ds_table.Tables[0].Rows[i]["ZIP_CODE"].ToString());
                                    }
                                    fields.SetField("11 Total Charges", ds_table.Tables[0].Rows[0]["BILL_AMOUNT"].ToString());
                                    fields.SetField("12.Amt.Paid", ds_table.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
                                    fields.SetField("13.Balance", ds_table.Tables[0].Rows[0]["BALANCE"].ToString());
                                    fields.SetField("1", ds_table.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString());
                                }

                                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                                string[] u = url.Split('/');
                                string str = "";
                                for (int i = 0; i < u.Length; i++)
                                {
                                    if (i < 3)
                                    {
                                        if (i == 0)
                                        {
                                            str = u[i];
                                        }
                                        else
                                            str = str + "/" + u[i];
                                    }
                                }

                                stamper.Close();
                                //string szPath =  ApplicationSettings.GetParameterValue("DocumentManagerURL") + GetCompanyName(txtCompanyID.Text) + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Packet Document/" + newPdfFilename;
                                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szPath + "');", true);
                            }

                            #endregion
                            string openPath = szURLDocumentManager + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                            returnPath = openPath;

                        }
                    }
                    // Copy generated file from Packet Document to WC File.
                    objNF3Template = new Bill_Sys_NF3_Template();

                    String szBasePhysicalPath = objNF3Template.getPhysicalPath();
                    //changes for Doc Manager for New Bill Path -- pravin
                    //  String szNewPath = CmpName + "/" + szCaseID + "/No Fault File/Bills/" + speciality.Trim() + "/";
                    String szNewPath = szDestinationDir;
                    if (File.Exists(szBasePhysicalPath + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName))
                    {
                        if (!Directory.Exists(szBasePhysicalPath + szNewPath))
                        {
                            Directory.CreateDirectory(szBasePhysicalPath + szNewPath);
                        }
                        File.Copy(szBasePhysicalPath + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName, szBasePhysicalPath + szNewPath + strGenFileName);
                    }



                    //Tushar
                    string bt_CaseType, bt_include, str_1500;
                    _MUVGenerateFunction = new MUVGenerateFunction();
                    string prefix = speciality.Substring(0, 2);
                    string PrcoedureGroup;
                    if (prefix.ToString().Equals("PR"))
                    {
                        PrcoedureGroup = _bill_Sys_NF3_Template.getGroup(szBillNumber);
                    }
                    else
                    {
                        PrcoedureGroup = speciality;
                    }
                    bt_include = _MUVGenerateFunction.get_bt_include(CmpId, PrcoedureGroup, "", "Speciality");
                    bt_CaseType = _MUVGenerateFunction.get_bt_include(CmpId, "", "WC000000000000000001", "CaseType");
                    if (bt_include == "True" && bt_CaseType == "True")
                    {
                        Session["TM_SZ_CASE_ID"] = szCaseID;

                        str_1500 = _MUVGenerateFunction.FillPdf(szBillNumber.ToString());

                        MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szNewPath + strGenFileName, objNF3Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + str_1500, objNF3Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + str_1500.Replace(".pdf", "_MER.pdf"));
                        strGenFileName = str_1500.Replace(".pdf", "_MER.pdf");
                        File.Copy(szBasePhysicalPath + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName, szBasePhysicalPath + szNewPath + strGenFileName);

                        returnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                    }
                    //End Of Code



                    ArrayList objAL = new ArrayList();
                    objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                    objAL.Add(szBillNumber);
                    objAL.Add(szCaseID);
                    objAL.Add(CmpId);
                    _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);

                    ArrayList objAL1 = new ArrayList();

                    // changes for Doc manager for News Bill Path-- pravin
                    if (sz_Type == "OLD")
                    {

                        objAL1.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL1.Add(szNewPath + strGenFileName); // SZ_BILL_PATH
                        objAL1.Add(CmpId);
                        objAL1.Add(szCaseID);
                        objAL1.Add(strGenFileName); // SZ_BILL_NAME
                        objAL1.Add(szNewPath); // SZ_BILL_FILE_PATH
                        objAL1.Add(UserName);
                        objAL1.Add(speciality.Trim());
                        objAL1.Add("WC");
                        objAL1.Add(CaseNO);
                        objNF3Template.saveGeneratedBillPath(objAL1);
                    }
                    else
                    {
                        objAL1.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL1.Add(szNewPath + strGenFileName); // SZ_BILL_PATH
                        objAL1.Add(CmpId);
                        objAL1.Add(szCaseID);
                        objAL1.Add(strGenFileName); // SZ_BILL_NAME
                        objAL1.Add(szNewPath); // SZ_BILL_FILE_PATH
                        objAL1.Add(UserName);
                        objAL1.Add(speciality.Trim());
                        objAL1.Add("WC");
                        objAL1.Add(CaseNO);
                        objAL1.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL1);
                    }

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = strGenFileName;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = UserId;
                    _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        log4net.Config.XmlConfigurator.Configure();
        log.Debug("atul: " + returnPath);
        return returnPath;

    }
    public string GeneratePDFForWorkerComp(string szBillNumber, string szCaseID, string p_szPDFNo, string CmpId, string CmpName, string UserId, string UserName, string CaseNO, string speciality, int CmpBit, ServerConnection conn)
    {
        speciality = speciality.Trim();
        string returnPath = "";
        DataSet ds1500form = new DataSet();
        string bt_1500_Form = "";
        int iCount = 0;

        iCount = GetProcedureCount(szBillNumber, conn);
        _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
        String szDefaultPhysicalPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/";

        // changes for Doc manager for News Bill Path-- pravin
        objVerification_Desc = new Bill_Sys_Verification_Desc();
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();


        objVerification_Desc.sz_bill_no = szBillNumber;
        objVerification_Desc.sz_company_id = CmpId;
        objVerification_Desc.sz_flag = "BILL";

        ArrayList arrNf_Param = new ArrayList();
        ArrayList arrNf_NodeType = new ArrayList();
        string sz_Type = "";
        String szDestinationDir = "";


        arrNf_Param.Add(objVerification_Desc);

        arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Param, conn);
        if (arrNf_NodeType.Contains("NFVER"))
        {
            sz_Type = "OLD";

            szDestinationDir = CmpName + "/" + szCaseID + "/No Fault File/Bills/" + speciality + "/";
        }
        else
        {
            sz_Type = "NEW";
            szDestinationDir = CmpName + "/" + szCaseID + "/No Fault File/Medicals/" + speciality + "/" + "Bills/";
        }
        if (p_szPDFNo == "5")
        {
            PsychologyBillPDF objPsyPdf = new PsychologyBillPDF();

            string szFilename = objPsyPdf.PsyPDF(szBillNumber, szCaseID, CmpId, CmpName, UserId, UserName, CaseNO, speciality, szDestinationDir, conn);
            Bill_Sys_NF3_Template objNF3Templatepsypdf = new Bill_Sys_NF3_Template();
            ArrayList objAL = new ArrayList();
            if (sz_Type == "OLD")
            {
                objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                objAL.Add(szDestinationDir + szFilename); // SZ_BILL_PATH
                objAL.Add(CmpId);
                objAL.Add(szCaseID);
                objAL.Add(szFilename); // SZ_BILL_NAME
                objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                objAL.Add(UserName);
                objAL.Add(speciality.Trim());
                objAL.Add("WC");
                objAL.Add(CaseNO);
                objNF3Templatepsypdf.saveGeneratedBillPath(objAL, conn);
            }
            else
            {
                objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                objAL.Add(szDestinationDir + szFilename); // SZ_BILL_PATH
                objAL.Add(CmpId);
                objAL.Add(szCaseID);
                objAL.Add(szFilename); // SZ_BILL_NAME
                objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                objAL.Add(UserName);
                objAL.Add(speciality.Trim());
                objAL.Add("WC");
                objAL.Add(CaseNO);
                objAL.Add(arrNf_NodeType[0].ToString());
                objNF3Templatepsypdf.saveGeneratedBillPath_New(objAL, conn);
            }


            _DAO_NOTES_EO = new DAO_NOTES_EO();
            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szFilename;

            _DAO_NOTES_BO = new DAO_NOTES_BO();
            _DAO_NOTES_EO.SZ_USER_ID = UserId;
            _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
            _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

            string PathOpenPdf = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + szFilename;

            return PathOpenPdf;

        }

        log.Debug("In Function");
        if (iCount > 5)
        {
            string szURLDocumentManager = ApplicationSettings.GetParameterValue("DocumentManagerURL");

            try
            {
                string szProcCodePathPdf = GetTreatmentPdf(szCaseID, CmpId, szBillNumber, szDefaultPhysicalPath1, conn);
                string xmlFilePath_wc5 = "";
                string xmlFilePath1_wc5 = "";
                string PathOpen = "";
                objNF3Template = new Bill_Sys_NF3_Template();
                objCaseDetailsBO = new CaseDetailsBO();
                //changes for Add only 1500 Form For Insurance Company -- pravin

                ds1500form = objCaseDetailsBO.Get1500FormBitForInsurance(CmpId, szBillNumber, conn);
                if (ds1500form.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1500form.Tables[0].Rows.Count; i++)
                    {
                        bt_1500_Form = ds1500form.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (bt_1500_Form == "1")
                {
                    string str_1500 = "";
                    String szSourceDir = CmpName + "/" + szCaseID + "/Packet Document/";

                    string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                    Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                    str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, szBillNumber, CmpName, szCaseID, CmpId, conn);

                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }
                    returnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + str_1500;
                    ArrayList objAL = new ArrayList();
                    if (sz_Type == "OLD")
                    {
                        objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL.Add(szDestinationDir + str_1500); // SZ_BILL_PATH
                        objAL.Add(CmpId);
                        objAL.Add(szCaseID);
                        objAL.Add(str_1500); // SZ_BILL_NAME
                        objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                        objAL.Add(UserName);
                        objAL.Add(speciality.Trim());
                        objAL.Add("WC");
                        objAL.Add(CaseNO);
                        objNF3Template.saveGeneratedBillPath(objAL, conn);
                    }
                    else
                    {
                        objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL.Add(szDestinationDir + str_1500); // SZ_BILL_PATH
                        objAL.Add(CmpId);
                        objAL.Add(szCaseID);
                        objAL.Add(str_1500); // SZ_BILL_NAME
                        objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                        objAL.Add(UserName);
                        objAL.Add(speciality.Trim());
                        objAL.Add("WC");
                        objAL.Add(CaseNO);
                        objAL.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL, conn);
                    }


                    // Start : Save Notes for Bill.
                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = UserId;
                    _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


                }
                else
                {
                    if (p_szPDFNo == "1")
                    {
                        _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
                        GenerateItextPdf objGenerateItextPdf = new GenerateItextPdf();
                        String szDefaultPhysicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/";
                        PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                        if (CmpBit == 1)
                        {
                            xmlFilePath_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_P1"].ToString();
                        }
                        else
                        {
                            xmlFilePath_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_P1"].ToString();
                        }
                        string PdfPath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P1"].ToString();
                        //string strGenFileName = _pDFValueReplacement.ReplacePDFvalues(xmlFilePath_wc5, PdfPath, szBillNumber, CmpName, szCaseID);

                        Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                        Bill_Sys_Data obj1 = new Bill_Sys_Data();
                        //obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, xmlFilePath_wc5, PdfPath, CmpId, szCaseID, CmpName);
                        obj1 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC40Part1(CmpId, szCaseID, szBillNumber, CmpName, conn);
                        string strGenFileName = obj1.billurl;

                        ArrayList objAL = new ArrayList();
                        objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                        objAL.Add(szBillNumber);
                        objAL.Add(szCaseID);
                        objAL.Add(CmpId);
                        _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL, conn);
                        string openPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                        string szNewPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_" + szCaseID + "1.pdf";
                        string szFirstMerge = MergePDF.MergePDFFiles(openPath, szProcCodePathPdf, szNewPath);


                        if (CmpBit == 1)
                        {
                            xmlFilePath1_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_P2"].ToString();
                        }
                        else
                        {
                            xmlFilePath1_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_P2"].ToString();
                        }
                        string PdfPath1 = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P2"].ToString();
                        //string strGenFileName1 = _pDFValueReplacement.ReplacePDFvalues(xmlFilePath1_wc5, PdfPath1, szBillNumber, CmpName, szCaseID);

                        //Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                        Bill_Sys_Data obj2 = new Bill_Sys_Data();
                        // obj2 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, xmlFilePath1_wc5, PdfPath1, CmpId, szCaseID, CmpName);
                        obj2 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC40Part2(CmpId, szCaseID, szBillNumber, CmpName, conn);
                        string strGenFileName1 = obj2.billurl;

                        ArrayList objAL1 = new ArrayList();
                        objAL1.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName1);
                        objAL1.Add(szBillNumber);
                        objAL1.Add(szCaseID);
                        objAL1.Add(CmpId);
                        _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL1, conn);
                        string openPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName1;
                        string szNewPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_" + szCaseID + "2.pdf";
                        // string szcopyto = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/No Fault File/" + "Bills/" + speciality.Trim() + "/";
                        string szcopyto = _bill_Sys_NF3_Template.getPhysicalPath() + szDestinationDir;
                        // PathOpen =  ApplicationSettings.GetParameterValue("DocumentManagerURL") +CmpName + "/" + szCaseID + "/No Fault File/" + "Bills/"+speciality.Trim()+"/";
                        PathOpen = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir;
                        string szSecondMerge = MergePDF.MergePDFFiles(szNewPath, openPath1, szNewPath1);

                        if (File.Exists(szNewPath1))
                        {
                            if (!Directory.Exists(szcopyto))
                            {
                                Directory.CreateDirectory(szcopyto);
                            }
                            File.Copy(szNewPath1, szcopyto + szBillNumber + "_MRG.pdf", true);
                        }
                        string szFinalPath = PathOpen + szBillNumber + "_MRG.pdf";


                        //Tushar  : To Mearge MUV Billing PDF
                        string bt_CaseType, bt_include, str_1500;
                        _MUVGenerateFunction = new MUVGenerateFunction();
                        string prefix = speciality.Substring(0, 2);
                        string PrcoedureGroup;
                        if (prefix.ToString().Equals("PR"))
                        {
                            PrcoedureGroup = _bill_Sys_NF3_Template.getGroup(szBillNumber, conn);
                        }
                        else
                        {
                            PrcoedureGroup = speciality;
                        }
                        bt_include = _MUVGenerateFunction.get_bt_include(CmpId, PrcoedureGroup, "", "Speciality");
                        bt_CaseType = _MUVGenerateFunction.get_bt_include(CmpId, "", "WC000000000000000001", "CaseType");
                        if (bt_include == "True" && bt_CaseType == "True")
                        {
                            Session["TM_SZ_CASE_ID"] = szCaseID;
                            str_1500 = _MUVGenerateFunction.FillPdf(szBillNumber.ToString(), conn);

                            MergePDF.MergePDFFiles(szcopyto + szBillNumber + "_MRG.pdf", objNF3Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + str_1500, szcopyto + szBillNumber + "_MRG.pdf");

                            //returnPath =  ApplicationSettings.GetParameterValue("DocumentManagerURL") + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_MRG.pdf";
                        }
                        //End Of Code



                        //  @SZ_BILL_NUMBER='RR463',@SZ_BILL_PATH='IDF Diagnostic Facility/3211/No Fault File/Bills/MRI/RR463_9967_201007191920522052.pdf',@SZ_BILL_NAME='RR463_9967_201007191920522052.pdf',@SZ_USER_NAME='mri',@SZ_SPECIALITY='MRI',@SZ_COMPANY_ID='CO000000000000000043',@SZ_CASE_ID='57',@SZ_BILL_FILE_PATH='IDF Diagnostic Facility/3211/No Fault File/Bills/MRI/',@CASE_TYPE='NF'    

                        ArrayList objAL2 = new ArrayList();
                        // changes for Doc manager for News Bill Path-- pravin

                        if (sz_Type == "OLD")
                        {
                            objAL2.Add(szBillNumber); // SZ_BILL_NUMBER
                            objAL2.Add(szDestinationDir + szBillNumber + "_MRG.pdf"); // SZ_BILL_PATH
                            objAL2.Add(CmpId);
                            objAL2.Add(szCaseID);
                            objAL2.Add(szBillNumber + "_MRG.pdf"); // SZ_BILL_NAME
                            objAL2.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                            objAL2.Add(UserName);
                            objAL2.Add(speciality.Trim());
                            objAL2.Add("WC");
                            objAL2.Add(CaseNO);
                            objNF3Template.saveGeneratedBillPath(objAL2, conn);
                        }
                        else
                        {
                            objAL2.Add(szBillNumber); // SZ_BILL_NUMBER
                            objAL2.Add(szDestinationDir + szBillNumber + "_MRG.pdf"); // SZ_BILL_PATH
                            objAL2.Add(CmpId);
                            objAL2.Add(szCaseID);
                            objAL2.Add(szBillNumber + "_MRG.pdf"); // SZ_BILL_NAME
                            objAL2.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                            objAL2.Add(UserName);
                            objAL2.Add(speciality.Trim());
                            objAL2.Add("WC");
                            objAL2.Add(CaseNO);
                            objAL2.Add(arrNf_NodeType[0].ToString());
                            objNF3Template.saveGeneratedBillPath_New(objAL2, conn);
                        }



                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = strGenFileName;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = UserId;
                        _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                        returnPath = szFinalPath;



                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);
                    }
                    if (p_szPDFNo == "2")
                    {

                        log4net.Config.XmlConfigurator.Configure();
                        log4net.Config.XmlConfigurator.Configure();
                        GenerateItextPdf objGenerateItextPdf = new GenerateItextPdf();
                        String szDefaultPhysicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/";
                        PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                        if (CmpBit == 1)
                        {
                            try
                            {
                                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"] != null && System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                                {
                                    xmlFilePath_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1_SEC"].ToString();
                                    log.Debug("atul " + ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1_SEC"].ToString());
                                }
                                else
                                {
                                    xmlFilePath_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString();
                                    log.Debug("atul " + ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message, ex);
                            }
                        }
                        else
                        {
                            try
                            {
                                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"] != null && System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                                {
                                    xmlFilePath_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1_SEC"].ToString();
                                    log.Debug("atul " + ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1_SEC"].ToString());
                                }
                                else
                                {
                                    xmlFilePath_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString();
                                    log.Debug("atul " + ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P1"].ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message, ex);
                            }
                        }
                        string PdfPath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_2_P1"].ToString();
                        //string strGenFileName = _pDFValueReplacement.ReplacePDFvalues(xmlFilePath_wc5, PdfPath, szBillNumber, CmpName, szCaseID);

                        Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                        Bill_Sys_Data obj1 = new Bill_Sys_Data();
                        //obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, xmlFilePath_wc5, PdfPath, CmpId, szCaseID, CmpName);
                        obj1 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC42Part1(CmpId, szCaseID, szBillNumber, CmpName, conn);
                        string strGenFileName = obj1.billurl;

                        log.Debug("atul " + strGenFileName);
                        ArrayList objAL = new ArrayList();
                        objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                        objAL.Add(szBillNumber);
                        objAL.Add(szCaseID);
                        objAL.Add(CmpId);
                        _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL, conn);
                        string openPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                        log.Debug("openPath " + openPath);
                        string szNewPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_" + szCaseID + "1.pdf";
                        log.Debug("szNewPath " + szNewPath);
                        string szFirstMerge = MergePDF.MergePDFFiles(openPath, szProcCodePathPdf, szNewPath);
                        if (CmpBit == 1)
                        {
                            xmlFilePath1_wc5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P2"].ToString();
                        }
                        else
                        {
                            xmlFilePath1_wc5 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_2_P2"].ToString();
                        }
                        string PdfPath1 = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_2_P2"].ToString();
                        log.Debug("xmlFilePath1_wc5 " + xmlFilePath1_wc5);
                        //string strGenFileName1 = _pDFValueReplacement.ReplacePDFvalues(xmlFilePath1_wc5, PdfPath1, szBillNumber, CmpName, szCaseID);

                        // Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                        Bill_Sys_Data obj2 = new Bill_Sys_Data();
                        // obj2 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, xmlFilePath1_wc5, PdfPath1, CmpId, szCaseID, CmpName);
                        obj2 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC42Part2(CmpId, szCaseID, szBillNumber, CmpName, conn);
                        string strGenFileName1 = obj2.billurl;

                        log.Debug("strGenFileName1 " + strGenFileName1);
                        ArrayList objAL1 = new ArrayList();
                        objAL1.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName1);
                        objAL1.Add(szBillNumber);
                        objAL1.Add(szCaseID);
                        objAL1.Add(CmpId);
                        _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL1, conn);
                        string openPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName1;
                        log.Debug("openPath1 " + openPath1);
                        string szNewPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_" + szCaseID + "2.pdf";
                        log.Debug("szNewPath1 " + szNewPath1);
                        // string szcopyto = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/No Fault File/" + "Bills/" + speciality.Trim() + "/";
                        string szcopyto = _bill_Sys_NF3_Template.getPhysicalPath() + szDestinationDir;
                        log.Debug("szcopyto " + szcopyto);
                        // PathOpen =  ApplicationSettings.GetParameterValue("DocumentManagerURL") + CmpName + "/" + szCaseID + "/No Fault File/" + "Bills/" + speciality.Trim() + "/";
                        PathOpen = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir;
                        log.Debug("PathOpen " + PathOpen);
                        string szSecondMerge = MergePDF.MergePDFFiles(szNewPath, openPath1, szNewPath1);
                        log.Debug("szNewPath1 " + szNewPath1);
                        if (File.Exists(szNewPath1))
                        {
                            if (!Directory.Exists(szcopyto))
                            {
                                Directory.CreateDirectory(szcopyto);
                            }
                            log.Debug("szNewPath1 " + szcopyto + szBillNumber + "_MRG.pdf");
                            File.Copy(szNewPath1, szcopyto + szBillNumber + "_MRG.pdf", true);
                        }
                        string szFinalPath = PathOpen + szBillNumber + "_MRG.pdf";

                        //Tushar  : To Mearge MUV Billing PDF
                        string bt_CaseType, bt_include, str_1500;
                        _MUVGenerateFunction = new MUVGenerateFunction();
                        string prefix = speciality.Substring(0, 2);
                        string PrcoedureGroup;
                        if (prefix.ToString().Equals("PR"))
                        {
                            PrcoedureGroup = _bill_Sys_NF3_Template.getGroup(szBillNumber, conn);
                        }
                        else
                        {
                            PrcoedureGroup = speciality;
                        }
                        bt_include = _MUVGenerateFunction.get_bt_include(CmpId, PrcoedureGroup, "", "Speciality");
                        bt_CaseType = _MUVGenerateFunction.get_bt_include(CmpId, "", "WC000000000000000001", "CaseType");
                        if (bt_include == "True" && bt_CaseType == "True")
                        {
                            Session["TM_SZ_CASE_ID"] = szCaseID;
                            str_1500 = _MUVGenerateFunction.FillPdf(szBillNumber.ToString(), conn);
                            MergePDF.MergePDFFiles(szcopyto + szBillNumber + "_MRG.pdf", objNF3Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + str_1500, szcopyto + szBillNumber + "_MRG.pdf");

                            //szFinalPath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_MRG.pdf";
                        }
                        //End Of Code
                        ArrayList objAL3 = new ArrayList();
                        // changes for Doc manager for News Bill Path-- pravin
                        if (sz_Type == "OLD")
                        {

                            objAL3.Add(szBillNumber); // SZ_BILL_NUMBER
                            objAL3.Add(szDestinationDir + szBillNumber + "_MRG.pdf"); // SZ_BILL_PATH
                            objAL3.Add(CmpId);
                            objAL3.Add(szCaseID);
                            objAL3.Add(szBillNumber + "_MRG.pdf"); // SZ_BILL_NAME
                            objAL3.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                            objAL3.Add(UserName);
                            objAL3.Add(speciality.Trim());
                            objAL3.Add("WC");
                            objAL3.Add(CaseNO);
                            objNF3Template.saveGeneratedBillPath(objAL3, conn);
                        }
                        else
                        {
                            objAL3.Add(szBillNumber); // SZ_BILL_NUMBER
                            objAL3.Add(szDestinationDir + szBillNumber + "_MRG.pdf"); // SZ_BILL_PATH
                            objAL3.Add(CmpId);
                            objAL3.Add(szCaseID);
                            objAL3.Add(szBillNumber + "_MRG.pdf"); // SZ_BILL_NAME
                            objAL3.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                            objAL3.Add(UserName);
                            objAL3.Add(speciality.Trim());
                            objAL3.Add("WC");
                            objAL3.Add(CaseNO);
                            objAL3.Add(arrNf_NodeType[0].ToString());
                            objNF3Template.saveGeneratedBillPath_New(objAL3, conn);
                        }


                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = strGenFileName;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = UserId;
                        _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                        returnPath = szFinalPath;
                        returnPath = szFinalPath;
                        //http://localhost/MBSScans/Blue%20Ridge%20Medical/1321/No%20Fault%20File/Bills/Physiotheropy/sa39_MRG.pdf
                        //http://localhost/MBSScans/Blue Ridge Medical/1321/No Fault File/Bills/Physiotheropy /sa39_MRG.pdf
                        //  ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);
                    }
                    if (p_szPDFNo == "4")  //Add For Greater than count 6--sunil
                    {
                        string strGenFileName = "";
                        if (CmpBit == 1)
                        {

                        }
                        else
                        {
                            Bill_Sys_OCT_Bills obj_OCT = new Bill_Sys_OCT_Bills();
                            string sz_Bill_ID = szBillNumber;
                            DataSet ds1 = new DataSet();
                            DataSet ds_table = new DataSet();
                            ds1 = obj_OCT.GET_OCT_Bills_detail(sz_Bill_ID, conn);
                            ds_table = obj_OCT.GET_OCT_Bills_Table(sz_Bill_ID, conn);
                            DataTable dtoptpt = new DataTable();
                            dtoptpt.Columns.Add("MONTH");
                            dtoptpt.Columns.Add("DAY");
                            dtoptpt.Columns.Add("YEAR");
                            dtoptpt.Columns.Add("TO_MONTH");
                            dtoptpt.Columns.Add("TO_DAY");
                            dtoptpt.Columns.Add("TO_YEAR");
                            dtoptpt.Columns.Add("SZ_PROCEDURE_CODE");
                            dtoptpt.Columns.Add("SZ_DIGNOSIS");
                            dtoptpt.Columns.Add("DC_1");
                            dtoptpt.Columns.Add("DC_2");
                            dtoptpt.Columns.Add("DC_3");
                            dtoptpt.Columns.Add("DC_4");
                            dtoptpt.Columns.Add("DC_5");
                            dtoptpt.Columns.Add("DC_6");
                            dtoptpt.Columns.Add("FL_AMOUNT");
                            dtoptpt.Columns.Add("I_UNIT");
                            dtoptpt.Columns.Add("BILL_AMOUNT");
                            dtoptpt.Columns.Add("PAID_AMOUNT");
                            dtoptpt.Columns.Add("BALANCE");
                            dtoptpt.Columns.Add("ZIP_CODE");
                            dtoptpt.Columns.Add("PLACE_OF_SERVICE");

                            ArrayList arrPaths = new ArrayList();
                            ArrayList arrNewPAth = new ArrayList();
                            int iFlag = 1;
                            int count = -1;
                            for (int i = 0; i < ds_table.Tables[0].Rows.Count; i++)
                            {
                                if (count == -1)
                                {
                                    dtoptpt.Clear();
                                }
                                DataRow drotpt = dtoptpt.NewRow();
                                drotpt["MONTH"] = ds_table.Tables[0].Rows[i]["MONTH"].ToString();
                                drotpt["DAY"] = ds_table.Tables[0].Rows[i]["DAY"].ToString();
                                drotpt["YEAR"] = ds_table.Tables[0].Rows[i]["YEAR"].ToString();
                                drotpt["TO_MONTH"] = ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString();
                                drotpt["TO_DAY"] = ds_table.Tables[0].Rows[i]["TO_DAY"].ToString();
                                drotpt["TO_YEAR"] = ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString();
                                drotpt["SZ_PROCEDURE_CODE"] = ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString();
                                drotpt["SZ_DIGNOSIS"] = ds_table.Tables[0].Rows[i]["SZ_DIGNOSIS"].ToString();
                                drotpt["DC_1"] = ds_table.Tables[0].Rows[i]["DC_1"].ToString();
                                drotpt["DC_2"] = ds_table.Tables[0].Rows[i]["DC_2"].ToString();
                                drotpt["DC_3"] = ds_table.Tables[0].Rows[i]["DC_3"].ToString();
                                drotpt["DC_4"] = ds_table.Tables[0].Rows[i]["DC_4"].ToString();
                                drotpt["DC_5"] = ds_table.Tables[0].Rows[i]["DC_5"].ToString();
                                drotpt["DC_6"] = ds_table.Tables[0].Rows[i]["DC_6"].ToString();
                                drotpt["FL_AMOUNT"] = ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString();
                                drotpt["I_UNIT"] = ds_table.Tables[0].Rows[i]["I_UNIT"].ToString();
                                drotpt["BILL_AMOUNT"] = ds_table.Tables[0].Rows[i]["BILL_AMOUNT"].ToString();
                                drotpt["PAID_AMOUNT"] = ds_table.Tables[0].Rows[i]["PAID_AMOUNT"].ToString();
                                drotpt["BALANCE"] = ds_table.Tables[0].Rows[i]["BALANCE"].ToString();
                                drotpt["ZIP_CODE"] = ds_table.Tables[0].Rows[i]["ZIP_CODE"].ToString();
                                drotpt["PLACE_OF_SERVICE"] = ds_table.Tables[0].Rows[i]["PLACE_OF_SERVICE"].ToString();
                                dtoptpt.Rows.Add(drotpt);
                                count++;

                                if (count >= 5 && i != ds_table.Tables[0].Rows.Count - 1)
                                {
                                    string pdfFilePathnew = ConfigurationManager.AppSettings["PDF_FILE_PATH_OCT_1"].ToString();
                                    count = -1;
                                    DataSet dscountgrester = new DataSet();
                                    dscountgrester.Tables.Add(dtoptpt.Copy());
                                    string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                                    string szPdFIndex = i.ToString();
                                    string szNewPdfPath = Getotptpdfchangefield("page_" + szPdFIndex, dscountgrester, ds1, szBillNumber, szCaseID, CmpId, CmpName, pdfFilePathnew);
                                    arrNewPAth.Add(szURLDocumentManager_OCT + "/" + szNewPdfPath);

                                }
                                else if (i == ds_table.Tables[0].Rows.Count - 1)
                                {
                                    string pdfFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_OCT"].ToString();
                                    count = -1;
                                    DataSet dscountgrester = new DataSet();
                                    string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                                    dscountgrester.Tables.Add(dtoptpt.Copy());
                                    string szPdFIndex = i.ToString();
                                    string szNewPdfPath = Getotptpdf("page_" + szPdFIndex, dscountgrester, ds1, szBillNumber, szCaseID, CmpId, CmpName, pdfFilePath);
                                    arrNewPAth.Add(szURLDocumentManager_OCT + "/" + szNewPdfPath);

                                }
                            }

                            string szNewFile = arrNewPAth[0].ToString();

                            for (int i = 0; i < arrNewPAth.Count - 1; i++)
                            {

                                string szURLDocumentManager_OCT = ConfigurationManager.AppSettings["CREATED_PDF_FILE_PATH"].ToString();
                                strGenFileName = szBillNumber + "_" + szCaseID + "_" + i.ToString() + ".pdf";
                                string NewPdfPath = szURLDocumentManager_OCT + "/" + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                                string szFirstMerge = MergePDF.MergePDFFiles(szNewFile, arrNewPAth[i + 1].ToString(), NewPdfPath);
                                szNewFile = NewPdfPath;

                            }

                            string szFinalOtptPdf = szNewFile;
                            string sztimepath = DateTime.Now.ToString("MMddyyyyhhmmss");
                            string szcopyto = _bill_Sys_NF3_Template.getPhysicalPath() + szDestinationDir;
                            if (File.Exists(szNewFile))
                            {
                                if (!Directory.Exists(szcopyto))
                                {
                                    Directory.CreateDirectory(szcopyto);
                                }
                                log.Debug("szNewPath1 " + szcopyto + szBillNumber + "_" + sztimepath + "_1MRG.pdf");
                                File.Copy(szNewFile, szcopyto + szBillNumber + "_" + sztimepath + "_1MRG.pdf", true);
                            }
                            PathOpen = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir;
                            string szFinalPath = PathOpen + szBillNumber + "_" + sztimepath + "_1MRG.pdf";



                            //Tushar  : To Mearge MUV Billing PDF
                            string bt_CaseType, bt_include, str_1500;
                            _MUVGenerateFunction = new MUVGenerateFunction();
                            string prefix = speciality.Substring(0, 2);
                            string PrcoedureGroup;
                            if (prefix.ToString().Equals("PR"))
                            {
                                PrcoedureGroup = _bill_Sys_NF3_Template.getGroup(szBillNumber, conn);
                            }
                            else
                            {
                                PrcoedureGroup = speciality;
                            }
                            bt_include = _MUVGenerateFunction.get_bt_include(CmpId, PrcoedureGroup, "", "Speciality");
                            bt_CaseType = _MUVGenerateFunction.get_bt_include(CmpId, "", "WC000000000000000001", "CaseType");
                            if (bt_include == "True" && bt_CaseType == "True")
                            {
                                Session["TM_SZ_CASE_ID"] = szCaseID;
                                str_1500 = _MUVGenerateFunction.FillPdf(szBillNumber.ToString(), conn);
                                MergePDF.MergePDFFiles(szcopyto + szBillNumber + "_" + sztimepath + "_1MRG.pdf", objNF3Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + str_1500, szcopyto + szBillNumber + "_" + sztimepath + "_1MRG.pdf");

                                //szFinalPath =  ApplicationSettings.GetParameterValue("DocumentManagerURL") + CmpName + "/" + szCaseID + "/Packet Document/" + szBillNumber + "_MRG.pdf";
                            }
                            //End Of Code
                            ArrayList objAL3 = new ArrayList();
                            // changes for Doc manager for News Bill Path-- pravin
                            if (sz_Type == "OLD")
                            {

                                objAL3.Add(szBillNumber); // SZ_BILL_NUMBER
                                objAL3.Add(szDestinationDir + szBillNumber + "_" + sztimepath + "_1MRG.pdf"); // SZ_BILL_PATH
                                objAL3.Add(CmpId);
                                objAL3.Add(szCaseID);
                                objAL3.Add(szBillNumber + "_" + sztimepath + "_1MRG.pdf"); // SZ_BILL_NAME
                                objAL3.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                                objAL3.Add(UserName);
                                objAL3.Add(speciality.Trim());
                                objAL3.Add("WC");
                                objAL3.Add(CaseNO);
                                objNF3Template.saveGeneratedBillPath(objAL3, conn);
                            }
                            else
                            {
                                objAL3.Add(szBillNumber); // SZ_BILL_NUMBER
                                objAL3.Add(szDestinationDir + szBillNumber + "_" + sztimepath + "_1MRG.pdf"); // SZ_BILL_PATH
                                objAL3.Add(CmpId);
                                objAL3.Add(szCaseID);
                                objAL3.Add(szBillNumber + "_" + sztimepath + "_1MRG.pdf"); // SZ_BILL_NAME
                                objAL3.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                                objAL3.Add(UserName);
                                objAL3.Add(speciality.Trim());
                                objAL3.Add("WC");
                                objAL3.Add(CaseNO);
                                objAL3.Add(arrNf_NodeType[0].ToString());
                                objNF3Template.saveGeneratedBillPath_New(objAL3, conn);
                            }


                            _DAO_NOTES_EO = new DAO_NOTES_EO();
                            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = strGenFileName;

                            _DAO_NOTES_BO = new DAO_NOTES_BO();
                            _DAO_NOTES_EO.SZ_USER_ID = UserId;
                            _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                            _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                            returnPath = szFinalPath;


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                //  Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
            }
        }
        else
        {
            String szURLDocumentManager = "";
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            objNF3Template = new Bill_Sys_NF3_Template();
            objCaseDetailsBO = new CaseDetailsBO();
            szURLDocumentManager = ApplicationSettings.GetParameterValue("DocumentManagerURL");
            String szDefaultPhysicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/";
            string strGenFileName = "";
            try
            {           //changes for Add only 1500 Form For Insurance Company -- pravin

                ds1500form = objCaseDetailsBO.Get1500FormBitForInsurance(CmpId, szBillNumber, conn);
                if (ds1500form.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1500form.Tables[0].Rows.Count; i++)
                    {
                        bt_1500_Form = ds1500form.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (bt_1500_Form == "1")
                {
                    String szSourceDir = CmpName + "/" + szCaseID + "/Packet Document/";
                    string str_1500 = "";


                    string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                    Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                    str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, szBillNumber, CmpName, szCaseID, CmpId, conn);

                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }
                    returnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + str_1500;
                    ArrayList objAL = new ArrayList();
                    if (sz_Type == "OLD")
                    {
                        objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL.Add(szDestinationDir + str_1500); // SZ_BILL_PATH
                        objAL.Add(CmpId);
                        objAL.Add(szCaseID);
                        objAL.Add(str_1500); // SZ_BILL_NAME
                        objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                        objAL.Add(UserName);
                        objAL.Add(speciality.Trim());
                        objAL.Add("WC");
                        objAL.Add(CaseNO);
                        objNF3Template.saveGeneratedBillPath(objAL, conn);
                    }
                    else
                    {
                        objAL.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL.Add(szDestinationDir + str_1500); // SZ_BILL_PATH
                        objAL.Add(CmpId);
                        objAL.Add(szCaseID);
                        objAL.Add(str_1500); // SZ_BILL_NAME
                        objAL.Add(szDestinationDir); // SZ_BILL_FILE_PATH
                        objAL.Add(UserName);
                        objAL.Add(speciality.Trim());
                        objAL.Add("WC");
                        objAL.Add(CaseNO);
                        objAL.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL, conn);
                    }


                    // Start : Save Notes for Bill.
                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = UserId;
                    _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


                }
                else
                {
                    if (p_szPDFNo == "1")
                    {
                        PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                        string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
                        if (CmpBit == 1)
                        {
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName, conn);
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }
                        else
                        {
                            log.Debug("In First Pdf");
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            GenerateItextPdf objGenerateItextPdf = new GenerateItextPdf();
                            // obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName);
                            obj1 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC40LessThan5(CmpId, szCaseID, szBillNumber, CmpName, conn);
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }

                        //ArrayList objAL = new ArrayList();
                        //objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                        //objAL.Add(szBillNumber);
                        //objAL.Add(szCaseID);
                        //objAL.Add(CmpId);
                        //_bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
                        string openPath = szURLDocumentManager + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;

                        returnPath = openPath;
                    }

                    if (p_szPDFNo == "2")
                    {
                        PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                        GenerateItextPdf objGenerateItextPdf = new GenerateItextPdf();
                        string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C42"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
                        if (CmpBit == 1)
                        {
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            try
                            {
                                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"] == "Secondary")
                                    obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C42_SEC"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName, conn);
                                else
                                    obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName, conn);
                            }
                            catch
                            { obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName, conn); }
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }
                        else
                        {
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            try
                            {
                                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"] != null && System.Web.HttpContext.Current.Session["GenerateBill_insType"] == "Secondary")
                                    obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42_SEC"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName, conn);
                                else
                                    obj1 = (Bill_Sys_Data)objGenerateItextPdf.GeneratC42LessThan5(CmpId, szCaseID, szBillNumber, CmpName, conn);
                                //obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName);

                            }
                            catch (Exception ex)
                            {/* obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName,conn);*/
                                throw new Exception(ex.Message, ex);
                            }
                            strGenFileName = obj1.billurl;


                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }

                        //ArrayList objAL = new ArrayList();
                        //objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                        //objAL.Add(szBillNumber);
                        //objAL.Add(szCaseID);
                        //objAL.Add(CmpId);
                        //_bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);

                        string openPath = szURLDocumentManager + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;

                        returnPath = openPath;
                    }

                    if (p_szPDFNo == "3")
                    {
                        PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                        string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C43"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";

                        if (CmpBit == 1)
                        {
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName, conn);
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }
                        else
                        {
                            Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                            Bill_Sys_Data obj1 = new Bill_Sys_Data();
                            obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillNumber, ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, CmpId, szCaseID, CmpName, conn);
                            strGenFileName = obj1.billurl;

                            //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, szBillNumber, CmpName, szCaseID);
                        }

                        //ArrayList objAL = new ArrayList();
                        //objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                        //objAL.Add(szBillNumber);
                        //objAL.Add(szCaseID);
                        //objAL.Add(CmpId);
                        //_bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);

                        string openPath = szURLDocumentManager + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;

                        returnPath = openPath;
                    }
                    if (p_szPDFNo == "4")   //Kapil
                    {

                        string ReadpdfFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_OCT"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";

                        if (CmpBit == 1)
                        {

                        }
                        else
                        {
                            Bill_Sys_OCT_Bills obj_OCT = new Bill_Sys_OCT_Bills();
                            string sz_Bill_ID = szBillNumber;
                            DataSet ds1 = new DataSet();
                            DataSet ds_table = new DataSet();
                            ds1 = obj_OCT.GET_OCT_Bills_detail(sz_Bill_ID, conn);
                            ds_table = obj_OCT.GET_OCT_Bills_Table(sz_Bill_ID, conn);

                            strGenFileName = szBillNumber + "_" + szCaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmm") + ".pdf";
                            //strGenFileName=newPdfFilename;
                            //string Dr_Cohen_Electronic_Script = ConfigurationManager.AppSettings["PDF_FILE_PATH_PATIENT_INFORMATION"].ToString();
                            // string pdfPath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString()) + newPdfFilename;
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
                            PdfReader reader = new PdfReader(ReadpdfFilePath);
                            PdfStamper stamper = new PdfStamper(reader, System.IO.File.Create(pdfPath));
                            AcroFields fields = stamper.AcroFields;
                            #region code for generate pdf Kapil
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0]["OCCUPATIONAL THERAPISTS REPORT"].ToString() == "1")
                                {
                                    fields.SetField("OCCUPATIONAL THERAPISTS REPORT", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["OCCUPATIONAL THERAPISTS REPORT"].ToString() == "0")
                                {
                                    fields.SetField("OCCUPATIONAL THERAPISTS REPORT", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["PHYSICAL THERAPISTS REPORT"].ToString() == "1")
                                {
                                    fields.SetField("PHYSICAL THERAPISTS REPORT", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["PHYSICAL THERAPISTS REPORT"].ToString() == "0")
                                {
                                    fields.SetField("PHYSICAL THERAPISTS REPORT", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["BT_PPO"].ToString() == "1")
                                {
                                    fields.SetField("Check Box14", "1");
                                    fields.SetField("Check Box15", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["BT_PPO"].ToString() == "0")
                                {
                                    fields.SetField("Check Box14", "0");
                                    fields.SetField("Check Box15", "1");
                                }


                                if (ds1.Tables[0].Rows[0]["Check Box16"].ToString() == "1")
                                {
                                    fields.SetField("Check Box16", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box16"].ToString() == "0")
                                {
                                    fields.SetField("Check Box16", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box17"].ToString() == "1")
                                {
                                    fields.SetField("Check Box17", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box17"].ToString() == "0")
                                {
                                    fields.SetField("Check Box17", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box18"].ToString() == "1")
                                {
                                    fields.SetField("Check Box18", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box18"].ToString() == "0")
                                {
                                    fields.SetField("Check Box18", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString() == null || ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString() == "")
                                {
                                    fields.SetField("WCB CASE NORow1", "-");
                                }
                                else
                                {
                                    fields.SetField("WCB CASE NORow1", ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString() == null || ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString() == "")
                                {
                                    fields.SetField("CARRIER CASE NO IF KNOWNRow1", "-");
                                }
                                else
                                {
                                    fields.SetField("CARRIER CASE NO IF KNOWNRow1", ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString() == null || ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString() == "")
                                {
                                    fields.SetField("DATE OF INJURYRow1", "-");
                                }
                                else
                                {
                                    fields.SetField("DATE OF INJURYRow1", ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text46"].ToString() == null || ds1.Tables[0].Rows[0]["Text46"].ToString() == "")
                                {
                                    fields.SetField("Text46", "-");
                                }
                                else
                                {
                                    fields.SetField("Text46", ds1.Tables[0].Rows[0]["Text46"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString() == null || ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString() == "")
                                {
                                    fields.SetField("ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1", "-");
                                }
                                else
                                {
                                    fields.SetField("ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1", ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString() == null || ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString() == "")
                                {
                                    fields.SetField("INJURED PERSONS SOCIAL SECURITY NUMBERRow1", "-");
                                }
                                else
                                {
                                    fields.SetField("INJURED PERSONS SOCIAL SECURITY NUMBERRow1", ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString() == null || ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString() == "")
                                {
                                    fields.SetField("INJURED PERSON", "-");
                                }
                                else
                                {
                                    fields.SetField("INJURED PERSON", ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text32"].ToString() == null || ds1.Tables[0].Rows[0]["Text32"].ToString() == "")
                                {
                                    fields.SetField("Text32", "-");
                                }
                                else
                                {
                                    fields.SetField("Text32", ds1.Tables[0].Rows[0]["Text32"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString() == null || ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString() == "")
                                {
                                    fields.SetField("TELEPHONE NO", "-");
                                }
                                else
                                {
                                    fields.SetField("TELEPHONE NO", ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString() == null || ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString() == "")
                                {
                                    fields.SetField("First Name Middle Initial Last NameEMPLOYER", "-");
                                }
                                else
                                {
                                    fields.SetField("First Name Middle Initial Last NameEMPLOYER", ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString() == null || ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString() == "")
                                {
                                    fields.SetField("ADDRESS Include Apt NoEMPLOYER", "-");
                                }
                                else
                                {
                                    fields.SetField("ADDRESS Include Apt NoEMPLOYER", ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString() == null || ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString() == "")
                                {
                                    fields.SetField("PATIENTS DATE OF BIRTH", "-");
                                }
                                else
                                {
                                    fields.SetField("PATIENTS DATE OF BIRTH", ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString());

                                }
                                if (ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString() == null || ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString() == "")
                                {
                                    fields.SetField("INSURANCE CARRIER", "-");
                                }
                                else
                                {
                                    fields.SetField("INSURANCE CARRIER", ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text30"].ToString() == null || ds1.Tables[0].Rows[0]["Text30"].ToString() == "")
                                {
                                    fields.SetField("Text30", "-");
                                }
                                else
                                {
                                    fields.SetField("Text30", ds1.Tables[0].Rows[0]["Text30"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString() == null || ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString() == "")
                                {
                                    fields.SetField("REFERRING PHYSICIAN PODIATRIST", "-");
                                }
                                else
                                {
                                    fields.SetField("REFERRING PHYSICIAN PODIATRIST", ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text31"].ToString() == null || ds1.Tables[0].Rows[0]["Text31"].ToString() == "")
                                {
                                    fields.SetField("Text31", "-");
                                }
                                else
                                {
                                    fields.SetField("Text31", ds1.Tables[0].Rows[0]["Text31"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString() == null || ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString() == "")
                                {
                                    fields.SetField("TELEPHONE NO_2", "-");
                                }
                                else
                                {
                                    fields.SetField("TELEPHONE NO_2", ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString() == "")
                                {
                                    fields.SetField("REFERRING PHYSICIAN PODIATRIST", "-");
                                }
                                else
                                {
                                    fields.SetField("REFERRING PHYSICIAN PODIATRIST", ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString() == "")
                                {
                                    fields.SetField("Text31", "-");
                                }
                                else
                                {
                                    fields.SetField("Text31", ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box19"].ToString() == "1")
                                {
                                    fields.SetField("Check Box19", "0");
                                    fields.SetField("Check Box20", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box19"].ToString() == "0")
                                {
                                    fields.SetField("Check Box19", "1");
                                    fields.SetField("Check Box20", "0");
                                }
                                //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "1")
                                //{
                                //    fields.SetField("Check Box20", "1");
                                //}
                                //else if (ds1.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "0")
                                //{
                                //    fields.SetField("Check Box20", "0");
                                //}
                                if (ds1.Tables[0].Rows[0]["Text43"].ToString() == null || ds1.Tables[0].Rows[0]["Text43"].ToString() == "")
                                {
                                    fields.SetField("Text43", "-");
                                }
                                else
                                {
                                    fields.SetField("Text43", ds1.Tables[0].Rows[0]["Text43"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text33"].ToString() == null || ds1.Tables[0].Rows[0]["Text33"].ToString() == "")
                                {
                                    fields.SetField("Text33", "-");
                                }
                                else
                                {
                                    fields.SetField("Text33", ds1.Tables[0].Rows[0]["Text33"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString() == null || ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString() == "")
                                {
                                    fields.SetField("patient history of preexisting injury disease", "-");
                                }
                                else
                                {
                                    fields.SetField("patient history of preexisting injury disease", ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "0")
                                {
                                    fields.SetField("Check Box34", "1");
                                    fields.SetField("Check Box35", "0");
                                    fields.SetField("Check Box36", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "1")
                                {
                                    fields.SetField("Check Box34", "0");
                                    fields.SetField("Check Box35", "1");
                                    fields.SetField("Check Box36", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "2")
                                {
                                    fields.SetField("Check Box34", "0");
                                    fields.SetField("Check Box35", "0");
                                    fields.SetField("Check Box36", "1");
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box24"].ToString() == "1")
                                {
                                    fields.SetField("Check Box23", "1");
                                    fields.SetField("Check Box24", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box24"].ToString() == "0")
                                {
                                    fields.SetField("Check Box23", "0");
                                    fields.SetField("Check Box24", "1");
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box21"].ToString() == "1")
                                {
                                    fields.SetField("Check Box21", "1");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box21"].ToString() == "0")
                                {
                                    fields.SetField("Check Box21", "0");
                                }
                                if (ds1.Tables[0].Rows[0]["a Your evaluation"].ToString() == null || ds1.Tables[0].Rows[0]["a Your evaluation"].ToString() == "")
                                {
                                    fields.SetField("a Your evaluation", "-");
                                }
                                else
                                {
                                    fields.SetField("a Your evaluation", ds1.Tables[0].Rows[0]["a Your evaluation"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString() == null || ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString() == "")
                                {
                                    fields.SetField("b 1 Patients condition and progress", "-");
                                }
                                else
                                {
                                    fields.SetField("b 1 Patients condition and progress", ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString());
                                }
                                //if (ds1.Tables[0].Rows[0]["Check Box21"].ToString()== "1")
                                //{
                                //    fields.SetField("Check Box21", "1");
                                //}
                                //else if (ds1.Tables[0].Rows[0]["Check Box21"].ToString()== "0")
                                //{
                                //    fields.SetField("Check Box21", "0");
                                //}
                                if (ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString() == null || ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString() == "")
                                {
                                    fields.SetField("Treatment and planned future treatment", "-");
                                }
                                else
                                {
                                    fields.SetField("Treatment and planned future treatment", ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "1")
                                {
                                    fields.SetField("Check Box22", "1");
                                    fields.SetField("Check Box25", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "0")
                                {
                                    fields.SetField("Check Box22", "0");
                                    fields.SetField("Check Box25", "1");
                                }

                                if (ds1.Tables[0].Rows[0]["Text44"].ToString() == null || ds1.Tables[0].Rows[0]["Text44"].ToString() == "")
                                {
                                    fields.SetField("Text44", "-");
                                }
                                else
                                {
                                    fields.SetField("Text44", ds1.Tables[0].Rows[0]["Text44"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text45"].ToString() == null || ds1.Tables[0].Rows[0]["Text45"].ToString() == "")
                                {
                                    fields.SetField("Text45", "-");
                                }
                                else
                                {
                                    fields.SetField("Text45", ds1.Tables[0].Rows[0]["Text45"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString() == null || ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString() == "")
                                {
                                    fields.SetField("4 Dates of visits on which this report is based", "-");
                                }
                                else
                                {
                                    fields.SetField("4 Dates of visits on which this report is based", ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString() == null || ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString() == "")
                                {
                                    fields.SetField("If no was patient referred back to attending doctor D Yes D No", "-");
                                }
                                else
                                {
                                    fields.SetField("If no was patient referred back to attending doctor D Yes D No", ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box26"].ToString() == "1")
                                {
                                    fields.SetField("Check Box26", "1");
                                    fields.SetField("Check Box27", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box26"].ToString() == "0")
                                {
                                    fields.SetField("Check Box26", "0");
                                    fields.SetField("Check Box27", "1");
                                }
                                if (ds1.Tables[0].Rows[0]["H_2"].ToString() == null || ds1.Tables[0].Rows[0]["H_2"].ToString() == "")
                                {
                                    fields.SetField("H_2", "-");
                                }
                                else
                                {
                                    fields.SetField("H_2", ds1.Tables[0].Rows[0]["H_2"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box28"].ToString() == "1")
                                {
                                    fields.SetField("Check Box28", "1");
                                    fields.SetField("Check Box29", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box28"].ToString() == "0")
                                {
                                    fields.SetField("Check Box28", "0");
                                    fields.SetField("Check Box29", "1");
                                }
                                if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "1")
                                {
                                    fields.SetField("Check Box22", "1");
                                    fields.SetField("Check Box25", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "0")
                                {
                                    fields.SetField("Check Box22", "0");
                                    fields.SetField("Check Box25", "1");
                                }
                                if (ds1.Tables[0].Rows[0]["Text37"].ToString() == null || ds1.Tables[0].Rows[0]["Text37"].ToString() == "")
                                {
                                    fields.SetField("Text37", "-");
                                }
                                else
                                {
                                    fields.SetField("Text37", ds1.Tables[0].Rows[0]["Text37"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["Text38"].ToString() == null || ds1.Tables[0].Rows[0]["Text38"].ToString() == "")
                                {
                                    fields.SetField("Text38", "-");
                                }
                                else
                                {
                                    fields.SetField("Text38", ds1.Tables[0].Rows[0]["Text38"].ToString());
                                }
                                //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(51).ToString() == null || ds1.Tables[0].Rows[0].ItemArray.GetValue(51).ToString() == "")
                                //{
                                //    fields.SetField("1", "-");
                                //}
                                //else
                                //{
                                //    fields.SetField("1", ds1.Tables[0].Rows[0].ItemArray.GetValue(51).ToString());
                                //}
                                //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(52).ToString() == null || ds1.Tables[0].Rows[0].ItemArray.GetValue(52).ToString() == "")
                                //{
                                //    fields.SetField("2", "-");
                                //}
                                //else
                                //{
                                //    fields.SetField("2", ds1.Tables[0].Rows[0].ItemArray.GetValue(52).ToString());
                                //}
                                //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(53).ToString() == null || ds1.Tables[0].Rows[0].ItemArray.GetValue(53).ToString() == "")
                                //{
                                //    fields.SetField("3", "-");
                                //}
                                //else
                                //{
                                //    fields.SetField("3", ds1.Tables[0].Rows[0].ItemArray.GetValue(53).ToString());
                                //}
                                //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(54).ToString() == null || ds1.Tables[0].Rows[0].ItemArray.GetValue(54).ToString() == "")
                                //{
                                //    fields.SetField("4", "-");
                                //}
                                //else
                                //{
                                //    fields.SetField("4", ds1.Tables[0].Rows[0].ItemArray.GetValue(54).ToString());
                                //}
                                if (ds1.Tables[0].Rows[0]["Check Box40"].ToString() == "True")
                                {
                                    fields.SetField("Check Box40", "1");
                                    fields.SetField("Check Box41", "0");
                                }
                                else if (ds1.Tables[0].Rows[0]["Check Box40"].ToString() == "False")
                                {
                                    fields.SetField("Check Box40", "0");
                                    fields.SetField("Check Box41", "1");
                                }
                                //if (ds1.Tables[0].Rows[0]["Check Box41"].ToString()== "1")
                                //{
                                //    fields.SetField("Check Box41", "1");
                                //}
                                //else if (ds1.Tables[0].Rows[0]["Check Box41"].ToString()== "0")
                                //{
                                //    fields.SetField("Check Box41", "0");
                                //}
                                if (ds1.Tables[0].Rows[0]["Text39"].ToString() == null || ds1.Tables[0].Rows[0]["Text39"].ToString() == "")
                                {
                                    fields.SetField("TELEPHONE NO_2", "-");
                                }
                                else
                                {
                                    fields.SetField("TELEPHONE NO_2", ds1.Tables[0].Rows[0]["Text39"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString() == null || ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString() == "")
                                {
                                    fields.SetField("9 NYS License Number", "-");
                                }
                                else
                                {
                                    fields.SetField("9 NYS License Number", ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString() == "")
                                {
                                    fields.SetField("Text39", "-");
                                }
                                else
                                {
                                    fields.SetField("Text39", ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString() == null || ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString() == "")
                                {
                                    fields.SetField("10 Patients Account Number", "-");
                                }
                                else
                                {
                                    fields.SetField("10 Patients Account Number", ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString() == null || ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString() == "")
                                {
                                    fields.SetField("WCB CASE NORow1", "-");
                                }
                                else
                                {
                                    fields.SetField("WCB CASE NORow1", ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString() == "")
                                {
                                    fields.SetField("15 Therapists Name Address  Phone No", "-");
                                }
                                else
                                {
                                    fields.SetField("15 Therapists Name Address  Phone No", ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == "")
                                {
                                    fields.SetField("16 Therapists Billing Name Address  Phone No", "-");
                                }
                                else
                                {
                                    fields.SetField("16 Therapists Billing Name Address  Phone No", ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == "")
                                {
                                    fields.SetField("txtcheck", "-");
                                }
                                else
                                {
                                    fields.SetField("txtcheck", ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString());
                                }
                                if (ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == "")
                                {
                                    fields.SetField("txtcheck1", "-");
                                }
                                else
                                {
                                    fields.SetField("txtcheck1", ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString());
                                }
                                if (ds_table.Tables[0].Rows.Count > 0)
                                {
                                    string Fmmtext = "FmmText";
                                    string FddText = "FddText";
                                    string FyyText = "FyyText";
                                    string TmmText = "TmmText";
                                    string TddText = "TddText";
                                    string TyyText = "TyyText";
                                    string BText = "BText";
                                    string DCText = "DCText";
                                    string DMText = "DMText";
                                    //string EText = "EText";
                                    string EText = "DC";
                                    string FText = "FText";
                                    string GText = "GText";
                                    string HText = "HText";
                                    string IText = "IText";
                                    for (int i = 0; i < ds_table.Tables[0].Rows.Count; i++)
                                    {
                                        //fields.SetField(Fmmtext+i.ToString(), ds_table.Tables[0].Rows[i].ItemArray.GetValue("MONTH").ToString()); 
                                        fields.SetField(Fmmtext + i.ToString(), ds_table.Tables[0].Rows[i]["MONTH"].ToString());

                                        fields.SetField(FddText + i.ToString(), ds_table.Tables[0].Rows[i]["DAY"].ToString());

                                        fields.SetField(FyyText + i.ToString(), ds_table.Tables[0].Rows[i]["YEAR"].ToString());

                                        fields.SetField(TmmText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString());

                                        fields.SetField(TddText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_DAY"].ToString());

                                        fields.SetField(TyyText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString());

                                        fields.SetField(BText + i.ToString(), ds_table.Tables[0].Rows[i]["PLACE_OF_SERVICE"].ToString());

                                        fields.SetField(DCText + i.ToString(), ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());

                                        //fields.SetField(DMText+i.ToString(), ds_table.Tables[0].Rows[i].ItemArray.GetValue("").ToString()); 
                                        fields.SetField(EText + i.ToString(), ds_table.Tables[0].Rows[i]["DC_1"].ToString());

                                        fields.SetField(FText + i.ToString(), ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString());

                                        fields.SetField(GText + i.ToString(), ds_table.Tables[0].Rows[i]["I_UNIT"].ToString());

                                        //fields.SetField(HText+i.ToString(), ds_table.Tables[0].Rows[i].ItemArray.GetValue("I_UNIT").ToString()); 

                                        fields.SetField(IText + i.ToString(), ds_table.Tables[0].Rows[i]["ZIP_CODE"].ToString());
                                    }
                                    fields.SetField("11 Total Charges", ds_table.Tables[0].Rows[0]["BILL_AMOUNT"].ToString());
                                    fields.SetField("12.Amt.Paid", ds_table.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
                                    fields.SetField("13.Balance", ds_table.Tables[0].Rows[0]["BALANCE"].ToString());
                                    fields.SetField("1", ds_table.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString());
                                }

                                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                                string[] u = url.Split('/');
                                string str = "";
                                for (int i = 0; i < u.Length; i++)
                                {
                                    if (i < 3)
                                    {
                                        if (i == 0)
                                        {
                                            str = u[i];
                                        }
                                        else
                                            str = str + "/" + u[i];
                                    }
                                }

                                stamper.Close();
                                //string szPath =  ApplicationSettings.GetParameterValue("DocumentManagerURL") + GetCompanyName(txtCompanyID.Text) + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Packet Document/" + newPdfFilename;
                                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szPath + "');", true);
                            }

                            #endregion
                            string openPath = szURLDocumentManager + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                            returnPath = openPath;

                        }
                    }
                    // Copy generated file from Packet Document to WC File.
                    objNF3Template = new Bill_Sys_NF3_Template();

                    String szBasePhysicalPath = objNF3Template.getPhysicalPath();
                    //changes for Doc Manager for New Bill Path -- pravin
                    //  String szNewPath = CmpName + "/" + szCaseID + "/No Fault File/Bills/" + speciality.Trim() + "/";
                    String szNewPath = szDestinationDir;
                    if (File.Exists(szBasePhysicalPath + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName))
                    {
                        if (!Directory.Exists(szBasePhysicalPath + szNewPath))
                        {
                            Directory.CreateDirectory(szBasePhysicalPath + szNewPath);
                        }
                        File.Copy(szBasePhysicalPath + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName, szBasePhysicalPath + szNewPath + strGenFileName);
                    }



                    //Tushar
                    string bt_CaseType, bt_include, str_1500;
                    _MUVGenerateFunction = new MUVGenerateFunction();
                    string prefix = speciality.Substring(0, 2);
                    string PrcoedureGroup;
                    if (prefix.ToString().Equals("PR"))
                    {
                        PrcoedureGroup = _bill_Sys_NF3_Template.getGroup(szBillNumber, conn);
                    }
                    else
                    {
                        PrcoedureGroup = speciality;
                    }
                    bt_include = _MUVGenerateFunction.get_bt_include(CmpId, PrcoedureGroup, "", "Speciality");
                    bt_CaseType = _MUVGenerateFunction.get_bt_include(CmpId, "", "WC000000000000000001", "CaseType");
                    if (bt_include == "True" && bt_CaseType == "True")
                    {
                        Session["TM_SZ_CASE_ID"] = szCaseID;

                        str_1500 = _MUVGenerateFunction.FillPdf(szBillNumber.ToString(), conn);

                        MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szNewPath + strGenFileName, objNF3Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + str_1500, objNF3Template.getPhysicalPath() + CmpName + "/" + szCaseID + "/Packet Document/" + str_1500.Replace(".pdf", "_MER.pdf"));
                        strGenFileName = str_1500.Replace(".pdf", "_MER.pdf");
                        File.Copy(szBasePhysicalPath + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName, szBasePhysicalPath + szNewPath + strGenFileName);

                        returnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                    }
                    //End Of Code



                    ArrayList objAL = new ArrayList();
                    objAL.Add(CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                    objAL.Add(szBillNumber);
                    objAL.Add(szCaseID);
                    objAL.Add(CmpId);
                    _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL, conn);

                    ArrayList objAL1 = new ArrayList();

                    // changes for Doc manager for News Bill Path-- pravin
                    if (sz_Type == "OLD")
                    {

                        objAL1.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL1.Add(szNewPath + strGenFileName); // SZ_BILL_PATH
                        objAL1.Add(CmpId);
                        objAL1.Add(szCaseID);
                        objAL1.Add(strGenFileName); // SZ_BILL_NAME
                        objAL1.Add(szNewPath); // SZ_BILL_FILE_PATH
                        objAL1.Add(UserName);
                        objAL1.Add(speciality.Trim());
                        objAL1.Add("WC");
                        objAL1.Add(CaseNO);
                        objNF3Template.saveGeneratedBillPath(objAL1, conn);
                    }
                    else
                    {
                        objAL1.Add(szBillNumber); // SZ_BILL_NUMBER
                        objAL1.Add(szNewPath + strGenFileName); // SZ_BILL_PATH
                        objAL1.Add(CmpId);
                        objAL1.Add(szCaseID);
                        objAL1.Add(strGenFileName); // SZ_BILL_NAME
                        objAL1.Add(szNewPath); // SZ_BILL_FILE_PATH
                        objAL1.Add(UserName);
                        objAL1.Add(speciality.Trim());
                        objAL1.Add("WC");
                        objAL1.Add(CaseNO);
                        objAL1.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL1, conn);
                    }

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = strGenFileName;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = UserId;
                    _DAO_NOTES_EO.SZ_CASE_ID = szCaseID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        log4net.Config.XmlConfigurator.Configure();
        log.Debug("atul: " + returnPath);
        return returnPath;

    }

    public string GeneratePDFForReferalWorkerComp(string szBillId, string szCaseId, string szCompanyId, string szCompanyName, string szUserId, string szUserName, string szSpecility)
    {
        string szDefaultPath = "";
        string szReturnPath = "";

        string CmpId = "";
        Bill_Sys_NF3_Template _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
        try
        {
            objNF3Template = new Bill_Sys_NF3_Template();

            // changes for Doc manager for News Bill Path-- pravin
            objVerification_Desc = new Bill_Sys_Verification_Desc();
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            objCaseDetailsBO = new CaseDetailsBO();

            objVerification_Desc.sz_bill_no = szBillId;
            objVerification_Desc.sz_company_id = szCompanyId;
            objVerification_Desc.sz_flag = "BILL";

            ArrayList arrNf_Param = new ArrayList();
            ArrayList arrNf_NodeType = new ArrayList();
            DataSet ds1500form = new DataSet();
            string bt_1500_Form = "";
            string sz_Type = "";

            String szDestinationDir = "";


            arrNf_Param.Add(objVerification_Desc);

            arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Param);
            if (arrNf_NodeType.Contains("NFVER"))
            {
                sz_Type = "OLD";

                szDestinationDir = szCompanyName + "/" + szCaseId + "/No Fault File/Bills/" + szSpecility + "/";
            }
            else
            {
                sz_Type = "NEW";
                szDestinationDir = szCompanyName + "/" + szCaseId + "/No Fault File/Medicals/" + szSpecility + "/" + "Bills/";
            }

            int iCount = 0;

            iCount = GetProcedureCount(szBillId);
            if (iCount <= 4)
            {

                String szLastPDFFileName = "";

                //szDestinationDir = szCompanyName + "/" + szCaseId + "/No Fault File/Bills/" + szSpecility + "/";

                String szSourceDir = "";
                szSourceDir = szCompanyName + "/" + szCaseId + "/Packet Document/";

                //changes for Add only 1500 Form For Insurance Company -- pravin

                ds1500form = objCaseDetailsBO.Get1500FormBitForInsurance(szCompanyId, szBillId);
                if (ds1500form.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1500form.Tables[0].Rows.Count; i++)
                    {
                        bt_1500_Form = ds1500form.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (bt_1500_Form == "1")
                {
                    string str_1500 = "";
                    string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                    Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                    str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, szBillId, szCompanyName, szCaseId, szCompanyId);

                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }

                    szReturnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + str_1500;
                    ArrayList objAL = new ArrayList();
                    if (sz_Type == "OLD")
                    {
                        objAL.Add(szBillId);
                        objAL.Add(szDestinationDir + str_1500);
                        objAL.Add(szCompanyId);
                        objAL.Add(szCaseId);
                        objAL.Add(str_1500);
                        objAL.Add(szDestinationDir);
                        objAL.Add(szUserName);
                        objAL.Add(szSpecility);
                        //objAL.Add("");
                        objAL.Add("WC");
                        objAL.Add(szCaseId);
                        //objAL.Add(txtCaseNo.Text);
                        objNF3Template.saveGeneratedBillPath(objAL);
                    }
                    else
                    {
                        objAL.Add(szBillId);
                        objAL.Add(szDestinationDir + str_1500);
                        objAL.Add(szCompanyId);
                        objAL.Add(szCaseId);
                        objAL.Add(str_1500);
                        objAL.Add(szDestinationDir);
                        objAL.Add(szUserName);
                        objAL.Add(szSpecility);
                        //objAL.Add("");
                        objAL.Add("WC");
                        objAL.Add(szCaseId);
                        //objAL.Add(txtCaseNo.Text);
                        objAL.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL);
                    }


                    // Start : Save Notes for Bill.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = szUserId;
                    _DAO_NOTES_EO.SZ_CASE_ID = szCaseId;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = szCompanyId;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);


                }
                else
                {

                    String szGenereatedFileName = "";
                    objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string szXMLFileName;
                    string szOriginalPDFFileName;
                    szXMLFileName = ConfigurationManager.AppSettings["WCReferal_XML_FILE"].ToString();
                    szOriginalPDFFileName = ConfigurationManager.AppSettings["WCReferal_PDF_FILE"].ToString();

                    String szPDFPage = "";
                    //szPDFPage = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, szBillId, szCompanyName, szCaseId);
                    // Bill_Sys_Formating_Pdf obj = new Bill_Sys_Formating_Pdf();
                    Bill_Sys_Data obj1 = new Bill_Sys_Data();

                    //obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillId, szXMLFileName, szOriginalPDFFileName, szCompanyId, szCaseId, szCompanyName);

                    GeneratePDFForRefferalWC obj = new GeneratePDFForRefferalWC();
                    //obj1 = (Bill_Sys_Data)obj.FoeMating_Pdf(szBillId, szXMLFileName, szOriginalPDFFileName, szCompanyId, szCaseId, szCompanyName);
                    obj1 = (Bill_Sys_Data)obj.GenerateC4AMRLessThen4(szOriginalPDFFileName, szCompanyId, szCaseId, szBillId, szCompanyName);

                    szPDFPage = obj1.billurl;
                    #region File saving logic
                    String szOpenFilePath = "";
                    szGenereatedFileName = szDestinationDir + szPDFPage;
                    szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szGenereatedFileName;
                    string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + szGenereatedFileName;
                    string szFileNameForSaving = "";
                    szReturnPath = szOpenFilePath;

                    // Save Entry in Table
                    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
                    }
                    // End

                    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
                    {
                        szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                    }
                    else
                    {
                        if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                        {
                            szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                        }
                        else
                        {
                            szFileNameForSaving = szOpenFilePath.ToString();
                            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                        }
                    }
                    String[] szTemp;
                    string szBillName = "";
                    szTemp = szFileNameForSaving.Split('/');
                    ArrayList objAL = new ArrayList();
                    szFileNameForSaving = szFileNameForSaving.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                    szBillName = szTemp[szTemp.Length - 1].ToString();

                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
                    }


                    //Tushar
                    string bt_CaseType, bt_include, str_1500;
                    string strComp = szCompanyId.ToString();
                    _MUVGenerateFunction = new MUVGenerateFunction();
                    bt_include = _MUVGenerateFunction.get_bt_include(strComp, szSpecility, "", "Speciality");
                    bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000001", "CaseType");
                    if (bt_include == "True" && bt_CaseType == "True")
                    {
                        Session["TM_SZ_CASE_ID"] = szCaseId;
                        str_1500 = _MUVGenerateFunction.FillPdf(szBillId.ToString());

                        MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDestinationDir + szBillName, objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName.Replace(".pdf", "_MER.pdf"));
                        szBillName = szBillName.Replace(".pdf", "_MER.pdf");
                        szReturnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + szBillName;
                    }
                    //changes for Doc Manager For New Bill path -- pravin
                    if (sz_Type == "OLD")
                    {
                        objAL.Add(szBillId);
                        objAL.Add(szDestinationDir + szBillName);
                        objAL.Add(szCompanyId);
                        objAL.Add(szCaseId);
                        objAL.Add(szBillName);
                        objAL.Add(szDestinationDir);
                        objAL.Add(szUserName);
                        objAL.Add(szSpecility);
                        //objAL.Add("");
                        objAL.Add("WC");
                        objAL.Add(szCaseId);
                        //objAL.Add(txtCaseNo.Text);
                        objNF3Template.saveGeneratedBillPath(objAL);
                    }
                    else
                    {
                        objAL.Add(szBillId);
                        objAL.Add(szDestinationDir + szBillName);
                        objAL.Add(szCompanyId);
                        objAL.Add(szCaseId);
                        objAL.Add(szBillName);
                        objAL.Add(szDestinationDir);
                        objAL.Add(szUserName);
                        objAL.Add(szSpecility);
                        //objAL.Add("");
                        objAL.Add("WC");
                        objAL.Add(szCaseId);
                        //objAL.Add(txtCaseNo.Text);
                        objAL.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAL);
                    }


                    // Start : Save Notes for Bill.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = szUserId;
                    _DAO_NOTES_EO.SZ_CASE_ID = szCaseId;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = szCompanyId;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                    #endregion

                    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" +  ApplicationSettings.GetParameterValue("DocumentManagerURL") + szPDFPage + "'); ", true);
                }
            }
            else
            {
                String szLastPDFFileName = "";

                szDestinationDir = szCompanyName + "/" + szCaseId + "/No Fault File/Bills/" + szSpecility + "/";

                String szSourceDir = "";
                szSourceDir = szCompanyName + "/" + szCaseId + "/Packet Document/";

                //changes for Add only 1500 Form For Insurance Company -- pravin

                ds1500form = objCaseDetailsBO.Get1500FormBitForInsurance(szCompanyId, szBillId);
                if (ds1500form.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1500form.Tables[0].Rows.Count; i++)
                    {
                        bt_1500_Form = ds1500form.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (bt_1500_Form == "1")
                {
                    string str_1500 = "";
                    string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                    Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                    str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, szBillId, szCompanyName, szCaseId, szCompanyId);

                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }

                    szReturnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + str_1500;

                    ArrayList objAl1 = new ArrayList();

                    if (sz_Type == "OLD")
                    {
                        objAl1.Add(szBillId);
                        objAl1.Add(szDestinationDir + str_1500);
                        objAl1.Add(szCompanyId);
                        objAl1.Add(szCaseId);
                        objAl1.Add(str_1500);
                        objAl1.Add(szDestinationDir);
                        objAl1.Add(szUserName);
                        objAl1.Add(szSpecility);
                        //objAL.Add("");
                        objAl1.Add("WC");
                        objAl1.Add(szCaseId);
                        //objAL.Add(txtCaseNo.Text);
                        objNF3Template.saveGeneratedBillPath(objAl1);
                    }
                    else
                    {
                        objAl1.Add(szBillId);
                        objAl1.Add(szDestinationDir + str_1500);
                        objAl1.Add(szCompanyId);
                        objAl1.Add(szCaseId);
                        objAl1.Add(str_1500);
                        objAl1.Add(szDestinationDir);
                        objAl1.Add(szUserName);
                        objAl1.Add(szSpecility);
                        //objAL.Add("");
                        objAl1.Add("WC");
                        objAl1.Add(szCaseId);
                        objAl1.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAl1);

                    }


                    // Start : Save Notes for Bill.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = szUserId;
                    _DAO_NOTES_EO.SZ_CASE_ID = szCaseId;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = szCompanyId;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                }
                else
                {

                    String szGenereatedFileName = "";
                    objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string szXMLFileName;
                    string szOriginalPDFFileName;
                    szXMLFileName = ConfigurationManager.AppSettings["WCReferal_XML_FILE"].ToString();

                    szOriginalPDFFileName = ConfigurationManager.AppSettings["WCReferal_PDF_FILE_5_1"].ToString();

                    String szPDFPage1 = "";
                    //szPDFPage = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, szBillId, szCompanyName, szCaseId);

                    GeneratePDFForRefferalWC obj = new GeneratePDFForRefferalWC();
                    Bill_Sys_Data obj1 = new Bill_Sys_Data();
                    obj1 = (Bill_Sys_Data)obj.GenerateC4AMRLessThen4(szOriginalPDFFileName, szCompanyId, szCaseId, szBillId, szCompanyName);

                    szPDFPage1 = obj1.billurl;



                    string szXMLFileName1;
                    string szOriginalPDFFileName1;
                    szXMLFileName1 = ConfigurationManager.AppSettings["WCReferal_XML_FILE_5"].ToString();

                    szOriginalPDFFileName1 = ConfigurationManager.AppSettings["WCReferal_PDF_FILE_5_2"].ToString();

                    String szPDFPage2 = "";
                    //szPDFPage = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, szBillId, szCompanyName, szCaseId);
                    GeneratePDFForRefferalWC obj2 = new GeneratePDFForRefferalWC();
                    Bill_Sys_Data obj3 = new Bill_Sys_Data();
                    obj3 = (Bill_Sys_Data)obj2.GenerateC4AMRGreaterThen4(szOriginalPDFFileName1, szCompanyId, szCaseId, szBillId, szCompanyName);
                    szPDFPage2 = obj3.billurl;



                    string PdfPath1 = _bill_Sys_NF3_Template.getPhysicalPath() + szSourceDir + szPDFPage1;
                    string PdfPath2 = _bill_Sys_NF3_Template.getPhysicalPath() + szSourceDir + szPDFPage2;

                    string NewPdfPath = _bill_Sys_NF3_Template.getPhysicalPath() + szSourceDir + szBillId + "_" + szCaseId + "_" + DateTime.Now.ToString("M_d_yyyy_h_mm_ss_tt") + "1.pdf";

                    string lastPdf = _bill_Sys_NF3_Template.getPhysicalPath() + szSourceDir + szBillId + "_" + szCaseId + "_" + DateTime.Now.ToString("M_d_yyyy_h_mm_ss_tt") + "_MRG.pdf";

                    string szFirstMerge = MergePDF.MergePDFFiles(PdfPath1, PdfPath2, NewPdfPath);
                    string szOriginalPDFFileName3 = ConfigurationManager.AppSettings["WCReferal_PDF_FILE_5_3"].ToString();
                    String szPDFPage3 = "";
                    szPDFPage3 = _bill_Sys_NF3_Template.getPhysicalPath() + szSourceDir + szBillId + "_" + szCaseId + "_" + DateTime.Now.ToString("M_d_yyyy_h_mm_ss_tt") + "_3.pdf";
                    File.Copy(szOriginalPDFFileName3, szPDFPage3);

                    string szSecondMerge = MergePDF.MergePDFFiles(NewPdfPath, szPDFPage3, lastPdf);
                    string szBillName = szBillId + "_" + DateTime.Now.ToString("M_d_yyyy_h_mm_ss_tt") + ".pdf";
                    string BillFile = _bill_Sys_NF3_Template.getPhysicalPath() + szDestinationDir + szBillName;
                    // File.Copy(lastPdf, BillFile);

                    if (File.Exists(lastPdf))
                    {
                        if (!Directory.Exists(_bill_Sys_NF3_Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(_bill_Sys_NF3_Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(lastPdf, BillFile);
                    }

                    szReturnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + szBillName;
                    ArrayList objAl1 = new ArrayList();

                    //changes for Doc Manager for new bill path -- pravin
                    if (sz_Type == "OLD")
                    {
                        objAl1.Add(szBillId);
                        objAl1.Add(szDestinationDir + szBillName);
                        objAl1.Add(szCompanyId);
                        objAl1.Add(szCaseId);
                        objAl1.Add(szBillName);
                        objAl1.Add(szDestinationDir);
                        objAl1.Add(szUserName);
                        objAl1.Add(szSpecility);
                        //objAL.Add("");
                        objAl1.Add("WC");
                        objAl1.Add(szCaseId);
                        //objAL.Add(txtCaseNo.Text);
                        objNF3Template.saveGeneratedBillPath(objAl1);
                    }
                    else
                    {
                        objAl1.Add(szBillId);
                        objAl1.Add(szDestinationDir + szBillName);
                        objAl1.Add(szCompanyId);
                        objAl1.Add(szCaseId);
                        objAl1.Add(szBillName);
                        objAl1.Add(szDestinationDir);
                        objAl1.Add(szUserName);
                        objAl1.Add(szSpecility);
                        //objAL.Add("");
                        objAl1.Add("WC");
                        objAl1.Add(szCaseId);
                        objAl1.Add(arrNf_NodeType[0].ToString());
                        objNF3Template.saveGeneratedBillPath_New(objAl1);

                    }


                    // Start : Save Notes for Bill.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = szUserId;
                    _DAO_NOTES_EO.SZ_CASE_ID = szCaseId;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = szCompanyId;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return szReturnPath;
    }

    public string GetProcedureNumber(string szBillNo)
    {
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        string szCount = "";
        try
        {

            objSqlConn.Open();
            SqlCommand objSqlComm1 = new SqlCommand("SP_GET_PRPC_NUMBER ", objSqlConn);
            objSqlComm1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader objDR;
            objSqlComm1.CommandType = CommandType.StoredProcedure;
            objSqlComm1.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNo);
            objDR = objSqlComm1.ExecuteReader();


            while (objDR.Read())
            {
                szCount = objDR[1].ToString();


            }


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objSqlConn.Close();
        }
        return szCount;

    }
    public string GetProcedureNumber(string szBillNo, ServerConnection conn)
    {
        SqlDataReader objDR = null;
        string szCount = "";
        try
        {
            string Query = "";
            Query = Query + "Exec SP_GET_PRPC_NUMBER ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", szBillNo, ",");

            Query = Query.TrimEnd(',');

            objDR = conn.ExecuteReader(Query);


            while (objDR.Read())
            {
                szCount = objDR[1].ToString();


            }
            objDR.Close();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            if (objDR != null && !objDR.IsClosed) { objDR.Close(); }

        }
        return szCount;

    }

    public string Getotptpdf(string szNewName, DataSet ds_table, DataSet ds1, string szBillNumber, string szCaseID, string CmpId, string CmpName, string pdfname)
    {
        string strGenFileName = "";
        string ReadpdfFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_OCT"].ToString();
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
        #region code for generate pdf Kapil
        if (ds1.Tables[0].Rows.Count > 0)
        {
            if (ds1.Tables[0].Rows[0]["OCCUPATIONAL THERAPISTS REPORT"].ToString() == "1")
            {
                fields.SetField("OCCUPATIONAL THERAPISTS REPORT", "1");
            }
            else if (ds1.Tables[0].Rows[0]["OCCUPATIONAL THERAPISTS REPORT"].ToString() == "0")
            {
                fields.SetField("OCCUPATIONAL THERAPISTS REPORT", "0");
            }
            if (ds1.Tables[0].Rows[0]["PHYSICAL THERAPISTS REPORT"].ToString() == "1")
            {
                fields.SetField("PHYSICAL THERAPISTS REPORT", "1");
            }
            else if (ds1.Tables[0].Rows[0]["PHYSICAL THERAPISTS REPORT"].ToString() == "0")
            {
                fields.SetField("PHYSICAL THERAPISTS REPORT", "0");
            }
            if (ds1.Tables[0].Rows[0]["BT_PPO"].ToString() == "1")
            {
                fields.SetField("Check Box14", "1");
                fields.SetField("Check Box15", "0");
            }
            else if (ds1.Tables[0].Rows[0]["BT_PPO"].ToString() == "0")
            {
                fields.SetField("Check Box14", "0");
                fields.SetField("Check Box15", "1");
            }


            if (ds1.Tables[0].Rows[0]["Check Box16"].ToString() == "1")
            {
                fields.SetField("Check Box16", "1");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box16"].ToString() == "0")
            {
                fields.SetField("Check Box16", "0");
            }
            if (ds1.Tables[0].Rows[0]["Check Box17"].ToString() == "1")
            {
                fields.SetField("Check Box17", "1");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box17"].ToString() == "0")
            {
                fields.SetField("Check Box17", "0");
            }
            if (ds1.Tables[0].Rows[0]["Check Box18"].ToString() == "1")
            {
                fields.SetField("Check Box18", "1");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box18"].ToString() == "0")
            {
                fields.SetField("Check Box18", "0");
            }
            if (ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString() == null || ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString() == "")
            {
                fields.SetField("WCB CASE NORow1", "-");
            }
            else
            {
                fields.SetField("WCB CASE NORow1", ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString() == null || ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString() == "")
            {
                fields.SetField("CARRIER CASE NO IF KNOWNRow1", "-");
            }
            else
            {
                fields.SetField("CARRIER CASE NO IF KNOWNRow1", ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString() == null || ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString() == "")
            {
                fields.SetField("DATE OF INJURYRow1", "-");
            }
            else
            {
                fields.SetField("DATE OF INJURYRow1", ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text46"].ToString() == null || ds1.Tables[0].Rows[0]["Text46"].ToString() == "")
            {
                fields.SetField("Text46", "-");
            }
            else
            {
                fields.SetField("Text46", ds1.Tables[0].Rows[0]["Text46"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString() == null || ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString() == "")
            {
                fields.SetField("ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1", "-");
            }
            else
            {
                fields.SetField("ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1", ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString() == null || ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString() == "")
            {
                fields.SetField("INJURED PERSONS SOCIAL SECURITY NUMBERRow1", "-");
            }
            else
            {
                fields.SetField("INJURED PERSONS SOCIAL SECURITY NUMBERRow1", ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString() == null || ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString() == "")
            {
                fields.SetField("INJURED PERSON", "-");
            }
            else
            {
                fields.SetField("INJURED PERSON", ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text32"].ToString() == null || ds1.Tables[0].Rows[0]["Text32"].ToString() == "")
            {
                fields.SetField("Text32", "-");
            }
            else
            {
                fields.SetField("Text32", ds1.Tables[0].Rows[0]["Text32"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString() == null || ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString() == "")
            {
                fields.SetField("TELEPHONE NO", "-");
            }
            else
            {
                fields.SetField("TELEPHONE NO", ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString() == null || ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString() == "")
            {
                fields.SetField("First Name Middle Initial Last NameEMPLOYER", "-");
            }
            else
            {
                fields.SetField("First Name Middle Initial Last NameEMPLOYER", ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString() == null || ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString() == "")
            {
                fields.SetField("ADDRESS Include Apt NoEMPLOYER", "-");
            }
            else
            {
                fields.SetField("ADDRESS Include Apt NoEMPLOYER", ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString() == null || ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString() == "")
            {
                fields.SetField("PATIENTS DATE OF BIRTH", "-");
            }
            else
            {
                fields.SetField("PATIENTS DATE OF BIRTH", ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString());

            }
            if (ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString() == null || ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString() == "")
            {
                fields.SetField("INSURANCE CARRIER", "-");
            }
            else
            {
                fields.SetField("INSURANCE CARRIER", ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text30"].ToString() == null || ds1.Tables[0].Rows[0]["Text30"].ToString() == "")
            {
                fields.SetField("Text30", "-");
            }
            else
            {
                fields.SetField("Text30", ds1.Tables[0].Rows[0]["Text30"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString() == null || ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString() == "")
            {
                fields.SetField("REFERRING PHYSICIAN PODIATRIST", "-");
            }
            else
            {
                fields.SetField("REFERRING PHYSICIAN PODIATRIST", ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text31"].ToString() == null || ds1.Tables[0].Rows[0]["Text31"].ToString() == "")
            {
                fields.SetField("Text31", "-");
            }
            else
            {
                fields.SetField("Text31", ds1.Tables[0].Rows[0]["Text31"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString() == null || ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString() == "")
            {
                fields.SetField("TELEPHONE NO_2", "-");
            }
            else
            {
                fields.SetField("TELEPHONE NO_2", ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString() == "")
            {
                fields.SetField("REFERRING PHYSICIAN PODIATRIST", "-");
            }
            else
            {
                fields.SetField("REFERRING PHYSICIAN PODIATRIST", ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString() == "")
            {
                fields.SetField("Text31", "-");
            }
            else
            {
                fields.SetField("Text31", ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Check Box19"].ToString() == "1")
            {
                fields.SetField("Check Box19", "0");
                fields.SetField("Check Box20", "1");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box19"].ToString() == "0")
            {
                fields.SetField("Check Box19", "1");
                fields.SetField("Check Box20", "0");
            }

            if (ds1.Tables[0].Rows[0]["Text43"].ToString() == null || ds1.Tables[0].Rows[0]["Text43"].ToString() == "")
            {
                fields.SetField("Text43", "-");
            }
            else
            {
                fields.SetField("Text43", ds1.Tables[0].Rows[0]["Text43"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text33"].ToString() == null || ds1.Tables[0].Rows[0]["Text33"].ToString() == "")
            {
                fields.SetField("Text33", "-");
            }
            else
            {
                fields.SetField("Text33", ds1.Tables[0].Rows[0]["Text33"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString() == null || ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString() == "")
            {
                fields.SetField("patient history of preexisting injury disease", "-");
            }
            else
            {
                fields.SetField("patient history of preexisting injury disease", ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "0")
            {
                fields.SetField("Check Box34", "1");
                fields.SetField("Check Box35", "0");
                fields.SetField("Check Box36", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "1")
            {
                fields.SetField("Check Box34", "0");
                fields.SetField("Check Box35", "1");
                fields.SetField("Check Box36", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "2")
            {
                fields.SetField("Check Box34", "0");
                fields.SetField("Check Box35", "0");
                fields.SetField("Check Box36", "1");
            }
            if (ds1.Tables[0].Rows[0]["Check Box24"].ToString() == "1")
            {
                fields.SetField("Check Box23", "1");
                fields.SetField("Check Box24", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box24"].ToString() == "0")
            {
                fields.SetField("Check Box23", "0");
                fields.SetField("Check Box24", "1");
            }
            if (ds1.Tables[0].Rows[0]["Check Box21"].ToString() == "1")
            {
                fields.SetField("Check Box21", "1");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box21"].ToString() == "0")
            {
                fields.SetField("Check Box21", "0");
            }
            if (ds1.Tables[0].Rows[0]["a Your evaluation"].ToString() == null || ds1.Tables[0].Rows[0]["a Your evaluation"].ToString() == "")
            {
                fields.SetField("a Your evaluation", "-");
            }
            else
            {
                fields.SetField("a Your evaluation", ds1.Tables[0].Rows[0]["a Your evaluation"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString() == null || ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString() == "")
            {
                fields.SetField("b 1 Patients condition and progress", "-");
            }
            else
            {
                fields.SetField("b 1 Patients condition and progress", ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString());
            }

            if (ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString() == null || ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString() == "")
            {
                fields.SetField("Treatment and planned future treatment", "-");
            }
            else
            {
                fields.SetField("Treatment and planned future treatment", ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "1")
            {
                fields.SetField("Check Box22", "1");
                fields.SetField("Check Box25", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "0")
            {
                fields.SetField("Check Box22", "0");
                fields.SetField("Check Box25", "1");
            }

            if (ds1.Tables[0].Rows[0]["Text44"].ToString() == null || ds1.Tables[0].Rows[0]["Text44"].ToString() == "")
            {
                fields.SetField("Text44", "-");
            }
            else
            {
                fields.SetField("Text44", ds1.Tables[0].Rows[0]["Text44"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text45"].ToString() == null || ds1.Tables[0].Rows[0]["Text45"].ToString() == "")
            {
                fields.SetField("Text45", "-");
            }
            else
            {
                fields.SetField("Text45", ds1.Tables[0].Rows[0]["Text45"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString() == null || ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString() == "")
            {
                fields.SetField("4 Dates of visits on which this report is based", "-");
            }
            else
            {
                fields.SetField("4 Dates of visits on which this report is based", ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString() == null || ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString() == "")
            {
                fields.SetField("If no was patient referred back to attending doctor D Yes D No", "-");
            }
            else
            {
                fields.SetField("If no was patient referred back to attending doctor D Yes D No", ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Check Box26"].ToString() == "1")
            {
                fields.SetField("Check Box26", "1");
                fields.SetField("Check Box27", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box26"].ToString() == "0")
            {
                fields.SetField("Check Box26", "0");
                fields.SetField("Check Box27", "1");
            }
            if (ds1.Tables[0].Rows[0]["H_2"].ToString() == null || ds1.Tables[0].Rows[0]["H_2"].ToString() == "")
            {
                fields.SetField("H_2", "-");
            }
            else
            {
                fields.SetField("H_2", ds1.Tables[0].Rows[0]["H_2"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Check Box28"].ToString() == "1")
            {
                fields.SetField("Check Box28", "1");
                fields.SetField("Check Box29", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box28"].ToString() == "0")
            {
                fields.SetField("Check Box28", "0");
                fields.SetField("Check Box29", "1");
            }
            if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "1")
            {
                fields.SetField("Check Box22", "1");
                fields.SetField("Check Box25", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "0")
            {
                fields.SetField("Check Box22", "0");
                fields.SetField("Check Box25", "1");
            }
            if (ds1.Tables[0].Rows[0]["Text37"].ToString() == null || ds1.Tables[0].Rows[0]["Text37"].ToString() == "")
            {
                fields.SetField("Text37", "-");
            }
            else
            {
                fields.SetField("Text37", ds1.Tables[0].Rows[0]["Text37"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text38"].ToString() == null || ds1.Tables[0].Rows[0]["Text38"].ToString() == "")
            {
                fields.SetField("Text38", "-");
            }
            else
            {
                fields.SetField("Text38", ds1.Tables[0].Rows[0]["Text38"].ToString());
            }

            if (ds1.Tables[0].Rows[0]["Check Box40"].ToString() == "True")
            {
                fields.SetField("Check Box40", "1");
                fields.SetField("Check Box41", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box40"].ToString() == "False")
            {
                fields.SetField("Check Box40", "0");
                fields.SetField("Check Box41", "1");
            }

            if (ds1.Tables[0].Rows[0]["Text39"].ToString() == null || ds1.Tables[0].Rows[0]["Text39"].ToString() == "")
            {
                fields.SetField("TELEPHONE NO_2", "-");
            }
            else
            {
                fields.SetField("TELEPHONE NO_2", ds1.Tables[0].Rows[0]["Text39"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString() == null || ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString() == "")
            {
                fields.SetField("9 NYS License Number", "-");
            }
            else
            {
                fields.SetField("9 NYS License Number", ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString() == "")
            {
                fields.SetField("Text39", "-");
            }
            else
            {
                fields.SetField("Text39", ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString() == null || ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString() == "")
            {
                fields.SetField("10 Patients Account Number", "-");
            }
            else
            {
                fields.SetField("10 Patients Account Number", ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString() == null || ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString() == "")
            {
                fields.SetField("WCB CASE NORow1", "-");
            }
            else
            {
                fields.SetField("WCB CASE NORow1", ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString() == "")
            {
                fields.SetField("15 Therapists Name Address  Phone No", "-");
            }
            else
            {
                fields.SetField("15 Therapists Name Address  Phone No", ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == "")
            {
                fields.SetField("16 Therapists Billing Name Address  Phone No", "-");
            }
            else
            {
                fields.SetField("16 Therapists Billing Name Address  Phone No", ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString());
            }


            if (ds_table.Tables[0].Rows.Count > 0)
            {
                string Fmmtext = "FmmText";
                string FddText = "FddText";
                string FyyText = "FyyText";
                string TmmText = "TmmText";
                string TddText = "TddText";
                string TyyText = "TyyText";
                string BText = "BText";
                string DCText = "DCText";
                string DMText = "DMText";
                string EText = "DC";
                string FText = "FText";
                string GText = "GText";
                string HText = "HText";
                string IText = "IText";
                for (int i = 0; i < ds_table.Tables[0].Rows.Count; i++)
                {

                    fields.SetField(Fmmtext + i.ToString(), ds_table.Tables[0].Rows[i]["MONTH"].ToString());

                    fields.SetField(FddText + i.ToString(), ds_table.Tables[0].Rows[i]["DAY"].ToString());

                    fields.SetField(FyyText + i.ToString(), ds_table.Tables[0].Rows[i]["YEAR"].ToString());

                    fields.SetField(TmmText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString());

                    fields.SetField(TddText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_DAY"].ToString());

                    fields.SetField(TyyText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString());

                    fields.SetField(BText + i.ToString(), ds_table.Tables[0].Rows[i]["PLACE_OF_SERVICE"].ToString());

                    fields.SetField(DCText + i.ToString(), ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());

                    fields.SetField(EText + i.ToString(), ds_table.Tables[0].Rows[i]["DC_1"].ToString());

                    fields.SetField(FText + i.ToString(), ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString());

                    fields.SetField(GText + i.ToString(), ds_table.Tables[0].Rows[i]["I_UNIT"].ToString());

                    fields.SetField(IText + i.ToString(), ds_table.Tables[0].Rows[i]["ZIP_CODE"].ToString());
                }
                fields.SetField("11 Total Charges", ds_table.Tables[0].Rows[0]["BILL_AMOUNT"].ToString());
                fields.SetField("12.Amt.Paid", ds_table.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
                fields.SetField("13.Balance", ds_table.Tables[0].Rows[0]["BALANCE"].ToString());
                fields.SetField("1", ds_table.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString());
            }

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] u = url.Split('/');
            string str = "";
            for (int i = 0; i < u.Length; i++)
            {
                if (i < 3)
                {
                    if (i == 0)
                    {
                        str = u[i];
                    }
                    else
                        str = str + "/" + u[i];
                }
            }
            fields.RenameField("OCCUPATIONAL THERAPISTS REPORT", "OCCUPATIONAL THERAPISTS REPORT" + szNewName);
            fields.RenameField("PHYSICAL THERAPISTS REPORT", "PHYSICAL THERAPISTS REPORT" + szNewName);
            fields.RenameField("Check Box14", "Check Box14" + szNewName);
            fields.RenameField("Check Box15", "Check Box15" + szNewName);
            fields.RenameField("Check Box16", "Check Box16" + szNewName);
            fields.RenameField("Check Box17", "Check Box17" + szNewName);
            fields.RenameField("Check Box18", "Check Box18" + szNewName);
            fields.RenameField("WCB CASE NORow1", "WCB CASE NORow1" + szNewName);
            fields.RenameField("CARRIER CASE NO IF KNOWNRow1", "CARRIER CASE NO IF KNOWNRow1" + szNewName);
            fields.RenameField("DATE OF INJURYRow1", "DATE OF INJURYRow1" + szNewName);
            fields.RenameField("Text46", "Text46" + szNewName);
            fields.RenameField("ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1", "ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1" + szNewName);
            fields.RenameField("INJURED PERSONS SOCIAL SECURITY NUMBERRow1", "INJURED PERSONS SOCIAL SECURITY NUMBERRow1" + szNewName);
            fields.RenameField("INJURED PERSON", "INJURED PERSON" + szNewName);
            fields.RenameField("Text32", "Text32" + szNewName);
            fields.RenameField("TELEPHONE NO", "TELEPHONE NO" + szNewName);
            fields.RenameField("First Name Middle Initial Last NameEMPLOYER", "First Name Middle Initial Last NameEMPLOYER" + szNewName);
            fields.RenameField("ADDRESS Include Apt NoEMPLOYER", "ADDRESS Include Apt NoEMPLOYER" + szNewName);
            fields.RenameField("PATIENTS DATE OF BIRTH", "PATIENTS DATE OF BIRTH" + szNewName);
            fields.RenameField("INSURANCE CARRIER", "INSURANCE CARRIER" + szNewName);
            fields.RenameField("Text30", "Text30" + szNewName);
            fields.RenameField("REFERRING PHYSICIAN PODIATRIST", "REFERRING PHYSICIAN PODIATRIST" + szNewName);

            fields.RenameField("Text31", "Text31" + szNewName);
            fields.RenameField("TELEPHONE NO_2", "TELEPHONE NO_2" + szNewName);
            fields.RenameField("Check Box19", "Check Box19" + szNewName);
            fields.RenameField("Check Box20", "Check Box20" + szNewName);
            fields.RenameField("Text43", "Text43" + szNewName);
            fields.RenameField("Text33", "Text33" + szNewName);
            fields.RenameField("patient history of preexisting injury disease", "patient history of preexisting injury disease" + szNewName);
            fields.RenameField("Check Box34", "Check Box34" + szNewName);
            fields.RenameField("Check Box35", "Check Box35" + szNewName);
            fields.RenameField("Check Box36", "Check Box36" + szNewName);
            fields.RenameField("a Your evaluation", "a Your evaluation" + szNewName);
            fields.RenameField("b 1 Patients condition and progress", "b 1 Patients condition and progress" + szNewName);
            fields.RenameField("Check Box21", "Check Box21" + szNewName);
            fields.RenameField("Treatment and planned future treatment", "Treatment and planned future treatment" + szNewName);
            fields.RenameField("Check Box23", "Check Box23" + szNewName);
            fields.RenameField("Check Box24", "Check Box24" + szNewName);
            fields.RenameField("Text44", "Text44" + szNewName);
            fields.RenameField("Text45", "Text45" + szNewName);
            fields.RenameField("4 Dates of visits on which this report is based", "4 Dates of visits on which this report is based" + szNewName);
            fields.RenameField("If no was patient referred back to attending doctor D Yes D No", "If no was patient referred back to attending doctor D Yes D No" + szNewName);
            fields.RenameField("Check Box26", "Check Box26" + szNewName);
            fields.RenameField("Check Box27", "Check Box27" + szNewName);
            fields.RenameField("H_2", "H_2" + szNewName);
            fields.RenameField("Check Box28", "Check Box28" + szNewName);
            fields.RenameField("Check Box29", "Check Box29" + szNewName);
            fields.RenameField("Check Box22", "Check Box22" + szNewName);
            fields.RenameField("Check Box25", "Check Box25" + szNewName);

            fields.RenameField("Text37", "Text37" + szNewName);
            fields.RenameField("Text38", "Text38" + szNewName);
            fields.RenameField("1", "1" + szNewName);

            fields.RenameField("FmmText0", "FmmText0" + szNewName);
            fields.RenameField("FmmText1", "FmmText1" + szNewName);
            fields.RenameField("FmmText2", "FmmText2" + szNewName);
            fields.RenameField("FmmText3", "FmmText3" + szNewName);
            fields.RenameField("FmmText4", "FmmText4" + szNewName);
            fields.RenameField("FmmText5", "FmmText5" + szNewName);

            fields.RenameField("FddText0", "FddText0" + szNewName);
            fields.RenameField("FddText1", "FddText1" + szNewName);
            fields.RenameField("FddText2", "FddText2" + szNewName);
            fields.RenameField("FddText3", "FddText3" + szNewName);
            fields.RenameField("FddText4", "FddText4" + szNewName);
            fields.RenameField("FddText5", "FddText5" + szNewName);

            fields.RenameField("FyyText0", "FyyText0" + szNewName);
            fields.RenameField("FyyText1", "FyyText1" + szNewName);
            fields.RenameField("FyyText2", "FyyText2" + szNewName);
            fields.RenameField("FyyText3", "FyyText3" + szNewName);
            fields.RenameField("FyyText4", "FyyText4" + szNewName);
            fields.RenameField("FyyText5", "FyyText5" + szNewName);

            fields.RenameField("TmmText0", "TmmText0" + szNewName);
            fields.RenameField("TmmText1", "TmmText1" + szNewName);
            fields.RenameField("TmmText2", "TmmText2" + szNewName);
            fields.RenameField("TmmText3", "TmmText3" + szNewName);
            fields.RenameField("TmmText4", "TmmText4" + szNewName);
            fields.RenameField("TmmText5", "TmmText5" + szNewName);

            fields.RenameField("TddText0", "TddText0" + szNewName);
            fields.RenameField("TddText1", "TddText1" + szNewName);
            fields.RenameField("TddText2", "TddText2" + szNewName);
            fields.RenameField("TddText3", "TddText3" + szNewName);
            fields.RenameField("TddText4", "TddText4" + szNewName);
            fields.RenameField("TddText5", "TddText5" + szNewName);

            fields.RenameField("TyyText0", "TyyText0" + szNewName);
            fields.RenameField("TyyText1", "TyyText1" + szNewName);
            fields.RenameField("TyyText2", "TyyText2" + szNewName);
            fields.RenameField("TyyText3", "TyyText3" + szNewName);
            fields.RenameField("TyyText4", "TyyText4" + szNewName);
            fields.RenameField("TyyText5", "TyyText5" + szNewName);

            fields.RenameField("BText0", "BText0" + szNewName);
            fields.RenameField("BText1", "BText1" + szNewName);
            fields.RenameField("BText2", "BText2" + szNewName);
            fields.RenameField("BText3", "BText3" + szNewName);
            fields.RenameField("BText4", "BText4" + szNewName);
            fields.RenameField("BText5", "BText5" + szNewName);

            fields.RenameField("DCText0", "DCText0" + szNewName);
            fields.RenameField("DCText1", "DCText1" + szNewName);
            fields.RenameField("DCText2", "DCText2" + szNewName);
            fields.RenameField("DCText3", "DCText3" + szNewName);
            fields.RenameField("DCText4", "DCText4" + szNewName);
            fields.RenameField("DCText5", "DCText5" + szNewName);

            fields.RenameField("DMText0", "DMText0" + szNewName);

            fields.RenameField("DC0", "DC0" + szNewName);
            fields.RenameField("DC1", "DC1" + szNewName);
            fields.RenameField("DC2", "DC2" + szNewName);
            fields.RenameField("DC3", "DC3" + szNewName);
            fields.RenameField("DC4", "DC4" + szNewName);
            fields.RenameField("DC5", "DC5" + szNewName);
            fields.RenameField("FText0", "FText0" + szNewName);
            fields.RenameField("FText1", "FText1" + szNewName);
            fields.RenameField("FText2", "FText2" + szNewName);
            fields.RenameField("FText3", "FText3" + szNewName);
            fields.RenameField("FText4", "FText4" + szNewName);
            fields.RenameField("FText5", "FText5" + szNewName);

            fields.RenameField("GText0", "GText0" + szNewName);
            fields.RenameField("GText1", "GText1" + szNewName);
            fields.RenameField("GText2", "GText2" + szNewName);
            fields.RenameField("GText3", "GText3" + szNewName);
            fields.RenameField("GText4", "GText4" + szNewName);
            fields.RenameField("GText5", "GText5" + szNewName);

            fields.RenameField("HText0", "HText0" + szNewName);
            fields.RenameField("HText1", "HText1" + szNewName);
            fields.RenameField("HText2", "HText2" + szNewName);
            fields.RenameField("HText3", "HText3" + szNewName);
            fields.RenameField("HText4", "HText4" + szNewName);
            fields.RenameField("HText5", "HText5" + szNewName);

            fields.RenameField("IText0", "IText0" + szNewName);
            fields.RenameField("IText1", "IText1" + szNewName);
            fields.RenameField("IText2", "IText2" + szNewName);
            fields.RenameField("IText3", "IText3" + szNewName);
            fields.RenameField("IText4", "IText4" + szNewName);
            fields.RenameField("IText5", "IText5" + szNewName);

            fields.RenameField("Check Box40", "Check Box40" + szNewName);
            fields.RenameField("Check Box41", "Check Box41" + szNewName);
            fields.RenameField("Text39", "Text39" + szNewName);
            fields.RenameField("9 NYS License Number", "9 NYS License Number" + szNewName);
            fields.RenameField("10 Patients Account Number", "10 Patients Account Number" + szNewName);
            fields.RenameField("11 Total Charges", "11 Total Charges" + szNewName);
            fields.RenameField("12.Amt.Paid", "12.Amt.Paid" + szNewName);
            fields.RenameField("13.Balance", "13.Balance" + szNewName);
            fields.RenameField("15 Therapists Name Address  Phone No", "15 Therapists Name Address  Phone No" + szNewName);
            fields.RenameField("16 Therapists Billing Name Address  Phone No", "16 Therapists Billing Name Address  Phone No" + szNewName);
            stamper.Close();

        }

        #endregion
        string openPath = CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
        returnPath = openPath;
        return returnPath;

    }

    public string Getotptpdfchangefield(string szNewName, DataSet ds_table, DataSet ds1, string szBillNumber, string szCaseID, string CmpId, string CmpName, string pdfname)
    {
        string strGenFileName = "";
        string ReadpdfFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_OCT"].ToString();
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


        #region code for generate pdf Kapil
        if (ds1.Tables[0].Rows.Count > 0)
        {
            if (ds1.Tables[0].Rows[0]["OCCUPATIONAL THERAPISTS REPORT"].ToString() == "1")
            {
                fields.SetField("OCCUPATIONAL THERAPISTS REPORT", "1");
            }
            else if (ds1.Tables[0].Rows[0]["OCCUPATIONAL THERAPISTS REPORT"].ToString() == "0")
            {
                fields.SetField("OCCUPATIONAL THERAPISTS REPORT", "0");
            }
            if (ds1.Tables[0].Rows[0]["PHYSICAL THERAPISTS REPORT"].ToString() == "1")
            {
                fields.SetField("PHYSICAL THERAPISTS REPORT", "1");
            }
            else if (ds1.Tables[0].Rows[0]["PHYSICAL THERAPISTS REPORT"].ToString() == "0")
            {
                fields.SetField("PHYSICAL THERAPISTS REPORT", "0");
            }
            if (ds1.Tables[0].Rows[0]["BT_PPO"].ToString() == "1")
            {
                fields.SetField("Check Box14", "1");
                fields.SetField("Check Box15", "0");
            }
            else if (ds1.Tables[0].Rows[0]["BT_PPO"].ToString() == "0")
            {
                fields.SetField("Check Box14", "0");
                fields.SetField("Check Box15", "1");
            }


            if (ds1.Tables[0].Rows[0]["Check Box16"].ToString() == "1")
            {
                fields.SetField("Check Box16", "1");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box16"].ToString() == "0")
            {
                fields.SetField("Check Box16", "0");
            }
            if (ds1.Tables[0].Rows[0]["Check Box17"].ToString() == "1")
            {
                fields.SetField("Check Box17", "1");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box17"].ToString() == "0")
            {
                fields.SetField("Check Box17", "0");
            }
            if (ds1.Tables[0].Rows[0]["Check Box18"].ToString() == "1")
            {
                fields.SetField("Check Box18", "1");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box18"].ToString() == "0")
            {
                fields.SetField("Check Box18", "0");
            }
            if (ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString() == null || ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString() == "")
            {
                fields.SetField("WCB CASE NORow1", "-");
            }
            else
            {
                fields.SetField("WCB CASE NORow1", ds1.Tables[0].Rows[0]["WCB CASE NORow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString() == null || ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString() == "")
            {
                fields.SetField("CARRIER CASE NO IF KNOWNRow1", "-");
            }
            else
            {
                fields.SetField("CARRIER CASE NO IF KNOWNRow1", ds1.Tables[0].Rows[0]["CARRIER CASE NO IF KNOWNRow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString() == null || ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString() == "")
            {
                fields.SetField("DATE OF INJURYRow1", "-");
            }
            else
            {
                fields.SetField("DATE OF INJURYRow1", ds1.Tables[0].Rows[0]["DATE OF INJURYRow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text46"].ToString() == null || ds1.Tables[0].Rows[0]["Text46"].ToString() == "")
            {
                fields.SetField("Text46", "-");
            }
            else
            {
                fields.SetField("Text46", ds1.Tables[0].Rows[0]["Text46"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString() == null || ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString() == "")
            {
                fields.SetField("ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1", "-");
            }
            else
            {
                fields.SetField("ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1", ds1.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString() == null || ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString() == "")
            {
                fields.SetField("INJURED PERSONS SOCIAL SECURITY NUMBERRow1", "-");
            }
            else
            {
                fields.SetField("INJURED PERSONS SOCIAL SECURITY NUMBERRow1", ds1.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString() == null || ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString() == "")
            {
                fields.SetField("INJURED PERSON", "-");
            }
            else
            {
                fields.SetField("INJURED PERSON", ds1.Tables[0].Rows[0]["INJURED PERSON"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text32"].ToString() == null || ds1.Tables[0].Rows[0]["Text32"].ToString() == "")
            {
                fields.SetField("Text32", "-");
            }
            else
            {
                fields.SetField("Text32", ds1.Tables[0].Rows[0]["Text32"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString() == null || ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString() == "")
            {
                fields.SetField("TELEPHONE NO", "-");
            }
            else
            {
                fields.SetField("TELEPHONE NO", ds1.Tables[0].Rows[0]["TELEPHONE NO"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString() == null || ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString() == "")
            {
                fields.SetField("First Name Middle Initial Last NameEMPLOYER", "-");
            }
            else
            {
                fields.SetField("First Name Middle Initial Last NameEMPLOYER", ds1.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString() == null || ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString() == "")
            {
                fields.SetField("ADDRESS Include Apt NoEMPLOYER", "-");
            }
            else
            {
                fields.SetField("ADDRESS Include Apt NoEMPLOYER", ds1.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString() == null || ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString() == "")
            {
                fields.SetField("PATIENTS DATE OF BIRTH", "-");
            }
            else
            {
                fields.SetField("PATIENTS DATE OF BIRTH", ds1.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"].ToString());

            }
            if (ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString() == null || ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString() == "")
            {
                fields.SetField("INSURANCE CARRIER", "-");
            }
            else
            {
                fields.SetField("INSURANCE CARRIER", ds1.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text30"].ToString() == null || ds1.Tables[0].Rows[0]["Text30"].ToString() == "")
            {
                fields.SetField("Text30", "-");
            }
            else
            {
                fields.SetField("Text30", ds1.Tables[0].Rows[0]["Text30"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString() == null || ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString() == "")
            {
                fields.SetField("REFERRING PHYSICIAN PODIATRIST", "-");
            }
            else
            {
                fields.SetField("REFERRING PHYSICIAN PODIATRIST", ds1.Tables[0].Rows[0]["REFERRING PHYSICIAN PODIATRIST"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text31"].ToString() == null || ds1.Tables[0].Rows[0]["Text31"].ToString() == "")
            {
                fields.SetField("Text31", "-");
            }
            else
            {
                fields.SetField("Text31", ds1.Tables[0].Rows[0]["Text31"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString() == null || ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString() == "")
            {
                fields.SetField("TELEPHONE NO_2", "-");
            }
            else
            {
                fields.SetField("TELEPHONE NO_2", ds1.Tables[0].Rows[0]["TELEPHONE NO_2"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString() == "")
            {
                fields.SetField("REFERRING PHYSICIAN PODIATRIST", "-");
            }
            else
            {
                fields.SetField("REFERRING PHYSICIAN PODIATRIST", ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString() == "")
            {
                fields.SetField("Text31", "-");
            }
            else
            {
                fields.SetField("Text31", ds1.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Check Box19"].ToString() == "1")
            {
                fields.SetField("Check Box19", "0");
                fields.SetField("Check Box20", "1");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box19"].ToString() == "0")
            {
                fields.SetField("Check Box19", "1");
                fields.SetField("Check Box20", "0");
            }
            //if (ds1.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "1")
            //{
            //    fields.SetField("Check Box20", "1");
            //}
            //else if (ds1.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "0")
            //{
            //    fields.SetField("Check Box20", "0");
            //}
            if (ds1.Tables[0].Rows[0]["Text43"].ToString() == null || ds1.Tables[0].Rows[0]["Text43"].ToString() == "")
            {
                fields.SetField("Text43", "-");
            }
            else
            {
                fields.SetField("Text43", ds1.Tables[0].Rows[0]["Text43"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text33"].ToString() == null || ds1.Tables[0].Rows[0]["Text33"].ToString() == "")
            {
                fields.SetField("Text33", "-");
            }
            else
            {
                fields.SetField("Text33", ds1.Tables[0].Rows[0]["Text33"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString() == null || ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString() == "")
            {
                fields.SetField("patient history of preexisting injury disease", "-");
            }
            else
            {
                fields.SetField("patient history of preexisting injury disease", ds1.Tables[0].Rows[0]["patient history of preexisting injury disease"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "0")
            {
                fields.SetField("Check Box34", "1");
                fields.SetField("Check Box35", "0");
                fields.SetField("Check Box36", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "1")
            {
                fields.SetField("Check Box34", "0");
                fields.SetField("Check Box35", "1");
                fields.SetField("Check Box36", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box34"].ToString() == "2")
            {
                fields.SetField("Check Box34", "0");
                fields.SetField("Check Box35", "0");
                fields.SetField("Check Box36", "1");
            }
            if (ds1.Tables[0].Rows[0]["Check Box24"].ToString() == "1")
            {
                fields.SetField("Check Box23", "1");
                fields.SetField("Check Box24", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box24"].ToString() == "0")
            {
                fields.SetField("Check Box23", "0");
                fields.SetField("Check Box24", "1");
            }
            if (ds1.Tables[0].Rows[0]["Check Box21"].ToString() == "1")
            {
                fields.SetField("Check Box21", "1");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box21"].ToString() == "0")
            {
                fields.SetField("Check Box21", "0");
            }
            if (ds1.Tables[0].Rows[0]["a Your evaluation"].ToString() == null || ds1.Tables[0].Rows[0]["a Your evaluation"].ToString() == "")
            {
                fields.SetField("a Your evaluation", "-");
            }
            else
            {
                fields.SetField("a Your evaluation", ds1.Tables[0].Rows[0]["a Your evaluation"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString() == null || ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString() == "")
            {
                fields.SetField("b 1 Patients condition and progress", "-");
            }
            else
            {
                fields.SetField("b 1 Patients condition and progress", ds1.Tables[0].Rows[0]["b 1 Patients condition and progress"].ToString());
            }

            if (ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString() == null || ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString() == "")
            {
                fields.SetField("Treatment and planned future treatment", "-");
            }
            else
            {
                fields.SetField("Treatment and planned future treatment", ds1.Tables[0].Rows[0]["Treatment and planned future treatment"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "1")
            {
                fields.SetField("Check Box22", "1");
                fields.SetField("Check Box25", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "0")
            {
                fields.SetField("Check Box22", "0");
                fields.SetField("Check Box25", "1");
            }

            if (ds1.Tables[0].Rows[0]["Text44"].ToString() == null || ds1.Tables[0].Rows[0]["Text44"].ToString() == "")
            {
                fields.SetField("Text44", "-");
            }
            else
            {
                fields.SetField("Text44", ds1.Tables[0].Rows[0]["Text44"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text45"].ToString() == null || ds1.Tables[0].Rows[0]["Text45"].ToString() == "")
            {
                fields.SetField("Text45", "-");
            }
            else
            {
                fields.SetField("Text45", ds1.Tables[0].Rows[0]["Text45"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString() == null || ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString() == "")
            {
                fields.SetField("4 Dates of visits on which this report is based", "-");
            }
            else
            {
                fields.SetField("4 Dates of visits on which this report is based", ds1.Tables[0].Rows[0]["4 Dates of visits on which this report is based"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString() == null || ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString() == "")
            {
                fields.SetField("If no was patient referred back to attending doctor D Yes D No", "-");
            }
            else
            {
                fields.SetField("If no was patient referred back to attending doctor D Yes D No", ds1.Tables[0].Rows[0]["If no was patient referred back to attending doctor D Yes D No"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Check Box26"].ToString() == "1")
            {
                fields.SetField("Check Box26", "1");
                fields.SetField("Check Box27", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box26"].ToString() == "0")
            {
                fields.SetField("Check Box26", "0");
                fields.SetField("Check Box27", "1");
            }
            if (ds1.Tables[0].Rows[0]["H_2"].ToString() == null || ds1.Tables[0].Rows[0]["H_2"].ToString() == "")
            {
                fields.SetField("H_2", "-");
            }
            else
            {
                fields.SetField("H_2", ds1.Tables[0].Rows[0]["H_2"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Check Box28"].ToString() == "1")
            {
                fields.SetField("Check Box28", "1");
                fields.SetField("Check Box29", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box28"].ToString() == "0")
            {
                fields.SetField("Check Box28", "0");
                fields.SetField("Check Box29", "1");
            }
            if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "1")
            {
                fields.SetField("Check Box22", "1");
                fields.SetField("Check Box25", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box22"].ToString() == "0")
            {
                fields.SetField("Check Box22", "0");
                fields.SetField("Check Box25", "1");
            }
            if (ds1.Tables[0].Rows[0]["Text37"].ToString() == null || ds1.Tables[0].Rows[0]["Text37"].ToString() == "")
            {
                fields.SetField("Text37", "-");
            }
            else
            {
                fields.SetField("Text37", ds1.Tables[0].Rows[0]["Text37"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["Text38"].ToString() == null || ds1.Tables[0].Rows[0]["Text38"].ToString() == "")
            {
                fields.SetField("Text38", "-");
            }
            else
            {
                fields.SetField("Text38", ds1.Tables[0].Rows[0]["Text38"].ToString());
            }

            if (ds1.Tables[0].Rows[0]["Check Box40"].ToString() == "True")
            {
                fields.SetField("Check Box40", "1");
                fields.SetField("Check Box41", "0");
            }
            else if (ds1.Tables[0].Rows[0]["Check Box40"].ToString() == "False")
            {
                fields.SetField("Check Box40", "0");
                fields.SetField("Check Box41", "1");
            }

            if (ds1.Tables[0].Rows[0]["Text39"].ToString() == null || ds1.Tables[0].Rows[0]["Text39"].ToString() == "")
            {
                fields.SetField("TELEPHONE NO_2", "-");
            }
            else
            {
                fields.SetField("TELEPHONE NO_2", ds1.Tables[0].Rows[0]["Text39"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString() == null || ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString() == "")
            {
                fields.SetField("9 NYS License Number", "-");
            }
            else
            {
                fields.SetField("9 NYS License Number", ds1.Tables[0].Rows[0]["9 NYS License Number"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString() == null || ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString() == "")
            {
                fields.SetField("Text39", "-");
            }
            else
            {
                fields.SetField("Text39", ds1.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString() == null || ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString() == "")
            {
                fields.SetField("10 Patients Account Number", "-");
            }
            else
            {
                fields.SetField("10 Patients Account Number", ds1.Tables[0].Rows[0]["10 Patients Account Number"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString() == null || ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString() == "")
            {
                fields.SetField("WCB CASE NORow1", "-");
            }
            else
            {
                fields.SetField("WCB CASE NORow1", ds1.Tables[0].Rows[0]["WCB_NORow1"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString() == "")
            {
                fields.SetField("15 Therapists Name Address  Phone No", "-");
            }
            else
            {
                fields.SetField("15 Therapists Name Address  Phone No", ds1.Tables[0].Rows[0]["15 Therapists Name Address  Phone No"].ToString());
            }
            if (ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == null || ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString() == "")
            {
                fields.SetField("16 Therapists Billing Name Address  Phone No", "-");
            }
            else
            {
                fields.SetField("16 Therapists Billing Name Address  Phone No", ds1.Tables[0].Rows[0]["16 Therapists Billing Name Address  Phone No"].ToString());
            }


            if (ds_table.Tables[0].Rows.Count > 0)
            {
                string Fmmtext = "FmmText";
                string FddText = "FddText";
                string FyyText = "FyyText";
                string TmmText = "TmmText";
                string TddText = "TddText";
                string TyyText = "TyyText";
                string BText = "BText";
                string DCText = "DCText";
                string DMText = "DMText";
                string EText = "DC";
                string FText = "FText";
                string GText = "GText";
                string HText = "HText";
                string IText = "IText";
                for (int i = 0; i < ds_table.Tables[0].Rows.Count; i++)
                {

                    fields.SetField(Fmmtext + i.ToString(), ds_table.Tables[0].Rows[i]["MONTH"].ToString());

                    fields.SetField(FddText + i.ToString(), ds_table.Tables[0].Rows[i]["DAY"].ToString());

                    fields.SetField(FyyText + i.ToString(), ds_table.Tables[0].Rows[i]["YEAR"].ToString());

                    fields.SetField(TmmText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_MONTH"].ToString());

                    fields.SetField(TddText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_DAY"].ToString());

                    fields.SetField(TyyText + i.ToString(), ds_table.Tables[0].Rows[i]["TO_YEAR"].ToString());

                    fields.SetField(BText + i.ToString(), ds_table.Tables[0].Rows[i]["PLACE_OF_SERVICE"].ToString());

                    fields.SetField(DCText + i.ToString(), ds_table.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());

                    fields.SetField(EText + i.ToString(), ds_table.Tables[0].Rows[i]["DC_1"].ToString());

                    fields.SetField(FText + i.ToString(), ds_table.Tables[0].Rows[i]["FL_AMOUNT"].ToString());

                    fields.SetField(GText + i.ToString(), ds_table.Tables[0].Rows[i]["I_UNIT"].ToString());

                    fields.SetField(IText + i.ToString(), ds_table.Tables[0].Rows[i]["ZIP_CODE"].ToString());
                }
                fields.SetField("11 Total Charges", ds_table.Tables[0].Rows[0]["BILL_AMOUNT"].ToString());
                fields.SetField("12.Amt.Paid", ds_table.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
                fields.SetField("13.Balance", ds_table.Tables[0].Rows[0]["BALANCE"].ToString());
                fields.SetField("1", ds_table.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString());
            }

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] u = url.Split('/');
            string str = "";
            for (int i = 0; i < u.Length; i++)
            {
                if (i < 3)
                {
                    if (i == 0)
                    {
                        str = u[i];
                    }
                    else
                        str = str + "/" + u[i];
                }
            }


            fields.RenameField("OCCUPATIONAL THERAPISTS REPORT", "OCCUPATIONAL THERAPISTS REPORT" + szNewName);
            fields.RenameField("PHYSICAL THERAPISTS REPORT", "PHYSICAL THERAPISTS REPORT" + szNewName);
            fields.RenameField("Check Box14", "Check Box14" + szNewName);
            fields.RenameField("Check Box15", "Check Box15" + szNewName);
            fields.RenameField("Check Box16", "Check Box16" + szNewName);
            fields.RenameField("Check Box17", "Check Box17" + szNewName);
            fields.RenameField("Check Box18", "Check Box18" + szNewName);
            fields.RenameField("WCB CASE NORow1", "WCB CASE NORow1" + szNewName);
            fields.RenameField("CARRIER CASE NO IF KNOWNRow1", "CARRIER CASE NO IF KNOWNRow1" + szNewName);
            fields.RenameField("DATE OF INJURYRow1", "DATE OF INJURYRow1" + szNewName);
            fields.RenameField("Text46", "Text46" + szNewName);
            fields.RenameField("ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1", "ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1" + szNewName);
            fields.RenameField("INJURED PERSONS SOCIAL SECURITY NUMBERRow1", "INJURED PERSONS SOCIAL SECURITY NUMBERRow1" + szNewName);
            fields.RenameField("INJURED PERSON", "INJURED PERSON" + szNewName);
            fields.RenameField("Text32", "Text32" + szNewName);
            fields.RenameField("TELEPHONE NO", "TELEPHONE NO" + szNewName);
            fields.RenameField("First Name Middle Initial Last NameEMPLOYER", "First Name Middle Initial Last NameEMPLOYER" + szNewName);
            fields.RenameField("ADDRESS Include Apt NoEMPLOYER", "ADDRESS Include Apt NoEMPLOYER" + szNewName);
            fields.RenameField("PATIENTS DATE OF BIRTH", "PATIENTS DATE OF BIRTH" + szNewName);
            fields.RenameField("INSURANCE CARRIER", "INSURANCE CARRIER" + szNewName);
            fields.RenameField("Text30", "Text30" + szNewName);
            fields.RenameField("REFERRING PHYSICIAN PODIATRIST", "REFERRING PHYSICIAN PODIATRIST" + szNewName);

            fields.RenameField("Text31", "Text31" + szNewName);
            fields.RenameField("TELEPHONE NO_2", "TELEPHONE NO_2" + szNewName);
            fields.RenameField("Check Box19", "Check Box19" + szNewName);
            fields.RenameField("Check Box20", "Check Box20" + szNewName);
            fields.RenameField("Text43", "Text43" + szNewName);
            fields.RenameField("Text33", "Text33" + szNewName);
            fields.RenameField("patient history of preexisting injury disease", "patient history of preexisting injury disease" + szNewName);
            fields.RenameField("Check Box34", "Check Box34" + szNewName);
            fields.RenameField("Check Box35", "Check Box35" + szNewName);
            fields.RenameField("Check Box36", "Check Box36" + szNewName);
            fields.RenameField("a Your evaluation", "a Your evaluation" + szNewName);
            fields.RenameField("b 1 Patients condition and progress", "b 1 Patients condition and progress" + szNewName);
            fields.RenameField("Check Box21", "Check Box21" + szNewName);
            fields.RenameField("Treatment and planned future treatment", "Treatment and planned future treatment" + szNewName);
            fields.RenameField("Check Box23", "Check Box23" + szNewName);
            fields.RenameField("Check Box24", "Check Box24" + szNewName);
            fields.RenameField("Text44", "Text44" + szNewName);
            fields.RenameField("Text45", "Text45" + szNewName);
            fields.RenameField("4 Dates of visits on which this report is based", "4 Dates of visits on which this report is based" + szNewName);
            fields.RenameField("If no was patient referred back to attending doctor D Yes D No", "If no was patient referred back to attending doctor D Yes D No" + szNewName);
            fields.RenameField("Check Box26", "Check Box26" + szNewName);
            fields.RenameField("Check Box27", "Check Box27" + szNewName);
            fields.RenameField("H_2", "H_2" + szNewName);
            fields.RenameField("Check Box28", "Check Box28" + szNewName);
            fields.RenameField("Check Box29", "Check Box29" + szNewName);
            fields.RenameField("Check Box22", "Check Box22" + szNewName);
            fields.RenameField("Check Box25", "Check Box25" + szNewName);

            fields.RenameField("Text37", "Text37" + szNewName);
            fields.RenameField("Text38", "Text38" + szNewName);
            fields.RenameField("1", "1" + szNewName);

            fields.RenameField("FmmText0", "FmmText0" + szNewName);
            fields.RenameField("FmmText1", "FmmText1" + szNewName);
            fields.RenameField("FmmText2", "FmmText2" + szNewName);
            fields.RenameField("FmmText3", "FmmText3" + szNewName);
            fields.RenameField("FmmText4", "FmmText4" + szNewName);
            fields.RenameField("FmmText5", "FmmText5" + szNewName);

            fields.RenameField("FddText0", "FddText0" + szNewName);
            fields.RenameField("FddText1", "FddText1" + szNewName);
            fields.RenameField("FddText2", "FddText2" + szNewName);
            fields.RenameField("FddText3", "FddText3" + szNewName);
            fields.RenameField("FddText4", "FddText4" + szNewName);
            fields.RenameField("FddText5", "FddText5" + szNewName);

            fields.RenameField("FyyText0", "FyyText0" + szNewName);
            fields.RenameField("FyyText1", "FyyText1" + szNewName);
            fields.RenameField("FyyText2", "FyyText2" + szNewName);
            fields.RenameField("FyyText3", "FyyText3" + szNewName);
            fields.RenameField("FyyText4", "FyyText4" + szNewName);
            fields.RenameField("FyyText5", "FyyText5" + szNewName);

            fields.RenameField("TmmText0", "TmmText0" + szNewName);
            fields.RenameField("TmmText1", "TmmText1" + szNewName);
            fields.RenameField("TmmText2", "TmmText2" + szNewName);
            fields.RenameField("TmmText3", "TmmText3" + szNewName);
            fields.RenameField("TmmText4", "TmmText4" + szNewName);
            fields.RenameField("TmmText5", "TmmText5" + szNewName);

            fields.RenameField("TddText0", "TddText0" + szNewName);
            fields.RenameField("TddText1", "TddText1" + szNewName);
            fields.RenameField("TddText2", "TddText2" + szNewName);
            fields.RenameField("TddText3", "TddText3" + szNewName);
            fields.RenameField("TddText4", "TddText4" + szNewName);
            fields.RenameField("TddText5", "TddText5" + szNewName);

            fields.RenameField("TyyText0", "TyyText0" + szNewName);
            fields.RenameField("TyyText1", "TyyText1" + szNewName);
            fields.RenameField("TyyText2", "TyyText2" + szNewName);
            fields.RenameField("TyyText3", "TyyText3" + szNewName);
            fields.RenameField("TyyText4", "TyyText4" + szNewName);
            fields.RenameField("TyyText5", "TyyText5" + szNewName);

            fields.RenameField("BText0", "BText0" + szNewName);
            fields.RenameField("BText1", "BText1" + szNewName);
            fields.RenameField("BText2", "BText2" + szNewName);
            fields.RenameField("BText3", "BText3" + szNewName);
            fields.RenameField("BText4", "BText4" + szNewName);
            fields.RenameField("BText5", "BText5" + szNewName);

            fields.RenameField("DCText0", "DCText0" + szNewName);
            fields.RenameField("DCText1", "DCText1" + szNewName);
            fields.RenameField("DCText2", "DCText2" + szNewName);
            fields.RenameField("DCText3", "DCText3" + szNewName);
            fields.RenameField("DCText4", "DCText4" + szNewName);
            fields.RenameField("DCText5", "DCText5" + szNewName);

            fields.RenameField("DMText0", "DMText0" + szNewName);

            fields.RenameField("DC0", "DC0" + szNewName);
            fields.RenameField("DC1", "DC1" + szNewName);
            fields.RenameField("DC2", "DC2" + szNewName);
            fields.RenameField("DC3", "DC3" + szNewName);
            fields.RenameField("DC4", "DC4" + szNewName);
            fields.RenameField("DC5", "DC5" + szNewName);
            fields.RenameField("FText0", "FText0" + szNewName);
            fields.RenameField("FText1", "FText1" + szNewName);
            fields.RenameField("FText2", "FText2" + szNewName);
            fields.RenameField("FText3", "FText3" + szNewName);
            fields.RenameField("FText4", "FText4" + szNewName);
            fields.RenameField("FText5", "FText5" + szNewName);

            fields.RenameField("GText0", "GText0" + szNewName);
            fields.RenameField("GText1", "GText1" + szNewName);
            fields.RenameField("GText2", "GText2" + szNewName);
            fields.RenameField("GText3", "GText3" + szNewName);
            fields.RenameField("GText4", "GText4" + szNewName);
            fields.RenameField("GText5", "GText5" + szNewName);

            fields.RenameField("HText0", "HText0" + szNewName);
            fields.RenameField("HText1", "HText1" + szNewName);
            fields.RenameField("HText2", "HText2" + szNewName);
            fields.RenameField("HText3", "HText3" + szNewName);
            fields.RenameField("HText4", "HText4" + szNewName);
            fields.RenameField("HText5", "HText5" + szNewName);

            fields.RenameField("IText0", "IText0" + szNewName);
            fields.RenameField("IText1", "IText1" + szNewName);
            fields.RenameField("IText2", "IText2" + szNewName);
            fields.RenameField("IText3", "IText3" + szNewName);
            fields.RenameField("IText4", "IText4" + szNewName);
            fields.RenameField("IText5", "IText5" + szNewName);

            fields.RenameField("Check Box40", "Check Box40" + szNewName);
            fields.RenameField("Check Box41", "Check Box41" + szNewName);
            fields.RenameField("Text39", "Text39" + szNewName);
            fields.RenameField("9 NYS License Number", "9 NYS License Number" + szNewName);
            fields.RenameField("10 Patients Account Number", "10 Patients Account Number" + szNewName);
            fields.RenameField("11 Total Charges", "11 Total Charges" + szNewName);
            fields.RenameField("12.Amt.Paid", "12.Amt.Paid" + szNewName);
            fields.RenameField("13.Balance", "13.Balance" + szNewName);
            fields.RenameField("15 Therapists Name Address  Phone No", "15 Therapists Name Address  Phone No" + szNewName);
            fields.RenameField("16 Therapists Billing Name Address  Phone No", "16 Therapists Billing Name Address  Phone No" + szNewName);
            stamper.Close();

        }
        #endregion
        string openPath = CmpName + "/" + szCaseID + "/Packet Document/" + strGenFileName;
        returnPath = openPath;
        return returnPath;

    }

}
