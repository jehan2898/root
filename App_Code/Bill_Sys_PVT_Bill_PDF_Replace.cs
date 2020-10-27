using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Xml;
using System.IO;
using System.Data.Sql;
using log4net;
using System.Drawing;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using Microsoft.SqlServer.Management.Common;

/// <summary>
/// Summary description for Bill_Sys_PVT_Bill_PDF_Replace
/// </summary>
public class Bill_Sys_PVT_Bill_PDF_Replace
{
    private static ILog log = LogManager.GetLogger("Bill_Sys_PVT_Bill_PDF_Replace");
    public Bill_Sys_PVT_Bill_PDF_Replace()
    {

    }

    //public string ReplacePDFvalues(string pdfFileName, string billNumber, string szCompanyID, string szCaseID, string szCompID)
    //{
    //    string OutputFilePath = "", newPdfFilename="";
    //    newPdfFilename = getFileName(billNumber) + ".pdf";
    //    log.Debug(newPdfFilename);
    //    try

    //     {
    //        string compId = szCompID;
    //        DataSet dsPdfValue = getPDFData(billNumber, compId);
    //        DataSet dsProcValue = getServiceTableData(billNumber, compId);
    //        if (dsProcValue.Tables[0].Rows.Count <= 6)
    //        {
    //            log.Debug("in ReplacePDFvalues");
    //            //newPdfFilename = getFileName(billNumber) + ".pdf";
    //            //log.Debug(newPdfFilename);


    //            string pdfTemplate = pdfFileName;
    //            OutputFilePath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID) + newPdfFilename;
    //            log.Debug("OutputFilePath " + OutputFilePath);


    //            PdfReader pdfReader = new PdfReader(pdfTemplate);
    //            //PdfReader.unethicalreading = true; 
    //            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(OutputFilePath, FileMode.Create));
    //            AcroFields pdfFormFields = pdfStamper.AcroFields;
    //            if (dsPdfValue != null)
    //            {
    //                if (dsPdfValue.Tables[0] != null)
    //                {
    //                    //pdfFormFields.SetField("Name", "Insurance company name");
    //                    pdfFormFields.SetField("Name", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
    //                    pdfFormFields.SetField("2", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString());
    //                    //pdfFormFields.SetField("3", "Insurance company address");
    //                    pdfFormFields.SetField("3", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS2"].ToString());
    //                    //pdfFormFields.SetField("4", "Insurance company address");
    //                    pdfFormFields.SetField("4", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString());
    //                    //pdfFormFields.SetField("8", "Yes");
    //                    //pdfFormFields.SetField("9", "Yes");
    //                    //pdfFormFields.SetField("10", "Yes");
    //                    //pdfFormFields.SetField("11", "Yes");
    //                    //if (dsPdfValue.Tables[0].Rows[0]["SZ_ABBRIVATION"].ToString() == "PVT")
    //                    //{
    //                        pdfFormFields.SetField("12", "1");
    //                    //}
    //                    //else
    //                    //{
    //                    //    pdfFormFields.SetField("14", "1");
    //                    //}
    //                    //pdfFormFields.SetField("13", "Yes");

    //                    //Amod
    //                    //pdfFormFields.SetField("14", "1");
    //                    //pdfFormFields.SetField("15", "Insured ID No");

    //                    // Amod
    //                    if (dsPdfValue.Tables[0].Rows[0]["SZ_CLAIM_NO"] != null)
    //                    {
    //                        pdfFormFields.SetField("15", dsPdfValue.Tables[0].Rows[0]["SZ_CLAIM_NO"].ToString());
    //                    }

    //                    ////pdfFormFields.SetField("16", "Patien's Name LFM");
    //                    ////pdfFormFields.SetField("17", "Patient's DOB  MM");
    //                    ////pdfFormFields.SetField("18", "Patient's DOB  DD");
    //                    ////pdfFormFields.SetField("19", "Patient's DOB  YYYY");
    //                    ////pdfFormFields.SetField("20", "Yes");
    //                    ////pdfFormFields.SetField("21", "Yes");
    //                    ////pdfFormFields.SetField("22", "Insured's Name LFM");
    //                    ////pdfFormFields.SetField("23", "Patient's Address No,Street");
    //                    ////pdfFormFields.SetField("24", "Patient's Address city");
    //                    ////pdfFormFields.SetField("25", "Patient's Address state");
    //                    ////pdfFormFields.SetField("26", "Patient's Address zip");
    //                    ////pdfFormFields.SetField("27", "Patient's Address tele 1");
    //                    ////pdfFormFields.SetField("28", "Patient's Address tele 2");
    //                    pdfFormFields.SetField("16", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString());

    //                    // Amod - if the month is 1 digit then prefix it with 0
    //                    if (dsPdfValue.Tables[0].Rows[0]["DOB_MM"] != null)
    //                    {
    //                        if (!dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString().Equals(""))
    //                        {
    //                            try
    //                            {
    //                                int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
    //                                if (i <= 9)
    //                                {
    //                                    pdfFormFields.SetField("17", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
    //                                }
    //                                else
    //                                {
    //                                    pdfFormFields.SetField("17", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
    //                                }
    //                            }
    //                            catch (Exception i)
    //                            {
    //                                pdfFormFields.SetField("17", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
    //                            }
    //                        }
    //                    }

    //                    //pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //                    if (dsPdfValue.Tables[0].Rows[0]["DOB_DD"] != null)
    //                    {
    //                        if (!dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString().Equals(""))
    //                        {
    //                            try
    //                            {
    //                                int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //                                if (i <= 9)
    //                                {
    //                                    pdfFormFields.SetField("18", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //                                }
    //                                else
    //                                {
    //                                    pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //                                }
    //                            }
    //                            catch (Exception i)
    //                            {
    //                                pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //                            }
    //                        }
    //                    }
    //                    pdfFormFields.SetField("19", dsPdfValue.Tables[0].Rows[0]["DOB_YY"].ToString());
    //                    if (dsPdfValue.Tables[0].Rows[0]["MALE"].ToString() == "1")
    //                    {
    //                        pdfFormFields.SetField("20", "No");
    //                    }
    //                    if (dsPdfValue.Tables[0].Rows[0]["FEMALE"].ToString() == "1")
    //                    {
    //                        pdfFormFields.SetField("21", "No");
    //                    }

    //                    if (szCompID == "CO000000000000000129")
    //                    {
    //                        pdfFormFields.SetField("22", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString());
    //                    }
    //                    else
    //                    {
    //                        pdfFormFields.SetField("22", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString());
    //                    }
    //                    //pdfFormFields.SetField("22", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString());
    //                    pdfFormFields.SetField("23", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString());
    //                    pdfFormFields.SetField("24", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
    //                    pdfFormFields.SetField("25", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
    //                    pdfFormFields.SetField("26", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());
    //                    pdfFormFields.SetField("27", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_1_3"].ToString());
    //                    pdfFormFields.SetField("28", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_4_E"].ToString());

    //                    if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "1")
    //                    {
    //                        pdfFormFields.SetField("29", "1");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "2")
    //                    {
    //                        pdfFormFields.SetField("30", "1");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "3")
    //                    {
    //                        pdfFormFields.SetField("31", "1");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "4")
    //                    {
    //                        pdfFormFields.SetField("32", "1");
    //                    }
    //                    //pdfFormFields.SetField("29", "Yes");
    //                    //pdfFormFields.SetField("30", "Yes");
    //                    //pdfFormFields.SetField("31", "Yes");
    //                    //pdfFormFields.SetField("32", "Yes");
    //                    if (szCompID == "CO000000000000000129")
    //                    {
    //                        pdfFormFields.SetField("33", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString());
    //                        pdfFormFields.SetField("34", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
    //                        pdfFormFields.SetField("35", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
    //                        pdfFormFields.SetField("36", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());
    //                        pdfFormFields.SetField("37", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_1_3"].ToString());
    //                        pdfFormFields.SetField("38", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_4_E"].ToString());
    //                    }
    //                    else
    //                    {
    //                        pdfFormFields.SetField("33", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ADDRESS"].ToString());
    //                        pdfFormFields.SetField("34", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_CITY"].ToString());
    //                        pdfFormFields.SetField("35", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_STATE_ID"].ToString());
    //                        pdfFormFields.SetField("36", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ZIP"].ToString());
    //                        pdfFormFields.SetField("37", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE_1_3"].ToString());
    //                        pdfFormFields.SetField("38", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE_4_E"].ToString());
    //                    }
    //                    if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "1")
    //                    {
    //                        pdfFormFields.SetField("39", "1");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "2")
    //                    {
    //                        pdfFormFields.SetField("40", "1");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "3")
    //                    {
    //                        pdfFormFields.SetField("41", "1");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "4")
    //                    {
    //                        pdfFormFields.SetField("4110257", "1");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "5")
    //                    {
    //                        pdfFormFields.SetField("43", "1");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "6")
    //                    {
    //                        pdfFormFields.SetField("44", "1");
    //                    }
    //                    //if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "1")
    //                    //{
    //                    //    pdfFormFields.SetField("39", "1");
    //                    //}
    //                    //else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "2")
    //                    //{
    //                    //    pdfFormFields.SetField("40", "1");
    //                    //}
    //                    //else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "3")
    //                    //{
    //                    //    pdfFormFields.SetField("41", "1");
    //                    //}

    //                    //pdfFormFields.SetField("39", "Yes");
    //                    //pdfFormFields.SetField("40", "Yes");
    //                    //pdfFormFields.SetField("41", "Yes");
    //                    //pdfFormFields.SetField("42", "Yes");//?????????/
    //                    //pdfFormFields.SetField("43", "Yes");
    //                    //pdfFormFields.SetField("44", "Yes");
    //                    //pdfFormFields.SetField("45", "OTHER INSURED’S NAME LFM");
    //                    ////pdfFormFields.SetField("46", "OTHER INSURED’S policy/grp no");
    //                    pdfFormFields.SetField("46", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_NO"].ToString());
    //                    //pdfFormFields.SetField("47", "OTHER INSURED’S DOB MM");
    //                    //pdfFormFields.SetField("48", "OTHER INSURED’S DOB DD");
    //                    //pdfFormFields.SetField("49", "OTHER INSURED’S DOB YYYY");
    //                    //pdfFormFields.SetField("50", "Yes");
    //                    //pdfFormFields.SetField("51", "Yes");
    //                    //pdfFormFields.SetField("52", "EMPLOYER’S NAME OR SCHOOL NAME");
    //                    //pdfFormFields.SetField("53", "INSURANCE PLAN NAME OR PROGRAM NAME");
    //                    //pdfFormFields.SetField("54", "Yes");
    //                    //pdfFormFields.SetField("55", "Yes");
    //                    //pdfFormFields.SetField("56", "Yes");
    //                    //pdfFormFields.SetField("57", "Yes");

    //                    //pdfFormFields.SetField("59", "Yes");
    //                    if (szCompID == "CO000000000000000129")
    //                    {
    //                        pdfFormFields.SetField("55", "1");
    //                        pdfFormFields.SetField("57", "1");
    //                        pdfFormFields.SetField("60", "1");
    //                    }
    //                    else
    //                    {

    //                        if (dsPdfValue.Tables[0].Rows[0]["SZ_ABBRIVATION"].ToString() == "WC")
    //                        {
    //                            pdfFormFields.SetField("54", "1");
    //                            pdfFormFields.SetField("57", "1");
    //                            pdfFormFields.SetField("60", "1");
    //                        }
    //                        else if (dsPdfValue.Tables[0].Rows[0]["SZ_ABBRIVATION"].ToString() == "NF")
    //                        {
    //                            pdfFormFields.SetField("56", "1");
    //                            pdfFormFields.SetField("58", dsPdfValue.Tables[0].Rows[0]["SZ_ACCIDENT_STATE_ID"].ToString());
    //                            pdfFormFields.SetField("55", "1");
    //                            pdfFormFields.SetField("60", "1");
    //                        }
    //                        else
    //                        {
    //                            pdfFormFields.SetField("59", "1");
    //                            pdfFormFields.SetField("55", "1");

