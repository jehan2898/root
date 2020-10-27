using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.IO;
using testXFAItextSharp.DAO;

/// <summary>
/// Summary description for Bill_Sys_C4
/// </summary>
public class Bill_Sys_C4PDF
{
    public Bill_Sys_C4PDF()
    {
        //
        // TODO: Add constructor logic here
        //
        
    }

    public string printC4(string szPdfPath, string szBillNumber, string szCompanyID, string szCaseID)
    {
        Bill_Sys_C4DataBind objC4Data = new Bill_Sys_C4DataBind();
        Bill_Sys_C4DAO objC4DAO = new Bill_Sys_C4DAO();
        //string szBillNo = "mo1316";
        DataSet ds = objC4Data.GetData(szBillNumber);
        DataSet ds_Service_Table = objC4Data.Get_Service_Table(szBillNumber);
        DataSet ds_Patient_Complaints = objC4Data.Get_Patient_Complaints(szBillNumber);
        DataSet ds_Patient_TypeInjury = objC4Data.Get_Patient_TypeInjury(szBillNumber);
        DataSet ds_Patient_Exam = objC4Data.Get_PATIENT_PHYSICAL_EXAM(szBillNumber);
        DataSet ds_Diagnosis_Test = objC4Data.Get_DIAGNOSTIC_TESTS(szBillNumber);
        DataSet ds_Referral = objC4Data.Get_REFERRALS(szBillNumber);
        DataSet ds_Assistive_Device = objC4Data.Get_ASSISTIVE_DEVICES(szBillNumber);
        DataSet ds_Work_Status = objC4Data.Get_WORK_STATUS(szBillNumber);

        string newPdfFilename = getFileName(szBillNumber) + ".pdf";

        string outPutFilePath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID) + newPdfFilename;


        objC4DAO = objC4Data.bindObject(outPutFilePath, szBillNumber, ds, ds_Service_Table, ds_Patient_Complaints, ds_Patient_TypeInjury, ds_Patient_Exam, ds_Diagnosis_Test, ds_Referral, ds_Assistive_Device, ds_Work_Status);

