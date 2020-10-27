using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.SqlServer.Management.Common;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public class GenerateItextPdf
{
    private string strConn;

    private SqlConnection conn;

    private SqlCommand comm;

    private SqlDataReader dr;

    public GenerateItextPdf()
    {
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public object GeneratC40LessThan5(string CompnayId, string CaseId, string BillId, string CompanyName)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C4"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create), '4');
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value = this.GetValue(BillId);
            if (value.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString() != "")
            {
                float[] fieldPositions = acroFields.GetFieldPositions("SZ_OFFICE_NAME");
                if (fieldPositions == null || fieldPositions.Length <= 0)
                {
                    throw new ApplicationException("Error locating field");
                }
                float absoluteX = 139.5f;
                float num = 93.75f;
                float num2 = 370.5f;
                float num3 = 77.25f;
                Image instance = Image.GetInstance(value.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString());
                instance.ScaleToFit(num - 25f, num3 - 25f);
                instance.SetAbsolutePosition(absoluteX, num2 - 279f);
                PdfContentByte overContent = pdfStamper.GetOverContent(4);
                overContent.AddImage(instance);
            }
            else
            {
                acroFields.SetField("SZ_OFFICE_NAME", value.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString());
            }
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_MI", value.Tables[0].Rows[0]["SZ_MI"].ToString());
            acroFields.SetField("SZ_SOCIAL_SECURITY_NO", value.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_HOME_PHONE1", value.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE1"].ToString());
            acroFields.SetField("SZ_PATIENT_HOME_PHONE2", value.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE2"].ToString());
            acroFields.SetField("SZ_WCB_NO", value.Tables[0].Rows[0]["SZ_WCB_NO"].ToString());
            acroFields.SetField("SZ_CARRIER_CASE_NO", value.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_FULL_ADDRESS", value.Tables[0].Rows[0]["SZ_PATIENT_FULL_ADDRESS"].ToString());
            acroFields.SetField("SZ_PATIENT_CITY", value.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
            acroFields.SetField("SZ_PATIENT_STATE", value.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
            acroFields.SetField("SZ_PATIENT_ZIP", value.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());

            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_DD", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_MM", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_YY", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString());
            }
            acroFields.SetField("DOB_DD", value.Tables[0].Rows[0]["DOB_DD"].ToString());
            acroFields.SetField("DOB_MM", value.Tables[0].Rows[0]["DOB_MM"].ToString());
            acroFields.SetField("DOB_YY", value.Tables[0].Rows[0]["DOB_YY"].ToString());
            acroFields.SetField("SZ_JOB_TITLE", value.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString());
            acroFields.SetField("SZ_WORK_ACTIVITIES", value.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES1"].ToString());
            acroFields.SetField("SZ_WORK_ACTIVITIES_1", value.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES2"].ToString());
            acroFields.SetField("SZ_EMPLOYER_NAME", value.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString());
            acroFields.SetField("SZ_EMPLOYER_PHONE1", value.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE1"].ToString());
            acroFields.SetField("SZ_EMPLOYER_PHONE2", value.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE2"].ToString());
            acroFields.SetField("SZ_EMPLOYER_ADDRESS", value.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString());
            acroFields.SetField("SZ_EMPLOYER_CITY", value.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString());
            acroFields.SetField("SZ_EMPLOYER_STATE", value.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString());
            acroFields.SetField("SZ_EMPLOYER_ZIP", value.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString());
            acroFields.SetField("SZ_DOCTOR_NAME", value.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
            acroFields.SetField("SZ_WCB_AUTHORIZATION", value.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString());
            acroFields.SetField("SZ_WCB_RATING_CODE", value.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString());
            acroFields.SetField("SZ_OFFICE_ADDRESS", value.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
            acroFields.SetField("SZ_OFFICE_CITY", value.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString());
            acroFields.SetField("SZ_OFFICE_STATE", value.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString());
            acroFields.SetField("SZ_OFFICE_ZIP", value.Tables[0].Rows[0]["SZ_OFFICE_ZIP"].ToString());
            acroFields.SetField("SZ_BILLING_GROUP_NAME", value.Tables[0].Rows[0]["SZ_BILLING_GROUP_NAME"].ToString());
            acroFields.SetField("SZ_BILLING_ADDRESS", value.Tables[0].Rows[0]["SZ_BILLING_ADDRESS"].ToString());
            acroFields.SetField("SZ_BILLING_CITY", value.Tables[0].Rows[0]["SZ_BILLING_CITY"].ToString());
            acroFields.SetField("SZ_BILLING_STATE", value.Tables[0].Rows[0]["SZ_BILLING_STATE"].ToString());
            acroFields.SetField("SZ_BILLING_ZIP", value.Tables[0].Rows[0]["SZ_BILLING_ZIP"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE1", value.Tables[0].Rows[0]["SZ_OFFICE_PHONE1"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE2", value.Tables[0].Rows[0]["SZ_OFFICE_PHONE2"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE1", value.Tables[0].Rows[0]["SZ_BILLING_PHONE1"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE2", value.Tables[0].Rows[0]["SZ_BILLING_PHONE2"].ToString());
            acroFields.SetField("SZ_NPI", value.Tables[0].Rows[0]["SZ_NPI"].ToString());
            acroFields.SetField("SZ_FEDERAL_TAX_ID", value.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
            acroFields.SetField("SZ_INSURANCE_NAME", value.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
            acroFields.SetField("SZ_INSURANCE_STREET", value.Tables[0].Rows[0]["SZ_INSURANCE_STREET"].ToString());
            acroFields.SetField("SZ_INSURANCE_CITY", value.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString());
            acroFields.SetField("SZ_INSURANCE_STATE", value.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString());
            acroFields.SetField("SZ_INSURANCE_ZIP", value.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString());
            acroFields.SetField("SZ_DIGNOSIS", value.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString());
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_MI", value.Tables[0].Rows[0]["SZ_MI"].ToString());
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_DD", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_MM", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_YY", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString());
            }
            acroFields.SetField("SZ_DIAGNOSIS_CODE", value.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString());
            acroFields.SetField("txtTodayDate_DD", value.Tables[0].Rows[0]["txtTodayDate_DD"].ToString());
            acroFields.SetField("txtTodayDate_MM", value.Tables[0].Rows[0]["txtTodayDate_MM"].ToString());
            acroFields.SetField("txtTodayDate_YY", value.Tables[0].Rows[0]["txtTodayDate_YY"].ToString());
            acroFields.SetField("SZ_PATIENT_ACCOUNT_NO", value.Tables[0].Rows[0]["SZ_PATIENT_ACCOUNT_NO"].ToString());
            DataSet services = this.GetServices(BillId);
            for (int i = 0; i < services.Tables[0].Rows.Count; i++)
            {
                acroFields.SetField("FROM_MM_" + (i + 1).ToString(), services.Tables[0].Rows[i]["MONTH"].ToString());
                acroFields.SetField("FROM_DD_" + (i + 1).ToString(), services.Tables[0].Rows[i]["DAY"].ToString());
                acroFields.SetField("FROM_YY_" + (i + 1).ToString(), services.Tables[0].Rows[i]["YEAR"].ToString());
                acroFields.SetField("TO_MM_" + (i + 1).ToString(), services.Tables[0].Rows[i]["TO_MONTH"].ToString());
                acroFields.SetField("TO_DD_" + (i + 1).ToString(), services.Tables[0].Rows[i]["TO_DAY"].ToString());
                acroFields.SetField("TO_YY_" + (i + 1).ToString(), services.Tables[0].Rows[i]["TO_YEAR"].ToString());
                acroFields.SetField("CPT_" + (i + 1).ToString(), services.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
                acroFields.SetField("MODIFIER_" + (i + 1).ToString(), services.Tables[0].Rows[i]["SZ_MODIFIER"].ToString());
                acroFields.SetField("CHARGE_" + (i + 1).ToString(), services.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                acroFields.SetField("UNIT_" + (i + 1).ToString(), services.Tables[0].Rows[i]["I_UNIT"].ToString());
                acroFields.SetField("ZIP_" + (i + 1).ToString(), services.Tables[0].Rows[i]["ZIP_CODE"].ToString());
                acroFields.SetField("POS_" + (i + 1).ToString(), services.Tables[0].Rows[i]["PLACE_OF_SERVICE"].ToString());
                acroFields.SetField("DC_" + (i + 1).ToString(), value.Tables[0].Rows[0]["DC_1"].ToString());
            }
            acroFields.SetField("TOTAL_BILL_AMOUNT", services.Tables[0].Rows[0]["BILL_AMOUNT"].ToString());
            acroFields.SetField("TOTAL_PAID_AMOUNT", services.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
            acroFields.SetField("TOTAL_BAL_AMOUNT", services.Tables[0].Rows[0]["BALANCE"].ToString());
            if (value.Tables[0].Rows[0]["BT_PATIENT_MISSED_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_MISSED_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_No", "1");
            }
            if (value.Tables[0].Rows[0]["I_GENDER"].ToString() == "0")
            {
                acroFields.SetField("chkMale", "1");
            }
            if (value.Tables[0].Rows[0]["I_GENDER"].ToString() == "1")
            {
                acroFields.SetField("chkFemale", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ANOTHER_HEALTH_PROVIDER"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ANOTHER_HEALTH_PROVIDER"].ToString() == "1")
            {
                acroFields.SetField("chk_H_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_PREVIOUSLY_TREATED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_4_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_PREVIOUSLY_TREATED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_4_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_AFFECT_TREATMENT_PROGNOSIS"].ToString() == "0")
            {
                acroFields.SetField("chk_F_8_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_AFFECT_TREATMENT_PROGNOSIS"].ToString() == "1")
            {
                acroFields.SetField("chk_F_8_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "0")
            {
                acroFields.SetField("chk_G_1_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "1")
            {
                acroFields.SetField("chk_G_1_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "0")
            {
                acroFields.SetField("chk_G_2_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "1")
            {
                acroFields.SetField("chk_G_2_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "0")
            {
                acroFields.SetField("chk_G_3_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "1")
            {
                acroFields.SetField("chk_G_3_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "2")
            {
                acroFields.SetField("chk_G_3_NA", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PPO"].ToString() == "0")
            {
                acroFields.SetField("chkService", "1");
            }
            if (value.Tables[0].Rows[0]["BT_MEDICATION_RESTRICTIONS"].ToString() == "False")
            {
                acroFields.SetField("chk_H_2_None", "1");
            }
            if (value.Tables[0].Rows[0]["BT_MEDICATION_RESTRICTIONS"].ToString() == "True")
            {
                acroFields.SetField("chk_H_2_Other", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_3_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_3_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ADHERE_NEWYORK_TREATMENT"].ToString() == "0")
            {
                acroFields.SetField("chk_H_8_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ADHERE_NEWYORK_TREATMENT"].ToString() == "1")
            {
                acroFields.SetField("chk_H_8_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_CUR_WORKING"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_PatientWorkingYes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_CUR_WORKING"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_PatientWorkingNo", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "0")
            {
                acroFields.SetField("chkPhysician", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "1")
            {
                acroFields.SetField("chkPodiatrist", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "2")
            {
                acroFields.SetField("chkChiropractor", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "0")
            {
                acroFields.SetField("chk_SSN", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "1")
            {
                acroFields.SetField("chk_EIN", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Patient", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "1")
            {
                acroFields.SetField("chk_H_MedicalRecords", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "2")
            {
                acroFields.SetField("chk_H_Other", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "0")
            {
                acroFields.SetField("chk_H_5_WithinWeek", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "1")
            {
                acroFields.SetField("chk_H_5_1_2_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "2")
            {
                acroFields.SetField("chk_H_5_3_4_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "3")
            {
                acroFields.SetField("chk_H_5_5_6_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "4")
            {
                acroFields.SetField("chk_H_5_7_8_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "5")
            {
                acroFields.SetField("chk_H_5_Month", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "6")
            {
                acroFields.SetField("chk_H_5_Return", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_WORK_TYPE"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_PatientReturnYes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_WORK_TYPE"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_PatientReturnNo", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_H_2_1_2_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_H_2_3_7_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_H_2_8_14_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_H_2_15P_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_H_2_Unknown", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "5")
            {
                acroFields.SetField("chk_H_2_NA", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_H_2_1_2_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_H_2_3_7_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_H_2_8_14_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "0")
            {
                acroFields.SetField("chk_H_3_Patient", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "1")
            {
                acroFields.SetField("chk_H_3_PatientsEmployer", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "2")
            {
                acroFields.SetField("chk_H_4_NA", "1");
            }
            if (value.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Provider_1", "1");
            }
            if (value.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_Provider_2", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_I_2_a", "1");
                acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION"].ToString());
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_I_2_b", "1");
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_DD"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_DD", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_DD"].ToString());
                }
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_MM"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_MM", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_MM"].ToString());
                }
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_YY"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_YY", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_YY"].ToString());
                }
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "2")
            {
                acroFields.SetField("chk_I_2_c", "1");
                if (value.Tables[0].Rows[0]["Text249_DD"].ToString() != "0")
                {
                    acroFields.SetField("Text249_DD", value.Tables[0].Rows[0]["Text249_DD"].ToString());
                }
                if (value.Tables[0].Rows[0]["Text249_MM"].ToString() != "0")
                {
                    acroFields.SetField("Text249_MM", value.Tables[0].Rows[0]["Text249_MM"].ToString());
                }
                if (value.Tables[0].Rows[0]["Text249_YY"].ToString() != "0")
                {
                    acroFields.SetField("Text249_YY", value.Tables[0].Rows[0]["Text249_YY"].ToString());
                }
            }
            DataSet dataSet = this.PATIENT_COMPLAINTS(BillId);
            for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
            {
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Numbness/Tingling")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Numbness", "1");
                    }
                    acroFields.SetField("txt_F_Numbness", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Swelling")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Swelling", "1");
                    }
                    acroFields.SetField("txt_F_Swelling", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Pain")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Pain", "1");
                    }
                    acroFields.SetField("txt_F_Pain", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Weakness")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Weakness", "1");
                    }
                    acroFields.SetField("txt_F_Weakness", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Stiffness")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Stiffness", "1");
                    }
                    acroFields.SetField("txt_F_Stiffness", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Other(Specify)")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Other", "1");
                    }
                    acroFields.SetField("txt_F_Other", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet2 = this.Patientinjury(BillId);
            for (int k = 0; k < dataSet2.Tables[0].Rows.Count; k++)
            {
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Abrasion")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Abrasion", "1");
                    }
                    acroFields.SetField("txt_F_3_Abrasion", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Infectious Disease")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_InfectiousDisease", "1");
                    }
                    acroFields.SetField("txt_F_3_InfectiousDisease", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Amputation")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Amputation", "1");
                    }
                    acroFields.SetField("txt_F_3_Amputation", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Inhalation Exposure")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Inhalation_Exposure", "1");
                    }
                    acroFields.SetField("txt_F_3_Inhalation_Exposure", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Avulsion")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Avulsion", "1");
                    }
                    acroFields.SetField("txt_F_3_Avulsion", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Laceration")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Laceration", "1");
                    }
                    acroFields.SetField("txt_F_3_Laceration", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Bite")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Bite", "1");
                    }
                    acroFields.SetField("txt_F_3_Bite", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Needle Stick")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_NeedleStick", "1");
                    }
                    acroFields.SetField("txt_F_3_NeedleStick", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Burn")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Burn", "1");
                    }
                    acroFields.SetField("txt_F_3_Burn", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Poisoning/Toxic Effects")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Poisoning", "1");
                    }
                    acroFields.SetField("txt_F_3_Poisoning", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Contusion/Hematoma")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Contusion", "1");
                    }
                    acroFields.SetField("txt_F_3_Contusion", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Psychological")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Psychological", "1");
                    }
                    acroFields.SetField("txt_F_3_Psychological", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Crush Injury")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_CurshInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_CurshInjury", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Puncture Wound")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_PuntureWound", "1");
                    }
                    acroFields.SetField("txt_F_3_PuntureWound", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Dermatitis")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Dermatitis", "1");
                    }
                    acroFields.SetField("txt_F_3_Dermatitis", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Repetitive Strain Injury")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_RepetitiveStrainInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_RepetitiveStrainInjury", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Dislocation")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Dislocation", "1");
                    }
                    acroFields.SetField("txt_F_3_Dislocation", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Spinal Cord Injury")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_SpinalCordInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_SpinalCordInjury", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Fracture")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Fracture", "1");
                    }
                    acroFields.SetField("txt_F_3_Fracture", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Sprain/Strain")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Sprain", "1");
                    }
                    acroFields.SetField("txt_F_3_Sprain", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Hearing Loss")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_HearingLoss", "1");
                    }
                    acroFields.SetField("txt_F_3_HearingLoss", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Torn Ligament Tendon or Muscle")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Torn", "1");
                    }
                    acroFields.SetField("txt_F_3_Torn", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Hernia")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Hernia", "1");
                    }
                    acroFields.SetField("txt_F_3_Hernia", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Vision Loss")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_VisionLoss", "1");
                    }
                    acroFields.SetField("txt_F_3_VisionLoss", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Other(Specify) :")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Other", "1");
                    }
                    acroFields.SetField("txt_F_3_Other", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet3 = this.PatientPhysicalExam(BillId);
            for (int l = 0; l < dataSet3.Tables[0].Rows.Count; l++)
            {
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "None at present" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_7_None", "1");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Bruising")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Bruising", "1");
                    }
                    acroFields.SetField("txt_F_4_Bruising", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Burns")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Burns", "1");
                    }
                    acroFields.SetField("txt_F_4_Burns", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Crepitation")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Crepitation", "1");
                    }
                    acroFields.SetField("txt_F_4_Crepitation", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Deformity")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Deformity", "1");
                    }
                    acroFields.SetField("txt_F_4_Deformity", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Edema")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Edema", "1");
                    }
                    acroFields.SetField("txt_F_4_Edema", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Hematoma/Lump/Swelling")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Hematoma", "1");
                    }
                    acroFields.SetField("txt_F_4_Hematoma", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Joint Effusion")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_JointEffusion", "1");
                    }
                    acroFields.SetField("txt_F_4_JointEffusion", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Laceration/Sutures")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Laceration", "1");
                    }
                    acroFields.SetField("txt_F_4_Laceration", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Pain/Tenderness")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Pain", "1");
                    }
                    acroFields.SetField("txt_F_4_Pain", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Scar")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Scar", "1");
                    }
                    acroFields.SetField("txt_F_4_Scar", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Other(Specify) ::")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_OtherFindings", "1");
                    }
                    acroFields.SetField("txt_F_4_OtherFindings", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Neuromuscular Findings:" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_4_Neuromuscular", "1");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Abnormal/Restricted ROM" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_4_Abnormal", "1");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Active ROM")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_ActiveROM", "1");
                    }
                    acroFields.SetField("txt_F_4_ActiveROM", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Passive ROM")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_PassiveROM", "1");
                    }
                    acroFields.SetField("txt_F_4_PassiveROM", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Gait")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Gait", "1");
                    }
                    acroFields.SetField("txt_F_4_Gait", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Palpable Muscle Spasm")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Palpable", "1");
                    }
                    acroFields.SetField("txt_F_4_Palpable", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Reflexes")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Reflexes", "1");
                    }
                    acroFields.SetField("txt_F_4_Reflexes", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Sensation")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Sensation", "1");
                    }
                    acroFields.SetField("txt_F_4_Sensation", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Strength (Weakness)")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Strength", "1");
                    }
                    acroFields.SetField("txt_F_4_Strength", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Wasting/Muscle Atrophy")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Wasting", "1");
                    }
                    acroFields.SetField("txt_F_4_Wasting", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet4 = this.DiagnosticTests(BillId);
            for (int m = 0; m < dataSet4.Tables[0].Rows.Count; m++)
            {
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "CT Scan" && dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_CTScan", "1");
                }
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "EMG/NCS" && dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_EMGNCS", "1");
                }
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "MRI (specify):")
                {
                    if (dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_MRI", "1");
                    }
                    acroFields.SetField("txt_H_3_MRI", dataSet4.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Labs (specify):")
                {
                    if (dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Labs", "1");
                    }
                    acroFields.SetField("txt_H_3_Labs", dataSet4.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "X-rays (specify):")
                {
                    if (dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_XRay", "1");
                    }
                    acroFields.SetField("txt_H_3_XRay", dataSet4.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Other (specify):")
                {
                    if (dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Left_Other", "1");
                    }
                    acroFields.SetField("txt_H_3_Left_Other", dataSet4.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet5 = this.Referrals(BillId);
            for (int n = 0; n < dataSet5.Tables[0].Rows.Count; n++)
            {
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Chiropractor" && dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_Chiropractor", "1");
                }
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Internist/Family Physician" && dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_Interist", "1");
                }
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Occupational Therapist" && dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_Occupational", "1");
                }
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Physical Therapist" && dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_Physical", "1");
                }
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Specialist in")
                {
                    if (dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Specialist", "1");
                    }
                    acroFields.SetField("txt_H_3_Specialist", dataSet5.Tables[0].Rows[n]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Other (specify)")
                {
                    if (dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Right_Other", "1");
                    }
                    acroFields.SetField("txt_H_3_Right_Other", dataSet5.Tables[0].Rows[n]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet6 = this.AssistiveDevices(BillId);
            for (int num4 = 0; num4 < dataSet6.Tables[0].Rows.Count; num4++)
            {
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Cane" && dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Cane", "1");
                }
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Crutches" && dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Crutches", "1");
                }
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Orthotics" && dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Orthotics", "1");
                }
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Walker" && dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Walker", "1");
                }
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Wheelchair" && dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_WheelChair", "1");
                }
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Other-(specify):")
                {
                    if (dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_4_Other", "1");
                    }
                    acroFields.SetField("txt_H_4_Other", dataSet6.Tables[0].Rows[num4]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet7 = this.WorkStatus(BillId);
            for (int num5 = 0; num5 < dataSet7.Tables[0].Rows.Count; num5++)
            {
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Bending/twisting" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Bending", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Lifting" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Lifting", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Sitting" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Sitting", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Climbing stairs/ladders" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Climbing", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Operating heavy equipment" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_OperatingHeavy", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Standing" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Standing", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Environmental conditions" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_EnvironmentalCond", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Operation of motor vehicles" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_OpMotorVehicle", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Use of public transportation" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_UsePublicTrans", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Kneeling" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Kneeling", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Personal protective equipment" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_PersonalProtectiveEq", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Use of upper extremities" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_UseOfUpperExtremeities", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Other (explain):")
                {
                    if (dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_I_2_C_Other", "1");
                    }
                    acroFields.SetField("SZ_LIMITATION_DURATION", value.Tables[0].Rows[0]["SZ_LIMITATION_DURATION"].ToString());
                }
            }
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL"].ToString());
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL_1", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL_1"].ToString());
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL_2", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL_2"].ToString());
            acroFields.SetField("SZ_INJURY_LEARN_SOURCE_DESCRIPTION", value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION", value.Tables[0].Rows[0]["SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION"].ToString());
            acroFields.SetField("DT_PREVIOUSLY_TREATED_DATE", value.Tables[0].Rows[0]["DT_PREVIOUSLY_TREATED_DATE"].ToString());
            acroFields.SetField("DT_DATE_OF_EXAMINATION", value.Tables[0].Rows[0]["DT_DATE_OF_EXAMINATION"].ToString());
            acroFields.SetField("SZ_DIAGNOSTIC_TEST", value.Tables[0].Rows[0]["SZ_DIAGNOSTIC_TEST"].ToString());
            acroFields.SetField("SZ_DIAGNOSTIC_TEST_1", value.Tables[0].Rows[0]["SZ_DIAGNOSTIC_TEST_1"].ToString());
            acroFields.SetField("SZ_TREATMENT", value.Tables[0].Rows[0]["SZ_TREATMENT"].ToString());
            acroFields.SetField("SZ_TREATMENT_1", value.Tables[0].Rows[0]["SZ_TREATMENT_1"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY_1", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY_1"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY_2", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY_2"].ToString());
            acroFields.SetField("SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION", value.Tables[0].Rows[0]["SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1", value.Tables[0].Rows[0]["SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1"].ToString());
            acroFields.SetField("FT_TEMPORARY_IMPAIRMENT_PERCENTAGE", value.Tables[0].Rows[0]["FT_TEMPORARY_IMPAIRMENT_PERCENTAGE"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS", value.Tables[0].Rows[0]["SZ_TEST_RESULTS"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS_1", value.Tables[0].Rows[0]["SZ_TEST_RESULTS_1"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT_1", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT_1"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT_3", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT_3"].ToString());
            acroFields.SetField("SZ_MEDICATIONS_PRESCRIBED", value.Tables[0].Rows[0]["SZ_MEDICATIONS_PRESCRIBED"].ToString());
            acroFields.SetField("SZ_MEDICATIONS_ADVISED", value.Tables[0].Rows[0]["SZ_MEDICATIONS_ADVISED"].ToString());
            acroFields.SetField("SZ_MEDICATION_RESTRICTIONS_DESCRIPTION", value.Tables[0].Rows[0]["SZ_MEDICATION_RESTRICTIONS_DESCRIPTION"].ToString());
            acroFields.SetField("Text226", value.Tables[0].Rows[0]["Text226"].ToString());
            acroFields.SetField("SZ_APPOINTMENT", value.Tables[0].Rows[0]["SZ_APPOINTMENT"].ToString());
            acroFields.SetField("SZ_VARIANCE_GUIDELINES", value.Tables[0].Rows[0]["SZ_VARIANCE_GUIDELINES"].ToString());
            //acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_DD", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_DD"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_MM", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_MM"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_YY", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_YY"].ToString());
            acroFields.SetField("SZ_QUANIFY_LIMITATION", value.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION"].ToString());
            acroFields.SetField("SZ_QUANIFY_LIMITATION_1", value.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION_1"].ToString());
            acroFields.SetField("SZ_QUANIFY_LIMITATION_2", value.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION_2"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("SZ_READINGDOCTOR", value.Tables[0].Rows[0]["SZ_READINGDOCTOR"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("txtTodayDate", value.Tables[0].Rows[0]["txtTodayDate"].ToString());
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC40LessThan5(string CompnayId, string CaseId, string BillId, string CompanyName, ServerConnection conn)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C4"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create), '4');
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value = this.GetValue(BillId, conn);
            if (value.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString() != "")
            {
                float[] fieldPositions = acroFields.GetFieldPositions("SZ_OFFICE_NAME");
                if (fieldPositions == null || fieldPositions.Length <= 0)
                {
                    throw new ApplicationException("Error locating field");
                }
                float absoluteX = 139.5f;
                float num = 93.75f;
                float num2 = 370.5f;
                float num3 = 77.25f;
                Image instance = Image.GetInstance(value.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString());
                instance.ScaleToFit(num - 25f, num3 - 25f);
                instance.SetAbsolutePosition(absoluteX, num2 - 279f);
                PdfContentByte overContent = pdfStamper.GetOverContent(4);
                overContent.AddImage(instance);
            }
            else
            {
                acroFields.SetField("SZ_OFFICE_NAME", value.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString());
            }
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_MI", value.Tables[0].Rows[0]["SZ_MI"].ToString());
            acroFields.SetField("SZ_SOCIAL_SECURITY_NO", value.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_HOME_PHONE1", value.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE1"].ToString());
            acroFields.SetField("SZ_PATIENT_HOME_PHONE2", value.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE2"].ToString());
            acroFields.SetField("SZ_WCB_NO", value.Tables[0].Rows[0]["SZ_WCB_NO"].ToString());
            acroFields.SetField("SZ_CARRIER_CASE_NO", value.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_FULL_ADDRESS", value.Tables[0].Rows[0]["SZ_PATIENT_FULL_ADDRESS"].ToString());
            acroFields.SetField("SZ_PATIENT_CITY", value.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
            acroFields.SetField("SZ_PATIENT_STATE", value.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
            acroFields.SetField("SZ_PATIENT_ZIP", value.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());

            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_DD", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_MM", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_YY", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString());
            }
            acroFields.SetField("DOB_DD", value.Tables[0].Rows[0]["DOB_DD"].ToString());
            acroFields.SetField("DOB_MM", value.Tables[0].Rows[0]["DOB_MM"].ToString());
            acroFields.SetField("DOB_YY", value.Tables[0].Rows[0]["DOB_YY"].ToString());
            acroFields.SetField("SZ_JOB_TITLE", value.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString());
            acroFields.SetField("SZ_WORK_ACTIVITIES", value.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES1"].ToString());
            acroFields.SetField("SZ_WORK_ACTIVITIES_1", value.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES2"].ToString());
            acroFields.SetField("SZ_EMPLOYER_NAME", value.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString());
            acroFields.SetField("SZ_EMPLOYER_PHONE1", value.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE1"].ToString());
            acroFields.SetField("SZ_EMPLOYER_PHONE2", value.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE2"].ToString());
            acroFields.SetField("SZ_EMPLOYER_ADDRESS", value.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString());
            acroFields.SetField("SZ_EMPLOYER_CITY", value.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString());
            acroFields.SetField("SZ_EMPLOYER_STATE", value.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString());
            acroFields.SetField("SZ_EMPLOYER_ZIP", value.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString());
            acroFields.SetField("SZ_DOCTOR_NAME", value.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
            acroFields.SetField("SZ_WCB_AUTHORIZATION", value.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString());
            acroFields.SetField("SZ_WCB_RATING_CODE", value.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString());
            acroFields.SetField("SZ_OFFICE_ADDRESS", value.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
            acroFields.SetField("SZ_OFFICE_CITY", value.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString());
            acroFields.SetField("SZ_OFFICE_STATE", value.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString());
            acroFields.SetField("SZ_OFFICE_ZIP", value.Tables[0].Rows[0]["SZ_OFFICE_ZIP"].ToString());
            acroFields.SetField("SZ_BILLING_GROUP_NAME", value.Tables[0].Rows[0]["SZ_BILLING_GROUP_NAME"].ToString());
            acroFields.SetField("SZ_BILLING_ADDRESS", value.Tables[0].Rows[0]["SZ_BILLING_ADDRESS"].ToString());
            acroFields.SetField("SZ_BILLING_CITY", value.Tables[0].Rows[0]["SZ_BILLING_CITY"].ToString());
            acroFields.SetField("SZ_BILLING_STATE", value.Tables[0].Rows[0]["SZ_BILLING_STATE"].ToString());
            acroFields.SetField("SZ_BILLING_ZIP", value.Tables[0].Rows[0]["SZ_BILLING_ZIP"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE1", value.Tables[0].Rows[0]["SZ_OFFICE_PHONE1"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE2", value.Tables[0].Rows[0]["SZ_OFFICE_PHONE2"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE1", value.Tables[0].Rows[0]["SZ_BILLING_PHONE1"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE2", value.Tables[0].Rows[0]["SZ_BILLING_PHONE2"].ToString());
            acroFields.SetField("SZ_NPI", value.Tables[0].Rows[0]["SZ_NPI"].ToString());
            acroFields.SetField("SZ_FEDERAL_TAX_ID", value.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
            acroFields.SetField("SZ_INSURANCE_NAME", value.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
            acroFields.SetField("SZ_INSURANCE_STREET", value.Tables[0].Rows[0]["SZ_INSURANCE_STREET"].ToString());
            acroFields.SetField("SZ_INSURANCE_CITY", value.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString());
            acroFields.SetField("SZ_INSURANCE_STATE", value.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString());
            acroFields.SetField("SZ_INSURANCE_ZIP", value.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString());
            acroFields.SetField("SZ_DIGNOSIS", value.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString());
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_MI", value.Tables[0].Rows[0]["SZ_MI"].ToString());
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_DD", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_MM", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_YY", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString());
            }
            acroFields.SetField("SZ_DIAGNOSIS_CODE", value.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString());
            acroFields.SetField("txtTodayDate_DD", value.Tables[0].Rows[0]["txtTodayDate_DD"].ToString());
            acroFields.SetField("txtTodayDate_MM", value.Tables[0].Rows[0]["txtTodayDate_MM"].ToString());
            acroFields.SetField("txtTodayDate_YY", value.Tables[0].Rows[0]["txtTodayDate_YY"].ToString());
            acroFields.SetField("SZ_PATIENT_ACCOUNT_NO", value.Tables[0].Rows[0]["SZ_PATIENT_ACCOUNT_NO"].ToString());
            DataSet services = this.GetServices(BillId, conn);
            for (int i = 0; i < services.Tables[0].Rows.Count; i++)
            {
                acroFields.SetField("FROM_MM_" + (i + 1).ToString(), services.Tables[0].Rows[i]["MONTH"].ToString());
                acroFields.SetField("FROM_DD_" + (i + 1).ToString(), services.Tables[0].Rows[i]["DAY"].ToString());
                acroFields.SetField("FROM_YY_" + (i + 1).ToString(), services.Tables[0].Rows[i]["YEAR"].ToString());
                acroFields.SetField("TO_MM_" + (i + 1).ToString(), services.Tables[0].Rows[i]["TO_MONTH"].ToString());
                acroFields.SetField("TO_DD_" + (i + 1).ToString(), services.Tables[0].Rows[i]["TO_DAY"].ToString());
                acroFields.SetField("TO_YY_" + (i + 1).ToString(), services.Tables[0].Rows[i]["TO_YEAR"].ToString());
                acroFields.SetField("CPT_" + (i + 1).ToString(), services.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
                acroFields.SetField("MODIFIER_" + (i + 1).ToString(), services.Tables[0].Rows[i]["SZ_MODIFIER"].ToString());
                acroFields.SetField("CHARGE_" + (i + 1).ToString(), services.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                acroFields.SetField("UNIT_" + (i + 1).ToString(), services.Tables[0].Rows[i]["I_UNIT"].ToString());
                acroFields.SetField("ZIP_" + (i + 1).ToString(), services.Tables[0].Rows[i]["ZIP_CODE"].ToString());
                acroFields.SetField("POS_" + (i + 1).ToString(), services.Tables[0].Rows[i]["PLACE_OF_SERVICE"].ToString());
                acroFields.SetField("DC_" + (i + 1).ToString(), value.Tables[0].Rows[0]["DC_1"].ToString());
            }
            acroFields.SetField("TOTAL_BILL_AMOUNT", services.Tables[0].Rows[0]["BILL_AMOUNT"].ToString());
            acroFields.SetField("TOTAL_PAID_AMOUNT", services.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
            acroFields.SetField("TOTAL_BAL_AMOUNT", services.Tables[0].Rows[0]["BALANCE"].ToString());
            if (value.Tables[0].Rows[0]["BT_PATIENT_MISSED_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_MISSED_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_No", "1");
            }
            if (value.Tables[0].Rows[0]["I_GENDER"].ToString() == "0")
            {
                acroFields.SetField("chkMale", "1");
            }
            if (value.Tables[0].Rows[0]["I_GENDER"].ToString() == "1")
            {
                acroFields.SetField("chkFemale", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ANOTHER_HEALTH_PROVIDER"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ANOTHER_HEALTH_PROVIDER"].ToString() == "1")
            {
                acroFields.SetField("chk_H_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_PREVIOUSLY_TREATED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_4_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_PREVIOUSLY_TREATED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_4_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_AFFECT_TREATMENT_PROGNOSIS"].ToString() == "0")
            {
                acroFields.SetField("chk_F_8_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_AFFECT_TREATMENT_PROGNOSIS"].ToString() == "1")
            {
                acroFields.SetField("chk_F_8_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "0")
            {
                acroFields.SetField("chk_G_1_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "1")
            {
                acroFields.SetField("chk_G_1_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "0")
            {
                acroFields.SetField("chk_G_2_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "1")
            {
                acroFields.SetField("chk_G_2_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "0")
            {
                acroFields.SetField("chk_G_3_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "1")
            {
                acroFields.SetField("chk_G_3_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "2")
            {
                acroFields.SetField("chk_G_3_NA", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PPO"].ToString() == "0")
            {
                acroFields.SetField("chkService", "1");
            }
            if (value.Tables[0].Rows[0]["BT_MEDICATION_RESTRICTIONS"].ToString() == "False")
            {
                acroFields.SetField("chk_H_2_None", "1");
            }
            if (value.Tables[0].Rows[0]["BT_MEDICATION_RESTRICTIONS"].ToString() == "True")
            {
                acroFields.SetField("chk_H_2_Other", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_3_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_3_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ADHERE_NEWYORK_TREATMENT"].ToString() == "0")
            {
                acroFields.SetField("chk_H_8_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ADHERE_NEWYORK_TREATMENT"].ToString() == "1")
            {
                acroFields.SetField("chk_H_8_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_CUR_WORKING"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_PatientWorkingYes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_CUR_WORKING"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_PatientWorkingNo", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "0")
            {
                acroFields.SetField("chkPhysician", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "1")
            {
                acroFields.SetField("chkPodiatrist", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "2")
            {
                acroFields.SetField("chkChiropractor", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "0")
            {
                acroFields.SetField("chk_SSN", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "1")
            {
                acroFields.SetField("chk_EIN", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Patient", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "1")
            {
                acroFields.SetField("chk_H_MedicalRecords", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "2")
            {
                acroFields.SetField("chk_H_Other", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "0")
            {
                acroFields.SetField("chk_H_5_WithinWeek", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "1")
            {
                acroFields.SetField("chk_H_5_1_2_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "2")
            {
                acroFields.SetField("chk_H_5_3_4_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "3")
            {
                acroFields.SetField("chk_H_5_5_6_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "4")
            {
                acroFields.SetField("chk_H_5_7_8_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "5")
            {
                acroFields.SetField("chk_H_5_Month", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "6")
            {
                acroFields.SetField("chk_H_5_Return", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_WORK_TYPE"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_PatientReturnYes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_WORK_TYPE"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_PatientReturnNo", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_H_2_1_2_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_H_2_3_7_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_H_2_8_14_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_H_2_15P_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_H_2_Unknown", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "5")
            {
                acroFields.SetField("chk_H_2_NA", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_H_2_1_2_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_H_2_3_7_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_H_2_8_14_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "0")
            {
                acroFields.SetField("chk_H_3_Patient", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "1")
            {
                acroFields.SetField("chk_H_3_PatientsEmployer", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "2")
            {
                acroFields.SetField("chk_H_4_NA", "1");
            }
            if (value.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Provider_1", "1");
            }
            if (value.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_Provider_2", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_I_2_a", "1");
                acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION"].ToString());
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_I_2_b", "1");
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_DD"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_DD", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_DD"].ToString());
                }
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_MM"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_MM", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_MM"].ToString());
                }
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_YY"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_YY", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_YY"].ToString());
                }
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "2")
            {
                acroFields.SetField("chk_I_2_c", "1");
                if (value.Tables[0].Rows[0]["Text249_DD"].ToString() != "0")
                {
                    acroFields.SetField("Text249_DD", value.Tables[0].Rows[0]["Text249_DD"].ToString());
                }
                if (value.Tables[0].Rows[0]["Text249_MM"].ToString() != "0")
                {
                    acroFields.SetField("Text249_MM", value.Tables[0].Rows[0]["Text249_MM"].ToString());
                }
                if (value.Tables[0].Rows[0]["Text249_YY"].ToString() != "0")
                {
                    acroFields.SetField("Text249_YY", value.Tables[0].Rows[0]["Text249_YY"].ToString());
                }
            }
            DataSet dataSet = this.PATIENT_COMPLAINTS(BillId, conn);
            for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
            {
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Numbness/Tingling")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Numbness", "1");
                    }
                    acroFields.SetField("txt_F_Numbness", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Swelling")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Swelling", "1");
                    }
                    acroFields.SetField("txt_F_Swelling", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Pain")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Pain", "1");
                    }
                    acroFields.SetField("txt_F_Pain", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Weakness")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Weakness", "1");
                    }
                    acroFields.SetField("txt_F_Weakness", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Stiffness")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Stiffness", "1");
                    }
                    acroFields.SetField("txt_F_Stiffness", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Other(Specify)")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Other", "1");
                    }
                    acroFields.SetField("txt_F_Other", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet2 = this.Patientinjury(BillId, conn);
            for (int k = 0; k < dataSet2.Tables[0].Rows.Count; k++)
            {
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Abrasion")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Abrasion", "1");
                    }
                    acroFields.SetField("txt_F_3_Abrasion", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Infectious Disease")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_InfectiousDisease", "1");
                    }
                    acroFields.SetField("txt_F_3_InfectiousDisease", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Amputation")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Amputation", "1");
                    }
                    acroFields.SetField("txt_F_3_Amputation", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Inhalation Exposure")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Inhalation_Exposure", "1");
                    }
                    acroFields.SetField("txt_F_3_Inhalation_Exposure", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Avulsion")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Avulsion", "1");
                    }
                    acroFields.SetField("txt_F_3_Avulsion", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Laceration")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Laceration", "1");
                    }
                    acroFields.SetField("txt_F_3_Laceration", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Bite")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Bite", "1");
                    }
                    acroFields.SetField("txt_F_3_Bite", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Needle Stick")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_NeedleStick", "1");
                    }
                    acroFields.SetField("txt_F_3_NeedleStick", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Burn")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Burn", "1");
                    }
                    acroFields.SetField("txt_F_3_Burn", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Poisoning/Toxic Effects")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Poisoning", "1");
                    }
                    acroFields.SetField("txt_F_3_Poisoning", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Contusion/Hematoma")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Contusion", "1");
                    }
                    acroFields.SetField("txt_F_3_Contusion", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Psychological")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Psychological", "1");
                    }
                    acroFields.SetField("txt_F_3_Psychological", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Crush Injury")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_CurshInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_CurshInjury", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Puncture Wound")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_PuntureWound", "1");
                    }
                    acroFields.SetField("txt_F_3_PuntureWound", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Dermatitis")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Dermatitis", "1");
                    }
                    acroFields.SetField("txt_F_3_Dermatitis", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Repetitive Strain Injury")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_RepetitiveStrainInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_RepetitiveStrainInjury", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Dislocation")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Dislocation", "1");
                    }
                    acroFields.SetField("txt_F_3_Dislocation", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Spinal Cord Injury")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_SpinalCordInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_SpinalCordInjury", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Fracture")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Fracture", "1");
                    }
                    acroFields.SetField("txt_F_3_Fracture", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Sprain/Strain")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Sprain", "1");
                    }
                    acroFields.SetField("txt_F_3_Sprain", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Hearing Loss")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_HearingLoss", "1");
                    }
                    acroFields.SetField("txt_F_3_HearingLoss", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Torn Ligament Tendon or Muscle")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Torn", "1");
                    }
                    acroFields.SetField("txt_F_3_Torn", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Hernia")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Hernia", "1");
                    }
                    acroFields.SetField("txt_F_3_Hernia", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Vision Loss")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_VisionLoss", "1");
                    }
                    acroFields.SetField("txt_F_3_VisionLoss", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Other(Specify) :")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Other", "1");
                    }
                    acroFields.SetField("txt_F_3_Other", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet3 = this.PatientPhysicalExam(BillId, conn);
            for (int l = 0; l < dataSet3.Tables[0].Rows.Count; l++)
            {
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "None at present" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_7_None", "1");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Bruising")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Bruising", "1");
                    }
                    acroFields.SetField("txt_F_4_Bruising", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Burns")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Burns", "1");
                    }
                    acroFields.SetField("txt_F_4_Burns", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Crepitation")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Crepitation", "1");
                    }
                    acroFields.SetField("txt_F_4_Crepitation", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Deformity")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Deformity", "1");
                    }
                    acroFields.SetField("txt_F_4_Deformity", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Edema")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Edema", "1");
                    }
                    acroFields.SetField("txt_F_4_Edema", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Hematoma/Lump/Swelling")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Hematoma", "1");
                    }
                    acroFields.SetField("txt_F_4_Hematoma", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Joint Effusion")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_JointEffusion", "1");
                    }
                    acroFields.SetField("txt_F_4_JointEffusion", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Laceration/Sutures")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Laceration", "1");
                    }
                    acroFields.SetField("txt_F_4_Laceration", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Pain/Tenderness")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Pain", "1");
                    }
                    acroFields.SetField("txt_F_4_Pain", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Scar")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Scar", "1");
                    }
                    acroFields.SetField("txt_F_4_Scar", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Other(Specify) ::")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_OtherFindings", "1");
                    }
                    acroFields.SetField("txt_F_4_OtherFindings", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Neuromuscular Findings:" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_4_Neuromuscular", "1");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Abnormal/Restricted ROM" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_4_Abnormal", "1");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Active ROM")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_ActiveROM", "1");
                    }
                    acroFields.SetField("txt_F_4_ActiveROM", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Passive ROM")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_PassiveROM", "1");
                    }
                    acroFields.SetField("txt_F_4_PassiveROM", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Gait")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Gait", "1");
                    }
                    acroFields.SetField("txt_F_4_Gait", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Palpable Muscle Spasm")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Palpable", "1");
                    }
                    acroFields.SetField("txt_F_4_Palpable", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Reflexes")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Reflexes", "1");
                    }
                    acroFields.SetField("txt_F_4_Reflexes", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Sensation")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Sensation", "1");
                    }
                    acroFields.SetField("txt_F_4_Sensation", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Strength (Weakness)")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Strength", "1");
                    }
                    acroFields.SetField("txt_F_4_Strength", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Wasting/Muscle Atrophy")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Wasting", "1");
                    }
                    acroFields.SetField("txt_F_4_Wasting", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet4 = this.DiagnosticTests(BillId, conn);
            for (int m = 0; m < dataSet4.Tables[0].Rows.Count; m++)
            {
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "CT Scan" && dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_CTScan", "1");
                }
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "EMG/NCS" && dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_EMGNCS", "1");
                }
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "MRI (specify):")
                {
                    if (dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_MRI", "1");
                    }
                    acroFields.SetField("txt_H_3_MRI", dataSet4.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Labs (specify):")
                {
                    if (dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Labs", "1");
                    }
                    acroFields.SetField("txt_H_3_Labs", dataSet4.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "X-rays (specify):")
                {
                    if (dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_XRay", "1");
                    }
                    acroFields.SetField("txt_H_3_XRay", dataSet4.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Other (specify):")
                {
                    if (dataSet4.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Left_Other", "1");
                    }
                    acroFields.SetField("txt_H_3_Left_Other", dataSet4.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet5 = this.Referrals(BillId, conn);
            for (int n = 0; n < dataSet5.Tables[0].Rows.Count; n++)
            {
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Chiropractor" && dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_Chiropractor", "1");
                }
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Internist/Family Physician" && dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_Interist", "1");
                }
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Occupational Therapist" && dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_Occupational", "1");
                }
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Physical Therapist" && dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_Physical", "1");
                }
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Specialist in")
                {
                    if (dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Specialist", "1");
                    }
                    acroFields.SetField("txt_H_3_Specialist", dataSet5.Tables[0].Rows[n]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet5.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Other (specify)")
                {
                    if (dataSet5.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Right_Other", "1");
                    }
                    acroFields.SetField("txt_H_3_Right_Other", dataSet5.Tables[0].Rows[n]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet6 = this.AssistiveDevices(BillId, conn);
            for (int num4 = 0; num4 < dataSet6.Tables[0].Rows.Count; num4++)
            {
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Cane" && dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Cane", "1");
                }
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Crutches" && dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Crutches", "1");
                }
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Orthotics" && dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Orthotics", "1");
                }
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Walker" && dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Walker", "1");
                }
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Wheelchair" && dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_WheelChair", "1");
                }
                if (dataSet6.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Other-(specify):")
                {
                    if (dataSet6.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_4_Other", "1");
                    }
                    acroFields.SetField("txt_H_4_Other", dataSet6.Tables[0].Rows[num4]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet7 = this.WorkStatus(BillId, conn);
            for (int num5 = 0; num5 < dataSet7.Tables[0].Rows.Count; num5++)
            {
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Bending/twisting" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Bending", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Lifting" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Lifting", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Sitting" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Sitting", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Climbing stairs/ladders" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Climbing", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Operating heavy equipment" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_OperatingHeavy", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Standing" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Standing", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Environmental conditions" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_EnvironmentalCond", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Operation of motor vehicles" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_OpMotorVehicle", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Use of public transportation" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_UsePublicTrans", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Kneeling" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Kneeling", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Personal protective equipment" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_PersonalProtectiveEq", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Use of upper extremities" && dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_UseOfUpperExtremeities", "1");
                }
                if (dataSet7.Tables[0].Rows[num5]["SZ_TEXT"].ToString() == "Other (explain):")
                {
                    if (dataSet7.Tables[0].Rows[num5]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_I_2_C_Other", "1");
                    }
                    acroFields.SetField("SZ_LIMITATION_DURATION", value.Tables[0].Rows[0]["SZ_LIMITATION_DURATION"].ToString());
                }
            }
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL"].ToString());
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL_1", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL_1"].ToString());
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL_2", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL_2"].ToString());
            acroFields.SetField("SZ_INJURY_LEARN_SOURCE_DESCRIPTION", value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION", value.Tables[0].Rows[0]["SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION"].ToString());
            acroFields.SetField("DT_PREVIOUSLY_TREATED_DATE", value.Tables[0].Rows[0]["DT_PREVIOUSLY_TREATED_DATE"].ToString());
            acroFields.SetField("DT_DATE_OF_EXAMINATION", value.Tables[0].Rows[0]["DT_DATE_OF_EXAMINATION"].ToString());
            acroFields.SetField("SZ_DIAGNOSTIC_TEST", value.Tables[0].Rows[0]["SZ_DIAGNOSTIC_TEST"].ToString());
            acroFields.SetField("SZ_DIAGNOSTIC_TEST_1", value.Tables[0].Rows[0]["SZ_DIAGNOSTIC_TEST_1"].ToString());
            acroFields.SetField("SZ_TREATMENT", value.Tables[0].Rows[0]["SZ_TREATMENT"].ToString());
            acroFields.SetField("SZ_TREATMENT_1", value.Tables[0].Rows[0]["SZ_TREATMENT_1"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY_1", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY_1"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY_2", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY_2"].ToString());
            acroFields.SetField("SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION", value.Tables[0].Rows[0]["SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1", value.Tables[0].Rows[0]["SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1"].ToString());
            acroFields.SetField("FT_TEMPORARY_IMPAIRMENT_PERCENTAGE", value.Tables[0].Rows[0]["FT_TEMPORARY_IMPAIRMENT_PERCENTAGE"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS", value.Tables[0].Rows[0]["SZ_TEST_RESULTS"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS_1", value.Tables[0].Rows[0]["SZ_TEST_RESULTS_1"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT_1", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT_1"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT_3", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT_3"].ToString());
            acroFields.SetField("SZ_MEDICATIONS_PRESCRIBED", value.Tables[0].Rows[0]["SZ_MEDICATIONS_PRESCRIBED"].ToString());
            acroFields.SetField("SZ_MEDICATIONS_ADVISED", value.Tables[0].Rows[0]["SZ_MEDICATIONS_ADVISED"].ToString());
            acroFields.SetField("SZ_MEDICATION_RESTRICTIONS_DESCRIPTION", value.Tables[0].Rows[0]["SZ_MEDICATION_RESTRICTIONS_DESCRIPTION"].ToString());
            acroFields.SetField("Text226", value.Tables[0].Rows[0]["Text226"].ToString());
            acroFields.SetField("SZ_APPOINTMENT", value.Tables[0].Rows[0]["SZ_APPOINTMENT"].ToString());
            acroFields.SetField("SZ_VARIANCE_GUIDELINES", value.Tables[0].Rows[0]["SZ_VARIANCE_GUIDELINES"].ToString());
            //acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_DD", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_DD"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_MM", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_MM"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_YY", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_YY"].ToString());
            acroFields.SetField("SZ_QUANIFY_LIMITATION", value.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION"].ToString());
            acroFields.SetField("SZ_QUANIFY_LIMITATION_1", value.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION_1"].ToString());
            acroFields.SetField("SZ_QUANIFY_LIMITATION_2", value.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION_2"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("SZ_READINGDOCTOR", value.Tables[0].Rows[0]["SZ_READINGDOCTOR"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("txtTodayDate", value.Tables[0].Rows[0]["txtTodayDate"].ToString());
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC42LessThan5(string CompnayId, string CaseId, string BillId, string CompanyName)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C42"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value_C = this.GetValue_C42(BillId);
            if (value_C.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString() != "")
            {
                float[] fieldPositions = acroFields.GetFieldPositions("SZ_HEALT_CARE_NAME");
                if (fieldPositions == null || fieldPositions.Length <= 0)
                {
                    throw new ApplicationException("Error locating field");
                }
                float absoluteX = 139.5f;
                float num = 93.75f;
                float num2 = 370.5f;
                float num3 = 77.25f;
                Image instance = Image.GetInstance(value_C.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString());
                instance.ScaleToFit(num - 25f, num3 - 25f);
                instance.SetAbsolutePosition(absoluteX, num2 - 306f);
                PdfContentByte overContent = pdfStamper.GetOverContent(2);
                overContent.AddImage(instance);
            }
            else
            {
                acroFields.SetField("SZ_HEALT_CARE_NAME", value_C.Tables[0].Rows[0]["SZ_HEALT_CARE_NAME"].ToString());
            }
            acroFields.SetField("DT_DATE_OF_EXAMINATION", value_C.Tables[0].Rows[0]["DT_DATE_OF_EXAMINATION"].ToString());
            acroFields.SetField("SZ_WCB_NO", value_C.Tables[0].Rows[0]["SZ_WCB_NO"].ToString());
            acroFields.SetField("SZ_CARRIER_CASE_NO", value_C.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_MIDDLE_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_MIDDLE_NAME"].ToString());
            acroFields.SetField("DT_DATE_OF_ACCIDENT", value_C.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString());
            acroFields.SetField("SZ_SSN_NO", value_C.Tables[0].Rows[0]["SZ_SSN_NO"].ToString());
            acroFields.SetField("SZ_ADDRESS", value_C.Tables[0].Rows[0]["SZ_ADDRESS"].ToString());
            acroFields.SetField("SZ_CITY", value_C.Tables[0].Rows[0]["SZ_CITY"].ToString());
            acroFields.SetField("SZ_STATE", value_C.Tables[0].Rows[0]["SZ_STATE"].ToString());
            acroFields.SetField("SZ_ZIP", value_C.Tables[0].Rows[0]["SZ_ZIP"].ToString());
            acroFields.SetField("SZ_PATIENT_ACCOUNT_NO", value_C.Tables[0].Rows[0]["SZ_PATIENT_ACCOUNT_NO"].ToString());
            acroFields.SetField("SZ_DOCTOR_NAME", value_C.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
            acroFields.SetField("SZ_WCB_AOTH_NO", value_C.Tables[0].Rows[0]["SZ_WCB_AOTH_NO"].ToString());
            acroFields.SetField("SZ_WCB_RATING_CODE", value_C.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString());
            acroFields.SetField("SZ_FEDERAL_TAX_ID", value_C.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
            acroFields.SetField("SZ_OFFICE_ADDRESS", value_C.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
            acroFields.SetField("SZ_OFFICE_CITY", value_C.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString());
            acroFields.SetField("SZ_OFFICE_STATE", value_C.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString());
            acroFields.SetField("SZ_OFFICE_ZIP_CODE", value_C.Tables[0].Rows[0]["SZ_OFFICE_ZIP_CODE"].ToString());
            acroFields.SetField("SZ_BILLING_GROUP", value_C.Tables[0].Rows[0]["SZ_BILLING_GROUP"].ToString());
            acroFields.SetField("SZ_BILLING_ADDRESS", value_C.Tables[0].Rows[0]["SZ_BILLING_ADDRESS"].ToString());
            acroFields.SetField("SZ_BILLING_CITY", value_C.Tables[0].Rows[0]["SZ_BILLING_CITY"].ToString());
            acroFields.SetField("SZ_BILLING_STATE", value_C.Tables[0].Rows[0]["SZ_BILLING_STATE"].ToString());
            acroFields.SetField("SZ_BILLING_ZIP_CODE", value_C.Tables[0].Rows[0]["SZ_BILLING_ZIP_CODE"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE", value_C.Tables[0].Rows[0]["SZ_OFFICE_PHONE"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE", value_C.Tables[0].Rows[0]["SZ_BILLING_PHONE"].ToString());
            acroFields.SetField("SZ_NPI", value_C.Tables[0].Rows[0]["SZ_NPI"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY_CODE", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY_CODE"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY_ADDRESS", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY_ADDRESS"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS_CODE", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_CODE"].ToString());
            DataSet service_C = this.GetService_C42(BillId);
            for (int i = 0; i < service_C.Tables[0].Rows.Count; i++)
            {
                acroFields.SetField("SZ_FROM_MM_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["MONTH"].ToString());
                acroFields.SetField("SZ_FROM_DD_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["DAY"].ToString());
                acroFields.SetField("SZ_FROM_YY_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["YEAR"].ToString());
                acroFields.SetField("SZ_TO_MM_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["TO_MONTH"].ToString());
                acroFields.SetField("SZ_TO_DD_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["TO_DAY"].ToString());
                acroFields.SetField("SZ_TO_YY_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["TO_YEAR"].ToString());
                acroFields.SetField("SZ_CPT_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
                acroFields.SetField("SZ_CHARGES_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                acroFields.SetField("SZ_DAYS_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["I_UNIT"].ToString());
                acroFields.SetField("SZ_ZIP_CODE_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["ZIP_CODE"].ToString());
                acroFields.SetField("SZ_POS_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["PLACE_OF_SERVICE"].ToString());
                acroFields.SetField("SZ_MODIFER_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["SZ_MODIFIER"].ToString());

                acroFields.SetField("SZ_DIAG_CODE_" + (i + 1).ToString(), value_C.Tables[0].Rows[0]["SZ_DIAG_CODE_1"].ToString());

                acroFields.SetField("SZ_DIAG_CODE_" + (i + 1).ToString(), value_C.Tables[0].Rows[0]["SZ_DIAG_CODE_1"].ToString());

            }
            acroFields.SetField("SZ_TOTAL_AMOUNT", service_C.Tables[0].Rows[0]["BILL_AMOUNT"].ToString());
            acroFields.SetField("SZ_TOTAL_PAID_AMOUNT", service_C.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
            acroFields.SetField("SZ_TOTAL_BALANCE_AMOUNT", service_C.Tables[0].Rows[0]["BALANCE"].ToString());
            if (value_C.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "0")
            {
                acroFields.SetField("chk_D_5_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "1")
            {
                acroFields.SetField("chk_d_5_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "0")
            {
                acroFields.SetField("chk_E_1_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "1")
            {
                acroFields.SetField("chk_E_1_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "0")
            {
                acroFields.SetField("chk_E_2_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "1")
            {
                acroFields.SetField("chk_E_2_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "0")
            {
                acroFields.SetField("chk_E_3_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "1")
            {
                acroFields.SetField("chk_E_3_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "2")
            {
                acroFields.SetField("chk_E_3_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_IS_WORKING"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkingYes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_IS_WORKING"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkingNo", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_WORK_RESTRICTION"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkRestrictYes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_WORK_RESTRICTION"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkRestrictNo", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_BENEFIT"].ToString() == "0")
            {
                acroFields.SetField("chk_F_4_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_BENEFIT"].ToString() == "1")
            {
                acroFields.SetField("chk_F_3_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "0")
            {
                acroFields.SetField("chk_F_3_Patient", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "1")
            {
                acroFields.SetField("chk_F_3_Employer", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "2")
            {
                acroFields.SetField("chk_F_3_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "0")
            {
                acroFields.SetField("chk_D_7_withinweek", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "1")
            {
                acroFields.SetField("chk_D_7_1-2wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "2")
            {
                acroFields.SetField("chk_D_7_3-4wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "3")
            {
                acroFields.SetField("chk_D_7_5-6wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "4")
            {
                acroFields.SetField("chk_D_7_7-8wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "5")
            {
                acroFields.SetField("chk_D_7_mnths", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "6")
            {
                acroFields.SetField("chk_D_7_asneeded", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_1-2dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_3-7dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_8-14dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_15+dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_unknown", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_1-2dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_3-7dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_8-14dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_15+dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_unkown", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "5")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_F_2_a", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_F_2_b", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "2")
            {
                acroFields.SetField("chk_F_2_c", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "0")
            {
                acroFields.SetField("chk_F_4_ProvidedServices", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "1")
            {
                acroFields.SetField("chk_F_4_Supervised", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "0")
            {
                acroFields.SetField("CHK_SSN", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "1")
            {
                acroFields.SetField("CHK_EIN", "Yes");
            }
            DataSet dataSet = this.DIAGNOSTIC_TESTS_C42(BillId);
            for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
            {
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "CT Scan" && dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_ctscan", "Yes");
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "EMG/NCS" && dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_emg/ncs", "Yes");
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "MRI (specify):")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_mri", "Yes");
                    }
                    acroFields.SetField("txt_d_5mri", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Labs (specify):")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_labs", "Yes");
                    }
                    acroFields.SetField("txt_d_5labs", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "X-rays (specify):")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_xray", "Yes");
                    }
                    acroFields.SetField("txt_d_5xray", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Other (specify):")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_otherspecify1", "Yes");
                    }
                    acroFields.SetField("txt_d_5other1", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet2 = this.REFERRALS_C42(BillId);
            for (int k = 0; k < dataSet2.Tables[0].Rows.Count; k++)
            {
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Chiropractor" && dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_chiropractor", "Yes");
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Occupational Therapist" && dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_occutherapist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Internist/Family Physician" && dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_internist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Physical Therapist" && dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_phytherapist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Specialist in")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_specialist", "Yes");
                    }
                    acroFields.SetField("txt_d_5specialist", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Other (specify)")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_otherspecify2", "Yes");
                    }
                    acroFields.SetField("txt_d_5other2", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet3 = this.WORK_STATUS_C42(BillId);
            for (int l = 0; l < dataSet3.Tables[0].Rows.Count; l++)
            {
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Bending/twisting" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Bending", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Climbing stairs/ladders" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Climbing", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Environmental conditions" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F2Environment", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Kneeling" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Kneeling", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Other (explain):")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_2_OtherSpecify", "Yes");
                    }
                    acroFields.SetField("others", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Lifting" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Lifting", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Operating heavy equipment" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_OprEquip", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Operation of motor vehicles" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_vehical", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Personal protective equipment" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_proctectEquip", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Sitting" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F2cSit", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Standing" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Standing", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Use of public transportation" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Pubtransport", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Use of upper extremities" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_UpperExtremist", "Yes");
                }
            }
            acroFields.SetField("SZ_DIAGNOSIS_TREATEMENT_RENDERED", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_TREATEMENT_RENDERED"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS_TREATEMENT_RENDERED_1", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_TREATEMENT_RENDERED_1"].ToString());
            acroFields.SetField("SZ_NATURE_OF_INJURY", value_C.Tables[0].Rows[0]["SZ_NATURE_OF_INJURY"].ToString());
            acroFields.SetField("SZ_NATURE_OF_INJURY_1", value_C.Tables[0].Rows[0]["SZ_NATURE_OF_INJURY_1"].ToString());
            acroFields.SetField("SZ_AFFECTED_BY_INJURY", value_C.Tables[0].Rows[0]["SZ_AFFECTED_BY_INJURY"].ToString());
            acroFields.SetField("SZ_CHANGES_IN_TREATMENT_PLAN", value_C.Tables[0].Rows[0]["SZ_CHANGES_IN_TREATMENT_PLAN"].ToString());
            acroFields.SetField("SZ_TREATEMENT_RENDERED", value_C.Tables[0].Rows[0]["SZ_TREATEMENT_RENDERED"].ToString());
            acroFields.SetField("SZ_TREATEMENT_RENDERED_1", value_C.Tables[0].Rows[0]["SZ_TREATEMENT_RENDERED_1"].ToString());
            acroFields.SetField("SZ_APPOINTMENT_MONTH", value_C.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString());
            acroFields.SetField("FT_TEMPORARY_IMPAIRMENT_PERCENTAGE", value_C.Tables[0].Rows[0]["FT_TEMPORARY_IMPAIRMENT_PERCENTAGE"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS", value_C.Tables[0].Rows[0]["SZ_TEST_RESULTS"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS_1", value_C.Tables[0].Rows[0]["SZ_TEST_RESULTS_1"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION_0", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION0"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATIONMM", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATIONMM"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTIONMM", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTIONMM"].ToString());
            acroFields.SetField("SZ_QUANTIFY_LIMITATION", value_C.Tables[0].Rows[0]["SZ_QUANTIFY_LIMITATION"].ToString());
            acroFields.SetField("Text156", value_C.Tables[0].Rows[0]["Text156"].ToString());
            acroFields.SetField("SZ_BOARD_PROVIDER_NAME", value_C.Tables[0].Rows[0]["SZ_BOARD_PROVIDER_NAME"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value_C.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("CUR_MONTH", value_C.Tables[0].Rows[0]["CUR_MONTH"].ToString());
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC42LessThan5(string CompnayId, string CaseId, string BillId, string CompanyName, ServerConnection conn)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C42"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value_C = this.GetValue_C42(BillId, conn);
            if (value_C.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString() != "")
            {
                float[] fieldPositions = acroFields.GetFieldPositions("SZ_HEALT_CARE_NAME");
                if (fieldPositions == null || fieldPositions.Length <= 0)
                {
                    throw new ApplicationException("Error locating field");
                }
                float absoluteX = 139.5f;
                float num = 93.75f;
                float num2 = 370.5f;
                float num3 = 77.25f;
                Image instance = Image.GetInstance(value_C.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString());
                instance.ScaleToFit(num - 25f, num3 - 25f);
                instance.SetAbsolutePosition(absoluteX, num2 - 306f);
                PdfContentByte overContent = pdfStamper.GetOverContent(2);
                overContent.AddImage(instance);
            }
            else
            {
                acroFields.SetField("SZ_HEALT_CARE_NAME", value_C.Tables[0].Rows[0]["SZ_HEALT_CARE_NAME"].ToString());
            }
            acroFields.SetField("DT_DATE_OF_EXAMINATION", value_C.Tables[0].Rows[0]["DT_DATE_OF_EXAMINATION"].ToString());
            acroFields.SetField("SZ_WCB_NO", value_C.Tables[0].Rows[0]["SZ_WCB_NO"].ToString());
            acroFields.SetField("SZ_CARRIER_CASE_NO", value_C.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_MIDDLE_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_MIDDLE_NAME"].ToString());
            acroFields.SetField("DT_DATE_OF_ACCIDENT", value_C.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString());
            acroFields.SetField("SZ_SSN_NO", value_C.Tables[0].Rows[0]["SZ_SSN_NO"].ToString());
            acroFields.SetField("SZ_ADDRESS", value_C.Tables[0].Rows[0]["SZ_ADDRESS"].ToString());
            acroFields.SetField("SZ_CITY", value_C.Tables[0].Rows[0]["SZ_CITY"].ToString());
            acroFields.SetField("SZ_STATE", value_C.Tables[0].Rows[0]["SZ_STATE"].ToString());
            acroFields.SetField("SZ_ZIP", value_C.Tables[0].Rows[0]["SZ_ZIP"].ToString());
            acroFields.SetField("SZ_PATIENT_ACCOUNT_NO", value_C.Tables[0].Rows[0]["SZ_PATIENT_ACCOUNT_NO"].ToString());
            acroFields.SetField("SZ_DOCTOR_NAME", value_C.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
            acroFields.SetField("SZ_WCB_AOTH_NO", value_C.Tables[0].Rows[0]["SZ_WCB_AOTH_NO"].ToString());
            acroFields.SetField("SZ_WCB_RATING_CODE", value_C.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString());
            acroFields.SetField("SZ_FEDERAL_TAX_ID", value_C.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
            acroFields.SetField("SZ_OFFICE_ADDRESS", value_C.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
            acroFields.SetField("SZ_OFFICE_CITY", value_C.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString());
            acroFields.SetField("SZ_OFFICE_STATE", value_C.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString());
            acroFields.SetField("SZ_OFFICE_ZIP_CODE", value_C.Tables[0].Rows[0]["SZ_OFFICE_ZIP_CODE"].ToString());
            acroFields.SetField("SZ_BILLING_GROUP", value_C.Tables[0].Rows[0]["SZ_BILLING_GROUP"].ToString());
            acroFields.SetField("SZ_BILLING_ADDRESS", value_C.Tables[0].Rows[0]["SZ_BILLING_ADDRESS"].ToString());
            acroFields.SetField("SZ_BILLING_CITY", value_C.Tables[0].Rows[0]["SZ_BILLING_CITY"].ToString());
            acroFields.SetField("SZ_BILLING_STATE", value_C.Tables[0].Rows[0]["SZ_BILLING_STATE"].ToString());
            acroFields.SetField("SZ_BILLING_ZIP_CODE", value_C.Tables[0].Rows[0]["SZ_BILLING_ZIP_CODE"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE", value_C.Tables[0].Rows[0]["SZ_OFFICE_PHONE"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE", value_C.Tables[0].Rows[0]["SZ_BILLING_PHONE"].ToString());
            acroFields.SetField("SZ_NPI", value_C.Tables[0].Rows[0]["SZ_NPI"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY_CODE", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY_CODE"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY_ADDRESS", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY_ADDRESS"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS_CODE", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_CODE"].ToString());
            DataSet service_C = this.GetService_C42(BillId,conn);
            for (int i = 0; i < service_C.Tables[0].Rows.Count; i++)
            {
                acroFields.SetField("SZ_FROM_MM_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["MONTH"].ToString());
                acroFields.SetField("SZ_FROM_DD_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["DAY"].ToString());
                acroFields.SetField("SZ_FROM_YY_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["YEAR"].ToString());
                acroFields.SetField("SZ_TO_MM_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["TO_MONTH"].ToString());
                acroFields.SetField("SZ_TO_DD_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["TO_DAY"].ToString());
                acroFields.SetField("SZ_TO_YY_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["TO_YEAR"].ToString());
                acroFields.SetField("SZ_CPT_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString());
                acroFields.SetField("SZ_CHARGES_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["FL_AMOUNT"].ToString());
                acroFields.SetField("SZ_DAYS_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["I_UNIT"].ToString());
                acroFields.SetField("SZ_ZIP_CODE_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["ZIP_CODE"].ToString());
                acroFields.SetField("SZ_POS_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["PLACE_OF_SERVICE"].ToString());
                acroFields.SetField("SZ_MODIFER_" + (i + 1).ToString(), service_C.Tables[0].Rows[i]["SZ_MODIFIER"].ToString());

                acroFields.SetField("SZ_DIAG_CODE_" + (i + 1).ToString(), value_C.Tables[0].Rows[0]["SZ_DIAG_CODE_1"].ToString());

                acroFields.SetField("SZ_DIAG_CODE_" + (i + 1).ToString(), value_C.Tables[0].Rows[0]["SZ_DIAG_CODE_1"].ToString());

            }
            acroFields.SetField("SZ_TOTAL_AMOUNT", service_C.Tables[0].Rows[0]["BILL_AMOUNT"].ToString());
            acroFields.SetField("SZ_TOTAL_PAID_AMOUNT", service_C.Tables[0].Rows[0]["PAID_AMOUNT"].ToString());
            acroFields.SetField("SZ_TOTAL_BALANCE_AMOUNT", service_C.Tables[0].Rows[0]["BALANCE"].ToString());
            if (value_C.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "0")
            {
                acroFields.SetField("chk_D_5_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "1")
            {
                acroFields.SetField("chk_d_5_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "0")
            {
                acroFields.SetField("chk_E_1_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "1")
            {
                acroFields.SetField("chk_E_1_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "0")
            {
                acroFields.SetField("chk_E_2_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "1")
            {
                acroFields.SetField("chk_E_2_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "0")
            {
                acroFields.SetField("chk_E_3_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "1")
            {
                acroFields.SetField("chk_E_3_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "2")
            {
                acroFields.SetField("chk_E_3_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_IS_WORKING"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkingYes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_IS_WORKING"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkingNo", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_WORK_RESTRICTION"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkRestrictYes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_WORK_RESTRICTION"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkRestrictNo", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_BENEFIT"].ToString() == "0")
            {
                acroFields.SetField("chk_F_4_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_BENEFIT"].ToString() == "1")
            {
                acroFields.SetField("chk_F_3_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "0")
            {
                acroFields.SetField("chk_F_3_Patient", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "1")
            {
                acroFields.SetField("chk_F_3_Employer", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "2")
            {
                acroFields.SetField("chk_F_3_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "0")
            {
                acroFields.SetField("chk_D_7_withinweek", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "1")
            {
                acroFields.SetField("chk_D_7_1-2wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "2")
            {
                acroFields.SetField("chk_D_7_3-4wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "3")
            {
                acroFields.SetField("chk_D_7_5-6wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "4")
            {
                acroFields.SetField("chk_D_7_7-8wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "5")
            {
                acroFields.SetField("chk_D_7_mnths", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "6")
            {
                acroFields.SetField("chk_D_7_asneeded", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_1-2dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_3-7dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_8-14dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_15+dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_unknown", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_1-2dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_3-7dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_8-14dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_15+dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_unkown", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "5")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_F_2_a", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_F_2_b", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "2")
            {
                acroFields.SetField("chk_F_2_c", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "0")
            {
                acroFields.SetField("chk_F_4_ProvidedServices", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "1")
            {
                acroFields.SetField("chk_F_4_Supervised", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "0")
            {
                acroFields.SetField("CHK_SSN", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "1")
            {
                acroFields.SetField("CHK_EIN", "Yes");
            }
            DataSet dataSet = this.DIAGNOSTIC_TESTS_C42(BillId, conn);
            for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
            {
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "CT Scan" && dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_ctscan", "Yes");
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "EMG/NCS" && dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_emg/ncs", "Yes");
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "MRI (specify):")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_mri", "Yes");
                    }
                    acroFields.SetField("txt_d_5mri", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Labs (specify):")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_labs", "Yes");
                    }
                    acroFields.SetField("txt_d_5labs", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "X-rays (specify):")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_xray", "Yes");
                    }
                    acroFields.SetField("txt_d_5xray", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Other (specify):")
                {
                    if (dataSet.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_otherspecify1", "Yes");
                    }
                    acroFields.SetField("txt_d_5other1", dataSet.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet2 = this.REFERRALS_C42(BillId, conn);
            for (int k = 0; k < dataSet2.Tables[0].Rows.Count; k++)
            {
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Chiropractor" && dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_chiropractor", "Yes");
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Occupational Therapist" && dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_occutherapist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Internist/Family Physician" && dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_internist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Physical Therapist" && dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_phytherapist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Specialist in")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_specialist", "Yes");
                    }
                    acroFields.SetField("txt_d_5specialist", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Other (specify)")
                {
                    if (dataSet2.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_otherspecify2", "Yes");
                    }
                    acroFields.SetField("txt_d_5other2", dataSet2.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet3 = this.WORK_STATUS_C42(BillId, conn);
            for (int l = 0; l < dataSet3.Tables[0].Rows.Count; l++)
            {
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Bending/twisting" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Bending", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Climbing stairs/ladders" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Climbing", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Environmental conditions" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F2Environment", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Kneeling" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Kneeling", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Other (explain):")
                {
                    if (dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_2_OtherSpecify", "Yes");
                    }
                    acroFields.SetField("others", dataSet3.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Lifting" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Lifting", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Operating heavy equipment" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_OprEquip", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Operation of motor vehicles" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_vehical", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Personal protective equipment" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_proctectEquip", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Sitting" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F2cSit", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Standing" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Standing", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Use of public transportation" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Pubtransport", "Yes");
                }
                if (dataSet3.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Use of upper extremities" && dataSet3.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_UpperExtremist", "Yes");
                }
            }
            acroFields.SetField("SZ_DIAGNOSIS_TREATEMENT_RENDERED", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_TREATEMENT_RENDERED"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS_TREATEMENT_RENDERED_1", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_TREATEMENT_RENDERED_1"].ToString());
            acroFields.SetField("SZ_NATURE_OF_INJURY", value_C.Tables[0].Rows[0]["SZ_NATURE_OF_INJURY"].ToString());
            acroFields.SetField("SZ_NATURE_OF_INJURY_1", value_C.Tables[0].Rows[0]["SZ_NATURE_OF_INJURY_1"].ToString());
            acroFields.SetField("SZ_AFFECTED_BY_INJURY", value_C.Tables[0].Rows[0]["SZ_AFFECTED_BY_INJURY"].ToString());
            acroFields.SetField("SZ_CHANGES_IN_TREATMENT_PLAN", value_C.Tables[0].Rows[0]["SZ_CHANGES_IN_TREATMENT_PLAN"].ToString());
            acroFields.SetField("SZ_TREATEMENT_RENDERED", value_C.Tables[0].Rows[0]["SZ_TREATEMENT_RENDERED"].ToString());
            acroFields.SetField("SZ_TREATEMENT_RENDERED_1", value_C.Tables[0].Rows[0]["SZ_TREATEMENT_RENDERED_1"].ToString());
            acroFields.SetField("SZ_APPOINTMENT_MONTH", value_C.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString());
            acroFields.SetField("FT_TEMPORARY_IMPAIRMENT_PERCENTAGE", value_C.Tables[0].Rows[0]["FT_TEMPORARY_IMPAIRMENT_PERCENTAGE"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS", value_C.Tables[0].Rows[0]["SZ_TEST_RESULTS"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS_1", value_C.Tables[0].Rows[0]["SZ_TEST_RESULTS_1"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION_0", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION0"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATIONMM", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATIONMM"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTIONMM", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTIONMM"].ToString());
            acroFields.SetField("SZ_QUANTIFY_LIMITATION", value_C.Tables[0].Rows[0]["SZ_QUANTIFY_LIMITATION"].ToString());
            acroFields.SetField("Text156", value_C.Tables[0].Rows[0]["Text156"].ToString());
            acroFields.SetField("SZ_BOARD_PROVIDER_NAME", value_C.Tables[0].Rows[0]["SZ_BOARD_PROVIDER_NAME"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value_C.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("CUR_MONTH", value_C.Tables[0].Rows[0]["CUR_MONTH"].ToString());
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC40Part1(string CompnayId, string CaseId, string BillId, string CompanyName)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P1"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value = this.GetValue(BillId);
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_MI", value.Tables[0].Rows[0]["SZ_MI"].ToString());
            acroFields.SetField("SZ_SOCIAL_SECURITY_NO", value.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_HOME_PHONE1", value.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE1"].ToString());
            acroFields.SetField("SZ_PATIENT_HOME_PHONE2", value.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE2"].ToString());
            acroFields.SetField("SZ_WCB_NO", value.Tables[0].Rows[0]["SZ_WCB_NO"].ToString());
            acroFields.SetField("SZ_CARRIER_CASE_NO", value.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_FULL_ADDRESS", value.Tables[0].Rows[0]["SZ_PATIENT_FULL_ADDRESS"].ToString());
            acroFields.SetField("SZ_PATIENT_CITY", value.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
            acroFields.SetField("SZ_PATIENT_STATE", value.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
            acroFields.SetField("SZ_PATIENT_ZIP", value.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_DD", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_MM", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_YY", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString());
            }
            if (value.Tables[0].Rows[0]["DOB_DD"].ToString() != "0")
            {
                acroFields.SetField("DOB_DD", value.Tables[0].Rows[0]["DOB_DD"].ToString());
            }
            if (value.Tables[0].Rows[0]["DOB_MM"].ToString() != "0")
            {
                acroFields.SetField("DOB_MM", value.Tables[0].Rows[0]["DOB_MM"].ToString());
            }
            if (value.Tables[0].Rows[0]["DOB_YY"].ToString() != "0")
            {
                acroFields.SetField("DOB_YY", value.Tables[0].Rows[0]["DOB_YY"].ToString());
            }
            acroFields.SetField("SZ_PATIENT_ACCOUNT_NO", value.Tables[0].Rows[0]["SZ_PATIENT_ACCOUNT_NO"].ToString());
            acroFields.SetField("SZ_JOB_TITLE", value.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString());
            acroFields.SetField("SZ_WORK_ACTIVITIES", value.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES"].ToString());
            acroFields.SetField("SZ_EMPLOYER_NAME", value.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString());
            acroFields.SetField("SZ_EMPLOYER_PHONE1", value.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE1"].ToString());
            acroFields.SetField("SZ_EMPLOYER_PHONE2", value.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE2"].ToString());
            acroFields.SetField("SZ_EMPLOYER_ADDRESS", value.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString());
            acroFields.SetField("SZ_EMPLOYER_CITY", value.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString());
            acroFields.SetField("SZ_EMPLOYER_STATE", value.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString());
            acroFields.SetField("SZ_EMPLOYER_ZIP", value.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString());
            acroFields.SetField("SZ_DOCTOR_NAME", value.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
            acroFields.SetField("SZ_WCB_AUTHORIZATION", value.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString());
            acroFields.SetField("SZ_WCB_RATING_CODE", value.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString());
            acroFields.SetField("SZ_OFFICE_ADDRESS", value.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
            acroFields.SetField("SZ_OFFICE_CITY", value.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString());
            acroFields.SetField("SZ_OFFICE_STATE", value.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString());
            acroFields.SetField("SZ_OFFICE_ZIP", value.Tables[0].Rows[0]["SZ_OFFICE_ZIP"].ToString());
            acroFields.SetField("SZ_BILLING_GROUP_NAME", value.Tables[0].Rows[0]["SZ_BILLING_GROUP_NAME"].ToString());
            acroFields.SetField("SZ_BILLING_ADDRESS", value.Tables[0].Rows[0]["SZ_BILLING_ADDRESS"].ToString());
            acroFields.SetField("SZ_BILLING_CITY", value.Tables[0].Rows[0]["SZ_BILLING_CITY"].ToString());
            acroFields.SetField("SZ_BILLING_STATE", value.Tables[0].Rows[0]["SZ_BILLING_STATE"].ToString());
            acroFields.SetField("SZ_BILLING_ZIP", value.Tables[0].Rows[0]["SZ_BILLING_ZIP"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE1", value.Tables[0].Rows[0]["SZ_OFFICE_PHONE1"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE2", value.Tables[0].Rows[0]["SZ_OFFICE_PHONE2"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE1", value.Tables[0].Rows[0]["SZ_BILLING_PHONE1"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE2", value.Tables[0].Rows[0]["SZ_BILLING_PHONE2"].ToString());
            acroFields.SetField("SZ_NPI", value.Tables[0].Rows[0]["SZ_NPI"].ToString());
            acroFields.SetField("SZ_FEDERAL_TAX_ID", value.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
            acroFields.SetField("SZ_INSURANCE_NAME", value.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
            acroFields.SetField("SZ_CARRIER_CODE", value.Tables[0].Rows[0]["SZ_CARRIER_CODE"].ToString());
            acroFields.SetField("SZ_INSURANCE_STREET", value.Tables[0].Rows[0]["SZ_INSURANCE_STREET"].ToString());
            acroFields.SetField("SZ_INSURANCE_CITY", value.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString());
            acroFields.SetField("SZ_INSURANCE_STATE", value.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString());
            acroFields.SetField("SZ_INSURANCE_ZIP", value.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS", value.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString());
            if (value.Tables[0].Rows[0]["I_GENDER"].ToString() == "0")
            {
                acroFields.SetField("form1[0].P1[0].chkMale", "1");
            }
            if (value.Tables[0].Rows[0]["I_GENDER"].ToString() == "1")
            {
                acroFields.SetField("form1[0].P1[0].chkFemale", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "0")
            {
                acroFields.SetField("form1[0].P1[0].chkPhysician", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "1")
            {
                acroFields.SetField("form1[0].P1[0].chkPodiatrist", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "2")
            {
                acroFields.SetField("form1[0].P1[0].chkChiropractor", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "0")
            {
                acroFields.SetField("form1[0].P1[0].chk_SSN", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "1")
            {
                acroFields.SetField("form1[0].P1[0].chk_EIN", "1");
            }
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC40Part1(string CompnayId, string CaseId, string BillId, string CompanyName, ServerConnection conn)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P1"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value = this.GetValue(BillId, conn);
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_MI", value.Tables[0].Rows[0]["SZ_MI"].ToString());
            acroFields.SetField("SZ_SOCIAL_SECURITY_NO", value.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_HOME_PHONE1", value.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE1"].ToString());
            acroFields.SetField("SZ_PATIENT_HOME_PHONE2", value.Tables[0].Rows[0]["SZ_PATIENT_HOME_PHONE2"].ToString());
            acroFields.SetField("SZ_WCB_NO", value.Tables[0].Rows[0]["SZ_WCB_NO"].ToString());
            acroFields.SetField("SZ_CARRIER_CASE_NO", value.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_FULL_ADDRESS", value.Tables[0].Rows[0]["SZ_PATIENT_FULL_ADDRESS"].ToString());
            acroFields.SetField("SZ_PATIENT_CITY", value.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString());
            acroFields.SetField("SZ_PATIENT_STATE", value.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString());
            acroFields.SetField("SZ_PATIENT_ZIP", value.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString());
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_DD", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_MM", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_YY", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString());
            }
            if (value.Tables[0].Rows[0]["DOB_DD"].ToString() != "0")
            {
                acroFields.SetField("DOB_DD", value.Tables[0].Rows[0]["DOB_DD"].ToString());
            }
            if (value.Tables[0].Rows[0]["DOB_MM"].ToString() != "0")
            {
                acroFields.SetField("DOB_MM", value.Tables[0].Rows[0]["DOB_MM"].ToString());
            }
            if (value.Tables[0].Rows[0]["DOB_YY"].ToString() != "0")
            {
                acroFields.SetField("DOB_YY", value.Tables[0].Rows[0]["DOB_YY"].ToString());
            }
            acroFields.SetField("SZ_PATIENT_ACCOUNT_NO", value.Tables[0].Rows[0]["SZ_PATIENT_ACCOUNT_NO"].ToString());
            acroFields.SetField("SZ_JOB_TITLE", value.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString());
            acroFields.SetField("SZ_WORK_ACTIVITIES", value.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES"].ToString());
            acroFields.SetField("SZ_EMPLOYER_NAME", value.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString());
            acroFields.SetField("SZ_EMPLOYER_PHONE1", value.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE1"].ToString());
            acroFields.SetField("SZ_EMPLOYER_PHONE2", value.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE2"].ToString());
            acroFields.SetField("SZ_EMPLOYER_ADDRESS", value.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString());
            acroFields.SetField("SZ_EMPLOYER_CITY", value.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString());
            acroFields.SetField("SZ_EMPLOYER_STATE", value.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString());
            acroFields.SetField("SZ_EMPLOYER_ZIP", value.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString());
            acroFields.SetField("SZ_DOCTOR_NAME", value.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
            acroFields.SetField("SZ_WCB_AUTHORIZATION", value.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString());
            acroFields.SetField("SZ_WCB_RATING_CODE", value.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString());
            acroFields.SetField("SZ_OFFICE_ADDRESS", value.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
            acroFields.SetField("SZ_OFFICE_CITY", value.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString());
            acroFields.SetField("SZ_OFFICE_STATE", value.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString());
            acroFields.SetField("SZ_OFFICE_ZIP", value.Tables[0].Rows[0]["SZ_OFFICE_ZIP"].ToString());
            acroFields.SetField("SZ_BILLING_GROUP_NAME", value.Tables[0].Rows[0]["SZ_BILLING_GROUP_NAME"].ToString());
            acroFields.SetField("SZ_BILLING_ADDRESS", value.Tables[0].Rows[0]["SZ_BILLING_ADDRESS"].ToString());
            acroFields.SetField("SZ_BILLING_CITY", value.Tables[0].Rows[0]["SZ_BILLING_CITY"].ToString());
            acroFields.SetField("SZ_BILLING_STATE", value.Tables[0].Rows[0]["SZ_BILLING_STATE"].ToString());
            acroFields.SetField("SZ_BILLING_ZIP", value.Tables[0].Rows[0]["SZ_BILLING_ZIP"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE1", value.Tables[0].Rows[0]["SZ_OFFICE_PHONE1"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE2", value.Tables[0].Rows[0]["SZ_OFFICE_PHONE2"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE1", value.Tables[0].Rows[0]["SZ_BILLING_PHONE1"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE2", value.Tables[0].Rows[0]["SZ_BILLING_PHONE2"].ToString());
            acroFields.SetField("SZ_NPI", value.Tables[0].Rows[0]["SZ_NPI"].ToString());
            acroFields.SetField("SZ_FEDERAL_TAX_ID", value.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
            acroFields.SetField("SZ_INSURANCE_NAME", value.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString());
            acroFields.SetField("SZ_CARRIER_CODE", value.Tables[0].Rows[0]["SZ_CARRIER_CODE"].ToString());
            acroFields.SetField("SZ_INSURANCE_STREET", value.Tables[0].Rows[0]["SZ_INSURANCE_STREET"].ToString());
            acroFields.SetField("SZ_INSURANCE_CITY", value.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString());
            acroFields.SetField("SZ_INSURANCE_STATE", value.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString());
            acroFields.SetField("SZ_INSURANCE_ZIP", value.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS", value.Tables[0].Rows[0]["SZ_DIGNOSIS"].ToString());
            if (value.Tables[0].Rows[0]["I_GENDER"].ToString() == "0")
            {
                acroFields.SetField("form1[0].P1[0].chkMale", "1");
            }
            if (value.Tables[0].Rows[0]["I_GENDER"].ToString() == "1")
            {
                acroFields.SetField("form1[0].P1[0].chkFemale", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "0")
            {
                acroFields.SetField("form1[0].P1[0].chkPhysician", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "1")
            {
                acroFields.SetField("form1[0].P1[0].chkPodiatrist", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_DOCTOR_TYPE"].ToString() == "2")
            {
                acroFields.SetField("form1[0].P1[0].chkChiropractor", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "0")
            {
                acroFields.SetField("form1[0].P1[0].chk_SSN", "1");
            }
            if (value.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "1")
            {
                acroFields.SetField("form1[0].P1[0].chk_EIN", "1");
            }
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC40Part2(string CompnayId, string CaseId, string BillId, string CompanyName)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P2"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value = this.GetValue(BillId);
            if (value.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString() != "")
            {
                float[] fieldPositions = acroFields.GetFieldPositions("SZ_OFFICE_NAME");
                if (fieldPositions == null || fieldPositions.Length <= 0)
                {
                    throw new ApplicationException("Error locating field");
                }
                float absoluteX = 139.5f;
                float num = 93.75f;
                float num2 = 370.5f;
                float num3 = 77.25f;
                Image instance = Image.GetInstance(value.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString());
                instance.ScaleToFit(num + 25f, num3 + 25f);
                instance.SetAbsolutePosition(absoluteX, num2 - 285f);
                PdfContentByte overContent = pdfStamper.GetOverContent(3);
                overContent.AddImage(instance);
            }
            else
            {
                acroFields.SetField("SZ_OFFICE_NAME", value.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString());
            }
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_MI", value.Tables[0].Rows[0]["SZ_MI"].ToString());
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_DD", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_MM", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_YY", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString());
            }
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL"].ToString());
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL_1", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL_1"].ToString());
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL_2", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL_2"].ToString());
            acroFields.SetField("SZ_INJURY_LEARN_SOURCE_DESCRIPTION", value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION", value.Tables[0].Rows[0]["SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION"].ToString());
            acroFields.SetField("DT_PREVIOUSLY_TREATED_DATE", value.Tables[0].Rows[0]["DT_PREVIOUSLY_TREATED_DATE"].ToString());
            acroFields.SetField("DT_DATE_OF_EXAMINATION", value.Tables[0].Rows[0]["DT_DATE_OF_EXAMINATION"].ToString());
            acroFields.SetField("SZ_DIAGNOSTIC_TEST", value.Tables[0].Rows[0]["SZ_DIAGNOSTIC_TEST"].ToString());
            acroFields.SetField("SZ_DIAGNOSTIC_TEST_1", value.Tables[0].Rows[0]["SZ_DIAGNOSTIC_TEST_1"].ToString());
            acroFields.SetField("SZ_TREATMENT", value.Tables[0].Rows[0]["SZ_TREATMENT"].ToString());
            acroFields.SetField("SZ_TREATMENT_1", value.Tables[0].Rows[0]["SZ_TREATMENT_1"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY_1", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY_1"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY_2", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY_2"].ToString());
            acroFields.SetField("SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION", value.Tables[0].Rows[0]["SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1", value.Tables[0].Rows[0]["SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1"].ToString());
            acroFields.SetField("FT_TEMPORARY_IMPAIRMENT_PERCENTAGE", value.Tables[0].Rows[0]["FT_TEMPORARY_IMPAIRMENT_PERCENTAGE"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS", value.Tables[0].Rows[0]["SZ_TEST_RESULTS"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS_1", value.Tables[0].Rows[0]["SZ_TEST_RESULTS_1"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT_1", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT_1"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT_3", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT_3"].ToString());
            acroFields.SetField("SZ_MEDICATIONS_PRESCRIBED", value.Tables[0].Rows[0]["SZ_MEDICATIONS_PRESCRIBED"].ToString());
            acroFields.SetField("SZ_MEDICATIONS_ADVISED", value.Tables[0].Rows[0]["SZ_MEDICATIONS_ADVISED"].ToString());
            acroFields.SetField("SZ_MEDICATION_RESTRICTIONS_DESCRIPTION", value.Tables[0].Rows[0]["SZ_MEDICATION_RESTRICTIONS_DESCRIPTION"].ToString());
            acroFields.SetField("Text226", value.Tables[0].Rows[0]["Text226"].ToString());
            acroFields.SetField("SZ_APPOINTMENT", value.Tables[0].Rows[0]["SZ_APPOINTMENT"].ToString());
            acroFields.SetField("SZ_VARIANCE_GUIDELINES", value.Tables[0].Rows[0]["SZ_VARIANCE_GUIDELINES"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_DD", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_DD"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_MM", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_MM"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_YY", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_YY"].ToString());
            acroFields.SetField("SZ_QUANIFY_LIMITATION", value.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("SZ_READINGDOCTOR", value.Tables[0].Rows[0]["SZ_READINGDOCTOR"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("txtTodayDate", value.Tables[0].Rows[0]["txtTodayDate"].ToString());
            acroFields.SetField("txtTodayDate_DD", value.Tables[0].Rows[0]["txtTodayDate_DD"].ToString());
            acroFields.SetField("txtTodayDate_MM", value.Tables[0].Rows[0]["txtTodayDate_MM"].ToString());
            acroFields.SetField("txtTodayDate_YY", value.Tables[0].Rows[0]["txtTodayDate_YY"].ToString());
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Patient", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "1")
            {
                acroFields.SetField("chk_H_MedicalRecords", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "2")
            {
                acroFields.SetField("chk_H_Other", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ANOTHER_HEALTH_PROVIDER"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ANOTHER_HEALTH_PROVIDER"].ToString() == "1")
            {
                acroFields.SetField("chk_H_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_PREVIOUSLY_TREATED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_4_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_PREVIOUSLY_TREATED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_4_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_AFFECT_TREATMENT_PROGNOSIS"].ToString() == "0")
            {
                acroFields.SetField("chk_F_8_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_AFFECT_TREATMENT_PROGNOSIS"].ToString() == "1")
            {
                acroFields.SetField("chk_F_8_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "0")
            {
                acroFields.SetField("chk_G_1_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "1")
            {
                acroFields.SetField("chk_G_1_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "0")
            {
                acroFields.SetField("chk_G_2_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "1")
            {
                acroFields.SetField("chk_G_2_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "0")
            {
                acroFields.SetField("chk_G_3_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "1")
            {
                acroFields.SetField("chk_G_3_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "2")
            {
                acroFields.SetField("chk_G_3_NA", "1");
            }
            if (value.Tables[0].Rows[0]["BT_MEDICATION_RESTRICTIONS"].ToString() == "False")
            {
                acroFields.SetField("chk_H_2_None", "1");
            }
            if (value.Tables[0].Rows[0]["BT_MEDICATION_RESTRICTIONS"].ToString() == "True")
            {
                acroFields.SetField("chk_H_2_Other", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_3_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_3_No", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "0")
            {
                acroFields.SetField("chk_H_5_WithinWeek", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "1")
            {
                acroFields.SetField("chk_H_5_1_2_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "2")
            {
                acroFields.SetField("chk_H_5_3_4_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "3")
            {
                acroFields.SetField("chk_H_5_5_6_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "4")
            {
                acroFields.SetField("chk_H_5_7_8_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "5")
            {
                acroFields.SetField("chk_H_5_Month", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "6")
            {
                acroFields.SetField("chk_H_5_Return", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ADHERE_NEWYORK_TREATMENT"].ToString() == "0")
            {
                acroFields.SetField("chk_H_8_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ADHERE_NEWYORK_TREATMENT"].ToString() == "1")
            {
                acroFields.SetField("chk_H_8_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_MISSED_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_MISSED_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_CUR_WORKING"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_PatientWorkingYes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_CUR_WORKING"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_PatientWorkingNo", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_WORK_TYPE"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_PatientReturnYes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_WORK_TYPE"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_PatientReturnNo", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_I_2_a", "1");
                acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION"].ToString());
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_I_2_b", "1");
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_DD"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_DD", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_DD"].ToString());
                }
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_MM"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_MM", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_MM"].ToString());
                }
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_YY"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_YY", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_YY"].ToString());
                }
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "2")
            {
                acroFields.SetField("chk_I_2_c", "1");
                if (value.Tables[0].Rows[0]["Text249_DD"].ToString() != "0")
                {
                    acroFields.SetField("Text249_DD", value.Tables[0].Rows[0]["Text249_DD"].ToString());
                }
                if (value.Tables[0].Rows[0]["Text249_MM"].ToString() != "0")
                {
                    acroFields.SetField("Text249_MM", value.Tables[0].Rows[0]["Text249_MM"].ToString());
                }
                if (value.Tables[0].Rows[0]["Text249_YY"].ToString() != "0")
                {
                    acroFields.SetField("Text249_YY", value.Tables[0].Rows[0]["Text249_YY"].ToString());
                }
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_H_2_1_2_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_H_2_3_7_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_H_2_8_14_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_H_2_15P_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_H_2_Unknown", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "5")
            {
                acroFields.SetField("chk_H_2_NA", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "0")
            {
                acroFields.SetField("chk_H_3_Patient", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "1")
            {
                acroFields.SetField("chk_H_3_PatientsEmployer", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "2")
            {
                acroFields.SetField("chk_H_4_NA", "1");
            }
            if (value.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Provider_1", "1");
            }
            if (value.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_Provider_2", "1");
            }
            DataSet dataSet = this.PATIENT_COMPLAINTS(BillId);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Numbness/Tingling")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Numbness", "1");
                    }
                    acroFields.SetField("txt_F_Numbness", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Swelling")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Swelling", "1");
                    }
                    acroFields.SetField("txt_F_Swelling", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Pain")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Pain", "1");
                    }
                    acroFields.SetField("txt_F_Pain", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Weakness")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Weakness", "1");
                    }
                    acroFields.SetField("txt_F_Weakness", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Stiffness")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Stiffness", "1");
                    }
                    acroFields.SetField("txt_F_Stiffness", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Other(Specify)")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Other", "1");
                    }
                    acroFields.SetField("txt_F_Other", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet2 = this.Patientinjury(BillId);
            for (int j = 0; j < dataSet2.Tables[0].Rows.Count; j++)
            {
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Abrasion")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Abrasion", "1");
                    }
                    acroFields.SetField("txt_F_3_Abrasion", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Infectious Disease")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_InfectiousDisease", "1");
                    }
                    acroFields.SetField("txt_F_3_InfectiousDisease", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Amputation")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Amputation", "1");
                    }
                    acroFields.SetField("txt_F_3_Amputation", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Inhalation Exposure")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Inhalation_Exposure", "1");
                    }
                    acroFields.SetField("txt_F_3_Inhalation_Exposure", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Avulsion")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Avulsion", "1");
                    }
                    acroFields.SetField("txt_F_3_Avulsion", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Laceration")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Laceration", "1");
                    }
                    acroFields.SetField("txt_F_3_Laceration", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Bite")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Bite", "1");
                    }
                    acroFields.SetField("txt_F_3_Bite", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Needle Stick")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_NeedleStick", "1");
                    }
                    acroFields.SetField("txt_F_3_NeedleStick", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Burn")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Burn", "1");
                    }
                    acroFields.SetField("txt_F_3_Burn", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Poisoning/Toxic Effects")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Poisoning", "1");
                    }
                    acroFields.SetField("txt_F_3_Poisoning", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Contusion/Hematoma")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Contusion", "1");
                    }
                    acroFields.SetField("txt_F_3_Contusion", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Psychological")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Psychological", "1");
                    }
                    acroFields.SetField("txt_F_3_Psychological", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Crush Injury")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_CurshInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_CurshInjury", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Puncture Wound")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_PuntureWound", "1");
                    }
                    acroFields.SetField("txt_F_3_PuntureWound", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Dermatitis")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("form1[0].P2[0].chk_F_3_Dermatitis", "Yes");
                    }
                    acroFields.SetField("txt_F_3_Dermatitis", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Repetitive Strain Injury")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_RepetitiveStrainInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_RepetitiveStrainInjury", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Dislocation")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Dislocation", "1");
                    }
                    acroFields.SetField("txt_F_3_Dislocation", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Spinal Cord Injury")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_SpinalCordInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_SpinalCordInjury", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Fracture")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Fracture", "1");
                    }
                    acroFields.SetField("txt_F_3_Fracture", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Sprain/Strain")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Sprain", "1");
                    }
                    acroFields.SetField("txt_F_3_Sprain", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Hearing Loss")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_HearingLoss", "1");
                    }
                    acroFields.SetField("txt_F_3_HearingLoss", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Torn Ligament Tendon or Muscle")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Torn", "1");
                    }
                    acroFields.SetField("txt_F_3_Torn", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Hernia")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Hernia", "1");
                    }
                    acroFields.SetField("txt_F_3_Hernia", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Vision Loss")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_VisionLoss", "1");
                    }
                    acroFields.SetField("txt_F_3_VisionLoss", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Other(Specify) :")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Other", "1");
                    }
                    acroFields.SetField("txt_F_3_Other", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet3 = this.PatientPhysicalExam(BillId);
            for (int k = 0; k < dataSet3.Tables[0].Rows.Count; k++)
            {
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "None at present" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_7_None", "1");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Bruising")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Bruising", "1");
                    }
                    acroFields.SetField("txt_F_4_Bruising", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Burns")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Burns", "1");
                    }
                    acroFields.SetField("txt_F_4_Burns", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Crepitation")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Crepitation", "1");
                    }
                    acroFields.SetField("txt_F_4_Crepitation", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Deformity")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Deformity", "1");
                    }
                    acroFields.SetField("txt_F_4_Deformity", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Edema")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Edema", "1");
                    }
                    acroFields.SetField("txt_F_4_Edema", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Hematoma/Lump/Swelling")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Hematoma", "1");
                    }
                    acroFields.SetField("txt_F_4_Hematoma", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Joint Effusion")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_JointEffusion", "1");
                    }
                    acroFields.SetField("txt_F_4_JointEffusion", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Laceration/Sutures")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Laceration", "1");
                    }
                    acroFields.SetField("txt_F_4_Laceration", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Pain/Tenderness")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Pain", "1");
                    }
                    acroFields.SetField("txt_F_4_Pain", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Scar")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Scar", "1");
                    }
                    acroFields.SetField("txt_F_4_Scar", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Other(Specify) ::")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_OtherFindings", "1");
                    }
                    acroFields.SetField("txt_F_4_OtherFindings", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Neuromuscular Findings:" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_4_Neuromuscular", "1");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Abnormal/Restricted ROM" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_4_Abnormal", "1");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Active ROM")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_ActiveROM", "1");
                    }
                    acroFields.SetField("txt_F_4_ActiveROM", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Passive ROM")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_PassiveROM", "1");
                    }
                    acroFields.SetField("txt_F_4_PassiveROM", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Gait")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Gait", "1");
                    }
                    acroFields.SetField("txt_F_4_Gait", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Palpable Muscle Spasm")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Palpable", "1");
                    }
                    acroFields.SetField("txt_F_4_Palpable", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Reflexes")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Reflexes", "1");
                    }
                    acroFields.SetField("txt_F_4_Reflexes", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Sensation")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Sensation", "1");
                    }
                    acroFields.SetField("txt_F_4_Sensation", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Strength (Weakness)")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Strength", "1");
                    }
                    acroFields.SetField("txt_F_4_Strength", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Wasting/Muscle Atrophy")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Wasting", "1");
                    }
                    acroFields.SetField("txt_F_4_Wasting", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet4 = this.DiagnosticTests(BillId);
            for (int l = 0; l < dataSet4.Tables[0].Rows.Count; l++)
            {
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "CT Scan" && dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_CTScan", "1");
                }
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "EMG/NCS" && dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_EMGNCS", "1");
                }
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "MRI (specify):")
                {
                    if (dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_MRI", "1");
                    }
                    acroFields.SetField("txt_H_3_MRI", dataSet4.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Labs (specify):")
                {
                    if (dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Labs", "1");
                    }
                    acroFields.SetField("txt_H_3_Labs", dataSet4.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "X-rays (specify):")
                {
                    if (dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_XRay", "1");
                    }
                    acroFields.SetField("txt_H_3_XRay", dataSet4.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Other (specify):")
                {
                    if (dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Left_Other", "1");
                    }
                    acroFields.SetField("txt_H_3_Left_Other", dataSet4.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet5 = this.Referrals(BillId);
            for (int m = 0; m < dataSet5.Tables[0].Rows.Count; m++)
            {
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Chiropractor" && dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("form1[0].P4[0].chk_H_3_Chiropractor", "1");
                }
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Internist/Family Physician" && dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("form1[0].P4[0].chk_H_3_Interist", "1");
                }
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Occupational Therapist" && dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("form1[0].P4[0].chk_H_3_Occupational", "1");
                }
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Physical Therapist" && dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("form1[0].P4[0].chk_H_3_Physical", "1");
                }
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Specialist in")
                {
                    if (dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("form1[0].P4[0].chk_H_3_Specialist", "1");
                    }
                    acroFields.SetField("txt_H_3_Specialist", dataSet5.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Other (specify)")
                {
                    if (dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("form1[0].P4[0].chk_H_3_Right_Other", "1");
                    }
                    acroFields.SetField("txt_H_3_Right_Other", dataSet5.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet6 = this.WorkStatus(BillId);
            for (int n = 0; n < dataSet6.Tables[0].Rows.Count; n++)
            {
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Bending/twisting" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Bending", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Lifting" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Lifting", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Sitting" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Sitting", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Climbing stairs/ladders" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Climbing", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Operating heavy equipment" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_OperatingHeavy", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Standing" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Standing", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Environmental conditions" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_EnvironmentalCond", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Operation of motor vehicles" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_OpMotorVehicle", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Use of public transportation" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_UsePublicTrans", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Kneeling" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Kneeling", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Personal protective equipment" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_PersonalProtectiveEq", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Use of upper extremities" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_UseOfUpperExtremeities", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Other (explain):")
                {
                    if (dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_I_2_C_Other", "1");
                    }
                    acroFields.SetField("form1[0].P4[0].SZ_LIMITATION_DURATION", dataSet6.Tables[0].Rows[n]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet7 = this.AssistiveDevices(BillId);
            for (int num4 = 0; num4 < dataSet7.Tables[0].Rows.Count; num4++)
            {
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Cane" && dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Cane", "1");
                }
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Crutches" && dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Crutches", "1");
                }
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Orthotics" && dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Orthotics", "1");
                }
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Walker" && dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Walker", "1");
                }
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Wheelchair" && dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_WheelChair", "1");
                }
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Other-(specify):")
                {
                    if (dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_4_Other", "1");
                    }
                    acroFields.SetField("txt_H_4_Other", dataSet7.Tables[0].Rows[num4]["SZ_DESCRIPTION"].ToString());
                }
            }
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC40Part2(string CompnayId, string CaseId, string BillId, string CompanyName, ServerConnection conn)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_P2"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value = this.GetValue(BillId, conn);
            if (value.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString() != "")
            {
                float[] fieldPositions = acroFields.GetFieldPositions("SZ_OFFICE_NAME");
                if (fieldPositions == null || fieldPositions.Length <= 0)
                {
                    throw new ApplicationException("Error locating field");
                }
                float absoluteX = 139.5f;
                float num = 93.75f;
                float num2 = 370.5f;
                float num3 = 77.25f;
                Image instance = Image.GetInstance(value.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString());
                instance.ScaleToFit(num + 25f, num3 + 25f);
                instance.SetAbsolutePosition(absoluteX, num2 - 285f);
                PdfContentByte overContent = pdfStamper.GetOverContent(3);
                overContent.AddImage(instance);
            }
            else
            {
                acroFields.SetField("SZ_OFFICE_NAME", value.Tables[0].Rows[0]["SZ_OFFICE_NAME"].ToString());
            }
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_MI", value.Tables[0].Rows[0]["SZ_MI"].ToString());
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_DD", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_DD"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_MM", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_MM"].ToString());
            }
            if (value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString() != "0")
            {
                acroFields.SetField("DT_DATE_OF_ACCIDENT_YY", value.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT_YY"].ToString());
            }
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL"].ToString());
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL_1", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL_1"].ToString());
            acroFields.SetField("SZ_INJURY_ILLNESS_DETAIL_2", value.Tables[0].Rows[0]["SZ_INJURY_ILLNESS_DETAIL_2"].ToString());
            acroFields.SetField("SZ_INJURY_LEARN_SOURCE_DESCRIPTION", value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION", value.Tables[0].Rows[0]["SZ_ANOTHER_HEALTH_PROVIDER_DESCRIPTION"].ToString());
            acroFields.SetField("DT_PREVIOUSLY_TREATED_DATE", value.Tables[0].Rows[0]["DT_PREVIOUSLY_TREATED_DATE"].ToString());
            acroFields.SetField("DT_DATE_OF_EXAMINATION", value.Tables[0].Rows[0]["DT_DATE_OF_EXAMINATION"].ToString());
            acroFields.SetField("SZ_DIAGNOSTIC_TEST", value.Tables[0].Rows[0]["SZ_DIAGNOSTIC_TEST"].ToString());
            acroFields.SetField("SZ_DIAGNOSTIC_TEST_1", value.Tables[0].Rows[0]["SZ_DIAGNOSTIC_TEST_1"].ToString());
            acroFields.SetField("SZ_TREATMENT", value.Tables[0].Rows[0]["SZ_TREATMENT"].ToString());
            acroFields.SetField("SZ_TREATMENT_1", value.Tables[0].Rows[0]["SZ_TREATMENT_1"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY_1", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY_1"].ToString());
            acroFields.SetField("SZ_PROGNOSIS_RECOVERY_2", value.Tables[0].Rows[0]["SZ_PROGNOSIS_RECOVERY_2"].ToString());
            acroFields.SetField("SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION", value.Tables[0].Rows[0]["SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1", value.Tables[0].Rows[0]["SZ_AFFECT_TREATMENT_PROGNOSIS_DESCRIPTION_1"].ToString());
            acroFields.SetField("FT_TEMPORARY_IMPAIRMENT_PERCENTAGE", value.Tables[0].Rows[0]["FT_TEMPORARY_IMPAIRMENT_PERCENTAGE"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS", value.Tables[0].Rows[0]["SZ_TEST_RESULTS"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS_1", value.Tables[0].Rows[0]["SZ_TEST_RESULTS_1"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT_1", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT_1"].ToString());
            acroFields.SetField("SZ_PROPOSED_TREATEMENT_3", value.Tables[0].Rows[0]["SZ_PROPOSED_TREATEMENT_3"].ToString());
            acroFields.SetField("SZ_MEDICATIONS_PRESCRIBED", value.Tables[0].Rows[0]["SZ_MEDICATIONS_PRESCRIBED"].ToString());
            acroFields.SetField("SZ_MEDICATIONS_ADVISED", value.Tables[0].Rows[0]["SZ_MEDICATIONS_ADVISED"].ToString());
            acroFields.SetField("SZ_MEDICATION_RESTRICTIONS_DESCRIPTION", value.Tables[0].Rows[0]["SZ_MEDICATION_RESTRICTIONS_DESCRIPTION"].ToString());
            acroFields.SetField("Text226", value.Tables[0].Rows[0]["Text226"].ToString());
            acroFields.SetField("SZ_APPOINTMENT", value.Tables[0].Rows[0]["SZ_APPOINTMENT"].ToString());
            acroFields.SetField("SZ_VARIANCE_GUIDELINES", value.Tables[0].Rows[0]["SZ_VARIANCE_GUIDELINES"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_DD", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_DD"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_MM", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_MM"].ToString());
            acroFields.SetField("DT_PATIENT_MISSED_WORK_DATE_YY", value.Tables[0].Rows[0]["DT_PATIENT_MISSED_WORK_DATE_YY"].ToString());
            acroFields.SetField("SZ_QUANIFY_LIMITATION", value.Tables[0].Rows[0]["SZ_QUANIFY_LIMITATION"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("SZ_READINGDOCTOR", value.Tables[0].Rows[0]["SZ_READINGDOCTOR"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("txtTodayDate", value.Tables[0].Rows[0]["txtTodayDate"].ToString());
            acroFields.SetField("txtTodayDate_DD", value.Tables[0].Rows[0]["txtTodayDate_DD"].ToString());
            acroFields.SetField("txtTodayDate_MM", value.Tables[0].Rows[0]["txtTodayDate_MM"].ToString());
            acroFields.SetField("txtTodayDate_YY", value.Tables[0].Rows[0]["txtTodayDate_YY"].ToString());
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Patient", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "1")
            {
                acroFields.SetField("chk_H_MedicalRecords", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_INJURY_LEARN_SOURCE_ID"].ToString() == "2")
            {
                acroFields.SetField("chk_H_Other", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ANOTHER_HEALTH_PROVIDER"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ANOTHER_HEALTH_PROVIDER"].ToString() == "1")
            {
                acroFields.SetField("chk_H_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_PREVIOUSLY_TREATED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_4_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_PREVIOUSLY_TREATED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_4_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_AFFECT_TREATMENT_PROGNOSIS"].ToString() == "0")
            {
                acroFields.SetField("chk_F_8_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_AFFECT_TREATMENT_PROGNOSIS"].ToString() == "1")
            {
                acroFields.SetField("chk_F_8_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "0")
            {
                acroFields.SetField("chk_G_1_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "1")
            {
                acroFields.SetField("chk_G_1_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "0")
            {
                acroFields.SetField("chk_G_2_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "1")
            {
                acroFields.SetField("chk_G_2_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "0")
            {
                acroFields.SetField("chk_G_3_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "1")
            {
                acroFields.SetField("chk_G_3_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "2")
            {
                acroFields.SetField("chk_G_3_NA", "1");
            }
            if (value.Tables[0].Rows[0]["BT_MEDICATION_RESTRICTIONS"].ToString() == "False")
            {
                acroFields.SetField("chk_H_2_None", "1");
            }
            if (value.Tables[0].Rows[0]["BT_MEDICATION_RESTRICTIONS"].ToString() == "True")
            {
                acroFields.SetField("chk_H_2_Other", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_3_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_3_No", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "0")
            {
                acroFields.SetField("chk_H_5_WithinWeek", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "1")
            {
                acroFields.SetField("chk_H_5_1_2_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "2")
            {
                acroFields.SetField("chk_H_5_3_4_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "3")
            {
                acroFields.SetField("chk_H_5_5_6_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "4")
            {
                acroFields.SetField("chk_H_5_7_8_Week", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "5")
            {
                acroFields.SetField("chk_H_5_Month", "1");
            }
            if (value.Tables[0].Rows[0]["SZ_APPOINTMENT_MONTH"].ToString() == "6")
            {
                acroFields.SetField("chk_H_5_Return", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ADHERE_NEWYORK_TREATMENT"].ToString() == "0")
            {
                acroFields.SetField("chk_H_8_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_IS_ADHERE_NEWYORK_TREATMENT"].ToString() == "1")
            {
                acroFields.SetField("chk_H_8_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_MISSED_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_Yes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_MISSED_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_No", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_CUR_WORKING"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_PatientWorkingYes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_CUR_WORKING"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_PatientWorkingNo", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_WORK_TYPE"].ToString() == "0")
            {
                acroFields.SetField("chk_I_1_PatientReturnYes", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_WORK_TYPE"].ToString() == "1")
            {
                acroFields.SetField("chk_I_1_PatientReturnNo", "1");
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_I_2_a", "1");
                acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION"].ToString());
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_I_2_b", "1");
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_DD"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_DD", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_DD"].ToString());
                }
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_MM"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_MM", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_MM"].ToString());
                }
                if (value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_YY"].ToString() != "0")
                {
                    acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATION_YY", value.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATION_YY"].ToString());
                }
            }
            if (value.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "2")
            {
                acroFields.SetField("chk_I_2_c", "1");
                if (value.Tables[0].Rows[0]["Text249_DD"].ToString() != "0")
                {
                    acroFields.SetField("Text249_DD", value.Tables[0].Rows[0]["Text249_DD"].ToString());
                }
                if (value.Tables[0].Rows[0]["Text249_MM"].ToString() != "0")
                {
                    acroFields.SetField("Text249_MM", value.Tables[0].Rows[0]["Text249_MM"].ToString());
                }
                if (value.Tables[0].Rows[0]["Text249_YY"].ToString() != "0")
                {
                    acroFields.SetField("Text249_YY", value.Tables[0].Rows[0]["Text249_YY"].ToString());
                }
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_H_2_1_2_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_H_2_3_7_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_H_2_8_14_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_H_2_15P_Days", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_H_2_Unknown", "1");
            }
            if (value.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "5")
            {
                acroFields.SetField("chk_H_2_NA", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "0")
            {
                acroFields.SetField("chk_H_3_Patient", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "1")
            {
                acroFields.SetField("chk_H_3_PatientsEmployer", "1");
            }
            if (value.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "2")
            {
                acroFields.SetField("chk_H_4_NA", "1");
            }
            if (value.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "0")
            {
                acroFields.SetField("chk_H_Provider_1", "1");
            }
            if (value.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "1")
            {
                acroFields.SetField("chk_H_Provider_2", "1");
            }
            DataSet dataSet = this.PATIENT_COMPLAINTS(BillId, conn);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Numbness/Tingling")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Numbness", "1");
                    }
                    acroFields.SetField("txt_F_Numbness", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Swelling")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Swelling", "1");
                    }
                    acroFields.SetField("txt_F_Swelling", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Pain")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Pain", "1");
                    }
                    acroFields.SetField("txt_F_Pain", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Weakness")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Weakness", "1");
                    }
                    acroFields.SetField("txt_F_Weakness", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Stiffness")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Stiffness", "1");
                    }
                    acroFields.SetField("txt_F_Stiffness", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Other(Specify)")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_Other", "1");
                    }
                    acroFields.SetField("txt_F_Other", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet2 = this.Patientinjury(BillId, conn);
            for (int j = 0; j < dataSet2.Tables[0].Rows.Count; j++)
            {
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Abrasion")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Abrasion", "1");
                    }
                    acroFields.SetField("txt_F_3_Abrasion", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Infectious Disease")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_InfectiousDisease", "1");
                    }
                    acroFields.SetField("txt_F_3_InfectiousDisease", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Amputation")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Amputation", "1");
                    }
                    acroFields.SetField("txt_F_3_Amputation", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Inhalation Exposure")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Inhalation_Exposure", "1");
                    }
                    acroFields.SetField("txt_F_3_Inhalation_Exposure", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Avulsion")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Avulsion", "1");
                    }
                    acroFields.SetField("txt_F_3_Avulsion", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Laceration")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Laceration", "1");
                    }
                    acroFields.SetField("txt_F_3_Laceration", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Bite")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Bite", "1");
                    }
                    acroFields.SetField("txt_F_3_Bite", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Needle Stick")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_NeedleStick", "1");
                    }
                    acroFields.SetField("txt_F_3_NeedleStick", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Burn")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Burn", "1");
                    }
                    acroFields.SetField("txt_F_3_Burn", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Poisoning/Toxic Effects")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Poisoning", "1");
                    }
                    acroFields.SetField("txt_F_3_Poisoning", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Contusion/Hematoma")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Contusion", "1");
                    }
                    acroFields.SetField("txt_F_3_Contusion", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Psychological")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Psychological", "1");
                    }
                    acroFields.SetField("txt_F_3_Psychological", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Crush Injury")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_CurshInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_CurshInjury", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Puncture Wound")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_PuntureWound", "1");
                    }
                    acroFields.SetField("txt_F_3_PuntureWound", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Dermatitis")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("form1[0].P2[0].chk_F_3_Dermatitis", "Yes");
                    }
                    acroFields.SetField("txt_F_3_Dermatitis", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Repetitive Strain Injury")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_RepetitiveStrainInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_RepetitiveStrainInjury", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Dislocation")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Dislocation", "1");
                    }
                    acroFields.SetField("txt_F_3_Dislocation", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Spinal Cord Injury")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_SpinalCordInjury", "1");
                    }
                    acroFields.SetField("txt_F_3_SpinalCordInjury", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Fracture")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Fracture", "1");
                    }
                    acroFields.SetField("txt_F_3_Fracture", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Sprain/Strain")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Sprain", "1");
                    }
                    acroFields.SetField("txt_F_3_Sprain", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Hearing Loss")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_HearingLoss", "1");
                    }
                    acroFields.SetField("txt_F_3_HearingLoss", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Torn Ligament Tendon or Muscle")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Torn", "1");
                    }
                    acroFields.SetField("txt_F_3_Torn", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Hernia")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Hernia", "1");
                    }
                    acroFields.SetField("txt_F_3_Hernia", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Vision Loss")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_VisionLoss", "1");
                    }
                    acroFields.SetField("txt_F_3_VisionLoss", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Other(Specify) :")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_3_Other", "1");
                    }
                    acroFields.SetField("txt_F_3_Other", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet3 = this.PatientPhysicalExam(BillId, conn);
            for (int k = 0; k < dataSet3.Tables[0].Rows.Count; k++)
            {
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "None at present" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_7_None", "1");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Bruising")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Bruising", "1");
                    }
                    acroFields.SetField("txt_F_4_Bruising", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Burns")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Burns", "1");
                    }
                    acroFields.SetField("txt_F_4_Burns", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Crepitation")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Crepitation", "1");
                    }
                    acroFields.SetField("txt_F_4_Crepitation", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Deformity")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Deformity", "1");
                    }
                    acroFields.SetField("txt_F_4_Deformity", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Edema")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Edema", "1");
                    }
                    acroFields.SetField("txt_F_4_Edema", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Hematoma/Lump/Swelling")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Hematoma", "1");
                    }
                    acroFields.SetField("txt_F_4_Hematoma", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Joint Effusion")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_JointEffusion", "1");
                    }
                    acroFields.SetField("txt_F_4_JointEffusion", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Laceration/Sutures")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Laceration", "1");
                    }
                    acroFields.SetField("txt_F_4_Laceration", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Pain/Tenderness")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Pain", "1");
                    }
                    acroFields.SetField("txt_F_4_Pain", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Scar")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Scar", "1");
                    }
                    acroFields.SetField("txt_F_4_Scar", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Other(Specify) ::")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_OtherFindings", "1");
                    }
                    acroFields.SetField("txt_F_4_OtherFindings", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Neuromuscular Findings:" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_4_Neuromuscular", "1");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Abnormal/Restricted ROM" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_4_Abnormal", "1");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Active ROM")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_ActiveROM", "1");
                    }
                    acroFields.SetField("txt_F_4_ActiveROM", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Passive ROM")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_PassiveROM", "1");
                    }
                    acroFields.SetField("txt_F_4_PassiveROM", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Gait")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Gait", "1");
                    }
                    acroFields.SetField("txt_F_4_Gait", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Palpable Muscle Spasm")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Palpable", "1");
                    }
                    acroFields.SetField("txt_F_4_Palpable", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Reflexes")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Reflexes", "1");
                    }
                    acroFields.SetField("txt_F_4_Reflexes", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Sensation")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Sensation", "1");
                    }
                    acroFields.SetField("txt_F_4_Sensation", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Strength (Weakness)")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Strength", "1");
                    }
                    acroFields.SetField("txt_F_4_Strength", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Wasting/Muscle Atrophy")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_4_Wasting", "1");
                    }
                    acroFields.SetField("txt_F_4_Wasting", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet4 = this.DiagnosticTests(BillId, conn);
            for (int l = 0; l < dataSet4.Tables[0].Rows.Count; l++)
            {
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "CT Scan" && dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_CTScan", "1");
                }
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "EMG/NCS" && dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_3_EMGNCS", "1");
                }
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "MRI (specify):")
                {
                    if (dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_MRI", "1");
                    }
                    acroFields.SetField("txt_H_3_MRI", dataSet4.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Labs (specify):")
                {
                    if (dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Labs", "1");
                    }
                    acroFields.SetField("txt_H_3_Labs", dataSet4.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "X-rays (specify):")
                {
                    if (dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_XRay", "1");
                    }
                    acroFields.SetField("txt_H_3_XRay", dataSet4.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet4.Tables[0].Rows[l]["SZ_TEXT"].ToString() == "Other (specify):")
                {
                    if (dataSet4.Tables[0].Rows[l]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_3_Left_Other", "1");
                    }
                    acroFields.SetField("txt_H_3_Left_Other", dataSet4.Tables[0].Rows[l]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet5 = this.Referrals(BillId, conn);
            for (int m = 0; m < dataSet5.Tables[0].Rows.Count; m++)
            {
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Chiropractor" && dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("form1[0].P4[0].chk_H_3_Chiropractor", "1");
                }
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Internist/Family Physician" && dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("form1[0].P4[0].chk_H_3_Interist", "1");
                }
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Occupational Therapist" && dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("form1[0].P4[0].chk_H_3_Occupational", "1");
                }
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Physical Therapist" && dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("form1[0].P4[0].chk_H_3_Physical", "1");
                }
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Specialist in")
                {
                    if (dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("form1[0].P4[0].chk_H_3_Specialist", "1");
                    }
                    acroFields.SetField("txt_H_3_Specialist", dataSet5.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet5.Tables[0].Rows[m]["SZ_TEXT"].ToString() == "Other (specify)")
                {
                    if (dataSet5.Tables[0].Rows[m]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("form1[0].P4[0].chk_H_3_Right_Other", "1");
                    }
                    acroFields.SetField("txt_H_3_Right_Other", dataSet5.Tables[0].Rows[m]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet6 = this.WorkStatus(BillId, conn);
            for (int n = 0; n < dataSet6.Tables[0].Rows.Count; n++)
            {
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Bending/twisting" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Bending", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Lifting" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Lifting", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Sitting" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Sitting", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Climbing stairs/ladders" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Climbing", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Operating heavy equipment" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_OperatingHeavy", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Standing" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Standing", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Environmental conditions" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_EnvironmentalCond", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Operation of motor vehicles" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_OpMotorVehicle", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Use of public transportation" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_UsePublicTrans", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Kneeling" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_Kneeling", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Personal protective equipment" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_PersonalProtectiveEq", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Use of upper extremities" && dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_I_2_C_UseOfUpperExtremeities", "1");
                }
                if (dataSet6.Tables[0].Rows[n]["SZ_TEXT"].ToString() == "Other (explain):")
                {
                    if (dataSet6.Tables[0].Rows[n]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_I_2_C_Other", "1");
                    }
                    acroFields.SetField("form1[0].P4[0].SZ_LIMITATION_DURATION", dataSet6.Tables[0].Rows[n]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet7 = this.AssistiveDevices(BillId, conn);
            for (int num4 = 0; num4 < dataSet7.Tables[0].Rows.Count; num4++)
            {
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Cane" && dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Cane", "1");
                }
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Crutches" && dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Crutches", "1");
                }
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Orthotics" && dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Orthotics", "1");
                }
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Walker" && dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_Walker", "1");
                }
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Wheelchair" && dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_H_4_WheelChair", "1");
                }
                if (dataSet7.Tables[0].Rows[num4]["SZ_TEXT"].ToString() == "Other-(specify):")
                {
                    if (dataSet7.Tables[0].Rows[num4]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_H_4_Other", "1");
                    }
                    acroFields.SetField("txt_H_4_Other", dataSet7.Tables[0].Rows[num4]["SZ_DESCRIPTION"].ToString());
                }
            }
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC42Part1(string CompnayId, string CaseId, string BillId, string CompanyName)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_2_P1"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value_C = this.GetValue_C42(BillId);
            acroFields.SetField("DT_DATE_OF_EXAMINATION", value_C.Tables[0].Rows[0]["DT_DATE_OF_EXAMINATION"].ToString());
            acroFields.SetField("SZ_WCB_NO", value_C.Tables[0].Rows[0]["SZ_WCB_NO"].ToString());
            acroFields.SetField("SZ_CARRIER_CASE_NO", value_C.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_MIDDLE_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_MIDDLE_NAME"].ToString());
            acroFields.SetField("DT_DATE_OF_ACCIDENT", value_C.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString());
            acroFields.SetField("SZ_SSN_NO", value_C.Tables[0].Rows[0]["SZ_SSN_NO"].ToString());
            acroFields.SetField("SZ_ADDRESS", value_C.Tables[0].Rows[0]["SZ_ADDRESS"].ToString());
            acroFields.SetField("SZ_CITY", value_C.Tables[0].Rows[0]["SZ_CITY"].ToString());
            acroFields.SetField("SZ_STATE", value_C.Tables[0].Rows[0]["SZ_STATE"].ToString());
            acroFields.SetField("SZ_ZIP", value_C.Tables[0].Rows[0]["SZ_ZIP"].ToString());
            acroFields.SetField("SZ_PATIENT_ACCOUNT_NO", value_C.Tables[0].Rows[0]["SZ_PATIENT_ACCOUNT_NO"].ToString());
            acroFields.SetField("SZ_DOCTOR_NAME", value_C.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
            acroFields.SetField("SZ_WCB_AOTH_NO", value_C.Tables[0].Rows[0]["SZ_WCB_AOTH_NO"].ToString());
            acroFields.SetField("SZ_WCB_RATING_CODE", value_C.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString());
            acroFields.SetField("SZ_FEDERAL_TAX_ID", value_C.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
            acroFields.SetField("SZ_OFFICE_ADDRESS", value_C.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
            acroFields.SetField("SZ_OFFICE_CITY", value_C.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString());
            acroFields.SetField("SZ_OFFICE_STATE", value_C.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString());
            acroFields.SetField("SZ_OFFICE_ZIP_CODE", value_C.Tables[0].Rows[0]["SZ_OFFICE_ZIP_CODE"].ToString());
            acroFields.SetField("SZ_BILLING_GROUP", value_C.Tables[0].Rows[0]["SZ_BILLING_GROUP"].ToString());
            acroFields.SetField("SZ_BILLING_ADDRESS", value_C.Tables[0].Rows[0]["SZ_BILLING_ADDRESS"].ToString());
            acroFields.SetField("SZ_BILLING_CITY", value_C.Tables[0].Rows[0]["SZ_BILLING_CITY"].ToString());
            acroFields.SetField("SZ_BILLING_STATE", value_C.Tables[0].Rows[0]["SZ_BILLING_STATE"].ToString());
            acroFields.SetField("SZ_BILLING_ZIP_CODE", value_C.Tables[0].Rows[0]["SZ_BILLING_ZIP_CODE"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE", value_C.Tables[0].Rows[0]["SZ_OFFICE_PHONE"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE", value_C.Tables[0].Rows[0]["SZ_BILLING_PHONE"].ToString());
            acroFields.SetField("SZ_NPI", value_C.Tables[0].Rows[0]["SZ_NPI"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY_CODE", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY_CODE"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY_ADDRESS", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY_ADDRESS"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS_CODE", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_CODE"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS_TREATEMENT_RENDERED", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_TREATEMENT_RENDERED"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS_TREATEMENT_RENDERED_1", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_TREATEMENT_RENDERED_1"].ToString());
            if (value_C.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "0")
            {
                acroFields.SetField("CHK_SSN", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "1")
            {
                acroFields.SetField("CHK_EIN", "Yes");
            }
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC42Part1(string CompnayId, string CaseId, string BillId, string CompanyName, ServerConnection conn)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_2_P1"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value_C = this.GetValue_C42(BillId, conn);
            acroFields.SetField("DT_DATE_OF_EXAMINATION", value_C.Tables[0].Rows[0]["DT_DATE_OF_EXAMINATION"].ToString());
            acroFields.SetField("SZ_WCB_NO", value_C.Tables[0].Rows[0]["SZ_WCB_NO"].ToString());
            acroFields.SetField("SZ_CARRIER_CASE_NO", value_C.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString());
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_MIDDLE_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_MIDDLE_NAME"].ToString());
            acroFields.SetField("DT_DATE_OF_ACCIDENT", value_C.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString());
            acroFields.SetField("SZ_SSN_NO", value_C.Tables[0].Rows[0]["SZ_SSN_NO"].ToString());
            acroFields.SetField("SZ_ADDRESS", value_C.Tables[0].Rows[0]["SZ_ADDRESS"].ToString());
            acroFields.SetField("SZ_CITY", value_C.Tables[0].Rows[0]["SZ_CITY"].ToString());
            acroFields.SetField("SZ_STATE", value_C.Tables[0].Rows[0]["SZ_STATE"].ToString());
            acroFields.SetField("SZ_ZIP", value_C.Tables[0].Rows[0]["SZ_ZIP"].ToString());
            acroFields.SetField("SZ_PATIENT_ACCOUNT_NO", value_C.Tables[0].Rows[0]["SZ_PATIENT_ACCOUNT_NO"].ToString());
            acroFields.SetField("SZ_DOCTOR_NAME", value_C.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString());
            acroFields.SetField("SZ_WCB_AOTH_NO", value_C.Tables[0].Rows[0]["SZ_WCB_AOTH_NO"].ToString());
            acroFields.SetField("SZ_WCB_RATING_CODE", value_C.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString());
            acroFields.SetField("SZ_FEDERAL_TAX_ID", value_C.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"].ToString());
            acroFields.SetField("SZ_OFFICE_ADDRESS", value_C.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString());
            acroFields.SetField("SZ_OFFICE_CITY", value_C.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString());
            acroFields.SetField("SZ_OFFICE_STATE", value_C.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString());
            acroFields.SetField("SZ_OFFICE_ZIP_CODE", value_C.Tables[0].Rows[0]["SZ_OFFICE_ZIP_CODE"].ToString());
            acroFields.SetField("SZ_BILLING_GROUP", value_C.Tables[0].Rows[0]["SZ_BILLING_GROUP"].ToString());
            acroFields.SetField("SZ_BILLING_ADDRESS", value_C.Tables[0].Rows[0]["SZ_BILLING_ADDRESS"].ToString());
            acroFields.SetField("SZ_BILLING_CITY", value_C.Tables[0].Rows[0]["SZ_BILLING_CITY"].ToString());
            acroFields.SetField("SZ_BILLING_STATE", value_C.Tables[0].Rows[0]["SZ_BILLING_STATE"].ToString());
            acroFields.SetField("SZ_BILLING_ZIP_CODE", value_C.Tables[0].Rows[0]["SZ_BILLING_ZIP_CODE"].ToString());
            acroFields.SetField("SZ_OFFICE_PHONE", value_C.Tables[0].Rows[0]["SZ_OFFICE_PHONE"].ToString());
            acroFields.SetField("SZ_BILLING_PHONE", value_C.Tables[0].Rows[0]["SZ_BILLING_PHONE"].ToString());
            acroFields.SetField("SZ_NPI", value_C.Tables[0].Rows[0]["SZ_NPI"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY_CODE", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY_CODE"].ToString());
            acroFields.SetField("SZ_INSURANCE_COMPANY_ADDRESS", value_C.Tables[0].Rows[0]["SZ_INSURANCE_COMPANY_ADDRESS"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS_CODE", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_CODE"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS_TREATEMENT_RENDERED", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_TREATEMENT_RENDERED"].ToString());
            acroFields.SetField("SZ_DIAGNOSIS_TREATEMENT_RENDERED_1", value_C.Tables[0].Rows[0]["SZ_DIAGNOSIS_TREATEMENT_RENDERED_1"].ToString());
            if (value_C.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "0")
            {
                acroFields.SetField("CHK_SSN", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BIT_TAX_ID_TYPE"].ToString() == "1")
            {
                acroFields.SetField("CHK_EIN", "Yes");
            }
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC42Part2(string CompnayId, string CaseId, string BillId, string CompanyName)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_2_P2"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value_C = this.GetValue_C42(BillId);
            if (value_C.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString() != "")
            {
                float[] fieldPositions = acroFields.GetFieldPositions("SZ_HEALT_CARE_NAME");
                if (fieldPositions == null || fieldPositions.Length <= 0)
                {
                    throw new ApplicationException("Error locating field");
                }
                float absoluteX = 139.5f;
                float num = 93.75f;
                float num2 = 370.5f;
                float num3 = 77.25f;
                Image instance = Image.GetInstance(value_C.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString());
                instance.ScaleToFit(num - 25f, num3 - 25f);
                instance.SetAbsolutePosition(absoluteX, num2 - 307f);
                PdfContentByte overContent = pdfStamper.GetOverContent(1);
                overContent.AddImage(instance);
            }
            else
            {
                acroFields.SetField("SZ_HEALT_CARE_NAME", value_C.Tables[0].Rows[0]["SZ_HEALT_CARE_NAME"].ToString());
            }
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_MIDDLE_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_MIDDLE_NAME"].ToString());
            acroFields.SetField("DT_DATE_OF_ACCIDENT", value_C.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString());
            acroFields.SetField("SZ_NATURE_OF_INJURY", value_C.Tables[0].Rows[0]["SZ_NATURE_OF_INJURY"].ToString());
            acroFields.SetField("SZ_NATURE_OF_INJURY_1", value_C.Tables[0].Rows[0]["SZ_NATURE_OF_INJURY_1"].ToString());
            acroFields.SetField("SZ_AFFECTED_BY_INJURY", value_C.Tables[0].Rows[0]["SZ_AFFECTED_BY_INJURY"].ToString());
            acroFields.SetField("SZ_CHANGES_IN_TREATMENT_PLAN", value_C.Tables[0].Rows[0]["SZ_CHANGES_IN_TREATMENT_PLAN"].ToString());
            acroFields.SetField("SZ_TREATEMENT_RENDERED", value_C.Tables[0].Rows[0]["SZ_TREATEMENT_RENDERED"].ToString());
            acroFields.SetField("SZ_TREATEMENT_RENDERED_1", value_C.Tables[0].Rows[0]["SZ_TREATEMENT_RENDERED_1"].ToString());
            acroFields.SetField("FT_TEMPORARY_IMPAIRMENT_PERCENTAGE", value_C.Tables[0].Rows[0]["FT_TEMPORARY_IMPAIRMENT_PERCENTAGE"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS", value_C.Tables[0].Rows[0]["SZ_TEST_RESULTS"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS_1", value_C.Tables[0].Rows[0]["SZ_TEST_RESULTS_1"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION_0", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION0"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATIONMM", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATIONMM"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTIONMM", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTIONMM"].ToString());
            acroFields.SetField("SZ_QUANTIFY_LIMITATION", value_C.Tables[0].Rows[0]["SZ_QUANTIFY_LIMITATION"].ToString());
            acroFields.SetField("Text156", value_C.Tables[0].Rows[0]["Text156"].ToString());
            acroFields.SetField("SZ_BOARD_PROVIDER_NAME", value_C.Tables[0].Rows[0]["SZ_BOARD_PROVIDER_NAME"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value_C.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("CUR_MONTH", value_C.Tables[0].Rows[0]["CUR_MONTH"].ToString());
            if (value_C.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "0")
            {
                acroFields.SetField("chk_D_5_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "1")
            {
                acroFields.SetField("chk_d_5_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "0")
            {
                acroFields.SetField("chk_D_7_withinweek", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "1")
            {
                acroFields.SetField("chk_D_7_1-2wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "2")
            {
                acroFields.SetField("chk_D_7_3-4wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "3")
            {
                acroFields.SetField("chk_D_7_5-6wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "4")
            {
                acroFields.SetField("chk_D_7_7-8wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "5")
            {
                acroFields.SetField("chk_D_7_mnths", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "6")
            {
                acroFields.SetField("chk_D_7_asneeded", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "0")
            {
                acroFields.SetField("chk_E_1_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "1")
            {
                acroFields.SetField("chk_E_1_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "0")
            {
                acroFields.SetField("chk_E_2_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "1")
            {
                acroFields.SetField("chk_E_2_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "0")
            {
                acroFields.SetField("chk_E_3_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "1")
            {
                acroFields.SetField("chk_E_3_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "2")
            {
                acroFields.SetField("chk_E_3_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_IS_WORKING"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkingYes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_IS_WORKING"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkingNo", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_WORK_RESTRICTION"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkRestrictYes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_WORK_RESTRICTION"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkRestrictNo", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_F_2_a", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_F_2_b", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "2")
            {
                acroFields.SetField("chk_F_2_c", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_1-2dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_3-7dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_8-14dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_15+dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_unknown", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_1-2dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_3-7dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_8-14dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_15+dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_unkown", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "5")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "0")
            {
                acroFields.SetField("chk_F_3_Patient", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "1")
            {
                acroFields.SetField("chk_F_3_Employer", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "2")
            {
                acroFields.SetField("chk_F_3_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_BENEFIT"].ToString() == "0")
            {
                acroFields.SetField("chk_F_4_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_BENEFIT"].ToString() == "1")
            {
                acroFields.SetField("chk_F_3_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "0")
            {
                acroFields.SetField("chk_F_4_ProvidedServices", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "1")
            {
                acroFields.SetField("chk_F_4_Supervised", "Yes");
            }
            DataSet dataSet = this.DIAGNOSTIC_TESTS_C42(BillId);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "CT Scan" && dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_ctscan", "Yes");
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "EMG/NCS" && dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_emg/ncs", "Yes");
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "MRI (specify):")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_mri", "Yes");
                    }
                    acroFields.SetField("txt_d_5mri", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Labs (specify):")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_labs", "Yes");
                    }
                    acroFields.SetField("txt_d_5labs", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "X-rays (specify):")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_xray", "Yes");
                    }
                    acroFields.SetField("txt_d_5xray", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Other (specify):")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_otherspecify1", "Yes");
                    }
                    acroFields.SetField("txt_d_5other1", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet2 = this.REFERRALS_C42(BillId);
            for (int j = 0; j < dataSet2.Tables[0].Rows.Count; j++)
            {
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Chiropractor" && dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_chiropractor", "Yes");
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Occupational Therapist" && dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_occutherapist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Internist/Family Physician" && dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_internist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Physical Therapist" && dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_phytherapist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Specialist in")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_specialist", "Yes");
                    }
                    acroFields.SetField("txt_d_5specialist", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Other (specify)")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_otherspecify2", "Yes");
                    }
                    acroFields.SetField("txt_d_5other2", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet3 = this.WORK_STATUS_C42(BillId);
            for (int k = 0; k < dataSet3.Tables[0].Rows.Count; k++)
            {
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Bending/twisting" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Bending", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Climbing stairs/ladders" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Climbing", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Environmental conditions" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Environment", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Kneeling" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Kneeling", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Other (explain):")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_2_OtherSpecify", "Yes");
                    }
                    acroFields.SetField("others", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Lifting" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Lifting", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Operating heavy equipment" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_OprEquip", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Operation of motor vehicles" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_vehical", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Personal protective equipment" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_proctectEquip", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Sitting" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Sitting", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Standing" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Standing", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Use of public transportation" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Pubtransport", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Use of upper extremities" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_UpperExtremist", "Yes");
                }
            }
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return bill_Sys_Data;
    }
    public object GeneratC42Part2(string CompnayId, string CaseId, string BillId, string CompanyName, ServerConnection conn)
    {
        Bill_Sys_Data bill_Sys_Data = new Bill_Sys_Data();
        try
        {
            string text = this.getFileName(BillId) + ".pdf";
            string path = this.getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), CompanyName, CaseId) + text;
            bill_Sys_Data.billnumber = BillId;
            bill_Sys_Data.billurl = text;
            bill_Sys_Data.companyid = CompnayId;
            PdfReader reader = new PdfReader(ConfigurationManager.AppSettings["PDF_FILE_PATH_C4_2_P2"].ToString());
            Document document = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
            AcroFields acroFields = pdfStamper.AcroFields;
            document.Open();
            DataSet value_C = this.GetValue_C42(BillId, conn);
            if (value_C.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString() != "")
            {
                float[] fieldPositions = acroFields.GetFieldPositions("SZ_HEALT_CARE_NAME");
                if (fieldPositions == null || fieldPositions.Length <= 0)
                {
                    throw new ApplicationException("Error locating field");
                }
                float absoluteX = 139.5f;
                float num = 93.75f;
                float num2 = 370.5f;
                float num3 = 77.25f;
                Image instance = Image.GetInstance(value_C.Tables[0].Rows[0]["SZ_PHYSICIAN_SIGN"].ToString());
                instance.ScaleToFit(num - 25f, num3 - 25f);
                instance.SetAbsolutePosition(absoluteX, num2 - 307f);
                PdfContentByte overContent = pdfStamper.GetOverContent(1);
                overContent.AddImage(instance);
            }
            else
            {
                acroFields.SetField("SZ_HEALT_CARE_NAME", value_C.Tables[0].Rows[0]["SZ_HEALT_CARE_NAME"].ToString());
            }
            acroFields.SetField("SZ_PATIENT_LAST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_FIRST_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString());
            acroFields.SetField("SZ_PATIENT_MIDDLE_NAME", value_C.Tables[0].Rows[0]["SZ_PATIENT_MIDDLE_NAME"].ToString());
            acroFields.SetField("DT_DATE_OF_ACCIDENT", value_C.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString());
            acroFields.SetField("SZ_NATURE_OF_INJURY", value_C.Tables[0].Rows[0]["SZ_NATURE_OF_INJURY"].ToString());
            acroFields.SetField("SZ_NATURE_OF_INJURY_1", value_C.Tables[0].Rows[0]["SZ_NATURE_OF_INJURY_1"].ToString());
            acroFields.SetField("SZ_AFFECTED_BY_INJURY", value_C.Tables[0].Rows[0]["SZ_AFFECTED_BY_INJURY"].ToString());
            acroFields.SetField("SZ_CHANGES_IN_TREATMENT_PLAN", value_C.Tables[0].Rows[0]["SZ_CHANGES_IN_TREATMENT_PLAN"].ToString());
            acroFields.SetField("SZ_TREATEMENT_RENDERED", value_C.Tables[0].Rows[0]["SZ_TREATEMENT_RENDERED"].ToString());
            acroFields.SetField("SZ_TREATEMENT_RENDERED_1", value_C.Tables[0].Rows[0]["SZ_TREATEMENT_RENDERED_1"].ToString());
            acroFields.SetField("FT_TEMPORARY_IMPAIRMENT_PERCENTAGE", value_C.Tables[0].Rows[0]["FT_TEMPORARY_IMPAIRMENT_PERCENTAGE"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS", value_C.Tables[0].Rows[0]["SZ_TEST_RESULTS"].ToString());
            acroFields.SetField("SZ_TEST_RESULTS_1", value_C.Tables[0].Rows[0]["SZ_TEST_RESULTS_1"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION_0", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION0"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTION", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTION"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_LIMITATIONMM", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_LIMITATIONMM"].ToString());
            acroFields.SetField("SZ_PATIENT_RETURN_WORK_DESCRIPTIONMM", value_C.Tables[0].Rows[0]["SZ_PATIENT_RETURN_WORK_DESCRIPTIONMM"].ToString());
            acroFields.SetField("SZ_QUANTIFY_LIMITATION", value_C.Tables[0].Rows[0]["SZ_QUANTIFY_LIMITATION"].ToString());
            acroFields.SetField("Text156", value_C.Tables[0].Rows[0]["Text156"].ToString());
            acroFields.SetField("SZ_BOARD_PROVIDER_NAME", value_C.Tables[0].Rows[0]["SZ_BOARD_PROVIDER_NAME"].ToString());
            acroFields.SetField("SZ_SPECIALITY", value_C.Tables[0].Rows[0]["SZ_SPECIALITY"].ToString());
            acroFields.SetField("CUR_MONTH", value_C.Tables[0].Rows[0]["CUR_MONTH"].ToString());
            if (value_C.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "0")
            {
                acroFields.SetField("chk_D_5_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_DIAGNOSTIC_REFERRALS_REQUIRED"].ToString() == "1")
            {
                acroFields.SetField("chk_d_5_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "0")
            {
                acroFields.SetField("chk_D_7_withinweek", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "1")
            {
                acroFields.SetField("chk_D_7_1-2wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "2")
            {
                acroFields.SetField("chk_D_7_3-4wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "3")
            {
                acroFields.SetField("chk_D_7_5-6wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "4")
            {
                acroFields.SetField("chk_D_7_7-8wks", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "5")
            {
                acroFields.SetField("chk_D_7_mnths", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["I_PATIENT_NEXT_APPOINTMENT"].ToString() == "6")
            {
                acroFields.SetField("chk_D_7_asneeded", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "0")
            {
                acroFields.SetField("chk_E_1_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_COMPETENT_MEDICAL_CAUSE"].ToString() == "1")
            {
                acroFields.SetField("chk_E_1_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "0")
            {
                acroFields.SetField("chk_E_2_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_HISTORY"].ToString() == "1")
            {
                acroFields.SetField("chk_E_2_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "0")
            {
                acroFields.SetField("chk_E_3_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "1")
            {
                acroFields.SetField("chk_E_3_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_IS_CONSISTENT_WITH_OBJECTIVE_FINDINGS"].ToString() == "2")
            {
                acroFields.SetField("chk_E_3_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_IS_WORKING"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkingYes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_IS_WORKING"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkingNo", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_WORK_RESTRICTION"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkRestrictYes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_WORK_RESTRICTION"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkRestrictNo", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "0")
            {
                acroFields.SetField("chk_F_2_a", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "1")
            {
                acroFields.SetField("chk_F_2_b", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_RETURN_WORK"].ToString() == "2")
            {
                acroFields.SetField("chk_F_2_c", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_1-2dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_3-7dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_8-14dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_15+dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_RESTRICTION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_F_1_WorkRestrictPeriod_unknown", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "0")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_1-2dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "1")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_3-7dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "2")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_8-14dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "3")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_15+dys", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "4")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_unkown", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_HOW_LONG_LIMITATION_APPLY"].ToString() == "5")
            {
                acroFields.SetField("chk_F_2_c_LimitPeriod_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "0")
            {
                acroFields.SetField("chk_F_3_Patient", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "1")
            {
                acroFields.SetField("chk_F_3_Employer", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["INT_WHOM_DISCUSS"].ToString() == "2")
            {
                acroFields.SetField("chk_F_3_NA", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_BENEFIT"].ToString() == "0")
            {
                acroFields.SetField("chk_F_4_Yes", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_PATIENT_BENEFIT"].ToString() == "1")
            {
                acroFields.SetField("chk_F_3_No", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "0")
            {
                acroFields.SetField("chk_F_4_ProvidedServices", "Yes");
            }
            if (value_C.Tables[0].Rows[0]["BT_BOARD_AUTHORIZED"].ToString() == "1")
            {
                acroFields.SetField("chk_F_4_Supervised", "Yes");
            }
            DataSet dataSet = this.DIAGNOSTIC_TESTS_C42(BillId, conn);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "CT Scan" && dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_ctscan", "Yes");
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "EMG/NCS" && dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_emg/ncs", "Yes");
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "MRI (specify):")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_mri", "Yes");
                    }
                    acroFields.SetField("txt_d_5mri", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Labs (specify):")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_labs", "Yes");
                    }
                    acroFields.SetField("txt_d_5labs", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "X-rays (specify):")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_xray", "Yes");
                    }
                    acroFields.SetField("txt_d_5xray", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet.Tables[0].Rows[i]["SZ_TEXT"].ToString() == "Other (specify):")
                {
                    if (dataSet.Tables[0].Rows[i]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_otherspecify1", "Yes");
                    }
                    acroFields.SetField("txt_d_5other1", dataSet.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet2 = this.REFERRALS_C42(BillId, conn);
            for (int j = 0; j < dataSet2.Tables[0].Rows.Count; j++)
            {
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Chiropractor" && dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_chiropractor", "Yes");
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Occupational Therapist" && dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_occutherapist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Internist/Family Physician" && dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_internist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Physical Therapist" && dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_d_5_phytherapist", "Yes");
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Specialist in")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_specialist", "Yes");
                    }
                    acroFields.SetField("txt_d_5specialist", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet2.Tables[0].Rows[j]["SZ_TEXT"].ToString() == "Other (specify)")
                {
                    if (dataSet2.Tables[0].Rows[j]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_d_5_otherspecify2", "Yes");
                    }
                    acroFields.SetField("txt_d_5other2", dataSet2.Tables[0].Rows[j]["SZ_DESCRIPTION"].ToString());
                }
            }
            DataSet dataSet3 = this.WORK_STATUS_C42(BillId, conn);
            for (int k = 0; k < dataSet3.Tables[0].Rows.Count; k++)
            {
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Bending/twisting" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Bending", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Climbing stairs/ladders" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Climbing", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Environmental conditions" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Environment", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Kneeling" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Kneeling", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Other (explain):")
                {
                    if (dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                    {
                        acroFields.SetField("chk_F_2_OtherSpecify", "Yes");
                    }
                    acroFields.SetField("others", dataSet3.Tables[0].Rows[k]["SZ_DESCRIPTION"].ToString());
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Lifting" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Lifting", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Operating heavy equipment" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_OprEquip", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Operation of motor vehicles" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_vehical", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Personal protective equipment" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_proctectEquip", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Sitting" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Sitting", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Standing" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Standing", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Use of public transportation" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_Pubtransport", "Yes");
                }
                if (dataSet3.Tables[0].Rows[k]["SZ_TEXT"].ToString() == "Use of upper extremities" && dataSet3.Tables[0].Rows[k]["SZ_VALUE"].ToString() == "1")
                {
                    acroFields.SetField("chk_F_2_UpperExtremist", "Yes");
                }
            }
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return bill_Sys_Data;
    }

    public DataSet GetValue_C42(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("[SP_WORKER_TEMPLATE_SPECIFIC_C4.2]", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("FLAG", "ALL");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet GetValue_C42(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec [SP_WORKER_TEMPLATE_SPECIFIC_C4.2] ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "ALL", ",");
            Query = Query.TrimEnd(',');

            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }
    public DataSet GetService_C42(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("[SP_WORKER_TEMPLATE_SPECIFIC_C4.2]", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("FLAG", "SERVICE_TABLE");
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet GetService_C42(string BillId, ServerConnection conn)


    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "[SP_WORKER_TEMPLATE_SPECIFIC_C4.2] ";
            Query = Query + string.Format("{0}={1}{2}", "@FLAG", "SERVICE_TABLE", ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }
    public DataSet DIAGNOSTIC_TESTS_C42(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_C_FOUR_TWO", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "DIAGNOSTIC_TESTS");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet DIAGNOSTIC_TESTS_C42(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_C_FOUR_TWO ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "DIAGNOSTIC_TESTS", ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }

    public DataSet REFERRALS_C42(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_C_FOUR_TWO", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "REFERRALS");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet REFERRALS_C42(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_C_FOUR_TWO ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "REFERRALS", ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }
    public DataSet WORK_STATUS_C42(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_C_FOUR_TWO", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "WORK_STATUS");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet WORK_STATUS_C42(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_C_FOUR_TWO ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "WORK_STATUS", ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }
    public DataSet GetValue(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "ALL");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
    public DataSet GetValue(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "ALL", ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {

        }
        return dataSet;
    }
    public DataSet GetServices(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@FLAG", "SERVICE_TABLE");
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
    public DataSet GetServices(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec SP_WORKER_TEMPLATE_SPECIFIC_PDF_NEW ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "SERVICE_TABLE", ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {

        }
        return dataSet;
    }

    public DataSet PATIENT_COMPLAINTS(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "PATIENT_COMPLAINTS");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
    public DataSet PATIENT_COMPLAINTS(string BillId, ServerConnection conn)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            string Query = "";
            Query = Query + "Exec SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4 ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "PATIENT_COMPLAINTS", ",");
            Query = Query.TrimEnd(',');


            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {

        }
        return dataSet;
    }
    public DataSet Patientinjury(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "PATIENT_TYPE_INJURY");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet Patientinjury(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4 ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "PATIENT_TYPE_INJURY", ",");
            Query = Query.TrimEnd(',');

            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }
    public DataSet PatientPhysicalExam(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "PATIENT_PHYSICAL_EXAM");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet PatientPhysicalExam(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4 ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "PATIENT_PHYSICAL_EXAM", ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }
    public DataSet DiagnosticTests(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "DIAGNOSTIC_TESTS");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet DiagnosticTests(string BillId, ServerConnection conn)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            string Query = "";
            Query = Query + "Exec SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4 ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "DIAGNOSTIC_TESTS", ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }
    public DataSet Referrals(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "REFERRALS");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet Referrals(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4 ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "REFERRALS", ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }
    public DataSet AssistiveDevices(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "ASSISTIVE_DEVICES");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet AssistiveDevices(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4 ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "ASSISTIVE_DEVICES", ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }
    private string getFileName(string p_szBillNumber)
    {
        DateTime now = DateTime.Now;
        return string.Concat(new string[]
        {
            p_szBillNumber,
            "_",
            this.getRandomNumber(),
            "_",
            now.ToString("yyyyMMddHHmmssms")
        });
    }

    private string getRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 10000).ToString();
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

    private string getApplicationSetting(string p_szKey)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        sqlConnection.Open();
        string result = "";
        SqlCommand sqlCommand = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = '" + p_szKey.ToString().Trim() + "'", sqlConnection);
        sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        while (sqlDataReader.Read())
        {
            result = sqlDataReader["parametervalue"].ToString();
        }
        return result;
    }

    public DataSet WorkStatus(string BillId)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        sqlConnection.Open();
        try
        {
            SqlCommand sqlCommand = new SqlCommand("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4", sqlConnection);
            sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", BillId);
            sqlCommand.Parameters.AddWithValue("@FLAG", "WORK_STATUS");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }
    public DataSet WorkStatus(string BillId, ServerConnection conn)
    {

        DataSet dataSet = new DataSet();

        try
        {
            string Query = "";
            Query = Query + "Exec SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_NEW_WC4 ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", BillId, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "WORK_STATUS", ",");
            Query = Query.TrimEnd(',');
            dataSet = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return dataSet;
    }
}