    //                        }
    //                    }
    //                    //pdfFormFields.SetField("61", "RESERVED FOR LOCAL USE");
    //                    //pdfFormFields.SetField("62", "INSURED’S POLICY GROUP OR FECA NUMBER");
    //                    //pdfFormFields.SetField("63", "INSURED’S DOB MM");
    //                    //pdfFormFields.SetField("64", "INSURED’S DOB DD");
    //                    //pdfFormFields.SetField("65", "INSURED’S DOB YYYY");
    //                    if (dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"] != null)
    //                    {
    //                        if (!dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"].ToString().Equals(""))
    //                        {
    //                            try
    //                            {
    //                                int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"].ToString());
    //                                if (i <= 9)
    //                                {
    //                                    pdfFormFields.SetField("63", "0" + dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"].ToString());
    //                                }
    //                                else
    //                                {
    //                                    pdfFormFields.SetField("63", dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"].ToString());
    //                                }
    //                            }
    //                            catch (Exception i)
    //                            {
    //                                pdfFormFields.SetField("63", dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"].ToString());
    //                            }
    //                        }
    //                    }

    //                    //pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //                    if (dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"] != null)
    //                    {
    //                        if (!dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"].ToString().Equals(""))
    //                        {
    //                            try
    //                            {
    //                                int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"].ToString());
    //                                if (i <= 9)
    //                                {
    //                                    pdfFormFields.SetField("64", "0" + dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"].ToString());
    //                                }
    //                                else
    //                                {
    //                                    pdfFormFields.SetField("64", dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"].ToString());
    //                                }
    //                            }
    //                            catch (Exception i)
    //                            {
    //                                pdfFormFields.SetField("64", dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"].ToString());
    //                            }
    //                        }
    //                    }
    //                    pdfFormFields.SetField("65", dsPdfValue.Tables[0].Rows[0]["INS_DOB_YY"].ToString());
    //                    if (dsPdfValue.Tables[0].Rows[0]["INS_IS_MALE"].ToString() == "1")
    //                    {
    //                        pdfFormFields.SetField("66", "Yes");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["INS_IS_FEMALE"].ToString() == "1")
    //                    {
    //                        pdfFormFields.SetField("67", "No");
    //                    }
    //                    //pdfFormFields.SetField("66", "Yes");
    //                    //pdfFormFields.SetField("67", "Yes");
    //                    //pdfFormFields.SetField("68", "EMPLOYER’S NAME OR SCHOOL NAME");
    //                    ////pdfFormFields.SetField("69", "INSURANCE PLAN NAME OR PROGRAM NAME");
    //                    //pdfFormFields.SetField("Name", );
    //                    //Amod
    //                    pdfFormFields.SetField("69", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
    //                    //pdfFormFields.SetField("70", "Yes");
    //                    //pdfFormFields.SetField("71", "Yes");

    //                    //Amod
    //                    pdfFormFields.SetField("72", "SIGNATURE ON FILE");

    //                    //Amod
    //                    pdfFormFields.SetField("73", dsPdfValue.Tables[0].Rows[0]["SZ_TODAY"].ToString());

    //                    //Amod
    //                    pdfFormFields.SetField("74", "SIGNATURE ON FILE");

    //                    //Amod
    //                    pdfFormFields.SetField("75", dsPdfValue.Tables[0].Rows[0]["SZ_MM_ACCIDENT"].ToString());

    //                    //Amod
    //                    pdfFormFields.SetField("76", dsPdfValue.Tables[0].Rows[0]["SZ_DD_ACCIDENT"].ToString());

    //                    //Amod
    //                    pdfFormFields.SetField("77", dsPdfValue.Tables[0].Rows[0]["SZ_YY_ACCIDENT"].ToString());

    //                    //pdfFormFields.SetField("78", "first treat Date MM");
    //                    //pdfFormFields.SetField("79", "first treat Date DD");
    //                    //pdfFormFields.SetField("80", "first treat Date YYYY");
    //                    //pdfFormFields.SetField("81", "unable work from Date MM");
    //                    //pdfFormFields.SetField("82", "unable work from Date DD");
    //                    //pdfFormFields.SetField("83", "unable work from Date YYYY");
    //                    //pdfFormFields.SetField("84", "unable work to Date MM");
    //                    //pdfFormFields.SetField("85", "unable work to Date DD");
    //                    //pdfFormFields.SetField("86", "unable work to Date YYYY");
    //                    //pdfFormFields.SetField("87", "NAME OF REFERRING PROVIDER OR OTHER SOURCE");
    //                    pdfFormFields.SetField("87", dsPdfValue.Tables[0].Rows[0]["sz_reff_doctor"].ToString());
    //                    pdfFormFields.SetField("90", dsPdfValue.Tables[0].Rows[0]["sz_reff_doctor_npi"].ToString());

    //                    if (dsPdfValue.Tables[0].Rows[0]["IS_OUT_LAB_YES"].ToString() == "1")
    //                    {
    //                        pdfFormFields.SetField("98", "No");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["IS_OUT_LAB_NO"].ToString() == "1")
    //                    {
    //                        pdfFormFields.SetField("99", "No");
    //                    }

    //                    string diaPointer = "";
    //                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != null)
    //                    {
    //                        string[] diag1 = dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString().Split('.');
    //                        if (diag1.Length > 1)
    //                        {
    //                            pdfFormFields.SetField("102", diag1[0].ToString());
    //                            pdfFormFields.SetField("104", diag1[1].ToString());
    //                        }
    //                        else
    //                        {
    //                            pdfFormFields.SetField("102", diag1[0].ToString());
    //                        }
    //                        diaPointer = "1";
    //                    }
    //                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != null)
    //                    {
    //                        string[] diag2 = dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString().Split('.');
    //                        if (diag2.Length > 1)
    //                        {
    //                            pdfFormFields.SetField("105", diag2[0].ToString());
    //                            pdfFormFields.SetField("107", diag2[1].ToString());
    //                        }
    //                        else
    //                        {
    //                            pdfFormFields.SetField("105", diag2[0].ToString());
    //                        }

    //                        diaPointer += ",2";
    //                    }
    //                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != null)
    //                    {
    //                        string[] diag3 = dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString().Split('.');
    //                        if (diag3.Length > 1)
    //                        {
    //                            pdfFormFields.SetField("108", diag3[0].ToString());
    //                            pdfFormFields.SetField("110", diag3[1].ToString());
    //                        }
    //                        else
    //                        {
    //                            pdfFormFields.SetField("108", diag3[0].ToString());
    //                        }
    //                        diaPointer += ",3";
    //                    }
    //                    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != null)
    //                    {
    //                        string[] diag4 = dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString().Split('.');
    //                        if (diag4.Length > 1)
    //                        {
    //                            pdfFormFields.SetField("111", diag4[0].ToString());
    //                            pdfFormFields.SetField("113", diag4[1].ToString());
    //                        }
    //                        else
    //                        {
    //                            pdfFormFields.SetField("111", diag4[0].ToString());
    //                        }
    //                        diaPointer += ",4";
    //                    }

    //                    ////pdfFormFields.SetField("102", "diagnosis pre");
    //                    ////pdfFormFields.SetField("104", "dia pro");

    //                    ////pdfFormFields.SetField("105", "diagnosis pre");
    //                    ////pdfFormFields.SetField("107", "dia pro");

    //                    ////pdfFormFields.SetField("108", "diagnosis pre");
    //                    ////pdfFormFields.SetField("110", "dia pro");

    //                    ////pdfFormFields.SetField("111", "diagnosis pre");
    //                    ////pdfFormFields.SetField("113", "dia pro");
    //                    string firstModifier = "";
    //                    string multiModifier = "";

    //                    double totcharge = 0.0, paidAmt = 0.0, balAmt = 0.0;
    //                    if (dsProcValue != null)
    //                    {
    //                        if (dsProcValue.Tables.Count > 0 && dsProcValue.Tables[0].Rows.Count > 0)
    //                        {
    //                            //////////////////////1/////////////////////////////
    //                            for (int i = 0; i < dsProcValue.Tables[0].Rows.Count; i++)
    //                            {
    //                                firstModifier = "";
    //                                multiModifier = "";
    //                                if (dsPdfValue.Tables[0].Rows[0]["multiple_modifier"].ToString() == "1")
    //                                {
    //                                    if (dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString() != "" && dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString() != null)
    //                                    {
    //                                        string[] modifiers = dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString().Split(',');
    //                                        if (modifiers.Length > 1)
    //                                        {
    //                                            firstModifier = modifiers[0].ToString();
    //                                            for (int j = 1; j < modifiers.Length; j++)
    //                                            {
    //                                                multiModifier += modifiers[j].ToString() + ",";
    //                                            }
    //                                            multiModifier = multiModifier.Substring(0, multiModifier.Length - 1);
    //                                        }
    //                                        else
    //                                        {
    //                                            firstModifier = modifiers[0].ToString();
    //                                        }
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    firstModifier = dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString();
    //                                    multiModifier = "";
    //                                }
    //                                if (i == 0)
    //                                {
    //                                    pdfFormFields.SetField("120", dsProcValue.Tables[0].Rows[i]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("121", dsProcValue.Tables[0].Rows[i]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("122", dsProcValue.Tables[0].Rows[i]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("123", dsProcValue.Tables[0].Rows[i]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("124", dsProcValue.Tables[0].Rows[i]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("125", dsProcValue.Tables[0].Rows[i]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("126", dsProcValue.Tables[0].Rows[i]["PALCE OF SERVICE"].ToString());
    //                                    pdfFormFields.SetField("128", dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
    //                                    pdfFormFields.SetField("129", firstModifier);
    //                                    pdfFormFields.SetField("130", multiModifier);
    //                                    pdfFormFields.SetField("133", diaPointer);
    //                                    pdfFormFields.SetField("c1", dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
    //                                    totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                                    pdfFormFields.SetField("138", dsProcValue.Tables[0].Rows[i]["SZ_NPI"].ToString());
    //                                    pdfFormFields.SetField("136", dsProcValue.Tables[0].Rows[i]["I_UNIT"].ToString());
    //                                }
    //                                //////////////////////2/////////////////////////////
    //                                if (i == 1)
    //                                {
    //                                    pdfFormFields.SetField("142", dsProcValue.Tables[0].Rows[1]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("143", dsProcValue.Tables[0].Rows[1]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("144", dsProcValue.Tables[0].Rows[1]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("145", dsProcValue.Tables[0].Rows[1]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("146", dsProcValue.Tables[0].Rows[1]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("147", dsProcValue.Tables[0].Rows[1]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("148", dsProcValue.Tables[0].Rows[1]["PALCE OF SERVICE"].ToString());
    //                                    pdfFormFields.SetField("150", dsProcValue.Tables[0].Rows[1]["SZ_PROCEDURE_CODE"].ToString());
    //                                    pdfFormFields.SetField("151", firstModifier);
    //                                    pdfFormFields.SetField("152", multiModifier);
    //                                    pdfFormFields.SetField("155", diaPointer);
    //                                    pdfFormFields.SetField("c2", dsProcValue.Tables[0].Rows[1]["FL_AMOUNT"].ToString());
    //                                    totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                                    pdfFormFields.SetField("160", dsProcValue.Tables[0].Rows[1]["SZ_NPI"].ToString());
    //                                    pdfFormFields.SetField("158", dsProcValue.Tables[0].Rows[1]["I_UNIT"].ToString());