        //ListFieldNames();
        FillForm(objC4DAO, szPdfPath, outPutFilePath);
        return newPdfFilename;
    }

    public string printC4Part1(string szPdfPath, string szBillNumber, string szCompanyID, string szCaseID)
    {
        Bill_Sys_C4DataBind objC4Data = new Bill_Sys_C4DataBind();
        Bill_Sys_C4DAO objC4DAO = new Bill_Sys_C4DAO();
        //string szBillNo = "mo1316";
        DataSet ds = objC4Data.GetData(szBillNumber);
        DataSet ds_Service_Table = objC4Data.Get_Service_Table(szBillNumber);
        DataSet ds_Patient_Complaints = objC4Data.Get_Patient_Complaints(szBillNumber);
        DataSet ds_Patient_TypeInjury = objC4Data.Get_Patient_TypeInjury(szBillNumber);
        DataSet ds_Patient_Exam = objC4Data.Get_PATIENT_PHYSICAL_EXAM(szBillNumber);
        DataSet ds_Diagnosis_Test = objC4Data.Get_DIAGNOSTIC_TESTS(szBillNumber);
        DataSet ds_Referral = objC4Data.Get_REFERRALS(szBillNumber);
        DataSet ds_Assistive_Device = objC4Data.Get_ASSISTIVE_DEVICES(szBillNumber);
        DataSet ds_Work_Status = objC4Data.Get_WORK_STATUS(szBillNumber);

        string newPdfFilename = getFileName(szBillNumber) + ".pdf";

        string outPutFilePath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID) + newPdfFilename;

        string szReqPdfPath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P1"].ToString();//PDF_FILE_PATH_C4_P1
        if (szPdfPath.Contains("c4_worestrictions.pdf"))// == "E:\\Work\\BILLINGSYSTEM\\Code\\branch\\1.0.0\\application\\c4_worestrictions.pdf")
        {
            objC4DAO = objC4Data.bindObject(outPutFilePath, szBillNumber, ds, ds_Service_Table, ds_Patient_Complaints, ds_Patient_TypeInjury, ds_Patient_Exam, ds_Diagnosis_Test, ds_Referral, ds_Assistive_Device, ds_Work_Status);
            FillForm(objC4DAO, szPdfPath, outPutFilePath);
        }
        else if (szPdfPath == szReqPdfPath)// E:\\Work\\BILLINGSYSTEM\\Code\\branch\\1.0.0\\application\\c4.0Part1_New.pdf
        {
            objC4DAO = objC4Data.bindObject1(outPutFilePath, szBillNumber, ds);
            FillPart1(objC4DAO, szPdfPath, outPutFilePath);
        }
        return newPdfFilename;
    }

    public string printC4Part2(string szPdfPath, string szBillNumber, string szCompanyID, string szCaseID)
    {
        Bill_Sys_C4DataBind objC4Data = new Bill_Sys_C4DataBind();
        Bill_Sys_C4DAO objC4DAO = new Bill_Sys_C4DAO();
        //string szBillNo = "mo1316";
        DataSet ds = objC4Data.GetData(szBillNumber);
        DataSet ds_Service_Table = objC4Data.Get_Service_Table(szBillNumber);
        DataSet ds_Patient_Complaints = objC4Data.Get_Patient_Complaints(szBillNumber);
        DataSet ds_Patient_TypeInjury = objC4Data.Get_Patient_TypeInjury(szBillNumber);
        DataSet ds_Patient_Exam = objC4Data.Get_PATIENT_PHYSICAL_EXAM(szBillNumber);
        DataSet ds_Diagnosis_Test = objC4Data.Get_DIAGNOSTIC_TESTS(szBillNumber);
        DataSet ds_Referral = objC4Data.Get_REFERRALS(szBillNumber);
        DataSet ds_Assistive_Device = objC4Data.Get_ASSISTIVE_DEVICES(szBillNumber);
        DataSet ds_Work_Status = objC4Data.Get_WORK_STATUS(szBillNumber);

        string newPdfFilename = getFileName(szBillNumber) + ".pdf";

        string outPutFilePath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID) + newPdfFilename;

        string szReqPdfPath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P2"].ToString();

        if (szPdfPath.Contains("c4_worestrictions.pdf") )//== "E:\\Work\\BILLINGSYSTEM\\Code\\branch\\1.0.0\\application\\c4_worestrictions.pdf")
        {
            objC4DAO = objC4Data.bindObject(outPutFilePath, szBillNumber, ds, ds_Service_Table, ds_Patient_Complaints, ds_Patient_TypeInjury, ds_Patient_Exam, ds_Diagnosis_Test, ds_Referral, ds_Assistive_Device, ds_Work_Status);
            FillForm(objC4DAO, szPdfPath, outPutFilePath);
        }
        else if (szPdfPath.Contains("c4.0Part1_New.pdf") ) //== "e:/Work/BILLINGSYSTEM/Code/branch/1.0.0/application/c4.0Part1_New.pdf")// E:\\Work\\BILLINGSYSTEM\\Code\\branch\\1.0.0\\application\\c4.0Part1_New.pdf
        {
            objC4DAO = objC4Data.bindObject1(outPutFilePath, szBillNumber, ds);
            FillPart1(objC4DAO, szPdfPath, outPutFilePath);
        }
        else if (szPdfPath == szReqPdfPath)// E:\\Work\\BILLINGSYSTEM\\Code\\branch\\1.0.0\\application\\c4.0Part1_New.pdf
        {
            objC4DAO = objC4Data.bindObject(outPutFilePath, szBillNumber, ds, ds_Service_Table, ds_Patient_Complaints, ds_Patient_TypeInjury, ds_Patient_Exam, ds_Diagnosis_Test, ds_Referral, ds_Assistive_Device, ds_Work_Status);
            FillPart2(objC4DAO, szPdfPath, outPutFilePath);
        }
        return newPdfFilename;
    }

    public string printC4ForTest(string szPdfPath, string szBillNumber, string szCompanyID, string szCaseID)
    {
        Bill_Sys_C4DataBind objC4Data = new Bill_Sys_C4DataBind();
        Bill_Sys_C4DAO objC4DAO = new Bill_Sys_C4DAO();
        //string szBillNo = "mo1316";
        DataSet ds = objC4Data.GetDataForTest(szBillNumber);
        DataSet ds_Service_Table = objC4Data.Get_Service_Table(szBillNumber);
        DataSet ds_Patient_Complaints = objC4Data.Get_Patient_Complaints(szBillNumber);
        DataSet ds_Patient_TypeInjury = objC4Data.Get_Patient_TypeInjury(szBillNumber);
        DataSet ds_Patient_Exam = objC4Data.Get_PATIENT_PHYSICAL_EXAM(szBillNumber);
        DataSet ds_Diagnosis_Test = objC4Data.Get_DIAGNOSTIC_TESTS(szBillNumber);
        DataSet ds_Referral = objC4Data.Get_REFERRALS(szBillNumber);
        DataSet ds_Assistive_Device = objC4Data.Get_ASSISTIVE_DEVICES(szBillNumber);
        DataSet ds_Work_Status = objC4Data.Get_WORK_STATUS(szBillNumber);

        string newPdfFilename = getFileName(szBillNumber) + ".pdf";

        string outPutFilePath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID) + newPdfFilename;


        objC4DAO = objC4Data.bindObject(outPutFilePath, szBillNumber, ds, ds_Service_Table, ds_Patient_Complaints, ds_Patient_TypeInjury, ds_Patient_Exam, ds_Diagnosis_Test, ds_Referral, ds_Assistive_Device, ds_Work_Status);

        //ListFieldNames();
        FillForm(objC4DAO, szPdfPath, outPutFilePath);
        return newPdfFilename;
    }

    public string printC4Part1ForTest(string szPdfPath, string szBillNumber, string szCompanyID, string szCaseID)
    {
        Bill_Sys_C4DataBind objC4Data = new Bill_Sys_C4DataBind();
        Bill_Sys_C4DAO objC4DAO = new Bill_Sys_C4DAO();
        //string szBillNo = "mo1316";
        DataSet ds = objC4Data.GetDataForTest(szBillNumber);
        DataSet ds_Service_Table = objC4Data.Get_Service_Table(szBillNumber);
        DataSet ds_Patient_Complaints = objC4Data.Get_Patient_Complaints(szBillNumber);
        DataSet ds_Patient_TypeInjury = objC4Data.Get_Patient_TypeInjury(szBillNumber);
        DataSet ds_Patient_Exam = objC4Data.Get_PATIENT_PHYSICAL_EXAM(szBillNumber);
        DataSet ds_Diagnosis_Test = objC4Data.Get_DIAGNOSTIC_TESTS(szBillNumber);
        DataSet ds_Referral = objC4Data.Get_REFERRALS(szBillNumber);
        DataSet ds_Assistive_Device = objC4Data.Get_ASSISTIVE_DEVICES(szBillNumber);
        DataSet ds_Work_Status = objC4Data.Get_WORK_STATUS(szBillNumber);

        string newPdfFilename = getFileName(szBillNumber) + ".pdf";

        string outPutFilePath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID) + newPdfFilename;

        string szReqPdfPath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P1"].ToString();

        if (szPdfPath.Contains("c4_worestrictions.pdf"))// == "E:\\Work\\BILLINGSYSTEM\\Code\\branch\\1.0.0\\application\\c4_worestrictions.pdf")
        {
            objC4DAO = objC4Data.bindObject(outPutFilePath, szBillNumber, ds, ds_Service_Table, ds_Patient_Complaints, ds_Patient_TypeInjury, ds_Patient_Exam, ds_Diagnosis_Test, ds_Referral, ds_Assistive_Device, ds_Work_Status);
            FillForm(objC4DAO, szPdfPath, outPutFilePath);
        }
        else if (szPdfPath == szReqPdfPath)// E:\\Work\\BILLINGSYSTEM\\Code\\branch\\1.0.0\\application\\c4.0Part1_New.pdf
        {
            objC4DAO = objC4Data.bindObject1(outPutFilePath, szBillNumber, ds);
            FillPart1(objC4DAO, szPdfPath, outPutFilePath);
        }
        return newPdfFilename;
    }

    public string printC4Part2ForTest(string szPdfPath, string szBillNumber, string szCompanyID, string szCaseID)
    {
        Bill_Sys_C4DataBind objC4Data = new Bill_Sys_C4DataBind();
        Bill_Sys_C4DAO objC4DAO = new Bill_Sys_C4DAO();
        //string szBillNo = "mo1316";
        DataSet ds = objC4Data.GetDataForTest(szBillNumber);
        DataSet ds_Service_Table = objC4Data.Get_Service_Table(szBillNumber);
        DataSet ds_Patient_Complaints = objC4Data.Get_Patient_Complaints(szBillNumber);
        DataSet ds_Patient_TypeInjury = objC4Data.Get_Patient_TypeInjury(szBillNumber);
        DataSet ds_Patient_Exam = objC4Data.Get_PATIENT_PHYSICAL_EXAM(szBillNumber);
        DataSet ds_Diagnosis_Test = objC4Data.Get_DIAGNOSTIC_TESTS(szBillNumber);
        DataSet ds_Referral = objC4Data.Get_REFERRALS(szBillNumber);
        DataSet ds_Assistive_Device = objC4Data.Get_ASSISTIVE_DEVICES(szBillNumber);
        DataSet ds_Work_Status = objC4Data.Get_WORK_STATUS(szBillNumber);

        string newPdfFilename = getFileName(szBillNumber) + ".pdf";

        string outPutFilePath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID) + newPdfFilename;

        string szReqPdfPath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P2"].ToString();

        if (szPdfPath.Contains("c4_worestrictions.pdf"))// == "E:\\Work\\BILLINGSYSTEM\\Code\\branch\\1.0.0\\application\\c4_worestrictions.pdf")
        {
            objC4DAO = objC4Data.bindObject(outPutFilePath, szBillNumber, ds, ds_Service_Table, ds_Patient_Complaints, ds_Patient_TypeInjury, ds_Patient_Exam, ds_Diagnosis_Test, ds_Referral, ds_Assistive_Device, ds_Work_Status);
            FillForm(objC4DAO, szPdfPath, outPutFilePath);
        }
        else if (szPdfPath.Contains("c4.0Part1_New.pdf"))// == "e:/Work/BILLINGSYSTEM/Code/branch/1.0.0/application/c4.0Part1_New.pdf")// E:\\Work\\BILLINGSYSTEM\\Code\\branch\\1.0.0\\application\\c4.0Part1_New.pdf
        {
            objC4DAO = objC4Data.bindObject1(outPutFilePath, szBillNumber, ds);
            FillPart1(objC4DAO, szPdfPath, outPutFilePath);
        }
        else if (szPdfPath == szReqPdfPath)// E:\\Work\\BILLINGSYSTEM\\Code\\branch\\1.0.0\\application\\c4.0Part1_New.pdf
        {
            objC4DAO = objC4Data.bindObject(outPutFilePath, szBillNumber, ds, ds_Service_Table, ds_Patient_Complaints, ds_Patient_TypeInjury, ds_Patient_Exam, ds_Diagnosis_Test, ds_Referral, ds_Assistive_Device, ds_Work_Status);
            FillPart2(objC4DAO, szPdfPath, outPutFilePath);
        }
        return newPdfFilename;
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
    //private void ListFieldNames()
    //{
        
    //    string pdfTemplate = @"E:\Work\TestXFA\TemplatePDF\c4_worestrictions.pdf";
    //    this.Text += " - " + pdfTemplate;

    //    PdfReader pdfReader = new PdfReader(pdfTemplate);
    //    ArrayList ar = new ArrayList();
    //    StringBuilder sb = new StringBuilder();

    //    foreach (DictionaryEntry de in pdfReader.AcroFields.Fields)
    //    {
    //        ar.Add(de.Key.ToString());
    //        sb.Append(de.Key.ToString() + Environment.NewLine);

    //    }
    //}

    private void FillPart1(Bill_Sys_C4DAO PdfDAO, string szPdfPath, string outPutFilePath)
    {
        //string pdfTemplate = @"E:\Work\TestXFA\TemplatePDF\c4.0Part1_New.pdf";
        //string newFile = @"E:\Work\TestXFA\C4.0.1.pdf";
        string pdfTemplate = szPdfPath;
        string newFile = outPutFilePath;


        PdfReader pdfReader = new PdfReader(pdfTemplate);
        PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
        AcroFields pdfFormFields = pdfStamper.AcroFields;

        pdfFormFields.SetField("SZ_PATIENT_LAST_NAME", PdfDAO.LastName.ToString());
        pdfFormFields.SetField("SZ_PATIENT_FIRST_NAME", PdfDAO.FirstName.ToString());
        pdfFormFields.SetField("SZ_MI", PdfDAO.middleInitial.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_SOCIAL_SECURITY_NO", PdfDAO.socialSecNumber.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_HOME_PHONE1", PdfDAO.patientHomePhone1.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_HOME_PHONE2", PdfDAO.patientHomePhone2.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_WCB_NO", PdfDAO.WCBNumber.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_CARRIER_CASE_NO", PdfDAO.carrierCaseNo.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_FULL_ADDRESS", PdfDAO.patientAddress.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_CITY", PdfDAO.patientCity.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_STATE", PdfDAO.patientState.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_ZIP", PdfDAO.patientZip.ToString());
        pdfFormFields.SetField("DT_DATE_OF_ACCIDENT_DD", PdfDAO.accidentDate.ToString());
        pdfFormFields.SetField("DT_DATE_OF_ACCIDENT_MM", PdfDAO.accidentMonth.ToString());
        pdfFormFields.SetField("DT_DATE_OF_ACCIDENT_YY", PdfDAO.accidentYear.ToString());
        pdfFormFields.SetField("form1[0].P1[0].DOB_DD", PdfDAO.DOBDate.ToString());
        pdfFormFields.SetField("form1[0].P1[0].DOB_MM", PdfDAO.DOBMonth.ToString());
        pdfFormFields.SetField("form1[0].P1[0].DOB_YY", PdfDAO.DOBYear.ToString());

        // The form's checkboxes

        pdfFormFields.SetField("form1[0].P1[0].chkMale", PdfDAO.chkMale.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chkFemale", PdfDAO.chkFemale.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chk_SSN", PdfDAO.chk_SSN.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chk_EIN", PdfDAO.chk_EIN.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chkPhysician", PdfDAO.chkPhysician.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chkPodiatrist", PdfDAO.chkPodiatrist.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chkChiropractor", PdfDAO.chkChiropractor.ToString());


        // The rest of the form pdfFormFields

        pdfFormFields.SetField("form1[0].P1[0].SZ_JOB_TITLE", PdfDAO.jobTitle.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_WORK_ACTIVITIES", PdfDAO.workActivity.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_WORK_ACTIVITIES_1", PdfDAO.workActivity1.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_ACCOUNT_NO", PdfDAO.patientAccountNo.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_NAME", PdfDAO.employerName.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_PHONE1", PdfDAO.employerPhone1.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_PHONE2", PdfDAO.employerPhone2.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_ADDRESS", PdfDAO.SZ_EMPLOYER_ADDRESS.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_CITY", PdfDAO.SZ_EMPLOYER_CITY.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_STATE", PdfDAO.SZ_EMPLOYER_STATE.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_ZIP", PdfDAO.SZ_EMPLOYER_ZIP.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_DOCTOR_NAME", PdfDAO.SZ_DOCTOR_NAME.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_WCB_AUTHORIZATION", PdfDAO.SZ_WCB_AUTHORIZATION.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_WCB_RATING_CODE", PdfDAO.SZ_WCB_RATING_CODE.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_FEDERAL_TAX_ID", PdfDAO.SZ_FEDERAL_TAX_ID.ToString());//
        pdfFormFields.SetField("form1[0].P1[0].SZ_CARRIER_CODE", PdfDAO.SZ_CARRIER_CODE.ToString());

        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_ADDRESS", PdfDAO.SZ_OFFICE_ADDRESS.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_CITY", PdfDAO.SZ_OFFICE_CITY.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_STATE", PdfDAO.SZ_OFFICE_STATE.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_ZIP", PdfDAO.SZ_OFFICE_ZIP.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_GROUP_NAME", PdfDAO.SZ_BILLING_GROUP_NAME.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_ADDRESS", PdfDAO.SZ_BILLING_ADDRESS.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_CITY", PdfDAO.SZ_BILLING_CITY.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_STATE", PdfDAO.SZ_BILLING_STATE.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_ZIP", PdfDAO.SZ_BILLING_ZIP.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_PHONE1", PdfDAO.SZ_OFFICE_PHONE1.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_PHONE2", PdfDAO.SZ_OFFICE_PHONE2.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_PHONE1", PdfDAO.SZ_BILLING_PHONE1.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_PHONE2", PdfDAO.SZ_BILLING_PHONE2.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_NPI", PdfDAO.SZ_NPI.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_INSURANCE_NAME", PdfDAO.SZ_INSURANCE_NAME.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_INSURANCE_STREET", PdfDAO.SZ_INSURANCE_STREET.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_INSURANCE_CITY", PdfDAO.SZ_INSURANCE_CITY.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_INSURANCE_STATE", PdfDAO.SZ_INSURANCE_STATE.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_INSURANCE_ZIP", PdfDAO.SZ_INSURANCE_ZIP.ToString());
        pdfFormFields.SetField("SZ_DIAGNOSIS", PdfDAO.SZ_DIAGNOSIS_CODE.ToString());
        //pdfFormFields.SetField("SZ_BILL_ID", PdfDAO.SZ_BILL_ID.ToString());

        //string sTmp = "PDF Generated";
        //MessageBox.Show(sTmp, "Finished");
        pdfStamper.FormFlattening = true;
        // close the pdf

        pdfStamper.Close();
    }

    private void FillPart2(Bill_Sys_C4DAO PdfDAO, string szPdfPath, string outPutFilePath)
    {
        //string pdfTemplate = @"E:\Work\TestXFA\TemplatePDF\c4.0Part1_New.pdf";
        //string newFile = @"E:\Work\TestXFA\C4.0.1.pdf";
        string pdfTemplate = szPdfPath;
        string newFile = outPutFilePath;


        PdfReader pdfReader = new PdfReader(pdfTemplate);
        PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
        AcroFields pdfFormFields = pdfStamper.AcroFields;

        pdfFormFields.SetField("SZ_PATIENT_LAST_NAME", PdfDAO.LastName.ToString());
        pdfFormFields.SetField("SZ_PATIENT_FIRST_NAME", PdfDAO.FirstName.ToString());
        pdfFormFields.SetField("SZ_MI", PdfDAO.middleInitial.ToString());
        //pdfFormFields.SetField("SZ_BILL_ID", PdfDAO.SZ_BILL_ID.ToString());

        pdfFormFields.SetField("DT_DATE_OF_ACCIDENT_DD", PdfDAO.accidentDate.ToString());
        pdfFormFields.SetField("DT_DATE_OF_ACCIDENT_MM", PdfDAO.accidentMonth.ToString());
        pdfFormFields.SetField("DT_DATE_OF_ACCIDENT_YY", PdfDAO.accidentYear.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_INJURY_ILLNESS_DETAIL", PdfDAO.SZ_INJURY_ILLNESS_DETAIL.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_INJURY_ILLNESS_DETAIL_1", PdfDAO.SZ_INJURY_ILLNESS_DETAIL_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_INJURY_ILLNESS_DETAIL_2", PdfDAO.SZ_INJURY_ILLNESS_DETAIL_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_INJURY_LEARN_SOURCE_DESCRIPTION", PdfDAO.SZ_INJURY_LEARN_SOURCE_DESCRIPTION.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION", PdfDAO.SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION.ToString());
        pdfFormFields.SetField("form1[0].P2[0].DT_PREVIOUSLY_TREATED_DATE", PdfDAO.DT_PREVIOUSLY_TREATED_DATE.ToString());
        pdfFormFields.SetField("form1[0].P2[0].DT_DATE_OF_EXAMINATION", PdfDAO.DT_DATE_OF_EXAMINATION.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Numbness", PdfDAO.txt_F_Numbness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Pain", PdfDAO.txt_F_Pain.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Stiffness", PdfDAO.txt_F_Stiffness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Swelling", PdfDAO.txt_F_Swelling.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Weakness", PdfDAO.txt_F_Weakness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Other", PdfDAO.txt_F_Other.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Abrasion", PdfDAO.txt_F_3_Abrasion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Amputation", PdfDAO.txt_F_3_Amputation.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Avulsion", PdfDAO.txt_F_3_Avulsion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Bite", PdfDAO.txt_F_3_Bite.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Burn", PdfDAO.txt_F_3_Burn.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Contusion", PdfDAO.txt_F_3_Contusion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_CurshInjury", PdfDAO.txt_F_3_CurshInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Dermatitis", PdfDAO.txt_F_3_Dermatitis.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Dislocation", PdfDAO.txt_F_3_Dislocation.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Fracture", PdfDAO.txt_F_3_Fracture.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_HearingLoss", PdfDAO.txt_F_3_HearingLoss.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Hernia", PdfDAO.txt_F_3_Hernia.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Other", PdfDAO.txt_F_3_Other.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Other_1", PdfDAO.txt_F_3_Other_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_InfectiousDisease", PdfDAO.txt_F_3_InfectiousDisease.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Inhalation_Exposure", PdfDAO.txt_F_3_Inhalation_Exposure.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Laceration", PdfDAO.txt_F_3_Laceration.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_NeedleStick", PdfDAO.txt_F_3_NeedleStick.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Poisoning", PdfDAO.txt_F_3_Poisoning.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Psychological", PdfDAO.txt_F_3_Psychological.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_PuntureWound", PdfDAO.txt_F_3_PuntureWound.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_RepetitiveStrainInjury", PdfDAO.txt_F_3_RepetitiveStrainInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_SpinalCordInjury", PdfDAO.txt_F_3_SpinalCordInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Sprain", PdfDAO.txt_F_3_Sprain.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Torn", PdfDAO.txt_F_3_Torn.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_VisionLoss", PdfDAO.txt_F_3_VisionLoss.ToString());

        //PAGE 2 CHECKBOXES
        pdfFormFields.SetField("form1[0].P2[0].chkService", PdfDAO.chkService.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_Patient", PdfDAO.chk_H_Patient.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_MedicalRecords", PdfDAO.chk_H_MedicalRecords.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_Other", PdfDAO.chk_H_Other.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_Yes", PdfDAO.chk_H_Yes.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_No", PdfDAO.chk_H_No.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_4_Yes", PdfDAO.chk_H_4_Yes.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_4_No", PdfDAO.chk_H_4_No.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Numbness", PdfDAO.chk_F_Numbness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Pain", PdfDAO.chk_F_Pain.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Stiffness", PdfDAO.chk_F_Stiffness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Swelling", PdfDAO.chk_F_Swelling.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Weakness", PdfDAO.chk_F_Weakness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Other", PdfDAO.chk_F_Other.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Abrasion", PdfDAO.chk_F_3_Abrasion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Amputation", PdfDAO.chk_F_3_Amputation.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Avulsion", PdfDAO.chk_F_3_Avulsion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Bite", PdfDAO.chk_F_3_Bite.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Burn", PdfDAO.chk_F_3_Burn.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Contusion", PdfDAO.chk_F_3_Contusion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_CurshInjury", PdfDAO.chk_F_3_CurshInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Dermatitis", PdfDAO.chk_F_3_Dermatitis.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Dislocation", PdfDAO.chk_F_3_Dislocation.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Fracture", PdfDAO.chk_F_3_Fracture.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_HearingLoss", PdfDAO.chk_F_3_HearingLoss.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Hernia", PdfDAO.chk_F_3_Hernia.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Other", PdfDAO.chk_F_3_Other.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_InfectiousDisease", PdfDAO.chk_F_3_InfectiousDisease.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Inhalation_Exposure", PdfDAO.chk_F_3_Inhalation_Exposure.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Laceration", PdfDAO.chk_F_3_Laceration.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_NeedleStick", PdfDAO.chk_F_3_NeedleStick.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Poisoning", PdfDAO.chk_F_3_Poisoning.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Psychological", PdfDAO.chk_F_3_Psychological.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_PuntureWound", PdfDAO.chk_F_3_PuntureWound.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_RepetitiveStrainInjury", PdfDAO.chk_F_3_RepetitiveStrainInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_SpinalCordInjury", PdfDAO.chk_F_3_SpinalCordInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Sprain", PdfDAO.chk_F_3_Sprain.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Torn", PdfDAO.chk_F_3_Torn.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_VisionLoss", PdfDAO.chk_F_3_VisionLoss.ToString());

        //PAGE 3
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Bruising", PdfDAO.txt_F_4_Bruising.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Burns", PdfDAO.txt_F_4_Burns.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Crepitation", PdfDAO.txt_F_4_Crepitation.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Deformity", PdfDAO.txt_F_4_Deformity.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Edema", PdfDAO.txt_F_4_Edema.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Hematoma", PdfDAO.txt_F_4_Hematoma.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_JointEffusion", PdfDAO.txt_F_4_JointEffusion.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Laceration", PdfDAO.txt_F_4_Laceration.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Pain", PdfDAO.txt_F_4_Pain.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Scar", PdfDAO.txt_F_4_Scar.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_OtherFindings", PdfDAO.txt_F_4_OtherFindings.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_ActiveROM", PdfDAO.txt_F_4_ActiveROM.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_PassiveROM", PdfDAO.txt_F_4_PassiveROM.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Gait", PdfDAO.txt_F_4_Gait.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Palpable", PdfDAO.txt_F_4_Palpable.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Reflexes", PdfDAO.txt_F_4_Reflexes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Sensation", PdfDAO.txt_F_4_Sensation.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Strength", PdfDAO.txt_F_4_Strength.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Wasting", PdfDAO.txt_F_4_Wasting.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_DIAGNOSTIC_TEST", PdfDAO.SZ_DIAGNOSTIC_TEST.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_DIAGNOSTIC_TEST_1", PdfDAO.SZ_DIAGNOSTIC_TEST_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_TREATMENT", PdfDAO.SZ_TREATMENT.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_TREATMENT_1", PdfDAO.SZ_TREATMENT_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROGNOSIS_RECOVERY", PdfDAO.SZ_PROGNOSIS_RECOVERY.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROGNOSIS_RECOVERY_1", PdfDAO.SZ_PROGNOSIS_RECOVERY_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROGNOSIS_RECOVERY_2", PdfDAO.SZ_PROGNOSIS_RECOVERY_2.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION", PdfDAO.SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1", PdfDAO.SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].FT_TEMPORARY_IMPAIRMENT_PERCENTAGE", PdfDAO.FT_TEMPORARY_IMPAIRMENT_PERCENTAGE.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_TEST_RESULTS", PdfDAO.SZ_TEST_RESULTS.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_TEST_RESULTS_1", PdfDAO.SZ_TEST_RESULTS_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROPOSED_TREATEMENT", PdfDAO.SZ_PROPOSED_TREATEMENT.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROPOSED_TREATEMENT_1", PdfDAO.SZ_PROPOSED_TREATEMENT_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROPOSED_TREATEMENT_3", PdfDAO.SZ_PROPOSED_TREATEMENT_3.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_MEDICATIONS_PRESCRIBED", PdfDAO.SZ_MEDICATIONS_PRESCRIBED.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_MEDICATIONS_ADVISED", PdfDAO.SZ_MEDICATIONS_ADVISED.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_MEDICATION_RESTRICTIONS_DESCRIPTION", PdfDAO.SZ_MEDICATION_RESTRICTIONS_DESCRIPTION.ToString());
        pdfFormFields.SetField("form1[0].P3[0].Text226", PdfDAO.Text226.ToString());

        //PAGE 3 CHECKBOXES
        pdfFormFields.SetField("form1[0].P3[0].chk_F_7_None", PdfDAO.chk_F_7_None.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Bruising", PdfDAO.chk_F_4_Bruising.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Burns", PdfDAO.chk_F_4_Burns.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Crepitation", PdfDAO.chk_F_4_Crepitation.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Deformity", PdfDAO.chk_F_4_Deformity.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Edema", PdfDAO.chk_F_4_Edema.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Hematoma", PdfDAO.chk_F_4_Hematoma.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_JointEffusion", PdfDAO.chk_F_4_JointEffusion.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Laceration", PdfDAO.chk_F_4_Laceration.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Pain", PdfDAO.chk_F_4_Pain.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Scar", PdfDAO.chk_F_4_Scar.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_OtherFindings", PdfDAO.chk_F_4_OtherFindings.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Neuromuscular", PdfDAO.chk_F_4_Neuromuscular.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Abnormal", PdfDAO.chk_F_4_Abnormal.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_ActiveROM", PdfDAO.chk_F_4_ActiveROM.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_PassiveROM", PdfDAO.chk_F_4_PassiveROM.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Gait", PdfDAO.chk_F_4_Gait.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Palpable", PdfDAO.chk_F_4_Palpable.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Reflexes", PdfDAO.chk_F_4_Reflexes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Sensation", PdfDAO.chk_F_4_Sensation.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Strength", PdfDAO.chk_F_4_Strength.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Wasting", PdfDAO.chk_F_4_Wasting.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_8_Yes", PdfDAO.chk_F_8_Yes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_8_No", PdfDAO.chk_F_8_No.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_1_Yes", PdfDAO.chk_G_1_Yes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_1_No", PdfDAO.chk_G_1_No.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_2_Yes", PdfDAO.chk_G_2_Yes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_2_No", PdfDAO.chk_G_2_No.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_3_Yes", PdfDAO.chk_G_3_Yes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_3_No", PdfDAO.chk_G_3_No.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_3_NA", PdfDAO.chk_G_3_NA.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_H_2_None", PdfDAO.chk_H_2_None.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_H_2_Other", PdfDAO.chk_H_2_Other.ToString());


        //PAGE 4
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_MRI", PdfDAO.txt_H_3_MRI.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_Labs", PdfDAO.txt_H_3_Labs.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_XRay", PdfDAO.txt_H_3_XRay.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_Left_Other", PdfDAO.txt_H_3_Left_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_Specialist", PdfDAO.txt_H_3_Specialist.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_Right_Other", PdfDAO.txt_H_3_Right_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_4_Other", PdfDAO.txt_H_4_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_5_Month", PdfDAO.txt_H_5_Month.ToString());
        pdfFormFields.SetField("form1[0].P4[0].DT_PATIENT_MISSED_WORK_DATE_DD", PdfDAO.DT_PATIENT_MISSED_WORK_DATE_DD.ToString());
        pdfFormFields.SetField("form1[0].P4[0].DT_PATIENT_MISSED_WORK_DATE_MM", PdfDAO.DT_PATIENT_MISSED_WORK_DATE_MM.ToString());
        pdfFormFields.SetField("form1[0].P4[0].DT_PATIENT_MISSED_WORK_DATE_YY", PdfDAO.DT_PATIENT_MISSED_WORK_DATE_YY.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_PATIENT_RETURN_WORK_DESCRIPTION", PdfDAO.SZ_PATIENT_RETURN_WORK_DESCRIPTION.ToString());
        pdfFormFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_DD", PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_DD.ToString());
        pdfFormFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_MM", PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_MM.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_PATIENT_RETURN_WORK_LIMITATION_YY", PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_YY.ToString());
        pdfFormFields.SetField("form1[0].P4[0].Text249_DD", PdfDAO.Text249_DD.ToString());
        pdfFormFields.SetField("form1[0].P4[0].Text249_MM", PdfDAO.Text249_MM.ToString());
        pdfFormFields.SetField("form1[0].P4[0].Text249_YY", PdfDAO.Text249_YY.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_LIMITATION_DURATION", PdfDAO.SZ_LIMITATION_DURATION.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_QUANIFY_LIMITATION", PdfDAO.SZ_QUANIFY_LIMITATION.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_QUANIFY_LIMITATION_1", PdfDAO.SZ_QUANIFY_LIMITATION_1.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_QUANIFY_LIMITATION_2", PdfDAO.SZ_QUANIFY_LIMITATION_2.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_OFFICE_NAME", PdfDAO.SZ_OFFICE_NAME.ToString());
        pdfFormFields.SetField("SZ_SPECIALITY", PdfDAO.SZ_SPECIALITY.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_READINGDOCTOR", PdfDAO.SZ_READINGDOCTOR.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_PHYSICIAN_SIGN", PdfDAO.SZ_PHYSICIAN_SIGN.ToString());
        pdfFormFields.SetField("SZ_SPECIALITY", PdfDAO.SZ_SPECIALITY.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txtTodayDate_DD", PdfDAO.txtTodayDate_DD.ToString());
        pdfFormFields.SetField("txtTodayDate_MM", PdfDAO.txtTodayDate_MM.ToString());
        pdfFormFields.SetField("txtTodayDate_YY", PdfDAO.txtTodayDate_YY.ToString());


        //PAGE 4 CHECKBOXES
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Yes", PdfDAO.chk_H_3_Yes.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_No", PdfDAO.chk_H_3_No.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_CTScan", PdfDAO.chk_H_3_CTScan.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_EMGNCS", PdfDAO.chk_H_3_EMGNCS.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_MRI", PdfDAO.chk_H_3_MRI.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Labs", PdfDAO.chk_H_3_Labs.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_XRay", PdfDAO.chk_H_3_XRay.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Left_Other", PdfDAO.chk_H_3_Left_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Chiropractor", PdfDAO.chk_H_3_Chiropractor.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Interist", PdfDAO.chk_H_3_Interist.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Occupational", PdfDAO.chk_H_3_Occupational.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Physical", PdfDAO.chk_H_3_Physical.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Specialist", PdfDAO.chk_H_3_Specialist.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Right_Other", PdfDAO.chk_H_3_Right_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_Cane", PdfDAO.chk_H_4_Cane.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_Crutches", PdfDAO.chk_H_4_Crutches.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_Orthotics", PdfDAO.chk_H_4_Orthotics.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_Walker", PdfDAO.chk_H_4_Walker.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_WheelChair", PdfDAO.chk_H_4_WheelChair.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_Other", PdfDAO.chk_H_4_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_WithinWeek", PdfDAO.chk_H_5_WithinWeek.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_1_2_Week", PdfDAO.chk_H_5_1_2_Week.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_3_4_Week", PdfDAO.chk_H_5_3_4_Week.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_5_6_Week", PdfDAO.chk_H_5_5_6_Week.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_7_8_Week", PdfDAO.chk_H_5_7_8_Week.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_Month", PdfDAO.chk_H_5_Month.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_Return", PdfDAO.chk_H_5_Return.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_Yes", PdfDAO.chk_I_1_Yes.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_No", PdfDAO.chk_I_1_No.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_PatientWorkingYes", PdfDAO.chk_I_1_PatientWorkingYes.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_PatientWorkingNo", PdfDAO.chk_I_1_PatientWorkingNo.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_PatientReturnYes", PdfDAO.chk_I_1_PatientReturnYes.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_PatientReturnNo", PdfDAO.chk_I_1_PatientReturnNo.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_a", PdfDAO.chk_I_2_a.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_b", PdfDAO.chk_I_2_b.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_c", PdfDAO.chk_I_2_c.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Lifting", PdfDAO.chk_I_2_C_Lifting.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Bending", PdfDAO.chk_I_2_C_Bending.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Climbing", PdfDAO.chk_I_2_C_Climbing.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_EnvironmentalCond", PdfDAO.chk_I_2_C_EnvironmentalCond.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Kneeling", PdfDAO.chk_I_2_C_Kneeling.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Other", PdfDAO.chk_I_2_C_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Lifting", PdfDAO.chk_I_2_C_Lifting.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_OperatingHeavy", PdfDAO.chk_I_2_C_OperatingHeavy.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_OpMotorVehicle", PdfDAO.chk_I_2_C_OpMotorVehicle.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_PersonalProtectiveEq", PdfDAO.chk_I_2_C_PersonalProtectiveEq.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Sitting", PdfDAO.chk_I_2_C_Sitting.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Standing", PdfDAO.chk_I_2_C_Standing.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_UsePublicTrans", PdfDAO.chk_I_2_C_UsePublicTrans.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_UseOfUpperExtremeities", PdfDAO.chk_I_2_C_UseOfUpperExtremeities.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_1_2_Days", PdfDAO.chk_H_2_1_2_Days.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_3_7_Days", PdfDAO.chk_H_2_3_7_Days.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_8_14_Days", PdfDAO.chk_H_2_8_14_Days.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_15P_Days", PdfDAO.chk_H_2_15P_Days.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_Unknown", PdfDAO.chk_H_2_Unknown.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_NA", PdfDAO.chk_H_2_NA.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Patient", PdfDAO.chk_H_3_Patient.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_PatientsEmployer", PdfDAO.chk_H_3_PatientsEmployer.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_NA", PdfDAO.chk_H_4_NA.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_Provider_1", PdfDAO.chk_H_Provider_1.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_Provider_2", PdfDAO.chk_H_Provider_2.ToString());

        //string sTmp = "PDF Generated";
        //MessageBox.Show(sTmp, "Finished");
        pdfStamper.FormFlattening = true;
        // close the pdf

        pdfStamper.Close();
    }

    private void FillForm(Bill_Sys_C4DAO PdfDAO, string szPdfPath, string outPutFilePath)
    {


        
        

        //string pdfTemplate = @"E:\Work\TestXFA\TemplatePDF\c4_worestrictions.pdf";
        string pdfTemplate = szPdfPath;
        string newFile = outPutFilePath;
        //string newFile = @"E:\Work\TestXFA\C4.0.pdf";



        PdfReader pdfReader = new PdfReader(pdfTemplate);
        PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
        AcroFields pdfFormFields = pdfStamper.AcroFields;



        // set form pdfFormFields

        // The first worksheet and W-4 form

        pdfFormFields.SetField("SZ_PATIENT_LAST_NAME", PdfDAO.LastName.ToString());
        pdfFormFields.SetField("SZ_PATIENT_FIRST_NAME", PdfDAO.FirstName.ToString());
        pdfFormFields.SetField("SZ_MI", PdfDAO.middleInitial.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_SOCIAL_SECURITY_NO", PdfDAO.socialSecNumber.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_HOME_PHONE1", PdfDAO.patientHomePhone1.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_HOME_PHONE2", PdfDAO.patientHomePhone2.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_WCB_NO", PdfDAO.WCBNumber.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_CARRIER_CASE_NO", PdfDAO.carrierCaseNo.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_FULL_ADDRESS", PdfDAO.patientAddress.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_CITY", PdfDAO.patientCity.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_STATE", PdfDAO.patientState.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_ZIP", PdfDAO.patientZip.ToString());
        pdfFormFields.SetField("DT_DATE_OF_ACCIDENT_DD", PdfDAO.accidentDate.ToString());
        pdfFormFields.SetField("DT_DATE_OF_ACCIDENT_MM", PdfDAO.accidentMonth.ToString());
        pdfFormFields.SetField("DT_DATE_OF_ACCIDENT_YY", PdfDAO.accidentYear.ToString());
        pdfFormFields.SetField("form1[0].P1[0].DOB_DD", PdfDAO.DOBDate.ToString());
        pdfFormFields.SetField("form1[0].P1[0].DOB_MM", PdfDAO.DOBMonth.ToString());
        pdfFormFields.SetField("form1[0].P1[0].DOB_YY", PdfDAO.DOBYear.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_CARRIER_CODE", PdfDAO.SZ_CARRIER_CODE.ToString());

        // The form's checkboxes

        pdfFormFields.SetField("form1[0].P1[0].chkMale", PdfDAO.chkMale.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chkFemale", PdfDAO.chkFemale.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chk_SSN", PdfDAO.chk_SSN.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chk_EIN", PdfDAO.chk_EIN.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chkPhysician", PdfDAO.chkPhysician.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chkPodiatrist", PdfDAO.chkPodiatrist.ToString());
        pdfFormFields.SetField("form1[0].P1[0].chkChiropractor", PdfDAO.chkChiropractor.ToString());


        // The rest of the form pdfFormFields

        pdfFormFields.SetField("form1[0].P1[0].SZ_JOB_TITLE", PdfDAO.jobTitle.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_WORK_ACTIVITIES", PdfDAO.workActivity.ToString());//form1[0].P1[0].SZ_WORK_ACTIVITIES_1
        pdfFormFields.SetField("form1[0].P1[0].SZ_WORK_ACTIVITIES_1", PdfDAO.workActivity1.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_PATIENT_ACCOUNT_NO", PdfDAO.patientAccountNo.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_NAME", PdfDAO.employerName.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_PHONE1", PdfDAO.employerPhone1.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_PHONE2", PdfDAO.employerPhone2.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_ADDRESS", PdfDAO.SZ_EMPLOYER_ADDRESS.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_CITY", PdfDAO.SZ_EMPLOYER_CITY.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_STATE", PdfDAO.SZ_EMPLOYER_STATE.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_EMPLOYER_ZIP", PdfDAO.SZ_EMPLOYER_ZIP.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_DOCTOR_NAME", PdfDAO.SZ_DOCTOR_NAME.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_WCB_AUTHORIZATION", PdfDAO.SZ_WCB_AUTHORIZATION.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_WCB_RATING_CODE", PdfDAO.SZ_WCB_RATING_CODE.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_FEDERAL_TAX_ID", PdfDAO.SZ_FEDERAL_TAX_ID.ToString());


        // Second Worksheets pdfFormFields

        // In order to map the fields, I just pass them a sequential

        // number to mark them; once I know which field is which, I

        // can pass the appropriate value

        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_ADDRESS", PdfDAO.SZ_OFFICE_ADDRESS.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_CITY", PdfDAO.SZ_OFFICE_CITY.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_STATE", PdfDAO.SZ_OFFICE_STATE.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_ZIP", PdfDAO.SZ_OFFICE_ZIP.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_GROUP_NAME", PdfDAO.SZ_BILLING_GROUP_NAME.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_ADDRESS", PdfDAO.SZ_BILLING_ADDRESS.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_CITY", PdfDAO.SZ_BILLING_CITY.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_STATE", PdfDAO.SZ_BILLING_STATE.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_ZIP", PdfDAO.SZ_BILLING_ZIP.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_PHONE1", PdfDAO.SZ_OFFICE_PHONE1.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_OFFICE_PHONE2", PdfDAO.SZ_OFFICE_PHONE2.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_PHONE1", PdfDAO.SZ_BILLING_PHONE1.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_BILLING_PHONE2", PdfDAO.SZ_BILLING_PHONE2.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_NPI", PdfDAO.SZ_NPI.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_INSURANCE_NAME", PdfDAO.SZ_INSURANCE_NAME.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_INSURANCE_STREET", PdfDAO.SZ_INSURANCE_STREET.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_INSURANCE_CITY", PdfDAO.SZ_INSURANCE_CITY.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_INSURANCE_STATE", PdfDAO.SZ_INSURANCE_STATE.ToString());
        pdfFormFields.SetField("form1[0].P1[0].SZ_INSURANCE_ZIP", PdfDAO.SZ_INSURANCE_ZIP.ToString());
        //remain

        //
        //done all diagnossis for one
        pdfFormFields.SetField("SZ_DIAGNOSIS_CODE", PdfDAO.SZ_DIAGNOSIS_CODE.ToString());
        //pdfFormFields.SetField("form1[0].P1[0].SZ_DIAGNOSIS_CODE2", PdfDAO.SZ_DIAGNOSIS_CODE.ToString());
        //pdfFormFields.SetField("form1[0].P1[0].SZ_DIAGNOSIS_CODE3", PdfDAO.SZ_DIAGNOSIS_CODE.ToString());
        //pdfFormFields.SetField("SZ_DIAGNOSIS_CODE4", PdfDAO.SZ_DIAGNOSIS_CODE.ToString());
        //pdfFormFields.SetField("form1[0].P1[0].SZ_DIGNOSIS_DESC1", PdfDAO.SZ_DIAGNOSIS_CODE.ToString());
        //pdfFormFields.SetField("form1[0].P1[0].SZ_DIGNOSIS_DESC2", PdfDAO.SZ_DIAGNOSIS_CODE.ToString());
        //pdfFormFields.SetField("form1[0].P1[0].SZ_DIGNOSIS_DESC3", PdfDAO.SZ_DIAGNOSIS_CODE.ToString());
        //pdfFormFields.SetField("form1[0].P1[0].SZ_DIGNOSIS_DESC4", PdfDAO.SZ_DIAGNOSIS_CODE.ToString());
        //
        pdfFormFields.SetField("SZ_BILL_ID", PdfDAO.SZ_BILL_ID.ToString());



        //Page 2

        pdfFormFields.SetField("form1[0].P2[0].FROM_MM_1", PdfDAO.FROM_MM_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_MM_2", PdfDAO.FROM_MM_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_MM_3", PdfDAO.FROM_MM_3.ToString());
        pdfFormFields.SetField("FROM_MM_4", PdfDAO.FROM_MM_4.ToString());
        pdfFormFields.SetField("FROM_MM_5", PdfDAO.FROM_MM_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_MM_6", PdfDAO.FROM_MM_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_DD_1", PdfDAO.FROM_DD_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_DD_2", PdfDAO.FROM_DD_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_DD_3", PdfDAO.FROM_DD_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_DD_4", PdfDAO.FROM_DD_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_DD_5", PdfDAO.FROM_DD_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_DD_6", PdfDAO.FROM_DD_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_YY_1", PdfDAO.FROM_YY_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_YY_2", PdfDAO.FROM_YY_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_YY_3", PdfDAO.FROM_YY_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_YY_4", PdfDAO.FROM_YY_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_YY_5", PdfDAO.FROM_YY_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].FROM_YY_6", PdfDAO.FROM_YY_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_MM_1", PdfDAO.TO_MM_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_MM_2", PdfDAO.TO_MM_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_MM_3", PdfDAO.TO_MM_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_MM_4", PdfDAO.TO_MM_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_MM_5", PdfDAO.TO_MM_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_MM_6", PdfDAO.TO_MM_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_DD_1", PdfDAO.TO_DD_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_DD_2", PdfDAO.TO_DD_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_DD_3", PdfDAO.TO_DD_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_DD_4", PdfDAO.TO_DD_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_DD_5", PdfDAO.TO_DD_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_DD_6", PdfDAO.TO_DD_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_YY_1", PdfDAO.TO_YY_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_YY_2", PdfDAO.TO_YY_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_YY_3", PdfDAO.TO_YY_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_YY_4", PdfDAO.TO_YY_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_YY_5", PdfDAO.TO_YY_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TO_YY_6", PdfDAO.TO_YY_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].POS_1", PdfDAO.POS_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].POS_2", PdfDAO.POS_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].POS_3", PdfDAO.POS_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].POS_4", PdfDAO.POS_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].POS_5", PdfDAO.POS_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].POS_6", PdfDAO.POS_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].CPT_1", PdfDAO.CPT_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].CPT_2", PdfDAO.CPT_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].CPT_3", PdfDAO.CPT_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].CPT_4", PdfDAO.CPT_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].CPT_5", PdfDAO.CPT_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].CPT_6", PdfDAO.CPT_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].MODIFIER_1", PdfDAO.MODIFIER_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].MODIFIER_2", PdfDAO.MODIFIER_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].MODIFIER_3", PdfDAO.MODIFIER_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].MODIFIER_4", PdfDAO.MODIFIER_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].MODIFIER_5", PdfDAO.MODIFIER_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].MODIFIER_6", PdfDAO.MODIFIER_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_MODIFIER_B1", PdfDAO.SZ_MODIFIER_B1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_MODIFIER_B2", PdfDAO.SZ_MODIFIER_B2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_MODIFIER_B3", PdfDAO.SZ_MODIFIER_B3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_MODIFIER_B4", PdfDAO.SZ_MODIFIER_B4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_MODIFIER_B5", PdfDAO.SZ_MODIFIER_B5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_MODIFIER_B6", PdfDAO.SZ_MODIFIER_B6.ToString());
        pdfFormFields.SetField("DC_1", PdfDAO.DC_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].DC_2", PdfDAO.DC_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].DC_3", PdfDAO.DC_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].DC_4", PdfDAO.DC_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].DC_5", PdfDAO.DC_5.ToString());
        pdfFormFields.SetField("DC_6", PdfDAO.DC_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].CHARGE_1", PdfDAO.CHARGE_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].CHARGE_2", PdfDAO.CHARGE_2.ToString());
        pdfFormFields.SetField("CHARGE_3", PdfDAO.CHARGE_3.ToString());
        pdfFormFields.SetField("CHARGE_4", PdfDAO.CHARGE_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].CHARGE_5", PdfDAO.CHARGE_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].CHARGE_6", PdfDAO.CHARGE_6.ToString());
        pdfFormFields.SetField("UNIT_1", PdfDAO.UNIT_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].UNIT_2", PdfDAO.UNIT_2.ToString());
        pdfFormFields.SetField("UNIT_3", PdfDAO.UNIT_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].UNIT_4", PdfDAO.UNIT_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].UNIT_5", PdfDAO.UNIT_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].UNIT_6", PdfDAO.UNIT_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].COB_1", PdfDAO.COB_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].COB_2", PdfDAO.COB_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].COB_3", PdfDAO.COB_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].COB_4", PdfDAO.COB_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].COB_5", PdfDAO.COB_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].COB_6", PdfDAO.COB_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].ZIP_1", PdfDAO.ZIP_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].ZIP_2", PdfDAO.ZIP_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].ZIP_3", PdfDAO.ZIP_3.ToString());
        pdfFormFields.SetField("form1[0].P2[0].ZIP_4", PdfDAO.ZIP_4.ToString());
        pdfFormFields.SetField("form1[0].P2[0].ZIP_5", PdfDAO.ZIP_5.ToString());
        pdfFormFields.SetField("form1[0].P2[0].ZIP_6", PdfDAO.ZIP_6.ToString());
        pdfFormFields.SetField("form1[0].P2[0].TOTAL_BILL_AMOUNT", PdfDAO.TOTAL_BILL_AMOUNT.ToString());
        pdfFormFields.SetField("TOTAL_PAID_AMOUNT", PdfDAO.TOTAL_PAID_AMOUNT.ToString());
        pdfFormFields.SetField("TOTAL_BAL_AMOUNT", PdfDAO.TOTAL_BAL_AMOUNT.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_INJURY_ILLNESS_DETAIL", PdfDAO.SZ_INJURY_ILLNESS_DETAIL.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_INJURY_ILLNESS_DETAIL_1", PdfDAO.SZ_INJURY_ILLNESS_DETAIL_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_INJURY_ILLNESS_DETAIL_2", PdfDAO.SZ_INJURY_ILLNESS_DETAIL_2.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_INJURY_LEARN_SOURCE_DESCRIPTION", PdfDAO.SZ_INJURY_LEARN_SOURCE_DESCRIPTION.ToString());
        pdfFormFields.SetField("form1[0].P2[0].SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION", PdfDAO.SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION.ToString());
        pdfFormFields.SetField("form1[0].P2[0].DT_PREVIOUSLY_TREATED_DATE", PdfDAO.DT_PREVIOUSLY_TREATED_DATE.ToString());
        pdfFormFields.SetField("form1[0].P2[0].DT_DATE_OF_EXAMINATION", PdfDAO.DT_DATE_OF_EXAMINATION.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Numbness", PdfDAO.txt_F_Numbness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Pain", PdfDAO.txt_F_Pain.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Stiffness", PdfDAO.txt_F_Stiffness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Swelling", PdfDAO.txt_F_Swelling.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Weakness", PdfDAO.txt_F_Weakness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_Other", PdfDAO.txt_F_Other.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Abrasion", PdfDAO.txt_F_3_Abrasion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Amputation", PdfDAO.txt_F_3_Amputation.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Avulsion", PdfDAO.txt_F_3_Avulsion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Bite", PdfDAO.txt_F_3_Bite.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Burn", PdfDAO.txt_F_3_Burn.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Contusion", PdfDAO.txt_F_3_Contusion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_CurshInjury", PdfDAO.txt_F_3_CurshInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Dermatitis", PdfDAO.txt_F_3_Dermatitis.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Dislocation", PdfDAO.txt_F_3_Dislocation.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Fracture", PdfDAO.txt_F_3_Fracture.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_HearingLoss", PdfDAO.txt_F_3_HearingLoss.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Hernia", PdfDAO.txt_F_3_Hernia.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Other", PdfDAO.txt_F_3_Other.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Other_1", PdfDAO.txt_F_3_Other_1.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_InfectiousDisease", PdfDAO.txt_F_3_InfectiousDisease.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Inhalation_Exposure", PdfDAO.txt_F_3_Inhalation_Exposure.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Laceration", PdfDAO.txt_F_3_Laceration.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_NeedleStick", PdfDAO.txt_F_3_NeedleStick.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Poisoning", PdfDAO.txt_F_3_Poisoning.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Psychological", PdfDAO.txt_F_3_Psychological.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_PuntureWound", PdfDAO.txt_F_3_PuntureWound.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_RepetitiveStrainInjury", PdfDAO.txt_F_3_RepetitiveStrainInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_SpinalCordInjury", PdfDAO.txt_F_3_SpinalCordInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Sprain", PdfDAO.txt_F_3_Sprain.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_Torn", PdfDAO.txt_F_3_Torn.ToString());
        pdfFormFields.SetField("form1[0].P2[0].txt_F_3_VisionLoss", PdfDAO.txt_F_3_VisionLoss.ToString());

        //PAGE 2 CHECKBOXES
        pdfFormFields.SetField("form1[0].P2[0].chkService", PdfDAO.chkService.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_Patient", PdfDAO.chk_H_Patient.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_MedicalRecords", PdfDAO.chk_H_MedicalRecords.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_Other", PdfDAO.chk_H_Other.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_Yes", PdfDAO.chk_H_Yes.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_No", PdfDAO.chk_H_No.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_4_Yes", PdfDAO.chk_H_4_Yes.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_H_4_No", PdfDAO.chk_H_4_No.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Numbness", PdfDAO.chk_F_Numbness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Pain", PdfDAO.chk_F_Pain.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Stiffness", PdfDAO.chk_F_Stiffness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Swelling", PdfDAO.chk_F_Swelling.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Weakness", PdfDAO.chk_F_Weakness.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_Other", PdfDAO.chk_F_Other.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Abrasion", PdfDAO.chk_F_3_Abrasion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Amputation", PdfDAO.chk_F_3_Amputation.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Avulsion", PdfDAO.chk_F_3_Avulsion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Bite", PdfDAO.chk_F_3_Bite.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Burn", PdfDAO.chk_F_3_Burn.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Contusion", PdfDAO.chk_F_3_Contusion.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_CurshInjury", PdfDAO.chk_F_3_CurshInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Dermatitis", PdfDAO.chk_F_3_Dermatitis.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Dislocation", PdfDAO.chk_F_3_Dislocation.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Fracture", PdfDAO.chk_F_3_Fracture.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_HearingLoss", PdfDAO.chk_F_3_HearingLoss.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Hernia", PdfDAO.chk_F_3_Hernia.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Other", PdfDAO.chk_F_3_Other.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_InfectiousDisease", PdfDAO.chk_F_3_InfectiousDisease.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Inhalation_Exposure", PdfDAO.chk_F_3_Inhalation_Exposure.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Laceration", PdfDAO.chk_F_3_Laceration.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_NeedleStick", PdfDAO.chk_F_3_NeedleStick.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Poisoning", PdfDAO.chk_F_3_Poisoning.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Psychological", PdfDAO.chk_F_3_Psychological.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_PuntureWound", PdfDAO.chk_F_3_PuntureWound.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_RepetitiveStrainInjury", PdfDAO.chk_F_3_RepetitiveStrainInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_SpinalCordInjury", PdfDAO.chk_F_3_SpinalCordInjury.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Sprain", PdfDAO.chk_F_3_Sprain.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_Torn", PdfDAO.chk_F_3_Torn.ToString());
        pdfFormFields.SetField("form1[0].P2[0].chk_F_3_VisionLoss", PdfDAO.chk_F_3_VisionLoss.ToString());

        //PAGE 3
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Bruising", PdfDAO.txt_F_4_Bruising.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Burns", PdfDAO.txt_F_4_Burns.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Crepitation", PdfDAO.txt_F_4_Crepitation.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Deformity", PdfDAO.txt_F_4_Deformity.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Edema", PdfDAO.txt_F_4_Edema.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Hematoma", PdfDAO.txt_F_4_Hematoma.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_JointEffusion", PdfDAO.txt_F_4_JointEffusion.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Laceration", PdfDAO.txt_F_4_Laceration.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Pain", PdfDAO.txt_F_4_Pain.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Scar", PdfDAO.txt_F_4_Scar.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_OtherFindings", PdfDAO.txt_F_4_OtherFindings.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_ActiveROM", PdfDAO.txt_F_4_ActiveROM.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_PassiveROM", PdfDAO.txt_F_4_PassiveROM.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Gait", PdfDAO.txt_F_4_Gait.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Palpable", PdfDAO.txt_F_4_Palpable.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Reflexes", PdfDAO.txt_F_4_Reflexes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Sensation", PdfDAO.txt_F_4_Sensation.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Strength", PdfDAO.txt_F_4_Strength.ToString());
        pdfFormFields.SetField("form1[0].P3[0].txt_F_4_Wasting", PdfDAO.txt_F_4_Wasting.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_DIAGNOSTIC_TEST", PdfDAO.SZ_DIAGNOSTIC_TEST.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_DIAGNOSTIC_TEST_1", PdfDAO.SZ_DIAGNOSTIC_TEST_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_TREATMENT", PdfDAO.SZ_TREATMENT.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_TREATMENT_1", PdfDAO.SZ_TREATMENT_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROGNOSIS_RECOVERY", PdfDAO.SZ_PROGNOSIS_RECOVERY.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROGNOSIS_RECOVERY_1", PdfDAO.SZ_PROGNOSIS_RECOVERY_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROGNOSIS_RECOVERY_2", PdfDAO.SZ_PROGNOSIS_RECOVERY_2.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION", PdfDAO.SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1", PdfDAO.SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].FT_TEMPORARY_IMPAIRMENT_PERCENTAGE", PdfDAO.FT_TEMPORARY_IMPAIRMENT_PERCENTAGE.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_TEST_RESULTS", PdfDAO.SZ_TEST_RESULTS.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_TEST_RESULTS_1", PdfDAO.SZ_TEST_RESULTS_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROPOSED_TREATEMENT", PdfDAO.SZ_PROPOSED_TREATEMENT.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROPOSED_TREATEMENT_1", PdfDAO.SZ_PROPOSED_TREATEMENT_1.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_PROPOSED_TREATEMENT_3", PdfDAO.SZ_PROPOSED_TREATEMENT_3.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_MEDICATIONS_PRESCRIBED", PdfDAO.SZ_MEDICATIONS_PRESCRIBED.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_MEDICATIONS_ADVISED", PdfDAO.SZ_MEDICATIONS_ADVISED.ToString());
        pdfFormFields.SetField("form1[0].P3[0].SZ_MEDICATION_RESTRICTIONS_DESCRIPTION", PdfDAO.SZ_MEDICATION_RESTRICTIONS_DESCRIPTION.ToString());
        pdfFormFields.SetField("form1[0].P3[0].Text226", PdfDAO.Text226.ToString());

        //PAGE 3 CHECKBOXES
        pdfFormFields.SetField("form1[0].P3[0].chk_F_7_None", PdfDAO.chk_F_7_None.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Bruising", PdfDAO.chk_F_4_Bruising.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Burns", PdfDAO.chk_F_4_Burns.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Crepitation", PdfDAO.chk_F_4_Crepitation.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Deformity", PdfDAO.chk_F_4_Deformity.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Edema", PdfDAO.chk_F_4_Edema.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Hematoma", PdfDAO.chk_F_4_Hematoma.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_JointEffusion", PdfDAO.chk_F_4_JointEffusion.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Laceration", PdfDAO.chk_F_4_Laceration.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Pain", PdfDAO.chk_F_4_Pain.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Scar", PdfDAO.chk_F_4_Scar.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_OtherFindings", PdfDAO.chk_F_4_OtherFindings.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Neuromuscular", PdfDAO.chk_F_4_Neuromuscular.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Abnormal", PdfDAO.chk_F_4_Abnormal.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_ActiveROM", PdfDAO.chk_F_4_ActiveROM.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_PassiveROM", PdfDAO.chk_F_4_PassiveROM.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Gait", PdfDAO.chk_F_4_Gait.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Palpable", PdfDAO.chk_F_4_Palpable.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Reflexes", PdfDAO.chk_F_4_Reflexes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Sensation", PdfDAO.chk_F_4_Sensation.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Strength", PdfDAO.chk_F_4_Strength.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_4_Wasting", PdfDAO.chk_F_4_Wasting.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_8_Yes", PdfDAO.chk_F_8_Yes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_F_8_No", PdfDAO.chk_F_8_No.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_1_Yes", PdfDAO.chk_G_1_Yes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_1_No", PdfDAO.chk_G_1_No.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_2_Yes", PdfDAO.chk_G_2_Yes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_2_No", PdfDAO.chk_G_2_No.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_3_Yes", PdfDAO.chk_G_3_Yes.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_3_No", PdfDAO.chk_G_3_No.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_G_3_NA", PdfDAO.chk_G_3_NA.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_H_2_None", PdfDAO.chk_H_2_None.ToString());
        pdfFormFields.SetField("form1[0].P3[0].chk_H_2_Other", PdfDAO.chk_H_2_Other.ToString());


        //PAGE 4
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_MRI", PdfDAO.txt_H_3_MRI.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_Labs", PdfDAO.txt_H_3_Labs.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_XRay", PdfDAO.txt_H_3_XRay.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_Left_Other", PdfDAO.txt_H_3_Left_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_Specialist", PdfDAO.txt_H_3_Specialist.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_3_Right_Other", PdfDAO.txt_H_3_Right_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_4_Other", PdfDAO.txt_H_4_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txt_H_5_Month", PdfDAO.txt_H_5_Month.ToString());
        pdfFormFields.SetField("form1[0].P4[0].DT_PATIENT_MISSED_WORK_DATE_DD", PdfDAO.DT_PATIENT_MISSED_WORK_DATE_DD.ToString());
        pdfFormFields.SetField("form1[0].P4[0].DT_PATIENT_MISSED_WORK_DATE_MM", PdfDAO.DT_PATIENT_MISSED_WORK_DATE_MM.ToString());
        pdfFormFields.SetField("form1[0].P4[0].DT_PATIENT_MISSED_WORK_DATE_YY", PdfDAO.DT_PATIENT_MISSED_WORK_DATE_YY.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_PATIENT_RETURN_WORK_DESCRIPTION", PdfDAO.SZ_PATIENT_RETURN_WORK_DESCRIPTION.ToString());
        pdfFormFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_DD", PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_DD.ToString());
        pdfFormFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_MM", PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_MM.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_PATIENT_RETURN_WORK_LIMITATION_YY", PdfDAO.SZ_PATIENT_RETURN_WORK_LIMITATION_YY.ToString());
        pdfFormFields.SetField("form1[0].P4[0].Text249_DD", PdfDAO.Text249_DD.ToString());
        pdfFormFields.SetField("form1[0].P4[0].Text249_MM", PdfDAO.Text249_MM.ToString());
        pdfFormFields.SetField("form1[0].P4[0].Text249_YY", PdfDAO.Text249_YY.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_LIMITATION_DURATION", PdfDAO.SZ_LIMITATION_DURATION.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_QUANIFY_LIMITATION", PdfDAO.SZ_QUANIFY_LIMITATION.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_QUANIFY_LIMITATION_1", PdfDAO.SZ_QUANIFY_LIMITATION_1.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_QUANIFY_LIMITATION_2", PdfDAO.SZ_QUANIFY_LIMITATION_2.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_OFFICE_NAME", PdfDAO.SZ_OFFICE_NAME.ToString());
        pdfFormFields.SetField("SZ_SPECIALITY", PdfDAO.SZ_SPECIALITY.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_READINGDOCTOR", PdfDAO.SZ_READINGDOCTOR.ToString());
        pdfFormFields.SetField("form1[0].P4[0].SZ_PHYSICIAN_SIGN", PdfDAO.SZ_PHYSICIAN_SIGN.ToString());
        pdfFormFields.SetField("SZ_SPECIALITY", PdfDAO.SZ_SPECIALITY.ToString());
        pdfFormFields.SetField("form1[0].P4[0].txtTodayDate_DD", PdfDAO.txtTodayDate_DD.ToString());
        pdfFormFields.SetField("txtTodayDate_MM", PdfDAO.txtTodayDate_MM.ToString());
        pdfFormFields.SetField("txtTodayDate_YY", PdfDAO.txtTodayDate_YY.ToString());


        //PAGE 4 CHECKBOXES
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Yes", PdfDAO.chk_H_3_Yes.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_No", PdfDAO.chk_H_3_No.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_CTScan", PdfDAO.chk_H_3_CTScan.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_EMGNCS", PdfDAO.chk_H_3_EMGNCS.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_MRI", PdfDAO.chk_H_3_MRI.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Labs", PdfDAO.chk_H_3_Labs.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_XRay", PdfDAO.chk_H_3_XRay.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Left_Other", PdfDAO.chk_H_3_Left_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Chiropractor", PdfDAO.chk_H_3_Chiropractor.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Interist", PdfDAO.chk_H_3_Interist.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Occupational", PdfDAO.chk_H_3_Occupational.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Physical", PdfDAO.chk_H_3_Physical.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Specialist", PdfDAO.chk_H_3_Specialist.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Right_Other", PdfDAO.chk_H_3_Right_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_Cane", PdfDAO.chk_H_4_Cane.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_Crutches", PdfDAO.chk_H_4_Crutches.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_Orthotics", PdfDAO.chk_H_4_Orthotics.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_Walker", PdfDAO.chk_H_4_Walker.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_WheelChair", PdfDAO.chk_H_4_WheelChair.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_Other", PdfDAO.chk_H_4_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_WithinWeek", PdfDAO.chk_H_5_WithinWeek.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_1_2_Week", PdfDAO.chk_H_5_1_2_Week.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_3_4_Week", PdfDAO.chk_H_5_3_4_Week.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_5_6_Week", PdfDAO.chk_H_5_5_6_Week.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_7_8_Week", PdfDAO.chk_H_5_7_8_Week.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_Month", PdfDAO.chk_H_5_Month.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_5_Return", PdfDAO.chk_H_5_Return.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_Yes", PdfDAO.chk_I_1_Yes.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_No", PdfDAO.chk_I_1_No.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_PatientWorkingYes", PdfDAO.chk_I_1_PatientWorkingYes.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_PatientWorkingNo", PdfDAO.chk_I_1_PatientWorkingNo.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_PatientReturnYes", PdfDAO.chk_I_1_PatientReturnYes.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_1_PatientReturnNo", PdfDAO.chk_I_1_PatientReturnNo.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_a", PdfDAO.chk_I_2_a.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_b", PdfDAO.chk_I_2_b.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_c", PdfDAO.chk_I_2_c.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Lifting", PdfDAO.chk_I_2_C_Lifting.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Bending", PdfDAO.chk_I_2_C_Bending.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Climbing", PdfDAO.chk_I_2_C_Climbing.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_EnvironmentalCond", PdfDAO.chk_I_2_C_EnvironmentalCond.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Kneeling", PdfDAO.chk_I_2_C_Kneeling.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Other", PdfDAO.chk_I_2_C_Other.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Lifting", PdfDAO.chk_I_2_C_Lifting.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_OperatingHeavy", PdfDAO.chk_I_2_C_OperatingHeavy.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_OpMotorVehicle", PdfDAO.chk_I_2_C_OpMotorVehicle.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_PersonalProtectiveEq", PdfDAO.chk_I_2_C_PersonalProtectiveEq.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Sitting", PdfDAO.chk_I_2_C_Sitting.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_Standing", PdfDAO.chk_I_2_C_Standing.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_UsePublicTrans", PdfDAO.chk_I_2_C_UsePublicTrans.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_I_2_C_UseOfUpperExtremeities", PdfDAO.chk_I_2_C_UseOfUpperExtremeities.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_1_2_Days", PdfDAO.chk_H_2_1_2_Days.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_3_7_Days", PdfDAO.chk_H_2_3_7_Days.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_8_14_Days", PdfDAO.chk_H_2_8_14_Days.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_15P_Days", PdfDAO.chk_H_2_15P_Days.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_Unknown", PdfDAO.chk_H_2_Unknown.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_2_NA", PdfDAO.chk_H_2_NA.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_Patient", PdfDAO.chk_H_3_Patient.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_3_PatientsEmployer", PdfDAO.chk_H_3_PatientsEmployer.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_4_NA", PdfDAO.chk_H_4_NA.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_Provider_1", PdfDAO.chk_H_Provider_1.ToString());
        pdfFormFields.SetField("form1[0].P4[0].chk_H_Provider_2", PdfDAO.chk_H_Provider_2.ToString());


        //pdfFormFields.SetField("SZ_BILL_ID", "sa2135");
        //pdfFormFields.SetField("SZ_BILL_ID", "sa2135");
        //pdfFormFields.SetField("SZ_BILL_ID", "sa2135");
        //pdfFormFields.SetField("SZ_BILL_ID", "sa2135");
        //pdfFormFields.SetField("SZ_BILL_ID", "sa2135");
        //pdfFormFields.SetField("SZ_BILL_ID", "sa2135");
        //pdfFormFields.SetField("SZ_BILL_ID", "sa2135");







        // report by reading values from completed PDF

       // string sTmp = "PDF Generated";
       // MessageBox.Show(sTmp, "Finished");



        // flatten the form to remove editting options, set it to false

        // to leave the form open to subsequent manual edits

        pdfStamper.FormFlattening = true;



        // close the pdf

        pdfStamper.Close();

    } 

}
