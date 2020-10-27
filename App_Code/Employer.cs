using log4net;
using Microsoft.SqlServer.Management.Common;
using SautinSoft;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// Summary description for Employer
/// </summary>
public class Employer
{

    private static ILog log;

    static Employer()
    {
        Employer.log = LogManager.GetLogger("Employer");
    }

    public Employer()
    {
    }

    public string GenerateHTML(DataSet objDSLT, DataSet objItemList)
    {
        decimal num;
        decimal num1;
        decimal num2;
        string str = "";
        string str1 = "";
        string str2 = "";
        decimal num3 = new decimal(0);
        string[] strArrays = new string[] { "<table width='100%' border='0' align='center'  cellpadding='0' cellspacing='0'><tr><td width='100%' style='border-color :White;'><table width='672' border='0' cellpadding='0' cellspacing='0' align='center'><tbody> <tr><td valign='top' width='400'></td><td valign='top' width='270'> </td></tr><tr> <td valign='bottom' width='400' style='font-size:18px font-family:Trebuchet MS' align='center'><b>", objDSLT.Tables[0].Rows[0]["office_name"].ToString(), "</b></td></tr><tr><td valign='bottom' width='400' style='font-size:12px font-family:Trebuchet MS' align='center'>", objDSLT.Tables[0].Rows[0]["sz_office_address"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["sz_city_state_zip"].ToString(), "<BR>PHONE:  ", objDSLT.Tables[0].Rows[0]["sz_officephone"].ToString(), "<BR>FAX:  ", objDSLT.Tables[0].Rows[0]["sz_officefax"].ToString(), "<BR>EMAIL:  ", objDSLT.Tables[0].Rows[0]["szofficemail"].ToString(), "</td></tr><td>&nbsp;</td><tr> </tr><td valign='top' width='270' style='font-size:12px font-family:Trebuchet MS' align='right'>Date :  ", objDSLT.Tables[0].Rows[0]["bill_date"].ToString(), "</td></tr><tr><td valign='top' width='270' style='font-size:12px font-family:Trebuchet MS' align='right'>TN :  ", objDSLT.Tables[0].Rows[0]["sztax_id"].ToString(), "</td></tr><tr><td>&nbsp;</td></tr></tbody></table>" };
        str = string.Concat(strArrays);
        strArrays = new string[] { "<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center'><tr><td style='font-size:14px;'>Bill To</td><td>&nbsp;</td><td align='left' >Employer</td></tr><tr> <td valign='top' width='100%' style='font-size:16px; font-family:Trebuchet MS;  align='left'><b>", objDSLT.Tables[0].Rows[0]["sz_patient_name"].ToString(), "</b></td><td>&nbsp;</td><td valign='top' width='200' style='font-size:12px' ><b>", objDSLT.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString(), "</b></td></tr><tr><td valign='bottom' width='400' style='font-size:12px font-family:Trebuchet MS'>", objDSLT.Tables[0].Rows[0]["sz_patient_address"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["sz_state_city_zip"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["sz_patient_phone"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["sz_patient_email"].ToString(), "</td><td>&nbsp;</td><td valign='bottom' width='400' style='font-size:12px font-family:Trebuchet MS'  align='left'>", objDSLT.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["Employer_State"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString(), "<BR> </td><tr><td>&nbsp;</td></tr><tr><td></td><td></td><td valign='top' width='270' style='font-size:12px font-family:Trebuchet MS'  align='left'>PATIENT:  ", objDSLT.Tables[0].Rows[0]["sz_patient_name"].ToString(), "<BR>&nbsp<BR>DATE OF ACCIDENT:  ", objDSLT.Tables[0].Rows[0]["dt_date_of_accident"].ToString(), "<BR> <BR></td></tr></table>" };
        str1 = string.Concat(strArrays);
        str2 = "<div align='center'><table width='672' border='1' cellpadding='0' cellspacing='0' Bordercolor='#000066'><tbody><tr><td width='84' style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF'>UNIT</td><td width='372' align='center' style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF'>DESCRIPTION</td> <td width='108'  style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF' align='center'>UNIT PRICE</td><td  width='110'  style='font-size:12px font-family:Trebuchet MS' align='center' style='background-color:#CECEFF' >TOTAL AMOUNT</td></tr>";
        int num4 = 0;
        for (int i = 0; i < objItemList.Tables[0].Rows.Count; i++)
        {
            num4++;
            string str3 = "";
            if (num4 >= 20)
            {
                str2 = string.Concat(str2, "</tbody></table></div><span style='page-break-before: always;'>");
                str2 = string.Concat(str2, "<div align='center'><table width='672' border='1' cellpadding='0' cellspacing='0' Bordercolor='#000066'><tbody><tr><td width='84' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF'>UNIT</td><td width='372' align='center' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF'>DESCRIPTION</td> <td width='108'  style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF' align='center'>UNIT PRICE</td><td  width='110'  style='font-size:10px font-family:Trebuchet MS' align='center' style='background-color:#CECEFF' >TOTAL AMOUNT</td></tr>");
                if ((objItemList.Tables[0].Rows[i]["I_UNIT"].ToString() == "NULL" ? false : objItemList.Tables[0].Rows[i]["I_UNIT"].ToString() != ""))
                {
                    if ((objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString() == "NULL" ? false : objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString() != ""))
                    {
                        num = Convert.ToDecimal(objItemList.Tables[0].Rows[i]["I_UNIT"].ToString());
                        num1 = Convert.ToDecimal(objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                        num2 = num * num1;
                        num3 = num3 + num2;
                        str3 = num2.ToString();
                    }
                }
                strArrays = new string[] { str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'>", objItemList.Tables[0].Rows[i]["I_UNIT"].ToString(), "</td><td width='372' style='font-size:12px font-family:Trebuchet MS'>", objItemList.Tables[0].Rows[i]["description"].ToString(), "</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString(), "</td><td width='110' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", str3, "</td></tr>" };
                str2 = string.Concat(strArrays);
                num4 = 0;
            }
            else
            {
                if ((objItemList.Tables[0].Rows[i]["I_UNIT"].ToString() == "NULL" ? false : objItemList.Tables[0].Rows[i]["I_UNIT"].ToString() != ""))
                {
                    if ((objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString() == "NULL" ? false : objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString() != ""))
                    {
                        num = Convert.ToDecimal(objItemList.Tables[0].Rows[i]["I_UNIT"].ToString());
                        num1 = Convert.ToDecimal(objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                        num2 = num * num1;
                        num3 = num3 + num2;
                        str3 = num2.ToString();
                    }
                }
                strArrays = new string[] { str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'>", objItemList.Tables[0].Rows[i]["I_UNIT"].ToString(), "</td><td width='372' style='font-size:12px font-family:Trebuchet MS'>", objItemList.Tables[0].Rows[i]["description"].ToString(), "</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString(), "</td><td width='110' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", str3, "</td></tr>" };
                str2 = string.Concat(strArrays);
            }
        }
        num3.ToString();
        str2 = string.Concat(str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'></td><td width='372' style='font-size:12px font-family:Trebuchet MS'></td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='right'>TOTAL</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='right'>$", num3.ToString(), "</td></tr>");
        str2 = string.Concat(str2, "</tbody></table></div> <table width='672' border='0' cellpadding='0' cellspacing='0' align='center'><tr><td colspan='2' width='666' align='center'><tr><td  style='font-size:12px font-family:Trebuchet MS'><b> Make all checks payable to ", objDSLT.Tables[0].Rows[0]["office_name"].ToString(), "<BR>THANK YOU FOR YOUR BUSINESS!</b></td></tr></table></td></tr></td></tr></table>");
        return string.Concat(str, str1, str2);
    }

    public string GenerateHTML_LHR(DataSet objDSLT, DataSet objItemList)
    {
        decimal num;
        decimal num1;
        decimal num2;
        string str = "";
        string str1 = "";
        string str2 = "";
        decimal num3 = new decimal(0);
        string[] strArrays = new string[] { "<table width='100%' border='0' align='center'  cellpadding='0' cellspacing='0'><tr><td width='100%' style='border-color :White;'><table width='672' border='0' cellpadding='0' cellspacing='0' align='center'><tbody> <tr><td valign='top' width='400'></td><td valign='top' width='270'> </td></tr><tr> <td valign='bottom' width='400' style='font-size:18px font-family:Trebuchet MS' align='center'><b>", objDSLT.Tables[0].Rows[0]["office_name"].ToString(), "</b></td></tr><tr><td valign='bottom' width='400' style='font-size:12px font-family:Trebuchet MS' align='center'>", objDSLT.Tables[0].Rows[0]["sz_office_address"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["sz_city_state_zip"].ToString(), "<BR>PHONE:  ", objDSLT.Tables[0].Rows[0]["sz_officephone"].ToString(), "<BR>FAX:  ", objDSLT.Tables[0].Rows[0]["sz_officefax"].ToString(), "<BR>EMAIL:  ", objDSLT.Tables[0].Rows[0]["szofficemail"].ToString(), "</td></tr><td>&nbsp;</td><tr> </tr><td valign='top' width='270' style='font-size:12px font-family:Trebuchet MS' align='right'>Date :  ", objDSLT.Tables[0].Rows[0]["bill_date"].ToString(), "</td></tr><tr><td>&nbsp;</td></tr></tbody></table>" };
        str = string.Concat(strArrays);
        strArrays = new string[] { "<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center'><tr><td style='font-size:14px;'>Bill To :</td></tr><tr> <td valign='top' width='100%' style='font-size:14px; font-family:Trebuchet MS;  align='left'><b>", objDSLT.Tables[0].Rows[0]["sz_patient_name"].ToString(), "</b></td></tr><tr><td valign='bottom' width='400' style='font-size:12px font-family:Trebuchet MS'>", objDSLT.Tables[0].Rows[0]["sz_patient_address"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["sz_state_city_zip"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["sz_patient_phone"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["sz_patient_email"].ToString(), "</td></tr><tr><td style='font-size:14px;'>Attorney :</td></tr><tr><td valign='top' width='200' style='font-size:12px' ><b>", objDSLT.Tables[0].Rows[0]["Attorney_Name"].ToString(), "</b></td></tr><tr><td valign='bottom' width='400' style='font-size:12px font-family:Trebuchet MS'  align='left'>", objDSLT.Tables[0].Rows[0]["Attorney_Address"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["Attorney_State"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["Attorney_phone"].ToString(), "<BR>", objDSLT.Tables[0].Rows[0]["Attorney_email"].ToString(), "</td></tr><tr><td style='font-size:14px;'>DATE OF ACCIDENT:  ", objDSLT.Tables[0].Rows[0]["dt_date_of_accident"].ToString(), "</td></tr></table>" };
        str1 = string.Concat(strArrays);
        str2 = "<div align='center'><table width='672' border='1' cellpadding='0' cellspacing='0' Bordercolor='#000066'><tbody><tr><td width='84' style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF'>UNIT</td><td width='372' align='center' style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF'>DESCRIPTION</td> <td width='108'  style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF' align='center'>UNIT PRICE</td><td  width='110'  style='font-size:12px font-family:Trebuchet MS' align='center' style='background-color:#CECEFF' >TOTAL AMOUNT</td></tr>";
        int num4 = 0;
        for (int i = 0; i < objItemList.Tables[0].Rows.Count; i++)
        {
            num4++;
            string str3 = "";
            if (num4 >= 20)
            {
                str2 = string.Concat(str2, "</tbody></table></div><span style='page-break-before: always;'>");
                str2 = string.Concat(str2, "<div align='center'><table width='672' border='1' cellpadding='0' cellspacing='0' Bordercolor='#000066'><tbody><tr><td width='84' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF'>UNIT</td><td width='372' align='center' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF'>DESCRIPTION</td> <td width='108'  style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF' align='center'>UNIT PRICE</td><td  width='110'  style='font-size:10px font-family:Trebuchet MS' align='center' style='background-color:#CECEFF' >TOTAL AMOUNT</td></tr>");
                if ((objItemList.Tables[0].Rows[i]["I_UNIT"].ToString() == "NULL" ? false : objItemList.Tables[0].Rows[i]["I_UNIT"].ToString() != ""))
                {
                    if ((objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString() == "NULL" ? false : objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString() != ""))
                    {
                        num = Convert.ToDecimal(objItemList.Tables[0].Rows[i]["I_UNIT"].ToString());
                        num1 = Convert.ToDecimal(objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                        num2 = num * num1;
                        num3 = num3 + num2;
                        str3 = num2.ToString();
                    }
                }
                strArrays = new string[] { str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'>", objItemList.Tables[0].Rows[i]["I_UNIT"].ToString(), "</td><td width='372' style='font-size:12px font-family:Trebuchet MS'>", objItemList.Tables[0].Rows[i]["description"].ToString(), "</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString(), "</td><td width='110' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", str3, "</td></tr>" };
                str2 = string.Concat(strArrays);
                num4 = 0;
            }
            else
            {
                if ((objItemList.Tables[0].Rows[i]["I_UNIT"].ToString() == "NULL" ? false : objItemList.Tables[0].Rows[i]["I_UNIT"].ToString() != ""))
                {
                    if ((objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString() == "NULL" ? false : objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString() != ""))
                    {
                        num = Convert.ToDecimal(objItemList.Tables[0].Rows[i]["I_UNIT"].ToString());
                        num1 = Convert.ToDecimal(objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                        num2 = num * num1;
                        num3 = num3 + num2;
                        str3 = num2.ToString();
                    }
                }
                strArrays = new string[] { str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'>", objItemList.Tables[0].Rows[i]["I_UNIT"].ToString(), "</td><td width='372' style='font-size:12px font-family:Trebuchet MS'>", objItemList.Tables[0].Rows[i]["description"].ToString(), "</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", objItemList.Tables[0].Rows[i]["FL_AMOUNT"].ToString(), "</td><td width='110' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", str3, "</td></tr>" };
                str2 = string.Concat(strArrays);
            }
        }
        num3.ToString();
        str2 = string.Concat(str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'></td><td width='372' style='font-size:12px font-family:Trebuchet MS'></td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='right'>TOTAL</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='right'>$", num3.ToString(), "</td></tr>");
        str2 = string.Concat(str2, "</tbody></table></div> <table width='672' border='0' cellpadding='0' cellspacing='0' align='center'><tr><td colspan='2' width='666' align='center'><tr><td  style='font-size:12px font-family:Trebuchet MS'><b> Make all checks payable to ", objDSLT.Tables[0].Rows[0]["office_name"].ToString(), "<BR>*If attorney information has been provided at the time of services, a copy of this invoice may be sent to your attorney informing them of the Employer assignment you have made.</b></td></tr></table></td></tr></td></tr></table>");
        return string.Concat(str, str1, str2);
    }

    protected string GeneratePDF(string strHtml, string szhtmlPath, string szpdfPath, string szBillNo, string dirpath, string szCompanyID, string szCaseID, string Szspecialty, string szUserName, string szCasNo, string szUserId)
    {
        string str = "";
        string str1 = "";
        try
        {
            PdfMetamorphosis pdfMetamorphosi = new PdfMetamorphosis();
            pdfMetamorphosi.Serial = "10007706603";
            string str2 = string.Concat(this.getFileName(szBillNo), ".htm");
            Employer.log.Debug(string.Concat("htmfilename ", str2));
            string str3 = string.Concat(this.getFileName(szBillNo), ".pdf");
            Employer.log.Debug(string.Concat("pdffilename ", str3));
            Employer.log.Debug(string.Concat("szhtmlPath ", szhtmlPath));
            Employer.log.Debug(string.Concat("szhtmlPath ", szhtmlPath));
            if (!Directory.Exists(szhtmlPath))
            {
                Directory.CreateDirectory(szhtmlPath);
            }
            Employer.log.Debug(string.Concat("szpdfPath ", szpdfPath));
            string str4 = string.Concat(ApplicationSettings.GetParameterValue("PhysicalBasePath"), szpdfPath);
            Employer.log.Debug(string.Concat("szpdfPathPhy ", str4));
            if (!Directory.Exists(str4))
            {
                Directory.CreateDirectory(str4);
            }
            StreamWriter streamWriter = new StreamWriter(string.Concat(szhtmlPath, str2));
            streamWriter.Write(strHtml);
            streamWriter.Close();
            Employer.log.Debug(string.Concat("Path ", ApplicationSettings.GetParameterValue("PhysicalBasePath"), szpdfPath, str3));
            pdfMetamorphosi.HtmlToPdfConvertFile(string.Concat(szhtmlPath, str2), string.Concat(ApplicationSettings.GetParameterValue("PhysicalBasePath"), szpdfPath, str3));
            str = string.Concat(ApplicationSettings.GetParameterValue("DocumentManagerURL"), dirpath, str3);
            string node = this.GetNode(szBillNo, szCompanyID, "BILL");
            str1 = (!(node == "NFVER") ? "NEW" : "OLD");
            ArrayList arrayLists = new ArrayList();
            if (!(str1 == "OLD"))
            {
                arrayLists.Add(szBillNo);
                arrayLists.Add(string.Concat(dirpath, str3));
                arrayLists.Add(szCompanyID);
                arrayLists.Add(szCaseID);
                arrayLists.Add(str3);
                arrayLists.Add(szpdfPath);
                arrayLists.Add(szUserName);
                arrayLists.Add(Szspecialty);
                arrayLists.Add("LN");
                arrayLists.Add(szCasNo);
                arrayLists.Add(node);
                this.saveGeneratedBillPath_New(arrayLists);
            }
            else
            {
                arrayLists.Add(szBillNo);
                arrayLists.Add(string.Concat(dirpath, str3));
                arrayLists.Add(szCompanyID);
                arrayLists.Add(szCaseID);
                arrayLists.Add(str3);
                arrayLists.Add(szpdfPath);
                arrayLists.Add(szUserName);
                arrayLists.Add(Szspecialty);
                arrayLists.Add("LN");
                arrayLists.Add(szCasNo);
                this.saveGeneratedBillPath(arrayLists);
            }
            DAO_NOTES_EO dAONOTESEO = new DAO_NOTES_EO();
            dAONOTESEO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
            dAONOTESEO.SZ_ACTIVITY_DESC = str3;
            DAO_NOTES_BO dAONOTESBO = new DAO_NOTES_BO();
            dAONOTESEO.SZ_USER_ID = szUserId;
            dAONOTESEO.SZ_CASE_ID = szCaseID;
            dAONOTESEO.SZ_COMPANY_ID = szCompanyID;
            dAONOTESBO.SaveActivityNotes(dAONOTESEO);
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            Employer.log.Debug(string.Concat("exGeneratePDF : ", exception.Message.ToString()));
            Employer.log.Debug(string.Concat("exGeneratePDF : ", exception.StackTrace.ToString()));
        }
        return str;
    }


    protected string GeneratePDF(string strHtml, string szhtmlPath, string szpdfPath, string szBillNo, string dirpath, string szCompanyID, string szCaseID, string Szspecialty, string szUserName, string szCasNo, string szUserId, ServerConnection conn)
    {
        string str = "";
        string str1 = "";
        try
        {
            PdfMetamorphosis pdfMetamorphosi = new PdfMetamorphosis();
            pdfMetamorphosi.Serial = "10007706603";
            string str2 = string.Concat(this.getFileName(szBillNo), ".htm");
            Employer.log.Debug(string.Concat("htmfilename ", str2));
            string str3 = string.Concat(this.getFileName(szBillNo), ".pdf");
            Employer.log.Debug(string.Concat("pdffilename ", str3));
            Employer.log.Debug(string.Concat("szhtmlPath ", szhtmlPath));
            Employer.log.Debug(string.Concat("szhtmlPath ", szhtmlPath));
            if (!Directory.Exists(szhtmlPath))
            {
                Directory.CreateDirectory(szhtmlPath);
            }
            Employer.log.Debug(string.Concat("szpdfPath ", szpdfPath));
            string str4 = string.Concat(ApplicationSettings.GetParameterValue("PhysicalBasePath"), szpdfPath);
            Employer.log.Debug(string.Concat("szpdfPathPhy ", str4));
            if (!Directory.Exists(str4))
            {
                Directory.CreateDirectory(str4);
            }
            StreamWriter streamWriter = new StreamWriter(string.Concat(szhtmlPath, str2));
            streamWriter.Write(strHtml);
            streamWriter.Close();
            Employer.log.Debug(string.Concat("Path ", ApplicationSettings.GetParameterValue("PhysicalBasePath"), szpdfPath, str3));
            pdfMetamorphosi.HtmlToPdfConvertFile(string.Concat(szhtmlPath, str2), string.Concat(ApplicationSettings.GetParameterValue("PhysicalBasePath"), szpdfPath, str3));
            str = string.Concat(ApplicationSettings.GetParameterValue("DocumentManagerURL"), dirpath, str3);
            string node = this.GetNode(szBillNo, szCompanyID, "BILL", conn);
            str1 = (!(node == "NFVER") ? "NEW" : "OLD");
            ArrayList arrayLists = new ArrayList();
            if (!(str1 == "OLD"))
            {
                arrayLists.Add(szBillNo);
                arrayLists.Add(string.Concat(dirpath, str3));
                arrayLists.Add(szCompanyID);
                arrayLists.Add(szCaseID);
                arrayLists.Add(str3);
                arrayLists.Add(szpdfPath);
                arrayLists.Add(szUserName);
                arrayLists.Add(Szspecialty);
                arrayLists.Add("LN");
                arrayLists.Add(szCasNo);
                arrayLists.Add(node);
                this.saveGeneratedBillPath_New(arrayLists, conn);
            }
            else
            {
                arrayLists.Add(szBillNo);
                arrayLists.Add(string.Concat(dirpath, str3));
                arrayLists.Add(szCompanyID);
                arrayLists.Add(szCaseID);
                arrayLists.Add(str3);
                arrayLists.Add(szpdfPath);
                arrayLists.Add(szUserName);
                arrayLists.Add(Szspecialty);
                arrayLists.Add("LN");
                arrayLists.Add(szCasNo);
                this.saveGeneratedBillPath(arrayLists, conn);
            }
            DAO_NOTES_EO dAONOTESEO = new DAO_NOTES_EO();
            dAONOTESEO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
            dAONOTESEO.SZ_ACTIVITY_DESC = str3;
            DAO_NOTES_BO dAONOTESBO = new DAO_NOTES_BO();
            dAONOTESEO.SZ_USER_ID = szUserId;
            dAONOTESEO.SZ_CASE_ID = szCaseID;
            dAONOTESEO.SZ_COMPANY_ID = szCompanyID;
            dAONOTESBO.SaveActivityNotes(dAONOTESEO);
        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            Employer.log.Debug(string.Concat("exGeneratePDF : ", exception.Message.ToString()));
            Employer.log.Debug(string.Concat("exGeneratePDF : ", exception.StackTrace.ToString()));
            throw exception;
        }
        return str;
    }

    protected string GeneratePDFWithMuv(string strHtml, string szhtmlPath, string szpdfPath, string szBillNo, string dirpath, string szCompanyID, string szCaseID, string Szspecialty, string szUserName, string szCasNo, string szUserId)
    {
        Employer.log.Debug("Inside GeneratePDFWithMuv method.");
        string str = "";
        string str1 = "";
        try
        {
            PdfMetamorphosis pdfMetamorphosi = new PdfMetamorphosis();
            pdfMetamorphosi.Serial = "10007706603";
            string str2 = string.Concat(this.getFileName(szBillNo), ".htm");
            str1 = string.Concat(this.getFileName(szBillNo), ".pdf");
            Employer.log.Debug(string.Concat("pdffilename ", str1));
            Employer.log.Debug(string.Concat("szhtmlPath ", szhtmlPath));
            Employer.log.Debug(string.Concat("szhtmlPath ", szhtmlPath));
            if (!Directory.Exists(szhtmlPath))
            {
                Directory.CreateDirectory(szhtmlPath);
                Employer.log.Debug("Create Directory for szhtmlPath Successful.");
            }
            if (!Directory.Exists(szpdfPath))
            {
                Directory.CreateDirectory(szpdfPath);
                Employer.log.Debug("Create Directory for szpdfPath Successful.");
            }
            StreamWriter streamWriter = new StreamWriter(string.Concat(szhtmlPath, str2));
            streamWriter.Write(strHtml);
            streamWriter.Close();
            pdfMetamorphosi.HtmlToPdfConvertFile(string.Concat(szhtmlPath, str2), string.Concat(szpdfPath, str1));
            str = string.Concat(ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString(), dirpath, str1);
            Employer.log.Debug(string.Concat("logicalpath ", str));
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return str1;
    }

    public string GenratePdfForEmployer(string szCompanyID, string szBillNo, string szSpecialty, string szCaseID, string szUserName, string szCasNo, string szUserId)
    {
        decimal num;
        decimal num1;
        decimal num2;
        string[] str;
        Employer.log.Debug("Inside GenratePdfForEmployer method");
        string str1 = "";
        string str2 = "";
        decimal num3 = new decimal(0);
        string str3 = "";
        string str4 = "";
        //try
        //{
        //    try
        //    {
        DataSet dataSet = new DataSet();
        DataSet EmployerBillInfo = new DataSet();
        dataSet = this.GetEmployerInfo(szCompanyID, szBillNo);
        EmployerBillInfo = this.GetEmployerBillInfo(szBillNo);
        string shortDateString = DateTime.Now.Date.ToShortDateString();
        string str5 = string.Concat(ConfigurationSettings.AppSettings["EmployerTemplate"].ToString(), szCompanyID, "\\Employer_Template.htm");
        string str6 = File.ReadAllText(str5);
        string str7 = str6.Replace("@PROVIDER_NAME", dataSet.Tables[0].Rows[0]["office_name"].ToString());
        string str8 = str7.Replace("@PROVIDER_STREET", dataSet.Tables[0].Rows[0]["sz_office_address"].ToString());
        string str9 = str8.Replace("@PROVIDER_CITY", dataSet.Tables[0].Rows[0]["sz_city_state_zip"].ToString());
        string str10 = str9.Replace("@PROVIDER_PHONE", dataSet.Tables[0].Rows[0]["sz_officephone"].ToString());
        string str11 = str10.Replace("@PROVIDER_FAX", dataSet.Tables[0].Rows[0]["sz_officefax"].ToString());
        string str12 = str11.Replace("@PROVIDER_EMAIL", dataSet.Tables[0].Rows[0]["szofficemail"].ToString());
        string str13 = str12.Replace("@DATE", shortDateString);
        string str14 = str13.Replace("@TN", dataSet.Tables[0].Rows[0]["sztax_id"].ToString());
        string str15 = str14.Replace("@PATIENT_NAME", dataSet.Tables[0].Rows[0]["sz_patient_name"].ToString());
        string str15_1 = str15.Replace("@sz_patient_address", dataSet.Tables[0].Rows[0]["sz_patient_address"].ToString());
        string str16 = str15_1.Replace("@PATIENT_CITY_ZIP", dataSet.Tables[0].Rows[0]["sz_state_city_zip"].ToString());
        string str17 = str16.Replace("@PATIENT_PHONE", dataSet.Tables[0].Rows[0]["sz_patient_phone"].ToString());
        string str18 = str17.Replace("@PATIENT_EMAIL", dataSet.Tables[0].Rows[0]["sz_patient_email"].ToString());
        string str19 = str18.Replace("@EMPLOYER_NAME", dataSet.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString());
        string str20 = str19.Replace("@EMPLOYER_ADDRESS", dataSet.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString());
        string str21 = str20.Replace("@Employer_State", dataSet.Tables[0].Rows[0]["Employer_State"].ToString());
        string str22 = str21.Replace("@EMPLOYER_PHONE", dataSet.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString());
        string str23 = str22.Replace("@BOTTOM_PATIENT_NAME", dataSet.Tables[0].Rows[0]["sz_patient_name"].ToString());
        string str24 = str23.Replace("@ACCIDENT_DATE", dataSet.Tables[0].Rows[0]["dt_date_of_accident"].ToString());
        str24 = str24.Replace("@BILL_NUMBER", szBillNo);
        if (dataSet.Tables[0].Columns.Contains("sz_office_street"))
        {
            str24 = str24.Replace("@PROVIDER_ADDRESS", dataSet.Tables[0].Rows[0]["sz_office_street"].ToString());
        }
        str2 = "<table width='40%' border='1' cellpadding='0' cellspacing='0' Bordercolor='#000066'><tbody><tr><td width='84' style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF'>UNIT</td><td width='372' align='center' style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF'>DESCRIPTION</td> <td width='108'  style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF' align='center'>UNIT PRICE</td><td  width='110'  style='font-size:12px font-family:Trebuchet MS' align='center' style='background-color:#CECEFF' >TOTAL AMOUNT</td></tr>";
        int num4 = 0;
        for (int i = 0; i < EmployerBillInfo.Tables[0].Rows.Count; i++)
        {
            num4++;
            string str25 = "";
            if (num4 >= 20)
            {
                str2 = string.Concat(str2, "</tbody></table><span style='page-break-before: always;'>");
                str2 = string.Concat(str2, "<table width='672' border='1' cellpadding='0' cellspacing='0' Bordercolor='#000066'><tbody><tr><td width='84' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF'>UNIT</td><td width='372' align='center' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF'>DESCRIPTION</td> <td width='108' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF' align='center'>UNIT PRICE</td><td  width='110' style='font-size:10px font-family:Trebuchet MS' align='center' style='background-color:#CECEFF' >TOTAL AMOUNT</td></tr>");
                if ((EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString() == "NULL" ? false : EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString() != ""))
                {
                    if ((EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString() == "NULL" ? false : EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString() != ""))
                    {
                        num = Convert.ToDecimal(EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString());
                        num1 = Convert.ToDecimal(EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                        num2 = num * num1;
                        num3 = num3 + num2;
                        str25 = num2.ToString();
                    }
                }
                str = new string[] { str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'>", EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString(), "</td><td width='372' style='font-size:12px font-family:Trebuchet MS'>", EmployerBillInfo.Tables[0].Rows[i]["description"].ToString(), "</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString(), "</td><td width='110' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", str25, "</td></tr>" };
                str2 = string.Concat(str);
                num4 = 0;
            }
            else
            {
                if ((EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString() == "NULL" ? false : EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString() != ""))
                {
                    if ((EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString() == "NULL" ? false : EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString() != ""))
                    {
                        num = Convert.ToDecimal(EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString());
                        num1 = Convert.ToDecimal(EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                        num2 = num * num1;
                        num3 = num3 + num2;
                        str25 = num2.ToString();
                    }
                }
                str = new string[] { str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'>", EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString(), "</td><td width='372' style='font-size:12px font-family:Trebuchet MS'>", EmployerBillInfo.Tables[0].Rows[i]["description"].ToString(), "</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString(), "</td><td width='110' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", str25, "</td></tr>" };
                str2 = string.Concat(str);
            }
        }
        num3.ToString();
        str2 = string.Concat(str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'></td><td width='372' style='font-size:12px font-family:Trebuchet MS'></td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='right'>TOTAL</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='right'>$", num3.ToString(), "</td></tr>");
        str2 = string.Concat(str2, "</tbody></table>");
        string str26 = str24.Replace("@PROC_TABLE", str2);
        string str27 = str26.Replace("@BOTTOM_PROVIDER_NAME", dataSet.Tables[0].Rows[0]["office_name"].ToString());
        string node = this.GetNode(szBillNo, szCompanyID, "BILL");
        Employer.log.Debug(string.Concat("NodeType : ", node));
        if (!(node == "NFVER"))
        {
            str = new string[] { this.GetCompanyName(szCompanyID), "/", szCaseID, "/No Fault File/Medicals/", szSpecialty, "/Bills/" };
            str3 = string.Concat(str);
            str4 = "NEW";
        }
        else
        {
            str = new string[] { this.GetCompanyName(szCompanyID), "/", szCaseID, "/No Fault File/Bills/", szSpecialty, "/" };
            str3 = string.Concat(str);
            str4 = "OLD";
        }
        Employer.log.Debug(string.Concat("CasseType : ", str4));
        Employer.log.Debug(string.Concat("strNewPath : ", str3));
        string str28 = str3;
        string str29 = str3;
        Employer.log.Debug(string.Concat("dirpath ", str29));
        str = new string[] { ApplicationSettings.GetParameterValue("PhysicalBasePath"), this.GetCompanyName(szCompanyID), "/", szCaseID, "/Packet Document/" };
        string str30 = string.Concat(str);
        Employer.log.Debug(string.Concat("szhtmlpath ", str30));
        str1 = this.GeneratePDF(str27, str30, str28, szBillNo, str29, szCompanyID, szCaseID, szSpecialty, szUserName, szCasNo, szUserId);
        //    }
        //    catch (Exception exception1)
        //    {
        //    }
        //}
        //finally
        //{
        //}
        return str1;
    }


    public string GenratePdfForEmployer(string szCompanyID, string szBillNo, string szSpecialty, string szCaseID, string szUserName, string szCasNo, string szUserId, ServerConnection conn)
    {
        decimal num;
        decimal num1;
        decimal num2;
        string[] str;
        Employer.log.Debug("Inside GenratePdfForEmployer method");
        string str1 = "";
        string str2 = "";
        decimal num3 = new decimal(0);
        string str3 = "";
        string str4 = "";
        //try
        //{
        //    try
        //    {
        DataSet dataSet = new DataSet();
        DataSet EmployerBillInfo = new DataSet();
        dataSet = this.GetEmployerInfo(szCompanyID, szBillNo, conn);
        EmployerBillInfo = this.GetEmployerBillInfo(szBillNo, conn);
        string shortDateString = DateTime.Now.Date.ToShortDateString();
        string str5 = string.Concat(ConfigurationSettings.AppSettings["EmployerTemplate"].ToString(), szCompanyID, "\\Employer_Template.htm");
        string str6 = File.ReadAllText(str5);
        string str7 = str6.Replace("@PROVIDER_NAME", dataSet.Tables[0].Rows[0]["office_name"].ToString());
        string str8 = str7.Replace("@PROVIDER_STREET", dataSet.Tables[0].Rows[0]["sz_office_address"].ToString());
        string str9 = str8.Replace("@PROVIDER_CITY", dataSet.Tables[0].Rows[0]["sz_city_state_zip"].ToString());
        string str10 = str9.Replace("@PROVIDER_PHONE", dataSet.Tables[0].Rows[0]["sz_officephone"].ToString());
        string str11 = str10.Replace("@PROVIDER_FAX", dataSet.Tables[0].Rows[0]["sz_officefax"].ToString());
        string str12 = str11.Replace("@PROVIDER_EMAIL", dataSet.Tables[0].Rows[0]["szofficemail"].ToString());
        string str13 = str12.Replace("@DATE", shortDateString);
        string str14 = str13.Replace("@TN", dataSet.Tables[0].Rows[0]["sztax_id"].ToString());
        string str15 = str14.Replace("@PATIENT_NAME", dataSet.Tables[0].Rows[0]["sz_patient_name"].ToString());
        string str15_1 = str15.Replace("@sz_patient_address", dataSet.Tables[0].Rows[0]["sz_patient_address"].ToString());
        string str16 = str15_1.Replace("@PATIENT_CITY_ZIP", dataSet.Tables[0].Rows[0]["sz_state_city_zip"].ToString());
        string str17 = str16.Replace("@PATIENT_PHONE", dataSet.Tables[0].Rows[0]["sz_patient_phone"].ToString());
        string str18 = str17.Replace("@PATIENT_EMAIL", dataSet.Tables[0].Rows[0]["sz_patient_email"].ToString());
        string str19 = str18.Replace("@EMPLOYER_NAME", dataSet.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString());
        string str20 = str19.Replace("@EMPLOYER_ADDRESS", dataSet.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString());
        string str21 = str20.Replace("@Employer_State", dataSet.Tables[0].Rows[0]["Employer_State"].ToString());
        string str22 = str21.Replace("@EMPLOYER_PHONE", dataSet.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString());
        string str23 = str22.Replace("@BOTTOM_PATIENT_NAME", dataSet.Tables[0].Rows[0]["sz_patient_name"].ToString());
        string str24 = str23.Replace("@ACCIDENT_DATE", dataSet.Tables[0].Rows[0]["dt_date_of_accident"].ToString());
        str24 = str24.Replace("@BILL_NUMBER", szBillNo);
        if (dataSet.Tables[0].Columns.Contains("sz_office_street"))
        {
            str24 = str24.Replace("@PROVIDER_ADDRESS", dataSet.Tables[0].Rows[0]["sz_office_street"].ToString());
        }

        str2 = "<table width='40%' border='1' cellpadding='0' cellspacing='0' Bordercolor='#000066'><tbody><tr><td width='84' style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF'>UNIT</td><td width='372' align='center' style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF'>DESCRIPTION</td> <td width='108'  style='font-size:12px font-family:Trebuchet MS' style='background-color:#CECEFF' align='center'>UNIT PRICE</td><td  width='110'  style='font-size:12px font-family:Trebuchet MS' align='center' style='background-color:#CECEFF' >TOTAL AMOUNT</td></tr>";
        int num4 = 0;
        for (int i = 0; i < EmployerBillInfo.Tables[0].Rows.Count; i++)
        {
            num4++;
            string str25 = "";
            if (num4 >= 20)
            {
                str2 = string.Concat(str2, "</tbody></table><span style='page-break-before: always;'>");
                str2 = string.Concat(str2, "<table width='672' border='1' cellpadding='0' cellspacing='0' Bordercolor='#000066'><tbody><tr><td width='84' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF'>UNIT</td><td width='372' align='center' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF'>DESCRIPTION</td> <td width='108' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF' align='center'>UNIT PRICE</td><td  width='110' style='font-size:10px font-family:Trebuchet MS' align='center' style='background-color:#CECEFF' >TOTAL AMOUNT</td></tr>");
                if ((EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString() == "NULL" ? false : EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString() != ""))
                {
                    if ((EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString() == "NULL" ? false : EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString() != ""))
                    {
                        num = Convert.ToDecimal(EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString());
                        num1 = Convert.ToDecimal(EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                        num2 = num * num1;
                        num3 = num3 + num2;
                        str25 = num2.ToString();
                    }
                }
                str = new string[] { str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'>", EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString(), "</td><td width='372' style='font-size:12px font-family:Trebuchet MS'>", EmployerBillInfo.Tables[0].Rows[i]["description"].ToString(), "</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString(), "</td><td width='110' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", str25, "</td></tr>" };
                str2 = string.Concat(str);
                num4 = 0;
            }
            else
            {
                if ((EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString() == "NULL" ? false : EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString() != ""))
                {
                    if ((EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString() == "NULL" ? false : EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString() != ""))
                    {
                        num = Convert.ToDecimal(EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString());
                        num1 = Convert.ToDecimal(EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                        num2 = num * num1;
                        num3 = num3 + num2;
                        str25 = num2.ToString();
                    }
                }
                str = new string[] { str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'>", EmployerBillInfo.Tables[0].Rows[i]["I_UNIT"].ToString(), "</td><td width='372' style='font-size:12px font-family:Trebuchet MS'>", EmployerBillInfo.Tables[0].Rows[i]["description"].ToString(), "</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", EmployerBillInfo.Tables[0].Rows[i]["FL_AMOUNT"].ToString(), "</td><td width='110' style='font-size:12px font-family:Trebuchet MS' align='Right'>$", str25, "</td></tr>" };
                str2 = string.Concat(str);
            }
        }
        num3.ToString();
        str2 = string.Concat(str2, "<tr><td width='84' style='font-size:12px font-family:Trebuchet MS' align='left'></td><td width='372' style='font-size:12px font-family:Trebuchet MS'></td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='right'>TOTAL</td><td width='108' style='font-size:12px font-family:Trebuchet MS' align='right'>$", num3.ToString(), "</td></tr>");
        str2 = string.Concat(str2, "</tbody></table>");
        string str26 = str24.Replace("@PROC_TABLE", str2);
        string str27 = str26.Replace("@BOTTOM_PROVIDER_NAME", dataSet.Tables[0].Rows[0]["office_name"].ToString());
        string node = this.GetNode(szBillNo, szCompanyID, "BILL", conn);
        Employer.log.Debug(string.Concat("NodeType : ", node));
        if (!(node == "NFVER"))
        {
            str = new string[] { this.GetCompanyName(szCompanyID), "/", szCaseID, "/No Fault File/Medicals/", szSpecialty, "/Bills/" };
            str3 = string.Concat(str);
            str4 = "NEW";
        }
        else
        {
            str = new string[] { this.GetCompanyName(szCompanyID), "/", szCaseID, "/No Fault File/Bills/", szSpecialty, "/" };
            str3 = string.Concat(str);
            str4 = "OLD";
        }
        Employer.log.Debug(string.Concat("CasseType : ", str4));
        Employer.log.Debug(string.Concat("strNewPath : ", str3));
        string str28 = str3;
        string str29 = str3;
        Employer.log.Debug(string.Concat("dirpath ", str29));
        str = new string[] { ApplicationSettings.GetParameterValue("PhysicalBasePath"), this.GetCompanyName(szCompanyID), "/", szCaseID, "/Packet Document/" };
        string str30 = string.Concat(str);
        Employer.log.Debug(string.Concat("szhtmlpath ", str30));
        str1 = this.GeneratePDF(str27, str30, str28, szBillNo, str29, szCompanyID, szCaseID, szSpecialty, szUserName, szCasNo, szUserId, conn);
        //    }
        //    catch (Exception exception1)
        //    {
        //    }
        //}
        //finally
        //{
        //}
        return str1;
    }

    public string GenratePdfForEmployerWithMuv(string szCompanyID, string szBillNo, string szSpecialty, string szCaseID, string szUserName, string szCasNo, string szUserId)
    {
        string[] companyName;
        Employer.log.Debug("Inside GenratePdfForEmployerWithMuv method");
        string str = "";
        string str1 = "";
        string str2 = "";
        //try
        //{
        //    try
        //    {
        DataSet dataSet = new DataSet();
        DataSet dataSet1 = new DataSet();
        dataSet = this.GetEmployerInfo(szCompanyID, szBillNo);
        string str3 = this.GenerateHTML(dataSet, this.GetEmployerBillInfo(szBillNo));
        string node = this.GetNode(szBillNo, szCompanyID, "BILL");
        Employer.log.Debug(string.Concat("NodeType : ", node));
        if (!(node == "NFVER"))
        {
            companyName = new string[] { this.GetCompanyName(szCompanyID), "/", szCaseID, "/No Fault File/Medicals/", szSpecialty, "/Bills/" };
            str1 = string.Concat(companyName);
            str2 = "NEW";
        }
        else
        {
            companyName = new string[] { this.GetCompanyName(szCompanyID), "/", szCaseID, "/No Fault File/Bills/", szSpecialty, "/" };
            str1 = string.Concat(companyName);
            str2 = "OLD";
        }
        Employer.log.Debug(string.Concat("Case Type : ", str2));
        Employer.log.Debug(string.Concat("New Path : ", str1));
        string str4 = string.Concat(this.getPhysicalPath(), str1);
        Employer.log.Debug(string.Concat("szpdfpath: ", str4));
        string str5 = str1;
        companyName = new string[] { this.getPhysicalPath(), this.GetCompanyName(szCompanyID), "/", szCaseID, "/Packet Document/" };
        string str6 = string.Concat(companyName);
        str = this.GeneratePDFWithMuv(str3, str6, str4, szBillNo, str5, szCompanyID, szCaseID, szSpecialty, szUserName, szCasNo, szUserId);
        //    }
        //    catch (Exception exception)
        //    {
        //    }
        //}
        //finally
        //{
        //}
        return str;
    }

    public string GetCompanyName(string szCompanyId)
    {
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        string str1 = "";
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GET_COMPANY_NAME", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                str1 = dataSet.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return str1;
    }

    private string getFileName(string p_szBillNumber)
    {
        string pSzBillNumber = "";
        pSzBillNumber = p_szBillNumber;
        DateTime now = DateTime.Now;
        string str = string.Concat(p_szBillNumber, "_", now.ToString("yyyyMMddHHmmssms"));
        return str;
    }

    public DataSet GetEmployerBillInfo(string szBillNo)
    {
        DataSet dataSet = new DataSet();
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("sp_get_bill_info", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@sz_bill_number", szBillNo);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
    }
    public DataSet GetEmployerBillInfo(string szBillNo, ServerConnection sqlConnection)
    {
        DataSet dataSet = new DataSet();
        //string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        //SqlConnection sqlConnection = new SqlConnection(str);
        try
        {
            try
            {
                string Query = "";
                Query = Query + "Exec sp_get_bill_info ";
                Query = Query + string.Format("{0}='{1}'{2}", "@sz_bill_number", szBillNo, ",");

                Query = Query.TrimEnd(',');
                dataSet = sqlConnection.ExecuteWithResults(Query);
            }
            catch (Exception ex)
            {
                throw ex;
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            //if (sqlConnection.State == ConnectionState.Open)
            //{
            //    sqlConnection.Close();
            //}
        }
        return dataSet;
    }

    public DataSet GetEmployerInfo(string szCompanyID, string szBillNo)
    {
        DataSet dataSet = new DataSet();
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("sp_Employer_bill_info", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@sz_bill_number", szBillNo);
                sqlCommand.Parameters.AddWithValue("@sz_company_id", szCompanyID);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
    }
    public DataSet GetEmployerInfo(string szCompanyID, string szBillNo, ServerConnection sqlConnection)
    {
        DataSet dataSet = new DataSet();
        //string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        //SqlConnection sqlConnection = new SqlConnection(str);
        try
        {
            try
            {
                string Query = "";
                Query = Query + "Exec sp_Employer_bill_info ";
                Query = Query + string.Format("{0}='{1}'{2}", "@sz_bill_number", szBillNo, ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@sz_company_id", szCompanyID, ",");

                Query = Query.TrimEnd(',');
                dataSet = sqlConnection.ExecuteWithResults(Query);
            }
            catch (Exception ex)
            {
                throw ex;
                // Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            //if (sqlConnection.State == ConnectionState.Open)
            //{
            //    sqlConnection.Close();
            //}
        }
        return dataSet;
    }
    public string GetNode(string sz_billno, string sz_companyid, string sz_process)
    {
        string str;
        string str1 = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str1);
        string str2 = "";
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_CHECK_NODE", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_billno);
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
                sqlCommand.Parameters.AddWithValue("@SZ_PROCESS", sz_process);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    str2 = Convert.ToString(sqlDataReader.GetValue(0).ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                str = null;
                return str;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        str = str2;
        return str;
    }

    public string GetNode(string sz_billno, string sz_companyid, string sz_process, ServerConnection sqlConnection)
    {
        string str;
        SqlDataReader sqlDataReader = null;
        string str2 = "";
        try
        {
            try
            {
                string Query = "";
                Query = Query + "Exec SP_CHECK_NODE ";
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", sz_billno, ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", sz_companyid, ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_PROCESS", sz_process, ",");

                Query = Query.TrimEnd(',');



                sqlDataReader = sqlConnection.ExecuteReader(Query);
                while (sqlDataReader.Read())
                {
                    str2 = Convert.ToString(sqlDataReader.GetValue(0).ToString());
                }
                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //str = null;
                //return str;
                throw ex;
            }
        }
        finally
        {
            if (sqlDataReader != null && !sqlDataReader.IsClosed)
            {
                sqlDataReader.Close();
            }
        }
        str = str2;
        return str;
    }


    public string getPhysicalPath()
    {
        string str = "";
        string str1 = "";
        str1 = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str1);
        try
        {
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = (new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", sqlConnection)).ExecuteReader();
                while (sqlDataReader.Read())
                {
                    str = sqlDataReader["parametervalue"].ToString();
                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
    }

    public void saveGeneratedBillPath(ArrayList objAL)
    {
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        sqlConnection = new SqlConnection(str);
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_TXN_BILL_GENERATED", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[0].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_PATH", objAL[1].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", objAL[3].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_NAME", objAL[4].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_FILE_PATH", objAL[5].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_USER_NAME", objAL[6].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[7].ToString());
                sqlCommand.Parameters.AddWithValue("@CASE_TYPE", objAL[8].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_CASE_NO", objAL[9].ToString());
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
    }
    public void saveGeneratedBillPath(ArrayList objAL, ServerConnection sqlConnection)
    {
        //string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        //SqlConnection sqlConnection = new SqlConnection(str);
        //sqlConnection = new SqlConnection(str);
        try
        {
            try
            {
                // sqlConnection.Open();
                string Query = "";
                Query = Query + "Exec SP_TXN_BILL_GENERATED ";
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", objAL[0].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_PATH", objAL[1].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", objAL[2].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_CASE_ID", objAL[3].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NAME", objAL[4].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_FILE_PATH", objAL[5].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_USER_NAME", objAL[6].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_SPECIALITY", objAL[7].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@CASE_TYPE", objAL[8].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_CASE_NO", objAL[9].ToString(), ",");

                Query = Query.TrimEnd(',');
                sqlConnection.ExecuteNonQuery(Query);
            }
            catch (SqlException ex)
            {
                throw ex;
                // Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            //if (sqlConnection.State == ConnectionState.Open)
            //{
            //    sqlConnection.Close();
            //}
        }
    }

    public void saveGeneratedBillPath_New(ArrayList objAL)
    {
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_TXN_BILL_GENERATED_NEW", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[0].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_PATH", objAL[1].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", objAL[3].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_NAME", objAL[4].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_FILE_PATH", objAL[5].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_USER_NAME", objAL[6].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[7].ToString());
                sqlCommand.Parameters.AddWithValue("@CASE_TYPE", objAL[8].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_CASE_NO", objAL[9].ToString());
                sqlCommand.Parameters.AddWithValue("@SZ_NODE_TYPE", objAL[10].ToString());
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
    }
    public void saveGeneratedBillPath_New(ArrayList objAL, ServerConnection sqlConnection)
    {
        //string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        //SqlConnection sqlConnection = new SqlConnection(str);
        try
        {
            try
            {
                // sqlConnection.Open();
                string Query = "";
                Query = Query + "Exec SP_TXN_BILL_GENERATED_NEW ";
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", objAL[0].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_PATH", objAL[1].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", objAL[2].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_CASE_ID", objAL[3].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NAME", objAL[4].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_FILE_PATH", objAL[5].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_USER_NAME", objAL[6].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_SPECIALITY", objAL[7].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@CASE_TYPE", objAL[8].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_CASE_NO", objAL[9].ToString(), ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_NODE_TYPE", objAL[10].ToString(), ",");

                Query = Query.TrimEnd(',');
                sqlConnection.ExecuteNonQuery(Query);
            }
            catch (SqlException ex)
            {
                throw ex;
                // Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            //if (sqlConnection.State == ConnectionState.Open)
            //{
            //    sqlConnection.Close();
            //}
        }
    }
    public DataSet GetInvoicePayment(string sz_invoice_id, string SZ_COMPANY_ID)
    {
        DataSet dataSet = new DataSet();
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        sqlConnection.Open();
        SqlTransaction tr;
        tr = sqlConnection.BeginTransaction();


        try
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand("proc_get_invoice_payment_details", sqlConnection);
                sqlCommand.Transaction = tr;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@sz_invoice_id", sz_invoice_id);
                sqlCommand.Parameters.AddWithValue("@sz_company_id", SZ_COMPANY_ID);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
                tr.Commit();

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                tr.Rollback();
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
    }

    public DataSet saveInvoicePayment(ArrayList objAL)
    {
        DataSet dataSet = new DataSet();
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        sqlConnection.Open();
        SqlTransaction tr;
        tr = sqlConnection.BeginTransaction();
        try
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand("proc_save_invoice_payment", sqlConnection);
                sqlCommand.Transaction = tr;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@sz_invoice_id", objAL[0].ToString());
                sqlCommand.Parameters.AddWithValue("@sz_employer_id", objAL[1].ToString());
                sqlCommand.Parameters.AddWithValue("@sz_cheque_no", objAL[2].ToString());
                sqlCommand.Parameters.AddWithValue("@dt_cheque_date", objAL[3].ToString());
                sqlCommand.Parameters.AddWithValue("@mn_cheque_amount", objAL[4].ToString());
                sqlCommand.Parameters.AddWithValue("@sz_company_id", objAL[5].ToString());
                sqlCommand.Parameters.AddWithValue("@sz_updated_user_id", objAL[6].ToString());
                sqlCommand.Parameters.AddWithValue("@sz_payment_type", objAL[7].ToString());
                sqlCommand.Parameters.AddWithValue("@falg", objAL[8].ToString());
                sqlCommand.Parameters.AddWithValue("@I_Id", objAL[9].ToString());
                sqlCommand.Parameters.AddWithValue("@sz_notes", objAL[10].ToString());
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
                tr.Commit();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                tr.Rollback();
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
    }

    public DataSet DeleteInvoicePayment(ArrayList objAL)
    {
        DataSet dataSet = new DataSet();
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        sqlConnection.Open();
        SqlTransaction tr;
        tr = sqlConnection.BeginTransaction();
        try
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand("proc_delete_invoice_payment", sqlConnection);
                sqlCommand.Transaction = tr;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", objAL[0].ToString());
                sqlCommand.Parameters.AddWithValue("@sz_invoice_id ", objAL[1].ToString());
                sqlCommand.Parameters.AddWithValue("@sz_company_id ", objAL[2].ToString());

                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
                tr.Commit();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                tr.Rollback();
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
    }

    public int SavePayment(ArrayList objAL)
    {
        int iReturn=0;
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        
        try
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("PROC_EMPLOYER_INVOICE_PAYMENT", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_USRE_ID", objAL[1].ToString());
            sqlCommand.Parameters.AddWithValue("@MN_CHECK_AMOUNT", objAL[2].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_PAYMENT_TYPE", objAL[3].ToString());
            
            sqlCommand.Parameters.AddWithValue("@INVOICE_NO", objAL[4].ToString());
            sqlCommand.Parameters.AddWithValue("@DT_CHECK_DATE", objAL[5].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_EMPOLYER_ID", objAL[6].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_FLAG", "ADD");
            sqlCommand.Parameters.AddWithValue("@SZ_CHECK_NO", objAL[7].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_NOTES", objAL[8].ToString());
            iReturn =sqlCommand.ExecuteNonQuery();
        }
        catch (Exception)
        {

        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        return iReturn;
    }
    public DataSet GetPayment(string sz_invoice_id, string SZ_COMPANY_ID)
    {
        DataSet dataSet = new DataSet();
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        sqlConnection.Open();
        SqlTransaction tr;
        tr = sqlConnection.BeginTransaction();


        try
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand("PROC_EMPLOYER_INVOICE_PAYMENT", sqlConnection);
                sqlCommand.Transaction = tr;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@INVOICE_NO", sz_invoice_id);
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
                sqlCommand.Parameters.AddWithValue("@SZ_FLAG", "SELECT");
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
                tr.Commit();

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                tr.Rollback();
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
    }

    public int DeletePayment(ArrayList objAL,string companyid,string invoiceid)
    {
        int iReturn = 0;
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        sqlConnection.Open();
        SqlTransaction tr;
        tr = sqlConnection.BeginTransaction();
        try
        {
           
            for (int i = 0; i < objAL.Count; i++)
            {
                SqlCommand sqlCommand = new SqlCommand("PROC_EMPLOYER_INVOICE_PAYMENT", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
                sqlCommand.Parameters.AddWithValue("@I_ID", objAL[i].ToString());
                sqlCommand.Parameters.AddWithValue("@INVOICE_NO", invoiceid);
                sqlCommand.Parameters.AddWithValue("@SZ_FLAG", "DELETE");
                sqlCommand.Transaction = tr;
                iReturn = sqlCommand.ExecuteNonQuery();

            }


            tr.Commit();


        }
        catch (Exception ex)
        {
            iReturn = 0;
            tr.Rollback();

        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        return iReturn;
    }

    public int Update(ArrayList objAL)
    {
        int iReturn = 0;
        string str = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        sqlConnection.Open();
        SqlTransaction tr;
        tr = sqlConnection.BeginTransaction();
        try
        {

            SqlCommand sqlCommand = new SqlCommand("PROC_EMPLOYER_INVOICE_PAYMENT", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            sqlCommand.Parameters.AddWithValue("@I_ID", objAL[9].ToString());
            sqlCommand.Parameters.AddWithValue("@INVOICE_NO", objAL[4].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_FLAG", "DELETE");
            sqlCommand.Transaction = tr;
            iReturn = sqlCommand.ExecuteNonQuery();

             sqlCommand = new SqlCommand("PROC_EMPLOYER_INVOICE_PAYMENT", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_USRE_ID", objAL[1].ToString());
            sqlCommand.Parameters.AddWithValue("@MN_CHECK_AMOUNT", objAL[2].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_PAYMENT_TYPE", objAL[3].ToString());
            sqlCommand.Parameters.AddWithValue("@INVOICE_NO", objAL[4].ToString());
            sqlCommand.Parameters.AddWithValue("@DT_CHECK_DATE", objAL[5].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_EMPOLYER_ID", objAL[6].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_FLAG", "ADD");
            sqlCommand.Parameters.AddWithValue("@SZ_CHECK_NO", objAL[7].ToString());
            sqlCommand.Parameters.AddWithValue("@SZ_NOTES", objAL[8].ToString());
            sqlCommand.Transaction = tr;
            iReturn = sqlCommand.ExecuteNonQuery();
            tr.Commit();
        }
        catch (Exception)
        {
            iReturn = 0;
            tr.Rollback();
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        return iReturn;
    }
}