    //                                }
    //                                //////////////////////3/////////////////////////////
    //                                if (i == 2)
    //                                {
    //                                    pdfFormFields.SetField("164", dsProcValue.Tables[0].Rows[2]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("165", dsProcValue.Tables[0].Rows[2]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("166", dsProcValue.Tables[0].Rows[2]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("167", dsProcValue.Tables[0].Rows[2]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("168", dsProcValue.Tables[0].Rows[2]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("169", dsProcValue.Tables[0].Rows[2]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("170", dsProcValue.Tables[0].Rows[2]["PALCE OF SERVICE"].ToString());
    //                                    pdfFormFields.SetField("172", dsProcValue.Tables[0].Rows[2]["SZ_PROCEDURE_CODE"].ToString());
    //                                    pdfFormFields.SetField("173", firstModifier);
    //                                    pdfFormFields.SetField("174", multiModifier);
    //                                    pdfFormFields.SetField("177", diaPointer);
    //                                    pdfFormFields.SetField("c3", dsProcValue.Tables[0].Rows[2]["FL_AMOUNT"].ToString());
    //                                    totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                                    pdfFormFields.SetField("182", dsProcValue.Tables[0].Rows[2]["SZ_NPI"].ToString());
    //                                    pdfFormFields.SetField("180", dsProcValue.Tables[0].Rows[2]["I_UNIT"].ToString());
    //                                }
    //                                //////////////////////4////////////////////////////
    //                                if (i == 3)
    //                                {
    //                                    pdfFormFields.SetField("186", dsProcValue.Tables[0].Rows[3]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("187", dsProcValue.Tables[0].Rows[3]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("188", dsProcValue.Tables[0].Rows[3]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("189", dsProcValue.Tables[0].Rows[3]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("190", dsProcValue.Tables[0].Rows[3]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("191", dsProcValue.Tables[0].Rows[3]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("192", dsProcValue.Tables[0].Rows[3]["PALCE OF SERVICE"].ToString());
    //                                    pdfFormFields.SetField("194", dsProcValue.Tables[0].Rows[3]["SZ_PROCEDURE_CODE"].ToString());
    //                                    pdfFormFields.SetField("195", firstModifier);
    //                                    pdfFormFields.SetField("196", multiModifier);
    //                                    pdfFormFields.SetField("199", diaPointer);
    //                                    pdfFormFields.SetField("c4", dsProcValue.Tables[0].Rows[3]["FL_AMOUNT"].ToString());
    //                                    totcharge +=  (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                                    pdfFormFields.SetField("204", dsProcValue.Tables[0].Rows[3]["SZ_NPI"].ToString());
    //                                    pdfFormFields.SetField("202", dsProcValue.Tables[0].Rows[3]["I_UNIT"].ToString());
    //                                }
    //                                if (i == 4)
    //                                {
    //                                    //////////////////////5/////////////////////////////
    //                                    pdfFormFields.SetField("208", dsProcValue.Tables[0].Rows[4]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("209", dsProcValue.Tables[0].Rows[4]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("210", dsProcValue.Tables[0].Rows[4]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("211", dsProcValue.Tables[0].Rows[4]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("212", dsProcValue.Tables[0].Rows[4]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("213", dsProcValue.Tables[0].Rows[4]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("214", dsProcValue.Tables[0].Rows[4]["PALCE OF SERVICE"].ToString());
    //                                    pdfFormFields.SetField("216", dsProcValue.Tables[0].Rows[4]["SZ_PROCEDURE_CODE"].ToString());
    //                                    pdfFormFields.SetField("217", firstModifier);
    //                                    pdfFormFields.SetField("218", multiModifier);
    //                                    pdfFormFields.SetField("221", diaPointer);
    //                                    pdfFormFields.SetField("c5", dsProcValue.Tables[0].Rows[4]["FL_AMOUNT"].ToString());
    //                                    totcharge +=  (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                                    pdfFormFields.SetField("226", dsProcValue.Tables[0].Rows[4]["SZ_NPI"].ToString());
    //                                    pdfFormFields.SetField("224", dsProcValue.Tables[0].Rows[4]["I_UNIT"].ToString());
    //                                }
    //                                if (i == 5)
    //                                {
    //                                    //////////////////////6/////////////////////////////
    //                                    pdfFormFields.SetField("230", dsProcValue.Tables[0].Rows[5]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("231", dsProcValue.Tables[0].Rows[5]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("232", dsProcValue.Tables[0].Rows[5]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("233", dsProcValue.Tables[0].Rows[5]["DOS_MM"].ToString());
    //                                    pdfFormFields.SetField("234", dsProcValue.Tables[0].Rows[5]["DOS_DD"].ToString());
    //                                    pdfFormFields.SetField("235", dsProcValue.Tables[0].Rows[5]["DOS_YY"].ToString());
    //                                    pdfFormFields.SetField("236", dsProcValue.Tables[0].Rows[5]["PALCE OF SERVICE"].ToString());
    //                                    pdfFormFields.SetField("238", dsProcValue.Tables[0].Rows[5]["SZ_PROCEDURE_CODE"].ToString());
    //                                    pdfFormFields.SetField("239", firstModifier);
    //                                    pdfFormFields.SetField("240", multiModifier);
    //                                    pdfFormFields.SetField("243", diaPointer);
    //                                    pdfFormFields.SetField("c6", dsProcValue.Tables[0].Rows[5]["FL_AMOUNT"].ToString());
    //                                    totcharge +=  (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                                    pdfFormFields.SetField("248", dsProcValue.Tables[0].Rows[5]["SZ_NPI"].ToString());
    //                                    pdfFormFields.SetField("246", dsProcValue.Tables[0].Rows[5]["I_UNIT"].ToString());
    //                                }
    //                            }
    //                        }
    //                    }
    //                    ////pdfFormFields.SetField("249", "provider TAX I.D.");
    //                    ////pdfFormFields.SetField("250", "Yes");//
    //                    ////pdfFormFields.SetField("251", "Yes");
    //                    pdfFormFields.SetField("249", dsPdfValue.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
    //                    //pdfFormFields.SetField("250", "Yes");//
    //                    if (dsPdfValue.Tables[0].Rows[0]["TAX_TYPE"].ToString().ToLower() == "false")
    //                    {
    //                        pdfFormFields.SetField("250", "No");
    //                    }
    //                    else if (dsPdfValue.Tables[0].Rows[0]["TAX_TYPE"].ToString().ToLower() == "true")
    //                    {
    //                        pdfFormFields.SetField("251", "No");
    //                    }

    //                    ////pdfFormFields.SetField("252", "Pat acc no");
    //                    //pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_BILL_NUMBER"].ToString());
    //                    if (szCompID == "CO000000000000000129")
    //                    {
    //                        pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_CASE_NO"].ToString());
    //                    }
    //                    else
    //                    {
    //                        pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_CASE_NO"].ToString());
    //                    }
    //                    //pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_CASE_NO"].ToString());
    //                    ////pdfFormFields.SetField("253", "Yes");
    //                    if (dsPdfValue.Tables[0].Rows[0]["ACCEPT_ASSIGNMENT"].ToString() == "1")
    //                    {
    //                        pdfFormFields.SetField("253", "No");
    //                    }
    //                    ////pdfFormFields.SetField("254", "Yes");//

    //                    ////pdfFormFields.SetField("261", "Doctor Name");
    //                    balAmt = totcharge;
    //                    pdfFormFields.SetField("C255", Convert.ToString(totcharge));
    //                    pdfFormFields.SetField("C257", paidAmt.ToString());

    //                    //Amod commented
    //                    //pdfFormFields.SetField("C259", balAmt.ToString());
    //                    pdfFormFields.SetField("C259", Convert.ToString(totcharge));

    //                    pdfFormFields.SetField("261", dsPdfValue.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
    //                    pdfFormFields.SetField("31SIGN", dsPdfValue.Tables[0].Rows[0]["SIGNATURE_ON_FILE"].ToString());
    //                    pdfFormFields.SetField("262", dsPdfValue.Tables[0].Rows[0]["DATE"].ToString());
    //                    if (szCompID == "CO000000000000000129")
    //                    {
    //                        pdfFormFields.SetField("263", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString());
    //                        pdfFormFields.SetField("264", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
    //                        pdfFormFields.SetField("265", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS2"].ToString());
    //                    }
    //                    else
    //                    {
    //                        pdfFormFields.SetField("263", dsPdfValue.Tables[0].Rows[0]["SZ_REFF_OFFICE_NAME"].ToString());
    //                        pdfFormFields.SetField("264", dsPdfValue.Tables[0].Rows[0]["SZ_REFF_OFFICE_ADDRESS"].ToString());
    //                        pdfFormFields.SetField("265", dsPdfValue.Tables[0].Rows[0]["SZ_REFF_OFFICE_ADDRESS2"].ToString());
    //                    }
    //                    //pdfFormFields.SetField("263", dsPdfValue.Tables[0].Rows[0]["SZ_REFF_OFFICE_NAME"].ToString());
    //                    ////pdfFormFields.SetField("264", "Provider address");
    //                    ////pdfFormFields.SetField("265", "Provider city,state,zip");
    //                    //pdfFormFields.SetField("264", dsPdfValue.Tables[0].Rows[0]["SZ_REFF_OFFICE_ADDRESS"].ToString());
    //                    //pdfFormFields.SetField("265", dsPdfValue.Tables[0].Rows[0]["SZ_REFF_OFFICE_ADDRESS2"].ToString());

    //                    ////pdfFormFields.SetField("266", "Provider NPI");
    //                    if (szCompID == "CO000000000000000129")
    //                    {
    //                        pdfFormFields.SetField("266", dsPdfValue.Tables[0].Rows[0]["MST_OFFICE_NPI"].ToString());
    //                    }
    //                    else
    //                    {
    //                        pdfFormFields.SetField("266", dsPdfValue.Tables[0].Rows[0]["MST_REFF_OFFICE_NPI"].ToString());
    //                    }
    //                    //pdfFormFields.SetField("266", dsPdfValue.Tables[0].Rows[0]["MST_REFF_OFFICE_NPI"].ToString());
    //                    pdfFormFields.SetField("267", dsPdfValue.Tables[0].Rows[0]["32b"].ToString());
    //                    // Amod - commented
    //                    //pdfFormFields.SetField("267", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_1_3"].ToString() + dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_4_E"].ToString());

    //                    ////pdfFormFields.SetField("273", "provider NPI");
    //                    //if (szCompID == "CO000000000000000129")
    //                    //{
    //                    //    pdfFormFields.SetField("273", dsProcValue.Tables[0].Rows[0]["SZ_NPI"].ToString());

    //                    //}
    //                    //else
    //                    //{
    //                        pdfFormFields.SetField("273", dsPdfValue.Tables[0].Rows[0]["MST_OFFICE_NPI"].ToString());

    //                    //}
    //                    //pdfFormFields.SetField("273", dsPdfValue.Tables[0].Rows[0]["MST_OFFICE_NPI"].ToString());
    //                    pdfFormFields.SetField("274", dsPdfValue.Tables[0].Rows[0]["33b"].ToString());
    //                    ////pdfFormFields.SetField("268", "provider phone 3");
    //                    ////pdfFormFields.SetField("269", "provider phone rem");
    //                    pdfFormFields.SetField("268", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_1_3"].ToString());
    //                    pdfFormFields.SetField("269", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_4_E"].ToString());

    //                    ////pdfFormFields.SetField("270", "Billing provider name");
    //                    pdfFormFields.SetField("270", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString());
    //                    pdfFormFields.SetField("271", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
    //                    pdfFormFields.SetField("272", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS2"].ToString());

    //                    pdfStamper.FormFlattening = true;
    //                    pdfStamper.Close();
    //                }
    //            }
    //            return newPdfFilename;
    //        }
    //        else
    //        {
    //             //dsPdfValue
    //             //dsProcValue 
    //            ArrayList List = new ArrayList();

    //            int dscount = dsProcValue.Tables[0].Rows.Count;
    //            int count = 0;
    //            int loop = dscount / 6;
    //            for (int i = 0; i < loop;i++ )
    //            {
    //                DataTable dt = new DataTable();
    //                dt.Columns.Add("SZ_PROCEDURE_CODE");
    //                dt.Columns.Add("SZ_CODE_DESCRIPTION");
    //                dt.Columns.Add("FL_AMOUNT");
    //                dt.Columns.Add("FL_GROUP_AMOUNT");
    //                dt.Columns.Add("I_GROUP_AMOUNT_id");
    //                dt.Columns.Add("DOS_MM");
    //                dt.Columns.Add("DOS_DD");
    //                dt.Columns.Add("DOS_YY");
    //                dt.Columns.Add("SZ_NPI");
    //                dt.Columns.Add("PALCE OF SERVICE");
    //                dt.Columns.Add("SZ_MODIFIER");
    //                dt.Columns.Add("I_UNIT");
    //                int loopend =count + 6;
    //                for (int j = count; j < loopend; j++)
    //                   // for (int j = count; j <= count + 6; j++)
    //                {
    //                    DataRow drRow = dt.NewRow();
    //                    drRow["SZ_PROCEDURE_CODE"] = dsProcValue.Tables[0].Rows[j]["SZ_PROCEDURE_CODE"];
    //                    drRow["SZ_CODE_DESCRIPTION"] = dsProcValue.Tables[0].Rows[j]["SZ_CODE_DESCRIPTION"];
    //                    drRow["FL_AMOUNT"] = dsProcValue.Tables[0].Rows[j]["FL_AMOUNT"];
    //                    drRow["FL_GROUP_AMOUNT"] = dsProcValue.Tables[0].Rows[j]["FL_GROUP_AMOUNT"];
    //                    drRow["I_GROUP_AMOUNT_id"] = dsProcValue.Tables[0].Rows[j]["I_GROUP_AMOUNT_id"];
    //                    drRow["DOS_MM"] = dsProcValue.Tables[0].Rows[j]["DOS_MM"];
    //                    drRow["DOS_DD"] = dsProcValue.Tables[0].Rows[j]["DOS_DD"];
    //                    drRow["DOS_YY"] = dsProcValue.Tables[0].Rows[j]["DOS_YY"];
    //                    drRow["SZ_NPI"] = dsProcValue.Tables[0].Rows[j]["SZ_NPI"];
    //                    drRow["PALCE OF SERVICE"] = dsProcValue.Tables[0].Rows[j]["PALCE OF SERVICE"];
    //                    drRow["SZ_MODIFIER"] = dsProcValue.Tables[0].Rows[j]["SZ_MODIFIER"];
    //                    drRow["I_UNIT"] = dsProcValue.Tables[0].Rows[j]["I_UNIT"];
    //                    dt.Rows.Add(drRow); 
    //                    count++;
    //                }
    //                DataSet dsProc = new DataSet();
    //                dsProc.Tables.Add(dt.Copy());
    //               string newSubPdfFilename = getSubFileName(i) + ".pdf";
    //                PdfPrint( pdfFileName,  dsPdfValue,  dsProc, szCompanyID, szCaseID, newSubPdfFilename);
    //                List.Add(newSubPdfFilename);
    //            }
    //            //for (int k = count; k <= dscount; k++ )
    //            {
    //                DataTable oDT = new DataTable();
    //                oDT.Columns.Add("SZ_PROCEDURE_CODE");
    //                oDT.Columns.Add("SZ_CODE_DESCRIPTION");
    //                oDT.Columns.Add("FL_AMOUNT");
    //                oDT.Columns.Add("FL_GROUP_AMOUNT");
    //                oDT.Columns.Add("I_GROUP_AMOUNT_id");
    //                oDT.Columns.Add("DOS_MM");
    //                oDT.Columns.Add("DOS_DD");
    //                oDT.Columns.Add("DOS_YY");
    //                oDT.Columns.Add("SZ_NPI");
    //                oDT.Columns.Add("PALCE OF SERVICE");
    //                oDT.Columns.Add("SZ_MODIFIER");
    //                oDT.Columns.Add("I_UNIT");
    //                for (int j = count; j < dscount; j++)
    //               // for (int j = count; j <= count + 6; j++)
    //                {
    //                    DataRow drrRow = oDT.NewRow();
    //                    drrRow["SZ_PROCEDURE_CODE"] = dsProcValue.Tables[0].Rows[j]["SZ_PROCEDURE_CODE"];
    //                    drrRow["SZ_CODE_DESCRIPTION"] = dsProcValue.Tables[0].Rows[j]["SZ_CODE_DESCRIPTION"];
    //                    drrRow["FL_AMOUNT"] = dsProcValue.Tables[0].Rows[j]["FL_AMOUNT"];
    //                    drrRow["FL_GROUP_AMOUNT"] = dsProcValue.Tables[0].Rows[j]["FL_GROUP_AMOUNT"];
    //                    drrRow["I_GROUP_AMOUNT_id"] = dsProcValue.Tables[0].Rows[j]["I_GROUP_AMOUNT_id"];
    //                    drrRow["DOS_MM"] = dsProcValue.Tables[0].Rows[j]["DOS_MM"];
    //                    drrRow["DOS_DD"] = dsProcValue.Tables[0].Rows[j]["DOS_DD"];
    //                    drrRow["DOS_YY"] = dsProcValue.Tables[0].Rows[j]["DOS_YY"];
    //                    drrRow["SZ_NPI"] = dsProcValue.Tables[0].Rows[j]["SZ_NPI"];
    //                    drrRow["PALCE OF SERVICE"] = dsProcValue.Tables[0].Rows[j]["PALCE OF SERVICE"];
    //                    drrRow["SZ_MODIFIER"] = dsProcValue.Tables[0].Rows[j]["SZ_MODIFIER"];
    //                    drrRow["I_UNIT"] = dsProcValue.Tables[0].Rows[j]["I_UNIT"];
    //                    oDT.Rows.Add(drrRow);
    //                    count++;
    //                }
    //                DataSet ds1Proc = new DataSet();
    //                ds1Proc.Tables.Add(oDT.Copy());
    //                if (oDT.Rows.Count > 0)
    //                {
    //                    string newSubPdfFilename = getSubFileName(count) + ".pdf";
    //                    PdfPrint(pdfFileName, dsPdfValue, ds1Proc, szCompanyID, szCaseID, newSubPdfFilename);
    //                    List.Add(newSubPdfFilename);
    //                }
    //            }
    //            string FirstPath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID);
    //            string F0 = "";
    //            string f1 = "";
    //            string f2 = "";
    //            if (List.Count == 2)
    //            {
    //                F0 = List[0].ToString();
    //                f1 = List[1].ToString();
    //                f2 = getFileName(billNumber+"_merge") + ".pdf";
    //                MergePDF.MergePDFFiles(FirstPath + F0, FirstPath + f1, FirstPath + f2);
    //                File.Copy(FirstPath + f2, FirstPath + newPdfFilename);
    //                //merge
    //            }
    //            else
    //            {
    //                 F0 = List[0].ToString();
    //                 f1="";
    //                 f2="";
    //                for (int i = 0; i < List.Count-1; i++)
    //                {
    //                    f1 = List[i+1].ToString();
    //                    f2 = getFileName(billNumber + "_" + i.ToString()+"_merge") + ".pdf"; 
    //                    //"get file name call karaych";
    //                    //murhe(fp/po,fp+/p1,fp+p2)
    //                    MergePDF.MergePDFFiles(FirstPath +F0, FirstPath + f1, FirstPath + f2);
    //                    F0 = f2;

    //                }

    //                //murhe(fp/po,fp+/p1,fp+p2)

    //                File.Copy(FirstPath + f2, FirstPath + newPdfFilename);
    //                //file.copy(fp/f2,fp+newPdfFilename);
    //            }


    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    }
    //    return newPdfFilename;
    //}

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
                        //pdfFormFields.SetField("45", "OTHER INSURED’S NAME LFM");
                        ////pdfFormFields.SetField("46", "OTHER INSURED’S policy/grp no");
                        //pdfFormFields.SetField("46", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_NO"].ToString());
                        //pdfFormFields.SetField("47", "OTHER INSURED’S DOB MM");
                        //pdfFormFields.SetField("48", "OTHER INSURED’S DOB DD");
                        //pdfFormFields.SetField("49", "OTHER INSURED’S DOB YYYY");
                        //pdfFormFields.SetField("50", "Yes");
                        //pdfFormFields.SetField("51", "Yes");
                        //pdfFormFields.SetField("52", "EMPLOYER’S NAME OR SCHOOL NAME");
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
                        //pdfFormFields.SetField("63", "INSURED’S DOB MM");
                        //pdfFormFields.SetField("64", "INSURED’S DOB DD");
                        //pdfFormFields.SetField("65", "INSURED’S DOB YYYY");
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
                        //pdfFormFields.SetField("68", "EMPLOYER’S NAME OR SCHOOL NAME");
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
                        pdfFormFields.SetField("101", "0");

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
                        //if (dsPdfValue.Tables[0].Rows[0]["sz_abbrivation_id"].ToString() == "WC000000000000000003")
                        //{
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
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis5"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis5"].ToString() != null)
                        {
                            pdfFormFields.SetField("105", dsPdfValue.Tables[0].Rows[0]["Diagnosis5"].ToString());
                            diaPointer += "E";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis6"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis6"].ToString() != null)
                        {
                            pdfFormFields.SetField("107", dsPdfValue.Tables[0].Rows[0]["Diagnosis6"].ToString());
                            diaPointer += "F";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis7"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis7"].ToString() != null)
                        {
                            pdfFormFields.SetField("111", dsPdfValue.Tables[0].Rows[0]["Diagnosis7"].ToString());
                            diaPointer += "G";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis8"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis8"].ToString() != null)
                        {
                            pdfFormFields.SetField("113", dsPdfValue.Tables[0].Rows[0]["Diagnosis8"].ToString());
                            diaPointer += "H";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis9"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis9"].ToString() != null)
                        {
                            pdfFormFields.SetField("1080", dsPdfValue.Tables[0].Rows[0]["Diagnosis9"].ToString());
                            diaPointer += "I";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis10"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis10"].ToString() != null)
                        {
                            pdfFormFields.SetField("1090", dsPdfValue.Tables[0].Rows[0]["Diagnosis10"].ToString());
                            diaPointer += "J";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis11"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis11"].ToString() != null)
                        {
                            pdfFormFields.SetField("1100", dsPdfValue.Tables[0].Rows[0]["Diagnosis11"].ToString());
                            diaPointer += "K";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis12"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis12"].ToString() != null)
                        {
                            pdfFormFields.SetField("1110", dsPdfValue.Tables[0].Rows[0]["Diagnosis12"].ToString());
                            diaPointer += "L";
                        }
                        //}

                        //if (dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != null)
                        //{
                        //    string[] diag1 = dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString().Split('.');
                        //    if (diag1.Length > 1)
                        //    {
                        //        pdfFormFields.SetField("102", diag1[0].ToString());
                        //        pdfFormFields.SetField("104", diag1[1].ToString());
                        //    }
                        //    else
                        //    {
                        //        pdfFormFields.SetField("102", diag1[0].ToString());
                        //    }
                        //    diaPointer = "A";
                        //}
                        //if (dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != null)
                        //{
                        //    string[] diag2 = dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString().Split('.');
                        //    if (diag2.Length > 1)
                        //    {
                        //        pdfFormFields.SetField("108", diag2[0].ToString());
                        //        pdfFormFields.SetField("110", diag2[1].ToString());
                        //    }
                        //    else
                        //    {
                        //        pdfFormFields.SetField("108", diag2[0].ToString());
                        //    }

                        //    diaPointer += "B";
                        //}
                        //if (dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != null)
                        //{
                        //    string[] diag3 = dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString().Split('.');
                        //    if (diag3.Length > 1)
                        //    {
                        //        pdfFormFields.SetField("105", diag3[0].ToString());
                        //        pdfFormFields.SetField("107", diag3[1].ToString());
                        //    }
                        //    else
                        //    {
                        //        pdfFormFields.SetField("105", diag3[0].ToString());
                        //    }
                        //    diaPointer += "C";
                        //}
                        //if (dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != null)
                        //{
                        //    string[] diag4 = dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString().Split('.');
                        //    if (diag4.Length > 1)
                        //    {
                        //        pdfFormFields.SetField("111", diag4[0].ToString());
                        //        pdfFormFields.SetField("113", diag4[1].ToString());
                        //    }
                        //    else
                        //    {
                        //        pdfFormFields.SetField("111", diag4[0].ToString());
                        //    }
                        //    diaPointer += "D";
                        //}


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

                                        codeDesc =  dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                        codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                        codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                        codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                        codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                        codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                    dt.Columns.Add("SZ_1500_DESC");
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
                        drRow["SZ_1500_DESC"] = dsProcValue.Tables[0].Rows[j]["SZ_1500_DESC"];
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
                    oDT.Columns.Add("SZ_1500_DESC");
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
                        drrRow["SZ_1500_DESC"] = dsProcValue.Tables[0].Rows[j]["SZ_1500_DESC"];
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
    public string ReplacePDFvalues(string pdfFileName, string billNumber, string szCompanyID, string szCaseID, string szCompID, ServerConnection conn)
    {
        string OutputFilePath = "", newPdfFilename = "";
        string code_length = "";
        newPdfFilename = getFileName(billNumber) + ".pdf";
        log.Debug(newPdfFilename);
        try

        {
            string compId = szCompID;
            DataSet dsPdfValue = getPDFData(billNumber, compId,conn);
            DataSet dsProcValue = getServiceTableData(billNumber, compId,conn);
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
                        //pdfFormFields.SetField("45", "OTHER INSURED’S NAME LFM");
                        ////pdfFormFields.SetField("46", "OTHER INSURED’S policy/grp no");
                        //pdfFormFields.SetField("46", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_NO"].ToString());
                        //pdfFormFields.SetField("47", "OTHER INSURED’S DOB MM");
                        //pdfFormFields.SetField("48", "OTHER INSURED’S DOB DD");
                        //pdfFormFields.SetField("49", "OTHER INSURED’S DOB YYYY");
                        //pdfFormFields.SetField("50", "Yes");
                        //pdfFormFields.SetField("51", "Yes");
                        //pdfFormFields.SetField("52", "EMPLOYER’S NAME OR SCHOOL NAME");
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
                        //pdfFormFields.SetField("63", "INSURED’S DOB MM");
                        //pdfFormFields.SetField("64", "INSURED’S DOB DD");
                        //pdfFormFields.SetField("65", "INSURED’S DOB YYYY");
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
                        //pdfFormFields.SetField("68", "EMPLOYER’S NAME OR SCHOOL NAME");
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
                        pdfFormFields.SetField("101", "0");
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
                        //if (dsPdfValue.Tables[0].Rows[0]["sz_abbrivation_id"].ToString() == "WC000000000000000003")
                        //{
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
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis5"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis5"].ToString() != null)
                        {
                            pdfFormFields.SetField("105", dsPdfValue.Tables[0].Rows[0]["Diagnosis5"].ToString());
                            diaPointer += "E";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis6"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis6"].ToString() != null)
                        {
                            pdfFormFields.SetField("107", dsPdfValue.Tables[0].Rows[0]["Diagnosis6"].ToString());
                            diaPointer += "F";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis7"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis7"].ToString() != null)
                        {
                            pdfFormFields.SetField("111", dsPdfValue.Tables[0].Rows[0]["Diagnosis7"].ToString());
                            diaPointer += "G";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis8"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis8"].ToString() != null)
                        {
                            pdfFormFields.SetField("113", dsPdfValue.Tables[0].Rows[0]["Diagnosis8"].ToString());
                            diaPointer += "H";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis9"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis9"].ToString() != null)
                        {
                            pdfFormFields.SetField("1080", dsPdfValue.Tables[0].Rows[0]["Diagnosis9"].ToString());
                            diaPointer += "I";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis10"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis10"].ToString() != null)
                        {
                            pdfFormFields.SetField("1090", dsPdfValue.Tables[0].Rows[0]["Diagnosis10"].ToString());
                            diaPointer += "J";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis11"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis11"].ToString() != null)
                        {
                            pdfFormFields.SetField("1100", dsPdfValue.Tables[0].Rows[0]["Diagnosis11"].ToString());
                            diaPointer += "K";
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["Diagnosis12"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis12"].ToString() != null)
                        {
                            pdfFormFields.SetField("1110", dsPdfValue.Tables[0].Rows[0]["Diagnosis12"].ToString());
                            diaPointer += "L";
                        }
                        //}

                        //by aarti

                        //if (dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != null)
                        //{
                        //    string[] diag1 = dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString().Split('.');
                        //    if (diag1.Length > 1)
                        //    {
                        //        pdfFormFields.SetField("102", diag1[0].ToString());
                        //        pdfFormFields.SetField("104", diag1[1].ToString());
                        //    }
                        //    else
                        //    {
                        //        pdfFormFields.SetField("102", diag1[0].ToString());
                        //    }
                        //    diaPointer = "A";
                        //}
                        //if (dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != null)
                        //{
                        //    string[] diag2 = dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString().Split('.');
                        //    if (diag2.Length > 1)
                        //    {
                        //        pdfFormFields.SetField("108", diag2[0].ToString());
                        //        pdfFormFields.SetField("110", diag2[1].ToString());
                        //    }
                        //    else
                        //    {
                        //        pdfFormFields.SetField("108", diag2[0].ToString());
                        //    }

                        //    diaPointer += "B";
                        //}
                        //if (dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != null)
                        //{
                        //    string[] diag3 = dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString().Split('.');
                        //    if (diag3.Length > 1)
                        //    {
                        //        pdfFormFields.SetField("105", diag3[0].ToString());
                        //        pdfFormFields.SetField("107", diag3[1].ToString());
                        //    }
                        //    else
                        //    {
                        //        pdfFormFields.SetField("105", diag3[0].ToString());
                        //    }
                        //    diaPointer += "C";
                        //}
                        //if (dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != null)
                        //{
                        //    string[] diag4 = dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString().Split('.');
                        //    if (diag4.Length > 1)
                        //    {
                        //        pdfFormFields.SetField("111", diag4[0].ToString());
                        //        pdfFormFields.SetField("113", diag4[1].ToString());
                        //    }
                        //    else
                        //    {
                        //        pdfFormFields.SetField("111", diag4[0].ToString());
                        //    }
                        //    diaPointer += "D";
                        //}


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

                                        codeDesc =  dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString() ;
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
                                        codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                        codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                        codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                        codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                        codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                    dt.Columns.Add("SZ_1500_DESC");
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
                        drRow["SZ_1500_DESC"] = dsProcValue.Tables[0].Rows[j]["SZ_1500_DESC"];
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
                    oDT.Columns.Add("SZ_1500_DESC");
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
                        drrRow["SZ_1500_DESC"] = dsProcValue.Tables[0].Rows[j]["SZ_1500_DESC"];
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

            throw new Exception(ex.Message, ex);

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
   
    public DataSet getPDFData(string billNumber, string company_ID, ServerConnection conn)
    {
        DataSet ds;
        String Query = "";



        ds = new DataSet();
        try
        {
            try
            {
                if (System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                {
                    Query = Query + "Exec SP_GET_PRIVATE_BILL_NEW_SEC ";

                }
                else
                {
                    Query = Query + "Exec SP_GET_PRIVATE_BILL_NEW ";

                }
            }
            catch (Exception ex)
            {
                Query = Query + "Exec SP_GET_PRIVATE_BILL_NEW ";

            }
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", billNumber, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", company_ID, ",");


            Query = Query.TrimEnd(',');
            ds = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            new Exception(ex.Message, ex);
        }
        finally
        {

        }

        return ds;
    }

    //public DataSet getServiceTableData(string billNumber, string company_ID)
    //{
    //    DataSet ds;
    //    SqlCommand comm;
    //    SqlDataAdapter da;

    //    string strConn = "";
    //    strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

    //    SqlConnection conn;
    //    conn = new SqlConnection(strConn);
    //    conn.Open();
    //    ds = new DataSet();
    //    try
    //    {
    //        comm = new SqlCommand("SP_GET_PROC_CODE_OF_BILL", conn);
    //        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

    //        comm.CommandType = CommandType.StoredProcedure;
    //        comm.Parameters.AddWithValue("@SZ_BILL_ID", billNumber);
    //        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", company_ID);
    //        da = new SqlDataAdapter(comm);
    //        da.Fill(ds);
    //    }
    //    catch (Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    }
    //    finally
    //    {
    //        conn.Close();
    //    }

    //    return ds;
    //}

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
    public DataSet getServiceTableData(string billNumber, string company_ID ,ServerConnection conn)
    {
        DataSet ds;
        
        ds = new DataSet();
        try
        {
            String Query = "";
            Query = Query + "Exec SP_GET_PROC_CODE_OF_BILL ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", billNumber, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", company_ID, ",");
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

    private string getApplicationSetting(String p_szKey)
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

    //private string getPacketDocumentFolder(string p_szPath, string p_szCompanyID, string p_szCaseID)
    //{
    //    p_szPath = p_szPath + p_szCompanyID + "/" + p_szCaseID;
    //    if (Directory.Exists(p_szPath))
    //    {
    //        if (!Directory.Exists(p_szPath + "/Packet Document"))
    //        {
    //            Directory.CreateDirectory(p_szPath + "/Packet Document");
    //        }
    //    }
    //    else
    //    {
    //        Directory.CreateDirectory(p_szPath);
    //        Directory.CreateDirectory(p_szPath + "/Packet Document");
    //    }
    //    return p_szPath + "/Packet Document/";
    //}

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


    //public void PdfPrint(string pdfFileName, DataSet dsPdfValue, DataSet dsProcValue, string szCompanyID,string szCaseID,string newPdfFilename)
    //{
    //    string pdfTemplate = pdfFileName;
    //    string  OutputFilePath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID) + newPdfFilename;
    //    log.Debug("OutputFilePath " + OutputFilePath);
    //   // DataSet dsPdfValue = getPDFData(billNumber, compId);
    //   // DataSet dsProcValue = getServiceTableData(billNumber, compId);

    //    PdfReader pdfReader = new PdfReader(pdfTemplate);
    //    //PdfReader.unethicalreading = true; 
    //    PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(OutputFilePath, FileMode.Create));
    //    AcroFields pdfFormFields = pdfStamper.AcroFields;
    //    if (dsPdfValue != null)
    //    {
    //        if (dsPdfValue.Tables[0] != null)
    //        {
    //            //pdfFormFields.SetField("Name", "Insurance company name");
    //            pdfFormFields.SetField("Name", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
    //            pdfFormFields.SetField("2", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString());
    //            //pdfFormFields.SetField("3", "Insurance company address");
    //            pdfFormFields.SetField("3", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS2"].ToString());
    //            //pdfFormFields.SetField("4", "Insurance company address");
    //            pdfFormFields.SetField("4", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString());
    //            //pdfFormFields.SetField("8", "Yes");
    //            //pdfFormFields.SetField("9", "Yes");
    //            //pdfFormFields.SetField("10", "Yes");
    //            //pdfFormFields.SetField("11", "Yes");
    //            if (dsPdfValue.Tables[0].Rows[0]["SZ_ABBRIVATION"].ToString() == "PVT")
    //            {
    //                pdfFormFields.SetField("12", "1");
    //            }
    //            else
    //            {
    //                pdfFormFields.SetField("14", "1");
    //            }
    //            //pdfFormFields.SetField("13", "Yes");

    //            //Amod
    //            //pdfFormFields.SetField("14", "1");
    //            //pdfFormFields.SetField("15", "Insured ID No");

    //            // Amod
    //            if (dsPdfValue.Tables[0].Rows[0]["SZ_CLAIM_NO"] != null)
    //            {
    //                pdfFormFields.SetField("15", dsPdfValue.Tables[0].Rows[0]["SZ_CLAIM_NO"].ToString());
    //            }

    //            ////pdfFormFields.SetField("16", "Patien's Name LFM");
    //            ////pdfFormFields.SetField("17", "Patient's DOB  MM");
    //            ////pdfFormFields.SetField("18", "Patient's DOB  DD");
    //            ////pdfFormFields.SetField("19", "Patient's DOB  YYYY");
    //            ////pdfFormFields.SetField("20", "Yes");
    //            ////pdfFormFields.SetField("21", "Yes");
    //            ////pdfFormFields.SetField("22", "Insured's Name LFM");
    //            ////pdfFormFields.SetField("23", "Patient's Address No,Street");
    //            ////pdfFormFields.SetField("24", "Patient's Address city");
    //            ////pdfFormFields.SetField("25", "Patient's Address state");
    //            ////pdfFormFields.SetField("26", "Patient's Address zip");
    //            ////pdfFormFields.SetField("27", "Patient's Address tele 1");
    //            ////pdfFormFields.SetField("28", "Patient's Address tele 2");
    //            pdfFormFields.SetField("16", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString());

    //            // Amod - if the month is 1 digit then prefix it with 0
    //            if (dsPdfValue.Tables[0].Rows[0]["DOB_MM"] != null)
    //            {
    //                if (!dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString().Equals(""))
    //                {
    //                    try
    //                    {
    //                        int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
    //                        if (i <= 9)
    //                        {
    //                            pdfFormFields.SetField("17", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
    //                        }
    //                        else
    //                        {
    //                            pdfFormFields.SetField("17", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
    //                        }
    //                    }
    //                    catch (Exception i)
    //                    {
    //                        pdfFormFields.SetField("17", dsPdfValue.Tables[0].Rows[0]["DOB_MM"].ToString());
    //                    }
    //                }
    //            }

    //            //pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //            if (dsPdfValue.Tables[0].Rows[0]["DOB_DD"] != null)
    //            {
    //                if (!dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString().Equals(""))
    //                {
    //                    try
    //                    {
    //                        int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //                        if (i <= 9)
    //                        {
    //                            pdfFormFields.SetField("18", "0" + dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //                        }
    //                        else
    //                        {
    //                            pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //                        }
    //                    }
    //                    catch (Exception i)
    //                    {
    //                        pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //                    }
    //                }
    //            }
    //            pdfFormFields.SetField("19", dsPdfValue.Tables[0].Rows[0]["DOB_YY"].ToString());
    //            if (dsPdfValue.Tables[0].Rows[0]["MALE"].ToString() == "1")
    //            {
    //                pdfFormFields.SetField("20", "No");
    //            }
    //            if (dsPdfValue.Tables[0].Rows[0]["FEMALE"].ToString() == "1")
    //            {
    //                pdfFormFields.SetField("21", "No");
    //            }
    //            if (szCompanyID == "CO000000000000000129")
    //            {
    //                pdfFormFields.SetField("22", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString());
    //            }
    //            else
    //            {
    //                pdfFormFields.SetField("22", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString());
    //            }
    //            pdfFormFields.SetField("23", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString());
    //            pdfFormFields.SetField("24", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
    //            pdfFormFields.SetField("25", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
    //            pdfFormFields.SetField("26", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());
    //            pdfFormFields.SetField("27", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_1_3"].ToString());
    //            pdfFormFields.SetField("28", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_4_E"].ToString());

    //            if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "1")
    //            {
    //                pdfFormFields.SetField("29", "1");
    //            }
    //            else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "2")
    //            {
    //                pdfFormFields.SetField("30", "1");
    //            }
    //            else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "3")
    //            {
    //                pdfFormFields.SetField("31", "1");
    //            }
    //            else if (dsPdfValue.Tables[0].Rows[0]["SZ_RELACTION_WITH_PATIENT"].ToString() == "4")
    //            {
    //                pdfFormFields.SetField("32", "1");
    //            }
    //            //pdfFormFields.SetField("29", "Yes");
    //            //pdfFormFields.SetField("30", "Yes");
    //            //pdfFormFields.SetField("31", "Yes");
    //            //pdfFormFields.SetField("32", "Yes");
    //            if (szCompanyID == "CO000000000000000129")
    //            {
    //                pdfFormFields.SetField("33", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString());
    //                pdfFormFields.SetField("34", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
    //                pdfFormFields.SetField("35", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
    //                pdfFormFields.SetField("36", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());
    //                pdfFormFields.SetField("37", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_1_3"].ToString());
    //                pdfFormFields.SetField("38", dsPdfValue.Tables[0].Rows[0]["SZ_PATIENT_PHONE_4_E"].ToString());
    //            }
    //            else
    //            {
    //                pdfFormFields.SetField("33", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ADDRESS"].ToString());
    //                pdfFormFields.SetField("34", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_CITY"].ToString());
    //                pdfFormFields.SetField("35", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_STATE_ID"].ToString());
    //                pdfFormFields.SetField("36", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_ZIP"].ToString());
    //                pdfFormFields.SetField("37", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE_1_3"].ToString());
    //                pdfFormFields.SetField("38", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_HOLDER_PHONE_4_E"].ToString());
    //            }

    //            if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "1")
    //            {
    //                pdfFormFields.SetField("39", "1");
    //            }
    //            else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "2")
    //            {
    //                pdfFormFields.SetField("40", "1");
    //            }
    //            else if (dsPdfValue.Tables[0].Rows[0]["SZ_MARRIED_STATUS"].ToString() == "3")
    //            {
    //                pdfFormFields.SetField("41", "1");
    //            }

    //            //pdfFormFields.SetField("39", "Yes");
    //            //pdfFormFields.SetField("40", "Yes");
    //            //pdfFormFields.SetField("41", "Yes");
    //            //pdfFormFields.SetField("42", "Yes");//?????????/
    //            //pdfFormFields.SetField("43", "Yes");
    //            //pdfFormFields.SetField("44", "Yes");
    //            //pdfFormFields.SetField("45", "OTHER INSURED’S NAME LFM");
    //            ////pdfFormFields.SetField("46", "OTHER INSURED’S policy/grp no");
    //            pdfFormFields.SetField("46", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_NO"].ToString());
    //            //pdfFormFields.SetField("47", "OTHER INSURED’S DOB MM");
    //            //pdfFormFields.SetField("48", "OTHER INSURED’S DOB DD");
    //            //pdfFormFields.SetField("49", "OTHER INSURED’S DOB YYYY");
    //            //pdfFormFields.SetField("50", "Yes");
    //            //pdfFormFields.SetField("51", "Yes");
    //            //pdfFormFields.SetField("52", "EMPLOYER’S NAME OR SCHOOL NAME");
    //            //pdfFormFields.SetField("53", "INSURANCE PLAN NAME OR PROGRAM NAME");
    //            //pdfFormFields.SetField("54", "Yes");
    //            //pdfFormFields.SetField("55", "Yes");
    //            //pdfFormFields.SetField("56", "Yes");
    //            //pdfFormFields.SetField("57", "Yes");

    //            //pdfFormFields.SetField("59", "Yes");
    //            if (szCompanyID == "CO000000000000000129")
    //            {
    //                pdfFormFields.SetField("55", "1");
    //                pdfFormFields.SetField("57", "1");
    //                pdfFormFields.SetField("60", "1");
    //            }
    //            else
    //            {
    //                if (dsPdfValue.Tables[0].Rows[0]["SZ_ABBRIVATION"].ToString() == "WC")
    //                {
    //                    pdfFormFields.SetField("54", "1");
    //                    pdfFormFields.SetField("57", "1");
    //                    pdfFormFields.SetField("60", "1");
    //                }
    //                else if (dsPdfValue.Tables[0].Rows[0]["SZ_ABBRIVATION"].ToString() == "NF")
    //                {
    //                    pdfFormFields.SetField("56", "1");
    //                    pdfFormFields.SetField("58", dsPdfValue.Tables[0].Rows[0]["SZ_ACCIDENT_STATE_ID"].ToString());
    //                    pdfFormFields.SetField("55", "1");
    //                    pdfFormFields.SetField("60", "1");
    //                }
    //                else
    //                {
    //                    pdfFormFields.SetField("59", "1");
    //                    pdfFormFields.SetField("55", "1");
    //                    pdfFormFields.SetField("57", "1");
    //                }
    //            }
    //            //pdfFormFields.SetField("61", "RESERVED FOR LOCAL USE");
    //            //pdfFormFields.SetField("62", "INSURED’S POLICY GROUP OR FECA NUMBER");
    //            //pdfFormFields.SetField("63", "INSURED’S DOB MM");
    //            //pdfFormFields.SetField("64", "INSURED’S DOB DD");
    //            //pdfFormFields.SetField("65", "INSURED’S DOB YYYY");
    //            if (dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"] != null)
    //            {
    //                if (!dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"].ToString().Equals(""))
    //                {
    //                    try
    //                    {
    //                        int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"].ToString());
    //                        if (i <= 9)
    //                        {
    //                            pdfFormFields.SetField("63", "0" + dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"].ToString());
    //                        }
    //                        else
    //                        {
    //                            pdfFormFields.SetField("63", dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"].ToString());
    //                        }
    //                    }
    //                    catch (Exception i)
    //                    {
    //                        pdfFormFields.SetField("63", dsPdfValue.Tables[0].Rows[0]["INS_DOB_MM"].ToString());
    //                    }
    //                }
    //            }

    //            //pdfFormFields.SetField("18", dsPdfValue.Tables[0].Rows[0]["DOB_DD"].ToString());
    //            if (dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"] != null)
    //            {
    //                if (!dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"].ToString().Equals(""))
    //                {
    //                    try
    //                    {
    //                        int i = Convert.ToInt32(dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"].ToString());
    //                        if (i <= 9)
    //                        {
    //                            pdfFormFields.SetField("64", "0" + dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"].ToString());
    //                        }
    //                        else
    //                        {
    //                            pdfFormFields.SetField("64", dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"].ToString());
    //                        }
    //                    }
    //                    catch (Exception i)
    //                    {
    //                        pdfFormFields.SetField("64", dsPdfValue.Tables[0].Rows[0]["INS_DOB_DD"].ToString());
    //                    }
    //                }
    //            }
    //            pdfFormFields.SetField("65", dsPdfValue.Tables[0].Rows[0]["INS_DOB_YY"].ToString());
    //            if (dsPdfValue.Tables[0].Rows[0]["INS_IS_MALE"].ToString() == "1")
    //            {
    //                pdfFormFields.SetField("66", "Yes");
    //            }
    //            else if (dsPdfValue.Tables[0].Rows[0]["INS_IS_FEMALE"].ToString() == "1")
    //            {
    //                pdfFormFields.SetField("67", "No");
    //            }
    //            //pdfFormFields.SetField("66", "Yes");
    //            //pdfFormFields.SetField("67", "Yes");
    //            //pdfFormFields.SetField("68", "EMPLOYER’S NAME OR SCHOOL NAME");
    //            ////pdfFormFields.SetField("69", "INSURANCE PLAN NAME OR PROGRAM NAME");
    //            //pdfFormFields.SetField("Name", );
    //            //Amod
    //            pdfFormFields.SetField("69", dsPdfValue.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
    //            //pdfFormFields.SetField("70", "Yes");
    //            //pdfFormFields.SetField("71", "Yes");

    //            //Amod
    //            pdfFormFields.SetField("72", "SIGNATURE ON FILE");

    //            //Amod
    //            pdfFormFields.SetField("73", dsPdfValue.Tables[0].Rows[0]["SZ_TODAY"].ToString());

    //            //Amod
    //            pdfFormFields.SetField("74", "SIGNATURE ON FILE");

    //            //Amod
    //            pdfFormFields.SetField("75", dsPdfValue.Tables[0].Rows[0]["SZ_MM_ACCIDENT"].ToString());

    //            //Amod
    //            pdfFormFields.SetField("76", dsPdfValue.Tables[0].Rows[0]["SZ_DD_ACCIDENT"].ToString());

    //            //Amod
    //            pdfFormFields.SetField("77", dsPdfValue.Tables[0].Rows[0]["SZ_YY_ACCIDENT"].ToString());

    //            //pdfFormFields.SetField("78", "first treat Date MM");
    //            //pdfFormFields.SetField("79", "first treat Date DD");
    //            //pdfFormFields.SetField("80", "first treat Date YYYY");
    //            //pdfFormFields.SetField("81", "unable work from Date MM");
    //            //pdfFormFields.SetField("82", "unable work from Date DD");
    //            //pdfFormFields.SetField("83", "unable work from Date YYYY");
    //            //pdfFormFields.SetField("84", "unable work to Date MM");
    //            //pdfFormFields.SetField("85", "unable work to Date DD");
    //            //pdfFormFields.SetField("86", "unable work to Date YYYY");
    //            //pdfFormFields.SetField("87", "NAME OF REFERRING PROVIDER OR OTHER SOURCE");
    //            pdfFormFields.SetField("87", dsPdfValue.Tables[0].Rows[0]["sz_reff_doctor"].ToString());
    //            pdfFormFields.SetField("90", dsPdfValue.Tables[0].Rows[0]["sz_reff_doctor_npi"].ToString());

    //            if (dsPdfValue.Tables[0].Rows[0]["IS_OUT_LAB_YES"].ToString() == "1")
    //            {
    //                pdfFormFields.SetField("98", "No");
    //            }
    //            else if (dsPdfValue.Tables[0].Rows[0]["IS_OUT_LAB_NO"].ToString() == "1")
    //            {
    //                pdfFormFields.SetField("99", "No");
    //            }

    //            string diaPointer = "";
    //            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != null)
    //            {
    //                string[] diag1 = dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString().Split('.');
    //                if (diag1.Length > 1)
    //                {
    //                    pdfFormFields.SetField("102", diag1[0].ToString());
    //                    pdfFormFields.SetField("104", diag1[1].ToString());
    //                }
    //                else
    //                {
    //                    pdfFormFields.SetField("102", diag1[0].ToString());
    //                }
    //                diaPointer = "1";
    //            }
    //            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != null)
    //            {
    //                string[] diag2 = dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString().Split('.');
    //                if (diag2.Length > 1)
    //                {
    //                    pdfFormFields.SetField("105", diag2[0].ToString());
    //                    pdfFormFields.SetField("107", diag2[1].ToString());
    //                }
    //                else
    //                {
    //                    pdfFormFields.SetField("105", diag2[0].ToString());
    //                }

    //                diaPointer += ",2";
    //            }
    //            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != null)
    //            {
    //                string[] diag3 = dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString().Split('.');
    //                if (diag3.Length > 1)
    //                {
    //                    pdfFormFields.SetField("108", diag3[0].ToString());
    //                    pdfFormFields.SetField("110", diag3[1].ToString());
    //                }
    //                else
    //                {
    //                    pdfFormFields.SetField("108", diag3[0].ToString());
    //                }
    //                diaPointer += ",3";
    //            }
    //            if (dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != null)
    //            {
    //                string[] diag4 = dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString().Split('.');
    //                if (diag4.Length > 1)
    //                {
    //                    pdfFormFields.SetField("111", diag4[0].ToString());
    //                    pdfFormFields.SetField("113", diag4[1].ToString());
    //                }
    //                else
    //                {
    //                    pdfFormFields.SetField("111", diag4[0].ToString());
    //                }
    //                diaPointer += ",4";
    //            }

    //            ////pdfFormFields.SetField("102", "diagnosis pre");
    //            ////pdfFormFields.SetField("104", "dia pro");

    //            ////pdfFormFields.SetField("105", "diagnosis pre");
    //            ////pdfFormFields.SetField("107", "dia pro");

    //            ////pdfFormFields.SetField("108", "diagnosis pre");
    //            ////pdfFormFields.SetField("110", "dia pro");

    //            ////pdfFormFields.SetField("111", "diagnosis pre");
    //            ////pdfFormFields.SetField("113", "dia pro");
    //            string firstModifier = "";
    //            string multiModifier = "";

    //            double totcharge = 0.0, paidAmt = 0.0, balAmt = 0.0;
    //            if (dsProcValue != null)
    //            {
    //                if (dsProcValue.Tables.Count > 0 && dsProcValue.Tables[0].Rows.Count > 0)
    //                {
    //                    //////////////////////1/////////////////////////////
    //                    for (int i = 0; i < dsProcValue.Tables[0].Rows.Count; i++)
    //                    {
    //                        firstModifier = "";
    //                        multiModifier = "";
    //                        if (dsPdfValue.Tables[0].Rows[0]["multiple_modifier"].ToString() == "1")
    //                        {
    //                            if (dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString() != "" && dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString() != null)
    //                            {
    //                                string[] modifiers = dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString().Split(',');
    //                                if (modifiers.Length > 1)
    //                                {
    //                                    firstModifier = modifiers[0].ToString();
    //                                    for (int j = 1; j < modifiers.Length; j++)
    //                                    {
    //                                        multiModifier += modifiers[j].ToString() + ",";
    //                                    }
    //                                    multiModifier = multiModifier.Substring(0, multiModifier.Length - 1);
    //                                }
    //                                else
    //                                {
    //                                    firstModifier = modifiers[0].ToString();
    //                                }
    //                            }
    //                        }
    //                        else
    //                        {
    //                            firstModifier = dsProcValue.Tables[0].Rows[i]["SZ_MODIFIER"].ToString();
    //                            multiModifier = "";
    //                        }
    //                        if (i == 0)
    //                        {
    //                            pdfFormFields.SetField("120", dsProcValue.Tables[0].Rows[i]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("121", dsProcValue.Tables[0].Rows[i]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("122", dsProcValue.Tables[0].Rows[i]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("123", dsProcValue.Tables[0].Rows[i]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("124", dsProcValue.Tables[0].Rows[i]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("125", dsProcValue.Tables[0].Rows[i]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("126", dsProcValue.Tables[0].Rows[i]["PALCE OF SERVICE"].ToString());
    //                            pdfFormFields.SetField("128", dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
    //                            pdfFormFields.SetField("129", firstModifier);
    //                            pdfFormFields.SetField("130", multiModifier);
    //                            pdfFormFields.SetField("133", diaPointer);
    //                            pdfFormFields.SetField("c1", dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
    //                            totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                            pdfFormFields.SetField("138", dsProcValue.Tables[0].Rows[i]["SZ_NPI"].ToString());
    //                            pdfFormFields.SetField("136", dsProcValue.Tables[0].Rows[i]["I_UNIT"].ToString());
    //                        }
    //                        //////////////////////2/////////////////////////////
    //                        if (i == 1)
    //                        {
    //                            pdfFormFields.SetField("142", dsProcValue.Tables[0].Rows[1]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("143", dsProcValue.Tables[0].Rows[1]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("144", dsProcValue.Tables[0].Rows[1]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("145", dsProcValue.Tables[0].Rows[1]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("146", dsProcValue.Tables[0].Rows[1]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("147", dsProcValue.Tables[0].Rows[1]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("148", dsProcValue.Tables[0].Rows[1]["PALCE OF SERVICE"].ToString());
    //                            pdfFormFields.SetField("150", dsProcValue.Tables[0].Rows[1]["SZ_PROCEDURE_CODE"].ToString());
    //                            pdfFormFields.SetField("151", firstModifier);
    //                            pdfFormFields.SetField("152", multiModifier);
    //                            pdfFormFields.SetField("155", diaPointer);
    //                            pdfFormFields.SetField("c2", dsProcValue.Tables[0].Rows[1]["FL_AMOUNT"].ToString());
    //                            totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                            pdfFormFields.SetField("160", dsProcValue.Tables[0].Rows[1]["SZ_NPI"].ToString());
    //                            pdfFormFields.SetField("158", dsProcValue.Tables[0].Rows[1]["I_UNIT"].ToString());

    //                        }
    //                        //////////////////////3/////////////////////////////
    //                        if (i == 2)
    //                        {
    //                            pdfFormFields.SetField("164", dsProcValue.Tables[0].Rows[2]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("165", dsProcValue.Tables[0].Rows[2]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("166", dsProcValue.Tables[0].Rows[2]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("167", dsProcValue.Tables[0].Rows[2]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("168", dsProcValue.Tables[0].Rows[2]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("169", dsProcValue.Tables[0].Rows[2]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("170", dsProcValue.Tables[0].Rows[2]["PALCE OF SERVICE"].ToString());
    //                            pdfFormFields.SetField("172", dsProcValue.Tables[0].Rows[2]["SZ_PROCEDURE_CODE"].ToString());
    //                            pdfFormFields.SetField("173", firstModifier);
    //                            pdfFormFields.SetField("174", multiModifier);
    //                            pdfFormFields.SetField("177", diaPointer);
    //                            pdfFormFields.SetField("c3", dsProcValue.Tables[0].Rows[2]["FL_AMOUNT"].ToString());
    //                            totcharge += (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                            pdfFormFields.SetField("182", dsProcValue.Tables[0].Rows[2]["SZ_NPI"].ToString());
    //                            pdfFormFields.SetField("180", dsProcValue.Tables[0].Rows[2]["I_UNIT"].ToString());
    //                        }
    //                        //////////////////////4////////////////////////////
    //                        if (i == 3)
    //                        {
    //                            pdfFormFields.SetField("186", dsProcValue.Tables[0].Rows[3]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("187", dsProcValue.Tables[0].Rows[3]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("188", dsProcValue.Tables[0].Rows[3]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("189", dsProcValue.Tables[0].Rows[3]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("190", dsProcValue.Tables[0].Rows[3]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("191", dsProcValue.Tables[0].Rows[3]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("192", dsProcValue.Tables[0].Rows[3]["PALCE OF SERVICE"].ToString());
    //                            pdfFormFields.SetField("194", dsProcValue.Tables[0].Rows[3]["SZ_PROCEDURE_CODE"].ToString());
    //                            pdfFormFields.SetField("195", firstModifier);
    //                            pdfFormFields.SetField("196", multiModifier);
    //                            pdfFormFields.SetField("199", diaPointer);
    //                            pdfFormFields.SetField("c4", dsProcValue.Tables[0].Rows[3]["FL_AMOUNT"].ToString());
    //                            totcharge +=  (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                            pdfFormFields.SetField("204", dsProcValue.Tables[0].Rows[3]["SZ_NPI"].ToString());
    //                            pdfFormFields.SetField("202", dsProcValue.Tables[0].Rows[3]["I_UNIT"].ToString());
    //                        }
    //                        if (i == 4)
    //                        {
    //                            //////////////////////5/////////////////////////////
    //                            pdfFormFields.SetField("208", dsProcValue.Tables[0].Rows[4]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("209", dsProcValue.Tables[0].Rows[4]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("210", dsProcValue.Tables[0].Rows[4]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("211", dsProcValue.Tables[0].Rows[4]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("212", dsProcValue.Tables[0].Rows[4]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("213", dsProcValue.Tables[0].Rows[4]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("214", dsProcValue.Tables[0].Rows[4]["PALCE OF SERVICE"].ToString());
    //                            pdfFormFields.SetField("216", dsProcValue.Tables[0].Rows[4]["SZ_PROCEDURE_CODE"].ToString());
    //                            pdfFormFields.SetField("217", firstModifier);
    //                            pdfFormFields.SetField("218", multiModifier);
    //                            pdfFormFields.SetField("221", diaPointer);
    //                            pdfFormFields.SetField("c5", dsProcValue.Tables[0].Rows[4]["FL_AMOUNT"].ToString());
    //                            totcharge +=  (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                            pdfFormFields.SetField("226", dsProcValue.Tables[0].Rows[4]["SZ_NPI"].ToString());
    //                            pdfFormFields.SetField("224", dsProcValue.Tables[0].Rows[4]["I_UNIT"].ToString());
    //                        }
    //                        if (i == 5)
    //                        {
    //                            //////////////////////6/////////////////////////////
    //                            pdfFormFields.SetField("230", dsProcValue.Tables[0].Rows[5]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("231", dsProcValue.Tables[0].Rows[5]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("232", dsProcValue.Tables[0].Rows[5]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("233", dsProcValue.Tables[0].Rows[5]["DOS_MM"].ToString());
    //                            pdfFormFields.SetField("234", dsProcValue.Tables[0].Rows[5]["DOS_DD"].ToString());
    //                            pdfFormFields.SetField("235", dsProcValue.Tables[0].Rows[5]["DOS_YY"].ToString());
    //                            pdfFormFields.SetField("236", dsProcValue.Tables[0].Rows[5]["PALCE OF SERVICE"].ToString());
    //                            pdfFormFields.SetField("238", dsProcValue.Tables[0].Rows[5]["SZ_PROCEDURE_CODE"].ToString());
    //                            pdfFormFields.SetField("239", firstModifier);
    //                            pdfFormFields.SetField("240", multiModifier);
    //                            pdfFormFields.SetField("243", diaPointer);
    //                            pdfFormFields.SetField("c6", dsProcValue.Tables[0].Rows[5]["FL_AMOUNT"].ToString());
    //                            totcharge +=  (Convert.ToDouble(dsProcValue.Tables[0].Rows[i]["FL_AMOUNT"].ToString()));
    //                            pdfFormFields.SetField("248", dsProcValue.Tables[0].Rows[5]["SZ_NPI"].ToString());
    //                            pdfFormFields.SetField("246", dsProcValue.Tables[0].Rows[5]["I_UNIT"].ToString());
    //                        }
    //                    }
    //                }
    //            }
    //            ////pdfFormFields.SetField("249", "provider TAX I.D.");
    //            ////pdfFormFields.SetField("250", "Yes");//
    //            ////pdfFormFields.SetField("251", "Yes");
    //            pdfFormFields.SetField("249", dsPdfValue.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
    //            //pdfFormFields.SetField("250", "Yes");//
    //            if (dsPdfValue.Tables[0].Rows[0]["TAX_TYPE"].ToString().ToLower() == "false")
    //            {
    //                pdfFormFields.SetField("250", "No");
    //            }
    //            else if (dsPdfValue.Tables[0].Rows[0]["TAX_TYPE"].ToString().ToLower() == "true")
    //            {
    //                pdfFormFields.SetField("251", "No");
    //            }

    //            ////pdfFormFields.SetField("252", "Pat acc no");
    //            //pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_BILL_NUMBER"].ToString());
    //            if (szCompanyID == "CO000000000000000129")
    //            {
    //                pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_CASE_NO"].ToString());
    //            }
    //            else
    //            {
    //                pdfFormFields.SetField("252", dsPdfValue.Tables[0].Rows[0]["SZ_CASE_NO"].ToString());
    //            }
    //            ////pdfFormFields.SetField("253", "Yes");
    //            if (dsPdfValue.Tables[0].Rows[0]["ACCEPT_ASSIGNMENT"].ToString() == "1")
    //            {
    //                pdfFormFields.SetField("253", "No");
    //            }
    //            ////pdfFormFields.SetField("254", "Yes");//

    //            ////pdfFormFields.SetField("261", "Doctor Name");
    //            balAmt = totcharge;
    //            pdfFormFields.SetField("C255", Convert.ToString(totcharge));
    //            pdfFormFields.SetField("C257", paidAmt.ToString());

    //            //Amod commented
    //            //pdfFormFields.SetField("C259", balAmt.ToString());
    //            pdfFormFields.SetField("C259", Convert.ToString(totcharge));

    //            pdfFormFields.SetField("261", dsPdfValue.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
    //            pdfFormFields.SetField("31SIGN", dsPdfValue.Tables[0].Rows[0]["SIGNATURE_ON_FILE"].ToString());
    //            pdfFormFields.SetField("262", dsPdfValue.Tables[0].Rows[0]["DATE"].ToString());
    //            pdfFormFields.SetField("263", dsPdfValue.Tables[0].Rows[0]["SZ_REFF_OFFICE_NAME"].ToString());
    //            ////pdfFormFields.SetField("264", "Provider address");
    //            ////pdfFormFields.SetField("265", "Provider city,state,zip");
    //            pdfFormFields.SetField("264", dsPdfValue.Tables[0].Rows[0]["SZ_REFF_OFFICE_ADDRESS"].ToString());
    //            pdfFormFields.SetField("265", dsPdfValue.Tables[0].Rows[0]["SZ_REFF_OFFICE_ADDRESS2"].ToString());

    //            ////pdfFormFields.SetField("266", "Provider NPI");
    //            if (szCompanyID == "CO000000000000000129")
    //            {
    //                pdfFormFields.SetField("266", dsProcValue.Tables[0].Rows[0]["SZ_NPI"].ToString());
    //            }
    //            else
    //            {
    //                pdfFormFields.SetField("266", dsPdfValue.Tables[0].Rows[0]["MST_REFF_OFFICE_NPI"].ToString());
    //            }

    //            pdfFormFields.SetField("267", dsPdfValue.Tables[0].Rows[0]["32b"].ToString());

    //            // Amod - commented
    //            //pdfFormFields.SetField("267", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_1_3"].ToString() + dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_4_E"].ToString());

    //            ////pdfFormFields.SetField("273", "provider NPI");
    //            if (szCompanyID == "CO000000000000000129")
    //            {
    //                pdfFormFields.SetField("273", dsProcValue.Tables[0].Rows[0]["SZ_NPI"].ToString());
    //            }
    //            else
    //            {
    //                pdfFormFields.SetField("273", dsPdfValue.Tables[0].Rows[0]["MST_OFFICE_NPI"].ToString());

    //            }
    //            //pdfFormFields.SetField("273", dsPdfValue.Tables[0].Rows[0]["MST_OFFICE_NPI"].ToString());
    //            pdfFormFields.SetField("274", dsPdfValue.Tables[0].Rows[0]["33b"].ToString());

    //            ////pdfFormFields.SetField("268", "provider phone 3");
    //            ////pdfFormFields.SetField("269", "provider phone rem");
    //            pdfFormFields.SetField("268", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_1_3"].ToString());
    //            pdfFormFields.SetField("269", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_PHONE_4_E"].ToString());

    //            ////pdfFormFields.SetField("270", "Billing provider name");
    //            pdfFormFields.SetField("270", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString());
    //            pdfFormFields.SetField("271", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
    //            pdfFormFields.SetField("272", dsPdfValue.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS2"].ToString());

    //            pdfStamper.FormFlattening = true;
    //            pdfStamper.Close();
    //        }
    //    }

    //    //return newPdfFilename;

    //}


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
                //pdfFormFields.SetField("45", "OTHER INSURED’S NAME LFM");
                ////pdfFormFields.SetField("46", "OTHER INSURED’S policy/grp no");
                //pdfFormFields.SetField("46", dsPdfValue.Tables[0].Rows[0]["SZ_POLICY_NO"].ToString());
                //pdfFormFields.SetField("47", "OTHER INSURED’S DOB MM");
                //pdfFormFields.SetField("48", "OTHER INSURED’S DOB DD");
                //pdfFormFields.SetField("49", "OTHER INSURED’S DOB YYYY");
                //pdfFormFields.SetField("50", "Yes");
                //pdfFormFields.SetField("51", "Yes");
                //pdfFormFields.SetField("52", "EMPLOYER’S NAME OR SCHOOL NAME");
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
                //pdfFormFields.SetField("63", "INSURED’S DOB MM");
                //pdfFormFields.SetField("64", "INSURED’S DOB DD");
                //pdfFormFields.SetField("65", "INSURED’S DOB YYYY");
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
                //pdfFormFields.SetField("68", "EMPLOYER’S NAME OR SCHOOL NAME");
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
                pdfFormFields.SetField("101", "0");
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
                //if (dsPdfValue.Tables[0].Rows[0]["sz_abbrivation_id"].ToString() == "WC000000000000000003")
                //{
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
                if (dsPdfValue.Tables[0].Rows[0]["Diagnosis5"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis5"].ToString() != null)
                {
                    pdfFormFields.SetField("105", dsPdfValue.Tables[0].Rows[0]["Diagnosis5"].ToString());
                    diaPointer += "E";
                }
                if (dsPdfValue.Tables[0].Rows[0]["Diagnosis6"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis6"].ToString() != null)
                {
                    pdfFormFields.SetField("107", dsPdfValue.Tables[0].Rows[0]["Diagnosis6"].ToString());
                    diaPointer += "F";
                }
                if (dsPdfValue.Tables[0].Rows[0]["Diagnosis7"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis7"].ToString() != null)
                {
                    pdfFormFields.SetField("111", dsPdfValue.Tables[0].Rows[0]["Diagnosis7"].ToString());
                    diaPointer += "G";
                }
                if (dsPdfValue.Tables[0].Rows[0]["Diagnosis8"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis8"].ToString() != null)
                {
                    pdfFormFields.SetField("113", dsPdfValue.Tables[0].Rows[0]["Diagnosis8"].ToString());
                    diaPointer += "H";
                }
                if (dsPdfValue.Tables[0].Rows[0]["Diagnosis9"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis9"].ToString() != null)
                {
                    pdfFormFields.SetField("1080", dsPdfValue.Tables[0].Rows[0]["Diagnosis9"].ToString());
                    diaPointer += "I";
                }
                if (dsPdfValue.Tables[0].Rows[0]["Diagnosis10"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis10"].ToString() != null)
                {
                    pdfFormFields.SetField("1090", dsPdfValue.Tables[0].Rows[0]["Diagnosis10"].ToString());
                    diaPointer += "J";
                }
                if (dsPdfValue.Tables[0].Rows[0]["Diagnosis11"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis11"].ToString() != null)
                {
                    pdfFormFields.SetField("1100", dsPdfValue.Tables[0].Rows[0]["Diagnosis11"].ToString());
                    diaPointer += "K";
                }
                if (dsPdfValue.Tables[0].Rows[0]["Diagnosis12"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis12"].ToString() != null)
                {
                    pdfFormFields.SetField("1110", dsPdfValue.Tables[0].Rows[0]["Diagnosis12"].ToString());
                    diaPointer += "L";
                }
                // }

                //if (dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString() != null)
                //    {
                //        string[] diag1 = dsPdfValue.Tables[0].Rows[0]["Diagnosis1"].ToString().Split('.');
                //        if (diag1.Length > 1)
                //        {
                //            pdfFormFields.SetField("102", diag1[0].ToString());
                //            pdfFormFields.SetField("104", diag1[1].ToString());
                //        }
                //        else
                //        {
                //            pdfFormFields.SetField("102", diag1[0].ToString());
                //        }
                //        diaPointer = "A";
                //    }
                //    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString() != null)
                //    {
                //        string[] diag2 = dsPdfValue.Tables[0].Rows[0]["Diagnosis2"].ToString().Split('.');
                //        if (diag2.Length > 1)
                //        {
                //            pdfFormFields.SetField("108", diag2[0].ToString());
                //            pdfFormFields.SetField("110", diag2[1].ToString());
                //        }
                //        else
                //        {
                //            pdfFormFields.SetField("108", diag2[0].ToString());
                //        }

                //        diaPointer += "B";
                //    }
                //    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString() != null)
                //    {
                //        string[] diag3 = dsPdfValue.Tables[0].Rows[0]["Diagnosis3"].ToString().Split('.');
                //        if (diag3.Length > 1)
                //        {
                //            pdfFormFields.SetField("105", diag3[0].ToString());
                //            pdfFormFields.SetField("107", diag3[1].ToString());
                //        }
                //        else
                //        {
                //            pdfFormFields.SetField("105", diag3[0].ToString());
                //        }
                //        diaPointer += "C";
                //    }
                //    if (dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != "" && dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString() != null)
                //    {
                //        string[] diag4 = dsPdfValue.Tables[0].Rows[0]["Diagnosis4"].ToString().Split('.');
                //        if (diag4.Length > 1)
                //        {
                //            pdfFormFields.SetField("111", diag4[0].ToString());
                //            pdfFormFields.SetField("113", diag4[1].ToString());
                //        }
                //        else
                //        {
                //            pdfFormFields.SetField("111", diag4[0].ToString());
                //        }
                //        diaPointer += "D";
                //    }


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
                                codeDesc =  dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString() ;
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
                                codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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
                                codeDesc = dsProcValue.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "  " + dsProcValue.Tables[0].Rows[i]["SZ_1500_DESC"].ToString();
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